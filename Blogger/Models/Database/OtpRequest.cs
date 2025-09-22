using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class OtpRequest
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OtpTypeId { get; set; }

    public string OtpCode { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool? IsUsed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int ExpirationMinutes { get; set; }

    public DateTime? UsedAt { get; set; }

    public virtual OtpType OtpType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
