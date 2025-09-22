-- CREATE DATABASE Blogger;
-- GO
-- Uncomment if Database not created yet

USE Blogger;
GO

-- ===============================================
-- LOOKUP TABLES (All plural, no cascade issues)
-- ===============================================

CREATE TABLE GenderTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL
);

CREATE TABLE AudienceTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Public, Friends, OnlyMe
);

CREATE TABLE FriendshipStatuses (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Pending, Accepted, Following, Follower
);

CREATE TABLE ReactionTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Like, Love, Haha, Sad, etc.
);

CREATE TABLE MediaTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Image, Video, File, etc.
);

CREATE TABLE NotificationTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE GroupRoles (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Member, Admin, Moderator, etc.
);

CREATE TABLE OtpTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) UNIQUE NOT NULL -- EmailVerification, PasswordReset, TwoFactorAuth, etc.
);

CREATE TABLE MessageStatusTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Sent, Delivered, Read, Failed
);

CREATE TABLE BlockTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(30) UNIQUE NOT NULL -- Messages, Posts, Profile, Complete
);

CREATE TABLE PhotoTypes (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) UNIQUE NOT NULL -- Profile, Cover
);

-- ===============================================
-- CORE TABLES WITH SOFT DELETE FOR SPECIFIED TABLES
-- ===============================================

-- Users - Soft delete enabled
CREATE TABLE Users (
    ID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    PasswordSalt NVARCHAR(255) NOT NULL,
    Bio NVARCHAR(255),
    GenderTypeID INT,
    Birthday DATE,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    IsEmailVerified BIT DEFAULT 0,
    TwoFactorEnabled BIT DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    DeletedAt DATETIME NULL,
    FOREIGN KEY (GenderTypeID) REFERENCES GenderTypes(ID) ON DELETE SET NULL
);

-- UserPhotos - Regular table
CREATE TABLE UserPhotos (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    PhotoTypeID INT NOT NULL,
    PhotoURL NVARCHAR(255) NOT NULL,
    UploadedAt DATETIME DEFAULT GETDATE(),
    IsSelected BIT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE,
    FOREIGN KEY (PhotoTypeID) REFERENCES PhotoTypes(ID) ON DELETE CASCADE,
);

-- Ensure only one selected photo per user per photo type
CREATE UNIQUE INDEX IX_UserPhotos_OneSelectedPerType 
ON UserPhotos (UserID, PhotoTypeID) 
WHERE IsSelected = 1;

-- OtpRequests - Regular table
CREATE TABLE OtpRequests (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    OtpTypeID INT NOT NULL,
    OtpCode NVARCHAR(10) NOT NULL,
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    IsUsed BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ExpirationMinutes INT NOT NULL,
    UsedAt DATETIME NULL,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE,
    FOREIGN KEY (OtpTypeID) REFERENCES OtpTypes(ID) ON DELETE CASCADE
);

