using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.IL;
using Interface.CCC.Interoperability;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class IlStatisticsManager
    {
        private readonly IIlStatisticsManager _ilStatisticsManager = (IIlStatisticsManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BIlStatisticsManager, BusinessProcess.CCC");
        private readonly iILMessageStatsManager _ilMessageStatsManager = (iILMessageStatsManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BILMessageStatsManager, BusinessProcess.CCC");
        private readonly illMessageViewerManager _illMessageViewerManager = (illMessageViewerManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BlLMessageViewerManager, BusinessProcess.CCC");

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

        public List<ILMessageStats> GetMessageStats()
        {
            try
            {
                return _ilMessageStatsManager.GetIlMessageStats();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ILMessageViewer> GetMessages(string messageType, bool isSuccess)
        {
            try
            {
                return _illMessageViewerManager.GetIlMessages(messageType, isSuccess);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}