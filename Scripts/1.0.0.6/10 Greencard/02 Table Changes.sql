--Being used when saving lab order -- John
--If Exists(Select * from sys.columns where Name = N'AuditData' AND Object_ID = Object_ID(N'ord_Visit'))
--BEGIN
--	ALTER TABLE ord_Visit DROP COLUMN AuditData
--END
--GO

ALTER TABLE PatientHivDiagnosis
ADD HistoryARTUse int
Go