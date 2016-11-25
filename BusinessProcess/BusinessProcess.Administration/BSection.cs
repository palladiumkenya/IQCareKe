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
    public class BSection : ProcessBase
    {
        #region "Constructor"
        public BSection()
        {
        }
        #endregion

        public DataSet GetSection()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject SectionManager = new ClsObject();
                return (DataSet)SectionManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectSection_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewSection(string SectionName,  int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SectionManager = new ClsObject();
                SectionManager.Connection = this.Connection;
                SectionManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, SectionName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)SectionManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertSection_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Section record. Try Again..";
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

        public int UpdateSection(int SectionId,string SectionName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SectionManager = new ClsObject();
                SectionManager.Connection = this.Connection;
                SectionManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, SectionName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@SectionId", SqlDbType.Int, SectionId.ToString());

                DataRow theDR;
                theDR = (DataRow)SectionManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateSection_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Section record. Try Again..";
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
