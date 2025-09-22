using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class UserBlock
{
    public int BlockerId { get; set; }

    public int BlockedId { get; set; }

    public DateTime? BlockedAt { get; set; }

    public virtual User Blocked { get; set; } = null!;

    public virtual User Blocker { get; set; } = null!;

    public virtual ICollection<BlockType> BlockTypes { get; set; } = new List<BlockType>();
}
