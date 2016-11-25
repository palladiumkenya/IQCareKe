<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="NewAdmission.aspx.cs" Inherits="IQCare.Web.WardAdmission.NewAdmission"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register Src="~/PatientFinder.ascx" TagName="PatientFinder" TagPrefix="pf" %>
<%@ Register Src="~/WardAdmission/AdmitPatient.ascx" TagName="PatientWardAdmission" TagPrefix="paf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <h2 class="forms" align="left">
        Patient Ward Admission</h2>
    <div class="rounded">
        <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                    HorizontalAlign="Left" Visible="true">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                    <asp:HiddenField ID="HFormName" runat="server" />                   
                    <asp:HiddenField ID="HModuleID" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divPatientComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <pf:PatientFinder ID="FindPatient" runat="server" FilterByServiceLines="False" IncludeEnrollement="False"
                    AutoLoadRecords="False" NumberofRecords="50" CanAddPatient="False" />
                <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divNewAdmission" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <paf:PatientWardAdmission ID="ctrlAdmit" runat="server" OpenMode="NEW" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px;
                            background-color: #6699FF">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: White">
                                        <asp:Label ID="lblNotice" runat="server">Admission</asp:Label></span>
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
                        <asp:Button ID="btnOkAction" runat="server" Text="Close" Width="120px" Style="border: solid 1px #808080;" />&nbsp;&nbsp;&nbsp;<asp:Button
                            ID="btnExit" runat="server" Text="Close" Width="80px" Style="border: solid 1px #808080;
                            display: none" /></div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                    OkControlID="btnOkAction">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
         <div style="text-align: center; padding: 10px; white-space: nowrap; border: solid 1px #808080;"
            class="form pad5 center">
            <asp:Button ID="btnBack" runat="server" Text="Close" />
        </div>
    </div>
    <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;
                background-color: Gray; filter: alpha(opacity=50); opacity: 0.7; z-index: 120;
                moz-opacity: 0.5; khtml-opacity: .5">
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
    </asp:UpdateProgress>
</asp:Content>
