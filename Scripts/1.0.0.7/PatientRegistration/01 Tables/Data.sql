if not exists(select * from Identifiers where Name like '%NationalId%')
BEGIN
insert into Identifiers(Name,Code,DisplayName,DataType,CreatedBy,CreateDate,IdentifierType)
 values ('NationalID','PersonIdentification','NationalID','Text','1',GetDate(),(select Id from IdentifierType where Name='Person'))
END
go
if not exists(select * from Identifiers where Name like '%Passport%')
BEGIN
insert into Identifiers(Name,Code,DisplayName,DataType,CreatedBy,CreateDate,IdentifierType)
 values ('Passport','PersonIdentification','Passport','Text','1',GetDate(),(select Id from IdentifierType where Name='Person'))
END
go
if not exists(select * from Identifiers where Name like '%AlienRegistration%')
BEGIN
insert into Identifiers(Name,Code,DisplayName,DataType,CreatedBy,CreateDate,IdentifierType)
 values ('AlienRegistration','PersonIdentification','Alien Registration','Text','1',GetDate(),(select Id from IdentifierType where Name='Person'))
END
go
if not exists(select * from Identifiers where Name like '%BirthCertificate%')
BEGIN
insert into Identifiers(Name,Code,DisplayName,DataType,CreatedBy,CreateDate,IdentifierType)
 values ('BirthCertificate','PersonIdentification','BirthCertificate','Text','1',GetDate(),(select Id from IdentifierType where Name='Person'))
END
go





if not Exists(select * from LookupMaster where Name like 'EducationalLevel%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('EducationalLevel','EducationalLevel','0')
go
if not Exists(select * from LookupMaster where Name like 'ConsentOptions%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('ConsentOptions','ConsentOptions','0')

go






--if not exists(select * from LookupItem where Name like '%NotDocumented%')
--BEGIN
--insert into LookupItem(Name,DisplayName,DeleteFlag)
--values('NotDocumented','Not Documented','0')
--END
--go

--if  exists(select * from LookupItem where Name like 'NotDocumented%')
--BEGIN
--if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
--inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
--lit.Name='NotDocumented' and lm.Name='PatientType')
--BEGIN
--insert into LookupMasterItem 
--select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
--where lm.Name='PatientType' and lit.Name='NotDocumented'
--END
--END

--go

if not exists(select * from LookupItem where Name like 'None%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('None','None','0')
END
go
if  exists(select * from LookupItem where Name like 'None%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='None' and lm.Name='EducationalLevel')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationalLevel' and lit.Name='None'
END
END

go

if not exists(select * from LookupItem where Name like 'PrimarySchool%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PrimarySchool','Primary School','0')
END
go
if  exists(select * from LookupItem where Name like 'PrimarySchool%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='PrimarySchool' and lm.Name='EducationalLevel')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationalLevel' and lit.Name='PrimarySchool'
END
END

go

if not exists(select * from LookupItem where Name like 'SecondarySchool%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values ('SecondarySchool','Secondary School','0')
END
go
if  exists(select * from LookupItem where Name like 'SecondarySchool%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='SecondarySchool' and lm.Name='EducationalLevel')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationalLevel' and lit.Name='SecondarySchool'
END
END

go


if not exists(select * from LookupItem where Name like 'College%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('College','College/Univesity','0')
END
go
if  exists(select * from LookupItem where Name like 'College%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='College' and lm.Name='EducationalLevel')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='EducationalLevel' and lit.Name='College'
END
END



go








--consenttype

if not exists(select * from LookupItem where Name like 'Granted%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Granted','Granted','0')
END
go
if  exists(select * from LookupItem where Name like 'Granted%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Granted' and lm.Name='ConsentOptions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ConsentOptions' and lit.Name='Granted'
END
END

go



if not exists(select * from LookupItem where Name like 'Concentnotsought%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Concentnotsought',' Concent not sought','0')
END
go
if  exists(select * from LookupItem where Name like 'Concentnotsought%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Concentnotsought' and lm.Name='ConsentOptions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ConsentOptions' and lit.Name='Concentnotsought'
END
END


go


if not exists(select * from LookupItem where Name like 'ConsentPending%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ConsentPending','Consent Pending','0')
END
go
if  exists(select * from LookupItem where Name like 'ConsentPending%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='ConsentPending' and lm.Name='ConsentOptions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ConsentOptions' and lit.Name='ConsentPending'
END
END

go

if not exists(select * from LookupItem where Name like 'ConsentRefused%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ConsentRefused','Consent Refused','0')
END
go
if  exists(select * from LookupItem where Name like 'ConsentRefused%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='ConsentRefused' and lm.Name='ConsentOptions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ConsentOptions' and lit.Name='ConsentRefused'
END
END

go






if not exists(select * from LookupItem where Name like 'LimitedConsent%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LimitedConsent','Limited Consent','0')
END
go
if  exists(select * from LookupItem where Name like 'LimitedConsent%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='LimitedConsent' and lm.Name='ConsentOptions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ConsentOptions' and lit.Name='LimitedConsent'
END
END


go




if not exists(select * from LookupItem where Name like 'ConsentRescinded%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ConsentRescinded','Consent Rescinded','0')
END
go
if  exists(select * from LookupItem where Name like 'ConsentRescinded%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='ConsentRescinded' and lm.Name='ConsentOptions')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='ConsentOptions' and lit.Name='ConsentRescinded'

END 
END
go

/*
if not exists(select * from ServiceArea where Name like '%Registration%')
BEGIN
insert into ServiceArea(Name,Code,DisplayName,CreatedBy,CreateDate,DeleteFlag)
values('Registration','REG','Registration',1,GetDate(),0)

END


go*/






if not Exists(select * from LookupMaster where Name like 'PersonContactType%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('PersonContactType','PersonContactType','0')
go


if not exists(select * from LookupItem where Name like 'Emergency%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Emergency','Emergency','0')
END
go
if  exists(select * from LookupItem where Name like 'Emergency%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Emergency' and lm.Name='PersonContactType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PersonContactType' and lit.Name='Emergency'
END
END

go

if not exists(select * from LookupItem where Name like 'Nextofkin%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('NextofKin','NextofKin','0')
END
go
if  exists(select * from LookupItem where Name like 'Nextofkin%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='NextofKin' and lm.Name='PersonContactType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PersonContactType' and lit.Name='Nextofkin'
END
END

go




if not exists (select * from LookupMaster where name like 'Occupation%')
insert into LookupMaster (Name,DisplayName,DeleteFlag)
values('Occupation','Occupation','0')
go


if not exists(select * from LookupItem where Name like 'Police%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PoliceOfficer','Police Officer','0')
END
go
if  exists(select * from LookupItem where Name like 'PoliceOfficer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='PoliceOfficer' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='PoliceOfficer'
END
END
go

if not exists(select * from LookupItem where Name like 'Nurse%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Nurse','Nurse','0')
END
go

if  exists(select * from LookupItem where Name like 'Nurse%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Nurse'and lm.Name='Occupation'   )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Nurse'
END
END

go
if not exists(select * from LookupItem where Name like 'Farmer%')
begin
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Farmer','Farmer','0')
End
go
if  exists(select * from LookupItem where Name like 'Farmer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Farmer' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Farmer'
END
END
go


if not exists(select * from LookupItem where Name like 'SecurityGuard%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SecurityGuard','Security Guard','0')
END
go

if  exists(select * from LookupItem where Name like 'SecurityGuard%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='SecurityGuard' and lm.Name='Occupation' )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='SecurityGuard'
END
END
go




if not exists(select * from LookupItem where Name like 'Teacher%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Teacher','Teacher','0')
END
go
if  exists(select * from LookupItem where Name like 'Teacher%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Teacher' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Teacher'
END
END
go


if  exists(select * from LookupItem where Name like 'BusinessPerson%')
BEGIN
if not exists(select * from LookupItem where Name like 'BusinessPerson%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('BusinessPerson','Business Person','0')
END
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='BusinessPerson' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='BusinessPerson'
END
END
go



if  exists(select * from LookupItem where Name like 'Mechanic%')
BEGIN
if not exists(select * from LookupItem where Name like 'Mechanic%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Mechanic','Mechanic','0')
END
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Mechanic' and lm.Name='Occupation' )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Mechanic'
END
END
go






if not exists(select * from LookupItem where Name like 'Driver%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Driver','Driver','0')
END

go
if  exists(select * from LookupItem where Name like 'Driver%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Driver' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Driver'
END
END
go





if not exists(select * from LookupItem where Name like 'Conductor%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Conductor','Conductor','0')
END

go
if  exists(select * from LookupItem where Name like 'Conductor%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Conductor' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Conductor'
END
END



if not exists(select * from LookupItem where Name like 'Saloonist%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Saloonist','Saloonist','0')
END

go
if  exists(select * from LookupItem where Name like 'Saloonist%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Saloonist' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Saloonist'
END
END



if not exists(select * from LookupItem where Name like 'SelfEmployment%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SelfEmployment','Self Employment','0')
END
go
if  exists(select * from LookupItem where Name like 'SelfEmployment%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='SelfEmployment' and lm.Name='Occupation' )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='SelfEmployment'
END
END
go

if not exists(select * from LookupItem where Name like 'Butcher%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Butcher','Butcher','0')
END

go
if  exists(select * from LookupItem where Name like 'Butcher%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Butcher' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Butcher'
END
END

go

if not exists(select * from LookupItem where Name like 'OfficeAssistant%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('OfficeAssistant','Office Assistant','0')
END

go

if  exists(select * from LookupItem where Name like 'OfficeAssistant%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='OfficeAssistant' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='OfficeAssistant'
END
END



if not exists(select * from LookupItem where Name like 'CasualLabouror%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CasualLabouror','Casual Labouror','0')
END
go




if  exists(select * from LookupItem where Name like 'CasualLabouror%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='CasualLabouror' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'14.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='CasualLabouror'
END
END


go


if not exists(select * from LookupItem where Name like 'Electrician%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Electrician','Electrician','0')
END

go
if  exists(select * from LookupItem where Name like 'Electrician%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Electrician' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'15.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Electrician'
END
END






if not exists(select * from LookupItem where Name like 'Printer%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Printer','Printer','0')
END
go


if  exists(select * from LookupItem where Name like 'Printer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Printer'  and lm.Name='Occupation')
BEGIN 
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'16.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Printer'
END
END






if not exists(select * from LookupItem where Name like 'Engineering%' or name like 'Engineer%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Engineering','Engineering','0')
END




if  exists(select * from LookupItem where Name like 'Engineering%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Engineering'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'17.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Engineering'
END
END






if not exists(select * from LookupItem where Name like 'Researcher%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Researcher','Researcher','0')
END

go

if  exists(select * from LookupItem where Name like 'Researcher%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Researcher' and lm.Name='Occupation'  )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'18.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Researcher'
END
END




go

if not exists(select * from LookupItem where Name like 'CivilServant%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('CivilServant','Civil Servant','0')
END

if  exists(select * from LookupItem where Name like 'CivilServant%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='CivilServant'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'19.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='CivilServant'
END
END




if not exists(select * from LookupItem where Name like 'Chief%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Chief','Chief','0')
ENd
go

if  exists(select * from LookupItem where Name like 'Chief%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Chief'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'20.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Chief'
END
END

go

if not exists(select * from LookupItem where Name like 'HairDresser%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HairDresser','Hair Dresser','0')
END

go
if  exists(select * from LookupItem where Name like 'HairDresser%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='HairDresser' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'21.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='HairDresser'
END
END


go



if not exists(select * from LookupItem where Name like 'Carpenter%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Carpenter','Carpenter','0')
END

if  exists(select * from LookupItem where Name like 'Carpenter%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Carpenter' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'22.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Carpenter'
END
END






if not exists(select * from LookupItem where Name like 'Auditor%')
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Auditor','Auditor','0')

if  exists(select * from LookupItem where Name like 'Auditor%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Auditor' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'23.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Auditor'
END
END










if not exists(select * from LookupItem where Name like 'Secretariat%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Secretariat','Secretariat','0')
END

if  exists(select * from LookupItem where Name like 'Secretariat%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Secretariat' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'24.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Secretariat'
END
END



go

if not exists(select * from LookupItem where Name like 'Hotelier%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Hotelier','Hotelier','0')
END


if  exists(select * from LookupItem where Name like 'Hotelier%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Hotelier' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'25.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Hotelier'
END
END




go


if not exists(select * from LookupItem where Name like 'Accountant%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Accountant','Accountant','0')
END




if  exists(select * from LookupItem where Name like 'Accountant%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Accountant' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'26.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Accountant'
END
END


go



if not exists(select * from LookupItem where Name like 'HouseWife%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HouseWife','House Wife','0')
END

go

if  exists(select * from LookupItem where Name like 'HouseWife%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='HouseWife' and lm.Name='Occupation' )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'27.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='HouseWife'
END
END







if not exists(select * from LookupItem where Name like 'BarTender%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('BarTender','BarTender','0')
END

go
if  exists(select * from LookupItem where Name like 'BarTender%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='BarTender'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'28.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='BarTender'
END
END

 go
 
   
   
if not exists(select * from LookupItem where Name like 'Masonist%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Masonist','Masonist','0')
END
go
if  exists(select * from LookupItem where Name like 'Masonist%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Masonist'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'29.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Masonist'
END
END

 go
 


 if not exists(select * from LookupItem where Name like 'Gardener%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Gardener','Gardener','0')
END

go


if  exists(select * from LookupItem where Name like 'Gardener%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Gardener' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'30.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Gardener'
END
END

 go
 




 if not exists(select * from LookupItem where Name like 'Tailor%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Tailor','Tailor','0')
END
go


if  exists(select * from LookupItem where Name like 'Tailor%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Tailor' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'31.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Tailor'
END
END

 go
 



 if not exists(select * from LookupItem where Name like 'ClinicalOfficer%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ClinicalOfficer','Clinical Officer','0')
END

go
if  exists(select * from LookupItem where Name like 'ClinicalOfficer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='ClinicalOfficer' and lm.Name='Occupation') 
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'32.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='ClinicalOfficer'
END
END

 go
 




 if not exists(select * from LookupItem where Name like 'Beutician%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Beutician','Beutician','0')
END


if  exists(select * from LookupItem where Name like 'Beutician%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Beutician'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'33.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Beutician'
END
END

 go
 







 if not exists(select * from LookupItem where Name like 'HeadMaster%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HeadMaster','Head Master','0')
END
go

if  exists(select * from LookupItem where Name like 'HeadMaster%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='HeadMaster'and lm.Name='Occupation' )
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'34.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='HeadMaster'
END
END

 go
 




 if not exists(select * from LookupItem where Name like 'Hawker%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Hawker','Hawker','0')
END




if  exists(select * from LookupItem where Name like 'Hawker%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Hawker'  and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'35.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Hawker'
END
END

go

  
  if not exists(select * from LookupItem where Name like 'ITSpecialist%')
  BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ITSpecialist','ITSpecialist','0')     
END

go


if  exists(select * from LookupItem where Name like 'ITSpecialist%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='ITSpecialist' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'36.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='ITSpecialist'
END
END



        

 if not exists(select * from LookupItem where Name like 'Supervisor%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Supervisor','Supervisor','0')  
END

go
if  exists(select * from LookupItem where Name like 'Supervisor%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Supervisor' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'37.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Supervisor'
END
END




             


 if not exists(select * from LookupItem where Name like 'Counsellor%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Counsellor','Counsellor','0')
END


go
if  exists(select * from LookupItem where Name like 'Counsellor%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Counsellor' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'38.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Counsellor'
END
END





go

 if not exists(select * from LookupItem where Name like 'Technician%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Technician','Technician','0')
END
  if  exists(select * from LookupItem where Name like 'Technician%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Technician' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'39.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Technician'
END
END
      


 if not exists(select * from LookupItem where Name like 'HouseKeeper%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('HouseKeeper','House Keeper','0')
 END
 go
   if  exists(select * from LookupItem where Name like 'HouseKeeper%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='HouseKeeper' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'40.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='HouseKeeper'
END
END
  go    

        

 if not exists(select * from LookupItem where Name like 'PoolAssistant%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('PoolAssistant','Pool Assistant','0')
END
go

   if  exists(select * from LookupItem where Name like 'PoolAssistant%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='PoolAssistant' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'41.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='PoolAssistant'
END
END
      
go
        





 if not exists(select * from LookupItem where Name like 'Cooks%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Cooks','Cooks','0')
END

go

   if  exists(select * from LookupItem where Name like 'Cooks%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Cooks' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'42.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Cooks'
END
END
      
go
 


 if not exists(select * from LookupItem where Name like 'Watchman%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Watchman','Watchman','0')
END
go

   if  exists(select * from LookupItem where Name like 'Watchman%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Watchman' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'43.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Watchman'
END
END
      
go
 





 if not exists(select * from LookupItem where Name like 'Messenger%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Messenger','Messenger','0')
END
go

   if  exists(select * from LookupItem where Name like 'Messenger%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Messenger' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'44.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Messenger'
END
END
      
go
 


 if not exists(select * from LookupItem where Name like 'Student%')
 BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Student','Student','0')
END
go
   if  exists(select * from LookupItem where Name like 'Student%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Student' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'45.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Student'
END
END
 go
 
  




if not exists(select * from LookupItem where Name like 'LaundryWorker%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LaundryWorker','Laundry Worker','0')
END
go

   if  exists(select * from LookupItem where Name like 'LaundryWorker%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='LaundryWorker' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'46.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='LaundryWorker'
END
END


go


if not exists(select * from LookupItem where Name like 'Lecturer%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Lecturer','Lecturer','0')
END




go

   if  exists(select * from LookupItem where Name like 'Lecturer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Lecturer' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'47.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Lecturer'
END
END


go







if not exists(select * from LookupItem where Name like 'TeaPicker%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('TeaPicker','Tea Picker','0')     
   END
   
   go
   if  exists(select * from LookupItem where Name like 'TeaPicker%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='TeaPicker' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'48.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='TeaPicker'
END
END


go

            





 
if not exists(select * from LookupItem where Name like 'Insurer%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Insurer','Insurer','0')     
    END
	
	
	
	  go
   if  exists(select * from LookupItem where Name like 'Insurer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Insurer' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'49.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Insurer'
END
END


go
                   






if not Exists(select * from LookupItem where Name like '%MachineOperator%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('MachineOperator','Machine Operator','0')  
END



	  go
   if  exists(select * from LookupItem where Name like '%MachineOperator%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='MachineOperator' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'50.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='MachineOperator'
END
END


go
    









if not Exists(select * from LookupItem where Name like '%Suboordinatestaff%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Suboordinatestaff','Subo-ordinate staff','0') 
END
	  go
   if  exists(select * from LookupItem where Name like '%Suboordinatestaff%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Suboordinatestaff' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'51.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Suboordinatestaff'
END
END


go
    



 


if not Exists(select * from LookupItem where Name like '%ClericalOfficer%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('ClericalOfficer','Clerical Officer','0')  
END


go


 if  exists(select * from LookupItem where Name like '%ClericalOfficer%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='ClericalOfficer' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'52.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='ClericalOfficer'
END
END

go




if not Exists(select * from LookupItem where Name like '%Manager%')
BEGIN
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Manager','Manager','0')  
END

go

if  exists(select * from LookupItem where Name like '%Manager%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Manager' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'53.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Manager'
END
END

go






if not Exists(select * from LookupItem where Name like '%Waiter%')
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Waiter','Waiter','0') 







go

if  exists(select * from LookupItem where Name like '%Waiter%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Waiter' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'54.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Waiter'
END
END

go


if not Exists(select * from LookupItem where Name like '%SocialWorker%')
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('SocialWorker','Social Worker','0')  
 




 go

if  exists(select * from LookupItem where Name like '%SocialWorker%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='SocialWorker' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'55.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='SocialWorker'
END
END

go


 if not Exists(select * from LookupItem where Name like '%LandScaping%')
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LandScaping','Land Scaping','0') 




go

if  exists(select * from LookupItem where Name like '%LandScaping%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='LandScaping' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'56.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='LandScaping'
END
END

go

 if not Exists(select * from LookupItem where Name like '%LabTechinician%')
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('LabTechinician','Lab Techinician','0') 


go

if  exists(select * from LookupItem where Name like '%LabTechinician%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='LabTechinician' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'57.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='LabTechinician'
END
END

go

 if not Exists(select * from LookupItem where Name like '%Statistician%')
insert into LookupItem(Name,DisplayName,DeleteFlag)
values('Statistician','Statistician','0') 


go

if  exists(select * from LookupItem where Name like '%Statistician%')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where lit.Name='Statistician' and lm.Name='Occupation')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'58.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='Occupation' and lit.Name='Statistician'
END
END

go


















