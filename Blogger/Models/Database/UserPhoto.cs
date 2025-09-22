using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class UserPhoto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PhotoTypeId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public bool? IsSelected { get; set; }

    public virtual PhotoType PhotoType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
