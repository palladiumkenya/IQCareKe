using System.Data;
using DataAccess.Report;
using System;
using System.Collections.Generic;
using System.Xml;
namespace Interface.IQToolsReports
{
    public interface IReportIQQuery
    {
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        DataTable ExecQuery(Query query);
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="queryID">The query identifier.</param>
        /// <returns></returns>
        DataTable ExecQuery(int queryID, DateTime dateFrom, DateTime dateTo, int cd4CutOff = 350);
        /// <summary>
        /// Gets the full query document.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns></returns>
        string GetFullQueryDocument(string categoryName = "");

    }
}
