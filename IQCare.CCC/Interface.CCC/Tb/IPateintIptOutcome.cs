using System.Collections.Generic;
using Entities.CCC.Tb;

namespace Interface.CCC.Tb
{
    public interface IPatientIptOutcome
    {
        int AddPatientIptOutcome(PatientIptOutcome p);

        PatientIptOutcome GetPatientIptOutcome(int id);

        void DeletePatientIptOutcome(int id);

        int UpdatePatientIptOutcome(PatientIptOutcome p);

        List<PatientIptOutcome> GetByPatientId(int patientId);
    }
}