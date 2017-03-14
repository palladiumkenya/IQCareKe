using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Laboratory;
namespace IQCare.Web.Laboratory
{
    public partial class LabOrderList : System.Web.UI.Page
    {
        #region "Bind Grid"

        public void BindLaboratoryGrid()
        {

            BoundField theCol0 = new BoundField();
            theCol0.DataField = "LabID";
            theCol0.HeaderText = "LabID";
            theCol0.HeaderStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.DataField = "OrderedByDate";
            theCol1.HeaderText = "OrderedByDate";
            theCol1.HeaderStyle.CssClass = "textstyle";
            theCol1.ReadOnly = true;




            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";



            grdLabOrderList.Columns.Add(theCol0);
            grdLabOrderList.Columns.Add(theCol1);
            grdLabOrderList.Columns.Add(theBtn);

            grdLabOrderList.DataBind();
            grdLabOrderList.Columns[0].Visible = false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ILabFunctions LabManager;
            int PatientID;

            PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
            try
            {
                if (!IsPostBack)
                {
                    LabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet theDS = (DataSet)LabManager.GetPatientLabOrder(PatientID.ToString());
                    ViewState["grddata"] = theDS;
                    ViewState["gridSortDirection"] = "Desc";
                    this.grdLabOrderList.DataSource = theDS.Tables[0].DefaultView;
                    this.grdLabOrderList.DataBind();
                    BindLaboratoryGrid();
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
            Response.Redirect(theUrl);
        }

        protected void grdLabOrderList_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView theGD = (GridView)sender;
            IQCareUtils SortManager = new IQCareUtils();
            DataView theDV;
            if (ViewState["gridSortDirection"].ToString() == "Asc")
            {
                theDV = SortManager.GridSort((DataTable)ViewState["grddata"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Desc";
            }
            else
            {
                theDV = SortManager.GridSort((DataTable)ViewState["grddata"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Asc";
            }

            theGD.Columns.Clear();
            theGD.DataSource = theDV;
            BindLaboratoryGrid();
        }

        protected void grdLabOrderList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdLabOrderList.PageIndex;
            int thePageSize = grdLabOrderList.PageSize;

            GridViewRow theRow = grdLabOrderList.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int LabID = Convert.ToInt32(theRow.Cells[0].Text.ToString());

            string theUrl = string.Format("{0}LabID={1}", "LabOrder.aspx?name=" + "Edit" + "&", LabID);
            ViewState["LabId"] = LabID;

            Response.Redirect(theUrl);

        }

        protected void grdLabOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                string url = string.Format("{0}LabID={1}PatientId={2}", "LabOrder.aspx?name=" + "Edit" + "&", e.Row.Cells[0].Text + "&", Request.QueryString["PatientId"].ToString());
                e.Row.Attributes.Add("onclick", "window.location.href=('" + url + "')");   ////Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));

                //   e.Row.Attributes.Add("onclick", string.Format("{0}LabID={1}PatientId={2}", "LabOrder.aspx?name=" + "Edit" + "&", Page.ClientScript.GetPostBackEventReference(grdLabOrderList, "Select$" + e.Row.RowIndex.ToString()) + "&", Request.QueryString["PatientId"].ToString()));
                // Page.ClientScript.GetPostBackEventReference(grdLabOrderList, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            int LabId = Convert.ToInt32(ViewState["LabId"]);
            url = string.Format("{0}?LabID={1}&PatientId={2}", "LabOrder.aspx?name=" + "Add" + "&", 2, Request.QueryString["PatientId"].ToString());
            Response.Redirect(url);
        }

    }
}