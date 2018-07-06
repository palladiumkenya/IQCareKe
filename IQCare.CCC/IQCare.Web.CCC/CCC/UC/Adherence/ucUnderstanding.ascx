<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUnderstanding.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucUnderstanding" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Understanding of HIV infection and ART</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
						<div class="col-md-10 text-left">
							<label>1. Understands how HIV affects the body and risk of transmission to sexual partners and 
                                children during pregnancy and breastfeeding</label>
						</div>
						<div class="col-md-2 text-right">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbUnderstandHIVEffects" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
                        <div class="clearfix"></div>
					</div>
                    <hr />
					<div class="row">
						<div class="col-md-10 text-left">
							<label>2. Understands ART and how it works</label>
						</div>
						<div class="col-md-2 text-right">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbUnderstandART" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
                    <hr />
                    <div class="row">
						<div class="col-md-10 text-left">
							<label>3. Understands side effects and what to do in case of side effects</label>
						</div>
						<div class="col-md-2 text-right">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbUnderstandSideEffects" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
                    <hr />
                    <div class="row">
						<div class="col-md-10 text-left">
							<label>4. Understands benefits of adherence</label>
						</div>
						<div class="col-md-2 text-right">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbUnderstandAdherenceBenefits" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
                    <hr />
                    <div class="row">
						<div class="col-md-10 text-left">
							<label>5. Understands consequences of non-adherence including drug resistance and treatment failure</label>
						</div>
						<div class="col-md-2 text-right">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbUnderstandConsequences" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 1) {
            var uId = <%=understandingId%>;
            if (uId > 0) {
                updateUnderstandHIV(uId);
            }
            else {
                addUnderstandHIV();
            }
        }
    });

    function addUnderstandHIV() {
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var understandHIVEffects = $("input[name='<%=rbUnderstandHIVEffects.UniqueID %>']:checked").val();
        var understandART = $("input[name='<%=rbUnderstandART.UniqueID %>']:checked").val();
        var understandSideEffects = $("input[name='<%=rbUnderstandSideEffects.UniqueID %>']:checked").val();
        var understandAdherenceBenefits = $("input[name='<%=rbUnderstandAdherenceBenefits.UniqueID %>']:checked").val();
        var understandConsequences = $("input[name='<%=rbUnderstandConsequences.UniqueID %>']:checked").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addUnderstandHIV",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','understandHIVEffects': '" + understandHIVEffects + "'," +
            "'understandART': '" + understandART + "','understandSideEffects':'" + understandSideEffects + "','understandAdherenceBenefits':'" + understandAdherenceBenefits + "','understandConsequences':'" + understandConsequences + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Understanding HIV infection and ART saved successfully");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });
    }

    function updateUnderstandHIV(uId) {
        var uId = uId;
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var understandHIVEffects = $("input[name='<%=rbUnderstandHIVEffects.UniqueID %>']:checked").val();
        var understandART = $("input[name='<%=rbUnderstandART.UniqueID %>']:checked").val();
        var understandSideEffects = $("input[name='<%=rbUnderstandSideEffects.UniqueID %>']:checked").val();
        var understandAdherenceBenefits = $("input[name='<%=rbUnderstandAdherenceBenefits.UniqueID %>']:checked").val();
        var understandConsequences = $("input[name='<%=rbUnderstandConsequences.UniqueID %>']:checked").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addUnderstandHIV",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','understandHIVEffects': '" + understandHIVEffects + "'," +
            "'understandART': '" + understandART + "','understandSideEffects':'" + understandSideEffects + "','understandAdherenceBenefits':'" + understandAdherenceBenefits + "','understandConsequences':'" + understandConsequences + "','uId':'" + uId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Understanding HIV infection and ART updated");
            },
            error: function (response) {
                toastr.error("Understanding HIV infection and ART not updated");
            }
        });
    }

    jQuery(function ($) {
        var uId = <%=understandingId%>;
        if (uId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionOne').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
                $('#myWizard').find('#dsSectionOne').toggleClass('complete', true);
            });
        }
    });
</script>