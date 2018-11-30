IF (NOT EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'DaysPostPartum'))
BEGIN 
ALTER TABLE PatientProfile ADD DaysPostPartum INT NULL;
END


