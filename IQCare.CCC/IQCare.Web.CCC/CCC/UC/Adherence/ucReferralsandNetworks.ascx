<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReferralsandNetworks.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucReferralsandNetworks" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Referrals and Networks</span></label>
				</div>

				<div class="col-md-12 form-group" id="referralsandnetworks">
					<asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    $("#abmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 4) {
            addUpdateRNScreeningData();
        }
    });

    function addUpdateRNScreeningData()
    {
        var error = 0;
        $("#referralsandnetworks input[type=radio]:checked").each(function () {
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
        $("#referralsandnetworks textarea").each(function () {
            var categoryId = $(this).attr('id');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientClinicalNotesService.asmx/addPatientClinicalNotes",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','serviceAreaId':'" + serviceAreaId + "','notesCategoryId':'" + categoryId + "','clinicalNotes':'" + clinicalNotes + "','userId':'" + userId + "'}",
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
            toastr.success("Referrals and Networks Saved");
        }
    }
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getPatientNotes",
            data: "{'PatientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                //alert(JSON.stringify(response));
                $.each(JSON.parse(response.d), function (index, value) {
                    inputnotes = this.ClinicalNotes;
                    if ($("#" + this.NotesCategoryId).length > 0) {
                        $("#" + this.NotesCategoryId).val(inputnotes);
                    }
                });
            },
            error: function (response) {
                toastr.error("Notes could not be loaded");
            }
        });
        $.ajax({
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/getPatientScreening",
            data: "{'PatientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                $.each(JSON.parse(response.d), function (index, value) {
                    if ($("#" + this.ScreeningCategoryId).length > 0) {
                        var radioBtns = "#" + this.ScreeningCategoryId;
                        $(radioBtns + " input:radio[value='" + this.ScreeningValueId + "']").attr("checked", true);
                    }
                });
                $(".adherencebarriersloading").hide();
            },
            error: function (response) {
                toastr.error("Screening could  not be loaded");
            }
        });
    });
    jQuery(function ($) {
        var refId = <%=RefId%>;
        if (refId > 0) {
            $('#abmyWizard').wizard();
            $('#abmyWizard').find('#dsSectionFour').toggleClass('complete', true);
            $('#abmyWizard').on('changed.fu.wizard', function (evt, data) {
                $('#abmyWizard').find('#dsSectionFour').toggleClass('complete', true);
            });
        }
    });
</script>