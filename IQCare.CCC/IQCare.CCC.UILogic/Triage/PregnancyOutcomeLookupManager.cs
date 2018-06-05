using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC.Triage;

namespace IQCare.CCC.UILogic.Triage
{
    public class PregnancyOutcomeLookupManager
    {
        private IPregnancyOutcomeLookupManager _pregnancyOutcomeLookupManager = (IPregnancyOutcomeLookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPregnancyOutcomeLookup, BusinessProcess.CCC");

        public PregnancyOutcomeLookup GetLastPregnancyOutcomeLookup(int patientId)
        {
            try
            {
                return _pregnancyOutcomeLookupManager.GetLastPregnancyOutcomeLookup(patientId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
