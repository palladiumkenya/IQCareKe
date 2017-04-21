using Entities.CCC.Tb;
using System.Collections.Generic;

namespace Interface.CCC.Tb
{
    public interface IPatientIptWorkup
    {
        int AddPatientIptWorkup(PatientIptWorkup p);

        PatientIptWorkup GetPatientIptWorkup(int id);

        void DeletePatientIptWorkup(int id);

        int UpdatePatientIptWorkup(PatientIptWorkup p);

        List<PatientIptWorkup> GetByPatientId(int patientId);
    }
}