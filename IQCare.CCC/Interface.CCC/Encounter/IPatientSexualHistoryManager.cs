using Entities.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.Encounter
{
    public interface IPatientSexualHistoryManager
    {
        PatientSexualHistory AddPatientSexualHistory(PatientSexualHistory a);
        PatientSexualHistory UpdatePatientSexualHistory(PatientSexualHistory u);

        List<PatientSexualHistory> GetPatientSexualHistoryList(int patientId, int patientMasterVisitId);
        PatientSexualHistory GetPatientSexualHistory(int patientId, int patientMasterVisitId, int Id);
        
    }
}
