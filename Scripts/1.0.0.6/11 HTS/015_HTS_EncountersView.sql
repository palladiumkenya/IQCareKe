IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[HTS_EncountersView]'))
	DROP VIEW [dbo].[HTS_EncountersView]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[HTS_EncountersView]
AS
SELECT DISTINCT
ISNULL(ROW_NUMBER() OVER(ORDER BY PE.Id ASC), -1) AS RowID,
HE.Id EncounterId,
PE.Id,
PE.PatientId,
PE.EncounterStartTime EncounterDate,
ISNULL(CAST((CASE HE.EncounterType WHEN 1 THEN 'Initial Test' WHEN 2 THEN 'Repeat Test' END) AS VARCHAR(50)),'Initial') AS TestType,
Provider = (SELECT TOP 1 (UserFirstName + ' ' + UserLastName) FROM [dbo].[mst_User] WHERE UserID = HE.ProviderId),
ResultOne = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 RoundOneTestResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
ResultTwo = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 RoundTwoTestResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
FinalResult = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 FinalResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
Consent = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 ConsentValue FROM PatientConsent PC WHERE PC.PatientMasterVisitId = PM.Id AND PC.ConsentType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE ItemName = 'ConsentToBeTested') order by Id DESC)),
PartnerListingConsent = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 ConsentValue FROM PatientConsent PC WHERE PC.PatientMasterVisitId = PM.Id AND PC.ConsentType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE ItemName = 'ConsentToListPartners') order by Id DESC))

FROM [dbo].[PatientEncounter] PE
INNER JOIN [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId
INNER JOIN [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID