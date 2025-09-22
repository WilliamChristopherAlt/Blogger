using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class MessageStatusType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
