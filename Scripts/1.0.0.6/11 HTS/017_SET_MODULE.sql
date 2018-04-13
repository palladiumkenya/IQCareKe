If Not Exists(select 1 from mst_module WHERE ModuleName = 'HTS') 
Begin
	SET IDENTITY_INSERT [dbo].[mst_module] ON
	INSERT INTO mst_module (ModuleID, ModuleName, DeleteFlag, UserId, CreateDate, UpdateDate, Status, UpdateFlag, Identifier, PharmacyFlag, CanEnroll, ModuleFlag, DisplayName) 
	VALUES (283,'HTS',0, 1, GETDATE(), NULL, 2, 0, 0, 0, 0, 1, 'HTS');
	SET IDENTITY_INSERT [dbo].[mst_module] OFF
End


IF NOT EXISTS(SELECT 1 FROM [dbo].[ServiceArea] WHERE Name = 'HTS Module')
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceArea] ON
	INSERT INTO [dbo].[ServiceArea]([Id],[Name],[Code],[DisplayName],[CreatedBy],[CreateDate],[DeleteFlag])
	VALUES(2, N'HTS Module', N'HTS', N'HTS', 1, GETDATE(), 0);
	SET IDENTITY_INSERT [dbo].[ServiceArea] OFF
END