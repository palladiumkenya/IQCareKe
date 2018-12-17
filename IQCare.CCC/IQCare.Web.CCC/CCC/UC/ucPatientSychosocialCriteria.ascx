<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientSychosocialCriteria.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientSychosocialCriteria" %>
<div class="col-md-12" style="padding-top:4%">
    

    <div class="alert alert-danger" id="divMessage">
        <a href="#" class="close" data-dismiss="alert">&times;</a>
        <p id="danger"></p>
    </div>
    <div class="alert alert-success" id="divsuccess">
        <a href="#" class="close" data-dismiss="alert">&times;</a>
        <p id="success"></p>
    </div>


    <div class="panel panel-default" id="psychoCriteria" data-state="collapsed">
    <div class="panel-heading">
        <div class="panel-title">
            <h5>A. Psychosocial/Knowledge Criteria (applies to patients and caregivers)</h5>
        </div>
    </div>
    <div class="panel-body">
       
        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">1. Understands the nature of HIV infection and benefits of ART?</label>
           </div>
           <div class="col-md-2  pull-right">   
                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="benefitARTlbl1">
                    <input id="radiobenefitsARTYes" class="sr-only benefitsART"  name="benefitsART" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="benefitARTlbl2">
                    <input id="radiobenefitsARTNo" class="sr-only benefitsART"  name="benefitsART" type="radio" value="0" >No
                </label>
                 <!-- <input type="checkbox" class="onoffswitch-checkbox" id="benefitsART" value="0" />--> 
            </div>
       </div>
        <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">2. Has screened negative for alcohol or other drug use disorder, or is stable on treatment (see Section
4.6)</label>
           </div>
           <div class="col-md-2 pull-right">
               <!--<input type="checkbox" class="onoffswitch-checkbox" id="screenAlcohol" value="0"/>-->
               <label class="radio-custom radio-inline highlight" data-initialize="radio" id="screenAlcohollbl1">
                    <input id="screenAlcoholYes" class="sr-only"  name="screenAlcohol" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="screenAlcohollbl2">
                    <input id="screenAlcoholNo" class="sr-only "  name="screenAlcohol" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">3. Has screened negative for depression or other psychiatric illness, or is stable on treatment (see
Section 4.6)</label>
           </div>
           <div class="col-md-2 pull-right">

              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="depression" value="0"/>-->
                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="depressionlbl1">
                    <input id="depressionYes" class="sr-only"  name="depression" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="depressionlbl2">
                    <input id="depressionNo" class="sr-only "  name="depression" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">4. Is willing to disclose/has disclosed HIV status, ideally to a family member or close friend?</label>
           </div>
           <div class="col-md-2">
               <!--<input type="checkbox" class="onoffswitch-checkbox" id="disclosure" value="0"/>-->

                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="disclosurelbl1">
                    <input id="disclosureYes" class="sr-only"  name="disclosure" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="disclosurelbl2">
                    <input id="disclosureNo" class="sr-only "  name="disclosure" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">5. Has received demonstration of how to take/administer ART and other prescribed medication?</label>
           </div>
           <div class="col-md-2">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="administerART" value="0"/>-->

                    <label class="radio-custom radio-inline highlight" data-initialize="radio" id="administerARTlbl1">
                    <input id="administerARTYes" class="sr-only"  name="administerART" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="administerARTlbl2">
                    <input id="administerARTNo" class="sr-only "  name="administerART" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">6. Has received information on predictable side effects of ART and understands what steps to take in
case of these side effects?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="effectsART" value="0"/>-->

                   <label class="radio-custom radio-inline highlight" data-initialize="radio" id="effectsARTlbl1">
                    <input id="effectsARTYes" class="sr-only"  name="effectsART" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="effectsARTlbl2">
                    <input id="effectsARTNo" class="sr-only "  name="effectsART" type="radio" value="0" >No
                </label>
            </div>
       </div>


         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">7. For patients dependent on a caregiver: caregiver is committed to long-term support of the patient,
