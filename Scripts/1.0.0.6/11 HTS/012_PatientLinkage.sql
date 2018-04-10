If NOT Exists(Select * from sys.columns where Name = N'HealthWorker' AND Object_ID = Object_ID(N'PatientLinkage'))
BEGIN
	ALTER TABLE PatientLinkage ADD HealthWorker VARCHAR(50) NULL
END
GO

If NOT Exists(Select * from sys.columns where Name = N'Cadre' AND Object_ID = Object_ID(N'PatientLinkage'))
BEGIN
	ALTER TABLE PatientLinkage ADD Cadre VARCHAR(50) NULL
END
GO