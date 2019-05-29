if not Exists(select * from LookupMaster where Name like '%SexualPartnerHivStatusProfile%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('SexualPartnerHivStatusProfile','SexualPartnerHivStatusProfile','0')


go



if not exists(select * from LookupItem where Name like 'NotOnArt%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('NotOnArt','Not on ART','0')
END
go
if  exists(select * from LookupItem where Name like 'NotOnArt%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='NotOnArt' and lm.Name='SexualPartnerHivStatusProfile')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualPartnerHivStatusProfile' and lit.Name='NotOnArt'
END
END

go


if not exists(select * from LookupItem where Name like 'OnArtForLessThan6%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('OnArtForLessThan6','On ART for less than 6','0')
END
go
if  exists(select * from LookupItem where Name like 'OnArtForLessThan6%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='OnArtForLessThan6' and lm.Name='SexualPartnerHivStatusProfile')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualPartnerHivStatusProfile' and lit.Name='OnArtForLessThan6'
END
END

go


if not exists(select * from LookupItem where Name like 'ARTPoorAdherence%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ARTPoorAdherence','Poor adherence to ART use','0')
END
go
if  exists(select * from LookupItem where Name like 'ARTPoorAdherence%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ARTPoorAdherence' and lm.Name='SexualPartnerHivStatusProfile')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualPartnerHivStatusProfile' and lit.Name='ARTPoorAdherence'
END
END

go


if not exists(select * from LookupItem where Name like '%DetectableViralLoad%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('DetectableViralLoad','Detectable HIV viral load','0')
END
go
if  exists(select * from LookupItem where Name like 'DetectableViralLoad%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='DetectableViralLoad' and lm.Name='SexualPartnerHivStatusProfile')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualPartnerHivStatusProfile' and lit.Name='DetectableViralLoad'
END
END

go


if not exists(select * from LookupItem where Name like 'CoupleConceive%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CoupleConceive','Couple is trying to conceive','0')
END
go
if  exists(select * from LookupItem where Name like 'CoupleConceive%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='CoupleConceive' and lm.Name='SexualPartnerHivStatusProfile')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='SexualPartnerHivStatusProfile' and lit.Name='CoupleConceive'
END
END

