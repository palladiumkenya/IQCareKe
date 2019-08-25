IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'SyphilisResult' AND Object_ID = OBJECT_ID(N'HtsEncounterResult'))
BEGIN
	ALTER TABLE [dbo].[HtsEncounterResult] ADD SyphilisResult int NULL;
END