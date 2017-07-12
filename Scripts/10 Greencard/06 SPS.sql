/****** Object:  StoredProcedure [dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard]
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUpdatePharmacy_GreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveUpdatePharmacy_GreenCard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SaveUpdatePharmacy_GreenCard]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientFamilyPlanningMethod]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientFamilyPlanningMethod]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientFamilyPlanningMethod]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterVaccines]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterVaccines]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterVaccines]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterTS]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterTS]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPresentingComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPresentingComplaints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterPresentingComplaints]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPhysicalExam]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPhysicalExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterPhysicalExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPHDP]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPHDP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterPHDP]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPatientManagement]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPatientManagement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterPatientManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterGeneralExam]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterGeneralExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterGeneralExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterDiagnosis]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterDiagnosis]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterDiagnosis]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterComplaints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterComplaints]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterChronicIllness]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterChronicIllness]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterAllergies]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterAllergies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterAllergies]
GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_savePatientEncounterAdverseEvents]
GO
/****** Object:  StoredProcedure [dbo].[SP_mst_PatientToGreencardRegistration]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_mst_PatientToGreencardRegistration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_mst_PatientToGreencardRegistration]
GO
/****** Object:  StoredProcedure [dbo].[FamilyTesting_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FamilyTesting_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FamilyTesting_To_Greencard]
GO
/****** Object:  StoredProcedure [dbo].[sp_getZScores]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getZScores]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getZScores]
GO
/****** Object:  StoredProcedure [dbo].[sp_getTreatmentProgram]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getTreatmentProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getTreatmentProgram]
GO
/****** Object:  StoredProcedure [dbo].[sp_getRegimenClassification]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getRegimenClassification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getRegimenClassification]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyRegimens]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyRegimens]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyRegimens]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyFields]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyFields]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyFields]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugSwitchSubReasons]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugSwitchSubReasons]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyDrugSwitchSubReasons]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyDrugList]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugFrequency]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugFrequency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyDrugFrequency]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyBatch]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyBatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyBatch]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPendingPrescriptions]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPendingPrescriptions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPendingPrescriptions]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientPharmacyPrescription]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientPharmacyPrescription]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientPharmacyPrescription]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterVaccines]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterVaccines]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterVaccines]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterHistory]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterHistory]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterExam]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterDiagnosis]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterDiagnosis]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterDiagnosis]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterComplaints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterComplaints]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterChronicIllness]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterChronicIllness]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterAllergies]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterAllergies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterAllergies]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterAdverseEvents]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounter]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounter]
GO
/****** Object:  StoredProcedure [dbo].[sp_getItemIdByGroupAndItemName]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getItemIdByGroupAndItemName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getItemIdByGroupAndItemName]
GO
/****** Object:  StoredProcedure [dbo].[sp_getFacility]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getFacility]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getFacility]
GO
/****** Object:  StoredProcedure [dbo].[sp_getCurrentRegimen]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getCurrentRegimen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getCurrentRegimen]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientFamilyPlanningMethod]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientFamilyPlanningMethod]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientFamilyPlanningMethod]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterVaccines]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterVaccines]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterVaccines]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterPresentingComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterPresentingComplaints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterPresentingComplaints]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterPhysicalExam]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterPhysicalExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterPhysicalExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterPHDP]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterPHDP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterPHDP]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterGeneralExam]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterGeneralExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterGeneralExam]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterDiagnosis]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterDiagnosis]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterDiagnosis]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterComplaints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterComplaints]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterChronicIllness]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterChronicIllness]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterAllergies]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterAllergies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterAllergies]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletePatientEncounterAdverseEvents]
GO
/****** Object:  StoredProcedure [dbo].[SP_Bluecard_ToGreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Bluecard_ToGreenCard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_Bluecard_ToGreenCard]
GO
/****** Object:  StoredProcedure [dbo].[Pr_SF_GetPatientSearchresults]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_SF_GetPatientSearchresults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_SF_GetPatientSearchresults]
GO
/****** Object:  StoredProcedure [dbo].[pr_selectedListValue_Futures]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_selectedListValue_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_selectedListValue_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_GetBMI]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_GetBMI]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_GetBMI]
GO
/****** Object:  StoredProcedure [dbo].[PersonContact_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonContact_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PersonContact_Update]
GO
/****** Object:  StoredProcedure [dbo].[PersonContact_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonContact_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PersonContact_Insert]
GO
/****** Object:  StoredProcedure [dbo].[Person_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Person_Update]
GO
/****** Object:  StoredProcedure [dbo].[Person_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Person_Insert]
GO
/****** Object:  StoredProcedure [dbo].[PatientTreatmentSupporter_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporter_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientTreatmentSupporter_Insert]
GO
/****** Object:  StoredProcedure [dbo].[Patient_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Patient_Update]
GO
/****** Object:  StoredProcedure [dbo].[Patient_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Patient_Insert]
GO
/****** Object:  StoredProcedure [dbo].[Ord_Visit_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ord_Visit_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Ord_Visit_Insert]
GO
/****** Object:  StoredProcedure [dbo].[mstPatient_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mstPatient_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[mstPatient_Insert]
GO


/****** Object:  StoredProcedure [dbo].[PatientTreatmentSupporter_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporter_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientTreatmentSupporter_Update]
GO

CREATE PROCEDURE [dbo].[PatientTreatmentSupporter_Update] 
	-- Add the parameters for the stored procedure here
	@PersonId int, 
	@SupporterId int,
	@MobileContact varbinary(max),
	@CreatedBy int,
	@DeleteFlag bit,
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 IF @MobileContact IS NULL
	  SET @MobileContact = NULL;
	 ELSE
	  SET @MobileContact = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileContact);

	  UPDATE PatientTreatmentSupporter
	  SET SupporterId = @SupporterId, PersonId = @PersonId, MobileContact = @MobileContact , CreatedBy = @CreatedBy, DeleteFlag = @DeleteFlag
	  WHERE Id = @Id

	  SELECT SCOPE_IDENTITY() Id;
END


GO

/****** Object:  StoredProcedure [dbo].[mstPatient_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mstPatient_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[mstPatient_Insert] AS' 
END
GO

-- =============================================
-- Author: Felix Kithinji
-- Create date: 15-Mar-2017
-- Description: Insert
-- =============================================
ALTER PROCEDURE [dbo].[mstPatient_Insert] 
 -- Add the parameters for the stored procedure here
 @FirstName varchar(MAX),
 @LastName varchar(MAX),
 @MiddleName varchar(MAX),
 @LocationID int,
 @PatientEnrollmentID varchar(50),
 @ReferredFrom int,
 @RegistrationDate datetime,
 @Sex int,
 @DOB datetime,
 @DobPrecision int,
 @MaritalStatus int,
 @Address varchar(MAX),
 @Phone varchar(MAX),
 @UserID int,
 @PosId varchar(10),
 @ModuleId int,
 @StartDate datetime,
 @CreateDate datetime
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 DECLARE @Ptn_Pk int;
 DECLARE @EntryPointName varchar(50);
 DECLARE @Referral int;
 DECLARE @MaritalStatusName varchar(50);
 DECLARE @MaritalStatusId int;

 BEGIN
  Select @EntryPointName = ItemName
  from LookupItemView where ItemId=@ReferredFrom;

  SELECT @MaritalStatusName = ItemName
  from LookupItemView where ItemId=@MaritalStatus;

  select TOP 1 @Referral = ID from mst_Decode where NAME LIKE + '%'+ @EntryPointName + '%' AND CodeID = 17;
  select TOP 1 @MaritalStatusId = ID from mst_Decode where Name LIKE + '%'+ @MaritalStatusName + '%' AND CodeID=12;

  IF @Referral IS NULL
  BEGIN
  select TOP 1 @Referral = ID from mst_Decode where NAME = 'VCT'
  END
 END

    -- Insert statements for procedure here
 Insert Into mst_Patient(FirstName, LastName, MiddleName, LocationID, PatientEnrollmentID, ReferredFrom, RegistrationDate, Sex, DOB, DobPrecision, MaritalStatus, Address, Phone, UserID, PosId, Status, DeleteFlag, CreateDate,MovedToPatientTable)
 Values(
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstName),
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastName),
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MiddleName),
  @LocationID,
  @PatientEnrollmentID,
  @Referral,
  @RegistrationDate,
  @Sex,
  @DOB,
  @DobPrecision,
  @MaritalStatusId,
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@Address),
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@Phone),
  @UserID,
  @PosId,
  0,
  0,
  @CreateDate,
  1
 );
 SELECT @Ptn_Pk=@@IDENTITY;
 SELECT SCOPE_IDENTITY() Ptn_Pk;

 Insert Into Lnk_PatientProgramStart(Ptn_pk, ModuleId, StartDate, UserID, CreateDate)
 Values(
  @Ptn_Pk,
  @ModuleId,
  @StartDate,
  @UserID,
  @CreateDate
 );
END
GO
/****** Object:  StoredProcedure [dbo].[Ord_Visit_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ord_Visit_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Ord_Visit_Insert] AS' 
END
GO

-- =============================================
-- Author: Felix Kithinji
-- Create date: 15-Mar-2017
-- Description: Insert
-- =============================================
ALTER PROCEDURE [dbo].[Ord_Visit_Insert]
 -- Add the parameters for the stored procedure here
 @Ptn_Pk int,
 @LocationID int,
 @VisitDate datetime,
 @VisitType int,
 @UserID int,
 @CreateDate datetime,
 @ModuleId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 DECLARE @Id int

    -- Insert statements for procedure here
 Insert Into ord_Visit(Ptn_Pk, LocationID, VisitDate, VisitType, DeleteFlag, UserID, CreateDate, CreatedBy, ModuleId)
 Values(
  @Ptn_Pk,
  @LocationID,
  @VisitDate,
  @VisitType,
  0,
  @UserID,
  @CreateDate,
  @UserID,
  @ModuleId
 );
 SELECT @Id=@@IDENTITY;
END


GO
/****** Object:  StoredProcedure [dbo].[Patient_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Patient_Insert] AS' 
END
GO
ALTER PROCEDURE [dbo].[Patient_Insert] 
 -- Add the parameters for the stored procedure here
 @PersonId int,
 @ptn_pk int = null,
 @PatientIndex varchar(50),
 @DateOfBirth datetime,
 @NationalId varchar(100),
 @FacilityId int,
 @UserId int,
 @Active bit,
 @PatientType int,
 @DobPrecision bit
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)
 Values(
  @ptn_pk,
  @PersonId,
  @PatientIndex,
  @PatientType,
  @FacilityId,
  @Active,
  @DateOfBirth,
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@NationalId),
  0,
  @UserId,
  GETDATE(),
  NULL,
  @DobPrecision
 );
SELECT SCOPE_IDENTITY() Id;
END

GO
/****** Object:  StoredProcedure [dbo].[Patient_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Patient_Update] AS' 
END
GO

-- =============================================
-- Author: Felix Kithinji
-- Create date: 15-Mar-2017
-- Description: update
-- =============================================
ALTER PROCEDURE [dbo].[Patient_Update] 
 -- Add the parameters for the stored procedure here
 @ptn_pk int = null,
 @DateOfBirth datetime,
 @NationalId varchar(100),
 @FacilityId int,
 @AuditData xml=null,
 @Id int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 UPDATE Patient
 SET
  ptn_pk = @ptn_pk,
  DateOfBirth = @DateOfBirth,
  NationalId=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@NationalId),
  FacilityId=@FacilityId,
  AuditData=@AuditData
 WHERE
  Id=@Id
END


GO
/****** Object:  StoredProcedure [dbo].[PatientTreatmentSupporter_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporter_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientTreatmentSupporter_Insert] AS' 
END
GO


-- =============================================
-- Author: Felix
-- Create date: 28-Apr-2017
-- Description: Insert into Patient Treatment Supporter
-- =============================================
ALTER PROCEDURE [dbo].[PatientTreatmentSupporter_Insert] 
 -- Add the parameters for the stored procedure here
 @PersonId int, 
 @SupporterId int,
 @MobileContact varbinary(max),
 @CreatedBy int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 IF @MobileContact IS NULL
  SET @MobileContact = NULL;
 ELSE
  SET @MobileContact = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileContact);

    -- Insert statements for procedure here
 INSERT INTO PatientTreatmentSupporter([PersonId], [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
 VALUES(@PersonId, @SupporterId, @MobileContact, 0, @CreatedBy, GETDATE());

 SELECT SCOPE_IDENTITY() Id;
END



GO
/****** Object:  StoredProcedure [dbo].[Person_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Person_Insert] AS' 
END
GO
-- =============================================
-- Author:  Steve Osewe
-- Create date: 20-Jan-2017
-- Description: Insert
-- =============================================
ALTER PROCEDURE [dbo].[Person_Insert]
 -- Add the parameters for the stored procedure here
 @FirstName varchar(100),
 @MidName varchar(100)= Null,
 @LastName varchar(100),
 @Sex int,
 @DateOfBirth datetime = NULL,
 @DobPrecision bit = NULL,
 --@NationalId varchar(100) = null,
 @UserId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 --if(@DateOfBirth is null)BEGIN SET @DateOfBirth='1989-06-15' END
    -- Insert statements for procedure here
 Insert Into Person(FirstName, MidName,LastName,Sex,DateOfBirth,DobPrecision,Active,DeleteFlag,CreateDate,CreatedBy)
 Values(
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstName),
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MidName),
  ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastName),
  @Sex,
  @DateOfBirth,
  @DobPrecision,
  --ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@NationalId),
  1,
  0,
  GETDATE(),
  @UserId
 );
 SELECT SCOPE_IDENTITY() PersonId;
END
GO
/****** Object:  StoredProcedure [dbo].[Person_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Person_Update] AS' 
END
GO
ALTER PROCEDURE [dbo].[Person_Update]
 -- Add the parameters for the stored procedure here
 @FirstName varchar(100),
 @MidName varchar(100)= Null,
 @LastName varchar(100),
 @Sex int,
 @DateOfBirth datetime = NULL,
 --@NationalId varchar(100) = null,
 @Id int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 UPDATE Person
 SET
  FirstName=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstName),
  MidName=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MidName),
  LastName=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastName),
  Sex=@Sex,
  DateOfBirth=@DateOfBirth
  --NationalId=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@NationalId)
 WHERE
   Id=@Id;
END
GO
/****** Object:  StoredProcedure [dbo].[PersonContact_Insert]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonContact_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PersonContact_Insert] AS' 
END
GO
-- =============================================
-- Author:		Steve Osewe
-- Create date: 20-Jan-2017
-- Description:	Insert
-- =============================================
ALTER PROCEDURE [dbo].[PersonContact_Insert]
	-- Add the parameters for the stored procedure here
	@PersonId int=Null,
	@PhysicalAddress varbinary(max)= Null,
	@MobileNumber varbinary(max)=Null,
	@AlternativeNumber varbinary(max)=Null,
	@EmailAddress varbinary(max)=Null,
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @Id int;

	 IF @MobileNumber IS NULL
  SET @MobileNumber = NULL;
 ELSE
  SET @MobileNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileNumber);

	if( @AlternativeNumber IS NULL)
		BEGIN
		  SET @AlternativeNumber = NULL;
		END
	else
	BEGIN
	  SET @AlternativeNumber=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@AlternativeNumber)
	END

	if(@EmailAddress is null)
	begin
		SET @EmailAddress= NULL;
	end
	else
		begin
		  SET @EmailAddress=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@EmailAddress);
		end

	Insert Into PersonContact(PersonId,PhysicalAddress,MobileNumber,AlternativeNumber,EmailAddress,Active,DeleteFlag,CreateDate,CreatedBy)
	Values(
		@PersonId,
		ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@PhysicalAddress),
		@MobileNumber,
		-- ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileNumber),
		@AlternativeNumber,
		@EmailAddress,
		1,
		0,
		GETDATE(),
		@UserId
	);
	SET @Id =(Select SCOPE_IDENTITY() Id);
	
	-- Set Previous Contacts to Zero
	if(@Id>0)
	BEGIN
		UPDATE PersonContact SET Active=0 WHERE PersonId=@PersonId AND Id NOT IN(Id);
	END
END

GO
/****** Object:  StoredProcedure [dbo].[PersonContact_Update]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonContact_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PersonContact_Update] AS' 
END
GO
ALTER PROCEDURE [dbo].[PersonContact_Update]
 -- Add the parameters for the stored procedure here
 @PersonId int=Null,
 @PhysicalAddress varbinary(max)= Null,
 @MobileNumber varbinary(max)=Null,
 @AlternativeNumber varbinary(max)=Null,
 @EmailAddress varbinary(max)=Null,
 @Id int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 if( @AlternativeNumber IS NULL)
  BEGIN
    SET @AlternativeNumber = NULL;
  END
 else
 BEGIN
   SET @AlternativeNumber=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@AlternativeNumber)
 END

 if(@EmailAddress is null)
 begin
  SET @EmailAddress= NULL;
 end
 else
  begin
    SET @EmailAddress=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@EmailAddress);
  end

    -- Insert statements for procedure here
    UPDATE PersonContact
 SET
  PhysicalAddress=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@PhysicalAddress),
  MobileNumber=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileNumber),
  AlternativeNumber=@AlternativeNumber,
  EmailAddress=@EmailAddress
 WHERE 
  PersonId=@PersonId 
  AND
  Id=@Id 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_GetBMI]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_GetBMI]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pr_GetBMI] AS' 
END
GO
ALTER procedure [dbo].[pr_GetBMI]
(
@ptnpk varchar(50),
@locationID varchar(50),
@VisitID varchar(50)
)
as 
begin
-- select * from  dbo.dtl_PatientVitals where ptn_pk=128 and locationid=757 and visit_pk=1110 and height is not null and weight is not null

if exists(select 1 from  dbo.dtl_PatientVitals where ptn_pk=@ptnpk and locationid=@locationID and visit_pk=@VisitID and height is not null and weight is not null)
  begin 
		select  Weight / ((height / 100) * (height / 100))[BMI] from  dbo.dtl_PatientVitals where ptn_pk=@ptnpk and locationid=@locationID and visit_pk=@VisitID and height is not null and weight is not null
	end 
else if exists(select 1 from  dbo.dtl_PatientVitals where ptn_pk=@ptnpk and locationid=@locationID and height is not null and weight is not null)
 begin 
		select top 1 Weight / ((height / 100) * (height / 100))[BMI] from  dbo.dtl_PatientVitals where ptn_pk=@ptnpk and locationid=@locationID and height is not null and weight is not null order by visit_pk desc
	end 
else 
 begin select 0.0
	end 
end

GO

/****** Object:  StoredProcedure [dbo].[pr_Scheduler_UpdateAppointmentStatus]    Script Date: 5/22/2017 1:02:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Scheduler_UpdateAppointmentStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Scheduler_UpdateAppointmentStatus]
GO
/****** Object:  StoredProcedure [dbo].[pr_Scheduler_UpdateAppointmentStatus]    Script Date: 5/22/2017 1:02:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Scheduler_UpdateAppointmentStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pr_Scheduler_UpdateAppointmentStatus] AS' 
END
GO
ALTER PROCEDURE [dbo].[pr_Scheduler_UpdateAppointmentStatus]
(@Currentdate DATETIME,
 @locationid  INT
)
AS      
     --Begin------Check in PatientMasterVisit table if record exist(excluding the records of scheduler and enrollment visit type)      ------with in grace period of appointment date then update the status to met if the       ------appointment date + grace period has gone then update the status to missed      ------seprately check the record in lab order table as the lab entries does not go into the PatientMasterVisit table      -----------------------Update Met status--------------------------      
     UPDATE PatientAppointment
       SET
           StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Met'
     ),
           StatusDate =
     (
         SELECT MIN(VisitDate)
         FROM PatientMasterVisit c
         WHERE(c.visitdate BETWEEN(PatientAppointment.AppointmentDate -
                                  (
                                      SELECT appgraceperiod
                                      FROM mst_facility
                                      WHERE facilityid = @locationid
                                  )) AND(PatientAppointment.AppointmentDate +
                                        (
                                            SELECT appgraceperiod
                                            FROM mst_facility
                                            WHERE facilityid = @locationid
                                        ) + 1))
              AND c.PatientId = PatientAppointment.PatientId
              AND c.Id <> PatientAppointment.PatientMasterVisitId
              AND visittype NOT IN(5, 0)
         AND visittype <> 0
     )
     WHERE StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Pending'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientId IN
     (
         SELECT c.PatientId
         FROM PatientMasterVisit c
         WHERE((c.visitdate BETWEEN(PatientAppointment.AppointmentDate -
                                   (
                                       SELECT appgraceperiod
                                       FROM mst_facility
                                       WHERE facilityid = @locationid
                                   )) AND(PatientAppointment.AppointmentDate +
                                         (
                                             SELECT appgraceperiod
                                             FROM mst_facility
                                             WHERE facilityid = @locationid
                                         ) + 1)))
              AND c.PatientId = PatientAppointment.PatientId
              AND c.Id <> PatientAppointment.PatientMasterVisitId
              AND visittype NOT IN(5, 0)
         AND visittype <> 0
     );
     UPDATE PatientAppointment
       SET
           StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Met'
     ),
           StatusDate =
     (
         SELECT MIN(VisitDate)
         FROM PatientMasterVisit c
         WHERE(c.visitdate BETWEEN(PatientAppointment.AppointmentDate -
                                  (
                                      SELECT appgraceperiod
                                      FROM mst_facility
                                      WHERE facilityid = @locationid
                                  )) AND(PatientAppointment.AppointmentDate +
                                        (
                                            SELECT appgraceperiod
                                            FROM mst_facility
                                            WHERE facilityid = @locationid
                                        ) + 1))
              AND c.PatientId = PatientAppointment.PatientId
              AND c.Id <> PatientAppointment.PatientMasterVisitId
              AND visittype NOT IN(5, 0)
         AND visittype <> 0
     )
     WHERE StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Pending'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientId IN
     (
         SELECT c.Ptn_Pk
         FROM ord_patientlaborder c
         WHERE((c.createdate BETWEEN(PatientAppointment.AppointmentDate -
                                    (
                                        SELECT appgraceperiod
                                        FROM mst_facility
                                        WHERE facilityid = @locationid
                                    )) AND(PatientAppointment.AppointmentDate +
                                          (
                                              SELECT appgraceperiod
                                              FROM mst_facility
                                              WHERE facilityid = @locationid
                                          ) + 1)))
              AND c.Ptn_Pk = PatientAppointment.PatientId
              AND c.locationid = @locationid
     );
     ---- -----------------------Update Missed status--------------------------      
     UPDATE PatientAppointment
       SET
           StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Missed'
     ),
           StatusDate = @Currentdate
     WHERE StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Pending'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientId IN
     (
         SELECT PatientAppointment.PatientId
         FROM PatientAppointment
         WHERE StatusId =
         (
             SELECT LookupItemId
             FROM LookupMasterItem
             WHERE DisplayName LIKE 'Pending'
         )
               AND PatientId = PatientAppointment.PatientId
               AND StatusDate = PatientAppointment.StatusDate
               AND (PatientAppointment.AppointmentDate +
                   (
                       SELECT appgraceperiod
                       FROM mst_facility
                       WHERE facilityid = @locationid
                   ) < @currentdate)
     );
     UPDATE A
       SET
           StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Care Ended'
     ),
           StatusDate = @Currentdate
     FROM Patient AS P
          INNER JOIN PatientAppointment AS A ON P.Id = A.PatientId
     WHERE(P.Active = 0)
          AND A.DeleteFlag = 0
          AND A.StatusId IN
     (
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Missed'
     ),
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Pending'
     )
     );
     ----update status of all those active patients(previously inactive) who have careended appointments, to missed and      -----Then compare StatusDate with currentdate if curentdate is less then (StatusDate + graceperoiddate) then mark StatusId pending      
     UPDATE PatientAppointment
       SET
           StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Missed'
     ),
           StatusDate = @Currentdate
     WHERE StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Care Ended'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientId IN
     (
         SELECT PatientId
         FROM Patient
         WHERE Active = 1
     );
     UPDATE PatientAppointment
       SET
           StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Pending'
     ),
	StatusDate = @Currentdate
     WHERE StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'CareEnded'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND DATEADD(dd,
                      (
                          SELECT appgraceperiod
                          FROM mst_facility
                          WHERE facilityid = @locationid
                      ), StatusDate) >= @Currentdate;


     -----------------Update missed to Previously missed status---------------------------------      
     UPDATE PatientAppointment
       SET
           PatientAppointment.StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Previously Missed'
     ),
           StatusDate = @Currentdate
     WHERE PatientAppointment.StatusId =
     (
         SELECT LookupItemId
         FROM LookupMasterItem
         WHERE DisplayName LIKE 'Missed'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientAppointment.AppointmentDate <
     (
         SELECT MAX(b.StatusDate)
         FROM PatientAppointment b
         WHERE b.StatusId IN
         (
         (
             SELECT LookupItemId
             FROM LookupMasterItem
             WHERE DisplayName LIKE 'Pending'
         ),
         (
             SELECT LookupItemId
             FROM LookupMasterItem
             WHERE DisplayName LIKE 'Met'
         )
         )
               AND b.PatientId = PatientAppointment.PatientId
               AND (deleteflag IS NULL
                    OR deleteflag != 1)
     );
     --End

GO

/****** Object:  StoredProcedure [dbo].[pr_selectedListValue_Futures]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_selectedListValue_Futures]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pr_selectedListValue_Futures] AS' 
END
GO

ALTER procedure [dbo].[pr_selectedListValue_Futures]                                  
as                                  
begin            
   declare @CategoryId int            
   declare @BindTable varchar(300)            
   declare @FieldName varchar(300)            
   declare @Predefined int            
   declare @FieldId int            
   declare @ModuleId int 
   declare @RegFlag int           
   declare @selList varchar(MAX)            
            
   Create table #tmpList(FieldId int,FieldName Varchar(300), FieldValue varchar(max), Predefined int,             
    CodeId varchar(max),BindTable Varchar(300),ModuleId int)            
            
   declare ListFields cursor for            
   select '9999'+Convert(varchar,Id) [FieldId],isnull(CategoryId,0),BindTable,PdfName [FieldName],'1'[Predefined],ModuleId,ISNULL(PatientRegistration, 0) from mst_predefinedfields where controlid in (4,9,15,6)            
   union            
   select '8888'+Convert(varchar,Id) [FieldId],isnull(CategoryId,0),BindTable,FieldName [FieldName],'0'[Predefined],0 [ModuleId],ISNULL(PatientRegistration, 0) from mst_CustomFormField where controlid in (4,9,15,6)            
   open ListFields            
   fetch next from ListFields into @FieldId,@CategoryId,@BindTable,@FieldName,@Predefined,@ModuleId,@RegFlag             
   while @@fetch_status = 0            
     begin            
  set @selList = ''                        
        exec pr_GetListFormBuilder_Futures @BindTable,@CategoryId,@FieldName,@ModuleId,@RegFlag ,@selList output                        
        insert into #tmpList(FieldId,FieldName,FieldValue,Predefined,CodeId,BindTable,ModuleId)            
        values(@FieldId,@FieldName,isnull(@selList,''),@Predefined,@CategoryId,@BindTable,@ModuleId)                
        fetch next from ListFields into  @FieldId,@CategoryId,@BindTable,@FieldName,@Predefined,@ModuleId,@RegFlag             
    end                        
   close ListFields            
   deallocate ListFields            
   select * from #tmpList            
   drop table #tmpList                               
end 
GO
/****** Object:  StoredProcedure [dbo].[Pr_SF_GetPatientSearchresults]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_SF_GetPatientSearchresults]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Pr_SF_GetPatientSearchresults] AS' 
END
GO
ALTER PROCEDURE [dbo].[Pr_SF_GetPatientSearchresults]                                                              
(                                                                                                              
@lastname varchar(50)=null,                                                                                  
@middlename varchar(50)=null,                                                                                                              
@firstname varchar(50)=null,                                                                                                              
@enrollmentid varchar(50)=null,                                                                                                              
@ClinicId int=null,                                                                                                              
@gender int=null, 
@StateId int=null, 
@DistrictId int=null,                                                                                                             
@BlockId int=null,
@UniqueId varchar(50)=null,                                                    
@password varchar(50)=null                                                                                       
                                                                                                              
)                                                                                                              
AS                                                                                                              
BEGIN                                                                                     
                                                                                                              
 declare @sql varchar(max)                                                                                                              
 declare @sqlwhere varchar(max)                                                                                                              
 declare @sqlclause varchar(max)                             
                                                                                     
                                                                                       
 Declare @SymKey varchar(400)                                                                                      
 Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                                                                          
 exec(@SymKey)                                                                                
                                                                                         
 set @sqlclause = 'SELECT Ptn_Pk,                                                                                        
  convert(varchar(50), decryptbykey(Mst_Patient.firstname)) +'' ''+                                                                                  
  convert(varchar(50), decryptbykey(Mst_Patient.middlename))+'' ''+                                                                             
  convert(varchar(50), decryptbykey(Mst_Patient.lastname))[PatientName],                                                
  Mst_Patient.UniqueID[UniqueID],mst_Clinic.ClinicName                                      
  from mst_patient left outer join mst_Clinic on mst_patient.ClinicId=mst_Clinic.ClinicID'                                                  
 set @sqlwhere =  ' WHERE (mst_patient.DeleteFlag = 0 or mst_patient.DeleteFlag is null)'                                                            
                                                                                        
 set @sql=''                                                                                   
                                                                                                               
 if @firstname <> ''      
     begin                                                                                            
   set @sql=  @sql + ' AND convert(varchar(50), decryptbykey(Mst_Patient.firstname)) like ' + '''' + @firstname + '%' + ''''                                                     
  End        
                                                                        
 if @middlename <> ''       
   begin                                                                                                              
  set @sql=  @sql + ' AND convert(varchar(50), decryptbykey(Mst_Patient.middlename)) like ' + '''' + @middlename + '%' + ''''                                                                            
    End         
                                                                      
 if @lastname <> ''      
   begin                                                                                                                
   set @sql=  @sql + ' AND convert(varchar(50), decryptbykey(Mst_Patient.lastname)) like ' + '''' + @lastname + '%'+''''                                                                                                               
   End         
                                                                                                    
                                     
                                   
                                                                                                
 if @ClinicId > 0 and @ClinicId < 9999       
     begin               
   set @sql = @sql + ' AND Mst_Patient.ClinicId='+convert(varchar,@ClinicId)        
  End                                        
                              
 if @gender > 0       
      Begin                                               
   set @sql=  @sql + ' AND Mst_Patient.Sex = ' + convert(varchar,@gender)        
      End                                                                                                            
  if @StateId > 0       
      Begin                                               
   set @sql=  @sql + ' AND Mst_Patient.State = ' + convert(varchar,@StateId)        
      End
      if @DistrictId > 0       
      Begin                                               
   set @sql=  @sql + ' AND Mst_Patient.District = ' + convert(varchar,@DistrictId)        
      End
      if @BlockId > 0       
      Begin                                               
   set @sql=  @sql + ' AND Mst_Patient.Block = ' + convert(varchar,@BlockId)        
      End 
   
   if @UniqueId <> ''       
      Begin                                               
   set @sql=  @sql + ' AND Mst_Patient.UniqueId = ' + convert(varchar,@UniqueId)        
      End                                                                                                        
                                                                                                                
     
        
     
                                                                  
 set @sqlclause = @sqlclause + @sqlwhere + @sql + ' Order by firstname,middlename,lastname'                                                                                          
print(@sqlclause)        
--print(@sql)        
--print(@sqlwhere)                          
exec (@sqlclause)                                                                                                 
                                                                                     
Close symmetric key Key_CTC                                                           
                                                                                                          
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Bluecard_ToGreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Bluecard_ToGreenCard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_Bluecard_ToGreenCard] AS' 
END
GO

-- =============================================
-- Author:		<felix/stephen>
-- Create date: <03-22-2017>
-- Description:	<Patient registration migration from bluecard to greencard>
-- =============================================
ALTER PROCEDURE [dbo].[SP_Bluecard_ToGreenCard]
	-- Add the parameters for the stored procedure here
	@ptn_pk int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @FirstName varbinary(max), @MiddleName varbinary(max), @LastName varbinary(max), @Sex int, @Status bit , @DeleteFlag bit, 
		@CreateDate datetime, @UserID int,  @message varchar(80), @Id int, @PatientFacilityId varchar(50), @PatientType int, 
		@FacilityId varchar(10), @DateOfBirth datetime, @DobPrecision int, @NationalId varchar(100), @PatientId int,  
		@ARTStartDate date,@transferIn int,@CCCNumber varchar(20), @entryPoint int, @ReferredFrom int, @RegistrationDate datetime,
		@MaritalStatusId int, @MaritalStatus int, @DistrictName varchar(50), @CountyID int, @SubCountyID int, @WardID int,
		@Address varbinary(max), @Phone varbinary(max), @EnrollmentId int, @PatientIdentifierId int, @ServiceEntryPointId int,
		@PatientMaritalStatusID int, @PatientTreatmentSupporterID int, @PersonContactID int, @IDNational varbinary(max);

DECLARE @FirstNameT varchar(50), @LastNameT varchar(50), @TreatmentSupportTelNumber varbinary(max), 
			@CreateDateT datetime, @UserIDT int, @IDT int;
			
DECLARE @TreatmentSupportTelNumber_VARCHAR varchar(100);
  
PRINT '-------- Patients Report --------'; 
SELECT @message = '----- ptn_pk ' + CAST(@ptn_pk as varchar(50));
PRINT @message;
  
DECLARE mstPatient_cursor CURSOR FOR   
SELECT FirstName, MiddleName ,LastName,Sex, [Status], DeleteFlag, dbo.mst_Patient.CreateDate, dbo.mst_Patient.UserID, PatientFacilityId, PosId, DOB, DobPrecision, [ID/PassportNo],PatientEnrollmentID, [ReferredFrom], [RegistrationDate], MaritalStatus, DistrictName, Address, Phone
FROM mst_Patient
INNER JOIN
 dbo.Lnk_PatientProgramStart ON dbo.mst_Patient.Ptn_Pk = dbo.Lnk_PatientProgramStart.Ptn_pk
WHERE        (dbo.Lnk_PatientProgramStart.ModuleId = 203) AND dbo.mst_Patient.Ptn_Pk = @ptn_pk
  
OPEN mstPatient_cursor  
  
FETCH NEXT FROM mstPatient_cursor   
INTO @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId,@CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    PRINT ' '  
    SELECT @message = '----- patients From mst_patient: ' + CAST(@ptn_pk as varchar(50))
  
    PRINT @message  

	exec pr_OpenDecryptedSession;

	--set null dates
	IF @CreateDate IS NULL
		BEGIN
			SELECT @CreateDate = getdate();
		END
	--Due to the logic of green card
	IF @Status = 1
		BEGIN
			SELECT @Status = 0
		END
	ELSE
		BEGIN
			SELECT @Status = 1
		END

	IF @NationalId IS NULL
		SET @IDNational = 99999999;

	IF @Sex IS NOT NULL
		SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like (select Name from mst_Decode where id = @Sex) + '%');
	ELSE
		SET @Sex = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

	--Default all persons to new
	SET @ARTStartDate=( SELECT ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk);
	if(@ARTStartDate Is NULL) BEGIN SET @PatientType=(SELECT Id FROM LookupItem WHERE Name='New');SET @transferIn=0; END ELSE BEGIN SET @PatientType=(SELECT Id FROM LookupItem WHERE Name='TransferIn');SET @transferIn=1; END
	-- SELECT @PatientType = 1285

	--encrypt nationalid
	SET @IDNational=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@IDNational);

	IF NOT EXISTS ( SELECT TOP 1 ptn_pk FROM Patient WHERE ptn_pk = @ptn_pk)
		BEGIN
			Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
			Values(@FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID);

			SELECT @Id=@@IDENTITY;
			SELECT @message = 'Created Person Id: ' + CAST(@Id as varchar(50));
			PRINT @message;

			Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
			Values(@ptn_pk, @Id, @PatientFacilityId, @PatientType, @FacilityId, @Status, @DateOfBirth, @DobPrecision, @IDNational, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

			SELECT @PatientId=@@IDENTITY;
			SELECT @message = 'Created Patient Id: ' + CAST(@PatientId as varchar);
			PRINT @message;

			-- Insert to PatientEnrollment
			INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
			VALUES (@PatientId,1,(SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk),0, @transferIn,0 ,0 ,@UserID ,@CreateDate ,NULL)
		
			SELECT @EnrollmentId=@@IDENTITY;
			SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			PRINT @message;

			IF @CCCNumber IS NOT NULL
				BEGIN
					-- Patient Identifier
					INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
					VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

					SELECT @PatientIdentifierId=@@IDENTITY;
					SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
					PRINT @message;
				END

			--Insert into ServiceEntryPoint
			IF @ReferredFrom > 0
				SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
				IF @entryPoint IS NULL
					BEGIN
						SET @entryPoint = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
					END
			ELSE
				SET @entryPoint = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

			INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
			VALUES(@PatientId, 1, @entryPoint, 0 , @UserID, @CreateDate, 0);

			SELECT @ServiceEntryPointId=@@IDENTITY;
			SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
			PRINT @message;
	
			--Insert into MaritalStatus
			IF @MaritalStatus > 0
				BEGIN
					IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
						SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
					ELSE
						SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
				END
			ELSE
				SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

			INSERT INTO PatientMaritalStatus(PersonId, MaritalStatusId, Active, DeleteFlag, CreatedBy, CreateDate)
			VALUES(@Id, @MaritalStatusId, 1, 0, @UserID, @CreateDate);

			SELECT @PatientMaritalStatusID=@@IDENTITY;
			SELECT @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
			PRINT @message;

			--Insert into PersonLocation
			--SET @CountyID = (SELECT TOP 1 CountyId from County where CountyName like '%' + @DistrictName  + '%');
			--SET @WardID = (SELECT TOP 1 WardId FROM County WHERE WardName LIKE '%' +  +'%')

			--INSERT INTO PersonLocation(PersonId, County, SubCounty, Ward, Village, Location, SubLocation, LandMark, NearestHealthCentre, Active, DeleteFlag, CreatedBy, CreateDate)
			--VALUES(@Id, @CountyID, @SubCountyID, @WardID, @Village, @Location, @SubLocation, @LandMark, @NearestHealthCentre, 1, @DeleteFlag, @UserID, @CreateDate);
    
			--Insert into Treatment Supporter
			DECLARE Treatment_Supporter_cursor CURSOR FOR
			SELECT SUBSTRING(TreatmentSupporterName,0,charindex(' ',TreatmentSupporterName))as firstname ,
			SUBSTRING(TreatmentSupporterName,charindex(' ',TreatmentSupporterName) + 1,len(TreatmentSupporterName)+1)as lastname,
			TreatmentSupportTelNumber, CreateDate, UserID
			from dtl_PatientContacts WHERE ptn_pk = @ptn_pk;

			OPEN Treatment_Supporter_cursor
			FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT , @UserIDT

			IF @@FETCH_STATUS <> 0   
				PRINT '         <<None>>'       

			WHILE @@FETCH_STATUS = 0  
			BEGIN  

				--SELECT @message = '         ' + @product  
				--PRINT @message
				--SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR);
				IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL 
					BEGIN
						Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
						Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, @CreateDateT, @UserIDT);

						SELECT @IDT=@@IDENTITY;
						SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
						PRINT @message;

						IF @TreatmentSupportTelNumber_VARCHAR IS NOT NULL
						 SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR)

						INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
						VALUES(@Id, @IDT, @TreatmentSupportTelNumber, 0, @UserIDT, @CreateDateT);

						SELECT @PatientTreatmentSupporterID=@@IDENTITY;
						SELECT @message = 'Created PatientTreatmentSupporterID Id: ' + CAST(@PatientTreatmentSupporterID as varchar);
						PRINT @message;
					END

				FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT, @UserIDT
				END  

			CLOSE Treatment_Supporter_cursor  
			DEALLOCATE Treatment_Supporter_cursor

			--Insert into Person Contact
			IF @Address IS NOT NULL AND @Phone IS NOT NULL
				BEGIN
					INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
					VALUES(@Id, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

					SELECT @PersonContactID=@@IDENTITY;
					SELECT @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
					PRINT @message;
				END
		END
	ELSE
		BEGIN
			SET @Id = (SELECT TOP 1 PersonId FROM Patient WHERE ptn_pk = @ptn_pk);
			UPDATE Person
			SET FirstName = @FirstName, MidName = @MiddleName, LastName = @LastName, Sex = @Sex, Active = @Status, DeleteFlag = @DeleteFlag, CreateDate = @CreateDate, CreatedBy = @UserID
			WHERE Id = @Id;

			SELECT @message = 'Update Person Id: ' + CAST(@Id as varchar(50));
			PRINT @message;

			PRINT @Status;

			UPDATE Patient
			SET PatientIndex = @PatientFacilityId, PatientType = @PatientType, FacilityId = @FacilityId, Active = @Status, DateOfBirth = @DateOfBirth, DobPrecision = @DobPrecision, NationalId = @IDNational, DeleteFlag = @DeleteFlag, CreatedBy = @UserID, CreateDate = @CreateDate, RegistrationDate = @RegistrationDate
			WHERE PersonId = @Id;

			SELECT @PatientId=(SELECT TOP 1 Id FROM Patient WHERE PersonId = @Id);
			SELECT @message = 'Updated Patient';
			PRINT @message;

			-- UPDATE to PatientEnrollment
			UPDATE PatientEnrollment
			SET EnrollmentDate = (SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk), EnrollmentStatusId = 0, TransferIn = @transferIn, CareEnded = 0, DeleteFlag = 0, CreatedBy = @UserID, CreateDate = @CreateDate
			WHERE PatientId = @PatientId;

			SELECT @EnrollmentId = (SELECT TOP 1 Id FROM PatientEnrollment WHERE PatientId = @PatientId);
			SELECT @message = 'Updated PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			PRINT @message;

			BEGIN
			IF @CCCNumber IS NOT NULL
				BEGIN
				IF NOT EXISTS ( SELECT PatientId FROM PatientIdentifier WHERE PatientId = @PatientId AND PatientEnrollmentId = @EnrollmentId AND IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber'))
					BEGIN
						-- Patient Identifier
						INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
						VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

						SELECT @PatientIdentifierId=@@IDENTITY;
						SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
						PRINT @message;
					END
				ELSE
					BEGIN
						UPDATE PatientIdentifier
						SET IdentifierTypeId = (select top 1 Id from Identifiers where Code='CCCNumber'), IdentifierValue = @CCCNumber, DeleteFlag = 0, CreatedBy = @UserID, CreateDate = @CreateDate, Active = 0
						WHERE PatientId = @PatientId AND PatientEnrollmentId = @EnrollmentId AND IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber')
					END
				END
			END

			--Insert into ServiceEntryPoint
			IF @ReferredFrom > 0
				BEGIN
					SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
					
					IF @entryPoint IS NULL
						BEGIN
							SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
						END
						
					UPDATE ServiceEntryPoint
					SET EntryPointId = @entryPoint, CreatedBy = @UserID, CreateDate = @CreateDate
					WHERE PatientId = @PatientId;
					
					SELECT @ServiceEntryPointId=@@IDENTITY;
					SELECT @message = 'Updated ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
					PRINT @message;
				END
	
			--Updated into MaritalStatus
			IF @MaritalStatus > 0
				BEGIN
					BEGIN
						IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
							SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
						ELSE
							SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
					END
					UPDATE PatientMaritalStatus
					SET MaritalStatusId = @MaritalStatusId, CreatedBy = @UserID, CreateDate = @CreateDate
					WHERE PersonId = @Id;

					SELECT @PatientMaritalStatusID=@@IDENTITY;
					SELECT @message = 'Updated PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
					PRINT @message;
				END

			--Update into Treatment Supporter
			DECLARE Treatment_Supporter_cursor CURSOR FOR
			SELECT SUBSTRING(TreatmentSupporterName,0,charindex(' ',TreatmentSupporterName))as firstname ,
			SUBSTRING(TreatmentSupporterName,charindex(' ',TreatmentSupporterName) + 1,len(TreatmentSupporterName)+1)as lastname,
			TreatmentSupportTelNumber, CreateDate, UserID
			from dtl_PatientContacts WHERE ptn_pk = @ptn_pk;


			OPEN Treatment_Supporter_cursor
			FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT , @UserIDT

			IF @@FETCH_STATUS <> 0   
				PRINT '         <<None>>'       

			WHILE @@FETCH_STATUS = 0  
			BEGIN

				--SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber);
				IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL
					BEGIN
						IF NOT EXISTS (SELECT PersonId FROM PatientTreatmentSupporter WHERE PersonId = @Id)
							BEGIN
								Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
								Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, @CreateDateT, @UserIDT);

								SELECT @IDT=@@IDENTITY;
								SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
								PRINT @message;

								IF @TreatmentSupportTelNumber_VARCHAR IS NOT NULL
								SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR)

								INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
								VALUES(@Id, @IDT, @TreatmentSupportTelNumber, 0, @UserIDT, @CreateDateT);

							END
						ELSE
							BEGIN
								SET @IDT = (SELECT SupporterId FROM PatientTreatmentSupporter WHERE PersonId = @Id);

								UPDATE Person
								SET FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT)
								WHERE Id = @IDT;

								IF @TreatmentSupportTelNumber_VARCHAR IS NOT NULL
								SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR)

								UPDATE PatientTreatmentSupporter
								SET MobileContact = @TreatmentSupportTelNumber
								WHERE PersonId = @Id;

							END
						END

				FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT, @UserIDT
				END 

			CLOSE Treatment_Supporter_cursor  
			DEALLOCATE Treatment_Supporter_cursor

			--UPDATE into Person Contact
			IF @Address IS NOT NULL AND @Phone IS NOT NULL
				BEGIN
					UPDATE PersonContact
					SET PhysicalAddress = @Address, MobileNumber = @Phone
					WHERE PersonId = @Id;
				END

		END

    -- Get the next mst_patient.
    FETCH NEXT FROM mstPatient_cursor   
    INTO @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId, @CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone
END   
CLOSE mstPatient_cursor;  
DEALLOCATE mstPatient_cursor;  
END

GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterAdverseEvents] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterAdverseEvents]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update AdverseEvent set DeleteFlag = 1 where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
	
End


/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 3/22/2017 6:17:35 PM ******/
SET ANSI_NULLS ON





GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterAllergies]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterAllergies]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterAllergies] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 19th Mar 2017
-- Description:	save patient encounter - delete allergies
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterAllergies]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update patientallergy set DeleteFlag = 1 where PatientId = @PatientID
End








GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterChronicIllness]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterChronicIllness] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterChronicIllness]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update PatientChronicIllness set DeleteFlag = 1 where PatientId = @PatientID
End








GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterComplaints]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterComplaints] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 26th April 2017
-- Description:	save patient encounter - Presenting Complaints
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterComplaints]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update PresentingComplaints set DeleteFlag = 1 where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
	
End


/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 3/22/2017 6:17:35 PM ******/
SET ANSI_NULLS ON





GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterDiagnosis]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterDiagnosis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterDiagnosis] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterDiagnosis]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update PatientDiagnosis set DeleteFlag = 1 where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
End







GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterGeneralExam]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterGeneralExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterGeneralExam] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterGeneralExam]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
		declare @generalExamID int = (select top 1 Id from lookupmaster where Name = 'GeneralExamination')
		
		UPDATE PhysicalExamination SET DeleteFlag = 1
		where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and ExaminationTypeId = @generalExamID
		
End







GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterPHDP]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterPHDP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterPHDP] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterPHDP]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements. 
Set Nocount On;
-- Insert statements for procedure here
		UPDATE PatientPHDP SET DeleteFlag = 1
		where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
		
