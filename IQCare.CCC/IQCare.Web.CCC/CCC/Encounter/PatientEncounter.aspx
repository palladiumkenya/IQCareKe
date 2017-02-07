<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PatientEncounter" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

    <!-- Auto complete code here-->
    <script src="../Scripts/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/js/jquery-ui-1.8.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/css/jquery-ui.css"></script>
    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css" />   
<!--  .end auto complete   -->
       <!-- line graph for viral tracker  -->  
  	
		<style type="text/css">
		</style>
		<script type="text/javascript">
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
		                name: 'Patrick',
		                data: [200, 300, 500, 1000, 750, 500, 400]
		            }, {
		                name: 'Threshold',
		                data: [1000, 1000, 1000, 1000, 1000, 1000, 1000]
		            }]
		        });
		    });
		  		
		</script>
<!--end line graph for viral tracker    -->          
       <div class="row">
    <script src="../../CCCScripts/PatientEncounter.js"></script>
          
    <script src="../Scripts/js/jquery-ui-1.8.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/css/jquery-ui.css"></script>

    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css" />  
    <!--  .end auto complete   -->
       <!-- line graph for viral tracker  -->  
  	
		<style type="text/css">
		</style>
		<script type="text/javascript">
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
		                name: 'Patrick',
		                data: [200, 300, 500, 1000, 750, 500, 400]
		            }, {
		                name: 'Threshold',
		                data: [1000, 1000, 1000, 1000, 1000, 1000, 1000]
		            }]
		        });
		    });
		  		
		</script>


        <div class="row">
            <div class="col-md-12">
                <uc:PatientDetails ID="PatientSummary" runat="server" />
            </div>
            <div class="col-md-12 col-xs-12">

                 <ul class="nav nav-tabs" role="tablist">
                     <li role="presentation" class="active"> <a href="#encounter"    aria-controls="encounter"   role="tab" data-toggle="tab"><i class="fa fa-exchange fa-lg" aria-hidden="true"></i> Clinical Encounter</a></li>
                     <li role="presentation"> <a href="#vlTracker"    aria-controls="vlTracker"   role="tab" data-toggle="tab"><i class="fa fa-line-chart fa-lg" aria-hidden="true"></i> Viraload Tracker</a></li>
                     <li role="presentation"> <a href="#Laboratory"   aria-controls="Laboratory"  role="tab" data-toggle="tab"><i class="fa fa-flask fa-lg" aria-hidden="true"></i> Laboratory</a></li>
                     <li role="presentation"> <a href="#Pharmacy"     aria-controls="Pharmacy"    role="tab" data-toggle="tab"><i class="fa fa-tint fa-lg" aria-hidden="true"></i> Pharmacy</a></li>
                     <li role="presentation"> <a href="#history"      aria-controls="history"     role="tab" data-toggle="tab"><i class="fa fa-history fa-lg" aria-hidden="true"></i> Encounter History</a></li>
                 </ul>
             </div><!-- .col-md-12 -->
            
            <div class="col-md-12 col-xs-12">

                 <div class="tab-content">
                           
                      <div role="tabpanel" class="tab-pane active" id="home">
                          <div class="col-md-12">
                              <div class="col-md-4" style="padding-top: 1%">
                              
                                  <div class="col-md-12 bs-callout bs-callout-info">
                                      <div class="col-md-12"><label class="control-label pull-left text-info">Pharmacy Summary</label></div>
                                      <div class="col-md-12"><hr/></div>
                                  </div>
                              </div>
                              <div class="col-md-4" style="padding-top: 1%">
                             
                                  <div class="col-md-12 bs-callout bs-callout-default">
                                 <div class="col-md-12"><label class="control-label pull-left text-default">Laboratory Summary</label></div>
                                      <div class="col-md-12"><hr/></div>
                                       viral load summary here  vx
                                           <div class="col-md-12">
                                               <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment CD4</label></div>
                                               <div class="col-md-4">
                                                   <asp:Label runat="server" ID="EnrollmentCD4" CssClass="control-label" ClientIDMode="Static">0</asp:Label>
                                               </div>
                                           </div>
                                          <div class="col-md-12">
                                              <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment CD4 Date</label></div>
                                               <div class="col-md-4">
                                                   <asp:Label runat="server" ID="EnrollmentCD4Date" CssClass="control-label" ClientIDMode="Static">00-00-0000</asp:Label>
                                               </div>
                                          </div>
                                          <div class="col-md-12">
                                              <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment Viral Load</label></div>
                                               <div class="col-md-4">
                                                   <asp:Label runat="server" ID="enrollmentViralload" CssClass="control-label" ClientIDMode="Static">0</asp:Label>
                                               </div>
                                          </div>
                                          <div class="col-md-12">
                                              <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment Viralload Date</label></div>
                                               <div class="col-md-4">
                                                   <asp:Label runat="server" ID="enrollmentviralloaddate" CssClass="control-label" ClientIDMode="Static">00-00-000</asp:Label>
                                               </div>
                                          </div>
                                   
                                  </div>
                                  
                              </div>
                              <div class="col-md-4" style="padding-top: 1%">
                             
                                  <div class="col-md-12 bs-callout bs-callout-success">
                                       <div class="col-md-12"><label class="control-label pull-left text-success">Diagnosis Summary</label></div>
                                      <div class="col-md-12"><hr/></div>
                                  </div>
                              </div>
                          </div>
                          <div class="col-md-12">
                              <div class="col-md-4">
                                  
                                  <div class="col-md-12 bs-callout bs-callout-success">
                                      <div class="col-md-12"><label class="control-label pull-left text-success">Today's Vital Signs</label></div>
                                      <div class="col-md-12"><hr/></div>
                                      <div class="col-md-12">
                                           <div class="col-md-8"><label class="control-label pull-left">Height (cm)</label></div>
                                           <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalHeight" CssClass="control-label text-success">0 cms</asp:Label>
                                           </div>
                                      </div> 
                                      <div class="col-md-12">
                                          <div class="col-md-8"><label class="control-label pull-left">Weight (kg)</label></div>
                                          <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalsWeight" CssClass="control-label text-success">0 Kgs</asp:Label>
                                           </div>
                                      </div> 
                                      <div class="col-md-12">
                                          <div class="col-md-8"><label class="control-label pull-left">Head Circumference (cm)</label></div>
                                          <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalsCircumference" CssClass="control-label text-success">0 cms</asp:Label>
                                           </div>
                                      </div> 
                                      <div class="col-md-12">
                                          <div class="col-md-8"><label class="control-label pull-left">MUAC (cm)</label></div>
                                          <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalsMUAC" CssClass="control-label text-success">0 cms</asp:Label>
                                           </div>
                                      </div> 
                                      <div class="col-md-12">
                                          <div class="col-md-8"><label class="control-label pull-left">Blood Pressure </label></div>
                                          <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalBloodPressure" CssClass="control-label text-success">0 </asp:Label>
                                           </div>
                                      </div> 
                                       <div class="col-md-12">
                                           <div class="col-md-8"><label class="control-label pull-left">Temperature (0C)</label></div>
                                           <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalTemperature" CssClass="control-label text-success">0 0C</asp:Label>
                                           </div>
                                       </div> 
                                       <div class="col-md-12">
                                           <div class="col-md-8"><label class="control-label pull-left">Respiratory Rate</label></div>
                                           <div class="col-md-4">
                                               <asp:Label runat="server" ID="vitalRespiratoryRate" CssClass="control-label text-success">0 </asp:Label>
                                           </div>
                                       </div> 
                                       <div class="col-md-12">
                                           <div class="col-md-8"><label class="control-label pull-left">Blood Oxygen Saturation</label></div>
                                           <div class="col-md-4">
                                               <asp:Label runat="server" ID="lblOxygenSaturation" CssClass="control-label text-success">0 %</asp:Label>
                                           </div>
                                       </div> 
                                  </div>

                              </div>
                              <div class="col-md-4"></div>
                              <div class="col-md-4"></div>
                          </div>
                          <div class="col-md-12"><hr/></div>
                      </div><!-- .home-->

                    
                      <div  role="tabpanel" class="tab-pane fade" id="encounter">
                         <div class="col-md-12">
                             <div class="col-md-12">
                                 <div class="col-md-12  bs-callout bs-callout-info">
                                     
                                 </div>
                             </div>--%>
                             
                             <div class="col-md-12">
                                 <div class="wizard" data-initialize="wizard" id="myWizard">
                                    <div class="steps-container">
	                                    <ul class="steps">
		                                   
                                            <li data-step="1" data-name="template" class="active">
			                                    <span class="badge">1</span>Presenting Complaints
			                                    <span class="chevron"></span>
		                                    </li>
                                            
                                            <li data-step="2">
			                                    <span class="badge">3</span>Patient Chronic Illness
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
		                                    <%--<div class="col-md-12"><small class="muted pull-left"><strong>Presenting Complaints </strong></small></div> <div class="col-md-12"><hr /> </div>--%>  

                                            <%--here--%>

                                             <div class="col-md-12">
                                          <div class="col-md-4">
	                                            <div class="col-md-12 form-group">
	                                              <div class="col-md-12"><label class="control-label  pull-left">Visit Date</label></div>
	                                              <div class="col-md-12">
		                                              <div class="datepicker fuelux" id="DateOfVisit">
		                                              <div class="input-group">

			                                              <input class="form-control input-sm" id="VisitDate" type="text" runat="server" data-parsley-required="true" />
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
		                                              <label class="radio-custom radio-inline pull-left" data-initialize="radio">
			                                              <input class="sr-only" name="Scheduled" type="radio" value="1"> Yes
		                                              </label>
		                                              <label class="radio-custom radio-inline pull-left" data-initialize="radio">
			                                              <input class="sr-only" name="Scheduled" type="radio" data-parsley-required="true" value="0"> No
		                                              </label>
	                                              </div>
                                              </div>
                                            </div>

                                          <div class="col-md-4">
	                                         <div class="col-md-12 form-group">
		                                          <div class="col-md-12"><label class="control-label  pull-left">Visit By</label></div>
		                                          <div class="col-md-12">
			                                          <asp:DropDownList runat="server" ID="ddlVisitBy" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" />
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
                                                     <div class="col-md-7">
                                                         <%--<div class="col-md-12"><h1 class="text-primary pull-left"><small>Complaints & History of Complaints</small></h1></div>
                                                         <div class="col-md-12"><hr /></div>--%>
                                                        
                                                          <label class="control-label pull-left" for="complaints">Complaints Today</label>
                                                               <textarea runat="server" clientidmode="Static" id="complaints" class="form-control input-sm" placeholder="complaints...." rows="4"></textarea> 
                                                    </div>

                                                      <div class="col-md-5">
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
                                                                      <div class="col-md-12"><label class="control-label pull-left" ><small>Adverse event(s)</small></label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:TextBox runat="server" CssClass="form-control input-sm" ID="adverseEvent" ClientIDMode="Static" placeholder="adverse event.."></asp:TextBox>
                                                                     </div>
                                                                 </div>
                                                                 <div class="col-md-3">
                                                                     <div class="col-md-12"><label class="control-label" >Medicine Causing a/e</label></div>
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

                                                             <%--<div class="col-md-12 form-group">
                                                                <div class="col-md-3"></div>
                                                                  <div class="col-md-3"></div>
                                                                 <div class="col-md-3"></div>
                                                                  <div class="col-md-3">
                                                             <div class="col-md-12">--%>
                                                                 <%--<asp:LinkButton ID="btnAdverseEventsAdd" CssClass="btn btn-info btn-lg fa fa-plus-circle" onclick="AddAdverseReaction();">Add Adverse Event</asp:LinkButton>--%>
                                                                <%-- <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                        id="btnAddMilestones" onclick="AddAdverseReaction();">
                                        Add</button>
                                                             </div>
                                                         </div>
                                                             </div>--%>
                                                        </div> <%--.panel-body--%>

                                                        <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                        <table id="dtlAdverseEvents" class="table table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 50px;">
                                                                        Adverse Event
                                                                    </th>
                                                                    <th style="width: 50px;">
                                                                        Medicine Causing A/E
                                                                    </th>
                                                                    <th style="width: 30px;">
                                                                        Severity
                                                                    </th>
                                                                    <th style="width: 50px;">
                                                                        Action
                                                                    </th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                            <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 80px;" id="btnRemoveAdvEvent">Remove</button>
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
                                                                      <div class="col-md-12"><label class="control-label  pull-left">Female LMP</label></div>
                                                                      <div class="col-md-12">
                                                                          <div class="datepicker fuelux" id="FemaleLMP">
                                                                          <div class="input-group">
                                                                              <input class="form-control input-sm" id="lmp" type="text" runat="server" />
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
                                                                      <div class="col-md-12"><label class="control-label  pull-left">Pregnancy Status</label></div>
                                                                     <div class="col-md-12">
                                                                         <asp:DropDownList runat="server" ID="examinationPregnancyStatus" CssClass="form-control input-sm" ClientIDMode="Static"/>
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
                                                                          <label class="radio-custom radio-inline pull-left" data-initialize="radio">
                                                                              <input class="sr-only" name="ANCProfile" type="radio" value="1"> Yes
                                                                          </label>
                                                                          <label class="radio-custom radio-inline pull-left" data-initialize="radio">
                                                                              <input class="sr-only" name="ANCProfile" type="radio" value="0"> No
                                                                          </label>
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
                                                 <%--<div class="col-md-1">
                                                     <h4 class="pull-left text-warning"><i class="fa fa-user-md fa-5x" aria-hidden="true"></i></h4>
                                                 </div>--%>
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
                                                                    <table id="dtlChronicIllness" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="width: 50px;">
                                                                                    Illness
                                                                                </th>
                                                                                <th style="width: 50px;">
                                                                                    Current Treatment
                                                                                </th>
                                                                                <th style="width: 30px;">
                                                                                    Dose
                                                                                </th>
                                                                                <th style="width: 50px;">
                                                                                    Duration
                                                                                </th>
                                                                    
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 150px" id="btnRemoveChronicIllness">Remove Selected Row</button>
                                                                </div>
                                                             </div>
                                                         </div>

                                                     </div>
                                                     </div>
                                                     <%--<div class="col-md-12"><hr /></div>--%>
                                                      <%--<div class="col-md-12 form-group">--%>
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
                                                                        <div class="col-md-4">
                                                                            <div class="col-md-12"><label class="control-label pull-left">Vaccine Stage</label></div>
                                                                            <div class="col-md-12">
                                                                                <asp:DropDownList ID="ddlVaccineStage" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
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
                                                                                <table id="dtlVaccines" class="table table-bordered table-striped">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>
                                                                                                Vaccine
                                                                                            </th>
                                                                                            <th>
                                                                                                Vaccine Stage
                                                                                            </th>
                                                                                            <th>
                                                                                                Vaccination Date
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                </table>
                                                                                    <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 80px;" id="btnRemoveVaccine">Remove</button>
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
                                             
	                                    </div><%-- .data-step-3--%>
                                        
                                        <div class="step-pane sample-pane" data-step="4">
                                             <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT MANAGEMENT</strong></small></div> <div class="col-md-12"><hr /> </div>  
                                             
                                            <div class="col-md-1">
                                                <h4 class="pull-left text-danger"><i class="fa fa-bed fa-5x" aria-hidden="true"></i></h4>
                                            </div> 
                                            <div class="col-md-11">
                                                <div class="col-md-12">
                                                  
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
                                    <div class="col-md-12"><label class="control-label pull-left">Pending VL results</label></div>
                                   
                                    <!--pw implementation of laboratory module here-->
                                                 <div class="col-md-12">
                                                      <div class="col-md-12"><hr/></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Count#</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Ordered Test</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Order Date</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Ordered By</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Order Reason</label></div>  
                                                      <div class="col-md-1"><label class="control-label text-warning pull-left">Order Status</label></div>
                                                  </div>

                                <!--pw .implementation of  laboratory module here-->
                             
                                </div>
                                    <script src="https://code.highcharts.com/highcharts.js"></script>
                                    <script src="https://code.highcharts.com/modules/exporting.js"></script>
                                    <div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div> 
                                                            

                      <!-- pw .implementation of viral load tracker here-->
                      </div><!-- .viraload tracker-->

                      <div  role="tabpanel"  class="tab-pane fade" id="vlTracker">
                    <!-- pw implementation of viral load tracker here-->
                             <div class="col-md-6">
                                    <div class="col-md-12"><label class="control-label pull-left">Pending VL results</label></div>
                                   
                      <!--pw implementation of laboratory module here-->
                                                 <div class="col-md-12">
                                                      <div class="col-md-12"><hr/></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Count#</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Ordered Test</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Order Date</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Ordered By</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Order Reason</label></div>  
                                                      <div class="col-md-1"><label class="control-label text-warning pull-left">Order Status</label></div>
                                                  </div>

                     <!--pw .implementation of  laboratory module here-->
                             
                                </div>
                                    <script src="https://code.highcharts.com/highcharts.js"></script>
                                    <script src="https://code.highcharts.com/modules/exporting.js"></script>
                                    <div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div> 
                                                            

                      <!-- pw .implementation of viral load tracker here-->
                      </div><!-- .viraload tracker-->
                    
                      <div  role="tabpanel" class="tab-pane fade" id="Laboratory">
                            <div class="col-md-12" style="padding-top: 1%">
                              <label class="control-label pull-left"> <i class="fa fa-flask fa-lg" aria-hidden="true"></i> Laboratory Prescription </label>
                            </div>
                            
                            <div class="col-md-12 bs-callout bs-callout-danger">
                                <h4 class="pull-left"> <strong>Pending Labs :</strong> </h4>
                            </div>
                           
                            <div class="col-md-12 bs-callout bs-callout-info">
                                <div class="col-md-6">
                                    <div class="col-md-12"><label class="control-label pull-left">Previous Labs</label></div>
                                    <div class="col-md-12"><hr/>
                      <!--pw implementation of previous labs laboratory module here  porders-->
                                        <div class="col-md-12 form-group">
            
                                                             <table id="previouslab_orders" >
                                                                        <thead>
                                                                          <tr>
      	                                                                    <th>Count.# </th> 
                                                                            <th>Lab Test </th> 
      	                                                                    <th>Order Reason </th> 
      	                                                                    <th>Order Date </th> 
      	                                                                    <th>Order Status </th> 
      	                                                                  </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                          <tr>
      	                                                                    <td>demo</td> 
      	                                                                    <td>"txttName"</td>
      	                                                                    <td>demo</td> 
      	                                                                    <td>mm/dd/yyy</td> 
      	                                                                    <td>demo</td>                  
                                                                          </tr>
                         
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                            <!--
                                                 <div class="col-md-12">
                                                      <div class="col-md-12"><hr/></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Count#</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Ordered Test</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Order Date</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Ordered By</label></div>
                                                      <div class="col-md-2"><label class="control-label text-warning pull-left">Order Reason</label></div>  
                                                      <div class="col-md-1"><label class="control-label text-warning pull-left">Order Status</label></div>
                                                  </div>
                                        !>
                    <!--pw implementation of previous laboratory module here-->
                               </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-12"><label class="control-label pull-left">Order Lab Test(s)</label></div>
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                              <div class="panel-heading"></div>
                                              <div class="panel-body">
                                                  <div class="col-md-12 form-group">
                                                      <div class="col-md-3"><label class="control-label pull-left">Select Lab(s)</label></div>
                                                      <div class="col-md-9">
                                                           <asp:TextBox runat="server" ID="txtautofromDB" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="type to select...."></asp:TextBox>
                                                                                                              
                                                             </div>
                                                  </div>
                                                  <div class="col-md-12 form-group">
                                                       <div class="col-md-3"><label class="control-label pull-left"> Lab Order Reason</label></div>
                                                      <div class="col-md-9">
                                                          <asp:DropDownList runat="server" ID="labOrderReason" CssClass="form-control input-sm" ClientIDMode="Static" >
                                                         <asp:ListItem Text=" " Value="1" />
                                                         <asp:ListItem Text="Baseline" Value="2" />
                                                         <asp:ListItem Text="Routine" Value="3" />
                                                         <asp:ListItem Text="Confirmatory" Value="4" />
                                                         <asp:ListItem Text="Suspected drug resistance" Value="5" />
                                                         <asp:ListItem Text="Other" Value="5" />
                                                    </asp:DropDownList>
                                                      </div>
                                                  </div>
                                                  
                                                  <div class="col-md-12 form-group">
                                                       <div class="col-md-3"><label class="control-label pull-left"> Lab Order Notes</label></div>
                                                      <div class="col-md-9">
                                                         
                                                          <asp:TextBox runat="server" Rows="4" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="laboratory notes...."></asp:TextBox>
                                                      </div>
                                                  </div>
                                                  <div class="col-md-12">
                                                      <div class="col-md-10"></div>
                                                      <div class="col-md-2 pull-right">
                                                          <asp:LinkButton runat="server" CssClass="btn btn-warning fa fa-plus-circle"> Add Lab</asp:LinkButton>
                                                      </div>
                                                  </div>
                    
                                                  <div class="col-md-12">
                                                      <div class="col-md-12"><hr/></div>
                                                      <div class="col-md-3"><label class="control-label text-warning pull-left">Count.#</label></div>
                                                      <div class="col-md-3"><label class="control-label text-warning pull-left">Ordered Lab Test</label></div>
                                                      <div class="col-md-3"><label class="control-label text-warning pull-left">Lab Order Reason</label></div>
                                                      <div class="col-md-3"><label class="control-label text-warning pull-left">Lab Order Date</label></div>
                                                  </div>   
