using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Entities.CCC.Encounter.PatientEncounter;

namespace Interface.CCC
{
    public interface IPatientEncounter
    {
        int savePresentingComplaints(string PatientMasterVisitID, string PatientID, string ServiceID, string VisitDate, string VisitScheduled, string VisitBy, string anyComplaints, string Complaints, int TBScreening, int NutritionalStatus, int userId, List<AdverseEvents> adverseEvents, List<PresentingComplaints> presentingComplaints);
        int savePresentingComplaintsTS(string PatientMasterVisitID, string PatientID, string ServiceID, string VisitDate, string VisitScheduled, string VisitBy, int userId);
        int saveChronicIllness(string masterVisitID, string patientID, string userID, List<ChronicIlness> chronicIllness, List<Vaccines> Vaccines, List<Allergies> allergies);
        int savePhysicalEaxminations(string masterVisitID, string patientID, string userID, List<PhysicalExamination> physicalExam, List<string> generalExam);
        int savePatientManagement(string PatientMasterVisitID, string PatientID, string userID, string workplan, string ARVAdherence, string CTXAdherence, List<string> phdp, List<Diagnosis> diagnosis);
        int saveNeonatalMilestone(int PatientMasterVisitID, int PatientID, int userID, string mlAssessed, string mlOnsetDate, string mlAchieved, string mlStstus, string mlComment);
        PresentingComplaintsEntity getPatientEncounter(string PatientMasterVisitID, string PatientID);
        DataTable getPatientEncounterHistory(string PatientID);
        DataTable getPatientEncounterAdverseEvents(string PatientMasterVisitID, string PatientID);
        DataTable getPatientEncounterChronicIllness(string PatientMasterVisitID, string PatientID);
        DataTable getPatientEncounterVaccines(string PatientMasterVisitID, string PatientID);
        DataTable getPatientEncounterPhysicalExam(string PatientMasterVisitID, string PatientID);
        DataTable getPatientEncounterDiagnosis(string PatientMasterVisitID, string PatientID);
        DataTable getPatientEncounterAllergies(string PatientMasterVisitID, string PatientID);
        ZScoresParameters GetZScoreValues(string PatientID, string gender, string height);
        DataTable getPatientEncounterComplaints(string PatientMasterVisitID, string PatientID);
        DataTable getPatientWorkPlan(string PatientID);
        DataSet getPatientDSDParameters(string PatientID);
        DataTable isVisitScheduled(string PatientID);
        DataTable patientCategorizationAtEnrollment(string PatientID);
        DataTable getPatientPreviousTriage(string PatientMasterVisitID, string PatientID);
        DataTable getPatientMilestones(string PatientID);
        DataTable getImmunizationHistory(string PatientMasterVisitID, string PatientID);
        DataTable getTannersStaging(string PatientMasterVisitID, string PatientID);
        //DataTable saveImmunizationHistory(int PatientMasterVisitID, int PatientID, int UserId, int ImmunizationPeriod, int ImmunizationGiven, int ImmunizationDate);
    }
}
