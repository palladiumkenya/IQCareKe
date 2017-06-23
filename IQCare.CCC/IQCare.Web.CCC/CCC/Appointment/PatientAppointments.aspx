<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientAppointments.aspx.cs" Inherits="IQCare.Web.CCC.Appointment.TodaysAppointments" %>

<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

    <%--    <div class="col-md-12">
        <span id="Span1" class="text-capitalize pull-left glyphicon-text-size= fa-2x" runat="server">
            <i class="fa fa-calendar fa-2x" aria-hidden="true"></i>Todays Appointments</span>
    </div>--%>
    <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">
        <div class="col-md-12 form-group">
            <div class="col-md-12">
                <label class="control-label pull-left text-info">Patient Appointment Summary </label>

            </div>
            <table class="table table-hover" id="tblAppointment" clientidmode="Static" runat="server">
                <thead>
                    <tr class="active">
                        <th><span class="text-primary" aria-hidden="true">#</span></th>
                        <th><span class="text-primary" aria-hidden="true">Appointment Date</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Service Area</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Reason</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Description</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Differetiated Care</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Status</span> </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="col-md-12">
                <asp:LinkButton runat="server" ID="AddAppointment" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Appointment</asp:LinkButton>
            </div>

        <IQ:ucExtruder runat="server" ID="ucExtruder" />
    </div>
    <script>
        $(document).ready(function () {
            var patientId = "<%=PatientId%>";
            jQuery.support.cors = true;
            $.ajax(
            {
                type: "POST",
                url: "../WebService/PatientService.asmx/GetPatientAppointments",
                data: "{'patientId':'" + patientId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    var itemList = response.d;
                    var table = '';
                    itemList.forEach(function (item, i) {
                        n = i + 1;
                        table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + moment(item.AppointmentDate).format('DD-MMM-YYYY') + '</td><td style="text-align: left">' + item.ServiceArea + '</td><td style="text-align: left">' + item.Reason + '</td><td style="text-align: left">' + item.Description + '</td><td style="text-align: left">' + item.DifferentiatedCare + '</td><td style="text-align: left">' + item.Status + '</td></tr>';
                    });

                    $('#tblAppointment').append(table);

                },

                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
            $("#btnClose").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
            });

            $("#AddAppointment").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/Appointment/ScheduleAppointment.aspx") %>';
            });
        })

    </script>
</asp:Content>
