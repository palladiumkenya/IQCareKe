using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic.Triage;

namespace IQCare.Web.CCC.UC
{
    public partial class ucFemaleVitals : System.Web.UI.UserControl
    {
        protected int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected int PatientMasterVisitId
        {
            get { return Convert.ToInt32(Session["patientMasterVisitId"]); }
        }

        protected string PatientGender
        {
            get { return Convert.ToString( Session["Gender"]); }
        }

        protected int PatientAge
        {
            get { return Convert.ToInt32(Session["Age"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            //load dropdown values
            List<LookupItemView> lookupItem = lookupManager.GetLookItemByGroup("PregnancyStatus");
            if (lookupItem != null && lookupItem.Count > 0)
            {
                examinationPregnancyStatus.Items.Add(new ListItem("select", "0"));
                foreach (var k in lookupItem)
                {
                    examinationPregnancyStatus.Items.Add(new ListItem(k.ItemDisplayName + "(" + k.ItemName + ")", k.ItemId.ToString()));
                }
            }

            /* cacx screening */
            List<LookupItemView> cacxList = lookupManager.GetLookItemByGroup("CaCxScreening");
            if (cacxList != null && cacxList.Count > 0)
            {
                cacxscreening.Items.Add(new ListItem("select", "0"));
                foreach (var k in cacxList)
                {
                    cacxscreening.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> stiScreeList = lookupManager.GetLookItemByGroup("STIScreening");
            if (stiScreeList != null && stiScreeList.Count > 0)
            {
                stiScreening.Items.Add(new ListItem("select", "0"));
                foreach (var k in stiScreeList)
                {
                    stiScreening.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> stiPartnerList = lookupManager.GetLookItemByGroup("STIPartnerNotification");
            if (stiPartnerList != null && stiPartnerList.Count > 0)
            {
                stiPartnerNotification.Items.Add(new ListItem("select", "0"));
                foreach (var k in stiPartnerList)
                {
                    stiPartnerNotification.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                }
            }


            List<LookupItemView> fpStatusList = lookupManager.GetLookItemByGroup("FPStatus");
            if (fpStatusList != null && fpStatusList.Count > 0)
            {
                onFP.Items.Add(new ListItem("select", "0"));
                foreach (var k in fpStatusList)
                {
                    onFP.Items.Add(new ListItem(k.ItemDisplayName + "(" + k.ItemName + ")", k.ItemId.ToString()));
                }
            }

            List<LookupItemView> fpMethodList = lookupManager.GetLookItemByGroup("FPMethod");
            if (fpMethodList != null && fpMethodList.Count > 0)
            {
                //fpMethod.Items.Add(new ListItem("select", "0"));
                foreach (var k in fpMethodList)
                {
                    fpMethod.Items.Add(new ListItem(k.ItemDisplayName+"("+ k.ItemName+")", k.ItemId.ToString()));
                }
            }

            List<LookupItemView> NoFPLookup = lookupManager.GetLookItemByGroup("NoFamilyPlanning");
            if (NoFPLookup != null && NoFPLookup.Count > 0)
            {
                ddlNoFP.Items.Add(new ListItem("select", "0"));
                foreach (var k in NoFPLookup)
                {
                    ddlNoFP.Items.Add(new ListItem(k.ItemDisplayName + "(" + k.ItemName + ")", k.ItemId.ToString()));
                }
            }

            List<LookupItemView> pregnancyOutCome = lookupManager.GetLookItemByGroup("PregnancyOutcome");
            if (pregnancyOutCome != null && pregnancyOutCome.Count > 0)
            {
                PregnancyOutCome.Items.Add(new ListItem("select", "0"));
                foreach (var item in pregnancyOutCome)
                {
                    PregnancyOutCome.Items.Add(new ListItem(item.ItemDisplayName + "(" + item.ItemName + ")", item.ItemId.ToString()));
                }
            }

            string gender = Session["Gender"].ToString();

            if (gender == "Female")
            {
                var pregnancyStatus = new PatientPregnancyManager();
                int pgStatus = pregnancyStatus.CheckIfPatientPregnancyExisists(PatientId);
                var pregnancyList = pregnancyStatus.GetPatientPregnancy(PatientId);
                if (pgStatus > 0)
                {
                    lblPregnancyStatus.Text = "<span class='label label-info'> Pregnant </span> ";
                    if (pregnancyList != null)
                    {
                        foreach (var item in pregnancyList)
                        {
                            lblLMP.Text = "<span class='label label-info'>LMP : " + item.LMP.ToString("dd-MMM-yyyy") +
                                          "</span>";

                            lblEDD.Text = "<span class='label label-info'>EDD : " +
                                          Convert.ToDateTime(item.EDD).ToString("dd-MMM-yyyy") + "</span>";

                        }
                    }
                }
            }
        }
    }
}