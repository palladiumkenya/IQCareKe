<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="session1.aspx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.session1" %>
<%@ OutputCache duration="86400" varybyparam="none" %>
<style>
    .mmrbList{float: right;}
    .mmas4-results{margin-bottom: 30px;}
    .possiblebarrierstitle, .title-div{margin-top: 15px;}
    .rbList{float: right;}
    .rbList input{margin-left: 5px;}
    .session1notessection{display: none;}
    .session1loading{position: absolute;width: 100%;height: 100%;margin-left:-15px;z-index:999;background: rgba(204, 204, 204, 0.5);display: none;}
</style>
<form runat="server">
<div id="session1container">
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

				    <div class="col-md-12 form-group session1mmascontainer">
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
					    <label class="control-label pull-left"><span>Viral Load</span></label>
				    </div>

				    <div class="col-md-12 form-group" id="sessionviralloadpane">
					    <div class=""><label class="control-label pull-left"><span class="text-primary">Understanding Viral Load (High/Suppressed):</span></label></div>
                        <div class="understandingviralload">
                            <asp:PlaceHolder ID="PHUnderstandingViralLoad" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="possiblebarrierstitle"><label class="control-label pull-left"><span class="text-primary">ACCESS FOR POSSIBLE BARRIERS TO ADHERENCE</span></label>
                            <div class="clearfix"></div>
                        </div>
                        <div class="possiblebarriers">
                            <div class=""><label class="control-label pull-left"><span class="text-primary">Cognitive Barriers</span></label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="">
                                <asp:PlaceHolder ID="PHCognitiveBarriers" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="title-div"><label class="control-label pull-left"><span class="text-primary">Behavioural Barriers</span></label></div>
                            <div class="">
                                <asp:PlaceHolder ID="PHBahaviouralBarriers" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="title-div"><label class="control-label pull-left"><span class="text-primary">Emotional Barriers</span></label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="">
                                <asp:PlaceHolder ID="PHEmotionalBarriers" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="title-div"><label class="control-label pull-left"><span class="text-primary">Socio-Economic Barriers</span></label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="" id="socioeconomicbarrierssection">
                                <asp:PlaceHolder ID="PHSocioEconomicBarriers" runat="server"></asp:PlaceHolder>
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
					        <div class='input-group date FollowupDate'>
						        <span class="input-group-addon">
							        <span class="glyphicon glyphicon-calendar"></span>
						        </span>
                                <asp:PlaceHolder ID="PHFollowupDate" runat="server"></asp:PlaceHolder>
						        <%--<asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtFollowupDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>--%>
					        </div>
				        </div>
                        <asp:HiddenField ID="AppointmentId" runat="server" />
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
</div>
<div class="session1loading"><img src="../../Content/Img/PEPloading.gif" /></div>
</form>
<script type="text/javascript">
    $(".filldate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });
    $(".FollowupDate").datetimepicker({
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
        AppointmentCount();
    });
    function AppointmentCount() {
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
    var parentdivid = "";

    $(".session1mmascontainer input:radio").change(function (evt, data) {
        //MMMAS 4
        var selectionTotal = 0;
        parentdivid = $(this).closest('div.eahpanel').attr('id');
        $("#" + parentdivid + " .session1mmascontainer .mmas4container input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $(".session1mmascontainer input[name='" + rbName + "']");
            var rbInstanceCount = $(".session1mmascontainer input[name='" + rbName + "']").length;
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
            getmmas8();
        }
        else {
            $("#" + parentdivid + " .session1mmascontainer .mmas8container input[type=radio]:checked").each(function () {
                $(this).prop('checked', false);
            });
            $("#<%=mmas8TbScore.ClientID%>").val("");
            $("#" + parentdivid + " .mmas8container").hide();
        }
        $("#<%=mmas4TbScore.ClientID%>").val(selectionTotal);
        var mmas4Total = selectionTotal.toFixed(2);
        getMmas4Rating(mmas4Total);
    });
    function getmmas8() {
        //MMAS8
        var mmas8Total = 0;
        $("#" + parentdivid + " .session1mmascontainer input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $(".session1mmascontainer input[name='" + rbName + "']");
            var rbInstanceCount = $(".session1mmascontainer input[name='" + rbName + "']").length;
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
            getMmas8Rating(mmas8TotalScore);
            getMmas8Recommendation(mmas8TotalScore);
        });
    }
    $("#sessionviralloadpane input:radio").change(function (evt, data) {
        var radioButtons = $("input[type='radio']");
        var selectedValue = $(this).val();
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        var parentPanel = $(this).parent().closest('.row').attr('id').split(' ');
        var rbName = $(this).attr('name');
        showhidenotes(parentPanel, selectedValue, rbName);
    });
    function showhidenotes(parentPanel, selectedValue, rbName) {
        var radioButtons = $("#sessionviralloadpane input[name='" + rbName + "']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 0) {
            $("#" + parentPanel + " > .session1notessection").show();
        }
        else {
            $("#" + parentPanel + " > .session1notessection").hide();
        }

    }
    function getMmas4Rating(mmas4Total) {
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
    function getMmas8Rating(mmas8TotalScore) {
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
    function getMmas8Recommendation(mmas8TotalScore) {
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
    $(document).ready(function () {
        var PatientMasterVisitId = GetURLParameter('visitId');
        var pagehash = window.location.hash;
        $('.session1loading').show();
        if (PatientMasterVisitId > 0 && pagehash != "") {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientClinicalNotesService.asmx/getPatientNotesByVisitId",
                data: "{'PatientId': '" + patientId + "','PatientMasterVisitId':'" + PatientMasterVisitId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    $.each(JSON.parse(response.d), function (index, value) {
                        inputnotes = this.ClinicalNotes;
                        if ($("#session1tb" + this.NotesCategoryId).length > 0) {
                            $("#session1tb" + this.NotesCategoryId).val(inputnotes);
                        }
                    });
                    var sfd = $('input[type="text"].sessiononefollowdate').val();
                    getAppointmentId(sfd);
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
                    $.each(JSON.parse(response.d), function (index, value) {
                        if ($("#session1rb" + this.ScreeningCategoryId).length > 0) {
                            $("input:radio[name='session1rb" + this.ScreeningCategoryId + "'][value='" + this.ScreeningValueId + "']").attr("checked", true);
                        }
                    });
                    checkButtonsOnCtrls();
                },
                error: function (response) {
                    toastr.error("Screening could not be loaded");
                }
            });
            //$("#sessiononedata .FollowupDate input[type='text']").on("change", function () {
            //    alert("cahneged");
            //});
        }
        else {
            //check if there is an ongoing Enhance Adherence Counselling. If there is an ongoing counselling, redirect to an ongoing adherence else return false
            checkScreeningStatus();
        }
       
        //get notes
        $("#btnReset").click(function () {
            //resetFields();
        });
        $("#btnCancel").click(function () {
            window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
        });

        $("#btnOk").click(function () {
            $('#AlertModal').modal('hide');
        });

        $("#btnDismiss").click(function () {
            //resetFields();
        });
    });

    function getAppointmentId(sfd) {
        var appdate = $("input:text").val();
        var PatientMasterVisitId = GetURLParameter('visitId');
        <%--var AppointmentDate = $("#<%=AppointmentId.ClientID%>").val();--%>
        //alert("Date" + AppointmentDate);
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/getAppointmentId",
            data: "{'PatientMasterVisitId':'" + PatientMasterVisitId + "','date':'" + sfd +"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                //store appointment id
                if (response.d != null) {
                    $.each(JSON.parse(response.d), function (index, value) {
                        $("#<%=AppointmentId.ClientID%>").val(this.Id);
                    });
                    
                }
                else {
                    addPatientAppointment();
                }
                //alert("sfd"+sfd);
                //alert(JSON.stringify(response.d));
            },
            error: function (response) {
                toastr.error("Screening could not be loaded");
            }
        });
    }
    function checkScreeningStatus() {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/getScreeningByStatus",
            data: "{'Status': 'Ongoing'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                if (JSON.parse(response.d) != "") {
                    $.each(JSON.parse(response.d), function (index, value) {
                        if (this.PatientMasterVisitId > 0) {
                            toastr.info("Please wait, redirecting to ongoing Enhance Adherence Councelling");
                            $("#sessiononedata .loading").show();
                            window.location.replace("../Encounter/PatientEncounter.aspx?visitId=" + this.PatientMasterVisitId + "#EnhanceAdherence");
                        }
                        else {
                            addEnhanceAdherenceEncounter();
                            addCancellingStatus();
                        }
                    });
                }
                else {
                    addEnhanceAdherenceEncounter();
                    addCancellingStatus();
                }
                checkButtonsOnCtrls();
            },
            error: function (response) {
                toastr.error("Screening could not be loaded");
            }
        });
    }
    function addCancellingStatus() {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientScreeningService.asmx/AddCancellingStatus",
            data: "{'status':'Ongoing'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.info("New Enhance Adherence Councelling Created");
                checkScreeningStatus();
            },
            error: function (response) {
                toastr.error(response.d, "Error creating new adherence councelling");
            }
        });
    }
    function checkButtonsOnCtrls() {
        var mmas4Total = 0;
        $(".session1mmascontainer .mmas4container input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var radioButtons = $("input[name='" + rbName + "']");
            var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
            if (selectedIndex == 0) {
                mmas4Total = mmas4Total + 1;
            }
            else {
                mmas4Total = mmas4Total + 0;
            }
        });
        if (mmas4Total >= 1) {
            $(".session1mmascontainer .mmas8container").show();
        }
        else {
            //hide craffft subsequent panel
            $(".session1mmascontainer .mmas8container").hide();
        }
        $("#socioeconomicbarrierssection input[type=radio]:checked").each(function () {
            var selectedValue = $(this).val();
            var rbName = $(this).attr('name');
            var parentPanel = $(this).parent().closest('.row').attr('id');
            showhidenotes(parentPanel, selectedValue, rbName);
        });
        $('.session1loading').hide();
        $('html, body').animate({
            scrollTop: $('body').offset().top - 20 //#DIV_ID is an example. Use the id of your destination on the page
        }, 'fast');
        //$('#session1statusmodal').modal('hide');
    }
    function showhidenotes(parentPanel, selectedValue, rbName) {
        var radioButtons = $("#socioeconomicbarrierssection input[name='" + rbName + "']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 0) {
            $("#" + parentPanel + " > .session1notessection").show();
        }
        else {
            $("#" + parentPanel + " > .session1notessection").hide();
        }

    }
    function GetURLParameter(sParam)
    {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++)
        {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam)
            {
                return sParameterName[1];
            }
        }
    }
    //Save Data
    $("#eahmyWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 1) {
            var patientMasterVisitId = GetURLParameter('visitId');
            addUpdateSession1Data();
            addUpdateSession1Appointment();
            $("#sessiontwodata .loading").show();
            $("#sessiontwodata").load("../UC/EnhanceAdherenceCounselling/session2.aspx");
        }
    });
    function addEnhanceAdherenceEncounter() {
        var serviceArea = <%=serviceAreaId%>;
        var EncounterType = "EnhanceAdherence";
        var userId = <%=userId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientEncounterService.asmx/savePatientEncounter",
            data: "{'EncounterType': '" + EncounterType + "','ServiceAreaId': '" + serviceArea + "','UserId': '" + userId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Encounter Saved");
            },
            error: function (response) {
                toastr.error(response.d, JSON.stringify(response));
            }
        });
    }
    function addUpdateSession1Appointment() {
        var appointmentid = $("#<%=AppointmentId.ClientID%>").val();
        var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
        var appDate = $("#<%=appointmentDateTb.ClientID%>").val();
        if (moment('' + appDate + '').isAfter(futureDate)) {
            toastr.error("Appointment date cannot be set to over 7 months");
            return false;
        }
        else {
            if (appointmentid > 0) {
                updateAppointment();
            }
            else {
                addPatientAppointment();
            }
        } 
    }
    function checkExistingAppointment() {
        var patientId = "<%=PatientId%>";
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var serviceArea = <%=serviceAreaId%>;
        var reason = <%=reasonId%>;
        var differentiatedCare = <%=differentiatedCareId%>;
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
                }
                else {
                    addPatientAppointment();
                }
            },
            error: function (msg) {
                //alert(msg.responseText);
            }
        });
    }
    function updateAppointment() {
        var serviceArea = <%=serviceAreaId%>;
        var reason = <%=reasonId%>;
        var description = "Session 2 Adherence Counselling";
        var status = <%=followupStatusId%>;
        var differentiatedCareId = <%=differentiatedCareId%>;
        var appointmentDate = $("#<%=appointmentDateTb.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = GetURLParameter('visitId');
        var userId = <%=userId%>;
        var appointmentid = $("#<%=AppointmentId.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/UpdatePatientAppointment",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "','appointmentId':'" + appointmentid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Appointment saved successfully");
               // resetFields();
            },
            error: function (response) {
                toastr.error(response.d, "Appointment not saved");
            }
        });
    }
    function addPatientAppointment() {
        var serviceArea = <%=serviceAreaId%>;
        var reason = <%=reasonId%>;
        var description = "Session 2 Adherence Counselling";
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
    function addUpdateSession1Data() {
        var error = 0;
        $("#eahdatastep1 .mmrbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = GetURLParameter('visitId');
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('session1rb', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
            $.ajax({
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/AddUpdateScreeningDataByVisitId",
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
        $("#eahdatastep1 input[type=text]").each(function () {
            var categoryId = ($(this).attr('id')).replace('session1tb', '');
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
        $("#eahdatastep1 textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('session1tb', '');
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
        if (error == 0) {
            toastr.success("Session 1 Saved");
        }
    }
    $(window).ready(function () {
        <%--var patientNotes = '<%=Session["patientNotesDataObject"]%>';
        $.each(JSON.parse(patientNotes), function (index, value) {
            inputnotes = this.ClinicalNotes;
            if ($("#session1tb" + this.NotesCategoryId).length > 0) {
                $("#session1tb" + this.NotesCategoryId).val(inputnotes);
            }
        });
        //get screening
        var patientScreening = '<%=Session["patientScreeningDataObject"]%>';
        $.each(JSON.parse(patientScreening), function (index, value) {
            if ($("#session1rb" + this.ScreeningCategoryId).length > 0) {
                $("input:radio[name='session1rb" + this.ScreeningCategoryId + "'][value='" + this.ScreeningValueId + "']").attr("checked", true);
            }
        });--%>
        
    });
</script>
