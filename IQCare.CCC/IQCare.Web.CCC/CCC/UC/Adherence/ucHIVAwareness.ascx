<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHIVAwareness.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucHIVAwareness" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Awareness of HIV Status</span></label>
				</div>

				<div class="col-md-12 form-group" id="HIVStatusScreening">
                    <asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
   // jQuery(function ($) {
        $("#abmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
            var currentStep = data.step;
            if (currentStep == 1) {
                addUpdateHIVAwarenessScreeningData();
            }
        });

        function addUpdateHIVAwarenessScreeningData()
        {
            var error = 0;
            $("#HIVStatusScreening input[type=radio]:checked").each(function () {
                var screeningValue = $(this).val();
                var screeningCategory = $(this).closest("table").attr('id');
                var screeningType = <%=screenTypeId%>;
                var patientId = <%=PatientId%>;
                var patientMasterVisitId = <%=PatientMasterVisitId%>;
                var userId = <%=userId%>;
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientScreeningService.asmx/AddUpdateScreeningData",
                    data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','screeningType':'" + screeningType + "','screeningCategory':'" + screeningCategory + "','screeningValue':'" + screeningValue + "','userId':'" + userId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        error = 0;
                    },
                    error: function (response) {
                        error = 1;
                    }
                });
            });
            if (error == 0) {
                toastr.success("Awareness of HIV Status Saved");
            }
        }

        jQuery(function ($) {
            var HIVStatusId = <%=HIVStatusId%>;
            if (HIVStatusId > 0) {
                $('#abmyWizard').wizard();
                $('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
                $('#abmyWizard').on('changed.fu.wizard', function (evt, data) {
                    $('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
                });
            }
        });
    //});
</script>