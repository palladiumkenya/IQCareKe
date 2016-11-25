using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;

namespace IQCare.Web.Clinical
{
    public partial class FamilyInformation : BasePage
    {
        private string ARTNos = "";
        private AuthenticationManager Authentiaction = new AuthenticationManager();
        private IFamilyInfo PatientManager;
        private string PMTCTNos = "";
        
        public string fnConvertDate(string date)
        {
            string strConvertdate = string.Empty;
            string strMonth = string.Empty;
            string strDay = string.Empty;
            string strYear = string.Empty;
            if (date != "")
            {
                string strspliter = "/";
                char[] sep = { (strspliter.ToCharArray())[0] };

                string[] strDate = date.Split(sep);

                strMonth = GetMonthName(Convert.ToInt32(strDate[0]));
                strDay = strDate[1];
                strYear = strDate[2];
            }

            return strConvertdate = strDay + "-" + strMonth + "-" + strYear;
        }

        public string GetMonthName(int id)
        {
            string strMonth = string.Empty;
            switch (id)
            {
                case 1:
                    {
                        strMonth = "Jan";
                    }
                    break;

                case 2:
                    {
                        strMonth = "Feb";
                    }
                    break;

                case 3:
                    {
                        strMonth = "Mar";
                    }
                    break;

                case 4:
                    {
                        strMonth = "Apr";
                    }
                    break;

                case 5:
                    {
                        strMonth = "May";
                    }
                    break;

                case 6:
                    {
                        strMonth = "Jun";
                    }
                    break;

                case 7:
                    {
                        strMonth = "Jul";
                    }
                    break;

                case 8:
                    {
                        strMonth = "Aug";
                    }
                    break;

                case 9:
                    {
                        strMonth = "Sep";
                    }
                    break;

                case 10:
                    {
                        strMonth = "Oct";
                    }
                    break;

                case 11:
                    {
                        strMonth = "Nov";
                    }
                    break;

                case 12:
                    {
                        strMonth = "Dec";
                    }
                    break;

                default:
                    {
                        strMonth = "";
                    }
                    break;
            }
            return strMonth;
        }

        protected void btnAdd(object sender, EventArgs e)
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;
                btnadd.Enabled = false;
            }

