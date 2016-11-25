using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /// <summary>
    ///
    /// </summary>
    public partial class RearrangeList : System.Web.UI.Page
    {
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable theDT = new DataTable();

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        /// <summary>
        /// Handles the Click event of the btnDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.lstRearrangeListItems.SelectedIndex;
            object o = this.lstRearrangeListItems.SelectedItem;
            theDT = (DataTable)ViewState["tempTable"];
            if (i < this.lstRearrangeListItems.Items.Count - 1 && i >= 0)
            {
                //column value swapped Down
                theDT.Rows[i]["Label"] = theDT.Rows[i + 1]["Label"];
                theDT.Rows[i + 1]["Label"] = lstRearrangeListItems.Items[i];
                theDT.Rows[i]["CustomFieldID"] = theDT.Rows[i + 1]["CustomFieldID"];
                theDT.Rows[i + 1]["CustomFieldID"] = lstRearrangeListItems.SelectedValue;
                ViewState["tempTable"] = theDT;
                lstRearrangeListItems.Items.Clear();
                BindFunctions BindManager = new BindFunctions();
                BindManager.BindList(lstRearrangeListItems, theDT, "Label", "CustomFieldId");
                lstRearrangeListItems.SelectedIndex = i + 1;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            ICustomFields CustomFields;
            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            int CF = CustomFields.RearrangeCustomFields((DataTable)ViewState["tempTable"], Convert.ToInt32(Session["SystemId"].ToString()));
            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.close();\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        /// <summary>
        /// Handles the Click event of the btnUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnUp_Click(object sender, EventArgs e)
        {
            int i = this.lstRearrangeListItems.SelectedIndex;
            object o = this.lstRearrangeListItems.SelectedItem;
            theDT = (DataTable)ViewState["tempTable"];
            if (i > 0)
            {
                //column value swapped up
                theDT.Rows[i]["Label"] = theDT.Rows[i - 1]["Label"];
                theDT.Rows[i - 1]["Label"] = lstRearrangeListItems.Items[i];
                theDT.Rows[i]["CustomFieldID"] = theDT.Rows[i - 1]["CustomFieldID"];
                theDT.Rows[i - 1]["CustomFieldID"] = lstRearrangeListItems.SelectedValue; ;
                ViewState["tempTable"] = theDT;
                lstRearrangeListItems.Items.Clear();
                BindFunctions BindManager = new BindFunctions();
                BindManager.BindList(lstRearrangeListItems, theDT, "Label", "CustomFieldId");
                lstRearrangeListItems.SelectedIndex = i - 1;
            }

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlFormName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlFormName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ICustomFields CustomFields;
            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            DataSet theDS = CustomFields.GetRearrangeCustomFields(Convert.ToInt32(Session["SystemId"].ToString()));
            DataView theDSView = new DataView();
            theDSView.Table = theDS.Tables[0];
            string strValue = ddlFormName.SelectedItem.Text.ToString();
            theDSView.RowFilter = "FeatureName=" + "'" + strValue + "'";
            theDT = theDSView.ToTable();
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindList(lstRearrangeListItems, theDT, "Label", "CustomFieldId");
            ViewState["tempTable"] = theDT;
        }

        /// <summary>
        /// Fills the drop down features.
        /// </summary>
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

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetFeatures(Convert.ToInt32(Session["SystemId"]), theModList);
                BindFunctions BindManager = new BindFunctions();
                BindManager.BindCombo(ddlFormName, theDS.Tables[0], "FeatureName", "FeatureID");

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

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropDownFeatures();
            }
        }
    }
}