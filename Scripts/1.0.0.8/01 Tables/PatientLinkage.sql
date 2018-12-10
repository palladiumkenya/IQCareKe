IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ArtStartDate'AND Object_ID = OBJECT_ID(N'PatientLinkage'))
BEGIN
	ALTER TABLE [dbo].[PatientLinkage] ADD ArtStartDate datetime NULL;
END;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Comments'AND Object_ID = OBJECT_ID(N'PatientLinkage'))
BEGIN
	ALTER TABLE [dbo].[PatientLinkage] ADD Comments varchar(max) NULL;
END;