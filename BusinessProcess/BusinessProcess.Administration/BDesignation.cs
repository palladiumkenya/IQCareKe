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
    public class BDesignation : ProcessBase, IDesignation
    {
        #region "Constructor"
        public BDesignation()
        {
        }
        #endregion
        public DataSet GetDesignation()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DesignationManager = new ClsObject();
                return (DataSet)DesignationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDesignation_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDesignationByID(int designationid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@designationid", SqlDbType.Int, designationid.ToString());

                ClsObject DesignationManager = new ClsObject();
                return (DataSet)DesignationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDesignationByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewDesignation(string DesignationName, int UserID,  int Sequence)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DesignationManager = new ClsObject();
                DesignationManager.Connection = this.Connection;
                DesignationManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Designation_Name", SqlDbType.VarChar, DesignationName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
  //              ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());

               // DataRow theDR;
                int RowsAffected = (Int32)DesignationManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertDesignation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Designation record. Try Again..";
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

        public int UpdateDesignation(int DesignationID, string DesignationName, int UserID, int DeleteFlag, int Sequence)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DesignationManager = new ClsObject();
                DesignationManager.Connection = this.Connection;
                DesignationManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Designation_Name", SqlDbType.VarChar, DesignationName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@Designation_ID", SqlDbType.Int, DesignationID.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());

               // DataRow theDR;
                int RowsAffected = (Int32)DesignationManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateDesignation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Designation record. Try Again..";
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
        public void DeleteDesignation(int DesignationID)
        {
            ClsObject DesignationManager = new ClsObject();
            try
            {
                lock (this)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Original_Designation_id", SqlDbType.Int, DesignationID.ToString());
                    DesignationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteDesignation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DesignationManager = null;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                DesignationManager = null;
            }

        }
    }
}
