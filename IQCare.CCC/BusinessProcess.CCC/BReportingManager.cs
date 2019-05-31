using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.Entity;
using DataAccess.CCC.Repository;
using Entities.CCC.Reports;
using Interface.CCC.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Common;

namespace BusinessProcess.CCC
{
    public class BReportingManager : ProcessBase, IReportingManager
    {
        public DataTable gettxcurr(DateTime reportingdate)
        {
            lock (this)
            {
                ClsObject ReportingResults = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@todate", SqlDbType.DateTime, reportingdate.ToString());

                return (DataTable)ReportingResults.ReturnObject(ClsUtility.theParams, "sp_gettxcurr", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable getdefaulters(DateTime reportingdate, int mindays, int maxdays)
        {
            lock (this)
            {
                ClsObject ReportingResults = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@todate", SqlDbType.DateTime, reportingdate.ToString());
                ClsUtility.AddParameters("@mindefault", SqlDbType.DateTime, mindays.ToString());
                ClsUtility.AddParameters("@maxdefault", SqlDbType.DateTime, maxdays.ToString());
                return (DataTable)ReportingResults.ReturnObject(ClsUtility.theParams, "sp_getdefaulters", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable getltfu(DateTime fromdate, DateTime todate)
        {
            lock (this)
            {
                ClsObject ReportingResults = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@fromdate", SqlDbType.DateTime, fromdate.ToString());
                ClsUtility.AddParameters("@todate", SqlDbType.DateTime, todate.ToString());
                return (DataTable)ReportingResults.ReturnObject(ClsUtility.theParams, "sp_getltfu", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}
