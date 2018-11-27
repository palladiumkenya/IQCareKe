<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGBVScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucGBVScreening" %>
<style>
    /*.rbList{float: right;}
    .rbList input{margin-left: 5px;}*/
    .cageaidrbList td { text-align: left;}
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
               


	<div class="col-md-12" id="gbvscreening">

        <div class="panel panel-info">
			<div class="panel-body">
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">SEXUAL AND GENDER BASE VIOLENCE</span></label>
				</div>
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span>This form should be used at every visit to screen patients for Gender Based Violence for all patients accessing care at this facility</span></label>
				</div>
				<div class="col-md-12 form-group" id="gbvquestionscontainer">
                    <asp:PlaceHolder ID="PHgbvquestions" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
    </div>

      <button type="button" id="submitdata" class="btn btn-primary btn-next" data-last="Complete"/>
</div>
<script type="text/javascript">
    //var currentStep;
    //$("#scmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
    //    currentStep = data.step;
    //    if (currentStep == 3) {
    //        addUpdateFBVScreeningData();
    //    }
    //});

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

            if (!e.oldDate || !e.date.isSame(e.oldDate, 'day')) {
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
                    var values = Answers.filter((x) => {return x.value.length > 0})
            if (values != null) {
                if (values.length > 0) {
                    addGBVScreeningEncounter(dob);

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
        $("#gbvscreening .gbvrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('gbv', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }

         if (screeningValue > 0) {
                Answers.push({ 'Id': screeningCategory, 'value': screeningValue });
            }
        });

    }
      function addGBVScreeningEncounter (visitDate ) {
        var patientId = <%=PatientId%>;
     var dateOfVisit = $("#PersonVisitDate").val();
        var ServiceAreaId = <%=serviceAreaId%>;
        var EncounterType = "GBVScreening";
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
                    
                    
                     addUpdateGBVScreeningData(result);
                 }

                },
                error: function (response) {
                    error = 1;
                    toastr.error("GBV Screening not Saved");
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';


                }
        });
    }

  
    function addUpdateGBVScreeningData(mastervisitid)
    {
        var error = 0;
        $("#gbvscreening .gbvrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = mastervisitid;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('gbv', '');
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
        if (error == 0) {
            toastr.success("GBV Screening Saved");
             window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';

        }
    }
</script>