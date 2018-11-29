IF EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'PatientEncounterId' AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
  IF EXISTS (SELECT * FROM sys.foreign_keys  WHERE object_id = OBJECT_ID(N'FK_PatientOutcome_PatientEncounter') 
 AND parent_object_id = OBJECT_ID(N'dbo.PatientOutcome'))
 BEGIN
	ALTER TABLE dbo.PatientOutCome DROP CONSTRAINT [FK_PatientOutcome_PatientEncounter]
	ALTER TABLE dbo.PatientOutcome DROP COLUMN PatientEncounterId;
 END
END

IF EXISTS(SELECT 1 FROM sys.columns  WHERE Name = N'OutcomeId' AND Object_ID = Object_ID(N'dbo.PatientOutcome'))
BEGIN
BEGIN TRANSACTION 
ALTER TABLE dbo.PatientOutcome DROP COLUMN OutcomeId;

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FK_PatientOutcome_Id') AND type in (N'U'))
BEGIN
ALTER TABLE PatientOutcome DROP CONSTRAINT  [PK_PatientOutCome_Id]
ALTER TABLE PatientOutcome DROP COLUMN Id
ALTER TABLE PatientOutcome ADD Id_new INT IDENTITY(1, 1) NOT NULL 
EXEC sp_rename 'PatientOutcome.Id_new', 'Id', 'Column'
ALTER TABLE PatientOutcome ADD CONSTRAINT FK_PatientOutcome_Id PRIMARY KEY(Id);
END


COMMIT TRANSACTION
END


