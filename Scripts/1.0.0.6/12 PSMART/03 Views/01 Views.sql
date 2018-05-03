

-- PSMART VIEWS
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_CardDetails]'))
DROP VIEW [dbo].[PSmart_CardDetails]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_ClientEligibleList]'))
DROP VIEW [dbo].[PSmart_ClientEligibleList]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSMart_ExternalPatientId]'))
DROP VIEW [dbo].[PSMart_ExternalPatientId]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Psmart_HTSList]'))
DROP VIEW [dbo].[Psmart_HTSList]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_Immunization]'))
DROP VIEW [dbo].[PSmart_Immunization]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_InternalPatientId]'))
DROP VIEW [dbo].[vw_PersonGodsNumber]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_MotherDetails]'))
DROP VIEW [dbo].[PSmart_MotherDetails]

GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_MotherIdentifier]'))
DROP VIEW [dbo].[PSmart_MotherIdentifier]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSMart_MotherName]'))
DROP VIEW [dbo].[PSMart_MotherName]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_NextOfKin]'))
DROP VIEW [dbo].[PSmart_NextOfKin]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_NokName]'))
DROP VIEW [dbo].[PSmart_NokName]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_PatientAddress]'))
DROP VIEW [dbo].[PSmart_PatientAddress]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_PatientIdentification]'))
DROP VIEW [dbo].[PSmart_PatientIdentification]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_PatientName]'))
DROP VIEW [dbo].[PSmart_PatientName]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_PhysicalAddress]'))
DROP VIEW [dbo].[PSmart_PhysicalAddress]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmart_ProviderDetails]'))
DROP VIEW [dbo].[PSmart_ProviderDetails]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmartAuthUser]'))
DROP VIEW [dbo].[PSmartAuthUser]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PSmartHIVTest]'))
DROP VIEW [dbo].[PSmartHIVTest]
GO

---- 


