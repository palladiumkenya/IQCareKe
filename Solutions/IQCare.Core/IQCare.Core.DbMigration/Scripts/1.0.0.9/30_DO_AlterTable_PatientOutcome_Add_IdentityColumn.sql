BEGIN TRANSACTION 
ALTER TABLE PatientOutcome ADD Id_new INT IDENTITY(1, 1) NOT NULL 
GO

ALTER TABLE PatientOutcome DROP CONSTRAINT  [PK_PatientOutCome_Id]
ALTER TABLE PatientOutcome DROP COLUMN Id
GO

EXEC sp_rename 'PatientOutcome.Id_new', 'Id', 'Column'

ALTER TABLE PatientOutcome ADD CONSTRAINT FK_PatientOutcome_Id PRIMARY KEY(Id);

COMMIT TRANSACTION