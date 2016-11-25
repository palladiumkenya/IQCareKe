using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;
using Interface.Administration;

namespace BusinessProcess.Security
{
    public class BUser : ProcessBase,IUser,ITest 
    {

        #region "Constructor"
        public BUser()
        {
        }
        #endregion

        #region "Application Settings"

        public DataTable GetFacilityList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataTable)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetFacilityCmbList_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

       
        public DataSet GetFacilitySettings()
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFacility_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region "Login Functions"

        public DataSet GetUserCredentials(string UserName,int LocationId, int SystemId)
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LoginName", SqlDbType.VarChar, UserName);
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)LoginManager.ReturnObject(ClsUtility.theParams, "Pr_Security_UserLogin_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetEmployeeDetails()
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataTable)LoginManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public int UpdateAppointmentStatus(string Currentdate,int locationid )
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Currentdate", SqlDbType.VarChar, Currentdate.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, locationid.ToString());
                return (int)LoginManager.ReturnObject(ClsUtility.theParams, 
                    "pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }
      #endregion

        public string GetLoggedNFullName(int userID)
        {
            lock (this)
            {
                ClsObject obj = new ClsObject();
                ClsUtility.Init_Hashtable();
               // ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userID);

                 DataTable dt = (DataTable) obj.ReturnObject(ClsUtility.theParams,
                    string.Format("Select * From mst_User Where userID={0} ", userID),
                    ClsUtility.ObjectEnum.DataTable);

                 if (dt != null && dt.Rows.Count> 0)
                 {
                     return string.Format("{0} {1} username= {2}",
                         dt.Rows[0]["UserLastName"],
                         dt.Rows[0]["UserFirstName"]
                         , dt.Rows[0]["UserName"]);
                 }
                 else
                 {
                     return "User Not Found";
                 }

            }
        }
    }
}
