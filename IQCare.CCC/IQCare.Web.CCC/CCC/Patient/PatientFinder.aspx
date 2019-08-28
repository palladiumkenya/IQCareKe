<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientFinder.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientFinder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12 col-xs-12 col-sm-12 ">
        <div class="col-md-12 col-xs-12 col-sm-12 bs-callout bs-callout-info" id="searchGrid">
            <div class="col-md-12"><small class="pull-left"><strong><i class="fa fa-search fa-2x" aria-hidden="true">Find Patient </i></strong></small></div>
            <div class="col-md-12">
                <hr />
            </div>
            <div class="col-md-12">
                <div class="col-md-3">
                    <div class="col-md-12">
                        <label class="control-label pull-left">Identification Number</label></div>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="PatientNumber" CssClass="form-control input-sm" placeholder="ccc number..." ClientIDMode="Static"></asp:TextBox>
                    </div>

                </div>

                <div class="col-md-3">
                    <div class="col-md-12">
                        <label class="control-label pull-left">First Name</label></div>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="FirstName" CssClass="form-control input-sm" placeholder="first name.." ClientIDMode="Static"></asp:TextBox>

                    </div>

                </div>

                <div class="col-md-3">
                    <div class="col-md-12">
                        <label class="control-label pull-left">Middle Name</label></div>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="MiddleName" CssClass="form-control input-sm" placeholder="middle name.." ClientIDMode="Static"></asp:TextBox>

                    </div>

                </div>

                <div class="col-md-3">
                    <div class="col-md-12">
                        <label class="control-label pull-left">Last Name</label></div>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="LastName" CssClass="form-control mdb-select input-sm" placeholder="Last name.." ClientIDMode="Static"></asp:TextBox>

                    </div>

                </div>

            </div>

            <div class="col-md-12">
                <div class="col-md-3">
                    <div class="col-md-12">
                        <label class="control-label pull-left">Facility</label></div>
                    <div class="col-md-12 md-form">
                        <asp:DropDownList runat="server" ID="Facility" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                </div>

                <div class="col-md-3">
                </div>
                <div class="col-md-1"></div>
                <div class="col-md-2">
                    <div class="col-md-12 col-xs-12 col-sm-12">
                        <label><span class="fa fa-search-plus"></span> Duplicate finder</label>
                    </div>
                    <div class="col-md-12 col-xs-12 col-sm-12" >
                        <a id="btnFindDuplicate" class="btn btn-success btn-lg  btn-block  fa fa-object-ungroup" href="PatientDuplicateFinder.aspx"> Find Duplicates</a>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <hr />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <div class="col-md-12 pull-left" id="divAction" clientidmode="Static">
                        <i class="fa fa-spinner fa-pulse fa-3x fa-fw pull-left text-info"></i>
                        <span class="sr-only"></span><strong class="pull-left" id="divActionString" clientidmode="Static">Fetching Patient data</strong>
                    </div>
                </div>
                <%--<div class="col-md-4 col-xs-4 col-sm-4"></div> --%>
                <div class="col-md-8 col-xs-12 col-sm-12">
                    <div class="col-md-4 col-xs-12 col-sm-4">
                        <asp:LinkButton runat="server" ID="btnFindPatient" OnClientClick="return false" ClientIDMode="Static" CssClass="btn btn-info btn-lg  btn-block  fa fa-search"> Find Patient</asp:LinkButton>
                    </div>
                    <div class="col-md-4 col-xs-12 col-sm-4">
                        <asp:LinkButton runat="server" ID="btnReset" OnClientClick="return false" ClientIDMode="Static" CssClass="btn btn-warning btn-block btn-lg  fa fa-refresh"> Reset Find</asp:LinkButton>

                    </div>
                    <div class="col-md-4 col-xs-12 col-sm-4">
                        <asp:LinkButton runat="server" ClientIDMode="Static" OnClientClick="return false" ID="btnClose" CssClass="btn btn-danger btn-block btn-lg fa fa-times"> Close Find </asp:LinkButton>
                    </div>


                </div>
            </div>
        </div>

        <div class="col-md-12 col-xs-12 col-sm-12 bs-callout bs-callout-info" id="infoGrid">
            <div class="col-md-6">
                <label class="control-label pull-left text-warning fa fa-search-plus">Patient Search Results </label>
            </div>
            <div class="col-md-6 pull-right">
                <button id="btnRemoveGrid" class="btn btn-warning btn-lg btn-sm pull-right fa fa-arrow-circle-o-left" onclick="return false">Back to Search</button>
            </div>
        </div>

        <div class="col-md-12 col-xs-12 col-sm-12" style="padding: 5px; text-align: left;" id="infoGridMessage">
            <%--<strong><h4>Double Click To Select Patient</h4></strong>--%>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="PatientSearch">
            <table id="tblFindPatient" class="display" style="cursor: pointer" width="100%">
                <thead>
                    <tr>
                        <th style="width: 10px; display: none;">PatientId</th>
                        <th>CCC Number</th>
                        <th>First Name</th>
                        <th>Middle Name</th>
                        <th>Last Name</th>
                        <th>Date Of Birth</th>
                        <th>Sex</th>
                        <th>Enrollment Date</th>
                        <th>PatientStatus</th>
                        <th style="width: 10px;">PersonId</th>
                    </tr>
                </thead>
                <tbody></tbody>
                <tfoot>
                    <tr>
                        <th style="width: 10px; display: none;">PatientId</th>
                        <th>CCC Number</th>
                        <th>First Name</th>
                        <th>Middle Name</th>
                        <th>Last Name</th>
                        <th>Date Of Birth</th>
                        <th>Sex</th>
                        <th>Enrollment Date</th>
                        <th>PatientStatus</th>
                        <th style="width: 10px;">PersonId</th>

                    </tr>
                </tfoot>

            </table>
        </div>
        <style type="text/css">
            .sorting_1 {
                display: none;
            }
        </style>
    </div>
    <%--.col-md-12--%>

    <script type="text/javascript">
        $(document).ready(function () {

            $.ajaxSetup({
                cache: false
            });

            var isEnrolled = "enrolledClients";

            $("#lblNotEnrolled").on('checked.fu.checkbox',
                function () {
                    isEnrolled = 'notEnrolledClients';
                    // alert(isEnrolled);
                });
            $("#lblNotEnrolled").on('unchecked.fu.checkbox',
                function () {
                    isEnrolled = 'enrolledClients';
                    //alert(isEnrolled);
                });


            $("#divAction").hide("fast");

            $("#btnRemoveGrid").click(function () {
                $("#infoGrid").slideUp("fast",
                    function () {
                        $("#PatientSearch").slideUp("fast",
                            function () {
                                $("#searchGrid").slideDown("fast");
                            });
                    });
                $("#infoGridMessage").slideUp("fast");
            });

            $("#btnClose").click(function () {
                window.location.href ="<%=ResolveClientUrl("~/CCC/Home.aspx")%>";
            });

            $("#btnReset").click(function () {

                $("#<%=PatientNumber.ClientID%>").val('');
                $("#<%=FirstName.ClientID%>").val('');
                $("#<%=MiddleName.ClientID%>").val('');
                $("#<%=LastName.ClientID%>").val('');
                $("#<%=Facility.ClientID%>").val('');
            });

            $("#PatientSearch").hide();
            $("#infoGrid").hide();
            $("#infoGridMessage").hide();

            //$("#SearchDoB")
            //    .datepicker({ allowPastDates: true, momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' } });
            //$("#RegDate").datepicker('setDate','',{ allowPastDates: true, momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' } });

            /*--change cursor to hand during selection*/
            // $("#tblFindPatient tr").css('cursor', 'hand');

            var table = null;
            $("#btnFindPatient").click(function (e) {

                $("#divAction").show("fast");
                $("#divActionString").text("Consolidating table and data features..");

                table = $("#tblFindPatient").dataTable({
                    "oLanguage": {
                        "sZeroRecords": "No records to display",
                        "sSearch": "Search from all Records"
                    },
                    "bProcessing": true,
                    "bServerSide": true,
                    "ordering": true,
                    "searching": true,
                    "info": true,
                    "bDestroy": true,
                    "sAjaxSource": "../WebService/PatientLookupService.asmx/GetPatientSearchx",
                    "sPaginationType": "full_numbers",
                    "bDeferRender": true,
                    "responsive": true,
                    "sPaginate": true,
                    "lengthMenu": [[10, 20, 50], [10, 20, 50]],
                    "aoColumns":
                        [
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null
                        ],
                    "fnServerData": function (sSource, aoData, fnCallback) {
                        aoData.push({ "name": "patientId", "value": "" + $("#<%=PatientNumber.ClientID%>").val() + "" });
                        aoData.push({ "name": "firstName", "value": "" + $("#<%=FirstName.ClientID%>").val() + "" });
                        aoData.push({ "name": "middleName", "value": "" + $("#<%=MiddleName.ClientID%>").val() + "" });
                        aoData.push({ "name": "lastName", "value": "" + $("#<%=LastName.ClientID%>").val() + "" });
                        aoData.push({ "name": "facility", "value": "" + $("#<%=Facility.ClientID%>").find(":selected").val() + "" });
                        aoData.push({ "name": "isEnrolled", "value": "" + isEnrolled + "" });

                        $("#divActionString").text("Data features and table preparation complete");
                        var arrayReturn = [];


                        $.ajax({
                            "dataType": 'json',
                            "contentType": "application/json; charset=utf-8",
                            "type": "POST",
                            "url": sSource,
                            "data": JSON.stringify({ dataPayLoad: aoData }),
                            "success": function (msg) {
                                $("#divActionString").text("Rendering patients data...");
                                var json = jQuery.parseJSON(msg.d);
                                if (json === null) {
                                    var data = [];
                                    arrayReturn["data"] = data;
                                    arrayReturn["draw"] = 1;
                                    arrayReturn["recordsTotal"] = 0;
                                    arrayReturn["recordsFiltered"] = 0;

                                    fnCallback(arrayReturn);
                                    $("#PatientSearch").slideDown("fast", function () { $("#divAction").hide("fast"); $("#infoGrid").slideDown("fast", function () { $("#searchGrid").slideUp("fast") }); $("#infoGridMessage").slideDown("fast"); });
                                } else {
                                    fnCallback(json);
                                    $("#PatientSearch").slideDown("fast", function () { $("#divAction").hide("fast"); $("#infoGrid").slideDown("fast", function () { $("#searchGrid").slideUp("fast") }); $("#infoGridMessage").slideDown("fast"); });
                                }
                            },
                            "error": function (xhr, errorType, exception) {
                                $("#divAction").show("fast");

                                var jsonError = jQuery.parseJSON(xhr.responseText);
                                $("#divAction").text(jsonError.Message);
                                toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                                return false;
                            }
                        });
                    }
                });

            });

            //row selection
            $('#tblFindPatient').on('click', 'tbody tr', function () {
                // window.location.href = $(this).attr('href');
                var patientId = $(this).find('td').eq(0).text();
                var patientStatus = $(this).find('td').eq(8).text();
                var personId = $(this).find('td').eq(9).text();

                if (patientStatus === 'Not Enrolled') {
                    // alert("personId:" + patientId + " " + "Patient Status :" + patientStatus);
                    RedirectToRegistrationEdit(personId, isEnrolled);
                } else {
                    setSession(patientId, personId);
                }


            });

        });

        function setSession(patientId, personId) {
            //console.log(patientId);

            $.ajax({
                type: "POST",
                url: "PatientFinder.aspx/SetSelectedPatient", //Pagename/Functionname
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

        function RedirectToRegistrationEdit(personId, isEnrolled) {

            $.ajax({
                type: "POST",
                url: "patientRegistration.aspx/RedirectToRegistrationEdit", //Pagename/Functionname
                contentType: "application/json;charset=utf-8",
                data: "{'personId':'" + personId + "','isEnrolled':'" + isEnrolled + "'}",//data
                dataType: "json",
                success: function (data) {
                    if (data.d === "success") {
                        setTimeout(function () { window.location.href = "./patientRegistration.aspx" }, 500);
                    }
                },
                error: function (result) {

                    alert("error");

                }
            });
        }
    </script>
</asp:Content>
