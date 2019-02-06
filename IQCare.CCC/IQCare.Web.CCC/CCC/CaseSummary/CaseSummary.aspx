<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="CaseSummary.aspx.cs" Inherits="IQCare.Web.CCC.CaseSummary.CaseSummary" %>
<%@ Register Src="~/CCC/UC/ucCaseSummary.ascx"  TagName="ucCaseSummary" TagPrefix="uc1"%>
<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx"  TagName="ucPatientBrief" TagPrefix="uc2"%>
<%@ Register Src="~/CCC/UC/ucEvaluation.ascx"  TagName="ucEvaluation" TagPrefix="uc3"%>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
   <uc2:ucPatientBrief ID="ucPatientBrief" runat="server" />
    <style>
        .control-label{text-align: left !important;}
    </style>
      <div class="col-md-12" style="padding-top: 20px">
          <div class="col-md-12 form-group">
                        <label class="required control-label pull-left">Visit Date</label>
                        <div class="col-md-6 form-group">
                            <div class='input-group date ' id='VisitDatedatepicker'>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PersonVisitDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                            </div>
                        </div>
                    </div>
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="myWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="dsSectionOne" data-name="template" class="active complete">
						    <span class="badge">1</span>Case Summary
							<span class="chevron"></span>
					    </li>
					    <li data-step="2" id="dsSectionTwo" class="complete">
						    <span class="badge">2</span>Evaluation
                            <span class="chevron"></span>
					    </li>
					   
				    </ul>
                </div>
                <div class="step-content">
                    <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                             <uc1:ucCaseSummary ID="ucCaseSummary" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="datastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                           
                             <uc3:ucEvaluation ID="ucEvaluation" runat="server" />
                        </div>
                    </div>
                    
                    <div id="prevNextButton" class="actions">
					    <button type="button" class="btn btn-default btn-prev">
						    <span class="glyphicon glyphicon-arrow-left"></span>
                            Prev
					    </button>
					    <button type="button" class="btn btn-primary btn-next" data-last="Complete">
						    Next
						    <span class="glyphicon glyphicon-arrow-right"></span>
					    </button>
				    </div>
                </div>
                
            </div>
        </div>
    </div>
    <script type="text/javascript">

        var Answers = new Array;
        var EvaluationArr = new Array;
        var VisitId = 0;
         VisitId = "<%=visitPatientMasterVisitId%>";
        var VisitDate = "<%=VisitDate%>";
        
        var encounterExists = "<%=PatientEncounterExists%>";
       var enrollmentDate = "<%=DateOfEnrollment%>";
        $('#VisitDatedatepicker').datetimepicker({
             date: VisitDate,
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false
        });
        $('#VisitDatedatepicker').on('dp.change', function (e) {

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
        $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
            var currentStep = data.step;
            var nextStep = 0;
            var visitdate = $("#<%=PersonVisitDate.ClientID%>").val();
            if (data.direction === 'next') {
                nextStep = currentStep += 1;
            }
            else {
                previousStep = nextStep -= 1;
            }
            if (data.step === 1) {
                var error = 0;

                if (moment('' + visitdate + '').isAfter()) {
                    toastr.error("Visit date cannot be a future date.");
                    return false;
                }
                if (visitdate === "" || visitdate === null) {
                    toastr.error("VisitDate is a required field");
                }

                $('#VisitDatedatepicker').data('DateTimePicker').hide();

                CheckifCaseSummaryHasValues();
                var values = Answers.filter((x) => { return x.value.length > 0 });
                if (values != null) {
                    if (values.length > 0) {
                        AddCaseSummaryEncounter(visitdate);
                    }
                    else {
                        toastr.info("No data Saved since Fields are empty");
                        toastr.info("Kindly fill the questions below");
                        evt.preventDefault();
                        return false;
                    }
                }
            }
            else if (data.step == 2) {
                if (data.direction == 'previous') {
                    return;
                }
                else {
                    checkIfEvaluationHasValue();
                    var valuesev =ArrayEvaluation.filter((x) => { return x.value.length > 0 });
                    if (valuesev != null) {
                        if (valuesev.length > 0) {
                            if (VisitId > 0) {
                                console.log(VisitId);
                                AddUpdateEvaluation(VisitId);
                               
                            }
                            
                        }
                    }
                }
            }

        });

   function AddCaseSummaryEncounter (visitDate ) {
        var patientId = <%=PatientId%>;
     var dateOfVisit = $("#PersonVisitDate").val();
        var ServiceAreaId = 203;
        var EncounterType = "CaseSummary";
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
                   
                     VisitId = result;
                     AddUpdateCaseSummary(result);
                     
                 }

                },
                error: function (response) {
                    error = 1;
                    toastr.error("Alcohol and Drug Abuse Screening not Saved");
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';


                }
        });
    }
  
        
           if (encounterExists > 0) {
			//var $wizard = $('#myWizard').wizard();
			//var wizard = $wizard.data('wizard');
			//$wizard.off('click', 'li.complete');
			//$wizard.on('click', 'li', $.proxy(wizard.stepclicked, wizard));

			$('#myWizard').wizard();
			$('#myWizard').find('ul.steps li').toggleClass('complete', true);

			$('#myWizard').on('changed.fu.wizard', function (evt, data) {
				$('#myWizard').find('ul.steps li').toggleClass('complete', true);
			});
		}

    </script>
 
</asp:Content>