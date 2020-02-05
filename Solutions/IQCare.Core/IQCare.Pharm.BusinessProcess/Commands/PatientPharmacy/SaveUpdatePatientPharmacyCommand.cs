using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;

namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
    public class SaveUpdatePatientPharmacyCommand :IRequest<Result<SaveUpdatePharmacyResponse>>
    {
        public int PatientMasterVisitId { get; set; }

        public int Ptn_Pk { get; set; }
        public int PatientId { get; set; }

        public int LocationId { get; set; }

        public int UserId { get; set; }

        public string PrescriptionDate { get; set; }

        public int PrescribedBy { get; set; }

        public int DispensedBy { get; set; }

        public string DispensedDate { get; set; }

        public DateTime VisitDate { get; set; }

        public int pmscm { get; set; }
        public List<PrescriptionDetails> PrescriptionDetails { get; set; }
        
    }

    public class PrescriptionDetails
    {
      public   string DrugName { get; set; }
        public string DrugId { get; set; }
        public string DrugAbb { get; set; }
        public string batchId { get; set; }
        public string batchText { get; set; }
        public string Dose { get; set; }
        public string Freq { get; set; }
        public string FreqText { get; set; }
        public string Duration { get; set; }
        public string QuantityPres { get; set; }
        public  string QUantityDisp { get; set; }
        public string Reason { get; set; }
        public string ReasonText { get; set; }
        public string Regimen { get; set; }
        public string Regimentext { get; set; }
        public string Regimenline { get; set; }
        public string Regimenlinetext { get; set; }
        public string TreatmentPlan { get; set; }
        public string TreatmentPlantext { get; set; }
        public string TreatmentProgram { get; set; }
        public string TreatmentProgramText { get; set; }
        public string Morning { get; set; }
        public string Midday { get; set; }
        public string Evening { get; set; }
        public string Night { get; set; }

        public string Period { get; set; }

        public string PeriodTakentext { get; set; }
        public string Prophylaxis { get; set; }

        public bool Disabled { get; set; }
    }

    public class SaveUpdatePharmacyResponse
    {
        public List<String> Ptn_Pharmacy_Pk { get; set; }
    }
}
