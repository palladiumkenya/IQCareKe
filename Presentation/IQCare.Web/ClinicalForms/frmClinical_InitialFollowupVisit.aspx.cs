using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;
using IQCare.Web.UILogic;

namespace IQCare.Web.Clinical
{
    /// <summary>
    ///
    /// </summary>
    public partial class InitialFollowupVisit : BasePage
    {
        /// <summary>
        /// The initial followup visit
        /// </summary>
        private IinitialFollowupVisit _InitialFollowupVisit;

        /// <summary>
        /// The age
        /// </summary>
        private double age;

        /// <summary>
        /// The age in month
        /// </summary>
        private int ageInMonth;

        /// <summary>
        /// The age in year
        /// </summary>
        private int ageInYear;

        /// <summary>
        /// The arl
        /// </summary>
        private ArrayList arl = null;

        /// <summary>
        /// The art regimenin month
        /// </summary>
        private int ArtRegimeninMonth = 0;

        /// <summary>
        /// The art startin month
        /// </summary>
        private int ArtStartinMonth = 0;

        /// <summary>
        /// The authentication
        /// </summary>
        private AuthenticationManager Authentication;

        /// <summary>
        /// The data set for saving
        /// </summary>
        private DataSet dataSetForSaving;

        /// <summary>
        /// The ds bind control
        /// </summary>
        private DataSet DsBindControl;

        /// <summary>
        /// The ds bind data
        /// </summary>
        private DataSet DsBindData;

        /// <summary>
        /// The dt bind control
        /// </summary>
        private DataTable DtBindControl;

        /// <summary>
        /// The dt checked ids
        /// </summary>
        private DataTable DTCheckedIds;

        /// <summary>
        /// The dv bind control
        /// </summary>
        private DataView DvBindControl;

        /// <summary>
        /// The gender
        /// </summary>
        private string gender;

        /// <summary>
        /// The hash table
        /// </summary>
        private Hashtable hashTable;

        /// <summary>
        /// The icount
        /// </summary>
        private int icount;

        /// <summary>
        /// The is update
        /// </summary>
        private bool isUpdate;

        /// <summary>
        /// The location identifier
        /// </summary>
        private int locationID;

        /// <summary>
        /// The pat identifier
        /// </summary>
        private int PatID;

        /// <summary>
        /// The patient identifier
        /// </summary>
        private int patientID;

        /// <summary>
        /// The sb parameter
        /// </summary>
        private StringBuilder sbParameter = null;

        /// <summary>
        /// The sb values
        /// </summary>
        private StringBuilder sbValues;

        /// <summary>
        /// The strmultiselect
        /// </summary>
        private string strmultiselect;

        /// <summary>
        /// The table name
        /// </summary>
        private String TableName = null;

        /// <summary>
        /// The visit identifier
        /// </summary>
        private int visitID;

        /// <summary>
        /// Calculates the z scores.
        /// </summary>
        /// <param name="textHeight">Height of the text.</param>
        /// <param name="textWeight">The text weight.</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static List<ListItem> CalculateZScores(string textHeight, string textWeight)
        {
            List<ListItem> ZScoreList = new List<ListItem>();
            double L = 0, M = 0, S = 0, WeightAgeZ = 0, weight = 0, AgeYearUpperLimit = 15;
            DataSet ZScoreDS = new DataSet();
            IinitialFollowupVisit _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.
                CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
            string ZScoreLimit = ConfigurationManager.AppSettings.Get("ZScoreAgeLimitYear");
            if (ZScoreLimit != null)
                AgeYearUpperLimit = Double.Parse(ZScoreLimit);

            if (Convert.ToDouble(HttpContext.Current.Session["patientageinyearmonth"].ToString()) < AgeYearUpperLimit)
            {
                ZScoreDS = _InitialFollowupVisit.GetZScoreValues(
                    Convert.ToInt32(HttpContext.Current.Session["PatientId"]),
                    Convert.ToString(HttpContext.Current.Session["PatientSex"]),
                    textHeight);

                #region WeightForAge

                if (ZScoreDS.Tables[0].Rows.Count > 0)
                {
                    L = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
                    M = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
                    S = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
                    if (textWeight.Trim() != "")
                        weight = Convert.ToDouble(textWeight.Trim());
                    else weight = 0;
                    //Weight for age calculation
                    if (L != 0)
                        WeightAgeZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
                    else
                        WeightAgeZ = (Math.Log(weight / M)) / S;
                }

                #endregion WeightForAge
            }

            return ZScoreList;
        }

        /// <summary>
        /// Authentications this instance.
        /// </summary>
        public void auth()
        {
            Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(ApplicationAccess.InitialFollowupVisits, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (Authentication.HasFunctionRight(ApplicationAccess.InitialFollowupVisits, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnLabratory.Enabled = btnpharmacy.Enabled = buttonICF.Enabled = btnSave.Enabled = false;
                btnDataQualityCheck.Enabled = false;
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.InitialFollowupVisits, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.InitialFollowupVisits, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Text = "Delete";
                    btnLabratory.Enabled = btnpharmacy.Enabled = buttonICF.Enabled = btnSave.Enabled = false;
                    btnDataQualityCheck.Visible = false;
                }
            }

            if (Session["CEndedStatus"] != null)
            {
                if (((DataTable)Session["CEndedStatus"]).Rows.Count > 0)
                {
                    if (((DataTable)Session["CEndedStatus"]).Rows[0]["CareEnded"].ToString() == "1")
                    {
                        btnLabratory.Enabled = btnpharmacy.Enabled = buttonICF.Enabled = btnSave.Enabled = false;
                        btnDataQualityCheck.Enabled = false;
                    }
                }
            }
            if (age <= 14)
            {
                divAdultPharmacy.Visible = true;
            }
            else
            {
                divAdultPharmacy.Visible = true;
            }
            if (Session["Paperless"].ToString() == "1")
            {
                divLabOrderTest.Visible = true;
                divLaboratory.Visible = false;
            }
            else
            {
                divLabOrderTest.Visible = false;
                divLaboratory.Visible = true;
            }

            if (age < 15)
            {
                lblNutritionalProblems.Style.Remove("visibility");
                ddlNutritionalProblems.Style.Remove("visibility");
            }
            if (gender == "Male")
                tdPregnant.Style.Add("display", "none");

            if (gender == "Female" && age < 9)
            {
                tdPregnant.Style.Add("display", "none");
            }
            if (gender == "Female" && age >= 9)
            {
                tdInfantFeedingPractice.Style.Remove("display");
            }
            if (gender == "Female" && age >= 9)
            {
                ListItem item = ddlNutritionalSupport.Items.FindByText("TF =Therapeutic Feeding");
                ddlNutritionalSupport.Items.Remove(item);
            }
            else if (age < 2)
            {
                ListItem item = ddlNutritionalSupport.Items.FindByText("IFC =Infant Feeding Counselling");
                ddlNutritionalSupport.Items.Remove(item);
                ListItem item1 = ddlNutritionalSupport.Items.FindByText("FS =Food Support");
                ddlNutritionalSupport.Items.Remove(item1);
            }
            else
            {
                ListItem item = ddlNutritionalSupport.Items.FindByText("IFC =Infant Feeding Counselling");
                ddlNutritionalSupport.Items.Remove(item);
                ListItem item1 = ddlNutritionalSupport.Items.FindByText("TF =Therapeutic Feeding");
                ddlNutritionalSupport.Items.Remove(item1);
            }

            if (age <= 12)
            {
                tdFamilyPlanning.Style.Add("display", "none");
            }
        }

        /// <summary>
        /// Binds the control.
        /// </summary>
        /// <param name="ds">The ds.</param>
        public void BindControl(DataSet ds)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions bindFunctions = new BindFunctions();

            DtBindControl = ds.Tables[0];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["ART"] != DBNull.Value)
                {
                    ArtStartinMonth = Convert.ToDateTime(DtBindControl.Rows[0]["ART"]).Year * 12 + Convert.ToDateTime(DtBindControl.Rows[0]["ART"]).Month;
                }
                else
                {
                    ArtStartinMonth = 0;
                }
                if (DtBindControl.Rows[0]["REGIMEN"] != DBNull.Value)
                {
                    ArtRegimeninMonth = Convert.ToDateTime(DtBindControl.Rows[0]["REGIMEN"]).Year * 12 + Convert.ToDateTime(DtBindControl.Rows[0]["REGIMEN"]).Month;
                }
                else
                {
                    ArtRegimeninMonth = 0;
                }
            }

            txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "')");
            txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "')");

            if ((Convert.ToInt32(ViewState["VisitID"]) > 1) && (!IsPostBack))
            {
                string ArtinfoScript = "<script language = 'javascript' defer ='defer' id = 'BRuleArtInfo'>\n";
                ArtinfoScript += "HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "'); \n";
                ArtinfoScript += "HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "'); \n";
                ArtinfoScript += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "BRuleArtInfo", ArtinfoScript);
            }

            //Visit Type
            DvBindControl = new DataView(ds.Tables[1]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlvisittype, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            // Pregnancy status
            DvBindControl = new DataView(ds.Tables[2]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlpregnancy, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Family planning status
            DvBindControl = new DataView(ds.Tables[3]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlFamilyPanningStatus, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Family planning method
            DvBindControl = new DataView(ds.Tables[4]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(PnlFamilyPlanningMethod, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Reason Not on Family Planning
            DvBindControl = new DataView(ds.Tables[5]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlnotfamilyplanning, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //TB Status
            DvBindControl = new DataView(ds.Tables[6]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlTBStatus, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Potential Side Effects
            DvBindControl = new DataView(ds.Tables[7]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(PnlPotentialSideEffect, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //New OIs, Other Problems
            DvBindControl = new DataView(ds.Tables[8]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(PnlNewOIsProblemsOther, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //Nutritional Problems
            DvBindControl = new DataView(ds.Tables[9]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlNutritionalProblems, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //WHO Stage
            DvBindControl = new DataView(ds.Tables[10]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlWHOStage, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //Cotrimoxazole Adhere
            DvBindControl = new DataView(ds.Tables[11]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlCotrimoxazoleAdhere, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //ARV Drugs Adhere
            DvBindControl = new DataView(ds.Tables[12]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlarvdrugadhere, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Why Poor/Fair
            DvBindControl = new DataView(ds.Tables[13]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlwhypoorfair, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Subsituations/Interruption
            DvBindControl = new DataView(ds.Tables[14]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlsubsituationInterruption, DtBindControl, "Name", "ID");
                if (!this.IsPostBack)
                {
                    ListItem item = ddlsubsituationInterruption.Items.FindByText("To be filled in by the clinician");
                    if (item != null) item.Selected = true;
                }
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Referred To
            DvBindControl = new DataView(ds.Tables[15]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(PnlReferredTo, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Nutritional Support
            DvBindControl = new DataView(ds.Tables[16]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlNutritionalSupport, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Infant Feeding Practice
            DvBindControl = new DataView(ds.Tables[17]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlInfantFeedingPractice, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //At Risk Population
            DvBindControl = new DataView(ds.Tables[18]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(pnlriskpopulation, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //At Risk Population Services
            DvBindControl = new DataView(ds.Tables[19]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(pnlriskpopulationservice, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Prevention with positives (PwP)
            DvBindControl = new DataView(ds.Tables[20]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.CreateBlueCheckedList(pnlprewithpositive, DtBindControl, "", "");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Attending Clinician
            DvBindControl = new DataView(ds.Tables[21]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                DataColumn dataColumnFullName = new DataColumn("Name");
                dataColumnFullName.DataType = System.Type.GetType("System.String");
                DtBindControl.Columns.Add(dataColumnFullName);

                for (int i = 0; i < DtBindControl.Rows.Count; i++)
                    DtBindControl.Rows[i]["Name"] = DtBindControl.Rows[i]["FirstName"].ToString() + " " + DtBindControl.Rows[i]["LastName"].ToString();

                bindFunctions.BindCombo(ddlattendingclinician, DtBindControl, "Name", "EmployeeID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Therapy Change Codes
            DvBindControl = new DataView(ds.Tables[22]);
            DvBindControl.RowFilter = "CodeID='6'";
            DvBindControl.Sort = "ID ASC";
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlArvTherapyChangeCode, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }
            //Therapy Stop Codes
            DvBindControl = new DataView(ds.Tables[22]);
            DvBindControl.RowFilter = "CodeID='5'";
            DvBindControl.Sort = "ID ASC";
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                bindFunctions.BindCombo(ddlArvTherapyStopCode, DtBindControl, "Name", "ID");
                DvBindControl.Dispose();
                DtBindControl.Clear();
            }

            //Height
            DvBindControl = new DataView(ds.Tables[23]);
            if (DvBindControl.Table != null)
            {
                DtBindControl = (DataTable)iQCareUtils.CreateTableFromDataView(DvBindControl);
                if (DtBindControl.Rows.Count > 0 && ageInYear >= 18)
                    txtPhysHeight.Text = DtBindControl.Rows[0]["height"].ToString();
            }
        }

        /// <summary>
        /// Calculates the z score.
        /// </summary>
        public void CalculateZScore()
        {
            double L = 0,
                M = 0,
                S = 0,
                WeightAgeZ = 0,
                WeightHeightZ = 0,
                BMIz = 0,
                weight = 0,
                bmi = 0,
                AgeYearUpperLimit = 15;
            DataSet ZScoreDS = new DataSet();
            if (_InitialFollowupVisit == null)
                _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");

            string ZScoreLimit = ConfigurationManager.AppSettings.Get("ZScoreAgeLimitYear");
            if (ZScoreLimit != null)
                AgeYearUpperLimit = Double.Parse(ZScoreLimit);

            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) < AgeYearUpperLimit)
            {
                ZScoreDS = _InitialFollowupVisit.GetZScoreValues(Convert.ToInt32(Session["PatientId"]), Convert.ToString(Session["PatientSex"]), txtPhysHeight.Text);

                #region WeightForAge

                if (ZScoreDS.Tables[0].Rows.Count > 0)
                {
                    L = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
                    M = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
                    S = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
                    if (txtPhysWeight.Text.Trim() != "")
                        weight = Convert.ToDouble(txtPhysWeight.Text.Trim());
                    else weight = 0;
                    //Weight for age calculation
                    if (L != 0)
                        WeightAgeZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
                    else
                        WeightAgeZ = (Math.Log(weight / M)) / S;
                    if (WeightAgeZ >= 4)
                    {
                        lblWA.Text = "4";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ >= 3 && WeightAgeZ < 4)
                    {
                        lblWA.Text = "3";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ >= 2 && WeightAgeZ < 3)
                    {
                        lblWA.Text = "2";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ >= 1 && WeightAgeZ < 2)
                    {
                        lblWA.Text = "1";
                        lblWAClassification.Text = " Overweight";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ > -1 && WeightAgeZ < 1)
                    {
                        lblWA.Text = "0";
                        lblWAClassification.Text = " Normal";
                        lblWA.ForeColor = Color.Green;
                        lblWAClassification.ForeColor = Color.Green;
                    }
                    else if (WeightAgeZ <= -1 && WeightAgeZ > -2)
                    {
                        lblWA.Text = "-1";
                        lblWAClassification.Text = " Mild";
                        lblWA.ForeColor = Color.Orange;
                        lblWAClassification.ForeColor = Color.Orange;
                    }
                    else if (WeightAgeZ <= -2 && WeightAgeZ > -3)
                    {
                        lblWA.Text = "-2";
                        lblWAClassification.Text = " Moderate";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ <= -3 && WeightAgeZ > -4)
                    {
                        lblWA.Text = "-3";
                        lblWAClassification.Text = " Severe";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                    else if (WeightAgeZ <= -4)
                    {
                        lblWA.Text = "-4";
                        lblWAClassification.Text = " Severe";
                        lblWA.ForeColor = Color.Red;
                        lblWAClassification.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblWAClassification.Text = "Out of range";
                }

                #endregion WeightForAge

                #region WeightForHeight

                if (Convert.ToDouble(txtPhysHeight.Text) <= 120 && Convert.ToDouble(txtPhysHeight.Text) >= 45)
                {
                    try
                    {
                        if (ZScoreDS.Tables[1].Rows.Count > 0)
                        {
                            L = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["L"].ToString());
                            M = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["M"].ToString());
                            S = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["S"].ToString());
                            if (txtPhysWeight.Text != "")
                                weight = Convert.ToDouble(txtPhysWeight.Text);
                            else
                                weight = 0;
                            if (L != 0)
                                WeightHeightZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
                            else
                                WeightHeightZ = (Math.Log(weight / M)) / S;
                            if (WeightHeightZ >= 4)
                            {
                                lblWH.Text = "4";
                                lblWHClassification.Text = " Overweight";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                            else if (WeightHeightZ >= 3 && WeightHeightZ < 4)
                            {
                                lblWH.Text = "3";
                                lblWHClassification.Text = " Overweight";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                            else if (WeightHeightZ >= 2 && WeightHeightZ < 3)
                            {
                                lblWH.Text = "2";
                                lblWHClassification.Text = " Overweight";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                            else if (WeightHeightZ >= 1 && WeightHeightZ < 2)
                            {
                                lblWH.Text = "1";
                                lblWHClassification.Text = " Overweight";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                            else if (WeightHeightZ > -1 && WeightHeightZ < 1)
                            {
                                lblWH.Text = "0";
                                lblWHClassification.Text = " Normal";
                                lblWH.ForeColor = Color.Green;
                                lblWHClassification.ForeColor = Color.Green;
                            }
                            else if (WeightHeightZ <= -1 && WeightHeightZ > -2)
                            {
                                lblWH.Text = "-1";
                                lblWHClassification.Text = " Mild";
                                lblWH.ForeColor = Color.Orange;
                                lblWHClassification.ForeColor = Color.Orange;
                            }
                            else if (WeightHeightZ <= -2 && WeightHeightZ > -3)
                            {
                                lblWH.Text = "-2";
                                lblWHClassification.Text = " Moderate";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                            else if (WeightHeightZ <= -3 && WeightHeightZ > -4)
                            {
                                lblWH.Text = "-3";
                                lblWHClassification.Text = " Severe";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                            else if (WeightHeightZ <= -4)
                            {
                                lblWH.Text = "-4";
                                lblWHClassification.Text = " Severe";
                                lblWH.ForeColor = Color.Red;
                                lblWHClassification.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lblWH.Text = "Out of range";
                        }
                    }
                    catch 
                    {
                    }
                }
                else
                {
                    lblWH.Text = "";
                    lblWHClassification.Text = "";
                }

                #endregion WeightForHeight

                #region BMIz(Z-Score Calculation)

                if (ZScoreDS.Tables[2].Rows.Count > 0)
                {
                    L = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["L"].ToString());
                    M = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["M"].ToString());
                    S = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["S"].ToString());
                    if (txtPhysHeight.Text != "" && txtPhysWeight.Text != "")
                        bmi = Convert.ToDouble(txtPhysWeight.Text) / ((Convert.ToDouble(txtPhysHeight.Text) / 100) * (Convert.ToDouble(txtPhysHeight.Text) / 100));
                    else
                        bmi = 0;

                    if (L != 0)
                        BMIz = ((Math.Pow((bmi / M), L)) - 1) / (S * L);
                    else
                        BMIz = (Math.Log(bmi / M)) / S;

                    lblBMIz.Text = string.Format("{0:f2}", BMIz);

                    if (BMIz >= 4)
                    {
                        lblBMIz.Text = "4";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz >= 3 && BMIz < 4)
                    {
                        lblBMIz.Text = "3";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz >= 2 && BMIz < 3)
                    {
                        lblBMIz.Text = "2";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz >= 1 && BMIz < 2)
                    {
                        lblBMIz.Text = "1";
                        lblBMIzClassification.Text = " Overweight";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz > -1 && BMIz < 1)
                    {
                        lblBMIz.Text = "0";
                        lblBMIzClassification.Text = " Normal";
                        lblBMIz.ForeColor = Color.Green;
                        lblBMIzClassification.ForeColor = Color.Green;
                    }
                    else if (BMIz <= -1 && BMIz > -2)
                    {
                        lblBMIz.Text = "-1";
                        lblBMIzClassification.Text = " Mild";
                        lblBMIz.ForeColor = Color.Orange;
                        lblBMIzClassification.ForeColor = Color.Orange;
                    }
                    else if (BMIz <= -2 && BMIz > -3)
                    {
                        lblBMIz.Text = "-2";
                        lblBMIzClassification.Text = " Moderate";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz <= -3 && BMIz > -4)
                    {
                        lblBMIz.Text = "-3";
                        lblBMIzClassification.Text = " Severe";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                    else if (BMIz <= -4)
                    {
                        lblBMIz.Text = "-4";
                        lblBMIzClassification.Text = " Severe";
                        lblBMIz.ForeColor = Color.Red;
                        lblBMIzClassification.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblBMIz.Text = "";
                    lblBMIzClassification.Text = "";
                }

                #endregion BMIz(Z-Score Calculation)
            }
            else
            {
                lblWA.Text = "";
                lblWAClassification.Text = "";
                lblWH.Text = "";
                lblWHClassification.Text = "";
                lblBMIz.Text = "";
                lblBMIzClassification.Text = "";
            }

            ZScoreDS.Dispose();
        }

        /// <summary>
        /// Sets the control data.
        /// </summary>
        /// <param name="ds">The ds.</param>
        public void SetControlData(DataSet ds)
        {
            string script;
            //VisitDate & Attending Clinican & dataQuality
            DtBindControl = ds.Tables[0];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["visitDate"] != DBNull.Value)
                {
                    txtVisitDate.Text = Convert.ToDateTime(DtBindControl.Rows[0]["visitDate"].ToString()).ToString(Session["AppDateFormat"].ToString());
                    string ArtinfoScript = "<script language = 'javascript' defer ='defer' id = 'BRuleArtInfo'>\n";
                    ArtinfoScript += "HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "'); \n";
                    ArtinfoScript += "HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "'); \n";
                    ArtinfoScript += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "BRuleArtInfo", ArtinfoScript);
                }
                if (DtBindControl.Rows[0]["createDate"] != DBNull.Value)
                    ViewState["createDate"] = DtBindControl.Rows[0]["createDate"].ToString();
                if (DtBindControl.Rows[0]["dataQuality"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(DtBindControl.Rows[0]["dataQuality"]) == 1)
                        btnDataQualityCheck.CssClass = "greenbutton";
                }
                if (DtBindControl.Rows[0]["typeofvisit"] != DBNull.Value)
                {
                    ddlvisittype.SelectedValue = DtBindControl.Rows[0]["typeofvisit"].ToString();
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'typeofvisit'>\n";
                    script += "fnvisittype();\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "typeofvisit", script);
                }
            }
            //TreatmentSupporter Name & Contact
            DtBindControl = ds.Tables[1];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["treatmentSupporterName"] != DBNull.Value)
                    txtTreatmentSupporterName.Text = DtBindControl.Rows[0]["treatmentSupporterName"].ToString();
                if (DtBindControl.Rows[0]["treatmentSupporterContact"] != DBNull.Value)
                    txtTreatmentSupporterContact.Text = DtBindControl.Rows[0]["treatmentSupporterContact"].ToString();

                if (DtBindControl.Rows[0]["TBRegNumber"] != DBNull.Value)
                    txtTBtreatmentNumber.Value = DtBindControl.Rows[0]["TBRegNumber"].ToString();

                if (DtBindControl.Rows[0]["nutritionalProblem"] != DBNull.Value)
                    ddlNutritionalProblems.SelectedValue = DtBindControl.Rows[0]["nutritionalProblem"].ToString();

                if (DtBindControl.Rows[0]["attendingClinician"] != DBNull.Value)
                    ddlattendingclinician.SelectedValue = DtBindControl.Rows[0]["attendingClinician"].ToString();

                if (DtBindControl.Rows[0]["Scheduled"] != DBNull.Value)
                    chkifschedule.Checked = DtBindControl.Rows[0]["Scheduled"].ToString() == "1" ? true : false;
            }
            //date of next appointment
            DtBindControl = ds.Tables[2];
            if (DtBindControl.Rows.Count != 0)
                if (DtBindControl.Rows[0]["Dateofnextappointment"] != DBNull.Value)
                    txtdatenextappointment.Value = Convert.ToDateTime(DtBindControl.Rows[0]["Dateofnextappointment"].ToString()).ToString(Session["AppDateFormat"].ToString());

            //Height Weight
            DtBindControl = ds.Tables[3];
            if (DtBindControl.Rows.Count != 0)
            {
                double theWeight = 0;
                double theHeight = 0;
                if (DtBindControl.Rows[0]["height"] != DBNull.Value)
                {
                    txtPhysHeight.Text = DtBindControl.Rows[0]["height"].ToString();
                    theHeight = Convert.ToDouble(DtBindControl.Rows[0]["height"].ToString());
                }
                if (DtBindControl.Rows[0]["weight"] != DBNull.Value)
                {
                    txtPhysWeight.Text = DtBindControl.Rows[0]["weight"].ToString();
                    theWeight = Convert.ToDouble(DtBindControl.Rows[0]["weight"].ToString());
                }
                if (DtBindControl.Rows[0]["Temp"] != DBNull.Value)
                    txtphysTemp.Text = DtBindControl.Rows[0]["Temp"].ToString();
                if (DtBindControl.Rows[0]["BPSystolic"] != DBNull.Value)
                    txtBPSystolic.Text = DtBindControl.Rows[0]["BPSystolic"].ToString();
                if (DtBindControl.Rows[0]["BPDiastolic"] != DBNull.Value)
                    txtBPDiastolic.Text = DtBindControl.Rows[0]["BPDiastolic"].ToString();

                double theBMI = theWeight / (theHeight / 100 * theHeight / 100);
                txtBMI.Text = Convert.ToString(Math.Round(theBMI, 2));

                if (DtBindControl.Rows[0]["Muac"] != DBNull.Value)
                {
                    textMuac.Text = DtBindControl.Rows[0]["Muac"].ToString();
                }
            }

            //	Pregnancy & EDD & DateOfDelivery & PMTCT
            DtBindControl = ds.Tables[4];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["pregnant"] != DBNull.Value)
                {
                    ddlpregnancy.SelectedValue = DtBindControl.Rows[0]["pregnant"].ToString();
                    if (Convert.ToInt32(DtBindControl.Rows[0]["pregnant"].ToString()) == 89)
                    {
                        if (DtBindControl.Rows[0]["EDD"] != System.DBNull.Value)
                        {
                            this.txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", DtBindControl.Rows[0]["EDD"]);
                        }
                        if (Convert.ToInt32(DtBindControl.Rows[0]["ReferredtoPMTCT"].ToString()) == 1)
                        {
                            this.chkrefpmtct.Checked = true;
                        }
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                        script += "fnchange();\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                    }
                    if (Convert.ToInt32(DtBindControl.Rows[0]["Pregnant"].ToString()) == 91)
                    {
                        txtdateinducedabortion.Value = String.Format("{0:dd-MMM-yyyy}", DtBindControl.Rows[0]["DateofInducedAbortion"]);
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                        script += "show('Abortion');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                    }
                    if (Convert.ToInt32(DtBindControl.Rows[0]["Pregnant"].ToString()) == 92)
                    {
                        txtdatemiscarriage.Value = String.Format("{0:dd-MMM-yyyy}", DtBindControl.Rows[0]["DateofMiscarriage"]);
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                        script += "show('miscarriage');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                    }
                }
            }
            //PMTCTANCNumber
            DtBindControl = ds.Tables[6];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Columns.Contains("PMTCTANCNumber"))
                    if (DtBindControl.Rows[0]["PMTCTANCNumber"] != DBNull.Value)
                        txtANCNumber.Text = DtBindControl.Rows[0]["PMTCTANCNumber"].ToString();
            }

            //Family Planning & NumOfDaysHospitalized & NutritionalSupport
            DtBindControl = ds.Tables[7];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["familyPlanningStatus"] != DBNull.Value)
                    ddlFamilyPanningStatus.SelectedValue = DtBindControl.Rows[0]["familyPlanningStatus"].ToString();
                if (DtBindControl.Rows[0]["numOfDaysHospitalized"] != DBNull.Value)
                    txtNumOfDaysHospitalized.Text = DtBindControl.Rows[0]["numOfDaysHospitalized"].ToString();

                if (DtBindControl.Rows[0]["nutritionalSupport"] != DBNull.Value)
                    ddlNutritionalSupport.SelectedValue = DtBindControl.Rows[0]["nutritionalSupport"].ToString();

                if (DtBindControl.Rows[0]["NoFamilyPlanning"] != DBNull.Value)
                    ddlnotfamilyplanning.SelectedValue = DtBindControl.Rows[0]["NoFamilyPlanning"].ToString();

                if (ddlFamilyPanningStatus.SelectedItem.Text == "Currently on Family Planning" || ddlFamilyPanningStatus.SelectedItem.Text == "Wants Family Planning")
                {
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_0'>\n";
                    script += "show('divFamilyPlanningMethod');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "FamilyPlanning_0", script);
                    DtBindControl = ds.Tables[17];
                    if (DtBindControl.Rows.Count > 0)
                    {
                        FillCheckBoxListData(DtBindControl, PnlFamilyPlanningMethod, "familyPlanningMethodID", "");
                    }
                    else
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_1'>\n";
                        script += "hide('divFamilyPlanningMethod');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "FamilyPlanning_1", script);
                    }
                }
                else if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "NOT ON FAMILY PLANNING")
                {
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_1'>\n";
                    script += "fnfamilyplanning();\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "FamilyPlanning_1", script);
                }
            }
            //TB Status
            DtBindControl = ds.Tables[8];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["TBStatus"] != DBNull.Value)
                {
                    ddlTBStatus.SelectedValue = DtBindControl.Rows[0]["TBStatus"].ToString();

                    if (DtBindControl.Rows[0]["TBStatus"] != DBNull.Value)
                    {
                        ddlTBStatus.SelectedValue = DtBindControl.Rows[0]["TBStatus"].ToString();
                        if (ddlTBStatus.SelectedItem.Text.Trim() == "TB Rx")
                        {
                            if (DtBindControl.Rows[0]["TBRxStart"] != DBNull.Value)
                                this.txttbstartdate.Value = String.Format("{0:dd-MMM-yyyy}", DtBindControl.Rows[0]["TBRxStart"]);

                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_0'>\n";
                            script += "fnTBStatus();\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TBStatusRX_0", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_0'>\n";
                            script += "fnTBStatus();\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TBStatusRX_0", script);
                        }
                    }
                }
            }
            //  Subsitutions/Interruption
            DtBindControl = ds.Tables[16];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["TherapyPlan"] != DBNull.Value)
                {
                    ListItem item = ddlsubsituationInterruption.Items.FindByValue(DtBindControl.Rows[0]["TherapyPlan"].ToString());
                    if (item != null)
                    {
                        ddlsubsituationInterruption.ClearSelection();
                        item.Selected = true;
                        // ddlsubsituationInterruption.SelectedValue = DtBindControl.Rows[0]["TherapyPlan"].ToString();
                    }
                }

                if (DtBindControl.Rows[0]["TherapyReasonCode"] != System.DBNull.Value)
                {
                    if (this.ddlsubsituationInterruption.SelectedValue == "98")
                    {
                        this.ddlArvTherapyChangeCode.SelectedValue = DtBindControl.Rows[0]["TherapyReasonCode"].ToString();
                        this.txtarvTherapyChangeCodeOtherName.Value = DtBindControl.Rows[0]["TherapyOther"].ToString();
                        if (this.ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode10'>\n";
                            script += "show('arvTherapyChange');\n";
                            script += "show('otherarvTherapyChangeCode');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode10", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode11'>\n";
                            script += "show('arvTherapyChange');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode11", script);
                        }
                    }
                    if (this.ddlsubsituationInterruption.SelectedValue == "99")
                    {
                        this.ddlArvTherapyStopCode.SelectedValue = DtBindControl.Rows[0]["THerapyReasonCOde"].ToString();
                        this.txtarvTherapyStopCodeOtherName.Value = DtBindControl.Rows[0]["TherapyOther"].ToString();
                        DateTime theTmpDtTherapy = Convert.ToDateTime(DtBindControl.Rows[0]["PrescribedARVStartDate"]);
                        this.txtARTEndeddate.Value = theTmpDtTherapy.ToString(Session["AppDateFormat"].ToString());

                        if (this.ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode20'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "show('otherarvTherapyStopCode');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode20", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode21'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode21", script);
                        }
                    }
                }
            }
            //new oi problem
            DtBindControl = ds.Tables[9];

            if (DtBindControl.Rows.Count > 0)
            {
                FillCheckBoxListData(DtBindControl, PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemOther");
            }

            //Potential Side Effects:
            DtBindControl = ds.Tables[10];
            if (DtBindControl.Rows.Count > 0)
            {
                FillCheckBoxListData(DtBindControl, PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffectOther");
            }

            //11  WHO Stage
            DtBindControl = ds.Tables[11];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["WHOStage"] != DBNull.Value)
                    ddlWHOStage.SelectedValue = DtBindControl.Rows[0]["WHOStage"].ToString();
            }

            //12 Cotrimoxazole Adhere
            DtBindControl = ds.Tables[12];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["CPTAdhere"] != DBNull.Value)
                {
                    ddlCotrimoxazoleAdhere.SelectedValue = DtBindControl.Rows[0]["CPTAdhere"].ToString();
                }
            }

            //13 ARV Drugs Adhere + Reason
            DtBindControl = ds.Tables[13];
            if (DtBindControl.Rows.Count != 0)
            {
                if (DtBindControl.Rows[0]["ARVDrugsAdhere"] != DBNull.Value)
                {
                    ddlarvdrugadhere.SelectedValue = DtBindControl.Rows[0]["ARVDrugsAdhere"].ToString();
                    if (ddlarvdrugadhere.SelectedItem.Text == "G=Good")
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_0'>\n";
                        script += "fnARVDrug();\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsPoorFairOther_0", script);
                    }
                }

                if (DtBindControl.Rows[0]["reasonARVDrugsPoorFair"] != DBNull.Value)
                {
                    ddlwhypoorfair.SelectedValue = DtBindControl.Rows[0]["reasonARVDrugsPoorFair"].ToString();
                    if (ddlwhypoorfair.SelectedItem.Text.Contains("Other"))
                    {
                        //tdReasonARVDrugsPoorFairOther
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_0'>\n";
                        script += "show('divReasonARVDrugsother');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsPoorFairOther_0", script);
                    }
                    else
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_1'>\n";
                        script += "hide('divReasonARVDrugsother');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsPoorFairOther_1", script);
                    }
                }
                if (DtBindControl.Rows[0]["reasonARVDrugsPoorFairOther"] != DBNull.Value)
                    txtReasonARVDrugsPoorFairOther.Text = DtBindControl.Rows[0]["reasonARVDrugsPoorFairOther"].ToString();
            }
            ////14 ReferredTo + Other
            DtBindControl = ds.Tables[14];

            if (DtBindControl.Rows.Count > 0)
            {
                FillCheckBoxListData(DtBindControl, PnlReferredTo, "referredTo", "referredToOther");
            }
            //15 Infant Feeding Option
            DtBindControl = ds.Tables[15];
            if (DtBindControl.Rows.Count != 0)
                if (DtBindControl.Rows[0]["infantFeedingOption"] != DBNull.Value)
                    ddlInfantFeedingPractice.SelectedValue = DtBindControl.Rows[0]["infantFeedingOption"].ToString();

            //At Risk Population:
            DtBindControl = ds.Tables[18];

            if (DtBindControl.Rows.Count > 0)
            {
                FillCheckBoxListData(DtBindControl, pnlriskpopulation, "ID", "");
            }

            //At Risk Population Services
            DtBindControl = ds.Tables[19];

            if (DtBindControl.Rows.Count > 0)
            {
                FillCheckBoxListData(DtBindControl, pnlriskpopulationservice, "ID", "");
            }

            //Prevention with positives (PwP):
            DtBindControl = ds.Tables[20];

            if (DtBindControl.Rows.Count > 0)
            {
                FillCheckBoxListData(DtBindControl, pnlprewithpositive, "ID", "PWPOther");
            }
        }

        /// <summary>
        /// Show_hides this instance.
        /// </summary>
        public void show_hide()
        {
            string script = string.Empty;

            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
            script += "fnchange();fnfamilyplanning();fnTBStatus();fnarvdrugother();fnSubsituations();fnvisittype();\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Add" && Convert.ToInt32(ViewState["VisitID"]) > 0)
            {
                string theUrl;
                theUrl = string.Format("frmPatient_Home.aspx");
                Response.Redirect(theUrl);
            }
            else
            {
                string theUrl;
                theUrl = string.Format("frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDataQualityCheck control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnDataQualityCheck_Click(object sender, EventArgs e)
        {
            if (dataQualityCheck())
            {
                btnDataQualityCheck.CssClass = "greenButton";
                save(true);
            }
            else
            {
                btnDataQualityCheck.CssClass = "";
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLabratory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnLabratory_Click(object sender, EventArgs e)
        {
            try
            {
                save(false, "Labratory");
            }
            finally { }
        }

        /// <summary>
        /// Handles the Click event of the btnOrderLabTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnOrderLabTest_Click(object sender, EventArgs e)
        {
            try
            {
                save(false, "LabTest");
            }
            finally { }
        }

        /// <summary>
        /// Handles the Click event of the btnpharmacy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnpharmacy_Click(object sender, EventArgs e)
        {
            try
            {
                save(false, "Pharmacy");
            }
            finally { }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["name"] == "Delete")
                {
                    DeleteForm();
                }

                save(false);
            }
            finally { }
        }

        /// <summary>
        /// Handles the Click event of the buttonICF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void buttonICF_Click(object sender, EventArgs e)
        {
            save(false, "ICF");
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //Get Sessions
            if (CurrentSession.Current == null || Session["PatientId"] == null || Session["TechnicalAreaId"] == null)
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect("~/frmLogin.aspx", true);
            }
            patientID = Convert.ToInt32(Session["PatientId"].ToString());
            locationID = Convert.ToInt32(Session["AppLocationId"].ToString());
            gender = Session["PatientSex"].ToString();
            if (null != (Session["patientageinyearmonth"]))
            {
                age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
            }

            if (null != (Session["PatientAge"]))
            {
                ageInYear = (int)Convert.ToDouble(Session["PatientAge"].ToString());
            }

            if (null!= (Session["PatientAge"]))
            {
                ageInMonth = Convert.ToInt32(Session["PatientAge"].ToString().Substring(Session["PatientAge"].ToString().IndexOf(".") + 1));
            }
          

            //Header Texts
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Initial and Follow up Visits Form";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Initial and Follow up Visits Form";

            _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
            DsBindControl = _InitialFollowupVisit.GetInitialFollowupVisitData(patientID, locationID);
            BindControl(DsBindControl);
            //auth
            auth();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BMIAttributes();
            PutCustomControl();
            AddUIAttributes();
            show_hide();
            if (Convert.ToInt32(ViewState["VisitID"]) > 0)
            {
                isUpdate = true;
            }
            else
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    visitID = Convert.ToInt32(Session["PatientVisitId"]);
                    isUpdate = true;
                }
                else
                {
                    visitID = 0;
                }
                ViewState["VisitID"] = visitID;
            }
            if (!IsPostBack && Convert.ToInt32(ViewState["VisitID"]) > 0)
            {
                BMIAttributes();
                _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
                DsBindData = _InitialFollowupVisit.GetInitialFollowupVisitInfo(patientID, locationID, Convert.ToInt32(ViewState["VisitID"]));
                SetControlData(DsBindData);
            }
            FillOldData(patientID);
            BMIAttributes();
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    this.ShowHideICFButton();
            //}
        }

        /// <summary>
        /// Handles the TextChanged event of the txtPhysHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void txtPhysHeight_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                if (txtPhysHeight.Text != "" && txtPhysWeight.Text != "")
                {
                    CalculateZScore();
                }
                else if (txtPhysHeight.Text == "")
                {
                    lblWHClassification.Text = "Enter Height";
                    lblWHClassification.ForeColor = Color.Red;
                }
                else if (txtPhysWeight.Text == "")
                {
                    lblWAClassification.Text = "Enter Weight";
                    lblWHClassification.Text = "Enter weight";
                    lblWAClassification.ForeColor = Color.Red;
                    lblWHClassification.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtPhysWeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void txtPhysWeight_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                if (txtPhysHeight.Text != "" && txtPhysWeight.Text != "")
                {
                    CalculateZScore();
                }
                else if (txtPhysHeight.Text == "")
                {
                    lblWHClassification.Text = "Enter Height";
                    lblWHClassification.ForeColor = Color.Red;
                }
                else if (txtPhysWeight.Text == "")
                {
                    lblWAClassification.Text = "Enter Weight";
                    lblWHClassification.Text = "Enter weight";
                    lblWAClassification.ForeColor = Color.Red;
                    lblWHClassification.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Adds the UI attributes.
        /// </summary>
        private void AddUIAttributes()
        {
            // txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtVisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");

            txtdatemiscarriage.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            //   txtdatemiscarriage.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtdatemiscarriage.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtdateinducedabortion.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); ");
            //txtdateinducedabortion.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtdateinducedabortion.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txttbstartdate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            // txttbstartdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txttbstartdate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtdatenextappointment.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); ");
            //  txtdatenextappointment.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtdatenextappointment.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtBMI.Attributes.Add("onkeyup", "chkInteger('" + txtBMI.ClientID + "')");
            txtphysTemp.Attributes.Add("onkeyup", "chkDecimal('" + txtphysTemp.ClientID + "'); AddBoundary('" + txtphysTemp.ClientID + "','" + 30 + "','" + 50 + "')");
            txtphysTemp.Attributes.Add("onBlur", "isBetween('" + txtphysTemp.ClientID + "', '" + "Temperature" + "', '" + 30 + "', '" + 50 + "')");

            txtPhysHeight.Attributes.Add("onkeyup", "chkInteger('" + txtPhysHeight.ClientID + "')");
            txtPhysHeight.Attributes.Add("onBlur", "isBetween('" + txtPhysHeight.ClientID + "', '" + "physHeight" + "', '" + 0 + "', '" + 250 + "')");

            txtPhysWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtPhysWeight.ClientID + "')");
            txtPhysWeight.Attributes.Add("onBlur", "isBetween('" + txtPhysWeight.ClientID + "', '" + "physWeight" + "', '" + 0 + "', '" + 225 + "')");

            txtEDD.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtEDD.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtNumOfDaysHospitalized.Attributes.Add("onkeyup", "chkInteger('" + txtNumOfDaysHospitalized.ClientID + "')");
            txtNumOfDaysHospitalized.Attributes.Add("OnBlur", "chkInteger('" + txtNumOfDaysHospitalized.ClientID + "')");

            if (age >= 15)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeads", "hide('divscore');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeads", "show('divscore');", true);
            }
        }

        /// <summary>
        /// Bmis the attributes.
        /// </summary>
        private void BMIAttributes()
        {
            txtPhysWeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtBMI.ClientID + "','" + txtPhysWeight.ClientID + "','" + txtPhysHeight.ClientID + "');");
            txtPhysHeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtBMI.ClientID + "','" + txtPhysWeight.ClientID + "','" + txtPhysHeight.ClientID + "');");
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        private void closeWindow()
        {
            Session["ArtEncounterPatientVisitId"] = 0;
            ViewState["VisitID"] = 0;
            Session["PatientVisitId"] = 0;
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + Session["PatientId"].ToString() + "';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_InitialFollowupVisit.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        /// <summary>
        /// Datas the quality check.
        /// </summary>
        /// <returns></returns>
        private Boolean dataQualityCheck()
        {
            
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            DateTime temp;
            string validateMessage = "Following values are required to complete the data quality check:\\n\\n";
            //validateMessage += ddlvisittype.SelectedItem.Value;
            //validateMessage += "\\n\\nValue";
            //validateMessage += ddlvisittype.SelectedValue;
            //validateMessage += "\\n\\nIndex";
            //validateMessage += ddlvisittype.Items[ddlvisittype.SelectedIndex];
            //validateMessage += "\\n\\n";
            bool qualityCheck = true;
            restoreFontColor();

            #region Check Visit Date

            if (Session["RegDate"] != null && txtVisitDate.Text != "")
            {
                if (Convert.ToDateTime(txtVisitDate.Text) < Convert.ToDateTime(Session["RegDate"]))
                {
                    txtVisitDate.Focus();
                    MsgBuilder totalMsgBuilder = new MsgBuilder();
                    totalMsgBuilder.DataElements["MessageText"] = "Visit Date should not be less then registration date";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    return false;
                }
            }

            if (txtVisitDate.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtVisitDate.Focus();
                qualityCheck = false;
                lblVisitDate.Style.Add("color", "red");
            }
            else
            {
                if (!DateTime.TryParseExact(txtVisitDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtVisitDate.Focus();
                    qualityCheck = false;
                    lblVisitDate.Style.Add("color", "red");
                }
                else if (theCurrentDate < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "\\n";
                    txtVisitDate.Focus();
                    qualityCheck = false;
                    lblVisitDate.Style.Add("color", "red");
                }
            }

            #endregion Check Visit Date

            if (ddlvisittype.Items[ddlvisittype.SelectedIndex].ToString() != "TS - Treatment Supporter")
            {
                #region Weight

                if (txtPhysWeight.Text.Trim() == "")
                {

                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Weight";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";

                    txtPhysWeight.Focus();
                    qualityCheck = false;
                    lblWeight.Style.Add("color", "red");
                }

                #endregion Weight

                #region Height

                if (txtPhysHeight.Text.Trim() == "")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Height";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                    txtPhysHeight.Focus();
                    qualityCheck = false;
                    lblHeight.Style.Add("color", "red");
                }

                #endregion Height

                #region WHOStage

                if (ddlWHOStage.SelectedItem.Text == "Select")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -WHO Stage";
                    validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                    ddlWHOStage.Focus();
                    qualityCheck = false;
                    lblWHOStage.Style.Add("color", "red");
                }

                #endregion WHOStage

            }
            #region ARVDrugsAdhere

            if (ddlarvdrugadhere.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -ARV Drugs Adhere";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                ddlarvdrugadhere.Focus();
                qualityCheck = false;
                lblARVDrugsAdhere.Style.Add("color", "red");
            }

            #endregion ARVDrugsAdhere

            if (!qualityCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return qualityCheck;
        }

        /// <summary>
        /// Deletes the form.
        /// </summary>
        private void DeleteForm()
        {
            IinitialFollowupVisit InitialFollowupvisit;
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
            FormName = "Initial and Follow up Visits";

            InitialFollowupvisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
            theResultRow = (int)InitialFollowupvisit.DeleteInitialFollowupVisitForm(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Convert.ToInt32(Session["PatientVisitId"]));
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean fieldValidation()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            string validateMessage = "Following values are required:\\n\\n";
            bool validationCheck = true;

            DateTime temp, tempVisit, tempTCA;

         

            #region Check Visit Date

            if (Session["RegDate"] != null && txtVisitDate.Text != "")
            {
                if (Convert.ToDateTime(txtVisitDate.Text) < Convert.ToDateTime(Session["RegDate"]))
                {
                    txtVisitDate.Focus();
                    MsgBuilder totalMsgBuilder = new MsgBuilder();
                    totalMsgBuilder.DataElements["MessageText"] = "Visit Date should not be less then registration date";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    return false;
                }
            }

            if (txtVisitDate.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtVisitDate.Focus();
                validationCheck = false;
            }
            else
            {
                if (!DateTime.TryParseExact(txtVisitDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out tempVisit))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtVisitDate.Focus();
                    validationCheck = false;
                }
                else if (theCurrentDate.Date < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "\\n";
                    txtVisitDate.Focus();
                    validationCheck = false;
                }
            }

            #endregion Check Visit Date
            DateTime? _visitDate = null;
            if (!String.IsNullOrWhiteSpace(txtVisitDate.Text))
            {
                _visitDate = Convert.ToDateTime(txtVisitDate.Text);
            }
            
            #region Date of next appointment

            if (txtdatenextappointment.Value.Trim() != "")
            {
                DateTime.TryParseExact(txtVisitDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out tempVisit);
                if (!DateTime.TryParseExact(txtdatenextappointment.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out tempTCA))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Follow Up Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtdatenextappointment.Focus();
                    validationCheck = false;
                }
                else if (tempTCA <= tempVisit)
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("Initialfollowupvisitnextappointment", this) + "\\n";
                    txtdatenextappointment.Focus();
                    validationCheck = false;
                }
                //else if (theCurrentDate.Date >= Convert.ToDateTime(iQCareUtils.MakeDate(txtdatenextappointment.Value)))
                //{
                //    validateMessage += "-" + IQCareMsgBox.GetMessage("Initialfollowupvisitnextappointment", this) + "\\n";
                //    txtdatenextappointment.Focus();
                //    validationCheck = false;
                //}
            }

            #endregion Date of next appointment

            #region Check EDD

            if (txtEDD.Value.Trim() != "")
                if (!DateTime.TryParseExact(txtEDD.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -EDD";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtEDD.Focus();
                    validationCheck = false;
                }
                else if ((_visitDate.HasValue) && (_visitDate.Value > Convert.ToDateTime(iQCareUtils.MakeDate(txtEDD.Value))))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("EDDDate", this) + "\\n";
                    txtEDD.Focus();
                    validationCheck = false;
                }

            #endregion Check EDD

            #region Date of Miscarriage

            if (txtdatemiscarriage.Value.Trim() != "")
                if (!DateTime.TryParseExact(txtdatemiscarriage.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -EDD";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtdatemiscarriage.Focus();
                    validationCheck = false;
                }
                else if (_visitDate <= Convert.ToDateTime(iQCareUtils.MakeDate(txtdatemiscarriage.Value)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("DateofMiscarriage", this) + "\\n";
                    txtdatemiscarriage.Focus();
                    validationCheck = false;
                }

            #endregion Date of Miscarriage

            #region Date of Induced Abortion

            if (txtdateinducedabortion.Value.Trim() != "")
                if (!DateTime.TryParseExact(txtdateinducedabortion.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -EDD";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtdateinducedabortion.Focus();
                    validationCheck = false;
                }
                else if (Convert.ToDateTime(iQCareUtils.MakeDate(txtdateinducedabortion.Value)) > _visitDate)
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("DateofInducedAbortion", this) + "\\n";
                    txtdateinducedabortion.Focus();
                    validationCheck = false;
                }

            #endregion Date of Induced Abortion

            #region TB Treatment Start Date

            if (txttbstartdate.Value.Trim() != "")
                if (!DateTime.TryParseExact(txttbstartdate.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -EDD";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txttbstartdate.Focus();
                    validationCheck = false;
                }
                else if (_visitDate < Convert.ToDateTime(iQCareUtils.MakeDate(txttbstartdate.Value)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("TBTreatmentStartDate", this) + "\\n";
                    txttbstartdate.Focus();
                    validationCheck = false;
                }

            #endregion TB Treatment Start Date

            #region Subsituations/Interruption

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Subsitutions/Interruption";
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                ddlsubsituationInterruption.Focus();
                validationCheck = false;
            }

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                ddlArvTherapyStopCode.Focus();
                validationCheck = false;
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && txtARTEndeddate.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Date ART Ended";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                txtARTEndeddate.Focus();
                validationCheck = false;
            }

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && (theCurrentDate.Date < Convert.ToDateTime(iQCareUtils.MakeDate(txtARTEndeddate.Value))))
            {
                MsgBuilder theBuilder = new MsgBuilder();
                validateMessage += "-" + IQCareMsgBox.GetMessage("ARTEndDATE", this) + "\\n";
                txtARTEndeddate.Focus();
                validationCheck = false;
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other") && txtarvTherapyStopCodeOtherName.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "- Stop Regimen Reason Other(Specify)";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                ddlArvTherapyStopCode.Focus();
                validationCheck = false;
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                ddlArvTherapyChangeCode.Focus();
                validationCheck = false;
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                if (txtarvTherapyChangeCodeOtherName.Value == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Stop Regimen Reason (Other)Specify";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                    ddlArvTherapyChangeCode.Focus();
                    validationCheck = false;
                }
            }

            IQCareUtils theUtils = new IQCareUtils();
            _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");
            DataSet dsValidate = _InitialFollowupVisit.GetExistInitialFollowupVisitbydate(patientID, Convert.ToDateTime(theUtils.MakeDate(txtVisitDate.Text)), locationID);

            if (dsValidate.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ViewState["VisitID"]) != Convert.ToInt32(dsValidate.Tables[0].Rows[0][0]))
                {
                    IQCareMsgBox.Show("ARTHIVCareenCounterExists", this);
                    validationCheck = false;
                    return validationCheck; ;
                }
            }

            #endregion Subsituations/Interruption

            if (!validationCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return validationCheck;
        }

        /// <summary>
        /// Fills the CheckBox list data.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        /// <param name="thePnl">The PNL.</param>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="theFieldName">Name of the field.</param>
        private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
        {
            foreach (DataRow DR in theDT.Rows)
            {
                foreach (Control y in thePnl.Controls)
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    {
                        if (y.ID.StartsWith("Pnl"))
                        {
                            FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                        }
                    }
                    else
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                            if (((CheckBox)y).ID == "Chk-" + DR[FieldName].ToString() + "-" + theControlId[2].ToString())
                                ((CheckBox)y).Checked = true;
                        }
                        if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {
                            if (theFieldName != "")
                            {
                                string[] theControlId;
                                if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                                {
                                    theControlId = ((TextBox)y).ID.ToString().Split('-');
                                    ((TextBox)y).Text = DR[theFieldName].ToString();
                                }
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                                script += "show('txt" + (((TextBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "" + ((TextBox)y).ID + "", script);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fills the old data.
        /// </summary>
        /// <param name="PatID">The pat identifier.</param>
        private void FillOldData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;

            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = "";
                if (theCustomFields.Tables[0].Rows.Count > 0)
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                string theColName = "";
                foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
                {
                    if (theDR["ControlId"].ToString() != "9")
                    {
                        if (theColName == "")
                            theColName = theDR["Label"].ToString();
                        else
                            theColName = theColName + "," + theDR["Label"].ToString();
                    }
                }

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitID, 0, 0, Convert.ToInt32(ApplicationAccess.InitialFollowupVisits));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "HCACounter");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        /// <summary>
        /// Generates the custom fields values.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        private void GenerateCustomFieldsValues(Control Cntrl)
        {
            string pnlName = Cntrl.ID;
            sbValues = new StringBuilder();
            strmultiselect = string.Empty;
            string strfName = string.Empty;
            bool radioflag = false;

            int stpos = 0;
            int enpos = 0;
            if (ViewState["CustomFieldsData"] != null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            strfName = pnlName.ToUpper() + "TXT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] = ' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            strfName = pnlName.ToUpper() + "NUM";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append("," + strfName + "=Null");
                            }
                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            strfName = pnlName.ToUpper() + "DT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            strfName = pnlName.ToUpper() + "RADIO1";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            strfName = pnlName.ToUpper() + "RADIO2";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            strfName = pnlName.ToUpper() + "SELECTLIST";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] =  " + "0");
                            }
                        }
                    }
                }
            }

            if (ViewState["CustomFieldsData"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append("," + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append(",0");
                            }
                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(", " + "0");
                            }
                        }
                    }
                }
            }
            if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(CheckBoxList))
                    {
                        if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                        {
                            foreach (ListItem li in ((CheckBoxList)x).Items)
                            {
                                if (Convert.ToInt32(li.Selected) == 1)
                                {
                                    strmultiselect += " " + li.Value.ToString() + ",";
                                }
                            }
                            strmultiselect += "^";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the CheckBox listchecked i ds.
        /// </summary>
        /// <param name="thePnl">The PNL.</param>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="thetxtFieldName">Name of the thetxt field.</param>
        /// <param name="Flag">The flag.</param>
        /// <returns></returns>
        private DataTable GetCheckBoxListcheckedIDs(Panel thePnl, string FieldName, string thetxtFieldName, int Flag)
        {
            string chktrueother = "";
            int chktrueothervalue = 0;
            if (Flag == 0)
            {
                DTCheckedIds = new DataTable();

                if (DTCheckedIds.Columns.Contains(FieldName) == false && DTCheckedIds.Columns.Contains(FieldName) == false)
                {
                    DataColumn dataColumnPotentialSideEffect = new DataColumn(FieldName);
                    dataColumnPotentialSideEffect.DataType = System.Type.GetType("System.Int32");
                    DTCheckedIds.Columns.Add(dataColumnPotentialSideEffect);
                    if (thetxtFieldName != "")
                    {
                        DataColumn thepotentialSideEffect_Other = new DataColumn(thetxtFieldName);
                        thepotentialSideEffect_Other.DataType = System.Type.GetType("System.String");
                        DTCheckedIds.Columns.Add(thepotentialSideEffect_Other);
                    }
                }
            }
            DataRow theDR;
            foreach (Control y in thePnl.Controls)
            {
                if (y.GetType() == typeof(Panel))
                    GetCheckBoxListcheckedIDs((Panel)y, FieldName, thetxtFieldName, 1);
                else
                {
                    if (y.GetType() == typeof(CheckBox))
                    {
                        if (((CheckBox)y).Checked == true)
                        {
                            string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                            //theDR[FieldName] = theControlId[1].ToString();
                            if (theControlId[2].ToString().Contains("Other") == true)
                            {
                                chktrueother = theControlId[2].ToString();
                                chktrueothervalue = Convert.ToInt32(theControlId[1].ToString());
                            }
                            else
                            {
                                theDR = DTCheckedIds.NewRow();
                                theDR[FieldName] = theControlId[1].ToString();
                                DTCheckedIds.Rows.Add(theDR);
                            }
                        }
                    }
                    if (y.GetType() == typeof(TextBox))
                    {
                        if (thetxtFieldName != "")
                        {
                            if (((TextBox)y).ID.Contains("OtherTXT") == true)
                            {
                                theDR = DTCheckedIds.NewRow();
                                string[] theControlId = ((TextBox)y).ID.ToString().Split('-');
                                theDR[FieldName] = chktrueothervalue.ToString();
                                if (((TextBox)y).Text != "")
                                {
                                    theDR[thetxtFieldName] = ((TextBox)y).Text;
                                    DTCheckedIds.Rows.Add(theDR);
                                }
                            }
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                            script += "show('txt" + chktrueothervalue.ToString() + "');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "" + ((TextBox)y).ID + "", script);
                        }
                    }
                    //DTCheckedIds.Rows.Add(theDR);
                }
            }
            return DTCheckedIds;
        }

        /// <summary>
        /// Inserts the custom fields values.
        /// </summary>
        private void InsertCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            string sqlselect;
            int visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            PatID = Convert.ToInt32(Request.QueryString["patientid"]);
            ICustomFields CustomFields;
            if (ViewState["VisitID_add"] != null)
                visitID = Convert.ToInt32(ViewState["VisitID_add"]);
            //  if (txtvisitDate.Text.ToString() != "")
            visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

            if (sbValues.ToString().Trim() != "")
            {
                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
                if (arl.Count != 0)
                {
                    int p = 0;
                    foreach (object obj in arl)
                    {
                        sqlselect = "";
                        if (obj.ToString() != "")
                        {
                            if (FieldValues[p].ToString() != "")
                            {
                                string[] mValues = FieldValues[p].Split(new char[] { ',' });
                                foreach (string str in mValues)
                                {
                                    if (str.ToString() != "")
                                    {
                                        string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                        Int32 ispos = Convert.ToInt32(strtab.Length);
                                        Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
                                        sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                        sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
                                        try
                                        {
                                            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                            icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                            if (icount == -1)
                                            {
                                                return;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                        finally
                                        {
                                            CustomFields = null;
                                        }
                                    }
                                }
                            }
                        }
                        p += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Puts the custom control.
        /// </summary>
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.InitialFollowupVisits));
                //    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "HCACounter");
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "IFVisit");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                ViewState["CustomFieldsDS"] = theDS;
                pnlCustomList.Visible = true;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        /// <summary>
        /// Restores the color of the font.
        /// </summary>
        private void restoreFontColor()
        {
            lblVisitDate.Style.Remove("color");
            lblNextAppointment.Style.Remove("color");
            lblHeight.Style.Remove("color");
            lblWeight.Style.Remove("color");
            lblPregnant.Style.Remove("color");
            lblTBStatus.Style.Remove("color");
            lblWHOStage.Style.Remove("color");
            lblARVDrugsAdhere.Style.Remove("color");
        }

        /// <summary>
        /// Saves the specified is data quailty checked.
        /// </summary>
        /// <param name="isDataQuailtyChecked">if set to <c>true</c> [is data quailty checked].</param>
        private void save(bool isDataQuailtyChecked)
        {
            if (fieldValidation() == false)
            { return; }

            hashTable = new Hashtable();
            dataSetForSaving = new DataSet();
            _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");

            //Appointment Scheduling
            hashTable.Add("patientID", patientID.ToString());
            if (!isDataQuailtyChecked)
                hashTable.Add("dataQuality", "0");
            else
                hashTable.Add("dataQuality", "1");
            hashTable.Add("locationID", locationID.ToString());
            hashTable.Add("visitDate", txtVisitDate.Text);
            hashTable.Add("ModuleId", Convert.ToInt32(Session["TechnicalAreaId"]));
            hashTable.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));

            if (chkifschedule.Checked == true)
            {
                hashTable.Add("Scheduled", 1);
            }
            else
            {
                hashTable.Add("Scheduled", 0);
            }

            hashTable.Add("TypeofVisit", ddlvisittype.SelectedValue);
            hashTable.Add("treatmentSupporterName", txtTreatmentSupporterName.Text);
            hashTable.Add("treatmentSupporterContact", txtTreatmentSupporterContact.Text);

            //Clinical Status
            hashTable.Add("physTemp", txtphysTemp.Text);
            hashTable.Add("height", txtPhysHeight.Text);
            hashTable.Add("weight", txtPhysWeight.Text);
            hashTable.Add("BPSystolic", txtBPSystolic.Text);
            hashTable.Add("BPDiastolic", txtBPDiastolic.Text);
            hashTable.Add("muac", textMuac.Text);
            //Pegnancy
            hashTable.Add("pregnant", ddlpregnancy.SelectedValue);
            if (ddlpregnancy.SelectedValue.ToString() == "89")
            {
                hashTable.Add("EDD", txtEDD.Value);
                hashTable.Add("ANCNo", txtANCNumber.Text);
                if (chkrefpmtct.Checked)
                {
                    hashTable.Add("ReferredtoPMTCT", 1);
                }
                else
                {
                    hashTable.Add("ReferredtoPMTCT", 0);
                }
            }
            else if (ddlpregnancy.SelectedValue.ToString() == "91")
            {
                hashTable.Add("DateofInducedAbortion", txtdateinducedabortion.Value);
            }
            else if (ddlpregnancy.SelectedValue.ToString() == "92")
            {
                hashTable.Add("DateofMiscarriage", txtdatemiscarriage.Value);
            }

            //Family Planning
            hashTable.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
            //Table 0-Family Planning Methods
            DataTable dataTableFamilyPlanningMethods = new DataTable();
            if (ddlFamilyPanningStatus.SelectedItem.Text == "Currently on Family Planning" || ddlFamilyPanningStatus.SelectedItem.Text == "Wants Family Planning")
            {
                dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);
            }
            else if (ddlFamilyPanningStatus.SelectedItem.Text == "Not on Family Planning")
            {
                hashTable.Add("NoFamilyPlanning", ddlnotfamilyplanning.SelectedValue);
            }
            dataSetForSaving.Tables.Add(dataTableFamilyPlanningMethods);

            //TB Rx
            hashTable.Add("TBStatus", ddlTBStatus.SelectedValue);

            if (ddlTBStatus.SelectedValue.ToString() == "3")
            {
                hashTable.Add("TBStartDate", txttbstartdate.Value);
                hashTable.Add("TBTreatmentNo", txtTBtreatmentNumber.Value);
            }

            //Potential Side Effects
            DataTable dataTablePotentialSideEffect = new DataTable();
            dataTablePotentialSideEffect = GetCheckBoxListcheckedIDs(PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffect_Other", 0);
            dataSetForSaving.Tables.Add(dataTablePotentialSideEffect);

            //New OIs, Other Problems
            DataTable dataTableNewOIsProblems = new DataTable();
            dataTableNewOIsProblems = GetCheckBoxListcheckedIDs(PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

            //Nutritional Problems
            hashTable.Add("nutritionalProblem", ddlNutritionalProblems.SelectedValue);

            //WHO Stage
            hashTable.Add("WHOStage", ddlWHOStage.SelectedValue);
            //Pharmacy
            hashTable.Add("CotrimoxazoleAdhere", ddlCotrimoxazoleAdhere.SelectedValue);
            hashTable.Add("ARVDrugsAdhere", ddlarvdrugadhere.SelectedValue);
            hashTable.Add("WhyPooFair", ddlwhypoorfair.SelectedValue);
            hashTable.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);

            //Subsitutions/Interruption
            hashTable.Add("TherapyPlan", ddlsubsituationInterruption.SelectedValue);

            string OtherReason = "";
            string ARVTherapyCode = "0";
            string artendeddate = "";
            if (ddlsubsituationInterruption.SelectedValue == "98")
            {
                ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
                if (ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyChangeCodeOtherName.Value;
                }
            }
            else if (ddlsubsituationInterruption.SelectedValue == "99")
            {
                ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
                artendeddate = txtARTEndeddate.Value;
                if (ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyStopCodeOtherName.Value;
                }
            }

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "96")
            {
                Session["ARTEndedStatus"] = "";
            }
            else if (ddlsubsituationInterruption.SelectedValue.ToString() == "99")
            {
                Session["ARTEndedStatus"] = "ART Stopped";
            }
            hashTable.Add("PrescribedARVStartDate", artendeddate);
            hashTable.Add("TherapyReasonCode", ARVTherapyCode);
            hashTable.Add("TherapyOther", OtherReason);

            //Referred To
            DataTable dataTableReferredTo = new DataTable();
            dataTableReferredTo = GetCheckBoxListcheckedIDs(PnlReferredTo, "referredToID", "referredToOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableReferredTo);

            //If Hospitalized # of Days
            hashTable.Add("numOfDaysHospitalized", txtNumOfDaysHospitalized.Text);
            //Nutritional Support
            hashTable.Add("nutritionalSupport", ddlNutritionalSupport.SelectedValue);
            //Infant Feeding Option
            hashTable.Add("infantFeedingOption", ddlInfantFeedingPractice.SelectedValue);

            //Positive Prevention At Risk Population
            DataTable dtatRiskPopulation = new DataTable();
            dtatRiskPopulation = GetCheckBoxListcheckedIDs(pnlriskpopulation, "RiskPopulationID", "RiskPopulationOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dtatRiskPopulation);

            //Positive Prevention At Risk Population Services
            DataTable dtatRiskPopulationService = new DataTable();
            dtatRiskPopulationService = GetCheckBoxListcheckedIDs(pnlriskpopulationservice, "PopulationServiceID", "PopulationServiceOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dtatRiskPopulationService);

            //Prevention with positives (PwP)
            DataTable dtPreventionPositive = new DataTable();
            dtPreventionPositive = GetCheckBoxListcheckedIDs(pnlprewithpositive, "pwpID", "pwpID_Other", 0);
            dataSetForSaving.Tables.Add(dtPreventionPositive);

            //Name of attending clinician
            hashTable.Add("attendingClinician", ddlattendingclinician.SelectedValue);

            hashTable.Add("Datenextappointment", txtdatenextappointment.Value);

            //CreateDate
            if (isUpdate)
            {
                hashTable.Add("createDate", ViewState["createDate"].ToString());
                hashTable.Add("visitID", Convert.ToInt32(ViewState["VisitID"]));
            }

            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.InitialFollowupVisits, (DataSet)ViewState["CustomFieldsDS"]);
            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.InitialFollowupVisits, (DataSet)ViewState["CustomFieldsDS"]);
            }
            //Added by VY 2014 aug 27 to check whether the people want to auto populate the pharmacy form
            hashTable.Add("autoPopulatePharmacy", ckb_AutoPopPresc.Checked.ToString());
            DataSet dsreturn = _InitialFollowupVisit.SaveUpdateInitialFollowupVisitData(hashTable, dataSetForSaving, isUpdate, theCustomDataDT);
            Session["PatientVisitId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["visitID"].ToString());
            ViewState["VisitID"] = Session["PatientVisitId"];
            Session["ServiceLocationId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["LocationID"].ToString());
            DsBindData = _InitialFollowupVisit.GetInitialFollowupVisitInfo(patientID, locationID, Convert.ToInt32(ViewState["VisitID"]));
            if (DsBindData.Tables[0].Rows[0]["createDate"] != DBNull.Value)
                ViewState["createDate"] = DsBindData.Tables[0].Rows[0]["createDate"].ToString();
            hashTable.Clear();
            closeWindow();
        }

        /// <summary>
        /// Saves the specified is data quailty checked.
        /// </summary>
        /// <param name="isDataQuailtyChecked">if set to <c>true</c> [is data quailty checked].</param>
        /// <param name="button">The button.</param>
        private void save(bool isDataQuailtyChecked, string button)
        {
            if (fieldValidation() == false)
            { return; }

            hashTable = new Hashtable();
            dataSetForSaving = new DataSet();
            _InitialFollowupVisit = (IinitialFollowupVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialFollowupVisit, BusinessProcess.Clinical");

            //Appointment Scheduling
            hashTable.Add("patientID", patientID.ToString());
            if (!isDataQuailtyChecked)
                hashTable.Add("dataQuality", "0");
            else
                hashTable.Add("dataQuality", "1");
            hashTable.Add("locationID", locationID.ToString());
            hashTable.Add("visitDate", txtVisitDate.Text);
            hashTable.Add("ModuleId", Convert.ToInt32(Session["TechnicalAreaId"]));
            hashTable.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));

            if (chkifschedule.Checked == true)
            {
                hashTable.Add("Scheduled", 1);
            }
            else
            {
                hashTable.Add("Scheduled", 0);
            }
            hashTable.Add("TypeofVisit", ddlvisittype.SelectedValue);
            hashTable.Add("treatmentSupporterName", txtTreatmentSupporterName.Text);
            hashTable.Add("treatmentSupporterContact", txtTreatmentSupporterContact.Text);

            //Clinical Status
            hashTable.Add("physTemp", txtphysTemp.Text);
            hashTable.Add("height", txtPhysHeight.Text);
            hashTable.Add("weight", txtPhysWeight.Text);
            hashTable.Add("muac", textMuac.Text);
            hashTable.Add("BPSystolic", txtBPSystolic.Text);
            hashTable.Add("BPDiastolic", txtBPDiastolic.Text);

            //Pegnancy
            hashTable.Add("pregnant", ddlpregnancy.SelectedValue);
            if (ddlpregnancy.SelectedValue.ToString() == "89")
            {
                hashTable.Add("EDD", txtEDD.Value);
                hashTable.Add("ANCNo", txtANCNumber.Text);
                if (chkrefpmtct.Checked)
                {
                    hashTable.Add("ReferredtoPMTCT", 1);
                }
                else
                {
                    hashTable.Add("ReferredtoPMTCT", 0);
                }
            }
            else if (ddlpregnancy.SelectedValue.ToString() == "91")
            {
                hashTable.Add("DateofInducedAbortion", txtdateinducedabortion.Value);
            }
            else if (ddlpregnancy.SelectedValue.ToString() == "92")
            {
                hashTable.Add("DateofMiscarriage", txtdatemiscarriage.Value);
            }

            //Family Planning
            hashTable.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
            //Table 0-Family Planning Methods
            DataTable dataTableFamilyPlanningMethods = new DataTable();
            if (ddlFamilyPanningStatus.SelectedItem.Text == "Currently on Family Planning" || ddlFamilyPanningStatus.SelectedItem.Text == "Wants Family Planning")
            {
                dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);
            }
            else if (ddlFamilyPanningStatus.SelectedItem.Text == "Not on Family Planning")
            {
                hashTable.Add("NoFamilyPlanning", ddlnotfamilyplanning.SelectedValue);
            }
            dataSetForSaving.Tables.Add(dataTableFamilyPlanningMethods);

            //TB Rx
            hashTable.Add("TBStatus", ddlTBStatus.SelectedValue);

            if (ddlTBStatus.SelectedValue.ToString() == "3")
            {
                hashTable.Add("TBStartDate", txttbstartdate.Value);
                hashTable.Add("TBTreatmentNo", txtTBtreatmentNumber.Value);
            }

            //Potential Side Effects
            DataTable dataTablePotentialSideEffect = new DataTable();
            dataTablePotentialSideEffect = GetCheckBoxListcheckedIDs(PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffect_Other", 0);
            dataSetForSaving.Tables.Add(dataTablePotentialSideEffect);

            //New OIs, Other Problems
            DataTable dataTableNewOIsProblems = new DataTable();
            dataTableNewOIsProblems = GetCheckBoxListcheckedIDs(PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

            //Nutritional Problems
            hashTable.Add("nutritionalProblem", ddlNutritionalProblems.SelectedValue);

            //WHO Stage
            hashTable.Add("WHOStage", ddlWHOStage.SelectedValue);

            //Pharmacy
            hashTable.Add("CotrimoxazoleAdhere", ddlCotrimoxazoleAdhere.SelectedValue);
            hashTable.Add("ARVDrugsAdhere", ddlarvdrugadhere.SelectedValue);
            hashTable.Add("WhyPooFair", ddlwhypoorfair.SelectedValue);
            hashTable.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);

            //Subsitutions/Interruption
            hashTable.Add("TherapyPlan", ddlsubsituationInterruption.SelectedValue);

            string OtherReason = "";
            string ARVTherapyCode = "0";
            string artendeddate = "";
            if (ddlsubsituationInterruption.SelectedValue == "98")
            {
                ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
                if (ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyChangeCodeOtherName.Value;
                }
            }
            else if (ddlsubsituationInterruption.SelectedValue == "99")
            {
                ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
                artendeddate = txtARTEndeddate.Value;
                if (ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyStopCodeOtherName.Value;
                }
            }

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "96")
            {
                Session["ARTEndedStatus"] = "";
            }
            else if (ddlsubsituationInterruption.SelectedValue.ToString() == "99")
            {
                Session["ARTEndedStatus"] = "ART Stopped";
            }
            hashTable.Add("PrescribedARVStartDate", artendeddate);
            hashTable.Add("TherapyReasonCode", ARVTherapyCode);
            hashTable.Add("TherapyOther", OtherReason);

            //Referred To
            DataTable dataTableReferredTo = new DataTable();
            dataTableReferredTo = GetCheckBoxListcheckedIDs(PnlReferredTo, "referredToID", "referredToOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableReferredTo);

            //If Hospitalized # of Days
            hashTable.Add("numOfDaysHospitalized", txtNumOfDaysHospitalized.Text);
            //Nutritional Support
            hashTable.Add("nutritionalSupport", ddlNutritionalSupport.SelectedValue);
            //Infant Feeding Option
            hashTable.Add("infantFeedingOption", ddlInfantFeedingPractice.SelectedValue);

            //Positive Prevention At Risk Population
            DataTable dtatRiskPopulation = new DataTable();
            dtatRiskPopulation = GetCheckBoxListcheckedIDs(pnlriskpopulation, "RiskPopulationID", "RiskPopulationOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dtatRiskPopulation);

            //Positive Prevention At Risk Population Services
            DataTable dtatRiskPopulationService = new DataTable();
            dtatRiskPopulationService = GetCheckBoxListcheckedIDs(pnlriskpopulationservice, "PopulationServiceID", "PopulationServiceOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dtatRiskPopulationService);

            //Prevention with positives (PwP)
            DataTable dtPreventionPositive = new DataTable();
            dtPreventionPositive = GetCheckBoxListcheckedIDs(pnlprewithpositive, "pwpID", "pwpID_Other", 0);
            dataSetForSaving.Tables.Add(dtPreventionPositive);

            //Name of attending clinician
            hashTable.Add("attendingClinician", ddlattendingclinician.SelectedValue);

            hashTable.Add("Datenextappointment", txtdatenextappointment.Value);

            //CreateDate
            if (isUpdate)
            {
                hashTable.Add("createDate", ViewState["createDate"].ToString());
                hashTable.Add("visitID", Convert.ToInt32(ViewState["VisitID"]));
            }

            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.InitialFollowupVisits, (DataSet)ViewState["CustomFieldsDS"]);
            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.InitialFollowupVisits, (DataSet)ViewState["CustomFieldsDS"]);
            }

            DataSet dsreturn = _InitialFollowupVisit.SaveUpdateInitialFollowupVisitData(hashTable, dataSetForSaving, isUpdate, theCustomDataDT);
            Session["PatientVisitId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["visitID"].ToString());
            ViewState["VisitID"] = Session["PatientVisitId"];
            Session["ServiceLocationId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["LocationID"].ToString());
            DsBindData = _InitialFollowupVisit.GetInitialFollowupVisitInfo(patientID, locationID, Convert.ToInt32(ViewState["VisitID"]));
            if (DsBindData.Tables[0].Rows[0]["createDate"] != DBNull.Value)
                ViewState["createDate"] = DsBindData.Tables[0].Rows[0]["createDate"].ToString();
            hashTable.Clear();
            string script = string.Empty;

            if (button == "Pharmacy")
            {
                script = "<script language = 'javascript' defer ='defer' id = 'pharmOpen'>\n";
                if (ckb_AutoPopPresc.Checked)
                {
                    int cuttoffDate = (int)((DateTime.Parse(this.txtVisitDate.Text) - new DateTime(1, 1, 1)).TotalDays + 1);
                    script += "fnPageOpen('Pharmacy','1','" + cuttoffDate.ToString() + "');\n";
                }
                else script += "fnPageOpen('Pharmacy','0');\n";

                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pharmacy", script);
            }
            else if (button == "Labratory")
            {
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                script += "fnPageOpen('Labratory','0');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pharmacy", script);
            }
            else if (button == "LabTest")
            {
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                script += "fnPageOpen('LabTest','0');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pharmacy", script);
            }
            else if (button == "ICF")
            {
                Session["PatientVisitId"] = 0;
                Session["ServiceLocationId"] = 0;
                base.Session["FeatureID"] = buttonICF.CommandName;
                Session["LabId"] = 0;
                Response.Redirect("~/ClinicalForms/frmClinical_CustomForm.aspx");
            }
        }

        /// <summary>
        /// Shows the hide icf button.
        /// </summary>
        private void ShowHideICFButton()
        {
            buttonICF.Visible = false;
            DataSet dsFormsByForModule = new DataSet();
            if (ViewState["AddForms"] == null)
            {
                IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
                dsFormsByForModule = PatientHome.GetTechnicalAreaandFormName(ModuleId);
            }
            else
            {
                dsFormsByForModule = (DataSet)ViewState["AddForms"];
            }

            //var featureID =
            DataRow[] foundRows;
            foundRows = dsFormsByForModule.Tables[1].Select("FeatureName = 'Intensive Case Finding'");
            if (foundRows != null)
            {
                string featureID = foundRows[0]["FeatureID"].ToString();
                buttonICF.CommandName = featureID.ToString();
                buttonICF.Visible = true;
            }
        }

        /// <summary>
        /// Showhides the textbox.
        /// </summary>
        /// <param name="thePnl">The PNL.</param>
        private void ShowhideTextbox(Panel thePnl)
        {
            foreach (Control y in thePnl.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    if (y.ID.StartsWith("Pnl"))
                    {
                        ShowhideTextbox((System.Web.UI.WebControls.Panel)y);
                    }
                }
                else
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                        if (((CheckBox)y).Text.Contains("Other") && ((CheckBox)y).Checked == true)
                        {
                            //((CheckBox)y).Checked = true;
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((CheckBox)y).ID + ">\n";
                            script += "show('txt" + (((CheckBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "" + ((CheckBox)y).ID + "", script);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the custom fields values.
        /// </summary>
        private void UpdateCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            PatID = Convert.ToInt32(Request.QueryString["patientid"]);
            string sqlselect;
            string strdelete;
            Int32 visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            ICustomFields CustomFields;
            //  if (txtvisitDate.Text.ToString() != "")
            visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));
            if (ViewState["VisitID_add"] != null)
                visitID = Convert.ToInt32(ViewState["VisitID_add"]);

            if (sbValues.ToString().Trim() != "")
            {
                if (ViewState["CustomFieldsData"] != null)
                {
                    sbValues = sbValues.Remove(0, 1);
                    sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                    sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatID.ToString() + " and Visit_pk=" + visitID.ToString();
                }
                else
                {
                    sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                    sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";
                    ViewState["CustomFieldsData"] = 1;
                }

                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
                if (arl.Count != 0)
                {
                    int p = 0;
                    foreach (object obj in arl)
                    {
                        sqlselect = "";
                        strdelete = "";
                        if (obj.ToString() != "")
                        {
                            try
                            {
                                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + PatID.ToString() + " and LocationID=" + Session["AppLocationID"] + " and visit_pk=" + visitID;
                                icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

                                if (FieldValues[p].ToString() != "")
                                {
                                    string[] mValues = FieldValues[p].Split(new char[] { ',' });

                                    foreach (string str in mValues)
                                    {
                                        if (str.ToString() != "")
                                        {
                                            string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                            Int32 ispos = Convert.ToInt32(strtab.Length);
                                            Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

                                            sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,visit_pk,visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                            sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";

                                            icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                            if (icount == -1)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                CustomFields = null;
                            }
                        }
                        p += 1;
                    }
                }
            }
        }
    }
}