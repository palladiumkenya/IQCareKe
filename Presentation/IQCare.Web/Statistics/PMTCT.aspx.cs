using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Security;

namespace IQCare.Web.Statistics
{
    public partial class PMTCT : BasePage
    {
        DataSet theDS;
        private void ShowReport(DataTable theTable, string FileName)
        {
            IQWebUtils theUtils = new IQWebUtils();
            theUtils.ExporttoExcel(theTable, Response);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                lblLocation.Text = Session["FacilityName"].ToString();
                IFacility PMTCTFacManager;
                Double thePercent, theResultPercent;
                //, theTotalPateint, theTotalPMTCTPatient;
                PMTCTFacManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
                theDS = PMTCTFacManager.GetFacilityStatsPMTCT(Convert.ToInt16(Session["Facility"].ToString()));
                ViewState["theDS"] = theDS;


                /************************Cumulative Mothers Ever in PMTCT*************************/
                lblMothersEverEnroll.Text = theDS.Tables[0].Rows.Count.ToString();

                /************************Current Mothers in PMTCT*************************/
                Double theCurrentPMTCTMothers = Convert.ToDouble(theDS.Tables[1].Rows.Count);
                lblCurrentMothers.Text = theDS.Tables[1].Rows.Count.ToString();

                /************************Current Number of Women on ARV Prophylaxis *************************/
                /***ANC***/
                lblProANC.Text = theDS.Tables[2].Rows.Count.ToString();

                /***L&D***/
                lblProLD.Text = theDS.Tables[3].Rows.Count.ToString();
                /***PN***/
                lblProPN.Text = theDS.Tables[4].Rows.Count.ToString();

                /************************Current ANC Mothers ************************************************/
                Double theCurrentANCMothers = Convert.ToDouble(theDS.Tables[5].Rows.Count);
                thePercent = (theCurrentANCMothers / theCurrentPMTCTMothers) * 100;
                if (theCurrentPMTCTMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblCurrentANCMothers.Text = theDS.Tables[5].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current ANC HIV+ Mothers ******************************************/
                Double theCurrentANCHIVPosMothers = Convert.ToDouble(theDS.Tables[6].Rows.Count);
                thePercent = (theCurrentANCHIVPosMothers / theCurrentANCMothers) * 100;
                if (theCurrentANCMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblANCHIVPosMothers.Text = theDS.Tables[6].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current ANC HIV+ Mothers HIV+ Partner******************************/
                Double theANCPosMotherPosPartner = Convert.ToDouble(theDS.Tables[7].Rows.Count);
                thePercent = (theANCPosMotherPosPartner / theCurrentANCHIVPosMothers) * 100;
                if (theCurrentANCHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPosMotherPosPartner.Text = theDS.Tables[7].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current ANC HIV+ Mothers HIV- Partner*****************************/
                Double theANCPosMotherNegPartner = Convert.ToDouble(theDS.Tables[8].Rows.Count);
                thePercent = (theANCPosMotherNegPartner / theCurrentANCHIVPosMothers) * 100;
                if (theCurrentANCHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPosMotherNegPartner.Text = theDS.Tables[8].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current ANC HIV+ Mothers Unknown Partner**************************/
                Double theANCPosMotherUnknownPartner = Convert.ToDouble(theDS.Tables[9].Rows.Count);
                thePercent = (theANCPosMotherUnknownPartner / theCurrentANCHIVPosMothers) * 100;
                if (theCurrentANCHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPosMotherUnknownPartner.Text = theDS.Tables[9].Rows.Count + " " + "(" + theResultPercent + "%)";
                /************************Current ANC HIV- Mothers ******************************************/
                Double theCurrentANCHIVNegMothers = Convert.ToDouble(theDS.Tables[10].Rows.Count);
                thePercent = (theCurrentANCHIVNegMothers / theCurrentANCMothers) * 100;
                if (theCurrentANCMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblHIVNegMothers.Text = theDS.Tables[10].Rows.Count + " " + "(" + theResultPercent + "%)";
                /************************Current ANC HIV- Mothers HIV+ Partner******************************/
                Double theANCHIVNegMotherPosPartner = Convert.ToDouble(theDS.Tables[11].Rows.Count);
                thePercent = (theANCHIVNegMotherPosPartner / theCurrentANCHIVNegMothers) * 100;
                if (theCurrentANCHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkNegMotherPosPartner.Text = theDS.Tables[11].Rows.Count + " " + "(" + theResultPercent + "%)";
                /************************Current ANC HIV- Mothers HIV- Partner*****************************/
                Double theANCHIVNegMotherNegPartner = Convert.ToDouble(theDS.Tables[12].Rows.Count);
                thePercent = (theANCHIVNegMotherNegPartner / theCurrentANCHIVNegMothers) * 100;
                if (theCurrentANCHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkNegMotherNegPartner.Text = theDS.Tables[12].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current ANC HIV- Mothers Unknown Partner**************************/
                Double theANCHIVNegMotherUnknownPartner = Convert.ToDouble(theDS.Tables[13].Rows.Count);
                thePercent = (theANCHIVNegMotherUnknownPartner / theCurrentANCHIVNegMothers) * 100;
                if (theCurrentANCHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkNegMotherUnknownPartner.Text = theDS.Tables[13].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D Mothers*********************************************/
                Double theCurrentLDMothers = Convert.ToDouble(theDS.Tables[14].Rows.Count);
                thePercent = (theCurrentLDMothers / theCurrentPMTCTMothers) * 100;
                if (theCurrentPMTCTMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblCurrentLDMothers.Text = theDS.Tables[14].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV+ Mothers ****************************************/
                Double theCurrentLDHIVPosMothers = Convert.ToDouble(theDS.Tables[15].Rows.Count);
                thePercent = (theCurrentLDHIVPosMothers / theCurrentLDMothers) * 100;
                if (theCurrentLDMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblLDHIVPosMothers.Text = theDS.Tables[15].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV+ Mothers HIV+ Partner***************************/
                Double theCurrentLDPosMotherPosPartner = Convert.ToDouble(theDS.Tables[16].Rows.Count);
                thePercent = (theCurrentLDPosMotherPosPartner / theCurrentLDHIVPosMothers) * 100;
                if (theCurrentLDHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkLDPosMotherPosPartner.Text = theDS.Tables[16].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV+ Mothers HIV- Partner***************************/
                Double theCurrentLDPosMotherNegPartner = Convert.ToDouble(theDS.Tables[17].Rows.Count);
                thePercent = (theCurrentLDPosMotherNegPartner / theCurrentLDHIVPosMothers) * 100;
                if (theCurrentLDHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkLDPosMotherNegPartner.Text = theDS.Tables[17].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV+ Mothers Unknown Partner*************************/
                Double theCurrentLDPosMotherUnknownPartner = Convert.ToDouble(theDS.Tables[18].Rows.Count);
                thePercent = (theCurrentLDPosMotherUnknownPartner / theCurrentLDHIVPosMothers) * 100;
                if (theCurrentLDHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkLDPosMotherUnknownPartner.Text = theDS.Tables[18].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV- Mothers ******************************************/
                Double theCurrentLDHIVNegMothers = Convert.ToDouble(theDS.Tables[19].Rows.Count);
                thePercent = (theCurrentLDHIVNegMothers / theCurrentLDMothers) * 100;
                if (theCurrentLDMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblLDHIVNegMothers.Text = theDS.Tables[19].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV- Mothers HIV+ Partner******************************/
                Double theCurrentLDNegMotherPosPartner = Convert.ToDouble(theDS.Tables[20].Rows.Count);
                thePercent = (theCurrentLDNegMotherPosPartner / theCurrentLDHIVNegMothers) * 100;
                if (theCurrentLDHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkLDNegMotherPosPartner.Text = theDS.Tables[20].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV- Mothers HIV- Partner*****************************/
                Double theCurrentLDNegMotherNegPartner = Convert.ToDouble(theDS.Tables[21].Rows.Count);
                thePercent = (theCurrentLDNegMotherNegPartner / theCurrentLDHIVNegMothers) * 100;
                if (theCurrentLDHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkLDNegMotherNegPartner.Text = theDS.Tables[21].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current L&D HIV- Mothers Unknown Partner**************************/
                Double theCurrentLDNegMotherUnknownPartner = Convert.ToDouble(theDS.Tables[22].Rows.Count);
                thePercent = (theCurrentLDNegMotherUnknownPartner / theCurrentLDHIVNegMothers) * 100;
                if (theCurrentLDHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkLDNegMotherUnknownPartner.Text = theDS.Tables[22].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN Mothers *********************************************/
                Double theCurrentPNMothers = Convert.ToDouble(theDS.Tables[23].Rows.Count);
                thePercent = (theCurrentPNMothers / theCurrentPMTCTMothers) * 100;
                if (theCurrentPMTCTMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblCurrentPNMothers.Text = theDS.Tables[23].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV+ Mothers ****************************************/
                Double theCurrentPNHIVPosMothers = Convert.ToDouble(theDS.Tables[24].Rows.Count);
                thePercent = (theCurrentPNHIVPosMothers / theCurrentPNMothers) * 100;
                if (theCurrentPNMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblPNHIVPosMothers.Text = theDS.Tables[24].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV+ Mothers HIV+ Partner***************************/
                Double theCurrentPNPosMotherPosPartner = Convert.ToDouble(theDS.Tables[25].Rows.Count);
                thePercent = (theCurrentPNPosMotherPosPartner / theCurrentPNHIVPosMothers) * 100;
                if (theCurrentPNHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPNPosMotherPosPartner.Text = theDS.Tables[25].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV+ Mothers HIV- Partner***************************/
                Double theCurrentPNPosMotherNegPartner = Convert.ToDouble(theDS.Tables[26].Rows.Count);
                thePercent = (theCurrentPNPosMotherNegPartner / theCurrentPNHIVPosMothers) * 100;
                if (theCurrentPNHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPNPosMotherNegPartner.Text = theDS.Tables[26].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV+ Mothers Unknown Partner*************************/
                Double theCurrentPNPosMotherUnknownPartner = Convert.ToDouble(theDS.Tables[27].Rows.Count);
                thePercent = (theCurrentPNPosMotherUnknownPartner / theCurrentPNHIVPosMothers) * 100;
                if (theCurrentPNHIVPosMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPNPosMotherUnknownPartner.Text = theDS.Tables[27].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV- Mothers ****************************************/
                Double theCurrentPNHIVNegMothers = Convert.ToDouble(theDS.Tables[28].Rows.Count);
                thePercent = (theCurrentPNHIVNegMothers / theCurrentPNMothers) * 100;
                if (theCurrentPNMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lblPNHIVNegMothers.Text = theDS.Tables[28].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV- Mothers HIV+ Partner***************************/
                Double theCurrentPNNegMotherPosPartner = Convert.ToDouble(theDS.Tables[29].Rows.Count);
                thePercent = (theCurrentPNNegMotherPosPartner / theCurrentPNHIVNegMothers) * 100;
                if (theCurrentPNHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPNNegMotherPosPartner.Text = theDS.Tables[29].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV- Mothers HIV- Partner***************************/
                Double theCurrentPNNegMotherNegPartner = Convert.ToDouble(theDS.Tables[30].Rows.Count);
                thePercent = (theCurrentPNNegMotherNegPartner / theCurrentPNHIVNegMothers) * 100;
                if (theCurrentPNHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPNNegMotherNegPartner.Text = theDS.Tables[30].Rows.Count + " " + "(" + theResultPercent + "%)";

                /************************Current PN HIV- Mothers Unknown Partner*************************/
                Double theCurrentPNNegMotherUnknownPartner = Convert.ToDouble(theDS.Tables[31].Rows.Count);
                thePercent = (theCurrentPNNegMotherUnknownPartner / theCurrentPNHIVNegMothers) * 100;
                if (theCurrentPNHIVNegMothers != 0)
                {
                    theResultPercent = System.Math.Round(thePercent);
                }
                else
                {
                    theResultPercent = 0;
                }
                lnkPNNegMotherUnknownPartner.Text = theDS.Tables[31].Rows.Count + " " + "(" + theResultPercent + "%)";

            }

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }


        }
        protected void hlMothersEverEnroll_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[0], "PMTCTEverEnrollMothers");
        }
        protected void hlCurrentMothers_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[1], "PMTCTCurrentMothers");
        }
        protected void hlProANC_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "ProANCMothers");

        }
        protected void hlProLD_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "ProLDMothers");
        }
        protected void hlProPN_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "ProPNMothers");
        }
        protected void hlPosMotherPosPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[7], "ANCPosMotherPosPartner");
        }
        protected void hlPosMotherNegPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[8], "ANCPosMotherNegPartner");

        }
        protected void hlPosMotherUnknownPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "ANCPosMotherUnknownPartner");
        }
        protected void hlNegMotherPosPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[11], "ANCNegMotherPosPartner");

        }
        protected void hlNegMotherNegPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[12], "ANCNegMotherNegPartner");

        }

        protected void hlNegMotherUnknownPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[13], "ANCNegMotherUnknownPartner");

        }
        protected void hlLDPosMotherPosPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[16], "LDPosMotherPosPartner");

        }
        protected void hlLDPosMotherNegPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[17], "LDPosMotherNegPartner");

        }
        protected void hlLDPosMotherUnknownPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[18], "LDPosMotherUnknownPartner");

        }
        protected void hlLDNegMotherPosPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[20], "LDNegMotherPosPartner");

        }
        protected void hlLDNegMotherNegPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[21], "LDNegMotherNegPartner");

        }
        protected void hlLDNegMotherUnknownPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[22], "LDNegMotherUnknownPartner");

        }
        protected void hlPNPosMotherPosPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[25], "PNPosMotherPosPartner");

        }
        protected void hlPNPosMotherNegPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[26], "PNPosMotherNegPartner");

        }
        protected void hlPNPosMotherUnknownPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[27], "PNPosMotherUnknownPartner");

        }
        protected void hlPNNegMotherPosPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[29], "PNNegMotherPosPartner");

        }
        protected void hlPNNegMotherNegPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[30], "PNNegMotherNegPartner");
        }
        protected void hlPNNegMotherUnknownPartner_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[31], "PNNegMotherUnknownPartner");
        }
    }
}