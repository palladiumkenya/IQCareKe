using System;
using System.Data;

namespace IQCare.Web.Admin
{
    public partial class LaboratorySelectList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["LaboratorySelectList"] != null)
                {
                    lstselectList.DataSource = (DataTable)Session["LaboratorySelectList"];
                    lstselectList.DataValueField = "selectlist";
                    lstselectList.DataTextField = "selectlist";
                    lstselectList.DataBind();
                    // Session.Remove("LaboratorySelectList");
                }
            }
            btnAdd.Attributes.Add("onClick", "return txtAdd('txtselect')");
            btnSubmit.Attributes.Add("onClick", "return listBox_hasItem('lstselectList')");
            btnRemove.Attributes.Add("onClick", "return listBox_selected('lstselectList')");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtselect.Text.Trim()))
            {
                lstselectList.Items.Add(txtselect.Text.Trim());
                txtselect.Text = "";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable theDT = CreateSelectedTable();
            DataRow theDR;
            for (int i = 0; i < lstselectList.Items.Count; i++)
            {
                theDR = theDT.NewRow();
                theDR["selectlist"] = lstselectList.Items[i].Text.ToString().Trim();
                theDT.Rows.Add(theDR);
            }
            Session["LaboratorySelectList"] = theDT;
            ClientScript.RegisterStartupScript(this.GetType(), "btnSubmit_Click", "<script>closeMe();</script>");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lstselectList.SelectedValue))
            {
                lstselectList.Items.Remove(lstselectList.SelectedValue);
            }
        }

        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("selectlist", System.Type.GetType("System.String"));
            return theDT;
        }
    }
}