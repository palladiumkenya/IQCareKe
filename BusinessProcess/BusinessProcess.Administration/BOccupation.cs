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
    public class BOccupation : ProcessBase, IOccupation
    {
        #region "Constructor"
        public BOccupation()
        {
        }
        #endregion
        public DataSet GetOccupation()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject OccupationManager = new ClsObject();
                return (DataSet)OccupationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectOccupation_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetOccupationByID(int occid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@occid", SqlDbType.Int, occid.ToString());
                ClsObject OccupationManager = new ClsObject();
                return (DataSet)OccupationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectOccupationByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewOccupation(string OccupationName, int UserID,  int Sequence)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject OccupationManager = new ClsObject();
                OccupationManager.Connection = this.Connection;
                OccupationManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OccupationName", SqlDbType.VarChar, OccupationName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
              //  ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());

              //  DataRow theDR;
                int RowsAffected = (Int32)OccupationManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertOccupation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Occupation record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
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
        public int UpdateOccupation(int OccupationID, string OccupationName, int UserID, int DeleteFlag, int Sequence)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject OccupationManager = new ClsObject();
                OccupationManager.Connection = this.Connection;
                OccupationManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OccupationName", SqlDbType.VarChar, OccupationName);
                ClsUtility.AddParameters("@OccupationID", SqlDbType.Int, OccupationID.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());


                
                int RowsAffected = (Int32)OccupationManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateOccupation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Occupation record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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
        public void DeleteOccupation(int OccupationId)
        {
            ClsObject OccupationManager = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Original_OccupationID", SqlDbType.Int, OccupationId.ToString());
                OccupationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteOccupation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                OccupationManager = null;
            }
            catch
            {
                throw;
            }
            finally
            {
                OccupationManager = null;
            }

        }
    }
}
