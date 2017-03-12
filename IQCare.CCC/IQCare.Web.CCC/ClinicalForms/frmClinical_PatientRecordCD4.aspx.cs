using System;
using System.Collections;
using System.Data;
using Application.Common;
using Application.Presentation;
namespace IQCare.Web.Clinical
{

    public partial class PatientRecordCD4 : BasePage
    {
        Hashtable htCD4TLC = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean blnCD4 = false; // blnCD4hasValue,blnCD4%hasValue,blnTLChasValue,blnTLC%hasValue
           
            Boolean blnTLC = false;
            Boolean blnTLCPer = false;

            if (!IsPostBack == true)
            {
                htCD4TLC = (Hashtable)Session["htCD4TLC"];

                if (htCD4TLC["CD4"] != null)
                {
                    if (htCD4TLC["CD4"].ToString() != "")
                    {
                        txtCD4.Text = htCD4TLC["CD4"].ToString();
                        blnCD4 = true;
                    }
                }

                if (htCD4TLC["CD4Percent"] != null)
                {
                    if (htCD4TLC["CD4Percent"].ToString() != "")
                    {
                        txtCD4Percent.Text = htCD4TLC["CD4Percent"].ToString();
                      
                    }
                }

                if (htCD4TLC["TLC"] != null)
                {
                    if (htCD4TLC["TLC"].ToString() != "")
                    {
                        txtTLC.Text = htCD4TLC["TLC"].ToString();
                        blnTLC = true;
                    }
                }

                if (htCD4TLC["TLCPercent"] != null)
                {
                    if (htCD4TLC["TLCPercent"].ToString() != "")
                    {
                        txtTLCPercent.Text = htCD4TLC["TLCPercent"].ToString();
                        blnTLCPer = true;
                    }
                }

                if (htCD4TLC["OrderedBy"] != null)
                {
                    if (htCD4TLC["OrderedBy"].ToString() != "")
                        ddlOrderedbyName.SelectedValue = htCD4TLC["OrderedBy"].ToString();
                }

                if (htCD4TLC["OrderedDate"] != null)
                {
                    if (htCD4TLC["OrderedDate"].ToString() != "")
                        txtOrderedDate.Value = Convert.ToDateTime(htCD4TLC["OrderedDate"].ToString()).ToString(Session["AppDateFormat"].ToString());
                }

                if (htCD4TLC["ReportedBy"] != null)
                {
                    if (htCD4TLC["ReportedBy"].ToString() != "")
                        ddlReportedBy.SelectedValue = htCD4TLC["ReportedBy"].ToString();
                }

                if (htCD4TLC["ReportedDate"] != null)
                {
                    if (htCD4TLC["ReportedDate"].ToString() != "")
                        txtReportedDate.Value = Convert.ToDateTime(htCD4TLC["ReportedDate"].ToString()).ToString(Session["AppDateFormat"].ToString());
                }

                BindFunctions theBindMgr = new BindFunctions();
                theBindMgr.BindCombo(ddlOrderedbyName, ((DataTable)Session["Employee"]), "EmployeeName", "EmployeeId");
                theBindMgr.BindCombo(ddlReportedBy, ((DataTable)Session["Employee"]), "EmployeeName", "EmployeeId");

                if (Session["WhyEligible"].ToString() == "CD4 count/%")
                {
                    if (blnCD4 == false && blnCD4 == false)
                    {
                        ddlOrderedbyName.SelectedValue = "0";
                        ddlReportedBy.SelectedValue = "0";
                        txtOrderedDate.Value = "";
                        txtReportedDate.Value = "";
                    }
                }
                else
                {
                    if (blnTLC == false && blnTLCPer == false)
                    {
                        ddlOrderedbyName.SelectedValue = "0";
                        ddlReportedBy.SelectedValue = "0";
                        txtOrderedDate.Value = "";
                        txtReportedDate.Value = "";
                    }
                }

                if (Session["WhyEligible"].ToString() == "CD4 count/%")
                {
                    divTLC.Visible = false;
                    divTLCPercent.Visible = false;

                    if (htCD4TLC["LABID"] != null)
                    {
                        if (htCD4TLC["LABID"].ToString() == "0")
                        {
                            txtCD4.ReadOnly = false;
                            txtCD4Percent.ReadOnly = false;
                            btnSubmit.Enabled = true;
                            ddlOrderedbyName.Enabled = true;
                            ddlReportedBy.Enabled = true;
                            txtOrderedDate.Disabled = false;
                            txtReportedDate.Disabled = false;
                        }
                        else
                        {
                            txtCD4.ReadOnly = true;
                            txtCD4Percent.ReadOnly = true;
                            btnSubmit.Enabled = false;
                            ddlOrderedbyName.Enabled = false;
                            ddlReportedBy.Enabled = false;
                            txtOrderedDate.Disabled = true;
                            txtReportedDate.Disabled = true;
                        }
                    }
                }
                else
                {
                    divCD4.Visible = false;
                    divCD4Percent.Visible = false;
                }

            }
        }


        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                htCD4TLC = (Hashtable)Session["htCD4TLC"];

