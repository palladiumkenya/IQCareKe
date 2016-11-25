<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PMTCT.aspx.cs" Inherits="IQCare.Web.Statistics.PMTCT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .style1
        {
            width: 312px;
        }
        .style2
        {
            width: 368px;
        }
        .style3
        {
            width: 273px;
        }
        .style5
        {
            width: 14px;
        }
        .style11
        {
            width: 121px;
        }
        .blue
        {
            text-align: center;
        }
        .style12
        {
            text-align: center;
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
            <table width="100%" style="background-color: #666699; border-width: 1%" cellpadding="3"
        cellspacing="0">
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
                    PMTCT Facility Statistics
                </h3>
            </td>
            <td class="blue" valign="top" align="left">
                <div id="facility">
                    <asp:Label ID="lblLocation" runat="server" Text="" Style="text-align: left"></asp:Label></div>
            </td>
        </tr>
    </table>
    <br />
    <table width="95%" cellpadding="0" cellspacing="0" style="border-right: #666699;
        border-top: #666699; border-bottom: #666699; height: 372px;" border="1">
       
        <tr>
            <td align="left" valign="middle" id="PMTCT_1" class="style1">
                <table class="bold" cellpadding="5" cellspacing="0" border="0" style="width: 91%;
                    height: 209px;">
                    <tr>
                        <td colspan="2" class="style16">
                            <asp:Label ID="lblPMTCTEnroll" runat="server" Font-Bold="True" Text="PMTCT Enrolled"
                                Style="text-align: left;" Width="329px"></asp:Label><br />
                            <td class="style17">
                            </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkMothersEverEnroll" runat="server" OnClick="hlMothersEverEnroll_Click">Cumulative Mothers Ever in PMTCT:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right">
                            <asp:Label ID="lblMothersEverEnroll" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkCurrentMothers" runat="server" OnClick="hlCurrentMothers_Click">Current Mothers in PMTCT:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right">
                            <asp:Label ID="lblCurrentMothers" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            &nbsp;
                        </td>
                        <td class="style5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style2">
                            <asp:Label ID="lblCurrentWomenonARVPro" runat="server" Font-Bold="True" Text="Current Number Women on ARV Prophylaxis"
                                Style="text-align: left"></asp:Label>
                            <td>
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" class="style3">
                            <asp:LinkButton ID="lnkProANC" runat="server" OnClick="hlProANC_Click">ANC:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right">
                            <asp:Label ID="lblProANC" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkProLD" runat="server" OnClick="hlProLD_Click">L & D:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right">
                            <asp:Label ID="lblProLD" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkProPN" runat="server" OnClick="hlProPN_Click">Post Natal:</asp:LinkButton>
                        </td>
                        <td class="blue" align="right">
                            <asp:Label ID="lblProPN" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="PMTCT_2" align="center" valign="top" style="height: 290px; border-right: solid 1px #666699;
                border-top: solid 1px #666699; border-bottom: solid 1px #666699;">
                <table class="bold" cellspacing="0" border="0" style="width: 90%; height: 384px;">
                    <tr>
                        <td colspan="5" align="left" valign="top" style="text-align: center">
                            <asp:Label ID="lblHIVStatusDisCouple" runat="server" Font-Bold="True" Text="HIV Status and Discordant Couples"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td align="center" style="width: 20%; height: 25px">
                            <asp:Label ID="lblPartners" runat="server" Text="Partners"></asp:Label>
                        </td>
                        <td style="width: 20%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                        <td align="left" style="width: 20%; height: 25px">
                            <asp:Label ID="lblHIVPos" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%; height: 25px">
                            <asp:Label ID="lblHIVNeg" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%; height: 25px">
                            <asp:Label ID="lblUnknown" runat="server" Text="Unknown"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblmothers" runat="server" Text="Mothers"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblCurrentANC" runat="server" Text="Current ANC"></asp:Label>
                        </td>
                        <td class="blue" align="left" class="style12">
                            <asp:Label ID="lblCurrentANCMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblCurrentANCHIVPosMothers" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td class="blue" align="left" class="style12">
                            <asp:Label ID="lblANCHIVPosMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkPosMotherPosPartner" runat="server" OnClick="hlPosMotherPosPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkPosMotherNegPartner" runat="server" OnClick="hlPosMotherNegPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:LinkButton ID="lnkPosMotherUnknownPartner" runat="server" OnClick="hlPosMotherUnknownPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblCurrentHIVNegMothers" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td class="blue" align="left" class="style12">
                            <asp:Label ID="lblHIVNegMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkNegMotherPosPartner" runat="server" OnClick="hlNegMotherPosPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkNegMotherNegPartner" runat="server" OnClick="hlNegMotherNegPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:LinkButton ID="lnkNegMotherUnknownPartner" runat="server" OnClick="hlNegMotherUnknownPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <%-- </table><br />
                                               
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">--%>
                    <tr>
                        <td colspan="5" class="style12">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblCurrentLD" runat="server" Text="Current L&D"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblCurrentLDMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="3" class="style12">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblLDHIVPos" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblLDHIVPosMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkLDPosMotherPosPartner" runat="server" OnClick="hlLDPosMotherPosPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkLDPosMotherNegPartner" runat="server" OnClick="hlLDPosMotherNegPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:LinkButton ID="lnkLDPosMotherUnknownPartner" runat="server" OnClick="hlLDPosMotherUnknownPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblLDHIVNeg" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblLDHIVNegMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkLDNegMotherPosPartner" runat="server" OnClick="hlLDNegMotherPosPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkLDNegMotherNegPartner" runat="server" OnClick="hlLDNegMotherNegPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:LinkButton ID="lnkLDNegMotherUnknownPartner" runat="server" OnClick="hlLDNegMotherUnknownPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <%-- </table>
                        
                        <table class="bold" cellspacing="0" border="0" style="width: 90%">--%>
                    <tr>
                        <td colspan="5" class="style12">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblCurrentPN" runat="server" Text="Current Post Natal"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblCurrentPNMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td colspan="3" class="style12">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblPNHIVPos" runat="server" Text="HIV+"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblPNHIVPosMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkPNPosMotherPosPartner" runat="server" OnClick="hlPNPosMotherPosPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkPNPosMotherNegPartner" runat="server" OnClick="hlPNPosMotherNegPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:LinkButton ID="lnkPNPosMotherUnknownPartner" runat="server" OnClick="hlPNPosMotherUnknownPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style11">
                            <asp:Label ID="lblPNHIVNeg" runat="server" Text="HIV-"></asp:Label>
                        </td>
                        <td align="left" class="blue">
                            <asp:Label ID="lblPNHIVNegMothers" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkPNNegMotherPosPartner" runat="server" OnClick="hlPNNegMotherPosPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td style="width: 20%" class="style12">
                            <asp:LinkButton ID="lnkPNNegMotherNegPartner" runat="server" OnClick="hlPNNegMotherNegPartner_Click"
                                Text="0"></asp:LinkButton>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:LinkButton ID="lnkPNNegMotherUnknownPartner" runat="server" OnClick="hlPNNegMotherUnknownPartner_Click"
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
