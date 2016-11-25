using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BUser : ProcessBase,Iuser 
    {
        #region "Constructor"
        public BUser()
        {
        }
        #endregion

        #region "User List"

        public DataSet GetUserList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetUserList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Create User"
        public DataSet FillDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetDropDownData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewUser(string FName,string LName,string UserName,string Password,int UserId,int EmpId, Hashtable UserGroup)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                                
                UserManager.Connection = this.Connection;
                UserManager.Transaction = this.Transaction;

                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@fname", SqlDbType.VarChar, FName);
                ClsUtility.AddParameters("@lname", SqlDbType.VarChar, LName);
                ClsUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
                ClsUtility.AddParameters("@EmpId", SqlDbType.Int, EmpId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, Password);
                ClsUtility.AddParameters("@userid", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveNewUser_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                    return Convert.ToInt32(theDR[0]); 
                }

                #region "Insert Groups"
                int i = 1;
                for (i = 1; i <= UserGroup.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@UserId",SqlDbType.Int, theDR[0].ToString());
                    ClsUtility.AddParameters("@GroupId",SqlDbType.Int,UserGroup[i].ToString());
                    ClsUtility.AddParameters("@OperatorId", SqlDbType.Int, UserId.ToString());
                    UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertUserGroup_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                #endregion

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theDR[0]);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection); 
            }
        }

        public DataSet GetUserRecord(int UserId)
        {
            lock (this)
            {
                ClsObject UserManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectUser_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public void UpdateUserRecord(string FName, string LName, string UserName, string Password, int UserId,int OperatorId,int EmpId, Hashtable UserGroup)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                int RowsAffected = 0;

                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserLastName",SqlDbType.VarChar,LName);
                ClsUtility.AddParameters("@UserFirstName",SqlDbType.VarChar,FName);
                ClsUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
                ClsUtility.AddParameters("@Password",SqlDbType.VarChar,Password);
                ClsUtility.AddParameters("@EmpId", SqlDbType.Int, EmpId.ToString());
                ClsUtility.AddParameters("@OperatorID",SqlDbType.Int,OperatorId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());

                RowsAffected = (int)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_UpdateUser_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected < 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                #region "Update User Groups"

                string theSQL = string.Format("Delete from Lnk_UserGroup where UserId = {0}", UserId);
                ClsUtility.Init_Hashtable();
                RowsAffected = (int)UserManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);

                int i = 1;
                for (i = 1; i <= UserGroup.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@GroupId", SqlDbType.Int, UserGroup[i].ToString());
                    ClsUtility.AddParameters("@OperatorId", SqlDbType.Int, UserId.ToString());
                    UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertUserGroup_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                #endregion

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int DeleteUserRecord(int UserId)
        {
            ClsObject UserManager = new ClsObject();
            int theAffectedRow = 0;
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                theAffectedRow = (int)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteUser_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return theAffectedRow;
                //UserManager = null;
            }
            catch
            {
                throw;
            }
            finally
            {
                UserManager = null;
                
            }

        }
       #endregion

    }
}
