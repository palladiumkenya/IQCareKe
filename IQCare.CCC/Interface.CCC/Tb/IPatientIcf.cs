using Entities.CCC.Tb;
using System.Collections.Generic;

namespace Interface.CCC.Tb
{
    public interface IPatientIcf
    {
        int AddPatientIcf(PatientIcf p);

        PatientIcf GetPatientIcf(int id);

        void DeletePatientIcf(int id);

        int UpdatePatientIcf(PatientIcf p);

        List<PatientIcf> GetByPatientId(int patientId);
    }
}