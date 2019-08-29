IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'NursesComments' AND Object_ID = OBJECT_ID(N'PatientVitals'))
BEGIN
	ALTER TABLE [dbo].[PatientVitals] ADD NursesComments text NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'AgeforZ' AND Object_ID = OBJECT_ID(N'PatientVitals'))
BEGIN
	ALTER TABLE [dbo].[PatientVitals] ADD AgeforZ decimal(8, 2) NULL;
END