using Entities.CCC.Triage;
using System.Collections.Generic;

namespace Interface.CCC.Triage
{
    public interface IpatientPregnancyIndicatorManager
    {
        int AddPregnancyIndicator(PatientPregnancyIndicator a);
        int UpdatePreganacyIndcator(PatientPregnancyIndicator u);
        int DeletePregnancyIndicator(int Id);
        List<PatientPregnancyIndicator> GetPregnancyIndicator(int patientId);
        int CheckIfPregnancyIndicatorExisists(int patientId);

        int GetLastPregnancyStatus(int patientId);
    }
}
