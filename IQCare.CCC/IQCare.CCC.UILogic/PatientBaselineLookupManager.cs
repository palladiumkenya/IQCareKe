using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PatientBaselineLookupManager
    {
        private readonly IPatientBaselineLookupManager  _patientBaselineLookup = (IPatientBaselineLookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientBaselineLookupManager, BusinessProcess.CCC");

        public List<PatientBaselineLookup> GetPatientBaseline(int patientId)
        {
            var patientBaseline = _patientBaselineLookup.GetAllPatientBaseline(patientId);
            return patientBaseline;
        }

    }
}
