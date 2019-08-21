IF EXISTS (SELECT  *  FROM   INFORMATION_SCHEMA.COLUMNS  WHERE  TABLE_NAME = 'PatientARVHistory'
                      AND COLUMN_NAME = 'RegimenUse'
                      AND TABLE_SCHEMA='dbo')
                      BEGIN
					  ALTER TABLE PatientARVHistory DROP COLUMN  RegimenUse
						
						
					  END
					 

				
					   