using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Application.Common;
using Application.Presentation;
using Interface.Pharmacy;
using Interface.Clinical;

namespace IQCare.SCM
{
    public partial class frmPharmacynotes : Form
    {
        private ReportDocument rptDocument;
        private string theReportSource = string.Empty;
        public frmPharmacynotes()
        {
            InitializeComponent();
        }

        private void frmPharmacynotes_Load(object sender, EventArgs e)
        {
            patientDetails();
            rptDocument = new ReportDocument();
            IQCareUtils theUtil = new IQCareUtils();
            IDrug ReportDetails = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataSet theDS = (DataSet)ReportDetails.GetPharmacyNotes(GblIQCare.patientID);
            theDS.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\PharmacyNotes.xml");

            rptPharmacyNotes rep = new rptPharmacyNotes();
            rep.SetDataSource(theDS);
            //frmPharmacyNotes theRepViewer = new frmPharmacyNotes();
            frmReportViewer theRepViewer = new frmReportViewer();
            theRepViewer.MdiParent = this.MdiParent;
            theRepViewer.Location = new Point(0, 0);
            theRepViewer.crViewer.ReportSource = rep;
            theRepViewer.crViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            theRepViewer.Width = 902;
            theRepViewer.Height = 650;
            theRepViewer.crViewer.Width = 880;
            theRepViewer.crViewer.Height = 600;
            theRepViewer.crViewer.Location = new Point(11, 11);
            theRepViewer.btnExit_position(451, 615);
            theRepViewer.Show();
            this.Close();
            
        }

        private void patientDetails()
        {
            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet theDS = PatientManager.GetPatientDetails(GblIQCare.patientID, GblIQCare.SystemId, GblIQCare.ModuleId);
            //System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            PatientManager = null;

            #region "Fill Details"
            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (GblIQCare.SystemId == 1)
                {
                    lblPatientname.Text = theDS.Tables[0].Rows[0]["LastName"].ToString() + ", " + theDS.Tables[0].Rows[0]["FirstName"].ToString();
                    lblAge.Text = theDS.Tables[0].Rows[0]["AGEINYEARMONTH"].ToString();
                    lblGender.Text = theDS.Tables[0].Rows[0]["SexNM"].ToString();
                    if (theDS.Tables[19].Rows.Count > 0)
                    {
                        if (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended")
                        {
                            lblptnstatus.Text = "Care Ended";
                        }

                        else if (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() != "Care Ended")
                        {
                            lblptnstatus.Text = "Active";
                        }

                        DataTable dt = new DataTable();
                        dt = theDS.Tables[42];
                        if (theDS.Tables[42].Rows.Count > 0)
                        {
                            if (dt.Rows[0]["PatientExitReason"].ToString() == "93")
                            {
                                lblptnstatus.Text = "Care Ended";

                            }
                        }

                    }

                }
                else
                {


                }

            }
            #endregion
        }
    }
}
