using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
namespace IQCare.Web.Reports
{
    public partial class frmReportCustomNew : System.Web.UI.Page
    {
        Panel theMain = new Panel();
        DataSet dsTemp = new DataSet();
        DataSet dsCustomReport;
        DataSet dsExistingReport;
        DataTable dtCustomReportJoin;
        Int32 j;//dt.rows.count
        int gPanelId;
        int gRowId;
        //String gFldID;
        Boolean gIsLT = false;
        IReports CustomReport;
        private string theReportMode = string.Empty;
        private string ExportInPDF = string.Empty;
        string ModuleId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblMark") as Label).Text = "»";
                //(Master.FindControl("lblheader") as Label).Text = "Custom Reports";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Custom Reports";

                this.btnNewCategory.Attributes.Add("onclick", "javascript:document.getElementById('" + txtNewCategory.ClientID + "').style.visibility ='visible';document.getElementById('" + txtNewCategory.ClientID + "').focus();return true;");
                this.btnSaveReport.Attributes.Add("onclick", "javascript:return ValidatePage('" + txtTitle.ClientID + "','" + ddCategory.ClientID + "','" + txtNewCategory.ClientID + "');");
                //this.btnExportParameters.Attributes.Add("onclick", "javascript:return ValidatePage('" + txtTitle.ClientID + "','" + ddCategory.ClientID + "','" + txtNewCategory.ClientID + "');");
                //this.btnExportParameters.Attributes.Add("onclick", "javascript:return ValidatePage('" + txtTitle.ClientID + "','" + ddCategory.ClientID + "','" + txtNewCategory.ClientID + "');");

                if (txtNewCategory.Value != "")
                {
                    txtNewCategory.Attributes.Add("style", "visibility: visible");
                }
                DataTable theDTModule = (DataTable)Session["AppModule"];

                ModuleId = "1,2";

                //foreach (DataRow theDR in theDTModule.Rows)
                //{
                //    if (ModuleId == "")
                //        ModuleId = theDR["ModuleId"].ToString();
                //    else
                //        ModuleId = ModuleId + "," + theDR["ModuleId"].ToString();
                //}
                if (Page.IsPostBack != true)
                {
                    if (Request.QueryString["Import"] != null)
                    {
                        if (Request.QueryString["Import"] == "Yes")
                        {
                            ViewState["Mode"] = "U";
                        }
                        else
                        {
                            ViewState["Mode"] = "N";
                        }
                    }
                    else
                    {
                        ViewState["Mode"] = "N";
                    }
                    DataSet dsCategory;
                    CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    dsCategory = CustomReport.GetAllCategory();
                    ddCategory.DataSource = dsCategory.Tables[0];
                    ddCategory.DataTextField = "CategoryName";
                    ddCategory.DataValueField = "CategoryId";
                    ddCategory.DataBind();
                    ddCategory.Items.Insert(0, new ListItem("Select Category", ""));
                    ViewState["RptTable"] = CreateTable();
                    // for updating or Run existing Report
                    if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"].ToString() != "")
                    {
                        if (Convert.ToInt32(Request.QueryString["ReportId"]) > 0)
                        {
                            ViewState["RptTable"] = null;
                            ViewState["RptTable"] = MakeTableStructure();
                            CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                            dsExistingReport = CustomReport.GetCustomReportData(Convert.ToInt32(Request.QueryString["ReportId"]));
                            FillData(dsExistingReport);
                            //---29Jul08
                            //if (Request.QueryString["ReportImpMode"] != null && Request.QueryString["ReportImpMode"].ToString() != "")
                            //{
                            //    if (Request.QueryString["ReportImpMode"].ToString() == "RIEdit")
                            //    {
                            //        ViewState["Mode"] = "N";
                            ////        CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                            ////        int TotRecDel = CustomReport.DeleteCustomReport(Convert.ToInt32(Request.QueryString["ReportId"]));
                            //    }
                            //}
                            //else
                            //{
                            ViewState["Mode"] = "U";
                            //}
                            theReportMode = ViewState["Mode"].ToString();
                            ViewState["ReportId"] = Request.QueryString["ReportId"].ToString();
                        }
                    }//=====================================
                    else
                    {
                        #region "ImportFileData"
                        if (Session["CustomReportDS"] != null)
                        {
                            ViewState["RptTable"] = null;
                            ViewState["RptTable"] = MakeTableStructure();
                            DataSet FileDS = (DataSet)Session["CustomReportDS"];
                            Session.Remove("CustomReportDS");
                            FillData(FileDS);
                        }
                        else
                        {

                            rdoDynamicQuery.Checked = true;
                            rdoTSQL.Checked = false;
                            btnAddField.Enabled = true;
                            pnlCustomRpt.Enabled = true;
                            txtSQLStatement.Enabled = false;
                        }
                        #endregion
                    }

                }

