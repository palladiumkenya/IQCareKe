IF OBJECT_ID('dbo.PrepRiskAssessmentEncounterView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PrepRiskAssessmentEncounterView]
GO
 
CREATE VIEW  [dbo].[PrepRiskAssessmentEncounterView]
AS
	select pe.Id,p.PersonId,pe.PatientId,pe.EncounterTypeId,pmv.VisitDate,pe.PatientMasterVisitId,pe.EncounterStartTime as [Start]
,pe.EncounterEndTime as [End]
,sva.[Name] as [ServiceAreaName],pe.ServiceAreaId as ServiceAreaId,pe.DeleteFlag
,cwp.ItemName as [ClientWillingTakingPrep] ,aout.ItemName as [AssessmentOutCome]
from PatientEncounter pe inner join
LookupItemView ltv on ltv.ItemId=pe.EncounterTypeId
left join ServiceArea sva on sva.Id=pe.ServiceAreaId
inner join PatientMasterVisit pmv on pmv.Id=pe.PatientMasterVisitId
left join Patient p on p.Id=pe.PatientId
left join (select rs.Id,rs.PatientId,rs.PatientMasterVisitId,liv.MasterName,rs.RiskAssessmentId,rs.RiskAssessmentValue,liv.ItemName,rs.DeleteFlag from RiskAssessment rs
inner join (select  Id,[Name]  from LookupMaster where [Name]='ClientWillingTakePrep') rd  on rd.Id=rs.RiskAssessmentId
inner join LookupItemView liv on liv.ItemId=rs.RiskAssessmentValue
and liv.MasterId=rd.Id
where rs.DeleteFlag= 0)  cwp on cwp.PatientMasterVisitId=pe.PatientMasterVisitId 
and cwp.PatientId=pe.PatientId
left join (select rs.Id,rs.PatientId,rs.PatientMasterVisitId,liv.MasterName,rs.RiskAssessmentId,rs.RiskAssessmentValue,liv.ItemName,rs.DeleteFlag from RiskAssessment rs
inner join (select  Id,[Name]  from LookupMaster where [Name]='AssessmentOutcome') rd  on rd.Id=rs.RiskAssessmentId
inner join LookupItemView liv on liv.ItemId=rs.RiskAssessmentValue
and liv.MasterId=rd.Id
where rs.DeleteFlag= 0)  aout on aout.PatientMasterVisitId=pe.PatientMasterVisitId 
and aout.PatientId=pe.PatientId
where ltv.ItemName='PrepRiskAssessment-encounter'







