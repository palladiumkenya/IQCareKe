IF NOT EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'HTS Number')
BEGIN
	SET IDENTITY_INSERT [dbo].[Identifiers] ON
	INSERT INTO [dbo].[Identifiers] ([Id], [Name], [Code], [DisplayName], [DataType], [PrefixType], [SuffixType], [DeleteFlag], [CreatedBy], [CreateDate] ,[AuditData], [IdentifierType]) VALUES (8, N'HTS Number', N'HTSNumber', N'HTS Number', N'Numeric', NULL, NULL, 0, 1, GETDATE(), NULL, 1);
	SET IDENTITY_INSERT [dbo].[Identifiers] OFF
END;
GO