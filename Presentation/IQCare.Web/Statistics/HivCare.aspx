<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HivCare.aspx.cs" Inherits="IQCare.Web.Statistics.HivCare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
        .style2
        {
            text-align: center;
        }
        .blue
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

            document.write('<link rel="stylesheet" type="text/css" href="./style/StyleSheetBrowser.css" />');

        }
        else {
            document.write('<link rel="stylesheet" type="text/css" href="./style/styles.css" />');
        }


    </script>
    <script language="javascript" type="text/javascript">
        function GetMaleFemaleEnrolledPieChartinfo() {
            var noofmale, nooffemale;

            noofmale = '<%= noofmale %>';
            nooffemale = '<%= nooffemale %>';
            return "Male#" + noofmale + "~" + "Female#" + nooffemale;

        }
        function GetArt_NorartPieChartinfo() {
            var noofArt, noofNonArt;
            noofArt = '<%=noofart%>';
            noofNonArt = '<%=noofNonart%>';
            return "Art#" + noofArt + "~" + "NonArt#" + noofNonArt;

        }
        function GetArtByAgeChartinfo() {
            var noofArtupto2, noofArtupto4, noofArtupto15, noofArtabove15;

            noofArtupto2 = '<%= noofArtupto2 %>';
            noofArtupto4 = '<%= noofArtupto4 %>';
            noofArtupto15 = '<%= noofArtupto15 %>';
            noofArtabove15 = '<%= noofArtabove15 %>'
            return "0-1#" + noofArtupto2 + "~" + "2-4#" + noofArtupto4 + "~" + "5-14#" + noofArtupto15 + "~" + "above 15#" + noofArtabove15;

        }

        function ErrorMessage(errMsg) {

            window.alert(errMsg);
        }

    </script>
    <title>HIV Care Statistics</title>
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
                <img id="img1" alt="International Quality Care by AIDSRelief" src="../Images/iq_logo.gif"
                    border="0"/>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <h2>
                    HIV Care Facility Statistics
                </h2>
            </td>
            <td class="blue" valign="top" align="left">
                <div id="facility">
                    <asp:Label ID="lblLocation" runat="server" Text="" Style="text-align: left"></asp:Label></div>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
        <tr>
            <td style="width: 33%;margin-top:5px" valign="top">
                <table border="0" style="width: 100%; margin-left: 0px;">
                    <tr>
                        <td style="padding-top: 18px;margin-top:2px">
                            <asp:LinkButton ID="lnkEverEnrolledPatients" runat="server" OnClick="hlEverEnrolledPatients_Click">Ever 
                 Enrolled Patients:</asp:LinkButton>
                        </td>
                        <td align="right" style="padding-top: 18px;margin-top:2px">
                            <asp:Label ID="lblTotalPatient" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkFemalesEnrolled" runat="server" OnClick="hlFemalesEnrolled_Click">Females 
                 Enrolled:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px; padding-top: 18px">
                            <asp:Label ID="lblFemalePatient" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkMalesEnrolled" runat="server" OnClick="hlMalesEnrolled_Click">Males 
                 Enrolled:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px; padding-top: 18px;">
                            <asp:Label ID="lblMalePatient" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkTotalActivePatients" runat="server" OnClick="hlTotalActivePatients_Click">Total Active Patients:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px; padding-top: 18px;">
                            <asp:Label ID="lblTotalActivePatients" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkActiveNonARTPatients" runat="server" OnClick="hlActiveNonARTPatients_Click"> 
                 Active Non-ART Patients:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px; padding-top: 18px;">
                            <asp:Label ID="lblActiveNonARTPatients" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkActiveARTPatients" runat="server" OnClick="hlActiveARTPatients_Click">Active ART Patients </asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px; padding-top: 18px;">
                            <asp:Label ID="lblActiveARTPatients" runat="server" Text="0"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkARTMortality" runat="server" OnClick="hlARTMortality_Click">ART Mortality:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px; padding-top: 18px;">
                            <asp:Label ID="lblARTMortality" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkLosttoFollowUp" runat="server" OnClick="hlLosttoFollowUp_Click">Lost to Follow up Patient list</asp:LinkButton>
                        </td>
                        <td class="style7">
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" style="padding-top: 18px;">
                            <asp:LinkButton ID="lnkDueforTermination" runat="server" OnClick="hlDueforTermination_Click"
                                Style="text-align: left">Due for Termination List:</asp:LinkButton>
                        </td>
                        <td class="style7">
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 33%;" valign="top">
                <table cellpadding="8" cellspacing="2" border="0" style="width: 100%; margin-left: 0px;">
                    <tr>
                        <br />
                        <td colspan="2" valign="top" align="center" class="style14">
                            <asp:Label ID="lblbd" runat="server" Font-Bold="True" Text="Non-ART Patient Breakdown by Age and Sex"></asp:Label>
                            <br />
                            <asp:Label ID="lblbdm" runat="server" Font-Bold="True" Text="Males"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:LinkButton ID="lnkNonARTMUpto2" runat="server" OnClick="hlNonARTMUpto2_Click">0-1 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTMUpto2" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:LinkButton ID="lnkNonARTMUpto4" runat="server" OnClick="hlNonARTMUpto4_Click">2-4 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTMUpto4" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkNonARTMUpto14" runat="server" OnClick="hlNonARTMUpto14_Click">5-14 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTMUpto14" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkNonARTMAbove15" runat="server" OnClick="hlNonARTMAbove15_Click">15+ 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTMAbove15" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblFemale" runat="server" Font-Bold="True" Text="Females"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkNonARTFupto2" runat="server" OnClick="hlNonARTFUpto2_Click">0-1 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTFupto2" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkNonARTFupto4" runat="server" OnClick="hlNonARTFupto2_Click">2-4 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTFupto4" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkNonARTFUpto14" runat="server" OnClick="hlNonARTFUpto14_Click">5-14 
                            Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblNonARTFUpto14" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:LinkButton ID="lnkNonARTFAbove15" runat="server" OnClick="hlNonARTFAbove15_Click">15+ 
                            Years:</asp:LinkButton>
                            <td align="right" style="width: 30%; height: 25px">
                                <asp:Label ID="lblNonARTFAbove15" runat="server" Text="0"></asp:Label>
                            </td>
                    </tr>
                </table>
            </td>
            <td class="style4" valign="top">
                <table cellpadding="8" cellspacing="2" border="0" style="width: 101%; margin-left: 0px;">
                    <tr>
                        <br />
                        <td colspan="2" valign="top" align="center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="ART Patient Breakdown by Age and Sex"></asp:Label>
                            <br />
                            <asp:Label ID="lblARTMales" runat="server" Font-Bold="True" Text="Males"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTMUpto2" runat="server" OnClick="hlARTMUpto2_Click">0-1 Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTMUpto2" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTMUpto4" runat="server" OnClick="hlARTMUpto4_Click">2-4 Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTMUpto4" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTMUpto14" runat="server" OnClick="hlARTMUpto14_Click">5-14 Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTMUpto14" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTMAbove15" runat="server" OnClick="hlARTMAbove15_Click">15+ Years:</asp:LinkButton>
                            <td align="right" style="width: 30%; height: 25px">
                                <asp:Label ID="lblARTMAbove15" runat="server" Text="0"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style1" align="center">
                            <asp:Label ID="lblARTFemales" runat="server" Font-Bold="True" Text="Females"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTFUpto2" runat="server" OnClick="hlARTFUpto2_Click">0-1 Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTFUpto2" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTFUpto4" runat="server" OnClick="hlARTFUpto4_Click">2-4 Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTFUpto4" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTFUpto14" runat="server" OnClick="hlARTFUpto14_Click">5-14 Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTFUpto14" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:LinkButton ID="lnkARTFAbove15" runat="server" OnClick="hlARTFAbove15_Click">15+ Years:</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 30%; height: 25px">
                            <asp:Label ID="lblARTFAbove15" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
        <tr>
            <td colspan="3">
                <div id="divARTGraph" style="width: 100%" runat="server">
                    <table width="100%" height="100px">
                        <tbody>
                            <tr>
                                <td class="pad18" align="center" valign="middle" style="height: 100px; width: 33%;
                                    border-top: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid">
                                    <h3>
                                        Percent Males and Females Enrolled
                                    </h3>
                                    <div id="silverlightControlHost" style="width: 100%">
                                        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                                            width="100%">
                                            <param name="source" value="../ClientBin/IQCare_Graphs.xap" />
                                            <param name="onError" value="onSilverlightError" />
                                            <param name="background" value="white" />
                                            <param name="minRuntimeVersion" value="3.0.40818.0" />
                                            <param name="autoUpgrade" value="true" />
                                            <param name="InitParams" value="GraphName=GetMaleFemaleEnrolledPieChartinfo" />
                                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none">
                                                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                                                    style="border-style: solid" />
                                            </a>
                                        </object>
                                        <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;
                                            border: 0px"></iframe>
                                    </div>
                                </td>
                                <td class="pad18" align="center" valign="middle" style="height: 100px; width: 33%;
                                    border-top: #666699 1px solid; border-left: #666699 1px solid; border-bottom: #666699 1px solid">
                                    <h3>
                                        Percent ART and Non ART</h3>
                                    <div id="Div1" style="width: 100%">
                                        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                                            width="100%">
                                            <param name="source" value="../ClientBin/IQCare_Graphs.xap" />
                                            <param name="onError" value="onSilverlightError" />
                                            <param name="background" value="white" />
                                            <param name="minRuntimeVersion" value="3.0.40818.0" />
                                            <param name="autoUpgrade" value="true" />
                                            <param name="InitParams" value="GraphName=GetArt_NorartPieChartinfo" />
                                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0">
                                                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                                                    style="border-style: none" />
                                            </a>
                                        </object>
                                        <iframe id="Iframe1" style="visibility: hidden; height: 0px; width: 0px; border: 0px">
                                        </iframe>
                                    </div>
                                </td>
                                <td class="pad18" align="center" valign="middle" style="height: 100px; border-left: #666699 1px solid;
                                    border-right: #666699 1px solid; border-top: #666699 1px solid; border-bottom: #666699 1px solid">
                                    <h3>
                                        Percent ART by Age</h3>
                                    <div id="Div2" style="width: 100%">
                                        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                                            width="100%">
                                            <param name="source" value="../ClientBin/IQCare_Graphs.xap" />
                                            <param name="onError" value="onSilverlightError" />
                                            <param name="background" value="white" />
                                            <param name="minRuntimeVersion" value="3.0.40818.0" />
                                            <param name="autoUpgrade" value="true" />
                                            <param name="InitParams" value="GraphName=GetArtByAgeChartinfo" />
                                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none">
                                                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                                                    style="border-style: none" />
                                            </a>
                                        </object>
                                        <iframe id="Iframe2" style="visibility: hidden; height: 0px; width: 0px; border: 0px">
                                        </iframe>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
