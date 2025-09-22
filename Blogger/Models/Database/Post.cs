using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public int AudienceTypeId { get; set; }

    public int? SharedPostId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual AudienceType AudienceType { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Post> InverseSharedPost { get; set; } = new List<Post>();

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();

    public virtual ICollection<PostMedia> PostMedias { get; set; } = new List<PostMedia>();

    public virtual ICollection<PostReaction> PostReactions { get; set; } = new List<PostReaction>();

    public virtual ICollection<PostView> PostViews { get; set; } = new List<PostView>();

    public virtual Post? SharedPost { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
}
