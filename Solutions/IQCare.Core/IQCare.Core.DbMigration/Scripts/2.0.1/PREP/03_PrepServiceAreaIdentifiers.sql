
IF NOT EXISTS(select * from ServiceAreaIdentifiers sa inner join Identifiers i on i.Id=sa.IdentifierId
inner join ServiceArea sar on sar.Id=sa.ServiceAreaId where sar.Name='PREP')
BEGIN 
INSERT INTO ServiceAreaIdentifiers (ServiceAreaId,IdentifierId,RequiredFlag)
VALUES((select Id from ServiceArea where [Name]='PREP'),(select id from Identifiers where Code ='PREPNumber'),'1')
END