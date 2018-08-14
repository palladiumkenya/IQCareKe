IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonListView]'))
DROP VIEW [dbo].[PersonListView]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[PersonListView]
AS

SELECT
P.Id,
CAST(DECRYPTBYKEY(P.FirstName) AS VARCHAR(50)) AS FirstName, 
CAST(DECRYPTBYKEY(P.MidName) AS VARCHAR(50)) AS MiddleName,
CAST(DECRYPTBYKEY(P.LastName) AS VARCHAR(50)) AS LastName,
CAST(DECRYPTBYKEY(P.FirstName) AS VARCHAR(50)) + ' ' + CAST(DECRYPTBYKEY(P.MidName) AS VARCHAR(50)) + ' ' + CAST(DECRYPTBYKEY(P.LastName) AS VARCHAR(50)) AS FullName,
P.Sex,
 Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'),
P.DeleteFlag,
P.DateOfBirth,
PI.IdentifierValue
FROM Person P
LEFT JOIN Patient PT ON PT.PersonId = P.Id
LEFT JOIN (SELECT Main.PatientId, LEFT(Main.IdentifierValue,Len(Main.IdentifierValue)-1) As "IdentifierValue"
FROM
    (
        SELECT DISTINCT PI2.PatientId, 
            (
                SELECT IDE.Code + ':' + PI.IdentifierValue + ',' AS [text()]
                FROM [dbo].[PatientIdentifier] PI
				INNER JOIN [dbo].Identifiers IDE ON IDE.Id = PI.IdentifierTypeId
                WHERE PI.PatientId = PI2.PatientId
                ORDER BY PI.PatientId
                FOR XML PATH ('')
            ) [IdentifierValue]
        FROM [dbo].[PatientIdentifier] PI2
    ) [Main]) PI ON PI.PatientId = PT.Id


GO