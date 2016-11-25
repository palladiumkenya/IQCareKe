using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Interface.FormBuilder
{
    public interface IQueryBuilder
    {
        SqlConnection GetConnectionQueryBuilder();
        DataSet ReturnQueryResult(string theQuery);
        DataTable GetReportsCategory();
        DataTable GetCustomReports(Int32 CategoryId);
        DataTable SaveCustomReport(Int32 ReportId, Int32 CategoryId, string CategoryName, string ReportName, string ReportQuery, Int32 UserId);
        DataTable SaveCustomReport(Int32 ReportId, Int32 CategoryId, string CategoryName, string ReportName, string ReportQuery, Int32 UserId, string queryParameter);
        DataSet ExportReport(int ReportId);
        int SaveUpdateQueryBuilderReport(DataSet dsReportDetails, Int32 theUserId);
    }
}
