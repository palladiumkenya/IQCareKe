<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.UserList" Title="Untitled Page" Codebehind="frmAdmin_UserList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
   
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            User Administration</h1>
        <div class="center">
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div id="div1" class="top-inner">
                                            <div class="top" id="divtop">
                                                <h2>
                                                    User List</h2>
                                            </div>
                                         </div>
                                      </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 300px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="GrdUserList" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        PageSize="1" AllowSorting="true" OnRowDataBound="GrdUserList_RowDataBound" OnSelectedIndexChanging="GrdUserList_SelectedIndexChanging"
                                                        HorizontalAlign="Left" CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None"
                                                        OnSorting="GrdUserList_Sorting" CssClass="datatable table-striped table-responsive" BackColor="White">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <RowStyle CssClass="gridrow" />
                                                    </asp:GridView>
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
                    <%--<tr>
                        <td class="border pad5 formbg">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    User List</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid">
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
                    </tr>--%>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <%--</form>--%>
    </div>
</asp:Content>
