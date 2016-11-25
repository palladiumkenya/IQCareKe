<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionReversal.ascx.cs"
    Inherits="IQCare.Web.Billing.TransactionReversal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="panelFilter" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="form" align="left" style="white-space: nowrap">
                        <label style="padding-left: 10px" id="lblpurpose" runat="server">
                            Filter By:</label>
                        <asp:RadioButtonList ID="radioShowOptions" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="radioShowOptions_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="PENDING" Text="Pending" Selected="True" />
                            <asp:ListItem Value="APPROVED" Text="Approved" />
                            <asp:ListItem Value="REJECTED" Text="Rejected" />
                            <asp:ListItem Value="ALL" Text="All Requests"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="divComponent" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="HTranID" runat="server" />
        <asp:HiddenField ID="HCanRefund" runat="server" />
        <asp:HiddenField ID="HApprovalMode" runat="server" />
        <asp:HiddenField ID="HReversalId" runat="server" />
        <asp:HiddenField ID="HBillId" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HPrintMethodName" runat="server" />
        <asp:HiddenField ID="HPrintURL" runat="server" />
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="pad5 formbg border" width="100%" align="left">
                        <div id="divReversal" class="grid">
                            <div class="rounded">
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 280px; overflow: auto">
                                            <div id="div-reversalGridview" class="GridView whitebg">
                                                <asp:HiddenField ID="ReversalOpenItem" runat="server" />
                                                <asp:GridView ID="gridReversal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="datatable"
                                                    ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="transactionreversalid,transactionid,patientid,billid"
                                                    OnRowCommand="gridReversal_RowCommand" OnRowCreated="gridReversal_RowCreated"
                                                    OnRowDataBound="gridReversal_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt #" />
                                                        <asp:TemplateField HeaderText="Invoice #" InsertVisible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="buttonBillDetails" runat="server" CausesValidation="false" CommandName="BillDetails"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "BillNumber")%>' CommandArgument="<%# Container.DataItemIndex %>"
                                                                    Font-Bold="True"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RequestDate" HeaderText="Date Requested" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                        <asp:BoundField DataField="TransactionAmount" HeaderText="Amount" />
                                                        <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" />
                                                        <asp:TemplateField HeaderText="Reversal Reason" InsertVisible="False">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hrefundable" runat="server" />
                                                                <asp:Label ID="labelReversalReason" runat="server"></asp:Label>
                                                                <span style='display: <%# ShowResonNotes(Eval("ReversalReason")) %>; white-space: nowrap'>
                                                                    <asp:LinkButton ID="linkReversal" runat="server" CausesValidation="false" CommandName="ViewNotes"
                                                                        Text="..." CommandArgument="<%# Container.DataItemIndex %>" Font-Bold="True"></asp:LinkButton></span>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeViewReversalNotes" runat="server" PopupControlID="pnlPopupReversalNotes"
                                                                    TargetControlID="linkReversal" CancelControlID="btnReversalClose" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopupReversalNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #0DA9D0;">
                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        <br />
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        <%# DataBinder.Eval(Container.DataItem, "ReversalReason")%>
                                                                        &nbsp; on &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "RequestDate", "{0:dd-MMM-yyyy HH:mm}")%>&nbsp;&nbsp;by
                                                                        <%# DataBinder.Eval(Container.DataItem, "RequestedBy")%>&nbsp;&nbsp;.Transaction
                                                                        Amount:&nbsp; <b>
                                                                            <%# DataBinder.Eval(Container.DataItem, "TransactionAmount")%>
                                                                        </b>
                                                                        Payment Method:&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "PaymentName")%></b>
                                                                    </div>
                                                                    <div style="padding: 3px;" align="right">
                                                                        <asp:Button ID="btnReversalClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" InsertVisible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelApprovalStatus" runat="server" Text='<%# Bind("ApprovalStatus") %>'></asp:Label>
                                                                <span style='display: <%# ShowReversalNotes(Eval("ApprovalStatus")) %>; white-space: nowrap'>
                                                                    <asp:LinkButton ID="buttonViewNotes" runat="server" CausesValidation="false" CommandName="ViewNotes"
                                                                        Text="..." CommandArgument="<%# Container.DataItemIndex %>" Font-Bold="True"></asp:LinkButton></span>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeViewNotes" runat="server" PopupControlID="pnlPopupNotes"
                                                                    TargetControlID="buttonViewNotes" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopupNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #0DA9D0;">
                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        <br />
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        <%# DataBinder.Eval(Container.DataItem, "ApprovalStatus")%>
                                                                        on &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "ApprovalDate", "{0:dd-MMM-yyyy HH:mm}")%>&nbsp;&nbsp;by
                                                                        <%# DataBinder.Eval(Container.DataItem, "ApprovedBy")%>&nbsp;&nbsp; with comments
                                                                        <b>
                                                                            <%# DataBinder.Eval(Container.DataItem, "ApprovalNotes")%>
                                                                        </b>
                                                                    </div>
                                                                    <div style="padding: 3px;" align="right">
                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField InsertVisible="False" HeaderText="Refunded?">
                                                            <ItemTemplate>
                                                                <%# (Boolean.Parse(DataBinder.Eval(Container.DataItem, "[Refunded]").ToString())) ? "Yes" : "No"%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RefundDate" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HeaderText="Refund Date" />
                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <div style="text-align: center; padding: 10px; white-space: nowrap; display: <%# ShowApproval(Eval("ApprovalStatus")) %>">
                                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" Height="21px" Style="margin-right: 10px"
                                                                        Font-Bold="True" ForeColor="DarkGreen" CommandName="Approve" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                    <asp:Button ID="btnReject" runat="server" Text="Reject" Height="21px" Font-Bold="True"
                                                                        ForeColor="Maroon" CommandName="Reject" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <div style="white-space: nowrap">
                                                                    <span style='display: <%# ShowHideRefund(Eval("ApprovalStatus"),Eval("Refunded"),Eval("Refundable")) %>;
                                                                        white-space: nowrap'>
                                                                        <asp:Button ID="buttonRefund" runat="server" CausesValidation="false" Height="21px"
                                                                            Font-Bold="True" CommandName="Refund" Text="Refund" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:Button></span>
                                                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeRefundPay" runat="server" DisplayModalPopupID="mpeRefundPay"
                                                                        TargetControlID="buttonRefund">
                                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                                    <ajaxToolkit:ModalPopupExtender ID="mpeRefundPay" runat="server" PopupControlID="pnlRefundPopup"
                                                                        TargetControlID="buttonRefund" OkControlID="btnRefundYes" CancelControlID="btnRefundNo"
                                                                        BackgroundCssClass="modalBackground">
                                                                    </ajaxToolkit:ModalPopupExtender>
                                                                    <asp:Panel ID="pnlRefundPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                        width: 300px; border: 3px solid #0DA9D0;">
                                                                        <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                            text-align: center; font-weight: bold;">
                                                                            Refund the cash
                                                                        </div>
                                                                        <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                            <table border="0" cellpadding="15" cellspacing="0" style="width: 100%; height: 18px">
                                                                                <tr>
                                                                                    <td style="width: 48px" valign="middle" align="center">
                                                                                        <img src="../Images/mb_question.gif" alt="" width="32" height="32" />
                                                                                    </td>
                                                                                    <td style="width: 100%; padding-left: 20px" valign="middle" align="left">
                                                                                        Confirm Refund Cash ?<br />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div style="padding: 3px;" align="center">
                                                                            <asp:Button ID="btnRefundYes" runat="server" Text="Refund" ForeColor="DarkGreen" />
                                                                            <asp:Button ID="btnRefundNo" runat="server" Text="Cancel" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                                                                    </asp:Panel>
                                                                    <span style='display: <%# ShowPrintRefund(Eval("Refunded")) %>; white-space: nowrap'>
                                                                        <asp:Button ID="refundPrint" runat="server" CausesValidation="false" CommandName="PrintRefund"
                                                                            Text="Print Refund" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue" />
                                                                    </span>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
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
            </tbody>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="radioShowOptions" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnReject" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="gridReversal" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="billItemPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowItems" runat="server" Text="" Width="60px" Style="display: none" />
        <asp:Panel ID="divBillItems" runat="server" Style="display: none; width: 680px; border: solid 1px #808080;
            background-color: #F0F0F0; z-index: 15000">
            <asp:Panel ID="divItemTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 18px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                    <tr>
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px;">
                            <span style="font-weight: bold;">
                                <asp:Label ID="labelItemTitle" runat="server">Bill Details & Items in the Bill</asp:Label></span>
                        </td>
                        <td style="width: 5px; height: 19px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form">
                            <table width="100%">
                                <tr>
                                    <td style="font-weight: bold">
                                        Bill Number:
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillNumber" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        BillDate:
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold">
                                        Total Bill Amount:
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillAmount" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        Outstanding Amount:
                                    </td>
                                    <td>
                                        <asp:Label ID="labelUnPaidAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold">
                                        Billed By:
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBilledBy" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        Bill Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="labelBillStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: <% = NeedShowPatient %>">
                                    <td style="font-weight: bold">
                                        Patient Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblname" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        Patient Number:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td class="form" valign="top" style="width: 100%">
                                        <div>
                                            <h2 class="forms" align="left">
                                                Items in the bill</h2>
                                        </div>
                                        <div class="mid" style="height: 280px; overflow: auto">
                                            <div id="div-payItems" class="GridView whitebg">
                                                <asp:GridView ID="gridBillItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable" DataKeyNames="billItemID,billid"
                                                    Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                    ShowFooter="False" ShowHeaderWhenEmpty="True" Width="100%" CaptionAlign="Top"
                                                    Visible="true" BorderStyle="Solid">
                                                    <Columns>
                                                        <asp:BoundField DataField="BillItemDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Description" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                        <asp:BoundField DataField="sellingprice" HeaderText="Price" DataFormatString="{0:N}" />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:N}" />
                                                    </Columns>
                                                    <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />
                                                    <RowStyle CssClass="gridrow" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                text-align: center; padding-top: 5px; padding-bottom: 5px">
                <asp:Button ID="buttonCloseDetails" runat="server" Text="Close" Width="80px" Style="border: solid 1px #808080;" />
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="billDetailsPopup" runat="server" BehaviorID="TRmpeBID01"
            TargetControlID="btnShowItems" PopupControlID="divBillItems" BackgroundCssClass="modalBackground"
            CancelControlID="buttonCloseDetails" DropShadow="true" PopupDragHandleControlID="divItemTitle">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gridReversal" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnRaisePopup" runat="server" Text="" Width="60px" Style="display: none" />
        <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 380px;
            border: solid 1px #808080; background-color: #F0F0F0">
            <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 18px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                    <tr>
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px;">
                            <span style="font-weight: bold;">
                                <asp:Label ID="labelParamTitle" runat="server">Add Editing Item</asp:Label></span>
                        </td>
                        <td style="width: 5px; height: 19px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%" cellpadding="15" cellspacing="0" style="width: 100%; height: 18px">
                <tbody>
                    <asp:Panel ID="divReason" runat="server" Style="margin-top: 0px; margin-bottom: 5px;
                        border: solid 0px #C00000" HorizontalAlign="left">
                        <tr>
                            <td style="width: 48px" valign="middle" align="center" rowspan="2">
                                <img src="../images/mb_question.gif" alt="" width="32" height="32" />
                            </td>
                            <td style="width: 100%;" valign="middle" align="left">
                                <asp:Label ID="lblRejectError" runat="server" Style="font-weight: bold; color: #800000"
                                    Text="">* Please provide the reason *</asp:Label>
                                <asp:HiddenField ID="HRefundable" runat="server" />
                                <asp:TextBox ID="textReason" Rows="4" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="divCashRefund" runat="server" Style="margin-bottom: 5px;" HorizontalAlign="left">
                        <tr style="display: <%= sApproval %>">
                            <td>
                                <asp:Label ID="labelCashRefund" runat="server" Style="font-weight: bold; color: #800000"
                                    Text="">Have you refunded the cash to the client?</asp:Label>
                                <br />
                                <asp:RadioButtonList ID="rblOptionRefund" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="flow">
                                    <asp:ListItem Value="No" Text="No, to be given by cashier" Selected="True" />
                                    <asp:ListItem Value="Yes" Text="Yes" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </asp:Panel>
                </tbody>
            </table>
            <asp:Panel ID="divApproval" runat="server" Visible="true" Style="text-align: center;
                padding: 10px; width: 380px">
                <asp:Button ID="btnApprove" runat="server" Text="Approve" Height="21px" Style="margin-right: 10px"
                    Font-Bold="True" ForeColor="DarkGreen" OnClick="btnApprove_Click" />
                <asp:Button ID="btnReject" runat="server" Text="Reject" Height="21px" Font-Bold="True"
                    ForeColor="Maroon" OnClick="btnReject_Click" />
                <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" Height="21px" Style="margin-left: 10px"
                    Font-Bold="True" ForeColor="DarkBlue" /></asp:Panel>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="TRmpeBID02"
            TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
            CancelControlID="btnActionCancel" DropShadow="true" PopupDragHandleControlID="divTitle">
        </ajaxToolkit:ModalPopupExtender>
        <!-- Confirmation Popup -->
        <asp:Button ID="btn" runat="server" Style="display: none" />
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
                        <asp:Image ID="imgNotice" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px"
                            Width="32px" />
                    </td>
                    <td style="width: 100%;" valign="middle" align="center">
                        <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                text-align: center; padding-top: 5px; padding-bottom: 5px">
                <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;" />
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
            PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
            PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gridReversal" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
