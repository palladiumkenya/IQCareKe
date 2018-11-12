UPDATE Pregnancy SET Gravidae = NULL, Parity = NULL WHERE Gravidae = 'null' AND Parity ='null';


ALTER TABLE Pregnancy ALTER COLUMN Parity INT NULL

ALTER TABLE Pregnancy ALTER COLUMN Gravidae INT NULL

