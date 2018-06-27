using System;
using System.Collections.Generic;
using Entities.CCC.Neonatal;
using Entities.CCC.Encounter;

namespace Interface.CCC
{
    public interface IPatientNeonatal
    {
        int AddPatientNeonatal(PatientMilestone p);
        int AddImmunizationHistory(PatientImmunizationHistory I);
        int AddPatientNeonatalHistory(PatientNeonatalHistory n);
        void DeleteImmunization(int id);
    }
}
