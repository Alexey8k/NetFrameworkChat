--login
----in -> hash(sha1 'login|password(sha1)')
----return -> 0 - ok, 1 - delete, 2 - lock, 3 - online, 4 - fail
CREATE PROCEDURE [Login]
@hash VARBINARY(100)
AS
DECLARE @result TINYINT = 4
SELECT @result=
	CASE 1
		WHEN [onLine] THEN 3
		WHEN [locked] THEN 2
		WHEN [deleted] THEN 1
		ELSE 0
	END
FROM [User] 
WHERE [hash]= @hash
IF @result=0 
	UPDATE [User] SET [onLine]=1 WHERE [hash]=@hash
SELECT @result result
GO

--Registration
----in -> hash(sha1 'login|password(sha1)')
----return -> 0 - ok, 1 - login, 2 - email
CREATE PROCEDURE [Registration]
@login VARCHAR(15),
@hash VARBINARY(100),
@email VARCHAR(50),
@color32 INT,
@roleId TINYINT
AS	   
IF @roleId IS NULL 
	SELECT TOP(1) @roleId = [id] FROM [Role] WHERE [name] = 'user'
DECLARE @result TINYINT = CASE 
			WHEN exists(SELECT '' FROM [User] WHERE [login]=@login)
				THEN 1
			WHEN exists(SELECT '' FROM [User] WHERE [email]=@email)
				THEN 2
			ELSE 0
		END
IF @result=0
	INSERT INTO [User]([login],[email],[hash],[roleId],[color32],[dateCreation])
		VALUES(@login,@email,@hash,@roleId,@color32,GETDATE())
SELECT @result [Result] 
GO

CREATE PROCEDURE [Logout]
@userId INT
AS
UPDATE [User] SET [onLine]=0 WHERE [id]=@userId
GO

CREATE PROCEDURE GetCurrentUser
@hash VARBINARY(100)
AS
SELECT [id],[login],[color32],[roleId] FROM [User]
	WHERE [hash]=@hash
GO

DROP FUNCTION IF EXISTS [in].[GetFriendsId]
GO
CREATE FUNCTION [in].[GetFriendsId](@userId INT)
RETURNS @friendId TABLE
(
	[value] INT
)
AS
BEGIN
	INSERT INTO @friendId([value])
		SELECT IIF([userId1]=@userId,[userId2],[userId1]) 
			FROM [RelationFriend] 
			WHERE @userId IN ([userId1],[userId2]) AND [dateStop] IS NULL AND [dateStart] IS NOT NULL
	RETURN
END
GO

DROP FUNCTION IF EXISTS [in].[GetBlackListUsersId]
GO
CREATE FUNCTION [in].[GetBlackListUsersId](@userId INT)
RETURNS @blacListUserId TABLE
(
	[value] INT
)
AS
BEGIN
	INSERT INTO @blacListUserId([value])
		SELECT [userIdLocked] 
			FROM [RelationBlackList] 
			WHERE @userId=[userId] AND [dateUnlock] IS NULL
	RETURN
END
GO

DROP FUNCTION IF EXISTS [in].[GetRelationInt]
GO
CREATE FUNCTION [in].[GetRelationInt](@strRelation VARCHAR(20))
RETURNS INT 
AS
BEGIN
	RETURN CASE @strRelation
		WHEN 'none' THEN 0
		WHEN 'friend' THEN 1
		WHEN 'blacklist' THEN 2
		ELSE NULL END
END
GO

DROP FUNCTION IF EXISTS [UserRelation]
GO 
CREATE FUNCTION [in].[UserRelation](@currentUserId INT,@relationUserId INT)
RETURNS INT 
AS
BEGIN
	RETURN CASE
		WHEN @relationUserId IN (SELECT [value] FROM [in].GetFriendsId(@currentUserId)) THEN [in].GetRelationInt('friend')
		WHEN @relationUserId IN (SELECT [value] FROM [in].GetBlackListUsersId(@currentUserId)) THEN [in].GetRelationInt('blacklist')
		ELSE [in].GetRelationInt('none') END
END
GO

DROP PROCEDURE IF EXISTS [GetOnlineUsers]
GO
CREATE PROCEDURE [GetOnlineUsers]
@currentUserId INT
AS 
SELECT [id],[login],[color32],[roleId],[in].UserRelation(@currentUserId,[id]) [relation]
	FROM [User]
	WHERE [id]<>@currentUserId AND [deleted]=0 AND [locked]=0 AND [onLine]=1
GO

DROP PROCEDURE IF EXISTS [GetUserRelation]
GO
CREATE PROCEDURE [GetUserRelation]
@currentUserId INT,
@relationUserId INT
AS
SELECT [in].UserRelation(@currentUserId, @relationUserId) [relation]
GO

