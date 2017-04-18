<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientClinicalEncounter.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientClinicalEncounter" %>
 <div class="col-md-12" style="padding-top: 20px">

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
                <button type="button" class="btn btn-primary btn-next" data-last="Complete">
                    Next
		                    <span class="glyphicon glyphicon-arrow-right"></span>
                </button>
            </div>

            <div class="step-content">

                <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">

                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-12 form-group">
                                <div class="col-md-12">
                                    <label class="control-label  pull-left text-primary">*Visit Date</label>
                                </div>
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
                        </div>

                        <div class="col-md-4">
                            <div class="col-md-12 form-group">
                                <div class="col-md-12">
                                    <label class="control-label  pull-left text-primary">*Visit Scheduled?</label>
                                </div>
                                <div class="col-md-12">
           
                                    <label class="pull-left" style="padding-right:10px">
                                        <input id="vsYes" type="radio" name="Scheduled" value="1" ClientIDMode="Static" runat="server" />Yes
                                    </label>
                                    <label class="pull-left" style="padding-right:10px">
                                        <input id="vsNo" type="radio" name="Scheduled" value="0" ClientIDMode="Static" runat="server" data-parsley-required="true" />No
                                    </label>
                                    
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="col-md-12 form-group">
                                <div class="col-md-12">
                                    <label class="control-label  pull-left text-primary">*Visit By</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="ddlVisitBy" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="1" data-parsley-min-message="Value Required" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>
                    <%--to here--%>


                    <div class="col-md-12 form-group">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <label class="control-label pull-left text-primary" for="complaints">*Complaints Today</label>
                                <textarea runat="server" clientidmode="Static" id="complaints" class="form-control input-sm" placeholder="complaints...." rows="4" data-parsley-required="true"></textarea>
                            </div>

                            <div class="col-md-6">
                                <div class="col-md-12  form-group">
                                    <div class="col-md-6">
                                        <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">*TB Screening</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="tbscreeningstatus" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
                                    </div>
                                </div>

                                <div class="col-md-12 form-group">
                                    <div class="col-md-6">
                                        <label class="control-label pull-left input-sm text-primary" for="nutritionscreeningstatus">*Nutrition Status</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="nutritionscreeningstatus" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-md-12 form-group">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <div class="panel panel-info">

                                        <div class="panel-body">
                                            <div class="col-md-12 form-group"><label class="control-label pull-left">Adverse Event(s)</label></div>
                                            
                                            <div class="col-md-12 form-group">
                                                    <div class="col-md-3">
                                                        <div class="col-md-12"><label class="control-label pull-left" >Adverse event</label></div>
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
                                                            <asp:DropDownList runat="server" ID="AdverseEventAction" CssClass="form-control input-sm" ClientIDMode="Static"  />
                                                                        
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
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">Adverse Events</div>
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
                                        </div>

                                </div>
                                <%--.panel--%>
                            </div>
                        </div>
                        <%--col-md-11--%>
                    </div>

                    <div class="col-md-12 form-group">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <div class="panel panel-info">

                                    <div class="panel-body">
                                        <div class="col-md-12">
                                            <h1 class=" control-label pull-left"><i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>Presenting Complaints</h1>
                                        </div>
                                        <div class="col-md-12">
                                            <hr />
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">Female LMP</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="datepicker fuelux" id="FemaleLMP">
                                                            <div class="input-group">
                                                                <input class="form-control input-sm" id="lmp" type="text" runat="server" ClientIDMode="Static" />
                                                                <div class="input-group-btn">
                                                                    <button id="btnFemaleLMP" type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
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
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">Pregnancy Status</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="examinationPregnancyStatus" CssClass="form-control input-sm" ClientIDMode="Static" onchange="EnableDisableEDD();" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">EDD</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="datepicker fuelux" id="EDCD">
                                                            <div class="input-group">
                                                                <input class="form-control input-sm" id="ExpectedDateOfChildBirth" type="text" runat="server" ClientIDMode="Static" />
                                                                <div class="input-group-btn">
                                                                    <button id="btnEDD" type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
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
                                            </div>
                                            <div class="col-md-4">
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">ANC/PNC Profile</label>
                                                    </div>
                                                    <div class="col-md-12 pull-left">
                                                        <label class="pull-left" style="padding-right:10px">
                                                            <input id="ancYes" type="radio" name="ANC" value="1" ClientIDMode="Static"  runat="server" />Yes
                                                        </label>
                                                        <label class="pull-left" style="padding-right:10px">
                                                            <input id="ancNo" type="radio" name="ANC" value="0" ClientIDMode="Static" runat="server" />No
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">On Family Planning</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="onFP" ClientIDMode="Static" CssClass="form-control input-sm" onChange="showHideFPControls();" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group" id="divOnFP">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">FP Method</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:ListBox runat="server" ID="fpMethod" ClientIDMode="Static" CssClass="form-control input-sm" SelectionMode="Multiple" />           
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group" id="divNoFP" style="display: none">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">Reason not on FP</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="ddlNoFP" ClientIDMode="Static" CssClass="form-control input-sm" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">CaCX Screeing</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="cacxscreening" ClientIDMode="Static" CssClass="form-control input-sm" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">STI Screeing</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="stiScreening" ClientIDMode="Static" CssClass="form-control input-sm" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">STI Partner Notification</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="stiPartnerNotification" CssClass="form-control input-sm" ClientIDMode="Static" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- .panel-body--%>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
                <%-- .data-step-1--%>

                <div class="step-pane sample-pane" data-step="2">
                    <%--<div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Chronic Illness </strong></small></div> <div class="col-md-12"><hr /> </div>--%>
                    <div class="col-md-12">

                        <div class="col-md-12">
                            <%--<div class="col-md-12"><hr /></div>--%>
                            <div class="panel panel-info">
                                <div class="panel-body">
                                    <div class="col-md-12 form-group">
                                        <label class="control-label pull-left">Chronic Illnesses & Comorbidities</label>
                                    </div>

                                    <div class="col-md-12 form-group">
                                        <div class="col-md-4 form-group">
                                            <div class="col-md-12">
                                                <label for="ChronicIllnessName" class="control-label pull-left">Illness</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList runat="server" ID="ChronicIllnessName" CssClass="form-control input-sm" ClientIDMode="Static" />
                                            </div>
                                        </div>

                                        <div class="col-md-3 form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Current Treatment</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="illnessTreatment" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="treatment.."></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2 form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Dose</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="treatmentDose" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="dose.."></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2 form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Duration</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" CssClass="form-control input-sm" ID="treatmentDuration" ClientIDMode="Static" placeholder="0"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-1">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left"><span class="fa fa-cog">Action</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddChronicIllness" onclick="AddChronicIllness();">Add</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 form-group">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">Chronic Conditions</div>
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
                                    <div class="col-md-12 form-group">
                                        <label class="control-label pull-left">Patient Allergies</label>
                                    </div>

                                    <div class="col-md-12 form-group">
                                        <div class="col-md-4 form-group">
                                            <div class="col-md-12">
                                                <label for="AllergyName" class="control-label pull-left">Allergy</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox ID="txtAllergy" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="allergy.."></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3 form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Response</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="txtAllergyResponse" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="response.."></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="col-md-12">
                                                <label class="control-label  pull-left">Allergy Date</label>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="datepicker fuelux" id="AllergyDate">
                                                    <div class="input-group">
                                                        <input class="form-control input-sm" id="txtAllergyDate" type="text" runat="server" ClientIDMode="Static" />
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
   

                                        <div class="col-md-1">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left"><span class="fa fa-cog">Action</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddAllergy" onclick="AddAllergy();">Add</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 form-group">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">Allergies</div>
                                            <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                                <table id="dtlAllergy" class="table table-bordered table-striped" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th><span class="text-primary">Allergy</span></th>
                                                            <th><span class="text-primary">Response</span></th>
                                                            <th><span class="text-primary">Date</span></th>
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
                                    <div class="col-md-12 form-group">
                                        <label class="control-label pull-left">Antigen Today</label>
                                    </div>

                                    <div class="col-md-12 form-group ">
                                        <div class="col-md-4">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Vaccine</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlVaccine" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Vaccine Stage</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlVaccineStage" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-12">
                                                <label class="control-label  pull-left">Vaccination Date</label>
                                            </div>
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
                                        <div class="col-md-1">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">Action</label>
                                            </div>
                                            <div class="col-md-12">
                                                <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddVaccine" onclick="AddVaccine();">Add</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 form-group">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">Antigen Today</div>
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

                                </div>
                                <%-- .panel-body--%>
                            </div>
                            <%--.panel--%>
                        </div>

                    </div>

                </div>
                <%-- .data-step-2--%>

                <div class="step-pane sample-pane" data-step="3">
                    <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Examination</strong></small></div>
                    <div class="col-md-12">
                        <hr />
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <h4 class="pull-left text-warning"><i class="fa fa-search fa-5x" aria-hidden="true"></i></h4>
                        </div>
                        <div class="col-md-11">
                            <div class="col-md-12 form-group">
                                <div class="col-md-3 form-group">
                                    <div class="col-md-12">
                                        <label for="ChronicIllnessName" class="control-label pull-left">Examination Type</label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="ddlExaminationType" CssClass="form-control input-sm" ClientIDMode="Static" />
                                    </div>
                                </div>

                                <div class="col-md-3 form-group">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left">Examination</label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="ddlExamination" CssClass="form-control input-sm" ClientIDMode="Static" />
                                    </div>
                                </div>

                                <div class="col-md-5 form-group">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left">Findings</label>
                                    </div>
                                    <div class="col-md-12">

                                        <asp:TextBox runat="server" ID="txtExamFindings" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="Findings.." Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left"><span class="fa fa-cog">Action</span></label>
                                    </div>
                                    <div class="col-md-4">
                                        <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddPhysicalExam" onclick="AddPhysicalExam();">Add</button>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 form-group">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">Patient Examination</div>
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
                    </div>

                </div>
                <%-- .data-step-3--%>

                <div class="step-pane sample-pane" data-step="4">
                    <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT MANAGEMENT</strong></small></div>
                    <div class="col-md-12">
                        <hr />
                    </div>

                    <div class="col-md-1">
                        <h4 class="pull-left text-danger"><i class="fa fa-bed fa-5x" aria-hidden="true"></i></h4>
                    </div>
                    <div class="col-md-11">
                        <div class="col-md-12">

                            <div class="col-md-4">
                                <h1 class="col-md-12">Positive Health,Dignity & Prevention (PHDP)</h1>
                                <div class="col-md-12">
                                    <hr />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group col-md-12" style="text-align: left">
                                            <asp:CheckBoxList ID="cblPHDP" runat="server"></asp:CheckBoxList>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-8">

                                <h1 class="col-md-12">Patient Diagnosis and Treatment</h1>
                                <div class="col-md-12">
                                    <hr />
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <label class="control-label pull-left">Diagnosis (ICD 10 Codes)</label>
                                    </div>
                                    <div class="col-md-5">
                                        <label class="control-label pull-left">Treatment</label>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label pull-left">Action</label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6 form-group">
                                        <input type="text" id="Diagnosis" class="form-control input-sm" placeholder="Type Diagnosis......" runat="server" clientidmode="Static" />
                                    </div>

                                    <div class="col-md-5 form-group">
                                        <input type="text" id="DiagnosisTreatment" class="form-control input-sm" placeholder="treatment" runat="server" clientidmode="Static" />
                                    </div>
                                    <div class="col-md-1 form-group">
                                        <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddDiagnosis" onclick="AddDiagnosis();">Add</button>
                                    </div>
                                </div>

                                <div class="col-md-12 form-group">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">Diagnosis</div>
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
                            </div>

                        </div>
                        <div class="col-md-12">
                            <hr />
                        </div>
                        
                        <div class="col-md-12">
                            <button type="button" class="btn btn-info btn-sm pull-left" data-toggle="modal" data-target="#adherenceAssessmentModal">Adherence Assessment</button>
                        </div>

                        <!-- Modal -->
                        <div id="adherenceAssessmentModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header bg-info">
                                        <!--<button type="button" class="close" data-dismiss="modal">&times;</button>-->
                                        <h4 class="modal-title">Adherence Assessment</h4>

                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                                 
                                            <div class="col-md-12 form-group">
                                                <div class="col-md-9">Questions</div>
                                                <div class="col-md-3">
                                                    <div class="col-md-6">Yes</div>
                                                    <div class="col-md-6">No</div>
                                                </div>
                                            </div>
                                                 
                                            <div class="col-md-12 form-group">
                                                <div class="col-md-9"><label class="control-label pull-left">Do you ever forget to take your medicine?</label></div>
                                                <div class="col-md-3">
                                                                     
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question1_Yes" runat="server" GroupName="Question1" ClientIDMode="Static" Value="1" />
                                                    </div>

                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question1_No" runat="server" GroupName="Question1" ClientIDMode="Static" Value="0" />
                                                    </div>
                                                    
                                                    <div class="errorBlock1" style="color: red;"> Please select one option </div>
                                                </div>
                                            </div>
                                                 
                                            <div class="col-md-12 form-group">
                                                <div class="col-md-9"><label class="control-label pull-left">Are you careless at times about taking your medicine?</label></div>
                                                <div class="col-md-3">
                                                                     
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question2_Yes" runat="server" GroupName="Question2" ClientIDMode="Static" Value="1" />
                                                    </div>

                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question2_No" runat="server" GroupName="Question2" ClientIDMode="Static" Value="0" />
                                                    </div>
                                                    
                                                    <div class="errorBlock2" style="color: red;"> Please select one option </div>
                                                </div>
                                            </div>
                                                 
                                                 
                                            <div class="col-md-12 form-group">
                                                <div class="col-md-9"><label class="control-label pull-left">Sometimes if you feel worse when you take the medicine, do you stop taking it?</label></div>
                                                <div class="col-md-3">
                                                                      
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question3_Yes" runat="server" GroupName="Question3" ClientIDMode="Static" Value="1" />
                                                    </div>

                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question3_No" runat="server" GroupName="Question3" ClientIDMode="Static" Value="0" />
                                                    </div>

                                                    <div class="errorBlock3" style="color: red;"> Please select one option </div>
                                                </div>
                                            </div>
                                                 
                                                 
                                            <div class="col-md-12 form-group">
                                                <div class="col-md-9"><label class="control-label pull-left">When you feel better do you sometimes stop taking your medicine?</label></div>
                                                <div class="col-md-3">
                                                                   
                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question4_Yes" runat="server" GroupName="Question4" ClientIDMode="Static" Value="1" />
                                                    </div>

                                                    <div class="col-md-6">
                                                        <asp:RadioButton ID="Question4_No" runat="server" GroupName="Question4" ClientIDMode="Static" Value="0" />
                                                    </div>

                                                    <div class="errorBlock4" style="color: red;"> Please select one option </div>
                                                </div>
                                            </div>

                                        </div>                                        
                                    </div>
                                    <div class="modal-footer">
                                        <div class="col-md-12">
                                            <button type="button" id="btnAdherenceAssessment" class="btn btn-default" OnClientClick="return false;">Save</button>
                                            <button type="button" id="btnAdherenceAssessmentCancel" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                        <div class="col-md-12">
                            <hr />
                        </div>

                        <div class="col-md-12">

                            <div class="col-md-5">

                                <div class="col-md-12 form-group">
                                    <div class="col-md-6">
                                        <label class="control-label pull-left">ARV Adherence</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" ID="arvAdherance" CssClass="form-control input-sm" ClientIDMode="Static" />
                                    </div>
                                </div>

                                <div class="col-md-12 form-group">
                                    <div class="col-md-6">
                                        <label class="control-label pull-left">CTX/Dapsone Adherence</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ctxAdherance" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7">


                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            <label class="control-label pull-left">Referred for </label>
                                        </div>
                                        <div class="col-md-7 form-group">
                                            <asp:DropDownList ID="ddlReferredFor" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            <label class="control-label pull-left">Next Appointment </label>
                                        </div>
                                        <div class="col-md-7">
                                            <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" data-toggle="modal" id="AddAppointment" clientidmode="Static" onclientclick="return false">Add Appointment</button>
                                        </div>
                                    </div>

                                    


                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
            <%-- .wizard--%>
        </div>
        <%--.col-md-12--%>
    </div>
