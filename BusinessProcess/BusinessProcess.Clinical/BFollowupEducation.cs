using System;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Clinical;

namespace BusinessProcess.Clinical
{
    public class BFollowupEducation : ProcessBase, IFollowupEducation
    {
        public int DeleteFollowupEducation(int Id, int Ptn_pk)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteFollowupEducation = new ClsObject();
                DeleteFollowupEducation.Connection = this.Connection;

                DeleteFollowupEducation.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();

                ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, Id);
                ClsUtility.AddExtendedParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk);

                theAffectedRows = (int)DeleteFollowupEducation.ReturnObject(ClsUtility.theParams, "Pr_Clinical_DeleteFollowupEducation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedRows;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetAllFollowupEducationData(int patientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Ptn_pk", SqlDbType.Int, patientId);
                ClsObject FollowupEducation = new ClsObject();
                return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAllFollowupEducationData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCouncellingTopic(int CouncellingTypeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, CouncellingTypeId);
                ClsObject FollowupEducation = new ClsObject();
                return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "pr_clinical_GetCouncellingTypeTopic_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCouncellingType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FollowupEducation = new ClsObject();
                return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "pr_clinical_GetCouncellingType_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetSearchFollowupEducation(int patientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Ptn_pk", SqlDbType.Int, patientId);
                ClsObject FollowupEducation = new ClsObject();
                return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetFollowupEducation_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveFollowupEducation(int Id, int patientId, int typeId, int topicId, int visitPk, int locationId, DateTime visitDate, 
            string comments, string otherDetail, int userId, int deleteFlag,int? moduleId = null)
        {
            ClsObject FollowupEducation = new ClsObject();
            int retval = 0;
            try
            {
                //this.Connection = DataMgr.GetConnection();
                //this.Transaction = DataMgr.BeginTransaction(this.Connection);

                //FollowupEducation.Connection = this.Connection;
                //FollowupEducation.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, Id);
                ClsUtility.AddExtendedParameters("@Ptn_pk", SqlDbType.Int, patientId);
                ClsUtility.AddExtendedParameters("@VisitPk ", SqlDbType.Int, visitPk);
                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
                ClsUtility.AddExtendedParameters("@CouncellingTypeId", SqlDbType.Int, typeId);
                ClsUtility.AddExtendedParameters("@CouncellingTopicId", SqlDbType.Int, topicId);
                ClsUtility.AddExtendedParameters("@VisitDate", SqlDbType.DateTime, visitDate.ToString());
                ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, comments);
                ClsUtility.AddParameters("@OtherDetail", SqlDbType.VarChar, otherDetail);
                ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
                ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Int, deleteFlag);
                if (moduleId.HasValue)
                {
                    ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId.Value);
                }

                retval = (int)FollowupEducation.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SaveFollowupEducation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

               // DataMgr.CommitTransaction(this.Transaction);
                //DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
               // DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                FollowupEducation = null;
                //if (this.Connection != null)
                //    DataMgr.ReleaseConnection(this.Connection);
            }
            return retval;
        }
    }
}