<!--
<div class="col-md-12 form-group">
            
             <table id="currentlab_orders" >
                        <thead>
                          <tr>
      	                    <th>Count.# </th> 
                            <th>Lab Test </th> 
      	                    <th>Order Reason </th> 
      	                    <th>Order Date </th> 
      	                    <th>Order Status </th> 
      	                  </tr>
                        </thead>
                        <tbody>
                          <tr>
      	                    <td>demo</td> 
      	                    <td>demo</td>
      	                    <td>demo</td> 
      	                    <td>mm/dd/yyy</td> 
      	                    <td>demo</td>                  
                          </tr>
                         
                        </tbody>
                    </table>
                </div>

-->
   
                                                </div>
                                        </div>
                                    </div>

                                </div>
                                
                                <div class="col-md-12">
                                        <div class="col-md-12"><hr/></div>
                                        <div class="col-md-7"></div>
                                         <div class="col-md-5">
                                             <div class="col-md-3">
                                                 <asp:LinkButton runat="server" ID="btnSaveLab" CssClass="btn btn-info fa fa-plus-circle" ClientIDMode="Static"> Save Order</asp:LinkButton>
                                             </div>
                                             <div class="col-md-3">
                                                 <asp:LinkButton runat="server" ID="btnPrintOrder" CssClass="btn btn-primary fa fa-print" ClientIDMode="Static"> Print Order</asp:LinkButton>
                                             </div>
                                             <div class="col-md-3">
                                                 <asp:LinkButton runat="server" ID="btnResetOrder" CssClass="btn btn-warning fa fa-refresh" ClientIDMode="Static"> Reset Order</asp:LinkButton>
                                             </div>
                                             <div class="col-md-3">
                                                 <asp:LinkButton runat="server" ID="btnCancelOrder" CssClass="btn btn-danger fa fa-times" ClientIDMode="Static"> Cancel Order</asp:LinkButton>
                                             </div>
                                         </div>
                                    </div>
                            </div>
                            

                      </div><!-- .laboratory-->  <!--end pw implementation of  laboratory module here-->

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
                                                                   <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="regimLine" ClientIDMode="Static"/>
                                                              </div>
                                                              <div class="col-md-2 "></div>
                                                         </div>  
                                                        
                                                         <div class="col-md-12 bs-callout bs-callout-info">
                                                              <div class="row">
                                                                    <div class="col-md-12">
                                                                         
                                                                              <div class="col-md-3"><label class="control-label pull-left">Select Drug (s) </label></div>
                                                                              <div class="col-md-7">
                                                                                  <input type="text" class="form-control input-sm" placeholder="type to search...." runat="server" />
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
                       <div  role="tabpanel"    class="tab-pane fade"      id="history">...</div><!-- .history-->
                 </div><!-- .tab-content-->
            </div><!-- .col-md-12 -->
        </div><!-- .row -->
    <!-- ajax begin -->
   <script type="text/javascript">
       $(document).ready(function () {
           /*   Ajax functions originally set will return later ---
            $('#OnsetDate').datepicker();
            $('#FemaleLMP').datepicker();
            $('#EDCD').datepicker();
            $('#AntigenDate').datepicker();
            $('#NextAppDate').datepicker();
            $('#myWizard').wizard();    */

            //pw autocomplete
            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetLookupLabsList",
                dataType: "json",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    
                    var serverData = JSON.parse(data.d);    //clean object
                   
                    var labtests = [];
                    for (var i = 0; i < serverData.length; i++) {
                        
                        labtests.push(serverData[i]["ParameterName"]);  //push data to array important
                    }
                    
                    //console.log(labtests);                    
                              
                        $("[id$='txtautofromDB']").autocomplete({
                            source: labtests
                        
                     });
                },
                    error: function (errorThrown) {
                        alert(textStatus);
                        console.log(errorThrown)

                     }
            });

           //pw .autocomplete
         
       });		  
      
    </script>
    
    
