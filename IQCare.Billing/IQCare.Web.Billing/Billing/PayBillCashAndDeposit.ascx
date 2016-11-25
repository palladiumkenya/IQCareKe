<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayBillCashAndDeposit.ascx.cs"
    Inherits="IQCare.Web.Billing.PayBillCashAndDeposit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="notificationPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
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
                                <asp:Label ID="lblNotice" runat="server">Pay Bill</asp:Label></span>
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
                <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                    OnClick="btnOkAction_Click" /></div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
            PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True" BehaviorID="PBCmpeBID01"
            PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel runat="server" ID="upPayPanel" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
     <asp:HiddenField ID="HTran" runat="server" />
        <asp:HiddenField ID="HBillAmount" runat="server" />
        <asp:HiddenField ID="HBillID" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HDAmountDue" runat="server" />
        <asp:HiddenField ID="HDAmountToPay" runat="server" />
        <asp:HiddenField ID="HPayMethodName" runat="server" />
        <asp:HiddenField ID="HPayMethodID" runat="server" />
         <asp:HiddenField ID="HPayMode" runat="server" />
        <asp:Panel ID="panelCompute" runat="server" Style="display: block; width: 480px;" DefaultButton="buttonCompute">
            <table width="100%">
                <tr>
                    <td valign="top" style="width: 75%">
                        <table style="width: 100%">
                            <tr>
                                <td align="right" style="padding-left: 10px; font-weight: bold">
                                    Payment Mode:
                                </td>
                                <td>
                                    <asp:Label Style="padding-left: 10px" ID="labelPaymentMode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-left: 10px; font-weight: bold">
                                    Ref No:
                                </td>
                                <td>
                                    <asp:TextBox ID="textReferenceNo" runat="server" Width="180px" AutoComplete="Off" MaxLength="50"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="textReferenceNo" FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters"
                                        ValidChars=".-" />
                                </td>
                            </tr>
                            <asp:Panel runat="server" ID="panelDeposit">
                                <tr>
                                    <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                        Available Deposit:
                                    </td>
                                    <td>
                                        <asp:Label Style="padding-left: 10px" ID="labelAvailableDeposit" runat="server" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                    Outstanding Amount:
                                </td>
                                <td>
                                    <asp:Label Style="padding-left: 10px" ID="labelAmountOutstanding" runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                    Discount:
                                </td>
                                <td title="Discount plan">
                                     <asp:Label Style="padding-left: 10px" ID="labelDiscount" runat="server" Text="None"/>                                    
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                    Discounted Amount:
                                </td>
                                <td>
                                    <asp:Label Style="padding-left: 10px" ID="labelDiscountedAmountToPay" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                    Amount to pay:
                                </td>
                                <td title="The amount of the bill to pay">
                                    <asp:TextBox ID="textAmountToPay" runat="server" Width="180px" AutoComplete="Off"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteAmountToPay" runat="server" TargetControlID="textAmountToPay"
                                        FilterType="Numbers, Custom" ValidChars="." />
                                    <asp:RangeValidator ID="rgAmountToPay" runat="server" ControlToValidate="textAmountToPay"
                                        Type="Double" MinimumValue="0" MaximumValue="10" ErrorMessage="The value should be between 0 and  the outstanding amount"
                                        Display="Dynamic" Enabled="True" />
                                    <asp:Label Style="padding-left: 10px; font-weight: bold;" ID="lblAmountToPay" runat="server"
                                        Visible="false" />
                                </td>
                            </tr>
                            
                            <asp:Panel runat="server" ID="panelTenderedAmount">
                                <tr>
                                    <td align="right" style="padding-left: 10px; font-weight: bold; white-space: nowrap">
                                        Tendered Amount:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textTenderedAmount" runat="server" Width="180px" AutoComplete="Off"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteTenderedAmount" runat="server" TargetControlID="textTenderedAmount"
                                            FilterType="Numbers, Custom" ValidChars="." />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="textAmountToPay"
                                            ControlToValidate="textTenderedAmount" Display="Dynamic" ErrorMessage="Value should be greater than or equal to Amount to pay"
                                            Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </td>
                    <td align="center" width="25%" style="height: 100%">
                        <table width="100%" style="height: 100%">
                            <tr>
                                <td valign="middle">
                                    <asp:Button ID="buttonCompute" runat="server" Text="Compute" Height="70px"
                                        Style="white-space: nowrap" Font-Bold="True" ForeColor="InfoText" OnClick="buttonCompute_Click"
                                        Width="100%" Enabled="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="buttonStepBack" runat="server" Text="Cancel" Height="70px" Style="white-space: nowrap"
                                        Font-Bold="True" ForeColor="InfoText" Width="100%" Enabled="True" 
                                        CausesValidation="False" onclick="buttonStepBack_Click" 
                                        UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr style="border-width: 1px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelFinish" runat="server" Style="width: 480px;" Visible="false" DefaultButton="btnFinish">
            <table width="100%">
                <tr>
                    <td valign="top" style="width: 75%">
                        <table style="width: 100%">
                            <tr>
                                <td align="right">
                                    <label style="padding-left: 10px" id="label1">
                                        Total Bill:</label>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="lblTotalBill" runat="server">
                                        0:</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="padding-left: 10px; font-weight: bold">Amount to pay:</span>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="labelAmountTopay" runat="server">
                                        none</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="padding-left: 10px; font-weight: bold">Discount:</span>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="labelDiscountGiven" runat="server">
                                        none</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="padding-left: 10px; font-weight: bold">Payment Type:</span>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="labelPaymentType" runat="server">
                                        none</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <label style="padding-left: 10px" id="labelAmountTendered" runat="server">
                                        Amount Tendered:</label>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="lblPaid" runat="server">
                                        0:</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <label style="padding-left: 10px" id="lblMoneyType" runat="server">
                                        Change:</label>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="lblChange" runat="server">
                                        0:</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="padding-left: 10px; font-weight: bold">Amount Due:</span>
                                </td>
                                <td>
                                    <label style="padding-left: 10px" id="labelAmountDue" runat="server">
                                        0:</label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:CheckBox ID="ckbPrintReciept" MaxLength="20" runat="server" Text="Print Receipt"
                                        Checked="True" Visible="True" TextAlign="Left" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center" width="25%" style="height: 100%">
                        <table width="100%" style="height: 100%">
                            <tr>
                                <td valign="middle">
                                    <asp:Button ID="btnFinish" MaxLength="20" runat="server" Text="Finish" OnClick="btnFinish_Click"
                                        Height="70px" Width="100%" Enabled="false" Font-Bold="True" ForeColor="InfoText"/>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" width="20%">
                                    <asp:Button ID="btnCancel" MaxLength="20" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        Width="100%" Height="70px" Font-Bold="True" ForeColor="InfoText"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>               
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnFinish" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="buttonCompute" EventName="Click" />
        <%--  <asp:AsyncPostBackTrigger ControlID="ddlPaymentMode" EventName="SelectedIndexChanged" />--%>
    </Triggers>
</asp:UpdatePanel>
