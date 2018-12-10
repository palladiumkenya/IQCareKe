if not exists(select * from LookupItem  where  DisplayName like 'Unknown%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Unknown','Unknown','0')
END
go
if  exists(select * from LookupItem where Name like 'Unknown')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Unknown' and lm.Name='HivTestingResult')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HivTestingResult' and lit.Name='Unknown'
END
END


go