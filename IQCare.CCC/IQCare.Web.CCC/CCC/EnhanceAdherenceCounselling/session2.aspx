<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="session2.aspx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.session2" %>

<style>
    .mmrbList{float: right;}
    .mmas4-results{margin-bottom: 30px;}
    .possiblebarrierstitle, .title-div{margin-top: 15px;}
    .session2loading{position: absolute;width: 100%;height: 100%;margin-left:-15px;z-index:999;background: rgba(204, 204, 204, 0.5);display: none;}
</style>
<form runat="server">
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<%--<div class="panel panel-info">--%>
			<div class="panel-body">
				<div class="col-md-12 form-group" id="pilladherencepane">
                    <div class="col-md-6">
                        <asp:PlaceHolder ID="PHPillCount" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="col-md-6">
                        <asp:PlaceHolder ID="PHDateFilled" runat="server"></asp:PlaceHolder>
                    </div>
				</div>
			</div>
		<%--</div>--%>
	</div>
</div>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Morisky Medication Adherence Scale (MMAS - 4)</span></label>
				</div>

				<div class="col-md-12 form-group session2mmascontainer">
                    <div class="mmas4container">
                        <asp:PlaceHolder ID="PHMMAS4" runat="server"></asp:PlaceHolder>
                        <div class="row mmas4-results">
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS4Scores" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS4Rating" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                    <div class="mmas8container" style="display: none">
                        <asp:PlaceHolder ID="PHMMAS8" runat="server"></asp:PlaceHolder>
                        <div class="row mmas8-results">
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS8Scores" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS8Rating" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 15px;">
                            <div class="col-md-12">
                                <asp:PlaceHolder ID="PHMMASRecommendation" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Review Adherence Plan from previous session</span></label>
				</div>

				<div class="col-md-12 form-group" id="adherencereview">
                    <asp:PlaceHolder ID="PHAdherenceReview" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Referrals and Networks</span></label>
				</div>

				<div class="col-md-12 form-group" id="refnnet">
                    <asp:PlaceHolder ID="PHReferralsandNetworks" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group" id="followupdatepane">
                    <div class="col-md-6">
					    <label class="control-label pull-left"><span class="text-primary">Follow up Date</span></label>
				    </div>
				    <div class="col-md-6">
					    <div class='input-group date s2followupdate'>
						    <span class="input-group-addon">
							    <span class="glyphicon glyphicon-calendar"></span>
						    </span>
                            <asp:PlaceHolder ID="PHFollowupDate" runat="server"></asp:PlaceHolder>
						    <%--<asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtFollowupDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>--%>
					    </div>
                        <asp:HiddenField ID="S2AppointmentId" runat="server" />
				    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="session2loading"><img src="../../Content/Img/PEPloading.gif" /></div>
