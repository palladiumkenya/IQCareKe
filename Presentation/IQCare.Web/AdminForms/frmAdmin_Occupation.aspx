<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.Occupation" Title="Untitled Page" Codebehind="frmAdmin_Occupation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Occupation Master</h1>
        <div class="border center formbg">
            <br>
            <h2 class="forms" align="left">
                <asp:Label ID="lblH2" runat="server" Text="Add/Edit Occupation"></asp:Label></h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" width="30%">
                            <label class="right" for="LastName">
                                Occupation Name:</label>
                            <asp:TextBox ID="txtOccupationName" runat="server"></asp:TextBox>
                        </td>
                        <td class="border center pad5 whitebg" width="30%">
                            <label class="right30">
                                Sequence Number:</label>
                            <asp:TextBox ID="txtSeq" runat="server">
                            </asp:TextBox>
                        </td>
                        <td class="border center pad5 whitebg" width="40%">
                            <asp:Label class="right30" runat="server" ID="lblStatus">Status:</asp:Label>
                            <asp:DropDownList ID="ddStatus" runat="server">
                                <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table>
                <tbody>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </table>
            <br />
        </div>
    </div>
</asp:Content>
