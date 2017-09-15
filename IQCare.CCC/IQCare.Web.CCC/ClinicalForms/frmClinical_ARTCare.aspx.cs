using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
namespace IQCare.Web.Clinical
{
    public partial class ARTCare : BasePage
    {
        private ArrayList arl = new ArrayList();
        private AuthenticationManager Authentiaction = new AuthenticationManager();
        private Hashtable htArtCareParameters;
        private int icount;
        private int PatID;
        private int PatientID, LocationID, visitPK = 0;
        private int PId;
        private StringBuilder sbParameter=new StringBuilder();
        private StringBuilder sbValues;
        private string strmultiselect;
        private String TableName = "";

        protected void btn_close_Click(object sender, EventArgs e)
        {
            string theUrl = "";
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Session["PatientId"].ToString());
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                theUrl = string.Format("{0}", "frmPatient_History.aspx");
            }

            Response.Redirect(theUrl);
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            IPatientARTCare ACManager;
            ACManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                visitPK = Convert.ToInt32(Session["PatientVisitId"]);

                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);
            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);
            }
            Hashtable htparam = ArtCareParameters();
            ACManager.Save_Update_ARTCare(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]), 0, theCustomDataDT);
            SaveCancel();
        }

        protected void btnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            //ViewState.Remove("MasterData");
            ViewState.Remove("ARVMasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimen' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void btnTransRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", Session["ARVMasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["TransSelectedData"]);
            //ViewState.Remove("MasterData");
            Session.Remove("ARVMasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimen1' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void DQ_Check_Click(object sender, EventArgs e)
        {
            if (Validate_Data_Quality() == false)
            {
                return;
            }
            string msg = DataQuality_Msg();
            if (msg.Length > 69)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }
            else
            {
                IPatientARTCare ACManager;
                ACManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
                LocationID = Convert.ToInt32(Session["AppLocationId"]);
                PatientID = Convert.ToInt32(Session["PatientId"]);
                DataTable theCustomDataDT = null;
                if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
                {
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);
                }
                else
                {
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);
                }
                Hashtable htparam = ArtCareParameters();
                ACManager.Save_Update_ARTCare(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]), 1, theCustomDataDT);
                SaveCancel();
            }
        }

        protected void Init_ARTCare()
        {
            IPatientARTCare IEManager;
            try
            {
                txtarttransdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtarttransdate.ClientID + "', '" + txtarttransdate.ClientID + "')");
                txtarttransdate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtanotherRegimendate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtanotherRegimendate.ClientID + "', '" + txtanotherRegimendate.ClientID + "')");
                txtanotherRegimendate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

                PId = Convert.ToInt32(Session["PatientId"]);

                if (Session["PatientSex"].ToString() == "Female" && Convert.ToDecimal(Session["PatientAge"]) > 12)
                {
                    divanothpreg.Visible = true;
                    spnthispreg.Visible = true;
                }
                else
                {
                    divanothpreg.Visible = false;
                    spnthispreg.Visible = false;
                }

                IEManager = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
                DataSet theDS = IEManager.GetPatientARTCare(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
                Session["ARVMasterData"] = theDS.Tables[1];
                ViewState["MasterData"] = theDS.Tables[1];
                ViewState["MasterARVData"] = theDS.Tables[1];
                if (theDS.Tables[4].Rows.Count > 0 && theDS.Tables[4].Rows[0]["Visit_Id"] != System.DBNull.Value)
                {
                    Session["PatientVisitId"] = theDS.Tables[4].Rows[0]["Visit_Id"].ToString();
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                }
                else
                    Session["PatientVisitId"] = 0;

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                    {
                        this.txtcohortmnth.Value = String.Format("{0:MMM}", theDS.Tables[0].Rows[0]["ARTStartDate"]).ToUpper();
                    }
                    if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                    {
                        this.txtcohortyear.Value = String.Format("{0:yyyy}", theDS.Tables[0].Rows[0]["ARTStartDate"]).ToUpper();
                    }

                    // ART Start at This Facility Section
                    if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                    {
                        this.txtthisRegimendate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["ARTStartDate"]);
                    }
                }
                if (theDS.Tables[8].Rows.Count > 0 && theDS.Tables[8].Rows[0]["FirstLineRegimen"] != System.DBNull.Value)
                {
                    txtthisregimen.Value = theDS.Tables[8].Rows[0]["FirstLineRegimen"].ToString();
                }
                if (theDS.Tables[3].Rows.Count > 0 && theDS.Tables[3].Rows[0]["weight"] != System.DBNull.Value)
                {
                    this.txtthiswght.Value = theDS.Tables[3].Rows[0]["weight"].ToString();
                }
                if (theDS.Tables[6].Rows.Count > 0 && theDS.Tables[6].Rows[0]["cd4"] != System.DBNull.Value)
                {
                    this.txtthisCD4.Value = theDS.Tables[6].Rows[0]["cd4"].ToString();
                }
                if (theDS.Tables[7].Rows.Count > 0 && theDS.Tables[7].Rows[0]["CD4Percent"] != System.DBNull.Value)
                {
                    this.txtthisCD4percent.Value = theDS.Tables[7].Rows[0]["cd4percent"].ToString();
                }
                if (theDS.Tables[5].Rows.Count > 0 && theDS.Tables[5].Rows[0]["pregnant"] != System.DBNull.Value)
                {
                    if (theDS.Tables[5].Rows[0]["pregnant"].ToString() == "1")
                    {
                        this.ddlpregthis.SelectedValue = "309";
                    }
                    else
                    {
                        this.ddlpregthis.SelectedValue = "311";
                    }
                }
                if (theDS.Tables[9].Rows.Count > 0 && theDS.Tables[9].Rows[0]["whostage"] != System.DBNull.Value)
                {
                    this.lstthisClinicalStage.SelectedValue = theDS.Tables[9].Rows[0]["whostage"].ToString();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////
                //Grid Substitution of ARVs
                grdSubsARVs.DataSource = theDS.Tables[12];
                grdSubsARVs.DataBind();

                /////////////////////////////////////////////////////////////////////////////////////////////

                //Grid Interuptions of ARVs
                grdInteruptions.DataSource = theDS.Tables[13];
                grdInteruptions.DataBind();
                //For Editing Records
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {

                    //Transfer in on ART Section
                    if (theDS.Tables[10].Rows.Count > 0)
                    {
                        if (theDS.Tables[10].Rows[0]["ARTTransferInDate"] != System.DBNull.Value)
                        {
                            string ARTTransDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[10].Rows[0]["ARTTransferInDate"]);
                            if (ARTTransDate.ToString() != "01-Jan-1900")
                            {
                                this.txtarttransdate.Value = ARTTransDate.ToString();
                            }
                        }
                        if (theDS.Tables[10].Rows[0]["LPTFTransferId"] != System.DBNull.Value)
                        {
                            this.ddlarttransferinfrom.SelectedValue = theDS.Tables[10].Rows[0]["LPTFTransferId"].ToString();
                        }
                        if (theDS.Tables[10].Rows[0]["PrevARVRegimen"] != System.DBNull.Value)
                        {
                            this.txttransferARVs.Value = theDS.Tables[10].Rows[0]["PrevARVRegimen"].ToString();
                            string[] thetransStrRegimen = txttransferARVs.Value.Split(new Char[] { '/' });
                            DataView theARVTransDV = new DataView(theDS.Tables[1]);
                            if (txttransferARVs.Value != "")
                            {
                                ViewState["SelectedData"] = OldRegimenList(thetransStrRegimen, theARVTransDV);
                            }
                        }
                    }
                    if (theDS.Tables[11].Rows.Count > 0)
                    {
                        // ART Start at another Facility Section
                        if ((theDS.Tables[11].Rows[0]["FirstLineRegStDate"] != System.DBNull.Value) || (theDS.Tables[11].Rows[0]["FirstLineRegStDate"].ToString() != "1900-01-01"))
                        {
                            string firsLineRegDate = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[11].Rows[0]["FirstLineRegStDate"]);
                            if (firsLineRegDate.ToString() != "01-Jan-1900")
                            {
                                this.txtanotherRegimendate.Value = firsLineRegDate.ToString();
                            }
                        }
                        // Add By Deepak
                        if (txtanotherRegimendate.Value != string.Empty)
                        {
                            this.txtcohortmnth.Value = String.Format("{0:MMM}", Convert.ToDateTime(txtanotherRegimendate.Value)).ToUpper();
                        }
                        if (txtanotherRegimendate.Value != string.Empty)
                        {
                            this.txtcohortyear.Value = String.Format("{0:yyyy}", Convert.ToDateTime(txtanotherRegimendate.Value)).ToUpper();
                        }
                        if (theDS.Tables[11].Rows[0]["FirstLineReg"] != System.DBNull.Value)
                        {
                            txtanotherregimen.Value = theDS.Tables[11].Rows[0]["FirstLineReg"].ToString();
                            string[] theStrRegimen = txtanotherregimen.Value.Split(new Char[] { '/' });
                            DataView theARVDV = new DataView(theDS.Tables[1]);
                            if (txtanotherregimen.Value != "")
                            {
                                ViewState["TransSelectedData"] = OldRegimenList(theStrRegimen, theARVDV);
                            }
                        }

                        if (theDS.Tables[11].Rows[0]["weight"] != System.DBNull.Value)
                        {
                            this.txtanotherwght.Value = theDS.Tables[11].Rows[0]["weight"].ToString();
                        }
                        if (theDS.Tables[11].Rows[0]["cd4"] != System.DBNull.Value)
                        {
                            this.txtanotherCD4.Value = theDS.Tables[11].Rows[0]["cd4"].ToString();
                        }
                        if (theDS.Tables[11].Rows[0]["cd4percent"] != System.DBNull.Value)
                        {
                            this.txtanotherCD4percent.Value = theDS.Tables[11].Rows[0]["cd4percent"].ToString();
                        }
                        if (theDS.Tables[11].Rows[0]["pregnant"] != System.DBNull.Value)
                        {
                            this.ddlpregnantanother.SelectedValue = theDS.Tables[11].Rows[0]["pregnant"].ToString();
                        }
                        if (theDS.Tables[11].Rows[0]["whostage"] != System.DBNull.Value)
                        {
                            this.lstanotherClinicalStage.SelectedValue = theDS.Tables[11].Rows[0]["whostage"].ToString();
                        }
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////
                    //if (theDS1.Tables[0].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS1.Tables[0].Rows[0]["DataQuality"]) == 1)
                    //{
                    //    btncomplete.CssClass = "greenbutton";
                    //}
                }
                FillOldData(PId);
            }
            // }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                IEManager = null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "ART Care";
            //(Master.FindControl("lblformname") as Label).Text = "ART Care";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART Care";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART Care";
            PutCustomControl();
            if (!IsPostBack)
            {
                Authenticate();
                BindDropdowns();
                Init_ARTCare();
            }
            else
            {
                if ((DataTable)Application["AddRegimen"] != null)
                {
                    ViewState["MasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");
                    DataTable theDT = (DataTable)Application["AddRegimen"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txttransferARVs.Value = theStr;
                    Application.Remove("AddRegimen");
                }
                else if ((DataTable)Application["AddRegimen1"] != null)
                {
                    Session["ARVMasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");
                    DataTable theDT = (DataTable)Application["AddRegimen1"];
                    ViewState["TransSelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtanotherregimen.Value = theStr;
                    Application.Remove("AddRegimen1");
                }
            }
        }

        // Function to Pass HashTable Parameters
        private Hashtable ArtCareParameters()
        {
            htArtCareParameters = new Hashtable();
            //IQCareUtils theUtils = new IQCareUtils();

            htArtCareParameters.Add("ARTTransferindate", txtarttransdate.Value);
            htArtCareParameters.Add("ARTTransferinfrom", ddlarttransferinfrom.SelectedValue);
            htArtCareParameters.Add("transferARVs", txttransferARVs.Value);
            htArtCareParameters.Add("AnotherRegimennStartdt", txtanotherRegimendate.Value);
            htArtCareParameters.Add("AotherRegimen", txtanotherregimen.Value);
            htArtCareParameters.Add("AnotherWeight", txtanotherwght.Value);
            htArtCareParameters.Add("AnotherClinicalStage", lstanotherClinicalStage.SelectedValue);
            htArtCareParameters.Add("AotherCD4", txtanotherCD4.Value);
            htArtCareParameters.Add("AnotherCD4Percent", txtanotherCD4percent.Value);
            htArtCareParameters.Add("pregnant", ddlpregnantanother.SelectedValue);
            return htArtCareParameters;
        }

        /******** Fill Old Regimen List **********/

        private void Authenticate()
        {
            /***************** Check For User Rights ****************/
            // AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTCare, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btn_print.Enabled = false;
            }
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTCare, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btn_save.Enabled = false;
                    DQ_Check.Enabled = false;
                }
            }
            else
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTCare, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "";
                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Session["HIVPatientStatus"].ToString());
                    theUrl = string.Format("{0}?sts={1}", "frmPatient_History.aspx", Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTCare, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btn_save.Enabled = false;
                    DQ_Check.Enabled = false;
                }
                if (Convert.ToString(Session["HIVPatientStatus"]) == "1")
                {
                    btn_save.Enabled = false;
                    DQ_Check.Enabled = false;
                }
                //Privilages for Care End
                if (Convert.ToString(Session["CareEndFlag"]) == "1")
                {
                    btn_save.Enabled = true;
                    DQ_Check.Enabled = true;
                }
            }
        }

        private void BindDropdowns()
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_LPTF"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_LPTF"]);
                theDV.RowFilter = "DeleteFlag = 0";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                    BindManager.BindCombo(ddlarttransferinfrom, theDT, "Name", "Id");
                }
            }
            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDVPreg = new DataView(theDS.Tables["Mst_Decode"]);
                theDVPreg.RowFilter = "CodeId=1006 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVPreg.Sort = "SRNo";
                if (theDVPreg.Table != null)
                {
                    DataTable theDTPreg = (DataTable)theUtils.CreateTableFromDataView(theDVPreg);

                    BindManager.BindCombo(ddlpregnantanother, theDTPreg, "Name", "Id");
                    BindManager.BindCombo(ddlpregthis, theDTPreg, "Name", "Id");
                }
                DataView theDVWHOStage = new DataView(theDS.Tables["Mst_Decode"]);
                theDVWHOStage.RowFilter = "CodeId=22 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVWHOStage.Sort = "SRNo";
                if (theDVWHOStage.Table != null)
                {
                    DataTable theDTWHOStage = (DataTable)theUtils.CreateTableFromDataView(theDVWHOStage);

                    BindManager.BindCombo(lstanotherClinicalStage, theDTWHOStage, "Name", "Id");
                    BindManager.BindCombo(lstthisClinicalStage, theDTWHOStage, "Name", "Id");
                }
            }
        }

        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }

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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitPK, 0, 0, Convert.ToInt32(ApplicationAccess.ARTCare));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "ARTCare");
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

        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            DataView theDV = new DataView();
            if (theDT.Rows.Count != 0)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DataTable theFilDT = (DataTable)ViewState["MasterARVData"];
                    if (theFilDT.Rows.Count > 0)
                    {
                        theDV = new DataView(theFilDT);
                        theDV.RowFilter = "DrugId = " + theDT.Rows[i]["DrugId"]; //+ " and DrugTypeID = 37";
                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[j]["Abbr"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                                }
                            }
                        }
                    }
                    theRegimen = theRegimen.Trim();
                }
            }
            return theRegimen;
        }

        private void GenerateCustomFieldsValues(Control Cntrl)
        {
            string pnlName = Cntrl.ID;
            sbValues = new StringBuilder();
            strmultiselect = string.Empty;
            string strfName = string.Empty;
            Boolean radioflag = false;

            Int32 stpos = 0;
            Int32 enpos = 0;
            if (ViewState["CustomFieldsData"] != null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
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
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
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
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
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
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
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
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
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
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
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
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
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

        private void InsertCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            string sqlselect;
            Int32 visitID = 0;
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

        private DataTable OldRegimenList(string[] str, DataView theDV)
        {
            DataTable theDT = CreateSelectedTable();
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];
                    theDR[3] = theDV[0][3];
                    theDR[4] = theDV[0][4];

                    DataRow theTempeDR;
                    theTempeDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempeDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                }
            }
            return theDT;
        }

        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.ARTCare));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "ARTCare");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "ARTCare");
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

        private void SaveCancel()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);

            //string strPatientID = ViewState["PtnID"].ToString();
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('ART Care Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            if (Session["Redirect"].ToString() == "0")
            {
                script += "window.location.href='frmPatient_Home.aspx?PatientId=" + PatientID + "';\n";
            }
            else
            {
                script += "window.location.href='frmPatient_History.aspx?PatientId=" + PatientID + "';\n";
            }
            script += "}\n";
            script += "else \n";
            script += "{\n";
            //script += "window.location.href('frmClinical_ARTFollowup.aspx?name=Edit&PatientId=" + PatientID + "&visitid=" + visitPK + "&LocationId=" + locationid + "&sts=" + Request.QueryString["sts"].ToString() + "');\n";
            script += "window.location.href='frmClinical_ARTCare.aspx'\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

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

        private Boolean Validate_Data_Quality()
        {

            return true;
        }

       
        private string DataQuality_Msg()
        {
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";

            return strmsg;
        }

        
    }
}