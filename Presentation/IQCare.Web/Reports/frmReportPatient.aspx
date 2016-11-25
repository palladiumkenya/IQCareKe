<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportPatient" Title="Untitled Page" Codebehind="frmReportPatient.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        function hide(divId) {
            if (document.layers) document.layers[divId].visibility = 'hide';
            else if (document.all) document.all[divId].style.display = 'none';
            else if (document.getElementById) document.getElementById(divId).style.display = 'none';
        }

        //shows div
        function show(divId) {
            if (document.layers) document.layers[divId].visibility = 'show';
            else if (document.all) document.all[divId].style.display = 'inline';
            else if (document.getElementById) document.getElementById(divId).style.display = 'inline';
        }

    </script>
    <%--<form id ="PatientReports" method="post" runat="server">--%>
    <div>
        <h1 class="nomargin">
            Patient Reports</h1>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border pad5 whitebg" valign="top">
                        <label>
                            Select Report:</label>
                        <input type="radio" id="rdoUpARVPickup" name="rptName" value="UpARVPickup" onmouseup="up(this);" onfocus="up(this);"
                            onclick="down(this); show('DTRange')" runat="Server" />
                        <span id="Span1" class="smalllabel" runat="server">Upcoming ARV Pickup </span>
                        <input type="radio" id="rdoMisARVPickup" name="rptName" value="MisARVPickup" onmouseup="up(this);" onfocus="up(this);"
                            onclick="down(this); show('DTRange')" runat="Server" />
                        <span id="Span2" class="smalllabel" runat="server">Missed ARV Pickup</span>
                        <input type="radio" id="rdoNewPatients" name="rptName" value="NewPatients" onmouseup="up(this);" onfocus="up(this);"
                            onclick="down(this); show('DTRange')" runat="Server" />
                        <span id="Span3" class="smalllabel" runat="server">New Patients</span>
                        <input type="radio" id="rdoPregnantFU" name="rptName" value="PregnantFU" onmouseup="up(this);" onfocus="up(this);"
                            onclick="down(this); show('DTRange')" runat="Server" />
                        <span id="Span4" class="smalllabel" runat="server">PregnantFU</span>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" id="tdDate" runat="server">
                        <div id="DTRange" style="display: none">
                            <label>
                                Date Ordered (dd-mm-yyyy) From:</label>
                            <asp:TextBox ID="txtStartDate" MaxLength="10" Width="75px" runat="server"></asp:TextBox>
                            <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"
                                onclick="w_displayDatePicker('<%= txtStartDate.ClientID%>');" />
                            <label>
                                To:
                            </label>
                            <asp:TextBox ID="txtEndDate" MaxLength="10" Width="75px" runat="server"></asp:TextBox>
                            <img src="../images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"
                                onclick="w_displayDatePicker('<%= txtEndDate.ClientID%>');" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 center">
                        <asp:Button Text="Submit" ID="btnView" runat="server" OnClick="btnView_Click" />
                        <asp:Button Text=" Cancel " ID="btnReset" runat="server" OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
