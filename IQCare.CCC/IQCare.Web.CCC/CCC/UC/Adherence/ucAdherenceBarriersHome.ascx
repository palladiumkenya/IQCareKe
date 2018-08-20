<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdherenceBarriersHome.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucAdherenceBarriersHome" %>
<%@ Register TagPrefix="uc" TagName="tnHIVAwareness" Src="~/CCC/UC/Adherence/ucHIVAwareness.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnUnderstanding" Src="~/CCC/UC/Adherence/ucUnderstanding.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnDailyRoutine" Src="~/CCC/UC/Adherence/ucDailyRoutine.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnPsychosocialCircumstances" Src="~/CCC/UC/Adherence/ucPsychosocialCircumstances.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnScreening" Src="~/CCC/UC/Adherence/ucScreening.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnReferralsandNetworks" Src="~/CCC/UC/Adherence/ucReferralsandNetworks.ascx" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<style>
        .control-label{text-align: left !important;}
        .adherencebarriersloading{position: absolute;width: 100%;height: 100%;z-index:999;background: rgba(204, 204, 204, 0.5);}
    </style>
    <div class="col-md-12" style="padding-top: 20px">
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="abmyWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="dsSectionOne" data-name="template" class="active complete">
						    <span class="badge">1</span>Section 1
							<span class="chevron"></span>
					    </li>
					    <li data-step="2" id="dsSectionTwo" class="complete">
						    <span class="badge">2</span>Section 2
							<span class="chevron"></span>
					    </li>
					    <li data-step="3" id="dsSectionThree" data-name="" class="complete">
						    <span class="badge">3</span>Section 3
							<span class="chevron"></span>
					    </li>

					    <li data-step="4" id="dsSectionFour" data-name="" class="complete">
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
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="abdatastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                            <uc:tnDailyRoutine ID="DailyRoutine" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="abdatastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                        <div class="col-md-12 form-group">
                            <uc:tnPsychosocialCircumstances ID="PsychosocialCircumstances" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="abdatastep4" data-parsley-validate="true" data-show-errors="true" data-step="4">
                        <div class="col-md-12 form-group">
                            <uc:tnScreening ID="Screening" runat="server" />
                            <uc:tnReferralsandNetworks ID="ReferralsandNetworks" runat="server" />
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
                <div class="adherencebarriersloading"><img src="../../Images/PEPloading.gif" /></div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    jQuery(function ($) {
        $('#abmyWizard').wizard();
        $('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
        $('#abmyWizard').find('#dsSectionTwo').toggleClass('complete', true);
        $('#abmyWizard').find('#dsSectionThree').toggleClass('complete', true);
        $('#abmyWizard').find('#dsSectionFour').toggleClass('complete', true);
        $('#abmyWizard').on('changed.fu.wizard', function (evt, data) {
            $('#abmyWizard').find('#dsSectionOne').toggleClass('complete', true);
            $('#abmyWizard').find('#dsSectionTwo').toggleClass('complete', true);
            $('#abmyWizard').find('#dsSectionThree').toggleClass('complete', true);
            $('#abmyWizard').find('#dsSectionFour').toggleClass('complete', true);
        });
    });
    //$("#loadAdherenceBarriers").click(function () {
    //    $(".adherencebarriersloading").show();
    //});
</script>