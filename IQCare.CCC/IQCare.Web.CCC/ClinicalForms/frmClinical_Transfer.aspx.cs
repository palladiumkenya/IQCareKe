using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using IQCare.Web.UILogic;
namespace IQCare.Web.Clinical
{


    public partial class Transfer : BasePage
    {
        
        string BtnUpdate = "";
        string TransferId = "";
        DataSet theFacilityDS;


        private bool validate()
        {
            DataSet theDS = new DataSet();
            IPatientTransfer PatientTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
            theDS = PatientTransferMgr.GetDataValidate(PatientId.ToString(), txtTransferDate.Text);
            //Satellite Cannot Be Blank - 0
            if (ddSatellite.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "New Satellite";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            //Transfer Date cannot be Blank - 1
            if (txtTransferDate.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Transfer Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtTransferDate.Focus();
                return false;
            }

            //Transfer Date cannot be less than Latest Visit Dates - 2
            if (Convert.ToDateTime(txtTransferDate.Text) < Convert.ToDateTime(theDS.Tables[3].Rows[0]["VisitDate"].ToString()))
            {
                IQCareMsgBox.Show("TransferDate", this);
                txtTransferDate.Focus();
                return false;
            }

            //Cannot Tranfer a patient more than one satellite on same date - 3
            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DateExist"]) > 0)
            {
                IQCareMsgBox.Show("TransferDate_2", this);
                txtTransferDate.Focus();
                return false;
            }

            //Transfer date should be greater than the previous date - 4
            if (Convert.ToInt32(theDS.Tables[1].Rows[0]["Exist"]) == 1)
            {
                if (Convert.ToDateTime(theDS.Tables[1].Rows[0]["TransferredDate"]) > Convert.ToDateTime(txtTransferDate.Text))
                {
                    IQCareMsgBox.Show("TransferDate_3", this);
                    txtTransferDate.Focus();
                    return false;
                }
            }

            //Transfer Date Cannot Greater than the Current Date - 5
            if (Convert.ToDateTime(txtTransferDate.Text) > Convert.ToDateTime(Application["AppCurrentDate"]))
            {
                IQCareMsgBox.Show("TransferDate_1", this);
                txtTransferDate.Focus();
                return false;
            }
            //Current Satellite and New Satellite cannot be same.
            if (txtLocationName.Text == ddSatellite.SelectedItem.Text)
            {
                IQCareMsgBox.Show("DD_Satellite", this);
                ddSatellite.SelectedValue = "0";
                return false;
            }
            return true;
        }

        private bool validateEdit()
        {
            DataSet theDS = new DataSet();
            IPatientTransfer PatientTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
            theDS = PatientTransferMgr.GetDataValidate(PatientId.ToString(), txtTransferDate.Text);

            //Satellite Cannot Be Blank - 0
            if (ddSatelliteEdit.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "New Satellite";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            //Transfer Date cannot be Blank - 1
            if (TxtTransDateEdit.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Transfer Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtTransferDate.Focus();
                return false;
            }

            //Transfer Date cannot be greater than Enrolment Registration Date - 2
            if (Convert.ToDateTime(TxtTransDateEdit.Text) < Convert.ToDateTime(theDS.Tables[3].Rows[0]["VisitDate"].ToString()))
            {
                IQCareMsgBox.Show("TransferDate", this);
                txtTransferDate.Focus();
                return false;
            }

            //Cannot Tranfer a patient more than one satellite on same date - 3
            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DateExist"]) > 0)
            {
                IQCareMsgBox.Show("TransferDate_2", this);
                TxtTransDateEdit.Focus();
                return false;
            }

            //Transfer date should be greater than the previous date - 4


            //Transfer Date Cannot Greater than the Current Date - 5
            if (Convert.ToDateTime(TxtTransDateEdit.Text) > Convert.ToDateTime(Application["AppCurrentDate"].ToString()))
            {
                IQCareMsgBox.Show("TransferDate_1", this);
                txtTransferDate.Focus();
                return false;
            }
            //Validation between two dates
            DataSet theDS1 = new DataSet();
            theDS1 = PatientTransferMgr.GetDateValidateBetween(PatientId.ToString(), ViewState["TransferDate"].ToString());
            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["ID"]) > Convert.ToInt32(TransferId))
            {
                IQCareMsgBox.Show("TransferDate_3", this);
                TxtTransDateEdit.Focus();
                return false;
            }

            //Current Satellite and New Satellite cannot be same.
            if (txtFromSatellite.Text == ddSatelliteEdit.SelectedItem.Text)
            {
                IQCareMsgBox.Show("DD_Satellite", this);
                ddSatelliteEdit.SelectedValue = "0";
                return false;
            }

            return true;
        }

    
        private void BindTransferDetail()
        {

            txtLocationName.Text = Session["AppLocation"].ToString();
            txtLocationName.ReadOnly = true;

            /*Binding Satellite ID*/
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataTable theDT = new DataTable();

            DataSet theDS = new DataSet();
            IPatientTransfer PatientTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");

            if (btnSave.Text == "Save")
            {
                tredit.Visible = false;
                theDS = PatientTransferMgr.GetSatelliteLocation(PatientId.ToString(), TransferId, 0, Session["SystemId"].ToString());
                txtLocationName.Text = theDS.Tables[0].Rows[0]["CurrentSatName"].ToString();
                DataView theDV = new DataView(theDS.Tables[1]);
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddSatellite, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

            }
         

            if (btnSave.Text == "Update")
            {
                tredit.Visible = true;
                theDS = PatientTransferMgr.GetSatelliteLocation(PatientId.ToString(), TransferId, 1, Session["SystemId"].ToString());
           
                txtLocationNameEdit.Text = theDS.Tables[0].Rows[0]["CurrentSatName"].ToString();
                txtLocationNameEdit.Enabled = false;
                txtFromSatellite.Text = theDS.Tables[2].Rows[0]["TransferfromSatellite"].ToString();
                txtFromSatellite.Enabled = false;
                ViewState["FromID"] = theDS.Tables[2].Rows[0]["TransferredfromID"].ToString();
                //ddSatelliteEdit.Enabled = false;
                TxtTransDateEdit.Text = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[2].Rows[0]["TransferredDate"]));
                ViewState["TransferDate"] = TxtTransDateEdit.Text;
                DataView theDV = new DataView(theDS.Tables[1]);
                //theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddSatelliteEdit, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                ddSatelliteEdit.SelectedValue = theDS.Tables[2].Rows[0][3].ToString();
            }


        }
        private void BindGrid()
        {
            if (ViewState["Sorted"] != null)
            {
                IPatientTransfer PatientTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
                DataSet theDS = PatientTransferMgr.GetSatelliteLocation(PatientId.ToString(), TransferId, 0, Session["SystemId"].ToString());
                txtLocationName.Text = theDS.Tables[0].Rows[0]["CurrentSatName"].ToString();
                BindFunctions BindManager = new BindFunctions();
                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = new DataView(theDS.Tables[1]);
                DataTable theDT = new DataTable();
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddSatellite, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                GrdTransfer.DataSource = theDS.Tables[2];
                if (ViewState["grdDataSource"] == null)
                {
                    ViewState["grdDataSource"] = theDS.Tables[2];
                }
            }

            ViewState["Sorted"] = "";
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "ID";
            theCol0.DataField = "ID";
            theCol0.HeaderStyle.CssClass = "textstylehidden";
            theCol0.ItemStyle.CssClass = "textstylehidden";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "From Location";
            theCol1.DataField = "TransferfromSatellite";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.SortExpression = "TransferfromSatellite";
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "To Location";
            theCol2.DataField = "TransfertoSatellite";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.SortExpression = "TransfertoSatellite";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Transferred Date";
            theCol3.DataField = "TransferredDate";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "TransferredDate";
            theCol3.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            GrdTransfer.Columns.Add(theCol0);
            GrdTransfer.Columns.Add(theCol1);
            GrdTransfer.Columns.Add(theCol2);
            GrdTransfer.Columns.Add(theCol3);
            GrdTransfer.Columns.Add(theBtn);
            GrdTransfer.DataBind();

        }
        private void SaveUpdateMsg()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans=true;\n";
            script += "alert('Patient Transferred Successfully');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='.././frmFindAddPatient.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Location Transfer";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Patient Transfer";


            txtTransferDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtTransferDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            TxtTransDateEdit.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            TxtTransDateEdit.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

         

            BtnUpdate = Request.QueryString["name"].ToString();
            //(Master.FindControl("lblpntStatus") as Label).Text = Session["AppStatus"].ToString();

            if (CurrentSession.Current.CurrentPatient.Status == 1)
            {
                btnSave.Enabled = false;
            }
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.Transfer, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }
            if (BtnUpdate == "Edit")
            {
                tredit.Visible = true;
                tradd.Visible = false;
                btnSave.Text = "Update";
                TransferId = Request.QueryString["TransferId"].ToString();
                ViewState["RegDate"] = Request.QueryString["RegDate"].ToString();
            }
            if (!IsPostBack)
            {

                IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), 56, 0);
                ViewState["FacilityDS"] = theFacilityDS;
                SetPageLabels();

                ViewState["SortDirection"] = "Desc";
                ViewState["Sorted"] = "";
                ViewState["Save"] = null;
                BindTransferDetail();
                BindGrid();
                //SaveUpdateMsg();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Createdate = "";
            string ID = "";

            if (btnSave.Text == "Save")
            {
                if (validate() == false)
                {
                    return;
                }
                IPatientTransfer PatientTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
                DataTable theDT = PatientTransferMgr.GetSatelliteID(PatientId.ToString());
                string FromLocationID = theDT.Rows[0]["LocationID"].ToString();
                int retVal = PatientTransferMgr.SaveUpdate(ID, PatientId.ToString(), FromLocationID, ddSatellite.SelectedValue, txtTransferDate.Text, Convert.ToInt32(Session["AppUserId"]), Createdate, 0);
                ViewState["Save"] = "";
                SaveUpdateMsg();
            }


            else if (btnSave.Text == "Update")
            {
                if (validateEdit() == false)
                {
                    return;
                }
                IPatientTransfer PatientTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
                int Transfer = (int)PatientTransferMgr.SaveUpdate(TransferId, PatientId.ToString(), ViewState["FromID"].ToString(), ddSatelliteEdit.SelectedValue, TxtTransDateEdit.Text, Convert.ToInt32(Session["AppUserId"]), Createdate, 1);
                SaveUpdateMsg();
            }
            GrdTransfer.Columns.Clear();
            BindGrid();
        }
        protected void GrdTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdTransfer, "Select$" + e.Row.RowIndex.ToString()));
            }
        }
        protected void GrdTransfer_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdTransfer.PageIndex;
            int thePageSize = GrdTransfer.PageSize;

            GridViewRow theRow = GrdTransfer.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            string TransferId = theRow.Cells[0].Text.ToString();
            string RegDate = theRow.Cells[3].Text.ToString();
            ViewState["Update"] = "Update";
            string theUrl = string.Format("{0}PatientId={1}&TransferId={2}&RegDate={3}", "frmClinical_Transfer.aspx?name=" + "Edit" + "&", PatientId, TransferId, RegDate);
            Response.Redirect(theUrl);
        }
        protected void theBtn_Click(object sender, EventArgs e)
        {
            string theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", PatientId);
            Response.Redirect(theUrl);
        }
     
        protected void GrdTransfer_Sorting(object sender, GridViewSortEventArgs e)
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
            GrdTransfer.Columns.Clear();
            GrdTransfer.DataSource = theDV;
            if (ViewState["Save"] != null)
            {
                ViewState["Sorted"] = "";
            }
            else { ViewState["Sorted"] = null; }

            BindGrid();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", PatientId);
            Response.Redirect(theUrl);
        }
        private void SetPageLabels()
        {
            DataTable theDT = ((DataSet)ViewState["FacilityDS"]).Tables[0];
            //lblFileRef.InnerHtml = theDT.Rows[0]["Label"].ToString();
        }

    }
}