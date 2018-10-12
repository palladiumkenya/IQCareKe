/****** Object:  StoredProcedure [dbo].[sp_getPatientPharmacyPrescription]    Script Date: 8/29/2018 12:28:01 PM ******/




 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MorningDose'
          AND Object_ID = Object_ID(N'Dtl_PatientPharmacyOrder'))
BEGIN
ALTER TABLE Dtl_PatientPharmacyOrder
ADD MorningDose decimal(10,2)
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MiddayDose'
          AND Object_ID = Object_ID(N'Dtl_PatientPharmacyOrder'))
BEGIN
ALTER TABLE Dtl_PatientPharmacyOrder ADD MiddayDose decimal(10,2)
END



 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'EveningDose'
          AND Object_ID = Object_ID(N'Dtl_PatientPharmacyOrder'))
BEGIN
ALTER TABLE Dtl_PatientPharmacyOrder ADD EveningDose decimal(10,2)
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'NightDose'
          AND Object_ID = Object_ID(N'Dtl_PatientPharmacyOrder'))
BEGIN
ALTER TABLE Dtl_PatientPharmacyOrder ADD NightDose  decimal(10,2)
END



 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'comments'
          AND Object_ID = Object_ID(N'Dtl_PatientPharmacyOrder'))
BEGIN
ALTER TABLE Dtl_PatientPharmacyOrder ADD comments varchar(500)
END




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientPharmacyPrescription]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientPharmacyPrescription]
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 13th Mar 2017
-- Description:	get patient pharmacy prescription
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPatientPharmacyPrescription]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @pharmacy_pk int
	set @pharmacy_pk = (select top 1 ptn_pharmacy_pk from ord_PatientPharmacyOrder 
						where PatientMasterVisitId = @PatientMasterVisitID and DeleteFlag <> 1)

	select a.Drug_Pk,
	--(select batchId from dtl_patientPharmacyDispensed where ptn_pharmacy_pk = a.ptn_pharmacy_pk and drug_pk = a.Drug_Pk) batchId,
	a.BatchNo batchId,
	a.FrequencyID, b.abbreviation abbr, b.DrugName, c.Name batchName, 
	--a.SingleDose dose, 	d.Name freq, 
	a.MorningDose, a.MiddayDose, a.EveningDose, a.NightDose,
	a.duration, a.OrderedQuantity, a.DispensedQuantity,
	--(select dispensedQuantity from dtl_patientPharmacyDispensed where ptn_pharmacy_pk = a.ptn_pharmacy_pk and drug_pk = a.Drug_Pk)DispensedQuantity,
	a.Prophylaxis
	from dtl_PatientPharmacyOrder a inner join mst_drug b on a.Drug_Pk = b.Drug_pk
	left join Mst_Batch c on a.BatchNo = c.ID
	left join mst_Frequency d on a.FrequencyID = d.ID
	--left join dtl_patientPharmacyDispensed e on a.ptn_pharmacy_pk = e.ptn_pharmacy_pk
	where a.ptn_pharmacy_pk IN(SELECT ptn_pharmacy_pk from ord_PatientPharmacyOrder WHERE PatientMasterVisitId=@PatientMasterVisitID) -- a.ptn_pharmacy_pk = @pharmacy_pk (old implementation)
	
End