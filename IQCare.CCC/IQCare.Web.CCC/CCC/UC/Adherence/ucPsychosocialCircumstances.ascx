<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPsychosocialCircumstances.ascx.cs" Inherits="IQCare.Web.CCC.UC.Adherence.ucPsychosocialCircumstances" %>
<style>
    .notessection{display: none;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Psychosocial Circumstances</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
						<div class="col-md-12 text-left">
							<label>Who do you live with?</label>
						</div>
						<div class="col-md-12">
                            <asp:TextBox id="tbLivingWith" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
                        </div>
					</div>
                    <hr />
					<div class="row">
						<div class="col-md-12 text-left">
							<label >Who is aware of your HIV status? Are there people in your life with whom you've discussed your
                                HIV status and ART use?
							</label>
						</div>
                        <div class="col-md-12">
                            <asp:TextBox id="tbAware" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-8 text-left">
							<label>What is your support system?
							</label>
						</div>
						<div class="col-md-4">
                            <asp:RadioButtonList ClientIDMode="Static" ID="rbSupportSystem" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Treatment Buddy" Value="0"/>
                                <asp:ListItem Text="Support Group" Value="1"/>
                            </asp:RadioButtonList>
                        </div>
						<div class="col-md-12">
                            <asp:TextBox id="tbSupportSystemNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row" id="relationshippanel">
						<div class="col-md-10 text-left">
							<label>Are there changes in relationships with family members/friends</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbRelationshipChanges" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
                        <div class="col-md-12 notessection">
                            <asp:TextBox id="tbRelationshipChangesNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row" id="botheredPanel">
						<div class="col-md-10 text-left">
							<label>Does it bother you people might find out your HIV status?</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbBothered" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
                        <div class="col-md-12 notessection">
                            <asp:TextBox id="tbBotheredNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row" id="feeldifferentkly">
						<div class="col-md-10 text-left">
							<label>Do you feel that people treat you differently when they know your HIV status?</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbTreatedDifferently" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
                        <div class="col-md-12 notessection">
                            <asp:TextBox id="tbTreatedDifferentlyNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row" id="stigmapanel">
						<div class="col-md-10 text-left">
							<label>Is stigma intefering with taking medication on time or wih keeping clinical appointments?</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbInterferenceStigma" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
                        <div class="col-md-12 notessection">
                            <asp:TextBox id="tbInterferenceStigmaNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
                    <hr />
                    <div class="row" id="stoppedmedicationpanel">
						<div class="col-md-10 text-left">
							<label>Have you ever stopped using medication because of religious beliefs</label>
						</div>
						<div class="col-md-2">
							<asp:RadioButtonList ClientIDMode="Static" ID="rbStoppedMedication" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                                <asp:ListItem Text="Yes" Value="0"/>
                                <asp:ListItem Text="No" Value="1"/>
                            </asp:RadioButtonList>
						</div>
                        <div class="col-md-12 notessection">
                            <asp:TextBox id="tbStoppedMedicationNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    var currentStep;
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        currentStep = data.step;
        if (currentStep == 3) {
            var PCId = <%=PCId%>;
            if (PCId > 0) {
                updatePsychosocialCircumstances(PCId);
            }
            else {
                addPsychosocialCircumstances();
            }
        }
    });

    function updatePsychosocialCircumstances(PCId) {
        var PCId = PCId;
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var livingWith = $("#<%=tbLivingWith.ClientID%>").val();
        var aware = $("#<%=tbAware.ClientID%>").val();
        var supportSystem = $("input[name='<%=rbSupportSystem.UniqueID %>']:checked").val();
        var supportSystemNotes = $("#<%=tbSupportSystemNotes.ClientID%>").val();
        var relationshipChanges = $("input[name='<%=rbRelationshipChanges.UniqueID %>']:checked").val();
        var relationshipChangesNotes = $("#<%=tbRelationshipChangesNotes.ClientID%>").val();
        var bothered = $("input[name='<%=rbBothered.UniqueID %>']:checked").val();
        var botheredNotes = $("#<%=tbBotheredNotes.ClientID%>").val();
        var treatedDifferently = $("input[name='<%=rbTreatedDifferently.UniqueID %>']:checked").val();
        var treatedDifferentlyNotes = $("#<%=tbTreatedDifferentlyNotes.ClientID%>").val();
        var interferenceStigma = $("input[name='<%=rbInterferenceStigma.UniqueID %>']:checked").val();
        var interferenceStigmaNotes = $("#<%=tbInterferenceStigmaNotes.ClientID%>").val();
        var stoppedMedication = $("input[name='<%=rbStoppedMedication.UniqueID %>']:checked").val();
        var stoppedMedicationNotes = $("#<%=tbStoppedMedicationNotes.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/updatePsychosocialCircumstances",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','CreatedBy':'" + userId + "','livingWith': '" + livingWith + "'," +
            "'aware': '" + aware + "','supportSystem':'" + supportSystem + "','supportSystemNotes':'" + supportSystemNotes + "','relationshipChanges':'" + relationshipChanges + "'," +
            "'relationshipChangesNotes':'" + relationshipChangesNotes + "','bothered':'" + bothered + "','botheredNotes':'" + botheredNotes + "','treatedDifferently':'" + treatedDifferently + "'," +
            "'treatedDifferentlyNotes':'" + treatedDifferentlyNotes + "','interferenceStigma':'" + interferenceStigma + "','interferenceStigmaNotes':'" + interferenceStigmaNotes + "'," +
            "'stoppedMedication':'" + stoppedMedication + "','stoppedMedicationNotes':'" + stoppedMedicationNotes + "','PCId':'" + PCId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Psychosocial circumstances updated");
            },
            error: function (response) {
                toastr.error(response.d, "Psychosocial circumstances not updated");
            }
        });
    }

    function addPsychosocialCircumstances() {
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var livingWith = $("#<%=tbLivingWith.ClientID%>").val();
        var aware = $("#<%=tbAware.ClientID%>").val();
        var supportSystem = $("input[name='<%=rbSupportSystem.UniqueID %>']:checked").val();
        var supportSystemNotes = $("#<%=tbSupportSystemNotes.ClientID%>").val();
        var relationshipChanges = $("input[name='<%=rbRelationshipChanges.UniqueID %>']:checked").val();
        var relationshipChangesNotes = $("#<%=tbRelationshipChangesNotes.ClientID%>").val();
        var bothered = $("input[name='<%=rbBothered.UniqueID %>']:checked").val();
        var botheredNotes = $("#<%=tbBotheredNotes.ClientID%>").val();
        var treatedDifferently = $("input[name='<%=rbTreatedDifferently.UniqueID %>']:checked").val();
        var treatedDifferentlyNotes = $("#<%=tbTreatedDifferentlyNotes.ClientID%>").val();
        var interferenceStigma = $("input[name='<%=rbInterferenceStigma.UniqueID %>']:checked").val();
        var interferenceStigmaNotes = $("#<%=tbInterferenceStigmaNotes.ClientID%>").val();
        var stoppedMedication = $("input[name='<%=rbStoppedMedication.UniqueID %>']:checked").val();
        var stoppedMedicationNotes = $("#<%=tbStoppedMedicationNotes.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/AdherenceService.asmx/addPsychosocialCircumstances",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','CreatedBy':'" + userId + "','livingWith': '" + livingWith + "'," +
            "'aware': '" + aware + "','supportSystem':'" + supportSystem + "','supportSystemNotes':'" + supportSystemNotes + "','relationshipChanges':'" + relationshipChanges + "'," +
            "'relationshipChangesNotes':'" + relationshipChangesNotes + "','bothered':'" + bothered + "','botheredNotes':'" + botheredNotes + "','treatedDifferently':'" + treatedDifferently + "'," +
            "'treatedDifferentlyNotes':'" + treatedDifferentlyNotes + "','interferenceStigma':'" + interferenceStigma + "','interferenceStigmaNotes':'" + interferenceStigmaNotes + "'," +
            "'stoppedMedication':'" + stoppedMedication + "','stoppedMedicationNotes':'" + stoppedMedicationNotes + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Psychosocial circumstances saved");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });
    }

    $("#myWizard input:radio").change(function (evt,data) {
        var radioButtons = $("input[type='radio']");
        var selectedValue = $(this).val();
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        var parentPanel = $(this).parent().closest('.row').attr('id').split(' ');
        var rbName = $(this).attr('name');
        showhidenotes(parentPanel, selectedValue, rbName);
    });

    function showhidenotes(parentPanel, selectedValue, rbName)
    {
        var radioButtons = $("input[name='" + rbName + "']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 0)
        {
            $("#" + parentPanel + " > .notessection").show();
        }
        else {
            $("#" + parentPanel + " > .notessection").hide();
        }
        
    }

    $(document).ready(function () {
        $("#datastep3 input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var parentPanel = $(this).parent().closest('.row').attr('id');
            showhidenotes(parentPanel, selectedValue, rbName);
        });
    });

    jQuery(function ($) {
        var PCId = <%=PCId%>;        if (PCId > 0) {
            $('#myWizard').wizard();
            $('#myWizard').find('#dsSectionThree').toggleClass('complete', true);
            $('#myWizard').on('changed.fu.wizard', function (evt, data) {
                $('#myWizard').find('#dsSectionThree').toggleClass('complete', true);
            });
        }
    });
</script>