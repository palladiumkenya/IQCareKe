IF OBJECT_ID('dbo.PersonVitalsView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PersonVitalsView]
GO


CREATE VIEW [dbo].[PersonVitalsView]
AS
SELECT dbo.PatientVitals.id,per.Id as PersonId,
	  dbo.PatientVitals.PatientId
	  ,dbo.PatientVitals.PatientMasterVisitId
	  ,dbo.PatientVitals.Weight
	, dbo.PatientVitals.VisitDate
	,dbo.PatientVitals.CreateDate
From  Person per
left join Patient pat on pat.PersonId=per.Id
inner join dbo.PatientVitals  on dbo.PatientVitals.PatientId=pat.Id


GO


