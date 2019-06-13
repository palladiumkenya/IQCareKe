IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'PregnancyPlanned' AND Object_ID = OBJECT_ID(N'PregnancyIndicator'))
BEGIN
	ALTER TABLE [dbo].[PregnancyIndicator] ADD PregnancyPlanned int NULL;
END