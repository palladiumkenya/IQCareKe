

 
 if  Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='UND' and lm.Name='FPMethod')
BEGIN
delete lmi  from LookupMasterItem lmi inner join LookupItem
lt on lt.Id=lmi.LookupItemId 
inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
where lt.Name='UND' and lm.Name='FPMethod'

END