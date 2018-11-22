<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDepressionScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucDepressionScreening" %>
<style>
    .rbListDepression{float: right;}
    .rbListDepression input{margin-left: 5px;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
  
    <div class="col-md-12" id="PatientVisitDate" data-parsley-validate="true" data-show-errors="true">
            
            <div class="col-md-12"><label class="required control-label pull-left">Visit Date</label></div>

            <div class="col-md-4 form-group">
                <div class='input-group date' id='VisitDatedatepicker'>
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PersonVisitDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                </div>
            </div>
        </div>
      
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

        <button type="button" id="submitdata" class="btn btn-primary btn-next" data-last="Complete"/>
	</div>
      
</div>
<script type="text/javascript"> 
    var contain = "";
    var Answers = new Array;
    var VisitDate = "<%=VisitDate%>";

    

    $(document).ready(function () {
        $('#VisitDatedatepicker').datetimepicker({
        format: 'DD-MMM-YYYY',
            date: VisitDate,
           allowInputToggle: true,
           useCurrent: false

});
     
     $('#VisitDatedatepicker').datetimepicker({
            format: 'DD-MMM-YYYY',
           allowInputToggle: true,
           useCurrent: false
        });


        $('#VisitDatedatepicker').on('dp.change', function (e) {

              if( !e.oldDate || !e.date.isSame(e.oldDate, 'day')){
                 $(this).data('DateTimePicker').hide();
                 }

            var vDate = moment($("#PersonVisitDate").val(), 'DD-MMM-YYYYY').toDate();
            var validDateOfVisit = moment(vDate).isBefore(enrollmentDate);
            var futuredate = moment(vDate).isAfter(new Date());
            if (futuredate) {
                $("#<%=PersonVisitDate.ClientID%>").val('');
                toastr.error("Future dates not allowed!");
               
                return false;
            }
            if (validDateOfVisit) {
                toastr.error("VISIT date CANNOT be before ENROLLMENT date");
                $("#<%=PersonVisitDate.ClientID%>").val('');
                return false;
            }

        });

        $("#submitdata").click(function () {

            if ($('#PatientVisitDate').parsley().validate()) {
                var dob = $("#<%=PersonVisitDate.ClientID%>").val();
                if (moment('' + dob + '').isAfter()) {
                    toastr.error("Visit date cannot be a future date.");
                    return false;
                } 
          $('#VisitDatedatepicker').data('DateTimePicker').hide();
                    
                    checkifFieldsHavevalue();
                    var values = Answers.filter((x) => {return x.value.length > 0})
                if (values != null) {
                    if (values.length > 0) {
                        addDepressionScreeningEncounter(dob);
                    }
                    else {
                        toastr.info("No data Saved since Fields are empty");
                        window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
                    }


                }
                else {
                    return;
                }
                   //addUpdateDepressionScreeningData();
                  //getDepressionScreeningData();                   
                
            } 
       
       
        
        });
     var enrollmentDate = "<%=DateOfEnrollment%>";
    //var currentStep;
    //$("#scmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
    //    currentStep = data.step;
    //    if (currentStep == 1) {
    //        addUpdateDepressionScreeningData();
    //        getDepressionScreeningData();
    //    }
    //});



    });
    function checkifFieldsHavevalue() {
        
        $(".rbListDepression").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id');
            var checkedValue = $('#' + screeningCategory + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
            if (screeningValue > 0) {

                Answers.push({ 'Id': screeningCategory, 'value': screeningValue });
            }

        });
        $("#depressionscreening input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('notes', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;


            Answers.push({ 'Id': categoryId, 'value': clinicalNotes});
        });
       

       
    }

    function addDepressionScreeningEncounter (visitDate ) {
        var patientId = <%=PatientId%>;
     var dateOfVisit = $("#PersonVisitDate").val();
        var ServiceAreaId = <%=serviceAreaId%>;
        var EncounterType = "DepressionScreening";
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
               
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/GetPatientMasterVisitId",
                data: "{'PatientId': '" + patientId + "','ServiceAreaId':'"+ServiceAreaId+"','UserId':'"+userId+"','EncounterType':'"+EncounterType+"','visitDate': '" + dateOfVisit + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
             success: function (response) {
                
                 var res = JSON.parse(response.d);
                 if (res.Result > 0) {

                     var result = res.Result;
                    
                     toastr.success(res.Msg);
                   
                    
                     addUpdateDepressionScreeningData(result);
                 }

                },
                error: function (response) {
                    error = 1;
                    toastr.error("Depression Screening not Saved");
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';


                }
        });
    }

    function getDepressionScreeningData() {
       
          var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var userId = <%=userId%>;

        $.ajax({
               
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/GetDepressionScreeningData",
                data: "{'PatientId': '" + patientId + "','PatientMasterVisitId': '" + patientMasterVisitId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
             success: function (response) {
                 debugger;
                 var res = response.d;
                    console.log(Json.Parse(response.d));
              

                },
                error: function (response) {
                    error = 1;
                }
            });
    }
    function addUpdateDepressionScreeningData(mastervisitid)
    {
        var error = 0;
        $(".rbListDepression").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId =mastervisitid;
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
            toastr.success("Depression Screening Saved");
           window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
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
        if (selectionTotal => 1) {
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
        if (selectionTotal => 1) {
            $("#phq9panel").show();
        }
        else {
            $("#phq9panel").hide();
        }
    });
</script>