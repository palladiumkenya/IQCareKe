using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.IL;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BILMessageStatsManager : ProcessBase, iILMessageStatsManager
    {
        public List<ILMessageStats> GetIlMessageStats()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var stats = unitOfWork.IIlMessageStatsRepository.GetAll();
                return stats.ToList();
            }
        }
    }
}