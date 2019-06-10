IF OBJECT_ID('dbo.PrepRiskAssessmentEncounterView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PrepRiskAssessmentEncounterView]
GO
 
CREATE VIEW  [dbo].[PrepRiskAssessmentEncounterView]
AS
select pe.Id,p.PersonId,pe.PatientId,pe.EncounterTypeId,pmv.VisitDate,pe.PatientMasterVisitId,pe.EncounterStartTime as [Start]
,pe.EncounterEndTime as [End]
,sva.[Name] as [ServiceAreaName],pe.ServiceAreaId as ServiceAreaId,pe.DeleteFlag from PatientEncounter pe inner join
LookupItemView ltv on ltv.ItemId=pe.EncounterTypeId
left join ServiceArea sva on sva.Id=pe.ServiceAreaId
inner join PatientMasterVisit pmv on pmv.Id=pe.PatientMasterVisitId
left join Patient p on p.Id=pe.PatientId
where ltv.ItemName='PrepRiskAssessment-encounter'
