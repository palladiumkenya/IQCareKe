
IF EXISTS (SELECT  *  FROM   INFORMATION_SCHEMA.COLUMNS  WHERE  TABLE_NAME = 'PatientPrEPStatus'
                      AND COLUMN_NAME = 'ContraindicationsPrepPresent'
                      AND TABLE_SCHEMA='dbo')
                      BEGIN
					  DECLARE @SQL AS VARCHAR(MAX)
					  SET @SQL = 'INSERT INTO PatientScreening(PatientId,PatientMasterVisitId,ScreeningTypeId,ScreeningDone,ScreeningDate,ScreeningValueId,Active,DeleteFlag,CreatedBy,CreateDate,VisitDate)
					   select ps.PatientId,pe.PatientMasterVisitId,
					   (select Id from LookupMaster where Name=''ContraindicationsPrEP'') as ScreeningTypeId,''1'' as ScreeningDone,pmv.VisitDate as ScreeningDate,ps.ContraindicationsPrepPresent as ScreeningValueId,''0'' as Active,''0'' as DeleteFlag,ps.CreatedBy,ps.CreateDate,pmv.VisitDate from PatientPrEPStatus ps
					   inner join PatientEncounter pe on pe.Id=ps.PatientEncounterId
					   inner join PatientMasterVisit pmv on pmv.Id =pe.PatientMasterVisitId
					   GO
					     ALTER TABLE PatientPrEPStatus DROP COLUMN  ContraindicationsPrepPresent '
						
						EXEC @SQL;
					  END
					 
					   
                 
