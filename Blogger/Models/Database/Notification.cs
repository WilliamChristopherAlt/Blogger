using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int NotificationTypeId { get; set; }

    public int? ReferenceId { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual NotificationType NotificationType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
