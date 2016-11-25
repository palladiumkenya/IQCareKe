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
using System.Windows.Browser;

namespace IQCare_Graphs
{
    public partial class MainPage : UserControl
    {

        public string graphTitle = string.Empty;
        private double _oldUserControlWidth = 0.0;
        private double _oldUserControlHeight = 0.0;

        public MainPage()
        {
           
            InitializeComponent();
            _oldUserControlWidth = this.Width;

            _oldUserControlHeight = this.Height;
            this.Loaded += new RoutedEventHandler(Page_Loaded);
           // Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreen);
           // Application.Current.Host.Content.Resized += new EventHandler(Content_Resized);
            RetrieveParameterFromHost();
        }
        /// <summary>
        /// After the UserControl Loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ctlGridChart_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            ToggleFullScreen(); 
        }    
        private void ToggleFullScreen() {
            Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen; 
        }
        private void Content_FullScreen(object sender, EventArgs e)
        {
            try
            {
                if (Application.Current.Host.Content.IsFullScreen)
                {


                    //AnnotationCtrl.Visibility = Visibility.Collapsed;
                    //canvas.Width = Application.Current.Host.Content.ActualWidth;
                    //canvas.Height = Application.Current.Host.Content.ActualHeight;

                    //svImage.Width = Application.Current.Host.Content.ActualWidth;
                    //svImage.Height = Application.Current.Host.Content.ActualHeight - 30;
                    //RootLayoutScaleTransform.ScaleX = 1;
                    //RootLayoutScaleTransform.ScaleY = 1;

                }
                else
                {
                    //AnnotationCtrl.Visibility = Visibility.Visible;
                    //canvas.Height = 486;
                    //canvas.Width = 375;
                    //svImage.Height = 300;
                    //svImage.Width = 375;

                }

            }
            catch (Exception ex)
            {
                System.Windows.Browser.HtmlPage.Window.Alert(ex.Message + ",    " + ex.StackTrace);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
        /// <summary>
        /// This method Retrieves Parameter from the Host
        /// </summary>
     [ScriptableMember]
        private void RetrieveParameterFromHost()
        {
            object queryString;
            ScriptObject jsObj;
            string[] paramQuery;
            switch (GlobalVar.GraphType)
            {
                case "GetMaleFemaleEnrolledPieChartinfo":

                    jsObj = (ScriptObject)HtmlPage.Window.GetProperty("GetMaleFemaleEnrolledPieChartinfo");
                    queryString = jsObj.InvokeSelf(null);
                    paramQuery = queryString.ToString().Split('~');
                    DataContext = DataModel.MaleFemaleEnrolled_PieGraph(paramQuery[0], paramQuery[1]);
                   
                   // MFenollrd.Title = "Percent Males and Females Enrolled";
                   
                    return;
                case "GetArt_NorartPieChartinfo":

                    jsObj = (ScriptObject)HtmlPage.Window.GetProperty("GetArt_NorartPieChartinfo");
                    queryString = jsObj.InvokeSelf(null);
                    paramQuery = queryString.ToString().Split('~');
                    DataContext = DataModel.ArtandNonArt_PieGraph(paramQuery[0], paramQuery[1]);
                 
                   // MFenollrd.Title = "Art & Non Art ";

                    return;

                case "GetArtByAgeChartinfo":

                    jsObj = (ScriptObject)HtmlPage.Window.GetProperty("GetArtByAgeChartinfo");
                    queryString = jsObj.InvokeSelf(null);
                    paramQuery = queryString.ToString().Split('~');
                    DataContext = DataModel.GetArtByAgeChartinfo_PieGraph(paramQuery[0], paramQuery[1], paramQuery[2], paramQuery[3]);
                  //  MFenollrd.Title = "Art by age ";

                    return;
                default:
                    return;

            }
           
            
        }

     private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
     {

     }
    }

    public class clsMakePieGraph
    {
        public double dependentAttibute { get; set; }
        public string independentAttribute { get; set; }
    }
    public class DataModel
    {
        private readonly IList<clsMakePieGraph> _data = new List<clsMakePieGraph>();

        public IList<clsMakePieGraph> Data
        {
            get { return _data; }
        }

        public static DataModel MaleFemaleEnrolled_PieGraph(string strmaleinfo, string strfemaleinfo)
        {
            string[] param1 = strmaleinfo.ToString().Split('#');
            string[] param2 = strfemaleinfo.ToString().Split('#');

            DataModel m = new DataModel();
            try
            {
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param1[1]), independentAttribute = param1[0] });
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param2[1]), independentAttribute = param2[0] });  
            }
            catch (Exception ex)
            {
                HtmlPage.Window.Alert("MaleFemaleEnrolled_PieGraph:  " + ex.Message);
            }
            return m;
        }
        public static DataModel ArtandNonArt_PieGraph(string strartinfo, string strnonartinfo)
        {
            string[] param1 = strartinfo.ToString().Split('#');
            string[] param2 = strnonartinfo.ToString().Split('#');

            DataModel m = new DataModel();
            try
            {
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param1[1]), independentAttribute = param1[0] });
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param2[1]), independentAttribute = param2[0] });
            }
            catch (Exception ex)
            {
                HtmlPage.Window.Alert("ArtandNonArt_PieGraph:  " + ex.Message);
            }
            return m;
        }

        public static DataModel GetArtByAgeChartinfo_PieGraph(string input1, string input2, string input3, string input4)
        {
            string[] param1 = input1.ToString().Split('#');
            string[] param2 = input2.ToString().Split('#');
            string[] param3 = input3.ToString().Split('#');
            string[] param4 = input4.ToString().Split('#');
           
            

            DataModel m = new DataModel();
            try
            {
                m.Data.Add(new clsMakePieGraph { dependentAttibute =Convert.ToDouble(param1[1]), independentAttribute = param1[0] });
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param2[1]), independentAttribute = param2[0] });
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param3[1]), independentAttribute = param3[0] });
                m.Data.Add(new clsMakePieGraph { dependentAttibute = Convert.ToDouble(param4[1]), independentAttribute = param4[0] });
            }
            catch (Exception ex)
            {
                HtmlPage.Window.Alert("GetArtByAgeChartinfo_PieGraph:  " + ex.Message);
            }
            return m;
        }
    }
}
