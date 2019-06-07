--SET NOCOUNT ON
--GO

--IF DB_ID('NetFrameworkChatDb') IS NOT NULL
--BEGIN
--	USE [master]
--	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'NetFrameworkChatDb'
--	ALTER DATABASE [NetFrameworkChatDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
--	DROP DATABASE [NetFrameworkChatDb]
--END
--GO

--CREATE DATABASE [NetFrameworkChatDb]
--GO

--USE [NetFrameworkChatDb]
--GO

ALTER DATABASE [NetFrameworkChatDb] SET COMPATIBILITY_LEVEL = 130
GO

IF SCHEMA_ID('in') IS NULL
    EXEC('CREATE SCHEMA [in]');
GO

CREATE TABLE [Role]
(
	[id] TINYINT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(25) NOT NULL,
	CONSTRAINT PK_Role PRIMARY KEY([id])
)
GO

CREATE TABLE [User]
(
	[id] INT IDENTITY(1,1) NOT NULL,
	[login] VARCHAR(15) NOT NULL,
	[hash] VARBINARY(100) NOT NULL,
	[roleId] TINYINT NOT NULL,
	[color32] INT NOT NULL,
	[email] VARCHAR(50) NOT NULL,
	[dateCreation] DATETIME NOT NULL DEFAULT GETDATE(),
	[lastVisitDate] DATETIME NULL,
	[deleted] BIT NOT NULL CONSTRAINT DF_User_Deleted DEFAULT 0,
	[locked] BIT NOT NULL CONSTRAINT DF_User_Locked DEFAULT 0,
	[onLine] BIT NOT NULL CONSTRAINT DF_User_OnLine DEFAULT 0,
	CONSTRAINT PK_User PRIMARY KEY([id]),
	CONSTRAINT FK_User_Role FOREIGN KEY([roleId])
	REFERENCES [Role]([id]),
	CONSTRAINT CK_User_DeletedLocked CHECK(deleted=0 or (deleted<>locked)),
	CONSTRAINT UQ_User_Login UNIQUE([login]),
	CONSTRAINT UQ_User_Email UNIQUE([email])
)
GO

CREATE TABLE [Status]
(
	[id] INT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(50) NOT NULL,
	CONSTRAINT PK_MessageStatus PRIMARY KEY([id])
)
GO

CREATE TABLE [Message]
(
	[id] INT IDENTITY(1,1) NOT NULL,
	[text] VARCHAR(255) NOT NULL,
	[date] DATETIME NOT NULL,
	[ownerUserId] INT NOT NULL,
	[statusId] INT NOT NULL,
	CONSTRAINT PK_Message PRIMARY KEY([id]),
	CONSTRAINT FK_Message_User FOREIGN KEY([ownerUserId])
	REFERENCES [User]([id])
)
GO

--private end friend messages
CREATE TABLE [MessageUser]
(
	[messageId] INT NOT NULL,
	[userId] INT NOT NULL,
	CONSTRAINT FK_MessageUser_Message FOREIGN KEY([messageId])
	REFERENCES [Message]([id]),
	CONSTRAINT FK_MessageUser_User FOREIGN KEY([userId])
	REFERENCES [User]([id]),
	CONSTRAINT UQ_MessageUser UNIQUE([messageId],[userId]) 
)
GO

CREATE TABLE [LogUserDeleted]
(
	[idUser] INT NOT NULL,
	[dateDeleted] DATETIME NOT NULL DEFAULT GETDATE(),
	[dateRecovery] DATETIME NULL,
	CONSTRAINT FK_LogUserDeleted_User FOREIGN KEY([idUser])
	REFERENCES [User]([id])
)
GO

CREATE TABLE [Cause]
(
	[id] TINYINT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(100) NOT NULL,
	CONSTRAINT PK_Cause PRIMARY KEY([id]),
	CONSTRAINT UQ_Cause_Name UNIQUE([name])
)
GO

CREATE TABLE [LogUserLocked]
(
	[userId] INT NOT NULL,
	[userIdAdminLock] INT NOT NULL,
	[userIdAdminUnlock] INT NULL,
	[causeId] TINYINT NULL,
	[anotherCause] VARCHAR(150) NULL,
	[lockSpanMinutes] INT NULL,
	[dateLock] DATETIME NOT NULL DEFAULT GETDATE(),
	[dateUnlock] DATETIME NULL,
	CONSTRAINT FK_LogUserLocked_User FOREIGN KEY([userId])
	REFERENCES [User]([id]),
	CONSTRAINT FK_LogUserLocked_UserAdminLock FOREIGN KEY([userIdAdminLock])
	REFERENCES [User]([id]),
	CONSTRAINT FK_LogUserLocked_UserAdminUnlock FOREIGN KEY([userIdAdminUnlock])
	REFERENCES [User]([id]),
	CONSTRAINT FK_LogUserLocked_Cause FOREIGN KEY([causeId])
	REFERENCES [Cause]([id])
)
GO

CREATE TABLE [LogUserOnLine]
(
	[userId] INT NOT NULL,
	[dateIn] DATETIME NOT NULL DEFAULT GETDATE(),
	[dateOut] DATETIME NULL,
	CONSTRAINT FK_LogUserOnLine_User FOREIGN KEY([userId])
	REFERENCES [User]([id])
)
GO

DROP TABLE IF EXISTS [RelationFriend]
CREATE TABLE [RelationFriend]
(
	[userId1] INT NOT NULL,
	[userId2] INT NOT NULL,
	[dateStart] DATETIME NULL,
	[dateStop] DATETIME NULL,
	CONSTRAINT FK_RelationFriend_UserInitiator FOREIGN KEY([userId1])
	REFERENCES [User]([id]),
	CONSTRAINT FK_RelationFriend_UserRelation FOREIGN KEY([userId2])
	REFERENCES [User]([id])
)
GO

DROP TABLE IF EXISTS [RelationBlackList]
CREATE TABLE [RelationBlackList]
(
	[userId] INT NOT NULL,
	[userIdLocked] INT NOT NULL,
	[dateLock] DATETIME NOT NULL DEFAULT GETDATE(),
	[dateUnlock] DATETIME NULL,
	CONSTRAINT FK_RelationBlackList_UserInitiator FOREIGN KEY([userId])
	REFERENCES [User]([id]),
	CONSTRAINT FK_RelationBlackList_UserRelation FOREIGN KEY([userIdLocked])
	REFERENCES [User]([id])
)
GO

-------------------------Init Data-------------------------------

INSERT INTO [Status]([name])
	VALUES('private'),('frand'),('share')
GO

INSERT INTO [Role]([name])
	VALUES('sa'),('admin'),('user')
GO

INSERT INTO [User]([login],[email],[hash],[roleId],[color32])
	VALUES('sa','sa@mail.com',HASHBYTES('SHA1', 'sa|123'),1,44444),
		('user','user@mail.com',HASHBYTES('SHA1', 'user|123'),3,44444),
		('cat','cat@mail.com',HASHBYTES('SHA1', 'cat|123'),3,44444)
GO

CREATE USER [DefaultAppPool] FOR LOGIN [IIS AppPool\DefaultAppPool]
	WITH DEFAULT_SCHEMA = dbo
GO

ALTER ROLE db_datareader ADD MEMBER [DefaultAppPool]
GO

ALTER ROLE db_datawriter ADD MEMBER [DefaultAppPool]
GO

GRANT EXEC TO [DefaultAppPool]
GO

GRANT CONNECT TO [DefaultAppPool]
GO

--SET NOCOUNT OFF
--GO