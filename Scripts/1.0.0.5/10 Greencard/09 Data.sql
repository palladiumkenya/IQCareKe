IF NOT EXISTS(SELECT TOP 1 Id FROM IdentifierType WHERE Name = N'Patient')
BEGIN
	INSERT INTO [dbo].[IdentifierType] ([Name]) VALUES (N'Patient');
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM IdentifierType WHERE Name = N'Person')
BEGIN
	INSERT INTO [dbo].[IdentifierType] ([Name]) VALUES (N'Person');
END;

IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'CCC Registration Number')
BEGIN	
	UPDATE [dbo].[Identifiers] SET IdentifierType = 1
	WHERE Id  = 1
END;

IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'TB Registration Number')
BEGIN	
	UPDATE [dbo].[Identifiers] SET IdentifierType = 1
	WHERE Id  = 2
END;

IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'ANC Registration Number')
BEGIN
	UPDATE [dbo].[Identifiers] SET IdentifierType = 1
	WHERE Id  = 3
END;

IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'PMTCT Registration Number')
BEGIN
	UPDATE [dbo].[Identifiers] SET IdentifierType = 1
	WHERE Id  = 4
END;

IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'HEI Registration Number')
BEGIN
	UPDATE [dbo].[Identifiers] SET IdentifierType = 1
	WHERE Id  = 5
END;

IF EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'Patient Number')
BEGIN
	UPDATE [dbo].[Identifiers] SET IdentifierType = 1
	WHERE Id  = 6
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'GODS_NUMBER')
BEGIN
	SET IDENTITY_INSERT [dbo].[Identifiers] ON
	INSERT INTO [dbo].[Identifiers] ([Id], [Name], [Code], [DisplayName], [DataType], [PrefixType], [SuffixType], [DeleteFlag], [CreatedBy], [CreateDate] ,[AuditData], [IdentifierType]) VALUES (7, N'GODS_NUMBER', N'GODS_NUMBER', N'GODS NUMBER', N'Unique', NULL, NULL, 0, 1, GETDATE(), NULL, 2);
	SET IDENTITY_INSERT [dbo].[Identifiers] OFF
END;