IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PartnersView]'))
DROP VIEW [dbo].[Api_PartnersView]
GO

/****** Object:  View [dbo].[Api_PartnersView]    Script Date: 21-Mar-18 9:32:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Api_PartnersView]
AS
SELECT
	ISNULL(ROW_NUMBER() OVER(ORDER BY PR.Id ASC), -1) AS RowID,
	PR.PersonId,
	PR.PatientId,
	CAST(DECRYPTBYKEY(P.[FirstName]) AS VARCHAR(50)) AS [FirstName],
	CAST(DECRYPTBYKEY(P.[MidName]) AS VARCHAR(50)) AS [MidName],
	CAST(DECRYPTBYKEY(P.[LastName]) AS VARCHAR(50)) AS [LastName],
	PT.DateOfBirth,
	P.Sex,
	Gender = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = P.Sex AND MasterName = 'Gender'),
	PR.RelationshipTypeId,
	RelationshipType = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PR.RelationshipTypeId AND MasterName = 'Relationship')

FROM [dbo].[PersonRelationship] PR
INNER JOIN dbo.Patient AS PT ON PT.Id = PR.PatientId
INNER JOIN [dbo].[Person] P ON P.Id = PR.PersonId


GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientsView]'))
DROP VIEW [dbo].[Api_PatientsView]
GO
/****** Object:  View [dbo].[Api_PatientsView]    Script Date: 21-Mar-18 9:35:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[Api_PatientsView]
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


GO