If Exists(Select * from sys.columns where Name = N'HIVDiagnosisDate' AND Object_ID = Object_ID(N'PatientHivDiagnosis'))
BEGIN
	ALTER TABLE PatientHivDiagnosis ALTER COLUMN HIVDiagnosisDate DATETIME NULL
END
GO

If NOT Exists(Select * from sys.columns where Name = N'IdentifierType' AND Object_ID = Object_ID(N'Identifiers'))
BEGIN
	ALTER TABLE Identifiers ADD IdentifierType INT NULL
END
GO



If Exists(Select * from sys.columns where Name = N'EnrollmentDate' AND Object_ID = Object_ID(N'PatientHivDiagnosis'))
BEGIN
	ALTER TABLE PatientHivDiagnosis ALTER COLUMN EnrollmentDate DATETIME NULL
END
GO


If Exists(Select * from sys.columns where Name = N'DateStartedOnFirstLine' AND Object_ID = Object_ID(N'PatientTreatmentInitiation'))
BEGIN
	ALTER TABLE PatientTreatmentInitiation ALTER COLUMN DateStartedOnFirstLine DATETIME NULL
END
GO

If Exists(Select * from sys.columns where Name = N'Cohort' AND Object_ID = Object_ID(N'PatientTreatmentInitiation'))
BEGIN
	ALTER TABLE PatientTreatmentInitiation ALTER COLUMN Cohort varchar(20) NULL
END
GO

If Exists(Select * from sys.columns where Name = N'DateLastUsed' AND Object_ID = Object_ID(N'PatientARVHistory'))
BEGIN
	ALTER TABLE PatientARVHistory ALTER COLUMN DateLastUsed DATETIME NULL
END
GO

If Exists(Select * from sys.columns where Name = N'TransferInDate' AND Object_ID = Object_ID(N'PatientTransferIn'))
BEGIN
	ALTER TABLE PatientTransferIn ALTER COLUMN TransferInDate DATETIME NULL
END
GO

If Exists(Select * from sys.columns where Name = N'ptn_pk' AND Object_ID = Object_ID(N'Patient'))
BEGIN
	IF OBJECT_ID('unique_ptn_pk', 'C') IS NULL 
	BEGIN
		ALTER TABLE Patient	ADD CONSTRAINT unique_ptn_pk UNIQUE (ptn_pk);
	END
END
GO

--- Alter Columns for lookup Items-take in more texts.
	If Exists(Select * from sys.columns where Name = N'Name' AND Object_ID = Object_ID(N'LookupItem'))
	BEGIN
		ALTER TABLE LookupItem ALTER COLUMN Name varchar(200) NOT NULL
	END

	If Exists(Select * from sys.columns where Name = N'DisplayName' AND Object_ID = Object_ID(N'LookupItem'))
	BEGIN
		ALTER TABLE LookupItem ALTER COLUMN DisplayName varchar(200) NOT NULL
	END


	GO