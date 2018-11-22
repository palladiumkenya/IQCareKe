ALTER TABLE Pregnancy ALTER COLUMN Parity INT NULL

ALTER TABLE Pregnancy ALTER COLUMN Gravidae INT NULL

If NOT Exists(Select * from sys.columns where Name = N'Gestation' AND Object_ID = Object_ID(N'Pregnancy'))
BEGIN
	ALTER TABLE Pregnancy ADD  Gestation Decimal(18,2) NULL
END

