/****** Object:  View [dbo].[API_PatientVitalsView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[API_PatientVitalsView]'))
DROP VIEW [dbo].[API_PatientVitalsView]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[API_PatientVitalsView]
AS
SELECT        Id, ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) AS NationalId, DeleteFlag, CreatedBy, CreateDate, AuditData, 
                         RegistrationDate
FROM            dbo.Patient

GO


/****** Object:  View [dbo].[PregnancyOutcomeView]    Script Date: 1/25/2018 8:40:22 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PregnancyOutcomeView]'))
DROP VIEW [dbo].[PregnancyOutcomeView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PregnancyOutcomeView]
AS
SELECT        dbo.PregnancyIndicator.Id, dbo.PregnancyIndicator.PatientId, dbo.PregnancyIndicator.PatientMasterVisitId, dbo.PregnancyIndicator.LMP, dbo.PregnancyIndicator.EDD,
                             (SELECT        TOP (1) DisplayName
                               FROM            dbo.LookupItemView
                               WHERE        (ItemId = dbo.PregnancyIndicator.PregnancyStatusId) AND (MasterName = 'PregnancyStatus')) AS PregnancyStatus,
                             (SELECT        TOP (1) DisplayName
                               FROM            dbo.LookupItemView AS LookupItemView_1
                               WHERE        (ItemId = dbo.Pregnancy.Outcome)) AS Outcome
FROM            dbo.PregnancyIndicator INNER JOIN
                         dbo.Pregnancy ON dbo.PregnancyIndicator.PatientId = dbo.Pregnancy.PatientId AND dbo.PregnancyIndicator.PatientMasterVisitId = dbo.Pregnancy.PatientMasterVisitId

GO

/****** Object:  View [dbo].[PatientScreenigView]    Script Date: 01/25/2018 14:25:27 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientScreenigView]'))
DROP VIEW [dbo].[PatientScreenigView]
GO

/****** Object:  View [dbo].[PatientScreenigView]    Script Date: 01/25/2018 14:25:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientScreenigView]
AS
SELECT DISTINCT a.ptn_pk, a.Id AS PatientId, c.Id AS PatientMasterVisitId, c.VisitDate, TBScreening.TBStatus, NutritionScreening.NutritionStatus, CaCxScreening.CaCx
FROM            dbo.Patient AS a INNER JOIN
dbo.PatientScreening AS b ON a.Id = b.PatientId INNER JOIN
dbo.PatientMasterVisit AS c ON b.PatientMasterVisitId = c.Id LEFT OUTER JOIN
    (SELECT DISTINCT b.PatientId, c.VisitDate, CASE WHEN n.MasterName = 'TBStatus' THEN n.itemname END AS TBStatus
    FROM            dbo.PatientScreening AS b INNER JOIN
                                dbo.PatientMasterVisit AS c ON b.PatientMasterVisitId = c.Id INNER JOIN
                                dbo.LookupItemView AS n ON b.ScreeningValueId = n.ItemId
    WHERE        (n.MasterName IN ('TBStatus'))) AS TBScreening ON b.PatientId = TBScreening.PatientId AND c.VisitDate = TBScreening.VisitDate LEFT OUTER JOIN
    (SELECT DISTINCT b.PatientId, c.VisitDate, CASE WHEN n.MasterName = 'NutritionStatus' THEN n.itemname END AS NutritionStatus
    FROM            dbo.PatientScreening AS b INNER JOIN
                                dbo.PatientMasterVisit AS c ON b.PatientMasterVisitId = c.Id INNER JOIN
                                dbo.LookupItemView AS n ON b.ScreeningValueId = n.ItemId
    WHERE        (n.MasterName IN ('NutritionStatus'))) AS NutritionScreening ON b.PatientId = NutritionScreening.PatientId AND c.VisitDate = NutritionScreening.VisitDate LEFT OUTER JOIN
    (SELECT DISTINCT b.PatientId, c.VisitDate, CASE WHEN n.MasterName = 'CaCxScreening' THEN n.itemname END AS CaCx
    FROM            dbo.PatientScreening AS b INNER JOIN
                                dbo.PatientMasterVisit AS c ON b.PatientMasterVisitId = c.Id INNER JOIN
                                dbo.LookupItemView AS n ON b.ScreeningValueId = n.ItemId
    WHERE        (n.MasterName IN ('CaCxScreening'))) AS CaCxScreening ON b.PatientId = CaCxScreening.PatientId AND c.VisitDate = CaCxScreening.VisitDate LEFT OUTER JOIN
    (SELECT DISTINCT b.PatientId, c.VisitDate, CASE WHEN n.MasterName = 'STIScreening' THEN n.itemname END AS STIScreening
    FROM            dbo.PatientScreening AS b INNER JOIN
                                dbo.PatientMasterVisit AS c ON b.PatientMasterVisitId = c.Id INNER JOIN
                                dbo.LookupItemView AS n ON b.ScreeningValueId = n.ItemId
    WHERE        (n.MasterName IN ('STIScreening'))) AS STIScreening ON b.PatientId = STIScreening.PatientId AND c.VisitDate = STIScreening.VisitDate

GO




/****** Object:  View [dbo].[vw_PersonGodsNumber] ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_PersonGodsNumber]'))
DROP VIEW [dbo].[vw_PersonGodsNumber]
GO

/***** Object:  View [dbo].[vw_PersonGodsNumber]    Script Date: 3/8/2018 3:28:31 PM *****/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_PersonGodsNumber]
AS
SELECT        i.Id, i.Name, i.Code,i.DisplayName, i.DataType, i.PrefixType, i.SuffixType, p.PersonId,(SELECT Id FROM Patient WHERE PersonId=p.PersonId) PatientId, 
                         p.IdentifierId, p.IdentifierValue, p.DeleteFlag
