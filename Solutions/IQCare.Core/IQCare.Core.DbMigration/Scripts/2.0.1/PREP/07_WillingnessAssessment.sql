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
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RiskReductionEducation' and lit.Name='No'
END
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
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RiskReductionEducation' and lit.Name='Yes'
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


--client willing to take prep

if not Exists(select * from LookupMaster where Name like 'ClientWillingTakePrep')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('ClientWillingTakePrep','Client Willing to take PrEP','0')
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
lit.Name='Yes' and lm.Name='ClientWillingTakePrep')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientWillingTakePrep' and lit.Name='Yes'
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
lit.Name='No' and lm.Name='ClientWillingTakePrep')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ClientWillingTakePrep' and lit.Name='No'
END
END



go

--RiskEducationOffered


if not Exists(select * from LookupMaster where Name like 'RiskEducation')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('RiskEducation','Risk Education Offered','0')
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
lit.Name='Yes' and lm.Name='RiskEducation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RiskEducation' and lit.Name='Yes'
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
lit.Name='No' and lm.Name='RiskEducation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='RiskEducation' and lit.Name='No'
END
END


