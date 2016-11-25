using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
namespace IQCare.Web.Clinical
{
    public partial class PatientClassificationCTC : BasePage
    {
        IPatientClassification PatientClassificationMgr;
        AuthenticationManager Authentiaction = new AuthenticationManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["lblpntstatus"].ToString();
            //(Master.FindControl("lblRoot") as Label).Text = "";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Patient Classification";
            //(Master.FindControl("lblformname") as Label).Text = "Patient Classification";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient Classification";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Patient Classification";
            IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

            txtdate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }
            if (!IsPostBack)
            {
                if (Request.QueryString["name"] == "Add")
                {
                    Session["PtnRedirect"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                  //  Boolean blnRightfind = false;
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        string theUrl = string.Empty;
                        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PtnRedirect"].ToString());
                        Response.Redirect(theUrl);
                    }
                    else if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                    {
                     //   blnRightfind = true;
                        btnadd.Enabled = false;
                        btnsave.Enabled = false;
                    }


                }

                if (Session["lblpntstatus"].ToString() == "1")
                {
                    btnadd.Enabled = false;
                    btnsave.Enabled = false;
                }
                Session["SelectedId"] = "";
                Session["SelectedRow"] = -1;// index of row selected for editing from grid
                Session["RemoveFlag"] = "False";
                Session["Ptn_Pk"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                BindClassification();
                GetAllData();
            }

        }
        private void Refresh()
        {
            Session["SelectedId"] = "";
            ddlclassification.SelectedIndex = -1;
            Session["SelectedRow"] = -1;
            txtdate.Text = "";

        }
        private void ClearSession()
        {
            Session["SelectedId"] = null;
            Session["SelectedRow"] = null;// index of row selected for editing from grid

        }
        private void SaveCancel()
        {

            string strSession = Session["Ptn_Pk"].ToString();
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Patient Classification saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "Redirect(" + strSession + ");\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmClinical_PatientClassificationCTC.aspx?name=Edit&patientid=" + Session["Ptn_Pk"].ToString() + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
        public void BindClassification()
        {
            BindFunctions BindManager = new BindFunctions();
            PatientClassificationMgr = (IPatientClassification)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientClassification, BusinessProcess.Clinical");
            DataSet dsPatientClassification = PatientClassificationMgr.GetClassification(Convert.ToInt32(Session["SystemId"]));
            BindManager.BindCombo(ddlclassification, dsPatientClassification.Tables[0], "Name", "Id");
        }
        private void GetAllData()
        {
            PatientClassificationMgr = (IPatientClassification)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientClassification, BusinessProcess.Clinical");

            if (Session["Ptn_Pk"].ToString() != "0")
            {
                DataSet theDS = PatientClassificationMgr.GetAllPatientClassificationData(Convert.ToInt32(Session["Ptn_Pk"]));
                DataTable theDT = theDS.Tables[0].Copy();
                DataColumn[] thePKey = new DataColumn[3];
                thePKey.SetValue(theDT.Columns["Ptn_PK"], 0);
                thePKey.SetValue(theDT.Columns["ARTSponsorId"], 1);
                thePKey.SetValue(theDT.Columns["VisitDate"], 2);

                theDT.PrimaryKey = thePKey;
                Session["GridData"] = theDT; /// theDS.Tables[0];
                grdPClassification.DataSource = (DataTable)Session["GridData"];
                BindGrid();
            }

        }
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "PatientId";
            theCol0.DataField = "Ptn_pk";
            theCol0.ItemStyle.CssClass = "textstyle";
            grdPClassification.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "ARTSponsorID";
            theCol1.DataField = "ARTSponsorID";
            theCol1.ItemStyle.CssClass = "textstyle";
            grdPClassification.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Classification";
            theCol2.DataField = "Name";
            theCol2.SortExpression = "Name";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;
            grdPClassification.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Visit Date:";
            theCol3.DataField = "VisitDate";
            theCol3.SortExpression = "VisitDate";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;
            grdPClassification.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Location";
            theCol4.DataField = "LocationID";
            theCol4.SortExpression = "LocationID";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.ReadOnly = true;
            grdPClassification.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Visit_pk";
            theCol5.DataField = "Visit_pk";
            theCol5.SortExpression = "Visit_pk";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.ReadOnly = true;
            grdPClassification.Columns.Add(theCol5);

            if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
            {
                CommandField objfield = new CommandField();
                objfield.ButtonType = ButtonType.Link;
                objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
                objfield.ShowDeleteButton = true;
                grdPClassification.Columns.Add(objfield);
            }


            grdPClassification.DataBind();
            grdPClassification.Columns[0].Visible = false;
            grdPClassification.Columns[1].Visible = false;
            grdPClassification.Columns[4].Visible = false;
            grdPClassification.Columns[5].Visible = false;

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
                DateTime RegistrationDate = Convert.ToDateTime(dtPatientInfo.Rows[0]["RegistrationDate"].ToString());

                if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnadd.Enabled = false;
                    btnsave.Enabled = false;
                }

