using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class TBRegimenGeneric : System.Web.UI.Page
    {
        DataSet theMasterDS = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = " >> TB Regimen";
            if (!IsPostBack)
            {
                GetMasters();
                if (theMasterDS.Tables.Count == 0) // 10Mar08
                {
                    RefreshCache();
                    return;
                }
                if (Request.QueryString["name"] == "Add")
                {
                    lblH2.Text = "Add TB Regimen";
                    ddStatus.SelectedValue = "0";
                }
                if (Request.QueryString["name"] != null)
                {
                    if (Request.QueryString["name"].ToString() == "Edit")
                    {
                        lblH2.Text = "Edit TB Regimen";
                        int intRegimenID = Convert.ToInt32(Request.QueryString["RegimenId"]);
                        Session["rid"] = intRegimenID;
                        BindControl(intRegimenID);
                    }
                }

                ViewState["DrugType"] = theMasterDS.Tables[0];
                ViewState["GenericMaster"] = theMasterDS.Tables[1];
                ViewState["StrengthMaster"] = theMasterDS.Tables[2];
                ViewState["FrequencyMaster"] = theMasterDS.Tables[3];
                ViewState["GenericMasterFD"] = theMasterDS.Tables[1];

            }
            if ((Session["SelectedData"] != null) && (Session["SelectedData"].ToString() != ""))
            {
                string theDrugType = "";
                if (Session["Type"] != null)
                    theDrugType = Session["Type"].ToString();
                //ViewState["MasterData"] = Session["MasterData"];
                DataTable theDT = (DataTable)Session["SelectedData"];


                if (theDrugType == "Generic")
                {
                    BindFunctions BindManager = new BindFunctions();
                    ViewState["SelGeneric"] = theDT;
                    ViewState["GenericMaster"] = (DataTable)Session["MasterData"];
                    BindManager.BindList(lstGeneric, (DataTable)ViewState["SelGeneric"], "Name", "Id");
                }
                Session.Remove("Type");
                Session.Remove("MasterData");
                Session.Remove("SelectedData");
            }
        }
        private DataTable MakeSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));
            theDT.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
            return theDT;
        }
        public void BindControl(int id)
        {
            IDrugMst DrugManager;
            BindFunctions BindManager = new BindFunctions();
            DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
            DataSet theDS = DrugManager.GetTBRegimenGeneric(id);
            if (theDS.Tables[0].Rows.Count > 0)
            {

                DataTable SelectedDataTable = MakeSelectedTable();
                foreach (DataRow r in theDS.Tables[0].Rows)
                {
                    DataRow theDR;
                    theDR = SelectedDataTable.NewRow();
                    theDR[0] = Convert.ToInt32(r["GenericID"].ToString());
                    if (r["GenericAbbrevation"].ToString() != "")
                    {
                        theDR[1] = r["GenericName"].ToString() + "-[" + r["GenericAbbrevation"] + "]";
                    }
                    else
                    {
                        theDR[1] = r["GenericName"].ToString();
                    }
                    SelectedDataTable.Rows.Add(theDR);

                }
                ViewState["SelGeneric"] = SelectedDataTable;

                DataTable theDT = theDS.Tables[0];
                txtRegimenName.Text = Convert.ToString(theDS.Tables[0].Rows[0]["Name"]);
                ddTrMonths.SelectedValue = Convert.ToString(theDS.Tables[0].Rows[0]["TreatmentTime"]);
                txtSeqNo.Text = Convert.ToString(theDS.Tables[0].Rows[0]["SRNo"]);
                if (theDS.Tables[0].Rows[0]["Status"].ToString() == "Active")
                {
                    ddStatus.SelectedValue = "0";
                }
                else if (theDS.Tables[0].Rows[0]["Status"].ToString() == "In-Active")
                {
                    ddStatus.SelectedValue = "1";
                }


                BindManager.BindList(lstGeneric, theDT, "GenericName", "GenericID");
                txtRegimenName.Enabled = false;

                lstGeneric.Enabled = false;
                ddTrMonths.Enabled = true;
                btnAddGeneric.Enabled = true;
                btnSave.Text = "Update";
            }
        }
        protected void RefreshCache()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //script += "var ans=true;\n";
            script += "alert('Please refresh cache');\n";
            //script += "if (ans==true)\n";
            //script += "{\n";
            script += "window.location.href='../frmFacilityHome.aspx?';\n";
            //script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);

        }
        protected void btnAddGeneric_Click(object sender, EventArgs e)
        {
            IQCareUtils theUtils = new IQCareUtils();

            ViewState["AddGenTrue"] = "True";
            ViewState["GetStrengthFromDatabase"] = "T";
            ViewState["GetFrequencyFromDatabase"] = "T";

            Session.Add("DrugData", ViewState["GenericMaster"]);
            Session.Add("SelectedData", ViewState["SelGeneric"]);

            ////////////////////////////////Application.Add("MasterData", ViewState["DrugDD"]);
            string theScript = "<script language='javascript' id='DrugPopup'>\n";
            theScript += "window.open('frmAdmin_DoseFrequencySelector.aspx?Type=Generic&DrugType=" + 31 + "&Page=TBRegimenGeneric', 'DrugGeneric','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DrugPopup", theScript);
          
            theScript = "";
            theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
            theScript += "show('" + divStatus.ClientID + "');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);


            
        }
        private void GetMasters()
        {
            IDrugMst DrugManager;
            try
            {
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\DrugMasters.con"));
                if (theDSXML.Tables["Mst_DrugType"] != null)//10Mar08 -- put conditios
                {

                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_DrugType"].Copy()); // table 0

                    DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    DataSet theDS = new DataSet();
                    theDS = (DataSet)DrugManager.GetAllDropDowns();//pr_Admin_GetDrugDropDowns_Constella //all GenID,GenName,GenAbbr,DrugTypeID,DelFlag

                    DataView theDV;
                    DataTable theDT;
                    // incase of add OR Active 
                    if ((Request.QueryString["Status"] == null) || (Request.QueryString["Status"].ToString() == "Active"))
                    {
                        theDV = new DataView(theDS.Tables[0]);
                        theDV.RowFilter = "DeleteFlag=0";
                        theDT = theDV.Table;
                        theMasterDS.Tables.Add(theDT.Copy());//get only active generics  // table 1
                    }
                    else if (Request.QueryString["Status"].ToString() == "InActive")
                        theMasterDS.Tables.Add(theDS.Tables[0].Copy()); // get list of all generics // table 1

                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_Strength"].Copy()); // table 2
                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_Frequency"].Copy()); // table 3
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);

            }
            finally
            {
                DrugManager = null;
            }
        }
        private Boolean FieldValidation()
        {

            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
            string strRegimenName = txtRegimenName.Text;

            string strMonths = ddTrMonths.SelectedItem.Value.ToString();
            string strStatus = ddStatus.SelectedItem.Value;
            int intSrNo = 0;



            if (txtSeqNo.Text != "")
            {
                intSrNo = Convert.ToInt32(txtSeqNo.Text);
            }
            string strGeneric = "";


            if (lstGeneric.Items.Count > 0)
            {
                foreach (ListItem lstItem in lstGeneric.Items)
                {
                    if (strGeneric == "")
                    {
                        strGeneric = lstItem.Value.ToString();
                    }
                    else
                    {
                        strGeneric = strGeneric + "," + lstItem.Value.ToString();
                    }
                }
            }
            if (btnSave.Text == "Save")
            {
                try
                {
                    if (strRegimenName != "")
                    {

                        int therows = DrugManager.SaveUpdateTBRegimenGeneric(strRegimenName, 0, Convert.ToInt32(strMonths), Convert.ToInt32(strStatus), strGeneric, Convert.ToInt32(Session["AppUserId"]), intSrNo, 1);
                        Response.Redirect("frmAdmin_TBRegimenGenericList.aspx");
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
            if (btnSave.Text == "Update")
            {
                if (FieldValidation() == false)
                {
                    return;
                }
                try
                {
                    if (strRegimenName != "")
                    {

                        int therows = DrugManager.SaveUpdateTBRegimenGeneric(strRegimenName, Convert.ToInt32(Session["rid"]), Convert.ToInt32(strMonths), Convert.ToInt32(strStatus), strGeneric, Convert.ToInt32(Session["AppUserId"]), intSrNo, 2);
                        Response.Redirect("frmAdmin_TBRegimenGenericList.aspx");
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
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_TBRegimenGenericList.aspx");
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_TBRegimenGenericList.aspx");
        }
    }
}