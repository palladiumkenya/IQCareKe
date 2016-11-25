using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Administration;
using System;
using System.Data;
/////////////////////////////////////////////////////////////////////
// Code Written By   : Rakhi Tyagi
// Written Date      : 1 Sept 2006
// Modification Date : 30 Oct 2006
// Description       : Add/Edit UserGroup 
// Modification Date : 16 Feb 2007
/// /////////////////////////////////////////////////////////////////


namespace BusinessProcess.Administration
{
    public class BUserRole : ProcessBase, IUserRole
    {

        #region "Constructor"

        public BUserRole()
        {
        }

        #endregion

        #region Get UserRole List
        public DataSet GetUserRoleList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectGroup_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region Get UserGroupFeature List
        public DataSet GetUserGroupFeatureList(Int32 theSID,Int32 theFID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, theSID.ToString());
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, theFID.ToString());
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFeature_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region Get UserGroupFeatureList By ID
        public DataSet GetUserGroupFeatureListByID(int UserID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("GroupID", SqlDbType.Int, UserID.ToString());
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectUserGroupDetailByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
            

        }
        #endregion

        #region Add New UserGroupFeature

        public int SaveUserGroupDetail(int GroupID, string Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd, int EditIdentifiers)
        {
            ClsObject UserGroupManager = new ClsObject();
            
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                UserGroupManager.Connection = this.Connection;
                UserGroupManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                DataRow theDR ;

                ClsUtility.AddParameters("@GID", SqlDbType.Int, GroupID.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@GroupName", SqlDbType.VarChar, Groupname);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@PerEnrollment", SqlDbType.Int, EnrollmentFlag.ToString());
                ClsUtility.AddParameters("@PerCareEnd", SqlDbType.Int, PreCareEnd.ToString());
                ClsUtility.AddParameters("@EditIdentifiers", SqlDbType.Int, EditIdentifiers.ToString());
                

                theDR = (DataRow)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroup_Detail_Constella", ClsUtility.ObjectEnum.DataRow);
                int groupId = Convert.ToInt32(theDR[0].ToString());

                //if (GroupId == 0)
                //{
                //    MsgBuilder theMsg = new MsgBuilder();
                //    theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                //    Exception ex = AppException.Create("#C1", theMsg);
                //    throw ex;
                //}
                if(groupId != 0)
                {
                    for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[0].Rows[i]["Feature"].ToString());
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Save"].ToString());
                        ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Update"].ToString());
                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Delete"].ToString());
                        ClsUtility.AddParameters("@ViewFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["View"].ToString());
                        ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Print"].ToString());
                        
                        ClsUtility.AddParameters("@GroupID", SqlDbType.Int, groupId.ToString());

                        int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroupFunction_Detail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        if (RowsAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                            AppException.Create("#C1", theMsg);
                           
                        }

                    }

                    for (int i = 0; i < theDS.Tables[1].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[1].Rows[i]["Feature"].ToString());
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@ViewFlag", SqlDbType.Int, theDS.Tables[1].Rows[i]["View"].ToString());
                        ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@GroupID", SqlDbType.Int, groupId.ToString());

                        int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroupFunction_Detail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                            AppException.Create("#C1", theMsg);
                            
                        }
                    }


                    for (int i = 0; i < theDS.Tables[2].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[2].Rows[i]["Feature"].ToString());
                        if (theDS.Tables[2].Rows[i]["View"].ToString() != "0")
                        {
                            ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, "5");
                            ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "4");
                            ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, "2");
                            ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "3");
                        }
                        else
                        {
                            ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "0");
                            ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, "0");
                            ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                            ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, "0");
                        }
                        ClsUtility.AddParameters("@ViewFlag", SqlDbType.Int, theDS.Tables[2].Rows[i]["View"].ToString());
                        ClsUtility.AddParameters("@GroupID", SqlDbType.Int, groupId.ToString());

                        int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroupFunction_Detail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                            AppException.Create("#C1", theMsg);
                            
                        }
                    }
                }

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
                UserGroupManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Update UserGroup"
        public void UpdateUserGroup(int GroupId, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd,int EditIdentifiers)
        {
            ClsObject UserGroupManager = new ClsObject();
            DataRow theDR;
            try
            {
                int theRowsAffected = 0;
                ClsUtility.Init_Hashtable();
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                UserGroupManager.Connection = this.Connection;
                UserGroupManager.Transaction = this.Transaction;

                #region "Update UserGroup"

                ClsUtility.AddParameters("@GID", SqlDbType.Int, GroupId.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@GroupName", SqlDbType.VarChar, Groupname);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@PerEnrollment", SqlDbType.Int, EnrollmentFlag.ToString());
                ClsUtility.AddParameters("@PerCareEnd", SqlDbType.Int, PreCareEnd.ToString());
                ClsUtility.AddParameters("@EditIdentifiers", SqlDbType.Int, EditIdentifiers.ToString());
                theDR = (DataRow)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroup_Detail_Constella", ClsUtility.ObjectEnum.DataRow);
                if (theDR[0].ToString() == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating UserGroup. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #endregion


                /************   Delete Previous Records **********/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Original_GroupID", SqlDbType.Int, GroupId.ToString());
                
                theRowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteGroupFeature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowsAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating UserGroupRole. Try Again..";
                    AppException.Create("#C1", theMsg);

                }

               

                #region "Insert Records"
                for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[0].Rows[i]["Feature"].ToString());
                    ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Save"].ToString());
                    ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Update"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Delete"].ToString());
                    ClsUtility.AddParameters("@ViewFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["View"].ToString());
                    ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, theDS.Tables[0].Rows[i]["Print"].ToString());
                    ClsUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());

                    int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroupFunction_Detail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (RowsAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                        AppException.Create("#C1", theMsg);
                       
                    }

                }

                for (int i = 0; i < theDS.Tables[1].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[1].Rows[i]["Feature"].ToString());
                    ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@ViewFlag", SqlDbType.Int, theDS.Tables[1].Rows[i]["View"].ToString());
                    ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());
                  
                    int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroupFunction_Detail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (RowsAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                        AppException.Create("#C1", theMsg);
                       
                    }
                }


                for (int i = 0; i < theDS.Tables[2].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[2].Rows[i]["Feature"].ToString());
                    if (theDS.Tables[2].Rows[i]["View"].ToString() != "0")
                    {
                        ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, "5");
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "4");
                        ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, "2");
                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "3");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@SaveFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@UpdateFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                        ClsUtility.AddParameters("@PrintFlag", SqlDbType.Int, "0");
                    }
                    ClsUtility.AddParameters("@ViewFlag", SqlDbType.Int, theDS.Tables[2].Rows[i]["View"].ToString());
                    ClsUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());
                    
                    int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroupFunction_Detail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (RowsAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                        AppException.Create("#C1", theMsg);
                        
                    }
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
                UserGroupManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

 
    }  

}


