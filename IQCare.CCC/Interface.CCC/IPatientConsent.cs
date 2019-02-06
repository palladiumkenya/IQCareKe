using Entities.CCC.Consent;
using System.Collections.Generic;

namespace Interface.CCC
{
    public interface IPatientConsent
    {
        int AddPatientConsents(PatientConsent p);

        PatientConsent GetPatientConsent(int id);

        void DeletePatientConsent(int id);

        int UpdatePatientConsent(PatientConsent p);

        List<PatientConsent> GetByPatientId(int patientId);

        List<PatientConsent> GetPatientConsentByType(int patientId, int consentType);
    }
}