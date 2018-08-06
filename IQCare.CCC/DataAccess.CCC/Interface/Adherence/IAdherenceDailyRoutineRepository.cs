using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.Context;

namespace DataAccess.CCC.Interface.Adherence
{
    public interface IAdherenceDailyRoutineRepository : IRepository<DailyRoutine>
    {
        List<DailyRoutine> getDailyRoutine(int personId, int patientMasterVisitId);
        int updateDailyRoutine(DailyRoutine DR);
    }
}
