using System;
using System.Data;
using System.Web.UI;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class DrugScheduleSelector : System.Web.UI.Page
    {
        #region "Field Validation Function"
        private Boolean FieldValidation()
        {
            MsgBuilder theBuilder = new MsgBuilder();


            if (txtAdd.Text == "")
            {
                if (ViewState["Type"].ToString() == "Schedule")
                {
                    theBuilder.DataElements["Control"] = "Schedule Name";
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
            //theDT.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
            return theDT;
        }
        private void BindList()
        {
            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
            BindFunctions theBind = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();


            if (ViewState["SelectedData"] == null)
            {
                DataTable Select = MakeSelectedTable();
                ViewState["SelectedData"] = Select;
            }
            //string strID = GetGenericID((DataTable)ViewState["SelectedData"]);
            theBind.BindList(lstSelected, (DataTable)ViewState["SelectedData"], "Name", "Id");
            if (Request.QueryString["Type"] == "Schedule")
            {
                lblAdd.Text = "Add Schedule :";
                DataTable theDT = (DataTable)ViewState["DrugData"];
                theBind.BindList(lstAvailable, theDT, "Name", "Id");
                ViewState["MasterTable"] = theDT;
            }
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
                    ViewState["DrugData"] = (DataTable)Session["DrugScheduleData"];
                    if (Session["SelectedScheduleData"] != null)
                        ViewState["SelectedData"] = (DataTable)Session["SelectedScheduleData"];
                    Session.Remove("DrugScheduleData");
                    Session.Remove("SelectedScheduleData");

                    Init_Form();
                }

                if (ViewState["Type"] != null)
                {
                    if (ViewState["Type"].ToString() == "Schedule")
                    {
                        lblHeader.Text = "Drug Schedule Mapping";
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAvailable.SelectedIndex >= 0)
                {
                    BindFunctions BindManager = new BindFunctions();
                    IQCareUtils theUtils = new IQCareUtils();

                    if (Request.QueryString["Type"] == "Schedule")
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

                    if (Request.QueryString["Type"] == "Schedule")
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

                if (ViewState["Type"].ToString() == "Schedule")
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

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable NewTB = new DataTable();
            NewTB = (DataTable)ViewState["MasterTable"];

            Session.Add("SelectedScheduleData", (DataTable)ViewState["SelectedData"]);
            Session.Add("Type", ViewState["Type"]);
            Session.Add("MasterScheduleData", NewTB);

            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.opener.GetControl();\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Done", theScript);

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);

        }
    }
}