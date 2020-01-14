IF NOT EXISTS(SELECT * FROM [ServiceAreaIdentifiers] WHERE ServiceAreaId = (SELECT Id FROM ServiceArea WHERE Name = 'OTZ') AND IdentifierId = (SELECT Id FROM Identifiers WHERE Name = 'OTZNumber')) 
BEGIN
	INSERT INTO [ServiceAreaIdentifiers] ([ServiceAreaId], [IdentifierId],[RequiredFlag])
	VALUES((SELECT Id FROM ServiceArea WHERE Name = 'OTZ'), (SELECT Id FROM Identifiers WHERE Name = 'OTZNumber'), 1);
END