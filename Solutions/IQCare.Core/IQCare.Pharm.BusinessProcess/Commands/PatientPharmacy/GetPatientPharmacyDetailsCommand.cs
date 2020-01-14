using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy;

namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
   public  class GetPatientPharmacyDetailsCommand :IRequest<Result<GetPatientPharmacyResponse>>
    {

        public int PatientId { get; set; }

        public int PatientMasterVisitId { get; set; }

        
    }

public class GetPatientPharmacyResponse
{
    public int? PatientId { get; set; }

    public int? PatientMasterVisitId { get; set; }
    
    public int ptn_pk { get; set; }
     public DateTime? VisitDate { get; set; }
        public DateTime? OrderedByDate { get; set; }

        public DateTime? DispensedDate { get; set; }

        public List<DrugDetails> DrugDetails { get; set; }
}


    public class DrugDetails
    {
        public int ptn_pharmacy_pk { get; set; }
        public string DrugName { get; set; }
        public string DrugId { get; set; }
        public string DrugAbb { get; set; }
        public string batchId { get; set; }
        public string batchText { get; set; }
        public string Dose { get; set; }
        public string Freq { get; set; }
        public string FreqText { get; set; }
        public string Duration { get; set; }
        public string QuantityPres { get; set; }
        public string QUantityDisp { get; set; }
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

        
    }



}
