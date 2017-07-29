<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientSychosocialCriteria.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientSychosocialCriteria" %>
<div class="col-md-12" style="padding-top:4%">
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
                  <input type="checkbox" class="onoffswitch-checkbox" id="benefitsART" value="0" /> 
            </div>
       </div>
        <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">2. Has screened negative for alcohol or other drug use disorder, or is stable on treatment (see Section
4.6)</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="screenAlcohol" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">3. Has screened negative for depression or other psychiatric illness, or is stable on treatment (see
Section 4.6)</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="depression" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">4. Is willing to disclose/has disclosed HIV status, ideally to a family member or close friend?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="disclosure" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">5. Has received demonstration of how to take/administer ART and other prescribed medication?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="administerART" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">6. Has received information on predictable side effects of ART and understands what steps to take in
case of these side effects?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="effectsART" value="0"/>
            </div>
       </div>


         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">7. For patients dependent on a caregiver: caregiver is committed to long-term support of the patient,
daily administration of ART, and meets the criteria above?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="dependents" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>


        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">8. Other likely barriers to adherence have been identified and there is a plan in place to address them
(e.g. frequent travel for work, plan to deal with unexpected travel, distance from clinic, etc)?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="adherenceBarriers" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">9. Patient/caregiver has provided accurate locator information and contact details?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="locator" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>


        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">10. Patient/caregiver feels ready to start ART today?</label>
           </div>
           <div class="col-md-2">
              <input type="checkbox" class="onoffswitch-checkbox" id="caregiver" value="0"/>
            </div>
       </div>

    </div>
</div>
</div>

<div class="col-md-12">
    <div class="panel panel-default" data-state="collapsed">
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
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="convinient" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">2. Treatment supporter has been identified and engaged in HIV education, or will attend next
counselling session?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="TSIdentified" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">3. Is aware of support group meeting time/s?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="supportGroup" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">4. If facility has SMS reminder system: Has enrolled into SMS reminder system?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="EnrollSMSReminder" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>
            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">5. Other support systems are in place or planned (e.g. setting phone alarm, pill box)?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="OtherSupportSystem" value="0"/>
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $('.panel').lobiPanel({ options: { }  });

	    $("#benefitsART").onoff();$("#screenAlcohol").onoff();$("#depression").onoff();$("#disclosure").onoff();$("#administerART").onoff();$("#effectsART").onoff();
		$("#dependents").onoff();$("#adherenceBarriers").onoff();$("#locator").onoff();$("#caregiver").onoff();

		$("#convinient").onoff();$("#TSIdentified").onoff();$("#supportGroup").onoff();$("#EnrollSMSReminder").onoff(); $("#OtherSupportSystem").onoff();

        var benefitsART;var screenAlcohol;var depression;var isclosure;var administerART;var effectsART;var dependents; var adherenceBarriers; var locator;var caregiver;
        var convinient; var TSIdentified; var supportGroup; var EnrollSMSReminder; var OtherSupportSystem;

        /* Psychosocial ART assessment */

        $("#benefitsART").on('change', function () { benefitsART = this.checked ? 1 : 0; }).change();
        $("#screenAlcohol").on('change', function () { screenAlcohol = this.checked ? 1 : 0; }).change();
        $("#depression").on('change', function () { depression = this.checked ? 1 : 0; }).change();
        $("#disclosure").on('change', function () { disclosure = this.checked ? 1 : 0; }).change();
        $("#administerART").on('change', function () { administerART = this.checked ? 1 : 0; }).change();
        $("#effectsART").on('change', function () { effectsART = this.checked ? 1 : 0; }).change();
        $("#dependents").on('change', function () { dependents = this.checked ? 1 : 0; }).change();
        $("#adherenceBarriers").on('change', function () { adherenceBarriers = this.checked ? 1 : 0; }).change();
        $("#locator").on('change', function () { locator = this.checked ? 1 : 0; }).change();
        $("#caregiver").on('change', function () { caregiver = this.checked ? 1 : 0; }).change();

        /* Support system criteria */
        $("#convinient").on('change', function () { convinient = this.checked ? 1 : 0; }).change();
        $("#TSIdentified").on('change', function () { TSIdentified = this.checked ? 1 : 0; }).change();
        $("#supportGroup").on('change', function () { supportGroup = this.checked ? 1 : 0; }).change();
        $("#EnrollSMSReminder").on('change', function () { EnrollSMSReminder = this.checked ? 1 : 0; }).change();
        $("#OtherSupportSystem").on('change', function () { OtherSupportSystem = this.checked ? 1 : 0; }).change();


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
                alert(isDonePyschosocialCriteria);
            },
            error: function (xhr, errorType, exception) {
                var jsonError = jQuery.parseJSON(xhr.responseText);
                toastr.error("" + xhr.status + "" + jsonError.Message);
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
                alert(isDoneSupportSystemCriteria);
            },
            error: function (xhr, errorType, exception) {
                var jsonError = jQuery.parseJSON(xhr.responseText);
                toastr.error("" + xhr.status + "" + jsonError.Message);
            }
        });

    });



 </script>