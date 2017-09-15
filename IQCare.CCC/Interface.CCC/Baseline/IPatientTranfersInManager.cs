using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientTranfersInManager
    {
        int AddPatientTranferIn(PatientTransferIn patientTransferIn);
        int UpdatePatientTransferIn(PatientTransferIn patientTransferIn);
        int DeletePatientTransferIn(int id);
        List<PatientTransferIn> GetPatientTransferIns(int patientId);
        int CheckifPatientTransferExisits(int patientId);
    }
}
