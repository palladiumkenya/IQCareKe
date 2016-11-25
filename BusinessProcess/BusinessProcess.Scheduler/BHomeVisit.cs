using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Application.Common;
using Interface.Scheduler;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;


namespace BusinessProcess.Scheduler
{
    class BHomeVisit : ProcessBase, IHomeVisit
    {
        #region constructor
        public BHomeVisit()
        {
        }
        #endregion
       
        #region "Get Fields For Grid"
        public DataSet GetFieldsforGrid(int Patient_ID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject HomeVisitManager = new ClsObject();
                ClsUtility.AddParameters("Patientid", SqlDbType.Int, Patient_ID.ToString());
                return (DataSet)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_GetHomeVisitList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Get Fields For Add"
        public DataSet GetFieldsforAdd(int Patient_ID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject HomeVisitManager = new ClsObject();
                ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectHomeVisitfields_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Get Fields For Edit"

        public DataSet GetFieldsforEdit(int Patient_ID, int HomeVisitID, DataTable theCustomFieldData)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject HomeVisitManager = new ClsObject();
                ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                ClsUtility.AddParameters("@HomeVisitID", SqlDbType.Int, HomeVisitID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectEditHomeVisitfields_Constella", ClsUtility.ObjectEnum.DataSet);

                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                //for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                //{
                //    ClsUtility.Init_Hashtable();
                //    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                //    theQuery = theQuery.Replace("#99#", Patient_ID.ToString());
                //    theQuery = theQuery.Replace("#00#", HomeVisitID.ToString());
                //    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                //    int RowsAffected = (Int32)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //}
            }

        }
        public DataSet GetEmployees(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject Employees = new ClsObject();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)Employees.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region SaveNewPatientDetail
        public DataSet SaveHomeVisit(int LocationID, int ptn_pk, string PatientCHW, string PatientAlternateCHW, int hvPerWeek1, int hvPerWeek2, int hvPerWeek3, int hvPerWeek4, int VisitsPerWeek, int Duration, DateTime StartDate, int UserId, int HomeVisitID, int Flag, int DataQualityFlag,  DataTable theCustomFieldData)
        {
           
            ClsObject HomeVisitManager = new ClsObject();
            DataSet theDT = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                HomeVisitManager.Connection = this.Connection;
                HomeVisitManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@PatientCHW", SqlDbType.VarChar, PatientCHW);
                ClsUtility.AddParameters("@PatientAlternateCHW", SqlDbType.VarChar, PatientAlternateCHW);
                ClsUtility.AddParameters("@hvPerWeek1", SqlDbType.Int, hvPerWeek1.ToString());
                ClsUtility.AddParameters("@hvPerWeek2", SqlDbType.Int, hvPerWeek2.ToString());
                ClsUtility.AddParameters("@hvPerWeek3", SqlDbType.Int, hvPerWeek3.ToString());
                ClsUtility.AddParameters("@hvPerWeek4", SqlDbType.Int, hvPerWeek4.ToString());
                ClsUtility.AddParameters("@VisitsPerWeek", SqlDbType.Int, VisitsPerWeek.ToString());
                ClsUtility.AddParameters("@Duration", SqlDbType.Int, Duration.ToString());
                ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@HomeVisitID", SqlDbType.Int, HomeVisitID.ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, DataQualityFlag.ToString());
              
                //////if (Flag == 0)
                //////{
                //////    theDT = (DataSet)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SaveHomeVisit_Constella", ClsUtility.ObjectEnum.DataSet);
                //////}
                //////else if (Flag == 1)
                //////{
                //////    theDT = (DataSet)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateHomeVisit_Constella", ClsUtility.ObjectEnum.DataSet);
                    
                //////}
                theDT = (DataSet)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateHomeVisit_Constella", ClsUtility.ObjectEnum.DataSet);
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

               
                String VisitIDHome = "";

                VisitIDHome = theDT.Tables[1].Rows[0][0].ToString();

                ////string theSQL = string.Format("Select IDENT_CURRENT('dtl_PatientHomeVisit')");
                ////ClsUtility.Init_Hashtable();
                ////DataTable DTVisitID = (DataTable)HomeVisitManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                ////VisitIDHome = DTVisitID.Rows[0][0].ToString();
               
               

                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ptn_pk.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#00#", VisitIDHome.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + StartDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)HomeVisitManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                DataMgr.CommitTransaction(this.Transaction);
                
                 DataMgr.ReleaseConnection(this.Connection);
                 return theDT;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                HomeVisitManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Delete Home Visit Form"
        public int DeleteHomeVisitForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteHomeVisitForm = new ClsObject();
                DeleteHomeVisitForm.Connection = this.Connection;
                DeleteHomeVisitForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteHomeVisitForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                

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
        #endregion

    }
}
