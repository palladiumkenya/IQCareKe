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
            
            <table class="table table-hover" id="tblAppointment">
                <thead class="thead-default">
                <tr>
                    <th><span class="text-primary" aria-hidden="true">#</span></th>
                    <th><span class="text-primary" aria-hidden="true">Appointment Date</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Service Area</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Reason</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Differetiated Care</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Status</span> </th>
                </tr>

                </thead>

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
            var arrayAppointments = [];

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
                    //console.log(itemList);
                    for (var i = 0, len = itemList.length; i < len; i++) {
                        //console.log(itemList[i]);
                        arrayAppointments.push(
                            [
                                i,
                                moment(itemList[i].AppointmentDate).format('DD-MMM-YYYY'),
                                itemList[i].ServiceArea,
                                itemList[i].Reason,
                                itemList[i].DifferentiatedCare,
                                itemList[i].Status
                            ]
                        );
                    }
                    initialiseDataTable(arrayAppointments);
                },

                error: function (msg) {
                    //alert(msg.responseText);
                }
            });

            function initialiseDataTable(data) {
                $("#tblAppointment").dataTable().fnDestroy();
                tableAppointments = $('#tblAppointment').DataTable({
                    "aaData": data,
                    paging: true,
                    searching: true
                });
            }


            $("#btnClose").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
            });

            $("#AddAppointment").click(function () {
                setTimeout(function() {
                        window.location.href = '<%=ResolveClientUrl("~/CCC/Appointment/ScheduleAppointment.aspx") %>';
                    },
                    2000);
            });
        })

    </script>
</asp:Content>
