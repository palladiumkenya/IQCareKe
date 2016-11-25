using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;  
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;


namespace BusinessProcess.Administration
{
    class BDeletePatient : ProcessBase, IDeletePatient
    {
        public DataTable GetPatientDetails(int PatientID)
        {
            lock (this)
            {
                ClsObject GetPatientDetails = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataTable)GetPatientDetails.ReturnObject(ClsUtility.theParams, "pr_Admin_GetPatientDetailById_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        //public DataTable GetPatientDetailsByEnrollment(string theEnrollID)
        //{
        //    ClsObject GetPatientDetails = new ClsObject();
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, theEnrollID);
        //    return (DataTable)GetPatientDetails.ReturnObject(ClsUtility.theParams, "pr_Admin_GetPatientDetailByEnrollment_Constella", ClsUtility.ObjectEnum.DataTable);
        //}
        
        public int DeletePatient(int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeletePatientDetails = new ClsObject();
                DeletePatientDetails.Connection = this.Connection;
                DeletePatientDetails.Transaction = this.Transaction;
                               
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                theAffectedRows =  (int)DeletePatientDetails.ReturnObject(ClsUtility.theParams, "pr_Admin_DeletePatient_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
