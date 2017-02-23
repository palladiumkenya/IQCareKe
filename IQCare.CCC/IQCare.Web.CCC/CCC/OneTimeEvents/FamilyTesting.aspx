<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="FamilyTesting.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.FamilyTesting" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">


        <uc:PatientDetails ID="PatientSummary" runat="server" />




        <div class="col-md-12 bs-callout bs-callout-info">
            <div class="col-md-12">
                <div class="col-md-12">

                    <div class="col-md-12">
                        <small>Enter Family Testing information</small><hr />
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Name(s)</label>
                            </div>
                            <div class="col-md-6">
                                <input id="Name" class="form-control input-sm" type="text" runat="server" placeholder="Family Names" />
                            </div>
                        </div>



                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Baseline HIV Status</label>
                            </div>
                            <div class="col-md-6">
                                <select runat="server" id="BaselineHIVStatus" class="form-control input-sm"></select>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">HIV Testing Results</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="hivtestingresult" ClientIDMode="Static" CssClass="form-control input-sm" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">CCC Referal</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="CccReferal" ClientIDMode="Static" CssClass="form-control input-sm" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Relationship</label>
                            </div>
                            <div class="col-md-6">
                                <select runat="server" id="Relationship" class="form-control input-sm"></select>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Baseline HIV Status Date</label>
                            </div>
                            <div class="col-md-6">
                                <div class="datepicker fuelux form-group" id="BaselineHIVStatusD">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="BaselineHIVStatusDate"></asp:TextBox>
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

                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">HIV Testing Date</label>
                            </div>
                            <div class="col-md-6">
                                <div class="datepicker fuelux form-group" id="TestingDate">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="HIVTestingDate"></asp:TextBox>
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
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12">
                    <asp:LinkButton runat="server" ID="btnAdd" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Member</asp:LinkButton>
                </div>

                <div class="col-md-12">
                    <hr />
                </div>

                <div class="col-md-12">
                    <div class="col-md-12 form-group">
                        <div class="col-md-12 bg-primary"><span class="pull-left"></span>Family Members </div>
                        <table class="table table-hover" id="tblFamilyTesting" clientidmode="Static" runat="server">
                            <thead>
                                <tr>
                                    <th class="text-primary">#</th>
                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Name(s)</i> </th>
                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Relationship</i> </th>
                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Baserline HIV Status</i> </th>
                                    <th><i class="fa fa-calendar-check-o text-primary" aria-hidden="true">Baseline HIV Status Date</i> </th>
                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">HIV Testing Results</i> </th>
                                    <th><i class="fa fa-calendar-check-o text-primary" aria-hidden="true">HIV Testing Results Date</i> </th>
                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">CCC Referal</i></th>
                                    <th><span class="fa fa-times text-danger text-primary pull-right"> Action</span></th>
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
                        <asp:LinkButton runat="server" ID="btnOneTimeEventsTracker" CssClass=" btn btn-info btn-lg fa fa-arrow-circle-o-right"> Save Family Testing</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnReloadHistory" CssClass=" btn btn-warning fa fa-refresh btn-lg"> Reset Family Form</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnClose" CssClass=" btn btn-danger fa fa-times btn-lg"> Close Family Form</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <%-- .col-md-12--%>
    </div>
    <%--.container-fluid--%>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#BaselineHIVStatusD').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#TestingDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#btnAdd").click(function (e) {
                var name = $("#<%=Name.ClientID%>").val();
                var relationship = $("#<%=Relationship.ClientID%>").val();
                var baselineHivStatus = $("#<%=BaselineHIVStatus.ClientID%>").val();
                var baselineHivStatusDate = $("#<%=BaselineHIVStatusDate.ClientID%>").val();
                var hivTestingResults = $("#<%=hivtestingresult.ClientID%>").val();
                var hivTestingResultsDate = $("#<%=HIVTestingDate.ClientID%>").val();
                var cccreferal = $("#<%=CccReferal.ClientID%>").val();

                //identifierList.push("" + baselineHivStatusDate + "");
                //enrollmentNoList.push("" + hivTestingResults + "");

                var tr = "<tr><td align='left'></td><td align='left'>" + name + "</td><td align='left'>" + relationship + "</td><td align='left'>" + baselineHivStatus + "</td><td align='left'>" + moment(baselineHivStatusDate).format('DD-MMM-YYYY') + "</td><td align='left'>" + hivTestingResults + "</td><td align='left'>" + hivTestingResultsDate + "</td><td align='left'>" + cccreferal + "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                $("#tblEnrollment>tbody:first").append('' + tr + '');

                resetElements();

                e.preventDefault();
            });
            
            function resetElements(parameters) {
                $("#Name").val("");
                $("#Relationship").val("");
                $("#Relationship").val("");
                $("#BaselineHIVStatus").val("");
                $("#BaselineHIVStatusDate").val("");
                $("#hivtestingresult").val("");
                $("#HIVTestingDate").val("");
                $("#CccReferal").val("");
            }
                
            $("#btnClose").click(function () {
                window.location.href = '/CCC/Home.aspx';
            });
        });

    </script>

</asp:Content>
