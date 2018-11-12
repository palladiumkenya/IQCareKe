using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Encounter;
using Entities.CCC.Neonatal;

namespace DataAccess.CCC.Interface.Encounter
{
    public interface IMilestonesRepository: IRepository<PatientMilestone>
    {
        List<PatientMilestone> getPatientMilestones(int patientId);
        List<PatientMilestone> getMilestoneAssessed(int milestoneAssessed);
    }
}
