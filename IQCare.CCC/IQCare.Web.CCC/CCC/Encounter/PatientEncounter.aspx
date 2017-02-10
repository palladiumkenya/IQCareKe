<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PatientEncounter" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../../CCCScripts/PatientEncounter.js"></script>   
    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css" />   

       <!-- line graph for viral tracker  -->  
  	
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

                      <div  role="tabpanel" class="tab-pane active" id="encounter">
                         <div class="col-md-12" style="padding-top:20px">
                             <%--<div class="col-md-12">
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
			                                              <input class="sr-only" name="Scheduled" type="radio" value="1" > Yes
		                                              </label>
		                                              <label class="radio-custom radio-inline pull-left" data-initialize="radio">
			                                              <input class="sr-only" name="Scheduled" type="radio" data-parsley-required="true" value="0" > No
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
                                                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Adverse Event</span></th>
                                                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Medicine Causing A/E</span></th>
                                                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Severity</span></th>
                                                                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Action</span></th>
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
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Illness</span></th>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Current Treatment</span></th>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Dose</span></th>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Duration</span></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                        
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
                                                                                            <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Vaccine</span></th>
                                                                                            <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Vaccine Stage</span></th>
                                                                                            <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Vaccination Date</span></th>
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
                                                                    <table id="dtlPhysicalExam" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Examination Type</span></th>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Examination</span></th>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Findings</span></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                     
                                                                </div>
                                                             </div>
                                                         </div>
                                                    
                                                 </div>

                                                 <div class="col-md-1"></div>
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
                                                                      <%--<asp:LinkButton runat="server" ID="btnAddDiagnosis" CssClass="btn btn-info btn-lg fa fa-plus-circle"> Add</asp:LinkButton>--%>
                                                                    <button type="button" Class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddDiagnosis" onclick="AddDiagnosis();">Add</button>
                                                                 </div>
                                                                
                                                            </div>

                                                           <div class="col-md-12 form-group">
                                                            <div class="panel panel-info">
                                                                 <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlDiagnosis" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Diagnosis</span></th>
                                                                                <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Treatment</span></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                   
                                                                </div>
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
                      <div  role="tabpanel"  class="tab-pane fade" id="vloadTracker">
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
                          
                                    <%--<script src="../Scripts/js/jquery-ui-1.8.custom.min.js" type="text/javascript"></script>--%>
                                    <%--<script src="../Scripts/css/exporting.css"></script>--%>
                                    <%--<link href="../Scripts/css/exporting.css" rel="stylesheet" type="text/css" />--%>   

                             <!--      <script src="../Scripts/js/highcharts.js" type="text/javascript"></script>
                                    <script src="https://code.highcharts.com/modules/exporting.js"></script>  -->

                                    <%--<div id="container" style="min-width: 450px; height: 300px; margin: 0 auto"></div>--%> 
                                                            

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
            
                                                             <table id="plab_orders" >
                                                                        <thead>
                                                                          <tr>
      	                                                                    <th> # </th>  
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
                                                          
                                                           <asp:TextBox runat="server" ID="labTestTypes" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="type to select...."></asp:TextBox>
                                                                                                              
                                                             </div>
                                                  </div>
                                                  <div class="col-md-12 form-group">
                                                       <div class="col-md-3"><label class="control-label pull-left"> Lab Order Reason</label></div>
                                                      <div class="col-md-9">
                                                          <asp:DropDownList runat="server" ID="OrderReason" CssClass="form-control input-sm" ClientIDMode="Static" >
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
                                                         
                                                          <asp:TextBox runat="server" ID="LabNotes" Rows="4" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="laboratory notes...."></asp:TextBox>
                                                      </div>
                                                  </div>
                                                  <div class="col-md-12">
                                                      <div class="col-md-10"></div>
                                                      <div class="col-md-2 pull-right">
                                            <asp:LinkButton runat="server" ID="btnAddLab"  ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle"> Add Lab</asp:LinkButton>
                                                        
                                                      </div>
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
                                                              
   
                                                </div>
                                        </div>
                                    </div>

                                </div>
                                
                                <div class="col-md-12">
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

           /////////////////////////////////PATIENT ENCOUNTER////////////////////////////////////////////////

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
                info: false,
                ordering: false
            });

            $('#dtlChronicIllness').DataTable({
                paging: false,
                searching: false,
                info: false,
                ordering: false
            });

            $('#dtlVaccines').DataTable({
                paging: false,
                searching: false,
                info: false,
                ordering: false
            });

            $('#dtlPhysicalExam').DataTable({
                paging: false,
                searching: false,
                info: false,
                ordering: false
            });

            $('#dtlDiagnosis').DataTable({
                paging: false,
                searching: false,
                info: false,
                ordering: false
            });

            var tableAdverseEvents = $('#dtlAdverseEvents').DataTable();
            var tableChronicIllness = $('#dtlChronicIllness').DataTable();
            var tableVaccines = $('#dtlVaccines').DataTable();
            var tablePhysicalExam = $('#dtlPhysicalExam').DataTable();
            var tableDiagnosis = $('#dtlDiagnosis').DataTable();
            
            var index;

            $("#dtlAdverseEvents").on('click',
                '.btnDelete',
                function () {
                    $(this).closest('tr').remove();
                    var y = $(this).closest('tr').find('td').eq(0).html();
                    index = arrAdverseEvent.findIndex(x => x.adverseEvent == y);
                    if (index > -1) {
                        arrAdverseEvent.splice(index, 1);
                    }
                });

            ////dtlChronicIllness
            $("#dtlChronicIllness").on('click',
                '.btnDelete',
                function () {
                    $(this).closest('tr').remove();
                    var y = $(this).closest('tr').find('td').eq(0).html();
                    index = arrChronicIllness.findIndex(x => x.adverseEvent == y);
                    if (index > -1) {
                        arrChronicIllness.splice(index, 1);
                    }
                });

            ////dtlVaccines
            $("#dtlVaccines").on('click',
                '.btnDelete',
                function () {
                    $(this).closest('tr').remove();
                    var y = $(this).closest('tr').find('td').eq(0).html();
                    index = arrVaccine.findIndex(x => x.adverseEvent == y);
                    if (index > -1) {
                        arrVaccine.splice(index, 1);
                    }
                });


            ////dtlPhysicalExam
            $("#dtlPhysicalExam").on('click',
                '.btnDelete',
                function () {
                    $(this).closest('tr').remove();
                    var y = $(this).closest('tr').find('td').eq(0).html();
                    index = arrPhysicalExam.findIndex(x => x.adverseEvent == y);
                    if (index > -1) {
                        arrPhysicalExam.splice(index, 1);
                    }
                });

            ////dtlDiagnosis
            $("#dtlDiagnosis").on('click',
                '.btnDelete',
                function () {
                    $(this).closest('tr').remove();
                    var y = $(this).closest('tr').find('td').eq(0).html();
                    index = arrDiagnosis.findIndex(x => x.adverseEvent == y);
                    if (index > -1) {
                        arrDiagnosis.splice(index, 1);
                    }
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
                            savePatientPatientManagement();
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
                           // alert("Saved");
                            toastr.success(response.d, "Presenting Complaints");
                        },
                        error: function (response) {
                            //alert(msg);
                            toastr.error(response.d, "Presenting Complaints Error");
                        }
                    });
            }


            function savePatientEncounterChronicIllness() {
 
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientEncounterChronicIllness",
                    data: "{'chronicIllness':'" + JSON.stringify(arrChronicIllness) + "','vaccines':'" + JSON.stringify(arrVaccine) + "'}",
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
                
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientPhysicalExam",
                    data: "{'physicalExam':'" + JSON.stringify(arrPhysicalExam) + "'}",
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
                
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientManagement",
                    data: "{'phdp':'" + phdp + "','ARVAdherence':'" + arvAdherence + "','CTXAdherence':'" + ctxAdherence + "','appointmentDate':'" + nextAppDate + "','appointmentType':'" + appointmentType + "','diagnosis':'" + JSON.stringify(arrDiagnosis) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // alert("Saved");
                        toastr.success(response.d, "Presenting Complaints");
                    },
                    error: function (response) {
                        //alert(msg);
                        toastr.error(response.d, "Presenting Complaints Error");
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
                              
                         $("[id$='labTestTypes']").autocomplete({
                            source: labtests
                        
                     });
                },
                    error: function (errorThrown) {
                        alert(textStatus);
                        console.log(errorThrown)

                     }
            });


   // Load lab order
            $("#btnAddLab").click(function (e) {
              


                var labOrderDate = moment().format('D MMM, YYYY');
                var labType = $("#labTestTypes").val();
                var labOrderReason = $("#OrderReason").find(":selected").text();
                var labNotes = $("#LabNotes").val();

                    

              if (labType < 1) {
                    generate("error", "Please select at least One(1) Lab Type from the List");
                    return false;
                }  
                if (labOrderReason < 1) {
                    generate("error", "Please select at least One(1) Lab Order Reason from the List");
                    return false;
                }
                
               else {
                   
                   
                    var tr = "<tr><td></td><td align='left'>" + labType + "</td><td align='left'>" + labOrderReason + "</td><td align='left'>" + labOrderDate + "</td></tr>";
                    $("#tblAddLabs>tbody:first").append('' + tr + '');
                   
                }

                e.preventDefault();
            });
          

           



       });		  
     // Save lab order
       $("#btnSaveLab").click(function (e) {
           var _fp = [];
           var data = $('#tblAddLabs tr').each(function (row, tr) {
               _fp[row] = {
                  "labType": $(tr).find('td:eq(2)').val()
                , "orderReason": $(tr).find('td:eq(3)').val()
                , "labOrderDate": $(tr).find('td:eq(4)').val()

               }
           });
           _fp.shift();//first row will be empty -so remove

           if ($.isEmptyObject(_fp)) {
               generate("error", "You have not added any lab order");
               return false;
           } else {
               var patientID = $("#entryPoint").val();
               addLabOrder(_fp, patientID);
           }

           
       });
       function addLabOrder(_fp, patientID) {
           var labOrder = JSON.stringify(_fp);
         
           $.ajax({
               type: "POST",
               //url: "../WebService/EnrollmentService.asmx/AddPatient",
               url: "../WebService/LabService.asmx/AddLabOrder",
               data: "{'patientID':'" + 1058 + "','labType': '" + labType + "','orderReason': '" + orderReason + "','  labNotes': '" + LabNotes + "','labOrderDate': '" + labOrderDate + "'}",
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   //generate('success', '<p>,</p>' + response.d);
                   toastr.success(response.d, "Lab order successful");
               },
               error: function (response) {
                   //generate('error', response.d);
                   toastr.error(response.d, "Lab order");
               }
           });
       };	
       function generate(type, text) {

           var n = noty({
               text: text,
               type: type,
               dismissQueue: true,
               progressBar: true,
               timeout: 5000,
               layout: 'topRight',
               closeWith: ['click'],
               theme: 'relax',
               maxVisible: 10,
               animation: {
                   open: 'animated bounceInLeft',
                   close: 'animated bounceOutLeft',
                   easing: 'swing',
                   speed: 500
               }
           });
           console.log('html: ' + n.options.id);
           return n;
       }
           
         
         
     	  
      
    </script>


</asp:Content>