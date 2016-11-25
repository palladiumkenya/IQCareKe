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
    public class BCodeDecode : ProcessBase,ICodeDecode 
    {
        #region "Constructor"
        public BCodeDecode()
        {
        }
        #endregion
        public DataSet GetCodes()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CodeManager = new ClsObject();
                return (DataSet)CodeManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectCode_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDecCodes()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DeCodeManager = new ClsObject();
                return (DataSet)DeCodeManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectDeCode_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }



        public int SaveNewCode(string Name, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CodeManager = new ClsObject();
                CodeManager.Connection = this.Connection;
                CodeManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)CodeManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_AddCode_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Code record. Try Again..";
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


        public int UpdateCode(int CodeId,string Name, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CodeManager = new ClsObject();
                CodeManager.Connection = this.Connection;
                CodeManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)CodeManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateCode_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in updating Code record. Try Again..";
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

        public int SaveNewDeCode(string Name, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeCodeManager = new ClsObject();
                DeCodeManager.Connection = this.Connection;
                DeCodeManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)DeCodeManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_AddDeCode_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving DeCode record. Try Again..";
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

        public int UpdateDeCode(string Name, int ID,int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeCodeManager = new ClsObject();
                DeCodeManager.Connection = this.Connection;
                DeCodeManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());

                DataRow theDR;
                theDR = (DataRow)DeCodeManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateDeCode_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving DeCode record. Try Again..";
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
