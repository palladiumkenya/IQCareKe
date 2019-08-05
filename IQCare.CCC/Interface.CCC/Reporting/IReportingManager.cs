using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Interface.CCC.Reporting;
using Entities.CCC.Reports;

namespace Interface.CCC.Reporting
{
    public interface IReportingManager
    {
        DataTable gettxcurr(DateTime reportingdate);
        DataTable getfirstdefaulters(DateTime reportingdate, int mindays, int maxdays);
        DataTable getseconddefaulters(DateTime reportingdate, int mindays, int maxdays);
        DataTable getltfu(DateTime fromdate, DateTime todate);
        int AddPatientTracing(Tracing PT);
        List<Tracing> GetPatientTracingData(int patientMasterVisitId);
    }
}
