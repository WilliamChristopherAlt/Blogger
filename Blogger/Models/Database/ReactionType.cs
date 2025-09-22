using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class ReactionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<PostReaction> PostReactions { get; set; } = new List<PostReaction>();
}