</div><!-- .encounter-->

<script type="text/javascript">
    var genderId = <%=genderID%>;
    var gender = "<%=gender%>";

    $(document).ready(function () {
        $('.errorBlock1').hide();
        $('.errorBlock2').hide();
        $('.errorBlock3').hide();
        $('.errorBlock4').hide();

        showHideFPControls();

        var getVisitDateVal = "<%= this.visitdateval %>";
        var getFemaleLMPVal = "<%= this.LMPval %>";
        var getEDDPVal = "<%= this.EDDval %>";
        var getNxtAppDateVal = "<%= this.nxtAppDateval %>";
    
        if (getVisitDateVal == '' || getVisitDateVal == '01-Jan-1900')
            getVisitDateVal = 0;

        if (getFemaleLMPVal == '' || getFemaleLMPVal == '01-Jan-1900')
            getFemaleLMPVal = 0;

        if (getEDDPVal == '' || getEDDPVal == '01-Jan-1900')
            getEDDPVal = 0;

        if (getNxtAppDateVal == '' || getVisitDateVal == '01-Jan-1900') 
            getNxtAppDateVal = 0;
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
           $('#AllergyDate').datepicker({
               allowPastDates: true,
               date: 0,
               restricted: [{from: tomorrow, to: Infinity}],
               momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
               //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
           });
           $('#AntigenDate').datepicker({
               allowPastDates: true,
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
            if (gender == "Male") {
                
                $("#lmp").val("");
                $("#examinationPregnancyStatus").val("");
                $("#ExpectedDateOfChildBirth").val("");
                $("#cacxscreening").val("");

                $("#<%=lmp.ClientID%>").prop('disabled', true);
                $("#btnFemaleLMP").prop('disabled', true);
                $("#FemaleLMP").addClass('noevents');
                $("#<%=examinationPregnancyStatus.ClientID%>").prop('disabled', true);
                $("#<%=ExpectedDateOfChildBirth.ClientID%>").prop('disabled', true);
                $("#btnEDD").prop('disabled', true);
                $("#EDCD").addClass('noevents');
                $("#ancYes").prop('disabled', true);
                $("#ancNo").prop('disabled', true);
                $("#<%=cacxscreening.ClientID%>").prop('disabled', true);
            } else {
                $("#<%=lmp.ClientID%>").prop('disabled', false);
                $("#btnFemaleLMP").prop('disabled', false);
                $("#<%=examinationPregnancyStatus.ClientID%>").prop('disabled', false);
                $("#<%=ExpectedDateOfChildBirth.ClientID%>").prop('disabled', false);
                $("#btnEDD").prop('disabled', false);
                $("#ancYes").prop('disabled', false);
                $("#ancNo").prop('disabled', false);
                $("#<%=cacxscreening.ClientID%>").prop('disabled', false);

            }
            //.gender validation
            //pregnancy validations
       
            var pregnant = "Pregnant";

            var pregnancy = $("#<%=examinationPregnancyStatus.ClientID%>").find(':selected').text();

            if (pregnancy !== pregnant) {
                
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

            var allergyTable = $('#dtlAllergy').DataTable({
                ajax: {
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/GetAllergies",
                    dataSrc: 'd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                paging: false,
                searching: false,
                info: false,
                ordering: false
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
                    window.location.href = '<%=ResolveClientUrl("~/CCC/Encounter/PatientEncounter.aspx") %>';

                    var index = reactionEventList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                    if (index > -1) {
                        reactionEventList.splice(index, 1);
                    }

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

                    var index = chronicIllnessList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                    if (index > -1) {
                        chronicIllnessList.splice(index, 1);
                    }

                });

        ////dtlAllergy
            $("#dtlAllergy").on('click',
                '.btnDelete',
                function () {
                    allergyTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();

                    var index = AllergyList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                    if (index > -1) {
                        AllergyList.splice(index, 1);
                    }

                });

            ////dtlVaccines
            $("#dtlVaccines").on('click',
                '.btnDelete',
                function () {
                    vaccineTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();

                    var index = vaccineList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                    if (index > -1) {
                        vaccineList.splice(index, 1);
                    }

                    var index1 = vaccineStageList.indexOf($(this).parents('tr').find('td:eq(1)').text());
                    if (index1 > -1) {
                        vaccineStageList.splice(index1, 1);
                    }
                   
                });


            ////dtlPhysicalExam
            $("#dtlPhysicalExam").on('click',
                '.btnDelete',
                function () {
                    examTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();

                    var index = physicalExamList.indexOf($(this).parents('tr').find('td:eq(1)').text());
                    if (index > -1) {
                        physicalExamList.splice(index, 1);
                    }
                    
                });

            ////dtlDiagnosis
            $("#dtlDiagnosis").on('click',
                '.btnDelete',
                function () {
                    diagnosisTable
                        .row($(this).parents('tr'))
                        .remove()
                        .draw();

                    var index = diagnosisList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                    if (index > -1) {
                        diagnosisList.splice(index, 1);
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
                            //savePatientPatientManagement();
                            $.when(savePatientPatientManagement()).then(function() {
                                        setTimeout(function() {
                                                window.location
                                                    .href =
                                                    '<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
                                            },
                                            2000);
                                    });
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
                var visitScheduled = $("input[name$=Scheduled]:checked").val();
                var ANCProfile = $("input[name$=ANC]:checked").val();      
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
                var onFP = $("#<%=onFP.ClientID%>").find(":selected").val();
                var FPMethod = getSelectedItemsList('fpMethod');
                var ReasonNotOnFP = $("#<%=ddlNoFP.ClientID%>").find(":selected").val();
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
                        data: "{'VisitDate':'" + visitDate + "','VisitScheduled':'" + visitScheduled + "','VisitBy':'" + visitBy + "','Complaints':'" + complaints + "','TBScreening':'" + tbscreening + "','NutritionalStatus':'" + nutritionscreening + "','lmp':'" + LMP + "','PregStatus':'" + pregStatus + "','edd':'" + EDD + "','ANC':'" + ANCProfile + "', 'OnFP':'" + onFP + "','fpMethod':'" + FPMethod + "','ReasonNotOnFP':'" + ReasonNotOnFP + "','CaCx':'" + CaCx + "','STIScreening':'" + STIScreening + "','STIPartnerNotification':'" + STIPartnerNotification + "', 'adverseEvent':'" + JSON.stringify(adverseEventsArray) + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            
                            console.log(response.d);
                            if (response.d > 0)
                               
                                toastr.success(response.d, "Presenting Complaints");
                            else
                           
                                toastr.error(response.d,"Error occured while saving Presenting Complaints");
                        },
                        error: function (response) {
                         
                            toastr.error(response.d, "Error occured while saving Presenting Complaints");
                        }
                    });
                }
       


            function savePatientEncounterChronicIllness() {
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
                var rowCount = $('#dtlAllergy tbody tr').length;
                var AllergyArray = new Array();
                try {
                    for (var i = 0 ; i < rowCount; i++) {
                        AllergyArray[i] = {
                            "allergy": allergyTable.row(i).data()[0],
                            "allergyResponse": allergyTable.row(i).data()[1],
                            "allergyDate": allergyTable.row(i).data()[2]
                        }
                    }
                }
                catch (ex) { }
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
                    data: "{'chronicIllness':'" + JSON.stringify(chronicIllnessArray) + "','vaccines':'" + JSON.stringify(vaccineArray) + "','allergies':'" + JSON.stringify(AllergyArray) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "Chronic Illness");
                    },
                    error: function (response) {
                        toastr.error(response.d, "Chronic Illness Error");
                    }
                });
            }

            function savePatientPhysicalExams() {
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
                        toastr.error(response.d, "Physical Exam Error");
                    }
                });
            }


            function savePatientPatientManagement() {
                
                var phdp = getCheckBoxListItemsChecked('<%= cblPHDP.ClientID %>');
                var arvAdherence = $("#<%=arvAdherance.ClientID%>").find(":selected").val();
                var ctxAdherence = $("#<%=ctxAdherance.ClientID%>").find(":selected").val();
                var nextAppDate = "";
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
                        toastr.success(response.d, "Patient Management");
                    },
                    error: function (response) {
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

            function getSelectedItemsList(elementId)
            {
                var x = document.getElementById(elementId);
                var selectedValues = '';
                for (var i = 0; i < x.options.length; i++) {
                    if(x.options[i].selected){
                        selectedValues += x.options[i].value + ',';
                    }
                }
                return selectedValues;
            }

            $("#btnAdherenceAssessment").click(function() {
                if (!$('#adherenceAssessmentModal').parsley().validate()) {
                    return false;
                }
                var Question1 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question1']:checked").val());
                var Question2 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question2']:checked").val());
                var Question3 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question3']:checked").val());
                var Question4 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question4']:checked").val());

                $('.errorBlock1').hide();
                $('.errorBlock2').hide();
                $('.errorBlock3').hide();
                $('.errorBlock4').hide();

                if (isNaN(Question1)) {
                    $('.errorBlock1').show();
                    return false;
                }

                if (isNaN(Question2)) {
                    $('.errorBlock2').show();
                    return false;
                }

                if (isNaN(Question3)) {
                    $('.errorBlock3').show();
                    return false;
                }

                if (isNaN(Question4)) {
                    $('.errorBlock4').show();
                    return false;
                }

                $('.errorBlock1').hide();
                $('.errorBlock2').hide();
                $('.errorBlock3').hide();
                $('.errorBlock4').hide();

                console.log(Question1);
                console.log(Question2);
                console.log(Question3);
                console.log(Question4);

                var adherenceScore = Question1 + Question2 + Question3 + Question4;

                console.log(adherenceScore);
            });
    });
</script>