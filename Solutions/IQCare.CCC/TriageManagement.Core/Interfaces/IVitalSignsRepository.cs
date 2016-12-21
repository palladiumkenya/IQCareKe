using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Common.Core.Interfaces;
using TriageManagement.Core.Model;

namespace TriageManagement.Core.Interfaces
{
    public interface IVitalSignsRepository :IRepository<PatientVitals>
    {
        PatientVitals GetCurrentPatientVitals(int patientId);
        PatientVitals GetPatientVitalsHistoryByDateRange(DateTime vitalsFromDate, DateTime vitalsToDate);
    }
}
