/****** Object:  View [dbo].[vw_Maternity-encounters]    Script Date: 10/30/2018 9:38:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_Maternity-encounters]
AS
SELECT 
p.Id,
e.PatientMasterVisitId,
e.EncounterTypeId,
e.EncounterStartTime,
e.EncounterEndTime,
 p.PatientId,
  p.VisitNumber
FROM            dbo.PatientProfile p
INNER JOIN 
PatientEncounter e
ON
e.PatientMasterVisitId= p.PatientMasterVisitId
WHERE e.EncounterTypeId IN(SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='maternity-encounter')
AND
e.DeleteFlag=0 AND p.DeleteFlag=0
GO




