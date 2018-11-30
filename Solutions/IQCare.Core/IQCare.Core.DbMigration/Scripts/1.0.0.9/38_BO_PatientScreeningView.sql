IF OBJECT_ID('dbo.PatientScreeningDetailsView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientScreeningDetailsView]
GO

CREATE VIEW [dbo].[PatientScreeningDetailsView]
AS

SELECT ps.Id,ps.PatientId, ps.PatientMasterVisitId, ps.ScreeningTypeId, ps.ScreeningCategoryId,ps.ScreeningDate,
ps.ScreeningDone, ps.ScreeningValueId, ps.VisitDate, ps.Comment, ps.CreateDate, ps.CreatedBy, ps.DeleteFlag,
lm.DisplayName AS screeningDisplayName, lm.Name as ScreeningTypeName

from PatientScreening ps
INNER JOIN LookupMaster lm 
ON lm.Id = ps.ScreeningTypeId
GO