<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSocialHistory.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucSocialHistory" %>
<style>
    .rbList td { text-align: left;}
    #socialhistoryquestions table{float: left !important;}
</style>
<div class="panel panel-info">
	<div class="panel-body">
        <div class="col-md-12 form-group">
            <div id="recordSocialHistory">
                <asp:PlaceHolder ID="PHSocialHistory" runat="server"></asp:PlaceHolder>
            </div>
	    </div>
        <div id="socialhistorycontentpanel">
            <div class="col-md-12 form-group">
			    <label class="control-label pull-left" id="lbl">Social History</label>
		    </div>

		    <div class="col-md-12 form-group">
                <div class="" id="socialhistoryquestions">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </div>
		    </div>
            <div class="col-md-12">
                <asp:PlaceHolder ID="PHSocialHistoryNotes" runat="server"></asp:PlaceHolder>
            </div>
        </div>
	</div>
</div>
<script type="text/javascript">
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 2) {
            var socialHistoryId = <%=SocialHistoryId%>;
            if (socialHistoryId > 0) {
                //updateDailyRoutine(dailyRoutineId);
            }
            else {
                addSocialHistory();
            }
            updateSocialHistoryData();
            updateSocialHistoryNotes();
        }
    });


    var DrinkAlcohol = 0;
    var Smoke = 0;
    var UseDrugs = 0;

    //add social history
    function addSocialHistory() {
        var error = 0;
        $("#socialhistoryquestions input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/SocialHistoryService.asmx/addSocialHistory",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','screeningType':'" + screeningType + "','screeningCategory':'" + screeningCategory + "','screeningValue':'" + screeningValue + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    error = 0;
                },
                error: function (response) {
                    toastr.error(JSON.stringify(response));
                    break;
                }
            });
        });
        if (error == 0) {
            toastr.success("Social History Saved");
        }
    }

    //update social history
    function updateSocialHistory(socialHistoryId) {
<%--        DrinkAlcoholId = "";
        SmokeId = "";
        UseDrugsId = "";
        var SocialHistoryNotes = $("#<%=tbSocialHistoryNotes.ClientID%>").val();
        var recordSocialHistory = $("input[name='<%=rbList.UniqueID %>']:checked").val();
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
        });--%>
    }

    function updateSocialHistoryData() {
        var error = 0;
        $("#recordSocialHistory input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=recordId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/updateScreeningYesNo",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','screeningType':'" + screeningType + "','screeningCategory':'" + screeningCategory + "','screeningValue':'" + screeningValue + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    error = 0;
                },
                error: function (response) {
                    toastr.error(JSON.stringify(response));
                    break;
                }
            });
        });
        if (error == 0) {
            toastr.success("Social History Saved");
        }
    }

    function updateSocialHistoryNotes() {
        var categoryId = <%=NotesId%>;
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var clinicalNotes = $("#<%=notesTb.ClientID%>").val();
        var serviceAreaId = 203;
        var userId = <%=userId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/addPatientClinicalNotes",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','serviceAreaId':'" + serviceAreaId + "','notesCategoryId':'" + categoryId + "','clinicalNotes':'" + clinicalNotes + "','userId':'" + userId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                error = 0;
            },
            error: function (response) {
                toastr.error(JSON.stringify(response));
                break;
            }
        });
        if (error == 0) {
            toastr.success("Notes Saved");
        }
    }

    $(document).ready(function () {
        showHideSocialHistoryPanel();
    });

    $("input[name='<%=rbList.UniqueID%>']").change(function () {
        showHideSocialHistoryPanel();
    });

    function showHideSocialHistoryPanel() {
        var radioButtons = $("input[name='<%=rbList.UniqueID%>']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 1) {
            $("#socialhistorycontentpanel").hide();
        }
        else {
            $("#socialhistorycontentpanel").show();
        }
    }
</script>