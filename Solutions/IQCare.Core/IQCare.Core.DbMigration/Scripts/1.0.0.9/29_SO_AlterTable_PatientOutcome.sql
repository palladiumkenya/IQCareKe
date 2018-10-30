IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'PatientEncounterId'
          AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
    -- Column Exists
	ALTER TABLE dbo.PatientOutcome DROP COLUMN PatientEncounterId;
END

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'OutcomeId'
          AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
    -- Column Exists
	ALTER TABLE dbo.PatientOutcome DROP COLUMN OutcomeId;
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'Id'
          AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
    -- Column Exists
	ALTER TABLE dbo.PatientOutcome ADD Id int identity
END