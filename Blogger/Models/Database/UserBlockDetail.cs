using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class UserBlockDetail
{
    public int Id { get; set; }

    public int UserBlockId { get; set; }

    public int BlockTypeId { get; set; }

    public virtual BlockType BlockType { get; set; } = null!;

    public virtual UserBlock UserBlock { get; set; } = null!;
}
