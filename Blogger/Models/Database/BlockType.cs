using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class BlockType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserBlock> UserBlocks { get; set; } = new List<UserBlock>();
}
