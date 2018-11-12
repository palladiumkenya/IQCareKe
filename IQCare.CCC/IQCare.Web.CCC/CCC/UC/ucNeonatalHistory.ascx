<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNeonatalHistory.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucNeonatalHistory" %>
<style>
    .milestone-link{float: right;right: 20px;}
    .col-centered{float: none;margin: 0 auto;}
    .text-center{text-align: center !important;}
</style>
<!--- Milestones Panel --->
<div class="panel panel-info" id="neonatalcontainer">
	<div class="panel-body">
        <div class="col-md-12 form-group" id="neonatalrecord">
            <div id="recordNeonatalHistory">
                <asp:PlaceHolder ID="PHNeonatalHistory" runat="server"></asp:PlaceHolder>
            </div>
	    </div>
        <div class="" id="neonatalcontentpanel">
            <div class="milestones-panel">
                <div class="col-md-12 form-group">
			        <label class="control-label pull-left">Milestones</label>
                    <div class="milestone-link pull-right"><a target="_blank" href="../../Content/resources/Milestones.pdf"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> Milestone Guides</a></div>
		        </div>
                <div id="neonatalform">
                    <div class="col-md-12 form-group">
			            <div class="col-md-3 form-group">
				            <div class="col-md-12">
                                <label for="txtMilestoneAssessed" class="control-label pull-left">Milestone Assessed</label>
				            </div>
				            <div class="col-md-12">
					            <asp:DropDownList runat="server" ID="ddlMilestoneAssessed" CssClass="form-control input-sm" ClientIDMode="Static" />
				            </div>
			            </div>
										
			            <div class="col-md-3 form-group">
				            <div class="col-md-12">
					            <label class="control-label pull-left" for="txtMilestoneOnsetDate">Onset Date</label>
				            </div>
				            <div class="col-md-12">
					            <div class='input-group date' id='MilestoneOnsetDate'>
						            <span class="input-group-addon">
							            <span class="glyphicon glyphicon-calendar"></span>
						            </span>
						            <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtMilestoneOnsetDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					            </div>
				            </div>
			            </div>

			            <div class="col-md-1 form-group">
				            <div class="col-md-12">
					            <label class="control-label pull-left" for="cbMilestoneAchieved">Achieved</label>
				            </div>
				            <div class="col-md-12">
					            <asp:CheckBox CssClass="pull-left" ClientIDMode="Static" ID="cbMilestoneAchieved" runat="server" value="1"/>
				            </div>
			            </div>

			            <div class="col-md-2 form-group">
				            <div class="col-md-12">
					            <label class="control-label pull-left">Status</label>
				            </div>
				            <div class="col-md-12">
					            <asp:DropDownList runat="server" ID="ddlMilestoneStatus" CssClass="form-control input-sm" ClientIDMode="Static" />
				            </div>
			            </div>
										

			            <div class="col-md-3">
				            <div class="col-md-12">
					            <label class="control-label pull-left">Comment</label>
				            </div>
				            <div class="col-md-12">
					            <asp:TextBox runat="server" ID="txtMilestoneComment" CssClass="form-control input-sm" ClientIDMode="Static" />
				            </div>
			            </div>
		            </div>

                    <div class="col-md-12 form-group">
			            <div class="col-md-12 text-center">
				            <label class="control-label"><span class="fa fa-cog">Action</span></label>
			            </div>
			            <div class="col-md-4 col-centered">
				            <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddMilestones" onclick="AddNeonatalMilestones();">Add</button>
			            </div>
                    </div>
                </div>
		        <div class="col-md-12 form-group" id="neonataltable">
			        <div class="panel panel-primary">
				        <div class="panel-heading">Milestones Assessed</div>
				        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
					        <table id="dtlMilestones" class="table table-bordered table-striped" width="100%">
						        <thead>
							        <tr>
                                        <th><span class="text-primary">Id</span></th>
								        <th><span class="text-primary">Milestone Assessed</span></th>
								        <th><span class="text-primary">Date Assessed</span></th>
								        <th><span class="text-primary">Achieved</span></th>
								        <th><span class="text-primary">Status</span></th>
								        <th><span class="text-primary">Comment</span></th>
                                        <th><span class="text-primary">Delete</span></th>
							        </tr>
						        </thead>
					        </table>

				        </div>
			        </div>
		        </div>
            </div>

            <div class="immunization-panel">
		        <div class="col-md-12 form-group">
			        <label class="control-label pull-left">Immunization History</label>
		        </div>

		        <div class="col-md-12 form-group" id="immunizationform">
			        <div class="col-md-3 form-group">
				        <div class="col-md-12">
                            <label for="txtMilestoneAssessed" class="control-label pull-left">Period</label>
					        <%--<label for="ChronicIllnessName" class="control-label pull-left">Illness</label>--%>
				        </div>
				        <div class="col-md-12">
					        <asp:DropDownList runat="server" ID="ddlImmunizationPeriod" CssClass="form-control input-sm" ClientIDMode="Static" />
				        </div>
			        </div>

			        <div class="col-md-3 form-group">
				        <div class="col-md-12">
					        <label class="control-label pull-left" for="ddlImmunizationGiven">Immunization Given</label>
				        </div>
				        <div class="col-md-12">
					        <asp:DropDownList runat="server" ID="ddlImmunizationGiven" CssClass="form-control input-sm" ClientIDMode="Static" />
				        </div>
			        </div>

                    <div class="col-md-3 form-group">
				        <div class="col-md-12">
					        <label class="control-label pull-left" for="txtImmunizationDate">Date Immunized</label>
				        </div>
				        <div class="col-md-12">
					        <div class='input-group date' id='ImmunizationDate'>
						        <span class="input-group-addon">
							        <span class="glyphicon glyphicon-calendar"></span>
						        </span>
						        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtImmunizationDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					        </div>
				        </div>
			        </div>
										

			        <div class="col-md-3">
				        <div class="col-md-12">
					        <label class="control-label pull-left"><span class="fa fa-cog">Action</span></label>
				        </div>
				        <div class="col-md-4">
					        <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddImmunizationHistory" onclick="AddImmunizationHistory();">Add</button>
				        </div>
			        </div>
		        </div>

		        <div class="col-md-12 form-group">
			        <div class="panel panel-primary">
				        <div class="panel-heading">Immunization History</div>
				        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
					        <table id="dtlImmunizationHistory" class="table table-bordered table-striped" width="100%">
						        <thead>
							        <tr>
                                        <th><span class="text-primary">Id</span></th>
								        <th><span class="text-primary">Period</span></th>
								        <th><span class="text-primary">Immunization Given</span></th>
								        <th><span class="text-primary">Date Immunized</span></th>
                                        <th><span class="text-primary">Delete</span></th>
							        </tr>
						        </thead>
					        </table>

				        </div>
			        </div>
		        </div>

	        </div>
            <div class="panel-body">
                <div class="col-md-12 form-group">
                    <asp:PlaceHolder ID="PHNeonatalHistoryNotes" runat="server"></asp:PlaceHolder>
			        <%--<label class="control-label pull-left">Neonatal History Notes</label>
                    <textarea runat="server" clientidmode="Static" id="neonatalhistorynotes" class="form-control input-sm" placeholder="" rows="3"></textarea>--%>
		        </div>
            </div>
	    </div>
    </div>
