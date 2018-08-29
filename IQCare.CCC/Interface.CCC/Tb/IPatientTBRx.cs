using System.Collections.Generic;
using Entities.CCC.Tb;

namespace Interface.CCC.Tb
{
    public interface IPatientTBRx
    {
        int AddPatientTBRx(PatientTBRx p);
        List<PatientTBRx> GetByPatientId(int patientId);
        int UpdatePatientTBRx(PatientTBRx p);
    }
}
