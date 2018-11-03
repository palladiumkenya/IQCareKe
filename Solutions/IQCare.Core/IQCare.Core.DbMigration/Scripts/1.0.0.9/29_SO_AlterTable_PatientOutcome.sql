IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'PatientEncounterId'
          AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
    -- Column Exists
	ALTER TABLE dbo.PatientOutCome DROP CONSTRAINT IF EXISTS [FK_PatientOutcome_PatientEncounter]
	ALTER TABLE dbo.PatientOutcome DROP COLUMN PatientEncounterId;
END

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'OutcomeId'
          AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
    -- Column Exists
	ALTER TABLE dbo.PatientOutcome DROP COLUMN OutcomeId;
END


BEGIN TRANSACTION 
ALTER TABLE PatientOutcome ADD Id_new INT IDENTITY(1, 1) NOT NULL 
GO

ALTER TABLE PatientOutcome DROP CONSTRAINT IF EXISTS  [PK_PatientOutCome_Id]
ALTER TABLE PatientOutcome DROP COLUMN Id
GO

EXEC sp_rename 'PatientOutcome.Id_new', 'Id', 'Column'

ALTER TABLE PatientOutcome ADD CONSTRAINT FK_PatientOutcome_Id PRIMARY KEY(Id);

COMMIT TRANSACTION