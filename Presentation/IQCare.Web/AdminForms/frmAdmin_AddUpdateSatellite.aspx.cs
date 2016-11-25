using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class AddUpdateSatellite : System.Web.UI.Page
    {
        #region "Redirect Function"

        private void redirect()
        {
            if (ViewState["redirect"].ToString() == "1")
            {
                Response.Redirect("frmAdmin_FacilitySetup.aspx");
            }
            else if (ViewState["redirect"].ToString() == "2")
            {
                Response.Redirect("frmAdmin_Satellites.aspx");
            }
        }

        #endregion "Redirect Function"

        #region "Validate Function"

        private Boolean ValidateSaveField()
        {
            ISatellite SatelliteMgr = (ISatellite)ObjectFactory.CreateInstance("BusinessProcess.Administration.BSatellite, BusinessProcess.Administration");
            DataTable theDT = SatelliteMgr.GetSatelliteByID_Name(txtSatelliteID.Text, txtSatelliteName.Text);

            if (txtSatelliteID.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Satellite ID";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSatelliteID.Focus();
                return false;
            }
            else if (txtSatelliteName.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Satellite Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSatelliteName.Focus();
                return false;
            }
            else if (txtPriority.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Priority";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtPriority.Focus();
                return false;
            }
            if (theDT.Rows[0]["Exist"].ToString() == "1")
            {
                if ((txtSatelliteID.Text == theDT.Rows[0]["SatelliteID"].ToString()) && (txtSatelliteName.Text == theDT.Rows[0]["Name"].ToString()))
                {
                    IQCareMsgBox.Show("SatelliteIDName", this);
                    txtSatelliteID.Focus();
                    return false;
                }
                else if (txtSatelliteID.Text == theDT.Rows[0]["SatelliteID"].ToString())
                {
                    IQCareMsgBox.Show("SatelliteID", this);
                    txtSatelliteID.Focus();
                    return false;
                }
                else if (txtSatelliteName.Text == theDT.Rows[0]["Name"].ToString())
                {
                    IQCareMsgBox.Show("SatelliteName", this);
                    txtSatelliteName.Focus();
                    return false;
                }
            }
            return true;
        }

        private Boolean ValidateUpdateField()
        {
            ISatellite SatelliteMgr = (ISatellite)ObjectFactory.CreateInstance("BusinessProcess.Administration.BSatellite, BusinessProcess.Administration");
            DataSet theDS = SatelliteMgr.GetSatelliteByID_Edit(ViewState["ID"].ToString());
            if (txtSatelliteID.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Satellite ID";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSatelliteID.Focus();
                return false;
            }
            else if (txtSatelliteName.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Satellite Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSatelliteName.Focus();
                return false;
            }
            else if (txtPriority.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Priority";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtPriority.Focus();
                return false;
            }
            else if ((ddStatus.SelectedValue == "1") && (theDS.Tables[1].Rows[0]["No_of_Patient"].ToString() != "0"))
            {
                IQCareMsgBox.Show("SatelliteStatus", this);
                txtSatelliteID.Focus();
                return false;
            }
            foreach (DataRow DR in theDS.Tables[0].Rows)
            {
                if (DR["SatelliteID"].ToString() == txtSatelliteID.Text)
                {
                    IQCareMsgBox.Show("SatelliteID", this);
                    txtSatelliteID.Focus();
                    return false;
                }
            }

            foreach (DataRow DR in theDS.Tables[0].Rows)
            {
                if (DR["Name"].ToString() == txtSatelliteName.Text)
                {
                    IQCareMsgBox.Show("SatelliteName", this);
                    txtSatelliteName.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion "Validate Function"

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Request.QueryString["name"];
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = " >> Satellite";

            if (!IsPostBack)
            {
                ViewState["redirect"] = Request.QueryString["Redirect"];
                /*For Save or Update Message*/
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Name"] = "Satellite";
                /*****/
                ViewState["ID"] = "";
                if (lblTitle.Text == "Edit")
                {
                    int ID = Convert.ToInt32(Request.QueryString["SatelliteId"]);
                    lblTitle.Text = "Edit Satellite/Location";
                    btnSave.Text = "Update";
                    IQCareMsgBox.ShowConfirm("CustomUpdateRecord", theBuilder, btnSave);
                    ISatellite SatelliteEditMgr = (ISatellite)ObjectFactory.CreateInstance("BusinessProcess.Administration.BSatellite, BusinessProcess.Administration");
                    DataTable DT = SatelliteEditMgr.GetSatellite(ID);
                    ViewState["ID"] = DT.Rows[0]["ID"].ToString();
                    txtSatelliteID.Text = DT.Rows[0]["SatelliteID"].ToString();
                    txtSatelliteName.Text = DT.Rows[0]["Name"].ToString();
                    ddStatus.SelectedValue = DT.Rows[0]["DeleteFlag"].ToString();
                    txtPriority.Text = DT.Rows[0]["SRNo"].ToString();
                }
                else
                {
                    td1.Visible = false;
                    td2.ColSpan = 2;
                    lblTitle.Text = "Add Satellite/Location";
                    IQCareMsgBox.ShowConfirm("CustomSaveRecord", theBuilder, btnSave);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Flag = 1;
                String Createdate = "";
                if (btnSave.Text != "Update")
                {
                    Flag = 0;
                    if (ValidateSaveField() == false)
                    {
                        return;
                    }
                }
                else
                {
                    if (ValidateUpdateField() == false)
                    {
                        return;
                    }
                }
                ISatellite SatelliteMgr = (ISatellite)ObjectFactory.CreateInstance("BusinessProcess.Administration.BSatellite, BusinessProcess.Administration");
                int Satellite = (int)SatelliteMgr.SaveUpdateSatellite(ViewState["ID"].ToString(), txtSatelliteID.Text, txtSatelliteName.Text, ddStatus.SelectedValue, Convert.ToInt32(txtPriority.Text), Flag, Convert.ToInt32(Session["AppUserId"]), Createdate);
                redirect();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                //SatelliteMgr = null;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSatelliteID.Text = "";
            txtSatelliteName.Text = "";
            txtPriority.Text = "";
            ddStatus.SelectedValue = "0";
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            redirect();
        }
    }
}