            if (FieldValidation())
            {
                DataRow[] foundRows;
                grdFamily.Visible = true;
                DataTable theDT = new DataTable();
                theDT = (DataTable)Session["GridData"];
                foundRows = theDT.Select("RFirstName='" + txtfname.Text + "' and RLastName='" + txtlname.Text + "'");
                if (Session["SaveFlag"].ToString() == "Add")
                {
                    if (foundRows.Length < 1)
                    {
                        //Add mode - a new member is be added and he is not already in the grid

                        theDT = (DataTable)Session["GridData"];
                        DataRow theDR = theDT.NewRow();

                        //if(Request.QueryString["RefId"]==null)
                        //    theDR["ptn_pk"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                        //else
                        //    theDR["ptn_pk"] = Convert.ToInt32(Request.QueryString["RefId"]);
                        theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                        theDR["RFirstName"] = txtfname.Text;
                        theDR["RLastName"] = txtlname.Text;
                        theDR["SexId"] = ddlgender.SelectedItem.Value; ;
                        theDR["SexDesc"] = ddlgender.SelectedItem.Text;
                        theDR["AgeYear"] = txtAgeYear.Text;
                        if (txtAgeMonth.Text != "")
                        {
                            theDR["AgeMonth"] = txtAgeMonth.Text;
                        }
                        theDR["RelationshipTypeID"] = ddlrelationtype.SelectedItem.Value;

                        if (ddlrelationtype.SelectedIndex > 0)
                            theDR["RelationshipTypeDesc"] = ddlrelationtype.SelectedItem.Text;

                        theDR["HivStatusID"] = ddlhivstatus.SelectedItem.Value; ;
                        theDR["HivStatusDesc"] = ddlhivstatus.SelectedItem.Text;
                        theDR["HivCareStatusID"] = ddlhivcstatus.SelectedItem.Value;
                        theDR["HivCareStatusDesc"] = ddlhivcstatus.SelectedItem.Text;
                        if (ddlrelationtype.SelectedItem.Value.ToString() == "3" || ddlrelationtype.SelectedItem.Value.ToString() == "11")
                        {
                            if (txtRelationDate.Value != "")
                                theDR["RelationshipDate"] = Convert.ToDateTime(txtRelationDate.Value).ToShortDateString();
                        }
                        else
                        {
                            theDR["RelationshipDate"] = "";
                        }

                        if (Session["ReferenceId"] == null)
                            theDR["Registered?"] = "No";
                        else if (Session["ReferenceId"].ToString() == "")
                            theDR["Registered?"] = "No";
                        else
                        {
                            theDR["Registered?"] = "Yes";
                            theDR["ReferenceId"] = Convert.ToInt32(Session["ReferenceId"]);
                        }

                        if (Session["RegistrationNo"] != null)
                            theDR["RegistrationNo"] = Session["RegistrationNo"].ToString();

                        if (Session["ClinicID"] != null)
                            theDR["FileNo"] = Session["ClinicID"].ToString();

                        theDT.Rows.Add(theDR);

                        Session["GridData"] = theDT;
                        grdFamily.Columns.Clear();
                        grdFamily.DataSource = (DataTable)Session["GridData"];
                        btnfind.Visible = false;
                    }
                    else
                    {
                        IQCareMsgBox.Show("FamilyMemberExists", this);
                        return;
                    }
                }
                else if (Session["SaveFlag"].ToString() == "Edit")
                {
                    //Edit mode- ie member is selected from grid

                    int r = Convert.ToInt32(Session["SelectedRow"]);

                    theDT.Rows[r]["RFirstName"] = txtfname.Text;
                    theDT.Rows[r]["RLastName"] = txtlname.Text;
                    theDT.Rows[r]["SexId"] = ddlgender.SelectedItem.Value; ;
                    theDT.Rows[r]["SexDesc"] = ddlgender.SelectedItem.Text;
                    theDT.Rows[r]["AgeYear"] = txtAgeYear.Text;
                    if (txtAgeMonth.Text != "")
                    {
                        theDT.Rows[r]["AgeMonth"] = txtAgeMonth.Text;
                    }
                    theDT.Rows[r]["RelationshipTypeID"] = ddlrelationtype.SelectedItem.Value;
                    if (ddlrelationtype.SelectedIndex > 0)
                        theDT.Rows[r]["RelationshipTypeDesc"] = ddlrelationtype.SelectedItem.Text;

                    theDT.Rows[r]["HivStatusID"] = ddlhivstatus.SelectedItem.Value; ;
                    theDT.Rows[r]["HivStatusDesc"] = ddlhivstatus.SelectedItem.Text;
                    theDT.Rows[r]["HivCareStatusID"] = ddlhivcstatus.SelectedItem.Value;
                    theDT.Rows[r]["HivCareStatusDesc"] = ddlhivcstatus.SelectedItem.Text;
                    if (ddlrelationtype.SelectedItem.Value.ToString() == "3" || ddlrelationtype.SelectedItem.Value.ToString() == "11")
                    {
                        if (txtRelationDate.Value != "")
                            theDT.Rows[r]["RelationshipDate"] = Convert.ToDateTime(txtRelationDate.Value).ToShortDateString();
                    }
                    else
                    {
                        theDT.Rows[r]["RelationshipDate"] = "";
                    }

                    if (theDT.Rows[r]["ReferenceId"] == null)
                        theDT.Rows[r]["Registered?"] = "No";
                    else if (theDT.Rows[r]["ReferenceId"].ToString() == "")
                        theDT.Rows[r]["Registered?"] = "No";
                    else
                        theDT.Rows[r]["Registered?"] = "Yes";

                    Session["GridData"] = theDT;
                    grdFamily.Columns.Clear();
                    grdFamily.DataSource = (DataTable)Session["GridData"];
                }
                if (((DataTable)Session["GridData"]).Rows.Count == 0)
                    btnSubmit.Enabled = false;
                else
                    btnSubmit.Enabled = true;

                BindGrid();
                Refresh();
                btnadd.Text = "Add Member";
                Session["ReferenceId"] = "";
                Session["RegistrationNo"] = "";
                btnSubmit.Enabled = true;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            if (Session["CEForm"] == null)
            {
                if (Request.QueryString["name"] != null)
                {
                    if (Request.QueryString["name"] == "Edit")
                    {
                        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["Ptn_Pk"].ToString());
                    }
                    else
                    {
                        if (Session["SystemId"].ToString() == "1")
                        {
                            if (Session["fmLocationID"] != null && Session["fmsts"] != null)
                            {
                                //theUrl = string.Format("../ClinicalForms/frmClinical_Enrolment.aspx?name=Edit&patientid=" + Convert.ToString(Session["PtnRedirect"]) + "&locationid=" + Convert.ToString(Session["fmLocationID"]) + "&sts=" + Session["fmsts"].ToString() + "");
                                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PtnRedirect"].ToString());
                            }
                            else
                            {
                                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PtnRedirect"].ToString());
                                Response.Redirect(theUrl);
                            }
                        }
                        else
                        {
                            if (Session["fmLocationID"] != null && Session["fmsts"] != null)
                            {
                                theUrl = string.Format("../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit&patientid=" + Session["PtnRedirect"] + "&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                            }
                            else
                            {
                                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PtnRedirect"].ToString());
                                Response.Redirect(theUrl);
                            }
                        }
                    }
                    Response.Redirect(theUrl);
                }
                else
                {
                    if (Request.QueryString["Refid"] != null)
                    {
                        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["Ptn_Pk"].ToString());
                        Response.Redirect(theUrl);
                    }
                    else
                    {
                        if (Session["PtnRedirect"] != null && Session["fmLocationID"] != null)
                        {
                            if (Session["SystemId"].ToString() == "1")
                            {
                                theUrl = string.Format("../ClinicalForms/frmClinical_Enrolment.aspx?name=Edit&patientid=" + Session["PtnRedirect"] + "&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                            }
                            else
                            {
                                theUrl = string.Format("../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit&patientid=" + Session["PtnRedirect"] + "&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                            }

                            Response.Redirect(theUrl);
                        }
                        else
                        {
                            theUrl = string.Format("../frmFindAddPatient.aspx?FormName=FamilyInfo");
                            Response.Redirect(theUrl);
                        }
                    }
                }
            }
            else
            {
                theUrl = string.Format("../ClinicalForms/frmClinical_Enrolment.aspx?name=Edit&patientid=" + Session["CEPtnPk"] + "&locationid=" + Session["fmLocationID"].ToString() + "&sts=" + Session["fmsts"].ToString() + "");
                Response.Redirect(theUrl);
            }
        }

        protected void btnFind(object sender, EventArgs e)
        {
            string theUrl = string.Format("./FindPatient.aspx?FormName=FamilyInfo");
            if (Session["SaveFlag"] != null)
            {
                if (Session["SaveFlag"].ToString() == "Edit")
                {
                    Session["SaveFlag"] = "Add";
                }
            }
            Response.Redirect(theUrl);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int Id, Ptn_Pk = 0, Sex, AgeYear, AgeMonth, RelationshipType, HivStatus = 0, HivCareStatus = 0, UserID, DeleteFlag, ReferenceId;
            string RFirstName, RLastName, RegistrationNo, RelationshipDate;

            PatientManager = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");

            DataTable theDT = (DataTable)Session["GridData"];

            foreach (DataRow theDR in theDT.Rows)
            {
                if (theDR["Id"] == DBNull.Value)
                    Id = -1;
                else
                    Id = Convert.ToInt32(theDR["Id"]);

                Ptn_Pk = Convert.ToInt32(Session["Ptn_Pk"]);
                RFirstName = theDR["RFirstName"].ToString();
                RLastName = theDR["RLastName"].ToString();
                Sex = Convert.ToInt32(theDR["SexId"]);
                AgeYear = Convert.ToInt32(theDR["AgeYear"]);
                if (theDR["AgeMonth"] != DBNull.Value)
                {
                    AgeMonth = Convert.ToInt32(theDR["AgeMonth"]);
                }
                else
                {
                    AgeMonth = 0;
                }
                if (!string.IsNullOrEmpty(theDR["RelationshipTypeId"].ToString()))
                    RelationshipType = Convert.ToInt32(theDR["RelationshipTypeId"]);
                else
                    RelationshipType = 0;

                UserID = Convert.ToInt32(Session["AppUserId"]);
                DeleteFlag = 0;
                if (theDR["ReferenceId"] != DBNull.Value)
                {
                    ReferenceId = Convert.ToInt32(theDR["ReferenceId"]);
                    if (theDR["HivCareStatusID"].ToString() != "")
                        HivCareStatus = Convert.ToInt32(theDR["HivCareStatusID"]);
                    if (theDR["HivStatusID"].ToString() != "")
                        HivStatus = Convert.ToInt32(theDR["HivStatusID"]);
                    RegistrationNo = "";
                }
                else
                {
                    HivCareStatus = Convert.ToInt32(theDR["HivCareStatusID"]);
                    HivStatus = Convert.ToInt32(theDR["HivStatusID"]);
                    ReferenceId = -1;
                    RegistrationNo = "NON";
                }

                RelationshipDate = theDR["RelationshipDate"].ToString();
                if (RelationshipDate == "")
                {
                    RelationshipDate = "01-01-1900";
                }
                PatientManager.SaveFamilyInfo(Id, Ptn_Pk, RFirstName, RLastName, Sex, AgeYear, AgeMonth, RelationshipType, HivStatus, HivCareStatus, UserID, DeleteFlag, ReferenceId, RegistrationNo, Convert.ToDateTime(RelationshipDate));
            }

            ClearSession();
            btnSubmit.Enabled = false;
            SaveCancel();
        }

        protected void DisableControls()
        {
            txtfname.Enabled = false;
            txtlname.Enabled = false;
            ddlgender.Enabled = false;
            txtAgeYear.Enabled = false;
            txtAgeMonth.Enabled = false;
            regthisclinic.Enabled = false;
            ddlhivstatus.Enabled = false;
            ddlhivcstatus.Enabled = false;
        }

        protected void EnableControls()
        {
            txtfname.Enabled = true;
            txtlname.Enabled = true;
            ddlgender.Enabled = true;
            txtAgeYear.Enabled = true;
            txtAgeMonth.Enabled = true;
            regthisclinic.Enabled = true;
            ddlhivstatus.Enabled = true;
            ddlhivcstatus.Enabled = true;
        }

        protected void grdFamily_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //So that when the user clicks on the row - the corresponding row is edited
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        if (Session["lblpntstatus"] != null)
                        {
                            if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                            {
                                e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                                e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                                e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdFamily, "Select$" + e.Row.RowIndex.ToString()));
                            }
                        }
                    }
                    if (e.Row.Cells[0].Text.ToString() == "0")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                        e.Row.Cells[18].Visible = false;
                    }
                }
                if (Session["lblpntstatus"] != null)
                {
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                    {
                        LinkButton objlink = (LinkButton)e.Row.Cells[18].Controls[0];
                        objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                        e.Row.Cells[18].ID = e.Row.RowIndex.ToString();
                        //btnSubmit.Enabled = false;
                    }
                }

            }
        }

        protected void grdFamily_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //if (!Ok2Delete(e.RowIndex)) return;
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["GridData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());

            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {
                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["Id"] != null) && (theDT.Rows[r]["Id"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["Id"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["Id"]);
                                PatientManager = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");
                                PatientManager.DeleteFamilyInfo(Id, Convert.ToInt32(Session["AppUserId"]));
                            }
                        }
                    }

                    theDT.Rows[r].Delete();
                    theDT.AcceptChanges();
                    Session["GridData"] = theDT;
                    grdFamily.Columns.Clear();
                    grdFamily.DataSource = (DataTable)Session["GridData"];
                    BindGrid();
                    //ClientScript.RegisterStartupScript(this.GetType(), "grdFamily_RowDeleting", "<script>return fnDeleteMsg();;</script>");
                    IQCareMsgBox.Show("DeleteSuccess", this);
                    Refresh();

                    if (((DataTable)Session["GridData"]).Rows.Count == 0)
                        btnSubmit.Enabled = false;
                    else
                        btnSubmit.Enabled = true;
                    Refresh();
                }
                else
                {
                    grdFamily.Visible = false;
                    IQCareMsgBox.Show("DeleteSuccess", this);
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void grdFamily_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
            {
                btnSubmit.Enabled = true;
                btnadd.Enabled = true;
            }

            if (Session["lblpntstatus"].ToString() == "1")
            {
                btnadd.Enabled = false;
                btnSubmit.Enabled = false;
            }
            else
            {
                btnadd.Enabled = true;
            }
            int thePage = grdFamily.PageIndex;
            int thePageSize = grdFamily.PageSize;

            GridViewRow theRow = grdFamily.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["GridData"]);

            int r = theIndex;

            if (theDT.Rows.Count > 0)
            {
                txtfname.Text = theDT.Rows[r]["RFirstName"].ToString();
                txtlname.Text = theDT.Rows[r]["RLastName"].ToString();
                if (theDT.Rows[r].IsNull("SexId") != true)
                {
                    ddlgender.SelectedValue = theDT.Rows[r]["SexId"].ToString();
                }
                else
                {
                    ddlgender.SelectedValue = "0";
                }

                if (theDT.Rows[r]["ReferenceId"] == null)
                {
                    regthisclinic.SelectedItem.Text = "No";
                    EnableControls();
                }
                else if (theDT.Rows[r]["ReferenceId"].ToString() == "")
                {
                    regthisclinic.SelectedItem.Text = "No";
                    EnableControls();
                }
                else
                {
                    regthisclinic.SelectedItem.Text = "Yes";
                    DisableControls();
                }

                txtAgeYear.Text = theDT.Rows[r]["AgeYear"].ToString();
                txtAgeMonth.Text = theDT.Rows[r]["agemonth"].ToString();
                if (theDT.Rows[r].IsNull("RelationshipTypeID") != true && theDT.Rows[r].IsNull("RelationshipTypeID").ToString() != string.Empty)
                {
                    ddlrelationtype.SelectedValue = theDT.Rows[r]["RelationshipTypeID"].ToString();
                }
                else
                {
                    ddlrelationtype.SelectedValue = "0";
                }
                if (theDT.Rows[r]["HivStatusID"].ToString() != "")
                {
                    ddlhivstatus.SelectedValue = theDT.Rows[r]["HivStatusID"].ToString();
                }
                if (theDT.Rows[r]["HivcareStatusID"].ToString() != "")
                {
                    ddlhivcstatus.SelectedValue = theDT.Rows[r]["HivcareStatusID"].ToString();
                }
                if (theDT.Rows[r]["RelationshipDate"].ToString() != "")
                {
                    if (theDT.Rows[r]["RelationshipTypeID"].ToString() == "3" || theDT.Rows[r]["RelationshipTypeID"].ToString() == "11")
                    {
                        txtRelationDate.Value = fnConvertDate(theDT.Rows[r]["RelationshipDate"].ToString());
                        //txtRelationDate.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["RelationshipDate"].ToString());
                        ClientScript.RegisterStartupScript(this.GetType(), "grdFamily_SelectedIndexChanging", "<script>ShowRelationDt();</script>");
                    }
                }
                Session["SelectedRow"] = theIndex;
                Session["SaveFlag"] = "Edit";
                btnadd.Text = "Update Member";
            }
        }

        protected void grdFamily_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;

            if (Session["SortDirection"].ToString() == "Asc")
            {
                theDV = clsUtil.GridSort((DataTable)Session["GridData"], e.SortExpression, Session["SortDirection"].ToString());
                Session["SortDirection"] = "Desc";
            }
            else
            {
                theDV = clsUtil.GridSort((DataTable)Session["GridData"], e.SortExpression, Session["SortDirection"].ToString());
                Session["SortDirection"] = "Asc";
            }

            grdFamily.Columns.Clear();
            grdFamily.DataSource = theDV;
            BindGrid();
            if (btnSubmit.Enabled == true)
            {
                btnSubmit.Enabled = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientStatus"] != null)
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["PatientStatus"].ToString();
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Family Information";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            Master.ExecutePatientLevel = true;
            BindHeader();

            if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["name"] == "Add")
                {
                    //Session["PtnRedirect"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                    Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
                    Boolean blnRightfind = false;
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        string theUrl = string.Empty;
                        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PtnRedirect"].ToString());
                        Response.Redirect(theUrl);
                    }
                    else if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                    {
                        blnRightfind = true;
                        btnSubmit.Enabled = false;
                        btnadd.Enabled = false;
                    }
                    else if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                    {
                        if (!blnRightfind)
                        {
                            btnSubmit.Enabled = false;
                            btnadd.Enabled = false;
                        }
                    }
                }
                btnfind.Visible = false;

                txtAgeYear.Attributes.Add("onkeyup", "chkNumeric('" + txtAgeYear.ClientID + "')");
                txtAgeMonth.Attributes.Add("onkeyup", "chkNumeric('" + txtAgeMonth.ClientID + "');");
                txtAgeMonth.Attributes.Add("onkeyup", "ChkAgeMonth();");
                //Session["PtnRedirect"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);

                //btnSubmit.Enabled = false;
                if (Request.QueryString["RefId"] == null)
                {
                    Session["SaveFlag"] = "Add";
                    Session["SelectedId"] = "";
                    Session["SelectedRow"] = -1;
                    Session["RemoveFlag"] = "False";
                    //Session["Ptn_Pk"]=Convert.ToInt32(Request.QueryString["PatientId"]);
                    Session["Ptn_Pk"] = Convert.ToInt32(Session["PatientId"]);
                    if (Session["PtnRedirect"] == null)
                    {
                        //Session["PtnRedirect"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                        Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
                    }
                    if (Request.QueryString["strfy"] != null)
                    {
                        Session["CEForm"] = Convert.ToInt32(Request.QueryString["strfy"]);
                        //Session["CEPtnPk"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                        Session["CEPtnPk"] = Convert.ToInt32(Session["PatientId"]);
                    }
                    Session["ReferenceId"] = "";
                    Session["RegistrationNo"] = "";
                    FillDropDowns();
                    GetAllData();
                }
                else
                {
                    FillDropDowns();
                    SearchFamilyInfo();
                }

                Session["SortDirection"] = "Desc";
            }
            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (dtPatientInfo != null)
            {
                if (Session["SystemId"].ToString() == "1")
                {
                    lblname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                    lblpatientnamepmtct.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                }
                else
                {
                    lblname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                    lblpatientnamepmtct.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                }
                //lblname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                //lblpatientnamepmtct.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
                lblIQnumberpmtct.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
                //lblpatientid.Text = dtPatientInfo.Rows[0]["PatientEnrollmentID"].ToString();
                //lblfileno.Text = dtPatientInfo.Rows[0]["PatientClinicID"].ToString();
                PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
                ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
                if (PMTCTNos != null && PMTCTNos != "")
                {
                    pmtct.Visible = true;
                    //lblancno.Text = dtPatientInfo.Rows[0]["ANCNumber"].ToString();
                    //lblpmtctno.Text = dtPatientInfo.Rows[0]["PMTCTNumber"].ToString();
                    //lbladmissionno.Text = dtPatientInfo.Rows[0]["AdmissionNumber"].ToString();
                    //lbloutpatientno.Text = dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();

                    TechnicalAreaIdentifier();
                }
                else
                {
                    pmtct.Visible = false;
                }
                if (ARTNos == "")
                {
                    divART.Visible = false;
                }
                else
                {
                    divART.Visible = true;
                }
                if (PMTCTNos != "" && ARTNos != "")
                {
                    pmtctname.Visible = false;
                }
            }

            if (Session["lblpntstatus"].ToString() == "1")
            {
                btnadd.Enabled = false;
                btnSubmit.Enabled = false;
            }
            else
            {
                btnadd.Enabled = true;
            }
            if (Session["CareEndFlag"].ToString() == "1")
            {
                btnadd.Enabled = true;
                btnSubmit.Enabled = true;
            }
        }

        protected void regthisclinic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regthisclinic.SelectedItem.Value == "1")
            {
                btnfind.Visible = true;
            }
            else
            {
                btnfind.Visible = false;
            }
        }

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Id";
            theCol0.DataField = "Id";
            theCol0.ItemStyle.CssClass = "textstyle";
            grdFamily.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            grdFamily.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Last Name";
            theCol2.DataField = "RLastName";
            theCol2.SortExpression = "RLastName";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;
            grdFamily.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "First Name";
            theCol3.DataField = "RFirstName";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "RFirstName";
            theCol3.ReadOnly = true;
            grdFamily.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            if (((DataTable)ViewState["grdDataSource"]).Rows.Count > 0)
            {
                theCol4.HeaderText = ((DataTable)ViewState["grdDataSource"]).Rows[1][0].ToString().Trim();
            }//"Existing Hosp/Clinic #";//"Enrollment No.";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.DataField = "RegistrationNo";
            theCol4.SortExpression = "RegistrationNo";
            theCol4.ReadOnly = true;
            grdFamily.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            if (((DataTable)ViewState["grdDataSource"]).Rows.Count > 0)
            {
                theCol5.HeaderText = ((DataTable)ViewState["grdDataSource"]).Rows[0][0].ToString().Trim();
            }//"Existing Hosp/Clinic #";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.DataField = "FileNo";
            theCol5.SortExpression = "FileNo";
            theCol5.ReadOnly = true;
            grdFamily.Columns.Add(theCol5);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Registered?";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "Registered?";
            theCol6.SortExpression = "Registered?";
            theCol6.ReadOnly = true;
            grdFamily.Columns.Add(theCol6);

            //BoundField theCol7 = new BoundField(); // double
            //theCol7.HeaderText = "Relation";
            //theCol7.DataField = "RelationshipTypeDesc";
            //theCol7.ItemStyle.CssClass = "textstyle";
            //grdFamily.Columns.Add(theCol7);

            BoundField theCol7 = new BoundField(); // double
            theCol7.HeaderText = "RelationshipTypeId";
            theCol7.DataField = "RelationshipTypeId";
            theCol7.ItemStyle.CssClass = "textstyle";
            grdFamily.Columns.Add(theCol7);

            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "Relationship";
            theCol8.DataField = "RelationshipTypeDesc";
            theCol8.ItemStyle.CssClass = "textstyle";
            theCol8.SortExpression = "RelationshipTypeDesc";
            theCol8.ReadOnly = true;
            grdFamily.Columns.Add(theCol8);

            BoundField theCol8_1 = new BoundField();
            theCol8_1.HeaderText = "Relationship Date";
            theCol8_1.DataField = "RelationshipDate";
            //theCol8_1.DataField = String.Format("{0:dd-MMM-yyyy}", theCol8_1.DataField);
            theCol8_1.ItemStyle.CssClass = "textstyle";
            theCol8_1.SortExpression = "RelationshipDate";
            grdFamily.Columns.Add(theCol8_1);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "SexId";
            theCol9.ItemStyle.CssClass = "textstyle";
            theCol9.DataField = "SexId";
            theCol9.ReadOnly = true;
            grdFamily.Columns.Add(theCol9);

            BoundField theCol10 = new BoundField();
            theCol10.HeaderText = "Sex";
            theCol10.DataField = "SexDesc";
            theCol10.ItemStyle.CssClass = "textstyle";
            theCol10.SortExpression = "SexDesc";
            grdFamily.Columns.Add(theCol10);

            BoundField theCol11 = new BoundField();
            theCol11.HeaderText = "Age(yrs)";
            theCol11.ItemStyle.CssClass = "textstyle";
            theCol11.DataField = "AgeYear";
            theCol11.SortExpression = "AgeYear";
            theCol11.ReadOnly = true;
            grdFamily.Columns.Add(theCol11);

            BoundField theCol12 = new BoundField();
            theCol12.HeaderText = "Age(mths)";
            theCol12.ItemStyle.CssClass = "textstyle";
            theCol12.DataField = "AgeMonth";
            theCol12.SortExpression = "AgeMonth";
            theCol12.ReadOnly = true;
            grdFamily.Columns.Add(theCol12);

            BoundField theCol13 = new BoundField();
            theCol13.HeaderText = "HivStatusId";
            theCol13.DataField = "HivStatusId";
            theCol13.ItemStyle.CssClass = "textstyle";
            grdFamily.Columns.Add(theCol13);

            BoundField theCol14 = new BoundField();
            theCol14.HeaderText = "HIVStatus";
            theCol14.ItemStyle.CssClass = "textstyle";
            theCol14.DataField = "HivStatusDesc";
            theCol14.SortExpression = "HivStatusDesc";
            theCol14.ReadOnly = true;
            grdFamily.Columns.Add(theCol14);

            BoundField theCol15 = new BoundField();
            theCol15.HeaderText = "HIVCareStatusId";
            theCol15.DataField = "HIVCareStatusId";
            theCol15.ItemStyle.CssClass = "textstyle";
            grdFamily.Columns.Add(theCol15);

            BoundField theCol16 = new BoundField();
            theCol16.HeaderText = "HIVCareStatus";
            theCol16.DataField = "HivCareStatusDesc";
            theCol16.ItemStyle.CssClass = "textstyle";
            theCol16.SortExpression = "HivCareStatusDesc";
            theCol16.ReadOnly = true;
            grdFamily.Columns.Add(theCol16);

            //ButtonField theBtn = new ButtonField();
            //theBtn.ButtonType = ButtonType.Image;
            //theBtn.HeaderText = "Delete";
            //theBtn.CommandName = "Remove";
            //theBtn.ImageUrl= "../Images/del.gif";
            ////theBtn.HeaderText = "Remove";
            //grdFamily.Columns.Add(theBtn);

            //if (Authentiaction.HasFunctionRight(ApplicationAccess.FamilyInfo, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
            //{
            CommandField objfield = new CommandField();
            objfield.ButtonType = ButtonType.Link;
            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            objfield.ShowDeleteButton = true;
            grdFamily.Columns.Add(objfield);

            //}

            grdFamily.DataBind();
            grdFamily.Columns[0].Visible = false;
            grdFamily.Columns[1].Visible = false;
            grdFamily.Columns[7].Visible = false;
            grdFamily.Columns[9].Visible = false;
            grdFamily.Columns[10].Visible = false;
            grdFamily.Columns[14].Visible = false;
            grdFamily.Columns[16].Visible = false;
        }

        private void BindHeader()
        {
            DataSet theFacilityDS = new DataSet();
            AuthenticationManager Authentication = new AuthenticationManager();
            IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            if (Session["SystemId"].ToString() == "1")
            {
                //lbldisplay.Text = "Existing Hosp/Clinic # :";
                theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.FamilyInfo, 0);
                //lbldisplay.Text = theFacilityDS.Tables[0].Rows[0][0].ToString() + ":";
            }
            else if (Session["SystemId"].ToString() == "2")
            {
                theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.FamilyInfo, 0);
                //lbldisplay.Text = theFacilityDS.Tables[0].Rows[0][0].ToString() + ":";
            }

            ViewState["grdDataSource"] = theFacilityDS.Tables[0];
        }

        private void ClearSession()
        {
            Session["SaveFlag"] = null; // "Edit"
            Session["SelectedId"] = null;
            Session["SelectedRow"] = null;// index of row selected for editing from grid
            //Session["RemoveFlag"] = null;
            //Session["Ptn_Pk"] = Convert.ToInt32(Request.QueryString["PatientId"]);
            Session["ReferenceId"] = null;
            Session["RegistrationNo"] = null;
        }

        private Boolean FieldValidation()
        {
            IIQCareSystem IQCareSecurity;
            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();
            PatientManager = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");

            if (txtfname.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "First Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtfname.Focus();
                return false;
            }
            if (txtlname.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Last Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtlname.Focus();
                return false;
            }
            if (ddlgender.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Sex";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if (txtAgeYear.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Age (Year)";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtAgeYear.Focus();
                return false;
            }

            //if (txtAgeMonth.Text == "")
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Age (Month)";
            //    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            //    txtAgeMonth.Focus();
            //    return false;
            //}
            if (Request.QueryString["RefId"] == null)
            {
                if (txtAgeMonth.Text != "")
                {
                    if ((Convert.ToInt32(txtAgeMonth.Text) < 0) || (Convert.ToInt32(txtAgeMonth.Text) > 11))
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Age (Month)";
                        IQCareMsgBox.Show("AgeMonthRange", theMsg, this);
                        return false;
                    }
                }
            }
            // Comment By deepak
            //if (ddlrelationtype.SelectedIndex == 0)
            //{
            //    MsgBuilder theMsg = new MsgBuilder();
            //    theMsg.DataElements["Control"] = "Relationship Type";
            //    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            //    return false;
            //}
            /////Removed, due to changes in Business Rules //////
            ////if (Convert.ToInt32(ddlrelationtype.SelectedValue) == 3 || Convert.ToInt32(ddlrelationtype.SelectedValue) == 11)
            ////{
            ////    DateTime dtCheckDate=Convert.ToDateTime("01/01/1900");
            ////    if (txtRelationDate.Value != "")
            ////    {
            ////        if (Convert.ToDateTime(txtRelationDate.Value) < dtCheckDate)
            ////        {
            ////            MsgBuilder theBuilder = new MsgBuilder();
            ////            theBuilder.DataElements["Control"] = "Birth/Marriage";
            ////            IQCareMsgBox.Show("DateCheck", theBuilder, this);
            ////            txtRelationDate.Focus();
            ////            ClientScript.RegisterStartupScript(this.GetType(), "grdFamily_SelectedIndexChanging", "<script>ShowRelationDt();</script>");
            ////            return false;
            ////        }
            ////        if (Convert.ToDateTime(txtRelationDate.Value) > theCurrentDate)
            ////        {
            ////            MsgBuilder theBuilder = new MsgBuilder();
            ////            theBuilder.DataElements["Control"] = "Birth/Marriage";
            ////            IQCareMsgBox.Show("DateCheckfuture", theBuilder, this);
            ////            txtRelationDate.Focus();
            ////            ClientScript.RegisterStartupScript(this.GetType(), "grdFamily_SelectedIndexChanging", "<script>ShowRelationDt();</script>");
            ////            return false;
            ////        }
            ////    }

            ////}

            if (ddlhivstatus.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "HIV Status";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if (ddlhivcstatus.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "HIV Care Status";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }

            return true;
        }
        private void FillDropDowns()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["Mst_HivCareStatus"]);
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlhivcstatus, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["Mst_RelationshipType"]);
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlrelationtype, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "DeleteFlag=0 and CodeID=10";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlhivstatus, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "DeleteFlag=0 and CodeID=4";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlgender, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }
        private void GetAllData()
        {
            if (Request.QueryString["back"] != null)
            {
                if (((DataTable)Session["GridData"]).Rows.Count == 0)
                    btnSubmit.Enabled = false;
                else
                    btnSubmit.Enabled = true;

                grdFamily.DataSource = (DataTable)Session["GridData"];
                grdFamily.DataBind();
                BindGrid();
            }
            else
            {
                PatientManager = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");
                //pr_Clinical_GetAllFamilyData_Constella
                if (Session["Ptn_Pk"].ToString() != "0")
                {
                    DataSet theDS = PatientManager.GetAllFamilyData(Convert.ToInt32(Session["Ptn_Pk"]));

                    //theDR["RelationshipDate"]=string.Format("{0:dd-MMM-yyyy}",theDR["RelationshipDate"].ToString());
                    //string.Format("dd-MMM-yyyy",theDR["RelationshipDate"].ToString()
                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnSubmit.Enabled = false;
                    }
                    Session["GridData"] = theDS.Tables[0];
                    grdFamily.DataSource = (DataTable)Session["GridData"];

                    BindGrid();
                }
            }
        }

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg + "')</script>"));
        }

        private bool Ok2Delete(int ri) // ri is the record index to be deleted
        {
            if (Session["ri"] == null ||
                (!((ri == ((int)Session["ri"])) &&
                (DateTime.Now.Subtract((DateTime)Session["ri_time_stamp"]).Seconds < 2))))
            {
                Session["ri"] = ri;
                Session["ri_time_stamp"] = DateTime.Now;
                return true;
            }
            return false;
        }

        private void populatefamilydata()
        {
        }
        private void Refresh()
        {
            Session["SaveFlag"] = "Add";
            Session["SelectedId"] = "";
            Session["ClinicID"] = "";
            txtfname.Text = "";
            txtlname.Text = "";
            txtAgeMonth.Text = "";
            txtAgeYear.Text = "";
            ddlgender.SelectedIndex = -1;
            regthisclinic.SelectedItem.Text = "No";
            ddlrelationtype.SelectedIndex = -1;
            ddlhivstatus.SelectedIndex = -1;
            ddlhivcstatus.SelectedIndex = -1;
            Session["SelectedRow"] = -1;
            txtRelationDate.Value = "";
            EnableControls();
        }

        private void SaveCancel()
        {
            string strSession = Session["Ptn_Pk"].ToString();
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Family Information saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "Redirect(" + strSession + ");\n";
            //script += "window.location.href('../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["Ptn_Pk"].ToString() + "');\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href('../ClinicalForms/frmFamilyInformation.aspx?name=Edit&patientid=" + Session["Ptn_Pk"].ToString() + "');\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
            //string theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["Ptn_Pk"].ToString());
        }

        private void SearchFamilyInfo()
        {
            Boolean blnValid = true;

            PatientManager = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");

            Session["ReferenceId"] = Convert.ToInt32(Request.QueryString["RefId"]);

            //---- check whether the patient is twice selected OR the patient is selecting himself as family member

            if (Convert.ToInt32(Session["ReferenceId"]) == Convert.ToInt32(Session["Ptn_Pk"])) // selecting himself
            {
                grdFamily.DataSource = (DataTable)Session["GridData"];
                grdFamily.DataBind();
                IQCareMsgBox.Show("SelectHimself", this);
                blnValid = false;
            }

            foreach (DataRow theDR in ((DataTable)Session["GridData"]).Rows)
            {
                if (theDR["ReferenceId"] != DBNull.Value)
                {
                    if (Convert.ToInt32(Session["ReferenceId"]) == Convert.ToInt32(theDR["ReferenceId"])) // patient already selected
                    {
                        grdFamily.DataSource = (DataTable)Session["GridData"];
                        grdFamily.DataBind();
                        IQCareMsgBox.Show("DuplicateSelect", this);
                        blnValid = false;
                        break;
                    }
                }
            }

            if (blnValid == true)
            {
                //Pr_Clinical_GetFamilyInfo_Constella
                DataSet theDS = PatientManager.GetSearchFamilyInfo(Convert.ToInt32(Session["ReferenceId"]));
                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0]["FirstName"] != null)
                        txtfname.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
                    if (theDS.Tables[0].Rows[0]["LastName"] != null)
                        txtlname.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
                    //if (theDS.Tables[0].Rows[0]["PatientEnrollmentID"] != null)
                    //txtpatientid.Text  = theDS.Tables[0].Rows[0]["PatientEnrollmentID"].ToString();
                    if (theDS.Tables[0].Rows[0]["Sex"] != null)
                        ddlgender.SelectedValue = theDS.Tables[0].Rows[0]["Sex"].ToString();
                    if (theDS.Tables[0].Rows[0]["AgeYear"] != null)
                        txtAgeYear.Text = theDS.Tables[0].Rows[0]["AgeYear"].ToString();
                    if (theDS.Tables[0].Rows[0]["AgeMonth"] != null)
                        txtAgeMonth.Text = theDS.Tables[0].Rows[0]["AgeMonth"].ToString();
                    if (theDS.Tables[0].Rows[0]["HivStatus"] != null && theDS.Tables[0].Rows[0]["HivStatus"].ToString() != "0" && theDS.Tables[0].Rows[0]["HivStatus"].ToString() != "")
                    {
                        ddlhivstatus.SelectedValue = theDS.Tables[0].Rows[0]["HivStatus"].ToString();
                    }
                    else
                    {
                        ddlhivstatus.SelectedValue = "39";
                    }

                    if (theDS.Tables[0].Rows[0]["PatientDiedStatus"].ToString() != "")
                    {
                        ddlhivcstatus.SelectedValue = "4";
                    }
                    else
                    {
                        if (theDS.Tables[0].Rows[0]["HivCareStatus"].ToString() != "")
                        {
                            ddlhivcstatus.SelectedValue = "2";
                        }
                        else
                        {
                            ddlhivcstatus.SelectedValue = "1";
                        }
                    }
                    if (Session["ReferenceId"] == null)
                        regthisclinic.SelectedItem.Text = "No";
                    else if (Session["ReferenceId"].ToString() == "")
                        regthisclinic.SelectedItem.Text = "No";
                    else
                        regthisclinic.SelectedItem.Text = "Yes";

                    if (theDS.Tables[0].Rows[0]["RegistrationNo"] != null)
                        Session["RegistrationNo"] = theDS.Tables[0].Rows[0]["RegistrationNo"].ToString();
                    if (theDS.Tables[0].Rows[0]["PatientClinicID"] != null)
                        Session["ClinicID"] = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                }
                DisableControls();
            }
            grdFamily.DataSource = (DataTable)Session["GridData"];
            BindGrid();
        }
        private void TechnicalAreaIdentifier()
        {
            int intmoduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
            IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(intmoduleID, Convert.ToInt32(Session["PatientId"]));

            if (DSTab.Tables[0].Rows.Count > 0)
            {
                if (DSTab.Tables[0].Rows.Count > 0)
                {
                    //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                    Label theLabelIdentifier1 = new Label();
                    theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                    theLabelIdentifier1.Text = DSTab.Tables[0].Rows[0][0].ToString() + " : " + DSTab.Tables[1].Rows[0][0].ToString();
                    thePnlIdent.Controls.Add(theLabelIdentifier1);
                    //thePnlIdent.Controls.Add(new LiteralControl("</td>"));
                }

                if (DSTab.Tables[0].Rows.Count > 1)
                {
                    //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                    Label theLabelIdentifier2 = new Label();
                    theLabelIdentifier2.ID = "Lbl_" + DSTab.Tables[0].Rows[1][0].ToString();
                    theLabelIdentifier2.CssClass = "pad18";
                    theLabelIdentifier2.Text = DSTab.Tables[0].Rows[1][0].ToString() + " : " + DSTab.Tables[1].Rows[0][1].ToString();
                    thePnlIdent.Controls.Add(theLabelIdentifier2);
                    //thePnlIdent.Controls.Add(new LiteralControl("</td>"));
                }

                if (DSTab.Tables[0].Rows.Count > 2)
                {
                    //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                    Label theLabelIdentifier3 = new Label();
                    theLabelIdentifier3.ID = "Lbl_" + DSTab.Tables[0].Rows[2][0].ToString();
                    theLabelIdentifier3.CssClass = "pad18";
                    theLabelIdentifier3.Text = DSTab.Tables[0].Rows[2][0].ToString() + " : " + DSTab.Tables[1].Rows[0][2].ToString();
                    thePnlIdent.Controls.Add(theLabelIdentifier3);
                    //thePnlIdent.Controls.Add(new LiteralControl("</td>"));
                }

                if (DSTab.Tables[0].Rows.Count > 3)
                {
                    //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                    Label theLabelIdentifier4 = new Label();
                    theLabelIdentifier4.ID = "Lbl_" + DSTab.Tables[0].Rows[3][0].ToString();
                    theLabelIdentifier4.CssClass = "pad18";
                    theLabelIdentifier4.Text = DSTab.Tables[0].Rows[3][0].ToString() + " : " + DSTab.Tables[1].Rows[0][3].ToString();
                    thePnlIdent.Controls.Add(theLabelIdentifier4);
                    //thePnlIdent.Controls.Add(new LiteralControl("</td>"));
                }
            }
        }
    }

}