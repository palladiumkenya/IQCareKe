

if not exists(select * from LookupItem where Name like 'MonthlyRefill-encounter')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('MonthlyRefill-encounter','MonthlyRefill-encounter','0')
END
go
if  exists(select * from LookupItem where Name like 'MonthlyRefill-encounter')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='MonthlyRefill-encounter' and lm.Name='EncounterType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'23.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EncounterType' and lit.Name='MonthlyRefill-encounter'
END
END
