IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = 2)
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] ON
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([Id] ,[ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (2, 2, 8, 1);
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] OFF
END;
Go


IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = 3)
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] ON
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([Id] ,[ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (3, 3, 3, 1);
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([Id] ,[ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (4, 3, 6, 0);
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] OFF
END;
Go


IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = 4)
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] ON
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([Id] ,[ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (5, 4, 3, 1);
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([Id] ,[ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (6, 4, 6, 0);
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] OFF
END;
Go