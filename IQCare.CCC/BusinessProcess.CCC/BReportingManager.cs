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

        public DataTable getfirstdefaulters(DateTime reportingdate, int mindays, int maxdays)
        {
            lock (this)
            {
                ClsObject ReportingResults = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ftodate", SqlDbType.DateTime, reportingdate.ToString());
                ClsUtility.AddParameters("@minfdefault", SqlDbType.DateTime, mindays.ToString());
                ClsUtility.AddParameters("@maxfdefault", SqlDbType.DateTime, maxdays.ToString());
                return (DataTable)ReportingResults.ReturnObject(ClsUtility.theParams, "sp_getdefaulters", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable getseconddefaulters(DateTime reportingdate, int mindays, int maxdays)
        {
            lock (this)
            {
                ClsObject ReportingResults = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@stodate", SqlDbType.DateTime, reportingdate.ToString());
                ClsUtility.AddParameters("@minsdefault", SqlDbType.DateTime, mindays.ToString());
                ClsUtility.AddParameters("@maxsdefault", SqlDbType.DateTime, maxdays.ToString());
                return (DataTable)ReportingResults.ReturnObject(ClsUtility.theParams, "sp_getSeconddefaulters", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable getltfu(DateTime fromdate, DateTime todate)
        {
            lock (this)
            {
                ClsObject ReportingResults = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@fromdate", SqlDbType.DateTime, fromdate.ToString());
                ClsUtility.AddParameters("@ltodate", SqlDbType.DateTime, todate.ToString());
                return (DataTable)ReportingResults.ReturnObject(ClsUtility.theParams, "sp_getltfu", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public int AddPatientTracing(Tracing PT)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                int Result = 0;
                unitOfWork.ReportingRepository.Add(PT);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public List<Tracing> GetPatientTracingData(int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var tracingData = unitOfWork.ReportingRepository.FindBy(x => x.PatientMasterVisitId == patientMasterVisitId).ToList();
                unitOfWork.Dispose();
                return tracingData;
            }
        }
    }
}
