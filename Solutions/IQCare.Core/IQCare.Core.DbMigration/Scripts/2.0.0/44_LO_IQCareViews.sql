IF OBJECT_ID('dbo.[PatientPopulationView]', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientPopulationView]
GO

CREATE VIEW [dbo].[PatientPopulationView]
AS
     SELECT DISTINCT
            b.ptn_pk AS PatientPK,
            CASE
                WHEN a.PopulationType = 'General Population'
                THEN 'General Population'
                WHEN a.PopulationType = 'Key Population'
                THEN case when c.ItemName in ('SW','PWID','FSW','MSM','Other')then c.ItemName  else 'Other' end 
            END AS PopulationCategory
     FROM dbo.PatientPopulation AS a
          INNER JOIN dbo.Patient AS b ON a.PersonId = b.PersonId
          LEFT OUTER JOIN dbo.LookupItemView AS c ON a.PopulationCategory = c.ItemId
     WHERE(a.DeleteFlag = 0);

GO


IF OBJECT_ID('dbo.[PatientPersonView]', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientPersonView]
GO

CREATE VIEW [dbo].[PatientPersonView]
AS


Select A.Id
	, A.PersonId
	, A.ptn_pk
	, A.PatientIndex
	,(Select Top 1 Name From Lookupitem Where Id = A.PatientType) PatientTypeName
	,A.PatientType
	,A.FacilityId
	  ,cast(decryptbykey(FirstName) As varchar(50)) As FirstName
	  ,cast(decryptbykey(MidName) As varchar(50)) As MiddleName
	  ,cast(decryptbykey(LastName) As varchar(50)) As LastName
	  ,(Select Top 1 Name From Lookupitem Where Id = B.Sex)  SexName
	  , B.Sex
	  ,A.Active
	  ,A.DeleteFlag
	  ,A.CreateDate
	  ,A.CreatedBy
	  ,cast (A.AuditData As varchar(max))AuditData
	  ,Isnull(A.DateOfBirth,B.DateOfBirth) DateOfBirth
	  ,Isnull(A.DobPrecision,B.DobPrecision) DobPrecision
	  ,cast(decryptbykey(A.NationalId) As varchar(50)) As NationalId
	  ,A.RegistrationDate
From Patient A inner join dbo.Person B On A.PersonId=B.Id





GO






