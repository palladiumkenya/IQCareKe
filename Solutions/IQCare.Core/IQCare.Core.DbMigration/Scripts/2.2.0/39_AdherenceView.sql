IF OBJECT_ID('dbo.AdherenceView', 'V') IS NOT NULL
    DROP VIEW [dbo].[AdherenceView]
GO

CREATE VIEW AdherenceView as

select ao.Id,ao.PatientId,ao.PatientMasterVisitId,ao.Score,
ao.AdherenceType,lm.[Name] as AdherenceTypeName, lti.DisplayName as ScoreName ,ao.DeleteFlag,pmv.VisitDate from AdherenceOutcome  ao
inner join  LookupMaster  lm on lm.Id=ao.AdherenceType
inner join LookupItem lti on lti.Id=ao.Score
inner join PatientMasterVisit pmv on pmv.Id =ao.PatientMasterVisitId
WHERE lm.[Name]='PrepAdherence'


