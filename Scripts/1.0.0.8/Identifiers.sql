IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Id = 6)
BEGIN
	UPDATE Identifiers SET DisplayName = 'Clinic ID', Name = 'Clinic Number', Code = 'ClinicId' WHERE Id = 6;
END;
Go