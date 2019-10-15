IF OBJECT_ID('dbo.PatientPreventiveServiceView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientPreventiveServiceView]
GO

CREATE VIEW [dbo].[PatientPreventiveServiceView]
AS
SELECT        
 Id,
  PatientId,
  PatientMasterVisitId,
  PreventiveServiceId,
  (SELECT TOP 1 l.ItemName FROM LookupItemView l WHERE l.ItemId = p.PreventiveServiceId) PreventiveService,
  PreventiveServiceDate,
	  Description,
	   DeleteFlag, 
	   CreatedBy,
       CreateDate,
	    AuditData,
		 NextSchedule
FROM    dbo.PatientPreventiveServices p
WHERE DeleteFlag  <> 1
GO


