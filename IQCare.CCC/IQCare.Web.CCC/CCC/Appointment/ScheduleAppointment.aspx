<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ScheduleAppointment.aspx.cs" Inherits="IQCare.Web.CCC.Appointment.ScheduleAppointment" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%--    <div class="col-md-12">
        <span id="Span1" class="text-capitalize pull-left glyphicon-text-size= fa-2x" runat="server">
            <i class="fa fa-calendar fa-2x" aria-hidden="true"></i>New Appointment</span>
    </div>--%>
    <div class="col-md-12">
        <uc:PatientDetails runat="server" />
        <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">
            <div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12 form-group">
                    <div class="col-md-12">
                        <label class="control-label pull-left text-primary">Appointment Details</label>
                    </div>
                    <div class="col-md-12">
                        <hr style="margin-bottom: 1%; margin-top: 1%" />
                    </div>
                </div>

                <div class="col-md-12 form-group">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label class="control-label pull-left">Date</label>
                                </div>
                                <div class="col-md-12">
                                    <div class='input-group date' id='PersonAppointmentDate'>
										<span class="input-group-addon">
											<span class="glyphicon glyphicon-calendar"></span>
										</span>
										<asp:TextBox runat="server"  CssClass="form-control input-sm" ID="AppointmentDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" required ="True" data-parsley-min-message="Input the appointment date"></asp:TextBox>
									</div>
                                </div> 

                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Service Area</label>
                            </div>
                            <div class="col-md-12 pull-right">
                                <asp:DropDownList runat="server" ID="ServiceArea" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" data-parsley-min-message="Select the service area" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label for="reason" class="control-label pull-left">Reason</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" data-parsley-min-message="Select the reason" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label for="reason" class="control-label pull-left">Differentiated Care</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="DifferentiatedCare" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" data-parsley-min-message="Select differentiated care" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label for="description" class="control-label pull-left">Description</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="description" CssClass="form-control input-sm" ClientIDMode="Static" minlength="5" data-parsley-minlength-message="Description cannot be less than 5 characters" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label for="status" class="control-label pull-left">Status</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="status" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <hr />
            </div>
            <div class="col-md-12">
                <div class="col-md-3"></div>
                <div class="col-md-9">
                    <div class="col-md-3"></div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnSaveAppointment" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Appointment </asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Form  </asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close Appointment </asp:LinkButton>
                    </div>

                </div>
            </div>

        </div>
        <asp:TextBox runat="server" ID="txtpatientMasterVisitId" ClientIDMode="Static" Visible="False" />
        <asp:TextBox runat="server" ID="txtpatientId" ClientIDMode="Static" Visible="False" />

    </div>

    <!-- Modal -->
    <div id="AlertModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Appointments</h4>

                </div>
                <div class="modal-body">
                    <div class="row" id="ModalMessage"></div>
                </div>

                <div class="modal-footer">
                    <button id="btnOk" type="button" class="btn btn-default" onclientclick="return false;">OK</button>
                    <button id="btnDismiss" type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $("#PersonAppointmentDate").datetimepicker({
            defaultDate: $("#<%=AppointmentDate.ClientID%>").val(),
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false
        }).on("dp.change", function (selectedDate) {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
            if (moment('' + appDate + '').isAfter(futureDate)) {
                toastr.error("Appointment date cannot be set to over 7 months");
                $("#<%=AppointmentDate.ClientID%>").val("");
                return false;
            }
            AppointmentCount();
        });

        $(document).ready(function () {
            $("#AppointmentDate").val("");
            $("#btnSaveAppointment").click(function () {
                var appointmentid = <%=AppointmentId%>;
                if ($('#AppointmentForm').parsley().validate()) {
                    var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
                    var appDate = $("#<%=AppointmentDate.ClientID%>").val();
                    if (moment('' + appDate + '').isAfter(futureDate)) {
                        toastr.error("Appointment date cannot be set to over 7 months");
                        return false;
                    }
                    if (appointmentid > 0) {
                        updateAppointment();
                    }
                    else {
                        checkExistingAppointment();
                    }
                   // checkExistingAppointment();
                }
                //if (appointmentid > 0) {
                //    updateAppointment();
                //}
                //else {
                //    checkExistingAppointment();
                //}
            });
            $("#btnReset").click(function () {
                resetFields();
            });
            $("#btnCancel").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
            });

            $("#btnOk").click(function () {
                $('#AlertModal').modal('hide');
            });

            $("#btnDismiss").click(function () {
                resetFields();
            });

        });

        
