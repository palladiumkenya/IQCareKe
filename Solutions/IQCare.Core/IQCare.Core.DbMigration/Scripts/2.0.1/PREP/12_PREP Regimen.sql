delete lmi  from  LookupMasterItem lmi 
inner join LookupMaster lm
on lmi.LookupMasterId=lm.Id
inner join LookupItem li on li.Id=lmi.LookupItemId

where lm.Name='PrEPRegimen' and li.[Name]='CS2C'

