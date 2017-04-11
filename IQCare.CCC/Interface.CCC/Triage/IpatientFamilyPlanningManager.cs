using Entities.CCC.Triage;
using System.Collections.Generic;

namespace Interface.CCC.Triage
{
    public interface IpatientFamilyPlanningManager
    {
        int AddFamilyPlanningStatus(PatientFamilyPlanning a);
        int UpdateFamilyPlanningStatus(PatientFamilyPlanning u);
        int DeleteFamilyPlanningStatus(int Id);
        List<PatientFamilyPlanning> GetPatientFamilyPlanningStatus(int patientId);
        int CheckFamilyPlanningExists(int patientId);
    }
}
