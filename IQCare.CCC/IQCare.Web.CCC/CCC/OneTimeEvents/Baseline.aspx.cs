using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC;
using Interface.CCC.Enrollment;
using Interface.CCC.Lookup;
using Interface.CCC.Patient;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Triage;

namespace IQCare.Web.CCC.OneTimeEvents
{
    public partial class Baseline : System.Web.UI.Page
    {
        
        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
        }

        protected int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected int PatientMasterVisitId
        {
            get { return Convert.ToInt32(Session["patientMasterVisitId"]); }
        }

        protected string Gender
        {
            get { return Session["Gender"].ToString(); }
        }

        protected int Age
        {
            get { return Convert.ToInt32(Session["Age"]); }
        }

        protected DateTime DateOfBirth
        {
            get { return Convert.ToDateTime(Session["DateOfBirth"]); }
        }

        protected int PregnancyStatus
        {
            get
            {
                var pgstatus = new PatientPregnancyManager();
                return pgstatus.CheckIfPatientPregnancyExisists(PatientId);
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            IPatientVitals patientVitals  = (IPatientVitals)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientVitals, BusinessProcess.CCC");

            List<LookupCounty> ct = mgr.GetLookupCounties();

            if (ct != null && ct.Count > 0)
            {
                TransferFromCounty.Items.Add(new ListItem("select", "0"));
                foreach (var item in ct)
                {
                    TransferFromCounty.Items.Add(new ListItem(item.CountyName, item.CountyId.ToString()));
                }
            }

            //who stage WHOStage
            List<LookupItemView> ms = mgr.GetLookItemByGroup("WHOStage");
            if (ms != null && ms.Count > 0)
            {
                WHOStageAtEnrollment.Items.Add(new ListItem("select", "0"));
                bwhoStage.Items.Add(new ListItem("select", "0"));
                foreach (var k in ms)
                {
                    WHOStageAtEnrollment.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                    bwhoStage.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                }
            }

            /* Regimen classification*/
            List<LookupItemView> lookupItem;
            if (Age > 15)
            {
                lookupItem = mgr.GetLookItemByGroup("RegimenClassificationAdult");
            }
            else
            {
                lookupItem = mgr.GetLookItemByGroup("RegimenClassificationPaeds");
            }
            //List<LookupItemView> lookupItem = mgr.GetLookItemByGroup("RegimenClassification");
            if (lookupItem != null && lookupItem.Count > 0)
            {
                regimenCategory.Items.Add(new ListItem("select", "0"));
                InitiationRegimen.Items.Add(new ListItem("select", "0"));
                foreach (var k in lookupItem)
                {
                    regimenCategory.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    InitiationRegimen.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            /* Get patientBaseline Vitals */
            var ptnVitals = patientVitals.GetPatientVitalsBaseline(PatientId);
            if (ptnVitals != null)
            {
                BaselineWeight.Text = Convert.ToString(ptnVitals.Weight);
                BaselineMUAC.Text = Convert.ToString(ptnVitals.Muac);
                BaselineHeight.Text = Convert.ToString(ptnVitals.Height);
                BaselineBMI.Text = Convert.ToString(ptnVitals.BMI);

            }

        }
    }
}