using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{


    public class BPatientClassification : ProcessBase, IPatientClassification
    {
        public DataTable SaveUpdatePatientClassification(int Ptn_pk, int LocationID, int Visit_pk, int ARTSponsorID, int UserId, DateTime DateEffective, int DeleteFlag)  
       {
           ClsObject PatientClassification = new ClsObject();
           //int retval = 0;
           try
           {
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);

               PatientClassification.Connection = this.Connection;
               PatientClassification.Transaction = this.Transaction;

               ClsUtility.Init_Hashtable();

               ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
               ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@VisitPk ", SqlDbType.Int, Visit_pk.ToString());
               ClsUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, ARTSponsorID.ToString());               
               ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());              
               ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, DateEffective.ToString());               
               ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

               DataTable DT = (DataTable)PatientClassification.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SavePatientClassification_Constella", ClsUtility.ObjectEnum.DataTable);
              
               DataMgr.CommitTransaction(this.Transaction);
               DataMgr.ReleaseConnection(this.Connection);
               return DT;
           }
           catch
           {
               DataMgr.RollBackTransation(this.Transaction);
               throw;
           }
           finally
           {
               PatientClassification = null;
               if (this.Connection != null)
                   DataMgr.ReleaseConnection(this.Connection);

           }
          
       }
        public int DeletePatientClassification(int Ptn_pk, int ARTSponsorID, DateTime DateEffective)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeletePatientClassification = new ClsObject();
                DeletePatientClassification.Connection = this.Connection;

                DeletePatientClassification.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());               
                //ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, ARTSponsorID.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, DateEffective.ToString());
                theAffectedRows = (int)DeletePatientClassification.ReturnObject(ClsUtility.theParams, "Pr_Clinical_DeletePatientClassification_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
        public DataSet GetClassification(int SystemId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsObject PatientClassification = new ClsObject();
                return (DataSet)PatientClassification.ReturnObject(ClsUtility.theParams, "pr_clinical_GetClassification_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetAllPatientClassificationData(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                //ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject PatientClassification = new ClsObject();
                return (DataSet)PatientClassification.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAllPatientClassificationData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

    }

}
