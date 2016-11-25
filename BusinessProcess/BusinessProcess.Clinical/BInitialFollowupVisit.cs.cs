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
    /// <summary>
    /// 
    /// </summary>
    public class BInitialFollowupVisit : ProcessBase, IinitialFollowupVisit
    {

        /// <summary>
        /// Gets the initial followup visit data.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet GetInitialFollowupVisitData(int patientID, int locationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetInitialFollowupVisitData", ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Gets the initial followup visit information.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="visitID">The visit identifier.</param>
        /// <returns></returns>
        public DataSet GetInitialFollowupVisitInfo(int patientID, int locationID, int visitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetInitialFollowupVisitInfo", ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Saves the update initial followup visit data.
        /// </summary>
        /// <param name="hashTable">The hash table.</param>
        /// <param name="dataSet">The data set.</param>
        /// <param name="isUpdate">if set to <c>true</c> [is update].</param>
        /// <param name="theCustomDataDT">The custom data dt.</param>
        /// <returns></returns>
        public DataSet SaveUpdateInitialFollowupVisitData(Hashtable hashTable, DataSet dataSet, bool isUpdate, DataTable theCustomDataDT)
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
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                if (hashTable.ContainsKey("ModuleId"))
                {
                    ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, Convert.ToInt32(hashTable["ModuleId"]));
                }
                //Appointment Scheduling
                ClsUtility.AddParameters("@visitDate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@TypeofVisit", SqlDbType.Int, hashTable["TypeofVisit"].ToString());
                ClsUtility.AddParameters("@Scheduled", SqlDbType.Int, hashTable["Scheduled"].ToString());
                ClsUtility.AddParameters("@treatmentSupporterName", SqlDbType.VarChar, hashTable["treatmentSupporterName"].ToString());
                ClsUtility.AddParameters("@treatmentSupporterContact", SqlDbType.VarChar, hashTable["treatmentSupporterContact"].ToString());


                //Clinical Status
                ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["physTemp"].ToString());
                ClsUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                ClsUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                if (hashTable.ContainsKey("muac"))
                {
                    ClsUtility.AddParameters("@Muac", SqlDbType.Decimal, hashTable["muac"].ToString());
                }


                //Pegnancy
                ClsUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
                if (hashTable["pregnant"].ToString() == "89")
                {
                    ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                    ClsUtility.AddParameters("@ANCNo", SqlDbType.Int, hashTable["ANCNo"].ToString());
                    ClsUtility.AddParameters("@ReferredtoPMTCT", SqlDbType.Int, hashTable["ReferredtoPMTCT"].ToString());
                }
                if (hashTable["pregnant"].ToString() == "91")
                {
                    ClsUtility.AddParameters("@DateofInducedAbortion", SqlDbType.DateTime, hashTable["DateofInducedAbortion"].ToString());
                }
                if (hashTable["pregnant"].ToString() == "92")
                {
                    ClsUtility.AddParameters("@DateofMiscarriage", SqlDbType.DateTime, hashTable["DateofMiscarriage"].ToString());
                }

                //Family planning
                ClsUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());
                if (hashTable.ContainsKey("NoFamilyPlanning"))
                {
                    ClsUtility.AddParameters("@NoFamilyPlanning", SqlDbType.Int, hashTable["NoFamilyPlanning"].ToString());
                }


                //TB
                ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                if (hashTable.ContainsKey("TBStartDate"))
                {
                    ClsUtility.AddParameters("@TBRxStart", SqlDbType.DateTime, hashTable["TBStartDate"].ToString());
                }
                if (hashTable.ContainsKey("TBTreatmentNo"))
                {
                    ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                }

                //Nutritional Problems
                ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());

                //WHO Stage
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, hashTable["WHOStage"].ToString());

                //pharmacy
                ClsUtility.AddParameters("@CotrimoxazoleAdhere", SqlDbType.Int, hashTable["CotrimoxazoleAdhere"].ToString());
                ClsUtility.AddParameters("@ARVDrugsAdhere", SqlDbType.Int, hashTable["ARVDrugsAdhere"].ToString());
                ClsUtility.AddParameters("@WhyPooFair", SqlDbType.Int, hashTable["WhyPooFair"].ToString());
                if (hashTable.ContainsKey("reasonARVDrugsPoorFairOther"))
                {
                    ClsUtility.AddParameters("@reasonARVDrugsPoorFairOther", SqlDbType.VarChar, hashTable["reasonARVDrugsPoorFairOther"].ToString());
                }

                //Subsitutions/Interruption
                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, hashTable["TherapyPlan"].ToString());
                ClsUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, hashTable["TherapyReasonCode"].ToString());
                ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, hashTable["TherapyOther"].ToString());
                ClsUtility.AddParameters("@PrescribedARVStartDate", SqlDbType.DateTime, hashTable["PrescribedARVStartDate"].ToString());


                ClsUtility.AddParameters("@numOfDaysHospitalized", SqlDbType.VarChar, hashTable["numOfDaysHospitalized"].ToString());
                ClsUtility.AddParameters("@nutritionalSupport", SqlDbType.Int, hashTable["nutritionalSupport"].ToString());
                ClsUtility.AddParameters("@infantFeedingOption", SqlDbType.Int, hashTable["infantFeedingOption"].ToString());

                ClsUtility.AddParameters("@attendingClinician", SqlDbType.Int, hashTable["attendingClinician"].ToString());
                ClsUtility.AddParameters("@Datenextappointment", SqlDbType.DateTime, hashTable["Datenextappointment"].ToString());



                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                if (!isUpdate)
                {
                    // DataSet tempDataSet;
                    theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveInitialFollowupVisit", ClsUtility.ObjectEnum.DataSet);
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
                        if (hashTable.ContainsKey("TBTreatmentNo"))
                        {
                            ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                        }
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
                        ClsUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                        ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    //Positive Prevention At Risk Population
                    for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, dataSet.Tables[4].Rows[i]["RiskPopulationID"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Positive Prevention At Risk Population Services
                    for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@PopulationServiceID", SqlDbType.Int, dataSet.Tables[5].Rows[i]["PopulationServiceID"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulationServices", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Prevention with positives (PwP)
                    for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@pwpID", SqlDbType.Int, dataSet.Tables[6].Rows[i]["pwpID"].ToString());
                        ClsUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[6].Rows[i]["pwpID_Other"]))) ? "" : dataSet.Tables[6].Rows[i]["pwpID_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                    theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateInitialFollowupVisit", ClsUtility.ObjectEnum.DataSet);


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
                        ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                        if (hashTable.ContainsKey("TBTreatmentNo"))
                        {
                            ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                        }
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
                        ClsUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                        ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Positive Prevention At Risk Population
                    for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, dataSet.Tables[4].Rows[i]["RiskPopulationID"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Positive Prevention At Risk Population Services
                    for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@PopulationServiceID", SqlDbType.Int, dataSet.Tables[5].Rows[i]["PopulationServiceID"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulationServices", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Prevention with positives (PwP)
                    for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@pwpID", SqlDbType.Int, dataSet.Tables[6].Rows[i]["pwpID"].ToString());
                        ClsUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[6].Rows[i]["pwpID_Other"]))) ? "" : dataSet.Tables[6].Rows[i]["pwpID_Other"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                /*  //VY for autopopulate pharmacy form 
                  ClsUtility.Init_Hashtable();
                  ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                  ClsUtility.AddParameters("@autopopulate", SqlDbType.Int, hashTable["autoPopulatePharmacy"].ToString());
                  int rs = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_AutoPopulateFromLast", ClsUtility.ObjectEnum.ExecuteNonQuery);
                     */
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
        /// <summary>
        /// Deletes the initial followup visit form.
        /// </summary>
        /// <param name="FormName">Name of the form.</param>
        /// <param name="OrderNo">The order no.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int DeleteInitialFollowupVisitForm(string FormName, int OrderNo, int PatientId, int UserID)
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
        /// <summary>
        /// Gets the exist initial followup visitbydate.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="VisitdByDate">The visitd by date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet GetExistInitialFollowupVisitbydate(int PatientID, DateTime VisitdByDate, int locationID)
        {

            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
            ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitdByDate.ToString());
            ClsUtility.AddParameters("@location", SqlDbType.Int, locationID.ToString());
            ClsObject VisitManager = new ClsObject();
            return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_InitialFollowupVisit_DateValidate_Constella", ClsUtility.ObjectEnum.DataSet);
        }
        /// <summary>
        /// Gets the z score values.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public DataSet GetZScoreValues(int PatientID, string gender, string height)
        {
            ClsObject ClsObj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
            ClsUtility.AddParameters("@sex", SqlDbType.VarChar, gender.ToString());
            ClsUtility.AddParameters("@height", SqlDbType.VarChar, height.ToString());
            DataSet dsZScore = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_ZScores", ClsUtility.ObjectEnum.DataSet);
            return dsZScore;

        }
    }
}
