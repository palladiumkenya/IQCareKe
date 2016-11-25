using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Clinical ;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;


namespace BusinessProcess.Clinical
{
   public class BDeleteForm : ProcessBase, IDeleteForm
    {
       #region "Constuctor"
        public BDeleteForm()
        {
        }
        #endregion
        public DataSet GetPatientForms(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientGetForm = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                //return (DataSet)PatientGetForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientForms_Constella", ClsUtility.ObjectEnum.DataSet);

                return (DataSet)PatientGetForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientHistory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        
        
        }




       //public int DeletePatientForms(DataTable theDT, int PatientId, int DeleteFlag)
       public int DeletePatientForms(DataTable theDT, int PatientId)
       {

           try
           {
               int theAffectedRows = 0;
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);
               
               ClsObject PatientDeleteForm = new ClsObject();
               PatientDeleteForm.Connection = this.Connection;
               PatientDeleteForm.Transaction = this.Transaction;

               for (int i = 0; i < theDT.Rows.Count; i++)
               {
                   ClsUtility.Init_Hashtable();
                   ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, theDT.Rows[i][0].ToString());
                   ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, theDT.Rows[i][1].ToString());
                   ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                   //ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                   theAffectedRows = (int)PatientDeleteForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
               }

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
