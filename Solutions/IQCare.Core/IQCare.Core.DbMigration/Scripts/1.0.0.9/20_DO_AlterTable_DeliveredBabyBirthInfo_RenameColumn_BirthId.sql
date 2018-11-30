IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'BirthId'))
BEGIN 
EXEC sp_rename 'DeliveredBabyBirthInformation.BirthId', 'Id', 'COLUMN';
END
