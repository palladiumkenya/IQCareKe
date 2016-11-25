<%@ Control Language="C#" AutoEventWireup="true" 
    Inherits="IQCare.Web.Billing.PatientDeposits" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnRaisePopup" runat="server" Text="New Deposit" Width="80px" Style="display: block;margin-bottom:5px"
            class="greenbutton" />
        <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 380px;
            border: solid 1px #808080; background-color: #6699FF" DefaultButton="btnNewDeposit">
            <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 18px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                    <tr>
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px;">
                            <span style="font-weight: bold;">
                                <asp:Label ID="labelParamTitle" runat="server">New Cash Deposit</asp:Label></span>
                        </td>
                        <td style="width: 5px; height: 19px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%" cellpadding="15" cellspacing="0" style="width: 100%; height: 18px; background-color:#CCFFFF">
                <tbody>
                    <tr>
                        <td style="width: 48px" valign="middle" align="center" rowspan="2">
                            <img src="../images/mb_question.gif" alt="" width="32" height="32" />
                        </td>
                        <td valign="middle" align="left" style="white-space: nowrap">
                            <asp:Label ID="lblDepositAmount" runat="server" Style="font-weight: bold; color: #800000"
                                Text="">* Deposit Amount *</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="textAmount" runat="server" AutoCompleteType="None" AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteAmountToPay" runat="server" TargetControlID="textAmount"
                                FilterType="Numbers" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:Panel ID="divApproval" runat="server" Visible="true" Style="text-align: center;
                padding: 10px; width: 360px">
                <asp:Button ID="btnNewDeposit" runat="server" Text="Save" Height="21px" Style="margin-right: 10px"
                    Font-Bold="True" OnClick="btnNewDeposit_Click" />
                <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" Height="21px" Style="margin-left: 10px"
                    Font-Bold="True" /></asp:Panel>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="PDmpeBID01"
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
            PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True" BehaviorID="PDmpeBID02"
            PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
     <asp:AsyncPostBackTrigger ControlID="btnNewDeposit" EventName="Click" />
     <asp:AsyncPostBackTrigger ControlID="btnRefundYes" EventName="Click" />
      <asp:AsyncPostBackTrigger ControlID="buttonRefund" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="divComponent" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="HCanReceive" runat="server" />
        <asp:HiddenField ID="HCanRefund" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HPrintMethodName" runat="server" />
        <asp:HiddenField ID="HPrintURL" runat="server" />
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td style="font-weight: bold; height: 30px;">
                                    Date of Last Deposit:
                                </td>
                                <td>
                                    <asp:Label ID="labelDepositDate" runat="server" Height="30px"></asp:Label>
                                </td>
                                <td rowspan="4" align="center" width="25%">
                                    <asp:Button ID="buttonViewTransactions" runat="server" Text="View Transactions" Height="100px"
                                        Font-Bold="True" ForeColor="InfoText" Width="100%" Enabled="True" OnClick="buttonViewTransactions_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; height: 30px;">
                                   Last Amount Deposited:
                                </td>
                                <td>
                                    <asp:Label ID="labelDepositedAmount" runat="server" Height="30px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; height: 30px;">
                                    Received By:
                                </td>
                                <td>
                                    <asp:Label ID="labelReceivedBy" runat="server" Height="30px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; height: 30px;">
                                    Total Amount Available:
                                </td>
                                <td align="left" valign="middle">
                                    <div style="white-space: nowrap;vertical-align:middle; text-align:left">
                                        <asp:Label ID="labelTotalAvailable" runat="server" Height="30px" Text="0.0"></asp:Label>&nbsp;&nbsp;
                                        <span style='display: <%# ShowHideRefund() %>; white-space: nowrap'>
                                            <asp:Button ID="buttonRefund" runat="server" CausesValidation="false" Height="30px" OnClick="btnRefundYes_Click" 
                                                Font-Bold="True" CommandName="Refund" Text="Return" ForeColor="Blue" ToolTip="Return the available deposit to the client">
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
                                                Return the entire available deposit in cash.
                                            </div>
                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                <table border="0" cellpadding="15" cellspacing="0" style="width: 100%; height: 18px">
                                                    <tr>
                                                        <td style="width: 48px" valign="middle" align="center">
                                                            <img src="../Images/mb_question.gif" alt="" width="32" height="32" />
                                                        </td>
                                                        <td style="width: 100%; padding-left: 20px" valign="middle" align="left">
                                                            Confirm deposit return ?<br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="padding: 3px;" align="center">
                                                <asp:Button ID="btnRefundYes" runat="server" Text="Return" ForeColor="DarkGreen" />
                                                <asp:Button ID="btnRefundNo" runat="server" Text="Cancel" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnNewDeposit" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnRefundYes" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="transactionPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnShowTransactions" runat="server" Text="" Width="60px" Style="display: none" />
        <asp:Panel ID="divTransactions" runat="server" Style="display: none; width: 680px;
            border: solid 1px #808080; background-color: #F0F0F0; height: 380px; overflow: auto">
            <asp:Panel ID="divItemTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                cursor: move; height: 18px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                    <tr>
                        <td style="width: 5px; height: 19px;">
                        </td>
                        <td style="width: 100%; height: 19px;">
                            <h2 class="forms" align="left">
                                <asp:Label ID="labelItemTitle" runat="server">Patient Deposit Transactions</asp:Label></h2>
                        </td>
                        <td style="width: 5px; height: 19px;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table cellspacing="6" cellpadding="0" width="680px" border="0">
                <tbody>                    
                    <tr>
                        <td class="pad5 formbg border">
                            <div id="divDeposits" class="grid" style="width: 100%; margin-top: 8px;">
                                <div class="rounded">
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 280px; overflow: auto">
                                                <div id="div-depositGridview" class="GridView whitebg">
                                                    <asp:GridView ID="gridTransaction" runat="server" AllowSorting="False" AutoGenerateColumns="False"
                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive" DataKeyNames="transactionid"
                                                        Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                        ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" BorderStyle="Solid">
                                                        <Columns>
                                                            <asp:BoundField DataField="ReferenceNumber" HeaderText="Ref Number" />
                                                            <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                            <asp:TemplateField InsertVisible="False" ShowHeader="True" HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <div style='text-align: <%# FormatAmount(Eval("TransactionType")) %>'>
                                                                        <%# FormatAmountDisplay(Eval("TransactionType"),DataBinder.Eval(Container.DataItem, "Amount", "{0:N}"))%>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ReceivedBy" HeaderText="Received By" />
                                                            <asp:BoundField DataField="TransactionDescription" HeaderText="Description" />
                                                            <asp:TemplateField InsertVisible="False" ShowHeader="False" Visible="false">
                                                                <ItemTemplate>
                                                                    <div style="white-space: nowrap">
                                                                        <span style='white-space: nowrap'>
                                                                            <asp:Button ID="transactionPrint" runat="server" CausesValidation="false" CommandName="PrintTransaction"
                                                                                Text="Print" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue" /></span>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />
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
            <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                text-align: center; padding-top: 5px; padding-bottom: 5px">
                <asp:Button ID="buttonCloseDetails" runat="server" Text="Close" Width="80px" Style="border: solid 1px #808080;" />
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="depositDetailsPopup" runat="server" BehaviorID="PDmpeBID03"
            TargetControlID="btnShowTransactions" PopupControlID="divTransactions" BackgroundCssClass="modalBackground"
            CancelControlID="buttonCloseDetails" DropShadow="true" PopupDragHandleControlID="divItemTitle">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnNewDeposit" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnRefundYes" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="buttonViewTransactions" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
