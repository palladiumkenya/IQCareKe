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
            PatientCareEndingManager careEndingManager = new PatientCareEndingManager();

            if (!IsPostBack)
            {
                ILookupManager mgr =
                (ILookupManager)
                ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                List<LookupItemView> vw = mgr.GetLookItemByGroup("CareEnded");

                if (vw != null && vw.Count > 0)
                {
                    Reason.Items.Add(new ListItem("select", "0"));

                    foreach (var item in vw)
                    {
                        Reason.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }
                int patientId = int.Parse(Session["PatientPK"].ToString());
                //if (PmVisitId > 0)
                //{
                //    var careEnded = careEndingManager.GetPatientCareEndingByVisitId(patientId, PmVisitId);
                //    if(careEnded.Count > 0)
                //    {
                //        careEnded[0].
                //    }
                //}
            }

        }
    }
}