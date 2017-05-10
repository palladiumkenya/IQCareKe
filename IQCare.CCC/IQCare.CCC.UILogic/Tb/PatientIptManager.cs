using Application.Presentation;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Tb
{
    public class PatientIptManager
    {
        private IPatientIpt _patientIpt = (IPatientIpt)ObjectFactory.CreateInstance("BusinessProcess.CCC.Tb.BPatientIpt, BusinessProcess.CCC");

        public int AddPatientIpt(PatientIpt p)
        {
            PatientIpt patientIpt = new PatientIpt()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Weight = p.Weight,
                AdheranceMeasurement = p.AdheranceMeasurement,
                Hepatotoxicity = p.Hepatotoxicity,
                IptDateCollected = p.IptDateCollected,
                IptDueDate = p.IptDueDate,
                Peripheralneoropathy = p.Peripheralneoropathy,
                Rash = p.Rash,
                HepatotoxicityAction = p.HepatotoxicityAction,
                PeripheralneoropathyAction = p.PeripheralneoropathyAction,
                RashAction = p.RashAction,
                AdheranceMeasurementAction = p.AdheranceMeasurementAction,
            };
            return _patientIpt.AddPatientIpt(patientIpt);
        }

        public PatientIpt GetPatientIpt(int id)
        {
            var patientIpt = _patientIpt.GetPatientIpt(id);
            return patientIpt;
        }

        public void DeletePatientIpt(int id)
        {
            _patientIpt.DeletePatientIpt(id);
        }

        public int UpdatePatientIpt(PatientIpt p)
        {
            PatientIpt patientIpt = new PatientIpt()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Weight = p.Weight,
                AdheranceMeasurement = p.AdheranceMeasurement,
                Hepatotoxicity = p.Hepatotoxicity,
                IptDateCollected = p.IptDateCollected,
                IptDueDate = p.IptDueDate,
                Peripheralneoropathy = p.Peripheralneoropathy,
                Rash = p.Rash,
                HepatotoxicityAction = p.HepatotoxicityAction,
                PeripheralneoropathyAction = p.PeripheralneoropathyAction,
                RashAction = p.RashAction,
                AdheranceMeasurementAction = p.AdheranceMeasurementAction,
            };
            return _patientIpt.UpdatePatientIpt(patientIpt);
        }

        public List<PatientIpt> GetByPatientId(int patientId)
        {
            var patientIpt = _patientIpt.GetByPatientId(patientId);
            return patientIpt;
        }
    }
}