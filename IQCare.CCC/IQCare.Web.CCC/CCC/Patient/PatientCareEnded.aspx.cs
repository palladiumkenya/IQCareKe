using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientCareEnded : System.Web.UI.Page
    {
        protected int PmVisitId
        {
            get { return Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : "0"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                List<LookupItemView> vw = mgr.GetLookItemByGroup("CareEnded");
                List<LookupItemView> tracingOutcomeOptions = mgr.GetLookItemByGroup("TracingOutcomeCareEnded");
                List<LookupItemView> reasonLostToFollowupOptions = mgr.GetLookItemByGroup("ReasonLostToFollowupCareEnded");
                List<LookupItemView> reasonForDeathOptions = mgr.GetLookItemByGroup("ReasonForDeathCareEnded");
                

                if (vw != null && vw.Count > 0)
                {
                    Reason.Items.Add(new ListItem("select", "0"));

                    foreach (var item in vw)
                    {
                        if(item.ItemName != "HIV Negative" && item.ItemName != "Confirmed HIV Negative")
                            Reason.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }

                if (tracingOutcomeOptions != null && tracingOutcomeOptions.Count > 0)
                {
                    TracingOutcome.Items.Add(new ListItem("select", ""));
                    foreach (var item in tracingOutcomeOptions)
                    {
                        TracingOutcome.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }


                if (reasonLostToFollowupOptions != null && reasonLostToFollowupOptions.Count > 0)
                {
                    ReasonLostToFollowup.Items.Add(new ListItem("select", ""));
                    foreach (var item in reasonLostToFollowupOptions)
                    {
                        ReasonLostToFollowup.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }


                if (reasonForDeathOptions != null && reasonForDeathOptions.Count > 0)
                {
                    reasonsForDeath.Items.Add(new ListItem("select", ""));
                    foreach (var item in reasonForDeathOptions)
                    {
                        reasonsForDeath.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }
            }

        }
    }
}