using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class PhotoType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserPhoto> UserPhotos { get; set; } = new List<UserPhoto>();
}
