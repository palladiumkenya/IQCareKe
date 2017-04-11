using Entities.CCC.Triage;
using System.Collections.Generic;

namespace Interface.CCC.Triage
{
    public interface IPatientFamilyPlanningMethodManager
    {
        int AddFamilyPlanningMethod(PatientFamilyPlanningMethod a);
        int UpdateFamilyPlanningMethod(PatientFamilyPlanningMethod u );
        int DeleteFamilyPlanningMethod(int id);  
        List<PatientFamilyPlanningMethod> GetPatientFamilyPlanningMethod(int patientId);
        int CheckIfPatientHasFamilyPlanningMethod(int patientId);
    }
}
