ALTER VIEW [dbo].[HTS_EncountersDetailView]
AS
SELECT DISTINCT
ISNULL(ROW_NUMBER() OVER(ORDER BY PE.Id ASC), -1) AS RowID,
HE.PersonId PersonId,
HE.Id EncounterId,
PE.Id PatientEncounterId,
PE.PatientId PatientId,
PE.EncounterStartTime EncounterDate,
ISNULL(CAST((CASE HE.EncounterType WHEN 1 THEN 'Initial Test' WHEN 2 THEN 'Repeat Test' END) AS VARCHAR(50)),'Initial') AS TestType,
Provider = (SELECT TOP 1 (UserFirstName + ' ' + UserLastName) FROM [dbo].[mst_User] WHERE UserID = HE.ProviderId),
ResultOne = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 RoundOneTestResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
ResultTwo = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 RoundTwoTestResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
FinalResult = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 FinalResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
SyphilisResult = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 SyphilisResult FROM [dbo].[HtsEncounterResult] WHERE HtsEncounterId = HE.Id ORDER BY Id DESC)),
Consent = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 ConsentValue FROM PatientConsent PC WHERE PC.PatientMasterVisitId = PM.Id AND PC.ConsentType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE ItemName = 'ConsentToBeTested') order by Id DESC)),
PartnerListingConsent = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 ConsentValue FROM PatientConsent PC WHERE PC.PatientMasterVisitId = PM.Id AND PC.ConsentType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE ItemName = 'ConsentToListPartners') order by Id DESC)),
PartnerListingConsentDeclineReason = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE MasterName = 'ReasonsPartner' AND ItemId = (SELECT TOP 1 DeclineReason FROM PatientConsent PC WHERE PC.PatientMasterVisitId = PM.Id AND PC.ConsentType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE ItemName = 'ConsentToListPartners') order by Id DESC)),
ServiceEntryPoint = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.TestEntryPoint),
EverTested = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.EverTested),
HE.MonthsSinceLastTest MonthsSinceLastTest,
EverSelfTested = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.EverSelfTested),
TestedAs = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.TestedAs),
CoupleDiscordant = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.CoupleDiscordant),
HE.EncounterRemarks EncounterRemarks,
FinalResultGiven = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.FinalResultGiven),
TestingStrategy = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = HE.TestingStrategy),
TBScreening = (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = (SELECT TOP 1 ScreeningValueId FROM PatientScreening PTS WHERE PTS.PatientMasterVisitId = PM.Id AND PTS.ScreeningTypeId = (SELECT TOP 1 MasterId FROM LookupItemView WHERE MasterName = 'TbScreening')))

FROM [dbo].[PatientEncounter] PE
INNER JOIN [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId
INNER JOIN [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID


