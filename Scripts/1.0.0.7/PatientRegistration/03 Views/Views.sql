IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ContactsListView]'))
DROP VIEW [dbo].[ContactsListView]
GO


/****** Object:  View [dbo].[ContactsListView]    Script Date: 6/18/2018 12:54:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[ContactsListView]
AS
SELECT        p.Id,CAST(DECRYPTBYKEY(p.FirstName) as varchar(100)) as FirstName,
CAST(DECRYPTBYKEY(p.MidName) as varchar(100)) as MiddleName,
CAST(DECRYPTBYKEY(p.LastName) as varchar(100)) as LastName, pc.PersonId, 
CAST(DECRYPTBYKEY(pc.PhysicalAddress) AS VARCHAR(50)) AS PhysicalAddress, 
CAST(DECRYPTBYKEY(pc.MobileNumber) AS VARCHAR(50)) AS MobileNumber, 
               CAST(DECRYPTBYKEY(pc.AlternativeNumber) AS VARCHAR(50)) AS AlternativeNumber
			   , CAST(DECRYPTBYKEY(pc.EmailAddress) AS VARCHAR(50)) AS EmailAddress,
			   pidd.IdentifierValue as EnrollmentNumber,
			   pin.IdentifierValue as PersonIdentificationNumber,
			   lit.ItemName as Gender,
			   p.DeleteFlag as  DeleteFlag
FROM  dbo.Person p left join dbo.PersonContact pc  on p.Id=  pc.PersonId  
left join dbo.PersonIdentifier pin on pin.PersonId=p.Id
left join LookupItemView lit on lit.ItemId=p.Sex
left join dbo.Patient pt on pt.PersonId= p.Id
left join dbo.PatientIdentifier pidd on pidd.PatientId=pt.Id
where  (pc.DeleteFlag is null or pc.DeleteFlag=0)





GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonIdentifierView]'))
DROP VIEW [dbo].[PersonIdentifierView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PersonIdentifierView]
AS
SELECT        p.Id,p.Id as PersonId, CAST(DECRYPTBYKEY(p.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(p.MidName) AS VARCHAR(50)) AS MiddleName, CAST(DECRYPTBYKEY(p.LastName) AS VARCHAR(50)) AS LastName, 
                         p.Sex, p.Active, p.DeleteFlag, p.CreateDate, p.CreatedBy, p.AuditData, p.DateOfBirth, p.DobPrecision,lpidt.PersonIdentifierType,lpidt.PersonIdentifier,lpidt.PersonIdentifierValue,
						 liv.PatientIdentifier,liv.PatientIdentifierType,liv.PatientIdentifierValue
FROM        dbo.Person p
left  join dbo.PersonIdentifier pid on pid.PersonId=p.Id
left join  (select pid.PersonId,idf.Id as PatientIdentifier,idf.Name as PatientIdentifierType,pid.IdentifierValue as PatientIdentifierValue from dbo.Identifiers idf
inner join  IdentifierType idt on idt.Id=idf.IdentifierType
inner join PersonIdentifier pid on  pid.IdentifierId=idf.Id
where idt.Name ='Patient' ) liv on liv.PatientIdentifier=pid.IdentifierId and liv.PersonId=pid.PersonId
left join (select pid.PersonId,idf.id as PersonIdentifier,idf.Name as PersonIdentifierType,pid.IdentifierValue as PersonIdentifierValue from dbo.Identifiers idf
inner join  IdentifierType idt on idt.Id=idf.IdentifierType
inner join PersonIdentifier pid on  pid.IdentifierId=idf.Id
where idt.Name ='Person') lpidt on lpidt.PersonIdentifier=pid.IdentifierId and lpidt.PersonId=pid.PersonId



go


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonEmergencyView]'))
DROP VIEW [dbo].[PersonEmergencyView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[PersonEmergencyView]
AS
select Cast(ROW_NUMBER() OVER(order by t.PersonId) as Int) as Id,t.PersonId,t.EmergencyContactPersonId,t.EmergencyFirstName,t.EmergencyLastName,t.EmergencyMidName,t.MobileContact,
t.RelationshipTypeId,t.Sex,t.Gender,t.RegisteredToClinic,t.DeleteFlag,t.CreatedBy
,t.ConsentType,t.ConsentValue,t.ConsentReason,t.NextofkinItemId,t.NextofkinItemName,t.EmergencyItemId,t.EmergencyItemName,t.CreateDate from(select 
distinct pme.PersonId,pme.EmergencyContactPersonId 
,ROW_NUMBER() OVER(partition by pme.PersonId,pme.EmergencyContactPersonId order by pme.PersonId) as rownum,
CAST(DECRYPTBYKEY(p.FirstName) as varchar(50)) as EmergencyFirstName ,
CAST(DECRYPTBYKEY(p.LastName) as varchar(50)) as EmergencyLastName,
CAST(DECRYPTBYKEY(p.MidName) as varchar(50)) as EmergencyMidName,
CAST(DECRYPTBYKEY(pme.MobileContact) as varchar(100)) as MobileContact,
plr.RelationshipTypeId,
p.Sex,lt.ItemName as Gender,pme.RegisteredToClinic,

pme.DeleteFlag,
pme.CreatedBy,

pcs.ConsentType,pcs.ConsentValue,pcs.ConsentReason
 , ltmNextofkin.ItemName as NextofkinItemName
  ,ltmNextofkin.ItemId as NextofkinItemId,
  ltmNextofkin.CreateDate,
  ltmEmergency.ItemName  as EmergencyItemName,
  ltmEmergency.ItemId as EmergencyItemId
from PersonEmergencyContact pme

inner join Person p on p.Id=pme.EmergencyContactPersonId
inner join LookupItemView lt on  lt.ItemId=p.Sex
left join PersonRelationship plr on
 plr.IndexPersonId=pme.PersonId and 
 plr.PersonId=pme.EmergencyContactPersonId
 left join PersonConsent pcs on pcs.PersonId=pme.PersonId
 and pcs.EmergencyContactId =pme.EmergencyContactPersonId
left join(
 select distinct pme.PersonId,pme.EmergencyContactPersonId,pme.ContactType,ltm.ItemId,pme.CreateDate,
 ltm.ItemName
   from PersonEmergencyContact pme
   inner join(
 select ItemId, ItemName from LookupItemView where MasterName='PersonContactType'
  and ItemName='NextofKin')ltm on ltm.ItemId=pme.ContactType
  )ltmNextofkin on ltmNextofkin.EmergencyContactPersonId=pme.EmergencyContactPersonId
  and ltmNextofkin.PersonId=pme.PersonId
 left join(
  select distinct pme.PersonId,pme.EmergencyContactPersonId,pme.ContactType,ltm.ItemId,ltm.ItemName,pme.CreateDate
   from PersonEmergencyContact pme
   inner join(
 select ItemId, ItemName from LookupItemView where MasterName='PersonContactType'
  and ItemName='Emergency')ltm on ltm.ItemId=pme.ContactType
  )ltmEmergency on ltmEmergency.PersonId=pme.PersonId 
  and ltmEmergency.EmergencyContactPersonId=pme.EmergencyContactPersonId

 
 ) t where t.rownum='1'




GO





GO




IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonIdentifierView]'))
DROP VIEW [dbo].[PersonIdentifierView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonIdentifierView]
AS
SELECT        p.Id,p.Id as PersonId, CAST(DECRYPTBYKEY(p.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(p.MidName) AS VARCHAR(50)) AS MiddleName, CAST(DECRYPTBYKEY(p.LastName) AS VARCHAR(50)) AS LastName, 
                         p.Sex, p.Active, p.DeleteFlag, p.CreateDate, p.CreatedBy, p.AuditData, p.DateOfBirth, p.DobPrecision,lpidt.PersonIdentifierType,lpidt.PersonIdentifier,lpidt.PersonIdentifierValue,pii.IdentifierValue as EnrollmentNumber,
						 pt.RegistrationDate as RegistrationDate,
						 liv.PatientIdentifier,liv.PatientIdentifierType,liv.PatientIdentifierValue,
						 pt.Id as Patientid
FROM        dbo.Person p
left  join dbo.PersonIdentifier pid on pid.PersonId=p.Id
left join  (select pid.PersonId,idf.Id as PatientIdentifier,idf.Name as PatientIdentifierType,pid.IdentifierValue as PatientIdentifierValue from dbo.Identifiers idf
inner join  IdentifierType idt on idt.Id=idf.IdentifierType
inner join PersonIdentifier pid on  pid.IdentifierId=idf.Id
where idt.Name ='Patient' ) liv on liv.PatientIdentifier=pid.IdentifierId and liv.PersonId=pid.PersonId
left join (select pid.PersonId,idf.id as PersonIdentifier,idf.Name as PersonIdentifierType,pid.IdentifierValue as PersonIdentifierValue from dbo.Identifiers idf
inner join  IdentifierType idt on idt.Id=idf.IdentifierType
inner join PersonIdentifier pid on  pid.IdentifierId=idf.Id
where idt.Name ='Person') lpidt on lpidt.PersonIdentifier=pid.IdentifierId and lpidt.PersonId=pid.PersonId
left join  Patient pt on pt.PersonId=p.Id
left join  PatientIdentifier pii on pii.PatientId=pt.Id



go



IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonListView]'))
DROP VIEW [dbo].[PersonListView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[PersonListView]
AS
SELECT   p.Id,p.Id as PersonId,p.DeleteFlag, CAST(DECRYPTBYKEY(p.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(p.MidName) AS VARCHAR(50)) AS MiddleName,
 CAST(DECRYPTBYKEY(p.LastName) AS VARCHAR(50)) AS LastName, CAST(DECRYPTBYKEY(pt.NationalId) as varchar(50)) as NationalId,
p.Sex, CASE WHEN p.DateOfBirth is null then pt.DateOfBirth else p.DateOfBirth end as DateOfBirth,lpidt.PersonIdentifierType,lpidt.PersonIdentifier
,lpidt.PersonIdentifierValue,pii.IdentifierValue as EnrollmentNumber,
pt.RegistrationDate as RegistrationDate,
--liv.PatientIdentifier,liv.PatientIdentifierType,liv.PatientIdentifierValue,
						 pt.Id as Patientid
						
FROM        dbo.Person p
left  join dbo.PersonIdentifier pid on pid.PersonId=p.Id
left join  (select pid.PersonId,idf.Id as PatientIdentifier,idf.Name as PatientIdentifierType,pid.IdentifierValue as PatientIdentifierValue from dbo.Identifiers idf
inner join  IdentifierType idt on idt.Id=idf.IdentifierType
inner join PersonIdentifier pid on  pid.IdentifierId=idf.Id
where idt.Name ='Patient' ) liv on liv.PatientIdentifier=pid.IdentifierId and liv.PersonId=pid.PersonId
left join (select pid.PersonId,idf.id as PersonIdentifier,idf.Name as PersonIdentifierType,pid.IdentifierValue as PersonIdentifierValue from dbo.Identifiers idf
inner join  IdentifierType idt on idt.Id=idf.IdentifierType
inner join PersonIdentifier pid on  pid.IdentifierId=idf.Id
where idt.Name ='Person') lpidt on lpidt.PersonIdentifier=pid.IdentifierId and lpidt.PersonId=pid.PersonId
left join  Patient pt on pt.PersonId=p.Id 
left join  PatientIdentifier pii on pii.PatientId=pt.Id





go

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonView]'))
DROP VIEW [dbo].[PersonView]


/****** Object:  View [dbo].[PersonView]    Script Date: 6/13/2018 11:40:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[PersonView]
AS
SELECT        p.Id, CAST(DECRYPTBYKEY(p.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(p.MidName) AS VARCHAR(50)) AS MiddleName, CAST(DECRYPTBYKEY(p.LastName) AS VARCHAR(50)) AS LastName, 
                         p.Sex, p.Active, p.DeleteFlag, p.CreateDate, p.CreatedBy, p.AuditData, p.DateOfBirth, p.DobPrecision,pt.RegistrationDate
FROM            dbo.Person p
left join dbo.Patient  pt on pt.PersonId=p.Id


GO






IF EXISTS(select * FROM sys.views where name = 'CountyView')
DROP VIEW [dbo].[CountyView]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[CountyView]
AS

select distinct CountyId,CountyName from County


go




IF EXISTS(select * FROM sys.views where name = 'SubCountyView')
DROP VIEW [dbo].[SubCountyView]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[SubCountyView]
AS

select distinct CountyId,SubCountyId,SubCountyName from County 

go







IF EXISTS(select * FROM sys.views where name = 'WardView')
DROP VIEW [dbo].[WardView]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[WardView]
AS
select distinct SubCountyId,WardId,WardName from County


go













