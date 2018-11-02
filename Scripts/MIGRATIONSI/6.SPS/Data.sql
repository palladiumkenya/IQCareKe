


IF NOT EXISTS(select * from LookupMasterItem l 
inner join LookupMaster lt on lt.Id=l.LookupMasterId
inner join LookupItem li on li.Id=l.LookupItemId
where lt.Name='MaritalStatus' and li.Name='Married')
INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank)
values((select Id from LookupMaster where Name='MaritalStatus'),(select  Id from LookupItem where
 Name='Married'),'Married','7')

 GO

 
update  l
set DisplayName =li.DisplayName
from LookupMasterItem l 
inner join LookupMaster lt on lt.Id=l.LookupMasterId
inner join LookupItem li on li.Id=l.LookupItemId
where lt.Name='MaritalStatus'

go

update l 
set OrdRank=7
from LookupMasterItem l 
inner join LookupMaster lt on lt.Id=l.LookupMasterId
inner join LookupItem li on li.Id=l.LookupItemId
where lt.Name='MaritalStatus' and li.Name='Married'
