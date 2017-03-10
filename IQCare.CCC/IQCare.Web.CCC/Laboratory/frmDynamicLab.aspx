<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="frmDynamicLab.aspx.cs" Inherits="IQCare.Web.Laboratory.DynamicLabForm" %>

<asp:Content ID="pageContent" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 <script language="javascript" type="text/javascript">
    function WindowPrint() {
        window.print();
    }

</script>
    <div class="center" style="padding: 8px;">
    <span style="margin-left: 835px">
            <asp:LinkButton ID="lnkLabTest" runat="server" Text="Edit Lab Order" OnClick="lnkLabTest_Click"></asp:LinkButton></span>
        <asp:HiddenField ID="HMODe" runat="server" /> <div class="border center pad5 formbg" id="maindiv" runat="server">
          
          
        </div>
         <br />
            <div class="border center formbg">
                <br />
                <h2 class="forms" align="left">
                    Approval and Signatures</h2>
                <table cellspacing="6" cellpadding="0" border="0" width="100%">
                    <tbody>
                       
                        <tr>
                            <td class="form" align="center">
                                <label id="lblreportedby" runat="server">
                                    Reported by:</label>
                                <asp:DropDownList ID="ddlLabReportedbyName" runat="Server">
                                </asp:DropDownList>
                            </td>
                            <td class="form" align="center">
                                <label id="lblreportedbydate" runat="server" for="labReportedbyDate">
                                    Reported By Date:</label>
                                <asp:TextBox ID="txtlabReportedbyDate" MaxLength="12" size="11" runat="server"></asp:TextBox>
                                <img id="IMG1" onclick="w_displayDatePicker('<%=txtlabReportedbyDate.ClientID%>');"
                                    height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                    border="0" name="appDateimg" />
                                <span class="smallerlabel" id="SPAN1">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" colspan="2">
                                <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                    Wrap="true">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2">
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
    </div>
</asp:Content>
