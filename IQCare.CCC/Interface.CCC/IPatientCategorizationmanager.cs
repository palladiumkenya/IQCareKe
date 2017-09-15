using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Encounter;

namespace Interface.CCC
{
    public interface IPatientCategorizationManager
    {
        int AddPatientCategorization(PatientCategorization p);
        PatientCategorization GetPatientCategorization(int id);
        void DeletePatientCategorization(int id);
        int UpdatePatientCategorization(PatientCategorization p);
        List<PatientCategorization> GetByPatientId(int patientId);
    }
}
