using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class CommunityRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();
}
