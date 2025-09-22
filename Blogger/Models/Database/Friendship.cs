using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class Friendship
{
    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public int FriendshipStatusId { get; set; }

    public DateTime? RequestedAt { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public virtual FriendshipStatus FriendshipStatus { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
