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
    public partial class MainPage : UserControl
    {
        DateTime transactionStartDate;
        DateTime transactionEndDate;

        public MainPage(IDictionary<string, string> Parameters)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "MMM-yyyy";
            this.mcChart.Title = "Average Total Cost per Patient per Month" + Environment.NewLine + "(Admin, Consult, All Lab, All Pharmacy)";
            this.mcChartAvgCD4.Title = "Average CD4 Cost per Patient per Month";
            //this.mcChartAvgExcludingCD4.Title = "Average Cost Other Labs Excluding CD4 per Patient per Month";
            this.mcChartAvgCostCoveredByProgramPatient.Title = "Total Average Cost of OIs Covered by Program, OIs-Purchased" + Environment.NewLine + " per Patient per Month";
            this.mcChartArvAvgCostCoveredByProgramPatient.Title = "Total Average Total Cost of ARVs Covered by Program," + Environment.NewLine + " ARVs Purchased  per Patient per Month";
            this.mcChartCumCostCoveredByProgramPatient.Title = "Cumulative Total Cost of OIs Covered by Program, OIs Purchased" + Environment.NewLine + " by Patient per Patient per Month";
            //this.DownloadArvAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;
            this.DownloadPatientsCostLink.Visibility = Visibility.Collapsed;
            this.DownloadPatientsCD4CostLink.Visibility = Visibility.Collapsed;
            this.DownloadPatientsExcludingCD4CostLink.Visibility = Visibility.Collapsed;
            this.DownloadAvgARVandOILink.Visibility = Visibility.Collapsed;
            this.DownloadCumARVandOILink.Visibility = Visibility.Collapsed;
            this.DownloadTotalCostLostToFollowupLink.Visibility = Visibility.Collapsed;
            this.DownloadCumTotalCostLostToFollowupLink.Visibility = Visibility.Collapsed;
            this.DownloadAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;
            this.DownloadArvAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;
            this.DownloadCumCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;

            if (Parameters.ContainsKey("TranDateFrom"))
            {
                if (Parameters["TranDateFrom"] != "")
                {
                    transactionStartDate = Convert.ToDateTime("01-" + Parameters["TranDateFrom"]);
                    DateTime tranToDate = Convert.ToDateTime("01-" + Parameters["TranDateTo"]);
                    transactionEndDate = Convert.ToDateTime(DateTime.DaysInMonth(tranToDate.Year, tranToDate.Month) + "-" + Parameters["TranDateTo"]);
                    this.LoadPatientCostPerMonthData(transactionStartDate, transactionEndDate);
                }
            }
        }

        public void LoadPatientCostPerMonthData(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityPatientsCostPerMonthCompleted += new EventHandler<GetFacilityPatientsCostPerMonthCompletedEventArgs>(this.client_GetFacilityPatientsCostPerMonthCompleted);
            client.GetFacilityPatientsCostPerMonthAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityPatientsCostPerMonthCompleted(object sender, GetFacilityPatientsCostPerMonthCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new PatientCost()
                             {
                                 MonthYear = item.Element("MonthYear").Value,
                                 AvgCost = item.Element("AvgCost").Value
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChart.Series[0]).ItemsSource = enumerable;

            if (Enumerable.ToList<PatientCost>(enumerable).Count > 0)
            {
                this.DownloadPatientsCostLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadPatientsCostLink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityAvgCD4CostPerPatient(transactionStartDate, transactionEndDate);
        }

        private void DownloadPatientsCostLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<PatientCost> itemsSource = (IEnumerable<PatientCost>)((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)this.mcChart.Series[0]).ItemsSource;
            Enumerable.ToList<PatientCost>(itemsSource).ExportList<PatientCost>();
        }

        public void LoadFacilityAvgCD4CostPerPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityAvgCD4CostPerPatientCompleted += new EventHandler<GetFacilityAvgCD4CostPerPatientCompletedEventArgs>(client_GetFacilityAvgCD4CostPerPatientCompleted);
            client.GetFacilityAvgCD4CostPerPatientAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityAvgCD4CostPerPatientCompleted(object sender, GetFacilityAvgCD4CostPerPatientCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new PatientCost()
                             {
                                 MonthYear = item.Element("MonthYear").Value,
                                 AvgCost = item.Element("AvgCost").Value
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartAvgCD4.Series[0]).ItemsSource = enumerable;

            if (Enumerable.ToList<PatientCost>(enumerable).Count > 0)
            {
                this.DownloadPatientsCD4CostLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadPatientsCD4CostLink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityAvgExcludingCD4CostPerPatient(transactionStartDate, transactionEndDate);
        }

        private void DownloadPatientsCD4CostLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<PatientCost> itemsSource = (IEnumerable<PatientCost>)((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)this.mcChartAvgCD4.Series[0]).ItemsSource;
            Enumerable.ToList<PatientCost>(itemsSource).ExportList<PatientCost>();
        }


        public void LoadFacilityAvgExcludingCD4CostPerPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityAvgExcludingCD4CostPerPatientCompleted += new EventHandler<GetFacilityAvgExcludingCD4CostPerPatientCompletedEventArgs>(this.client_GetFacilityAvgExcludingCD4CostPerPatientCompleted);
            client.GetFacilityAvgExcludingCD4CostPerPatientAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityAvgExcludingCD4CostPerPatientCompleted(object sender, GetFacilityAvgExcludingCD4CostPerPatientCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new PatientCost()
                             {
                                 MonthYear = item.Element("MonthYear").Value,
                                 AvgCost = item.Element("AvgCost").Value
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartAvgExcludingCD4.Series[0]).ItemsSource = enumerable;

            if (Enumerable.ToList<PatientCost>(enumerable).Count > 0)
            {
                this.DownloadPatientsExcludingCD4CostLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadPatientsExcludingCD4CostLink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityTotalAvgCostofARVandOIPerPatientPerMonth(transactionStartDate, transactionEndDate);
        }

        private void DownloadPatientsExcludingCD4CostLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<PatientCost> itemsSource = (IEnumerable<PatientCost>)((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)this.mcChartAvgExcludingCD4.Series[0]).ItemsSource;
            Enumerable.ToList<PatientCost>(itemsSource).ExportList<PatientCost>();
        }

        public void LoadFacilityTotalAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityTotalAvgCostofARVandOIPerPatientPerMonthCompleted += new EventHandler<GetFacilityTotalAvgCostofARVandOIPerPatientPerMonthCompletedEventArgs>(client_GetFacilityTotalAvgCostofARVandOIPerPatientPerMonthCompleted);
            client.GetFacilityTotalAvgCostofARVandOIPerPatientPerMonthAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityTotalAvgCostofARVandOIPerPatientPerMonthCompleted(object sender, GetFacilityTotalAvgCostofARVandOIPerPatientPerMonthCompletedEventArgs e)
        {
            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new SchemeResult()
                             {
                                 Name = item.Element("MonthYear").Value,
                                 Value = Convert.ToDecimal(item.Element("AvgCostForARV").Value)
                             };

            var enumerable1 = from item in xmlDoc.Descendants("Table")
                              select new SchemeResult()
                              {
                                  Name = item.Element("MonthYear").Value,
                                  Value = Convert.ToDecimal(item.Element("AvgCostForOI").Value)
                              };

            StackedColumnSeries series1 = (StackedColumnSeries)mcChartAvgARVandOI.Series[0];
            series1.Title = "ARVs";
            series1.ItemsSource = enumerable;


            StackedColumnSeries series2 = (StackedColumnSeries)mcChartAvgARVandOI.Series[1];
            series2.Title = "OIs";
            series2.ItemsSource = enumerable1;

            if (Enumerable.ToList<SchemeResult>(enumerable).Count > 0)
            {
                this.DownloadAvgARVandOILink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadAvgARVandOILink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityTotalCumCostofARVandOIPerPatientPerMonth(transactionStartDate, transactionEndDate);
        }

        private void DownloadAvgARVandOILink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<SchemeResult> itemsSource = (IEnumerable<SchemeResult>)((Facility.Reports.StackedColumnSeries)this.mcChartAvgARVandOI.Series[0]).ItemsSource;
            Enumerable.ToList<SchemeResult>(itemsSource).ExportList<SchemeResult>();
        }

        public void LoadFacilityTotalCumCostofARVandOIPerPatientPerMonth(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient(); 
            client.GetFacilityCumulAvgCostofARVandOIPerPatientPerMonthCompleted += new EventHandler<GetFacilityCumulAvgCostofARVandOIPerPatientPerMonthCompletedEventArgs>(client_GetFacilityCumulAvgCostofARVandOIPerPatientPerMonthCompleted);
            client.GetFacilityCumulAvgCostofARVandOIPerPatientPerMonthAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityCumulAvgCostofARVandOIPerPatientPerMonthCompleted(object sender, GetFacilityCumulAvgCostofARVandOIPerPatientPerMonthCompletedEventArgs e)
        {
            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new SchemeResult()
                             {
                                 Name = item.Element("MonthYear").Value,
                                 Value = Convert.ToDecimal(item.Element("TotalAvgCostForARV").Value)
                             };

            var enumerable1 = from item in xmlDoc.Descendants("Table")
                              select new SchemeResult()
                              {
                                  Name = item.Element("MonthYear").Value,
                                  Value = Convert.ToDecimal(item.Element("TotalAvgCostForOI").Value)
                              };

            StackedColumnSeries Cumseries1 = (StackedColumnSeries)mcChartCumARVandOI.Series[0];
            Cumseries1.Title = "ARVs";
            Cumseries1.ItemsSource = enumerable;


            StackedColumnSeries Cumseries2 = (StackedColumnSeries)mcChartCumARVandOI.Series[1];
            Cumseries2.Title = "OIs";
            Cumseries2.ItemsSource = enumerable1;

            if (Enumerable.ToList<SchemeResult>(enumerable).Count > 0)
            {
                this.DownloadCumARVandOILink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadCumARVandOILink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityTotalCostLostToFollowup(transactionStartDate, transactionEndDate);
        }

        private void DownloadCumARVandOILink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<SchemeResult> itemsSource = (IEnumerable<SchemeResult>)((Facility.Reports.StackedColumnSeries)this.mcChartCumARVandOI.Series[0]).ItemsSource;
            Enumerable.ToList<SchemeResult>(itemsSource).ExportList<SchemeResult>();
        }

        public void LoadFacilityTotalCostLostToFollowup(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityTotalCostLostToFollowupCompleted += new EventHandler<GetFacilityTotalCostLostToFollowupCompletedEventArgs>(client_GetFacilityTotalCostLostToFollowupCompleted);
            client.GetFacilityTotalCostLostToFollowupAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityTotalCostLostToFollowupCompleted(object sender, GetFacilityTotalCostLostToFollowupCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new PatientCost()
                             {
                                 MonthYear = item.Element("MonthYear").Value,
                                 AvgCost = item.Element("AvgCost").Value
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartTotalCostLostToFollowup.Series[0]).ItemsSource = enumerable;

            if (Enumerable.ToList<PatientCost>(enumerable).Count > 0)
            {
                this.DownloadTotalCostLostToFollowupLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadTotalCostLostToFollowupLink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityCumTotalCostLostToFollowup(transactionStartDate, transactionEndDate);
        }

        private void DownloadTotalCostLostToFollowupLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<PatientCost> itemsSource = (IEnumerable<PatientCost>)((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)this.mcChartTotalCostLostToFollowup.Series[0]).ItemsSource;
            Enumerable.ToList<PatientCost>(itemsSource).ExportList<PatientCost>();
        }

        public void LoadFacilityCumTotalCostLostToFollowup(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityCumTotalCostLostToFollowupCompleted += new EventHandler<GetFacilityCumTotalCostLostToFollowupCompletedEventArgs>(client_GetFacilityCumTotalCostLostToFollowupCompleted);
            client.GetFacilityCumTotalCostLostToFollowupAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityCumTotalCostLostToFollowupCompleted(object sender, GetFacilityCumTotalCostLostToFollowupCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new PatientCosts()
                             {
                                 MonthYear = Convert.ToDateTime(item.Element("MonthYear").Value),
                                 AvgCost = Convert.ToDecimal(item.Element("AvgCost").Value)
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.LineSeries)mcChartCumTotalCostLostToFollowup.Series[0]).ItemsSource = enumerable;

            if (Enumerable.ToList<PatientCosts>(enumerable).Count > 0)
            {
                this.DownloadCumTotalCostLostToFollowupLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadCumTotalCostLostToFollowupLink.Visibility = Visibility.Collapsed;
            }
            this.LoadFacilityAvgCostCovByProgramAndPatient(transactionStartDate, transactionEndDate);
        }

        private void DownloadCumTotalCostLostToFollowupLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<PatientCosts> itemsSource = (IEnumerable<PatientCosts>)((Microsoft.Windows.Controls.DataVisualization.Charting.LineSeries)this.mcChartCumTotalCostLostToFollowup.Series[0]).ItemsSource;
            Enumerable.ToList<PatientCosts>(itemsSource).ExportList<PatientCosts>();
        }

        public void LoadFacilityAvgCostCovByProgramAndPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityAvgCostCovByProgramAndPatientCompleted += new EventHandler<GetFacilityAvgCostCovByProgramAndPatientCompletedEventArgs>(client_GetFacilityAvgCostCovByProgramAndPatientCompleted);
            client.GetFacilityAvgCostCovByProgramAndPatientAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityAvgCostCovByProgramAndPatientCompleted(object sender, GetFacilityAvgCostCovByProgramAndPatientCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new Cost()
                             {
                                 MonthYear = item.Element("MonthYear").Value,
                                 AvgCost1 = item.Element("AvgCostForOICoveredByProg").Value,
                                 AvgCost2 = item.Element("AvgCostForOICoveredByPatient").Value
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartAvgCostCoveredByProgramPatient.Series[0]).ItemsSource = enumerable;
            ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartAvgCostCoveredByProgramPatient.Series[1]).ItemsSource = enumerable;


            if (Enumerable.ToList<Cost>(enumerable).Count > 0)
            {
                this.DownloadAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;
            }
            //this.LoadFacilityArvAvgCostCovByProgramAndPatient(transactionStartDate, transactionEndDate);
        }

        private void DownloadAvgCostCoveredByProgramPatientLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Cost> itemsSource = (IEnumerable<Cost>)((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)this.mcChartAvgCostCoveredByProgramPatient.Series[0]).ItemsSource;
            Enumerable.ToList<Cost>(itemsSource).ExportList<Cost>();
        }
        //public void LoadFacilityArvAvgCostCovByProgramAndPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        //{
        //    FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
        //    client.GetFacilityArvAvgCostCovByProgramAndPatientCompleted += new EventHandler<GetFacilityArvAvgCostCovByProgramAndPatientCompletedEventArgs>(client_GetFacilityArvAvgCostCovByProgramAndPatientCompleted);
        //    client.GetFacilityArvAvgCostCovByProgramAndPatientAsync(TransactionStartDate, TransactionEndDate);
        //}
        //private void client_GetFacilityArvAvgCostCovByProgramAndPatientCompleted(object sender, GetFacilityArvAvgCostCovByProgramAndPatientCompletedEventArgs e)
        //{

        //    XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

        //    var enumerable = from item in xmlDoc.Descendants("Table")
        //                     select new Cost()
        //                     {
        //                         MonthYear = item.Element("MonthYear").Value,
        //                         AvgCost1 = item.Element("AvgCostForOICoveredByProg").Value,
        //                         AvgCost2 = item.Element("AvgCostForOICoveredByPatient").Value
        //                     };

        //    ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartArvAvgCostCoveredByProgramPatient.Series[0]).ItemsSource = enumerable;
        //    ((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)mcChartArvAvgCostCoveredByProgramPatient.Series[1]).ItemsSource = enumerable;

        //    if (Enumerable.ToList<Cost>(enumerable).Count > 0)
        //    {
        //        this.DownloadArvAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        this.DownloadArvAvgCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;
        //    }
        //    this.LoadFacilityCumCostCovByProgramAndPatient(transactionStartDate, transactionEndDate);
        //}
        private void DownloadArvAvgCostCoveredByProgramPatientLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Cost> itemsSource = (IEnumerable<Cost>)((Microsoft.Windows.Controls.DataVisualization.Charting.ColumnSeries)this.mcChartArvAvgCostCoveredByProgramPatient.Series[0]).ItemsSource;
            Enumerable.ToList<Cost>(itemsSource).ExportList<Cost>();
        }

        public void LoadFacilityCumCostCovByProgramAndPatient(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            FacilityReportsServiceSoapClient client = new FacilityReportsServiceSoapClient();
            client.GetFacilityCumCostCovByProgramAndPatientCompleted += new EventHandler<GetFacilityCumCostCovByProgramAndPatientCompletedEventArgs>(client_GetFacilityCumCostCovByProgramAndPatientCompleted);
            client.GetFacilityCumCostCovByProgramAndPatientAsync(TransactionStartDate, TransactionEndDate);
        }

        private void client_GetFacilityCumCostCovByProgramAndPatientCompleted(object sender, GetFacilityCumCostCovByProgramAndPatientCompletedEventArgs e)
        {

            XDocument xmlDoc = XDocument.Parse(e.Result.Nodes[1].ToString());

            var enumerable = from item in xmlDoc.Descendants("Table")
                             select new Costs()
                             {
                                 MonthYear = Convert.ToDateTime(item.Element("MonthYear").Value),
                                 AvgCost1 = Convert.ToDecimal(item.Element("AvgCostForOICoveredByProg").Value),
                                 AvgCost2 = Convert.ToDecimal(item.Element("AvgCostForOICoveredByPatient").Value)
                             };

            ((Microsoft.Windows.Controls.DataVisualization.Charting.LineSeries)mcChartCumCostCoveredByProgramPatient.Series[0]).ItemsSource = enumerable;
            ((Microsoft.Windows.Controls.DataVisualization.Charting.LineSeries)mcChartCumCostCoveredByProgramPatient.Series[1]).ItemsSource = enumerable;

            if (Enumerable.ToList<Costs>(enumerable).Count > 0)
            {
                this.DownloadCumCostCoveredByProgramPatientLink.Visibility = Visibility.Visible;
            }
            else
            {
                this.DownloadCumCostCoveredByProgramPatientLink.Visibility = Visibility.Collapsed;
            }
        }

        private void DownloadCumCostCoveredByProgramPatientLink_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Costs> itemsSource = (IEnumerable<Costs>)((Microsoft.Windows.Controls.DataVisualization.Charting.LineSeries)this.mcChartCumCostCoveredByProgramPatient.Series[0]).ItemsSource;
            Enumerable.ToList<Costs>(itemsSource).ExportList<Costs>();
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

    public class PatientCost
    {
        public string AvgCost { get; set; }
        public string MonthYear { get; set; }
    }

    public class PatientCosts
    {
        public decimal AvgCost { get; set; }
        public DateTime MonthYear { get; set; }
    }

    public class SchemeResult
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class Cost
    {
        public string AvgCost1 { get; set; }
        public string AvgCost2 { get; set; }
        public string MonthYear { get; set; }
    }

    public class Costs
    {
        public decimal? AvgCost1 { get; set; }
        public decimal? AvgCost2 { get; set; }
        public DateTime MonthYear { get; set; }
    }

}
