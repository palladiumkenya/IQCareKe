
with PatientARTFastTrack as (select distinct PatientMasterVisitId,PatientId,CreateDate from PatientArtDistribution)


INSERT INTO PatientEncounter (PatientId,EncounterTypeId,PatientMasterVisitId,EncounterStartTime,EncounterEndTime,ServiceAreaId,DeleteFlag,CreatedBy,CreateDate,[Status])
select t.PatientId,t.EncounterTypeId,t.PatientMasterVisitId,t.EncounterStartTime,t.EncounterEndTime,t.ServiceAreaId,t.DeleteFlag,t.CreatedBy,t.CreateDate,t.[Status] from(
Select pat.PatientId,
(select ItemId from LookupItemView lm where lm.MasterName='EncounterType' and lm.ItemName='ARTFastTrack') as EncounterTypeId
,pat.PatientMasterVisitId 
,pmv.VisitDate as EncounterStartTime
,pmv.VisitDate as EncounterEndTime
,(select ModuleId from mst_module where ModuleName='CCC Patient Card MoH 257') as ServiceAreaId
,0 as DeleteFlag
,'1' as CreatedBy
,pat.CreateDate
,0 as [Status]
 from PatientARTFastTrack pat
 inner join PatientMasterVisit  pmv on pmv.Id=pat.PatientMasterVisitId
 )t
WHERE NOT EXISTS(
select * from PatientEncounter pe where pe.PatientId=t.PatientId and pe.PatientMasterVisitId=t.PatientMasterVisitId
and pe.EncounterTypeId=t.EncounterTypeId)
 


