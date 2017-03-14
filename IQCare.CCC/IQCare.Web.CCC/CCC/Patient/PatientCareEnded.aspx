<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientCareEnded.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientCareEnded" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">
        <div class="col-md-12 form-group">
            <div class="col-md-12">
                <label class="control-label pull-left text-info">Patient Care Summary</label>
            </div>
            <table class="table table-hover" id="tblCareEnded" clientidmode="Static" runat="server">
                <thead>
                    <tr class="active">
                        <th><span class="text-primary" aria-hidden="true">#</span></th>
                        <th><span class="text-primary" aria-hidden="true">Exit Date</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Exit Reason</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Status</span> </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div id="CareEndedForm" data-parsley-validate="true" data-show-errors="true" class="col-md-12 form-group" style="padding-left: 0px;">
            <label class="control-label pull-left text-info">Patient Care Summary</label>      
            <div class="col-md-12 form-group" style="padding-left: 0px;">
                
                <div class="col-md-3">
                    <div class="form-group">
                        <div class="col-md-12">
                            <label for="reason" class="control-label pull-left">Reason</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                        </div>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <div class="col-md-12">
                            <label for="reason" class="control-label pull-left">Care Ending Notes</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtCareEndingNotes" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Date</label>
                        </div>
                        <div class="col-md-12">
                            <div class="datepicker fuelux form-group" id="CareEndDate">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="AppointmentDate"></asp:TextBox>
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
                                                        </span><span class="year">2017</span>
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
                </div>

                <div class="col-md-3">
                    <div class="col-md-12">&nbsp;</div>
                    <div class="col-md-12">
                        <asp:LinkButton runat="server" ID="EndCare" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-minus-circle">End Care</asp:LinkButton>
                    </div>
                </div> 

            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#CareEndDate").datepicker({
                date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#EndCare").click(function () {
                if ($("#CareEndedForm").parsley().validate()) {
                    $.when(endCare()).then(function() {
                        setTimeout(function() {
                                getCareEnded();
                            },
                            2000);
                    });
                } else {
                    return false;
                }
                
            });

            getCareEnded();

            function endCare() {
                var careEndedDate = $('#CareEndDate').datepicker('getDate');
                var reason = $("#<%=Reason.ClientID%>").val();
                var careEndingNotes = escape($("#<%=txtCareEndingNotes.ClientID%>").val());

                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/EndPatientCare",
                    data: "{'exitDate':'" + moment(careEndedDate).format('DD-MMM-YYYY') + "','exitReason':'" + reason + "','careEndingNotes':'" + careEndingNotes + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "Care Ended");
                    },
                    error: function (response) {
                        toastr.error(response.d, "Care Ended Error");
                    }
                });
            }

            function getCareEnded() {
                $.ajax(
                {
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/GetPatientCareEnded",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function (response) {
                        //$("#tblCareEnded > tbody").empty();
                        $('#tblCareEnded tr:not(:first)').remove();
                        console.log(response.d);
                        var itemList = response.d;
                        var table = '';
                        itemList.forEach(function (item, i) {
                            n = i + 1;
                            table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + moment(item.ExitDate).format('DD-MMM-YYYY') + '</td><td style="text-align:left">' + item.ExitReason + '</td><td style="text-align: left">' + item.Status + '</td></tr>';
                        });

                        $('#tblCareEnded').append(table);

                    },

                    error: function (msg) {
                        console.log(msg.responseText);
                    }
                });
            }
        });
        
    </script>
</asp:Content>
