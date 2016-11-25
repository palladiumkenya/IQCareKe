<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.ManageDisease" Title="Untitled Page" Codebehind="frmAdmin_Disease.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="addeditDisease" method="post" runat="server">--%>
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Disease Master</h1>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br>
                <h2 class="forms" align="left">
                    <asp:Label ID="lblH2" runat="server" Text="Add/Edit Disease"></asp:Label></h2>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" width="50%">
                                <label class="right30">
                                    Disease name:</label>
                                <asp:TextBox ID="txtDiseaseName" runat="server"></asp:TextBox>
                            </td>
                            <td class="border center pad5 whitebg" width="50%">
                                <asp:Label class="right30" runat="server" ID="lblStatus">Status:</asp:Label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" width="50%">
                                <label class="right30">
                                    Sequence Number:</label>
                                <asp:TextBox ID="txtSeq" runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td class="pad5 center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
