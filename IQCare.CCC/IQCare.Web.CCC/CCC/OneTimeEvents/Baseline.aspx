<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="Baseline.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.Baseline" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
   
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    
    <div class="col-md-12">
        <div class="col-md-12">
           <div class="col-md-12">
             <div class="bs-callout bs-callout-danger hidden">
                 <h4 class="fa fa-exclamation-circle"> VALIDATION ERROR(S)</h4>
                 <p>This form seems to be invalid :(</p>
             </div>
        </div> 


            <div class="bs-callout bs-callout-info hidden">
                  <h4 class="fa fa-check-square-o"> All SECTION VALIDATION PASSED</h4>
                  <p>Everything seems to be ok :)</p>
             </div>

        </div>

         <div class="wizard" data-initialize="wizard" id="myWizard">
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
	                <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                        <div class="col-md-12">
                             <div class="col-md-12"><small class="text-primary pull-left">1.Patient Transfer Status</small></div>
                             <div class="col-md-12"><hr/></div>

                             <div class="col-md-12 form-group">
                                  <div class="col-md-4">
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblTINA">Transfer In ?</asp:Label></div>                                
                                           <div class="col-md-12">
                                                <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblTransferInYes">
                                                       <input runat="server" class="sr-only pull-left" name="TransferIn" id="TransferInYes" type="checkbox" value="Yes" ClientIDMode="Static" /> <span class="checkbox-label pull-left"> <strong>Yes</strong> <i> [if patient from another Hospital]</i></span> />
                                                </label>
                                           </div>

                                           <div class="col-md-12">
                                                <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblTransferInNo">
                                                      <input runat="server" class="sr-only" id="TransferInNo" name="TransferIn" type="checkbox" value="No" checked="checked"> <span class="checkbox-label"><strong> No</strong> <i> [ if patient has not enrolled before]</i></span> />
                                                </label>
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
                                            <div class="datepicker fuelux" Id="TIARTStartDate">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="StartDateART" type="text" />
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
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="Label1">Regimen Category </asp:Label></div>
                                       <div class="col-md-12">
                                           <asp:DropDownList runat="server" ID="regimenCategory" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-minlength="1" data-parsley-required="true"/>
                                       </div>
                                  </div>
                                  <div class="col-md-4">
                                      <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblRegimen">Regimen</asp:Label></div>
                                       <div class="col-md-12">
                                           <asp:DropDownList runat="server" ID="RegimenId" CssClass="form-control input-sm" ClientIDMode="Static" data-parsley-minlength="1" data-parsley-required="true"/>
                                        </div>
                                  </div>
     
                             </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-4">
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblfacility">Facility Transferred from :</asp:Label></div>  
                                       <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="TransferFromFacility" CssClass="form-control input-sm" placeholder="facility name.." ClientIDMode="Static" data-parsley-required="true" data-parsley-minlegth="4"></asp:TextBox>
                                       </div>
                                  </div>
                                <div class="col-md-4">
                                       <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblmflcode">MFL Code:</asp:Label></div>
                                       <div class="col-md-12">
                                           <asp:TextBox runat="server" ID="FacilityMFLCode" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="mfl code" data-parsley-required="true" data-parsley-type="number" data-parsley-maxlength="5"></asp:TextBox>
                                       </div>
                                  </div>
                                <div class="col-md-4">
                                      <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" id="lblcounty">County:</asp:Label></div>
                                      <div class="col-md-12">
                                           <asp:DropDownList runat="server" id ="TransferFromCounty" CssClass="form-control" ClientIDMode="Static" data-parsley-required="true"/>
                                      </div>
                                 </div>
                            </div>
                             <div class="form-group col-md-12">
                                  
                                 <div class="col-md-12">
                                     <div class="col-md-12"><label class="control-label pull-left">TransferIn Notes</label></div>
                                     <div class="col-md-12">
                                         <asp:TextBox runat="server" ID="transferInNotes" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div> 
                                 

                             </div>
                        </div>
	                </div><%-- .data-step1--%>
                  
	                <div class="step-pane sample-pane" id="datastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
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
                                       <asp:DropDownList runat="server" ID="WHOStageAtEnrollment" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-min="0"/>
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
                 	
                    <div class="step-pane sample-pane" id="datastep3" data-parsley-validate="true" data-show-errors="true" data-step="3">
                         <div class="col-md-12"><small class="text-primary pull-left">3.Patient ARV History</small></div>
                         <div class="col-md-12"><hr/></div>
                        <div class="form-group col-md-12">
                             <div class="col-md-12" ><asp:Label runat="server" CssClass="control-label pull-left" id="lblARTUse"><i class="fa fa-history" aria-hidden="true"></i> History of ART Use </asp:Label></div> 
                        </div>

                        <div class="form-group col-md-12">
                        
                                 <div class="form-group col-md-3 pull-left">
                                      <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox" id="PrEP">
                                          <asp:CheckBox runat="server" Text="PEP" ClientIDMode="Static" CssClass="sr-only"/> PREP (Pre exposure prophylaxis)
                                        </label>
                                 </div>
                                 <div class="form-group col-md-3">
                                    <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblPMTCT">
                                       <asp:CheckBox runat="server" CssClass="sr-only" ID="PMTCT" ClientIDMode="Static" Text="PMTCT"/><span class="checkbox-label"> PMTCT (Prevention of mother to child Transmission of HIV)</span>
                                    </label>
                                 </div>
                                
                                 <div class="form-group col-md-3">
                                    <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblPEP">
                                       <asp:CheckBox runat="server" CssClass="sr-only" ID="CheckBox1" ClientIDMode="Static" Text="PMTCT"/><span class="checkbox-label"> PEP (Post exposure prophylaxis)</span>
                                    </label>
                                 </div>

                                 <div class="form-group col-md-3">
                                    <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblNONE">
                                        <asp:CheckBox runat="server" CssClass="sr-only"  ID="NONE" ClientIDMode="Static" Text="NONE"/><span class="checkbox-label"> NONE</span>
                                   </label>
                                 </div>
                            
                          
                        </div>

                        
                        <div class="form-group col-md-12">
                             <div class="col-md-4">
                                 <div class="col-md-12"><label class="control-label pull-left">Purpose</label></div>
                                 <div class="col-md-12">
                                      <asp:TextBox runat="server" CssClass="form-control input-sm" ID="RegimenPurpose" ClientIDMode="Static" data-parsley-required="true"/>
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
                                <div class="col-md-12"><label class="control-label pull-right">Action</label></div>
                                <div class="col-md-12">
                                     <asp:LinkButton runat="server" ID="AddPriorHistory"  ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle pull-right"> Add ART Use</asp:LinkButton>
                                </div>  
                            </div>
                            
                        </div>
                        
                        <div class="col-md-12">
                            <table class="table table-condensed table-striped" id="tblARVUseHistory">
                                 <thead>
                                     <tr>
                                         <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">  count.#</span></th>
                                         <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">  Regimen Purpose</span></th>
                                         <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">  Regimen</span></th>
                                         <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">  Date Last Used</span></th>
                                         <th><i class="fa fa-arrow-circle-o-right text-danger" aria-hidden="true"></i><span class="text-primary">   Action</span></th>
                                     </tr>
                                 </thead>
                                <tbody></tbody>
                            </table>
                        </div>
	                </div><%-- .data-step-3--%>
                 
                    <div class="step-pane sample-pane" id="datastep4" data-parsley-validate="true" data-show-errors="true" data-step="4">
                        <div class="col-md-12 form-group">
                            
                            <div class="col-md-12">
                                <div class="col-md-12"><small class="text-primary pull-left">4.Baseline Assessment</small></div>
                                <div class="col-md-12"><hr /></div>
                            </div> 
                            <div class="col-md-12 form-group">
                                <div class="col-md-12"><label class="control-label pull-left">Baseline Assessment (Tick as appropriate)</label></div> 
                            </div>
                            
                            <div class=" form-group col-md-12">
                                 <div class="col-md-2 col-xs-12"> 
                                     <div class="col-md-12 col-xs-12 col-sm-12"><label class="control-label pull-left">Who Stage</label></div>  
                                     <div class="col-md-12 col-xs-12 col-sm-12">
                                         <asp:DropDownList runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="bwhoStage" data-parsley-min="0"/>
                                     </div>
                                </div>

                                 <div class="col-md-2 col-xs-12">
                                     <div class="col-md-12 col-xs-12 col-sm-12"><label class="control-label pull-left">CD4 Count </label></div> 
                                     <div class="col-md-12">
                                          <asp:TextBox runat="server" CssClass="form-control input-sm" ID="bCd4Count" placeholder="cd4 count"  ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>

                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblBVCoInfection">
                                             <input class="sr-only" type="checkbox" id="BVCoInfection" value="true"> <span class="checkbox-label"> HBV Infected</span>
                                      </label>
                                 </div>
                                
                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblPregnancy">
                                             <input class="sr-only" type="checkbox" id="Pregnancy" value="true"> <span class="checkbox-label"> Pregnant</span>
                                      </label>
                                 </div>
                                
                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblBHIV">
                                            <input class="sr-only" type="checkbox" id="BHIV" value="true"> <span class="checkbox-label"> B/HIV</span>
                                      </label>
                                 </div>

                            </div>

                            <div class="form-group col-md-12">
                                                                            
                                 

                                 <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastFeeding">
                                           <input class="sr-only" type="checkbox" id="BreastFeeding" value="true"> <span class="checkbox-label"> BreastFeeding</span>
                                     </label>
                                 </div>
                                 
                                <div class="col-md-2 col-xs-12">
                                      <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTbInfected">
                                           <input class="sr-only" type="checkbox" id="TBInfected" value="true"> <span class="checkbox-label"> TB Infected</span>
                                     </label>
                                 </div>

                                <div class="col-md-8"></div>
                                
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                            </div>

                            <div class="form-group col-md-12">
                                 <div class="col-md-3">
                                      <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblMUAC">MUAC </asp:label></div>
                                      <div class="col-md-12">
                                          <asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineMUAC" CssClass="form-control input-sm" placeholder="MUAC" data-parsley-required="true"></asp:TextBox>
                                      </div>
                                 </div>
                                 <div class="col-md-3">
                                      <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblWeight">Weight (Kgs) </asp:label></div>
                                      <div class="col-md-12">
                                          <asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineWeight" CssClass="form-control input-sm" placeholder="0.0 kgs" data-parsley-required="true" data-aprsley-type="number" data-parsley-min="2"></asp:TextBox>
                                      </div>
                                 </div>
                                 <div class="col-md-3">
                                      <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblheight">Height (cm) </asp:label></div>
                                      <div class="col-md-12">
                                          <asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineHeight" CssClass="form-control input-sm" placeholder="0.00 cms" data-parsley-required="true" data-parsley-type="number" data-parsley-min="5"></asp:TextBox>
                                      </div>
                                </div>
                                 <div class="col-md-3">
                                     <div class="col-md-12"><asp:label runat="server" class="control-label pull-left" id="lblBMI">BMI </asp:label></div>
                                     <div class="col-md-12">
                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="BaselineBMI" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                     </div>
                                </div>
                            </div>                             
                        </div>
                    </div><%-- .col-md-12--%>

	                <div class="step-pane sample-pane" id="datastep5" data-parsley-validate="true" data-show-errors="true" data-step="5">
                         <div class="col-md-12">
                             <div class="col-md-12"><small class="text-primary pull-left">5. Treatment Inititation</small></div>  
                             <div class="col-md-12"><hr/></div>
                             <div class="col-md-12 form-group">
                                 <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">Date Started On 1st Line</label></div>
                                     <div class="col-md-12">
                                         <div class="datepicker fuelux" id="DateStartedOn1stLine">
                                               <div class="input-group">
                                                    <input class="form-control input-sm" id="TreatmeantInitiationDateStartedOn1stLine" type="text" /><div class="input-group-btn">
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
                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="BaselineViralload" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="2"></asp:TextBox>
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
                             
                             <div class="col-md-12 form-group">
                                  <div class="col-md-3">
                                      <div class="col-md-12"><label class="control-label pull-left">Regimen Category</label></div>
                                      <div class="col-md-12">
                                          <asp:DropDownList runat="server" ID="InitiationRegimen" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="0"/>
                                      </div>
                                  </div>
                                 <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">Regimen</label></div>
                                     <div class="col-md-12">
                                          <asp:DropDownList runat="server" ID="RegimenInitiationId" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="0"/>
                                     </div>
                                 </div>
                                 <div class="col-md-3"></div>
                                 <div class="col-md-3"></div>
                             </div>
                         </div>
	                   
                    </div><%-- .data-step-4--%>
             </div>


         </div><%-- .wizard--%>   

    </div><%-- .col-md-12--%>
    
    <script type="text/javascript">
        $(document).ready(function(){

            var purposeList = new Array();
            var userId=<%=UserId%>; /* get the current userId*/
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var transferIn = 0;
            var treatmentType = 0;

            var whostage = '';
            var cD4Count='';
            var bVCoInfection = false;
            var breastfeeding = false;
            var pregnancy = false;
            var bHiV = false;
            var tbInfection = false;

            var today = new Date();

            $('#TIARTStartDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#TIDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#DHID').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
               // restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#DOE').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
               // restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#DARTI').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]

            });
            $('#DLUsed').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#DateStartedOn1stLine').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
               // restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#BaselineViralloadDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });

            /*-- check for future dates -- check if ART start Date >TI Date */
            $('#TIARTStartDate').on('changed.fu.datepicker dateClicked.fu.datepicker',function(event, date) {

                var artStartDate = $('#TIARTStartDate').datepicker('getDate');
                var tiDate= $('#TIDate').datepicker('getDate');
                var isAfter=moment(artStartDate).isAfter(tiDate);              
                var futureDate = moment(artStartDate).isAfter(today);
                if (isAfter) {
                    toastr.error("ART Start Date CANNOT be greater than transferin Date",
                        "ART Start Date-Transfer in Validation");
                    $("#TIDate").datepicker('setDate','');
                    return false;
                }

                if(futureDate)
                {
                    toastr.error("Future Dates NOT ALLOWED!");
                    return false;
                }

            });

            /*--validate future dates */
            $('#TIDate').on('changed.fu.datepicker dateClicked.fu.datepicker',
                function(event, date) {
                    var tiDate= $('#TIDate').datepicker('getDate');
                    var futureDate=moment(tiDate).isAfter(today)
                    if (futureDate) {
                        toastr.error("future dates NOT allowed !");
                        return false;
                    }
                });

            /* set cohort yeat and Month */
            $("#DateStartedOn1stLine").on('changed.fu.datepicker dateClicked.fu.datepicker',
                function(event, date) {
                    
                    var dateStarted = $(this).datepicker('getDate');
                    var startMonth = dateStarted.format('MMM');
                    var startYear = dateStarted.getUTCFullYear();
                    $("#ARTCohort").val(startMonth + '-' + startYear);
                });

            /* clientside validation */
            disableIfNotTransferIn();
            noneChecked();

            $("#lblTransferInYes").checkbox('uncheck');
            $("#lblTransferInNo").checkbox('check');
            $("#lblNONE").checkbox('check');

            $("#lblTransferInYes").on('checked.fu.checkbox',
                function() {
                    //uncheck No
                    transferIn = 1;
                    $("#lblTransferInNo").checkbox('uncheck');
                    enableIfTransferIn();
                });

            //$("#lblTransferInYes").on('unchecked.fu.checkbox',
            //    function () {
            //        //uncheck No
            //        $("#lblTransferInNo").checkbox('uncheck');
            //        enableIfTransferIn();
            //    });

            $("#lblTransferInNo").on("checked.fu.checkbox",
                function() {
                    transferIn = 0;
                    $("#lblTransferInYes").checkbox('uncheck');
                    disableIfNotTransferIn();
                });
            //$("#lblTransferInNo").on("unchecked.fu.checkbox",
            //    function() {
            //        $("#lblTransferInYes").checkbox('uncheck');
            //        disableIfNotTransferIn();
            //    });
            $("#PrEP").on("checked.fu.checkbox",
                function() {
                    treatmentType = 1;
                    $("#lblPMTCT").checkbox('uncheck');
                    $("#lblPEP").checkbox('uncheck');
                    $("#lblNONE").checkbox('uncheck');
                    $("#RegimenPurpose").val();
                    $("#RegimenPurpose").val("PrEP");
                    noneUnchecked();
                });
            $("#lblPEP").on("checked.fu.checkbox",
                function() {
                    treatmentType = 2;
                    $("#lblPMTCT").checkbox('uncheck');
                    $("#PrEP").checkbox('uncheck');
                    $("#lblNONE").checkbox('uncheck');
                    $("#RegimenPurpose").val();
                    $("#RegimenPurpose").val("PEP");
                    noneUnchecked();
                });
            $("#lblPMTCT").on("checked.fu.checkbox",
                function() {
                    treatmentType = 3;
                    $("#PrEP").checkbox('uncheck');
                    $("#lblPEP").checkbox('uncheck');
                    $("#lblNONE").checkbox('uncheck');
                    $("#RegimenPurpose").val();
                    $("#RegimenPurpose").val("PMTCT");
                    noneUnchecked();
                });
            $("#lblNONE").on("checked.fu.checkbox",
                function() {
                    treatmentType = 4;
                    $("#lblPMTCT").checkbox('uncheck');
                    $("#lblPEP").checkbox('uncheck');
                    $("#PrEP").checkbox('uncheck');
                    $("#RegimenPurpose").val("");
                    noneChecked();
                });



            $("#AddPriorHistory").click(function(e) {
               
                var purposeCount = 0;
                var pusposeFound = 0;
                var regimenId = 1;

                var purpose = $("#<%=RegimenPurpose.ClientID%>").val();
               // var regimenId = $("#<%=HistoryRegimen%>").find(":selected").val();
                var regimen = $("#<%=HistoryRegimen.ClientID%>").val();
                var dateLastUsed = $("#DLUsed").datepicker('getDate');

                if (moment('' + dateLastUsed + '').isAfter()) {

                    toastr.warning("Future dates not allowed for baseline assessment.", "Baseline Assessment");
                    return false;
                }

                if (regimenId < 1) {
                    toastr.warning("Select at least 1 ARV regimen used!", "Baseline Assessment");
                    return false;
                }

                if (purpose.length<1) {
                    toastr.warning("ARV use Purpose is required!", "Baseline Assessment");
                    return false;
                }
                pusposeFound = $.inArray("" + purpose + "", purposeList);

                if (pusposeFound > -1) {

                    toastr.warning(identifier + "Identifier already exisits in the Lits,", "Baseline Assessment ");
                    return false; // message box herer
                } else {

                    purposeList.push("" + purpose + "");
                    var tr = '<tr><td align="left">'+regimenId+'</td><td align="left">' + purpose + '</td><td align="left">'+regimen+'</td><td align="left">' + moment(dateLastUsed).format('DD-MMM-YYYY') + '</td><td align="right"><button type="button" class="btnDelete btn btn-danger fa fa-minus-circle btn-fill" > Remove</button></td></tr>';
                    $("#tblARVUseHistory>tbody:first").append('' + tr + '');
                }

                e.preventDefault();

            });
    
            $("#tblARVUseHistory").on('click','.btnDelete',function() {
                    $(this).closest('tr').remove();
                    var x = $(this).closest('tr').find('td').eq(0).html();
                    purposeList.splice($.inArray(x, purposeList), 1);
            });

            //datastep4
           // $("#lblwhostage").checkbox('uncheck');
            //$("#lblCD4Count").checkbox('uncheck');
            $("#lblBVCoInfection").checkbox('uncheck');
            $("#lblPregnancy").checkbox('uncheck');
            $("#lblBreastFeeding").checkbox('uncheck');
            $("#lblBHIV").checkbox('uncheck');
            $("#lblTbInfection").checkbox('uncheck');

            /* when checked */
           // $("#lblwhostage").on('checked.fu.checkbox',function () { whostage = true; });
           // $("#lblCD4Count").on('checked.fu.checkbox', function () { cD4Count = true; });
            $("#lblBVCoInfection").on('checked.fu.checkbox', function () { bVCoInfection = true; });
            $("#lblPregnancy").on('checked.fu.checkbox', function () { pregnancy = true; });
            $("#lblBreastFeeding").on('checked.fu.checkbox', function () { breastfeeding = true; });
            $("#lblBHIV").on('checked.fu.checkbox', function () { bHiV = true; });
            $("#lblTbInfection").on('checked.fu.checkbox', function () { tbInfection = true; });

            /* when unchecked */
           // $("#lblwhostage").on('unchecked.fu.checkbox', function () { whostage = false; });
            //$("#lblCD4Count").on('unchecked.fu.checkbox', function () { cD4Count = false; });
            $("#lblBVCoInfection").on('unchecked.fu.checkbox', function () { bVCoInfection = false; });
            $("#lblPregnancy").on('unchecked.fu.checkbox', function () { pregnancy = false; });
            $("#lblBreastFeeding").on('unchecked.fu.checkbox', function () { breastfeeding = false; });
            $("#lblBHIV").on('unchecked.fu.checkbox', function () { bHiV = false; });
            $("#lblTbInfection").on('unchecked.fu.checkbox', function () { tbInfection = false; });

    <%--         $("#<%=.ClientID%>").prop('disabled', true);
            $("#<%=.ClientID%>").prop('disabled', true);
            $("#<%=.ClientID%>").prop('disabled', true);
             $("#<%=.ClientID%>").prop('disabled',true);--%>

            function enableIfTransferIn() {
                $("#TIDate").datepicker('enable');
                $("#TIARTStartDate").datepicker('enable');
                $("#DateStartedOn1stLine").datepicker('enable');
                $("#<%=RegimenId.ClientID%>").prop('disabled', false);
                 $("#<%=regimenCategory.ClientID%>").prop('disabled', false);
                $("#<%=TransferFromFacility.ClientID%>").prop('disabled', false);
                $("#<%=FacilityMFLCode.ClientID%>").prop('disabled', false);
                $("#<%=TransferFromCounty.ClientID%>").prop('disabled', false);
                $("#<%=transferInNotes.ClientID%>").prop('disabled', false);

            }

            function disableIfNotTransferIn() {
                
                $("#TIDate").datepicker('disable');
                $("#TIARTStartDate").datepicker('disable');
                $("#DateStartedOn1stLine").datepicker('disable');
                $("#<%=RegimenId.ClientID%>").prop('disabled', true);
                $("#<%=regimenCategory.ClientID%>").prop('disabled', true);
                $("#<%=TransferFromFacility.ClientID%>").prop('disabled', true);
                $("#<%=FacilityMFLCode.ClientID%>").prop('disabled', true);
                $("#<%=TransferFromCounty.ClientID%>").prop('disabled', true);
                $("#<%=transferInNotes.ClientID%>").prop('disabled', true);
            }

            function noneChecked() {
                $("#<%=RegimenPurpose.ClientID%>").prop('disabled', true);
                $("#<%=HistoryRegimen.ClientID%>").prop('disabled', true);
                $("#DLUsed").datepicker("disable");
                $("#<%=AddPriorHistory.ClientID%>").attr("disabled");
            }

            function noneUnchecked() {
                $("#<%=RegimenPurpose.ClientID%>").prop('disabled', false);
                $("#<%=HistoryRegimen.ClientID%>").prop('disabled', false);
                $("#DLUsed").datepicker("enable");
                $("#<%=AddPriorHistory.ClientID%>").removeAttr("disabled");   
            }

  $("#myWizard")
    .on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        var nextStep = 0;
        var previousStep = 0;
        var totalError = 0;
        var stepError = 0;
        /*var form = $("form[name='form1']");*/


        if (data.direction === 'next')
            nextStep = currentStep += 1;
        else
            previousStep = nextStep -= 1;
        if (data.step === 1) {
                $('#datastep1').parsley().destroy();
                $('#datastep1').parsley({
                excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
                });

            /* add constraints based on age*/
            if ($('#datastep1').parsley().validate()) {
                
                if (transferIn > 0) {
                    addPatientTransferIn();
                } else {
                    toastr.info("Patient is NOT a transferIN", "PatientTransferIn status");
                }
               
            } else {
                stepError = $('.parsley-error').length === 0;
                totalError += stepError;
                evt.preventDefault();
            }
        }
        else if (data.step === 2) {
            $('#datastep2').parsley().destroy();
            $('#datastep2').parsley({
                excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
            });
            if ($("#datastep2").parsley().validate()) {
                managePatientHivDiagnosis();
            } else {
                stepError = $('.parsley-error').length === 0;
                totalError += stepError;
                evt.preventDefault();
            }
        }
        else if (data.step === 3) {
            $('#datastep3').parsley().destroy();
            $('#datastep3').parsley({
                excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
            });
            if ($("#datastep3").parsley().validate()) {
                if (treatmentType > 3) {
                    toastr.info("No ART/ARV History Selected for this patient!");
                } else {
                    managePatientArvHistory();
                }
                
            } else {
                stepError = $('.parsley-error').length === 0;
                totalError += stepError;
                evt.preventDefault();
            }
        }
        else if (data.step === 4) {
            $('#datastep4').parsley().destroy();
            $('#datastep4').parsley({
                excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
            });
            if ($("#datastep4").parsley().validate()) {
                managePatientBaselineAssessment();
            } else {
                stepError = $('.parsley-error').length === 0;
                totalError += stepError;
                evt.preventDefault();
            }
        }
        else if (data.step === 5) {
            $("#datastep5").parsley().destroy();
            $('#datastep4').parsley({
                excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
            });
            if ($("#datastep5").parsley().validate()) {
              if(transferIn>0){  managePatientTreatmentInitiation();}
            } else {
                stepError = $('.parsley-error').length === 0;
                totalError += stepError;
                evt.preventDefault();
            }
        }       
    })
    .on("changed.fu.wizard",
        function () {

        })
    .on('stepclicked.fu.wizard',
        function () {

        })
    .on('finished.fu.wizard',
        function (e) {
            window.open.href('<%=ResolveClientUrl("~/CCC/patient/PatientHome.aspx")%>');
        });

            /*filter regimens*/
            $("#regimenCategory").on("change",
                function() {

                    var reg = $(this).find(":selected").text();
                    var str =reg.replace(/\s+/g,''); 
                    //reg =reg.replace("/+/g", "");

                    $.ajax({
                        type: "POST",
                        url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
                        data: "{'masterName':'" + str + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response) {
                            var itemList = JSON.parse(response.d);
                            $("#<%=RegimenId.ClientID%>").find('option').remove().end();
                            $("#<%=RegimenId.ClientID%>").append('<option value="0">Select</option>');
                            $.each(itemList,
                                function(index, itemList) {
                                    $("#<%=RegimenId.ClientID%>")
                                        .append('<option value="' +
                                            itemList.ItemId +
                                            '">' +
                                            itemList.ItemName +"("+itemList.ItemDisplayName+")"+
                                            '</option>');
                                });
                        },
                        error: function(response) {
                            toastr.error("Error in Fetching Ward list " + response.d, "Fetching Ward List");
                        }
                    });
                });

            $("#InitiationRegimen").on("change",
                function() {

                    var reg = $(this).find(":selected").text();
                    var str =reg.replace(/\s+/g,''); 
                    //reg =reg.replace("/+/g", "");

                    $.ajax({
                        type: "POST",
                        url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
                        data: "{'masterName':'" + str + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response) {
                            var itemList = JSON.parse(response.d);
                            $("#<%=RegimenInitiationId.ClientID%>").find('option').remove().end();
                            $("#<%=RegimenInitiationId.ClientID%>").append('<option value="0">Select</option>');
                            $.each(itemList,
                                function(index, itemList) {
                                    $("#<%=RegimenInitiationId.ClientID%>")
                                        .append('<option value="' +
                                            itemList.ItemId +
                                            '">' +
                                            itemList.ItemName +"("+itemList.ItemDisplayName+")"+
                                            '</option>');
                                });
                        },
                        error: function(response) {
                            toastr.error("Error in Fetching Ward list " + response.d, "Fetching Ward List");
                        }
                    });
                });

            

            /* datat persistence functions */
            function addPatientTransferIn() {

                var serviceAreaId = 0;
                var transferInDate = moment($('#TIDate').datepicker('getDate')).format('DD-MMM-YYYY');
                var treatmentStartDate = moment($('#<%=lblARTStartDate.ClientID%>').datepicker('getDate')).format('DD-MMM-YYYY');
                var currentTreatment = $('#<%=RegimenId.ClientID%>').find(":selected").val();
                var facilityFrom = $('#<%=TransferFromFacility.ClientID%>').val();
                var mflCode = $('#<%=FacilityMFLCode.ClientID%>').val();
                var countyFrom = $('#<%=TransferFromCounty.ClientID%>').find(":selected").val();
                var transferInNotes = $('#<%=transferInNotes.ClientID%>').val();
                var ptnId = patientId;
                var ptnmasterVisitId = patientMasterVisitId;

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientBaselineService.asmx/AddPatientTransferStatus",
                    data: "{'patientId':'" + ptnId +"','patientMastervisitId':'" +ptnmasterVisitId +"','transferinDate':'" +transferInDate +"','treatmentStartDate':'"+treatmentStartDate+"','serviceAreaId':'" +serviceAreaId +"','currentTreatment':'" +currentTreatment +"','facilityFrom':'" +
                        facilityFrom +"','mflCode':'" +mflCode +"','countyFrom':'" +countyFrom +"','transferInNotes':'" +transferInNotes +"','userId':'" +userId +"'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        toastr.success(response.d, "PatientTransferIn Status");
                    },
                    error: function(xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);

                    }
                });
            }

            
            function managePatientHivDiagnosis() {

                var id = 0;
                var hivDiagnosisDate = moment($('#DHID').datepicker('getDate')).format('DD-MMM-YYYY');
                var enrollmentDate = moment($('#DOE').datepicker('getDate')).format('DD-MMM-YYYY');
                var artInitiationDate = moment($('#DARTI').datepicker('getDate')).format('DD-MMM-YYYY');
                var enrollmentWhoStage = $('#<%=WHOStageAtEnrollment.ClientID%>').find(":selected").val();
                var ptnId = patientId;
                var ptnmasterVisitId = patientMasterVisitId;

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientBaselineService.asmx/managePatientHivDiagnosis",
                    data: "{'id':'" +id +"','patientId':'" +ptnId +"','patientMasterVisitId':'" + ptnmasterVisitId +"','hivDiagnosisDate':'" +hivDiagnosisDate +"','enrollmentDate':'" + enrollmentDate +"','enrollmentWhoStage':'" + enrollmentWhoStage +"','artInitiationDate':'" +artInitiationDate + "','userId':'" + userId +"'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        toastr.success(response.d, "Patient HIV Dignosis Status");
                    },
                    error: function(xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);

                    }
                });
            }


            function managePatientArvHistory() {

                var id = 0;
                var ptnId = patientId;
                var ptnmasterVisitId = patientMasterVisitId;
                var treatment;
                if(treatmentType===1){treatment='PrEP'} else if(treatmentType===2){ treatment='PEP'} else if(treatmentType===3){treatment='PMTCT'}

                var artuseHistoryTable = new Array();
                $("#tblARVUseHistory tr").each(function(row, tr) {
                    artuseHistoryTable[row] = {
                        "treatment":treatment,
                        "purpose": $(tr).find('td:eq(1)').text(),
                        "regimen": $(tr).find('td:eq(2)').text(),
                        "dateLastUsed": $(tr).find('td:eq(3)').text()
                    }
                });

                artuseHistoryTable.shift();//first row will be empty-so remove it
                var jsonArtHistory = JSON.stringify(artuseHistoryTable);

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientBaselineService.asmx/managePatientArvHistory",
                    data: "{'id':'" + id + "','patientId':'" + ptnId + "','patientMasterVisitId':'" + ptnmasterVisitId + "','artuseStrings':'" + jsonArtHistory + "','userId':'" + userId +
                        "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        toastr.success(response.d);
                    },
                    error: function(xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    }
                });
            }



            function managePatientBaselineAssessment() {



                var muac= $("#<%=BaselineMUAC.ClientID%>").val();
                var weight= $("#<%=BaselineWeight.ClientID%>").val(); 
                var height= $("#<%=BaselineHeight.ClientID%>").val();
                whostage = $("#<%=bwhoStage.ClientID%>").find(":selected").val();
                cD4Count = $("#<%=bCd4Count.ClientID%>").val();
                var id = 0;
                var ptnId = patientId;
                var ptnmasterVisitId = patientMasterVisitId;
                
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientBaselineService.asmx/managePatientBaselineAssessment",
                    data: "{'id':'" + id + "','patientId':'" + ptnId + "','patientMasterVisitId':'" + ptnmasterVisitId + "','pregnant':'" + pregnancy + "','hbvInfected':'" + bVCoInfection + "','tbInfected':'" + tbInfection + "','whoStage':'" + whostage + "','breastfeeding':'" + breastfeeding + "','cd4Count':'" + cD4Count + "','muac':'" + muac + "','weight':'" + weight + "','height':'" + height + "','userId':'" + userId +
                        "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d);
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    }
                });
            }

            function managePatientTreatmentInitiation() {
                
                var viralLoad= $("#<%=BaselineViralload.ClientID%>").val(); 
                var viralLoadDate= moment($("#BaselineViralloadDate").datepicker("getDate")).format('DD-MMM-YYYY');
                var artCohort= $("#<%=ARTCohort.ClientID%>").val(); 
                var firstlineStartDate= moment($("#DateStartedOn1stLine").datepicker('getDate')).format('DD-MMM-YYYY');
                var startRegimen = $("#<%=RegimenInitiationId.ClientID%>").find(":selected").val();

                var id = 0;
                var ptnId = patientId;
                var ptnmasterVisitId = patientMasterVisitId;

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientBaselineService.asmx/managePatientTreatmentInitiation",
                    data: "{'id':'" + id + "','patientId':'" + ptnId + "','patientMasterVisitid':'" + ptnmasterVisitId + "','dateStartedOnFirstLine':'" + firstlineStartDate + "','cohort':'" + artCohort + "','regimen':'" + startRegimen + "','baselineViralload':'" + viralLoad + "','baselineViralLoadDate':'" + viralLoadDate + "','userId':'" + userId +
                        "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d);
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    }
                });
            }

        });
    </script>

</asp:Content>































































