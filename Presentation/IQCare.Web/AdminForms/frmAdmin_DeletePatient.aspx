<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="IQCare.Web.Admin.DeletePatient" Title="Untitled Page"
    CodeBehind="frmAdmin_DeletePatient.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/PatientFinder.ascx" TagName="PatientFinder" TagPrefix="uc1" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Delete Patient</h1>
        <div class="center" style="padding: 5px;">
            <uc1:PatientFinder ID="ctrlFindPatient" runat="server" FilterByServiceLines="False"
                IncludeEnrollement="False" AutoLoadRecords="False" NumberofRecords="50" CanAddPatient="False" />
            <asp:Button ID="buttonProxy" runat="server" Text="" Style="display: none" OnClick="buttonProxy_Click" />
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form" align="center">
                            <asp:Button ID="btnBack" Text="Back" runat="server" />
                            <asp:Button ID="theBtn" Text="OK" CssClass="textstylehidden" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:UpdatePanel ID="divActionComponent" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <ContentTemplate>
                    <ajaxToolkit:ModalPopupExtender ID="mpePatientDelete" runat="server" PopupControlID="pnlPopup"
                        TargetControlID="buttonRaiseItemPopup" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:HiddenField ID="HPatientId" runat="server" />
                    <asp:HiddenField ID="HLocationId" runat="server" />
                    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                    <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                        width: 300px; border: 3px solid #0DA9D0;">
                        <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                            text-align: center; font-weight: bold;">
                            Delete Patient Confirmation
                        </div>
                        <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                            This action cannot be reversed.<br />
                            <asp:Label ID="labelDeleteText" runat="server"></asp:Label>
                        </div>
                        <div style="padding: 3px;" align="right">
                            <asp:Button ID="btnYes" runat="server" Text=" Yes " ForeColor="DarkGreen" OnClick="DeleteSelectedPatient" /><asp:Button
                                ID="btnNo" runat="server" Text=" No " ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="buttonProxy" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="notificationPanel" runat="server">
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
                                OnClick="btnOkAction_Click" CausesValidation="false" />
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
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
