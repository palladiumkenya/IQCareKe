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
    class BContactCare : ProcessBase, IContactCare
    {
        #region "constructor"
        public BContactCare()
        {
        }
        #endregion

        #region Get DropDowns
        public DataSet GetDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_BindDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion
        #region "Get Date for Last Actual Contact
        //public DataSet GetLastActualContact(int Patient_ID)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsObject CateManager = new ClsObject();
        //    ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
        //    return (DataSet)CateManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_CareTracking_ActualContact_Constella", ClsUtility.ObjectEnum.DataSet);
        //}

        #endregion

        #region "Get Date for Last Actual Contact
        public DataSet GetProgramStatus(int Patient_ID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CateManager = new ClsObject();
                ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                return (DataSet)CateManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_ProgramStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region  "Get date for CareEnd
        public DataSet GetCareEndDate(int ptn_pk, string ProgName)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CateEndDate = new ClsObject();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@ProgName ", SqlDbType.VarChar, ProgName.ToString());
                return (DataSet)CateEndDate.ReturnObject(ClsUtility.theParams, "pr_Scheduler_GetCareEndDate_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion


        #region Get Fields
        public DataSet GetFieldsforID(int Patient_ID, int LocationId, int SystemId, int ModuleId, int FeatureId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectContactCarefields_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region Get ContactListforID
        public DataSet GetContactListforID(int Patient_ID, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_GetContactList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion


        #region GetFieldsforEdit

        public DataSet GetFieldsforEdit(int Patient_ID, int LocationId, int CareEndedID, int TrackingID, DataTable theCustomFieldData)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@CareEndedID", SqlDbType.Int, CareEndedID.ToString());
                ClsUtility.AddParameters("@TrackingID", SqlDbType.Int, TrackingID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_EditContactCare_Constella", ClsUtility.ObjectEnum.DataSet);

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
                //    theQuery = theQuery.Replace("#88#", LocationId.ToString());
                //    theQuery = theQuery.Replace("#11#", CareEndedID.ToString());
                //    theQuery = theQuery.Replace("#22#", TrackingID.ToString());
                //    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                //    int theRowsAffected = (Int32)CareManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //}
            }
           
        }

        #endregion

        #region GetNoofCareEnded
        public DataTable GetCareEndedNos(int Patient_ID, DateTime LastcontactDate, int flagcontactdate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, Patient_ID.ToString());
                ClsUtility.AddParameters("@contactDate", SqlDbType.DateTime, LastcontactDate.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flagcontactdate.ToString());
                return (DataTable)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_NoofCareEnded_onDate_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        
        #region GetCareEndedDetails
        public DataTable GetCareDetails(int CEndedId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                ClsUtility.AddParameters("@CEndedID", SqlDbType.Int, CEndedId.ToString());

                return (DataTable)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_CareEnded_Details_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        /*
        #region SaveNewPatientDetail
        public DataSet SaveContactCare(int ptn_pk, int LocationId, int ARTended, DateTime ARTenddate, int ARTendreason, int careended, int exitreason, int dropreason, DateTime dateofdeath, int deathreason, string deathreasondescription, int employeeid, DateTime careendeddate, DateTime DateLastContact, int UserID, int Status, DateTime MissedAppDate, int DataQuality, int LPTFTransfer, int LostFollowreason, string Stop_Lostreason_Other)
        {
            ClsObject CareManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@ARTended", SqlDbType.Int, ARTended.ToString());
                ClsUtility.AddParameters("@ARTenddate", SqlDbType.Int, ARTenddate.ToString());
                ClsUtility.AddParameters("@ARTendreason", SqlDbType.Int, ARTendreason.ToString());
                ClsUtility.AddParameters("@Careended", SqlDbType.Int, careended.ToString());
                ClsUtility.AddParameters("@PatientExitReason", SqlDbType.Int, exitreason.ToString());
                ClsUtility.AddParameters("@DroppedOutReason", SqlDbType.Int, dropreason.ToString());
                ClsUtility.AddParameters("@LostFollowreason", SqlDbType.Int, LostFollowreason.ToString());
                ClsUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, Stop_Lostreason_Other.ToString());
                ClsUtility.AddParameters("@DeathDate", SqlDbType.DateTime, dateofdeath.ToString());
                ClsUtility.AddParameters("@DeathReason", SqlDbType.Int, deathreason.ToString());
                ClsUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, deathreasondescription.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, employeeid.ToString());
                ClsUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, careendeddate.ToString());
                ClsUtility.AddParameters("@DateLastContact", SqlDbType.DateTime, DateLastContact.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@MissedAppDate", SqlDbType.DateTime, MissedAppDate.ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.DateTime, DataQuality.ToString());
                ClsUtility.AddParameters("@LPTFTransfer", SqlDbType.Int, LPTFTransfer.ToString());
                //ClsUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, careendeddate.ToString());
               
              
               // int RowsAffected;
                theDs = (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SaveCareTrackingDetails_Constella", ClsUtility.ObjectEnum.DataSet);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDs;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally 
            {
              CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
        #endregion
         */

        #region UpdatePatientDetail
        /*
        public int UpdateContactCare(int ptn_pk, int LocationId, int ARTended, DateTime ARTenddate, int ARTendreason, int careended, int exitreason, int dropreason, string dropreasonother, DateTime dateofdeath, int deathreason, string deathreasondescription, int employeeid, DateTime careendeddate, DateTime DateLastContact, int UserID, int Status, int TrackingID, int CareEndedID, DateTime MissedAppDate, int DataQuality)
        {
            ClsObject CareManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@ARTended", SqlDbType.Int, ARTended.ToString());
                ClsUtility.AddParameters("@ARTendreason", SqlDbType.Int, ARTendreason.ToString());
                ClsUtility.AddParameters("@careended", SqlDbType.Int, careended.ToString());
                ClsUtility.AddParameters("@ARTenddate", SqlDbType.Int, ARTenddate.ToString());
                ClsUtility.AddParameters("@PatientExitReason", SqlDbType.Int, exitreason.ToString());
                ClsUtility.AddParameters("@DroppedOutReason", SqlDbType.Int, dropreason.ToString());
                ClsUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, dropreasonother.ToString());
                ClsUtility.AddParameters("@DeathDate", SqlDbType.DateTime, dateofdeath.ToString());
                ClsUtility.AddParameters("@DeathReason", SqlDbType.Int, deathreason.ToString());
                ClsUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, deathreasondescription.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, employeeid.ToString());
                ClsUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, careendeddate.ToString());
                ClsUtility.AddParameters("@DateLastContact", SqlDbType.DateTime, DateLastContact.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@TrackingID", SqlDbType.Int, TrackingID.ToString());
                ClsUtility.AddParameters("@CareEndedID", SqlDbType.Int, CareEndedID.ToString());
                ClsUtility.AddParameters("@MissedAppDate", SqlDbType.DateTime, MissedAppDate.ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.DateTime, DataQuality.ToString());
               // ClsUtility.AddParameters("@LostFollowreason", SqlDbType.Int, LostFollowreason.ToString());
               

                //ClsUtility.AddParameters("@CreateDate", SqlDbType.DateTime, CreateDate.ToString());

                int RowsAffected;
                RowsAffected = (Int32)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateCareTrackingDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
        */
        #endregion

        public DataSet CheckModuleTrackingStatus(Int32 thePtn_Pk,Int32 theModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePtn_Pk.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, theModuleId.ToString());
                ClsObject theModCheck = new ClsObject();
                return (DataSet)theModCheck.ReturnObject(ClsUtility.theParams, "Pr_Scheduler_CheckPatientModuleTrackingStatus_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet SaveContactCare(Hashtable ht, int DataQuality, DataTable theCustomFieldData)
        {
            ClsObject CareManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();

                ClsUtility.Init_Hashtable();
                if(ht["PatientID"]!= null)
                    ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, ht["PatientID"].ToString());
                else
                    ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, "");
                if(ht["LocationID"]!=null)
                    ClsUtility.AddParameters("@LocationId", SqlDbType.VarChar, ht["LocationID"].ToString());
                else
                    ClsUtility.AddParameters("@LocationId", SqlDbType.VarChar, "");
                if(ht["theMissedAppDate"]!= null)
                    ClsUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, ht["theMissedAppDate"].ToString());
                else
                    ClsUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, "");
                if(ht["theDateContact"]!=null)
                    ClsUtility.AddParameters("@DateLastContact", SqlDbType.VarChar, ht["theDateContact"].ToString());
                else
                    ClsUtility.AddParameters("@DateLastContact", SqlDbType.VarChar,"");
                if(ht["theARTEnd"]!=null)
                    ClsUtility.AddParameters("@ARTended", SqlDbType.VarChar, ht["theARTEnd"].ToString());
                else
                    ClsUtility.AddParameters("@ARTended", SqlDbType.VarChar, "");
                if(ht["theARTEnddate"]!=null)
                    ClsUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, ht["theARTEnddate"].ToString());
                else
                    ClsUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, "");
                if(ht["ARTendreaon"]!=null)
                    ClsUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, ht["ARTendreaon"].ToString());
                else
                    ClsUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, "");
                if(ht["theCareEnd"]!=null)
                    ClsUtility.AddParameters("@Careended", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    ClsUtility.AddParameters("@Careended", SqlDbType.VarChar, "");
                if(ht["ExitReason"]!=null)
                    ClsUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, ht["ExitReason"].ToString());
                else
                    ClsUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, "");
                if(ht["ddLostFollowreason"]!=null)
                    ClsUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, ht["ddLostFollowreason"].ToString());
                else
                    ClsUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, "");
                if(ht["txtdropoutother"]!=null)
                    ClsUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar, ht["txtdropoutother"].ToString());
                else
                    ClsUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar,"");
                if(ht["lptfreason"]!=null)
                    ClsUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, ht["lptfreason"].ToString());
                else
                    ClsUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, "");
                if(ht["ddDroppedOutReason"]!=null)
                    ClsUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, ht["ddDroppedOutReason"].ToString());
                else
                    ClsUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, "");
                if(ht["ddDeathReason"]!=null)
                    ClsUtility.AddParameters("@DeathReason", SqlDbType.VarChar, ht["ddDeathReason"].ToString());
                else
                    ClsUtility.AddParameters("@DeathReason", SqlDbType.VarChar, "");
                if(ht["txtDeathDate"]!=null)
                    ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, ht["txtDeathDate"].ToString());
                else
                    ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, "");
                if(ht["txtDeathReasonDescription"]!=null)
                    ClsUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, ht["txtDeathReasonDescription"].ToString());
                else
                    ClsUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, "");
                if(ht["PMTCTCareEnded"]!=null)
                    ClsUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, ht["PMTCTCareEnded"].ToString());
                else
                    ClsUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, "");
                if(ht["txtCareEndDate"]!=null)
                    ClsUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, ht["txtCareEndDate"].ToString());
                else
                    ClsUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, "1900-01-01");
                if (ht["theCareEnd"] != null)
                    ClsUtility.AddParameters("@Status", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "");

                ClsUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["ddinterviewer"].ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ht["theModule"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserId"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQuality.ToString());
                DataTable dtp = new DataTable();
                DataSet objDs = new DataSet();
                theDs = (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SaveCareTrackingDetails_Constella", ClsUtility.ObjectEnum.DataSet);
                
                //dtp = objDs.Tables[0];

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

                //String CareEndedID = "";

                //string theSQL = string.Format("Select IDENT_CURRENT('dtl_PatientCareended')");

                //ClsUtility.Init_Hashtable();
                //DataTable DTVisitID = (DataTable)CareManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                //CareEndedID = DTVisitID.Rows[0][0].ToString();
                   
                   
               
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#11#", theDs.Tables[0].Rows[0]["CareendedID"].ToString());
                    theQuery = theQuery.Replace("#22#", theDs.Tables[1].Rows[0]["TrackingID"].ToString());   
                    //theQuery = theQuery.Replace("#11#", CareEndedID.ToString());
                    //theQuery = theQuery.Replace("#22#", TrackingID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["txtCareEndDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)CareManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }


               
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDs;
            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }



        }

        public DataTable  UpdateContactCare(Hashtable ht, int DataQuality, int CareEndedID, int TrackingID, DataTable theCustomFieldData)
        {
            ClsObject CareManager = new ClsObject();
           
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();

                ClsUtility.Init_Hashtable();
                if (ht["PatientID"] != null)
                    ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, ht["PatientID"].ToString());
                else
                    ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, "");
                if (ht["LocationID"] != null)
                    ClsUtility.AddParameters("@LocationId", SqlDbType.VarChar, ht["LocationID"].ToString());
                else
                    ClsUtility.AddParameters("@LocationId", SqlDbType.VarChar, "");
                if (ht["theMissedAppDate"] != null)
                    ClsUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, ht["theMissedAppDate"].ToString());
                else
                    ClsUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, "");
                if (ht["theDateContact"] != null)
                    ClsUtility.AddParameters("@DateLastContact", SqlDbType.VarChar, ht["theDateContact"].ToString());
                else
                    ClsUtility.AddParameters("@DateLastContact", SqlDbType.VarChar, "");
                if (ht["theARTEnd"] != null)
                    ClsUtility.AddParameters("@ARTended", SqlDbType.VarChar, ht["theARTEnd"].ToString());
                else
                    ClsUtility.AddParameters("@ARTended", SqlDbType.VarChar, "");
                if (ht["theARTEnddate"] != null)
                    ClsUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, ht["theARTEnddate"].ToString());
                else
                    ClsUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, "");
                if (ht["ARTendreaon"] != null)
                    ClsUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, ht["ARTendreaon"].ToString());
                else
                    ClsUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, "");
                if (ht["theCareEnd"] != null)
                    ClsUtility.AddParameters("@Careended", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    ClsUtility.AddParameters("@Careended", SqlDbType.VarChar, "");
                if (ht["ExitReason"] != null)
                    ClsUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, ht["ExitReason"].ToString());
                else
                    ClsUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, "");
                if (ht["ddLostFollowreason"] != null)
                    ClsUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, ht["ddLostFollowreason"].ToString());
                else
                    ClsUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, "");
                if (ht["txtdropoutother"] != null)
                    ClsUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar, ht["txtdropoutother"].ToString());
                else
                    ClsUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar, "");
                if (ht["lptfreason"] != null)
                    ClsUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, ht["lptfreason"].ToString());
                else
                    ClsUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, "");
                if (ht["ddDroppedOutReason"] != null)
                    ClsUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, ht["ddDroppedOutReason"].ToString());
                else
                    ClsUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, "");
                if (ht["ddDeathReason"] != null)
                    ClsUtility.AddParameters("@DeathReason", SqlDbType.VarChar, ht["ddDeathReason"].ToString());
                else
                    ClsUtility.AddParameters("@DeathReason", SqlDbType.VarChar, "");
                if (ht["txtDeathDate"] != null)
                    ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, ht["txtDeathDate"].ToString());
                else
                    ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, "");
                if (ht["txtDeathReasonDescription"] != null)
                    ClsUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, ht["txtDeathReasonDescription"].ToString());
                else
                    ClsUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, "");
                if (ht["PMTCTCareEnded"] != null)
                    ClsUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, ht["PMTCTCareEnded"].ToString());
                else
                    ClsUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, "");
                if (ht["txtCareEndDate"] != null)
                    ClsUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, ht["txtCareEndDate"].ToString());
                else
                    ClsUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, "1900-01-01");
                if (ht["theCareEnd"] != null)
                    ClsUtility.AddParameters("@Status", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    ClsUtility.AddParameters("@Status", SqlDbType.VarChar, "");

                ClsUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["ddinterviewer"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserId"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQuality.ToString());
                ClsUtility.AddParameters("@TrackingID", SqlDbType.VarChar, TrackingID.ToString());
                ClsUtility.AddParameters("@CareEndedID", SqlDbType.VarChar, CareEndedID.ToString());
                DataTable theDT;
                theDT = (DataTable)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateCareTrackingDetails_Constella", ClsUtility.ObjectEnum.DataTable);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", theDT.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#11#", CareEndedID.ToString());
                    theQuery = theQuery.Replace("#22#", TrackingID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["txtCareEndDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)CareManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet PatientPrevProgram(int Ptn_Pk)
        {
            ClsObject CareManager = new ClsObject();
          //  int RowsAffected;
            try
            {
                ClsUtility.Init_Hashtable();
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());

                return (DataSet)CareManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_PatientPrevProgram_Constella", ClsUtility.ObjectEnum.DataSet);

             //   DataMgr.CommitTransaction(this.Transaction);
             //   DataMgr.ReleaseConnection(this.Connection);

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}

