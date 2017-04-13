<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIptOutcome.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucIptOutcome" %>

<div class="col-md-12 form-group">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="col-md-12">
                <div class="col-md-12 form-group">
                    <label class="control-label pull-left">Isoniazid Preventive Therapy Outcome</label>
                </div>

                <div class="col-md-12 form-group">
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Event</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="iptEvent" ClientIDMode="Static"/>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Reason For Discontinuation</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="discontinuation" ClientIDMode="Static"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=discontinuation.ClientID%>").prop('disabled', true);

                function AddPatientIptOutcome() {
            var iptEvent = $("#<%=iptEvent.ClientID%>").val();
            var reasonForDiscontinuation = $("#<%=discontinuation.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientTbService.asmx/AddPatientIptOutcome",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','iptEvent': '" + iptEvent + "','reasonForDiscontinuation': '" + reasonForDiscontinuation +  "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Patient IPT outcome saved successfully");
                    resetAppointmentFields();
                },
                error: function (response) {
                    toastr.error(response.d, "Patient IPT outcome not saved");
                }
            });
        }
    });

</script>
