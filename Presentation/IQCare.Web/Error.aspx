<%@ Page Language="C#" AutoEventWireup="True" Inherits="Error" Codebehind="Error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en-US" xml:lang="en-US" xmlns="http://www.w3.org/1999/xhtml">
<head id="masterHead">
    <title>International Quality Care Patient Management and Monitoring System</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <link rel="stylesheet" type="text/css" href="Style/styles.css" />
</head>
<script language="javascript" type="text/javascript">

    function fnHelp() {
        var path = frmLogin.CallHelp().value;
        window.location.href = path;
        //window.showHelp("./IQCareHelp/IQCareARUserManualSep2010.chm")
    }
    function CloseWindow() {
        window.focus();
    }
</script>
<body>
    <form id="signIn" style="width: 100%" enableviewstate="true" runat="server">
    <div class="loginpageheight">
        <div class="utility" align="right">
            <a class="utility" href="frmLogin.aspx" onclick="window.open('./IQCareHelp/index.html'); return false">
                Help</a>
        </div>
        <center>
            <div id="main">
                <div id="bluetop">
                    <img id="logo" height="94" alt="International Quality Care " src="./images/iq_logo.gif"
                        width="236" border="0" />
                    <img id="pmms" height="53" alt="Patient Management and Monitoring System" src="./images/pmms.gif"
                        width="264" border="0" />
                    <img id="collage" height="117" alt="" src="./images/collage.jpg" width="424" border="0" />
                    <img id="tabfacility" height="23" alt="Facility Name" src="./images/tab_facility.gif"
                        width="377" border="0" />
                </div>
                <div style="padding-top: 140px;">
                    <!-- begin content area -->
                    <table cellpadding="0" cellspacing="10" style="width: 100%; border: solid 1px #FF8080;
                        background-color: #FCFCE0">
                        <tr>
                            <td style="width: 48px">
                                
                            </td>
                            <td>
                                <div style="font-size: 16px; font-weight: bold; font-family: Arial Black, Tahoma, Verdana, Arial;
                                    color: #C00000">
                                    System Error</div>
                                Unrecoverable system error has occured within IQCare during processing. Please contact
                                the support team.
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <br />
                                <br />
                                <asp:HyperLink ID="lnkReferer" runat="server">Go back to the previous page.</asp:HyperLink><br />
                                <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/frmLogin.aspx">Go to the Login page.</asp:HyperLink><br />
                            </td>
                        </tr>
                            </table>
                            <asp:Panel ID="pnlErrorDetails" runat="server" Style="border: solid 1px #808080;
                            padding: 10px; background-color: #EFEFEF; margin: 10px; margin-top: 20px">
                            <asp:Label ID="txtErrorDetails" runat="server"></asp:Label>
                        </asp:Panel>
                
                </div>
            </div>
        </center>
        <table width="900">
            <tr style="width: 100%">
                <td align="left" style="width: 50%">
                    <p>
                        <a href="http://futuresgroup.com/ " onclick="window.open('http://futuresgroup.com/ '); return false">
                            <img src="./Images/FGI.jpg" width="70" vspace="5" border="0" alt="" /></a>
                    </p>
                </td>
            </tr>
        </table>
        <!-- end content area -->
    </div>
    </form>
</body>
</html>