daily administration of ART, and meets the criteria above?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!--<input type="checkbox" class="onoffswitch-checkbox" id="dependents" value="0"/>-->

                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="dependentslbl1">
                    <input id="dependentsYes" class="sr-only"  name="dependents" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="dependentslbl2">
                    <input id="dependentsNo" class="sr-only "  name="dependents" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>


        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">8. Other likely barriers to adherence have been identified and there is a plan in place to address them
(e.g. frequent travel for work, plan to deal with unexpected travel, distance from clinic, etc)?</label>
           </div>
           <div class="col-md-2 pull-right">
               <!--<input type="checkbox" class="onoffswitch-checkbox" id="adherenceBarriers" value="0"/>-->

                 <label class="radio-custom radio-inline highlight" data-initialize="radio" id="adherenceBarrierslbl1">
                    <input id="adherenceBarriersYes" class="sr-only"  name="adherenceBarriers" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="adherenceBarrierslbl2">
                    <input id="adherenceBarriersNo" class="sr-only "  name="adherenceBarriers" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">9. Patient/caregiver has provided accurate locator information and contact details?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="locator" value="0"/>-->


               
                 <label class="radio-custom radio-inline highlight" data-initialize="radio" id="locatorlbl1">
                    <input id="locatorYes" class="sr-only"  name="locator" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="locatorlbl2">
                    <input id="locatorNo" class="sr-only "  name="locator" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>


        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">10. Patient/caregiver feels ready to start ART today?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!--<input type="checkbox" class="onoffswitch-checkbox" id="caregiver" value="0"/>-->

                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="caregiverlbl1">
                    <input id="caregiverYes" class="sr-only"  name="caregiver" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="caregiverlbl2">
                    <input id="caregiverNo" class="sr-only "  name="caregiver" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>
        <div class="col-md-12">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <button id="btnPsychosocialCriterial" class="btn btn-sm btn-primary">Save Pyschosocial Criteria</button>
            </div>
            <div class="col-md-4"></div>
        </div>

    </div>
</div>
</div>

<div class="col-md-12">
    <div class="panel panel-default" id="PsychosocialSupport" data-state="collapsed">
        <div class="panel-heading">
            <div class="panel-title">
                <h5>B. Support Systems Criteria (applies to patients and caregivers)</h5>
            </div>
        </div>
        <div class="panel-body">
             <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">1. Has identified convenient time/s of day for taking ART, and/or associated dose/s with daily
event/s?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="convinient" value="0"/>-->
               
                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="convinientlbl1">
                    <input id="convinientYes" class="sr-only"  name="convinient" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="convinientlbl2">
                    <input id="convinientNo" class="sr-only "  name="convinient" type="radio" value="0" >No
                </label>

            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">2. Treatment supporter has been identified and engaged in HIV education, or will attend next
