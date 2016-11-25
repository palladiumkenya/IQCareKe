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
    public class BEducation : ProcessBase,IEducation 
    {
        #region "Constructor"
        public BEducation()
        {
        }
        #endregion

        #region Education List
        public DataSet GetEducation()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject EducationManager = new ClsObject();

                return (DataSet)EducationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_Select_Education_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region Education By ID
        public DataSet GetEducationByID(int EducationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@EducationId", SqlDbType.Int, EducationId.ToString());

                ClsObject EducationManager = new ClsObject();
                return (DataSet)EducationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_Select_EducationByID_Constella ", ClsUtility.ObjectEnum.DataSet);
            }
        
        }
        #endregion

        #region Save New Education
        public int SaveNewEducation( int UserID, string EducationName , int Sequence)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject EducationManager = new ClsObject();
                EducationManager.Connection = this.Connection;
                EducationManager.Transaction = this.Transaction;
                
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@EducationName", SqlDbType.VarChar, EducationName);
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                
                DataRow theDR;
                theDR = (DataRow)EducationManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_Insert_Education_Constella", ClsUtility.ObjectEnum.DataRow);
                
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Education record. Try Again..";
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

        #endregion

        #region Update Education
        public int UpdateEducation(int EducationID, int UserID, string EducationName, int DeleteFlag, int Sequence)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject EducationManager = new ClsObject();
                EducationManager.Connection = this.Connection;
                EducationManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@EducationId", SqlDbType.Int, EducationID.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@EducationName", SqlDbType.VarChar, EducationName);
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
              
                int rowsAffected = (Int32)EducationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_Update_Education_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
              
                if (rowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Education record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(rowsAffected);
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

        #endregion

        #region Remove Education

        public DataSet DeleteEducation(int EducationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject EducationManager = new ClsObject();
                ClsUtility.AddParameters("@Original_EducationID", SqlDbType.Int, EducationId.ToString());

                return (DataSet)EducationManager.ReturnObject(ClsUtility.theParams, "pr_Admin_Delete_Education_Constella", ClsUtility.ObjectEnum.DataSet);
            }
            
        }
        #endregion
    }
}
