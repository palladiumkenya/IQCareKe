
IF  OBJECT_ID('OVC_CaregiverView', 'V') IS NOT NULL
    DROP VIEW [OVC_CaregiverView]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OVC_CaregiverView]
AS
SELECT
	ISNULL(ROW_NUMBER() OVER(ORDER BY PR.Id ASC), -1) AS RowID,
	PR.PersonId,
	PR.PatientId,
	CAST(DECRYPTBYKEY(P.[FirstName]) AS VARCHAR(50)) AS [FirstName],
	CAST(DECRYPTBYKEY(P.[MidName]) AS VARCHAR(50)) AS [MidName],
	CAST(DECRYPTBYKEY(P.[LastName]) AS VARCHAR(50)) AS [LastName],
	P.DateOfBirth,
	P.Sex,
	Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'),
	PR.RelationshipTypeId,
	RelationshipType = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PR.RelationshipTypeId AND MasterName = 'CaregiverRelationship')

FROM [dbo].[PersonRelationship] PR
INNER JOIN dbo.Patient AS PT ON PT.Id = PR.PatientId
INNER JOIN [dbo].[Person] P ON P.Id = PR.PersonId





