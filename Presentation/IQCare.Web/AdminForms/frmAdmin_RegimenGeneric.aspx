<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.RegimenGeneric" Codebehind="frmAdmin_RegimenGeneric.aspx.cs" %>

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
                if (document.getElementById('<%=txtRegimenCode.ClientID %>').value == "") {
                    alert("Enter Regimen Code");
                    document.getElementById('<%=txtRegimenCode.ClientID %>').focus();
                    return false;
                }
                else if (document.getElementById('<%=txtRegimenName.ClientID %>').value == "") {
                    alert("Enter Regimen Name");
                    document.getElementById('<%=txtRegimenName.ClientID %>').focus();
                    return false;
                }
                else if (document.getElementById('<%=ddStage.ClientID %>').value == "0") {
                    alert("Please select stage");
                    document.getElementById('<%=ddStage.ClientID %>').focus();
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
        <div class="border center formbg">
            <br />
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border center pad5 whitebg" width="50%">
                            <asp:Label ID="lblRegimenCode" runat="server" Text="Regimen Code:" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtRegimenCode" runat="server"></asp:TextBox>
                        </td>
                        <td class="border center pad5 whitebg" width="50%">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%" align="right">
                                        <label>
                                            Regimen Name:</label>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="txtRegimenName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border center pad5 whitebg" width="50%">
                            <label>
                                Line:</label>
                            <asp:DropDownList ID="ddStage" runat="server">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="First" Value="First"></asp:ListItem>
                                <asp:ListItem Text="Second" Value="Second"></asp:ListItem>
                                <asp:ListItem Text="Third" Value="Third"></asp:ListItem>
                                <asp:ListItem Text="Not Approved" Value="Not Approved"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                            </asp:DropDownList>
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
                    <tr>
                        <td class="border center pad5 whitebg" width="50%">
                            <label class="margin">
                                Status :</label>
                            <asp:DropDownList ID="ddStatus" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">InActive</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="border center pad5 whitebg" width="50%">
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
                        <td class="border center pad5 whitebg" width="100%" colspan="2">
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
</asp:Content>
