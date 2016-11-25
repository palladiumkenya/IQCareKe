using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.ReportSource;
//using CrystalDecisions.Shared;
//using System.Xml;
//using System.Xml.XPath;
//using System.Xml.Schema;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
namespace IQCare.SCM
{
    public partial class frmPatientClinicalSummary : Form
    {
        private ReportDocument rptDocument;
        private string theReportSource = string.Empty;

        public frmPatientClinicalSummary()
        {
            InitializeComponent();
        }

        private void frmPatientClinicalSummary_Load(object sender, EventArgs e)
        {
            //rptDocument = new ReportDocument();
            //IQCareUtils theUtil = new IQCareUtils();
            //IPatientHome ReportDetails = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome,BusinessProcess.Clinical");
            //DataSet theDS = (DataSet)ReportDetails.GetPatientSummaryInformation(GblIQCare.patientID, GblIQCare.ModuleId);
            //string filename = GblIQCare.GetXMLPath() + "PatientClinicalSummary.xml";
            //System.IO.FileStream myFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            //System.Xml.XmlTextWriter MyXmlTextWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);
            //theDS.WriteXmlSchema(MyXmlTextWriter);
            //MyXmlTextWriter.Close();
            //ReportDetails = null;

            //theReportSource = GblIQCare.GetReportPath() + "rptPatientClinicalSummary.rpt";
            //rptDocument.Load(theReportSource);
            //rptDocument.SetDataSource(theDS);
            //crViewer.ReportSource = rptDocument;
            //crViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;

            rptDocument = new ReportDocument();
            IQCareUtils theUtil = new IQCareUtils();
            IPatientHome ReportDetails = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome,BusinessProcess.Clinical");
            DataSet theDS = (DataSet)ReportDetails.GetPatientSummaryInformation(GblIQCare.patientID, GblIQCare.ModuleId);
            theDS.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\PatientClinicalSummary.xml");

            rptPatientClinicalSummary rep = new rptPatientClinicalSummary();
            rep.SetDataSource(theDS);
            frmReportViewer theRepViewer = new frmReportViewer();
            theRepViewer.MdiParent = this.MdiParent;
            theRepViewer.Location = new Point(0, 0);
            theRepViewer.crViewer.ReportSource = rep;
            theRepViewer.crViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            theRepViewer.Width = 902;
            theRepViewer.Height = 650;
            theRepViewer.crViewer.Width = 880;
            theRepViewer.crViewer.Height = 600;
            theRepViewer.crViewer.Location = new Point(11,11);
            theRepViewer.btnExit_position(451, 615);
            theRepViewer.Show();
            this.Close();

        }
    }
}
