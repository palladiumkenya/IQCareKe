using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Entities.CCC.Encounter.PatientEncounter;

namespace Interface.CCC
{
    public interface IPatientPharmacy
    {
        int saveUpdatePharmacy(string PatientMasterVisitID, string PatientId, string LocationID, string OrderedBy,
            string UserID, string RegimenType, string DispensedBy, string RegimenLine, string ModuleID,
            List<DrugPrescription> drugPrescription);
        DataTable getPharmacyDrugList(string regimenLine);
        List<DrugFrequency> getPharmacyDrugFrequency();
        List<DrugBatch> getPharmacyDrugBatch(string DrugPk);
        DataTable getPharmacyDrugSubstitutionInterruptionReason(string TreatmentPlan);
    }
}
