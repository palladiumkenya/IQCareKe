﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUnderstanding.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucUnderstanding" %>

<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Understanding of HIV infection and ART</span></label>
				</div>

				<div class="col-md-12 form-group" id="UnderstandingHIVInfection">
					<asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    var UHScreeningData = new Array();
    //$("#abmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
    //    var currentStep = data.step;
    //    if (currentStep == 1) {
              
    //              checkifFieldUHScreeningData();
    //             var values = UHScreeningData.filter((x) => {return x.value.length > 0})
    //            if (values != null) {
    //                if (values.length > 0) {
    //                     addUpdateUHScreeningData();
    //                }
    //                else {
    //                    toastr.info("No data Saved since Fields are empty");
    //                    return false;
    //                }

    //            }
           
    //    }
    //});
    function checkifFieldUHScreeningData() {
        $("#UnderstandingHIVInfection input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
              if (screeningValue > 0) {
                 UHScreeningData.push({ 'Id': screeningCategory, 'value': screeningValue });
            }


        });

    }
    function addUpdateUHScreeningData(mastervisitid) {
        var error = 0;
        $("#UnderstandingHIVInfection input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId =mastervisitid;
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
            toastr.success("Understanding of HIV infection and ART Saved");
        }
    }

    function GetHivInfectionData() {
        var patientId = <%=PatientId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/getPatientScreening",
            data: "{'PatientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                var res = JSON.parse(response.d);
                if (res != null) {
                    $.each(JSON.parse(response.d), function (index, value) {
                        if ($("#" + this.ScreeningCategoryId).length > 0) {
                            var radioBtns = "#" + this.ScreeningCategoryId;
                            $(radioBtns + " input:radio[value='" + this.ScreeningValueId + "']").attr("checked", true);
                        }
                    });
                }
            },
            error: function (response) {
                toastr.error("Screening could  not be loaded");
            }
        });
    }
   $(document).ready(function () {
      
        GetHivInfectionData();
        
    });
    jQuery(function ($) {
        var uId = <%=understandingId%>;
        if (uId > 0) {
            $('#abmyWizard').wizard();
            $('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
            $('#abmyWizard').on('changed.fu.wizard', function (evt, data) {
                $('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
            });
        }
    });
</script>