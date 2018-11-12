<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnhanceAdherenceHome.aspx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.EnhanceAdherenceHome" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<%@ Register TagPrefix="uc" TagName="tnSession1" Src="~/CCC/UC/EnhanceAdherenceCounselling/ucSession1.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnSession2" Src="~/CCC/UC/EnhanceAdherenceCounselling/ucSession2.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnSession3" Src="~/CCC/UC/EnhanceAdherenceCounselling/ucSession3.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnSession4" Src="~/CCC/UC/EnhanceAdherenceCounselling/ucSession4.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnEndSessionViralLoad" Src="~/CCC/UC/EnhanceAdherenceCounselling/ucEndSessionViralLoad.ascx" %>
<style>
    .control-label{text-align: left !important;}
</style>
<form runat="server">
    <div class="col-md-12" style="padding-top: 20px">
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="eahmyWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="eahSectionOne" data-name="template" class="active complete">
						    <span class="badge">1</span>Session 1
						    <span class="chevron"></span>
					    </li>
					    <li data-step="2" id="eahSectionTwo" class="complete">
						    <span class="badge">2</span>Session 2
						    <span class="chevron"></span>
					    </li>
					    <li data-step="3" id="eahSectionThree" data-name="" class="complete">
						    <span class="badge">3</span>Session 3
						    <span class="chevron"></span>
					    </li>

					    <li data-step="4" id="eahSectionFour" data-name="" class="complete">
						    <span class="badge">4</span>Session 4
						    <span class="chevron"></span>
					    </li>

                        <li data-step="5" id="eahSectionFive" data-name="" class="complete">
						    <span class="badge">5</span>Viral Load
						    <span class="chevron"></span>
					    </li>
				    </ul>
                </div>
                <div class="step-content">
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                            <uc:tnSession1 ID="tnSession1" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                            <uc:tnSession2 ID="tnSession2" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                        <div class="col-md-12 form-group">
                            <uc:tnSession3 ID="tnSession3" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep4" data-parsley-validate="true" data-show-errors="true" data-step="4">
                        <div class="col-md-12 form-group">
                            <uc:tnSession4 ID="tnSession4" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep5" data-parsley-validate="true" data-show-errors="true" data-step="5">
                        <div class="col-md-12 form-group">
                            <uc:tnEndSessionViralLoad ID="endSessionViralLoad" runat="server" />
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
    <div id="AlertModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Appointments</h4>

                </div>
                <div class="modal-body">
                    <div class="row" id="ModalMessage"></div>
                </div>

                <div class="modal-footer">
                    <button id="btnOk" type="button" class="btn btn-default" onclientclick="return false;">OK</button>
                    <button id="btnDismiss" type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="adherencebarriersloading"><img src="../../Content/Img/PEPloading.gif" /></div>
</form>

<script type="text/javascript">
    jQuery(function ($) {
        $('#eahmyWizard').wizard();
        $('#eahmyWizard').find('#eahSectionOne').toggleClass('complete', true);
        $('#eahmyWizard').find('#eahSectionTwo').toggleClass('complete', true);
        $('#eahmyWizard').find('#eahSectionThree').toggleClass('complete', true);
        $('#eahmyWizard').find('#eahSectionFour').toggleClass('complete', true);
        $('#eahmyWizard').find('#eahSectionFive').toggleClass('complete', true);
        $('#eahmyWizard').on('changed.fu.wizard', function (evt, data) {
            $('#eahmyWizard').find('#eahSectionOne').toggleClass('complete', true);
            $('#eahmyWizard').find('#eahSectionTwo').toggleClass('complete', true);
            $('#eahmyWizard').find('#eahSectionThree').toggleClass('complete', true);
            $('#eahmyWizard').find('#eahSectionFour').toggleClass('complete', true);
            $('#eahmyWizard').find('#eahSectionFive').toggleClass('complete', true);
        });
    });
</script>
