<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCRAFFT.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucCRAFFT" %>
<style>
    /*.rbList{float: right;}
    .rbList input{margin-left: 5px;}*/
    .cageaidrbList td { text-align: left;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12" id="crafftscreening">
		<div class="panel panel-info" id="crafftinitialscreening">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span>CRAFFT Alcohol and Drug Abuse Screening in Adolescents</span></label>
				</div>
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">During the past 12 months, did you</span></label>
				</div>
				<div class="col-md-12 form-group" id="alcoholfrequencyquestions">
                    <asp:PlaceHolder ID="PHCRAFFTFrequency" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>

        <!-- Alcohol screening -->
        <div class="panel panel-info" id="crafftsubsequentscreening">
			<div class="panel-body">
				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCRAFFTAlcoholScreening" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHCRAFFTScore" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHCrafftRisk" runat="server"></asp:PlaceHolder>
                        </div>
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
                        <asp:PlaceHolder ID="PHCRAFFTNotes" runat="server"></asp:PlaceHolder>
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
            addUpdateCRAFFTScreeningData();
        }
    });
    //save crafft data
    function addUpdateCRAFFTScreeningData()
    {
        //save radio button list data
        var error = 0;
        $("#crafftscreening .crafftrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('crafft', '');
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
        $("#crafftscreening input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('crafft', '');
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
        $("#crafftscreening textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('crafft', '');
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

    $("#crafftinitialscreening input:radio").change(function (evt, data) {
        var selectionTotal = 0;
        $("#crafftinitialscreening input[type=radio]:checked").each(function () {
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
        if (selectionTotal >= 1) {
            $("#crafftsubsequentscreening").show();
        }
        else {
            //reset subsequent panel controls
            $("#crafftsubsequentscreening input[type=radio]:checked").each(function () {
                $(this).prop('checked', false);
                $("#<%=tbCrafftScore.ClientID%>").val("");
                $("#<%=tbCrafftRisk.ClientID%>").val("");
            });
            //hide craffft subsequent panel
            $("#crafftsubsequentscreening").hide();
        }
    });

    $(document).ready(function () {
        var selectionTotal = 0;
        $("#crafftinitialscreening input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                selectionTotal = selectionTotal + 1;
            }
        });
        if (selectionTotal >= 1) {
            $("#crafftsubsequentscreening").show();
        }
        else {
            //reset subsequent panel controls
            $("#crafftsubsequentscreening input[type=radio]:checked").each(function () {
                $(this).prop('checked', false);
                $("#<%=tbCrafftScore.ClientID%>").val("");
                $("#<%=tbCrafftRisk.ClientID%>").val("");
            });
            //hide craffft subsequent panel
            $("#crafftsubsequentscreening").hide();
        }
    });

    //Get CRAFFT SCORES
    $("#crafftsubsequentscreening input:radio").change(function (evt, data) {
        var selectionTotal = 0;
        $("#crafftsubsequentscreening input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                selectionTotal = selectionTotal + 1;
            }
            else
            {
                selectionTotal = selectionTotal + 0;
            }
            
        });
        //select depression severity and recommended management
        $("#<%=tbCrafftScore.ClientID%>").val(selectionTotal);
        selectCrafftRisk(selectionTotal);
    });

    function selectCrafftRisk(selectionTotal) {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getAlcoholRiskNotes",
            data: "{'alcoholScore': '" + selectionTotal + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=tbCrafftRisk.ClientID%>").val(response.d);
            },
            error: function (response) {
                toastr.error("Error Selecting Alcohol Risk");
            }
        });
    }
</script>