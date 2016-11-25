<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.TanzaniaNACPMonthlyReport"
    Title="Untitled Page" Codebehind="frmReport_TanzaniaNACPMonthlyReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        function btnEnabledDisabled() {

            if (document.getElementById('<%=rdoMonth.ClientID%>').checked == true) {
                document.getElementById('dvMonth').disabled = ""
                document.getElementById('dvQuarter').disabled = "disabled"

            }

            else if (document.getElementById('<%=rdoQuarter.ClientID%>').checked == true) {
                document.getElementById('dvQuarter').disabled = ""
                document.getElementById('dvMonth').disabled = "disabled"
            }



        } 
   
    </script>
    <div>
        <%-- <form runat="server" id="frmNMonthlyReport">--%>
        <h1 class="nomargin">
            Tanzania NACP Monthly/Quarterly Report
        </h1>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border center whitebg" style="width: 562px">
                        <label class="margin10left">
                            Select Month
                        </label>
                        <input type="radio" id="rdoMonth" name="rdoOption" onclick="btnEnabledDisabled(this)"
                            runat="server" />
                    </td>
                    <td class="border center whitebg" width="40%">
                        <label class="margin10left">
                            Select Quarter</label>
                        <input type="radio" id="rdoQuarter" name="rdoOption" onclick="btnEnabledDisabled(this)"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 Whitebg">
                        <div id="dvMonth">
                            <label>
                                Month
                            </label>
                            <asp:DropDownList ID="ddMonth" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label>
                                Year</label>
                            <asp:TextBox ID="txtYear" MaxLength="8" size="5" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td class="border center whitebg" style="height: 46px">
                        <div id="dvQuarter">
                            <label>
                                Quarter
                            </label>
                            <asp:DropDownList ID="ddQuarter" runat="server" Width="100px">
                            </asp:DropDownList>
                            <label>
                                Year</label>
                            <asp:TextBox ID="txtyears" MaxLength="4" size="5" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="pad5 center">
                        <asp:Label ID="lblWarning" runat="server" Width="100%" CssClass="textjustify" Text="*Warning: Tanzania Report can take some time to display which depends on the speed of your computer. Please be patient and allow 2-10 minutes"
                            ForeColor="Red" Font-Bold="true"></asp:Label>
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
    </div>
</asp:Content>
