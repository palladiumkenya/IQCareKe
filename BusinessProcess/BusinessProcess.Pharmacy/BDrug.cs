using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Pharmacy;
using System;
using System.Data;

namespace BusinessProcess.Pharmacy
{
    public class BDrug : ProcessBase, IDrug
    {
        #region "Constructor"
        public BDrug()
        {
        }
        #endregion

        #region "ART Status Validation"
        public DataTable CheckARTStopStatus(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                return (DataTable)PharmacyManager.ReturnObject(ClsUtility.theParams, "Pr_Pharmacy_GetARTStopStatus_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }


        #endregion

        #region "DrugMasters"

        public DataSet GetPharmacyMasters(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetMasterDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetGenericID_CTC_Detail(int RegimenID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, RegimenID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetGernricRegimenDetails_CTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet Get_TBRegimen_Detail(int RegimenID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, RegimenID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetTBRegimenDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region"GetNon-ARTDate"
        public DataSet GetNonARTDate(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetNonARTDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "EmployeeList"
        public DataTable GetEmployeeDetails()
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataTable)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        #region "Print Prescription"
        public DataSet GetPharmacyPrescriptionDetails(int PharmacyID, int PatientId, int IQCareFlag)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, PharmacyID.ToString());
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@IQCareFlag", SqlDbType.Int, IQCareFlag.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyPrescription_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region "PharmacyList"

        public DataSet GetPharmacyList(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPharmacyList", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetPatientRecordformStatus(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPatientRecordformStatus", ClsUtility.ObjectEnum.DataSet);
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
        public DataSet GetExistPharmacy_CTC_Detail(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetExistPharmacy_CTC_Details", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_AgeValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyFormDespensedbydate(int PatientID, DateTime DispensedByDate)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, @DispensedByDate.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DateValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "FixedDrugStrength"
        public DataTable GetStrengthForFixedDrug(int Drug_pk)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_pk.ToString());
                return (DataTable)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetStrengthForFixedDrug_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        #region "FixedDrugStrength"
        public DataTable GetFrequencyForFixedDrug(int Drug_pk)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_pk.ToString());
                return (DataTable)PharmacyManager.ReturnObject(ClsUtility.theParams, "[pr_Admin_GetFrequencyForFixedDrug_Constella]", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        #region "DrugsFrequency"



        public DataSet FillDropDown()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();

                ClsObject DrugManager = new ClsObject();

                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetFrquencyStrength_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion

        #region "Save Drug Order Detail"

        public int SaveUpdateDrugOrder(int patientID, int LocationID, int PharmacyId, int RegimenLine, string PharmacyNotes, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int flag, int SCMFlag)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    //ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    //theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    //if (theRowAffected == 0)
                    //{
                    //    MsgBuilder theMsg = new MsgBuilder();
                    //    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    //    AppException.Create("#C1", theMsg);
                    //}
                }
                #region "Regimen"

                string theRegimen = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (DrugTable.Rows[i]["GenericId"] == System.DBNull.Value)
                    {
                        DrugTable.Rows[i]["GenericId"] = 0;
                    }
                    
                        if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0 )
                        {
                            DataView theDV = new DataView(Master.Tables[0]);
                            theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                            theRegimen = theRegimen.Trim();
                        }
                        else
                        {
                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                            theRegimen = theRegimen.Trim();
                        }
                    
                
                }

                #endregion

                
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine.ToString());
                ClsUtility.AddParameters("@PharmacyNotes", SqlDbType.Int, PharmacyNotes.ToString());
                ClsUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, flag.ToString());
                theDR = (DataRow)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SaveUpdatePharmacy_Constella", ClsUtility.ObjectEnum.DataRow);

                PharmacyId = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyId == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyId;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, DrugTable.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, DrugTable.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@DrugSchedule", SqlDbType.Int, DrugTable.Rows[i]["DrugSchedule"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                    ClsUtility.AddParameters("@SCMflag", SqlDbType.Int, SCMFlag.ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, DrugTable.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, DrugTable.Rows[i]["TrMonth"].ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyId;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveDrugOrder(int patientID, int LocationID, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData,int PeriodTaken)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"

