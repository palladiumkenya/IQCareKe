IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'BloodLoss'))
BEGIN 
EXEC sp_rename 'PatientDelivery.BloodLoss', 'BloodLossCapacity', 'COLUMN';
END
