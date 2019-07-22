if not Exists(select * from LookupMaster where Name like 'PrepAppointmentReason')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('PrepAppointmentReason','PrepAppointmentReason','0')
END



if  exists(select * from LookupItem where Name like 'Pharmacy Refill')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Pharmacy Refill' and lm.Name='PrepAppointmentReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAppointmentReason' and lit.Name='Pharmacy Refill'
END
END

go




if  exists(select * from LookupItem where Name like 'Lab Tests')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Lab Tests' and lm.Name='PrepAppointmentReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAppointmentReason' and lit.Name='Lab Tests'
END
END


go


if  exists(select * from LookupItem where Name like 'Follow Up')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Follow Up' and lm.Name='PrepAppointmentReason')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepAppointmentReason' and lit.Name='Follow Up'
END
END