FROM            dbo.Identifiers i INNER JOIN
                         dbo.PersonIdentifier p ON i.Id = p.IdentifierId
WHERE        (i.Name = 'GODS_NUMBER')

GO

/****** Object:  View [dbo].[API_DrugPrescription]    Script Date: 01/25/2018 14:25:27 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[API_DrugPrescription]'))
DROP VIEW [dbo].[API_DrugPrescription]
GO

/****** Object:  View [dbo].[API_DrugPrescription]    Script Date: 01/25/2018 14:25:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[API_DrugPrescription]
AS
SELECT
	o.Ptn_pk AS ptnpk
   ,(SELECT TOP 1 Id FROM Patient	WHERE ptn_pk = o.ptn_pk)	AS PatientId
   ,o.PatientMasterVisitId
   ,i.IdentifierValue [ID]
   ,(SELECT TOP 1	f.NationalId	FROM mst_Facility f	WHERE f.FacilityID=o.LocationID)	[SENDING_FACILITY]
   ,ISNULL((SELECT	Top 1 gn.IdentifierValue	FROM vw_PersonGodsNumber gn	WHERE gn.PatientId = i.patientId)	, '') EXT_ID
   ,'GODS_NUMBER' [EXT_IDENTIFIER_TYPE]
   ,'MPI' [EXT_ASSIGNING_AUTHOURITY]
   ,'CCC_NUMBER' [IDENTIFIER_TYPE]
   ,'CCC' [ASSIGNING_AUTHORITY]
   ,(SELECT top 1 Name	FROM mst_Decode	WHERE ID = o.ProgID)	INDICATION
   ,'' TREATMENT_INSTRUCTION
   ,CAST(DECRYPTBYKEY(p.NationalId) AS VARCHAR(50)) [ID2]
   ,'NATIONAL_ID' [IDENTIFIER_TYPE2]
   ,'GOK' [ASSIGNING_AUTHORITY2]
   ,o.ptn_pharmacy_pk
   ,CAST(DECRYPTBYKEY(g.FirstName) AS VARCHAR(50)) [FIRST_NAME]
   ,CAST(DECRYPTBYKEY(g.MidName) AS VARCHAR(50)) [MIDDLE_NAME]
   ,CAST(DECRYPTBYKEY(g.LastName) AS VARCHAR(50)) [LAST_NAME]
   ,o.OrderedByDate [TRANSACTION_DATETIME]
   ,'CA' [ORDER_CONTROL]
   ,o.ptn_pharmacy_pk [NUMBER]
   ,'IQCARE' [ENTITY]
   ,o.OrderedByDate [PHARMACY_ORDER_DATE]
   ,'CA' [ORDER_STATUS]
   ,(SELECT TOP 1	CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50))		FROM mst_user u		WHERE u.UserID = o.OrderedBy)	[ORDERING_PHYSICIAN_FIRST_NAME]
   ,(SELECT TOP 1 CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50))		FROM mst_user u		WHERE u.UserID = o.OrderedBy)	[ORDERING_PHYSICIAN_LAST_NAME]
   ,d.PatientInstructions [NOTES]
   ,CASE
		WHEN d.Drug_Pk IN (SELECT Drug_Pk	FROM lnk_DrugGeneric g WHERE g.GenericID IN (SELECT	GenericID	FROM lnk_DrugTypeGeneric t
					WHERE t.DrugTypeId = 37)) THEN (SELECT TOP 1 ItemName	FROM LookupItemView		WHERE ItemId = a.RegimenId) --(SELECT g.GenericID FROM lnk_DrugGeneric g where g.Drug_pk= d.Drug_Pk AND g.GenericID IN(SELECT t.Drug_pk FROM lnk_DrugTypeGeneric t WHERE DrugTypeId=37)) THEN '' -- d.Drug_PK IN IN(SELECT top 1 mst_Drug m WHERE m.Drug_Pk IN()WHERE -- k.GenericId FROM lnk_DrugTypeGeneric k WHERE k.DrugTypeId=37) then (SELECT top 1 l.ItemName  FROM LookupItemView l WHERE ItemId=a.RegimenId)
		ELSE (SELECT TOP 1	GenericName	FROM mst_Generic m	WHERE m.GenericID IN (SELECT g.GenericID FROM lnk_DrugGeneric g	WHERE g.Drug_pk = d.Drug_pk))
	END [DRUG_NAME]
   ,'NASCOP_CODES' [CODING_SYSTEM]
   ,(SELECT Top 1	StrengthName FROM mst_Strength	WHERE StrengthId IN (SELECT TOP 1 StrengthId	FROM lnk_DrugStrength WHERE DrugId = d.Drug_Pk))	[STRENGTH]
   ,d.SingleDose [DOSAGE]
   ,(SELECT TOP 1	f.Name	FROM mst_Frequency f	WHERE f.ID = d.FrequencyID) [FREQUENCY]
   ,d.Duration [DURATION]
   ,d.OrderedQuantity [QUANTITY_PRESCRIBED]
   ,o.PharmacyNotes [PRESCRIPTION_NOTES]
FROM dtl_PatientPharmacyOrder d
INNER JOIN ord_PatientPharmacyOrder o	ON o.ptn_pharmacy_pk = d.ptn_pharmacy_pk
INNER JOIN Patient p	ON p.ptn_pk = o.Ptn_pk
INNER JOIN Person g	ON g.Id = p.PersonId
LEFT JOIN ARVTreatmentTracker a	ON a.PatientMasterVisitId = o.PatientMasterVisitId	AND a.PatientId = o.PatientId
INNER JOIN PatientIdentifier i	ON i.PatientId = p.Id
WHERE i.IdentifierTypeId = 1
AND o.PatientMasterVisitId IS NOT NULL
  
GO

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

CREATE VIEW [dbo].[psmart_HTSList]
AS
SELECT    
    DISTINCT 
	  h.PersonId,
	  P.Id PatientId,
	  PatientEncounterID,
	 CASE WHEN  (SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=P.Id AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  IS NULL THEN ''
	 ELSE 
		(SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=P.Id AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) 
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
	  (SELECT top 1 Ward FROM county WHERE WardId= L.Ward) IS NULL THEN ''
	ELSE
	 (SELECT top 1 Ward FROM county WHERE WardId= L.Ward) 
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


CREATE VIEW [dbo].[PSmart_CardDetails]
AS
SELECT       
	L.PersonId [PersonId],
	L.PatientId PatientId,
	L.CardSerialNumber CardSerialNumber,
	'ACTIVE' [STATUS],
	'' [REASON],
	(SELECT CreateDate FROM PatientIdentifier WHERE IdentifierValue=''+L.CardSerialNumber+'') [LAST_UPDATED],
	(SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [LAST_UPDATED_FACILITY]

FROM psmart_HTSList L
go

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

CREATE VIEW [dbo].[PSmart_ExternalPatientId]
AS
SELECT  
      p.Id Ptn_pk,
	  p.Id [PatientId],
	  p.PersonId [PersonId],
	  CASE WHEN  (SELECT IdentifierValue FROM PatientIdentifier WHERE PatientId=P.Id AND IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
	  ELSE
	   (SELECT IdentifierValue FROM PatientIdentifier WHERE PatientId=P.Id AND IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) 
	  END [CardSerialNumber],
	  CASE WHEN (SELECT IdentifierValue FROM PatientIdentifier WHERE PatientId=P.Id AND IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE Code='GODS_NUMBER')) IS NULL THEN ''
	  ELSE
		(SELECT IdentifierValue FROM PatientIdentifier WHERE IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE Code='GODS_NUMBER'))
	  END
	   [ID],
	  'GODS_NUMBER' [IDENTIFIER_TYPE],
	  'MPI' [ASSIGNING_FACILITY]
FROM    
      dbo.psmart_HTSList H
INNER JOIN 
	 dbo.Patient P
ON
     p.Id=H.PatientId
WHERE

	H.AGE<15

GO

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


CREATE VIEW [dbo].[PSmart_InternalPatientId]
AS

		SELECT 
		L.PatientId PatientId,
		L.PersonId,
		CASE WHEN (SELECT top 1 i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
			(SELECT top 1  i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
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
		CASE WHEN (SELECT top 1 i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
			(SELECT top 1 i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
		END [CardSerialNumber],
		'CARD_SERIAL_NUMBER' [IDENTIFIER_TYPE],
	    'CARD_REGISTRY' [ASSIGNING_AUTHORITY],
	    (SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
				CASE WHEN (SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
			(SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
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
		CASE WHEN (SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) IS NULL THEN ''
		ELSE
			(SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))
		END [CardSerialNumber],
		'CCC_NUMBER' [IDENTIFIER_TYPE],
	    'CCC' [ASSIGNING_AUTHORITY],
	    (SELECT Top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) [ASSIGNING_FACILITY],
		CASE WHEN (SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CCCNumber')) IS NULL THEN ''
		ELSE
			(SELECT i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CCCNumber'))
		END [ID]
	FROM psmart_HTSList L


CREATE VIEW [dbo].[PSmart_MotherDetails]
AS

SELECT 
 
  p.Id PersonId,
  L.CardSerialNumber,
	
	(CAST(DECRYPTBYKEY((SELECT FirstName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) [FIRST_NAME],
	(CAST(DECRYPTBYKEY((SELECT MidName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) [MIDDLE_NAME],
	(CAST(DECRYPTBYKEY((SELECT LastName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) [LAST_NAME]
	--isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
	--isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
FROM 
psmart_HTSList L
INNER JOIN 
Patient P
ON
P.id=L.PatientId

INNER JOIN 
PersonRelationship R
ON
R.PatientId=L.PatientId
WHERE R.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='Mother')


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

CREATE VIEW [dbo].[PSmart_MotherIdentifier]
AS

SELECT 
 
  p.Id PersonId,
  L.CardSerialNumber,
	
	(SELECT IdentifierValue FROM PatientIdentifier i WHERE i.PatientId=(SELECT id FROM patient p WHERE p.PersonId=R.PersonId) AND i.IdentifierTypeId IN(SELECT Id FROM Identifiers WHERE code='CCCNumber'))[ID],
	'CCC_NUMBER' IDENTIFIER_TYPE,
	'CCC'  ASSIGNING_AUTHORITY,
	 (SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) ASSIGNING_FACILITY
FROM 
psmart_HTSList L
INNER JOIN 
Patient P
ON
P.id=L.PatientId

INNER JOIN 
PersonRelationship R
ON
R.PatientId=L.PatientId
WHERE R.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='Mother')


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


CREATE VIEW [dbo].[PSmart_MotherName]
AS

SELECT 
 
  p.Id PersonId,
  L.CardSerialNumber,
	
	(CAST(DECRYPTBYKEY((SELECT FirstName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) [FIRST_NAME],
	(CAST(DECRYPTBYKEY((SELECT MidName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) [MIDDLE_NAME],
	(CAST(DECRYPTBYKEY((SELECT LastName FROM Person WHERE Id=R.PersonId)) AS VARCHAR(50))) [LAST_NAME]
	--isnull(CAST(DECRYPTBYKEY(f.RMiddleName ) AS VARCHAR(50)),'') MIDDLE_NAME,
	--isnull(CAST(DECRYPTBYKEY( f.RLastName ) AS VARCHAR(50)),'')LAST_NAME
FROM 
psmart_HTSList L
INNER JOIN 
Patient P
ON
P.id=L.PatientId

INNER JOIN 
PersonRelationship R
ON
R.PatientId=L.PatientId
WHERE R.RelationshipTypeId IN(SELECT v.ItemId FROM LookupItemView v WHERE v.MasterName='Relationship' AND v.ItemName='Mother')


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

CREATE VIEW [dbo].[PSmart_NextOfKin]
AS
SELECT       
	L.PatientId [PatientId],
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


CREATE VIEW [dbo].[PSmart_PatientAddress]
AS
SELECT
	P.PersonId PersonId
   ,P.Id AS PatientId
   ,(SELECT i.IdentifierValue from PatientIdentifier i WHERE i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  [CardSerialNumber]
   ,isnull(CAST(DECRYPTBYKEY(C.PhysicalAddress) AS VARCHAR(50)),'') AS POSTAL_ADDRESS
FROM 
  PatientEncounter E
  INNER JOIN 
  Patient  P
  ON
  P.Id=E.PatientId
INNER JOIN
Person ps
ON
ps.Id=P.PersonId
INNER JOIN 
PersonContact C
ON
ps.Id=C.PersonId
WHERE
E.PatientId IN(SELECT top 1 E.PatientId FROM PatientEncounter e WHERE e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='HtsEncounter'))	


GO

CREATE VIEW [dbo].[PSmart_PatientIdentification]
AS
SELECT DISTINCT
	L.PersonId
   ,L.PatientId
   , CASE WHEN  (SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  IS NULL THEN ''
	 ELSE 
		(SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) 
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
   ,CASE WHEN (SELECT DateOfDeath	FROM PatientCareending WHERE DateOfDeath IS NOT NULL AND PatientId=L.PatientId) IS NULL THEN ''
    ELSE
		(SELECT DateOfDeath	FROM PatientCareending WHERE DateOfDeath IS NOT NULL AND PatientId=L.PatientId) 
	END AS DEATH_DATE --format(cast(ptn.DateOfDeath   as date),'yyyyMMdd') AS DEATH_DATE
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

INNER JOIN 
	PatientContact C
ON
	c.PersonId=L.PersonId
INNER JOIN
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

CREATE VIEW [dbo].[PSmart_PatientName]
AS
SELECT DISTINCT
	L.PersonId PersonId
   ,L.PatientId PatientId,
    CASE WHEN  (SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  IS NULL THEN ''
	 ELSE 
		(SELECT i.IdentifierValue from PatientIdentifier i WHERE i.PatientId=L.PatientId AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER')) 
	 END [CardSerialNumber]
   ,L.FirstName FIRST_NAME
   ,L.MidName MIDDLE_NAME
   ,L.LastName LAST_NAME
FROM psmart_HTSList L
  

GO

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
	L.SubCounty [SubCounty],
	L.County [COUNTY],
	L.LandMark [NEAREST_LANDMARK]
FROM    
 psmart_HTSList L    

GO

CREATE VIEW [dbo].[psmart_ProviderDetails]
AS
SELECT  
    DISTINCT     
	  P.PersonId PersonId,
	 P.PersonId PatientId, 
	 (SELECT i.IdentifierValue from PatientIdentifier i WHERE i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CARD_SERIAL_NUMBER'))  [CardSerialNumber],    
      (SELECT UserFirstName +' '+ UserLastName FROM mst_User WHERE UserID=h.ProviderId) [NAME],
      convert(varchar, h.ProviderId) ID
FROM   
      HtsEncounter h
INNER JOIN 
Patient P
ON
P.PersonId=h.PersonId

GO

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
	   ( SELECT top 1 Name FROM LookupItem WHERE id= R.FinalResult )
	   END [RESULT],
	   CASE H.EncounterType
			when 1 then 'SCREENING'
			WHEN 2 THEN 'CONFIRMATORY'
			ELSE ''
	   END [TYPE],
      (SELECT top 1 NationalId FROM mst_Facility WHERE DeleteFlag=0) FACILITY,
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
	 (SELECT Name FROM LookupItem WHERE id=H.TestingStrategy)
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
	t.Result [RESULT],
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






