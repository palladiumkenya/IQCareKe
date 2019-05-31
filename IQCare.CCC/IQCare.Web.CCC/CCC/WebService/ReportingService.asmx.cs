using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using IQCare.CCC.UILogic.Reporting;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for ReportingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ReportingService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public int getNumberOfTxcurr(string reportingdate)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int txcurrcount = 0;
            DataTable theDT = reportingLogic.gettxcurr(Convert.ToDateTime(reportingdate));
            txcurrcount = theDT.Rows.Count;
            return txcurrcount;
        }
        [WebMethod]
        public int getNumberOfDefaulters(string reportingdate, int mindays, int maxdays)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int txcurrcount = 0;
            DataTable theDT = reportingLogic.getdefaulters(Convert.ToDateTime(reportingdate), mindays, maxdays);
            txcurrcount = theDT.Rows.Count;
            return txcurrcount;
        }
        [WebMethod]
        public int getltfu(string fromdate, string todate)
        {
            ReportingResultsManager reportingLogic = new ReportingResultsManager();
            int ltfu = 0;
            DataTable theDT = reportingLogic.getltfu(Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));
            ltfu = theDT.Rows.Count;
            return ltfu;
        }
    }
}
