IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RegistrationDate'AND Object_ID = OBJECT_ID(N'PatientTreatmentSupporter'))
BEGIN
	ALTER TABLE [dbo].[PatientTreatmentSupporter] ADD RegistrationDate int NOT NULL DEFAULT(1);
END;