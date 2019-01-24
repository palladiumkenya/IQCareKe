IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactCategory'AND Object_ID = OBJECT_ID(N'PatientTreatmentSupporter'))
BEGIN
	ALTER TABLE [dbo].[PatientTreatmentSupporter] ADD ContactCategory int NOT NULL DEFAULT(1);
END;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactRelationship'AND Object_ID = OBJECT_ID(N'PatientTreatmentSupporter'))
BEGIN
	ALTER TABLE [dbo].[PatientTreatmentSupporter] ADD ContactRelationship int NULL;
END;