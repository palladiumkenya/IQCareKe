<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.PatientRecordCD4" Codebehind="frmClinical_PatientRecordCD4.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="lblHeader" runat="server">Lab Values</title>
    <link rel="stylesheet" type="text/css" href="../Style/styles.css" />
    <link rel="stylesheet" type="text/css" href="../Style/calendar.css" />
    <script language="javascript" type="text/javascript" src="../incl/weeklycalendar.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/dateformat.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/jsDate.js"></script>
    <script language="javascript" type="text/javascript">        buildWeeklyCalendar(1);</script>
</head>
<body>
    <form id="CTC" method="post" runat="server">
    <div class="border center formbg" style="width: 696px; height: 204px; padding-left: 5px;
        padding-right: 5px; padding-top: 5px; padding-bottom: 5px">
        <table class="left" border="0">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" align="left">
                        <div id="divCD4" runat="server">
                            <label style="margin-left: 31px">
                                CD4:</label>
                            <asp:TextBox ID="txtCD4" runat="server"> </asp:TextBox>
                            <label>
                                c/mm^3</label>
                        </div>
                    </td>
                    <td class="border pad5 whitebg" style="width: 337px">
                        <div id="divCD4Percent" runat="server">
                            <asp:TextBox ID="txtCD4Percent" runat="server"> </asp:TextBox>
                            <label>
                                Percent</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" align="center">
                        <div id="divTLC" runat="server">
                            <label class="left">
                                TLC:</label>
                            <asp:TextBox ID="txtTLC" runat="server"> </asp:TextBox>
                            <label>
                                10^3 cells/mcl</label>
                        </div>
                    </td>
                    <td class="border pad5 whitebg" style="width: 337px">
                        <div id="divTLCPercent" runat="server">
                            <asp:TextBox ID="txtTLCPercent" runat="server"> </asp:TextBox>
                            <label>
                                Percent</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form" style="width: 315px; height: 17px;">
                        <label class="right50 required">
                            *Ordered by:</label>
                        <asp:DropDownList ID="ddlOrderedbyName" runat="Server">
                            <asp:ListItem> Albert Hidoson</asp:ListItem>
                            <asp:ListItem> braham Lincon</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="form" style="width: 337px; height: 17px;">
                        <label class="right50 required" for="OrderedDate">
                            *Ordered by Date:</label>
                        <input id="txtOrderedDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="OrderedDate"
                            runat="server" />
                        <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtOrderedDate.ClientID%>');"
                            height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                            border="0" name="appDateimg" />
                        <span class="smallerlabel" id="appDatespan1">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
                <tr>
                    <td class="form" style="width: 315px; height: 22px;">
                        <label class="right50 required">
                            *Reported by:</label>
                        <asp:DropDownList ID="ddlReportedBy" runat="server">
                            <asp:ListItem> Albert Hidoson</asp:ListItem>
                            <asp:ListItem> braham Lincon</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="form" style="width: 337px; height: 22px;">
                        <label class="right50 required" for="ReportedbyDate">
                            *Reported by Date:</label>
                        <input id="txtReportedDate" onblur="DateFormat(this,this.value,event,false,'3')"
                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                            maxlength="11" size="11" name="ReportedDate" runat="server" />
                        <img id="appDateimg2" onclick="w_displayDatePicker('<%=txtReportedDate.ClientID%>');"
                            height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                            border="0" name="appDateimg" />
                        <span class="smallerlabel" id="appDatespan2">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
                <tr>
                    <td class="form" align="center" colspan="2" style="height: 16px">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
