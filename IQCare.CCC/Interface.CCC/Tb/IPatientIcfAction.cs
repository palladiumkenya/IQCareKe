using Entities.CCC.Tb;
using System.Collections.Generic;

namespace Interface.CCC.Tb
{
    public interface IPatientIcfAction
    {
        int AddPatientIcfAction(PatientIcfAction p);

        PatientIcfAction GetPatientIcfAction(int id);

        void DeletePatientIcfAction(int id);

        int UpdatePatientIcfAction(PatientIcfAction p);

        List<PatientIcfAction> GetByPatientId(int patientId);
    }
}