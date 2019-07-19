using System;
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
        void UpdateBlueCardBaselineTransferInHistory(int? ptn_pk, DateTime? confirmHIVPosDate, DateTime? dateEnrolledInCare, int whostage);
        void UpdateBlueCardBaselineTransferInTreatment(int? ptn_pk, DateTime? transferInDate, DateTime? treatmentStartDate, int currentTreatment, string facilityFrom, int countyFrom);
    }
}
