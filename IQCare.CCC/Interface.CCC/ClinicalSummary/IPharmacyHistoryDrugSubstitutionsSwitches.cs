using System;
using System.Collections.Generic;

namespace Interface.CCC.ClinicalSummary
{
    public interface IPharmacyHistoryDrugSubstitutionsSwitches
    {
        List<PharmacyHistory> GetPharmacyDrugsSubstitutionsSwitchesData(int ptn_pk);
    }

    [Serializable]
    public class PharmacyHistory
    {
        public string regimentype { get; set; }
        public DateTime DispensedByDate { get; set; }
    }
}