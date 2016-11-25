<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.CustomItems" Title="Untitled Page" CodeBehind="frmAdmin_CustomItems.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Customize Lists</h1>
        <div class="center" style="padding: 5px;">
            <div class="formbg border center">
                <table width="100%" border="0" cellpadding="0" cellspacing="6">
                    <tbody>
                        <tr>
                            <td class="pad5 formbg center">
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                    </h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView ID="grdCustomizeItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            BorderWidth="0" GridLines="None" CssClass="datatable" CellPadding="0" CellSpacing="0"
                                                            OnRowDataBound="ItemDataBoundEventHandler1">
                                                            <Columns>
                                                                <asp:BoundField DataField="Listname" HeaderText="Name" SortExpression="Listname">
                                                                    <ItemStyle CssClass="textstyle padgrid" HorizontalAlign="Left" Wrap="False" Font-Underline="True"
                                                                        ForeColor="#0000C0"></ItemStyle>
                                                                    <HeaderStyle BorderStyle="None" ForeColor="#000066" CssClass="tableheaderstyle pad5"
                                                                        HorizontalAlign="Left" Wrap="False" Width="100%"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Formname">
                                                                    <HeaderStyle BorderStyle="None" BorderColor="white" />
                                                                    <ItemStyle BorderColor="white" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CategoryId">
                                                                    <HeaderStyle BorderStyle="None" BorderColor="white" />
                                                                    <ItemStyle BorderColor="white" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TableName">
                                                                    <HeaderStyle BorderStyle="None" BorderColor="white" />
                                                                    <ItemStyle BorderColor="white" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FeatureID">
                                                                    <HeaderStyle BorderStyle="None" BorderColor="white" />
                                                                    <ItemStyle BorderColor="white" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Update">
                                                                    <HeaderStyle BorderStyle="None" BorderColor="white" />
                                                                    <ItemStyle BorderColor="white" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/XMLFiles/customizelist.xml">
                                                        </asp:XmlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
        </div>
    </div>
</asp:Content>
