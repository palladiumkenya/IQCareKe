using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    public class PatientEncounter
    {
        public string complaints { get; set; }

        [Serializable]
        public class AdverseEvents
        {
            public string adverseSeverityID { get; set; }
            public string adverseEvent { get; set; }
            public string medicineCausingAE { get; set; }
            public string adverseSeverity { get; set; }
            public string adverseAction { get; set; }

        }
        [Serializable]
        public class Vaccines
        {
            public string vaccineID { get; set; }
            public string vaccineStageID { get; set; }
            public string vaccine { get; set; }
            public string vaccineStage { get; set; }
            public string vaccineDate { get; set; }
        }
        [Serializable]
        public class ChronicIlness
        {
            public string chronicIllnessID { get; set; }
            public string chronicIllness { get; set; }
            public string treatment { get; set; }
            public string dose { get; set; }
            public string OnsetDate { get; set; }
            public string Active { get; set; }
        }

        [Serializable]
        public class Allergies
        {
            public string allergyID { get; set; }
            public string reactionID { get; set; }
            public string severityID { get; set; }
            public string allergy { get; set; }
            public string reaction { get; set; }
            public string severity { get; set; }
            public string onsetDate { get; set; }
        }

        [Serializable]
        public class PhysicalExamination
        {
            public string examTypeID { get; set; }
            public string examID { get; set; }
            public string examType { get; set; }
            public string exam { get; set; }
            public string findings { get; set; }
        }

        [Serializable]
        public class Diagnosis
        {
            public string diagnosis { get; set; }
            public string treatment { get; set; }
        }

        [Serializable]
        public class PresentingComplaintsEntity
        {
            public string visitDate { get; set; }
            public string visitScheduled { get; set; }
            public string visitBy { get; set; }
            public string anyComplaint { get; set; }
            public string complaints { get; set; }
            public string tbScreening { get; set; }
            public string nutritionStatus { get; set; }
            public string lmp { get; set; }
            public string pregStatus { get; set; }
            public string edd { get; set; }
            public string ancProfile { get; set; }
            public string onFP { get; set; }
            public string[] fpMethod { get; set; }
            public string reasonNotOnFP { get; set; }
            public string CaCX { get; set; }
            public string STIScreening { get; set; }
            public string STIPartnerNotification { get; set; }
            public string ARVAdherence { get; set; }
            public string CTXAdherence { get; set; }
            public string nextAppointmentDate { get; set; }
            public string nextAppointmentType { get; set; }
            public string[] phdp { get; set; }
        }

        [Serializable]
        public class DrugFrequency
        {
            public string id { get; set; }
            public string frequency { get; set; }
            public string multiplier { get; set; }
        }

        [Serializable]
        public class DrugBatch
        {
            public string id { get; set; }
            public string batch { get; set; }
        }

        [Serializable]
        public class DrugPrescription
        {
            public string DrugId { get; set; }
            public string BatchId { get; set; }
            public string FreqId { get; set; }
            public string DrugAbbr { get; set; }
            public string Dose { get; set; }
            public string Duration { get; set; }
            public string qtyPres { get; set; }
            public string qtyDisp { get; set; }
            public string prophylaxis { get; set; }
        }

        [Serializable]
        public class PharmacyFields
        {
            public string TreatmentProgram { get; set; }
            public string PeriodTaken { get; set; }
            public string TreatmentPlan { get; set; }
            public string TreatmentPlanReason { get; set; }
            public string RegimenLine { get; set; }
            public string Regimen { get; set; }
        }

        [Serializable]
        public class ZScoresParameters
        {
            public double L_WA { get; set; }
            public double M_WA { get; set; }
            public double S_WA { get; set; }
            public double L_WH { get; set; }
            public double M_WH { get; set; }
            public double S_WH { get; set; }
            public double L_BMIz { get; set; }
            public double M_BMIz { get; set; }
            public double S_BMIz { get; set; }
        }

        [Serializable]
        public class ZScores
        {
            public double weightForAge { get; set; }
            public double weightForHeight { get; set; }
            public double BMIz { get; set; }
        }

        [Serializable]
        public class KeyValue
        {
            public string ItemId { get; set; }
            public string DisplayName { get; set; }
        }

        [Serializable]
        public class PresentingComplaints
        {
            public string presentingComplaintID { get; set; }
            public string presentingComplaint { get; set; }
            public string onsetDate { get; set; }
        }
    }
}
