using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class PollOption
{
    public int Id { get; set; }

    public int PollId { get; set; }

    public string OptionText { get; set; } = null!;

    public int OptionOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Poll Poll { get; set; } = null!;

    public virtual ICollection<PollVote> PollVotes { get; set; } = new List<PollVote>();
}
