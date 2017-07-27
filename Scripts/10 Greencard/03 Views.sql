/****** Object:  View [dbo].[Mst_Drug]    Script Date: 5/11/2017 4:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]'))
DROP VIEW [dbo].[Mst_Drug]
GO
/****** Object:  View [dbo].[Mst_Drug]    Script Date: 5/11/2017 4:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[view_patientVisit]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[view_patientVisit]'))
DROP VIEW [dbo].[view_patientVisit]
GO
/****** Object:  View [dbo].[ServiceAreaFormView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServiceAreaFormView]'))
DROP VIEW [dbo].[ServiceAreaFormView]
GO
/****** Object:  View [dbo].[ServiceAreaBusinessRuleView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServiceAreaBusinessRuleView]'))
DROP VIEW [dbo].[ServiceAreaBusinessRuleView]
GO
/****** Object:  View [dbo].[PersonView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonView]'))
DROP VIEW [dbo].[PersonView]
GO
/****** Object:  View [dbo].[PersonContactView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonContactView]'))
DROP VIEW [dbo].[PersonContactView]
GO
/****** Object:  View [dbo].[PatientVisitView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientVisitView]'))
DROP VIEW [dbo].[PatientVisitView]
GO
/****** Object:  View [dbo].[PatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientView]'))
DROP VIEW [dbo].[PatientView]
GO
/****** Object:  View [dbo].[PatientTreatmentTrackerView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentTrackerView]'))
DROP VIEW [dbo].[PatientTreatmentTrackerView]
GO
/****** Object:  View [dbo].[PatientTreatmentSupporterView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporterView]'))
DROP VIEW [dbo].[PatientTreatmentSupporterView]
GO
/****** Object:  View [dbo].[PatientPopulationView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientPopulationView]'))
DROP VIEW [dbo].[PatientPopulationView]
GO
/****** Object:  View [dbo].[PatientBaselineView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientBaselineView]'))
DROP VIEW [dbo].[PatientBaselineView]
GO
/****** Object:  View [dbo].[ord_PatientLabOrder]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientLabOrder]'))
DROP VIEW [dbo].[ord_PatientLabOrder]
GO
/****** Object:  View [dbo].[Laboratory_ViralLoad]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Laboratory_ViralLoad]'))
DROP VIEW [dbo].[Laboratory_ViralLoad]
GO
/****** Object:  View [dbo].[LookupView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LookupView]'))
DROP VIEW [dbo].[LookupView]
GO
/****** Object:  View [dbo].[gcPatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[gcPatientView]'))
DROP VIEW [dbo].[gcPatientView]
GO
/****** Object:  View [dbo].[PatientRegistrationView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientRegistrationView]'))
DROP VIEW [dbo].[PatientRegistrationView]
GO
/****** Object:  View [dbo].[facilityStatisticsView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[facilityStatisticsView]'))
DROP VIEW [dbo].[facilityStatisticsView]
GO
/****** Object:  View [dbo].[PatientServiceEnrollmentView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientServiceEnrollmentView]'))
DROP VIEW [dbo].[PatientServiceEnrollmentView]
GO
/****** Object:  View [dbo].[LookupItemView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LookupItemView]'))
DROP VIEW [dbo].[LookupItemView]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_PatientCareEnding]'))
DROP VIEW [dbo].[VW_PatientCareEnding]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientICFView]'))
DROP VIEW [dbo].[PatientICFView]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientPopulationView]'))
DROP VIEW [dbo].[PatientPopulationView]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[BlueCardAppointmentView]'))
DROP VIEW [dbo].[BlueCardAppointmentView]
GO
/****** Object:  View [dbo].[LookupItemView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[LookupItemView]
AS
SELECT        MasterId, ItemId, MasterName, ItemName, DisplayName, ItemDisplayName, OrdRank, ISNULL(ROW_NUMBER() OVER(ORDER BY ItemId DESC), -1) AS RowID
FROM            (SELECT        M.Id AS MasterId, I.Id AS ItemId, M.Name AS MasterName, I.Name AS ItemName, L.DisplayName, L.DisplayName AS ItemDisplayName, L.OrdRank
                          FROM            dbo.LookupMaster AS M INNER JOIN
                                                    dbo.LookupMasterItem AS L ON M.Id = L.LookupMasterId INNER JOIN
                                                    dbo.LookupItem AS I ON L.LookupItemId = I.Id) AS X


GO
/****** Object:  View [dbo].[PatientServiceEnrollmentView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientServiceEnrollmentView]
AS
SELECT        dbo.PatientIdentifier.IdentifierValue AS EnrollmentNumber, dbo.LookupItemView.DisplayName AS ServiceArea, dbo.PatientEnrollment.EnrollmentDate, 
                         CAST((CASE dbo.PatientEnrollment.CareEnded WHEN 0 THEN 'Active' WHEN 1 THEN 'In-Active' END) AS varchar(50)) AS PatientStatus, dbo.PatientIdentifier.PatientId, dbo.Person.Id AS PersonId,ISNULL(ROW_NUMBER() OVER (ORDER BY PersonId DESC), - 1) AS Id
FROM            dbo.PatientIdentifier INNER JOIN
                         dbo.LookupItemView ON dbo.PatientIdentifier.IdentifierTypeId = dbo.LookupItemView.ItemId INNER JOIN
                         dbo.PatientEnrollment ON dbo.PatientIdentifier.PatientEnrollmentId = dbo.PatientEnrollment.Id INNER JOIN
                         dbo.Patient ON dbo.PatientEnrollment.PatientId = dbo.Patient.Id RIGHT OUTER JOIN
                         dbo.Person ON dbo.Patient.PersonId = dbo.Person.Id


GO

/***** Object:  View [dbo].[gcPatientView]    Script Date: 7/25/2017 12:43:40 PM *****/

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE VIEW [dbo].[gcPatientView]
AS
     SELECT DISTINCT
            pt.Id,
            pt.PersonId,
            pt.ptn_pk,
            pni.IdentifierValue AS EnrollmentNumber,
            pt.PatientIndex,
            CAST(DECRYPTBYKEY(pn.FirstName) AS VARCHAR(50)) AS FirstName,
            CAST(DECRYPTBYKEY(pn.MidName) AS VARCHAR(50)) AS MiddleName,
            CAST(DECRYPTBYKEY(pn.LastName) AS VARCHAR(50)) AS LastName,
            pn.Sex,
            pn.Active,
            pt.RegistrationDate,
            pe.EnrollmentDate AS [EnrollmentDate ],
            CAST((CASE pe.CareEnded
                      WHEN 0
                      THEN 'Active'
                      WHEN 1
                      THEN
                 (
                     SELECT TOP 1 ItemName
                     FROM LookupItemView
                     WHERE MasterName = 'CareEnded'
                           AND ItemId = ptC.ExitReason
                 )
                  END) AS VARCHAR(50)) AS PatientStatus,
            ptC.ExitReason,
            pt.DateOfBirth,
            CAST(DECRYPTBYKEY(pt.NationalId) AS VARCHAR(50)) AS NationalId,
            pt.FacilityId,
            pt.PatientType,
            pe.TransferIn,
            CAST(DECRYPTBYKEY(pc.MobileNumber) AS VARCHAR(20)) AS MobileNumber,
            ISNULL(
                  (
                      SELECT TOP 1 ScreeningValueId
                      FROM PatientScreening
                      WHERE patientId = pt.Id
                            AND ScreeningTypeId IN
                      (
                          SELECT id
                          FROM LookupMaster
                          WHERE Name = 'TBStatus'
                      ) ORDER BY Id DESC
                  ), 0) AS TBStatus,
            ISNULL(
                  (
                      SELECT TOP 1 ScreeningValueId
                      FROM PatientScreening
                      WHERE patientId = pt.Id
                            AND ScreeningTypeId IN
                      (
                          SELECT id
                          FROM LookupMaster
                          WHERE Name = 'NutritionStatus'
                      ) ORDER BY Id DESC
                  ), 0) AS NutritionStatus,
            ISNULL(
                  (
                      SELECT TOP 1 Categorization
                      FROM PatientCategorization
                      WHERE PatientId = pt.Id ORDER BY Id DESC
                  ), 0) AS Categorization
     FROM dbo.Patient AS pt
          INNER JOIN dbo.Person AS pn ON pn.Id = pt.PersonId
          INNER JOIN dbo.PatientEnrollment AS pe ON pt.Id = pe.PatientId
          INNER JOIN dbo.PatientIdentifier AS pni ON pni.PatientId = pt.Id
          INNER JOIN dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id
          LEFT OUTER JOIN dbo.PatientCareending AS ptC ON pt.Id = ptC.PatientId
          LEFT OUTER JOIN dbo.PersonContact AS pc ON pc.PersonId = pt.PersonId
     WHERE(dbo.Identifiers.Name = 'CCC Registration Number');
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
/****** Object:  View [dbo].[LookupView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[LookupView]
AS
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'MST_DECODE'				Category
	From mst_Decode D
	Inner Join Mst_Code C On C.CodeID = D.CodeID
	Union All
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'MST_MODDECODE'				Category
	From mst_ModDecode D
	Inner Join Mst_ModCode C On C.CodeID = D.CodeID
	Union All
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'MST_BLUEDECODE'			Category
	From mst_BlueDecode D
	Inner Join Mst_BlueCode C On C.CodeID = D.CodeID
	Union All
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'PMTCTDECODE'				Category
	From mst_pmtctDeCode D
	Inner Join mst_pmtctCode C On C.CodeID = D.CodeID



GO
/****** Object:  View [dbo].[ord_PatientLabOrder]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[ord_PatientLabOrder]
AS
Select	Id As LabID,
		Null As OldLabID,
		Ptn_Pk,
		LocationId,
		OrderedBy As OrderedbyName,
		OrderDate As OrderedbyDate,
		(
			Select Top (1)
				ResultBy
			From dbo.dtl_LabOrderTest
			Where (LabOrderId = lo.Id) And ResultBy Is Not Null
		)
		As ReportedbyName,
		(
			Select Top (1)
				ResultDate
			From dbo.dtl_LabOrderTest As R
			Where (LabOrderId = lo.Id) And R.ResultDate Is Not Null
		)
		As ReportedbyDate,
		Null As CheckedbyName,
		Null As CheckedbyDate,
		PreClinicLabDate,
		DeleteFlag,
		UserId,
		CreateDate,
		UpdateDate,
		VisitId,
		Null As LabPeriod,
		OrderNumber As LabNumber
From dbo.ord_LabOrder As lo

GO
/****** Object:  View [dbo].[Laboratory_ViralLoad]    Script Date: 5/17/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Laboratory_ViralLoad]
AS
SELECT        labTrac.Id, labTrac.patientId, PatientLabTracker_1.ResultValues, PatientLabTracker_1.FacilityId
FROM            (SELECT        MAX(Id) AS Id, patientId
                          FROM            dbo.PatientLabTracker
                          GROUP BY patientId, LabTestId
                          HAVING         (LabTestId = 3)) AS labTrac INNER JOIN
                         dbo.PatientLabTracker AS PatientLabTracker_1 ON labTrac.Id = PatientLabTracker_1.Id
                        WHERE        (PatientLabTracker_1.Results = 'Complete')
GO

/***** Object:  View [dbo].[PatientBaselineView]    Script Date: 7/25/2017 12:44:26 PM *****/

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE VIEW [dbo].[PatientBaselineView]
AS
     SELECT dbo.PatientHivDiagnosis.PatientId,
            dbo.PatientHivDiagnosis.Id,
            dbo.PatientHivDiagnosis.PatientMasterVisitId,
            dbo.PatientTransferIn.ServiceAreaId,
            dbo.PatientTransferIn.TransferInDate,
            dbo.PatientTransferIn.TreatmentStartDate,
            ISNULL(dbo.PatientTransferIn.CurrentTreatment, 0) AS CurrentTreatment,
     (
         SELECT Name+' ('+DisplayName+')' AS Expr1
         FROM dbo.LookupItem
         WHERE(Id = dbo.PatientTransferIn.CurrentTreatment)
     ) AS CurrentTreatmentName,
            dbo.PatientTransferIn.FacilityFrom,
            dbo.PatientTransferIn.MFLCode,
            ISNULL(dbo.PatientTransferIn.CountyFrom, 0) AS CountyFrom,
            dbo.PatientTransferIn.TransferInNotes,
            dbo.PatientHivDiagnosis.HIVDiagnosisDate,
            dbo.PatientHivDiagnosis.EnrollmentDate,
            ISNULL(dbo.PatientHivDiagnosis.EnrollmentWHOStage, 0) AS EnrollmentWHOStage,
     (
         SELECT Name
         FROM dbo.LookupItem AS LookupItem_3
         WHERE(Id = dbo.PatientHivDiagnosis.EnrollmentWHOStage)
     ) AS EnrollmentWHOStageName,
            dbo.PatientHivDiagnosis.ARTInitiationDate,
     (
         SELECT TOP 1 DispensedByDate
         FROM ord_PatientPharmacyOrder
         WHERE PatientId = dbo.PatientHivDiagnosis.PatientId
               AND ptn_pharmacy_pk IN
         (
             SELECT ptn_pharmacy_pk
             FROM dtl_PatientPharmacyOrder
             WHERE Prophylaxis = 0
         ) ORDER BY ptn_pharmacy_pk ASC
     ) AS ARTInitiationDateNew,
            dbo.PatientTreatmentInitiation.DateStartedOnFirstLine,
            dbo.PatientTreatmentInitiation.Cohort,
            ISNULL(dbo.PatientTreatmentInitiation.Regimen, 0) AS Regimen,
     (
         SELECT Name+' ('+DisplayName+')' AS Expr1
         FROM dbo.LookupItem AS LookupItem_2
         WHERE(Id = dbo.PatientTreatmentInitiation.Regimen)
     ) AS RegimenName,
            dbo.PatientTreatmentInitiation.BaselineViralload,
            dbo.PatientTreatmentInitiation.BaselineViralloadDate,
            dbo.PatientBaselineAssessment.HBVInfected,
            dbo.PatientBaselineAssessment.Pregnant,
            dbo.PatientBaselineAssessment.TBinfected,
            dbo.PatientBaselineAssessment.WHOStage,
     (
         SELECT Name
         FROM dbo.LookupItem AS LookupItem_1
         WHERE(Id = dbo.PatientBaselineAssessment.WHOStage)
     ) AS WhoStageName,
            dbo.PatientBaselineAssessment.BreastFeeding,
            dbo.PatientBaselineAssessment.CD4Count,
            dbo.PatientBaselineAssessment.MUAC,
            dbo.PatientBaselineAssessment.Weight,
            dbo.PatientBaselineAssessment.Height,
            dbo.PatientBaselineAssessment.BMI,
            dbo.PatientTreatmentInitiation.ldl
     FROM dbo.PatientHivDiagnosis
          LEFT OUTER JOIN dbo.PatientTransferIn ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientTransferIn.PatientId
          LEFT OUTER JOIN dbo.PatientTreatmentInitiation ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientTreatmentInitiation.PatientId
          LEFT OUTER JOIN dbo.PatientBaselineAssessment ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientBaselineAssessment.PatientId;
