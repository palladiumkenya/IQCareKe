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
                    <th><span class="text-primary" aria-hidden="true">Edit</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Delete</span> </th>
                    <th><span class="text-primary" aria-hidden="true">AppointmentId</span> </th>
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
                                i+1,
                                moment(itemList[i].AppointmentDate).format('DD-MMM-YYYY'),
                                itemList[i].ServiceArea,
                                itemList[i].Reason,
                                itemList[i].DifferentiatedCare,
                                itemList[i].Status,
                                itemList[i].EditAppointment,
                                itemList[i].DeleteAppointment,
                                itemList[i].AppointmentId
                            ]
                        );
                    }
                    initialiseDataTable(arrayAppointments);
                },

                error: function (msg) {
                    //alert(msg.responseText);
                }
            });

            var appointmentsTable;
            function initialiseDataTable(data) {
                 $("#tblAppointment").dataTable().fnDestroy();
                 tableAppointments = $('#tblAppointment').DataTable({
                     "columnDefs": [
                         {
                             "targets": [8],
                             "visible": false,
                             "searchable": false
                         }
                     ],
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

        $("#tblAppointment").on('click', '.btnDelete', function () {
            var AppointmentId = tableAppointments.row($(this).parents('tr')).data()["8"];
            DeleteAppointment(AppointmentId);
            tableAppointments.row($(this).parents('tr'))
            .remove()
            .draw();
                
            var index = reactionEventList.indexOf($(this).parents('tr').find('td:eq(0)').text());
            if (index > -1) {
                reactionEventList.splice(index, 1);
            }
        });

        function DeleteAppointment(appointmentid){
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/DeleteAppointment",
                data: "{'AppointmentId': '" + appointmentid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Appointment Deleted successfully");
                    //resetFields();
                    //setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                    },
                error: function (response) {
                    alert(JSON.stringify(response));
                        toastr.error(response.d, "Appointment not deleted");
                    }
            });
        }
    </script>
</asp:Content>
