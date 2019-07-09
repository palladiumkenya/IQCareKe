if not Exists(select * from LookupMaster where Name like '%PrepAdherence%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('PrepAdherence','PrepAdherence','0')


go



if not exists(select * from LookupItem where Name like 'Good')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Good','Good','0')
END
go
if  exists(select * from LookupItem where Name like 'Good')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Good' and lm.Name='PrepAdherence')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAdherence' and lit.Name='Good'
END
END

go



if not exists(select * from LookupItem where Name like 'Bad')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Bad','Bad','0')
END
go
if  exists(select * from LookupItem where Name like 'Bad')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Bad' and lm.Name='PrepAdherence')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAdherence' and lit.Name='Bad'
END
END
go


if not exists(select * from LookupItem where Name like 'Fair')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Fair','Fair','0')
END
go
if  exists(select * from LookupItem where Name like 'Fair')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Fair' and lm.Name='PrepAdherence')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAdherence' and lit.Name='Fair'
END
END

go


if not exists(select * from LookupItem where Name like 'N/A')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('N/A','N/A','0')
END
go
if  exists(select * from LookupItem where Name like 'N/A')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='N/A' and lm.Name='PrepAdherence')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAdherence' and lit.Name='N/A'
END
END

go
