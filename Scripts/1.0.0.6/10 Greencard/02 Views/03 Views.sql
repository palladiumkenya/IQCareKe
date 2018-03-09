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
  o.Ptn_pk as ptnpk,
  (SELECT top 1 Id FROM Patient WHERE ptn_pk=o.ptn_pk) as PatientId,
  o.PatientMasterVisitId, i.IdentifierValue [ID],
	(SELECT top 1 f.NationalId FROM mst_Facility f WHERE f.DeleteFlag=0) [SENDING_FACILITY],
	ISNULL((SELECT gn.IdentifierValue FROM vw_PersonGodsNumber gn WHERE gn.PatientId=i.patientId),'') EXT_ID,
	'GODS_NUMBER' [EXT_IDENTIFIER_TYPE],
	 'MPI' [EXT_ASSIGNING_AUTHOURITY],

  'CCC_NUMBER' [IDENTIFIER_TYPE],
  'CCC' [ASSIGNING_AUTHORITY],
  (SELECT Name FROM mst_Decode WHERE ID=o.ProgID) INDICATION,
  '' TREATMENT_INSTRUCTION,
   CAST(DECRYPTBYKEY(p.NationalId) AS VARCHAR(50))  [ID2],
  'NATIONAL_ID' [IDENTIFIER_TYPE2],
  'GOK' [ASSIGNING_AUTHORITY2],
  o.ptn_pharmacy_pk,
  CAST(DECRYPTBYKEY(g.FirstName) AS VARCHAR(50))  [FIRST_NAME],
   CAST(DECRYPTBYKEY(g.MidName) AS VARCHAR(50))  [MIDDLE_NAME],
   CAST(DECRYPTBYKEY(g.LastName) AS VARCHAR(50))  [LAST_NAME],
  o.OrderedByDate [TRANSACTION_DATETIME],
  'CA' [ORDER_CONTROL],
  o.ptn_pharmacy_pk [NUMBER],
  'IQCARE' [ENTITY],
  o.OrderedByDate [PHARMACY_ORDER_DATE],
  'CA' [ORDER_STATUS],
  (SELECT CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) FROM mst_user u WHERE u.UserID=o.OrderedBy) [ORDERING_PHYSICIAN_FIRST_NAME],
  (SELECT CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) FROM mst_user u WHERE u.UserID=o.OrderedBy) [ORDERING_PHYSICIAN_LAST_NAME],
  d.PatientInstructions[NOTES],
  case 
     when d.Drug_Pk IN(SELECT Drug_Pk FROM lnk_DrugGeneric g WHERE g.GenericID IN(SELECT GenericID FROM lnk_DrugTypeGeneric t WHERE t.DrugTypeId=37)) THEN (SELECT ItemName FROM LookupItemView WHERE ItemId=a.RegimenId) --(SELECT g.GenericID FROM lnk_DrugGeneric g where g.Drug_pk= d.Drug_Pk AND g.GenericID IN(SELECT t.Drug_pk FROM lnk_DrugTypeGeneric t WHERE DrugTypeId=37)) THEN '' -- d.Drug_PK IN IN(SELECT top 1 mst_Drug m WHERE m.Drug_Pk IN()WHERE -- k.GenericId FROM lnk_DrugTypeGeneric k WHERE k.DrugTypeId=37) then (SELECT top 1 l.ItemName  FROM LookupItemView l WHERE ItemId=a.RegimenId)
	 ELSE	
	 (SELECT GenericName FROM mst_Generic m WHERE m.GenericID IN(SELECT g.GenericID FROM lnk_DrugGeneric g where g.Drug_pk= d.Drug_pk))
  END [DRUG_NAME],
 'NASCOP_CODES' [CODING_SYSTEM] ,
 (SELECT StrengthName FROM mst_Strength WHERE StrengthId IN(SELECT top 1 StrengthId FROM lnk_DrugStrength WHERE DrugId=d.Drug_Pk))[STRENGTH],
 d.SingleDose [DOSAGE],
 (SELECT top 1 f.Name FROM mst_Frequency f WHERE f.ID=d.FrequencyID)[FREQUENCY],
 d.Duration [DURATION],
 d.OrderedQuantity [QUANTITY_PRESCRIBED],
 o.PharmacyNotes [PRESCRIPTION_NOTES]
  FROM dtl_PatientPharmacyOrder d
	 INNER JOIN 
  ord_PatientPharmacyOrder o
  ON
  o.ptn_pharmacy_pk=d.ptn_pharmacy_pk
  INNER JOIN Patient p
  ON
  p.ptn_pk=o.Ptn_pk
  INNER JOIN Person g
  ON
  g.Id=p.PersonId
  LEFT JOIN 
  ARVTreatmentTracker a
  ON
  a.PatientMasterVisitId=o.PatientMasterVisitId AND a.PatientId=o.PatientId
  INNER JOIN 
  PatientIdentifier i
  ON
  i.PatientId=p.Id WHERE i.IdentifierTypeId=1 AND o.PatientMasterVisitId IS NOT NULL
  
GO