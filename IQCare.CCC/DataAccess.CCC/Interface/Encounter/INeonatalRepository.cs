using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Encounter;
using Entities.CCC.Neonatal;

namespace DataAccess.CCC.Interface.Encounter
{
    public interface INeonatalRepository: IRepository<PatientNeonatalHistory>
    {
        List<PatientNeonatalHistory> getNeonatalNotes(int personId, int patientMasterVisitId);
    }
}