                #region "Validations"
                if (Convert.ToInt32(ddlclassification.SelectedValue) < 1)
                {
                    IQCareMsgBox.Show("Classification", this);
                    return;
                }
                if (txtdate.Text == "")
                {
                    IQCareMsgBox.Show("MissingVisitDate", this);
                    return;
                }
                IQCareUtils theUtil = new IQCareUtils();
                if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtdate.Text)))
                {
                    IQCareMsgBox.Show("CompareDate5", this);
                    txtdate.Focus();
                    return;
                }
                DataTable theDT = (DataTable)Session["GridData"];
                DataView theDV = new DataView(theDT);
                theDV.Sort = "VisitDate Desc";
                if (theDV.Count > 0 && theDV[0]["ARTSponsorID"].ToString() == ddlclassification.SelectedValue)
                {
                    IQCareMsgBox.Show("DuplicateClassification", this);
                    ddlclassification.Focus();
                    return;
                }

                if (theDV.Count > 0 && Convert.ToDateTime(theDV[0]["VisitDate"]) > Convert.ToDateTime(theUtil.MakeDate(txtdate.Text)))
                {
                    IQCareMsgBox.Show("ClassificationDateinPast", this);
                    ddlclassification.Focus();
                    return;
                }
                //if ((DateTime)Session["PRegistrationDate"] > Convert.ToDateTime(theUtil.MakeDate(txtdate.Text)))
                if (RegistrationDate > Convert.ToDateTime(theUtil.MakeDate(txtdate.Text)))
                {
                    IQCareMsgBox.Show("EnrollDate", this);
                    txtdate.Focus();
                    return;
                }
                #endregion

                IQCareUtils theUtils = new IQCareUtils();
                theDT = (DataTable)Session["GridData"];
                DataRow theDR = theDT.NewRow();
                theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                theDR["ARTSponsorID"] = ddlclassification.SelectedItem.Value;
                theDR["Name"] = ddlclassification.SelectedItem.Text;
                theDR["VisitDate"] = txtdate.Text;
                theDR["LocationID"] = Convert.ToInt32(Session["AppLocationId"]);
                theDR["Visit_pk"] = 0;
                theDT.Rows.Add(theDR);
                Session["GridData"] = theDT;
                grdPClassification.Columns.Clear();
                grdPClassification.DataSource = (DataTable)Session["GridData"];
                BindGrid();
                Refresh();
                btnadd.Text = "Add";
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (((DataTable)Session["GridData"]).Rows.Count < 1)
            {
                IQCareMsgBox.Show("PharmacyNoData", this);
                return;
            }

            int Ptn_pk = 0, ARTSponsorID, UserId, LocationID, DeleteFlag, Visit_pk;

            DateTime DateEffective;
            PatientClassificationMgr = (IPatientClassification)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientClassification, BusinessProcess.Clinical");
            DataTable theDT = (DataTable)Session["GridData"];
            foreach (DataRow theDR in theDT.Rows)
            {
                IQCareUtils theUtils = new IQCareUtils();
                Ptn_pk = Convert.ToInt32(Session["Ptn_Pk"]);
                ARTSponsorID = Convert.ToInt32(theDR["ARTSponsorID"]);
                UserId = Convert.ToInt32(Session["AppUserId"]);
                LocationID = Convert.ToInt32(Session["AppLocationId"]);
                DeleteFlag = 0;
                DateEffective = Convert.ToDateTime(theDR["VisitDate"]);
                Visit_pk = Convert.ToInt32(theDR["Visit_pk"]);
                PatientClassificationMgr.SaveUpdatePatientClassification(Ptn_pk, LocationID, Visit_pk, ARTSponsorID, UserId, DateEffective, DeleteFlag);
                DataSet theDS = PatientClassificationMgr.GetAllPatientClassificationData(Convert.ToInt32(Session["Ptn_Pk"]));
                Session["GridData"] = theDS.Tables[0];
                theDT = (DataTable)Session["GridData"];
            }
            ClearSession();
            SaveCancel();
        }
        protected void grdPClassification_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdPClassification, "Select$" + e.Row.RowIndex.ToString()));
                    }
                }
                if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientClassification, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[6].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this record ?')) return false;";
                    e.Row.Cells[6].ID = e.Row.RowIndex.ToString();
                    ViewState["VisitDate"] = e.Row.Cells[3].Text;
                    ViewState["ARTSponsorID"] = e.Row.Cells[1].Text;
                }

            }
        }
        protected void grdPClassification_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["GridData"]);
            int theRow = Convert.ToInt32(e.RowIndex);

            PatientClassificationMgr = (IPatientClassification)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientClassification, BusinessProcess.Clinical");
            PatientClassificationMgr.DeletePatientClassification(Convert.ToInt32(Session["Ptn_Pk"]), Convert.ToInt32(ViewState["ARTSponsorID"]), Convert.ToDateTime(ViewState["VisitDate"]));
            theDT.Rows[theRow].Delete();
            theDT.AcceptChanges();
            Session["GridData"] = theDT;
            grdPClassification.Columns.Clear();
            grdPClassification.DataSource = (DataTable)Session["GridData"];
            BindGrid();
            IQCareMsgBox.Show("DeleteSuccess", this);
            Refresh();
            btnadd.Text = "Add";
        }

        protected void grdPClassification_Sorting(object sender, GridViewSortEventArgs e)
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
            grdPClassification.Columns.Clear();
            grdPClassification.DataSource = theDV;
            BindGrid();
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["Ptn_Pk"].ToString());
            Response.Redirect(theUrl);
        }

    }
}