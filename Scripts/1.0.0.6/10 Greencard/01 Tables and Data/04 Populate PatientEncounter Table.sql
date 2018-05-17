--------Triage
declare @TriageEncounter varchar(50) = (select top 1 Id from lookupitem where Name = 'Triage-encounter');
insert into PatientEncounter 
select a.patientid, @TriageEncounter, a.PatientMasterVisitId, a.createdate, a.createdate, 203, 0, 1, a.createdate, null, 0 
from PatientVitals a where not exists (select 1 from PatientEncounter where PatientId = a.patientid and encountertypeid=@TriageEncounter and patientmastervisitid = a.PatientMasterVisitId)

--------clinical
declare @clinicalEncounter varchar(50) = (select top 1 Id from lookupitem where Name = 'ccc-encounter');
insert into PatientEncounter 
select a.patientid, @clinicalEncounter, a.Id, a.Start, a.Start, 203, 0, 1, a.createdate, null, 0 
from patientmastervisit a where visitby > 0 and not exists (select 1 from PatientEncounter where PatientId = a.patientid and encountertypeid=@clinicalEncounter and patientmastervisitid = a.Id)

-------lab
declare @LabEncounter varchar(50) = (select top 1 Id from lookupitem where Name = 'Lab-encounter')
insert into PatientEncounter
select pmv.PatientId, @LabEncounter, pmv.Id, pmv.start, pmv.start, 205, 0, 1, ord.createdate, null,0 
from ord_laborder ord inner join patientmastervisit pmv on ord.PatientMasterVisitId = pmv.id
where ord.patientmastervisitid is not null and not exists (select 1 from PatientEncounter where PatientId = pmv.PatientId and encountertypeid=@LabEncounter and patientmastervisitid = pmv.Id)

-------pharmacy
declare @PharmacyEncounter varchar(50) = (select top 1 Id from lookupitem where Name = 'Pharmacy-encounter')
insert into PatientEncounter
select pmv.PatientId, @PharmacyEncounter, pmv.Id, pmv.start, pmv.start, 204, 0, 1, ord.createdate, null,0
from ord_patientpharmacyorder ord inner join patientmastervisit pmv on ord.PatientMasterVisitId = pmv.id
where patientmastervisitid is not null and not exists (select 1 from PatientEncounter where PatientId = pmv.PatientId and encountertypeid=@PharmacyEncounter and patientmastervisitid = pmv.Id)