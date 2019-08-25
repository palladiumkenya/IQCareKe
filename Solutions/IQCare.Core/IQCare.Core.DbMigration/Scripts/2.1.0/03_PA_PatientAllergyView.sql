IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientAllergyView]'))
DROP VIEW [dbo].[PatientAllergyView]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientAllergyView]
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
GO
