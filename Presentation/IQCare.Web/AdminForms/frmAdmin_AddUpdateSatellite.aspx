<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.AddUpdateSatellite" CodeBehind="frmAdmin_AddUpdateSatellite.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id ="AddUpdateSatellite" method="post" runat="server" title="Add/Update Satellite">--%>
    <div>
        <h1 class="margin">
            <asp:Label ID="lblTitle" runat="server" Text="Add/Edit LaboratoryTest"></asp:Label></h1>
        <div class="center" style="padding: 5px;">
            <div class="border formbg center">
                <table width="100%" border="0" cellpadding="0" cellspacing="6">
                    <tbody>
                        <tr>
                            <td class="pad5 whitebg border">
                                <label class="right40" for="SatelliteID">
                                    Satellite ID:</label>
                                <asp:TextBox ID="txtSatelliteID" runat="server"></asp:TextBox>
                            </td>
                            <td class="pad5 whitebg border">
                                <label class="right40" for="SatelliteName">
                                    Satellite Name:</label>
                                <asp:TextBox ID="txtSatelliteName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="td1" class="pad5 whitebg border" runat="server">
                                <label class="right40" for="Status">
                                    Status:</label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td id="td2" class="pad5 whitebg border" runat="server">
                                <label class="right40" for="Priority">
                                    Priority:</label>
                                <asp:TextBox ID="txtPriority" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>