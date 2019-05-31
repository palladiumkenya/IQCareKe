using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Interface.CCC.Reporting
{
    public interface IReportingManager
    {
        DataTable gettxcurr(DateTime reportingdate);
        DataTable getdefaulters(DateTime reportingdate, int mindays, int maxdays);
        DataTable getltfu(DateTime fromdate, DateTime todate);
    }
}
