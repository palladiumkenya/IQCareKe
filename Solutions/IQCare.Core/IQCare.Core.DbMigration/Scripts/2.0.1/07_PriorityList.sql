if not Exists(select * from LookupMaster where Name like 'Priority')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('Priority','Priority','0')

go



if not exists(select * from LookupItem where Name like '%LifeThreatened%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LifeThreatened','Life Threatened','0')
END
go
if  exists(select * from LookupItem where Name like 'LifeThreatened%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='LifeThreatened' and lm.Name='Priority')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Priority' and lit.Name='LifeThreatened'
END
END


go




if not exists(select * from LookupItem where Name like '%Emergency%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Emergency','Emergency','0')
END
go
if  exists(select * from LookupItem where Name like 'Emergency%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Emergency' and lm.Name='Priority')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Priority' and lit.Name='Emergency'
END
END

GO

if not exists(select * from LookupItem where Name like '%High%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('High','High','0')
END
go
if  exists(select * from LookupItem where Name like 'High%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='High' and lm.Name='Priority')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Priority' and lit.Name='High'
END
END

go



if not exists(select * from LookupItem where Name like '%Medium%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Medium','Medium','0')
END
go
if  exists(select * from LookupItem where Name like 'Medium%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Medium' and lm.Name='Priority')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Priority' and lit.Name='Medium'
END
END
go

if not exists(select * from LookupItem where Name like '%Normal%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Normal','Normal','0')
END
go
if  exists(select * from LookupItem where Name like 'Normal%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Normal' and lm.Name='Priority')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Priority' and lit.Name='Normal'
END
END
go



if not exists(select * from LookupItem where Name like 'Low')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Low','Low','0')
END
go
if  exists(select * from LookupItem where Name like 'Low' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Low' and lm.Name='Priority')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Priority' and lit.Name='Low'
END
END
