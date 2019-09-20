IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'OtherDisability' AND Object_ID = OBJECT_ID(N'HtsEncounter'))
BEGIN
	ALTER TABLE [dbo].[HtsEncounter] ADD OtherDisability varchar(max) NULL;
END