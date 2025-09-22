using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class OtpType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OtpRequest> OtpRequests { get; set; } = new List<OtpRequest>();
}
