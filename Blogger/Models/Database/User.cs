using System;
using System.Collections.Generic;

namespace Blogger.Models.Database;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string? Bio { get; set; }

    public int? GenderTypeId { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsEmailVerified { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Community> Communities { get; set; } = new List<Community>();

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual ICollection<Friendship> FriendshipReceivers { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipSenders { get; set; } = new List<Friendship>();

    public virtual GenderType? GenderType { get; set; }

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<OtpRequest> OtpRequests { get; set; } = new List<OtpRequest>();

    public virtual ICollection<PollVote> PollVotes { get; set; } = new List<PollVote>();

    public virtual ICollection<PostReaction> PostReactions { get; set; } = new List<PostReaction>();

    public virtual ICollection<PostView> PostViews { get; set; } = new List<PostView>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UserBlock> UserBlockBlockeds { get; set; } = new List<UserBlock>();

    public virtual ICollection<UserBlock> UserBlockBlockers { get; set; } = new List<UserBlock>();

    public virtual ICollection<UserPhoto> UserPhotos { get; set; } = new List<UserPhoto>();
}
