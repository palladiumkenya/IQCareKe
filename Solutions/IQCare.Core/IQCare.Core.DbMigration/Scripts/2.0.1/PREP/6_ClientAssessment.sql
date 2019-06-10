if not Exists(select * from LookupMaster where Name like '%ClientsBehaviourRiskAssessment%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('ClientsBehaviourRiskAssessment','Clients Behaviour Risk Assessment','0')





if not exists(select * from LookupItem where Name like 'SexualPartnersHivRisk%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SexualPartnersHivRisk','Sex partner(s) at high risk for HIV & HIV status unknown','0')
END
go
if  exists(select * from LookupItem where Name like 'SexualPartnersHivRisk%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SexualPartnersHivRisk' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='SexualPartnersHivRisk'
END
END

go
if not exists(select * from LookupItem where Name like 'SexMoreThan1Partner%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SexMoreThan1Partner','Has sex with more than 1 partner','0')
END
go
if  exists(select * from LookupItem where Name like 'SexMoreThan1Partner%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SexMoreThan1Partner' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='SexMoreThan1Partner'
END
END


go
if not exists(select * from LookupItem where Name like 'OngoingIPVGBV%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('OngoingIPVGBV','Ongoing IPV/GBV','0')
END
go
if  exists(select * from LookupItem where Name like 'OngoingIPVGBV%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='OngoingIPVGBV' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='OngoingIPVGBV'
END
END

go




if not exists(select * from LookupItem where Name like 'TransactionalSex%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('TransactionalSex','Transactional Sex','0')
END
go
if  exists(select * from LookupItem where Name like 'TransactionalSex%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='TransactionalSex' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='TransactionalSex'
END
END


go



if not exists(select * from LookupItem where Name like 'RecentSTI%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('RecentSTI','Recent STI(past 6 months)','0')
END
go
if  exists(select * from LookupItem where Name like 'RecentSTI%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='RecentSTI' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='RecentSTI'
END
END


go



if not exists(select * from LookupItem where Name like 'RecurrentUsePEP%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('RecurrentUsePEP','Recurrent use of post exposure prophylaxis','0')
END
go
if  exists(select * from LookupItem where Name like 'RecurrentUsePEP%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='RecurrentUsePEP' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='RecurrentUsePEP'
END
END

go



if not exists(select * from LookupItem where Name like 'RecurrentSexInfluence%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('RecurrentSexInfluence','Recurrent sex under the influence of alcohol/recreational','0')
END
go
if  exists(select * from LookupItem where Name like 'RecurrentSexInfluence%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='RecurrentSexInfluence' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='RecurrentSexInfluence'
END
END


go


if not exists(select * from LookupItem where Name like 'InconsistentCondomUse%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('InconsistentCondomUse','Inconsistent or no condom use','0')
END
go
if  exists(select * from LookupItem where Name like 'InconsistentCondomUse%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='InconsistentCondomUse' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='InconsistentCondomUse'
END
END

go




if not exists(select * from LookupItem where Name like 'InjectionDrugUse%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('InjectionDrugUse','Injection drug use with shared needles and/or syringes','0')
END
go
if  exists(select * from LookupItem where Name like 'InjectionDrugUse%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='InjectionDrugUse' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientsBehaviourRiskAssessment' and lit.Name='InjectionDrugUse'
END
END



--Assessment
go

if not Exists(select * from LookupMaster where Name like 'AssessmentOutCome')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('AssessmentOutCome','Assessment Outcome','0')
END
if not exists(select * from LookupItem where Name like 'Risk')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Risk','Risk','0')
END
go
if  exists(select * from LookupItem where Name like 'Risk')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Risk' and lm.Name='AssessmentOutCome')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AssessmentOutCome' and lit.Name='Risk'
END
END

go



if not exists(select * from LookupItem where Name like 'NoRisk')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('NoRisk','No Risk','0')
END
go
if  exists(select * from LookupItem where Name like 'NoRisk')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='NoRisk' and lm.Name='AssessmentOutCome')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='AssessmentOutCome' and lit.Name='NoRisk'
END
END




--RiskReduction Education
if not Exists(select * from LookupMaster where Name like 'RiskReductionEducation')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('RiskReductionEducation','Risk Reduction Education Offered','0')
END

if not Exists(select * from LookupMaster where Name like 'RiskReductionEducation')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('RiskReductionEducation','Risk Reduction Education Offered','0')
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
lit.Name='Yes' and lm.Name='RiskReductionEducation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RiskReductionEducation' and lit.Name='Yes'
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
lit.Name='No' and lm.Name='RiskReductionEducation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RiskReductionEducation' and lit.Name='No'
END
END




--Referral For PreventionServices

if not Exists(select * from LookupMaster where Name like 'ReferralPreventionServices')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('ReferralPreventionServices','Referral For Other Prevention Services','0')
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
lit.Name='Yes' and lm.Name='ReferralPreventionServices')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ReferralPreventionServices' and lit.Name='Yes'
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
lit.Name='No' and lm.Name='ReferralPreventionServices')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ReferralPreventionServices' and lit.Name='No'
END
END




