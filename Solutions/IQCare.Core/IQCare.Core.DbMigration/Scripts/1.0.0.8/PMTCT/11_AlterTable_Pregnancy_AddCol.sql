IF (NOT EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'Parity2'))
BEGIN 
ALTER TABLE Pregnancy  ADD  Parity2 INT  NULL;
END


IF (NOT EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'Gestation'))
BEGIN 
ALTER TABLE Pregnancy  ADD  Gestation DECIMAL(18,2)  NULL;
END
