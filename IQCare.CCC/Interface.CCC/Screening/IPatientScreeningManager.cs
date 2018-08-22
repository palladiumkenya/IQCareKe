using Entities.CCC.Screening;
using System;
using System.Collections.Generic;

namespace Interface.CCC.Screening
{
    public interface IPatientScreeningManager
    {
        int AddPatientScreening(PatientScreening a);
        int UpdatePatientScreening(PatientScreening u);
        int DeletePatientScreening(int Id);
        List<PatientScreening> GetPatientScreening(int patientId);
        List<PatientScreening> GetPatientScreening(int patientId, DateTime visitdate, int screeningCategoryId);
        int CheckIfPatientScreeningExists(int patientId);
        int CheckIfPatientScreeningExists(int patientId, DateTime visitDate, int screeningCategoryId, int screeningTypeId);
    }
}
