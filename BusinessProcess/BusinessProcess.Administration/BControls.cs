using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
namespace BusinessProcess.Administration
{
    public class BControls : ProcessBase, IControl
    {
        #region "Constructor"
        public BControls()
        {
        }
        #endregion
        public DataSet GetControl()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ControlManager = new ClsObject();
                return (DataSet)ControlManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectControl_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewControl(string Name, string sDataType,int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ControlManager = new ClsObject();
                ControlManager.Connection = this.Connection;
                ControlManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@DataType", SqlDbType.VarChar, sDataType);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)ControlManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_AddControl_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Control record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theDR[0]);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateControl(int ControlId,string Name, string sDataType, int UserID)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ControlManager = new ClsObject();
                ControlManager.Connection = this.Connection;
                ControlManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@DataType", SqlDbType.VarChar, sDataType);
                ClsUtility.AddParameters("@ControlId", SqlDbType.Int, ControlId.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)ControlManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateControl_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Control record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theDR[0]);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}
