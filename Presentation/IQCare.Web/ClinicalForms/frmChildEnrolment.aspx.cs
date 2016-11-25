using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using Interface.Security;
namespace IQCare.Web.Clinical
{

    public partial class ChildEnrolment : BasePage
    {
        AuthenticationManager Authentiaction = new AuthenticationManager();
        string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
        DataTable dtTemp, dtInfo;
        DataSet theDS;
        int icount;

        private bool FieldValidation()
        {
            IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            if (TxtFirstName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "First Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtFirstName.Focus();
                return false;
            }
            if (TxtLastName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Last Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtLastName.Focus();
                return false;
            }

            if (txtHEINo.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "HEI Number";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                return false;
            }

            if (TxtRegistrationDate.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Registration Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtRegistrationDate.Focus();
                return false;
            }
            DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(TxtRegistrationDate.Text));
            if (theEnrolDate > theCurrentDate)
            {
                IQCareMsgBox.Show("EnrolDate", this);
                TxtRegistrationDate.Focus();
                return false;
            }

            if (DDGender.SelectedValue == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Sex";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                DDGender.Focus();
                return false;
            }

            if (TxtDOB.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "DOB";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtDOB.Focus();
                return false;
            }
            DateTime theDOBDate = Convert.ToDateTime(theUtils.MakeDate(TxtDOB.Text));
            if (theDOBDate > theCurrentDate)
            {
                IQCareMsgBox.Show("DOBDate", this);
                TxtDOB.Focus();
                return false;
            }

            if (theDOBDate > theEnrolDate)
            {
                IQCareMsgBox.Show("DOB_EnrolDate", this);
                return false;
            }

            return true;
        }

        protected void AddAttributes()
        {
            TxtRegistrationDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            TxtDOB.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
            txtSysDate.Text = Application["AppCurrentDate"].ToString();


        }

