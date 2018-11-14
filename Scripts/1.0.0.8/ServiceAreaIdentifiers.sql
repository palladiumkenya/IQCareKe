IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = 2)
BEGIN
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] ON
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([Id] ,[ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (2, 2, 8, 1);
	SET IDENTITY_INSERT [dbo].[ServiceAreaIdentifiers] OFF
END;
Go


IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = 3)
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (3, 3, 1);
	--INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (3, 6, 0);
END;
Go


IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = 6)
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (6, 5, 1);
END;
Go



IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT top 1 id FROM ServiceArea WHERE code='pnc'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (4, (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'PNCNumber'), 1);
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT top 1 id FROM ServiceArea WHERE code='maternity'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (5, (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'MaternityNumber'), 1);
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId =(SELECT top 1 id FROM ServiceArea WHERE code='anc'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (5, (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'ANCNumber'), 1);
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId =(SELECT top 1 id FROM ServiceArea WHERE code='hei'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) VALUES (5, (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'HEINumber'), 1);
END;

Go

