
CREATE VIEW [dbo].[PatientFamilyPlanningMethodView]
AS
SELECT pfm.Id, pfm.PatientId,pfm.FPMethodId, pfm.PatientFPId,pfm.Active, pfm.AuditData,pfm.CreateDate,pfm.CreatedBy, pfm.DeleteFlag,
lki.DisplayName, lki.Name

from PatientFamilyPlanningMethod pfm 
INNER JOIN LookupItem lki 
ON lki.Id = pfm.FPMethodId
GO
