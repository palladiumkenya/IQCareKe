/****** Object:  View [dbo].[gcPatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[gcPatientView]'))
DROP VIEW [dbo].[gcPatientView]
GO
/***** Object:  View [dbo].[gcPatientView]    Script Date: 7/25/2017 12:43:40 PM *****/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE VIEW [dbo].[gcPatientView]
AS
     SELECT DISTINCT 
                         pt.Id, pt.PersonId, pt.ptn_pk, pni.IdentifierValue AS EnrollmentNumber, pt.PatientIndex, CAST(DECRYPTBYKEY(pn.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(pn.MidName) AS VARCHAR(50)) 
                         AS MiddleName, CAST(DECRYPTBYKEY(pn.LastName) AS VARCHAR(50)) AS LastName, pn.Sex, pn.Active, pt.RegistrationDate, pe.EnrollmentDate AS [EnrollmentDate ], 
                         ISNULL(CAST((CASE pe.CareEnded WHEN 0 THEN 'Active' WHEN 1 THEN
                             (SELECT        TOP 1 ItemName
                               FROM            LookupItemView
                               WHERE        MasterName = 'CareEnded' AND ItemId = ptC.ExitReason) END) AS VARCHAR(50)),'Active') AS PatientStatus, ptC.ExitReason, pt.DateOfBirth, CAST(DECRYPTBYKEY(pt.NationalId) AS VARCHAR(50)) AS NationalId, 
                         pt.FacilityId, pt.PatientType, pe.TransferIn, CAST(DECRYPTBYKEY(pc.MobileNumber) AS VARCHAR(20)) AS MobileNumber, ISNULL
                             ((SELECT        TOP (1) ScreeningValueId
                                 FROM            dbo.PatientScreening
                                 WHERE        (PatientId = pt.Id) AND (ScreeningTypeId IN
                                                              (SELECT        Id
                                                                FROM            dbo.LookupMaster
                                                                WHERE        (Name = 'TBStatus')))
                                 ORDER BY Id DESC), 0) AS TBStatus, ISNULL
                             ((SELECT        TOP (1) ScreeningValueId
                                 FROM            dbo.PatientScreening AS PatientScreening_1
                                 WHERE        (PatientId = pt.Id) AND (ScreeningTypeId IN
                                                              (SELECT        Id
                                                                FROM            dbo.LookupMaster AS LookupMaster_1
                                                                WHERE        (Name = 'NutritionStatus')))
                                 ORDER BY Id DESC), 0) AS NutritionStatus, ISNULL
                             ((SELECT        TOP (1) Categorization
                                 FROM            dbo.PatientCategorization
                                 WHERE        (PatientId = pt.Id)
                                 ORDER BY id DESC), 0) AS Categorization, pt.DobPrecision
FROM            dbo.Patient AS pt INNER JOIN
                         dbo.Person AS pn ON pn.Id = pt.PersonId INNER JOIN
                         dbo.PatientEnrollment AS pe ON pt.Id = pe.PatientId INNER JOIN
                         dbo.PatientIdentifier AS pni ON pni.PatientId = pt.Id INNER JOIN
                         dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id LEFT OUTER JOIN
                         dbo.PatientCareending AS ptC ON pt.Id = ptC.PatientId LEFT OUTER JOIN
                         dbo.PersonContact AS pc ON pc.PersonId = pt.PersonId
WHERE        (dbo.Identifiers.Name = 'CCC Registration Number') AND pn.DeleteFlag=0;
GO

/****** Object:  View [dbo].[PatientRegistrationView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientRegistrationView]'))
DROP VIEW [dbo].[PatientRegistrationView]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientRegistrationView]
AS
SELECT        Id, ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) AS NationalId, DeleteFlag, CreatedBy, CreateDate, AuditData, 
                         RegistrationDate
FROM            dbo.Patient

GO

/****** Object:  View [dbo].[RegimenMapView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RegimenMapView]'))
DROP VIEW [dbo].[RegimenMapView]
GO

/****** Object:  View [dbo].[RegimenMapView]    Script Date: 8/17/2017 11:29:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[RegimenMapView]
AS
SELECT ROW_NUMBER() OVER(PARTITION BY R.Ptn_Pk ORDER BY V.VisitDate ASC) as RowNumber,p.Id as patientId,p.ptn_pk,R.Visit_Pk,R.RegimenMap_Pk,ISNULL(R.RegimenType,'NA') as RegimenType,R.UserID,V.VisitDate,R.DeleteFlag

FROM  dtl_RegimenMap R INNER JOIN 
patient p
ON
p.ptn_pk=R.Ptn_Pk
inner join ord_Visit V 
on V.Visit_Id = R.Visit_Pk

WHERE R.RegimenType<>''

GO


/****** Object:  View [dbo].[PatientTreatmentTrackerView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentTrackerView]'))
DROP VIEW [dbo].[PatientTreatmentTrackerView]
GO

/****** Object:  View [dbo].[PatientTreatmentTrackerView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientTreatmentTrackerView]
AS
SELECT  a.Id, a.PatientId, p.ptn_pk, a.ServiceAreaId, a.PatientMasterVisitId, a.RegimenStartDate, a.RegimenId,
    (SELECT        Name + '(' + DisplayName + ')' AS Expr1
    FROM            dbo.LookupItem
    WHERE        (Id = a.RegimenId)) AS Regimen, a.RegimenLineId,
    (SELECT        Name
    FROM            dbo.LookupItem AS LookupItem_3
    WHERE        (Id = a.RegimenLineId)) AS RegimenLine, a.DrugId, a.RegimenStatusDate, a.TreatmentStatusId,
    (SELECT        Name
    FROM            dbo.LookupItem AS LookupItem_2
    WHERE        (Id = a.TreatmentStatusId)) AS TreatmentStatus, a.ParentId, a.CreateBy AS CreatedBy, a.CreateDate, a.DeleteFlag, a.TreatmentStatusReasonId,
    (SELECT        Name
    FROM            dbo.LookupItem AS LookupItem_1
    WHERE        (Id = a.TreatmentStatusReasonId)) AS TreatmentReason, dbo.ord_PatientPharmacyOrder.DispensedByDate, (SELECT top 1 isnull(M.visitDate,M.Start) FROM PATIENTMASTERVISIT M WHERE M.PatientId=a.PatientId) as VisitDate
FROM            dbo.ARVTreatmentTracker AS a INNER JOIN
dbo.Patient AS p ON p.Id = a.PatientId INNER JOIN
dbo.ord_PatientPharmacyOrder ON a.PatientMasterVisitId = dbo.ord_PatientPharmacyOrder.PatientMasterVisitId


UNION ALL

SELECT 
	
	R.RegimenMap_Pk,R.patientId,R.ptn_pk,0 as ServiceAreaId,0 as PatientmasterVisitId,
	(SELECT TOP 1  p.DispensedByDate FROM ord_PatientPharmacyOrder p WHERE p.VisitID=R.Visit_Pk) as RegimenStartDate,
	ISNULL((SELECT top 1 id FROM LookupItem WHERE displayName IN(CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN 'TDF + 3TC + NVP'
			WHEN '3TC/AZT/NVP'	THEN 'AZT + 3TC + NVP'
			WHEN '3TC/AZT/EFV'	THEN 'AZT + 3TC + EFV '
			WHEN '3TC/AZT/LOPr' THEN 'AZT + 3TC + LPV/r'
			WHEN '3TC/LOPr/TDF' THEN 'TDF + 3TC + LPV/r'
			WHEN '3TC/ABC/EFV'	THEN 'TDF + 3TC + EFV'
			WHEN '3TC/ABC/LOPr' THEN 'ABC + 3TC + LPV/r'
			WHEN '3TC/ABC/NVP'	THEN 'ABC + 3TC + NVP'
			WHEN '3TC/EFV/TDF'	THEN 'TDF + 3TC + EFV'
			WHEN '3TC/NVP/AZT' THEN  'AZT + 3TC + NVP'
		END)),0) as RegimenId,
	ISNULL((
		CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='TDF + 3TC + NVP')+'(TDF + 3TC + NVP)'
			WHEN '3TC/AZT/NVP'	THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='AZT + 3TC + NVP')+'(AZT + 3TC + NVP)'
			WHEN '3TC/AZT/EFV'	THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='AZT + 3TC + EFV')+'(AZT + 3TC + EFV)'
			WHEN '3TC/AZT/LOPr' THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='AZT + 3TC + LPV/r')+'(AZT + 3TC + LPV/r)'
			WHEN '3TC/LOPr/TDF' THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='TDF + 3TC + LPV/r')+'(TDF + 3TC + LPV/r)'
			WHEN '3TC/ABC/EFV'	THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='TDF + 3TC + EFV')+'(TDF + 3TC + EFV)'
			WHEN '3TC/ABC/LOPr' THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='ABC + 3TC + LPV/r')+'(ABC + 3TC + LPV/r)'
			WHEN '3TC/ABC/NVP'	THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='ABC + 3TC + NVP')+'(ABC + 3TC + NVP)'
			WHEN '3TC/EFV/TDF'	THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='TDF + 3TC + EFV')+'(TDF + 3TC + EFV)'
			WHEN '3TC/NVP/AZT'  THEN (SELECT top 1 Name FROM lookupitem WHERE DisplayName='AZT + 3TC + NVP')+ '(AZT + 3TC + NVP)'
		END
	), (select TOP 1 Name from lookupitem where Name='Unknown')) as Regimen,
	ISNULL((SELECT top 1 id FROM LookupItem WHERE Name IN(SELECT 
	CASE MasterName 
	WHEN 'AdultFirstLineRegimen' THEN 'AdultARTFirstLine'
	WHEN 'AdultSecondlineRegimen' THEN 'AdultARTSecondLine'
	WHEN 'AdultThirdlineRegimen' THEN 'AdultARTThirdLine'
	WHEN 'PaedsFirstLineRegimen' THEN 'PaedsARTFirstLine'
	WHEN 'PaedsSecondlineRegimen' THEN 'PaedsARTSecondLine'
	WHEN 'PaedsThirdlineRegimen' THEN 'PaedsARTThirdLine' 
	END 
	
	FROM LookupItemView WHERE ItemDisplayName IN(CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN 'TDF + 3TC + NVP'
			WHEN '3TC/AZT/NVP'	THEN 'AZT + 3TC + NVP'
			WHEN '3TC/AZT/EFV'	THEN 'AZT + 3TC + EFV '
			WHEN '3TC/AZT/LOPr' THEN 'AZT + 3TC + LPV/r'
			WHEN '3TC/LOPr/TDF' THEN 'TDF + 3TC + LPV/r'
			WHEN '3TC/ABC/EFV'	THEN 'TDF + 3TC + EFV'
			WHEN '3TC/ABC/LOPr' THEN 'ABC + 3TC + LPV/r'
			WHEN '3TC/ABC/NVP'	THEN 'ABC + 3TC + NVP'
			WHEN '3TC/EFV/TDF'	THEN 'TDF + 3TC + EFV'
			WHEN '3TC/NVP/AZT' THEN  'AZT + 3TC + NVP'
		END))),(select TOP 1 Id from lookupitem where Name='Unknown')) as RegimenLineId,
	ISNULL((SELECT top 1 Name FROM LookupItem WHERE Name IN(SELECT 
	CASE MasterName 
	WHEN 'AdultFirstLineRegimen' THEN 'AdultARTFirstLine'
	WHEN 'AdultSecondlineRegimen' THEN 'AdultARTSecondLine'
	WHEN 'AdultThirdlineRegimen' THEN 'AdultARTThirdLine'
	WHEN 'PaedsFirstLineRegimen' THEN 'PaedsARTFirstLine'
	WHEN 'PaedsSecondlineRegimen' THEN 'PaedsARTSecondLine'
	WHEN 'PaedsThirdlineRegimen' THEN 'PaedsARTThirdLine' 
	END  

	FROM LookupItemView WHERE ItemDisplayName IN(CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN 'TDF + 3TC + NVP'
			WHEN '3TC/AZT/NVP'	THEN 'AZT + 3TC + NVP'
			WHEN '3TC/AZT/EFV'	THEN 'AZT + 3TC + EFV '
			WHEN '3TC/AZT/LOPr' THEN 'AZT + 3TC + LPV/r'
			WHEN '3TC/LOPr/TDF' THEN 'TDF + 3TC + LPV/r'
			WHEN '3TC/ABC/EFV'	THEN 'TDF + 3TC + EFV'
			WHEN '3TC/ABC/LOPr' THEN 'ABC + 3TC + LPV/r'
			WHEN '3TC/ABC/NVP'	THEN 'ABC + 3TC + NVP'
			WHEN '3TC/EFV/TDF'	THEN 'TDF + 3TC + EFV'
			WHEN '3TC/NVP/AZT' THEN  'AZT + 3TC + NVP'
		END))),(select TOP 1 Name from lookupitem where Name='Unknown')) as RegimenLine,
	NULL as DrugId,
	NULL as RegimenStatusDate,
	(
	 case R.RowNumber
		  When 1 then (SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='Start Treatment') 
		  ELSE (SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='Continue current treatment') 
	 end
	) as TreatmentStatusId,
	(
	  case R.RowNumber

		when 1 then 'Start Treatment'
		ELSE 'Continue Current Treatment'

	  end
	) as TreatmentStatus,
	0 as ParentId,R.UserID,R.VisitDate,R.DeleteFlag,0 as TreatmentStatusReasonid,NULL as TreatmentReason,
	(SELECT TOP 1 p.DispensedByDate FROM ord_PatientPharmacyOrder p WHERE p.VisitID=R.Visit_Pk ) as DispensedByDate, (SELECT top 1 D.visitDate FROM ord_Visit D Where D.ptn_pk=R.ptn_pk)


	FROM RegimenMapView R
	INNER JOIN ord_PatientPharmacyOrder o 
	ON
	o.VisitID=R.Visit_Pk
	 WHERE R.RegimenType<>'' AND R.RegimenType IS NOT NULL AND o.DispensedByDate IS NOT NULL
GO


/****** Object:  View [dbo].[facilityStatisticsView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[facilityStatisticsView]'))
DROP VIEW [dbo].[facilityStatisticsView]
GO

/****** Object:  View [dbo].[facilityStatisticsView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW 
 [dbo].[facilityStatisticsView]
AS
SELECT        (SELECT        1 AS Expr1) AS Id,
                             (SELECT        ISNULL(COUNT(PT.Id), 0) AS Expr1
                               FROM            dbo.Patient AS PT INNER JOIN
                         dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId INNER JOIN
                         dbo.PatientIdentifier AS PI ON PT.Id = PI.PatientId AND PE.Id = PI.PatientEnrollmentId INNER JOIN
                         dbo.Identifiers AS IDE ON IDE.Id = PI.IdentifierTypeId
WHERE        (PT.DeleteFlag = 0) AND (IDE.Name = 'CCC Registration Number') AND (PT.PatientType =
                             (SELECT        TOP (1) ItemId
                               FROM            dbo.LookupItemView
                               WHERE        (MasterName = 'PatientType') AND (ItemName = 'New')))) AS TotalCumulativePatients,
                             (SELECT        COUNT(DISTINCT PatientId) AS Expr1
                               FROM            dbo.PatientTreatmentTrackerView
                               WHERE        (DATEDIFF(DAY, CreateDate, GETDATE()) <= 90) AND (RegimenLine IS NOT NULL)) +
                             (SELECT        COUNT(DISTINCT Ptn_Pk) AS Expr1
                               FROM            dbo.dtl_RegimenMap AS r
                               WHERE        (Ptn_Pk NOT IN
                                                             (SELECT        ptn_pk
                                                               FROM            dbo.PatientTreatmentTrackerView AS PatientTreatmentTrackerView_1)) AND (DATEDIFF(DAY, CreateDate, GETDATE()) <= 90)) AS TotalActiveOnArt,
                             (SELECT        ISNULL(COUNT(PT.Id), 0) AS Expr1
FROM            dbo.Patient AS PT INNER JOIN
                         dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId INNER JOIN
                         dbo.PatientIdentifier AS PI ON PT.Id = PI.PatientId AND PE.Id = PI.PatientEnrollmentId INNER JOIN
                         dbo.Identifiers AS IDE ON PI.IdentifierTypeId = IDE.Id
WHERE        (PT.PatientType =
                             (SELECT        TOP (1) ItemId
                               FROM            dbo.LookupItemView
                               WHERE        (MasterName = 'PatientType') AND (ItemName = 'Transfer-In'))) AND (PT.DeleteFlag = 0) AND (IDE.Name = 'CCC Registration Number')) AS TotalTransferIn,
                             (SELECT        ISNULL(COUNT(*), 0) AS Expr1
                               FROM            dbo.PatientCareending
                               WHERE        (ExitReason IN
                                                             (SELECT        Id
                                                               FROM            dbo.LookupItem AS LookupItem_3
                                                               WHERE        (Name = 'Transfer Out')))) +
                             (SELECT        COUNT(DISTINCT Ptn_Pk) AS Expr1
                               FROM            dbo.dtl_PatientCareEnded
                               WHERE        (PatientExitReason IN (118, 349))) AS TotalPatientsTransferedOut,
                             (SELECT        ISNULL(COUNT(*), 0) AS Expr1
                               FROM            dbo.dtl_PatientPharmacyOrder
                               WHERE        (Drug_Pk IN (460, 1095, 1015968, 969, 970, 971, 972, 973, 974, 975, 976, 977, 978, 979, 1005, 1006, 1010, 1011, 1012, 1013, 1014, 1015)) AND (DispensedQuantity > 0) AND (DATEDIFF(Day, 
                                                         CreateDate, GETDATE()) <= 90)) AS TotalOnCtxDapson,
                             (SELECT        ISNULL(COUNT(*), 0) AS Expr1
                               FROM            dbo.PatientCareending AS PatientCareending_1
                               WHERE        (ExitReason IN
                                                             (SELECT        Id
                                                               FROM            dbo.LookupItem AS LookupItem_2
                                                               WHERE        (Name = 'Death')))) +
                             (SELECT        COUNT(DISTINCT Ptn_Pk) AS Expr1
                               FROM            dbo.dtl_PatientCareEnded AS dtl_PatientCareEnded_1
                               WHERE        (PatientExitReason = 93)) AS TotalPatientsDead,
                             (SELECT        ISNULL(COUNT(PT.Id), 0) AS Expr1
FROM            dbo.Patient AS PT INNER JOIN
                         dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId INNER JOIN
                         dbo.PatientIdentifier AS PI ON PT.Id = PI.PatientId AND PE.Id = PI.PatientEnrollmentId INNER JOIN
                         dbo.Identifiers AS IDE ON PI.IdentifierTypeId = IDE.Id
WHERE        (PT.PatientType =
                             (SELECT        TOP (1) ItemId
                               FROM            dbo.LookupItemView
                               WHERE        (MasterName = 'PatientType') AND (ItemName = 'Transit'))) AND (PT.DeleteFlag = 0) AND (IDE.Name = 'CCC Registration Number')) AS TotalTransit,
                             (SELECT        ISNULL(COUNT(DISTINCT Ptn_Pk), 0) AS Expr1
                               FROM            dbo.dtl_PatientCareEnded AS p
                               WHERE        (PatientExitReason = 91)) AS LostToFollowUp,
							    (SELECT COUNT(DISTINCT PatientId) FROM PatientIdentifier I WHERE I.IdentifierTypeId IN(SELECT id FROM Identifiers WHERE Code='CCCNumber') AND I.PatientId IN(SELECT TOP 1 PatientId from ord_PatientPharmacyOrder O WHERE DATEDIFF(day,O.DispensedByDate,GETDATE())>90 ORDER BY DispensedByDate DESC) AND I.PatientId NOT IN(SELECT PatientId FROM PatientCareending)) TotalUndocumentedLTFU
GO


/****** Object:  View [dbo].[TestingSummaryStatistics]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[TestingSummaryStatistics]'))
DROP VIEW [dbo].[TestingSummaryStatistics]
GO

/****** Object:  View [dbo].[TestingSummaryStatistics]    Script Date: 7/27/2017 3:00:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[TestingSummaryStatistics]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY Name) AS Id, Name, Value
FROM (SELECT 'Contacts with known status' AS Name, COUNT(*) AS Value 
FROM dbo.PersonRelationship PR LEFT OUTER JOIN (
	SELECT PersonId, TestingResult, row_number() Over (Partition By PersonId Order By TestingDate Desc) RowNum FROM HIVTesting
) C on C.PersonId = PR.PersonId AND C.RowNum = 1
WHERE (PR.BaselineResult IN (SELECT ItemId FROM dbo.LookupItemView WHERE (MasterName = 'BaseLineHivStatus') AND (ItemName <> 'Unknown') AND (ItemName <> 'Never Tested'))) OR C.TestingResult <> (SELECT TOP 1 ItemId FROM dbo.LookupItemView WHERE (MasterName = 'HivTestingResult') AND (ItemName = 'Never Tested'))

UNION ALL

SELECT 'Contacts with unknown status' AS Name, COUNT(*) AS Value 
FROM dbo.PersonRelationship PR
LEFT OUTER JOIN (SELECT PersonId, TestingResult, row_number() Over (Partition By PersonId Order By TestingDate Desc) RowNum FROM dbo.HIVTesting
) C on PR.PersonId = C.PersonId AND C.RowNum = 1
WHERE (PR.BaselineResult IN (SELECT ItemId FROM dbo.LookupItemView WHERE (MasterName = 'BaseLineHivStatus') AND (ItemName = 'Unknown' OR ItemName = 'Never Tested')) AND c.PersonId IS NULL) OR C.TestingResult = (SELECT TOP 1 ItemId FROM dbo.LookupItemView WHERE (MasterName = 'HivTestingResult') AND (ItemName = 'Never Tested'))

UNION ALL

SELECT 'Total Listed' AS Name, COUNT(*) AS Value
FROM dbo.PersonRelationship

UNION ALL

SELECT 'Total Positive' AS Name, COUNT(*) AS total
FROM dbo.PersonRelationship PR LEFT OUTER JOIN (
SELECT PersonId, TestingResult, row_number() Over (Partition By PersonId Order By TestingDate Desc) RowNum FROM HIVTesting
) C ON C.PersonId = PR.PersonId AND c.RowNum = 1
WHERE (BaselineResult = (SELECT ItemId FROM dbo.LookupItemView WHERE (MasterName = 'BaseLineHivStatus') AND (ItemName = 'Tested Positive'))) OR C.TestingResult =  (SELECT TOP 1 ItemId FROM dbo.LookupItemView WHERE (MasterName = 'HivTestingResult') AND (ItemName = 'Tested Positive'))

UNION ALL 

SELECT 'Linked to Care' AS Name, COUNT(*) AS Value FROM PatientLinkage) TestingSummaryStatistics
GO

/****** Object:  View [dbo].[PatientStabilitySummary]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientStabilitySummary]'))
DROP VIEW [dbo].[PatientStabilitySummary]
GO

/****** Object:  View [dbo].[PatientStabilitySummary]    Script Date: 7/27/2017 3:43:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientStabilitySummary]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY Category) AS Id,count(*) AS Value, Category FROM (Select Case
  When C.Id Is Null Or C.Categorization = 2 Then 'Unstable'
  Else 'Stable'
 End As Category 
From PatientEnrollment PE
INNER JOIN dbo.Patient PT ON PT.Id = pe.PatientId
INNER JOIN dbo.PatientIdentifier PI ON PE.Id = PI.PatientEnrollmentId 
INNER JOIN dbo.Identifiers IE ON PI.IdentifierTypeId = IE.Id
Left Outer Join (
Select PatientId
  ,    Id
  ,    Categorization
  ,    row_number() Over (Partition By PatientId Order By DateAssessed Desc) RowNum
From PatientCategorization

) C On C.PatientId = Pe.PatientId
And C.RowNum = 1
Where ServiceAreaId = 1 AND IE.Name = 'CCC Registration Number' AND PT.DeleteFlag = 0) AS Categorization
GROUP BY Category

GO