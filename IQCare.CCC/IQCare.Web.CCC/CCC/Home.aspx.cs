using System;
using System.Web;
using Application.Presentation;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Interoperability;

namespace IQCare.Web.CCC
{
    public partial class Home : System.Web.UI.Page
    {
        public int AppLocationId;
        ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");


        protected void Page_Load(object sender, EventArgs e)
        {
            AppLocationId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
            Session["PatientPK"] = 0;
            
            if (!IsPostBack)
            {
                try
                {
                    TestingSummaryStatisticsManager statistics = new TestingSummaryStatisticsManager();
                    PatientStabilitySummaryManager summaryManager = new PatientStabilitySummaryManager();
                    IlStatisticsManager ilStatisticsManager = new IlStatisticsManager();

                    var stats = ilStatisticsManager.GetILStatistics();
                    lblOutgoing.Text = stats.Outbox.ToString();
                    lblIncoming.Text = stats.Inbox.ToString();
                  
                    var statList = statistics.GetAllStatistics();
                    var summaryList = summaryManager.GetAllStabilitySummaries();

                    if (statList.Count > 0)
                    {
                        var html = "";
                        var Label = "";

                        for (int i = 0; i < statList.Count; i++)
                        {
                            Label = "label" + i;
                            html += "<div class='col-md-10'>";
                            html += "<label class='control-label pull-left'>" + statList[i].Name + ":</label>";
                            html += "</div>";
                            html += "<div class='col-md-2 pull-right'>";
                            html += "<label for='value' id='" + Label + "' class='control-label text-success pull-right'>";
                            html += "<span class='badge pull-right'>" + statList[i].Value + "</span>";
                            html += "</div>";
                            html += "<div class='col-md-12'><hr></div>";
                        }
                        testingSummaryStatistics.InnerHtml = html;
                    }

                    if (summaryList.Count > 0)
                    {
                        var html = "";
                        var Label = "";

                        for (int i = 0; i < summaryList.Count; i++)
                        {
                            Label = "label" + i;
                            html += "<div class='col-md-9'>";
                            html += "<label class='control-label pull-left'>" + summaryList[i].Category + ":</label>";
                            html += "</div>";
                            html += "<div class='col-md-2 pull-right'>";
                            html += "<label for='value' id='" + Label + "' class='control-label text-success pull-right'>";
                            //summaryList[i].Category = '"' + summaryList[i].Category + '"';
                            html += "<button id='btn" + summaryList[i].Category + "' class='badge pull-right' onClick='GenExcel(\"" + summaryList[i].Category + "\");'>" + summaryList[i].Value + "</button>";
                            html += "</div>";
                            html += "<div class='col-md-12'><hr></div>";
                        }
                        stabilitySummaryStatictics.InnerHtml = html;
                    }

                }
                catch 
                {

                }
                try
                {
                    var facilityStatistics = _lookupManager.GetLookupFacilityStatistics();
                    foreach (var item in facilityStatistics)
                    {
                        //lblTotalPatients.Text = "<span class='badge pull-right'>" + item.TotalCumulativePatients.ToString() + "</span>";
                        //lblOnART.Text = "<span class='badge pull-right'>" + item.TotalActiveOnArt.ToString() + "</span>";
                        //lblctx.Text = "<span class='badge pull-right'>" + item.TotalOnCtxDapson.ToString() + "</span>";
                        //lbltransit.Text = "<span class='badge pull-right'>" + item.TotalTransit.ToString() + "</span>";
                        //lbltransferin.Text = "<span class='badge pull-right'>" + item.TotalTransferIn.ToString() + "</span>";
                        lbldead.Text = "<span class='badge pull-right'>" + item.TotalPatientsDead.ToString() + "</span>";
                        lbltransferout.Text = "<span class='badge pull-right'>" + item.TotalPatientsTransferedOut.ToString() + "</span>";
                        //lblundocumentedltf.Text = "<span class='badge pull-right'>" + item.TotalUndocumentedLTFU.ToString() + "</span>";
                        totalDocumetedLTFU.Text = "<span class='badge pull-right'>" + item.LostToFollowUp.ToString() + "</span>";

                        //lblctx.Text = "<span class='badge pull-right'>" + +"</span>";
                    }
                }
                catch{ }
            }
            
        }

        
    }
}