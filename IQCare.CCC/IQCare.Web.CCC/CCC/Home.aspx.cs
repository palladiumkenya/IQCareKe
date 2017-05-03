using System;
using System.Web;
using Application.Presentation;
using Interface.CCC.Lookup;

namespace IQCare.Web.CCC
{
    public partial class Home : System.Web.UI.Page
    {
        public int AppLocationId;
        ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");


        protected void Page_Load(object sender, EventArgs e)
        {
            AppLocationId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
           var facilityStatistics= _lookupManager.GetLookupFacilityStatistics();
            foreach (var item in facilityStatistics)
            {
                lblTotalPatients.Text = "<span class='badge pull-right'>" + item.TotalCumulativePatients.ToString()+"</span>";
                lblOnART.Text = "<span class='badge pull-right'>" + item.TotalActiveOnArt.ToString()+"</span>";
                lblctx.Text = "<span class='badge pull-right'>"+ item.TotalOnCtxDapson.ToString() + "</span>";
                lbltransit.Text = "<span class='badge pull-right'>" + item.TotalTransit.ToString() +"</span>";
                lbltransferin.Text = "<span class='badge pull-right'>" + item.TotalTransferIn.ToString()+"</span>";
                lbldead.Text = "<span class='badge pull-right'>" + item.TotalPatientsDead.ToString() +"</span>";
                lbltransferout.Text = "<span class='badge pull-right'>" + item.TotalPatientsTransferedOut.ToString() +"</span>";
                //lblctx.Text = "<span class='badge pull-right'>" + +"</span>";
            }
        }
        
    }
}