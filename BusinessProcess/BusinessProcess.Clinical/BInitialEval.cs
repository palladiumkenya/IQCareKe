using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

//using Application.Common;
namespace BusinessProcess.Clinical
{
    public class BInitialEval : ProcessBase, IInitialEval
    {
        #region "Constructor"
        public BInitialEval()
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
                return (DataTable)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatient_No_of_IE_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        //public DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
        //    ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.Int, visitdate.ToString());
        //    ClsUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
        //    ClsObject IEManager = new ClsObject();
        //    return (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatient_No_of_VisitDateConstella", ClsUtility.ObjectEnum.DataSet);
        //}

        public DataSet GetPatientInitialEvaluation(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIE_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject IEManager = new ClsObject();
                return (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetInitialEvaluationDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCurrentDate()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject IEDate = new ClsObject();
                return (DataSet)IEDate.ReturnObject(ClsUtility.theParams, "clinic_getdate", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int Update_DataQuality(int patientid, int visitpk, int dataquality, int locationid)
        {
            
            ClsObject IEManager = new ClsObject();
            int retval = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, visitpk.ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, locationid.ToString());
                retval = (int)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Update_DataQuality_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            
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
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }
        
        #region "20-06-2007 -1 Jayant"
        public DataSet SaveInitialEvaluation(Hashtable ht, int none, int notDocumented, int AssoCondnone, int AssoCondnotDocumented, DataSet theDS_IE, ArrayList AssessmentAL, int VisitIE, string AssessmentDescription1, string AssessmentDescription2, int intflag, int DataQualityFlag, DataTable theCustomFieldData,string ClinicalNotes)
        {
            ClsObject IEManager = new ClsObject();
            DataSet theDS;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                ClsUtility.AddParameters("@Visit_typeID", SqlDbType.Int, ht["VisitTypeID"].ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, ht["VisitPKID"].ToString());
                ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.VarChar, Convert.ToDateTime(ht["visitdate"]).ToString());
                ClsUtility.AddParameters("@HIVDiagnosisdate", SqlDbType.VarChar, ht["HIVDiagnosisdate"].ToString());
                 
                ClsUtility.AddParameters("@diagnosisverified", SqlDbType.Int, ht["diagnosisverified"].ToString());
                ClsUtility.AddParameters("@disclosed", SqlDbType.Int, ht["disclosed"].ToString());
                ClsUtility.AddParameters("@lmp", SqlDbType.VarChar, ht["lmp"].ToString());
                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, ht["Pregnant"].ToString());
                ClsUtility.AddParameters("@Delivered", SqlDbType.VarChar, ht["Delivered"].ToString());
                ClsUtility.AddParameters("@DelDate", SqlDbType.VarChar, ht["DelDate"].ToString());
                ClsUtility.AddParameters("@EDDDate", SqlDbType.VarChar, ht["EDDDate"].ToString());
                ClsUtility.AddParameters("@flagsulfa", SqlDbType.VarChar, ht["flagsulfa"].ToString());
                ClsUtility.AddParameters("@sulfaallergyID", SqlDbType.VarChar, ht["allergy_Sulfa_ID"].ToString());
                ClsUtility.AddParameters("@otherallergyID", SqlDbType.VarChar, ht["allergy_Other_ID"].ToString());
                ClsUtility.AddParameters("@allergynameother", SqlDbType.VarChar, ht["allergynameother"].ToString());
                ClsUtility.AddParameters("@longTermMedsSulfa", SqlDbType.VarChar, ht["longTermMedsSulfa"].ToString());
                ClsUtility.AddParameters("@longTermMedsSulfaDesc", SqlDbType.VarChar, ht["longTermMedsSulfaDesc"].ToString());
                ClsUtility.AddParameters("@longTermTBMed", SqlDbType.VarChar, ht["longTermTBMed"].ToString());
                ClsUtility.AddParameters("@longTermTBMedDesc", SqlDbType.VarChar, ht["longTermTBMedDesc"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther1", SqlDbType.VarChar, ht["longTermMedsOther1"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther1Desc", SqlDbType.VarChar, ht["longTermMedsOther1Desc"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther2", SqlDbType.VarChar, ht["longTermMedsOther2"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther2Desc", SqlDbType.VarChar, ht["longTermMedsOther2Desc"].ToString());

                ClsUtility.AddParameters("@PrevLowestCD4None", SqlDbType.VarChar, ht["PrevLowestCD4None"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4NotDocumented", SqlDbType.VarChar, ht["PrevLowestCD4NotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4", SqlDbType.VarChar, ht["PrevLowestCD4"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4Percent", SqlDbType.VarChar, ht["PrevLowestCD4Percent"].ToString());

                ClsUtility.AddParameters("@PrevARVsCD4None", SqlDbType.VarChar, ht["PrevARVsCD4None"].ToString());                
                ClsUtility.AddParameters("@PrevARVsCD4NotDocumented", SqlDbType.VarChar, ht["PrevARVsCD4NotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4", SqlDbType.VarChar, ht["PrevARVsCD4"].ToString());
                ClsUtility.AddParameters("@VisitID_IE", SqlDbType.VarChar , VisitIE.ToString());
                ClsUtility.AddParameters("@PrevARVsCD4Percent", SqlDbType.VarChar, ht["PrevARVsCD4Percent"].ToString());

                ClsUtility.AddParameters("@PrevMostRecentCD4None", SqlDbType.VarChar, ht["PrevMostRecentCD4None"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4NotDocumented", SqlDbType.VarChar, ht["PrevMostRecentCD4NotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4", SqlDbType.VarChar, ht["PrevMostRecentCD4"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4Percent", SqlDbType.VarChar, ht["PrevMostRecentCD4Percent"].ToString());

                ClsUtility.AddParameters("@PrevMostRecentViralLoadNone", SqlDbType.VarChar, ht["PrevMostRecentViralLoadNone"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentViralLoadNotDocumented", SqlDbType.VarChar, ht["PrevMostRecentViralLoadNotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentViralLoad", SqlDbType.VarChar, ht["PrevMostRecentViralLoad"].ToString());

                ClsUtility.AddParameters("@PrevARVExposureNone", SqlDbType.VarChar, ht["PrevARVExposureNone"].ToString());
                ClsUtility.AddParameters("@PrevARVExposureNotDocumented", SqlDbType.VarChar, ht["PrevARVExposureNotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevARVExposure", SqlDbType.VarChar, ht["PrevARVExposure"].ToString());
                ClsUtility.AddParameters("@CurrentART", SqlDbType.VarChar, ht["CurrentART"].ToString());
                ClsUtility.AddParameters("@PrevSingleDoseNVP", SqlDbType.VarChar, ht["PrevSingleDoseNVP"].ToString());

                ClsUtility.AddParameters("@PrevARVRegimen", SqlDbType.VarChar, ht["PrevARVRegimen"].ToString());                
                ClsUtility.AddParameters("@PrevARVRegimen1Name", SqlDbType.VarChar, ht["PrevARVRegimen1Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen1Months", SqlDbType.VarChar, ht["PrevARVRegimen1Months"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen2Name", SqlDbType.VarChar, ht["PrevARVRegimen2Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen2Months", SqlDbType.VarChar, ht["PrevARVRegimen2Months"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen3Name", SqlDbType.VarChar, ht["PrevARVRegimen3Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen3Months", SqlDbType.VarChar, ht["PrevARVRegimen3Months"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen4Name", SqlDbType.VarChar, ht["PrevARVRegimen4Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen4Months", SqlDbType.VarChar, ht["PrevARVRegimen4Months"].ToString());
                
                ClsUtility.AddParameters("@Temp", SqlDbType.VarChar, ht["Temp"].ToString());
                ClsUtility.AddParameters("@RR", SqlDbType.VarChar, ht["RR"].ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.VarChar, ht["HR"].ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.VarChar, ht["BPDiastolic"].ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.VarChar, ht["BPSystolic"].ToString());
                ClsUtility.AddParameters("@Height", SqlDbType.VarChar, ht["Height"].ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.VarChar, ht["Weight"].ToString());
                ClsUtility.AddParameters("@Pain", SqlDbType.VarChar, ht["Pain"].ToString());
                ClsUtility.AddParameters("@WABStage", SqlDbType.VarChar, ht["WABStage"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.VarChar, ht["WHOStage"].ToString());
                ClsUtility.AddParameters("@ARVtherapyPlan", SqlDbType.VarChar, ht["ARVtherapyPlan"].ToString());
                ClsUtility.AddParameters("@ARVTherapyReasonCode", SqlDbType.VarChar, ht["ARVTherapyReasonCode"].ToString());
                ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, ht["ARVTherapyReasonOther"].ToString());
                ClsUtility.AddParameters("@signatureid", SqlDbType.VarChar, ht["Signatureid"].ToString());
                ClsUtility.AddParameters("@userID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQualityFlag.ToString());
                
                ClsUtility.AddParameters("@PrevSingleDoseNVPDate1", SqlDbType.VarChar, ht["txtprevSingleDoseNVPDate1"].ToString());
                ClsUtility.AddParameters("@PrevSingleDoseNVPDate2", SqlDbType.VarChar, ht["txtprevSingleDoseNVPDate2"].ToString());
                ClsUtility.AddParameters("@currentARTStartDate", SqlDbType.VarChar, ht["currentARTStartDate"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentViralLoadDate", SqlDbType.VarChar, ht["PrevMostRecentViralLoadDate"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4Date", SqlDbType.VarChar, ht["PrevARVsCD4Date"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4Date", SqlDbType.VarChar, ht["PrevLowestCD4Date"].ToString());
                ClsUtility.AddParameters("@longTermTBStartDate", SqlDbType.VarChar, ht["longTermTBStartDate"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4Date", SqlDbType.VarChar, ht["PrevMostRecentCD4Date"].ToString());
                ClsUtility.AddParameters("@AppExist", SqlDbType.VarChar, ht["AppExist"].ToString());
                ClsUtility.AddParameters("@VisitIDApp", SqlDbType.VarChar, Convert.ToString(ht["VisitIDApp"]));
                ClsUtility.AddParameters("@appdate", SqlDbType.VarChar, Convert.ToString(ht["appdate"]));
                ClsUtility.AddParameters("@appreason", SqlDbType.VarChar, Convert.ToString(ht["appreason"]));
                ClsUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, ClinicalNotes);
                ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, ht["Flag"].ToString());
                theDS=(DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateIE_Constella", ClsUtility.ObjectEnum.DataSet);
              
                if (Convert.ToInt32(ht["AppExist"].ToString()) == 1)
                {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@Visit_pkAppID", SqlDbType.Int, ht["VisitIDApp"].ToString());
                        ClsUtility.AddParameters("@signatureid", SqlDbType.BigInt, ht["Signatureid"].ToString());
                        int RowsAffected = (int)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateIEAppointmentSignature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                /**Disclose To**/
                for (int i = 0; i < theDS_IE.Tables[0].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@disclosureid", SqlDbType.Int, theDS_IE.Tables[0].Rows[i]["DisclosureID"].ToString());
                    ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, theDS_IE.Tables[0].Rows[i]["DisclosureOther"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    int retvaldisclose = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateDiscloseIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
     
                }
                /** Presenting Complaints **/
                for (int i = 0; i < theDS_IE.Tables[1].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_IE.Tables[1].Rows[i]["PresentComplaintsID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalcomplaint = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                /****TBScreening*****/
                for (int i = 0; i < theDS_IE.Tables[5].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_IE.Tables[5].Rows[i]["TBScreeningID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalcomplaint = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                /* MediHistoryManager*/
                //None
                if (none == 95)
                {
                    Boolean DiseasePresent = false;
                    String DiseaseYear = "1900";
                    String SpDisease = "None";
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, none.ToString());
                    ClsUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, DiseaseYear);
                    ClsUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, SpDisease);
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalMedHistory = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                //Not Documented
                else if (notDocumented == 94)
                {
                    Boolean DiseasePresent = false;
                    String DiseaseYear = "1900";
                    String SpDisease = "Notdocumented";
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int,  notDocumented.ToString());
                    ClsUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, DiseaseYear);
                    ClsUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, SpDisease);
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalMedHistory = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                else if (none == 0 && notDocumented == 0)
                {
                    for (int i = 0; i < theDS_IE.Tables[2].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, theDS_IE.Tables[2].Rows[i]["MedHistoryID"].ToString());
                        ClsUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[2].Rows[i]["MediHisDiseasePresent"].ToString());
                        ClsUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[2].Rows[i]["YearDiseasePresent"].ToString());
                        ClsUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, theDS_IE.Tables[2].Rows[i]["SpecifyDiseasePresent"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalMedHistory = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /* Associate Condition Left */
                //Associated Assocond - None
                if(AssoCondnone == 97)
                {
                    Boolean DiseasePresent = false;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, AssoCondnone.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalleft = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                //Associated Assocond - Not Documented
                if(AssoCondnotDocumented == 96)
                {

                    Boolean DiseasePresent = false;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, AssoCondnotDocumented.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalleft = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                //HIV Associated Conditions
                if (AssoCondnone == 0 && AssoCondnotDocumented == 0)
                {
                    //Left Side Items.
                    for (int i = 0; i < theDS_IE.Tables[3].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_IE.Tables[3].Rows[i]["chkHIVAssoCondID1"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[3].Rows[i]["HIVAssoDiseasePresent1"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[3].Rows[i]["HIVAssocCondYear1"].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalleft = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Right Side Items
                    for (int i = 0; i < theDS_IE.Tables[4].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_IE.Tables[4].Rows[i]["chkHIVAssoCondid2"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[4].Rows[i]["HIVAssoDiseasePresent2"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[4].Rows[i]["HIVAssocCondYear2"].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_IE.Tables[4].Rows[i]["HIVAssoDiseaseDesc"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalright = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /* Saving Assessment Details */
                 for (int i = 0; i < AssessmentAL.Count; i++)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@AssessmentID", SqlDbType.Int, AssessmentAL[i].ToString());
                    ClsUtility.AddParameters("@Description1", SqlDbType.VarChar, AssessmentDescription1.ToString());
                    ClsUtility.AddParameters("@Description2", SqlDbType.VarChar, AssessmentDescription2.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalAssessnent = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateAssessment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                //Generating VisitID from IE Form
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["patientid"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["locationid"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["visitdate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            
            }
            return theDS;
        }
        #endregion
        //public DataSet GetInitialEvaluationVisitDate(int patientid)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
        //    ClsObject UserManager = new ClsObject();
        //    return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIEVisitDate_Constella", ClsUtility.ObjectEnum.DataSet);
        //}

        public DataSet GetInitialEvaluationUpdate(int visitpk, int patientid, int locationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, visitpk.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIEUpdate_Constella", ClsUtility.ObjectEnum.DataSet);
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
        public DataSet GetARTStatus(int patientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsObject PatientARTStatus = new ClsObject();
                return (DataSet)PatientARTStatus.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetARTStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetPregnantStatus(int patientID, string VisitDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitDate.ToString());
                ClsObject PatientARTStatus = new ClsObject();
                return (DataSet)PatientARTStatus.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPregnantStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetAppointment(int patientID, int locationID, DateTime AppDate, int AppReason)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@AppDate", SqlDbType.DateTime, AppDate.ToString());
                ClsUtility.AddParameters("@AppReason", SqlDbType.Int, AppReason.ToString());
                ClsObject PatientAppStatus = new ClsObject();
                return (DataSet)PatientAppStatus.ReturnObject(ClsUtility.theParams, "pr_clinical_Appointment_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
      } 
}
