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
    public class BHivCareARTEncounter : ProcessBase, IHivCareARTEncounter
    {
        #region "Constructor"
        public BHivCareARTEncounter()
        {
        }
        #endregion

        #region "HIVCare and ART Encounter"
         public DataSet GetExistHIVArtCareEncounterbydate(int PatientID, DateTime VisitdByDate ,int locationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitdByDate.ToString());
                ClsUtility.AddParameters("@location", SqlDbType.Int, locationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_HIVArtCareEncounter_DateValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
       

        public DataSet GetHIVCareARTPatientFormData(int patientID, int locationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetHIVCareARTPatientFormData", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetHIVCareARTPatientVisitInfo(int patientID, int locationID, int visitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetHIVCareARTPatientVisitInfo", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdateHIVCareARTPatientVisit(Hashtable hashTable, DataSet dataSet, bool isUpdate, DataTable theCustomDataDT)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@dataQuality", SqlDbType.Int, hashTable["dataQuality"].ToString());

                //Appointment Scheduling
                ClsUtility.AddParameters("@visitDate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@treatmentSupporterName", SqlDbType.VarChar, hashTable["treatmentSupporterName"].ToString());
                ClsUtility.AddParameters("@treatmentSupporterContact", SqlDbType.VarChar, hashTable["treatmentSupporterContact"].ToString());
                ClsUtility.AddParameters("@followUpDate", SqlDbType.DateTime, hashTable["followUpDate"].ToString());

                //Clinical Status
                ClsUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                ClsUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                ClsUtility.AddParameters("@oedema", SqlDbType.Int, hashTable["oedema"].ToString());

                ClsUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
                ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                ClsUtility.AddParameters("@gestation", SqlDbType.Int, hashTable["gestation"].ToString());
                ClsUtility.AddParameters("@PMTCT", SqlDbType.Int, hashTable["PMTCT"].ToString());
                ClsUtility.AddParameters("@PMTCTANCNumber", SqlDbType.VarChar, hashTable["PMTCTANCNumber"].ToString());
                ClsUtility.AddParameters("@deliveryDate", SqlDbType.DateTime, hashTable["deliveryDate"].ToString());
                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, hashTable["MUAC"].ToString());

                //   ClsUtility.AddParameters("@PostPartum", SqlDbType.Int, hashTable["PostPartum"].ToString());
                //TB Status
                ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                ClsUtility.AddParameters("@TBRxStart", SqlDbType.VarChar, hashTable["TBRxStart"].ToString());
                ClsUtility.AddParameters("@TBRxStop", SqlDbType.VarChar, hashTable["TBRxStop"].ToString());
                ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBRegNumber"].ToString());


                //Subsitutions/Interruption


                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, hashTable["TherapyPlan"].ToString());
                ClsUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, hashTable["TherapyReasonCode"].ToString());
                ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, hashTable["TherapyOther"].ToString());
                ClsUtility.AddParameters("@PrescribedARVStartDate", SqlDbType.DateTime, hashTable["PrescribedARVStartDate"].ToString());

                //ClsUtility.AddParameters("@potentialSideEffectOtherID", SqlDbType.Int, hashTable["potentialSideEffectOtherID"].ToString());
                //ClsUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, hashTable["potentialSideEffectOther"].ToString());

                //ClsUtility.AddParameters("@newOIsProblemOtherID", SqlDbType.Int, hashTable["newOIsProblemOtherID"].ToString());
                //ClsUtility.AddParameters("@newOIsProblemOther", SqlDbType.VarChar, hashTable["newOIsProblemOther"].ToString());

                //ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                ClsUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, hashTable["WHOStage"].ToString());


                ClsUtility.AddParameters("@CPTAdhere", SqlDbType.Int, hashTable["CPTAdhere"].ToString());
                ClsUtility.AddParameters("@ARVDrugsAdhere", SqlDbType.Int, hashTable["ARVDrugsAdhere"].ToString());

                ClsUtility.AddParameters("@reasonARVDrugsPoorFair", SqlDbType.Int, hashTable["reasonARVDrugsPoorFair"].ToString());
                ClsUtility.AddParameters("@reasonARVDrugsPoorFairOther", SqlDbType.VarChar, hashTable["reasonARVDrugsPoorFairOther"].ToString());

                //  ClsUtility.AddParameters("@referredTo", SqlDbType.Int, hashTable["referredTo"].ToString());
                //  ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, hashTable["referredToOther"].ToString());
                ClsUtility.AddParameters("@numOfDaysHospitalized", SqlDbType.VarChar, hashTable["numOfDaysHospitalized"].ToString());
                ClsUtility.AddParameters("@nutritionalSupport", SqlDbType.Int, hashTable["nutritionalSupport"].ToString());
                ClsUtility.AddParameters("@infantFeedingOption", SqlDbType.Int, hashTable["infantFeedingOption"].ToString());
                ClsUtility.AddParameters("@attendingClinician", SqlDbType.Int, hashTable["attendingClinician"].ToString());
                ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                ClsUtility.AddParameters("@Scheduled", SqlDbType.Int, hashTable["Scheduled"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                ClsUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                if (!isUpdate)
                {
                    // DataSet tempDataSet;
                    theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTPatientVisit", ClsUtility.ObjectEnum.DataSet);
                    // visitID = (int)tempDataSet.Tables[0].Rows[0]["visitID"];
                    visitID = (int)theDS.Tables[0].Rows[0]["visitID"];
                    //Family Planning Methods
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                        // ClsUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());
                        ClsUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                        // ClsUtility.AddParameters("@numOfDaysHospitalized", SqlDbType.VarChar, hashTable["numOfDaysHospitalized"].ToString());
                        // ClsUtility.AddParameters("@nutritionalSupport", SqlDbType.Int, hashTable["nutritionalSupport"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareFamilyPlanning", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Potential Side Effects
                    for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@potentialSideEffectID", SqlDbType.Int, dataSet.Tables[1].Rows[i]["potentialSideEffectID"].ToString());
                        ClsUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"]))) ? "" : dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePotentialSideEffect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //New OIs Problems
                    for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@newOIsProblemID", SqlDbType.Int, dataSet.Tables[2].Rows[i]["newOIsProblemID"].ToString());
                        ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                        ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBRegNumber"].ToString());
                        ClsUtility.AddParameters("@newOIsProblemIDOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"]))) ? "" : dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"].ToString());
                        ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareNewOIsProblem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    //Referred To
                    for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        //ClsUtility.AddParameters("@referredTo", SqlDbType.Int, hashTable["referredTo"].ToString());
                        //ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, hashTable["referredToOther"].ToString());
                        ClsUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                        ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }



                    for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                        theQuery = theQuery.Replace("#99#", hashTable["patientID"].ToString());
                        theQuery = theQuery.Replace("#88#", hashTable["locationID"].ToString());
                        theQuery = theQuery.Replace("#77#", visitID.ToString());
                        theQuery = theQuery.Replace("#66#", "'" + hashTable["visitDate"].ToString() + "'");
                        ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                        int RowsAffected = (Int32)VisitManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    
                }
                else
                {
                    visitID = Convert.ToInt32(hashTable["visitID"].ToString());
                    ClsUtility.AddParameters("@visitID", SqlDbType.Int, hashTable["visitID"].ToString());
                    ClsUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                    theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVCareARTPatientVisit", ClsUtility.ObjectEnum.DataSet);
                    //Family Planning Methods
                    //for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    //{
                    //    ClsUtility.Init_Hashtable();
                    //    ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                    //    ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                    //    ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                    //    ClsUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());
                    //    ClsUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                    //    ClsUtility.AddParameters("@numOfDaysHospitalized", SqlDbType.VarChar, hashTable["numOfDaysHospitalized"].ToString());
                    //    ClsUtility.AddParameters("@nutritionalSupport", SqlDbType.Int, hashTable["nutritionalSupport"].ToString());
                    //    ClsUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                    //    int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVCareFamilyPlanning", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                        ClsUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareFamilyPlanning", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }


                    //Potential Side Effects


                    for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@potentialSideEffectID", SqlDbType.Int, dataSet.Tables[1].Rows[i]["potentialSideEffectID"].ToString());
                        ClsUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"]))) ? "" : dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"].ToString());
                        ClsUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePotentialSideEffect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //New OIs Problems
                    for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@newOIsProblemID", SqlDbType.Int, dataSet.Tables[2].Rows[i]["newOIsProblemID"].ToString());
                        ClsUtility.AddParameters("@newOIsProblemIDOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"]))) ? "" : dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"].ToString());
                        ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                        ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBRegNumber"].ToString());
                        ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                        ClsUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareNewOIsProblem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    //Referred To
                    for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        //ClsUtility.AddParameters("@referredTo", SqlDbType.Int, hashTable["referredTo"].ToString());
                        //ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, hashTable["referredToOther"].ToString());
                        ClsUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                        ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                        theQuery = theQuery.Replace("#99#", hashTable["patientID"].ToString());
                        theQuery = theQuery.Replace("#88#", hashTable["locationID"].ToString());
                        theQuery = theQuery.Replace("#77#", visitID.ToString());
                        theQuery = theQuery.Replace("#66#", "'" + hashTable["visitDate"].ToString() + "'");
                        ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                        int RowsAffected = (Int32)VisitManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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
        #region "Delete  Form"
        public int DeleteHIVCareEncounterForms(string FormName, int OrderNo, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteForm = new ClsObject();
                DeleteForm.Connection = this.Connection;
                DeleteForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
