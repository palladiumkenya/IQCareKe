

IF NOT EXISTS(select * from LookupMaster where [Name] ='PrepPopulationType')
BEGIN
INSERT INTO LookupMaster([Name],DisplayName,[DeleteFlag])
values('PrepPopulationType','PrepPopulationType',0)
END
--Key.Pop
if  exists(select * from LookupItem where Name like 'Key.Pop')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Key.Pop' and lm.Name='PrepPopulationType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepPopulationType' and lit.Name='Key.Pop'
END
END

go

--Gen.Pop

if  exists(select * from LookupItem where Name like 'Gen.Pop')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Gen.Pop' and lm.Name='PrepPopulationType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepPopulationType' and lit.Name='Gen.Pop'
END
END

go

--

if not exists(select * from LookupItem where Name like 'Dis.Cop')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Dis.Cop','Discordant Couple','0')
END


if  exists(select * from LookupItem where Name like 'Dis.Cop')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Dis.Cop' and lm.Name='PrepPopulationType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PrepPopulationType' and lit.Name='Dis.Cop'
END
END

go








if not Exists(select * from LookupMaster where Name like '%DiscordantCouple%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('DiscordantCouple','Discordant Couple','0')







if not exists(select * from LookupItem where Name like 'AGYW')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('AGYW','AGYW','0')
END
go
if  exists(select * from LookupItem where Name like 'AGYW')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='AGYW' and lm.Name='DiscordantCouple')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='DiscordantCouple' and lit.Name='AGYW'
END
END



if not exists(select * from LookupItem where Name like 'FisherFolks')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('FisherFolks','Fisher Folks','0')
END
go
if  exists(select * from LookupItem where Name like 'FisherFolks')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='FisherFolks' and lm.Name='DiscordantCouple')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='DiscordantCouple' and lit.Name='FisherFolks'
END
END


