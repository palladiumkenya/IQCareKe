

/****** Object:  View [dbo].[PatientAllergyView]    Script Date: 09/10/2019 15:27:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientAllergyView]
AS
SELECT 
Id,
PatientId,
PatientMasterVisitId,
Allergen,
AllergenName = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = Allergen AND MasterName = 'Allergies'),
DeleteFlag,
CreateBy,
CreateDate,
Reaction,
ReactionName = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = Reaction AND MasterName = 'AllergyReactions'),
Severity,
SeverityName = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = Severity AND MasterName = 'ADRSeverity'),
OnsetDate

FROM PatientAllergy
where DeleteFlag <> 1
GO


