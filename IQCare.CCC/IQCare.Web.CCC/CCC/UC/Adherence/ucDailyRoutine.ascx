<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDailyRoutine.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucDailyRoutine" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Daily Routine</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
						<div class="col-md-12 text-left">
							<label>Tell me about your typical day (Review the patient's/Caregiver's daily routine)</label>
						</div>
						<div class="col-md-12">
							<asp:TextBox id="tbTypicalDay" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
					<div class="row">
						<div class="col-md-12 text-left">
							<label>Please tell me how you take each of your medicines (Review how the patient takes medicine or how the caregiver administers it)</label>
						</div>
						<div class="col-md-12">
							<asp:TextBox id="tbMedicineAdministration" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row">
						<div class="col-md-12 text-left">
							<label>What do you do in case of visits or travels?</label>
						</div>
						<div class="col-md-12">
							<asp:TextBox id="tbTravelCase" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row">
						<div class="col-md-12 text-left">
							<label>For orphans/children: who is the primary caregiver? Assess their level of commitment</label>
						</div>
						<div class="col-md-12">
							<asp:TextBox id="tbPrimaryCaregiver" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
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
        if (currentStep == 2) {
            var dailyRoutineId = <%=dailyRoutineId%>;
            if (dailyRoutineId > 0) {
                updateDailyRoutine(dailyRoutineId);
            }
            else {
                addDailyRoutine();
            }
        }
    });

    function updateDailyRoutine(dailyRoutineId) {
        var DRId = dailyRoutineId;
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var typicalDay = $("#<%=tbTypicalDay.ClientID%>").val();
        var medicineAdministration = $("#<%=tbMedicineAdministration.ClientID%>").val();
        var travelCase = $("#<%=tbTravelCase.ClientID%>").val();
        var primaryCaregiver = $("#<%=tbPrimaryCaregiver.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/updateDailyRoutine",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','typicalDay': '" + typicalDay + "','medicineAdministration':'" + medicineAdministration + "','travelCase':'" + travelCase + "','primaryCaregiver':'" + primaryCaregiver + "','DRId':'" + DRId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Daily routine Updated");
            },
            error: function (response) {
                toastr.error(response.d, "Daily routine not updated");
            }
        });
    }
    function addDailyRoutine() {
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var typicalDay = $("#<%=tbTypicalDay.ClientID%>").val();
        var medicineAdministration = $("#<%=tbMedicineAdministration.ClientID%>").val();
        var travelCase = $("#<%=tbTravelCase.ClientID%>").val();
        var primaryCaregiver = $("#<%=tbPrimaryCaregiver.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addAdherenceDailyRoutine",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','typicalDay': '" + typicalDay + "','medicineAdministration':'" + medicineAdministration + "','travelCase':'" + travelCase + "','primaryCaregiver':'" + primaryCaregiver + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Daily routine saved");
            },
            error: function (response) {
                toastr.success("Daily routine not saved");
            }
        });
    }

    jQuery(function ($) {
        var dailyRoutineId = <%=dailyRoutineId%>;        if (dailyRoutineId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionTwo').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
                $('#myWizard').find('#dsSectionTwo').toggleClass('complete', true);
            });
        }
    });
</script>