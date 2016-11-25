<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.NigeriaMonthlyNACAReport" Codebehind="frmReport_NigeriaMonthlyNACAReport.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <%--  <form runat="server" id="frmNMonthlyReport">--%>
        <h1 class="nomargin">
            Nigeria-Monthly NACA Report
        </h1>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border pad5 Whitebg">
                        <label>
                            Date Ordered From
                        </label>
                        <input id="txtDateOrderedFrom" maxlength="8" size="5" runat="server" />
                        <%--<img ID="imgstart" src="../Images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"  onclick="w_displayDatePicker('<%= txtDateOrderedFrom.ClientID%>');" />--%>
                        <%--<SPAN class=smallerlabel>(DD-MMM-YYYY)</SPAN>--%>
                        <span class="smallerlabel">(MMM-YYYY)</span>
                        <label>
                            To</label>
                        <input id="txtDateOrderedTo" maxlength="8" size="5" runat="server" />
                        <%--<img ID="imgend" src="../Images/cal_icon.gif" height="22" alt="Date Helper" hspace="3" border="0"  onclick="w_displayDatePicker('<%= txtDateOrderedTo.ClientID%>');" />
         <SPAN class=smallerlabel>(DD-MMM-YYYY)</SPAN> --%>
                        <span class="smallerlabel">(MMM-YYYY)</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="pad5 center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
