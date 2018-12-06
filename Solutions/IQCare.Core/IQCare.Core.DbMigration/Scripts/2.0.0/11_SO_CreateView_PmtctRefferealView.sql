
IF OBJECT_ID('dbo.PmtctReferralView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientPreventiveServiceView]
GO


CREATE VIEW [dbo].[PmtctReferralView]
AS
SELECT        
	 r.Id,
	 r.PatientId,
	 r.ReferredFrom,
	 (SELECT top 1 l.itemName FROM LookupItemView l where l.itemId=r.ReferredFrom) ReferredFromName,
	 r.PatientMasterVisitId,
	 r.ReferralReason,
	 r.ReferredTo,
	 (SELECT top 1 l.ItemName FROM LookupItemView l WHERE l.ItemId=r.ReferredTo) RefferedToName,
	 r.ReferralDate,
	 r.ReferredBy, 
	 r.DeleteFlag, 
     r.CreateBy,
	 r.CreateDate
FROM  dbo.PmtctReferral r
GO


