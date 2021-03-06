IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'Frequency' AND Object_ID = OBJECT_ID(N'mst_Facility'))
BEGIN
	ALTER TABLE mst_Facility ADD Frequency int NULL DEFAULT(1);
END

UPDATE mst_Facility SET Frequency = 1;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'TestedForSyphilis' AND Object_ID = OBJECT_ID(N'BaselineAntenatalCare'))
BEGIN
	ALTER TABLE [dbo].[BaselineAntenatalCare] ADD TestedForSyphilis int NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'SyphilisTestUsed' AND Object_ID = OBJECT_ID(N'BaselineAntenatalCare'))
BEGIN
	ALTER TABLE [dbo].[BaselineAntenatalCare] ADD SyphilisTestUsed int NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'SyphilisResults' AND Object_ID = OBJECT_ID(N'BaselineAntenatalCare'))
BEGIN
	ALTER TABLE [dbo].[BaselineAntenatalCare] ADD SyphilisResults int NULL;
END

ALTER TABLE [dbo].[BaselineAntenatalCare] ALTER COLUMN [TreatedForSyphilis] int NULL