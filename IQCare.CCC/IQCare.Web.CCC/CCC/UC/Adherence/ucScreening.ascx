<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucScreening" %>
<%@ Register TagPrefix="uc" TagName="tnDepressionScreening" Src="~/CCC/UC/Depression/ucUpdateDepressionScreening.ascx" %>
<style>
    .modal-dialog {width: 80%;margin: 30px auto;}
    .modal-body{height: 500px;overflow-y: scroll;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Depression Health Screening</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
                        <div class="col-md-12">
                            <div class="depression-measurements">
                                Depression Severity: <span class="label label-success">0-4 None</span>
                                <span class="label label-primary">5-9 Mild</span> <span class="label label-info">10-14 Moderate</span> <span class="label label-warning">15-19 Moderately Severe</span> <span class="label label-danger">20 - 30 Severe</span>
                            </div>
                        </div>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHDepressionTotal" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHDepressionSeverity" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-12">
                            <asp:PlaceHolder ID="PHRecommendedManagement" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-md-12">
                            <a class="btn btn-primary" data-toggle="modal" data-target="#depressionHealthScreeningModal">Click to Update</a>
                        </div>
                    </div>              
				</div>
			</div>
		</div>
	</div>
</div>

<!-- deptression screening modal -->
<div class="modal fade" id="depressionHealthScreeningModal" tabindex="-1" role="dialog" aria-labelledby="depressionHealthScreeningModalTitle" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="depressionHealthScreeningModalTitle">Depression Health Screening</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="">
                    <uc:tnDepressionScreening ID="DepressionScreeningPanel" runat="server" />
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $("#scmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 4) {
            var ScreenId = <%=ScreenId%>;
            if (ScreenId > 0) {
                updateAdherenceScreen(ScreenId);
            }
            else {
                addAdherenceScreen();
            }
        }
    });

    function updateAdherenceScreen(ScreenId)
    {
        <%--var ScreenId = ScreenId;
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var total = $("#<%=tbTotal.ClientID%>").val();
        var depressionSeverity = $("#<%=tbDepressionSeverity.ClientID%>").val();
        var recommendedManagement = $("#<%=tbRecommendedManagement.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addAdherenceScreen",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','total': '" + total + "'," +
            "'depressionSeverity': '" + depressionSeverity + "','recommendedManagement':'" + recommendedManagement + "','ScreeningId':'" + ScreenId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Depression screen updated");
            },
            error: function (response) {
                toastr.error("Depression screen not updated");
            }
        });--%>
    }

    function addAdherenceScreen() {
        <%--var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var total = $("#<%=tbTotal.ClientID%>").val();
        var depressionSeverity = $("#<%=tbDepressionSeverity.ClientID%>").val();
        var recommendedManagement = $("#<%=tbRecommendedManagement.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addAdherenceScreen",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','total': '" + total + "'," +
            "'depressionSeverity': '" + depressionSeverity + "','recommendedManagement':'" + recommendedManagement + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Depression screen saved");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });--%>
    }

    jQuery(function ($) {
        var ScreenId = <%=ScreenId%>;
        if (ScreenId > 0) {
            $('#scmyWizard').wizard();
            $('#scmyWizard').find('#dsSectionFour').toggleClass('complete', true);
            $('#scmyWizard').on('changed.fu.wizard', function (evt, data) {
                $('#scmyWizard').find('#dsSectionFour').toggleClass('complete', true);
            })
        }
    });
</script>