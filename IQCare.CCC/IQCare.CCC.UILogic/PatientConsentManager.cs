using Entities.CCC.Consent;
using System;
using System.Collections.Generic;
using Application.Presentation;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PatientConsentManager
    {
        private IPatientConsent _consent = (IPatientConsent)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientConsent, BusinessProcess.CCC");
        public int AddPatientConsents(PatientConsent p)
        {
            PatientConsent consent = new PatientConsent()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                ServiceAreaId = p.ServiceAreaId,
                ConsentType = p.ConsentType,
                ConsentDate = p.ConsentDate,
                DeclineReason = p.DeclineReason
            };
            return _consent.AddPatientConsents(consent);
        }

        public PatientConsent GetPatientConsent(int id)
        {
            var consent = _consent.GetPatientConsent(id);
            return consent;
        }

        public void DeletePatientConsent(int id)
        {
            _consent.DeletePatientConsent(id);
        }

        public int UpdatePatientConsent(PatientConsent p)
        {
            PatientConsent consent = new PatientConsent()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                ServiceAreaId = p.ServiceAreaId,
                ConsentType = p.ConsentType,
                ConsentDate = p.ConsentDate,
                DeclineReason = p.DeclineReason
            };
            return _consent.UpdatePatientConsent(consent);
        }

        public List<PatientConsent> GetByPatientId(int patientId)
        {
            var consent = _consent.GetByPatientId(patientId);
            return consent;
        }
    }
}