                string theRegimen = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (DrugTable.Rows[i]["GenericId"] == System.DBNull.Value)
                    {
                        DrugTable.Rows[i]["GenericId"] = 0;
                    }
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(Master.Tables[4]);
                        theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion

                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                ClsUtility.AddParameters("ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                theDR = (DataRow)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePharmacy_Constella", ClsUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, DrugTable.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, DrugTable.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, DrugTable.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, DrugTable.Rows[i]["TrMonth"].ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Save Drug Order CTC Detail"
        public DataSet GetARVStatus(int patientid, DateTime DispensedBy)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.DateTime, DispensedBy.ToString());
                return (DataSet)DrugManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetARVStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveUpdateDrugOrder_CTC(int patientID, int LocationID,int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData,int flag)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);
                    }
                }
                #region "Regimen"
                
                int Prophylaxis = 0;
                string theRegimen = "";
                string theRegimenID = "";
                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {

                        if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                        {
                            DataView theDV = new DataView(Master.Tables[14]);
                            if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                theDV.RowFilter = "RegimenID = " + DrugTable.Rows[i]["RegimenID"].ToString(); ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                                if (theDV.Count > 0)
                                {
                                    theRegimenID = theDV[0]["RegimenID"].ToString();
                                    theRegimen = theDV[0]["RegimenName"].ToString();

                                }
                            }
                        }
                        else
                        {

                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, theRegimenID);
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                ClsUtility.AddParameters("ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                theDR = (DataRow)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SaveUpdatePharmacy_CTC_Constella", ClsUtility.ObjectEnum.DataRow);

                PharmacyId = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyId == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyId;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, DrugTable.Rows[i]["RegimenID"].ToString());

                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Int, DrugTable.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                    {
                        ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    }
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }

                
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyId;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveDrugOrder_CTC(int patientID, int LocationID, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID,  DataTable theCustomFieldData)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"
                int theRowAffected = 0;
                int Prophylaxis = 0;
                string theRegimen = "";
                string theRegimenID = "";
                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {

                        if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                        {
                            DataView theDV = new DataView(Master.Tables[14]);
                            if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                theDV.RowFilter = "RegimenID = " + DrugTable.Rows[i]["RegimenID"].ToString(); ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                                if (theDV.Count > 0)
                                {
                                    theRegimenID = theDV[0]["RegimenID"].ToString();
                                    theRegimen = theDV[0]["RegimenName"].ToString();

                                }
                            }
                        }
                        else
                        {

                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, theRegimenID);
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                ClsUtility.AddParameters("ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());

                theDR = (DataRow)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePharmacy_CTC_Constella", ClsUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, DrugTable.Rows[i]["RegimenID"].ToString());

                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Int, DrugTable.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                    {
                        ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    }
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }

                //string[] mValues = strCustomField.Split(new char[] { '!' });

                //foreach (string str in mValues)
                //{
                //    if (str.ToString() != "")
                //    {
                //        string sqlstr = str.Replace("99999", PharmacyID.ToString());
                //        ClsUtility.Init_Hashtable();
                //        ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr);

                //        theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);

                //    }
                //    if (theRowAffected == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Saving Custom Fields. Try Again..";
                //        AppException.Create("#C1", theMsg);

                //    }

                //}

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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'"+ OrderedByDate.ToString()+"'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region Update ExistDrugDetail

        public int UpdateExistDrug(int patientID, int LocationID, int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;


                /************   Delete Previous Records **********/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #region "Regimen"

                string theRegimen = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(Master.Tables[4]);
                        theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion

                /************  Insert Pharmacy Details ***********/

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }

                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_UpdatePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, DrugTable.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, DrugTable.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, DrugTable.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, DrugTable.Rows[i]["TrMonth"].ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int UpdateExistDrug_CTC(int patientID, int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;


                /************   Delete Previous Records **********/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #region "Regimen"

                string theRegimen = "";
                string theRegimenID = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                        {
                            DataView theDV = new DataView(Master.Tables[14]);
                            if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                theDV.RowFilter = "RegimenID = " + DrugTable.Rows[i]["RegimenID"].ToString(); ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                                if (theDV.Count > 0)
                                {
                                    theRegimenID = theDV[0]["RegimenID"].ToString();
                                    theRegimen = theDV[0]["RegimenName"].ToString();

                                }
                            }
                        }
                        else
                        {
                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion

                /************  Insert Pharmacy Details ***********/

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, theRegimenID);
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());

                theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_UpdatePharmacyDetail_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    ClsUtility.AddParameters("@RegimenID", SqlDbType.Int, DrugTable.Rows[i]["RegimenID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Int, DrugTable.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                    {
                        ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    }
                    theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }
                //string[] mValues = strCustomField.Split(new char[] { '!' });
                //foreach (string str in mValues)
                //{


                //    if (str.ToString() != "")
                //    {
                //        string sqlstr = str.Replace("99999", PharmacyId.ToString());
                //        ClsUtility.Init_Hashtable();
                //        ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr);

                //        theRowAffected = (int)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);

                //    }
                //    if (theRowAffected == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Saving Custom Fields. Try Again..";
                //        AppException.Create("#C1", theMsg);

                //    }
               // }
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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion


        #region "Delete Drug Form"
        public int DeleteDrugForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteDrugForm = new ClsObject();
                DeleteDrugForm.Connection = this.Connection;
                DeleteDrugForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteDrugForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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

        public DataTable ReturnDatatableQuery(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQLTable_Parse", ClsUtility.ObjectEnum.DataTable);
            }
        }

        //#############
        //John Macharia - Start
        //26-Jul-2012
        //
        public DataSet GetPharmacyNotes(int PatientID)
        {
            lock (this)
            {
                ClsObject thePN = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                return (DataSet)thePN.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPharmacyNotes_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //John Macharia - End



        /// <summary>
        /// Finds the name of the drug by.
        /// </summary>
        /// <param name="SearchText">The search text.</param>
        /// <param name="CheckQuantity">if set to <c>true</c> [check quantity].</param>
        /// <param name="ExcludeDrugType">Type of the exclude drug.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataTable FindDrugByName(string SearchText, bool CheckQuantity = false, int? ExcludeDrugType = null)
        {
            lock (this)
            {
                ClsObject theON = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SearchText", SqlDbType.VarChar, SearchText);
                ClsUtility.AddExtendedParameters("@CheckQuantity", SqlDbType.Bit, CheckQuantity);
                if (ExcludeDrugType.HasValue)
                {
                    ClsUtility.AddExtendedParameters("@ExcludeDrugType", SqlDbType.Int, ExcludeDrugType.Value);
                }
                return (DataTable)theON.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_FindDrugByName", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}




