<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="frmFollowUpEducation.aspx.cs" Inherits="IQCare.Web.CCC.FollowupEducation.frmFollowUpEducation" %>
<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx"  TagName="ucPatientBrief" TagPrefix="uc2"%>
<%@ Register Src="~/CCC/UC/ucFollowupEducation.ascx"  TagName="ucFollowupEducation" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
   <uc2:ucPatientBrief ID="ucPatientBrief" runat="server" />
    <style>
        .control-label{text-align: left !important;}
    </style>
     <div class="col-md-12" style="padding-top: 20px">
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="myWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="dsSectionOne" data-name="template" class="active complete">
						    <span class="badge">1</span>Follow-Up Education
							<span class="chevron"></span>
					    </li>
					   
					   
				    </ul>
                </div>
                <div class="step-content">
                    <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                              <uc1:ucFollowupEducation ID="ucFollowupEducation" runat="server" />
                           
                        </div>
                    </div>
                    
                    
                    <div id="prevNextButton" class="actions">
					   <%-- <button type="button" class="btn btn-default btn-prev">
						    <span class="glyphicon glyphicon-arrow-left"></span>
                            Prev
					    </button>--%>
					    <button type="button" class="btn btn-primary btn-next" data-last="Complete">
						    Next
						   <%-- <span class="glyphicon glyphicon-arrow-right"></span>--%>
					    </button>
				    </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
