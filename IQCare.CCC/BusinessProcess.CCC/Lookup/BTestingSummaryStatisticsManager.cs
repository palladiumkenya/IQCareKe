using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BTestingSummaryStatisticsManager : ProcessBase, ITestingSummaryStatisticsManager
    {
        public List<TestingSummaryStatistics> GetAllStatistics()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var statistics = unitOfWork.TestingSummaryStatisticsRepository.GetAll();
                unitOfWork.Dispose();
                return statistics.ToList();
            }
        }
    }
}
