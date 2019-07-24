IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'PlanningToGetPregnant' AND Object_ID = OBJECT_ID(N'PregnancyIndicator'))
BEGIN
	ALTER TABLE [dbo].[PregnancyIndicator] ADD PlanningToGetPregnant int NULL;
END