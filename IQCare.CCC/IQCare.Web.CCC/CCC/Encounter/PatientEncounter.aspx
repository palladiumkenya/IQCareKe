<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="PatientEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PatientEncounter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
  
        <div class="container-fluid">
        <div class="row"></div>
        
        <div class="row">
             
            <div class="col-md-12 col-xs-12">

                 <ul class="nav nav-tabs" role="tablist">
                     <li role="presentation" class="active"><a href="#home"><i class="fa fa-home fa-lg" aria-hidden="true"></i> Home</a></li>
                     <li role="presentation"> <a href="#Baseline"     aria-controls="Baseline"    role="tab" data-toggle="tab"><i class="fa fa-arrow-circle-o-right fa-lg" aria-hidden="true"></i> Baseline</a></li>
                     <li role="presentation"> <a href="#encounter"    aria-controls="encounter"   role="tab" data-toggle="tab"><i class="fa fa-exchange fa-lg" aria-hidden="true"></i> Clinical Encounter</a></li>
                     <li role="presentation"> <a href="#vlTracker"    aria-controls="vlTracker"   role="tab" data-toggle="tab"><i class="fa fa-line-chart fa-lg" aria-hidden="true"></i> Viraload Tracker</a></li>
                     <li role="presentation"> <a href="#Laboratory"   aria-controls="Laboratory"  role="tab" data-toggle="tab"><i class="fa fa-flask fa-lg" aria-hidden="true"></i> Laboratory</a></li>
                     <li role="presentation"> <a href="#Pharmacy"     aria-controls="Pharmacy"    role="tab" data-toggle="tab"><i class="fa fa-tint fa-lg" aria-hidden="true"></i> Pharmacy</a></li>
                     <li role="presentation"> <a href="#history"      aria-controls="history"     role="tab" data-toggle="tab"><i class="fa fa-history fa-lg" aria-hidden="true"></i> Encounter History</a></li>
                 </ul>
             </div><!-- .col-md-12 -->
            
            <div class="col-md-12 col-xs-12">

                 <div class="tab-content">
                           
                      <div  role="tabpanel"     class="tab-pane active"    id="home">

                           </div><!-- .home-->

                      <div  role="tabpanel"     class="tab-pane fade"      id="Baseline">
                            <div class="row">
                                <br /> 
                                <div class="col-md-12">
                                    <div class="col-md-12"><h3 class="text-primary pull-left fa fa-yelp fa-3x"> Patient Baseline Information </h3></div>
                                    <div class="col-md-12"><hr /></div>
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <div class="row">
    
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
                                                </div><%-- .row--%>

                                                <div class="row">

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

                                                    <div class="col-md-4">
                                                        <div class="col-md-12"><h5 class="fa fa-bolt pull-left"> Action</h5></div>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                 <asp:LinkButton ID="btnSave" runat="server" CssClass=" btn btn-success fa fa-cog"> Save Baseline</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:LinkButton ID="btnCancel" runat="server" CssClass=" btn btn-danger fa fa-times"> Clear Baseline</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-3"></div>
                                                            <div class="col-md-3"></div>
                                                        </div>
                                                       
                                                    </div><%--.col-md-4--%>
                                                </div><%-- .row--%>

                                            </div><%-- .panel-body--%>
                                        </div><%-- .panel-default--%>
                                    </div>
                                  </div> 
                               </div><!-- .row-->
                      </div> <%--baseline tabe--%>

                      <div  role="tabpanel"     class="tab-pane fade"      id="encounter">
                         <div class="row">
                             <div class="col-md-12"><h5 class="pull-left"><i class="fa fa-crosshairs fa-2x" aria-hidden="true"> Patient Encounter </i></h5></div>
                             <div class="col-md-12">
                                 <div class="wizard" data-initialize="wizard" id="myWizard">
                                    <div class="steps-container">
	                                    <ul class="steps">
		                                    <li data-step="1" data-name="template"  class="active">
			                                    <span class="badge">1</span>Triage
			                                    <span class="chevron"></span>
		                                    </li>
                                            <li data-step="2" data-name="campaign">
			                                    <span class="badge">2</span>Presenting Complaints
			                                    <span class="chevron"></span>
		                                    </li>
		                                    <li data-step="3">
			                                    <span class="badge">3</span>Patient Examination
			                                    <span class="chevron"></span>
		                                    </li>
		                                    <li data-step="4" data-name="template">
			                                    <span class="badge">4</span>Patient Management
			                                    <span class="chevron"></span>
		                                    </li>
	                                    </ul>
                                    </div>
                                    <div class="actions">
	                                    <button type="button" class="btn btn-default btn-prev">
		                                    <span class="glyphicon glyphicon-arrow-left"></span>Prev</button>
	                                    <button type="button" class="btn btn-primary btn-next" data-last="Complete">Next
		                                    <span class="glyphicon glyphicon-arrow-right"></span>
	                                    </button>
                                    </div>
                                    <div class="step-content">
	                                    <div class="step-pane active sample-pane " data-step="1">
		                                    
                                            <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT TRIAGE</strong></small></div> <div class="col-md-12"><hr /> </div>                                                   
                                            <div class="row">
                                                <div class="col-md-1">
                                                    <h4 class="pull-left text-primary"><i class="fa fa-heartbeat fa-5x" aria-hidden="true"></i></h4>
                                                </div>
                                                <div class="col-md-5 ">
                                                    <div class="col-md-12"><h1 class="text-primary pull-left"><small>Anthropometric Measurement</small></h1></div>
                                                    <div class="col-md-12"><hr /></div>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label for="Theight" class="control-label col-md-12">*Height (Cms)</label>
                                                                <div class="col-md-12">
                                                                    <input runat="server" type="text" id="Theight" class="form-control input-sm" placeholder="height" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                             <div class="form-group">
                                                                <label for="Tweight" class="control-label col-md-12">*Weight (Kgs)</label>
                                                                <div class="col-md-12">
                                                                    <input runat="server" type="text" id="Tweight" class="form-control input-sm" placeholder="weight" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                             <div class="form-group">
                                                                <div class="col-md-12"><label for="Tbmi" class="control-label pull-left">* BMI</label></div>
                                                                <div class="col-md-12">
                                                                    <input runat="server" type="text" id="TBMI" class="form-control input-sm" placeholder="bmi" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                             <div class="form-group">
                                                                <label for="THeadCircumference" class="control-label col-md-12">Circumference(cm)</label>
                                                                <div class="col-md-12">
                                                                    <input runat="server" type="text" id="THeadCircumference" class="form-control input-sm" placeholder="head" />
                                                                </div>
                                                             </div>
                                                        </div>


	                                                 </div>

                                                    <div class="row" style="padding-top:3%;padding-bottom:3%">
                                                        <div class="col-md-3">
                                                             <div class="form-group">
                                                                <label for="TMUAC" class="control-label col-md-12 pull-left">* MUAC (cms)</label>
                                                                <div class="col-md-12">
                                                                    <input runat="server" type="text" id="TMUAC" class="form-control input-sm" placeholder="muac" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-3">
                                                             <div class="form-group">
                                                                <div class="col-md-12"><label for="Tzscore" class="control-label col-md-12 pull-left">Z-Score</label></div>
                                                                <div class="col-md-12">
                                                                    <input runat="server" type="text" id="Tzscore" class="form-control input-sm" placeholder="z-score" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12"><hr /></div>
                                                    <div class="col-md-12"><h1 class="text-primary pull-left"> Vital Signs </h1></div>

                                                    <div class="row" style="padding-top:5%;padding-bottom:3%">
                                                         <div class="col-md-5">
                                                              <div class="form-group">
                                                                   <div class="col-md-12"><label for="Tbp" class="control-label pull-left">BP(mm/Hg)</label></div>
                                                                   <div class="col-md-5" style="padding-right:1px"><input runat="server" type="text" id="Tmm" class="form-control input-sm" placeholder="mm" /></div>
                                                                   <div class="col-md-2" style="padding-left:1px;padding-right:1px"><label class="control-label">/</label></div>
                                                                   <div class="col-md-5" style="padding-left:1px"><input runat="server" type="text" id="THg" class="form-control input-sm" placeholder="Hg" /></div>
                                                              </div>
                                                         </div>
                                                        <div class="col-md-3">
                                                             <div class="form-group">
                                                                 <div class="col-md-12"><label for="temp" class="control-label pull-left">Temperature(C)</label></div>
                                                                 <div class="col-md-12">
                                                                     <input runat="server" type="text" id="temp" class="form-control input-sm" placeholder="temp" />
                                                                 </div>
                                                             </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                             <div class="form-group">
                                                                 <div class="col-md-12"><label for="Trr" class="control-label pull-left">Respiratory Rate</label></div>
                                                                 <div class="col-md-12">
                                                                     <input runat="server" type="text" id="Trr" class="form-control input-sm" placeholder="bpm" />
                                                                 </div>
                                                             </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                             <div class="form-group">
                                                                 <div class="col-md-12"><label for="THeartRate" class="control-label pull-left">Heart Rate (bpm)</label></div>
                                                                 <div class="col-md-12">
                                                                      <input runat="server" type="text" id="THeartRate" class="form-control input-sm" placeholder="bpm" />
                                                                 </div>
                                                             </div>
                                                        </div>
                                                        <div class="col-md-3">

                                                        </div>
                                                        <div class="col-md-4">
                                                             <div class="form-group">
                                                                 <div class="col-md-12"><label for="TSPO2" class="control-label pull-left">SPO2 %</label></div>
                                                                 <div class="col-md-12">
                                                                      <input runat="server" type="text" id="TSPO2" class="form-control input-sm" placeholder="spo2" />
                                                                 </div>
                                                             </div>
                                                        </div>
                                                    </div>

                                                </div><%-- .col-md-5--%>
                                                <div class="col-md-6">
                                                    <div class="col-md-12"><h1 class="text-primary pull-left"><small>Known Allergies</small></h1></div>
                                                    <div class="col-md-12"><hr /></div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <div class="col-md-12"><label for="" class="control-label pull-left">Known Allergy(s)</label></div>
                                                                <div class="col-md-12">
                                                                    <input type="text" id="allergy" class="form-control input-sm" placeholder="known allergy" /> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <div class="col-md-12"><label for="allergyOnsetDate" class="control-label pull-left">Allergy Onset Date</label></div> 
                                                                <div class="col-md-12">
                                                                    <div class="datepicker fuelux" id="OnsetDate">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="allergyOnsetDate" type="text" />
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
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <div class="col-md-12"><label for="btnAddAllergy" class="control-label pull-right"> <span class=" fa fa-cog"> Action</span>  </label></div>
                                                                <div class="col-md-12">
                                                                    <asp:LinkButton runat="server" id="btnAddAllergy" CssClass="btn btn-info fa fa-plus pull-right" Text="Add Allergy"></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding-top:3%">
                                                        <div class="col-md-12 form-group">
                                                            <div class="col-md-12"><label for="" class="control-label pull-left">Nurse Note(s)</label></div>
                                                            <div class="col-md-12">
                                                                <textarea id="NurseNotes" class="form-control" rows="3" runat="server"></textarea>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div><%-- .col-md-5--%>
		                                    </div>

                                        </div><%-- data-step1--%>

	                                    <div class="step-pane sample-pane" data-step="2">
		                                    <div class="col-md-12"><small class="muted pull-left"><strong>Presenting Complaints </strong></small></div> <div class="col-md-12"><hr /> </div>  

                                            <div class="row">
                                                <div class="col-md-1">
                                                    <h4 class="pull-left text-danger"><i class="fa fa-user-md fa-5x" aria-hidden="true"></i></h4>
                                                </div>
                                                <div class="col-md-5">
                                                     <div class="col-md-12"><h1 class="text-primary pull-left"><small>Complaints & History of Complaints</small></h1></div>
                                                     <div class="col-md-12"><hr /></div>
                                                      <div class="row">
                                                        
                                                           <div class="col-md-12 form-group">

                                                                  <div class="col-md-12"><label class="control-label pull-left" for="complaints">Complaints Today</label></div>
                                                                  <div class="col-md-12">
                                                                      <textarea id="complaints" class="form-control input-sm" rows="4"></textarea>
                                                                  </div>
                                                           </div>
                                                    </div>
                                                    <div class="row">
                                                         
                                                             <div class="col-md-6  form-group">
                                                                 <div class="col-md-12"><label class="control-label pull-left input-sm" for="TBScreening">TB Screening</label></div>
                                                                 <div class="col-md-12">
                                                                     <select id="TBScreening" class="form-control"></select>
                                                                 </div>
                                                             </div>
                                                             <div class="col-md-6">
                                                                 <div class="col-md-12"><label class="control-label pull-left input-sm" for="NutritionStatus">Nutrition Status</label></div>
                                                                 <div class="col-md-12">
                                                                     <select id="NutritionStatus" class="form-control"></select>
                                                                 </div>
                                                             </div>
                         
                                                    </div>
                                                </div>
                                                a<div class="col-md-6">
                                                     <div class="col-md-12"><h1 class="text-primary pull-left">Adverse Event(s)</h1></div>
                                                     <div class="col-md-12"><hr /></div>
                                                     <div class="row">
                                                         <div class="col-md-3">
                                                              <div class="col-md-12"><label class="control-label pull-left" ><small>Adverse event(s)</small></label></div>
                                                             <div class="col-md-12">
                                                                 <input type="text" id="adverseEvent" class="form-control input-sm" runat="server" />
                                                             </div>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <div class="col-md-12"><label class="control-label" >Medicine Causing a/e</label></div>
                                                             <div class="col-md-12">
                                                                 <input type="text" id="medicineCauseingae" class="form-control input-sm" runat="server" />
                                                             </div>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <div class="col-md-12"><label class="control-label pull-left" >Severity</label></div>
                                                             <div class="col-md-12">
                                                                 <input type="text" id="severity" class="form-control input-sm" runat="server" />
                                                             </div>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <div class="col-md-12"><label class="control-label pull-left" >Action</label></div>
                                                             <div class="col-md-12">
                                                                 <input type="text" id="adverseAction" class="form-control input-sm" runat="server" />
                                                             </div>
                                                         </div>
                                                     </div>
                                                     <div class="row" style="padding-top:3%">
                                                         <div class="col-md-3"></div>
                                                         <div class="col-md-3"></div>
                                                         <div class="col-md-3"></div>
                                                         <div class="col-md-3">
                                                             <div class="col-md-12">
                                                                 <asp:LinkButton runat="server" ID="btnAdverseEventsAdd" CssClass="btn btn-info btn-lg fa fa-plus-circle">Add Adverse Event</asp:LinkButton>
                                                             </div>
                                                         </div>
                                                     </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-11">
                                                    
                                                </div>
                                            </div>
                                            
                                            <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-11">
                                                   <div class="col-md-12">
                                                     <div class="panel panel-default">
                                                     <div class="panel-heading">Patient Diagnosis Today</div>
                                                     <div class="panel-body">
                                                         <div class="row">
                                                             <div class="col-md-4">
                                                                  <div class="col-md-12 form-group">
                                                                      <div class="col-md-5"><label class="control-label  pull-left">Female LMP</label></div>
                                                                      <div class="col-md-7">
                                                                          <div class="datepicker fuelux" id="FemaleLMP">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="lmp" type="text" />
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
                                                                      <div class="col-md-5"><label class="control-label  pull-left">Pregnancy Status</label></div>
                                                                     <div class="col-md-7">
                                                                         <select class="form-control input-sm" id="pregnancyStatus" runat="server" ></select>
                                                                     </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group">
                                                                     <div class="col-md-5"><label class="control-label  pull-left">EDCD</label></div>
                                                                     <div class="col-md-7">
                                                                         <div class="datepicker fuelux" id="EDCD">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="ExpectedDateOfChildBirth" type="text" />
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
                                                             <div class="col-md-4">
                                                                  <div class="col-md-12 form-group">
                                                                       <div class="col-md-5"><label class="control-label  pull-left">ANC/PNC Profile</label></div>
                                                                      <div class="col-md-7">
                                                                          <select id="ancpncProfile" class="form-control input-sm"></select>
                                                                      </div>
                                                                  </div>
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-5"><label class="control-label  pull-left">On Family Planning</label></div>
                                                                      <div class="col-md-7">
                                                                          <select id="OnFamilyPlanning" class="form-control input-sm"></select>
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group">
                                                                     <div class="col-md-5"><label class="control-label  pull-left">FP Method</label></div>
                                                                      <div class="col-md-7">
                                                                          <select id="FPMethod" class="form-control input-sm"></select>
                                                                      </div>
                                                                 </div>
                                                                 
                                                             </div>
                                                             <div class="col-md-4">
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-5"><label class="control-label  pull-left">CaCX Screeing</label></div>
                                                                      <div class="col-md-7">
                                                                          <select id="cacxscreen" class="form-control input-sm"></select>
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-5"><label class="control-label  pull-left">STI Screeing</label></div>
                                                                      <div class="col-md-7">
                                                                          <select id="stiscreen" class="form-control input-sm"></select>
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-5"><label class="control-label  pull-left">STI Partner Notification</label></div>
                                                                      <div class="col-md-7">
                                                                          <select id="STIPartnerNotification" class="form-control input-sm"></select>
                                                                      </div>
                                                                 </div>
                                                             </div>
                                                         </div>
                                                    </div><%-- .panel-body--%>
                                                </div>
                                                   </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-11">
                                                    <div class="col-md-12"><hr /></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-4">
                                                    <div class="col-md-12">
                                                       <div class="col-md-12 form-group"><label class="control-label pull-left">Antigen Today</label></div> 
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                 <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblbcg">
                                                                         <input class="sr-only" type="checkbox" id="BCG" value="option1"> <span class="checkbox-label"><strong> BCG </strong></span>
                                                                     </label>
                                                                </div>
                                                        
                                                                 <div class="form-group col-md-6">
                                                                    <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblpv">
                                                                      <input class="sr-only" type="checkbox" id="PV" value="option1"> <span class="checkbox-label"><strong>Polio Vaccine </strong> </span>
                                                                    </label>
                                                                </div>

                                                                 <div class="form-group col-md-6">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblpenta">
                                                                          <input class="sr-only" type="checkbox" id="penta" value="option1"> <span class="checkbox-label"><strong> Pentavallent </strong></span>
                                                                    </label>     
                                                                </div>

                                                                 <div class="form-group col-md-6">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblpcv">
                                                                         <input class="sr-only" type="checkbox" id="pcv" value="option1"> <span class="checkbox-label"> <strong>Pneumococcal </strong></span>
                                                                    </label>
                                                                </div>
                                                        
                                                                 <div class="form-group col-md-6">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblmeasles">
                                                                         <input class="sr-only" type="checkbox" id="measles" value="option1"> <span class="checkbox-label"> Measles </span>
                                                                    </label>
                                                                </div>
                                                            </div>
                                                           
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-11">
                                                            <div class="col-md-12">
                                                             </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div class="col-md-7">
                                                     <div class="row">
                                                        <div class="form-group col-md-12">
                                                            <div class="panel panel-default">
                                                            <div class="panel-heading">Add any vaccination/antigen give to Child</div>
                                                            <div class="panel-body">
                                                               
                                                                 <div class="col-md-12">
                                                                      <div class="form-group col-md-3">
                                                                            <div class="col-md-12"><label class="control-label pull-left">Antigen</label></div>
                                                                            <div class="col-md-12">
                                                                                <select runat="server" id="AntigenToday" class="form-control input-sm"></select>
                                                                            </div>
                                                                        </div>
                                                                      <div class="col-md-3">
                                                                              <div class="col-md-12"><label class="control-label pull-left">Duration(wks)</label></div>
                                                                              <div class="col-md-12">
                                                                                    <select runat="server"  id="antigenDuration" class="form-control input-sm"></select>
                                                                              </div>
                                                                        </div>
                                                                      <div class="col-md-4">
                                                                            <div class="col-md-12"><label class="control-label pull-left">Date Given</label></div>
                                                                            <div class="col-md-12">
                                                                                <div class="datepicker fuelux" id="AntigenDate">
                                                                                      <div class="input-group">
                                                                                          <input class="form-control input-sm" id="DateGiven" type="text" />
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
                                                                      <div class="col-md-2">
                                                                    <div class="col-md-12"><label class="control-label pull-right fa fa-cog">Action</label></div> 
                                                                    <div class="col-md-12">
                                                                          <asp:LinkButton runat="server" ID="btnAmtigenAdd" CssClass="btn btn-info btn-lg fa fa-plus-circle">Add Antigen</asp:LinkButton>
                                                                     </div>
                                                                </div>
                                                                 </div>
                                                                
                                                            </div>
                                                        </div>
                                                            
                                                           <%-- <div class="col-md-12 form-group"><label class="control-label pull-left"> Add any vaccination/antigen give to Child</label></div> --%>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><%--.row--%>

                                            <div class="row">
                                                
                                                <div class="col-md-1"></div>
                                                 <div class="col-md-11">
                                                     <div class="col-md-12"><hr /></div> 
                                                     <div class="col-md-12 form-group"><label class="control-label pull-left">Chronic Illnesses & Comorbidities</label></div>
                                                     <div class="row">
                                                         <div class="col-md-12">
                                                              <div class="col-md-5 form-group">
                                                                   <div class="col-md-12"><label class="control-label pull-left">Illness</label></div>
                                                                  <div class="col-md-12">
                                                                      <select runat="server" id="ChronicIllness" class="form-control input-sm"></select>
                                                                  </div>
                                                              </div>

                                                              <div class="col-md-3 form-group">
                                                                   <div class="col-md-12"><label class="control-label pull-left">Current Treatment</label></div>
                                                                   <div class="col-md-12">
                                                                       <input runat="server" class="form-control input-sm" id="ChronicIllnessTreatment" />
                                                                   </div>
                                                              </div>

                                                             <div class="col-md-2 form-group">
                                                                 <div class="col-md-12"><label class="control-label pull-left"> Dose</label></div>
                                                                 <div class="col-md-12">
                                                                     <select runat="server" class="form-control input-sm" id="ChronicIllnessDose"></select>
                                                                 </div>
                                                             </div>
                                                             <div class="col-md-1 form-group">
                                                                  <div class="col-md-12"><label class="control-label pull-left"> Duration</label></div>
                                                                 <div class="col-md-12">
                                                                     <input type="text" min="0" max="180" class="form-control" runat="server" />
                                                                 </div>
                                                             </div>

                                                             <div class="col-md-1">
                                                                 <div class="col-md-12"><label class="control-label pull-left"><span class="fa fa-cog">Action</span></label></div>
                                                                 <div class="col-md-4">
                                                                     <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-info btn-lg fa fa-plus-circle">Add</asp:LinkButton>
                                                                 </div>
                                                             </div>
                                                         </div>
                                                         
                                                     </div>
                                                 </div>
                                            </div>


	                                    </div>
	                                    
                                        <div class="step-pane sample-pane" data-step="3">
                                             <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Examination</strong></small></div> <div class="col-md-12"><hr /> </div>  
                                             <div class="row">
                                                 <div class="col-md-1">
                                                     <h4 class="pull-left text-warning"><i class="fa fa-search fa-5x" aria-hidden="true"></i></h4>
                                                 </div>
                                                 <div class="col-md-11">
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><h1 class="text-primary pull-left"><small>Physical Examination</small> </h1></div>
                                                         <div class="col-md-12"><hr /></div>

                                                         <div class="col-md-12 form-group">
                                                             <label class="control-label pull-left">Indicate Physical Examination Findings below </label>
                                                         </div>

                                                         <div class="form-group col-md-12">
                                                              <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblpallor">
                                                                    <input class="sr-only" type="checkbox" id="pallor" value="option1"> <span class="checkbox-label"><strong> Pallor </strong> </span>
                                                            </label>
                                                         </div>
                                                         <div class="form-group col-md-12">
                                                             <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblJaundice">
                                                                 <input class="sr-only" type="checkbox" id="Jaundice" value="option1"> <span class="checkbox-label"><strong> Jaundice </strong> </span>
                                                            </label>
                                                         </div>
                                                         <div class="form-group col-md-12">
                                                             <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblOedema">
                                                                 <input class="sr-only" type="checkbox" id="Oedema" value="option1"> <span class="checkbox-label"><strong> Oedema </strong> </span>
                                                            </label>
                                                         </div>
                                                         <div class="form-group col-md-12">
                                                             <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblOralThhrush">
                                                                 <input class="sr-only" type="checkbox" id="OralThrush" value="option1"> <span class="checkbox-label"><strong> Oral Thrush </strong> </span>
                                                            </label>
                                                         </div>
                                                     </div>
                                                     <div class="col-md-6">
                                                          <div class="col-md-12"><h1 class="text-primary pull-left"><small>Physical Examination Notes</small> </h1></div>
                                                         <div class="col-md-12"><hr /></div>
                                                         <div class="row">
                                                             <div class="col-md-12 form-group">
                                                                 <label class="control-label pull-left">Examination Notes on further findings</label>
                                                            </div>
                                                             <div class="col-md-12">
                                                                 <textarea runat="server" class="form-control input-sm" id="examinationNotes" rows="6"></textarea>
                                                             </div>
                                                         </div>
                                                     </div>
                                                 </div>
                                             </div>
                                             
	                                    </div>

                                        <div class="step-pane sample-pane" data-step="4">
                                             <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT MANAGEMENT</strong></small></div> <div class="col-md-12"><hr /> </div>  
                                             <div class="row">
                                                  <div class="col-md-1">
                                                      <h4 class="pull-left text-danger"><i class="fa fa-bed fa-5x" aria-hidden="true"></i></h4>
                                                 </div>

                                                  <div class="col-md-11">
                                                  
                                                       <div class="col-md-4">
                                                       <h1 class="col-md-12"><small class="pull-left">Positive Health,Dignity & Prevention (PHDP)</small></h1>
                                                       <div class="col-md-12"><hr /></div>
                                                            
                                                            <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblAdc">
                                                                         <input class="sr-only" type="checkbox" id="adc" value="option1"> <span class="checkbox-label"><strong> Adherance Counselling (Adc) </strong></span>
                                                                     </label>
                                                                  </div>

                                                            <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblcondom">
                                                                         <input class="sr-only" type="checkbox" id="CondonUse" value="option1"> <span class="checkbox-label"><strong> Condom Distribution (CD) </strong></span>
                                                                     </label>
                                                                  </div>

                                                            <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblsubstanceAbuse">
                                                                         <input class="sr-only" type="checkbox" id="SubstanceAbuse" value="option1"> <span class="checkbox-label"><strong> Substance Abuse (SA) </strong></span>
                                                                     </label>
                                                                  </div>

                                                            <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lbldsp">
                                                                         <input class="sr-only" type="checkbox" id="DisclosureToSexPartner" value="option1"> <span class="checkbox-label"><strong> Disclosure to Sex Partner (Disc) </strong></span>
                                                                     </label>
                                                                  </div>

                                                            <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPartnerTesting">
                                                                         <input class="sr-only" type="checkbox" id="Partner Testing" value="option1"> <span class="checkbox-label"><strong> Partner Testing (PT) </strong></span>
                                                                     </label>
                                                                  </div>

                                                             <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblstiscreening">
                                                                         <input class="sr-only" type="checkbox" id="STIScreening" value="option1"> <span class="checkbox-label"><strong> STI Screening (STI)</strong></span>
                                                                     </label>
                                                                  </div>

                                                             
                                                  </div>

                                                       <div class="col-md-8">
                                                            
                                                            <h1 class="col-md-12"><small class="pull-left"> Patient Diagnosis and Treatment</small></h1>
                                                            <div class="col-md-12"><hr /></div>
                                                            
                                                            <div class="col-md-12">
                                                                 <div class="col-md-7"><label class="control-label pull-left">Diagnosis (ICD 10 Codes)</label></div>
                                                                 <div class="col-md-3"><label class="control-label pull-left">Treatment</label></div>
                                                                 <div class="col-md-2"><label class="control-label pull-left">Action</label></div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                 <div class="col-md-7 form-group">
                                                                      <input type="text" id="Diagnosis" class ="form-control input-sm" placeholder="Type Diagnosis......" runat="server" />
                                                                 </div>
                                                                
                                                                 <div class="col-md-3 form-group">
                                                                     <input type="text" id="DiagnosisTreatment" class ="form-control input-sm" placeholder="treatment" runat="server" />
                                                                 </div>
                                                                 
                                                                <div class="col-md-2 form-group">
                                                                      <asp:LinkButton runat="server" ID="btnAddDiagnosis" CssClass="btn btn-info btn-lg fa fa-plus-circle"> Add</asp:LinkButton>
                                                                 </div>
                                                                
                                                            </div>

                                                            <div class="col-md-12">
                                                                 <div class="col-md-12">
                                                                      <div class="panel panel-default">
			                                                          <div class="panel-heading">Patient Diagnosis Today</div>
			                                                              <div class="panel-body">
		   
			                                                              </div>
                                                                      </div>
                                                                 </div> 
                                                            </div>
                                                 </div>
                                                   
                                                  </div><%-- .col-md-11--%>
                                                    
                                             </div>
                                             
                                             <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-11">
                                                     <div class="col-md-4">
                                                          <div class="col-md-12"><hr /></div>
                                                          <div class="col-md-12">

                                                               <div class="col-md-8">
                                                                  <div class="col-md-12"><label class="control-label pull-left">ARV Adherence</label></div>
                                                              </div>

                                                               <div class="col-md-4">
                                                                  <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblGood">
                                                                         <input class="sr-only" type="checkbox" id="ARVGood" value="option1"> <span class="checkbox-label"><strong> Good </strong></span>
                                                                     </label>
                                                                  </div>

                                                                  <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblFair">
                                                                         <input class="sr-only" type="checkbox" id="ARVFair" value="option1"> <span class="checkbox-label"><strong> Fair </strong></span>
                                                                     </label>
                                                                  </div>

                                                                  <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblPoor">
                                                                         <input class="sr-only" type="checkbox" id="ARVPoor" value="option1"> <span class="checkbox-label"><strong> Poor </strong></span>
                                                                     </label>
                                                                  </div>
                                                              </div>
                                                          </div><%-- .col-md-12--%>
                                                          <div class="col-md-12">
                                                             <div class="col-md-8">
                                                                  <div class="col-md-12"><label class="control-label pull-left">CTX/Dapsone Adherence</label></div>
                                                              </div>
                                                             <div class="col-md-4">
                                                                  <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblCTXGood">
                                                                         <input class="sr-only" type="checkbox" id="CTXGood" value="option1"> <span class="checkbox-label"><strong> Good </strong></span>
                                                                     </label>
                                                                  </div>

                                                                  <div class="form-group col-md-12">
                                                                     <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblCTXFair">
                                                                         <input class="sr-only" type="checkbox" id="CTXFair" value="option1"> <span class="checkbox-label"><strong> Fair </strong></span>
                                                                     </label>
                                                                  </div>

                                                                  <div class="form-group col-md-12">
                                                                         <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblCTXPoor">
                                                                             <input class="sr-only" type="checkbox" id="CTXPoor" value="option1"> <span class="checkbox-label"><strong> Poor </strong></span>
                                                                         </label>
                                                                      </div>
                                                             </div>
                                                         </div>

                                                          <div class="col-md-12">
                                                              <div class="col-md-12"><hr /></div>
                                                              <div class="col-md-12">
                                                                   <div class="col-md-5"><label class="control-label pull-left">Next Appointment </label></div>
                                                                   <div class="col-md-7">
                                                                        <div class="datepicker fuelux form-group" id="NextAppDate">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="NextAppointmentDate" type="text" />
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
                                                                   <div class="col-md-5"><label class="control-label pull-left">Referred for </label></div>
                                                                   <div class="col-md-7 form-group">
                                                                       <input type="text" class="form-control input-sm" id="ReferredFor" placeholder="referred for" runat="server" />
                                                                   </div>
                                                              </div>
                                                              
                                                              <div class="col-md-12">
                                                                  <div class="col-md-5"><label class="control-label pull-left">Referred for </label></div>
                                                                  <div class="col-md-7 form-group">
                                                                      <select class="form-control input-sm" id="PrescribedBy" runat="server"></select>
                                                                  </div>
                                                              </div>
                                                          </div>
                                                     </div><%-- .col-md-4--%>
                                                     <div class="col-md-8">
                                                         <div class="col-md-12"><hr /></div>
                                                         <div class="col-md-12">
                                                              <div class="form-group col-md-4">
                                                                   <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblART">
                                                                          <input class="sr-only" type="checkbox" id="DrugsART" value="option1"> <span class="checkbox-label"><strong> ART </strong></span>
                                                                   </label>
                                                              </div>
                                                                   
                                                              <div class="form-group col-md-4">
                                                                   <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblNonART">
                                                                           <input class="sr-only" type="checkbox" id="DrugsARV" value="option1"> <span class="checkbox-label"><strong> Non-ART </strong></span>
                                                                   </label>
                                                              </div>

                                                               <div class="form-group col-md-4">
                                                                    <label class="checkbox-custom checkbox-inline pull-left" data-initialize="checkbox"  id="lblProphylaxis">
                                                                           <input class="sr-only" type="checkbox" id="Prophylaxis" value="option1"> <span class="checkbox-label"><strong> Prophylaxis </strong></span>
                                                                     </label>
                                                               </div>
                                                         </div> <%--.col-md-12--%>

                                                         <div class="col-md-12">
                                                             <hr />
                                                              <div class="col-md-4 form-group"><label class="control-label pull-left">Regimen Line </label></div>     
                                                              <div class="col-md-4 form-group">
                                                                   <select id="RegimenLine" runat="server" class="form-control input-sm" ></select>
                                                              </div>
                                                              <div class="col-md-4 form-group"></div>
                                                         </div>  
                                                         <div class="col-md-12">  
                                                              <div class="col-md-4 form-group"><label class="control-label pull-left">Subsitututions/Switches/Interuptions </label></div>
                                                              <div class="col-md-4 form-group">
                                                                   <select id="substitution" runat="server" class="form-control input-sm" ></select>
                                                              </div>
                                                              <div class="col-md-4"></div>

                                                         </div>
                                                             
                                                         <div class="col-md-12">
                                                              <div class="panel panel-default">
                                                          <div class="panel-heading">Drug Prescription </div>
                                                          <div class="panel-body">
                                                               <div class="row">
                                                                    <div class="col-md-12">
                                                                         <div class="col-md-10">
                                                                         <div class="col-md-12"><label class="control-label pull-left">Select Drug (s) </label></div>
                                                                         <div class="col-md-12">
                                                                              <input type="text" class="form-control input-sm" placeholder="type to search...." runat="server" />
                                                                         </div>
                                                                     </div>
                                                                                   
                                                                         <div class="col-md-2">
                                                                          <div class="col-md-12"><label class="control-lable pull-left">*</label></div>
                                                                          <div class="col-md-12">
                                                                               <asp:LinkButton runat="server" CssClass="btn btn-info btn-lg fa fa-plus-circle"> Add Drug</asp:LinkButton> 
                                                                          </div>
                                                                    </div>
                                                                    </div>
                                                                    <div class="col-md-12"><hr /></div>
                                                                    <div class="col-md-12">
                                                                         <div class="col-md-4 pull-left"><label class="control-label pull-left"> Drug</label></div>
                                                                         <div class="col-md-2 pull-left"><label class="control-label pull-left"> Dose</label></div>
                                                                        <div class="col-md-2 pull-left"><label class="control-label pull-left">  Frequency</label></div>
                                                                         <div class="col-md-2 pull-left"><label class="control-label pull-left"> Quantity</label></div>
                                                                         <div class="col-md-2 pull-left"><label class="control-label pull-left"> Duration</label></div>
                                                                    </div>  
                                                                    <div class="col-md-12">
                                                                       <div class="col-md-4"><label class="control-label pull-left text-primary">--Selected drug--</label></div>
                                                                        <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="Dose" /> </div>
                                                                        <div class="col-md-2"><select class="form-control input-sm" id="Frequency" runat="server"></select></div>
                                                                       <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="Quantity" /> </div>
                                                                       <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="Duration" /> </div>
                                                                    </div>
                                                                 </div>
                                                           </div>
                                                     </div>
                                                         </div>            
                                                     </div>
                                                 </div>
                                               
                                            </div> <%--.row--%>

 
                                        </div><%-- .data-step-4--%>
                                 </div><%-- .wizard--%>
                             </div> <%--.col-md-12--%>
                         </div>
                      </div><!-- .encounter-->
                      <div  role="tabpanel"     class="tab-pane fade"      id="vlTracker">...</div><!-- .viraload tracker-->
                      <div  role="tabpanel"     class="tab-pane fade"      id="Laboratory">...</div><!-- .laboratory-->
                      <div  role="tabpanel"     class="tab-pane fade"      id="Pharmacy">...</div><!-- .pharmacy-->
                       <div  role="tabpanel"    class="tab-pane fade"      id="history">...</div><!-- .history-->
                 </div><!-- .tab-content-->
            </div><!-- .col-md-12 -->
        </div><!-- .row -->
    </div><!-- .container-fluid -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ARTStartDate').datepicker();
            $('#TIDate').datepicker();
            $('#DHID').datepicker();
            $('#DOE').datepicker();
            $('#DARTI').datepicker();
            $('#OnsetDate').datepicker();
            $('#FemaleLMP').datepicker();
            $('#EDCD').datepicker();
            $('#AntigenDate').datepicker();
            $('#NextAppDate').datepicker();
            $('#myWizard').wizard();
        });

    </script>
</asp:Content>
