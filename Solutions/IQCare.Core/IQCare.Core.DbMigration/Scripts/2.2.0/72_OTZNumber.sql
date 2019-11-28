IF NOT EXISTS(SELECT * FROM ServiceArea WHERE Name = 'OTZ')
BEGIN
	INSERT INTO ServiceArea(Name, Code, DisplayName, CreatedBy, CreateDate, DeleteFlag)
	VALUES('OTZ', 'OTZ', 'OTZ', 1, GETDATE(), 0);
END

IF NOT EXISTS(SELECT * FROM Identifiers WHERE Name = 'OTZNumber')
BEGIN
	INSERT INTO Identifiers(Name, Code, DisplayName, DataType, PrefixType, SuffixType, DeleteFlag, CreatedBy, CreateDate, IdentifierType)
	VALUES('OTZNumber', 'OTZNumber', 'OTZNumber', 'Numeric', NULL, NULL, 0, 1, GETDATE(), 1);
END


IF NOT EXISTS(SELECT * FROM [ServiceAreaIdentifiers] WHERE ServiceAreaId = (SELECT Id FROM ServiceArea WHERE Name = 'OTZ') AND IdentifierId = (SELECT Id FROM Identifiers WHERE Name = 'OTZNumber')) 
BEGIN
	INSERT INTO [ServiceAreaIdentifiers] ([ServiceAreaId], [IdentifierId],[RequiredFlag])
	VALUES((SELECT Id FROM ServiceArea WHERE Name = 'OTZ'), (SELECT Id FROM Identifiers WHERE Name = 'OTZNumber'), 1);
END