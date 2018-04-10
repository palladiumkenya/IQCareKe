IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientsView]'))
DROP VIEW [dbo].[Api_PatientsView]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Api_PatientsView]
AS
SELECT 
	   ISNULL(ROW_NUMBER() OVER(ORDER BY PersonId ASC), -1) AS RowID,
	   P.[Id] PersonId,
	   PT.Id PatientId,
	   PT.ptn_pk,
	   CAST(DECRYPTBYKEY(P.[FirstName]) AS VARCHAR(50)) AS [FirstName], 
	   CAST(DECRYPTBYKEY(P.[MidName]) AS VARCHAR(50)) AS [MidName],
	   CAST(DECRYPTBYKEY(P.[LastName]) AS VARCHAR(50)) AS [LastName],
	   P.Sex,
	   Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'),
	   PT.[DateOfBirth],
	   PT.[DobPrecision],
	   PatientType = CASE(SELECT ItemName FROM LookupItemView WHERE ItemId = PT.PatientType AND MasterName = 'PatientType') WHEN 'New' THEN 'NEW' WHEN 'Transfer-In' THEN 'TRANSFER-IN' WHEN 'Transit' THEN 'TRANSIT' ELSE '' END,
	   CAST(DECRYPTBYKEY(PT.[NationalId]) AS VARCHAR(50)) AS [NationalId],
	   [RegistrationDate],
	   PE.EnrollmentDate,
	   pni.IdentifierValue,
	   SE.Id ServiceAreaId,
	   SE.Name ServiceAreaName
	   
FROM [dbo].[Person] P
INNER JOIN dbo.Patient AS PT ON P.Id = PT.PersonId
INNER JOIN dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId 
INNER JOIN dbo.PatientIdentifier AS pni ON pni.PatientId = PT.Id 
INNER JOIN dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id
INNER JOIN dbo.ServiceArea SE ON SE.Id = PE.ServiceAreaId

G0