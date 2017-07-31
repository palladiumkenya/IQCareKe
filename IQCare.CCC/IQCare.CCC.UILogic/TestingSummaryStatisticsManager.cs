using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class TestingSummaryStatisticsManager
    {
        readonly ITestingSummaryStatisticsManager mgr = (ITestingSummaryStatisticsManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BTestingSummaryStatisticsManager, BusinessProcess.CCC");

        public List<TestingSummaryStatistics> GetAllStatistics()
        {
            try
            {
                return mgr.GetAllStatistics();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
