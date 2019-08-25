IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'TracingOutome' AND Object_ID = OBJECT_ID(N'PatientCareending'))
BEGIN
	ALTER TABLE [dbo].[PatientCareending] ADD TracingOutome int NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ReasonLostToFollowup' AND Object_ID = OBJECT_ID(N'PatientCareending'))
BEGIN
	ALTER TABLE [dbo].[PatientCareending] ADD ReasonLostToFollowup int NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ReasonForTransferOut' AND Object_ID = OBJECT_ID(N'PatientCareending'))
BEGIN
	ALTER TABLE [dbo].[PatientCareending] ADD ReasonForTransferOut varchar(max) null;
END


IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'DateExpectedToReport' AND Object_ID = OBJECT_ID(N'PatientCareending'))
BEGIN
	ALTER TABLE [dbo].[PatientCareending] ADD DateExpectedToReport datetime NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ReasonsForDeath' AND Object_ID = OBJECT_ID(N'PatientCareending'))
BEGIN
	ALTER TABLE [dbo].[PatientCareending] ADD ReasonsForDeath int NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'SpecificCausesOfDeath' AND Object_ID = OBJECT_ID(N'PatientCareending'))
BEGIN
	ALTER TABLE [dbo].[PatientCareending] ADD SpecificCausesOfDeath int NULL;
END