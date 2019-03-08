/****** Object:  View [dbo].[PersonListView]    Script Date: 2/26/2019 9:21:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[PersonListView]
AS

SELECT
P.Id,
PT.Id PatientId,
CAST(DECRYPTBYKEY(P.FirstName) AS VARCHAR(50)) AS FirstName, 
CAST(DECRYPTBYKEY(P.MidName) AS VARCHAR(50)) AS MiddleName,
CAST(DECRYPTBYKEY(P.LastName) AS VARCHAR(50)) AS LastName,
CAST(DECRYPTBYKEY(P.FirstName) AS VARCHAR(50)) + ' ' + CAST(DECRYPTBYKEY(P.MidName) AS VARCHAR(50)) + ' ' + CAST(DECRYPTBYKEY(P.LastName) AS VARCHAR(50)) AS FullName,
P.Sex,
 Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'),
P.DeleteFlag,
ISNULL(P.DateOfBirth, PT.DateOfBirth) AS DateOfBirth,
PI.IdentifierValue,
CAST(DECRYPTBYKEY(PC.MobileNumber) AS VARCHAR(50)) AS MobileNumber

FROM Person P
LEFT JOIN Patient PT ON PT.PersonId = P.Id
LEFT JOIN PersonContact PC ON PC.PersonId = P.Id 
LEFT JOIN (SELECT Main.PatientId, LEFT(Main.IdentifierValue,Len(Main.IdentifierValue)-1) As "IdentifierValue"
FROM
    (
        SELECT DISTINCT PI2.PatientId, 
            (
                SELECT 
				IDE.Code + ': ' 
				+ PI.IdentifierValue + ', Enrollment Date: ' 
				+ convert(varchar, PE.EnrollmentDate, 106) 
				+ ', PatientStatus: ' + ISNULL(CAST(( CASE PE.CareEnded WHEN 0 THEN 'Active' WHEN 1 THEN 'CareEnded' ELSE 'Not Enrolled' END) AS VARCHAR(50)), 'Active')  AS [text()]
                FROM [dbo].[PatientIdentifier] PI
				INNER JOIN [dbo].Identifiers IDE ON IDE.Id = PI.IdentifierTypeId
				LEFT JOIN [dbo].PatientEnrollment PE ON PI.PatientEnrollmentId = PE.Id
                WHERE PI.PatientId = PI2.PatientId
                ORDER BY PI.PatientId
                FOR XML PATH ('br')
            ) [IdentifierValue]
        FROM [dbo].[PatientIdentifier] PI2
    ) [Main]) PI ON PI.PatientId = PT.Id


GO


