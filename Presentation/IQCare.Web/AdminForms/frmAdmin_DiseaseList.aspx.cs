using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DiseaseList : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Pankaj Kumar
        // Written Date      : 25th July 2006
        // Modification Date :
        // Description       : Disease List
        //
        /// <summary>
        /// Binds the grid.
        /// </summary>
        //

        #region User functions

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Disease_pk";
            theCol0.DataField = "Disease_pk";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "DiseaseName";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "DiseaseName";
            theCol1.SortExpression = "DiseaseName";
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

            grdDisease.Columns.Add(theCol0);
            grdDisease.Columns.Add(theCol7);
            grdDisease.Columns.Add(theCol1);
            grdDisease.Columns.Add(theCol2);

            grdDisease.Columns.Add(theBtn);

            grdDisease.DataBind();
            grdDisease.Columns[0].Visible = false;
        }

        #endregion User functions

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_Disease.aspx?name=Add";
            Response.Redirect(url);
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdDisease control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdDisease_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdDisease, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the grdDisease control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void grdDisease_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = ((Label)grdDisease.SelectedRow.FindControl("lblDiseaseID")).Text;
            Response.Write(selectedText);
            Response.Write("frmAdmin_Disease.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdDisease control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdDisease_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdDisease.PageIndex;
            int thePageSize = grdDisease.PageSize;

            GridViewRow theRow = grdDisease.Rows[e.NewSelectedIndex];

            // if (theRow.Cells[3].Text.ToString() != "InActive")
            {
                int DiseaseId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
                string theUrl = string.Format("{0}diseaseid={1}", "frmAdmin_Disease.aspx?name=" + "Edit" + "&", DiseaseId);
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Handles the Sorting event of the grdDisease control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void grdDisease_Sorting(object sender, GridViewSortEventArgs e)
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
            grdDisease.Columns.Clear();
            grdDisease.DataSource = theDV;
            BindGrid();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            IDiseases DiseaseManager;
            try
            {
                if (!IsPostBack)
                {
                    DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                    DataSet theDS = DiseaseManager.GetDiseases();
                    this.grdDisease.DataSource = theDS.Tables[0];
                    this.grdDisease.DataBind();
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
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                DiseaseManager = null;
            }
        }
    }
}