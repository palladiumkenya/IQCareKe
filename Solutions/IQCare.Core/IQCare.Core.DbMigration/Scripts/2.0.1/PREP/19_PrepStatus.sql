if not Exists(select * from LookupMaster where Name like '%RefillPrepStatus%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('RefillPrepStatus','Prep Status','0')


go



if not exists(select * from LookupItem where Name like 'Continue')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Continue','Continue','0')
END
go
if  exists(select * from LookupItem where Name like 'Continue')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Continue' and lm.Name='RefillPrepStatus')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RefillPrepStatus' and lit.Name='Continue'
END
END

go


if not exists(select * from LookupItem where Name like 'DisContinue')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('DisContinue','DisContinue','0')
END
go
if  exists(select * from LookupItem where Name like 'DisContinue')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='DisContinue' and lm.Name='RefillPrepStatus')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RefillPrepStatus' and lit.Name='DisContinue'
END
END

go
