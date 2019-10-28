using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
   public  class PharmacyDrugVisitDetails
    {
        public Int64 RowID { get; set; }
        public int? PatientId { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public int? VisitID { get; set; }

        public DateTime? VisitDate { get; set; }

        public int? Ptn_pk { get; set; }

        public int ptn_pharmacy_pk { get; set; }

        public int? OrderedBy { get; set; }

        public string OrderedByName { get; set; }

        public DateTime? OrderedByDate { get; set; }

        public DateTime? DispensedByDate { get; set; }

        public int? DispensedbyName { get; set; }

        public int? ProgID { get; set; }

        public string TreatmentProgram { get; set; }

        public int? PeriodTaken { get; set; }

        public string OrderStatusText { get; set; }

        public int OrderStatus { get; set; }

        public string PeriodTakenText { get; set; }

        public string Regimen { get; set; }

        public int? RegimenId { get; set; }

        public string RegimenLine { get; set; }

        public int? RegimenLineId { get; set; }

        public int? TreatmentPlan { get; set; }
        public string TreatmentPlanText { get; set; }
        public string TreatmentPlanReason { get; set; }

        public int? TreatmentPlanReasonId { get; set; }

        public int? Drug_pk { get; set; }

        public string DrugName { get; set; }

        public string StrengthName { get; set; }

        public int?  StrengthID { get; set; }

        public string FrequencyName { get; set; }

        public int? Multiplier { get; set; }

        public int? FrequencyID { get; set; }

        public decimal? SingleDose { get; set; }

        public decimal? Duration { get; set;}

        public decimal? DispensedQuantity { get; set; }

        public decimal? OrderedQuantity { get; set; }

        public int? UserID { get; set; }

        public string UserName { get; set; }

        public int? Prophylaxis { get; set; }

        public string BatchName { get; set; }

        public int? BatchNo { get; set; }

        public decimal? MorningDose { get; set; }

        public decimal? MiddayDose { get; set; }

        public decimal? EveningDose { get; set; }

        public decimal? NightDose { get; set; }

        public string Abbreviation { get; set; }
    }

}
