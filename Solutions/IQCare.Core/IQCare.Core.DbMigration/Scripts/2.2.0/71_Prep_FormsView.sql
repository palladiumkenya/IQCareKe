
IF OBJECT_ID('dbo.PREP_FormsView', 'V') IS NOT NULL
BEGIN
    DROP VIEW [dbo].[PREP_FormsView]
END
GO


CREATE VIEW  [dbo].[PREP_FormsView]
AS

 SELECT ISNULL(ROW_NUMBER() OVER(ORDER BY prep.PatientMasterVisitId ASC), -1) AS RowID,* from (
 SELECT distinct 0 as EncounterId,pmv.PatientId,'Initiation' as Form,pmv.Id as PatientMasterVisitId,CONVERT(date,pmv.VisitDate) as VisitDate ,pmv.VisitType,lt.DisplayName,pe.EnrollmentDate,paa.AppointmentDate,paa.AppointmentReason,rev.AssessmentOutCome,rev.ClientWillingTakingPrep
,pvv.[Weight],prst.PrepStatusToday,prst.SignsOrSymptomsHiv,ps.ScreeningValue as Contraindications from PatientMasterVisit pmv
inner join LookupItem lt on lt.Id=pmv.VisitType
inner join ServiceArea sa on sa.Id=pmv.ServiceId
left join(select pe.PatientId,pe.ServiceAreaId,pe.EnrollmentDate from(select pe.PatientId,pe.ServiceAreaId,pe.EnrollmentDate,pe.DeleteFlag,ROW_NUMBER() OVER(partition by pe.PatientId order by pe.CreateDate desc)rownum from PatientEnrollment pe 
inner join ServiceArea sa on sa.Id=pe.ServiceAreaId 
where sa.[Code]= 'PREP'
and (pe.DeleteFlag is null or pe.DeleteFlag =0))pe where pe.rownum ='1')pe on pe.PatientId =pmv.PatientId
inner join (select pa.PatientId,pa.PatientMasterVisitId,pa.Code,pa.AppointmentDate,pa.AppointmentReason,pa.DeleteFlag from (select pa.PatientId,pa.PatientMasterVisitId,sa.Code,pa.AppointmentDate,pa.ServiceAreaId,lta.[Name] as AppointmentReason
 ,ROW_NUMBER() OVER(partition by  pa.PatientMasterVisitId , pa.PatientId order by pa.PatientId desc)rownum,pa.DeleteFlag,pa.CreateDate,pa.[Description] from PatientAppointment pa  
inner join LookupItem lta on lta.Id=pa.ReasonId
inner join ServiceArea sa on sa.Id =pa.ServiceAreaId
where sa.Code='PREP' and (pa.DeleteFlag is null or pa.DeleteFlag =0))pa where pa.rownum=1
)paa on paa.PatientMasterVisitId=pmv.Id
inner join PrepRiskAssessmentEncounterView rev on rev.PatientId=pmv.PatientId and rev.VisitDate=CONVERT(date,pmv.VisitDate)
left join  (select pvv.PatientId,pvv.VisitDate,pvv.[Weight],pvv.PatientMasterVisitId from (select pv.PatientId,pv.PatientMasterVisitId,pv.Height,pv.Weight,pmv.VisitDate,pmv.DeleteFlag,ROW_NUMBER() over(
PARTITION BY pv.PatientId,pmv.VisitDate order by pv.PatientMasterVisitId,pv.CreateDate desc)rownum
 from PatientVitals pv
inner join PatientMasterVisit pmv on pmv.id=pv.PatientMasterVisitId 
where pv.DeleteFlag is null or pv.DeleteFlag =0
)pvv where pvv.rownum=1)pvv  on pvv.PatientId =paa.PatientId and CONVERT(date,pvv.VisitDate)=CONVERT(date,pmv.VisitDate)
left join (select  prs.PatientId,prs.PatientMasterVisitId,prs.SignsOrSymptomsHiv,prs.VisitDate,prs.PrepStatusToday from (select ps.PatientId,ps.PatientEncounterId,pmv.VisitDate,pmv.Id as PatientMasterVisitId,lts.DisplayName as SignsOrSymptomsHiv,lti.DisplayName as PrepStatusToday,ps.DateField,ROW_NUMBER()
OVER(Partition by ps.PatientId,pmv.VisitDate order by pmv.Id desc)rownum
 from PatientPrEPStatus ps
  inner join LookupItem lti on lti.Id=ps.PrepStatusToday
  inner join LookupItem lts on lts.Id=ps.SignsOrSymptomsHIV
 inner join PatientMasterVisit pmv on pmv.Id=ps.PatientEncounterId
 )prs where prs.rownum =1)prst on prst.PatientId=pmv.PatientId and CONVERT(date,prst.VisitDate) =CONVERT(date,pmv.VisitDate)
 left join(SELECT  distinct ps.PatientMasterVisitId,ps.PatientId,ps.VisitDate,
    SUBSTRING(
        (
            SELECT distinct ','+li.DisplayName  AS [text()]
            FROM PatientScreening ST1
			left join LookupItemView li on li.ItemId=ST1.ScreeningValueId
			and li.MasterId =ST1.ScreeningCategoryId
            WHERE ST1.PatientId = ps.PatientId
			and ST1.PatientMasterVisitId=ps.PatientMasterVisitId
			and ST1.DeleteFlag = ps.DeleteFlag
			and li.MasterName='ContraindicationsPrEP'
			    FOR XML PATH ('')
        ), 2, 1000) [ScreeningValue]
FROM PatientScreening ps
left join LookupItemView li on li.ItemId=ps.ScreeningValueId
			and li.MasterId =ps.ScreeningCategoryId
where ps.DeleteFlag =0 
and li.MasterName='ContraindicationsPrEP'

)ps on CONVERT(date,ps.VisitDate) = CONVERT(date,pmv.VisitDate) and  ps.PatientId=pmv.PatientId 
where lt.DisplayName='Enrollment' and sa.[Name]='PREP'
union all


select distinct pe.Id as EncounterId, pmv.PatientId,'MonthlyRefill' as Form,pmv.Id as PatientMasterVisitId,CONVERT(date,pmv.VisitDate) as VisitDate ,pmv.VisitType,lt.DisplayName,NULL as EnrollmentDate,paa.AppointmentDate,paa.AppointmentReason,
rev.AssessmentOutCome,rev.ClientWillingTakingPrep
,pvv.[Weight],prepst.PrepStatus    PrepStatusToday,NULL as SignsOrSymptomsHiv,NULL as Contraindications 
from PatientEncounter pe
inner join PatientMasterVisit pmv on pmv.Id=pe.PatientMasterVisitId and pmv.PatientId =pe.PatientId
inner join LookupItem lt on lt.Id=pe.EncounterTypeId
inner join ServiceArea sa on sa.Id=pmv.ServiceId

left join (select pa.PatientId,pa.PatientMasterVisitId,pa.Code,pa.AppointmentDate,pa.AppointmentReason,pa.DeleteFlag from (select pa.PatientId,pa.PatientMasterVisitId,sa.Code,pa.AppointmentDate,pa.ServiceAreaId,lta.[Name] as AppointmentReason
 ,ROW_NUMBER() OVER(partition by  pa.PatientMasterVisitId , pa.PatientId order by pa.PatientId desc)rownum,pa.DeleteFlag,pa.CreateDate,pa.[Description] from PatientAppointment pa  
inner join LookupItem lta on lta.Id=pa.ReasonId
inner join ServiceArea sa on sa.Id =pa.ServiceAreaId
where sa.Code='PREP' and (pa.DeleteFlag is null or pa.DeleteFlag =0))pa where pa.rownum=1
)paa on paa.PatientMasterVisitId=pmv.Id

left join PrepRiskAssessmentEncounterView rev on rev.PatientId=pmv.PatientId and rev.VisitDate=CONVERT(date,pmv.VisitDate)
left join  (select pvv.PatientId,pvv.VisitDate,pvv.[Weight],pvv.PatientMasterVisitId from (select pv.PatientId,pv.PatientMasterVisitId,pv.Height,pv.Weight,pmv.VisitDate,pmv.DeleteFlag,ROW_NUMBER() over(
PARTITION BY pv.PatientId,pmv.VisitDate order by pv.PatientMasterVisitId,pv.CreateDate desc)rownum
 from PatientVitals pv
inner join PatientMasterVisit pmv on pmv.id=pv.PatientMasterVisitId 
where pv.DeleteFlag is null or pv.DeleteFlag =0
)pvv where pvv.rownum=1)pvv  on pvv.PatientId =paa.PatientId and CONVERT(date,pvv.VisitDate)=CONVERT(date,pmv.VisitDate)

left join (select prepst.PatientId,prepst.PatientMasterVisitId,prepst.VisitDate,prepst.ItemDisplayName as PrepStatus from (select psc.Id,psc.PatientId,psc.PatientMasterVisitId,psc.ScreeningTypeId,
psc.VisitDate,psc.ScreeningValueId,ltv.ItemDisplayName,ROW_NUMBER() OVER(partition by psc.PatientId,psc.PatientMasterVisitId order by psc.PatientMasterVisitId,psc.CreateDate desc) rownum from PatientScreening psc 
inner join LookupItemView ltv on ltv.ItemId=psc.ScreeningValueId and ltv.MasterId=psc.ScreeningTypeId
where ltv.MasterName='RefillPrepStatus')prepst where prepst.rownum='1') prepst
on prepst.PatientId=paa.PatientId and CONVERT(date,prepst.VisitDate) =CONVERT(date,pmv.VisitDate)
where lt.DisplayName='MonthlyRefill-encounter' and sa.[Name]='PREP'
union all




select distinct  pe.Id as EncounterId,pmv.PatientId,'PrepEncounter' as Form,pmv.Id as PatientMasterVisitId,CONVERT(date,pmv.VisitDate) as VisitDate ,pmv.VisitType,lt.DisplayName,NULL as EnrollmentDate,paa.AppointmentDate,paa.AppointmentReason,
rev.AssessmentOutCome,rev.ClientWillingTakingPrep
,pvv.[Weight],prst.PrepStatusToday,prst.SignsOrSymptomsHiv,ps.ScreeningValue as Contraindications 
from PatientEncounter pe
inner join PatientMasterVisit pmv on pmv.Id=pe.PatientMasterVisitId and pmv.PatientId =pe.PatientId
inner join LookupItem lt on lt.Id=pe.EncounterTypeId
inner join ServiceArea sa on sa.Id=pmv.ServiceId

left join (select pa.PatientId,pa.PatientMasterVisitId,pa.Code,pa.AppointmentDate,pa.AppointmentReason,pa.DeleteFlag from (select pa.PatientId,pa.PatientMasterVisitId,sa.Code,pa.AppointmentDate,pa.ServiceAreaId,lta.[Name] as AppointmentReason
 ,ROW_NUMBER() OVER(partition by  pa.PatientMasterVisitId , pa.PatientId order by pa.PatientId desc)rownum,pa.DeleteFlag,pa.CreateDate,pa.[Description] from PatientAppointment pa  
inner join LookupItem lta on lta.Id=pa.ReasonId
inner join ServiceArea sa on sa.Id =pa.ServiceAreaId
where sa.Code='PREP' and (pa.DeleteFlag is null or pa.DeleteFlag =0))pa where pa.rownum=1
)paa on paa.PatientMasterVisitId=pmv.Id

left join PrepRiskAssessmentEncounterView rev on rev.PatientId=pmv.PatientId and rev.VisitDate=CONVERT(date,pmv.VisitDate)
left join  (select pvv.PatientId,pvv.VisitDate,pvv.[Weight],pvv.PatientMasterVisitId from (select pv.PatientId,pv.PatientMasterVisitId,pv.Height,pv.Weight,pmv.VisitDate,pmv.DeleteFlag,ROW_NUMBER() over(
PARTITION BY pv.PatientId,pmv.VisitDate order by pv.PatientMasterVisitId,pv.CreateDate desc)rownum
 from PatientVitals pv
inner join PatientMasterVisit pmv on pmv.id=pv.PatientMasterVisitId 
where pv.DeleteFlag is null or pv.DeleteFlag =0
)pvv where pvv.rownum=1)pvv  on pvv.PatientId =paa.PatientId and CONVERT(date,pvv.VisitDate)=CONVERT(date,pmv.VisitDate)
left join (select  prs.PatientId,prs.PatientMasterVisitId,prs.SignsOrSymptomsHiv,prs.VisitDate,prs.PrepStatusToday from (select ps.PatientId,ps.PatientEncounterId,pmv.VisitDate,pmv.Id as PatientMasterVisitId,lts.DisplayName as SignsOrSymptomsHiv,lti.DisplayName as PrepStatusToday,ps.DateField,ROW_NUMBER()
OVER(Partition by ps.PatientId,pmv.VisitDate order by pmv.Id desc)rownum
 from PatientPrEPStatus ps
  inner join LookupItem lti on lti.Id=ps.PrepStatusToday
  inner join LookupItem lts on lts.Id=ps.SignsOrSymptomsHIV
  inner join PatientEncounter pe on pe.Id =ps.PatientEncounterId
 inner join PatientMasterVisit pmv on pmv.Id=pe.PatientMasterVisitId
 
 )prs where prs.rownum =1)prst on prst.PatientId=pmv.PatientId and CONVERT(date,prst.VisitDate) =CONVERT(date,pmv.VisitDate)
 left join(SELECT  distinct ps.PatientMasterVisitId,ps.PatientId,ps.VisitDate,
    SUBSTRING(
        (
            SELECT distinct ','+li.DisplayName  AS [text()]
            FROM PatientScreening ST1
			left join LookupItemView li on li.ItemId=ST1.ScreeningValueId
			and li.MasterId =ST1.ScreeningCategoryId
            WHERE ST1.PatientId = ps.PatientId
			and ST1.PatientMasterVisitId=ps.PatientMasterVisitId
			and ST1.DeleteFlag = ps.DeleteFlag
			and li.MasterName='ContraindicationsPrEP'
			    FOR XML PATH ('')
        ), 2, 1000) [ScreeningValue]
FROM PatientScreening ps
left join LookupItemView li on li.ItemId=ps.ScreeningValueId
			and li.MasterId =ps.ScreeningCategoryId
where ps.DeleteFlag =0 
and li.MasterName='ContraindicationsPrEP'

)ps on CONVERT(date,ps.VisitDate) = CONVERT(date,pmv.VisitDate) and  ps.PatientId=pmv.PatientId
where lt.DisplayName='prep-encounter' and sa.[Name]='PREP'
)prep