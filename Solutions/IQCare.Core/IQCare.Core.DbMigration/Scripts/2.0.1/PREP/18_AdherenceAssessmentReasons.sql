if not Exists(select * from LookupMaster where Name like '%AdherenceAssessmentReasons%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('AdherenceAssessmentReasons','AdherenceAssessmentReasons','0')


go



if not exists(select * from LookupItem where Name like 'PillLost')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PillLost','Lost/out of pills','0')
END
go
if  exists(select * from LookupItem where Name like 'PillLost')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PillLost' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='PillLost'
END
END

go







if not exists(select * from LookupItem where Name like 'HivSeparated')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HivSeparated','Separated from HIV+','0')
END
go
if  exists(select * from LookupItem where Name like 'HivSeparated')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='HivSeparated' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='HivSeparated'
END
END


go



if not exists(select * from LookupItem where Name like 'NoPerceivedRisk')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('NoPerceivedRisk','No Perceived Risk','0')
END
go
if  exists(select * from LookupItem where Name like 'NoPerceivedRisk')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='NoPerceivedRisk' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='NoPerceivedRisk'
END
END

go




if not exists(select * from LookupItem where Name like 'SideEffects')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SideEffects','Side Effects','0')
END
go
if  exists(select * from LookupItem where Name like 'SideEffects')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SideEffects' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='SideEffects'
END
END

go

if not exists(select * from LookupItem where Name like 'Sick')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Sick','Sick','0')
END
go
if  exists(select * from LookupItem where Name like 'Sick')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Sick' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='Sick'
END
END

go


if not exists(select * from LookupItem where Name like 'Stigma')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Stigma','Stigma','0')
END
go
if  exists(select * from LookupItem where Name like 'Stigma')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Stigma' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='Stigma'
END
END


go

if not exists(select * from LookupItem where Name like 'Pill Burden')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Pill Burden','Pill Burden','0')
END
go
if  exists(select * from LookupItem where Name like 'Pill Burden')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Pill Burden' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='Pill Burden'
END
END



go




if not exists(select * from LookupItem where Name like '%SharedOthers')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SharedOthers','Shared with Others','0')
END
go
if  exists(select * from LookupItem where Name like 'SharedOthers')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SharedOthers' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='SharedOthers'
END
END



if not exists(select * from LookupItem where Name like 'None')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('None','None','0')
END
go
if  exists(select * from LookupItem where Name like 'None')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='None' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='None'
END
END




if not exists(select * from LookupItem where Name like 'Other')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Other','Other','0')
END
go
if  exists(select * from LookupItem where Name like 'Other')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Other' and lm.Name='AdherenceAssessmentReasons')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AdherenceAssessmentReasons' and lit.Name='Other'
END
END
