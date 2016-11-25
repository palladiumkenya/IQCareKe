using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Clinical;


namespace BusinessProcess.Clinical
{
    public class BFamilyInfo : ProcessBase, IFamilyInfo
    {
        public int SaveFamilyInfo(int Id, int Ptn_Pk, string RFirstName, string RLastName, int Sex, int AgeYear, int AgeMonth, int RelationshipType, int HivStatus, int HivCareStatus, int UserId, int DeleteFlag, int ReferenceId, string RegistrationNo, DateTime RelationshipDate)
        {
            ClsObject FamilyInfo = new ClsObject();
            int retval = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                FamilyInfo.Connection = this.Connection;
                FamilyInfo.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@RFirstName", SqlDbType.VarChar, RFirstName.ToString());
                ClsUtility.AddParameters("@RLastName", SqlDbType.VarChar, RLastName.ToString());
                ClsUtility.AddParameters("@Sex", SqlDbType.Int, Sex.ToString());
                ClsUtility.AddParameters("@AgeYear", SqlDbType.Int, AgeYear.ToString());
                if (AgeMonth != 0)
                {
                    ClsUtility.AddParameters("@AgeMonth", SqlDbType.Int, AgeMonth.ToString());
                }
                ClsUtility.AddParameters("@RelationshipType", SqlDbType.Int, RelationshipType.ToString());
                ClsUtility.AddParameters("@HivStatus", SqlDbType.Int, HivStatus.ToString());
                ClsUtility.AddParameters("@HivCareStatus", SqlDbType.Int, HivCareStatus.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@ReferenceId", SqlDbType.Int, ReferenceId.ToString());
                ClsUtility.AddParameters("@RegistrationNo", SqlDbType.VarChar, RegistrationNo.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                if (RelationshipDate.DayOfYear != 1)
                {
                    ClsUtility.AddParameters("@RelationshipDate", SqlDbType.DateTime, RelationshipDate.ToString());
                }
                retval = (int)FamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SaveFamilyInfo_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                
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
                FamilyInfo = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }
        public DataSet GetAllFamilyData(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FamilyInfo = new ClsObject();
                return (DataSet)FamilyInfo.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAllFamilyData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
     
        public DataSet GetSearchFamilyInfo(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FamilyInfo = new ClsObject();
                return (DataSet)FamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetFamilyInfo_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject GetDropDowns = new ClsObject();
                return (DataSet)GetDropDowns.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int DeleteFamilyInfo(int Id,int @UserId)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteFamilyInfo = new ClsObject();
                DeleteFamilyInfo.Connection = this.Connection;
                DeleteFamilyInfo.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                theAffectedRows = (int)DeleteFamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_Clinical_DeleteFamilyInfo_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
    }


}
