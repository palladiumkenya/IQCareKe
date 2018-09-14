IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'ANC')
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Id],[Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(3, N'ANC', N'ANC', N'ANC', 1, GETDATE(), 0);
	SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'PNC')
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Id],[Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(4, N'PNC', N'PNC', N'PNC', 1, GETDATE(), 0);
	SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'Maternity')
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Id],[Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(5, N'Maternity', N'Maternity', N'Maternity', 1, GETDATE(), 0);
	SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'HEI')
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Id],[Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(6, N'HEI', N'HEI', N'HIV EXPOSED INFANTS', 1, GETDATE(), 0);
	SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END