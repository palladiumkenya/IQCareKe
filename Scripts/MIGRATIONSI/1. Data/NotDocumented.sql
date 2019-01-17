SET IDENTITY_INSERT County ON
GO
declare  @Countyid as int;
declare @Ward as int;
declare @SubCountyid as int;
IF EXISTS(SELECT CountyId,CountyName from County where CountyName='NOT DOCUMENTED')
BEGIN
set @CountyId=(select CountyId from County where CountyName='NOT DOCUMENTED')
END
IF NOT EXISTS(SELECT CountyId,CountyName from County where CountyName='NOT DOCUMENTED')
BEGIN
set @Countyid=(select  max(CountyId) + 1  from County )

END
IF EXISTS(SELECT WardId,WardName from County where CountyName='NOT DOCUMENTED')
BEGIN
set @Ward=(select WardId from County where CountyName='NOT DOCUMENTED')
END
IF NOT EXISTS(SELECT WardId,WardName from County where CountyName='NOT DOCUMENTED')
BEGIN
set @Ward=(select  max(WardId) + 1  from County )

END
IF EXISTS(SELECT SubcountyId,Subcountyname from County where CountyName='NOT DOCUMENTED')
BEGIN
set @SubCountyid=(select SubcountyId from County where CountyName='NOT DOCUMENTED')
END
IF NOT EXISTS(SELECT SubcountyId,Subcountyname from County where CountyName='NOT DOCUMENTED')
BEGIN
set @SubCountyid=(select  max(SubcountyId) + 1  from County )

END

IF EXISTS(select * from County where CountyName='NOT DOCUMENTED')
BEGIN
UPDATE  County SET CountyId=@Countyid,CountyName='NOT DOCUMENTED',SubcountyId=@SubCountyid,Subcountyname='NOT DOCUMENTED',
WardId=@Ward,WardName='NOT DOCUMENTED'
where CountyName='NOT DOCUMENTED'
END
IF NOT EXISTS(select * from County where CountyName='NOT DOCUMENTED')
BEGIN
declare @id as int;
set @id=(select  max(Id) + 1  from County )
INSERT INTO County(Id,CountyId,CountyName,SubcountyId,Subcountyname,WardId,WardName)
values( @id,@Countyid,'NOT DOCUMENTED',@SubCountyid,'NOT DOCUMENTED',@Ward,'NOT DOCUMENTED')
END

GO
SET IDENTITY_INSERT County OFF
