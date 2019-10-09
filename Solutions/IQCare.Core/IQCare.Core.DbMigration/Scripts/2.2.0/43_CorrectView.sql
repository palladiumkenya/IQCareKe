
IF OBJECT_ID('dbo.PharmacyDrugVisitDetailsView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PharmacyDrugVisitDetailsView]
GO


CREATE VIEW  [dbo].[PharmacyDrugVisitDetailsView]
AS


select  ISNULL(ROW_NUMBER() OVER(ORDER BY po.ptn_pharmacy_pk ASC), -1) AS RowID, po.PatientId,po.PatientMasterVisitId,po.VisitID,v.VisitDate
,po.Ptn_pk,po.ptn_pharmacy_pk,po.OrderedBy,mo.UserName as OrderedByName,
po.OrderedByDate,po.DispensedByDate ,po.DispensedBy as DispensedbyName,po.ProgID,
dc.[Name] as TreatmentProgram ,po.PharmacyPeriodTaken as PeriodTaken,CASE when po.orderstatus ='1' then 'New Order'
WHEN po.orderstatus =2 then 'Complete' 
WHEN po.orderstatus=3 then 'Partial' end as OrderStatusText,
po.orderstatus,
lt.DisplayName as [PeriodTakenText] , 

(lti.name + '(' + lti.displayname + ')') as Regimen 
,lti.Id as RegimenId
,ltv.Id as RegimenLineId,
ltv.DisplayName as RegimenLine,
ltt.DisplayName as TreatmentPlanText,
ltt.Id as TreatmentPlan,
ltrr.Id as TreatmentPlanReasonId,
ltrr.DisplayName as TreatmentPlanReason,

dtlpharm.Drug_Pk,dtlpharm.DrugName,dtlpharm.StrengthName,dtlpharm.StrengthID,dtlpharm.FrequencyName,dtlpharm.Multiplier,dtlpharm.FrequencyID,
dtlpharm.SingleDose,dtlpharm.Duration,dtlpharm.DispensedQuantity,dtlpharm.OrderedQuantity,dtlpharm.UserID,dtlpharm.UserName,
dtlpharm.Prophylaxis,dtlpharm.BatchName,dtlpharm.BatchNo,dtlpharm.MorningDose,dtlpharm.MiddayDose,dtlpharm.EveningDose,dtlpharm.NightDose,
dtlpharm.Abbreviation


from ord_PatientPharmacyOrder po
inner join mst_Decode dc on dc.id =po.ProgID
left join mst_User mo on mo.UserID =po.OrderedBy
left join mst_User mdo on mdo.UserID=po.DispensedBy
inner join ord_Visit v on v.Visit_Id =po.VisitID
left join LookupItem ltv on ltv.Id=po.RegimenLine
left join LookupItemView lt on lt.ItemId=po.PharmacyPeriodTaken
left join ARVTreatmentTracker att on att.PatientMasterVisitId=po.PatientMasterVisitId and att.RegimenLineId=po.RegimenLine
left join LookupItem lti on lti.Id=att.RegimenId
left join LookupItem ltt on ltt.Id =att.TreatmentStatusId
left join LookupItem ltrr on ltrr.Id =att.TreatmentStatusReasonId

left join(select  dpo.ptn_pharmacy_pk, d.Abbreviation,dpo.Drug_Pk,d.DrugName,mst.StrengthName,dpo.StrengthID,fq.[Name] as FrequencyName,fq.Multiplier,dpo.FrequencyID,
dpo.SingleDose,dpo.Duration,dpo.DispensedQuantity,dpo.OrderedQuantity,dpo.Financed,dpo.UserID,us.UserName,
dpo.Prophylaxis,bt.[Name] as BatchName,dpo.BatchNo,dpo.MorningDose,dpo.MiddayDose,dpo.EveningDose,dpo.NightDose
 from dtl_PatientPharmacyOrder dpo
left join Mst_Drug d on d.Drug_pk =dpo.Drug_Pk
left join mst_Strength mst on mst.StrengthId=dpo.StrengthID
left join mst_Frequency fq on fq.ID=dpo.FrequencyID
left join mst_User us on us.UserID =dpo.UserID
left join Mst_Batch bt on bt.ID=dpo.BatchNo

) dtlpharm on dtlpharm.ptn_pharmacy_pk=po.ptn_pharmacy_pk 




where (po.DeleteFlag is null) or (po.DeleteFlag = 0)
--order by po.ptn_pharmacy_pk desc



