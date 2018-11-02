
if not exists(select * from LookupMaster where Name like 'EducationOutcome%')
BEGIN
INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag)
values('EducationOutcome','Education Outcome','0')
END



if not exists(select * from LookupItem  where  Name like 'Completed')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Completed','Completed','0')
END
go
if  exists(select * from LookupItem where Name like 'Completed')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Completed' and lm.Name='EducationOutcome')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationOutcome' and lit.Name='Completed'
END
END



if not exists(select * from LookupItem  where  Name like 'NotCompleted')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('NotCompleted','NotCompleted','0')
END
go
if  exists(select * from LookupItem where Name like 'NotCompleted')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='NotCompleted' and lm.Name='EducationOutcome')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationOutcome' and lit.Name='NotCompleted'
END
END






















if not exists(select * from LookupItem  where  Name like 'MSW')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('MSW','Male Sex Worker','0')
END
go
if  exists(select * from LookupItem where Name like 'MSW')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='MSW' and lm.Name='KeyPopulation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='KeyPopulation' and lit.Name='MSW'
END
END


go




if not exists(select * from LookupMaster where  Name like 'HTSOccupation')
BEGIN
insert into LookupMaster(Name,DisplayName,DeleteFlag)
values('HTSOccupation','HTSOccupation','0')
END
go





if not exists(select * from LookupItem  where  Name like 'Transportation')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Transportation','Transportation','0')
END
go
if  exists(select * from LookupItem where Name like 'Transportation')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Transportation' and lm.Name='HTSOccupation')
BEGIN


insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Transportation'
END
END







if not exists(select * from LookupItem  where  Name like 'SexWorker')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SexWorker','Sex Worker','0')
END
go
if  exists(select * from LookupItem where Name like 'SexWorker')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SexWorker' and lm.Name='HTSOccupation')
BEGIN


insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,2 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='SexWorker'
END
END




if not exists(select * from LookupItem  where  Name like 'Professional')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Professional','Professional','0')
END
go
if  exists(select * from LookupItem where Name like 'Professional')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Professional' and lm.Name='HTSOccupation')
BEGIN



insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,3 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Professional'
END
END




if not exists(select * from LookupItem  where  Name like '%BusinessOwner')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('BusinessOwner','Business Owner','0')
END
go
if  exists(select * from LookupItem where Name like 'BusinessOwner')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='BusinessOwner' and lm.Name='HTSOccupation')
BEGIN
DECLARE @No as int=(select top 1.OrdRank from LookupItemView lv where lv.MasterName='HTSOccupation'
order by OrdRank desc)
SET @No=@No+1;


insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,4 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='BusinessOwner'
END
END






if not exists(select * from LookupItem  where   Name like 'Unskilledlabor')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Unskilledlabor','Unskilled labor','0')
END
go
if  exists(select * from LookupItem where Name like 'Unskilledlabor')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Unskilledlabor' and lm.Name='HTSOccupation')
BEGIN



insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,5 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Unskilledlabor'
END
END



if not exists(select * from LookupItem  where  DisplayName like 'Student')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Student','Student','0')
END
go
if  exists(select * from LookupItem where Name like 'Student')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Student' and lm.Name='HTSOccupation')
BEGIN



insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,6 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Student'
END
END






if not exists(select * from LookupItem  where  DisplayName like 'Homemaker')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Homemaker','Homemaker','0')
END
go
if  exists(select * from LookupItem where Name like 'Homemaker')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Homemaker' and lm.Name='HTSOccupation')
BEGIN



insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,7 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Homemaker'
END
END







if not exists(select * from LookupItem  where  DisplayName like 'Unemployed')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Unemployed','Unemployed','0')
END
go
if  exists(select * from LookupItem where Name like 'Unemployed')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Unemployed' and lm.Name='HTSOccupation')
BEGIN



insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,8 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Unemployed'
END
END






if not exists(select * from LookupItem  where  DisplayName like 'Other')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Other','Other','0')
END
go
if  exists(select * from LookupItem where Name like 'Other')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Other' and lm.Name='HTSOccupation')
BEGIN



insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,9 as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSOccupation' and lit.Name='Other'
END
END





if not exists(select * from LookupItem  where  DisplayName like 'PNS')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PNS','PNS','0')
END
go
if  exists(select * from LookupItem where Name like 'PNS')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PNS' and lm.Name='HTSEntryPoints')
BEGIN
DECLARE @No as int=(select top 1.OrdRank from LookupItemView lv where lv.MasterName='HTSEntryPoints'
order by OrdRank desc)
SET @No=@No+1;


insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,(Select @No) as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='HTSEntryPoints' and lit.Name='PNS'
END
END



if not exists(select * from LookupItem  where  DisplayName like 'PNS')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PNS','PNS','0')
END
go
if  exists(select * from LookupItem where Name like 'PNS')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PNS' and lm.Name='Strategy')
BEGIN
DECLARE @No as int=(select top 1.OrdRank from LookupItemView lv where lv.MasterName='Strategy'
order by OrdRank desc)
SET @No=@No+1;


insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,(Select @No) as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Strategy' and lit.Name='PNS'
END
END