                htCD4TLC["LABID"] = htCD4TLC["LABID"];
                htCD4TLC["CD4"] = txtCD4.Text;
                htCD4TLC["CD4Percent"] = txtCD4Percent.Text;
                htCD4TLC["TLC"] = txtTLC.Text;
                htCD4TLC["TLCPercent"] = txtTLCPercent.Text;
                htCD4TLC["OrderedBy"] = ddlOrderedbyName.SelectedValue;
                htCD4TLC["OrderedDate"] = txtOrderedDate.Value;
                htCD4TLC["ReportedBy"] = ddlReportedBy.SelectedValue;
                htCD4TLC["ReportedDate"] = txtReportedDate.Value;
                Session["htCD4TLC"] = htCD4TLC;

                string theScript;
                theScript = "<script language='javascript' id='CD4TLCPopup'>\n";
                theScript += "window.opener.fnsubmit();\n";
                theScript += "window.close();\n";
                theScript += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
            }
        }
        private Boolean Validation()
        {
            Boolean theValid = true;
            DataSet theDS = new DataSet();

            IQCareUtils theUtils = new IQCareUtils();

            if (txtCD4.Visible == true)
            {
                if ((txtCD4.Text == "") && (txtCD4Percent.Text == ""))
                {
                    if (txtCD4.Text == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "CD4";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }

                    if (txtCD4Percent.Text == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "CD4 Percent";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }
                }
            }
            else if (txtTLC.Visible == true)
            {
                if ((txtTLC.Text == "") && (txtTLCPercent.Text == ""))
                {
                    if (txtTLC.Text == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "TLC";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }

                    if (txtTLCPercent.Text == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "TLC Percent";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }
                }
            }
            if (ddlOrderedbyName.SelectedValue.ToString() == "0")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Ordered by";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }

            if (txtOrderedDate.Value == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Ordered Date";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }

            if (ddlReportedBy.SelectedValue.ToString() == "0")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Reported by";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }

            if (txtReportedDate.Value == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Reported Date";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }

            if (Convert.ToDateTime(txtOrderedDate.Value) > Convert.ToDateTime(Session["VisitDate"]))
            {
                IQCareMsgBox.Show("OrderVisitDate", this);
                txtOrderedDate.Focus();
                return false;
            }
            if (Convert.ToDateTime(txtReportedDate.Value) > Convert.ToDateTime(Session["VisitDate"]))
            {
                IQCareMsgBox.Show("ReportVisitDate", this);
                txtReportedDate.Focus();
                return false;
            }
            return theValid;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           

            Session["htCD4TLC"] = "";

            string theScript;
            theScript = "<script language='javascript' id='CD4TLCPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }
    }
}