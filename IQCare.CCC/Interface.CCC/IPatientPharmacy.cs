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
        DataTable getPharmacyDrugList(string regimenLine);
        List<DrugFrequency> getPharmacyDrugFrequency();
        List<DrugBatch> getPharmacyDrugBatch(string DrugPk);
    }
}
