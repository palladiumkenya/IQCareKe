IF EXISTS (SELECT 1  FROM   INFORMATION_SCHEMA.COLUMNS  WHERE  TABLE_NAME = 'PatientDelivery'
                      AND COLUMN_NAME = 'ProfileId'
                      AND TABLE_SCHEMA='dbo')
  BEGIN
      ALTER TABLE PatientDelivery DROP COLUMN ProfileId
  END
GO