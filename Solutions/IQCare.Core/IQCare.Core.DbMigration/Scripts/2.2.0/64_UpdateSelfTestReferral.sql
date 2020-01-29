





update lm set lm.DisplayName='Self-referral'
 from LookupMasterItem lm inner join 
LookupItem lt on lt.Id=lm.LookupItemId
inner join LookupMaster lmm on lmm.Id=lm.LookupMasterId
where lmm.Name='Entrypoint'

go

update LookupItem set  Name='Self-Referral' ,
DisplayName ='Self-Referral' where Name='Self-Test'