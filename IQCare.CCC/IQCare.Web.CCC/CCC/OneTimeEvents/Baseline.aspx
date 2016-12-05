<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="Baseline.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.Baseline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
         <div class="col-md-12"><small class="pull-left fa fa-arrow-circle-o-right fa-2x"> Baseline Assessment & Treatment Inititiation</small></div>
              <div class="col-md-12"><hr /></div>
              <div class="col-md-12">
                   <div class="panel panel-default">
                        <div class="panel-body">
                             <div class="col-md-12">
    
                                  <div class="col-md-4">
                                                        <div class="col-md-12"><h5 class=" pull-left"><i class="fa fa-random" aria-hidden="true"></i> Patient Transfer Status</h5></div>
                                                        <div class="col-md-12"><hr /></div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                 <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblTINA">Transfer In ?</asp:Label></div>
                                                                    <div class="col-md-6">
                                                                         <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTransferInYes">
                                                                                <input class="sr-only" id="TransferInYes" type="checkbox" value="option1"> <span class="checkbox-label"> Yes </span>
                                                                        </label>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                         <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTransferInNo">
                                                                                <input class="sr-only" id="TransferInNo" type="checkbox" value="option1"> <span class="checkbox-label"> No </span>
                                                                        </label>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-right" id="lblTIDate">TransferIn Date</asp:Label></div> 
                                                                    <div class="col-md-12">
                                                                        <div class="datepicker fuelux" id="TIDate">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="TransferInDate" type="text" />
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
                                                                                            </span> <span class="year">2014</span>
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
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
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
                                                            <div class="row" style="padding-top:2%">
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblARTStartDate">ART Start Date</asp:Label></div>
                                                                    <div class="col-md-12">
                                                                        <div class="datepicker fuelux" id="ARTStartDate">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="myDatepickerInput" type="text" />
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
                                                                                            </span> <span class="year">2014</span>
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
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
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
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-right" id="lblRegimen">Regimen</asp:Label></div>
                                                                    <div class="col-md-12">
                                                                        <input type="text" runat="server" id="TIRegimen" class="form-control input-sm pull-right" placeholder="regimen" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="padding-top:15px">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblfacility">Facility Transferred from :</asp:Label></div>  
                                                                    <div class="col-md-12">
                                                                        <input type="text" id="facilityTransferredFrom" class="form-control input-sm" placeholder="facility name.." />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row"  style="padding-top:15px">
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblmflcode">MFL Code:</asp:Label></div>
                                                                    <div class="col-md-12">
                                                                        <input type="text" id="mflcode"  class="form-control input-sm" placeholder="mfl code" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-right" id="lblcounty">County:</asp:Label></div>
                                                                    <div class="col-md-12">
                                                                        <select id="county" class="form-control pull-right">

                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div><%-- .fomr-group--%>
                                                    </div><%-- .col-md-4--%>

                                  <div class="col-md-4">
                                                       <div class="col-md-12"> <h5 class=" pull-left"><i class="fa fa-user-md" aria-hidden="true"></i> Patient HIV Diagnosis</h5></div>
                                                        <div class="col-md-12"><hr /></div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblDateOfHIVDiagnosis">Date of HIV Diagnosis</asp:Label></div>
                                                                <div class="col-md-12">
                                                                     <div class="datepicker fuelux" id="DHID">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="DateOfHIVDiagnosis" type="text" />
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
                                                                                            </span> <span class="year">2014</span>
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
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
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
                                                            <div class="col-md-6">
                                                                <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-right" id="lblDateOfEnrollment">Date of Enrollment</asp:Label></div>
                                                                <div class="col-md-12">
                                                                     <div class="datepicker fuelux" id="DOE">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="DateOfEnrollment" type="text" />
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
                                                                                            </span> <span class="year">2014</span>
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
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
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

                                                        <div class="row" style="padding-top:15px">
                                                            <div class="col-md-6">
                                                                <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblwhostage">WHO Stage at Enrollment</asp:Label></div>
                                                                <div class="col-md-12">
                                                                    <select id="whostage" class="form-control input-sm">

                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-right" id="lblDateOfARTInitiation">Date of ART Initiation</asp:Label></div>
                                                                <div class="col-md-12">
                                                                     <div class="datepicker fuelux" id="DARTI">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="DateOfARTInitiation" type="text" />
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
                                                                                            </span> <span class="year">2014</span>
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
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
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

                                                        <div class="row" style="padding-top:15px">
                                                            <div class="col-md-12">
                                                                <div class="col-md-12">
                                                                     <div class="panel panel-default">
                                                                        <div class="panel-body">
                                                                            <div class="col-md-12" ><asp:Label runat="server" CssClass="control-label pull-left" id="lblARTUse"><i class="fa fa-history" aria-hidden="true"></i> History of ART Use </asp:Label></div> 
                                                                            <div class="col-md-12" style="padding-top:3%">
                                                                                <div class="col-md-4">
                                                                                    <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPEP">
                                                                                      <input class="sr-only" type="checkbox" id="PEP" value="option1"> <span class="checkbox-label"> PEP</span>
                                                                                    </label>
                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPMTCT">
                                                                                      <input class="sr-only" type="checkbox" id="PMTCT" value="option1"> <span class="checkbox-label"> PMTCT</span>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                 
                                                                                    <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblNONE">
                                                                                      <input class="sr-only" type="checkbox" id="NONE" value="option1"> <span class="checkbox-label"> NONE</span>
                                                                                    </label>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12" style="padding-top:3%">
                                                                                <asp:LinkButton ID="AddPriorHistory" CssClass="btn btn-info pull-left" runat="server" ><i class="fa fa-plus-circle" aria-hidden="true"></i> Add History of ART use</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                     </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                        </div>
                                                    </div><%-- .col-md-4--%>

                                  <div class="col-md-4">
                                                        <div class="col-md-12"><h5 class="pull-left"><i class="fa fa-cog" aria-hidden="true"></i> Patient Assessment </h5></div> 
                                                        <div class="col-md-12"><hr /></div>
                                                        <div class="form-group">
                                                            <div class="col-md-12"><i class="pull-left text-primary" style="padding-bottom:1%"><i class="fa fa-info-circle text-danger" aria-hidden="true"></i>Select patient baseline assessment checks</i></div>
                                                            
                                                            <div class="col-md-12">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-4">
                                                                                 <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblWHOStageHistory">
                                                                                      <input class="sr-only" type="checkbox" id="WHOStageHistory" value="option1"> <span class="checkbox-label"> WHO Stage</span>
                                                                                 </label>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                 <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblCD4Count">
                                                                                      <input class="sr-only" type="checkbox" id="CD4Count" value="option1"> <span class="checkbox-label"> CD4 Count</span>
                                                                                 </label>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                 <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBVCoInfection">
                                                                                      <input class="sr-only" type="checkbox" id="BVCoInfection" value="option1"> <span class="checkbox-label"> BV Co-Infection</span>
                                                                                 </label>
                                                                            </div>
                                                 
                                                                        </div>
                                                                        <div class="row" style="padding-top:3%">
                                                                           <div class="col-md-4">
                                                                                 <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPregnancy">
                                                                                      <input class="sr-only" type="checkbox" id="Pregnancy" value="option1"> <span class="checkbox-label"> Pregnancy</span>
                                                                                 </label>
                                                                            </div>
                                                                            
                                                                            <div class="col-md-4">
                                                                                 <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblB/HIV">
                                                                                      <input class="sr-only" type="checkbox" id="BHIV" value="option1"> <span class="checkbox-label"> B/HIV</span>
                                                                                 </label>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                 <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastFeeding">
                                                                                      <input class="sr-only" type="checkbox" id="BreastFeeding" value="option1"> <span class="checkbox-label"> BreastFeeding</span>
                                                                                 </label>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row" style="padding-top:3%">
                                                                            <div class="col-md-4">
                                                                                 <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblMUAC">MUAC </asp:label></div>
                                                                                 <div class="col-md-12"><input type="text" class="form-control input-sm" id="MUAC" placeholder="muac.." /></div>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                 <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblWeight">Weight (Kgs) </asp:label></div>
                                                                                 <div class="col-md-12"><input type="text" class="form-control input-sm" id="weight" placeholder="kg.." /></div>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                 <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblheight">Height (m) </asp:label></div>
                                                                                 <div class="col-md-12"><input type="text" class="form-control input-sm" id="height" placeholder="m.." /></div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row" style="padding-top:3%">
                                                                            <div class="col-md-4">
                                                                                 <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblBMI">BMI </asp:label></div>
                                                                                 <div class="col-md-12"><input type="text" class="form-control input-sm" id="bmi" placeholder="bmi.." /></div>
                                                                            </div>
                                                                            <div class="col-md-4"></div>
                                                                            <div class="col-md-4"></div>
                                                                        </div>
                                                                    </div><%-- .panel-body--%>
                                                                </div>
                                                            </div><%-- .col-md-12--%>

                                                        </div><%-- .fomr-group--%>
                                                    </div><%-- .col-md-4--%>
                            </div><%-- .col-md-12--%>

                            <div class="col-md-12">

                                  <div class="col-md-4">
                                                        <div class="col-md-12"><h5 class=" pull-left"><i class="fa fa-heartbeat" aria-hidden="true"></i> Treatment Initiation</h5></div>
                                                        <div class="col-md-12"><hr /></div>
                                                        
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblDateStartedOn1stLine">Date Started on 1st Line:</asp:Label> </div>
                                                                    <div class="col-md-12">
                                                                        <input  type="text" id="DateStartedOn1stLine" class="form-control input-sm" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblARTCohort"> ART Cohort: </asp:Label> </div>
                                                                    <div class="col-md-12">
                                                                        <input runat="server" type="text" readonly  id="ARTCohort" class="form-control input-sm" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="padding-top:3%">
                                                                <div class="col-md-6">
                                                                     <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblBaselineViralLoad"> Baseline Viral Load </asp:Label> </div>
                                                                     <div class="col-md-12">
                                                                        <input runat="server" type="text" id="BVL" class="form-control input-sm" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblBaselineVLDate">Baseline Viral Load Date </asp:Label> </div>
                                                                     <div class="col-md-12">
                                                                        <input runat="server" type="text" id="BVLDate" class="form-control input-sm" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                      
                                                    </div><%-- .col-md-4--%>
                                                    
                                 <div class="col-md-4">

                                                        <div class="col-md-12">
                                                            <table class="table table-stripped table-condensed">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Purpose</th>
                                                                        <th>Regimen</th>
                                                                        <th>Date Last Used</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>xxx</td>
                                                                        <td>xx</td>
                                                                        <td>xx</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                 <div class="col-md-4"></div><%--.col-md-4--%>

                           </div><%-- .col-md-12--%>
                           
                            <div class="col-md-12">
                                <div class="col-md-12"><hr /></div>
                                <div class="col-md-12">
                                    <div class="col-md-6"></div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4 pull-right">
                                        <div class="col-md-4"><asp:LinkButton ID="btnSave" runat="server" CssClass=" btn btn-info btn-lg fa fa-cog pull-right"> Save Baseline</asp:LinkButton></div>
                                         <div class="col-md-4"><asp:LinkButton ID="btnClose" runat="server" CssClass=" btn btn-warning btn-lg fa fa-minus-circle pull-right"> Close Baseline</asp:LinkButton></div>
                                         <div class="col-md-4"><asp:LinkButton ID="btnCancel" runat="server" CssClass=" btn btn-danger btn-lg fa fa-bin pull-right"> Clear Baseline</asp:LinkButton></div>
                                        
                                    </div>
                                </div>
                            </div>

                        </div><%-- .panel-body--%>
                  </div><%-- .panel-default--%>
             </div><%-- .col-md-12--%>
    </div><%-- .col-md-12--%>
    
    <script type="text/javascript">
        $(document).ready(function(){
            $('#ARTStartDate').datepicker();
            $('#TIDate').datepicker();
            $('#DHID').datepicker();
            $('#DOE').datepicker();
            $('#DARTI').datepicker();
        });
    </script>

</asp:Content>