GO

/****** Object:  View [dbo].[PatientPopulationView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientPopulationView]
AS
SELECT b.ptn_pk AS PatientPK, CASE WHEN a.PopulationType = 'General Population' THEN 'General Population' ELSE c.name END AS PopulationCategory
FROM     dbo.PatientPopulation AS a INNER JOIN
                  dbo.Patient AS b ON a.PersonId = b.PersonId LEFT OUTER JOIN
                  dbo.LookupItem AS c ON a.PopulationCategory = b.Id


GO
/****** Object:  View [dbo].[PatientTreatmentSupporterView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientTreatmentSupporterView]
AS
SELECT        Id, PersonId, SupporterId, CAST(DECRYPTBYKEY(MobileContact) AS VARCHAR(50)) AS MobileContact, DeleteFlag, CreatedBy, CreateDate, AuditData
FROM            dbo.PatientTreatmentSupporter


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
    WHERE        (Id = a.TreatmentStatusReasonId)) AS TreatmentReason, dbo.ord_PatientPharmacyOrder.DispensedByDate
FROM            dbo.ARVTreatmentTracker AS a INNER JOIN
dbo.Patient AS p ON p.Id = a.PatientId INNER JOIN
dbo.ord_PatientPharmacyOrder ON a.PatientMasterVisitId = dbo.ord_PatientPharmacyOrder.PatientMasterVisitId
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
                             (SELECT        ISNULL(COUNT(*), 0) AS Expr1
                               FROM            dbo.Patient
                               WHERE        (Id IN
                                                             (SELECT        PatientId
                                                               FROM            dbo.PatientEnrollment)) AND (PatientType NOT IN
                                                             (SELECT        dbo.Patient.Id
                                                               FROM            dbo.LookupItemView
                                                               WHERE        (ItemName = 'Transit') OR
                                                                                         (ItemName = 'Transfer-In'))) AND (Id IN
                                                             (SELECT        PatientId
                                                               FROM            dbo.PatientIdentifier
                                                               WHERE        (IdentifierTypeId IN
                                                                                             (SELECT        Id
                                                                                               FROM            dbo.Identifiers
                                                                                               WHERE        (Name = 'CCC Registration Number')))))) AS TotalCumulativePatients,
                             (SELECT        COUNT(DISTINCT PatientId) AS Expr1
                               FROM            dbo.PatientTreatmentTrackerView
                               WHERE        (DATEDIFF(DAY, CreateDate, GETDATE()) <= 90) AND (RegimenLine IS NOT NULL)) +
                             (SELECT        COUNT(DISTINCT Ptn_Pk) AS Expr1
                               FROM            dbo.dtl_RegimenMap AS r
                               WHERE        (Ptn_Pk NOT IN
                                                             (SELECT        ptn_pk
                                                               FROM            dbo.PatientTreatmentTrackerView AS PatientTreatmentTrackerView_1)) AND (DATEDIFF(DAY, CreateDate, GETDATE()) <= 90)) AS TotalActiveOnArt,
                             (SELECT        ISNULL(COUNT(*), 0) AS Expr1
                               FROM            dbo.Patient AS Patient_3
                               WHERE        (PatientType IN
                                                             (SELECT        Id
                                                               FROM            dbo.LookupItem
                                                               WHERE        (Name = 'Transfer-In'))) AND (Id IN
                                                             (SELECT        PatientId
                                                               FROM            dbo.PatientEnrollment AS PatientEnrollment_2)) AND (Id IN
                                                             (SELECT        PatientId
                                                               FROM            dbo.PatientIdentifier AS PatientIdentifier_2
                                                               WHERE        (IdentifierTypeId IN
                                                                                             (SELECT        Id
                                                                                               FROM            dbo.Identifiers AS Identifiers_2
                                                                                               WHERE        (Name = 'CCC Registration Number')))))) +
                             (SELECT        COUNT(DISTINCT Ptn_Pk) AS Expr1
                               FROM            dbo.mst_Patient
                               WHERE        (TransferIn IS NOT NULL) AND (Ptn_Pk NOT IN
                                                             (SELECT        ptn_pk
                                                               FROM            dbo.Patient AS Patient_2))) AS TotalTransferIn,
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
                             (SELECT        ISNULL(COUNT(*), 0) AS Expr1
                               FROM            dbo.Patient AS Patient_1
                               WHERE        (PatientType IN
                                                             (SELECT        Id
                                                               FROM            dbo.LookupItem AS LookupItem_1
                                                               WHERE        (Name = 'Transit'))) AND (Id IN
                                                             (SELECT        PatientId
                                                               FROM            dbo.PatientEnrollment AS PatientEnrollment_1)) AND (Id IN
                                                             (SELECT        PatientId
                                                               FROM            dbo.PatientIdentifier AS PatientIdentifier_1
                                                               WHERE        (IdentifierTypeId IN
                                                                                             (SELECT        Id
                                                                                               FROM            dbo.Identifiers AS Identifiers_1
                                                                                               WHERE        (Name = 'CCC Registration Number')))))) +
                             (SELECT        COUNT(DISTINCT Ptn_Pk) AS Expr1
                               FROM            dbo.mst_Patient AS mst_Patient_1
                               WHERE        (Ptn_Pk IN
                                                             (SELECT        Ptn_pk
                                                               FROM            dbo.Lnk_PatientProgramStart
                                                               WHERE        (ModuleId = 20)))) AS TotalTransit,
                             (SELECT        ISNULL(COUNT(DISTINCT Ptn_Pk), 0) AS Expr1
                               FROM            dbo.dtl_PatientCareEnded AS p
                               WHERE        (PatientExitReason = 91)) AS LostToFollowUp
GO


GO
/****** Object:  View [dbo].[PatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientView]
AS
Select	Ptn_Pk
	,	cast(decryptbykey(FirstName) As varchar(50))	As FirstName
	,	cast(decryptbykey(LastName) As varchar(50))		As LastName
	,	cast(decryptbykey(MiddleName) As varchar(50))	As MiddleName
	,	cast(decryptbykey(FirstName) As varchar(50)) + ' '+Isnull(cast(decryptbykey(MiddleName) As varchar(50)) ,'') + cast(decryptbykey(LastName) As varchar(50))		As PatientName
	,	LocationId
	,	IQNumber
	,	RegistrationDate
	,	DOB
	,	Case Sex
			When 16 Then 'Male'
			Else 'Female'
		End												As Sex
	,	DobPrecision
	,	DateOfDeath
	,	MaritalStatus
	,	Sex												As SexId
	,	Nullif(Convert(varchar(100), Decryptbykey([Address])),'') As [Address]
	,	Nullif(Convert(varchar(100), Decryptbykey(Phone)),'') As Phone
	,	PatientFacilityId
	,	UserId
	,	CreateDate
	,	UpdateDate
	,	DeleteFlag
	,	Status
From mst_Patient
Where (DeleteFlag = 0 Or DeleteFlag Is Null)

GO
/****** Object:  View [dbo].[PatientVisitView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create VIEW [dbo].[PatientVisitView]
AS
Select	V.Visit_Id		VisitId
	,	VisitName
	,	V.Ptn_Pk		PatientId
	,	V.LocationID	LocationId
	,	V.VisitDate
	,	Isnull(V.DataQuality,0) DataQuality
	,	Isnull(V.Signature,0) [Signature]
	,	V.CreateDate
	,	V.UserID		UserId
	,	Cast(Isnull(V.DeleteFlag,0) as bit) DeleteFlag
From Ord_visit V
Inner Join mst_VisitType T On T.VisitTypeID = V.VisitType
Where V.Ptn_Pk > 0


GO
/****** Object:  View [dbo].[PersonContactView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonContactView]
AS
SELECT        Id, PersonId, CAST(DECRYPTBYKEY(PhysicalAddress) AS VARCHAR(50)) AS PhysicalAddress, CAST(DECRYPTBYKEY(MobileNumber) AS VARCHAR(50)) AS MobileNumber, 
                         CAST(DECRYPTBYKEY(AlternativeNumber) AS VARCHAR(50)) AS AlternativeNumber, CAST(DECRYPTBYKEY(EmailAddress) AS VARCHAR(50)) AS EmailAddress, Active, DeleteFlag, CreatedBy, CreateDate
FROM            dbo.PersonContact


GO
/****** Object:  View [dbo].[PersonView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonView]
AS
SELECT        Id, CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) AS MiddleName, CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) AS LastName, 
                         Sex, Active, DeleteFlag, CreateDate, CreatedBy, AuditData, DateOfBirth, DobPrecision
FROM            dbo.Person


GO
/****** Object:  View [dbo].[ServiceAreaBusinessRuleView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[ServiceAreaBusinessRuleView]
AS
Select	SR.Id
	,	SR.BusRuleId
	,	R.Name			As BusRuleName
	,	R.DeleteFlag	As BusRuleDeleteFlag
	,	R.ReferenceId	As BusRuleReferenceId
	,	SR.Value
	,	SR.Value1
	,	SR.SetType
	,	M.ModuleId		As ModuleId
	,	M.ModuleName	
	,	M.DisplayName	
	,	M.CanEnroll	
From lnk_ServiceBusinessRule As SR
Inner Join mst_module As M On M.ModuleId = SR.ModuleId
Inner Join Mst_BusinessRule As R On R.Id = SR.BusRuleId



GO
/****** Object:  View [dbo].[ServiceAreaFormView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ServiceAreaFormView]
AS
Select Distinct	F.FeatureID		As FeatureId
	,	F.FeatureName
	,	F.ReportFlag
	,	F.FeatureDeleteFlag	As FeatureDeleteFlag
	,	F.AdminFlag
	,	F.Published
	,	F.ModuleId
	,	F.MultiVisit
	,	F.RegistrationFormFlag
	,	F.ReferenceID	As ReferenceId
	,	F.CanLink
	,	F.FormId
	,	F.FormName
	,	F.FormDescription
	,	F.Custom
	,	F.CategoryId
	,	F.CategoryName Code
	,	F.FormDeleteFlag
	,	PermissionCount PermCount
From FormView As F
--Inner Join mst_VisitType As V On V.FeatureId = F.FeatureID
--Left Outer Join( Select D.ID, D.Name, C.CodeID, D.Code From mst_Decode D Inner Join mst_Code C On C.CodeID=D.CodeID And C.Name='Form Category') D On D.ID= V.CategoryId
Where (F.FeatureDeleteFlag = 0)
	And (F.AdminFlag = 0)
	And (F.FormDeleteFlag = 0)
	Or (F.FeatureDeleteFlag Is Null)
	And (F.AdminFlag Is Null)
	And (F.FormDeleteFlag Is Null)
Union
Select	Distinct F.FeatureID		As FeatureId
	,	F.FeatureName
	,	F.ReportFlag
	,	F.FeatureDeleteFlag
	,	F.AdminFlag
	,	F. Published
	,	L.ModuleId
	,	F.MultiVisit
	,	F.RegistrationFormFlag
	,	F.ReferenceId
	,	F.CanLink
	,	F.FormId
	,	F.FormName
	,	F.FormDescription
	,	F.Custom
	,	F.CategoryId
	,	F.CategoryName Code
	,	F.FormDeleteFlag
	,	PermissionCount PermCount
From FormView As F
--Inner Join mst_VisitType As V On V.FeatureId = F.FeatureID
Inner Join lnk_SplFormModule L On L.FeatureId = F.FeatureID
--Left Outer Join( Select D.ID, D.Name, C.CodeID, D.Code From mst_Decode D Inner Join mst_Code C On C.CodeID=D.CodeID And C.Name='Form Category') D On D.ID= V.CategoryId
Where (F.FeatureDeleteFlag = 0)
	And (F.AdminFlag = 0)
	And (F.FormDeleteFlag = 0)
	Or (F.FeatureDeleteFlag Is Null)
	And (F.AdminFlag Is Null)
	And (F.FormDeleteFlag Is Null)





GO
/****** Object:  View [dbo].[view_patientVisit]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[view_patientVisit]
AS
SELECT        TOP (100) PERCENT dbo.PatientMasterVisit.id AS visitID, 'Green Card' AS VisitName, dbo.PatientMasterVisit.patientId, dbo.PatientMasterVisit.visitDate, 
                         dbo.mst_User.UserName, dbo.PatientMasterVisit.DeleteFlag
FROM            dbo.PatientMasterVisit INNER JOIN
                         dbo.mst_User ON dbo.PatientMasterVisit.createdBy = dbo.mst_User.UserID
WHERE        (dbo.PatientMasterVisit.visitDate IS NOT NULL) AND (dbo.PatientMasterVisit.DeleteFlag IS NULL OR
                         dbo.PatientMasterVisit.DeleteFlag = 0 AND dbo.PatientMasterVisit.VisitType NOT IN(SELECT Id FROM LookupItem WHERE Name='Enrollment'))



GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "X"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 221
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LookupItemView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LookupItemView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientHivDiagnosis"
            Begin Extent = 
               Top = 12
               Left = 275
               Bottom = 142
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "PatientTransferIn"
            Begin Extent = 
               Top = 18
               Left = 17
               Bottom = 148
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientTreatmentInitiation"
            Begin Extent = 
               Top = 6
               Left = 513
               Bottom = 136
               Right = 723
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "PatientBaselineAssessment"
            Begin Extent = 
               Top = 6
               Left = 761
               Bottom = 136
               Right = 955
            End
            DisplayFlags = 280
            TopColumn = 12
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 31
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2175
         Width = 1500
         Width = 1500
         Width = 2385
         Width = 2595
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Wi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientBaselineView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'dth = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientBaselineView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientBaselineView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 7
               Left = 323
               Bottom = 170
               Right = 517
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 565
               Bottom = 170
               Right = 759
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1980
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientPopulationView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientPopulationView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[24] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[13] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[58] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientIdentifier"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 232
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupItemView"
            Begin Extent = 
               Top = 170
               Left = 276
               Bottom = 300
               Right = 459
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientEnrollment"
            Begin Extent = 
               Top = 25
               Left = 381
               Bottom = 155
               Right = 570
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 81
               Left = 717
               Bottom = 211
               Right = 887
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Person"
            Begin Extent = 
               Top = 169
               Left = 482
               Bottom = 299
               Right = 652
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 2175
         Width = 1980
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin Criteria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientServiceEnrollmentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Pane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientServiceEnrollmentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientServiceEnrollmentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientTreatmentSupporter"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientTreatmentSupporterView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientTreatmentSupporterView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PersonContact"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonContactView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonContactView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Person"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 8
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[12] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientMasterVisit"
            Begin Extent = 
               Top = 12
               Left = 263
               Bottom = 204
               Right = 433
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "mst_User"
            Begin Extent = 
               Top = 20
               Left = 21
               Bottom = 149
               Right = 191
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2190
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_patientVisit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_patientVisit'
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]'))
EXEC dbo.sp_executesql @statement = N'
/*
 Created By Joseph Njung''e
 Return DrugList from mst_itemmaster
 ItemTypeID 300	 = Drugs
*/
CREATE VIEW [dbo].[Mst_Drug]
AS
SELECT        D.Item_PK AS Drug_pk, D.ItemCode AS DrugID, D.ItemTypeID, D.ItemName AS DrugName, D.DeleteFlag, D.CreatedBy AS UserID, D.CreateDate, D.UpdateDate, D.DispensingMargin, D.DispensingUnitPrice, 
                         D.FDACode, D.Manufacturer, D.MaxStock, D.MinStock, D.PurchaseUnitPrice, D.QtyPerPurchaseUnit, ISNULL(CC.ItemSellingPrice, 0) AS SellingUnitPrice, D.DispensingUnit, D.PurchaseUnit, CC.EffectiveDate, 
                         1 AS Sequence, D.ItemInstructions, D.abbreviation
