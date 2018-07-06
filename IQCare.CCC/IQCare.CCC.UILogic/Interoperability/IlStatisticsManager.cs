using System;
using Application.Presentation;
using Entities.CCC.IL;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class IlStatisticsManager
    {
        private readonly IIlStatisticsManager _ilStatisticsManager = (IIlStatisticsManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BIlStatisticsManager, BusinessProcess.CCC");

        public IlStatistics GetILStatistics()
        {
            try
            {
                return _ilStatisticsManager.GetIlStatistics();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}