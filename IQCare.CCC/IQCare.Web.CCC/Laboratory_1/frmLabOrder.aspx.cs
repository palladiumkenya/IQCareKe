
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Laboratory;
using Interface.Security;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;



/////////////////////////////////////////////////////////////////////
// Code Written By   : Pankaj Kumar
// Written Date      : 13th Oct 2006
// Modified By       : Sanjay Rana
// Modification Date : 21st Dec 2006
// Description       : Lab Order Form
// Modified By       : Amitava Sinha
// Modification Date : 2nd Feb 2007
// Modified By       : Meetu Rahul
// Modification Date : 11 Nov 2008
/// /////////////////////////////////////////////////////////////////
namespace IQCare.Web.Laboratory
{
    public partial class frmLabOrder : BasePage
    {
        

        //Amitava Sinha
  

        private IIQCareSystem IQCareSecurity;
     
        //private ILabMst Labs;
        private int  OrderId;
        
      
       
        private DataTable theAddLabs;
        private DataSet theDSLabs;
        private DataTable theDTLabReported = new DataTable();
        private DataSet theExistDS = new DataSet();
     
        private DateTime theVisitDate, theCurrentDate;
     
        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl = "";
         

            if (Convert.ToInt32(ViewState["LabId"]) == 0)
            {
                theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"]);
            }
            else if (Convert.ToInt32(ViewState["LabId"]) > 0)
            {
                ViewState["LabId"] = null;
                theUrl = string.Format("../ClinicalForms/frmPatient_History.aspx");
            }
            if (Request.QueryString["opento"] == "ArtForm")
            {
                if (Convert.ToInt32(Session["ArtEncounterPatientVisitId"]) > 0)
                {
                    Session["PatientVisitId"] = Session["ArtEncounterPatientVisitId"];
                }
                string script = "<script language = 'javascript' defer ='defer' id = 'Showclose'>\n";
                script += "self.close();";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Showclose", script);
                return;
            }
            Response.Redirect(theUrl);
        }

        protected void btncomplete_Click(object sender, EventArgs e)
        {
            string strCustomField = string.Empty;
            ILabFunctions LabMaster;
            LabMaster = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            try
            {
                ViewState["SaveData"] = CreateStaticTable();

                DataTable LabResults = ReadLabTable(PnlLab);
                if (((DataTable)ViewState["SaveData"]).Rows.Count == 0 && (LabResults.Rows.Count == 0 || ViewState["AdditionalLabRes"].ToString() == "False"))
                {
                    IQCareMsgBox.Show("NoRecordSelected", this);
                    return;
                }

                IQCareUtils theUtils = new IQCareUtils();
                string dt2 = "";
                if (txtLabtobeDone.Text != "")
                {
                    dt2 = theUtils.MakeDate(txtLabtobeDone.Text.ToString());
                }
                else
                {
                    dt2 = theUtils.MakeDate("01-01-1900");
                }
                string dt = theUtils.MakeDate(txtLabOrderedbyDate.Text.ToString());
                string dt1 = theUtils.MakeDate(txtlabReportedbyDate.Text.ToString());

                Hashtable theHT = new Hashtable();
                if (Convert.ToInt32(ViewState["LabId"]) == 0)
                {
                    theHT = BuildOrderHashTable(Convert.ToInt32(Session["AppLocationId"]), ddLabOrderedbyName.SelectedValue.ToString(), System.Convert.ToDateTime(dt), ddlLabReportedbyName.SelectedValue.ToString(), Convert.ToDateTime(dt1), ddLabOrderedbyName.SelectedValue.ToString(), Convert.ToDateTime(dt), Convert.ToDateTime(dt2), Convert.ToInt32(Session["AppUserId"]), 1, 0, ddLabPeriodDone.SelectedValue.ToString(), txtLabnumber.Text);
                }
                else if (Convert.ToInt32(ViewState["LabId"]) > 0)
                {
                    theHT = BuildOrderHashTable(Convert.ToInt32(Session["AppLocationId"]), ddLabOrderedbyName.SelectedValue.ToString(), System.Convert.ToDateTime(dt), ddlLabReportedbyName.SelectedValue.ToString(), Convert.ToDateTime(dt1), ddLabOrderedbyName.SelectedValue.ToString(), Convert.ToDateTime(dt), Convert.ToDateTime(dt2), Convert.ToInt32(Session["AppUserId"]), 2, Convert.ToInt32(ViewState["LabID"]), ddLabPeriodDone.SelectedValue.ToString(), txtLabnumber.Text);
                } //Ajay End

                ////else
                ////{
                ////        theHT = BuildOrderHashTable(Convert.ToInt32(Session["AppLocationId"]), ddLabOrderedbyName.SelectedValue.ToString(), System.Convert.ToDateTime(dt), ddlLabReportedbyName.SelectedValue.ToString(), Convert.ToDateTime(dt1), ddLabOrderedbyName.SelectedValue.ToString(), Convert.ToDateTime(dt), Convert.ToDateTime(dt2), Convert.ToInt32(Session["AppUserId"]), 2, Convert.ToInt32(ViewState["OrderId"]), ddLabPeriodDone.SelectedValue.ToString());
                ////}

                DataTable theCustomDataDT = new DataTable();
                if (Request.QueryString["name"] == "Add" && Convert.ToInt32(ViewState["LabId"]) == 0)
                {
                    ///////////////
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.Laboratory, (DataSet)ViewState["CustomFieldsDS"]);
                    ///////////////
                }//Added by Ajay End
                else //if (Request.QueryString["name"] == "Edit" )
                {
                    ///////////////
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.Laboratory, (DataSet)ViewState["CustomFieldsDS"]);
                    ///////////////
                }
                /////////////////////
                DataTable dtLab = LabMaster.SaveNewLabOrder(theHT, LabResults, strCustomField, Session["Paperless"].ToString(), theCustomDataDT);
                if (dtLab != null && dtLab.Rows.Count > 0 && Convert.ToInt32(dtLab.Rows[0][0].ToString()) == 0)
                {
                    IQCareMsgBox.Show("LabDetailExists", this);
                    return;
                }
                else
                    ViewState["LabId"] = dtLab.Rows[0][0].ToString();

