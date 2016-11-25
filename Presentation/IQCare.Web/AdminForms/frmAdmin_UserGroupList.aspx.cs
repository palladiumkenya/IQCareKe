using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Application.Common;
using Application.Presentation;
using Interface.Administration;

/////////////////////////////////////////////////////////////////////
// Code Written By   : Rakhi Tyagi
// Written Date      : 1 Sept 2006
// Modification Date : 16 Feb 2007
// Description       : Add/Edit UserGroup
//
/// /////////////////////////////////////////////////////////////////
namespace IQCare.Web.Admin
{
    public partial class UserGroupList : System.Web.UI.Page
    {
        public void BindGrid()
        {
            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Group ID";
            theCol1.DataField = "GroupID";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Group Name";
            theCol2.DataField = "GroupName";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.SortExpression = "GroupName";
            theCol2.ItemStyle.Font.Underline = true;
            theCol2.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdUsergroup.Columns.Add(theCol1);
            grdUsergroup.Columns.Add(theCol2);
            grdUsergroup.Columns.Add(theBtn);

            grdUsergroup.DataBind();
            grdUsergroup.Columns[0].Visible = false;
        }

        protected void btnAddgroup_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = "frmAdmin_UserGroup.aspx?name=Add";
            Response.Redirect(theUrl);
        }

        protected void btnCancelgroup_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = "../frmFacilityHome.aspx";
            Response.Redirect(theUrl);
        }

        protected void grdUsergroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdUsergroup, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdUsergroup_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow theRow = grdUsergroup.Rows[e.NewSelectedIndex];
            int GroupId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
            string GrpNm = theRow.Cells[1].Text.ToString();

            string theUrl = string.Format("{0}GroupID={1}&Grpnm={2}", "frmAdmin_UserGroup.aspx?name=" + "Edit" + "&", GroupId, GrpNm);
            Response.Redirect(theUrl);
        }

        protected void grdUsergroup_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Init_Form();
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
            BindGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            //(Master.FindControl("lblheader") as Label).Text = "User Group Administration";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Group Administration";

            IUserRole UserRoleManager;
            try
            {
                if (!IsPostBack)
                {
                    UserRoleManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
                    DataSet theDS = (DataSet)UserRoleManager.GetUserRoleList();
                    this.grdUsergroup.DataSource = theDS.Tables[0];
                    ViewState["gridSortDirection"] = theDS.Tables[0];
                    if (ViewState["gridSource"] == null)
                    {
                        ViewState["gridSource"] = theDS.Tables[0];
                        ViewState["gridSortDirection"] = "Desc";
                    }
                    this.grdUsergroup.DataBind();

                    BindGrid();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theMsgBuilder = new MsgBuilder();
                theMsgBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsgBuilder, this);
                return;
            }
            finally
            {
                UserRoleManager = null;
            }
        }

        /****************** For Sorting ********************/
    }
}