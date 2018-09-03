using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Interface.Clinical;
using Application.Presentation;
using System.Data;
using Interface.SCM;
using System.Drawing;
using AjaxControlToolkit;
using System.Web.Script.Serialization;
using Application.Common;
using System.Text;
using System.Web.Services;

namespace IQCare.Web.PMSCM
{
    public partial class frmPharmacyDispense_PatientOrder : LogPage
    {
        IDrug PrescriptionManager;
        StringBuilder str = new StringBuilder();
        private static int chkavdrugs = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
            //Above line added by Rahmat on (22-Mar-17) form stoping data save more than 1 time.
            PrescriptionManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");

            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Dispensing >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Current Order";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Dispense";

            LoadPendingPharmacyOrders();

            if (!IsPostBack)
            {
                hdnPanelStatus.Value = "C";
                if (Request.QueryString["opento"] == "ArtForm")
                {
                    if (Convert.ToInt32(Session["TherapyPlan"]) != 95)
                    {
                        btnPriorPrescription.Visible = false;
                        if (Convert.ToInt32(Request.QueryString["TherapyPlan"]) == 95)
                        {
                            btnPriorPrescription.Visible = true;
                        }
                    }
                    Session["PatientVisitId"] = 0;
                    Session["ptnPharmacyPK"] = 0;
                }
                else if (Request.QueryString["sts"] == "0")
                {
                    Session["PatientVisitId"] = 0;
                    Session["ptnPharmacyPK"] = 0;
                }

                btnPrintLabel.Visible = false;
                PopulatePharmacyVitalDetails();
                addAttributes();

                SetSCMPaperlesssMode();

                gvDispenseDrugs.DataSource = "";
                gvDispenseDrugs.DataBind();
                int VisitID = Convert.ToInt32(Session["PatientVisitID"]);
                LoadPendingPharmacyOrderDetails(VisitID);
                try
                {
                    ddlDispensingStore.SelectedValue = Session["StoreID"].ToString();
                }
                catch { }



                if (Session["StoreID"] == null || Session["StoreID"].ToString() == "" || Convert.ToInt16(Session["StoreID"]) == 0)
                {
                    HttpContext.Current.Session["StoreID"] = 0;
                    //Ken 04-May-2015 - comented out
                    //HttpContext.Current.Session["DrugList"] = PrescriptionManager.GetPharmacyDrugList_Web(0, Session["SCMModule"] == null ? "" : Session["SCMModule"].ToString(), Session["TechnicalAreaId"].ToString());
                }

                HttpContext.Current.Session["PartialDispense"] = 0;
                Authenticate();
                //txtDispenseDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //ddlPrescribedBy.SelectedValue = Session["AppUserEmployeeId"].ToString();
                //ddlDispensedBy.SelectedValue = Session["AppUserEmployeeId"].ToString();
                BindDropdownSignature(Session["AppUserEmployeeId"].ToString());
                if (ddlDispensedBy.SelectedValue == "0")
                {
                    if (str.ToString() != "")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "DispenseByNotSelected", str.ToString(), true);
                }
            }

