using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;

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