-- Friendships - Regular table
CREATE TABLE Friendships (
    ID INT PRIMARY KEY IDENTITY,
    SenderID INT NOT NULL,
    ReceiverID INT NOT NULL,
    FriendshipStatusID INT NOT NULL,
    RequestedAt DATETIME DEFAULT GETDATE(),
    AcceptedAt DATETIME NULL,
    CONSTRAINT UC_Friends UNIQUE (SenderID, ReceiverID),
    FOREIGN KEY (SenderID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (ReceiverID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (FriendshipStatusID) REFERENCES FriendshipStatuses(ID) ON DELETE CASCADE
);

-- UserBlocks - Regular table
CREATE TABLE UserBlocks (
    ID INT PRIMARY KEY IDENTITY,
    BlockerID INT NOT NULL,
    BlockedID INT NOT NULL,
    BlockTypeID INT NOT NULL,
    BlockedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT UC_Blocks UNIQUE (BlockerID, BlockedID, BlockTypeID),
    FOREIGN KEY (BlockerID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (BlockedID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (BlockTypeID) REFERENCES BlockTypes(ID) ON DELETE CASCADE
);

-- Posts - Soft delete enabled
CREATE TABLE Posts (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    Content NVARCHAR(MAX),
    AudienceTypeID INT NOT NULL,
    SharedPostID INT NULL, -- Reference to original post when sharing
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsDeleted BIT DEFAULT 0,
    DeletedAt DATETIME NULL,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE,
    FOREIGN KEY (AudienceTypeID) REFERENCES AudienceTypes(ID) ON DELETE CASCADE,
    FOREIGN KEY (SharedPostID) REFERENCES Posts(ID)
);

-- PostViews - Regular table
CREATE TABLE PostViews (
    ID INT PRIMARY KEY IDENTITY,
    PostID INT NOT NULL,
    UserID INT NOT NULL,
    ViewedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PostID) REFERENCES Posts(ID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE NO ACTION
);

-- PostMedias - Regular table
CREATE TABLE PostMedias (
    ID INT PRIMARY KEY IDENTITY,
    PostID INT NOT NULL,
    MediaTypeID INT NOT NULL,
    MediaURL NVARCHAR(255),
    FOREIGN KEY (PostID) REFERENCES Posts(ID) ON DELETE CASCADE,
    FOREIGN KEY (MediaTypeID) REFERENCES MediaTypes(ID) ON DELETE CASCADE
);

-- Polls - Regular table
CREATE TABLE Polls (
    ID INT PRIMARY KEY IDENTITY,
    PostID INT NOT NULL,
    Question NVARCHAR(500) NOT NULL,
    AllowMultipleChoices BIT DEFAULT 0,
    ExpiresAt DATETIME NULL, -- NULL means no expiration
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (PostID) REFERENCES Posts(ID) ON DELETE CASCADE
);

-- PollOptions - Regular table
CREATE TABLE PollOptions (
    ID INT PRIMARY KEY IDENTITY,
    PollID INT NOT NULL,
    OptionText NVARCHAR(200) NOT NULL,
    OptionOrder INT NOT NULL, -- For displaying options in order
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PollID) REFERENCES Polls(ID) ON DELETE CASCADE,
    CONSTRAINT UC_PollOption_Order UNIQUE (PollID, OptionOrder)
);

-- PollVotes - Regular table
CREATE TABLE PollVotes (
    ID INT PRIMARY KEY IDENTITY,
    PollOptionID INT NOT NULL,
    UserID INT NOT NULL,
    VotedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PollOptionID) REFERENCES PollOptions(ID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE NO ACTION,
    CONSTRAINT UC_UserPollOption UNIQUE (PollOptionID, UserID)
);

-- Comments - Soft delete enabled
CREATE TABLE Comments (
    ID INT PRIMARY KEY IDENTITY,
    PostID INT NOT NULL,
    ParentCommentID INT NULL,
    UserID INT NOT NULL,
    Content NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsDeleted BIT DEFAULT 0,
    DeletedAt DATETIME NULL,
    FOREIGN KEY (PostID) REFERENCES Posts(ID) ON DELETE CASCADE,
    FOREIGN KEY (ParentCommentID) REFERENCES Comments(ID) ON DELETE NO ACTION,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE NO ACTION
);

-- PostReactions - Regular table
CREATE TABLE PostReactions (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    ReactionTypeID INT NOT NULL,
    PostID INT NOT NULL,
    ReactedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (ReactionTypeID) REFERENCES ReactionTypes(ID) ON DELETE CASCADE,
    FOREIGN KEY (PostID) REFERENCES Posts(ID) ON DELETE CASCADE,
    CONSTRAINT UC_PostUserReaction UNIQUE (UserID, PostID)
);

-- CommentReactions - Regular table
CREATE TABLE CommentReactions (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    ReactionTypeID INT NOT NULL,
    CommentID INT NOT NULL,
    ReactedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (ReactionTypeID) REFERENCES ReactionTypes(ID) ON DELETE CASCADE,
    FOREIGN KEY (CommentID) REFERENCES Comments(ID) ON DELETE NO ACTION,
    CONSTRAINT UC_CommentUserReaction UNIQUE (UserID, CommentID)
);

-- Hashtags - Regular table
CREATE TABLE Hashtags (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) UNIQUE NOT NULL
);

-- PostHashtags - Regular table
CREATE TABLE PostHashtags (
    PostID INT NOT NULL,
    HashtagID INT NOT NULL,
    PRIMARY KEY (PostID, HashtagID),
    FOREIGN KEY (PostID) REFERENCES Posts(ID) ON DELETE CASCADE,
    FOREIGN KEY (HashtagID) REFERENCES Hashtags(ID) ON DELETE CASCADE
);

-- Groups - Soft delete enabled
CREATE TABLE Groups (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    CreatorID INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0,
    DeletedAt DATETIME NULL,
    FOREIGN KEY (CreatorID) REFERENCES Users(ID) ON DELETE CASCADE
);

-- GroupMembers - Regular table
CREATE TABLE GroupMembers (
    GroupID INT NOT NULL,
    UserID INT NOT NULL,
    GroupRoleID INT NOT NULL,
    JoinedAt DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (GroupID, UserID),
    FOREIGN KEY (GroupID) REFERENCES Groups(ID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (GroupRoleID) REFERENCES GroupRoles(ID) ON DELETE CASCADE
);

-- Messages - Soft delete enabled
CREATE TABLE Messages (
    ID INT PRIMARY KEY IDENTITY,
    SenderID INT NOT NULL,
    ReceiverID INT NOT NULL,
    Content NVARCHAR(1000),
    SentAt DATETIME DEFAULT GETDATE(),
    MessageStatusTypeID INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    DeletedAt DATETIME NULL,
    FOREIGN KEY (SenderID) REFERENCES Users(ID) ON DELETE NO ACTION,
    FOREIGN KEY (MessageStatusTypeID) REFERENCES MessageStatusTypes(ID) ON DELETE CASCADE,
    FOREIGN KEY (ReceiverID) REFERENCES Users(ID) ON DELETE NO ACTION
);

-- MessageMedias - Regular table
CREATE TABLE MessageMedias (
    ID INT PRIMARY KEY IDENTITY,
    MessageID INT NOT NULL,
    MediaTypeID INT NOT NULL,
    MediaURL NVARCHAR(255),
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MessageID) REFERENCES Messages(ID) ON DELETE CASCADE,
    FOREIGN KEY (MediaTypeID) REFERENCES MediaTypes(ID) ON DELETE CASCADE
);

-- Notifications - Regular table
CREATE TABLE Notifications (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    NotificationTypeID INT NOT NULL,
    ReferenceID INT,
    Message NVARCHAR(255),
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(ID) ON DELETE CASCADE,
    FOREIGN KEY (NotificationTypeID) REFERENCES NotificationTypes(ID) ON DELETE CASCADE
);

GO

-- ===============================================
-- TRIGGERS TO PREVENT HARD DELETES
-- ===============================================

-- Prevent hard delete of Users
CREATE TRIGGER TR_Users_Prevent_Delete
ON Users
FOR DELETE
AS
BEGIN
    RAISERROR('Hard delete not allowed on Users table. Use soft delete by setting IsDeleted = 1.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent hard delete of Posts
CREATE TRIGGER TR_Posts_Prevent_Delete
ON Posts
FOR DELETE
AS
BEGIN
    RAISERROR('Hard delete not allowed on Posts table. Use soft delete by setting IsDeleted = 1.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent hard delete of Comments
CREATE TRIGGER TR_Comments_Prevent_Delete
ON Comments
FOR DELETE
AS
BEGIN
    RAISERROR('Hard delete not allowed on Comments table. Use soft delete by setting IsDeleted = 1.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent hard delete of Groups
CREATE TRIGGER TR_Groups_Prevent_Delete
ON Groups
FOR DELETE
AS
BEGIN
    RAISERROR('Hard delete not allowed on Groups table. Use soft delete by setting IsDeleted = 1.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent hard delete of Messages
CREATE TRIGGER TR_Messages_Prevent_Delete
ON Messages
FOR DELETE
AS
BEGIN
    RAISERROR('Hard delete not allowed on Messages table. Use soft delete by setting IsDeleted = 1.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- ===============================================
-- TRIGGERS TO PREVENT UPDATES ON TYPES TABLES
-- ===============================================

-- Prevent updates on GenderTypes
CREATE TRIGGER TR_GenderTypes_Prevent_Update
ON GenderTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on GenderTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on AudienceTypes
CREATE TRIGGER TR_AudienceTypes_Prevent_Update
ON AudienceTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on AudienceTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on FriendshipStatuses
CREATE TRIGGER TR_FriendshipStatuses_Prevent_Update
ON FriendshipStatuses
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on FriendshipStatuses table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on ReactionTypes
CREATE TRIGGER TR_ReactionTypes_Prevent_Update
ON ReactionTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on ReactionTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on MediaTypes
CREATE TRIGGER TR_MediaTypes_Prevent_Update
ON MediaTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on MediaTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on NotificationTypes
CREATE TRIGGER TR_NotificationTypes_Prevent_Update
ON NotificationTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on NotificationTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on GroupRoles
CREATE TRIGGER TR_GroupRoles_Prevent_Update
ON GroupRoles
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on GroupRoles table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on OtpTypes
CREATE TRIGGER TR_OtpTypes_Prevent_Update
ON OtpTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on OtpTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on MessageStatusTypes
CREATE TRIGGER TR_MessageStatusTypes_Prevent_Update
ON MessageStatusTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on MessageStatusTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on BlockTypes
CREATE TRIGGER TR_BlockTypes_Prevent_Update
ON BlockTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on BlockTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO

-- Prevent updates on PhotoTypes
CREATE TRIGGER TR_PhotoTypes_Prevent_Update
ON PhotoTypes
FOR UPDATE
AS
BEGIN
    RAISERROR('Updates not allowed on PhotoTypes table.', 16, 1);
    ROLLBACK TRANSACTION;
END;

GO