End







GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterPhysicalExam]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterPhysicalExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterPhysicalExam] AS' 
END
GO

-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterPhysicalExam]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	declare @generalExamID int = (select top 1 Id from lookupmaster where Name = 'GeneralExamination')

	update PhysicalExamination set DeleteFlag = 1 
	where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and ExaminationTypeId <> @generalExamID
End







GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterPresentingComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterPresentingComplaints]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterPresentingComplaints] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 26th April 2017
-- Description:	save patient encounter - Presenting Complaints
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterPresentingComplaints]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update PresentingComplaints set DeleteFlag = 1 where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
	
End


/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 3/22/2017 6:17:35 PM ******/
SET ANSI_NULLS ON





GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientEncounterVaccines]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientEncounterVaccines]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientEncounterVaccines] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientEncounterVaccines]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update Vaccination set DeleteFlag = 1 where PatientId = @PatientID
End








GO
/****** Object:  StoredProcedure [dbo].[sp_deletePatientFamilyPlanningMethod]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletePatientFamilyPlanningMethod]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_deletePatientFamilyPlanningMethod] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	delete family planning method
-- =============================================
ALTER PROCEDURE [dbo].[sp_deletePatientFamilyPlanningMethod]
	-- Add the parameters for the stored procedure here
	@PatientID varchar(50) = null,
	@FPId varchar(50) = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	update PatientFamilyPlanningMethod set DeleteFlag = 1 where PatientId = @PatientID and PatientFPId = @FPId
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getCurrentRegimen]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getCurrentRegimen]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getCurrentRegimen] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get current regimen
-- =============================================
ALTER PROCEDURE [dbo].[sp_getCurrentRegimen]
	-- Add the parameters for the stored procedure here
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select top 1 a.RegimenId, a.RegimenLineId, b.VisitDate
	from ARVTreatmentTracker a inner join PatientMasterVisit b on a.PatientMasterVisitId = b.id
	where a.PatientId = @PatientID
	order by b.VisitDate desc

End








GO
/****** Object:  StoredProcedure [dbo].[sp_getFacility]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getFacility]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getFacility] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 11th April 2017
-- Description:	get Facility
-- =============================================
ALTER PROCEDURE [dbo].[sp_getFacility]
	-- Add the parameters for the stored procedure here
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	select FacilityID,FacilityName,SatelliteID,DeleteFlag, UserID from mst_facility where deleteflag=0

End








GO
/****** Object:  StoredProcedure [dbo].[sp_getItemIdByGroupAndItemName]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getItemIdByGroupAndItemName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getItemIdByGroupAndItemName] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 11th April 2017
-- Description:	get ItemId By Group And ItemName
-- =============================================
ALTER PROCEDURE [dbo].[sp_getItemIdByGroupAndItemName]
	-- Add the parameters for the stored procedure here
	@groupName varchar(100) = null,
	@ItemName varchar(100) = null
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	select * from [dbo].[LookupItemView] where MasterName = @groupName and ItemName = @ItemName

End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounter]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounter] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 10th Feb 2017
-- Description:	get patient encounter
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounter]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @TBScreeningTypeID int = (Select Id from LookupMaster where name = 'TBStatus')
	declare @NutritionScreeningTypeID int = (Select Id from LookupMaster where name = 'NutritionStatus')
	declare @GeneralExamMasterId int = (Select top 1 Id from LookupMaster where name = 'GeneralExamination')
	declare @ARVAdherenceType int = (Select Id from LookupMaster where name = 'ARVAdherence')
	declare @CTXAdherenceType int = (Select Id from LookupMaster where name = 'CTXAdherence')

	--0
	select * from PatientMasterVisit where id = @PatientMasterVisitID and patientId = @PatientID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--1
	select * from ComplaintsHistory where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--2
	select * from PhysicalExamination where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and ExaminationTypeId = @GeneralExamMasterId and (DeleteFlag is null OR DeleteFlag = 0)
	
	--TB Screening 3
	select ScreeningValueId from PatientScreening 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and ScreeningTypeId = @TBScreeningTypeID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--Nutrition Screening 4
	select ScreeningValueId from PatientScreening 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and ScreeningTypeId = @NutritionScreeningTypeID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--5
	select * from PatientPHDP where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID 
	and (DeleteFlag is null OR DeleteFlag = 0)

	--6 ARV Adherence
	select Score from AdherenceOutcome 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and AdherenceType = @ARVAdherenceType
	and (DeleteFlag is null OR DeleteFlag = 0)

	--7 CTX Adherence
	select Score from AdherenceOutcome 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and AdherenceType = @CTXAdherenceType
	and (DeleteFlag is null OR DeleteFlag = 0)

	--8 workplan
	select * from PatientClinicalNotes
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--9 ICF
	select * from [dbo].[PatientIcf]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--10 ICF Action
	select * from [dbo].[PatientIcfAction]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--11 IPT
	select * from [dbo].[PatientIpt]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--12 IPT Outcome
	select * from [dbo].[PatientIptOutcome]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--13 IPT Workup
	select * from [dbo].[PatientIptWorkup]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

End





GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterAdverseEvents] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterAdverseEvents]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select Severity SeverityID,EventName,EventCause,b.DisplayName Severity,[Action] 
	from AdverseEvent a left join LookupItem b on a.Severity = b.Id
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterAllergies]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterAllergies]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterAllergies] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 19th Mar 2017
-- Description:	get patient encounter Allergies
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterAllergies]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select Allergen allergyId,reaction reactionId, severity severityId, b.displayname allergy, c.DisplayName reaction, 
	d.DisplayName severity, CONVERT(varchar(20),a.onsetdate,106) onsetDate
	from patientallergy a inner join lookupitem b on a.allergen = b.Id
	left join lookupitem c on a.Reaction = c.id
	left join lookupitem d on a.severity = d.id
	where patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterChronicIllness]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterChronicIllness] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterChronicIllness]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select chronicIllness chronicIllnessID, b.DisplayName chronicIllnessName, Treatment,dose,
	convert(varchar(20),OnsetDate,106) OnsetDate,active 
	from PatientChronicIllness a inner join LookupItem b on a.ChronicIllness = b.Id
	where patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterComplaints]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterComplaints] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter complaints
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterComplaints]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select presentingComplaintsId, b.DisplayName complaint, convert(varchar(20), onsetDate, 106) onsetDate
	from presentingComplaints a left join LookupItem b on a.presentingComplaintsId = b.Id
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterDiagnosis]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterDiagnosis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterDiagnosis] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Diagnosis
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterDiagnosis]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select a.Diagnosis, b.DisplayName, ManagementPlan
	from PatientDiagnosis a inner join lookupitem b on a.diagnosis = b.id
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterExam]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterExam] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Physical Exam
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterExam]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select ExaminationTypeId examTypeID, ExamId examID, b.DisplayName examType, c.DisplayName exam, Finding findings
	from PhysicalExamination a 
	inner join LookupItem b on a.ExaminationTypeId = b.Id
	left join LookupItem c on a.ExamId = c.Id
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterHistory]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterHistory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterHistory] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 10th Feb 2017
-- Description:	get patient encounter History
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterHistory]
	-- Add the parameters for the stored procedure here
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @enrollmentVisitType int = (select id from lookupitem where name = 'Enrollment')

	SELECT        dbo.PatientMasterVisit.Id AS visitID, 'Green Card' AS VisitName, dbo.PatientMasterVisit.PatientId, dbo.PatientMasterVisit.VisitDate, dbo.mst_User.UserName, 
							 dbo.PatientMasterVisit.DeleteFlag
	FROM            dbo.PatientMasterVisit INNER JOIN
							 dbo.mst_User ON dbo.PatientMasterVisit.CreatedBy = dbo.mst_User.UserID
	WHERE  dbo.PatientMasterVisit.PatientId = @PatientID and  (dbo.PatientMasterVisit.VisitDate IS NOT NULL) AND (dbo.PatientMasterVisit.DeleteFlag IS NULL OR
							 dbo.PatientMasterVisit.DeleteFlag = 0) and dbo.PatientMasterVisit.visittype is null
	UNION
	SELECT        dbo.PatientMasterVisit.Id AS visitID, 'Pharmacy' AS VisitName, dbo.PatientMasterVisit.PatientId, dbo.PatientMasterVisit.VisitDate, dbo.mst_User.UserName, 
							 dbo.PatientMasterVisit.DeleteFlag
	FROM            dbo.PatientMasterVisit INNER JOIN
							 dbo.mst_User ON dbo.PatientMasterVisit.CreatedBy = dbo.mst_User.UserID INNER JOIN
							 ord_patientpharmacyorder ON PatientMasterVisit.Id = ord_patientpharmacyorder.patientmastervisitid
	WHERE  dbo.PatientMasterVisit.PatientId = @PatientID and (dbo.PatientMasterVisit.VisitDate IS NOT NULL) AND (dbo.PatientMasterVisit.DeleteFlag IS NULL OR
							 dbo.PatientMasterVisit.DeleteFlag = 0) and dbo.PatientMasterVisit.visittype is null


	order by visitdate desc
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterVaccines]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterVaccines]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterVaccines] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Vaccines
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterVaccines]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select vaccine vaccineID, VaccineStage VaccineStageID, b.DisplayName VaccineName, c.DisplayName VaccineStageName, Convert(varchar(12),VaccineDate,106)VaccineDate
	from Vaccination a 
	inner join LookupItem b on a.Vaccine = b.Id
	left join LookupItem c on a.VaccineStage = c.Id
	where patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPatientPharmacyPrescription]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientPharmacyPrescription]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientPharmacyPrescription] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 13th Mar 2017
-- Description:	get patient pharmacy prescription
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientPharmacyPrescription]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @pharmacy_pk int
	set @pharmacy_pk = (select ptn_pharmacy_pk from ord_PatientPharmacyOrder 
						where PatientMasterVisitId = @PatientMasterVisitID and DeleteFlag <> 1)

	select a.Drug_Pk,
	--(select batchId from dtl_patientPharmacyDispensed where ptn_pharmacy_pk = a.ptn_pharmacy_pk and drug_pk = a.Drug_Pk) batchId,
	a.BatchNo batchId,
	a.FrequencyID,b.abbreviation abbr,b.DrugName,c.Name batchName,a.SingleDose dose, 
	d.Name freq,a.duration,a.OrderedQuantity,a.DispensedQuantity,
	--(select dispensedQuantity from dtl_patientPharmacyDispensed where ptn_pharmacy_pk = a.ptn_pharmacy_pk and drug_pk = a.Drug_Pk)DispensedQuantity,
	a.Prophylaxis
	from dtl_PatientPharmacyOrder a inner join mst_drug b on a.Drug_Pk = b.Drug_pk
	left join Mst_Batch c on a.BatchNo = c.ID
	left join mst_Frequency d on a.FrequencyID = d.ID
	--left join dtl_patientPharmacyDispensed e on a.ptn_pharmacy_pk = e.ptn_pharmacy_pk
	where a.ptn_pharmacy_pk = @pharmacy_pk
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPendingPrescriptions]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPendingPrescriptions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPendingPrescriptions] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 16th Mar 2017
-- Description:	get patient encounter Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPendingPrescriptions]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select a.PatientMasterVisitId, a.Ptn_pk, e.identifiervalue, c.FirstName, c.MidName, c.LastName, d.UserLastName + d.UserFirstName as prescribedBy 
	from ord_PatientPharmacyOrder a inner join patient b on a.ptn_pk = b.Id
	inner join person c on b.PersonId = c.Id
	inner join mst_User d on a.OrderedBy = d.UserID
	inner join PatientIdentifier e on e.PatientId = a.Ptn_pk
	where (a.DeleteFlag is null or a.DeleteFlag = 0) and (a.orderstatus = 1 or a.orderstatus = 3)
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyBatch]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyBatch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyBatch] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get pharmacy drug batches
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyBatch]
	-- Add the parameters for the stored procedure here
	@DrugPk int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select a.id, a.Name + ' ~ Expiry Date: ' + Convert(varchar(20), a.ExpiryDate,106) + ' ~ Quantity: ' + CONVERT(varchar(20),sum(b.Quantity)) as Name 
	from Mst_Batch a inner join Dtl_StockTransaction b on a.id = b.batchid
	where a.ItemID = @DrugPk and a.DeleteFlag <> 1
	group by a.id, a.Name, a.ExpiryDate
	order by a.ExpiryDate asc

End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugFrequency]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugFrequency]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyDrugFrequency] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 3rd June 2017
-- Description:	get pharmacy drug frequency
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyDrugFrequency]
	-- Add the parameters for the stored procedure here

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select id,name,multiplier from mst_Frequency where DeleteFlag <> 1
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyDrugList] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get pharmacy drug list
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyDrugList]
	-- Add the parameters for the stored procedure here
	@pmscm int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	--select Drug_pk, DrugName,CONCAT(Drug_pk, '~',abbreviation, '~', DrugName)val 
	--from mst_drug

	IF(@pmscm = 1)
	BEGIN
		Select	D.Drug_pk, D.DrugName,		Convert(varchar(5),D.Drug_pk) + ' '+ '~' + isnull(D.abbreviation,'') + '~'+ D.DrugName val 
		From Dtl_StockTransaction As ST	Inner Join Mst_Store As S On S.Id = ST.StoreId And S.DispensingStore = 1
		Right Outer Join Mst_Drug As D On D.Drug_pk = ST.ItemId 
		where D.DeleteFlag = 0
		Group By D.Drug_pk,	D.DrugName, D.abbreviation
		having Sum(ST.Quantity) > 0
	END
	ELSE
	BEGIN
		Select	D.Drug_pk, D.DrugName,
		Convert(varchar(5),D.Drug_pk) + ' '+ '~' + isnull(D.abbreviation,'') + '~'+ D.DrugName val 
		From Dtl_StockTransaction As ST	Inner Join Mst_Store As S On S.Id = ST.StoreId And S.DispensingStore = 1
		Right Outer Join Mst_Drug As D On D.Drug_pk = ST.ItemId 
		where D.DeleteFlag = 0
		Group By D.Drug_pk,	D.DrugName, D.abbreviation
	END

End







GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugSwitchSubReasons]    Script Date: 5/22/2017 6:10:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugSwitchSubReasons]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyDrugSwitchSubReasons] AS' 
END
GO
-- =============================================-- Author:		John Macharia-- Create date: 8th Mar 2017-- Description:	get pharmacy drug switch, substitution reasons-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyDrugSwitchSubReasons]
-- Add the parameters for the stored procedure here
@TreatmentPlan VARCHAR(50) = NULL
AS
     BEGIN
         -- SET NOCOUNT ON added to prevent extra result sets from-- interfering with SELECT statements.
         SET NOCOUNT ON;

         --select LookupItemId, DisplayName from LookupMasterItem where DisplayName = @TreatmentPlan
         SELECT ItemId,
                DisplayName
         FROM LookupItemView
         WHERE MasterId =
         (
             SELECT Id
             FROM LookupMaster
             WHERE DisplayName = @TreatmentPlan
         ) ORDER BY OrdRank;
     END;
GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyFields]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyFields]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyFields] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get pharmacy fields
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyFields]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

		--13 Pharmacy Parameters
	select b.ProgID, b.pharmacyPeriodTaken ,a.TreatmentStatusId,a.TreatmentStatusReasonId, a.RegimenLineId, a.RegimenId
	from ARVTreatmentTracker a inner join ord_PatientPharmacyOrder b on a.PatientMasterVisitId = b.PatientMasterVisitId
	where a.PatientMasterVisitId = @PatientMasterVisitID
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyRegimens]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyRegimens]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyRegimens] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 8th Mar 2017
-- Description:	get pharmacy drug switch, substitution reasons
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyRegimens]
	-- Add the parameters for the stored procedure here
	@regimenLine int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On

	select LookupItemId, b.name + '(' + b.displayname + ')' DisplayName from LookupMasterItem a inner join lookupitem b on a.lookupitemid = b.id
	where a.LookupMasterId = @regimenLine
	order by OrdRank
End








GO
/****** Object:  StoredProcedure [dbo].[sp_getRegimenClassification]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getRegimenClassification]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getRegimenClassification] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get regimen classification
-- =============================================
ALTER PROCEDURE [dbo].[sp_getRegimenClassification]
	-- Add the parameters for the stored procedure here

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	declare @lookUpMasterID int = (select Id from lookupmaster where name = 'RegimenClassification')

	select * from lookupmasteritem where lookupmasterid = @lookUpMasterID order by ordrank

End








GO
/****** Object:  StoredProcedure [dbo].[sp_getTreatmentProgram]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getTreatmentProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getTreatmentProgram] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 4th May 2017
-- Description:	get pharmacy Treatment Program
-- =============================================
ALTER PROCEDURE [dbo].[sp_getTreatmentProgram]
	-- Add the parameters for the stored procedure here
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	select id, name from [dbo].[mst_decode] where codeid = 33 and deleteflag = 0

End








GO
/****** Object:  StoredProcedure [dbo].[sp_getZScores]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getZScores]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getZScores] AS' 
END
GO
-- =============================================      
-- Author:  John Macharia      
-- Create date:       
-- Modify date: 31 July 2014    
-- Description: Paediatric  scores    
-- =============================================  
ALTER procedure [dbo].[sp_getZScores]
(
	@PatientID int=null,
	@sex varchar(10)=null,
	@height float = null
)

As

begin
Declare @ageInDays int, @ageInMonths int;

Select @ageInMonths = Datediff(Month, DateOfBirth, Getdate()),
	@ageInDays = Datediff(Day, DateOfBirth, Getdate())
From patient
Where Id = @PatientID;


--0 weight for Age
If (@ageInDays <= 1856) 
Begin
	Select *
	From [dbo].[z_waz_young]
	Where Case  When @Sex= 'Female' And Sex= 2 Then 1
				When @Sex = 'Male' And Sex = 1 Then 1
				Else 0 End = 1
	And agedays = @ageInDays;
End
Else If(@ageInMonths >=61)
Begin
	Select *
	From [dbo].[z_waz_old]
	Where Case  When @Sex= 'Female' And Sex= 2 Then 1
				When @Sex = 'Male' And Sex = 1 Then 1
				Else 0 End = 1
	And ageMos = @ageInMonths;
End
Else
Begin	
	Select 1
End
--0 weight for Age
--If (@sex = 'Female' And @ageInDays <= 1856) Begin
--Select * From [dbo].[z_waz_young] Where Sex = 2 And agedays = @ageInDays
--End 
--Else If (@sex = 'Male' And @ageInDays <= 1856) Begin
--Select * From [dbo].[z_waz_young] Where Sex = 1 And agedays = @ageInDays
--End 

--Else If (@sex = 'Female' And @ageInMonths >= 61) Begin
--Select *
--From [dbo].[z_waz_old]
--Where Sex = 2
--And Agemos = @ageInMonths
--End Else If (@sex = 'Male' And @ageInMonths >= 61) Begin
--Select *
--From [dbo].[z_waz_old]
--Where Sex = 1
--And Agemos = @ageInMonths
--End Else Begin
--Select 1
--End

--1 weight for Height

If (@height Between 45 And 110) 
Begin
	Select *
	From [dbo].[z_whz_young]
	Where Case  When @Sex= 'Female' And Sex= 2 Then 1
				When @Sex = 'Male' And Sex = 1 Then 1
				Else 0 End = 1
	And LengthCm = @height;
End
Else If (@height Between 65 And 120) 
Begin
	Select *
	From [dbo].[z_whz_old]
	Where Case  When @Sex= 'Female' And Sex= 2 Then 1
				When @Sex = 'Male' And Sex = 1 Then 1
				Else 0 End = 1
	And HeightCm = @height;
End
Else
Begin	
	Select 1
End

--If (@sex = 'Female' And @height >= 45 And @height <= 110) Begin
--Select *
--From [dbo].[z_whz_young]
--Where Sex = 2
--And LengthCm = @height
--End 
--Else If (@sex = 'Male' And @height >= 45 And @height <= 110) Begin
--Select *
--From [dbo].[z_whz_young]
--Where Sex = 1
--And LengthCm = @height
--End 
--Else If (@sex = 'Female' And @height >= 65 And @height <= 120) Begin
--Select *
--From [dbo].[z_whz_old]
--Where Sex = 2
--And HeightCm = @height
--End Else If (@sex = 'Male' And @height >= 65 And @height <= 120) Begin
--Select *
--From [dbo].[z_whz_old]
--Where Sex = 1
--And HeightCm = @height
--End Else Begin
--Select 1
--End

/* 2 BMIz */
If (@ageInDays Between 0 And 1856) 
Begin
	Select *
	From [dbo].z_bmiz_young
	Where Case  When @Sex= 'Female' And Sex= 2 Then 1
				When @Sex = 'Male' And Sex = 1 Then 1
				Else 0 End = 1
	And agedays = @ageInDays;
