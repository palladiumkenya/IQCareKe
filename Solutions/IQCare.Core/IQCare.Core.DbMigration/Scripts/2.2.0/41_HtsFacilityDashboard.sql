CREATE VIEW [dbo].[HtsFacilityDashboard]
AS
SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Id,
(SELECT COUNT(*) TotalTested FROM (
SELECT DISTINCT HE.PersonId from HtsEncounter HE
INNER JOIN HtsEncounterResult HER ON HER.HtsEncounterId = HE.Id
) AS T1) AS TotalTested,
(SELECT COUNT(*)  FROM (select DISTINCT HE.PersonId from HtsEncounter HE
INNER JOIN HtsEncounterResult HER ON HER.HtsEncounterId = HE.Id
WHERE HER.FinalResult = (SELECT ItemId FROM LookupItemView WHERE ItemName = 'Positive' AND MasterName = 'HIVFinalResults')) AS T) AS TotalPositive,
(SELECT COUNT(*) FROM(SELECT DISTINCT PersonId FROM PatientLinkage) AS T2) AS TotalPositiveWithLinkageForm,
(SELECT COUNT(*) FROM(SELECT P.* FROM Patient P
INNER JOIN PatientIdentifier PI ON PI.PatientId = P.Id
WHERE PI.IdentifierTypeId = 1 AND P.PersonId IN (
select DISTINCT HE.PersonId from HtsEncounter HE
INNER JOIN HtsEncounterResult HER ON HER.HtsEncounterId = HE.Id
WHERE HER.FinalResult = (SELECT ItemId FROM LookupItemView WHERE ItemName = 'Positive' AND MasterName = 'HIVFinalResults'))) as z) as TotalPositiveAndEnrolledInCCC