<%--        $("#AppointmentDate").change(function () {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
            if (moment('' + appDate + '').isAfter(futureDate)) {
                toastr.error("Appointment date cannot be set to over 7 months");
                $("#<%=AppointmentDate.ClientID%>").val("");
                return false;
            }
            AppointmentCount();
        });--%>

        <%--$('#PersonAppointmentDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {
            alert("Appointment Date Changed");
<%--        $('#PersonAppointmentDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
            if (moment('' + appDate + '').isAfter(futureDate)) {
                toastr.error("Appointment date cannot be set to over 7 months");
                $("#<%=AppointmentDate.ClientID%>").val("");
                return false;
            }
            AppointmentCount();
        });--%>

        function AppointmentCount() {
            jQuery.support.cors = true;
            var date = $("#<%=AppointmentDate.ClientID%>").val();
            $.ajax(
                {
                    type: "POST",
                    url: "../WebService/PatientService.asmx/GetPatientAppointmentCount",
                    data: "{'date':'" + date + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function (response) {
                        var count = response.d;
                        var message = count + " appointment(s) scheduled on " + date;
                        document.getElementById("ModalMessage").innerHTML = message;
                        $('#AlertModal').modal('show');
                    },

                    error: function (msg) {
                        //alert(msg.responseText);
                    }
                });
        }

        function checkExistingAppointment() {
            var patientId = "<%=PatientId%>";
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            jQuery.support.cors = true;
            $.ajax(
                {
                    type: "POST",
                    url: "../WebService/PatientService.asmx/GetExistingPatientAppointment",
                    data: "{'patientId':'" + patientId + "','appointmentDate': '" + appointmentDate + "','serviceAreaId': '" + serviceArea + "','reasonId': '" + reason + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    cache: false,
                    success: function (response) {
                        if (response.d != null) {
                            toastr.error("Appointment already exists");
                            return false;
                        }
                        else
                        {
                            addPatientAppointment();
                        } 
                    },
                    error: function (msg) {
                        //alert(msg.responseText);
                    }
                });
        }

        function addPatientAppointment() {
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            var description = $("#<%=description.ClientID%>").val();
            var status = $("#<%=status.ClientID%>").val();
            var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
            /*if (status === '') { status = null }*/
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=UserId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientAppointment",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Appointment saved successfully");
                    resetFields();
                    setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                },
                error: function (response) {
                    toastr.error(response.d, "Appointment not saved");
                }
            });
        }

        function updateAppointment() {
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            var description = $("#<%=description.ClientID%>").val();
            var status = $("#<%=status.ClientID%>").val();
            var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
            /*if (status === '') { status = null }*/
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=UserId%>;
            var appointmentid = <%=AppointmentId%>
            $.ajax({
                    type: "POST",
                    url: "../WebService/PatientService.asmx/UpdatePatientAppointment",
                    data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "','appointmentId':'" + appointmentid+"'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "Appointment saved successfully");
                        resetFields();
                        setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                },
                error: function (response) {
                    toastr.error(response.d, "Appointment not saved");
                }
            });
        }

        <%--function updateAppointment() {
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            var description = $("#<%=description.ClientID%>").val();
            var status = $("#<%=status.ClientID%>").val();
            var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
            /*if (status === '') { status = null }*/
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=UserId%>;
            var appointmentid = <%=AppointmentId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/UpdatePatientAppointment",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "','appointmentId':'" + appointmentid+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Appointment saved successfully");
                    resetFields();
                    setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/Appointment/patientAppointments.aspx") %>'; }, 2500);
                },
                error: function (response) {
                    toastr.error(response.d, "Appointment not saved");
                }
            });
        }--%>

        function resetFields(parameters) {
            var appointmentid = <%=AppointmentId%>;
            if (appointmentid < 1) {
                $("#ServiceArea").val("");
                $("#Reason").val("");
                $("#DifferentiatedCare").val("");
                $("#description").val("");
                $("#AppointmentDate").val("");
            }
        }

        
    </script>
</asp:Content>


