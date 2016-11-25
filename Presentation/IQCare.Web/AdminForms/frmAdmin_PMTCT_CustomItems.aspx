<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.PMTCTCustomItems"
    Title="Untitled Page" Codebehind="frmAdmin_PMTCT_CustomItems.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <%-- <form id="Customizelist" method="post" runat="server">--%>
        <h1 class="margin" style="padding-left: 10px;">
            Customize Lists</h1>
        <div class="center" style="padding: 5px;">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg" style="padding: 15px;">
                            <div class="GridView whitebg" style="cursor: pointer; height: 280px; overflow: auto">
                                <asp:TreeView ID="tvcustomlist" runat="server">
                                </asp:TreeView>
                                <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/XMLFiles/customizelist.xml">
                                </asp:XmlDataSource>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <%-- </form>--%>
    </div>
</asp:Content>
