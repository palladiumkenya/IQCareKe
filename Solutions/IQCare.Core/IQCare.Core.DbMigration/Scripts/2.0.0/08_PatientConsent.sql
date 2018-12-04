IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'PersonId'AND Object_ID = OBJECT_ID(N'PatientConsent'))
BEGIN
	ALTER TABLE [dbo].[PatientConsent] ADD PersonId int null;
END;


IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Comments'AND Object_ID = OBJECT_ID(N'PatientConsent'))
BEGIN
	ALTER TABLE [dbo].[PatientConsent] ADD Comments varchar(50) null;
END;