using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class CommentReaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ReactionTypeId { get; set; }

    public int CommentId { get; set; }

    public DateTime? ReactedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual ReactionType ReactionType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
