SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PregnancyOutcomeView]'))
DROP VIEW [dbo].[PregnancyOutcomeView]
GO

CREATE VIEW [dbo].[PregnancyOutcomeView]
AS
SELECT        dbo.PregnancyIndicator.Id, dbo.PregnancyIndicator.PatientId, dbo.PregnancyIndicator.PatientMasterVisitId, dbo.PregnancyIndicator.LMP, dbo.PregnancyIndicator.EDD,
                             (SELECT        TOP (1) DisplayName
                               FROM            dbo.LookupItemView
                               WHERE        (ItemId = dbo.PregnancyIndicator.PregnancyStatusId) AND (MasterName = 'PregnancyStatus')) AS PregnancyStatus,
                             (SELECT        TOP (1) DisplayName
                               FROM            dbo.LookupItemView AS LookupItemView_1
                               WHERE        (ItemId = dbo.Pregnancy.Outcome)) AS Outcome,
							   PregnancyIndicator.PregnancyStatusId,
							   Pregnancy.Outcome OutcomeStatus
FROM            dbo.PregnancyIndicator INNER JOIN
                         dbo.Pregnancy ON dbo.PregnancyIndicator.PatientId = dbo.Pregnancy.PatientId AND dbo.PregnancyIndicator.PatientMasterVisitId = dbo.Pregnancy.PatientMasterVisitId

GO

