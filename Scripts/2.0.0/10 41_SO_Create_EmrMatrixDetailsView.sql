
IF EXISTS(select * FROM sys.views where name = 'EmrMatrixDetails')
BEGIN 
  DROP VIEW EmrMatrixDetails;
END
GO

CREATE VIEW [dbo].[EmrMatrixDetails]
AS
SELECT  
 -- (SELECT max(ReportRunDate) FROM  [IQTools_KeHMIS].[dbo].ReportRunLog WHERE ReportName='NASCOP_MOH731') as 'LastMoH731RunDate',
  (SELECT max(e.EncounterStartTime) FROM PatientEncounter e WHERE e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='ccc-encounter' )) as 'LastLoginDate',
  (SELECT top 1 VersionName FROM AppAdmin ) as 'EmrVersion',
  'IQCare' as 'EmrName',
  (		
		CASE (SELECT [name] FROM master.dbo.sysdatabases WHERE [name]='IQTools_KeHMIS')
			 WHEN 'IQTools_KeHMIS' THEN (SELECT max(ReportRunDate) FROM  [IQTools_KeHMIS].[dbo].ReportRunLog WHERE ReportName='NASCOP_MOH731')
			-- WHEN 'IQTools' THEN (SELECT max(ReportRunDate) FROM  [IQTools_KeHMIS].[dbo].ReportRunLog WHERE ReportName='NASCOP_MOH731')
			 WHEN NULL THEN NULL 
		 END
  ) as 'LastMoH731RunDate'
