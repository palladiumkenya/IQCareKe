IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'DeliveryComplications'))
BEGIN 
ALTER TABLE DeliveredBabyBirthInformation  ALTER COLUMN DeliveryComplications BIT NOT NULL;
EXEC sp_rename 'DeliveredBabyBirthInformation.DeliveryComplications', 'DeliveredBabyBirthInformation.DeliveryComplicationsExperienced', 'COLUMN';
END