                CreateControls((DataTable)ViewState["RptTable"]);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);

            }
        }

        protected void rdoDynamicQuery_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDynamicQuery.Checked)
            {
                btnAddField.Enabled = true;
                pnlCustomRpt.Enabled = true;

                txtSQLStatement.Enabled = false;
                txtSQLStatement.Text = "";
                makeQuery();
                //Page_Load(sender, e);
            }
            //else if(rdoTSQL.Checked)
            //{
            //}
        }

        protected void rdoTSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTSQL.Checked)
            {
                //RemoveAllPanels();
                btnAddField.Enabled = false;
                pnlCustomRpt.Enabled = false;
                txtSQLStatement.Enabled = true;
                txtSQLStatement.Text = "";
                //Page_Load(sender, e);

            }
            //else if(rdoDynamicQuery.Checked)
            //{

            //}

        }

        private void CreateControls(DataTable theDT)
        {
            Boolean ShowAndOr;
            Boolean ShowAdd;
            Boolean ShowRemove;

            //ViewState["pnlSpaceId"]=0;


            pnlCustomRpt.Controls.Clear();
            j = theDT.Rows.Count;
            SortDataTable(theDT, "PanelId asc");

            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToInt32(theDR["RowId"]) == 1)
                {
                    //ViewState["fldDS"] = "";
                    gPanelId = Convert.ToInt32(theDR["PanelId"]);
                    theMain = (System.Web.UI.WebControls.Panel)CreateGroupField(theDR);

                }
                else
                {
                    gRowId = Convert.ToInt32(theDR["RowId"]);

                    //----for AndOR
                    if (Convert.ToInt32(theDR["RowId"]) <= 2)
                    {
                        ShowAndOr = false;
                    }
                    else
                    {
                        ShowAndOr = true;
                    }

                    //----for Add Filter
                    DataView dv = new DataView((DataTable)ViewState["RptTable"]);
                    dv.RowFilter = "PanelId=" + gPanelId;
                    int MaxRowNo = dv.Count;

                    if ((Convert.ToInt32(theDR["RowId"]) == MaxRowNo) && (Convert.ToInt32(theDR["RowId"]) != 5))
                    {
                        ShowAdd = true;
                    }
                    else
                    {
                        ShowAdd = false;
                    }

                    if ((Convert.ToInt32(theDR["RowId"])) == MaxRowNo && (Convert.ToInt32(theDR["RowId"]) != 2))
                    {
                        ShowRemove = true;
                    }
                    else
                    {
                        ShowRemove = false;
                    }
                    if (Convert.ToInt32(theDR["RowId"]) >= 2)
                    {
                        CreateConRow(theMain, theDR, false, j, ShowAndOr, ShowAdd, ShowRemove);
                    }
                }

            }

        }

        private Control CreateGroupField(DataRow theDR)
        {
            //------Creating new Table        

            IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            BindFunctions BindManager = new BindFunctions();

            Panel pnlSub = new Panel(); // for each new field
            pnlSub.ID = "pnlSub" + theDR["PanelId"].ToString();
            pnlSub.Width = 900;
            //pnlSub.Height = 100;
            pnlSub.BorderWidth = 1;
            pnlCustomRpt.Controls.Add(pnlSub);

            Panel pnlSpace1 = new Panel(); // for each new group - providing Space
            pnlSpace1.ID = "pnlSpace1" + gPanelId;
            pnlSpace1.Width = 900;
            pnlSpace1.Height = 1;
            pnlSpace1.BorderWidth = 0;
            pnlSub.Controls.Add(pnlSpace1);

            Panel pnl1 = new Panel();
            pnl1.ID = pnlSub.ID + "Fld" + theDR["RowId"].ToString();
            pnl1.Width = 870;
            pnl1.Height = 50;
            pnlSub.Controls.Add(pnl1);


            Label lblSpaceA1 = new Label();
            lblSpaceA1.ID = "lblSpaceA1" + gPanelId + gRowId;
            lblSpaceA1.Width = 15;
            lblSpaceA1.Text = "";
            pnl1.Controls.Add(lblSpaceA1);

            Label lblGroup = new Label();
            lblGroup.ID = "lblGroup" + gPanelId + gRowId;
            lblGroup.Text = "Field Group";
            lblGroup.Font.Bold = true;
            lblGroup.Visible = true;
            pnl1.Controls.Add(lblGroup);

            Label lblSpace1 = new Label();
            lblSpace1.ID = "lblSpace1" + gPanelId + gRowId;
            lblSpace1.Width = 10;
            lblSpace1.Text = "";
            pnl1.Controls.Add(lblSpace1);

            DropDownList ddlGroup = new DropDownList();
            ddlGroup.ID = pnlSub.ID + theDR["RowId"].ToString() + "Grp";

            ddlGroup.SelectedValue = "0";
            DataSet objGroup = new DataSet();
            objGroup = ReportManager.GetAllFieldGroups(Convert.ToInt32(Session["SystemId"].ToString()));
            DataView dv = new DataView();
            dv = objGroup.Tables[0].DefaultView;
            dv.RowFilter = "ModuleId in (0," + ModuleId + ")";
            DataTable dt = new DataTable();
            dt = dv.ToTable();

            BindManager.BindCombo(ddlGroup, dt, "GroupName", "GroupId");
            //BindManager.BindCombo(ddlGroup, ReportManager.GetAllFieldGroups(Convert.ToInt32(Session["SystemId"].ToString())).Tables[0], "GroupName", "GroupId");
            ddlGroup.SelectedIndexChanged += new EventHandler(ddlGroup_SelectedIndexChanged);
            ddlGroup.AutoPostBack = true;
            ddlGroup.Visible = true;
            ddlGroup.Width = 200;
            pnl1.Controls.Add(ddlGroup);


            DataView dvG = new DataView((DataTable)ViewState["RptTable"]);
            dvG.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = 1";

            if ((dvG[0]["GroupId"] != null) && (dvG[0]["GroupId"].ToString() != ""))
            {
                ddlGroup.SelectedValue = dvG[0]["GroupId"].ToString();
            }

            dvG.Dispose();

            Label lblSpace2 = new Label();
            lblSpace2.ID = "lblSpace2" + gPanelId + gRowId;
            //change-alias
            //lblSpace2.Width = 60;
            lblSpace2.Width = 20;
            lblSpace2.Text = "";
            pnl1.Controls.Add(lblSpace2);

            Label lblField = new Label();
            lblField.Text = "Field";
            lblField.Font.Bold = true;
            lblField.Visible = true;
            pnl1.Controls.Add(lblField);

            Label lblSpace3 = new Label();
            lblSpace3.ID = "lblSpace3" + gPanelId + gRowId;
            lblSpace3.Width = 15;
            lblSpace3.Text = "";
            pnl1.Controls.Add(lblSpace3);

            DropDownList ddlField = new DropDownList();
            ddlField.ID = pnlSub.ID + theDR["RowId"].ToString() + "Field";
            ddlField.Visible = true;
            ddlField.AutoPostBack = true;

            if (ViewState["GroupFields" + gPanelId] != null)
            {
                if (ddlField.Items.Count > 0)
                {
                    ddlField.Items.Clear();
                }

                ddlField.SelectedValue = null;
                if (ddlGroup.SelectedIndex != 0)
                    BindManager.BindCombo(ddlField, ((DataSet)ViewState["GroupFields" + gPanelId]).Tables[0], "FieldName", "FieldId");

            }
            else if ((Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"].ToString() != "") || Request.QueryString["Import"] == "Yes")
            {
                if (ddlGroup.SelectedValue.ToString() != "0")
                {
                    ViewState["GroupFields" + gPanelId] = ReportManager.GetFields(Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(Session["SystemId"].ToString()));
                    if (ddlField.Items.Count > 0)
                    {
                        ddlField.Items.Clear();
                    }

                    ddlField.SelectedValue = null;
                    BindManager.BindCombo(ddlField, ((DataSet)ViewState["GroupFields" + gPanelId]).Tables[0], "FieldName", "FieldId");
                }
            }
            ddlField.Width = 200;
            ddlField.SelectedIndexChanged += new EventHandler(ddlField_SelectedIndexChanged);
            pnl1.Controls.Add(ddlField);

            //ddlField.Attributes.Add("onBlur", "return RestoreValue(this.options[this.selectedIndex].value,'" + hdnFld.ClientID + "','" + gPanelId + "');");

            DataView dvF = new DataView((DataTable)ViewState["RptTable"]);
            dvF.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = 1";

            if ((dvF[0]["FieldId"] != null) && (dvF[0]["FieldId"].ToString() != ""))
            {
                ddlField.SelectedValue = "0";
                ddlField.SelectedValue = dvF[0]["FieldId"].ToString();
                //hdnFld.Value = ddlField.SelectedValue;  
            }
            dvF.Dispose();
            ViewState["fldDS" + gPanelId.ToString()] = ddlField.DataSource;


            //gFldID = ddlField.ID;

            Label lblSpace4 = new Label();
            lblSpace4.ID = "lblSpace4" + gPanelId + gRowId;
            //change-alias
            //lblSpace4.Width = 60;
            lblSpace4.Width = 20;
            lblSpace4.Text = "";
            pnl1.Controls.Add(lblSpace4);

            Label lblFunction = new Label();
            lblFunction.Text = "Function";
            lblFunction.Font.Bold = true;
            lblFunction.Visible = true;
            pnl1.Controls.Add(lblFunction);

            Label lblSpace5 = new Label();
            lblSpace5.ID = "lblSpace5" + gPanelId + gRowId;
            lblSpace5.Width = 10;
            lblSpace5.Text = "";
            pnl1.Controls.Add(lblSpace5);

            DropDownList ddlFunc = new DropDownList();
            ddlFunc.ID = pnlSub.ID + theDR["RowId"].ToString() + "Func";
            ddlFunc.Visible = true;
            ddlFunc.AutoPostBack = true;
            DDLFuncBind(ddlFunc);
            pnl1.Controls.Add(ddlFunc);

            ddlFunc.SelectedIndexChanged += new EventHandler(ddlFunc_SelectedIndexChanged);


            DataView dvFn = new DataView((DataTable)ViewState["RptTable"]);
            dvFn.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = 1";

            if ((dvFn[0]["Function"] != null) && (dvFn[0]["Function"].ToString() != ""))
            {
                ddlFunc.SelectedValue = dvFn[0]["Function"].ToString();
            }
            dvFn.Dispose();

            Label lblSpace6 = new Label();
            lblSpace6.ID = "lblSpace6" + gPanelId + gRowId;
            //lblSpace6.Width = 30;
            lblSpace6.Width = 20;
            lblSpace6.Text = "";
            pnl1.Controls.Add(lblSpace6);

            Label lblSort = new Label();
            lblSort.ID = "lblSort" + gPanelId + gRowId;
            lblSort.Text = "Sort";
            lblSort.Font.Bold = true;
            lblSort.Visible = true;
            pnl1.Controls.Add(lblSort);

            Label lblSpace7 = new Label();
            lblSpace7.ID = "lblSpace7" + gPanelId + gRowId;
            lblSpace7.Width = 10;
            lblSpace7.Text = "";
            pnl1.Controls.Add(lblSpace7);

            DropDownList ddlSort = new DropDownList();
            ddlSort.ID = pnlSub.ID + theDR["RowId"].ToString() + "Sort";
            ddlSort.Visible = true;
            ddlSort.AutoPostBack = true;
            ddlSort.SelectedIndexChanged += new EventHandler(ddlSort_SelectedIndexChanged);
            pnl1.Controls.Add(ddlSort);
            DDLSortBind(ddlSort);

            //--------------------checkbox placement------------------------------------------
            //Panel pnlChk = new Panel();
            //pnlChk.ID = pnlSub.ID + "IsDisplay" + theDR["RowId"].ToString();
            //pnlChk.Width = 300;
            //pnlChk.Height = 0;
            // pnlChk.BorderWidth = 1;
            //pnlSub.Controls.Add(pnlChk);

            Label pnlSpaceD = new Label(); // for each new group - providing Space
            pnlSpaceD.ID = "pnlSpaceD" + gPanelId;
            pnlSpaceD.Width = 900;
            pnlSpaceD.Height = 0;
            pnlSpaceD.Visible = false;
            pnlSpaceD.Text = "";
            pnl1.Controls.Add(pnlSpaceD);

            Label lblSpaceC2 = new Label();
            lblSpaceC2.ID = "lblSpaceC2" + gPanelId + theDR["RowId"].ToString();
            lblSpaceC2.Width = 60;
            lblSpaceC2.Text = "";
            pnl1.Controls.Add(lblSpaceC2);


            CheckBox chkDisplay = new CheckBox();
            chkDisplay.ID = pnlSub.ID + theDR["RowId"].ToString() + "chkDisplay";
            chkDisplay.Visible = true;
            chkDisplay.AutoPostBack = true;
            chkDisplay.Checked = true;
            chkDisplay.CheckedChanged += new EventHandler(chkDisplay_CheckedChanged);
            pnl1.Controls.Add(chkDisplay);


            DataView dvC = new DataView((DataTable)ViewState["RptTable"]);
            dvC.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = 1";

            if (dvC[0]["IsDisplay"] != null)
            {
                if (dvC[0]["IsDisplay"].ToString() == "1")
                {
                    chkDisplay.Checked = true;
                }
                else
                {
                    chkDisplay.Checked = false;
                }
            }
            dvC.Dispose();

            Label lblSpaceChk = new Label();
            lblSpaceChk.ID = pnlSub.ID + theDR["RowId"].ToString() + "lblSpaceChk";
            lblSpaceChk.Width = 15;
            lblSpaceChk.Text = "";
            pnl1.Controls.Add(lblSpaceChk);


            Label lblDisplay = new Label();
            lblDisplay.ID = pnlSub.ID + theDR["RowId"].ToString() + "lblDisplay";
            lblDisplay.Width = 100;
            lblDisplay.Text = "Display Field";
            lblDisplay.Font.Bold = true;
            //lblDisplay.ForeColor = System.Drawing.Color.Blue;
            pnl1.Controls.Add(lblDisplay);

            //--------------------------------------------------------------------------------
            //-----------------------Display Name --------------------------------------------
            Label lblDisSpc = new Label();
            lblDisSpc.ID = "lblDisSpc" + gPanelId + gRowId;
            lblDisSpc.Width = 67;
            lblDisSpc.Text = "";
            pnl1.Controls.Add(lblDisSpc);

            Label lblAlias = new Label();
            lblAlias.Text = "Display Name";
            lblAlias.ID = "lblAlias" + gPanelId + gRowId;
            lblAlias.Font.Bold = true;
            lblAlias.Visible = true;
            pnl1.Controls.Add(lblAlias);

            Label lblSpcAlias1 = new Label();
            lblSpcAlias1.ID = "lblSpcAlias" + gPanelId + gRowId;
            lblSpcAlias1.Width = 10;
            lblSpcAlias1.Text = "";
            pnl1.Controls.Add(lblSpcAlias1);


            TextBox txtAlias = new TextBox();
            txtAlias.ID = pnlSub.ID + theDR["RowId"].ToString() + "Alias";
            txtAlias.Width = 200;
            txtAlias.Text = "";
            txtAlias.AutoPostBack = true;
            txtAlias.EnableViewState = true;


            //---populate alias text box
            DataView dvA = new DataView((DataTable)ViewState["RptTable"]);
            dvA.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = 1";

            if ((dvA[0]["Alias"] != null) && (dvA[0]["Alias"].ToString() != ""))
            {
                txtAlias.Text = dvA[0]["Alias"].ToString();
            }
            dvA.Dispose();
            txtAlias.TextChanged += new EventHandler(txtAlias_TextChanged);
            pnl1.Controls.Add(txtAlias);

            //ViewState["Alias" + gPanelId.ToString()] = txtAlias.Text.ToString();


            Label lblSpcAlias2 = new Label();
            lblSpcAlias2.ID = "lblSpcAlias2" + gPanelId + gRowId;
            lblSpcAlias2.Width = 400;
            lblSpcAlias2.Text = "";
            pnl1.Controls.Add(lblSpcAlias2);

            //--------------------------------------------------------------------------------
            DataView dvS = new DataView((DataTable)ViewState["RptTable"]);
            dvS.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = 1";

            if ((dvS[0]["Sort"] != null) && (dvS[0]["Sort"].ToString() != ""))
            {
                ddlSort.SelectedValue = dvS[0]["Sort"].ToString();
            }
            dvS.Dispose();
            if (gPanelId > 1)
            {
                btnRemoveField.Visible = true;
            }
            else
            {
                btnRemoveField.Visible = false;
            }

            return pnlSub;
        }

        void txtAlias_TextChanged(object sender, EventArgs e)
        {
            int PanelNo = ((TextBox)sender).ID.LastIndexOf("A");
            int bPos = ((TextBox)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 3)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((TextBox)sender).ID.Substring(bPos + 1, noOfChar));

            foreach (DataRow dr2 in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr2["PanelId"]) == PanelNo1))
                {
                    dr2["Alias"] = ((TextBox)sender).Text;
                }
            }

            //ViewState["RptTable"] = DT;
            makeQuery();
        }

        void chkDisplay_CheckedChanged(object sender, EventArgs e)
        {
            int PanelNo = ((CheckBox)sender).ID.IndexOf("c");
            int bPos = ((CheckBox)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 3)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((CheckBox)sender).ID.Substring(bPos + 1, noOfChar));

            if (((CheckBox)sender).Checked == true)
            {
                ViewState["chkDisplay" + PanelNo1] = "1";
            }
            else
            {
                ViewState["chkDisplay" + PanelNo1] = "0";
            }
            DataTable DT = (DataTable)ViewState["RptTable"];
            if (ViewState["chkDisplay" + PanelNo1] != null)
            {
                foreach (DataRow theDR in ((DataTable)(ViewState["RptTable"])).Rows)
                {
                    if (Convert.ToInt32(theDR[0]) == PanelNo1)
                        theDR["IsDisplay"] = ViewState["chkDisplay" + PanelNo1];
                }
            }
            ViewState["RptTable"] = DT;
            makeQuery();
        }

        void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PanelNo = ((DropDownList)sender).ID.LastIndexOf("S");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 3)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));

            foreach (DataRow dr2 in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr2["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr2["RowId"]) == 1))
                {
                    if (((DropDownList)sender).SelectedValue.ToString() != "Select")
                        dr2["Sort"] = ((DropDownList)sender).SelectedValue;
                    else
                        dr2["Sort"] = "";
                }
            }
            makeQuery();
        }

        void ddlFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PanelNo = ((DropDownList)sender).ID.IndexOf("F");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 3)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));


            foreach (DataRow dr2 in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr2["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr2["RowId"]) == 1))
                {
                    if (((DropDownList)sender).SelectedValue.ToString() != "Select")
                        dr2["Function"] = ((DropDownList)sender).SelectedValue;
                    else
                        dr2["Function"] = "";
                }
            }
            makeQuery();
        }

        void ddlField_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PanelNo = ((DropDownList)sender).ID.IndexOf("F");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 3)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));
            ViewState["Field" + PanelNo1] = ((DropDownList)sender).SelectedValue;

            int ifld = 0;
            string Vw_Name = "";

            Control ctl = ((DropDownList)sender).Parent;

            DataTable DTReport = (DataTable)ViewState["RptTable"];
            if (ViewState["Field" + PanelNo1] != null)
            {
                DataRow[] theDR = DTReport.Select("PanelId=" + PanelNo1 + " and RowId=1 ");
                if ((theDR.Length > 0) && (theDR[0]["FieldId"].ToString() != ""))
                {
                    ifld = Convert.ToInt32(theDR[0]["FieldId"].ToString());
                    //Vw_Name = theDR[0]["ViewName"].ToString();
                }
            }

            if (ViewState["Field" + PanelNo1] != null)
            {
                //if (PanelNo1 == Convert.ToInt32(arrstring[1]))
                //{
                if ((ViewState["Field" + PanelNo1].ToString() != ifld.ToString()) && (ifld.ToString() != "0"))
                {
                    DataTable DT = (DataTable)ViewState["RptTable"];
                    if (ViewState["Field" + PanelNo1] != null)
                    {
                        DataRow[] theDR = DT.Select("PanelId=" + PanelNo1 + " and FieldId=" + ifld + " ");

                        if (theDR.Length > 1)
                        {
                            for (int i = 0; i < theDR.Length; i++)
                            {
                                DT.Rows.Remove(theDR[i]);
                            }

                            DataRow newDR = DT.NewRow();
                            newDR[0] = Convert.ToInt32(PanelNo1);
                            newDR[1] = "1";

                            DataTable tmpDT = (DataTable)((DropDownList)sender).DataSource;
                            DataView tmpDV = new DataView(tmpDT);
                            tmpDV.RowFilter = "FieldId = " + ((DropDownList)sender).SelectedValue;
                            newDR["ViewName"] = tmpDV[0]["ViewName"].ToString();

                            newDR["IsDisplay"] = "1";
                            DT.Rows.Add(newDR);
                            DataRow theDR1 = DT.NewRow();
                            theDR1[0] = Convert.ToInt32(PanelNo1);
                            theDR1[1] = "2";
                            theDR1["ViewName"] = Vw_Name.ToString();
                            theDR1["IsDisplay"] = "1";
                            DT.Rows.Add(theDR1);
                            //ViewState["Field" + PanelNo1] = null;
                            ViewState["RptTable"] = DT;
                        }
                    }
                }
                //}
            }
            foreach (DataRow dr2 in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr2["PanelId"]) == PanelNo1))
                {
                    dr2["FieldId"] = ((DropDownList)sender).SelectedValue;
                    dr2["FieldName"] = ((DropDownList)sender).SelectedItem.Text.ToString();
                    dr2["Alias"] = ((DropDownList)sender).SelectedItem.Text.ToString(); // same for alias and ddlfield

                    DataTable tmpDT = (DataTable)((DropDownList)sender).DataSource;
                    DataView tmpDV = new DataView(tmpDT);
                    tmpDV.RowFilter = "FieldId = " + ((DropDownList)sender).SelectedValue;
                    dr2["ViewName"] = tmpDV[0]["ViewName"].ToString();

                    if (ViewState["ddlGroupValue" + PanelNo1] != null)
                    {
                        dr2["GroupId"] = ViewState["ddlGroupValue" + PanelNo1].ToString();
                    }
                    //dr2["ViewName"] = ViewState["View" + PanelNo1];

                }
            }
            //change-alias
            //where ddlfield changes alias should change automatically

            foreach (Control x in ctl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    //pnlSub.ID + theDR["RowId"].ToString() + "Field";
                    if (x.ID == "pnlSub" + PanelNo1 + "1" + "Alias")
                    {
                        ((System.Web.UI.WebControls.TextBox)x).Text = ((DropDownList)sender).SelectedItem.Text.ToString();
                        break;
                    }
                }
            }
            Page_Load(sender, e);
            makeQuery();

            //makeQuery();
        }

        void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean blnChange = false;
            int PanelNo = ((DropDownList)sender).ID.IndexOf("G");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 3)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));
            ViewState["ddlGroupValue" + PanelNo1] = ((DropDownList)sender).SelectedValue;

            DataTable DT = (DataTable)ViewState["RptTable"];
            if (ViewState["Field" + PanelNo1] != null)
            {
                DataRow[] theDR = DT.Select("PanelId=" + PanelNo1 + " and FieldId=" + ViewState["Field" + PanelNo1] + " ");

                if (theDR.Length > 1)
                {
                    for (int i = 0; i < theDR.Length; i++)
                    {
                        DT.Rows.Remove(theDR[i]);
                    }

                    DataRow newDR = DT.NewRow();
                    newDR[0] = Convert.ToInt32(PanelNo1);
                    newDR[1] = "1";
                    newDR["IsDisplay"] = "1";
                    DT.Rows.Add(newDR);
                    DataRow theDR1 = DT.NewRow();
                    theDR1[0] = Convert.ToInt32(PanelNo1);
                    theDR1[1] = "2";
                    theDR1["IsDisplay"] = "1";
                    DT.Rows.Add(theDR1);
                    ViewState["Field" + PanelNo1] = null;

                    ViewState["RptTable"] = DT;
                    blnChange = true;
                }

            }

            IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            BindFunctions BindManager = new BindFunctions();
            ViewState["GroupFields"] = null;

            DataSet dsField = new DataSet();
            dsField = ReportManager.GetFields(Convert.ToInt32(((DropDownList)sender).SelectedValue), Convert.ToInt32(Session["SystemId"].ToString()));
            DataView dvField = new DataView();
            dvField = dsField.Tables[0].DefaultView;
            dvField.RowFilter = "ModuleId in (0," + ModuleId + ")";
            DataSet dsNewField = new DataSet();
            dsNewField.Tables.Add(dvField.ToTable());
            ViewState["GroupFields" + PanelNo1] = dsNewField;

            //ViewState["GroupFields" + PanelNo1] = ReportManager.GetFields(Convert.ToInt32(((DropDownList)sender).SelectedValue), Convert.ToInt32(Session["SystemId"].ToString()));


            Control ctl = ((DropDownList)sender).Parent;

            //dont populate field if groupid is 0
            //variable set to 0 if groupid is not selected else the groupid is assigned
            int iGroupIdSelected = 0;

            foreach (DataRow dr2 in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr2["PanelId"]) == PanelNo1))
                {
                    dr2["GroupId"] = ViewState["ddlGroupValue" + PanelNo1].ToString();
                    iGroupIdSelected = (int)dr2["GroupId"];
                }
            }
            //else
            //{
            //IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            //BindFunctions BindManager = new BindFunctions();
            //ViewState["GroupFields"] = null;
            //ViewState["GroupFields" + PanelNo1] = ReportManager.GetFields(Convert.ToInt32(((DropDownList)sender).SelectedValue));
            //Control ctl = ((DropDownList)sender).Parent;
            //foreach (DataRow dr2 in ((DataTable)ViewState["RptTable"]).Rows)
            //{
            //    if ((Convert.ToInt32(dr2["PanelId"]) == PanelNo1))
            //    {
            //        dr2["GroupId"] = ViewState["ddlGroupValue" + PanelNo1].ToString();
            //        dr2["ViewName"] = ((DataSet)ViewState["GroupFields" + PanelNo1]).Tables[0].Rows[0]["ViewName"].ToString();
            //        //break;
            //    }
            //}

            //if groupid is selected only then fill the field
            if (iGroupIdSelected != 0)
            {
                foreach (Control x in ctl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        //pnlSub.ID + theDR["RowId"].ToString() + "Field";
                        if (x.ID == "pnlSub" + PanelNo1 + "1" + "Field")
                        {
                            if (((System.Web.UI.WebControls.DropDownList)x).SelectedValue != null || ((System.Web.UI.WebControls.DropDownList)x).SelectedValue == "0")
                                ((System.Web.UI.WebControls.DropDownList)x).SelectedValue = "0";

                            //BindManager.BindCombo((System.Web.UI.WebControls.DropDownList)x, ((DataTable)ViewState["GroupFields" + PanelNo1]), "FieldName", "FieldId");
                            BindManager.BindCombo((System.Web.UI.WebControls.DropDownList)x, ((DataSet)ViewState["GroupFields" + PanelNo1]).Tables[0], "FieldName", "FieldId");
                            //if (ViewState["ddlfield"] != null)
                            // ((System.Web.UI.WebControls.DropDownList)x).SelectedValue = ViewState["ddlfield"].ToString();
                            break;
                        }
                    }
                }
            }

            if (blnChange == true)
            {
                Page_Load(sender, e);
                makeQuery();
                if (txtSQLStatement.Text.Length < 20)
                    txtSQLStatement.Text = "";
            }
        }

        public void CreateConRow(Panel thePanel, DataRow theDR, Boolean SecondRow, Int32 j, Boolean theShowAndOr, Boolean theShowAdd, Boolean theShowRemove)
        {


            IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            BindFunctions BindManager = new BindFunctions();

            Panel pnl2 = new Panel();
            foreach (Control x in thePanel.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    if (x.ID == thePanel.ID + "Con" + theDR["RowId"].ToString())
                    {
                        thePanel.Controls.Remove((System.Web.UI.WebControls.Panel)x);
                        break;
                    }
                }
            }
            pnl2.ID = thePanel.ID + "Con" + theDR["RowId"].ToString();
            pnl2.Width = 870;
            pnl2.Height = 30;
            thePanel.Controls.Add(pnl2);
            pnl2.Controls.Clear();

            if ((Convert.ToInt32(theDR["RowId"]) >= 3) && (gIsLT == true))
            {
                //dont show
                Label lblSpace131LT = new Label();
                lblSpace131LT.ID = "lblSpace131LT" + gPanelId + gRowId;
                lblSpace131LT.Width = 275;
                lblSpace131LT.Text = "";
                pnl2.Controls.Add(lblSpace131LT);
            }
            else
            {
                if (theShowAndOr == true)
                {
                    Label lblSpace131 = new Label();
                    //lblSpace131.Width = 50;
                    lblSpace131.ID = "lblSpace131" + gPanelId + gRowId;
                    lblSpace131.Width = 0;
                    lblSpace131.Text = "";
                    pnl2.Controls.Add(lblSpace131);

                    DropDownList ddlAndOrNr = new DropDownList();
                    ddlAndOrNr.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "AndOr";
                    ddlAndOrNr.Items.Add("And");
                    ddlAndOrNr.Items.Add("Or");
                    ddlAndOrNr.Width = 50;
                    ddlAndOrNr.AutoPostBack = true;
                    ddlAndOrNr.SelectedIndexChanged += new EventHandler(ddlAndOrNr_SelectedIndexChanged);
                    pnl2.Controls.Add(ddlAndOrNr);


                    DataView dvA = new DataView((DataTable)ViewState["RptTable"]);
                    dvA.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                    if ((dvA[0]["AndOr"] != null) && (dvA[0]["AndOr"].ToString() != ""))
                    {
                        ddlAndOrNr.SelectedValue = dvA[0]["AndOr"].ToString();
                    }
                    dvA.Dispose();


                    Label lblSpace13 = new Label();
                    lblSpace13.ID = "lblSpace13" + gPanelId + gRowId;
                    lblSpace13.Width = 20;
                    lblSpace13.Text = "";
                    pnl2.Controls.Add(lblSpace13);
                }
                else
                {
                    Label lblCntAndOr = new Label();
                    lblCntAndOr.ID = "lblCntAndOr" + gPanelId + gRowId;
                    lblCntAndOr.Width = 70;
                    lblCntAndOr.Text = "";
                    pnl2.Controls.Add(lblCntAndOr);
                }

                Label lblWhere1 = new Label();
                lblWhere1.ID = "lblWhere1" + gPanelId + gRowId;
                lblWhere1.Text = "Where";
                lblWhere1.Font.Bold = true;
                lblWhere1.Width = 35;
                lblWhere1.Visible = true;
                pnl2.Controls.Add(lblWhere1);

                Label lblSpace8 = new Label();
                lblSpace8.ID = "lblSpace8" + gPanelId + gRowId;
                lblSpace8.Width = 10;
                lblSpace8.Text = "";
                pnl2.Controls.Add(lblSpace8);

                //----Operator <,>,= 

                DropDownList ddlOperator1 = new DropDownList();
                ddlOperator1.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "Optr";
                ddlOperator1.Visible = true;
                ddlOperator1.Width = 75;
                ddlOperator1.AutoPostBack = true;
                //ddlOperator1.EnableViewState = true;
                ddlOperator1.SelectedIndexChanged += new EventHandler(ddlOperator1_SelectedIndexChanged);
                pnl2.Controls.Add(ddlOperator1);
                DDLOperatorBind(ddlOperator1);


                DataView dvO = new DataView((DataTable)ViewState["RptTable"]);
                dvO.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                if ((dvO[0]["Operator"] != null) && (dvO[0]["Operator"].ToString() != ""))
                {
                    ddlOperator1.SelectedValue = dvO[0]["Operator"].ToString();
                }
                dvO.Dispose();


                Label lblSpace9 = new Label();
                lblSpace9.ID = "lblSpace9" + gPanelId + gRowId;
                lblSpace9.Width = 20;
                lblSpace9.Text = "";
                pnl2.Controls.Add(lblSpace9);

                //----- value
                Label lblValue1 = new Label();
                lblValue1.ID = "lblValue1" + gPanelId + gRowId;
                lblValue1.Text = "Value";
                lblValue1.Width = 30;
                lblValue1.Font.Bold = true;
                lblValue1.Visible = true;
                //   if ((Convert.ToInt32(theDR["RowId"]) >= 3) && (gIsLT == true))
                //       lblValue1.Visible = false;
                pnl2.Controls.Add(lblValue1);

                Label lblSpace10 = new Label();
                lblSpace10.ID = "lblSpace10" + gPanelId + gRowId;
                lblSpace10.Width = 10;
                lblSpace10.Text = "";
                pnl2.Controls.Add(lblSpace10);


                Int32 iFieldID = 0;
                // //---show appropriate control for Value ie - 4 for text box, 7-dropdown , 8-datepick
                //if (ViewState["ddlfield"] != null && ViewState["ddlfield"].ToString() != "")
                //if hdnFld.Value != null
                gIsLT = false;
                if (theDR.IsNull("FieldId") == false)
                {

                    iFieldID = Convert.ToInt32(theDR["FieldId"].ToString());
                }
                else
                {
                    if (ViewState["Field" + gPanelId] != null)
                    {
                        iFieldID = Convert.ToInt32(ViewState["Field" + gPanelId].ToString());
                    }

                }

                if (iFieldID > 0)
                {
                    //if ((ViewState["ddlfield"].ToString()) == "75")
                    #region "LabCondition"
                    if (iFieldID.ToString() != "")
                    {
                        if (iFieldID.ToString() == "75")
                        {
                            gIsLT = true;
                        }
                        else
                        {
                            gIsLT = false;
                        }
                    }
                    #endregion
                    //Maintaining Type of the field 
                    DataTable theDT = (DataTable)ViewState["fldDS" + gPanelId.ToString()];
                    DataView theDV = new DataView(theDT);

                    if (theDR.IsNull("FieldId") == false)
                    {
                        theDV.RowFilter = "FieldId = " + theDR["FieldId"].ToString();
                    }
                    else
                    {
                        //theDV.RowFilter = "FieldId = " + ViewState["ddlfield"].ToString();
                        theDV.RowFilter = "FieldId = " + iFieldID;
                    }
                    if (theDV.Count > 0)
                    {
                        if ((theDV[0]["ValueType"] != null) && (theDV[0]["ValueType"].ToString() != "") && ((theDV[0]["ValueType"]) != DBNull.Value))
                        {
                            ViewState["View" + gPanelId] = theDV[0]["ViewName"];
                            ViewState["FldName" + gPanelId] = theDV[0]["FieldName"];
                            if (Convert.ToInt32(theDV[0]["ValueType"]) == 4)
                            {
                                TextBox txtValue = new TextBox();
                                txtValue.Visible = true;
                                txtValue.Width = 120;
                                txtValue.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "txtValue";
                                txtValue.Attributes.Add("onchange", "fnChangeText('" + ddlOperator1.ID + "','" + txtValue.ID + "');");
                                //ddlOperator1.Attributes.Add("onchange", "fnChangeCondition('" + ddlOperator1.ID + "','" + txtValue.ID + "');");
                                txtValue.AutoPostBack = true;
                                txtValue.TextChanged += new EventHandler(txtValue_TextChanged);
                                pnl2.Controls.Add(txtValue);

                                DataView dvVT = new DataView((DataTable)ViewState["RptTable"]);
                                dvVT.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                                if ((dvVT[0]["Value"] != null) && (dvVT[0]["Value"].ToString() != ""))
                                {
                                    txtValue.Text = dvVT[0]["Value"].ToString();
                                }
                                dvVT.Dispose();
                            }
                            else if (Convert.ToInt32(theDV[0]["ValueType"]) == 7 || Convert.ToInt32(theDV[0]["ValueType"]) == 6)
                            {
                                DropDownList ddlValue = new DropDownList();
                                ddlValue.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "ddlValue";
                                ddlValue.Visible = true;
                                ddlValue.Attributes.Add("onchange", "fnChangeValue('" + ddlOperator1.ID + "','" + ddlValue.ID + "');");
                                ddlValue.Width = 120;
                                if ((Convert.ToInt32(theDR["RowId"]) >= 3) && (gIsLT == true))
                                    ddlValue.Visible = false;

                                String theViewName = theDV[0]["ViewName"].ToString();
                                //BindManager.BindCombo(ddlValue, ReportManager.GetDropDownValueForField(ddlField.SelectedItem.Text.Trim(),theViewName);
                                //IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                                DataTable theDTValue = (DataTable)ReportManager.GetDropDownValueForField(Convert.ToInt32(theDV[0]["FieldId"]), "[" + theDV[0]["FieldName"].ToString().Trim() + "]", theViewName, Convert.ToInt32(Session["SystemId"].ToString()));

                                /*----insert select 
                                DataRow theDR1 = theDTValue.NewRow();
                                theDR1[0] = "Select";
                                theDTValue.Rows.InsertAt(theDR1, 0);
                                */
                                ddlValue.DataSource = theDTValue;
                                ddlValue.DataTextField = theDTValue.Columns[0].ColumnName;
                                ddlValue.DataValueField = theDTValue.Columns[0].ColumnName;
                                ddlValue.DataBind();
                                ddlValue.Items.Insert(0, new ListItem("Select", ""));
                                ddlValue.AutoPostBack = true;
                                ddlValue.SelectedIndexChanged += new EventHandler(ddlValue_SelectedIndexChanged);
                                pnl2.Controls.Add(ddlValue);

                                DataView dvV = new DataView((DataTable)ViewState["RptTable"]);
                                dvV.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];


                                if ((dvV[0]["Value"] != null) && (dvV[0]["Value"].ToString() != ""))
                                {
                                    if (ddlValue.Items.FindByText(dvV[0]["Value"].ToString()) != null)
                                    {
                                        //ddlValue.SelectedValue = "0";
                                        ddlValue.SelectedValue = dvV[0]["Value"].ToString();
                                    }
                                }
                                else
                                {
                                    DataTable dtDefault = (DataTable)ViewState["RptTable"];
                                    DataRow[] drView = dtDefault.Select("PanelId = " + gPanelId + " ");
                                    if (drView.Length > 0)
                                    {
                                        for (int jj = 0; jj < drView.Length; jj++)
                                        {
                                            drView[jj]["ViewName"] = ViewState["View" + gPanelId].ToString();
                                            drView[jj]["FieldName"] = ViewState["FldName" + gPanelId].ToString();
                                        }
                                    }

                                }
                                dvV.Dispose();
                            }
                            else if (Convert.ToInt32(theDV[0]["ValueType"]) == 5 || Convert.ToInt32(theDV[0]["ValueType"]) == 8)
                            {
                                TextBox DateValue = new TextBox();
                                DateValue.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "DateValue";
                                DateValue.Attributes.Add("onchange", "fnChangeDateValue('" + ddlOperator1.ID + "','" + DateValue.ID + "');");
                                ddlOperator1.Attributes.Add("onchange", "fnChangeCondition('" + ddlOperator1.ID + "','" + DateValue.ID + "');");
                                DateValue.Width = 120;
                                DateValue.Visible = true;
                                Control ctl = (TextBox)DateValue;
                                DateValue.AutoPostBack = true;
                                DateValue.TextChanged += new EventHandler(DateValue_TextChanged);
                                pnl2.Controls.Add(DateValue);
                                DateValue.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3');fnChangeDateValue('" + ddlOperator1.ID + "','" + DateValue.ID + "');");
                                DateValue.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');fnChangeDateValue('" + ddlOperator1.ID + "','" + DateValue.ID + "');");



                                //string theUrl = "frmReportCustomNew.aspx";
                                //DateValue.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')");

                                pnl2.Controls.Add(new LiteralControl("&nbsp;"));


                                Image imgValue = new Image();
                                imgValue.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "imgValue";

                                imgValue.Visible = true;
                                imgValue.ToolTip = "Date Helper";
                                imgValue.ImageUrl = "~/images/cal_icon.gif";

                                pnl2.Controls.Add(imgValue);

                                imgValue.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");


                                DataView dvVD = new DataView((DataTable)ViewState["RptTable"]);
                                dvVD.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                                if ((dvVD[0]["Value"] != null) && (dvVD[0]["Value"].ToString() != ""))
                                {
                                    DateValue.Text = dvVD[0]["Value"].ToString();
                                }
                                dvVD.Dispose();

                            }
                            else
                            {
                                TextBox txtValue = new TextBox();
                                txtValue.Visible = true;
                                txtValue.Width = 120;
                                txtValue.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "txtValue";
                                txtValue.Attributes.Add("onchange", "fnChangeText('" + ddlOperator1.ID + "','" + txtValue.ID + "');");
                                //ddlOperator1.Attributes.Add("onchange", "fnChangeCondition('" + ddlOperator1.ID + "','" + txtValue.ID + "');");
                                txtValue.AutoPostBack = true;
                                txtValue.TextChanged += new EventHandler(txtValue_TextChanged);
                                pnl2.Controls.Add(txtValue);

                                DataView dvVT = new DataView((DataTable)ViewState["RptTable"]);
                                dvVT.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                                if ((dvVT[0]["Value"] != null) && (dvVT[0]["Value"].ToString() != ""))
                                {
                                    txtValue.Text = dvVT[0]["Value"].ToString();
                                }
                                dvVT.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    //show empty textbox
                    TextBox txtValue = new TextBox();
                    txtValue.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "txtValue";
                    txtValue.Width = 120;
                    txtValue.Visible = true;
                    //     if ((Convert.ToInt32(theDR["RowId"]) >= 3) && (gIsLT == true))
                    //         txtValue.Visible = false;
                    pnl2.Controls.Add(txtValue);


                }

            }
            ////////// //--show lab details starts--
            if (gIsLT == true)
            {
                Label lblSpace11LTA = new Label();
                lblSpace11LTA.ID = "lblSpace11LTA" + gPanelId.ToString() + theDR["RowId"].ToString();
                lblSpace11LTA.Width = 10;
                lblSpace11LTA.Text = " ";
                pnl2.Controls.Add(lblSpace11LTA);

                //if row=2 show and Test Result
                //if row=3,4,5 show ddl for And/Or


                //------changed-----

                //Label lblwhereLTB = new Label();
                //lblwhereLTB.ID = "lblwhereLTB" + gPanelId + theDR["RowId"].ToString();
                //lblwhereLTB.Text = "and Test Result";
                //lblwhereLTB.Width = 90;
                //lblwhereLTB.Font.Bold = true;
                //lblwhereLTB.Visible = true;
                //pnl2.Controls.Add(lblwhereLTB);

                //------changed-----
                if (theDR["RowId"].ToString() == "2")
                {
                    Label lblwhereLTB = new Label();
                    lblwhereLTB.ID = "lblwhereLTB" + gPanelId + theDR["RowId"].ToString();
                    lblwhereLTB.Text = "And";
                    lblwhereLTB.Width = 50;
                    lblwhereLTB.Font.Bold = true;
                    lblwhereLTB.Visible = true;
                    pnl2.Controls.Add(lblwhereLTB);

                    Label lblwhereLTBS1 = new Label();
                    lblwhereLTBS1.ID = "lblwhereLTBS1" + gPanelId + theDR["RowId"].ToString();
                    lblwhereLTBS1.Text = "";
                    lblwhereLTBS1.Width = 10;
                    lblwhereLTBS1.Font.Bold = true;
                    lblwhereLTBS1.Visible = true;
                    pnl2.Controls.Add(lblwhereLTBS1);

                    Label lblwhereLTBW2 = new Label();
                    lblwhereLTBW2.ID = "lblwhereLTBW2" + gPanelId + theDR["RowId"].ToString();
                    lblwhereLTBW2.Text = "Test Result";
                    lblwhereLTBW2.Width = 70;
                    lblwhereLTBW2.Font.Bold = true;
                    lblwhereLTBW2.Visible = true;
                    pnl2.Controls.Add(lblwhereLTBW2);


                }
                else
                {
                    DropDownList ddlAndOrNr1 = new DropDownList();
                    ddlAndOrNr1.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "AndOr1";
                    ddlAndOrNr1.Items.Add("And");
                    ddlAndOrNr1.Items.Add("Or");
                    ddlAndOrNr1.Width = 50;
                    ddlAndOrNr1.AutoPostBack = true;
                    ddlAndOrNr1.EnableViewState = true;
                    ddlAndOrNr1.SelectedIndexChanged += new EventHandler(ddlAndOrNr1_SelectedIndexChanged);
                    pnl2.Controls.Add(ddlAndOrNr1);



                    DataView dvA = new DataView((DataTable)ViewState["RptTable"]);
                    dvA.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                    if ((dvA[0]["AndOr1"] != null) && (dvA[0]["AndOr1"].ToString() != ""))
                    {
                        ddlAndOrNr1.SelectedValue = dvA[0]["AndOr1"].ToString();
                    }
                    dvA.Dispose();


                    Label lblwhereLTB = new Label();
                    lblwhereLTB.ID = "LblAndOr1" + gPanelId + theDR["RowId"].ToString();
                    lblwhereLTB.Text = "";
                    lblwhereLTB.Width = 10;
                    lblwhereLTB.Font.Bold = true;
                    lblwhereLTB.Visible = true;
                    pnl2.Controls.Add(lblwhereLTB);

                    Label lblwhereLTB1 = new Label();
                    lblwhereLTB1.ID = "lblwhereLTB1" + gPanelId + theDR["RowId"].ToString();
                    lblwhereLTB1.Text = "Test Result";
                    lblwhereLTB1.Width = 70;
                    lblwhereLTB1.Font.Bold = true;
                    lblwhereLTB1.Visible = true;
                    pnl2.Controls.Add(lblwhereLTB1);


                }

                //----------

                Label lblSpace12LTC = new Label();
                lblSpace12LTC.ID = "lblSpace12LTC" + gPanelId + theDR["RowId"].ToString();
                lblSpace12LTC.Width = 10;
                lblSpace12LTC.Text = " ";
                pnl2.Controls.Add(lblSpace12LTC);

                DropDownList ddlOperator1LT = new DropDownList();
                ddlOperator1LT.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "OptrLT";
                ddlOperator1LT.Visible = true;
                ddlOperator1LT.Width = 75;
                ddlOperator1LT.AutoPostBack = true;
                ddlOperator1LT.EnableViewState = true;
                ddlOperator1LT.SelectedIndexChanged += new EventHandler(ddlOperator1LT_SelectedIndexChanged);
                pnl2.Controls.Add(ddlOperator1LT);
                DDLOperatorBind(ddlOperator1LT);

                DataView dvLTO = new DataView((DataTable)ViewState["RptTable"]);
                dvLTO.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"].ToString();

                if (dvLTO[0]["Operator1"] != null & dvLTO[0]["Operator1"].ToString() != "")
                {
                    ddlOperator1LT.SelectedValue = dvLTO[0]["Operator1"].ToString();
                }
                dvLTO.Dispose();


                Label lblSpace13LT = new Label();
                lblSpace13LT.ID = "lblSpace13LT" + gPanelId + theDR["RowId"].ToString();
                lblSpace13LT.Width = 20;
                lblSpace13LT.Text = "";
                pnl2.Controls.Add(lblSpace13LT);

                ////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////
                #region "Put Control for Lab"

                DataTable theDT = (DataTable)ViewState["fldDS" + gPanelId.ToString()];
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "FieldId = 79";
                if (theDV.Count > 0)
                {
                    if ((theDV[0]["ValueType"] != null) && (theDV[0]["ValueType"].ToString() != "") && ((theDV[0]["ValueType"]) != DBNull.Value))
                    {
                        ViewState["View" + gPanelId] = theDV[0]["ViewName"];
                        ViewState["FldName" + gPanelId] = "Lab Test] as [Lab Test],Rpt_Laboratory.[Test Results";
                        if (Convert.ToInt32(theDV[0]["ValueType"]) == 4)
                        {
                            TextBox txtValue = new TextBox();
                            txtValue.Visible = true;
                            txtValue.Width = 120;
                            txtValue.ID = thePanel.ID + "ConResult" + theDR["RowId"].ToString() + "txtValue";
                            txtValue.AutoPostBack = true;
                            txtValue.TextChanged += new EventHandler(txtLabConValue_TextChanged);
                            pnl2.Controls.Add(txtValue);
                            //string theFieldName;

                            //DataView dvLCon = new DataView((DataTable)ViewState["RptTable"]);
                            //dvLCon.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"].ToString();
                            //if ((dvLCon[0]["Operator1"] != null) || (dvLCon[0]["Operator1"].ToString() != ""))
                            //{
                            //    theFieldName = dvLCon[0]["Value"].ToString();
                            //}
                            //dvLCon.Dispose();

                            DataView dvVT = new DataView((DataTable)ViewState["RptTable"]);
                            dvVT.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];

                            if ((dvVT[0]["Value1"] != null) && (dvVT[0]["Value1"].ToString() != ""))
                            {
                                txtValue.Text = dvVT[0]["Value1"].ToString();
                            }
                            dvVT.Dispose();
                        }
                        else if (Convert.ToInt32(theDV[0]["ValueType"]) == 7)
                        {
                            DropDownList ddlValue = new DropDownList();
                            ddlValue.ID = thePanel.ID + "ConResult" + theDR["RowId"].ToString() + "ddlValue";
                            ddlValue.Visible = true;
                            ddlValue.Width = 120;
                            String theViewName = theDV[0]["ViewName"].ToString();
                            string theFieldName = "";

                            DataView dvLCon = new DataView((DataTable)ViewState["RptTable"]);
                            dvLCon.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"].ToString();

                            if ((dvLCon[0]["Operator1"] != null) || (dvLCon[0]["Operator1"].ToString() != ""))
                            {
                                theFieldName = dvLCon[0]["Value"].ToString();
                            }
                            dvLCon.Dispose();

                            //BindManager.BindCombo(ddlValue, ReportManager.GetDropDownValueForField(ddlField.SelectedItem.Text.Trim(),theViewName);
                            //IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                            DataTable theDTValue;
                            if (theFieldName != "")
                            {
                                theDTValue = (DataTable)ReportManager.GetDropDownValueForField(79, theFieldName, theViewName, Convert.ToInt32(Session["SystemId"].ToString()));
                            }
                            else
                            {
                                theDTValue = (DataTable)ReportManager.GetDropDownValueForField(79, "", theViewName, Convert.ToInt32(Session["SystemId"].ToString()));
                            }

                            /*----insert select 
                            DataRow theDR1 = theDTValue.NewRow();
                            theDR1[0] = "Select";
                            theDTValue.Rows.InsertAt(theDR1, 0);
                            */
                            ddlValue.DataSource = theDTValue;
                            ddlValue.DataTextField = theDTValue.Columns[0].ColumnName;
                            ddlValue.DataValueField = theDTValue.Columns[0].ColumnName;
                            ddlValue.DataBind();
                            ddlValue.Items.Insert(0, new ListItem("Select", ""));
                            ddlValue.AutoPostBack = true;
                            ddlValue.SelectedIndexChanged += new EventHandler(ddlLabTestValue_SelectedIndexChanged);
                            pnl2.Controls.Add(ddlValue);


                            DataView dvVLT = new DataView((DataTable)ViewState["RptTable"]);
                            dvVLT.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"].ToString();

                            if ((dvVLT[0]["Value1"] != null) && (dvVLT[0]["Value1"].ToString() != ""))
                            {
                                if (ddlValue.Items.FindByText(dvVLT[0]["Value"].ToString()) != null)
                                {
                                    ddlValue.SelectedValue = dvVLT[0]["Value1"].ToString();
                                }
                            }
                            //else
                            //{
                            //    DataTable dtDefault = (DataTable)ViewState["RptTable"];
                            //    DataRow[] drView = dtDefault.Select("PanelId = " + gPanelId + " ");
                            //    if (drView.Length > 0)
                            //    {
                            //        for (int jj = 0; jj < drView.Length; jj++)
                            //        {
                            //            drView[jj]["ViewName"] = ViewState["View" + gPanelId].ToString();
                            //            drView[jj]["FieldName"] = ViewState["FldName" + gPanelId].ToString();
                            //        }
                            //    }

                            //}
                            dvVLT.Dispose();


                            //DataView dvV = new DataView((DataTable)ViewState["RptTable"]);
                            //dvV.RowFilter = "PanelId = " + gPanelId + " and " + "RowId = " + theDR["RowId"];


                            //if ((dvV[0]["Value"] != null) && (dvV[0]["Value"].ToString() != ""))
                            //{
                            //    if (ddlValue.Items.FindByText(dvV[0]["Value"].ToString()) != null)
                            //    {
                            //        //ddlValue.SelectedValue = "0";
                            //        ddlValue.SelectedValue = dvV[0]["Value"].ToString();
                            //    }
                            //}
                            //else
                            //{
                            //    DataTable dtDefault = (DataTable)ViewState["RptTable"];
                            //    DataRow[] drView = dtDefault.Select("PanelId = " + gPanelId + " ");
                            //    if (drView.Length > 0)
                            //    {
                            //        for (int jj = 0; jj < drView.Length; jj++)
                            //        {
                            //            drView[jj]["ViewName"] = ViewState["View" + gPanelId].ToString();
                            //            drView[jj]["FieldName"] = ViewState["FldName" + gPanelId].ToString();
                            //        }
                            //    }

                            //}

                        }
                    }
                }
                //TextBox txtLT = new TextBox();
                //txtLT.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "txtLT";
                ////txtLT.Width = 100;
                //txtLT.Width = 80;
                //txtLT.Visible = true;
                //txtLT.AutoPostBack = true;
                //txtLT.TextChanged += new EventHandler(txtLT_TextChanged);
                //pnl2.Controls.Add(txtLT);

                #endregion
                ////////////////////////////////////////////////////////////////////

            }
            //--show lab details ends--



            Label lblSpace11 = new Label();
            lblSpace11.ID = thePanel.ID + "lbl" + theDR["RowId"].ToString() + "LBL";
            //if ((Convert.ToInt32(theDR["RowId"]) >= 3) && (gIsLT == true))
            if ((gIsLT == true))
            {
                lblSpace11.Width = 80;
                //lblSpace11.Width = 250;
            }
            else
            {
                lblSpace11.Width = 20;
            }

            // lblSpace11.Text = "";
            pnl2.Controls.Add(lblSpace11);

            if (theShowAdd == true)
            {
                UpdatePanel up1 = new UpdatePanel();
                up1.ChildrenAsTriggers = false;
                up1.UpdateMode = UpdatePanelUpdateMode.Conditional;

                LinkButton lnkAdd = new LinkButton();
                lnkAdd.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "LnkAdd";
                lnkAdd.Text = "Add Filter";
                lnkAdd.Click += new EventHandler(lnkAdd_Click);
                lnkAdd.Visible = true;
                pnl2.Controls.Add(lnkAdd);

                PostBackTrigger trig = new PostBackTrigger();

                trig.ControlID = lnkAdd.ID;



                up1.Triggers.Add(trig);
                up1.Visible = false;
                pnl2.Controls.Add(up1);


            }
            if (theShowRemove == true)
            {
                UpdatePanel up1 = new UpdatePanel();
                up1.ChildrenAsTriggers = false;
                up1.UpdateMode = UpdatePanelUpdateMode.Conditional;

                Label lblSpace111 = new Label();
                lblSpace111.ID = "lblSpace111" + gPanelId + theDR["RowId"].ToString();
                lblSpace111.Width = 25;
                lblSpace111.Text = "";
                pnl2.Controls.Add(lblSpace111);

                LinkButton lnkRemove = new LinkButton();
                lnkRemove.ID = thePanel.ID + "Con" + theDR["RowId"].ToString() + "LnkRemove";
                lnkRemove.Visible = true;
                lnkRemove.Text = "Remove";
                lnkRemove.Click += new EventHandler(lnkRemove_Click);
                pnl2.Controls.Add(lnkRemove);

                PostBackTrigger trig = new PostBackTrigger();

                trig.ControlID = lnkRemove.ID;



                up1.Triggers.Add(trig);
                up1.Visible = false;
                pnl2.Controls.Add(up1);

            }

        }

        void DateValue_TextChanged(object sender, EventArgs e)
        {
            int PanelNo = ((TextBox)sender).ID.IndexOf("C");
            int bPos = ((TextBox)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((TextBox)sender).ID.Substring(bPos + 1, noOfChar));

            int Pos = ((TextBox)sender).ID.IndexOf("DateValue");
            int RowPos = Convert.ToInt32(((TextBox)sender).ID.Substring(Pos - 1, 1));


            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if (Convert.ToInt32(dr["PanelId"]) == PanelNo1)
                {
                    if (ViewState["View" + PanelNo1] != null)
                        dr["ViewName"] = ViewState["View" + PanelNo1];
                    if (ViewState["FldName" + PanelNo1] != null)
                        dr["FieldName"] = ViewState["FldName" + PanelNo1].ToString();
                }
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["Value"] = ((TextBox)sender).Text.ToString();
                    break;
                }

            }

            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
        }

        void ddlOperator1LT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PanelNo = ((DropDownList)sender).ID.IndexOf("C");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));
            int Pos = ((DropDownList)sender).ID.IndexOf("O");
            int RowPos = Convert.ToInt32(((DropDownList)sender).ID.Substring(Pos - 1, 1));



            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    if (((DropDownList)sender).SelectedValue == "Select")
                        dr["Operator1"] = "";
                    else
                        dr["Operator1"] = ((DropDownList)sender).SelectedValue;

                    if (ViewState["View" + PanelNo1] != null)
                        dr["ViewName"] = ViewState["View" + PanelNo1];
                    if (ViewState["FldName" + PanelNo1] != null)
                        dr["FieldName"] = ViewState["FldName" + PanelNo1].ToString();
                    if (ViewState["Field" + PanelNo1] != null)
                        dr["FieldId"] = ViewState["Field" + PanelNo1].ToString();

                    //if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) >= 2) && (dr.IsNull("AndOr1") == true) && (dr["AndOr1"].ToString() != ""))
                    if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) >= 2) && ((dr.IsNull("AndOr1") == true) || (dr["AndOr1"].ToString() == "")))
                    {
                        dr["AndOr1"] = "And";
                    }

                    break;
                }

            }
            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();

            //ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            ////CreateControls((DataTable)ViewState["RptTable"]);
        }

        void txtLT_TextChanged(object sender, EventArgs e)
        {
            int PanelNo = ((TextBox)sender).ID.IndexOf("C");
            int bPos = ((TextBox)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((TextBox)sender).ID.Substring(bPos + 1, noOfChar));
            int Pos = ((TextBox)sender).ID.IndexOf("txt");
            int RowPos = Convert.ToInt32(((TextBox)sender).ID.Substring(Pos - 1, 1));

            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    if (RowPos > 2)
                    {
                        if (ViewState["Field" + PanelNo1] != null)
                            dr["FieldId"] = ViewState["Field" + PanelNo1].ToString();
                        if (ViewState["ddlGroupValue" + PanelNo1] != null)
                            dr["GroupId"] = ViewState["ddlGroupValue" + PanelNo1].ToString();
                        if (ViewState["View" + PanelNo1] != null)
                            dr["ViewName"] = ViewState["View" + PanelNo1];
                        if (ViewState["FldName" + PanelNo1] != null)
                            dr["FieldName"] = ViewState["FldName" + PanelNo1].ToString();
                        //---
                    }
                    dr["Value1"] = ((TextBox)sender).Text.ToString();

                    //---
                    //if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) >=2) && (dr.IsNull("AndOr1") == true) && (dr["AndOr1"].ToString() != ""))
                    if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) >= 2) && ((dr.IsNull("AndOr1") == true) || (dr["AndOr1"].ToString() == "")))
                    {
                        dr["AndOr1"] = "And";
                    }

                    break;
                }

            }
            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();

            //ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            ////CreateControls((DataTable)ViewState["RptTable"]);

        }

        void ddlAndOrNr_SelectedIndexChanged(object sender, EventArgs e)
        {

            int PanelNo = ((DropDownList)sender).ID.IndexOf("C");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));

            int Pos = ((DropDownList)sender).ID.IndexOf("A");
            int RowPos = Convert.ToInt32(((DropDownList)sender).ID.Substring(Pos - 1, 1));


            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["AndOr"] = ((DropDownList)sender).SelectedValue;
                    break;
                }

            }
            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
            //CreateControls((DataTable)ViewState["RptTable"]);
        }

        void ddlAndOrNr1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PanelNo = ((DropDownList)sender).ID.IndexOf("C");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));

            int Pos = ((DropDownList)sender).ID.IndexOf("A");
            int RowPos = Convert.ToInt32(((DropDownList)sender).ID.Substring(Pos - 1, 1));


            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["AndOr1"] = ((DropDownList)sender).SelectedValue;
                    break;
                }

            }
            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
        }

        void ddlValue_SelectedIndexChanged(object sender, EventArgs e)
        {

            int PanelNo = ((DropDownList)sender).ID.IndexOf("C");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));
            int Pos = ((DropDownList)sender).ID.IndexOf("ddlValue");
            int RowPos = Convert.ToInt32(((DropDownList)sender).ID.Substring(Pos - 1, 1));

            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["Value"] = ((DropDownList)sender).SelectedValue.ToString();
                    break;
                }
            }

            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
        }

        void ddlLabTestValue_SelectedIndexChanged(object sender, EventArgs e)
        {

            int PanelNo = ((DropDownList)sender).ID.IndexOf("C");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));
            int Pos = ((DropDownList)sender).ID.IndexOf("ddlValue");
            int RowPos = Convert.ToInt32(((DropDownList)sender).ID.Substring(Pos - 1, 1));

            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["Value1"] = ((DropDownList)sender).SelectedValue.ToString();
                    break;
                }
            }

            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
        }

        void txtValue_TextChanged(object sender, EventArgs e)
        {

            int PanelNo = ((TextBox)sender).ID.IndexOf("C");
            int bPos = ((TextBox)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((TextBox)sender).ID.Substring(bPos + 1, noOfChar));

            int Pos = ((TextBox)sender).ID.IndexOf("txtValue");
            int RowPos = Convert.ToInt32(((TextBox)sender).ID.Substring(Pos - 1, 1));


            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if (Convert.ToInt32(dr["PanelId"]) == PanelNo1)
                {
                    if (ViewState["View" + PanelNo1] != null)
                        dr["ViewName"] = ViewState["View" + PanelNo1];
                    if (ViewState["FldName" + PanelNo1] != null)
                        dr["FieldName"] = ViewState["FldName" + PanelNo1].ToString();
                }
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["Value"] = ((TextBox)sender).Text.ToString();
                    break;
                }

            }

            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
            //CreateControls((DataTable)ViewState["RptTable"]);


        }

        void txtLabConValue_TextChanged(object sender, EventArgs e)
        {

            int PanelNo = ((TextBox)sender).ID.IndexOf("C");
            int bPos = ((TextBox)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((TextBox)sender).ID.Substring(bPos + 1, noOfChar));

            int Pos = ((TextBox)sender).ID.IndexOf("txtValue");
            int RowPos = Convert.ToInt32(((TextBox)sender).ID.Substring(Pos - 1, 1));


            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                //if (Convert.ToInt32(dr["PanelId"]) == PanelNo1)
                //{
                //    if (ViewState["View" + PanelNo1] != null)
                //        dr["ViewName"] = ViewState["View" + PanelNo1];
                //    if (ViewState["FldName" + PanelNo1] != null)
                //        dr["FieldName"] = ViewState["FldName" + PanelNo1].ToString();
                //}
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["Value1"] = ((TextBox)sender).Text.ToString();
                    break;
                }
            }

            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            makeQuery();
            //CreateControls((DataTable)ViewState["RptTable"]);


        }

        void ddlOperator1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int PanelNo = ((DropDownList)sender).ID.IndexOf("C");
            int bPos = ((DropDownList)sender).ID.IndexOf("b");
            int noOfChar = 0;
            if (PanelNo == bPos + 2)
            {
                noOfChar = 1;
            }
            else
            {
                noOfChar = 2;
            }
            int PanelNo1 = Convert.ToInt32(((DropDownList)sender).ID.Substring(bPos + 1, noOfChar));
            int Pos = ((DropDownList)sender).ID.IndexOf("O");
            int RowPos = Convert.ToInt32(((DropDownList)sender).ID.Substring(Pos - 1, 1));

            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) == RowPos))
                {
                    dr["Operator"] = ((DropDownList)sender).SelectedValue;
                    if ((((DropDownList)sender).SelectedValue.Trim() == "IS NULL") || (((DropDownList)sender).SelectedValue.Trim() == "IS NOT NULL"))
                    {
                        dr["Value"] = null;
                    }
                    //store And Or
                    //if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) > 2) && (dr.IsNull("AndOr") == true) && (dr["AndOr"].ToString() != ""))
                    if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1) && (Convert.ToInt32(dr["RowId"]) > 2) && (dr.IsNull("AndOr") == true) && (dr["AndOr"].ToString() == ""))
                    {
                        dr["AndOr"] = "And";
                    }

                }
            }
            foreach (DataRow dr in ((DataTable)ViewState["RptTable"]).Rows)
            {
                if ((Convert.ToInt32(dr["PanelId"]) == PanelNo1))
                {
                    if (ViewState["Field" + PanelNo1] != null)
                    {
                        dr["FieldId"] = ViewState["Field" + PanelNo1].ToString();
                        if (ViewState["ddlGroupValue" + PanelNo1] != null)
                            dr["GroupId"] = ViewState["ddlGroupValue" + PanelNo1].ToString();
                        if (ViewState["View" + PanelNo1] != null)
                            dr["ViewName"] = ViewState["View" + PanelNo1];
                        if (ViewState["FldName" + PanelNo1] != null)
                            dr["FieldName"] = ViewState["FldName" + PanelNo1].ToString();
                    }

                }

            }


            ViewState["RptTable"] = ((DataTable)ViewState["RptTable"]);
            //CreateControls((DataTable)ViewState["RptTable"]);
            makeQuery();
        }

        public void lnkAdd_Click(object sender, EventArgs e)
        {
            //ViewState["ddlfield"] = "";
            DataTable DT = (DataTable)ViewState["RptTable"];
            int i = DT.Rows.Count;
            DataRow theDR = DT.Rows[i - 1];

            int PanelNo = ((LinkButton)sender).ID.IndexOf("C");
            int PanelNo1 = Convert.ToInt32(((LinkButton)sender).ID.Substring(PanelNo - 1, 1));
            int Pos = ((LinkButton)sender).ID.IndexOf("L");
            int RowPos = Convert.ToInt32(((LinkButton)sender).ID.Substring(Pos - 1, 1));

            //pnlSub1Con2LnkAdd
            //DropDownList theDList = new DropDownList();
            //theDList.ID = "pnlSub" + PanelNo1.ToString() + "Con1" + "Field" ;

            DataView theDV = new DataView(DT);
            theDV.RowFilter = "PanelId = " + PanelNo1 + " and RowId = " + Convert.ToInt32(RowPos);

            DataRow newDR = DT.NewRow();
            //newDR[0] = Convert.ToInt32(theDR["PanelId"]);
            //newDR[1] = Convert.ToInt32(theDR["RowId"]) + 1;
            newDR[0] = Convert.ToInt32(PanelNo1);
            newDR[1] = Convert.ToInt32(RowPos) + 1;
            if (theDV[0]["GroupId"] != System.DBNull.Value)
                newDR[2] = theDV[0]["GroupId"].ToString();
            if (theDV[0]["FieldId"] == null)
                newDR[3] = theDV[0]["FieldId"].ToString();

            DT.Rows.Add(newDR);

            ViewState["RptTable"] = DT;

            CreateControls((DataTable)ViewState["RptTable"]);
        }

        public void lnkRemove_Click(object sender, EventArgs e)
        {
            DataTable DT = (DataTable)ViewState["RptTable"];
            int i = DT.Rows.Count;
            int PanelNo = ((LinkButton)sender).ID.IndexOf("C");
            int PanelNo1 = Convert.ToInt32(((LinkButton)sender).ID.Substring(PanelNo - 1, 1));
            int Pos = ((LinkButton)sender).ID.IndexOf("Lnk");
            int RowPos = Convert.ToInt32(((LinkButton)sender).ID.Substring(Pos - 1, 1));

            DataRow[] theDR = DT.Select("PanelId=" + PanelNo1 + "  AND RowId=" + RowPos + " ");
            if (theDR.Length > 0)
            {
                DT.Rows.Remove(theDR[0]);
            }
            ViewState["RptTable"] = DT;
            makeQuery();
            Page_Load(sender, e);
            //CreateControls((DataTable)ViewState["RptTable"]);
            //CreateNextFilter_forNonLab(ref tbCustomRpt);
        }

        #region "Field Values"
        protected void populateField(DataRow theDR, Control theContainer)
        {
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    foreach (Control y in x.Controls)
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        {
                            populateField(theDR, x);
                        }
                        else
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                if (x.ID == "pnlSub" + theDR["PanelId"] + theDR["RowId"].ToString() + "Field")
                                {
                                    IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                                    BindFunctions BindManager = new BindFunctions();
                                    BindManager.BindCombo((DropDownList)x, ((DataSet)ViewState["GroupFields"]).Tables[0], "FieldName", "FieldId");
                                }
                            }
                        }
                    }

                }
            }
        }
        private int GroupValue(DataRow theDR, Control theContainer)
        {
            int Value = 0;
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    foreach (Control y in x.Controls)
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        {
                            GroupValue(theDR, x);
                        }
                        else
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                if (x.ID == "pnlSub" + theDR["PanelId"] + theDR["RowId"].ToString() + "Grp")
                                {
                                    Value = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                }
                            }
                        }
                    }

                }
            }
            return Value;
        }

        private int FieldValue(DataRow theDR, Control theContainer)
        {
            int Value = 0;
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    FieldValue(theDR, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        //if (x.ID == "tbCustomRpt" + theDR[0]["PanelId"].ToString() + "_trFR" + theDR[0]["RowId"].ToString() + "_ddlField")
                        if (x.ID == "pnlSub" + theDR["PanelId"].ToString() + theDR["RowId"].ToString() + "Fld")
                        {
                            Value = Convert.ToInt32(((DropDownList)x).SelectedValue);
                        }
                    }
                }
            }
            return Value;
        }

        private string FunctionValue(DataRow[] theDR, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    FunctionValue(theDR, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "tbCustomRpt" + theDR[0]["PanelId"].ToString() + "_trFR" + theDR[0]["RowId"].ToString() + "_ddlFunc")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }

        private string SortValue(DataRow[] theDR, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    SortValue(theDR, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "tbCustomRpt" + theDR[0]["PanelId"].ToString() + "_trFR" + theDR[0]["RowId"].ToString() + "_ddlSort")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }

        private string OperatorValue(DataRow[] theDR, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    OperatorValue(theDR, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "tbCustomRpt" + theDR[0]["PanelId"].ToString() + "_trFR" + theDR[0]["RowId"].ToString() + "_ddlOperator")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }

        private string StoreOperatorValue(Int32 thePanelId, Int32 theRowId, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    StoreOperatorValue(thePanelId, theRowId, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {

                        if (x.ID == "pnlSub" + thePanelId.ToString() + "Con" + theRowId.ToString() + "Optr")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }

        private string StoreFunction(Int32 thePanelId, Int32 theRowId, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    StoreFunction(thePanelId, theRowId, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "pnlSub" + thePanelId.ToString() + "1" + "Func")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }
        private string StoreAndOrValue(Int32 thePanelId, Int32 theRowId, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    StoreAndOrValue(thePanelId, theRowId, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "pnlSub" + thePanelId.ToString() + "Con" + theRowId.ToString() + "AndOr")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }

        private string StoreSort(Int32 thePanelId, Int32 theRowId, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    StoreSort(thePanelId, theRowId, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "pnlSub" + thePanelId.ToString() + theRowId.ToString() + "Sort")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                }
            }
            return Value;
        }
        private string StoreCondValue(Int32 thePanelId, Int32 theRowId, Control theContainer)
        {
            string Value = "";
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    StoreCondValue(thePanelId, theRowId, x);
                }
                else
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID == "pnlSub" + thePanelId.ToString() + "Con" + theRowId.ToString() + "ddlValue")
                        {
                            Value = ((DropDownList)x).SelectedValue;
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID == "pnlSub" + thePanelId.ToString() + "Con" + theRowId.ToString() + "txtValue")
                        {
                            Value = ((TextBox)x).Text;
                        }
                    }

                }
            }
            return Value;
        }
        protected Boolean ANDORSelection(int jj)
        {
            bool _val = false;
            switch (jj)
            {
                case 1:
                    _val = true;
                    break;
                case 2:
                    _val = true;
                    break;
                default:
                    _val = false;
                    break;
            }
            return _val;

        }
        protected Boolean ADDSelection(int jj)
        {
            bool val = false;
            switch (jj)
            {
                case 1:
                    val = true;
                    break;
                case 2:
                    val = false;
                    break;
                case 3:
                    val = false;
                    break;
                case 4:
                    val = false;
                    break;
                default:
                    val = false;
                    break;
            }
            return val;

        }
        protected Boolean RemoveSelection(int jj)
        {
            bool returnval = false;
            switch (jj)
            {
                case 1:
                    returnval = true;
                    break;
                case 3:
                    returnval = false;
                    break;
                case 4:
                    returnval = false;
                    break;
                default:
                    returnval = false;
                    break;
            }
            return returnval;

        }

        #endregion

        #region "Bind Dropdown"

        protected void DDLFuncBind(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add("Select");
            ddl.Items.Add("Group");
            ddl.Items.Add("Sum");
            ddl.Items.Add("Count");
            ddl.Items.Add("Max");
            ddl.Items.Add("Min");
            ddl.Items.Add("Count Distinct");
            ddl.Items.Add("Distinct");
        }
        protected void DDLSortBind(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add("Select");
            ddl.Items.Add("Ascending");
            ddl.Items.Add("Descending");
        }
        protected void DDLOperatorBind(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add("Select");
            ddl.Items.Add("IS NULL");
            ddl.Items.Add("IS NOT NULL");
            ddl.Items.Add("=");
            ddl.Items.Add("!=");
            ddl.Items.Add(">");
            ddl.Items.Add("<");
            ddl.Items.Add("<=");
            ddl.Items.Add(">=");
        }


        #endregion

        protected void btnAddField_Click(object sender, EventArgs e)
        {
            ViewState["ddlfield"] = "";
            DataTable DT = (DataTable)ViewState["RptTable"];

            int i = DT.Rows.Count;
            DataRow theDR = DT.Rows[i - 1];

            DataRow newDR = DT.NewRow();
            newDR[0] = Convert.ToInt32(theDR["PanelId"]) + 1;
            newDR[1] = 1;
            newDR["IsDisplay"] = 1;

            DT.Rows.Add(newDR);
            ViewState["RptTable"] = DT;


            i = DT.Rows.Count;
            theDR = DT.Rows[i - 1];

            newDR = DT.NewRow();
            newDR[0] = Convert.ToInt32(theDR["PanelId"]);
            newDR[1] = 2;
            newDR["IsDisplay"] = 1;

            DT.Rows.Add(newDR);
            ViewState["RptTable"] = DT;

            //ViewState["ddlGroupValue"] = null;
            // ViewState["GroupFields"] = null;    ///From initilizing the group fields on the addition //
            ViewState["ddlfield"] = "0";        ///From initilizing the fields on the addition //
            //hdnFld.Value = "";                                            
            //CreateControls((DataTable)ViewState["RptTable"]);
            //pnlCustomRpt.Controls.Clear();
            makeQuery();
            Page_Load(sender, e);
        }

        protected void btnRemoveField_Click(object sender, EventArgs e)
        {
            DataTable DT = (DataTable)ViewState["RptTable"];
            if (gPanelId > 1)
            {
                DataRow[] theDR = DT.Select("PanelId=" + gPanelId + " ");
                if (theDR.Length > 0)
                {
                    for (int i = 0; i < theDR.Length; i++)
                    {
                        DT.Rows.Remove(theDR[i]);
                    }
                }
                ViewState["Field" + gPanelId] = null;
                ViewState["RptTable"] = DT;
                makeQuery();
                Page_Load(sender, e);
            }
        }

        public DataTable CreateTable()
        {
            // DataSet dsTemp = new DataSet();


            DataTable dtTemp = new DataTable("dtCReport");
            dtTemp.Columns.Add(new DataColumn("PanelId", typeof(int)));          //0
            dtTemp.Columns.Add(new DataColumn("RowId", typeof(int)));            //1 
            dtTemp.Columns.Add("GroupId", System.Type.GetType("System.Int32"));  //2 
            dtTemp.Columns.Add("FieldId", System.Type.GetType("System.Int32"));  //3 
            dtTemp.Columns.Add("Function", System.Type.GetType("System.String"));//4 
            dtTemp.Columns.Add("Sort", System.Type.GetType("System.String"));    //5 
            dtTemp.Columns.Add("AndOr", System.Type.GetType("System.String"));   //6 
            dtTemp.Columns.Add("Operator", System.Type.GetType("System.String"));//7 
            dtTemp.Columns.Add("Value", System.Type.GetType("System.String"));   //8 
            dtTemp.Columns.Add("Operator1", System.Type.GetType("System.String"));//9  
            dtTemp.Columns.Add("Value1", System.Type.GetType("System.String"));  //10    Test Result
            dtTemp.Columns.Add("FieldType", System.Type.GetType("System.Int32"));//11  for Field Control Type
            dtTemp.Columns.Add("ViewName", System.Type.GetType("System.String"));//12 View Name
            dtTemp.Columns.Add("FieldName", System.Type.GetType("System.String"));//13 Field Name
            dtTemp.Columns.Add("AndOr1", System.Type.GetType("System.String"));   //14 for Lab Test
            dtTemp.Columns.Add("IsDisplay", System.Type.GetType("System.String"));   //15 IsDisplay -1 /0 for display
            dtTemp.Columns.Add("Alias", System.Type.GetType("System.String"));//16 Field Alias

            DataRow theDR = dtTemp.NewRow();
            theDR[0] = "1";
            theDR[1] = "1";
            theDR["IsDisplay"] = "1";

            dtTemp.Rows.InsertAt(theDR, 0);
            DataRow theDR1 = dtTemp.NewRow();
            theDR1[0] = "1";
            theDR1[1] = "2";
            theDR1["IsDisplay"] = "1";

            dtTemp.Rows.InsertAt(theDR1, 1);
            return dtTemp;
        }

        public DataTable MakeTableStructure()
        {
            // DataSet dsTemp = new DataSet();

            DataTable dtTemp = new DataTable("dtCReport");
            dtTemp.Columns.Add(new DataColumn("PanelId", typeof(int)));          //0
            dtTemp.Columns.Add(new DataColumn("RowId", typeof(int)));            //1 
            dtTemp.Columns.Add("GroupId", System.Type.GetType("System.Int32"));  //2 
            dtTemp.Columns.Add("FieldId", System.Type.GetType("System.Int32"));  //3 
            dtTemp.Columns.Add("Function", System.Type.GetType("System.String"));//4 
            dtTemp.Columns.Add("Sort", System.Type.GetType("System.String"));    //5 
            dtTemp.Columns.Add("AndOr", System.Type.GetType("System.String"));   //6 
            dtTemp.Columns.Add("Operator", System.Type.GetType("System.String"));//7 
            dtTemp.Columns.Add("Value", System.Type.GetType("System.String"));   //8 
            dtTemp.Columns.Add("Operator1", System.Type.GetType("System.String"));//9  
            dtTemp.Columns.Add("Value1", System.Type.GetType("System.String"));  //10    Test Result
            dtTemp.Columns.Add("FieldType", System.Type.GetType("System.Int32"));//11  for Field Control Type
            dtTemp.Columns.Add("ViewName", System.Type.GetType("System.String"));//12 View Name
            dtTemp.Columns.Add("FieldName", System.Type.GetType("System.String"));//13 Field Name
            dtTemp.Columns.Add("AndOr1", System.Type.GetType("System.String"));   //14 for labtest 
            dtTemp.Columns.Add("IsDisplay", System.Type.GetType("System.String"));   //14
            dtTemp.Columns.Add("Alias", System.Type.GetType("System.String"));   //14

            return dtTemp;
        }

        private static void SortDataTable(DataTable dt, string sort)
        {
            DataTable newDT = dt.Clone();
            int rowCount = dt.Rows.Count;

            DataRow[] foundRows = dt.Select(null, sort);
            for (int i = 0; i < rowCount; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = foundRows[i][j];
                }
                DataRow data_row = newDT.NewRow();
                data_row.ItemArray = arr;
                newDT.Rows.Add(data_row);
            }

            //clear the incoming dt 
            dt.Rows.Clear();

            for (int i = 0; i < newDT.Rows.Count; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = newDT.Rows[i][j];
                }

                DataRow data_row = dt.NewRow();
                data_row.ItemArray = arr;
                dt.Rows.Add(data_row);
            }

        }

        private void makeQuery() // to make dynamic Query
        {
            //if T-SQL is created then dont create dynamic query
            if (rdoTSQL.Checked)
            {
                return;
            }

            //Variables and control declaration
            string theField = ""; //all fields list
            int theFieldId = 0;
            string theAlias = "";
            string theOrderBy = "";
            string theGroupBy = "";
            string theQuery = "Select distinct";
            string theCondition = "";
            string[] arr = new string[10];
            string theViews = "";
            string theSQL = "";
            string strValue = string.Empty;
            //bool CheckAnd;
            Boolean PanelChange = false;


            Boolean LastLTSelected = false;

            IQCareUtils theUtils = new IQCareUtils();
            int Counter = 0;
            txtSQLStatement.Text = "";

            string join = "";
            Int32 pnlId = 1; // to be reInitialised
            foreach (DataRow dr in ((DataTable)(ViewState["RptTable"])).Rows)
            {


                if (Convert.ToInt32(dr["PanelId"]) != pnlId)
                {
                    pnlId = pnlId + 1; // increment - for next run
                    PanelChange = true;
                    Counter = Counter + 1;
                }

                if ((Convert.ToInt32(dr["PanelId"]) == pnlId) && (dr["FieldName"] != null && dr["FieldName"].ToString() != ""))
                {

                    if (dr["FieldName"].ToString().Trim() == "Lab Test")
                    {
                        LastLTSelected = true;
                    }
                    else
                    {
                        LastLTSelected = false;
                    }

                    if (Convert.ToInt32(dr["RowId"]) == 1)
                    {
                        //from first Row get field,function,sort, viewname
                        if (theViews != "")
                        {
                            if (theViews.IndexOf(dr["ViewName"].ToString()) == -1)
                                theViews = theViews + "," + dr["ViewName"].ToString(); // get views list
                        }
                        else
                        {
                            theViews = dr["ViewName"].ToString(); // get views list
                        }
                        theField = dr["ViewName"].ToString() + "." + "[" + dr["FieldName"].ToString().Trim() + "]"; // get field list
                        theFieldId = Convert.ToInt32(dr["FieldId"]);
                        //change-alias
                        theAlias = dr["Alias"].ToString().Trim();

                        //function

                        #region "Functions"
                        if ((dr["Function"] != null) && (dr["Function"].ToString() != "") && (dr["Function"].ToString() != "Select")) // if any Function like sum,count etc selected
                        {
                            if ((dr["Function"].ToString().Trim().ToUpper() == "COUNT"))      //|| (dr["Function"].ToString().Trim().ToUpper() == "SUM") || (dr["Function"].ToString().Trim().ToUpper() == "MAX") || (dr["Function"].ToString().Trim().ToUpper() == "COUNT DISTINCT"))
                            {
                                string fldname = dr["FieldName"].ToString();
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                    {
                                        if (LastLTSelected == false)
                                        {
                                            if (theQuery.Length == 15)
                                                theQuery = theQuery + " Count(" + theField + ") [" + theAlias + "]";
                                            else
                                                theQuery = theQuery + ", Count(" + theField + ") [" + theAlias + "]";
                                        }
                                        else
                                        {
                                            if (theQuery.Length == 15)
                                                theQuery = theQuery + " " + theField + " [" + theAlias + "], Count(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";
                                            else
                                                theQuery = theQuery + ", " + theField + " [" + theAlias + "], Count(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";

                                            if (theGroupBy.ToString() == "")
                                                theGroupBy = " GROUP BY Rpt_Laboratory.[Lab Test]";
                                            else
                                                theGroupBy += ",Rpt_Laboratory.[Lab Test]";
                                        }
                                    }
                                }
                            }
                            else if (dr["Function"].ToString().Trim().ToUpper() == "MAX")
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (LastLTSelected == false)
                                    {
                                        if (theQuery.Length == 15)
                                            theQuery = theQuery + " Max(" + theField + ") [" + theAlias + "]";
                                        else
                                            theQuery = theQuery + ", Max(" + theField + ") [" + theAlias + "]";
                                    }
                                    else
                                    {
                                        if (theQuery.Length == 15)
                                            theQuery = theQuery + " " + theField + " [" + theAlias + "], Max(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";
                                        else
                                            theQuery = theQuery + ", " + theField + " [" + theAlias + "], Max(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";

                                        if (theGroupBy.ToString() == "")
                                            theGroupBy = " GROUP BY Rpt_Laboratory.[Lab Test]";
                                        else
                                            theGroupBy += ",Rpt_Laboratory.[Lab Test]";
                                    }
                                }
                            }
                            else if (dr["Function"].ToString().Trim().ToUpper() == "MIN")
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (LastLTSelected == false)
                                    {
                                        if (theQuery.Length == 15)
                                            theQuery = theQuery + " Min(" + theField + ") [" + theAlias + "]";
                                        else
                                            theQuery = theQuery + ", Min(" + theField + ") [" + theAlias + "]";
                                    }
                                    else
                                    {
                                        if (theQuery.Length == 15)
                                            theQuery = theQuery + " " + theField + " [" + theAlias + "], Min(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";
                                        else
                                            theQuery = theQuery + ", " + theField + " [" + theAlias + "], Min(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";

                                        if (theGroupBy.ToString() == "")
                                            theGroupBy = " GROUP BY Rpt_Laboratory.[Lab Test]";
                                        else
                                            theGroupBy += " , Rpt_Laboratory.[Lab Test]";
                                    }
                                }
                            }
                            else if (dr["Function"].ToString().Trim().ToUpper() == "COUNT DISTINCT")
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (LastLTSelected == false)
                                    {
                                        if (theQuery.Length == 15)
                                        {
                                            theQuery = theQuery.Replace("Select", " Select DISTINCT ") + " COUNT (" + theField + ") as [" + theAlias + "]";
                                        }
                                        else
                                        {
                                            theQuery = theQuery.Replace("Select", " Select DISTINCT ") + ", COUNT (" + theField + ") as [" + theAlias + "]";
                                        }
                                    }
                                    else
                                    {
                                        if (theQuery.Length == 15)
                                        {
                                            theQuery = theQuery.Replace("Select", " Select DISTINCT ") + " " + theField + " [" + theAlias + "] COUNT(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";
                                        }
                                        else
                                        {
                                            theQuery = theQuery.Replace("Select", " Select DISTINCT ") + ", " + theField + " [" + theAlias + "] COUNT(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";
                                        }
                                        if (theGroupBy.ToString() == "")
                                            theGroupBy = " GROUP BY Rpt_Laboratory.[Lab Test]";
                                        else
                                            theGroupBy += " , Rpt_Laboratory.[Lab Test]";
                                    }
                                }
                            }
                            else if (dr["Function"].ToString().Trim().ToUpper() == "DISTINCT")
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (LastLTSelected == false)
                                        if (theQuery.Length == 15)
                                        {
                                            theQuery = theQuery.Replace("Select", " Select DISTINCT ") + " " + theField + " [" + theAlias + "] ";
                                        }
                                        else
                                        {
                                            theQuery = theQuery.Replace("Select", " Select DISTINCT ") + " ," + theField + " [" + theAlias + "] ";
                                        }
                                }
                            }
                            else if (dr["Function"].ToString().Trim().ToUpper() == "SUM")
                            {
                                if (theQuery.Trim().Length != 15)
                                    theQuery += ",";
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (LastLTSelected == false)
                                    {
                                        theQuery = theQuery + " SUM(" + theField + ") as [" + theAlias + "]";
                                    }
                                    else
                                    {
                                        theQuery = theQuery + " " + theField + " [" + theAlias + "], SUM(" + dr["ViewName"].ToString() + ".[Test Results]) [" + theAlias + "]";
                                        if (theGroupBy.ToString() == "")
                                            theGroupBy = " GROUP BY Rpt_Laboratory.[Lab Test],Rpt_Laboratory.[Test Results]";
                                        else
                                            theGroupBy += " , Rpt_Laboratory.[Lab Test],Rpt_Laboratory.[Test Results]";
                                    }
                                }
                            }
                            else if (dr["Function"].ToString().Trim().ToUpper() == "GROUP")
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                {
                                    if (LastLTSelected == false)
                                    {
                                        if (theQuery.Length == 15)
                                            theQuery = theQuery + " " + theField + " as [" + theAlias + "]";
                                        else
                                            theQuery = theQuery + ", " + theField + " as [" + theAlias + "]";

                                        if (theGroupBy.ToString() == "")
                                            theGroupBy = " GROUP BY " + theField;
                                        else
                                            theGroupBy += "," + theField;
                                    }
                                    else
                                    {
                                        if (theQuery.Length == 15)
                                            theQuery = theQuery + " " + theField + " as [" + theAlias + "]";
                                        else
                                            theQuery = theQuery + ", " + theField + " as [" + theAlias + "]";

                                        if (theGroupBy.ToString() == "")
                                            theGroupBy = " GROUP BY Rpt_Laboratory.[Lab Test],Rpt_Laboratory.[Test Results]";
                                        else
                                            theGroupBy += ",Rpt_Laboratory.[Lab Test],Rpt_Laboratory.[Test Results]";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (theQuery.Length != 15)
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                    theQuery = theQuery + ", " + theField + " as [" + theAlias + "]";
                            }
                            else
                            {
                                if (Convert.ToInt32(dr["IsDisplay"]) == 1)
                                    theQuery = theQuery + "  " + theField + " as [" + theAlias + "]";
                            }
                        }
                        #endregion

                        #region "Sort"
                        if (dr["Sort"] != null)
                        {
                            if (PanelChange == false)
                            {
                                if (dr["Sort"].ToString().Trim().ToUpper() == "DESCENDING")
                                {
                                    if (theOrderBy.Length != 0)
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = theOrderBy + "," + "Convert(datetime," + theField + ",101)" + " DESC";
                                        }
                                        else
                                        {
                                            theOrderBy = theOrderBy + "," + theField + " DESC";
                                        }
                                    }
                                    else
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = " ORDER BY " + "Convert(datetime," + theField + ",101)" + " DESC";
                                        }
                                        else
                                        {
                                            theOrderBy = " ORDER BY " + theField + " DESC";
                                        }
                                    }
                                }
                                else if (dr["Sort"].ToString().Trim().ToUpper() == "ASCENDING")
                                {
                                    if (theOrderBy.Length != 0)
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = theOrderBy + "," + "Convert(datetime," + theField + ",101)" + " ASC";
                                        }
                                        else
                                        {
                                            theOrderBy = theOrderBy + "," + theField + " ASC";
                                        }

                                    }
                                    else
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = " ORDER BY " + "Convert(datetime," + theField + ",101)" + " ASC";
                                        }
                                        else
                                        {
                                            theOrderBy = " ORDER BY " + theField + " ASC";
                                        }
                                    }

                                }
                            }
                            else
                            {

                                if (dr["Sort"].ToString().Trim().ToUpper() == "DESCENDING")
                                {
                                    if (theOrderBy.Length != 0)
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = theOrderBy + "," + "Convert(datetime," + theField + ",101)" + " DESC";
                                        }
                                        else
                                        {
                                            theOrderBy = theOrderBy + "," + theField + " DESC";
                                        }
                                    }

                                    else
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = " ORDER BY " + "Convert(datetime," + theField + ",101)" + " DESC";
                                        }
                                        else
                                        {
                                            theOrderBy = " ORDER BY " + theField + " DESC";
                                        }

                                    }

                                }
                                else if (dr["Sort"].ToString().Trim().ToUpper() == "ASCENDING")
                                {
                                    if (theOrderBy.Length != 0)
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = theOrderBy + "," + "Convert(datetime," + theField + ",101)" + " ASC";
                                        }
                                        else
                                        {
                                            theOrderBy = theOrderBy + "," + theField + " ASC";
                                        }
                                    }

                                    else
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Ordered by Date]" || theField == "Rpt_Laboratory.[Reported by Date]")
                                        {
                                            theOrderBy = " ORDER BY " + "Convert(datetime," + theField + ",101)" + " ASC";
                                        }
                                        else
                                        {
                                            theOrderBy = " ORDER BY " + theField + " ASC";
                                        }
                                    }

                                }
                            }
                        }
                        #endregion
                    }
                    //Rows other than 1 - for AndOr, where , value
                    if ((Convert.ToInt32(dr["PanelId"]) == pnlId) && Convert.ToInt32(dr["RowId"]) > 1) //&& (((dr.IsNull("Value") == false) && (dr["Value"].ToString() != "")) || (dr.IsNull("Value1") == false) && (dr["Value1"].ToString() != "")))
                    {
                        //---------------blnValue1
                        if (Convert.ToInt32(dr["RowId"]) >= 2) // if some value is there in TestResult ie value1
                        {

                        }

                        ////////////////////////////////////////////
                        if (Convert.ToInt32(dr["RowId"]) > 2)
                        {
                            if (theCondition.Trim() != "")
                            {
                                int Idxand = theCondition.LastIndexOf("and");
                                int Idxor = theCondition.LastIndexOf("or");
                                if (Idxand > 0)
                                {
                                    theCondition = theCondition.Insert((Idxand + 4), "(");
                                }
                                else if (Idxor > 0)
                                {
                                    theCondition = theCondition.Insert((Idxor + 3), "(");
                                }
                            }
                        }
                        ////////////////////////////////////////////
                        #region "Conditions"
                        if (theFieldId == 75)
                            theField = "Rpt_Laboratory.[Lab Test]";
                        if (dr.IsNull("Operator") == false) // it has data
                        {
                            if (PanelChange == true)
                            {
                                if (IsNumberTextValue(dr["Value"].ToString()))// if numeric in value
                                {
                                    string opr = "";
                                    if (dr["AndOr"].ToString() == "")
                                    {
                                        opr += " and ";
                                    }
                                    else
                                    {
                                        opr += dr["AndOr"].ToString();
                                    }
                                    if (theField == "Rpt_Laboratory.[Lab Test]")
                                    {

                                        if (dr["Operator"].ToString() != "Select")
                                            arr[Counter] = opr + theField + " " + dr["Operator"] + " " + dr["Value"] + " ";
                                    }
                                    else if (theField == "Rpt_Laboratory.[Test Results]")
                                    {

                                        if (dr["Operator"].ToString() != "Select")
                                            arr[Counter] = opr + "ISNUMERIC(" + theField + ")=1 and Convert(decimal, " + theField + ") " + dr["Operator"] + " " + dr["Value"] + " ";
                                    }
                                    else if (dr["GroupId"].ToString() == "26")
                                    {
                                        if (dr["Operator"].ToString() != "Select")
                                        {
                                            theCondition += opr + " " + theField + " " + dr["Operator"];
                                            if (dr["Value"].ToString().Trim() != "")
                                                theCondition += " '" + dr["Value"] + "' ";
                                        }
                                    }
                                    else
                                    {
                                        if (dr["Operator"].ToString() != "Select")
                                        {
                                            theCondition += opr + " " + theField + " " + dr["Operator"];
                                            if (dr["Value"].ToString().Trim() != "")
                                                theCondition += " " + dr["Value"] + " ";
                                        }
                                    }
                                }
                                else // if not numeric in value
                                {
                                    if (dr["Value"].ToString().Length == 11 && IsDate(dr["Value"].ToString()) == true) //Assuming Date 
                                    {
                                        if ((dr["Value"].ToString().Trim().Substring(2, 1) == "-") && (dr["Value"].ToString().Trim().Substring(6, 1) == "-"))
                                        {
                                            strValue = theUtils.MakeDate(dr["Value"].ToString().Trim());
                                            string opr = "";
                                            if (dr["AndOr"].ToString() == "")
                                            {
                                                opr = " and ";
                                            }
                                            else
                                            {
                                                opr = dr["AndOr"].ToString();
                                            }
                                            if (dr["Operator"].ToString() != "Select")
                                                theCondition = theCondition + opr + " Convert(DateTime," + theField + ",103)" + dr["Operator"] + "'" + Convert.ToDateTime(strValue) + "'";
                                        }
                                    }
                                    else
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Test]")//-----------1
                                        {

                                            strValue = dr["Value"].ToString().Trim();
                                            strValue = strValue.Replace("and", "##");
                                            string opr = "";
                                            if (dr["AndOr"].ToString() == "")
                                            {
                                                opr = " and ";
                                            }
                                            else
                                            {
                                                opr = dr["AndOr"].ToString();
                                            }
                                            if (dr["Operator"].ToString() != "Select")
                                                arr[Counter] = opr + " " + theField;
                                            if (strValue != "")
                                                arr[Counter] += " " + dr["Operator"] + " '" + strValue + "' ";
                                        }
                                        else
                                        {
                                            strValue = dr["Value"].ToString().Trim();
                                            strValue = strValue.Replace("and", "##");
                                            string opr = "";
                                            if (dr["AndOr"].ToString() == "")
                                            {
                                                opr = " and ";
                                            }
                                            else
                                            {
                                                opr = dr["AndOr"].ToString();
                                            }
                                            if (dr["Operator"].ToString() != "Select")
                                                theCondition = theCondition + " " + opr + " LTRIM(RTRIM(" + theField + "))  " + dr["Operator"] + " '" + strValue + "'  ";
                                        }

                                    }
                                    PanelChange = false;
                                }
                            }
                            else // if not same panel
                            {
                                if (IsNumberTextValue(dr["Value"].ToString()))
                                {
                                    if (theField == "Rpt_Laboratory.[Lab Test]")
                                    {
                                        string opr = "";
                                        if (dr["AndOr"].ToString() == "")
                                        {
                                            opr = " and ";
                                        }
                                        else
                                        {
                                            opr = dr["AndOr"].ToString();
                                        }
                                        if (dr["Operator"].ToString() != "Select")
                                        {
                                            arr[Counter] += opr + " " + theField + "  " + dr["Operator"];
                                            if (dr["Value"].ToString() != "")
                                                arr[Counter] += " " + dr["Value"] + " ";
                                        }
                                    }
                                    else if (dr["GroupId"].ToString() == "26")
                                    {
                                        string opr = "";
                                        if (dr["AndOr"].ToString() == "")
                                        {
                                            opr = " and ";
                                        }
                                        else
                                        {
                                            opr = dr["AndOr"].ToString();
                                        }
                                        if (dr["Operator"].ToString() != "Select")
                                        {
                                            theCondition = theCondition + opr + " " + theField + "  " + dr["Operator"];

                                            if (dr["Value"].ToString() != "")
                                                theCondition += " '" + dr["Value"] + "' ";
                                        }
                                    }
                                    else
                                    {
                                        string opr = "";
                                        if (dr["AndOr"].ToString() == "")
                                        {
                                            opr = " and ";
                                        }
                                        else
                                        {
                                            opr = dr["AndOr"].ToString();
                                        }

                                        if (dr["Operator"].ToString() != "Select")
                                        {
                                            theCondition = theCondition + opr + " " + theField + "  " + dr["Operator"];

                                            if (dr["Value"].ToString() != "")
                                                theCondition += " " + dr["Value"] + " ";
                                        }
                                    }
                                    //theCondition = theCondition + " " + theField + "  " + dr["Operator"] + " " + dr["Value"] + " ";
                                }
                                else
                                {
                                    //if (dr["Value"].ToString().Length == 11) // Rupesh:28Jul08 : Bug#1042: If length of value=11 then it didnt appear in query
                                    if (dr["Value"].ToString().Length == 11 && IsDate(dr["Value"].ToString()) == true) //Assuming Date 
                                    {
                                        if ((dr["Value"].ToString().Trim().Substring(2, 1) == "-") && (dr["Value"].ToString().Trim().Substring(6, 1) == "-"))
                                        {
                                            strValue = theUtils.MakeDate(dr["Value"].ToString().Trim());
                                            string opr = "";
                                            if (dr["AndOr"].ToString() == "")
                                            {
                                                opr = " and ";
                                            }
                                            else
                                            {
                                                opr = dr["AndOr"].ToString();
                                            }
                                            if (dr["Operator"].ToString() != "Select")
                                                theCondition = theCondition + opr + " Convert(DateTime," + theField + ",103)  " + dr["Operator"] + " '" + Convert.ToDateTime(strValue) + "' ";
                                        }
                                    }
                                    else
                                    {
                                        if (theField == "Rpt_Laboratory.[Lab Test]")
                                        {
                                            strValue = dr["Value"].ToString().Trim();
                                            strValue = strValue.Replace("and", "##");
                                            //theCondition = theCondition + " LTRIM(RTRIM(" + theField + "))  " + dr["Operator"] + " '" + strValue + "' ";
                                            string opr = "";
                                            if (dr["AndOr"].ToString() == "")
                                            {
                                                opr = " and ";
                                            }
                                            else
                                            {
                                                opr = dr["AndOr"].ToString();
                                            }
                                            //arr[Counter] += opr + " LTRIM(RTRIM(" + theField + "))  " + "  " + dr["Operator"] + " '" + strValue + "' ";
                                            if (dr["Operator"].ToString() != "Select")
                                                arr[Counter] += arr[Counter] + opr + " LTRIM(RTRIM(" + theField + "))  " + dr["Operator"] + " '" + strValue + "' ";
                                        }
                                        else
                                        {
                                            strValue = dr["Value"].ToString().Trim();
                                            strValue = strValue.Replace("and", "##");
                                            string opr = "";
                                            if (dr["AndOr"].ToString() == "")
                                            {
                                                opr = " and ";
                                            }
                                            else
                                            {
                                                opr = dr["AndOr"].ToString();
                                            }
                                            if (dr["Operator"].ToString() != "Select")
                                                theCondition = theCondition + " " + opr + "  LTRIM(RTRIM(" + theField + "))  " + dr["Operator"] + " '" + strValue + "' ";

                                        }
                                    }
                                }
                            }
                        }

                        if (Convert.ToInt32(dr["RowId"]) > 2)
                        {
                            if (theCondition.Trim() != "")
                                theCondition = theCondition + ") ";   // And / Or
                        }


                        if (dr.IsNull("Operator1") == false && dr["Operator1"].ToString() != "")
                        {

                            if (theQuery.IndexOf(dr["ViewName"].ToString() + ".[Test Results]") == -1 && Convert.ToInt32(dr["IsDisplay"]) == 1)
                                theQuery = theQuery + ", " + dr["ViewName"].ToString() + ".[Test Results] ";

                            if (theField == "Rpt_Laboratory.[Lab Test]")
                            {

                                if (dr["RowId"].ToString() == "2")//---------------
                                {
                                    string opr = "";
                                    if (dr["AndOr"].ToString() == "")
                                    {
                                        opr = " and ";
                                    }
                                    else
                                    {
                                        opr = dr["AndOr"].ToString();
                                    }
                                    if (IsNumberTextValue(dr["Value1"].ToString()) == true)
                                    {
                                        if (dr["Operator"].ToString() != "Select")
                                            arr[Counter] = arr[Counter] + opr + " ((IsNumeric(" + dr["ViewName"].ToString() + ".[Test Results])=1 and Convert(Decimal," + dr["ViewName"].ToString() + ".[Test Results])  " + dr["Operator1"] + " " + dr["Value1"] + ") )";
                                    }
                                    else
                                    {
                                        if (dr["Operator"].ToString() != "Select")
                                            arr[Counter] = arr[Counter] + opr + " ((IsNumeric(" + dr["ViewName"].ToString() + ".[Test Results])=0 and " + dr["ViewName"].ToString() + ".[Test Results] " + dr["Operator1"] + " '" + dr["Value1"] + "') )";
                                    }
                                }
                                else
                                {
                                    string opr = "";
                                    if (dr["AndOr"].ToString() == "")
                                    {
                                        opr = " and ";
                                    }
                                    else
                                    {
                                        opr = dr["AndOr"].ToString();
                                    }

                                    if (IsNumberTextValue(dr["Value1"].ToString()) == true)
                                    {
                                        if (dr["Operator"].ToString() != "Select")
                                            arr[Counter] = arr[Counter] + opr + " ((IsNumeric(" + dr["ViewName"].ToString() + ".[Test Results])=1 and Convert(Decimal," + dr["ViewName"].ToString() + ".[Test Results])  " + dr["Operator1"] + " " + dr["Value1"] + ") )";
                                    }
                                    else
                                    {
                                        if (dr["Operator"].ToString() != "Select")
                                            arr[Counter] = arr[Counter] + opr + " ((IsNumeric(" + dr["ViewName"].ToString() + ".[Test Results])=0 and " + dr["ViewName"].ToString() + ".[Test Results] " + dr["Operator1"] + " '" + dr["Value1"] + "') )";
                                    }
                                }

                            }
                        }

                        #endregion
                    }
                }

            }

            #region "Join Creation"
            string[] arrView = theViews.Split(',');


            CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            if (arrView.Length > 1)
            {
                DataTable theJoins = new DataTable();
                theJoins.Columns.Add("Criteria", System.Type.GetType("System.String"));
                int j;
                int i;
                for (i = 0; i < arrView.Length; i++)
                {
                    for (j = i; j < arrView.Length; j++)
                    {
                        ////// As the system is binded with patient and Visit, these two views will have
                        ///// high importance than any other view
                        if (theViews.Contains("Rpt_PatientDemographics") == true)
                        {
                            if (arrView[j].ToString() != "Rpt_PatientDemographics")
                                dtCustomReportJoin = CustomReport.GetCustomReportJoin("Rpt_PatientDemographics", arrView[j].ToString(), 0);
                        }
                        #region "Put Joins Together"
                        if (dtCustomReportJoin != null)
                        {
                            foreach (DataRow dr in dtCustomReportJoin.Rows)
                            {
                                if (theJoins.Rows.Count > 0)
                                {
                                    DataRow[] theDR = theJoins.Select("Criteria = '" + dr[0].ToString() + "'");
                                    if (theDR.Length < 1)
                                    {
                                        DataRow JDR = theJoins.NewRow();
                                        JDR[0] = dr[0];
                                        theJoins.Rows.Add(JDR);
                                    }
                                }
                                else
                                {
                                    DataRow JDR = theJoins.NewRow();
                                    JDR[0] = dr[0];
                                    theJoins.Rows.Add(JDR);
                                }
                            }
                        }

                        #endregion
                        if (theViews.Contains("Rpt_Visit") == true)
                        {
                            if (arrView[j].ToString() != "Rpt_Visit")
                            {
                                DataTable theDT = (DataTable)ViewState["RptTable"];
                                DataView theDV1 = new DataView(theDT);
                                theDV1.RowFilter = "FieldId=311";
                                if (theDV1.Count > 0 && arrView[j].ToString() != "Rpt_PatientDemographics")
                                {
                                    dtCustomReportJoin = CustomReport.GetCustomReportJoin("Rpt_Visit", arrView[j].ToString(), 1);
                                }
                                else if (arrView[j].ToString() != "Rpt_PatientDemographics")
                                {
                                    dtCustomReportJoin = CustomReport.GetCustomReportJoin("Rpt_Visit", arrView[j].ToString(), 0);
                                }
                            }
                        }
                        #region "Put Joins Together"
                        if (dtCustomReportJoin != null)
                        {
                            foreach (DataRow dr in dtCustomReportJoin.Rows)
                            {
                                if (theJoins.Rows.Count > 0)
                                {
                                    DataRow[] theDR = theJoins.Select("Criteria = '" + dr[0].ToString() + "'");
                                    if (theDR.Length < 1)
                                    {
                                        DataRow JDR = theJoins.NewRow();
                                        JDR[0] = dr[0];
                                        theJoins.Rows.Add(JDR);
                                    }
                                }
                                else
                                {
                                    DataRow JDR = theJoins.NewRow();
                                    JDR[0] = dr[0];
                                    theJoins.Rows.Add(JDR);
                                }
                            }
                        }

                        #endregion

                        if (theViews.Contains("Rpt_Visit") == false && theViews.Contains("Rpt_PatientDemographics") == false)
                        {
                            dtCustomReportJoin = CustomReport.GetCustomReportJoin(arrView[i].ToString(), arrView[j].ToString(), 0);
                        }

                        #region "Put Joins Together"
                        if (dtCustomReportJoin != null)
                        {
                            foreach (DataRow dr in dtCustomReportJoin.Rows)
                            {
                                if (theJoins.Rows.Count > 0)
                                {
                                    DataRow[] theDR = theJoins.Select("Criteria = '" + dr[0].ToString() + "'");
                                    if (theDR.Length < 1)
                                    {
                                        DataRow JDR = theJoins.NewRow();
                                        JDR[0] = dr[0];
                                        theJoins.Rows.Add(JDR);
                                    }
                                }
                                else
                                {
                                    DataRow JDR = theJoins.NewRow();
                                    JDR[0] = dr[0];
                                    theJoins.Rows.Add(JDR);
                                }
                            }
                        }

                        #endregion

                    }
                }
                DataView theDV = new DataView(theJoins);

                for (i = 0; i < theDV.Count; i++)
                {
                    if (join != "")
                    {
                        join = join + "  and  " + theDV[i]["Criteria"].ToString();
                    }
                    else
                    {
                        join = theDV[i]["Criteria"].ToString();
                    }
                }
                theJoins.Rows.Clear();
            }
            #endregion

            #region "Query Creation"

            theQuery = theQuery + " FROM " + theViews;
            /////////
            string LbCondition = "";
            for (int k = 0; k < arr.Length; k++)
            {
                if (arr[k] != null)
                {
                    if (LbCondition == "")
                    {
                        LbCondition = arr[k];
                    }
                    else
                    {
                        LbCondition = LbCondition + arr[k];
                    }
                }
            }


            //////////
            theSQL = theQuery.Trim();
            if (join.Trim() != "") // || theCondition.Trim() !="")
            {
                theSQL = theSQL + " WHERE " + join;
            }
            if (theCondition.Length > 6)
            {
                if (theSQL.IndexOf("WHERE") == -1)
                    theSQL = theSQL + " WHERE " + theCondition;
                else
                    theSQL = theSQL + theCondition;
            }
            if (LbCondition.Length > 6)
            {
                if (theSQL.IndexOf("WHERE") == -1)
                    theSQL = theSQL + " WHERE " + LbCondition;
                else
                    theSQL = theSQL + LbCondition;
            }
            if (theGroupBy.Trim() != "")
            {
                theSQL = theSQL + theGroupBy;
            }
            if (theOrderBy.Trim() != "")
            {
                theSQL = theSQL + theOrderBy;
            }

            theSQL = theSQL.Replace("and  and", " and ");
            theSQL = theSQL.Replace("WHERE  and", " WHERE ");
            theSQL = theSQL.Replace("AndAnd", " And ");
            theSQL = theSQL.Replace("and    and", " and ");
            theSQL = theSQL.Replace("WHERE   and", " WHERE ");
            theSQL = theSQL.Replace("##", "and");

            //Added- Naveen 23-Sept-2010
            theSQL = theSQL.Replace("DISTINCT  distinct", " DISTINCT ");

            txtSQLStatement.Text = theSQL;
            #endregion
        }

        private bool IsDate(string Date)
        {
            string theMon = Date.Substring(3, 3);
            if (theMon == "Jan" || theMon == "Feb" || theMon == "Mar" || theMon == "Apr" || theMon == "May" || theMon == "Jun" || theMon == "Jul" || theMon == "Jul" || theMon == "Aug" || theMon == "Sep" || theMon == "Oct" || theMon == "Nov" || theMon == "Dec")
            {
                return true;
            }
            return false;
        }

        private bool IsNumberTextValue(string strNumber)
        {
            string theNumber = string.Empty;
            int theCheck = 1;
            theNumber = strNumber.Replace(".", "");

            for (int i = 0; i < theNumber.Length; i++)
            {
                if (Char.IsNumber(theNumber, i))
                {
                    theCheck = 1;
                }
                else
                {
                    theCheck = 0;
                    break;
                }


            }

            if (theCheck == 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private DataSet CreateDataSet()
        {
            DataSet dsCustomReports = new DataSet();

            DataTable dtMstReport = new DataTable("dtMstReport");
            DataTable dtlReportFields = new DataTable("dtlReportFields");
            DataTable dtlReportFilter = new DataTable("dtlReportFilter");

            //============= adding columns to MstReport DataTable ================
            dtMstReport.Columns.Add(new DataColumn("ColumnNo", typeof(int)));
            dtMstReport.Columns.Add(new DataColumn("CategoryId", typeof(int)));
            dtMstReport.Columns.Add(new DataColumn("CategoryName", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("ReportName", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("Description", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("Condition", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("RptType", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("ReportId", typeof(int)));

            //============= adding columns to Report's Field DataTable ================
            dtlReportFields.Columns.Add(new DataColumn("GroupId", typeof(int)));
            dtlReportFields.Columns.Add(new DataColumn("FieldId", typeof(int)));
            dtlReportFields.Columns.Add(new DataColumn("FieldName", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("FieldLabel", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("AggregateFunction", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("IsDisplay", typeof(bool)));
            dtlReportFields.Columns.Add(new DataColumn("Sequence", typeof(int)));
            dtlReportFields.Columns.Add(new DataColumn("Sort", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("ViewName", typeof(string)));

            //============= adding columns to Report's Filter DataTable ================
            dtlReportFilter.Columns.Add(new DataColumn("LinkFieldId", typeof(int)));
            dtlReportFilter.Columns.Add(new DataColumn("Operator", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("FilterValue", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("AndOr", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("Sequence", typeof(int)));
            dtlReportFilter.Columns.Add(new DataColumn("Operator1", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("FilterValue1", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("PanelId", typeof(int)));
            dtlReportFilter.Columns.Add(new DataColumn("AndOr1", typeof(string)));

            dsCustomReports.Tables.Add(dtMstReport);
            dsCustomReports.Tables.Add(dtlReportFields);
            dsCustomReports.Tables.Add(dtlReportFilter);


            return dsCustomReports;
        }

        protected void btnSaveReport_Click(object sender, EventArgs e)
        {
            makeQuery();
            string saveFlag;
            saveFlag = Save();

            hidMessage.Value = "";


            if (saveFlag.Length < 8) // ie contains reportid and not any other message
            {
                CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                DataSet DSCustomReport = CustomReport.GetCustomReport(Convert.ToInt32(saveFlag));
                //DSCustomReport.WriteXmlSchema("c:\\CustomReport.xml");  
                string theRptName = "";

                if (Convert.ToInt32(DSCustomReport.Tables[4].Rows[0][0]) > 0 && Convert.ToInt32(DSCustomReport.Tables[4].Rows[0][0]) < 8)
                {
                    theRptName = "rptPotrait";
                }
                else
                {
                    theRptName = "rptLandscape";
                }

                if (DSCustomReport.Tables[2].Rows.Count > 0)
                {
                    Session.Add("ReportData", DSCustomReport);
                    Response.Redirect("frmReportViewer.aspx?ReportId=" + saveFlag.ToString() + "&rptType=" + theRptName);
                }
                else
                {

                }


                //Server.Transfer("frmReportViewer.aspx?ReportId=" + saveFlag.ToString() + "&rptType=" + rptType, true);
            }
            else if (saveFlag == "No Records")
            {
                hidMessage.Value = "No";
            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = saveFlag.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private string Save()
        {
            //=============== Table 1 (dtMstReport) ==============================
            //makeQuery();

            if (txtTitle.Text.Trim() == "")
            {
                return "Select Report Title First.";
            }

            if (ddCategory.SelectedIndex < 1)
            {
                if (txtNewCategory.Value.Trim() == "")
                {
                    return "Report Category Not Provided.";
                }
            }

            if (txtSQLStatement.Text.Trim() == "")
                return "Nothing to Query on.";

            if (txtSQLStatement.Text.IndexOf("*") > 0)
            {
                return " * is not allowed in the query.";
            }

            try
            {
                CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                //DataTable dtParse = (DataTable)CustomReport.ParseSQLStatement(txtSQLStatement.Text.Trim().ToString());
                string strParse = (string)CustomReport.ParseSQLStatement(txtSQLStatement.Text.Trim().ToString());

                if (strParse != "Valid SQL" && strParse != "No Records")
                {
                    return strParse;
                }
            }
            catch (SqlException err)
            {
                return err.Message.ToString();
            }
            finally
            {
                CustomReport = null;
            }

            try
            {

                CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                DataRow drMstReport;
                string theReportCategory = string.Empty;
                dsCustomReport = CreateDataSet();
                drMstReport = dsCustomReport.Tables["dtMstReport"].NewRow();
                if (this.ddCategory.SelectedValue != null && this.ddCategory.SelectedValue != "")
                {
                    drMstReport["CategoryId"] = Convert.ToInt32(this.ddCategory.SelectedValue);
                    theReportCategory = this.ddCategory.SelectedItem.Text.ToString().Trim();
                }
                else
                {
                    drMstReport["CategoryId"] = 0;
                }

                if (txtNewCategory != null && txtNewCategory.Value != "")
                {
                    // Code for Check Duplicate Report Category

                    DataTable dtCheckCategoryName = new DataTable();
                    dtCheckCategoryName = CustomReport.GetCategory(txtNewCategory.Value.Trim());

                    theReportCategory = txtNewCategory.Value.Trim();

                    if (dtCheckCategoryName != null)
                    {
                        string strview = ViewState["Mode"].ToString();
                        if (strview == "N")
                        {
                            if (dtCheckCategoryName.Rows.Count >= 1)
                            {
                                string message = "";
                                message = "<script> alert('" + txtNewCategory.Value.Trim() + " Category already exists.');</script>";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", message);
                                //return 0;
                                return message.ToString();
                            }
                            else
                            {
                                drMstReport["CategoryName"] = txtNewCategory.Value.Trim();
                            }
                        }
                    }



                    //


                }
                else
                {
                    drMstReport["CategoryName"] = "";
                }

                if ((txtTitle != null) && (txtTitle.Text.Trim() != ""))
                {

                    // Code for Check Duplicate Report Name

                    DataTable dtCheckReportName = new DataTable();

                    string theChkReportName = this.txtTitle.Text.Trim();


                    dtCheckReportName = CustomReport.GetReport(theReportCategory, theChkReportName);


                    if (dtCheckReportName != null)
                    {

                        if ((dtCheckReportName.Rows.Count >= 1) && (ViewState["Mode"].ToString() == "N"))
                        {
                            string message = "";
                            message = "<script> alert('Report title already exists.');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", message);
                            //return 0;
                            return message.ToString();
                        }
                        else
                        {
                            drMstReport["ReportName"] = this.txtTitle.Text.Trim();
                        }
                    }



                }

                //drMstReport["ReportName"] = this.txtTitle.Text.Trim();
                drMstReport["Description"] = this.txtDescription.Text.Trim();
                drMstReport["Condition"] = this.txtSQLStatement.Text.Trim();
                if (this.hidReportid.Value.Trim() != "")
                    drMstReport["Reportid"] = this.hidReportid.Value.Trim();

                if (rdoDynamicQuery.Checked)
                    drMstReport["RptType"] = "DYNAMIC";
                else
                    drMstReport["RptType"] = "TSQL";

                dsCustomReport.Tables["dtMstReport"].Rows.Add(drMstReport);
                //===================================================================
                //=============== Table 1 (dtlReportFields) ==============================

                DataRow drReportFields;
                DataRow drReportFilter;
                if (rdoDynamicQuery.Checked)
                {
                    foreach (DataRow dr in ((DataTable)(ViewState["RptTable"])).Rows)
                    {
                        drReportFields = dsCustomReport.Tables["dtlReportFields"].NewRow();

                        if (dr.IsNull("GroupId") == true)
                        {
                            string message = "";
                            message = "<script> alert('Plase select Group of column : " + Convert.ToString(Convert.ToInt32(dr["PanelId"].ToString())) + "');</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", message);
                            //return 0;
                            return message.ToString();
                        }
                        if (dr["RowId"].ToString() == "1")
                        {
                            //check both GroupId and FieldId should not be empty.
                            if (dr["GroupId"].ToString() != "" && dr["FieldId"].ToString() != "")
                            {

                                drReportFields["GroupId"] = Convert.ToInt32(dr["GroupId"].ToString());
                                drReportFields["FieldId"] = Convert.ToInt32(dr["FieldId"].ToString());
                                //change-alias
                                drReportFields["FieldName"] = dr["FieldName"].ToString();
                                drReportFields["FieldLabel"] = dr["Alias"].ToString();
                                drReportFields["AggregateFunction"] = dr["Function"].ToString();

                                if (dr["IsDisplay"].ToString() == "1")
                                {
                                    drReportFields["IsDisplay"] = true;
                                }
                                else
                                {
                                    drReportFields["IsDisplay"] = false;
                                }

                                drReportFields["Sequence"] = Convert.ToInt32(dr["PanelId"].ToString());
                                drReportFields["Sort"] = dr["Sort"].ToString();
                                drReportFields["ViewName"] = dr["ViewName"].ToString();
                                dsCustomReport.Tables["dtlReportFields"].Rows.Add(drReportFields);
                            }
                        }

                        if (Convert.ToInt32(dr["RowId"].ToString()) >= 2 && (dr.IsNull("Operator") == false || dr.IsNull("Operator1") == false)) //&& ((dr.IsNull("Value") == false) || (dr.IsNull("Value1") == false)))
                        {
                            drReportFilter = dsCustomReport.Tables["dtlReportFilter"].NewRow();
                            drReportFilter["LinkFieldId"] = dr["FieldId"].ToString();
                            drReportFilter["Operator"] = dr["Operator"].ToString();
                            drReportFilter["FilterValue"] = dr["Value"].ToString();
                            drReportFilter["AndOr"] = dr["AndOr"].ToString();
                            drReportFilter["Sequence"] = Convert.ToInt32(dr["RowId"].ToString());
                            drReportFilter["Operator1"] = dr["Operator1"].ToString();
                            drReportFilter["FilterValue1"] = dr["Value1"].ToString();
                            drReportFilter["PanelId"] = dr["PanelId"].ToString();
                            drReportFilter["AndOr1"] = dr["AndOr1"].ToString(); // labtest changed
                            dsCustomReport.Tables["dtlReportFilter"].Rows.Add(drReportFilter);
                            //save here with special condition for row 2
                        }
                    }
                }
                //CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                int theReportId;
                if (ViewState["Mode"] != null && ViewState["Mode"].ToString() == "U")
                {
                    dsCustomReport.Tables["dtMstReport"].Rows[0]["ReportId"] = Convert.ToInt32(ViewState["ReportId"]);
                    CustomReport.SaveCustomReport(dsCustomReport, 1);
                    theReportId = Convert.ToInt32(ViewState["ReportId"]);
                }
                else
                {
                    ViewState["Mode"] = "U";
                    theReportId = CustomReport.SaveCustomReport(dsCustomReport, 0);
                }
                ViewState["ReportId"] = theReportId;
                //return theReportId;
                return theReportId.ToString();
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message.ToString();
            }

        }

        private void FillData(DataSet dsExistingReport)
        {
            if (dsExistingReport != null)
            {
                DataTable theDT = (DataTable)(ViewState["RptTable"]);
                DataRow theRow;
                DataView theDV;
                if (dsExistingReport.Tables[0] != null)
                {
                    this.txtTitle.Text = dsExistingReport.Tables[0].Rows[0]["ReportName"].ToString();
                    this.txtDescription.Text = dsExistingReport.Tables[0].Rows[0]["Description"].ToString();
                    this.ddCategory.SelectedValue = dsExistingReport.Tables[0].Rows[0]["CategoryId"].ToString();
                    this.txtSQLStatement.Text = dsExistingReport.Tables[0].Rows[0]["Condition"].ToString();
                    this.hidReportid.Value = dsExistingReport.Tables[0].Rows[0]["Reportid"].ToString();
                    ViewState["ReportId"] = dsExistingReport.Tables[0].Rows[0]["Reportid"].ToString();
                    //default setting of query type
                    if (dsExistingReport.Tables[0].Rows[0]["RptType"].ToString() == "DYNAMIC")
                    {
                        rdoDynamicQuery.Checked = true;
                        rdoTSQL.Checked = false;
                        btnAddField.Enabled = true;
                        pnlCustomRpt.Enabled = true;
                        txtSQLStatement.Enabled = false;
                    }
                    else
                    {
                        rdoDynamicQuery.Checked = false;
                        rdoTSQL.Checked = true;
                        btnAddField.Enabled = false;
                        pnlCustomRpt.Enabled = false;
                        txtSQLStatement.Enabled = true;
                        ViewState["RptTable"] = CreateTable();
                    }

                }
                if (dsExistingReport.Tables[1] != null)
                {
                    foreach (DataRow theDR in dsExistingReport.Tables[1].Rows)
                    {

                        theRow = theDT.NewRow();

                        theRow["PanelId"] = theDR["Sequence"].ToString();
                        theRow["RowId"] = "1";
                        theRow["GroupId"] = Convert.ToInt32(theDR["GroupId"]);
                        ViewState["ddlGroupValue" + theDR["Sequence"].ToString()] = Convert.ToInt32(theDR["GroupId"]);
                        theRow["FieldId"] = Convert.ToInt32(theDR["FieldId"]);
                        ViewState["Field" + theDR["Sequence"].ToString()] = Convert.ToInt32(theDR["FieldId"]);

                        if (theDR["isDisplay"].ToString() == "1")
                        {
                            theRow["isDisplay"] = "1";
                        }
                        else
                        {
                            theRow["isDisplay"] = "0";
                        }

                        theRow["FieldName"] = theDR["FieldName"];
                        ViewState["FldName" + theDR["Sequence"].ToString()] = theDR["FieldName"].ToString();
                        ViewState["Alias" + theDR["Sequence"].ToString()] = theDR["Alias"].ToString(); //change-alias
                        theRow["ViewName"] = theDR["ViewName"];
                        ViewState["View" + theDR["Sequence"].ToString()] = theDR["ViewName"].ToString();
                        theRow["Function"] = theDR["AggregateFunction"];
                        theRow["Sort"] = theDR["Sort"];
                        theRow["Alias"] = theDR["Alias"].ToString(); //change-alias
                        theDT.Rows.Add(theRow);

                        if (dsExistingReport.Tables[2] != null && dsExistingReport.Tables[2].Rows.Count > 0)
                        {
                            theDV = new DataView(dsExistingReport.Tables[2], "ReportFieldId=" + theDR["ReportFieldId"], "Sequence", DataViewRowState.CurrentRows);
                            if (theDV.Count > 0)
                            {
                                for (int i = 0; i < theDV.Count; i++)
                                {
                                    theRow = theDT.NewRow();
                                    theRow["PanelId"] = Convert.ToInt32(theDR["Sequence"].ToString());
                                    theRow["RowId"] = Convert.ToInt32(theDV[i]["Sequence"].ToString());
                                    theRow["GroupId"] = theDR["GroupId"];
                                    theRow["FieldId"] = theDR["FieldId"];
                                    //theRow["isDisplay"] = theDR["isDisplay"];
                                    if (theDR["isDisplay"].ToString() == "1")
                                    {
                                        theRow["isDisplay"] = "1";
                                    }
                                    else
                                    {
                                        theRow["isDisplay"] = "0";
                                    }
                                    theRow["FieldName"] = theDR["FieldName"];
                                    theRow["ViewName"] = theDR["ViewName"].ToString();
                                    theRow["Function"] = null;
                                    theRow["Sort"] = null;
                                    theRow["AndOr"] = theDV[i]["AndOr"].ToString();
                                    theRow["Operator"] = theDV[i]["Operator"].ToString();
                                    theRow["Value"] = theDV[i]["FilterValue"].ToString();
                                    theRow["Operator1"] = theDV[i]["Operator1"].ToString();
                                    theRow["Value1"] = theDV[i]["FilterValue1"].ToString();
                                    theRow["AndOr1"] = theDV[i]["AndOr1"].ToString();

                                    theDT.Rows.Add(theRow);
                                }
                            }
                            else
                            {
                                theRow = theDT.NewRow();

                                theRow["PanelId"] = theDR["Sequence"].ToString();
                                theRow["RowId"] = "2";
                                theRow["GroupId"] = Convert.ToInt32(theDR["GroupId"]);
                                theRow["FieldId"] = Convert.ToInt32(theDR["FieldId"]);
                                //theRow["isDisplay"] = theDR["isDisplay"];
                                if (theDR["isDisplay"].ToString() == "1")
                                {
                                    theRow["isDisplay"] = "1";
                                }
                                else
                                {
                                    theRow["isDisplay"] = "0";
                                }
                                theRow["FieldName"] = theDR["FieldName"];
                                theRow["ViewName"] = theDR["ViewName"];
                                theRow["Function"] = null;
                                theRow["Sort"] = null;
                                theDT.Rows.Add(theRow);
                            }
                        }
                        else
                        {
                            theRow = theDT.NewRow();

                            theRow["PanelId"] = theDR["Sequence"].ToString();
                            theRow["RowId"] = "2";
                            theRow["GroupId"] = Convert.ToInt32(theDR["GroupId"]);
                            theRow["FieldId"] = Convert.ToInt32(theDR["FieldId"]);
                            //theRow["isDisplay"] = theDR["isDisplay"];
                            theRow["FieldName"] = theDR["FieldName"];
                            theRow["ViewName"] = theDR["ViewName"];
                            theRow["Function"] = null;
                            theRow["Sort"] = null;
                            theDT.Rows.Add(theRow);
                        }
                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //Server.Transfer("frmReportCustom.aspx");
            Response.Redirect("frmReportCustom.aspx?r=no", true);
        }

        //protected void btnExportParameters_Click(object sender, EventArgs e)
        //{
        //    string saveFlag;
        //    //saveFlag contain report id
        //    saveFlag = Save();

        //    string rptType=string.Empty;
        //    if (rdoDynamicQuery.Checked)
        //        rptType = "Dynamic";
        //    else
        //        rptType = "TSQL";


        //    if (saveFlag.Length < 8) // ie contains reportid and not any other message
        //    {
        //        //display report
        //        //CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
        //        //string strParse = (string)CustomReport.ParseSQLStatement(txtSQLStatement.Text.Trim().ToString());
        //        //Response.Redirect("frmReportViewer.aspx?ReportId=" + saveFlag.ToString() + "&rptType=" + rptType, true);

        //        if (this.txtTitle.Text.ToString() != "")
        //        {
        //            DataSet dsExistingReport, dsCustomReport;
        //            CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
        //            dsExistingReport = CustomReport.GetCustomReportData(Convert.ToInt32(saveFlag));
        //            Stream stream = new MemoryStream();
        //            dsExistingReport.WriteXml(stream,XmlWriteMode.WriteSchema);
        //            byte[] Buffer;

        //            Buffer = new byte[stream.Length];
        //            stream.Position = 0;
        //            stream.Read(Buffer, 0, (int)stream.Length);
        //            stream.Close();

        //            Response.Clear();

        //            Response.ContentType = "application/xml";
        //            Response.AddHeader("content-disposition", "attachment; filename=Report.xml");
        //            Response.BinaryWrite(Buffer);

        //            Response.End();

        //            //DataTable dtReportField = null;
        //            //DataTable dtReportFilter = null;

        //            //if (dsExistingReport.Tables.Count > 1 && dsExistingReport.Tables[1] != null)
        //            //{
        //            //    dtReportField = dsExistingReport.Tables[1];
        //            //}
        //            //if (dsExistingReport.Tables.Count > 2 && dsExistingReport.Tables[2] != null)
        //            //{
        //            //    dtReportFilter = dsExistingReport.Tables[2];
        //            //}

        //            //DataRow drMstReport;
        //            //dsCustomReport = CreateDataSet();
        //            //drMstReport = dsCustomReport.Tables["dtMstReport"].NewRow();

        //            //drMstReport["ReportName"] = dsExistingReport.Tables[0].Rows[0]["ReportName"].ToString();
        //            //drMstReport["Description"] = dsExistingReport.Tables[0].Rows[0]["Description"].ToString();
        //            //drMstReport["Condition"] = dsExistingReport.Tables[0].Rows[0]["Condition"].ToString();
        //            //drMstReport["CategoryId"] = dsExistingReport.Tables[0].Rows[0]["CategoryId"].ToString();
        //            //drMstReport["RptType"] = dsExistingReport.Tables[0].Rows[0]["RptType"].ToString();
        //            //dsCustomReport.Tables["dtMstReport"].Rows.Add(drMstReport);

        //            //DataRow drReportFields;
        //            //DataRow drReportFilter;
        //            //DataView dvFilter;
        //            //if (drMstReport["RptType"].ToString() == "DYNAMIC")
        //            //{
        //            //    if (dtReportField != null)
        //            //    {
        //            //        foreach (DataRow dr in dtReportField.Rows)
        //            //        {
        //            //            drReportFields = dsCustomReport.Tables["dtlReportFields"].NewRow();

        //            //            drReportFields["GroupId"] = Convert.ToInt32(dr["GroupId"]);
        //            //            drReportFields["FieldId"] = Convert.ToInt32(dr["FieldId"]);

        //            //            drReportFields["FieldLabel"] = dr["FieldName"].ToString();
        //            //            drReportFields["AggregateFunction"] = dr["AggregateFunction"].ToString();
        //            //            drReportFields["IsDisplay"] = dr["IsDisplay"];
        //            //            drReportFields["Sequence"] = dr["Sequence"];
        //            //            drReportFields["Sort"] = dr["Sort"];
        //            //            drReportFields["ViewName"] = dr["ViewName"];
        //            //            dsCustomReport.Tables["dtlReportFields"].Rows.Add(drReportFields);
        //            //            //===============================================================================
        //            //            dvFilter = new DataView(dtReportFilter, "ReportFieldId=" + dr["ReportFieldId"], "Sequence", DataViewRowState.CurrentRows);
        //            //            if (dvFilter.Count > 0)
        //            //            {
        //            //                for (int i = 0; i < dvFilter.Count; i++)
        //            //                {
        //            //                    drReportFilter = dsCustomReport.Tables["dtlReportFilter"].NewRow();
        //            //                    drReportFilter["LinkFieldId"] = Convert.ToInt32(dr["FieldId"]); //Convert.ToInt32(dr["Sequence"]);
        //            //                    drReportFilter["Operator"] = dvFilter[i]["Operator"].ToString();
        //            //                    drReportFilter["FilterValue"] = dvFilter[i]["FilterValue"].ToString();
        //            //                    drReportFilter["AndOr"] = dvFilter[i]["AndOr"].ToString();
        //            //                    drReportFilter["Sequence"] = dvFilter[i]["Sequence"].ToString();

        //            //                    drReportFilter["Operator1"] = dvFilter[i]["Operator1"].ToString();
        //            //                    drReportFilter["FilterValue1"] = dvFilter[i]["FilterValue1"].ToString();
        //            //                    drReportFilter["PanelId"] = Convert.ToInt32(dr["Sequence"]);
        //            //                    drReportFilter["AndOr1"] = dvFilter[i]["AndOr1"].ToString(); // labtest changed

        //            //                    dsCustomReport.Tables["dtlReportFilter"].Rows.Add(drReportFilter);
        //            //                }
        //            //            }
        //            //            //===============================================================================
        //            //        }
        //            //    }
        //            //}
        //        }
        //    }
        //    if (saveFlag == "No Records")
        //    {
        //        //return;
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["MessageText"] = saveFlag.ToString();
        //        IQCareMsgBox.Show("#C1", theBuilder, this);

        //    }
        //    else
        //    {
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["MessageText"] = saveFlag.ToString();
        //        IQCareMsgBox.Show("#C1", theBuilder, this);
        //    }

        //}
        protected void scm_customreport_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string saveFlag;
            //saveFlag contain report id
            saveFlag = Save();

            string rptType = string.Empty;
            if (rdoDynamicQuery.Checked)
                rptType = "Dynamic";
            else
                rptType = "TSQL";


            if (saveFlag.Length < 8) // ie contains reportid and not any other message
            {

                if (this.txtTitle.Text.ToString() != "")
                {
                    DataSet dsExistingReport;
                    CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    dsExistingReport = CustomReport.GetCustomReportData(Convert.ToInt32(saveFlag));
                    Stream stream = new MemoryStream();
                    dsExistingReport.WriteXml(stream, XmlWriteMode.WriteSchema);
                    byte[] Buffer;

                    Buffer = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(Buffer, 0, (int)stream.Length);
                    stream.Close();

                    Response.Clear();

                    Response.ContentType = "application/xml";
                    Response.AddHeader("content-disposition", "attachment; filename=Report.xml");
                    Response.BinaryWrite(Buffer);

                    Response.End();


                }
            }
            if (saveFlag == "No Records")
            {
                //return;
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = saveFlag.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);

            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = saveFlag.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

        }
    }
}