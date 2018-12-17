IF NOT (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'AgeAtMenarche'))
BEGIN 
ALTER TABLE Pregnancy ADD AgeAtMenarche Decimal(18,2) null
END