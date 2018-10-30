IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'DeliveryComplications'))
BEGIN 
ALTER TABLE PatientDelivery  ALTER COLUMN DeliveryComplications BIT NOT NULL;
EXEC sp_rename 'PatientDelivery.DeliveryComplications', 'DeliveryComplicationsExperienced', 'COLUMN';
END
