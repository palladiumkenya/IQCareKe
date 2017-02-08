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
        int savePresentingComplaints(string PatientMasterVisitID, string PatientID, string ServiceID, string VisitDate, string VisitScheduled, string VisitBy, string Complaints, int TBScreening, int NutritionalStatus, string lmp, string PregStatus, string edd, string ANC, int OnFP, int fpMethod, string CaCx, string STIScreening, string STIPartnerNotification, List<AdverseEvents> adverseEvents);
        int saveChronicIllness(string masterVisitID, string patientID, List<ChronicIlness> chronicIllness, List<Vaccines> Vaccines);
       
    }
}
