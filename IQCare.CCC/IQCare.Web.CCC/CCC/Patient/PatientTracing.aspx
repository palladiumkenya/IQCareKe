<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientTracing.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientTracing" %>
<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
    <div class="patienttracingwrap">
        <div class="col-md-12 form-group">
			<div class="col-md-12">
				<div class="panel panel-info">
					<div class="panel-body">
						<div class="col-md-12 form-group">
							<label class="control-label pull-left">Patient Tracing</label>
						</div>

						<div class="col-md-12 form-group">
							<div class="col-md-4">
								<div class="col-md-12">
									<label class="control-label pull-left input-sm text-primary" for="tracingdate">Tracing Date</label>
								</div>
								<div class="col-md-12">
                                    <div class='input-group date' id='patienttracingdate'>
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                        <input type="text" Class="form-control input-sm" id="tracingdate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" data-parsley-min-message="Input the appointment date" value=""/>
                                        <input type="hidden" id="tracingdateholder" />
                                    </div>
								</div>
							</div>
							<div class="col-md-4">
								<div class="col-md-12">
									<label class="control-label pull-left input-sm text-primary" for="tracingmethod">Tracing Method</label>
								</div>
								<div class="col-md-12">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="tracingmethod" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="col-md-12">
									<label class="control-label pull-left input-sm text-primary" for="tracingoutcome">Tracing Outcome</label>
								</div>
								<div class="col-md-12">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="tracingoutcome" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
								</div>
							</div>
						</div>
                        <!-- Other tracing outcome -->
                        <div class="col-md-12 form-group" style="display: none;" id="othertracingoutcomepanel"> 
                            <div class="col-md-12">
                                <div class="col-md-12">
								    <label class="control-label pull-left input-sm text-primary" for="othertracingoutcome">Other Tracing Outcome</label>
							    </div>
							    <div class="col-md-12">
								    <textarea class="form-control input-sm" id="othertracingoutcome"></textarea>
							    </div>
                            </div>
                        </div>
                        <!-- Death tracing -->
                        <div class="col-md-12 form-group" id="deathoutcome" style="display: none">
							<div class="col-md-4">
								<div class="col-md-12">
									<label class="control-label pull-left input-sm text-primary" for="tracingdate">Date of Death</label>
								</div>
								<div class="col-md-12">
                                    <div class='input-group date' id='patientDateofDeath'>
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                        <asp:TextBox runat="server"  CssClass="form-control input-sm" ID="tracingdateofdeath" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" data-parsley-min-message="Input the appointment date"></asp:TextBox>
                                        <input type="hidden" id="tracingdateofdeathholder" />
                                    </div>
								</div>
							</div>
						</div>
                        <!-- Transfer tracing -->
                        <div class="col-md-12 form-group" id="transferoutcome" style="display: none;">
							<div class="col-md-4">
								<div class="col-md-12">
									<label class="control-label pull-left input-sm text-primary" for="tracingdate">Date of Transfer</label>
								</div>
								<div class="col-md-12">
                                    <div class='input-group date' id='patientDateofTransfer'>
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                        <asp:TextBox runat="server"  CssClass="form-control input-sm" ID="tracingdateoftransfer" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" data-parsley-min-message="Input the appointment date"></asp:TextBox>
                                         <input type="hidden" id="tracingdateoftransferholder" />
                                    </div>
								</div>
							</div>
							<div class="col-md-4">
								<div class="col-md-12">
									<label class="control-label pull-left input-sm text-primary" for="tracingmethod">Tracing Out Facility</label>
								</div>
								<div class="col-md-12">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="transferfacility" ClientIDMode="Static" data-parsley-min-message="Value Required" />
								</div>
							</div>
						</div>
                        <!-- Ready for reinitianing -->
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <div class="col-md-12">
								    <label class="control-label pull-left input-sm text-primary" for="tracingnotes">Tracing Notes</label>
							    </div>
							    <div class="col-md-12">
								    <textarea class="form-control input-sm" id="tracingnotes"></textarea>
							    </div>
                            </div>
                        </div>
					</div>
				</div>
			</div>
            <div class="modal-footer">
				<button type="button" class="btn btn-danger fa fa-times" data-dismiss="modal"> Cancel </button>
				<button type="button" class="btn btn-primary fa fa-plus-circle" id="btnSavePatientTracing"> Save changes </button>
			</div>
		</div>
    </div>
    <IQ:ucExtruder runat="server" ID="ucExtruder" />
    <script>
        $("#patienttracingdate").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: true
        }).on("dp.change", function (selectedDate) {
            var selectedday = $("#tracingdate").val();
            $("#tracingdateholder").val(selectedday);
        });

        $("#patientDateofTransfer").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: true
        }).on("dp.change", function (selectedDate) {
            var selectedday = $("#tracingdateofdeath").val();
            $("#tracingdateofdeathholder").val(selectedday);
        });

        $("#patientDateofDeath").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: true
        }).on("dp.change", function (selectedDate) {
            var selectedday = $("#tracingdateoftransfer").val();
            $("#tracingdateoftransferholder").val(selectedday);
        });


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


        var tracingdata = getTracingData();
        function getTracingData() {
            var visitid = GetURLParameter('visitId');
            if (visitid > 0) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/ReportingService.asmx/gettracingdata",
                    data: "{'visitid':'" + visitid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $.each(JSON.parse(response.d), function (index, value) {
                            //var tracingDate = new Date(this.TracingDate);
                            console.log(value);
                            console.log(index);
                            // DateTracingDone
                            $("#tracingdate").val(moment(value.DateTracingDone).format('DD-MMM-YYYY'));
                            $("#tracingmethod").val(value.Mode);
                            $("#tracingoutcome").val(value.Outcome);
                            $("#tracingnotes").val(value.Remarks);
                        });
                    }
                });
            }
        }

        $('#tracingoutcome').on('change', function () {
            var tracingoutcome = $("#tracingoutcome option:selected").text();

            if(tracingoutcome.includes("Dead")){
                $("#deathoutcome").show();
            }
            else {
                $("#deathoutcome").hide();
            }

            if(tracingoutcome.includes("transfer")){
                $("#transferoutcome").show();
            }
            else {
                $("#transferoutcome").hide();
            }

            if(tracingoutcome.includes("Other")){
                $("#othertracingoutcomepanel").show();
            }
            else {
                $("#othertracingoutcomepanel").hide();
            }
        });

        $("#btnSavePatientTracing").click(function () {
            var PatientId = '<%=PatientId%>';
            var PersonId= '<%=PersonId%>';
            var tracingoutcometext = $("#tracingoutcome option:selected").text();
            var tracingdate = $("#tracingdateholder").val();
            var tracingmethod = $("#tracingmethod").val();
            var tracingoutcome = $("#tracingoutcome").val();
            var othertracingoutcome = $("#othertracingoutcome").val();
            var tracingdateofdeath = $("#tracingdateofdeathholder").val();
            var tracingdateoftransfer = $("#tracingdateoftransferholder").val();
            var transferfacility = $("#transferfacility").val();
            var tracingnotes = $("#tracingnotes").val();

            if (tracingdate == "" || new Date(tracingdate) > new Date()) {
                toastr.error("Tracing date should not be greater than today or blank.");
            }
            else if (tracingmethod == 0) {
                toastr.error("Please select tracing method.");
            }
            else if (tracingoutcome == 0) {
                toastr.error("Please select tracing outcome.");
            }
            else {
                if (tracingoutcometext.includes("Dead") && (tracingdateofdeath == "" || new Date(tracingdateofdeath) > new Date())) {
                    toastr.error("Date of death should not be greater than today or blank.");
                }
                else if (tracingoutcometext.includes("transfer") && (transferfacility == "")) {
                    toastr.error("Please input transfer facility.");
                }
                else {
                    var tracingStatus = "";
                    if (tracingoutcometext.includes("Did not attempt to trace patient")) {
                        tracingStatus = 0;
                    }
                    else {
                        tracingStatus = 1;
                    }
                    $.ajax({
                        type: "POST",
                        url: "../WebService/ReportingService.asmx/saveTracingData",
                        data: "{'PatientId':'" + PatientId + "','PersonId':'" + PersonId + "','tracingdate':'" + tracingdate + "','tracingmethod':'" + tracingmethod + "','tracingoutcome':'" + tracingoutcome + "','othertracingoutcome':'" + othertracingoutcome + "'," +
                            "'tracingdateofdeath': '" + tracingdateofdeath + "','tracingdateoftransfer':'" + tracingdateoftransfer + "','transferfacility':'" + transferfacility + "','tracingnotes':'" + tracingnotes + "','tracingstatus':'"+tracingStatus+"'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: false,
                        success: function (response) {
                            toastr.success("Tracing Data Saved");
                            setTimeout(function () {
                                window.location.href = '<%=ResolveClientUrl("~/CCC/Home.aspx") %>';
                            }, 2000);
                        },
                        error: function (response) {
                            toastr.error("Error saving tracing data");
                        }
                    });
                }
            }
        });
    </script>
</asp:Content>
