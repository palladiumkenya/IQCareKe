using Entities.Records.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
    public interface IPatientConsent
    {
        int AddPatientConsents(PatientConsent p);

        PatientConsent GetPatientConsent(int id);

        void DeletePatientConsent(int id);

        int UpdatePatientConsent(PatientConsent p);

        List<PatientConsent> GetByPatientId(int patientId);
    }
}
