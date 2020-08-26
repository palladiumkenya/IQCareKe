<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.CCC.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style type="text/css">
        .reports-date-row{display: table;width: 100%;margin-bottom: 15px;}
        .from-reports-date-row, .to-reports-date-row{display: table-cell;}
        .table-report-results{display: table;width: 95%;margin: 20px;margin: 0px auto;}
        .txcurr-col, .reporting-col, .defaults30-col, .defaults90-col, ltfu-col{display: table-cell;}
        .txcurr-col, .defaults30-col, .defaults90-col, ltfu-col{width: 22%;}
        .reporting-col{width: 4%;}
        .reporting-head{background: #5bc0de;color: #fff;}
    </style>
    <div class="col-md-12 col-xs-12 col-sm-12">
        <div class="col-md-8 col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-body">                       
                        <div class="col-md-12">
                            <h5>More Reports</h5>
                           <div class="reports-date-row">
                               <div class="from-reports-date-row">
                                    <div class="col-md-12">
                                        <div class='input-group date' id='reportingfromdate'>
                                            <span class="input-group-addon">
                                                FROM <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                            <asp:TextBox runat="server"  CssClass="form-control input-sm" ID="fromreportingdateinput" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" data-parsley-min-message="Input the appointment date"></asp:TextBox>
                                        </div>
                                    </div>
                               </div>
                               <div class="to-reports-date-row">
                                   <div class="col-md-12">
                                        <div class='input-group date' id='reportingtodate'>
                                            <span class="input-group-addon">
                                                TO <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                            <asp:TextBox runat="server"  CssClass="form-control input-sm" ID="toreportingdateinput" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" data-parsley-min-message="Input the appointment date"></asp:TextBox>
                                        </div>
                                    </div>
                               </div>
                           </div>
                            <div class="table-report-results">
                                <div class="txcurr-col">
                                    <div class="txcurr-head reporting-head">
                                        TX Curr (30 Days)
                                    </div>
                                    <div class="txcurr-body reporting-body">
                                        <a href="Patient/PatientLinelist.aspx" id="txcurrlink"><i class="fa fa-users" aria-hidden="true"></i> <span id="txcurrspan">Loading...</span></a>
                                    </div>
                                </div>
                                <div class="reporting-col"></div>
                                <div class="defaults30-col">
                                    <div class="defaults30-head reporting-head">
                                        Defaulters (1 - 30)
                                    </div>
                                    <div class="defaults30-body reporting-body">
                                        <a href="Patient/PatientLinelist.aspx" id="firstdefaulterslink"><i class="fa fa-users" aria-hidden="true"></i> <span id="firststagedef">Loading...</span></a>
                                    </div>
                                </div>
                                <div class="reporting-col"></div>
                                <div class="defaults90-col">
                                    <div class="defaults90-head reporting-head">
                                        LTFU (31 - 90)
                                    </div>
                                    <div class="defaults90-body reporting-body">
                                        <a href="Patient/PatientLinelist.aspx" id="seconddefaulterslink"><i class="fa fa-users" aria-hidden="true"></i> <span id="secondstagedef">Loading...</span></a>
                                    </div>
                                </div>
                                <div class="reporting-col"></div>
                                <div class="ltfu-col">
                                    <div class="ltfu-head reporting-head">
                                        LTFU (Above 90)
                                    </div>
                                    <div class="ltfu-body reporting-body">
                                        <a href="Patient/PatientLinelist.aspx" id="ltfulink"><i class="fa fa-users" aria-hidden="true"></i> <span id="ltfu">Loading...</span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
           
                    </div> <!-- .panel-body -->
                </div>

        </div><!-- .col-md-8 -->
    </div><!-- .col-md-12 -->
    <script type="text/javascript">
        //$( ".reporting-head" ).click(function() {
        //  $('.disablepage').show();
        //});
         var facilityId = '<%=AppLocationId%>';

        function GenExcel(category) {
            $.ajax({
                url: 'WebService/PatientSummaryService.asmx/GenerateExcel',
                data: "{'category':'" + category + "'}",
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    //pending = response.d;
                    //document.getElementById("pendingVL").innerHTML = "<span class='badge'>" + response.d + "</span>";

                }

            });
        }

        $(document).ready(function () {   
        $("#reportingfromdate").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: true,
            defaultDate: new Date()
        }).on("dp.change", function (selectedDate) {
            document.getElementById("ltfu").innerHTML = "Loading...";
            var selectedday = $("#<%=toreportingdateinput.ClientID%>").val();
            var fromselectedday = $("#<%=fromreportingdateinput.ClientID%>").val();
            getltfu(fromselectedday, selectedday);
        });

        $("#reportingtodate").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: true,
           defaultDate: new Date()
        }).on("dp.change", function (selectedDate) {
            //var selectedday = moment();
            //alert(selectedday);
            document.getElementById("txcurrspan").innerHTML = "Loading...";
            document.getElementById("firststagedef").innerHTML = "Loading...";
            document.getElementById("secondstagedef").innerHTML = "Loading...";
            document.getElementById("ltfu").innerHTML = "Loading...";
            var selectedday = $("#<%=toreportingdateinput.ClientID%>").val();
            var fromselectedday = $("#<%=fromreportingdateinput.ClientID%>").val();
            gettxcurr(fromselectedday, selectedday);
            //getfirststagedefaulters(selectedday);
            //getsecondstagedefaulters(selectedday);
            //getltfu(fromselectedday, selectedday);
        });

            var today = new Date();    
            var month = today.getMonth()+1;
            var day = today.getDate();
            var reportingdate = today.getFullYear() + '/' +
                        ((''+month).length<2 ? '0' : '') + month + '/' +
                (('' + day).length < 2 ? '0' : '') + day;
           // var gettodaydata = getTodayData(reportingdate);
            var txcurrcount = gettxcurr("1900-01-01", reportingdate);
            //var firststagedefaulters = getfirststagedefaulters(reportingdate);
            //var secondstagedefaulters = getsecondstagedefaulters(reportingdate);
            //var ltfu = getltfu(reportingdate, reportingdate);

            //function getTodayData(reportingdate) {

            //}

            //gettxcurr
            function gettxcurr(fromselectedday, reportingdate) { 
                $.ajax({
                    url: 'WebService/ReportingService.asmx/getNumberOfTxcurr',
                    data: "{'reportingdate':'" + reportingdate + "'}",
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function (response) {
                        document.getElementById("txcurrspan").innerHTML = response.d;
                        if (response.d > 0) {
                            $("#txcurrlink").attr("href", "Patient/PatientLinelist.aspx?q=txcurr&qfrom=" + fromselectedday + "&qto=" + reportingdate + "");
                        }
                        else {
                            $("#txcurrlink").attr("href", "#");
                        }   
                        if (response.d != null) {
                            getfirststagedefaulters(fromselectedday, reportingdate);
                        }
                    }
                });
            }

            //get 1- 30 defaulters
            function getfirststagedefaulters(fromselectedday, reportingdate) {
                $.ajax({
                    url: 'WebService/ReportingService.asmx/getFirstStageDefaulters',
                    data: "{'reportingdate':'" + reportingdate + "','mindays':'1','maxdays':'30'}",
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function (response) {
                        document.getElementById("firststagedef").innerHTML = response.d;
                        if (response.d > 0) {
                            $("#firstdefaulterslink").attr("href", "Patient/PatientLinelist.aspx?q=firstdefaulters&qfrom=" + fromselectedday + "&qto=" + reportingdate + "");
                        }
                        else {
                            $("#firstdefaulterslink").attr("href", "#");
                        } 
                        if (response.d != null) {
                            getsecondstagedefaulters(fromselectedday, reportingdate);
                        }
                    }
                });
            }

            //get 31  - 90 defaulters
            function getsecondstagedefaulters(fromselectedday, reportingdate) {
                $.ajax({
                    url: 'WebService/ReportingService.asmx/getSecondStageDefaulters',
                    data: "{'reportingdate':'" + reportingdate + "','mindays':'31','maxdays':'90'}",
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function (response) {
                        document.getElementById("secondstagedef").innerHTML = response.d;
                        if (response.d > 0) {
                            $("#seconddefaulterslink").attr("href", "Patient/PatientLinelist.aspx?q=seconddefaulters&qfrom=" + fromselectedday + "&qto=" + reportingdate + "");
                        }
                        else {
                            $("#seconddefaulterslink").attr("href", "#");
                        } 
                        if (response.d != null) {
                            getltfu(fromselectedday, reportingdate);
                        }
                    }
                });
            }

            //LTFU
            function getltfu(fromdate, todate) {
                $.ajax({
                    url: 'WebService/ReportingService.asmx/getltfu',
                    data: "{'fromdate':'" + fromdate + "','todate':'" + todate + "'}",
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function (response) {
                        document.getElementById("ltfu").innerHTML = response.d;
                        if (response.d > 0) {
                            $("#ltfulink").attr("href", "Patient/PatientLinelist.aspx?q=ltfu&qfrom=" + fromdate + "&qto=" + todate + "");
                        }
                        else {
                            $("#ltfulink").attr("href", "#");
                        } 
                    }
                });
            }
        });      
    </script>
  
</asp:Content>
