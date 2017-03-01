<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="FamilyTesting.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.FamilyTesting" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">


        <uc:PatientDetails ID="PatientSummary" runat="server" />
        <asp:LinkButton runat="server" ID="btn_open_modal" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg">View Patient Family Members</asp:LinkButton>



        <div class="col-md-12 bs-callout bs-callout-info">
            <div class="col-md-12" id="FamilyTestingForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12">

                    <div class="col-md-12">
                        <small>Person Details</small><hr />
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">First Name</label>
                            </div>
                            <div class="col-md-6">
                                <input id="FirstName" class="form-control input-sm" type="text" runat="server" placeholder="First Name" required="true" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Middle Name</label>
                            </div>
                            <div class="col-md-6">
                                <input id="MiddleName" class="form-control input-sm" type="text" runat="server" placeholder="Middle Name" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Last Name</label>
                            </div>
                            <div class="col-md-6">
                                <input id="LastName" class="form-control input-sm" type="text" runat="server" placeholder="Last Name" required="true" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Relationship</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="Relationship" ClientIDMode="Static" CssClass="form-control input-sm" required="true" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Sex</label>
                            </div>
                            <div class="col-md-6">
                                <select runat="server" id="Sex" class="form-control input-sm" required="true"></select>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Date of Birth</label>
                            </div>
                            <div class="col-md-6">
                                <div class="datepicker fuelux form-group" id="DateOfBirth">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="Dob"></asp:TextBox>
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

                    <div class="col-md-12">
                        <small>HIV Testing information</small><hr />
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Baseline HIV Status</label>
                            </div>
                            <div class="col-md-6">
                                <select runat="server" id="BaselineHIVStatus" class="form-control input-sm" required="true"></select>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">HIV Testing Results</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="hivtestingresult" ClientIDMode="Static" CssClass="form-control input-sm" required="true" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">CCC Referal</label>
                            </div>
                            <div class="col-md-6">
                                <%--<asp:DropDownList runat="server" ID="CccReferal" ClientIDMode="Static" CssClass="form-control input-sm" required="true"/>--%>
                                <asp:DropDownList ID="CccReferal" runat="server" AutoPostBack="False" CssClass="form-control input-sm" onChange="CccEnabled();">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
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
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="HIVTestingDate" required="true"></asp:TextBox>
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
                                <label class="control-label pull-left">CCC Number</label>
                            </div>
                            <div class="col-md-6">
                                <input id="cccNumber" class="form-control input-sm" type="text" runat="server" enabled="False" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12">
                    <asp:LinkButton runat="server" ID="btnAdd" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Member</asp:LinkButton>
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
                                    <th><span class="fa fa-times text-danger text-primary pull-right">Action</span></th>
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
                        <asp:LinkButton runat="server" ID="btnSave" CssClass=" btn btn-info btn-lg fa fa-arrow-circle-o-right" ClientIDMode="Static" OnClientClick="return false;"> Save Family Testing</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnReset" CssClass=" btn btn-warning fa fa-refresh btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Reset Family Form</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnClose" CssClass=" btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close Family Form</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <%-- .col-md-12--%>
    </div>
    <%--.container-fluid--%>

    <div class="modal fade bs-example-modal-lg" id="ViewFamilyModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-primary">Family Members</div>
                <table class="table table-hover" id="tableFamilymembers" clientidmode="Static" runat="server">
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
                            <th><span class="fa fa-times text-danger text-primary pull-right">Action</span></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var familyMembers = [];
            $('#BaselineHIVStatusD').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#TestingDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#DateOfBirth').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#btnAdd").click(function (e) {
                if ($('#FamilyTestingForm').parsley().validate()) {
                    var firstName = $("#<%=FirstName.ClientID%>").val();
                    var middleName = $("#<%=MiddleName.ClientID%>").val();
                    var lastName = $("#<%=LastName.ClientID%>").val();
                    var sex = $("#<%=Sex.ClientID%>").val();
                    var dob = $("#<%=Dob.ClientID%>").val();
                    var name = $("#<%=FirstName.ClientID%>").val() + ' ' + $("#<%=MiddleName.ClientID%>").val() + ' ' + $("#<%=LastName.ClientID%>").val();
                    var relationshipId = $("#<%=Relationship.ClientID%>").val();
                    var baselineHivStatusId = $("#<%=BaselineHIVStatus.ClientID%>").val();
                    var baselineHivStatusDate = $("#<%=BaselineHIVStatusDate.ClientID%>").val();
                    var hivTestingresultId = $("#<%=hivtestingresult.ClientID%>").val();
                    var hivTestingresultDate = $("#<%=HIVTestingDate.ClientID%>").val();
                    var cccreferal = $("#<%=CccReferal.ClientID%>").val();
                    var cccReferalNumber = $("#<%=cccNumber.ClientID%>").val();
                    var table = "<tr><td align='left'></td><td align='left'>" + name + "</td><td align='left'>" + relationshipId + "</td><td align='left'>" + baselineHivStatusId + "</td><td align='left'>" + moment(baselineHivStatusDate).format('DD-MMM-YYYY') + "</td><td align='left'>" + hivTestingresultId + "</td><td align='left'>" + hivTestingresultDate + "</td><td align='left'>" + cccreferal + "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                    $("#tblFamilyTesting>tbody:first").append('' + table + '');
                    var testing = { firstName: firstName, middleName: middleName, lastName: lastName, sex: sex, dob: dob,  relationshipId: relationshipId, baselineHivStatusId: baselineHivStatusId, baselineHivStatusDate: baselineHivStatusDate, hivTestingresultId: hivTestingresultId, hivTestingresultDate: hivTestingresultDate, cccreferal: cccreferal, cccReferalNumber: cccReferalNumber};                
                    familyMembers.push(testing);
                    resetElements();
                    e.preventDefault();
                } else {
                    return false;
                }
                
            });

            $("#btnSave").click(function () {
                if (familyMembers.length < 1) {
                    toastr.error("error", "Please insert at least One(1) family member");
                    return false;
                }
                for (var i = 0, len = familyMembers.length; i < len; i++) {
                    addFamilyTesting(familyMembers[i]);
                }
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
            });

            $("#btnClose").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
                
            });

            $("#tblFamilyTesting").on('click', '.btnDelete', function () {
                $(this).closest('tr').remove();
            });

            $("#btnReset").click(function () {
                resetElements();
            });

            $("#btn_open_modal").click(function(event) {
                $('#ViewFamilyModal').modal('show');
            });
        });

        function resetElements(parameters) {
            $(".input-sm").val("");
        }

        function addFamilyTesting(testing) {
            var firstName = testing.firstName;
            var middleName = testing.middleName;
            var lastName = testing.lastName;
            var sex = testing.sex;
            var dob = testing.dob;
            var relationshipId = testing.relationshipId;
            var baselineHivStatusId = testing.baselineHivStatusId;
            var baselineHivStatusDate = testing.baselineHivStatusDate;
            var hivTestingresultId = testing.hivTestingresultId;
            var hivTestingresultDate = testing.hivTestingresultDate;
            var cccreferal = testing.cccreferal;
            var cccReferalNumber = testing.cccReferalNumber;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientFamilyTesting",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','firstName': '" + firstName + "','middleName': '" + middleName + "','lastName': '" + lastName + "','sex': '" + sex + "','dob': '" + dob + "','relationshipId': '" + relationshipId + "','baselineHivStatusId': '" + baselineHivStatusId + "','baselineHivStatusDate': '" + baselineHivStatusDate + "','hivTestingresultId': '" + hivTestingresultId + "','hivTestingresultDate': '" + hivTestingresultDate + "','cccreferal': '" + cccreferal + "','cccReferalNumber': '" + cccReferalNumber +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Family testing saved successfully");
                },
                error: function (response) {
                    toastr.error(response.d, "Family testing not saved");
                }
            });
        }

        function CccEnabled() {
            if ($("#<%=CccReferal.ClientID%>").val() === "False") {
                $("#<%=cccNumber.ClientID%>").disabled = 'true';
            } else {
                $("#<%=cccNumber.ClientID%>").disabled = 'false';
            }
        }

    </script>

</asp:Content>