        public void BindGrid(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    grdChildInfo.DataSource = null;
                    grdChildInfo.DataSource = dt;
                    grdChildInfo.DataBind();
                }

            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        public void GetHEIIDNumber()
        {
            if (dtTemp.Rows.Count != 0)
            {
                string lastHEINo = dtTemp.Rows[dtTemp.Rows.Count - 1]["HEIIDNumber"].ToString();
                if (lastHEINo.IndexOf('-') >= 0)
                {
                    string[] addNo = lastHEINo.Split('-');
                    ViewState["AddNo"] = addNo[1].ToString();
                }
                else
                {
                    ViewState["AddNo"] = "";
                }
            }
            else
            {
                ViewState["AddNo"] = 0;
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["lblpntstatus"].ToString();
            Master.ExecutePatientLevel = true;
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                string theUrl = string.Empty;
                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PatientId"].ToString());
                Response.Redirect(theUrl);
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {

                btnsave.Enabled = false;
                btnAdd.Enabled = false;
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {

                btnsave.Enabled = false;
                btnAdd.Enabled = false;

            }
            AddAttributes();
            IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            theDS = ptnMgrPMTCT.GetChildDetail(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppLocationId"].ToString()));
            dtTemp = theDS.Tables[0];
            dtInfo = theDS.Tables[1];
            string strPatientName = Session["PatientName"].ToString();
            string[] strname = strPatientName.Split(',');
            ViewState["FName"] = strname[1].ToString();
            ViewState["LName"] = strname[0].ToString();
            GetHEIIDNumber();


            if (ViewState["DT"] == null)
            {
                ViewState["DT"] = dtTemp;
                ViewState["iSerialNo"] = Convert.ToInt32(dtInfo.Rows[0][0]) + 1;
                ViewState["FirstName"] = "Baby of " + strname[1].ToString();
                ViewState["LastName"] = strname[0].ToString();
                TxtFirstName.Text = ViewState["FirstName"].ToString();
                TxtLastName.Text = ViewState["LastName"].ToString();
                //TxtAdmissionNo.Text = Session["AdmissionNo"].ToString() + "-" + ViewState["iSerialNo"];
                txtHEINo.Text = Session["HEIIDNumber"].ToString() + "-" + ViewState["iSerialNo"];
            }
            else
            {
                ViewState["FirstName"] = TxtFirstName.Text;
                ViewState["LastName"] = TxtLastName.Text;
                ViewState["MidName"] = TxtMidName.Text;
                TxtFirstName.Text = ViewState["FirstName"].ToString();
                TxtLastName.Text = ViewState["LastName"].ToString();
                TxtMidName.Text = ViewState["MidName"].ToString();
                if (btnAdd.Text != "Update Child")
                {
                    try
                    {
                        if (ViewState["AddStatus"].ToString() != "1")
                        {
                            if (Convert.ToString(txtHEINo.Text).IndexOf('-') >= 0)
                            {
                                txtHEINo.Text = Session["HEIIDNumber"].ToString() + "-" + ViewState["iSerialNo"];
                            }
                        }
                    }
                    catch { }
                }

            }
            BindGrid(dtTemp);
            if (dtTemp.Rows.Count == 0)
            {
                btnsave.Enabled = false;
            }
            else
            {
                btnsave.Enabled = true;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {


        }

        protected void grdChildInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

           

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdChildInfo, "Select$" + e.Row.RowIndex.ToString()));

                    }
                }
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true)
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[8].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                    e.Row.Cells[8].ID = e.Row.RowIndex.ToString();

                }
                for (int j = 0; j < 8; j++)
                {
                    if (j == 7)
                    {
                        LinkButton lb = (LinkButton)e.Row.FindControl("LbGridChildEnrol");
                        lb.CommandArgument = e.Row.Cells[j].Text;
                        e.Row.Cells[j].Visible = false;
                    }

                }
            }


        }

        protected void grdChildInfo_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

            dtTemp = (DataTable)ViewState["DT"];
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (dtTemp.Rows[i][7].ToString() == "0")
                {
                    ViewState["Status"] = "Add";
                    Hashtable theHT = AddUpdateData(i);
                    IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                    DataTable theCustomDataDT = new DataTable();
                    DataTable theDS = ptnMgrPMTCT.SavePatientRegistrationPMTCT(theHT, theCustomDataDT);
                    ViewState["visitPk"] = theDS.Rows[0]["Visit_ID"].ToString();
                    ViewState["PtnID"] = theDS.Rows[0]["PatientID"].ToString();
                    DataSet theDSInfantInfo = ptnMgrPMTCT.SaveInfantInfo(
                        Convert.ToInt32(ViewState["PtnID"]),
                        Convert.ToInt32(Session["AppLocationId"]), 
                        Convert.ToInt32(ViewState["visitPk"]), 
                        Convert.ToInt32(Session["PatientId"]), 
                        Convert.ToInt32(Session["AppUserId"]));
                    BindGrid(dtTemp);
                }
                else
                {
                    ViewState["Status"] = "Edit";
                    Hashtable theHT = AddUpdateData(i);
                    IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                    DataTable theCustomDataDT = new DataTable();
                    //DataTable theDS = ptnMgrPMTCT.UpdatePatientRegistrationPMTCT(theHT, theCustomDataDT);
                    DataTable theDS = ptnMgrPMTCT.SavePatientRegistrationPMTCT(theHT, theCustomDataDT);
                    BindGrid(dtTemp);
                }
                SaveCancel();
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Session["PatientId"].ToString());
            Response.Redirect(theUrl);
        }

        public void ClearControl()
        {
            TxtRegistrationDate.Text = "";
            TxtDOB.Text = "";
            DDGender.SelectedIndex = 0;
            txtHEINo.Text = Session["HEIIDNumber"].ToString() + "-" + ViewState["iSerialNo"];
            TxtFirstName.Text = "Baby of " + ViewState["FName"].ToString();
            TxtLastName.Text = ViewState["LName"].ToString();

        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {

                btnsave.Enabled = false;
                btnAdd.Enabled = false;
            }
            else
            {
                btnsave.Enabled = true;
                btnAdd.Enabled = true;
            }
            if (ViewState["DT"] != null)
            {
                dtTemp = (DataTable)ViewState["DT"];
            }
            if (FieldValidation() == false)
            { return; }

            if (btnAdd.Text == "Add Child")
            {

                icount = dtTemp.Rows.Count;
                DataRow row;
                row = dtTemp.NewRow();
                row["FirstName"] = ViewState["FirstName"].ToString();
                row["LastName"] = ViewState["LastName"].ToString();
                string strRegdate = TxtRegistrationDate.Text;
                DateTime dtReg = Convert.ToDateTime(strRegdate);
                strRegdate = dtReg.ToString("dd MMM yyyy");
                row["RegistrationDate"] = strRegdate;
                row["HEIIDNumber"] = txtHEINo.Text;
                string strDob = TxtDOB.Text;
                DateTime dtDob = Convert.ToDateTime(strDob);
                strDob = dtDob.ToString("dd MMM yyyy");
                row["DOB"] = strDob;
                row["Sex"] = DDGender.SelectedItem.Text;
                row["Id"] = "0";
                ViewState["iSerialNo"] = Convert.ToInt32(ViewState["iSerialNo"]) + 1;
                dtTemp.Rows.InsertAt(row, icount);
                ViewState["DT"] = dtTemp;
                ViewState["Count"] = dtTemp.Rows.Count + 1;
                BindGrid(dtTemp);
                ClearControl();
            }
            else if (btnAdd.Text == "Update Child")
            {

                int r = Convert.ToInt32(Session["SelectedRow"]);
                dtTemp.Rows[r]["FirstName"] = TxtFirstName.Text;
                dtTemp.Rows[r]["LastName"] = TxtLastName.Text;
                string strRegdate = TxtRegistrationDate.Text;
                DateTime dtReg = Convert.ToDateTime(strRegdate);
                strRegdate = dtReg.ToString("dd MMM yyyy");
                dtTemp.Rows[r]["RegistrationDate"] = strRegdate;
                dtTemp.Rows[r]["HEIIDNumber"] = txtHEINo.Text;
                string strDob = TxtDOB.Text;
                DateTime dtDob = Convert.ToDateTime(strDob);
                strDob = dtDob.ToString("dd MMM yyyy");
                dtTemp.Rows[r]["DOB"] = strDob;
                dtTemp.Rows[r]["Sex"] = DDGender.SelectedItem.Text;
                ViewState["DT"] = dtTemp;
                BindGrid(dtTemp);
                btnAdd.Text = "Add Child";
                ClearControl();

            }



        }

        protected Hashtable AddUpdateData(int rowid)
        {
            Hashtable theHT = new Hashtable();
            theHT.Clear();
            if (ViewState["Status"].ToString() == "Edit")
            {
                theHT.Add("PatientID", Convert.ToInt32(dtTemp.Rows[rowid][7].ToString()));
            }
            else
            {
                theHT.Add("PatientID", 0);
            }

            theHT.Add("FName", dtTemp.Rows[rowid][0].ToString());
            theHT.Add("MName", "");
            theHT.Add("LName", dtTemp.Rows[rowid][2].ToString());
            theHT.Add("RegDate", dtTemp.Rows[rowid][4].ToString());
            if (dtTemp.Rows[rowid][3].ToString() == "Male")
            {
                theHT.Add("Gender", 16);
            }
            else
            {
                theHT.Add("Gender", 17);
            }
            theHT.Add("DOB", dtTemp.Rows[rowid][5].ToString());
            theHT.Add("Status", 0);
            theHT.Add("MStatus", "");
            theHT.Add("TransferIn", "");
            theHT.Add("RefFrom", "");
            theHT.Add("ANCNumber", "");
            theHT.Add("PMTCTNumber", "");// New field is add to PMTCTS
                           // theHT.Add("Admission", dtTemp.Rows[rowid][6].ToString());
            theHT.Add("Admission", "");
            theHT.Add("HEIIDNumber", dtTemp.Rows[rowid][6].ToString());
            theHT.Add("OutpatientNumber", "");
            if (Session["Address"] == null)
            {
                Session["Address"] = "/";
            }
            if (Session["VillageId"] == null)
            {
                Session["VillageId"] = 0;
            }
            if (Session["DistrictId"] == null)
            {
                Session["DistrictId"] = 0;
            }
            if (Session["EmerPhNo"] == null)
            {
                Session["EmerPhNo"] = "";
            }

            theHT.Add("Address", Session["Address"].ToString());
            theHT.Add("Village", Session["VillageId"].ToString());
            theHT.Add("District", Session["DistrictId"].ToString());
            theHT.Add("Phone", Session["EmerPhNo"].ToString());
            theHT.Add("CountryId", Session["AppCountryId"].ToString());
            theHT.Add("POSId", Session["AppPosID"].ToString());
            theHT.Add("SatelliteId", Session["AppSatelliteId"].ToString());
            theHT.Add("LocationID", Convert.ToInt32(Session["AppLocationId"].ToString()));
            theHT.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));
            theHT.Add("DOBPrecision", "");
            theHT.Add("DataQuality", 0);
            theHT.Add("VisitType", 11);

            return theHT;

        }


        private void SaveCancel()
        {


            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href('frmPatient_Home.aspx?PatientId=" + Session["PatientId"].ToString() + "');\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href('frmChildEnrolment.aspx?name=Edit&patientid=" + Session["PatientId"].ToString() + "');\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        protected void TxtFirstName_TextChanged(object sender, EventArgs e)
        {
            ViewState["FirstName"] = TxtFirstName.Text;
        }

        protected void TxtLastName_TextChanged(object sender, EventArgs e)
        {
            ViewState["LastName"] = TxtLastName.Text;
        }
        protected void TxtMidName_TextChanged(object sender, EventArgs e)
        {
            ViewState["MidName"] = TxtMidName.Text;
        }
        protected void grdChildInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            btnAdd.Text = "Add Child";
            int p = Convert.ToInt32(e.RowIndex);
            dtTemp = (DataTable)ViewState["DT"];
            GetHEIIDNumber();
            if (dtTemp.Rows[p][7].ToString() != "0")
            {
                IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                ptnMgrPMTCT.DeleteInfantInfo(Convert.ToInt32(dtTemp.Rows[p][7]), Convert.ToInt32(Session["AppUserId"]));
            }
            dtTemp.Rows[p].Delete();
            dtTemp.AcceptChanges();
            ViewState["DT"] = dtTemp;
            BindGrid((DataTable)ViewState["DT"]);
            txtHEINo.Text = Session["HEIIDNumber"].ToString() + "_" + ViewState["iSerialNo"];
            IQCareMsgBox.Show("DeleteSuccess", this);
            if (((DataTable)ViewState["DT"]).Rows.Count == 0)
            {
                btnsave.Enabled = false;
                //grdChildInfo.Rows.Count = -1;
                grdChildInfo.DataSource = ViewState["DT"];
                grdChildInfo.DataBind();

            }
            else
            {
                btnsave.Enabled = true;
            }

        }

        protected void grdChildInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            BindGrid((DataTable)(ViewState["DT"]));
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ChildEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
            {
                btnsave.Enabled = true;
                btnAdd.Enabled = true;

            }
            else
            {
                btnsave.Enabled = false;
                btnAdd.Enabled = false;
            }
            int thePage = grdChildInfo.PageIndex;
            int thePageSize = grdChildInfo.PageSize;
            GridViewRow theRow = grdChildInfo.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            int rowindex = theIndex;
            TxtFirstName.Text = grdChildInfo.Rows[rowindex].Cells[0].Text.ToString();
            TxtLastName.Text = grdChildInfo.Rows[rowindex].Cells[2].Text.ToString();
            string strRegdate = grdChildInfo.Rows[rowindex].Cells[3].Text.ToString();
            DateTime dtReg = Convert.ToDateTime(strRegdate);
            strRegdate = dtReg.ToString("dd-MMM-yyyy");
            TxtRegistrationDate.Text = strRegdate;
            string strgender = grdChildInfo.Rows[rowindex].Cells[4].Text.ToString();
            ListItem li = DDGender.Items.FindByText(strgender);
            if (li != null)
            {
                DDGender.SelectedIndex = -1;
                li.Selected = true;
            }
            string strDOB = grdChildInfo.Rows[rowindex].Cells[5].Text.ToString();
            DateTime dtDob = Convert.ToDateTime(strDOB);
            strDOB = dtDob.ToString("dd-MMM-yyyy");
            TxtDOB.Text = strDOB;
            txtHEINo.Text = grdChildInfo.Rows[rowindex].Cells[6].Text.ToString();
            ViewState["UpdateAddNo"] = grdChildInfo.Rows[rowindex].Cells[6].Text.ToString();
            Session["SelectedRow"] = rowindex;
            btnAdd.Text = "Update Child";





        }

        protected void txtHEINo_TextChanged(object sender, EventArgs e)
        {
            string strHEINo = txtHEINo.Text;
            if (btnAdd.Text == "Add Child")
            {
                if (strHEINo.IndexOf('-') <= 0)
                {
                    txtHEINo.Text = strHEINo + "-" + ViewState["iSerialNo"];
                    ViewState["AddStatus"] = "1";
                }
            }
            else if (btnAdd.Text == "Update Child")
            {
                string strUpdateAddNo = ViewState["UpdateAddNo"].ToString();
                if (strUpdateAddNo.IndexOf('-') >= 0)
                {
                    string[] strSplitAddNo = strUpdateAddNo.Split('-');
                    ViewState["UpdateAddNo"] = strSplitAddNo[1].ToString();
                }
                if (strHEINo.IndexOf('-') <= 0)
                {
                    txtHEINo.Text = strHEINo + "-" + ViewState["UpdateAddNo"];
                    ViewState["AddStatus"] = "2";
                }
            }

        }
        protected void grdChildInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "cmdBind")
            {
                Session["MotherPtnpk"] = Session["PatientId"];
                Session["PatientId"] = e.CommandArgument.ToString();
                string Url = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"]);
                Response.Redirect(Url);
            }

        }
    }
}