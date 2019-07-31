if not Exists(select * from LookupMaster where Name like 'PrepDeclineReason')
BEGIN
insert into LookupMaster ([Name],DisplayName,DeleteFlag)
values('PrepDeclineReason','PrEP Decline Reasons','0')
END


if not exists(select * from LookupItem where Name like 'ManyHivTests')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('ManyHivTests','Too many HIV tests','0')
END
go
if  exists(select * from LookupItem where Name like 'ManyHivTests')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ManyHivTests' and lm.Name='PrepDeclineReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDeclineReason' and lit.Name='ManyHivTests'
END
END


if not exists(select * from LookupItem where Name like 'Side Effects(ADR)')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('Side Effects(ADR)','Side Effects(ADR)','0')
END
go
if  exists(select * from LookupItem where Name like 'Side Effects(ADR)')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Side Effects(ADR)' and lm.Name='PrepDeclineReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDeclineReason' and lit.Name='Side Effects(ADR)'
END
END


if not exists(select * from LookupItem where Name like 'Pill Burden')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('Pill Burden','Pill Burden','0')
END
go
if  exists(select * from LookupItem where Name like 'Pill Burden')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Pill Burden' and lm.Name='PrepDeclineReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDeclineReason' and lit.Name='Pill Burden'
END
END



if not exists(select * from LookupItem where Name like 'takinglongtimepills')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('takinglongtimepills','Taking pills for a long time','0')
END
go
if  exists(select * from LookupItem where Name like 'takinglongtimepills')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='takinglongtimepills' and lm.Name='PrepDeclineReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDeclineReason' and lit.Name='takinglongtimepills'
END
END




if not exists(select * from LookupItem where Name like 'Stigma')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('Stigma','Stigma','0')
END
go
if  exists(select * from LookupItem where Name like 'Stigma')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Stigma' and lm.Name='PrepDeclineReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDeclineReason' and lit.Name='Stigma'
END
END










if not exists(select * from LookupItem where Name like 'None')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('None','None','0')
END
go
if  exists(select * from LookupItem where Name like 'None')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='None' and lm.Name='PrepDeclineReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDeclineReason' and lit.Name='None'
END
END

