<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIptOutcome.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucIptOutcome" %>

<div class="col-md-12 form-group">
    <div class="col-md-12 form-group">
        <div class="col-md-12">
            <div class="col-md-12">
                <label class="control-label pull-left">Event</label>
            </div>
            <div class="col-md-12">
                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="iptEvent" ClientIDMode="Static" onclick="IptOutcomeEvent();" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="col-md-12">
                <label class="control-label pull-left">Reason For Discontinuation</label>
            </div>
            <div class="col-md-12">
                <asp:TextBox runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="discontinuation" ClientIDMode="Static" />
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=discontinuation.ClientID%>").prop('disabled', true);

        getIptOutcome();
    });

    function IptOutcomeEvent() {
        if ($("#iptEvent :selected").text() === "Discontinued") {
            $("#<%=discontinuation.ClientID%>").prop('disabled', false);
        } else {
            $("#<%=discontinuation.ClientID%>").prop('disabled', true);
        }
    }


    function getIptOutcome() {
        
        var iptEvent = $("#iptEvent").val();
        var reasonForDiscontinuation = $("#discontinuation").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientTbService.asmx/GetPatientIptOutcome",
            data: "{'patientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d !== null) {
                    var ipt = JSON.parse(response.d);
                    $('#iptEvent').val(ipt.IptEvent);
                    $("#<%=discontinuation.ClientID%>").val(ipt.ReasonForDiscontinuation)
                }
            },
            error: function (response) {
                toastr.error(response.d, "Unable to fetch IPT Outcome");
            }
        });

    }

</script>
