<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="IQCare.Web.Scheduler.AppointmentMain"
    Title="Untitled Page" CodeBehind="~/Scheduler/frmScheduler_AppointmentMain.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Scheduler/PatientAppointmentControl.ascx" TagName="Schedule"
    TagPrefix="app" %>
<%@ Register Src="~/Scheduler/AppointmentList.ascx" TagName="List" TagPrefix="app" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
   
   <div class="row">
        
        <script language="javascript" type="text/javascript">
          
        </script>
        <div class="row">
            <div class="col-md-6 pull-left">
                 <span id="Span1" class="text-capitalize pull-left glyphicon-text-size= fa-2x" runat="server">
                            <i class="fa fa-calendar fa-2x" aria-hidden="true"></i> Appointments </span>
            </div>
        </div><!-- .row --><br />
<%--        <h1 class="nomargin" style="padding-left: 10px;">
            Appointments</h1>--%>
        
        
        
            <div class="panel panel-default">

                <div class="panel-heading"><span class="pull-left">Set / View Patient Appointments</span><br /></div>

                <div class="panel-body">

                     <div class="row">
                         <div class="col-md-4 pull-left">
                             <div class="col-md-3 pull-left">
                                  <asp:Button ID="btnExcel" CssClass="btn btn-warning" runat="server" Font-Bold="true" OnClick="btnExcel_Click" Text="Export to Excel" />
                            </div>
                         </div><!-- .col-md-4-->

                         <div class="col-md-4"></div><!-- .col-md-4-->
                         
                         <div class="col-md-4">
                              <div class="col-md-6 pull-right">
                                   <asp:Button ID="btnNewAppointment" CssClass="btn btn-info col-md-8" runat="server" Font-Bold="true" onclick="btnNewAppointment_Click" Text="New Appointment" />
                              </div>
                         </div><!-- .col-md-4-->

                     </div><!-- .row -->

<%--                     <div class="row">
                        <div class="col-md-12"><hr /></div> 
                    </div>--%>

                    <div class="row">
                         <%--<table border="0" cellpadding="0" cellspacing="6">
                            <tr>
                                <td align="left" width="50%">
                                   <asp:Button ID="btnExcel" CssClass="btn btn-warning" runat="server" Font-Bold="true" OnClick="btnExcel_Click"
                                        Text="Export to Excel" />
                                </td>
                                <td align="right" width="50%">
                                   <%--  <asp:Button ID="btnNewAppointment" CssClass="btn btn-info" runat="server" Font-Bold="true" 
                                        Text="New Appointment" onclick="btnNewAppointment_Click" />
                                </td>
                            </tr>
                        </table>--%>

                        <asp:UpdatePanel runat="server" ID="divErrorComponent" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="alert aler-danger"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="HFormName" runat="server" />
                                    <asp:HiddenField ID="HModuleID" runat="server" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <app:List ID="AppGrid" runat="server" />
                                
                                <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                <asp:PostBackTrigger ControlID="btnExcel" />
                                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="divScheduleComponet" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <app:Schedule ID="SchedulePatient" runat="server" OpenMode="NEW" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
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

                   <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
                        <ProgressTemplate>
                            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                                <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                                    background-color: #FFFFC0; width: 110px; height: 24px;">
                                    <tr>
                                        <td style="white-space: nowrap">
                                            <span style="white-space: nowrap">&nbsp;Processing....<asp:Image runat="server" ID="imggif"
                                                ImageUrl="~/Images/loading.gif" ImageAlign="AbsMiddle" Style="white-space: nowrap" /></span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                </div><!-- .panel-body -->
            </div><!-- .panel-default-->
                  
    </div><!-- .row -->
</asp:Content>
