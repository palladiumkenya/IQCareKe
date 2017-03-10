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
            public string duration { get; set; }
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
            public string complaints { get; set; }
            public string tbScreening { get; set; }
            public string nutritionStatus { get; set; }
            public string lmp { get; set; }
            public string pregStatus { get; set; }
            public string edd { get; set; }
            public string ancProfile { get; set; }
            public string onFP { get; set; }
            public string fpMethod { get; set; }
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
        }
    }
}
