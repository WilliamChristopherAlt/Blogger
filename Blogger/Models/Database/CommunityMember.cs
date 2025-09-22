using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class CommunityMember
{
    public int CommunityId { get; set; }

    public int UserId { get; set; }

    public int CommunityRoleId { get; set; }

    public DateTime? JoinedAt { get; set; }

    public virtual Community Community { get; set; } = null!;

    public virtual CommunityRole CommunityRole { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
