<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="IQCare.Web.Scheduler.AppointmentNewHistory"
    Title="Untitled Page" CodeBehind="~/Scheduler/frmScheduler_AppointmentNewHistory.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Src="~/Scheduler/PatientAppointmentControl.ascx" TagName="Schedule"
    TagPrefix="app" %>
<%@ Register Src="~/Scheduler/AppointmentList.ascx" TagName="List" TagPrefix="app" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <style>
        z-index:1000</style>
    <script type="text/javascript">
        function CheckDate(vDateName) {

            var mYear = vDateName.value.substr(7, 4)
            if (mYear != '') {
                if (mYear < 1930) {
                    alert("Selected year should be between 1930 and Current year. Reenter..");
                    vDateName.value = "";
                    vDateName.focus();

                }
            }
        }
    </script>
    <div class="row">
        <asp:HiddenField ID="HPatientId" runat="server" />
        <asp:Button ID="btnNewAppointment" runat="server" OnClick="btnNewAppointment_Click"
            Text="New Appointment" CssClass="btn btn-info pull-right" />
    </div>
    <!-- .row -->
    <%-- <div class="nomargin" style="padding-left: 10px; padding-top: 2px; padding-bottom: 10px;">
       <asp:HiddenField ID="HPatientId" runat="server" />
        <table border="0" cellpadding="0" cellspacing="6" width="100%">
            <tr>
                <td align="left" width="50%">
                </td>
                <td align="right" width="50%">
                    <%--<asp:Button ID="btnNewAppointment" runat="server" Font-Bold="true" OnClick="btnNewAppointment_Click"
                        Text="New Appointment" CssClass="greenbutton"/>
                </td>
            </tr>
        </table>
    </div>--%>
    <div class="row">
        <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                    HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                    <asp:HiddenField ID="HFormName" runat="server" />
                    <asp:HiddenField ID="HModuleID" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- .row -->
    <div class="row" style="padding-top: 5px">
        <div class="border formbg">
            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <app:List ID="AppGrid" runat="server" />
                    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divScheduleComponet" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <app:Schedule ID="SchedulePatient" runat="server" OpenMode="NEW" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNewAppointment" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <%--<asp:UpdatePanel ID="notificationPanel" runat="server">
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
        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                    <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                        background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="white-space: nowrap">
                                <span style="white-space: nowrap">&nbsp;Processing....<asp:Image runat="server" ID="imggif"
                                    ImageUrl="~/Images/loading.gif" ImageAlign="AbsMiddle" Style="white-space: nowrap" /></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </div>
    <!-- .row -->
</asp:Content>