                string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
                script += "document.getElementById('" + btnok.ClientID + "').click();\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "aftersavefunction", script);
                return;
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
                LabMaster = null;
            }
        }

       
        protected void btnok_Click(object sender, EventArgs e)
        {
            string theUrl = "";
          
            if (Convert.ToString(ViewState["LabId"]) == "0")
            {
                theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"]);
            }
            else
            {
                theUrl = string.Format("../ClinicalForms/frmPatient_History.aspx");
            }
            if (Request.QueryString["opento"] == "ArtForm")
            {
                if (Convert.ToInt32(Session["ArtEncounterPatientVisitId"]) > 0)
                {
                    Session["PatientVisitId"] = Session["ArtEncounterPatientVisitId"];
                }

                string script = "<script language = 'javascript' defer ='defer' id = 'Showclose'>\n";
                script += "self.close();";
                script += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Showclose", script);
                return;
            }
            Response.Redirect(theUrl);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                DeleteForm();
            }
            else
            {
                if (FieldValidation() == false)
                {
                    return;
                }
                string msg = string.Empty; /// = ShowCaution(); Business rule changed,no requirement for caution msgs.
                if (msg.Length > 45)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Message"] = msg;
                    IQCareMsgBox.ShowConfirm("ShowCautionMsg", theBuilder, btncomplete);
                    string script = "<script language = 'javascript' defer ='defer' id = 'ShowCaution'>\n";
                    script += "document.getElementById('" + btncomplete.ClientID + "').click();\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowCaution", script);
                    return;
                }
                else
                {
                   
                    string script = "<script language = 'javascript' defer ='defer' id = 'ShowCaution'>\n";
                    script += "document.getElementById('" + btncomplete.ClientID + "').click();\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowCaution", script);

                    return;
                }

            }
        }

        //    DataSet theDSLabs = LabMaster.GetLabValues();
        //    DataView theDV = new DataView(theDSLabs.Tables[0]);
        protected void lnkLabTest_Click(object sender, EventArgs e)
        {
            ILabFunctions LabOrderMgr;
            LabOrderMgr = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet DSLabID = (DataSet)LabOrderMgr.GetPatientLabTestID(Convert.ToString(ViewState["LabId"]));
            Session["LabId"] = ViewState["LabId"];
            Session["LabOther"] = DSLabID.Tables[0];
            Session["LabTestID"] = DSLabID.Tables[2];
            Session["TestDatetxt"] = txtLabOrderedbyDate.Text;

            String theOrdScript;
            theOrdScript = "<script language='javascript' id='LabOrderPopup'>\n";
            // theOrdScript += "window.open('frmLabOrderTests.aspx?Mode=Edit&LabID=" + ViewState["LabId"] + "&sts="+ Convert.ToString(Request.QueryString["sts"])+"','LabSelection','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');\n";
            theOrdScript += "window.open('LabOrderForm.aspx','LabSelection','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');\n";
            theOrdScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "LabOrderPopup", theOrdScript);
        }

        protected void OtherLabs_Click(object sender, EventArgs e)
        {
            string theScript;
            Session.Add("MasterData", ViewState["LabMaster"]);
            Session.Add("SelectedData", ViewState["AddLab"]);
            theScript = "<script language='javascript' id='LabPopup'>\n";
            theScript += "window.open('frmLabSelector.aspx?Mode=" + Request.QueryString["name"] + "','LabSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "LabPopup", theScript);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["opento"] == "ArtForm")
                    {
                        Session["PatientVisitId"] = 0;
                    }
                }
                if (Session["Paperless"].ToString() == "0")
                {
                    lblreportedby.Attributes.Add("class", "required");
                    lblreportedbydate.Attributes.Add("class", "required");
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
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Lab Order";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Request.QueryString["sts"]);

            if (Session["LabId"] != null)
            {
                ViewState["LabId"] = Session["LabId"];
                Session["LabId"] = null;
            }

            DataTable theDTModule = (DataTable)Session["AppModule"];
            string ModuleId = "";
            foreach (DataRow theDR in theDTModule.Rows)
            {
                if (ModuleId == "")
                    ModuleId = theDR["ModuleId"].ToString();
                else
                    ModuleId = ModuleId + "," + theDR["ModuleId"].ToString();
            }

            #region "Authentication"

            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            //RTyagi..19Feb.07
            ViewState["LabId"] = Session["PatientVisitId"];
            if (Convert.ToInt32(ViewState["LabId"]) > 0)
            {
                //For PaperLess
                if (Session["SystemId"].ToString() == "1")
                {
                    if (Session["Paperless"].ToString() == "1")
                    {
                        //(Master.FindControl("lblformname") as Label).Text = "Lab Results";
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Lab Results";
                        //  EnableControlStatic();
                        //panelOrderedLabs.Visible = true;
                    }
                    else
                    {
                        //(Master.FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                        lnkLabTest.Visible = false;
                        // panelOrderedLabs.Visible = false;
                        //  EnableControlStatic();
                    }
                }
                else
                {
                    //(Master.FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                    lnkLabTest.Visible = false;
                    //EnableControlStatic();
                }

                this.EnableControlDynamic();
                if (Authentication.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?sts={1}", "../ClinicalForms/frmPatient_History.aspx", Convert.ToString(Request.QueryString["sts"]));
                    Response.Redirect(theUrl);
                    lnkLabTest.Enabled = false;
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                    lnkLabTest.Enabled = false;
                }

            }
            //else if (Request.QueryString["name"] == "Add")
            else if (Convert.ToInt32(ViewState["LabId"]) == 0)
            {
                //For Non Paperless
                EnableControlStatic();
                //(Master.FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                lnkLabTest.Visible = false;
                if (Authentication.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                //For PaperLess
                if (Session["SystemId"].ToString() == "1")
                {
                    if (Session["Paperless"].ToString() == "1")
                    {
                        //(Master.FindControl("lblformname") as Label).Text = "Lab Results";
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Lab Results";
                        EnableControlDynamic();
                    }
                    else
                    {
                        //(Master.FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                        lnkLabTest.Visible = false;
                        EnableControlStatic();
                    }
                }
                else
                {
                    //(Master.FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Order Form";
                    lnkLabTest.Visible = false;
                    EnableControlStatic();
                }

                if (Authentication.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?sts={1}", "../ClinicalForms/frmClinical_DeleteForm.aspx", Convert.ToString(Request.QueryString["sts"]));
                    Response.Redirect(theUrl);
                    lnkLabTest.Enabled = false;
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Text = "Delete";
                    btnsave.Enabled = false;
                    lnkLabTest.Enabled = false;
                    // btnQualityCheck.Visible = false;
                }
            }
            #endregion "Authentication"
            if (Request.QueryString["name"] == "Delete")
            {
                btncomplete.Visible = false;
                btnsave.Text = "Delete";
            }

            if (Page.IsPostBack != true)
            {
                Init_Form();
            }
            if (Session["CareEndFlag"].ToString() == "1")
            {
                btnsave.Enabled = true;
            }
            if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "1")
            {
                btnsave.Enabled = false;
            }
            TxtESR.Attributes.Add("onkeyup", "chkDecimal('" + TxtESR.ClientID + "')");
            TxtESR.Attributes.Add("onblur", "CheckValue('" + TxtESR.ClientID + "')");

            PutCustomControl();
            try
            {
                Int32 PID = Convert.ToInt32(Session["PatientId"]);
                if (!Page.IsPostBack)
                {
                    ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

                    DataSet theDSPatient = LabResultManager.GetPatientInfo(PID.ToString());
                    if (theDSPatient.Tables[1].Rows.Count > 0)
                    {
                        ViewState["IEVisitDate"] = theDSPatient.Tables[1].Rows[0]["VisitDate"];
                        if (theDSPatient.Tables[2].Rows.Count > 0)
                        {
                            ViewState["VisitDate"] = theDSPatient.Tables[2].Rows[0]["StartDate"];
                        }
                    }
                    ViewState["LabRanges"] = theDSLabs;
                    ViewState["LabMaster"] = theDSLabs.Tables[2];

                    ViewState["AddLab"] = theAddLabs;

                    ViewState["OrderId"] = OrderId;

                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["FormName"] = "LabOrder";
                    if (OrderId == 0)
                    {
                        IQCareMsgBox.ShowConfirm("AddLabRecord", theBuilder, btnok);
                    }
                    else if (Request.QueryString["name"] == "Delete" && OrderId > 0)
                    {
                        MsgBuilder theBuilder1 = new MsgBuilder();
                        theBuilder1.DataElements["FormName"] = "Lab Order";
                        IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder1, btnsave);
                    }
                    else if (OrderId > 0)
                    {
                        IQCareMsgBox.ShowConfirm("UpdateLabRecord", theBuilder, btnok);
                    }


                    //AddFieldAttributes();
                    //Ajay Sh on 6Jul09--for custom field values populated at update mode
                    FillOldCustomData(PID);
                }
                else
                {
                    if (Session["Paperless"].ToString() == "0")
                    {
                        if ((DataSet)Session["AddLab"] != null)
                        {
                            ViewState["LabMaster"] = ((DataSet)Session["AddLab"]).Tables[0];
                            ViewState["AddLab"] = ((DataSet)Session["AddLab"]).Tables[1];
                            Session.Remove("AddLab");
                        }
                        if ((DataTable)ViewState["AddLab"] != null)
                        {
                            LoadAdditionalLabs((DataTable)ViewState["AddLab"], PnlLab);
                        }
                    }
                    else if ((DataTable)ViewState["AddLab"] != null)
                    {
                        LoadAdditionalLabs((DataTable)ViewState["AddLab"], PnlLab);
                    }
                }
               

                if (ChkViral.Checked == true)
                {
                    VLdetectable();
                }
                FillUnits();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        protected void Page_PreRender(Object sender, EventArgs e)
        {
           
        }

        protected void preclinic_changed(object sender, EventArgs e)
        {
            if (this.txtLabtobeDone.Text != "")
            {
                this.preclinicLabs.Checked = true;
            }
        }

        protected void unitsCreatinine_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet theDS = (DataSet)ViewState["LabRanges"];
            DataView theDV = new DataView(theDS.Tables[0]);
            //txtCreatinine.Text = "";
            if (this.unitsCreatinine.SelectedIndex == 1)
            {
                theDV.RowFilter = "SubTestId = 12";
                txtCreatinine.Attributes.Add("onkeyup", "chkDecimal('" + txtCreatinine.ClientID + "'); AddBoundary('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtCreatinine.Attributes.Add("onblur", "CheckValue('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "')");
                txtCreatinine.Focus();
            }
            if (this.unitsCreatinine.SelectedIndex == 2)
            {
                theDV.RowFilter = "SubTestId = 106";
                txtCreatinine.Attributes.Add("onkeyup", "chkDecimal('" + txtCreatinine.ClientID + "'); AddBoundary('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtCreatinine.Attributes.Add("onblur", "CheckValue('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "')");
                txtCreatinine.Focus();
            }
        }

        private void AddFieldAttributes(Hashtable HTUnit)
        {
          

            txtLabtobeDone.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLabtobeDone.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            txtLabOrderedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLabOrderedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            txtlabReportedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlabReportedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            //for boundary values table is theDV[5]

            DataSet theDS = (DataSet)ViewState["LabRanges"];
            DataView theDV = new DataView(theDS.Tables[4]);

            theDV.RowFilter = "SubTestId = 1 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtCD4.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4.ClientID + "'); AddBoundary('" + txtCD4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtCD4.Attributes.Add("onblur", "CheckValue('" + txtCD4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtCD4.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

            theDV.RowFilter = "SubTestId = 2 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtCD4perc.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4perc.ClientID + "'); AddBoundary('" + txtCD4perc.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtCD4perc.Attributes.Add("onblur", "CheckValue('" + txtCD4perc.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtCD4perc.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
    

            //theDV.RowFilter = "SubTestName = 'ViralLoad' and DefaultUnit=1";
            theDV.RowFilter = "SubTestId = 3 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtViralLoad.Attributes.Add("onblur", "chkDecimal('" + txtViralLoad.ClientID + "');AddBoundary('" + txtViralLoad.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtViralLoad.Attributes.Add("onblur", "CheckValue('" + txtViralLoad.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtViralLoad.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 107 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtViralLoadDet.Attributes.Add("onkeyup", "chkDecimal('" + txtViralLoadDet.ClientID + "'); AddBoundary('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtViralLoadDet.Attributes.Add("onblur", "CheckValue('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            theDV.RowFilter = "SubTestId = 5 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtHCT.Attributes.Add("onkeyup", "chkDecimal('" + txtHCT.ClientID + "'); AddBoundary('" + txtHCT.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtHCT.Attributes.Add("onblur", "CheckValue('" + txtHCT.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtHCT.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            
            theDV.RowFilter = "SubTestId = 6 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtHB.Attributes.Add("onkeyup", "chkDecimal('" + txtHB.ClientID + "'); AddBoundary('" + txtHB.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtHB.Attributes.Add("onblur", "CheckValue('" + txtHB.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtHB.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
        
            theDV.RowFilter = "SubTestId = 7 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtWBC.Attributes.Add("onkeyup", "chkDecimal('" + txtWBC.ClientID + "'); AddBoundary('" + txtWBC.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtWBC.Attributes.Add("onblur", "CheckValue('" + txtWBC.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtWBC.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 57 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                TxtESR.Attributes.Add("onkeyup", "chkDecimal('" + TxtESR.ClientID + "'); AddBoundary('" + TxtESR.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                TxtESR.Attributes.Add("onblur", "CheckValue('" + TxtESR.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + TxtESR.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

            theDV.RowFilter = "SubTestId = 9 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtPlatelets.Attributes.Add("onkeyup", "chkDecimal('" + txtPlatelets.ClientID + "'); AddBoundary('" + txtPlatelets.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtPlatelets.Attributes.Add("onblur", "CheckValue('" + txtPlatelets.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtPlatelets.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            theDV.RowFilter = "SubTestId = 10 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtAST.Attributes.Add("onkeyup", "chkDecimal('" + txtAST.ClientID + "'); AddBoundary('" + txtAST.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtAST.Attributes.Add("onblur", "CheckValue('" + txtAST.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtAST.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           theDV.RowFilter = "SubTestId = 12 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtCreatinine.Attributes.Add("onkeyup", "chkDecimal('" + txtCreatinine.ClientID + "'); AddBoundary('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtCreatinine.Attributes.Add("onblur", "CheckValue('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtCreatinine.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            //theDV.RowFilter = "SubTestName = 'Amylase' and DefaultUnit=1";
            theDV.RowFilter = "SubTestId = 13 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtAmylase.Attributes.Add("onkeyup", "chkDecimal('" + txtAmylase.ClientID + "'); AddBoundary('" + txtAmylase.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtAmylase.Attributes.Add("onblur", "CheckValue('" + txtAmylase.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtAmylase.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            
            theDV.RowFilter = "SubTestId = 90 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis1.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis1.ClientID + "'); AddBoundary('" + txtUrinalysis1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis1.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 91 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis2.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis2.ClientID + "'); AddBoundary('" + txtUrinalysis2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis2.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 92 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis3.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis3.ClientID + "'); AddBoundary('" + txtUrinalysis3.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis3.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis3.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis3.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
         
            theDV.RowFilter = "SubTestId = 93 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis4.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis4.ClientID + "'); AddBoundary('" + txtUrinalysis4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis4.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis4.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 94 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis5.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis5.ClientID + "'); AddBoundary('" + txtUrinalysis5.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis5.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis5.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis5.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 95 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis6.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis6.ClientID + "'); AddBoundary('" + txtUrinalysis6.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis6.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis6.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis6.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            
            theDV.RowFilter = "SubTestId = 96 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtUrinalysis7.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis7.ClientID + "'); AddBoundary('" + txtUrinalysis7.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtUrinalysis7.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis7.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis7.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
          
            theDV.RowFilter = "SubTestId = 82 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                neut1.Attributes.Add("onkeyup", "chkDecimal('" + neut1.ClientID + "'); AddBoundary('" + neut1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                neut1.Attributes.Add("onblur", "CheckValue('" + neut1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + neut1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

            theDV.RowFilter = "SubTestId = 83 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                neut2.Attributes.Add("onkeyup", "chkDecimal('" + neut2.ClientID + "'); AddBoundary('" + neut2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                neut2.Attributes.Add("onblur", "CheckValue('" + neut2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + neut2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 84 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                lymph1.Attributes.Add("onkeyup", "chkDecimal('" + lymph1.ClientID + "'); AddBoundary('" + lymph1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                lymph1.Attributes.Add("onblur", "CheckValue('" + lymph1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + lymph1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
          
            theDV.RowFilter = "SubTestId = 85 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                lymph2.Attributes.Add("onkeyup", "chkDecimal('" + lymph2.ClientID + "'); AddBoundary('" + lymph2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                lymph2.Attributes.Add("onblur", "CheckValue('" + lymph2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + lymph2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 86 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                mono1.Attributes.Add("onkeyup", "chkDecimal('" + mono1.ClientID + "'); AddBoundary('" + mono1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                mono1.Attributes.Add("onblur", "CheckValue('" + mono1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + mono1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

           
            theDV.RowFilter = "SubTestId = 87 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                mono2.Attributes.Add("onkeyup", "chkDecimal('" + mono2.ClientID + "'); AddBoundary('" + mono2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                mono2.Attributes.Add("onblur", "CheckValue('" + mono2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + mono2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 88 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                eosin1.Attributes.Add("onkeyup", "chkDecimal('" + eosin1.ClientID + "'); AddBoundary('" + eosin1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                eosin1.Attributes.Add("onblur", "CheckValue('" + eosin1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + eosin1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

            theDV.RowFilter = "SubTestId = 89 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                eosin2.Attributes.Add("onkeyup", "chkDecimal('" + eosin2.ClientID + "'); AddBoundary('" + eosin2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                eosin2.Attributes.Add("onblur", "CheckValue('" + eosin2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + eosin2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
          
            theDV.RowFilter = "SubTestId = 28 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtRBCs.Attributes.Add("onkeyup", "chkNumeric('" + txtRBCs.ClientID + "'); AddBoundary('" + txtRBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtRBCs.Attributes.Add("onblur", "CheckValue('" + txtRBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtRBCs.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

            theDV.RowFilter = "SubTestId = 29 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtWBCs.Attributes.Add("onkeyup", "chkNumeric('" + txtWBCs.ClientID + "'); AddBoundary('" + txtWBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtWBCs.Attributes.Add("onblur", "CheckValue('" + txtWBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtWBCs.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }

         
            theDV.RowFilter = "SubTestId = 30 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtNeutrophils.Attributes.Add("onkeyup", "chkNumeric('" + txtNeutrophils.ClientID + "'); AddBoundary('" + txtNeutrophils.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtNeutrophils.Attributes.Add("onblur", "CheckValue('" + txtNeutrophils.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtNeutrophils.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 31 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtLymphocytes.Attributes.Add("onkeyup", "chkNumeric('" + txtLymphocytes.ClientID + "'); AddBoundary('" + txtLymphocytes.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtLymphocytes.Attributes.Add("onblur", "CheckValue('" + txtLymphocytes.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtLymphocytes.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
            
            theDV.RowFilter = "SubTestId = 54 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtGlucose.Attributes.Add("onkeyup", "chkDecimal('" + txtGlucose.ClientID + "');  AddBoundary('" + txtGlucose.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtGlucose.Attributes.Add("onblur", "CheckValue('" + txtGlucose.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtGlucose.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
           
            theDV.RowFilter = "SubTestId = 32 and DefaultUnit=1";
            if (theDV.Count != 0)
            {
                txtProtein.Attributes.Add("onkeyup", "chkDecimal('" + txtProtein.ClientID + "');  AddBoundary('" + txtProtein.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                txtProtein.Attributes.Add("onblur", "CheckValue('" + txtProtein.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtProtein.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
        }

        private void BindCustomControls(DataRow theDR)
        {
            Panel thePnl = new Panel();
            thePnl.ID = "pnl" + theDR["SubTestId"].ToString();
            thePnl.Height = 20;
            thePnl.Width = 850;
            thePnl.Controls.Clear();

            /////// Space//////
            Label theSpace = new Label();
            theSpace.ID = "theSpace" + theDR["SubTestId"].ToString();
            theSpace.Width = 5;
            theSpace.Text = "";
            thePnl.Controls.Add(theSpace);

            ////////////////////
            Label theTestName = new Label();
            theTestName.ID = "theName" + theDR["SubTestId"].ToString();
            theTestName.Width = 400; //140;
            theTestName.Text = theDR["SubTestName"].ToString();
            thePnl.Controls.Add(theTestName);

            Label theSpace2 = new Label();
            theSpace2.ID = "theSpace2" + theDR["SubTestId"].ToString();
            theSpace2.Width = 20;
            theSpace2.Text = "";
            thePnl.Controls.Add(theSpace2);

            if (ViewState["LabRanges"] == null)
                ViewState["LabRanges"] = theDSLabs;

            DataSet theDSselectList = (DataSet)ViewState["LabRanges"];
            DataView theDVselectList = new DataView(theDSselectList.Tables[1]);
            theDVselectList.RowFilter = "SubTestId = " + theDR["SubTestId"].ToString();
            if (theDVselectList.Count != 0)
            {
                DropDownList theddlLabResult = new DropDownList();
                theddlLabResult.ID = "ddlLabResult" + theDR["SubTestId"].ToString();
                theddlLabResult.Width = 120;
                for (int i = 0; i < theDVselectList.Count; i++)
                {
                    theddlLabResult.Items.Add(theDVselectList[i].Row["Result"].ToString());
                }
                theddlLabResult.Items.Insert(0, "Select");
                thePnl.Controls.Add(theddlLabResult);
            }
            else
            {
                TextBox theLabResult = new TextBox();
                theLabResult.ID = "LabResult" + theDR["SubTestId"].ToString();
                theLabResult.Width = 120;
                thePnl.Controls.Add(theLabResult);

                if (ViewState["LabRanges"] == null)
                    ViewState["LabRanges"] = theDSLabs;

                DataSet theDS = (DataSet)ViewState["LabRanges"];
                DataView theDV = new DataView(theDS.Tables[4]);
                theDV.RowFilter = "SubTestName = '" + theDR["SubTestName"].ToString() + "'";
                if (theDV.Count != 0)
                {
                    theLabResult.Attributes.Add("onkeyup", "chkDecimal('ctl00$clinicalheaderfooter$" + theLabResult.ClientID + "'); AddBoundary('ctl00$clinicalheaderfooter$" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    theLabResult.Attributes.Add("onblur", "CheckValue('ctl00$clinicalheaderfooter$" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('ctl00$clinicalheaderfooter$" + theLabResult.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            ////////////Space////////////////////////
            Label theSpace3 = new Label();
            theSpace3.ID = "theSpace3" + theDR["SubTestId"].ToString();
            theSpace3.Width = 20;
            theSpace3.Text = "";
            thePnl.Controls.Add(theSpace3);

            //////////////////////////////////////

            CheckBox theFinChk = new CheckBox();
            theFinChk.ID = "FinChk" + theDR["SubTestId"].ToString();
            theFinChk.Text = "";
            theFinChk.Checked = false;
            thePnl.Controls.Add(theFinChk);

            Label theTestId = new Label();
            theTestId.ID = "lblTestId" + theDR["LabTestId"].ToString();
            theTestId.Text = "";
            thePnl.Controls.Add(theTestId);

            PnlLab.Controls.Add(thePnl);
        }

        private void BindDropdownOrderBy(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Employee"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);

                theDV.RowFilter = "DeleteFlag=0";

                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddLabOrderedbyName, theDT, "EmployeeName", "EmployeeId");
                }
            }
        }

        private void BindDropdownReportedBy(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Employee"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }

                    BindManager.BindCombo(ddlLabReportedbyName, theDT, "EmployeeName", "EmployeeId");
                }
            }
        }

        private Hashtable BuildOrderHashTable(int LocationID, string OrderedByName, DateTime OrderedByDate, string ReportedByName, DateTime ReportedByDate, string CheckedByName, DateTime CheckedByDate, DateTime PreClinicLabDate, Int32 UserId,
                                              int TransactionId, int OrderId, string LabPeriod, string Labnumber)
        {
            //This procedure creates the hashtable for the ord_laborder

            Hashtable htParameters = new Hashtable();
            htParameters.Clear();
            
            int LabID = Convert.ToInt32(ViewState["LabId"]);

            htParameters.Add("LabID", LabID.ToString());

            htParameters.Add("PatientID", PatientId.ToString());
            htParameters.Add("LocationID", LocationID.ToString());
            htParameters.Add("OrderedByName", Convert.ToInt32(OrderedByName.ToString()));
            htParameters.Add("OrderedByDate", OrderedByDate.ToString());
            htParameters.Add("ReportedByName", Convert.ToInt32(ReportedByName.ToString()));
            htParameters.Add("ReportedByDate", ReportedByDate.ToString());
            htParameters.Add("CheckedByName", Convert.ToInt32(CheckedByName.ToString()));
            htParameters.Add("CheckedByDate", CheckedByDate.ToString());
            htParameters.Add("PreClinicLabDate", PreClinicLabDate.ToString());
            htParameters.Add("UserId", UserId.ToString());
            htParameters.Add("Transaction", TransactionId.ToString());
            htParameters.Add("OrderId", OrderId.ToString());
            htParameters.Add("LabPeriod", LabPeriod.ToString());
            htParameters.Add("LabNumber", Labnumber.ToString());

            return htParameters;
        }

        private DataTable CreateStaticTable()
        {
            DataTable dtLabs = new DataTable();
            dtLabs.Columns.Add("LabTestId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabParameterId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabResult", System.Type.GetType("System.Decimal"));
            dtLabs.Columns.Add("LabResult1", System.Type.GetType("System.String"));
            dtLabs.Columns.Add("LabResultId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("Financed", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("UnitId", System.Type.GetType("System.Int32"));

            dtLabs.Columns["Financed"].DefaultValue = 0;

            DataRow drLab;

            Hashtable HTUnitId = new Hashtable();

            HTUnitId = (Hashtable)ViewState["HTUnitId"];

            #region "Building Table"
            //CSF Culture
            if (csfCulture.Text.ToString() != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 26;
                drLab["LabParameterID"] = 26;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888; // Rupesh 19Dec07
                drLab["LabResult1"] = csfCulture.Text;
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            stooldesc.Disabled = false;
            //Stool Desc
            if (stooldesc.Value.ToString() != "" && (rdoStoolList.Checked == true || rdoStoolList2.Checked == true))
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 105;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888; // Rupesh 19Dec07 - its done 1 or 0 below
                drLab["LabResult1"] = stooldesc.Value;
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //X-Ray
            if (rdochestxray1.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 88;
                drLab["LabParameterID"] = 113;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            if (rdochestxray2.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 88;
                drLab["LabParameterID"] = 113;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            if (xraytxt.Value != "" & (rdochestxray1.Checked == true || rdochestxray2.Checked == true))
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 88;
                drLab["LabParameterID"] = 104;
                drLab["LabResult"] = 99998888;
                drLab["LabResult1"] = xraytxt.Value;
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Culture Sensitivity
            if (txtcultureSensitivity.Value.ToString() != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 22;
                drLab["LabParameterID"] = 22;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888; // Rupesh
                drLab["LabResult1"] = txtcultureSensitivity.Value;
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //CD4 Lab
            if (txtCD4.Text.ToString() != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 1;
                drLab["LabParameterID"] = 1;
                drLab["LabResult"] = Convert.ToDecimal(txtCD4.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["CD4"] == null) ? 0 : HTUnitId["CD4"];
                dtLabs.Rows.Add(drLab);
            }

            //CD4% Lab
            if (txtCD4perc.Text.ToString() != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 1;
                drLab["LabParameterID"] = 2;
                drLab["LabResult"] = Convert.ToDecimal(txtCD4perc.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["CD4Percent"] == null) ? 0 : HTUnitId["CD4Percent"];
                dtLabs.Rows.Add(drLab);
            }

            //GramStain
            if (txtgramStain.Value != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 20;
                drLab["LabParameterID"] = 20;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888;
                drLab["LabResult1"] = txtgramStain.Value;
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Viral Load
            if (txtViralLoad.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 3;
                drLab["LabParameterID"] = 3;
                drLab["LabResult"] = Convert.ToDecimal(txtViralLoad.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["ViralLoad"] == null) ? 0 : HTUnitId["ViralLoad"];
                dtLabs.Rows.Add(drLab);
            }

            //Viral Load Undetectable
            if (txtViralLoadDet.Text != "" && ChkViral.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 3;
                drLab["LabParameterID"] = 107;
                drLab["LabResult"] = Convert.ToDecimal(txtViralLoadDet.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] = 0; // Rupesh : 01Feb08 : viralload undetectable was not accepting proper values
                drLab["UnitId"] = (HTUnitId["ViralLoadDet"] == null) ? 0 : HTUnitId["ViralLoadDet"]; ;
                dtLabs.Rows.Add(drLab);
            }

            //Store Plasma
            if (storePlasma.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 4;
                drLab["LabParameterID"] = 4;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //HB
            if (txtHB.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 6;
                drLab["LabParameterID"] = 6;
                drLab["LabResult"] = Convert.ToDecimal(txtHB.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["Hct/Hb"] == null) ? 0 : HTUnitId["Hct/Hb"];
                dtLabs.Rows.Add(drLab);
            }

            //HCT
            if (txtHCT.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 5;
                drLab["LabParameterID"] = 5;
                drLab["LabResult"] = Convert.ToDecimal(txtHCT.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["HCTPercent"] == null) ? 0 : HTUnitId["HCTPercent"];
                dtLabs.Rows.Add(drLab);
            }

            //WBC
            if (txtWBC.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 7;
                drLab["LabParameterID"] = 7;
                drLab["LabResult"] = Convert.ToDecimal(txtWBC.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["WBC"] == null) ? 0 : HTUnitId["WBC"];
                dtLabs.Rows.Add(drLab);
            }

            //Platelets
            if (txtPlatelets.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 9;
                drLab["LabParameterID"] = 9;
                drLab["LabResult"] = Convert.ToDecimal(txtPlatelets.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["Platelets"] == null) ? 0 : HTUnitId["Platelets"];
                dtLabs.Rows.Add(drLab);
            }
            //ESR
            if (TxtESR.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 28;
                drLab["LabParameterID"] = 57;
                drLab["LabResult"] = Convert.ToDecimal(TxtESR.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["ESR"] == null) ? 0 : HTUnitId["ESR"];
                dtLabs.Rows.Add(drLab);
            }

            //AST
            if (txtAST.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 10;
                drLab["LabParameterID"] = 10;
                drLab["LabResult"] = Convert.ToDecimal(txtAST.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["AST"] == null) ? 0 : HTUnitId["AST"];
                dtLabs.Rows.Add(drLab);
            }

            //ALT
            if (txtALT.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 11;
                drLab["LabParameterID"] = 11;
                drLab["LabResult"] = Convert.ToDecimal(txtALT.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["ALT"] == null) ? 0 : HTUnitId["ALT"];
                dtLabs.Rows.Add(drLab);
            }

            //Creatinine
            if (txtCreatinine.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 12;
                drLab["LabParameterID"] = 12; //Convert.ToInt32(unitsCreatinine.SelectedValue);
                drLab["LabResult"] = Convert.ToDecimal(txtCreatinine.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["Creatininemg"] == null) ? 0 : HTUnitId["Creatininemg"];
                dtLabs.Rows.Add(drLab);
            }

            //Amylase
            if (txtAmylase.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 13;
                drLab["LabParameterID"] = 13;
                drLab["LabResult"] = Convert.ToDecimal(txtAmylase.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["Amylase"] == null) ? 0 : HTUnitId["Amylase"];
                dtLabs.Rows.Add(drLab);
            }

            //Pregnancy
            if (rdoclPreg.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 14;
                drLab["LabParameterID"] = 14;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Pregnancy
            if (rdoclPreg2.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 14;
                drLab["LabParameterID"] = 14;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Malaria
            if (rdochkMalaria.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 15;
                drLab["LabParameterID"] = 15;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Malaria
            if (rdochkMalaria2.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 15;
                drLab["LabParameterID"] = 15;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Serum
            if (rdorbSerum1.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 16;
                drLab["LabParameterID"] = 16;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Serum
            if (rdorbSerum2.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 16;
                drLab["LabParameterID"] = 16;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //CSF Crypto
            if (rdocblSerumCrypto1.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 23;
                drLab["LabParameterID"] = 23;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //CSF Crypto
            if (rdocblSerumCrypto2.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 23;
                drLab["LabParameterID"] = 23;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Add CSF India
            if (rdocsfIndiaInk1.Checked)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 24;
                drLab["LabParameterID"] = 24;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Add CSF India
            if (rdocsfIndiaInk2.Checked)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 24;
                drLab["LabParameterID"] = 24;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Add CSF Gram Stain
            if (rdocsfgramstain1.Checked)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 25;
                drLab["LabParameterID"] = 25;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Add CSF Gram Stain
            if (rdocsfgramstain2.Checked)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 25;
                drLab["LabParameterID"] = 25;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Business Logic for Glucose yet to be defined/////
            //if (txtGlucose.Text != "" && unitsGlucose.SelectedIndex!=0)
            if (txtGlucose.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 54; //27;
                drLab["LabParameterID"] = 54; //Convert.ToInt32(unitsGlucose.SelectedValue);
                drLab["LabResult"] = Convert.ToDecimal(txtGlucose.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["Glucose"] == null) ? 0 : HTUnitId["Glucose"];
                dtLabs.Rows.Add(drLab);
            }

            //Add Protein   ----- Business Logic yet to be defined
            //if (txtProtein.Text != "" && unitsProtein.SelectedIndex!=0)
            if (txtProtein.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 32;
                drLab["LabParameterID"] = 32; //Convert.ToInt32(unitsProtein.SelectedValue);
                drLab["LabResult"] = Convert.ToDecimal(txtProtein.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["Protein"] == null) ? 0 : HTUnitId["Protein"];
                dtLabs.Rows.Add(drLab);
            }

            //Add StoolList
            if (rdoStoolList.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 55;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Add StoolList
            if (rdoStoolList2.Checked == true)
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 55;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //Add RBCs
            if (txtRBCs.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 56;
                drLab["LabParameterID"] = 28;
                drLab["LabResult"] = Convert.ToDecimal(txtRBCs.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["CCRbcs"] == null) ? 0 : HTUnitId["CCRbcs"];
                dtLabs.Rows.Add(drLab);
            }

            //Add WBCs
            if (txtWBCs.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 57;
                drLab["LabParameterID"] = 29;
                drLab["LabResult"] = Convert.ToDecimal(txtWBCs.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["CCWBCs"] == null) ? 0 : HTUnitId["CCWBCs"];
                dtLabs.Rows.Add(drLab);
            }

            //Add Neutrophils
            if (txtNeutrophils.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 30;
                drLab["LabParameterID"] = 30;
                drLab["LabResult"] = Convert.ToDecimal(txtNeutrophils.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["CCNeutrophilis"] == null) ? 0 : HTUnitId["CCNeutrophilis"];
                dtLabs.Rows.Add(drLab);
            }

            //Add Lymphocytes
            if (txtLymphocytes.Text != "")
            {
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 31;
                drLab["LabParameterID"] = 31;
                drLab["LabResult"] = Convert.ToDecimal(txtLymphocytes.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["CCLymphocytes"] == null) ? 0 : HTUnitId["CCLymphocytes"];
                dtLabs.Rows.Add(drLab);
            }

            if (sputumafb1.Checked)
            {
                //Add AFB1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 17;
                drLab["LabParameterID"] = 17;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            if (sputumafb12.Checked)
            {
                //Add AFB1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 17;
                drLab["LabParameterID"] = 17;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (sputumafb22.Checked)
            {
                //Add AFB2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 18;
                drLab["LabParameterID"] = 18;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            if (sputumafb21.Checked)
            {
                //Add AFB2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 18;
                drLab["LabParameterID"] = 18;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (sputumafb3.Checked)
            {
                //Add AFB3
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 19;
                drLab["LabParameterID"] = 19;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (sputumafb31.Checked)
            {
                //Add AFB3
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 19;
                drLab["LabParameterID"] = 19;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (cblSerologyPos.Checked == true)//(HIV_Rapid_Test.SelectedValue=="Pos")//(cblSerologyPos.Checked == true)
            {
                //Add Serology
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 53;
                drLab["LabParameterID"] = 53;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            else if (cblSerologyNeg.Checked == true)//(HIV_Rapid_Test.SelectedValue=="Neg")//
            {
                //Add Serology
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 53;
                drLab["LabParameterID"] = 53;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //--- Rupesh 08May ---
            if (cblPCRResultsPos.Checked == true)
            {
                //Add PCR Results
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 94;
                drLab["LabParameterID"] = 114;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            else if (cblPCRResultsNeg.Checked == true)
            {
                //Add PCR Results
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 94;
                drLab["LabParameterID"] = 114;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            //------------------------
            //----Meetu 16 Sep 2009  Begin---

            if (cblFTAPos.Checked == true)
            {
                //Add FTA Pos Results

                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 74;
                drLab["LabParameterID"] = 74;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (cblFTANeg.Checked == true)
            {
                //Add FTA Neg Results

                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 74;
                drLab["LabParameterID"] = 74;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (cblRPRVDRLPos.Checked == true)
            {
                //Add RPR/VDRL Pos Results

                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 75;
                drLab["LabParameterID"] = 75;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (cblRPRVDRLNeg.Checked == true)
            {
                //Add RPR/VDRL Neg Results

                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 75;
                drLab["LabParameterID"] = 75;
                drLab["LabResult"] = 0;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (HIVSerologyConfirm.Checked == true)
            {
                //Add Serology confirm
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 85;
                drLab["LabParameterID"] = 101;
                drLab["LabResult"] = 1;
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            //----Meetu End -----//

            //else if (HIVSerologyConfirm.Checked == false)
            //{
            //    //Add Serology confirm
            //    drLab = dtLabs.NewRow();
            //    drLab["LabTestID"] = 85;
            //    drLab["LabParameterID"] = 101;
            //    drLab["LabResult"] = 0;
            //    drLab["LabResult1"] = "";
            //    drLab["LabResultId"] = 0;
            //    drLab["Financed"] = 1;
            //    dtLabs.Rows.Add(drLab);
            //}

            if (neut1.Text != "")
            {
                //Add neut1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 82;
                drLab["LabResult"] = Convert.ToDecimal(neut1.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffNeut"] == null) ? 0 : HTUnitId["DiffNeut"];
                dtLabs.Rows.Add(drLab);
            }

            if (neut2.Text != "")
            {
                //Add neut2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 83;
                drLab["LabResult"] = Convert.ToDecimal(neut2.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffNeutperc"] == null) ? 0 : HTUnitId["DiffNeutperc"];
                dtLabs.Rows.Add(drLab);
            }

            if (lymph1.Text != "")
            {
                //Add lymph1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 84;
                drLab["LabResult"] = Convert.ToDecimal(lymph1.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffLymph"] == null) ? 0 : HTUnitId["DiffLymph"];
                dtLabs.Rows.Add(drLab);
            }
            if (lymph2.Text != "")
            {
                //Add lymph2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 85;
                drLab["LabResult"] = Convert.ToDecimal(lymph2.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffLymphperc"] == null) ? 0 : HTUnitId["DiffLymphperc"];
                dtLabs.Rows.Add(drLab);
            }

            if (mono1.Text != "")
            {
                //Add mono1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 86;
                drLab["LabResult"] = Convert.ToDecimal(mono1.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffMono"] == null) ? 0 : HTUnitId["DiffMono"];
                dtLabs.Rows.Add(drLab);
            }

            if (mono2.Text != "")
            {
                //Add mono2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 87;
                drLab["LabResult"] = Convert.ToDecimal(mono2.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffMonoperc"] == null) ? 0 : HTUnitId["DiffMonoperc"];
                dtLabs.Rows.Add(drLab);
            }

            if (eosin1.Text != "")
            {
                //Add eosin1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 88;
                drLab["LabResult"] = Convert.ToDecimal(eosin1.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffEosin"] == null) ? 0 : HTUnitId["DiffEosin"];
                dtLabs.Rows.Add(drLab);
            }

            if (eosin2.Text != "")
            {
                //Add eosin2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 8;
                drLab["LabParameterID"] = 89;
                drLab["LabResult"] = Convert.ToDecimal(eosin2.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = (HTUnitId["DiffEosinperc"] == null) ? 0 : HTUnitId["DiffEosinperc"];
                dtLabs.Rows.Add(drLab);
            }

            if (txtUrinalysis1.Text != "")
            {
                //Add Urinalysis1
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 90;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis1.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] = 0;
                drLab["UnitId"] = HTUnitId["UrinalysisSpecGrav"]; // Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }

            if (txtUrinalysis2.Text != "")
            {
                //Add Urinalysis2
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 91;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis2.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] =0;
                drLab["UnitId"] = HTUnitId["UrinalysisGlucose"]; // Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }

            if (txtUrinalysis3.Text != "")
            {
                //Add Urinalysis3
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 92;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis3.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] = 0;
                drLab["UnitId"] = HTUnitId["UrinalysisKetone"];// Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }
            if (txtUrinalysis4.Text != "")
            {
                //Add Urinalysis4
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 93;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis4.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] =0;
                drLab["UnitId"] = HTUnitId["UrinalysisProtein"];// Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }
            if (txtUrinalysis5.Text != "")
            {
                //Add Urinalysis5
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 94;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis5.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] = 0;
                drLab["UnitId"] = HTUnitId["UrinalysisLeukEst"];// Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }

            if (txtUrinalysis6.Text != "")
            {
                //Add Urinalysis6
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 95;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis6.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] = 0;
                drLab["UnitId"] = HTUnitId["UrinalysisNitrate"];// Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }
            if (txtUrinalysis7.Text != "")
            {
                //Add Urinalysis7
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 96;
                drLab["LabResult"] = Convert.ToDecimal(txtUrinalysis7.Text);
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                //drLab["UnitId"] = 0;
                drLab["UnitId"] = HTUnitId["UrinalysisBlood"];// Rupesh 21Dec07
                dtLabs.Rows.Add(drLab);
            }
            //Business rules for Paperless
            //if (Session["Paperless"].ToString() == "1")
            //{
            //    if (Convert.ToInt32(urineblood.SelectedValue) > 0)
            //    {
            //        //Add urineblood
            //        drLab = dtLabs.NewRow();
            //        drLab["LabTestID"] = 21;
            //        drLab["LabParameterID"] = 97;
            //        //drLab["LabResult"] = 0;
            //        drLab["LabResult"] = 99998888; // Rupesh
            //        drLab["LabResult1"] = "";
            //        drLab["LabResultId"] = Convert.ToInt32(urineblood.SelectedValue);
            //        drLab["Financed"] = 1;
            //        drLab["UnitId"] = 0;
            //        dtLabs.Rows.Add(drLab);

            //    }
            //    //Add urineWBC
            //    if (Convert.ToInt32(urineWBC.SelectedValue) > 0)
            //    {
            //        drLab = dtLabs.NewRow();
            //        drLab["LabTestID"] = 21;
            //        drLab["LabParameterID"] = 98;
            //        //drLab["LabResult"] = 0;
            //        drLab["LabResult"] = 99998888;//Rupesh 18Dec07
            //        drLab["LabResult1"] = "";
            //        drLab["LabResultId"] = Convert.ToInt32(urineWBC.SelectedValue);
            //        drLab["Financed"] = 1;
            //        drLab["UnitId"] = 0;
            //        dtLabs.Rows.Add(drLab);

            //    }
            //    //Add urineBact
            //    if (Convert.ToInt32(urineBact.SelectedValue) > 0)
            //    {
            //        drLab = dtLabs.NewRow();
            //        drLab["LabTestID"] = 21;
            //        drLab["LabParameterID"] = 99;
            //        //drLab["LabResult"] = 0;
            //        drLab["LabResult"] = 99998888;//Rupesh 18Dec07
            //        drLab["LabResult1"] = "";
            //        drLab["LabResultId"] = Convert.ToInt32(urineBact.SelectedValue);
            //        drLab["Financed"] = 1;
            //        drLab["UnitId"] = 0;
            //        dtLabs.Rows.Add(drLab);
            //    }

            //}

            //Business rules for Non Paperless
            //else
            //{
            if (Convert.ToInt32(urineblood.SelectedValue) > 0)
            {
                //Add urineblood
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 97;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888; // Rupesh
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = Convert.ToInt32(urineblood.SelectedValue);
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            else if (urineblood.SelectedItem.Text == "Select")
            {
                //Add urineBact
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 97;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888;//Rupesh 18Dec07
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            if (Convert.ToInt32(urineWBC.SelectedValue) > 0)
            {
                //Add urineWBC
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 98;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888;//Rupesh 18Dec07
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = Convert.ToInt32(urineWBC.SelectedValue);
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            else if (urineWBC.SelectedItem.Text == "Select")
            {
                //Add urineBact
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 98;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888;//Rupesh 18Dec07
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            if (Convert.ToInt32(urineBact.SelectedValue) > 0)
            {
                //Add urineBact
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 99;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888;//Rupesh 18Dec07
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = Convert.ToInt32(urineBact.SelectedValue);
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }
            else if (urineBact.SelectedItem.Text == "Select")
            {
                //Add urineBact
                drLab = dtLabs.NewRow();
                drLab["LabTestID"] = 21;
                drLab["LabParameterID"] = 99;
                //drLab["LabResult"] = 0;
                drLab["LabResult"] = 99998888;//Rupesh 18Dec07
                drLab["LabResult1"] = "";
                drLab["LabResultId"] = 0;
                drLab["Financed"] = 1;
                drLab["UnitId"] = 0;
                dtLabs.Rows.Add(drLab);
            }

            for (int i = 0; i < urineCasts.Items.Count; i++)
            {
                //Add urineCasts
                if (urineCasts.Items[i].Selected == true)
                {
                    drLab = dtLabs.NewRow();
                    drLab["LabTestID"] = 21;
                    drLab["LabParameterID"] = 100;
                    //drLab["LabResult"] = 0;
                    drLab["LabResult"] = 99998888; // Rupesh
                    drLab["LabResult1"] = "";
                    drLab["LabResultId"] = Convert.ToInt32(urineCasts.Items[i].Value);
                    drLab["Financed"] = 1;
                    drLab["UnitId"] = 0;
                    dtLabs.Rows.Add(drLab);
                }
            }
            #endregion "Building Table"

            return dtLabs;
        }

        private void DeleteForm()
        {
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"]);
            FormName = "Laboratory";

            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            theResultRow = (int)LabResultManager.DeleteLabForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"].ToString()));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx");
                Response.Redirect(theUrl);
            }
        }

        private void EnableControlDynamic()
        {
            try
            {
                //Making Additional Lab Button visible false
                OtherLabs.Visible = false;

                String LabID = Convert.ToString(ViewState["LabId"]);
                if (LabID != "0")
                {
                    ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet theDSLabResult = LabResultManager.GetPatientLab(LabID);

                    foreach (DataRow dr in theDSLabResult.Tables[0].Rows)
                    {
                        int LabIDLocal = Convert.ToInt32(dr["SubTestID"]);
                        switch (LabIDLocal)
                        {
                            case 1:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "td1", "document.getElementById('td1').disabled=false;", true);
                                txtCD4.Enabled = true;
                                txtCD4perc.Enabled = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCD4", "document.getElementById('lblCD4').className='right35 required';", true);
                                txtCD4.Enabled = true;
                                break;

                            case 2:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "td1", "document.getElementById('td1').disabled=false;", true);
                                txtCD4.Enabled = true;
                                txtCD4perc.Enabled = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCD4", "document.getElementById('lblCD4').className='right35 required';", true);
                                txtCD4perc.Enabled = true;
                                break;

                            case 53:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp53", "document.getElementById('sp53').disabled= false;", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblsero", "document.getElementById('lblsero').className='right35 required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblSerologyPos", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblSerologyPos').disabled='false';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblSerologyNeg", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblSerologyNeg').disabled='false';", true);
                                cblSerologyPos.Disabled = false;
                                cblSerologyNeg.Disabled = false;
                                //HIV_Rapid_Test.Enabled = true;
                                break;

                            case 101:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp101", "document.getElementById('sp101').disabled='';", true);
                                HIVSerologyConfirm.Enabled = true;
                                break;

                            case 110:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp104", "document.getElementById('sp104').disabled=false;", true);
                                break;

                            case 114:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp110", "document.getElementById('sp110').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "tdPR", "document.getElementById('tdPR').disabled=false;", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblPCR", "document.getElementById('lblPCR').className='right35 required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblPCRResultsPos", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblPCRResultsPos').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblPCRResultsNeg", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblPCRResultsNeg').disabled='';", true);
                                cblPCRResultsPos.Disabled = false;
                                cblPCRResultsNeg.Disabled = false;
                                break;

                            case 3:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "tblVL", "document.getElementById('tblVL').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblVLoad", "document.getElementById('lblVLoad').className='required';", true);
                                txtViralLoad.Enabled = true;
                                lblViralLoad.Enabled = true;
                                break;

                            case 107:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "tdVLUdtck", "document.getElementById('tdVLUdtck').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ChkViral", "document.getElementById('ctl00_IQCareContentPlaceHolder_ChkViral').disabled='';", true);
                                ChkViral.Disabled = false;
                                break;

                            case 4:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "tdSP", "document.getElementById('tdSP').disabled='';", true);
                                storePlasma.Enabled = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSPlasma", "document.getElementById('lblSPlasma').className='required';", true);
                                break;

                            case 5:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblHCT", "document.getElementById('lblHCT').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblHCT1", "document.getElementById('lblHCT').className='required';", true);
                                txtHCT.Enabled = true;
                                lblHCTPercent.Enabled = true;
                                break;

                            case 6:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblHb", "document.getElementById('lblHb').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblHb1", "document.getElementById('lblHb').className='margin20 required';", true);
                                txtHB.Enabled = true;
                                lblHb.Enabled = true;
                                break;

                            case 7:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblWBC", "document.getElementById('lblWBC').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblWBC1", "document.getElementById('lblWBC').className='required';", true);
                                txtWBC.Enabled = true;
                                lblWBC.Enabled = true;
                                break;

                            case 8:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff", "document.getElementById('lblDiff').disabled='';", true);
                                break;

                            case 82:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff9", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff1", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeut", "document.getElementById('lblNeut').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeut1", "document.getElementById('lblNeut').className='required';", true);
                                neut1.Enabled = true;
                                lblDiffNeut.Enabled = true;
                                break;

                            case 83:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff10", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff2", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeut2", "document.getElementById('lblNeut').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeut3", "document.getElementById('lblNeut').className='required';", true);
                                neut2.Enabled = true;
                                lblDiffneut2.Enabled = true;
                                break;

                            case 84:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff11", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff3", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymph", "document.getElementById('lblLymph').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymph1", "document.getElementById('lblLymph').className='required';", true);
                                lymph1.Enabled = true;
                                lblDiffLymph.Enabled = true;
                                break;

                            case 85:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff12", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff4", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymph2", "document.getElementById('lblLymph').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymph3", "document.getElementById('lblLymph').className='required';", true);
                                lymph2.Enabled = true;
                                lblLymphPerc.Enabled = true;
                                break;

                            case 86:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff13", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff5", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblMono", "document.getElementById('lblMono').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblMono1", "document.getElementById('lblMono').className='required';", true);
                                mono1.Enabled = true;
                                lblDiffMono.Enabled = true;
                                break;

                            case 87:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff14", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff6", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblMono2", "document.getElementById('lblMono').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblMono3", "document.getElementById('lblMono').className='required';", true);
                                mono2.Enabled = true;
                                lblDiffMono2.Enabled = true;
                                break;

                            case 88:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff15", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff7", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblEosin", "document.getElementById('lblEosin').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblEosin1", "document.getElementById('lblEosin').className='required';", true);
                                eosin1.Enabled = true;
                                lblDiffEosin.Enabled = true;
                                break;

                            case 89:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff16", "document.getElementById('lblDiff').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff8", "document.getElementById('lblDiff').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblEosin2", "document.getElementById('lblEosin').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblEosin3", "document.getElementById('lblEosin').className='required';", true);
                                eosin2.Enabled = true;
                                lblDiffEosinPerc.Enabled = true;
                                break;

                            case 9:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblPlats", "document.getElementById('lblPlats').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblPlats1", "document.getElementById('lblPlats').className='required';", true);
                                txtPlatelets.Enabled = true;
                                lblPlatelets.Enabled = true;
                                break;

                            case 10:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblASTSGOT_html", "document.getElementById('lblASTSGOT_html').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblASTSGOT_html1", "document.getElementById('lblASTSGOT_html').className='required';", true);
                                txtAST.Enabled = true;
                                lblASTSGOT.Enabled = true;
                                break;

                            case 11:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblALTSPGT_html", "document.getElementById('lblALTSPGT_html').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblALTSPGT_html1", "document.getElementById('lblALTSPGT_html').className='required';", true);
                                txtALT.Enabled = true;
                                lblAltSpgt.Enabled = true;
                                break;

                            case 12:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblcreatinine", "document.getElementById('lblcreatinine').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblcreatinine1", "document.getElementById('lblcreatinine').className='right40 required';", true);
                                txtCreatinine.Enabled = true;
                                lblCreatinine.Enabled = true;
                                break;

                            case 13:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblAmylase", "document.getElementById('lblAmylase').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblAmylase1", "document.getElementById('lblAmylase').className='margin50 required';", true);
                                txtAmylase.Enabled = true;
                                lblAmylase.Enabled = true;
                                break;

                            case 14:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "PregSpan", "document.getElementById('PregSpan').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdoclPreg", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdoclPreg').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdoclPreg2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdoclPreg2').disabled='';", true);
                                rdoclPreg.Disabled = false;
                                rdoclPreg2.Disabled = false;
                                break;

                            case 15:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "tdMparasite", "document.getElementById('tdMparasite').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblMparasite", "document.getElementById('lblMparasite').className='required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdochkMalaria", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdochkMalaria').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdochkMalaria2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdochkMalaria2').disabled='';", true);
                                rdochkMalaria.Disabled = false;
                                rdochkMalaria2.Disabled = false;
                                break;

                            case 16:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "tdSCr", "document.getElementById('tdSCr').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSCryp", "document.getElementById('lblSCryp').className='required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdorbSerum1", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdorbSerum1').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdorbSerum2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdorbSerum2').disabled='';", true);
                                rdorbSerum1.Disabled = false;
                                rdorbSerum2.Disabled = false;
                                break;

                            case 17:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB", "document.getElementById('lblSpAFB').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB1", "document.getElementById('lblSpAFB').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp1AFB", "document.getElementById('sp1AFB').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sputumafb1", "document.getElementById('ctl00_IQCareContentPlaceHolder_sputumafb1').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sputumafb12", "document.getElementById('ctl00_IQCareContentPlaceHolder_sputumafb12').disabled='';", true);
                                sputumafb1.Disabled = false;
                                sputumafb12.Disabled = false;
                                break;

                            case 18:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB2", "document.getElementById('lblSpAFB2').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp2AFB", "document.getElementById('sp2AFB').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sputumafb21", "document.getElementById('ctl00_IQCareContentPlaceHolder_sputumafb21').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sputumafb22", "document.getElementById('ctl00_IQCareContentPlaceHolder_sputumafb22').disabled='';", true);
                                sputumafb21.Disabled = false;
                                sputumafb22.Disabled = false;
                                break;

                            case 19:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB3", "document.getElementById('lblSpAFB3').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp3AFB", "document.getElementById('sp3AFB').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sputumafb3", "document.getElementById('ctl00_IQCareContentPlaceHolder_sputumafb3').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sputumafb31", "document.getElementById('ctl00_IQCareContentPlaceHolder_sputumafb31').disabled='';", true);
                                sputumafb3.Disabled = false;
                                sputumafb31.Disabled = false;
                                break;

                            case 20:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "tdGStain", "document.getElementById('tdGStain').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGstain", "document.getElementById('lblGstain').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "txtgramStain", "document.getElementById('ctl00_IQCareContentPlaceHolder_txtgramStain').disabled='';", true);
                                txtgramStain.Disabled = false;

                                break;

                            case 21:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis1", "document.getElementById('lblurilysis').className='required';", true);
                                break;

                            case 22:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCulture", "document.getElementById('lblCulture').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCulture1", "document.getElementById('lblCulture').className='required';", true);
                                txtcultureSensitivity.Disabled = false;
                                break;

                            case 90:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis2", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblspecGrav", "document.getElementById('lblspecGrav').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblspecGrav1", "document.getElementById('lblspecGrav').className='required';", true);
                                txtUrinalysis1.Enabled = true;
                                lblUrinalysisSpecGrav.Enabled = true;
                                break;

                            case 91:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis3", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGlucose", "document.getElementById('lblGlucose').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGlucose1", "document.getElementById('lblGlucose').className='required';", true);
                                txtUrinalysis2.Enabled = true;
                                lblUrinalysisGlucose.Enabled = true;
                                break;

                            case 92:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis4", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblKetone", "document.getElementById('lblKetone').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblKetone1", "document.getElementById('lblKetone').className='required';", true);
                                txtUrinalysis3.Enabled = true;
                                lblUrinalysisKetone.Enabled = true;
                                break;

                            case 93:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis5", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblProtein", "document.getElementById('lblProtein').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblProtein1", "document.getElementById('lblProtein').className='required';", true);
                                txtUrinalysis4.Enabled = true;
                                lblUrinalysisProtein.Enabled = true;
                                break;

                            case 94:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis6", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLeuk", "document.getElementById('lblLeuk').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLeuk1", "document.getElementById('lblLeuk').className='required';", true);
                                txtUrinalysis5.Enabled = true;
                                lblUrinalysisLeukEst.Enabled = true;
                                break;

                            case 95:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis7", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNitrate", "document.getElementById('lblNitrate').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNitrate1", "document.getElementById('lblNitrate').className='required';", true);
                                txtUrinalysis6.Enabled = true;
                                lblUrinalysisNitrate.Enabled = true;
                                break;

                            case 96:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis8", "document.getElementById('lblurilysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblBlood", "document.getElementById('lblBlood').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblBlood1", "document.getElementById('lblBlood').className='required';", true);
                                txtUrinalysis7.Enabled = true;
                                lblUrinalysisBlood.Enabled = true;
                                break;

                            case 97:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis", "document.getElementById('lblUrinalysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis1", "document.getElementById('lblUrinalysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinMBlood", "document.getElementById('lblUrinMBlood').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinMBlood1", "document.getElementById('lblUrinMBlood').className='smalllabel right30 required';", true);
                                urineblood.Enabled = true;
                                break;

                            case 98:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis", "document.getElementById('lblUrinalysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis2", "document.getElementById('lblUrinalysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrMicrWBC", "document.getElementById('lblUrMicrWBC').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrMicrWBC1", "document.getElementById('lblUrMicrWBC').className='smalllabel margin10 required';", true);
                                urineWBC.Enabled = true;
                                break;

                            case 99:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis", "document.getElementById('lblUrinalysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis3", "document.getElementById('lblUrinalysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrMicrBac", "document.getElementById('lblUrMicrBac').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrMicrBac1", "document.getElementById('lblUrMicrBac').className='smalllabel right30 required';", true);
                                urineBact.Enabled = true;
                                break;

                            case 100:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis", "document.getElementById('lblUrinalysis').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis1", "document.getElementById('lblUrinalysis').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinMicCast", "document.getElementById('lblUrinMicCast').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinMicCast1", "document.getElementById('lblUrinMicCast').className='pad5 required';", true);
                                urineCasts.Enabled = true;
                                break;

                            case 103:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCulture", "document.getElementById('lblCulture').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCultur1", "document.getElementById('lblCulture').className=required;", true);
                                txtcultureSensitivity.Disabled = false;
                                break;

                            case 105:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblStool", "document.getElementById('lblStool').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblStool1", "document.getElementById('lblStool').className='right20 required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "divstool", "document.getElementById('divstool').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdoStoolList", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdoStoolList').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdoStoolList2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdoStoolList2').disabled='';", true);
                                rdoStoolList.Disabled = false;
                                rdoStoolList2.Disabled = false;
                                break;

                            case 55:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblstoolDesc", "document.getElementById('lblstoolDesc').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblstoolDesc1", "document.getElementById('lblstoolDesc').className='right20 required';", true);
                                stooldesc.Disabled = false;
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "stooldesc", "document.getElementById('ctl00_IQCareContentPlaceHolder_stooldesc').disabled='';", true);
                                break;

                            case 57:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblESR", "document.getElementById('lblESR').disabled=false;", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblESR", "document.getElementById('lblESR').className='right20 required';", true);
                                TxtESR.Enabled = true;
                                lblESR2.Enabled = true;
                                break;

                            case 23:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "trCry", "document.getElementById('trCry').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCrypAg", "document.getElementById('lblCrypAg').className='required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdocblSerumCrypto1", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdocblSerumCrypto1').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdocblSerumCrypto2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdocblSerumCrypto2').disabled='';", true);
                                rdocblSerumCrypto1.Disabled = false;
                                rdocblSerumCrypto2.Disabled = false;
                                break;

                            case 24:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "trCSFInk", "document.getElementById('trCSFInk').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCSFInk", "document.getElementById('lblCSFInk').className='required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdocsfIndiaInk1", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdocsfIndiaInk1').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdocsfIndiaInk2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdocsfIndiaInk2').disabled='';", true);
                                rdocsfIndiaInk1.Disabled = false;
                                rdocsfIndiaInk2.Disabled = false;
                                break;

                            case 25:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "trGStain", "document.getElementById('trGStain').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCSFGram", "document.getElementById('lblCSFGram').className='required';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdocsfgramstain1", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdocsfgramstain1').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdocsfgramstain2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdocsfgramstain2').disabled='';", true);
                                rdocsfgramstain1.Disabled = false;
                                rdocsfgramstain2.Disabled = false;
                                break;

                            case 26:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "trCulture", "document.getElementById('trCulture').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCultur", "document.getElementById('lblCultur').className='required';", true);
                                csfCulture.Enabled = true;
                                break;

                            case 28:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCellcount", "document.getElementById('lblCellcount').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCellcount1", "document.getElementById('lblCellcount').className='required';", true);
                                txtRBCs.Enabled = true;
                                lblCellCountRBC.Enabled = true;
                                break;

                            case 29:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblWBChtml", "document.getElementById('lblWBChtml').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblWBChtml1", "document.getElementById('lblWBChtml').className='required';", true);
                                txtWBCs.Enabled = true;
                                lblCellCountWBC.Enabled = true;
                                break;

                            case 30:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeuthtml", "document.getElementById('lblNeuthtml').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeuthtml1", "document.getElementById('lblNeuthtml').className='required';", true);
                                txtNeutrophils.Enabled = true;
                                lblCellCountNeutrophils.Enabled = true;
                                break;

                            case 31:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymp", "document.getElementById('lblLymp').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymp1", "document.getElementById('lblLymp').className='required';", true);
                                txtLymphocytes.Enabled = true;
                                lblCellCountLymphocytes.Enabled = true;
                                break;

                            case 54:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblbiochem", "document.getElementById('lblbiochem').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblbiochem1", "document.getElementById('lblbiochem').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGlucosehtml", "document.getElementById('lblGlucosehtml').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGlucosehtml1", "document.getElementById('lblGlucosehtml').className='margin20 required';", true);
                                txtGlucose.Enabled = true;
                                lblGlucose.Enabled = true;
                                break;

                            case 32:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblbiochem2", "document.getElementById('lblbiochem').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblbiochem3", "document.getElementById('lblbiochem').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblProteinhtml", "document.getElementById('lblProteinhtml').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblProteinhtml1", "document.getElementById('lblProteinhtml').className='margin50 required';", true);
                                txtProtein.Enabled = true;
                                lblProtein.Enabled = true;
                                break;

                            case 74:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp102", "document.getElementById('sp102').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblFTAPos", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblFTAPos').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblFTANeg", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblFTANeg').disabled='';", true);
                                cblFTAPos.Disabled = false;
                                cblFTANeg.Disabled = false;

                                break;

                            case 75:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "sp103", "document.getElementById('sp103').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblRPRVDRLPos", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblRPRVDRLPos').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "cblRPRVDRLNeg", "document.getElementById('ctl00_IQCareContentPlaceHolder_cblRPRVDRLNeg').disabled='';", true);
                                cblRPRVDRLPos.Disabled = false;
                                cblRPRVDRLNeg.Disabled = false;
                                break;

                            case 104:
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCXray", "document.getElementById('lblCXray').disabled='';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCXray1", "document.getElementById('lblCXray').className='required';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "divX-Ray", "document.getElementById('divX-Ray').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdochestxray1", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdochestxray1').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "rdochestxray2", "document.getElementById('ctl00_IQCareContentPlaceHolder_rdochestxray2').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "divtxtarea", "document.getElementById('ctl00_IQCareContentPlaceHolder_divtxtarea').disabled='';", true);
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "xraytxt", "document.getElementById('xraytxt').disabled='';", true);
                                rdochestxray1.Disabled = false;
                                rdochestxray2.Disabled = false;
                                xraytxt.Disabled = false;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch { }
            finally { }
        }

        private void EnableControlStatic()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "td1", "document.getElementById('td1').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCD4", "document.getElementById('lblCD4').className='right35';", true);
            txtCD4.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "td1", "document.getElementById('td1').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCD4", "document.getElementById('lblCD4').className='right35';", true);
            txtCD4perc.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp53", "document.getElementById('sp53').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblsero", "document.getElementById('lblsero').className='right35';", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp101", "document.getElementById('sp101').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp102", "document.getElementById('sp102').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp103", "document.getElementById('sp103').disabled=false;", true);
            cblSerologyPos.Disabled = false;
            cblSerologyNeg.Disabled = false;
            // HIV_Rapid_Test.Enabled = true;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "tdPR", "document.getElementById('tdPR').disabled=false;", true);
            HIVSerologyConfirm.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tblVL", "document.getElementById('tblVL').disabled=false;", true);
            txtViralLoad.Enabled = true;
            lblViralLoad.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tdVLUdtck", "document.getElementById('tdVLUdtck').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tdSP", "document.getElementById('tdSP').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblHCT", "document.getElementById('lblHCT').disabled=false;", true);
            txtHCT.Enabled = true;
            lblHCTPercent.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblHb", "document.getElementById('lblHb').disabled=false;", true);
            txtHB.Enabled = true;
            lblHb.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblWBC", "document.getElementById('lblWBC').disabled=false;", true);
            txtWBC.Enabled = true;
            lblWBC.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblDiff", "document.getElementById('lblDiff').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeut", "document.getElementById('lblNeut').disabled=false;", true);
            neut1.Enabled = true;
            lblDiffNeut.Enabled = true;
            neut2.Enabled = true;
            lblDiffneut2.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymph", "document.getElementById('lblLymph').disabled=false;", true);
            lymph1.Enabled = true;
            lblDiffLymph.Enabled = true;
            lymph2.Enabled = true;
            lblLymphPerc.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblMono", "document.getElementById('lblMono').disabled=false;", true);
            mono1.Enabled = true;
            lblDiffMono.Enabled = true;
            mono2.Enabled = true;
            lblDiffMono2.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblEosin", "document.getElementById('lblEosin').disabled=false;", true);
            eosin1.Enabled = true;
            lblDiffEosin.Enabled = true;
            eosin2.Enabled = true;
            lblDiffEosinPerc.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblPlats", "document.getElementById('lblPlats').disabled=false;", true);
            txtPlatelets.Enabled = true;
            lblPlatelets.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblESR", "document.getElementById('lblESR').disabled=false;", true);
            TxtESR.Enabled = true;
            lblESR2.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblASTSGOT_html", "document.getElementById('lblASTSGOT_html').disabled=false;", true);
            txtAST.Enabled = true;
            lblASTSGOT.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblALTSPGT_html", "document.getElementById('lblALTSPGT_html').disabled=false;", true);
            txtALT.Enabled = true;
            lblAltSpgt.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblcreatinine", "document.getElementById('lblcreatinine').disabled=false;", true);
            txtCreatinine.Enabled = true;
            lblCreatinine.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblAmylase", "document.getElementById('lblAmylase').disabled=false;", true);
            txtAmylase.Enabled = true;
            lblAmylase.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tdMparasite", "document.getElementById('tdMparasite').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tdSCr", "document.getElementById('tdSCr').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB", "document.getElementById('lblSpAFB').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp1AFB", "document.getElementById('sp1AFB').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB", "document.getElementById('lblSpAFB').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp2AFB", "document.getElementById('sp2AFB').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblSpAFB", "document.getElementById('lblSpAFB').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp3AFB", "document.getElementById('sp3AFB').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tdGStain", "document.getElementById('tdGStain').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblspecGrav", "document.getElementById('lblspecGrav').disabled=false;", true);
            txtUrinalysis1.Enabled = true;
            lblUrinalysisSpecGrav.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PregSpan", "try{document.getElementById('PregSpan').disabled=false;}catch{}", true);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGlucose", "document.getElementById('lblGlucose').disabled=false;", true);
            txtUrinalysis2.Enabled = true;
            lblUrinalysisGlucose.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblKetone", "document.getElementById('lblKetone').disabled=false;", true);
            txtUrinalysis3.Enabled = true;
            lblUrinalysisKetone.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblProtein", "document.getElementById('lblProtein').disabled=false;", true);
            txtUrinalysis4.Enabled = true;
            lblUrinalysisProtein.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLeuk", "document.getElementById('lblLeuk').disabled=false;", true);
            txtUrinalysis5.Enabled = true;
            lblUrinalysisLeukEst.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNitrate", "document.getElementById('lblNitrate').disabled=false;", true);
            txtUrinalysis6.Enabled = true;
            lblUrinalysisNitrate.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblurilysis", "document.getElementById('lblurilysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblBlood", "document.getElementById('lblBlood').disabled=false;", true);
            txtUrinalysis7.Enabled = true;
            lblUrinalysisBlood.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis", "document.getElementById('lblUrinalysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinMBlood", "document.getElementById('lblUrinMBlood').disabled=false;", true);
            urineblood.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrMicrWBC", "document.getElementById('lblUrMicrWBC').disabled=false;", true);
            urineWBC.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrMicrBac", "document.getElementById('lblUrMicrBac').disabled=false;", true);
            urineBact.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinalysis", "document.getElementById('lblUrinalysis').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblUrinMicCast", "document.getElementById('lblUrinMicCast').disabled=false;", true);
            urineCasts.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCulture", "document.getElementById('lblCulture').disabled=false;", true);
            txtcultureSensitivity.Disabled = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblStool", "document.getElementById('lblStool').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "divstool", "document.getElementById('divstool').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblstoolDesc", "document.getElementById('lblstoolDesc').disabled=false;", true);
            stooldesc.Disabled = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "trCry", "document.getElementById('trCry').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "trCSFInk", "document.getElementById('trCSFInk').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "trGStain", "document.getElementById('trGStain').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "trCulture", "document.getElementById('trCulture').disabled=false;", true);
            csfCulture.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCellcount", "document.getElementById('lblCellcount').disabled=false;", true);
            txtRBCs.Enabled = true;
            lblCellCountRBC.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblWBChtml", "document.getElementById('lblWBChtml').disabled=false;", true);
            txtWBCs.Enabled = true;
            lblCellCountWBC.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblNeuthtml", "document.getElementById('lblNeuthtml').disabled=false;", true);
            txtNeutrophils.Enabled = true;
            lblCellCountNeutrophils.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblLymp", "document.getElementById('lblLymp').disabled=false;", true);
            txtLymphocytes.Enabled = true;
            lblCellCountLymphocytes.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblbiochem", "document.getElementById('lblbiochem').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblGlucosehtml", "document.getElementById('lblGlucosehtml').disabled=false;", true);
            txtGlucose.Enabled = true;
            lblGlucose.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblProteinhtml", "document.getElementById('lblProteinhtml').disabled=false;", true);
            txtProtein.Enabled = true;
            lblProtein.Enabled = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblCXray", "document.getElementById('lblCXray').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "divX-Ray", "document.getElementById('divX-Ray').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "divtxtarea", "document.getElementById('divtxtarea').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sp110", "document.getElementById('sp110').disabled=false;", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lblPCR", "document.getElementById('lblPCR').className='right35';", true);
            cblPCRResultsPos.Disabled = false;
            cblPCRResultsNeg.Disabled = false;
            ChkViral.Disabled = false;
            storePlasma.Enabled = true;
            rdoclPreg.Disabled = false;
            rdoclPreg2.Disabled = false;
            rdochkMalaria.Disabled = false;
            rdochkMalaria2.Disabled = false;
            rdorbSerum1.Disabled = false;
            rdorbSerum2.Disabled = false;
            sputumafb1.Disabled = false;
            sputumafb12.Disabled = false;
            sputumafb21.Disabled = false;
            sputumafb22.Disabled = false;
            sputumafb3.Disabled = false;
            sputumafb31.Disabled = false;
            txtgramStain.Disabled = false;
            rdoStoolList.Disabled = false;
            rdoStoolList2.Disabled = false;
            rdocblSerumCrypto1.Disabled = false;
            rdocblSerumCrypto2.Disabled = false;
            rdocsfIndiaInk1.Disabled = false;
            rdocsfIndiaInk2.Disabled = false;
            rdocsfgramstain1.Disabled = false;
            rdocsfgramstain2.Disabled = false;
            cblFTAPos.Disabled = false;
            cblFTANeg.Disabled = false;
            cblRPRVDRLPos.Disabled = false;
            cblRPRVDRLNeg.Disabled = false;
            rdochestxray1.Disabled = false;
            rdochestxray2.Disabled = false;
            xraytxt.Disabled = false;
        }

        private Boolean FieldValidation()
        {
            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            //--- Rupesh 24-Mar-08: starts: Remove all check from LabToBeDon---
            // if (txtLabtobeDone.Text != "")
            // {
            //     if (Convert.ToDateTime(theUtils.MakeDate(txtLabtobeDone.Text)) < Convert.ToDateTime(Application["AppCurrentDate"]))
            //     {
            //         IQCareMsgBox.Show("LabtobeDone", this);
            //         txtLabtobeDone.Focus();
            //         return false;

            //     }
            // }
            //--- Rupesh 24-Mar-08: ends: Remove all check from LabToBeDon---

            //if (txtLabtobeDone.Text == "")
            //{
            //    MsgBuilder theMsg = new MsgBuilder();
            //    theMsg.DataElements["Control"] = "Lab Date";
            //    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            //    return false;
            //}

            if (txtLabtobeDone.Text != "")
            {
                theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtLabtobeDone.Text));
                if (ViewState["IEVisitDate"] != null)
                {
                    DateTime theIEVisitDate = Convert.ToDateTime(ViewState["IEVisitDate"].ToString());
                    if (theIEVisitDate > theVisitDate)
                    {
                        IQCareMsgBox.Show("CompareLabTobeDoneDate", this);
                        txtLabtobeDone.Focus();
                        return false;
                    }
                    //else if (theVisitDate > theCurrentDate)
                    //{
                    //    IQCareMsgBox.Show("LabToBeDoneGreater", this);
                    //    txtLabtobeDone.Focus();
                    //    return false;
                    //}
                }
            }

            if (txtLabOrderedbyDate.Text.ToString() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Lab OrderedBy Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtLabOrderedbyDate.Focus();
                return false;
            }
            else if (txtLabOrderedbyDate.Text.ToString() != "")
            {
                /*
                theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtLabtobeDone.Text));
                if (Convert.ToDateTime(theUtils.MakeDate(txtLabOrderedbyDate.Text)) < theVisitDate)
                {
                    IQCareMsgBox.Show("LabOrderDate", this);
                    txtLabOrderedbyDate.Focus();
                    return false;
                }
                */
                theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtLabOrderedbyDate.Text));

                //if (ViewState["IEVisitDate"] != null)

                if (ViewState["VisitDate"] != null)
                {
                    //DateTime theIEVisitDate = Convert.ToDateTime(ViewState["IEVisitDate"].ToString());
                    DateTime theVisitDate1 = Convert.ToDateTime(ViewState["VisitDate"].ToString());
                    if (theVisitDate1 > theVisitDate)
                    {
                        #region "21-Jun-07 - 1"
                        IQCareMsgBox.Show("CompareLabVisitDate", this);
                        txtLabOrderedbyDate.Focus();
                        return false;
                        #endregion "21-Jun-07 - 1"
                    }
                    else if (theVisitDate > theCurrentDate)
                    {
                        IQCareMsgBox.Show("NonARTVisitDate", this);
                        txtLabOrderedbyDate.Focus();
                        return false;
                    }
                }
            }
            if (ddLabOrderedbyName.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Lab OrderedBy Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddLabOrderedbyName.Focus();
                return false;
            }
            if (Session["Paperless"].ToString() == "0")
            {
                if (txtlabReportedbyDate.Text.ToString() == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Lab Reported By Date";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtlabReportedbyDate.Focus();
                    return false;
                }
                else if (txtlabReportedbyDate.Text.ToString() != "")
                {
                    theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtlabReportedbyDate.Text));

                    DateTime theReportDate = Convert.ToDateTime(theUtils.MakeDate(txtlabReportedbyDate.Text));

                    if (theReportDate < theVisitDate)
                    {
                        IQCareMsgBox.Show("LabReportedDate", this);
                        txtlabReportedbyDate.Focus();
                        return false;
                    }
                    if (theReportDate > theCurrentDate)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        IQCareMsgBox.Show("LabReportedDate", this);
                        txtlabReportedbyDate.Focus();
                        return false;
                    }
                }
            }
            if (Session["Paperless"].ToString() == "0")
            {
                if (ddlLabReportedbyName.SelectedIndex == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Reported by Name";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    ddlLabReportedbyName.Focus();
                    return false;
                }
            }

            if ((txtLabOrderedbyDate.Text.Trim() != "") && (txtlabReportedbyDate.Text.Trim() != ""))
            {
                DateTime theOrdByDate = Convert.ToDateTime(theUtils.MakeDate(txtLabOrderedbyDate.Text.Trim()));
                DateTime theDispByDate = Convert.ToDateTime(theUtils.MakeDate(txtlabReportedbyDate.Text.Trim()));
                if (theOrdByDate > theDispByDate)
                {
                    IQCareMsgBox.Show("LabReportedDate", this);
                    txtLabOrderedbyDate.Focus();
                    return false;
                }
            }
            //--- Rupesh : starts : 01Feb08 ---
            if (ChkViral.Checked == true && txtViralLoadDet.Text == "")
            {
                IQCareMsgBox.Show("UnChkViralLoad", this);
                txtLabOrderedbyDate.Focus();
                return false;
            }

            //}

            //--- Rupesh : ends : 01Feb08 ---

            return true;
        }

        //Populate Old Data in the Custom Field
        //private void FillOldCustomData(Control Cntrl, Int32 PatID)
        private void FillOldCustomData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;

            try
            {
                if (ViewState["CustomFieldsDS"] != null)
                {
                    DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                    string theTblName = "";

                    if (theCustomFields.Tables[0].Rows.Count > 0)
                        theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                    string theColName = "";
                    foreach (DataRow theDRs in theCustomFields.Tables[0].Rows)
                    {
                        if (theDRs["ControlId"].ToString() != "9")
                        {
                            if (theColName == "")
                                theColName = theDRs["Label"].ToString();
                            else
                                theColName = theColName + "," + theDRs["Label"].ToString();
                        }
                    }

                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, 0, Convert.ToInt32(ViewState["LabID"]), 0, Convert.ToInt32(ApplicationAccess.Laboratory));
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "Lab");
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
                CustomFields = null;
            }
        }

        //private void FillOldData(Int32 PatID)
        private void FillOldData(Control Cntrl, DataRow theDR)
        {
            //////DataSet dsvalues = null;
            //////ICustomFields CustomFields;

            //////try
            //////{
            //////    DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
            //////    string theTblName = "";
            //////    if (theCustomFields.Tables[0].Rows.Count > 0)
            //////        theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
            //////    string theColName = "";
            //////    foreach (DataRow theDRs in theCustomFields.Tables[0].Rows)
            //////    {
            //////        if (theDRs["ControlId"].ToString() != "9")
            //////        {
            //////            if (theColName == "")
            //////                theColName = theDRs["Label"].ToString();
            //////            else
            //////                theColName = theColName + "," + theDRs["Label"].ToString();
            //////        }
            //////    }

            //////    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            //////    dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0,0, Convert.ToInt32(ViewState["LabID"]),0, Convert.ToInt32(ApplicationAccess.Laboratory));
            //////    CustomFieldClinical theCustomManager = new CustomFieldClinical();
            //////    theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "Lab");
            //////}
            //////catch (Exception err)
            //////{
            //////    MsgBuilder theBuilder = new MsgBuilder();
            //////    theBuilder.DataElements["MessageText"] = err.Message.ToString();
            //////    IQCareMsgBox.Show("#C1", theBuilder, this);
            //////}
            //////finally
            //////{
            //////    CustomFields = null;
            //////}

            //This procedure fills the additional labs from the database in the additional labs panel
            int y = 0;

            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    FillOldData(x, theDR);
                }
                else
                {
                    {
                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {
                            if (x.ID.StartsWith("LabResult"))
                                y = Convert.ToInt32(x.ID.Substring(9, x.ID.Length - 9));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                ((TextBox)x).Text = theDR["TestResults1"].ToString();
                            }
                        }
                        else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                        {
                            if (x.ID.StartsWith("ddlLabResult"))
                                y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                //((TextBox)x).Text = theDR["TestResults1"].ToString();
                                for (int k = 0; k < ((DropDownList)x).Items.Count; k++)
                                {
                                    if (((DropDownList)x).Items[k].Value == theDR["TestResults1"].ToString())
                                    {
                                        ((DropDownList)x).Items.FindByValue(theDR["TestResults1"].ToString()).Selected = true;
                                    }
                                }
                            }
                        }
                        else if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            if (x.ID.ToUpper().StartsWith("FinChk"))
                                y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["Financed"]);
                            }
                        }
                    }
                }
            }
        }

        private void FillUnits()
        {
            //---- Rupesh 26Dec07 to display SELECTED units starts------
            DataSet theDS = (DataSet)ViewState["LabRanges"];

            //if ((Request.QueryString["name"] == "Edit") && (ViewState["OldDS"] != null))
            if ((Convert.ToInt32(ViewState["LabId"]) > 0) && (ViewState["OldDS"] != null))
            {
                foreach (DataRow theDRold in ((DataSet)ViewState["OldDS"]).Tables[0].Rows)
                {
                    foreach (DataRow theDR in theDS.Tables[4].Rows)
                    {
                        if (theDR["SubTestId"].ToString() == theDRold["SubTestId"].ToString())
                        {
                            if (theDRold.IsNull("Units") == false)
                            {
                                theDR["unitID"] = theDRold["Units"].ToString();
                                theDR["UnitName"] = theDRold["UnitName"].ToString();
                            }
                            ViewState["LabRanges"] = theDS;
                            break;
                        }
                    }
                }
            }

            //---------- ends--------------

            DataView theDV;
            Hashtable HTUnitId = new Hashtable();
            if (ViewState["HTUnitId"] != null)
                HTUnitId = (Hashtable)ViewState["HTUnitId"];

            //----- commented by Rupesh 26Dec07 ------
            //DataSet theDS = (DataSet)ViewState["LabRanges"];
            //------------------

            theDV = new DataView(theDS.Tables[4]);

            txtLabtobeDone.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLabtobeDone.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            txtLabOrderedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLabOrderedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            txtlabReportedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlabReportedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            if (labelCD4.Text == "")
            {
                //theDV.RowFilter = "SubTestName='CD4' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=1 and DefaultUnit is not null";
                labelCD4.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["CD4"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";

                if (theDV.Count != 0)
                {
                    txtCD4.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4.ClientID + "'); AddBoundary('" + txtCD4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtCD4.Attributes.Add("onblur", "CheckValue('" + txtCD4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtCD4.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            if (lblCD4Perc.Text == "")
            {
                //theDV.RowFilter = "SubTestName='CD4Percent' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=2 and DefaultUnit is not null";
                lblCD4Perc.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["CD4Percent"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtCD4perc.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4perc.ClientID + "'); AddBoundary('" + txtCD4perc.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtCD4perc.Attributes.Add("onblur", "CheckValue('" + txtCD4perc.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtCD4perc.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            if (lblViralLoad.Text == "")
            {
                //theDV.RowFilter = "SubTestName='ViralLoad' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=3 and DefaultUnit is not null";
                lblViralLoad.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["ViralLoad"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtViralLoad.Attributes.Add("onblur", "chkDecimal('" + txtViralLoad.ClientID + "');AddBoundary('" + txtViralLoad.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtViralLoad.Attributes.Add("onblur", "CheckValue('" + txtViralLoad.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtViralLoad.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");

                    //--- commented by Rupesh : 01Feb08 : viral load undetectable was not accepting values within correct range : issued no 696
                    //txtViralLoadDet.Attributes.Add("onkeyup", "chkDecimal('" + txtViralLoadDet.ClientID + "'); AddBoundary('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    //txtViralLoadDet.Attributes.Add("onblur", "CheckValue('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            //---- Rupesh : 01Feb08 : starts :viral load undetectable was not accepting values within correct range : issued no 696----
            theDV.RowFilter = "SubTestId=107 and DefaultUnit is not null";
            HTUnitId["ViralLoadDet"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
            txtViralLoadDet.Attributes.Add("onkeyup", "chkDecimal('" + txtViralLoadDet.ClientID + "'); AddBoundary('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            txtViralLoadDet.Attributes.Add("onblur", "CheckValue('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtViralLoadDet.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            //---- Rupesh : 01Feb08 : ends : viral load undetectable was not accepting values within correct range : issued no 696 ----

            if (lblHCTPercent.Text == "")
            {
                //theDV.RowFilter = "SubTestName='HCTPercent' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=5 and DefaultUnit is not null";
                lblHCTPercent.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["HCTPercent"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtHCT.Attributes.Add("onkeyup", "chkDecimal('" + txtHCT.ClientID + "'); AddBoundary('" + txtHCT.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtHCT.Attributes.Add("onblur", "CheckValue('" + txtHCT.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtHCT.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            if (lblHb.Text == "")
            {
                //theDV.RowFilter = "SubTestName='Hct/Hb' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=6 and DefaultUnit is not null";
                lblHb.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["Hct/Hb"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtHB.Attributes.Add("onkeyup", "chkDecimal('" + txtHB.ClientID + "'); AddBoundary('" + txtHB.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtHB.Attributes.Add("onblur", "CheckValue('" + txtHB.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtHB.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            if (lblWBC.Text == "")
            {
                //theDV.RowFilter = "SubTestName='WBC' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=7 and DefaultUnit is not null";
                lblWBC.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["WBC"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtWBC.Attributes.Add("onkeyup", "chkDecimal('" + txtWBC.ClientID + "'); AddBoundary('" + txtWBC.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtWBC.Attributes.Add("onblur", "CheckValue('" + txtWBC.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtWBC.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblDiffNeut.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffNeut' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=82 and DefaultUnit is not null";
                lblDiffNeut.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffNeut"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    neut1.Attributes.Add("onkeyup", "chkDecimal('" + neut1.ClientID + "'); AddBoundary('" + neut1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    neut1.Attributes.Add("onblur", "CheckValue('" + neut1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + neut1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblDiffneut2.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffNeutperc' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=83 and DefaultUnit is not null";
                lblDiffneut2.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffNeutperc"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    neut2.Attributes.Add("onkeyup", "chkDecimal('" + neut2.ClientID + "'); AddBoundary('" + neut2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    neut2.Attributes.Add("onblur", "CheckValue('" + neut2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + neut2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblDiffLymph.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffLymph' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=84 and DefaultUnit is not null";
                lblDiffLymph.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffLymph"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    lymph1.Attributes.Add("onkeyup", "chkDecimal('" + lymph1.ClientID + "'); AddBoundary('" + lymph1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    lymph1.Attributes.Add("onblur", "CheckValue('" + lymph1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + lymph1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblLymphPerc.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffLymphperc' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=85 and DefaultUnit is not null";
                lblLymphPerc.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffLymphperc"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    lymph2.Attributes.Add("onkeyup", "chkDecimal('" + lymph2.ClientID + "'); AddBoundary('" + lymph2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    lymph2.Attributes.Add("onblur", "CheckValue('" + lymph2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + lymph2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblDiffMono.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffMono' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=86 and DefaultUnit is not null";
                lblDiffMono.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffMono"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    mono1.Attributes.Add("onkeyup", "chkDecimal('" + mono1.ClientID + "'); AddBoundary('" + mono1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    mono1.Attributes.Add("onblur", "CheckValue('" + mono1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + mono1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            if (lblDiffMono2.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffMonoperc' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=87 and DefaultUnit is not null";
                lblDiffMono2.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffMonoperc"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    mono2.Attributes.Add("onkeyup", "chkDecimal('" + mono2.ClientID + "'); AddBoundary('" + mono2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    mono2.Attributes.Add("onblur", "CheckValue('" + mono2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + mono2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblDiffEosin.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffEosin' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=88 and DefaultUnit is not null";
                lblDiffEosin.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffEosin"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    eosin1.Attributes.Add("onkeyup", "chkDecimal('" + eosin1.ClientID + "'); AddBoundary('" + eosin1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    eosin1.Attributes.Add("onblur", "CheckValue('" + eosin1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + eosin1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblDiffEosinPerc.Text == "")
            {
                //theDV.RowFilter = "SubTestName='DiffEosinperc' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=89 and DefaultUnit is not null";
                lblDiffEosinPerc.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["DiffEosinperc"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    eosin2.Attributes.Add("onkeyup", "chkDecimal('" + eosin2.ClientID + "'); AddBoundary('" + eosin2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    eosin2.Attributes.Add("onblur", "CheckValue('" + eosin2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + eosin2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblPlatelets.Text == "")
            {
                //theDV.RowFilter = "SubTestName='Platelets' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=9 and DefaultUnit is not null";
                lblPlatelets.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["Platelets"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtPlatelets.Attributes.Add("onkeyup", "chkDecimal('" + txtPlatelets.ClientID + "'); AddBoundary('" + txtPlatelets.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtPlatelets.Attributes.Add("onblur", "CheckValue('" + txtPlatelets.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtPlatelets.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            //if (lblESR2.Text != "")
            //{
            //    //theDV.RowFilter = "SubTestName='ESR' and DefaultUnit is not null";
            //    theDV.RowFilter = "SubTestId=57 and DefaultUnit is not null";
            //    lblESR2.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
            //    HTUnitId["ESR"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
            //    if (theDV.Count != 0)
            //    {
            //       TxtESR.Attributes.Add("onkeyup", "chkDecimal('" + TxtESR.ClientID + "'); AddBoundary('" + TxtESR.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            //      TxtESR.Attributes.Add("onblur", "CheckValue('" + TxtESR.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + TxtESR.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            //    }
            //}

            if (lblASTSGOT.Text == "")
            {
                //theDV.RowFilter = "SubTestName='AST' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=10 and DefaultUnit is not null";
                lblASTSGOT.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["AST"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtAST.Attributes.Add("onkeyup", "chkDecimal('" + txtAST.ClientID + "'); AddBoundary('" + txtAST.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtAST.Attributes.Add("onblur", "CheckValue('" + txtAST.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtAST.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblAltSpgt.Text == "")
            {
                //theDV.RowFilter = "SubTestName='ALT' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=11 and DefaultUnit is not null";
                lblAltSpgt.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["ALT"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtALT.Attributes.Add("onkeyup", "chkDecimal('" + txtALT.ClientID + "'); AddBoundary('" + txtALT.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtALT.Attributes.Add("onblur", "CheckValue('" + txtALT.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtALT.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblCreatinine.Text == "")
            {
                //theDV.RowFilter = "SubTestName='Creatininemg' and DefaultUnit is not null"; //set it according to the database as the name is there.
                theDV.RowFilter = "SubTestId=12 and DefaultUnit is not null";
                lblCreatinine.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["Creatininemg"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtCreatinine.Attributes.Add("onkeyup", "chkDecimal('" + txtCreatinine.ClientID + "'); AddBoundary('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtCreatinine.Attributes.Add("onblur", "CheckValue('" + txtCreatinine.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtCreatinine.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblAmylase.Text == "")
            {
                //theDV.RowFilter = "SubTestName='Amylase' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=13 and DefaultUnit is not null";
                lblAmylase.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["Amylase"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtAmylase.Attributes.Add("onkeyup", "chkDecimal('" + txtAmylase.ClientID + "'); AddBoundary('" + txtAmylase.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtAmylase.Attributes.Add("onblur", "CheckValue('" + txtAmylase.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtAmylase.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblCellCountRBC.Text == "")
            {
                //theDV.RowFilter = "SubTestName='CCRbcs' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=28 and DefaultUnit is not null";
                lblCellCountRBC.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["CCRbcs"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtRBCs.Attributes.Add("onkeyup", "chkNumeric('" + txtRBCs.ClientID + "'); AddBoundary('" + txtRBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtRBCs.Attributes.Add("onblur", "CheckValue('" + txtRBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtRBCs.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblCellCountWBC.Text == "")
            {
                //theDV.RowFilter = "SubTestName='CCWBCs' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=29 and DefaultUnit is not null";
                lblCellCountWBC.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["CCWBCs"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtWBCs.Attributes.Add("onkeyup", "chkNumeric('" + txtWBCs.ClientID + "'); AddBoundary('" + txtWBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtWBCs.Attributes.Add("onblur", "CheckValue('" + txtWBCs.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtWBCs.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblCellCountNeutrophils.Text == "")
            {
                //theDV.RowFilter = "SubTestName='CCNeutrophilis' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=30 and DefaultUnit is not null";
                lblCellCountNeutrophils.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["CCNeutrophilis"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtNeutrophils.Attributes.Add("onkeyup", "chkNumeric('" + txtNeutrophils.ClientID + "'); AddBoundary('" + txtNeutrophils.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtNeutrophils.Attributes.Add("onblur", "CheckValue('" + txtNeutrophils.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtNeutrophils.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblCellCountLymphocytes.Text == "")
            {
                //theDV.RowFilter = "SubTestName='CCLymphocytes' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=31 and DefaultUnit is not null";
                lblCellCountLymphocytes.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["CCLymphocytes"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtLymphocytes.Attributes.Add("onkeyup", "chkNumeric('" + txtLymphocytes.ClientID + "'); AddBoundary('" + txtLymphocytes.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtLymphocytes.Attributes.Add("onblur", "CheckValue('" + txtLymphocytes.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtLymphocytes.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblGlucose.Text == "")
            {
                //theDV.RowFilter = "SubTestName='Glucose' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=54 and DefaultUnit is not null";
                lblGlucose.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["Glucose"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtGlucose.Attributes.Add("onkeyup", "chkDecimal('" + txtGlucose.ClientID + "');  AddBoundary('" + txtGlucose.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtGlucose.Attributes.Add("onblur", "CheckValue('" + txtGlucose.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtGlucose.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblProtein.Text == "")
            {
                //theDV.RowFilter = "SubTestName='Protein' and DefaultUnit is not null";
                theDV.RowFilter = "SubTestId=32 and DefaultUnit is not null";
                lblProtein.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["Protein"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtProtein.Attributes.Add("onkeyup", "chkDecimal('" + txtProtein.ClientID + "');  AddBoundary('" + txtProtein.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtProtein.Attributes.Add("onblur", "CheckValue('" + txtProtein.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtProtein.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisSpecGrav.Text == "")
            {
                theDV.RowFilter = "SubTestId=90 and DefaultUnit is not null";
                lblUrinalysisSpecGrav.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisSpecGrav"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis1.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis1.ClientID + "'); AddBoundary('" + txtUrinalysis1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis1.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis1.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis1.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisGlucose.Text == "")
            {
                theDV.RowFilter = "SubTestId=91 and DefaultUnit is not null";
                lblUrinalysisGlucose.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisGlucose"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis2.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis2.ClientID + "'); AddBoundary('" + txtUrinalysis2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis2.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis2.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis2.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisKetone.Text == "")
            {
                theDV.RowFilter = "SubTestId=92 and DefaultUnit is not null";
                lblUrinalysisKetone.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisKetone"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis3.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis3.ClientID + "'); AddBoundary('" + txtUrinalysis3.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis3.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis3.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis3.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisProtein.Text == "")
            {
                theDV.RowFilter = "SubTestId=93 and DefaultUnit is not null";
                lblUrinalysisProtein.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisProtein"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis4.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis4.ClientID + "'); AddBoundary('" + txtUrinalysis4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis4.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis4.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis4.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisLeukEst.Text == "")
            {
                theDV.RowFilter = "SubTestId=94 and DefaultUnit is not null";
                lblUrinalysisLeukEst.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisLeukEst"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis5.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis5.ClientID + "'); AddBoundary('" + txtUrinalysis5.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis5.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis5.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis5.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisNitrate.Text == "")
            {
                theDV.RowFilter = "SubTestId=95 and DefaultUnit is not null";
                lblUrinalysisNitrate.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisNitrate"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis6.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis6.ClientID + "'); AddBoundary('" + txtUrinalysis6.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis6.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis6.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis6.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }
            if (lblUrinalysisBlood.Text == "")
            {
                theDV.RowFilter = "SubTestId=96 and DefaultUnit is not null";
                lblUrinalysisBlood.Text = (theDV.Count != 0) ? theDV[0]["UnitName"].ToString() : "";
                HTUnitId["UrinalysisBlood"] = (theDV.Count != 0) ? theDV[0]["UnitId"].ToString() : "";
                if (theDV.Count != 0)
                {
                    txtUrinalysis7.Attributes.Add("onkeyup", "chkNumeric('" + txtUrinalysis7.ClientID + "'); AddBoundary('" + txtUrinalysis7.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    txtUrinalysis7.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis7.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('" + txtUrinalysis7.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                }
            }

            ViewState["HTUnitId"] = HTUnitId;
            //AddFieldAttributes((Hashtable)ViewState["HTUnitId"]);
        }

        private void Init_Form()
        {
            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

            theDSLabs = LabResultManager.GetLabValues(); //pr_Laboratory_GetLabValues_constella

            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataSet theDSXML1 = new DataSet();
            theDSXML1.ReadXml(MapPath("..\\XMLFiles\\LabMasters.con"));
            DataView theDVLabPeriod = new DataView(theDSXML1.Tables["mst_PatientLabPeriod"]);
            theDVLabPeriod.RowFilter = "DeleteFlag=0";
            DataTable theDTLabPeriod = theUtils.CreateTableFromDataView(theDVLabPeriod);
            BindManager.BindCombo(ddLabPeriodDone, theDTLabPeriod, "Name", "Id");

            if (Convert.ToInt32(ViewState["LabId"]) == 0)
            {
                if (theDSXML.Tables["Mst_Employee"] != null)
                {
                    DataView theDVLabOrdered = new DataView(theDSXML.Tables["Mst_Employee"]);
                    theDVLabOrdered.RowFilter = "DeleteFlag=0";
                    if (theDVLabOrdered.Table != null)
                    {
                        DataTable theDTLabOrdered = (DataTable)theUtils.CreateTableFromDataView(theDVLabOrdered);
                        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                        {
                            theDVLabOrdered = new DataView(theDTLabOrdered);
                            theDVLabOrdered.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                            if (theDVLabOrdered.Count > 0)
                                theDTLabOrdered = theUtils.CreateTableFromDataView(theDVLabOrdered);
                        }

                        BindManager.BindCombo(ddLabOrderedbyName, theDTLabOrdered, "EmployeeName", "EmployeeId");
                        theDTLabReported = theDTLabOrdered;
                        BindManager.BindCombo(ddlLabReportedbyName, theDTLabReported, "EmployeeName", "EmployeeId");
                        theDVLabOrdered.Dispose();
                        theDTLabOrdered.Clear();
                    }
                }

                //if (theDSXML.Tables["Mst_Employee"] != null)
                //{
                //    //DataTable theMaster_1 = (DataTable)Application["AppEmployee"];
                //    //DataView theDVLabOrdered = new DataView(theMaster_1);
                //    DataView theDVLabOrdered = new DataView(theDSXML.Tables["Mst_Employee"]);
                //    theDVLabOrdered.RowFilter = "DeleteFlag=0";
                //    DataTable theDTLabOrdered = theUtils.CreateTableFromDataView(theDVLabOrdered);
                //    BindManager.BindCombo(ddLabOrderedbyName, theDTLabOrdered, "EmployeeName", "EmployeeId");
                //    DataTable theDTLabReported = theDTLabOrdered;
                //    BindManager.BindCombo(ddlLabReportedbyName, theDTLabReported, "EmployeeName", "EmployeeId");
                //    theDVLabOrdered.Dispose();
                //    //theDTLabOrdered.Clear();
                //}
            }
            //else
            //{
            //    pr_Admin_GetEmployeeDetails_Constella

            if (theDSXML.Tables["Mst_Employee"] != null)
            {
                DataView theDVLabOrdered = new DataView(theDSXML.Tables["Mst_Employee"]);
                theDVLabOrdered.RowFilter = "DeleteFlag=0";
                if (theDVLabOrdered.Table != null)
                {
                    DataTable theDTLabOrdered = (DataTable)theUtils.CreateTableFromDataView(theDVLabOrdered);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDVLabOrdered = new DataView(theDTLabOrdered);
                        theDVLabOrdered.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                        if (theDVLabOrdered.Count > 0)
                            theDTLabOrdered = theUtils.CreateTableFromDataView(theDVLabOrdered);
                    }

                    //BindManager.BindCombo(ddLabOrderedbyName, theDTLabOrdered, "EmployeeName", "EmployeeId");
                    theDTLabReported = theDTLabOrdered;
                    BindManager.BindCombo(ddlLabReportedbyName, theDTLabReported, "EmployeeName", "EmployeeId");
                    theDVLabOrdered.Dispose();
                    theDTLabOrdered.Clear();
                }
            }
            //    if (theDSXML.Tables["Mst_Employee"] != null)
            //    {
            //        // DataTable theMaster_1 = (DataTable)Application["AppEmployee"];
            //        //DataTable theDTLabOrdered = theMaster_1.Copy();
            //        //BindManager.BindCombo(ddLabOrderedbyName, theDTLabOrdered, "EmployeeName", "EmployeeId");

            //        BindManager.BindCombo(ddLabOrderedbyName, theDSXML.Tables["Mst_Employee"], "EmployeeName", "EmployeeId");
            //        //DataTable theDTLabReported = theDTLabOrdered;
            //        BindManager.BindCombo(ddlLabReportedbyName, theDTLabReported, "EmployeeName", "EmployeeId");
            //        BindManager.BindCombo(ddlLabReportedbyName, theDSXML.Tables["Mst_Employee"], "EmployeeName", "EmployeeId");

            //    }

            //}

            #region "Bind Urine Controls"
            ////Microscopic Blood////////
            DataTable theDT = theDSLabs.Tables[1];
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "SubTestId = 97";
            theDT = theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(urineblood, theDT, "Result", "ResultId");

            ///Microscopic WBC//////
            theDT = theDSLabs.Tables[1];
            theDV = new DataView(theDT);
            theDV.RowFilter = "SubTestId = 98";
            theDT = theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(urineWBC, theDT, "Result", "ResultId");

            ////Microscopic Bacteria//////
            theDT = theDSLabs.Tables[1];
            theDV = new DataView(theDT);
            theDV.RowFilter = "SubTestId = 99";
            theDT = theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(urineBact, theDT, "Result", "ResultId");

            ////Microscopic Cast /////////
            theDT = theDSLabs.Tables[1];
            theDV = new DataView(theDT);
            theDV.RowFilter = "SubTestId = 100";
            theDT = theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(urineCasts, theDT, "Result", "ResultId");

            #endregion "Bind Urine Controls"

            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);

            DataSet theDSPatient = LabResultManager.GetPatientInfo(PatientID.ToString());//pr_Laboratory_GetPatientInfo_Constella

            //DataSet objPatientStatus = new DataSet();
            //objPatientStatus = LabResultManager.GetPatientRecordformStatus(PatientID);
            //if (objPatientStatus != null)
            //{
            //    if (objPatientStatus.Tables[0].Rows.Count > 0)
            //    {
            //        string ARTStatus = string.Empty;
            //        if (objPatientStatus.Tables[2].Rows[0]["ModuleId"].ToString() == "2")
            //        {
            //            ARTStatus = objPatientStatus.Tables[0].Rows[0]["status"].ToString();

            //            if (ARTStatus == "ART" || ARTStatus == "Due for Termination" || ARTStatus == "Non-ART" || ARTStatus == "PMTCT" || ARTStatus == "")
            //            {
            //                btnsave.Enabled = true;
            //            }
            //            else
            //            {
            //                btnsave.Enabled = false;
            //            }
            //        }
            //        if (objPatientStatus.Tables[2].Rows[0]["ModuleId"].ToString() == "1")
            //        {
            //            ARTStatus = objPatientStatus.Tables[1].Rows[0]["status"].ToString();

            //            if (ARTStatus == "ART" || ARTStatus == "Due for Termination" || ARTStatus == "Non-ART" || ARTStatus == "PMTCT" || ARTStatus == "")
            //            {
            //                btnsave.Enabled = true;
            //            }
            //            else
            //            {
            //                btnsave.Enabled = false;
            //            }

            //        }
            //    }
            //}

            ////if (theDSPatient.Tables[0].Rows.Count > 0)
            ////{
            ////    this.lblPatientName.Text = theDSPatient.Tables[0].Rows[0][0].ToString();
            ////    this.lblpatientenrol1.Text = theDSPatient.Tables[0].Rows[0]["PatientId"].ToString();
            ////    this.lblExisclinicID1.Text = theDSPatient.Tables[0].Rows[0][2].ToString();
            ////}
            if (Convert.ToInt32(theDSPatient.Tables[0].Rows[0]["Sex"].ToString()) == 17)
            {
                lblpreg.Visible = true;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "PregSpan", "document.getElementById('" + lblpreg.ClientID + "').style.display = \"\";", true);
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "PregSpan", "document.getElementById('PregSpan').disabled=false;", true);
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "PregSpan", "document.getElementById('" + lblpreg.ClientID + "').style.display = \"none\";", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "PregSpan", "document.getElementById('" + lblpreg.ClientID + "').disabled=false;", true);
                lblpreg.Visible = false;
            }

            PnlLab.Controls.Clear();
            //lblSatelliteId.Text = Convert.ToString(Session["AppSatelliteId"]);

            if (Convert.ToInt32(ViewState["LabId"]) > 0)
            {
                OrderId = Convert.ToInt32(ViewState["LabId"]);
                ViewState["LabID"] = Convert.ToInt32(ViewState["LabId"]);
                //Pr_Laboratory_GetLabResults_Constella -- gets selected unit also from dtl_PatientLabResults
                theExistDS = LabResultManager.GetPatientLab(OrderId.ToString());
                int C = theExistDS.Tables[0].Rows.Count;
                ViewState["OldDS"] = theExistDS;

                #region "FillCustomControls"
                theAddLabs = new DataTable();
                theAddLabs.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
                theAddLabs.Columns.Add("LabName", System.Type.GetType("System.String"));
                theAddLabs.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
                theAddLabs.Columns.Add("SubTestName", System.Type.GetType("System.String"));
                theAddLabs.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
                theAddLabs.Constraints.Add("Con1", theAddLabs.Columns["SubTestID"], true);

                foreach (DataRow thedr in theExistDS.Tables[0].Rows)
                {
                    if (Convert.ToInt32(thedr["LabTypeId"]) == 1)
                    {
                        DataRow tmpDR = theAddLabs.NewRow();
                        tmpDR[0] = thedr["LabTestId"];
                        tmpDR[1] = thedr["LabName"];
                        tmpDR[2] = thedr["SubTestId"];
                        tmpDR[3] = thedr["SubTestName"];
                        tmpDR[4] = thedr["LabTypeId"];
                        theAddLabs.Rows.Add(tmpDR);
                        BindCustomControls(thedr);
                    }
                }
                foreach (DataRow dr in theExistDS.Tables[0].Rows)
                {
                    FillOldData(PnlLab, dr);
                    //FillOldData(PatientID);
                }
                #endregion "FillCustomControls"

                LoadPatientLabOrder(theExistDS);// unit names also
                DataTable dataTableLabTest = theExistDS.Tables[0].Copy();
                DataTable dtUniqueTests = new DataTable();
                string[] TobeDistinct = { "LabName", "SubTestName", "UnitName" };
                dtUniqueTests = dataTableLabTest.DefaultView.ToTable(true, TobeDistinct);
                panelOrderedLabs.Visible = false;
                if (dtUniqueTests.Rows.Count > 0)
                {
                    rptLabTest.DataSource = dtUniqueTests;
                    rptLabTest.DataBind();
                    panelOrderedLabs.Visible = true;
                }
            }
        }

        private void LoadAdditionalLabs(DataTable theDT, Panel thePanel)
        {
            if (theDT != null)
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                    BindCustomControls(theDR);
                }
            }
        }

        private void LoadPatientLabOrder(DataSet theDS)
        {
            //Load the Lab results for an existing lab order from the database
            if (Convert.ToInt32(ViewState["LabId"]) > 0)
            {
                DataSet theDSDetails = theDS;
                Hashtable HTUnitId = new Hashtable();
                if (ViewState["HTUnitId"] != null)
                    HTUnitId = (Hashtable)ViewState["HTUnitId"];

                for (int i = 0; i < theDSDetails.Tables[0].Rows.Count; i++)
                {
                    switch (theDSDetails.Tables[0].Rows[i]["SubTestId"].ToString())
                    {
                        //CSF Culture
                        case "26":

                            if (theDSDetails.Tables[0].Rows[i]["TestResults1"] != System.DBNull.Value)
                            {
                                csfCulture.Text = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            }
                            break;
                        //Stool Description
                        case "105":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults1"] != System.DBNull.Value)
                            {
                                stooldesc.Value = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            }
                            break;

                        //Culture/Sensitivity
                        case "22":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults1"] != System.DBNull.Value)
                            {
                                txtcultureSensitivity.Value = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            }
                            break;
                        //CD4
                        case "1":
                            //--txtCD4.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtCD4.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                labelCD4.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["CD4"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtCD4.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4.ClientID + "'); AddBoundary('" + txtCD4.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtCD4.Attributes.Add("onblur", "CheckValue('" + txtCD4.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtCD4.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //HIV Serology Confirmatory
                        case "101":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    HIVSerologyConfirm.Checked = true;
                            }
                            break;
                        //CD4 Percentage
                        case "2":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtCD4perc.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCD4Perc.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["CD4Percent"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtCD4perc.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4perc.ClientID + "'); AddBoundary('" + txtCD4perc.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtCD4perc.Attributes.Add("onblur", "CheckValue('" + txtCD4perc.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtCD4perc.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            //--txtCD4perc.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            break;
                        //Gram stain
                        case "20":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults1"] != System.DBNull.Value)
                            {
                                txtgramStain.Value = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            }
                            break;
                        //Viral Load
                        case "3":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtViralLoad.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblViralLoad.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["ViralLoad"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtViralLoad.Attributes.Add("onkeyup", "chkDecimal('" + txtViralLoad.ClientID + "'); AddBoundary('" + txtViralLoad.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtViralLoad.Attributes.Add("onblur", "CheckValue('" + txtViralLoad.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtViralLoad.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;

                        //Store Plasma
                        case "4":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    storePlasma.Checked = true;
                            }
                            break;
                        //HB
                        case "6":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtHB.Text = theDSDetails.Tables[0].Rows[i]["TestResults"].ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblHb.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Hct/Hb"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtHB.Attributes.Add("onkeyup", "chkDecimal('" + txtHB.ClientID + "'); AddBoundary('" + txtHB.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtHB.Attributes.Add("onblur", "CheckValue('" + txtHB.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtHB.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //HCT
                        case "5":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtHCT.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblHCTPercent.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["HCTPercent"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtHCT.Attributes.Add("onkeyup", "chkDecimal('" + txtHCT.ClientID + "'); AddBoundary('" + txtHCT.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtHCT.Attributes.Add("onblur", "CheckValue('" + txtHCT.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtHCT.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //WBC
                        case "7":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtWBC.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblWBC.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["WBC"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();

                                txtWBC.Attributes.Add("onkeyup", "chkDecimal('" + txtWBC.ClientID + "'); AddBoundary('" + txtWBC.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtWBC.Attributes.Add("onblur", "CheckValue('" + txtWBC.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtWBC.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //Platelets
                        case "9":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtPlatelets.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblPlatelets.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Platelets"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtPlatelets.Attributes.Add("onkeyup", "chkDecimal('" + txtPlatelets.ClientID + "'); AddBoundary('" + txtPlatelets.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtPlatelets.Attributes.Add("onblur", "CheckValue('" + txtPlatelets.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtPlatelets.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;

                        case "57":

                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                TxtESR.Text = theDSDetails.Tables[0].Rows[i]["TestResults"].ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblESR2.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["ESR"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                TxtESR.Attributes.Add("onkeyup", "chkDecimal('" + TxtESR.ClientID + "'); AddBoundary('" + TxtESR.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                TxtESR.Attributes.Add("onblur", "CheckValue('" + TxtESR.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + TxtESR.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;

                        //AST/SGOT
                        case "10":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtAST.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblASTSGOT.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["AST"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtAST.Attributes.Add("onkeyup", "chkDecimal('" + txtAST.ClientID + "'); AddBoundary('" + txtAST.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtAST.Attributes.Add("onblur", "CheckValue('" + txtAST.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtAST.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //ALT/SGPT
                        case "11":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtALT.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblAltSpgt.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["ALT"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtALT.Attributes.Add("onkeyup", "chkDecimal('" + txtALT.ClientID + "'); AddBoundary('" + txtALT.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtALT.Attributes.Add("onblur", "CheckValue('" + txtALT.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtALT.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //Creatinine mg/dL
                        case "12":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtCreatinine.Text = theDSDetails.Tables[0].Rows[i]["TestResults"].ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCreatinine.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Creatininemg"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtCreatinine.Attributes.Add("onkeyup", "chkDecimal('" + txtCreatinine.ClientID + "'); AddBoundary('" + txtCreatinine.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtCreatinine.Attributes.Add("onblur", "CheckValue('" + txtCreatinine.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtCreatinine.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            unitsCreatinine.SelectedValue = "12";
                            break;
                        //Creatinine nmole/dL
                        case "106":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtCreatinine.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCreatinine.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Creatininemg"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtCreatinine.Attributes.Add("onkeyup", "chkDecimal('" + txtCreatinine.ClientID + "'); AddBoundary('" + txtCreatinine.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtCreatinine.Attributes.Add("onblur", "CheckValue('" + txtCreatinine.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtCreatinine.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            unitsCreatinine.SelectedValue = "106";
                            break;
                        //Amylase
                        case "13":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtAmylase.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblAmylase.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Amylase"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtAmylase.Attributes.Add("onkeyup", "chkDecimal('" + txtAmylase.ClientID + "'); AddBoundary('" + txtAmylase.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtAmylase.Attributes.Add("onblur", "CheckValue('" + txtAmylase.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtAmylase.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //Pregnancy
                        case "14":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdoclPreg.Checked = true;
                                else
                                    rdoclPreg2.Checked = true;
                            }
                            break;
                        //Malaria
                        case "15":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdochkMalaria.Checked = true;
                                else
                                    rdochkMalaria2.Checked = true;
                            }
                            break;
                        //Serum Crypto
                        case "16":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdorbSerum1.Checked = true;
                                else
                                    rdorbSerum2.Checked = true;
                            }
                            break;
                        //CSF Crypto Ag
                        case "23":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdocblSerumCrypto1.Checked = true;
                                else
                                    rdocblSerumCrypto2.Checked = true;
                            }
                            break;
                        //CSF Crypto India Link
                        case "24":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdocsfIndiaInk1.Checked = true;
                                else
                                    rdocsfIndiaInk2.Checked = true;
                            }
                            break;
                        //CSF Crypto Gram Stain
                        case "25":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdocsfgramstain1.Checked = true;
                                else
                                    rdocsfgramstain2.Checked = true;
                            }
                            break;

                        /////////////----glucose -54 and protein 32 New
                        case "54":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtGlucose.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblGlucose.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Glucose"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtGlucose.Attributes.Add("onkeyup", "chkDecimal('" + txtGlucose.ClientID + "'); AddBoundary('" + txtGlucose.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtGlucose.Attributes.Add("onblur", "CheckValue('" + txtGlucose.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtGlucose.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;

                        case "32":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtProtein.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblProtein.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["Protein"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtProtein.Attributes.Add("onkeyup", "chkDecimal('" + txtProtein.ClientID + "'); AddBoundary('" + txtProtein.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtProtein.Attributes.Add("onblur", "CheckValue('" + txtProtein.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtProtein.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;

                        //////////--old before unit were default -Begin

                        ////Bio Chemistry Glucose mg/dL
                        //case "109":
                        //    txtGlucose.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    //--txtGlucose.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    unitsGlucose.SelectedValue = "109";
                        //    break;
                        ////Bio Chemistry Glucose nmole/dL
                        //case "110":
                        //    txtGlucose.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    //--txtGlucose.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    unitsGlucose.SelectedValue = "110";
                        //    break;

                        ////Bio Chemistry Protien mg/dL
                        //case "111":
                        //    txtProtein.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    //--txtProtein.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    unitsProtein.SelectedValue = "111";
                        //    break;
                        ////Bio Chemistry Protien nmole/dL
                        //case "112":
                        //    txtProtein.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    //--txtProtein.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                        //    unitsProtein.SelectedValue = "112";
                        //    break;
                        //////////--old before unit were default -End

                        /////Business Logic for Glucose yet to be defined/////
                        //////if (txtGlucose.Text != "")
                        //////{
                        //////    //Add Glucose
                        //////    drLab = dtLabs.NewRow();
                        //////    drLab["LabTestID"] = 54;
                        //////    drLab["LabParameterID"] = 0;
                        //////    drLab["TestResultss"] = Convert.ToInt32(txtGlucose.Text);
                        //////    dtLabs.Rows.Add(drLab);
                        //////}
                        //Stool O/P
                        case "55":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    rdoStoolList.Checked = true;
                                else
                                    rdoStoolList2.Checked = true;
                            }
                            break;
                        //CSF Cellcount RBC
                        case "28":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtRBCs.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCellCountRBC.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["CCRbcs"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtRBCs.Attributes.Add("onkeyup", "chkDecimal('" + txtRBCs.ClientID + "'); AddBoundary('" + txtRBCs.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtRBCs.Attributes.Add("onblur", "CheckValue('" + txtRBCs.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtRBCs.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //CSF Cellcount WBC
                        case "29":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtWBCs.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCellCountWBC.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["CCWBCs"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtWBCs.Attributes.Add("onkeyup", "chkDecimal('" + txtWBCs.ClientID + "'); AddBoundary('" + txtWBCs.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtWBCs.Attributes.Add("onblur", "CheckValue('" + txtWBCs.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtWBCs.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //CSF Cellcount Neutrophils
                        case "30":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtNeutrophils.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCellCountNeutrophils.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["CCNeutrophilis"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtNeutrophils.Attributes.Add("onkeyup", "chkDecimal('" + txtNeutrophils.ClientID + "'); AddBoundary('" + txtNeutrophils.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtNeutrophils.Attributes.Add("onblur", "CheckValue('" + txtNeutrophils.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtNeutrophils.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //CSF Cellcount Lymphocytes
                        case "31":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtLymphocytes.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblCellCountLymphocytes.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["CCLymphocytes"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtLymphocytes.Attributes.Add("onkeyup", "chkDecimal('" + txtLymphocytes.ClientID + "'); AddBoundary('" + txtLymphocytes.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtLymphocytes.Attributes.Add("onblur", "CheckValue('" + txtLymphocytes.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtLymphocytes.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;

                        //Add Protein   ----- Business Logic yet to be defined
                        ////if (txtProtein.Text != "")
                        ////{
                        ////    drLab = dtLabs.NewRow();
                        ////    drLab["LabTestID"] = 32;
                        ////    drLab["LabParameterID"] = 1;
                        ////    drLab["TestResultss"] = txtProtein.Text;
                        ////    drLab["LabUnits"] = Convert.ToInt32(unitsProtein.SelectedValue);
                        ////    dtLabs.Rows.Add(drLab);
                        ////}
                        //AFB #1
                        case "17":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    sputumafb1.Checked = true;
                                else
                                    sputumafb12.Checked = true;
                            }
                            break;
                        //AFB #2
                        case "18":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    sputumafb21.Checked = true;
                                else
                                    sputumafb22.Checked = true;
                            }
                            break;
                        //AFB #3
                        case "19":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    sputumafb3.Checked = true;
                                else
                                    sputumafb31.Checked = true;
                            }
                            break;
                        //Chest X ray Normal
                        case "113":
                            //rdochestxray1.Checked = true;
                            //if (theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString() != "")
                            //{
                            //  xraytxt.Value = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            //}
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                {
                                    rdochestxray1.Checked = true;
                                }
                                else
                                {
                                    rdochestxray2.Checked = true;
                                }
                            }
                            break;
                        //Chest X ray Abnormal
                        case "104":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults1"] != System.DBNull.Value)
                            {
                                xraytxt.Value = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            }
                            //rdochestxray2.Checked = true;
                            //if (theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString() != "")
                            //{
                            //    xraytxt.Value = theDSDetails.Tables[0].Rows[i]["TestResults1"].ToString();
                            //}
                            break;
                        //HIV Serology Pos /Neg
                        case "53":
                            //HIV_Rapid_Test.ClearSelection();
                            cblSerologyNeg.Checked = cblSerologyPos.Checked = false;
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                {
                                    //HIV_Rapid_Test.SelectedIndex = 0;
                                    cblSerologyPos.Checked = true;
                                }
                                else
                                {
                                    //HIV_Rapid_Test.SelectedIndex = 1;
                                    cblSerologyNeg.Checked = true;
                                }
                            }
                            break;

                        //------- Rupesh 08May----
                        //PCR Results Pos /Neg
                        case "114":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    cblPCRResultsPos.Checked = true;
                                else
                                    cblPCRResultsNeg.Checked = true;
                            }
                            break;

                        //----Meetu Begin 16 Sep 2009

                        //RPR/VDRL Pos/Neg

                        case "74":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    cblFTAPos.Checked = true;
                                else
                                    cblFTANeg.Checked = true;

                            }
                            break;
                        // FTA Pos/Neg

                        case "75":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) == 1)
                                    cblRPRVDRLPos.Checked = true;
                                else
                                    cblRPRVDRLNeg.Checked = true;
                            }
                            break;
                        //    //----Meetu End -----//

                        //Viral Load Undectable
                        case "107":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                if (Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]) != 0)
                                {
                                    string script = "<script language = 'javascript' defer ='defer' id = 'VLDetectable'>\n";
                                    script += "document.getElementById('" + ChkViral.ClientID + "').click();\n";
                                    script += "</script>\n";
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "VLDetectable", script);
                                    txtViralLoadDet.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                                    txtViralLoadDet.Attributes.Add("onkeyup", "chkDecimal('" + txtViralLoadDet.ClientID + "'); AddBoundary('" + txtViralLoadDet.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                    txtViralLoadDet.Attributes.Add("onblur", "CheckValue('" + txtViralLoadDet.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtViralLoadDet.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                }
                            }
                            break;
                        //WBC Diff Neut
                        case "82":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                neut1.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"].ToString() != "")
                            {
                                lblDiffNeut.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffNeut"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                neut1.Attributes.Add("onkeyup", "chkDecimal('" + neut1.ClientID + "'); AddBoundary('" + neut1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                neut1.Attributes.Add("onblur", "CheckValue('" + neut1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + neut1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }

                            //--neut1.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            break;
                        //WBC Diff Neut Per
                        case "83":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                neut2.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblDiffneut2.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffNeutperc"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                neut2.Attributes.Add("onkeyup", "chkDecimal('" + neut2.ClientID + "'); AddBoundary('" + neut2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                neut2.Attributes.Add("onblur", "CheckValue('" + neut2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + neut2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }

                            break;
                        //WBC Diff Lymp
                        case "84":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                lymph1.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblDiffLymph.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffLymph"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                lymph1.Attributes.Add("onkeyup", "chkDecimal('" + lymph1.ClientID + "'); AddBoundary('" + lymph1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                lymph1.Attributes.Add("onblur", "CheckValue('" + lymph1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + lymph1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //WBC Diff Lymp Per
                        case "85":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                lymph2.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblLymphPerc.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffLymphperc"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                lymph2.Attributes.Add("onkeyup", "chkDecimal('" + lymph2.ClientID + "'); AddBoundary('" + lymph2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                lymph2.Attributes.Add("onblur", "CheckValue('" + lymph2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + lymph2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            //--lymph2.Text = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            break;
                        //WBC Diff Mono
                        case "86":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                mono1.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblDiffMono.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffMono"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                mono1.Attributes.Add("onkeyup", "chkDecimal('" + mono1.ClientID + "'); AddBoundary('" + mono1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                mono1.Attributes.Add("onblur", "CheckValue('" + mono1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + mono1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //WBC Diff Mono Per
                        case "87":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                mono2.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblDiffMono2.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffMonoperc"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                mono2.Attributes.Add("onkeyup", "chkDecimal('" + mono2.ClientID + "'); AddBoundary('" + mono2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                mono2.Attributes.Add("onblur", "CheckValue('" + mono2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + mono2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //WBC Diff Eosin
                        case "88":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                eosin1.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblDiffEosin.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffEosin"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                eosin1.Attributes.Add("onkeyup", "chkDecimal('" + eosin1.ClientID + "'); AddBoundary('" + eosin1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                eosin1.Attributes.Add("onblur", "CheckValue('" + eosin1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + eosin1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //WBC Diff Eosin Per
                        case "89":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                eosin2.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblDiffEosinPerc.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["DiffEosinperc"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                eosin2.Attributes.Add("onkeyup", "chkDecimal('" + eosin2.ClientID + "'); AddBoundary('" + eosin2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                eosin2.Attributes.Add("onblur", "CheckValue('" + eosin2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + eosin2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Grav
                        case "90":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis1.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisSpecGrav.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisSpecGrav"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis1.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis1.ClientID + "'); AddBoundary('" + txtUrinalysis1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis1.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis1.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Glucose
                        case "91":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis2.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisGlucose.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisGlucose"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis2.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis2.ClientID + "'); AddBoundary('" + txtUrinalysis2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis2.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis2.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Ketone
                        case "92":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis3.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisKetone.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisKetone"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis3.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis3.ClientID + "'); AddBoundary('" + txtUrinalysis3.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis3.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis3.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis3.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Protien
                        case "93":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis4.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisProtein.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisProtein"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis4.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis4.ClientID + "'); AddBoundary('" + txtUrinalysis4.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis4.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis4.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis4.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Leuk
                        case "94":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis5.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisLeukEst.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisLeukEst"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis5.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis5.ClientID + "'); AddBoundary('" + txtUrinalysis5.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis5.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis5.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis5.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Nitrate
                        case "95":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis6.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisNitrate.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisNitrate"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis6.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis6.ClientID + "'); AddBoundary('" + txtUrinalysis6.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis6.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis6.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis6.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Blood
                        case "96":
                            if (theDSDetails.Tables[0].Rows[i]["TestResults"] != System.DBNull.Value)
                            {
                                txtUrinalysis7.Text = Convert.ToDecimal(theDSDetails.Tables[0].Rows[i]["TestResults"]).ToString();
                            }
                            if (theDSDetails.Tables[0].Rows[i]["UnitName"] != System.DBNull.Value)
                            {
                                lblUrinalysisBlood.Text = theDSDetails.Tables[0].Rows[i]["UnitName"].ToString();
                                HTUnitId["UrinalysisBlood"] = theDSDetails.Tables[0].Rows[i]["Units"].ToString();
                                txtUrinalysis7.Attributes.Add("onkeyup", "chkDecimal('" + txtUrinalysis7.ClientID + "'); AddBoundary('" + txtUrinalysis7.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                                txtUrinalysis7.Attributes.Add("onblur", "CheckValue('" + txtUrinalysis7.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MinBoundaryValue"].ToString() + "');CheckValue2('" + txtUrinalysis7.ClientID + "','" + theDSDetails.Tables[0].Rows[i]["MaxBoundaryValue"].ToString() + "')");
                            }
                            break;
                        //UnineAlalysis Microscopic Blood
                        case "97":
                            if (theDSDetails.Tables[0].Rows[i]["TestResultId"] != System.DBNull.Value)
                            {
                                urineblood.SelectedValue = theDSDetails.Tables[0].Rows[i]["TestResultId"].ToString();
                            }
                            break;
                        //UnineAlalysis Microscopic WBC
                        case "98":
                            if (theDSDetails.Tables[0].Rows[i]["TestResultId"] != System.DBNull.Value)
                            {
                                urineWBC.SelectedValue = theDSDetails.Tables[0].Rows[i]["TestResultId"].ToString();
                            }
                            break;
                        //UnineAlalysis Microscopic Bacteria
                        case "99":
                            if (theDSDetails.Tables[0].Rows[i]["TestResultId"] != System.DBNull.Value)
                            {
                                urineBact.SelectedValue = theDSDetails.Tables[0].Rows[i]["TestResultId"].ToString();
                            }
                            break;
                        //UnineAlalysis Microscopic Cast
                        case "100":
                            if (theDSDetails.Tables[0].Rows[i]["TestResultId"] != System.DBNull.Value)
                            {
                                Int32 fID = Convert.ToInt32(theDSDetails.Tables[0].Rows[i]["TestResultId"].ToString());
                                for (int kk = 0; kk < urineCasts.Items.Count; kk++)
                                {
                                    if (Convert.ToInt32(urineCasts.Items[kk].Value) == fID)
                                    {
                                        urineCasts.Items[kk].Selected = true;
                                    }
                                }
                            }
                            //urineCasts.SelectedValue = theDSDetails.Tables[0].Rows[i]["TestResultId"].ToString();
                            break;
                    }
                }
                if (theDSDetails.Tables[0].Rows[0]["LabPeriod"] != System.DBNull.Value)
                {
                    ddLabPeriodDone.SelectedValue = theDSDetails.Tables[0].Rows[0]["LabPeriod"].ToString();
                }
                if (theDSDetails.Tables[0].Rows[0]["LabNumber"] != System.DBNull.Value)
                {
                    txtLabnumber.Text = theDSDetails.Tables[0].Rows[0]["LabNumber"].ToString();
                }
                if (theDSDetails.Tables[0].Rows[0]["OrderedByName"] != System.DBNull.Value)
                {
                    BindDropdownOrderBy(theExistDS.Tables[0].Rows[0]["OrderedByName"].ToString());
                    this.ddLabOrderedbyName.SelectedValue = theDSDetails.Tables[0].Rows[0]["OrderedByName"].ToString();
                }
                if (theDSDetails.Tables[0].Rows[0]["ReportedByName"] != System.DBNull.Value)
                {
                    BindDropdownReportedBy(theExistDS.Tables[0].Rows[0]["ReportedByName"].ToString());
                    this.ddlLabReportedbyName.SelectedValue = theDSDetails.Tables[0].Rows[0]["ReportedByName"].ToString();
                }

                if (theDSDetails.Tables[0].Rows[0]["ReportedByDate"] != System.DBNull.Value)
                {
                    this.txtlabReportedbyDate.Text = (((DateTime)theDSDetails.Tables[0].Rows[0]["ReportedByDate"]).ToString(Session["AppDateFormat"].ToString())).ToString();
                    if (txtlabReportedbyDate.Text == "01-Jan-1900")
                    {
                        txtlabReportedbyDate.Text = "";
                    }
                }
                if (theDSDetails.Tables[0].Rows[0]["OrderedByDate"] != System.DBNull.Value)
                {
                    this.txtLabOrderedbyDate.Text = (((DateTime)theDSDetails.Tables[0].Rows[0]["OrderedByDate"]).ToString(Session["AppDateFormat"].ToString())).ToString();
                }
                if (theDSDetails.Tables[0].Rows[0].IsNull("PreClinicLabDate") == false)
                    if (((DateTime)theDSDetails.Tables[0].Rows[0]["PreClinicLabDate"]).ToString(Session["AppDateFormat"].ToString()) != "01-Jan-1900")
                        this.txtLabtobeDone.Text = (((DateTime)theDSDetails.Tables[0].Rows[0]["PreClinicLabDate"]).ToString(Session["AppDateFormat"].ToString())).ToString();

                if (this.txtLabtobeDone.Text != "")
                {
                    this.preclinicLabs.Checked = true;
                }

                ViewState["HTUnitId"] = HTUnitId;
            }
        }

        private DataTable MakeLabTable(Control theContainer)
        {
            //This procedure creates a table with the additional labs that the user has selected so that they can be saved in the db
            DataTable theDT = new DataTable();
            if (ViewState["Data"] == null)
            {
                //theDT = CreateAdditionalLabsTable();
            }
            else
            {
                theDT = (DataTable)ViewState["Data"];
            }

            int LabId = 0;
            int theResultId = 0;
            int theFinanced = 1;

            DataRow theRow;
            foreach (Control y in theContainer.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    foreach (Control x in y.Controls)
                    {
                        if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        {
                            MakeLabTable(x);
                        }
                        else
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                            {
                                if (x.ID.StartsWith("LabNm"))
                                {
                                    LabId = Convert.ToInt32(x.ID.Substring(5));
                                }
                            }
                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                if (x.ID.StartsWith("LabResult"))
                                {
                                    theResultId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                }
                            }

                            if ((ViewState["LabPanel"].ToString() == "Additional Lab") && (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox)))
                            {
                                if (((CheckBox)x).Checked == true)
                                {
                                    theFinanced = 2;
                                }
                            }
                            if (LabId != 0 && theResultId != 0)
                            {
                                theRow = theDT.NewRow();
                                theRow["LabID"] = LabId;
                                theRow["Result"] = theResultId;
                                theRow["Financed"] = theFinanced;
                                theDT.Rows.Add(theRow);
                                LabId = 0;
                                theResultId = 0;
                                theFinanced = 0;
                            }
                        }
                    }
            }
            return theDT;
        }

        // Create Custom Controls
        // Creation Date : 16-Jan-2007
        // Amitava Sinha
        //Updated by Ajay on 6-Jul-2009
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.Laboratory));

                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "Lab");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }

                ViewState["CustomFieldsDS"] = theDS;
                pnlCustomList.Visible = true;

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        private DataTable ReadLabTable(Control theContainer)
        {
            ViewState["AdditionalLabRes"] = "False";
            // This procedure reads the the additional labs on the panel labs  into a datatable

            DataTable theDT = (DataTable)ViewState["SaveData"];

            int theSubTestId = 0;
            int theLabTestId = 0;
            string theResultId = string.Empty;
            int theFinanced = 2;

            DataRow theRow;
            foreach (Control y in theContainer.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    foreach (Control x in y.Controls)
                    {
                        if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        {
                            ReadLabTable(x);
                        }
                        else
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                            {
                                if (x.ID.StartsWith("theName"))
                                {
                                    theSubTestId = Convert.ToInt32(x.ID.Substring(7));
                                }
                            }
                            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                            {
                                if (x.ID.StartsWith("LabResult"))
                                {
                                    theResultId = ((TextBox)x).Text; //Convert.ToInt32(((TextBox)x).Text);
                                }
                            }
                            /////20june
                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                if (x.ID.StartsWith("ddlLabResult"))
                                {
                                    theResultId = ((DropDownList)x).SelectedValue.ToString(); //Convert.ToInt32(((TextBox)x).Text);
                                }
                            }
                            ////
                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                            {
                                if (x.ID.StartsWith("FinChk"))
                                {
                                    theFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                }
                            }

                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                            {
                                if (x.ID.StartsWith("lblTestId"))
                                {
                                    theLabTestId = Convert.ToInt32(x.ID.Substring(9));
                                }
                            }

                            if (theLabTestId != 0 && theSubTestId != 0 && theResultId != "" && theFinanced != 2)
                            {
                                ViewState["AdditionalLabRes"] = "True";
                                theRow = theDT.NewRow();
                                theRow["LabTestId"] = theLabTestId;
                                theRow["LabParameterId"] = theSubTestId;
                                //theRow["LabResult"] = 0;
                                theRow["LabResult"] = 99998888; // Rupesh 19Dec07
                                theRow["LabResult1"] = theResultId;
                                theRow["LabResultId"] = 0;
                                theRow["Financed"] = theFinanced;
                                theDT.Rows.Add(theRow);
                                theSubTestId = 0;
                                theLabTestId = 0;
                                theResultId = string.Empty;
                                theFinanced = 2;
                            }
                        }
                    }
            }

            return theDT;
        }

        //private void BindHeader()
        //{
        //   theFacilityDS = new DataSet();
        //    AuthenticationManager Authentication = new AuthenticationManager();
        //    IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");

        //    theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.Laboratory, 0);

        //    DataTable theDT = theFacilityDS.Tables[0];
        //    lblpatientenrol.InnerHtml = theDT.Rows[0]["Label"].ToString();
        //    lblExisclinicID.InnerHtml = theDT.Rows[1]["Label"].ToString();

        //}
        private string ShowCaution()
        {
            string strMsg = "";
            DataSet theDS = (DataSet)ViewState["LabRanges"];
            //LabMaster = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

            //DataSet theDS = LabMaster.GetLabValues();

            DataView theDV = new DataView(theDS.Tables[0]);
            theDV.RowFilter = "LabTestID = 11";
            //Check the lab range values
            if ((theDV.Count > 0) && (txtALT.Text != ""))
            {
                if ((Convert.ToDecimal(txtALT.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtALT.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + "ALT : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "LabTestID = 13";
            if ((theDV.Count > 0) && (txtAmylase.Text != ""))
            {
                if ((Convert.ToDecimal(txtAmylase.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtAmylase.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Amylase : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "LabTestID = 10";
            if ((theDV.Count > 0) && (txtAST.Text != ""))
            {
                if ((Convert.ToDecimal(txtAST.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtAST.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " AST : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }

            theDV.RowFilter = "SubTestId = 1";
            if ((theDV.Count > 0) && (txtCD4.Text != ""))
            {
                if (Convert.ToInt32(txtCD4.Text) < Convert.ToInt32(theDV[0]["MinNormalRange"]) || (Convert.ToInt32(txtCD4.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " CD4 : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 2";
            if ((theDV.Count > 0) && (txtCD4perc.Text != ""))
            {
                if ((Convert.ToInt32(txtCD4perc.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtCD4perc.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " CD4% : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 12";
            if ((theDV.Count > 0) && (txtCreatinine.Text != "") && (unitsCreatinine.SelectedValue == "12"))
            {
                if ((Convert.ToDecimal(txtCreatinine.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtCreatinine.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Creatinine : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 106";
            if ((theDV.Count > 0) && (txtCreatinine.Text != "") && (unitsCreatinine.SelectedValue == "106"))
            {
                if ((Convert.ToDecimal(txtCreatinine.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtCreatinine.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Creatinine : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 3";
            if ((theDV.Count > 0) && (txtViralLoad.Text != ""))
            {
                if ((Convert.ToInt32(txtViralLoad.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtViralLoad.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Viral :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 6";
            if ((theDV.Count > 0) && (txtHB.Text != ""))
            {
                if ((Convert.ToDecimal(txtHB.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtHB.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Hb :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 5";
            if ((theDV.Count > 0) && (txtHCT.Text != ""))
            {
                if ((Convert.ToInt32(txtHCT.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtHCT.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " HCT : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 7";
            if ((theDV.Count > 0) && (txtWBC.Text != ""))
            {
                if ((Convert.ToDecimal(txtWBC.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtWBC.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " WBC :" + theDV[0]["MinNormalRange"].ToString() + " and " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 9";

            if ((theDV.Count > 0) && (txtPlatelets.Text != ""))
            {
                if ((Convert.ToInt32(txtPlatelets.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtPlatelets.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Platelets :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 57";

            if ((theDV.Count > 0) && (TxtESR.Text != ""))
            {
                if ((Convert.ToInt32(TxtESR.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(TxtESR.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " ESR :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }

            theDV.RowFilter = "SubTestId = 28";
            if ((theDV.Count > 0) && (txtRBCs.Text != ""))
            {
                if ((Convert.ToInt32(txtRBCs.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtRBCs.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " RBCs : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 29";
            if ((theDV.Count > 0) && (txtWBCs.Text != ""))
            {
                if ((Convert.ToInt32(txtWBCs.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtWBCs.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " WBCs : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 30";
            if ((theDV.Count > 0) && (txtNeutrophils.Text != ""))
            {
                if ((Convert.ToInt32(txtNeutrophils.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtNeutrophils.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Neutrophils :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 31";
            if ((theDV.Count > 0) && (txtLymphocytes.Text != ""))
            {
                if ((Convert.ToInt32(txtLymphocytes.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtLymphocytes.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Lymphocytes : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 111";
            if ((theDV.Count > 0) && (txtProtein.Text != "") && (unitsProtein.SelectedValue == "111"))
            {
                if ((Convert.ToInt32(txtProtein.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtProtein.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Protein : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 112";
            if ((theDV.Count > 0) && (txtProtein.Text != "") && (unitsProtein.SelectedValue == "112"))
            {
                if ((Convert.ToInt32(txtProtein.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtProtein.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Protein : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 109";
            if ((theDV.Count > 0) && (txtGlucose.Text != "") && (unitsGlucose.SelectedValue == "109"))
            {
                if ((Convert.ToInt32(txtGlucose.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtGlucose.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Glucose : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 110";
            if ((theDV.Count > 0) && (txtGlucose.Text != "") && (unitsGlucose.SelectedValue == "110"))
            {
                if ((Convert.ToInt32(txtGlucose.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtGlucose.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Glucose : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 82";
            if ((theDV.Count > 0) && (neut1.Text != ""))
            {
                if ((Convert.ToInt32(neut1.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(neut1.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Diff Neut :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 83";
            if ((theDV.Count > 0) && (neut2.Text != ""))
            {
                if ((Convert.ToInt32(neut2.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(neut2.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Diff Neut% :" + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 84";
            if ((theDV.Count > 0) && (lymph1.Text != ""))
            {
                if ((Convert.ToInt32(lymph1.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(lymph1.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Lymph : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 85";
            if ((theDV.Count > 0) && (lymph2.Text != ""))
            {
                if ((Convert.ToInt32(lymph2.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(lymph2.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Lymph : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 86";
            if ((theDV.Count > 0) && (mono1.Text != ""))
            {
                if ((Convert.ToInt32(mono1.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(mono1.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Mono : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 87";
            if ((theDV.Count > 0) && (mono2.Text != ""))
            {
                if ((Convert.ToInt32(mono2.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(mono2.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Mono% : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 88";
            if ((theDV.Count > 0) && (eosin1.Text != ""))
            {
                if ((Convert.ToInt32(eosin1.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(eosin1.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Eosin : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 89";
            if ((theDV.Count > 0) && (eosin2.Text != ""))
            {
                if ((Convert.ToInt32(eosin2.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(eosin2.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Eosin% : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 90";
            if ((theDV.Count > 0) && (txtUrinalysis1.Text != ""))
            {
                if ((Convert.ToDecimal(txtUrinalysis1.Text.Trim()) < Convert.ToDecimal(theDV[0]["MinNormalRange"])) || (Convert.ToDecimal(txtUrinalysis1.Text.Trim()) > Convert.ToDecimal(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Specific Gravity : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 91";
            if ((theDV.Count > 0) && (txtUrinalysis2.Text != ""))
            {
                if ((Convert.ToInt32(txtUrinalysis2.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtUrinalysis2.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Glucose : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 92";
            if ((theDV.Count > 0) && (txtUrinalysis3.Text != ""))
            {
                if ((Convert.ToInt32(txtUrinalysis3.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtUrinalysis3.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + "Ketone : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 93";
            if ((theDV.Count > 0) && (txtUrinalysis4.Text != ""))
            {
                if ((Convert.ToInt32(txtUrinalysis4.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtUrinalysis4.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Protein : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }

            theDV.RowFilter = "SubTestId = 94";
            if ((theDV.Count > 0) && (txtUrinalysis5.Text != ""))
            {
                if ((Convert.ToInt32(txtUrinalysis5.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtUrinalysis5.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Leuk Est : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 95";
            if ((theDV.Count > 0) && (txtUrinalysis6.Text != ""))
            {
                if ((Convert.ToInt32(txtUrinalysis6.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtUrinalysis6.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + " Nitrate : " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            theDV.RowFilter = "SubTestId = 96";
            if ((theDV.Count > 0) && (txtUrinalysis7.Text != ""))
            {
                if ((Convert.ToInt32(txtUrinalysis7.Text.Trim()) < Convert.ToInt32(theDV[0]["MinNormalRange"])) || (Convert.ToInt32(txtUrinalysis7.Text.Trim()) > Convert.ToInt32(theDV[0]["MaxNormalRange"])))
                {
                    strMsg = strMsg + "Blood " + theDV[0]["MinNormalRange"].ToString() + " to " + theDV[0]["MaxNormalRange"].ToString() + "\\n";
                }
            }
            if (strMsg.Trim() != "")
            {
                strMsg = "Following tests are outside the normal range:\\n" + strMsg;
            }
            return strMsg;
        }

        /***************** Delete Record *****************/

        private void VLdetectable()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'VLDetectableload'>\n";
            script += "show('undetect');\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "VLDetectableload", script);
        }

    

       
    }
}