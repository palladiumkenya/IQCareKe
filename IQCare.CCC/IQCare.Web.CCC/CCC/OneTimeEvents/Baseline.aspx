<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="Baseline.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.Baseline" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

	<div class="col-md-12 col-sm-12 col-xs-12">
		<uc:PatientDetails ID="PatientSummary" runat="server" />
	</div>

	<div class="col-md-12 col-xs-12 col-sm-12">

		<div class="col-md-12 form-group">
			<div class="col-md-12">
				<div class="bs-callout bs-callout-danger hidden">
					<h4 class="fa fa-exclamation-circle">VALIDATION ERROR(S)</h4>
					<p>This form seems to be invalid :(</p>
				</div>
			</div>


			<div class="bs-callout bs-callout-info hidden">
				<h4 class="fa fa-check-square-o">All SECTION VALIDATION PASSED</h4>
				<p>Everything seems to be ok :)</p>
			</div>

		</div>
	</div>
	
	<div class="col-xs-12 col-sm-12 col-md-12">
		<div class="wizard" data-initialize="wizard" id="myWizard">
			<div class="steps-container">
				<ul class="steps">
					<li data-step="1" data-name="campaign" class="active">
						<span class="badge"> 1 </span>Patient Treatment History
							<span class="chevron"></span>
					</li>
<%--                    <li data-step="2">
						<span class="badge">2</span>HIV Diagnosis
							<span class="chevron"></span>
					</li>--%>
<%--                    <li data-step="3">
						<span class="badge">3</span>ARV History
							<span class="chevron"></span>
					</li>--%>
					<li data-step="2">
						<span class="badge"> 2 </span>Baseline Assessment & Treatment Initiation
							<span class="chevron"></span>
					</li>

					<%--<li data-step="3" data-name="template">
						<span class="badge"> 3 </span>Treatment Initiation
							<span class="chevron"></span>
					</li>--%>
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
				   
					 <div class="col-md-12" id="divTransferin">
						<div class="col-md-12"><small class="text-primary pull-left"> Patient Transfer Status</small></div>
						<div class="col-md-12">
						   <div class="col-md-12"><hr /></div>
						</div>

						<div class="col-md-12 form-group">
							<div class="col-md-4">
								<%--<div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblTINA">Transfer In ?</asp:Label></div>                                
										   <div class="col-md-12">
												<label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblTransferInYes">
													   <input runat="server" class="sr-only pull-left" name="TransferIn" id="TransferInYes" type="checkbox" value="Yes" ClientIDMode="Static" /> <span class="checkbox-label pull-left"> <strong>Yes</strong> <i> [if patient from another Hospital]</i></span> />
												</label>
										   </div>

										   <div class="col-md-12">
												<label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblTransferInNo">
													  <input runat="server" class="sr-only" id="TransferInNo" name="TransferIn" type="checkbox" value="No" checked="checked"> <span class="checkbox-label"><strong> No</strong> <i> [ if patient has not enrolled before]</i></span> />
												</label>
										   </div>--%>
							</div>

							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="lblTIDate">TransferIn Date</asp:Label></div>

								<div class="col-md-12">
									<div class="datepicker fuelux" id="TIDate">
										<div class="input-group">
											<input class="form-control input-sm" id="TransferInDate" type="text" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
											<div class="input-group-btn">
												<button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
												   <%-- <span class="glyphicon glyphicon-calendar"></span>--%>
													<span><i class="fa fa-calendar" aria-hidden="true"></i></span>
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

							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="lblARTStartDate">ART Start Date(First Ever)</asp:Label></div>
								<div class="col-md-12">
									<div class="datepicker fuelux" id="TIARTStartDate">
										<div class="input-group">
											<input class="form-control input-sm" id="StartDateART" type="text" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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

						<div class="form-group col-md-12">
							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="Label1">Regimen Category </asp:Label></div>
								<div class="col-md-12">
									<asp:DropDownList runat="server" ID="regimenCategory" ClientIDMode="Static" CssClass="form-control" data-parsley-min="1" data-parsley-required="true" />
								</div>
							</div>
							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="lblRegimen">Regimen</asp:Label></div>
								<div class="col-md-12">
									<asp:DropDownList runat="server" ID="RegimenId" CssClass="form-control" ClientIDMode="Static" data-parsley-min="1" data-parsley-required="true" />
								</div>
							</div>

						</div>

						<div class="col-md-12">
							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="lblfacility">Facility Transferred from :</asp:Label></div>
								<div class="col-md-12">
                                    <asp:DropDownList ID="TransferFromFacility" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="lblmflcode">MFL Code:</asp:Label></div>
								<div class="col-md-12">
									<asp:TextBox runat="server" ID="FacilityMFLCode" CssClass="form-control" ClientIDMode="Static" placeholder="mfl code" data-parsley-required="true" data-parsley-type="number" data-parsley-maxlength="5"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-4">
								<div class="col-md-12">
									<asp:Label runat="server" CssClass="control-label pull-left" ID="lblcounty">County:</asp:Label></div>
								<div class="col-md-12">
									<asp:DropDownList runat="server" ID="TransferFromCounty" CssClass="form-control" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1" />
								</div>
							</div>
						</div>
						<div class="form-group col-md-12">

							<div class="col-md-12">
								<div class="col-md-12">
									<label class="control-label pull-left">TransferIn Notes</label></div>
								<div class="col-md-12">
									<asp:TextBox runat="server" ID="transferInNotes" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
								</div>
							</div>


						</div>
					</div> <%-- .divTransferin--%>

					 <div class="col-md-12" id="divHivDiagnosis" data-parsley-validate="true" data-show-errors="true">
						  <div class="col-md-12">
							  <div class="col-md-12"><small class="text-primary pull-left"> Patient HIV Diagnosis</small></div>
						  </div>
						  
						  <div class="col-md-12">
							<div class="col-md-12"><hr /></div>
						  </div>

						  <div class="form-group col-md-12">
								<div class="col-md-4">
									<div class="col-md-12">
										<asp:Label runat="server" CssClass="control-label pull-left" ID="lblDateOfHIVDiagnosis">Date of HIV Diagnosis</asp:Label></div>
									<div class="col-md-12">
										<div class="datepicker fuelux" id="DHID">
											<div class="input-group">
												<input class="form-control input-sm" id="DateOfHIVDiagnosis" type="text" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
								<div class="col-md-4">
									<div class="col-md-12">
										<asp:Label runat="server" CssClass="control-label pull-left" ID="lblDateOfEnrollment">Date First Enrolled Into Care:</asp:Label></div>
									<div class="col-md-12">
										<div class="datepicker fuelux" id="DOE">
											<div class="input-group">
												<input class="form-control input-sm" id="DateOfEnrollment" type="text" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
								<div class="col-md-4">
									<div class="col-md-12">
										<asp:Label runat="server" CssClass="control-label pull-left" ID="lblwhostage">WHO Stage at Enrollment</asp:Label></div>
									<div class="col-md-12">
										<asp:DropDownList runat="server" ID="WHOStageAtEnrollment" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-min="1" />
									</div>
								</div>
						  </div>

						  <div class="form-group col-md-12">
								<div class="col-md-4">
									<div class="col-md-12">
										<asp:Label runat="server" CssClass="control-label pull-left" ID="lblDateOfARTInitiation">Date of ART Initiation</asp:Label></div>
									<div class="col-md-12">
										<div class="datepicker fuelux" id="DARTI">
											<div class="input-group">
												<input class="form-control input-sm" id="DateOfARTInitiation" type="text" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
								<div class="col-md-4"></div>
								<div class="col-md-4"></div>
					</div>

					 </div> <%-- .divHivDiagnosis--%>
					

					 <div class="col-md-12" id="divarvHistory" data-parsley-validate="true" data-show-errors="true">
						<div class="col-md-12">
							<div class="col-md-12"><small class="text-primary pull-left"> Patient ARV History</small></div>
						</div>

						<div class="col-md-12">
							<div class="col-md-12"><hr /></div>
						</div>

						<div class="form-group col-md-12">
							<div class="col-md-2"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblARTUse"><i aria-hidden="true"></i>History of ART Use?</asp:Label></div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ARTUseHistory" runat="server" ClientIDMode="Static" data-parsley-required="true" CssClass="form-control input-sm pull-left"></asp:DropDownList>
                            </div>
						</div>

						<div class="form-group col-md-12">
							<div class="col-md-3">
								<div class="col-md-12">
									<label class="control-label pull-left">Purpose</label></div>
								<div class="col-md-12">
                                    <asp:DropDownList ID="RegimenPurpose" runat="server" ClientIDMode="Static" onChange="selectRegimens();" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-min="1" data-parsley-min-message="Please select purpose"></asp:DropDownList>
								</div>
							</div>
                            
						    <div class="col-md-3">
						        <div class="col-md-12">
						            <label class="control-label pull-left" for="DropDownList1">Regimen Line</label></div>
						        <div class="col-md-12">
						            <asp:DropDownList ID="DropDownList1" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-min="1" data-parsley-min-message="Please select Regimen"></asp:DropDownList>
						        </div>
						    </div>
                            
						    <div class="col-md-2">
						        <div class="col-md-12">
						            <label class="control-label pull-left" for="DropDownList1"><i>Other Regimen not listed</i></label></div>
						        <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblOtherRegimen">
						            <input runat="server" class="sr-only pull-left" name="otherRegimen" id="otherRegimen" type="checkbox" value="Yes" ClientIDMode="Static" /> <span class="checkbox-label pull-left"> <strong>Other Regimen</strong> </span>
						        </label>
						    </div>

							<div class="col-md-2">
								<div class="col-md-12">
									<label class="control-label pull-left">Regimen</label></div>
								<div class="col-md-12">
									<asp:TextBox runat="server" ID="HistoryRegimen" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true"></asp:TextBox>
								</div>
							</div>
                            


							<div class="col-md-2">
								<div class="col-md-12">
									<label class="control-label pull-left">Date Last Used</label></div>
								<div class="col-md-12">
									<div class="datepicker fuelux" id="DLUsed">
										<div class="input-group">
											<input class="form-control input-sm" id="RegimenDateLastUsed" type="text" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
                         <div class="form-group col-md-12">
                             <div class="col-md-10"></div>
                             <div class="col-md-2">
                                <!-- <div class="col-md-12">
                                     <label class="control-label pull-right">Action</label></div> -->
                                 <div class="col-md-12">
                                     <asp:LinkButton runat="server" ID="AddPriorHistory" ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info btn-sm fa fa-plus-circle pull-right"> Add ART</asp:LinkButton>
                                 </div>
                             </div>
                         </div>

						<div class="col-md-12">
						<table class="table table-condensed table-striped" id="tblARVUseHistory">
							<thead>
								<tr>
									<th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">count.#</span></th>
									<th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Regimen Purpose</span></th>
									<th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Regimen</span></th>
									<th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true"></i><span class="text-primary">Date Last Used</span></th>
									<th><i class="fa fa-arrow-circle-o-right text-danger" aria-hidden="true"></i><span class="text-primary">Action</span></th>
								</tr>
							</thead>
							<tbody></tbody>
						</table>
					</div>
					 </div> <%-- .divArvHistory--%>
			   
				 </div><%-- .data-step1--%>
				
				 <div class="step-pane sample-pane" id="datastep2" data-parsley-validate="true" data-show-errors="true" data-step="2">
					<div class="col-md-12 form-group">

						<div class="col-md-12">
							<div class="col-md-12"><small class="text-primary pull-left"> Baseline Assessment</small></div>
							<div class="col-md-12">
								<hr />
							</div>
						</div>
						<div class="col-md-12 form-group">
							<div class="col-md-12">
								<label class="control-label pull-left">Baseline Assessment (Tick as appropriate)</label></div>
						</div>

						<div class=" form-group col-md-12">
							<div class="col-md-2 col-xs-12">
								<div class="col-md-12 col-xs-12 col-sm-12">
									<label class="control-label pull-left">Who Stage</label></div>
								<div class="col-md-12 col-xs-12 col-sm-12">
									<asp:DropDownList runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="bwhoStage" data-parsley-min="1" />
								</div>
							</div>

							<div class="col-md-2 col-xs-12">
								<div class="col-md-12 col-xs-12 col-sm-12">
									<label class="control-label pull-left" id="cd4countlabel">CD4 Count </label>
								</div>
								<div class="col-md-12">
									<asp:TextBox runat="server" CssClass="form-control input-sm" ID="bCd4Count" placeholder="cd4 count" ClientIDMode="Static" data-parsley-pattern="^[1-9]\d*(\.\d+)?$" ></asp:TextBox>
								</div>
							</div>

                            <div class="col-md-2 col-xs-12">
								<div class="col-md-12 col-xs-12 col-sm-12">
									<label class="control-label pull-left">HBV Infected </label>
								</div>
								<div class="col-md-12">
									<asp:RadioButtonList id="BVCoInfection" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
								</div>
							</div>
							<%--<div class="col-md-2 col-xs-12">
								<label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox" id="lblBVCoInfection">
									<input class="sr-only" type="checkbox" id="BVCoInfection" value="true">
									<span class="checkbox-label">HBV Infected</span>
								</label>
							</div>--%>

