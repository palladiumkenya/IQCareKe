using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class Customfields : System.Web.UI.Page, ICallbackEventHandler
    {
        public Int32 cfieldID;
        public int ControlID;
        public Hashtable htFeatures;
        public DataTable theDT = null;
        public DataTable theDTCount = null;
        private int codeID = 0;

        // Mst_Code ID
        private string decodeValues = "";

        private string deleteValues = "";
       
        private int icount;
        private string oldLabel;
        private string str, strCallback;
        private string thescript;

        public string GetCallbackResult()
        {
            return str;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            ICustomFields CustomFields;
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomListName(eventArgument.Trim().Replace(" ", "_"));
                if (theDS != null && theDS.Tables[0].Rows.Count > 0)
                {
                    str = theDS.GetXml();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataView theDSView = new DataView();
            theDSView.Table = (DataTable)ViewState["temptable"];
            if (Chkformname.SelectedItem != null)
            {
                string strValue = Chkformname.SelectedItem.Text;
                theDSView.RowFilter = "FeatureName=" + "'" + strValue + "'";
            }
            theDTCount = theDSView.ToTable();
            int rowcount = theDTCount.Rows.Count;

            CreateFeatureTable();

            if (FieldValidation() == false)
            {
                return;
            }

            if (btnAdd.Text.ToString() == "Add Custom Field")
            {
                codeID = 0;
                SaveData();
                ClearAll();
            }
            else if (btnAdd.Text.ToString() == "Edit Custom Field")
            {
                ICustomFields CustomFields;
                Int32 datalength = 0;
                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                    if (ViewState["cfieldID"] != null)
                        cfieldID = (Int32)ViewState["cfieldID"];
                    if (ViewState["mstCodeID"] != null)
                        codeID = (Int32)ViewState["mstCodeID"];
                    if (ViewState["OldLabel"] != null)
                        oldLabel = (string)ViewState["OldLabel"];

                    if ((ControlID.ToString() == "4") || (ControlID.ToString() == "9"))
                    {
                        //Get the decode value on the basis of codeid
                        DataSet decodeDs = new DataSet();
                        decodeDs = CustomFields.GetDecodeValues(codeID);
                        if (decodeDs.Tables.Count > 0)
                        {
                            for (int j = 0; j < decodeDs.Tables[0].Rows.Count; j++)
                            {
                                //dsList = CustomFields.SaveCodeDecode(txtflabel.Text.Trim().Replace(" ","_"), theDT.Rows[j]["Name"].ToString(),j+1, Convert.ToInt32(Session["AppUserId"]));
                                decodeValues = decodeValues + decodeDs.Tables[0].Rows[j][1].ToString() + "/n";
                                deleteValues = deleteValues + decodeDs.Tables[0].Rows[j][2].ToString() + "/n";
                            }
                        }
                    }
                    //icount = CustomFields.DeleteCustomFields(Convert.ToInt32(cfieldID));
                    for (int i = 0; i < Chkformname.Items.Count; i++)
                    {
                        if (Convert.ToInt32(Chkformname.Items[i].Selected) == 1)
                        {
                            //for Numeric Field

                            if (ControlID == 3)
                            {
                                int minvalue;
                                int maxvalue;
                                string unitsvalue;
                                if (txtmin.Text.Trim() == "")
                                {
                                    minvalue = 0;
                                }
                                else
                                {
                                    minvalue = Convert.ToInt32(txtmin.Text.Trim());
                                }

                                if (txtmax.Text.Trim() == "")
                                {
                                    maxvalue = 0;
                                }
                                else
                                {
                                    maxvalue = Convert.ToInt32(txtmax.Text.Trim());
                                }

                                if (txtunits.Text.Trim() == "")
                                {
                                    unitsvalue = string.Empty;
                                }
                                else
                                {
                                    unitsvalue = txtunits.Text.Trim();
                                }
                                datalength = 4;
                                icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 1, Convert.ToInt32(minvalue), Convert.ToInt32(maxvalue), unitsvalue.ToString(), 0, "int", oldLabel.ToString(), 0, Convert.ToInt32(datalength), decodeValues, deleteValues, Convert.ToInt32(Session["SystemId"]), rowcount);
                            }
                            //for Select List or Multi Select List
                            else if (ControlID.ToString() == "4")
                            {
                                datalength = 4;
                                icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 0, 0, 0, string.Empty, Convert.ToInt32(codeID), "int", oldLabel.ToString(), 0, Convert.ToInt32(datalength), decodeValues, deleteValues, Convert.ToInt32(Session["SystemId"]), rowcount);
                            }
                            else if (ControlID.ToString() == "9")
                            {
                                datalength = 4;
                                icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 0, 0, 0, string.Empty, Convert.ToInt32(codeID), "int", oldLabel.ToString(), 1, Convert.ToInt32(datalength), decodeValues, deleteValues, Convert.ToInt32(Session["SystemId"]), rowcount);
                            }
                            else
                            {
                                string dataType = string.Empty;
                                if (ControlID.ToString() == "8")
                                {
                                    dataType = "varchar(250)";
                                    datalength = 250;
                                }
                                else if (ControlID.ToString() == "1")
                                {
                                    dataType = "varchar(100)";
                                    datalength = 100;
                                }
                                else if (ControlID.ToString() == "5")
                                {
                                    dataType = "DateTime";
                                    datalength = 8;
                                }
                                else if (ControlID.ToString() == "6")
                                {
                                    dataType = "bit";
                                    datalength = 1;
                                }
                                icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 0, 0, 0, string.Empty, 0, dataType.ToString(), oldLabel.ToString(), 0, Convert.ToInt32(datalength), decodeValues, deleteValues, Convert.ToInt32(Session["SystemId"]), rowcount);
                            }
                        }
                    }
                    DataSet theDSCust = CustomFields.GetCustomFields(Convert.ToInt32(Session["SystemId"].ToString()));
                    this.grdCustomfields.DataSource = theDSCust.Tables[0];
                    ViewState["grdDataSource"] = theDSCust.Tables[0];
                    grdCustomfields.DataBind();

                    if (icount == -1)
                    {
                        IQCareMsgBox.Show("CustomFieldExists", this);

                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("CustomFieldUpdate", this);
                    }
                }

                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    CustomFields = null;
                    btnAdd.Text = "Add Custom Field";
                    rbtntext.Checked = false;
                    rbtnnumber.Checked = false;
                    ClearAll();
                    cfieldID = 0;
                    EnableButton();
                }
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmFacilityHome.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add Custom Field";
            rbtntext.Checked = false;
            rbtnnumber.Checked = false;
            customTxtLines.Enabled = true;

            ClearAll();
            cfieldID = 0;
            EnableButton();
            Response.Redirect("../frmFacilityHome.aspx");
        }

        protected void Chkformname_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void FillDropDownFeatures()
        {
            ICustomFields CustomFields;
            try
            {
                DataTable theDTModule = (DataTable)Session["AppModule"];
                string theModList = "";
                foreach (DataRow theDR in theDTModule.Rows)
                {
                    if (theModList == "")
                        theModList = theDR["ModuleId"].ToString();
                    else
                        theModList = theModList + "," + theDR["ModuleId"].ToString();
                }

                if (theModList == "1,2")
                {
                    theModList = "0";
                }
                else if (theModList == "1")
                {
                    theModList = "1";
                }
                else
                {
                    theModList = "2";
                }

                //string[] mvalues = theModList.Split(new char[] { ',' });

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetFeatures(Convert.ToInt32(Session["SystemId"]), theModList);
                BindFunctions BindManager = new BindFunctions();
                BindManager.BindCheckedList(Chkformname, theDS.Tables[0], "FeatureName", "FeatureID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                CustomFields = null;
            }
        }

        protected void grdCustomfields_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Change") == 0)
            {
                // get the row index stored in the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument);

                // get the GridViewRow where the command is raised
                GridViewRow theRow = ((GridView)e.CommandSource).Rows[index];

                String strid = (String)this.grdCustomfields.DataKeys[index].Value;
                string[] arrstring = new string[5];
                char[] splitter = { '*' };
                if (strid.ToString() != "")
                {
                    arrstring = strid.Split(splitter);
                }

                int icustomFieldID = Convert.ToInt32(arrstring[0]);
                int formID = Convert.ToInt32(arrstring[1]);
                int cntrlID = Convert.ToInt32(arrstring[2]);
                codeID = Convert.ToInt32(arrstring[3]);
                string lbldesc = Convert.ToString(arrstring[4]);

                txtfdesc.Text = lbldesc.ToString();

                string labelName = Convert.ToString(theRow.Cells[1].Text.ToString());

                txtflabel.Text = labelName.ToString();
                cfieldID = icustomFieldID;
                ViewState["cfieldID"] = icustomFieldID;
                ViewState["mstCodeID"] = codeID;
                ViewState["OldLabel"] = labelName.ToString();
                btnAdd.Text = "Edit Custom Field";
                EnableButton();
                rbtntext.Checked = false;
                rbtnselect.Checked = false;
                rbtnnumber.Checked = false;
                rbtndate.Checked = false;
                rbtnyesno.Checked = false;
                rbtnmulti.Checked = false;

                FormPopulate(formID, cntrlID, icustomFieldID);
            }
            else if (e.CommandName.CompareTo("Activate") == 0)
            {
                // get the row index stored in the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument);

                // get the GridViewRow where the command is raised
                GridViewRow theRow = ((GridView)e.CommandSource).Rows[index];

                String id = (String)this.grdCustomfields.DataKeys[index].Value;
                Int32 CCFieldID = 0;
                if (id.ToString() != "")
                {
                    CCFieldID = Convert.ToInt32(id.Substring(0, id.IndexOf("*")));
                }
                DeleteRow(CCFieldID, 1);
            }
            else if (e.CommandName.CompareTo("Inactive") == 0)
            {
                // get the row index stored in the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument);

                // get the GridViewRow where the command is raised
                GridViewRow theRow = ((GridView)e.CommandSource).Rows[index];

                String id = (String)this.grdCustomfields.DataKeys[index].Value;
                Int32 CCFieldID = 0;
                if (id.ToString() != "")
                {
                    CCFieldID = Convert.ToInt32(id.Substring(0, id.IndexOf("*")));
                }
                btnAdd.Text = "Add Custom Field";
                rbtntext.Checked = false;
                rbtnnumber.Checked = false;
                customTxtLines.Enabled = true;
                ClearAll();
                cfieldID = 0;
                EnableButton();
                DeleteRow(CCFieldID, 0);
            }
        }

        protected void grdCustomfields_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                if (e.Row.Cells[4].Text == "Inactive")
                {
                    // reference the Inactive LinkButton
                    LinkButton lnkAct = (LinkButton)e.Row.Cells[6].Controls[0];
                    lnkAct.Text = "Activate";
                    lnkAct.CommandName = "Activate";
                    lnkAct.ForeColor = System.Drawing.Color.Blue;
                    lnkAct.OnClientClick = "return confirm('Are you sure you want to activate this custom field?')";
                    e.Row.Cells[5].Enabled = false;
                }
                if (e.Row.Cells[4].Text == "Active")
                {
                    // reference the Activate LinkButton
                    LinkButton lnkDeAct = (LinkButton)e.Row.Cells[6].Controls[0];
                    lnkDeAct.Text = "Inactive";
                    lnkDeAct.CommandName = "Inactive";
                    lnkDeAct.ForeColor = System.Drawing.Color.Blue;
                    lnkDeAct.OnClientClick = "return confirm('Are you sure you want to inactivate this custom field?')";
                }
                //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdCustomfields, "Change$" + e.Row.RowIndex.ToString()));
                //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdCustomfields, "Remove$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdCustomfields_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void grdCustomfields_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdCustomfields.PageIndex;
            int thePageSize = grdCustomfields.PageSize;
        }

        protected void grdCustomfields_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Desc";
            }
            else
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Asc";
            }
            grdCustomfields.Columns.Clear();
            grdCustomfields.DataSource = theDV;
            BindGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;

            //(Master.FindControl("lblheader") as Label).Text = "Configure Custom Fields";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Configure Custom Fields";
            ICustomFields CustomFields;

            // CODE TO IMPLEMENT CLIENT CALL BACK METHODS  -- START
            //rbtnselect.Attributes.Add("onClick", "javaScript:return SendCodeName();");
            //rbtnmulti.Attributes.Add("onClick", "javaScript:return SendCodeName();");

            ClientScriptManager m = Page.ClientScript;
            str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
            strCallback = "function CallServer(args,context){" + str + ";}";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);

            // CODE TO IMPLEMENT CLIENT CALL BACK METHODS  -- FINISH

            try
            {
                if (!IsPostBack)
                {
                    FillDropDownFeatures();

                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    DataSet theDS = CustomFields.GetCustomFields(Convert.ToInt32(Session["SystemId"].ToString()));
                    this.grdCustomfields.DataSource = theDS.Tables[0];
                    this.grdCustomfields.DataBind();
                    if (ViewState["grdDataSource"] == null)
                        ViewState["grdDataSource"] = theDS.Tables[0];
                    ViewState["SortDirection"] = "Desc";
                    BindGrid();
                    ViewState["temptable"] = theDS.Tables[0];
                }
                else
                {
                    if ((DataTable)Session["AddCustomList"] != null)
                    {
                        DataTable theDT = (DataTable)Session["AddCustomList"];
                        //Application.Remove("AddCustomList");
                        ViewState["CustomList"] = theDT;
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                CustomFields = null;
            }

            txtmin.Attributes.Add("onkeypress", "return CheckNumericValue(event);");
            txtmax.Attributes.Add("onKeypress", "return CheckNumericValue(event);");
            txtflabel.Attributes.Add("onkeypress", "return CheckAlphaNumeric(event);");
            txtfdesc.Attributes.Add("onkeypress", "return CheckAlpha(event);");
            txtflabel.Attributes.Add("onBlur", "CheckAlphaFirstLetter();");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdCustomfields.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl00");
                    //Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl01");
                }
            }

            base.Render(writer);
        }

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "CustomFieldID";
            theCol0.DataField = "CustomFieldID";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Label Name";
            theCol1.DataField = "Label";
            theCol1.SortExpression = "Label";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Form";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataField = "FeatureName";
            theCol3.SortExpression = "FeatureName";
            theCol3.ReadOnly = true;

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Field Type";
            theCol5.DataField = "Name";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.SortExpression = "Name";
            theCol5.ReadOnly = true;

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Status";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "Status";
            theCol6.SortExpression = "Status";
            theCol6.ReadOnly = true;

            ButtonField theBtn1 = new ButtonField();
            theBtn1.ButtonType = ButtonType.Button;
            theBtn1.Text = "Change";
            theBtn1.CommandName = "Change";
            theBtn1.HeaderStyle.Width = 70;

            //theBtn1.HeaderStyle.CssClass = "textstylehidden";
            //theBtn1.ItemStyle.CssClass = "textstylehidden";

            ButtonField theBtn2 = new ButtonField();
            theBtn2.ButtonType = ButtonType.Link;
            theBtn2.Text = "Activate";
            theBtn2.CommandName = "Activate";
            theBtn2.HeaderStyle.Width = 70;

            grdCustomfields.Columns.Add(theCol0);
            grdCustomfields.Columns.Add(theCol1);
            //grdCustomfields.Columns.Add(theCol2);
            grdCustomfields.Columns.Add(theCol3);
            //grdCustomfields.Columns.Add(theCol4);
            grdCustomfields.Columns.Add(theCol5);
            grdCustomfields.Columns.Add(theCol6);

            grdCustomfields.Columns.Add(theBtn1);
            grdCustomfields.Columns.Add(theBtn2);
            //grdCustomfields.Columns.Add(theBtn3);

            grdCustomfields.DataBind();
            grdCustomfields.Columns[0].Visible = false;
            //grdCustomfields.Columns[2].Visible = false;
            //grdCustomfields.Columns[4].Visible = false;
            //grdCustomfields.Columns[6].Visible = false;
        }

        private void ClearAll()
        {
            for (int i = 0; i < Chkformname.Items.Count; i++)
            {
                Chkformname.Items[i].Selected = false;
                Chkformname.Items[i].Enabled = true;
            }
            txtflabel.Text = "";
            txtfdesc.Text = "";
            try
            {
                Session["AddCustomList"] = null;
                ViewState["CustomList"] = null;
            }
            catch
            {
            }
        }

        private Boolean ControlChecked()
        {
            ControlID = 0;
            if (rbtntext.Checked == true)
            {
                ControlID = Convert.ToInt32(customTxtLines.SelectedValue);
                return true;
            }
            if (rbtnselect.Checked == true)
            {
                ControlID = Convert.ToInt32(Request.Form["hdnselect"]);

                return true;
            }
            if (rbtndate.Checked == true)
            {
                ControlID = Convert.ToInt32(Request.Form["hdnDate"]);
                return true;
            }
            if (rbtnnumber.Checked == true)
            {
                ControlID = Convert.ToInt32(Request.Form["hdnNumber"]);
                return true;
            }
            if (rbtnyesno.Checked == true)
            {
                ControlID = Convert.ToInt32(Request.Form["hdnyesno"]);
                return true;
            }
            if (rbtnmulti.Checked == true)
            {
                ControlID = Convert.ToInt32(Request.Form["hdnmultiline"]);
                return true;
            }
            return false;
        }

        private void CreateFeatureTable()
        {
            htFeatures = new Hashtable();
            htFeatures.Clear();

            int i = 0;
            int j = 1;
            for (i = 0; i < Chkformname.Items.Count; i++)
            {
                if (Convert.ToInt32(Chkformname.Items[i].Selected) == 1)
                {
                    htFeatures.Add(j, Chkformname.Items[i].Value);
                    j = j + 1;
                }
            }
        }

        private void DeleteRow(int FieldID, int Dflag)
        {
            ICustomFields CustomFields;
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                icount = CustomFields.DeleteCustomFields(Convert.ToInt32(FieldID), Convert.ToInt32(Dflag));
                DataSet theDS = CustomFields.GetCustomFields(Convert.ToInt32(Session["SystemId"].ToString()));
                this.grdCustomfields.DataSource = theDS.Tables[0];
                grdCustomfields.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                CustomFields = null;
            }
        }

        private void DisabledButton()
        {
            rbtntext.Disabled = true;
            rbtnselect.Disabled = true;
            rbtndate.Disabled = true;
            rbtnnumber.Disabled = true;
            rbtnyesno.Disabled = true;
            rbtnmulti.Disabled = true;
            //Chkformname.Enabled = false;
        }

        private void EnableButton()
        {
            rbtntext.Disabled = false;
            rbtnselect.Disabled = false;
            rbtndate.Disabled = false;
            rbtnnumber.Disabled = false;
            rbtnyesno.Disabled = false;
            rbtnmulti.Disabled = false;
            //Chkformname.Enabled  = true ;
        }

        private Boolean FieldValidation()
        {
            if (txtflabel.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Field Label";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtflabel.Focus();
                if (btnAdd.Text.ToString() != "Edit Custom Field")
                {
                    rbtntext.Checked = false;
                    rbtnnumber.Checked = false;
                }
                return false;
            }

            if (txtflabel.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Field Label";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtflabel.Focus();
                if (btnAdd.Text.ToString() != "Edit Custom Field")
                {
                    rbtntext.Checked = false;
                    rbtnnumber.Checked = false;
                }
                return false;
            }
            if (htFeatures.Count < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Forms";
                IQCareMsgBox.Show("BlankList", theBuilder, this);
                Chkformname.Focus();
                rbtntext.Checked = false;
                rbtnnumber.Checked = false;
                return false;
            }
            if (ControlChecked() == false)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Select Options ";
                IQCareMsgBox.Show("UncheckedButton", theMsg, this);
                rbtntext.Checked = false;
                rbtnnumber.Checked = false;
                return false;
            }
            if ((rbtnselect.Checked == true) || (rbtnmulti.Checked == true))
            {
                if (btnAdd.Text.ToString() == "Add Custom Field")
                {
                    DataTable dtCheck = (DataTable)ViewState["CustomList"];
                    if (dtCheck == null)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "List";
                        IQCareMsgBox.Show("BlankListValues", theBuilder, this);
                        return false;
                    }
                }
            }
            //If entered txtflabel includes special characters with it then remove them first
            //txtflabel.Text. =
            return true;
        }

        private void FormPopulate(int fID, int CID, int CustFieldID)
        {
            for (int i = 0; i < Chkformname.Items.Count; i++)
            {
                Chkformname.Items[i].Selected = false;
            }
            for (int i = 0; i < Chkformname.Items.Count; i++)
            {
                if (Convert.ToInt32(Chkformname.Items[i].Value) == fID)
                {
                    Chkformname.Items[i].Selected = true;
                    Chkformname.Items[i].Enabled = false;
                }
                else
                {
                    Chkformname.Items[i].Enabled = true;
                }
            }

            switch (CID)
            {
                case 8:

                    thescript = "";
                    thescript += "<script language = 'javascript' defer ='defer' id = 'CT'>\n";
                    thescript += "show('textbox');\n";
                    thescript += "</script>\n";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CT", thescript);
                    rbtntext.Checked = true;
                    customTxtLines.SelectedValue = Convert.ToString("8");
                    customTxtLines.Enabled = false;
                    DisabledButton();

                    break;

                case 1:
                    thescript = "";
                    thescript += "<script language = 'javascript' defer ='defer' id = 'CT'>\n";
                    thescript += "show('textbox');\n";
                    thescript += "</script>\n";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CT", thescript);
                    rbtntext.Checked = true;
                    customTxtLines.SelectedValue = Convert.ToString("1");
                    customTxtLines.Enabled = false;
                    DisabledButton();

                    break;

                case 9:
                    thescript = "";
                    thescript += "<script language = 'javascript' defer ='defer' id = 'CT'>\n";
                    thescript += "showPopup();\n";
                    thescript += "</script>\n";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CT", thescript);
                    rbtnmulti.Checked = true;
                    DisabledButton();

                    break;

                case 4:
                    thescript = "";
                    thescript += "<script language = 'javascript' defer ='defer' id = 'CT'>\n";
                    thescript += "showPopup();\n";
                    //thescript += "document.getElementById('" + rbtnselect.ClientID + "').click();\n";
                    thescript += "</script>\n";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CT", thescript);
                    rbtnselect.Checked = true;
                    DisabledButton();

                    break;

                case 5:
                    rbtndate.Checked = true;
                    DisabledButton();

                    break;

                case 3:
                    thescript = "";
                    thescript += "<script language = 'javascript' defer ='defer' id = 'CT'>\n";
                    thescript += "show('numeric');\n";

                    thescript += "</script>\n";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CT", thescript);
                    rbtnnumber.Checked = true;
                    ICustomFields CustomFields;
                    try
                    {
                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                        DataSet theDS = CustomFields.GetCustomFieldsUnits(CustFieldID);
                        if (theDS != null && theDS.Tables[0].Rows.Count > 0)
                        {
                            txtmin.Text = theDS.Tables[0].Rows[0]["MinControl"].ToString();
                            txtmax.Text = theDS.Tables[0].Rows[0]["MaxControl"].ToString();
                            txtunits.Text = theDS.Tables[0].Rows[0]["Unit"].ToString();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        CustomFields = null;
                    }
                    DisabledButton();

                    break;

                case 6:
                    thescript = "";
                    thescript += "<script language = 'javascript' defer ='defer' id = 'CT'>\n";
                    thescript += "document.getElementById('" + rbtnyesno.ClientID + "').click();\n";
                    thescript += "</script>\n";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CT", thescript);
                    rbtnyesno.Checked = true;
                    DisabledButton();

                    break;
            }
        }

        private void SaveData()
        {
            DataView theDSView = new DataView();
            theDSView.Table = (DataTable)ViewState["temptable"];
            string strValue = Chkformname.SelectedItem.Text;
            theDSView.RowFilter = "FeatureName=" + "'" + strValue + "'";
            theDTCount = theDSView.ToTable();
            int rowcount = theDTCount.Rows.Count;

            ICustomFields CustomFields;
            Int32 thesize = 0;
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");

                for (int i = 0; i < Chkformname.Items.Count; i++)
                {
                    decodeValues = "";
                    deleteValues = "";
                    if (Convert.ToInt32(Chkformname.Items[i].Selected) == 1)
                    {
                        //for Numeric Field
                        if (ControlID.ToString() == "3")
                        {
                            int minvalue;
                            int maxvalue;
                            string unitsvalue;
                            if (txtmin.Text.Trim() == "")
                            {
                                minvalue = 0;
                            }
                            else
                            {
                                minvalue = Convert.ToInt32(txtmin.Text.Trim());
                            }

                            if (txtmax.Text.Trim() == "")
                            {
                                maxvalue = 0;
                            }
                            else
                            {
                                maxvalue = Convert.ToInt32(txtmax.Text.Trim());
                            }

                            if (txtunits.Text.Trim() == "")
                            {
                                unitsvalue = string.Empty;
                            }
                            else
                            {
                                unitsvalue = txtunits.Text.Trim();
                            }
                            thesize = 4;
                            icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 1, Convert.ToInt32(minvalue), Convert.ToInt32(maxvalue), unitsvalue.ToString(), 0, "int", string.Empty, 0, Convert.ToInt32(thesize), "", "", Convert.ToInt32(Session["SystemId"]), rowcount);
                        }
                        //for Select List or Multi Select List
                        else if ((ControlID.ToString() == "4") || (ControlID.ToString() == "9"))
                        {
                            DataTable theDT = (DataTable)ViewState["CustomList"];
                            //if (Convert.ToInt32(codeID) == 0)
                            //{
                            if (theDT != null)
                            {
                                for (int j = 0; j < theDT.Rows.Count; j++)
                                {
                                    //dsList = CustomFields.SaveCodeDecode(txtflabel.Text.Trim().Replace(" ","_"), theDT.Rows[j]["Name"].ToString(),j+1, Convert.ToInt32(Session["AppUserId"]));
                                    decodeValues = decodeValues + theDT.Rows[j]["Name"].ToString() + "/n";
                                }

                                if (ControlID.ToString() == "4")
                                {
                                    thesize = 4;
                                    icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 0, 0, 0, string.Empty, Convert.ToInt32(codeID), "int", string.Empty, 0, Convert.ToInt32(thesize), decodeValues, "", Convert.ToInt32(Session["SystemId"]), rowcount);
                                }
                                else if (ControlID.ToString() == "9")
                                {
                                    thesize = 4;
                                    icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 0, 0, 0, string.Empty, Convert.ToInt32(codeID), "int", string.Empty, 1, Convert.ToInt32(thesize), decodeValues, "", Convert.ToInt32(Session["SystemId"]), rowcount);
                                }
                            }

                            //if (dsList != null && dsList.Tables[0].Rows.Count > 0)
                            //{
                            //    codeID = Convert.ToInt32(dsList.Tables[0].Rows[0]["CodeID"]);

                            //}
                            //}
                        }
                        else
                        {
                            string dataType = string.Empty;
                            if (ControlID.ToString() == "8")
                            {
                                dataType = "varchar(250)";
                                thesize = 250;
                            }
                            else if (ControlID.ToString() == "1")
                            {
                                dataType = "varchar(80)";
                                thesize = 80;
                            }
                            else if (ControlID.ToString() == "5")
                            {
                                dataType = "DateTime";
                                thesize = 8;
                            }
                            else if (ControlID.ToString() == "6")
                            {
                                dataType = "bit";
                                thesize = 1;
                            }

                            icount = CustomFields.SaveCustomFields(txtflabel.Text.Trim().Replace(" ", "_"), txtfdesc.Text.Trim(), Convert.ToInt32(Chkformname.Items[i].Value), 0, ControlID, Convert.ToInt32(Session["AppUserId"]), 0, 0, 0, string.Empty, 0, dataType.ToString(), string.Empty, 0, Convert.ToInt32(thesize), decodeValues, "", Convert.ToInt32(Session["SystemId"]), rowcount);
                        }
                    }
                }

                DataSet theDS = CustomFields.GetCustomFields(Convert.ToInt32(Session["SystemId"].ToString()));
                this.grdCustomfields.DataSource = theDS.Tables[0];
                ViewState["grdDataSource"] = theDS.Tables[0];
                grdCustomfields.DataBind();

                if (icount == -1)
                {
                    IQCareMsgBox.Show("CustomFieldExists", this);
                    return;
                }
                else if (icount == 0)
                {
                    IQCareMsgBox.Show("NameExists", this);
                    return;
                }
                else
                {
                    IQCareMsgBox.Show("CustomFieldSave", this);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                CustomFields = null;
                rbtntext.Checked = false;
                rbtnnumber.Checked = false;
            }
        }
    }

}