<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDisclosure.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucDisclosure" %>
<div class="col-md-12 form-group">
	<div class="col-md-12" id="disclosurescreening">
        <div class="panel panel-info">
			<div class="panel-body">
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Disclosure</span></label>
				</div>
				<div class="col-md-12 form-group" id="disclosurequestionscontainer">
                    <asp:PlaceHolder ID="PHDisclosureQuestions" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
    </div>
</div>
<script type="text/javascript">
    $("#disclosurescreening input:radio").change(function (evt, data) {
        var disclosedToRbList = $(".disclosedToList").closest("div .col-md-6").attr('id');
        $("#disclosurescreening input[type=radio]:checked").each(function () {
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                $("#" + disclosedToRbList).show();
            }
            else {
                $("#" + disclosedToRbList).hide();
            }
        });
    });
    $(document).ready(function () {
        var disclosedToRbList = $(".disclosedToList").closest("div .col-md-6").attr('id');
        $("#" + disclosedToRbList).hide();
        $("#disclosurescreening input[type=radio]:checked").each(function () {
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                $("#" + disclosedToRbList).show();
            }
            else {
                $("#" + disclosedToRbList).hide();
            }
        });
    });
    $("#scmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 4 || currentStep == 3) {
            addUpdateDisclosureData();
        }
    });
    function addUpdateDisclosureData()
    {
        var error = 0;
        $("#scmyWizard .disclosurerbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('disclosurerbl', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
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
        $("#scmyWizard .disclosureddList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('disclosureddl', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue).val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
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
            toastr.success("Disclosure Screening Saved");
        }
    }
</script>