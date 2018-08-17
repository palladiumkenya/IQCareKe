 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucICF.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucICF" %>
<style>
    #ICFScreeningSection, #TuberclosisTreatmentPanel, #IPTPanel, #tbScreeningOutcomePanel, #ICFActionTakenPanel{display: none;}
</style>
<div class="clearfix"></div>
<div class="icfwrap">
    <%--ICF - Intensified Case Finding--%>
    <div class="col-md-12 form-group" id="ICFPanel">
	    <div class="col-md-12">
		    <div class="panel panel-info">
			    <div class="panel-body">
				    <div class="row">
					    <div class="col-md-12 form-group">
                            <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">TB Intensified Case Finding</label>
					    </div>
				    </div>
                    
				    <div class="row">
					    <div class="col-md-6  form-group">
						    <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlOnAntiTBDrugs">Currently on Anti-TB Drugs?</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlOnAntiTBDrugs" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
					    </div>
				    </div>
                    <div class="row" id="ICFScreeningSection">
                        <div class="col-md-3  form-group">
						    <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlICFCough">Cough</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFCough" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
					    </div>
                        <div class="col-md-3  form-group">
						    <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlICFFever">Fever</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFFever" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
					    </div>
                        <div class="col-md-3  form-group">
						    <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlICFWeight">Noticeable Weight Loss</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFWeight" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
					    </div>
                        <div class="col-md-3  form-group">
						    <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlICFNightSweats">Night Sweats</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFNightSweats" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
					    </div>
                    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <%--//Tuberclosis Treatment--%>
    <%--ICF Action Taken--%>
    <div class="col-md-12 form-group" id="ICFActionTakenPanel">
	    <div class="col-md-12">
		    <div class="panel panel-info">
			    <div class="panel-body">
                    <div class="row">
					    <div class="col-md-12 form-group">
                            <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">ICF Action Taken</label>
					    </div>
				    </div>
                    <div class="row" id="ICFActionScreeningSection">
                        <div class="col-md-4">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlSputumSmear">Sputum Smear</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlSputumSmear" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlGeneXpert">Gene Xpert</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlGeneXpert" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlChestXray">Chest X-Ray</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlChestXray" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlStartAntiTB">Start Anti-TB</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlStartAntiTB" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlInvitationofContacts">Invitation of Contacts</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlInvitationofContacts" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlEvaluatedforIPT">Evaluated for IPT</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlEvaluatedforIPT" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                    </div>
                </div>
		    </div>
	    </div>
    </div>
    <%--//ICF Action Taken--%>
    <%--Tuberclosis Treatment--%>
    <div class="col-md-12 form-group" id="TuberclosisTreatmentPanel">
	    <div class="col-md-12">
		    <div class="panel panel-info">
			    <div class="panel-body">
                    <div class="row" id="TBRXSection">
                        <div class="col-md-12 form-group">
                            <label class="control-label pull-left input-sm text-primary">Tuberclosis Treatment</label>
					    </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-4  form-group">
						        <div class="col-md-12">
							        <label class="control-label pull-left input-sm" for="tbTBRXStartDate">TB Rx Start Date</label>
						        </div>
						        <div class="col-md-12">
							        <div class='input-group date icfdate'>
						                <span class="input-group-addon">
							                <span class="glyphicon glyphicon-calendar"></span>
						                </span>
						                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="tbTBRXStartDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					                </div>
						        </div>
					        </div>
                            <div class="col-md-4  form-group">
						        <div class="col-md-12">
							        <label class="control-label pull-left input-sm" for="tbTBRXEndDate">TB Rx End Date</label>
						        </div>
						        <div class="col-md-12">
							        <div class='input-group date icfdate'>
						                <span class="input-group-addon">
							                <span class="glyphicon glyphicon-calendar"></span>
						                </span>
						                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="tbTBRXEndDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					                </div>
						        </div>
					        </div>
                            <div class="col-md-4  form-group">
						        <div class="col-md-12">
							        <label class="control-label pull-left input-sm" for="ddlICFRegimen">Regimen</label>
						        </div>
						        <div class="col-md-12">
							        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFRegimen" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						        </div>
					        </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <%--//Tuberclosis Treatment--%>
    <%--Tuberclosis Screening Outcome --%>
    <div class="col-md-12 form-group" id="tbScreeningOutcomePanel">
	    <div class="col-md-12">
		    <div class="panel panel-info">
			    <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">Tuberclosis Screening Outcome</label>
					    </div>
                        <div class="col-md-4 text-center center-block">
                            <div class="col-md-12">
							    <label class="control-label pull-left input-sm" for="ddlICFTBScreeningOutcome">Outcome</label>
						    </div>
						    <div class="col-md-12">
							    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFTBScreeningOutcome" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						    </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <%--//Tuberclosis Screening Outcome--%> 
    <%--IPT--%>
    <div class="col-md-12 form-group" id="IPTPanel">
	    <div class="col-md-12">
		    <div class="panel panel-info">
			    <div class="panel-body">
                    <div class="row" id="IPTSection">
                        <div class="col-md-12 form-group">
                            <label class="control-label pull-left input-sm text-primary">IPT</label>
					    </div>
                        <div class="col-md-12">
                            <div class="col-md-6 text-center">
                                <div class="col-md-12">
							        <label class="control-label pull-left input-sm" for="ddlICFCurrentlyOnIPT">Currrently on IPT?</label>
						        </div>
						        <div class="col-md-12">
							        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFCurrentlyOnIPT" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						        </div>
                            </div>
                            <div class="col-md-6 text-center">
                                <div class="col-md-12">
							        <label class="control-label pull-left input-sm" for="ddlICFStartIPT">Start IPT?</label>
						        </div>
						        <div class="col-md-12">
							        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFStartIPT" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						        </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                        <div style="height: 30px;"></div>
                        <div class="col-md-12">
                            <div class="col-md-4">
							    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIptWorkUp2" data-toggle="modal" data-target="#IptClientWorkupModal">IPT Client Workup</button>
						    </div>
						    <div class="col-md-4">
							    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIpt2" data-toggle="modal" data-target="#IptDetailsModal">IPT Follow Up</button>
						    </div>
						    <div class="col-md-4">
							    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIptOutcome2" data-toggle="modal" data-target="#IptOutcomeModal">IPT Outcome</button>
						    </div>
                        </div>
                    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <%--//IPT--%>
    <div class="clearfix"></div>
