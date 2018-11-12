if not Exists(select * from LookupMaster where Name like 'SexualOrientation%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('SexualOrientation','SexualOrientation','0')
go
if not Exists(select * from LookupMaster where Name like 'HighRisk%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('HighRisk','HighRisk','0')

go

if not Exists(select * from LookupMaster where Name like 'SexualScreening%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('SexualScreening','SexualScreening','0')

go

if not exists(select * from LookupItem where Name like 'Yes%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Yes','yes','0')
END
go
if  exists(select * from LookupItem where Name like 'Yes')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Yes' and lm.Name='SexualScreening')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualScreening' and lit.Name='Yes'
END
END


go



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
lit.Name='No' and lm.Name='SexualScreening')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualScreening' and lit.Name='No'

END

END





if not exists(select * from LookupItem where Name like '%Heterosexual%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Heterosexual','Hetero sexual','0')
END
go
if  exists(select * from LookupItem where Name like 'Heterosexual%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Heterosexual' and lm.Name='SexualOrientation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualOrientation' and lit.Name='HeteroSexual'
END
END

if not exists(select * from LookupItem where Name like '%Homosexual%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Homosexual','Homo sexual','0')
END
go
if  exists(select * from LookupItem where Name like 'Homosexual%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Homosexual' and lm.Name='SexualOrientation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualOrientation' and lit.Name='HomoSexual'
END
END


if not exists(select * from LookupItem where Name like '%Bisexual%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Bisexual','Bi sexual','0')
END
go
if  exists(select * from LookupItem where Name like 'Bisexual%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Bisexual' and lm.Name='SexualOrientation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualOrientation' and lit.Name='BiSexual'
END
END



--High Risk


if not exists(select * from LookupItem where Name like '%Intergenerational%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Intergenerational','Intergenerational','0')
END
go
if  exists(select * from LookupItem where Name like 'Intergenerational%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Intergenerational' and lm.Name='HighRisk')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HighRisk' and lit.Name='Intergenerational'
END
END






if not exists(select * from LookupItem where Name like '%Sexforpay%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Sexforpay','Sex for pay','0')
END
go
if  exists(select * from LookupItem where Name like 'Sexforpay%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Sexforpay' and lm.Name='HighRisk')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HighRisk' and lit.Name='Sexforpay'
END
END





if not exists(select * from LookupItem where Name like '%Groupsex%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Groupsex','Group sex','0')
END
go
if  exists(select * from LookupItem where Name like 'Groupsex%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Groupsex' and lm.Name='HighRisk')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HighRisk' and lit.Name='Groupsex'
END
END




if not exists(select * from LookupItem where Name like '%MultiplePartners%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('MultiplePartners','Multiple Partners','0')
END
go
if  exists(select * from LookupItem where Name like 'MultiplePartners%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='MultiplePartners' and lm.Name='HighRisk')
BEGIN
insert into LookupMasterItem
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HighRisk' and lit.Name='MultiplePartners'
END
END



if not exists(select * from LookupItem where Name like '%SexwithoutCondoms%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SexwithoutCondoms','Sex without Condoms','0')
END
go
if  exists(select * from LookupItem where Name like 'SexwithoutCondoms%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SexwithoutCondoms' and lm.Name='HighRisk')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HighRisk' and lit.Name='SexwithoutCondoms'
END
END



if not exists(select * from LookupItem where Name like '%Sexwhenintoxicated%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Sexwhenintoxicated','Sex when intoxicated','0')
END
go
if  exists(select * from LookupItem where Name like 'Sexwhenintoxicated%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Sexwhenintoxicated' and lm.Name='HighRisk')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HighRisk' and lit.Name='Sexwhenintoxicated'
END
END
