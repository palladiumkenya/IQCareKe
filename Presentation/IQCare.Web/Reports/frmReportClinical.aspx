<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportClinical" Title="Untitled Page" Codebehind="frmReportClinical.aspx.cs" %>

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
        function btn_Back() {
            history.back();

        }



    </script>
    <div>
        <%--<form id ="frmClinicalReports"  method="post" runat="server">--%>
        <h1 class="nomargin">
            Facility ARV Pick up Report</h1>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border pad5 whitebg" valign="top">
                        <label>
                            Select Report:</label>
                        <input type="radio" id="rdoARVAdherence" name="rptName" value="ARVAdherence" onmouseup="up(this);" onfocus="up(this);"
                            onclick="down(this); show('DClient');hide('DTRange');hide('DType')" runat="Server" />
                        <span id="Span1" class="smalllabel" runat="server">Adherence ARV Collection </span>
                        <input type="radio" id="rdoMisARVAppointment" name="rptName" value="MisARVAppointment" onmouseup="up(this);"
                            onfocus="up(this);" onclick="down(this); show('DTRange');show('DType');hide('DClient')"
                            runat="Server" />
                        <span id="Span2" class="smalllabel" runat="server">Missed ARV Appointment</span>
                        <!--<input  type="radio" id="rdoNewPatients"  name="rptName" value="NewPatients" onfocus="up(this);" onclick="down(this); show('DTRange')" runat="Server"/> <span id="Span3" class="smalllabel" runat="server">New Patients</span>
     <input  type="radio" id="rdoPregnantFU"  name="rptName" value="PregnantFU" onfocus="up(this);" onclick="down(this); show('DTRange')" runat="Server"/> <span id="Span4" class="smalllabel" runat="server">PregnantFU</span> -->
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" id="tdDType" runat="server">
                        <div id="DType" style="display: none">
                            <input type="radio" id="rdoOrdered" name="dType" value="O" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('DTRange')"
                                runat="Server" />
                            <span id="SpanOrdered" class="smalllabel" runat="server">Ordered Date</span>
                            <input type="radio" id="rdoDispensed" name="dType" value="D" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this); show('DTRange')" runat="Server" />
                            <span id="SpanDispensed" class="smalllabel" runat="server">Dispensed Date</span>
                        </div>
                        <div id="DClient" style="display: none">
                            <input type="radio" id="rdoAllClients" name="sClient" value="A" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this)" runat="Server" />
                            <span id="SpanAll" class="smalllabel" runat="server">All Patients</span>
                            <input type="radio" id="rdoSelectClient" name="sClient" value="S" onmouseup="up(this);" onfocus="up(this);"
                                onclick="down(this)" runat="Server" />
                            <span id="SpanSelect" class="smalllabel" runat="server">Select Patient</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" id="tdDate" runat="server">
                        <div id="DTRange" style="display: none">
                            <label>
                                Defaulter as at:</label>
                            <asp:TextBox ID="txtStartDate" MaxLength="10" Width="75px" runat="server"></asp:TextBox>
                            <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"
                                onclick="w_displayDatePicker('<%= txtStartDate.ClientID%>');" />
                            <!--<label > To: </label> 
   <asp:TextBox id="txtEndDate" maxlength="10" width=75px runat="server"></asp:TextBox>
      <img src="../images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0" onclick="w_displayDatePicker('<%= txtEndDate.ClientID%>');" />  -->
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 center">
                        <asp:Button Text="Submit" ID="btnView" runat="server" OnClick="btnView_Click" />
                        <asp:Button Text=" Cancel " ID="btnReset" runat="server" OnClick="btnReset_Click" />
                        <input type="button" value="Back" onclick="javascript:btn_Back();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
