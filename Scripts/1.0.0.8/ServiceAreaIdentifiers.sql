IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'HTS Module'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) 
	VALUES ((SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'HTS Module'), (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'HTSNumber'), 1);
END;
Go


IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'ANC'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) 
	VALUES ((SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'ANC'), (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'ANCNumber'), 1);
END;
Go


IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'HEI'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) 
	VALUES ((SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'HEI'), (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'HEIRegistration'), 1);
END;
Go



IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'PNC'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) 
	VALUES ((SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'PNC'), (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'PNCNumber'), 1);
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM ServiceAreaIdentifiers WHERE ServiceAreaId = (SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'Maternity'))
BEGIN
	INSERT INTO [dbo].[ServiceAreaIdentifiers] ([ServiceAreaId] ,[IdentifierId] ,[RequiredFlag]) 
	VALUES ((SELECT TOP 1 Id FROM ServiceArea WHERE Name = 'Maternity'), (SELECT TOP 1 Id FROM Identifiers WHERE Code = 'MaternityNumber'), 1);
END;
Go

