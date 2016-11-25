using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using Application.Common;
using Interface.Scheduler;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;

namespace BusinessProcess.Scheduler
{
    class BCareEnded : ProcessBase, ICareEnded
    {
        #region "constructor"
        public BCareEnded()
        {
        }
        #endregion

        #region GetDynamicControl
        public DataSet GetDynamicControl(int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetDynamicControl_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveGetDynamicControlDatat(string sqlquery, string PatientId, string CareEndedDate)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Query", SqlDbType.VarChar, sqlquery.ToString());
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, CareEndedDate.ToString());

                Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveCustomFormData_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return NoRowsEffected;
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

        public DataSet GetSavedFormData(int VisitId,int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@TrackingId", SqlDbType.Int, VisitId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "Pr_CareTracking_GetSavedFormData_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCareEndedDeathReason(int ModuleID)
        {
            lock (this)
            {
                ClsObject CareEnded = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Moduleid", SqlDbType.Int, ModuleID.ToString());
                return (DataSet)CareEnded.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetDeathReason_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion


    }
}
