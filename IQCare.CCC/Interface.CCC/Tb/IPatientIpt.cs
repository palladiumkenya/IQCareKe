using Entities.CCC.Tb;
using System.Collections.Generic;

namespace Interface.CCC.Tb
{
    public interface IPatientIpt
    {
        int AddPatientIpt(PatientIpt p);

        PatientIpt GetPatientIpt(int id);

        void DeletePatientIpt(int id);

        int UpdatePatientIpt(PatientIpt p);

        List<PatientIpt> GetByPatientId(int patientId);
    }
}