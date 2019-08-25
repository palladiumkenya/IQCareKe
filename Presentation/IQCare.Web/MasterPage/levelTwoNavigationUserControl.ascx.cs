using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using System.Data;
using System.Collections;
using IQCare.Web.UILogic;

namespace IQCare.Web.MasterPage
{
    public partial class levelTwoNavigationUserControl : System.Web.UI.UserControl
    {
        public int PatientId = 0;
        private string ARTNos = "";
        private string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";

        private string PMTCTNos = "";

        private int PtnARTStatus;

        //string ModuleId = "";
        private int PtnPMTCTStatus;
        public static void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;

            if ((String.IsNullOrEmpty(target) ||
                target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
                String.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException(
                        "Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);

                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }

                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page,
                    typeof(Page),
                    "Redirect",
                    script,
                    true);
            }
        }

        //Dynamic Forms
        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        // [WebMethod(EnableSession = true), ScriptMethod]
        public void SetDynamic_Session(string id)
        {
            //System.Windows.Forms.MessageBox.Show("Executed setdynamicsession");
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["FeatureID"] = id;
            HttpContext.Current.Session["LabID"] = HttpContext.Current.Session["Lab_ID"] = null;

        }

        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        // [WebMethod(EnableSession = true), ScriptMethod]
        public void SetPatientId_Session()
        {
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["LabId"] = 0;
            HttpContext.Current.Session["LabID"] = HttpContext.Current.Session["Lab_ID"] = null;
        }
        /*
            public void setSessionIds_Patient()
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("Menu Script"))
                {
                    string script = "\n window.onload = function(){";
                    script += "\n var menuTable = document.getElementById('" + patientLevelMenu.ClientID + "');";
                    script += "\n if (menuTable == null || typeof (menuTable) == \"undefined\") return;";
                    script += "\n var menuLinks = menuTable.getElementsByTagName('a');";

                    script += "\n   for(i=0;i<menuLinks.length;i++)";
                    script += "\n     {";
                    //script += "\n       menuLinks[i].onclick = function(){return confirm('u sure to postback?');}";
                    script += "\n       menuLinks[i].onclick = function(){ MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();}";
                    script += "\n     }";
                    script += "\n   setOnClickForNextLevelMenuItems(menuTable.nextSibling);";
                    script += "\n }";//window onload close
                    script += "\n function setOnClickForNextLevelMenuItems(currentMenuItemsContainer){";
                    script += "\n   var id = currentMenuItemsContainer.id;";
                    script += "\n if (id == null || typeof (id) == \"undefined\") return;";
                    script += "\n   var len = id.length;";
                    script += "\n     if(id != null && typeof(id) != 'undefined' && id.substring(0,parseInt(len)-7) == '" + patientLevelMenu.ClientID + "' && id.substring(parseInt(len)-5,parseInt(len)) == 'Items')";
                    script += "\n      {";
                    script += "\n        var subMenuLinks = currentMenuItemsContainer.getElementsByTagName('a');";
                    script += "\n        for(i=0;i<subMenuLinks.length;i++)";
                    script += "\n          {";
                    //script += "\n            subMenuLinks[i].onclick = function(){return confirm('u sure to postback?');}";
                    script += "\n            subMenuLinks[i].onclick = function(){MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();}";
                    script += "\n          }";
                    script += "\n        setOnClickForNextLevelMenuItems(currentMenuItemsContainer.nextSibling);";
                    script += "\n      }";
                    script += "\n }";
                }
            }
            */
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Execute the control")]
        [System.ComponentModel.Bindable(true)]
        public bool CanExecute
        {
            get
            {
                if (this.hLoad.Value == "")
                    this.hLoad.Value = "FALSE";
                return bool.Parse(this.hLoad.Value);

            }
            set
            {
                this.hLoad.Value = value.ToString().ToUpper();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //setSessionIds_Patient();

            try
            {
                if (!IsPostBack && Convert.ToInt32(Session["PatientId"]) > 0 && this.CanExecute)
                {
                    this.SetUrls();

                    CurrentSession currentSession = CurrentSession.Current;
                    if (!currentSession.HasPatient)
                    {
                        currentSession = currentSession.SetCurrentPatient(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
                    }

                    DataTable dtForms = currentSession.CurrentFormSet;

                    if (currentSession.CurrentEnrollment != null)
                    {
                        if (currentSession.CurrentEnrollment.CareStatus == "Active")
                        {
                            if (dtForms != null && dtForms.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtForms.Rows)
                                {

                                    MenuItem child = new MenuItem(row["FormDescription"].ToString(), "DYN" + row["FeatureId"].ToString());
                                    patientLevelMenu.Items[4].ChildItems.Add(child);
                                }
                            }
                        }
                        else
                        {
                            this.DisableMenuItems("mnuScheduleAppointment", "mnuWaitingList", "mnuPatientTransfer", "mnuCreateNewForm");

                        }
                    }
                    if (dtForms != null && dtForms.Rows.Count > 0)
                    {
                        dtForms.DefaultView.RowFilter = "code = 'CONSULTATION'";
                        DataTable dtClinicalForms = dtForms.DefaultView.ToTable();
                        dtForms.DefaultView.RowFilter = "Code='CARE_END'";
                        DataTable dtCareEnd = dtForms.DefaultView.ToTable();
                        dtForms.DefaultView.RowFilter = "";
                    }
                    string url = Request.RawUrl.ToString();
                    Application["PrvFrm"] = url;
                    Init_Menu();
                    AuthenticationRights();
                }




                // Load_MenuRegistration();
                // Load_MenuCreateNewForm();




            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        private void DisableMenuItems(params string[] mnus)
        {
            foreach (string str in mnus)
            {
                MenuItem item = patientLevelMenu.FindItem(str);
                if (item != null)
                {
                    item.Enabled = false;
                    item.NavigateUrl = "";
                }
            }
        }
        private void SetUrls()
        {
            bool isPaperless = CurrentSession.Current.Facility.PaperLess;

            //  string thePaperUrl = string.Format("{0}sts={1}", "../Laboratory/LabRecordEntry.aspx?", PtnPMTCTStatus);
            //  string theUrl = string.Format("{0}sts={1}", "../Laboratory/LabRequestForm.aspx?", PtnPMTCTStatus);
            //  MenuItemCollection mnuLab = patientLevelMenu.Items[4].ChildItems;
            //  //.ChildItems["mnuLabOrder"];
            //MenuItem item =  mnuLab.AsQueryable().Cast<MenuItem>().Where(c => c.Value == "mnuLabOrder").FirstOrDefault();
            //  if (item != null)
            //  {
            //      item.NavigateUrl = isPaperless ? theUrl : thePaperUrl;
            //  }
            Utility objUtil = new Utility();
            string returnUrl = objUtil.Encrypt(("ClinicalForms/frmPatient_Home.aspx"));
            MenuItem mnuWaitingList = patientLevelMenu.FindItem("mnuWaitingList");
            if (mnuWaitingList != null)
            {
                if (isPaperless)
                {


                    mnuWaitingList.NavigateUrl = string.Format("~/Queue/PatientWaitingList.aspx?PID={0}&hashtag={1}", PatientId, returnUrl);
                }
                else
                {
                    patientLevelMenu.Items.Remove(mnuWaitingList);
                }
            }

            MenuItem mnuScheduler = patientLevelMenu.FindItem("mnuScheduleAppointment");
            if (mnuScheduler != null)
            {
                mnuScheduler.NavigateUrl = string.Format("{0}&&hashtag={1}", "~/Scheduler/frmScheduler_AppointmentNewHistory.aspx?name=Add", returnUrl);

            }
            MenuItem mnReport = patientLevelMenu.FindItem("mnuReport");
            Guid g = Guid.NewGuid();
            var item = (from MenuItem i in mnReport.ChildItems
                        //where i.Value == "mnuDrugPickUp"
                        select i
                      ).ToList();
            if (item != null)
            {
                MenuItem mnudrugPickup = item.Where(mn => mn.Value == "mnuDrugPickUp").FirstOrDefault();

                if (mnudrugPickup != null)
                {
                    g = Guid.NewGuid();
                    mnudrugPickup.NavigateUrl = string.Format("{0}&key={1}", "~/Reports/frmReport_PatientARVPickup.aspx?name=Add", g.ToString());
                }
                MenuItem mnuProfile = item.Where(mn => mn.Value == "mnuPatientProfile").FirstOrDefault();
                if (mnuProfile != null)
                {
                    g = Guid.NewGuid();
                    mnuProfile.NavigateUrl = string.Format("~/Reports/frmReportViewer.aspx?name=Add&ReportName=PatientProfile&key={0}", g.ToString());
                }
                MenuItem mnuDebit = item.Where(mn => mn.Value == "mnuDebitNote").FirstOrDefault();
                if (mnuDebit != null)
                {
                    g = Guid.NewGuid();
                    mnuDebit.NavigateUrl = string.Format("~/Reports/frmReportDebitNote.aspx?name=Add&key={0}", g.ToString());
                }
            }


            MenuItem mnuRef = patientLevelMenu.FindItem("mnuRegistrationMain");
            if (mnuRef != null)
            {
                 item = (from MenuItem i in mnuRef.ChildItems
                                //where i.Value == "mnuDrugPickUp"
                            select i
                      ).ToList();
                if(item != null)
                {
                    MenuItem mnuReg = item.Where(mn => mn.Value == "mnuRegistration").FirstOrDefault();
                    if(mnuReg  != null)
                    {
                        g = Guid.NewGuid();
                        mnuReg.NavigateUrl = string.Format("~/Patient/Registration.aspx?name=edit&key={0}", g.ToString());
                    }
                    MenuItem mnuEnrol = item.Where(mn => mn.Value == "mnuEnrollment").FirstOrDefault();
                    if (mnuEnrol != null)
                    {
                        g = Guid.NewGuid();
                        mnuEnrol.NavigateUrl = string.Format("~/Patient/AddTechnicalArea.aspx?name=edit&key={0}", g.ToString());
                    }
                }
              
                
            }
            mnuRef = patientLevelMenu.FindItem("mnuExistingForms");
            if (mnuRef != null)
            {
                g = Guid.NewGuid();
                mnuRef.NavigateUrl = string.Format("~/ClinicalForms/frmPatient_History.aspx?name=view&key={0}", g.ToString());
            }

            mnuRef = patientLevelMenu.FindItem("mnuClinicalDeleteForm");
            if (mnuRef != null)
            {
                g = Guid.NewGuid();
                mnuRef.NavigateUrl = string.Format("~/ClinicalForms/frmClinical_DeleteForm.aspx?name=Del&key={0}", g.ToString());
            }

            mnuRef = patientLevelMenu.FindItem("mnuPatientTransfer");
            if (mnuRef != null)
            {
                g = Guid.NewGuid();
                mnuRef.NavigateUrl = string.Format("~/ClinicalForms/frmClinical_Transfer.aspx?name=Add&key={0}", g.ToString());
            }





        }



        #region "Hide menu item by value"

        public void RemoveMenuItemByValue(MenuItemCollection items, String value)
        {
            List<MenuItem> rmvMenuItem = new List<MenuItem>();

            //Breadth first, look in the collection
            foreach (MenuItem item in items)
            {
                if (item.Value == value)
                {
                    rmvMenuItem.Add(item);
                    
                }
            }

            if (rmvMenuItem.ToArray().Length != 0)
            {
                for (int j = 0; j < rmvMenuItem.ToArray().Length; j++)
                {
                    items.Remove(rmvMenuItem[j]);
                }
            }

            //Search children
            foreach (MenuItem item in items)
            {
                RemoveMenuItemByValue(item.ChildItems, value);
            }
        }

        #endregion "Hide menu item by value"

        #region "Assign URL by value"



        #endregion "Assign URL by value"

        #region "Assign Attributes"

        //patientLevelMenu.Attributes.Add("onClick", "fnSetformID('" + theDR["FeatureID"].ToString() + "');");
        public void AssignAttribute(MenuItemCollection items, String value, String url)
        {
            foreach (MenuItem item in items)
            {
                if (item.Value == value)
                {
                    //patientLevelMenu.Attributes.Add("onClick", "window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script type='text/javascript'>function openWin1() { window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=700,scrollbars=yes');};</script>");

                    //item.NavigateUrl = "javascript:window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');";
                    item.NavigateUrl = "javascript:openWin1();";
                }
            }

            foreach (MenuItem item in items)
            {
                AssignAttribute(item.ChildItems, value, url);
            }
        }

        #endregion "Assign Attributes"

        //#region "Disable Menu Items"

        private void disableMenuItem()
        {
            patientLevelMenu.Items[0].Selectable = false;
            for (int i = 0; i < patientLevelMenu.Items[0].ChildItems.Count; i++)
            {
                patientLevelMenu.Items[0].ChildItems[i].Selectable = false;
            }

            patientLevelMenu.Items[4].Selectable = false;
            for (int i = 0; i < patientLevelMenu.Items[4].ChildItems.Count; i++)
            {
                patientLevelMenu.Items[4].ChildItems[i].Selectable = false;
            }
        }

        //#endregion "Disable Menu Items"
        #region "Authentication Clinical Header"

        private void AuthenticationRights()
        {
            if (Session["TechnicalAreaId"] == null)
            {
            }
            else
            {
                string ModuleId;
                DataView theDV = new DataView((DataTable)Session["UserRight"]);
                if (Session["TechnicalAreaId"] != null || Session["TechnicalAreaId"].ToString() != "")
                {
                    if (Convert.ToInt32(Session["TechnicalAreaId"].ToString()) != 0)
                    {
                        ModuleId = "0," + Session["TechnicalAreaId"].ToString();
                    }
                    else
                        ModuleId = "0";
                }
                else
                    ModuleId = "0";
                theDV.RowFilter = "ModuleId in (" + ModuleId + ")";
                DataTable theDT = new DataTable();
                theDT = theDV.ToTable();

                if (PMTCTNos != null && PMTCTNos == "")
                {
                }

                ////////////////////////////////////

                AuthenticationManager Authentication = new AuthenticationManager();

                if (Authentication.HasFeatureRight(ApplicationAccess.AdultPharmacy, theDT) == false)
                {
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacyPMTCT");
                }
                if (Authentication.HasFeatureRight(ApplicationAccess.ARTFollowup, theDT) == false)
                {
                    //mnuFollowupART.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupART");
                }


                if (Authentication.HasFeatureRight(ApplicationAccess.HomeVisit, theDT) == false)
                {
                    //mnuHomeVisit.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHomeVisit");
                }
                if (Authentication.HasFeatureRight(ApplicationAccess.InitialEvaluation, theDT) == false)
                {
                    //mnuInitEval.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInitEval");
                }



                if (Authentication.HasFeatureRight(ApplicationAccess.NonARTFollowup, theDT) == false)
                {
                    //mnuNonARTFollowUp.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNonARTFollowUp");
                }

                if (Authentication.HasFeatureRight(ApplicationAccess.DeleteForm, theDT) == false)
                {
                    //mnuClinicalDeleteForm.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuClinicalDeleteForm");
                }
                if (Authentication.HasFeatureRight(ApplicationAccess.PatientARVPickup, theDT) == false)
                {
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientProfile");
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuDrugPickUp");
                }
                if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, theDT) == false)
                {
                    //mnuScheduleAppointment.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuScheduleAppointment");
                }

                if (Authentication.HasFeatureRight(ApplicationAccess.SchedularAppointment, theDT) == false)
                {
                    //mnuScheduleAppointment.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuScheduleAppointment");
                }

                if (Authentication.HasFeatureRight(ApplicationAccess.FamilyInfo, theDT) == false)
                {
                    //mnuFamilyInformation.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFamilyInformation");
                }

                if (Authentication.HasFeatureRight(ApplicationAccess.ChildEnrollment, theDT) == false)
                {
                    //mnuInfantFollowUp.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInfantFollowUp");
                }

                if (Authentication.HasFeatureRight(ApplicationAccess.PatientClassification, theDT) == false)
                {
                    //mnuPatientClassification.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientClassification");
                }
                if (Authentication.HasFeatureRight(ApplicationAccess.FollowupEducation, theDT) == false)
                {
                    //mnuFollowupEducation.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
                }
                //else
                //{
                //    DataSet theDS = (DataSet)ViewState["AddForms"];
                //    DataView theFormDV = new DataView(theDS.Tables[1]);
                //    theFormDV.RowFilter = "FeatureId=" + ApplicationAccess.FollowupEducation.ToString();
                //    if (theFormDV.Count < 1)
                //        //mnuFollowupEducation.Visible = false;
                //        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
                //}

                if (Authentication.HasFeatureRight(ApplicationAccess.Transfer, theDT) == false)
                {
                    //mnuPatientTranfer.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientTransfer");
                }

                //if (Authentication.HasFeatureRight(ApplicationAccess.Dashboard, theDT) == false)
                //{
                //    RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "Dashboard");
                //}
                //if (Authentication.HasFeatureRight(ApplicationAccess.Dispense, theDT) == false)
                //{
                //    RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "Dispense");
                //}
                //if (Authentication.HasFeatureRight(ApplicationAccess.StockSummaryWeb, theDT) == false)
                //{
                //    RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "StockSummaryWeb");
                //}
                //if (Authentication.HasFeatureRight(ApplicationAccess.StockManagement, theDT) == false)
                //{
                //    RemoveMenuItemByValue(PharmacyDispensingMenu.Items, "StockManagement");
                //}


            }
        }

        #endregion "Authentication Clinical Header"

     //   #region "Load Menu"

        //public void LoadCreateNewMenu(String url, int i)
        //{
        //    //for (int i = patientLevelMenu.Items[4].ChildItems.Count - 1; i >= 0; i--)
        //    //{
        //    //    patientLevelMenu.Items[4].ChildItems.RemoveAt(i);
        //    //}
        //    try
        //    {
        //        if (!IsPostBack)
        //        {
        //            IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

        //            DataSet technicalAreaandFormName = PatientHome.GetTechnicalAreaandFormName(Convert.ToInt32(Session["TechnicalAreaId"]));
        //            DataTable datatable = new DataTable();
        //            if (technicalAreaandFormName.Tables[1].Rows.Count > 0)
        //                datatable = this.BindFormBusinessRules(technicalAreaandFormName.Tables[1], technicalAreaandFormName.Tables[3]);

        //            MenuItem child = new MenuItem(datatable.Rows[i]["FeatureName"].ToString(), url);
        //            if (Convert.ToInt32(datatable.Rows[i]["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
        //            {
        //                child = new MenuItem("Order Lab Tests", url);

        //                patientLevelMenu.Items[4].ChildItems.Add(child);
        //            }
        //            else
        //            {
        //                patientLevelMenu.Items[4].ChildItems.Add(child);
        //            }

        //            DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
        //            // string CareEnded = string.Empty;
        //            if (theCEntedStatusDT != null)
        //            {
        //                if (theCEntedStatusDT.Rows.Count > 0)
        //                {
        //                    // CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
        //                    if (Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]) == "1")
        //                    {
        //                        disableMenuItem();
        //                    }
        //                }
        //            }
        //            technicalAreaandFormName.Dispose();
        //            PatientHome = null;
        //        }
        //    }
        //    catch { }
        //}

      //  #endregion "Load Menu"

        #region "Load Partial Menu"

        public void LoadCreatePartialMenu(String url, int CountryId)
        {
            ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
            var item = (from MenuItem i in patientLevelMenu.Items
                        where i.Value == "mnuCreateNewForm"
                        select i
                       ).FirstOrDefault();
            if (item != null)
            {

                foreach (DataRow dr in theDS.Tables[0].Rows)
                {
                    MenuItem child = new MenuItem(dr["FeatureName"].ToString(), "mnu" + dr["FeatureID"].ToString(), "", url);
                    ((MenuItem)item).ChildItems.Add(child);
                }
            }

        }

        #endregion "Load Partial Menu"

        #region "Divs"

        private void AdditionalForms()
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFamilyInformation");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientClassification");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuExposedInfant");
        }

        private void ClinicID()
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInitEval");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupART");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNonARTFollowUp");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrder");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTest");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHomeVisit");
            //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAdultPharmacy");
        }

        private void DivDynModule()
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderDynm");
        }

        private void divKenyaBlueCard(bool showArtVisit = false)
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTHistory");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTTherapy");
            if (!showArtVisit)
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTVisit");
        }

        private void divPMTCT()
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacyPMTCT");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderPMTCT");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTestPMTCT");
        }

        private void divUgandaBlueCard()
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPriorARTHIVCare");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTCare");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHIVCareARTEncounter");
        }

        private void EnrolmentARTPMTCT()
        {
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuEnrolment");
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPMTCTEnrol");
        }
        #endregion "Divs"

        protected void patientLevelMenu_MenuItemClick1(object sender, MenuEventArgs e)
        {
            if (e.Item.Value.ToString().StartsWith("DYN"))
            {
                string strFeatureId = e.Item.Value.ToString().Replace("DYN", "");
                SetPatientId_Session();
                SetDynamic_Session(strFeatureId);

                DataRow[] rows = CurrentSession.Current.CurrentFormSet.Select("FeatureId=" + strFeatureId);
                if (rows.Length > 0)
                {
                    string url = rows[0]["Url"].ToString();
                    if (url.Contains('|'))
                    {
                        string[] urlParts = url.Split('|');
                        url = urlParts[0];
                    }
                    Response.Redirect(url);
                }

            }
            //if (e.Item.Value.ToString().Contains('|'))
            //{
            //    string[] urlParts = e.Item.Value.Split('|');
            //    SetPatientId_Session();
            //    SetDynamic_Session(urlParts[1]);
            //    Response.Redirect(urlParts[0]);
            //}
            else
            {
                SetPatientId_Session();

                string itemValue = e.Item.Value.ToString();
                string itemUrl = e.Item.NavigateUrl.ToString();
                string theUrl;
                string _patientID = PatientId.ToString();
                string _satellite = Session["AppSatelliteId"].ToString();
                string _countryId = Session["AppCountryId"].ToString();
                string _posId = Session["AppPosID"].ToString();
                string _pntStatus = lblpntStatus.Text;

                switch (e.Item.Value)
                {



                    //case "mnuPatienHome":
                    //    theUrl = string.Format("{0}&PatientId={1}&LocationId={2}&FormName={3}&sts={4}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", Convert.ToInt32(Request.QueryString["PatientId"]), Session["AppLocationId"].ToString(), "PatientHome", lblpntStatus.Text);
                    //    break;
                    //case "mnuScheduleAppointment":
                    //    theUrl = string.Format("{0}&PatientId={1}&LocationId={2}&FormName={3}&sts={4}", "../Scheduler/frmScheduler_AppointmentNewHistory.aspx?name=Add", Convert.ToInt32(Request.QueryString["PatientId"]), Session["AppLocationId"].ToString(), "PatientHome", lblpntStatus.Text);
                    //    break;

                    case "mnuFamilyInformation":
                        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmFamilyInformation.aspx?name=Add", PatientId.ToString());
                        break;

                    case "mnuPatientClassification":
                        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmClinical_PatientClassificationCTC.aspx?name=Add", PatientId.ToString());
                        break;

                    case "mnuFollowupEducation":
                        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmFollowUpEducationCTC.aspx?name=Add", PatientId.ToString());
                        break;

                    case "mnuExposedInfant":
                        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmExposedInfantEnrollment.aspx", PatientId.ToString());
                        break;

                    case "mnuPriorARTHIVCare":
                        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frm_PriorArt_HivCare.aspx", PatientId.ToString());
                        break;

                    case "mnuARTCare":
                        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARTCare.aspx");
                        break;

                    case "mnuHIVCareARTEncounter":
                        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_HIVCareARTCardEncounter.aspx");
                        break;

                    //case "mnuARTVisit":
                    //    theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_InitialFollowupVisit.aspx", PatientId.ToString());
                    //    break;

                    //case "mnuARTTherapy":
                    //    theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_ARVTherapy.aspx", PatientId.ToString());
                    //    break;

                    //case "mnuARTHistory":
                    //    theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_ARTHistory.aspx", PatientId.ToString());
                    //    break;

                    //case "mnuPMTCTEnrol":
                    //case "mnuRegistration":
                    //    theUrl = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    //    break;

                    case "mnuEnrolment":
                        theUrl = this.GetEnrollmentUrl();
                        break;

                    case "mnuInitEval":
                        theUrl = "../ClinicalForms/frmClinical_InitialEvaluation.aspx"; break;
                    case "mnuFollowupART":
                        theUrl = "../ClinicalForms/frmClinical_ARTFollowup.aspx";
                        break;

                    case "mnuNonARTFollowUp":
                        theUrl = "../ClinicalForms/frmClinical_NonARTFollowUp.aspx";
                        break;

                    //case "mnuPharmacy":
                    //    theUrl = "../Pharmacy/frmPharmacyForm.aspx?Prog=ART";
                    //    break;

                    //case "mnuLabOrder":
                    //    theUrl = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnARTStatus);
                    //    break;

                    case "mnuHomeVisit":
                        theUrl = string.Format("{0}", "../Scheduler/frmScheduler_HomeVisit.aspx");
                        break;

                    //case "mnuPharmacyPMTCT":
                    //    theUrl = "../Pharmacy/frmPharmacyForm.aspx?Prog=PMTCT";
                    //    break;

                    //case "mnuLabOrderPMTCT":
                    //    theUrl = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnPMTCTStatus);
                    //    break;

                    //case "mnuOrderLabTestPMTCT":
                    //    theUrl = string.Format("{0}sts={1}", "../Laboratory/LabOrderForm.aspx?", PtnPMTCTStatus);
                    //    break;




                    //case "mnuXRay":
                    //    theUrl = string.Format("{0}&sts={1}", "../Laboratory/XRayOrderForm.aspx?name=Add", lblpntStatus.Text);
                    //    break;

                    default:
                        theUrl = itemValue;
                        break;

                }
                Response.Redirect(theUrl);
                //  Response.Redirect(e.Item.Value.ToString());
                //}
            }
        }

        /// <summary>
        /// Sets the enrollment URL.
        /// </summary>
        /// <returns></returns>
        private string GetEnrollmentUrl()
        {
            string theUrl = "";
            if (ARTNos != null && ARTNos == "")
            {
                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Add", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                }
            }
            else if (PMTCTNos != null && PMTCTNos == "")
            {
                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                }
            }
            else
            {
                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                }
            }
            return theUrl;
        }

        private int getVisitTypeID(String pageName)
        {
            try
            {
                switch (pageName.TrimEnd(".aspx".ToCharArray()))
                {
                    case "frmClinical_ARTHistory":
                        return 18;
                    case "frmClinical_ARVTherapy":
                        return 19;
                    case "frmClinical_InitialFollowupVisit":
                        return 17;
                    case "frmPharmacyForm":
                        return 4;
                    case "frmLabOrder":
                    case "frmDynamicLab":
                    case "LabRequestForm.aspx":
                    case "LabRecordEntry.aspx":
                        return 6;
                    case "frmClinical_CustomForm":
                        return int.Parse(HttpContext.Current.Session["FeatureID"].ToString());
                    default:
                        return 99;
                }
            }
            catch
            {
                return 99;
            }
        }

        /// <summary>
        /// Init_s the menu.
        /// </summary>
        private void Init_Menu()
        {
            //IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
            // DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
            //ViewState["AddForms"] = theDS;

            if (Convert.ToInt32(Session["PatientId"]) != 0)
                PatientId = Convert.ToInt32(Session["PatientId"]);

            if (PatientId == 0)
            {
                Session["PatientInformation"] = null;
            }

            if (Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("frmLogOff.aspx");
            }
            if (ModuleId != 203)
            {
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientBlueCard");
            }

            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (dtPatientInfo != null && dtPatientInfo.Rows.Count > 0)
            {
                if (Session["SystemId"].ToString() == "1")
                    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                else
                    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();

                lblDOB.Text = Convert.ToDateTime(dtPatientInfo.Rows[0]["DOB"]).ToString("dd-MMM-yyyy");

                lblAge.Text = string.Format("{0} years {1} months", dtPatientInfo.Rows[0]["Age"], dtPatientInfo.Rows[0]["Age1"]);

                lblSex.Text = dtPatientInfo.Rows[0]["SexNM"].ToString();

                PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
                ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
            }
            else
            {
                PanelPatiInfo.Visible = false;
            }

            DataTable dtLabels = (DataTable)Session["DynamicLabels"];
            if (dtLabels != null)
            {
                //lblenroll.Text = dtLabels.Rows[4]["Label"].ToString();
                //lblClinicNo.Text = dtLabels.Rows[3]["Label"].ToString();
                if (GblIQCare.Scheduler == 0)
                {
                    //trARTNo.Visible = true;
                    thePnlIdent.Visible = true;
                    TechnicalAreaIdentifier();
                }
                else
                {
                    thePnlIdent.Visible = false;
                    //trARTNo.Visible = false;

                    GblIQCare.Scheduler = 0;
                }
            }
            setBillStaus();

            //################  Master Settings ###################
            string UserID = "";
            if (Session["AppUserID"].ToString() != null)
                UserID = Session["AppUserId"].ToString();



            string theUrl;
            //////if (lblpntStatus.Text == "0")
            //////{
            if (Session["PtnPrgStatus"] != null)
            {
                DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
                DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
                string PatientExitReason = string.Empty;
                string PMTCTCareEnded = string.Empty;
                string CareEnded = string.Empty;
                if (theCEntedStatusDT.Rows.Count > 0)
                {
                    PatientExitReason = Convert.ToString(theCEntedStatusDT.Rows[0]["PatientExitReason"]);
                    PMTCTCareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["PMTCTCareEnded"]);
                    CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                    if (CareEnded == "1")
                    {
                        disableMenuItem();
                    }
                }

                //if ((theStatusDT.Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") || (Session["PMTCTPatientStatus"]!= null && Session["PMTCTPatientStatus"].ToString() == "1"))
                if ((Convert.ToString(theStatusDT.Rows[0]["PMTCTStatus"]) == "PMTCT Care Ended") || (PatientExitReason == "93" && PMTCTCareEnded == "1"))
                {
                    PtnPMTCTStatus = 1;
                    Session["PMTCTPatientStatus"] = 1;
                }
                else
                {
                    PtnPMTCTStatus = 0;
                    Session["PMTCTPatientStatus"] = 0;
                    //LoggedInUser.PatientStatus = 0;
                }
                //if ((theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended") || (Session["HIVPatientStatus"]!= null && Session["HIVPatientStatus"].ToString() == "1"))
                if ((Convert.ToString(theStatusDT.Rows[0]["ART/PalliativeCare"]) == "Care Ended") || (PatientExitReason == "93" && CareEnded == "1"))
                {
                    PtnARTStatus = 1;
                    Session["HIVPatientStatus"] = 1;
                }
                else
                {
                    PtnARTStatus = 0;
                    Session["HIVPatientStatus"] = 0;
                }
            }

            if (lblpntStatus.Text == "0" && (PtnARTStatus == 0 || PtnPMTCTStatus == 0))
            //if (PtnARTStatus == 0 || PtnPMTCTStatus == 0)
            {
                if (PtnARTStatus == 0)
                {
                    //########### Initial Evaluation ############
                    //theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx?name=Add", PtnARTStatus);
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx");
                    // AssignUrl(patientLevelMenu.Items, "mnuInitEval", theUrl);

                    string theUrl18 = string.Format("{0}", "../ClinicalForms/frmClinical_ARTFollowup.aspx");
                    // AssignUrl(patientLevelMenu.Items, "mnuFollowupART", theUrl18);

                    string theUrl1 = string.Format("{0}", "../ClinicalForms/frmClinical_NonARTFollowUp.aspx");
                    Session.Remove("ExixstDS1");
                    //AssignUrl(patientLevelMenu.Items, "mnuNonARTFollowUp", theUrl1);


                }

                if (PtnPMTCTStatus == 0)
                {


                }
            }




        }

        private void Load_MenuCreateNewForm()
        {
            /*
             * int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
             DataSet theDS = (DataSet)ViewState["AddForms"];
             DataTable dataTable = new DataTable();
             if (theDS.Tables[1].Rows.Count > 0)
                 dataTable = this.BindFormBusinessRules(theDS.Tables[1], theDS.Tables[3]);
             int rowNo = 0;
             foreach (DataRow theDR in theDS.Tables[1].Rows)
             {
                 if (Convert.ToInt32(theDR["Featureid"]) != 71)
                 {
                     string theURL = "", theLabTest = "";
                     if (Convert.ToInt32(theDR["FeatureId"]) == 3)
                         //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Adult.aspx?Prog=''");
                         theURL = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=''");
                     else if (Convert.ToInt32(theDR["FeatureId"]) == 4)
                         //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=''");
                         theURL = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=''");
                     else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "0")
                         theURL = string.Format("{0}sts={1}", "../Laboratory/LabRecordEntry.aspx?", lblpntStatus.Text);
                     else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                     {
                         Guid g = Guid.NewGuid();
                         theURL = string.Format("{0}?key={1}&sts={2}&name=add", "../Laboratory/LabRequestForm.aspx", g.ToString(), lblpntStatus.Text);
                         theLabTest = theURL;
                     }
                     else if (theDR["ReferenceId"].ToString() == "SERVICE_REQUEST")
                     {
                         Guid g = Guid.NewGuid();
                         theURL = string.Format("{0}?key={1}&sts={2}&name=add", "ClinicalService/ServiceRecordEntry.aspx", g.ToString(), lblpntStatus.Text);
                     }
                     else if (theDR["FeatureName"].ToString() == "Care Termination")
                         theURL = string.Format("{0}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?");
                     else
                         theURL = string.Format("{0}|{1}", "../ClinicalForms/CustomForm.aspx?", theDR["FeatureId"].ToString());
                     //theURL = string.Format("{0}&Id={1}", "../ClinicalForms/frmClinical_CustomForm.aspx?", theDR["FeatureId"].ToString());

                     if (ModuleId.ToString() == "1")
                     {
                         DivDynModule(); divKenyaBlueCard(); divUgandaBlueCard(); ClinicID();

                         LoadCreateNewMenu(theURL, rowNo);
                         if (lblpntStatus.Text == "1")
                             disableMenuItem();
                     }
                     else if (ModuleId.ToString() == "2")
                     {
                         DivDynModule(); divKenyaBlueCard(); divUgandaBlueCard(); divPMTCT();

                         LoadCreateNewMenu(theURL, rowNo);
                         if (lblpntStatus.Text != "1")
                         {
                         }
                     }
                     else if (ModuleId.ToString() == "202")
                     {
                         DivDynModule(); divKenyaBlueCard(); ClinicID(); divPMTCT();
                         if (lblpntStatus.Text != "1")
                         {
                             if (theLabTest != "")
                             {
                                 LoadCreateNewMenu(theLabTest, rowNo);
                             }
                             else
                             {
                                 LoadCreateNewMenu(theURL, rowNo);
                             }
                         }
                     }
                     else if (ModuleId.ToString() == "203")
                     {
                         DivDynModule(); divUgandaBlueCard(); ClinicID(); divPMTCT();
                         if (lblpntStatus.Text != "1")
                         {
                             if (theLabTest != "")
                             {
                                 LoadCreateNewMenu(theLabTest, rowNo);
                             }
                             else
                             {
                                 LoadCreateNewMenu(theURL, rowNo);
                             }
                         }
                     }
                     else
                     {
                         if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                         {
                             divKenyaBlueCard(); divUgandaBlueCard(); ClinicID(); divPMTCT();
                             LoadCreateNewMenu(theURL, rowNo);
                         }
                         else
                         {
                             LoadCreateNewMenu(theURL, rowNo);
                             if (lblpntStatus.Text == "1")
                                 disableMenuItem();
                         }
                     }
                 }
                 rowNo++;
             }

             if (ModuleId.ToString() == "1")
             {
                 //divPMTCT.Visible = true;
                 DivDynModule();
                 //DivDynModule.Visible = false;
                 ClinicID();
                 //ClinicID.Visible = false;
                 divUgandaBlueCard();
                 //divUgandaBlueCard.Visible = false;

                 //todo
                 divKenyaBlueCard();
             }
             else if (ModuleId.ToString() == "2")
             {
                 divPMTCT();
                 //divPMTCT.Visible = false;
                 DivDynModule();
                 //DivDynModule.Visible = false;
                 //ClinicID.Visible = true;
                 divUgandaBlueCard();
                 //divUgandaBlueCard.Visible = false;

                 //todo
                 divKenyaBlueCard();
             }
             else if (ModuleId.ToString() == "202")
             {
                 divPMTCT();
                 //divPMTCT.Visible = false;
                 DivDynModule();
                 //DivDynModule.Visible = false;
                 ClinicID();
                 //ClinicID.Visible = false;
                 //divUgandaBlueCard.Visible = true;

                 //todo
                 divKenyaBlueCard();
             }
             else if (ModuleId.ToString() == "203")
             {
                 divPMTCT();
                 //divPMTCT.Visible = false;
                 DivDynModule();
                 //DivDynModule.Visible = false;
                 ClinicID();
                 //ClinicID.Visible = false;
                 divUgandaBlueCard();
                 //divUgandaBlueCard.Visible = false;
                 //divKenyaBlueCard.Visible = true;
             }
             else if (ModuleId.ToString() == "204")
             {
                 //todo
                 DivDynModule();

                 divPMTCT();
                 //divPMTCT.Visible = false;
                 //DivDynModule.Visible = true;
                 ClinicID();
                 //ClinicID.Visible = false;
                 divUgandaBlueCard();
                 //divUgandaBlueCard.Visible = false;
                 divKenyaBlueCard(true);
             }
             else
             {
                 //todo
                 DivDynModule();

                 divPMTCT();
                 //divPMTCT.Visible = false;
                 //DivDynModule.Visible = true;
                 ClinicID();
                 //ClinicID.Visible = false;
                 divUgandaBlueCard();
                 //divUgandaBlueCard.Visible = false;
                 divKenyaBlueCard();
             }

             */
        }
        private void Load_MenuPartial(int PatientId, string Status, int CountryId)
        {
            ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
            foreach (DataRow dr in theDS.Tables[0].Rows)
            {
                //string theURL = string.Format("{0}&PatientId={1}&FormID={2}&sts={3}", "../ClinicalForms/frmClinical_CustomForm.aspx?name=Add", PatientId.ToString(), dr["FeatureID"].ToString(), Status);
                string theURL = string.Format("{0}", "../ClinicalForms/frmClinical_CustomForm.aspx?");

                if (Status == "0")
                {
                    //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FormID"] + "' onClick=fnSetformID('"+dr["FeatureID"].ToString()+"'); HRef=" + theURL + " runat='server'>" + dr["FeatureName"] + "</a>"));
                    LoadCreatePartialMenu(theURL, CountryId);
                    //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FeatureID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server' "));
                    if (PtnARTStatus == 1)
                    {
                        disableMenuItem();
                        //divPMTCT.Controls.Add(new LiteralControl("Disabled='true'"));
                    }
                    //divPMTCT.Controls.Add(new LiteralControl(" >" + dr["FeatureName"] + "</a>"));
                }
                else
                {
                    //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FormID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); runat='server'>" + dr["FeatureName"] + "</a>"));
                    LoadCreatePartialMenu(theURL, CountryId);
                    //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FeatureID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server' "));
                    if (PtnARTStatus == 1)
                    {
                        disableMenuItem();
                        //divPMTCT.Controls.Add(new LiteralControl("Disabled='true'"));
                    }
                    //divPMTCT.Controls.Add(new LiteralControl(" >" + dr["FeatureName"] + "</a>"));
                }
            }
        }

        //private void Load_MenuRegistration()
        //{
        //    int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);

        //    IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

        //    if (ModuleId == 2)
        //    {
        //    }
        //    else
        //    {
        //        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuEnrolment");
        //    }
        //}
        public DataTable BindFormBusinessRules(DataTable dtform, DataTable dtbusinessrules)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FeatureID", typeof(string));
            dataTable.Columns.Add("FeatureName", typeof(string));
            foreach (DataRow row1 in (InternalDataCollectionBase)dtform.Rows)
            {
                DataTable table = new DataView(dtbusinessrules)
                {
                    RowFilter = ("FeatureID=" + row1["FeatureID"].ToString())
                }.ToTable();
                Hashtable hashtable = new Hashtable();
                if (table.Rows.Count > 0)
                {
                    DataRow[] dataRowArray1 = table.Select("SetType=1");
                    DataRow[] dataRowArray2 = table.Select("SetType=2");
                    int length1 = dataRowArray1.Length;
                    DataRow[] dataRowArray3 = table.Select("SetType=1 and BusRuleId=16");
                    DataRow[] dataRowArray4 = table.Select("SetType=1 and BusRuleId=14");
                    DataRow[] dataRowArray5 = table.Select("SetType=1 and BusRuleId=15");
                    int length2 = dataRowArray2.Length;
                    DataRow[] dataRowArray6 = table.Select("SetType=2 and BusRuleId=16");
                    DataRow[] dataRowArray7 = table.Select("SetType=2 and BusRuleId=14");
                    DataRow[] dataRowArray8 = table.Select("SetType=2 and BusRuleId=15");
                    if (length1 > 0)
                    {
                        if (length1 == 3)
                        {
                            foreach (DataRow dataRow in dataRowArray3)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "16" && dataRow["Value"] != DBNull.Value && (dataRow["Value1"] != DBNull.Value && Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow["Value"])) && (Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow["Value1"]) && (this.Session["PatientSex"].ToString() == "Male" || this.Session["PatientSex"].ToString() == "Female")) && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                        if (length1 == 2)
                        {
                            foreach (DataRow dataRow1 in dataRowArray3)
                            {
                                if (Convert.ToString(dataRow1["BusRuleId"]) == "16" && dataRow1["Value"] != DBNull.Value && dataRow1["Value1"] != DBNull.Value)
                                {
                                    if (Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow1["Value"]) && Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow1["Value1"]) && this.Session["PatientSex"].ToString() == "Male")
                                    {
                                        foreach (DataRow dataRow2 in dataRowArray4)
                                        {
                                            if (Convert.ToString(dataRow2["BusRuleId"]) == "14" && this.Session["PatientSex"].ToString() == "Male" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                            {
                                                DataRow row2 = dataTable.NewRow();
                                                row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                                row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                                dataTable.Rows.Add(row2);
                                                hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                            }
                                        }
                                    }
                                    else if (Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow1["Value"]) && Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow1["Value1"]) && this.Session["PatientSex"].ToString() == "Female")
                                    {
                                        foreach (DataRow dataRow2 in dataRowArray5)
                                        {
                                            if (Convert.ToString(dataRow2["BusRuleId"]) == "15" && this.Session["PatientSex"].ToString() == "Female" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                            {
                                                DataRow row2 = dataTable.NewRow();
                                                row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                                row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                                dataTable.Rows.Add(row2);
                                                hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            if (dataRowArray3.Length == 0)
                            {
                                foreach (DataRow dataRow in dataRowArray4)
                                {
                                    if (Convert.ToString(dataRow["BusRuleId"]) == "14" && this.Session["PatientSex"].ToString() == "Male" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                    {
                                        DataRow row2 = dataTable.NewRow();
                                        row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                        row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                        dataTable.Rows.Add(row2);
                                        hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                    }
                                }
                                foreach (DataRow dataRow in dataRowArray5)
                                {
                                    if (Convert.ToString(dataRow["BusRuleId"]) == "15" && this.Session["PatientSex"].ToString() == "Female" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                    {
                                        DataRow row2 = dataTable.NewRow();
                                        row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                        row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                        dataTable.Rows.Add(row2);
                                        hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                    }
                                }
                            }
                        }
                        if (length1 == 1)
                        {
                            foreach (DataRow dataRow in dataRowArray3)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "16" && dataRow["Value"] != DBNull.Value && (dataRow["Value1"] != DBNull.Value && Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow["Value"])) && (Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow["Value1"]) && !hashtable.Contains((object)row1["FeatureName"].ToString())))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                        if (length1 == 1)
                        {
                            foreach (DataRow dataRow in dataRowArray4)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "14" && this.Session["PatientSex"].ToString() == "Male" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                        if (length1 == 1)
                        {
                            foreach (DataRow dataRow in dataRowArray5)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "15" && this.Session["PatientSex"].ToString() == "Female" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                    }
                    if (length2 > 0)
                    {
                        if (length2 == 3)
                        {
                            foreach (DataRow dataRow in dataRowArray6)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "16" && dataRow["Value"] != DBNull.Value && (dataRow["Value1"] != DBNull.Value && Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow["Value"])) && (Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow["Value1"]) && (this.Session["PatientSex"].ToString() == "Male" || this.Session["PatientSex"].ToString() == "Female")) && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                        if (length2 == 2)
                        {
                            foreach (DataRow dataRow1 in dataRowArray6)
                            {
                                if (Convert.ToString(dataRow1["BusRuleId"]) == "16" && dataRow1["Value"] != DBNull.Value && dataRow1["Value1"] != DBNull.Value)
                                {
                                    if (Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow1["Value"]) && Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow1["Value1"]) && this.Session["PatientSex"].ToString() == "Male")
                                    {
                                        foreach (DataRow dataRow2 in dataRowArray7)
                                        {
                                            if (Convert.ToString(dataRow2["BusRuleId"]) == "14" && this.Session["PatientSex"].ToString() == "Male" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                            {
                                                DataRow row2 = dataTable.NewRow();
                                                row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                                row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                                dataTable.Rows.Add(row2);
                                                hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                            }
                                        }
                                    }
                                    else if (Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow1["Value"]) && Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow1["Value1"]) && this.Session["PatientSex"].ToString() == "Female")
                                    {
                                        foreach (DataRow dataRow2 in dataRowArray8)
                                        {
                                            if (Convert.ToString(dataRow2["BusRuleId"]) == "15" && this.Session["PatientSex"].ToString() == "Female" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                            {
                                                DataRow row2 = dataTable.NewRow();
                                                row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                                row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                                dataTable.Rows.Add(row2);
                                                hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            if (dataRowArray6.Length == 0)
                            {
                                foreach (DataRow dataRow in dataRowArray7)
                                {
                                    if (Convert.ToString(dataRow["BusRuleId"]) == "14" && this.Session["PatientSex"].ToString() == "Male" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                    {
                                        DataRow row2 = dataTable.NewRow();
                                        row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                        row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                        dataTable.Rows.Add(row2);
                                        hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                    }
                                }
                                foreach (DataRow dataRow in dataRowArray8)
                                {
                                    if (Convert.ToString(dataRow["BusRuleId"]) == "15" && this.Session["PatientSex"].ToString() == "Female" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                    {
                                        DataRow row2 = dataTable.NewRow();
                                        row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                        row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                        dataTable.Rows.Add(row2);
                                        hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                    }
                                }
                            }
                        }
                        if (length2 == 1)
                        {
                            foreach (DataRow dataRow in dataRowArray6)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "16" && dataRow["Value"] != DBNull.Value && (dataRow["Value1"] != DBNull.Value && Convert.ToDecimal(this.Session["PatientAge"]) >= Convert.ToDecimal(dataRow["Value"])) && (Convert.ToDecimal(this.Session["PatientAge"]) <= Convert.ToDecimal(dataRow["Value1"]) && !hashtable.Contains((object)row1["FeatureName"].ToString())))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                        if (length2 == 1)
                        {
                            foreach (DataRow dataRow in dataRowArray7)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "14" && this.Session["PatientSex"].ToString() == "Male" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                        if (length2 == 1)
                        {
                            foreach (DataRow dataRow in dataRowArray8)
                            {
                                if (Convert.ToString(dataRow["BusRuleId"]) == "15" && this.Session["PatientSex"].ToString() == "Female" && !hashtable.Contains((object)row1["FeatureName"].ToString()))
                                {
                                    DataRow row2 = dataTable.NewRow();
                                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                                    dataTable.Rows.Add(row2);
                                    hashtable.Add((object)row1["FeatureName"].ToString(), (object)row1["FeatureName"].ToString());
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["FeatureName"] = (object)row1["FeatureName"].ToString();
                    row2["FeatureID"] = (object)row1["FeatureID"].ToString();
                    dataTable.Rows.Add(row2);
                }
            }
            return dataTable;
        }

        //TODO Done code for Bill
        private void setBillStaus()
        {
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string pageName = fileinfo.Name;

            try
            {
                /*
                if (pageName.Equals("frmBilling_ClientBill.aspx") || pageName.Equals("frmBilling_PayBill.aspx"))
                {
                    Tr1.Visible = false;
                    Tr2.Visible = false;
                    patientLevelMenu.Visible = false;
                    return;
                }

                int visitTypeID = getVisitTypeID(pageName);
                if (visitTypeID == 99)
                {
                    Tr2.Visible = false;
                }
                else if (visitTypeID == 6)//Lab order form show labs paid for
                {
                    Tr2.Visible = true;
                    Label theLabelBill;
                    IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                    DataTable theLabsDt = BillingManager.GetPaidLabs(int.Parse(Session["PatientId"].ToString()));
                    String labs = "", labelText = "Attention:This service has not been paid for!!";
                    foreach (DataRow row in theLabsDt.Rows)
                    {
                        labs = String.Format("{0}{1}, ", labs, row[0]);
                    }
                    if (theLabsDt.Rows.Count == 0)
                    {
                        labelText = "Attention:This service has not been paid for!!";
                        theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Red, Text = labelText };
                    }
                    else
                    {
                        labelText = "Patient is cleared to recieve the following lab tests: " + labs.ToUpper();
                        theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Green, Text = labelText };
                    }

                    thePnlBill.Controls.Add(theLabelBill);
                }
                else if (visitTypeID == 4)//Pharmacy form
                {
                    Tr2.Visible = true;
                    Label theLabelBill;
                    IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                    DataTable theDrugsDt = BillingManager.GetPaidDrugs(int.Parse(Session["PatientId"].ToString()));
                    String drugs = "", labelText = "Attention:This service has not been paid for!!";
                    foreach (DataRow row in theDrugsDt.Rows)
                    {
                        drugs = String.Format("{0}{1} {2},{3} ", drugs, row[1], row[0], Environment.NewLine);
                    }
                    if (theDrugsDt.Rows.Count == 0)
                    {
                        labelText = "Attention:This service has not been paid for!!";
                        theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Red, Text = labelText };
                    }
                    else
                    {
                        labelText = "Patient is cleared to recieve the following Drugs: " + Environment.NewLine + drugs;
                        theLabelBill = new Label { ID = "Lbl_bill", ForeColor = System.Drawing.Color.Green, Text = labelText };
                    }

                    thePnlBill.Controls.Add(theLabelBill);
                }
                else
                {
                    //check whether this is a form that has previously been filled if so it dosnt have to be paid for again
                    if (Session["PatientVisitId"] != null && int.Parse(Session["PatientVisitId"].ToString()) != 0) return;
                    IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                    bool itemPaid;
                    DataTable drItemStatus = BillingManager.isVisitTypePaid(visitTypeID, int.Parse(Session["PatientId"].ToString()));
                    int returnValue = Int32.Parse(drItemStatus.Rows[0][0].ToString());

                    if (returnValue == -1) // item has no price
                    {
                        Tr2.Visible = false;
                    }
                    else
                    {
                        itemPaid = returnValue > 0 ? true : false;

                        Tr2.Visible = true;
                        Label theLabelBill = new Label
                        {
                            ID = "Lbl_bill",
                            ForeColor = System.Drawing.Color.Green,
                            Text = itemPaid ? "Patient is cleared to recieve this service" : "Attention:This service has not been paid for!!"
                        };
                        thePnlBill.Controls.Add(theLabelBill);
                    }

                }*/
            }
            catch (Exception ex)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = ex.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        private void TechnicalAreaIdentifier()
        {
            int intmoduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
            if (intmoduleID > 0)
            {

                IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                System.Data.DataSet dsTab = PatientHome.GetTechnicalAreaIdentifierFuture(intmoduleID, Convert.ToInt32(Session["PatientId"]));

                if (dsTab.Tables[0].Rows.Count > 0)
                {
                    if (dsTab.Tables[0].Rows.Count > 0)
                    {
                        //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                        Label theLabelIdentifier1 = new Label();
                        theLabelIdentifier1.ID = "Lbl_" + dsTab.Tables[0].Rows[0][0].ToString();
                        int i = 0;
                        foreach (DataRow DRLabel in dsTab.Tables[0].Rows)
                        {
                            foreach (DataRow DRLabel1 in dsTab.Tables[1].Rows)
                            {
                                theLabelIdentifier1.Text = theLabelIdentifier1.Text + "    " + DRLabel[0].ToString() + " : " + DRLabel1[i].ToString();
                            }
                            i++;
                        }

                        thePnlIdent.Controls.Add(theLabelIdentifier1);
                    }

                }
            }
        }
    }
}