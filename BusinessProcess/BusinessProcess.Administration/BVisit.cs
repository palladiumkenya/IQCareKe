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
    public class BVisit : ProcessBase,IVisit 
    {
        #region "Constructor"
        public BVisit()
        {
        }
        #endregion
        public DataSet GetVisitTypeByID(int visitid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject VisitTypeManager = new ClsObject();
                ClsUtility.AddParameters("@visittypeid", SqlDbType.Int, visitid.ToString());
                return (DataSet)VisitTypeManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectVisitTypesByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetVisitType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject VisitTypeManager = new ClsObject();
                return (DataSet)VisitTypeManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectVisitTypes_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteVisitType(int visittypeid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject VisitTypeManager = new ClsObject();
                ClsUtility.AddParameters("@Original_VisitTypeID", SqlDbType.Int, visittypeid.ToString());
                return (DataSet)VisitTypeManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteVisitType_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveNewVisitType( string VisitName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject VisitTypeManager = new ClsObject();
                VisitTypeManager.Connection = this.Connection;
                VisitTypeManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@VisitName", SqlDbType.VarChar, VisitName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)VisitTypeManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_AddVisitType_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Visit type record. Try Again..";
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
        public int UpdateVisitType(int VisitTypeID, string VisitName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject VisitTypeManager = new ClsObject();
                VisitTypeManager.Connection = this.Connection;
                VisitTypeManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@VisitTypeID", SqlDbType.Int, VisitTypeID.ToString());
                ClsUtility.AddParameters("@VisitName", SqlDbType.VarChar, VisitName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)VisitTypeManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateVisitType_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Visit type record. Try Again..";
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
