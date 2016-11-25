using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BFollowup : ProcessBase,IFollowup 
    {
        #region "Constructor"
        public BFollowup()
        {
        }
        #endregion
        public DataTable GetPatient_No_Of_IE(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                ClsObject IEManager = new ClsObject();
                return (DataTable)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatient_No_of_ARTFU_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.Int, visitdate.ToString());
                ClsUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
                ClsObject IEManager = new ClsObject();
                return (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatient_No_of_VisitDateConstella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetARTFollowUPVisitDate(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIE_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientFollowUpART(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIE_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLatestCD4ViralLoad(int patientid, DateTime visitdate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, visitdate.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetCD4ViralLoad_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLatestHeight(int patientid, DateTime visitdate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, visitdate.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientHeight_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //Dropdown Lists
        public DataSet GetAllDropDownsART(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsObject FUARTManager = new ClsObject();
                return (DataSet)FUARTManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetFollowupARTDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //Saving and Updating

        public int Save_Update_FollowUP(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDS_ARTFU, int VisitIE, int rdoARVSideEffectsNone, int rdoARVSideEffectsNotDocumented, int rdoOIsAIDsIllnessNone, int rdoOIsAIDsIllnessNotDocumented ,int userID, Boolean Save, Boolean Update, DateTime createDate, int DataQualityFlag, DataTable theCustomFieldData)
        {
            int retval = 0;
            ClsObject FollowupManager = new ClsObject();
            try
            {
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    FollowupManager.Connection = this.Connection;
                    FollowupManager.Transaction = this.Transaction;
            
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.DateTime, ht["Visitdate"].ToString());
                    ClsUtility.AddParameters("@PrevARVsCD4", SqlDbType.Int, ht["txtpriorARVsFU"].ToString());
                    ClsUtility.AddParameters("@PrevARVsCD4Date", SqlDbType.DateTime, ht["txtpriorARVsCD4DateFU"].ToString());
                    ClsUtility.AddParameters("@VisitID_IE", SqlDbType.DateTime, VisitIE.ToString());
                    ClsUtility.AddParameters("@Visit_typeID", SqlDbType.Int, ht["VisittypeIDFU"].ToString());
                    ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, ht["Pregnant"].ToString());
                    ClsUtility.AddParameters("@Delivered", SqlDbType.VarChar, ht["Delivered"].ToString());
                    ClsUtility.AddParameters("@DelDate", SqlDbType.VarChar, ht["DelDate"].ToString());
                    ClsUtility.AddParameters("@EDDDate", SqlDbType.VarChar, ht["EDDDate"].ToString());
                    ClsUtility.AddParameters("@lmp", SqlDbType.DateTime, ht["LMPdate"].ToString());
                    ClsUtility.AddParameters("@MissedLastWeek", SqlDbType.Int, ht["DosesMissedLastWeek"].ToString());
                    ClsUtility.AddParameters("@MissedLastMonth", SqlDbType.Int, ht["DosesMissedLastMonth"].ToString());
                    ClsUtility.AddParameters("@NumDOTPerWeek", SqlDbType.Int, ht["NumDOTPerWeek"].ToString());
                    ClsUtility.AddParameters("@NumHomeVisitsPerWeek", SqlDbType.Int, ht["NumHomeVisitsPerWeek"].ToString());
                    ClsUtility.AddParameters("@SupportGroup", SqlDbType.Bit, ht["SupportGroup"].ToString());
                    ClsUtility.AddParameters("@InterruptedDate", SqlDbType.DateTime, ht["InterruptedDate"].ToString());
                    ClsUtility.AddParameters("@InterruptedNumDays", SqlDbType.Int, ht["InterruptedNumDays"].ToString());
                    ClsUtility.AddParameters("@StoppedDate", SqlDbType.DateTime, ht["stoppedDate"].ToString());
                    ClsUtility.AddParameters("@StoppedNumDays", SqlDbType.Int, ht["stoppedNumDays"].ToString());
                    ClsUtility.AddParameters("@HerbalMeds", SqlDbType.Bit, ht["HerbalMeds"].ToString());
                    ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, ht["physTemp"].ToString());
                    ClsUtility.AddParameters("@RR", SqlDbType.Decimal, ht["physRR"].ToString());
                    ClsUtility.AddParameters("@HR", SqlDbType.Decimal, ht["physHR"].ToString());
                    ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, ht["physBPDiastolic"].ToString());
                    ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, ht["physBPSystolic"].ToString());
                    ClsUtility.AddParameters("@Height", SqlDbType.Int, ht["physHeight"].ToString());
                    ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, ht["physWeight"].ToString());
                    ClsUtility.AddParameters("@Pain", SqlDbType.Int, ht["physExamPain"].ToString());
                    ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, ht["phyWHOstage"].ToString());
                    ClsUtility.AddParameters("@WABStage", SqlDbType.Int, ht["physWABStage"].ToString());
                    ClsUtility.AddParameters("@ARVtherapyPlan", SqlDbType.Int, ht["ARVtherapyplan"].ToString());
                    ClsUtility.AddParameters("@ARTEnddate", SqlDbType.DateTime, ht["ARTEndDate"].ToString());
                    ClsUtility.AddParameters("@ARVTHerapyReasonCode", SqlDbType.Int, ht["ArvTherapyReasonCode"].ToString());
                    ClsUtility.AddParameters("@TherapyOther", SqlDbType.Int, ht["ARVTherapyReasonOther"].ToString());
                    ClsUtility.AddParameters("@AppExist", SqlDbType.VarChar, ht["AppExist"].ToString());
                    ClsUtility.AddParameters("@VisitIDApp", SqlDbType.VarChar, Convert.ToString(ht["VisitIDApp"]));
                    ClsUtility.AddParameters("@appdate", SqlDbType.VarChar, Convert.ToString(ht["appdate"]));
                    ClsUtility.AddParameters("@appreason", SqlDbType.VarChar, Convert.ToString(ht["appreason"]));
                    ClsUtility.AddParameters("@employeeID", SqlDbType.BigInt, ht["Signatureid"].ToString());
                    ClsUtility.AddParameters("@signatureid", SqlDbType.BigInt, ht["Signatureid"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                    ClsUtility.AddParameters("@dataquality", SqlDbType.Int, DataQualityFlag.ToString());
                    ClsUtility.AddParameters("@plannotes", SqlDbType.VarChar, ht["PlanNote"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                    //Ajay Kumar-05-Jan-2010 Begin
                    //Clinical note added
                    ClsUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, ht["ClinicalNotes"].ToString());
                    //Ajay Kumar-05-Jan-2010 End
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, ht["Flag"].ToString());
                    int retvalother = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateFollowUp_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //if (Save == true)
                    //{
                    //    int retvalother = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveFollowUp_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}
                    //else if (Update == true)
                    //{
                        
                    //    int retvalother = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateFollowUP_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);            
                    //}
                
                    /*Appointment Status*/
                   
                    //Updating Most Recent EmployeeID for appointment
                    if (Convert.ToInt32(ht["AppExist"].ToString()) == 1)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Visit_pkAppID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@signatureid", SqlDbType.BigInt, ht["Signatureid"].ToString());
                        int RowsAffected = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateIEAppointmentSignature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
               
                /* Reason Missed */
                    if (Update == true)
                    {
                        ////////Delete Statement
                        //////ClsUtility.Init_Hashtable();
                        //////ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //////ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        //////ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        //////int retvalmissed1 = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteMissedReason_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //Update Statement
                        for (int i = 0; i < theDS_ARTFU.Tables[0].Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            ClsUtility.AddParameters("@MissedReasonID", SqlDbType.Int, theDS_ARTFU.Tables[0].Rows[i]["MissedReasonID"].ToString());
                            ClsUtility.AddParameters("@OtherReason_Desc", SqlDbType.VarChar, theDS_ARTFU.Tables[0].Rows[i][1].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                            int retvalmissed = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMissedReason_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                    else if (Save == true)
                    {
                        for (int i = 0; i < theDS_ARTFU.Tables[0].Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@MissedReasonID", SqlDbType.Int, theDS_ARTFU.Tables[0].Rows[i]["MissedReasonID"].ToString());
                            ClsUtility.AddParameters("@OtherReason_Desc", SqlDbType.VarChar, theDS_ARTFU.Tables[0].Rows[i][1].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalmissed = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveMissedReason_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                    

                    //Save and Update Complaints  
                    if (Save == true)
                    {
                        for (int i = 0; i < theDS_ARTFU.Tables[1].Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[1].Rows[i]["PresentComplaintsID"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalcomplaint = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                    else if (Update == true)
                    {//pr_Clinical_DeleteFollowUP_PatientSymptoms_Constella
                        //Delete Statement
                        ////ClsUtility.Init_Hashtable();
                        ////ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        ////ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ////ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ////int retvalcomplaint1 = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //Update Statement
                        for (int i = 0; i < theDS_ARTFU.Tables[1].Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[1].Rows[i]["PresentComplaintsID"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, createDate.ToString());
                            int retvalcomplaint = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }

                 //ARV Side Effects - Left 
                    if (Save == true)
                    {
                        if (rdoARVSideEffectsNone == 31)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, rdoARVSideEffectsNone.ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalarvleft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoARVSideEffectsNotDocumented == 32)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, rdoARVSideEffectsNotDocumented.ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalarvleft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoARVSideEffectsNone == 0 && rdoARVSideEffectsNotDocumented == 0)
                        {
                            for (int i = 0; i < theDS_ARTFU.Tables[2].Rows.Count; i++)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                                ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[2].Rows[i]["ARVSideEffectsID1"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                                int retvalarvleft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        }
                    }
                    else if (Update == true)
                    {
                            if (rdoARVSideEffectsNone == 31)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, rdoARVSideEffectsNone.ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                            int retvalarvleft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoARVSideEffectsNotDocumented == 32)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, rdoARVSideEffectsNotDocumented.ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                            int retvalarvleft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoARVSideEffectsNone == 0 && rdoARVSideEffectsNotDocumented == 0)
                        {
                            for (int i = 0; i < theDS_ARTFU.Tables[2].Rows.Count; i++)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                                ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[2].Rows[i]["ARVSideEffectsID1"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                                //ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                                int retvalarvleft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        }  
                    }
                    //ARV Side Effects Right
                    if (Save == true)
                    {
                        for (int i = 0; i < theDS_ARTFU.Tables[3].Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[3].Rows[i]["ARVSideEffectsID2"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalarvright = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                    else if (Update == true)
                    {

                        for (int i = 0; i < theDS_ARTFU.Tables[3].Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[3].Rows[i]["ARVSideEffectsID2"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                            int retvalarvright = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                    }


                    //TBScreen
                    if (Save == true)
                    {//pr_Clinical_SaveComplaintsIEFU_Constella
                       
                            for (int i = 0; i < theDS_ARTFU.Tables[4].Rows.Count; i++)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                                ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[4].Rows[i]["TBScreenID"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                                int retvalTBScreen = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        
                    }
                    else if (Update == true)
                    {
                        

                            for (int i = 0; i < theDS_ARTFU.Tables[4].Rows.Count; i++)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                                ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_ARTFU.Tables[4].Rows[i]["TBScreenID"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                                //ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                                int retvalTBScreen = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        

                    }

                //Save OI & AIDS Defining Illness - left
                    if (Save == true)
                    {
                        if (rdoOIsAIDsIllnessNone == 99)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, rdoOIsAIDsIllnessNone.ToString());
                            ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                            ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "Blank");
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalAssocCondLeft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoOIsAIDsIllnessNotDocumented == 98)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, rdoOIsAIDsIllnessNotDocumented.ToString());
                            ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                            ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "Blank");
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            int retvalAssocCondLeft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoOIsAIDsIllnessNone == 0 && rdoOIsAIDsIllnessNotDocumented == 0)
                        {
                            for (int i = 0; i < theDS_ARTFU.Tables[5].Rows.Count; i++)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                                ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_ARTFU.Tables[5].Rows[i]["OI_AIDS_ID1"].ToString());
                                ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                                ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_ARTFU.Tables[5].Rows[i][1].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                                int retvalAssocCondLeft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        }
                    }

                    else if (Update == true)
                    {
                        ////////Delete Statement
                        ////ClsUtility.Init_Hashtable();
                        ////ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        ////ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ////ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ////int retvalAssocCondLeft1 = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientDisease_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (rdoOIsAIDsIllnessNone == 99)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, rdoOIsAIDsIllnessNone.ToString());
                            ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                            ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "Blank");
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, createDate.ToString());
                            int retvalAssocCondLeft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Update_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoOIsAIDsIllnessNotDocumented == 98)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                            ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, rdoOIsAIDsIllnessNotDocumented.ToString());
                            ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                            ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "Blank");
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                            int retvalAssocCondLeft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Update_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }

                        else if (rdoOIsAIDsIllnessNone == 0 && rdoOIsAIDsIllnessNotDocumented == 0)
                        {
                            for (int i = 0; i < theDS_ARTFU.Tables[5].Rows.Count; i++)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                                ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_ARTFU.Tables[5].Rows[i]["OI_AIDS_ID1"].ToString());
                                ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                                ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_ARTFU.Tables[5].Rows[i][1].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                                ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                                int retvalAssocCondLeft = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Update_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        }
                    }

                //Save OI & AIDS Defining Illness - right
                if (Save == true)
                {
                    for (int i = 0; i < theDS_ARTFU.Tables[6].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_ARTFU.Tables[6].Rows[i]["OI_AIDS_ID2"].ToString());
                        ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_ARTFU.Tables[6].Rows[i][1].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                        int retvalAssocCondright = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                else if (Update == true)
                {
                    for (int i = 0; i < theDS_ARTFU.Tables[6].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_ARTFU.Tables[6].Rows[i]["OI_AIDS_ID2"].ToString());
                        ClsUtility.AddParameters("@DateofHIVAssocDisease", SqlDbType.DateTime, ht["Visitdate"].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_ARTFU.Tables[6].Rows[i][1].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                        int retvalAssocCondright = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Update_OI_AID_ILLNESS_FU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Saving Assessment
                if (Save == true)
                {//Save statement
                    for (int i = 0; i < theDS_ARTFU.Tables[7].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@AssessmentID", SqlDbType.Int, theDS_ARTFU.Tables[7].Rows[i]["AssessmentID"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                        int retvalAssessment = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveAssessmentFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                else if (Update == true)
                {
                    //Delete Statement
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@plannotes", SqlDbType.VarChar, ht["PlanNote"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                     
                    int retvalAssessment1 = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientAssessment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   
                    //Update
                    for (int i = 0; i < theDS_ARTFU.Tables[7].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@AssessmentID", SqlDbType.Int, theDS_ARTFU.Tables[7].Rows[i]["AssessmentID"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@createdate", SqlDbType.Int, createDate.ToString());
                        int retvalAssessment = (int)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateAssessmentFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
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
                //Generating VisitID from ART Followup Form
                String VisitIDART = "";
                if (Save == true)
                {
                    string theSQL = string.Format("Select IDENT_CURRENT('ord_Visit')");
                    ClsUtility.Init_Hashtable();
                    DataTable DTVisitID = (DataTable)FollowupManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                    VisitIDART = DTVisitID.Rows[0][0].ToString();
                }
                if (Update == true)
                {
                    VisitIDART = VisitID.ToString();
                }
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#77#", VisitIDART);
                    theQuery = theQuery.Replace("#66#", "'" + ht["Visitdate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)FollowupManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////
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
                    if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
                
                }
           return retval;
        }

        public DataSet GetFollowUpARTupdate(int patientID, int VisitID, int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetFollowUpARTUpdate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetClinicalDate(int patientid, int visittype)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
                ClsObject PatientEnrolManager = new ClsObject();
                return (DataSet)PatientEnrolManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckClinicalDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }


        #region "Delete ART Form"
        public int DeleteARTForms(string FormName, int OrderNo, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteARTForm = new ClsObject();
                DeleteARTForm.Connection = this.Connection;
                DeleteARTForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteARTForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
