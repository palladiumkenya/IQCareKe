If Exists(Select * from sys.columns where Name = N'DeclineReason' AND Object_ID = Object_ID(N'PatientConsent'))
BEGIN
	ALTER TABLE PatientConsent ALTER COLUMN DeclineReason INT NULL
END
GO