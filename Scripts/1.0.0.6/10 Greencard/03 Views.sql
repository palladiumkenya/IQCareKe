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