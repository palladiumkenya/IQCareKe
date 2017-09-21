IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'IdentifierType' AND Object_ID = OBJECT_ID(N'Identifiers'))
BEGIN
	ALTER TABLE [dbo].[Identifiers] ADD IdentifierType int NULL DEFAULT(1);
END;