using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class GenderType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
