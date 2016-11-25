using System;
using System.Data;
using System.Web.UI;
using Application.Common;
using Application.Presentation;
namespace IQCare.Web.Clinical
{
    public partial class OIDrugSelector : BasePage
    {
        #region "User Defined Function"

        private void BindList()
        {
            DataTable theDT = (DataTable)ViewState["DrugData"];
            DataTable DTSelected = (DataTable)ViewState["SelectedData"];
            BindFunctions theBind = new BindFunctions();
            theBind.BindList(lstDrugList, theDT, "DrugName", "DrugId");
            theBind.BindList(lstSelectedDrug, DTSelected, "DrugName", "DrugId");
            ViewState["DrugTable"] = theDT;
        }

        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            return theDT;
        }

        #endregion "User Defined Function"

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)ViewState["SelectedData"];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstDrugList.SelectedValue);
                theDR[1] = lstDrugList.SelectedItem.Text;
                DataTable theDT1 = (DataTable)ViewState["DrugTable"];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstDrugList.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDT.Rows.Add(theDR);
                lstSelectedDrug.DataSource = theDT;
                lstSelectedDrug.DataBind();
                ViewState["SelectedData"] = theDT;

                theDT1.Rows.Remove(theDR1[0]);
                DataTable theDT2 = (DataTable)ViewState["DrugData"];
                DataRow[] theDR2 = theDT2.Select("DrugId=" + lstDrugList.SelectedValue);
                theDT2.Rows.Remove(theDR2[0]);
                ViewState["DrugData"] = theDT2;

                lstDrugList.DataSource = theDT1;
                lstDrugList.DataBind();
                ViewState["DrugTable"] = theDT1;
                txtSearch.Focus();
                txtSearch_TextChanged(sender, e);
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

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow theDR;
                DataTable theDT = (DataTable)ViewState["DrugTable"];
                theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                theDR[1] = lstSelectedDrug.SelectedItem.Text;
                DataTable theDT1 = (DataTable)ViewState["SelectedData"];
                DataRow[] theDR1 = theDT1.Select("DrugId=" + lstSelectedDrug.SelectedValue);
                theDR[2] = theDR1[0][2];
                theDT.Rows.Add(theDR);

                DataTable theDT2 = (DataTable)ViewState["DrugData"];
                DataRow theDR2 = theDT2.NewRow();
                theDR2[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                theDR2[1] = lstSelectedDrug.SelectedItem.Text;
                theDR2[2] = theDR1[0][2];
                theDT2.Rows.Add(theDR2);
                ViewState["DrugData"] = theDT2;

                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = theUtils.GridSort(theDT, "DrugName", "asc");
                theDT = theUtils.CreateTableFromDataView(theDV);
                lstDrugList.DataSource = theDT;
                lstDrugList.DataBind();
                ViewState["DrugTable"] = theDT;

                theDT1.Rows.Remove(theDR1[0]);
                lstSelectedDrug.DataSource = theDT1;
                lstSelectedDrug.DataBind();
                ViewState["SelectedData"] = theDT1;
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
            Application.Add("OtherDrugs", ViewState["SelectedData"]);
            Session["MasterDrugData"] = ViewState["DrugData"];
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.opener.GetControl();\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Done", theScript);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblHeader.Text = "OI Treatment and Other Medications";
            if (this.IsPostBack != true)
            {
                ViewState["DrugData"] = (DataTable)Session["MasterDrugData"];
                if ((DataTable)Session["SelectedDrug"] != null)
                {
                    ViewState["SelectedData"] = Session["SelectedDrug"];
                }
                else
                {
                    ViewState["SelectedData"] = CreateSelectedTable();
                }

                ViewState["DrugType"] = Request.QueryString["DrugType"].ToString();
                Application.Remove("MasterData");
                Application.Remove("SelectedDrug");
                txtSearch.Attributes.Add("onKeyUp", ClientScript.GetPostBackEventReference(txtSearch, "txtSearch_TextChanged"));
                // txtSearch.Attributes.Add("onKeyUp", this.GetPostBackClientEvent(txtSearch, "txtSearch_TextChanged"));
                BindList();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView theDV = new DataView((DataTable)ViewState["DrugTable"]);
            theDV.RowFilter = "DrugName like '" + txtSearch.Text + "%'";
            IQCareUtils theUtil = new IQCareUtils();
            BindFunctions theBind = new BindFunctions();
            theBind.BindList(lstDrugList, theUtil.CreateTableFromDataView(theDV), "DrugName", "DrugId");
            txtSearch.Focus();
        }
    }
}