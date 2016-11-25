using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;
using Facility.Reports.FacilityReportsService;
using System.Xml.Linq;
using Microsoft.Windows.Controls.DataVisualization;
using Microsoft.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Browser;
using System.ServiceModel;

namespace Facility.Reports
{
    public partial class DebitNote : UserControl
    {
        public DebitNote(IDictionary<string, string> Parameters)
        {
            InitializeComponent();
            if (Parameters.ContainsKey("PatientID"))
            {
                if (Parameters["PatientID"] != "")
                {
                    this.LoadPatientDebitNoteTotalCostByMonth(Convert.ToInt32( Parameters["PatientID"]));
                }
            }
        }

        public void LoadPatientDebitNoteTotalCostByMonth(int PatientID)
        {

            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetPatientDebitNoteTotalCostByMonthCompleted +=new EventHandler<GetPatientDebitNoteTotalCostByMonthCompletedEventArgs>(client_GetPatientDebitNoteTotalCostByMonthCompleted);
            client.GetPatientDebitNoteTotalCostByMonthAsync(PatientID);
        }

        private void client_GetPatientDebitNoteTotalCostByMonthCompleted(object sender, GetPatientDebitNoteTotalCostByMonthCompletedEventArgs e)
        {
            try
            {
                XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

                var enumerable = from item in xmlDoc.Descendants("Table")
                                 select new SchemeData()
                                 {
                                     Name = Convert.ToDateTime(item.Element("VisitDate").Value),
                                     Value = Convert.ToDecimal(item.Element("TotalCost").Value)
                                 };

                ((Microsoft.Windows.Controls.DataVisualization.Charting.LineSeries)mcChart.Series[0]).ItemsSource = enumerable;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("An application error occured.");
                MessageBox.Show("No Data to display Graph.");
            }
        }

        private void mcChart_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.ToggleFullScreen();
        }

        private void ToggleFullScreen()
        {
            Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
        }
    }

    public class SchemeData
    {
        public DateTime Name { get; set; }
        public decimal Value { get; set; }
    }
}
