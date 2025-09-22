using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class PostView
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public DateTime? ViewedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