FROM            dbo.Mst_ItemMaster AS D INNER JOIN
                         dbo.Mst_ItemType AS I ON I.ItemTypeID = D.ItemTypeID LEFT OUTER JOIN
                             (SELECT DISTINCT ItemId, ItemType, PriceStatus, ItemSellingPrice, EffectiveDate
                               FROM            dbo.lnk_ItemCostConfiguration) AS CC ON CC.ItemId = D.Item_PK AND CC.ItemType = D.ItemTypeID AND CC.PriceStatus = 1
WHERE        (I.ItemName = ''Pharmaceuticals'')

' 
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE VIEW [dbo].[VW_PatientCareEnding]
AS
     SELECT dbo.Patient.ptn_pk,
            dbo.PatientMasterVisit.VisitDate,
            dbo.PatientCareending.ExitReason,
            dbo.LookupItemView.ItemName AS [Patient CareEnd Reason],
            dbo.PatientCareending.TransferOutfacility AS LPTFTransfer,
            dbo.PatientCareending.DateOfDeath,
            dbo.PatientCareending.ExitDate AS CareEndedDate,
            dbo.PatientCareending.Id AS CareEndedID,
            dbo.PatientCareending.CareEndingNotes,
            dbo.PatientCareending.Active,
            dbo.PatientCareending.DeleteFlag
     FROM dbo.Patient
          INNER JOIN dbo.PatientCareending ON dbo.Patient.Id = dbo.PatientCareending.PatientId
          INNER JOIN dbo.PatientMasterVisit ON dbo.PatientCareending.PatientMasterVisitId = dbo.PatientMasterVisit.Id
          INNER JOIN dbo.LookupItemView ON dbo.PatientCareending.ExitReason = dbo.LookupItemView.ItemId;
