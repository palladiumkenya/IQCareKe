<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"  EnableEventValidation="false"
    CodeBehind="FindServiceOrder.aspx.cs" Inherits="IQCare.Web.ClinicalService.FindServiceOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../Incl/quicksearch.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        function FilterGrid() {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=grdPatientOrder] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            })
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(FilterGrid);
    </script>
    <div>
        <h2 class="forms" align="left">
            Service Orders</h2>
        <div class="center">
            <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="true">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divFilterComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0" class="form">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <label style="padding-left: 10px" id="lblpurpose" runat="server">
                                        Search for:</label>
                                    <asp:RadioButtonList ID="rbtlst_findOrder" runat="server" AutoPostBack="True" 
                                        RepeatDirection="Horizontal" 
                                        onselectedindexchanged="rbtlst_findOrder_SelectedIndexChanged">
                                        <asp:ListItem id="rbt_openorders" Text="Pending Orders" Selected="True"></asp:ListItem>
                                        <asp:ListItem id="rbt_patients" Text="Patient"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" style="display: none">
                                    <asp:CheckBox ID="ckbTodaysOrders" runat="server" Text="Today's orders only" AutoPostBack="True"
                                        Checked="True" oncheckedchanged="ckbTodaysOrders_CheckedChanged"></asp:CheckBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="pendingPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="grid" id="divOrders" style="width: 100%;">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: 200px; overflow: auto">
                                        <div id="div-gridview" class="GridView whitebg">
                                            <asp:GridView ID="grdPatientOrder" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                Width="100%" BorderColor="white" PageIndex="1" BorderWidth="1" GridLines="None"
                                                CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="grdPatientOrder_SelectedIndexChanged"
                                                AutoGenerateSelectButton="False" DataKeyNames="PatientID" OnRowDataBound="grdPatientOrder_RowDataBound"
                                                OnDataBound="grdPatientOrder_DataBound">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <RowStyle CssClass="gridrow" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Patient #" DataField="ID" />
                                                    <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                                    <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                                    <asp:BoundField HeaderText="DOB" DataField="DOB" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField HeaderText="Ordered By" DataField="OrderedBy"  />
                                                    <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField HeaderText="Requested From" DataField="OrderedBy"  />
                                                </Columns>
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ckbTodaysOrders" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
             <uc1:progresscontrol ID="progressControl1" runat="server" />
        </div>
    </div>
</asp:Content>
