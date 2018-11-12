<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCaseSummary.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucCaseSummary" %>
<%@ Register TagPrefix="uc" TagName="PatientTriageSummary" Src="~/CCC/UC/ucPatientTriageSummary.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<div class="box box-default box-solid" id="divCaseSummary">
 <div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Case Summary</span></label>
				</div>

				<div class="col-md-12 form-group" id="casesummary">
					<asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
                <%--Lab Results--%>
                <div>
                      <div class="col-md-12 form-group" style="margin-top: 30px;">
                        <div class="col-md-12 col-xs-12 col-sm-12">
	                        <div id="LabResults" class="panel panel-primary">
		                        <div class="panel-heading">Lab Results</div>
		                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
			                        <table id="dtlLabResults" class="table table-bordered table-striped" style="width: 100%">
				                        <thead>
					                        <tr>
                                               <th><span class="text-primary">Date</span></th>
						                       <%-- <th><span class="text-primary">VisitID</span></th>--%>
						                        <th><span class="text-primary">Date</span></th>
						                        <th><span class="text-primary">Investigation(s)</span></th>
						                        <th><span class="text-primary">Results</span></th>
                                               <%-- <th><span class="text-primary">viral Load</span></th>--%>
						                       
					                        </tr>
				                        </thead>
				                        <tbody></tbody>
			                        </table>
		                        </div>
	                        </div>
                            </div>
	                </div>
                </div>
                <%--Regimen Changes--%>
                <div>
                      <div class="col-md-12 form-group" style="margin-top: 30px;">
                        <div class="col-md-12 col-xs-12 col-sm-12">
	                        <div id="RegimenChanges" class="panel panel-primary">
		                        <div class="panel-heading">Current Regimen</div>
		                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
			                        <table id="dtlRegimenChanges" class="table table-bordered table-striped" style="width: 100%">
				                        <thead>
					                        <tr>
						                        <th><span class="text-primary">Date</span></th>
						                        <th><span class="text-primary">Regimen</span></th>
						                        <th><span class="text-primary">Switch</span></th>
						                    </tr>
				                        </thead>
				                        <tbody></tbody>
			                        </table>
		                        </div>
	                        </div>
                            </div>
	                </div>
                </div>
                 <%--OIS--%>
                <div>
                      <div class="col-md-6 form-group" style="margin-top: 30px;">
                        <div class="col-md-12 col-xs-12 col-sm-12">
	                        <div id="OIS" class="panel panel-primary">
		                        <div class="panel-heading">OIS</div>
		                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
			                        <table id="dtlOIS" class="table table-bordered table-striped" style="width: 100%">
				                        <thead>
					                        <tr>
						                        <th><span class="text-primary">Date</span></th>
						                        <th><span class="text-primary">OIS</span></th>
						                    </tr>
				                        </thead>
				                        <tbody></tbody>
			                        </table>
		                        </div>
	                        </div>
                            </div>
	                </div>
                </div>
                 <%--Condition--%>
                <div>
                      <div class="col-md-6 form-group" style="margin-top: 30px;">
                        <div class="col-md-12 col-xs-12 col-sm-12">
	                        <div id="Condition" class="panel panel-primary">
		                        <div class="panel-heading">Condition</div>
		                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
			                        <table id="dtlCondition" class="table table-bordered table-striped" style="width: 100%">
				                        <thead>
					                        <tr>
						                        <th><span class="text-primary">Date</span></th>
						                        <th><span class="text-primary">Condition</span></th>
						                    </tr>
				                        </thead>
				                        <tbody></tbody>
			                        </table>
		                        </div>
	                        </div>
                            </div>
	                </div>
                </div>
				
                <%--Anthropoemetric Measurements--%>
                <div>
                      <div class="col-md-12 form-group" style="margin-top: 30px;">
                        <div class="col-md-12 col-xs-12 col-sm-12">
	                        <div id="presentingComplaintsTable" class="panel panel-primary">
		                        <div class="panel-heading">Anthropoemetric Measurements</div>
		                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
			                        <table id="dtlPreviousTriage" class="table table-bordered table-striped" style="width: 100%">
				                        <thead>
					                        <tr>
						                        <th><span class="text-primary">Date</span></th>
						                        <th><span class="text-primary">Height</span></th>
						                        <th><span class="text-primary">Weight</span></th>
						                        <th><span class="text-primary">MUAC</span></th>
                                                <th><span class="text-primary">Systolic Bp</span></th>
						                        <th><span class="text-primary">Diastolic Bp</span></th>
						                        <th><span class="text-primary">Temp</span></th>
						                        <th><span class="text-primary">Pulse Rate</span></th>
                                                <th><span class="text-primary">Respiration Rate</span></th>
						                        <th><span class="text-primary">SPOC (%)</span></th>
					                        </tr>
				                        </thead>
				                        <tbody></tbody>
			                        </table>
		                        </div>
	                        </div>
                            </div>
	                </div>
                </div>

			</div>
		</div>
	</div>
</div>
</div>
<script type="text/javascript">
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 2) {
            var error = 0;
            $("#casesummary textarea").each(function () {
                var categoryId = $(this).attr('id');
               // alert(categoryId);
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
                toastr.success("Case Summary saved");
            }
        }
    });

    jQuery(function ($) {
        var caseSummaryId = <%=caseSummaryId%>;
        if (caseSummaryId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionTwo').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
            $('#myWizard').find('#dsSectionTwo').toggleClass('complete', true);
            });
        }
    });

    $(document).ready(function () {
        // alert("Page iko ready sasa");
       // $.hivce.loader('show');
        //GetClinicalSummaryData();
        var previousTriage = $('#dtlPreviousTriage').DataTable({
        ajax: {
            type: "POST",
            url: "../WebService/PatientEncounterService.asmx/LoadVitalSigns",
            dataSrc: 'd',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        },
        paging: false,
        searching: false,
        info: false,
        ordering: false,
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ]
        });

        // Load Lab Results
         var previousTriage = $('#dtlLabResults').DataTable({
        ajax: {
            type: "POST",
            url: "../WebService/LabService.asmx/ExtruderSpecificResults",
            dataSrc: 'd',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        },
        paging: false,
        searching: false,
        info: false,
        ordering: false,
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ]
        });


    });
   
  
</script>