using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Laboratory;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Laboratory
{
    public class BLabFunctions : ProcessBase, ILabFunctions
    {
        #region "Constructor"
        public BLabFunctions()
        {
        }
        #endregion



        public DataTable FindLabByName(string SearchText, int? IncludeDepartment = null, int? ExcludeDepartment = null)
        {
            lock (this)
            {
                ClsObject theON = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabName", SqlDbType.VarChar, SearchText);
                if (IncludeDepartment.HasValue)
                {
                    ClsUtility.AddExtendedParameters("@IncludeDepartment", SqlDbType.Int, IncludeDepartment.Value);
                }
                if (ExcludeDepartment.HasValue)
                {
                    ClsUtility.AddExtendedParameters("@ExcludeLabDepartment", SqlDbType.Int, ExcludeDepartment.Value);
                }
                return (DataTable)theON.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabTestID", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetBmiValue(int PatientID, int LocationID, int VisitID, int statusHW)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@IsHeightWeight", SqlDbType.Int, statusHW.ToString());
                ClsObject LabManager = new ClsObject();
                return (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Clincial_GetBMIValue", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetEmployeeDetails()
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataTable)LoginManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetLaborderdate(int PatientID, int LocationID, int LabId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@LabTestId", SqlDbType.Int, LabId.ToString());
                ClsObject LabManager = new ClsObject();
                return (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Laboratory_GetLabOrderDate_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetLabValues()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_GetLabValues_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the ordered labs.
        /// </summary>
        /// <param name="LabId">The lab identifier.</param>
        /// <returns></returns>
        public DataSet GetOrderedLabs(int LabId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Laboratory_GetPatientsLabs", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientInfo(string patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.VarChar, patientid.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Laboratory_GetPatientInfo_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetPatientLab(String LabId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_GetLabResults_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientLabOrder(String PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_GetLabOrder_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientLabTestID(String LabId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject OrderMgr = new ClsObject();
                return (DataSet)OrderMgr.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_GetLabTestID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientRecordformStatus(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Laboratory_GetPatientRecordformStatus_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable ReturnLabQuery(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQLTable_Parse", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Saves the dynamic lab results.
        /// </summary>
        /// <param name="labID">The lab identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="ReportedByName">Name of the reported by.</param>
        /// <param name="reportedByDate">The reported by date.</param>
        /// <param name="LabResults">The lab results.</param>
        public void SaveDynamicLabResults(int labID, int userID, int ReportedByName, DateTime reportedByDate, DataTable LabResults)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@labID", SqlDbType.Int, labID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                ClsUtility.AddParameters("@ReportedByName", SqlDbType.Int, ReportedByName.ToString());
                ClsUtility.AddParameters("@ReportedByDate", SqlDbType.Date, reportedByDate.ToString("yyyyMMdd"));
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow row in LabResults.Rows)
                {

                    sbItems.Append("<row>");
                    sbItems.Append("<ParameterID>" + row["ParameterID"].ToString() + "</ParameterID>");
                    sbItems.Append("<TestResults>" + row["TestResult"].ToString() + "</TestResults>");
                    sbItems.Append("<TestResults1>" + row["TestResult1"].ToString() + "</TestResults1>");
                    sbItems.Append("<TestResultId>" + row["TestResultId"].ToString() + "</TestResultId>");
                    sbItems.Append("<Units>" + row["Units"].ToString() + "</Units>");
                    sbItems.Append("</row>");
                }
                sbItems.Append("</root>");
                ClsUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                ClsObject LabManagerTest = new ClsObject();
                LabManagerTest.ReturnObject(ClsUtility.theParams, "dbo.pr_Laboratory_SaveDynamicResults", ClsUtility.ObjectEnum.ExecuteNonQuery);

            }
        }

        /// <summary>
        /// Saves the lab order.
        /// </summary>
        /// <param name="Ptn_pk">The PTN_PK.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="ParameterID">The parameter identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="OrderedByName">Name of the ordered by.</param>
        /// <param name="OrderedByDate">The ordered by date.</param>
        /// <param name="LabID">The lab identifier.</param>
        /// <param name="PreClinicLabDate">The pre clinic lab date.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="IsRadiology">if set to <c>true</c> [is radiology].</param>
        /// <returns></returns>
        public DataSet SaveLabOrder(int Ptn_pk,
            int LocationId, DataTable LabTests, int UserId, int OrderedByName, 
            DateTime OrderedByDate, DateTime PreClinicLabDate, 
            int? LabOrderId = null, int? ModuleId = null, 
            bool IsRadiology = false,
            string ClinicalOrderNotes = "")
        {
            lock (this)
            {
                ClsObject LabManagerTest = new ClsObject();
                try
                {
                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                    foreach (DataRow row in LabTests.Rows)
                    {

                        sbItems.Append("<request>");
                        sbItems.Append("<TestID>" + row["TestID"].ToString() + "</TestID>");
                        sbItems.Append("<TestNotes>" + row["TestNotes"].ToString() + "</TestNotes>");
                        sbItems.Append("<IsGroup>" + row["IsGroup"].ToString() + "</IsGroup>");
                        sbItems.Append("</request>");
                    }

                    DataRow[] groupRows = LabTests.Select("IsGroup = 1");
                    if (groupRows != null && groupRows.Length > 0)
                    {//
                        //DataTable
                    }

                    sbItems.Append("</root>");


                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    LabManagerTest.Connection = this.Connection;
                    LabManagerTest.Transaction = this.Transaction;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());                   
                    ClsUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                    ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                    if (LabOrderId.HasValue)
                        ClsUtility.AddExtendedParameters("@LabID", SqlDbType.Int, LabOrderId.Value);
                    if (PreClinicLabDate.ToString() == "")
                    { }
                    else
                        ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());
                    ClsUtility.AddExtendedParameters("@IsRadiology", SqlDbType.Bit, IsRadiology);

                    if (ModuleId.HasValue)
                        ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, ModuleId.Value);
                    ClsUtility.AddExtendedParameters("@ClinicalOrderNotes", SqlDbType.VarChar, ClinicalOrderNotes);
                    ClsUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());

                    DataSet dsLabTests = (DataSet)LabManagerTest.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella_wip", ClsUtility.ObjectEnum.DataSet);

                    //    LabID = dsLabTests.Tables[0].Rows[0]["LabID"].ToString();
                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return dsLabTests;
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
        }

        /// <summary>
        /// Saves the lab order tests.
        /// </summary>
        /// <param name="Ptn_pk">The PTN_PK.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="LabTestList">The parameter identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="OrderedByName">Name of the ordered by.</param>
        /// <param name="OrderedByDate">The ordered by date.</param>
        /// <param name="LabID">The lab identifier.</param>
        /// <param name="FlagExist">The flag exist.</param>
        /// <param name="PreClinicLabDate">The pre clinic lab date.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="IsRadiology">if set to <c>true</c> [is radiology].</param>
        /// <returns></returns>
        public DataSet SaveLabOrderTests(
            int Ptn_pk,
            int LocationID,
            DataTable LabTestList,
            int UserId,
            int OrderedByName,
            string OrderedByDate,
            string LabID,
            int FlagExist,
            string PreClinicLabDate,
            int? ModuleID = null,
            bool IsRadiology = false,
            string ClinicalOrderNotes= "")
        {
            ClsObject LabManagerTest = new ClsObject();
            try
            {




                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                LabManagerTest.Connection = this.Connection;
                LabManagerTest.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@LabTestId", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                if (LabID != "")
                    ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabID);
                ClsUtility.AddParameters("@FlagExist", SqlDbType.Int, FlagExist.ToString());
                if (PreClinicLabDate.ToString() == "")
                { }
                else
                    ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());
                ClsUtility.AddExtendedParameters("@IsRadiology", SqlDbType.Bit, IsRadiology);

                ClsUtility.AddExtendedParameters("@ClinicalOrderNotes", SqlDbType.VarChar, ClinicalOrderNotes);
                if (ModuleID.HasValue)
                    ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, ModuleID.Value);



                DataSet dsLabTests = (DataSet)LabManagerTest.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella", ClsUtility.ObjectEnum.DataSet);

                LabID = dsLabTests.Tables[0].Rows[0]["LabID"].ToString();

                for (int i = 0; i < LabTestList.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@LabTestId", SqlDbType.Int, LabTestList.Rows[i]["TestID"].ToString());
                    ClsUtility.AddParameters("@RequestNotes", SqlDbType.Int, LabTestList.Rows[i]["TestNotes"].ToString());
                    ClsUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                    ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabID.ToString());
                    ClsUtility.AddParameters("@FlagExist", SqlDbType.Int, FlagExist.ToString());
                    if (PreClinicLabDate.ToString() == "")
                        ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.VarChar, "");
                    else
                        ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());

                    dsLabTests = (DataSet)LabManagerTest.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella", ClsUtility.ObjectEnum.DataSet);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dsLabTests;
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

        public DataSet SaveLabOrderTests(int Ptn_pk, int LocationID, DataTable ParameterID, int UserId, int OrderedByName, string OrderedByDate, string LabID, int FlagExist, string PreClinicLabDate)
        {
            ClsObject LabManagerTest = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                LabManagerTest.Connection = this.Connection;
                LabManagerTest.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@LabTestId", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabID.ToString());
                ClsUtility.AddParameters("@FlagExist", SqlDbType.Int, FlagExist.ToString());

                ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());
                DataSet dsLabTests = (DataSet)LabManagerTest.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella", ClsUtility.ObjectEnum.DataSet);

                for (int i = 0; i < ParameterID.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@LabTestId", SqlDbType.Int, ParameterID.Rows[i][0].ToString());
                    ClsUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                    ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabID.ToString());
                    ClsUtility.AddParameters("@FlagExist", SqlDbType.Int, FlagExist.ToString());
                    ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());
                    dsLabTests = (DataSet)LabManagerTest.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella", ClsUtility.ObjectEnum.DataSet);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dsLabTests;
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

        public DataTable SaveNewLabOrder(Hashtable ht, DataTable dt, string strCustomField, string paperless, DataTable theCustomFieldData)
        {
            try
            {
                int LabID = 0;
                //  int theRowAffected = 0;
                DataTable dtresult;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                ClsUtility.AddParameters("@OrderedByName", SqlDbType.Int, ht["OrderedByName"].ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.VarChar, ht["OrderedByDate"].ToString());
                ClsUtility.AddParameters("@ReportedByName", SqlDbType.Int, ht["ReportedByName"].ToString());
                ClsUtility.AddParameters("@ReportedByDate", SqlDbType.VarChar, ht["ReportedByDate"].ToString());
                ClsUtility.AddParameters("@CheckedByName", SqlDbType.Int, ht["CheckedByName"].ToString());
                ClsUtility.AddParameters("@CheckedByDate", SqlDbType.VarChar, ht["CheckedByDate"].ToString());
                ClsUtility.AddParameters("@PreClinicLabDate", SqlDbType.VarChar, ht["PreClinicLabDate"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                ClsUtility.AddParameters("@Transaction", SqlDbType.Int, ht["Transaction"].ToString());
                ClsUtility.AddParameters("@Orderid", SqlDbType.Int, ht["OrderId"].ToString());
                ClsUtility.AddParameters("@LabPeriod", SqlDbType.Int, ht["LabPeriod"].ToString());
                ClsUtility.AddParameters("@LabNumber", SqlDbType.Int, ht["LabNumber"].ToString());
                dtresult = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "Pr_Laboratory_SaveLabOrder_Constella", ClsUtility.ObjectEnum.DataTable);

                if (dtresult != null && dtresult.Rows.Count > 0)
                {
                    LabID = Convert.ToInt32(dtresult.Rows[0][0].ToString());
                    if (LabID == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Lab Order Records. Try Again..";
                        AppException.Create("#C1", theMsg);
                        return dtresult;

                    }
                }

                if (paperless == "1")
                {
                    if (Convert.ToInt32(ht["Transaction"]) == 2)
                    {
                        string theSQL = string.Format("update dtl_PatientLabResults set TestResultID = NULL, TestResults = NULL, TestResults1 = NULL where LabId = {0}", Convert.ToInt32(ht["OrderId"]));
                        ClsUtility.Init_Hashtable();
                        int Rows = (int)LabManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    DataTable dtres;
                    if (Convert.ToInt32(ht["Transaction"]) == 2)
                    {
                        int DeleteParameterFlag = 0;
                        //string theSQL = string.Format("update dtl_PatientLabResults set TestResultID = NULL where ParameterID={0} and LabID={1}", "100", Convert.ToInt32(ht["OrderId"]));
                        //int Rows = (int)LabManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, ht["OrderId"].ToString());
                            ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                            ClsUtility.AddParameters("@ParameterID", SqlDbType.VarChar, dt.Rows[i]["LabParameterId"].ToString());
                            ClsUtility.AddParameters("@TestResults", SqlDbType.VarChar, dt.Rows[i]["LabResult"].ToString());
                            ClsUtility.AddParameters("@TestResults1", SqlDbType.VarChar, dt.Rows[i]["LabResult1"].ToString());
                            ClsUtility.AddParameters("@TestResultId", SqlDbType.Int, dt.Rows[i]["LabResultId"].ToString());
                            ClsUtility.AddParameters("@Financed", SqlDbType.VarChar, dt.Rows[i]["Financed"].ToString());
                            ClsUtility.AddParameters("@UnitId", SqlDbType.VarChar, dt.Rows[i]["UnitId"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                            if (dt.Rows[i]["LabParameterId"].ToString() == "100")
                            {
                                if (DeleteParameterFlag == 0)
                                {

                                    string theSQL = string.Format("delete from dtl_PatientLabResults where ParameterID = {0} and LabID={1}", "100", Convert.ToInt32(ht["OrderId"]));
                                    int Rows = (int)LabManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                                    dtres = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Lab_UpdateResults_Constella", ClsUtility.ObjectEnum.DataTable);
                                    DeleteParameterFlag = 1;
                                }
                                else
                                {
                                    dtres = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Lab_UpdateResults_Constella", ClsUtility.ObjectEnum.DataTable);
                                }

                            }
                            else
                            {
                                dtres = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Lab_UpdateResults_Constella", ClsUtility.ObjectEnum.DataTable);
                            }

                        }
                    }
                    else
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, dtresult.Rows[0][0].ToString());
                            ClsUtility.AddParameters("@LabTestID", SqlDbType.VarChar, dt.Rows[i]["LabTestId"].ToString());
                            ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                            ClsUtility.AddParameters("@ParameterID", SqlDbType.VarChar, dt.Rows[i]["LabParameterId"].ToString());
                            ClsUtility.AddParameters("@TestResults", SqlDbType.VarChar, dt.Rows[i]["LabResult"].ToString());
                            ClsUtility.AddParameters("@TestResults1", SqlDbType.VarChar, dt.Rows[i]["LabResult1"].ToString());
                            ClsUtility.AddParameters("@TestResultId", SqlDbType.Int, dt.Rows[i]["LabResultId"].ToString());
                            ClsUtility.AddParameters("@Financed", SqlDbType.VarChar, dt.Rows[i]["Financed"].ToString());
                            ClsUtility.AddParameters("@UnitId", SqlDbType.VarChar, dt.Rows[i]["UnitId"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                            dtres = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Lab_AddResults_Constella", ClsUtility.ObjectEnum.DataTable);

                        }


                    }

                }
                else
                {
                    if (Convert.ToInt32(ht["Transaction"]) == 2)
                    {
                        string theSQL = "delete from dtl_PatientLabResults where LabId =" + Convert.ToInt32(ht["OrderId"]);
                        theSQL += "delete from Dtl_PatientBillTransaction where LabId =" + Convert.ToInt32(ht["OrderId"]) + " and ptn_pk=" + ht["PatientID"].ToString();
                        ClsUtility.Init_Hashtable();
                        int Rows = (int)LabManager.ReturnObject(ClsUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    DataTable dtres;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        if (Convert.ToInt32(ht["Transaction"]) == 1)
                        {
                            ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, dtresult.Rows[0][0].ToString());
                        }
                        else
                        {
                            ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, ht["OrderId"].ToString());
                        }
                        ClsUtility.AddParameters("@LabTestID", SqlDbType.VarChar, dt.Rows[i]["LabTestId"].ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                        ClsUtility.AddParameters("@ParameterID", SqlDbType.VarChar, dt.Rows[i]["LabParameterId"].ToString());
                        ClsUtility.AddParameters("@TestResults", SqlDbType.VarChar, dt.Rows[i]["LabResult"].ToString());
                        ClsUtility.AddParameters("@TestResults1", SqlDbType.VarChar, dt.Rows[i]["LabResult1"].ToString());
                        ClsUtility.AddParameters("@TestResultId", SqlDbType.Int, dt.Rows[i]["LabResultId"].ToString());
                        ClsUtility.AddParameters("@Financed", SqlDbType.VarChar, dt.Rows[i]["Financed"].ToString());
                        ClsUtility.AddParameters("@UnitId", SqlDbType.VarChar, dt.Rows[i]["UnitId"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                        dtres = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Lab_AddResults_Constella", ClsUtility.ObjectEnum.DataTable);

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
                    //theQuery = theQuery.Replace("#99#", dtresult.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", dtresult.Rows[0][1].ToString());
                    //theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#33#", dtresult.Rows[0][0].ToString());
                    //theQuery = theQuery.Replace("#33#", ht["OrderID"].ToString());
                    theQuery = theQuery.Replace("#44#", "'" + ht["OrderedByDate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)LabManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtresult;
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
        #region "Delete Lab Form"
        public int DeleteLabForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteLabForm = new ClsObject();
                DeleteLabForm.Connection = this.Connection;
                DeleteLabForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteLabForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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


        //public DataTable GetResultSelectList(int TestParameterId)
        //{
        //    throw new NotImplementedException();
        //}

        //public DataTable GetTestParameterResultUnit(int TestParameterId)
        //{
        //    throw new NotImplementedException();
        //}

        //public DataTable GetTestParameters(int LabTestId)
        //{
        //    throw new NotImplementedException();
        //}

        
    }
}
