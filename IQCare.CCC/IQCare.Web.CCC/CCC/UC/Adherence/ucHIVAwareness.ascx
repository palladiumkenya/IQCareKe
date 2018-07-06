<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHIVAwareness.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucHIVAwareness" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Awareness of HIV Status</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
						<div class="col-md-10 text-left">
							<label>1. Has the patient/caregiver accepted HIV status?</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbAccepted" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
                    <hr />
					<div class="row">
						<div class="col-md-10 text-left">
							<label>2. For children/adolescents: is age - appropriate disclosure underway/complete?</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbDisclosureComplete" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 1) {
            var HIVStatusId = <%=HIVStatusId%>;
            if (HIVStatusId > 0) {
                updateAdherenceHIVStatus(HIVStatusId);
            }
            else {
                addAdherenceHIVStatus();
            }
        }
    });

    function updateAdherenceHIVStatus(HIVStatusId) {
        var statusId = HIVStatusId;
        var acceptedStatus = $("input[name='<%=rbAccepted.UniqueID %>']:checked").val();
        var disclosureComplete = $("input[name='<%=rbDisclosureComplete.UniqueID %>']:checked").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/updateHIVStatus",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','AcceptedStatus': '" + acceptedStatus + "'," +
            "'DisclosureComplete': '" + disclosureComplete + "','statusId':'"+statusId+"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Awareness of HIV status updated");
            },
            error: function (response) {
                toastr.error(response.d, "Awareness of HIV status Not Updated");
            }
        });
    }

    function addAdherenceHIVStatus() {
        var acceptedStatus = $("input[name='<%=rbAccepted.UniqueID %>']:checked").val();
        var disclosureComplete = $("input[name='<%=rbDisclosureComplete.UniqueID %>']:checked").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addAdherenceHIVStatus",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','AcceptedStatus': '" + acceptedStatus + "'," +
            "'DisclosureComplete': '" + disclosureComplete + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "HIV Status Awareness Saved Successfully");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response.d));
            }
        });
    }

    jQuery(function ($) {
        var HIVStatusId = <%=HIVStatusId%>;
        if (HIVStatusId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionOne').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
                $('#myWizard').find('#dsSectionOne').toggleClass('complete', true);
            });
        }
    });
</script>