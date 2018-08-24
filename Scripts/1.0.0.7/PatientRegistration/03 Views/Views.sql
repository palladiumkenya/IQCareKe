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













