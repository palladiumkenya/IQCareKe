<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCAGEAID.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucCAGEAID" %>
<style>
    /*.rbList{float: right;}
    .rbList input{margin-left: 5px;}*/
    .cagearbList td { text-align: left;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
    <div class="col-md-12" id="PatientVisitDate" data-parsley-validate="true" data-show-errors="true" >
         <div class="panel panel-info">
                <div class="panel-body">
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
            </div>
        </div>
               
        
    
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
     <button type="button" id="submitdata" class="btn btn-primary btn-next" data-last="Complete"/>

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
                if (dob === "" || dob === null) {
                    toastr.error("VisitDate is a required field");
                }

            }
              
                       $('#VisitDatedatepicker').data('DateTimePicker').hide();
                    checkifFieldsHavevalue();
               var values = Answers.filter((x) => { return x.value.length > 0 });
            if (values != null) {
                if (values.length > 0) {
                    addAlcoholScreeningEncounter(dob);
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
                
          
      
       
       
        
        });
     var enrollmentDate = "<%=DateOfEnrollment%>";



    });

    function checkifFieldsHavevalue() {
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
            if (screeningValue > 0) {
                Answers.push({ 'Id': screeningCategory, 'value': screeningValue });
            }
        });

        $("#cageaidscreening input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('cage', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;

            Answers.push({ 'Id': categoryId, 'value': clinicalNotes});
        });

        $("#cageaidscreening textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('cage', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;

             Answers.push({ 'Id': categoryId, 'value': clinicalNotes});
        });


    }
   // var currentStep;
    //$("#scmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
    //    currentStep = data.step;
    //    if (currentStep == 2) {
    //        addUpdateCAGEAIDScreeningData();
    //    }
    //});

    function addAlcoholScreeningEncounter (visitDate ) {
        var patientId = <%=PatientId%>;
     var dateOfVisit = $("#PersonVisitDate").val();
        var ServiceAreaId = <%=serviceAreaId%>;
        var EncounterType = "AlcoholandDrugAbuseScreening";
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
                    
                   
                     addUpdateCAGEAIDScreeningData(result);
                 }

                },
                error: function (response) {
                    error = 1;
                    toastr.error("Alcohol and Drug Abuse Screening not Saved");
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';


                }
        });
    }

    function addUpdateCAGEAIDScreeningData(mastervisitid)
    {
        var error = 0;
        $("#cageaidscreening .cagerbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = mastervisitid;
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
        $("#cageaidscreening textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('cage', '');
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
            toastr.success("Alcohol and Drug Use Screening Saved");
             window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
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