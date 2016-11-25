using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ControlListSelector : System.Web.UI.Page
    {


        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Boolean flagadd = false;
            if (lstControlList.Items.Count > 0)
            {
                for (int i = 0; i < lstControlList.Items.Count; i++)
                {
                    if (lstControlList.Items[i].Text.Trim() == txtList.Text.Trim().ToString())
                    {
                        flagadd = true;
                    }
                }
            }
            if (flagadd == false)
            {
                if (txtList.Text.Trim() != "")
                {
                    lstControlList.Items.Add(txtList.Text);
                }
            }
            txtList.Text = "";
            txtList.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
                return;
            DataRow theDR;
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));

            foreach (ListItem item in lstControlList.Items)
            {
                theDR = theDT.NewRow();
                theDR[0] = item.Text.Trim();
                theDT.Rows.Add(theDR);
            }

            Session["AddCustomList"] = theDT;
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["List"] != "")
            {
                Page.Title = Request.QueryString["List"].ToString();
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //ICustomFields CustomFields;

            if (!IsPostBack)
            {
                if (Request.QueryString["Label"] != "")
                    lblField.Text = Request.QueryString["Label"];

                try
                {
                    if ((Session["AddCustomList"] != null))
                    {
                        DataTable theDT = new DataTable();
                        theDT = (DataTable)Session["AddCustomList"];
                        if (theDT.Rows.Count > 0)
                        {
                            for (int i = 0; i < theDT.Rows.Count; i++)
                            {
                                lstControlList.Items.Add(theDT.Rows[i][0].ToString());
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            btnSubmit.Attributes.Add("onClick", "return Validate(lstControlList);");
            btnAdd.Attributes.Add("onClick", "CheckDuplicate('lstControlList');");
            txtList.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnAdd.UniqueID + "').click();return false;}} else {return true}; ");
        }

        /// <summary>
        /// Validates the data.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateData()
        {
            if (lstControlList.Items.Count == 0)
            {
                //MsgBuilder theBuilder = new MsgBuilder();
                //theBuilder.DataElements["Control"] = "Field Label";
                //IQCareMsgBox.Show("BlankListBox", theBuilder, this);
                return false;
            }
            return true;
        }
    }
}