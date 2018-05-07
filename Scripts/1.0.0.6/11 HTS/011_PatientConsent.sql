If Exists(Select * from sys.columns where Name = N'DeclineReason' AND Object_ID = Object_ID(N'PatientConsent'))
BEGIN
	if ((SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PatientConsent' AND COLUMN_NAME = 'DeclineReason')='datetime')
	BEGIN
		ALTER TABLE PatientConsent DROP COLUMN DeclineReason
		ALTER TABLE PatientConsent ADD DeclineReason INT NULL
	END
END
GO


If NOT Exists(Select * from sys.columns where Name = N'ConsentValue' AND Object_ID = Object_ID(N'PatientConsent'))
BEGIN
	ALTER TABLE [dbo].[PatientConsent] ADD ConsentValue int NULL;
END
GO