</form>
<script type="text/javascript">
    var Answers = new Array;
    $(".filldate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });
    $(".s2followupdate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    }).on("dp.change", function (selectedDate) {
        var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
        var appDate = $("#<%=appointmentDateTb.ClientID%>").val();
        if (moment('' + appDate + '').isAfter(futureDate)) {
            toastr.error("Appointment date cannot be set to over 7 months");
            $("#<%=appointmentDateTb.ClientID%>").val("");
            return false;
        }
        AppointmentS2Count();
    });
    function AppointmentS2Count() {
        jQuery.support.cors = true;
        var date = $("#<%=appointmentDateTb.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/GetPatientAppointmentCount",
            data: "{'date':'" + date + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                var count = response.d;
                var message = count + " appointment(s) scheduled on " + date;
                document.getElementById("ModalMessage").innerHTML = message;
                $('#AlertModal').modal('show');
            },

            error: function (msg) {
                //alert(msg.responseText);
            }
        });
    }
    $(".session2mmascontainer input:radio").change(function (evt, data) {
        //MMMAS 4
        var selectionTotal = 0;
        parentdivid = $(this).closest('div.eahpanel').attr('id');
        $("#" + parentdivid + " .session2mmascontainer .mmas4container input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $(".session2mmascontainer input[name='" + rbName + "']");
            var rbInstanceCount = $(".session2mmascontainer input[name='" + rbName + "']").length;
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                selectionTotal = selectionTotal + 1;
            }
            else {
                selectionTotal = selectionTotal + 0;
            }
        });
        if (selectionTotal >= 1) {
            $("#" + parentdivid + " .mmas8container").show();
            getS2mmas8();
        }
        else {
            $("#" + parentdivid + " .session2mmascontainer .mmas8container input[type=radio]:checked").each(function () {
                $(this).prop('checked', false);
            });
            $("#<%=mmas8TbScore.ClientID%>").val("");
            $("#" + parentdivid + " .mmas8container").hide();
        }
        $("#<%=mmas4TbScore.ClientID%>").val(selectionTotal);
        var mmas4Total = selectionTotal.toFixed(2);
        getS2Mmas4Rating(mmas4Total);
    });
    function getS2mmas8() {
        //MMAS8
        var mmas8Total = 0;
        $("#" + parentdivid + " .session2mmascontainer input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $(".session2mmascontainer input[name='" + rbName + "']");
            var rbInstanceCount = $(".session2mmascontainer input[name='" + rbName + "']").length;
            var lastIndex = radioButtons.index(radioButtons.last());
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (lastIndex == 4) {
                mmas8Total = mmas8Total + selectedIndex * (1 / lastIndex);
            }
            else {
                if (selectedIndex == 0) {
                    mmas8Total = mmas8Total + 1;
                }
                else {
                    mmas8Total = mmas8Total + 0;
                }
            }
            $("#<%=mmas8TbScore.ClientID%>").val(mmas8Total);
            var mmas8TotalScore = mmas8Total.toFixed(2);
            getS2Mmas8Rating(mmas8TotalScore);
            getS2Mmas8Recommendation(mmas8TotalScore);
        });
    }
    function getS2Mmas4Rating(mmas4Total) {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getMmasRating",
            data: "{'MmasScore': '" + mmas4Total + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=mmas4TbRating.ClientID%>").val(response.d);
            },
            error: function (response) {
                toastr.error("Error Selecting MMAS4 Rating");
            }
        });
    }
    function getS2Mmas8Rating(mmas8TotalScore) {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getMmasRating",
            data: "{'MmasScore': '" + mmas8TotalScore + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=mmas8TbRating.ClientID%>").val(response.d);
            },
            error: function (response) {
                toastr.error("Error Selecting MMAS8 Rating");
            }
        });
    }
    function getS2Mmas8Recommendation(mmas8TotalScore) {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getMmasRecommendation",
            data: "{'MmasScore': '" + mmas8TotalScore + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#<%=mmas8TbRecommendation.ClientID%>").val(response.d);
            },
            error: function (response) {
                toastr.error("Error Selecting MMAS8 Recommendation");
            }
        });
    }
    //Save Data
    $("#eahmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 2) {
              CheckSession2HasValue();
          
            var values = Answers.filter((x) => { return x.value.length > 0 })
            if (values != null) {
                if (values.length > 0) {
                    addUpdateSession2Data();
                    addUpdateSession2Appointment();
                    $("#sessionthreedata .loading").show();
                    $("#sessionthreedata").load("../EnhanceAdherenceCounselling/session3.aspx");
                }
                else {
                    toastr.info("Kindly fill the  Session 2 section with required data");
                    evt.preventDefault();
                    return false;
                }
            }
        }
    });

    function CheckSession2HasValue() {

        $("#eahdatastep2 .mmrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('session2rb', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
            if (screeningValue > 0) {
                
                 Answers.push({ 'Id': screeningCategory, 'value': screeningValue });
            }
        });

        $("#eahdatastep2 input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('session2tb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
                   Answers.push({ 'Id': categoryId, 'value': clinicalNotes });
        });

        $("#eahdatastep2 textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('session2tb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            Answers.push({ 'Id': categoryId, 'value': clinicalNotes });


        });

    }
    function addUpdateSession2Appointment() {
        var appointmentid = $("#<%=S2AppointmentId.ClientID%>").val();
        var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
        var appDate = $("#<%=appointmentDateTb.ClientID%>").val();
        if (moment('' + appDate + '').isAfter(futureDate)) {
            toastr.error("Appointment date cannot be set to over 7 months");
            return false;
        }
        else {
            if (appointmentid > 0) {
                updateS2Appointment();
            }
            else {
                addPatientS2Appointment();
            }
        }
    }
    function checkExistingS2Appointment() {
        var patientId = "<%=PatientId%>";
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var serviceArea = 203;
        var reason = <%=reasonId%>;
        var differentiatedCare = <%=differentiatedCareId%>
            jQuery.support.cors = true;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/GetExistingPatientAppointment",
            data: "{'patientId':'" + patientId + "','appointmentDate': '" + appointmentDate + "','serviceAreaId': '" + serviceArea + "','reasonId': '" + reason + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            cache: false,
            success: function (response) {
                if (response.d != null) {
                    toastr.error("Appointment already exists on the selected day");
                    return false;
                }
                else {
                    addPatientS2Appointment();
                }
            },
            error: function (msg) {
                //alert(msg.responseText);
            }
        });
    }
    function updateS2Appointment() {
        var serviceArea = 203;
        var reason = <%=reasonId%>;
        var description = "Session 3 Adherence Counselling";
        var status = <%=followupStatusId%>;
        var differentiatedCareId = <%=differentiatedCareId%>;
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = GetURLParameter('visitId');
        var userId = <%=userId%>;
        var appointmentid = $("#<%=S2AppointmentId.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/UpdatePatientAppointment",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "','appointmentId':'" + appointmentid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Appointment saved successfully");
                //resetFields();
            },
            error: function (response) {
                toastr.error(response.d, "Appointment not saved");
            }
        });
    }
    function addPatientS2Appointment() {
        var serviceArea = 203;
        var reason = <%=reasonId%>;
        var description = "Session 3 Adherence Counselling";
        var status = <%=followupStatusId%>;
        var differentiatedCareId = <%=differentiatedCareId%>;
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = GetURLParameter('visitId');
        var userId = <%=userId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/AddPatientAppointment",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Appointment saved successfully");
            },
            error: function (response) {
                toastr.error(response.d, "Appointment not saved");
            }
        });
    }
    function addUpdateSession2Data() {
        var error = 0;
        $("#eahdatastep2 .mmrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('session2rb', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
            $.ajax({
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/AddUpdateScreeningData",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','screeningType':'" + screeningType + "','screeningCategory':'" + screeningCategory + "','screeningValue':'" + screeningValue + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    error = 0;
                },
                error: function (response) {
                    error = 1;
                }
            });
        });
        $("#eahdatastep2 input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('session2tb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            if (categoryId > 1) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientClinicalNotesService.asmx/addPatientClinicalNotesByVisitId",
                    data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','serviceAreaId':'" + serviceAreaId + "','notesCategoryId':'" + categoryId + "','clinicalNotes':'" + clinicalNotes + "','userId':'" + userId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        error = 0;
                    },
                    error: function (response) {
                        error = 1;
                    }
                });
            }
        });
        $("#eahdatastep2 textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('session2tb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            if (categoryId > 1) {
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
                        error = 1;
                    }
                });
            }
        });
        if (error == 0) {
            toastr.success("Session2 Saved");
        }
    }
    $(document).ready(function () {
        var PatientMasterVisitId = GetURLParameter('visitId');
        var patientId = '<%=PatientId%>';

  var SRNQuestion1='<%=Session2Refferal1ItemId%>';
        var SRNQuestion3 = '<%=Session2Refferal3ItemId%>';
        var SRNQuestion2 = '<%=Session2Refferal2ItemId%>';

        var ItemNo = '<%=ItemNo%>';
        var ItemYes = '<%=ItemYes%>';

         $("input:radio[name='session2rb"+SRNQuestion1+ "']").change(function (evt, data) {
        
             var selectedValue = $(this).val();
             if (selectedValue > 0) {
                 if (selectedValue == ItemYes) {


                     

                     $("#session2tb" + SRNQuestion3).prop('disabled', false);
                    // $("#session2tb" + SRNQuestion2).prop('disabled', false);
                      $("input:radio[name='session2rb" + SRNQuestion2 + "']").prop('disabled', false);
                 }
                 else {

                     $("#session2tb" + SRNQuestion3).prop('disabled', true);
                    // $("#session2tb" + SRNQuestion2).prop('disabled', true);
                     $("input:radio[name='session2rb" + SRNQuestion2 + "']").prop('disabled', true);
                      $("input:radio[name='session2rb" + SRNQuestion2 + "']").prop('checked', false);
                 }
             }
             else {
                 $("#session2tb" + SRNQuestion3).prop('disabled', false);
                 //$("#session2tb" + SRNQuestion2).prop('disabled', false);
                  $("input:radio[name='session2rb" + SRNQuestion2 + "']").prop('disabled', false);
             }
 
            });


        $('.session2loading').show();
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getPatientNotesByVisitId",
            data: "{'PatientId': '" + patientId + "','PatientMasterVisitId':'" + PatientMasterVisitId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                var result = JSON.parse(response.d);
                $.each(JSON.parse(response.d), function (index, value) {
                    inputnotes = this.ClinicalNotes;
                    if ($("#session2tb" + this.NotesCategoryId).length > 0) {
                        $("#session2tb" + this.NotesCategoryId).val(inputnotes);
                    }
                });
                var s2fd = $('input[type="text"].s2followupdateinput').val();
                gets2AppointmentId(s2fd);
            },
            error: function (response) {
                toastr.error("Notes could not be loaded");
            }
        });
        $.ajax({
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/getScreeningByIdandMasterVisit",
            data: "{'PatientId': '" + patientId + "','PatientMasterVisitId':'" + PatientMasterVisitId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
               
                var result = JSON.parse(response.d);
        
                $.each(JSON.parse(response.d), function (index, value) {
                  
                    if ($("#session2rb" + this.ScreeningCategoryId).length > 0) {
                        $("input:radio[name='session2rb" + this.ScreeningCategoryId + "'][value='" + this.ScreeningValueId + "']").attr("checked", true);
                    }
                });
                checkSession2ButtonsOnCtrls();
            },
            error: function (response) {
                toastr.error("Screening could not be loaded");
            }
        });
    });
    function gets2AppointmentId(s2fd) {
        var PatientMasterVisitId = GetURLParameter('visitId');
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/getAppointmentId",
            data: "{'PatientMasterVisitId':'" + PatientMasterVisitId + "','date':'" + s2fd + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.d != null) {
                    $.each(JSON.parse(response.d), function (index, value) {
                        $("#<%=S2AppointmentId.ClientID%>").val(this.Id);
                    });

                }
                else {
                    addUpdateSession2Appointment();
                }
            },
            error: function (response) {
                toastr.error("Screening could not be loaded");
            }
        });
    }
    function checkSession2ButtonsOnCtrls() {
        var mmas4Total = 0;
        $(".session2mmascontainer .mmas4container input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            mmas4Total = mmas4Total + selectedIndex;
        });

        if (mmas4Total >= 1) {
            $(".session2mmascontainer .mmas8container").show();
        }
        else {
            //hide craffft subsequent panel
            $(".session2mmascontainer .mmas8container").hide();
        }
        $('.session2loading').hide();
    }
</script>
