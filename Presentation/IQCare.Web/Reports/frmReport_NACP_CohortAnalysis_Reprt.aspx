<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.NACP_CohortAnalysis_Reprt"
    Title="Untitled Page" Codebehind="frmReport_NACP_CohortAnalysis_Reprt.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        function btnEnabledDisabled() {

            if (document.getElementById('<%=rdoMonth.ClientID%>').checked == true) {
                document.getElementById('dvMonth').disabled = ""
                document.getElementById('dvQuarter').disabled = "disabled"
                document.getElementById('<%=ddMonthyear.ClientID %>').selectedIndex = 0;
                document.getElementById('<%=txtyears.ClientID %>').value = '';

            }

            else if (document.getElementById('<%=rdoQuarter.ClientID%>').checked == true) {
                document.getElementById('dvQuarter').disabled = ""
                document.getElementById('dvMonth').disabled = "disabled"
                document.getElementById('<%=ddMonth.ClientID %>').selectedIndex = 0;
                document.getElementById('<%=txtYear.ClientID %>').value = '';

            }



        } 
   
    </script>
    <div>
        <%--<form runat="server" id="frmNMonthlyReport">--%>
        <h1 class="nomargin">
            Cohort Tracking
        </h1>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border center whitebg" style="width: 562px">
                        <label class="margin10left">
                            One Cohort-Six Month
                        </label>
                        <input type="radio" id="rdoMonth" name="rdoOption" onclick="btnEnabledDisabled(this)"
                            runat="server" />
                    </td>
                    <td class="border center whitebg" width="40%">
                        <label class="margin10left">
                            Six Cohort-Two Year</label>
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
                                Month
                            </label>
                            <asp:DropDownList ID="ddMonthyear" runat="server" Width="100px">
                            </asp:DropDownList>
                            <label>
                                Year</label>
                            <asp:TextBox ID="txtyears" MaxLength="4" size="5" runat="server"></asp:TextBox>
                        </div>
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
