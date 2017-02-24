<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ScheduleAppointment.aspx.cs" Inherits="IQCare.Web.CCC.Appointment.ScheduleAppointment" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%--    <div class="col-md-12">
        <span id="Span1" class="text-capitalize pull-left glyphicon-text-size= fa-2x" runat="server">
            <i class="fa fa-calendar fa-2x" aria-hidden="true"></i>New Appointment</span>
    </div>--%>
    <div class="col-md-12">
        <uc:PatientDetails runat="server" />
        <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">

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
                                <div class="datepicker fuelux form-group" id="PersonAppointmentDate">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="AppointmentDate"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                                <span class="sr-only">Toggle Calendar</span>
                                            </button>
                                            <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                                <div class="datepicker-calendar">
                                                    <div class="datepicker-calendar-header">
                                                        <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                                        <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                                        <button type="button" class="title" data-month="11" data-year="2014">
                                                            <span class="month">
                                                                <span data-month="0">January</span>
                                                                <span data-month="1">February</span>
                                                                <span data-month="2">March</span>
                                                                <span data-month="3">April</span>
                                                                <span data-month="4">May</span>
                                                                <span data-month="5">June</span>
                                                                <span data-month="6">July</span>
                                                                <span data-month="7">August</span>
                                                                <span data-month="8">September</span>
                                                                <span data-month="9">October</span>
                                                                <span data-month="10">November</span>
                                                                <span data-month="11" class="current">December</span>
                                                            </span><span class="year">2017</span>
                                                        </button>
                                                    </div>
                                                    <table class="datepicker-calendar-days">
                                                        <thead>
                                                            <tr>
                                                                <th>Su</th>
                                                                <th>Mo</th>
                                                                <th>Tu</th>
                                                                <th>We</th>
                                                                <th>Th</th>
                                                                <th>Fr</th>
                                                                <th>Sa</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>
                                                    <div class="datepicker-calendar-footer">
                                                        <button type="button" class="datepicker-today">Today</button>
                                                    </div>
                                                </div>
                                                <div class="datepicker-wheels" aria-hidden="true">
                                                    <div class="datepicker-wheels-month">
                                                        <h2 class="header">Month</h2>
                                                        <ul>
                                                            <li data-month="0">
                                                                <button type="button">Jan</button></li>
                                                            <li data-month="1">
                                                                <button type="button">Feb</button></li>
                                                            <li data-month="2">
                                                                <button type="button">Mar</button></li>
                                                            <li data-month="3">
                                                                <button type="button">Apr</button></li>
                                                            <li data-month="4">
                                                                <button type="button">May</button></li>
                                                            <li data-month="5">
                                                                <button type="button">Jun</button></li>
                                                            <li data-month="6">
                                                                <button type="button">Jul</button></li>
                                                            <li data-month="7">
                                                                <button type="button">Aug</button></li>
                                                            <li data-month="8">
                                                                <button type="button">Sep</button></li>
                                                            <li data-month="9">
                                                                <button type="button">Oct</button></li>
                                                            <li data-month="10">
                                                                <button type="button">Nov</button></li>
                                                            <li data-month="11">
                                                                <button type="button">Dec</button></li>
                                                        </ul>
                                                    </div>
                                                    <div class="datepicker-wheels-year">
                                                        <h2 class="header">Year</h2>
                                                        <ul></ul>
                                                    </div>
                                                    <div class="datepicker-wheels-footer clearfix">
                                                        <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                                        <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Service Area</label>
                        </div>
                        <div class="col-md-12 pull-right">
                            <asp:DropDownList runat="server" ID="ServiceArea" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label for="reason" class="control-label pull-left">Reason</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
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
                                <asp:DropDownList runat="server" ID="DifferentiatedCare" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label for="description" class="control-label pull-left">Description</label>
                            </div>
                            <div class="col-md-12">
                                <asp:TextBox runat="server" ID="description" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
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

            <div class="col-md-12">
                <hr />
            </div>
            <div class="col-md-12">
                <div class="col-md-6"></div>
                <div class="col-md-6">
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
    <script type="text/javascript">
        $('#PersonAppointmentDate').datepicker({
            allowPastDates: false,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        });

        $(document).ready(function () {
            $("#btnSaveAppointment").click(function () {
                if ($('#vitalsform').parsley().validate()) {
                    addPatientAppointment();
                } else {
                    return false;
                }
            });
            $("#btnReset").click(function () {
                resetFields();
            });
            $("#btnCancel").click(function () {
                debugger;
                window.location.href = '/CCC/patient/patientHome.aspx';
            });
        });

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
            debugger;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientAppointment",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Appointment saved successfully");
                    resetFields();
                },
                error: function (response) {
                    toastr.error(response.d, "Appointment not saved");
                }
            });
        }

        function resetFields(parameters) {
            $("#ServiceArea").val("");
            $("#Reason").val("");
            $("#DifferentiatedCare").val("");
            $("#description").val("");
            $("#status").val("");
            $("#AppointmentDate").val("");
        }
    </script>
</asp:Content>