</div>


<script type="text/javascript">
    $("#MilestoneOnsetDate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });

    $("#ImmunizationDate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });


    //MILESTONES
    var getMilestonesTbl;
    var previousMilestones = getMilestones();
    function getMilestones() {
        getMilestonesTbl = $('#dtlMilestones').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/NeonatalEncounterService.asmx/LoadMilestones",
                dataSrc: 'd',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            },
            destroy: true,
            paging: false,
            searching: false,
            info: false,
            ordering: false,
            columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }

    function AddNeonatalMilestones() {
        var milestoneAchieved = 0;
        var milestoneAssessed = $("#<%=ddlMilestoneAssessed.ClientID%>").val();
        var milestoneOnsetDate = $("#<%=txtMilestoneOnsetDate.ClientID%>").val();
        var milestoneStatus = $("#<%=ddlMilestoneStatus.ClientID%>").val();
        var milestoneComment = $("#<%=txtMilestoneComment.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        if ($("#<%=cbMilestoneAchieved.ClientID%>").prop("checked") == true) {
            milestoneAchieved = 1;
        }
        else {
            milestoneAchieved = 0;
        }
        $.ajax({
            type: "POST",
            url: "../WebService/NeonatalEncounterService.asmx/addNeonatalMilestones",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','milestoneAssessed': '" + milestoneAssessed + "'," +
            "'milestoneOnsetDate': '" + milestoneOnsetDate + "', 'milestoneAchieved':'" + milestoneAchieved + "','milestoneStatus':'" + milestoneStatus + "'," +
            "'milestoneComment':'" + milestoneComment + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                getMilestones();
                if (response.d == "Existing")
                {
                    toastr.error("Neonatal Milestone already exisiting");
                }
                else
                {
                    toastr.success("Neonatal Milestone saved successfully");
                }
            },
            error: function (response) {
                toastr.error("Neonatal Milestone not saved");
            }
        });
    }



    //IMMUNIZATION
    //get immunization histotry
    var getImmunizationTbl;
    var previousImmunization = getImmunization();
    function getImmunization() {
        getImmunizationTbl = $('#dtlImmunizationHistory').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/NeonatalEncounterService.asmx/loadImmunization",
                dataSrc: 'd',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            },
            destroy: true,
            paging: false,
            searching: false,
            info: false,
            ordering: false,
            columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }
    //add immunization history
    function AddImmunizationHistory() {
        var ImmunizationPeriod = $("#<%=ddlImmunizationPeriod.ClientID%>").val();
        var ImmunizationGiven = $("#<%=ddlImmunizationGiven.ClientID%>").val();
        var ImmunizationDate = $("#<%=txtImmunizationDate.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/NeonatalEncounterService.asmx/addImmunizationHistory",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','immunizationPeriod':'" + ImmunizationPeriod + "'," +
            "'immunizationGiven':'" + ImmunizationGiven + "','immunizationDate':'" + ImmunizationDate + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                getImmunization();
                if (response.d == "success")
                {
                    toastr.success(response.d, "Neonatal Milestone saved successfully");
                }
                else {
                    toastr.error(response.d, "Immunization stage already existing");
                }
                
            },
            error: function (response) {
                toastr.error(response.d, "Immunization History Not Saved");
            }
        });
    }

    //Neonatal history notes
    $(document).on('click', '.btn-next', function () {
        if ($('#dsAdditionalHistory').hasClass('complete') && $('#dsPatientExamination').hasClass('active')) {
            var notesId = <%=NotesId%>;
            updateNeonatalScreeningData();
            updateNeonatalNotes();
        }
    });

    function updateNeonatalScreeningData()
    {
        var error = 0;
        $("#recordNeonatalHistory input[type=radio]:checked").each(function () {
            var screeningValue = $(this).val();
            var screeningCategory = $(this).closest("table").attr('id');
            var screeningType = <%=screenTypeId%>;
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
            toastr.success("Neonatal History Saved");
        }
    }

    function updateNeonatalNotes()
    {
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

    $("#dtlMilestones").on('click', '.btnDelete', function () {
        var milestoneId = getMilestonesTbl.row($(this).parents('tr')).data()["0"];
        var milestoneAssessed = getMilestonesTbl.row($(this).parents('tr')).data()["1"];
        var clickSection = this;
        var delStatus = confirmDeleteMilestone(milestoneId, milestoneAssessed, clickSection);
        if (delStatus == "Milestone not deleted") {
            toastr.error("Milestone not deleted");
        }
    });

    function confirmDeleteMilestone(milestoneId, milestoneAssessed, clickSection) {
        var txt;
        var r = confirm("Delete " + milestoneAssessed + " Milestone");
        if (r == true) {
            DeleteMilestone(milestoneId, clickSection);
        }
        else {
            return "Milestone not deleted";
        }
    }

    function DeleteMilestone(milestoneId, clickSection)
    {
        $.ajax({
            type: "POST",
            url: "../WebService/NeonatalEncounterService.asmx/DeleteMilestone",
            data: "{'milestoneId': '" + milestoneId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                getMilestonesTbl.row($(clickSection).parents('tr'))
                    .remove().draw();
                var index = reactionEventList.indexOf($(clickSection).parents('tr').find('td:eq(0)').text());
                if (index > -1) {
                    reactionEventList.splice(index, 1);
                }
                toastr.success("Milestone Deleted");
            },
            error: function (response) {
                toastr.error("Milestone not deleted");
            }
        });
    }

    $("#dtlImmunizationHistory").on('click', '.btnDelete', function () {
        var immunizationId = getImmunizationTbl.row($(this).parents('tr')).data()["0"];
        var immunizationPeriod = getImmunizationTbl.row($(this).parents('tr')).data()["1"];
        var clickSection = this;
        var delStatus = confirmDeleteImmunization(immunizationId, immunizationPeriod, clickSection);
        if (delStatus == "Immunization not deleted") {
            toastr.error("immunization not deleted");
        }
    });

    function confirmDeleteImmunization(immunizationId, immunizationPeriod, clickSection) {
        var txt;
        var r = confirm("Delete " + immunizationPeriod + " Period");
        if (r == true) {
            DeleteImmunization(immunizationId, clickSection);
        }
        else {
            return "Immunization not deleted";
        }
    }

    function DeleteImmunization(immunizationId, clickSection) {
        $.ajax({
            type: "POST",
            url: "../WebService/NeonatalEncounterService.asmx/DeleteImmunization",
            data: "{'ImmunizationId': '" + immunizationId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                getImmunizationTbl.row($(clickSection).parents('tr'))
                    .remove().draw();
                var index = reactionEventList.indexOf($(clickSection).parents('tr').find('td:eq(0)').text());
                if (index > -1) {
                    reactionEventList.splice(index, 1);
                }
                toastr.success("Immunization Deleted");
            },
            error: function (response) {
                toastr.error("Immunization not deleted");
            }
        });
    }
    $(document).ready(function () {
        var patientAge = <%=age%>;
        var screeningDone = <%=screeningValue%>;
        var immscreeningvalue = <%=immscreeningvalue%>;
        if (patientAge > 5) {
            if (immscreeningvalue > 0) {
                $("#neonatalrecord").hide();
                $("#neonatalform").hide();
                $("#immunizationform").hide();
            }
            else {
                $("#neonatalcontainer").hide();
            }
        }
        else {
            showHideNeonatalHistoryPanel();
        }
    });

    $("input[name = '<%=rbList.UniqueID %>']").change(function () {
        showHideNeonatalHistoryPanel();
    });

    function showHideNeonatalHistoryPanel() {
        var radioButtons = $("input[name='<%=rbList.UniqueID%>']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 1)
        {
            $("#neonatalcontentpanel").hide();
        }
        else
        {
            $("#neonatalcontentpanel").show();
        }
    }
</script>

			