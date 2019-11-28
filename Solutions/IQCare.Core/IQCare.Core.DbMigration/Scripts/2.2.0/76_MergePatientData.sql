-- =============================================
-- Author:	Felix
-- Create date: 28-Nov-2019
-- Description:	Merge Records
-- =============================================
CREATE PROCEDURE MergePatientData
	-- Add the parameters for the stored procedure here
	@preferredPersonId as Int,
	@unPreferredPersonId as Int,
	@userid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		DECLARE @count INT;
		DECLARE @i INT = 1;
		DECLARE @patientEnrollmentId int;

		DECLARE @preferredPtnPk as Int;
		DECLARE @unpreferredPtnPk as Int;

		DECLARE @preferredPatientId int = 0;
		DECLARE @unpreferredPatientId int = 0;

		-- set ptn_pk
		SET @preferredPtnPk= (select ptn_pk from Patient where PersonId = @preferredPersonId);
		SET @unpreferredPtnPk= (select ptn_pk from Patient where PersonId = @unPreferredPersonId);

		-- set PatientId
		SET @preferredPatientId= (select Id from Patient where PersonId = @preferredPersonId);
		SET @unpreferredPatientId= (select Id from Patient where PersonId = @unPreferredPersonId);



		--PatientID
		UPDATE PatientTreatementDiagnosis SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientTransferIn SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientScreening SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PmtctReferral SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientProphylaxis SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPhysicalExamination SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPHDP SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientIptWorkup SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientIptOutcome SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientIpt SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientIcfAction SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientIcf SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientHivDiagnosis SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientFamilyPlanningMethod SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientFamilyPlanning SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientEncounter SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientDiagnosis SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientConsent SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientClinicalNotes SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientClinicalDiagnosis SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientChronicIllness SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientBaselineAssessment SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientARVHistory SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientAppointment  SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientAllergy SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE INHProphylaxis SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE HIVEnrollmentBaseline SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE Disclosure SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE ComplaintsHistory SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE ARVTreatmentTracker SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientArtDistribution SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE AdverseEvent SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE AdultChildVaccination SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE AdherenceOutcome SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE AdherenceAssessment SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientWHOStage SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientReenrollment SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientLabTracker SET patientId = @preferredPatientId WHERE patientId = @unpreferredPatientId;
		UPDATE HeiLabTests SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPartnerProfile SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientAppointmentReasons SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE RiskAssessment SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientCircumcisionStatus SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPrEPStatus SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientMilestone SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE HEIFeeding SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE HEIEncounter SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE HEIProfile SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE HeiMilestone SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPncExercises SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientAdverseEventOutcome SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientCategorization SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientSupportSystemCriteria SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPsychosocialCriteria SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE Vaccination SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE TreatmentEventTracker SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE TBTreatmentTracker SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE ServiceEntryPoint SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE BaselineAntenatalCare SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPreventiveServices SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPartnerTesting SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientLabSample SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientDrugAdministration  SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientCounselling SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE VisitDetails SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE TuberclosisTreatment SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientSocialHistory SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientTannersStaging SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE TannersStaging  SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientNeonatalHistory SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientProfile SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientPartner SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientHighRisk SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientSexualHistory SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientOI SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PresentingComplaints SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PregnancyLog SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PregnancyIndicator SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE Pregnancy SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PhysicalExamination SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientVitals SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PatientTreatmentInitiation SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;
		UPDATE PersonRelationship SET PatientId = @preferredPatientId where PatientId = @unpreferredPatientId;
		UPDATE PatientMasterVisit SET PatientId = @preferredPatientId where PatientId = @unpreferredPatientId;

		--PersonId
		UPDATE PatientPopulation SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PatientOVCStatus SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PatientMaritalStatus SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE HIVTesting SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE HIVReConfirmatoryTest SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PersonOccupation SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PersonEducation SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PersonPriority SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE Tracing SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE HtsScreeningOptions SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE HtsScreening SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE AppStateStore SET PersonId = @preferredPersonId, PatientId = @preferredPatientId WHERE PersonId = @unPreferredPersonId AND PatientId = @unpreferredPatientId;
		UPDATE Referral SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE ClientDisability SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE HtsEncounter SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE ImmunizationTracker SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PersonIdentifier SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PatientLinkage SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PersonLocation SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PersonContact SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;
		UPDATE PatientTreatmentSupporter SET PersonId = @preferredPersonId WHERE PersonId = @unPreferredPersonId;

		--ptn_pk
		UPDATE DTL_FBCUSTOMFIELD_PNSFORM SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE [DTL_CUSTOMFORM_Contact Tracing and Outcomes_PNSTRACING] SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE [DTL_FBCUSTOMFIELD_PNSTRACING] SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE HIVTestTracker SET Ptn_Pk = @preferredPtnPk WHERE Ptn_Pk = @unpreferredPtnPk;
		UPDATE ord_LabOrder SET Ptn_Pk = @preferredPtnPk, PatientId = @preferredPatientId WHERE Ptn_Pk = @unpreferredPtnPk AND PatientId = @unpreferredPatientId;
		UPDATE ord_ClinicalServiceOrder SET Ptn_Pk = @preferredPtnPk WHERE Ptn_Pk = @unpreferredPtnPk;
		UPDATE dtl_BillingReceipt SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE ord_Bill_Reversals SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE ord_bill SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE dtl_Bill SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE dtl_BillDepositTransaction SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE dtl_PatientItemsOrder SET ptn_Pk = @preferredPtnPk WHERE ptn_Pk = @unpreferredPtnPk;
		UPDATE mst_Bill SET ptn_Pk = @preferredPtnPk WHERE ptn_Pk = @unpreferredPtnPk;
		UPDATE Dtl_PatientDeleteLog SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE dtl_PatientWardAdmission SET Ptn_PK = @preferredPtnPk WHERE Ptn_PK = @unpreferredPtnPk;
		UPDATE dtl_PatientClinicalNotes SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_CustomField_Home_Visit SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_CustomField_Laboratory SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_CareEnd_ANC_Maternity_and_Postnatal SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_CareEnd_TB SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_AllergiesDetails SET ptn_pk = @preferredPtnPk WHERE ptn_pk = @unpreferredPtnPk;
		UPDATE Lnk_PatientReEnrollment SET Ptn_Pk = @preferredPtnPk WHERE Ptn_Pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_CareEnd_CCC_Patient_Card_MoH_257 SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientARVEligibility SET ptn_pk = @preferredPtnPk WHERE ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientPreventionwithpositives  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientAtRiskPopulationServices SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientAtRiskPopulation SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientBlueCardPriorART SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE [DTL_CUSTOMFORM_Sputum Smear_TB_Patient_Profile] SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_TB_Patient_Profile  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_TB_Initial_and_Continuation_Phase SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_PresentingComplaintsPaed SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Paediatric_Clinical_Evaluation_Form SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Intensive_Case_Finding SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_SkinExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_ChestLungsExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_CNSExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_GenitourinaryExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_OralCavityExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_CardiovascularExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_AbdomenExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_LymphNodesExam SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Initial_Assesment_Form  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Cervical_Cancer_Screening_Form SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_NextAppointmentReason SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_PriorityHomeVisitReason SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Plan_ SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Assesment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Skin SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Chest_Lungs SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Abdomen SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_LymphNodes SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_CNS SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Cardiovascular SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_PhysicalExamGeneral SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Adult_Clinical_Evaluation_Form SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_PresentingComplaints SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Maternal_And_Exposed_Infant_III SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Maternal_And_Exposed_Infant_II SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Maternal_and_Exposed_Infant_I SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_HEI_Part_II SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Immunisation SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_HEI_Follow_Up_Card SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_ChildRefferedFrom SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Patient_Registration SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_ICD10Field SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientHouseHoldInfo  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_patientInterviewer  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_patientDeposits SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_patientGuarantor SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_urbanResidence SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_ruralResidence SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_DrugAllegiesAndMedicalCondition SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PriorArvAndHivCare SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientFamilyPlanning SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientARTEncounter SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientARTCare SET ptn_pk = @preferredPtnPk WHERE ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_ExposedInfant SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE Dtl_PatientBillTransaction SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE Dtl_StockTransaction SET PtnPk = @preferredPtnPk WHERE PtnPk = @unpreferredPtnPk;
		UPDATE Ord_PatientBillTransaction SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_CareEnd_HIVCARE_STATICFORM SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientTBType SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientTBHIVCare SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Family_HIV_History SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Antenatal_Visits SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE [DTL_FBCUSTOMFIELD_L&D_and_Postpartum_Plan] SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_ImmunizationsBooster SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Immunizations9mths SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Immunizations14wks SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Immunizations6wks SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Immunizations10wks SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_Immunizations0wks SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_CareEnd_PACTInfant SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_CareEnd_PACTMother SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Infant_Outcome SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Assessment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Vital_Status SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Infant_Visit SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_HIV_Testing SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Immunizations SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Infant_ARV_History SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Infant_Enrollment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_OVPPenta2 SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_OPVPenta1 SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Anetnatal_Visits  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Antenatal_and_Delivery_Plan  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Maternal_HIV_History  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_FBCUSTOMFIELD_Mother_Enrollment  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_OtherMedicationsAntenatal SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FB_OtherMedications SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientCareRestarted SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_Adherence_Reason SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PMTCTDetails SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientPriorART SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE Dtl_PatientOIDeathReason SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientARTRestart  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientClinicalNotes_Old  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_InfantParent SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_InfantInfo SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PMTCTStatus SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_patientCounseling SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientDelivery SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientOtherTreatment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FamilyInfo SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientContacts SET ptn_pk = @preferredPtnPk WHERE ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientAdherence SET ptn_pk = @preferredPtnPk WHERE ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_FollowupEducation SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientARVInfo SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientTransfer SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE ord_PatientPharmacyOrder SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientReferredTo SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientHivOther SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_ARTSponsor SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_HIVAIDSCareTypes SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_Adherence_Missed_Reason SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientAssessment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;

		UPDATE dtl_RegimenMap SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientCareEnded SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientHIVBarrier SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientHomeVisit SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientAppointment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE ord_Visit SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;

		UPDATE dtl_PatientVitals  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientStage  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientClinicalStatus  SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;

		UPDATE dtl_PatientAllergy SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientArvTherapy SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientHivPrevCareIE SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE lnk_NonDocumented SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;


		UPDATE dtl_PatientDisclosure SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientTrackingCare SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientSymptoms SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientHivPrevCareEnrollment SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;



		UPDATE dtl_PatientDisease SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_Multiselect_line SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE DTL_Adult_Initial_Evaluation_Form SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_TBScreening SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;
		UPDATE dtl_PatientLockedRecordsForDispensing SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk;



		--think about
		UPDATE GreenCardBlueCard_Transactional SET PersonId = @preferredPersonId, Ptn_Pk = @preferredPtnPk WHERE PersonId = @unPreferredPersonId and Ptn_Pk = @unpreferredPtnPk;

		--FINALLY
		UPDATE PatientCareending SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;

		CREATE TABLE #TPatientEnrollment(Id INT IDENTITY(1,1), PatientEnrollmentId int, ServiceAreaId int); 

		--Insert data to temporary table #Tdtl_FamilyInfo 
		INSERT INTO #TPatientEnrollment(
			PatientEnrollmentId, ServiceAreaId
		)
		SELECT Id, ServiceAreaId FROM PatientEnrollment WHERE PatientId = @unpreferredPatientId;

		SELECT @count = COUNT(Id) FROM #TPatientEnrollment
		BEGIN
			WHILE (@i <= @count)
			BEGIN
				DECLARE @ServiceAreaId int;
				SELECT @patientEnrollmentId = PatientEnrollmentId, @ServiceAreaId = ServiceAreaId FROM #TPatientEnrollment WHERE Id = @i;
		
				IF NOT EXISTS(SELECT * FROM PatientEnrollment WHERE PatientId = @preferredPatientId AND ServiceAreaId = @ServiceAreaId)
					BEGIN
						UPDATE PatientIdentifier SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId AND PatientEnrollmentId = @patientEnrollmentId;
						UPDATE PatientEnrollment SET PatientId = @preferredPatientId WHERE PatientId = @unpreferredPatientId;			
					END
				ELSE
					BEGIN
						UPDATE PatientIdentifier SET DeleteFlag = 1 WHERE PatientId = @unpreferredPatientId AND PatientEnrollmentId = @patientEnrollmentId;
						UPDATE PatientEnrollment SET DeleteFlag = 1 WHERE PatientId = @unpreferredPatientId AND ServiceAreaId = @ServiceAreaId; 
					END

				SELECT @i = @i + 1;
			END
		END

		DECLARE @count_lnk INT;
		DECLARE @i_lnk INT = 1;

		CREATE TABLE #TLnk_PatientProgramStart(Id INT IDENTITY(1,1), Ptn_Pk int, ModuleId int);
		INSERT INTO #TLnk_PatientProgramStart(
			Ptn_Pk, ModuleId
		)
		SELECT Ptn_pk, ModuleId FROM Lnk_PatientProgramStart WHERE Ptn_pk = @unpreferredPtnPk;

		SELECT @count_lnk = COUNT(Id) FROM #TLnk_PatientProgramStart
		BEGIN
			WHILE (@i_lnk <= @count_lnk)
			BEGIN
				DECLARE @ModuleId int;
				SELECT @ModuleId = ModuleId FROM #TLnk_PatientProgramStart WHERE Id = @i_lnk;
		
				IF NOT EXISTS(SELECT * FROM Lnk_PatientProgramStart WHERE Ptn_pk = @unpreferredPtnPk AND ModuleId = @ModuleId)
					BEGIN
						UPDATE Lnk_PatientProgramStart SET Ptn_pk = @preferredPtnPk WHERE Ptn_pk = @unpreferredPtnPk AND ModuleId = @ModuleId;		
					END
				ELSE
					BEGIN
						DELETE FROM Lnk_PatientProgramStart WHERE Ptn_pk = @unpreferredPtnPk AND ModuleId = @ModuleId;
					END

				SELECT @i_lnk = @i_lnk + 1;
			END
		END

		UPDATE Patient set DeleteFlag = 1 WHERE Id = @unpreferredPatientId;
		UPDATE Person set DeleteFlag = 1 WHERE Id = @unPreferredPersonId;
		UPDATE mst_Patient SET DeleteFlag = 1 WHERE Ptn_Pk = @unpreferredPtnPk;

		INSERT INTO [dbo].[PatientMergingLog] (PreferredPatientId, UnPreferredPatientId, CreatedBy, CreateDate)
		VALUES(@preferredPatientId, @unpreferredPatientId, @userid, GETDATE());

		DROP TABLE #TPatientEnrollment
		DROP TABLE #TLnk_PatientProgramStart

		IF @@TRANCOUNT > 0	COMMIT;
	END TRY
	BEGIN CATCH
		Declare @ErrorMessage NVARCHAR(4000),@ErrorSeverity Int,@ErrorState Int;

		Select	@ErrorMessage = Error_message(),@ErrorSeverity = Error_severity(),	@ErrorState = Error_state();

		Raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState  );

		IF @@TRANCOUNT > 0  ROLLBACK
	END CATCH
END
