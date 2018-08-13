<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDepressionScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucDepressionScreening" %>
<style>
    .rbList{float: right;}
    .rbList input{margin-left: 5px;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12" id="depressionscreening">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">During the last two weeks have you ever been bothered by:</span></label>
				</div>

				<div class="col-md-12 form-group" id="depressionscreeningquestions">
					<div class="row">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
					</div>
				</div>
			</div>
		</div>

        <!-- PHQ 9 -->
        <div class="panel panel-info" id="phq9panel">
			<div class="panel-body">
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span>PATIENT HAELTH QUESTIONNAIRE (PHQ - 9)</span></label>
				</div>
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">On the last two weeks, how often have you been bothered by any of the following problems?</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="depression-measurements">
                                Depression Severity: <span class="label label-success">0-4 None</span>
                                <span class="label label-primary">5-9 Mild</span> <span class="label label-info">10-14 Moderate</span> <span class="label label-warning">15-19 Moderately Severe</span> <span class="label label-danger">20 - 30 Severe</span>
                            </div>
                        </div>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHDepressionTotal" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHDepressionSeverity" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-12">
                            <asp:PlaceHolder ID="PHRecommendedManagement" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    var currentStep;
    $("#scmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        currentStep = data.step;
        if (currentStep == 1) {
            addUpdateDepressionScreeningData();
        }
    });

    function addUpdateDepressionScreeningData()
    {
        var error = 0;
        $(".rbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id');
            var checkedValue = $('#' + screeningCategory + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined')
            {
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
        $("#depressionscreening input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('notes', '');
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
            toastr.success("Depression Screening Saved");
        }
    }

    $("#phq9panel input:radio").change(function (evt, data) {
        var selectionTotal = 0;
        $("#phq9panel input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            selectionTotal = selectionTotal + selectedIndex;
        });
        //select depression severity and recommended management
        selectDepressionNotes(selectionTotal);
        $("#<%=depressionTotalTb.ClientID%>").val(selectionTotal);
        if ($("#sc<%=depressionTotalTb.ClientID%>").length != 0) {
            $("#sc<%=depressionTotalTb.ClientID%>").val(selectionTotal);
        }
    });

    function selectDepressionNotes(selectionTotal)
    {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getDepressionSeverityNotes",
            data: "{'depressionFrequency': '" + selectionTotal + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=depressionSeverityTb.ClientID%>").val(response.d);
                if ($("#sc<%=depressionSeverityTb.ClientID%>").length != 0) {
                    $("#sc<%=depressionSeverityTb.ClientID%>").val(response.d);
                }
            },
            error: function (response) {
                toastr.error("Error selecting Depression Severity");
            }
        });
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getDepressionRMNotes",
            data: "{'depressionFrequency': '" + selectionTotal + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=depressionReccommendationTb.ClientID%>").val(response.d);
                if ($("#sc<%=depressionReccommendationTb.ClientID%>").length != 0) {
                    $("#sc<%=depressionReccommendationTb.ClientID%>").val(response.d);
                }
            },
            error: function (response) {
                toastr.error("Error selecting Depression Recommended Management");
            }
        });
    }

    $("#depressionscreeningquestions input:radio").change(function (evt, data) {
        var selectionTotal = 0;
        $("#depressionscreeningquestions input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                selectionTotal = selectionTotal + 1;
            }
        });
        if (selectionTotal == 2) {
            $("#phq9panel").show();
        }
        else {
            $("#phq9panel input[type=radio]:checked").each(function () {
                $(this).prop('checked', false);
                var selectionTotal = 0;
                $("#<%=depressionTotalTb.ClientID%>").val(selectionTotal);
                selectDepressionNotes(selectionTotal);
            });
            $("#phq9panel").hide();
        }
    });

    $(document).ready(function () {
        var selectionTotal = 0;
        $("#depressionscreeningquestions input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                selectionTotal = selectionTotal + 1;
            }
        });
        if (selectionTotal == 2) {
            $("#phq9panel").show();
        }
        else {
            $("#phq9panel").hide();
        }
    });
</script>