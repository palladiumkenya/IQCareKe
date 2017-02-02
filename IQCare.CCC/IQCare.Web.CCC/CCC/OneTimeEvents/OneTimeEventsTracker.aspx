﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="OneTimeEventsTracker.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.OneTimeEventsTracker" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <div class="col-md-12">
             <uc:PatientDetails ID="PatientSummary" runat="server" />
        </div>
        
        <div class="col-md-12">

             <div class="panel panel-default">
                  <div class="panel-body" id="onetimeeventstracker" data-parsley-validate="true" data-show-errors="true">
                        
                       <div class="col-md-12">
                            <div class="col-md-12"><label class="control-label pull-left">Patient one time events tracker</label></div> 
                            <div class="col-md-12"><hr/></div>
                       </div>

                         <div class="col-md-12 form-group">
                              <div class="col-md-2 label label-success"><label class="pull-left"><small>Disclosure To </small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                              <div class="col-md-1"><label class="control-label pull-left"> Adolescents</label></div>
                              <div class="col-md-7">
                                  <div class="col-md-12">
                                       <div class="col-md-5"><label class="control-label pull-left"> Stage 1 Disclosure Date</label></div>
                                       <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="Stage1">
                                               <div class="input-group">
                                                    <input class="form-control input-sm" id="Stage1Date" type="text" data-parsley-required="true" runat="server" />
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
                                  
                                  <div class="col-md-12">
                                       <div class="col-md-5"><label class="control-label pull-left"> Stage 2 Disclosure Date</label></div>
                                       <div class="col-md-7">
                                            <div class="datepicker fuelux form-group" id="Stage2">
                                               <div class="input-group">
                                                                                  <input class="form-control input-sm" id="Stage2Date" type="text" runat="server"  />
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
                                  
                                  <div class="col-md-12">
                                       <div class="col-md-5"><label class="control-label pull-left"> Stage 3 Disclosure Date</label></div>
                                       <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="Stage3">
                                               <div class="input-group">
                                                                                  <input class="form-control input-sm" id="Stage3Date" type="text" runat="server"  />
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
                                  
                                  <div class="col-md-12">
                                       <div class="col-md-5"><label class="control-label pull-left">Sex Partner Disclose Date</label></div>
                                       <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="SexPartner">
                                               <div class="input-group">
                                                                                  <input class="form-control input-sm" id="SexPartnerDate" type="text" runat="server"  />
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

                              </div><%-- .col-md-7--%>
                              

                            

                  

                        </div><%-- .col-md-12--%>

                        <div class="col-md-12 form-group">
                            <div class="col-md-2 label label-default" ><label class="pull-left"><small>INH Prophylaxis</small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                            <div class="col-md-1"><label class="control-label pull-left"> Prophylaxis</label></div>
                            <div class="col-md-7">
                                 <div class="col-md-12 form-group">
                                      <div class="col-md-5"><label class="control-label pull-left"> INH Start Date</label></div>
                                      <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="StartDate">
                                               <div class="input-group">
                                                                                  <!--<input class="form-control input-sm" id="INHStartDate" type="text" data-parsley-required="true" />-->
                                                                                    <asp:TextBox runat="server" ID="INHStartDate" CssClass="form-control input-sm" ClientIDMode="Static" data-parsley-required="true" type="text"></asp:TextBox>
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

                                 <div class="col-md-12 form-group">
                                      <div class="col-md-5"><label class="control-label pull-left"> INH Completion ? </label></div>
                                      <div class="col-md-7">
                                           <label class="radio-custom radio-inline pull-left" data-initialize="radio"  id="lblCompletionYes">
                                               <input class="sr-only" type="radio" id="CompletionYes" value="option1" name="INH"> <span class="checkbox-label"><strong> Yes </strong></span>
                                           </label>

                                           <label class="radio-custom radio-inline pull-left" data-initialize="radio"  id="lblCompletionNo">
                                             <input class="sr-only" type="radio" id="CompletionNo" value="option1" name="INH"> <span class="checkbox-label"><strong> No </strong></span>
                                           </label>
                                      </div>
                                 </div>
                            </div><%-- .col-md-7--%>
                        </div><%--.col-md-12--%>

                        <div class="col-md-12 form-group">
                            <div class="col-md-2 label label-success" ><label class="pull-left"><small >Vaccination Adult</small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                            <div class="col-md-1"></div>
                            <div class="col-md-7">
                                 <div class="col-md-12">
                                      <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblHBV">
                                        <input class="sr-only" type="checkbox" id="HBV" value="option1"> <span class="checkbox-label"><strong> HBV </strong></span>
                                 </label></div>

                                      <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblFluVaccine">
                                         <input class="sr-only" type="checkbox" id="FluVaccine" value="option1"> <span class="checkbox-label"><strong> Flu Vaccine </strong></span>
                                         </label>
                                      </div>
                                 </div>

                                 <div class="col-md-12">
                                      <div class="col-md-5"><label class="control-label pull-left">Other (Vaccination) Specify</label></div>
                                      <div class="col-md-7">
                                          <asp:TextBox runat="server" ID="vaccinationotheradult" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="other specify.."></asp:TextBox>
                                      </div>
                                 </div>
                            </div><%-- .col-md-7--%>
                        </div><%-- .col-md-12--%>

                        <div class="col-md-12 form-group">
                            <div class="col-md-2 label label-success"><label class="pull-left"><small>Vaccination Child</small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                            <div class="col-md-1"></div> 
                        </div><%-- .col-md-12--%>
                        
                      <div class="col-md-12">
                          <div class="panel panel-primary">
                                    <div class="panel-heading">Child Vaccination</div>
                                    <div class="panel-body">
                                        <div class="col-md-1">
                                             <div class="col-md-12"><label class="control-label pull-left"> BCG</label></div>
                                             <div class="col-md-12">
                                                 <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblBCG">
                                                        <input class="sr-only" type="checkbox" id="BCG" value="option1"> <span class="checkbox-label"><strong> BCG </strong></span>
                                                 </label>
                                             </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-12"><label class="control-label pull-left"> Polio Vaccine</label></div>
                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPolioVaccine1">
                                                    <input class="sr-only" type="checkbox" id="PolioVaccinw1" value="option1"> <span class="checkbox-label"><strong> Polio Vaccine 1 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblpoliovaccine2">
                                                    <input class="sr-only" type="checkbox" id="poliovaccine2" value="option1"> <span class="checkbox-label"><strong> Polio Vaccine 2 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblpoliovaccine3">
                                                    <input class="sr-only" type="checkbox" id="PolioVaccine3" value="option1"> <span class="checkbox-label"><strong> Polio Vaccine 3 </strong></span>
                                             </label></div>

                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-12"><label class="control-label pull-left"> Pentavalent</label></div>
                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPentavalent1">
                                                    <input class="sr-only" type="checkbox" id="Pentavalent1" value="option1"> <span class="checkbox-label"><strong> Pentavalent 1 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPentavalent2">
                                                    <input class="sr-only" type="checkbox" id="Pentavalent2" value="option1"> <span class="checkbox-label"><strong> Pentavalent 2 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPentavalent3">
                                                    <input class="sr-only" type="checkbox" id="Pentavalent3" value="option1"> <span class="checkbox-label"><strong> Pentavalent 3 </strong></span>
                                             </label></div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-12"><label class="control-label pull-left"> Pneumococcal</label></div>
                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPneumococcal1">
                                                    <input class="sr-only" type="checkbox" id="Pneumococcal1" value="option1"> <span class="checkbox-label"><strong> Pneumococcal 1 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPneumococcal2">
                                                    <input class="sr-only" type="checkbox" id="Pneumococcal2" value="option1"> <span class="checkbox-label"><strong> Pneumococcal 2 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPneumococcal3">
                                                    <input class="sr-only" type="checkbox" id="Pneumococcal3" value="option1"> <span class="checkbox-label"><strong> Pneumococcal 3 </strong></span>
                                             </label></div>
                                        </div>
                                        <div class="col-md-2">
                                             <div class="col-md-12"><label class="control-label pull-left"> Measles</label></div>
                                             <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblMeasles1">
                                                    <input class="sr-only" type="checkbox" id="Measles1" value="option1"> <span class="checkbox-label"><strong> Measles 1 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblMeasles2">
                                                    <input class="sr-only" type="checkbox" id="Measles2" value="option1"> <span class="checkbox-label"><strong> Measles 2 </strong></span>
                                             </label></div>

                                            <div class="col-md-12 form-group"><label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblMeasles3">
                                                    <input class="sr-only" type="checkbox" id="Measlesl3" value="option1"> <span class="checkbox-label"><strong> Measles 3 </strong></span>
                                             </label></div>
                                        </div>
                                        
                                    </div>
                                </div>
                      </div>

                        <div class="col-md-12">
                            <hr />
                            <div class="col-md-6"></div>

                            <div class="col-md-6">
                                 <div class="col-md-4"><asp:LinkButton runat="server" ID="btnOneTimeEventsTracker" CssClass=" btn btn-info btn-lg fa fa-arrow-circle-o-right"> Save One Time Event</asp:LinkButton></div>
                                 <div class="col-md-4"><asp:LinkButton runat="server" ID="LinkButton1" CssClass=" btn btn-warning btn-lg fa fa-refresh" > Reset One Time Event</asp:LinkButton></div> 
                                 <div class="col-md-4"><asp:LinkButton runat="server" ID="btnClose" CssClass=" btn btn-danger fa fa-times btn-lg" > Close One Time Event</asp:LinkButton></div>
                            </div>
                        </div>

                  </div> <%--.panel-body--%>
            </div>
        </div>
        
    </div><%-- .container-fluid--%>
        <script type="text/javascript">
        $(document).ready(function () {
            /*$('#Stage1').datepicker();
            $('#Stage2').datepicker();
            $('#Stage3').datepicker();
            $('#SexPartner').datepicker();*/

        });

        $('#Stage1').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $('#Stage2').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $('#Stage3').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $('#SexPartner').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $("#ctl00_IQCareContentPlaceHolder_btnOneTimeEventsTracker").click(function () {
            if ($("#onetimeeventstracker").parsley().validate()) {
                return true;
            } else {
                return false;
            }
        });

    </script>
</asp:Content>
