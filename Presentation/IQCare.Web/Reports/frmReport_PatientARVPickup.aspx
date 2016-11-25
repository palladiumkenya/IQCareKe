<%@ Page AutoEventWireup="True" MasterPageFile="~/MasterPage/IQCare.master"
    Inherits="IQCare.Web.Reports.PatientARVPickup" Language="C#" Codebehind="frmReport_PatientARVPickup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--   <form runat="server" id="frmPatientARVPickup">--%>
    <div style="padding: 8px;">
        <%-- <h1 class="margin">Patient ARV Pick-up</h1>--%>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border center whitebg" style="padding-left: 20" align="left" valign="top">
                        <%--<label id="lblSelect"> All Facility/Satellite:</label>--%>
                        <input id="chkAll" enableviewstate="true" type="checkbox" value="None" name="All"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="border center whitebg">
                        <label>
                            Date Ordered From :</label>
                        <asp:TextBox ID="txtStartDate" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                        <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"
                            onclick="w_displayDatePicker('<%= txtStartDate.ClientID%>');" />
                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        <label>
                            To</label>
                        <asp:TextBox ID="txtEndDate" MaxLength="11" Width="75px" runat="server"></asp:TextBox>
                        <img src="../Images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"
                            onclick="w_displayDatePicker('<%= txtEndDate.ClientID%>');" />
                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="pad5 center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmitClick" />
                        <asp:Button ID="btnCancel" runat="server" Text="Back" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
