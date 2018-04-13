If Exists(Select * from sys.columns where Name = N'DeclineReason' AND Object_ID = Object_ID(N'PatientConsent'))
BEGIN
	ALTER TABLE PatientConsent ALTER COLUMN DeclineReason INT NULL
END
GO

If NOT Exists(Select * from sys.columns where Name = N'ConsentValue' AND Object_ID = Object_ID(N'PatientConsent'))
BEGIN
	ALTER TABLE [dbo].[PatientConsent] ADD ConsentValue int NULL;
END
GO