            setValidatorsBasedOntechnicalArea();
            JavaScriptFunctionsOnLoad();
            resizeScreen();

        }

        private void setValidatorsBasedOntechnicalArea()
        {
            //Jayant - 01/05/2015
            if (Convert.ToInt16(Session["Paperless"]) == 1)
            {
                if (Convert.ToInt32(Session["TechnicalAreaId"]) != 201)
                {
                    if (Convert.ToInt32(Session["PatientVisitID"]) == 0)
                    {
                        //RequiredFieldValidatorDateDispensed.Enabled = false;
                        //RequiredFieldValidatorDispBy.Enabled = false;

                        lblDispenseDate.CssClass = "";
                        lblDispensedBy.CssClass = "";
                        if (gvDispenseDrugs.Columns[11].HeaderText == "Qty Disp")
                            gvDispenseDrugs.Columns[11].Visible = false;
                        else if (gvDispenseDrugs.Columns[12].HeaderText == "Qty Disp")
                            gvDispenseDrugs.Columns[12].Visible = false;
                        ddlDispensedBy.Enabled = false;
                        txtDispenseDate.Disabled = true;
                        btnFullyDispensed.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "disableDateImg", "disbleDateImage();", true);
                    }
                    else if (Session["typeOfDispense"] != null)
                    {
                        if (Session["typeOfDispense"].ToString() == "New Order")
                        {
                            //RequiredFieldValidatorDateDispensed.Enabled = false;
                            //RequiredFieldValidatorDispBy.Enabled = false;
                            lblDispenseDate.CssClass = "";
                            lblDispensedBy.CssClass = "";
                        }
                    }

                }
                //RequiredFieldValidatorDispenStore.Enabled = false;
            }

            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 201)
            {
                //RequiredFieldValidatorDispenStore.Enabled = false;
            }

            if (Convert.ToInt32(Session["PatientVisitID"]) > 0)
            {
                if (Session["typeOfDispense"] != null)
                {
                    if (Session["typeOfDispense"].ToString() != "New Order")
                    {
                        txtDrug.Enabled = false;
                    }
                }
            }
        }

        private void SetSCMPaperlesssMode()
        {
            //Jayant-01/05/2015
            bool SCMOn = false;
            bool Paperless = false;


            if (Convert.ToInt16(Session["Paperless"]) == 1)
                Paperless = true;
            else
                Paperless = false;

            if (Session["SCMModule"] != null)
                SCMOn = true;
            else
                SCMOn = false;

            if (!Paperless && SCMOn)
            {
                //ddlDispensingStore.CssClass = "";
                //lblDispensingStoreLabel.CssClass = "";
                btnFullyDispensed.Visible = false;
                //RequiredFieldValidatorDispenStore.Enabled = false;
            }
            else if (Paperless && SCMOn)
            {
                //show columns
                gvDispenseDrugs.Columns[1].HeaderStyle.CssClass = ""; //Unit
                gvDispenseDrugs.Columns[1].ItemStyle.CssClass = "";
                gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = ""; //Batch No
                gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "";
                gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = ""; //Expiry date
                gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "";


            }
            else if (!Paperless && !SCMOn)
            {
                ddlDispensingStore.CssClass = "hidden";
                lblDispensingStoreLabel.CssClass = "hidden";
                btnPrintLabels.CssClass = "hidden";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Prescription";

                //Hide columns
                gvDispenseDrugs.Columns[1].HeaderStyle.CssClass = "hidden"; //Unit
                gvDispenseDrugs.Columns[1].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = "hidden"; //Batch No
                gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = "hidden"; //Expiry date
                gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "hidden";

                btnFullyDispensed.Visible = false;
                //RequiredFieldValidatorDispenStore.Enabled = false;
            }
            else if (!SCMOn)
            {
                ddlDispensingStore.CssClass = "hidden";
                lblDispensingStoreLabel.CssClass = "hidden";
                btnPrintLabels.CssClass = "hidden";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Prescription";

                //Hide columns
                gvDispenseDrugs.Columns[1].HeaderStyle.CssClass = "hidden"; //Unit
                gvDispenseDrugs.Columns[1].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = "hidden"; //Batch No
                gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = "hidden"; //Expiry date
                gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "hidden";
                //RequiredFieldValidatorDispenStore.Enabled = false;
            }


            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 201) //Clinical
            {
                if (SCMOn && !Paperless)
                {
                    //ddlDispensingStore.CssClass = "";
                    lblDispensingStoreLabel.CssClass = "";
                    //Hide columns
                    gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = ""; //Batch No
                    gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "";
                    gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = ""; //Expiry date
                    gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "";
                }
                else if (SCMOn && Paperless)
                {
                    if (Convert.ToInt32(Session["PatientVisitID"]) > 0)
                    {
                        //ddlDispensingStore.CssClass = "";
                        lblDispensingStoreLabel.CssClass = "";
                        //Hide columns
                        gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = ""; //Batch No
                        gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "";
                        gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = ""; //Expiry date
                        gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "";
                    }
                }
                else
                {
                    ddlDispensingStore.CssClass = "hidden";
                    lblDispensingStoreLabel.CssClass = "hidden";
                    //Hide columns
                    gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = "hidden"; //Batch No
                    gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "hidden";
                    gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = "hidden"; //Expiry date
                    gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "hidden";
                }

                Control myControl1 = (Master.FindControl("mainMaster") as Control);

                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = true;
                
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = false;

            }
            else //Pharmacy
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = true;
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("thePnlIdent") as Panel).Visible = false;
                //(Master.FindControl("facilityBanner") as Control).Visible = false;
                //(Master.FindControl("patientBanner") as Control).Visible = false;
                //(Master.FindControl("username1") as Control).Visible = false;
                //(Master.FindControl("currentdate1") as Control).Visible = false;
                //(Master.FindControl("facilityName") as Control).Visible = false;
                //(Master.FindControl("imageFlipLevel2") as Control).Visible = false;
            }
        }

        private void LoadPendingPharmacyOrders()
        {
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataTable theDT = thePharmacyManager.GetPharmacyExistingRecord(Convert.ToInt32(Session["PatientID"]), Convert.ToInt32(Session["StoreID"] == null ? "0" : Session["StoreID"].ToString()));
            gvPendingorders.DataSource = theDT;
            gvPendingorders.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //  this.SetStyle();
        }

        private void PopulateGrid(DataRow SelectedDrug)
        {
            DataTable dt = GetGridData();

            //Add the new data to datatable
            DataRow dr = dt.NewRow();
            dr["orderId"] = 0;
            dr["StoreId"] = 0;
            dr["DrugId"] = SelectedDrug["Drug_pk"].ToString();
            dr["DrugName"] = SelectedDrug["DrugName"].ToString();
            dr["Unit"] = SelectedDrug["DispensingUnit"].ToString();
            dr["BatchNo"] = string.Empty;
            dr["BatchId"] = string.Empty;
            dr["BatchQty"] = SelectedDrug["BatchQty"].ToString();
            dr["SellingPrice"] = SelectedDrug["SellingPrice"].ToString();
            dr["BatchQty"] = SelectedDrug["BatchQty"].ToString();
            dr["ExpiryDate"] = Convert.ToDateTime(SelectedDrug["ExpiryDate"]).ToString("dd-MMM-yyyy");
            dr["Morning"] = SelectedDrug["MorDose"].ToString();
            dr["Midday"] = SelectedDrug["MidDose"].ToString();
            dr["Evening"] = SelectedDrug["EvenDose"].ToString();
            dr["Night"] = SelectedDrug["NightDose"].ToString();
            dr["Duration"] = string.Empty;
            dr["PillCount"] = string.Empty;
            dr["QtyPrescribed"] = string.Empty;
            dr["QtyDispensed"] = string.Empty;
            dr["Comments"] = string.Empty;
            dr["Prophylaxis"] = "0";
            dr["Instructions"] = SelectedDrug["ItemInstructions"].ToString();
            dr["DispensingUnitId"] = SelectedDrug["DispensingId"].ToString();
            dr["PrintPrescriptionStatus"] = string.Empty;
            dr["QtyUnitDisp"] = SelectedDrug["QtyUnitDisp"].ToString();
            dr["syrup"] = SelectedDrug["syrup"].ToString();
            dr["UserID"] = string.Empty;
            dr["GenericAbbrevation"] = SelectedDrug["GenericAbbrevation"].ToString();
            dt.Rows.Add(dr);

            //Populate grid
            gvDispenseDrugs.DataSource = dt;
            gvDispenseDrugs.DataBind();

            //Fill Batch in Gridview Dropdown(RAHMAT 27-Mar-2017)
            HttpContext.Current.Session["StoreID"] = ddlDispensingStore.SelectedValue;
            foreach (GridViewRow gvrow in gvDispenseDrugs.Rows)
            {
                int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvrow.RowIndex].Value);
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));
                DropDownList ddlBatchNo = (DropDownList)gvrow.FindControl("ddlBatchNo");
                Label lblExpiryDate = (Label)gvrow.FindControl("lblExpiryDate");
                HiddenField hBatchQty = (HiddenField)gvrow.FindControl("hBatchQty");

                ddlBatchNo.DataSource = dsBatches;
                ddlBatchNo.DataTextField = "BatchQty";
                ddlBatchNo.DataValueField = "BatchID";
                ddlBatchNo.DataBind();
                if (ddlBatchNo.Items.Count > 0)
                {
                    int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                    var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                      where Convert.ToInt32(tmp["Batchid"]) == batchID
                                      select tmp["ExpiryDate"]).FirstOrDefault();
                    lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");

                    var BatchQty = (from DataRow tmp in dsBatches.Tables[0].Rows
                                    where Convert.ToInt32(tmp["Batchid"]) == batchID
                                    select tmp["BatchQty"]).FirstOrDefault();//quantity replace by BatchQty (Rahmat: 10Feb2018)
                    hBatchQty.Value = BatchQty.ToString();
                }
                else
                {
                    lblExpiryDate.Text = "";
                    hBatchQty.Value = "0";
                }
            }
            Session["StoreID"] = ddlDispensingStore.SelectedValue.ToString();
        }

        void SetStyle()
        {
            HtmlGenericControl facilityBanner = (Master.FindControl("facilityBanner") as HtmlGenericControl);
            if (facilityBanner != null) facilityBanner.Style.Add("display", "inline");

            HtmlGenericControl patientBanner = (Master.FindControl("patientBanner") as HtmlGenericControl);
            if (patientBanner != null) patientBanner.Style.Add("display", "none");
            HtmlGenericControl username1 = (Master.FindControl("username1") as HtmlGenericControl);
            if (username1 != null)
                username1.Attributes["class"] = "usernameLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl currentdate1 = (Master.FindControl("currentdate1") as HtmlGenericControl);
            if (currentdate1 != null) currentdate1.Attributes["class"] = "currentdateLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl facilityName = (Master.FindControl("facilityName") as HtmlGenericControl);
            if (facilityName != null) facilityName.Attributes["class"] = "facilityLevel1"; //Style.Add("display", "inline");
            //userNameLevel2.Style.Add("display", "none");
            //currentDateLevel2.Style.Add("display", "none");
            HtmlGenericControl imageFlipLevel2 = (Master.FindControl("imageFlipLevel2") as HtmlGenericControl);
            if (imageFlipLevel2 != null) imageFlipLevel2.Style.Add("display", "none");
            //facilityLevel2.Style.Add("display", "none");
            HtmlGenericControl level2Navigation = (Master.FindControl("level2Navigation") as HtmlGenericControl);
            if (level2Navigation != null) level2Navigation.Style.Add("display", "none");

            //Menu patientLevel = ((this.Master.FindControl("level2Navigation") as UserControl).FindControl("patientLevelMenu") as Menu);
            //if (patientLevel != null) patientLevel.Style.Add("display", "none");
            //HtmlGenericControl alerts = (Master.FindControl("level2Navigation").FindControl("UserControl_Alerts1") as HtmlGenericControl);
            //if (alerts != null) alerts.Style.Add("display", "inline");
        }

        private void PopulatePharmacyVitalDetails()
        {
            PrescriptionManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug,BusinessProcess.SCM");

            DataSet dsPharmacyVitals = PrescriptionManager.GetPharmacyVitals(Convert.ToInt32(Session["PatientID"]));

            txtStartWeight.Text = dsPharmacyVitals.Tables[0].Rows[0]["StartWeight"].ToString();
            txtStartheight.Text = dsPharmacyVitals.Tables[0].Rows[0]["StartHeight"].ToString();
            txtCurrentWeight.Text = dsPharmacyVitals.Tables[0].Rows[0]["CurrentWeight"].ToString();
            txtCurrentHeight.Text = dsPharmacyVitals.Tables[0].Rows[0]["Currentheight"].ToString();

            txtStartReg.Text = dsPharmacyVitals.Tables[1].Rows[0]["StartRegimen"].ToString();
            txtStartRegLine.Text = dsPharmacyVitals.Tables[1].Rows[0]["StartRegimenLine"].ToString();
            txtStartRegDate.Text = dsPharmacyVitals.Tables[1].Rows[0]["StartRegimenDate"].ToString();

            txtLastReg.Text = dsPharmacyVitals.Tables[2].Rows[0]["LastRegimen"].ToString();
            txtLastRegLine.Text = dsPharmacyVitals.Tables[2].Rows[0]["LastRegimenLine"].ToString();

            if (dsPharmacyVitals.Tables[3].Rows[0]["OnTBtreatment"].ToString() == "Yes")
            {
                rbOnTBTreatment.Items[0].Selected = true;
                lblIPTStartDate.Text = "TB Rx Start Date:";
                lblIPTEndDate.Text = "TB Rx End Date:";

                txtIPTStartDate.Text = dsPharmacyVitals.Tables[3].Rows[0]["TBRegimenStartDate"].ToString();
                txtIPTEndDate.Text = dsPharmacyVitals.Tables[3].Rows[0]["TBRegimenEndDate"].ToString();
            }
            else
            {
                rbOnTBTreatment.Items[1].Selected = true;
                lblIPTStartDate.Text = "IPT Start Date:";
                lblIPTEndDate.Text = "IPT End Date:";
                if (!string.IsNullOrEmpty(dsPharmacyVitals.Tables[4].Rows[0]["INHStartDate"].ToString()))
                {
                    txtIPTStartDate.Text = Convert.ToDateTime(dsPharmacyVitals.Tables[4].Rows[0]["INHStartDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrEmpty(dsPharmacyVitals.Tables[4].Rows[0]["INHEndDate"].ToString()))
                {
                    txtIPTEndDate.Text = Convert.ToDateTime(dsPharmacyVitals.Tables[4].Rows[0]["INHEndDate"].ToString()).ToString("dd-MMM-yyyy");
                }
            }

            if (dsPharmacyVitals.Tables[5].Rows.Count > 0)
            {
                txtPreviousApptDate.Text = dsPharmacyVitals.Tables[5].Rows[0]["AppDate"].ToString() == "01 Jan 1900" ? "" : dsPharmacyVitals.Tables[5].Rows[0]["AppDate"].ToString();
                txtDaysToPreviousAppt.Text = dsPharmacyVitals.Tables[5].Rows[0]["DaysToNextAppointment"].ToString();
            }

            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataTable theDT = new DataTable();

            theDT = dsPharmacyVitals.Tables[6];
            BindManager.BindCombo(ddlTreatmentProg, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[7];
            BindManager.BindCombo(ddlTreatmentPlan, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[8];
            BindManager.BindCombo(ddlWHOStage, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[9];
            BindManager.BindCombo(ddlDispensingStore, theDT, "Name", "id");
            ddlDispensingStore.SelectedIndex = 1;
            HttpContext.Current.Session["StoreID"] = ddlDispensingStore.SelectedValue;

            theDT = dsPharmacyVitals.Tables[10];
            //BindManager.BindCombo(ddlPrescribedBy, theDT, "Name", "EmployeeId");
            //BindManager.BindCombo(ddlDispensedBy, theDT, "Name", "EmployeeId");

            //BindUserDropdown(ddlPrescribedBy, string.Empty);
            //BindUserDropdown(ddlDispensedBy, string.Empty);

            BindUserDropdown(ddlPrescribedBy, Session["AppUserId"].ToString());

            BindUserDropdown(ddlDispensedBy, Session["AppUserId"].ToString());
            if (Convert.ToInt16(Session["Paperless"]) == 1)
            {

                ddlDispensedBy.SelectedValue = "0";

            }
            else { ddlDispensedBy.SelectedValue = Session["AppUserId"].ToString(); }

            //comented out by VY
            //if (dsPharmacyVitals.Tables[11].Rows.Count > 0) ddlTreatmentProg.SelectedValue = dsPharmacyVitals.Tables[11].Rows[0]["id"].ToString();
            if (dsPharmacyVitals.Tables[12].Rows.Count > 0) ddlTreatmentPlan.SelectedValue = dsPharmacyVitals.Tables[12].Rows[0]["id"].ToString();
            if (Session["WHOStage"] != null)
                ddlWHOStage.SelectedValue = Convert.ToString(Session["WHOStage"] == null ? "0" : Session["WHOStage"]);
            else
            {
                if (dsPharmacyVitals.Tables[13].Rows.Count > 0) ddlWHOStage.SelectedValue = dsPharmacyVitals.Tables[13].Rows[0]["id"].ToString();
            }

            /*Patient Classification  */
            theDT = dsPharmacyVitals.Tables[14];
            BindManager.BindCombo(ddlPtnClassification, theDT, "NAME", "ID");

            if (dsPharmacyVitals.Tables[15].Rows.Count > 0)
            {
                DataTable dtRow = new DataTable();
                dtRow = dsPharmacyVitals.Tables[15];

                this.ddlPtnClassification.SelectedValue = dtRow.Rows[0]["PatientClassification"].ToString();
                //mgt.IsEnrolDifferenciatedCare = string.IsNullOrEmpty(dtRow.Rows[0]["IsEnrolDifferenciatedCare"].ToString()) == false ? Convert.ToInt32(dtRow.Rows[0]["IsEnrolDifferenciatedCare"]) : 0;
            }

        }

        private void addAttributes()
        {
            txtCurrentWeight.Attributes.Add("onkeyup", "chkNumeric('" + txtCurrentWeight.ClientID + "')");
            txtCurrentWeight.Attributes.Add("OnBlur", "CalculateBSA('" + txtCurrentWeight.ClientID + "','" + txtCurrentHeight.ClientID + "','" + txtCurrentBSA.ClientID + "');");

            txtCurrentHeight.Attributes.Add("onkeyup", "chkNumeric('" + txtCurrentHeight.ClientID + "')");
            txtCurrentHeight.Attributes.Add("OnBlur", "CalculateBSA('" + txtCurrentWeight.ClientID + "','" + txtCurrentHeight.ClientID + "','" + txtCurrentBSA.ClientID + "');");

            txtprescriptionDate.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtprescriptionDate.ClientID + "', '" + txtprescriptionDate.ClientID + "');");
            txtDispenseDate.Attributes.Add("onblur", "isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtDispenseDate.ClientID + "', '" + txtDispenseDate.ClientID + "');");

            txtIPTStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtIPTStartDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtIPTStartDate.ClientID + "', '" + txtIPTStartDate.ClientID + "');");
            txtIPTEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtIPTEndDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtIPTEndDate.ClientID + "', '" + txtIPTEndDate.ClientID + "');");

            txtNextApptDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtNextApptDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + txtNextApptDate.ClientID + "', '" + Application["AppCurrentDate"] + "', '" + txtNextApptDate.ClientID + "');");

            txtDaysToNextAppt.Attributes.Add("onkeyup", "chkNumeric('" + txtDaysToNextAppt.ClientID + "')");
            txtDaysToNextAppt.Attributes.Add("OnBlur", "CalculateNextAppointment('" + txtNextApptDate.ClientID + "','" + txtDaysToNextAppt.ClientID + "');");
        }

        private void JavaScriptFunctionsOnLoad()
        {
            string script = "CalculateBSA('" + txtCurrentWeight.ClientID + "','" + txtCurrentHeight.ClientID + "','" + txtCurrentBSA.ClientID + "'); ";
            script = script + "CalculateBSA('" + txtStartWeight.ClientID + "','" + txtStartheight.ClientID + "','" + txtStartBSA.ClientID + "');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BSA", script, true);
        }

        protected void txtNextApptDate_TextChanged(object sender, EventArgs e)
        {
            DateTime NextAppDate = Convert.ToDateTime(txtNextApptDate.Text);
            txtDaysToNextAppt.Text = (NextAppDate - DateTime.Now).TotalDays.ToString();
        }

        protected void gvDispenseDrugs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["DrugId"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["DrugId"]);
                int QtyUnitDisp = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["QtyUnitDisp"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["QtyUnitDisp"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["QtyUnitDisp"]);
                int syrup = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["syrup"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["syrup"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["syrup"]);
                int StoreId = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["StoreId"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["StoreId"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["StoreId"]);

                //Ken 04-May-2015 - Load batch details from the DB
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                //DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));                
                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, StoreId);
                //

                DropDownList ddlBatchNo = (DropDownList)e.Row.FindControl("ddlBatchNo");
                ddlBatchNo.DataSource = dsBatches.Tables[0];
                ddlBatchNo.DataTextField = "BatchQty";
                ddlBatchNo.DataValueField = "BatchID";
                ddlBatchNo.DataBind();

                TextBox txtMorning = (TextBox)e.Row.FindControl("txtMorning");
                TextBox txtMidday = (TextBox)e.Row.FindControl("txtMidday");
                TextBox txtEvening = (TextBox)e.Row.FindControl("txtEvening");
                TextBox txtNight = (TextBox)e.Row.FindControl("txtNight");
                TextBox txtDuration = (TextBox)e.Row.FindControl("txtDuration");
                TextBox txtQtyPrescribed = (TextBox)e.Row.FindControl("txtQtyPrescribed");
                TextBox txtPillCount = (TextBox)e.Row.FindControl("txtPillCount");
                TextBox txtQtyDispensed = (TextBox)e.Row.FindControl("txtQtyDispensed");
                TextBox txtRefillQty1 = (TextBox)e.Row.FindControl("txtRefillQty1");
                ImageButton delete = (ImageButton)e.Row.FindControl("DeleteButton");

                txtMorning.MaxLength = 4;
                txtMidday.MaxLength = 4;
                txtEvening.MaxLength = 4;
                txtNight.MaxLength = 4;
                txtDuration.MaxLength = 4;
                txtQtyPrescribed.MaxLength = 4;
                txtPillCount.MaxLength = 4;
                txtQtyDispensed.MaxLength = 4;
                txtRefillQty1.MaxLength = 4;

                Label lblExpiryDate = (Label)e.Row.FindControl("lblExpiryDate");
                HiddenField hBatchQty = (HiddenField)e.Row.FindControl("hBatchQty");
                txtMorning.Attributes.Add("onkeyup", "chkDecimal('" + txtMorning.ClientID + "');");
                txtMidday.Attributes.Add("onkeyup", "chkDecimal('" + txtMidday.ClientID + "');");
                txtEvening.Attributes.Add("onkeyup", "chkDecimal('" + txtEvening.ClientID + "');");
                txtNight.Attributes.Add("onkeyup", "chkDecimal('" + txtNight.ClientID + "');");
                txtQtyPrescribed.Attributes.Add("onkeyup", "chkDecimal('" + txtQtyPrescribed.ClientID + "');");
                txtDuration.Attributes.Add("onkeyup", "chkDecimal('" + txtDuration.ClientID + "');");
                txtPillCount.Attributes.Add("onkeyup", "chkDecimal('" + txtPillCount.ClientID + "');");
                txtQtyDispensed.Attributes.Add("onkeyup", "chkDecimal('" + txtQtyDispensed.ClientID + "');");
                //txtRefillQty.Attributes.Add("onkeyup", "chkDecimal('" + txtRefillQty.ClientID + "');");
                txtDuration.Attributes.Add("OnBlur", "CalculateDrugsPrescribed('" + txtMorning.ClientID + "','" + txtMidday.ClientID + "','" + txtEvening.ClientID + "','" + txtNight.ClientID + "','" + txtDuration.ClientID + "','" + txtQtyPrescribed.ClientID + "','" + syrup + "','" + QtyUnitDisp + "');");
                txtQtyDispensed.Attributes.Add("OnBlur", "chkQtyDispGreaterQtyPres('" + txtQtyPrescribed.ClientID + "','" + txtQtyDispensed.ClientID + "','" + txtRefillQty1.ClientID + "');");
                //txtRefillQty.Attributes.Add("OnBlur", "chkQtyDispGreaterQtyPres('" + txtQtyPrescribed.ClientID + "','" + txtQtyDispensed.ClientID + "','" + txtRefillQty.ClientID + "');");


                str.Append("DispenseBySelect('" + ddlDispensedBy.ClientID + "', '" + txtQtyDispensed.ClientID + "', '" + txtRefillQty1.ClientID + "');");
                ddlDispensedBy.Attributes.Add("OnChange", str.ToString());

                //set the expiry date
                if (ddlBatchNo.Items.Count > 0)
                {
                    int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                    var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                      where Convert.ToInt32(tmp["Batchid"]) == batchID
                                      select tmp["ExpiryDate"]).FirstOrDefault();
                    var BatchQty = (from DataRow tmp in dsBatches.Tables[0].Rows
                                    where Convert.ToInt32(tmp["Batchid"]) == batchID
                                    select tmp["Quantity"]).FirstOrDefault();
                                    //select tmp["BatchQty"]).FirstOrDefault();

                    lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");
                    hBatchQty.Value = BatchQty.ToString();
                }
                else
                {
                    lblExpiryDate.Text = "";
                    hBatchQty.Value = "0";
                }


                RangeValidator rng = e.Row.FindControl("RangeValidatorQtyDisp") as RangeValidator;
                if (rng != null)
                {
                    //if (Convert.ToInt16(Session["Paperless"]) != 1)
                    //if (RequiredFieldValidatorDispBy.Enabled == true)
                    //{
                    //    rng.Enabled = true;
                    //}
                }

                RequiredFieldValidator rfv = e.Row.FindControl("RequiredFieldValidatorQtyDisp") as RequiredFieldValidator;
                if (rfv != null)
                {
                    //if (Convert.ToInt16(Session["Paperless"]) != 1)
                    //if (RequiredFieldValidatorDispBy.Enabled == true)
                    //{
                    //    rfv.Enabled = true;
                    //}
                }

                if (Session["typeOfDispense"] != null)
                {
                    if (Session["typeOfDispense"].ToString() == "Partial Dispense" || Session["typeOfDispense"].ToString() == "Already Dispensed Order")
                    {
                        delete.Visible = false;
                    }
                    else if (gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["UserID"].ToString() != Session["AppUserId"].ToString() && gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["UserID"].ToString() != string.Empty && gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["UserID"].ToString() != "0")
                    {
                        delete.Visible = false;
                        //gvDispenseDrugs.Rows[e.Row.RowIndex].Cells[16].ControlStyle.CssClass = "hidden";
                    }
                }
            }
        }
        void DecimalText_Load(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.MaxLength = 3;
            tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchDrugs(string prefixText, int count)
        {
            List<string> Drugsdetail = new List<string>();
            List<Drugs> dtDrugList = GetDrugs(prefixText, count);

            var drugList = from Drugs tmp in dtDrugList.AsEnumerable()
                           where tmp.DrugName.ToString().ToLower().Contains(prefixText.ToLower())
                           select tmp;

            foreach (Drugs c in drugList)
            {
                Drugsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.DrugName.ToString(), c.DrugId.ToString()));
            }

            return Drugsdetail;
        }

        public static List<Drugs> GetDrugs(string prefixText, int count)
        {
            List<Drugs> items = new List<Drugs>();
            IDrug objRptFields;
            objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug,BusinessProcess.SCM");
            string sqlQuery = "";
            DataTable dataTable;
            //creating Sql Query
            sqlQuery = returnquery(prefixText);

            dataTable = objRptFields.ReturnDatatableQuery(sqlQuery);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataTable distinctTable = dataTable.AsEnumerable()
                           .GroupBy(x => x.Field<string>("DrugName"))
                           .Select(g => g.First()).CopyToDataTable();
                if (distinctTable.Rows.Count > 0)
                {
                    foreach (DataRow row in distinctTable.Rows)
                    {
                        try
                        {
                            Drugs item = new Drugs();
                            item.DrugId = (int)row["Drug_pk"];
                            item.DrugName = (string)row["DrugName"];
                            item.AvlQty = Convert.ToInt32(row["QTY"].ToString());
                            items.Add(item);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return items;
        }
        private static string returnquery(string prefixText)
        {

            string sqlQuery = "";
            if (chkavdrugs == 1)
            {
                if (HttpContext.Current.Session["SCMModule"] != null)
                {
                    if (Convert.ToInt32(HttpContext.Current.Session["StoreID"]) == 0)
                    {
                        sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                        sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";
                    }
                    else
                    {
                        sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                        sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.StoreId=" + HttpContext.Current.Session["StoreID"].ToString() + " and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";
                    }

                }
                else
                {
                    sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                    sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";

                }
            }
            else
            {
                if (Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]) != 201)
                {
                    sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                    sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' Group by md.Drug_pk,md.Drugname";
                }
                else
                {
                    if (HttpContext.Current.Session["SCMModule"] != null)
                    {
                        if (Convert.ToInt32(HttpContext.Current.Session["StoreID"]) == 0)
                        {
                            sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                            sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";
                        }
                        else
                        {
                            sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                            sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.StoreId=" + HttpContext.Current.Session["StoreID"].ToString() + " and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";
                        }

                    }
                    else
                    {
                        sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                        sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' Group by md.Drug_pk,md.Drugname";
                    }
                }

            }

            return sqlQuery;
        }

        public class Drugs
        {
            protected int _DrugId;
            public int DrugId
            {
                get { return _DrugId; }
                set { _DrugId = value; }
            }

            protected int _avlqty;
            public int AvlQty
            {
                get { return _avlqty; }
                set { _avlqty = value; }
            }

            protected string _drugName;
            public string DrugName
            {
                get { return _drugName; }
                set { _drugName = value; }
            }
        }

        protected void txtDrug_TextChanged(object sender, EventArgs e)
        {
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            if (txtDrug.Text.Trim().Length > 0)
            {
                int DrugId = 0;
                bool correctRegimenPicked = true;

                if (hdCustID.Value != "")
                {
                    if ((Convert.ToInt32(hdCustID.Value) != 0))
                    {
                        DrugId = Convert.ToInt32(hdCustID.Value);

                        //Ken 04-May-2015
                        var SelectedDrug = thePharmacyManager.GetSelectedDrugDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"])).Tables[0].Rows[0];

                        //Check if the correct regimen has been pick
                        if (SelectedDrug["GenericAbbrevation"].ToString().Length > 0)
                        {
                            string[] sSelectedReg = SelectedDrug["GenericAbbrevation"].ToString().ToLower().Split('/');
                            string sCurrentReg = txtLastReg.Text.ToLower();

                            foreach (string Regpart in sSelectedReg)
                            {
                                if (!sCurrentReg.Contains(Regpart))
                                {
                                    correctRegimenPicked = false;
                                }
                            }
                        }

                        if (correctRegimenPicked)
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else if (!correctRegimenPicked && (ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("change")
                            || ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("substitute")
                            || ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("switch")))
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else if (txtLastReg.Text.Length == 0)
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertRegimen", "alert('You have picked a different regimen. This patient is currently on " + txtLastReg.Text + "');", true);
                            /* Changed By: Gaurav
                             * Dated: 5 May 2016
                             * Purpose: Alert changed to confirm msg.
                             */
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertRegimen", "AlertRegimen('" + txtLastReg.Text + "');", true);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "ClearTextBox", "ClearTextBox('" + txtDrug.ClientID + "');", true);
                checkARTDrugs();
            }
        }

        protected void ddlRegimenCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlRegimenCode.SelectedValue;
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["mst_Regimen"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["mst_Regimen"]);
                theDV.RowFilter = "DeleteFlag=0";
                //
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    var dataRow = theDT.AsEnumerable().Where(x => x.Field<int>("Id") == Convert.ToInt32(selectedValue));
                    DataTable dt = dataRow.CopyToDataTable<DataRow>();
                    ddlregimenLine.SelectedValue = dt.Rows[0]["RegimenLineID"].ToString();
                    ddlTreatmentProg.SelectedValue = dt.Rows[0]["Purpose"].ToString();

                    string showregimen = "true";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "regimendd", "showRegimenDDown('" + showregimen + "');", true);

                }

            }

        }

        protected void btnHidAddDrug_Click(object sender, EventArgs e)
        {
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            if (txtDrug.Text.Trim().Length > 0)
            {
                int DrugId = 0;
                bool correctRegimenPicked = true;

                if (hdCustID.Value != "")
                {
                    if ((Convert.ToInt32(hdCustID.Value) != 0))
                    {
                        DrugId = Convert.ToInt32(hdCustID.Value);

                        //Ken 04-May-2015
                        var SelectedDrug = thePharmacyManager.GetSelectedDrugDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"])).Tables[0].Rows[0];

                        //Check if the correct regimen has been pick
                        if (SelectedDrug["GenericAbbrevation"].ToString().Length > 0)
                        {
                            string[] sSelectedReg = SelectedDrug["GenericAbbrevation"].ToString().ToLower().Split('/');
                            string sCurrentReg = txtLastReg.Text.ToLower();

                            foreach (string Regpart in sSelectedReg)
                            {
                                if (!sCurrentReg.Contains(Regpart))
                                {
                                    correctRegimenPicked = false;
                                }
                            }
                        }

                        if (correctRegimenPicked)
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else if (!correctRegimenPicked && (ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("change")
                            || ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("substitute")
                            || ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("switch")))
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else if (txtLastReg.Text.Length == 0)
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "ClearTextBox", "ClearTextBox('" + txtDrug.ClientID + "');", true);
                checkARTDrugs();
            }
        }

        private Boolean FieldValidation()
        {
            //Store Validation
            if (ddlregimenLine.SelectedValue == "0" && hdnregimenLine.Value != "hidden")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please Select Regimen line";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;

                return false;
            }
            if (ddlRegimenCode.SelectedValue == "0" && hdnregimenCode.Value != "hidden")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please Select Regimen Code";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;

                return false;
            }
            if (ddlTreatmentProg.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please Select Treatment Program ";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;
            }
            //Validation for ddlTreatmentPlan 
            if (ddlTreatmentPlan.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please Select Treatment Plan ";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;
            }

            if (gvDispenseDrugs.Rows.Count == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please select at least one drug";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;


            }
            if (ddlPrescribedBy.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please Select Prescribed by";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;
            }
            if (txtprescriptionDate.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Please Enter Prescribed Date";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;
            }

            //if (Convert.ToInt32(Session["ptnPharmacyPK"]) > 0 && txtDispenseDate.Value == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["MessageText"] = "Please Enter Dispensed Date";
            //    IQCareMsgBox.Show("#C1", theBuilder, this);
            //    Label lblError = new Label();
            //    lblError.Text = (Master.FindControl("lblError") as Label).Text;
            //    return false;

            //}

            if (Convert.ToInt16(Session["Paperless"]) != 1)
            {
                if (Session["SCMModule"] != null)
                {
                    if ((Session["SCMModule"]).ToString() == "PMSCM" && ddlDispensingStore.CssClass != "hidden")
                    {
                        if (ddlDispensingStore.SelectedValue == "0")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = "Please Select Dispensing Store";
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            Label lblError = new Label();
                            //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                            return false;
                        }
                    }
                }
                if (ddlDispensedBy.SelectedValue == "0")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Please Select Dispensed by";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    Label lblError = new Label();
                    //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                    return false;
                }
                if (txtDispenseDate.Value == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Please Enter Dispensed Date";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    Label lblError = new Label();
                    //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                    return false;
                }
                bool error = false;
                string Messege = "";
                string msg = "Please select the batch number for: ";
                foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
                {
                    Label lblDrugName = (Label)gvRow.FindControl("lblDrugName");
                    TextBox txtQtyDispensed = (TextBox)gvRow.FindControl("txtQtyDispensed");
                    DropDownList ddlBatchNo = (DropDownList)gvRow.FindControl("ddlBatchNo");
                    HiddenField hBatchQty = (HiddenField)gvRow.FindControl("hBatchQty");

                    if ((ddlBatchNo.SelectedValue == null || ddlBatchNo.SelectedValue == "" || ddlBatchNo.SelectedValue == "0"))
                    {
                        if (Session["SCMModule"] != null)
                        {
                            if (txtQtyDispensed.Text != null && Convert.ToInt32(txtQtyDispensed.Text.Trim() == "" ? "0" : txtQtyDispensed.Text.Trim()) > 0 && (Session["SCMModule"] == "PMSCM") && ddlDispensingStore.CssClass != "hidden")
                            {
                                Messege += Messege + lblDrugName.Text + "</br>";
                                error = true;
                            }
                        }
                    }

                }
                if (error)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = msg + Messege;
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    Label lblError = new Label();
                    //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                    return false;
                }

            }
            else if (Convert.ToInt32(Session["Paperless"]) == 1)
            {
                if (Convert.ToInt32(Session["PatientVisitID"]) != 0)
                {
                    /*
                     * Modified by : Gaurav 
                     * Dated: 28 Apr 2016
                     * Purpose: dispenser or the prescriber to correct the error of the missed dosing if ststus is "New Order"
                     */

                    if (Session["typeOfDispense"].ToString().ToLower() != "new order")
                    {
                        if (Session["SCMModule"] != null)
                        {
                            if ((Session["SCMModule"].ToString() == "PMSCM") && ddlDispensingStore.CssClass != "hidden")
                            {
                                if (ddlDispensingStore.SelectedValue == "0")
                                {
                                    MsgBuilder theBuilder = new MsgBuilder();
                                    theBuilder.DataElements["MessageText"] = "Please Select Dispensing Store";
                                    IQCareMsgBox.Show("#C1", theBuilder, this);
                                    Label lblError = new Label();
                                    //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                                    return false;
                                }
                            }
                        }
                        if (ddlDispensedBy.SelectedValue == "0")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = "Please Select Dispensed by";
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            Label lblError = new Label();
                            //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                            return false;
                        }
                        if (txtDispenseDate.Value == "")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = "Please Enter Dispensed Date";
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            Label lblError = new Label();
                            //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                            return false;
                        }
                        bool error = false;
                        string Messege = "";
                        string msg = "Please select the batch number for: ";
                        foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
                        {
                            Label lblDrugName = (Label)gvRow.FindControl("lblDrugName");
                            TextBox txtQtyDispensed = (TextBox)gvRow.FindControl("txtQtyDispensed");
                            DropDownList ddlBatchNo = (DropDownList)gvRow.FindControl("ddlBatchNo");
                            if ((ddlBatchNo.SelectedValue == null || ddlBatchNo.SelectedValue == "" || ddlBatchNo.SelectedValue == "0"))
                            {
                                if (Session["SCMModule"] != null)
                                {
                                    if (txtQtyDispensed.Text != null && Convert.ToInt32(txtQtyDispensed.Text) > 0 && (Session["SCMModule"].ToString() == "PMSCM") && ddlDispensingStore.CssClass != "hidden")
                                    {
                                        Messege += Messege + lblDrugName.Text + "</br>";
                                        error = true;
                                    }
                                }
                            }

                        }
                        if (error)
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = msg + Messege;
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            Label lblError = new Label();
                            //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                            return false;
                        }
                    }
                    else
                    {
                        bool error = false;
                        string Messege = "";
                        string msg = "Please select the batch number for: ";
                        foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
                        {
                            Label lblDrugName = (Label)gvRow.FindControl("lblDrugName");
                            TextBox txtQtyDispensed = (TextBox)gvRow.FindControl("txtQtyDispensed");
                            DropDownList ddlBatchNo = (DropDownList)gvRow.FindControl("ddlBatchNo");
                            if ((ddlBatchNo.SelectedValue == null || ddlBatchNo.SelectedValue == "" || ddlBatchNo.SelectedValue == "0"))
                            {
                                if (Session["SCMModule"] != null)
                                {
                                    if (txtQtyDispensed.Text != null && Convert.ToInt32(txtQtyDispensed.Text) > 0 && (Session["SCMModule"].ToString() == "PMSCM") && ddlDispensingStore.CssClass != "hidden")
                                    {
                                        Messege += Messege + lblDrugName.Text + "</br>";
                                        error = true;
                                    }
                                }
                            }

                        }
                        if (error)
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = msg + Messege;
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            Label lblError = new Label();
                            //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                            return false;
                        }
                    }

                }

            }

            //Check for Mbungoni to avoid ordering or dispensing if avaialble quantity is not enough
            //--------------------------------------------------------------------
            bool error1 = false;
            string Messege1 = "";
            string msg1 = "The prescribed or dispense amount is more than the available quantity for: ";
            foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
            {
                Label lblDrugName = (Label)gvRow.FindControl("lblDrugName");
                TextBox txtQtyDispensed = (TextBox)gvRow.FindControl("txtQtyDispensed");
                DropDownList ddlBatchNo = (DropDownList)gvRow.FindControl("ddlBatchNo");
                HiddenField hBatchQty = (HiddenField)gvRow.FindControl("hBatchQty");
                TextBox txtQtyPrescribed = (TextBox)gvRow.FindControl("txtQtyPrescribed");

                if (txtQtyDispensed.Text.Length > 0)
                {
                    if (txtQtyDispensed.Text != null && Convert.ToDecimal(txtQtyDispensed.Text) > 0 && Convert.ToDecimal(txtQtyDispensed.Text) > Convert.ToDecimal(hBatchQty.Value))
                    {
                        Messege1 += Messege1 + lblDrugName.Text + "</br>";
                        error1 = true;
                    }
                }
                else if (txtQtyPrescribed.Text != null && Convert.ToDecimal(txtQtyPrescribed.Text) > 0 && Convert.ToDecimal(txtQtyPrescribed.Text) > Convert.ToDecimal(hBatchQty.Value))
                {
                    Messege1 += Messege1 + lblDrugName.Text + "</br>";
                    error1 = true;
                }
            }
            if (error1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = msg1 + Messege1;
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                //lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;
            }
            //---------------------------------------------------------------


            IQCareMsgBox.HideMessage(this);
            return true;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                IQCareUtils theUtils = new IQCareUtils();
                int delete = theUtils.DeleteForm("Pharmacy", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

                if (delete == 0)
                {
                    IQCareMsgBox.Show("RemoveFormError", this);
                    return;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteSuccessful", "alert('Form deleted successfully');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteSuccessful", "alert('Form deleted successfully.');", true);
                    //Response.Redirect("../ClinicalForms/frmPatient_Home.aspx");
                    string theUrl;
                    theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx?Func=Delete");
                    Response.Redirect(theUrl);
                }

            }
            else
            {
                if (FieldValidation() == false)
                {
                    checkARTDrugs();
                    return;
                }

                if (gvDispenseDrugs.Rows.Count > 0)
                {
                    if (Page.IsValid)
                    {
                        try
                        {
                            DataTable theDT = this.GetGridData();
                            string theRegimen = "";
                            Int32 theProgId = 0;
                            int theOrderId = Convert.ToInt32(theDT.Rows[0]["orderId"]);
                            //double theAge = Convert.ToDouble((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblAge") as Label).Text);
                            double theAge = Convert.ToDouble(Session["patientageinyearmonth"] == null ? "0" : Session["patientageinyearmonth"].ToString());
                            int DispensedBy = Convert.ToInt32(ddlDispensedBy.SelectedValue);
                            int iStoreID = Convert.ToInt32(ddlDispensingStore.SelectedValue);
                            string NextAppointmentDate = txtNextApptDate.Text;
                            int therapyPlan = Convert.ToInt32(ddlTreatmentPlan.SelectedValue);
                            int ptnClassification = Convert.ToInt32(ddlPtnClassification.SelectedValue);
                            int isEnrolDifferenciatedCare = Convert.ToInt32(this.hidDifferenciatedCare.Value);

                            if (theAge > 15)
                                theProgId = 116;
                            else
                                theProgId = 117;
                            int datastatus = 0;

                            if (NextAppointmentDate == "") NextAppointmentDate = "01-Jan-1900";

                            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");

                            PrescriptionManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug,BusinessProcess.SCM");
                            DataSet dsPharmacyVitals = PrescriptionManager.GetPharmacyVitals(Convert.ToInt32(Session["PatientID"]));
                            bool IsUpdateIPT = true;
                            if (dsPharmacyVitals.Tables[3].Rows[0]["OnTBtreatment"].ToString() == "Yes")
                            {
                                IsUpdateIPT = false;
                            }
                            else
                            {
                                IsUpdateIPT = true;
                            }

                            if (Convert.ToInt32(HttpContext.Current.Session["PartialDispense"]) != 1)
                            {
                                //Save normal dispensing
                                DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense_Web(
                                PatientId: Convert.ToInt32(Session["PatientID"]),
                                LocationId: Convert.ToInt32(Session["AppLocationId"]),
                                StoreId: iStoreID,
                                UserId: Convert.ToInt32(Session["AppUserId"]),
                                dispensedBy: DispensedBy,
                                DispDate: txtDispenseDate.Value,
                                OrderType: theProgId,//(int)Convert.ToDouble(Session["PatientAge"].ToString()) >= 15 ? 116 : 117,
                                ProgramId: theProgId,
                                    //Convert.ToInt32(ddlTreatmentProg.SelectedValue), 
                                Regimen: theRegimen,
                                OrderId: theOrderId,
                                theDT: theDT,
                                PharmacyRefillDate: NextAppointmentDate,
                                DataStatus: datastatus,
                                orderedBy: Convert.ToInt32(ddlPrescribedBy.SelectedValue),
                                orderDate: txtprescriptionDate.Value,
                                deleteScript: ViewState["deleteScript"] == null ? "" : ViewState["deleteScript"].ToString(),
                                regimenLine: string.IsNullOrEmpty(ddlregimenLine.SelectedValue) == true ? 0 : int.Parse(ddlregimenLine.SelectedValue),
                                regimenCode: string.IsNullOrEmpty(ddlRegimenCode.SelectedValue) == true ? 0 : int.Parse(ddlRegimenCode.SelectedValue),
                                therapyPlan: Convert.ToInt32(therapyPlan.ToString()),
                                    patientClassification: ptnClassification,
                                    isEnrolDifferenciatedCare: isEnrolDifferenciatedCare);
                                int val = theOrderId;
                                int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());

                                SaveUpdateArt(ptnPharmacyPK);
                                if (IsUpdateIPT)
                                {
                                    int visitId = Convert.ToInt32(dtPharmacyDetails.Rows[0]["VisitId"].ToString());
                                    string IPTStartdt = string.Empty;
                                    string IPTEnddt = string.Empty;
                                    if (string.IsNullOrEmpty(this.txtIPTStartDate.Text) == true || string.IsNullOrWhiteSpace(this.txtIPTStartDate.Text) == true)
                                        IPTStartdt = "01-01-1900";
                                    else
                                        IPTStartdt = this.txtIPTStartDate.Text;
                                    if (string.IsNullOrEmpty(this.txtIPTEndDate.Text) == true || string.IsNullOrWhiteSpace(this.txtIPTEndDate.Text) == true)
                                        IPTEnddt = "01-01-1900";
                                    else
                                        IPTEnddt = this.txtIPTEndDate.Text;
                                    thePharmacyManager.SaveUpdateIPTData(Convert.ToInt32(Session["PatientID"]), visitId, Convert.ToDateTime(IPTStartdt), Convert.ToDateTime(IPTEnddt));
                                }
                                //Unlock patient
                                thePharmacyManager.LockpatientForDispensing(Convert.ToInt32(Session["PatientID"]), 0, Session["AppUserName"].ToString(), DateTime.Now.ToString("dd-MMM-yyyy"), false);

                                //Response.Write("<script>alert('Drugs saved successfully.');</script>");
                            }

                            else if (Session["typeOfDispense"].ToString() == "New Order" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
                            {
                                DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense_Web(Convert.ToInt32(Session["PatientID"]), Convert.ToInt32(Session["AppLocationId"]), iStoreID, Convert.ToInt32(Session["AppUserId"]), DispensedBy,
                                  txtDispenseDate.Value, theProgId, Convert.ToInt32(ddlTreatmentProg.SelectedValue), theRegimen, theOrderId, theDT,
                                                  NextAppointmentDate, datastatus, Convert.ToInt32(ddlPrescribedBy.SelectedValue), txtprescriptionDate.Value, ViewState["deleteScript"] == null ? "" : ViewState["deleteScript"].ToString(), int.Parse(ddlregimenLine.SelectedValue), int.Parse(ddlRegimenCode.SelectedValue)
                                                  , Convert.ToInt32(therapyPlan.ToString()),
                                                    patientClassification: ptnClassification,
                                                    isEnrolDifferenciatedCare: isEnrolDifferenciatedCare);
                                int val = theOrderId;
                                int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                                //Session["ptnPharmacyPK"] = ptnPharmacyPK;
                                SaveUpdateArt(ptnPharmacyPK);

                                if (IsUpdateIPT)
                                {
                                    int visitId = Convert.ToInt32(dtPharmacyDetails.Rows[0]["VisitId"].ToString());
                                    string IPTStartdt = string.Empty;
                                    string IPTEnddt = string.Empty;
                                    if (string.IsNullOrEmpty(this.txtIPTStartDate.Text) == true || string.IsNullOrWhiteSpace(this.txtIPTStartDate.Text) == true)
                                        IPTStartdt = "01-01-1900";
                                    else
                                        IPTStartdt = this.txtIPTStartDate.Text;
                                    if (string.IsNullOrEmpty(this.txtIPTEndDate.Text) == true || string.IsNullOrWhiteSpace(this.txtIPTEndDate.Text) == true)
                                        IPTEnddt = "01-01-1900";
                                    else
                                        IPTEnddt = this.txtIPTEndDate.Text;
                                    thePharmacyManager.SaveUpdateIPTData(ptnPharmacyPK, visitId, Convert.ToDateTime(IPTStartdt), Convert.ToDateTime(IPTEnddt));
                                }
                                //Unlock patient
                                thePharmacyManager.LockpatientForDispensing(Convert.ToInt32(Session["PatientID"]), 0, Session["AppUserName"].ToString(), DateTime.Now.ToString("dd-MMM-yyyy"), false);

                                //Response.Write("<script>alert('Drugs saved successfully.');</script>");
                            }


                            else
                            {
                                //Save a partial dispense refill
                                thePharmacyManager.SavePharmacyRefill_Web(theDT, Convert.ToInt32(Session["AppUserId"]), DispensedBy, txtDispenseDate.Value, ViewState["deleteScript"] == null ? "" : ViewState["deleteScript"].ToString());

                                //Unlock patient
                                thePharmacyManager.LockpatientForDispensing(Convert.ToInt32(Session["PatientID"]), 0, Session["AppUserName"].ToString(), DateTime.Now.ToString("dd-MMM-yyyy"), false);

                                //Response.Write("<script>alert('Drugs saved successfully.');</script>");
                            }



                            if (Request.QueryString["opento"] == "ArtForm")
                            {
                                if (Convert.ToInt32(Session["ArtEncounterPatientVisitId"]) > 0)
                                {
                                    Session["PatientVisitId"] = Session["ArtEncounterPatientVisitId"];
                                }
                                else if (Convert.ToInt32(Session["PharmacyUCPatientVisitId"]) > 0)
                                {
                                    if (Request.QueryString["LastRegimenDispensed"] == "True")
                                    {
                                        Session["PatientVisitId"] = Session["PharmacyUCPatientVisitId"];
                                    }
                                }
                                else
                                {
                                    Session["LabId"] = 0;
                                    Session["PatientVisitId"] = 0;
                                }
                                Response.Write("<script>self.close();</script>");
                                //script += "self.close();";
                                Session["PharmacyUCPatientVisitId"] = 0;
                            }
                            else
                            {

                                if (Convert.ToInt32(Session["TechnicalAreaId"]) != 201)
                                {
                                    //Response.Write("<script>window.location.href='../ClinicalForms/frmPatient_History.aspx';</script>");
                                    IQCareMsgBox.NotifyAction("Drugs saved successfully", "Pharmacy Dispense Form", false, this, "window.location.href='../ClinicalForms/frmPatient_History.aspx?sts=" + 0 + "';");
                                }
                                else
                                {
                                    //Response.Write("<script>window.location.href='frmPharmacyDispense_FindPatient.aspx';</script>");
                                    IQCareMsgBox.NotifyAction("Drugs saved successfully", "Pharmacy Dispense Form", false, this, "window.location.href='frmPharmacyDispense_FindPatient.aspx';");
                                }
                            }



                        }
                        catch (Exception ex)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alertErrorOnSave", "alert('Error: " + ex.Message + "');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertRequiredFields", "alert('Please enter all the required fields');", true);
                    }
                }
                //else
                //{
                //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "emptyGrid", "alert('Please select at least one drug.');", true);
                //}
            }

        }

        public void SaveUpdateArt(int OrderID)
        {
            IDrug thePharmacyManager1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            string theNxtApointmentDate = "";
            string theWeight = "";
            string theHeight = "";

            if (theNxtApointmentDate == "")
                theNxtApointmentDate = "01-01-1900";
            else
                theNxtApointmentDate = txtNextApptDate.Text;

            if (txtCurrentWeight.Text == "")
                theWeight = "0";
            else
                theWeight = txtCurrentWeight.Text;

            if (txtCurrentHeight.Text == "")
                theHeight = "0";
            else
                theHeight = txtCurrentHeight.Text;

            DataSet theDS1 = thePharmacyManager1.SaveHivTreatementPharmacyField(OrderID, theWeight, theHeight,
                                                Convert.ToInt32(ddlTreatmentProg.SelectedValue), /*Convert.ToInt32(cmdPeriodTaken.SelectedValue)*/ 0,
                /*Convert.ToInt32(cmbProvider.SelectedValue)*/ 0,
                                                string.IsNullOrEmpty(ddlregimenLine.SelectedValue) == true ? 0 : Convert.ToInt32(ddlregimenLine.SelectedValue),
                                                string.IsNullOrEmpty(ddlRegimenCode.SelectedValue) == true ? 0 : Convert.ToInt32(ddlRegimenCode.SelectedValue),
                                                Convert.ToDateTime(theNxtApointmentDate), /*Convert.ToInt32(cmbReason.SelectedValue)*/ 0);
            return;
        }

        public DataTable GetGridData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("orderId", typeof(string)));
            dt.Columns.Add(new DataColumn("StoreId", typeof(string)));
            dt.Columns.Add(new DataColumn("DrugId", typeof(string)));
            dt.Columns.Add(new DataColumn("DrugName", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("BatchNo", typeof(string)));
            dt.Columns.Add(new DataColumn("BatchId", typeof(string)));
            dt.Columns.Add(new DataColumn("SellingPrice", typeof(string)));
            dt.Columns.Add(new DataColumn("BatchQty", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpiryDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Morning", typeof(string)));
            dt.Columns.Add(new DataColumn("Midday", typeof(string)));
            dt.Columns.Add(new DataColumn("Evening", typeof(string)));
            dt.Columns.Add(new DataColumn("Night", typeof(string)));
            dt.Columns.Add(new DataColumn("Duration", typeof(string)));
            dt.Columns.Add(new DataColumn("PillCount", typeof(string)));
            dt.Columns.Add(new DataColumn("QtyPrescribed", typeof(string)));
            dt.Columns.Add(new DataColumn("QtyDispensed", typeof(string)));
            dt.Columns.Add(new DataColumn("RefillQty", typeof(string)));
            dt.Columns.Add(new DataColumn("Prophylaxis", typeof(string)));
            dt.Columns.Add(new DataColumn("Comments", typeof(string)));
            dt.Columns.Add(new DataColumn("Instructions", typeof(string)));
            dt.Columns.Add(new DataColumn("DispensingUnitId", typeof(string)));
            dt.Columns.Add(new DataColumn("PrintPrescriptionStatus", typeof(string)));
            dt.Columns.Add(new DataColumn("QtyUnitDisp", typeof(string)));
            dt.Columns.Add(new DataColumn("syrup", typeof(string)));
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));


            //vy added regimenmap info
            dt.Columns.Add(new DataColumn("GenericAbbrevation", typeof(string)));

            //Add existing data to data table
            foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
            {
                int orderID = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["orderId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["orderId"]);
                int DrugID = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DrugId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DrugId"]);
                Label lblDrugName = (Label)gvRow.FindControl("lblDrugName");
                Label lblUnit = (Label)gvRow.FindControl("lblUnit");
                DropDownList ddlBatchNo = (DropDownList)gvRow.FindControl("ddlBatchNo");
                Label lblSellingPrice = (Label)gvRow.FindControl("lblSellingPrice");
                HiddenField hBatchQty = (HiddenField)gvRow.FindControl("hBatchQty");
                Label lblExpiryDate = (Label)gvRow.FindControl("lblExpiryDate");
                TextBox txtMorning = (TextBox)gvRow.FindControl("txtMorning");
                TextBox txtMidday = (TextBox)gvRow.FindControl("txtMidday");
                TextBox txtEvening = (TextBox)gvRow.FindControl("txtEvening");
                TextBox txtNight = (TextBox)gvRow.FindControl("txtNight");
                TextBox txtDuration = (TextBox)gvRow.FindControl("txtDuration");
                TextBox txtPillCount = (TextBox)gvRow.FindControl("txtPillCount");
                TextBox txtQtyPrescribed = (TextBox)gvRow.FindControl("txtQtyPrescribed");
                TextBox txtQtyDispensed = (TextBox)gvRow.FindControl("txtQtyDispensed");
                TextBox txtRefillQty1 = (TextBox)gvRow.FindControl("txtRefillQty1");
                CheckBox chkProphylaxis = (CheckBox)gvRow.FindControl("chkProphylaxis");
                CheckBox chkPrintPrescription = (CheckBox)gvRow.FindControl("chkPrintPrescrip");
                TextBox txtComments = (TextBox)gvRow.FindControl("txtComments");
                Label lblInstructions = (Label)gvRow.FindControl("lblInstructions");
                Label lblregimen = (Label)gvRow.FindControl("lblRegimen");
                int DispensingUnitId = 0;
                if (lblUnit.Text.Length > 0)
                    DispensingUnitId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DispensingUnitId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DispensingUnitId"]);
                else
                    DispensingUnitId = 0;

                int BatchID = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["BatchId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["BatchId"]);
                int QtyUnitDisp = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["QtyUnitDisp"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["QtyUnitDisp"]);
                int syrup = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["syrup"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["syrup"]);
                int userid = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["UserID"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["UserID"]);

                DataRow dr = dt.NewRow();
                dr["orderId"] = orderID;
                dr["DrugId"] = DrugID;
                dr["DrugName"] = lblDrugName.Text;
                dr["Unit"] = lblUnit.Text;
                dr["BatchNo"] = ddlBatchNo.Text;
                dr["BatchQty"] = hBatchQty.Value.ToString();
                //dr["SellingPrice"] = lblSellingPrice.Text;
                dr["BatchId"] = ddlBatchNo.SelectedValue.ToString();
                if (lblExpiryDate.Text == "")
                    lblExpiryDate.Text = "01-Jan-1900";
                dr["ExpiryDate"] = lblExpiryDate.Text;
                dr["Morning"] = txtMorning.Text;
                dr["Midday"] = txtMidday.Text;
                dr["Evening"] = txtEvening.Text;
                dr["Night"] = txtNight.Text;
                dr["Duration"] = txtDuration.Text;
                dr["PillCount"] = txtPillCount.Text;
                dr["QtyPrescribed"] = txtQtyPrescribed.Text;
                dr["QtyDispensed"] = txtQtyDispensed.Text;
                dr["RefillQty"] = txtRefillQty1.Text;
                dr["Prophylaxis"] = chkProphylaxis.Checked.ToString();
                dr["Comments"] = txtComments.Text;
                dr["Instructions"] = lblInstructions.Text;
                dr["DispensingUnitId"] = DispensingUnitId;
                dr["PrintPrescriptionStatus"] = chkPrintPrescription.Checked.ToString();
                dr["QtyUnitDisp"] = QtyUnitDisp;
                dr["syrup"] = syrup;
                dr["UserID"] = userid;
                dr["GenericAbbrevation"] = lblregimen.Text;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        protected void gvPendingorders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PatientVisitID"] = Convert.ToInt32(gvPendingorders.DataKeys[gvPendingorders.SelectedIndex].Values["visitID"]);
            int VisitID = Convert.ToInt32(Session["PatientVisitID"]);
            LoadPendingPharmacyOrderDetails(VisitID);
        }

        private void LoadPendingPharmacyOrderDetails(int visitID)
        {
            DataSet theDSPriorPresc = PrescriptionManager.GetPriorPrescription(Convert.ToInt32(Session["PatientID"]));
            ViewState["Priorpresc"] = theDSPriorPresc.Tables[0];
            if (theDSPriorPresc.Tables[0].Rows.Count < 1)
            {
                btnPriorPrescription.Enabled = false;
            }


            if (visitID > 0)
            {
                btnPriorPrescription.Visible = false;
                ddlPrescribedBy.Enabled = (ddlPrescribedBy.SelectedValue == "Select") ? true : false;
                //txtprescriptionDate.Enabled = (ddlPrescribedBy.Enabled == true) ? true : false;
                txtprescriptionDate.Disabled = (ddlPrescribedBy.Enabled == false) ? false : true;
                //if (!ddlPrescribedBy.Enabled) { dtpSpan.Style.Add("display", "none"); }
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet theDS = thePharmacyManager.GetPharmacyExistingRecordDetails_Web(visitID);

                if (theDS.Tables[0].Rows.Count > 0)
                {

                    HttpContext.Current.Session["typeOfDispense"] = theDS.Tables[2].Rows[0]["OrderStatus"].ToString();

                    //--------------------------------------------------------------------
                    /*if (Session["typeOfDispense"].ToString().ToLower() == "partial dispense")
                    {
                        gvDispenseDrugs.Columns[11].HeaderText = "Already<br/> Disp";
                        gvDispenseDrugs.Columns[11].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                        gvDispenseDrugs.Columns[12].HeaderText = "Qty disp";

                        gvDispenseDrugs.Columns[11].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[11].ItemStyle.CssClass = "";

                        gvDispenseDrugs.Columns[12].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[12].ItemStyle.CssClass = "";
                    }
                    else
                    {
                        gvDispenseDrugs.Columns[11].HeaderText = "Already<br/> Disp";
                        gvDispenseDrugs.Columns[11].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                        gvDispenseDrugs.Columns[12].HeaderText = "Qty disp";

                        gvDispenseDrugs.Columns[11].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[11].ItemStyle.CssClass = "";

                        gvDispenseDrugs.Columns[12].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[12].ItemStyle.CssClass = "";
                    }*/

                    gvDispenseDrugs.Columns[11].HeaderText = "Already<br/> Disp";
                    gvDispenseDrugs.Columns[11].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvDispenseDrugs.Columns[11].HeaderStyle.CssClass = "";
                    gvDispenseDrugs.Columns[11].ItemStyle.CssClass = "";

                    gvDispenseDrugs.Columns[12].HeaderText = "Qty disp";
                    gvDispenseDrugs.Columns[12].HeaderStyle.CssClass = "";
                    gvDispenseDrugs.Columns[12].ItemStyle.CssClass = "";


                    gvDispenseDrugs.DataSource = null;
                    gvDispenseDrugs.DataSource = theDS.Tables[0];
                    gvDispenseDrugs.DataBind();
                    HttpContext.Current.Session["StoreID"] = theDS.Tables[1].Rows[0]["StoreId"].ToString();
                    Session["ptnPharmacyPK"] = theDS.Tables[1].Rows[0]["ptn_pharmacy_pk"].ToString();
                    ddlPrescribedBy.SelectedValue = theDS.Tables[1].Rows[0]["OrderedBy"].ToString();
                    //added by VY
                    ddlTreatmentProg.SelectedValue = theDS.Tables[0].Rows[0]["TreatmentProgram"].ToString();
                    ddlregimenLine.SelectedValue = theDS.Tables[0].Rows[0]["RegimenLine"].ToString();

                    ddlPtnClassification.SelectedValue = theDS.Tables[0].Rows[0]["PatientClassification"].ToString();
                    hidDifferenciatedCare.Value = Convert.ToInt32(theDS.Tables[0].Rows[0]["IsEnrolDifferenciatedCare"]).ToString();

                    // Add by Gaurav, Dated 19 May 2016, Purpose: Get treatment plan from pharmacy table
                    if (Convert.ToInt32(theDS.Tables[0].Rows[0]["TreatmentPlan"].ToString()) != 0)
                    {
                        ddlTreatmentPlan.SelectedValue = theDS.Tables[0].Rows[0]["TreatmentPlan"].ToString();
                    }

                    ddlRegimenCode.SelectedValue = theDS.Tables[0].Rows[0]["RegimenId"].ToString();

                    if (theDS.Tables[1].Rows[0]["OrderedByDate"].ToString() != "")
                        txtprescriptionDate.Value = System.Text.RegularExpressions.Regex.Replace(theDS.Tables[1].Rows[0]["OrderedByDate"].ToString(), " ", "-");
                    else
                        txtprescriptionDate.Value = theDS.Tables[1].Rows[0]["OrderedByDate"].ToString();


                    ddlDispensedBy.SelectedValue = theDS.Tables[1].Rows[0]["DispensedBy"].ToString();


                    if (theDS.Tables[1].Rows[0]["DispensedBy"].ToString() != "")
                        txtDispenseDate.Value = System.Text.RegularExpressions.Regex.Replace(theDS.Tables[1].Rows[0]["DispensedByDate"].ToString(), " ", "-");
                    else
                        txtDispenseDate.Value = theDS.Tables[1].Rows[0]["DispensedByDate"].ToString();
                    HttpContext.Current.Session["MarkasFullyDispense"] = theDS.Tables[1].Rows[0]["DispensedByDate"].ToString();
                    HttpContext.Current.Session["PartialDispense"] = 1;

                    foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
                    {
                        if (Convert.ToInt16(Session["Paperless"]) == 1)
                        {
                            /*
                             * Modified by : Gaurav 
                             * Dated: 28 Apr 2016
                             * Purpose: dispenser or the prescriber to correct the error of the missed dosing if ststus is "New Order"
                             */
                            if (Session["typeOfDispense"].ToString().ToLower() == "new order")
                            {
                                (gvRow.FindControl("txtMorning") as TextBox).Enabled = true;
                                (gvRow.FindControl("txtMidday") as TextBox).Enabled = true;
                                (gvRow.FindControl("txtEvening") as TextBox).Enabled = true;
                                (gvRow.FindControl("txtNight") as TextBox).Enabled = true;
                                (gvRow.FindControl("txtDuration") as TextBox).Enabled = true;
                                (gvRow.FindControl("txtQtyPrescribed") as TextBox).Enabled = true;
                            }
                            else
                            {
                                (gvRow.FindControl("txtMorning") as TextBox).Enabled = false;
                                (gvRow.FindControl("txtMidday") as TextBox).Enabled = false;
                                (gvRow.FindControl("txtEvening") as TextBox).Enabled = false;
                                (gvRow.FindControl("txtNight") as TextBox).Enabled = false;
                                (gvRow.FindControl("txtDuration") as TextBox).Enabled = false;
                                (gvRow.FindControl("txtQtyPrescribed") as TextBox).Enabled = false;
                            }


                            if (Session["typeOfDispense"].ToString().ToLower() == "partial dispense")
                            {
                                (gvRow.FindControl("txtPillCount") as TextBox).Enabled = false;
                                (gvRow.FindControl("txtRefillQty1") as TextBox).Enabled = false;
                            }
                        }

                        TextBox txtRefillQty1 = (gvRow.FindControl("txtRefillQty1") as TextBox);
                        TextBox txtPillCount = (gvRow.FindControl("txtPillCount") as TextBox);
                        TextBox txtQtyDispensed = (gvRow.FindControl("txtQtyDispensed") as TextBox);
                        TextBox txtQtyPrescribed = (gvRow.FindControl("txtQtyPrescribed") as TextBox);

                        if (txtPillCount.Text.Length == 0) txtPillCount.Text = "0";
                        if (txtQtyDispensed.Text.Length == 0) txtQtyDispensed.Text = "0";
                        if (txtQtyPrescribed.Text.Length == 0) txtQtyPrescribed.Text = "0";
                        if (txtRefillQty1.Text.Length == 0) txtRefillQty1.Text = "0";

                        //txtRefillQty.Text = (double.Parse(txtQtyPrescribed.Text) - (double.Parse(txtPillCount.Text) + double.Parse(txtQtyDispensed.Text))).ToString();
                        //txtRefillQty.Text = double.Parse(txtQtyDispensed.Text).ToString();
                    }
                    string showregimen = "false";
                    for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                    {
                        if (theDS.Tables[0].Rows[i]["UserID"].ToString() != Session["AppUserId"].ToString())
                        {
                            gvDispenseDrugs.Rows[i].Cells[16].ControlStyle.CssClass = "hidden";
                        }
                        if (theDS.Tables[0].Rows[i]["GenericAbbrevation"].ToString() != "")
                        {
                            showregimen = "true";
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "regimendd", "showRegimenDDown('" + showregimen + "');", true);
                }

            }
            else
            {
                if (Convert.ToInt16(Session["Paperless"]) == 1)
                {
                    // ddlPrescribedBy.SelectedValue = Session["AppUserId"].ToString();
                    ddlPrescribedBy.SelectedValue = "0";

                }
                else { ddlPrescribedBy.SelectedValue = Session["AppUserId"].ToString(); }
                txtprescriptionDate.Value = Application["AppCurrentDate"].ToString();
                txtDispenseDate.Value = Application["AppCurrentDate"].ToString();
            }

        }

        protected void gvPendingorders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPendingorders, "Select$" + e.Row.RowIndex);

            }
        }

        protected void ddlDispensingStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["StoreID"] = ddlDispensingStore.SelectedValue;
            //Ken 04-May-2015 commented out
            //HttpContext.Current.Session["DrugList"] = PrescriptionManager.GetPharmacyDrugList_Web(Convert.ToInt32(HttpContext.Current.Session["StoreID"]), HttpContext.Current.Session["SCMModule"] == null ? "" : HttpContext.Current.Session["SCMModule"].ToString(), Session["TechnicalAreaId"].ToString());
            //DataSet ds = (DataSet)HttpContext.Current.Session["DrugList"];
            //

            foreach (GridViewRow gvrow in gvDispenseDrugs.Rows)
            {
                int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvrow.RowIndex].Value);

                //var SelectedDrug = (from DataRow tmp in ds.Tables[0].Rows
                //                    where Convert.ToInt32(tmp["Drug_Pk"]) == DrugId && Convert.ToInt32(tmp["AvailQty"]) > 0
                //                    orderby Convert.ToDateTime(tmp["ExpiryDate"]) ascending
                //                    select new { BatchID = tmp["Batchid"], BatchName = tmp["BatchQty"] });

                //Ken 04-May-2015 - Load batch details from the DB
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));
                //

                DropDownList ddlBatchNo = (DropDownList)gvrow.FindControl("ddlBatchNo");
                Label lblExpiryDate = (Label)gvrow.FindControl("lblExpiryDate");
                HiddenField hBatchQty = (HiddenField)gvrow.FindControl("hBatchQty");

                ddlBatchNo.DataSource = dsBatches;
                ddlBatchNo.DataTextField = "BatchQty";
                ddlBatchNo.DataValueField = "BatchID";
                ddlBatchNo.DataBind();

                if (ddlBatchNo.Items.Count > 0)
                {
                    int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                    var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                      where Convert.ToInt32(tmp["Batchid"]) == batchID
                                      select tmp["ExpiryDate"]).FirstOrDefault();

                    lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");

                    var BatchQty = (from DataRow tmp in dsBatches.Tables[0].Rows
                                    where Convert.ToInt32(tmp["Batchid"]) == batchID
                                    //select tmp["quantity"]).FirstOrDefault();
                                    select tmp["BatchQty"]).FirstOrDefault();
                    hBatchQty.Value = BatchQty.ToString();
                }
                else
                {
                    lblExpiryDate.Text = "";
                    hBatchQty.Value = "0";
                }
            }

            Session["StoreID"] = ddlDispensingStore.SelectedValue.ToString();
        }

        protected void gvDispenseDrugs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = GetGridData();
            dt.Rows.RemoveAt(e.RowIndex);
            if (ViewState["deleteScript"] == null)
                ViewState["deleteScript"] = "delete from dtl_patientpharmacyorder where ptn_pharmacy_pk=" + gvDispenseDrugs.DataKeys[e.RowIndex].Values["orderId"].ToString() + " and drug_pk=" + gvDispenseDrugs.DataKeys[e.RowIndex].Values["DrugId"].ToString() + ";";
            else
                ViewState["deleteScript"] = ViewState["deleteScript"] + "delete from dtl_patientpharmacyorder where ptn_pharmacy_pk=" + gvDispenseDrugs.DataKeys[e.RowIndex].Values["orderId"].ToString() + " and drug_pk=" + gvDispenseDrugs.DataKeys[e.RowIndex].Values["DrugId"].ToString() + ";";
            //deleteScript = deleteScript + "delete from dtl_patientpharmacyorder where ptn_pharmacy_pk=" + gvDispenseDrugs.DataKeys[e.RowIndex].Values["orderId"].ToString() + " and drug_pk=" + gvDispenseDrugs.DataKeys[e.RowIndex].Values["DrugId"].ToString() + ";";

            gvDispenseDrugs.DataSource = dt;
            gvDispenseDrugs.DataBind();
            checkARTDrugs();
        }

        protected void btnFullyDispensed_Click(object sender, EventArgs e)
        {
            if (gvDispenseDrugs.Rows.Count > 0)
            {
                DataTable theDT = this.GetGridData();
                int OrderId = Convert.ToInt32(theDT.Rows[0]["orderId"]);
                string Reason = theDT.Rows[0]["comments"].ToString();
                if (OrderId == 0 || HttpContext.Current.Session["MarkasFullyDispense"].ToString() == "")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "GridMarkFullyDispfirst", "alert('Please dispense drug first.');", true);
                    return;
                }
                //QtyDispensed
                foreach (DataRow row in theDT.Rows)
                {
                    row["QtyDispensed"] = row["QtyPrescribed"].ToString();
                }

                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                thePharmacyManager.MarkOrderAsFullyDispensed(OrderId, Reason);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSaveDispense", "alert('Details saved successfully in database');", true);

                if (Convert.ToInt32(Session["TechnicalAreaId"]) != 201)
                {
                    Response.Redirect("../ClinicalForms/frmPatient_History.aspx");
                }
                else
                {
                    Response.Redirect("frmPharmacyDispense_FindPatient.aspx");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "emptyGridMarkFullyDisp", "alert('No drug selected.');", true);
            }
        }

        private void BindDropdownSignature(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            //    DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            //    theDV.RowFilter = "DeleteFlag = 0";
            //    if (theDV.Table != null)
            //    {
            //        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            //        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
            //        {
            //            theDV = new DataView(theDT);
            //            theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
            //            if (theDV.Count > 0)
            //                theDT = theUtils.CreateTableFromDataView(theDV);
            //        }
            //        //BindManager.BindCombo(ddlDispensedBy, theDT, "EmployeeName", "EmployeeId");
            //        BindUserDropdown(ddlDispensedBy, string.Empty);
            //    }
            //}
            //vy added regimenLine

            if (theDS.Tables["mst_RegimenLine"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["mst_RegimenLine"]);
                theDV.RowFilter = "DeleteFlag=0";
                //
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindFunctions theBindMgr = new BindFunctions();
                    theBindMgr.BindCombo(ddlregimenLine, theDT, "Name", "Id");
                    theDV.Dispose();
                    theDT.Clear();
                }

            }
            if (theDS.Tables["mst_Regimen"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["mst_Regimen"]);
                theDV.RowFilter = "DeleteFlag=0";
                //
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindFunctions theBindMgr = new BindFunctions();
                    theBindMgr.BindCombo(ddlRegimenCode, theDT, "Name", "Id");
                    theDV.Dispose();
                    theDT.Clear();
                }

            }
        }

        private void BindUserDropdown(DropDownList DropDownID, String userId)
        {
            Dictionary<int, string> userList = new Dictionary<int, string>();
            CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
            if (!string.IsNullOrEmpty(userId))
            {
                if (userList.ContainsKey(Convert.ToInt32(userId)))
                {
                    DropDownID.SelectedValue = userId;
                    //SecurityPerTabSignature = userId;
                }
            }
        }

        protected void ddlBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlBatchNo = (DropDownList)sender;
            GridViewRow gvOrdersRow = ((GridViewRow)ddlBatchNo.Parent.Parent);
            Label lblExpiryDate = (Label)gvOrdersRow.FindControl("lblExpiryDate");
            HiddenField hBatchQty = (HiddenField)gvOrdersRow.FindControl("hBatchQty");
            int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvOrdersRow.RowIndex].Value);

            if (lblExpiryDate != null)
            {
                //Ken. 05-May-2015
                //DataSet ds = (DataSet)Session["DrugList"];
                int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));
                //

                var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                  where Convert.ToInt32(tmp["Batchid"]) == batchID
                                  select tmp["ExpiryDate"]).FirstOrDefault();
                var BatchQty = (from DataRow tmp in dsBatches.Tables[0].Rows
                                where Convert.ToInt32(tmp["Batchid"]) == batchID
                                select tmp["quantity"]).FirstOrDefault();

                lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");
                hBatchQty.Value = BatchQty.ToString();
            }
        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            Session["PatientVisitId"] = 0;
            Session["typeOfDispense"] = "New Order";
            Session["PartialDispense"] = 0;

            setValidatorsBasedOntechnicalArea();

            gvDispenseDrugs.DataSource = null;
            gvDispenseDrugs.DataBind();
            ddlPrescribedBy.Enabled = true;
            txtprescriptionDate.Disabled = false;
            //dtpSpan.Style.Add("display", "inline");
            ddlPrescribedBy.SelectedIndex = 0;
            ddlDispensedBy.SelectedIndex = 0;
            //txtprescriptionDate.Value = "";
            //txtDispenseDate.Value = "";
            txtprescriptionDate.Value = Application["AppCurrentDate"].ToString();
            txtDispenseDate.Value = Application["AppCurrentDate"].ToString();
            txtDrug.Enabled = true;
        }

        private void resizeScreen()
        {
            if (!object.Equals(Session["browserWidth"], null))
            {
                if (Session["browserWidth"].ToString() != null && Session["browserWidth"].ToString() != "")
                {
                    if (Convert.ToInt32(Session["browserWidth"].ToString()) > 1200)
                    {
                        LiteralControl ltr = new LiteralControl();
                        ltr.Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                                    @"#mainMaster
                            {
                                width: 100% !important;
                            }
                            #containerMaster
                            {
                                width: 1200px !important;
                            }
                            #ulAlerts
                            {
                                width: 1180px !important;
                            }
                            #divPatientInfo123
                            {
                                width: 1180px !important;
                            }
                            </style>
                            ";
                        this.Page.Header.Controls.Add(ltr);
                    }
                    else
                    {
                        LiteralControl ltr = new LiteralControl();
                        ltr.Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                                    @"#mainMaster
                            {
                                width: 100% !important;
                            }
                            #containerMaster
                            {
                                width: 90% !important;
                            }
                            #ulAlerts
                            {
                                width: 100% !important;
                            }
                            #divPatientInfo123
                            {
                                width: 99% !important;
                            }
                            </style>
                            ";
                        this.Page.Header.Controls.Add(ltr);
                    }
                }
            }
        }

        protected void btnPrintLabel_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmprintdialog.aspx?visitID=" + Convert.ToInt32(Session["PatientVisitId"].ToString()) + "&ptnpk=" + Convert.ToInt32(Session["PatientId"].ToString()) + "' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.RegisterStartupScript("DrgPopup", theScript);
        }

        protected void btnPrintPres_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["ptnPharmacyPK"]) > 0)
            {
                string theUrl = string.Format("{0}&ReportName={1}&sts={2}", "../Reports/frmReportViewer.aspx?name=Add", "PharmacyPrescription", "0");
                Response.Redirect(theUrl);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertprintdispense", "alert('Please save Drug first in database to print prescription');", true);
            }
        }

        private void Authenticate()
        {
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentication = new AuthenticationManager();

            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 201)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    btnSave.Enabled = Authentication.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Add, (DataTable)Session["UserRight"]);
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnSave.Enabled = Authentication.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Update, (DataTable)Session["UserRight"]);
                }
                if (Request.QueryString["name"] == "Delete")
                {
                    if (Authentication.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        string theUrl = "";
                        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_DeleteForm.aspx");
                        Response.Redirect(theUrl);
                    }

                    btnSave.Text = "Delete";
                    btnSave.Enabled = Authentication.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Delete, (DataTable)Session["UserRight"]);
                }
                //Privilages for Care End
                if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
                {
                    btnSave.Enabled = true;
                }
            }
            else
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    btnSave.Enabled = Authentication.HasFunctionRight(ApplicationAccess.Dispense, FunctionAccess.Add, (DataTable)Session["UserRight"]);
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnSave.Enabled = Authentication.HasFunctionRight(ApplicationAccess.Dispense, FunctionAccess.Update, (DataTable)Session["UserRight"]);
                }
                //if (Request.QueryString["name"] == "Delete")
                //{
                //    if (Authentication.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                //    {
                //        string theUrl = "";
                //        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_DeleteForm.aspx");
                //        Response.Redirect(theUrl);
                //    }

                //    btnSave.Text = "Delete";
                //    btnSave.Enabled = Authentication.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Delete, (DataTable)Session["UserRight"]);
                //}
                //Privilages for Care End
                //if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
                //{
                //    btnSave.Enabled = true;
                //}
            }

        }

        private void checkARTDrugs()
        {
            string sshowreg = "false";
            foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
            {
                Label lblregimen = (Label)gvRow.FindControl("lblRegimen");
                if (lblregimen.Text != "")
                {
                    sshowreg = "true";
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "regimendd", "showRegimenDDown('" + sshowreg + "');", true);
        }

        protected void btnPriorPrescription_Click(object sender, EventArgs e)
        {
            gvDispenseDrugs.DataSource = null;
            DataTable dt = ((DataTable)ViewState["Priorpresc"]);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvDispenseDrugs.DataSource = dt;
                gvDispenseDrugs.DataBind();
                if (ddlRegimenCode.Items.FindByValue(Convert.ToString(dt.Rows[0]["RegimenId"] == null ? "" : dt.Rows[0]["RegimenId"])) != null)
                {
                    ddlRegimenCode.SelectedValue = Convert.ToString(dt.Rows[0]["RegimenId"] == null ? "" : dt.Rows[0]["RegimenId"]);
                }
                if (ddlregimenLine.Items.FindByValue(Convert.ToString(dt.Rows[0]["RegimenLine"] == null ? "" : dt.Rows[0]["RegimenLine"])) != null)
                {
                    ddlregimenLine.SelectedValue = Convert.ToString(dt.Rows[0]["RegimenLine"] == null ? "" : dt.Rows[0]["RegimenLine"]);
                }
                checkARTDrugs();
            }
            else
            {
                gvDispenseDrugs.DataSource = null;
            }
        }

        protected void chkAvailDrugs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAvailDrugs.Checked)
                chkavdrugs = 1;
            else
                chkavdrugs = 0;
        }
    }
}