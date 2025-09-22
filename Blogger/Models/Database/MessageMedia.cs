using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class MessageMedia
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public int MediaTypeId { get; set; }

    public string? MediaUrl { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual MediaType MediaType { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
