<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.CCC.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12 col-xs-12 col-sm-12">
        <div class="col-md-8 col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="col-md-6" style="padding:1px">
                                <label class="control-label pull-left fa fa-sort-amount-asc"> Filter by Date </label>
                            </div>
                            <div class="col-md-6">
                                <div class="datepicker fuelux form-group" id="AppointmentDate">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="Date"></asp:TextBox>
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

                        <div class="col-md-12">
                            <hr />
                        </div>

                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-12 col-sm-12" style="padding:3px">
                                <div class="col-md-6"><h5 class="pull-left"><asp:Label runat="server">Booked:</asp:Label></h5></div>
                                <div class="col-md-6">
                                    <h6> <asp:Label runat="server" ID="lblbooked" CssClass="text-info pull-left"><span class="badge">0</span></asp:Label></h6>
                                </div>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-12" style="padding:3px">
                                <div class="col-md-6"><h5 class="pull-left"><asp:Label runat="server">Visited:</asp:Label></h5></div>
                                <div class="col-md-6">
                                     <h6><asp:Label runat="server" ID="lblvisited" CssClass="text-info pull-left"><span class="badge"> 0 </span> </asp:Label></h6>
                                </div>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-12" style="padding:3px">
                                <div class="col-md-8" ><h5 class="pull-left"><asp:Label runat="server">Not Visited :</asp:Label></h5></div>
                                <div class="col-md-3">
                                    <h6> <asp:Label runat="server" ID="lblpending" CssClass="text-info pull-left"><span class="badge"> 0 </span> </asp:Label></h6>
                                </div>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-12" style="padding:3px">
                                <div class="col-md-8"><h5 class="pull-left"><asp:Label runat="server">Walk-ins :</asp:Label></h5></div>
                                <div class="col-md-4">
                                    <h6> <asp:Label runat="server" ID="lblwalkins" CssClass="text-info pull-left"> <span class="badge"> 0 </span></asp:Label></h6>
                                </div>
                            </div>
                            <div class="col-md-12"><hr/></div>
                 
                        </div> <!-- .col-md-12 -->
                        <div class="col-md-12">
                           <div class="col-md-12"><h4 class="pull-left"><asp:Label runat="server" ID="lblcame"> 0</asp:Label></h4></div> 
                        </div>
           
                    </div> <!-- .panel-body -->
                </div>

        </div><!-- .col-md-8 -->
        <div class="col-md-4 col-xs-12 col-xs-12">
            <div class="col-md-12 label label-info">
               <label class="label label-info pull-left fa fa-line-chart fa-2x"> Facility Viral Load Order/Results Tracker</label>
            </div>
            <div class="col-md-12"><hr /></div>
            <div class="col-md-12">
                <div class="col-md-7"><label class="control-label pull-left">Pending VL Tests</label></div>
                <div class="col-md-5 pull-right">
                    <asp:Label runat="server" ClientIDMode="Static" ID="pendingVL" CssClass="control-label text-success pull-right"></asp:Label>
                </div>
            </div>
            <div class="col-md-12"><hr></div>

            <div class="col-md-12">
                 <div class="col-md-7"><label class="control-label pull-left">Complete VL Tests</label></div>
                <div class="col-md-5 pull-right">
                    <asp:Label runat="server" ClientIDMode="Static" ID="completeVL" CssClass="control-label text-success pull-right"></asp:Label>
                </div>
            </div>
            <div class="col-md-12"><hr /></div>
            <div class="col-md-12">
                <div class="col-md-7"><label class="control-label pull-left text-danger">Total Suppressed </label></div>
                <div class="col-md-5 pull-right">
                    <asp:Label runat="server" ClientIDMode="Static" ID="lblsuppressed" CssClass="control-label text-success pull-right"><span class="badge pull-right">0</span></asp:Label>
                </div>
            </div>
              <div class="col-md-12">
                   <div class="col-md-12"><hr /></div>
                <div class="col-md-7"><label class="control-label pull-left text-danger">Total Unsuppressed </label></div>
                <div class="col-md-5 pull-right">
                    <asp:Label runat="server" ClientIDMode="Static" ID="lblunsuppressed" CssClass="control-label text-success pull-right"><span class="badge pull-right">0</span></asp:Label>
                </div>
            </div>
            <div class="col-md-12"><hr></div>

            <div class="col-md-12">
                
                <div class="col-md-12 pull-right"><h5 class="pull-left"><asp:Label runat="server" ID="lblvl">0% </asp:Label></h5></div>
            </div>
        </div>
    </div><!-- .col-md-12 -->

    <div class="col-md-12 col-xs-12 col-sm-12">
         <div class="col-md-4 col-xs-12 col-sm-12">
                <div class="col-md-12 label label-info">
                  <label class="label label-info pull-left fa fa-bar-chart fa-2x"> Facility Summary </label>
                </div>
                <div class="col-md-12" style="padding-bottom:2%"></div>
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Cumulative patients :</label></div>
                    <div class="col-md-2 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lblTotalPatients" CssClass="control-label text-success pull-right"><span class="badge pull-right">0</span></asp:Label>
                    </div>
                </div>           
                <div class="col-md-12"><hr></div>

                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Active on ART :</label></div>
                        <div class="col-md-5 pull-right">
                            <asp:Label runat="server" ClientIDMode="Static" ID="lblOnART" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                        </div>
                </div>
                <div class="col-md-12"><hr></div>
                
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total on CTX/Dapson :</label></div>
                    <div class="col-md-5 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lblctx" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                    </div>
                </div>
                <div class="col-md-12"><hr></div>
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Transit :</label></div>
                    <div class="col-md-5 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lbltransit" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                    </div>
                </div> 
                <div class="col-md-12"><hr></div>
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Transfer-Ins :</label></div>
                    <div class="col-md-5 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lbltransferin" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                    </div>
                </div>
             <div class="col-md-12"><hr></div>
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Undcoumented LTF :</label></div>
                    <div class="col-md-5 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lblundocumentedltf" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                    </div>
                </div>

         </div>
         
             <div class="col-md-4 col-xs-12 col-sm-12">
                <div class="col-md-12 label label-info">
                  <label class="label label-info pull-left fa fa-pie-chart fa-2x"> Care Ending Summary </label>
                </div>
                <div class="col-md-12" style="padding-bottom:2%"></div>
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Dead:</label></div>
                    <div class="col-md-2 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lbldead" CssClass="control-label text-success pull-right"><span class="badge pull-right">0</span></asp:Label>
                    </div>
                </div>           
                <div class="col-md-12"><hr></div>

                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Transfer Out :</label></div>
                        <div class="col-md-5 pull-right">
                            <asp:Label runat="server" ClientIDMode="Static" ID="lbltransferout" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                        </div>
                </div>
                <div class="col-md-12"><hr></div>
                
                <div class="col-md-12">
                    <div class="col-md-7"><label class="control-label pull-left">Total Documented LTFU :</label></div>
                    <div class="col-md-5 pull-right">
                        <asp:Label runat="server" ClientIDMode="Static" ID="totalltu" CssClass="control-label text-success pull-right"><span class="badge">0</span></asp:Label>
                    </div>
                </div>
                <div class="col-md-12"><hr></div>
         </div>
      

         <div class="col-md-4 col-xs-12 col-sm-12">
              <div class="col-md-12 label label-warning">
                <label class="label label-warning fa fa-bar-chart fa-2x pull-left"> Differentiated Care model Statistics</label>
              </div>
              <div class="col-md-12"><hr /></div>
              <div class="col-md-12">
                <div class="col-md-7"><label class="control-label pull-left">S=Stable Patients</label></div>
                <div class="col-md-5 pull-right">
                    <asp:Label runat="server" ClientIDMode="Static" ID="lblstable" CssClass="control-label text-success pull-right"> <span class="badge"> 0 </span> </asp:Label></asp:Label>
                </div>
            </div>
            <div class="col-md-12"><hr></div>
            <div class="col-md-12">
                 <div class="col-md-7"><label class="control-label pull-left">U=Unstable Patients</label></div>
                <div class="col-md-5 pull-right">
                    <asp:Label runat="server" ClientIDMode="Static" ID="lblunstable" CssClass="control-label text-success pull-right"> <span class="badge"> 0 </span> </asp:Label></asp:Label>
                </div>
            </div>
            <div class="col-md-12"><hr /></div>
            <div class="col-md-12">
                 <div class="col-md-6"><h5 class="pull-left"><asp:Label runat="server"> </asp:Label></h5></div>
            </div>


         </div>
    </div> <!-- .col-md-12-->
    
    <%--<div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">
        <div class="col-md-12 form-group">
            <div class="col-md-4">
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="datepicker fuelux form-group" id="AppointmentDate">
                            <div class="input-group">
                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="Date"></asp:TextBox>
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
            <div class="col-md-12">
                <table class="table table-hover" id="tblAppointment" clientidmode="Static" runat="server">
                    <thead>
                        <tr class="active">
                            <th><span class="text-primary" aria-hidden="true">Total Appointments</span> </th>
                            <th><span class="text-primary" aria-hidden="true">Met Appointments</span> </th>
                            <th><span class="text-primary" aria-hidden="true">Missed Appointments</span> </th>
                            <th><span class="text-primary" aria-hidden="true">Walk-Ins</span> </th>
                        </tr>
                    </thead>
                    <tbody clientidmode="Static" id="appointmentBody">
                    </tbody>
                </table>
            </div>
        </div>
    </div>--%>
    <script type="text/javascript">
         var facilityId = '<%=AppLocationId%>';
    
        $(document).ready(function () {           

            var ptncame=0;
            $('#AppointmentDate').datepicker({
                allowPastDates: true,
                Date: 0,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            AppointmentStatistics();
      

        $("#Date").change(function () {
            AppointmentStatistics();
        });

            $('#AppointmentDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {
                AppointmentStatistics();
            });

            function AppointmentStatistics() {
                jQuery.support.cors = true;
                var date = $("#<%=Date.ClientID%>").val();
                $.ajax(
                {
                    type: "POST",
                    url: "WebService/FacilityService.asmx/GetAppointmentStatistics",
                    data: "{'date':'" + date + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function (response) {
                        var item = response.d;

                        $("#<%=lblbooked.ClientID%>").html("<span class='badge'>" + item.TotalAppointments + "</span>");
                        $("#<%=lblvisited.ClientID%>").html("<span class='badge'>" + item.MetAppointments + "</span>");
                        $("#<%=lblpending.ClientID%>").html("<span class='badge'>" + item.MissedAppointments + "</span>");
                        $("#<%=lblwalkins.ClientID%>").html("<span class='badge'>" + item.WalkIns + "</span>");

                        ptncame = (item.MetAppointments / (item.TotalAppointments) * 100);
                        !isNaN(ptncame) ? ptncame.toFixed(2) : ptncame=0;
                        if(ptncame<1){ptncame=0}
                        $("#<%=lblcame.ClientID%>").html("<span class='badge text-info'>"+ptncame+ " % </span> of <span class='badge'>"+item.TotalAppointments+"</span> patients booked, Honoured Appointment today!");

                        <%--                    var table = '';
                    table += '<tr><td style="text-align: left">' +
                        item.TotalAppointments +
                        '</td><td style="text-align: left">' +
                        item.MetAppointments +
                        '</td><td style="text-align: left">' +
                        item.MissedAppointments +
                        '</td><td style="text-align: left">' +
                        item.WalkIns +
                        '</td></tr>';
                   $('#<%=tblAppointment.ClientID%> tr').not(function(){ return !!$(this).has('th').length; }).remove();
                   $('#tblAppointment').append(table);--%>

                    },

                    error: function (msg) {
                        alert(msg.responseText);
                    }
                });
            }

            //console.log("get viral load  called");     
            var pending=0;
            var complete=0;
            var percentage = 0;
            var suppressed = 0;
            var unsuppressed = 0;
            var percentage_suppressed = 0;
            var percentage_unsuppressed = 0;
 
      
        $(document).ready(function () {  
           

            //console.log("get viral load  called");            

            $.ajax({
                url: 'WebService/LabService.asmx/GetFacilityVLPendingCount',
                data: "{'facilityId':'" + facilityId + "'}",
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    pending = response.d;
                    document.getElementById("pendingVL").innerHTML= "<span class='badge'>"+ response.d + "</span>";

                }

            });

            $.ajax({
                url: 'WebService/LabService.asmx/GetFacilityVLCompleteCount',
                data: "{'facilityId':'" + facilityId + "'}",
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    complete = response.d;
                    document.getElementById("completeVL").innerHTML = "<span class='badge'> " + response.d + " </span>";
                    percentage = ((complete / (pending + complete)) * 100);
                    !isNaN(percentage) ? percentage.toFixed(2) : percentage=0;
            $("#<%=lblvl.ClientID%>").html("Result Rate "+ percentage +" %");


                }

            });

            //suppression
              $.ajax({
                  url: 'WebService/LabService.asmx/GetFacilityViralLoadSuppressed',
                data: "{'facilityId':'" + facilityId + "'}",
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    console.log(response.d);
                    suppressed = response.d;                   
                  
                    document.getElementById("lblsuppressed").innerHTML = "<span class='badge'> " + suppressed + " </span>";
                    !isNaN(suppressed) ? suppressed.toFixed(2) : suppressed = 0;

                }

              });
              $.ajax({
                  url: 'WebService/LabService.asmx/GetFacilityViralLoadUnSuppressed',
                data: "{'facilityId':'" + facilityId + "'}",
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    console.log(response.d);
                    unsuppressed = response.d;
                    document.getElementById("lblunsuppressed").innerHTML = "<span class='badge'> " + unsuppressed + " </span>";
                    !isNaN(unsuppressed) ? unsuppressed.toFixed(2) : unsuppressed = 0;

                    //Percentages
                   // document.getElementById("lblsuppressed").innerHTML = "<span class='badge'> " + percentage_suppressed + "%" + " </span>";                  
                   // percentage_unsuppressed = ((unsuppressed / (suppressed + unsuppressed)) * 100);
                   // percentage_suppressed = ((suppressed / (suppressed + unsuppressed)) * 100);
                    //document.getElementById("lblunsuppressed").innerHTML = "<span class='badge'> " + percentage_unsuppressed + "%" + " </span>";
                    //document.getElementById("lblsuppressed").innerHTML = "<span class='badge'> " + percentage_suppressed + "%" + " </span>";
                   
         

                }

            });
            //.suppression

           });
        });

       
    </script>
  
</asp:Content>
