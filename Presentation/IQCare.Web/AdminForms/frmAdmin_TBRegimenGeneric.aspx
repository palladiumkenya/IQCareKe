<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.TBRegimenGeneric" Codebehind="frmAdmin_TBRegimenGeneric.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="addeditmasterdrug" method="post" runat="server">--%>
    <div>
        <h3 class="margin" align="left">
            <asp:Label ID="lblH2" runat="server"></asp:Label></h3>
        <script language="javascript" type="text/javascript">
            function GetControl() {
                document.forms[0].submit();
            }
            function fnValidate() {
                if (document.getElementById('<%=txtRegimenName.ClientID %>').value == "") {
                    alert("Enter TB Regimen Name");
                    document.getElementById('<%=txtRegimenName.ClientID %>').focus();
                    return false;
                }
                else if (document.getElementById('<%=ddTrMonths.ClientID %>').value == "0") {
                    alert("Please select Month");
                    document.getElementById('<%=ddTrMonths.ClientID %>').focus();
                    return false;
                }
                else if (document.getElementById('<%=ddStatus.ClientID %>').value == "") {
                    alert("Please select status");
                    document.getElementById('<%=ddStatus.ClientID %>').focus();
                    return false;
                }
                else if (document.getElementById('<%=txtSeqNo.ClientID %>').value == "") {
                    alert("Please Enter Priority");
                    document.getElementById('<%=txtSeqNo.ClientID %>').focus();
                    return false;
                }

                var list = document.getElementById('<%=lstGeneric.ClientID %>');
                var listcount = list.getElementsByTagName('option');
                var blnfound = false;

                if (listcount.length > 0) {
                    blnfound = true;

                }

                if (!blnfound) {
                    var msg = "Please enter generic name";
                    alert(msg);
                    return false;
                }

                return true;
            }
 
        </script>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br />
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="border center pad5 whitebg" width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%" align="right">
                                                        <label>
                                                            TB Regimen Name:</label>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:TextBox ID="txtRegimenName" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="border center pad5 whitebg" width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%" align="right">
                                                        <label>
                                                            Generic:</label>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnAddGeneric" runat="server" Text="..." OnClick="btnAddGeneric_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ListBox ID="lstGeneric" runat="server" Height="40px" Width="150px"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" width="40%">
                                <label>
                                    Treatment Time (Months):</label>
                                <asp:DropDownList ID="ddTrMonths" runat="server">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="border center pad5 whitebg" width="30%">
                                <label class="margin">
                                    Status :</label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">InActive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="border center pad5 whitebg" width="30%">
                                <table>
                                    <tr>
                                        <td style="width: 50%" align="right">
                                            <asp:Label ID="lblPriority" runat="server" Text="Priority :" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:TextBox ID="txtSeqNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" width="100%" colspan="3">
                                <div id="divStatus" runat="server" style="display: inline">
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return fnValidate();"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                                <asp:Button ID="btn" runat="server" CssClass="textstylehidden" Text="OK" OnClick="btn_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
