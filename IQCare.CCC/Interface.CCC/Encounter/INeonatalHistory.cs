using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;
using Entities.CCC.Neonatal;

namespace Interface.CCC.Encounter
{
    public interface INeonatalHistory
    {
        List<PatientNeonatalHistory> getNeonatalNotes(int PatientId, int PatientMasterVisitId);
        int updateNeonatalNotes(PatientNeonatalHistory nn);
        List<PatientMilestone> getPatientMilestones(int patientId);
        List<PatientImmunizationHistory> getPatientImmunization(int patientId);
        List<PatientMilestone> getMilestoneAssessed(int milestoneAssessed);
        void DeleteMilestone(int Id);
    }
}
