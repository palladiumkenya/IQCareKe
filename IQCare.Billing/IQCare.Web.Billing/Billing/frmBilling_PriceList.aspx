<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmBilling_PriceList.aspx.cs" Inherits="IQCare.Web.Billing.PriceList" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function PrintReport() {

            var _priceDate = $('#<%=textPriceListDate.ClientID%>').val();

            var url = "PrintPriceList.aspx?print=true";
            OpenNewWindow(url, "PrintPriceList");
        }
        function OpenNewWindow(pageurl, pgname) {
            var w = screen.width - 60;
            var h = screen.height - 60;
            var winprops = "location=no,scrollbars=yes,resizable=yes,status=no";
            var frmwin = window.open(pageurl, pgname, winprops);
            if (parseInt(navigator.appVersion) >= 4) {
                frmwin.window.focus();
            }
        }
             
    </script>
    <style>
        div.ajax__calendar_container, div.ajax__calendar_body
        {
            width: 225px;
        }
        
        .ajax__calendar_days td
        {
            padding: 2px 4px;
        }
    </style>
   <hr />
    <div>
<%--        <h2 class="forms" align="left">
            Price List</h2>--%>
        <asp:UpdatePanel runat="server" ID="divFilterComponent" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="panelFilter" runat="server" DefaultButton="btnFilter">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0" class="form pad5 center">
                        <tbody>
                            <tr>
                                <td style="text-align: left; margin-left: 5px">
                                    <asp:Button ID="btnPrint" CssClass="btn btn-info" runat="server" Text="Print Price List" Width="120px" Style="display: block;
                                        margin-bottom: 5px" class="greenbutton" />

                                    <ajaxToolkit:ConfirmButtonExtender ID="cbePrint" runat="server" DisplayModalPopupID="mpePrintPriceList"
                                        TargetControlID="btnPrint">
                                    </ajaxToolkit:ConfirmButtonExtender>
                                    <ajaxToolkit:ModalPopupExtender ID="mpePrintPriceList" runat="server" PopupControlID="pnlPopup"
                                        TargetControlID="btnPrint" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                        width: 300px; border: 3px solid #0DA9D0;">
                                        <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                            text-align: center; font-weight: bold;">
                                            Confirmation
                                        </div>
                                        <div style="min-height: 30px; line-height: 20px; text-align: center; font-weight: bold;">
                                            The full price list will be printed.<br />
                                            Only items whose price has been set will be printed.
                                        </div>
                                        <div style="min-height: 30px; line-height: 20px; text-align: center; font-weight: bold;">
                                            Price Date:&nbsp;&nbsp;
                                            <asp:TextBox ID="textPriceListDate" runat="server" Text="" MaxLength="11" Width="100px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="textPriceListDate"
                                                ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textPriceListDate"
                                                Format="dd-MMM-yyyy" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="textPriceListDate"
                                                Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                                ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </div>
                                        <div style="min-height: 30px; line-height: 20px; text-align: center; font-weight: bold;">
                                            Do you want to proceed?
                                        </div>
                                        <div style="padding: 3px;" align="right">
                                            <asp:Button ID="btnYes" runat="server" Text="Proceed" ForeColor="DarkGreen" OnClick="btnPrint_Click"
                                                CausesValidation="false" /><asp:Button ID="btnNo" runat="server" Text="No" ForeColor="DarkBlue"
                                                    Style="margin-left: 10px" /></div>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; margin-left: 3px">
                                    Item Type:
                                </td>
                                <td style="text-align: left; white-space: nowrap">
                                    <asp:DropDownList ID="ddlItemType" CssClass="form-control" runat="server" Width="230px" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; margin-left: 3px">
                                    Item Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="textSearchText" CssClass="form-control" runat="server" MaxLength="25" Width="250px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteSearchText" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textSearchText" ValidChars="-/\* ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: left; margin-left: 3px">
                                    Show priced only
                                    <asp:RadioButtonList ID="rblOption" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left; white-space: nowrap">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary" Text="View Price List" Width="120px" OnClick="btnFilter_Click"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnYes" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="divErrorCompenent" UpdateMode="Always">
            <ContentTemplate>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td align="left" style="padding-left: 10px; padding-right: 15px">
                                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </asp:Panel>
                                <asp:HiddenField ID="HPageIndex" runat="server" />
                                <asp:HiddenField ID="HSearchText" runat="server" />
                                <asp:HiddenField ID="HItemType" runat="server" />
                                <asp:HiddenField ID="HPerm" runat="server" />
                                <asp:HiddenField ID="HPages" runat="server" />
                                <asp:HiddenField ID="HPriced" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="pad5 formbg border">
                                <div id="divPriceList" class="grid" style="width: 100%;">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        <asp:Label runat="server" ID="labelNote"></asp:Label></h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 280px; overflow: auto">
                                                    <div id="div-PricelistGridview" class="GridView whitebg">
                                                        <asp:GridView ID="gridPriceList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                            EmptyDataText="No Data to display" ShowHeaderWhenEmpty="True" Width="100%" BorderStyle="Solid"
                                                            DataKeyNames="ItemID,ItemTypeID,VersionStamp" OnRowCommand="gridPriceList_RowCommand"
                                                            OnRowDataBound="gridPriceList_RowDataBound" PageSize="50" OnRowCreated="gridPriceList_RowCreated"
                                                            AllowPaging="false" Visible="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-Width="40%">
                                                                    <ItemStyle Width="40%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Selling Price">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelPrice" runat="server" Text='<%# Bind("SellingPrice", "{0:N}") %>'></asp:Label>
                                                                        </span><span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:TextBox ID="textPrice" runat="server" Text='<%# Bind("SellingPrice", "{0:N}") %>'
                                                                                MaxLength="11" Width="70px"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftePrice" runat="server" TargetControlID="textPrice"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pricing Type">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelPriceType" runat="server"></asp:Label>
                                                                        </span><span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:DropDownList runat="server" ID="ddlPriceType" AutoPostBack="false">
                                                                                <asp:ListItem Value="Item">Item</asp:ListItem>
                                                                                <asp:ListItem Value="Dose">Dose</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Effective Date">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# HideEdit() %>; white-space: nowrap'>
                                                                            <asp:Label ID="labelPriceDate" runat="server" Text='<%# Bind("PriceDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                        </span>
                                                                        <div style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:TextBox ID="textPriceDate" runat="server" Text='<%# Bind("PriceDate", "{0:dd-MMM-yyyy}") %>'
                                                                                MaxLength="11" Width="70px"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="textPriceDate"
                                                                                ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                                                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textPriceDate"
                                                                                Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_past_dates" />
                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="textPriceDate"
                                                                                Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                                                                ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999">
                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                        </div>
                                                                        <asp:HiddenField ID="hdVersionStamp" runat="server" Value="" />
                                                                        <asp:HiddenField ID="HFlag" runat="server" Value="" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="15%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <span style='display: <%# ShowInEdit() %>; white-space: nowrap'>
                                                                            <asp:CheckBox runat="server" ID="chkDelete" AutoPostBack="false" TextAlign="Left" />
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <RowStyle CssClass="gridrow" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="bottom-outer">
                                    <div class="bottom-inner">
                                        <div class="bottom" style="text-align: center">
                                            <br />
                                            <div id="divAction" style="white-space: nowrap; text-align: center;">
                                                <asp:Button ID="buttonSave" CssClass="btn btn-info" runat="server" Text="Save Price List" Width="120px" OnClick="buttonSave_Click"
                                                    CausesValidation="false" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="buttonCancel" CssClass="btn btn-warning" runat="server" Text="Cancel" OnClick="buttonCancel_Click" Width="120px" />
                                               
                                                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-danger" Text="Close" Width="120px" /></div>
                                            <br />
                                            <asp:Repeater ID="rptPager" runat="server" OnItemCommand="rptPager_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CommandName="Navigate"
                                                        CausesValidation="false"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form pad5 center" style="white-space: nowrap; text-align: center">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rptPager" />
                <asp:AsyncPostBackTrigger ControlID="btnYes" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
