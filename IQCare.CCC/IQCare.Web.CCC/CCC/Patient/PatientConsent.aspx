<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientConsent.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientConsent" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <uc:PatientDetails ID="PatientSummary" runat="server" />

        <div class="col-md-12 bs-callout bs-callout-info" id="PatientConsentDetails">
            <div class="col-md-12" id="PatientConsentForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Consent Type</label>
                            </div>
                            <div class="col-md-12">
                                <select runat="server" id="ConsentType" class="form-control input-sm" required="true" clientidmode="Static"></select>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Date Consent</label>
                        </div>
                        <div class="datepicker fuelux form-group" id="DateofConsent">
                            <div class="input-group">
                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="ConsentDate"></asp:TextBox>
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
                <div class="col-md-12">
                    <asp:LinkButton runat="server" ID="btnAdd" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Detail</asp:LinkButton>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>

                <div class="col-md-12">
                    <div class="col-md-12 form-group">
                        <div class="col-md-12 bg-primary"><span class="pull-left"></span>Consent Details </div>
                        <table class="table table-hover" id="tblPatientConsent" clientidmode="Static" runat="server">
                            <thead>
                                <tr>
                                    <th class="text-primary">#</th>
                                    <th><span class="text-primary" aria-hidden="true">Consent Type</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">Consent Date</span> </th>
                                    <th><span class="text-danger text-primary pull-right">Action</span></th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-md-10"></div>
                </div>
            </div>

            <div class="col-md-12">
                <hr />
                <div class="col-md-7"></div>

                <div class="col-md-5">

                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnSave" CssClass=" btn btn-info btn-lg fa fa-arrow-circle-o-right" ClientIDMode="Static" OnClientClick="return false;"> Save</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnReset" CssClass=" btn btn-warning fa fa-refresh btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Reset </asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnClose" CssClass=" btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 bs-callout bs-callout-info" id="ViewPatientConsentDetails">
            <table class="table table-hover" id="tablePatientConsent" clientidmode="Static" runat="server">
                <thead>
                    <tr class="active">
                        <th class="text-primary">#</th>
                        <th><span class="text-primary" aria-hidden="true">Consent Type</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Consent Date</span> </th>
                        <%--<th><span class="text-danger text-primary pull-right">Action</span></th>--%>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="col-md-12">
                <asp:LinkButton runat="server" ID="PatientConsentAdd" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Consent</asp:LinkButton>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#PatientConsentDetails").hide();
            LoadPatientConsent();
            var patientConsent = [];
            $('#DateofConsent').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#btnAdd").click(function (e) {
                if ($('#PatientConsentForm').parsley().validate()) {
                    var consentTypeId = $("#<%=ConsentType.ClientID%>").val();
                    var consentType = $("#ConsentType :selected").text();
                    var consentDate = $("#<%=ConsentDate.ClientID%>").val();
                        var table = "<tr><td align='left'></td><td align='left'>" +
                            consentType +
                            "</td><td align='left'>" +
                            consentDate +
                            "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                        
                        $("#tblPatientConsent>tbody:first").append('' + table + '');
                        var consent = {
                            consentType: consentTypeId,
                            consentDate: consentDate
                        };
                        patientConsent.push(consent);
                        resetElements();
                        e.preventDefault();
                } else {
                    return false;
                }
                
            });

            $("#btnSave").click(function () {
                if (patientConsent.length < 1) {
                    toastr.error("error", "Please insert at least One(1) family member");
                    return false;
                }
                for (var i = 0, len = patientConsent.length; i < len; i++) {
                    addPatientConsent(patientConsent[i]);
                }
                
            });

            $("#btnClose").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
                
            });

            $("#tblPatientConsent").on('click', '.btnDelete', function () {
                $(this).closest('tr').remove();
            });

            $("#btnReset").click(function () {
                resetElements();
            });

            $("#PatientConsentAdd").click(function () {
                $("#ViewPatientConsentDetails").hide();
                $("#PatientConsentDetails").show();
                resetElements();
            });

        });

        function addPatientConsent(consent) {
            var consentType = consent.consentType;
            var consentDate = consent.consentDate;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            debugger;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientConsent",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','consentType': '" + consentType + "','consentDate': '" + consentDate +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Patient consent saved successfully");
                    //delay to show success message before redirect
                    setTimeout(function() { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                        },
                        error: function (response) {
                            toastr.error(response.d, "Patient consent not saved");
                        }
                    });
                }

                function LoadPatientConsent() {
                    var patientId ="<%=PatientId%>";
                    jQuery.support.cors = true;
                    $.ajax(
                    {
                        type: "POST",
                        url: "../WebService/PatientService.asmx/GetpatientConsent",
                        data: "{'patientId':'" + patientId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: false,
                        success: function (response) {
                            var itemList = response.d;
                            var table = '';
                            itemList.forEach(function (item, i) {
                                var n = i + 1;
                                table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + item.ConsentType + '</td><td style="text-align: left">' + moment(item.ConsentDate).format('DD-MMM-YYYY') + '</td></tr>';
                            });
                   
                            $('#tablePatientConsent').append(table);

                        },

                        error: function (msg) {
                            alert(msg.responseText);
                        }
                    });
                }

                function resetElements(parameters) {
                    $(".input-sm").val("");
                }

    </script>

</asp:Content>
