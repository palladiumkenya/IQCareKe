<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viralload.aspx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.viralload" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<style>
    .viralloadloading{position: absolute;width: 100%;height: 100%;margin-left:-15px;z-index:999;background: rgba(204, 204, 204, 0.5);display: none;}
</style>
<form runat="server">
    <div class="col-md-12 form-group viralloadcontainer">
	    <div class="col-md-12">
		    <div class="panel panel-info">
			    <div class="panel-body">
				    <div class="col-md-12 form-group">
					    <label class="control-label pull-left"><span class="text-primary">Viral Load</span></label>
				    </div>

				    <div class="col-md-12 form-group" id="mmasquestions">
                        <asp:PlaceHolder ID="PHEndSessionViralLoad" runat="server"></asp:PlaceHolder>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <div class="viralloadloading"><img src="../../Content/Img/PEPloading.gif" /></div>
</form>
<script type="text/javascript">
    $("#eahmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        //alert(currentStep);
        if (currentStep == 5) {
            addUpdateViralLoadData();
            updateCancellingStatus();
            
        
        }
    });
    function updateCancellingStatus() {
         var patientMasterVisitId = GetURLParameter('visitId');
        $.ajax({
           
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/addCancellingVisitStatus",
            data: "{'status':'Complete','PatientMasterVisitId':'" + patientMasterVisitId + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.info("Ongoing adherence councelling closed");
                 window.location.href ='<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
               // checkScreeningStatus();
            },
            error: function (response) {
                toastr.error(response.d, "Error closing ongoing adherence councelling");
            }
        });
    }
    function addUpdateViralLoadData() {
        var error = 0;
        $("#eahdatastep5 .endvlrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('endvlrbList', '');
         
            //alert(screeningCategory);
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
        $("#eahdatastep5 textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('endViralLoadTb', '');
           // alert(categoryId);
            var patientId = <%=PatientId%>;
           // alert(patientId);
            var patientMasterVisitId = GetURLParameter('visitId');
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            if (categoryId > 1) {
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
            }
        });
        if (error == 0) {
            toastr.success("Viral Load Saved");
        }
    }
    $(document).ready(function () {
        $('.viralloadloading').show();
        debugger;
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = GetURLParameter('visitId');
      
        $.ajax({
            type: "POST",
                url: "../WebService/PatientClinicalNotesService.asmx/getPatientNotesByVisitId",
            data: "{'PatientId': '" + patientId + "','PatientMasterVisitId':'" + patientMasterVisitId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.d != null) {
                   
                    var result = JSON.parse(response.d);
                    if (result != null) {
                        //alert(JSON.stringify(response));
                        if (result.length > 0) {
                            $.each(result, function (index, value) {

                            

                                inputnotes = this.ClinicalNotes;


                                if ($("#endViralLoadTb" + this.NotesCategoryId).length > 0) {
                                    debugger;
                                    $("#endViralLoadTb" + this.NotesCategoryId).val(inputnotes);
                                }


                            });
                        }
                    }
                }
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
                
                if (response.d != null) {
                    var result = JSON.parse(response.d);
                    if (result != null) {
                        if (result.length > 0) {
                            //alert(JSON.stringify(response));
                            $.each(result, function (index, value) {
                                if ($("#endvlrbList" + this.ScreeningCategoryId).length > 0) {
                                    $("input:radio[name='endvlrbList" + this.ScreeningCategoryId + "'][value='" + this.ScreeningValueId + "']").attr("checked", true);
                                }
                            });
                        }
                    }
                }
                checkViralLoadButtonsOnCtrls();
            },
            error: function (response) {
                toastr.error("Screening could not be loaded");
            }
        });
    });
    function checkViralLoadButtonsOnCtrls() {
        $('.viralloadloading').hide();
    }
</script>
