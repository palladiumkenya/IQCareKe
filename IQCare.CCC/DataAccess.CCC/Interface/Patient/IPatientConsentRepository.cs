using DataAccess.Context;
using Entities.CCC.Consent;
using System.Collections.Generic;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientConsentRepository : IRepository<PatientConsent>
    {
        List<PatientConsent> GetByPatientId(int patientId);
    }
}