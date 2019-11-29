IF NOT EXISTS(SELECT * FROM ServiceArea WHERE Name = 'OVC')
BEGIN
	INSERT INTO ServiceArea(Name, Code, DisplayName, CreatedBy, CreateDate, DeleteFlag)
	VALUES('OVC', 'OVC', 'OVC', 1, GETDATE(), 0);
END

IF NOT EXISTS(SELECT * FROM Identifiers WHERE Name = 'OVCNumber')
BEGIN
	INSERT INTO Identifiers(Name, Code, DisplayName, DataType, PrefixType, SuffixType, DeleteFlag, CreatedBy, CreateDate, IdentifierType)
	VALUES('OVCNumber', 'OVCNumber', 'OVCNumber', 'Numeric', NULL, NULL, 0, 1, GETDATE(), 1);
END





IF NOT EXISTS(SELECT * FROM Identifiers WHERE Name = 'CPMISNumber')
BEGIN
	INSERT INTO Identifiers(Name, Code, DisplayName, DataType, PrefixType, SuffixType, DeleteFlag, CreatedBy, CreateDate, IdentifierType)
	VALUES('CPMISNumber', 'CPMISNumber', 'CPMISNumber', 'Text', NULL, NULL, 0, 1, GETDATE(), 1);
END






IF NOT EXISTS(select * from ServiceAreaIdentifiers sa inner join Identifiers i on i.Id=sa.IdentifierId
inner join ServiceArea sar on sar.Id=sa.ServiceAreaId where sar.Name='OVC' and i.Code='OVCNumber')
BEGIN 
INSERT INTO ServiceAreaIdentifiers (ServiceAreaId,IdentifierId,RequiredFlag)
VALUES((select Id from ServiceArea where [Name]='OVC'),(select id from Identifiers where Code ='OVCNumber'),'1')
END



IF NOT EXISTS(select * from ServiceAreaIdentifiers sa inner join Identifiers i on i.Id=sa.IdentifierId
inner join ServiceArea sar on sar.Id=sa.ServiceAreaId where sar.Name='OVC' and i.Code='CPMISNumber')
BEGIN 
INSERT INTO ServiceAreaIdentifiers (ServiceAreaId,IdentifierId,RequiredFlag)
VALUES((select Id from ServiceArea where [Name]='OVC'),(select id from Identifiers where Code ='CPMISNumber'),'1')
END