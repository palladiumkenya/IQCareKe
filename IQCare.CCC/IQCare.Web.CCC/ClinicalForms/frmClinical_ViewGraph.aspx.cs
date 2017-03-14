using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartDirector;

namespace IQCare.Web.Clinical
{
    public partial class ViewGraph : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["whichGraph"].ToString() == "weight_bmi")
            {
                lblCD4.Visible = false;
                WebChartViewerCD4VL.Visible = false;
                Double[] Weight = (Double[])Session["Weight_graph"];
                Double[] BMI = (Double[])Session["BMI_graph"];
                DateTime[] YearWeightBMI = (DateTime[])Session["YearWeightBMI_graph"];
                createChartWeight(WebChartViewerWeight, Weight, BMI, YearWeightBMI);
            }
            else if (Session["whichGraph"].ToString() == "cd4")
            {
                lblWeightBMI.Visible = false;
                WebChartViewerWeight.Visible = false;
                double[] CD4 = (double[])Session["CD4_Graph"];
                double[] ViralLoad = (double[])Session["ViralLoad_Graph"];
                DateTime[] YearCD4 = (DateTime[])Session["YearCD4_Graph"];
                DateTime[] YearVL = (DateTime[])Session["YearVL_Graph"];
                DateTime[] Year = (DateTime[])Session["Year_Graph"];
                createChartCD4(WebChartViewerCD4VL, CD4, ViralLoad, YearCD4, YearVL, Year);
            }
            else
            {
            }

        }

        public void createChartWeight(WebChartViewer Wviewer, Double[] Weight, Double[] BMI, DateTime[] YearWeightBMI)
        {
            XYChart c = new XYChart(900, 570, 0xddddff, 0x000000, 1);
            c.addLegend(90, 10, false, "Arial Bold", 7).setBackground(0xcccccc);
            c.setPlotArea(60, 60, 700, 430, 0xffffff).setGridColor(0xcccccc, 0xccccccc);
            c.xAxis().setTitle("Year");
            c.xAxis().setLabelStyle("Arial", 8, 1).setFontAngle(90);
            c.yAxis().setLinearScale(0, 200, 10, 0);
            c.yAxis2().setLogScale(0, 1000, 10);

            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);
            layer.addDataSet(Weight, 0xff0000, "Weight").setDataSymbol(Chart.CircleShape, 5);
            int count = YearWeightBMI.Length;
            layer.setXData(YearWeightBMI);

            LineLayer layer1 = c.addLineLayer2();
            layer1.setLineWidth(2);
            layer1.setUseYAxis2();
            layer1.addDataSet(BMI, 0x008800, "BMI").setDataSymbol(Chart.CircleShape, 5);
            layer1.setXData(YearWeightBMI);

            // Output the chart
            Wviewer.Image = c.makeWebImage(Chart.PNG);
            //Include tool tip for the chart
            Wviewer.ImageMap = c.getHTMLImageMap("", "",
              "title='{dataSetName} Count on {xLabel}={value}'");
        }

        private void createChartCD4(WebChartViewer viewer, Double[] CD4, Double[] ViralLoad, DateTime[] YearCD4, DateTime[] YearVL, DateTime[] Year)
        {
            XYChart c = new XYChart(900, 570, 0xddddff, 0x000000, 1);
            c.addLegend(90, 10, false, "Arial Bold", 7).setBackground(0xcccccc);
            c.setPlotArea(60, 60, 700, 430, 0xffffff).setGridColor(0xcccccc, 0xccccccc);
            c.xAxis().setTitle("Year");
            c.xAxis().setLabelStyle("Arial", 8, 1).setFontAngle(90);
            c.yAxis().setLinearScale(0, 1500, 100, 0);
            c.yAxis2().setLogScale(10, 10000, 10);

            LineLayer layer = c.addLineLayer2();

            layer.setLineWidth(2);
            layer.addDataSet(CD4, 0xff0000, "CD4").setDataSymbol(Chart.CircleShape, 5);
            layer.setXData(YearCD4);

            LineLayer layer1 = c.addLineLayer2();
            layer1.setLineWidth(2);
            layer1.setUseYAxis2();
            layer1.addDataSet(ViralLoad, 0x008800, "Viralload").setDataSymbol(Chart.CircleShape, 5);
            layer1.setXData(YearVL);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);
            viewer.ImageMap = c.getHTMLImageMap("", "",
                "title='{dataSetName} Count on {xLabel}={value}'");
        }
    }
}