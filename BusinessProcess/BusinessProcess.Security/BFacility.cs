using System;
using System.Data;
using System.Data.SqlClient;

using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;
using Application.Common;

namespace BusinessProcess.Security
{
    public class BFacility : ProcessBase,IFacility 
    {
        #region "Constructor"
        public BFacility()
        {
        }
        #endregion

        public DataSet GetFacilityStats(int LocationId)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Security_facilitydetails1_constella", ClsUtility.ObjectEnum.DataSet);
            }
           
        }

        public DataSet GetHIVCareFacilityStats(int LocationId)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Security_HIVCareFacilityStatistics_constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetFacilityStatsPMTCT(int LocationId)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Security_facilitydetailsPMTCT_constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetFacilityStatsExposedInfants(int LocationId)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Security_facilitydetailsExposedInfants_constella", ClsUtility.ObjectEnum.DataSet);
            }
        }


        public DataSet GetFacilityData(DateTime RangeFrom, DateTime RangeTo, int LocationId)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DateFrom", SqlDbType.DateTime, RangeFrom.ToString());
                ClsUtility.AddParameters("@DateTo", SqlDbType.DateTime, RangeTo.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationId.ToString());
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Security_GetfacilityDatainRange_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetExportData(string theStr)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@theStr", SqlDbType.DateTime, theStr.ToString());
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Security_GetfacilityExportData_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }


    }
}
