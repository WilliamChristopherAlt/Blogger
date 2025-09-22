using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class MediaType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();

    public virtual ICollection<PostMedia> PostMedia { get; set; } = new List<PostMedia>();
}
