using Application.Presentation;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Tb
{
    public class PatientIcfManager
    {
        private IPatientIcf _patientIcf = (IPatientIcf)ObjectFactory.CreateInstance("BusinessProcess.CCC.Tb.BPatientIcf, BusinessProcess.CCC");

        public int AddPatientIcf(PatientIcf p)
        {
            PatientIcf patientIcf = new PatientIcf()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Cough = p.Cough,
                Fever = p.Fever,
                NightSweats = p.NightSweats,
                WeightLoss = p.WeightLoss,
                OnAntiTbDrugs = p.OnAntiTbDrugs,
                OnIpt = p.OnIpt,
                EverBeenOnIpt = p.EverBeenOnIpt
            };
            return _patientIcf.AddPatientIcf(patientIcf);
        }

        public PatientIcf GetPatientIcf(int id)
        {
            var patientIcf = _patientIcf.GetPatientIcf(id);
            return patientIcf;
        }

        public void DeletePatientIcf(int id)
        {
            _patientIcf.DeletePatientIcf(id);
        }

        public int UpdatePatientIcf(PatientIcf p)
        {
            PatientIcf patientIcf = new PatientIcf()
            {
                Id = p.Id,
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Cough = p.Cough,
                Fever = p.Fever,
                NightSweats = p.NightSweats,
                WeightLoss = p.WeightLoss,
                OnIpt = p.OnIpt,
                OnAntiTbDrugs = p.OnAntiTbDrugs,
                EverBeenOnIpt = p.EverBeenOnIpt
            };
            return _patientIcf.UpdatePatientIcf(patientIcf);
        }

        public List<PatientIcf> GetByPatientId(int patientId)
        {
            var patientIcf = _patientIcf.GetByPatientId(patientId);
            return patientIcf;
        }
    }
}