counselling session?</label>
           </div>
           <div class="col-md-2">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="TSIdentified" value="0"/>-->

                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="TSIdentifiedlbl1">
                    <input id="TSIdentifiedYes" class="sr-only"  name="TSIdentified" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="TSIdentifiedlbl2">
                    <input id="TSIdentifiedNo" class="sr-only "  name="TSIdentified" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">3. Is aware of support group meeting time/s?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="supportGroup" value="0"/>-->

               
                <label class="radio-custom radio-inline highlight" data-initialize="radio" id="supportGrouplbl1">
                    <input id="supportGroupYes" class="sr-only"  name="supportGroup" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="supportGrouplbl2">
                    <input id="supportGroupNo" class="sr-only "  name="supportGroup" type="radio" value="0" >No
                </label>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">4. If facility has SMS reminder system: Has enrolled into SMS reminder system?</label>
           </div>
           <div class="col-md-2 pull-right">
               <!--<input type="checkbox" class="onoffswitch-checkbox" id="EnrollSMSReminder" value="0"/>-->
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="EnrollSMSReminderlbl1">
                    <input id="EnrollSMSReminderYes" class="sr-only"  name="EnrollSMSReminder" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="EnrollSMSReminderlbl2">
                    <input id="EnrollSMSReminderNo" class="sr-only "  name="EnrollSMSReminder" type="radio" value="0" >No
                </label>

            </div>
       </div>

         <div class="col-md-12"><hr /></div>
            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">5. Other support systems are in place or planned (e.g. setting phone alarm, pill box)?</label>
           </div>
           <div class="col-md-2 pull-right">
              <!-- <input type="checkbox" class="onoffswitch-checkbox" id="OtherSupportSystem" value="0"/>-->
               <label class="radio-custom radio-inline highlight" data-initialize="radio" id="OtherSupportSystemlbl1">
                    <input id="OtherSupportSystemYes" class="sr-only"  name="OtherSupportSystem" type="radio" value="1" >Yes
                </label>
                  <label class="radio-custom radio-inline highlight" data-initialize="radio" id="OtherSupportSystemlbl2">
                    <input id="OtherSupportSystemNo" class="sr-only "  name="OtherSupportSystem" type="radio" value="0" >No
                </label>

            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12 formgroup">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <button id="btnSupportSystemCriteria" class="btn btn-sm btn-primary">Save Support System Criteria</button>
            </div>
            <div class="col-md-4"></div>
        </div>

    </div>

        
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $("#divMessage").hide('fast', function () { $("#divsuccess").hide();});

        $('#psychoCriteria').lobiPanel({ options: {} });

        $('#PsychosocialSupport').lobiPanel({ options: {} });

        

       // $("#benefitsART").onoff();
      //  $("#screenAlcohol").onoff();
       // $("#depression").onoff();
       // $("#disclosure").onoff(); $("#administerART").onoff(); $("#effectsART").onoff();
		//$("#dependents").onoff();$("#adherenceBarriers").onoff();$("#locator").onoff();$("#caregiver").onoff();

		//$("#convinient").onoff();$("#TSIdentified").onoff();$("#supportGroup").onoff();$("#EnrollSMSReminder").onoff(); $("#OtherSupportSystem").onoff();

        var benefitsART = null; var screenAlcohol = null; var depression = null; var disclosure = null; var administerART = null; var effectsART = null; var dependents = null; var adherenceBarriers = null; var locator = null; var caregiver = null;
        var convinient = null; var TSIdentified = null; var supportGroup = null; var EnrollSMSReminder = null; var OtherSupportSystem = null;


        /* Support system criteria */
      //  $("#convinient").on('change', function () { convinient = this.checked ? true : false; }).change();
       // $("#TSIdentified").on('change', function () { TSIdentified = this.checked ? true : false; }).change();
       // $("#supportGroup").on('change', function () { supportGroup = this.checked ? true : false; }).change();
       // $("#EnrollSMSReminder").on('change', function () { EnrollSMSReminder = this.checked ? true : false; }).change();
        //$("#OtherSupportSystem").on('change', function () { OtherSupportSystem = this.checked ? true : false; }).change();

      



        $('input[name=benefitsART]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=benefitsART]:radio:checked').val().toString();
            if (value == "1") {
                benefitsART = true;
               
            }
            else if (value == "0") {
                benefitsART = false;
                
            }
        });

        
        $('input[name=screenAlcohol]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=screenAlcohol]:radio:checked').val().toString();
            if (value == "1") {
                screenAlcohol = true;
               
            }
            else if (value == "0") {
                screenAlcohol = false;
               
            }
        });

         $('input[name=depression]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=depression]:radio:checked').val().toString();
            if (value == "1") {
               depression = true;
                
            }
            else if (value == "0") {
                depression = false;
                
            }
        });

        
         $('input[name=disclosure]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=disclosure]:radio:checked').val().toString();
            if (value == "1") {
               disclosure = true;
               
            }
            else if (value == "0") {
               disclosure = false;
              
            }
        });

           
         $('input[name=administerART]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=administerART]:radio:checked').val().toString();
            if (value == "1") {
               administerART = true;
                
            }
            else if (value == "0") {
               administerART = false;
              
            }
        });

        $('input[name=effectsART]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=effectsART]:radio:checked').val().toString();
            if (value == "1") {
               effectsART = true;
               
            }
            else if (value == "0") {
               effectsART = false;
                
            }
        });

          $('input[name=dependents]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=dependents]:radio:checked').val().toString();
            if (value == "1") {
              dependents = true;
               
            }
            else if (value == "0") {
               dependents = false;
               
            }
        });

           $('input[name=adherenceBarriers]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=adherenceBarriers]:radio:checked').val().toString();
            if (value == "1") {
              adherenceBarriers = true;
               
            }
            else if (value == "0") {
               adherenceBarriers = false;
               
            }
          });

          $('input[name=locator]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=locator]:radio:checked').val().toString();
            if (value == "1") {
              locator = true;
              
            }
            else if (value == "0") {
              locator = false;
               
            }
        });

         $('input[name=caregiver]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=caregiver]:radio:checked').val().toString();
            if (value == "1") {
             caregiver= true;
              
            }
            else if (value == "0") {
              caregiver = false;
                
            }
          });

        
         $('input[name=convinient]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=convinient]:radio:checked').val().toString();
            if (value == "1") {
              convinient= true;
               
            }
            else if (value == "0") {
              convinient = false;
                
            }
          });


           
         $('input[name=TSIdentified]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=TSIdentified]:radio:checked').val().toString();
            if (value == "1") {
              TSIdentified= true;
               
            }
            else if (value == "0") {
              TSIdentified = false;
               
            }
        });



         $('input[name=supportGroup]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=supportGroup]:radio:checked').val().toString();
            if (value == "1") {
              supportGroup= true;
               
            }
            else if (value == "0") {
              supportGroup = false;
              
            }
        });


        
         $('input[name=EnrollSMSReminder]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=EnrollSMSReminder]:radio:checked').val().toString();
            if (value == "1") {
              EnrollSMSReminder= true;
                
            }
            else if (value == "0") {
              EnrollSMSReminder = false;
              
            }
        });



         $('input[name=OtherSupportSystem]:radio').click(function () {   //Hook the click event for selected elements
           
            var value = $('input[name=OtherSupportSystem]:radio:checked').val().toString();
            if (value == "1") {
             OtherSupportSystem= true;
                
            }
            else if (value == "0") {
              OtherSupportSystem = false;
               
            }
        });












      //  $("#benefitsART").on('change', function () { benefitsART = this.checked ? true : false; }).change();
       // $("#screenAlcohol").on('change', function () { screenAlcohol = this.checked ? true : false; }).change();
       // $("#depression").on('change', function () { depression = this.checked ? true : false; }).change();
      //  $("#disclosure").on('change', function () { disclosure = this.checked ? true : false; }).change();
       // $("#administerART").on('change', function () { administerART = this.checked ? true : false; }).change();
       // $("#effectsART").on('change', function () { effectsART = this.checked ? true : false; }).change();
       // $("#dependents").on('change', function () { dependents = this.checked ? true : false; }).change();
       // $("#adherenceBarriers").on('change', function () { adherenceBarriers = this.checked ? true : false; }).change();
        //$("#locator").on('change', function () { locator = this.checked ? true : false; }).change();
        //$("#caregiver").on('change', function () { caregiver = this.checked ? true : false; }).change();


        var patientId ="<%=PatientId%>";
        var patientMasterVisitId = "<%=PatientMasterVisitId%>";
        var isDonePyschosocialCriteria = 0;
        var isDoneSupportSystemCriteria = 0;

        $.ajax({
            type: "POST",
            url: "../WebService/PatientTreatmentpreparation.asmx/CheckIfPsychosocialCriteriaExists",
            data: "{'patientId':'" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                isDonePyschosocialCriteria = response.d;
                if (isDonePyschosocialCriteria < 1) {

                $("#divMessage").show('fast', function() {
                    $("#danger").append("ART Treatment Preparation Assessment is PENDING! --for this Client");
                    //enable buttons
                    $("#btnPsychosocialCriterial").prop('disabled', false);
                    $("#btnSupportSystemCriteria").prop('disabled', false);
                });                   
                } else {
                    $("#divsuccess").show('fast', function() {
                        $("#success").append("ART Treatment Preparation Completed! | View Answers for the ART Treatment preparation Questions");
                       // disbale buttons
                        $("#btnPsychosocialCriterial").prop('disabled', true);
                        $("#btnSupportSystemCriteria").prop('disabled', true);
                    });   
                }

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientTreatmentpreparation.asmx/GetPatientPsychosocialCriteria",
                    data: "{'patientId':'" + patientId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        
                        var itemList = response.d;
                        $.each(itemList, function (index, itemList) {

                            if (itemList.BenefitART != null && itemList.BenefitART != undefined) {
                                if (itemList.BenefitART == true) {


                                    $('#benefitARTlbl1').addClass("checked");

                                }
                                else if (itemList.BenefitART == false) {
                                    $('#benefitARTlbl2').addClass("checked");
                                }
                            }
                            if (itemList.Alcohol != null && itemList.Alcohol != undefined) {
                                if (itemList.Alcohol == true) {
                                    $('#screenAlcohollbl1').addClass("checked");

                                }
                                else if (itemList.Alcohol == false) {
                                    $('#screenAlcohollbl2').addClass("checked");
                                }
                            }
                            //{ $("#screenAlcohol").prop("checked", true); }
                            if (itemList.Depression != null && itemList.Depression != undefined) {
                                if (itemList.Depression == true) {
                                    $("#depressionlbl1").addClass("checked");
                                }
                                else if (itemList.Depression == false) {
                                    $("#depressionlbl2").addClass("checked");
                                }
                            }

                            if (itemList.Disclosure != null && itemList.Disclosure !=undefined)
                            {
                                if (itemList.Disclosure == true) {
                                    $("#disclosurelbl1").addClass("checked");
                                }
                                else if (itemList.Disclosure == false) {
                                     $("#disclosurelbl2").addClass("checked");
                                }
                               // $("#disclosure").prop("checked", true);


                            }
                            if (itemList.AdministerART!=null && itemList.AdministerART !=undefined)
                            {
                                if (itemList.AdministerART == true) {
                                    $("#administerARTlbl1").addClass("checked");
                                }
                                else if (itemList.AdministerART == false) {
                                    $("#administerARTlbl2").addClass("checked");
                                }
                            }
                            if (itemList.effectsART!=null && itemList.effectsART !=undefined) {
                                if (itemList.effectsART == true) {
                                    $("#effectsARTlbl1").addClass("checked");
                                }
                                else if (itemList.effectsART == false) {
                                  $("#effectsARTlbl2").addClass("checked");
                                }
                             
                            }
                            if (itemList.dependents != null && itemList.dependents != undefined) {
                                if (itemList.dependents == true) {
                                    $("#dependentslbl1").addClass("checked");
                                }
                                else if (itemList.dependents == false) {
                                    $("#dependentslbl2").addClass("checked");
                                }
                            }
                            //{ $("#dependents").prop("checked", true); }
                            if (itemList.AdherenceBarriers != null && itemList.AdherenceBarriers != undefined) {
                                if (itemList.AdherenceBarriers == true) {
                                    $("#adherenceBarrierslbl1").addClass("checked");
                                }
                                else if (itemList.AdherenceBarriers == false) {
                                    $("#adherenceBarrierslbl2").addClass("checked");
                                }
                            }
                           // { $("#adherenceBarriers").prop("checked", true); }
                            if (itemList.AccurateLocator != null && itemList.AccurateLocator != undefined) {
                                if (itemList.AccurateLocator == true) {
                                    $("#locatorlbl1").addClass("checked");
                                }
                                else if (itemList.AccurateLocator == false) {
                                    $("#locatorlbl2").addClass("checked");
                                }

                            }
                           // { $("#locator").prop("checked", true); }
                                if (itemList.startART!=null && itemList.startART!=undefined) {
                                    if (itemList.startART == true) {
                                        $("#caregiverlbl1").addClass("checked");
                                        //$("#caregiver").prop("checked", true);
                                    }
                                    else if (itemList.startART == false) {
                                        $("#caregiverlbl2").addClass("checked");
                                    }
                                }
                        });
                        //if (response.d !=='') { $("#btnPsychosocialCriterial").prop('disabled', true);}
                        
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message);
                    }
                });
               
            },
            error: function (xhr, errorType, exception) {
                var jsonError = jQuery.parseJSON(xhr.responseText);
                toastr.error("" + xhr.status + "" + jsonError.Message);
            }
        });

        /* insert is ART preparation is missing */
        
        $("#btnPsychosocialCriterial").click(function () {

            var error = 0;
            if (benefitsART == null) {
                error = 1;
                toastr.error("Kindly fill Question1 for psychosocial criteria");
                return false;
               
            }
          else if (screenAlcohol == null) {
                error = 1;
                toastr.error("kindly fill Question2 for psychosocialcriteria");
                return false;

            }
            else if (depression == null) {
                error = 1;
                toastr.error("kindly fill Question3 for psychosocialcriteria");
                return false;

            }
            else if (disclosure == null) {
                error = 1;
                toastr.error("kindly fill Question4 for psychosocial criteria");
                return false;

            }
            else if (administerART == null) {
                error = 1;
                toastr.error("kindly fill Question5 for psychosocial criteria");
                return false;

            }
            else if (effectsART == null) {
                error = 1;
                toastr.error("kindly fill Question6 for psychosocial criteria");
                return false;

            }
            else if (dependents == null) {
                toastr.error("kindly fill Question7 for psychosocial criteria");
                return false;
            }
            else if (adherenceBarriers == null) {
                error = 1;
                toastr.error("kindly fill Question8 for psychosocialcriteria");
                return false;
            }
            else if (locator == null) {
                error = 1;
                toastr.error("kindly fill Question9 for psychosocial criteria");
                return false;
            }
            else if (caregiver == null) {
                error = 1;
                toastr.error("kindly fill Question10 for psychosocial criteria");
                return false;
            }
          else {
              error = 0;
          }

            if (isDonePyschosocialCriteria === 0 && error == 0) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientTreatmentpreparation.asmx/AddPatientPsychosocialCriteria",
                    data: "{'patientId':'" + patientId + "','patientmastervisitId':'" + patientMasterVisitId + "','benefitART':'" + benefitsART + "','alcohol':'" + screenAlcohol + "','depression':'" + depression + "','disclosure':'" + disclosure + "','administerART':'" + administerART + "','effectsART':'" + effectsART + "','dependents':'" + dependents + "','adherence':'" + adherenceBarriers + "','locator':'" + locator + "','caregiver':'" + caregiver + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d);
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message);
                    }
                });
            }
            else {
                toastr.error("Psychosocial Criteria data not saved");
            }
            });
         
       $.ajax({
            type: "POST",
            url: "../WebService/PatientTreatmentpreparation.asmx/CheckIfSupportSystemCriteriaExists",
            data: "{'patientId':'" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                isDoneSupportSystemCriteria = response.d;

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientTreatmentpreparation.asmx/GetPatientSupportSystemCriteria",
                    data: "{'patientId':'" + patientId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var itemList = response.d;
                        //if (response.d !== '') {
                        //    $("#btnSupportSystemCriteria").prop('disabled', true);
                        //}
                        $.each(itemList, function (index, itemList) {

                            if (itemList.TakingART != null && itemList.TakingART != undefined) {
                                if (itemList.TakingART === true) {
                                    $("#convinientlbl1").addClass("checked");
                                   // $("#convinient").prop("checked", true);
                                }
                                else if (itemList.TakingART == false) {
                                    $("#convinientlbl2").addClass("checked");
                                }
                            }
                            if (itemList.TSIdentified != null && itemList.TSIdentified != undefined) {
                                if (itemList.TSIdentified === true) {
                                    $("#TSIdentifiedlbl1").addClass("checked");
                                   // $("#TSIdentified").prop("checked", true);
                                }
                                else if (itemList.TSIdentified == false) {
                                     $("#TSIdentifiedlbl2").addClass("checked");
                                }
                            }

                            if (itemList.supportGroup != null && itemList.supportGroup != undefined) {
                                if (itemList.supportGroup === false) {
                                    $("#supportGrouplbl2").addClass("checked");
                                }
                               else  if (itemList.supportGroup === true) {
                                     $("#supportGrouplbl1").addClass("checked");
                                    //$("#supportGroup").prop("checked", true);
                                }
                            }

                            if (itemList.EnrollSMSReminder != null && itemList.EnrollSMSReminder != undefined) {
                                if (itemList.EnrollSMSReminder === true) {
                                    $("#EnrollSMSReminderlbl1").addClass("checked");
                                    //$("#EnrollSMSReminder").prop("checked", true);
                                }
                                else if (itemList.EnrollSMSReminder == false) {
                                    $("#EnrollSMSReminderlbl2").addClass("checked");
                                }
                            }

                            if (itemList.OtherSupportSystems != null && itemList.OtherSupportSystems != undefined) {
                                if (itemList.OtherSupportSystems === true) {
                                    $("#OtherSupportSystemlbl1").addClass("checked");
                                    //$("#OtherSupportSystem").prop("checked", true);
                                }
                                else if (itemList.OtherSupportSystems == false) {
                                    $("#OtherSupportSystemlbl2").addClass("checked");
                                }
                            }
                        });
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message);
                    }
                });
                
            },
            error: function (xhr, errorType, exception) {
                var jsonError = jQuery.parseJSON(xhr.responseText);
                toastr.error("" + xhr.status + "" + jsonError.Message);
            }
            });


    $("#btnSupportSystemCriteria").click(function () {
        var error = 0;
        if (convinient == null) {
            error = 1;
            toastr.error("Kindly fill Question 1 for SupportSystem Criteria Section");
            return false;
        }
        else if (TSIdentified == null) {
            error = 1;
            toastr.error("Kindly fill Question 2 for SupportSystem Criteria Section");
            return false;

        }
        else if (supportGroup == null) {
            error = 1;
            toastr.error("Kindly fill Question 3 for SupportSystem Criteria Section");
            return false;
        }
        else if (EnrollSMSReminder == null) {
            error = 1;
            toastr.error("Kindly fill Question 4 for SupportSystem Criteria Section");
            return false;
        }
        else if (OtherSupportSystem == null) {
            error = 1;
            toastr.error("Kindly fill Question 5 for SupportSystem Criteria Section");
            return false;
        }
        else {
            error = 0;
        }
       
        if (isDoneSupportSystemCriteria === 0 && error == 0) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientTreatmentpreparation.asmx/AddPatientSupportSystemCriteria",
                data: "{'patientId':'" + patientId + "','patientmastervisitId':'" + patientMasterVisitId + "','takingART':'" + convinient + "','supportGroup':'" + supportGroup + "','TSIdentified':'" + TSIdentified + "','smsreminder':'" + EnrollSMSReminder + "','othersupport':'" + OtherSupportSystem + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d);
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message);
                }
            });
        }
        else {
            toastr.error("SupportSystem Criteria Data not Saved");
        }
       });

      
    });



 </script>