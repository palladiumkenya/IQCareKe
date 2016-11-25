<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.ManageEducation" Title="Untitled Page" EnableViewState="false"
    CodeBehind="Add_Edit_Education.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="addeditEducation" method="post" runat="server">--%>
    <div>
        <!--<H3 class=margin align=left><asp:Label ID="lblH3" runat="server" ></asp:Label></H3>-->
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border center pad5 whitebg" width="50%">
                                <label>
                                    Education Name:</label>
                                <asp:TextBox ID="txtEducationName" runat="server"></asp:TextBox>
                            </td>
                            <td class="border center pad5 whitebg" width="50%">
                                <label>
                                    Education Active:</label>
                                <asp:DropDownList ID="ddEducation" runat="server">
                                    <asp:ListItem>Active</asp:ListItem>
                                    <asp:ListItem>Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td class="center pad5">
                                <asp:Button ID="btnSave" runat="server" Text="Save" />
                                <asp:Button ID="btnDelete" runat="server" Text="Remove" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>