/****** Object:  View [dbo].[psmart_HTSList]    Script Date: 4/30/2018 3:37:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[psmart_HTSList]
AS
SELECT    
    DISTINCT 
	  h.PersonId,
	  P.Id PatientId,
	  PatientEncounterID,
	 CASE WHEN  (SELECT i.IdentifierValue from PersonIdentifier i WHERE i.PersonId=P.PersonId AND i.IdentifierId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  IS NULL THEN ''
	 ELSE 
		(SELECT i.IdentifierValue from PersonIdentifier i WHERE i.PersonId=P.PersonId AND i.IdentifierId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  
	 END [CardSerialNumber],
	  CAST(DECRYPTBYKEY(ps.FirstName) AS VARCHAR(50)) FirstName,
	  CAST(DECRYPTBYKEY(ps.LastName) AS VARCHAR(50)) LastName,
	  CAST(DECRYPTBYKEY(ps.MidName) AS VARCHAR(50)) MidName,
	  P.DateOfBirth,
	 CASE WHEN
	   (SELECT top 1 Name FROM LookupItem WHERE id= ps.Sex) IS NULL THEN ''
	 ELSE
	  (SELECT top 1 Name FROM LookupItem WHERE id= ps.Sex) 
	 END Sex ,
	  CAST(DATEDIFF(DD,P.DateOfBirth,GETDATE())/365.25 as INT) [AGE],
	  p.DobPrecision,
	  CAST(DECRYPTBYKEY(C.MobileNumber) AS VARCHAR(50)) MobileNumber,
	 CAST(DECRYPTBYKEY(C.PhysicalAddress) AS VARCHAR(50))  PhysicalAddress,
	CASE WHEN  L.Village IS NULL THEN ''
	ELSE
		 L.Village
	 END Village,
	CASE WHEN  
	  (SELECT top 1 WardName FROM county WHERE WardId= L.Ward) IS NULL THEN ''
	ELSE
	 (SELECT top 1 WardName FROM county WHERE WardId= L.Ward) 
	END  Ward,
	CASE WHEN 
	  (SELECT top 1 Subcountyname FROM county WHERE SubcountyId= L.SubCounty) IS NULL THEN ''
	  ELSE
	    (SELECT top 1 Subcountyname FROM county WHERE SubcountyId= L.SubCounty)
	  END  SubCounty,
	CASE WHEN
	  (SELECT top 1 CountyName FROM county WHERE CountyId= L.County) IS NULL THEN ''
	ELSE
	(SELECT top 1 CountyName FROM county WHERE CountyId= L.County)
	END  County,
	CASE WHEN
	  L.LandMark IS NULL THEN ''
	ELSE
	  L.LandMark 
	 END LANDMARK,
	 CASE WHEN  L.NearestHealthCentre IS NULL THEN ''
	 ELSE
		 L.NearestHealthCentre 
	 END [NEARESTLANDMARK]
 
FROM   
         dbo.HtsEncounter h
INNER JOIN 
Person ps
ON
ps.Id= h.PersonId
INNER JOIN 
Patient P
ON
P.PersonId=ps.Id
LEFT JOIN 
PersonContact C
ON
C.PersonId=h.PersonId
LEFT JOIN
PersonLocation L
ON
L.PersonId=h.PersonId

WHERE CAST(DATEDIFF(DD,P.DateOfBirth,GETDATE())/365.25 as INT) < 15
GO

------------ 
CREATE VIEW [dbo].[PSmart_CardDetails]
AS
SELECT       
	L.PersonId [PersonId],
	L.PatientId PatientId,
	L.CardSerialNumber CardSerialNumber,
	'ACTIVE' [STATUS],
	'' [REASON],
	'' [LAST_UPDATED],
	(SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [LAST_UPDATED_FACILITY]

FROM psmart_HTSList L
GO

-----------

CREATE VIEW [dbo].[PSmart_ClientEligibleList]
AS
SELECT
DISTINCT
	e.PatientId PatientId
   ,e.FirstName [FIRSTNAME]
   ,e.MidName [MIDDLENAME]
   ,e.LastName[LASTNAME]
   ,e.sex [GENDER],
	
   --,DATEDIFF(YEAR, ptn.DateOfBirth, GETDATE()) [AGE],
  e.[AGE]
-- DATEDIFF(hour,ptn.DateOfBirth,GETDATE())/8766.0
FROM psmart_HTSList e
WHERE
	e.CardSerialNumber IS NULL OR e.CardSerialNumber=''

--INNER JOIN
--Patient p
--ON
--p.Id=e.PatientId
--INNER JOIN 
--Person ps 
--ON
--ps.Id=p.Id
----INNER JOIN Patient ptn
----	ON ptn.ptn_pk = h.Ptn_pk
----INNER JOIN Person p
----	ON p.Id = ptn.Id WHERE CAST(DATEDIFF(DD,ptn.DateOfBirth,GETDATE())/365.25 as INT) <15

--WHERE
--	e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='Hts-encounter')
--AND
--	CAST(DATEDIFF(DD,ps.DateOfBirth,GETDATE())/365.25 as INT) <15
	
GO
----------------

CREATE VIEW [dbo].[PSmart_ExternalPatientId]
AS
SELECT  
      p.Id Ptn_pk,
	  p.Id [PatientId],
	  p.PersonId [PersonId],
	  CASE WHEN H.CardSerialNumber is null then '' -- (SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=P.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
	  ELSE
	   H.CardSerialNumber -- (SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=P.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
	  END [CardSerialNumber],
	  CASE WHEN (SELECT IdentifierValue FROM PatientIdentifier WHERE PatientId=P.Id AND IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE Code='GODS_NUMBER')) IS NULL THEN ''
	  ELSE
		(SELECT IdentifierValue FROM PatientIdentifier WHERE IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE Code='GODS_NUMBER'))
	  END
	   [ID],
	  'GODS_NUMBER' [IDENTIFIER_TYPE],
	  (SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY] ,
	  'MPI' [ASSIGNING_AUTHORITY]
FROM    
      dbo.psmart_HTSList H
INNER JOIN 
	 dbo.Patient P
ON
     p.Id=H.PatientId
WHERE

	H.AGE<15

GO

--- 

CREATE VIEW [dbo].[PSmart_Immunization]
AS


   SELECT 

		L.PersonId PersonId,
		L.PatientId PatientId,
		L.CardSerialNumber [CardSerialNumber],
		CASE WHEN i.[AntigenAdministered] IS NULL THEN ''
		ELSE
		 i.[AntigenAdministered]
		END  [NAME],
		CASE WHEN i.[DateAdministered] IS NULL THEN ''
		
		ELSE
		i.[DateAdministered]
		END  [DATE_ADMINISTERED]
   FROM 
     ImmunizationTracker i
	INNER JOIN 
	psmart_HTSList L
	ON
	L.PatientId=i.[PtnPk]

GO

------------ 

CREATE VIEW [dbo].[PSmart_InternalPatientId]
AS

		SELECT 
		L.PatientId PatientId,
		L.PersonId,
		CASE WHEN L.CardSerialNumber IS NULL THEN '' --(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
		  L.CardSerialNumber	-- (SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
		END [CardSerialNumber],
		'HTS_NUMBER' [IDENTIFIER_TYPE],
	    'HTS' [ASSIGNING_AUTHORITY],
	    (SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
			CASE WHEN (SELECT top 1 i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='HTSNumber')) IS NULL THEN ''
		ELSE
			(SELECT top 1 i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='HTSNumber'))
		END [ID]
	FROM psmart_HTSList L


	UNION

	SELECT 
		L.PatientId PatientId,
		L.PersonId,
		CASE WHEN L.CardSerialNumber IS NULL THEN '' -- (SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
		  L.CardSerialNumber	--(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
		END [CardSerialNumber],
		'CARD_SERIAL_NUMBER' [IDENTIFIER_TYPE],
	    'CARD_REGISTRY' [ASSIGNING_AUTHORITY],
	    (SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
				CASE WHEN L.CardSerialNumber IS NULL THEN '' --(SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
			L.CardSerialNumber-- (SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
		END [ID]
	FROM psmart_HTSList L

	--	SELECT 
	--		DISTINCT
	
	--p.Ptn_Pk PatientId,
	--p.Ptn_Pk PersonId,
	--p.CardSerialNumber [CardSerialNumber],
	--'CARD_SERIAL_NUMBER' [IDENTIFIER_TYPE],
	--'CARD_REGISTRY' [ASSIGNING_AUTHORITY],
	--(SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
	--p.CardSerialNumber [ID]
	--FROM 

	--[dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
	--INNER JOIN 
	--mst_Patient p
	--ON
	--p.Ptn_Pk=h.Ptn_pk
	
	--UNION 
	--SELECT 
	--	DISTINCT
	
	--p.Ptn_Pk PatientId,
	--p.Ptn_Pk PersonId,
	--p.CardSerialNumber [CardSerialNumber],
	--'HEI_NUMBER' [IDENTIFIER_TYPE],
	--'MCH' [ASSIGNING_AUTHORITY],
	--(SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
	--p.HEIIDNumber [ID]
	--FROM 

	--[dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
	--INNER JOIN 
	--mst_Patient p
	--ON
	--p.Ptn_Pk=h.Ptn_pk WHERE p.HEIIDNumber IS NOT NULL

	UNION
SELECT 
		L.PatientId,
		L.PersonId,
		CASE WHEN L.CardSerialNumber IS NULL THEN '' --(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
			L.CardSerialNumber --(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
		END [CardSerialNumber],
		'CCC_NUMBER' [IDENTIFIER_TYPE],
	    'CCC' [ASSIGNING_AUTHORITY],
	    (SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
		CASE WHEN L.CardSerialNumber IS NULL THEN '' -- (SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CCCNumber')) IS NULL THEN ''
		ELSE
			L.CardSerialNumber ---	(SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CCCNumber'))
		END [ID]
	FROM psmart_HTSList L WHERE (SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CCCNumber')) IS NOT NULL

GO


--- 



CREATE VIEW [dbo].[psmart_MotherDetails]
AS

SELECT 
 
  L.PersonId PersonId,
  L.PatientId,
  L.CardSerialNumber,
	
	CASE WHEN (CAST(DECRYPTBYKEY((SELECT p.FirstName FROM Person p WHERE p.Id IN(SELECT r.PersonId FROM PersonRelationship r WHERE r.RelationshipTypeId IN(SELECT v.id FROM LookupItem v WHERE v.Name='mother') AND r.PatientId=L.PatientId))) AS VARCHAR(50))) IS NULL THEN ''
	ELSE
	 (CAST(DECRYPTBYKEY((SELECT p.FirstName FROM Person p WHERE p.Id IN(SELECT r.PersonId FROM PersonRelationship r WHERE r.RelationshipTypeId IN(SELECT v.id FROM LookupItem v WHERE v.Name='mother') AND r.PatientId=L.PatientId))) AS VARCHAR(50)))
	END [FIRST_NAME],
	CASE WHEN (CAST(DECRYPTBYKEY((SELECT p.MidName FROM Person p WHERE p.Id IN(SELECT r.PersonId FROM PersonRelationship r WHERE r.RelationshipTypeId IN(SELECT v.id FROM LookupItem v WHERE v.Name='mother') AND r.PatientId=L.PatientId))) AS VARCHAR(50))) IS NULL THEN ''
	ELSE
	 (CAST(DECRYPTBYKEY((SELECT p.MidName FROM Person p WHERE p.Id IN(SELECT r.PersonId FROM PersonRelationship r WHERE r.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='mother') AND r.PatientId=L.PatientId))) AS VARCHAR(50)))
	END [MIDDLE_NAME],
	CASE WHEN (CAST(DECRYPTBYKEY((SELECT p.LastName FROM Person p WHERE p.Id IN(SELECT r.PersonId FROM PersonRelationship r WHERE r.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='mother') AND r.PatientId=L.PatientId))) AS VARCHAR(50))) IS NULL THEN ''
	ELSE
	 (CAST(DECRYPTBYKEY((SELECT p.LastName FROM Person p WHERE p.Id IN(SELECT r.PersonId FROM PersonRelationship r WHERE r.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='mother') AND r.PatientId=L.PatientId))) AS VARCHAR(50)))
	END [LAST_NAME]
	--isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
	--isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
FROM 
psmart_HTSList L

--LEFT JOIN 
--PersonRelationship R
--ON
--R.PatientId=L.PatientId
--WHERE R.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='Mother')


--SELECT       
--	ptn.Ptn_Pk [PersonId],
--	ptn.CardSerialNumber [CardSerialNumber],
--	isnull(CAST(DECRYPTBYKEY(f.RFirstName ) AS VARCHAR(50)),'') [FIRST_NAME],
--	isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
--	isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
--FROM [dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
--left JOIN 
--mst_Patient ptn
--ON
--ptn.Ptn_Pk=h.Ptn_pk
--LEFT JOIN dtl_FamilyInfo f
--ON
--f.Ptn_pk=h.Ptn_pk
--WHERE f.Sex=17 AND f.RelationshipType=10

--UNION

--SELECT       
--	ptn.Ptn_Pk [PersonId],
--	ptn.CardSerialNumber [CardSerialNumber],
--	isnull(CAST(DECRYPTBYKEY(f.RFirstName ) AS VARCHAR(50)),'')[FIRST_NAME],
--	isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'')MIDDLE_NAME,
--	isnull(CAST(DECRYPTBYKEY(f.RLastName) AS VARCHAR(50)),'') LAST_NAME
--FROM [dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
--left JOIN 
--mst_Patient ptn
--ON
--ptn.Ptn_Pk=h.Ptn_pk
--LEFT JOIN dtl_FamilyInfo f
--ON
--f.Ptn_pk=h.Ptn_pk

GO

------------


CREATE VIEW [dbo].[PSmart_MotherIdentifier]
AS

SELECT 
 
  L.PersonId PersonId,
  L.CardSerialNumber,
	
	--CASE WHEN (SELECT IdentifierValue FROM PatientIdentifier i WHERE i.PatientId=(SELECT id FROM patient p WHERE p.PersonId=L.PersonId) AND i.IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE code='CCCNumber')) IS NULL THEN '' 
	--ELSE
	--	(SELECT IdentifierValue FROM PatientIdentifier i WHERE i.PatientId=(SELECT id FROM patient p WHERE p.PersonId=L.PersonId) AND i.IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE code='CCCNumber'))
	--END [ID],
	I.IdentifierValue [ID],
	-- 'CCC_NUMBER' IDENTIFIER_TYPE,
	'' IDENTIFIER_TYPE,
	--'CCC'  ASSIGNING_AUTHORITY,
	'' ASSIGNING_AUTHORITY,
	'' ASSIGNING_FACILITY -- (SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) ASSIGNING_FACILITY
FROM 
PersonRelationship R
INNER JOIN 

psmart_HTSList L
ON
L.PersonId=R.PersonId
INNER JOIN 
PatientIdentifier I
On
I.PatientId=L.PatientId
WHERE I.IdentifierTypeId IN(SELECT Id FROM Identifiers i WHERE i.Code='CCCNumber')
--INNER JOIN 
--Patient P
--ON
--P.id=L.PatientId

--LEFT JOIN 
--PersonRelationship R
--ON
--R.PatientId=L.PatientId
--WHERE R.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='Mother')


--SELECT       
--	ptn.Ptn_Pk [PersonId],
--	ptn.CardSerialNumber [CardSerialNumber],
--	isnull(CAST(DECRYPTBYKEY(f.RFirstName ) AS VARCHAR(50)),'') [FIRST_NAME],
--	isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
--	isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
--FROM [dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
--left JOIN 
--mst_Patient ptn
--ON
--ptn.Ptn_Pk=h.Ptn_pk
--LEFT JOIN dtl_FamilyInfo f
--ON
--f.Ptn_pk=h.Ptn_pk
--WHERE f.Sex=17 AND f.RelationshipType=10

--UNION

--SELECT       
--	ptn.Ptn_Pk [PersonId],
--	ptn.CardSerialNumber [CardSerialNumber],
--	isnull(CAST(DECRYPTBYKEY(f.RFirstName ) AS VARCHAR(50)),'')[FIRST_NAME],
--	isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'')MIDDLE_NAME,
--	isnull(CAST(DECRYPTBYKEY(f.RLastName) AS VARCHAR(50)),'') LAST_NAME
--FROM [dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
--left JOIN 
--mst_Patient ptn
--ON
--ptn.Ptn_Pk=h.Ptn_pk
--LEFT JOIN dtl_FamilyInfo f
--ON
--f.Ptn_pk=h.Ptn_pk

GO




-------------- 


CREATE VIEW [dbo].[PSmart_MotherName]
AS

SELECT 
 
  p.Id PersonId,
  L.CardSerialNumber,
	
CASE WHEN (CAST(DECRYPTBYKEY((SELECT FirstName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) IS NULL THEN '' 
	ELSE
	(CAST(DECRYPTBYKEY((SELECT FirstName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) END  [FIRST_NAME],
	CASE WHEN (CAST(DECRYPTBYKEY((SELECT MidName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) IS NULL THEN ''
	ELSE (CAST(DECRYPTBYKEY((SELECT MidName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) END [MIDDLE_NAME],
	CASE WHEN (CAST(DECRYPTBYKEY((SELECT LastName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) IS NULL THEN ''
	ELSE 
	(CAST(DECRYPTBYKEY((SELECT LastName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) END
	 [LAST_NAME]
	--isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
	--isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
FROM 
psmart_HTSList L
INNER JOIN 
Patient P
ON
P.id=L.PatientId

LEFT JOIN 
PersonRelationship R
ON
R.PatientId=L.PatientId
--WHERE R.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='Mother')


--SELECT       
--	ptn.Ptn_Pk [PersonId],
--	ptn.CardSerialNumber [CardSerialNumber],
--	isnull(CAST(DECRYPTBYKEY(f.RFirstName ) AS VARCHAR(50)),'') [FIRST_NAME],
--	isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
--	isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
--FROM [dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
--left JOIN 
--mst_Patient ptn
--ON
--ptn.Ptn_Pk=h.Ptn_pk
--LEFT JOIN dtl_FamilyInfo f
--ON
--f.Ptn_pk=h.Ptn_pk
--WHERE f.Sex=17 AND f.RelationshipType=10

--UNION

--SELECT       
--	ptn.Ptn_Pk [PersonId],
--	ptn.CardSerialNumber [CardSerialNumber],
--	isnull(CAST(DECRYPTBYKEY(f.RFirstName ) AS VARCHAR(50)),'')[FIRST_NAME],
--	isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'')MIDDLE_NAME,
--	isnull(CAST(DECRYPTBYKEY(f.RLastName) AS VARCHAR(50)),'') LAST_NAME
--FROM [dbo].[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] h
--left JOIN 
--mst_Patient ptn
--ON
--ptn.Ptn_Pk=h.Ptn_pk
--LEFT JOIN dtl_FamilyInfo f
--ON
--f.Ptn_pk=h.Ptn_pk


GO

------------- 
CREATE VIEW [dbo].[PSmart_NextOfKin]
AS
SELECT       
	L.PatientId [PatientId],
	L.PersonId,
	'' ID,
	L.CardSerialNumber [CardSerialNumber],
	'' RELATIONSHIP,
	'' [ADDRESS],
	'' [PHONE_NUMBER],
	'' SEX,
	'' DATE_OF_BIRTH,
	'' CONTACT_ROLE
FROM  psmart_HTSList L


GO

----------

CREATE VIEW [dbo].[psmart_NokName]
AS
SELECT DISTINCT      
	L.PatientId [PersonId],
	L.CardSerialNumber [CardSerialNumber],
	'' ID,
 '' as FIRST_NAME,
	'' MIDDLE_NAME,
	''LAST_NAME
FROM psmart_HTSList L

GO

------ 

CREATE VIEW [dbo].[PSmart_PatientAddress]
AS
SELECT
	L.PersonId PersonId
   ,L.PatientId PatientId
   ,L.CardSerialNumber [CardSerialNumber]
   ,isnull(CAST(DECRYPTBYKEY(C.PhysicalAddress) AS VARCHAR(50)),'') AS POSTAL_ADDRESS
FROM 
 psmart_HTSList L
 LEFT JOIN 
 PatientContact C
 ON
 C.PersonId=L.PersonId


GO

-----------


CREATE VIEW [dbo].[PSmart_PatientIdentification]
AS
SELECT DISTINCT
	L.PersonId
   ,L.PatientId
   , CASE WHEN L.CardSerialNumber IS NULL THEN ''  --(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
	 ELSE 
	  L.CardSerialNumber	--(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
	 END [CardSerialNumber]
   ,case when
		 format(cast(L.DateOfBirth  as date),'yyyyMMdd') is null THEN ''
    ELSE
		format(cast(L.DateOfBirth  as date),'yyyyMMdd') 
	END  DATE_OF_BIRTH 
   ,case L.DobPrecision		
			when '0' then 'Estimated'
			when '1' then 'Exact'
			else ''	
		END
		AS DATE_OF_BIRTH_PRECISION
   ,
   L.Sex SEX
 --  ,CASE WHEN (SELECT DateOfDeath FROM PatientCareending WHERE patientId=L.PatientId AND  DateOfDeath IS NOT NULL AND PatientId=L.PatientId) IS NULL THEN ''
 --   ELSE
	--	(SELECT DateOfDeath	FROM PatientCareending WHERE patientId=L.PatientId AND  DateOfDeath IS NOT NULL AND PatientId=L.PatientId) 
	--END AS DEATH_DATE --format(cast(ptn.DateOfDeath   as date),'yyyyMMdd') AS DEATH_DATE
	,'' DEATH_DATE
   ,'' AS DEATH_INDICATOR
   ,CASE WHEN CAST(DECRYPTBYKEY(C.mobileNo) AS VARCHAR(50)) IS NULL THEN '' 
    ELSE 
	 CAST(DECRYPTBYKEY(C.mobileNo) AS VARCHAR(50))
	END PHONE_NUMBER
   ,CASE WHEN (SELECT Name FROM LookupItem WHERE id= m.MaritalStatusId) IS NULL THEN ''
   ELSE
    (SELECT Name FROM LookupItem WHERE id= m.MaritalStatusId)
   END MARITAL_STATUS
FROM psmart_HTSList L

LEFT JOIN 
	PatientContact C
ON
	c.PersonId=L.PersonId
LEFT JOIN
	PatientMaritalStatus m
ON
	m.PersonId=L.PersonId

--INNER JOIN Person AS p
--	ON p.Id = ptn.PersonId
--LEFT OUTER JOIN PatientCareending AS pe
--	ON pe.PatientId = ptn.Id
--LEFT OUTER JOIN PatientMaritalStatus AS m
--	ON m.PersonId = p.Id
--LEFT OUTER JOIN PersonContact AS c
--	ON c.PersonId = p.Id


GO
---  ----

CREATE VIEW [dbo].[PSmart_PatientName]
AS
SELECT DISTINCT
	L.PersonId PersonId
   ,L.PatientId PatientId,
    CASE WHEN L.CardSerialNumber IS NULL THEN ''  --(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
	 ELSE 
		L.CardSerialNumber --(SELECT IdentifierValue i FROM PersonIdentifier i WHERE i.PersonId=L.PersonId AND i.IdentifierId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
	 END [CardSerialNumber]
   ,L.FirstName FIRST_NAME
   ,L.MidName MIDDLE_NAME
   ,L.LastName LAST_NAME
FROM psmart_HTSList L
  

GO
----- 
CREATE VIEW [dbo].[psmart_PhysicalAddress]
AS
SELECT  
    DISTINCT     
	L.PersonId,
	L.PatientId
	,CASE WHEN  (SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  IS NULL THEN ''
	 ELSE 
		(SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) 
	 END [CardSerialNumber],
	 L.Village  [VILLAGE],
	L.Ward	[WARD],
	L.SubCounty [SUB_COUNTY],
	L.County [COUNTY],
	L.LandMark [NEAREST_LANDMARK]
FROM    
 psmart_HTSList L    

GO

----------- 


CREATE VIEW [dbo].[psmart_ProviderDetails]
AS
SELECT  
    DISTINCT     
	  L.PersonId PersonId,
	 L.PatientId PatientId, 
	 L.CardSerialNumber  [CardSerialNumber],    
      (SELECT UserFirstName +' '+ UserLastName FROM mst_User WHERE UserID=h.ProviderId) [NAME],
      convert(varchar, h.ProviderId) ID
FROM   
      psmart_HTSList L
INNER JOIN 
HtsEncounter h
ON
L.PersonId=h.PersonId

GO

-------   
CREATE VIEW [dbo].[PSmartAuthUser]
AS
SELECT        
	UserID as userId,
	UserName,
	UserFirstName +' '+ UserLastName [DisplayName],
	[Password],
	(SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) FACILITY
FROM 
	dbo.mst_User where DeleteFlag = 0


GO

------- 
CREATE VIEW [dbo].[PsmartEligibleList]
AS
SELECT
DISTINCT
	e.PatientId PatientId
   ,e.FirstName [FIRSTNAME]
   ,e.MidName [MIDDLENAME]
   ,e.LastName[LASTNAME]
   ,e.sex [GENDER],
	
   --,DATEDIFF(YEAR, ptn.DateOfBirth, GETDATE()) [AGE],
  e.[AGE]
-- DATEDIFF(hour,ptn.DateOfBirth,GETDATE())/8766.0
FROM psmart_HTSList e
WHERE
	e.CardSerialNumber IS NULL OR e.CardSerialNumber=''

--INNER JOIN
--Patient p
--ON
--p.Id=e.PatientId
--INNER JOIN 
--Person ps 
--ON
--ps.Id=p.Id
----INNER JOIN Patient ptn
----	ON ptn.ptn_pk = h.Ptn_pk
----INNER JOIN Person p
----	ON p.Id = ptn.Id WHERE CAST(DATEDIFF(DD,ptn.DateOfBirth,GETDATE())/365.25 as INT) <15

--WHERE
--	e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='Hts-encounter')
--AND
--	CAST(DATEDIFF(DD,ps.DateOfBirth,GETDATE())/365.25 as INT) <15
	
GO

-------- 




CREATE VIEW [dbo].[psmartHIVTest]
AS
SELECT  
    DISTINCT     
	  L.PersonId,
	  L.PatientId,
	  L.CardSerialNumber,
	   format(cast(E.CreateDate as date),'yyyyMMdd') [DATE],

      CASE WHEN  R.FinalResult IS NULL THEN ''
	   ELSE
	   ( SELECT top 1 rtrim(ltrim(UPPER(Name))) FROM LookupItem WHERE id= R.FinalResult )
	   END [RESULT],
	   CASE H.EncounterType
			when 1 then 'SCREENING'
			WHEN 2 THEN 'CONFIRMATORY'
			ELSE ''
	   END [TYPE],
      (SELECT top 1 PosID FROM mst_Facility WHERE DeleteFlag=0) FACILITY,
	  (SELECT Name FROM FacilityList WHERE MFLCode=(SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0)) [FACILITYNAME],

 --    case  (select top 1 Name from mst_ModDeCode where CodeID=396 AND ID= h.strategyHTS)
	    
	--	when 'HP' then 'Health facility patient'
	--	when 'NP' then 'Non-Patient'
	--	when 'VI' then 'Integrated VCT Sites'
	--	when 'VS' then 'Stand-alone VCT Sites'
	--	when 'HB' then 'Hom-based'
	--	when 'MO' then 'Mobile and Outreach'
	--	when 'Others' then 'Others'		
	--END [STRATEGY],    
	CASE WHEN H.TestingStrategy IS NULL THEN ''
	
	ELSE
	 (SELECT SUBSTRING(Name,0,CHARINDEX(':',Name,0)) FROM LookupItem WHERE id=H.TestingStrategy)
	END [STRATEGY],
      (SELECT UserFirstName +' '+ UserLastName FROM mst_User WHERE UserID=H.ProviderId) [NAME],
      convert(varchar(10), H.ProviderId) ID
FROM   
    psmart_HTSList L
INNER JOIN 
HtsEncounter H
ON
H.PersonId=L.PersonId
INNER JOIN HtsEncounterResult R
ON
R.HtsEncounterId=H.Id

INNER JOIN 
PatientEncounter E
ON
E.Id=H.PatientEncounterID



UNION ALL

SELECT 

	L.PersonId PersonId,
	L.PatientId PatientId,
	L.CardSerialNumber [CardSerialNumber],
	 format(cast(t.ResultDate as date),'yyyyMMdd') [DATE],
	rtrim(ltrim(UPPER(t.Result)))  [RESULT],
	t.ResultCategory [TYPE],
	t.MFLCode [FACILITY], 
	(SELECT Name FROM FacilityList WHERE MFLCode=t.MFLCode) [FACILITYNAME],
	t.Strategy [STRATEGY],
	t.ProviderName [NAME],
	convert(varchar(5), t.ProviderId) [ID]
FROM HIVTestTracker t
INNER JOIN 
psmart_HTSList L
ON
L.PatientId=t.Ptn_pk

GO