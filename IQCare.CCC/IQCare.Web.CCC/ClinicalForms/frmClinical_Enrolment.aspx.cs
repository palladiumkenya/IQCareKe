using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;
using System.Web.Services;
using System.Web.Script.Services;

namespace IQCare.Web.Clinical
{

    public partial class Enrolment : BasePage
    {
        IPatientRegistration PatientManager;

        #region "Local Variables"
        Hashtable htParameters;
        DropDownList ddlptf = new DropDownList();
        DataTable DT_artSponsor, DT_caretypes, DT_disclosure;
        
        #endregion
        #region "User functions"
        private Boolean FieldValidation()
        {
            IIQCareSystem IQCareSecurity;
            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();


            if (txtenrollmentDate.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Enrolment Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtenrollmentDate.Focus();
                return false;
            }
            DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Text));
            if (theEnrolDate > theCurrentDate)
            {
                IQCareMsgBox.Show("EnrolDate", this);
                txtenrollmentDate.Focus();
                return false;
            }


            if (Convert.ToInt32(txtageEnrollmentYears.Text) > 3 && rdopatientreferredby.SelectedValue == "58")
            {
                IQCareMsgBox.Show("PMTCT", this);
                return false;
            }

            if (Convert.ToInt32(txtageEnrollmentYears.Text) > 3 && Convert.ToInt32(txtageEnrollmentMonths.Text) > 0 && rdopatientreferredby.SelectedValue == "58")
            {
                IQCareMsgBox.Show("PMTCT", this);
                return false;
            }

