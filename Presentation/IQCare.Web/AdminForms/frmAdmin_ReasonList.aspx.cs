using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class ReasonList : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Pankaj Kumar
        // Written Date      : 25th July 2006
        // Modification Date :
        // Description       : Reason List
        //
        /// /////////////////////////////////////////////////////////////////

        protected void grdReason_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdReason, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IReason ReasonManager;
            try
            {
                if (!IsPostBack)
                {
                    ReasonManager = (IReason)ObjectFactory.CreateInstance("BusinessProcess.Administration.BReason, BusinessProcess.Administration");
                    DataSet theDS = ReasonManager.GetReason();
                    this.grdReason.DataSource = theDS.Tables[0];
                    ViewState["gridSortDirection"] = "Desc";

                    //this.grdReason.DataBind();
                    //this.grdReason.Columns[0].Visible = false;
                    if (ViewState["grdDataSource"] == null)
                        ViewState["grdDataSource"] = theDS.Tables[0];
                    ViewState["SortDirection"] = "Desc";
                    BindGrid();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", this);
                return;
            }
            finally
            {
                ReasonManager = null;
            }
        }

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "ReasonId";
            theCol0.DataField = "ReasonId";
            theCol0.ItemStyle.CssClass = "textstylehidden";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Reason Name";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "ReasonName";
            theCol1.SortExpression = "ReasonName";
            theCol1.ItemStyle.Font.Underline = true;

            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Status";
            theCol2.DataField = "Status";
            theCol2.SortExpression = "Status";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Priority";
            theCol7.DataField = "SRNo";
            theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.SortExpression = "SRNo";
            theCol7.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdReason.Columns.Add(theCol0);
            grdReason.Columns.Add(theCol7);
            grdReason.Columns.Add(theCol1);
            grdReason.Columns.Add(theCol2);

            grdReason.Columns.Add(theBtn);

            grdReason.DataBind();
            grdReason.Columns[0].Visible = false;
        }

        protected void grdReason_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_Reason.aspx?name=Add";
            Response.Redirect(url);
        }

        protected void grdReason_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdReason.PageIndex;
            int thePageSize = grdReason.PageSize;

            GridViewRow theRow = grdReason.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int reasonid = Convert.ToInt32(theRow.Cells[0].Text.ToString());

            {
                string theUrl = string.Format("{0}ReasonId={1}", "frmAdmin_Reason.aspx?name=" + "Edit" + "&", reasonid);
                Response.Redirect(theUrl);
            }
        }

        protected void grdReason_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Init_Form();
            GridView theGD = (GridView)sender;
            IQCareUtils SortManager = new IQCareUtils();
            DataView theDV;
            if (ViewState["gridSortDirection"].ToString() == "Asc")
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["grdDataSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Desc";
            }
            else
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["grdDataSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Asc";
            }

            theGD.Columns.Clear();
            theGD.DataSource = theDV;
            BindGrid();
        }
    }
}