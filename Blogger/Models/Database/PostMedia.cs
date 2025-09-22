using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class PostMedia
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int MediaTypeId { get; set; }

    public string? MediaUrl { get; set; }

    public virtual MediaType MediaType { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
