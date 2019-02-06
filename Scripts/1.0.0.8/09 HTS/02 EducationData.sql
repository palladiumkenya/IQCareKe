if not exists(select * from LookupItem  where  Name like 'University')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('University','University','0')
END
go



if  exists(select * from LookupItem where Name like 'University')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='University' and lm.Name='EducationalLevel')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationalLevel' and lit.Name='University'
END
END


go 

if exists(select * from LookupItem where Name='College')
BEGIN
update LookupItem set DisplayName ='College' where Name='College'


 update   lmi  
 set lmi.DisplayName='College' 
from LookupMasterItem as lmi
 inner join LookupItem li on li.Id=lmi.LookupItemId
  where li.[Name] = 'College'

  END

  
  
  if exists(select * from LookupItem where Name='SecondarySchool')
BEGIN
update LookupItem set Name='Secondary'  where DisplayName='Secondary School'



  END

    if exists(select * from LookupItem where Name='PrimarySchool')
BEGIN
update LookupItem set Name='Primary'  where DisplayName='Primary School'



  END