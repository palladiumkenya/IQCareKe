<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReferralsandNetworks.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucReferralsandNetworks" %>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Referrals and Networks</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
						<div class="col-md-10 text-left">
							<label>Has the patient been referred to other other services? [Nutrition, psychosocial support services, other medical clinics, substance us treatment, etc]</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbPatientReferred" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
                    <hr />
					<div class="row">
						<div class="col-md-10 text-left">
							<label>Did he/she attend the appointments?</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbAppointmentsAttended" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
					</div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12 text-left">
							<label>What was the experience? Do the referrals need to be re-organized?</label>
						</div>
                        <div class="col-md-12">
                            <asp:TextBox id="tbExperience" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
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
        if (currentStep == 4) {
            var refId = <%=RefId%>;
            if (refId > 0) {
                updateAdherenceReferrals(refId);
            }
            else {
                addAdherenceReferrals();
            }
        }
    });

    function updateAdherenceReferrals(refId) {
        var refId = refId;
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var patientReferred = $("input[name='<%=rbPatientReferred.UniqueID %>']:checked").val();
        var appointmentsAttended = $("input[name='<%=rbAppointmentsAttended.UniqueID %>']:checked").val();
        var experience = $("#<%=tbExperience.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addAdherenceReferrals",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','patientReferred': '" + patientReferred + "'," +
            "'appointmentsAttended': '" + appointmentsAttended + "','experience':'" + experience + "','RefId':'" + refId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Referrals and Networks saved");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });
    }

    function addAdherenceReferrals() {
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var patientReferred = $("input[name='<%=rbPatientReferred.UniqueID %>']:checked").val();
        var appointmentsAttended = $("input[name='<%=rbAppointmentsAttended.UniqueID %>']:checked").val();
        var experience = $("#<%=tbExperience.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addAdherenceReferrals",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','patientReferred': '" + patientReferred + "'," +
            "'appointmentsAttended': '" + appointmentsAttended + "','experience':'" + experience + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Referrals and Networks saved");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });
    }

    jQuery(function ($) {
        var refId = <%=RefId%>;
        if (refId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionFour').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
                $('#myWizard').find('#dsSectionFour').toggleClass('complete', true);
            });
        }
    });
</script>