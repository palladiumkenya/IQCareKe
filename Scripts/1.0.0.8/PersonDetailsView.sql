IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonDetailsView]'))
DROP VIEW [dbo].[PersonDetailsView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[PersonDetailsView]
AS

SELECT        
P.Id, 
CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) AS FirstName, 
CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) AS MiddleName, 
CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) AS LastName,
Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'),
MaritalStatus = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PM.MaritalStatusId AND MasterName = 'MaritalStatus'),
EducationLevel = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PE.EducationLevel AND MasterName = 'EducationalLevel'),
Occupation = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PO.Occupation AND MasterName = 'Occupation'),
County = (SELECT TOP 1 CountyName FROM County WHERE WardId = PL.Ward),
SubCounty = (SELECT TOP 1 Subcountyname FROM County WHERE WardId = PL.Ward),
Ward = (SELECT TOP 1 WardName FROM County WHERE WardId = PL.Ward),
PL.LandMark as Village,
PL.NearestHealthCentre,
P.DateOfBirth, 
P.DobPrecision,
CAST(DECRYPTBYKEY(PC.MobileNumber) AS VARCHAR(50)) AS MobileNumber,
CAST(DECRYPTBYKEY(PC.AlternativeNumber) AS VARCHAR(50)) AS AlternativeNumber,
CAST(DECRYPTBYKEY(PC.EmailAddress) AS VARCHAR(50)) AS EmailAddress
FROM  dbo.Person AS P
LEFT JOIN Patient PT ON PT.PersonId = P.Id
LEFT JOIN PatientMaritalStatus PM ON PM.PersonId = P.Id
LEFT JOIN PersonEducation PE ON PE.PersonId = P.Id
LEFT JOIN PersonOccupation PO ON PO.PersonId = P.Id
LEFT JOIN PersonLocation PL ON PL.PersonId = P.Id
LEFT JOIN PersonContact PC ON PC.PersonId = P.Id


GO