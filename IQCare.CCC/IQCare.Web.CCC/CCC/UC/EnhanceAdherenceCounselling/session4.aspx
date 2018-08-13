<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="session4.aspx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.session4" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<style>
    .mmrbList{float: right;}
    .mmas4-results{margin-bottom: 30px;}
    .possiblebarrierstitle, .title-div{margin-top: 15px;}
    .session4loading{position: absolute;width: 100%;height: 100%;margin-left:-15px;z-index:999;background: rgba(204, 204, 204, 0.5);display: none;}
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

				    <div class="col-md-12 form-group session4mmascontainer">
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
					        <div class='input-group date s4followupdate'>
						        <span class="input-group-addon">
							        <span class="glyphicon glyphicon-calendar"></span>
						        </span>
                                <asp:PlaceHolder ID="PHFollowupDate" runat="server"></asp:PlaceHolder>
						        <%--<asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtFollowupDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>--%>
					        </div>
				        </div>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <div class="session4loading"><img src="../../Images/PEPloading.gif" /></div>
</form>
<script type="text/javascript">
    $(".filldate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });
    $(".s4followupdate").datetimepicker({
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
        AppointmentS4Count();
        });
    function AppointmentS4Count() {
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
    $(".session4mmascontainer input:radio").change(function (evt, data) {
        //MMMAS 4
        var selectionTotal = 0;
        parentdivid = $(this).closest('div.eahpanel').attr('id');
        $("#" + parentdivid + " .session4mmascontainer .mmas4container input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $(".session4mmascontainer input[name='" + rbName + "']");
            var rbInstanceCount = $(".session4mmascontainer input[name='" + rbName + "']").length;
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
            getS4mmas8();
        }
        else {
            $("#" + parentdivid + " .session4mmascontainer .mmas8container input[type=radio]:checked").each(function () {
                $(this).prop('checked', false);
            });
            $("#<%=mmas8TbScore.ClientID%>").val("");
            $("#" + parentdivid + " .mmas8container").hide();
        }
        $("#<%=mmas4TbScore.ClientID%>").val(selectionTotal);
        var mmas4Total = selectionTotal.toFixed(2);
        getS4Mmas4Rating(mmas4Total);
    });
    function getS4mmas8() {
        //MMAS8
        var mmas8Total = 0;
        $("#" + parentdivid + " .session4mmascontainer input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $(".session4mmascontainer input[name='" + rbName + "']");
            var rbInstanceCount = $(".session4mmascontainer input[name='" + rbName + "']").length;
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
            getS4Mmas8Rating(mmas8TotalScore);
            getS4Mmas8Recommendation(mmas8TotalScore);
        });
        }
        function getS4Mmas4Rating(mmas4Total) {
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
    function getS4Mmas8Rating(mmas8TotalScore) {
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
    function getS4Mmas8Recommendation(mmas8TotalScore) {
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
        if (currentStep == 4) {
            addUpdateSession4Data();
            addUpdateSession4Appointment();
        }
    });
    function addUpdateSession4Appointment() {
        var appointmentid = <%=appointmentId%>;
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=appointmentDateTb.ClientID%>").val();
        if (moment('' + appDate + '').isAfter(futureDate)) {
            toastr.error("Appointment date cannot be set to over 7 months");
            return false;
        }
        else {
            if (appointmentid > 0) {
                updateS4Appointment();
            }
            else {
                checkExistingS4Appointment();
            }
        }
    }
    function checkExistingS4Appointment() {
        var patientId = "<%=PatientId%>";
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var serviceArea = <%=serviceAreaId%>;
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
                    addPatientS4Appointment();
                }
            },
            error: function (msg) {
                //alert(msg.responseText);
            }
        });
    }
    function updateS4Appointment() {
        var serviceArea = <%=serviceAreaId%>;
        var reason = <%=reasonId%>;
        var description = "Session 4 Adherence Counselling";
        var status = <%=followupStatusId%>;
        var differentiatedCareId = <%=differentiatedCareId%>;
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var userId = <%=userId%>;
        var appointmentid = <%=appointmentId%>;
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
    function addPatientS4Appointment() {
        var serviceArea = <%=serviceAreaId%>;
        var reason = <%=reasonId%>;
        var description = "Session 4 Adherence Counselling";
        var status = <%=followupStatusId%>;
        var differentiatedCareId = <%=differentiatedCareId%>;
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
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
    function addUpdateSession4Data() {
        var error = 0;
        $("#eahdatastep4 .mmrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('session4rb', '');
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
        $("#eahdatastep4 input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('session4tb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
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
        $("#eahdatastep4 textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('session4tb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
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
            toastr.success("Session 4 Saved");
        }
    }
    $(document).ready(function () {
        $('.session4loading').show();
        $.ajax({
            type: "POST",
            url: "../WebService/PatientClinicalNotesService.asmx/getPatientNotes",
            data: "{'PatientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                //alert(JSON.stringify(response));
                $.each(JSON.parse(response.d), function (index, value) {
                    inputnotes = this.ClinicalNotes;
                    if ($("#session4tb" + this.NotesCategoryId).length > 0) {
                        $("#session4tb" + this.NotesCategoryId).val(inputnotes);
                    }
                });
            },
            error: function (response) {
                toastr.error("Notes could not be loaded");
            }
        });
        $.ajax({
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/getPatientScreening",
            data: "{'PatientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                //alert(JSON.stringify(response));
                $.each(JSON.parse(response.d), function (index, value) {
                    if ($("#session4rb" + this.ScreeningCategoryId).length > 0) {
                        $("input:radio[name='session4rb" + this.ScreeningCategoryId + "'][value='" + this.ScreeningValueId + "']").attr("checked", true);
                    }
                });
                checkSession4ButtonsOnCtrls();
            },
            error: function (response) {
                toastr.error("Notes could not be loaded");
            }
        });
    });
    function checkSession4ButtonsOnCtrls() {
        var mmas4Total = 0;
        $(".session4mmascontainer .mmas4container input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            mmas4Total = mmas4Total + selectedIndex;
        });

        if (mmas4Total >= 1) {
            $(".session4mmascontainer .mmas8container").show();
        }
        else {
            //hide craffft subsequent panel
            $(".session4mmascontainer .mmas8container").hide();
        }
        $('.session4loading').hide();
    }
</script>