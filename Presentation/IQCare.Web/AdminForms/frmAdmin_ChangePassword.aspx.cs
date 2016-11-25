using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /// <summary>
    ///
    /// </summary>
    public partial class ChangePassword : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the ServerClick event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExit_ServerClick(object sender, EventArgs e)
        {
            if (Session["MandatoryChange"] != null)
            {
                if (Session["MandatoryChange"].ToString() == "1")
                {
                    Response.Redirect(ViewState["url"].ToString());
                }
                else
                    Response.Redirect("~/frmFacilityHome.aspx");
            }
            else
                Response.Redirect("../frmFacilityHome.aspx");
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }

            if (GetPassWord_Data() == false)
            {
                return;
            }
            else
            {
                IQCareMsgBox.Show("UpdateMessage", this);
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Password";
            DataSet theXMLDS = new DataSet();
            theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theXMLDS.Tables["Mst_Facility"]);
            theDV.RowFilter = "FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);
            if (theDV[0]["StrongPassFlag"] != DBNull.Value)
            {
                if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                {
                    string[] Username = Session["AppUserName"].ToString().Split(' ');
                    txtNewPassword.Attributes.Add("onblur", "if (validstrngpwd('" + txtNewPassword.ClientID + "','" + Username[0].ToString() + "')){\n alert(document.getElementById('unique_id').innerHTML);\n document.getElementById('" + txtNewPassword.ClientID + "').value='';\n}");
                }
            }
            if (!IsPostBack)
            {
                ViewState["url"] = Application["PrvFrm"].ToString();
                Application.Remove("PrvFrm");
                txtUserName.Text = Session["AppUserName"].ToString();
                txtUserName.Enabled = false;
            }
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation()
        {
            //Visit Date Validations
            if (txtNewPassword.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "New Password";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                return false;
            }
            if (txtConfirmpassword.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Confirm Password";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the pass word_ data.
        /// </summary>
        /// <returns></returns>
        private Boolean GetPassWord_Data()
        {
            IPassword PasswordMgr;
            try
            {
                PasswordMgr = (IPassword)ObjectFactory.CreateInstance("BusinessProcess.Administration.BPassword, BusinessProcess.Administration");
                DataSet DSGetPassData = PasswordMgr.GetUserData(Convert.ToInt32(Session["AppUserId"].ToString()));
                Utility theUtil = new Utility();
                string thePass = theUtil.Decrypt(DSGetPassData.Tables[0].Rows[0]["Password"].ToString());
                if (txtExisPassword.Text.Trim() != thePass)
                {
                    IQCareMsgBox.Show("ExistPasswordNotMatch", this);
                    return false;
                }

                if (Session["MandatoryChange"] != null)
                {
                    if ((Session["MandatoryChange"].ToString() == "1") || (Session["MandatoryChange"].ToString() == "2"))
                    {
                        if (txtNewPassword.Text == thePass)
                        {
                            IQCareMsgBox.Show("PasswordMatchSame", this);
                            return false;
                        }
                    }
                }
                if (txtNewPassword.Text == txtConfirmpassword.Text)
                {
                    PasswordMgr.UpdatePassword(Convert.ToInt32(Session["AppUserId"].ToString()), txtNewPassword.Text);
                }
                else
                {
                    IQCareMsgBox.Show("NewConfirmPasswordNotMatch", this);
                    return false;
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
                PasswordMgr = null;
            }
            return true;
        }
    }
}