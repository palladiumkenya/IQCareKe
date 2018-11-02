IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'PostPartum'))
BEGIN 
EXEC sp_rename 'PatientProfile.PostPartum', 'DaysPostPartum', 'COLUMN';
END