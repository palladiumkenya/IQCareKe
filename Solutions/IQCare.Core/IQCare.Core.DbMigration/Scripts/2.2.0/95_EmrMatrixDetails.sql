/***** Object:  View [dbo].[EmrMatrixDetails]    Script Date: 06/03/2020 10:42:37 *****/
IF OBJECT_ID('dbo.EmrMatrixDetails', 'V') IS NOT NULL
    DROP VIEW [dbo].[EmrMatrixDetails]
GO

DECLARE @IQTools as Varchar(max) 
IF EXISTS(select top 1 d.[name] as NME   FROM master..sysaltfiles f 
                        INNER JOIN master..sysdatabases d ON f.dbid = d.dbid 
                        Where f.name like '%iqtools%' and f.fileid = 1)
BEGIN
SET  @IQTools=(select t.NME from (select top 1 d.[name] as NME   FROM master..sysaltfiles f 
                        INNER JOIN master..sysdatabases d ON f.dbid = d.dbid 
                        Where f.name like '%iqtools%' and f.fileid = 1)t)

	DECLARE @SQL nvarchar(MAX) =
 N' CREATE VIEW [dbo].[EmrMatrixDetails]
AS
SELECT  
   (SELECT max(e.EncounterStartTime) FROM PatientEncounter e WHERE e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName=''ccc-encounter'' )) as ''LastLoginDate'',
  (SELECT top 1 VersionName FROM AppAdmin ) as ''EmrVersion'',
  ''IQCare'' as ''EmrName'',
 
 (		
		CASE (select t.NME from (select top 1 d.[name] as NME   FROM master..sysaltfiles f 
                        INNER JOIN master..sysdatabases d ON f.dbid = d.dbid 
                        Where f.name like ''%iqtools%'' and f.fileid = 1)t)	  
			 WHEN ''' + @IQTools + '''   THEN (SELECT max(ReportRunDate) FROM  ' + @IQTools + '.[dbo].ReportRunLog WHERE ReportName=''NASCOP_MOH731'')
			 WHEN NULL THEN NULL 

		 END
  ) as ''LastMoH731RunDate''
';



EXEC sp_executesql @SQL;
END 
ELSE
BEGIN

SET @IQTools=NULL;

DECLARE @SQL1 nvarchar(MAX) =
 N' CREATE VIEW [dbo].[EmrMatrixDetails]
AS
SELECT  
   (SELECT max(e.EncounterStartTime) FROM PatientEncounter e WHERE e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName=''ccc-encounter'' )) as ''LastLoginDate'',
  (SELECT top 1 VersionName FROM AppAdmin ) as ''EmrVersion'',
  ''IQCare'' as ''EmrName'',
  NULL as ''LastMoH731RunDate''
';


EXEC sp_executesql @SQL1;

END






