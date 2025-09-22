using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class Poll
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public string Question { get; set; } = null!;

    public bool? AllowMultipleChoices { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<PollOption> PollOptions { get; set; } = new List<PollOption>();

    public virtual Post Post { get; set; } = null!;
}