</div>
<script type="text/javascript">
    $(".icfdate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });
    function showHideCtrls(objectsToHide, objectsToShow) {
        $.each(objectsToHide, function (index, value) {
            $("#" + value).hide();
        });
        $.each(objectsToShow, function (index, value) {
            $("#" + value).show();
        });
    }
    function sectionReset(sectionsToReset) {
        $.each(sectionsToReset, function (index, value) {
            $("#" + value + " select").each(function () {
                $(this).prop('selectedIndex', 0);
            });
        });
    }
    function disableEnableCtrls(objectsToDisable,objectsToEnable) {
        $.each(objectsToDisable, function (index, value) {
            $("#" + value).prop('disabled', true);
        });
        $.each(objectsToEnable, function (index, value) {
            $("#" + value).prop('disabled', false);
        });
    }
    function getTBOutcome(tbScreenScore) {
        $("#ddlICFTBScreeningOutcome").prop('selectedIndex', tbScreenScore);
    }
    //Currently on Anti-TB Drugs selection change
    $('#ddlOnAntiTBDrugs').change(function () {
        var selectedIndex = ($(this).prop('selectedIndex'));
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        var tbScreenScore = 0;
        if (selectedIndex == 1) {
            objectsToShow = ["tbScreeningOutcomePanel"];
            objectsToHide = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
            sectionsToReset = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
            tbScreenScore = 4;
        }
        else if (selectedIndex == 2) {
            objectsToShow = ['ICFScreeningSection','IPTPanel'];
            objectsToHide = ['tbScreeningOutcomePanel'];
            sectionsToReset = ['tbScreeningOutcomePanel'];
        }
        else {
            sectionsToReset = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel', 'tbScreeningOutcomePanel'];
            objectsToHide = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel', 'tbScreeningOutcomePanel'];
            objectsToShow = [];
        }
        showHideCtrls(objectsToHide, objectsToShow); 
        sectionReset(sectionsToReset);
        getTBOutcome(tbScreenScore);
    });
    //ICF Screening selection change
    $("#ICFScreeningSection select").change(function (evt, data) {
        var totalIndex = 0;
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        $("#ICFScreeningSection select").each(function () {
            var selectedIndex = ($(this).prop('selectedIndex'));
            if (selectedIndex == 1) {
                totalIndex = totalIndex + 1;
            }
            else {
                totalIndex = totalIndex + 0;
            }
        });
        if (totalIndex >= 1) {
            objectsToShow = ['ICFActionTakenPanel'];
            objectsToHide = [];
        }
        else {
            objectsToShow = [];
            objectsToHide = ['ICFActionTakenPanel'];
            sectionsToReset = ['ICFActionTakenPanel'];
            //Reset action taken controls
            $("#ICFActionTakenPanel select").each(function () {
                $(this).prop('selectedIndex', 0);
            });
        }
        showHideCtrls(objectsToHide, objectsToShow);
        sectionReset(sectionsToReset);
    });
    //ICF Action Screening Section Selection Change
    $("#ICFActionScreeningSection select").change(function (evt, data) {
        var IPTScore = 0;
        var TBScreeningScore = 0;
        var TBTreatmentScore = 0;
        var TBOutcomeScore = 0;
        var tbScreenScore = 0;
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        var objectsToDisable = [];
        var objectsToEnable = [];
        $("#ICFActionScreeningSection select").each(function () {
            var selectedIndex = ($(this).prop('selectedIndex'));
            if (selectedIndex == 1) {
                TBOutcomeScore = TBOutcomeScore + 1;
                IPTScore = IPTScore + 0;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
            else if (selectedIndex == 2) {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 0;
                TBTreatmentScore = TBTreatmentScore + 1;
            }
            else if (selectedIndex == 3) {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 1;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
            else if (selectedIndex == 4) {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 1;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
            else {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 0;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
        });
        if (TBTreatmentScore >= 1) {
            objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
            objectsToShow = ['TuberclosisTreatmentPanel', 'tbScreeningOutcomePanel'];
            tbScreenScore = 2;
        }
        else {
            if (TBOutcomeScore >= 1) {
                objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                objectsToHide = ['TuberclosisTreatmentPanel'];
                objectsToShow = ['tbScreeningOutcomePanel'];
                tbScreenScore = 2;
            }
            else {
                if (IPTScore>=1){
                    objectsToEnable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                    objectsToHide = ['TuberclosisTreatmentPanel'];
                    objectsToShow = ['tbScreeningOutcomePanel'];
                    tbScreenScore = 1;
                }
                else {
                    objectsToEnable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                    objectsToHide = ['TuberclosisTreatmentPanel'];
                    tbScreenScore = 0;
                } 
            }
        }
        showHideCtrls(objectsToHide, objectsToShow);
        sectionReset(sectionsToReset);
        disableEnableCtrls(objectsToDisable, objectsToEnable);
        getTBOutcome(tbScreenScore);
    });
    //start TBRx
    $("#ddlICFRegimen").change(function (evt, data) {
        var tbScreenScore = 0;
        var selectedIndex = ($(this).prop('selectedIndex'));
        var todayDate = new Date();
        var tbrxStartDate = $("#tbTBRXStartDate").val();
        var tbrxEndDate = $("#tbTBRXEndDate").val();
        alert(todayDate);
        if (selectedIndex >= 1 && tbrxStartDate != "") {
            if (new Date(tbrxStartDate) <= new Date(todayDate) && new Date(tbrxEndDate) >= new Date(todayDate)) {
                tbScreenScore = 4;
            }
            else {
                tbScreenScore = 2;
            }
        }
        else {
            tbScreenScore = 2;
        }
        getTBOutcome(tbScreenScore);
    }); 

    ///SAVE DATA
    function addPatientIcf() {
        var cough = $("#<%=ddlICFCough.ClientID%>").val();
        var weightLoss = $("#<%=ddlICFWeight.ClientID%>").val();
        var nightSweats = $("#<%=ddlICFNightSweats.ClientID%>").val();
        var fever = $("#<%=ddlICFFever.ClientID%>").val();
        var onIpt = $("#<%=ddlICFCurrentlyOnIPT.ClientID%>").val();
        var onAntiTbDrugs = $("#<%=ddlOnAntiTBDrugs.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var everBeenOnIpt = $("#<%=ddlICFStartIPT.ClientID%>").val();
        $.ajax({
            type: "POST",
            url: "../WebService/PatientTbService.asmx/AddPatientIcf",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','cough': '" + cough + "','fever': '" + fever + "','nightSweats': '" + nightSweats + "','weightLoss': '" + weightLoss + "','onAntiTbDrugs': '" + onAntiTbDrugs + "','onIpt': '" + onIpt + "','everBeenOnIpt': '" + everBeenOnIpt + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Patient ICF saved successfully");
            },
            error: function (response) {
                toastr.error(response.d, "Patient ICF not saved");
            }
        });
    }



</script>