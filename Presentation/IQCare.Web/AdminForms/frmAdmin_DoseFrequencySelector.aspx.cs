using System;
using System.Data;
using System.Web.UI;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class DoseFrequencySelector : System.Web.UI.Page
    {
        #region "Field Validation Function"
        private Boolean FieldValidation()
        {
            MsgBuilder theBuilder = new MsgBuilder();
            if (Request.QueryString["Type"] == "Generic")
            {
                //DataTable theDT = (DataTable)ViewState["DrugData"];
                if (ViewState["DrugType"].ToString() == "37")
                {

                    if (txtAdd.Text == "")
                    {
                        if (txtAbbv.Text == "")
                        {
                            theBuilder.DataElements["Control"] = "Generic Abbreviation";
                            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                            return false;
                        }
                        else
                        {
                            theBuilder.DataElements["Control"] = "Generics";
                            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                            return false;
                        }
                    }
                    else if (txtAbbv.Text == "")
                    {
                        theBuilder.DataElements["Control"] = "Generic Abbreviation";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        return false;
                    }
                }

            }

            if (txtAdd.Text == "")
            {

                if (ViewState["Type"].ToString() == "Strength")
                {
                    theBuilder.DataElements["Control"] = "Strength Name";
                }
                else if (ViewState["Type"].ToString() == "Frequency")
                {
                    theBuilder.DataElements["Control"] = "Frequency Name";
                }
                else if (ViewState["Type"].ToString() == "Schedule")
                {
                    theBuilder.DataElements["Control"] = "Schedule Name";
                }
                else
                {
                    theBuilder.DataElements["Control"] = "Generic Name";
                }
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtAdd.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region "User Functions"
        private void Init_Form()
        {
            BindList();
        }

        private DataTable MakeSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));
            theDT.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
            return theDT;
        }
        private void BindList()
        {
            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
            BindFunctions theBind = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            txtAbbv.Visible = false;
            lblAbbv.Visible = false;

            if (ViewState["SelectedData"] == null)
            {
                DataTable Select = MakeSelectedTable();
                ViewState["SelectedData"] = Select;
            }
            string strID = GetGenericID((DataTable)ViewState["SelectedData"]);
            theBind.BindList(lstSelected, (DataTable)ViewState["SelectedData"], "Name", "Id");
            if (Request.QueryString["Type"] == "Generic")
            {
                lblAdd.Text = "Add Generic :";
                if (ViewState["DrugData"] != null)
                {
                    DataView theDV = new DataView((DataTable)ViewState["DrugData"]);
                    if (strID != "")
                    {
                        theDV.RowFilter = "DrugTypeId=" + ViewState["DrugType"].ToString() + " and GenericId not in(" + strID + ")";
                    }
                    else
                    {
                        theDV.RowFilter = "DrugTypeId=" + ViewState["DrugType"].ToString();
                    }
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    //---- filtered data
                    DataTable theDT1 = CreateGenericTable(theDT);
                    ViewState["MasterTable"] = theDT1;

                    theBind.BindList(lstAvailable, theDT1, "GenericName", "GenericId");
                    if (Convert.ToInt32(ViewState["DrugType"]) == 37)
                    {
                        txtAbbv.Visible = true;
                        lblAbbv.Visible = true;
                    }
                }
            }
            else if (Request.QueryString["Type"] == "Strength")
            {
                lblAdd.Text = "Add Strength :";
                DataView theDV = new DataView(((DataTable)ViewState["DrugData"]));
                theDV.Sort = "StrengthName asc";
                DataTable theDT = theDV.ToTable();
                theBind.BindList(lstAvailable, theDT, "StrengthName", "StrengthId");
                ViewState["MasterTable"] = theDT;
            }
            else if (Request.QueryString["Type"] == "Frequency")
            {
                lblAdd.Text = "Add Frequency :";
                DataTable theDT = (DataTable)ViewState["DrugData"];
                theBind.BindList(lstAvailable, theDT, "Name", "Id");
                ViewState["MasterTable"] = theDT;
            }
            else if (Request.QueryString["Type"] == "Schedule")
            {
                lblAdd.Text = "Add Schedule :";
                DataTable theDT = (DataTable)ViewState["DrugData"];
                theBind.BindList(lstAvailable, theDT, "Name", "Id");
                ViewState["MasterTable"] = theDT;
            }
        }
        public string GetGenericID(DataTable dt)
        {
            string strID = "";
            foreach (DataRow r in dt.Rows)
            {
                if (strID == "")
                {
                    strID = r[0].ToString();
                }
                else
                {
                    strID = strID + "," + r[0].ToString();
                }
            }
            return strID;
        }
        public DataTable CreateGenericTable(DataTable GenricTable)
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("GenericName", System.Type.GetType("System.String"));
            theDT.Columns.Add("GenericAbbrevation", System.Type.GetType("System.String"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DeleteFlag", System.Type.GetType("System.Int32"));


            foreach (DataRow theGenricDR in GenricTable.Rows)
            {

                DataRow theDR = theDT.NewRow();
                theDR[0] = theGenricDR[0];
                if (theGenricDR[2].ToString() != "" && theGenricDR[1].ToString().IndexOf(theGenricDR[2].ToString()) < 0)
                {
                    theDR[1] = theGenricDR[1].ToString() + "-[" + theGenricDR[2].ToString() + "]";
                }
                else
                {
                    theDR[1] = theGenricDR[1].ToString();
                }
                theDR[2] = theGenricDR[2];
                theDR[3] = theGenricDR[3];
                theDR[4] = theGenricDR[4];
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            IDrugMst DrugManager;

            try
            {
                DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                if (IsPostBack != true)
                {

                    ViewState["Type"] = Request.QueryString["Type"].ToString();
                    ViewState["DrugType"] = Request.QueryString["DrugType"].ToString();
                    ViewState["DrugData"] = (DataTable)Session["DrugData"];
                    if (Session["SelectedData"] != null)
                        ViewState["SelectedData"] = (DataTable)Session["SelectedData"];
                    Session.Remove("DrugData");
                    Session.Remove("SelectedData");
                    if (Request.QueryString["Page"] != null)
                    {
                        if (Request.QueryString["Page"].ToString() == "RegimenGeneric")
                        {

                            showAbbv.Visible = false;
                        }
                        else
                        {
                            showAbbv.Visible = true;
                        }
                        if (Request.QueryString["Page"].ToString() == "TBRegimenGeneric")
                        {

                            showAbbv.Visible = false;
                        }
                        else
                        {
                            showAbbv.Visible = true;
                        }
                    }
                    Init_Form();
                }

                if (ViewState["Type"] != null)
                {
                    if (ViewState["Type"].ToString() == "Strength")
                    {
                        lblHeader.Text = "Drug Strength Mapping";
                    }
                    else if (ViewState["Type"].ToString() == "Frequency")
                    {
                        lblHeader.Text = "Drug Frequency Mapping";
                    }
                    else if (ViewState["Type"].ToString() == "Schedule")
                    {
                        lblHeader.Text = "Drug Schedule Mapping";
                    }
                    else
                    {
                        lblHeader.Text = "Drug Generic Mapping";
                    }
                    lstAvailable.Focus();
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
                DrugManager = null;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable NewTB = new DataTable();
            NewTB = (DataTable)ViewState["MasterTable"];

            Session.Add("SelectedData", (DataTable)ViewState["SelectedData"]);
            Session.Add("Type", ViewState["Type"]);
            Session.Add("MasterData", NewTB);

            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.opener.GetControl();\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Done", theScript);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAvailable.SelectedIndex >= 0)
                {
                    BindFunctions BindManager = new BindFunctions();
                    IQCareUtils theUtils = new IQCareUtils();

                    if (Request.QueryString["Type"] == "Strength")
                    {
                        DataRow theDR;
                        DataTable theDTAvail = (DataTable)ViewState["MasterTable"];

                        DataTable theDTSel = (DataTable)ViewState["SelectedData"];

                        DataView theDV = new DataView(theDTAvail);
                        theDV.RowFilter = "StrengthId =" + lstAvailable.SelectedValue;
                        theDR = theDTSel.NewRow();
                        theDR[0] = Convert.ToInt32(theDV[0][0]);
                        theDR[1] = theDV[0][1].ToString();
                        theDTSel.Rows.Add(theDR);
                        lstSelected.DataSource = theDTSel;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDTSel;

                        DataRow[] theDR1 = theDTAvail.Select("StrengthId='" + lstAvailable.SelectedValue + "'");
                        theDTAvail.Rows.Remove(theDR1[0]);
                        lstAvailable.DataSource = theDTAvail;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDTAvail;
                    }
                    else if (Request.QueryString["Type"] == "Frequency")
                    {
                        DataRow theDR;
                        DataTable theDTAvail = (DataTable)ViewState["MasterTable"];

                        DataTable theDTSel = (DataTable)ViewState["SelectedData"];

                        DataView theDV = new DataView(theDTAvail);
                        theDV.RowFilter = "Id =" + lstAvailable.SelectedValue;
                        theDR = theDTSel.NewRow();
                        theDR[0] = Convert.ToInt32(theDV[0][0]);                    ////(lstAvailable.SelectedValue);
                        theDR[1] = theDV[0][1].ToString();
                        theDTSel.Rows.Add(theDR);
                        lstSelected.DataSource = theDTSel;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDTSel;

                        theDR = null;
                        theDV.Dispose();
                        // theDR[2] = "";
                        /////lstAvailable.SelectedItem.Text;

                        DataRow[] theDR1;
                        theDR1 = theDTAvail.Select("Id='" + lstAvailable.SelectedValue + "'");
                        theDTAvail.Rows.Remove(theDR1[0]);
                        lstAvailable.DataSource = theDTAvail;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDTAvail;
                    }
                    else if (Request.QueryString["Type"] == "Schedule")
                    {
                        DataRow theDR;
                        DataTable theDTAvail = (DataTable)ViewState["MasterTable"];

                        DataTable theDTSel = (DataTable)ViewState["SelectedData"];

                        DataView theDV = new DataView(theDTAvail);
                        theDV.RowFilter = "Id =" + lstAvailable.SelectedValue;
                        theDR = theDTSel.NewRow();
                        theDR[0] = Convert.ToInt32(theDV[0][0]);                    ////(lstAvailable.SelectedValue);
                        theDR[1] = theDV[0][1].ToString();
                        theDTSel.Rows.Add(theDR);
                        lstSelected.DataSource = theDTSel;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDTSel;

                        theDR = null;
                        theDV.Dispose();
                        // theDR[2] = "";
                        /////lstAvailable.SelectedItem.Text;

                        DataRow[] theDR1;
                        theDR1 = theDTAvail.Select("Id='" + lstAvailable.SelectedValue + "'");
                        theDTAvail.Rows.Remove(theDR1[0]);
                        lstAvailable.DataSource = theDTAvail;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDTAvail;
                    }
                    else
                    {
                        DataRow theDR;
                        DataTable theDTAvail = (DataTable)ViewState["MasterTable"];

                        //if (ViewState["DrugStrengthSel"] != null)
                        //{
                        DataTable theDTSel = (DataTable)ViewState["SelectedData"];

                        DataView theDV = new DataView(theDTAvail);
                        theDV.RowFilter = "GenericId =" + lstAvailable.SelectedValue;
                        theDR = theDTSel.NewRow();
                        theDR[0] = Convert.ToInt32(theDV[0][0]);                    ////(lstAvailable.SelectedValue);
                        theDR[1] = theDV[0][1].ToString();
                        theDR[2] = theDV[0]["GenericAbbrevation"];
                        /////lstAvailable.SelectedItem.Text;
                        theDTSel.Rows.Add(theDR);
                        lstSelected.DataSource = theDTSel;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDTSel;

                        DataRow[] theDR1 = theDTAvail.Select("GenericId=" + lstAvailable.SelectedValue);
                        theDTAvail.Rows.Remove(theDR1[0]);
                        lstAvailable.DataSource = theDTAvail;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDTAvail;


                    }
                }
                else
                {
                    IQCareMsgBox.Show("NoItemToAdd", this);
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSelected.SelectedIndex >= 0)
                {

                    if (Request.QueryString["Type"] == "Strength")
                    {
                        DataRow theDR;
                        DataTable theDT = (DataTable)ViewState["MasterTable"];
                        theDR = theDT.NewRow();
                        theDR[0] = Convert.ToInt32(lstSelected.SelectedValue);
                        theDR[1] = lstSelected.SelectedItem.Text;
                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtils = new IQCareUtils();
                        DataView theDV = theUtils.GridSort(theDT, "StrengthName", "asc");
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = theDT;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDT;

                        //DataTable theDT1 = (DataTable)ViewState["SelectedData"];
                        //DataRow[] theDR1 = theDT1.Select("Id=" + lstSelected.SelectedValue);
                        //theDT1.Rows.Remove(theDR1[0]);

                        foreach (DataRow theDR2 in ((DataTable)ViewState["SelectedData"]).Rows)
                        {
                            if (theDR2["Id"].ToString() == lstSelected.SelectedValue.ToString())
                            {
                                ((DataTable)ViewState["SelectedData"]).Rows.Remove(theDR2);
                                break;
                            }
                        }
                        DataTable theDT1 = (DataTable)ViewState["SelectedData"];
                        lstSelected.DataSource = theDT1;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDT1;
                    }
                    else if (Request.QueryString["Type"] == "Frequency")
                    {
                        DataRow theDR;
                        DataTable theDT = (DataTable)ViewState["MasterTable"];
                        theDR = theDT.NewRow();
                        theDR[0] = Convert.ToInt32(lstSelected.SelectedValue);
                        theDR[1] = lstSelected.SelectedItem.Text;
                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtils = new IQCareUtils();
                        DataView theDV = theUtils.GridSort(theDT, "Name", "asc");
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = theDT;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDT;

                        foreach (DataRow theDR2 in ((DataTable)ViewState["SelectedData"]).Rows)
                        {
                            if (theDR2["Id"].ToString() == lstSelected.SelectedValue.ToString())
                            {
                                ((DataTable)ViewState["SelectedData"]).Rows.Remove(theDR2);
                                break;
                            }
                        }
                        DataTable theDT1 = (DataTable)ViewState["SelectedData"];
                        lstSelected.DataSource = theDT1;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDT1;
                    }
                    else if (Request.QueryString["Type"] == "Schedule")
                    {
                        DataRow theDR;
                        DataTable theDT = (DataTable)ViewState["MasterTable"];
                        theDR = theDT.NewRow();
                        theDR[0] = Convert.ToInt32(lstSelected.SelectedValue);
                        theDR[1] = lstSelected.SelectedItem.Text;
                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtils = new IQCareUtils();
                        DataView theDV = theUtils.GridSort(theDT, "Name", "asc");
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = theDT;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDT;

                        foreach (DataRow theDR2 in ((DataTable)ViewState["SelectedData"]).Rows)
                        {
                            if (theDR2["Id"].ToString() == lstSelected.SelectedValue.ToString())
                            {
                                ((DataTable)ViewState["SelectedData"]).Rows.Remove(theDR2);
                                break;
                            }
                        }
                        DataTable theDT1 = (DataTable)ViewState["SelectedData"];
                        lstSelected.DataSource = theDT1;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDT1;
                    }
                    else
                    {
                        DataRow theDR;
                        DataTable theDT = (DataTable)ViewState["MasterTable"];
                        DataTable theDT1 = (DataTable)ViewState["SelectedData"];
                        DataRow[] theDR1 = theDT1.Select("Id=" + lstSelected.SelectedValue);
                        theDR = theDT.NewRow();
                        theDR[0] = Convert.ToInt32(lstSelected.SelectedValue);
                        theDR[1] = lstSelected.SelectedItem.Text;
                        theDR["GenericAbbrevation"] = theDR1[0][2];
                        theDR["DrugTypeID"] = ViewState["DrugType"].ToString();


                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtils = new IQCareUtils();
                        DataView theDV = theUtils.GridSort(theDT, "GenericName", "asc");
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = theDT;
                        lstAvailable.DataBind();
                        ViewState["MasterTable"] = theDT;

                        theDT1.Rows.Remove(theDR1[0]);
                        lstSelected.DataSource = theDT1;
                        lstSelected.DataBind();
                        ViewState["SelectedData"] = theDT1;
                    }
                }
                else
                {
                    IQCareMsgBox.Show("NoItemToRemove", this);
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Data Validation"
                if (FieldValidation() == false)
                {
                    return;
                }
                #endregion

                //DataTable theDT = (DataTable)ViewState["DrugData"]; //rupesh
                DataTable theDT = (DataTable)ViewState["MasterTable"];

                IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                if (ViewState["Type"].ToString() == "Strength")
                {
                    DataTable theNDT = (DataTable)DrugManager.CreateStrength(txtAdd.Text.Trim(), Convert.ToInt32(Session["AppUserId"].ToString()));
                    if (theNDT.Rows[0][0].ToString() == "0")
                    {
                        IQCareMsgBox.Show("StrengthExists", this);
                        return;
                    }
                    else
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR[0] = theNDT.Rows[0][0];
                        theDR[1] = theNDT.Rows[0][1];
                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtil = new IQCareUtils();
                        DataView theDV = theUtil.GridSort(theDT, "StrengthName", "asc");
                        ViewState["DrugStrengthAvail"] = theUtil.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = (DataTable)ViewState["DrugStrengthAvail"];
                        lstAvailable.DataBind();
                        txtAdd.Text = "";
                        ViewState["MasterTable"] = ViewState["DrugStrengthAvail"];
                    }
                }
                else if (ViewState["Type"].ToString() == "Frequency")
                {
                    DataTable theNDT = (DataTable)DrugManager.CreateFrequency(txtAdd.Text.Trim(), Convert.ToInt32(Session["AppUserId"]));
                    if (theNDT.Rows[0][0].ToString() == "0")
                    {
                        IQCareMsgBox.Show("FrequencyExists", this);
                        return;
                    }
                    else
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR[0] = theNDT.Rows[0][0];
                        theDR[1] = theNDT.Rows[0][1];
                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtil = new IQCareUtils();
                        DataView theDV = theUtil.GridSort(theDT, "Name", "asc");
                        ViewState["DrugFrequencyAvail"] = theUtil.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = (DataTable)ViewState["DrugFrequencyAvail"];
                        lstAvailable.DataBind();
                        txtAdd.Text = "";
                        ViewState["MasterTable"] = ViewState["DrugFrequencyAvail"];
                    }
                }
                else if (ViewState["Type"].ToString() == "Schedule")
                {
                    DataTable theNDT = (DataTable)DrugManager.CreateSchedule(txtAdd.Text.Trim(), Convert.ToInt32(Session["AppUserId"]));
                    if (theNDT.Rows[0][0].ToString() == "0")
                    {
                        IQCareMsgBox.Show("ScheduleExists", this);
                        return;
                    }
                    else
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR[0] = theNDT.Rows[0][0];
                        theDR[1] = theNDT.Rows[0][1];
                        theDT.Rows.Add(theDR);
                        IQCareUtils theUtil = new IQCareUtils();
                        DataView theDV = theUtil.GridSort(theDT, "Name", "asc");
                        ViewState["DrugScheduleAvail"] = theUtil.CreateTableFromDataView(theDV);
                        lstAvailable.DataSource = (DataTable)ViewState["DrugScheduleAvail"];
                        lstAvailable.DataBind();
                        txtAdd.Text = "";
                        ViewState["MasterTable"] = ViewState["DrugScheduleAvail"];
                    }
                }
                else if (ViewState["Type"].ToString() == "Generic")
                {
                    //DataTable theDT = (DataTable)ViewState["DrugData"];
                    string theGenericAbbrv;
                    if (txtAbbv.Text == "")
                    {
                        theGenericAbbrv = "";
                    }
                    else
                    {
                        theGenericAbbrv = txtAbbv.Text;
                    }
                    int theDrgType = Convert.ToInt32(ViewState["DrugType"]);

                    DataSet theNDT = (DataSet)DrugManager.CreateGeneric(txtAdd.Text.Trim(), theGenericAbbrv, theDrgType, Convert.ToInt32(Session["AppUserId"]));
                    if (theNDT.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        if (theDrgType == 37)
                        {
                            IQCareMsgBox.Show("GenericExists", this);
                            return;
                        }
                        else
                        {
                            IQCareMsgBox.Show("NonARVGenericExists", this);
                            return;
                        }
                    }

                    else
                    {
                        DataTable DT = new DataTable();
                        DT = theDT.Copy();
                        DataRow theDR = DT.NewRow();
                        theDR[0] = theNDT.Tables[0].Rows[0][0];
                        theDR[1] = theNDT.Tables[0].Rows[0][1];
                        theDR[2] = theNDT.Tables[0].Rows[0][2];
                        theDR[3] = theNDT.Tables[1].Rows[0][0];
                        DT.Rows.Add(theDR);

                        IQCareUtils theUtil = new IQCareUtils();
                        DataView theDV = theUtil.GridSort(DT, "GenericName", "asc");
                        DT = theUtil.CreateTableFromDataView(theDV);

                        ViewState["DrugData"] = DT;
                        theDV = new DataView(DT);
                        theDV.RowFilter = "DrugTypeId=" + ViewState["DrugType"].ToString();
                        DT = theUtil.CreateTableFromDataView(theDV);
                        ViewState["MasterTable"] = DT;

                        lstAvailable.DataSource = DT;
                        lstAvailable.DataBind();
                        if (theDrgType == 37)
                        {
                            txtAbbv.Text = "";
                            txtAdd.Text = "";
                        }
                        else
                        {
                            txtAdd.Text = "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
    }

}