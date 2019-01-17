<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucEvaluation.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucEvaluation" %>
<div class="box box-default box-solid" id="divEvaluation">
 <div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Adherence and Treatment Failure Evaluation</span></label>
             	</div>

				<div class="col-md-12 form-group" id="evaluation">
					<asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
            	<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Other Relevant ART History</span></label>
             	</div>

				<div class="col-md-12 form-group" id="otherARTHistory">
					<asp:PlaceHolder ID="ARTHistoryQuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
</div>
<script type="text/javascript">
    var ArrayEvaluation = new Array;
    var ScreeningArray = new Array;
    function checkIfEvaluationHasValue() {
        ScreeningArray.length = 0;
        ArrayEvaluation.length = 0;
        $("#evaluation input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;

            ScreeningArray.push({ 'Id': screeningCategory, 'value': screeningValue });
        });
        $("#evaluation textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('notes', '');
            var patientId = <%=PatientId%>;
           
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;

            ArrayEvaluation.push({ 'Id': categoryId, 'value': clinicalNotes});
        });
        $("#otherARTHistory textarea").each(function () {
            var categoryId = $(this).attr('id').replace('notes', '');
            //alert(categoryId);
            var patientId = <%=PatientId%>;
         
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
             ArrayEvaluation.push({ 'Id': categoryId, 'value': clinicalNotes});
        });
   }

    function AddUpdateEvaluation(mastervisitid) {
        var ScreeningData = new Array;
        var ClinicalNotesData = new Array;
        var error = 0;
        $("#evaluation input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var rdIdValue = $(this).attr('id');
            // alert(screeningValue);
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            if (screeningValue > 0) {
                ScreeningData.push({ 'Id': rdIdValue, 'screeningType': screeningType, 'screeningCategory': screeningCategory, 'screeningValue': screeningValue, 'userId': userId });
            }
        });
        if (ScreeningData.length > 0) {
                var patientId = <%=PatientId%>;
                var patientMasterVisitId = mastervisitid;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/AddUpdateScreeningRecord",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','ScreeningData':'" + JSON.stringify(ScreeningData) + "'}",
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
        $("#evaluation textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('notes', '');
            //alert(categoryId);
            var patientId = <%=PatientId%>;
            
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            ClinicalNotesData.push({'notesCategoryId': categoryId, 'clinicalNotes': clinicalNotes, 'serviceAreaId': serviceAreaId, 'userId': userId  });
        });
        $("#otherARTHistory textarea").each(function () {
            var categoryId = $(this).attr('id').replace('notes', '');
            //alert(categoryId);
            var patientId = <%=PatientId%>;
          
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
           ClinicalNotesData.push({'notesCategoryId': categoryId, 'clinicalNotes': clinicalNotes, 'serviceAreaId': serviceAreaId, 'userId': userId  });;
        });
        if (ClinicalNotesData.length > 0) {
              var patientId = <%=PatientId%>;
            var patientMasterVisitId = mastervisitid;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientClinicalNotesService.asmx/AddPatientClinicalNotesRecord",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','clinicaldata':'" + JSON.stringify(ClinicalNotesData)+ "'}",
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
     
        if (error == 1) {
            toastr.error("Not all Data was saved");
        }
        else {
            toastr.info("Evaluation Data was saved");
            window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
        }
    }
      
    <%--$("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 2) {
            var error = 0;
            $("#evaluation input[type=radio]:checked").each(function () {
                var screeningValue = $(this).val();
                // alert(screeningValue);
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
            $("#evaluation textarea").each(function () {
                var categoryId = ($(this).attr('id')).replace('notes','');
                //alert(categoryId);
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
             $("#otherARTHistory textarea").each(function () {
                var categoryId = $(this).attr('id').replace('notes','');
                //alert(categoryId);
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
                toastr.success("Case Evaluation saved");
            }
        }
    });--%>
 
<%--    jQuery(function ($) {
        var evaluationId = <%=evaluationId%>;
        if (evaluationId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionTwo').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
            $('#myWizard').find('#dsSectionTwo').toggleClass('complete', true);
            });
        }
    });--%>
</script>
