if not Exists(select * from LookupMaster where Name like 'PrepDiscontinueReason')
BEGIN
insert into LookupMaster ([Name],DisplayName,DeleteFlag)
values('PrepDiscontinueReason','PrEP Discontinue Reasons','0')
END


if not exists(select * from LookupItem where Name like 'HivTestPositive')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('HivTestPositive','Hiv test is positive','0')
END
go
if  exists(select * from LookupItem where Name like 'HivTestPositive')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='HivTestPositive' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='HivTestPositive'
END
END

go


if not exists(select * from LookupItem where Name like 'RenalDysfunction')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('RenalDysfunction','Renal dysfunction','0')
END
go
if  exists(select * from LookupItem where Name like 'RenalDysfunction')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='RenalDysfunction' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='RenalDysfunction'
END
END



go


if not exists(select * from LookupItem where Name like 'ClientRequest')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('ClientRequest','Client request','0')
END
go
if  exists(select * from LookupItem where Name like 'ClientRequest')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ClientRequest' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='ClientRequest'
END
END




if not exists(select * from LookupItem where Name like 'NotAdherentPrep')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('NotAdherentPrep','Not adherent to PrEP','0')
END
go
if  exists(select * from LookupItem where Name like 'NotAdherentPrep')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='NotAdherentPrep' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='NotAdherentPrep'
END
END





go




if not exists(select * from LookupItem where Name like 'ViralsuppressionPartner')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('ViralsuppressionPartner','Viral suppression of HIV+ Partner','0')
END
go
if  exists(select * from LookupItem where Name like 'ViralsuppressionPartner')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ViralsuppressionPartner' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='ViralsuppressionPartner'
END
END


go


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
lit.Name='ManyHivTests' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='ManyHivTests'
END
END

go




if not exists(select * from LookupItem where Name like 'Other')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('Other','Other','0')
END
go
if  exists(select * from LookupItem where Name like 'Other')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Other' and lm.Name='PrepDiscontinueReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepDiscontinueReason' and lit.Name='Other'
END
END






