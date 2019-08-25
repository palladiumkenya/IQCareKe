IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'BreastFeeding' AND Object_ID = OBJECT_ID(N'PregnancyIndicator'))
BEGIN
	ALTER TABLE [dbo].[PregnancyIndicator] ADD BreastFeeding int NULL;
END