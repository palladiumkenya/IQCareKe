using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.CCC.Context;
using DataAccess.Context;
using DataAccess.CCC.Interface.Adherence;

namespace DataAccess.CCC.Repository.Adherence
{
    public class AdherenceDailyRoutineRepository:BaseRepository<DailyRoutine>, IAdherenceDailyRoutineRepository
    {
        private readonly GreencardContext _context;
        public AdherenceDailyRoutineRepository():base(new GreencardContext())
        {

        }
        public AdherenceDailyRoutineRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<DailyRoutine> getDailyRoutine(int personId, int patientMasterVisitId)
        {
            IAdherenceDailyRoutineRepository dailyRoutineRepository = new AdherenceDailyRoutineRepository();
            var dailyRoutineList = dailyRoutineRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return dailyRoutineList.ToList();
        }
        public int updateDailyRoutine(DailyRoutine DR)
        {
            IAdherenceDailyRoutineRepository dailyRoutineRepository = new AdherenceDailyRoutineRepository();
            dailyRoutineRepository.updateDailyRoutine(DR);
            return DR.Id;
        }
    }
}
