<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="FamilyTesting.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.FamilyTesting" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">


        <uc:PatientDetails ID="PatientSummary" runat="server" />
        <%--<asp:LinkButton runat="server" ID="btn_open_modal" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg">View Patient Family Members</asp:LinkButton>--%>

        <div class="col-md-12 bs-callout bs-callout-info" id="FamilyTestingDetails">
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
                    <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Baseline HIV Status</label>
                            </div>
                            <div class="col-md-12">
                                <select runat="server" id="BaselineHIVStatus" class="form-control input-sm" required="true" clientidmode="Static" onchange="BaselineEnabled();"></select>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Baseline HIV Status Date</label>
                            </div>
                            <div class="col-md-12">
                                <div class="datepicker fuelux form-group" id="BaselineHIVStatusD">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="BaselineHIVStatusDate"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown" clientidmode="Static" id="btnBaselineHIVStatusDate">
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

                    <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">HIV Testing Results</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" ID="hivtestingresult" ClientIDMode="Static" CssClass="form-control input-sm" onChange="HivEnabled();CccEnabled();" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">HIV Testing Date</label>
                            </div>
                            <div class="col-md-12">
                                <div class="datepicker fuelux form-group" id="TestingDate">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="HIVTestingDate"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown" clientidmode="Static" id="btnHIVTestingDate">
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

                    <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">CCC Referal</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList ID="CccReferal" runat="server" AutoPostBack="False" CssClass="form-control input-sm" onChange="CccEnabled();" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">CCC Number</label>
                            </div>
                            <div class="col-md-12" id="cccnum">
                                <input id="cccNumber" class="form-control input-sm" type="text" runat="server" data-parsley-trigger="keyup" data-parsley-pattern-message="Please enter a valid CCC Number. Format ((XXXXX-XXXXX))" data-parsley-pattern="/^[0-9]{5}-[0-9]{5}$/" />
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
                                    <th><span class="text-primary" aria-hidden="true">Name</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">Relationship</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">Baserline HIV Status</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">Baseline HIV Status Date</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">HIV Testing Results</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">HIV Testing Results Date</span> </th>
                                    <th><span class="text-primary" aria-hidden="true">CCC Referal</span></th>
                                    <th><span class="text-primary pull-right">Action</span></th>
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

        <div class="col-md-12 bs-callout bs-callout-info" id="ViewFamilyTestingDetails">
            <table class="table table-hover" id="tableFamilymembers" clientidmode="Static" runat="server">
                <thead>
                    <tr class="active">
                        <th class="text-primary">#</th>
                        <th><span class="text-primary" aria-hidden="true">Name</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Relationship</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Baseline HIV Status</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Baseline HIV Status Date</span> </th>
                        <th><span class="text-primary" aria-hidden="true">HIV Testing Results</span> </th>
                        <th><span class="text-primary" aria-hidden="true">HIV Testing Results Date</span> </th>
                        <th><span class="text-primary" aria-hidden="true">CCC Referal</span></th>
                        <th><span class="text-primary pull-right">Action</span></th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="col-md-12">
                <asp:LinkButton runat="server" ID="FamilyAdd" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Member</asp:LinkButton>
            </div>
        </div>


        <%--Edit Modal--%>
        <div id="editFamilyTestingModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-info">
                        <h4 class="modal-title">Edit Family Testing</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
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
                                            <input id="fName" class="form-control input-sm" type="text" runat="server" placeholder="First Name" required="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Middle Name</label>
                                        </div>
                                        <div class="col-md-6">
                                            <input id="mName" class="form-control input-sm" type="text" runat="server" placeholder="Middle Name" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Last Name</label>
                                        </div>
                                        <div class="col-md-6">
                                            <input id="lName" class="form-control input-sm" type="text" runat="server" placeholder="Last Name" required="true" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Relationship</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList runat="server" ID="relationshipMod" ClientIDMode="Static" CssClass="form-control input-sm" required="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Sex</label>
                                        </div>
                                        <div class="col-md-6">
                                            <select runat="server" id="sexMod" class="form-control input-sm" required="true" clientidmode="Static"></select>
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Date of Birth</label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="datepicker fuelux form-group" id="DateOfBirthMod">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="dobMod"></asp:TextBox>
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
                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Baseline HIV Status</label>
                                        </div>
                                        <div class="col-md-12">
                                            <select runat="server" id="bHivStatusMod" class="form-control input-sm" required="true" clientidmode="Static" onchange="BaselineEnabledMod();"></select>
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Baseline HIV Status Date</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="datepicker fuelux form-group" id="BaselineHIVStatusDMod">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="bHivStatusDateMod"></asp:TextBox>
                                                    <div class="input-group-btn">
                                                        <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown" clientidmode="Static" id="btnBaselineHIVStatusDate">
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

                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">HIV Testing Results</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="testingStatusMod" ClientIDMode="Static" CssClass="form-control input-sm" onChange="HivEnabledMod();CccEnabledMod();" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">HIV Testing Date</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="datepicker fuelux form-group" id="TestingDateMod">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="testingStatusDateMod"></asp:TextBox>
                                                    <div class="input-group-btn">
                                                        <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown" clientidmode="Static" id="btnHIVTestingDate">
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

                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">CCC Referal</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="cccReferalMod" runat="server" AutoPostBack="False" CssClass="form-control input-sm" onChange="CccEnabledMod();" ClientIDMode="Static">
                                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">CCC Number</label>
                                        </div>
                                        <div class="col-md-12" id="cccnumMod">
                                            <input id="cccNumberMod" class="form-control input-sm" type="text" runat="server" data-parsley-trigger="keyup" data-parsley-pattern-message="Please enter a valid CCC Number. Format ((XXXXX-XXXXX))" data-parsley-pattern="/^[0-9]{5}-[0-9]{5}$/" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <button type="button" id="btnSaveFamilyTesting" class="btn btn-default" onclientclick="return false;">Save</button>
                            </div>
                            <div class="col-md-6">
                                <button type="button" id="btnCancelFamilyTesting" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var familyMembers = [];
            $('#BaselineHIVStatusD').datepicker({
                allowPastDates: true,
                date:0,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#TestingDate').datepicker({
                allowPastDates: true,
                date:0,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#DateOfBirth').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $('#BaselineHIVStatusDMod').datepicker({
                allowPastDates: true,
                date:0,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#TestingDateMod').datepicker({
                allowPastDates: true,
                date:0,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            $('#DateOfBirthMod').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#FamilyTestingDetails").hide();
            loadFamilyTesting();

            $("#btnAdd").click(function (e) {
                if ($('#FamilyTestingForm').parsley().validate()) {
                    var firstName = escape($("#<%=FirstName.ClientID%>").val());
                    var middleName = escape($("#<%=MiddleName.ClientID%>").val());
                    var lastName = escape($("#<%=LastName.ClientID%>").val());
                    var sex = $("#<%=Sex.ClientID%>").val();
                    var dob = $("#<%=Dob.ClientID%>").val();
                    var name = $("#<%=FirstName.ClientID%>").val() + ' ' + $("#<%=MiddleName.ClientID%>").val() + ' ' + $("#<%=LastName.ClientID%>").val();
                    var relationshipId = $("#<%=Relationship.ClientID%>").val();
                    var relationship = $("#Relationship :selected").text();
                    var baselineHivStatusId = $("#<%=BaselineHIVStatus.ClientID%>").val();
                    var baselineHivStatus = $("#BaselineHIVStatus :selected").text();
                    var baselineHivStatusDate = $("#<%=BaselineHIVStatusDate.ClientID%>").val();
                    var hivTestingresultId = $("#<%=hivtestingresult.ClientID%>").val();
                    var hivTestingresult = $("#hivtestingresult :selected").text();
                    var hivTestingresultDate = $("#<%=HIVTestingDate.ClientID%>").val();
                    var cccreferal = $("#<%=CccReferal.ClientID%>").val();
                    var cccReferalNumber = $("#<%=cccNumber.ClientID%>").val();
                    var previousDate = moment().subtract(1, 'days').format('DD-MMM-YYYY');
                    var adult = moment().subtract(15, 'years').format('DD-MMM-YYYY');
                    //validations
                    if (moment('' + dob + '').isAfter()) {
                        toastr.error("Date of birth cannot be a future date.");
                        return false;
                    }
                    if (moment('' + dob + '').isAfter(previousDate)) {
                        toastr.error("Date of birth cannot be today.");
                        return false;
                    }
                    if (moment('' + baselineHivStatusDate + '').isAfter()) {
                        toastr.error("Baseline HIV status date cannot be a future date.");
                        return false;
                    }
                    if (moment('' + hivTestingresultDate + '').isAfter()) {
                        toastr.error("HIV testing result date cannot be a future date.");
                        return false;
                    }
                    if (moment('' + baselineHivStatusDate + '').isBefore(dob)) {
                        toastr.error("Baseline HIV status date cannot be before the date of birth.");
                        return false;
                    } 
                    if (moment('' + hivTestingresultDate + '').isBefore(baselineHivStatusDate)) {
                        toastr.error("Baseline HIV testing date cannot be after HIV testing result date.");
                        return false;
                    }
                    if (moment('' + hivTestingresultDate + '').isBefore(dob)) {
                        toastr.error("HIV testing result date cannot be before the date of birth.");
                        return false;
                    }
                    if (moment('' + baselineHivStatusDate + '').isAfter(hivTestingresultDate)) {
                        toastr.error("Baseline HIV status date cannot be greater than HIV testing result date.");
                        return false;
                    }
                    if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Spouse")||($("#Relationship :selected").text() === "Partner")))  {
                        toastr.error("A child cannot have a spouse or partner.");
                        return false;
                    }
                    else {
                        var table = "<tr><td align='left'></td><td align='left'>" +
                            name +
                            "</td><td align='left'>" +
                            relationship +
                            "</td><td align='left'>" +
                            baselineHivStatus +
                            "</td><td align='left'>" +
                            baselineHivStatusDate +
                            "</td><td align='left'>" +
                            hivTestingresult +
                            "</td><td align='left'>" +
                            hivTestingresultDate +
                            "</td><td align='left'>" +
                            cccreferal +
                            "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                        $("#tblFamilyTesting>tbody:first").append('' + table + '');
                       
                        var testing = {
                            firstName: firstName,
                            middleName: middleName,
                            lastName: lastName,
                            sex: sex,
                            dob: dob,
                            relationshipId: relationshipId,
                            baselineHivStatusId: baselineHivStatusId,
                            baselineHivStatusDate: baselineHivStatusDate,
                            hivTestingresultId: hivTestingresultId,
                            hivTestingresultDate: hivTestingresultDate,
                            cccreferal: cccreferal,
                            cccReferalNumber: cccReferalNumber
                        };
                        familyMembers.push(testing);
                        resetElements();
                        e.preventDefault();
                    }
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

            $("#FamilyAdd").click(function () {
                $("#ViewFamilyTestingDetails").hide();
                $("#FamilyTestingDetails").show();
                resetElements();
            });

            //update family testing
            $("#btnSaveFamilyTesting").click(function() {
                updateFamilyTesting();
            });

            function loadFamilyTesting() {
                var patientId ="<%=PatientId%>";
                jQuery.support.cors = true;
                $.ajax(
                {
                    type: "POST",
                    url: "../WebService/PatientService.asmx/GetFamilyTestings",
                    data: "{'patientId':'" + patientId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function (response) {
                        window.itemList = response.d;
                        var table = '';
                        itemList.forEach(function (item, i) {
                            var n = i + 1;
                            var name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
                            var baselineDate = item.BaseLineHivStatusDate;
                            if (baselineDate != null) {
                                baselineDate = moment(item.BaseLineHivStatusDate).format('DD-MMM-YYYY');
                            } else {
                                baselineDate = "";
                            }

                            var testingDate = item.HivStatusResultDate;
                            if (testingDate != null) {
                                testingDate = moment(item.HivStatusResultDate).format('DD-MMM-YYYY');
                            } else {
                                testingDate = "";
                            }
                            table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + name + '</td><td style="text-align: left">' + item.Relationship + '</td><td style="text-align: left">' + item.BaseLineHivStatus + '</td><td style="text-align: left">' + baselineDate + '</td><td style="text-align: left">' + item.HivStatusResult + '</td><td style="text-align: left">' + testingDate + '</td><td style="text-align: left">' + item.CccReferal + "</td><td align='right'><button type='button' id= 'btnEditTesting' class='btn btn-link btn-sm pull-right' data-toggle='modal' data-target='#editFamilyTestingModal' onClick='editFamilyTesting(this)'> Edit</button></td></tr>";
                        });
                   
                        $('#tableFamilymembers').append(table);

                    },

                    error: function (msg) {
                        alert(msg.responseText);
                    }
                });
            }
        });

        function editFamilyTesting(x) {
            window.row = x.parentNode.parentNode.rowIndex;
            var rowIndex = row - 1;
            var familyTesting = itemList[rowIndex];
            $("#<%=fName.ClientID%>").val(familyTesting.FirstName);
            $("#<%=mName.ClientID%>").val(familyTesting.MiddleName);
            $("#<%=lName.ClientID%>").val(familyTesting.LastName);
            var dd = document.getElementById('sexMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.Sex) {
                    dd.selectedIndex = i;
                    break;
                }
            }
            $("#<%=dobMod.ClientID%>").val(moment(familyTesting.DateOfBirth).format('DD-MMM-YYYY'));
            var dd = document.getElementById('relationshipMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.Relationship) {
                    dd.selectedIndex = i;
                    break;
                }
            }
            var dd = document.getElementById('bHivStatusMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.BaseLineHivStatus) {
                    dd.selectedIndex = i;
                    break;
                }
            }
            $("#<%=bHivStatusDateMod.ClientID%>").val(moment(familyTesting.BaseLineHivStatusDate).format('DD-MMM-YYYY'));
            var dd = document.getElementById('testingStatusMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.HivStatusResult) {
                    dd.selectedIndex = i;
                    break;
                }
            }
            $("#<%=testingStatusDateMod.ClientID%>").val(moment(familyTesting.HivStatusResultDate).format('DD-MMM-YYYY'));
            var dd = document.getElementById('cccReferalMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.CccReferal) {
                    dd.selectedIndex = i;
                    break;
                }
            }
            $("#<%=cccNumberMod.ClientID%>").val(familyTesting.CccReferalNumber);
        }

        function resetElements(parameters) {
            $(".input-sm").val("");
        }

        function updateFamilyTesting() {
            if ($('#editFamilyTestingModal').parsley().validate()) {
                var patientId = <%=PatientId%>;
                var patientMasterVisitId = <%=PatientMasterVisitId%>;
                var firstName = escape($("#<%=fName.ClientID%>").val());
                var middleName = escape($("#<%=mName.ClientID%>").val());
                var lastName = escape($("#<%=lName.ClientID%>").val());
                var sex = $("#<%=sexMod.ClientID%>").val();
                var dob = $("#<%=dobMod.ClientID%>").val();
                var relationshipId = $("#<%=relationshipMod.ClientID%>").val();
                var baselineHivStatusId = $("#<%=bHivStatusMod.ClientID%>").val();
                var baselineHivStatusDate = $("#<%=bHivStatusDateMod.ClientID%>").val();
                var hivTestingresultId = $("#<%=testingStatusMod.ClientID%>").val();
                var hivTestingresultDate = $("#<%=testingStatusDateMod.ClientID%>").val();
                var cccreferal = $("#<%=cccReferalMod.ClientID%>").val();
                var cccReferalNumber = $("#<%=cccNumberMod.ClientID%>").val();
                var previousDate = moment().subtract(1, 'days').format('DD-MMM-YYYY');
                var adult = moment().subtract(15, 'years').format('DD-MMM-YYYY');
                //validations
                if (moment('' + dob + '').isAfter()) {
                    toastr.error("Date of birth cannot be a future date.");
                    return false;
                }
                if (moment('' + dob + '').isAfter(previousDate)) {
                    toastr.error("Date of birth cannot be today.");
                    return false;
                }
                if (moment('' + baselineHivStatusDate + '').isAfter()) {
                    toastr.error("Baseline HIV status date cannot be a future date.");
                    return false;
                }
                if (moment('' + hivTestingresultDate + '').isAfter()) {
                    toastr.error("HIV testing result date cannot be a future date.");
                    return false;
                }
                if (moment('' + baselineHivStatusDate + '').isBefore(dob)) {
                    toastr.error("Baseline HIV status date cannot be before the date of birth.");
                    return false;
                } 
                if (moment('' + hivTestingresultDate + '').isBefore(baselineHivStatusDate)) {
                    toastr.error("Baseline HIV testing date cannot be after HIV testing result date.");
                    return false;
                }
                if (moment('' + hivTestingresultDate + '').isBefore(dob)) {
                    toastr.error("HIV testing result date cannot be before the date of birth.");
                    return false;
                }
                if (moment('' + baselineHivStatusDate + '').isAfter(hivTestingresultDate)) {
                    toastr.error("Baseline HIV status date cannot be greater than HIV testing result date.");
                    return false;
                }
                if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Spouse")||($("#Relationship :selected").text() === "Partner")))  {
                    toastr.error("A child cannot have a spouse or partner.");
                    return false;
                }
                else {
                    debugger;  
                    $.ajax({
                        type: "POST",
                        url: "../WebService/PatientService.asmx/UpdatePatientFamilyTesting",
                        data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','firstName': '" + firstName + "','middleName': '" + middleName + "','lastName': '" + lastName + "','sex': '" + sex + "','dob': '" + dob + "','relationshipId': '" + relationshipId + "','baselineHivStatusId': '" + baselineHivStatusId + "','baselineHivStatusDate': '" + baselineHivStatusDate + "','hivTestingresultId': '" + hivTestingresultId + "','hivTestingresultDate': '" + hivTestingresultDate + "','cccreferal': '" + cccreferal + "','cccReferalNumber': '" + cccReferalNumber +"'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Family testing updated successfully");
                            //delay to show success message before redirect
                            setTimeout(function() { window.location.href = '<%=ResolveClientUrl("~/CCC/OneTimeEvents/FamilyTesting.aspx") %>'; }, 2500);
                        },
                        error: function (response) {
                            toastr.error(response.d, "Family testing not updated");
                        }
                    });
                }
            } 
            else {
                return false;
            }
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
                    //delay to show success message before redirect
                    setTimeout(function() { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                },
                error: function (response) {
                    toastr.error(response.d, "Family testing not saved");
                }
            });
        }

        function CccEnabled() {
            if (($("#hivtestingresult :selected").text() === "Tested Negative") || ($("#hivtestingresult :selected").text() === "Never Tested")) {
                $("#<%=cccNumber.ClientID%>").prop('disabled',true);
                        $("#<%=CccReferal.ClientID%>").prop('disabled',true);
                    }
                    else if ($("#CccReferal").val() === 'False') {
                        $("#<%=cccNumber.ClientID%>").prop('disabled',true);
            } else {
                $("#<%=cccNumber.ClientID%>").prop('disabled',false);
                $("#<%=CccReferal.ClientID%>").prop('disabled',false);
            }
    }
      
    function BaselineEnabled() {
        if ($("#BaselineHIVStatus :selected").text() === "Never Tested") {
            $("#<%=cccNumber.ClientID%>").prop('disabled',true);
            $("#<%=CccReferal.ClientID%>").prop('disabled',true);
            $("#<%=BaselineHIVStatusDate.ClientID%>").prop('disabled',true);
            $("#BaselineHIVStatusD").addClass('noneevents');
        } else {
            $("#<%=cccNumber.ClientID%>").prop('disabled',false);
            $("#<%=CccReferal.ClientID%>").prop('disabled',false);
            $("#BaselineHIVStatusD").removeClass('noneevents');
            $("#<%=BaselineHIVStatusDate.ClientID%>").prop('disabled',false);
        }
    }

    function HivEnabled() {
        if ($("#hivtestingresult :selected").text() === "Never Tested") {
              
            $("#<%=HIVTestingDate.ClientID%>").prop('disabled',true);
            $("#TestingDate").addClass('noneevents');
        } else {
              
            $("#<%=HIVTestingDate.ClientID%>").prop('disabled',false);
            $("#TestingDate").removeClass('noneevents');
        }
    }

        function CccEnabledMod() {
            if (($("#testingStatusMod :selected").text() === "Tested Negative") || ($("#testingStatusMod :selected").text() === "Never Tested")) {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',true);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled',true);
            }
            else if ($("#cccReferalMod").val() === 'False') {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',true);
            } else {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',false);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled',false);
            }
        }
      
        function BaselineEnabledMod() {
            if ($("#bHivStatusMod :selected").text() === "Never Tested") {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',true);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled',true);
                $("#<%=bHivStatusDateMod.ClientID%>").prop('disabled',true);
                $("#BaselineHIVStatusDMod").addClass('noneevents');
            } else {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',false);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled',false);
                $("#BaselineHIVStatusDMod").removeClass('noneevents');
                $("#<%=bHivStatusDateMod.ClientID%>").prop('disabled',false);
            }
        }

        function HivEnabledMod() {
            if ($("#testingStatusMod :selected").text() === "Never Tested") {
              
                $("#<%=HIVTestingDate.ClientID%>").prop('disabled',true);
                $("#TestingDateMod").addClass('noneevents');
            } else {
              
                $("#<%=testingStatusDateMod.ClientID%>").prop('disabled',false);
                $("#TestingDateMod").removeClass('noneevents');
            }
        }
    </script>

</asp:Content>
