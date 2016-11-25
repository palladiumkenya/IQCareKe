<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Pharmacy.PharmacySelector" Codebehind="frmpharmacyselector.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="../style/styles.css" />
    <link rel="stylesheet" type="text/css" href="../style/calendar.css" />
    <title id="lblHeader" runat="server"></title>
</head>
<body>
    <script language="javascript" type="text/javascript" src="../incl/menu.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/IQCareScript.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/weeklycalendar.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/highlightLabels.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/dateformat.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/jsDate.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/disableEnter.js"></script>
    <script language="javascript" type="text/javascript">        buildWeeklyCalendar(0);</script>
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
    </script>
    <form id="frmpharmacyselect" method="post" runat="server">
    <h1 id="H1" class="nomargin" runat="server">
        Pharmacy</h1>
    <div class="border formbg">
        <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" colspan="2" nowrap="nowrap">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pharmacy(select all
                                        that apply):</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="divborderPaperlessLab" align="left">
                                        <asp:CheckBoxList ID="chkPharmacyselect" runat="server" Width="100%">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 center" colspan="2">
                        <asp:Button ID="btnsave" runat="server" Font-Size="12px" Width="75px" Text="Save"
                            OnClick="btnsave_Click" />
                        <asp:Button ID="btnclose" runat="server" Font-Size="12px" Width="75px" Text="Close"
                            OnClick="btnclose_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
