with PatientCareEnded as (select distinct pc.PatientId,pc.PatientMasterVisitId,pc.CreateDate,pc.CreatedBy,pmv.VisitDate,pmv.[Start],pmv.[End]
 from PatientCareending pc
 inner join PatientMasterVisit pmv on pmv.Id= pc.PatientMasterVisitId)
 

 INSERT INTO PatientEncounter (PatientId,EncounterTypeId,PatientMasterVisitId,EncounterStartTime,EncounterEndTime,ServiceAreaId,DeleteFlag,CreatedBy,CreateDate,[Status])
select t.PatientId,t.EncounterTypeId,t.PatientMasterVisitId,t.EncounterStartTime,t.EncounterEndTime,t.ServiceAreaId,t.DeleteFlag,t.CreatedBy,t.CreateDate,t.[Status] from(
Select pat.PatientId,
(select ItemId from LookupItemView lm where lm.MasterName='EncounterType' and lm.ItemName='CareEnded') as EncounterTypeId
,pat.PatientMasterVisitId 
,CASE WHEN pmv.VisitDate is null then pmv.Start else pmv.VisitDate end as EncounterStartTime
,CASE WHEN pmv.VisitDate is null then pmv.Start else pmv.VisitDate end  as EncounterEndTime
,(select ModuleId from mst_module where ModuleName='CCC Patient Card MoH 257') as ServiceAreaId
,0 as DeleteFlag
,'1' as CreatedBy
,pat.CreateDate
,0 as [Status]
 from PatientCareEnded  pat
 inner join PatientMasterVisit  pmv on pmv.Id=pat.PatientMasterVisitId
 )t
WHERE NOT EXISTS(
select * from PatientEncounter pe where pe.PatientId=t.PatientId and pe.PatientMasterVisitId=t.PatientMasterVisitId
and pe.EncounterTypeId=t.EncounterTypeId)