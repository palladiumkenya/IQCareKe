<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDailyRoutine.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucDailyRoutine" %>

<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Daily Routine</span></label>
				</div>

				<div class="col-md-12 form-group" id="dailyroutinescreening">
					<asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    var ArrayDailyRoutine = new Array;
    //$("#abmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
    //    var currentStep = data.step;
    //    if (currentStep == 2) {
    //        checkifDailyRoutineHasValues();
    //        var values =  ArrayDailyRoutine.filter((x) => { return x.value.length > 0 })
    //                  if (values != null) {
    //                      if (values.length > 0) {
    //                          AddUpdateDailyRoutine(mastervisitid);
    //                      }
    //                      else {
    //                          toastr.info("No data Saved since Fields are empty");
                                 
    //                      }
            
    //    }
    //});

    function AddUpdateDailyRoutine(mastervisitid) {
        var error = 0;
            $("#dailyroutinescreening textarea").each(function () {
                var categoryId = $(this).attr('id');
                var patientId = <%=PatientId%>;
                var patientMasterVisitId = mastervisitid;
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
                toastr.success("Daily routine saved");
            }
    }

    function checkifDailyRoutineHasValues() {
        $("#dailyroutinescreening textarea").each(function () {
            var categoryId = $(this).attr('id');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;

            ArrayDailyRoutine.push({ 'Id': categoryId, 'value': clinicalNotes});

        });
    }

    function GetPatientDailyRoutineNotes() {

        var patientId=<%=PatientId%>;
          $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getPatientNotes",
            data: "{'PatientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
              success: function (response) {

                     var res = JSON.parse(response.d);
                if (res != null) {
                      //alert(JSON.stringify(response));
                      $.each(JSON.parse(response.d), function (index, value) {
                          inputnotes = this.ClinicalNotes;
                          if ($("#" + this.NotesCategoryId).length > 0) {
                              $("#" + this.NotesCategoryId).val(inputnotes);
                          }
                      });
                  }
            },
            error: function (response) {
                toastr.error("Notes could not be loaded");
            }
        });


    }
    $(document).ready(function () {
        
        GetPatientDailyRoutineNotes();
      
    });
    jQuery(function ($) {
        var dailyRoutineId = <%=dailyRoutineId%>;
        if (dailyRoutineId > 0) {
            $('#abmyWizard').wizard();
            $('#abmyWizard').find('#dsSectionTwo').toggleClass('complete', true);
            $('#abmyWizard').on('changed.fu.wizard', function (evt, data) {
                $('#abmyWizard').find('#dsSectionTwo').toggleClass('complete', true);
            });
        }
    });
</script>