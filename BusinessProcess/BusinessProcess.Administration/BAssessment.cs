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
    public class BAssessment :ProcessBase,IAssessment 
    {
        #region "Constructor"
        public BAssessment()
        {
        }
   #endregion
        public DataSet GetAssessmentList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                return (DataSet)AssessmentManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectAssessmentandCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteAssessment(int AssessmentID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                ClsUtility.AddParameters("@Original_AssessmentID", SqlDbType.Int, AssessmentID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteAssessment_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAssessmentTypeList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                return (DataSet)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SelectAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAssessmentByIDList(int AssessmentID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                ClsUtility.AddParameters("@assessmentid", SqlDbType.Int, AssessmentID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SelectAssessmentByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAssessmentTypeByIDList(int AssessmentCategoryID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                ClsUtility.AddParameters("@assessmentcategoryid", SqlDbType.Int, AssessmentCategoryID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SelectAssessmentCategoryByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteAssessmentType(int AssessmentCategoryID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                ClsUtility.AddParameters("@Original_AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveNewAssessment(int AssessmentCategoryID, string AssessmentName, int UserId)
        
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());
                ClsUtility.AddParameters("@AssessmentName", SqlDbType.VarChar, AssessmentName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertAssessment_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Assessment record. Try Again..";
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
        public int SaveNewAssessmentType(string AssessmentCategoryName, int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@AssessmentCategoryName", SqlDbType.VarChar, AssessmentCategoryName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_AddAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Assessment type record. Try Again..";
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
        public int UpdateAssessment(int AssessmentId,int AssessmentCategoryID, string AssessmentName, int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@AssessmentID", SqlDbType.Int, AssessmentId.ToString());
                ClsUtility.AddParameters("@AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());
                ClsUtility.AddParameters("@AssessmentName", SqlDbType.VarChar, AssessmentName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateAssessment_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in updating Assessment record. Try Again..";
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
        public int UpdateAssessmentType( int AssessmentCategoryID, string AssessmentCategoryName, int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());
                ClsUtility.AddParameters("@AssessmentName", SqlDbType.VarChar, AssessmentCategoryName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in updating Assessment type record. Try Again..";
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
