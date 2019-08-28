if  Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='SexualPartnersHivRisk' and lm.Name='ClientsBehaviourRiskAssessment')
BEGIN
DELETE l FROM LookupMasterItem l 
inner join LookupMaster lm on lm.Id=l.LookupMasterId 
inner join LookupItem lit on lit.Id=l.LookupItemId
where 
lit.Name='SexualPartnersHivRisk' and lm.Name='ClientsBehaviourRiskAssessment'

END
