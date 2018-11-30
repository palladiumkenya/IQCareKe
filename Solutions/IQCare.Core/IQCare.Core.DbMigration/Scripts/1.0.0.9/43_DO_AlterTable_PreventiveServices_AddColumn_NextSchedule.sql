IF NOT EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'NextSchedule' AND Object_ID = Object_ID(N'dbo.PatientPreventiveServices'))
BEGIN
ALTER TABLE PatientPreventiveServices ADD NextSchedule DATETIME NULL;
END


