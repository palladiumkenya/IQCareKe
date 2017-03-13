using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using System.Web;
using IQCare.Web.UILogic;

namespace IQCare.Web.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FollowUpEducationCTC : BasePage
    {
        /// <summary>
        /// The authentication
        /// </summary>
        private AuthenticationManager Authentication = new AuthenticationManager();
        /// <summary>
        /// The followup education
        /// </summary>
        private IFollowupEducation educMgr;
        /// <summary>
        /// Binds the type of the councelling.
        /// </summary>
        public void BindCouncellingType()
        {
            BindFunctions BindManager = new BindFunctions();
            educMgr = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
            DataSet dsFollowupEducation = educMgr.GetCouncellingType();
            BindManager.BindCombo(ddlcouncellingtype, dsFollowupEducation.Tables[0], "Name", "Id");
        }

        /// <summary>
        /// Handles the Click event of the btnadd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnadd_Click(object sender, EventArgs e)
        {
            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            DateTime RegistrationDate = Convert.ToDateTime(dtPatientInfo.Rows[0]["RegistrationDate"].ToString());
            if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnaddtopic.Enabled = false;
            }

            if (Convert.ToInt32(ddlcouncellingtype.SelectedValue) < 1)
            {
                IQCareMsgBox.Show("CouncellingNotSelected", this);
                return;
            }
            if (Convert.ToInt32(ddlcouncellingtopic.SelectedValue) < 1)
            {
                IQCareMsgBox.Show("CouncellingTopicNotSelected", this);
                return;
            }
            if (txtvisitdate.Text == "")
            {
                IQCareMsgBox.Show("MissingVisitDate", this);
                return;
            }
            IQCareUtils theUtil = new IQCareUtils();
            if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtvisitdate.Text)))
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitdate.Focus();
                return;
            }
            if (RegistrationDate > Convert.ToDateTime(theUtil.MakeDate(txtvisitdate.Text)))
            {
                IQCareMsgBox.Show("VisitDateFollowup", this);
                txtvisitdate.Focus();
                return;
            }
            DataTable theDT = (DataTable)Session["GridData"];
            //DataView theDV = new DataView(theDT);
            
            //theDV.Sort = "VisitDate Desc";
            if (btnaddtopic.Text != "Update Topic")
            {
                string strDate = txtvisitdate.Text;
                string _typeId = ddlcouncellingtype.SelectedValue;
                string _topic = ddlcouncellingtopic.SelectedValue;
                if (theDT != null)
                {
                    DataRow[] dups = theDT.Select(string.Format("VisitDate = '{0}' and CouncellingTypeId={1} and CouncellingType={2}", strDate, _typeId, _topic));
                    if (dups.Length > 0)
                    {
                        IQCareMsgBox.Show("DuplicateFollowupEducation", this);
                        ddlcouncellingtopic.Focus();
                        return;
                    }
                }
                //if (theDV.Count > 0 && theDV[0]["CouncellingTopicId"].ToString() == ddlcouncellingtopic.SelectedValue)
                //{
                //    IQCareMsgBox.Show("DuplicateFollowupEducation", this);
                //    ddlcouncellingtopic.Focus();
                //    return;
                //}

                
            }
            if (Session["SaveFlag"].ToString() == "Add")
            {
                IQCareUtils theUtils = new IQCareUtils();
                theDT = (DataTable)Session["GridData"];
                DataRow theDR = theDT.NewRow();
                theDR["Ptn_Pk"] = this.PatientId;
                theDR["VisitDate"] = txtvisitdate.Text;
                theDR["CouncellingTypeId"] = ddlcouncellingtype.SelectedItem.Value;
                theDR["CouncellingType"] = ddlcouncellingtype.SelectedItem.Text;
                theDR["CouncellingTopicId"] = ddlcouncellingtopic.SelectedItem.Value;
                theDR["CouncellingTopic"] = ddlcouncellingtopic.SelectedItem.Text;
                theDR["OtherDetail"] = txtotherctopic.Text;
                theDR["Comments"] = txtcomments.Text;
                theDR["Visit_pk"] = 0;
                theDR.SetField("RowStatus", "Added");
                theDT.Rows.Add(theDR);
                Session["GridData"] = theDT;
                grdFollowupEducation.Columns.Clear();
                grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
            }
            else if (Session["SaveFlag"].ToString() == "Edit")
            {
                if (((DataTable)Session["GridData"]).Rows.Count < 1)
                {
                    IQCareMsgBox.Show("PharmacyNoData", this);
                    Refresh();
                    return;
                }

                int r = Convert.ToInt32(Session["SelectedRow"]);
                if (theDT.Rows[r]["RowStatus"].ToString() == "Added")
                {
                    theDT.Rows[r].Delete();
                    theDT.AcceptChanges();
                    DataRow theDR = theDT.NewRow();
                    theDR["Ptn_Pk"] = Session["Ptn_Pk"];
                    theDR["VisitDate"] = txtvisitdate.Text;
                    theDR["CouncellingTypeId"] = ddlcouncellingtype.SelectedItem.Value;
                    theDR["CouncellingType"] = ddlcouncellingtype.SelectedItem.Text;
                    theDR["CouncellingTopicId"] = ddlcouncellingtopic.SelectedItem.Value;
                    theDR["CouncellingTopic"] = ddlcouncellingtopic.SelectedItem.Text;
                    theDR["OtherDetail"] = txtotherctopic.Text;
                    theDR["Comments"] = txtcomments.Text;
                    theDR["Visit_pk"] = 0;
                    theDR.SetField("RowStatus", "Added");
                    theDT.Rows.Add(theDR);
                }
                else
                {
                    theDT.Rows[r]["VisitDate"] = txtvisitdate.Text;
                    theDT.Rows[r]["Comments"] = txtcomments.Text;
                    theDT.Rows[r].SetField("RowStatus", "Modified");
                }
                theDT.AcceptChanges();
                Session["GridData"] = theDT;
                grdFollowupEducation.Columns.Clear();
                grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
            }
            BindGrid();
            Refresh();
            btnaddtopic.Text = "Add Topic";
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" +PatientId.ToString());
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (((DataTable)Session["GridData"]).Rows.Count < 1)
            {
                IQCareMsgBox.Show("PharmacyNoData", this);
                return;
            }

            int Id,  CouncellingTypeId, CouncellingTopicId, UserId, DeleteFlag, Visit_pk, LocationID;
            string Comments, OtherDetail;
            DateTime VisitDate;

            educMgr = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
            DataTable theMainDT = (DataTable)Session["GridData"];

            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = "RowStatus <> 'UnModified'";
            DataTable theDT = dv.ToTable();

            foreach (DataRow theDR in theDT.Rows)
            {
                if (theDR["Id"] == DBNull.Value)
                    Id = -1;
                else
                    Id = Convert.ToInt32(theDR["Id"]);
                IQCareUtils theUtils = new IQCareUtils();
               
                CouncellingTypeId = Convert.ToInt32(theDR["CouncellingTypeId"]);
                CouncellingTopicId = Convert.ToInt32(theDR["CouncellingTopicId"]);
                LocationID = Convert.ToInt32(Session["AppLocationId"]);
                UserId = Convert.ToInt32(Session["AppUserId"]);
                DeleteFlag = 0;
                Comments = theDR["Comments"].ToString();
                OtherDetail = theDR["OtherDetail"].ToString();
                VisitDate = Convert.ToDateTime(theDR["VisitDate"]);
                Visit_pk = Convert.ToInt32(theDR["Visit_pk"]);

                educMgr.SaveFollowupEducation(Id, this.PatientId, CouncellingTypeId, CouncellingTopicId, Visit_pk, LocationID, VisitDate, Comments, OtherDetail, UserId, DeleteFlag,this.ModuleId);
            }

            ClearSession();
            SaveCancel();
        }
        private int ModuleId
        {
            get
            {
                return
                  Convert.ToInt32(Session["TechnicalAreaId"].ToString());
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlcouncellingtopic control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlcouncellingtopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcouncellingtopic.SelectedValue == "35")
            {
                tdotherctopic.Visible = true;
                txtotherctopic.Focus();
            }
            else
            {
                tdotherctopic.Visible = false;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlcouncellingtype control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlcouncellingtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcouncellingtype.SelectedItem.Value.ToString() != "" && ddlcouncellingtype.SelectedItem.Value.ToString() != "0")
            {
                BindFunctions BindManager = new BindFunctions();
                educMgr = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
                DataSet dsFollowupEducation = educMgr.GetCouncellingTopic(Convert.ToInt32(ddlcouncellingtype.SelectedItem.Value.ToString()));
                BindManager.BindCombo(ddlcouncellingtopic, dsFollowupEducation.Tables[0], "Name", "Id");
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdFollowupEducation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdFollowupEducation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdFollowupEducation, "Select$" + e.Row.RowIndex.ToString()));
                    }
                }
                if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[10].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this record ?')) return false;";
                    e.Row.Cells[10].ID = e.Row.RowIndex.ToString();
                }
            }
        }

        /// <summary>
        /// Handles the RowDeleting event of the grdFollowupEducation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void grdFollowupEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["GridData"]);
            int theRow = Convert.ToInt32(e.RowIndex);

            int Id = -1;
            if (theDT.Rows.Count > 0)
            {
                if ((theDT.Rows[theRow]["Id"] != null) && (theDT.Rows[theRow]["Id"] != DBNull.Value))
                {
                    if (theDT.Rows[theRow]["Id"].ToString() != "")
                    {
                        Id = Convert.ToInt32(theDT.Rows[theRow]["Id"]);
                        educMgr = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");
                        educMgr.DeleteFollowupEducation(Id, Convert.ToInt32(Session["Ptn_pk"]));
                    }
                }

                theDT.Rows[theRow].Delete();
                theDT.AcceptChanges();
                Session["GridData"] = theDT;
                grdFollowupEducation.Columns.Clear();
                grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
                BindGrid();
                IQCareMsgBox.Show("DeleteSuccess", this);
                Refresh();
            }
            else
            {
                grdFollowupEducation.Visible = false;
                IQCareMsgBox.Show("DeleteSuccess", this);
                //Refresh();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdFollowupEducation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdFollowupEducation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
            {
                btnsave.Enabled = true;
                btnaddtopic.Enabled = true;
            }
            if (Session["lblpntstatus"].ToString() == "1")
            {
                btnaddtopic.Enabled = false;
                btnsave.Enabled = false;
            }
            else
            {
                btnaddtopic.Enabled = true;
            }

            int thePage = grdFollowupEducation.PageIndex;
            int thePageSize = grdFollowupEducation.PageSize;

            GridViewRow theRow = grdFollowupEducation.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["GridData"]);

            int r = theIndex;

            // Fill data in Textboxes from grid
            //Edit the selected row
            if (theDT.Rows.Count > 0)
            {
                txtvisitdate.Text = theDT.Rows[r]["VisitDate"].ToString();
                ddlcouncellingtype.SelectedValue = theDT.Rows[r]["CouncellingTypeId"].ToString();
                ddlcouncellingtype_SelectedIndexChanged(sender, e);
                ddlcouncellingtopic.SelectedValue = theDT.Rows[r]["CouncellingTopicId"].ToString();
                txtotherctopic.Text = theDT.Rows[r]["OtherDetail"].ToString();
                txtcomments.Text = theDT.Rows[r]["Comments"].ToString();
                if (theDT.Rows[r]["CouncellingTopicId"].ToString() == "35")
                {
                    tdotherctopic.Visible = true;
                }
                else
                {
                    tdotherctopic.Visible = false;
                }

                Session["SelectedRow"] = theIndex;
                Session["SaveFlag"] = "Edit";
                btnaddtopic.Text = "Update Topic";
            }
        }

        /// <summary>
        /// Handles the Sorting event of the grdFollowupEducation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void grdFollowupEducation_Sorting(object sender, GridViewSortEventArgs e)
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
            grdFollowupEducation.Columns.Clear();
            grdFollowupEducation.DataSource = theDV;
            BindGrid();
        }
       
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentSession.Current == null || Session["PatientId"] == null || Session["TechnicalAreaId"] == null)
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect("~/frmLogin.aspx", true);
            }
        
            if (!IsPostBack)
            {
                IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                DataSet objDs = new DataSet();
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["lblpntstatus"].ToString();

                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Follow-up Education";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Follow-up Education";
                if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
                {
                    btnPrint.Enabled = false;
                }
                if (Request.QueryString["name"] == "Add")
                {
                    //Session["PtnRedirect"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                    bool blnRightfind = false;
                    if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        string theUrl = string.Empty;
                        theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + this.PatientId.ToString());
                        Response.Redirect(theUrl);
                    }
                    else if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                    {
                        blnRightfind = true;
                        btnaddtopic.Enabled = false;
                        btnsave.Enabled = false;
                    }
                    else if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                    {
                        if (!blnRightfind)
                        {
                            btnsave.Enabled = true;
                            btnaddtopic.Enabled = true;
                        }
                    }
                }

                txtvisitdate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtvisitdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

                Session["SaveFlag"] = "Add"; // "Edit"
                Session["SelectedId"] = "";
                Session["SelectedRow"] = -1;// index of row selected for editing from grid
                Session["RemoveFlag"] = "False";
                //Session["Ptn_Pk"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                BindCouncellingType();
                GetAllData();

                if (Session["lblpntstatus"].ToString() == "1")
                {
                    btnaddtopic.Enabled = false;
                    btnsave.Enabled = false;
                }
                else
                {
                    btnaddtopic.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Id";
            theCol0.DataField = "Id";
            theCol0.ItemStyle.CssClass = "textstyle";
            grdFollowupEducation.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "Ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            grdFollowupEducation.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Visit Date";
            theCol2.DataField = "VisitDate";
            theCol2.SortExpression = "VisitDate";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Councelling Type";
            theCol3.DataField = "CouncellingTypeId";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "CouncellingTypeId";
            theCol3.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Councelling Type";
            theCol4.DataField = "CouncellingType";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.SortExpression = "CouncellingType";
            theCol4.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Councelling Topic";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.DataField = "CouncellingTopicId";
            theCol5.SortExpression = "CouncellingTopicId";
            theCol5.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol5);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Councelling Topic";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "CouncellingTopic";
            theCol6.SortExpression = "CouncellingTopic";
            theCol6.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol6);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Other Councelling Topic";
            theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.DataField = "OtherDetail";
            theCol7.SortExpression = "OtherDetail";
            theCol7.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol7);

            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "Comments";
            theCol8.ItemStyle.CssClass = "textstyle";
            theCol8.DataField = "Comments";
            theCol8.SortExpression = "Comments";
            theCol8.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol8);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Visit_pk";
            theCol9.DataField = "Visit_pk";
            theCol9.SortExpression = "Visit_pk";
            theCol9.ItemStyle.CssClass = "textstyle";
            theCol9.ReadOnly = true;
            grdFollowupEducation.Columns.Add(theCol9);

            if (Authentication.HasFunctionRight(ApplicationAccess.FollowupEducation, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
            {
                CommandField objfield = new CommandField();
                objfield.ButtonType = ButtonType.Link;
                objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete' border='0' />";
                objfield.ShowDeleteButton = true;
                grdFollowupEducation.Columns.Add(objfield);
            }

            grdFollowupEducation.DataBind();
            grdFollowupEducation.Columns[0].Visible = false;
            grdFollowupEducation.Columns[1].Visible = false;
            grdFollowupEducation.Columns[3].Visible = false;
            grdFollowupEducation.Columns[5].Visible = false;
            grdFollowupEducation.Columns[9].Visible = false;
        }

        /// <summary>
        /// Clears the session.
        /// </summary>
        private void ClearSession()
        {
            Session["SaveFlag"] = null; // "Edit"
            Session["SelectedId"] = null;
            Session["SelectedRow"] = null;// index of row selected for editing from grid
        }

        /// <summary>
        /// Gets all data.
        /// </summary>
        private void GetAllData()
        {
            educMgr = (IFollowupEducation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowupEducation, BusinessProcess.Clinical");

            if (this.PatientId > 0)
            {
                DataSet theDS = educMgr.GetAllFollowupEducationData(this.PatientId);
                DataColumn newCol = new DataColumn("RowStatus", typeof(string));
                newCol.DefaultValue = "UnModified";
                theDS.Tables[0].Columns.Add(newCol);
                theDS.AcceptChanges();
                Session["GridData"] = theDS.Tables[0];
                grdFollowupEducation.DataSource = (DataTable)Session["GridData"];
                BindGrid();
            }
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        private void Refresh()
        {
            Session["SaveFlag"] = "Add";
            Session["SelectedId"] = "";
            ddlcouncellingtype.SelectedIndex = -1;
            ddlcouncellingtopic.SelectedIndex = -1;
            txtotherctopic.Text = "";
            txtvisitdate.Text = "";
            txtcomments.Text = "";
            Session["SelectedRow"] = -1;
        }

        /// <summary>
        /// Saves the cancel.
        /// </summary>
        private void SaveCancel()
        {
            string strSession = this.PatientId.ToString();
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Follow-up Education saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "Redirect(" + strSession + ");\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmFollowUpEducationCTC.aspx?name=Edit&patientid=" + this.PatientId.ToString() + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
    }
}