            if (chkPatientTransferin.Checked == true)
            {
                if (ddlptfTransfer.SelectedValue == "0")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "LPTF Transfer";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    return false;
                }
            }

            if (chkPatientTransferin.Checked == false && ddlptfTransfer.SelectedValue != "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "LPTF Transfer";
                IQCareMsgBox.Show("UncheckedButton", theBuilder, this);
                return false;
            }
            //if (Request.QueryString["name"] == "Edit")
            if (Convert.ToInt32(ViewState["visitPk"]) > 0)
            {
                DateTime theARTRegDate = Convert.ToDateTime(theUtils.MakeDate(ViewState["ARTRegDate"].ToString()));
                if (theEnrolDate < theARTRegDate)
                {
                    IQCareMsgBox.Show("RegistrationDate", this);
                    txtenrollmentDate.Focus();
                    return false;
                }
            }

            return true;
        }

        private void BindDropdown(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Employee"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
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
                    BindManager.BindCombo(lstenrollmentInterviewerName, theDT, "EmployeeName", "EmployeeId");
                }
            }

        }

        protected Boolean IELAB_Validation()
        {
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            //DataSet DSVisit_IELAB = PatientManager.GetVisitDate_IELAB(Convert.ToInt32(Request.QueryString["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            DataSet DSVisit_IELAB = PatientManager.GetVisitDate_IELAB(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            if (Convert.ToInt32(DSVisit_IELAB.Tables[0].Rows[0]["VisitDateflag"]) == 1)
            {
                IQCareUtils theUtils = new IQCareUtils();
                DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Text));
                for (int i = 0; i < DSVisit_IELAB.Tables[1].Rows.Count; i++)
                {
                    DateTime IELabVisitDate = Convert.ToDateTime(DSVisit_IELAB.Tables[1].Rows[i]["VisitDate"]);
                    IELabVisitDate = Convert.ToDateTime(IELabVisitDate.ToString(Session["AppDateFormat"].ToString()));
                    if (IELabVisitDate < theEnrolDate)
                    {
                        IQCareMsgBox.Show("EnrolmentIE_LAB", this);
                        txtenrollmentDate.Focus();
                        return false;
                    }
                }
            }
            return true;
        }
        private string DataQuality_Msg()
        {
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";
            if (txtenrollmentDate.Text.Trim() == "")
            {
                string scriptEnrolDate = "<script language = 'javascript' defer ='defer' id = 'EnrolDate_ID'>\n";
                scriptEnrolDate += "To_Change_Color('lblenroldate');\n";
                scriptEnrolDate += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "EnrolDate_ID", scriptEnrolDate);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Enrollment Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";

            }

            return strmsg;
        }
        private Boolean FieldValidation_DQ()
        {
            IIQCareSystem IQCareSecurity;
            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Text));
            if (theEnrolDate > theCurrentDate)
            {
                IQCareMsgBox.Show("EnrolDate", this);
                txtenrollmentDate.Focus();
                return false;
            }
            return true;

        }
        private void BuildParameterHashTable()
        {
            htParameters = new Hashtable();
            htParameters.Clear();
            //if (Request.QueryString["name"] == "Edit")
            if (Convert.ToInt32(ViewState["visitPk"]) > 0)
            {
                //PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
                
                htParameters.Add("PatientID", PatientId);
                htParameters.Add("LocationID", Session["AppLocationId"].ToString());
            }
            htParameters.Add("RegistrationDate", txtenrollmentDate.Text);
            int PatientTransfer = this.chkPatientTransferin.Checked == true ? 1 : 0;
            htParameters.Add("Transferin", PatientTransfer);
            if (chkPatientTransferin.Checked == true)
            {
                htParameters.Add("LPTFTransferfrom", ddlptfTransfer.SelectedValue);
            }
            else
            {
                htParameters.Add("LPTFTransferfrom", 0);
            };

            htParameters.Add("EducationLevel", ddeducationLevel.SelectedValue);
            htParameters.Add("Literacy", ddLiterarcy.SelectedValue.Trim());
            htParameters.Add("Interviewer", lstenrollmentInterviewerName.SelectedValue);
            htParameters.Add("HIVStatus", ddHIVStatus.SelectedValue);
            htParameters.Add("KnowHIVChildStatus", ddHIVStatus_Child.SelectedValue);
            //HIV Child Care Status
            //if (Convert.ToInt32(txtageCurrentYears.Text) < 5)
            //{
            //    htParameters.Add("KnowHIVChildStatus", ddHIVStatus_Child.SelectedValue);
            //}
            //else
            //{
            //    htParameters.Add("KnowHIVChildStatus", "");
            //}
            htParameters.Add("HIVStatusChangedDate", DateTime.Now);
            //Let the Contact Person know the HIV Status 
            if (rbtnknownHIVStatusYes.Checked == true)
            {
                htParameters.Add("KnowHIVStatus", 1);
                htParameters.Add("DiscussStatus", "");
            }
            else if (rbtnknownHIVStatusNo.Checked == true)
            {
                htParameters.Add("KnowHIVStatus", 0);

                if (rbtnHIVdiscussStatusYes.Checked == true)
                {
                    htParameters.Add("DiscussStatus", 1);
                }
                else if (rbtnHIVdiscussStatusNo.Checked == true)
                {
                    htParameters.Add("DiscussStatus", 0);
                }
                else
                    htParameters.Add("DiscussStatus", "");
            }
            else
            {
                htParameters.Add("KnowHIVStatus", "");
                htParameters.Add("DiscussStatus", "");
            }

            //HIV Care already received
            if (rbtnprevHIVCareYes.Checked == true)
            {
                htParameters.Add("PrevHIVCare", 1);
            }

            else if (rbtnPrevHIVCareNo.Checked == true)
            {
                htParameters.Add("PrevHIVCare", 0);
            }

            else
            {
                htParameters.Add("PrevHIVCare", "");
            }

            //Ever Been in ARTs
            if (rbtnprevARTYes.Checked == true)
            {
                htParameters.Add("ArtSponsor", 1);

                if (rbtnmedRecordsYes.Checked == true)
                {
                    htParameters.Add("PrevMedRecords", 1);
                }
                else if (rbtnmedRecordsNo.Checked == true)
                {
                    htParameters.Add("PrevMedRecords", 0);
                }
                else
                {
                    htParameters.Add("PrevMedRecords", "");
                }
            }
            else if (rbtnprevARTNo.Checked == true)
            {
                htParameters.Add("ArtSponsor", 0);
                htParameters.Add("PrevMedRecords", "");
            }
            else
            {
                htParameters.Add("ArtSponsor", "");
                htParameters.Add("PrevMedRecords", "");
            }
            htParameters.Add("EmploymentStatus", ddemploymentstatus.SelectedValue.ToString());
            htParameters.Add("Occupation", ddoccuption.SelectedValue.ToString());
            htParameters.Add("MonthlyIncome", txtmonthlyIncome.Text);
            htParameters.Add("NumChildren", txtChildren.Text);
            htParameters.Add("NumPeopleHousehold", txtPeopleHousehold.Text);
            htParameters.Add("DistanceTravelled", txtdistanceTravelled.Text);
            htParameters.Add("TimeTravelled", txttimeTravelled.Text);
            htParameters.Add("TimeTravelledUnits", ddtimetravelledUnits.SelectedValue);
            if (rbtnHIVdisclosureYes.Checked == true)
            {
                htParameters.Add("HIVDisclosure", 1);
                /* if (HIVdisclosureName.Checked == true)
                 {
                     htParameters.Add("DiscloseOtherID", 5);
                     htParameters.Add("HIVDisclosureOther", txtHIVdisclosureOther.Text.Trim());
                 }
                 else
                 {
                     htParameters.Add("DiscloseOtherID", "");
                     htParameters.Add("HIVDisclosureOther", "");
                 }*/
            }
            else if (rbtnHIVdisclosureNo.Checked == true)
            {
                htParameters.Add("HIVDisclosure", 0);


            }
            else
            {
                htParameters.Add("HIVDisclosure", "");
            }

            htParameters.Add("NumHouseholdHIVTest", txtHouseholdHIVTest.Text);
            htParameters.Add("NumHouseholdHIVPositive", txtHouseholdHIVPositive.Text);
            htParameters.Add("NumHouseholdHIVDied", numHouseholdHIVDied.Text);
            if (rbtnsupportGroupYes.Checked == true)
            {
                htParameters.Add("SupportGroup", 1);
                htParameters.Add("SupportGroupName", txtsupportGroupName.Text.Trim());
            }
            else if (rbtnsupportGroupNo.Checked == true)
            {
                htParameters.Add("SupportGroup", 0);
                htParameters.Add("SupportGroupName", "");
            }
            else
            {
                htParameters.Add("SupportGroup", "");
                htParameters.Add("SupportGroupName", "");
            }
            int referfromID = AddPatientRef();
            if (referfromID == 61)
            {
                htParameters.Add("ReferredFrom", referfromID);
                htParameters.Add("ReferredFromSpecify", ddlptf.SelectedValue);
            }
            else
            {
                htParameters.Add("ReferredFrom", referfromID);
                htParameters.Add("ReferredFromSpecify", 0);
            }

            htParameters.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));
        }
        private void Show_Hide()
        {
            if (txtknownHIVStatus.Text == "1")
            {
                string script0 = "<script language = 'javascript'  defer = 'defer' id = 'afterfunction'>\n";
                script0 += "show('discuss'); \n";
                script0 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction", script0);
            }

            if (ViewState["prevHIVCareYes"].ToString() == "1")
            {
                string script1 = "<script language = 'javascript' defer ='defer' id = 'afterfunction1'>\n";
                script1 += " show('caretype'); \n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction1", script1);
                if (ViewState["prevHIVcareother"].ToString() == "1")
                {
                    string script2 = "<script language = 'javascript' defer ='defer' id = 'afterfunction2'>\n";
                    script2 += "show('otherprevHIVType'); \n";
                    script2 += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "afterfunction2", script2);

                }
            }
            if (ViewState["prevARTYes"].ToString() == "1")
            {
                string script1 = "<script language = 'javascript' defer ='defer' id = 'afterfunction3'>\n";
                script1 += "show('artSponsor'); \n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction3", script1);

                if (ViewState["prevARTsSponsorother"].ToString() == "1")
                {
                    string script2 = "<script language = 'javascript' defer ='defer' id = 'afterfunction4'>\n";
                    script2 += "show('otherprevARTsSponsor'); \n";
                    script2 += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "afterfunction4", script2);
                }
            }

            if (ViewState["HIVdisclosureYes"].ToString() == "1")
            {
                string script1 = "<script language = 'javascript' defer ='defer' id = 'afterfunction5'>\n";
                script1 += "show('showHIVdisclosureName'); \n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction5", script1);
            }

            if (ViewState["rbtnsupportGroupYes"].ToString() == "1")
            {
                string script1 = "<script language = 'javascript' defer ='defer' id = 'afterfunction7'>\n";
                script1 += "show('supportName'); \n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction7", script1);

            }

            if (ViewState["divHIVStatusChild"].ToString() == "1")
            {
                string script1 = "<script language = 'javascript' defer ='defer' id = 'afterfunction8'>\n";
                script1 += "show('HIVStatusChild'); \n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction8", script1);

            }
            int PatientRefID = AddPatientRef();
            if (PatientRefID == 61)
            {
                string script1 = "<script language = 'javascript' defer ='defer' id = 'afterfunction9'>\n";
                script1 += "show('lptf'); \n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "afterfunction9", script1);
            }


        }
        private void ShowHideHIVDisclosure()
        {
            foreach (HtmlTableRow r in tblHIVdisclosure.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Value == "Other")
                            {
                                if (((HtmlInputCheckBox)ct).Checked == true)
                                {
                                    string script = "";
                                    script = "<script language = 'javascript' defer='defer' id='HIVDisclosure'>\n";
                                    script += "show('otherDisclosure');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HIVDisclosure", script);
                                }
                            }
                        }
                    }
                }
            }

        }
        private DataTable AddHIVCareTypes()
        {

            DataTable DTHIVCareType = new DataTable();

            DataColumn theHIVCareTypeID = new DataColumn("HIVCareTypeID");
            theHIVCareTypeID.DataType = System.Type.GetType("System.Int32");
            DTHIVCareType.Columns.Add(theHIVCareTypeID);

            DataColumn theHIVCareTypeOtherID = new DataColumn("HIVCareTypeOther");
            theHIVCareTypeOtherID.DataType = System.Type.GetType("System.String");
            DTHIVCareType.Columns.Add(theHIVCareTypeOtherID);

            DataRow drHIVCareType;
            int HIVCareTypeID_Other = 0;
            String strHIVCareType = "";
            foreach (HtmlTableRow tr in tblreceiveHIVCare.Rows)
            {
                drHIVCareType = DTHIVCareType.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strHIVCareType = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in DT_caretypes.Rows)
                                {
                                    if (strHIVCareType == dr[1].ToString())
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            HIVCareTypeID_Other = Convert.ToInt32(dr[0].ToString());
                                            strHIVCareType = dr[1].ToString();
                                        }

                                        else
                                        {

                                            drHIVCareType["HIVCareTypeID"] = dr[0].ToString();
                                            drHIVCareType["HIVCareTypeOther"] = null;
                                            DTHIVCareType.Rows.Add(drHIVCareType);
                                        }
                                    }
                                }

                            }
                        }

                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {

                            if (strHIVCareType == "Other")
                            {
                                drHIVCareType["HIVCareTypeID"] = HIVCareTypeID_Other.ToString();
                                drHIVCareType["HIVCareTypeOther"] = ((HtmlInputText)ct).Value;
                                DTHIVCareType.Rows.Add(drHIVCareType);
                            }
                        }
                    }
                }
            }
            return DTHIVCareType;
        }
        private DataTable AddARTSponsor()
        {

            DataTable DTARTSponsor = new DataTable();

            DataColumn theARTSponsorID = new DataColumn("ARTsponsorID");
            theARTSponsorID.DataType = System.Type.GetType("System.Int32");
            DTARTSponsor.Columns.Add(theARTSponsorID);

            DataColumn theARTSponsorOther = new DataColumn("ARTSponsorOther");
            theARTSponsorOther.DataType = System.Type.GetType("System.String");
            DTARTSponsor.Columns.Add(theARTSponsorOther);

            DataRow drARTSponsor;
            int ARTSponsorOtherID = 0;
            String strARTSponsor = "";

            foreach (HtmlTableRow tr in tblArtSponsor.Rows)
            {
                drARTSponsor = DTARTSponsor.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strARTSponsor = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in DT_artSponsor.Rows)
                                {
                                    if (strARTSponsor == dr[1].ToString())
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            ARTSponsorOtherID = Convert.ToInt32(dr[0].ToString());
                                            strARTSponsor = dr[1].ToString();
                                        }
                                        else
                                        {
                                            drARTSponsor["ARTsponsorID"] = Convert.ToInt32(dr[0].ToString());
                                            drARTSponsor["ARTSponsorOther"] = null;
                                            DTARTSponsor.Rows.Add(drARTSponsor);
                                        }

                                    }

                                }
                            }

                        }

                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {

                            if (strARTSponsor == "Other")
                            {
                                drARTSponsor["ARTSponsorID"] = ARTSponsorOtherID.ToString();
                                drARTSponsor["ARTSponsorOther"] = ((HtmlInputText)ct).Value;
                                DTARTSponsor.Rows.Add(drARTSponsor);
                            }
                        }
                    }
                }
            }
            return DTARTSponsor;
        }
        private DataTable AddDisclosure()
        {
            //code for saving disclosure details.
            DataTable dtDisclosure = new DataTable();

            DataColumn theDisclosureID = new DataColumn("DisclosureID");
            theDisclosureID.DataType = System.Type.GetType("System.Int32");
            dtDisclosure.Columns.Add(theDisclosureID);

            DataColumn theDisclosureOther = new DataColumn("DisclosureOther");
            theDisclosureOther.DataType = System.Type.GetType("System.String");
            dtDisclosure.Columns.Add(theDisclosureOther);

            DataRow drdisclosure;
            int DisclosureOtherID = 0;
            string strDisclosure = "";

            foreach (HtmlTableRow tr in tblHIVdisclosure.Rows)
            {
                drdisclosure = dtDisclosure.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strDisclosure = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in DT_disclosure.Rows)
                                {
                                    if (strDisclosure == dr[1].ToString())
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            DisclosureOtherID = Convert.ToInt32(dr[0].ToString());
                                            strDisclosure = dr[1].ToString();
                                        }
                                        else
                                        {
                                            drdisclosure["DisclosureID"] = Convert.ToInt32(dr[0].ToString());
                                            drdisclosure["DisclosureOther"] = null;
                                            dtDisclosure.Rows.Add(drdisclosure);
                                        }

                                    }
                                }
                            }
                        }
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (strDisclosure == "Other")
                            {
                                drdisclosure["DisclosureID"] = DisclosureOtherID;
                                drdisclosure["DisclosureOther"] = ((HtmlInputText)ct).Value;
                                dtDisclosure.Rows.Add(drdisclosure);
                            }

                        }
                    }

                }
            }
            return dtDisclosure;
        }
        private DataTable AddBarrier()
        {
            DataTable dtBarrier = new DataTable();

            DataColumn theBarrierID = new DataColumn("BarrierID");
            theBarrierID.DataType = System.Type.GetType("System.Int32");
            dtBarrier.Columns.Add(theBarrierID);

            DataRow DRbarrier;

            for (int i = 0; i < cblbarrierstocare2.Items.Count; i++)
            {
                if (cblbarrierstocare2.Items[i].Selected)
                {
                    DRbarrier = dtBarrier.NewRow();
                    DRbarrier["BarrierID"] = Convert.ToInt32(cblbarrierstocare2.Items[i].Value);
                    dtBarrier.Rows.Add(DRbarrier);
                }
            }
            return dtBarrier;
        }
        private int AddPatientRef()
        {
            int patientRefID = 0;
            foreach (Control x in pnlprb.Controls)
            {
                foreach (Control y in x.Controls)
                {
                    if (y.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (((HtmlInputRadioButton)y).Checked == true)
                        {
                            patientRefID = Convert.ToInt32(((HtmlInputRadioButton)y).ID);
                        }
                    }
                }
            }
            return patientRefID;
        }

        private void LoadPatientData()
        {
            int PId;
            PId = Convert.ToInt32(Session["PatientId"]);
            IQCareUtils theUtil = new IQCareUtils();

            //if (Request.QueryString["name"] == "Edit")
            if (Convert.ToInt32(ViewState["visitPk"]) > 0)
            {
                PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                //int locationID = Convert.ToInt32(Request.QueryString["locationid"]);
                int locationID = Convert.ToInt32(Session["AppLocationId"]);
                //updating DataQuality Status - Common Procedure call
                DataTable theDT = PatientManager.theVisitIDDT(PId.ToString());
                ViewState["visitPk"] = theDT.Rows[0]["Visit_ID"].ToString();
                ViewState["VisitDate"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDT.Rows[0]["VisitDate"]));
                DataSet theDS = PatientManager.GetPatientEnroll(PId.ToString(), Convert.ToInt32(ViewState["visitPk"]));
                ViewState["DataQuality"] = "0";
                ViewState["LocationID"] = theDS.Tables[1].Rows[0]["LocationID"].ToString();
                if (theDS.Tables[5].Rows.Count > 0)
                {
                    this.ddHIVStatus.SelectedValue = theDS.Tables[5].Rows[0]["HIVStatus"].ToString();
                    this.ddHIVStatus_Child.SelectedValue = theDS.Tables[5].Rows[0]["HivStatus_Child"].ToString();
                }
                ViewState["PtnClinicID"] = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                //this.txtenrollmentDate.Text = ((DateTime)theDS.Tables[10].Rows[0]["RegDate"]).ToString(Session["AppDateFormat"].ToString());
                this.txtenrollmentDate.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDT.Rows[0]["VisitDate"]));
                ViewState["ARTRegDate"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[10].Rows[0]["RegDate"]));
                if (theDS.Tables[1].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS.Tables[1].Rows[0]["DataQuality"]) == 1)
                {
                    btncomplete.ControlStyle.BackColor = System.Drawing.Color.LightGreen;
                }
                if (theDS.Tables[0].Rows[0]["Status"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Status"]) == 1)
                    {
                        btnsave.Enabled = false;
                        btncomplete.Enabled = false;
                    }
                }
                if (theDS.Tables[5].Rows.Count > 0)
                {
                    if (theDS.Tables[5].Rows[0]["SupportGroup"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[5].Rows[0]["SupportGroup"]) == 0)
                        {
                            this.rbtnsupportGroupNo.Checked = true;
                        }
                        else if (Convert.ToInt32(theDS.Tables[5].Rows[0]["SupportGroup"]) == 1)
                        {
                            this.rbtnsupportGroupYes.Checked = true;
                            string scriptSG = "";
                            scriptSG = "<script language = 'javascript' defer ='defer' id = 'SupportGroup'>\n";
                            scriptSG += "show('supportName');\n";
                            scriptSG += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "SupportGroup", scriptSG);
                        }
                    }

                    this.txtHouseholdHIVPositive.Text = theDS.Tables[5].Rows[0]["NumHouseholdHIVPositive"].ToString();
                    this.txtHouseholdHIVTest.Text = theDS.Tables[5].Rows[0]["NumHouseholdHIVTest"].ToString();
                    this.numHouseholdHIVDied.Text = theDS.Tables[5].Rows[0]["NumHouseholdHIVDied"].ToString();
                    this.txtsupportGroupName.Text = theDS.Tables[5].Rows[0]["SupportGroupName"].ToString();


                    //HIV Disclosure Status
                    if (theDS.Tables[5].Rows[0]["HIVdisclosure"] != System.DBNull.Value)
                    {

                        if (Convert.ToInt32(theDS.Tables[5].Rows[0]["HIVdisclosure"]) == 1)
                        {
                            string DisclosureOther = "";
                            string DisclosureOtherDesc = "";
                            if (theDS.Tables[6].Rows.Count >= 1)
                            {
                                for (int i = 0; i < theDS.Tables[6].Rows.Count; i++)
                                {
                                    foreach (HtmlTableRow tr in tblHIVdisclosure.Rows)
                                    {
                                        foreach (HtmlTableCell tc in tr.Cells)
                                        {
                                            foreach (Control ct in tc.Controls)
                                            {
                                                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                                {
                                                    foreach (DataRow dr in DT_disclosure.Rows)
                                                    {
                                                        if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                                        {
                                                            if (dr[0].ToString() == theDS.Tables[6].Rows[i]["DisclosureID"].ToString())
                                                            {
                                                                if (dr[1].ToString() == "Other")
                                                                {
                                                                    DisclosureOther = dr[1].ToString();
                                                                    DisclosureOtherDesc = theDS.Tables[6].Rows[i]["DisclosureOther"].ToString();
                                                                    ((HtmlInputCheckBox)ct).Checked = true;
                                                                }
                                                                else
                                                                {
                                                                    ((HtmlInputCheckBox)ct).Checked = true;

                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                                {
                                                    if (DisclosureOther == "Other")
                                                    {
                                                        ((HtmlInputText)ct).Value = DisclosureOtherDesc;
                                                        string script = "";
                                                        script = "<script language = 'javascript' defer ='defer' id = 'OtherDisclosure_0'>\n";
                                                        script += "show('otherDisclosure');\n";
                                                        script += "</script>\n";
                                                        ClientScript.RegisterStartupScript(this.GetType(), "OtherDisclosure_0", script);
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }
                            rbtnHIVdisclosureYes.Checked = true;
                            string HIVDis = "";
                            HIVDis = "<script language = 'javascript' defer ='defer' id = 'HIVDisclosure'>\n";
                            HIVDis += "show('showHIVdisclosureName');\n";
                            HIVDis += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "HIVDisclosure", HIVDis);
                        }
                        else if (Convert.ToInt32(theDS.Tables[5].Rows[0]["HIVdisclosure"]) == 0)
                        {
                            rbtnHIVdisclosureNo.Checked = true;
                        }
                    }

                    //Begin: Code to Populate "Barrier to Successful HIV Care" in Edit Mode 

                    if (theDS.Tables[7].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS.Tables[7].Rows.Count; i++)
                        {
                            for (int j = 0; j < cblbarrierstocare2.Items.Count; j++)
                            {
                                if (cblbarrierstocare2.Items[j].Value == theDS.Tables[7].Rows[i]["BarrierID"].ToString())
                                {
                                    cblbarrierstocare2.Items[j].Selected = true;
                                }
                            }
                        }
                    }
                    //End 

                }

                this.txtageEnrollmentYears.Text = theDS.Tables[0].Rows[0]["EnrolAge"].ToString();
                this.txtageEnrollmentMonths.Text = theDS.Tables[0].Rows[0]["EnrolAge1"].ToString();
                if (theDS.Tables[0].Rows[0]["TransferIn"].ToString() == "1")
                {
                    this.chkPatientTransferin.Checked = true;
                }
                this.ddlptfTransfer.SelectedValue = theDS.Tables[0].Rows[0]["LPTFTransferId"].ToString();

                // if (Convert.ToInt32(this.txtageCurrentYears.Text) < 5)
                //{
                string strHIVStatusChild = "";
                strHIVStatusChild = "<script language = 'javascript' defer ='defer' id = 'strHIVStatusChild_ID'>\n";
                strHIVStatusChild += "show('HIVStatusChild');\n";
                strHIVStatusChild += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "strHIVStatusChild_ID", strHIVStatusChild);
                // }

                if (theDS.Tables[4].Rows.Count > 0)
                {
                    if (theDS.Tables[4].Rows[0]["MonthlyIncome"] != System.DBNull.Value)
                    {
                        this.txtmonthlyIncome.Text = theDS.Tables[4].Rows[0]["MonthlyIncome"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["NumPeopleHousehold"] != System.DBNull.Value)
                    {
                        this.txtPeopleHousehold.Text = theDS.Tables[4].Rows[0]["NumPeopleHousehold"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["DistanceTravelled"] != System.DBNull.Value)
                    {
                        this.txtdistanceTravelled.Text = theDS.Tables[4].Rows[0]["DistanceTravelled"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["TimeTravelled"] != System.DBNull.Value)
                    {
                        this.txttimeTravelled.Text = theDS.Tables[4].Rows[0]["TimeTravelled"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["TravelledUnits"] != System.DBNull.Value)
                    {
                        this.ddtimetravelledUnits.Text = theDS.Tables[4].Rows[0]["TravelledUnits"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["EmploymentStatus"] != System.DBNull.Value)
                    {
                        this.ddemploymentstatus.SelectedValue = theDS.Tables[4].Rows[0]["EmploymentStatus"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["Occupation"] != System.DBNull.Value)
                    {
                        this.ddoccuption.SelectedValue = theDS.Tables[4].Rows[0]["Occupation"].ToString();
                    }
                    if (theDS.Tables[4].Rows[0]["NumChildren"] != System.DBNull.Value)
                    {
                        this.txtChildren.Text = theDS.Tables[4].Rows[0]["NumChildren"].ToString();
                    }
                }
                //ART Sponsors
                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["PrevART"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[3].Rows[0]["PrevART"]) == 1)
                        {
                            string OtherSponsor_ID = "";
                            string OtherSponsorDesc = "";
                            this.rbtnprevARTYes.Checked = true;
                            for (int i = 0; i < theDS.Tables[9].Rows.Count; i++)
                            {
                                foreach (HtmlTableRow r in tblArtSponsor.Rows)
                                {
                                    foreach (HtmlTableCell c in r.Cells)
                                    {
                                        foreach (Control ct in c.Controls)
                                        {
                                            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                            {
                                                foreach (DataRow dr in DT_artSponsor.Rows)
                                                {
                                                    if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                                    {
                                                        if (dr[0].ToString() == theDS.Tables[9].Rows[i]["ARTSponsorID"].ToString())
                                                        {
                                                            if (dr[1].ToString() == "Other")
                                                            {
                                                                OtherSponsor_ID = dr[1].ToString();
                                                                OtherSponsorDesc = theDS.Tables[9].Rows[i]["ARTSponsorDesc"].ToString();
                                                                ((HtmlInputCheckBox)ct).Checked = true;
                                                            }
                                                            else
                                                            {
                                                                ((HtmlInputCheckBox)ct).Checked = true;

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                            {
                                                if (OtherSponsor_ID == "Other")
                                                {
                                                    ((HtmlInputText)ct).Value = OtherSponsorDesc;
                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'OtherSponsor_0'>\n";
                                                    script += "show('otherARTSponsor');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "OtherSponsor_0", script);
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                            if (theDS.Tables[3].Rows[0]["PrevMedRecords"] != System.DBNull.Value)
                            {
                                if (Convert.ToInt32(theDS.Tables[3].Rows[0]["PrevMedRecords"]) == 1)
                                {
                                    this.rbtnmedRecordsYes.Checked = true;
                                }
                                else if (Convert.ToInt32(theDS.Tables[3].Rows[0]["PrevMedRecords"]) == 0)
                                {
                                    this.rbtnmedRecordsNo.Checked = true;
                                }
                            }
                            string PrevART = "";
                            PrevART = "<script language = 'javascript' defer ='defer' id = 'PrevARTSponser'>\n";
                            PrevART += "show('artSponsor');\n";
                            PrevART += "show('medRecordsdiv');\n";
                            PrevART += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "PrevARTSponser", PrevART);
                        }
                        if (Convert.ToInt32(theDS.Tables[3].Rows[0]["PrevART"]) == 0)
                        {
                            rbtnprevARTNo.Checked = true;
                        }
                    }
                    else
                    {
                        rbtnprevARTUnknown.Checked = true;
                    }

                    //Received care for HIV/AIDS
                    if (theDS.Tables[3].Rows[0]["PrevHIVCare"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[3].Rows[0]["PrevHIVCare"]) == 1)
                        {
                            string OtherCareType_ID = "";
                            string OtherCareTypeDesc = "";
                            this.rbtnprevHIVCareYes.Checked = true;
                            for (int i = 0; i < theDS.Tables[8].Rows.Count; i++)
                            {
                                foreach (HtmlTableRow r in tblreceiveHIVCare.Rows)
                                {
                                    foreach (HtmlTableCell c in r.Cells)
                                    {
                                        foreach (Control ct in c.Controls)
                                        {
                                            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                            {
                                                foreach (DataRow dr in DT_caretypes.Rows)
                                                {
                                                    if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                                    {
                                                        if (dr[0].ToString() == theDS.Tables[8].Rows[i]["HIVAIDsCareID"].ToString())
                                                        {
                                                            if (dr[1].ToString() == "Other")
                                                            {
                                                                OtherCareType_ID = dr[1].ToString();
                                                                OtherCareTypeDesc = theDS.Tables[8].Rows[i]["HIVAIDsCareDesc"].ToString();
                                                                ((HtmlInputCheckBox)ct).Checked = true;
                                                            }
                                                            else
                                                            {
                                                                ((HtmlInputCheckBox)ct).Checked = true;
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                            {
                                                if (OtherCareType_ID == "Other")
                                                {
                                                    ((HtmlInputText)ct).Value = OtherCareTypeDesc;
                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'CareType_0'>\n";
                                                    script += "show('otherCareTypes');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "CareType_0", script);
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            string Caretype = "";
                            Caretype = "<script language = 'javascript' defer ='defer' id = 'PrevCaretype'>\n";
                            Caretype += "show('caretype');\n";
                            Caretype += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "PrevCaretype", Caretype);
                        }

                        else if (Convert.ToInt32(theDS.Tables[3].Rows[0]["PrevHIVCare"]) == 0)
                        {
                            this.rbtnPrevHIVCareNo.Checked = true;
                        }
                    }

                }
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    if (theDS.Tables[2].Rows[0]["EmergContactKnowsHIVStatus"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[2].Rows[0]["EmergContactKnowsHIVStatus"]) == 0)
                        {
                            string Statusscript = "";
                            Statusscript = "<script language = 'javascript' defer ='defer' id = 'DiscussStatus_HIV'>\n";
                            Statusscript += "show('discuss'); \n";
                            Statusscript += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "DiscussStatus_HIV", Statusscript);
                            this.rbtnknownHIVStatusNo.Checked = true;
                            if (theDS.Tables[2].Rows[0]["DiscussStatus"] != System.DBNull.Value)
                            {
                                if (Convert.ToInt32(theDS.Tables[2].Rows[0]["DiscussStatus"]) == 0)
                                {
                                    this.rbtnHIVdiscussStatusNo.Checked = true;
                                }
                                else if (Convert.ToInt32(theDS.Tables[2].Rows[0]["DiscussStatus"]) == 1)
                                {
                                    this.rbtnHIVdiscussStatusYes.Checked = true;
                                }
                            }

                        }
                        else if (Convert.ToInt32(theDS.Tables[2].Rows[0]["EmergContactKnowsHIVStatus"]) == 1)
                        {
                            this.rbtnknownHIVStatusYes.Checked = true;
                        }
                    }


                }
                this.txteducationother.Text = theDS.Tables[0].Rows[0]["EducationLevel"].ToString();
                this.ddeducationLevel.SelectedValue = theDS.Tables[0].Rows[0]["EducationLevel"].ToString();
                this.ddLiterarcy.SelectedValue = theDS.Tables[0].Rows[0]["Literacy"].ToString();
                BindDropdown(theDS.Tables[0].Rows[0]["EmployeeID"].ToString());

                this.lstenrollmentInterviewerName.SelectedValue = theDS.Tables[0].Rows[0]["EmployeeID"].ToString();

                //this.txtNotes.Text = theDS.Tables[0].Rows[0]["Notes"].ToString();

                foreach (Control x in pnlprb.Controls)
                {
                    foreach (Control y in x.Controls)
                    {
                        if (y.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                        {
                            if (y.ID == theDS.Tables[0].Rows[0]["ReferredFrom"].ToString())
                            {
                                if (y.ID == "61")
                                {
                                    ((HtmlInputRadioButton)y).Checked = true;
                                    ddlptf.SelectedValue = theDS.Tables[0].Rows[0]["ReferredFromSpecify"].ToString();
                                    string ScriptOtherFacility = "<script language = 'javascript' defer ='defer' id = 'afterotherfacility'>\n";
                                    ScriptOtherFacility += "show('lptf'); \n";
                                    ScriptOtherFacility += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "afterotherfacility", ScriptOtherFacility);

                                }
                                else { ((HtmlInputRadioButton)y).Checked = true; }
                            }

                        }
                    }
                }
                ////Amitava Sinha
                ///if (ViewState["ControlCreated"] != null)
                FillOldData(Convert.ToInt32(PId.ToString()));
            }
        }
        protected void Init_Form()
        {
            try
            {
                BindFunctions BindManager = new BindFunctions();
                IQCareUtils theUtils = new IQCareUtils();
                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

                //if (Request.QueryString["name"] == "Add")
                if (Convert.ToInt32(ViewState["visitPk"]) == 0)
                {

                    //////////////////////////////

                    DataView theDV = new DataView();
                    DataTable theDT = new DataTable();

                    theDV = new DataView(theDSXML.Tables["Mst_Education"]);
                    theDV.RowFilter = "DeleteFlag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddeducationLevel, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();

                    }
                    /*******/

                    theDV = new DataView(theDSXML.Tables["mst_LPTF"]);
                    theDV.RowFilter = "DeleteFlag=0 and SystemId=1";
                    ViewState["LPTF"] = null;
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        ViewState["LPTF"] = theDT;
                        BindManager.BindCombo(ddlptfTransfer, theDT, "Name", "ID");
                        if (theDT.Rows.Count == 0)
                        {
                            ListItem theItem = new ListItem();
                            theItem.Text = "-Select-";
                            theItem.Value = "0";
                            ddlptfTransfer.Items.Add(theItem);
                        }
                        theDV.Dispose();
                        // theDT.Clear();

                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=9 and (SystemID=0 or SystemID=1)";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddemploymentstatus, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=10";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddHIVStatus, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                    /*******/
                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=11 and (SystemID=0 or SystemID=1)";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddLiterarcy, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                    /*******/

                    theDV = new DataView(theDSXML.Tables["Mst_Occupation"]);
                    theDV.RowFilter = "DeleteFlag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddoccuption, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=14";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddtimetravelledUnits, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["mst_Employee"]);
                    theDV.RowFilter = "DeleteFlag=0";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                        {
                            theDV = new DataView(theDT);
                            theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                            if (theDV.Count > 0)
                                theDT = theUtils.CreateTableFromDataView(theDV);
                        }
                        BindManager.BindCombo(lstenrollmentInterviewerName, theDT, "EmployeeName", "EmployeeId");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    /*******/
                    theDV = new DataView(theDSXML.Tables["mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=16 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCheckedList(cblbarrierstocare2, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                    /*******/
                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=17 and SystemID=1 and ModuleId in(0," + Convert.ToInt32(Session["TechnicalAreaId"]) + ")";
                    ViewState["tblpatientRefAdd"] = null;
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        ViewState["tblpatientRefAdd"] = theDT;
                        theDV.Dispose();
                        //theDT.Clear();
                    }
                    theDV = new DataView(theDSXML.Tables["Mst_HIVAIDSCareTypes"]);
                    theDV.RowFilter = "Deleteflag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        DT_caretypes = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_caretypes.Rows.Count; i++)
                        {
                            HtmlTableRow tr_caretypes = new HtmlTableRow();
                            HtmlInputCheckBox chkcaretypes = new HtmlInputCheckBox();
                            HtmlTableCell tc_caretypes = new HtmlTableCell();
                            chkcaretypes.ID = Convert.ToString(col);
                            chkcaretypes.Value = DT_caretypes.Rows[i][1].ToString();
                            tc_caretypes.Controls.Add(chkcaretypes);
                            tc_caretypes.Controls.Add(new LiteralControl(chkcaretypes.Value));
                            tr_caretypes.Cells.Add(tc_caretypes);
                            if (chkcaretypes.Value == "Other")
                            {
                                HtmlTableCell tc_caretypes_1 = new HtmlTableCell();
                                tc_caretypes_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherCareTypes'>"));
                                tc_caretypes_1.Controls.Add(new LiteralControl("<DIV id='otherCareTypes' style='DISPLAY:none'>Specify: "));
                                HtmlInputText HTextotherCareTypes = new HtmlInputText();
                                HTextotherCareTypes.ID = "txtotherCareTypes";
                                HTextotherCareTypes.Size = 10;
                                tc_caretypes_1.Controls.Add(HTextotherCareTypes);
                                tc_caretypes_1.Controls.Add(new LiteralControl(HTextotherCareTypes.Value));
                                tc_caretypes_1.Controls.Add(new LiteralControl("</DIV>"));
                                tr_caretypes.Cells.Add(tc_caretypes_1);
                                chkcaretypes.Attributes.Add("onclick", "toggle('otherCareTypes'); cleartxtbox('ctl00_IQCareContentPlaceHolder_5', '" + HTextotherCareTypes.ClientID + "')");
                            }
                            col++;
                            tblreceiveHIVCare.Rows.Add(tr_caretypes);
                        }
                        theDV.Dispose();
                    }
                    //*****Jayant -- ART Sponser 
                    theDV = new DataView(theDSXML.Tables["mst_ARTSponsor"]);
                    theDV.RowFilter = "Deleteflag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        DT_artSponsor = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_artSponsor.Rows.Count; i++)
                        {
                            HtmlTableRow tr_artsponsor = new HtmlTableRow();
                            HtmlInputCheckBox chkartSponsor = new HtmlInputCheckBox();
                            HtmlTableCell tc_artsponsor = new HtmlTableCell();
                            chkartSponsor.ID = Convert.ToString("r" + col);
                            chkartSponsor.Value = DT_artSponsor.Rows[i][1].ToString();
                            tc_artsponsor.Controls.Add(chkartSponsor);
                            tc_artsponsor.Controls.Add(new LiteralControl(chkartSponsor.Value));
                            tr_artsponsor.Cells.Add(tc_artsponsor);
                            if (chkartSponsor.Value == "Other")
                            {
                                HtmlTableCell tc_artsponsor_1 = new HtmlTableCell();
                                tc_artsponsor_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherARTSponsor'>"));
                                tc_artsponsor_1.Controls.Add(new LiteralControl("<DIV id='otherARTSponsor' style='DISPLAY:none'>Specify: "));
                                HtmlInputText HTextotherARTSponsor = new HtmlInputText();
                                HTextotherARTSponsor.ID = "txtotherARTSponsor";
                                HTextotherARTSponsor.Size = 10;
                                tc_artsponsor_1.Controls.Add(HTextotherARTSponsor);
                                tc_artsponsor_1.Controls.Add(new LiteralControl(HTextotherARTSponsor.Value));
                                tc_artsponsor_1.Controls.Add(new LiteralControl("</DIV>"));
                                tr_artsponsor.Cells.Add(tc_artsponsor_1);
                                chkartSponsor.Attributes.Add("onclick", "toggle('otherARTSponsor');");
                            }
                            col++;
                            tblArtSponsor.Rows.Add(tr_artsponsor);
                        }
                        theDV.Dispose();
                    }
                    //*****Jayant -- Mst_Disclosure 18-06-2008
                    theDV = new DataView(theDSXML.Tables["Mst_HivDisclosure"]);
                    theDV.RowFilter = "Deleteflag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        DT_disclosure = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_disclosure.Rows.Count; i++)
                        {
                            HtmlTableRow trdisclosure = new HtmlTableRow();
                            HtmlTableCell tcdisclosure = new HtmlTableCell();
                            HtmlInputCheckBox chkdisclosure = new HtmlInputCheckBox();
                            chkdisclosure.ID = Convert.ToString("dis" + col);
                            chkdisclosure.Value = DT_disclosure.Rows[i][1].ToString();
                            tcdisclosure.Controls.Add(chkdisclosure);
                            tcdisclosure.Controls.Add(new LiteralControl(chkdisclosure.Value));
                            trdisclosure.Controls.Add(tcdisclosure);
                            if (chkdisclosure.Value == "Other")
                            {
                                HtmlTableCell tcdisclosureother = new HtmlTableCell();
                                tcdisclosure.Controls.Add(new LiteralControl("<LABEL class=right style='font-weight:bold' for='otherdisclosure'>"));
                                tcdisclosure.Controls.Add(new LiteralControl("<DIV id='otherDisclosure' style='DISPLAY:none'>Specify: "));

                                HtmlInputText txtdisclosureother = new HtmlInputText();
                                txtdisclosureother.ID = "txtdisclosureother";
                                txtdisclosureother.Size = 10;
                                tcdisclosure.Controls.Add(txtdisclosureother);
                                tcdisclosure.Controls.Add(new LiteralControl(txtdisclosureother.Value));
                                tcdisclosure.Controls.Add(new LiteralControl("</DIV>"));
                                trdisclosure.Cells.Add(tcdisclosure);
                                chkdisclosure.Attributes.Add("onclick", "toggle('otherDisclosure');");
                            }
                            col++;
                            tblHIVdisclosure.Rows.Add(trdisclosure);
                        }
                    }

                }
                //if (Request.QueryString["name"] == "Edit")
                if (Convert.ToInt32(ViewState["visitPk"]) > 0)
                {
                    DataView theDV = new DataView();
                    DataTable theDT = new DataTable();


                    theDV = new DataView(theDSXML.Tables["Mst_Education"]);
                    theDV.RowFilter = "SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddeducationLevel, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["mst_LPTF"]);
                    theDV.RowFilter = "SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        ViewState["LPTF"] = theDT;
                        BindManager.BindCombo(ddlptfTransfer, theDT, "Name", "ID");
                        if (theDT.Rows.Count == 0)
                        {
                            ListItem theItem = new ListItem();
                            theItem.Text = "-Select-";
                            theItem.Value = "0";
                            ddlptfTransfer.Items.Add(theItem);
                        }
                        theDV.Dispose();
                        //theDT.Clear();
                    }


                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeID=9 and (SystemID=0 or SystemID=1)";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddemploymentstatus, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeID=10 and (SystemID=0 or SystemID=1)";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddHIVStatus, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                    /*******/
                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeID=11 and (SystemID=0 or SystemID=1)";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddLiterarcy, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                    /*******/
                    //HIVAIDS Care Types
                    theDV = new DataView(theDSXML.Tables["Mst_HIVAIDSCareTypes"]);
                    theDV.RowFilter = "Deleteflag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        DT_caretypes = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_caretypes.Rows.Count; i++)
                        {
                            HtmlTableRow tr_caretypes = new HtmlTableRow();
                            HtmlInputCheckBox chkcaretypes = new HtmlInputCheckBox();
                            HtmlTableCell tc_caretypes = new HtmlTableCell();
                            chkcaretypes.ID = Convert.ToString(col);
                            chkcaretypes.Value = DT_caretypes.Rows[i][1].ToString();
                            tc_caretypes.Controls.Add(chkcaretypes);
                            tc_caretypes.Controls.Add(new LiteralControl(chkcaretypes.Value));
                            tr_caretypes.Cells.Add(tc_caretypes);
                            if (chkcaretypes.Value == "Other")
                            {
                                HtmlTableCell tc_caretypes_1 = new HtmlTableCell();
                                tc_caretypes_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherCareTypes'>"));
                                tc_caretypes_1.Controls.Add(new LiteralControl("<DIV id='otherCareTypes' style='DISPLAY:none'>Specify: "));
                                HtmlInputText HTextotherCareTypes = new HtmlInputText();
                                HTextotherCareTypes.ID = "txtotherCareTypes";
                                HTextotherCareTypes.Size = 10;
                                tc_caretypes_1.Controls.Add(HTextotherCareTypes);
                                tc_caretypes_1.Controls.Add(new LiteralControl(HTextotherCareTypes.Value));
                                tc_caretypes_1.Controls.Add(new LiteralControl("</DIV>"));
                                tr_caretypes.Cells.Add(tc_caretypes_1);
                                //chkcaretypes.Attributes.Add("onclick", "toggle('otherCareTypes');");
                                chkcaretypes.Attributes.Add("onclick", "toggle('otherCareTypes'); cleartxtbox('ctl00_IQCareContentPlaceHolder_5', '" + HTextotherCareTypes.ClientID + "')");
                            }
                            col++;
                            tblreceiveHIVCare.Rows.Add(tr_caretypes);
                        }
                    }
                    //ART Sponsor

                    theDV = new DataView(theDSXML.Tables["mst_ARTSponsor"]);
                    theDV.RowFilter = "Deleteflag=0 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        int col = 0;
                        DT_artSponsor = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        for (int i = 0; i < DT_artSponsor.Rows.Count; i++)
                        {
                            HtmlTableRow tr_artsponsor = new HtmlTableRow();
                            HtmlInputCheckBox chkartSponsor = new HtmlInputCheckBox();
                            HtmlTableCell tc_artsponsor = new HtmlTableCell();
                            chkartSponsor.ID = Convert.ToString("r" + col);
                            chkartSponsor.Value = DT_artSponsor.Rows[i][1].ToString();
                            tc_artsponsor.Controls.Add(chkartSponsor);
                            tc_artsponsor.Controls.Add(new LiteralControl(chkartSponsor.Value));
                            tr_artsponsor.Cells.Add(tc_artsponsor);
                            if (chkartSponsor.Value == "Other")
                            {
                                HtmlTableCell tc_artsponsor_1 = new HtmlTableCell();
                                tc_artsponsor_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherARTSponsor'>"));
                                tc_artsponsor_1.Controls.Add(new LiteralControl("<DIV id='otherARTSponsor' style='DISPLAY:none'>Specify: "));
                                HtmlInputText HTextotherARTSponsor = new HtmlInputText();
                                HTextotherARTSponsor.ID = "txtotherARTSponsor";
                                HTextotherARTSponsor.Size = 10;
                                tc_artsponsor_1.Controls.Add(HTextotherARTSponsor);
                                tc_artsponsor_1.Controls.Add(new LiteralControl(HTextotherARTSponsor.Value));
                                tc_artsponsor_1.Controls.Add(new LiteralControl("</DIV>"));
                                tr_artsponsor.Cells.Add(tc_artsponsor_1);
                                chkartSponsor.Attributes.Add("onclick", "toggle('otherARTSponsor'); cleartxtbox();");
                            }
                            col++;
                            tblArtSponsor.Rows.Add(tr_artsponsor);
                        }

                    }
                    /******/
                    //HIV Disclosure
                    theDV = theDV = new DataView(theDSXML.Tables["Mst_HivDisclosure"]);
                    theDV.RowFilter = "SystemID=1";
                    if (theDV.Table != null)
                    {
                        DT_disclosure = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_disclosure.Rows.Count; i++)
                        {
                            HtmlTableRow trdisclosure = new HtmlTableRow();
                            HtmlTableCell tcdisclosure = new HtmlTableCell();
                            HtmlInputCheckBox chkdisclosure = new HtmlInputCheckBox();
                            chkdisclosure.ID = Convert.ToString("dis" + col);
                            chkdisclosure.Value = DT_disclosure.Rows[i][1].ToString();
                            tcdisclosure.Controls.Add(chkdisclosure);
                            tcdisclosure.Controls.Add(new LiteralControl(chkdisclosure.Value));
                            trdisclosure.Controls.Add(tcdisclosure);
                            if (chkdisclosure.Value == "Other")
                            {
                                HtmlTableCell tcdisclosureother = new HtmlTableCell();
                                tcdisclosure.Controls.Add(new LiteralControl("<LABEL class=right style='font-weight:bold' for='otherdisclosure'>"));
                                tcdisclosure.Controls.Add(new LiteralControl("<DIV id='otherDisclosure' style='DISPLAY:none'>Specify: "));

                                HtmlInputText txtdisclosureother = new HtmlInputText();
                                txtdisclosureother.ID = "txtdisclosureother";
                                txtdisclosureother.Size = 10;
                                tcdisclosure.Controls.Add(txtdisclosureother);
                                tcdisclosure.Controls.Add(new LiteralControl(txtdisclosureother.Value));
                                tcdisclosure.Controls.Add(new LiteralControl("</DIV>"));
                                trdisclosure.Cells.Add(tcdisclosure);
                                chkdisclosure.Attributes.Add("onclick", "toggle('otherDisclosure');");
                            }
                            col++;
                            tblHIVdisclosure.Rows.Add(trdisclosure);
                        }
                    }


                    theDV = new DataView(theDSXML.Tables["Mst_Occupation"]);
                    theDV.RowFilter = "SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddoccuption, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "DeleteFlag=0 and CodeID=14 and (SystemID=1 or SystemID=0)";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddtimetravelledUnits, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    /*******/
                    theDV = new DataView(theDSXML.Tables["mst_Decode"]);
                    theDV.RowFilter = "CodeID=16 and SystemID=1";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCheckedList(cblbarrierstocare2, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                    /*******/
                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeID=17 and SystemID=1";
                    ViewState["tblpatientRefEdit"] = null;
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        ViewState["tblpatientRefEdit"] = theDT;
                        theDV.Dispose();
                    }

                }
                PatientManager = null;

                /////// Add List Item /////
                ListItem theItem1 = new ListItem();
                theItem1.Text = "This Facility";
                theItem1.Attributes.Add("onclick", "javaScript:if(this.checked == true){var ans=true; alert('Please Verify that this patient already present in this facility'); if (ans==true){window.location.href='../frmFindAddPatient.aspx';};}");
                //chkartSponsor.Items.Add(theItem1); 

                ///////////////////////////

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
                PatientManager = null;
            }
        }
        private void fillpatientreferredfrom()
        {

            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(ViewState["visitPk"]) == 0)
            {
                if (ViewState["tblpatientRefAdd"] != null)
                {
                    for (int i = 0; i < ((DataTable)ViewState["tblpatientRefAdd"]).Rows.Count; i++)
                    {
                        Panel pnlIn = new Panel();
                        pnlIn.ID = "PnlIn" + i;
                        pnlprb.Controls.Add(pnlIn);

                        HtmlInputRadioButton rdopatientreferredby = new HtmlInputRadioButton();
                        rdopatientreferredby.ID = ((DataTable)ViewState["tblpatientRefAdd"]).Rows[i][0].ToString();
                        rdopatientreferredby.Value = ((DataTable)ViewState["tblpatientRefAdd"]).Rows[i][1].ToString();
                        rdopatientreferredby.Name = "Radio";
                        pnlIn.Controls.Add(rdopatientreferredby);

                        Label thepnrblbl = new Label();
                        thepnrblbl.ID = "pnlIn" + i + "_" + "lbl";
                        thepnrblbl.Text = ((DataTable)ViewState["tblpatientRefAdd"]).Rows[i][1].ToString() + "<br>";
                        thepnrblbl.Width = 20;
                        thepnrblbl.CssClass = "labelright";
                        thepnrblbl.Font.Bold = true;
                        pnlIn.Controls.Add(thepnrblbl);

                        if (rdopatientreferredby.Value == "Other facility")
                        {
                            BindFunctions lpftmgr = new BindFunctions();
                            lpftmgr.BindCombo(ddlptf, (DataTable)ViewState["LPTF"], "Name", "ID");
                            pnlIn.Controls.Add(new LiteralControl("<div id='lptf', style='display:none; padding-left:40px'>"));
                            pnlIn.Controls.Add(ddlptf);
                            pnlIn.Controls.Add(new LiteralControl("</div>"));

                            rdopatientreferredby.Attributes.Add("onclick", "down(this); toggle('lptf');");
                            rdopatientreferredby.Attributes.Add("onfocus", "up(this)");
                        }
                        else
                        {
                            rdopatientreferredby.Attributes.Add("onclick", "down(this); hide('lptf');");
                            rdopatientreferredby.Attributes.Add("onfocus", "up(this)");
                        }

                    }
                }

            }

            else if (Convert.ToInt32(ViewState["visitPk"]) > 0)
            {
                if (ViewState["tblpatientRefEdit"] != null)
                {
                    for (int i = 0; i < ((DataTable)ViewState["tblpatientRefEdit"]).Rows.Count; i++)
                    {
                        Panel pnlIn = new Panel();
                        pnlIn.ID = "PnlIn" + i;
                        pnlprb.Controls.Add(pnlIn);

                        HtmlInputRadioButton rdopatientreferredby = new HtmlInputRadioButton();
                        rdopatientreferredby.ID = ((DataTable)ViewState["tblpatientRefEdit"]).Rows[i][0].ToString();
                        rdopatientreferredby.Value = ((DataTable)ViewState["tblpatientRefEdit"]).Rows[i][1].ToString();
                        rdopatientreferredby.Name = "Radio";
                        pnlIn.Controls.Add(rdopatientreferredby);
                        rdopatientreferredby.Attributes.Add("onclick", "down(this)");
                        rdopatientreferredby.Attributes.Add("onfocus", "up(this)");

                        Label thepnrblbl = new Label();
                        thepnrblbl.ID = "pnlIn" + i + "_" + "lbl";
                        thepnrblbl.Text = ((DataTable)ViewState["tblpatientRefEdit"]).Rows[i][1].ToString() + "<br>";
                        thepnrblbl.Width = 20;
                        thepnrblbl.CssClass = "labelright";
                        thepnrblbl.Font.Bold = true;
                        pnlIn.Controls.Add(thepnrblbl);


                        if (rdopatientreferredby.Value == "Other facility")
                        {
                            BindFunctions lpftmgr = new BindFunctions();
                            lpftmgr.BindCombo(ddlptf, (DataTable)ViewState["LPTF"], "Name", "ID");
                            pnlIn.Controls.Add(new LiteralControl("<div id='lptf', style='display:none; padding-left:40px'>"));
                            pnlIn.Controls.Add(ddlptf);
                            pnlIn.Controls.Add(new LiteralControl("</div>"));

                            rdopatientreferredby.Attributes.Add("onclick", "down(this); toggle('lptf');");
                            rdopatientreferredby.Attributes.Add("onfocus", "up(this)");
                        }
                        else
                        {
                            rdopatientreferredby.Attributes.Add("onclick", "down(this); hide('lptf');");
                            rdopatientreferredby.Attributes.Add("onfocus", "up(this)");
                        }
                    }
                }
            }
        }

        # region "Custom Fields"
        // Create Custom Controls 
        // Creation Date : 16-Jan-2007 
        // Amitava Sinha
        // Updated by Sanjay on 26-June-2009
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.Enrollment));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "Enroll");
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
        //Amitava Sinha 
        //Populate Old Data in the Custom Field
        private void FillOldData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;

            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = "";
                if (theCustomFields.Tables[0].Rows.Count > 0)
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                string theColName = "";
                foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
                {
                    if (theDR["ControlId"].ToString() != "9")
                    {
                        if (theColName == "")
                            theColName = theDR["Label"].ToString();
                        else
                            theColName = theColName + "," + theDR["Label"].ToString();
                    }
                }

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, Convert.ToInt32(ViewState["visitPk"]), 0, 0, Convert.ToInt32(ApplicationAccess.Enrollment));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "Enrol");
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
        #endregion
        #endregion
        # region "Function to Maintain ViewState of Controls"
        //Jayanta Kr. Das

        private void Maintain_ViewState(Boolean prevHIVCareYes, Boolean prevARTYes, Boolean HIVdisclosureYes, Boolean rbtnsupportGroupYes)
        {
            if (prevHIVCareYes) ViewState["prevHIVCareYes"] = "1";

            if (prevARTYes) ViewState["prevARTYes"] = "1";

            if (HIVdisclosureYes) ViewState["HIVdisclosureYes"] = "1";

            if (rbtnsupportGroupYes) ViewState["rbtnsupportGroupYes"] = "1";

            //if (ChildAge.Contains("0") || ChildAge.Contains("1") || ChildAge.Contains("2") || ChildAge.Contains("3") || ChildAge.Contains("4") || ChildAge.Contains("5") || ChildAge.Contains("6") || ChildAge.Contains("7") || ChildAge.Contains("8") || ChildAge.Contains("9"))
            //{ }
            //else ChildAge = "";


            //if (ChildAge != "")
            //{
            //    if (Convert.ToInt32(ChildAge) < 5) ViewState["divHIVStatusChild"] = "1";
            //}

        }

        # endregion

        protected void Page_Init(Object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 2)
            {
                btnsave.Enabled = false;
                btncomplete.Enabled = false;
            }
            IPatientRegistration PatientMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataTable theDT = PatientMgr.theVisitIDDT(Session["PatientId"].ToString());
            if (theDT.Rows.Count > 0)
            {
                ViewState["visitPk"] = theDT.Rows[0]["Visit_ID"].ToString();
            }
            //Filling Dropdowns//Jayanta Kr. Das..12Jul2007
            Init_Form();
            fillpatientreferredfrom();
            //RTyagi..19Feb.07
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentication = new AuthenticationManager();

            if (Authentication.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(ViewState["visitPk"]) == 0)
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
            }
            else
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtageEnrollmentYears.Text = Request[this.txtageEnrollmentYears.UniqueID];
            this.txtageEnrollmentMonths.Text = Request[this.txtageEnrollmentMonths.UniqueID];
            //  Ajax.Utility.RegisterTypeForAjax(typeof(ClinicalForms_frmClinical_Enrolment));
            //(Master.FindControl("lblformname") as Label).Text = "HIV Care Enrollment";
            //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Enrollment";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Enrollment";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "HIV Care Enrollment";

            //if (Request.QueryString["locationid"] != null)
            //    Session["fmLocationID"] = Request.QueryString["locationid"].ToString();

            if (Request.QueryString["sts"] != null)
                Session["fmsts"] = Request.QueryString["sts"].ToString();
            Maintain_ViewState(rbtnprevHIVCareYes.Checked, rbtnprevARTYes.Checked, rbtnHIVdisclosureYes.Checked, rbtnsupportGroupYes.Checked);
            txtknownHIVStatus.Text = "0";

            if (rbtnknownHIVStatusNo.Checked == true)
            {
                txtknownHIVStatus.Text = "1";
            }
            ShowHideHIVDisclosure();
            Show_Hide();
            PutCustomControl();
            try
            {
                if (Request.QueryString["sts"] == "1" || Session["HIVPatientStatus"].ToString() == "1")
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
                txtenrollmentDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtenrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
                txtmonthlyIncome.Attributes.Add("onkeyup", "chkNumeric('" + txtmonthlyIncome.ClientID + "')");
                txtChildren.Attributes.Add("onkeyup", "chkNumeric('" + txtChildren.ClientID + "')");
                txtPeopleHousehold.Attributes.Add("onkeyup", "chkNumeric('" + txtPeopleHousehold.ClientID + "')");
                txtdistanceTravelled.Attributes.Add("onkeyup", "chkNumeric('" + txtdistanceTravelled.ClientID + "')");
                txttimeTravelled.Attributes.Add("onkeyup", "chkNumeric('" + txttimeTravelled.ClientID + "')");
                txtHouseholdHIVPositive.Attributes.Add("onkeyup", "chkNumeric('" + txtHouseholdHIVPositive.ClientID + "')");
                txtHouseholdHIVTest.Attributes.Add("onkeyup", "chkNumeric('" + txtHouseholdHIVTest.ClientID + "')");
                numHouseholdHIVDied.Attributes.Add("onkeyup", "chkNumeric('" + numHouseholdHIVDied.ClientID + "')");
                rbtnPrevHIVCareNo.Attributes.Add("OnClick", "down(this); hide('caretype'); hide('otherCareTypes'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_', '" + tblreceiveHIVCare.Rows.Count + "');");
                rbtnprevARTNo.Attributes.Add("OnClick", "down(this); hide('artSponsor');hide('medRecordsdiv'); hide('otherARTSponsor'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_r', '" + tblArtSponsor.Rows.Count + "');");
                rbtnprevARTUnknown.Attributes.Add("OnClick", "down(this); hide('artSponsor');hide('medRecordsdiv'); hide('otherARTSponsor'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_r', '" + tblArtSponsor.Rows.Count + "');");
                rbtnHIVdisclosureNo.Attributes.Add("OnClick", "down(this); hide('showHIVdisclosureName'); hide('otherDisclosure'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_dis', '" + tblHIVdisclosure.Rows.Count + "');");
                cblbarrierstocare2.Attributes.Add("nowrap", "nowrap");

                foreach (ListItem li in cblbarrierstocare2.Items)
                {
                    li.Attributes.Add("onclick", "CheckItem('" + cblbarrierstocare2.ClientID + "','" + li.Value + "');");
                }

                if (!IsPostBack)
                {

                    ViewState["ptnid"] = "";
                    LoadPatientData();
                    DOB_Attributes();
                    //if (Request.QueryString["name"] == "Add")
                    if (Convert.ToInt32(ViewState["visitPk"]) == 0)
                    {
                        //if (Request.QueryString["PatientID"] != null)
                        if (Convert.ToInt32(Session["PatientID"]) > 0)
                        {

                            ViewState["ptnid"] = Session["PatientID"];
                        }
                    }
                    //if (Request.QueryString["name"] == "Edit")
                    if (Convert.ToInt32(ViewState["visitPk"]) > 0)
                    {
                        //For DataQuality Button
                        MsgBuilder theDQ = new MsgBuilder();
                        theDQ.DataElements["Name"] = "DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close";
                        IQCareMsgBox.ShowConfirm("Enrolment", theDQ, theBtnDQ);

                        //For Save button
                        MsgBuilder theSave = new MsgBuilder();
                        theSave.DataElements["Name"] = "Form updated successfully. Do you want to close";
                        IQCareMsgBox.ShowConfirm("Enrolment", theSave, btnOk);
                    }

                }
                Page.EnableViewState = true;
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
                PatientManager = null;
            }
        }
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string GetDuplicateRecord(string strfname, string strmname, string strlname, string address, string Phone)
        {
            IPatientRegistration PatientManager;
            StringBuilder objBilder = new StringBuilder();
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet dsPatient = new DataSet();
            dsPatient = PatientManager.GetDuplicatePatientSearchResults(strlname, strmname, strfname, address, Phone);

            if (dsPatient.Tables[0].Rows.Count > 0)
            {
                objBilder.Append("<table border='0'  width='100%'>");
                objBilder.Append("<tr style='background-color:#e1e1e1'>");
                objBilder.Append("<td class='smallerlabel'>PatientID</td>");
                objBilder.Append("<td class='smallerlabel'>F name</td>");
                objBilder.Append("<td class='smallerlabel'>L name</td>");
                objBilder.Append("<td class='smallerlabel'>Enrollment Date</td>");
                objBilder.Append("<td class='smallerlabel'>Existing Hosp/Clinic #</td>");
                objBilder.Append("<td class='smallerlabel'>Chief/Local Council</td>");
                objBilder.Append("<td class='smallerlabel'>Phone</td>");
                objBilder.Append("<td class='smallerlabel'>Facility</td>");
                objBilder.Append("</tr>");
                for (int i = 0; i < dsPatient.Tables[0].Rows.Count; i++)
                {
                    objBilder.Append("<tr>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientEnrollmentID"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["firstname"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["lastname"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientClinicID"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["LocalCouncil"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["Phone"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["FacilityName"].ToString() + "</td>");
                    objBilder.Append("</tr>");
                }
                objBilder.Append("</table>");
            }
            return objBilder.ToString();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                Show_Hide();
                return;
            }

            try
            {
                BuildParameterHashTable();
                PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                //if (Request.QueryString["name"] == "Edit")
                if (Convert.ToInt32(ViewState["visitPk"]) > 0)
                {
                    DataTable dtcaretype = AddHIVCareTypes();
                    DataTable dtartsponsor = AddARTSponsor();
                    DataTable dtdis = AddDisclosure();
                    DataTable dtBarrier = AddBarrier();
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);
                    DataSet PatientId = PatientManager.InsertUpdatePatientRegistration(htParameters, dtcaretype, dtartsponsor, dtdis, dtBarrier, Convert.ToInt32(ViewState["visitPk"]), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                    btncomplete.CssClass = "none";
                    RecordSave_Update();
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
                PatientManager = null;
            }

        }
        protected void DOB_Attributes()
        {
            IIQCareSystem SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = SystemManager.SystemDate();
            SystemManager = null;

            txtSysDate.Text = theCurrentDate.ToString(Session["AppDateFormat"].ToString());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";

            if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "1")
            {
                //if (Request.QueryString["name"] == "Add")
                if (Convert.ToInt32(ViewState["visitPk"]) == 0)
                {

                    string theUrl;
                    theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
                    Response.Redirect(theUrl);
                }

                //else if (Request.QueryString["name"] == "Edit")
                if (Convert.ToInt32(ViewState["visitPk"]) > 0)
                {
                    string theUrl;
                    // theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "frmPatient_History.aspx");

                    Response.Redirect(theUrl);
                }
            }
            else
            {
                //if (Request.QueryString["name"] == "Add")
                if (Convert.ToInt32(ViewState["visitPk"]) == 0)
                {

                    if (ViewState["ptnid"].ToString() == "")
                    {
                        Response.Redirect("~/frmFindAddPatient.aspx");
                    }
                    else
                    {
                        string theUrl;
                        theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", ViewState["ptnid"].ToString());
                        Response.Redirect(theUrl);
                    }
                }
                else
                {
                    string theUrl;
                    // theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "frmPatient_History.aspx");
                    Response.Redirect(theUrl);
                }
            }
        }

        #region "Sanjay"
        private void RecordSave_Update()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
            script += "document.getElementById('" + btnOk.ClientID + "').click();\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "aftersavefunction", script);
            return;
        }

        private void RecordSave_UpdateDQ()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'afterupdatefunction'>\n";
            script += "document.getElementById('" + theBtnDQ.ClientID + "').click();\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "afterupdatefunction", script);
            return;
        }
        #endregion

        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + ViewState["ptnid"].ToString() + "';\n";
            //script += "}\n";
            //script += "else \n";
            //script += "{\n";
            //script += "window.location.href('frmClinical_Enrolment.aspx?name=Edit&PatientId=" + ViewState["ptnid"].ToString() + "&sts=" + 0 + "');\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void DQCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + ViewState["ptnid"].ToString() + "';\n";
            //script += "}\n";
            //script += "else \n";
            //script += "{\n";
            //script += "window.location.href('frmClinical_Enrolment.aspx?name=Edit&PatientId=" + ViewState["ptnid"].ToString() + "&sts=" + 0 + "');\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string theUrl;
            if (Convert.ToInt32(ViewState["visitPk"]) == 0 && ViewState["ptnid"] != null)
            {
                SaveCancel();
            }
            else if (Convert.ToInt32(ViewState["visitPk"]) == 0 && ViewState["ptnid"] == null)
            {
                theUrl = string.Format("{0}", "../frmFindAddPatient.aspx");
                Response.Redirect(theUrl);
            }
            else if (Convert.ToInt32(ViewState["visitPk"]) > 0 && ViewState["ptnid"] != null)
            {
                //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), "0");
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }

        }
        protected void btncomplete_Click(object sender, EventArgs e)
        {
            string msg = DataQuality_Msg();
            if (msg.Length > 69)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }
            else
            {
                ViewState["DataQualityFlag"] = "1";
            }
            if (FieldValidation_DQ() == false)
            {
                Show_Hide();
                return;
            }

            try
            {
                BuildParameterHashTable();
                PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                //if (Request.QueryString["name"] == "Edit")
                if (Convert.ToInt32(ViewState["visitPk"]) > 0)
                {
                    DataTable dtcaretype = AddHIVCareTypes();
                    DataTable dtartsponsor = AddARTSponsor();
                    DataTable dtdis = AddDisclosure();
                    DataTable dtBarrier = AddBarrier();
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);
                    DataSet PatientId = PatientManager.InsertUpdatePatientRegistration(htParameters, dtcaretype, dtartsponsor, dtdis, dtBarrier, Convert.ToInt32(ViewState["visitPk"]), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                    btncomplete.CssClass = "greenbutton";
                    RecordSave_UpdateDQ();
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
                PatientManager = null;
            }
        }
        protected void theBtnDQ_Click(object sender, EventArgs e)
        {
            string theUrl;
            if (Convert.ToInt32(ViewState["visitPk"]) == 0 && ViewState["ptnid"] != null)
            {
                DQCancel();
            }
            else if (Convert.ToInt32(ViewState["visitPk"]) == 0 && ViewState["ptnid"] == null)
            {
                theUrl = string.Format("{0}", "../frmFindAddPatient.aspx");
                Response.Redirect(theUrl);
            }
            else if (Convert.ToInt32(ViewState["visitPk"]) > 0 && ViewState["ptnid"] != null)
            {
                //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), "0");
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
        }

        protected void ddvillageName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnFamilyInfo_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                Show_Hide();
                return;
            }
            //if (Request.QueryString["PatientId"] != null)
            if (Convert.ToInt32(Session["PatientID"]) > 0)
            {
                string strfy = string.Empty;
                //string theUrl = string.Format("{0}?PatientId={1}&sts={2}&strfy={3}", "frmFamilyInformation.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString(), "1");
                string theUrl = string.Format("{0}", "frmFamilyInformation.aspx");
                Response.Redirect(theUrl);
            }
        }
    }

}