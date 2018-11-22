<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CCC/Greencard.Master"  CodeBehind="AdherenceBarriersHome.aspx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.AdherenceBarriersHome" %>
<%@ Register TagPrefix="uc" TagName="tnHIVAwareness" Src="~/CCC/Adherence/ucHIVAwareness.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnUnderstanding" Src="~/CCC/Adherence/ucUnderstanding.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnDailyRoutine" Src="~/CCC/Adherence/ucDailyRoutine.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnPsychosocialCircumstances" Src="~/CCC/Adherence/ucPsychosocialCircumstances.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnScreening" Src="~/CCC/Adherence/ucScreening.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnReferralsandNetworks" Src="~/CCC/Adherence/ucReferralsandNetworks.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<style>
    .control-label{text-align: left !important;}
    .section1loading, .section2loading, .section3loading, .section4loading{position: absolute;width: 100%;height: 100%;margin-left:-15px;z-index:999;background: rgba(204, 204, 204, 0.5);display: none;}
    .adherencebarriersloading{position: absolute;width: 100%;height: 100%;margin-left:-15px;z-index:999;background: rgba(204, 204, 204, 0.5);display: none;}
</style>

    <div class="col-md-12" style="padding-top: 20px"> 
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

        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="abmyWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="dsSectionOne" data-name="template" class="active ">
						    <span class="badge">1</span>Section 1
							<span class="chevron"></span>
					    </li>
					    <li data-step="2" id="dsSectionTwo" >
						    <span class="badge">2</span>Section 2
							<span class="chevron"></span>
					    </li>
					    <li data-step="3" id="dsSectionThree" data-name="" >
						    <span class="badge">3</span>Section 3
							<span class="chevron"></span>
					    </li>

					    <li data-step="4" id="dsSectionFour" data-name="" >
						    <span class="badge">4</span>Section 4
							<span class="chevron"></span>
					    </li>
				    </ul>
                </div>
                <div class="step-content">
                    <div class="step-pane active sample-pane" id="abdatastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                            <uc:tnHIVAwareness ID="HIVAwareness" runat="server" />
                            <uc:tnUnderstanding ID="Understanding" runat="server" />
                            <div class="section1loading"><img src="../../Content/Img/PEPloading.gif" /></div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="abdatastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                            <uc:tnDailyRoutine ID="DailyRoutine" runat="server" />
                            <div class="section2loading"><img src="../../Content/Img/PEPloading.gif" /></div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="abdatastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                        <div class="col-md-12 form-group">
                            <uc:tnPsychosocialCircumstances ID="PsychosocialCircumstances" runat="server" />
                            <div class="section3loading"><img src="../../Content/Img/PEPloading.gif" /></div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="abdatastep4" data-parsley-validate="true" data-show-errors="true" data-step="4">
                        <div class="col-md-12 form-group">
                            <uc:tnScreening ID="Screening" runat="server" />
                            <uc:tnReferralsandNetworks ID="ReferralsandNetworks" runat="server" />
                            <div class="section4loading"><img src="../../Content/Img/PEPloading.gif" /></div>
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
        var mastervisitid;
        var encounterExists = "<%=PatientEncounterExists%>";
        var patientId = <%=PatientId%>;
           
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
             var dob = $("#<%=PersonVisitDate.ClientID%>").val();

            });

  $('#abmyWizard').on('actionclicked.fu.wizard', function (evt, data) {
              var currentStep = data.step;
                var nextStep = 0;
                var previousStep = 0;
                var totalError = 0;
      var stepError = 0;
       var dob = $("#PersonVisitDate").val();

      if (data.direction === 'next') {
          nextStep = currentStep += 1;
      }
      else {
          previousStep = nextStep -= 1;
      }
              
      if (data.step === 1) {
          if ($('#PatientVisitDate').parsley().validate()) {
             
              if (moment('' + dob + '').isAfter()) {
                  toastr.error("Visit date cannot be a future date.");
                  return;
              }
              if (dob === "" || dob === null) {
                  toastr.error("Visit Date is required");
                  evt.preventDefault();
                  return;
              }
            }

          checkifFieldHIVAwarenessHaveValue();
          var values = HivAwarenessArray.filter((x) => { return x.value.length > 0 })
        if (values != null) {
                            if (values.length > 0) {

                                addAdherenceBarriesEncounter(dob);
                                addUpdateHIVAwarenessScreeningData(mastervisitid);
                            }
                            else {
                                toastr.info("Kindly fill the Awareness of HIV Status section");
                              evt.preventDefault();
                            return false;
                            }

                        }
                        else {
                            toastr.info("Kindly fill the Awareness of HIV Status section");
                            evt.preventDefault();
                            return false;
              }

         checkifFieldUHScreeningData();
                        var screeningvalues = UHScreeningData.filter((x) => { return x.value.length > 0 })
                        if (screeningvalues != null) {
                            if (screeningvalues.length > 0) {
                                addAdherenceBarriesEncounter(dob);
                                addUpdateUHScreeningData(mastervisitid);
                            }
                            else {
                                toastr.info("Kindly fill Understanding of HIV infection and ART section ");
                               evt.preventDefault();
                            return false;
                            }

                        }
                        else {
                            toastr.info("Kindly fill Understanding of HIV infection and ART section ");
                           evt.preventDefault();
                            return false;

                        }


               
          

      }

            else if (data.step ==2) {
                if (data.direction == 'previous') {
                    return;
                }
                else {
                    checkifDailyRoutineHasValues();
                    var values = ArrayDailyRoutine.filter((x) => { return x.value.length > 0 })
                    if (values != null) {
                        if (values.length > 0) {
                            AddUpdateDailyRoutine(mastervisitid);
                        }
                        else {
                            toastr.info("Kindly fill the daily routine records since the fields are empty");
                            evt.preventDefault();
                            return false;

                        }

                    }
                    else {
                         toastr.info("Kindly fill the daily routine records since the fields are empty");
                            evt.preventDefault();
                            return false;
                    }
                }
            }
            else if (data.step == 3) {
                if (data.direction == 'previous') {
                    return;
                }
                else {

                    checkifUpdatePCScreeningDateHasValues();
                    var values = PCScreeningDataArray.filter((x) => { return x.value.length > 0 })
                    if (values != null) {
                        if (values.length > 0) {
                            addUpdatePCScreeningData(mastervisitid);
                        }
                        else {
                            toastr.info("Kindly fill the Psychosocial Criteria Screening Data ");
                            evt.preventDefault();
                            return false;

                        }
                    }
                    else {
                         toastr.info("Kindly fill the Psychosocial Criteria Screening Data ");
                            evt.preventDefault();
                            return false;

                    }
                }
            }
            else if (data.step == 4) {
                if (data.direction === 'previous') {
                    return;
                }
                else {
                    CheckifRNSScreeningHasValues();
                    var values = UpdateRNSScreeningArray .filter((x) => { return x.value.length > 0 })
                    if (values != null) {
                        if (values.length > 0) {
                            addUpdateRNScreeningData();
                        }
                        else {
                           toastr.info("Kindly fill the Referral Networks data");
                        evt.preventDefault();
                        return false;

                        }


                    }
                    else {
                     toastr.info("Kindly fill the Referral Networks data");
                        evt.preventDefault();
                        return false;
                    }
                }
            }
            //$('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
            //$('#abmyWizard').find('#dsSectionTwo').toggleClass('complete', true);
            //$('#abmyWizard').find('#dsSectionThree').toggleClass('complete', true);
        });   //$('#abmyWizard').find('#dsSectionFour').toggleClass('complete', true);
       

     

        if (encounterExists > 0) {
			//var $wizard = $('#myWizard').wizard();
			//var wizard = $wizard.data('wizard');
			//$wizard.off('click', 'li.complete');
			//$wizard.on('click', 'li', $.proxy(wizard.stepclicked, wizard));

			$('#abmyWizard').wizard();
			$('#abmyWizard').find('ul.steps li').toggleClass('complete', true);

			$('#abmyWizard').on('changed.fu.wizard', function (evt, data) {
				$('#abmyWizard').find('ul.steps li').toggleClass('complete', true);
			});
		}
        $("#loadAdherenceBarriers").click(function () {
            $(".adherencebarriersloading").show();
            });


           // $('.awarenessloading').show();
          //  GetPatientHIVAwareneseScreening();
          
            //GetScreeeningData();
           // GetReferralNetworksData();
              //GetHivInfectionData();
          //  GetPyschosocialPatientNotesandScreening();
           // GetPatientDailyRoutineNotes();
});

        
            
        
 function addAdherenceBarriesEncounter (visitDate ) {
     var patientId = <%=PatientId%>;

     
     var dateOfVisit = $("#PersonVisitDate").val();
        var ServiceAreaId = <%=serviceAreaId%>;
        var EncounterType = "Adherence-Barriers";
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
                   

                     mastervisitid = result;
                   //  addUpdateDepressionScreeningData(result);
                 }

                },
                error: function (response) {
                    error = 1;
                    toastr.error("Adherence Barriers Screening not Saved");
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';


                }
        });
    }
      
            //$('#abmyWizard').wizard();
            //$('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
            //$('#abmyWizard').find('#dsSectionTwo').toggleClass('complete', true);
            //$('#abmyWizard').find('#dsSectionThree').toggleClass('complete', true);
            //$('#abmyWizard').find('#dsSectionFour').toggleClass('complete', true);
      
       
    </script>
</asp:Content>