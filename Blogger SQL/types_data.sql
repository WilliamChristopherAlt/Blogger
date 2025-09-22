USE Blogger;
GO

-- Insert Gender Types
INSERT INTO GenderTypes (Name) VALUES 
('Male'),
('Female'),
('Non-binary'),
('Prefer not to say'),
('Other');

-- Insert Audience Types
INSERT INTO AudienceTypes (Name) VALUES 
('Public'),
('Friends'),
('OnlyMe');

-- Insert Friendship Statuses
INSERT INTO FriendshipStatuses (Name) VALUES 
('Pending'),
('Accepted'),
('Declined');

-- Insert Reaction Types
INSERT INTO ReactionTypes (Name) VALUES 
('Like'),
('Dislike'),
('Favorite');

-- Insert Media Types
INSERT INTO MediaTypes (Name) VALUES 
('JPG'),
('MP4'),
('MP3'),
('PDF'),
('GIF');

-- Insert Notification Types
INSERT INTO NotificationTypes (Name) VALUES 
('Friend Request'),
('Friend Request Accepted'),
('Post Like'),
('Post Comment'),
('Comment Reply'),
('Post Share'),
('Mention in Post'),
('Mention in Comment'),
('Group Invitation'),
('Group Post'),
('Message Received'),
('Birthday Reminder'),
('Post Reaction'),
('Comment Reaction'),
('New Follower'),
('Group Member Added'),
('Group Role Changed'),
('Event Invitation'),
('Memory Reminder'),
('Profile View');

-- Insert Group Roles
INSERT INTO GroupRoles (Name) VALUES 
('Member'),
('Moderator'),
('Creator');

-- Insert OTP Types
INSERT INTO OtpTypes (Name) VALUES 
('Email Verification'),
('Password Reset'),
('Two Factor Auth'),
('Phone Verification'),
('Account Recovery'),
('Login Verification'),
('Email Change');

-- Insert Message Status Types
INSERT INTO MessageStatusTypes (Name) VALUES 
('Sent'),
('Read'),
('Deleted');

-- Insert Block Types
INSERT INTO BlockTypes (Name) VALUES 
('Messages'),
('Profile'),
('Notifications'),
('Complete');

-- Insert Photo Types
INSERT INTO PhotoTypes (Name) VALUES 
('Profile'),
('Cover');

GO