<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PatientAppointmentControl.ascx.cs"
    Inherits="IQCare.Web.Scheduler.PatientAppointmentControl" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<script language="javascript" type="text/javascript">

</script>
<asp:Panel ID="AppointmentPopup" runat="server" Style="display: none; width: 700px"
    Width="700px" DefaultButton="buttonSave">
    <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <asp:Panel ID="Panel1" runat="server">
                    <i class="fa fa-clock-o pul-left" aria-hidden="true"></i><span class=""></span>Appointment
                    Form
                </asp:Panel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <label class="">
                            <strong>Patient Name:</strong></label><asp:Label ID="lblname" runat="server"></asp:Label>
                    </div>
                    <!-- .col-md-4-->
                    <div class="col-md-2">
                        <b>Age:</b><asp:Label ID="lblAge" runat="server"></asp:Label>
                    </div>
                    <!-- .col-md-2-->
                    <div class="col-md-2">
                        <b>Sex:</b>
                        <asp:Label ID="lblSex" runat="server"></asp:Label>
                    </div>
                    <!-- .col-md-2-->
                    <div class="col-md-3" style="padding-left: 5px">
                        <strong>Facility ID</strong> :<asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                    </div>
                    <!-- .col-md-4-->
                </div>
                <!-- .row -->
                <div class="row well well-sm" id="divTitle">
                    <div class="col-md-12">
                        <i class="text-danger pull-left">* All fields are required</i></div>
                    <hr />
                    <div class="row">
                        <asp:Panel ID="panelError" runat="server">
                            <asp:Label ID="errorLabel" runat="server" CssClass="text-danger" Style="font-weight: bold;
                                color: #800000" Text=""></asp:Label>
                            <asp:HiddenField ID="HPatientId" runat="server" />
                            <asp:HiddenField ID="HAppointmentId" runat="server" Value="-1" />
                            <asp:HiddenField ID="HMode" runat="server" Value="New" />
                            <asp:HiddenField ID="HLocationId" runat="server" />
                            <asp:HiddenField ID="HUserId" runat="server" />
                            <asp:HiddenField ID="HPatientGender" runat="server" />
                            <asp:HiddenField ID="HPatientAge" runat="server" />
                        </asp:Panel>
                    </div>
                    <div class="row" style="padding-bottom: 3px">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                Appointment Status</label>
                        </div>
                        <!-- .col-md-5-->
                        <div class="col-md-7">
                            <asp:Label ID="labelAppStatus" runat="server" Text="" Height="25px" Font-Bold="true"></asp:Label></div>
                        <!-- .col-md-5-->
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                <span class="text-danger">*</span>Service Area:</label></div>
                        <div class="form-group col-md-5">
                            <asp:DropDownList ID="ddlServiceArea" CssClass="form-control input-sm" runat="server"
                                AutoPostBack="false" ViewStateMode="Enabled" EnableViewState="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!-- .row -->
                    <div class="row">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                Purpose:</label>
                        </div>
                        <!-- .col-md-5-->
                        <div class="form-group col-md-5">
                            <asp:DropDownList ID="ddAppPurpose" CssClass="form-control input-sm" runat="server"
                                AutoPostBack="false" ViewStateMode="Enabled" EnableViewState="true">
                            </asp:DropDownList>
                        </div>
                        <!-- .col-md-5-->
                    </div>
                    <!-- .row-->
                    <div class="row">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                Appointment Date :</label>
                        </div>
                        <!-- .col-md-5-->
                        <div class="form-group col-md-5" style="white-space: nowrap; position: relative;padding-right:0">
                            <asp:TextBox ID="textAppointmentDate" CssClass="form-control col-md-3 input-sm" runat="server"
                                MaxLength="12" AutoComplete="false" Style="padding-right: 0%"></asp:TextBox>
                            <%--  <IQ:IQDatePicker ID="dpAppointment" runat="server" ImageCSSClass="datePickerImage" TextBoxCssClass="form-control col-md-5 input-sm"></IQ:IQDatePicker>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="textAppointmentDate"
                                ValidationGroup="df_x" ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator>
                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textAppointmentDate"
                                PopupButtonID="Image1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_past_dates" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="textAppointmentDate"
                                Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Label ID="labelAppointmentDate" runat="server" Visible="false" Font-Bold="true" />
                        </div>
                        <div class="col-md-2" style="padding: 0; text-align: left">
                            <asp:ImageButton runat="Server" ID="Image1" Height="22" CssClass="" Style="hspace: 3;
                                width: 22; height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                        </div>
                        <!-- .col-md-5-->
                    </div>
                    <!-- .row-->
                    <div class="row">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                Provider :
                            </label>
                        </div>
                        <!-- .col-md-5-->
                        <div class="form-group col-md-5">
                            <asp:UpdatePanel ID="divProviderPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddAppProvider" CssClass="form-control input-sm" runat="server"
                                        AutoPostBack="false" OnSelectedIndexChanged="SelectedProviderChanged">
                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="labelAvailablity" Font-Bold="true" ForeColor="Red"
                                        Style="text-align: right; margin-bottom: 10px;" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="labelProvider" runat="server" Visible="false" Font-Bold="true" />
                        </div>
                        <!-- .col-md-5-->
                    </div>
                    <!-- .row-->
                    <div class="row">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                Notes :
                            </label>
                        </div>
                        <!-- .col-md-5-->
                        <div class="form-group col-md-5">
                            <asp:TextBox ID="txtAppNotes" CssClass="form-control" runat="server" TextMode="MultiLine"
                                Rows="4" Columns="60"> </asp:TextBox>
                        </div>
                        <!-- .col-md-5-->
                    </div>
                    <!-- .row-->
                    <div class="row">
                        <div class="col-md-5">
                            <label class="control-label pull-right">
                                Booked By :</label></div>
                        <div class="form-group col-md-5">
                            <asp:Label ID="labelBokeddBy" CssClass="pull-left" runat="server" Visible="true"
                                Font-Bold="true" />
                        </div>
                    </div>
                    <!-- .row -->
                    <br />
                    <div class="row" id="divAction">
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-5 pull-left">
                            <asp:Button ID="buttonSave" CssClass="btn btn-info input-sm pull-left" runat="server"
                                Text="Save Appointment" OnClick="buttonSave_Click" CausesValidation="true" ValidationGroup="df_x" />
                            <span style="display: <%=sDelete %>">
                                <asp:Button ID="btnDelete" CssClass="btn btn-danger input-sm" runat="server" Text="Delete Appointment"
                                    OnClick="buttonDelete_Click" CausesValidation="false" />
                            </span>
                            <asp:Button ID="buttonCancel" CssClass="btn btn-danger input-sm" runat="server" Text="Cancel"
                                OnClick="buttonCancel_Click" />
                        </div>
                        <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteAppointment" runat="server" DisplayModalPopupID="mpeDeleteAppointment"
                            TargetControlID="btnDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <ajaxToolkit:ModalPopupExtender ID="mpeDeleteAppointment" runat="server" PopupControlID="pnlPopupDel"
                            TargetControlID="btnDelete" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlPopupDel" runat="server" Style="display: none; background-color: #FFFFFF;
                            width: 300px; border: 3px solid #0DA9D0;">
                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                text-align: center; font-weight: bold;">
                                Confirmation
                            </div>
                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;
                                width: 280px; white-space: normal">
                                This action cannot be reversed.<br />
                                Are you sure you want to delete this patient appointment ?
                            </div>
                            <div style="padding: 3px; text-align: right">
                                <asp:Button ID="btnYes" CssClass="btn btn-info" runat="server" Text="Yes" /><asp:Button
                                    ID="btnNo" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                        </asp:Panel>
                    </div>
                    <!-- .row -->
                </div>
                <!-- .row -->
            </div>
            <!-- .row -->
        </div>
        <!-- .panel -->
    </div>
    <!-- .row -->
</asp:Panel>
<asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="AppointmentDialog" runat="server" TargetControlID="buttonRaiseItemPopup"
    PopupControlID="AppointmentPopup" BackgroundCssClass="modalBackground" BehaviorID="appointmentx572"
    PopupDragHandleControlID="divTitle" Enabled="True" DynamicServicePath="">
</ajaxToolkit:ModalPopupExtender>
