IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EverTested'AND Object_ID = OBJECT_ID(N'HtsEncounter'))
BEGIN
	ALTER TABLE [dbo].[HtsEncounter] ALTER COLUMN EverTested int NULL;
END;

IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EverSelfTested'AND Object_ID = OBJECT_ID(N'HtsEncounter'))
BEGIN
	ALTER TABLE [dbo].[HtsEncounter] ALTER COLUMN EverSelfTested int NULL;
END;