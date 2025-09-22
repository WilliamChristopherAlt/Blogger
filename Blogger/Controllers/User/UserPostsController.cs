using Blogger.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Controllers.User
{
    public class UserPostsController : Controller
    {
        private readonly BloggerContext _context;

        public UserPostsController(BloggerContext context)
        {
            _context = context;
        }

        // GET: User/UserPosts
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            // FIXED: Simplified the query and removed overly restrictive filtering
            var posts = await _context.Posts
                .Where(p => p.IsDeleted == false)
                .Include(p => p.User)
                    .ThenInclude(u => u.UserPhotos) // Load all user photos, filter in view
                .Include(p => p.AudienceType)
                .Include(p => p.PostMedias)
                    .ThenInclude(pm => pm.MediaType)
                .Include(p => p.PostReactions)
                    .ThenInclude(pr => pr.ReactionType)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                    .ThenInclude(c => c.User)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                    .ThenInclude(c => c.CommentReactions)
                .Include(p => p.Hashtags)
                .Include(p => p.SharedPost)
                    .ThenInclude(sp => sp.User)
                .Include(p => p.Polls)
                    .ThenInclude(poll => poll.PollOptions)
                        .ThenInclude(po => po.PollVotes) // Ensure poll votes are loaded
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsSplitQuery()
                .ToListAsync();

            // Get total count separately (more efficient)
            var totalPosts = await _context.Posts.CountAsync(p => p.IsDeleted == false);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize);
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < ViewBag.TotalPages;

            return View("Views/User/UserPosts/Index.cshtml", posts);
        }

        [HttpPost]
        public async Task<IActionResult> React(int postId, int reactionTypeId)
        {
            try
            {
                // Get current user ID (implement your user authentication logic)
                var userId = GetCurrentUserId(); // You need to implement this method

                // Check if user already reacted
                var existingReaction = await _context.PostReactions
                    .FirstOrDefaultAsync(pr => pr.PostId == postId && pr.UserId == userId);

                if (existingReaction != null)
                {
                    if (existingReaction.ReactionTypeId == reactionTypeId)
                    {
                        // Remove reaction if same type
                        _context.PostReactions.Remove(existingReaction);
                    }
                    else
                    {
                        // Update reaction type
                        existingReaction.ReactionTypeId = reactionTypeId;
                    }
                }
                else
                {
                    // Add new reaction
                    var newReaction = new PostReaction
                    {
                        PostId = postId,
                        UserId = userId,
                        ReactionTypeId = reactionTypeId
                    };
                    _context.PostReactions.Add(newReaction);
                }

                await _context.SaveChangesAsync();

                // Get updated reaction counts
                var reactionCounts = await _context.PostReactions
                    .Where(pr => pr.PostId == postId)
                    .Include(pr => pr.ReactionType)
                    .GroupBy(pr => pr.ReactionTypeId)
                    .Select(g => new {
                        reactionTypeId = g.Key,
                        count = g.Count(),
                        name = g.First().ReactionType.Name
                    })
                    .ToListAsync();

                return Json(new { success = true, reactions = reactionCounts });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // GET: User/UserPosts/Details/5 - Also optimized
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Where(p => p.Id == id && p.IsDeleted == false)
                .Include(p => p.User)
                    .ThenInclude(u => u.UserPhotos.Where(up => up.IsSelected == true && up.PhotoTypeId == 1))
                .Include(p => p.AudienceType)
                .Include(p => p.PostMedias)
                    .ThenInclude(pm => pm.MediaType)
                .Include(p => p.PostReactions)
                    .ThenInclude(pr => pr.ReactionType)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                    .ThenInclude(c => c.User)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                    .ThenInclude(c => c.CommentReactions)
                        .ThenInclude(cr => cr.ReactionType)
                .Include(p => p.Hashtags)
                .Include(p => p.SharedPost)
                    .ThenInclude(sp => sp.User)
                .Include(p => p.Polls)
                    .ThenInclude(poll => poll.PollOptions)
                        .ThenInclude(po => po.PollVotes)
                            .ThenInclude(pv => pv.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        private int GetCurrentUserId()
        {
            return 1; // Placeholder - implement your auth logic
        }
    }
}