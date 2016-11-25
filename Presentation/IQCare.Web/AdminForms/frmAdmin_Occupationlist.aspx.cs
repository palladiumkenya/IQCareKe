using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class Occupationlist : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Pankaj Kumar
        // Written Date      : 25th July 2006
        // Modification Date :
        // Description       : Occupation List
        //
        /// /////////////////////////////////////////////////////////////////

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "OccupationID";
            theCol0.DataField = "OccupationID";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "OccupationName";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "OccupationName";
            theCol1.SortExpression = "OccupationName";
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
            theCol7.DataField = "Sequence";
            theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.SortExpression = "Sequence";
            theCol7.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdOccupation.Columns.Add(theCol0);
            grdOccupation.Columns.Add(theCol7);
            grdOccupation.Columns.Add(theCol1);
            grdOccupation.Columns.Add(theCol2);

            grdOccupation.Columns.Add(theBtn);

            grdOccupation.DataBind();
            grdOccupation.Columns[0].Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IOccupation OccupationManager;
            try
            {
                if (!IsPostBack)
                {
                    OccupationManager = (IOccupation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BOccupation, BusinessProcess.Administration");
                    DataSet theDS = OccupationManager.GetOccupation();
                    this.grdOccupation.DataSource = theDS.Tables[0];
                    this.grdOccupation.DataBind();
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
                OccupationManager = null;
            }
        }

        protected void grdOccupation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdOccupation.PageIndex;
            int thePageSize = grdOccupation.PageSize;

            GridViewRow theRow = grdOccupation.Rows[e.NewSelectedIndex];

            // if (theRow.Cells[3].Text.ToString() != "InActive")
            {
                int OccupationId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
                string theUrl = string.Format("{0}occupationid={1}", "frmAdmin_Occupation.aspx?name=" + "Edit" + "&", OccupationId);
                Response.Redirect(theUrl);
            }
            //else
            //{
            //    IQCareMsgBox.Show("UserListInactiveSelect", this);
            //}
        }

        protected void grdOccupation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdOccupation, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_Occupation.aspx?name=Add";
            Response.Redirect(url);
        }

        protected void grdOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = ((Label)grdOccupation.SelectedRow.FindControl("lblDiseaseID")).Text;
            Response.Write(selectedText);
            Response.Write("frmAdmin_Occupation.aspx");
        }

        protected void grdOccupation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IOccupation OccupationManager;
            try
            {
                grdOccupation.PageIndex = e.NewPageIndex;
                OccupationManager = (IOccupation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BOccupation, BusinessProcess.Administration");
                DataSet theDS = OccupationManager.GetOccupation();
                this.grdOccupation.DataSource = theDS.Tables[0];
                this.grdOccupation.DataBind();
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
                OccupationManager = null;
            }
        }

        protected void grdOccupation_PageIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }

        public void ItemDataBoundEventHandler(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("ondblclick", "this.style.backgroundColor='#e1e1e1'");
                e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#ffffff'");
            }
        }

        protected void grdOccupation_Sorting(object sender, GridViewSortEventArgs e)
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
            grdOccupation.Columns.Clear();
            grdOccupation.DataSource = theDV;
            BindGrid();
        }
    }
}