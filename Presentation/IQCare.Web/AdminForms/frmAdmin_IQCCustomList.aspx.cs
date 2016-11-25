using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class CTCCustomList : System.Web.UI.Page
    {
        private void Bind_Grid()
        {
            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Priority";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "SRNo";
            theCol1.SortExpression = "SRNo";
            theCol1.ItemStyle.Font.Underline = true;
            theCol1.ReadOnly = true;
            grdCustom.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Code";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "Code";
            theCol2.SortExpression = "Code";
            theCol2.ItemStyle.Font.Underline = true;
            theCol2.ReadOnly = true;
            grdCustom.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = lblHeader.Text;
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataField = "Name";
            theCol3.SortExpression = "Name";
            theCol3.ReadOnly = true;
            grdCustom.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Status";
            theCol4.DataField = "Status";
            theCol4.SortExpression = "Status";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.ReadOnly = true;
            grdCustom.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "ID";
            theCol5.DataField = "ID";
            theCol5.SortExpression = "ID";
            theCol5.ItemStyle.CssClass = "textstylehidden";
            theCol5.ReadOnly = true;
            grdCustom.Columns.Add(theCol5);

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";
            grdCustom.Columns.Add(theBtn);

            grdCustom.DataBind();
            grdCustom.Columns[4].Visible = false;

        }

        private void Init_Form()
        {

            lblHeader.Text = ViewState["ListName"].ToString();

            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomList(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(Session["SystemId"]));
            if (ViewState["ModuleId"] != null)
            {
                if (Convert.ToInt32(ViewState["ModuleId"]) > 0 && ViewState["TableName"].ToString().ToUpper() == "DECODE")
                {
                    IQCareUtils theUtilsCF = new IQCareUtils();
                    DataView theDVCF = new DataView(theDT);
                    theDVCF.RowFilter = "ModuleId in(0," + Convert.ToInt32(ViewState["ModuleId"]) + ")";
                    theDT = new DataTable();
                    theDT = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);
                }
            }
            if (ViewState["gridSource"] == null)
            {
                ViewState["gridSource"] = theDT;
                ViewState["gridSortDirection"] = "Desc";
            }

            grdCustom.DataSource = theDT;
            Bind_Grid();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack != true)
                {
                    ViewState["CategoryId"] = 0;
                    //ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                    if (Request.QueryString["TableName"].ToString() == "PreDefinedFields")
                    {
                        ViewState["TableName"] = "ModDeCode";
                    }
                    else
                    {
                        ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                    }
                    if (Request.QueryString["CategoryId"].ToString() != "")
                    {
                        ViewState["CategoryId"] = Convert.ToInt32(Request.QueryString["CategoryId"]);
                    }
                    if (Request.QueryString["ModId"].ToString() != "")
                    {
                        ViewState["ModuleId"] = Convert.ToInt32(Request.QueryString["ModId"]);
                    }
                    ViewState["ListName"] = Request.QueryString["LstName"].ToString();
                    ViewState["FID"] = Request.QueryString["Fid"].ToString();
                    ViewState["Update"] = Request.QueryString["Upd"].ToString();
                    //    (Master.FindControl("lblMark") as Label).Visible = true;
                    //(Master.FindControl("lblRoot") as Label).Text = " » Customize Lists";
                    //(Master.FindControl("lblMark") as Label).Text = "";
                    //(Master.FindControl("lblMark") as Label).Visible = false;
                    //(Master.FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();

                    Init_Form();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(theUrl);

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //string theUrl = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCCustomPage.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
            string theUrl = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6}", "frmAdmin_IQCCustomPage.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
            Response.Redirect(theUrl);
        }

        protected void grdCustom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdCustom, "Select$" + e.Row.RowIndex.ToString()));
            }

        }

        protected void grdCustom_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            int thePage = grdCustom.PageIndex;
            int thePageSize = grdCustom.PageSize;
            GridViewRow theRow = grdCustom.Rows[e.NewSelectedIndex];
            if (theRow.Cells[4].Text.ToString() != "")
            {
                int Id = Convert.ToInt32(theRow.Cells[4].Text.ToString());

                //string theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}&Fid={5}&Upd={6} ", "frmAdmin_IQCCustomPage.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
                string theUrl = string.Format("{0}?SelectedId={1}&TableName={2}&CategoryId={3}&LstName={4}&Fid={5}&Upd={6}&ModId={7}", "frmAdmin_IQCCustomPage.aspx", Id, ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(theUrl);
            }



        }

        protected void grdCustom_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView theGD = (GridView)sender;
            IQCareUtils SortManager = new IQCareUtils();
            DataView theDV;
            if (ViewState["gridSortDirection"].ToString() == "Asc")
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Desc";
            }
            else
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Asc";
            }

            theGD.Columns.Clear();
            theGD.DataSource = theDV;
            Bind_Grid();

        }
    }

}