IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonKinContactsView]'))
DROP VIEW [dbo].[PersonKinContactsView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonKinContactsView]
AS
SELECT 
PTS.Id, 
PTS.PersonId, 
PTS.SupporterId, 
PTS.DeleteFlag, 
PTS.ContactCategory, 
PTS.ContactRelationship, 
CAST(DECRYPTBYKEY(P.FirstName) AS VARCHAR(50)) FirstName,
CAST(DECRYPTBYKEY(P.MidName) AS VARCHAR(50)) MiddleName,
CAST(DECRYPTBYKEY(p.LastName) AS VARCHAR(50)) LastName,
CAST(DECRYPTBYKEY(PC.mobileNo) AS VARCHAR(50)) MobileNo   
FROM dbo.PatientTreatmentSupporter PTS
LEFT JOIN dbo.PatientContact PC ON PTS.Id = PC.id 
LEFT JOIN dbo.Person P ON PTS.SupporterId = P.Id

GO