using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class FriendshipStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
}
