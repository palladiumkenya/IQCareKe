using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.SCM;
using Application.Common;
using Application.Presentation;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace IQCare.SCM
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void frmStockLedger_Load(object sender, EventArgs e)
        {
            BindCombo();
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);

        }

        private void BindCombo()
        {
            try
            {
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                BindFunctions theBindManager = new BindFunctions();

                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
                DataTable theStoreDT = theDV.ToTable();
                theBindManager.Win_BindCombo(ddlStore, theStoreDT, "Name", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (rdoStockLedger.Checked)
            {

                ReportDocument objRptDoc = new ReportDocument();
                //IMasterList objStockLedger = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                ISCMReport objStockLedger = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport,BusinessProcess.SCM");
                DataSet theDS = objStockLedger.GetStockLedgerData(Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text));
                theDS.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\StockLedger.xml");
                rptStockLedger rep = new rptStockLedger();
                rep.SetDataSource(theDS);
              //  rep.ParameterFields["FormDate","1"];
                rep.SetParameterValue("FromDate", dtpFrom.Text);
                rep.SetParameterValue("ToDate", dtpTo.Text);
                rep.SetParameterValue("ToClosingDate", (Convert.ToString(dtpTo.Text)).Replace('-', ' '));
                rep.SetParameterValue("facilityname", GblIQCare.AppLocation);
 
                   // , Convert.ToString(dtpFrom.Text)];

                frmReportViewer theRepViewer = new frmReportViewer();
                theRepViewer.MdiParent = this.MdiParent;
                theRepViewer.Location = new Point(0, 0);
                theRepViewer.crViewer.ReportSource = rep;
                theRepViewer.Show();
                this.Close();

            }

        }

        private void rdoStockLedger_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoStockLedger.Checked == true)
            {
                pnlStockLedger.Visible = true;
            }
            else
            {
                pnlStockLedger.Visible = false;

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
