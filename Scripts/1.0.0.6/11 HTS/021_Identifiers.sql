IF NOT EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'AFYA_MOBILE_ID')
BEGIN
	SET IDENTITY_INSERT [dbo].[Identifiers] ON
	INSERT INTO [dbo].[Identifiers] ([Id], [Name], [Code], [DisplayName], [DataType], [PrefixType], [SuffixType], [DeleteFlag], [CreatedBy], [CreateDate] ,[AuditData], [IdentifierType]) 
	VALUES (10, N'AFYA_MOBILE_ID', N'AFYA_MOBILE_ID', N'AFYA MOBILE ID', N'Unique', NULL, NULL, 0, 1, GETDATE(), NULL, 2);
	SET IDENTITY_INSERT [dbo].[Identifiers] OFF
END;