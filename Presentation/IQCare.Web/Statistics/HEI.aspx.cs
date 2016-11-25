using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Security;

namespace IQCare.Web.Statistics
{
    public partial class HEI : BasePage
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
                if (IsPostBack != true)
                {
                    Init_Form();

                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }



        }

        private void Init_Form()
        {


            lblLocation.Text = Session["FacilityName"].ToString();
            IFacility ExposedInfantsFacManager;
            Double thePercent, theResultPercent;
            ExposedInfantsFacManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");

            theDS = ExposedInfantsFacManager.GetFacilityStatsExposedInfants(Convert.ToInt16(Session["Facility"].ToString()));

            ViewState["theDS"] = theDS;


            /****Cumulative Exposed Infants*****************************************************/
            lblExposedInfants.Text = theDS.Tables[0].Rows.Count.ToString();

            /****Current Total Exposed Infants*****************************************************/
            lblCurrentExposedInfants.Text = theDS.Tables[1].Rows.Count.ToString();

            /****Current PMTCT Infants*****************************************************/
            lblCurrentPMTCTInfants.Text = theDS.Tables[2].Rows.Count.ToString();

            /****Current HIV Care Infants*****************************************************/
            lblCurrentHIVCareInfants.Text = theDS.Tables[3].Rows.Count.ToString();

            /************************Age < 2 Months (PCR) *************************/
            Double thePCRLessthan2months = Convert.ToDouble(theDS.Tables[4].Rows.Count);
            lblPCRLessthan2months.Text = theDS.Tables[4].Rows.Count.ToString();

            /************************Age < 2 Months--Number/Percent Tested(PCR) *************************/
            Double thePercentTestedResult = Convert.ToDouble(theDS.Tables[5].Rows.Count);
            thePercent = (thePercentTestedResult / thePCRLessthan2months) * 100;
            if (thePCRLessthan2months != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblPercentTestedResult.Text = theDS.Tables[5].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+ *************************/
            Double theTotalHIVPos = Convert.ToDouble(theDS.Tables[6].Rows.Count);
            thePercent = (theTotalHIVPos / thePercentTestedResult) * 100;
            if (thePercentTestedResult != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblTotalHIVPos.Text = theDS.Tables[6].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV- *************************/
            Double theTotalHIVNeg = Convert.ToDouble(theDS.Tables[7].Rows.Count);
            thePercent = (theTotalHIVNeg / thePercentTestedResult) * 100;
            if (thePercentTestedResult != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblTotalHIVNeg.Text = theDS.Tables[7].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate *************************/
            Double theIndeterminateTested = Convert.ToDouble(theDS.Tables[8].Rows.Count);
            thePercent = (theIndeterminateTested / thePercentTestedResult) * 100;
            if (thePercentTestedResult != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblIndeterminateTested.Text = theDS.Tables[8].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(EBF)*************************/
            Double theHIVPosEBFlessthan2 = Convert.ToDouble(theDS.Tables[9].Rows.Count);
            thePercent = (theHIVPosEBFlessthan2 / theTotalHIVPos) * 100;
            if (theTotalHIVPos != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosEBFlessthan2.Text = theDS.Tables[9].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(EBMS)*************************/
            Double theHIVPosRFlessthan2 = Convert.ToDouble(theDS.Tables[10].Rows.Count);
            thePercent = (theHIVPosRFlessthan2 / theTotalHIVPos) * 100;
            if (theTotalHIVPos != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosRFlessthan2.Text = theDS.Tables[10].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(MF)*************************/
            Double theHIVPosMFlessthan2 = Convert.ToDouble(theDS.Tables[11].Rows.Count);
            thePercent = (theHIVPosMFlessthan2 / theTotalHIVPos) * 100;
            if (theTotalHIVPos != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosMFlessthan2.Text = theDS.Tables[11].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV+(Other)*************************/
            Double theHIVPosOtherlessthan2 = Convert.ToDouble(theDS.Tables[12].Rows.Count);
            thePercent = (theHIVPosOtherlessthan2 / theTotalHIVPos) * 100;
            if (theTotalHIVPos != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosOtherlessthan2.Text = theDS.Tables[12].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(EBF)*************************/
            Double theHIVNegEBFlessthan2 = Convert.ToDouble(theDS.Tables[13].Rows.Count);
            thePercent = (theHIVNegEBFlessthan2 / theTotalHIVNeg) * 100;
            if (theTotalHIVNeg != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegEBFlessthan2.Text = theDS.Tables[13].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(EBMS)*************************/
            Double theHIVNegRFlessthan2 = Convert.ToDouble(theDS.Tables[14].Rows.Count);
            thePercent = (theHIVNegRFlessthan2 / theTotalHIVNeg) * 100;
            if (theTotalHIVNeg != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegRFlessthan2.Text = theDS.Tables[14].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(MF)*************************/
            Double theHIVNegMFlessthan2 = Convert.ToDouble(theDS.Tables[15].Rows.Count);
            thePercent = (theHIVNegMFlessthan2 / theTotalHIVNeg) * 100;
            if (theTotalHIVNeg != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegMFlessthan2.Text = theDS.Tables[15].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--HIV-(Other)*************************/
            Double theHIVNegOtherlessthan2 = Convert.ToDouble(theDS.Tables[16].Rows.Count);
            thePercent = (theHIVNegOtherlessthan2 / theTotalHIVNeg) * 100;
            if (theTotalHIVNeg != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegOtherlessthan2.Text = theDS.Tables[16].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(EBF)*************************/
            Double theIndeterminateEBFlessthan2 = Convert.ToDouble(theDS.Tables[17].Rows.Count);
            thePercent = (theIndeterminateEBFlessthan2 / theIndeterminateTested) * 100;
            if (theIndeterminateTested != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateEBFlessthan2.Text = theDS.Tables[17].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(EBMS)*************************/
            Double theIndeterminateRFlessthan2 = Convert.ToDouble(theDS.Tables[18].Rows.Count);
            thePercent = (theIndeterminateRFlessthan2 / theIndeterminateTested) * 100;
            if (theIndeterminateTested != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateRFlessthan2.Text = theDS.Tables[18].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(RF)*************************/
            Double theIndeterminateMFlessthan2 = Convert.ToDouble(theDS.Tables[19].Rows.Count);
            thePercent = (theIndeterminateMFlessthan2 / theIndeterminateTested) * 100;
            if (theIndeterminateTested != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateMFlessthan2.Text = theDS.Tables[19].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age < 2 Months --Number/Percent Tested (PCR)--Indeterminate(Other)*************************/
            Double theIndeterminateOtherlessthan2 = Convert.ToDouble(theDS.Tables[20].Rows.Count);
            thePercent = (theIndeterminateOtherlessthan2 / theIndeterminateTested) * 100;
            if (theIndeterminateTested != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateOtherlessthan2.Text = theDS.Tables[20].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age < 12 Months (PCR) *************************/
            Double thePCR2to12months = Convert.ToDouble(theDS.Tables[21].Rows.Count);
            lblPCR2to12months.Text = theDS.Tables[21].Rows.Count.ToString();

            /************************Age >= 2 and Age < 12 Months--Number/Percent Tested(PCR) *************************/
            Double thePercentTested2to12PCR = Convert.ToDouble(theDS.Tables[22].Rows.Count);
            thePercent = (thePercentTested2to12PCR / thePCR2to12months) * 100;
            if (thePCR2to12months != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblPercentTested2to12PCR.Text = theDS.Tables[22].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age < 12 Months --Number/Percent Tested (PCR)--HIV+ *************************/
            Double theHIVPos2to12 = Convert.ToDouble(theDS.Tables[23].Rows.Count);
            thePercent = (theHIVPos2to12 / thePercentTested2to12PCR) * 100;
            if (thePercentTested2to12PCR != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblTotalHIVPos2to12.Text = theDS.Tables[23].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age < 12 Months --Number/Percent Tested (PCR)--HIV- *************************/
            Double theTotalHIVNeg2to12 = Convert.ToDouble(theDS.Tables[24].Rows.Count);
            thePercent = (theTotalHIVNeg2to12 / thePercentTested2to12PCR) * 100;
            if (thePercentTested2to12PCR != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblTotalHIVNeg2to12.Text = theDS.Tables[24].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate *************************/
            Double theIndeterminateTested2to12 = Convert.ToDouble(theDS.Tables[25].Rows.Count);
            thePercent = (theIndeterminateTested2to12 / thePercentTested2to12PCR) * 100;
            if (thePercentTested2to12PCR != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblIndeterminateTested2to12.Text = theDS.Tables[25].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(EBF)*************************/
            Double theHIVPosEBF2to12 = Convert.ToDouble(theDS.Tables[26].Rows.Count);
            thePercent = (theHIVPosEBF2to12 / theHIVPos2to12) * 100;
            if (theHIVPos2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosEBF2to12.Text = theDS.Tables[26].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(EBMS)*************************/
            Double theHIVPosRF2to12 = Convert.ToDouble(theDS.Tables[27].Rows.Count);
            thePercent = (theHIVPosRF2to12 / theHIVPos2to12) * 100;
            if (theHIVPos2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosRF2to12.Text = theDS.Tables[27].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(MF)*************************/
            Double theHIVPosMF2to12 = Convert.ToDouble(theDS.Tables[28].Rows.Count);
            thePercent = (theHIVPosMF2to12 / theHIVPos2to12) * 100;
            if (theHIVPos2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosMF2to12.Text = theDS.Tables[28].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV+(Other)*************************/
            Double theHIVPosOther2to12 = Convert.ToDouble(theDS.Tables[29].Rows.Count);
            thePercent = (theHIVPosOther2to12 / theHIVPos2to12) * 100;
            if (theHIVPos2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosOther2to12.Text = theDS.Tables[29].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months  --Number/Percent Tested (PCR)--HIV-(EBF)*************************/
            Double theHIVNegEBF2to12 = Convert.ToDouble(theDS.Tables[30].Rows.Count);
            thePercent = (theHIVNegEBF2to12 / theTotalHIVNeg2to12) * 100;
            if (theTotalHIVNeg2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegEBF2to12.Text = theDS.Tables[30].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months  --Number/Percent Tested (PCR)--HIV-(EBMS)*************************/
            Double theHIVNegRF2to12 = Convert.ToDouble(theDS.Tables[31].Rows.Count);
            thePercent = (theHIVNegRF2to12 / theTotalHIVNeg2to12) * 100;
            if (theTotalHIVNeg2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegRF2to12.Text = theDS.Tables[31].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months  --Number/Percent Tested (PCR)--HIV-(MF)*************************/
            Double theHIVNegMF2to12 = Convert.ToDouble(theDS.Tables[32].Rows.Count);
            thePercent = (theHIVNegMF2to12 / theTotalHIVNeg2to12) * 100;
            if (theTotalHIVNeg2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegMF2to12.Text = theDS.Tables[32].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--HIV-(Other)*************************/
            Double theHIVNegOther2to12 = Convert.ToDouble(theDS.Tables[33].Rows.Count);
            thePercent = (theHIVNegOther2to12 / theTotalHIVNeg2to12) * 100;
            if (theTotalHIVNeg2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegOther2to12.Text = theDS.Tables[33].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(EBF)*************************/
            Double theIndeterminateEBF2to12 = Convert.ToDouble(theDS.Tables[34].Rows.Count);
            thePercent = (theIndeterminateEBF2to12 / theIndeterminateTested2to12) * 100;
            if (theIndeterminateTested2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateEBF2to12.Text = theDS.Tables[34].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(EBMS)*************************/
            Double theIndeterminateRF2to12 = Convert.ToDouble(theDS.Tables[35].Rows.Count);
            thePercent = (theIndeterminateRF2to12 / theIndeterminateTested2to12) * 100;
            if (theIndeterminateTested2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateRF2to12.Text = theDS.Tables[35].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(MF)*************************/
            Double theIndeterminateMF2to12 = Convert.ToDouble(theDS.Tables[36].Rows.Count);
            thePercent = (theIndeterminateMF2to12 / theIndeterminateTested2to12) * 100;
            if (theIndeterminateTested2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateMF2to12.Text = theDS.Tables[36].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 2 and Age <12 Months --Number/Percent Tested (PCR)--Indeterminate(Other)*************************/
            Double theIndeterminateOther2to12 = Convert.ToDouble(theDS.Tables[37].Rows.Count);
            thePercent = (theIndeterminateOther2to12 / theIndeterminateTested2to12) * 100;
            if (theIndeterminateTested2to12 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateOther2to12.Text = theDS.Tables[37].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months (PCR) *************************/
            Double the18to24MonthPCR = Convert.ToDouble(theDS.Tables[38].Rows.Count);
            lbl18to24RConfirm.Text = theDS.Tables[38].Rows.Count.ToString();

            /************************Age < 2 Months--Number/Percent Tested(PCR) *************************/
            Double thePercentTested18to24months = Convert.ToDouble(theDS.Tables[39].Rows.Count);
            thePercent = (thePercentTested18to24months / the18to24MonthPCR) * 100;
            if (the18to24MonthPCR != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblPercentTested18to24months.Text = theDS.Tables[39].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+ *************************/
            Double theTotalHIVPos18to24 = Convert.ToDouble(theDS.Tables[40].Rows.Count);
            thePercent = (theHIVPos2to12 / thePercentTested18to24months) * 100;
            if (thePercentTested18to24months != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblTotalHIVPos18to24.Text = theDS.Tables[40].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV- *************************/
            Double theTotalHIVNeg18to24 = Convert.ToDouble(theDS.Tables[41].Rows.Count);
            thePercent = (theTotalHIVNeg18to24 / thePercentTested18to24months) * 100;
            if (thePercentTested18to24months != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblTotalHIVNeg18to24.Text = theDS.Tables[41].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months Months --Number/Percent Tested (PCR)--Indeterminate *************************/
            Double theIndeterminateTested18to24 = Convert.ToDouble(theDS.Tables[42].Rows.Count);
            thePercent = (theIndeterminateTested18to24 / thePercentTested18to24months) * 100;
            if (thePercentTested18to24months != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lblIndeterminateTested18to24.Text = theDS.Tables[42].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(EBF)*************************/
            Double theHIVPosEBF18to24 = Convert.ToDouble(theDS.Tables[43].Rows.Count);
            thePercent = (theHIVPosEBF18to24 / theTotalHIVPos18to24) * 100;
            if (theTotalHIVPos18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosEBF18to24.Text = theDS.Tables[43].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(EBMS)*************************/
            Double theHIVPosRF18to24 = Convert.ToDouble(theDS.Tables[44].Rows.Count);
            thePercent = (theHIVPosRF18to24 / theTotalHIVPos18to24) * 100;
            if (theTotalHIVPos18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosRF18to24.Text = theDS.Tables[44].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(MF)*************************/
            Double theHIVPosMF18to24 = Convert.ToDouble(theDS.Tables[45].Rows.Count);
            thePercent = (theHIVPosMF18to24 / theTotalHIVPos18to24) * 100;
            if (theTotalHIVPos18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosMF18to24.Text = theDS.Tables[45].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV+(Other)*************************/
            Double theHIVPosOther18to24 = Convert.ToDouble(theDS.Tables[46].Rows.Count);
            thePercent = (theHIVPosOther18to24 / theTotalHIVPos18to24) * 100;
            if (theTotalHIVPos18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVPosOther18to24.Text = theDS.Tables[46].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months --Number/Percent Tested (PCR)--HIV-(EBF)*************************/
            Double theHIVNegEBF18to24 = Convert.ToDouble(theDS.Tables[47].Rows.Count);
            thePercent = (theHIVNegEBF18to24 / theTotalHIVNeg18to24) * 100;
            if (theTotalHIVNeg18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegEBF18to24.Text = theDS.Tables[47].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--HIV-(EBMS)*************************/
            Double theHIVNegRF18to24 = Convert.ToDouble(theDS.Tables[48].Rows.Count);
            thePercent = (theHIVNegRF18to24 / theTotalHIVNeg18to24) * 100;
            if (theTotalHIVNeg18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegRF18to24.Text = theDS.Tables[48].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--HIV-(MF)*************************/
            Double theHIVNegMF18to24 = Convert.ToDouble(theDS.Tables[49].Rows.Count);
            thePercent = (theHIVNegMF18to24 / theTotalHIVNeg18to24) * 100;
            if (theTotalHIVNeg18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegMF18to24.Text = theDS.Tables[49].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--HIV-(Other)*************************/
            Double theHIVNegOther18to24 = Convert.ToDouble(theDS.Tables[50].Rows.Count);
            thePercent = (theHIVNegOther18to24 / theTotalHIVNeg18to24) * 100;
            if (theTotalHIVNeg18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkHIVNegOther18to24.Text = theDS.Tables[50].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(EBF)*************************/
            Double theIndeterminateEBF18to24 = Convert.ToDouble(theDS.Tables[51].Rows.Count);
            thePercent = (theIndeterminateEBF18to24 / theIndeterminateTested18to24) * 100;
            if (theIndeterminateTested18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateEBF18to24.Text = theDS.Tables[51].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(EBMS)*************************/
            Double theIndeterminateRF18to24 = Convert.ToDouble(theDS.Tables[52].Rows.Count);
            thePercent = (theIndeterminateRF2to12 / theIndeterminateRF18to24) * 100;
            if (theIndeterminateTested18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateRF18to24.Text = theDS.Tables[52].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(MF)*************************/
            Double theIndeterminateMF18to24 = Convert.ToDouble(theDS.Tables[53].Rows.Count);
            thePercent = (theIndeterminateMF18to24 / theIndeterminateTested18to24) * 100;
            if (theIndeterminateTested18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateMF18to24.Text = theDS.Tables[53].Rows.Count + " " + "(" + theResultPercent + "%)";

            /************************Age >= 18 and Age < 24 Months--Number/Percent Tested (PCR)--Indeterminate(Other)*************************/
            Double theIndeterminateOther18to24 = Convert.ToDouble(theDS.Tables[54].Rows.Count);
            thePercent = (theIndeterminateOther18to24 / theIndeterminateTested18to24) * 100;
            if (theIndeterminateTested18to24 != 0)
            {
                theResultPercent = System.Math.Round(thePercent);
            }
            else
            {
                theResultPercent = 0;
            }
            lnkIndeterminateOther18to24.Text = theDS.Tables[54].Rows.Count + " " + "(" + theResultPercent + "%)";


            /************************Cumulative ARV Prophylaxis *************************/
            lblInfantsARVProphylaxis.Text = theDS.Tables[55].Rows.Count.ToString();

            /************************Current ARV Prophylaxis *************************/
            lblInfantsCurrentProphylaxis.Text = theDS.Tables[56].Rows.Count.ToString();

            /************************Cumulative ARV Treatment *************************/
            lblInfantsCumulativeARV.Text = theDS.Tables[57].Rows.Count.ToString();

            /************************Current ARV Treatment *************************/
            lblInfantsCurrentARV.Text = theDS.Tables[58].Rows.Count.ToString();

            /**********Cotrimoxizole Prophylaxis-Cumulative Started < 2 Months*************************/
            lblContrimProCumulessthan2.Text = theDS.Tables[59].Rows.Count.ToString();

            /*********Cotrimoxizole Prophylaxis-Current Started < 2 Months*************************/
            lblContrimProCurrentlessthan2.Text = theDS.Tables[60].Rows.Count.ToString();

            /********Cotrimoxizole Prophylaxis-Cumulative Started 2-24 Months*************************/
            lblContrimProCumu2to24.Text = theDS.Tables[61].Rows.Count.ToString();

            /*******Cotrimoxizole Prophylaxis-Current Started < 2-24 Months*************************/
            lblContrimProCurrent2to24.Text = theDS.Tables[62].Rows.Count.ToString();



        }
        protected void hlExposedInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[1], "CumulativeExposedInfants");
        }
        protected void hlCurrentExposedInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "CurrentExposedInfants");
        }
        protected void hlCurrentPMTCTInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "CurrentPMTCTInfants");
        }
        protected void hlCurrentHIVCareInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "CurrentHIVCareInfants");


        }
        protected void hlInfantsARVPro_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[55], "InfantsCumulativeARVProphylaxis");

        }
        protected void hlInfantsCurrentProphylaxis_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[56], "InfantsCurrentARVProphylaxis");
        }
        protected void hlInfantsCumulativeARV_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[57], "InfantsCumulativeARVTreatment");

        }
        protected void hlInfantsCurrentARV_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[58], "InfantsCurrentARVTreatment");

        }
        protected void hlContrimProCumulessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[59], "ContrimProCumulessthan2");

        }
        protected void hlContrimProCurrentlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[60], "ContrimProCurrentlessthan2");

        }
        protected void hlContrimProCumu2to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[61], "ContrimProCurrent2to24");

        }
        protected void hlContrimProCurrent2to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[62], "ContrimProCumu2to24");

        }
        protected void hlInfantsnotonContrim_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[63], "InfantsnotonContrim");

        }
        protected void hlHIVPosEBFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "AgeLessthan2HIVPosEBF");


        }
        protected void hlHIVPosRFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[10], "AgeLessthan2HIVPosRF");


        }
        protected void hlHIVPosMFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[11], "AgeLessthan2HIVPosMF");

        }
        protected void hlHIVPosOtherlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[12], "AgeLessthan2HIVPosOther");

        }
        protected void hlHIVNegEBFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[13], "AgeLessthan2HIVNegEBF");

        }
        protected void hlHIVNegRFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[14], "AgeLessthan2HIVNegRF");


        }
        protected void hlHIVNegMFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[15], "AgeLessthan2HIVNegMF");

        }
        protected void hlHIVNegOtherlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[16], "AgeLessthan2HIVNegOther");

        }
        protected void hlIndeterminateEBFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[17], "AgeLessthan2IndeterminateEBF");

        }
        protected void hlIndeterminateRFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[18], "AgeLessthan2IndeterminateRF");

        }
        protected void hlHIVPosEBF2to12_Click(object sender, EventArgs e)
        {

            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[26], "HIVPos2to12EBF");

        }
        protected void hlHIVPosRF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[27], "HIVPos2to12RF");


        }
        protected void hlHIVPosOther2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[29], "HIVPos2to12Other");

        }
        protected void hlHIVNegEBF2to12_Click(object sender, EventArgs e)
        {

            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[30], "HIVNeg2to12EBF");

        }
        protected void hlHIVNegRF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[31], "HIVNeg2to12RF");

        }
        protected void hlHIVNegMF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[32], "HIVNeg2to12MF");

        }
        protected void hlHIVNegOther2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[33], "HIVNeg2to12Other");


        }
        protected void hlIndeterminateEBF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[34], "Indeterminate2to12EBF");


        }
        protected void hlIndeterminateRF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[35], "Indeterminate2to12RF");


        }
        protected void hlIndeterminateMF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[36], "Indeterminate2to12MF");

        }
        protected void hlIndeterminateOther2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[37], "Indeterminate2to12Other");


        }
        protected void hlHIVPosEBF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[43], "HIVPos18to24EBF");

        }
        protected void hlHIVPosRF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[44], "HIVPos18to24RF");


        }
        protected void hlHIVPosMF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[45], "HIVPos18to24MF");


        }
        protected void hlHIVPosOther18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[46], "HIVPos18to24Other");

        }
        protected void hlHIVNegEBF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[47], "HIVNeg18to24EBF");


        }
        protected void hlHIVNegRF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[48], "HIVNeg18to24RF");

        }
        protected void hlHIVNegMF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[49], "HIVNeg18to24MF");

        }
        protected void hlHIVNegOther18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[50], "HIVNeg18to24Other");

        }
        protected void hlIndeterminateEBF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[51], "Indeterminate18to24EBF");

        }
        protected void hlIndeterminateMF18to24_Click(object sender, EventArgs e)
        {

            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[53], "Indeterminate18to24MF");

        }
        protected void hlIndeterminateOther18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[54], "Indeterminate18to24Other");

        }

        protected void hlIndeterminateMFlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[19], "AgeLessthan2IndeterminateMF");
        }
        protected void hlIndeterminateOtherlessthan2_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[20], "AgeLessthan2IndeterminateOther");

        }
        protected void hlHIVPosMF2to12_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[28], "HIVPos2to12MF");

        }
        protected void hlIndeterminateRF18to24_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[52], "Indeterminate18to24RF");
        }
    }
}