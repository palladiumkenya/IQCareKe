<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmBilling_Discount.aspx.cs" Inherits="IQCare.Web.Billing.Discount" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <hr />
    <div style="padding-top: 18px;">
        <h2 class="forms" align="left">
            Discount Plans</h2>
    </div>
    <div class="border center formbg">
        <asp:UpdatePanel ID="upError" runat="server">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                    border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <%--<div class="center" style="padding: 5px;">
            <div class="border center">--%>
        <table width="100%" border="0" cellpadding="0" cellspacing="6">
            <tbody>
                <tr>
                    <td class="border pad5 formbg">
                        <asp:UpdatePanel runat="server" ID="updatePanelMasterItem" 
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid">
                                                    <div id="divGridData" class="GridView whitebg" style="cursor: pointer; height: 280px;
                                                        overflow: auto">
                                                        <asp:GridView ID="gridDiscountPlan" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                            Width="100%" PageIndex="1" BorderWidth="0px" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                            CellPadding="0" DataKeyNames="PlanID" OnRowCommand="gridDiscountPlan_RowCommand"
                                                            OnRowDataBound="gridDiscountPlan_RowDataBound" OnSelectedIndexChanging="gridDiscountPlan_SelectedIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField DataField="Name" HeaderText="Discount Name" ReadOnly="true" SortExpression="Name">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Rate" HeaderText="Discount Rate" ReadOnly="true" SortExpression="Rate"
                                                                    DataFormatString="{0:P2}">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                               <%-- <asp:TemplateField HeaderText="Payment Method" SortExpression="Payment Method">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="labelPayMethod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountedPayMethod.Name") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hPayMethodID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountedPayMethod.ID") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ReadOnly="true" SortExpression="StartDate"
                                                                    DataFormatString="{0:dd-MMM-yyyy}">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ReadOnly="true" SortExpression="EndDate"
                                                                    DataFormatString="{0:dd-MMM-yyyy}">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Status" SortExpression="Active">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="labelStatus" runat="server" Text='<%# (Boolean.Parse(Eval("Active").ToString())) ? "Active" : "InActive" %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="buttonSubmit" EventName="Command" />
                                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CausesValidation="False" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                            CausesValidation="False" />
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel ID="DiscountPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Button ID="btnShowDiscount" runat="server" Text="" Width="60px" Style="display: none" />
                <asp:Panel ID="divDiscount" runat="server" Style="display: none; width: 680px; border: solid 1px #808080;
                    background-color: #6699FF" DefaultButton="buttonSubmit">
                    <asp:Panel ID="divDiscountTitle" runat="server" Style="border: solid 1px #808080;
                        margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold;">
                                        <asp:Label ID="labelDiscountTitle" runat="server">Discount Plan Details</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table cellpadding="1" cellspacing="1" border="0" width="680px" style="border: solid 1px #808080;
                        background-color: #CCFFFF; margin-bottom: 10px">
                       <tr>
                            <td colspan="2" align="left">
                                <i>All of the fields in this section are required.</i>
                            </td>
                        </tr>
                        <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="true">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <hr class="forms">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold;">
                                 Name:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="textDiscountName" runat="server" Width="200px" AutoComplete="false"
                                    MaxLength="100"></asp:TextBox>
                                <asp:HiddenField ID="prevDiscountName" runat="server" />
                                <asp:HiddenField ID="currentID" runat="server" Value="-1" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textDiscountName"
                                    ValidChars="&-,%:; " />
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td align="left" style="font-weight: bold;">
                                Payment Method:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="180px" ViewStateMode="Enabled">
                                </asp:DropDownList>
                                <asp:Label ID="labelPaymentMethod" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="prevPaymentID" runat="server" Value="" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold;">
                                 Rate (%):
                            </td>
                            <td align="left">
                                <asp:TextBox ID="textRate" runat="server" Width="180px" AutoComplete="Off"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FteRate" runat="server" TargetControlID="textRate"
                                    FilterType="Numbers, Custom" ValidChars="." />
                                <asp:RangeValidator ID="rgRate" runat="server" ControlToValidate="textRate" Type="Double"
                                    MinimumValue="0" MaximumValue="100" ErrorMessage="The value should be more than 0 and  less than 100"
                                    Display="Dynamic" Enabled="True" />
                                <asp:HiddenField ID="prevRate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Start Date:
                            </td>
                            <td align="left" style="white-space: nowrap; vertical-align: middle">
                                <asp:TextBox ID="textStartDate" runat="server" Width="230px" MaxLength="12" AutoComplete="Off"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="hspace: 3; width: 22;
                                    height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textStartDate"
                                    PopupButtonID="Image1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_past_dates" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="textStartDate"
                                    ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="textStartDate"
                                    Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                    ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                End Date:
                            </td>
                            <td align="left" style="white-space: nowrap; vertical-align: middle">
                                <asp:TextBox ID="textEndDate" runat="server" Width="230px" MaxLength="12" AutoComplete="false"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="ImageButton1" Height="22" Style="width: 22; height: 22"
                                    ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="textEndDate"
                                    PopupButtonID="ImageButton1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_past_dates" />
                                <br />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="textEndDate"
                                    Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                    ClearMaskOnLostFocus="True" CultureName="en-GB" Mask="99-LLL-9999" >
                                </ajaxToolkit:MaskedEditExtender><ajaxToolkit:MaskedEditValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="textEndDate"
                                ControlExtender="MaskedEditExtender1"
                                IsValidEmpty ="true"
                                    ErrorMessage="*" Display="None" 
                                    ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$">
                                    </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold;">
                                 Status:
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px;
                                padding-bottom: 5px">
                                <asp:Button ID="buttonSubmit" CssClass="btn btn-info" runat="server" Text="Save" Width="120px" OnClick="buttonSubmit_Click"
                                    CausesValidation="False" />
                                <asp:Button ID="buttonClose" runat="server" CssClass="btn btn-btn-danger" Text="Close" Width="120px" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="discountPopup" runat="server" BehaviorID="ptpBehavior"
                    TargetControlID="btnShowDiscount" PopupControlID="divDiscount" BackgroundCssClass="modalBackground"
                    CancelControlID="buttonClose" DropShadow="true" PopupDragHandleControlID="divDiscountTitle">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridDiscountPlan" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="gridDiscountPlan" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    <%--    <asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: White">
                                        <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="width: 48px" valign="middle" align="center">
                                <asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px"
                                    Width="32px" />
                            </td>
                            <td style="width: 100%;" valign="middle" align="center">
                                <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                        text-align: center; padding-top: 5px; padding-bottom: 5px">
                        <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                            OnClick="btnOkAction_Click" CausesValidation="False" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel--%>>
        <%--<asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                    <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                        background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                            </td>
                            <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px;
                                height: 22px; padding-left: 5px">
                                Processing....
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </div>
</asp:Content>
