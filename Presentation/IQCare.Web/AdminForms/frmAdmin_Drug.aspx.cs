using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Pankaj Kumar
    // Code Modified By  : Sanjay Rana
    // Written Date      : 25th July 2006
    // Modification Date : 24th Nov 2006
    // Description       : Drug Master
    //
    /// <summary>
    ///
    /// </summary>
    //
    public partial class ManageDrug : BasePage
    {
        #region "Variables"

        /// <summary>
        /// The maximum dose
        /// </summary>
        private Decimal MaxDose, MinDose;

        /// <summary>
        /// The generic table
        /// </summary>
        private DataTable theGenericTable = new DataTable();

        /// <summary>
        /// The master ds
        /// </summary>
        private DataSet theMasterDS = new DataSet();

        #endregion "Variables"

        #region "Validation Function"

        /// <summary>
        /// Handles the Click event of the btn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_Click(object sender, EventArgs e)
        {
            //if (Request.QueryString["name"] == "Add")
            //{
            string url = "frmAdmin_Druglist.aspx";
            Response.Redirect(url, false);
        }

        /// <summary>
        /// Handles the Click event of the btnAddDose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddDose_Click(object sender, EventArgs e)
        {
            if (lstGeneric.Items.Count > 0)
            {
                if (FieldValidation1() == false)
                {
                    return;
                }
                ViewState["AddGenTrue"] = "False";
                ViewState["GetStrengthFromDatabase"] = "F";
                // Application.Add("DrugData", (DataSet)ViewState["DrugStrengthFrequency"]);
                Session.Add("DrugData", ViewState["StrengthMaster"]);
                Session.Add("SelectedData", ViewState["SelStrength"]);

                string theScript = "<script language='javascript' id='DrgStrength'>\n";
                theScript += "window.open('frmAdmin_DoseFrequencySelector.aspx?Type=Strength&DrugType=" + ddDrugTypeID.SelectedValue + "','DrugStrength','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximizable=no,resizable=no,minimizable=no,width=700,height=350,scrollbars=yes');\n";
                theScript += "</script>\n";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "DrgStrength", theScript);
                ddDrugType_SelectedIndexChanged(sender, e);
                #region "Display Controls"
                theScript = "";
                theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
                theScript += "show('" + divStatus.ClientID + "');\n";
                theScript += "</script>\n";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
                #endregion "Display Controls"
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddFrequency control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddFrequency_Click(object sender, EventArgs e)
        {
            if (lstGeneric.Items.Count > 0)
            {
                if (FieldValidation1() == false)
                {
                    return;
                }
                ViewState["AddGenTrue"] = "False";
                ViewState["GetFrequencyFromDatabase"] = "F";
                Session.Add("DrugData", ViewState["FrequencyMaster"]);
                Session.Add("SelectedData", ViewState["SelFrequency"]);
                string theScript = "<script language='javascript' id='DrgPopup'>\n";
                theScript += "window.open('frmAdmin_DoseFrequencySelector.aspx?Type=Frequency&DrugType=" + ddDrugTypeID.SelectedValue + "','DrugFrequency','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximizable=no,resizable=no,minimizable=no,width=700,height=350,scrollbars=yes');\n";
                theScript += "</script>\n";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "DrgFrequency", theScript);
                ddDrugType_SelectedIndexChanged(sender, e);
                #region "Display Controls"
                theScript = "";
                theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
                theScript += "show('" + divStatus.ClientID + "');\n";
                theScript += "</script>\n";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
                #endregion "Display Controls"
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddGeneric control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddGeneric_Click(object sender, EventArgs e)
        {
            IQCareUtils theUtils = new IQCareUtils();
            if (FieldValidation1() == false)
            {
                return;
            }
            ViewState["AddGenTrue"] = "True";

            ViewState["GetStrengthFromDatabase"] = "T";
            ViewState["GetFrequencyFromDatabase"] = "T";

            if (txtDrugName.Text != "")
            {
            }

            Session.Add("DrugData", ViewState["GenericMaster"]);
            Session.Add("SelectedData", ViewState["SelGeneric"]);

            ////////////////////////////////Application.Add("MasterData", ViewState["DrugDD"]);
            string theScript = "<script language='javascript' id='DrugPopup'>\n";
            theScript += "window.open('frmAdmin_DoseFrequencySelector.aspx?Type=Generic&DrugType=" + ddDrugTypeID.SelectedValue + "', 'DrugGeneric','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DrugPopup", theScript);
            ddDrugType_SelectedIndexChanged(sender, e);
            #region "Display Controls"
            theScript = "";
            theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
            theScript += "show('" + divStatus.ClientID + "');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
            #endregion "Display Controls"
        }

        /// <summary>
        /// Handles the Click event of the btnAddshedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddshedule_Click(object sender, EventArgs e)
        {
            //if (lstshedule.Items.Count > 0)
            //{
            if (FieldValidation1() == false)
            {
                return;
            }
            ViewState["AddGenTrue"] = "False";
            ViewState["GetFrequencyFromDatabase"] = "S";
            Session.Add("DrugScheduleData", ViewState["ScheduleMaster"]);
            Session.Add("SelectedScheduleData", ViewState["SelSchedule"]);
            string theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('frmAdmin_DrugScheduleSelector.aspx?Type=Schedule&DrugType=" + ddDrugTypeID.SelectedValue + "','DrugSchedule','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximizable=no,resizable=no,minimizable=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DrgSchedule", theScript);
            ddDrugType_SelectedIndexChanged(sender, e);
            #region "Display Controls"
            theScript = "";
            theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
            theScript += "show('" + divStatus.ClientID + "');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
            #endregion "Display Controls"
            //}
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = "frmAdmin_Druglist.aspx";
            Response.Redirect(url, false);
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Add")
            {
                if (FieldValidation() == false)
                {
                    ddDrugType_SelectedIndexChanged(sender, e);
                    return;
                }
                if (FieldValidation2() == false)
                {
                    ddDrugType_SelectedIndexChanged(sender, e);
                    return;
                }
            }
            else
            {
                if (FieldValidation() == false)
                {
                    ddDrugType_SelectedIndexChanged(sender, e);
                    return;
                }
            }
            int DrugId = 0;
            int ExistDrugId = 0;
            DataTable theExistGeneric = new DataTable();
            DataTable theStrengthDT = new DataTable();
            DataTable theFrequencyDT = new DataTable();
            DataTable theScheduleDT = new DataTable();
            try
            {
                IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                int theDrugTypeID = Convert.ToInt32(ddDrugTypeID.SelectedValue);
                //Updated by Naveen 22-Nov-2011
                //if (theDrugTypeID == 37)
                //{
                if (ViewState["SelStrength"] != null)
                {
                    theStrengthDT = (DataTable)ViewState["SelStrength"];
                }
                if (ViewState["SelFrequency"] != null)
                {
                    theFrequencyDT = (DataTable)ViewState["SelFrequency"];
                }

                //}
                if (theDrugTypeID == 60)
                {
                    if (ViewState["SelSchedule"] != null)
                    {
                        theScheduleDT = (DataTable)ViewState["SelSchedule"];
                    }
                }

                DataTable theGenericDT = (DataTable)ViewState["SelGeneric"];
                if (theDrugTypeID != 37)
                {
                    //MaxDose = Convert.ToDecimal(maxDosetxt.Text);
                    //MinDose = Convert.ToDecimal(minDosetxt.Text);
                    MaxDose = 0;
                    MinDose = 0;
                }

                if (Request.QueryString["name"] == "Add")
                {
                    DrugId = (int)DrugManager.SaveUpdateDrugDetails(ExistDrugId, txtDrugName.Text, txtDrugAbbre.Text, Convert.ToInt32(ddStatus.SelectedValue), theExistGeneric, theGenericDT, MaxDose, MinDose, theDrugTypeID, Convert.ToInt32(Session["AppUserId"]), theStrengthDT, theFrequencyDT, theScheduleDT, 0);
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    //theExistGeneric = (DataTable)ViewState["ExistGenericDT"];
                    theExistGeneric = (DataTable)ViewState["SelGeneric"];
                    ExistDrugId = Convert.ToInt32(Request.QueryString["DrugId"]);
                    //theGenericDT = (DataTable)ViewState["ExistGenericDT"];
                    theGenericDT = (DataTable)ViewState["SelGeneric"];

                    DrugId = (int)DrugManager.SaveUpdateDrugDetails(ExistDrugId, txtDrugName.Text, txtDrugAbbre.Text, Convert.ToInt32(ddStatus.SelectedValue), theExistGeneric, theGenericDT, MaxDose, MinDose, theDrugTypeID, Convert.ToInt32(Session["AppUserId"]), theStrengthDT, theFrequencyDT, theScheduleDT, 1);
                }

                //-------------rupesh 18-oct - for inactivating/activating generics     starts-------
                if (Request.QueryString["name"] == "Edit")
                {
                    IDrugMst GenericManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    if ((txtDrugName.Text.ToString() == "") && (ddStatus.SelectedIndex == 1) && (lstGeneric.Items.Count == 1)) // ie inactive
                    {
                        int therows = DrugManager.InActivateGeneric(Convert.ToInt32(((DataTable)ViewState["SelGeneric"]).Rows[0][0]));
                    }
                    else if ((txtDrugName.Text.ToString() == "") && (ddStatus.SelectedIndex == 0) && (lstGeneric.Items.Count == 1))//activate
                    {
                        int therows = DrugManager.ActivateGeneric(Convert.ToInt32(((DataTable)ViewState["SelGeneric"]).Rows[0][0]));
                    }
                }
                //-------------rupesh 18-oct - for inactivating/activating generics     ends-------

                btn_Click(sender, e);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                ddDrugType_SelectedIndexChanged(sender, e);
                return;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddDrugType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddDrugType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Fills the drop downs.
        /// </summary>
        protected void FillDropDowns()
        {
            try
            {
                BindFunctions BindManager = new BindFunctions();
                BindManager.BindCombo(ddDrugTypeID, (DataTable)ViewState["DrugType"], "DrugTypeName", "DrugTypeID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="Sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object Sender, EventArgs e)
        {
            Init_Page();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Drugs";
            lblH2.Text = Request.QueryString["name"];

            /********************** Adding Attributes *********************/

            string QueryString = Request.QueryString["name"];
            if (!IsPostBack)
            {
                ViewState["PrevDrug"] = txtDrugName.Text; // used for chking duplicate drug

                ViewState["AddGenTrue"] = "False";
                //Session.Remove("SelectedScheduleData");
                if (theMasterDS.Tables.Count == 0) // 10Mar08
                {
                    RefreshCache();
                    return;
                }

                ViewState["DrugType"] = theMasterDS.Tables[0];
                ViewState["GenericMaster"] = theMasterDS.Tables[1];
                ViewState["StrengthMaster"] = theMasterDS.Tables[2];
                ViewState["FrequencyMaster"] = theMasterDS.Tables[3];
                ViewState["ScheduleMaster"] = theMasterDS.Tables[4];
                ViewState["GenericMasterFD"] = theMasterDS.Tables[1]; // for the case when FD is selected

                FillDropDowns();
                if (ViewState["ExistDS"] != null)
                {
                    #region "Display Controls"
                    string theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
                    theScript += "show('" + divStatus.ClientID + "');\n";
                    theScript += "</script>\n";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
                    #endregion "Display Controls"

                    ddDrugTypeID.SelectedValue = ViewState["DrugTypeId"].ToString();
                    ViewState["ExistGenericDT"] = theGenericTable;

                    DataTable theSelectedGeneric = MakeSelectedGenericTable();
                    foreach (DataRow theDR in ((DataTable)ViewState["ExistGenericDT"]).Rows)
                    {
                        DataView theDV = new DataView((DataTable)ViewState["GenericMaster"]);
                        theDV.RowFilter = "GenericID=" + theDR["Id"];
                        if (theDV.Count > 0) //Rupesh 13Dec07
                        {
                            DataRow theDR1 = theSelectedGeneric.NewRow();
                            theDR1["Id"] = theDV[0]["GenericID"];
                            theDR1["Name"] = theDV[0]["GenericName"];
                            theDR1["Abbrevation"] = theDV[0]["GenericAbbrevation"];
                            theSelectedGeneric.Rows.Add(theDR1);
                        }
                    }
                    ViewState["SelGeneric"] = theSelectedGeneric;

                    DataTable DT1 = (DataTable)ViewState["SelGeneric"];
                    foreach (DataRow theDR1 in DT1.Rows)
                    {
                        DataTable theDT1 = (DataTable)ViewState["GenericMaster"];
                        DataRow[] DR1 = theDT1.Select("GenericID = " + theDR1["Id"].ToString());
                        if (DR1.Length > 0)
                            theDT1.Rows.Remove(DR1[0]);
                        ViewState["GenericMaster"] = theDT1;
                    }

                    //if (ddDrugTypeID.SelectedValue == "37")
                    //{
                    DataTable SelStrength = ((DataSet)ViewState["ExistDS"]).Tables[2];
                    SelStrength.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
                    ViewState["SelStrength"] = SelStrength;

                    DataTable SelFrequency = ((DataSet)ViewState["ExistDS"]).Tables[3];
                    SelFrequency.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
                    ViewState["SelFrequency"] = SelFrequency;
                    #region "Filter Values"

                    //////// StrengthMaster /////////
                    DataTable DT = (DataTable)ViewState["SelStrength"];
                    foreach (DataRow theDR in DT.Rows)
                    {
                        DataTable theDT = (DataTable)ViewState["StrengthMaster"];
                        DataRow[] DR = theDT.Select("StrengthId = '" + theDR["Id"].ToString() + "'");
                        if (DR.Length > 0)
                            theDT.Rows.Remove(DR[0]);
                        ViewState["StrengthMaster"] = theDT;
                    }

                    //////// FrequencyMaster /////////
                    DT = (DataTable)ViewState["SelFrequency"];
                    foreach (DataRow theDR in ((DataTable)ViewState["SelFrequency"]).Rows)
                    {
                        DataTable theDT = (DataTable)ViewState["FrequencyMaster"];
                        DataRow[] DR = theDT.Select("Id = '" + theDR["Id"].ToString() + "'");
                        if (DR.Length > 0) //10-Mar-08
                            theDT.Rows.Remove(DR[0]);
                        ViewState["FrequencyMaster"] = theDT;
                    }

                    #endregion "Filter Values"
                    //}
                    ///////////////////Drug Schedule///////////////////////////
                    if (ddDrugTypeID.SelectedValue == "60")
                    {
                        DataTable SelSchedule = ((DataSet)ViewState["ExistDS"]).Tables[7];
                        //SelFrequency.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
                        ViewState["SelSchedule"] = SelSchedule;

                        #region "Filter Values"

                        //////// ScheduleMaster /////////

                        DataTable DTSched = (DataTable)ViewState["SelSchedule"];
                        if (DTSched.Rows.Count > 0)
                        {
                            foreach (DataRow theDR in ((DataTable)ViewState["SelSchedule"]).Rows)
                            {
                                DataTable theDT = (DataTable)ViewState["ScheduleMaster"];
                                DataRow[] DR = theDT.Select("Id = '" + theDR["Id"].ToString() + "'");
                                if (DR.Length > 0) //10-Mar-08
                                    theDT.Rows.Remove(DR[0]);
                                ViewState["ScheduleMaster"] = theDT;
                            }
                        }

                        #endregion "Filter Values"
                    }
                    if (ddDrugTypeID.SelectedValue == "60")
                    {
                        btnAddshedule.Enabled = true;
                        arvShow1.Visible = false;
                        vaccshow.Visible = true;
                    }
                    else
                    {
                        btnAddshedule.Enabled = false;
                        vaccshow.Visible = false;
                        arvShow1.Visible = true;
                    }
                    ///////////////////////////////////////////////////////////
                    ddDrugType_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                #region "Strength/Frequency/Generic Config"
                if ((Session["SelectedData"] != null) && (Session["SelectedData"].ToString() != ""))
                {
                    string theDrugType = "";
                    if (Session["Type"] != null)
                        theDrugType = Session["Type"].ToString();
                    //ViewState["MasterData"] = Session["MasterData"];
                    DataTable theDT = (DataTable)Session["SelectedData"];

                    if (theDrugType == "Strength")
                    {
                        ViewState["SelStrength"] = theDT;
                        ViewState["StrengthMaster"] = (DataTable)Session["MasterData"];
                    }
                    else if (theDrugType == "Frequency")
                    {
                        ViewState["SelFrequency"] = theDT;
                        ViewState["FrequencyMaster"] = (DataTable)Session["MasterData"];
                    }
                    else if (theDrugType == "Generic")
                    {
                        ViewState["SelGeneric"] = theDT;
                        ViewState["GenericMaster"] = (DataTable)Session["MasterData"];
                    }
                    Session.Remove("Type");
                    Session.Remove("MasterData");
                    Session.Remove("SelectedData");

                    ddDrugType_SelectedIndexChanged(sender, e);
                    #region "Display Controls"
                    string theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
                    theScript += "show('" + divStatus.ClientID + "');\n";
                    theScript += "</script>\n";
                    ClientScript.RegisterClientScriptBlock(this.GetType(),"ShowStatus", theScript);
                    #endregion "Display Controls"
                }
                #endregion "Strength/Frequency/Generic Config"

                #region "Schedule Config"
                if ((Session["SelectedScheduleData"] != null) && (Session["SelectedScheduleData"].ToString() != ""))
                {
                    string theDrugType = "";
                    if (Session["Type"] != null)
                        theDrugType = Session["Type"].ToString();
                    //ViewState["MasterData"] = Session["MasterData"];
                    DataTable theDT = (DataTable)Session["SelectedScheduleData"];

                    if (ddDrugTypeID.SelectedValue == "60")
                    {
                        ViewState["SelSchedule"] = theDT;
                        ViewState["ScheduleMaster"] = (DataTable)Session["MasterScheduleData"];
                    }

                    Session.Remove("Type");
                    Session.Remove("SelectedScheduleData");

                    ddDrugType_SelectedIndexChanged(sender, e);
                    #region "Display Controls"
                    string theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
                    theScript += "show('" + divStatus.ClientID + "');\n";
                    theScript += "</script>\n";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
                    #endregion "Display Controls"
                }
                #endregion "Schedule Config"

                #region "Generic Abbrevation"
                //if ((DataTable)ViewState["SelGeneric"] != null)
                if (ViewState["SelGeneric"] != null)
                {
                    if (ViewState["SelGeneric"].ToString() != "")
                    {
                        //if (ddDrugTypeID.SelectedValue == "37")
                        //{
                        string Abb = "";
                        foreach (DataRow DR in ((DataTable)ViewState["SelGeneric"]).Rows)
                        {
                            if (Abb == "")
                            {
                                Abb = DR[2].ToString();
                            }
                            else
                            {
                                Abb = Abb + "/" + DR[2].ToString();
                            }
                        }
                        txtDrugAbbre.Text = Abb;
                        //}
                    }
                }

                #endregion "Generic Abbrevation"
                BindFunctions BindManager = new BindFunctions();
                if (ViewState["SelStrength"] != null)
                {
                    BindManager.BindList(lstStrength, (DataTable)ViewState["SelStrength"], "Name", "Id");
                }
                if (ViewState["SelFrequency"] != null)
                {
                    //BindManager.BindList(lstFrequency, (DataTable)ViewState["SelFrequency"], "Name", "Id");
                }
                if (ViewState["SelSchedule"] != null)
                {
                    BindManager.BindList(lstshedule, (DataTable)ViewState["SelSchedule"], "Name", "Id");
                }
                if (ViewState["SelGeneric"] != null)
                {
                    BindManager.BindList(lstGeneric, (DataTable)ViewState["SelGeneric"], "Name", "Id");
                    ddDrugTypeID.Enabled = false;

                    if (txtDrugName.Text != "")
                    {
                        //btnAddDose.Enabled = false;
                        //btnAddFrequency.Enabled = false;
                        if (ddDrugTypeID.SelectedValue == "60")
                        {
                            btnAddshedule.Enabled = true;
                        }
                        else
                            btnAddshedule.Enabled = false;
                    }
                    else
                    {
                        if (lstGeneric.Items.Count == 0)
                        {
                            //btnAddDose.Enabled = false;
                            //btnAddFrequency.Enabled = false;
                        }
                        else
                        {
                            btnAddDose.Enabled = true;
                            //btnAddFrequency.Enabled = true;
                            if (ddDrugTypeID.SelectedValue == "60")
                            {
                                btnAddshedule.Enabled = true;
                            }
                            else
                                btnAddshedule.Enabled = false;
                        }
                    }

                    #region "------------fill Strength / Dose-----------"

                    if (lstGeneric.Items.Count == 1)
                    {
                        if (ViewState["AddGenTrue"].ToString() == "True")
                        {
                            //---get all strengths for currently selected generic
                            //---show these strengths as it is if we have more than one generic selected
                            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                            DataTable theDT = new DataTable();
                            DataTable theDT2 = new DataTable();

                            theDT2.Columns.Add("Id", System.Type.GetType("System.Int32"));//----
                            theDT2.Columns.Add("Name", System.Type.GetType("System.String"));

                            foreach (DataRow theDR in ((DataTable)ViewState["SelGeneric"]).Rows)
                            {
                                theDT = (DataTable)DrugManager.GetStrengthByGenericID(Convert.ToInt32(theDR[0]));
                                foreach (DataRow theDR1 in theDT.Rows)
                                {
                                    DataRow theDR2 = theDT2.NewRow();
                                    theDR2["Id"] = theDR1["Id"].ToString();
                                    theDR2["Name"] = theDR1["Name"].ToString();
                                    theDT2.Rows.Add(theDR2);
                                }
                            }
                            DataTable theStrengthSelectedDT = new DataTable();
                            theStrengthSelectedDT = (DataTable)ViewState["SelStrength"];
                            if (theStrengthSelectedDT != null)
                            {
                                theDT2.Merge(theStrengthSelectedDT);
                            }
                            ViewState["SelStrength"] = theDT2;
                            lstStrength.Items.Clear();
                            BindManager.BindList(lstStrength, (DataTable)ViewState["SelStrength"], "Name", "Id");
                        }
                        else//ie if dose btn is clicked
                        {
                            //show all selected strengths
                            lstStrength.Items.Clear();
                            BindManager.BindList(lstStrength, (DataTable)ViewState["SelStrength"], "Name", "Id");
                        }
                    }
                    else if (lstGeneric.Items.Count > 1)
                    {
                        //---get all strengths for currently selected generic
                        //---show these strengths after geting distinct, as it is if we have more than one generic selected
                        IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                        DataTable theDT = new DataTable();
                        DataTable theDT2 = new DataTable();

                        theDT2.Columns.Add("Id", System.Type.GetType("System.Int32"));
                        //theDT2.Columns.Add("Id", System.Type.GetType("System.String"));//----
                        theDT2.Columns.Add("Name", System.Type.GetType("System.String"));

                        foreach (DataRow theDR in ((DataTable)ViewState["SelGeneric"]).Rows)
                        {
                            theDT = (DataTable)DrugManager.GetStrengthByGenericID(Convert.ToInt32(theDR[0]));
                            foreach (DataRow theDR1 in theDT.Rows)
                            {
                                DataRow theDR2 = theDT2.NewRow();
                                theDR2["Id"] = theDR1["Id"].ToString();
                                theDR2["Name"] = theDR1["Name"].ToString();
                                theDT2.Rows.Add(theDR2);
                            }
                        }
                        //get distinct values
                        DataTable theDT3 = new DataTable();
                        //theDT3.Columns.Add("Id", System.Type.GetType("System.String"));
                        theDT3.Columns.Add("Id", System.Type.GetType("System.Int32"));
                        theDT3.Columns.Add("Name", System.Type.GetType("System.String"));

                        foreach (DataRow theDR in theDT2.Rows)
                        {
                            Boolean add = true;
                            foreach (DataRow theDR3 in theDT3.Rows)
                            {
                                if ((theDR3["Id"].ToString().Trim()) == (theDR["Id"].ToString().Trim()))
                                {
                                    add = false;
                                }
                            }
                            if (add == true)
                            {
                                DataRow theDR4 = theDT3.NewRow();
                                theDR4["id"] = theDR["Id"].ToString();
                                theDR4["Name"] = theDR["Name"].ToString();
                                theDT3.Rows.Add(theDR4);
                            }
                        }
                        DataTable theStrengthSelectedDT = new DataTable();
                        theStrengthSelectedDT = (DataTable)ViewState["SelStrength"];
                        if (theStrengthSelectedDT != null)
                        {
                            theDT3.Merge(theStrengthSelectedDT);
                        }
                        ViewState["SelStrength"] = theDT3;
                        lstStrength.Items.Clear();
                        BindManager.BindList(lstStrength, (DataTable)ViewState["SelStrength"], "Name", "Id");
                    }
                    //--remove selected strength from from ViewState["StrengthMaster"]
                    if (ViewState["SelStrength"] != null)
                    {
                        foreach (DataRow theDRS in ((DataTable)ViewState["SelStrength"]).Rows)
                        {
                            foreach (DataRow theDRM in ((DataTable)ViewState["StrengthMaster"]).Rows)
                            {
                                if (theDRM["StrengthId"].ToString().Trim() == theDRS["Id"].ToString().Trim())
                                {
                                    ((DataTable)ViewState["StrengthMaster"]).Rows.Remove(theDRM);
                                    break;
                                }
                            }
                        }
                    }
                    #endregion "------------fill Strength / Dose-----------"

                    #region"-----fill Frequency-----"
                    if (lstGeneric.Items.Count == 1)
                    {
                        if (ViewState["AddGenTrue"].ToString() == "True")
                        {
                            //---get all strengths for currently selected generic
                            //---show these strengths as it is if we have more than one generic selected
                            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                            DataTable theDT = new DataTable();
                            DataTable theDT2 = new DataTable();

                            //theDT2.Columns.Add("Id", System.Type.GetType("System.String"));//----
                            theDT2.Columns.Add("Id", System.Type.GetType("System.Int32"));
                            theDT2.Columns.Add("Name", System.Type.GetType("System.String"));

                            foreach (DataRow theDR in ((DataTable)ViewState["SelGeneric"]).Rows)
                            {
                                theDT = (DataTable)DrugManager.GetFrequencyByGenericID(Convert.ToInt32(theDR[0]));
                                foreach (DataRow theDR1 in theDT.Rows)
                                {
                                    DataRow theDR2 = theDT2.NewRow();
                                    theDR2["Id"] = theDR1["Id"].ToString();
                                    theDR2["Name"] = theDR1["Name"].ToString();
                                    theDT2.Rows.Add(theDR2);
                                }
                            }
                            DataTable thefreqSelectedDT = new DataTable();
                            thefreqSelectedDT = (DataTable)ViewState["SelFrequency"];
                            if (thefreqSelectedDT != null)
                            {
                                theDT2.Merge(thefreqSelectedDT);
                            }
                            ViewState["SelFrequency"] = theDT2;
                            lstFrequency.Items.Clear();
                            //BindManager.BindList(lstFrequency, (DataTable)ViewState["SelFrequency"], "Name", "Id");
                        }
                        else//ie if freq btn is clicked
                        {
                            //show all selected freqency
                            lstFrequency.Items.Clear();
                            //BindManager.BindList(lstFrequency, (DataTable)ViewState["SelFrequency"], "Name", "Id");
                        }
                    }
                    else if (lstGeneric.Items.Count > 1)
                    {
                        //---get all frq for currently selected generic
                        //---show these frq after geting distinct, as it is if we have more than one generic selected
                        IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                        DataTable theDT = new DataTable();
                        DataTable theDT2 = new DataTable();

                        //theDT2.Columns.Add("Id", System.Type.GetType("System.String"));//----
                        theDT2.Columns.Add("Id", System.Type.GetType("System.Int32"));
                        theDT2.Columns.Add("Name", System.Type.GetType("System.String"));

                        foreach (DataRow theDR in ((DataTable)ViewState["SelGeneric"]).Rows)
                        {
                            theDT = (DataTable)DrugManager.GetFrequencyByGenericID(Convert.ToInt32(theDR[0]));
                            foreach (DataRow theDR1 in theDT.Rows)
                            {
                                DataRow theDR2 = theDT2.NewRow();
                                theDR2["Id"] = theDR1["Id"].ToString();
                                theDR2["Name"] = theDR1["Name"].ToString();
                                theDT2.Rows.Add(theDR2);
                            }
                        }
                        //get distinct values
                        DataTable theDT3 = new DataTable();
                        //theDT3.Columns.Add("Id", System.Type.GetType("System.String"));
                        theDT3.Columns.Add("Id", System.Type.GetType("System.Int32"));
                        theDT3.Columns.Add("Name", System.Type.GetType("System.String"));

                        foreach (DataRow theDR in theDT2.Rows)
                        {
                            Boolean add = true;
                            foreach (DataRow theDR3 in theDT3.Rows)
                            {
                                if ((theDR3["Id"].ToString().Trim()) == (theDR["Id"].ToString().Trim()))
                                {
                                    add = false;
                                }
                            }
                            if (add == true)
                            {
                                DataRow theDR4 = theDT3.NewRow();
                                theDR4["id"] = theDR["Id"].ToString();
                                theDR4["Name"] = theDR["Name"].ToString();
                                theDT3.Rows.Add(theDR4);
                            }
                        }
                        DataTable thefreqSelectedDT = new DataTable();
                        thefreqSelectedDT = (DataTable)ViewState["SelFrequency"];
                        if (thefreqSelectedDT != null)
                        {
                            theDT3.Merge(thefreqSelectedDT);
                        }
                        ViewState["SelFrequency"] = theDT3;
                        lstFrequency.Items.Clear();
                        //BindManager.BindList(lstFrequency, (DataTable)ViewState["SelFrequency"], "Name", "Id");
                    }
                    //--remove selected frequency from from ViewState["FrequencyMaster"]
                    if (ViewState["SelFrequency"] != null)
                    {
                        foreach (DataRow theDRS in ((DataTable)ViewState["SelFrequency"]).Rows)
                        {
                            foreach (DataRow theDRM in ((DataTable)ViewState["FrequencyMaster"]).Rows)
                            {
                                if (theDRM["ID"].ToString().Trim() == theDRS["Id"].ToString().Trim())
                                {
                                    ((DataTable)ViewState["FrequencyMaster"]).Rows.Remove(theDRM);
                                    break;
                                }
                            }
                        }
                    }
                    #endregion "Validation Function"
                    ///////////////////////////////////////////
                    #region"-----fill Schedule-----"
                    if (Convert.ToInt32(Request.QueryString["DrugId"]) > 0)
                    {
                        if (ViewState["AddGenTrue"].ToString() == "True")
                        {
                            //---get all strengths for currently selected generic
                            //---show these strengths as it is if we have more than one generic selected
                            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                            DataTable theDT = new DataTable();
                            DataTable theDT2 = new DataTable();

                            theDT2.Columns.Add("Id", System.Type.GetType("System.String"));//----
                            theDT2.Columns.Add("Name", System.Type.GetType("System.String"));

                            theDT = (DataTable)DrugManager.GetScheduleByDrugID(Convert.ToInt32(Request.QueryString["DrugId"]));
                            foreach (DataRow theDR1 in theDT.Rows)
                            {
                                DataRow theDR2 = theDT2.NewRow();
                                theDR2["Id"] = theDR1["Id"].ToString();
                                theDR2["Name"] = theDR1["Name"].ToString();
                                theDT2.Rows.Add(theDR2);
                            }

                            ViewState["SelSchedule"] = theDT2;
                            lstFrequency.Items.Clear();
                            BindManager.BindList(lstshedule, (DataTable)ViewState["SelSchedule"], "Name", "Id");
                        }
                        else//ie if freq btn is clicked
                        {
                            //show all selected freqency
                            lstshedule.Items.Clear();
                            if (ViewState["SelSchedule"] != null)
                            {
                                BindManager.BindList(lstshedule, (DataTable)ViewState["SelSchedule"], "Name", "Id");
                            }
                        }
                    }

                    //--remove selected frequency from from ViewState["FrequencyMaster"]
                    if (ViewState["SelSchedule"] != null)
                    {
                        foreach (DataRow theDRS in ((DataTable)ViewState["SelSchedule"]).Rows)
                        {
                            foreach (DataRow theDRM in ((DataTable)ViewState["ScheduleMaster"]).Rows)
                            {
                                if (theDRM["ID"].ToString().Trim() == theDRS["Id"].ToString().Trim())
                                {
                                    ((DataTable)ViewState["ScheduleMaster"]).Rows.Remove(theDRM);
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                    //////////////////////////////////////////
                }
            }

            if (txtDrugName.Text != "")
            {
                if (ddDrugTypeID.SelectedValue == "60")
                {
                    btnAddshedule.Enabled = true;
                    vaccshow.Visible = true;
                }
                else
                {
                    btnAddshedule.Enabled = false;
                    vaccshow.Visible = false;
                }
            }
            else
            {
                if (lstGeneric.Items.Count == 0)
                {
                }
                else
                {
                    btnAddDose.Enabled = true;
                    //btnAddFrequency.Enabled = true;
                    if (ddDrugTypeID.SelectedValue == "60")
                    {
                        btnAddshedule.Enabled = true;
                        vaccshow.Visible = true;
                    }
                    else
                    {
                        btnAddshedule.Enabled = false;
                        vaccshow.Visible = false;
                    }
                }
            }

            if ((ddStatus.SelectedItem.ToString() == "InActive") || (Request.QueryString["name"] == "Edit"))
            {
                btnAddDose.Enabled = false;
                btnAddFrequency.Enabled = false;
                btnAddGeneric.Enabled = false;
                txtDrugName.Enabled = false;
                btnAddshedule.Enabled = false;
            }
            if ((Request.QueryString["name"] == "Edit") && (txtDrugName.Text != ""))
            {
                btnAddDose.Enabled = false;
                btnAddFrequency.Enabled = false;
                if (ddDrugTypeID.SelectedValue == "60" && ddStatus.SelectedItem.ToString() != "InActive")
                {
                    btnAddshedule.Enabled = true;
                }
            }
            if ((Request.QueryString["name"] == "Edit") && (ViewState["PrevDrug"].ToString() != ""))
            {
                btnAddDose.Enabled = false;
                btnAddFrequency.Enabled = false;
                if (ddDrugTypeID.SelectedValue == "60" && ddStatus.SelectedItem.ToString() != "InActive")
                {
                    btnAddshedule.Enabled = false;
                }
            }

            if (Request.QueryString["name"] == "Add")
            //if((Request.QueryString["name"] == "Add") && (ddDrugTypeID.SelectedValue == "37"))
            {
                arvShow.Visible = true;
                arvShow1.Visible = true;
                nonARVShow1.Visible = false;

                string theScript = "<script language='javascript' defer = 'defer' id='ShowChange'>\n";
                if (ddDrugTypeID.SelectedValue == "60")
                {
                    theScript += "show('" + arvShow.ClientID + "'); show('" + vaccshow.ClientID + "'); \n";
                    vaccshow.Visible = true;
                    arvShow1.Visible = false;
                }
                else
                {
                    theScript += "show('" + arvShow.ClientID + "'); show('" + arvShow1.ClientID + "');\n";
                    vaccshow.Visible = false;
                }
                theScript += "</script>\n";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowChange", theScript);
            }
        }

        /// <summary>
        /// Refreshes the cache.
        /// </summary>
        protected void RefreshCache()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //script += "var ans=true;\n";
            script += "alert('Please refresh cache');\n";
            //script += "if (ans==true)\n";
            //script += "{\n";
            script += "window.location.href='../frmFacilityHome.aspx?';\n";
            //script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        /// <summary>
        /// Backups this instance.
        /// </summary>
        private void Backup()
        {
        }

        /// <summary>
        /// Clear_fieldses this instance.
        /// </summary>
        private void clear_fields()
        {
            /********* Clear all form fields *********/
            txtDrugName.Text = "";
            txtDrugAbbre.Text = "";
            ddDrugTypeID.ClearSelection();
            lstGeneric.ClearSelection();
            txtDrugName.Focus();
        }

        /// <summary>
        /// Exists the generics.
        /// </summary>
        /// <returns></returns>
        private DataTable ExistGenerics()
        {
            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
            //DataSet theDS = (DataSet)DrugManager.GetDrug();
            DataSet theDS = (DataSet)DrugManager.GetDrugMst(); //11Mar08

            IQCareUtils theUtils = new IQCareUtils();

            string theDrugType = Request.QueryString["DrugType"].ToString(); ;
            int DrugID = Convert.ToInt32(Request.QueryString["DrugId"]);
            string GenericName = Request.QueryString["Generic"].ToString();
            DataTable theDT = new DataTable();
            string GenericAbbv = "";
            theDT.Columns.Add("Drug_Pk", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));

            //if (GenericName.Length != 0)
            //{
            if (theDrugType.Trim().ToUpper() == "ARV MEDICATION")//---for ARV
            {
                string[] theGeneric = GenericName.Split(new char[] { '/' });

                foreach (string str in theGeneric)
                {
                    if (str.ToString() != "")
                    {
                        DataRow theDR = theDT.NewRow();
                        DataView theDV = new DataView(theDS.Tables[0]);
                        theDV.RowFilter = "GenericName ='" + str.Trim() + "' and DrugTypeName = '" + theDrugType + "' and Drug_Pk = " + DrugID;
                        if (theDV.Count > 0)
                        {
                            if (GenericAbbv == "")
                            {
                                GenericAbbv = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                GenericAbbv = GenericAbbv + '/' + theDV[0]["GenericAbbrevation"].ToString();
                            }
                            ViewState["Abbrevation"] = GenericAbbv;
                            theDR["Drug_Pk"] = theDV[0]["Drug_Pk"];
                            theDR["Id"] = theDV[0]["GenericId"];
                            theDR["DrugTypeId"] = theDV[0]["DrugTypeId"];
                            theDR["Name"] = theDV[0]["GenericName"];
                            theDT.Rows.Add(theDR);
                            //ViewState["ExistGenericDT"] = theDT;
                        }
                    }
                }
                theGenericTable = theDT;
            }
            else
            {
                DataRow theDR = theDT.NewRow();
                DataView theDV = new DataView(theDS.Tables[0]);
                if ((GenericName.Trim().ToString() != "") && (GenericName.IndexOf("/") < 1))
                {
                    theDV.RowFilter = "GenericName ='" + GenericName.Trim() + "' and DrugTypeName = '" + theDrugType + "' and Drug_Pk = " + DrugID;
                    theDR["Name"] = theDV[0]["GenericName"];
                }
                else
                    theDV.RowFilter = "DrugTypeName = '" + theDrugType + "' and Drug_Pk = " + DrugID;
                theDR["Drug_Pk"] = theDV[0]["Drug_Pk"];
                if (GenericName.IndexOf("/") < 1)
                    theDR["Name"] = theDV[0]["GenericName"];
                theDR["Id"] = theDV[0]["GenericId"];
                theDR["DrugTypeId"] = theDV[0]["DrugTypeId"];
                theDT.Rows.Add(theDR);
                theGenericTable = theDT;
            }
            //}

            return theDT;
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation()
        {
            MsgBuilder theBuilder = new MsgBuilder();

            if (ddDrugTypeID.SelectedValue == "0")
            {
                theBuilder.DataElements["Control"] = "Drug Type";
                EventArgs e = new EventArgs();
                ddDrugType_SelectedIndexChanged(btnSave, e);
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddDrugTypeID.Focus();
                return false;
            }

            if (lstGeneric.Items.Count == 0)
            {
                IQCareMsgBox.Show("GenericSelection", this);
                return false;
            }
            //---if trying to save a tradename, chek whether generic's dose & freq -already exists or not

            if ((txtDrugName.Text == "") && (Convert.ToInt32(ddDrugTypeID.SelectedValue) == 37))
            {
                theBuilder.DataElements["Control"] = "Trade Name For ARV Medication";

                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtDrugName.Focus();
                return false;
                /////////////////
            }
            //------ rupesh 18Oct  Inactivating Generic starts------------------
            if ((txtDrugName.Text.ToString() == "") && (Convert.ToInt32(ddDrugTypeID.SelectedValue) != 37))
            {
                theBuilder.DataElements["Control"] = "Trade Name For Non ARV Medication";
                //EventArgs e = new EventArgs();
                //ddDrugType_SelectedIndexChanged(btnSave, e);
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtDrugName.Focus();
                return false;
                //string url = "frmAdmin_Druglist.aspx"; // rupesh 10-sep-07
                //Response.Redirect(url);
            }

            if ((txtDrugName.Text.ToString() == "") && (ddStatus.SelectedIndex == 1) && (lstGeneric.Items.Count == 1)) // ie inactive
            {
                IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                //foreach (DataRow theDR in ((DataTable)ViewState["SelGeneric"]).Rows)

                DataTable theDT = DrugManager.GetDrugsForGenericID(Convert.ToInt32(((DataTable)ViewState["SelGeneric"]).Rows[0][0]));
                if (theDT.Rows.Count > 0)
                {
                    IQCareMsgBox.Show("CannotInactivate", this);
                    return false;
                }
            }
            //------ rupesh 18Oct  Inactivating Generic ends------------------

            if (lstGeneric.Items.Count > 1)
            {
                if (txtDrugName.Text == "")
                {
                    theBuilder.DataElements["Control"] = "Trade Name For Fixed Dose";
                    EventArgs e = new EventArgs();
                    ddDrugType_SelectedIndexChanged(btnSave, e);
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtDrugName.Focus();
                    return false;
                }
            }

            if ((Request.QueryString["name"] == "Edit") && (ViewState["PrevDrug"].ToString() != "") && (txtDrugName.Text == ""))
            {
                theBuilder.DataElements["Control"] = "Trade Name";
                EventArgs e = new EventArgs();
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                return false;
            }

            if ((Request.QueryString["name"] == "Edit") && (ViewState["PrevDrug"].ToString() == "") && (txtDrugName.Text != ""))
            {
                IQCareMsgBox.Show("CannotAdd", this);
                txtDrugName.Focus();
                return false;
            }

            //--chk duplicate drug
            if (((Request.QueryString["name"] == "Add") && (txtDrugName.Text != "")) ||
               ((Request.QueryString["name"] == "Edit") && ((ViewState["PrevDrug"].ToString()) != (txtDrugName.Text.ToString()))))
            {
                IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                //DataSet theDS = (DataSet)DrugManager.GetDrug();
                DataSet theDS = (DataSet)DrugManager.GetDrugMst();
                foreach (DataRow theDR in (theDS.Tables[0].Rows))
                {
                    if (txtDrugName.Text.Trim().ToString().ToUpper() == theDR["DrugName"].ToString().ToUpper())
                    {
                        IQCareMsgBox.Show("DuplicateDrug", this);
                        return false;
                    }
                }
            }
            if (lstStrength.Items.Count == 0)
            {
                theBuilder.DataElements["Control"] = "Dose";
                EventArgs e = new EventArgs();
                ddDrugType_SelectedIndexChanged(btnSave, e);
                IQCareMsgBox.Show("BlankList", theBuilder, this);
                //lstFrequency.Focus();
                return false;
            }
            if (Convert.ToInt32(ddDrugTypeID.SelectedValue) == 60)
            {
                if (txtDrugName.Text == "")
                {
                    theBuilder.DataElements["Control"] = "Trade Name";
                    EventArgs e = new EventArgs();
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    return false;
                }
                if (lstshedule.Items.Count == 0)
                {
                    theBuilder.DataElements["Control"] = "Schedule";
                    EventArgs e = new EventArgs();
                    ddDrugType_SelectedIndexChanged(btnSave, e);
                    IQCareMsgBox.Show("BlankList", theBuilder, this);
                    lstshedule.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Fields the validation1.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation1()
        {
            if (ddDrugTypeID.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                EventArgs e = new EventArgs();
                ddDrugType_SelectedIndexChanged(btnSave, e);
                theBuilder.DataElements["Control"] = "Drug Type";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddDrugTypeID.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Fields the validation2.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation2()
        {
            MsgBuilder theBuilder = new MsgBuilder();
            EventArgs e = new EventArgs();
            if (lstGeneric.Items.Count == 0)
            {
                if (txtDrugName.Text.Trim() != "")
                {
                    theBuilder.DataElements["Controls"] = "Drug Name";
                    ddDrugType_SelectedIndexChanged(btnSave, e);
                    IQCareMsgBox.Show("FillTextBox", theBuilder, this);
                }
                theBuilder.DataElements["Control"] = "Generic Name";
                ddDrugType_SelectedIndexChanged(btnSave, e);
                IQCareMsgBox.Show("BlankList", theBuilder, this);
                lstGeneric.Focus();
                return false;
            }
            else
            {
                if (txtDrugName.Text != "")
                {
                    IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    DataTable theDT = (DataTable)DrugManager.GetExistDrug(txtDrugName.Text);
                    if (theDT.Rows.Count != 0)
                    {
                        ddDrugType_SelectedIndexChanged(btnSave, e);
                        IQCareMsgBox.Show("DrugExists", this);
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region "User functions"

        /// <summary>
        /// Gets the masters.
        /// </summary>
        private void GetMasters()
        {
            IDrugMst DrugManager;
            try
            {
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\DrugMasters.con"));
                if (theDSXML.Tables["Mst_DrugType"] != null)//10Mar08 -- put conditios
                {
                    DataView theDrugTypeView = new DataView(theDSXML.Tables["Mst_DrugType"].Copy());
                    theDrugTypeView.Sort = "DrugTypeName asc";
                    theMasterDS.Tables.Add(theDrugTypeView.ToTable()); // table 0

                    DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    DataSet theDS = new DataSet();
                    theDS = (DataSet)DrugManager.GetAllDropDowns();//pr_Admin_GetDrugDropDowns_Constella //all GenID,GenName,GenAbbr,DrugTypeID,DelFlag

                    DataView theDV;
                    DataTable theDT;
                    // incase of add OR Active
                    if ((Request.QueryString["Status"] == null) || (Request.QueryString["Status"].ToString() == "Active"))
                    {
                        theDV = new DataView(theDS.Tables[0]);
                        theDV.RowFilter = "DeleteFlag=0";
                        theDT = theDV.Table;
                        theMasterDS.Tables.Add(theDT.Copy());//get only active generics  // table 1
                    }
                    else if (Request.QueryString["Status"].ToString() == "InActive")
                        theMasterDS.Tables.Add(theDS.Tables[0].Copy()); // get list of all generics // table 1

                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_Strength"].Copy()); // table 2
                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_Frequency"].Copy()); // table 3
                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_DrugSchedule"].Copy()); // table 4
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                DrugManager = null;
            }
        }

        /// <summary>
        /// Init_s the page.
        /// </summary>
        private void Init_Page()
        {
            if (Page.IsPostBack != true)
            {
                GetMasters();

                if (Request.QueryString["name"] == "Add")
                {
                    lblH2.Text = "Add Drug";
                    txtDrugAbbre.ReadOnly = true;
                    //arvShow.Visible = false;
                    //arvShow1.Visible = false;
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    IDrugMst DrugManager;
                    BindFunctions BindManager = new BindFunctions();
                    lblH2.Text = "Edit Drug";
                    txtDrugAbbre.ReadOnly = true;
                    //btnAddGeneric.Enabled = false;
                    DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");

                    DataTable theDT1 = ExistGenerics();
                    int GenericId = 0;
                    int Drug_Pk = 0;
                    int DrugTypeId = 0;
                    if (theDT1.Rows.Count != 0)
                    {
                        BindManager.BindList(lstGeneric, theDT1, "Name", "Id");

                        if (theDT1.Rows[0]["Drug_Pk"] != System.DBNull.Value)
                        {
                            Drug_Pk = Convert.ToInt32(theDT1.Rows[0]["Drug_Pk"].ToString());
                        }
                        //Updated by naveen 22-Nov-2011
                        //if (Drug_Pk == 0)
                        //{
                        if (theDT1.Rows[0]["Id"] != System.DBNull.Value)
                        {
                            GenericId = Convert.ToInt32(theDT1.Rows[0]["Id"].ToString());
                        }
                        //}
                        if (ViewState["Abbrevation"] != null)
                        {
                            txtDrugAbbre.Text = ViewState["Abbrevation"].ToString();
                        }
                        else
                        {
                            txtDrugAbbre.Text = "";
                        }
                        if (theDT1.Rows[0]["DrugTypeId"] != System.DBNull.Value)
                        {
                            DrugTypeId = Convert.ToInt32(theDT1.Rows[0]["DrugTypeId"].ToString());
                            ddDrugTypeID.SelectedValue = DrugTypeId.ToString();
                            ddDrugTypeID.Enabled = false;
                        }
                    }
                    DataSet theExistDS = (DataSet)DrugManager.GetGenericDrug(Drug_Pk, GenericId, DrugTypeId);
                    DataSet theExistStrengthDS = new DataSet();
                    if (theExistDS.Tables.Count != 0)
                    {
                        //----rupesh 18-oct for activate/inactive generics starts-------
                        if (theExistDS.Tables[0].Rows.Count != 0)
                        {
                            if (theExistDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "True")
                                ddStatus.SelectedIndex = 1;
                            else
                                ddStatus.SelectedIndex = 0;
                        }
                        //----rupesh 18-oct for activate/inactive generics ends-------

                        if (theExistDS.Tables[1].Rows.Count != 0)
                        {
                            if (theExistDS.Tables[1].Rows[0]["DrugName"] != System.DBNull.Value)
                            {
                                txtDrugName.Text = theExistDS.Tables[1].Rows[0]["DrugName"].ToString();
                                ddStatus.SelectedValue = theExistDS.Tables[1].Rows[0]["DeleteFlag"].ToString();
                            }
                            else
                            {
                                txtDrugName.Text = "";
                            }
                        }

                        //Update by Naveen 22-Nov-2011
                        //if (DrugTypeId == 37)
                        //{
                        if (theExistDS.Tables[2].Rows.Count != 0)
                        {
                            BindManager.BindList(lstStrength, theExistDS.Tables[2], "Name", "Id");
                        }

                        if (theExistDS.Tables[3].Rows.Count != 0)
                        {
                            //BindManager.BindList(lstFrequency, theExistDS.Tables[3], "Name", "Id");
                        }
                        //}
                        else if (DrugTypeId == 60)
                        {
                            if (theExistDS.Tables[7].Rows.Count != 0)
                            {
                                BindManager.BindList(lstshedule, theExistDS.Tables[7], "Name", "Id");
                            }
                        }
                        else
                        {
                            if (theExistDS.Tables[4].Rows.Count != 0)
                            {
                                if (theExistDS.Tables[4].Rows[0]["MaxDose"] != System.DBNull.Value)
                                {
                                    // maxDosetxt.Text = theExistDS.Tables[4].Rows[0]["MaxDose"].ToString();
                                }

                                if (theExistDS.Tables[4].Rows[0]["MinDose"] != System.DBNull.Value)
                                {
                                    //  minDosetxt.Text = theExistDS.Tables[4].Rows[0]["MinDose"].ToString();
                                }
                            }
                        }
                        ViewState["ExistDS"] = theExistDS;
                        ViewState["DrugTypeId"] = DrugTypeId;
                        if (DrugTypeId == 60)
                        {
                            vaccshow.Visible = true;
                            arvShow.Visible = true;
                            arvShow1.Visible = false;
                        }
                        /////////////////
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Makes the selected generic table.
        /// </summary>
        /// <returns></returns>
        private DataTable MakeSelectedGenericTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));
            theDT.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
            return theDT;
        }
    }
}