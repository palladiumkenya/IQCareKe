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
                  <input type="checkbox" class="onoffswitch-checkbox" id="benefitsART" /> 
            </div>
       </div>
        <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">2. Has screened negative for alcohol or other drug use disorder, or is stable on treatment (see Section
4.6)</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="screenAlcohol" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">3. Has screened negative for depression or other psychiatric illness, or is stable on treatment (see
Section 4.6)</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="depression" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">4. Is willing to disclose/has disclosed HIV status, ideally to a family member or close friend?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="disclosure" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">5. Has received demonstration of how to take/administer ART and other prescribed medication?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="administerART" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">6. Has received information on predictable side effects of ART and understands what steps to take in
case of these side effects?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="effectsART" />
            </div>
       </div>


         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">7. For patients dependent on a caregiver: caregiver is committed to long-term support of the patient,
daily administration of ART, and meets the criteria above?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="dependents" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>


        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">8. Other likely barriers to adherence have been identified and there is a plan in place to address them
(e.g. frequent travel for work, plan to deal with unexpected travel, distance from clinic, etc)?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="adherenceBarriers" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">9. Patient/caregiver has provided accurate locator information and contact details?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="locator" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>


        <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">10. Patient/caregiver feels ready to start ART today?</label>
           </div>
           <div class="col-md-2">
              <input type="checkbox" class="onoffswitch-checkbox" id="caregiver" />
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
               <input type="checkbox" class="onoffswitch-checkbox" id="convinient" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">2. Treatment supporter has been identified and engaged in HIV education, or will attend next
counselling session?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="TSIdentified" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">3. Is aware of support group meeting time/s?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="supportGroup" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">4. If facility has SMS reminder system: Has enrolled into SMS reminder system?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="EnrollSMSReminder" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>
            <div class="col-md-12">
           <div class="col-md-10">
               <label class="control-lable pull-left">5. Other support systems are in place or planned (e.g. setting phone alarm, pill box)?</label>
           </div>
           <div class="col-md-2">
               <input type="checkbox" class="onoffswitch-checkbox" id="OtherSupportSystem" />
            </div>
       </div>

         <div class="col-md-12"><hr /></div>

        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

        $('.panel').lobiPanel({
            options: {
               
            }   
        });
$('input[type=checkbox]').onoff();

    });

    $('selector').lobiPanel({
        reload: {
            icon: 'fa fa-refresh'
        },
        editTitle: {
            icon: 'fa fa-edit',
            icon2: 'fa fa-save'
        },
        unpin: {
            icon: 'fa fa-arrows'
        },
        minimize: {
            icon: 'fa fa-chevron-up',
            icon2: 'fa fa-chevron-down'
        },
        close: {
            icon: 'fa fa-times-circle'
        },
        expand: {
            icon: 'fa fa-expand',
            icon2: 'fa fa-compress'
        }
    });

 </script>