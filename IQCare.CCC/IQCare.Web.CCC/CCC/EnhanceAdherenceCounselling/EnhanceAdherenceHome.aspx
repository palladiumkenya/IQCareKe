<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CCC/Greencard.Master"  CodeBehind="EnhanceAdherenceHome.aspx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.EnhanceAdherenceHome" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<%@ Register TagPrefix="uc" TagName="tnSession1" Src="~/CCC/EnhanceAdherenceCounselling/ucSession1.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnSession2" Src="~/CCc/EnhanceAdherenceCounselling/ucSession2.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnSession3" Src="~/CCC/EnhanceAdherenceCounselling/ucSession3.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnSession4" Src="~/CCC/EnhanceAdherenceCounselling/ucSession4.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnEndSessionViralLoad" Src="~/CCC/EnhanceAdherenceCounselling/ucEndSessionViralLoad.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder"  runat="server">
<style>
    .control-label{text-align: left !important;}
</style>

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
                            <div id="sessiononedata">
                                <div class="loading" style="display: none;"><img src="../../Images/PEPloading.gif" /></div>
                                 
                            </div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                            <div id="sessiontwodata">
                                <div class="loading" style="display: none;"><img src="../../Images/PEPloading.gif" /></div>
                             
                            </div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                        <div class="col-md-12 form-group">
                            <div id="sessionthreedata">
                                <div class="loading" style="display: none;"><img src="../../Images/PEPloading.gif" /></div>
                               
                            </div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep4" data-parsley-validate="true" data-show-errors="true" data-step="4">
                        <div class="col-md-12 form-group">
                            <div id="sessionfourdata">
                                <div class="loading" style="display: none;"><img src="../../Images/PEPloading.gif" /></div>
                                
                            </div>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane eahpanel" id="eahdatastep5" data-parsley-validate="true" data-show-errors="true" data-step="5">
                        <div class="col-md-12 form-group">
                            <div id="sessionviralloaddata">
                                <div class="loading" style="display: none;"><img src="../../Images/PEPloading.gif" /></div>


                            </div>
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

      $(document).ready(function () {
     
            $(this).scrollTop(0);
            $("#sessiononedata .loading").show();
            $("#sessiononedata").load("../EnhanceAdherenceCounselling/session1.aspx");
      
    });
    $("#loadEnhanceAdherence").click(function () {
        $("#sessiononedata .loading").show();
        $("#sessiononedata").load("../EnhanceAdherenceCounselling/session1.aspx");
      
      //  $("#EnhanceAdherence .loading").hide();
    });
    $("#eahSectionTwo").click(function () {
        $("#sessiontwodata .loading").show();
        $("#sessiontwodata").load("../EnhanceAdherenceCounselling/session2.aspx");
        //$("#EnhanceAdherence .loading").hide();
    });
    $("#eahSectionThree").click(function () {
        $("#sessionthreedata .loading").show();
        $("#sessionthreedata").load("../EnhanceAdherenceCounselling/session3.aspx");
        //$("#EnhanceAdherence .loading").hide();
    });
    $("#eahSectionFour").click(function () {
        $("#sessionfourdata .loading").show();
        $("#sessionfourdata").load("../EnhanceAdherenceCounselling/session4.aspx");
        //$("#EnhanceAdherence .loading").hide();
    });
    $("#eahSectionFive").click(function () {
        $("#sessionviralloaddata .loading").show();
        $("#sessionviralloaddata").load("../EnhanceAdherenceCounselling/viralload.aspx");
        //$("#EnhanceAdherence .loading").hide();
    });
   
</script>
</asp:Content>