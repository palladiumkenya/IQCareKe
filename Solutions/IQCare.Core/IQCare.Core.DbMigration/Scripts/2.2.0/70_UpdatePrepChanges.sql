

update LookupItem set DisplayName ='TDF+3TC' where [Name]='PRP1B'
update LookupItem set DisplayName ='TDF+3TC' where [Name]='PRP1B'


--select * from LookupItem where Name='PRP1B'


update lmi  set lmi.DisplayName='TDF+3TC'
 from LookupMasterItem lmi 
inner join LookupItem lt on lmi.LookupItemId=lt.Id 
inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
where lt.[Name]='PRP1B'