End
Else If (@ageInMonths Between 61 And 229) 
Begin
	Select *
	From [dbo].[z_bmiz_old]
	Where Case  When @Sex= 'Female' And Sex= 2 Then 1
				When @Sex = 'Male' And Sex = 1 Then 1
				Else 0 End = 1
	And Agemos = @ageInMonths;
End
Else
Begin	
	Select 1
End

--If (@sex = 'Female' And @ageInDays >= 0 And @ageInDays <= 1856) Begin
--Select *
--From z_bmiz_young
--Where Sex = 2
--And agedays = @ageInDays
--End Else If (@sex = 'Male' And @ageInDays >= 0 And @ageInDays <= 1856) Begin
--Select *
--From z_bmiz_young
--Where Sex = 1
--And agedays = @ageInDays
--End Else If (@sex = 'Female' And @ageInMonths >= 61 And @ageInMonths <= 229) Begin
--Select *
--From z_bmiz_old
--Where Sex = 2
--And Agemos = @ageInMonths
--End Else If (@sex = 'Male' And @ageInMonths >= 61 And @ageInMonths <= 229) Begin
--Select *
--From z_bmiz_old
--Where Sex = 1
--And Agemos = @ageInMonths
--End Else Begin
--Select 1
--End
End


GO
/****** Object:  StoredProcedure [dbo].[FamilyTesting_To_Greencard]    Script Date: 7/12/2017 11:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FamilyTesting_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FamilyTesting_To_Greencard] AS' 
END
GO
-- =============================================
-- Author: Felix
-- Create date: 12-Jul-2017
-- Description:	move family testing to greencard
-- =============================================
ALTER PROCEDURE [dbo].[FamilyTesting_To_Greencard]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Ptn_pk int, @RFirstName varbinary(max), @RLastName varbinary(max), @Sex int, @AgeYear int, @AgeMonth int,
			@RelationshipDate datetime, @RelationshipType int, @HivStatus int, @HivCareStatus int, @RegistrationNo int,
			@FileNo int, @ReferenceId int, @UserId int, @DeleteFlag int, @CreateDate datetime, @UpdateDate datetime,
			@message varchar(100), @DOB datetime, @StartDate datetime, @PersonId int, @PatientId int, @RelationshipTypeId int,
			@BaselineResult int, @HivStatusString varchar(20), @PatientMasterVisitId int, @VisitType int;

	PRINT '-------- Family Testing Migration --------';  
	exec pr_OpenDecryptedSession;

	DECLARE familyTesting_cursor CURSOR FOR  
	SELECT dbo.dtl_FamilyInfo.Ptn_pk, RFirstName, RLastName, Sex, AgeYear, AgeMonth, RelationshipDate, RelationshipType, HivStatus, HivCareStatus, RegistrationNo, FileNo, ReferenceId, dbo.dtl_FamilyInfo.UserId, DeleteFlag, dbo.dtl_FamilyInfo.CreateDate, dbo.dtl_FamilyInfo.UpdateDate, dbo.Lnk_PatientProgramStart.StartDate
	FROM    dbo.dtl_FamilyInfo INNER JOIN dbo.Lnk_PatientProgramStart ON dbo.dtl_FamilyInfo.Ptn_pk = dbo.Lnk_PatientProgramStart.Ptn_pk
	WHERE dbo.Lnk_PatientProgramStart.ModuleId = 203;

	OPEN familyTesting_cursor;

	FETCH NEXT FROM familyTesting_cursor   
	INTO @ptn_pk, @RFirstName, @RLastName, @Sex, @AgeYear, @AgeMonth, @RelationshipDate, @RelationshipType, @HivStatus, @HivCareStatus, @RegistrationNo, @FileNo, @ReferenceId, @UserId, @DeleteFlag, @CreateDate, @UpdateDate, @StartDate

	WHILE @@FETCH_STATUS = 0
	BEGIN
		BEGIN TRANSACTION

		PRINT ' '  
		SELECT @message = '----- Family Testing: ' + CAST(@ptn_pk as varchar(50));

		IF @Sex IS NOT NULL
			BEGIN
				IF ((select top 1  Name from mst_Decode where id = @Sex) = 'Male' OR (select top 1 Name from mst_Decode where id = @Sex) = 'Female')
					BEGIN
						SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like + (select top 1  Name from mst_Decode where id = @Sex) + '%');
					END
				ELSE
					SET @Sex = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
			END
		ELSE
			SET @Sex = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

		BEGIN
			SET @DOB = DATEADD(year, -@AgeYear, @StartDate);
			SET @DOB = DATEADD(month, -@AgeMonth, @DOB);
		END;

		IF @CreateDate IS NULL
			BEGIN
				SET @CreateDate = @StartDate;
			END

		INSERT INTO Person(FirstName, MidName, LastName, Sex, DateOfBirth, DobPrecision, Active, DeleteFlag, CreateDate, CreatedBy)
		VALUES(@RFirstName, NULL, @RLastName, @Sex, @DOB, 1, 1, 0, @CreateDate, @UserId);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into person';
				RETURN
			END

		SELECT @PersonId = SCOPE_IDENTITY();
		SELECT @message = 'Created Person Id: ' + CAST(@PersonId as varchar(50));
		PRINT @message;

		SET @PatientId = (select Id from Patient where ptn_pk = @Ptn_pk);

		IF @RelationshipType IS NOT NULL
			BEGIN
				IF EXISTS ((SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%Relationship%' and ItemName like + '%' + (select top 1  Name from mst_Decode where id = @RelationshipType) + '%'))
					BEGIN
						SET @RelationshipTypeId = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%Relationship%' and ItemName like + '%' + (select top 1  Name from mst_Decode where id = @RelationshipType) + '%');
					END
				ELSE
					SET @RelationshipTypeId = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
			END
		ELSE
			SET @RelationshipTypeId = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');


		INSERT INTO [dbo].[PersonRelationship](PersonId, RelatedTo, RelationshipTypeId, DeleteFlag, CreatedBy, CreateDate)
		VALUES(@PersonId, @PatientId, @RelationshipTypeId, 0, @UserId, @CreateDate);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into PersonRelationship';
				RETURN
			END

		SELECT @message = 'Created PersonRelationship Id: ' + CAST(SCOPE_IDENTITY() as varchar(50));
		PRINT @message;


		SET @HivStatusString = (SELECT TOP 1 Name FROM mst_Decode WHERE CodeID=10 AND ID=@HivStatus);


		SET @BaselineResult = CASE @HivStatusString  
			 WHEN 'Positive' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName like '%' + @HivStatusString + '%') 
			 WHEN 'Negative' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName like '%' + @HivStatusString + '%')   
			 WHEN 'Unknown' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName = 'Never Tested')
			 ELSE (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown')
		  END

		SET @VisitType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='VisitType' AND ItemName='Enrollment');

		SET @PatientMasterVisitId = (SELECT TOP 1 Id FROM PatientMasterVisit where VisitType = @VisitType and PatientId = @PatientId);

		INSERT INTO HIVTesting(PersonId, BaselineResult, BaselineDate, TestingDate, TestingResult, ReferredToCare, CCCNumber, EnrollmentId, DeleteFlag, CreatedBy, CreateDate, AuditData, PatientMasterVisitId)
		VALUES(@PersonId, @BaselineResult, @CreateDate, NULL, 0, 0, NULL, 0, 0, @UserId, @CreateDate, NULL, @PatientMasterVisitId);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into HIVTesting';
				RETURN
			END

		SELECT @message = 'Created HIVTesting Id: ' + CAST(SCOPE_IDENTITY() as varchar(50));
		PRINT @message;

	END;

	FETCH NEXT FROM familyTesting_cursor   
	INTO @ptn_pk, @RFirstName, @RLastName, @Sex, @AgeYear, @AgeMonth, @RelationshipDate, @RelationshipType, @HivStatus, @HivCareStatus, @RegistrationNo, @FileNo, @ReferenceId, @UserId, @DeleteFlag, @CreateDate, @UpdateDate, @StartDate

	CLOSE familyTesting_cursor;  
	DEALLOCATE familyTesting_cursor;  
END


GO
/****** Object:  StoredProcedure [dbo].[SP_mst_PatientToGreencardRegistration]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_mst_PatientToGreencardRegistration]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_mst_PatientToGreencardRegistration] AS' 
END
GO
-- =============================================
-- Author:		<felix/stephen>
-- Create date: <03-22-2017>
-- Description:	<Patient registration migration from bluecard to greencard>
-- =============================================
ALTER PROCEDURE [dbo].[SP_mst_PatientToGreencardRegistration]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ptn_pk int, @FirstName varbinary(max), @MiddleName varbinary(max), @LastName varbinary(max), @Sex int, @Status bit, @Status_Greencard bit , @DeleteFlag bit, 
		@CreateDate datetime, @UserID int,  @message varchar(80), @PersonId int, @PatientFacilityId varchar(50), @PatientType int, 
		@FacilityId varchar(10), @DateOfBirth datetime, @DobPrecision int, @NationalId varbinary(max), @IDNumber varchar(100), @PatientId int,  
		@ARTStartDate date,@transferIn int,@CCCNumber varchar(20), @entryPoint int, @ReferredFrom int, @RegistrationDate datetime,
		@MaritalStatusId int, @MaritalStatus int, @DistrictName varchar(50), @CountyID int, @SubCountyID int, @WardID int,
		@Address varbinary(max), @Phone varbinary(max), @EnrollmentId int, @PatientIdentifierId int, @ServiceEntryPointId int,
		@PatientMaritalStatusID int, @PatientTreatmentSupporterID int, @PersonContactID int, @LocationID int;
		
	DECLARE @ExitReason int, @ExitDate datetime, @DateOfDeath datetime, @UserID_CareEnded int, @CreateDate_CareEnded datetime;
  
	PRINT '-------- Patients Report --------';  
	exec pr_OpenDecryptedSession;

	DECLARE mstPatient_cursor CURSOR FOR   
	SELECT mst_Patient.Ptn_Pk, FirstName, MiddleName ,LastName,Sex, [Status], DeleteFlag, mst_Patient.CreateDate, mst_Patient.UserID, PatientFacilityId, PosId, DOB, DobPrecision, [ID/PassportNo], PatientEnrollmentID, [ReferredFrom], [RegistrationDate], MaritalStatus, DistrictName, Address, Phone, LocationID
	FROM mst_Patient INNER JOIN  dbo.Lnk_PatientProgramStart ON dbo.mst_Patient.Ptn_Pk = dbo.Lnk_PatientProgramStart.Ptn_pk
	WHERE MovedToPatientTable =0
	ORDER BY mst_Patient.Ptn_Pk;
  
	OPEN mstPatient_cursor;
  
	FETCH NEXT FROM mstPatient_cursor   
	INTO @ptn_pk, @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @IDNumber,@CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone, @LocationID
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		-- STEP 1: Start the transaction
		BEGIN TRANSACTION

		PRINT ' '  
		SELECT @message = '----- patients From mst_patient: ' + CAST(@ptn_pk as varchar(50))
  
		PRINT @message  

		--set null dates
		IF @CreateDate is null
			SET @CreateDate = getdate()
		--Due to the logic of green card
		IF @Status = 1
			SET @Status_Greencard = 0
		ELSE
			SET @Status_Greencard = 1

		IF @IDNumber IS NULL
			SET @IDNumber = 99999999;

		IF @Sex IS NOT NULL
			BEGIN
				IF ((select top 1  Name from mst_Decode where id = @Sex) = 'Male' OR (select top 1 Name from mst_Decode where id = @Sex) = 'Female')
					BEGIN
						SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like + (select top 1  Name from mst_Decode where id = @Sex) + '%');
					END
				ELSE
					SET @Sex = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
			END
		ELSE
			SET @Sex = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

		--Default all persons to new
		SET @ARTStartDate=( SELECT top 1  ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk And ARTTransferInDate Is Not Null);
		if(@ARTStartDate Is NULL) BEGIN SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='New');SET @transferIn=0; END ELSE BEGIN SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='Transfer-In');SET @transferIn=1; END
		-- SELECT @PatientType = 1285

		--encrypt nationalid
		SET @NationalId=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@IDNumber);
		
		IF @Status = 1
			BEGIN
				DECLARE @PatientExitReason varchar(50);
				
				SET @PatientExitReason = (SELECT TOP 1 Name FROM mst_Decode WHERE CodeID=23 AND ID = (SELECT TOP 1 PatientExitReason FROM dtl_PatientCareEnded WHERE Ptn_Pk = @ptn_pk AND CareEnded=1));
				IF @PatientExitReason = 'Lost to follow-up'
					BEGIN
						SET @PatientExitReason = 'LostToFollowUp';
					END
				ELSE IF @PatientExitReason = 'Transfer to another LPTF' OR @PatientExitReason = 'Transfer to ART'
					BEGIN
						SET @PatientExitReason = 'Transfer Out';
					END
				ELSE IF NOT EXISTS(select top 1 ItemId from LookupItemView where MasterName = 'CareEnded' AND ItemName like '%' + @PatientExitReason + '%')
					BEGIN
						SET @PatientExitReason = 'Transfer Out';
					END
				SET @ExitReason = (select top 1 ItemId from LookupItemView where MasterName = 'CareEnded' AND ItemName like '%' + @PatientExitReason + '%');
				SET @ExitDate = (SELECT top 1 CareEndedDate FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
				SET @DateOfDeath = (SELECT top 1 DeathDate FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
				SET @UserID_CareEnded = (SELECT top 1 UserID FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
				SET @CreateDate_CareEnded = (SELECT top 1 CreateDate FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
			END
			

		Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
		Values(@FirstName, @MiddleName, @LastName, @Sex, @Status_Greencard, @DeleteFlag, @CreateDate, @UserID);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into person';
				RETURN
			END

		SELECT @PersonId = SCOPE_IDENTITY();
		SELECT @message = 'Created Person Id: ' + CAST(@PersonId as varchar(50));
		PRINT @message;

		Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
		Values(@ptn_pk, @PersonId, @PatientFacilityId, @PatientType, @FacilityId, @Status_Greencard, @DateOfBirth, @DobPrecision, @NationalId, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into patient';
				RETURN
			END

		SELECT @PatientId = SCOPE_IDENTITY();
		SELECT @message = 'Created Patient Id: ' + CAST(@PatientId as varchar);
		PRINT @message;

		--Insert into Enrollment Table
		DECLARE Enrollment_cursor CURSOR FOR
		SELECT ModuleId, [StartDate], [UserID], [CreateDate] FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk;

		DECLARE @ModuleId int, @StartDate datetime, @UserID_Enrollment int, @CreateDate_Enrollment datetime;

		OPEN Enrollment_cursor
		FETCH NEXT FROM Enrollment_cursor INTO @ModuleId, @StartDate, @UserID_Enrollment, @CreateDate_Enrollment

		IF @@FETCH_STATUS <> 0   
			PRINT '         <<None>>'       

		WHILE @@FETCH_STATUS = 0  
		BEGIN
			
			IF @ModuleId = 203
			BEGIN 
				INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
				VALUES (@PatientId,1, @StartDate,0, @transferIn, @Status ,0 ,@UserID_Enrollment ,@CreateDate_Enrollment ,NULL);

				SELECT @EnrollmentId = SCOPE_IDENTITY();
				SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
				PRINT @message;
			END

			FETCH NEXT FROM Enrollment_cursor INTO  @ModuleId, @StartDate, @UserID_Enrollment, @CreateDate_Enrollment
			END  

		CLOSE Enrollment_cursor  
		DEALLOCATE Enrollment_cursor

			---- Insert to PatientEnrollment
			--INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
			--VALUES (@PatientId,1,(SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk and ModuleId=203),0, @transferIn, @Status ,0 ,@UserID ,@CreateDate ,NULL);

			--IF @@ERROR <> 0
			--	BEGIN
			--		-- Rollback the transaction
			--		ROLLBACK

			--		-- Raise an error and return
			--		--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
			--		PRINT 'Error Occurred inserting into patient enrollment';
			--		RETURN
			--	END
		
			--SELECT @EnrollmentId = SCOPE_IDENTITY();
			--SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			--PRINT @message;

			IF @CCCNumber IS NOT NULL AND @ModuleId = 203
				BEGIN
					-- Patient Identifier
					INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
					VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

					IF @@ERROR <> 0
						BEGIN
							-- Rollback the transaction
							ROLLBACK

							-- Raise an error and return
							--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
							PRINT 'Error Occurred inserting into patient identifier';
							RETURN
						END

					SELECT @PatientIdentifierId = SCOPE_IDENTITY();
					SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
					PRINT @message;
				END

		--Insert into ServiceEntryPoint
		IF @ReferredFrom > 0 bEGIN
			SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT top 1 Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
			IF @entryPoint IS NULL
				BEGIN
					SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
				END
		END
		ELSE
			SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

		INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
		VALUES(@PatientId, 1, @entryPoint, 0 , @UserID, @CreateDate, 0);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into service entry point';
				RETURN
			END

		SELECT @ServiceEntryPointId = SCOPE_IDENTITY();
		SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
		PRINT @message;
	
		--Insert into MaritalStatus
		IF @MaritalStatus > 0
			BEGIN
				IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
					SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select TOP 1 Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
				ELSE
					SET @MaritalStatusId = (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
			END
		ELSE
			SET @MaritalStatusId = (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

		INSERT INTO PatientMaritalStatus(PersonId, MaritalStatusId, Active, DeleteFlag, CreatedBy, CreateDate)
		VALUES(@PersonId, @MaritalStatusId, 1, 0, @UserID, @CreateDate);

		IF @@ERROR <> 0
			BEGIN
				-- Rollback the transaction
				ROLLBACK

				-- Raise an error and return
				--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
				PRINT 'Error Occurred inserting into patient marital status';
				RETURN
			END

		SELECT @PatientMaritalStatusID = SCOPE_IDENTITY();
		SELECT @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
		PRINT @message;

		--Insert into PersonLocation
		--SET @CountyID = (SELECT TOP 1 CountyId from County where CountyName like '%' + @DistrictName  + '%');
		--SET @WardID = (SELECT TOP 1 WardId FROM County WHERE WardName LIKE '%' +  +'%')

		--INSERT INTO PersonLocation(PersonId, County, SubCounty, Ward, Village, Location, SubLocation, LandMark, NearestHealthCentre, Active, DeleteFlag, CreatedBy, CreateDate)
		--VALUES(@Id, @CountyID, @SubCountyID, @WardID, @Village, @Location, @SubLocation, @LandMark, @NearestHealthCentre, 1, @DeleteFlag, @UserID, @CreateDate);
    
		--Insert into Treatment Supporter
		DECLARE Treatment_Supporter_cursor CURSOR FOR
		SELECT SUBSTRING(TreatmentSupporterName,0,charindex(' ',TreatmentSupporterName))as firstname ,
		SUBSTRING(TreatmentSupporterName,charindex(' ',TreatmentSupporterName) + 1,len(TreatmentSupporterName)+1)as lastname,
		TreatmentSupportTelNumber, CreateDate, UserID
		from dtl_PatientContacts WHERE ptn_pk = @ptn_pk;

		DECLARE @FirstNameT varchar(50), @LastNameT varchar(50), @TreatmentSupportTelNumber varchar(50), 
		@CreateDateT datetime, @UserIDT int, @IDT int;


		OPEN Treatment_Supporter_cursor
		FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT , @UserIDT

		IF @@FETCH_STATUS <> 0   
			PRINT '         <<None>>'       

		WHILE @@FETCH_STATUS = 0  
		BEGIN  

			--SELECT @message = '         ' + @product  
			--PRINT @message
		
			IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL 
				BEGIN
					Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
					Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, getdate(), @UserIDT);

					IF @@ERROR <> 0
						BEGIN
							-- Rollback the transaction
							ROLLBACK

							-- Raise an error and return
							--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
							PRINT 'Error Occurred inserting into person for treatment supporter';
							RETURN
						END

					SELECT @IDT = SCOPE_IDENTITY();
					SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
					PRINT @message;

					INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
					VALUES(@PersonId, @IDT, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber), 0, @UserIDT, getdate());

					IF @@ERROR <> 0
						BEGIN
							-- Rollback the transaction
							ROLLBACK

							-- Raise an error and return
							--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
							PRINT 'Error Occurred inserting into patient treatment supporter';
							RETURN
						END

					SELECT @PatientTreatmentSupporterID = SCOPE_IDENTITY();
					SELECT @message = 'Created PatientTreatmentSupporterID Id: ' + CAST(@PatientTreatmentSupporterID as varchar);
					PRINT @message;
				END

			FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT, @UserIDT
			END  

		CLOSE Treatment_Supporter_cursor  
		DEALLOCATE Treatment_Supporter_cursor

		--Insert into Person Contact
		IF @Address IS NOT NULL AND @Phone IS NOT NULL
			BEGIN
				INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
				VALUES(@PersonId, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

				IF @@ERROR <> 0
					BEGIN
						-- Rollback the transaction
						ROLLBACK

						-- Raise an error and return
						--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
						PRINT 'Error Occurred inserting into person contact';
						RETURN
					END

				SELECT @PersonContactID = SCOPE_IDENTITY();
				SELECT @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
				PRINT @message;
			END

		--Starting baseline
		print 'starting baseline';

		DECLARE @HBVInfected bit, @Pregnant bit, @TBinfected bit, @WHOStage int, @WHOStageString varchar(50), @BreastFeeding bit, 
				@CD4Count decimal , @MUAC decimal, @Weight decimal, @Height decimal, @artstart datetime,
				@ClosestARVDate datetime, @PatientMasterVisitId int, @HIVDiagnosisDate datetime, @EnrollmentDate datetime,
				@EnrollmentWHOStage int, @EnrollmentWHOStageString varchar(50), @VisitDate datetime, @Cohort varchar(50), @visit_id int;

		Select TOP 1 @artstart = ARTStartDate	From mst_Patient	Where Ptn_Pk = @ptn_pk	And LocationID = @LocationId;
		select TOP 1 @visit_id = visit_id from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId;
		
		print 'set @artstart and @visit_id';

		SET @Pregnant = 0;

		IF @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like 'Female%')
			BEGIN
				--SET @Pregnant = 0;
				IF EXISTS(select TOP 1 Name from mst_Decode where id=(select TOP 1 eligibleThrough from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId) and name like 'Pregnancy')
					BEGIN
						SET @Pregnant = 1;
					END
			END
			
		print 'set @Sex';

		If EXISTS(SELECT * FROM dtl_PatientVitals dtl WHERE dtl.Visit_pk = @visit_id ) Begin
			SET @Weight = (Select Top (1) dtl.[Weight]
			From ord_Visit As ord
			Inner Join
				dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
			Where (ord.Ptn_Pk = @ptn_pk)
			And (dtl.[Weight] Is Not Null)
			And (ord.Visit_Id = @visit_id));
		End 
		Else Begin
			SET @Weight = NULL;
		End
		
		print 'set @Weight';

		If exists (SELECT * FROM dtl_PatientVitals dtl WHERE dtl.Visit_pk = @visit_id) Begin
			SET @Height = (Select Top 1 dtl.Height
			From Ord_visit ord
			Inner Join
				dtl_PatientVitals dtl On dtl.visit_pk = ord.Visit_Id
			Where ord.ptn_pk = @ptn_pk
			And dtl.Height Is Not Null
			And (ord.Visit_Id = @visit_id));
		End 
		Else Begin
			SET @Height = NULL;
		End
		
		print 'set @Height';

		If EXISTS(SELECT * FROM dtl_PatientVitals dtl WHERE dtl.Visit_pk = @visit_id) Begin
			SET @MUAC = (Select Top (1) dtl.Muac
			From ord_Visit As ord
			Inner Join
				dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
			Where (ord.Ptn_Pk = @ptn_pk)
			And (dtl.Muac Is Not Null)
			And (ord.Visit_Id = @visit_id));
		End
		
		print 'set @MUAC';

		SET @TBinfected = 0;
		IF EXISTS(select TOP 1 Name from mst_Decode where id=(select TOP 1 eligibleThrough from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId) and name like 'TB/HIV')
			BEGIN
				SET @TBinfected = 1;
			END
			
		print 'set @TBinfected';

		SET @BreastFeeding = 0;
		IF EXISTS(select TOP 1 Name from mst_Decode where id=(select TOP 1 eligibleThrough from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId) and name like 'BreastFeeding')
			BEGIN
				SET @TBinfected = 1;
			END
			
		print 'set @BreastFeeding';

		SET @HIVDiagnosisDate = (select top 1 ConfirmHIVPosDate from dtl_PatientHivPrevCareEnrollment where ptn_pk = @ptn_pk);
		print 'set @HIVDiagnosisDate';
		SET @EnrollmentDate = (select TOP 1 DateEnrolledInCare from dtl_PatientHivPrevCareEnrollment where ptn_pk=@ptn_pk);
		print 'set @EnrollmentDate';
		SET @EnrollmentWHOStageString = (SELECT TOP 1 Name FROM mst_Decode WHERE ID = (SELECT TOP 1 WHOStage FROM dtl_PatientARVEligibility where WHOStage > 0 AND ptn_pk=@ptn_pk) and codeid=22 AND Name <> 'N/A');
		print 'set @EnrollmentWHOStage';
		SET @Cohort = (select  TOP 1 convert(char(3),[FirstLineRegStDate] , 0) + ' ' + CONVERT(varchar(10), year([FirstLineRegStDate])) from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);
		print 'set @Cohort';
		SET @CD4Count = (SELECT top 1 CD4 FROM dtl_PatientARVEligibility WHERE ptn_pk = @ptn_pk)
		print 'set @CD4Count';
		SET @WHOStageString = (SELECT TOP 1 WHOStage FROM dtl_PatientARVEligibility where ptn_pk = @ptn_pk);

		print 'set @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @Cohort, @CD4Count, @WHOStage';
		
		SET @EnrollmentWHOStage = CASE @EnrollmentWHOStageString  
			 WHEN '1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
			 WHEN '2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
			 WHEN '3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
			 WHEN '4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
			 WHEN 'T1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
			 WHEN 'T2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
			 WHEN 'T3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
			 WHEN 'T4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
			 ELSE (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown')
		  END
		  
		SET @WHOStage = CASE @WHOStageString  
			 WHEN '1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
			 WHEN '2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
			 WHEN '3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
			 WHEN '4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
			 WHEN 'T1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
			 WHEN 'T2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
			 WHEN 'T3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
			 WHEN 'T4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
			 ELSE (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown')
		  END
		  
		SET @VisitDate = (SELECT TOP 1 [VisitDate] FROM [dbo].[ord_Visit] where [Ptn_Pk] = @ptn_pk AND [VisitType] in(18, 19));
		IF @EnrollmentDate IS NULL BEGIN SET @EnrollmentDate = GETDATE(); END;

		INSERT INTO PatientMasterVisit(PatientId, ServiceId, Start, [End], Active, VisitDate, VisitScheduled, VisitBy, VisitType, [Status], CreateDate, DeleteFlag, CreatedBy)
		VALUES(@PatientId, 1, @EnrollmentDate, NULL, 0, @VisitDate, NULL, NULL, (SELECT top 1 ItemId FROM LookupItemView WHERE	MasterName like '%VisitType%' and ItemName like '%Enrollment%'), NULL, GETDATE(), 0 , @UserID);

		SET @PatientMasterVisitId = SCOPE_IDENTITY();
		
		SELECT @message = 'Created PatientMasterVisit Id: ' + CAST(@PatientMasterVisitId as varchar);
		PRINT @message;
			
		IF @Status = 1
			BEGIN
				INSERT INTO [dbo].[PatientCareending] ([PatientId] ,[PatientMasterVisitId] ,[PatientEnrollmentId] ,[ExitReason] ,[ExitDate] ,[TransferOutfacility] ,[DateOfDeath] ,[CareEndingNotes] ,[Active] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
				VALUES(@PatientId ,@PatientMasterVisitId ,@EnrollmentId ,@ExitReason , @ExitDate ,NULL ,@DateOfDeath ,NULL ,0 ,0,@UserID_CareEnded ,@CreateDate_CareEnded ,NULL);
			END
			
		SELECT @message = 'Created PatientCareending Id: ' + CAST(SCOPE_IDENTITY() as varchar);
		PRINT @message;

		IF (@Weight IS NOT NULL AND @Height IS NOT NULL AND @Weight > 0 AND @Height > 0)
		BEGIN
			INSERT INTO [dbo].[PatientBaselineAssessment]([PatientId], [PatientMasterVisitId], [HBVInfected], [Pregnant], [TBinfected], [WHOStage], [BreastFeeding], [CD4Count], [MUAC], [Weight], [Height], [DeleteFlag], [CreatedBy], [CreateDate] )
			VALUES(@PatientId, @PatientMasterVisitId, 0, @Pregnant, @TBinfected, @WHOStage, @BreastFeeding, @CD4Count, @MUAC, @Weight, @Height, 0 , @UserID, GETDATE());

			IF @@ERROR <> 0
				BEGIN
					-- Rollback the transaction
					ROLLBACK

					-- Raise an error and return
					--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
					PRINT 'Error Occurred inserting into patient baseline assessment';
					RETURN
				END
			SELECT @message = 'Created PatientBaselineAssessment Id: ' + CAST(SCOPE_IDENTITY() as varchar);
			PRINT @message;
		END

		IF EXISTS(SELECT * FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk)
			BEGIN
				DECLARE @TransferInDate datetime, @TreatmentStartDate datetime, @CurrentART varchar(50), @FacilityFrom varchar(150), @CreateDateTransfer datetime, @MFLCODE int;

				SET @TransferInDate = (SELECT TOP 1 ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
				SET @TreatmentStartDate = (SELECT TOP 1 FirstLineRegStDate FROM dtl_PatientARTCare WHERE ptn_pk = @ptn_pk);
				SET @CurrentART = (SELECT TOP 1 CurrentART FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
				SET @FacilityFrom = (SELECT TOP 1 ARTTransferInFrom FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
				SET @CreateDateTransfer = (SELECT TOP 1 CreateDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);

				SET @MFLCODE = (select TOP 1 PosId from mst_Patient WHERE Ptn_pk = @ptn_pk);

				IF @TransferInDate IS NOT NULL AND @TreatmentStartDate IS NOT NULL AND @CurrentART IS NOT NULL AND @FacilityFrom IS NOT NULL AND @MFLCODE IS NOT NULL
					BEGIN
						INSERT INTO [dbo].[PatientTransferIn]([PatientId], [PatientMasterVisitId], [ServiceAreaId], [TransferInDate], [TreatmentStartDate], [CurrentTreatment],  [FacilityFrom] , [MFLCode] ,[CountyFrom] , [TransferInNotes], [DeleteFlag] ,[CreatedBy] , [CreateDate])
						VALUES(@PatientId, @PatientMasterVisitId, 1, @TransferInDate, @TreatmentStartDate, @CurrentART, @FacilityFrom, @MFLCODE, (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), ' ', 0 , @UserID, @CreateDateTransfer);

						IF @@ERROR <> 0
						BEGIN
							-- Rollback the transaction
							ROLLBACK

							-- Raise an error and return
							--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
							PRINT 'Error Occurred inserting into patient transfer in';
							RETURN
						END
						SELECT @message = 'Created PatientTransferIn Id: ' + CAST(SCOPE_IDENTITY() as varchar);
						PRINT @message;
					END
		END

		IF EXISTS (Select	ptn_pk,	locationID,	Visit_pk [VisitId], a.PurposeId, b.Name [Purpose], a.Regimen [Regimen],	a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk)
			BEGIN
				DECLARE @TreatmentType varchar(50), @Purpose varchar(50), @Regimen varchar(50), @DateLastUsed datetime;
			
				SET @TreatmentType = (select TOP 1 [Name] from mst_Decode where codeID=33 AND ID = (Select top 1 a.PurposeId From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk));
				SET @Purpose = (select TOP 1 b.Name [Purpose] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
				SET @Regimen = (select TOP 1 a.Regimen [Regimen] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
				SET @DateLastUsed = (select TOP 1 a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);

				IF @TreatmentType IS NOT NULL AND @Purpose IS NOT NULL AND @Regimen IS NOT NULL AND @DateLastUsed IS NOT NULL
					BEGIN
					INSERT INTO [dbo].[PatientARVHistory]([PatientId], [PatientMasterVisitId], [TreatmentType], [Purpose] , [Regimen], [DateLastUsed], [DeleteFlag] , [CreatedBy] , [CreateDate])
					VALUES(@PatientId, @PatientMasterVisitId, @TreatmentType, @Purpose, @Regimen, @DateLastUsed, 0, @UserID, @CreateDate);
					END
				IF @@ERROR <> 0
					BEGIN
						-- Rollback the transaction
						ROLLBACK

						-- Raise an error and return
						--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
						PRINT 'Error Occurred inserting into patient arv history';
						RETURN
					END
					SELECT @message = 'Created PatientARVHistory Id: ' + CAST(SCOPE_IDENTITY() as varchar);
					PRINT @message;
			END

		IF EXISTS(select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk)
			BEGIN
				DECLARE @DateStartedOnFirstLine datetime;
				SET @DateStartedOnFirstLine = (select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);

				IF @DateStartedOnFirstLine IS NULL
					BEGIN
						SET @DateStartedOnFirstLine = GETDATE();
						SET @Cohort = (select  convert(char(3),GETDATE() , 0) + ' ' + CONVERT(varchar(10), year(GETDATE())));
					END
					
				INSERT INTO [dbo].[PatientTreatmentInitiation]([PatientMasterVisitId], [PatientId], [DateStartedOnFirstLine], [Cohort], Regimen, [RegimenCode] , [BaselineViralload] , [BaselineViralloadDate] , [DeleteFlag] , [CreatedBy] , [CreateDate] )
				VALUES(@PatientMasterVisitId, @PatientId, @DateStartedOnFirstLine, @Cohort, Null,(SELECT TOP 1 FirstLineReg FROM dtl_PatientARTCare where ptn_pk = @ptn_pk) , NULL, NULL, 0, @UserID, @CreateDate);

				IF @@ERROR <> 0
					BEGIN
						-- Rollback the transaction
						ROLLBACK

						-- Raise an error and return
						--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
						PRINT 'Error Occurred inserting into patient treatment initiation';
						RETURN
					END
					SELECT @message = 'Created PatientTreatmentInitiation Id: ' + CAST(SCOPE_IDENTITY() as varchar);
					PRINT @message;
			END

		IF @HIVDiagnosisDate IS NOT NULL AND @EnrollmentDate IS NOT NULL AND @EnrollmentWHOStage IS NOT NULL AND @artstart IS NOT NULL
			BEGIN
				INSERT INTO [dbo].[PatientHivDiagnosis]([PatientMasterVisitId] , [PatientId] , [HIVDiagnosisDate] , [EnrollmentDate] , [EnrollmentWHOStage] , [ARTInitiationDate] , [DeleteFlag] , [CreatedBy] , [CreateDate])
				VALUES(@PatientMasterVisitId, @PatientId, @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @artstart, 0 , @UserID, @CreateDate);

				IF @@ERROR <> 0
					BEGIN
						-- Rollback the transaction
						ROLLBACK

						-- Raise an error and return
						--RAISERROR ('Error in deleting employees in DeleteDepartment.', 16, 1)
						PRINT 'Error Occurred inserting into patient hiv diagnosis';
						RETURN
					END
					SELECT @message = 'Created PatientHivDiagnosis Id: ' + CAST(SCOPE_IDENTITY() as varchar);
					PRINT @message;
			END
		--ending baseline
		Update mst_Patient Set MovedToPatientTable =1 Where Ptn_Pk=@ptn_pk;
		INSERT INTO [dbo].[GreenCardBlueCard_Transactional] ([PersonId] ,[Ptn_Pk]) VALUES (@PersonId , @ptn_pk);
		COMMIT;
		
		SELECT @message = 'Completed Inserting Patient: ' + CAST(@ptn_pk as varchar);
		PRINT @message;
		
		-- Get the next mst_patient.
		FETCH NEXT FROM mstPatient_cursor   
		INTO @ptn_pk, @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @IDNumber, @CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone, @LocationID
	END   
	CLOSE mstPatient_cursor;  
	DEALLOCATE mstPatient_cursor;
END


GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterAdverseEvents] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterAdverseEvents]
	-- Add the parameters for the stored procedure here
	@masterVisitID int = null,
	@PatientID int = null,
	@adverseEvent varchar(250) = null,
	@medicineCausingAE varchar(250) = null,
	@adverseSeverity varchar(250) = null,
	@adverseAction varchar(250) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
if exists(select 1 from AdverseEvent where PatientMasterVisitId = @masterVisitID and PatientId = @PatientID and EventName = @adverseEvent)
	BEGIN
		update AdverseEvent set EventCause = @medicineCausingAE, Severity = @adverseSeverity,[Action] = @adverseAction, DeleteFlag = 0
		where PatientMasterVisitId = @masterVisitID and PatientId = @PatientID and EventName = @adverseEvent
	END
	ELSE
	BEGIN
		insert into AdverseEvent(PatientId,PatientMasterVisitId,EventName,EventCause,Severity,[Action],DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@adverseEvent,@medicineCausingAE,@adverseSeverity,@adverseAction,0,@userID,GETDATE())
	END
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterAllergies]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterAllergies]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterAllergies] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 19th Mar 2017
-- Description:	save patient encounter - Allergies
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterAllergies]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@allergy varchar(50) = null,
	@allergyReaction varchar(50) = null,
	@allergySeverity varchar(50) = null,
	@allergyOnsetDate varchar(50) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	if exists(select 1 from patientallergy where Allergen = @allergy and PatientId = @PatientID)
	BEGIN
		update patientallergy set Allergen = @allergy, Reaction = @allergyReaction, severity = @allergySeverity,
		OnsetDate = @allergyOnsetDate, DeleteFlag = 0
		where Allergen = @allergy and PatientId = @PatientID
	END
	ELSE
	BEGIN
		insert into patientallergy(PatientId,PatientMasterVisitId,Allergen,Reaction,Severity,OnsetDate,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@allergy,@allergyReaction,@allergySeverity,@allergyOnsetDate, 0,@userID,GETDATE())
	END

End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterChronicIllness]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterChronicIllness] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterChronicIllness]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@chronicIllness varchar(50) = null,
	@treatment varchar(250) = null,
	@dose varchar(50) = null,
	@onsetDate varchar(20) = null,
	@active int = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	if exists(select 1 from PatientChronicIllness where ChronicIllness = @chronicIllness and PatientId = @PatientID)
	BEGIN
		update PatientChronicIllness set Treatment = @treatment,Dose = @dose,OnsetDate = @onsetDate, active = @active, DeleteFlag = 0
		where ChronicIllness = @chronicIllness and PatientId = @PatientID
	END
	ELSE
	BEGIN
		insert into PatientChronicIllness(PatientId,PatientMasterVisitId,ChronicIllness,Treatment,Dose,OnsetDate,active,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@chronicIllness,@treatment,@dose,@onsetDate,@active,0,@userID,GETDATE())
	END
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterComplaints]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterComplaints] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 26th Apr 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterComplaints]
	-- Add the parameters for the stored procedure here
	@masterVisitID int = null,
	@PatientID int = null,
	@presentingComplaintID int = null,
	@onsetDate varchar(50) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
if exists(select 1 from presentingComplaints where PatientMasterVisitId = @masterVisitID and PatientId = @PatientID and PresentingComplaintsId = @presentingComplaintID)
	BEGIN
		update presentingComplaints set onsetDate = @onsetDate, DeleteFlag = 0
		where PatientMasterVisitId = @masterVisitID and PatientId = @PatientID and PresentingComplaintsId = @presentingComplaintID
	END
	ELSE
	BEGIN
		insert into presentingComplaints(PatientId,PatientMasterVisitId,PresentingComplaintsId,onsetDate,DeleteFlag,CreatedBy,CreatedDate) 
		values(@PatientID,@MasterVisitID,@presentingComplaintID,@onsetDate,0,@userID,GETDATE())
	END
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterDiagnosis]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterDiagnosis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterDiagnosis] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterDiagnosis]
	-- Add the parameters for the stored procedure here
	@masterVisitID int = null,
	@PatientID int = null,
	@diagnosis varchar(250) = null,
	@treatment varchar(250) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	if exists(select 1 from PatientDiagnosis where Diagnosis = @diagnosis and PatientId = @PatientID and PatientMasterVisitID = @masterVisitID)
	BEGIN
		update PatientDiagnosis set ManagementPlan = @treatment, DeleteFlag = 0
		where Diagnosis = @diagnosis and PatientId = @PatientID and PatientMasterVisitID = @masterVisitID
	END
	ELSE
	BEGIN
		insert into PatientDiagnosis(PatientId,PatientMasterVisitId,Diagnosis,ManagementPlan,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@diagnosis,@treatment,0,@userID,GETDATE())
	END
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterGeneralExam]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterGeneralExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterGeneralExam] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 27th Apr 2017
-- Description:	save patient encounter - General Examination
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterGeneralExam]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@Exam varchar(50) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here

	declare @generalExamMasterID int = (select top 1 Id from lookupmaster where Name = 'GeneralExamination')
	declare @ExamID int = (select LookupItemId from LookupMasterItem where LookupMasterId = @generalExamMasterID and DisplayName = @Exam)

	if exists(select 1 from PhysicalExamination where PatientMasterVisitId = @MasterVisitID
	and PatientId = @PatientID and ExaminationTypeId = @generalExamMasterID and ExamId = @ExamID)
	BEGIN
		update PhysicalExamination set DeleteFlag = 0
		where PatientMasterVisitId = @MasterVisitID and PatientId = @PatientID and ExaminationTypeId = @generalExamMasterID
		and ExamId = @ExamID
	END
	ELSE
	BEGIN
		insert into PhysicalExamination(PatientId,PatientMasterVisitId,ExaminationTypeId,ExamId,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@generalExamMasterID,@ExamID,0,@userID,GETDATE())
	END
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPatientManagement]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPatientManagement]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterPatientManagement] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Patient Management
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterPatientManagement]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null,
	@ARVAdherence int = null,
	@CTXAdherence int = null,
	@WorkPlan varchar(1000) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	declare @ARVAdherenceType int = (Select Id from LookupMaster where name = 'ARVAdherence')
	declare @CTXAdherenceType int = (Select Id from LookupMaster where name = 'CTXAdherence')

	if(@PatientMasterVisitID > 0)
	BEGIN
		if(@ARVAdherence > 0)
		BEGIN
			if exists(select 1 from AdherenceOutcome where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @ARVAdherenceType)
			BEGIN
				update AdherenceOutcome set Score = @ARVAdherence
				where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @ARVAdherenceType
			END
			ELSE
			BEGIN
				insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
				values(@PatientID,@PatientMasterVisitID,@ARVAdherenceType,@ARVAdherence,0,@userID,GETDATE())
			END
		END

		if(@CTXAdherence > 0)
		BEGIN
			if exists(select 1 from AdherenceOutcome where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @CTXAdherenceType)
			BEGIN
				update AdherenceOutcome set Score = @CTXAdherence
				where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @CTXAdherenceType
			END
			ELSE
			BEGIN
				insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
				values(@PatientID,@PatientMasterVisitID,@CTXAdherenceType,@CTXAdherence,0,@userID,GETDATE())
			END
		END

		if exists(select 1 from PatientClinicalNotes where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID)
		BEGIN
			update PatientClinicalNotes set ClinicalNotes = @WorkPlan
			where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
		END
		ELSE
		BEGIN
			insert into PatientClinicalNotes(PatientId,PatientMasterVisitId,ServiceAreaId,ClinicalNotes,DeleteFlag,CreatedBy,CreateDate) 
			values(@PatientID,@PatientMasterVisitID,203,@WorkPlan,0,@userID,GETDATE())
		END
	END
	ELSE
	BEGIN
		if(@ARVAdherence > 0)
		BEGIN
			insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
			values(@PatientID,@PatientMasterVisitID,@ARVAdherenceType,@ARVAdherence,0,@userID,GETDATE())
		END

		if(@CTXAdherence > 0)
		BEGIN
			insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
			values(@PatientID,@PatientMasterVisitID,@CTXAdherenceType,@CTXAdherence,0,@userID,GETDATE())
		END

		insert into PatientClinicalNotes(PatientId,PatientMasterVisitId,ServiceAreaId,ClinicalNotes,DeleteFlag,CreatedBy,CreateDate) 
		values(@PatientID,@PatientMasterVisitID,203,@WorkPlan,0,@userID,GETDATE())
	END
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPHDP]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPHDP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterPHDP] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterPHDP]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@phdp varchar(50) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	declare @phdpMasterID int = (select Id from LookupMaster where Name = 'PHDP')
	declare @phdpID int = (select LookupItemId from LookupMasterItem where LookupMasterId = @phdpMasterID and DisplayName = @phdp)

	if exists(select 1 from PatientPHDP where PatientMasterVisitId = @MasterVisitID and PatientId = @PatientID and Phdp = @phdpID)
	BEGIN
		UPDATE PatientPHDP SET DeleteFlag = 0
		where PatientMasterVisitId = @MasterVisitID and PatientId = @PatientID and Phdp = @phdpID
	END
	ELSE
	BEGIN
		insert into PatientPHDP(PatientId,PatientMasterVisitId,phdp,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@phdpID,0,@userID,GETDATE())
	END
	
	
End







GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPhysicalExam]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPhysicalExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterPhysicalExam] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 7th Feb 2017
-- Description:	save patient encounter - Physical Examination
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterPhysicalExam]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@examType int = null,
	@exam varchar(250) = null,
	@findings varchar(50) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	if exists(select 1 from PhysicalExamination where PatientMasterVisitId = @MasterVisitID
	and PatientId = @PatientID and ExaminationTypeId = @examType and ExamId = @exam)
	BEGIN
		update PhysicalExamination set Finding = @findings, DeleteFlag = 0
		where PatientMasterVisitId = @MasterVisitID and PatientId = @PatientID and ExaminationTypeId = @examType and ExamId = @exam
	END
	ELSE
	BEGIN
		insert into PhysicalExamination(PatientId,PatientMasterVisitId,ExaminationTypeId,ExamId,Finding,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@examType,@exam,@findings,0,@userID,GETDATE())
	END
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterPresentingComplaints]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterPresentingComplaints]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterPresentingComplaints] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 20th Jan 2017
-- Description:	save patient encounter - Presenting Complaints
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterPresentingComplaints]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null,
	@serviceID int = null,
	@VisitDate datetime = null,
	@VisitScheduled int =null,
	@VisitBy int = null,
	@anyPresentingComplaints int = null,
	@ComplaintsNotes nvarchar(1000) = null,
	@TBScreening int = null,
	@NutritionalStatus int = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	declare @TBScreeningTypeID int = (Select Id from LookupMaster where name = 'TBStatus')
	declare @NutritionScreeningTypeID int = (Select Id from LookupMaster where name = 'NutritionStatus')

	IF(@PatientMasterVisitID > 0)
	BEGIN
		update PatientMasterVisit set visitDate = @VisitDate, visitScheduled = @VisitScheduled, visitBy = @VisitBy
		where id = @PatientMasterVisitID and PatientId = @PatientID

		if exists(select 1 from ComplaintsHistory where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID)
		BEGIN
			update ComplaintsHistory set PresentingComplaint = @ComplaintsNotes, AnyComplaint = @anyPresentingComplaints 
			where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
		END
		ELSE
		BEGIN
			insert into ComplaintsHistory (PatientId,PatientMasterVisitId,AnyComplaint,PresentingComplaint,DeleteFlag,CreateBy,CreateDate) 
			values(@PatientID,@PatientMasterVisitID,@anyPresentingComplaints,@ComplaintsNotes,0,@userID,GETDATE())
		END

		if(@TBScreening > 0)
		begin
			IF EXISTS(SELECT 1 FROM PatientScreening WHERE PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID 
			and ScreeningTypeId = @TBScreeningTypeID)
			BEGIN
				update PatientScreening set ScreeningValueId = @TBScreening
				where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and ScreeningTypeId = @TBScreeningTypeID
			END
			ELSE
			BEGIN
				insert into PatientScreening (PatientId,PatientMasterVisitId,ScreeningTypeId,ScreeningDone,ScreeningValueId,DeleteFlag,CreatedBy,CreateDate)
				values(@PatientID,@PatientMasterVisitID,@TBScreeningTypeID,1,@TBScreening,0,@userID,GETDATE())
			END
		end

		if(@NutritionalStatus > 0)
		begin
			IF EXISTS(SELECT 1 FROM PatientScreening WHERE PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID 
			and ScreeningTypeId = @NutritionScreeningTypeID)
			BEGIN
				update PatientScreening set ScreeningValueId = @NutritionalStatus
				where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and ScreeningTypeId = @NutritionScreeningTypeID
			END
			ELSE
			BEGIN
				insert into PatientScreening (PatientId,PatientMasterVisitId,ScreeningTypeId,ScreeningDone,ScreeningValueId,DeleteFlag,CreatedBy,CreateDate)
				values(@PatientID,@PatientMasterVisitID,@NutritionScreeningTypeID,1,@NutritionalStatus,0,@userID,GETDATE())
			END
		end
		
		select @PatientMasterVisitID as PatientMasterVisitID
	END
	ELSE
	BEGIN
		insert into PatientMasterVisit (patientId,serviceId,start,active,createdBy,createDate,visitDate,visitScheduled,visitBy)
		values(@PatientID,@serviceID,CONVERT (time, GETDATE()),1,@userID,GETDATE(),@VisitDate,@VisitScheduled,@VisitBy)
		SELECT @PatientMasterVisitID = SCOPE_IDENTITY()

		insert into ComplaintsHistory (PatientId,PatientMasterVisitId,AnyComplaint,PresentingComplaint,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@PatientMasterVisitID,@anyPresentingComplaints,@ComplaintsNotes,0,@userID,GETDATE())

		if(@TBScreening > 0)
		begin
			insert into PatientScreening (PatientId,PatientMasterVisitId,ScreeningTypeId,ScreeningDone,ScreeningValueId,DeleteFlag,CreatedBy,CreateDate)
			values(@PatientID,@PatientMasterVisitID,@TBScreeningTypeID,1,@TBScreening,0,@userID,GETDATE())
		end
		if(@NutritionalStatus > 0)
		begin
			insert into PatientScreening (PatientId,PatientMasterVisitId,ScreeningTypeId,ScreeningDone,ScreeningValueId,DeleteFlag,CreatedBy,CreateDate)
			values(@PatientID,@PatientMasterVisitID,@NutritionScreeningTypeID,1,@NutritionalStatus,0,@userID,GETDATE())
		end
		

		select @PatientMasterVisitID as PatientMasterVisitID

	END
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterTS]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterTS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterTS] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 20th Jan 2017
-- Description:	save patient encounter - Presenting Complaints
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterTS]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null,
	@serviceID int = null,
	@VisitDate datetime = null,
	@VisitScheduled int =null,
	@VisitBy int = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	IF(@PatientMasterVisitID > 0)
	BEGIN
		update PatientMasterVisit set visitDate = @VisitDate, visitScheduled = @VisitScheduled, visitBy = @VisitBy
		where id = @PatientMasterVisitID and PatientId = @PatientID
		
		select @PatientMasterVisitID as PatientMasterVisitID
	END
	ELSE
	BEGIN
		insert into PatientMasterVisit (patientId,serviceId,start,active,createdBy,createDate,visitDate,visitScheduled,visitBy)
		values(@PatientID,@serviceID,CONVERT (time, GETDATE()),1,@userID,GETDATE(),@VisitDate,@VisitScheduled,@VisitBy)
		SELECT @PatientMasterVisitID = SCOPE_IDENTITY()
		

		select @PatientMasterVisitID as PatientMasterVisitID

	END
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterVaccines]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterVaccines]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientEncounterVaccines] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Chronic Illness
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterVaccines]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@vaccine varchar(50) = null,
	@vaccineStage varchar(50) = null,
	@vaccineDate varchar(50) = null,
	@dose int = null,
	@duration int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	if exists(select 1 from Vaccination where Vaccine = @vaccine and VaccineStage = @vaccineStage and PatientId = @PatientID)
	BEGIN
		update Vaccination set VaccineDate = @vaccineDate, DeleteFlag = 0
		where Vaccine = @vaccine and VaccineStage = @vaccineStage and PatientId = @PatientID
	END
	ELSE
	BEGIN
		insert into Vaccination(PatientId,PatientMasterVisitId,Vaccine,VaccineStage,VaccineDate,DeleteFlag,CreatedBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@vaccine,@vaccineStage,@vaccineDate, 0,1,GETDATE())
	END
	
End








GO
/****** Object:  StoredProcedure [dbo].[sp_savePatientFamilyPlanningMethod]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientFamilyPlanningMethod]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_savePatientFamilyPlanningMethod] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 11th Mar 2017
-- Description:	save patient family planning method
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientFamilyPlanningMethod]
	-- Add the parameters for the stored procedure here
	@PatientID int = null,
	@FPId int = null,
	@fpMethod int = null
	

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	IF EXISTS(SELECT 1 FROM PatientFamilyPlanningMethod where patientid = @PatientID and PatientFPId = @FPId and FPMethodId = @fpMethod)
	BEGIN
		update PatientFamilyPlanningMethod set DeleteFlag = 0
		where patientid = @PatientID and PatientFPId = @FPId and FPMethodId = @fpMethod
	END
	ELSE
	BEGIN
		insert into PatientFamilyPlanningMethod (PatientId,PatientFPId, FPMethodId,DeleteFlag)
		values(@PatientID,@FPId,@fpMethod,0)
	END
END







GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUpdatePharmacy_GreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveUpdatePharmacy_GreenCard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveUpdatePharmacy_GreenCard] AS' 
END
GO
ALTER PROCEDURE [dbo].[sp_SaveUpdatePharmacy_GreenCard]                                                
( 
 @PatientMasterVisitID int = 0,                                               
 @PatientId int = null,                                                
 @LocationID int = null,                                                
 @OrderedBy int = null,                                                                                                                                          
 @UserID int = null,                                                 
 @RegimenType varchar(50) = null,                                                
 @DispensedBy int=null,                                                
 @DispensedByDate datetime=null,    
 @RegimenLine int = null,                
 @PharmacyNotes varchar(200) = null,
 @ModuleID int = null,

 @TreatmentProgram int = null,
 @PeriodTaken int = null,

 @TreatmentPlan int = null, 
 @TreatmentPlanReason int = null,
 @Regimen int = null                 
)                                                
                                                
As       
Begin               
	Declare @ptn_pharmacy int,@RegimenMap_Pk int,@ARTStartDate datetime,@Ptn_Pharmacy_Pk int, @ptn_pk int, @visitPK int

	Select @RegimenType = Nullif(Ltrim(Rtrim(@RegimenType)), '')

	set @ptn_pk = (select ptn_pk from patient where id = @PatientId)

	IF EXISTS(select 1 from ord_PatientPharmacyOrder where PatientMasterVisitId = @PatientMasterVisitID) 
	BEGIN
		set @Ptn_Pharmacy_Pk = (select ptn_pharmacy_pk from ord_PatientPharmacyOrder where patientmasterVisitID = @PatientMasterVisitID)
		Update [ord_PatientPharmacyOrder] Set
			[OrderedBy] = @OrderedBy, [DispensedBy] = @DispensedBy,
			[ProgID] = @TreatmentProgram, [UpdateDate] = Getdate(),
			[ProviderID] = 1, [DispensedByDate] = @DispensedByDate,
			UserID = @UserID,	Regimenline = @Regimenline,
			PharmacyNotes = @PharmacyNotes, pharmacyperiodtaken = @PeriodTaken
		Where patientmasterVisitID = @PatientMasterVisitID

		Update ARVTreatmentTracker set regimenid = @Regimen, regimenLineId = @RegimenLine, TreatmentStatusId = @TreatmentPlan,
		TreatmentStatusReasonId = @TreatmentPlanReason

		If(@RegimenType Is Not Null) 
		Begin
		
			Select @RegimenMap_Pk = RegimenMap_Pk
			From dtl_regimenmap a, ord_patientpharmacyorder b
			Where a.ptn_pk = b.ptn_pk
			And b.ptn_pharmacy_pk = a.orderID
			And b.Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk;

			Update [dtl_RegimenMap] Set
				[RegimenType] = @RegimenType,
				[UpdateDate] = Getdate()
			Where ([RegimenMap_Pk] = @RegimenMap_Pk)
		End
		
		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@ptn_pk)
		
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @ptn_pk;

		Select @ptn_pharmacy_pk;
	END
	ELSE
	BEGIN
		insert into ord_Visit (ptn_pk,locationid,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate,CreatedBy)
		values(@ptn_pk, @locationID,GETDATE(),4,1,0,@UserID,GETDATE(),@UserID)

		set @visitPK = SCOPE_IDENTITY();

		Insert Into dbo.ord_PatientPharmacyOrder (
			Ptn_pk, PatientID, patientmasterVisitID, LocationID, OrderedBy, OrderedByDate, DispensedBy, DispensedByDate, ProgID,
			UserID, CreateDate, ProviderID, Regimenline, PharmacyNotes, VisitID, pharmacyPeriodTaken)
		Values (
			@ptn_pk,@PatientId, @PatientMasterVisitID, @LocationID, @OrderedBy, getdate(), @DispensedBy, @DispensedByDate, @TreatmentProgram, 
			@UserID, Getdate(), 1, @RegimenLine, @PharmacyNotes, @visitPK, @PeriodTaken);

		Set @ptn_pharmacy =SCOPE_IDENTITY();

		Insert into ARVTreatmentTracker (PatientId,ServiceAreaId,PatientMasterVisitId,RegimenId,RegimenLineId,
		TreatmentStatusId,TreatmentStatusReasonId, DeleteFlag, CreateBy, createdate)
		values(@patientid,@moduleid,@PatientMasterVisitID,@Regimen,@RegimenLine,@TreatmentPlan,@TreatmentPlanReason,
		0,@UserID,GETDATE())

		Update ord_PatientPharmacyOrder Set
			ReportingID = (Select Right('000000' + Convert(varchar, @ptn_pharmacy), 6))
		Where ptn_pharmacy_pk = @ptn_pharmacy;

		--If (@DispensedByDate Is Not Null And @DispensedBy > 0) Begin
		--	Update ord_PatientPharmacyOrder Set
		--		OrderStatus = 2
		--	Where ptn_pharmacy_pk = @ptn_pharmacy;
		--End

		
		If(@RegimenType Is Not Null) 
		Begin	
			Insert Into dtl_RegimenMap (Ptn_Pk,	LocationID,	Visit_Pk, RegimenType, OrderId,	UserID,	CreateDate)
			Values (@ptn_pk, @LocationID, @visitPK, @RegimenType, @ptn_pharmacy, @UserID, Getdate());
		End

		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@ptn_pk)
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @ptn_pk;

		Select @ptn_pharmacy;

	END
End






GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard] AS' 
END
GO
ALTER PROCEDURE [dbo].[sp_SaveUpdatePharmacyPrescription_GreenCard]                                    
	@ptn_pharmacy_pk int = null,
	@DrugId int = null,
	@BatchId varchar(50) = null,
	@Dose decimal(18,2)=0,                                 
	@FreqId int = null,
	@Duration decimal(18,2) = null,
	@qtyPres decimal(18,2) = null,
	@qtyDisp decimal(18,2) = null,
	@UserID int = null,                
	@Prophylaxis int = null,
	@pmscm varchar(50) = null                               
                                    
AS                                        
Begin
	declare @TotalOrderedQuantity int,@TotalDispensedQuantity int
	Select @TotalOrderedQuantity = 0, @TotalDispensedQuantity = 0
			
	if exists(select 1 from dtl_PatientPharmacyOrder where ptn_pharmacy_pk = @ptn_pharmacy_pk And Drug_Pk = @DrugId
	 and BatchNo = @BatchId)
	BEGIN
		Update dtl_PatientPharmacyOrder Set
			SingleDose = @Dose,
			FrequencyID = @FreqId,
			Duration = @Duration,
			OrderedQuantity = @qtyPres,
			BatchNo = @BatchId,
			UserID = @UserID,
			DispensedQuantity = @qtyDisp,
			UpdateDate = Getdate(),
			Prophylaxis = @Prophylaxis
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk And Drug_Pk = @DrugId and BatchNo = @BatchId

		if(@pmscm = 1 and @qtyDisp > 0)
		BEGIN
			--IF NOT EXISTS(select 1 from dtl_patientPharmacyDispensed where ptn_pharmacy_pk = @ptn_pharmacy_pk and Drug_Pk = @DrugId
			--and batchId = @BatchId)
			--BEGIN
			--	INSERT into dtl_patientPharmacyDispensed
			--	(ptn_pharmacy_pk,drug_pk,batchId,frequencyID,dose,duration,dispensedQuantity,dispensedDate,deleteFlag)
			--	values(@ptn_pharmacy_pk,@DrugId,@BatchId,@FreqId,@Dose,@Duration,@qtyDisp,GETDATE(),0)
			--END
			
			update ord_PatientPharmacyOrder set DispensedByDate = GETDATE(), DispensedBy = @UserID
		END
	
	END
	ELSE
	BEGIN
		Insert Into dtl_PatientPharmacyOrder (
			ptn_pharmacy_pk,Drug_Pk,SingleDose,FrequencyID,Duration,OrderedQuantity,BatchNo, DispensedQuantity, UserID,CreateDate,Prophylaxis,
			genericid,StrengthID)
		Values (
			@ptn_pharmacy_pk,@DrugId,@Dose,@FreqId,@Duration,@qtyPres,@BatchId,@qtyDisp,@UserID,Getdate(),@Prophylaxis,0,0);

		if(@pmscm = 1 and @qtyDisp > 0)
		BEGIN
			--INSERT into dtl_patientPharmacyDispensed
			--(ptn_pharmacy_pk,drug_pk,batchId,frequencyID,dose,duration,dispensedQuantity,dispensedDate,deleteFlag)
			--values(@ptn_pharmacy_pk,@DrugId,@BatchId,@FreqId,@Dose,@Duration,@qtyDisp,GETDATE(),0)

			declare @ptn_pk int = (select Ptn_pk from ord_patientpharmacyorder where ptn_pharmacy_pk=@ptn_pharmacy_pk)
			declare @storeID int = (select top 1 StoreId from Dtl_StockTransaction where BatchId = @BatchId)

			insert into Dtl_StockTransaction
			(ItemId,BatchId,ptn_pharmacy_pk, PtnPk,storeid,transactiondate,quantity,UserId,createdate)
			values(@DrugId,@BatchId,@ptn_pharmacy_pk,@ptn_pk,@storeID,getdate(),-@qtyDisp,@UserID,GETDATE())

			update ord_PatientPharmacyOrder set DispensedByDate = GETDATE(), DispensedBy = @UserID
		END
		
	END

	Select	@TotalOrderedQuantity = Sum(a.OrderedQuantity),
			@TotalDispensedQuantity = Sum(a.dispensedQuantity)
	From dtl_PatientPharmacyOrder a
	Where a.ptn_pharmacy_pk = @ptn_pharmacy_pk

	Update ord_PatientPharmacyOrder Set
		OrderStatus = Case 
						When DispensedByDate Is Null Then 1 --new order
						When DispensedByDate Is Not Null  And @TotalDispensedQuantity = @TotalOrderedQuantity Then 2 --complete
						When DispensedByDate Is Not Null  And @TotalDispensedQuantity < @TotalOrderedQuantity Then 3 --partial
						Else orderstatus End
	Where ptn_pharmacy_pk = @ptn_pharmacy_pk

                                     
End





GO
