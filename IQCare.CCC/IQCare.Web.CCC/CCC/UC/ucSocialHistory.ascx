<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSocialHistory.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucSocialHistory" %>
<style>
    .rbList td { text-align: left;}
}
</style>
<div class="panel panel-info">
	<div class="panel-body">
        <div class="col-md-12 form-group">
		    <div>
			    <label class="control-label  pull-left text-primary">Record Social History</label>
		    </div>

		    <div>
			    <asp:RadioButtonList ClientIDMode="Static" ID="rbRecordSocialHistory" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                </asp:RadioButtonList>
		    </div>
	    </div>
        <div id="socialhistorycontentpanel">
            <div class="col-md-12 form-group">
			    <label class="control-label pull-left">Social History</label>
		    </div>

		    <div class="col-md-12 form-group">
			    <div class="col-md-4">
                    <label for="rbDrinkAlcohol" class="control-label pull-left"><span class="text-primary">How often do you have a drink containing alcohol?</span></label>
                    <div class="col-md-12">
                        <asp:RadioButtonList ClientIDMode="Static" ID="rbDrinkAlcohol" RepeatColumns="1" RepeatDirection="Vertical" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                        </asp:RadioButtonList>
                    </div>
			    </div>
                <div class="col-md-4">
                    <label for="rbSmoke" class="control-label pull-left"><span class="text-primary">How often do you smoke?</span></label>
                    <div class="col-md-12">
                        <asp:RadioButtonList ClientIDMode="Static" ID="rbSmoke" RepeatColumns="1" RepeatDirection="Vertical" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="rbUseDrugs" class="control-label pull-left"><span class="text-primary">How often do you use drugs?</span></label>
                    <div class="col-md-12">
                        <asp:RadioButtonList ClientIDMode="Static" ID="rbUseDrugs" RepeatColumns="1" RepeatDirection="Vertical" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2">
                        </asp:RadioButtonList>
                    </div>
                </div>
		    </div>
            <div class="col-md-12">
                <label for="taSocialHistoryNotes" class="control-label pull-left">Social History Notes</label>
                <asp:TextBox id="tbSocialHistoryNotes" ClientIDMode="Static" TextMode="multiline" Rows="3" runat="server" class="form-control input-sm" width="100%"/>
            </div>
        </div>
	</div>
</div>
<script type="text/javascript">
    $(document).on('click', '.btn-next', function () {
        if ($('#dsAdditionalHistory').hasClass('complete') && $('#dsPatientExamination').hasClass('active'))
        {
            var socialHistoryId = <%=SocialHistoryId%>;
            if (socialHistoryId > 0) {
                updateSocialHistory(socialHistoryId);
                alert("updating");
            }
            else {
                addSocialHistory();
            }
        }
    });

    var DrinkAlcohol = 0;
    var Smoke = 0;
    var UseDrugs = 0;

    //add social history
    function addSocialHistory() {
        DrinkAlcoholId = $("input[name='<%=rbDrinkAlcohol.UniqueID %>']:checked").val();
        SmokeId = $("input[name='<%=rbSmoke.UniqueID %>']:checked").val();
        UseDrugsId = $("input[name='<%=rbUseDrugs.UniqueID %>']:checked").val();
        var SocialHistoryNotes = $("#<%=tbSocialHistoryNotes.ClientID%>").val();
        var recordSocialHistory = $("input[name='<%=rbRecordSocialHistory.UniqueID %>']:checked").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        DrinkAlcohol = (DrinkAlcoholId > 0) ? DrinkAlcoholId : 0;
        Smoke = (SmokeId > 0) ? SmokeId : 0;
        UseDrugs = (UseDrugsId > 0) ? UseDrugsId : 0;
        alert(UseDrugs);
        $.ajax({
            type: "POST",
            url: "../WebService/SocialHistoryService.asmx/addSocialHistory",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','DrinkAlcohol': '" + DrinkAlcohol + "'," +
            "'Smoke': '" + Smoke + "', 'UseDrugs':'" + UseDrugs + "', 'SocialHistoryNotes':'" + SocialHistoryNotes + "','RecordSocialHistory':'" + recordSocialHistory +"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Social History Saved Successfully");
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
            }
        });
    }

    //update social history
    function updateSocialHistory(socialHistoryId) {
        DrinkAlcoholId = $("input[name='<%=rbDrinkAlcohol.UniqueID %>']:checked").val();
        SmokeId = $("input[name='<%=rbSmoke.UniqueID %>']:checked").val();
        UseDrugsId = $("input[name='<%=rbUseDrugs.UniqueID %>']:checked").val();
        var SocialHistoryNotes = $("#<%=tbSocialHistoryNotes.ClientID%>").val();
        var recordSocialHistory = $("input[name='<%=rbRecordSocialHistory.UniqueID %>']:checked").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        DrinkAlcohol = (DrinkAlcoholId > 0) ? DrinkAlcoholId : 0;
        Smoke = (SmokeId > 0) ? SmokeId : 0;
        UseDrugs = (UseDrugsId > 0) ? UseDrugsId : 0;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/SocialHistoryService.asmx/updateSocialHistory",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','DrinkAlcohol': '" + DrinkAlcohol + "'," +
            "'Smoke': '" + Smoke + "', 'UseDrugs':'" + UseDrugs + "', 'SocialHistoryNotes':'" + SocialHistoryNotes + "','SocialHistoryId':'" + socialHistoryId + "','RecordSocialHistory':'" + recordSocialHistory +"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Social History Updated Successfully");
            },
            error: function (response) {
                toastr.error(response.d, "Social History Not Updated");
                //toastr.error(JSON.stringify(response));
            }
        });
    }

    $(document).ready(function () {
        showHideSocialHistoryPanel();
    });

    $("input[name='<%=rbRecordSocialHistory.UniqueID%>']").change(function () {
        showHideSocialHistoryPanel();
    });

    function showHideSocialHistoryPanel() {
        var radioButtons = $("input[name='<%=rbRecordSocialHistory.UniqueID%>']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 1) {
            $("#socialhistorycontentpanel").hide();
        }
        else {
            $("#socialhistorycontentpanel").show();
        }
    }
</script>