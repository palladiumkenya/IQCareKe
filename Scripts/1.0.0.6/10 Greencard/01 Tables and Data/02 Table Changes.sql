--Being used when saving lab order -- John
--If Exists(Select * from sys.columns where Name = N'AuditData' AND Object_ID = Object_ID(N'ord_Visit'))
--BEGIN
--	ALTER TABLE ord_Visit DROP COLUMN AuditData
--END
--GO

If Not Exists(Select * from sys.columns where Name = N'HistoryARTUse' AND Object_ID = Object_ID(N'PatientHivDiagnosis'))
BEGIN
	ALTER TABLE PatientHivDiagnosis ADD HistoryARTUse int
END
GO

If Not Exists(Select * from sys.columns where Name = N'Status' AND Object_ID = Object_ID(N'patientencounter'))
BEGIN
	ALTER TABLE patientencounter ADD Status int
END
GO

EXEC sp_RENAME 'patientencounter.createby' , 'createdby', 'COLUMN'
Go