GO
EXEC sys.sp_addextendedproperty
     @name = N'MS_DiagramPane1',
     @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Patient"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 378
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientCareending"
            Begin Extent = 
               Top = 7
               Left = 303
               Bottom = 379
               Right = 530
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "PatientMasterVisit"
            Begin Extent = 
               Top = 7
               Left = 578
               Bottom = 106
               Right = 772
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "LookupItemView"
            Begin Extent = 
               Top = 7
               Left = 820
               Bottom = 261
               Right = 1034
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1200
         Width = 1200
         Width = 2544
         Width = 3432
         Width = 2280
         Width = 1680
         Width = 2004
         Width = 1800
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2196
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
',
     @level0type = N'SCHEMA',
     @level0name = N'dbo',
     @level1type = N'VIEW',
     @level1name = N'VW_PatientCareEnding';
GO
EXEC sys.sp_addextendedproperty
     @name = N'MS_DiagramPane2',
     @value = N'         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
',
     @level0type = N'SCHEMA',
     @level0name = N'dbo',
     @level1type = N'VIEW',
     @level1name = N'VW_PatientCareEnding';
GO
EXEC sys.sp_addextendedproperty
     @name = N'MS_DiagramPaneCount',
     @value = 2,
     @level0type = N'SCHEMA',
     @level0name = N'dbo',
     @level1type = N'VIEW',
     @level1name = N'VW_PatientCareEnding';
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE VIEW [dbo].[PatientICFView]
AS
     SELECT Ptn_pk,
            Visit_Pk,
            Symptom,
            SymptomCategory
     FROM
     (
         SELECT Ptn_pk,
                Visit_Pk,
                'Presumed TB' AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(Cough = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Presumed TB' AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(Fever = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Presumed TB' AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(WeightLoss = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Presumed TB' AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(Sweat = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Presumed TB' AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(ContactTB = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'No signs' AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(Cough = 0
               OR Cough IS NULL)
              AND (WeightLoss = 0
                   OR WeightLoss IS NULL)
              AND (Sweat = 0
                   OR Sweat IS NULL)
              AND (ContactTB = 0
                   OR ContactTB IS NULL)
              AND (Fever = 0
                   OR Fever IS NULL)
         UNION
         SELECT DISTINCT
                a.Ptn_pk,
                a.Visit_pk,
                b.Name AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.dtl_PatientOtherTreatment AS a
              INNER JOIN dbo.mst_BlueDecode AS b ON a.TBStatus = b.ID
         WHERE(b.Name NOT IN('Not Done', 'TB Rx'))
         UNION
         SELECT Ptn_pk,
                Visit_Pk,
                'Yellow Urine' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(YellowUrine = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Numbness Adult' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(NumbnessAdult = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Numbness Peads' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(NumbnessPead = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Yellow Eyes' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(YellowEyes = 1)
         UNION
         SELECT DISTINCT
                Ptn_pk,
                Visit_Pk,
                'Tender Abdomen' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.DTL_FBCUSTOMFIELD_Intensive_Case_Finding AS n
         WHERE(TenderAbdomen = 1)
         UNION
         SELECT b.ptn_pk,
                a.PatientMasterVisitId AS visit_pk,
                CASE
                    WHEN a.Cough = 1
                         OR a.NightSweats = 1
                         OR a.WeightLoss = 1
                         OR a.Fever = 1
                    THEN 'Presumed TB'
                    ELSE 'No signs'
                END AS Symptom,
                'TB Screening' AS SymptomCategory
         FROM dbo.PatientIcf AS a
              INNER JOIN dbo.PatientMasterVisit AS v ON v.Id = a.PatientMasterVisitId
                                                        AND a.PatientId = v.PatientId
              INNER JOIN dbo.Patient AS b ON a.PatientId = b.Id
                                             AND v.PatientId = b.Id
         UNION
         SELECT b.ptn_pk,
                a.PatientMasterVisitId AS visit_pk,
                'Yellow Urine' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.PatientIptWorkup AS a
              INNER JOIN dbo.PatientMasterVisit AS v ON v.Id = a.PatientMasterVisitId
                                                        AND a.PatientId = v.PatientId
              INNER JOIN dbo.Patient AS b ON a.PatientId = b.Id
                                             AND v.PatientId = b.Id
         WHERE(a.YellowColouredUrine = 1)
         UNION
         SELECT b.ptn_pk,
                a.PatientMasterVisitId AS visit_pk,
                'Numbness' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.PatientIptWorkup AS a
              INNER JOIN dbo.PatientMasterVisit AS v ON v.Id = a.PatientMasterVisitId
                                                        AND a.PatientId = v.PatientId
              INNER JOIN dbo.Patient AS b ON a.PatientId = b.Id
                                             AND v.PatientId = b.Id
         WHERE(a.Numbness = 1)
         UNION
         SELECT b.ptn_pk,
                a.PatientMasterVisitId AS visit_pk,
                'Yellow Eyes' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.PatientIptWorkup AS a
              INNER JOIN dbo.PatientMasterVisit AS v ON v.Id = a.PatientMasterVisitId
                                                        AND a.PatientId = v.PatientId
              INNER JOIN dbo.Patient AS b ON a.PatientId = b.Id
                                             AND v.PatientId = b.Id
         WHERE(a.YellownessOfEyes = 1)
         UNION
         SELECT b.ptn_pk,
                a.PatientMasterVisitId AS visit_pk,
                'Tender Abdomen' AS Symptom,
                'IPT Workup' AS SymptomCategory
         FROM dbo.PatientIptWorkup AS a
              INNER JOIN dbo.PatientMasterVisit AS v ON v.Id = a.PatientMasterVisitId
                                                        AND a.PatientId = v.PatientId
              INNER JOIN dbo.Patient AS b ON a.PatientId = b.Id
                                             AND v.PatientId = b.Id
         WHERE(a.AbdominalTenderness = 1)
     ) AS a_1;
GO
EXEC sys.sp_addextendedproperty
     @name = N'MS_DiagramPane1',
     @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a_1"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
',
     @level0type = N'SCHEMA',
     @level0name = N'dbo',
     @level1type = N'VIEW',
     @level1name = N'PatientICFView';
GO
EXEC sys.sp_addextendedproperty
     @name = N'MS_DiagramPaneCount',
     @value = 1,
     @level0type = N'SCHEMA',
     @level0name = N'dbo',
     @level1type = N'VIEW',
     @level1name = N'PatientICFView';
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
ALTER VIEW [dbo].[PatientPopulationView]
AS
     SELECT DISTINCT
            b.ptn_pk AS PatientPK,
            CASE
                WHEN a.PopulationType = 'General Population'
                THEN 'General Population'
                WHEN a.PopulationType = 'Key Population'
                THEN c.ItemName
            END AS PopulationCategory
     FROM dbo.PatientPopulation AS a
          INNER JOIN dbo.Patient AS b ON a.PersonId = b.PersonId
          LEFT OUTER JOIN dbo.LookupItemView AS c ON a.PopulationCategory = c.ItemId
     WHERE(a.DeleteFlag = 0);
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE VIEW [dbo].[BlueCardAppointmentView]
AS
     SELECT Pat.Id PatientId,
            AppointmentId,
            FacilityName =
     (
         SELECT TOP 1 F.FacilityName
         FROM mst_Facility F
         WHERE F.FacilityID = PA.LocationID
     ),
            Pa.Visit_pk VisitId,
            AppDate AppointmentDate,
            AR.Name Reason,
            StatusName [AppointmentStatus],
            E.FirstName+' '+E.LastName AS Provider,
            PA.AppNote Description,
            ServiceArea =
     (
         SELECT ModuleName
         FROM mst_module M
         WHERE M.ModuleId = PA.ModuleID
     ),
            isnull(PA.UpdateDate, PA.CreateDate) StatusDate,ISNULL(ROW_NUMBER() OVER(ORDER BY Pat.Id DESC), -1) AS RowId
     FROM dtl_PatientAppointment PA
          INNER JOIN mst_patient P ON p.Ptn_Pk = PA.Ptn_pk
          INNER JOIN Patient Pat ON P.Ptn_Pk = Pat.ptn_pk
          LEFT OUTER JOIN vw_AppointmentReasons AR ON AR.ID = AppReason
          INNER JOIN
     (
         SELECT ID StatusID,
                Name StatusName
         FROM mst_decode
         WHERE codeId = 3
     ) ST ON ST.StatusID = PA.AppStatus
          INNER JOIN
     (
         SELECT UserId CreatedById,
                UserFirstName+' '+UserLastName CreatedBy
         FROM mst_User
     ) UC ON UC.CreatedById = PA.UserID
          LEFT OUTER JOIN
     (
         SELECT UserId UpdatedById,
                UserFirstName+' '+UserLastName UpdatedBy
         FROM mst_User
     ) MD ON MD.UpdatedById = PA.UpdateUserId
          LEFT OUTER JOIN mst_Employee E ON E.EmployeeID = PA.EmployeeID
     WHERE PA.DeleteFlag = 0
           AND P.DeleteFlag = 0;
GO