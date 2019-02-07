using System;
using System.Collections.Generic;
using Application.Presentation;
using Interface.CCC.ClinicalSummary;

namespace IQCare.CCC.UILogic
{
    public class PharmacyHistoryDrugSubstitutionsManager
    {
        IPharmacyHistoryDrugSubstitutionsSwitches _mgr = (IPharmacyHistoryDrugSubstitutionsSwitches)ObjectFactory.CreateInstance("BusinessProcess.CCC.ClinicalSummary.BPharmacyHistoryDrugSubstitutionsSwitches, BusinessProcess.CCC");

        public List<PharmacyHistory> GetPharmacyDrugsSubstitutionsSwitchesData(int ptn_pk)
        {
            try
            {
                return _mgr.GetPharmacyDrugsSubstitutionsSwitchesData(ptn_pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}