using System;
using System.Data;
using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Pharmacy;

namespace BusinessProcess.Pharmacy
{
    /// <summary>
    /// 
    /// </summary>
    internal class BPediatric : ProcessBase, IPediatric
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BPediatric"/> class.
        /// </summary>
        public BPediatric()
        {
        }

        #endregion "Constructor"

        #region "Get Pediatric Fields"

        /// <summary>
        /// Gets the pediatric fields.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetPediatricFields(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPediatricDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the exist pharmacy form.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="OrderedByDate">The ordered by date.</param>
        /// <returns></returns>
        public DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                return (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_AgeValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the exist pharmacy form despensedbydate.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DispensedByDate">The dispensed by date.</param>
        /// <returns></returns>
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

        #endregion "Get Pediatric Fields"

        #region "Paediatric List"

        /// <summary>
        /// Gets the exist paediatric details.
        /// </summary>
        /// <param name="PharmacyID">The pharmacy identifier.</param>
        /// <returns></returns>
        public DataSet GetExistPaediatricDetails(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetExistPaediatricDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the patient recordform status.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
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

        #endregion "Paediatric List"

        #region "Save Paediatric Details"

        /// <summary>
        /// Saves the update paediatric detail.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="PharmacyID">The pharmacy identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="RegimenLine">The regimen line.</param>
        /// <param name="PharmacyNotes">The pharmacy notes.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theDrgMst">The DRG MST.</param>
        /// <param name="OrderedBy">The ordered by.</param>
        /// <param name="OrderedByDate">The ordered by date.</param>
        /// <param name="DispensedBy">The dispensed by.</param>
        /// <param name="dispensedByDate">The dispensed by date.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="OrderType">Type of the order.</param>
        /// <param name="VisitType">Type of the visit.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="Height">The height.</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="FDC">The FDC.</param>
        /// <param name="progId">The prog identifier.</param>
        /// <param name="ProviderID">The provider identifier.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <param name="PeriodTaken">The period taken.</param>
        /// <param name="flag">The flag.</param>
        /// <param name="SCMFlag">The SCM flag.</param>
        /// <param name="AppntDate">The appnt date.</param>
        /// <param name="AppntReason">The appnt reason.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <returns></returns>
        public int SaveUpdatePaediatricDetail(int patientId, int pharmacyId, int locationId,
            int regimenLine, string pharmacyNotes, DataTable theDT, DataSet theDrgMst, int OrderedBy,
            DateTime OrderedByDate, int DispensedBy, DateTime dispensedByDate, int signature,
            int employeeId, int orderType, int visitType, int userId, decimal height, decimal weight,
            int FDC, int progId, int providerId, DataTable theCustomFieldData, int periodTaken,
            int flag, int SCMFlag, DateTime AppntDate, int AppntReason, int? moduleId = null)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    //ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    //theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    //if (theRowAffected == 0)
                    //{
                    //    MsgBuilder theMsg = new MsgBuilder();
                    //    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    //    AppException.Create("#C1", theMsg);
                    //}
                }

                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[23]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                        if (theDV.Count > 0)
                        {
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
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                        if (theDV.Count > 0)
                        {
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

                #endregion "Regimen"

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientId.ToString());
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, pharmacyId.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (dispensedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, dispensedByDate.ToString());
                }
                if (flag == 2)
                {
                    if (dispensedByDate.Year.ToString() != "1900")
                    {
                        ClsUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, dispensedByDate.ToString());
                    }
                }
                //ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddExtendedParameters("@OrderType", SqlDbType.Int, orderType);
                ClsUtility.AddExtendedParameters("@Signature", SqlDbType.Int, signature);
                ClsUtility.AddExtendedParameters("@EmployeeID", SqlDbType.Int, employeeId);
                ClsUtility.AddExtendedParameters("@VisitType", SqlDbType.Int, visitType);
                ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userId);
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddExtendedParameters("@RegimenLine", SqlDbType.Int, regimenLine);
                ClsUtility.AddParameters("@PharmacyNotes", SqlDbType.VarChar, pharmacyNotes);
                ClsUtility.AddExtendedParameters("@Height", SqlDbType.Decimal, height);
                ClsUtility.AddExtendedParameters("@Weight", SqlDbType.Decimal, weight);
                ClsUtility.AddExtendedParameters("@FDC", SqlDbType.Int, FDC);
                ClsUtility.AddExtendedParameters("@ProgID", SqlDbType.Int, progId);
                ClsUtility.AddExtendedParameters("@ProviderID", SqlDbType.Int, providerId);
                ClsUtility.AddExtendedParameters("@PeriodTaken", SqlDbType.Int, periodTaken);
                ClsUtility.AddExtendedParameters("@flag", SqlDbType.Int, flag);
                if (AppntDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddExtendedParameters("@AppntDate", SqlDbType.DateTime, AppntDate.ToString());
                }

                ClsUtility.AddExtendedParameters("@AppntReason", SqlDbType.Int, AppntReason);
                if (moduleId.HasValue)
                {
                    ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, moduleId.Value);
                }

                theDR = (DataRow)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SaveUpdatePediatric_Constella", ClsUtility.ObjectEnum.DataRow);

                pharmacyId = Convert.ToInt32(theDR[0].ToString());
                if (pharmacyId == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return pharmacyId;
                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    //ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userId);
                    ClsUtility.AddExtendedParameters("@ptn_pharmacy_pk", SqlDbType.Int, pharmacyId);
                    //ClsUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                    ClsUtility.AddParameters("@SCMflag", SqlDbType.Int, SCMFlag.ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@DrugSchedule", SqlDbType.Int, theDT.Rows[i]["ScheduleId"].ToString());
                    ClsUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDT.Rows[i]["PrintPrescriptionStatus"].ToString());
                    ClsUtility.AddParameters("@PatientInstructions", SqlDbType.Int, theDT.Rows[i]["PatientInstructions"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                for (int i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientId.ToString());
                    theQuery = theQuery.Replace("#88#", locationId.ToString());
                    theQuery = theQuery.Replace("#55#", pharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return pharmacyId;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the paediatric detail.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theDrgMst">The DRG MST.</param>
        /// <param name="OrderedBy">The ordered by.</param>
        /// <param name="OrderedByDate">The ordered by date.</param>
        /// <param name="DispensedBy">The dispensed by.</param>
        /// <param name="DispensedByDate">The dispensed by date.</param>
        /// <param name="Signature">The signature.</param>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="OrderType">Type of the order.</param>
        /// <param name="VisitType">Type of the visit.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Height">The height.</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="FDC">The FDC.</param>
        /// <param name="ProgID">The prog identifier.</param>
        /// <param name="ProviderID">The provider identifier.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <param name="PeriodTaken">The period taken.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public int SavePaediatricDetail(int patientID, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy,
            DateTime DispensedByDate, int Signature, int EmployeeID, int LocationID, int OrderType, int VisitType, int UserID, decimal Height,
            decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int? ModuleId = null)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                        if (theDV.Count > 0)
                        {
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
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                        if (theDV.Count > 0)
                        {
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

                #endregion "Regimen"

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
                //ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                ClsUtility.AddParameters("@FDC", SqlDbType.Int, FDC.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());

                theDR = (DataRow)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePediatric_Constella", ClsUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;
                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["SingleDose"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion "Save Paediatric Details"

        #region "Update Paediatric Details"

        /// <summary>
        /// Updates the paediatric detail.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="PharmacyID">The pharmacy identifier.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theDrgMst">The DRG MST.</param>
        /// <param name="OrderedBy">The ordered by.</param>
        /// <param name="DispensedBy">The dispensed by.</param>
        /// <param name="Signature">The signature.</param>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <param name="OrderType">Type of the order.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="Height">The height.</param>
        /// <param name="Weight">The weight.</param>
        /// <param name="FDC">The FDC.</param>
        /// <param name="ProgID">The prog identifier.</param>
        /// <param name="ProviderID">The provider identifier.</param>
        /// <param name="OrderedByDate">The ordered by date.</param>
        /// <param name="ReportedByDate">The reported by date.</param>
        /// <param name="theCustomFieldData">The custom field data.</param>
        /// <param name="PeriodTaken">The period taken.</param>
        /// <returns></returns>
        public int UpdatePaediatricDetail(int patientID, int LocationID, int PharmacyID, DataTable theDT, DataSet theDrgMst, int OrderedBy, int DispensedBy, int Signature, int EmployeeID, int OrderType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DateTime OrderedByDate, DateTime ReportedByDate, DataTable theCustomFieldData, int PeriodTaken)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;

                /************   Delete Previous Records **********/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                        if (theDV.Count > 0)
                        {
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
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37";
                        if (theDV.Count > 0)
                        {
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

                #endregion "Regimen"

                /************  Insert Paediatric Details ***********/

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                if (ReportedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, ReportedByDate.ToString());
                }

                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());

                theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_UpdatePediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["SingleDose"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion "Update Paediatric Details"

        #region "Delete Pediatric Form"

        /// <summary>
        /// Deletes the pediatric forms.
        /// </summary>
        /// <param name="FormName">Name of the form.</param>
        /// <param name="OrderNo">The order no.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        public int DeletePediatricForms(string FormName, int OrderNo, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeletePediatricForm = new ClsObject();
                DeletePediatricForm.Connection = this.Connection;
                DeletePediatricForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeletePediatricForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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

        #endregion "Delete Pediatric Form"

        /// <summary>
        /// Gets the patient pharmacy orders.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="CutOffDate">The cut off date.</param>
        /// <param name="ShowMostRecent">if set to <c>true</c> [show most recent].</param>
        /// <param name="DrugType">Type of the drug.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientPharmacyOrders(int PatientID, DateTime CutOffDate, bool ShowMostRecent = true, int DrugType = 0)
        {
            ClsObject _clsObject = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, PatientID);
            ClsUtility.AddExtendedParameters("@CutOffDate", SqlDbType.DateTime, CutOffDate);
            ClsUtility.AddExtendedParameters("@ShowMostRecent", SqlDbType.Bit, ShowMostRecent);
            ClsUtility.AddExtendedParameters("@DrugTypeID", SqlDbType.Int, DrugType);
            DataTable dt = _clsObject.ReturnObject(ClsUtility.theParams, "dbo.pr_Pharmacy_GetPatientPharmacyOrderList", ClsUtility.ObjectEnum.DataTable) as DataTable;
            return dt;
        }
    }
}