</asp:Content>




























































































    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#DateOfVisit').datepicker({
                allowPastDates: true,
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
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#EDCD').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#AntigenDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#NextAppDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            $('#vaccineDate').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
            });
            
            ////////////////////////////////////////////////////////////////////////////////////////////
            $('#dtlAdverseEvents').DataTable({
                paging: false,
                searching: false,
                info: false
            });

            $('#dtlChronicIllness').DataTable({
                paging: false,
                searching: false,
                info: false
            });

            $('#dtlVaccines').DataTable({
                paging: false,
                searching: false,
                info: false
            });


            var tableAdverseEvents = $('#dtlAdverseEvents').DataTable();
            var tableChronicIllness = $('#dtlChronicIllness').DataTable();
            var tableVaccines = $('#dtlVaccines').DataTable();
        
            var index;

            ///dtlAdverseEvents
            $('#dtlAdverseEvents tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    tableAdverseEvents.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            $('#btnRemoveAdvEvent').click(function () {
                tableAdverseEvents.row('.selected').remove().draw(false);
                if (index > -1) {
                    arrAdverseEvent.splice(index, 1);
                }
            });

            $('#dtlAdverseEvents').on('click', 'tr', function () {
                var aData = tableAdverseEvents.row(this).data();
                index = arrAdverseEvent.findIndex(x => x.adverseEvent == aData[0]);
            });


            ////dtlChronicIllness
            $('#dtlChronicIllness tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    tableChronicIllness.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            $('#btnRemoveChronicIllness').click(function () {
                tableChronicIllness.row('.selected').remove().draw(false);
                if (index > -1) {
                    arrChronicIllness.splice(index, 1);
                }
            });

            $('#dtlChronicIllness').on('click', 'tr', function () {
                var aData = tableChronicIllness.row(this).data();
                index = arrChronicIllness.findIndex(x => x.chronicIllness == aData[0]);
            });

            ////dtlVaccines
            $('#dtlVaccines tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    tableVaccines.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            $('#btnRemoveVaccine').click(function () {
                tableVaccines.row('.selected').remove().draw(false);
                if (index > -1) {
                    arrVaccines.splice(index, 1);
                }
            });

            $('#dtlVaccines').on('click', 'tr', function () {
                var aData = tableVaccines.row(this).data();
                index = arrVaccines.findIndex(x => x.vaccine == aData[0]);
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
                        /*else if (data.step === 3) {
                            if ($("#datastep3").parsley().validate()) {

                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                        else if (data.step === 4) {
                            if ($("#datastep2").parsley().validate()) {

                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                if (totalError > 0) {
                                    $('.bs-callout-danger').toggleClass('hidden', f);
                                }
                                evt.preventDefault();
                            }
                            //var ok4 = $('.parsley-error').length === 0;
                            //$('.bs-callout-info').toggleClass('hidden', !ok4);
                        }*/
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
                var visitScheduled = $('input[name="Scheduled"]:checked').val();
                var visitBy = $("#<%=ddlVisitBy.ClientID%>").find(":selected").val();
                var complaints = $("#<%=complaints.ClientID%>").val();
                var tbscreening = $("#<%=tbscreeningstatus.ClientID%>").find(":selected").val();
                var nutritionscreening = $("#<%=nutritionscreeningstatus.ClientID%>").find(":selected").val();
                var LMP = $("#<%=lmp.ClientID%>").val();
                var pregStatus = $("#<%=examinationPregnancyStatus.ClientID%>").find(":selected").val();
                var EDD = $("#<%=ExpectedDateOfChildBirth.ClientID%>").val();
                var ANCProfile = $('input[name="ANCProfile"]:checked').val();
                var onFP = $("#<%=onFP.ClientID%>").find(":selected").val();
                var FPMethod = $("#<%=fpMethod.ClientID%>").find(":selected").val();

                var CaCx = $("#<%=cacxscreening.ClientID%>").find(":selected").val();
                var STIScreening = $("#<%=stiScreening.ClientID%>").find(":selected").val();
                var STIPartnerNotification = $("#<%=stiPartnerNotification.ClientID%>").find(":selected").val();
                
                    $.ajax({
                        type: "POST",
                        url: "../WebService/PatientEncounterService.asmx/savePatientEncounterPresentingComplaints",
                        data: "{'VisitDate':'" + visitDate + "','VisitScheduled':'" + visitScheduled + "','VisitBy':'" + visitBy + "','Complaints':'" + complaints + "','TBScreening':'" + tbscreening + "','NutritionalStatus':'" + nutritionscreening + "','lmp':'" + LMP + "','PregStatus':'" + pregStatus + "','edd':'" + EDD + "','ANC':'" + ANCProfile + "', 'OnFP':'" + onFP + "','fpMethod':'" + FPMethod + "','CaCx':'" + CaCx + "','STIScreening':'" + STIScreening + "','STIPartnerNotification':'" + STIPartnerNotification + "', 'adverseEvent':'" + JSON.stringify(arrAdverseEvent) + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            alert("Saved");        
                        },
                        failure: function (msg) {
                            alert(msg);
                        }
                    });
            }



            function savePatientEncounterChronicIllness() {
                var BCGVaccine = $('#BCG:checked').val();
                var PolioVaccine = $('.sr-only:checked').val();
                var PentaVaccine = $('.sr-only:checked').val();
                var PCV = $('.sr-only:checked').val();
                var Measles = $('.sr-only:checked').val();
 
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientEncounterChronicIllness",
                    data: "{'chronicIllness':'" + JSON.stringify(arrChronicIllness) + "','vaccines':'" + JSON.stringify(arrVaccine) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert("Saved");        
                    },
                    failure: function (msg) {
                        alert(msg);
                    }
                });
            }


        });

    </script>
</asp:Content>