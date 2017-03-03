<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PatientEncounter" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../Scripts/js/PatientEncounter.js"></script>
  
     <div class="col-md-12">
                <uc:PatientDetails ID="PatientSummary" runat="server" />
            </div>
            <div class="col-md-12 col-xs-12">

                 <ul class="nav nav-tabs" role="tablist">
                     <li role="presentation" class="active"> <a href="#encounter"    aria-controls="encounter"   role="tab" data-toggle="tab"><i class="fa fa-exchange fa-lg" aria-hidden="true"></i> Clinical Encounter</a></li>
                     <li role="presentation"> <a href="#vlTracker"    aria-controls="vlTracker"   role="tab" data-toggle="tab"><i class="fa fa-line-chart fa-lg" aria-hidden="true"></i> Viraload Tracker</a></li>
                     <li role="presentation"> <a href="#Laboratory"   aria-controls="Laboratory"  role="tab" data-toggle="tab"><i class="fa fa-flask fa-lg" aria-hidden="true"></i> Laboratory</a></li>
                     <li role="presentation"> <a href="#Pharmacy"     aria-controls="Pharmacy"    role="tab" data-toggle="tab"><i class="fa fa-tint fa-lg" aria-hidden="true"></i> Pharmacy</a></li>
                 </ul>
             </div><!-- .col-md-12 -->
            
          <div class="col-md-12 col-xs-12">  

                 <div class="tab-content">    

                      <div  role="tabpanel" class="tab-pane active" id="encounter">
                         <div class="col-md-12" style="padding-top:20px">
                             
                             <div class="col-md-12">
                                 <div class="wizard" data-initialize="wizard" id="myWizard">
                                    <div class="steps-container">
	                                    <ul class="steps">
		                                   
                                            <li data-step="1" data-name="template" class="active">
			                                    <span class="badge">1</span>Presenting Complaints
			                                    <span class="chevron"></span>
		                                    </li>
                                            
                                            <li data-step="2">
			                                    <span class="badge">2</span>Patient Chronic Illness
			                                    <span class="chevron"></span>
		                                    </li>
		                                    <li data-step="3" id="dsPatientExamination" data-name="">
			                                    <span class="badge">3</span>Patient Examination
			                                    <span class="chevron"></span>
		                                    </li>
                                            
		                                    <li data-step="4" id="dsPatientManagement" data-name="">
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
	                                    
	                                    <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
		                                   
                                             <div class="col-md-12">
                                          <div class="col-md-4">
	                                            <div class="col-md-12 form-group">
	                                              <div class="col-md-12"><label class="control-label  pull-left">Visit Date</label></div>
	                                              <div class="col-md-12">
		                                              <div class="datepicker" id="DateOfVisit">
		                                              <div class="input-group">
                                                          <asp:TextBox ID="VisitDate" runat="server" class="form-control input-sm" data-parsley-required="true"></asp:TextBox>
			                                              <%--<input class="form-control input-sm" id="VisitDate" type="text" runat="server" data-parsley-required="true" />--%>
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
	                                               <div class="col-md-12"><label class="control-label  pull-left">Visit Scheduled?</label></div>
	                                               <div class="col-md-12">
                                                       <asp:RadioButtonList ID="rblVisitScheduled" runat="server" RepeatDirection="Horizontal" data-parsley-mincheck="1">
                                                           <asp:ListItem Text="Yes" Value="1" />
                                                           <asp:ListItem Text="No" Value="0" />
                                                       </asp:RadioButtonList>
                                                       
		                                              <%--<label class="pull-left" style="padding-right:20px">
			                                              <input runat="server" id="scheduledYes" name="Scheduled" type="radio" value="1" ClientIDMode="Static" > Yes
		                                              </label>
		                                              <label class="pull-left">
			                                              <input runat="server" id="scheduledNo" name="Scheduled" type="radio" data-parsley-required="true" value="0" ClientIDMode="Static"> No
                                                        </label>--%>
	                                              </div>
                                              </div>
                                            </div>

                                          <div class="col-md-4">
	                                         <div class="col-md-12 form-group">
		                                          <div class="col-md-12"><label class="control-label  pull-left">Visit By</label></div>
		                                          <div class="col-md-12">
			                                          <asp:DropDownList runat="server" ID="ddlVisitBy" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="1" data-parsley-min-message="Value Required" />
		                                          </div>
	                                         </div>
                                        </div>
                                      </div>
                                            <div class="col-md-12"><hr /> </div>
                                            <%--to here--%>


                                            <div class="col-md-12 form-group">
                                                <%--<div class="col-md-1">
                                                    <h4 class="pull-left text-danger"><i class="fa fa-user-md fa-5x" aria-hidden="true"></i></h4>
                                                </div>--%>
                                                <div class="col-md-12">
                                                     <div class="col-md-6">
                                                         <%--<div class="col-md-12"><h1 class="text-primary pull-left"><small>Complaints & History of Complaints</small></h1></div>
                                                         <div class="col-md-12"><hr /></div>--%>
                                                        
                                                          <label class="control-label pull-left" for="complaints">Complaints Today</label>
                                                               <textarea runat="server" clientidmode="Static" id="complaints" class="form-control input-sm" placeholder="complaints...." rows="4"></textarea> 
                                                    </div>

                                                      <div class="col-md-6">
                                                            <%--<div class="col-md-12"><small class="muted pull-left"><strong>TB Screening and Nutrition Status</strong></small></div><div class="col-md-12"><hr /> </div> --%>
                                                            <div class="col-md-12  form-group">
                                                                <div class="col-md-6"><label class="control-label pull-left input-sm" for="tbscreeningstatus">TB Screening</label></div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="tbscreeningstatus" ClientIDMode="Static"/>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-12 form-group">
                                                                <div class="col-md-6"><label class="control-label pull-left input-sm" for="nutritionscreeningstatus">Nutrition Status</label></div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="nutritionscreeningstatus" ClientIDMode="Static"  />
                                                                     
                                                                </div>
                                                            </div>
                         
                                                     </div>
                                                </div>
                                            </div>
                                          
                                            <div class="col-md-12 form-group">
                                                <%--<div class="col-md-1"></div>--%>
                                                <div class="col-md-12">
                                                   <div class="col-md-12">
                                                        <div class="panel panel-info">

                                                        <div class="panel-body">
                                                            <div class="col-md-12"><h1 class=" control-label pull-left"><i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i> Adverse Event(s)</h1></div>
                                                            <div class="col-md-12"><hr /></div>
                                                            <div class="col-md-12 form-group">
                                                                 <div class="col-md-3">
                                                                      <div class="col-md-12"><label class="control-label pull-left" >Adverse event(s)</label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="adverseEvent" ClientIDMode="Static" placeholder="adverse event.."></asp:TextBox>
                                                                     </div>
                                                                 </div>
                                                                 <div class="col-md-3">
                                                                     <div class="col-md-12"><label class="control-label" >Medicine Causing A/E</label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="AdverseEventCause" ClientIDMode="Static" placeholder="cause..."></asp:TextBox>
                                                                     </div>
                                                                 </div>
                                                                 <div class="col-md-3">
                                                                     <div class="col-md-12"><label class="control-label pull-left" >Severity</label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlAdverseEventSeverity" ClientIDMode="Static"  />
                                                                     </div>
                                                                 </div>
                                                                 <div class="col-md-2">
                                                                     <div class="col-md-12"><label class="control-label pull-left" >Action</label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:TextBox runat="server" ID="AdverseEventAction" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="action.."></asp:TextBox>
                                                                     </div>
                                                                 </div>
                                                                <div class="col-md-1">
                                                                     <div class="col-md-12"><label class="control-label pull-left" ></label></div>
                                                                     <div class="col-md-12">
                                                                         <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddMilestones" onclick="AddAdverseReaction();">Add</button>
                                                                     </div>
                                                                 </div>
                                                             </div>
                                                        </div> <%--.panel-body--%>

                                                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                        <table id="dtlAdverseEvents" class="table table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">SeverityID</span></th>
                                                                    <th><span class="text-primary">Adverse Event</span></th>
                                                                    <th><span class="text-primary">Medicine Causing A/E</span></th>
                                                                    <th><span class="text-primary">Severity</span></th>
                                                                    <th><span class="text-primary">Action</span></th>
                                                                    <th><span class="text-primary"></span></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                            
                                                        </div>
                                                    
                                                    </div> <%--.panel--%>
                                                   </div>
                                                </div><%--col-md-11--%>
                                            </div>

                                            <div class="col-md-12 form-group">
                                                <%--<div class="col-md-1"></div>--%>
                                                <div class="col-md-12">
                                                   <div class="col-md-12">
                                                     <div class="panel panel-info">

                                                     <div class="panel-body">
                                                         <div class="col-md-12"><h1 class=" control-label pull-left"><i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i> Presenting Complaints</h1></div>
                                                            <div class="col-md-12"><hr /></div>
                                                         <div class="col-md-12">
                                                             <div class="col-md-4">
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-12"><label class="control-label  pull-left">Pregnancy Status</label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:DropDownList runat="server" ID="examinationPregnancyStatus" CssClass="form-control input-sm" ClientIDMode="Static" onChange="EnableDisableEDD();" />
                                                                     </div>
                                                                 </div>
                                                                  <div class="col-md-12 form-group">
                                                                      <div class="col-md-12"><label class="control-label  pull-left">Female LMP</label></div>
                                                                      <div class="col-md-12">
                                                                          <div class="datepicker fuelux" id="FemaleLMP">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="lmp" type="text" runat="server" onkeyup="EnableDisableEDD();" onblur="EnableDisableEDD();" />
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
                                                                     <div class="col-md-12"><label class="control-label  pull-left">EDD</label></div>
                                                                     <div class="col-md-12">
                                                                         <div class="datepicker fuelux" id="EDCD">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="ExpectedDateOfChildBirth" type="text" runat="server" />
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
                                                                       <div class="col-md-12"><label class="control-label  pull-left">ANC/PNC Profile</label></div>
                                                                       <div class="col-md-12">
                                                                           <asp:RadioButtonList ID="rblANCProfile" runat="server" RepeatDirection="Horizontal">
                                                                               <asp:ListItem Text="Yes" Value="1" />
                                                                               <asp:ListItem Text="No" Value="0" />
                                                                           </asp:RadioButtonList>

                                                                          <%--<label class="radio-custom radio-inline pull-left" data-initialize="radio">
                                                                              <input class="sr-only" name="ANCProfile" type="radio" value="1"> Yes
                                                                          </label>
                                                                          <label class="radio-custom radio-inline pull-left" data-initialize="radio">
                                                                              <input class="sr-only" name="ANCProfile" type="radio" value="0"> No
                                                                          </label>--%>
                                                                      </div>
                                                                  </div>
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-12"><label class="control-label  pull-left">On Family Planning</label></div>
                                                                      <div class="col-md-12">
                                                                          <asp:DropDownList runat="server" ID="onFP" ClientIDMode="Static" CssClass="form-control input-sm" onChange="showHideFPControls();" />
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group" id="divOnFP">
                                                                     <div class="col-md-12"><label class="control-label  pull-left">FP Method</label></div>
                                                                      <div class="col-md-12">
                                                                          <asp:DropDownList runat="server" ID="fpMethod" ClientIDMode="Static" CssClass="form-control input-sm"/>
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group" id="divNoFP" style="display:none">
                                                                     <div class="col-md-12"><label class="control-label  pull-left">Reason not on FP</label></div>
                                                                      <div class="col-md-12">
                                                                          <asp:DropDownList runat="server" ID="ddlNoFP" ClientIDMode="Static" CssClass="form-control input-sm"/>
                                                                      </div>
                                                                 </div>
                                                                 
                                                             </div>
                                                             <div class="col-md-4">
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-12"><label class="control-label  pull-left">CaCX Screeing</label></div>
                                                                      <div class="col-md-12">
                                                                          <asp:DropDownList runat="server" ID="cacxscreening" ClientIDMode="Static" CssClass="form-control input-sm" />
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-12"><label class="control-label  pull-left">STI Screeing</label></div>
                                                                      <div class="col-md-12">
                                                                          <asp:DropDownList runat="server" ID="stiScreening" ClientIDMode="Static" CssClass="form-control input-sm" />
                                                                      </div>
                                                                 </div>
                                                                 <div class="col-md-12 form-group">
                                                                      <div class="col-md-12"><label class="control-label  pull-left">STI Partner Notification</label></div>
                                                                      <div class="col-md-12">
                                                                          <asp:DropDownList runat="server" ID="stiPartnerNotification" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                      </div>
                                                                 </div>
                                                             </div>
                                                         </div>
                                                    </div><%-- .panel-body--%>
                                                </div>
                                                   </div>
                                                </div>
                                            </div>


                                        </div><%-- .data-step-1--%>
                                        
                                        <div class="step-pane sample-pane" data-step="2">
                                            <%--<div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Chronic Illness </strong></small></div> <div class="col-md-12"><hr /> </div>--%>  
                                            <div class="col-md-12">
                                                 
                                                 <div class="col-md-12">
                                                     <%--<div class="col-md-12"><hr /></div>--%> 
                                                     <div class="panel panel-info">
                                                     <div class="panel-body">
                                                     <div class="col-md-12 form-group"><label class="control-label pull-left">Chronic Illnesses & Comorbidities</label></div>
                                                    
                                                      <div class="col-md-12 form-group">
                                                              <div class="col-md-4 form-group">
                                                                  <div class="col-md-12"><label for="ChronicIllnessName" class="control-label pull-left">Illness</label></div>
                                                                  <div class="col-md-12">
                                                                      <asp:DropDownList runat="server" ID="ChronicIllnessName" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                  </div>
                                                              </div>

                                                              <div class="col-md-3 form-group">
                                                                   <div class="col-md-12"><label class="control-label pull-left">Current Treatment</label></div>
                                                                   <div class="col-md-12">
                                                                       <asp:TextBox runat="server" ID="illnessTreatment" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="treatment.."></asp:TextBox>
                                                                   </div>
                                                              </div>

                                                             <div class="col-md-2 form-group">
                                                                 <div class="col-md-12"><label class="control-label pull-left"> Dose</label></div>
                                                                 <div class="col-md-12">
                                                                     <asp:TextBox runat="server" ID="treatmentDose" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="dose.."></asp:TextBox>
                                                                 </div>
                                                             </div>
                                                             <div class="col-md-2 form-group">
                                                                  <div class="col-md-12"><label class="control-label pull-left"> Duration</label></div>
                                                                 <div class="col-md-12">
                                                                     <asp:TextBox runat="server" CssClass="form-control input-sm" ID="treatmentDuration" ClientIDMode="Static" placeholder="0"></asp:TextBox>
                                                                 </div>
                                                             </div>

                                                             <div class="col-md-1">
                                                                 <div class="col-md-12"><label class="control-label pull-left"><span class="fa fa-cog">Action</span></label></div>
                                                                 <div class="col-md-4">
                                                                     <%--<asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-info btn-lg fa fa-plus-circle">Add</asp:LinkButton>--%>
                                                                     <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddChronicIllness" onclick="AddChronicIllness();">Add</button>
                                                                 </div>
                                                             </div>
                                                         </div>

                                                     <div class="col-md-12 form-group">
                                                            <div class="panel panel-info">
                                                                 <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlChronicIllness" class="table table-bordered table-striped" width="100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th><span class="text-primary">IllnessID</span></th>
                                                                                <th><span class="text-primary">Illness</span></th>
                                                                                <th><span class="text-primary">Current Treatment</span></th>
                                                                                <th><span class="text-primary">Dose</span></th>
                                                                                <th><span class="text-primary">Duration</span></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                        
                                                                </div>
                                                             </div>
                                                         </div>

                                                     </div>
                                                     </div>
                                                     
                                                            <div class="panel panel-info">

                                                                <div class="panel-body">
                                                                     <div class="col-md-12 form-group"><label class="control-label pull-left">Antigen Today</label></div>
                                                                     <%--<div class="col-md-12"><hr/></div>--%>

                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-4">
                                                                            <div class="col-md-12"><label class="control-label pull-left">Vaccine</label></div>
                                                                            <div class="col-md-12">
                                                                                <asp:DropDownList ID="ddlVaccine" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" ></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="col-md-12"><label class="control-label pull-left">Vaccine Stage</label></div>
                                                                            <div class="col-md-12">
                                                                                <asp:DropDownList ID="ddlVaccineStage" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="col-md-12"><label class="control-label  pull-left">Vaccination Date</label></div>
	                                                                            <div class="col-md-12">
		                                                                            <div class="datepicker fuelux" id="vaccineDate">
		                                                                            <div class="input-group">
			                                                                            <input class="form-control input-sm" id="txtVaccinationDate" type="text" runat="server" ClientIDMode="Static" />
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
                                                                        <div class="col-md-1">
                                                                            <div class="col-md-12"><label class="control-label pull-left">Action</label></div>
                                                                            <div class="col-md-12">
                                                                                <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddVaccine" onclick="AddVaccine();" >Add</button>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12 form-group">
                                                                        <div class="panel panel-info">
                                                                             <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                                                <table id="dtlVaccines" class="table table-bordered table-striped" width="100%">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th><span class="text-primary">VaccineID</span></th>
                                                                                            <th><span class="text-primary">VaccineStageID</span></th>
                                                                                            <th><span class="text-primary">Vaccine</span></th>
                                                                                            <th><span class="text-primary">Vaccine Stage</span></th>
                                                                                            <th><span class="text-primary">Vaccination Date</span></th>
                                                                                            <th></th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                </table>
                                                                                    
                                                                            </div>
                                                                         </div>
                                                                     </div>
                                                            
                                                                </div><%-- .panel-body--%>

                                                            </div> <%--.panel--%>
                                                      <%--</div>--%><%-- .col-md-12--%>
                                                  </div>

                                            </div>

                                        </div><%-- .data-step-2--%>
	                                    
                                        <div class="step-pane sample-pane" data-step="3">
                                             <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Examination</strong></small></div> <div class="col-md-12"><hr /> </div>  
                                             <div class="col-md-12">
                                                 <div class="col-md-1">
                                                     <h4 class="pull-left text-warning"><i class="fa fa-search fa-5x" aria-hidden="true"></i></h4>
                                                 </div>
                                                 <div class="col-md-11">
                                                     <div class="col-md-12 form-group">
                                                              <div class="col-md-3 form-group">
                                                                  <div class="col-md-12"><label for="ChronicIllnessName" class="control-label pull-left">Examination Type</label></div>
                                                                  <div class="col-md-12">
                                                                      <asp:DropDownList runat="server" ID="ddlExaminationType" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                  </div>
                                                              </div>

                                                              <div class="col-md-3 form-group">
                                                                   <div class="col-md-12"><label class="control-label pull-left">Examination</label></div>
                                                                   <div class="col-md-12">
                                                                       <asp:DropDownList runat="server" ID="ddlExamination" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                   </div>
                                                              </div>

                                                             <div class="col-md-5 form-group">
                                                                 <div class="col-md-12"><label class="control-label pull-left"> Findings</label></div>
                                                                 <div class="col-md-12">

                                                                     <asp:TextBox runat="server" ID="txtExamFindings" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="Findings.." Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                                 </div>
                                                             </div>

                                                             <div class="col-md-1">
                                                                 <div class="col-md-12"><label class="control-label pull-left"><span class="fa fa-cog">Action</span></label></div>
                                                                 <div class="col-md-4">
                                                                     <%--<asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-info btn-lg fa fa-plus-circle">Add</asp:LinkButton>--%>
                                                                     <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddPhysicalExam" onclick="AddPhysicalExam();">Add</button>
                                                                 </div>
                                                             </div>
                                                         </div>

                                                        <div class="col-md-12 form-group">
                                                            <div class="panel panel-info">
                                                                 <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlPhysicalExam" class="table table-bordered table-striped" width="100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th><span class="text-primary">ExaminationTypeID</span></th>
                                                                                <th><span class="text-primary">ExaminationID</span></th>
                                                                                <th><span class="text-primary">Examination Type</span></th>
                                                                                <th><span class="text-primary">Examination</span></th>
                                                                                <th><span class="text-primary">Findings</span></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                     
                                                                </div>
                                                             </div>
                                                         </div>
                                                    
                                                 </div>

                                                 <%--<div class="col-md-1"></div>
                                                 <div class="col-md-11">
                                                     <div class="col-md-12">
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
                                                 </div>--%>
                                             </div>
                                             
	                                    </div><%-- .data-step-3--%>
                                        
                                        <div class="step-pane sample-pane" data-step="4">
                                             <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT MANAGEMENT</strong></small></div> <div class="col-md-12"><hr /> </div>  
                                             
                                            <div class="col-md-1">
                                                <h4 class="pull-left text-danger"><i class="fa fa-bed fa-5x" aria-hidden="true"></i></h4>
                                            </div> 
                                            <div class="col-md-11">
                                                <div class="col-md-12">
                                                  
                                                       <div class="col-md-4">
                                                       <h1 class="col-md-12">Positive Health,Dignity & Prevention (PHDP)</h1>
                                                       <div class="col-md-12"><hr /></div>
                                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                               <ContentTemplate>
                                                                   <div class="form-group col-md-12" style="text-align:left">
                                                                       <asp:CheckBoxList ID="cblPHDP" runat="server" ></asp:CheckBoxList>
                                              
                                                                    </div>     
                                                               </ContentTemplate>
                                                           </asp:UpdatePanel>                         
                                                  </div>

                                                       <div class="col-md-8">
                                                            
                                                            <h1 class="col-md-12"> Patient Diagnosis and Treatment</h1>
                                                            <div class="col-md-12"><hr /></div>
                                                            
                                                            <div class="col-md-12">
                                                                 <div class="col-md-6"><label class="control-label pull-left">Diagnosis (ICD 10 Codes)</label></div>
                                                                 <div class="col-md-5"><label class="control-label pull-left">Treatment</label></div>
                                                                 <div class="col-md-1"><label class="control-label pull-left">Action</label></div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                 <div class="col-md-6 form-group">
                                                                      <input type="text" id="Diagnosis" class ="form-control input-sm" placeholder="Type Diagnosis......" runat="server" ClientIDMode="Static" />
                                                                 </div>
                                                                
                                                                 <div class="col-md-5 form-group">
                                                                     <input type="text" id="DiagnosisTreatment" class ="form-control input-sm" placeholder="treatment" runat="server" ClientIDMode="Static" />
                                                                 </div>
                                                                <div class="col-md-1 form-group">
                                                                    <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddDiagnosis" onclick="AddDiagnosis();">Add</button>
                                                                </div>
                                                            </div>

                                                           <div class="col-md-12 form-group">
                                                            <div class="panel panel-info">
                                                                 <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlDiagnosis" class="table table-bordered table-striped" width="100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th><span class="text-primary">Diagnosis</span></th>
                                                                                <th><span class="text-primary">Treatment</span></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                   
                                                                </div>
                                                             </div>
                                                         </div>


                                                            <%--<div class="col-md-12">
                                                                 <div class="col-md-12">
                                                                      <div class="panel panel-default">
			                                                          <div class="panel-heading">Patient Diagnosis Today</div>
			                                                              <div class="panel-body">
		   
			                                                              </div>
                                                                      </div>
                                                                 </div> 
                                                            </div>--%>
                                                 </div>
                                                   
                                                  </div><%-- .col-md-11--%>
                                                <div class="col-md-12"><hr /></div>
                                                <div class="col-md-12">
                                                     
                                                    <div class="col-md-5">
                                                          
                                                          <div class="col-md-12 form-group">
                                                              <div class="col-md-6"><label class="control-label pull-left">ARV Adherence</label></div>
                                                               <div class="col-md-6">
                                                                    <asp:DropDownList runat="server" ID="arvAdherance" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                              </div>
                                                          </div><%-- .col-md-12--%>

                                                        <div class="col-md-12 form-group">
                                                              <div class="col-md-6"><label class="control-label pull-left">CTX/Dapsone Adherence</label></div>
                                                              <div class="col-md-6">
                                                                  <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ctxAdherance" ClientIDMode="Static"/>
                                                              </div>
                                                         </div>
                                                    </div>
                                                    <div class="col-md-7">
                                                          

                                                          <div class="col-md-12">
                                                              <%--<div class="col-md-12"><hr /></div>--%>
                                                              <div class="col-md-12">
                                                                   <div class="col-md-5"><label class="control-label pull-left">Next Appointment </label></div>
                                                                   <div class="col-md-7">
                                                                        <div class="datepicker fuelux form-group" id="NextAppDate">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="NextAppointmentDate" type="text" runat="server" />
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
                                                                       <%--<input type="text" class="form-control input-sm" id="ReferredFor" placeholder="referred for" runat="server" />--%>
                                                                       <asp:DropDownList ID="ddlReferredFor" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                                                   </div>
                                                              </div>
                                                              
                                                              
                                                          </div>
                                                     </div><%-- .col-md-4--%>
                                                     <%--<div class="col-md-8"></div>--%>
                                                 
                                                </div> <%--.col-md-12--%>
                                            </div> <%--.col-md-11--%>
                                            
                                        </div><%-- .data-step-4--%>


                                 </div><%-- .wizard--%>
                             </div> <%--.col-md-12--%>
                         </div>
                      </div><!-- .encounter-->
                    </div>                     
               
              <div  role="tabpanel"  class="tab-pane fade" id="vlTracker">
                    <!-- pw implementation of viral load tracker here-->

                      <div class="col-md-6">      
                       <div class="col-md-12 bs-callout bs-callout-danger">
                                <h4 class="pull-left"> <strong>Pending VL Test(s):</strong> </h4>

                           
                            <table class="table table-striped table-condensed" id="tblVlpending" clientidmode="Static" runat="server">
                                                <thead>
                                                    <tr >
                                                        <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                                                         <th> <i class="control-label text-warning pull-right" aria-hidden="true">VL Test</i> </th>
                                                          <th> <i class="control-label text-warning pull-right" aria-hidden="true">Test Reason</i> </th>
                                                         <th> <i class="control-label text-warning pull-right " aria-hidden="true">Test Date</i> </th>
                                                         <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Status </i></th>
                                                        
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
                                                    <tr >
                                                          <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                                                         <th> <i class="control-label text-warning pull-right" aria-hidden="true">VL Test</i> </th>
                                                          <th> <i class="control-label text-warning pull-right" aria-hidden="true">Test Reason</i> </th>
                                                         <th> <i class="control-label text-warning pull-right " aria-hidden="true">Test Date</i> </th>
                                                         <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Status </i></th>
                                                        
                                                          
                                                          </tr>
                                                     </thead>
                                                   <tbody>                        
                                                  </tbody>                  
                                                </table>
                                            </div> 
                                               
                                    </div>
                       
                                   
                             <div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div> 
                                                            

                      <!-- pw .implementation of viral load tracker line graph here-->
                </div><!-- .viraload tracker-->
                    
             <div  role="tabpanel" class="tab-pane fade" id="Laboratory">
                                               
                           
                     <div class="col-md-6 col-sx-12 col-sm-12">  
                          <div class="col-md-12 bs-callout bs-callout-danger">
                                <h4 class="pull-left"> <strong>Pending Labs:</strong> </h4>                           
                                <table class="table table-striped table-condensed" id="tblPendingLabs" clientidmode="Static" runat="server">
                                                <thead>
                                                    <tr >
                                                        <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                                                         <th> <i class="control-label text-warning pull-right" aria-hidden="true">Lab Test</i> </th>
                                                          <th> <i class="control-label text-warning pull-right" aria-hidden="true">Order Reason</i> </th>
                                                         <th> <i class="control-label text-warning pull-right " aria-hidden="true">Order Date</i> </th>
                                                         <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Status </i></th>
                                                         </tr>
                                                </thead>
                                                <tbody>                        
                                                </tbody>                  
                                                </table>
                            </div>    
        

                         <div class="col-md-12 bs-callout bs-callout-info">
                         <h4 class="pull-left"> <strong>Complete Labs:</strong> </h4>    
                      <!--pw implementation of previous labs laboratory module here  previous orders-->
                                        
                                        <div class="col-md-12 form-group">
                                              <table class="table table-striped table-condensed" id="tblPrevLabs" clientidmode="Static" runat="server">
                                                <thead>
                                                    <tr >
                                                         <th> <i class="control-label text-warning pull-left" aria-hidden="true"> # </i> </th>
                                                         <th> <i class="control-label text-warning pull-left" aria-hidden="true"> Lab Test</i> </th>
                                                         <th> <i class="control-label text-warning pull-left " aria-hidden="true"> Order Reason</i> </th>
                                                        <th> <i class="control-label text-warning pull-left" aria-hidden="true"> Order Date </i></th>
                                                         <th> <i class="control-label text-warning pull-left" aria-hidden="true"> Order Status </i></th>
                                                        
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
                                                         
                                                          <asp:TextBox runat="server" Width="200" ID="labTestTypes" data-provide="typeahead" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="type to select...."></asp:TextBox>
                                                      </div>
                                                  </div>

                                                  <div class="col-md-12 form-group">
                                                                      <div class="col-md-4"><label class="control-label  pull-left">Reason</label></div>
                                                                     <div class="col-md-8">
                                                                         <asp:DropDownList runat="server" ID="orderReason" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                     </div>
                                                         </div>
                                                  
                                                 <div class="col-md-12 form-group">
                                                       <div class="col-md-4"><label class="control-label pull-left">Lab Notes</label></div>
                                                      <div class="col-md-8">
                                                          <asp:TextBox runat="server" ID="labNotes" Rows="4" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="laboratory notes...."></asp:TextBox>
                                                      </div>
                                                  </div>
                                 <!--datepicker  -->
                              
                                   <div class="col-md-12 form-group">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left">Date</label>
                                        </div>
                                        <div class="col-md-8">
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
                                                      <div class="col-md-10"></div>
                                                      <div class="col-md-3 pull-right ">
                                            <asp:LinkButton runat="server" ID="btnAddLab"  ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle "> Add Lab</asp:LinkButton>
                                                        
                                                      </div>
                                                      <div></div>
                                                  </div>
                                                  
                                                 <div class="col-md-12 form-group">                                         
                                            <table class="table table-striped table-condensed" id="tblAddLabs" clientidmode="Static" runat="server">
                                                <thead>
                                                     
                                                    <tr >
                                                         <th> <i class="control-label text-warning pull-left" aria-hidden="true"> # </i> </th>
                                                         <th> <i class="control-label text-warning pull-left" aria-hidden="true"> Lab Test</i> </th>
                                                         <th> <i class="control-label text-warning pull-left " aria-hidden="true"> Order Reason</i> </th>
                                                        <th> <i class="control-label text-warning pull-left" aria-hidden="true"> Order Date </i></th>
                                                        
                                                    </tr>
                                                  </thead>
                                                  <tbody>                        
                                                 </tbody>                  
                                                 </table>
                                            </div>
                                                              
   
                                                               <%-- </div>--%>
                             </div><%-- panel body--%>
                        </div>     
                    
                     <div class="col-md-12"><hr/></div>
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
                        
                      <div  role="tabpanel" class="tab-pane fade" id="Pharmacy">
                     <div class="col-md-12" style="padding-top: 1%">
                              <label class="control-label pull-left"> <i class="fa fa-tint fa-lg" aria-hidden="true"></i> Drup Prescription </label>
                          </div>
                          <div class="col-md-12 bs-callout bs-callout-danger">
                                <h4 class="pull-left"> <strong>Pending Dispensing </strong></h4>
                            </div>
                          <div class="col-md-12">
                                <div class="panel panel-info">

                                    <div class="panel-body">
                                         <div class="col-md-12">
                                              <div class="col-md-4">
                                                  <div class="col-md-12"><label class="control-label pull-left"> Last Drug Prescription</label></div>
                                                  <div class="col-md-12"><hr/></div>
                                              </div>
                                              <div class="col-md-8">
                                                  <div class="col-md-12"><label class="control-label pull-left"> Drup Prescription </label></div>
                                                  <div class="col-md-12"><hr /></div>

                                                         <div class="col-md-12 form-group">  
                                                              <div class="col-md-4"><label class="control-label pull-left">Subsitututions/Switches/Interuptions </label></div>
                                                              <div class="col-md-6 pull-right">
                                                                   <asp:DropDownList runat="server" CssClass="form-control input-sm " id="PrescriptionOption" ClientIDMode="Static"/>
                                                               </div>
                                                              <div class="col-md-2"></div>
                                                         </div>   
                                                         <div class="col-md-12 form-group">
                                                           <div class="col-md-4"><label class="control-label pull-left">Drug Classification </label></div>
                                                           <div class="col-md-6  pull-right">
                                                               <asp:DropDownList runat="server" id="drugCategory" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                           </div>
                                                           <div class="col-md-2"></div>
                                                         </div> <%--.col-md-12--%>

                                                         <div class="col-md-12 form-group">
                                                             
                                                              <div class="col-md-4"><label class="control-label pull-left">Regimen Line </label></div>     
                                                              <div class="col-md-6  pull-right">
                                                                   <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="regimenLine" ClientIDMode="Static"  onchange="getPharmacyDrugList();"/>
                                                              </div>
                                                              <div class="col-md-2 "></div>
                                                         </div>  
                                                        
                                                         <div class="col-md-12 bs-callout bs-callout-info">
                                                              <div class="row">
                                                                    <div class="col-md-12">
                                                                         
                                                                              <div class="col-md-3"><label class="control-label pull-left">Select Drug (s) </label></div>
                                                                              <div class="col-md-7">
                                                                                  <input type="text" data-provide="typeahead" id="txtSelectDrug" class="form-control input-sm" placeholder="type to search...." runat="server" />
                                                                              </div>
                                                                             <div class="col-md-2">
                                                                               <asp:LinkButton runat="server" CssClass="btn btn-warning btn-lg fa fa-plus-circle"> Add Drug</asp:LinkButton> 
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
                                                                       <div class="col-md-4"><label class="control-label pull-left text-warning">--Selected drug--</label></div>
                                                                        <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="Dose" /> </div>
                                                                        <div class="col-md-2"><select class="form-control input-sm" id="Frequency" runat="server"></select></div>
                                                                       <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="Quantity" /> </div>
                                                                       <div class="col-md-2"><input type="text" class="form-control input-sm" runat="server" id="Duration" /> </div>
                                                                    </div>
                                                                 </div>
                                                         </div>            
                                                     

                                              </div>
                                         </div>
                                         
                                        <div class="col-md-12">
                                              <div class="col-md-12"><hr/></div>
                                              <div class="col-md-6"></div>
                                             <div class="col-md-6">
                                                <div class="col-md-3"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-info btn-sm fa fa-plus-circle"> Save Prescription</asp:LinkButton></div>
                                                 <div class="col-md-3"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-primary btn-sm  fa fa-print"> Print Prescription</asp:LinkButton></div>
                                                 <div class="col-md-3"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-warning btn-sm fa fa-refresh"> Reset Prescription</asp:LinkButton></div>
                                                <div class="col-md-3"><asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-danger btn-sm  fa fa-times"> Close Prescription</asp:LinkButton></div>
                                             </div>
                                        </div>
                                    </div><%-- .panel-body--%>

                                </div><%-- .panel--%>

                          </div><%-- .col-md-12--%>
                </div><!-- .pharmacy-->
                      
              </div><!-- .laboratory-->  <!--end pw implementation of  laboratory module here-->
                  
              

                       <%--<div  role="tabpanel"    class="tab-pane fade"      id="history">
                           
                       </div>--%><!-- .history-->
                 <%--</div>--%><!-- .tab-content-->
           </div> 

    <!-- ajax begin -->
   <script type="text/javascript">
       var patientId = <%=PatientId%>;
       var patientMasterVisitId = <%=PatientMasterVisitId%>;
       var genderId = <%=genderID%>;
       var gender = "<%=gender%>";

     $(document).ready(function () {     
           

         //console.log(patientId);
         //console.log(patientMasterVisitId);
         //console.log(genderId);
         //console.log(gender);

     $("#LabDatePicker").datepicker({
           //date: null,
           allowPastDates: true,
           momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
     });
     var LabOrderList = new Array();
        
      $.ajax({
               type: "POST",
               url: "../WebService/LabService.asmx/GetLookupPreviousLabsList",
               data: "{'patient_ID':'" + patientId + "'}",
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
                       var month = currentTime.getMonth() + 1;
                       var day = currentTime.getDate();
                       var year = currentTime.getFullYear();
                       var sampleDate = day + "/" + month + "/" + year;
                      
                       table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';
                   
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
               data: "{'patient_ID':'" + patientId + "'}",
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
                       var month = currentTime.getMonth() + 1;
                       var day = currentTime.getDate();
                       var year = currentTime.getFullYear();
                       var sampleDate = day + "/" + month + "/" + year;
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
               data: "{'patient_ID':'" + patientId + "'}",
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
                       var month = currentTime.getMonth() + 1;
                       var day = currentTime.getDate();
                       var year = currentTime.getFullYear();
                       var sampleDate = day + "/" + month + "/" + year;
                       // alert(date);

                       table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';
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
            data: "{'patient_ID':'" + patientId + "'}",
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
                    var month = currentTime.getMonth() + 1;
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var sampleDate = day + "/" + month + "/" + year;
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
                   var labtests = [];
                   for (var i = 0; i < serverData.length; i++) {

                       labtests.push(serverData[i]["ParameterName"]);
                   }

                  // console.log(labtests);
                   awesomplete.list = labtests;
                      }
                 });
           
        });
      
      
         // Load lab order
       $("#btnAddLab").click(function (e) {

           var labOrderFound = 0;

               var labOrderDate = $("#<%=LabDate.ClientID%>").val();
               var labType = $("#labTestTypes").val();
               var labOrderReason = $("#orderReason").find(":selected").text();
               var labOrderNotes = $("#labNotes").val();

               if (labType < 1) {
                  toastr.error("Please select at least One(1) Lab Type from the List");
                   return false;
               }
               if (labOrderReason < 1) {
                   toastr.error("Please select at least One(1) Lab Order Reason from the List");
                   return false;
               }

               labOrderFound = $.inArray("" + labType + "", LabOrderList);

           if (labOrderFound > -1) {

               toastr.error("error", labType + " Lab selected already exists in the List");
               return false; // message box herer
           }
           if (labOrderDate < 1) {
                   toastr.error("Please input a date for the lab order");
                   return false;
               }

               else {

                  
               LabOrderList.push("" + labType + "");
                   var tr = "<tr><td></td><td align='left'>" + labType + "</td><td align='left'>" + labOrderReason + "</td><td align='left'>" + labOrderDate + "</td><td visibility: hidden>" + labOrderNotes + "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                   $("#tblAddLabs>tbody:first").append('' + tr + '');
                   resetLabOrder();
               }

               e.preventDefault();
            });

        $("#tblAddLabs").on('click', '.btnDelete', function () {
             $(this).closest('tr').remove();
             var x = $(this).closest('tr').find('td').eq(0).html();

             //identifierList.splice($.inArray(x, identifierList), 1);
             //enrollmentNoList.splice($.inArray(x, enrollmentNoList), 1);
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
             $("#LabDate").val("");
         }
        

           // Save lab order
      $("#btnSaveLab").click(function (e) {
               var _fp = [];
               var data = $('#tblAddLabs tr').each(function (row, tr) {


                   _fp[row] = {
                       "labType": $(tr).find('td:eq(1)').text()
                     , "orderReason": $(tr).find('td:eq(2)').text()
                     , "labOrderDate": $(tr).find('td:eq(3)').text()
                    , "labNotes": $(tr).find('td:eq(4)').text()

                   }
               });
               _fp.shift();

               if ($.isEmptyObject(_fp)) {
                   toastr.error("You have not added any lab order");
                 
                   return false;
               } else {
                  
                   addLabOrder(_fp);
               }

               $("#tblAddLabs td").parent().remove();
           });


        function addLabOrder(_fp) {
               var labOrder = JSON.stringify(_fp);
              // console.log(patientId);
               //console.log(labOrder);
               $.ajax({
                   type: "POST",

                   url: "../WebService/LabService.asmx/AddLabOrder",
                   data: "{'patient_ID':'" + patientId + "','patientMasterVisitId':'" + patientMasterVisitId + "','patientLabOrder': '" + labOrder + "'}",
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {

                       toastr.success(response.d, "Lab order successful");
                   },
                   error: function (response) {
                       //generate('error', response.d);
                       toastr.error(response.d, "Lab order unsuccessful");
                   }
               });
           };
           $(function () {
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
                       plotLines: [{
                           value: 0,
                           width: 1,
                           color: '#808080'
                       }]
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
                   series: [{
                       name: 'VL',
                       data: [200, 300, 500, 1000, 750, 500, 400]
                   }, {
                       name: 'Threshold',
                       data: [1000, 1000, 1000, 1000, 1000, 1000, 1000]
                   }]
               });
           });

           
           /////////////////////////////////PATIENT ENCOUNTER////////////////////////////////////////////////
           var getVisitDateVal = "<%= this.visitdateval %>";
           var getFemaleLMPVal = "<%= this.LMPval %>";
           var getEDDPVal = "<%= this.EDDval %>";
           var getNxtAppDateVal = "<%= this.nxtAppDateval %>";
    

           if (getVisitDateVal == '')
               getVisitDateVal = new Date();

           if (getFemaleLMPVal == '')
               getFemaleLMPVal = new Date();

           if (getEDDPVal == '')
               getEDDPVal = new Date();

           if (getNxtAppDateVal == '')
               getNxtAppDateVal = new Date();
         //Date processing
           var today = new Date();
           var tomorrow = new Date();
           tomorrow.setDate(today.getDate() + 1);

            $('#DateOfVisit').datepicker({
                allowPastDates: true,
                date: getVisitDateVal,
                restricted: [{from: tomorrow, to: Infinity}],
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#OnsetDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
           
            $('#FemaleLMP').datepicker({
                allowPastDates: true,
                date: getFemaleLMPVal,
                restricted: [{from: tomorrow, to: Infinity}],
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#EDCD').datepicker({
                allowPastDates: true,
                date: getEDDPVal,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#AntigenDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#NextAppDate').datepicker({
                allowPastDates: false,
                date: getNxtAppDateVal,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#vaccineDate').datepicker({
                allowPastDates: true,
                date: 0,
                restricted: [{from: tomorrow, to: Infinity}],
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            
         ////////////////////////////////////////////////////////////////////////////////////////////
         //Gender validations
            var male = "Male";
            if (gender == male) {
                
                $("#lmp").val("");
                $("#examinationPregnancyStatus").val("");
                $("#ExpectedDateOfChildBirth").val("");
                $("#cacxscreening").val("");

                $("#<%=lmp.ClientID%>").prop('disabled', true);
                $("#<%=examinationPregnancyStatus.ClientID%>").prop('disabled', true);
                $("#<%=ExpectedDateOfChildBirth.ClientID%>").prop('disabled', true);
                $("#<%=cacxscreening.ClientID%>").prop('disabled', true);
                } else {
                $("#<%=lmp.ClientID%>").prop('disabled', false);
                $("#<%=examinationPregnancyStatus.ClientID%>").prop('disabled', false);
                $("#<%=ExpectedDateOfChildBirth.ClientID%>").prop('disabled', false);
                 $("#<%=cacxscreening.ClientID%>").prop('disabled', false);

                }
         //.gender validation
         //pregnancy validations
       
         var pregnant = "Pregnant";

         var pregnancy = $("#<%=examinationPregnancyStatus.ClientID%>").find(':selected').text();
         //console.log(patientId);
         //console.log(pregnancy);

         if (pregnancy != pregnant) {
                
              $("#<%=ExpectedDateOfChildBirth.ClientID%>").prop('disabled', true);
               
                } else {
                
                $("#<%=ExpectedDateOfChildBirth.ClientID%>").prop('disabled', false);
              

                }
         //.pregnancy validation
        var advEventsTable = $('#dtlAdverseEvents').DataTable({
                ajax: {
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/GetAdverseEvents",
                    dataSrc: 'd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                paging: false,
                searching: false,
                info: false,
                ordering: false,
                columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
                    ]
            });


           var chronicTable = $('#dtlChronicIllness').DataTable({
               ajax: {
                   type: "POST",
                   url: "../WebService/PatientEncounterService.asmx/GetChronicIllness",
                   dataSrc: 'd',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json"
               },
                paging: false,
                searching: false,
                info: false,
                ordering: false,
                columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
                ]
            });

           var vaccineTable = $('#dtlVaccines').DataTable({
               ajax: {
                   type: "POST",
                   url: "../WebService/PatientEncounterService.asmx/GetVaccines",
                   dataSrc: 'd',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json"
               },
                paging: false,
                searching: false,
                info: false,
                ordering: false,
                columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [1],
                    "visible": false,
                    "searchable": false
                }
                ]
            });

           var examTable = $('#dtlPhysicalExam').DataTable({
               ajax: {
                   type: "POST",
                   url: "../WebService/PatientEncounterService.asmx/GetPhysicalExam",
                   dataSrc: 'd',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json"
               },
                paging: false,
                searching: false,
                info: false,
                ordering: false,
                columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [1],
                    "visible": false,
                    "searchable": false
                }
                ]
            });

           var diagnosisTable = $('#dtlDiagnosis').DataTable({
               ajax: {
                   type: "POST",
                   url: "../WebService/PatientEncounterService.asmx/GetDiagnosis",
                   dataSrc: 'd',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json"
               },
                paging: false,
                searching: false,
                info: false,
                ordering: false
            });

            var index;

            $("#dtlAdverseEvents").on('click',
                '.btnDelete',
                function () {
                    advEventsTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();
                    //$(this).closest('tr').remove();
                    //var y = $(this).closest('tr').find('td').eq(0).html();
                    //index = arrAdverseEventUI.findIndex(x => x.adverseEvent == y);
                    //if (index > -1) {
                    //    arrAdverseEventUI.splice(index, 1);
                    //}
                });

            ////dtlChronicIllness
            $("#dtlChronicIllness").on('click',
                '.btnDelete',
                function () {
                    chronicTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();
                });

            ////dtlVaccines
            $("#dtlVaccines").on('click',
                '.btnDelete',
                function () {
                    vaccineTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();
                   
                });


            ////dtlPhysicalExam
            $("#dtlPhysicalExam").on('click',
                '.btnDelete',
                function () {
                    examTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();
                    
                });

            ////dtlDiagnosis
            $("#dtlDiagnosis").on('click',
                '.btnDelete',
                function () {
                    diagnosisTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();
                    
                });
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            //$('#myWizard').wizard();
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

                            /* add constraints based on age*/

                            if ($('#datastep1').parsley().validate()) {
                                savePatientEncounterPresentingComplaint();
                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                        else if (data.step === 2) {
                            savePatientEncounterChronicIllness();
                            //if ($("#datastep2").parsley().validate()) {

                            //} else {
                            //    stepError = $('.parsley-error').length === 0;
                            //    totalError += stepError;
                            //    evt.preventDefault();
                            //}
                        }
                        else if (data.step === 3) {
                            savePatientPhysicalExams();
                            //if ($("#datastep3").parsley().validate()) {

                            //} else {
                            //    stepError = $('.parsley-error').length === 0;
                            //    totalError += stepError;
                            //    evt.preventDefault();
                            //}
                        }
                        else if (data.step === 4) {
                            $.when(savePatientPatientManagement()).then(function() {
                                setTimeout(function() {
                                    window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';
                                },
                                    2000);
                            });

                            //savePatientPatientManagement();
                            //
                            //if ($("#datastep2").parsley().validate()) {

                            //} else {
                            //    stepError = $('.parsley-error').length === 0;
                            //    totalError += stepError;
                            //    if (totalError > 0) {
                            //        $('.bs-callout-danger').toggleClass('hidden', f);
                            //    }
                            //    evt.preventDefault();
                            //}
                            ////var ok4 = $('.parsley-error').length === 0;
                            ////$('.bs-callout-info').toggleClass('hidden', !ok4);
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

                        });

            function savePatientEncounterPresentingComplaint() {

             

                var visitDate = $("#<%=VisitDate.ClientID%>").val();
                //var visitScheduled = $('input[name="Scheduled"]:checked').val();
                ////////////////////////////////////////
                var rblVS = '<%= rblVisitScheduled.ClientID %>';
                var rblANC = '<%= rblANCProfile.ClientID %>';
                
                var listVS = document.getElementById(rblVS); //Client ID of the radiolist
                var listANC = document.getElementById(rblANC);
                var inputsVS = listVS.getElementsByTagName("input");
                var inputsANC = listANC.getElementsByTagName("input");
                var visitScheduled;
                var ANCProfile;
                for (var i = 0; i < inputsVS.length; i++) {
                    if (inputsVS[i].checked) {
                        visitScheduled = inputsVS[i].value;
                        break;
                    }
                }

                for (var i = 0; i < inputsANC.length; i++) {
                    if (inputsANC[i].checked) {
                        ANCProfile = inputsANC[i].value;
                        break;
                    }
                }


                /////////////////////////////////////////////
                if (visitScheduled == undefined)
                {
                    alert("Kindly select if visit was scheduled.");
                    var specificField = $(rblVS).parsley();
                    // add the error
                    window.ParsleyUI.addError(specificField, "myCustomError", 'this is a custom error message');
                    // remove the error
                    window.ParsleyUI.removeError(specificField, "myCustomError");

                    //window.ParsleyUI.addError(rblVS, "Visit Scheduled", "required");
                }
           
                if (ANCProfile == undefined)
                {
                    ANCProfile = "99";
                }
                    

                var visitBy = $("#<%=ddlVisitBy.ClientID%>").find(":selected").val();
                var complaints = $("#<%=complaints.ClientID%>").val();
                var tbscreening = $("#<%=tbscreeningstatus.ClientID%>").find(":selected").val();
                var nutritionscreening = $("#<%=nutritionscreeningstatus.ClientID%>").find(":selected").val();
                var LMP = $("#<%=lmp.ClientID%>").val();
                var pregStatus = $("#<%=examinationPregnancyStatus.ClientID%>").find(":selected").val();
                var EDD = $("#<%=ExpectedDateOfChildBirth.ClientID%>").val();
                //var ANCProfile = $('input[name="ANCProfile"]:checked').val();
                var onFP = $("#<%=onFP.ClientID%>").find(":selected").val();
                var FPMethod = $("#<%=fpMethod.ClientID%>").find(":selected").val();

                var CaCx = $("#<%=cacxscreening.ClientID%>").find(":selected").val();
                var STIScreening = $("#<%=stiScreening.ClientID%>").find(":selected").val();
                var STIPartnerNotification = $("#<%=stiPartnerNotification.ClientID%>").find(":selected").val();

                ///////////////////////////////////////////////////////
                var rowCount = $('#dtlAdverseEvents tbody tr').length;
                var adverseEventsArray = new Array();
                try {
                    for (var i = 0 ; i < rowCount; i++) {
                        adverseEventsArray[i] = {
                            "adverseSeverityID": advEventsTable.row(i).data()[0],
                            "adverseEvent": advEventsTable.row(i).data()[1],
                            "medicineCausingAE": advEventsTable.row(i).data()[2],
                            "adverseSeverity": advEventsTable.row(i).data()[3],
                            "adverseAction": advEventsTable.row(i).data()[4]
                        }
                    }
                }
                catch (ex) {  }

                    $.ajax({
                        type: "POST",
                        url: "../WebService/PatientEncounterService.asmx/savePatientEncounterPresentingComplaints",
                        data: "{'VisitDate':'" + visitDate + "','VisitScheduled':'" + visitScheduled + "','VisitBy':'" + visitBy + "','Complaints':'" + complaints + "','TBScreening':'" + tbscreening + "','NutritionalStatus':'" + nutritionscreening + "','lmp':'" + LMP + "','PregStatus':'" + pregStatus + "','edd':'" + EDD + "','ANC':'" + ANCProfile + "', 'OnFP':'" + onFP + "','fpMethod':'" + FPMethod + "','CaCx':'" + CaCx + "','STIScreening':'" + STIScreening + "','STIPartnerNotification':'" + STIPartnerNotification + "', 'adverseEvent':'" + JSON.stringify(adverseEventsArray) + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d > 0)
                                toastr.success(response.d, "Presenting Complaints");
                            else
                                toastr.error("Error occured while saving Presenting Complaints");
                        },
                        error: function (response) {
                            //alert(msg);
                            toastr.error(response.d, "Error occured while saving Presenting Complaints");
                        }
                    });
            }


           function savePatientEncounterChronicIllness() {
               ///////////////////////////////////////////////////////
               var rowCount = $('#dtlChronicIllness tbody tr').length;
               var chronicIllnessArray = new Array();
               try {
                   for (var i = 0 ; i < rowCount; i++) {
                       chronicIllnessArray[i] = {
                           "chronicIllnessID": chronicTable.row(i).data()[0],
                           "chronicIllness": chronicTable.row(i).data()[1],
                           "treatment": chronicTable.row(i).data()[2],
                           "dose": chronicTable.row(i).data()[3],
                           "duration": chronicTable.row(i).data()[4],
                       }
                   }
               }
               catch (ex) { }
               ///////////////////////////////////////////
                //var chronicIllnessTable = new Array();
                //$("#dtlChronicIllness tr").each(function (row, tr) {
                //    chronicIllnessTable[row] = {
                //        "chronicIllness": $(tr).find('td:eq(0)').text(),
                //        "treatment": $(tr).find('td:eq(1)').text(),
                //        "dose": $(tr).find('td:eq(2)').text(),
                //        "duration": $(tr).find('td:eq(3)').text()
                //    }
                //});

               ///////////////////////////////////////////////////////
               var rowCount = $('#dtlVaccines tbody tr').length;
               var vaccineArray = new Array();
               try {
                   for (var i = 0 ; i < rowCount; i++) {
                       vaccineArray[i] = {
                           "vaccineID": vaccineTable.row(i).data()[0],
                           "vaccineStageID": vaccineTable.row(i).data()[1],
                           "vaccine": vaccineTable.row(i).data()[2],
                           "vaccineStage": vaccineTable.row(i).data()[3],
                           "vaccineDate": vaccineTable.row(i).data()[4],

                       }
                   }
               }
               catch (ex) { }
               

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientEncounterChronicIllness",
                    data: "{'chronicIllness':'" + JSON.stringify(chronicIllnessArray) + "','vaccines':'" + JSON.stringify(vaccineArray) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "Chronic Illness");
                    },
                    error: function (response) {
                        //alert(msg);
                        toastr.error(response.d, "Chronic Illness Error");
                    }
                });
            }

           function savePatientPhysicalExams() {
               ///////////////////////////////////////////////////////
               var rowCount = $('#dtlPhysicalExam tbody tr').length;
               var physicalExamArray = new Array();
               try {
                   for (var i = 0 ; i < rowCount; i++) {
                       physicalExamArray[i] = {
                           "examTypeID": examTable.row(i).data()[0],
                           "examID": examTable.row(i).data()[1],
                           "examType": examTable.row(i).data()[2],
                           "exam": examTable.row(i).data()[3],
                           "findings": examTable.row(i).data()[4],

                       }
                   }
               }
               catch (ex) { }

                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientPhysicalExam",
                    data: "{'physicalExam':'" + JSON.stringify(physicalExamArray) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "Physical Exam");
                    },
                    error: function (response) {
                        //alert(msg);
                        toastr.error(response.d, "Physical Exam Error");
                    }
                });
            }


            function savePatientPatientManagement() {
                
                var phdp = getCheckBoxListItemsChecked('<%= cblPHDP.ClientID %>');
                var arvAdherence = $("#<%=arvAdherance.ClientID%>").find(":selected").val();
                var ctxAdherence = $("#<%=ctxAdherance.ClientID%>").find(":selected").val();
                var nextAppDate = $("#<%=NextAppointmentDate.ClientID%>").val();
                var appointmentType = $("#<%=ddlReferredFor.ClientID%>").find(":selected").val();

                var rowCount = $('#dtlDiagnosis tbody tr').length;
                var diagnosisArray = new Array();
                try {
                    for (var i = 0 ; i < rowCount; i++) {
                        diagnosisArray[i] = {
                            "diagnosis": diagnosisTable.row(i).data()[0],
                            "treatment": diagnosisTable.row(i).data()[1]
                        }
                    }
                }
                catch (ex) { }
                
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientManagement",
                    data: "{'phdp':'" + phdp + "','ARVAdherence':'" + arvAdherence + "','CTXAdherence':'" + ctxAdherence + "','appointmentDate':'" + nextAppDate + "','appointmentType':'" + appointmentType + "','diagnosis':'" + JSON.stringify(diagnosisArray) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // alert("Saved");
                        toastr.success(response.d, "Patient Management");
                    },
                    error: function (response) {
                        //alert(msg);
                        toastr.error(response.d, "Patient Management Error");
                    }
                });
            }

         


            function getCheckBoxListItemsChecked(elementId) {
                var elementRef = document.getElementById(elementId);
                var checkBoxArray = elementRef.getElementsByTagName('input');
                var checkedValues = '';

                for (var i = 0; i < checkBoxArray.length; i++) {
                    var checkBoxRef = checkBoxArray[i];

                    if (checkBoxRef.checked == true) {
                        var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');

                        if (labelArray.length > 0) {
                            if (checkedValues.length > 0)
                                checkedValues += ',';

                            checkedValues += labelArray[0].innerHTML;
                        }
                    }
                }

                return checkedValues;
            }



           ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

         
  });
      
       function getPharmacyDrugList() {
           //var input = document.getElementById("txtSelectDrug");
           //var awesomplete = new Awesomplete(input, {
           //    minChars: 1,
           //    autoFirst: true
           //});
             
               $.ajax({
                   type: "POST",
                   url: "../WebService/PatientEncounterService.asmx/GetDrugList",
                   data: "{'regimenLine':'" + 0 + "'}",
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {
                       var drugListData = response.d;
                       //alert(serverData.length);

                       var drugList = [];
                       for (var i = 0; i < drugListData.length; i++) {

                           drugList.push(drugListData[i][1]);
                           alert(drugListData[i][0] + " " + drugListData[i][1])
                       }

                       //awesomplete.list = drugList;
                       $( "#ctl00_IQCareContentPlaceHolder_txtSelectDrug" ).autocomplete({
                           source: drugList
                       });

                       toastr.success(response.d, "Drug List");
                   },
                   error: function (response) {
                       alert(response.d);
                       toastr.error(response.d, "Drug List Error");
                   }
               });
               
       }
   
</script>
    
</asp:Content>