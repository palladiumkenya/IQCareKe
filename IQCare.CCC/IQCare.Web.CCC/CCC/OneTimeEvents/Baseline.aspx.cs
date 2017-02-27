using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

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
            get { return Convert.ToInt32(Session["patientId"]); }
        }

        protected int PatientMasterVisitId
        {
            get { return Convert.ToInt32(Session["patientMasterVisitId"]); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            List<LookupCounty> ct = mgr.GetLookupCounties();

            if (ct != null && ct.Count > 0)
            {
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
            List<LookupItemView> lookupItem = mgr.GetLookItemByGroup("RegimenClassification");
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

        }
    }
}