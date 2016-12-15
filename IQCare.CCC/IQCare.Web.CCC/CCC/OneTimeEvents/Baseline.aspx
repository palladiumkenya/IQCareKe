<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Greencard.master" AutoEventWireup="true" CodeBehind="Baseline.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.Baseline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
<%--         <div class="col-md-12"><small class="pull-left fa fa-arrow-circle-o-right fa-2x"> Baseline Assessment & Treatment Inititiation</small></div>
              <div class="col-md-12"><hr /></div>--%>
         <div class="wizard" data-initialize="wizard" id="PatientBaseline">
              <div class="steps-container">
	               <ul class="steps">
		                <li data-step="1" data-name="campaign" class="active">
			                <span class="badge">1</span>Transfer Status
			                <span class="chevron"></span>
		                </li>
		                <li data-step="2">
			                <span class="badge">2</span>HIV Diagnosis
			                <span class="chevron"></span>
		                </li>
                       	<li data-step="3">
			                <span class="badge">3</span>ARV History
			                <span class="chevron"></span>
		                </li>
                        <li data-step="4">
			                <span class="badge">4</span>Baseline Assessment
			                <span class="chevron"></span>
		                </li>

		                <li data-step="5" data-name="template">
			                <span class="badge">5</span>Treatment Initiation
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
	                <div class="step-pane active sample-pane" data-step="1">
                        <div class="col-md-12">
                             <div class="col-md-12"><small class="text-primary pull-left">1.Patient Transfer Status</small></div>
                             <div class="col-md-12"><hr/></div>

                             <div class="col-md-12 form-group">
                                  <div class="col-md-4">
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblTINA">Transfer In ?</asp:Label></div>
                                       <div class="col-md-12">
                                           <div class="col-md-6">
                                                <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTransferInYes">
                                                       <input class="sr-only" id="TransferInYes" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                                </label>
                                           </div>
                                           <div class="col-md-6">
                                                <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTransferInNo">
                                                      <input class="sr-only" id="TransferInNo" type="checkbox" value="option1"> <span class="checkbox-label"> No </span>
                                                </label>
                                           </div>
                                       </div> 
                                  </div>
                                  
                                 <div class="col-md-4">
                                      <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblTIDate">TransferIn Date</asp:Label></div> 
                                      
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

                                  <div class="col-md-4">
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
                             </div>
                            
                             <div class="form-group col-md-12">
                                  <div class="col-md-4">
                                      <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblRegimen">Regimen</asp:Label></div>
                                       <div class="col-md-12">
                                           <asp:TextBox runat="server" id="TransferRegimen" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="regimen"></asp:TextBox>
                                        </div>
                                  </div>

                                  <div class="col-md-4">
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblfacility">Facility Transferred from :</asp:Label></div>  
                                       <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="TransferFromFacility" CssClass="form-control input-sm" placeholder="facility name.." ClientIDMode="Static"></asp:TextBox>
                                       </div>
                                  </div>

                                  <div class="col-md-4">
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblmflcode">MFL Code:</asp:Label></div>
                                       <div class="col-md-12">
                                           <asp:TextBox runat="server" ID="FacilityMFLCode" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="mfl code"></asp:TextBox>
                                       </div>
                                  </div>
                             </div>
                            
                             <div class="form-group col-md-12">
                                 <div class="col-md-4">
                                      <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblcounty">County:</asp:Label></div>
                                      <div class="col-md-12">
                                           <asp:DropDownList runat="server" id ="TransferFromCounty" CssClass="form-control" ClientIDMode="Static"/>
                                      </div>
                                 </div> 
                                 <div class="col-md-4"></div> 
                                 <div class="col-md-4"></div> 

                             </div>
                        </div>
	                </div><%-- .data-step1--%>
                  
	                <div class="step-pane sample-pane " data-step="2">
                         <div class="col-md-12"><small class="text-primary pull-left">2.Patient HIV Diagnosis</small></div>
                         <div class="col-md-12"><hr/></div>
                         <div class="form-group col-md-12">
                              <div class="col-md-4">
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
                             <div class="col-md-4">
                                  <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblDateOfEnrollment">Date of Enrollment</asp:Label></div>
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
                             <div class="col-md-4">
                                  <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblwhostage">WHO Stage at Enrollment</asp:Label></div>
                                  <div class="col-md-12">
                                       <asp:DropDownList runat="server" ID="WHOStageAtEnrollment" ClientIDMode="Static" CssClass="form-control input-sm"/>
                                  </div>
                             </div>
                         </div>
                         <div class="form-group col-md-12">
                              <div class="col-md-4">
                                   <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblDateOfARTInitiation">Date of ART Initiation</asp:Label></div>
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
                             <div class="col-md-4"></div>
                             <div class="col-md-4"></div>
                         </div>
	                </div><%-- .data-step-2--%>
                 	
                    <div class="step-pane sample-pane" data-step="3">
                         <div class="col-md-12"><small class="text-primary pull-left">3.Patient ARV History</small></div>
                         <div class="col-md-12"><hr/></div>
                        <div class="form-group col-md-12">
                             <div class="col-md-12" ><asp:Label runat="server" CssClass="control-label pull-left" id="lblARTUse"><i class="fa fa-history" aria-hidden="true"></i> History of ART Use </asp:Label></div> 
                        </div>

                        <div class="form-group col-md-12">
                            <div class="col-md-2">
                                 <div class="form-group col-md-12">
                                      <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox" id="PEP">
                                          <asp:CheckBox runat="server" Text="PEP" ClientIDMode="Static" CssClass="sr-only"/> PEP .
                                        </label>
                                 </div>
                                 <div class="form-group col-md-12">
                                    <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblPMTCT">
                                       <asp:CheckBox runat="server" CssClass="sr-only" ID="PMTCT" ClientIDMode="Static" Text="PMTCT"/><span class="checkbox-label"> PMTCT</span>
                                    </label>
                                 </div>
                                 <div class="form-group col-md-12">
                                    <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblNONE">
                                        <asp:CheckBox runat="server" CssClass="sr-only" Checked="True" ID="NONE" ClientIDMode="Static" Text="NONE"/><span class="checkbox-label"> NONE</span>
                                   </label>
                                 </div>
                            </div>
                            <div class="col-md-10"></div>
                        </div>

                        
                        <div class="form-group col-md-12">
                             <div class="col-md-4">
                                 <div class="col-md-12"><label class="control-label pull-left">Purpose</label></div>
                                 <div class="col-md-12">
                                      <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="RegimenPurpose" ClientIDMode="Static"/>
                                 </div>
                             </div>

                             <div class="col-md-3">
                                <div class="col-md-12"><label class="control-label pull-left">Regimen</label></div>
                                <div class="col-md-12">
                                     <asp:TextBox runat="server" ID="HistoryRegimen" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>

                             <div class="col-md-3">
                                <div class="col-md-12"><label class="control-label pull-left">Date Last Used</label></div>
                                <div class="col-md-12">
                                     <div class="datepicker fuelux" id="DLUsed">
                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="RegimenDateLastUsed" type="text" />
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
                                <div class="col-md-12"><label class="control-label pull-left">Action</label></div>
                                <asp:LinkButton ID="AddPriorHistory" CssClass="btn btn-info pull-left" runat="server" ><i class="fa fa-plus-circle" aria-hidden="true"></i> Add History of ART</asp:LinkButton>
                            </div>
                        </div>
	                </div><%-- .data-step-3--%>
                 
                    <div class="step-pane sample-pane" data-step="4">
                        <div class="col-md-12">
                            
                            <div class="col-md-12">
                                <div class="col-md-12"><small class="text-primary pull-left">4.Baseline Assessment</small></div>
                                <div class="col-md-12"><hr /></div>
                            </div> 
                            <div class="col-md-12 form-group">
                                <div class="col-md-12"><label class="control-label pull-left">Baseline Assessment (Tick as appropriate)</label></div> 
                            </div>
                            
                            <div class=" form-group col-md-12">
                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblWHOStageHistory">
                                            <input class="sr-only" type="checkbox" id="WHOStageHistory" value="option1"> <span class="checkbox-label"> WHO Stage</span>
                                     </label>
                                </div>

                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblCD4Count">
                                            <input class="sr-only" type="checkbox" id="CD4Count" value="option1"> <span class="checkbox-label"> CD4 Count</span>
                                      </label>
                                 </div>

                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBVCoInfection">
                                             <input class="sr-only" type="checkbox" id="BVCoInfection" value="option1"> <span class="checkbox-label"> BV Co-Infection</span>
                                      </label>
                                 </div>
                                
                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPregnancy">
                                             <input class="sr-only" type="checkbox" id="Pregnancy" value="option1"> <span class="checkbox-label"> Pregnancy</span>
                                      </label>
                                 </div>
                                
                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblB/HIV">
                                            <input class="sr-only" type="checkbox" id="BHIV" value="option1"> <span class="checkbox-label"> B/HIV</span>
                                      </label>
                                 </div>

                            </div>

                            <div class="form-group col-md-12">
                                                                            
                                 

                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastFeeding">
                                           <input class="sr-only" type="checkbox" id="BreastFeeding" value="option1"> <span class="checkbox-label"> BreastFeeding</span>
                                     </label>
                                 </div>
                                <div class="col-md-10"></div>
                                
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                            </div>

                            <div class="form-group col-md-12">
                                 <div class="col-md-3">
                                      <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblMUAC">MUAC </asp:label></div>
                                      <div class="col-md-12">
                                          <asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineMUAC" CssClass="form-control input-sm" placeholder="MUAC"></asp:TextBox>
                                      </div>
                                 </div>
                                 <div class="col-md-3">
                                      <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblWeight">Weight (Kgs) </asp:label></div>
                                      <div class="col-md-12">
                                          <asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineWeight" CssClass="form-control input-sm" placeholder="0.0 kgs"></asp:TextBox>
                                      </div>
                                 </div>
                                 <div class="col-md-3">
                                      <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblheight">Height (cm) </asp:label></div>
                                      <div class="col-md-12">
                                          <asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineHeight" CssClass="form-control input-sm" placeholder="0.00 cms"></asp:TextBox>
                                      </div>
                                </div>
                                 <div class="col-md-3">
                                     <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblBMI">BMI </asp:label></div>
                                     <div class="col-md-12">
                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="BaselineBMI" ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                </div>
                            </div>                             
                        </div>
                    </div><%-- .col-md-12--%>

	                <div class="step-pane sample-pane" data-step="5">
                         <div class="col-md-12">
                             <div class="col-md-12"><small class="text-primary pull-left">5. Treatment Inititation</small></div>  
                             <div class="col-md-12"><hr/></div>
                             <div class="col-md-12">
                                 <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">Date Started On 1st Line</label></div>
                                     <div class="col-md-12">
                                         <div class="datepicker fuelux" id="DateStartedOn1stLine">
                                               <div class="input-group">
                                                    <input class="form-control input-sm" id="TreatmeantInitiationDateStartedOn1stLine" type="text" />
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
                                 <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">ART Cohort</label></div>
                                     <div class="col-md-12">
                                          <asp:TextBox runat="server" ID="ARTCohort" CssClass="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">Baseline Viralload</label></div>
                                     <div class="col-md-12">
                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="BaselineViralload" ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">Baseline Viraload Date</label></div>
                                     <div class="col-md-12">
                                         <div class="datepicker fuelux" id="BaselineViralloadDate">
                                               <div class="input-group">
                                                    <input class="form-control input-sm" id="TreatmeantInitiationBaselineViralloadDate" type="text" />
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
	                   
                    </div><%-- .data-step-4--%>
                </div>


         </div><%-- .wizard--%>   

    </div><%-- .col-md-12--%>
    
    <script type="text/javascript">
        $(document).ready(function(){
            $('#ARTStartDate').datepicker();
            $('#TIDate').datepicker();
            $('#DHID').datepicker();
            $('#DOE').datepicker();
            $('#DARTI').datepicker();
            $('#DLUsed').datepicker();
            $('#DateStartedOn1stLine').datepicker();
            $('#BaselineViralloadDate').datepicker();
        });
    </script>

</asp:Content>































