using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class LPFTPatientTransferList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ICustomList LPTFListMgr;
            try
            {
                if (!IsPostBack)
                {
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "LPTF Patient Transfer";

                    LPTFListMgr = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                    DataSet theDS = LPTFListMgr.GetLPTFPatientTransfer(1);
                    this.grdMasterLPTFPatientTransfer.DataSource = theDS.Tables[0];
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
                LPTFListMgr = null;
            }
        }

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Priority";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.DataField = "ID";
            theCol0.SortExpression = "ID";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "LPTF Patient Transfer";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "Name";
            theCol1.SortExpression = "Name";
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "AIDSRelief Site";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "ARFunded";
            theCol2.SortExpression = "ARFunded";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Status";
            theCol3.DataField = "Status";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "Status";
            theCol3.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdMasterLPTFPatientTransfer.Columns.Add(theCol0);
            grdMasterLPTFPatientTransfer.Columns.Add(theCol1);
            grdMasterLPTFPatientTransfer.Columns.Add(theCol2);
            grdMasterLPTFPatientTransfer.Columns.Add(theCol3);

            grdMasterLPTFPatientTransfer.Columns.Add(theBtn);

            grdMasterLPTFPatientTransfer.DataBind();
            grdMasterLPTFPatientTransfer.Columns[0].Visible = false;
        }

        protected void grdMasterLPTFPatientTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdMasterLPTFPatientTransfer, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdMasterLPTFPatientTransfer_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdMasterLPTFPatientTransfer.PageIndex;
            int thePageSize = grdMasterLPTFPatientTransfer.PageSize;

            GridViewRow theRow = grdMasterLPTFPatientTransfer.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int LPTFId = Convert.ToInt32(theRow.Cells[0].Text.ToString());

            string theUrl = string.Format("{0}LPTFId={1}", "frmAdmin_LPTFPatientTransfer.aspx?name=" + "Edit" + "&", LPTFId);
            Response.Redirect(theUrl);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_LPTFPatientTransfer.aspx?name=Add";
            Response.Redirect(url);
        }

        protected void grdMasterLPTFPatientTransfer_Sorting(object sender, GridViewSortEventArgs e)
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

            grdMasterLPTFPatientTransfer.Columns.Clear();
            grdMasterLPTFPatientTransfer.DataSource = theDV;
            BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_PMTCT_CustomItems.aspx");
        }
    }
}