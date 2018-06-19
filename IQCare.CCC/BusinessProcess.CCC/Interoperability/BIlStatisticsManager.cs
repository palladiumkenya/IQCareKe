using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.IL;
using Interface.CCC.Interoperability;

namespace BusinessProcess.CCC.Interoperability
{
    public class BIlStatisticsManager :ProcessBase ,IIlStatisticsManager
    {
        public IlStatistics GetIlStatistics()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var stats = unitOfWork.IlStatisticsRepository.FindBy(x => x.Id > 0).FirstOrDefault();
                return stats;
            }
        }
    }
}