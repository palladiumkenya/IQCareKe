
if not Exists(select * from LookupMaster where Name like 'PartnerHIVStatus')
BEGIN
insert into LookupMaster ([Name],DisplayName,DeleteFlag)
values('PartnerHIVStatus','Established Partner Hiv Status','0')
END


if not exists(select * from LookupItem where Name like 'PartnerHIVStatus')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('PartnerHIVStatus','EstablishedParterHivStatus','0')
END
go
if  exists(select * from LookupItem where Name like 'PartnerHIVStatus')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PartnerHIVStatus' and lm.Name='PartnerHIVStatus')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PartnerHIVStatus' and lit.Name='PartnerHIVStatus'
END
END





if not exists(select * from LookupMaster where Name like 'ARTStartDate')
BEGIN
insert into LookupMaster ([Name],DisplayName,DeleteFlag)
values('ARTStartDate', 'ART Start Date', '0')
END
if not exists(select * from LookupItem where Name like 'PartnerARTStartDate')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PartnerARTStartDate','Partner ART StartDate','0')
END



if  exists(select * from LookupItem where Name like 'PartnerARTStartDate')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
 lit.Name='PartnerARTStartDate' and  lm.Name='ARTStartDate')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ARTStartDate' and lit.Name='PartnerARTStartDate'
END
END




if not exists(select * from LookupMaster where Name like 'Duration')
BEGIN
insert into LookupMaster(Name,DisplayName,DeleteFlag)
values('Duration','Duration','0')
END
go
if NOT  exists(select * from LookupItem where Name like 'HIVSeroDiscordant')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('HIVSeroDiscordant','HIVSeroDiscordant Duration',0)
END
IF  EXISTS(select * from LookupItem where Name like 'HIVSeroDiscordant')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
 lit.Name='HIVSeroDiscordant' and  lm.Name='Duration')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Duration' and lit.Name='HIVSeroDiscordant'
END
END





if not exists(select * from LookupMaster where [Name] like 'Children')
BEGIN
insert into LookupMaster(Name,DisplayName,DeleteFlag)
values('Children','Children','0')
END
go
if NOT exists(select * from LookupItem where Name like 'HIVPartnerChildren')
BEGIN
insert into LookupItem([Name],DisplayName,DeleteFlag)
values('HIVPartnerChildren','HIVPartnerChildren',0)
END
IF  EXISTS(select * from LookupItem where Name like 'HIVPartnerChildren')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
 lit.Name='HIVPartnerChildren' and  lm.Name='Children')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Children' and lit.Name='HIVPartnerChildren'
END
END




if not Exists(select * from LookupMaster where Name like 'PartnerCCCEnrollment')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('PartnerCCCEnrollment','PartnerCCCEnrollment','0')
END

if NOT exists(select * from LookupItem where Name like 'Yes')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Yes','Yes','0')
END
go
if  exists(select * from LookupItem where Name like 'Yes')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Yes' and lm.Name='PartnerCCCEnrollment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PartnerCCCEnrollment' and lit.Name='Yes'
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
lit.Name='No' and lm.Name='PartnerCCCEnrollment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PartnerCCCEnrollment' and lit.Name='No'
END
END





if not exists(select * from LookupItem where Name like 'Unknown')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Unknown','Unknown','0')
END
go
if  exists(select * from LookupItem where Name like 'Unknown')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Unknown' and lm.Name='PartnerCCCEnrollment')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PartnerCCCEnrollment' and lit.Name='Unknown'
END
END






if not Exists(select * from LookupMaster where Name like 'SexWithoutCondom')
BEGIN
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('SexWithoutCondom','SexWithoutCondom','0')
END

if NOT exists(select * from LookupItem where Name like 'Yes')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Yes','Yes','0')
END
go
if  exists(select * from LookupItem where Name like 'Yes')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Yes' and lm.Name='SexWithoutCondom')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexWithoutCondom' and lit.Name='Yes'
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
lit.Name='No' and lm.Name='SexWithoutCondom')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexWithoutCondom' and lit.Name='No'
END
END

