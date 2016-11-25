using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class CustomItems : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 25th Aug 2006
        // Modification Date :
        // Description       : Custom Item List
        //
        /// /////////////////////////////////////////////////////////////////

        #region "User Functions"

        private void BindGrid()
        {
            IQCareUtils theUtil = new IQCareUtils();
            DataSet dscustomiseList;
            string filePath = Server.MapPath("~/XMLFiles/customizelist.xml");
            dscustomiseList = new DataSet();
            dscustomiseList.ReadXml(filePath);
            DataView theDV = new DataView(dscustomiseList.Tables[0]);
            theDV.RowFilter = "SystemId=" + Session["SystemId"].ToString() + " or SystemId=0";
            DataTable theXMLDT = theUtil.CreateTableFromDataView(theDV);

            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomFieldList(Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["AppLocationId"]));
            if (theDT != null && theDT.Rows.Count > 0)
            {
                theXMLDT.Merge(theDT);
            }
            theDV = new DataView(theXMLDT);
            theDV.Sort = "Listname asc";
            grdCustomizeItems.DataSource = theUtil.CreateTableFromDataView(theDV);
            grdCustomizeItems.DataBind();
            grdCustomizeItems.Columns[1].Visible = false;
            grdCustomizeItems.Columns[2].Visible = false;
            grdCustomizeItems.Columns[3].Visible = false;
            grdCustomizeItems.Columns[4].Visible = false;
            grdCustomizeItems.Columns[5].Visible = false;
        }

        #endregion "User Functions"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }

            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customize Lists";
        }

        public void ItemDataBoundEventHandler1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataView theDV = new DataView((DataTable)Session["UserRight"]);
                DataTable theDTModule = (DataTable)Session["AppModule"];
                string ModuleId = "";
                foreach (DataRow theDR in theDTModule.Rows)
                {
                    if (ModuleId == "")
                        ModuleId = theDR["ModuleId"].ToString();
                    else
                        ModuleId = ModuleId + "," + theDR["ModuleId"].ToString();
                }
                theDV.RowFilter = "ModuleId in (0," + ModuleId + ")";
                DataTable theDT = new DataTable();
                theDT = theDV.ToTable();

                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");

                AuthenticationManager Authentication = new AuthenticationManager();
                if ((e.Row.Cells[4].Text != "0") && (Authentication.HasFeatureRight(Convert.ToInt32(e.Row.Cells[4].Text), theDT) == false))
                {
                    string theMsg = "alert('You are Not Authorized to Access this Functionality.')";
                    e.Row.Attributes.Add("onclick", theMsg);
                }
                else
                {
                    string url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", e.Row.Cells[1].Text.ToString(), e.Row.Cells[3].Text.ToString(), e.Row.Cells[2].Text.ToString(), e.Row.Cells[0].Text.ToString(), e.Row.Cells[4].Text.ToString(), e.Row.Cells[5].Text.ToString());
                    e.Row.Attributes.Add("onclick", "window.location.href=('" + url + "')");
                }
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[0].ColumnSpan = 2;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmFacilityHome.aspx");
        }
    }
}