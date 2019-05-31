using Application.Presentation;
using Interface.CCC;
using IQCare.CCC.UILogic.Visit;
using IQCare.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Interface.CCC.Reporting;
using Entities.CCC.Reports;

namespace IQCare.CCC.UILogic.Reporting
{
    public class ReportingResultsManager
    {
        public DataTable gettxcurr(DateTime reportingdate)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            return reportingResults.gettxcurr(reportingdate);
        }
        public DataTable getdefaulters(DateTime reportingdate, int mindays, int maxdays)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            return reportingResults.getdefaulters(reportingdate, mindays, maxdays);
        }
        public DataTable getltfu(DateTime fromdate, DateTime todate)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            return reportingResults.getltfu(fromdate, todate);
        }
    }
}
