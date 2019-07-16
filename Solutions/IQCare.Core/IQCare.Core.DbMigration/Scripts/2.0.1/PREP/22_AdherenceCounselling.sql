if not Exists(select * from LookupMaster where Name like 'AdherenceCounselling')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('AdherenceCounselling','AdherenceCounselling','0')
END

if not exists(select * from LookupItem where Name like 'Yes')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Yes','Yes','0')
END
go
if  exists(select * from LookupItem where Name like 'Yes')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Yes' and lm.Name='AdherenceCounselling')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceCounselling' and lit.Name='Yes'
END
END


if not exists(select * from LookupItem where Name like 'No')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('No','No','0')
END
go
if  exists(select * from LookupItem where Name like 'No')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='No' and lm.Name='AdherenceCounselling')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceCounselling' and lit.Name='No'
END
END



