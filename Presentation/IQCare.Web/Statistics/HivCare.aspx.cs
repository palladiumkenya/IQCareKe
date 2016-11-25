using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using Interface.Security;

namespace IQCare.Web.Statistics
{
    public partial class HivCare : BasePage
    {
        #region "Variable Declaration"

        public double noofart;
        public double noofArtabove15;
        public double noofArtupto15;
        // public double noofArtbelow1;
        public double noofArtupto2;

        public double noofArtupto4;
        public double nooffemale;
        public double noofmale;
        public double noofNonart;
        private System.Data.DataSet theDS;
        #endregion "Variable Declaration"

        protected void hlActiveARTPatients_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[5], "ActiveARTPatients");
        }

        protected void hlActiveNonARTPatients_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "ActiveNonARTPatients");
        }

        protected void hlARTFAbove15_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[22], "ARTFemaleabove15");
        }

        protected void hlARTFUpto14_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[21], "ARTFemale5-14");
        }

        protected void hlARTFUpto2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[19], "ARTFemale0-1");
        }

        protected void hlARTFUpto4_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[20], "ARTFemale2-4");
        }

        protected void hlARTMAbove15_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[18], "ARTMaleabove15");
        }

        protected void hlARTMortality_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[6], "ARTMortality");
        }

        protected void hlARTMUpto14_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[17], "ARTMale5-14");
        }

        protected void hlARTMUpto2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[15], "ARTMale0-1");
        }

        protected void hlARTMUpto4_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[16], "ARTMale2-4");
        }

        protected void hlDueforTermination_Click(object sender, EventArgs e)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            System.Data.DataSet dsARTUnknown = ReportDetails.GetPtnotvisitedrecentlyUnknown(Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToInt32(Session["AppLocationId"]));
            string FName = "ARTunknown";
            IQWebUtils theUtils = new IQWebUtils();
            string thePath = Server.MapPath("~\\ExcelFiles\\" + FName + ".xls");
            string theTemplatePath = Server.MapPath("~\\ExcelFiles\\IQCareTemplate.xls");
            theUtils.ExporttoExcel(dsARTUnknown.Tables[0], Response);
            Response.Redirect("~\\ExcelFiles\\" + FName + ".xls");
        }

        protected void hlEverEnrolledPatients_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[0], "EverEnrolledPatient");
        }

        protected void hlFemalesEnrolled_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[1], "FemalesEnroll");
        }

        protected void hlLosttoFollowUp_Click(object sender, EventArgs e)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            DataTable dtLosttoFollowupPatientReport = (DataTable)ReportDetails.GetLosttoFollowupPatientReport(Convert.ToInt32(Session["AppLocationId"])).Tables[0];
            string FName = "LstFollowup";
            IQWebUtils theUtils = new IQWebUtils();
            string thePath = Server.MapPath(".\\ExcelFiles\\" + FName + ".xls");
            string theTemplatePath = Server.MapPath(".\\ExcelFiles\\IQCareTemplate.xls");
            theUtils.ExporttoExcel(dtLosttoFollowupPatientReport, Response);
            Response.Redirect(".\\ExcelFiles\\" + FName + ".xls");
        }

        protected void hlMalesEnrolled_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "MalesEnroll");
        }

        protected void hlNonARTFAbove15_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[14], "NonARTFemaleabove15");
        }

        protected void hlNonARTFUpto14_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[13], "NonARTFemale5-14");
        }

        protected void hlNonARTFupto2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[12], "NonARTFemale2-4");
        }

        protected void hlNonARTFUpto2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[11], "NonARTFemale0-1");
        }

        protected void hlNonARTMAbove15_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[10], "NonARTMaleabove15");
        }

        protected void hlNonARTMUpto14_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "NonARTMale5-14");
        }

        protected void hlNonARTMUpto2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[7], "NonARTMale0-1");
        }

        protected void hlNonARTMUpto4_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[8], "NonARTMale2-4");
        }

        protected void hlTotalActivePatients_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "ActivePatients");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblLocation.Text = Session["FacilityName"].ToString();
                IFacility HIVCareFacManager;
                Double thePercent, theResultPercent, theTotalPateint;
                HIVCareFacManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
                theDS = HIVCareFacManager.GetHIVCareFacilityStats(Convert.ToInt16(Session["Facility"].ToString()));
                ViewState["theDS"] = theDS;

                lblTotalPatient.Text = theDS.Tables[0].Rows.Count.ToString();

                theTotalPateint = Convert.ToDouble(theDS.Tables[0].Rows.Count);

                /**********************Caluclate Female Patients *************/
                Double theTotFemale = Convert.ToDouble(theDS.Tables[1].Rows.Count);
                thePercent = (theTotFemale / theTotalPateint) * 100;
                if (theTotalPateint != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblFemalePatient.Text = theDS.Tables[1].Rows.Count + " " + "(" + theResultPercent + "%)";

                ///**********************Calculate Male Patients ***************/
                Double theTotMale = Convert.ToDouble(theDS.Tables[2].Rows.Count);
                thePercent = (theTotMale / theTotalPateint) * 100;
                if (theTotalPateint != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblMalePatient.Text = theDS.Tables[2].Rows.Count + " " + "(" + theResultPercent + "%)";
                lblTotalActivePatients.Text = theDS.Tables[3].Rows.Count.ToString();

                ///*********************Calculate NonART AND Pateint % ****************/
                Double theActivePatient = Convert.ToDouble(theDS.Tables[3].Rows.Count);
                Double theTotNonArtPatient = Convert.ToDouble(theDS.Tables[4].Rows.Count);
                thePercent = (theTotNonArtPatient / theActivePatient) * 100;
                if (theActivePatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblActiveNonARTPatients.Text = theDS.Tables[4].Rows.Count + " " + "(" + theResultPercent + "%)";

                ///*********************Calculate ART AND Pateint % ****************/

                Double theTotArtPatient = Convert.ToDouble(theDS.Tables[5].Rows.Count);

                thePercent = (theTotArtPatient / theActivePatient) * 100;
                if (theActivePatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblActiveARTPatients.Text = theDS.Tables[5].Rows.Count + " " + "(" + theResultPercent + "%)";

                ///*********************Calculate ARTMortality %  ****************/

                Double theARTMortality = Convert.ToDouble(theDS.Tables[6].Rows.Count);
                Double theTotalARTPatient = Convert.ToDouble(theDS.Tables[5].Rows.Count);
                thePercent = (theARTMortality / theTotalARTPatient) * 100;
                if (theTotalARTPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTMortality.Text = Convert.ToDouble(theDS.Tables[6].Rows.Count).ToString();
                // lblARTMortality.Text = theDS.Tables[6].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************ Calculate Non-ART Male Patient % By Age and Sex *************************/
                /*** Non-ART Male Patient 0-1 *****/
                Double theNonARTPtn;
                theNonARTPtn = Convert.ToDouble(theDS.Tables[7].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTMUpto2.Text = theDS.Tables[7].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** Non-ART Male Patient 2-4 *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[8].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTMUpto4.Text = theDS.Tables[8].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** Non-ART Male Patient 5-14 *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[9].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTMUpto14.Text = theDS.Tables[9].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** Non-ART Male Patient 15+ *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[10].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTMAbove15.Text = theDS.Tables[10].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************ Calculate Non-ART Female Patient % By Age and Sex *************************/
                /*** Non-ART Female Patient 0-1 *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[11].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTFupto2.Text = theDS.Tables[11].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** Non-ART Female Patient 2-4 *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[12].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTFupto4.Text = theDS.Tables[12].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** Non-ART Female Patient 5-14 *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[13].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }

                lblNonARTFUpto14.Text = theDS.Tables[13].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** Non-ART Female Patient 15+ *****/
                theNonARTPtn = Convert.ToDouble(theDS.Tables[14].Rows.Count);
                thePercent = (theNonARTPtn / theTotNonArtPatient) * 100;
                if (theTotNonArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblNonARTFAbove15.Text = theDS.Tables[14].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************theTotArtPatient Calculate ART Pateint % By Age and Sex *************************/

                Double theARTPtn, TotARTFMUpto2, TotARTFMUpto5, TotARTFMUpto15, TotARTFMAbove15;

                /*** ART Male Patient 0-1 *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[15].Rows.Count);

                TotARTFMUpto2 = theARTPtn; //For Graph
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTMUpto2.Text = theDS.Tables[15].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Male Patient 2-4 *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[16].Rows.Count);
                TotARTFMUpto5 = theARTPtn;//For Graph
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTMUpto4.Text = theDS.Tables[16].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Male Patient 5-14 *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[17].Rows.Count);
                TotARTFMUpto15 = theARTPtn;//For Graph
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTMUpto14.Text = theDS.Tables[17].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Male Patient 15+ *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[18].Rows.Count);
                TotARTFMAbove15 = theARTPtn; //For Graph
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTMAbove15.Text = theDS.Tables[18].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Female Patient 0-1 *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[19].Rows.Count);
                TotARTFMUpto2 = TotARTFMUpto2 + theARTPtn;//For Graph
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTFUpto2.Text = theDS.Tables[19].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Female Patient 2-4 *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[20].Rows.Count);
                TotARTFMUpto5 = TotARTFMUpto5 + theARTPtn;//For Graph
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTFUpto4.Text = theDS.Tables[20].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Female Patient 5-14 *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[21].Rows.Count);
                TotARTFMUpto15 = TotARTFMUpto15 + theARTPtn;
                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTFUpto14.Text = theDS.Tables[21].Rows.Count + " " + "(" + theResultPercent + "%)";

                /*** ART Female Patient 15+ *****/
                theARTPtn = Convert.ToDouble(theDS.Tables[22].Rows.Count);
                TotARTFMAbove15 = TotARTFMAbove15 + theARTPtn;

                thePercent = (theARTPtn / theTotArtPatient) * 100;
                if (theTotArtPatient != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblARTFAbove15.Text = theDS.Tables[22].Rows.Count + " " + "(" + theResultPercent + "%)";
                PieChart(theTotFemale, theTotMale);
                ArtNonArtPieChart(theTotArtPatient, theTotNonArtPatient);
                PerArtAge(TotARTFMUpto2, TotARTFMUpto5, TotARTFMUpto15, TotARTFMAbove15, theTotArtPatient);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void ArtNonArtPieChart(double TotArtPatient, double TotNonArtPatient)
        {
            noofart = TotArtPatient;
            noofNonart = TotNonArtPatient;

        }

        private void PerArtAge(double TotARTFMUpto2, double TotARTFMUpto5, double TotARTFMUpto15, double TotARTFMAbove15, double theTotArtPatient)
        {
            //if (Convert.ToInt32(TotARTFMUpto2) < 1 || Convert.ToInt32(TotARTFMUpto5) < 1 || Convert.ToInt32(TotARTFMUpto15) < 1 || Convert.ToInt32(TotARTFMAbove15) < 1 || Convert.ToInt32(theTotArtPatient) < 1)
            //    return;

            //noofArtbelow1=theTotArtPatient;
            noofArtupto2 = TotARTFMUpto2;
            noofArtupto4 = TotARTFMUpto5;
            noofArtupto15 = TotARTFMUpto15;
            noofArtabove15 = TotARTFMAbove15;

        }

        private void PieChart(double TotFemale, double TotMale)
        {
            noofmale = TotMale;
            nooffemale = TotFemale;

        }

        private void ShowReport(DataTable theTable, string FileName)
        {
            IQWebUtils theUtils = new IQWebUtils();
            theUtils.ExporttoExcel(theTable, Response);
        }
    }
}