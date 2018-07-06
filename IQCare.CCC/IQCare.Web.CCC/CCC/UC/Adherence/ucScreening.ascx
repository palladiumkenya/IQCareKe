<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucScreening" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Depression Health Screening</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
						<div class="col-md-8 text-left">
							<label>Total</label>
						</div>
						<div class="col-md-4">
                            <div class="input-group">                     
                               <asp:TextBox runat="server" ID="tbTotal" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." data-parsley-trigger="keyup" data-parsley-type="number" Min="0" Max="200" data-parsley-range="[0, 200]" ></asp:TextBox>
                               <span class="input-group-addon">/30</span>
                            </div>
						</div>
					</div>
                    <hr />
					<div class="row">
						<div class="col-md-8 text-left">
							<label>Depression Severity</label>
						</div>
						<div class="col-md-4">
							<asp:TextBox id="tbDepressionSeverity" ClientIDMode="Static" runat="server" class="form-control input-sm"/>
						</div>
					</div>
                    <hr />
                    <div class="row">
                        <div class="col-md-8 text-left">
							<label>Recommended Management</label>
						</div>
						<div class="col-md-4">
							<asp:TextBox id="tbRecommendedManagement" ClientIDMode="Static" runat="server" class="form-control input-sm"/>
						</div>
                    </div>
                    <div class="col-md-12">
                        <a href="#" onClick="window.open('../Depression/DepressionScreening.aspx','pagename','resizable,height=260,width=370'); return false;">New Page</a><noscript>You need Javascript to use the previous link or use <a href="yourpage.htm" target="_blank">Click</a></noscript>
                        <a href='<%# ResolveUrl("~/CCC/Depression/DepressionScreening.aspx") %>' target="popup" class="btn btn-primary" onclick='window.open('<%# ResolveUrl("~/CCC/Depression/DepressionScreening.aspx") %>'),'name','width=600,height=400')'>Click To Open</a>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
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
        var ScreenId = ScreenId;
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
        });
    }

    function addAdherenceScreen() {
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
            "'depressionSeverity': '" + depressionSeverity + "','recommendedManagement':'" + recommendedManagement + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Depression screen saved");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });
    }

    jQuery(function ($) {
        var ScreenId = <%=ScreenId%>;
        if (ScreenId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionFour').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
                $('#myWizard').find('#dsSectionFour').toggleClass('complete', true);
            })
        }
    });
</script>