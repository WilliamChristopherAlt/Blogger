using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class Message
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string? Content { get; set; }

    public DateTime? SentAt { get; set; }

    public int MessageStatusTypeId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();

    public virtual MessageStatusType MessageStatusType { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
