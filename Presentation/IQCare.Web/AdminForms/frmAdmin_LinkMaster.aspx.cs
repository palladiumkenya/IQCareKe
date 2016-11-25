using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class LinkMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Id"] = Convert.ToInt32(Request.QueryString["SelectedId"]);
                ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                ViewState["CategoryId"] = Request.QueryString["CategoryId"].ToString();
                ViewState["ListName"] = Request.QueryString["LstName"].ToString();
                ViewState["FID"] = Request.QueryString["Fid"].ToString();
                ViewState["Update"] = Request.QueryString["Upd"].ToString();
                ViewState["SortDirection"] = "Asc";
                btnSave.Enabled = false;

                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();

                lblHeader.Text = Request.QueryString["LstName"].ToString();
                if (lblHeader.Text == "Region-District")
                {
                    lblfrom.Text = "Region:";
                    lblto.Text = "District:";
                    FillDropDownsRD();
                }
                else if (lblHeader.Text == "District-Ward")
                {
                    lblfrom.Text = "District:";
                    lblto.Text = "Ward:";
                    FillDropDownsDW();
                }
                else if (lblHeader.Text == "District-Division")
                {
                    lblfrom.Text = "District:";
                    lblto.Text = "Division:";
                    FillDropDownsDD();
                }
                else if (lblHeader.Text == "Ward-Village")
                {
                    lblfrom.Text = "Ward:";
                    lblto.Text = "Village:";
                    FillDropDownsWV();
                }
                else if (lblHeader.Text == "Counselling Type-Counselling Topic")
                {
                    lblfrom.Text = "Counselling Type:";
                    lblto.Text = "Counselling Topic:";

                    FillDropDownsCTT();
                }
                AuthenticationManager Authentication = new AuthenticationManager();
                if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                }
            }
        }

        private void FillDropDownsRD()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["mst_Province"]);
            theDV.Sort = "Name asc";
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlRegion, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["mst_District"]);
            theDV.Sort = "Name asc";
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                //BindManager.BindCheckedList(chkDestict, theDT, "Name", "ID");
                //theDV.Dispose();
                //theDT.Clear();
                BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }

        private void FillDropDownsDW()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["mst_District"]);
            theDV.Sort = "Name asc";
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlRegion, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["mst_Ward"]);
            theDV.Sort = "Name asc";
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
               
                BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }

        private void FillDropDownsDD()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["mst_District"]);

            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlRegion, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["mst_Division"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
               
                BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }

        private void FillDropDownsWV()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["mst_Ward"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlRegion, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["mst_Village"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
              
                BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }

        public void ShowControlNew(DataTable theDT)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<table id='tbVillage' cellpadding='0' cellspacing='2' width='100%' border='0'>");

            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                str.Append("<tr>");
                str.Append("<td><input type='checkbox' id='" + theDT.Rows[i]["Id"].ToString() + "'/>" + theDT.Rows[i]["Name"].ToString() + "</td>");
                str.Append("</tr>");
            }
            str.Append("</table>");
            //showresult.InnerHtml = str.ToString();
        }

        private void FillDropDownsCTT()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

            DataView theDV = new DataView(theDSXML.Tables["mst_CouncellingType"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlRegion, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["mst_CouncellingTopic"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
             
                BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }

        public void FillSelected()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataTable theDT = new DataTable();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["mst_District"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDV.Sort = "Name asc";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
        }

        private void Bind_Grid(DataTable theDT)
        {
            grdLink.Columns.Clear();
            grdLink.DataSource = theDT;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = theDT.Columns[1].ColumnName.ToString();
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.ItemStyle.Width = 300;
            theCol1.DataField = theDT.Columns[1].ColumnName.ToString();
            theCol1.SortExpression = theDT.Columns[1].ColumnName.ToString();
            theCol1.ItemStyle.Font.Underline = true;
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = theDT.Columns[3].ColumnName.ToString();
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ItemStyle.Width = 300;
            theCol2.DataField = theDT.Columns[3].ColumnName.ToString();
            theCol2.SortExpression = theDT.Columns[3].ColumnName.ToString();
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = theDT.Columns[0].ColumnName.ToString();
            theCol3.DataField = theDT.Columns[0].ColumnName.ToString();
            theCol3.SortExpression = theDT.Columns[0].ColumnName.ToString();
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = theDT.Columns[2].ColumnName.ToString();
            theCol4.DataField = theDT.Columns[2].ColumnName.ToString();
            theCol4.SortExpression = theDT.Columns[2].ColumnName.ToString();
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.ReadOnly = true;

            CommandField objfield = new CommandField();
            objfield.ButtonType = ButtonType.Link;
            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete' border='0' />";
            objfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            objfield.ItemStyle.Width = 50;
            objfield.ShowDeleteButton = true;

            grdLink.Columns.Add(theCol1);
            grdLink.Columns.Add(theCol2);
            grdLink.Columns.Add(theCol3);
            grdLink.Columns.Add(theCol4);
            grdLink.Columns.Add(objfield);

            grdLink.DataBind();
            grdLink.Columns[2].Visible = false;
            grdLink.Columns[3].Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICustomList CustomManager;
            try
            {
                if (Convert.ToInt32(ddlRegion.SelectedValue) <= 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = lblfrom.Text;
                    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    ddlRegion.Focus();
                    return;
                }

                if (Convert.ToInt32(ddDistrict.SelectedValue) <= 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = lblto.Text;
                    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    ddDistrict.Focus();
                    return;
                }
                CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
                DataTable DTupdateprior = new DataTable();
                ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");

                int DeleteCount = CustomManager.DeleteCustomMasterLinkRecord(ViewState["TableName"].ToString(), Convert.ToInt32(ddlRegion.SelectedItem.Value));
                DataTable theDT = (DataTable)ViewState["GrdData"];
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    int RowsAffected = CustomManager.SaveUpdateCustomMasterLinkRecord(ViewState["TableName"].ToString(), Convert.ToInt32(theDT.Rows[i][0]), Convert.ToInt32(theDT.Rows[i][2]), Convert.ToInt32(Session["AppUserId"]));
                }

                string theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
                Response.Redirect(theUrl);
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
                CustomManager = null;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(theUrl);
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            DataTable DTFindRecord = new DataTable();
            ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DTFindRecord = CustomManager.GetCustomMasterLinkRecord(ViewState["TableName"].ToString(), Convert.ToInt32(ddlRegion.SelectedItem.Value));
            DataColumn[] thePKey = new DataColumn[2];
            thePKey[0] = DTFindRecord.Columns[0];
            thePKey[1] = DTFindRecord.Columns[2];
            DTFindRecord.PrimaryKey = thePKey;
            ViewState["GrdData"] = DTFindRecord;
            Bind_Grid(DTFindRecord);

           
        }

        public void FillNonSelectd()
        {
            ICustomList CustomManager;
            BindFunctions BindManager = new BindFunctions();
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            DataTable DTFindRecord = new DataTable();
            ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DTFindRecord = CustomManager.GetCustomMasterNonSelectedRecord(ViewState["TableName"].ToString());
            if (DTFindRecord != null)
            {
                //BindManager.BindCheckedList(chkDestict, DTFindRecord, "Name", "ID");
                DTFindRecord.Dispose();
                DTFindRecord.Clear();
            }
        }

        protected void grdLink_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton objlink = (LinkButton)e.Row.Cells[4].Controls[0];
                objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this record ?')) return false;";
                e.Row.Cells[4].ID = e.Row.RowIndex.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlRegion.SelectedValue) <= 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = lblfrom.Text;
                    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    ddlRegion.Focus();
                    return;
                }

                if (Convert.ToInt32(ddDistrict.SelectedValue) <= 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = lblto.Text;
                    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    ddDistrict.Focus();
                    return;
                }

                btnSave.Enabled = true;
                DataTable theDT = (DataTable)ViewState["GrdData"];
                DataRow theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(ddlRegion.SelectedValue);
                theDR[1] = ddlRegion.SelectedItem.ToString();
                theDR[2] = Convert.ToInt32(ddDistrict.SelectedValue);
                theDR[3] = ddDistrict.SelectedItem.ToString();
                theDT.Rows.Add(theDR);
                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = new DataView(theDT);
                theDV.Sort = theDT.Columns[3].ColumnName.ToString() + " asc";
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                ViewState["GrdData"] = theDT;
                Bind_Grid(theDT);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        protected void grdLink_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ICustomList CustomManager;
                DataTable theDT = (DataTable)ViewState["GrdData"];
                Bind_Grid(theDT);
                ViewState["GrdData"] = theDT;
                int r = Convert.ToInt32(e.RowIndex.ToString());
                int Id = -1;

                if (theDT.Rows.Count > 0)
                {
                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r][2] != null) && (theDT.Rows[r][2] != DBNull.Value))
                        {
                            if (theDT.Rows[r][2].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r][2]);
                                CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                                CustomManager.DeleteCustomMasterLinkRecordParticular(ViewState["TableName"].ToString(), Convert.ToInt32(Id));
                            }
                        }
                    }

                    theDT.Rows[r].Delete();
                    theDT.AcceptChanges();
                    ViewState["GrdData"] = theDT;
                    grdLink.Columns.Clear();
                    grdLink.DataSource = ViewState["GrdData"];
                    Bind_Grid(theDT);
                    IQCareMsgBox.Show("DeleteSuccess", this);

                    if (((DataTable)ViewState["GrdData"]).Rows.Count == 0)
                        btnSave.Enabled = false;
                    else
                        btnSave.Enabled = true;
                }
                else
                {
                    grdLink.Visible = false;
                    IQCareMsgBox.Show("DeleteSuccess", this);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void grdLink_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                ViewState["SortDirection"] = "Desc";
                theDV = clsUtil.GridSort((DataTable)ViewState["GrdData"], e.SortExpression, ViewState["SortDirection"].ToString());
            }
            else
            {
                ViewState["SortDirection"] = "Asc";
                theDV = clsUtil.GridSort((DataTable)ViewState["GrdData"], e.SortExpression, ViewState["SortDirection"].ToString());
            }
            grdLink.Columns.Clear();
            Bind_Grid(clsUtil.CreateTableFromDataView(theDV));
        }
    }
}