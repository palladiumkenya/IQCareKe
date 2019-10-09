if not Exists(select * from LookupMaster where Name like 'ServicePoint')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('ServicePoint','ServicePoint','0')
END
go



if not exists(select * from LookupItem where Name like '%ClinicalService%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ClinicalService','ClinicalService','0')
END
go
if  exists(select * from LookupItem where Name like 'ClinicalService%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ClinicalService' and lm.Name='ServicePoint')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lit.Name='ClinicalService' and lm.Name='ServicePoint'
END
END

go


if not exists(select * from LookupItem where Name like '%Cashier%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Cashier','Cashier','0')
END
go
if  exists(select * from LookupItem where Name like 'Cashier%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Cashier' and lm.Name='ServicePoint')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lit.Name='Cashier' and lm.Name='ServicePoint'
END
END

go



if not exists(select * from LookupItem where Name like '%Consultation%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Consultation','Consultation','0')
END
go
if  exists(select * from LookupItem where Name like 'Consultation%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Consultation' and lm.Name='ServicePoint')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lit.Name='Consultation' and lm.Name='ServicePoint'
END
END

go


if not exists(select * from LookupItem where Name like '%Laboratory%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Laboratory','Laboratory','0')
END
go
if  exists(select * from LookupItem where Name like 'Laboratory%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Laboratory' and lm.Name='ServicePoint')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ServicePoint' and lit.Name='Laboratory'
END
END

go



if not exists(select * from LookupItem where Name like '%Pharmacy%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Pharmacy','Pharmacy','0')
END
go
if  exists(select * from LookupItem where Name like 'Pharmacy%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Pharmacy' and lm.Name='ServicePoint')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lit.Name='Pharmacy' and lm.Name='ServicePoint'
END
END

go


if not exists(select * from LookupItem where Name like '%Triage%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Triage','Triage','0')
END
go
if  exists(select * from LookupItem where Name like 'Triage%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Triage' and lm.Name='ServicePoint')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lit.Name='Triage' and lm.Name='ServicePoint'
END
END
