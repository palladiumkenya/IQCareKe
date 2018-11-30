<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PatientDisclosure.aspx.cs" Inherits="IQCare.Web.CCC.Screening.PatientDisclosure" %>
<%@ Register TagPrefix="uc" TagName="tnDisclosure" Src="~/CCC/UC/Depression/ucDisclosure.ascx" %>
<form runat="server">
  <style>
        .control-label{text-align: left !important;}
    </style>
     <div class="col-md-12" style="padding-top: 20px">
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="myWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="dsSectionOne" data-name="template" class="active complete">
						    <span class="badge">1</span>Disclosure Screening
							<span class="chevron"></span>
					    </li>
					   
					   
				    </ul>
                </div>
                <div class="step-content">
                    <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                              <uc:tnDisclosure ID="DisclosurePanel" runat="server" />
                           
                        </div>
                    </div>
                    
                    
                   <%-- <div id="prevNextButton" class="actions">
					    <button type="button" class="btn btn-default btn-prev">
						    <span class="glyphicon glyphicon-arrow-left"></span>
                            Prev
					    </button>
					    <button type="button" class="btn btn-primary btn-next" data-last="Complete">
						    Next
						   <%-- <span class="glyphicon glyphicon-arrow-right"></span>
					    </button>
				    </div>--%>
                </div>
                
            </div>
        </div>
    </div>
</form>
