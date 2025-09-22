using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class PollVote
{
    public int Id { get; set; }

    public int PollOptionId { get; set; }

    public int UserId { get; set; }

    public DateTime? VotedAt { get; set; }

    public virtual PollOption PollOption { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
