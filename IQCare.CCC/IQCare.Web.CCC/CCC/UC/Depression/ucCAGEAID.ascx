<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCAGEAID.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucCAGEAID" %>
<style>
    /*.rbList{float: right;}
    .rbList input{margin-left: 5px;}*/
    .cagearbList td { text-align: left;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12" id="cageaidscreening">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">CAGE-AID SCREENING FOR ALCOHOL AND DRUG USE DISORDERS FOR ADULTS</span></label>
				</div>

				<div class="col-md-12 form-group" id="cageaidinitialquestions">
					<div class="row">
                        <asp:PlaceHolder ID="PHCageFrequency" runat="server"></asp:PlaceHolder>
					</div>
				</div>
			</div>
		</div>

        <!-- Alcohol screening -->
        <div class="panel panel-info" id="cageaidalcoholsubsequentqs">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">In the last three months</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCAGEAlcoholScreening" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHCAGEAIDScore" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHRisk" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
				</div>
			</div>
		</div>

        <!--- smoking --->
        <div class="panel panel-info" id="cageaidsmokingsubsequentqs">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">During the past 12 months</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCageSmokingScreening" runat="server"></asp:PlaceHolder>
                    </div>
				</div>
			</div>
		</div>

        <!--- notes --->
        <div class="panel panel-info" id="notespanel">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Notes</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCageNotes" runat="server"></asp:PlaceHolder>
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
        if (currentStep == 2) {
            addUpdateCAGEAIDScreeningData();
        }
    });
    function addUpdateCAGEAIDScreeningData()
    {
        var error = 0;
        $("#cageaidscreening .cagerbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('cage', '');
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
        $("#cageaidscreening input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('cage', '');
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
        $("#cageaidscreening textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('cage', '');
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
            toastr.success("Alcohol and Drug Use Screening Saved");
        }
    }
    //hide show subsequent questions
    $("#cageaidinitialquestions input:radio").change(function (evt, data) {
        var smokeTotal = 0;
        $(".smokerb input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            smokeTotal = smokeTotal + selectedIndex;
        });

        if (smokeTotal >= 2) {
            $("#cageaidsmokingsubsequentqs").show();
        }
        else {
            //hide craffft subsequent panel
            $("#cageaidsmokingsubsequentqs").hide();
        }

        var alcoholTotal = 0;
        $(".alcoholrb input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            alcoholTotal = alcoholTotal + selectedIndex;
        });

        if (alcoholTotal >= 1) {
            $("#cageaidalcoholsubsequentqs").show();
        }
        else {
            //hide craffft subsequent panel
            $("#cageaidalcoholsubsequentqs").hide();
        }
    });
    $(document).ready(function () {
        var smokeTotal = 0;
        $(".smokerb input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            smokeTotal = smokeTotal + selectedIndex;
        });

        if (smokeTotal >= 2) {
            $("#cageaidsmokingsubsequentqs").show();
        }
        else {
            //hide craffft subsequent panel
            $("#cageaidsmokingsubsequentqs").hide();
        }

        var alcoholTotal = 0;
        $(".alcoholrb input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            alcoholTotal = alcoholTotal + selectedIndex;
        });

        if (alcoholTotal >= 1) {
            $("#cageaidalcoholsubsequentqs").show();
        }
        else {
            //hide craffft subsequent panel
            $("#cageaidalcoholsubsequentqs").hide();
        }
    });

    //CAGEAID Scores
    $("#cageaidalcoholsubsequentqs input:radio").change(function (evt, data) {
        var selectionTotal = 0;
        $("#cageaidalcoholsubsequentqs input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                selectionTotal = selectionTotal + 1;
            }
            else {
                selectionTotal = selectionTotal + 0;
            }

        });
        //select depression severity and recommended management
        $("#<%=tbCageScore.ClientID%>").val(selectionTotal);
        selectCageRisk(selectionTotal);
    });

    function selectCageRisk(selectionTotal) {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getAlcoholRiskNotes",
            data: "{'alcoholScore': '" + selectionTotal + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=tbCageRisk.ClientID%>").val(response.d);
            },
            error: function (response) {
                toastr.error("Error Selecting Alcohol Risk");
            }
        });
    }
</script>