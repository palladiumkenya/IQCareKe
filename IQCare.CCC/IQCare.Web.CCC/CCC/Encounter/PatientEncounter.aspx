﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PatientEncounter" EnableEventValidation="false" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="uc" TagName="ucExtruder" %>
<%@ Register Src="~/CCC/UC/ucPharmacyPrescription.ascx" TagPrefix="uc" TagName="ucPharmacyPrescription" %>
<%@ Register Src="~/CCC/UC/ucPatientClinicalEncounter.ascx" TagPrefix="uc" TagName="ucPatientClinicalEncounter" %>




<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../Scripts/js/PatientEncounter.js"></script>
    
     <div class="col-md-12">
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    </div>
    <div class="col-md-12 col-xs-12">

        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#encounter" aria-controls="encounter" role="tab" data-toggle="tab"><i class="fa fa-exchange fa-lg" aria-hidden="true"></i>Clinical Encounter</a></li>
            <li role="presentation"><a href="#vlTracker" aria-controls="vlTracker" role="tab" data-toggle="tab"><i class="fa fa-line-chart fa-lg" aria-hidden="true"></i>Viraload Tracker</a></li>
            <li role="presentation"><a href="#Laboratory" aria-controls="Laboratory" role="tab" data-toggle="tab"><i class="fa fa-flask fa-lg" aria-hidden="true"></i>Laboratory</a></li>
            <li role="presentation"><a href="#Pharmacy" aria-controls="Pharmacy" role="tab" data-toggle="tab"><i class="fa fa-tint fa-lg" aria-hidden="true"></i>Pharmacy</a></li>
        </ul>
    </div>
    <!-- .col-md-12 -->

    <div class="col-md-12 col-xs-12">

        <div class="tab-content">

            <div role="tabpanel" class="tab-pane active" id="encounter">
                <uc:ucPatientClinicalEncounter runat="server" id="ucPatientClinicalEncounter" />
            </div>

            <div role="tabpanel" class="tab-pane fade" id="vlTracker">
                <!-- pw implementation of viral load tracker here-->

                <div class="col-md-6">
                    <div class="col-md-12 bs-callout bs-callout-danger">
                        <h4 class="pull-left"><strong>Pending VL Test(s):</strong> </h4>

                           
                            <table class="table table-striped table-condensed" id="tblVlpending" clientidmode="Static" runat="server">
                                                 <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">VL Test</span></th>
                                                                    <th><span class="text-primary">Test Reason</span></th>
                                                                    <th><span class="text-primary">Test Date</span></th>
                                                                    <th><span class="text-primary">Status</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                <tbody>                        
                                                </tbody>                  
                                                </table>
                            </div>
                      
                         <div class="col-md-12 bs-callout bs-callout-info">
                                         <h4 class="pull-left"> <strong>Complete VL Test(s):</strong> </h4>    
                                              <table class="table table-striped table-condensed" id="tblVL" clientidmode="Static" runat="server">
                                                
                                                   <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">VL Test</span></th>
                                                                    <th><span class="text-primary">Test Reason</span></th>
                                                                    <th><span class="text-primary">Test Date</span></th>
                                                                    <th><span class="text-primary">Status</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                  
                                                   <tbody>                        
                                                  </tbody>                  
                                                </table>
                                            </div> 
                                               
                                    </div>
                       
                                   
                             <div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div> 
                                                            
            <%--       <div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div> --%>
                      <!-- pw .implementation of viral load tracker line graph here-->
                </div><!-- .viraload tracker-->
                    
             <div  role="tabpanel" class="tab-pane fade" id="Laboratory">
                            <%--<div class="col-md-12" style="padding-top: 1%">
                              <label class="control-label pull-left"> <i class="fa fa-flask fa-lg" aria-hidden="true"></i> Laboratory Prescription </label>
                            </div>    -->--%>
                            
                       
                           
                        <div class="col-md-6">  
                         <div class="col-md-12 bs-callout bs-callout-danger">
                                <h4 class="pull-left"> <strong>Pending Labs:</strong> </h4>                           
                                <table class="table table-striped table-condensed" id="tblPendingLabs" clientidmode="Static" runat="server">
                                    
                                                           <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">Lab Test</span></th>
                                                                    <th><span class="text-primary">Order Reason</span></th>
                                                                    <th><span class="text-primary">Order Date</span></th>
                                                                    <th><span class="text-primary">Status</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                    
                                                       <tbody>                        
                                                  </tbody>                  
                                                </table>
                                         <div class="col-md-3 pull-right ">
                                      <asp:LinkButton runat="server" ID="addResults" ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle "> Add Results</asp:LinkButton>

                                        </div>
                            </div>    
        
                

                <div class="col-md-12 bs-callout bs-callout-info">
                         <h4 class="pull-left"> <strong>Complete Labs:</strong> </h4>    
                      <!--pw implementation of previous labs laboratory module here  previous orders-->
                                        
                                        <div class="col-md-12 form-group">
                                              <table class="table table-striped table-condensed" id="tblPrevLabs" clientidmode="Static" runat="server">
                                               
                                                   <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">Lab Test</span></th>
                                                                    <th><span class="text-primary">Order Reason</span></th>
                                                                    <th><span class="text-primary">Order Date</span></th>
                                                                    <th><span class="text-primary">Results</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                  
                                                <tbody>                        
                                                </tbody>                  
                                                </table>

                        </div>
                    </div>

                       </div>
                           
                       <div class="col-md-6">
                         <div class="col-md-12">
                                        <div class="col-md-12"><label class="control-label pull-left">Order Lab Test(s)</label></div>
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                              <div class="panel-heading"></div>
                                              <div class="panel-body">
                                               
                                                  <div class="col-md-12 form-group">
                                                       <div class="col-md-4"><label class="control-label pull-left">Select Lab</label></div>
                                                      <div class="col-md-8">
                                                         
                                                          <asp:TextBox runat="server" Width="230" ID="labTestTypes" data-provide="typeahead" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="type to select...."></asp:TextBox>
                                                          
                                                      </div>
                                                  </div>
                                                  <div class="col-md-12 form-group">
                                                                      <div class="col-md-4"><label class="control-label  pull-left">Reason</label></div>
                                                                     <div class="col-md-8">
                                                                         <asp:DropDownList runat="server" ID="orderReason" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                     </div>
                                                         </div>
                                                  
                                                 <div class="col-md-12 form-group">
                                                       <div class="col-md-4"><label class="control-label pull-left">Lab Test Notes</label></div>
                                                      <div class="col-md-8">
                                                         
                                                          <asp:TextBox runat="server" ID="labNotes" Rows="4" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="laboratory test notes...."></asp:TextBox>
                                                      </div>
                                                  </div>                         



                                    <div class="col-md-12">
                                        <div class="col-md-10"></div>
                                        <div class="col-md-3 pull-right ">
                                            <asp:LinkButton runat="server" ID="btnAddLab" ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle "> Add Lab</asp:LinkButton>

                                        </div>
                                        <div></div>
                                    </div>
                                <div class="col-md-12 bs-callout bs-callout-info">
                                    <div class="col-md-12 form-group">
                                        <table class="table table-striped table-condensed" id="tblAddLabs" clientidmode="Static" runat="server">
                                            <thead>

                                                <tr>
                                                    <th><i class="control-label text-warning pull-left" aria-hidden="true"># </i></th>
                                                    <th><i class="control-label text-warning pull-left" aria-hidden="true">Lab Test</i> </th>
                                                    <th><i class="control-label text-warning pull-left " aria-hidden="true">Order Reason</i> </th>
                                                    

                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                    </div>
                                <!--here  -->                                        
                                     <!--datepicker  -->
                              
                                   <div class="col-md-12 form-group">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left">Date</label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="datepicker fuelux form-group" id="LabDatePicker">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="LabDate" data-parsley-required="true"></asp:TextBox>
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
                                                                        </span><span class="year">2014</span>
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
                                                       <div class="col-md-4"><label class="control-label pull-left">Clinical Order Notes</label></div>
                                                      <div class="col-md-8">
                                                         
                                                          <asp:TextBox runat="server" ID="orderNotes" Rows="4" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="laboratory order notes...."></asp:TextBox>
                                                      </div>
                                                  </div>
                                <!-- to here  -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-7"></div>
                <div class="col-md-5">
                    <div class="col-md-3">

                        <asp:LinkButton runat="server" ID="btnSaveLab" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle" ClientIDMode="Static"> Save Order</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnPrintOrder" CssClass="btn btn-primary fa fa-print" ClientIDMode="Static"> Print Order</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnResetOrder" OnClientClick="return false" CssClass="btn btn-warning fa fa-refresh" ClientIDMode="Static"> Reset Order</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnCancelOrder" OnClientClick="return false" CssClass="btn btn-danger fa fa-times" ClientIDMode="Static"> Cancel Order</asp:LinkButton>
                    </div>
                </div>

                <%--</div>--%>
            </div>
            <!-- .laboratory-->
            <!--end pw implementation of  laboratory module here-->

            <div role="tabpanel" class="tab-pane fade" id="Pharmacy">
                <uc:ucPharmacyPrescription runat="server" id="ucPharmacyPrescription" />
            </div><!-- .pharmacy-->

            <%--<div  role="tabpanel"    class="tab-pane fade"      id="history">
                           
                       </div>--%><!-- .history-->
            <%--</div>--%><!-- .tab-content-->
            <uc:ucExtruder runat="server" ID="ucExtruder" />

        </div>
    </div>
    <div class="modal"  id="AppointmentModal" tabindex="-1" role="dialog" aria-labelledby="Appointmentlabel" aria-hidden="true" clientidmode="Static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content" >
                <div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Appointment Details</h4>
                    </div>

                    <div class="modal-body">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Date</label>
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
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left">Service Area</label>
                                    </div>
                                    <div class="col-md-12 pull-right">
                                        <asp:DropDownList runat="server" ID="ServiceArea" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="reason" class="control-label pull-left">Reason</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="reason" class="control-label pull-left">Differentiated Care</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="DifferentiatedCare" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="description" class="control-label pull-left">Description</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="description" CssClass="form-control input-sm" ClientIDMode="Static" required="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label for="status" class="control-label pull-left">Status</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="status" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12">
                            <asp:LinkButton runat="server" ID="btnSaveAppointment" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Appointment </asp:LinkButton>                        
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- ajax begin -->
    <script type="text/javascript">
        var patientId = '<%=PatientId%>';
        var patientMasterVisitId = '<%=PatientMasterVisitId%>';    
        var ptn_pk = '<%=Ptn_pk%>';        
        var facilityId = '<%=AppLocationId%>';
        var moduleId = '<%=ModuleId%>';
      
        var jan_vl = "";
        var march_vl = "";
        
        $(document).ready(function () {           


            $("#LabDatePicker").datepicker({
                //date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            var LabOrderList = new Array();
        
            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetLookupPreviousLabsList",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];    
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                      
                        table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.ResultValues + '</td></tr>';
                   
                    });
                  
                    $('#tblPrevLabs').append(table);
                    $('#tblPrevLabs tr:not(:first-child').each(function(idx){
                        $(this).children(":eq(0)").html(idx + 1);
                    });


                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });

            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetLookupPendingLabsList",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                   // console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                     "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];                        
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                        // alert(date);
                        
                            table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';
                     
                    });

                  
                    $('#tblPendingLabs').append(table);
                    $('#tblPendingLabs tr:not(:first-child').each(function(idx){
                    $(this).children(":eq(0)").html(idx + 1);
                    });

                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });
            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetvlTests",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];    ;
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                        // alert(date);

                        table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.ResultValues + '</td></tr>';
                    });

                    $('#tblVL').append(table);
                    $('#tblVL tr:not(:first-child').each(function(idx){
                        $(this).children(":eq(0)").html(idx + 1);
                    });
                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });

            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetPendingvlTests",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    // console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];    
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                        // alert(date);

                        table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';
                    });

                    $('#tblVlpending').append(table);
                    $('#tblVlpending tr:not(:first-child').each(function(idx){
                        $(this).children(":eq(0)").html(idx + 1);
                    });
                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });

            var labtests = [];
            var input = document.getElementById("labTestTypes");
            var awesomplete = new Awesomplete(input, {
                minChars: 1,
                autoFirst: true
            });

            $("input").on("keyup", function () {
                $.ajax({
                    url: '../WebService/LookupService.asmx/GetLookupLabsList',
                    type: 'POST',
                    dataType: 'json',
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
           
                    success: function (data) {
                        var serverData = JSON.parse(data.d);
                        //console.log(serverData);
                        
                        var obj = [];
                        obj[key] = val;

                        //labtests.push(obj);
                        
                        for (var i = 0; i < serverData.length; i++) {
                            var key = parseInt(serverData[i]["Id"]);
                            var val = serverData[i]["Name"];

                            labtests[key] = val;
                        }

                        console.log(labtests);
                        awesomplete.list = labtests;
                    }
                });
           
            });
      
      
            function val2key (val, array){
                for(var key in array){
                    var this_val = array[key];
                    if(this_val == val){
                        return key;
                        break;
                    }
                }
            }
            // Load lab order   
            $("#btnAddLab").click(function (e) {

                var labOrderFound = 0;

                //var labOrderDate = $("#<%=LabDate.ClientID%>").val();               
                var labName = $("#labTestTypes").val();
                var labNameId=val2key(labName, labtests);              
                var labOrderReason = $("#orderReason").find(":selected").text();
                var labOrderNotes = $("#labNotes").val();

                if (labName < 1) {
                    toastr.error("Please select at least One(1) Lab Type from the List");
                    return false;
                }
                if (labOrderReason < 1) {
                    toastr.error("Please select at least One(1) Lab Order Reason from the List");
                    return false;
                }

                labOrderFound = $.inArray("" + labName + "", LabOrderList);

                if (labOrderFound > -1) {

                    toastr.error("error", labName + " Lab selected already exists in the List");
                    return false; // message box herer
                }
               

                else {

                  
                    LabOrderList.push("" + labName + "");
                    var tr = "<tr><td></td><td visibility: hidden>" + labNameId + "</td><td align='left'>" + labName + "</td><td align='left'>" + labOrderReason + "</td><td visibility: hidden>" + labOrderNotes + "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                    $("#tblAddLabs>tbody:first").append('' + tr + '');
                    resetLabOrder();

                    $('#tblAddLabs tr:not(:first-child').each(function(idx){
                        $(this).children("td:eq(0)").html(idx + 1);
                    }); 
                }

                e.preventDefault();
            });

            $("#tblAddLabs").on('click', '.btnDelete', function () {
                $(this).closest('tr').remove();
                var x = $(this).closest('tr').find('td').eq(0).html();

               
            });
       
            $("#btnCancelOrder").click(function (e) {
                $("#tblAddLabs td").parent().remove();
            });
      
            $("#btnResetOrder").click(function (e) {   
                resetLabOrder();
            });
        
            function resetLabOrder(parameters) {
                $("#labTestTypes").val("");
                $("#orderReason").val("");
                $("#labNotes").val("");              
            }
        

            // Save lab order
            $("#btnSaveLab").click(function (e) {
                var labOrderDate = $("#<%=LabDate.ClientID%>").val();   
                var orderNotes = $("#orderNotes").val();
                var _fp = [];
                var table  = "";              
                var data = $('#tblAddLabs tr').each(function (row, tr) {
                    _fp[row] = {
                                "labNameId": $(tr).find('td:eq(1)').text()
                               , "labName": $(tr).find('td:eq(2)').text()
                              , "orderReason": $(tr).find('td:eq(3)').text()                             
                             , "labNotes": $(tr).find('td:eq(4)').text()

                            }                  
                    
                });
                _fp.shift();

                if ($.isEmptyObject(_fp)) {
                    toastr.error("You have not added any lab order");
                 
                    return false;
                } else {
                  
                    addLabOrder(_fp,labOrderDate,orderNotes);       
                  
                                      
                    $('#tblAddLabs tr:first').remove();
                    var data = $('#tblAddLabs tr').each(function (row, tr) {
                        _fp[row] = {
                                "labNameId": $(tr).find('td:eq(1)').text()
                               , "labName": $(tr).find('td:eq(2)').text()
                              , "orderReason": $(tr).find('td:eq(3)').text()                            
                             , "labNotes": $(tr).find('td:eq(4)').text()

                        }                      
                    
                        table ="<tr><td></td><td>" + $(tr).find('td:eq(2)').text() + "</td><td>" + $(tr).find('td:eq(3)').text() + "</td><td>" +labOrderDate+ "</td><td>" + "Pending" + "</td></tr>";
                        $("#tblPendingLabs>tbody:first").append(table);              

                        $('#tblPendingLabs tr:not(:first-child').each(function(idx){
                            $(this).children("td:eq(0)").html(idx + 1);
                        }); 

                    }); 

                }

              
                $("#tblAddLabs td").parent().remove();
            });

            function addLabOrder(_fp,labOrderDate,orderNotes) {
                var labOrder = JSON.stringify(_fp);
                //console.log(labOrderDate);
                //console.log(orderNotes);
                $.ajax({
                    type: "POST",

                    url: "../WebService/LabService.asmx/AddLabOrder",
                    data: "{'patientPk':'" + ptn_pk + "','patientMasterVisitId':'" + patientMasterVisitId + "','labOrderDate':'" + labOrderDate + "','orderNotes':'" + orderNotes + "','patientLabOrder': '" + labOrder + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        toastr.success(response.d, "Lab order successful");
                    },
                    
                });

            };

            // Load lab results        
            $("#addResults").click(function (e) {
                window.location.href = '<%=ResolveClientUrl("~/laboratory/request/findlaborder.aspx")%>'; 
              
            });           
           
         

            $('#PersonAppointmentDate').datepicker({
                allowPastDates: false,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#AppointmentDate").change(function () {
                AppointmentCount();
            });

            $('#PersonAppointmentDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function(event,date) {
                AppointmentCount();
            });

            $("#btnSaveAppointment").click(function () {
                if ($('#AppointmentForm').parsley().validate()) {
                    var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
                    var appDate = $("#<%=AppointmentDate.ClientID%>").val();
                    if (moment('' + appDate + '').isAfter(futureDate)) {
                        toastr.error("Appointment date cannot be set to over 7 months");
                        return false;
                    }
                    addPatientAppointment();
                } else {
                    return false;
                }
            });

            $("#AddAppointment").click(function () {
                $('#AppointmentModal').modal('show');
            });
            
          
            function getViralLoad() {
                
                //console.log("get viral load  called");
                $.ajax({
                    url: '../WebService/LabService.asmx/GetViralLoad',
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function (response) {
                        //console.log(response.d);
                        var items = response.d;
                        items.forEach(function (item, i) {

                            if (item.Month == 1) {

                                jan_vl = item.ResultValue;
                                   
                            } else if (item.Month == 4) {

                                march_vl = item.ResultValue;                                   
                                   
                            }

                        });

                    }

                });
            }
            // function viralLoadGraph() {
            $(function() {
                $.when(getViralLoad()).then(function () {
                    setTimeout(function () {
                        viralLoadGraph();
                    },
                                          2000);
                });
            });
       
            function viralLoadGraph() {
              
               // console.log("encounter viral load  graph called")
                //console.log(march_vl);
                $('#container').highcharts({
                    title: {
                        text: 'Viral Load Trend',
                        x: -20 //center
                    },
                    subtitle: {
                        text: 'VL cp/ml',
                        x: -20
                    },
                    xAxis: {
                        categories: ['Jan', 'Mar', 'May', 'Jul', 'Sep', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: 'Viral Load cp/ml'
                        },
                        plotLines: [
                            {
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }
                        ]
                    },
                    tooltip: {
                        valueSuffix: 'cp/ml'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: 'VL',
                            data: [180, march_vl, "","", "", "", ""]
                        }, {
                            name: 'Threshold',
                            data: [1000, 1000, 1000, 1000, 1000, 1000, 1000]
                        }
                    ]
                });
            };
        });
       
        ////////////////////////////////////End doc ready///////////////////////////////////////////////////////////////////////////////
        function addPatientAppointment() {
            var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
            var reason = $("#<%=Reason.ClientID%>").val();
            var description = $("#<%=description.ClientID%>").val();
            var status = $("#<%=status.ClientID%>").val();
            var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
            /*if (status === '') { status = null }*/
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientAppointment",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Appointment saved successfully");
                    resetAppointmentFields();
                },
                error: function (response) {
                    toastr.error(response.d, "Appointment not saved");
                }
            });
        }

        function AppointmentCount() {
            jQuery.support.cors = true;
            var date = $("#<%=AppointmentDate.ClientID%>").val();
            $.ajax(
            {
                type: "POST",
                url: "../WebService/PatientService.asmx/GetPatientAppointmentCount",
                data: "{'date':'" + date + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    var count = response.d;
                    var message = count + " appointment(s) scheduled on the chosen date.";
                    alert(message);
                },

                error: function(msg) {
                    alert(msg.responseText);
                }
            });
        }

        function resetAppointmentFields(parameters) {
            $("#ServiceArea").val("");
            $("#Reason").val("");
            $("#DifferentiatedCare").val("");
            $("#description").val("");
            $("#AppointmentDate").val("");
        }
            

    </script>

</asp:Content>