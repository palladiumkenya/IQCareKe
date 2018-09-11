IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Id = 6)
BEGIN
	UPDATE Identifiers SET DisplayName = 'Clinic ID' WHERE Id = 6;
END;
Go