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
        public DataTable getfirstdefaulters(DateTime reportingdate, int mindays, int maxdays)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            return reportingResults.getfirstdefaulters(reportingdate, mindays, maxdays);
        }
        public DataTable getseconddefaulters(DateTime reportingdate, int mindays, int maxdays)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            return reportingResults.getseconddefaulters(reportingdate, mindays, maxdays);
        }
        public DataTable getltfu(DateTime fromdate, DateTime todate)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            return reportingResults.getltfu(fromdate, todate);
        }

        public int AddPatientTracing(Tracing PT)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            int result = 0;
            result = reportingResults.AddPatientTracing(PT);
            return result;
        }

        public List<Tracing> getTracingData(int patientMasterVisitId)
        {
            IReportingManager reportingResults = (IReportingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BReportingManager, BusinessProcess.CCC");
            try
            {
                return reportingResults.GetPatientTracingData(patientMasterVisitId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
