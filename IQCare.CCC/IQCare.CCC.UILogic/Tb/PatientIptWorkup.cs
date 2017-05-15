using Application.Presentation;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Tb
{
    public class PatientIptWorkupManager
    {
        private IPatientIptWorkup _patientIptWorkup = (IPatientIptWorkup)ObjectFactory.CreateInstance("BusinessProcess.CCC.Tb.BPatientIptWorkup, BusinessProcess.CCC");

        public int AddPatientIptWorkup(PatientIptWorkup p)
        {
            PatientIptWorkup patientIptWorkup = new PatientIptWorkup()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                AbdominalTenderness = p.AbdominalTenderness,
                LiverFunctionTests = p.LiverFunctionTests,
                Numbness = p.Numbness,
                YellowColouredUrine = p.YellowColouredUrine,
                YellownessOfEyes = p.YellownessOfEyes
            };
            return _patientIptWorkup.AddPatientIptWorkup(patientIptWorkup);
        }

        public PatientIptWorkup GetPatientIptWorkup(int id)
        {
            var patientIptWorkup = _patientIptWorkup.GetPatientIptWorkup(id);
            return patientIptWorkup;
        }

        public void DeletePatientIptWorkup(int id)
        {
            _patientIptWorkup.DeletePatientIptWorkup(id);
        }

        public int UpdatePatientIptWorkup(PatientIptWorkup p)
        {
            PatientIptWorkup patientIptWorkup = new PatientIptWorkup()
            {
                Id = p.Id,
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                AbdominalTenderness = p.AbdominalTenderness,
                LiverFunctionTests = p.LiverFunctionTests,
                Numbness = p.Numbness,
                YellowColouredUrine = p.YellowColouredUrine,
                YellownessOfEyes = p.YellownessOfEyes
            };
            return _patientIptWorkup.UpdatePatientIptWorkup(patientIptWorkup);
        }

        public List<PatientIptWorkup> GetByPatientId(int patientId)
        {
            var patientIptWorkup = _patientIptWorkup.GetByPatientId(patientId);
            return patientIptWorkup;
        }
    }
}