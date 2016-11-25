using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 25th July 2006
        // Modification Date : 
        // Description       : User List
        //
        /////////////////////////////////////////////////////////////////////


        DataTable tmpDT = new DataTable();

        #region "User Function"

        private void BindGrid()
        {

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Last Name";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "UserLastName";
            theCol1.SortExpression = "UserLastName";
            theCol1.ItemStyle.Font.Underline = true;
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "First Name";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "UserFirstName";
            theCol2.SortExpression = "UserFirstName";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Groups";
            theCol3.DataField = "Groups";
            theCol3.SortExpression = "Groups";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Status";
            theCol4.DataField = "Status";
            theCol4.SortExpression = "Status";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.ReadOnly = true;

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "UserId";
            theCol5.DataField = "UserId";
            theCol5.ItemStyle.CssClass = "textstylehidden";
            theCol5.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            GrdUserList.Columns.Add(theCol1);
            GrdUserList.Columns.Add(theCol2);
            GrdUserList.Columns.Add(theCol3);
            GrdUserList.Columns.Add(theCol4);
            GrdUserList.Columns.Add(theCol5);
            GrdUserList.Columns.Add(theBtn);

            GrdUserList.DataBind();
            GrdUserList.Columns[4].Visible = false;

        }

        private void MakeGridTable(DataSet theDS)
        {
            tmpDT = theDS.Tables[0];
            tmpDT.Columns.Add("Groups");

            int i = 0;

            for (i = 0; i < tmpDT.Rows.Count; i++)
            {
                int theUserId = Convert.ToInt32(tmpDT.Rows[i]["UserId"]);
                DataView theDV = new DataView(theDS.Tables[1]);
                theDV.RowFilter = string.Format("UserId = {0}", theUserId);
                string theGroupName = "";
                if (theDV.Count > 0)
                {
                    int j = 0;
                    for (j = 0; j < theDV.Count; j++)
                    {
                        if (theGroupName.Trim() == "")
                        {
                            theGroupName = Convert.ToString(theDV[j].Row["GroupName"]);
                        }
                        else
                        {
                            theGroupName = theGroupName + ", " + Convert.ToString(theDV[j].Row["GroupName"]);
                        }
                    }

                }
                tmpDT.Rows[i]["Groups"] = theGroupName;
                theGroupName = "";
            }
            if (ViewState["gridSource"] == null)
            {
                ViewState["gridSource"] = tmpDT;
                ViewState["gridSortDirection"] = "Desc";
            }

            GrdUserList.DataSource = tmpDT;
            BindGrid();
        }

        private void Init_Form()
        {
            try
            {
                Iuser UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
                DataSet theDS = UserManager.GetUserList();
                MakeGridTable(theDS);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }



        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;

            //(Master.FindControl("lblheader") as Label).Text = "User Administration";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Administration";
            if (Page.IsPostBack == false)
            {
                Init_Form();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmadmin_adduser.aspx");
        }

        protected void GrdUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdUserList.PageIndex = e.NewPageIndex;
            GrdUserList.Columns.Clear();
            Init_Form();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmFacilityHome.aspx");
        }

        protected void GrdUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdUserList, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void GrdUserList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdUserList.PageIndex;
            int thePageSize = GrdUserList.PageSize;

            GridViewRow theRow = GrdUserList.Rows[e.NewSelectedIndex];

            if (theRow.Cells[3].Text.ToString() != "InActive")
            {
                int UserId = Convert.ToInt32(theRow.Cells[4].Text.ToString());
                string theUrl = string.Format("{0}?SelectedUserId={1}", "frmadmin_adduser.aspx", UserId);
                Response.Redirect(theUrl);
            }
            else
            {
                IQCareMsgBox.Show("UserListInactiveSelect", this);
            }
        }

        protected void GrdUserList_Sorting(object sender, GridViewSortEventArgs e)
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
    }
}