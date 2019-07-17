IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'BirthDefects' AND Object_ID = OBJECT_ID(N'PregnancyLog'))
BEGIN
	ALTER TABLE [dbo].[PregnancyLog] ADD BirthDefects int NULL;
END