DROP PROCEDURE IF EXISTS [GetUnreadMessages]
GO
CREATE PROCEDURE [GetUnreadMessages]
@userId INT
AS
SELECT m.id, m.text, m.date, m.statusId [status], u.login from [User] u
	JOIN (SELECT mm.* FROM [Message] mm
			JOIN [User] u ON u.lastVisitDate<=mm.date
			WHERE u.id=@userId) m 
		ON  m.ownerUserId=u.Id
	WHERE (m.statusId=[in].GetStatusId('Share') OR EXISTS(SELECT TOP 1 '' FROM [MessageUser] mu WHERE mu.messageId=m.id AND mu.userId=@userId))
		AND m.ownerUserId NOT IN (SELECT rbl.[userIdLocked] FROM [RelationBlackList] rbl WHERE rbl.userId=@userId)
GO

DROP FUNCTION IF EXISTS [in].[ToIntTable]
GO
CREATE FUNCTION [in].[ToIntTable](@str VARCHAR(MAX),@separator NVARCHAR(1))
RETURNS @intTable TABLE
(
	[value] INT
)
AS
BEGIN
	INSERT INTO @intTable([value])
		SELECT CAST(value AS INTEGER) FROM STRING_SPLIT(@str,@separator)
	RETURN
END
GO

DROP FUNCTION IF EXISTS [in].[GetStatusId]
GO
CREATE FUNCTION [in].[GetStatusId](@statusName VARCHAR(50))
RETURNS INT
AS
BEGIN
	DECLARE @id INT
    SELECT TOP 1 @id=[id] FROM [Status] WHERE [name]=@statusName
	RETURN @id
END
GO

DROP TYPE IF EXISTS IntTable
GO
CREATE TYPE IntTable AS TABLE 
(
	[value] INT NOT NULL
)
GO

DROP PROCEDURE IF EXISTS [in].[AddMessageUsers]
GO
CREATE PROCEDURE [in].[AddMessageUsers]
@messageId INT,
@userId IntTable READONLY
AS
	INSERT INTO [MessageUser]([messageId],[userId])
		SELECT @messageId,[value] FROM @userId
GO

DROP PROCEDURE IF EXISTS [AddMessage]
GO
CREATE PROCEDURE [AddMessage]
@text NVARCHAR(255),
@userId INT,
@statusId INT,
@usersId VARCHAR(MAX)=NULL
AS	
	DECLARE @shareId INT=[in].GetStatusId('share')
	IF @statusId<>@shareId AND (@usersId IS NULL OR @usersId='')
		THROW 
			51000, 
			'Error procedure "AddMessage":for param @statusId = 1, param @usersId not can is null or empty string.', 
			1
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO [Message] ([text],[date],[ownerUserId],[statusId])
			VALUES(@text,GETDATE(),@userId, @statusId)

		DECLARE @currentMassegeId INT=SCOPE_IDENTITY()
		DECLARE @tableUserId IntTable

		IF @statusId<>@shareId
			INSERT INTO @tableUserId([value])
				SELECT * FROM [in].ToIntTable(@usersId,',')
			
		IF EXISTS(SELECT TOP 1 '' FROM @tableUserId) 
			EXEC [in].AddMessageUsers @messageId=@currentMassegeId, @userId=@tableUserId
		COMMIT
	END TRY
	BEGIN CATCH
		IF @@trancount>0 ROLLBACK;
		THROW
	END CATCH
GO 

DROP PROCEDURE IF EXISTS [AddFriend]
GO
CREATE PROCEDURE [AddFriend]
@userId INT,
@usersId VARCHAR(MAX)
AS
INSERT INTO [Relationfriend]([userId1],[userId2])
	SELECT @userId, [value] FROM [in].ToIntTable(@usersId,',')
GO

DROP PROCEDURE IF EXISTS [AddBlackList]
GO
CREATE PROCEDURE [AddBlackList]
@userId INT,
@usersIdLocked VARCHAR(MAX)
AS
INSERT INTO [RelationBlackList]([userId],[userIdLocked])
	SELECT @userId, [value] FROM [in].ToIntTable(@usersIdLocked,',') 
GO

DROP PROCEDURE IF EXISTS [RemoveBlackList]
GO
CREATE PROCEDURE [RemoveBlackList]
@userId INT,
@usersIdLocked VARCHAR(MAX)
AS
UPDATE [RelationBlackList]
	SET [dateUnlock]=GETDATE()
	WHERE [userId]=@userId 
		AND [userIdLocked] IN (SELECT [value] FROM [in].ToIntTable(@usersIdLocked,',')) 
		AND [dateUnlock] IS NULL
GO


EXEC AddMessage 'test1',1,1,'2,2'

SELECT * FROM [Message]
SELECT * FROM [MessageUser]
SELECT * FROM [RelationBlackList]


DELETE [MessageUser]
DELETE [Message]



SELECT * FROM [in].ToIntTable('2',',')
