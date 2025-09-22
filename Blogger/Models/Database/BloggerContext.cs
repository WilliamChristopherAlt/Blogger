using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Models.Database;

public partial class BloggerContext : DbContext
{
    public BloggerContext()
    {
    }

    public BloggerContext(DbContextOptions<BloggerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AudienceType> AudienceTypes { get; set; }

    public virtual DbSet<BlockType> BlockTypes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReaction> CommentReactions { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<CommunityMember> CommunityMembers { get; set; }

    public virtual DbSet<CommunityRole> CommunityRoles { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<FriendshipStatus> FriendshipStatuses { get; set; }

    public virtual DbSet<GenderType> GenderTypes { get; set; }

    public virtual DbSet<Hashtag> Hashtags { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageMedia> MessageMedias { get; set; }

    public virtual DbSet<MessageStatusType> MessageStatusTypes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<OtpRequest> OtpRequests { get; set; }

    public virtual DbSet<OtpType> OtpTypes { get; set; }

    public virtual DbSet<PhotoType> PhotoTypes { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<PollOption> PollOptions { get; set; }

    public virtual DbSet<PollVote> PollVotes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostMedia> PostMedias { get; set; }

    public virtual DbSet<PostReaction> PostReactions { get; set; }

    public virtual DbSet<PostView> PostViews { get; set; }

    public virtual DbSet<ReactionType> ReactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBlock> UserBlocks { get; set; }

    public virtual DbSet<UserPhoto> UserPhotos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0BQ9RBN\\SQLEXPRESS;Database=Blogger;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AudienceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Audience__3214EC273BB6D07A");

            entity.ToTable(tb => tb.HasTrigger("TR_AudienceTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__Audience__737584F6C8D8EDBD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<BlockType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlockTyp__3214EC273267C891");

            entity.ToTable(tb => tb.HasTrigger("TR_BlockTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__BlockTyp__737584F66BD04928").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC27BECECFEF");

            entity.ToTable(tb => tb.HasTrigger("TR_Comments_Prevent_Delete"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ParentCommentId).HasColumnName("ParentCommentID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK__Comments__Parent__3D807D68");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comments__PostID__3C8C592F");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__UserID__3E74A1A1");
        });

        modelBuilder.Entity<CommentReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CommentR__3214EC276C7DB092");

            entity.HasIndex(e => new { e.UserId, e.CommentId }, "UC_CommentUserReaction").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.ReactedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReactionTypeId).HasColumnName("ReactionTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CommentRe__Comme__4BCE9CBF");

            entity.HasOne(d => d.ReactionType).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.ReactionTypeId)
                .HasConstraintName("FK__CommentRe__React__4ADA7886");

            entity.HasOne(d => d.User).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CommentRe__UserI__49E6544D");
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC279B574705");

            entity.ToTable(tb => tb.HasTrigger("TR_Communities_Prevent_Delete"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Creator).WithMany(p => p.Communities)
                .HasForeignKey(d => d.CreatorId)
                .HasConstraintName("FK__Communiti__Creat__57404F6B");
        });

        modelBuilder.Entity<CommunityMember>(entity =>
        {
            entity.HasKey(e => new { e.CommunityId, e.UserId }).HasName("PK__Communit__1DD2D7C33203F92D");

            entity.Property(e => e.CommunityId).HasColumnName("CommunityID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CommunityRoleId).HasColumnName("CommunityRoleID");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK__Community__Commu__5B10E04F");

            entity.HasOne(d => d.CommunityRole).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.CommunityRoleId)
                .HasConstraintName("FK__Community__Commu__5CF928C1");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Community__UserI__5C050488");
        });

        modelBuilder.Entity<CommunityRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC272DAAEE58");

            entity.ToTable(tb => tb.HasTrigger("TR_CommunitiyRoles_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__Communit__737584F6665D0EBA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => new { e.SenderId, e.ReceiverId }).HasName("PK__Friendsh__D4A2245B94028174");

            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.AcceptedAt).HasColumnType("datetime");
            entity.Property(e => e.FriendshipStatusId).HasColumnName("FriendshipStatusID");
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FriendshipStatus).WithMany(p => p.Friendships)
                .HasForeignKey(d => d.FriendshipStatusId)
                .HasConstraintName("FK__Friendshi__Frien__0FB9B2B8");

            entity.HasOne(d => d.Receiver).WithMany(p => p.FriendshipReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friendshi__Recei__0EC58E7F");

            entity.HasOne(d => d.Sender).WithMany(p => p.FriendshipSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friendshi__Sende__0DD16A46");
        });

        modelBuilder.Entity<FriendshipStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friendsh__3214EC275D57FDB5");

            entity.ToTable(tb => tb.HasTrigger("TR_FriendshipStatuses_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__Friendsh__737584F6523B4D86").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<GenderType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GenderTy__3214EC27F8D1F024");

            entity.ToTable(tb => tb.HasTrigger("TR_GenderTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__GenderTy__737584F6B0624EDA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Hashtag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hashtags__3214EC27B446A2B4");

            entity.HasIndex(e => e.Name, "UQ__Hashtags__737584F6DD091FDB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MediaTyp__3214EC27E42339E3");

            entity.ToTable(tb => tb.HasTrigger("TR_MediaTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__MediaTyp__737584F6C9AB3E31").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3214EC2725CDA5D2");

            entity.ToTable(tb => tb.HasTrigger("TR_Messages_Prevent_Delete"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MessageStatusTypeId).HasColumnName("MessageStatusTypeID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MessageStatusType).WithMany(p => p.Messages)
                .HasForeignKey(d => d.MessageStatusTypeId)
                .HasConstraintName("FK__Messages__Messag__62B20217");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Receiv__63A62650");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__61BDDDDE");
        });

        modelBuilder.Entity<MessageMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageM__3214EC27E87BC933");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");
            entity.Property(e => e.MediaUrl)
                .HasMaxLength(255)
                .HasColumnName("MediaURL");
            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MediaType).WithMany(p => p.MessageMedia)
                .HasForeignKey(d => d.MediaTypeId)
                .HasConstraintName("FK__MessageMe__Media__686ADB6D");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageMedia)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("FK__MessageMe__Messa__6776B734");
        });

        modelBuilder.Entity<MessageStatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MessageS__3214EC274EBB2A78");

            entity.ToTable(tb => tb.HasTrigger("TR_MessageStatusTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__MessageS__737584F6790B7619").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC27603EE37C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");
            entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("FK__Notificat__Notif__6E23B4C3");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__6D2F908A");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC278C018D14");

            entity.ToTable(tb => tb.HasTrigger("TR_NotificationTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__Notifica__737584F67D3C3062").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<OtpRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OtpReque__3214EC277ACB8BED");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.OtpCode).HasMaxLength(10);
            entity.Property(e => e.OtpTypeId).HasColumnName("OtpTypeID");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UsedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.OtpType).WithMany(p => p.OtpRequests)
                .HasForeignKey(d => d.OtpTypeId)
                .HasConstraintName("FK__OtpReques__OtpTy__0A00D962");

            entity.HasOne(d => d.User).WithMany(p => p.OtpRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__OtpReques__UserI__090CB529");
        });

        modelBuilder.Entity<OtpType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OtpTypes__3214EC27AA5B0D7D");

            entity.ToTable(tb => tb.HasTrigger("TR_OtpTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__OtpTypes__737584F6FBBFB976").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PhotoType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhotoTyp__3214EC2749D4502A");

            entity.ToTable(tb => tb.HasTrigger("TR_PhotoTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__PhotoTyp__737584F6171A1337").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Polls__3214EC27AF5A44AE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AllowMultipleChoices).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Question).HasMaxLength(500);

            entity.HasOne(d => d.Post).WithMany(p => p.Polls)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Polls__PostID__2D4A159F");
        });

        modelBuilder.Entity<PollOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PollOpti__3214EC2760101DE4");

            entity.HasIndex(e => new { e.PollId, e.OptionOrder }, "UC_PollOption_Order").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OptionText).HasMaxLength(200);
            entity.Property(e => e.PollId).HasColumnName("PollID");

            entity.HasOne(d => d.Poll).WithMany(p => p.PollOptions)
                .HasForeignKey(d => d.PollId)
                .HasConstraintName("FK__PollOptio__PollI__320ECABC");
        });

        modelBuilder.Entity<PollVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PollVote__3214EC2729E97EDD");

            entity.HasIndex(e => new { e.PollOptionId, e.UserId }, "UC_UserPollOption").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PollOptionId).HasColumnName("PollOptionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VotedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.PollOption).WithMany(p => p.PollVotes)
                .HasForeignKey(d => d.PollOptionId)
                .HasConstraintName("FK__PollVotes__PollO__36D37FD9");

            entity.HasOne(d => d.User).WithMany(p => p.PollVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PollVotes__UserI__37C7A412");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC276D716BCB");

            entity.ToTable(tb => tb.HasTrigger("TR_Posts_Prevent_Delete"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AudienceTypeId).HasColumnName("AudienceTypeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.SharedPostId).HasColumnName("SharedPostID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AudienceType).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AudienceTypeId)
                .HasConstraintName("FK__Posts__AudienceT__1E07D20F");

            entity.HasOne(d => d.SharedPost).WithMany(p => p.InverseSharedPost)
                .HasForeignKey(d => d.SharedPostId)
                .HasConstraintName("FK__Posts__SharedPos__1EFBF648");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Posts__UserID__1D13ADD6");

            entity.HasMany(d => d.Hashtags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostHashtag",
                    r => r.HasOne<Hashtag>().WithMany()
                        .HasForeignKey("HashtagId")
                        .HasConstraintName("FK__PostHasht__Hasht__527B9A4E"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK__PostHasht__PostI__51877615"),
                    j =>
                    {
                        j.HasKey("PostId", "HashtagId").HasName("PK__PostHash__11FDC9349A15E8A3");
                        j.ToTable("PostHashtags");
                        j.IndexerProperty<int>("PostId").HasColumnName("PostID");
                        j.IndexerProperty<int>("HashtagId").HasColumnName("HashtagID");
                    });
        });

        modelBuilder.Entity<PostMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostMedi__3214EC27685C9715");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");
            entity.Property(e => e.MediaUrl)
                .HasMaxLength(255)
                .HasColumnName("MediaURL");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.MediaType).WithMany(p => p.PostMedia)
                .HasForeignKey(d => d.MediaTypeId)
                .HasConstraintName("FK__PostMedia__Media__27913C49");

            entity.HasOne(d => d.Post).WithMany(p => p.PostMedias)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__PostMedia__PostI__269D1810");
        });

        modelBuilder.Entity<PostReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostReac__3214EC27E4DD013C");

            entity.HasIndex(e => new { e.UserId, e.PostId }, "UC_PostUserReaction").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.ReactedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReactionTypeId).HasColumnName("ReactionTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.PostReactions)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__PostReact__PostI__45219F30");

            entity.HasOne(d => d.ReactionType).WithMany(p => p.PostReactions)
                .HasForeignKey(d => d.ReactionTypeId)
                .HasConstraintName("FK__PostReact__React__442D7AF7");

            entity.HasOne(d => d.User).WithMany(p => p.PostReactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostReact__UserI__433956BE");
        });

        modelBuilder.Entity<PostView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostView__3214EC278C74C612");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ViewedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.PostViews)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__PostViews__PostI__22CC872C");

            entity.HasOne(d => d.User).WithMany(p => p.PostViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostViews__UserI__23C0AB65");
        });

        modelBuilder.Entity<ReactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reaction__3214EC2735ED21B8");

            entity.ToTable(tb => tb.HasTrigger("TR_ReactionTypes_Prevent_Update"));

            entity.HasIndex(e => e.Name, "UQ__Reaction__737584F61B7A7A9C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC270F588BA5");

            entity.ToTable(tb => tb.HasTrigger("TR_Users_Prevent_Delete"));

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4C07751AC").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053481A500B5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bio).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GenderTypeId).HasColumnName("GenderTypeID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsEmailVerified).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
            entity.Property(e => e.TwoFactorEnabled).HasDefaultValue(false);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.GenderType).WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__GenderTyp__7E8F26B6");
        });

        modelBuilder.Entity<UserBlock>(entity =>
        {
            entity.HasKey(e => new { e.BlockerId, e.BlockedId }).HasName("PK__UserBloc__416BCA142880C276");

            entity.Property(e => e.BlockerId).HasColumnName("BlockerID");
            entity.Property(e => e.BlockedId).HasColumnName("BlockedID");
            entity.Property(e => e.BlockedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Blocked).WithMany(p => p.UserBlockBlockeds)
                .HasForeignKey(d => d.BlockedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserBlock__Block__147E67D5");

            entity.HasOne(d => d.Blocker).WithMany(p => p.UserBlockBlockers)
                .HasForeignKey(d => d.BlockerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserBlock__Block__138A439C");

            entity.HasMany(d => d.BlockTypes).WithMany(p => p.UserBlocks)
                .UsingEntity<Dictionary<string, object>>(
                    "UserBlockDetail",
                    r => r.HasOne<BlockType>().WithMany()
                        .HasForeignKey("BlockTypeId")
                        .HasConstraintName("FK__UserBlock__Block__184EF8B9"),
                    l => l.HasOne<UserBlock>().WithMany()
                        .HasForeignKey("BlockerId", "BlockedId")
                        .HasConstraintName("FK__UserBlockDetails__175AD480"),
                    j =>
                    {
                        j.HasKey("BlockerId", "BlockedId", "BlockTypeId").HasName("PK__UserBloc__DE143EC4C4D97586");
                        j.ToTable("UserBlockDetails");
                        j.IndexerProperty<int>("BlockerId").HasColumnName("BlockerID");
                        j.IndexerProperty<int>("BlockedId").HasColumnName("BlockedID");
                        j.IndexerProperty<int>("BlockTypeId").HasColumnName("BlockTypeID");
                    });
        });

        modelBuilder.Entity<UserPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPhot__3214EC27D93534D9");

            entity.HasIndex(e => new { e.UserId, e.PhotoTypeId }, "IX_UserPhotos_OneSelectedPerType")
                .IsUnique()
                .HasFilter("([IsSelected]=(1))");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsSelected).HasDefaultValue(false);
            entity.Property(e => e.PhotoTypeId).HasColumnName("PhotoTypeID");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("PhotoURL");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PhotoType).WithMany(p => p.UserPhotos)
                .HasForeignKey(d => d.PhotoTypeId)
                .HasConstraintName("FK__UserPhoto__Photo__0448000C");

            entity.HasOne(d => d.User).WithMany(p => p.UserPhotos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPhoto__UserI__0353DBD3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
