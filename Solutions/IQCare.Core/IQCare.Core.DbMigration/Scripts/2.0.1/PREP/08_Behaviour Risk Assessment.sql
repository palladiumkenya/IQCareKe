if not Exists(select * from LookupMaster where Name like 'BehaviourRiskAssessment')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('BehaviourRiskAssessment','Behaviour Risk Assessment','0')
END
if not exists(select * from LookupItem where Name like 'BehaviourRiskAssessment')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('BehaviourRiskAssessment','Behaviour Risk Assessment','0')
END
go
if  exists(select * from LookupItem where Name like 'BehaviourRiskAssessment')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='BehaviourRiskAssessment' and lm.Name='BehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='BehaviourRiskAssessment' and lit.Name='BehaviourRiskAssessment'
END
END


go


if not exists(select * from LookupItem where Name like 'PrepRiskAssessment-encounter')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PrepRiskAssessment-encounter','PrepRiskAssessment-encounter','0')
END
go
if  exists(select * from LookupItem where Name like 'PrepRiskAssessment-encounter')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PrepRiskAssessment-encounter' and lm.Name='EncounterType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'22.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EncounterType' and lit.Name='PrepRiskAssessment-encounter'
END
END
