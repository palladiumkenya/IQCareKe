<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HEI.aspx.cs" Inherits="IQCare.Web.Statistics.HEI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 285px;
        }
        .style10
        {
            text-align: center;
            height: 43px;
        }
        .style13
        {
            width: 586px;
            height: 23px;
        }
        .style16
        {
            width: 10%;
            height: 28px;
        }
        .style19
        {
            height: 31px;
        }
        .style22
        {
            width: 105px;
            height: 28px;
        }
        .style25
        {
            width: 105px;
            height: 31px;
        }
        .style26
        {
            height: 30px;
        }
        .style28
        {
            height: 15px;
            width: 83px;
        }
        .style31
        {
            height: 25px;
            width: 68px;
        }
        .style35
        {
            height: 15px;
        }
        .style41
        {
            width: 15%;
            height: 27px;
        }
        .style48
        {
            height: 25px;
            width: 83px;
        }
        .style50
        {
            height: 25px;
            width: 171px;
        }
        .style53
        {
            height: 15px;
            width: 171px;
        }
        .style54
        {
            height: 31px;
            width: 171px;
        }
        .style56
        {
            height: 27px;
            width: 171px;
        }
        .style58
        {
            height: 27px;
            width: 83px;
        }
        .style60
        {
            height: 27px;
            width: 68px;
        }
        .style66
        {
            width: 15%;
            height: 20px;
        }
        .style72
        {
            width: 15%;
            height: 21px;
        }
        .style73
        {
            height: 33px;
            width: 171px;
        }
        .style74
        {
            height: 33px;
            width: 105px;
        }
        .style75
        {
            height: 33px;
        }
        .style76
        {
            height: 20px;
            width: 171px;
        }
        .style78
        {
            height: 20px;
            width: 83px;
        }
        .style80
        {
            height: 20px;
            width: 68px;
        }
        .style81
        {
            height: 21px;
            width: 171px;
        }
        .style83
        {
            height: 21px;
            width: 83px;
        }
        .style85
        {
            height: 21px;
            width: 68px;
        }
        .style86
        {
            width: 368px;
            height: 56px;
        }
        .style87
        {
            width: 368px;
            height: 41px;
        }
        .style88
        {
            width: 87%;
        }
        .style93
        {
            height: 25px;
            width: 64px;
        }
        .style94
        {
            height: 21px;
            width: 64px;
        }
        .style95
        {
            height: 20px;
            width: 64px;
        }
        .style96
        {
            height: 27px;
            width: 64px;
        }
        .style97
        {
            height: 25px;
            width: 105px;
        }
        .style98
        {
            height: 21px;
            width: 105px;
        }
        .style99
        {
            height: 20px;
            width: 105px;
        }
        .style100
        {
            height: 27px;
            width: 105px;
        }
        .style101
        {
            width: 87%;
            height: 39px;
        }
        .style102
        {
            height: 39px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../incl/menu.js"></script>
    <script language="javascript" type="text/javascript" src="../Incl/IQCareScript.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/highlightLabels.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/dateformat.js"></script>
    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1);
        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
        var browserName = navigator.appName;
        if (browserName != "Microsoft Internet Explorer") {

            document.write('<link rel="stylesheet" type="text/css" href="../style/StyleSheetBrowser.css" />');

        }
        else {
            document.write('<link rel="stylesheet" type="text/css" href="../style/styles.css" />');
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" style="background-color: #666699; border-width: 1%; height: 1px;"
        cellpadding="4" cellspacing="0">
        <tr>
            <td valign="top" style="color: White">
                IQCare Facility Statistics
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="8" cellspacing="0">
        <tr>
            <td valign="top" class="style13">
                <img id="img1" alt="International Quality Care by Palladium" src="../Images/iq_logo.gif"
                    border="0" />
            </td>
        </tr>
        <tr>
            <td class="style13" valign="top">
                <h3>
                    Exposed Infants</h3>
            </td>
            <td class="blue" valign="top" align="left">
                <div id="facility">
                    <asp:Label ID="lblLocation" runat="server" Text="" Style="text-align: left"></asp:Label></div>
            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="border-right: #666699; border-top: #666699;
        border-bottom: #666699; height: 547px; width: 833px; margin-left: 26px;" border="1">
        <tr>
            <td align="left" valign="top" id="PMTCT_1" class="style1">
                <table class="bold" cellpadding="2" cellspacing="0" border="0" style="width: 88%;
                    height: 568px; margin-top: 0px; margin-left: 22px;">
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkExposedInfants" runat="server" OnClick="hlExposedInfants_Click">Cumulative Exposed Infants:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblExposedInfants" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkCurrentExposedInfants" runat="server" OnClick="hlCurrentExposedInfants_Click">Current Total Exposed Infants:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblCurrentExposedInfants" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkCurrentPMTCTInfants" runat="server" OnClick="hlCurrentPMTCTInfants_Click">Current PMTCT Infants:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblCurrentPMTCTInfants" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkCurrentHIVCareInfants" runat="server" OnClick="hlCurrentHIVCareInfants_Click">Current HIV Care Infants:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblCurrentHIVCareInfants" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <br />
                    <tr>
                        <td colspan="2" align="Center" class="style86" valign="middle">
                            <asp:Label ID="lblProandTreatment" runat="server" Font-Bold="True" Text="ARV Prophylaxis and Treatment"
                                Style="text-align: center"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkInfantsARVPro" runat="server" OnClick="hlInfantsARVPro_Click">Cumulative ARV Prophylaxis:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblInfantsARVProphylaxis" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkInfantsCurrentProphy" runat="server" OnClick="hlInfantsCurrentProphylaxis_Click">Current ARV Prophylaxis:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblInfantsCurrentProphylaxis" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkInfantsCumulativeARV" runat="server" OnClick="hlInfantsCumulativeARV_Click">Cumulative ARV Treatment:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblInfantsCumulativeARV" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkInfantsCurrentARV" runat="server" OnClick="hlInfantsCurrentARV_Click">Current ARV Treatment:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblInfantsCurrentARV" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <br />
                    <tr>
                        <td colspan="2" align="Center" valign="middle" class="style87">
                            <asp:Label ID="lblCotriProphy" runat="server" Font-Bold="True" Text="Cotrimoxizole Prophylaxis"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkContrimProCumulessthan2" runat="server" OnClick="hlContrimProCumulessthan2_Click">Cumulative 
                               Started &lt; 2 Months :</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblContrimProCumulessthan2" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkContrimProCurrentlessthan2" runat="server" OnClick="hlContrimProCurrentlessthan2_Click">Current Started < 2 Months :</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblContrimProCurrentlessthan2" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style88">
                            <asp:LinkButton ID="lnkContrimProCumu2to24" runat="server" OnClick="hlContrimProCumu2to24_Click">Cumulative 2-24 Months :</asp:LinkButton>
                        </td>
                        <td class="blue" align="right" style="height: 25px">
                            <asp:Label ID="lblContrimProCumu2to24" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style101">
                            <asp:LinkButton ID="lnkContrimProCurrent2to24" runat="server" OnClick="hlContrimProCurrent2to24_Click">Current 2-24 Months :</asp:LinkButton>
                        </td>
                        <td class="blue" align="right">
                            <asp:Label ID="lblContrimProCurrent2to24" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <br />
                        <td colspan="2" class="style26" valign="bottom">
                            <asp:LinkButton ID="lnkInfantsnotonContrim" runat="server" OnClick="hlInfantsnotonContrim_Click">Exposed Infants Not Yet on Cotrim:</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="PMTCT_2" align="center" valign="top" style="border-right: solid 1px #666699;
                border-top: solid 1px #666699; border-bottom: solid 1px #666699;">
                <table class="bold" cellspacing="0" border="0" cellpadding="2" style="margin-left: 0px;
                    height: 601px; width: 495px;">
                    <tr>
                        <td colspan="6" align="center" valign="top" class="style10">
                            <asp:Label ID="lblHIVStatusandfeedingoption" runat="server" Font-Bold="True" Text="HIV Status and Feeding Options"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style50">
                            <br />
                            <br />
                        </td>
                        <td class="style97">
                        </td>
                        <td align="left" class="style28">
                            <asp:Label ID="lblEBF" runat="server" Text="EBF"></asp:Label>
                        </td>
                        <td align="left" class="style93">
                            <asp:Label ID="lblRF" runat="server" Text="RF"></asp:Label>
                        </td>
                        <td align="left" class="style31">
                            <asp:Label ID="lblMF" runat="server" Text="MF"></asp:Label>
                        </td>
                        <td align="left" style="width: 2%">
                            <asp:Label ID="lblOther" runat="server" Text="Other"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style53">
                            <asp:Label ID="lblPCRlessthan2" runat="server" Text="Age < 2 Months(PCR)"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblPCRLessthan2months" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="4" class="style16">
                        </td>
                    </tr>
                    <tr>
                        <td class="style73">
                            <asp:Label ID="lblPercentTested" runat="server" Text="Percent Tested"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblPercentTestedResult" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="4" class="style75">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style81">
                            <asp:Label ID="lblTestedResultHIVPos" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblTotalHIVPos" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="style83">
                            <asp:LinkButton ID="lnkHIVPosEBFlessthan2" runat="server" OnClick="hlHIVPosEBFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosRFlessthan2" runat="server" OnClick="hlHIVPosRFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosMFlessthan2" runat="server" OnClick="hlHIVPosMFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosOtherlessthan2" runat="server" OnClick="hlHIVPosOtherlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style76">
                            <asp:Label ID="lblPercentTestedHIVNeg" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblTotalHIVNeg" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegEBFlessthan2" runat="server" OnClick="hlHIVNegEBFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegRFlessthan2" runat="server" OnClick="hlHIVNegRFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegMFlessthan2" runat="server" OnClick="hlHIVNegMFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegOtherlessthan2" runat="server" OnClick="hlHIVNegOtherlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style56">
                            <asp:Label ID="lblIndeterminate" runat="server" Text="Indeterminate"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblIndeterminateTested" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateEBFlessthan2" runat="server" OnClick="hlIndeterminateEBFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateRFlessthan2" runat="server" OnClick="hlIndeterminateRFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateMFlessthan2" runat="server" OnClick="hlIndeterminateMFlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateOtherlessthan2" runat="server" OnClick="hlIndeterminateOtherlessthan2_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="style26">
                        </td>
                    </tr>
                    <tr>
                        <td class="style54">
                            <asp:Label ID="lblPCR2to12" runat="server" Text="Age 2-12 Months(PCR)"></asp:Label>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:Label ID="lblPCR2to12months" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="4" class="style19">
                        </td>
                    </tr>
                    <tr>
                        <td class="style50">
                            <asp:Label ID="lblPercentTested2to12" runat="server" Text="Percent Tested"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblPercentTested2to12PCR" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style50">
                            <asp:Label ID="lblHIVPos2to12" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblTotalHIVPos2to12" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosEBF2to12" runat="server" OnClick="hlHIVPosEBF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosRF2to12" runat="server" OnClick="hlHIVPosRF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosMF2to12" runat="server" OnClick="hlHIVPosMF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:LinkButton ID="lnkHIVPosOther2to12" runat="server" OnClick="hlHIVPosOther2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style50">
                            <asp:Label ID="lblHIVNeg2to12" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblTotalHIVNeg2to12" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegEBF2to12" runat="server" OnClick="hlHIVNegEBF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegRF2to12" runat="server" OnClick="hlHIVNegRF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegMF2to12" runat="server" OnClick="hlHIVNegMF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:LinkButton ID="lnkHIVNegOther2to12" runat="server" OnClick="hlHIVNegOther2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="blue">
                            <asp:Label ID="lblIndeterminate2to12" runat="server" Text="Indeterminate"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblIndeterminateTested2to12" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateEBF2to12" runat="server" OnClick="hlIndeterminateEBF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateRF2to12" runat="server" OnClick="hlIndeterminateRF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateMF2to12" runat="server" OnClick="hlIndeterminateMF2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:LinkButton ID="lnkIndeterminateOther2to12" runat="server" OnClick="hlIndeterminateOther2to12_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="style35">
                        </td>
                    </tr>
                    <tr>
                        <td class="style50">
                            <asp:Label ID="lbl18to24" runat="server" Text="Age 18-24 Months <br/>(Rapid Confirmatory)"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lbl18to24RConfirm" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td class="style50">
                            <asp:Label ID="lblPercentTested18to24" runat="server" Text="Percent Tested"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblPercentTested18to24months" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style50">
                            <asp:Label ID="lblHIVPos18to24" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblTotalHIVPos18to24" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosEBF18to24" runat="server" OnClick="hlHIVPosEBF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosRF18to24" runat="server" OnClick="hlHIVPosRF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVPosMF18to24" runat="server" OnClick="hlHIVPosMF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:LinkButton ID="lnkHIVPosOther18to24" runat="server" OnClick="hlHIVPosOther18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style50">
                            <asp:Label ID="lblHIVNeg18to24" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblTotalHIVNeg18to24" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="style48">
                            <asp:LinkButton ID="lnkHIVNegEBF18to24" runat="server" OnClick="hlHIVNegEBF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegRF18to24" runat="server" OnClick="hlHIVNegRF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkHIVNegMF18to24" runat="server" OnClick="hlHIVNegMF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:LinkButton ID="lnkHIVNegOther18to24" runat="server" OnClick="hlHIVNegOther18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style50">
                            <asp:Label ID="lblIndeterminate18to24" runat="server" Text="Indeterminate"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblIndeterminateTested18to24" runat="server" Text="0"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateEBF18to24" runat="server" OnClick="hlIndeterminateEBF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateRF18to24" runat="server" OnClick="hlIndeterminateRF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue">
                            <asp:LinkButton ID="lnkIndeterminateMF18to24" runat="server" OnClick="hlIndeterminateMF18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" class="blue" style="width: 15%; height: 25px">
                            <asp:LinkButton ID="lnkIndeterminateOther18to24" runat="server" OnClick="hlIndeterminateOther18to24_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
