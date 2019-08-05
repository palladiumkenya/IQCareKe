<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientLinelist.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientLinelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="linelistwrap">
        <asp:Button ID="btnExcel" CssClass="btn btn-warning" runat="server" Font-Bold="true" OnClick="btnExcel_Click" Text="Export to Excel" />
        <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="PatientSearch" style="border: 1px solid #808080;overflow-x:scroll;">
            <table id="dtlPatientLineList" class="display" style="cursor: pointer;width: 100%;">
                <thead>
                    <tr>
                        <th style="width: 10px; display: none;">#</th>
                        <th>CCC Number</th>
                        <th>First Name</th>
                        <th>Middle Name</th>
                        <th>Last Name</th>
                        <th>Phone</th>
                        <th>Dispense Date</th>
                        <th>Drug</th>
                        <th>Expected Return</th>
                        <th>Due</th>
                        <th>Traced</th>
                        <th>Patientid</th>
                        <th>Person Id</th>
                    </tr>
                </thead>
                <tbody></tbody>
                <tfoot>
                    <tr>
                        <th style="width: 10px; display: none;">#</th>
                        <th>CCC Number</th>
                        <th>First Name</th>
                        <th>Middle Name</th>
                        <th>Last Name</th>
                        <th>Phone</th>
                        <th>Dispense Date</th>
                        <th>Drug</th>
                        <th>Expected Return</th>
                        <th>Due</th>
                        <th>Traced</th>
                        <th>Patientid</th>
                        <th>Person Id</th> 
                    </tr>
                </tfoot>

            </table>
        </div>
    </div>
    <script>
        var qtype = GetURLParameter('q');
        var selectr = selectResults();
        function selectResults() {
            if (qtype == "txcurr") {
                gettxcurr();
            }
            else if (qtype == "firstdefaulters") {
                getfirstdefaulters();
            }
            else if (qtype == "seconddefaulters") {
                getseconddefaulters();
            }
            else if (qtype == "ltfu") {
                getltfu();
            }
            else {

            }
        }
        
        //var txcurr = gettxcurr();
        //txcurr
        function gettxcurr() {
            var qto = GetURLParameter('qto');
            $.ajax({
                type: "POST",
                url: "../WebService/ReportingService.asmx/gettxcurrlinelist",
                data: "{'reportingdate':'" + qto + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    advancedDataTable(response, "dtlPatientLineList");
                }
            });
        }

        //firstdeafulters
        function getfirstdefaulters() {
            var qto = GetURLParameter('qto');
            $.ajax({
                type: "POST",
                url: "../WebService/ReportingService.asmx/getfirstdefaultersll",
                data: "{'reportingdate':'" + qto + "','mindays':'1','maxdays':'30'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    advancedDataTable(response, "dtlPatientLineList");
                }
            });
        }

        //second defaulters
        function getseconddefaulters() {
            var qto = GetURLParameter('qto');
            $.ajax({
                type: "POST",
                url: "../WebService/ReportingService.asmx/getseconddefaultersll",
                data: "{'reportingdate':'" + qto + "','mindays':'31','maxdays':'90'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    advancedDataTable(response, "dtlPatientLineList");
                }
            });
        }

        //ltfu
        function getltfu() {
            var qfrom = GetURLParameter('qfrom');
            var qto = GetURLParameter('qto');
            $.ajax({
                type: "POST",
                url: "../WebService/ReportingService.asmx/getltfull",
                data: "{'fromdate':'" + qfrom + "','todate':'" + qto + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    advancedDataTable(response, "dtlPatientLineList");
                }
            });
        }

        function advancedDataTable(data, table) {
            // alert("Data from the db");
            $('#' + table).DataTable({
                data: data.d,
                destroy: true,
                paging: true,
                searching: true,
                info: false,
                ordering: true,
                columnDefs: [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ]
            });
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

        //row selection
        $('#dtlPatientLineList').on('click', 'tbody tr', function () {
            var patientId = $(this).find('td').eq(10).text();
            var personId = $(this).find('td').eq(11).text();
            if ((qtype == "firstdefaulters") || (qtype == "seconddefaulters") || (qtype == "ltfu")) {
                setSession(patientId, personId);
            }
        });

        function setSession(patientId, personId) {
            $.ajax({
                type: "POST",
                url: "PatientLinelist.aspx/SetSelectedPatient", //Pagename/Functionname
                contentType: "application/json;charset=utf-8",
                data: "{'patientId':'" + patientId + "','personId':'" + personId + "'}",//data
                dataType: "json",
                success: function (data) {
                    if (data.d == "success") {
                        setTimeout(function () { window.location.href = "../patient/patientHome.aspx" }, 500);
                    }
                },
                error: function (result) {

                    alert("error");

                }
            });
        }
    </script>
</asp:Content>
