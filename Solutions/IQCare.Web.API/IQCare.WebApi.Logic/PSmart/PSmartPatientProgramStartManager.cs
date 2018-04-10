using Application.Presentation;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.PSmart
{
    public class PSmartPatientProgramStartManager
    {
        private readonly IPatientProgramStartManager _patientProgramStartManager = (IPatientProgramStartManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BPatientProgramStartManager, BusinessProcess.WebApi");

        public int AddPatientProgrameStart(PatientProgramStart patientProgramStart)
        {
            return _patientProgramStartManager.AddPatientProgramStart(patientProgramStart);
        }
    }
}