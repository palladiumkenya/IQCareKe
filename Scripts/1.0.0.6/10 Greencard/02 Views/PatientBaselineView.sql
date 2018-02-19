
/****** Object:  View [dbo].[PatientBaselineView]    Script Date: 2/7/2018 12:33:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientBaselineView]
AS
SELECT DISTINCT 
                         dbo.PatientHivDiagnosis.PatientId, dbo.PatientHivDiagnosis.Id, dbo.PatientHivDiagnosis.PatientMasterVisitId, dbo.PatientTransferIn.ServiceAreaId, 
                         dbo.PatientTransferIn.TransferInDate, dbo.PatientTransferIn.TreatmentStartDate, ISNULL(dbo.PatientTransferIn.CurrentTreatment, 0) AS CurrentTreatment,
                             (SELECT        Name + ' (' + DisplayName + ')' AS Expr1
                               FROM            dbo.LookupItem
                               WHERE        (Id = dbo.PatientTransferIn.CurrentTreatment)) AS CurrentTreatmentName, dbo.PatientTransferIn.FacilityFrom, dbo.PatientTransferIn.MFLCode, 
                         ISNULL(dbo.PatientTransferIn.CountyFrom, 0) AS CountyFrom, dbo.PatientTransferIn.TransferInNotes, dbo.PatientHivDiagnosis.HIVDiagnosisDate, 
                         dbo.PatientHivDiagnosis.EnrollmentDate, ISNULL(dbo.PatientHivDiagnosis.EnrollmentWHOStage, 0) AS EnrollmentWHOStage,
                             (SELECT        Name
                               FROM            dbo.LookupItem AS LookupItem_3
                               WHERE        (Id = dbo.PatientHivDiagnosis.EnrollmentWHOStage)) AS EnrollmentWHOStageName, dbo.PatientHivDiagnosis.ARTInitiationDate,
                             (SELECT        TOP (1) DispensedByDate
                               FROM            dbo.ord_PatientPharmacyOrder
                               WHERE        (PatientId = dbo.PatientHivDiagnosis.PatientId) AND (ptn_pharmacy_pk IN
                                                             (SELECT        ptn_pharmacy_pk
                                                               FROM            dbo.dtl_PatientPharmacyOrder
                                                               WHERE        (Prophylaxis = 0)))
                               ORDER BY ptn_pharmacy_pk) AS ARTInitiationDateNew, dbo.PatientTreatmentInitiation.DateStartedOnFirstLine, dbo.PatientTreatmentInitiation.Cohort, 
                         ISNULL(dbo.PatientTreatmentInitiation.Regimen, 0) AS Regimen,
                             (SELECT        Name + ' (' + DisplayName + ')' AS Expr1
                               FROM            dbo.LookupItem AS LookupItem_2
                               WHERE        (Id = dbo.PatientTreatmentInitiation.Regimen)) AS RegimenName, dbo.PatientTreatmentInitiation.BaselineViralload, 
                         dbo.PatientTreatmentInitiation.BaselineViralloadDate, dbo.PatientBaselineAssessment.HBVInfected, dbo.PatientBaselineAssessment.Pregnant, 
                         dbo.PatientBaselineAssessment.TBinfected, dbo.PatientBaselineAssessment.WHOStage,
                             (SELECT        Name
                               FROM            dbo.LookupItem AS LookupItem_1
                               WHERE        (Id = dbo.PatientBaselineAssessment.WHOStage)) AS WhoStageName, dbo.PatientBaselineAssessment.BreastFeeding, 
                         dbo.PatientBaselineAssessment.CD4Count, dbo.PatientBaselineAssessment.MUAC, dbo.PatientBaselineAssessment.Weight, 
                         dbo.PatientBaselineAssessment.Height, dbo.PatientBaselineAssessment.BMI, dbo.PatientTreatmentInitiation.ldl, dbo.PatientHivDiagnosis.HistoryARTUse
FROM            dbo.PatientHivDiagnosis LEFT OUTER JOIN
                         dbo.PatientTransferIn ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientTransferIn.PatientId LEFT OUTER JOIN
                         dbo.PatientTreatmentInitiation ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientTreatmentInitiation.PatientId LEFT OUTER JOIN
                         dbo.PatientBaselineAssessment ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientBaselineAssessment.PatientId

GO


