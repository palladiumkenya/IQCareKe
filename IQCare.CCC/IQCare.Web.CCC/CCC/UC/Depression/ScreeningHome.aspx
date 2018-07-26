<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScreeningHome.aspx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ScreeningHome" %>
<%@ Register TagPrefix="uc" TagName="tnDepressionScreening" Src="~/CCC/UC/Depression/ucDepressionScreening.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnCAGEAIDScreening" Src="~/CCC/UC/Depression/ucCAGEAID.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnCRAFFTScreening" Src="~/CCC/UC/Depression/ucCRAFFT.ascx" %>
<%@ Register TagPrefix="uc" TagName="tnGBVScreening" Src="~/CCC/UC/Depression/ucGBVScreening.ascx" %>
<form runat="server">
    <div class="col-md-12" style="padding-top: 20px">
        <div class="col-md-12">
            <div class="wizard" data-initialize="wizard" id="scmyWizard">
                <div class="steps-container">
                    <ul class="steps">
					    <li data-step="1" id="depressiondatastep" data-name="template" class="active complete">
						    <span class="badge"></span>Depression Screening
							<span class="chevron"></span>
					    </li>
                        <li data-step="2" id="alcoholdatastep" data-name="template" class="active complete">
						    <span class="badge"></span>Alcohol and Drug Use Screening
							<span class="chevron"></span>
					    </li>
                        <li data-step="3" runat="server" id="gbvdatastep" data-name="template" class="active complete">
						    <span class="badge"></span>Gender Based Violence Screening
							<span class="chevron"></span>
					    </li>
				    </ul>
                </div>
                <div class="step-content">
                    <div class="step-pane active sample-pane" id="scdatastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12 form-group">
                            <uc:tnDepressionScreening ID="DepressionScreeningPanel" runat="server" />
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" id="scdatastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
                        <div class="col-md-12 form-group">
                            <asp:PlaceHolder ID="PHAlcoholSection" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="step-pane active sample-pane" runat="server" id="scdatastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                        <div class="col-md-12 form-group">
                            <uc:tnGBVScreening ID="GBVScreeningPanel" runat="server" />
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
</form>
        <script type="text/javascript">
        jQuery(function ($) {
            $('#scmyWizard').wizard();
            $('#scmyWizard').find('#depressiondatastep').toggleClass('complete', true);
            $('#scmyWizard').find('#alcoholdatastep').toggleClass('complete', true);
            $('#scmyWizard').find('#<%=gbvdatastep.ClientID%>').toggleClass('complete', true);
            $('#scmyWizard').on('changed.fu.wizard', function (evt, data) {
                $('#scmyWizard').find('#depressiondatastep').toggleClass('complete', true);
                $('#scmyWizard').find('#alcoholdatastep').toggleClass('complete', true);
                $('#scmyWizard').find('#<%=gbvdatastep.ClientID%>').toggleClass('complete', true);
            });
        });
    </script>
