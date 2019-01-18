
update v
set 
 v.PeriodId=vt.VaccineStage
 from Vaccination v  inner join
 Vaccination  vt on vt.Id=v.Id
LEFT join LookupItemView  lt on lt.ItemId=v.VaccineStage
 where  lt.MasterName='ImmunizationPeriod'
 and ISNUMERIC(v.VaccineStage) = 1


 go


 update v
set 
 v.VaccineStage = ''
 from Vaccination v  inner join
 Vaccination  vt on vt.Id=v.Id
 inner join LookupItemView  lt on lt.ItemId=v.VaccineStage
 where  lt.MasterName='ImmunizationPeriod'
 and ISNUMERIC(v.VaccineStage)=1

 
go







update  v set v.VaccineStageId=lt.ItemId  from Vaccination v inner join Vaccination vt on v.Id=vt.Id
left join  LookupItemView lt on lt.DisplayName=v.VaccineStage
where v.Vaccine <> 0 and lt.MasterName <> 'ImmunizationPeriod'