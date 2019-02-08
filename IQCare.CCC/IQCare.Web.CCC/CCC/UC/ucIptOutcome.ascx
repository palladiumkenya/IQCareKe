<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIptOutcome.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucIptOutcome" %>

<div class="col-md-12 form-group">
    <div class="col-md-12 form-group">
        <div class="col-md-12">
            <div class="col-md-12">
                <label class="control-label pull-left">Event</label>
            </div>
            <div class="col-md-12">
                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="iptEvent" ClientIDMode="Static" onclick="IptOutcomeEvent();" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="col-md-12">
                <label class="control-label pull-left">OutComeDate</label>
            </div>
         <div class="col-md-12">
									<div class="datepicker" id="IPTOutcomeDate">
										<div class="input-group">
											<asp:TextBox ID="IPTDate"   runat="server"   ClientIDMode="Static" class="form-control input-sm" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
											<%--<input class="form-control input-sm" id="VisitDate" type="text" runat="server" data-parsley-required="true" />--%>
											<div class="input-group-btn">
												<button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
													<span class="glyphicon glyphicon-calendar"></span>
													<span class="sr-only">Toggle Calendar</span>
												</button>
												<div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
													<div class="datepicker-calendar">
														<div class="datepicker-calendar-header">
															<button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
															<button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
															<button type="button" class="title" data-month="11" data-year="2014">
																<span class="month">
																	<span data-month="0">January</span>
																	<span data-month="1">February</span>
																	<span data-month="2">March</span>
																	<span data-month="3">April</span>
																	<span data-month="4">May</span>
																	<span data-month="5">June</span>
																	<span data-month="6">July</span>
																	<span data-month="7">August</span>
																	<span data-month="8">September</span>
																	<span data-month="9">October</span>
																	<span data-month="10">November</span>
																	<span data-month="11" class="current">December</span>
																</span><span class="year">2014</span>
															</button>
														</div>
														<table class="datepicker-calendar-days">
															<thead>
																<tr>
																	<th>Su</th>
																	<th>Mo</th>
																	<th>Tu</th>
																	<th>We</th>
																	<th>Th</th>
																	<th>Fr</th>
																	<th>Sa</th>
																</tr>
															</thead>
															<tbody></tbody>
														</table>
														<div class="datepicker-calendar-footer">
															<button type="button" class="datepicker-today">Today</button>
														</div>
													</div>
													<div class="datepicker-wheels" aria-hidden="true">
														<div class="datepicker-wheels-month">
															<h2 class="header">Month</h2>
															<ul>
																<li data-month="0">
																	<button type="button">Jan</button></li>
																<li data-month="1">
																	<button type="button">Feb</button></li>
																<li data-month="2">
																	<button type="button">Mar</button></li>
																<li data-month="3">
																	<button type="button">Apr</button></li>
																<li data-month="4">
																	<button type="button">May</button></li>
																<li data-month="5">
																	<button type="button">Jun</button></li>
																<li data-month="6">
																	<button type="button">Jul</button></li>
																<li data-month="7">
																	<button type="button">Aug</button></li>
																<li data-month="8">
																	<button type="button">Sep</button></li>
																<li data-month="9">
																	<button type="button">Oct</button></li>
																<li data-month="10">
																	<button type="button">Nov</button></li>
																<li data-month="11">
																	<button type="button">Dec</button></li>
															</ul>
														</div>
														<div class="datepicker-wheels-year">
															<h2 class="header">Year</h2>
															<ul></ul>
														</div>
														<div class="datepicker-wheels-footer clearfix">
															<button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
															<button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
														</div>
													</div>
												</div>
											</div>
										</div>
									
							  </div>
				
             </div>
            </div>
    
        <div class="col-md-12">
            <div class="col-md-12">
                <label class="control-label pull-left">Reason For Discontinuation</label>
            </div>
            <div class="col-md-12">
                <asp:TextBox runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="discontinuation" ClientIDMode="Static" />
            </div>
        </div>
        
    </div>
</div>


<script type="text/javascript">
     var tomorrow = new Date();
        var today = new Date();
    $(document).ready(function () {
       
    tomorrow.setDate(today.getDate() + 1);
        $("#<%=discontinuation.ClientID%>").prop('disabled', true);

        getIptOutcome();
    });

    $('#IPTOutcomeDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            defaultDate: today.getDate(),
            restricted: [{ from: tomorrow, to: Infinity }],
			
            allowInputToggle: true,
            useCurrent: false         
        });

    function IptOutcomeEvent() {
        if ($("#iptEvent :selected").text() === "Discontinued") {
            $("#<%=discontinuation.ClientID%>").prop('disabled', false);
        } else {
            $("#<%=discontinuation.ClientID%>").prop('disabled', true);
        }
    }


    function getIptOutcome() {
        
        var iptEvent = $("#iptEvent").val();
        var reasonForDiscontinuation = $("#discontinuation").val();
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientTbService.asmx/GetPatientIptOutcome",
            data: "{'patientId': '" + patientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d !== null) {
                    var ipt = JSON.parse(response.d);
                    $('#iptEvent').val(ipt.IptEvent);
                    $("#<%=discontinuation.ClientID%>").val(ipt.ReasonForDiscontinuation)

                    if (ipt.IPTOutComeDate != null || ipt.IPTOutComeDate != undefined || ipt.IPTOutComeDate != "") {
                        $('#IPTDate').val(ipt.IPTOutComeDate);
                    }
                }
            },
            error: function (response) {
                toastr.error(response.d, "Unable to fetch IPT Outcome");
            }
        });

    }

</script>
