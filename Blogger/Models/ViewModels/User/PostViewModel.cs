using System;
using System.Collections.Generic;

namespace Blogger.Models.ViewModels.User
{
    public class PostViewModel
    {
        public int PostID { get; set; }
        public string Username { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Content { get; set; }
        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int FavoritesCount { get; set; }
        public int CommentsCount { get; set; }
        public bool HasMedia { get; set; }
        public int MediaCount { get; set; }
        public List<string> MediaUrls { get; set; } = new List<string>();
        public List<string> MediaTypes { get; set; } = new List<string>();
        public List<string> Hashtags { get; set; } = new List<string>();

        // For shared posts
        public bool IsSharedPost { get; set; }
        public string OriginalPostUsername { get; set; }
        public string OriginalPostContent { get; set; }
        public DateTime? OriginalPostCreatedAt { get; set; }

        // Computed properties
        public string FormattedCreatedAt => CreatedAt?.ToString("MMM dd, yyyy 'at' HH:mm") ?? "";
        public string FormattedUpdatedAt => UpdatedAt?.ToString("MMM dd, yyyy 'at' HH:mm") ?? "";
        public string FormattedOriginalCreatedAt => OriginalPostCreatedAt?.ToString("MMM dd, yyyy 'at' HH:mm") ?? "";
        public bool IsEdited => UpdatedAt.HasValue && UpdatedAt > CreatedAt;
        public string HashtagsString => string.Join(" ", Hashtags.Select(h => $"#{h}"));
    }
}