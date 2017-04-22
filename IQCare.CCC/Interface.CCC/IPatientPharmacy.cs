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
            List<DrugPrescription> drugPrescription, string pmscmFlag, string TreatmentProgram,
            string PeriodTaken, string TreatmentPlan, string TreatmentPlanReason, string Regimen);
        DataTable getPharmacyDrugList(string PMSCM);
        List<DrugFrequency> getPharmacyDrugFrequency();
        List<DrugBatch> getPharmacyDrugBatch(string DrugPk);
        DataTable getPharmacyDrugSubstitutionInterruptionReason(string TreatmentPlan);
        DataTable getPharmacyPrescriptionDetails(string patientMasterVisitID);
        DataTable getPharmacyPendingPrescriptions(string patientMasterVisitID, string PatientID);
        DataTable getPharmacyRegimens(string regimenLine);
        List<PharmacyFields> getPharmacyFields(string patientMasterVisitID);
        List<PharmacyFields> getPharmacyCurrentRegimen(string patientId);
    }
}
