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
        int savePhysicalEaxminations(string masterVisitID, string patientID, List<PhysicalExamination> physicalExam);
        int savePatientManagement(string PatientMasterVisitID, string PatientID, string ARVAdherence, string CTXAdherence, string nextAppointment, string appointmentType, List<string> phdp, List<Diagnosis> diagnosis);
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



    }
}
