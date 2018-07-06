<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="AdherenceBarriers.aspx.cs" Inherits="IQCare.Web.CCC.Adherence.AdherenceBarriers" %>
<%@ Register TagPrefix="uc" TagName="tnHIVAwareness" Src="~/CCC/UC/Adherence/ucHIVAwareness.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnUnderstanding" Src="~/CCC/UC/Adherence/ucUnderstanding.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnDailyRoutine" Src="~/CCC/UC/Adherence/ucDailyRoutine.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnPsychosocialCircumstances" Src="~/CCC/UC/Adherence/ucPsychosocialCircumstances.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnScreening" Src="~/CCC/UC/Adherence/ucScreening.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnReferralsandNetworks" Src="~/CCC/UC/Adherence/ucReferralsandNetworks.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style>
        .control-label{text-align: left !important;}
    </style>
    <uc:PatientDetails runat="server" />
    <div class="col-md-12" style="padding-top: 20px">
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="myWizard">
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
                    <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                            <uc:tnHIVAwareness ID="HIVAwareness" runat="server" />
                            <uc:tnUnderstanding ID="Understanding" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="datastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                            <uc:tnDailyRoutine ID="DailyRoutine" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="datastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                        <div class="col-md-12 form-group">
                            <uc:tnPsychosocialCircumstances ID="PsychosocialCircumstances" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="datastep4" data-parsley-validate="true" data-show-errors="true" data-step="4">
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
                
            </div>
        </div>
    </div>
</asp:Content>
