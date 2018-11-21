USE [IQCare_PMTCT]
GO

/****** Object:  View [dbo].[PatientPreventiveServiceView]    Script Date: 11/21/2018 9:35:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientPreventiveServiceView]
AS
SELECT        
 Id,
  PatientId,
  PatientMasterVisitId,
  PreventiveServiceId,
  (SELECT top 1 l.ItemName FROM LookupItemView l WHERE l.ItemId = p.PreventiveServiceId) PreventiveService,
  PreventiveServiceDate,
	  Description,
	   DeleteFlag, 
	   CreatedBy,
       CreateDate,
	    AuditData,
		 NextSchedule
FROM    dbo.PatientPreventiveServices p
GO


