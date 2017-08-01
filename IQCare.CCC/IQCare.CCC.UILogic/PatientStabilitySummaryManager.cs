using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PatientStabilitySummaryManager
    {
        private IPatientStabilitySummaryManager mgr = (IPatientStabilitySummaryManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientStabilitySummaryManager, BusinessProcess.CCC");

        public List<PatientStabilitySummary> GetAllStabilitySummaries()
        {
            try
            {
                return mgr.GetAllStabilitySummaries();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
