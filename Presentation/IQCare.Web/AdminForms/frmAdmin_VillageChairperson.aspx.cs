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
using Interface.Security;
namespace IQCare.Web.Admin
{
    public partial class VillageChairperson : System.Web.UI.Page
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
                //(Master.FindControl("lblRoot") as Label).Text = " » Customize Lists";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();
                FillDropDownsRegion();
                AuthenticationManager Authentication = new AuthenticationManager();
                if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                }

            }
            Page.EnableViewState = true;
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICustomList CustomManager;
            try
            {
                CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
                DataTable DTupdateprior = new DataTable();
                ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                if (btnSave.Text == "Save")
                {
                    int RowsAffected = CustomManager.SaveUpdateVillageChairperson(Convert.ToInt32(ddlRegion.SelectedItem.Value), Convert.ToInt32(ddDistric.SelectedItem.Value), Convert.ToInt32(ddWard.SelectedItem.Value), Convert.ToInt32(ddlVillage.SelectedItem.Value), txtChairPerson.Text, Convert.ToInt32(Session["AppUserId"]), 1);
                }
                else if (btnSave.Text == "Update")
                {
                    int RowsAffected = CustomManager.SaveUpdateVillageChairperson(Convert.ToInt32(ddlRegion.SelectedItem.Value), Convert.ToInt32(ddDistric.SelectedItem.Value), Convert.ToInt32(ddWard.SelectedItem.Value), Convert.ToInt32(ddlVillage.SelectedItem.Value), txtChairPerson.Text, Convert.ToInt32(Session["AppUserId"]), 2);
                }
                string theUrl = "frmAdmin_CustomItems.aspx";
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

        private void FillDropDownsRegion()
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

        }

        private void FillDropDownsDistrict(int RegionID)
        {
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet District = new DataSet();
            District = CustomManager.GetDistric(RegionID, Convert.ToInt32(Session["SystemId"]));
            BindManager.BindCombo(ddDistric, District.Tables[0], "Name", "ID");


        }
        private void FillDropDownsWard(int District)
        {
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet Ward = new DataSet();
            Ward = CustomManager.GetWard(District, Convert.ToInt32(Session["SystemId"]));
            BindManager.BindCombo(ddWard, Ward.Tables[0], "Name", "ID");

        }
        private void FillDropDownsVillage(int Ward)
        {
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet Village = new DataSet();
            Village = CustomManager.GetVillage(Ward, Convert.ToInt32(Session["SystemId"]));
            BindManager.BindCombo(ddlVillage, Village.Tables[0], "Name", "ID");


        }
        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            string theUrl = "frmAdmin_CustomItems.aspx";
            Response.Redirect(theUrl);
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedItem.Value != "0")
            {
                FillDropDownsDistrict(Convert.ToInt32(ddlRegion.SelectedItem.Value));
            }
        }
        protected void ddDistric_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddDistric.SelectedItem.Value != "0")
            {
                FillDropDownsWard(Convert.ToInt32(ddDistric.SelectedItem.Value));
            }
        }
        protected void ddWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddWard.SelectedItem.Value != "0")
            {
                FillDropDownsVillage(Convert.ToInt32(ddWard.SelectedItem.Value));
            }
        }
        protected void ddlVillage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            DataTable DTFindRecord = new DataTable();
            ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            if (ddlRegion.SelectedItem.Value != "0" && ddDistric.SelectedItem.Value != "0" && ddWard.SelectedItem.Value != "0" && ddlVillage.SelectedItem.Value != "0")
            {
                DTFindRecord = CustomManager.GetVillageChairperson(Convert.ToInt32(ddlRegion.SelectedItem.Value), Convert.ToInt32(ddDistric.SelectedItem.Value), Convert.ToInt32(ddWard.SelectedItem.Value), Convert.ToInt32(ddlVillage.SelectedItem.Value));
                if (DTFindRecord.Rows.Count > 0)
                {
                    txtChairPerson.Text = DTFindRecord.Rows[0][0].ToString();
                    btnSave.Text = "Update";
                }
                else
                {
                    txtChairPerson.Text = "";
                    btnSave.Text = "Save";
                }
            }
        }
    }

}