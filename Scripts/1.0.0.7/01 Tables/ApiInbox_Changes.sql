IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'MessageType'AND Object_ID = OBJECT_ID(N'ApiInbox'))
BEGIN
	ALTER TABLE [dbo].[ApiInbox] ADD MessageType varchar(20) null;
END;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'IsSuccess'AND Object_ID = OBJECT_ID(N'ApiInbox'))
BEGIN
	ALTER TABLE [dbo].[ApiInbox] ADD IsSuccess bit null;
END;