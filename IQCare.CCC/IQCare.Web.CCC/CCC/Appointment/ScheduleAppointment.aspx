<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ScheduleAppointment.aspx.cs" Inherits="IQCare.Web.CCC.Appointment.ScheduleAppointment" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12 bs-callout bs-callout-info">
        <div class="col-md-12">
            <label class="control-lable pull-left">Patient Appointment </label>
        </div>
        <div class="col-md-12">
            <uc:PatientDetails runat="server" />
        </div>
        <div class="col-md-12 form-group">
            <div class="col-md-12">
                <label class="control-label pull-left text-primary">Appointment Details</label>
            </div>
            <div class="col-md-12">
                <hr style="margin-bottom: 1%; margin-top: 1%" />
            </div>
        </div>

        <div class="col-md-12">
            <div class="col-md-3">
                <div class="form-group">
                    <div class="col-md-12">
                        <label for="Inschool" class="control-label pull-left">Appointment Type </label>
                    </div>
                    <div class="col-md-12">
                        <asp:DropDownList runat="server" ID="AppointmentType" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <div class="col-md-12">
                        <label for="Inschool" class="control-label pull-left">Person To See </label>
                    </div>
                    <div class="col-md-12">
                        <asp:DropDownList runat="server" ID="AppointmentPerson" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <div class="col-md-12">
                        <label class="control-label pull-left">Appointment Date</label>
                    </div>
                    <div class="col-md-12">
                        <div class="datepicker fuelux form-group" id="PersonAppointmentDate">
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
                <div class="form-group">
                    <div class="col-md-12">
                        <label class="control-label pull-left">Action</label>
                    </div>
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary btn-add pull-left" id="add" data-last="Complete">
                            Add Details
                        <span class="glyphicon glyphicon-plus"></span>
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 form-group">
            <div class="col-md-12 bg-primary"><span class="pull-left"></span>Appointment Details </div>
            <table class="table table-striped table-condensed" id="tblAppointment" clientidmode="Static" runat="server">
                <thead>
                    <tr>
                        <th><i class="text-primary" aria-hidden="true">#</i></th>
                        <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Appointment Type</i> </th>
                        <th><i class="fa fa-calendar-check-o text-primary" aria-hidden="true">Appointment Date</i> </th>
                        <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Person To Visit</i> </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>

        </div>
        <div class="col-md-12">
            <div class="col-md-6"></div>
            <div class="col-md-6">
                <div class="col-md-4">
                    <asp:LinkButton runat="server" ID="btnSaveAppointment" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Appointment </asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg "> Reset Entry  </asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg"> Close Appointment </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('#PersonAppointmentDate').datepicker({
            allowPastDates: false,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        });
    </script>
</asp:Content>