<%--                            <div class="col-md-2 col-xs-12">
								<label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox" id="lblPregnancy">
									<input class="sr-only" type="checkbox" id="Pregnancy" value="true">
									<span class="checkbox-label">Pregnant</span>
								</label>
							</div>--%>

							<%--                                 <div class="col-md-2 col-xs-12">
									  <label class="checkbox-custom checkbox-inline highlight" data-initialize="checkbox"  id="lblBHIV">
											<input class="sr-only" type="checkbox" id="BHIV" value="true"> <span class="checkbox-label"> B/HIV</span>
									  </label>
								 </div>--%>
						</div>

<%--                        <div class="form-group col-md-12">



							<div class="col-md-2 col-xs-12">
								<label class="checkbox-custom checkbox-inline" data-initialize="checkbox" id="lblBreastFeeding">
									<input class="sr-only" type="checkbox" id="BreastFeeding" value="true">
									<span class="checkbox-label">BreastFeeding</span>
								</label>
							</div>

							<div class="col-md-2 col-xs-12">
								<label class="checkbox-custom checkbox-inline" data-initialize="checkbox" id="lblTbInfected">
									<input class="sr-only" type="checkbox" id="TBInfected" value="true">
									<span class="checkbox-label">TB Infected</span>
								</label>
							</div>

							<div class="col-md-8"></div>

							<div class="col-md-3"></div>
							<div class="col-md-3"></div>
						</div>--%>

						<div class="form-group col-md-12">
							<div class="col-md-3">
								<div class="col-md-12">
									<asp:Label runat="server" class="control-label pull-left" ID="lblMUAC">MUAC </asp:Label></div>
								<div class="col-md-12">
									<asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineMUAC" CssClass="form-control input-sm" placeholder="MUAC" data-parsley-pattern="^[1-9]\d*(\.\d+)?$" data-parsley-required="true"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-3">
								<div class="col-md-12">
									<asp:Label runat="server" class="control-label pull-left" ID="lblWeight">Weight (Kgs) </asp:Label></div>
								<div class="col-md-12">
									<asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineWeight" CssClass="form-control input-sm" placeholder="0.0 kgs" data-parsley-required="true" data-parsley-pattern="^[1-9]\d*(\.\d+)?$" data-parsley-min="2"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-3">
								<div class="col-md-12">
									<asp:Label runat="server" class="control-label pull-left" ID="lblheight">Height (cm) </asp:Label></div>
								<div class="col-md-12">
									<asp:TextBox runat="server" ClientIDMode="Static" ID="BaselineHeight" CssClass="form-control input-sm" placeholder="0.00 cms" data-parsley-required="true" data-parsley-pattern="^[1-9]\d*(\.\d+)?$" data-parsley-min="3"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-3">
								
								<div class="col-md-12 AdultBMI">
									<asp:Label runat="server" class="control-label pull-left" ID="lblBMI">BMI </asp:Label></div>
								<div class="col-md-12 AdultBMI">
									<asp:TextBox runat="server" CssClass="form-control input-sm" ID="BaselineBMI" ClientIDMode="Static" ReadOnly="True" Type="Number"></asp:TextBox>
								</div>
                                <div class="col-md-12 PaedsBMI">
									<asp:Label runat="server" class="control-label pull-left" ID="Label3">BMIz </asp:Label></div>
								<div class="col-md-12 PaedsBMI">
									<asp:TextBox runat="server" CssClass="form-control input-sm" ID="txtBMIz" ClientIDMode="Static" Enabled="False"  ></asp:TextBox>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-12 form-group" id="divTreatmentInitiation">
						<div class="col-md-12">
							<div class="col-md-12"><small class="text-primary pull-left"> Treatment Inititation</small></div>
							<div class="col-md-12">
							   <div class="col-md-12"> <hr /></div>
							</div>
							<div class="col-md-12 form-group">
								<div class="col-md-4">
									<div class="col-md-12">
										<label class="control-label pull-left">Date Started On 1st Line</label></div>
									<div class="col-md-12">
										<div class="datepicker fuelux" id="DateStartedOnFirstLine">
											<div class="input-group">
												<input class="form-control input-sm" id="TreatmeantInitiationDateStartedOn1stLine" type="text" data-parsley-require="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" /><div class="input-group-btn">
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
								<div class="col-md-4">
									<div class="col-md-12">
										<label class="control-label pull-left">ART Cohort</label></div>
									<div class="col-md-12">
										<asp:TextBox runat="server" ID="ARTCohort" CssClass="form-control input-sm" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
									</div>
								</div>
                            </div>

						    <div class="col-md-12 form-group">
                                <div class="col-md-1">
                                    <div class="col-md-12">&nbsp;</div>                              
                                    <label class="checkbox-custom checkbox-inline highlight pull-left" data-initialize="checkbox" id="chkLDL_label">
                                        <input class="sr-only" type="checkbox" id="chkLDL" value="true">
                                        <span class="checkbox-label">LDL</span>
                                    </label>
                                </div>

								<div class="col-md-2">
									<div class="col-md-12">
										<label class="control-label pull-left" style="font-size: 65%">Baseline Viralload</label>
									</div>
									<div class="col-md-12">
										<asp:TextBox runat="server" CssClass="form-control input-sm" ID="BaselineViralload" ClientIDMode="Static"></asp:TextBox>
									</div>
								</div>

								<div class="col-md-3">
									<div class="col-md-12">
										<label class="control-label pull-left">Baseline Viraload Date</label></div>
									<div class="col-md-12">
										<div class="datepicker fuelux" id="BaselineViralloadDate">
											<div class="input-group">
												<input class="form-control input-sm" id="TreatmeantInitiationBaselineViralloadDate" type="text" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
							
								<div class="col-md-3">
									<div class="col-md-12">
										<label class="control-label pull-left">Regimen Category</label></div>
									<div class="col-md-12">
										<asp:DropDownList runat="server" ID="InitiationRegimen" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="0" />
									</div>
								</div>

								<div class="col-md-3">
									<div class="col-md-12">
										<label class="control-label pull-left">Regimen</label></div>
									<div class="col-md-12">
										<asp:DropDownList runat="server" ID="RegimenInitiationId" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="0" />
									</div>
								</div>

							</div>
						</div>

					</div><!-- .form-group-->
				</div> <%-- .data-step-2--%>
			   
				<%-- <div class="step-pane sample-pane" id="datastep5" data-parsley-validate="true" data-show-errors="true" data-step="3">--%>

				<%-- </div> .data-step-3--%>
			   
			</div><!-- .step-content -->


		</div> <%-- .wizard--%>
	</div>

	<script type="text/javascript">

	    function selectRegimens()
	    {
            var valSelected = $("#<%=RegimenPurpose.ClientID%>").find(":selected").text();

	      
           
	        if(valSelected === "Select")
	        {
	            $("#<%=DropDownList1.ClientID%>").prop('disabled', true);
	        }
	        else{
	            $("#<%=DropDownList1.ClientID%>").prop('disabled', false);
	        }

	        valSelected = valSelected.replace(/\s/g, '');

	        switch (valSelected) {
	        case "PEP":
	            valSelected = 'PePRegimen';
	            break;
                    
	        case "PrEP":
	            valSelected = 'PrEPRegimen';
	            break;
	        case "PMTCT":
	            valSelected = 'PMTCTRegimens';
	            break;
	        case "ART":
	            valSelected = 'BaselineART';
	            break;		            
	        default:
	        }

	      
	        $.ajax({
	            url: '../WebService/PatientEncounterService.asmx/GetRegimensBasedOnRegimenLine',
	            type: 'POST',
	            dataType: 'json',
	            data: "{'RegimenLine':'" + valSelected + "'}",
	            contentType: "application/json; charset=utf-8",
	            success: function (data) {
	                var serverData = data.d;
	                $("#<%=DropDownList1.ClientID%>").find('option').remove().end();
	                $("#<%=DropDownList1.ClientID%>").append('<option value="0">Select</option>');
	                for (var i = 0; i < serverData.length; i++) {
	                    $("#<%=DropDownList1.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
	                }
	            }
	        });
	    }
	    $(document).ready(function() {

	        var purposeList = new Array();
	        var userId = <%=UserId%>; /* get the current userId*/
	        var patientId = <%=PatientId%>;
	        var patientMasterVisitId = <%=PatientMasterVisitId%>;
	        var age = <%=age%>;

           
	        var purposeCount = 1;
	        var transferIn = 0;
	        var treatmentType = 0;
	        var patientType = "";
	        var gender = "<%=Gender%>";
	        var age = "<%=Age%>";
	        var dob = "#<%=DateOfBirth%>";
	        var pgStatus = "<%=PregnancyStatus%>";

	        var whostage = '';
	        var cD4Count = '';
	        var bVCoInfection = false;
	        var breastfeeding = false;
	        var pregnancy = false;
	        var bHiV = false;
	        var tbInfection = false;
	        var ldl = false;

	        var today = new Date();

	        if(age > 15)
	        {
	            $('.AdultBMI').show();
	            $('.PaedsBMI').hide();
	            //document.getElementById('divBMI').style.display = 'block';
	            //document.getElementById('peadsZScores').style.display = 'none';
	        }
	        else{
	            $('.AdultBMI').hide();
	            $('.PaedsBMI').show();
	        }

	        $('#TIARTStartDate').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        $('#TIDate').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        //dateof hiv diagnosis
	        $('#DHID').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            // restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        $('#DOE').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            // restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });
	        $('#DARTI').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        $('#DLUsed').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        $('#DateStartedOnFirstLine').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            // restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        $('#BaselineViralloadDate').datepicker({
	            date: null,
	            allowPastDates: true,
	            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
	            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
	        });

	        $("#<%=BaselineWeight.ClientID%>").on('change',
	            function() {
	                var bmi = calcBmi();
	                if(age > 15){
	                    $("#<%=BaselineBMI.ClientID%>").val(bmi);
	                }
	                else {
	                    calcZScore();
	                }


	            });
	        $("#<%=BaselineHeight.ClientID%>").on('change',
	            function() {
	                var bmi = calcBmi();
	                if(age>15){
	                    $("#<%=BaselineBMI.ClientID%>").val(bmi);
	                }   
	                else {
	                    calcZScore();
	                }
	            });

	        $("#<%=WHOStageAtEnrollment.ClientID%>").on('change',
	            function() {

	                var who = $(this).find(":selected").val();
	                $("#<%=bwhoStage.ClientID%>").val(who);

	            });

	        if (age <= 5) {
	            $("#cd4countlabel").text("CD4 Percentage");
	        }
	        else {
	            $("#cd4countlabel").text("CD4 Count");
	        }
	        function calcBmi() {
	            var weight = document.getElementById('BaselineWeight').value;
	            var height = document.getElementById('BaselineHeight').value / 100;
	            var bmi = (weight / (height * height)).toFixed(1); //BMI fomula
	            return bmi;

	            //  Less than 18.5  Underweight
	            // 18.5 to 25  Normal
	            // 25-30           Overweight
	            // More than 30    Obese
	        }

	        $("#TransferFromFacility").select2({
	            placeholder: "Select Facility"
	        });

	        /*Get Patient Type */
	        $(window).on("load", checkPatientStatus);
	        $(window).on("load",getPatientBaselinePreloadValues);
	        //Load Patient Baseline data
	        //alert(transferIn);c
	        //if(transferIn===1){
	        //   $(window).on("load",getPatientBaselinePreloadValues);
	        //}

	        //$(window).on("load",getPatientEnrollmentDate);




	        /*-- check for future dates -- check if ART start Date >TI Date */
	        $('#TIARTStartDate').on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {

	                var artStartDate = $('#TIARTStartDate').datepicker('getDate');
	                var tiDate = $('#TIDate').datepicker('getDate');
	                var doe = $('#DOE').datepicker('getDate');


	                $("#DARTI").datepicker('setDate',moment(artStartDate).format('DD-MMM-YYYY'));

	                var futureDate = moment(artStartDate).isAfter(today); /* -- validate future dates -- */
	                if (futureDate) {
	                    $('#StartDateART').val('');
	                    toastr.error("Future Dates NOT ALLOWED!");
	                    return false;
	                }

	                /*-- validate art start date with transfrindate*/
	                var isAfter = moment(artStartDate).isAfter(tiDate);
	                if (isAfter) {
	                    toastr.error("ART Start Date CANNOT be greater than transferin Date");
	                    $('#StartDateART').val('');
	                    return false;
	                }
	                if (moment(artStartDate).isBefore(doe)) {

	                    $('#StartDateART').val('');
	                    toastr.error('ART Start Date CANNOT be before Enrollment Date');
	                    return false;
	                }

	                $("#DARTI").datepicker("setDate", artStartDate);
	                var startMonth = artStartDate.format('MMM');
	                var startYear = artStartDate.getUTCFullYear();
	                $("#ARTCohort").val(startMonth + '-' + startYear);

	            });

	        /*--validate future dates */
	        $('#TIDate').on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
	                var tiDate = $('#TIDate').datepicker('getDate');
	                var futureDate = moment(tiDate).isAfter(today);
	                if (futureDate) {
	                    //$("#TIDate").datepicker();
	                    $("#TransferInDate").val('');
	                    toastr.error("future dates NOT allowed !");
	                    return false;
	                }
	            });

	        /* date last used disable future dates*/
	        $('#DLUsed').on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
	                var dlDate = $('#DLUsed').datepicker('getDate');
	                var futureDate = moment(dlDate).isAfter(today);
	                if (futureDate) {
	                    toastr.error("Future dates NOT allowed on Date Last Used Entries!");
	                    $("#RegimenDateLastUsed").val('');
	                    return false;
	                }
	            });

	        /* date of hiv diagnosis*/
	        $("#DHID").on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
	                var dhid = $("#DHID").datepicker('getDate');
	                var tidate = $("#TIDate").datepicker('getDate');
	                var artStartDate = $('#TIARTStartDate').datepicker('getDate');
	                var doe = $('#DOE').datepicker('getDate');
	                //validate dates

	                var futureDate = moment(dhid).isAfter(today);
	                if (futureDate) {
	                    $("#DateOfHIVDiagnosis").val('');
	                    toastr.error("future dates NOT allowed");
	                    return false;
	                }
	                futureDate = moment(dhid).isBefore(dob);
	                if (futureDate) {
	                    $("#DateOfHIVDiagnosis").val('');
	                    toastr.error("Date OF HIV Diagnosis CANNOT be before Date of Birth");
	                    return false;
	                }
	                futureDate = moment(dhid).isAfter(tidate);
	                if (futureDate) {
	                    $("#DateOfHIVDiagnosis").val('');
	                    toastr.error("Date OF HIV Diagnosis CANNOT be after Transferin Date");
	                    return false;
	                }
	                futureDate = moment(dhid).isAfter(doe);
	                if (futureDate) {
	                    $("#DateOfHIVDiagnosis").val('');
	                    toastr.error("Date OF HIV Diagnosis CANNOT be after Enrollment Date");
	                    return false;
	                }
	                futureDate = moment(dhid).isAfter(artStartDate);
	                if (futureDate) {
	                    $("#DateOfHIVDiagnosis").val('');
	                    toastr.error("Date OF HIV Diagnosis CANNOT be after ART Start date");
	                    return false;
	                }

	                futureDate=moment(dhid).isAfter(doe);
	                if (futureDate) {
	                    $("#DateOfHIVDiagnosis").val('');
	                    toastr.error("Date OF HIV Diagnosis CANNOT be after Enrollment Date");
	                    return false;
	                }
	            });

	        /*DateOfEnrollment*/
	        $("#DOE").on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
	                var doe = $("#DOE").datepicker('getDate');
	                var futureDate = moment(doe).isAfter(today);
	                if (futureDate) {
	                    $("#DateOfEnrollment").val('');
	                    toastr("future dates NOT allowed");
	                    return false;
	                }
	                var dhid = $("#DHID").datepicker('getDate');
	                var earlyDate = moment(doe).isBefore(dhid);
	                if (earlyDate) {
	                    $("#DateOfEnrollment").val('');
	                    toastr("Enrollment Date CANNOT be before HIV Diagnosis Date");
	                    return false;
	                }
	            });

	        /* date of ART Initiation */
	        $("#DARTI").on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
		            
	                var darti = $("#DARTI").datepicker('getDate');
	                var artStartDate = $('#TIARTStartDate').datepicker('getDate');

	                var futureDate = moment(darti).isAfter(artStartDate);
	                if (futureDate) {
	                    $("#DateOfARTInitiation").val('');
	                    toastr("ART Initiation Date CANNOT be Afer ART Start Date");
	                    return false;
	                }

	            });

	        $("#DateOfARTInitiation").prop("disabled", true);
	        //$("#DARTI").addClass("noneevents");

	        /* limit future dates viralload baseline date*/
	        $("#BaselineViralloadDate").on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
	                var dlDate = $('#BaselineViralloadDate').datepicker('getDate');
	                var futureDate = moment(dlDate).isAfter(today);
	                if (futureDate) {
	                    toastr.error("Future dates NOT allowed on Baseline ViralLoad Entries");
	                    $("#TreatmeantInitiationBaselineViralloadDate").val('');
	                    return false;
	                }
	                var dhid = $("#DHID").datepicker('getDate');
	                if (moment(dlDate).isBefore(dhid)) {
	                    $("#TreatmeantInitiationBaselineViralloadDate").val('');
	                    toastr.error("Baseline Viral Load date CANNOT be ealier than HIV Diagnosis Date");
	                    return false;
	                }
	            });

	        /* set cohort yeat and Month */
	        $("#DateStartedOnFirstLine").on('changed.fu.datepicker dateClicked.fu.datepicker',
	            function(event, date) {
	                var dhid = $("#DHID").datepicker('getDate');
	                var dateFirstLine = $("#DateStartedOnFirstLine").datepicker('getDate');
	                if (moment(dateFirstLine).isBefore(dhid)) {

	                    $("#TreatmeantInitiationDateStartedOn1stLine").val('');
	                    toastr.error('Date Started on Firstline Cannot be earlier than Date of HIV Diagnosis');
	                    return false;
	                } else {
	                    var dateStarted = $(this).datepicker('getDate');
	                    var startMonth = dateStarted.format('MMM');
	                    var startYear = dateStarted.getUTCFullYear();
	                    $("#ARTCohort").val(startMonth + '-' + startYear);
	                }
	            });

	        $("#ARTUseHistory").change(function() {
	            var ARTHistory = $("#ARTUseHistory").val();
	            if (ARTHistory == "1") {
	                noneUnchecked();
	            } else {
	                noneChecked();
	            }
	        });

	        /* clientside validation */
	        //disableIfNotTransferIn();

            $("#lblOtherRegimen").on('checked.fu.checkbox', function() {
                $("#<%=HistoryRegimen.ClientID%>").prop('disabled', false);
                $("#<%=HistoryRegimen.ClientID%>").val('');
                $("#<%=DropDownList1.ClientID%>").prop("disabled", true);
                $("#<%=DropDownList1.ClientID%>").val('');

            });
	        $("#lblOtherRegimen").on('unchecked.fu.checkbox', function() {
                $("#<%=HistoryRegimen.ClientID%>").prop('disabled', true);
	            $("#<%=HistoryRegimen.ClientID%>").val('');
	            $("#<%=DropDownList1.ClientID%>").prop("disabled", false);
	            $("#<%=DropDownList1.ClientID%>").val('');

	        });

	        noneChecked();

	        // $("#lblTransferInYes").checkbox('uncheck');
	        // $("#lblTransferInNo").checkbox('check');
	        $("#lblNONE").checkbox('check');

	        //$("#lblTransferInYes").on('checked.fu.checkbox',
	        //    function() {
	        //        //uncheck No
	        //        transferIn = 1;
	        //        $("#lblTransferInNo").checkbox('uncheck');
	        //        enableIfTransferIn();
	        //    });

	        //$("#lblTransferInYes").on('unchecked.fu.checkbox',
	        //    function () {
	        //        //uncheck No
	        //        $("#lblTransferInNo").checkbox('uncheck');
	        //        enableIfTransferIn();
	        //    });

	        //$("#lblTransferInNo").on("checked.fu.checkbox",
	        //    function() {
	        //        transferIn = 0;
	        //        $("#lblTransferInYes").checkbox('uncheck');
	        //        disableIfNotTransferIn();
	        //    });
	        //$("#lblTransferInNo").on("unchecked.fu.checkbox",
	        //    function() {
	        //        $("#lblTransferInYes").checkbox('uncheck');
	        //        disableIfNotTransferIn();
	        //    });
	        //$("#PrEP").on("checked.fu.checkbox",
	        //	function() {
	        //		treatmentType = 1;
	        //		$("#lblPMTCT").checkbox('uncheck');
	        //		$("#lblPEP").checkbox('uncheck');
	        //		$("#lblNONE").checkbox('uncheck');
	        //		$("#RegimenPurpose").val();
	        //		$("#RegimenPurpose").val("PrEP");
	        //		noneUnchecked();
	        //             });

	        //$("#lblPEP").on("checked.fu.checkbox",
	        //	function() {
	        //		treatmentType = 2;
	        //		$("#lblPMTCT").checkbox('uncheck');
	        //		$("#PrEP").checkbox('uncheck');
	        //		$("#lblNONE").checkbox('uncheck');
	        //		$("#RegimenPurpose").val();
	        //		$("#RegimenPurpose").val("PEP");
	        //		noneUnchecked();
	        //	});
	        //$("#lblPMTCT").on("checked.fu.checkbox",
	        //	function() {
	        //		treatmentType = 3;
	        //		$("#PrEP").checkbox('uncheck');
	        //		$("#lblPEP").checkbox('uncheck');
	        //		$("#lblNONE").checkbox('uncheck');
	        //		$("#RegimenPurpose").val();
	        //		$("#RegimenPurpose").val("PMTCT");
	        //		noneUnchecked();
	        //	});
	        //$("#lblNONE").on("checked.fu.checkbox",
	        //	function() {
	        //		treatmentType = 4;
	        //		$("#lblPMTCT").checkbox('uncheck');
	        //		$("#lblPEP").checkbox('uncheck');
	        //		$("#PrEP").checkbox('uncheck');
	        //		$("#RegimenPurpose").val("");
	        //		noneChecked();
	        //             });
	        $("#chkLDL_label").on("checked.fu.checkbox",
	            function() {
	                $("#BaselineViralload").prop("disabled", true);
	                ldl = true;
	                //$("#TreatmeantInitiationBaselineViralloadDate").prop("disabled", true);
	            });

	        $("#chkLDL_label").on("unchecked.fu.checkbox",
	            function () {
	                $("#BaselineViralload").prop("disabled", false);
	                ldl = false;
	                //$("#TreatmeantInitiationBaselineViralloadDate").prop("disabled", true);
	            });


	        $("#AddPriorHistory").click(function(e) {
	            var pusposeFound = 0;
                var regimenId = 1;

	            var regimen = '';

	            var purpose = $("#<%=RegimenPurpose.ClientID%>").find(":selected").text();
	            // var regimenId = $("#<%=HistoryRegimen%>").find(":selected").val();

                if ($("#lblOtherRegimen").checkbox('isChecked')) {
                    regimen = $("#<%=HistoryRegimen.ClientID%>").val();
                } else {

                   regimen= $('#<%=DropDownList1.ClientID%>').find(":selected").text();
                }
	         
	            var dateLastUsed = moment($("#DLUsed").datepicker('getDate'));
			   
	            if (!dateLastUsed.isValid()) {
	                toastr.warning("Date last used is Required!");
	                return false;
	            }
	            if (regimen.length===0) {
	                toastr.warning("Please provide Regimen Name");
	                return false;
	            }

	            if (moment(dateLastUsed).isAfter()) {

	                toastr.warning("Future dates not allowed for baseline assessment.");
	                return false;
	            }

	            if (regimenId < 1) {
	                toastr.warning("Select at least 1 ARV regimen used!");
	                return false;
	            }

	            if (purpose.length < 1) {
	                toastr.warning("ARV use Purpose is required!");
	                return false;
	            }
	            pusposeFound = $.inArray("" + purpose + "", purposeList);

	            if (pusposeFound > -1) {

	                toastr.warning(purpose + "Identifier already exisits in the List,", "Baseline Assessment ");
	                return false; // message box herer
	            } else {

	                purposeList.push("" + purpose + "");
	                var tr = '<tr><td align="left">' +
	                    purposeCount +
	                    '</td><td align="left">' +
	                    purpose +
	                    '</td><td align="left">' +
	                    regimen +
	                    '</td><td align="left">' +
	                    moment(dateLastUsed).format('DD-MMM-YYYY') +
	                    '</td><td align="right"><button type="button" class="btnDelete btn btn-danger fa fa-minus-circle btn-fill" > Remove</button></td></tr>';
	                $("#tblARVUseHistory>tbody:first").append('' + tr + '');


	                <%--$("#<%=RegimenPurpose.ClientID%>").val("");
				    $("#<%=HistoryRegimen.ClientID%>").val("");
                    $("#RegimenDateLastUsed").val("");--%>

	                purposeCount += 1;
	            }

	            e.preventDefault();

	        });

	        $("#tblARVUseHistory").on('click',
	            '.btnDelete',
	            function() {
	                $(this).closest('tr').remove();
	                var x = $(this).closest('tr').find('td').eq(0).html();
	                purposeList.splice($.inArray(x, purposeList), 1);
	            });

	        //datastep4
	        // $("#lblwhostage").checkbox('uncheck');
	        //$("#lblCD4Count").checkbox('uncheck');
	        $("#lblBVCoInfection").checkbox('uncheck');
	        $("#chkLDL_label").checkbox('uncheck');
	        //$("#lblPregnancy").checkbox('uncheck');
	        //$("#lblBreastFeeding").checkbox('uncheck');
	        $("#lblBHIV").checkbox('uncheck');
	        //$("#lblTbInfection").checkbox('uncheck');

	        if (gender === 'male') {
	            //$("#lblPregnancy").checkbox('disable');
	            //$("#lblBreastFeeding").checkbox('disable');
	            $("#lblPMTCT").checkbox('disable');
	        } else {
	            //$("#lblPregnancy").checkbox('enable');
	            //$("#lblBreastFeeding").checkbox('enable');
	            $("#lblPMTCT").checkbox('enable');
	        }

	        <%--if (age <= 2) {
				$("#<%=BaselineMUAC.ClientID%>").prop('disabled', false);
			} else {
				$("#<%=BaselineMUAC.ClientID%>").prop('disabled', true);
			}--%>

	        if (pgStatus > 0 && age >= 10) {
	            $("#<%=BaselineMUAC.ClientID%>").prop('disabled', false);
	        } else if(age >= 10) {
	            $("#<%=BaselineMUAC.ClientID%>").prop('disabled', true);
	        }

	        /* when checked */
	        // $("#lblwhostage").on('checked.fu.checkbox',function () { whostage = true; });
	        // $("#lblCD4Count").on('checked.fu.checkbox', function () { cD4Count = true; });
	        $("#lblBVCoInfection").on('checked.fu.checkbox', function() { bVCoInfection = true; });
	        //$("#lblPregnancy").on('checked.fu.checkbox', function () { pregnancy = true; });
	        //$("#lblBreastFeeding").on('checked.fu.checkbox', function () { breastfeeding = true; });
	        //$("#lblBHIV").on('checked.fu.checkbox', function() { bHiV = true; });
	        //$("#lblTbInfection").on('checked.fu.checkbox', function () { tbInfection = true; });

	        /* when unchecked */
	        // $("#lblwhostage").on('unchecked.fu.checkbox', function () { whostage = false; });
	        //$("#lblCD4Count").on('unchecked.fu.checkbox', function () { cD4Count = false; });
	        $("#lblBVCoInfection").on('unchecked.fu.checkbox', function() { bVCoInfection = false; });
	        //$("#lblPregnancy").on('unchecked.fu.checkbox', function () { pregnancy = false; });
	        //$("#lblBreastFeeding").on('unchecked.fu.checkbox', function () { breastfeeding = false; });
	        //$("#lblBHIV").on('unchecked.fu.checkbox', function() { bHiV = false; });
	        //$("#lblTbInfection").on('unchecked.fu.checkbox', function () { tbInfection = false; });

	        <%--         $("#<%=.ClientID%>").prop('disabled', true);
			$("#<%=.ClientID%>").prop('disabled', true);
			$("#<%=.ClientID%>").prop('disabled', true);
			 $("#<%=.ClientID%>").prop('disabled',true);--%>

	        function enableIfTransferIn() {
				
	            $("#divTransferin").show("fast", function() { $("#divHivDiagnosis").show("fast"); });
	            $("#divTreatmentInitiation").show("fast");

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
    
	        function calcZScore()
	        {
	            var weight = document.getElementById('BaselineWeight').value;
	            var height = document.getElementById('BaselineHeight').value;
      
	            $.ajax({
	                url: '../WebService/PatientEncounterService.asmx/getZScoreValues',
	                type: 'POST',
	                dataType: 'json',
	                data: "{'height':'" + height + "','weight':'" + weight + "'}",
	                contentType: "application/json; charset=utf-8",
	                success: function (data) {
	                    var serverData = data.d;
	                    for (var i = 0; i < serverData.length; i++) {
                   
	                        $("#<%=txtBMIz.ClientID%>").val(serverData[i][2]);
                    
                       
	                    }
	                }
	            });
	        }
	        function disableIfNotTransferIn() {

	            /*-- disable the DIVs not required --*/
	            $("#divTransferin").hide("fast", function() { $("#divHivDiagnosis").show("fast"); });
	            $("#divTreatmentInitiation").hide("fast");

	            $("#TIDate").datepicker('disable');
	            $("#TIARTStartDate").datepicker('disable');
	            $("#DateStartedOn1stLine").datepicker('disabled');
	            $("#<%=RegimenId.ClientID%>").prop('disabled', true);
	            $("#<%=regimenCategory.ClientID%>").prop('disabled', true);
	            $("#<%=TransferFromFacility.ClientID%>").prop('disabled', true);
	            $("#<%=FacilityMFLCode.ClientID%>").prop('disabled', true);
	            $("#<%=TransferFromCounty.ClientID%>").prop('disabled', true);
	            $("#<%=transferInNotes.ClientID%>").prop('disabled', true);
	        }

	        function noneChecked() {
                $("#<%=RegimenPurpose.ClientID%>").prop('disabled', true);
	            $("#<%=DropDownList1.ClientID%>").prop("disabled",true);
	            $("#<%=RegimenPurpose.ClientID%>").val("");
	            $("#<%=HistoryRegimen.ClientID%>").prop('disabled', true);
	            $("#<%=HistoryRegimen.ClientID%>").val("");
	            $("#DLUsed").datepicker("disable");
                $("#RegimenDateLastUsed").val("");               
	            $("#lblOtherRegimen").checkbox('disable');
	            $("#<%=AddPriorHistory.ClientID%>").attr("disabled");
	        }

	        function noneUnchecked() {
                $("#<%=RegimenPurpose.ClientID%>").prop('disabled', false);
	            $("#<%=DropDownList1.ClientID%>").prop("disabled", false);
	           // $("#<%=HistoryRegimen.ClientID%>").prop('disabled', false);
                $("#DLUsed").datepicker("enable");
	            $("#lblOtherRegimen").checkbox('enable');
	            $("#<%=AddPriorHistory.ClientID%>").removeAttr("disabled");
	        }

	        //$("#lblBVCoInfection").checkbox('uncheck');
	        //$("#lblPregnancy").checkbox('uncheck');
	        //$("#lblBreastFeeding").checkbox('uncheck');
	        //$("#lblBHIV").checkbox('uncheck');
	        //$("#lblTbInfection").checkbox('uncheck');

	        /*get Patient EnrollmentDate*/
	        function getPatientEnrollmentDate() {

	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/GetPatientEnrollmentDate",
	                data: "{'patientId':'" + patientId + "'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function(response) {
	                    $("#DOE").datepicker('setDate', moment(response.d).format('DD-MMM-YYYY'));
	                },
	                error: function(xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);
	                }
	            });
	        }
     

	        function getPatientDateARTInitiation() {
	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/GetPatientARTInitiation",
	                data: "{'patientId':'" + patientId + "'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function (response) {
	                    $("#DARTI").datepicker('setDate', moment(response.d).format('DD-MMM-YYYY'));
                       
	                },
	                error: function (xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);
	                }
	            });
	        }

		   

	        /* autopopulate values if filled before */
	        function getPatientBaselinePreloadValues() {
	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/GetPatientBaseline",
	                data: "{'patientId':'" + patientId + "'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function(response) {
	                    // var data =JSON.stringify(response.d);
	                    var data = jQuery.parseJSON(response.d);

	                    $.each(data,
	                        function(idx, obj) {
	                            //var dates = moment(obj.TransferInDate).format('DD-MMM-YYYY');

	                            if (obj.Id > 0) {
	                                //$("#TransferInDate").val(obj.TransferInDate);
	                                $("#TIDate")
	                                    .datepicker('setDate', moment(obj.TransferInDate).format('DD-MMM-YYYY'));
	                                $("#TIARTStartDate")
	                                    .datepicker('setDate', moment(obj.ARTInitiationDate).format('DD-MMM-YYYY'));

	                                /* HIV DIAGNOSIS */
	                                if (obj.HivDiagnosisDate) {
	                                    $("#DHID")
	                                        .datepicker('setDate', moment(obj.HivDiagnosisDate).format('DD-MMM-YYYY'));
	                                }
	                                $("#DOE").datepicker('setDate', moment(obj.EnrollmentDate).format('DD-MMM-YYYY'));

	                                if (obj.ARTInitiationDate) {
	                                    $("#DARTI").datepicker('setDate', moment(obj.ARTInitiationDate).format('DD-MMM-YYYY'));
	                                } else {
	                                    $("#DARTI").datepicker('setDate', moment(obj.ARTInitiationDateNew).format('DD-MMM-YYYY'));
	                                }
                                    
	                                $("#WHOStageAtEnrollment").val(obj.EnrollmentWHOStage);

	                                $("#ARTUseHistory").val(obj.HistoryARTUse);

	                                $("#<%=TransferFromCounty.ClientID%>").val(obj.CountyFrom);
	                                var value = $("#<%=TransferFromFacility.ClientID%> option:contains('" + obj.FacilityFrom + "')").val();
	                                $("#<%=TransferFromFacility.ClientID%>").val(value).trigger("change");
	                                $("#<%=FacilityMFLCode.ClientID%>").val(obj.mflcode);
	                                $("#<%=transferInNotes.ClientID%>").val(obj.TransferInNotes);


	                                /* -- Baseline Assessment */
	                                $("#<%=bwhoStage.ClientID%>").val(obj.WHOStage);
	                                $("#<%=bCd4Count.ClientID%>").val(obj.CD4Count);
	                                $("#<%=BaselineMUAC.ClientID%>").val(obj.MUAC);
	                                $("#<%=BaselineWeight.ClientID%>").val(obj.Weight);
	                                $("#<%=BaselineHeight.ClientID%>").val(obj.Height);
                                    
	                                if(age > 15){
	                                    $("#<%=BaselineBMI.ClientID%>").val(obj.BMI);
	                                }
	                                else {
	                                    calcZScore();
                                                                                            

	                                }                                                                               
	                                if (obj.HBVInfected) {
	                                    var value = 104;
	                                    $(".BVCoInfectioninput input[value=" + value + "]").attr('checked', 'checked');
	                                } else {
	                                    var value = 105;
	                                    $(".BVCoInfectioninput input[value=" + value + "]").attr('checked', 'checked');
	                                }
	                                //if (obj.Pregnant) {$("#lblPregnancy").checkbox('check');}else{$("#lblPregnancy").checkbox('uncheck');}
	                                //if (obj.TBinfected) {$("#lblTbInfected").checkbox('check');}else{ $("#lblTbInfected").checkbox('uncheck');}
	                                //if(obj.BreastFeeding){$("#lblBreastFeeding").checkbox('check')}else{$("#lblBreastFeeding").checkbox('uncheck')}

									
	                                $("#DateStartedOnFirstLine").datepicker('setDate', moment(obj.DateStartedOnFirstline) .format('DD-MMM-YYYY'));
	                                $("#<%=ARTCohort.ClientID%>").val(obj.Cohort);
	                                $("#BaselineViralload").val(obj.BaselineViralLoad);
	                                if (obj.ldl) {
	                                    $("#chkLDL_label").checkbox('check');
	                                } else {
	                                    $("#chkLDL_label").checkbox('uncheck');
	                                }
                                    

	                                $("#BaselineViralloadDate")
	                                    .datepicker('setDate', moment(obj.BaselineViralLoadDate).format('DD-MMM-YYYY'));

	                                console.log(obj.CurrentTreatmentName);
                                    
	                                if(obj.CurrentTreatmentName !=null)   
	                                {
	                                    // $("#").datepicker('setDate',moment(obj.HivDiagnosisDate).format('DD-MMM-YYYY'));
	                                    $.ajax({
	                                        type: "POST",
	                                        url: "../WebService/PatientBaselineService.asmx/GetRegimenCategoryByRegimenName",
	                                        data: "{'regimenName':'" + obj.CurrentTreatmentName.substr(0,4) + "'}",
	                                        contentType: "application/json; charset=utf-8",
	                                        dataType: "json",
	                                        success: function(response) {

	                                            var masterId = response.d;
										   
	                                            /* Get the LookupMaster Name*/
	                                            $.ajax({
	                                                type: "POST",
	                                                url: "../WebService/LookupService.asmx/GetLookUpMasterNameFromId",
	                                                data: "{'masterId':'" + masterId + "'}",
	                                                contentType: "application/json; charset=utf-8",
	                                                dataType: "json",
	                                                success: function(response) {

	                                                    var itemNameSource = response.d;
	                                                    var itemName = response.d;
	                                                    if (itemName === 'AdultFirstLineRegimen') {
	                                                        itemName='AdultARTFirstLine';
	                                                    }

												  
	                                                    if (itemName === 'AdultSecondlineRegimen') {
	                                                        itemName ='AdultARTSecondLine';
	                                                    }
	                                                    if (itemName === 'AdultThirdlineRegimen') {
	                                                        itemName ='AdultARTThirdLine';
	                                                    }
	                                                    if (itemName === 'PaedsFirstLineRegimen') {
	                                                        itemName = 'PaedsARTFirstLine';
	                                                    }
	                                                    if (itemName === 'PaedsSecondlineRegimen') {
	                                                        itemName = 'PaedsARTSecondLine';
	                                                    }

                                                    
	                                                    $.ajax({
	                                                        type: "POST",
	                                                        url: "../WebService/LookupService.asmx/GetLookupItemId",
	                                                        data: "{'lookupItemName':'" + itemName + "'}",
	                                                        contentType: "application/json; charset=utf-8",
	                                                        dataType: "json",
	                                                        success: function(response) {
	                                                            var itemList = response.d;
	                                                            $("#<%=regimenCategory.ClientID%>").val(itemList);

	                                                            ////////////////////////////////////////////////
	                                                            $.ajax({
	                                                                type: "POST",
	                                                                url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
	                                                                data: "{'masterName':'" + itemNameSource + "'}",
	                                                                contentType: "application/json; charset=utf-8",
	                                                                dataType: "json",
	                                                                success: function(response) {
	                                                                    var itemList = JSON.parse(response.d);
												    
	                                                                    $("#<%=RegimenId.ClientID%>").find('option').remove().end();
	                                                                    $("#<%=RegimenId.ClientID%>")
	                                                                        .append('<option value="0">Select</option>');
	                                                                    $.each(itemList,
	                                                                        function(index, itemList) {
	                                                                            $("#<%=RegimenId.ClientID%>")
	                                                                                .append('<option value="' +
	                                                                                    itemList.ItemId +
	                                                                                    '">' +
	                                                                                    itemList.ItemName +
	                                                                                    "(" +
	                                                                                    itemList.ItemDisplayName +
	                                                                                    ")" +
	                                                                                    '</option>');
	                                                                        });
	                                                                    $("#<%=RegimenId.ClientID%>").val(obj.CurrentTreatment);
	                                                                },
	                                                                error: function(response) {
	                                                                    toastr
	                                                                        .error("Error in Fetching Ward list " + response.d);
	                                                                }
	                                                            }); // ajax end
										
	                                                        },
	                                                        error: function(response) {
	                                                            toastr
	                                                                .error("Error in Fetching Lookupmaster " + response.d);
	                                                        }
	                                                    }); // ajax end
												    
												   
											
	                                                    /////////////////////
	                                                },
	                                                error: function(response) {
	                                                    toastr
	                                                        .error("Error in Fetching Ward list " + response.d);
	                                                }
	                                            }); // ajax end

	                                        },
	                                        error: function(response) {}
	                                    }); //ajax end 
	                                    //----------------------
	                                }
	                                $.ajax({
	                                    type: "POST",
	                                    url: "../WebService/PatientBaselineService.asmx/GetRegimenCategory",
	                                    data: "{'regimenId':'" + obj.Regimen + "'}",
	                                    contentType: "application/json; charset=utf-8",
	                                    dataType: "json",
	                                    success: function(response) {
	                                        $("#<%=InitiationRegimen.ClientID%>").val(response.d);
	                                        console.log(response.d);
	                                        var reg = $("#<%=InitiationRegimen.ClientID%>").find(":selected").text();
	                                        var str = reg.replace(/\s+/g, '');

	                                        $.ajax({
	                                            type: "POST",
	                                            url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
	                                            data: "{'masterName':'" + str + "'}",
	                                            contentType: "application/json; charset=utf-8",
	                                            dataType: "json",
	                                            success: function(response) {
	                                                var itemList = JSON.parse(response.d);
	                                                $("#<%=RegimenInitiationId.ClientID%>").find('option').remove()
	                                                    .end();
	                                                $("#<%=RegimenInitiationId.ClientID%>")
	                                                    .append('<option value="0">Select</option>');
	                                                $.each(itemList,
	                                                    function(index, itemList) {
	                                                        $("#<%=RegimenInitiationId.ClientID%>")
	                                                            .append('<option value="' +
	                                                                itemList.ItemId +
	                                                                '">' +
	                                                                itemList.ItemName +
	                                                                "(" +
	                                                                itemList.ItemDisplayName +
	                                                                ")" +
	                                                                '</option>');
	                                                    });
	                                                $("#<%=RegimenInitiationId.ClientID%>").val(obj.Regimen);
	                                            },
	                                            error: function(response) {
	                                                toastr
	                                                    .error("Error in Fetching Ward list " + response.d,
	                                                        "Fetching Ward List");
	                                            }
	                                        }); // ajax end
	                                    },
	                                    error: function(response) {}
	                                }); //ajax end 

	                            } //end of loop

	                        });

	                },
	                error: function(response) {
	                    toastr.error("Error in Fetching Ward list " + response.d);
	                }
	            });
	        }

	        $("#myWizard").on("actionclicked.fu.wizard",function(evt, data) {
	                var currentStep = data.step;
	                var nextStep = 0;
	                var previousStep = 0;
	                var totalError = 0;
	                var stepError = 0;
	                var transferStatus = 0;
	                var hivDiagnosis = 0;
	                var arvHistory = 0;
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

	                    if ($("#datastep1").parsley().validate()) {
                                  
	                        if (transferIn === 1) {
	                            $.when(addPatientTransferIn()).then(managePatientHivDiagnosis());
	                            managePatientArvHistory();
	                        } else if (transferIn === 2) {
								   
	                            $.when(managePatientHivDiagnosis()).then(managePatientArvHistory());
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

							    
	                        if (transferIn === 1) {
	                            $.when(managePatientBaselineAssessment()).then(function () { setTimeout(function () { managePatientTreatmentInitiation(); }, 2000); });


	                            if (data.direction === 'next') {
	                                window.location.href = '<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
	                                toastr.success("Patient Baseline Assessment and Treatment initiation Completed successfully...");
	                            }
								 
								   
	                        } else {
	                            $.when(managePatientBaselineAssessment()).then()
                                        
	                            if (data.direction === 'next') {
	                                window.location.href = '<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
	                                toastr.success("Patient Baseline Assessment and ARV History Completed successfully...");
	                            }
								  
	                        }
								
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

	        function onInitiationRegimen() {
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
	        }

	        $("#InitiationRegimen").on("change", function() {
	            $("#InitiationRegimen").off();
	            $("#InitiationRegimen").on("change", onInitiationRegimen);
	        });                      

	        $("#InitiationRegimen").on("change", onInitiationRegimen);

	        /* datat persistence functions */
	        function addPatientTransferIn() {

	            var serviceAreaId = 0;
	            var transferInDate = moment($('#TIDate').datepicker('getDate')).format('DD-MMM-YYYY');
	            var treatmentStartDate = moment($('#TIARTStartDate').datepicker('getDate')).format('DD-MMM-YYYY');
	            <%--var treatmentStartDate = moment($('#<%=lblARTStartDate.ClientID%>').datepicker('getDate')).format('DD-MMM-YYYY'); --%>
	            var currentTreatment = $('#<%=RegimenId.ClientID%>').find(":selected").val();
	            var TransferFromFacility = $('#<%=TransferFromFacility.ClientID%>').select2('data');
	            <%--		var facilityFrom = $('#<%=TransferFromFacility.ClientID%>').val();--%>
	            var facilityFrom = TransferFromFacility[0].text;
	            var mflCode = $('#<%=FacilityMFLCode.ClientID%>').val();
	            var countyFrom = $('#<%=TransferFromCounty.ClientID%>').find(":selected").val();
	            var transferInNotes = $('#<%=transferInNotes.ClientID%>').val();
	            var ptnId = patientId;
	            var ptnmasterVisitId = patientMasterVisitId;

	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/ManagePatientTransferStatus",
	                data: "{'patientId':'" + ptnId +"','patientMastervisitId':'" +ptnmasterVisitId +"','transferinDate':'" +transferInDate +"','treatmentStartDate':'"+treatmentStartDate+"','serviceAreaId':'" +serviceAreaId +"','currentTreatment':'" +currentTreatment +"','facilityFrom':'" +
	                    facilityFrom +"','mflCode':'" +mflCode +"','countyFrom':'" +countyFrom +"','transferInNotes':'" +transferInNotes +"','userId':'" +userId +"'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function(response) {
	                    toastr.success(response.d);
	                },
	                error: function(xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);

	                }
	            });
	        }

			
	        function managePatientHivDiagnosis() {

	            var id = 0;
	            var hivDiagnosisDate = moment($('#DHID').datepicker('getDate')).format('DD-MMM-YYYY');
	            var enrollmentDate = moment($('#DOE').datepicker('getDate')).format('DD-MMM-YYYY');
	            var artInitiationDate = moment($('#DARTI').datepicker('getDate')).format('DD-MMM-YYYY');
	            var enrollmentWhoStage = $('#<%=WHOStageAtEnrollment.ClientID%>').find(":selected").val();
	            var historyARTUse = $('#<%=ARTUseHistory.ClientID%>').find(":selected").val();
	            var ptnId = patientId;
	            var ptnmasterVisitId = patientMasterVisitId;
	            if (artInitiationDate === 'Invalid date') {
	                artInitiationDate = '';};
	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/ManagePatientHivDiagnosis",
	                data: "{'id':'" +id +"','patientId':'" +ptnId +"','patientMasterVisitId':'" + ptnmasterVisitId +"','hivDiagnosisDate':'" +hivDiagnosisDate +"','enrollmentDate':'" + enrollmentDate +"','enrollmentWhoStage':'" + enrollmentWhoStage +"','artInitiationStr':'" +artInitiationDate + "','userId':'" + userId +"','historyARTUse':'" + historyARTUse +"'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function(response) {
	                    toastr.success(response.d, "Patient HIV Dignosis Status");
	                },
	                error: function(xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);

	                }
	            });
	        }




		   


	        function managePatientArvHistory() {

	            var id = 0;
	            var ptnId = patientId;
	            var ptnmasterVisitId = patientMasterVisitId;
	            var treatment = 'ART';
	            //if (treatmentType === 1) {
	            //    treatment = 'PrEP';
	            //} else if (treatmentType === 2) {
	            //    treatment = 'PEP';
	            //} else if (treatmentType === 3) {
	            //    treatment = 'PMTCT';
	            //}

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
	                url: "../WebService/PatientBaselineService.asmx/ManagePatientArvHistory",
	                data: "{'id':'" + id + "','patientId':'" + ptnId + "','patientMasterVisitId':'" + ptnmasterVisitId + "','artuseStrings':'" + jsonArtHistory + "','userId':'" + userId +
	                    "'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function(response) {
	                    toastr.success(response.d);
	                },
	                error: function(xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);
	                }
	            });
	        }

			
	        function managePatientBaselineAssessment() {

	            var muac= $("#<%=BaselineMUAC.ClientID%>").val();
	            var weight= $("#<%=BaselineWeight.ClientID%>").val(); 
	            var height= $("#<%=BaselineHeight.ClientID%>").val();
	            whostage = $("#<%=bwhoStage.ClientID%>").find(":selected").val();
	            cD4Count = $("#<%=bCd4Count.ClientID%>").val();
	            var hbvvalue = $(".BVCoInfectioninput input:checked").val();
	            if (!cD4Count) {
	                cD4Count = 0;
	            }

	            var id = 0;
	            if(muac<1){muac = 0;}
	            if (weight < 1) {
	                weight = 0;}
	            if (height < 1) {
	                height = 0;}
	            var ptnId = patientId;
	            var ptnmasterVisitId = patientMasterVisitId;
			    
	            // var datas = [{"id": id ,"patientId": ptnId  ,"patientMasterVisitId": ptnmasterVisitId ,"pregnant": pregnancy,"hbvInfected":bVCoInfection,"tbInfected":tbInfection,"whoStage": whostage,"breastfeeding": breastfeeding ,"cd4Count":cD4Count,"muac": muac,"weight":weight,"height":height,"userId": userId }];
	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/ManagePatientBaselineAssessment",
	                data:  "{'id':'"+id+"','patientId':'"+patientId+"','patientMasterVisitId':'"+patientMasterVisitId+"','pregnant':'"+pregnancy+"','hbvInfected':'"+hbvvalue+"','tbInfected':'"+tbInfection+"','whoStage':'"+whostage+"','breastfeeding':'"+breastfeeding+"','cd4Count':'"+cD4Count+"','muac':'"+muac+"','weight':"+weight+",'height':"+height+",'userId':'"+userId+"'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function (response) {
	                    //toastr.success(response.d);
	                },
	                error: function (xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);
	                }
	            });
	        }

	        function managePatientTreatmentInitiation() {
				
	            var viralLoad= $("#<%=BaselineViralload.ClientID%>").val(); 
	            var viralLoadDate = moment($("#BaselineViralloadDate").datepicker("getDate")).format("DD-MMM-YYYY");
	            var artCohort= $("#<%=ARTCohort.ClientID%>").val(); 
	            var firstlineStartDate= moment($("#DateStartedOnFirstLine").datepicker('getDate')).format('DD-MMM-YYYY');
	            var startRegimen = $("#<%=RegimenInitiationId.ClientID%>").find(":selected").val();
	            if (viralLoad < 1) {
	                viralLoad = 0;}
	            var id = 0;
	            var ptnId = patientId;
	            var ptnmasterVisitId = patientMasterVisitId;

	            if (viralLoadDate == "Invalid date") {
	                viralLoadDate = "";
	            }

	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/ManagePatientTreatmentInitiation",
	                data: "{'id':'" + id + "','patientId':'" + ptnId + "','patientMasterVisitid':'" + ptnmasterVisitId + "','dateStartedOnFirstLine':'" + firstlineStartDate + "','cohort':'" + artCohort + "','regimen':'" + startRegimen + "', 'ldl': '" + ldl + "','baselineViralload':'" + viralLoad + "','baselineViralLoadDate':'" + viralLoadDate + "','userId':'" + userId +
	                    "'}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function (response) {
	                    // <%--window.location.href('<%=ResolveClientUrl("~/CCC/patient/PatientHome.aspx")%>');--%>
	                    toastr.success(response.d);
						
	                },
	                error: function (xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);
	                }
	            });
	        }

	        function checkPatientStatus()
	        {
	            $.ajax({
	                type: "POST",
	                url: "../WebService/PatientBaselineService.asmx/GetPatientType",
	                data: "{'patientId':"+patientId  +"}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function(response) {
	                    patientType=response.d;
					   
	                    if(patientType==='Transfer-In'){ transferIn=1; enableIfTransferIn(); } else if(patientType==='New') { transferIn=2; disableIfNotTransferIn();}else { transferIn=3;}
							  
	                    if(transferIn===1) {
					
	                        getPatientBaselinePreloadValues();
	                        getPatientEnrollmentDate();
	                    }else if(transferIn===2){
	                        getPatientEnrollmentDate();
	                        getPatientDateARTInitiation();
	                    }
		   
					   
					   
	                },
	                error:function(xhr, errorType, exception) {
	                    var jsonError = jQuery.parseJSON(xhr.responseText);
	                    toastr.error("" + xhr.status + "" + jsonError.Message);
	                    return false;
	                }
	            });

	        }

	        $("#TransferFromFacility").change(function () {
	            var value = $(this).val();

	            $("#FacilityMFLCode").val(value);
	            //console.log(value);
	        });

	    });
	</script>

</asp:Content>