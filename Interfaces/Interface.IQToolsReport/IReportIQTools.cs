using System.Data;
using System.Xml;
using DataAccess.Report;
using System.Collections.Generic;
using System;
using System.Web.UI;
using System.Web;
namespace Interface.IQToolsReports
{
    /// <summary>
    /// 
    /// </summary>

    public  interface IReportIQTools
    {
       //Report
        /// <summary>
        /// Imports the reports from iq tools.
        /// </summary>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        void ImportReportsFromIQTools(bool overwrite = false);
        /// <summary>
        /// Sets the report parameters.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="cd4CutOff">The CD4 cut off.</param>
        /// <returns></returns>
        IReportIQTools SetReportParameters(DateTime dateFrom, DateTime dateTo, int cd4CutOff=350);
        /// <summary>
        /// Updates the report XSL.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        bool UpdateReportXsl(int reportId, byte[] buffer,string filename="",string fileExt="", string contentType="text/xsl", int fileLength=0);
        /// <summary>
        /// Removes the template document.
        /// </summary>
        /// <param name="reportID">The report identifier.</param>
        /// <returns></returns>
        bool GetXSLTemplate(Page pwPage);
        /// <summary>
        /// Gets the reports.
        /// </summary>
        /// <returns></returns>
        //List<Report> GetReports();
        /// <summary>
        /// Gets the reports.
        /// </summary>
        /// <returns></returns>
        DataTable GetReports();
        /// <summary>
        /// Gets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        string ReportName
        {
            get;
        }
        /// <summary>
        /// Gets the report XSD schema.
        /// </summary>
        /// <value>
        /// The report XSD schema.
        /// </value>
        string ReportXSDSchema { get; }
        /// <summary>
        /// Gets the report XSL.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        string ReportXslTemplate { get; }
        /// <summary>
        /// Gets the type of the report template content.
        /// </summary>
        /// <value>
        /// The type of the report template content.
        /// </value>
        string ReportTemplateContentType { get; }
        /// <summary>
        /// Gets a value indicating whether [report has template].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [report has template]; otherwise, <c>false</c>.
        /// </value>
        bool ReportHasTemplate { get; }

        /// <summary>
        /// Gets the size of the report template.
        /// </summary>
        /// <value>
        /// The size of the report template.
        /// </value>
        int ReportTemplateSize
        {
            get;
        }
        /// <summary>
        /// Gets the name of the report template file.
        /// </summary>
        /// <value>
        /// The name of the report template file.
        /// </value>
        string ReportTemplateFileName
        {
            get;
        }
        /// <summary>
        /// Gets the report queries.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <returns></returns>
        QueryCollection GetReportQueries(int reportId);
        /// <summary>
        /// Runs the report.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns></returns>
        void RunReport(int reportId, DateTime dateFrom, DateTime dateTo, int cd4CutOff = 350);
        /// <summary>
        /// Gets the report data.
        /// </summary>
        /// <returns></returns>
        string GetReportData();
        /// <summary>
        /// Runs the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        string ExecQuery(Query query);
        /// <summary>
        /// Gets the query maps.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        List<QueryMapping> GetQueryMaps(int queryId);
/// <summary>
/// Gets the reportby identifier.
/// </summary>
/// <param name="reportID">The report identifier.</param>
         void GetReportbyID(int reportID);
    }
}
