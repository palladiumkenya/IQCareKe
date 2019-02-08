using Application.Presentation;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;
using IQCare.Web.UILogic;

namespace IQCare.CCC.UILogic.Tb
{
    public class PatientIptOutcomeManager
    {
        private IPatientIptOutcome _patientIptOutcome = (IPatientIptOutcome)ObjectFactory.CreateInstance("BusinessProcess.CCC.Tb.BPatientIptOutcome, BusinessProcess.CCC");

        public int AddPatientIptOutcome(PatientIptOutcome p)
        {
            PatientIptOutcome patientIptOutcome = new PatientIptOutcome()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                IptEvent = p.IptEvent,
                ReasonForDiscontinuation = p.ReasonForDiscontinuation,
                CreatedBy = SessionManager.UserId
            };
            return _patientIptOutcome.AddPatientIptOutcome(patientIptOutcome);
        }

        public PatientIptOutcome GetPatientIptOutcome(int id)
        {
            var patientIptOutcome = _patientIptOutcome.GetPatientIptOutcome(id);
            return patientIptOutcome;
        }

        public void DeletePatientIptOutcome(int id)
        {
            _patientIptOutcome.DeletePatientIptOutcome(id);
        }

        public int UpdatePatientIptOutcome(PatientIptOutcome p)
        {
            PatientIptOutcome patientIptOutcome = new PatientIptOutcome()
            {
                Id = p.Id,
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                IptEvent = p.IptEvent,
                ReasonForDiscontinuation = p.ReasonForDiscontinuation,
                CreatedBy = SessionManager.UserId,
                IPTOutComeDate=p.IPTOutComeDate
            };
            return _patientIptOutcome.UpdatePatientIptOutcome(patientIptOutcome);
        }

        public List<PatientIptOutcome> GetByPatientId(int patientId)
        {
            var patientIptOutcome = _patientIptOutcome.GetByPatientId(patientId);
            return patientIptOutcome;
        }
    }
}