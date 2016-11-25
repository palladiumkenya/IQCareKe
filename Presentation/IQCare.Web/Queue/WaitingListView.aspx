<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="false"
    AutoEventWireup="True" CodeBehind="WaitingListView.aspx.cs" Inherits="IQCare.Web.Queue.WaitingListView" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<%--    <script type="text/javascript">
        function FilterGrid() {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=grdWaitingList] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            })
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(FilterGrid);
    </script>--%>
    <div>
        <h2 class="forms" align="left">
            Waiting List</h2>
        <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                    border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divFilterComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="border pad5">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td colspan="1">
                                    <div class="pad5" style="white-space: nowrap">
                                        <span>
                                            <label id="labeldepartm" class="required right35" for="ddlDepartment" style="margin-left: 10px;
                                                margin-right: 10px; width: 100px">
                                                *Select List (Queue):</label>
                                            <asp:DropDownList ID="ddwaitingList" runat="Server" Width="250px" Height="20px" OnSelectedIndexChanged="SelectedQueueChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </span>
                                    </div>
                                </td>
                                <td align="right">
                                    <div style="display: none">
                                        <label class="right" id="lblWaitingfor" runat="server">
                                            Waiting For:</label>
                                        <asp:DropDownList ID="ddWaitingFor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectedWaitingForChanged">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="whitebg border pad5" style="overflow: auto;">
                    <div class="grid">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: 200px; overflow: auto">
                                        <div id="grd_custom" class="GridView whitebg">
                                            <asp:GridView ID="grdWaitingList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                Width="100%" BorderColor="White" PageIndex="1" BorderWidth="1px" GridLines="None"
                                                CssClass="datatable" CellPadding="0" OnSelectedIndexChanged="SelectedPatientChanged"
                                                DataKeyNames="Ptn_PK,WaitingListID,ModuleID" OnRowDataBound="grdWaitingList_RowDataBound"
                                                ShowHeaderWhenEmpty="True" OnDataBound="grdWaitingList_DataBound">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <RowStyle CssClass="gridrow" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" SelectText="Serve" />
                                                    <asp:BoundField HeaderText="Patient ID" DataField="PatientFacilityID" />
                                                    <asp:BoundField HeaderText="PatientID" DataField="Ptn_PK" Visible="false" />
                                                    <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                                    <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                                    <asp:BoundField HeaderText="DOB" DataField="DOB" />
                                                    <asp:BoundField HeaderText="Gender" DataField="Sex" />
                                                    <asp:BoundField HeaderText="Waiting Time" DataField="TimeOnList" />
                                                    <asp:BoundField HeaderText="WaitingListID" DataField="WaitingListID" Visible="false" />
                                                    <asp:BoundField HeaderText="Added By" DataField="UserName" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddwaitingList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddWaitingFor" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="border pad5 whitebg">
            <div class="bottom-inner" style="text-align: center">
                <asp:Button ID="btnclose" runat="server" Font-Size="12px" Width="75px" Text="Close"
                    OnClick="ExitPage" />
            </div>
        </div>
    </div>
   <%-- <uc1:progressControl ID="progressControl1" runat="server" />--%>
</asp:Content>
