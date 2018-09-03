using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.SCM;
using Application.Presentation;
using Application.Common;
using Telerik.Web.UI;

namespace IQCare.Web.PMSCM
{
    public partial class frmPharmacy_Dashboard : LogPage
    {
        BindFunctions theBindManager = new BindFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                Session["PatientId"] = 0;
                Session["TechnicalAreaId"] = 201; // 206;
                ViewState["Facility"] = null;
                if (!IsPostBack)
                {
                    BindCombo();
                }
                ISCMReport objPODetails = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport, BusinessProcess.SCM");
                DataSet theDTPODetails = objPODetails.PharmacyDashBoard(Convert.ToInt32(ddlStore.SelectedValue));

                BarSeries barSeries = new BarSeries();
                barSeries.DataFieldY= "Quantity";
                barSeries.Name = "Quantity";
                barSeries.TooltipsAppearance.Visible = false;

                RadHtmlChart RadHtmlChart1 = new RadHtmlChart();
                RadHtmlChart1.ChartTitle.Text = "Drugs expiring in a months time";
                RadHtmlChart1.Legend.Appearance.Visible = false;
                RadHtmlChart1.PlotArea.Series.Add(barSeries);
                RadHtmlChart1.PlotArea.XAxis.DataLabelsField = "DrugName";
                RadHtmlChart1.PlotArea.XAxis.MajorGridLines.Visible = false;
                RadHtmlChart1.PlotArea.XAxis.MinorGridLines.Visible = false;
                RadHtmlChart1.PlotArea.YAxis.TitleAppearance.Text = "Quantity";
                RadHtmlChart1.PlotArea.YAxis.MinorGridLines.Visible = false;
                RadHtmlChart1.Skin = "Silk";
                RadHtmlChart1.Width = 1150;
                RadHtmlChart1.Height = 350;

                RadHtmlChart1.DataSource = theDTPODetails.Tables[0];
                RadHtmlChart1.DataBind();
                chart1.Controls.Add(RadHtmlChart1);

                ColumnSeries colseries1 = new ColumnSeries();
                colseries1.Name = "Appointments";
                colseries1.DataFieldY = "NoOfAppointments";
                colseries1.Appearance.FillStyle.BackgroundColor = System.Drawing.Color.Orange;
                colseries1.TooltipsAppearance.Color = System.Drawing.Color.White;

                ColumnSeries colseries2 = new ColumnSeries();
                colseries2.Name = "Visits";
                colseries2.DataFieldY = "NoOfVisits";
                colseries2.Appearance.FillStyle.BackgroundColor = System.Drawing.Color.Blue;
                colseries2.TooltipsAppearance.Color = System.Drawing.Color.White;

                RadHtmlChart RadHtmlChart2 = new RadHtmlChart();
                //RadHtmlChart2.Width = 500;
                RadHtmlChart2.Height = 321;
                RadHtmlChart2.Skin = "Silk";
                RadHtmlChart2.PlotArea.Series.Add(colseries1);
                RadHtmlChart2.PlotArea.Series.Add(colseries2);
                RadHtmlChart2.PlotArea.XAxis.DataLabelsField = "Day";
                RadHtmlChart2.PlotArea.XAxis.Color = System.Drawing.Color.SlateGray;
                RadHtmlChart2.PlotArea.XAxis.MinorGridLines.Visible = false;
                RadHtmlChart2.PlotArea.XAxis.MajorGridLines.Visible = false;
                RadHtmlChart2.PlotArea.XAxis.LabelsAppearance.TextStyle.Color= System.Drawing.Color.DarkGray;

                RadHtmlChart2.PlotArea.YAxis.Color = System.Drawing.Color.SlateGray;
                RadHtmlChart2.PlotArea.YAxis.MinorGridLines.Visible = false;
                RadHtmlChart2.PlotArea.YAxis.LabelsAppearance.TextStyle.Color = System.Drawing.Color.DarkGray;
                RadHtmlChart2.PlotArea.YAxis.TitleAppearance.Text = "No. of appointments/visits";
                RadHtmlChart2.PlotArea.YAxis.TitleAppearance.TextStyle.Color= System.Drawing.Color.DarkGray;
                RadHtmlChart2.ChartTitle.Text = "Patient Appointments vs Visits";

                RadHtmlChart2.DataSource = theDTPODetails.Tables[1];
                RadHtmlChart2.DataBind();
                chart2.Controls.Add(RadHtmlChart2);

                grdDrugsRunningOut.DataSource = theDTPODetails.Tables[2];
                grdDrugsRunningOut.DataBind();
                

                resizeScreen();
                //(Master.FindControl("pnlExtruder") as Panel).Visible = false;
                //(Master.FindControl("level2Navigation") as Control).Visible = true;
                ////(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Dashboard";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = true;
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("thePnlIdent") as Panel).Visible = false;
                
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("UserControl_Alerts1") as UserControl).Visible = false;
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;

                //(Master.FindControl("facilityBanner") as Control).Visible = false;
                //(Master.FindControl("patientBanner") as Control).Visible = false;
                //(Master.FindControl("username1") as Control).Visible = false;
                //(Master.FindControl("currentdate1") as Control).Visible = false;
                //(Master.FindControl("facilityName") as Control).Visible = false;
                //(Master.FindControl("imageFlipLevel2") as Control).Visible = false;

            }
            catch (Exception ex)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = ex.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            
        }

        private void BindCombo()
        {
            try
            {
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
                theDV.Sort = "Name ASC";
                DataTable theStoreDT = theDV.ToTable();
                theBindManager.BindCombo(ddlStore, theStoreDT, "Name", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

        }

        private void resizeScreen()
        {
            if (!object.Equals(Session["browserWidth"], null))
            {
                if (Session["browserWidth"].ToString() != null && Session["browserWidth"].ToString() != "")
                {
                    if (Convert.ToInt32(Session["browserWidth"].ToString()) > 1200)
                    {
                        LiteralControl ltr = new LiteralControl();
                        ltr.Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                                    @"#mainMaster
                            {
                                width: 100% !important;
                            }
                            #containerMaster
                            {
                                width: 1200px !important;
                            }
                            #ulAlerts
                            {
                                width: 1180px !important;
                            }
                            #divPatientInfo123
                            {
                                width: 1180px !important;
                            }
                            </style>
                            ";
                        this.Page.Header.Controls.Add(ltr);
                    }
                    else
                    {
                        LiteralControl ltr = new LiteralControl();
                        ltr.Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                                    @"#mainMaster
                            {
                                width: 100% !important;
                            }
                            #containerMaster
                            {
                                width: 90% !important;
                            }
                            #ulAlerts
                            {
                                width: 100% !important;
                            }
                            #divPatientInfo123
                            {
                                width: 99% !important;
                            }
                            </style>
                            ";
                        this.Page.Header.Controls.Add(ltr);
                    }
                }
            }
        }

    }
}