/****** Object:  View [dbo].[PersonKinContactsView]    Script Date: 12/4/2018 9:45:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[PersonKinContactsView]
AS
SELECT 
PTS.Id, 
PTS.PersonId, 
PTS.SupporterId, 
PTS.DeleteFlag, 
CASE (PTS.ContactCategory)
	WHEN 1
		THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'ContactCategory' AND ItemName = 'TreatmentSupporter')
	ELSE
		PTS.ContactCategory			
END AS ContactCategory, 
PTS.ContactRelationship, 
CAST(DECRYPTBYKEY(P.FirstName) AS VARCHAR(50)) FirstName,
CAST(DECRYPTBYKEY(P.MidName) AS VARCHAR(50)) MiddleName,
CAST(DECRYPTBYKEY(p.LastName) AS VARCHAR(50)) LastName,
P.Sex,
CAST(DECRYPTBYKEY(PC.MobileNumber) AS VARCHAR(50)) MobileNo,
PS.ConsentType,
PS.ConsentValue,
PS.Comments

FROM dbo.PatientTreatmentSupporter PTS
LEFT JOIN dbo.PersonContact PC ON PTS.SupporterId = PC.PersonId
LEFT JOIN dbo.Person P ON PTS.SupporterId = P.Id
LEFT JOIN dbo.PatientConsent PS on PS.PersonId = PTS.SupporterId

GO


