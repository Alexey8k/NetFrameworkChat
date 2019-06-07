CREATE TRIGGER [OnlineUserLog]
ON [User]
FOR UPDATE
AS		   
IF UPDATE([online])
BEGIN
	IF EXISTS(SELECT '' FROM [inserted] i JOIN [deleted] d ON i.[onLine]=d.[onLine])
		ROLLBACK TRANSACTION
	DECLARE @inserted BIT
		,@userId INT
	SELECT @inserted=[online], @userId=[id] FROM [inserted]
	IF @inserted=1
		INSERT INTO [LogUserOnline]([userId])
			VALUES(@userId)
	ELSE
		UPDATE [LogUserOnline]
			SET [dateOut]=GETDATE()
			WHERE [userId]=@userId AND [dateOut] IS NULL
END
GO

CREATE TRIGGER [UpdateLastVisitDate]
ON [User]
FOR UPDATE
AS
IF UPDATE([onLine]) 
	AND EXISTS(SELECT '' FROM [inserted] i JOIN [deleted] d ON i.[onLine]<>d.[onLine] WHERE i.[onLine]=0)
UPDATE [User]
	SET [lastVisitDate]=GETDATE()
	WHERE id IN (SELECT i.id FROM [inserted] i) 
GO

CREATE TRIGGER [NotUpdateDateCreation]
ON [User]
FOR UPDATE
AS
IF UPDATE([dateCreation])
	ROLLBACK TRANSACTION
GO

CREATE TRIGGER [NotUpdateDateIn]
ON [LogUserOnLine]
FOR UPDATE
AS
IF UPDATE([dateIn])
	ROLLBACK TRANSACTION
GO

CREATE TRIGGER [OneUpdateDateOut]
ON [LogUserOnLine]
FOR UPDATE
AS
IF NOT UPDATE([dateOut]) AND EXISTS(SELECT '' FROM [deleted] WHERE [dateOut] IS NOT NULL)
	ROLLBACK TRANSACTION
GO