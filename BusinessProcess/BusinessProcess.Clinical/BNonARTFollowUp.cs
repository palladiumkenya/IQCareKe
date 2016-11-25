using System;
using System.Collections.Generic;
using System.Text;
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
    public class BNonARTFollowUp : ProcessBase, INonARTFollowUp
    {
        #region "Constuctor"
        public BNonARTFollowUp()
        {

        }
        #endregion

        DataSet theDSResult;
        public DataSet GetPatientNonARTFollowUp(int PatientID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetNonARTFollowup_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #region "Get Exsist Details"

        public DataSet GetExistVisitNonARTFollowUp(int PatientID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                return (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetExisitNonARTFollowupVisit_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientExsistNonARTFollowUp(int PatientID, int VisitID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                return (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetNonARTFollowUpUpdate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetExistNonARTFollowUpDrugDetails(int PharmacyID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetExistNonARTFollowUpPharmacyDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetExistNonARTFollowUpDetails(int PharmacyID, int VisitID, int PateintID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                ClsUtility.AddParameters("@Visit_pk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PateintID.ToString());
                return (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetExistNonARTFollowUpDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetNonARTBoundaryValues()
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Laboratory_GetLabValues_constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetExistPharmacyDetail(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetExistPharmacyDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion

        public DataSet SaveNonARTFollowUp(int PatientID, int PharmacyID, int LocationID, int VisitID, DataSet theDS, DataTable theDT, Hashtable theHT, DateTime OrderedByDate, DateTime DispensedByDate, Boolean Signature, int EmployeeID, int UserID, Boolean flag, Boolean theHIVAssocDisease, int DataQualityFlag, DataTable theCustomFieldData)
        {
            ClsObject NonARTManager = new ClsObject();
            int theAffectedRows = 0;
          //  int PharmacyId = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                NonARTManager.Connection = this.Connection;
                NonARTManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                //Naveen-Added on 23-Aug-2010
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                //
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                //Naveen-Added on 23-Aug-2010
                ClsUtility.AddParameters("@Visit_pk", SqlDbType.Int, VisitID.ToString());
                //
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, theHT["OrderBy"].ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, theHT["DispensedBy"].ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Bit, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, theHT["VisitType"].ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, theHT["VisitDate"].ToString());
                ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, theHT["physTemp"].ToString());
                ClsUtility.AddParameters("@RR", SqlDbType.Decimal, theHT["physRR"].ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.Decimal, theHT["physHR"].ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, theHT["physBPDiastolic"].ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, theHT["physBPSystolic"].ToString());
                ClsUtility.AddParameters("@Height", SqlDbType.Int, theHT["physHeight"].ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, theHT["physWeight"].ToString());
                ClsUtility.AddParameters("@Pain", SqlDbType.Int, theHT["phyPain"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, theHT["physWHOStage"].ToString());
                ClsUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["physWABStage"].ToString());
                ClsUtility.AddParameters("@ClinicNotes", SqlDbType.VarChar, theHT["ClinicalNotes"].ToString());
                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, theHT["Pregnant"].ToString());
                ClsUtility.AddParameters("@EDDDate", SqlDbType.Int, theHT["EDDDate"].ToString());
                ClsUtility.AddParameters("@Delivered", SqlDbType.Int, theHT["Delivered"].ToString());
                ClsUtility.AddParameters("@DelDate", SqlDbType.Int, theHT["DelDate"].ToString());
             
                ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, theHT["LMP"].ToString());
                ClsUtility.AddParameters("@userId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, theHT["OrderType"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, DataQualityFlag.ToString());
                ClsUtility.AddParameters("@AppExist", SqlDbType.VarChar, theHT["AppExist"].ToString());
                ClsUtility.AddParameters("@VisitIDApp", SqlDbType.VarChar, theHT["VisitIDApp"].ToString());
                ClsUtility.AddParameters("@appdate", SqlDbType.VarChar, theHT["AppDate"].ToString());
                ClsUtility.AddParameters("@appreason", SqlDbType.VarChar, theHT["AppReason"].ToString());
                ClsUtility.AddParameters("@signatureid", SqlDbType.VarChar, theHT["Signatureid"].ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, theHT["Flag"].ToString());
                //ClsUtility.AddParameters("@UpdateMode", SqlDbType.Int, UpdateMode.ToString());
                ////if (flag == false)
                ////{
                ////    theDSResult = (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveNonARTFollowUP_Constella", ClsUtility.ObjectEnum.DataSet);
                ////    PharmacyId = Convert.ToInt32(theDSResult.Tables[0].Rows[0][0].ToString());

                ////}
                ////else if (flag == true)
                ////{

                    string strResult = string.Empty;
                    
                    int theResult = 0;
                    ////ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                    ////ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    theDSResult = (DataSet)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateNonARTFollowUP_Constella", ClsUtility.ObjectEnum.DataSet);
                    strResult = Convert.ToString(theDSResult.Tables[0].Rows[0][0]);
                    VisitID = Convert.ToInt32(theDSResult.Tables[1].Rows[0][1]);
                    if (theDSResult.Tables[0].Rows.Count > 0)
                    {
                        if (strResult == "")
                        {
                            theResult = 0;
                        }
                        else
                        {
                            if (PharmacyID == 0)
                            {
                                theResult = Convert.ToInt32(theDSResult.Tables[0].Rows[0][0].ToString());
                                PharmacyID = Convert.ToInt32(theDSResult.Tables[0].Rows[0][0].ToString());
                            }
                        }
                    }
                ////}
                if (theDSResult.Tables[0].Rows[0][0].ToString() == "")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Patient's Non-ART Follow-Up Details. Try Again..";
                    Exception ex = AppException.Create("#C1", theBL);
                    throw ex;
                }
                //Updating Appointment Status
                #region "Updating Appointment Status"

                if (Convert.ToInt32(theHT["AppExist"].ToString()) == 1)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@Visit_pkAppID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@signatureid", SqlDbType.Int, theHT["OrderBy"].ToString());
                    int RowsAffected = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateIEAppointmentSignature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                #endregion

                #region "Patient's Symptoms"

                //if (flag == true)
                //{
                //    /***************** Delete Previous Symptoms Records *******************/
                //    ClsUtility.Init_Hashtable();
                //    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                //    ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                //    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientSymptoms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    if (theAffectedRows == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Updating Patient's Symptoms Details for Non-ART FollowUp. Try Again..";
                //        Exception ex = AppException.Create("#C1", theMsg);
                //        throw ex;
                //    }

                //}

                if (theDS.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@symptomID", SqlDbType.Int, theDS.Tables[0].Rows[i]["PresentComplaintsID"].ToString());
                        ClsUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //if (flag == false)
                        //{
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //}
                        //else if (flag == true)
                        //{
                        //ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        //theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Symptoms Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }
                #endregion
                #region "Patient TB Symptoms"
                //if (flag == true)
                //{
                //    /***************** Delete Previous Symptoms Records *******************/
                //    ClsUtility.Init_Hashtable();
                //    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                //    ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                //    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientSymptoms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    if (theAffectedRows == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Updating Patient's TBSymptoms Details for Non-ART FollowUp. Try Again..";
                //        Exception ex = AppException.Create("#C1", theMsg);
                //        throw ex;
                //    }

                //}

                if (theDS.Tables[5].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[5].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@symptomID", SqlDbType.Int, theDS.Tables[5].Rows[i]["TBSymptomsID"].ToString());
                        ClsUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


                        //if (flag == false)
                        //{
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //}
                        //else if (flag == true)
                        //{
                        //    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //    ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //}
                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's TB Symptoms Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }
                #endregion


                #region "Patient's HIV Associated Diseases"

                //if (flag == true)
                //{
                //    /******************* Delete Previous HIV Associated Diseases Details *******************/
                //    theAffectedRows = 0;
                //    ClsUtility.Init_Hashtable();
                //    ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                //    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                //    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    if (theAffectedRows == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Updating Patient's HIV Associated Diseases Details for Non-ART FollowUp. Try Again..";
                //        Exception ex = AppException.Create("#C1", theMsg);
                //        throw ex;
                //    }
                //}

                if (theDS.Tables[2].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[2].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS.Tables[2].Rows[i][0].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS.Tables[2].Rows[i][1].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS.Tables[2].Rows[i][2].ToString());
                        ClsUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                  

                        //if (flag == false)
                        //{
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAssoConditionNonARTFollowUp_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                        //else if (flag == true)
                        //{
                        //    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //    ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's HIV Associated Diseases Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }

                    }
                }

                if (theHIVAssocDisease == true)
                {
                    if (theDS.Tables[3].Rows.Count != 0)
                    {
                        for (int i = 0; i < theDS.Tables[3].Rows.Count; i++)
                        {
                            theAffectedRows = 0;
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                            ClsUtility.AddParameters("@Locationid", SqlDbType.Int, LocationID.ToString());
                            ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS.Tables[3].Rows[i][0].ToString());
                            ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS.Tables[3].Rows[i][1].ToString());
                            ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS.Tables[3].Rows[i][2].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                            theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                      

                            //if (flag == false)
                            //{
                            //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVAssoConditionNonARTFollowUp_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            //}
                            //else if (flag == true)
                            //{
                            //    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //    ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                            //    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            //}
                            
                            if (theAffectedRows == 0)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["MessageText"] = "Error in Saving Patient's HIV Associated Diseases Details for Non-ART Follow-Up. Try Again..";
                                Exception ex = AppException.Create("#C1", theMsg);
                                throw ex;
                            }
                        }
                    }
                }
                #endregion

                #region "Assessments"

                if (flag == true)
                {
                    /****************** Delete Previous Assessments *******************/
                    theAffectedRows = 0;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@plannotes", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientAssessment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (theAffectedRows == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Patient's Assessments Details for Non-ART FollowUp. Try Again..";
                        Exception ex = AppException.Create("#C1", theMsg);
                        throw ex;
                    }
                }

                if (theDS.Tables[1].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[1].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@AssessmentID", SqlDbType.Int, theDS.Tables[1].Rows[i]["AssessmentID"].ToString());
                        ClsUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());

                        if (flag == false)
                        {
                            theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveAssessmentFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                        else if (flag == true)
                        {
                            ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ""); //createdate.ToString());
                            //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, theHT["CreateDate"].ToString()); //createdate.ToString());
                            theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateAssessmentFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        }

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Assessments Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }

                    }
                }
                #endregion

                #region "Drugs Details"
                if (flag == true)
                {
                    /***************** Delete Previous Drugs Records *******************/
                    //theAffectedRows = 0;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (theAffectedRows == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Patient's Pharmacy Details for Non-ART FollowUp. Try Again..";
                        Exception ex = AppException.Create("#C1", theMsg);
                        throw ex;
                    }
                }
                if (theDS.Tables[4].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[4].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        int theFinanced = 99999;
                        int theUnit = 99999;
                        Decimal theDose = 99999;
                        ClsUtility.Init_Hashtable();

                        ClsUtility.AddParameters("@GenericID", SqlDbType.Int, theDS.Tables[4].Rows[i]["GenericID"].ToString());
                        ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDS.Tables[4].Rows[i]["DrugID"].ToString());
                        ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDS.Tables[4].Rows[i]["Strength"].ToString());
                        ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDS.Tables[4].Rows[i]["Frequency"].ToString());
                        ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDS.Tables[4].Rows[i]["Duration"].ToString());
                        ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDS.Tables[4].Rows[i]["QtyPrescribed"].ToString());
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDS.Tables[4].Rows[i]["QtyDispensed"].ToString());
                        ClsUtility.AddParameters("@Finance", SqlDbType.Int, theFinanced.ToString());
                        ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theUnit.ToString());
                        ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDose.ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                        if (flag == false)
                        {
                            ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, theDSResult.Tables[0].Rows[0][0].ToString());
                        }
                        else if (flag == true)
                        {
                            ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        }
                        theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveNonARTDrugDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Pharmacy Details for Non-ART FollowUp. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }

                /***************Save Other OI *****************/

                if (theDT.Rows.Count != 0)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        theAffectedRows = 0;

                        ClsUtility.Init_Hashtable();

                        ClsUtility.AddParameters("@GenericID", SqlDbType.Int, theDT.Rows[i]["GenericID"].ToString());
                        ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                        ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["StrengthId"].ToString());
                        ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["FrequencyId"].ToString());
                        ClsUtility.AddParameters("@Duration", SqlDbType.Int, theDT.Rows[i]["Duration"].ToString());
                        ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Int, theDT.Rows[i]["QtyPrescribed"].ToString());
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, theDT.Rows[i]["QtyDispensed"].ToString());
                        ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                        ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["UnitId"].ToString());
                        ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                        if (flag == false)
                        {
                            ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, theDSResult.Tables[0].Rows[0][0].ToString());
                        }
                        else if (flag == true)
                        {
                            ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        }
                        theAffectedRows = (int)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveNonARTDrugDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Pharmacy Details for Non-ART FollowUp. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }

                #endregion


                #region "Custom Fields"
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
                //String VisitIDNonART = "";
                //if (flag == false)
                //{
                //    string theSQL = string.Format("Select IDENT_CURRENT('ord_Visit')");
                //    ClsUtility.Init_Hashtable();
                //    DataTable DTVisitID = (DataTable)NonARTManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                //    VisitIDNonART = DTVisitID.Rows[0][0].ToString();
                //}
                //if (flag == true)
                //{
                //    VisitIDNonART = VisitID.ToString();
                //}
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", PatientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#77#", VisitID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + theHT["VisitDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)NonARTManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////
                #endregion
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDSResult;
            }
            catch(Exception err)
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw err;
                
            }
            finally
            {
                NonARTManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
               
            }
        }


        #region "Delete Non-ART Form"
        public int DeleteNonARTForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteNonARTForm = new ClsObject();
                DeleteNonARTForm.Connection = this.Connection;
                DeleteNonARTForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteNonARTForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
