using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.pharmacy
{
    [Serializable]
    [Table("dtl_PatientPharmacyOrder")]
    public  class PatientPharmacyDispense
    {
        [Key]
        public int Id { get; set; }
        public int ptn_pharmacy_pk { get; set; }
        public int Drug_Pk { get; set; }
        public int GenericID { get; set; }
       public int StrengthID { get; set; }
        public int FrequencyID { get; set; }
        public decimal SingleDose { get; set; }
        public decimal Duration { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal ? DispensedQuantity { get; set; }
        public int ? Financed { get; set; }
        public int UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int ? RegimenId { get; set; }
        public int Prophylaxis { get; set; }
        public int ? BatchNo { get; set; }
        public string TreatmentPhase { get; set; }
        public int ? Month { get; set; }
        public DateTime ? ExpiryDate { get; set; }
        public int ? TabId { get; set; }
        public int ? PrintPrescriptionStatus { get; set; }
        public string PatientInstructions { get; set; }
        public string WhyPartial { get; set; }
        public int ? ScheduleId { get; set; }
        public int ? pillCount { get; set; }

    }
}
