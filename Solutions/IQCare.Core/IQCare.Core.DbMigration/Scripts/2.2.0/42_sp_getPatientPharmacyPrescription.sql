

/****** Object:  StoredProcedure [dbo].[sp_getPatientPharmacyPrescription]    Script Date: 30/09/2019 12:04:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



ALTER PROCEDURE [dbo].[sp_getPatientPharmacyPrescription]

	@PatientMasterVisitID int = null

AS
BEGIN

Set Nocount On;
	declare @pharmacy_pk int
	set @pharmacy_pk = (select top 1 ptn_pharmacy_pk from ord_PatientPharmacyOrder 
						where PatientMasterVisitId = @PatientMasterVisitID and DeleteFlag <> 1)

select a.Drug_Pk,
	
	a.BatchNo batchId,
	a.FrequencyID, b.abbreviation abbr, b.DrugName, c.Name batchName, a.SingleDose dose, 
	d.Name freq,

	a.MorningDose, a.MiddayDose, a.EveningDose, a.NightDose,
	a.duration, a.OrderedQuantity, a.DispensedQuantity,
	
	a.Prophylaxis,
	pvw.DrugName,
	pvw.ProgID,
	pvw.TreatmentProgram,
	pvw.TreatmentPlan,
	pvw.TreatmentPlanText,
	pvw.TreatmentPlanReason,
	pvw.TreatmentPlanReasonId,
	pvw.Regimen,
	pvw.RegimenId,
	pvw.RegimenLine,
	pvw.RegimenLineId,
	pvw.PeriodTaken,
	pvw.PeriodTakenText
	

	 from dtl_PatientPharmacyOrder a inner join mst_drug b on a.Drug_Pk = b.Drug_pk
	left join Mst_Batch c on a.BatchNo = c.ID
	left join mst_Frequency d on a.FrequencyID = d.ID
	inner join PharmacyDrugVisitDetailsView pvw on pvw.Drug_Pk= a.Drug_Pk  and pvw.ptn_pharmacy_pk=a.ptn_pharmacy_pk
	where a.ptn_pharmacy_pk IN(SELECT ptn_pharmacy_pk from ord_PatientPharmacyOrder WHERE PatientMasterVisitId=@PatientMasterVisitID) 
	
End	
GO


