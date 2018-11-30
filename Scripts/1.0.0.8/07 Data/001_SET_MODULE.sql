IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'ANC')
BEGIN
	--SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(N'ANC', N'ANC', N'ANC', 1, GETDATE(), 0);
	--SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'PNC')
BEGIN
	--SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(N'PNC', N'PNC', N'PNC', 1, GETDATE(), 0);
	--SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'Maternity')
BEGIN
	--SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(N'Maternity', N'Maternity', N'Maternity', 1, GETDATE(), 0);
	--SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'HEI')
BEGIN
	--SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(N'HEI', N'HEI', N'HIV EXPOSED INFANTS', 1, GETDATE(), 0);
	--SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END