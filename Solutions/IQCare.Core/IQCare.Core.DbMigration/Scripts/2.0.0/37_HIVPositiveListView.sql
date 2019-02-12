IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[HIVPositiveListView]'))
DROP VIEW [dbo].[HIVPositiveListView]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[HIVPositiveListView]
AS
SELECT ISNULL(ROW_NUMBER() OVER(ORDER BY PersonId ASC), -1) AS RowID, *
FROM(
SELECT P.PersonId FROM Patient P
INNER JOIN PatientEnrollment PE ON P.Id = PE.PatientId
WHERE PE.ServiceAreaId = 1
UNION
SELECT HE.PersonId FROM HtsEncounter HE
INNER JOIN HtsEncounterResult HER ON HER.HtsEncounterId = HE.Id
WHERE HER.FinalResult = (SELECT Top 1 ItemId FROM LookupItemView WHERE ItemName = 'Positive' AND MasterName = 'HIVFinalResults')
) HivPositiveList
GO