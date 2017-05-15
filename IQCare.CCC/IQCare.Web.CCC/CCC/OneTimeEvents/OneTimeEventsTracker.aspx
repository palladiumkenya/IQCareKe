<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="OneTimeEventsTracker.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.OneTimeEventsTracker" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
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
                        <asp:HiddenField ID="Age" runat="server" ClientIDMode="Static"></asp:HiddenField>
                        <asp:HiddenField ID="dob" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="enrollmentDate" runat="server" ClientIDMode="Static" />

                              <div class="col-md-2"><label class="pull-left"><small>Disclosure To </small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                              <div class="col-md-1"><label class="control-label pull-left"> Adolescents</label></div>
                              <div class="col-md-7">
                                  <div class="col-md-12">
                                       <div class="col-md-5"><label class="control-label pull-left"> Stage 1 Date</label></div>
                                       <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="Stage1">
                                               <div class="input-group">
                                                    <input  class="form-control input-sm" id="Stage1Date" type="text" runat="server" ClientIDMode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
                                       <div class="col-md-5"><label class="control-label pull-left"> Stage 2 Date</label></div>
                                       <div class="col-md-7">
                                            <div class="datepicker fuelux form-group" id="Stage2">
                                               <div class="input-group">
                                                                                  <input class="form-control input-sm" id="Stage2Date" type="text" runat="server"  ClientIDMode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
                                       <div class="col-md-5"><label class="control-label pull-left"> Stage 3 Date</label></div>
                                       <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="Stage3">
                                               <div class="input-group">
                                                                                  <input class="form-control input-sm" id="Stage3Date" type="text" runat="server" ClientIDMode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
                        </div>

                    <div class="col-md-12 form-group">
                            <div class="col-md-2"><label class="pull-left"><small>Disclosure To </small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                            <div class="col-md-1"><label class="control-label pull-left">Sex Partner</label></div>
                            <div class="col-md-7">
                                <div class="col-md-12">
                                       <div class="col-md-5"><label class="control-label pull-left">Date</label></div>
                                       <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="SexPartner">
                                               <div class="input-group">
                                                                                  <input class="form-control input-sm" id="SexPartnerDate" type="text" runat="server" ClientIDMode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
                            
                        </div>

                    <div class="col-md-12 form-group">
                            <div class="col-md-2" ><label class="pull-left"><small>INH Prophylaxis</small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                            <div class="col-md-1"><label class="control-label pull-left"> Prophylaxis</label></div>
                            <div class="col-md-7">
                                 <div class="col-md-12 form-group">
                                      <div class="col-md-5"><label class="control-label pull-left"> INH Start Date</label></div>
                                      <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="INHStartDatePicker">
                                               <div class="input-group">
                                                                                  <!--<input class="form-control input-sm" id="INHStartDate" type="text" data-parsley-required="true" />-->
                                                                                    <asp:TextBox runat="server" ID="INHStartDate" CssClass="form-control input-sm" ClientIDMode="Static" type="text" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                               <!--<input class="sr-only" type="radio" id="CompletionYes" value="option1" name="INH"> <span class="checkbox-label"><strong> Yes </strong></span>-->

                                               <asp:RadioButton ID="CompletionYes" runat="server" GroupName="INH" ClientIDMode="Static" /><span class="checkbox-label"><strong> Yes </strong></span>
                                           </label>

                                           <label class="radio-custom radio-inline pull-left" data-initialize="radio"  id="lblCompletionNo">
                                             <!--<input class="sr-only" type="radio" id="CompletionNo" value="option1" name="INH"> <span class="checkbox-label"><strong> No </strong></span>-->
                                               <asp:RadioButton ID="CompletionNo" runat="server" GroupName="INH" ClientIDMode="Static" /><span class="checkbox-label"><strong> No </strong></span>
                                           </label>
                                      </div>
                                 </div>

                                <div class="col-md-12 form-group" id="ISCompletionDate">
                                      <div class="col-md-5"><label class="control-label pull-left">Completion Date</label></div>
                                      <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="CompletionDate">
                                               <div class="input-group">
                                                    <!--<input class="form-control input-sm" id="INHStartDate" type="text" data-parsley-required="true" />-->
                                                    <asp:TextBox runat="server" ID="INHCompletionDate" CssClass="form-control input-sm" ClientIDMode="Static" type="text" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                
                                <div class="col-md-12" id="ISStopDate">
                                    <div class="col-md-5"><label class="control-label pull-left">Stop Date</label></div>
                                    <div class="col-md-7">
                                          <div class="datepicker fuelux form-group" id="StopDate">
                                               <div class="input-group">
                                                    <!--<input class="form-control input-sm" id="INHStartDate" type="text" data-parsley-required="true" />-->
                                                    <asp:TextBox runat="server" ID="INHStopDate" CssClass="form-control input-sm" ClientIDMode="Static" type="text" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                        </div><%--.col-md-12--%>

                    <div class="col-md-12 form-group">
                            <div class="col-md-2" ><label class="pull-left"><small >Vaccination Adult</small> <i class="fa fa-angle-double-right" aria-hidden="true"></i></label></div>
                            <div class="col-md-1"></div>
                            <div class="col-md-7">
                                 <div class="col-md-12">

                                      <div class="col-md-12 form-group">
                                          <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblHBV">
                                              <input class="sr-only" type="checkbox" id="HBV" value="HBV" name="vaccineAdult"><span class="checkbox-label"><strong> HBV </strong></span>
                                          </label>
                                      </div>

                                      <div class="col-md-12 form-group">
                                          <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblFluVaccine">
                                              <input class="sr-only" type="checkbox" id="FluVaccine" value="FluVaccine" name="vaccineAdult"><span class="checkbox-label"><strong>Flu Vaccine</strong></span>
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
                        
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                                <div class="panel-heading">Child Vaccination</div>
                                <div class="panel-body">

                                    <div class="col-md-4">
                                        <div class="col-md-12"><label class="pull-left control-label">Vaccine</label></div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="VaccineType" ClientIDMode="Static"/>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="col-md-12"><label class="pull-left control-label">Vaccine Stage</label></div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="VaccineStage" ClientIDMode="Static"/>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-12"><label class="control-label text-danger">Action</label></div>      
                                        <div class="col-md-12 pull-right">
                                                <asp:LinkButton runat="server" ID="btnAdd"  ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle"> Add Vaccine</asp:LinkButton>
                                        </div>
                                        </div>
                                        
                                    <div class="panel-body col-md-9 form-group">
                                        <table class="table table-striped table-condensed form-group" id="tblVaccines" clientidmode="Static" runat="server">
                                            <thead>
                                                <tr >
                                                    <th>#</th>
                                                    <th><i class="fa fa-calendar-check-o text-primary" aria-hidden="true">Vaccine</i> </th>
                                                    <th> <i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Vaccine Stage</i> </th>
                                                    <th><span class="fa fa-times text-danger text-primary pull-right"> Action</span></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>                               
                                </div>
                                    
                            </div>
                    </div>

                    <div class="col-md-12">
                            <div class="col-md-3"></div>

                            <div class="col-md-7">
                                 <div class="col-md-4"><asp:LinkButton runat="server" ID="btnOneTimeEventsTracker" CssClass=" btn btn-info btn-lg fa fa-arrow-circle-o-right" ClientIDMode="Static" OnClientClick="return false;"> Save One Time Event</asp:LinkButton></div>
                                 <div class="col-md-4"><asp:LinkButton runat="server" ID="btnResetOneTimeEvent" CssClass=" btn btn-warning btn-lg fa fa-refresh" ClientIDMode="Static" OnClientClick="return false;"> Reset One Time Event</asp:LinkButton></div> 
                                 <div class="col-md-4"><asp:LinkButton runat="server" ID="btnClose" CssClass=" btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close One Time Event</asp:LinkButton></div>
                            </div>
                            <div class="col-md-3"></div>
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

            $("#ISCompletionDate").hide();
            $("#ISStopDate").hide();

            $('input[name="ctl00$IQCareContentPlaceHolder$INH"]').on('change', function (e) {
                if ($('input[name="ctl00$IQCareContentPlaceHolder$INH"]:checked').val() == "CompletionYes") {
                    $("#ISCompletionDate").show();
                    $("#ISStopDate").hide();
                    $("#INHCompletionDate").val('');
                    $("#INHStopDate").val('');
                } else {
                    $("#ISCompletionDate").hide();
                    $("#ISStopDate").show();
                    $("#INHCompletionDate").val('');
                    $("#INHStopDate").val('');
                }
            });

            var Age = $("#Age").val();
            if (Age > 14 || Age < 6) {
                $("#<%=Stage1Date.ClientID%>").attr('disabled', 'disbaled');
                $("#Stage1").addClass("noneevents");
                $("#<%=Stage2Date.ClientID%>").attr('disabled', 'disbaled');
                $("#Stage2").addClass("noneevents");
                $("#<%=Stage3Date.ClientID%>").attr('disabled', 'disbaled');
                $("#Stage3").addClass("noneevents");
            }
            if (Age <= 14) {
                $("#<%=SexPartnerDate.ClientID%>").attr('disabled', 'disbaled');
                $("#SexPartner").addClass("noneevents");
                $("#HBV").prop("disabled", !this.checked);
                $("#FluVaccine").prop("disabled", !this.checked);
                $("#vaccinationotheradult").attr('disabled', 'disbaled');
               
            } else {
                $("#VaccineType").attr('disabled', 'disbaled');
                $("#VaccineStage").attr('disabled', 'disbaled');
                $("#btnAdd").attr('disabled', 'disbaled');
                $("#btnAdd").addClass("noneevents");
            }

            /*Lookup vaccines*/
            var vaccinesList = new Array();

            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetLookUpItemByName",
                data: "{'itemName':'Vaccine'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);
                    $("#<%=VaccineType.ClientID%>").find('option').remove().end();
                    $("#<%=VaccineType.ClientID%>").append('<option value="0">Select</option>');
                    $.each(itemList, function (index, itemList) {
                        $("#<%=VaccineType.ClientID%>").append('<option value="' + itemList.ItemId + '">' + itemList.ItemName + '</option>');
                    }); 
                },
                error: function (msg) {
                    alert(msg);
                }
            });

            $("#btnAdd").click(function(e) {

                var identifierCount = 0;
                var vaccineFound = 0;

                var vaccineTypeId = $("#<%=VaccineType.ClientID%>").find(':selected').val();
                var vaccineStageId = $("#<%=VaccineStage.ClientID%>").find(":selected").val();

                var vaccineType = $("#<%=VaccineType.ClientID%>").find(':selected').text();
                var vaccineStage = $("#<%=VaccineStage.ClientID%>").find(':selected').text();

                //console.log(vaccineStage);

                if (vaccineStageId === undefined || vaccineStageId === null) {
                    toastr.error("error", "Please select at least One(1) vaccine from the List");
                    return false;
                }else if (vaccineStageId < 1) {
                    toastr.error("error", "Please select at least One(1) vaccine from the List");
                    return false;
                }
                //console.log(vaccinesList);
                //vaccineFound = $.inArray("" + vaccineStageId + "", vaccinesList);
                for (var key in vaccinesList) {
                    if ((key == vaccineTypeId) && (vaccinesList[key] == vaccineStageId)) {
                        vaccineFound = -1;
                    }
                }
              
                if (vaccineFound == -1) {
                    
                    toastr.error("error", vaccineStage + " Vaccine already exists in the List");
                    return false; // message box herer
                } else {
                    
                    //vaccinesList.push("" + vaccineStageId + "");
                    vaccinesList[vaccineTypeId] = vaccineStageId;

                    var tr = "<tr><td align='left'></td><td align='left'>" + vaccineType + "</td><td align='left'>" + vaccineStage + "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                    $("#tblVaccines>tbody:first").append('' + tr + '');
                }

                e.preventDefault();
            });

            $("#tblVaccines").on('click', '.btnDelete', function () {
                $(this).closest('tr').remove();
                var x = $(this).closest('tr').find('td').eq(0).html();

                vaccinesList.splice($.inArray(x, vaccinesList), 1);
            });

            $("#VaccineType").on('change', function (e) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
                    data: "{'masterName': '" + $('#VaccineType option:selected').text() + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var itemList = JSON.parse(response.d);
                        $("#<%=VaccineStage.ClientID%>").find('option').remove().end();
                        $("#<%=VaccineStage.ClientID%>").append('<option value="0">Select</option>');
                        $.each(itemList, function (index, itemList) {
                            $("#<%=VaccineStage.ClientID%>").append('<option value="' + itemList.ItemId + '">' + itemList.DisplayName + '</option>');
                        }); 
                    },
                    error: function (msg) {
                        alert(msg);
                    }
                });
            });

            $("#btnClose").click(function() {
                window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';
            });


            $("#btnResetOneTimeEvent").click(function () {
                $("#Stage1Date").val("");
                $("#Stage2Date").val("");
                $("#Stage3Date").val("");
                $("#SexPartnerDate").val("");
                $("#INHStartDate").val("");
                $('#INHStartDatePicker').datepicker('setDate', "");
                $("#CompletionYes").attr("checked", false);
                $("#CompletionNo").attr("checked", false);
                $("#lblCompletionNo").removeClass("checked");
                $("#lblCompletionYes").removeClass("checked");
                $("#HBV").attr("checked", false);
                $("#lblHBV").removeClass("checked");
                $("#FluVaccine").attr("checked", false);
                $("#lblFluVaccine").removeClass("checked");
                $("#vaccinationotheradult").val("");
                $("#INHCompletionDate").val("");
                $("#INHStopDate").val("");

                $("#tblVaccines").find("tr:gt(0)").remove();
                vaccinesList.length = 0;

            });

            $("#btnOneTimeEventsTracker").click(function () {
                if ($("#onetimeeventstracker").parsley().validate()) {
                    var _fp = [];
                    var data = $('#tblVaccines tr').each(function (row, tr) {
                        _fp[row] = {
                            "vaccineType": $(tr).find('td:eq(1)').text()
                         , "vaccineStage": $(tr).find('td:eq(2)').text()
                        }
                    });
                    _fp.shift();//first row will be empty -so remove

                    var Stage1DateValue = $("#Stage1Date").val();
                    var Stage2DateValue = $("#Stage2Date").val();
                    var Stage3DateValue = $("#Stage3Date").val();
                    var SexPartnerDateValue = $("#SexPartnerDate").val();
                    var INHStartDateValue = $("#INHStartDate").val();
                    var INHCompletion = null;
                    var INHCompletionDateValue = "";

                    var Stage1 = $('#Stage1').datepicker('getDate');
                    var Stage2 = $("#Stage2").datepicker('getDate');
                    var Stage3 = $("#Stage3").datepicker('getDate');
                    var SexPartner = $("#SexPartner").datepicker('getDate');
                    var CompletionDate = $("#CompletionDate").datepicker('getDate');
                    var StopDate = $("#StopDate").datepicker('getDate');
                    var INHStartDate = $("#INHStartDatePicker").datepicker('getDate');
                    var dob = $("#dob").val();
                    var enrollmentDate = $("#enrollmentDate").val();
                    dob = moment(dob);

                    var isBeforeDobCompletionDate = moment(CompletionDate).isBefore(dob);
                    var isBeforedobStopDate = moment(StopDate).isBefore(dob);
                    var isBeforeINHStartDate = moment(INHStartDate).isBefore(dob);

                    var isBeforeStage1 = moment(Stage1).isBefore(dob);
                    var isBeforeStage2 = moment(Stage2).isBefore(dob);
                    var isBeforeStage3 = moment(Stage3).isBefore(dob);
                    var isBeforeSexPartner = moment(SexPartner).isBefore(dob);

                    //var isCompletionDateBeforeEnrollmentDate = moment(CompletionDate).isBefore(enrollmentDate);
                    //var isStopDateBeforeEnrollmentDate = moment(StopDate).isBefore(enrollmentDate);
                    //var isBeforeEnrollmentDate = moment(INHStartDate).isBefore(enrollmentDate);

                    var isCompletionDateBeforeINHStartDate = moment(CompletionDate).isBefore(INHStartDate);
                    var isStopDateBeforeINHStartDate = moment(CompletionDate).isBefore(INHStartDate);

                    if (isBeforeStage1) {
                        toastr.error("Disclosure stage 1 date should not before dob", "One Time Event Tracker");
                        return false;
                    }

                    if (isBeforeStage2) {
                        toastr.error("Disclosure stage 2 date should not before dob", "One Time Event Tracker");
                        return false;
                    }

                    if (isBeforeStage3) {
                        toastr.error("Disclosure stage 3 date should not before dob", "One Time Event Tracker");
                        return false;
                    }

                    if (isBeforeSexPartner) {
                        toastr.error("Disclosure Sex Partner date should not before dob", "One Time Event Tracker");
                        return false;
                    }

                    if (isCompletionDateBeforeINHStartDate) {
                        toastr.error("Completion date should not before INH start date", "One Time Event Tracker");
                        return false;
                    }

                    if (isStopDateBeforeINHStartDate) {
                        toastr.error("Stop date should not before date INH start date", "One Time Event Tracker");
                        return false;
                    }

                    if (isBeforeINHStartDate) {
                        toastr.error("INH start date should not before date of birth", "One Time Event Tracker");
                        return false;
                    }

                    if (isBeforeDobCompletionDate) {
                        toastr.error("Completion Date should not before date of birth", "One Time Event Tracker");
                        return false;
                    }

                    if (isBeforedobStopDate) {
                        toastr.error("Stop Date should not before date of birth", "One Time Event Tracker");
                        return false;
                    }

                    
                    //alert(document.getElementById("CompletionNo").Value);
                    //alert(document.getElementById("CompletionYes").Value);
                    var INH = $('input[name="ctl00$IQCareContentPlaceHolder$INH"]:checked').val();
                    if (INH == 'CompletionYes') {
                        INHCompletion = true;
                        //INHCompletionDateValue = $("#INHCompletionDate").val();
                        INHCompletionDateValue = moment(CompletionDate).format("DD-MMM-YYYY");
                    } else if (INH == 'CompletionNo') {
                        INHCompletion = false;
                        //INHCompletionDateValue = $("#INHCompletionDate").val();
                        INHCompletionDateValue = moment(StopDate).format("DD-MMM-YYYY");
                    }

                    var vaccineAdult = [];

                    $.each($("input[name='vaccineAdult']:checked"), function () {
                        vaccineAdult.push($(this).val());
                    });

                    if ($("#vaccinationotheradult").val() != null && $("#vaccinationotheradult").val() != "") {
                        vaccineAdult.push($("#vaccinationotheradult").val());
                    }

                    if (moment('' + Stage1 + '').isAfter()) {
                        toastr.error("Stage1. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    if (moment('' + Stage2 + '').isAfter()) {
                        toastr.error("Stage2. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    if (moment('' + Stage3 + '').isAfter()) {
                        toastr.error("Stage3. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    if (moment('' + SexPartner + '').isAfter()) {
                        toastr.error("SexPartner. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    if (moment('' + CompletionDate + '').isAfter()) {
                        toastr.error("CompletionDate. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    if (moment('' + StopDate + '').isAfter()) {
                        toastr.error("Stop Date. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    if (moment('' + INHStartDate + '').isAfter()) {
                        toastr.error("INHStartDate. Future dates not allowed.", "One Time Event Tracker");
                        return false;
                    }

                    /*console.log(Stage1DateValue);
                    console.log(Stage2DateValue);
                    console.log(Stage3DateValue);
                    console.log(SexPartnerDateValue);
                    console.log(INHStartDateValue);
                    console.log(INHCompletion);
                    console.log(INHCompletionDateValue);
                    console.log(_fp.length);
                    console.log(vaccineAdult.length);
                    console.log(vaccineAdult);*/

                    if (_fp.length == 0 && Stage1DateValue == "" && Stage2DateValue == "" && Stage3DateValue == "" && SexPartnerDateValue == "" && INHStartDateValue == "" && INHCompletion == null && INHCompletionDateValue == "" && vaccineAdult.length == 0) {
                        toastr.error("You submitted an empty form", "One Time Events Tracker");
                        return false;
                    }
                    
                    //console.log(vaccineAdult);
                    addOneTimeEventTracker(_fp, Stage1DateValue, Stage2DateValue, Stage3DateValue, SexPartnerDateValue, INHStartDateValue, INHCompletion, INHCompletionDateValue, vaccineAdult);

                } else {
                    return false;
                }
            });


            function addOneTimeEventTracker(_fp, Stage1DateValue, Stage2DateValue, Stage3DateValue, SexPartnerDateValue, INHStartDateValue, INHCompletion, INHCompletionDateValue, vaccineAdult) {
                var vaccines = JSON.stringify(_fp);
                var adultVaccine = JSON.stringify(vaccineAdult);

                $.ajax({
                    type: "POST",
                    url: "../WebService/OneTimeEventsTrackerService.asmx/addOneTimeEventsTracker",
                    data: "{'Stage1DateValue':'" + Stage1DateValue + "','Stage2DateValue':'" + Stage2DateValue + "', 'Stage3DateValue': '" +
                        Stage3DateValue + "', 'SexPartnerDateValue':'" + SexPartnerDateValue + "','INHStartDateValue': '" + INHStartDateValue +
                        "','INHCompletion': '" + INHCompletion + "','CompletionDate': '" + INHCompletionDateValue + "','adultVaccine': '" + adultVaccine + "','vaccines': '" + vaccines + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        toastr.success(response.d, "One Time Events Tracker");
                        window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';
                    },
                    error: function (response) {
                        //generate('error', response.d);
                        toastr.error(response.d, "One Time Events Tracker");
                    }
                });
            }
        });

            $('#Stage1').datepicker({
                date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
                restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });

            $('#Stage2').datepicker({
                date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
                restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });

        $('#Stage3').datepicker({
            date: null,
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $('#SexPartner').datepicker({
            date: null,
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $("#INHStartDatePicker").datepicker({
            date: null,
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $("#CompletionDate").datepicker({
            date: null,
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $("#StopDate").datepicker({
            date: null,
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });
    </script>
</asp:Content>
