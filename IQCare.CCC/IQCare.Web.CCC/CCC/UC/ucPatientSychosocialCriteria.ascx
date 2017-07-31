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

        <div class="col-md-12">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <button id="btnSupportSystemCriteria" class="btn btn-sm btn-primary">Save Support System Criteria</button>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $("#divMessage").hide('fast', function () { $("#divsuccess").hide();});

        $('.panel').lobiPanel({ options: {} });

	    $("#benefitsART").onoff();$("#screenAlcohol").onoff();$("#depression").onoff();$("#disclosure").onoff();$("#administerART").onoff();$("#effectsART").onoff();
		$("#dependents").onoff();$("#adherenceBarriers").onoff();$("#locator").onoff();$("#caregiver").onoff();

		$("#convinient").onoff();$("#TSIdentified").onoff();$("#supportGroup").onoff();$("#EnrollSMSReminder").onoff(); $("#OtherSupportSystem").onoff();

        var benefitsART = false; var screenAlcohol = false; var depression = false; var disclosure = false; var administerART = false; var effectsART = false; var dependents = false; var adherenceBarriers = false; var locator = false; var caregiver = false;
        var convinient = false; var TSIdentified = false; var supportGroup = false; var EnrollSMSReminder = false; var OtherSupportSystem = false;


        /* Support system criteria */
        $("#convinient").on('change', function () { convinient = this.checked ? true : false; }).change();
        $("#TSIdentified").on('change', function () { TSIdentified = this.checked ? true : false; }).change();
        $("#supportGroup").on('change', function () { supportGroup = this.checked ? true : false; }).change();
        $("#EnrollSMSReminder").on('change', function () { EnrollSMSReminder = this.checked ? true : false; }).change();
        $("#OtherSupportSystem").on('change', function () { OtherSupportSystem = this.checked ? true : false; }).change();

        $("#benefitsART").on('change', function () { benefitsART = this.checked ? true : false; }).change();
        $("#screenAlcohol").on('change', function () { screenAlcohol = this.checked ? true : false; }).change();
        $("#depression").on('change', function () { depression = this.checked ? true : false; }).change();
        $("#disclosure").on('change', function () { disclosure = this.checked ? true : false; }).change();
        $("#administerART").on('change', function () { administerART = this.checked ? true : false; }).change();
        $("#effectsART").on('change', function () { effectsART = this.checked ? true : false; }).change();
        $("#dependents").on('change', function () { dependents = this.checked ? true : false; }).change();
        $("#adherenceBarriers").on('change', function () { adherenceBarriers = this.checked ? true : false; }).change();
        $("#locator").on('change', function () { locator = this.checked ? true : false; }).change();
        $("#caregiver").on('change', function () { caregiver = this.checked ? true : false; }).change();


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

                $("#divMessage").show('fast', function () { $("#danger").append("ART Treatment Preparation Assessment is PENDING! --for this Client"); });                   
                } else {
                    $("#divsuccess").show('fast', function () { $("#success").append("ART Treatment Preparation Completed! | View Answers for the ART prep Questions"); });   
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
                            alert(response.d);
                            if (itemList.BenefitART == true) { $("#benefitsART").prop("checked", true); }
                            if (itemList.Alcohol) { $("#screenAlcohol").prop("checked", true); }
                            if (itemList.Depression) { $("#depression").prop("checked", true); }
                            if (itemList.Disclosure) { $("#disclosure").prop("checked", true); }
                            if (itemList.AdministerART) { $("#administerART").prop("checked", true); }
                            if (itemList.effectsART) { $("#effectsART").prop("checked", true); }
                            if (itemList.dependents) { $("#dependents").prop("checked", true); }
                            if (itemList.AdherenceBarriers) { $("#adherenceBarriers").prop("checked", true); }
                            if (itemList.AccurateLocator) { $("#locator").prop("checked", true); }
                            if (itemList.startART) { $("#caregiver").prop("checked", true); }
                        });
                        $("#btnPsychosocialCriterial").prop('disabled', true);
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

                if (isDonePyschosocialCriteria == 0) {
                    $.ajax({
                        type: "POST",
                        url: "../WebService/PatientTreatmentpreparation.asmx/AddPatientPsychosocialCriteria",
                        data: "{'patientId':'" + patientId + "','patientmastervisitId':'" + patientMasterVisitId + "','benefitART':'" + benefitsART + "','Alcohol':'" + screenAlcohol + "','depression':'" + depression + "','disclosure':'" + disclosure + "','administerART':'" + administerART + "','adherence':'" + adherenceBarriers + "','locator':'" + locator + "','caregiver':'" + caregiver + "'}",
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