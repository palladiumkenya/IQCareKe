if not Exists(select * from LookupMaster where Name like 'WHOStageIConditions%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('WHOStageIConditions','WHOStageIConditions','0')

go

if not Exists(select * from LookupMaster where Name like 'WHOStageIIConditions%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('WHOStageIIConditions','WHOStageIIConditions','0')

go

if not Exists(select * from LookupMaster where Name like 'WHOStageIIIConditions%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('WHOStageIIIConditions','WHOStageIIIConditions','0')

go

if not Exists(select * from LookupMaster where Name like 'WHOStageIVConditions%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('WHOStageIVConditions','WHOStageIVConditions','0')


go

if not exists(select * from LookupItem where Name like 'Asymptomatic%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Asymptomatic','Asymptomatic','0')
END
go
if  exists(select * from LookupItem where Name like 'Asymptomatic%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Asymptomatic' and lm.Name='WHOStageIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIConditions' and lit.Name='Asymptomatic'
END
END

go


if not exists(select * from LookupItem where Name like 'Lymphadenopathy%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Lymphadenopathy','Lymphadenopathy','0')
END
go
if  exists(select * from LookupItem where Name like 'Lymphadenopathy%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Lymphadenopathy' and lm.Name='WHOStageIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIConditions' and lit.Name='Lymphadenopathy'
END
END


go



if not exists(select * from LookupItem where Name like '%Herpeszoster%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Herpeszoster','Herpes zoster','0')
END
go
if  exists(select * from LookupItem where Name like 'Herpeszoster%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Herpeszoster' and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Herpeszoster'
END
END




if not exists(select * from LookupItem where Name like '%Seborrheicdermatitis%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Seborrheicdermatitis','Seborrheic dermatitis','0')
END
go
if  exists(select * from LookupItem where Name like 'Seborrheicdermatitis%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Seborrheicdermatitis' and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Seborrheicdermatitis'
END
END


go

if not exists(select * from LookupItem where Name like '%Prurigo%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Prurigo','Prurigo','0')
END
go
if  exists(select * from LookupItem where Name like 'Prurigo%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Prurigo' and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Prurigo'
END
END

go



if not exists(select * from LookupItem where Name like '%Recurrentoralulceration%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Recurrentoralulceration','Recurrent oral ulceration','0')
END
go
if  exists(select * from LookupItem where Name like 'Recurrentoralulceration%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Recurrentoralulceration'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Recurrentoralulceration' 
END
END


go



if not exists(select * from LookupItem where Name like '%Angular chelitis%'  or Name like 'Angularchelitis')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Angularchelitis','Angular chelitis','0')
END
go
if  exists(select * from LookupItem where Name like 'Angularchelitis%'  or  Name like '%Angular chelitis' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Angularchelitis'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Angularchelitis' 
END
END


go


if not exists(select * from LookupItem where Name like '%Recurrent URTI%'  or Name like 'RecurrentURTI')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('RecurrentURTI','Recurrent URTI','0')
END
go
if  exists(select * from LookupItem where Name like 'RecurrentURTI%'  or  Name like '%Recurrent URTI' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='RecurrentURTI'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='RecurrentURTI' 
END
END


go




if not exists(select * from LookupItem where  Name like 'Weightloss<10%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Weightloss<10%','Weight loss < 10%','0')
END
go
if  exists(select * from LookupItem where  Name like '%Weightloss<10%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Weightloss<10%'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Weightloss<10%' 
END
END


go





if not exists(select * from LookupItem where Name like '%Hepatosplenomegaly%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Hepatosplenomegaly','Hepatosplenomegaly','0')
END
go
if  exists(select * from LookupItem where Name like 'Hepatosplenomegaly%'  )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Hepatosplenomegaly'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Hepatosplenomegaly' 
END
END


go


if not exists(select * from LookupItem where Name like '%Lineargingivalerythema%'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Lineargingivalerythema','Linear gingivalerythema','0')
END
go
if  exists(select * from LookupItem where Name like 'Lineargingivalerythema'   )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Lineargingivalerythema'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Lineargingivalerythema' 
END
END

go



if not exists(select * from LookupItem where Name like '%Minorskinmanifestation%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Minorskinmanifestation','Minor skin manifestation','0')
END
go
if  exists(select * from LookupItem where Name like 'Minorskinmanifestation'  )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Minorskinmanifestation'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Minorskinmanifestation' 
END
END


go



if not exists(select * from LookupItem where Name like '%Molluscum%'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Molluscum','Molluscum','0')
END
go
if  exists(select * from LookupItem where Name like 'Molluscum'   )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Molluscum'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='Molluscum' 
END
END

go

if not exists(select * from LookupItem where Name like '%ParotidEnlargement%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ParotidEnlargement','Parotid Enlargement','0')
END
go
if  exists(select * from LookupItem where Name like 'ParotidEnlargement'   )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ParotidEnlargement'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='ParotidEnlargement' 
END
END

go





if not exists(select * from LookupItem where Name like '%OtherWHOStageIICondition%'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('OtherWHOStageIICondition','Other WHO Stage II Condition','0')
END
go
if  exists(select * from LookupItem where Name like 'OtherWHOStageIICondition'   )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='OtherWHOStageIICondition'  and lm.Name='WHOStageIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIConditions' and lit.Name='OtherWHOStageIICondition' 
END
END


go


if not exists(select * from LookupItem where Name like '%PulmonaryTBSmear+%'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PulmonaryTBSmear+','Pulmonary TB Smear +','0')
END
go
if  exists(select * from LookupItem where Name like 'PulmonaryTBSmear+' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PulmonaryTBSmear+'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='PulmonaryTBSmear+' 
END
END


go






if not exists(select * from LookupItem where Name like '%PulmonaryTBSmear-%'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PulmonaryTBSmear-','Pulmonary TB Smear -','0')
END
go
if  exists(select * from LookupItem where Name like 'PulmonaryTBSmear-'   )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PulmonaryTBSmear-'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='PulmonaryTBSmear-' 
END
END

go



if not exists(select * from LookupItem where  name like 'Chronicdiarrhoeawasting'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Chronicdiarrhoeawasting','Chronic diarrhoea wasting','0')
END
go
if  exists(select * from LookupItem where  Name like '%Chronicdiarrhoeawasting%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Chronicdiarrhoeawasting'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Chronicdiarrhoeawasting' 
END
END

go




if not exists(select * from LookupItem where  name like 'Oralcandidiasisthrush'  )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Oralcandidiasisthrush','Oral candidiasis thrush','0')
END
go
if  exists(select * from LookupItem where   Name like '%Oralcandidiasisthrush%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Oralcandidiasisthrush'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Oralcandidiasisthrush' 
END
END


go





if not exists(select * from LookupItem where Name like '%Oralhairyleukoplakia%'   )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Oralhairyleukoplakia','Oral hairy leukoplakia','0')
END
go
if  exists(select * from LookupItem where  Name like '%Oralhairyleukoplakia%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Oralhairyleukoplakia'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Oralhairyleukoplakia' 
END
END


go




if not exists(select * from LookupItem where Name like '%Pneumonia%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Pneumonia','Pneumonia','0')
END
go
if  exists(select * from LookupItem where Name like '%Pneumonia%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Pneumonia'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Pneumonia' 
END
END



go




if not exists(select * from LookupItem where Name like '%Weightloss>10%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Weightloss>10%','Weight loss > 10%','0')
END
go
if  exists(select * from LookupItem where  Name like '%Weightloss>10%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Weightloss>10%'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Weightloss>10%' 
END
END


go





if not exists(select * from LookupItem where  name like 'Prolongedfever%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Prolongedfever','Prolonged fever','0')
END
go
if  exists(select * from LookupItem where  Name like '%Prolongedfever%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Prolongedfever'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Prolongedfever' 
END
END
go





if not exists(select * from LookupItem where  name like 'LymphnodeTB%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LymphnodeTB','Lymphnode TB','0')
END
go
if  exists(select * from LookupItem where Name like '%LymphnodeTB%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='LymphnodeTB'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='LymphnodeTB' 
END
END


go





if not exists(select * from LookupItem where  name like 'Unexplainedanemia%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Unexplainedanemia','Unexplained anemia','0')
END
go
if  exists(select * from LookupItem where  Name like '%Unexplainedanemia%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Unexplainedanemia'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Unexplainedanemia' 
END
END


go



if not exists(select * from LookupItem where  name like 'Moderatemalnutrition%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Moderatemalnutrition','Moderate malnutrition','0')
END
go
if  exists(select * from LookupItem where Name like '%Moderatemalnutrition%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Moderatemalnutrition'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Moderatemalnutrition' 
END
END


go










if not exists(select * from LookupItem where Name like '%Ulcerativegingivitis%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Ulcerativegingivitis','Ulcerative gingivitis','0')
END
go
if  exists(select * from LookupItem where Name like 'Ulcerativegingivitis'   )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Ulcerativegingivitis'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='Ulcerativegingivitis' 
END
END

go






if not exists(select * from LookupItem where Name like '%PneumonitisLIP%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PneumonitisLIP','Pneumonitis LIP','0')
END
go
if  exists(select * from LookupItem where Name like 'PneumonitisLIP'  )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PneumonitisLIP'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='PneumonitisLIP' 
END
END


go




if not exists(select * from LookupItem where Name like '%LungdiseaseHIVAssociated%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LungdiseaseHIVAssociated','Lung disease HIV Associated','0')
END
go
if  exists(select * from LookupItem where Name like 'LungdiseaseHIVAssociated' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='LungdiseaseHIVAssociated'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='LungdiseaseHIVAssociated' 
END
END


go



if not exists(select * from LookupItem where Name like '%ExtrapulmonaryCryptococcosis%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ExtrapulmonaryCryptococcosis','Extrapulmonary Cryptococcosis','0')
END
go
if  exists(select * from LookupItem where Name like 'ExtrapulmonaryCryptococcosis')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ExtrapulmonaryCryptococcosis'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'14.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='ExtrapulmonaryCryptococcosis' 
END
END


go






if not exists(select * from LookupItem where Name like '%OtherWHOStageIIICondition%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('OtherWHOStageIIICondition','Other WHO Stage III Condition','0')
END
go
if  exists(select * from LookupItem where Name like 'OtherWHOStageIIICondition'  )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='OtherWHOStageIIICondition'  and lm.Name='WHOStageIIIConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'14.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIIIConditions' and lit.Name='OtherWHOStageIIICondition' 
END
END



go









if not exists(select * from LookupItem where Name like '%Encephalopathydementia%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Encephalopathydementia','Encephalopathy dementia','0')
END
go
if  exists(select * from LookupItem where Name like 'Encephalopathydementia' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Encephalopathydementia'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='Encephalopathydementia' 
END
END





go



if not exists(select * from LookupItem where Name like '%ExtrapulmonaryTB%' )
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ExtrapulmonaryTB','Extrapulmonary TB','0')
END
go
if  exists(select * from LookupItem where  Name like '%ExtrapulmonaryTB%' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ExtrapulmonaryTB'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='ExtrapulmonaryTB' 
END
END


go


if not exists(select * from LookupItem where name like 'KSCutaneous%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('KSCutaneous','KS Cutaneous','0')
END
go
if  exists(select * from LookupItem where Name like 'KSCutaneous' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='KSCutaneous'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='KSCutaneous' 
END
END

go






if not exists(select * from LookupItem where name like 'KSVisceral%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('KSVisceral','KS Visceral','0')
END
go
if  exists(select * from LookupItem where Name like 'KSVisceral' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='KSVisceral'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='KSVisceral' 
END
END

go


if not exists(select * from LookupItem where name like 'CandidiasisOesephogeal%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CandidiasisOesephogeal','Candidiasis Oesephogeal','0')
END
go
if  exists(select * from LookupItem where Name like 'CandidiasisOesephogeal' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='CandidiasisOesephogeal'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='CandidiasisOesephogeal' 
END
END


go





if not exists(select * from LookupItem where name like 'CMVRetinitis%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CMVRetinitis','CMV Retinitis','0')
END
go
if  exists(select * from LookupItem where Name like 'CMVRetinitis' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='CMVRetinitis'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='CMVRetinitis' 
END
END



go



if not exists(select * from LookupItem where name like 'ChronicHerpessimplex%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ChronicHerpessimplex','Chronic Herpes simplex','0')
END
go
if  exists(select * from LookupItem where Name like 'ChronicHerpessimplex' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='ChronicHerpessimplex'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='ChronicHerpessimplex' 
END
END


go




if not exists(select * from LookupItem where name like 'Salmonellosis%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Salmonellosis','Salmonellosis','0')
END
go
if  exists(select * from LookupItem where Name like 'Salmonellosis' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Salmonellosis'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='Salmonellosis' 
END
END




go





if not exists(select * from LookupItem where name like 'Mycobacteria%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Mycobacteria','Mycobacteria','0')
END
go
if  exists(select * from LookupItem where Name like 'Mycobacteria' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Mycobacteria'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='Mycobacteria' 
END
END


go




if not exists(select * from LookupItem where name like 'PCP%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PCP','PCP','0')
END
go
if  exists(select * from LookupItem where Name like 'PCP' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='PCP'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='PCP' 
END
END




go





if not exists(select * from LookupItem where name like 'Cryptosporidiosiswithdiarrhoea%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Cryptosporidiosiswithdiarrhoea','Cryptosporidiosis with diarrhoea','0')
END
go
if  exists(select * from LookupItem where Name like 'Cryptosporidiosiswithdiarrhoea' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Cryptosporidiosiswithdiarrhoea'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='Cryptosporidiosiswithdiarrhoea' 
END
END


go





if not exists(select * from LookupItem where name like 'Progressivemultifocal%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Progressivemultifocal','Progressive multifocal','0')
END
go
if  exists(select * from LookupItem where Name like 'Progressivemultifocal' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Progressivemultifocal'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='Progressivemultifocal' 
END
END


go



if not exists(select * from LookupItem where name like 'Leukoencephalopathy%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Leukoencephalopathy','Leukoencephalopathy','0')
END
go
if  exists(select * from LookupItem where Name like 'Leukoencephalopathy' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Leukoencephalopathy'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='Leukoencephalopathy' 
END
END


go




if not exists(select * from LookupItem where name like 'EndemicMycosis%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('EndemicMycosis','Endemic Mycosis','0')
END
go
if  exists(select * from LookupItem where Name like 'EndemicMycosis' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='EndemicMycosis'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='EndemicMycosis' 
END
END

go




if not exists(select * from LookupItem where name like 'AtypicalMycobacteriosisdisseminated%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('AtypicalMycobacteriosisdisseminated','Atypical Mycobacteriosis disseminated','0')
END
go
if  exists(select * from LookupItem where Name like 'AtypicalMycobacteriosisdisseminated' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='AtypicalMycobacteriosisdisseminated'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='AtypicalMycobacteriosisdisseminated' 
END
END



go

if not exists(select * from LookupItem where name like 'CNSToxoplasmosis%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CNSToxoplasmosis','CNS Toxoplasmosis','0')
END
go
if  exists(select * from LookupItem where Name like 'CNSToxoplasmosis' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='CNSToxoplasmosis'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='CNSToxoplasmosis' 
END
END



go





if not exists(select * from LookupItem where name like 'HIVrelatedCardiomyopathy%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HIVrelatedCardiomyopathy','HIV related Cardiomyopathy','0')
END
go
if  exists(select * from LookupItem where Name like 'HIVrelatedCardiomyopathy' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='HIVrelatedCardiomyopathy'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='HIVrelatedCardiomyopathy' 
END
END



go


if not exists(select * from LookupItem where name like 'HIVrelatedNephropathy%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HIVrelatedNephropathy','HIV related Nephropathy','0')
END
go
if  exists(select * from LookupItem where Name like 'HIVrelatedNephropathy' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='HIVrelatedNephropathy'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='HIVrelatedNephropathy' 
END
END

go



if not exists(select * from LookupItem where name like 'CerebralBCellNon-HodgkinsLymphoma%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CerebralBCellNon-Hodgkins','CerebralB Cell Non-Hodgkins Lymphoma','0')
END
go
if  exists(select * from LookupItem where Name like 'CerebralBCellNon-HodgkinsLymphoma' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='CerebralBCellNon-HodgkinsLymphoma'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'14.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='CerebralBCellNon-HodgkinsLymphoma' 
END
END

go




if not exists(select * from LookupItem where name like 'AcquiredRectoVesicoFistula%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('AcquiredRectoVesicoFistula','Acquired Recto Vesico Fistula','0')
END
go
if  exists(select * from LookupItem where Name like 'AcquiredRectoVesicoFistula' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='AcquiredRectoVesicoFistula'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'15.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='AcquiredRectoVesicoFistula' 
END
END





go




if not exists(select * from LookupItem where name like 'DisseminatedMycobacterialnotTB%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('DisseminatedMycobacterialnotTB','Disseminated Mycobacterial not TB','0')
END
go
if  exists(select * from LookupItem where Name like 'DisseminatedMycobacterialnotTB' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='DisseminatedMycobacterialnotTB'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'16.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='DisseminatedMycobacterialnotTB' 
END
END

go




if not exists(select * from LookupItem where name like 'SevereWasting3SD%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SevereWasting3SD','Severe Wasting 3SD','0')
END
go
if  exists(select * from LookupItem where Name like 'SevereWasting3SD' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SevereWasting3SD'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'17.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='SevereWasting3SD' 
END
END


go



if not exists(select * from LookupItem where name like 'RecurrentSevereBacterialInfections%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('RecurrentSevereBacterialInfections','Recurrent Severe Bacterial Infections','0')
END
go
if  exists(select * from LookupItem where Name like 'RecurrentSevereBacterialInfections' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='RecurrentSevereBacterialInfections'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'18.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='RecurrentSevereBacterialInfections' 
END
END


go


if not exists(select * from LookupItem where name like 'DisseminatedEndemicMycosis%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('DisseminatedEndemicMycosis','Disseminated Endemic Mycosis','0')
END
go
if  exists(select * from LookupItem where Name like 'DisseminatedEndemicMycosis' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='DisseminatedEndemicMycosis'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'19.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='DisseminatedEndemicMycosis' 
END
END


go



if not exists(select * from LookupItem where name like 'OtherWHOStageIVCondition%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('OtherWHOStageIVCondition','Other WHO Stage IV Condition','0')
END
go
if  exists(select * from LookupItem where Name like 'OtherWHOStageIVCondition' )
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='OtherWHOStageIVCondition'  and lm.Name='WHOStageIVConditions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'20.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='WHOStageIVConditions' and lit.Name='OtherWHOStageIVCondition' 
END
END






















