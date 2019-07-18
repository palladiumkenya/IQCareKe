<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientClinicalEncounter.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientClinicalEncounter" %>

<%@ Register Src="~/CCC/UC/ucIpt.ascx" TagPrefix="uc" TagName="Ipt" %>
<%@ Register Src="~/CCC/UC/ucIptClientWorkup.ascx" TagPrefix="uc" TagName="IptClientWorkup" %>
<%@ Register Src="~/CCC/UC/ucIptOutcome.ascx" TagPrefix="uc" TagName="IptOutcome" %>
<%@ Register Src="~/CCC/UC/ucPharmacyPrescription.ascx" TagPrefix="uc" TagName="ucPharmacyPrescription" %>
<%@ Register Src="~/CCC/UC/ucPatientLabs.ascx" TagPrefix="uc" TagName="ucPatientLabs" %>
<%@ Register Src="~/CCC/UC/ucGenderBasedViolenceAssessment.ascx" TagPrefix="uc" TagName="ucGenderBasedViolenceAssessment" %>
<%@ Register Src="~/CCC/UC/ucNeonatalHistory.ascx" TagPrefix="uc" TagName="ucNeonatalHistory" %>
<%@ Register Src="~/CCC/UC/ucICF.ascx" TagPrefix="uc" TagName="ucICF" %>

<style>
    #ICFScreeningSection, #TuberclosisTreatmentPanel, #IPTPanel, #ICFActionTakenPanel{display: none;}
</style>

<div class="col-md-12" style="padding-top: 20px">

	<div class="col-md-12">
		<div class="wizard" data-initialize="wizard" id="myWizard">
			<div class="steps-container">
				<ul class="steps">presenting

					<li data-step="1" data-name="template" class="active">
						<span class="badge">1</span>Presenting Complaints
								<span class="chevron"></span>
					</li>

					<li data-step="2" id="dsAdditionalHistory">
						<span class="badge">2</span>Additional History
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

			<%--<div class="actions">
				<button type="button" class="btn btn-default btn-prev">
					<span class="glyphicon glyphicon-arrow-left"></span>Prev</button>
				<button type="button" class="btn btn-primary btn-next" data-last="Complete">
					Next
							<span class="glyphicon glyphicon-arrow-right"></span>
				</button>
			</div>--%>

			<div class="step-content">

				<div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">

					<div class="col-md-12 form-group">
						<div class="col-md-4">
							
							  <div class="col-md-12"><label class="control-label  pull-left text-primary">*Visit Date</label></div>  
							  <div class="col-md-12">
									<div class="datepicker" id="DateOfVisit">
										<div class="input-group">
											<asp:TextBox ID="VisitDate" runat="server" class="form-control input-sm" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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

						<div class="col-md-4">
							<div class="col-md-12 form-group">
								<div class="col-md-12">
									<label class="control-label  pull-left text-primary">*Visit Scheduled?</label>
								</div>
								<div class="col-md-12">

									<label class="pull-left" style="padding-right: 10px">
										<input id="vsYes" type="radio" name="Scheduled" value="1" clientidmode="Static" runat="server" />Yes
									</label>
									<label class="pull-left" style="padding-right: 10px">
										<input id="vsNo" type="radio" name="Scheduled" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
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
									<asp:DropDownList runat="server" ID="ddlVisitBy" ClientIDMode="Static" CssClass="form-control" data-parsley-min="1" data-parsley-min-message="Value Required" onchange="showHideVisitByTS();" />
								</div>

							</div>
						</div>

					</div>

					<div class="col-md-12 form-group">
						<div class="col-md-12">
							<div class="panel panel-info">
								<div class="panel-body">
									<div class="col-md-12 form-group">
										<label class="control-label pull-left">Nutrition Assessment</label>
									</div>

									<div class="col-md-12 form-group">
										<div class="col-md-3">
											<div class="col-md-12">
												<label class="control-label pull-left input-sm">Height (cm)</label>
											</div>
											<div class="col-md-12">
												<asp:TextBox ID="txtHeight" CssClass="form-control input-sm" ClientIDMode="Static" Enabled="false" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-3">
											<div class="col-md-12">
												<label class="control-label pull-left input-sm">Weight (Kg)</label>
											</div>
											<div class="col-md-12">
												<asp:TextBox ID="txtWeight" CssClass="form-control input-sm" ClientIDMode="Static" Enabled="false" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-3">
											<div class="col-md-12">
												<label runat="server" ID="lblBMI" class="control-label pull-left input-sm">BMI</label>
                                                <label runat="server" ID="lblBMIz" class="control-label pull-left input-sm">BMIz</label>
											</div>
											<div class="col-md-12">
												<asp:TextBox ID="txtBMI" CssClass="form-control input-sm" ClientIDMode="Static" Enabled="false" runat="server"></asp:TextBox>
												<asp:TextBox ID="txtBMIZ" runat="server" CssClass="form-control input-sm" Enabled="false" ClientIDMode="Static"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-3">
											<div class="col-md-12">
												<label class="control-label pull-left input-sm text-primary" for="nutritionscreeningstatus">*Nutrition Status</label>
											</div>
											<div class="col-md-12">
												<asp:DropDownList runat="server" CssClass="form-control" ID="nutritionscreeningstatus" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />

											</div>
										</div>
									</div>
                                    <div class="nutritionscreeningsection" id="nutritionscreeningsection">
                                        <asp:PlaceHolder ID="PHNutritionScreeningNotes" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <%--<div class="col-md-12">
                                        <label class="control-label pull-left">Nutrition Assessment</label>
                                        <div class="">
                                            <textarea id="nutritionAssesmentNotes" class="form-control input-sm" placeholder="Notes..." rows="3"></textarea>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div>
											<label class="control-label  pull-left text-primary">*Any Presenting Complaints</label>
										</div>

										<div>
											<label class="pull-left" style="padding-right: 10px">
												<input id="Radio1" type="radio" name="anyComplaints" value="1" clientidmode="Static" runat="server" onclick="showHidePresentingComplaintsDivs();" />Yes
											</label>
											<label class="pull-left" style="padding-right: 10px">
												<input id="Radio2" type="radio" name="anyComplaints" value="0" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showHidePresentingComplaintsDivs();" />No
											</label>

										</div>
                                    </div>--%>
								</div>
							</div>
						</div>
					</div>

					<div class="col-md 12 form-group">
						<div class="col-md-4"></div>
						<div class="col-md-4"></div>
						<div class="col-md-4">
							<div id="divTreatmentSupporter" class="col-md-12">
								<button type="button" class="btn btn-info btn-lg fa" id="btnTreatmentSupporterVisit" onclick="savePatientEncounterTS();">Complete Encounter</button>
							</div>
						</div>
					</div>


					<div class="col-md-12">
						<hr />
					</div>
					<%--to here--%>
					<div class="col-md-12">
					<div id="step1Div">

						<div class="col-md-12 form-group" <%-- style="height:100%"--%>>
							<div class="col-md-12" <%--style="height:100%"--%>>
								<div class="panel panel-info" <%-- style="height:100%"--%>>

									<div class="panel-body">
										
										<div class="col-md-12 form-group">
											<div>
												<label class="control-label  pull-left text-primary">*Any Presenting Complaints</label>
											</div>

											<div>
												<label class="pull-left" style="padding-right: 10px">
													<input id="rdAnyComplaintsYes" type="radio" name="anyComplaints" value="1" clientidmode="Static" runat="server"   data-parsley-required="true" onclick="showHidePresentingComplaintsDivs();" />Yes
												</label>
												<label class="pull-left" style="padding-right: 10px">
													<input id="rdAnyComplaintsNo" type="radio" name="anyComplaints" value="0" clientidmode="Static" runat="server"   data-parsley-required="true"                                                          onclick="showHidePresentingComplaintsDivs();" />No
												</label>

											</div>
										</div>

										<div id="presentingComplaintsCtrls" class="col-md-12 form-group">
											<div class="col-md-5">
												<div class="col-md-12">
													<label class="control-label pull-left">Presenting Complaints</label>
												</div>
												<div class="col-md-12">
													<asp:TextBox ID="txtPresentingComplaintsID" runat="server" ClientIDMode="Static"></asp:TextBox>
													<asp:TextBox runat="server" CssClass="form-control input-sm" ID="txtPresentingComplaints" ClientIDMode="Static" placeholder="Presenting Complaints.."></asp:TextBox>
												</div>
											</div>
											<div class="col-md-5">
												<div class="col-md-12">
													<label class="control-label  pull-left">Number of days</label>
												</div>
												<div class="col-md-12">
													<asp:TextBox runat="server" CssClass="form-control input-sm" ID="numberOfDays" ClientIDMode="Static" Type="Number"></asp:TextBox>
												</div>
											</div>
											<div class="col-md-2">
												<div class="col-md-12">
													<label class="control-label pull-left"></label>
												</div>
												<div class="col-md-12">
													<button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddPresentingComplaints" onclick="AddPresentingComplaints()">Add</button>
												</div>
											</div>
										</div>

										<div class="col-md-12 form-group">
											<div id="presentingComplaintsTable" class="panel panel-primary">
												<div class="panel-heading">Presenting Complaints</div>
												<div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
													<table id="dtlPresentingComplaints" class="table table-bordered table-striped" style="width: 100%">
														<thead>
															<tr>
																<th><span class="text-primary">Presenting ComplaintsID</span></th>
																<th><span class="text-primary">Presenting Complaints</span></th>
																<th><span class="text-primary">Date of Onset</span></th>
																<th><span class="text-primary"></span></th>
															</tr>
														</thead>
														<tbody></tbody>
													</table>
												</div>
											</div>
										 </div>
										<div class="col-md-12">
											<div id="presentingComplaintsNotes">
												<label class="control-label pull-left text-primary" for="complaints">Presenting Complaints Notes :</label>
												<textarea runat="server" clientidmode="Static" id="complaints" class="form-control input-sm" placeholder="complaints...." rows="3"></textarea>
											</div>
										</div>
									
									
									</div><%--.panel-body--%>

								</div><%--.panel--%>
								
							</div>
						</div>
                        <div class="icfiptwrap">
                            <div class="clearfix"></div>
                                <div class="icfwrap">
                                    <%--ICF - Intensified Case Finding--%>
                                    <div class="col-md-12 form-group" id="ICFPanel">
	                                    <div class="col-md-12">
		                                    <div class="panel panel-info">
			                                    <div class="panel-body">
				                                    <div class="row">
					                                    <div class="col-md-12 form-group">
                                                            <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">TB Intensified Case Finding</label>
					                                    </div>
				                                    </div>
                    
				                                    <div class="row">
					                                    <div class="col-md-6  form-group">
						                                    <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlOnAntiTBDrugs">Currently on Anti-TB Drugs?</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlOnAntiTBDrugs" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
						                                    </div>
					                                    </div>
				                                    </div>
                                                    <div class="row" id="ICFScreeningSection">
                                                        <div class="col-md-3  form-group">
						                                    <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFCough">Cough</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFCough" ClientIDMode="Static" />
						                                    </div>
					                                    </div>
                                                        <div class="col-md-3  form-group">
						                                    <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFFever">Fever</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFFever" ClientIDMode="Static" />
						                                    </div>
					                                    </div>
                                                        <div class="col-md-3  form-group">
						                                    <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFWeight">Noticeable Weight Loss</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFWeight" ClientIDMode="Static" />
						                                    </div>
					                                    </div>
                                                        <div class="col-md-3  form-group">
						                                    <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFNightSweats">Night Sweats</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFNightSweats" ClientIDMode="Static" />
						                                    </div>
					                                    </div>
                                                    </div>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <%--//Tuberclosis Treatment--%>
                                    <%--ICF Action Taken--%>
                                    <div class="col-md-12 form-group" id="ICFActionTakenPanel">
	                                    <div class="col-md-12">
		                                    <div class="panel panel-info">
			                                    <div class="panel-body">
                                                    <div class="row">
					                                    <div class="col-md-12 form-group">
                                                            <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">ICF Action Taken</label>
					                                    </div>
				                                    </div>
                                                    <div class="row" id="ICFActionScreeningSection">
                                                        <div class="col-md-4">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFRegimen">Sputum Smear</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlSputumSmear" ClientIDMode="Static" />
						                                    </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFRegimen">Gene Xpert</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlGeneXpert" ClientIDMode="Static"/>
						                                    </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFRegimen">Chest X-Ray</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlChestXray" ClientIDMode="Static"  />
						                                    </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFRegimen">Start Anti-TB</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlStartAntiTB" ClientIDMode="Static" />
						                                    </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFRegimen">Invitation of Contacts</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlInvitationofContacts" ClientIDMode="Static" />
						                                    </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFRegimen">Evaluated for IPT</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlEvaluatedforIPT" ClientIDMode="Static" />
						                                    </div>
                                                        </div>
                                                    </div>
                                                </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <%--//ICF Action Taken--%>
                                    <%--Tuberclosis Treatment--%>
                                    <div class="col-md-12 form-group" id="TuberclosisTreatmentPanel">
	                                    <div class="col-md-12">
		                                    <div class="panel panel-info">
			                                    <div class="panel-body">
                                                    <div class="row" id="TBRXSection">
                                                        <div class="col-md-12 form-group">
                                                            <label class="control-label pull-left input-sm text-primary">Tuberclosis Treatment</label>
					                                    </div>
                                                        <div class="col-md-12 form-group">
                                                            <div class="col-md-4  form-group">
						                                        <div class="col-md-12">
							                                        <label class="control-label pull-left input-sm" for="tbTBRXStartDate">TB Rx Start Date</label>
						                                        </div>
						                                        <div class="col-md-12">
							                                        <div class='input-group date icfdate'>
						                                                <span class="input-group-addon">
							                                                <span class="glyphicon glyphicon-calendar"></span>
						                                                </span>
						                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="tbTBRXStartDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					                                                </div>
						                                        </div>
					                                        </div>
                                                            <div class="col-md-4  form-group">
						                                        <div class="col-md-12">
							                                        <label class="control-label pull-left input-sm" for="tbTBRXEndDate">TB Rx End Date</label>
						                                        </div>
						                                        <div class="col-md-12">
							                                        <div class='input-group date icfdate'>
						                                                <span class="input-group-addon">
							                                                <span class="glyphicon glyphicon-calendar"></span>
						                                                </span>
						                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="tbTBRXEndDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					                                                </div>
						                                        </div>
					                                        </div>
                                                            <div class="col-md-4  form-group">
						                                        <div class="col-md-12">
							                                        <label class="control-label pull-left input-sm" for="ddlICFRegimen">Regimen</label>
						                                        </div>
						                                        <div class="col-md-12">
							                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFRegimen" ClientIDMode="Static"/>
						                                        </div>
					                                        </div>
                                                        </div>
                                                    </div>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <%--//Tuberclosis Treatment--%>
                                    <%--Tuberclosis Screening Outcome --%>
                                    <div class="col-md-12 form-group" id="tbScreeningOutcomePanel">
	                                    <div class="col-md-12">
		                                    <div class="panel panel-info">
			                                    <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12 form-group">
                                                            <label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">Tuberclosis Screening Outcome</label>
					                                    </div>
                                                        <div class="col-md-4 text-center center-block">
                                                            <div class="col-md-12">
							                                    <label class="control-label pull-left input-sm" for="ddlICFTBScreeningOutcome">Outcome</label>
						                                    </div>
						                                    <div class="col-md-12">
							                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFTBScreeningOutcome" ClientIDMode="Static" />
						                                    </div>
                                                        </div>
                                                    </div>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <%--//Tuberclosis Screening Outcome--%> 
                                    <%--IPT--%>
                                    <div class="col-md-12 form-group" id="IPTPanel">
	                                    <div class="col-md-12">
		                                    <div class="panel panel-info">
			                                    <div class="panel-body">
                                                <div class="row">
                                                <div class="col-md-12">
                                                <div id="IPTHistoryTable" class="panel panel-primary">
												<div class="panel-heading">IPT History</div>
												<div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
													<table id="dtlIPTHistory" class="table table-bordered table-striped" style="width: 100%">
														<thead>
															<tr>
                                                                <th><span class="text-primary">IPTStartDate</span></th>
																<th><span class="text-primary">IPTOutCome</span></th>
																<th><span class="text-primary">IPTOutcomeDate</span></th>
																
															</tr>
														</thead>
														<tbody></tbody>
													</table>
												</div>
                                                </div>
                                                </div>
                                                </div>
                                                    <div class="row" id="IPTSection">
                                                        <div class="col-md-12 form-group">
                                                            <label class="control-label pull-left input-sm text-primary">IPT</label>
					                                    </div>
                                                        <div class="col-md-12">
                                                            <div class="col-md-6 text-center">
                                                                <div class="col-md-12">
							                                        <label class="control-label pull-left input-sm" for="ddlICFCurrentlyOnIPT">Currrently on IPT?</label>
						                                        </div>
						                                        <div class="col-md-12">
							                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFCurrentlyOnIPT" ClientIDMode="Static" />
						                                        </div>
                                                            </div>
                                                            <div class="col-md-6 text-center" id="startIptSection">
                                                                <div class="col-md-12">
							                                        <label class="control-label pull-left input-sm" for="ddlICFStartIPT">Start IPT?</label>
						                                        </div>
						                                        <div class="col-md-12">
							                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlICFStartIPT" ClientIDMode="Static"/>
						                                        </div>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                        <div style="height: 30px;"></div>
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
							                                    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIptWorkUp2" data-toggle="modal" data-target="#IptClientWorkupModal">IPT Client Workup</button>
						                                    </div>
						                                    <div class="col-md-4">
							                                    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIpt2" data-toggle="modal" data-target="#IptDetailsModal">IPT Follow Up</button>
						                                    </div>
						                                    <div class="col-md-4">
							                                    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIptOutcome2" data-toggle="modal" data-target="#IptOutcomeModal">IPT Outcome</button>
						                                    </div>
                                                        </div>
                                                    </div>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <%--//IPT--%>
                                    <div class="clearfix"></div>
                                </div>
                        </div>
                        <%--<uc:ucICF runat="server" ID="ucICF" />--%>
						<%--<div class="col-md-12 form-group">
							<div class="col-md-12">
								<div class="panel panel-info">
									<div class="panel-body">
										<div class="col-md-12 form-group">
											<label class="control-label pull-left">TB Intensified Case Findings(ICF)</label>
										</div>

										<div class="col-md-12 form-group">
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left  text-primary">Currently on Anti TB drugs?</label>
												</div>
												<div class="col-md-6">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="tbInfected" ClientIDMode="Static" onChange="tbInfectedChange();" required="true" data-parsley-required="true">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
														<asp:ListItem Text="No" Value="False"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left  text-primary">Currently on IPT?</label>
												</div>
												<div class="col-md-6">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="onIpt" ClientIDMode="Static" onChange="onIptChange();" required="false" data-parsley-required="false">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
														<asp:ListItem Text="No" Value="False"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left  text-primary">Ever been on IPT?</label>
												</div>
												<div class="col-md-6">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="EverBeenOnIpt" ClientIDMode="Static" onChange="EverBeenOnIptChange();" required="true" data-parsley-required="true">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
														<asp:ListItem Text="No" Value="False"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-12 form-group" clientidmode="Static" id="IcfForm">

												<div class="col-md-3 form-group">
													<div class="col-md-12">
														<label class="control-label pull-left">Cough</label>
													</div>
													<div class="col-md-12">
														<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="cough" ClientIDMode="Static" onChange="IcfChange();">
														    <asp:ListItem Text="select" Value="select"></asp:ListItem>
														    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
															<asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>

												<div class="col-md-3 form-group">
													<div class="col-md-12">
														<label class="control-label pull-left">Fever</label>
													</div>
													<div class="col-md-12">
														<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="fever" ClientIDMode="Static" onChange="IcfChange();">
														    <asp:ListItem Text="select" Value="select"></asp:ListItem>
														    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
															<asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
												<div class="col-md-3">
													<div class="col-md-12">
														<label class="control-label pull-left">Noticable Weight Loss</label>
													</div>
													<div class="col-md-12">
														<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="weightLoss" ClientIDMode="Static" onChange="IcfChange();">
														    <asp:ListItem Text="select" Value="select"></asp:ListItem>
														    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
															<asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
												<div class="col-md-3">
													<div class="col-md-12">
														<label class="control-label pull-left">Night Sweats</label>
													</div>
													<div class="col-md-12">
														<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="nightSweats" ClientIDMode="Static" onChange="IcfChange();">
														    <asp:ListItem Text="select" Value="select"></asp:ListItem>
														    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
															<asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>--%>

						<%--<div class="col-md-12 form-group" clientidmode="Static" id="IcfActionForm">
							<div class="col-md-12">
								<div class="panel panel-info">
									<div class="panel-body">
										<div class="col-md-12 form-group">
											<label class="control-label pull-left">ICF Action Taken</label>
										</div>

										<div class="col-md-12 form-group">
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left">Sputum Smear</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="sputum" ClientIDMode="Static" onChange="IcfActionChange();">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Ordered" Value="2"></asp:ListItem>
														<asp:ListItem Text="Positive" Value="1"></asp:ListItem>
														<asp:ListItem Text="Negative" Value="0"></asp:ListItem>
														<asp:ListItem Text="Not Done" Value="3"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left">Gene Xpert</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="geneXpert" ClientIDMode="Static" onChange="IcfActionChange();">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Ordered" Value="2"></asp:ListItem>
														<asp:ListItem Text="Positive" Value="1"></asp:ListItem>
														<asp:ListItem Text="Negative" Value="0"></asp:ListItem>
														<asp:ListItem Text="Not Done" Value="3"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left">Chest X-ray</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="chest" ClientIDMode="Static" onChange="IcfActionChange();">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Ordered" Value="2"></asp:ListItem>
														<asp:ListItem Text="Suggestive" Value="1"></asp:ListItem>
														<asp:ListItem Text="Normal" Value="0"></asp:ListItem>
														<asp:ListItem Text="Not Done" Value="3"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left">Start Anti-TB</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="antiTb" ClientIDMode="Static" onChange="IcfActionChange();">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
														<asp:ListItem Text="No" Value="False"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left">Invitation of Contacts</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="contactsInvitation" ClientIDMode="Static" onChange="IcfActionChange();">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
														<asp:ListItem Text="No" Value="False"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
											<div class="col-md-4">
												<div class="col-md-12">
													<label class="control-label pull-left">Evaluated for IPT</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="iptEvaluation" ClientIDMode="Static" onChange="IcfActionChange();">
														<asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
														<asp:ListItem Text="No" Value="False"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>--%>

						<%--<div class="col-md-12 form-group" clientidmode="Static" id="IptForm">
							<div class="col-md-12">
								<div class="panel panel-info">
									<div class="panel-body">
										<div class="col-md-12 form-group">
											<label class="control-label pull-left">Isoniazad Preventive Therapy(IPT)</label>
										</div>

										<div class="col-md-12 form-group">
											<div class="col-md-4">
												<button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIptWorkUp" data-toggle="modal" data-target="#IptClientWorkupModal">IPT Client Workup</button>
											</div>
											<div class="col-md-4">
												<button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIpt" data-toggle="modal" data-target="#IptDetailsModal">IPT Follow Up</button>
											</div>
											<div class="col-md-4">
												<button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddIptOutcome" data-toggle="modal" data-target="#IptOutcomeModal">IPT Outcome</button>
											</div>
										</div>--%>

										<div id="IptClientWorkupModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
											<div class="modal-dialog">
												<div class="modal-content">
													<div class="modal-header bg-info">
														<h4 class="modal-title">Isoniazid Preventive Therapy Client Work Up</h4>
													</div>
													<div class="modal-body">
														<div class="row">
															<uc:IptClientWorkup ID="IptCw" runat="server" />
														</div>
													</div>
													<div class="modal-footer">
														<div class="col-md-12 form-group">
															<div class="col-md-6">
																<button type="button" id="btnSaveIptWorkup" class="btn btn-default" onclientclick="return false;">Save</button>
															</div>
															<div class="col-md-6">
																<button type="button" id="btnCancelIptWorkup" class="btn btn-default" data-dismiss="modal">Cancel</button>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>

										<div id="IptDetailsModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
											<div class="modal-dialog">
												<div class="modal-content">
													<div class="modal-header bg-info">
														<h4 class="modal-title">Isoniazid Preventive Therapy(IPT)</h4>
													</div>
													<div class="modal-body">
														<div class="row">
															<uc:Ipt ID="IptDetails" runat="server" />
														</div>
													</div>
													<div class="modal-footer">
														<div class="col-md-12 form-group">
															<div class="col-md-6">
																<button type="button" id="btnSaveIptDetails" class="btn btn-default" onclientclick="return false;">Save</button>
															</div>
															<div class="col-md-6">
																<button type="button" id="btnCancelIptDetails" class="btn btn-default" data-dismiss="modal">Cancel</button>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>

										<div id="IptOutcomeModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
											<div class="modal-dialog">
												<div class="modal-content">
													<div class="modal-header bg-info">
														<h4 class="modal-title">Isoniazid Preventive Therapy Outcome</h4>
													</div>
													<div class="modal-body">
														<div class="row">
															<uc:IptOutcome ID="IptOutcomeForm" runat="server" />
														</div>
													</div>
													<div class="modal-footer">
														<div class="col-md-12 form-group">
															<div class="col-md-6">
																<button type="button" id="btnSaveIptOutcome" class="btn btn-default" onclientclick="return false;">Save</button>
															</div>
															<div class="col-md-6">
																<button type="button" id="btnCancelIptOutcome" class="btn btn-default" data-dismiss="modal">Cancel</button>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>

									<%--</div>
								</div>
							</div>
						</div>--%>

						<%--<div class="col-md-12 form-group">
							<div class="col-md-12">
								<div class="panel panel-info">
									<div class="panel-body">
										<div class="col-md-12">
											<div class="col-md-4">
												<div class="col-md-12 form-group">
												</div>
											</div>
										</div>

										<div class="col-md-12">
											<div class="col-md-6  form-group">
												<div class="col-md-6">
													<label class="control-label pull-left input-sm text-primary" for="tbscreeningstatus">*TB Screening</label>
												</div>
												<div class="col-md-6">
													<asp:DropDownList runat="server" CssClass="form-control input-sm" ID="tbscreeningstatus" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>--%>

						<div class="col-md-12 form-group">

							<div class="col-md-12">
								<div class="panel panel-info">

									<div class="panel-body">
										<div class="col-md-12 form-group">
											<div>
												<label class="control-label pull-left text-primary">*Any Adverse Event(s)</label>
											</div>

											<div>
												<label class="pull-left" style="padding-right: 10px">
													<input id="rdAnyAdverseEventsYes" type="radio" name="adverseEvents" value="1" clientidmode="Static" runat="server" onclick="showHideAdverseEventsDivs();" />Yes
												</label>
												<label class="pull-left" style="padding-right: 10px">
													<input id="rdAnyAdverseEventsNo" type="radio" name="adverseEvents" value="0" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showHideAdverseEventsDivs();" />No
												</label>

											</div>

										</div>

										<div id="adverseEventCtrls" class="col-md-12 form-group">
											<div class="col-md-3">
												<div class="col-md-12">
													<label class="control-label pull-left">Adverse event</label>
												</div>
												<div class="col-md-12">
													<asp:TextBox ID="adverseEventId" runat="server" ClientIDMode="Static"></asp:TextBox>
													<asp:TextBox runat="server" CssClass="form-control input-sm" ID="adverseEvent" ClientIDMode="Static" placeholder="adverse event.."></asp:TextBox>
												</div>
											</div>
											<div class="col-md-3">
												<div class="col-md-12">
													<label class="control-label pull-left">Severity</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ddlAdverseEventSeverity" ClientIDMode="Static" />
												</div>
											</div>
											<div class="col-md-3">
												<div class="col-md-12">
													<label class="control-label">Medicine Causing A/E</label>
												</div>
												<div class="col-md-12">
													<asp:TextBox runat="server" CssClass="form-control input-sm" ID="AdverseEventCause" ClientIDMode="Static" placeholder="cause..."></asp:TextBox>
												</div>
											</div>
											
											<div class="col-md-2">
												<div class="col-md-12">
													<label class="control-label pull-left">Action</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" ID="AdverseEventAction" CssClass="form-control input-sm" ClientIDMode="Static" />

												</div>
											</div>
											<div class="col-md-1">
												<div class="col-md-12">
													<label class="control-label pull-left"></label>
												</div>
												<div class="col-md-12">
													<button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddMilestones" onclick="AddAdverseReaction();">Add</button>
												</div>
											</div>
										
											<div class="col-md-12 form-group" id="divAdverseEventOther"  clientidmode="Static" style="margin-top: 1%">
												<div class="col-md-12"><label class="control-label pull-left text-primary"> Other Adverse Event(s)</label></div>
												<div class="col-md-12">
													
													<asp:TextBox runat="server" ID="txtAdverseEventOther" CssClass="form-control" ClientIDMode="Static" placeholder="Provide Other Adverse Event(s) " ></asp:TextBox>
												</div>
											</div>
									  </div>
										
										
									
										<div class="col-md-12">
											<div id="adverseEventsTable" class="panel panel-primary">
												<div class="panel-heading">Adverse Events</div>
												<div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
													<table id="dtlAdverseEvents" class="table table-bordered table-striped" style="width: 100%">
														<thead>
														<tr>
															<th><span class="text-primary">SeverityID</span></th>
															<th><span class="text-primary">AdverseEventId</span></th>
															<th><span class="text-primary">Adverse Events</span></th>
															<th><span class="text-primary">Medicine Causing A/E</span></th>
															<th><span class="text-primary">Severity</span></th>
															<th><span class="text-primary">Action</span></th>
															<th><span class="text-primary">Adverse Event Outcome</span></th>
														</tr>
														</thead>
														<tbody id="dtlAdverseEventsBdy"></tbody>
													</table>

												</div>
											</div>
										</div> 
								   
									 </div><%--.panel-body--%>
								</div> <%--.panel--%>
							   
							</div><%--col-md-12--%>

							
						</div>
					</div>
						</div>
				</div>
				<%-- .data-step-1--%>
				<div class="step-pane sample-pane" id="datastep2" data-step="2">
					<%--<div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Chronic Illness </strong></small></div> <div class="col-md-12"><hr /> </div>--%>
					<div class="col-md-12">

						<div class="col-md-12">
                            <div class="neonatal-history-wrap">
                                <asp:PlaceHolder ID="NeonatalHistoryPH" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="tanners-staging-warp">
                                <asp:PlaceHolder ID="TannersStagingPH" runat="server"></asp:PlaceHolder>
                            </div>
							<%--<div class="col-md-12"><hr /></div>--%>
							<div class="panel panel-info">
								<div class="panel-body">
									<div class="col-md-12 form-group">
										<label class="control-label pull-left">Chronic Illnesses</label>
									</div>

									<div class="col-md-12 form-group">
										<div class="col-md-3 form-group">
											<div class="col-md-12">
												<label for="ChronicIllnessName" class="control-label pull-left">Illness</label>
											</div>
											<div class="col-md-12">
												<asp:DropDownList runat="server" ID="ChronicIllnessName" CssClass="form-control input-sm" ClientIDMode="Static" />
											</div>
										</div>
										
										<div class="col-md-3 form-group">
											<div class="col-md-12">
												<label class="control-label pull-left">Onset Date</label>
											</div>
											<div class="col-md-12">
												<div class='input-group date' id='ChronicIllnessOnsetDate'>
													<span class="input-group-addon">
														<span class="glyphicon glyphicon-calendar"></span>
													</span>
													<asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtOnsetDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
												</div>
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

										<!--<div class="col-md-2 form-group">
											<div class="col-md-12">
												<label class="control-label pull-left">Dose</label>
											</div>
											<div class="col-md-12">
												<asp:TextBox runat="server" ID="treatmentDose" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="dose.." data-parsley-min="1"></asp:TextBox>
											</div>
										</div>-->
										

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
															<!--<th><span class="text-primary">Dose</span></th>-->
															<th><span class="text-primary">Onset Date</span></th>
															<th><span class="text-primary">Active</span></th>
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
										<div class="col-md-3 form-group">
											<div class="col-md-12">
												<label for="AllergyName" class="control-label pull-left">Substance Causing Allergy</label>
											</div>
											<div class="col-md-12">
												<asp:TextBox ID="txtAllergyId" Enabled="false" runat="server" ClientIDMode="Static"></asp:TextBox>
												<asp:TextBox ID="txtAllergy" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="allergy.."></asp:TextBox>
											</div>
										</div>

										<div class="col-md-3 form-group">
											<div class="col-md-12">
												<label class="control-label pull-left">Type of reaction</label>
											</div>
											<div class="col-md-12">
												<asp:TextBox ID="txtReactionTypeID" Enabled="false" runat="server" ClientIDMode="Static"></asp:TextBox>
												<asp:TextBox runat="server" ID="txtReactionType" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="reaction.."></asp:TextBox>
											</div>
										</div>

										<div class="col-md-2 form-group">
											<div class="col-md-12">
												<label class="control-label pull-left">Severity</label>
											</div>
											<div class="col-md-12">
												<asp:DropDownList ID="ddlAllergySeverity" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
											</div>
										</div>

										<div class="col-md-3">
											<div class="col-md-12">
												<label class="control-label  pull-left">Onset Date</label>
											</div>
											<div class="col-md-12">
												<div class="datepicker fuelux" id="AllergyDate">
													<div class="input-group">
														<input class="form-control input-sm" id="txtAllergyDate" type="text" runat="server" clientidmode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
												<label  class="control-label pull-left"><span class="fa fa-cog">Action</span></label>
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
															<th><span class="text-primary">AllergyID</span></th>
															<th><span class="text-primary">ReactionID</span></th>
															<th><span class="text-primary">severityID</span></th>
															<th><span class="text-primary">Allergy</span></th>
															<th><span class="text-primary">Reaction Type</span></th>
															<th><span class="text-primary">Severity</span></th>
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
                            <div id="divPreviousSexualHistory" class="panel panel-info">
                                 <div class="panel-body"> 
                                                 <div class="col-md-12 form-group">
								
                                <label class="control-label pull-left">Previous Sexual History for Last Visit</label>
							     
                               
                                </div>
                                <div class="col-md-12" >
                                    <div class="col-md-4"><label class="control-label  pull-left text-primary">*Visit Date</label></div>  
												 
                                   <div class="col-md-4"><input class="form-control input-sm" id="txtSexualHistoryVisitDate" type="text" 
                                                   runat="server" clientidmode="Static"  min="0"  disabled />
                                    </div>
							     </div>
                                <div class="col-md-12 " id="PreviousSexualOutcome">
                                    <label class="control-label pull-left">No History</label>
                                </div>
                                <div class="col-md-12 " id="PreviousSexualHistory">
                                     <div class="mt-2 col-md-12 form-group " style="margin-top:20px;">
										<div class="panel panel-primary">
											<div class="panel-heading">Sexual History Summary</div>
												<div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
												<table id="dtlPreviousHistory" class="table table-condensed table-bordered auto" width="100%">
													
                                                   
												</table>

											</div>
										</div>
									</div>

                                 </div>
                                </div>
                            </div>
                            <div id="divSexualHistory" class="panel panel-info">
                            <div class="panel-body"> 
        
                               <div class="col-md-12 form-group">
								
                                <label class="control-label pull-left">Sexual History</label>
							     
                               
                                </div>
                                
                                
                                 
                                 <div class="col-md-12 ">
                                     <div class="col-md-12">
                                     <div class="col-md-8">
                                        <label for="input-type" class="custom-control-label pull-left">Sexually Active in the past 6 months?</label>
                                    </div>
                                    <div class="col-md-4">
                                         <label class="pull-left" style="padding-right: 10px">
                                            <input type="radio" class="custom-control-input" name="optSexualHistory" value="No" id="radiono" >No
                                        </label>
                    
                                        <label class="pull-left" style="padding-right: 10px">
                                        <input type="radio" class="custom-control-input" name="optSexualHistory"  value="Yes" id="radioyes">Yes
                                        </label>
                                    </div>
                                   </div>
                                     <div class="col-md-4 sexhistory">
											<div class="col-md-12">
												<label class="control-label pull-left">Sexual Orientation</label>
											</div>
											<div class="col-md-12">
													<asp:DropDownList ID="ddlSexualOrientation" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
											</div>
									 </div>
                                       <div class=" col-md-12 sexhistory" >
                                         <div class="col-md-12">
                                          <label class="control-label pull-left">High Risk Behaviour</label>
									     </div>
										<div class="col-md-12">
                                               
                                                <select class="form-control select2" multiple="multiple"  data-placeholder="Select" id="ddlHighRiskBehaviour" style="width: 100%;">
                                                </select>

                                        

                                              
										</div>
                                     </div>

                                      
                                  <div class="col-md-4 sexhistory" >
											<div class="col-md-12">
												<label class="control-label pull-left">
                                                  # Partners in the past 6 Months </label>
                          
											</div>
											<div class="col-md-12">
												 <input class="form-control input-sm" id="txtPartners" type="number" 
                                                   runat="server" clientidmode="Static"  min="0"  disabled />
											</div>
									</div>
                                     <div class="col-md-4  sexhistory" >
											<div class="col-md-12">
												<label class="control-label pull-left">Partner's Status</label>
											</div>
											<div class="col-md-12">
												<asp:DropDownList ID="ddlPartnerStatus" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
											</div>
									</div>
                                     <div class="col-md-4 sexhistory" >
											<div class="col-md-12">
												<label class="control-label pull-left">Partner's Gender </label>
											</div>
											<div class="col-md-12">
												<asp:DropDownList ID="ddlPartnerGender" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
											</div>
                                   </div>
                                  <div class="col-md-12 sexhistory">
                                      
                                      <br />
                                  </div>
								 
                                       <div class="col-md-12 sexhistory center-block">
												<button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddSexualHist" onclick="addSexualHistory()">Add</button>
										</div>
                                 
                                     
                                     <div class="mt-2 col-md-12 form-group sexhistory" style="margin-top:20px;">
										<div class="panel panel-primary">
											<div class="panel-heading">SexualHistory</div>
											<div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
												<table id="dtlSexualHistory" class="table table-bordered table-striped" width="100%">
													<thead>
														<tr>
                                                            <th >#</th>
															<th><span class="text-primary">Partner Status</span></th>
															<th><span class="text-primary">Partner Gender</span></th>
															<th><span class="text-primary">Sexual Orientation</span></th>
															<th><span class="text-primary">High Risk Behaviour</span></th>
														    
                                                            <th>Action</th>
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
                            </div>
							<div id="divAntigenToday" class="panel panel-info">

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
														<input class="form-control input-sm" id="txtVaccinationDate" type="text" runat="server" clientidmode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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
                            <div class="SocialHistoryPH">
                                <asp:PlaceHolder ID="SocialHistoryPH" runat="server"></asp:PlaceHolder>
                            </div>
							<%--.panel--%>
						</div>

					</div>


				</div>
				<%-- .data-step-2--%>

				<div class="step-pane sample-pane" id="datastep3"  data-parsley-validate="true" data-show-errors="true" data-step="3">
					<div class="col-md-12"><small class="muted pull-left"><strong>PATIENT Examination</strong></small></div>
					<div class="col-md-12">
						<hr />
					</div>
					<div class="col-md-12">

						<div class="col-md-12">
							<div class="panel panel-primary">
								<div class="panel-heading">General Examination</div>
								<div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden; text-align: left; padding-left: 10px">
									<asp:CheckBoxList ID="cblGeneralExamination" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Width="100%" ClientIDMode="Static" onChange="cblGeneralExaminationChange(); return false;"></asp:CheckBoxList>
								</div>
							</div>


							<div class="col-md-12">
								<hr />
							</div>

							<div class="panel panel-info">
								<div class="col-md-12" >
									<small class="muted pull-left"><strong>Review of Systems</strong></small>
								</div>   
								<div class="panel-body" >
									<div class="col-md-6 form-group">
										<div>
											<label class="control-label  pull-left text-primary">*Are all systems okay</label>
										</div>

										<div>
											<label class="pull-left" style="padding-right: 10px">
												<input id="systemsOkYes" type="radio" name="systemsOkay" value="1" clientidmode="Static" runat="server" onclick="showHideSystemsOkayDivs();" />Yes
											</label>
											<label class="pull-left" style="padding-right: 10px">
												<input id="systemsOkNo" type="radio" name="systemsOkay" value="0" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showHideSystemsOkayDivs();" />No
											</label>

										</div>
										<div class="errorBlock" style="color: red;"> Please select one option </div>
									</div>
									<div class="col-md-6"></div>
									<div class="col-md-12 form-group" id="systemsOkayCtrls" clientidmode="Static">
									<div class="col-md-12 form-group">
										<div class="col-md-3 form-group">
											<div class="col-md-12">
												<label for="ChronicIllnessName" class="control-label pull-left">Systems</label>
											</div>
											<div class="col-md-12">
												<asp:HiddenField ID="hfExaminationReviewSystems" runat="server" ClientIDMode="Static" />
												<asp:DropDownList runat="server" ID="ddlExaminationType" CssClass="form-control input-sm" ClientIDMode="Static" onchange="loadSystemReviews();" required="true" data-parsley-min="1" data-parsley-min-message="Please select System" />
											</div>
										</div>

										<div class="col-md-3 form-group">
											<div class="col-md-12">
												<label class="control-label pull-left">Findings</label>
											</div>
											<div class="col-md-12">
												<asp:DropDownList runat="server" ID="ddlExamination" CssClass="form-control input-sm" ClientIDMode="Static" />
											</div>
										</div>

										<div class="col-md-5 form-group">
											<div class="col-md-12">
												<label class="control-label pull-left">Findings Notes..</label>
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
															<th><span class="text-primary">ReviewOfSystemsId</span></th>
															<th><span class="text-primary">SystemsTypeID</span></th>
															<th><span class="text-primary">FindingID</span></th>
															<th><span class="text-primary">Review of systems</span></th>
															<th><span class="text-primary">Findings</span></th>
															<th><span class="text-primary">Findings Notes</span></th>
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
							
							<div class="panel panel-primary">
                                <div class="panel-heading">WHO Staging</div>
								<div class="panel-body">
									<div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                         <div class="nav-tabs-custom">
                                          <ul class="nav nav-tabs">
                                            <li class="active"><a href="#Wtab_S1" data-toggle="tab">Stage 1</a></li>
                                            <li><a href="#Wtab_S2" data-toggle="tab">Stage 2</a></li>
                                            <li><a href="#Wtab_S3" data-toggle="tab">Stage 3</a></li>
                                            <li><a href="#Wtab_S4" data-toggle="tab">Stage 4</a></li>
                                         </ul>
                                        <div class="tab-content">
                                         <div class="tab-pane active" id="Wtab_S1">
                                             <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                            <table id="dtlStageI" class="table table-bordered table-striped">
                                              <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage I Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                   
                                                </tr>
                                            </thead>
                                            </table>
                                        </div>
                                         </div>
                                         <div class="tab-pane " id="Wtab_S2">
                                             <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                            <table id="dtlStageII" class="table table-bordered table-striped">
                                              <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage II Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                
                                                </tr>
                                            </thead>
                                            </table>
                                         </div>
                                          </div>
                                         <div class="tab-pane " id="Wtab_S3">
                                             <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                            <table id="dtlStageIII" class="table table-bordered table-striped">
                                              <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage III Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                    
                                                </tr>
                                            </thead>
                                            </table>
                                              </div>
                                         </div>
                                         <div class="tab-pane" id="Wtab_S4">
                                             <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
                                            <table id="dtlStageIV" class="table table-bordered table-striped" >
                                              <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage IV Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                   
                                                </tr>
                                            </thead>
                                            </table>
                                         </div>
                                         </div>
                                        </div>
                                         </div>
                                        </div>

                                          <div class="col-md-12">
                                            <br />
                                        </div>
                                      
										<div class="col-md-4">
											<label class="control-label  pull-left text-primary">*WHO Stage</label>
										</div>
                                      
										<div class="col-md-4">
											<asp:DropDownList ID="WHOStage" CssClass="form-control input-sm" runat="server" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1"  Enabled="false"></asp:DropDownList>
										</div>
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


					<div class="col-md-12">
						
					<div class="col-md-12">
						<div class="col-md-2">
							<button type="button" class="btn btn-info btn-sm pull-left" data-toggle="modal" data-target="#adherenceAssessmentModal">Adherence Assessment</button>
						</div>
						<div class="col-md-5">
							<div class="col-md-12 form-group">
								<div class="col-md-6">
									<label class="control-label pull-left">ARV Adherence</label>
								</div>
								<div class="col-md-6">
									<asp:DropDownList runat="server" ID="arvAdherance" CssClass="form-control input-sm" ClientIDMode="Static" Enabled="False" />
								</div>
							</div>


						</div>
						<div class="col-md-5">
							<div class="col-md-12 form-group">
								<div class="col-md-6">
									<label class="control-label pull-left">CTX/Dapsone Adherence</label>
								</div>
								<div class="col-md-6">
									<asp:DropDownList runat="server" CssClass="form-control input-sm" ID="ctxAdherance" ClientIDMode="Static" />
								</div>
							</div>
						</div>

					</div>

					<div class="col-md-12">
						<div class="col-md-2">
							<button type="button" id="btnStabilityAsessment" name="btnStabilityAsessment" class="btn btn-info btn-sm pull-left" data-toggle="modal" data-target="#differentiatedModal">Stability Assessment</button>
						</div>

						<div class="col-md-5">
							<div class="col-md-12 form-group">
								<div class="col-md-6">
									<label class="control-label pull-left">Stability Status</label>
								</div>
								<div class="col-md-6">
									<asp:DropDownList runat="server" ID="stabilityStatus" CssClass="form-control input-sm" ClientIDMode="Static" Enabled="False" />
								</div>
							</div>
						</div>
					</div>

					<div class="col-md-12">
						<div class="col-md-2">
							<button type="button" id="btnGbvAsessment" name="btnGbvAsessment" class="btn btn-info btn-sm pull-left" data-toggle="modal" data-target="#gbvAssessmentModal">GBV Assessment</button>
						</div>

						<div class="col-md-5">
							<div class="col-md-12 form-group">
								<div class="col-md-6">
									<label class="control-label pull-left">GBV Assessment done?</label>
								</div>
								<div class="col-md-6">
                                    <label class="control-label pull-left" id="lblGbvAssessmentDone">Yes/No</label>
								</div>
							</div>
						</div>
					</div>

					<div class="col-md-12">
						<hr />
					</div>

						<div class="col-md-12">
							<div class="col-md-12 form-group">
								<label class="control-label pull-left">Work Plan</label>
							</div>
							<div class="form-group col-md-12" style="text-align: left">
								<asp:TextBox ID="txtWorkPlan" CssClass="form-control input-sm" ClientIDMode="Static" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-12">
							<hr />
						</div>

						<div class="col-md-12">
							<div class="col-md-12 form-group">
								<label class="control-label pull-left">PHDP</label>
							</div>
							<div class="col-md-12 form-group">
								<label class="control-label pull-left">Select PHDP services offered from the list below</label>
							</div>
							<div class="form-group col-md-12" style="text-align: left">
								<asp:CheckBoxList ID="cblPHDP" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" Width="100%"></asp:CheckBoxList>
							</div>
						</div>
						<div class="col-md-12">
							<hr />
						</div>

						<div class="col-md-12">
							<div class="col-md-12 form-group">
								<label class="control-label pull-left">Lab Order</label>
							</div>
							<div class="form-group col-md-12" style="text-align: left">
								<button type="button" class="btn btn-info btn-sm pull-left" data-toggle="modal" data-target="#labModal" id="btnOrderLabTests">Order Lab Tests</button>
							</div>
						</div>

						<div class="col-md-12">
							<hr />
						</div>

						<div class="col-md-12">

							<div class="col-md-12 form-group">
								<label class="control-label pull-left">Patient Diagnosis and Treatment</label>
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
									<!--<asp:TextBox ID="txtDiagnosisID" Enabled="false" runat="server" ClientIDMode="Static"    ></asp:TextBox>
									<input type="text" id="Diagnosis" class="form-control input-sm"  placeholder="Type Diagnosis......" runat="server" clientidmode="Static" />-->
                                    <select class="form-control select2" data-placeholder="Select" id="Diagnosis" style="width: 100%;">
                                        <option value="0"></option>
                                    </select>

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
													<th><span class="text-primary">DiagnosisID</span></th>
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


						<!-- Modal -->
						<div id="adherenceAssessmentModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
							<div class="modal-dialog" style="width: 70%">
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
												<div class="col-md-9">
													<label class="control-label pull-left">1. Do you ever forget to take your medicine?</label>
												</div>
												<div class="col-md-3">

													<div class="col-md-6">
														<asp:RadioButton ID="Question1_Yes" runat="server" GroupName="Question1" ClientIDMode="Static" Value="1" />
													</div>

													<div class="col-md-6">
														<asp:RadioButton ID="Question1_No" runat="server" GroupName="Question1" ClientIDMode="Static" Value="0" />
													</div>

													<div class="errorBlock1" style="color: red;">Please select one option </div>
												</div>
											</div>

											<div class="col-md-12 form-group">
												<div class="col-md-9">
													<label class="control-label pull-left">2. Are you careless at times about taking your medicine?</label>
												</div>
												<div class="col-md-3">

													<div class="col-md-6">
														<asp:RadioButton ID="Question2_Yes" runat="server" GroupName="Question2" ClientIDMode="Static" Value="1" />
													</div>

													<div class="col-md-6">
														<asp:RadioButton ID="Question2_No" runat="server" GroupName="Question2" ClientIDMode="Static" Value="0" />
													</div>

													<div class="errorBlock2" style="color: red;">Please select one option </div>
												</div>
											</div>


											<div class="col-md-12 form-group">
												<div class="col-md-9">
													<label class="control-label pull-left">3. Sometimes if you feel worse when you take the medicine, do you stop taking it?</label>
												</div>
												<div class="col-md-3">

													<div class="col-md-6">
														<asp:RadioButton ID="Question3_Yes" runat="server" GroupName="Question3" ClientIDMode="Static" Value="1" />
													</div>

													<div class="col-md-6">
														<asp:RadioButton ID="Question3_No" runat="server" GroupName="Question3" ClientIDMode="Static" Value="0" />
													</div>

													<div class="errorBlock3" style="color: red;">Please select one option </div>
												</div>
											</div>


											<div class="col-md-12 form-group">
												<div class="col-md-9">
													<label class="control-label pull-left">4. When you feel better do you sometimes stop taking your medicine?</label>
												</div>
												<div class="col-md-3">

													<div class="col-md-6">
														<asp:RadioButton ID="Question4_Yes" runat="server" GroupName="Question4" ClientIDMode="Static" Value="1" />
													</div>

													<div class="col-md-6">
														<asp:RadioButton ID="Question4_No" runat="server" GroupName="Question4" ClientIDMode="Static" Value="0" />
													</div>

													<div class="errorBlock4" style="color: red;">Please select one option </div>
												</div>
											</div>
											
											<div class="col-md-12 form-group">
												<div class="col-md-4 pull-left">(MMAS-4) Score</div>
												<div class="col-md-2 pull-left" style="background-color: gray;"><asp:Label ID="adherenceScore" runat="server"></asp:Label></div>
												<div class="col-md-3 pull-left">Adherence Rating:</div>
												<div class="col-md-3 pull-left" style="background-color: gray;"><asp:Label ID="adherenceRating" runat="server"></asp:Label></div>
											</div>
											
											<div id="MMAS8">
												<div class="col-md-12 form-group">
													<div class="col-md-9">
														<label class="control-label pull-left">5. Did you take your medicine yesterday?</label>
													</div>
													<div class="col-md-3">

														<div class="col-md-6">
															<asp:RadioButton ID="Question5_Yes" runat="server" GroupName="Question5" ClientIDMode="Static" Value="1" />
														</div>

														<div class="col-md-6">
															<asp:RadioButton ID="Question5_No" runat="server" GroupName="Question5" ClientIDMode="Static" Value="0" />
														</div>

														<div class="errorBlock5" style="color: red;">Please select one option </div>
													</div>
												</div>
											
												<div class="col-md-12 form-group">
													<div class="col-md-9">
														<label class="control-label pull-left">6. When you feel like your symptoms are under control, do you sometimes stop taking your medicine?</label>
													</div>
													<div class="col-md-3">

														<div class="col-md-6">
															<asp:RadioButton ID="Question6_Yes" runat="server" GroupName="Question6" ClientIDMode="Static" Value="1" />
														</div>

														<div class="col-md-6">
															<asp:RadioButton ID="Question6_No" runat="server" GroupName="Question6" ClientIDMode="Static" Value="0" />
														</div>

														<div class="errorBlock6" style="color: red;">Please select one option </div>
													</div>
												</div>
											
												<div class="col-md-12 form-group">
													<div class="col-md-9">
														<label class="control-label pull-left">7. Taking medication every day is a real inconvenience for some people. Do you ever feel under pressure about sticking to your treatment plan?</label>
													</div>
													<div class="col-md-3">

														<div class="col-md-6">
															<asp:RadioButton ID="Question7_Yes" runat="server" GroupName="Question7" ClientIDMode="Static" Value="1" />
														</div>

														<div class="col-md-6">
															<asp:RadioButton ID="Question7_No" runat="server" GroupName="Question7" ClientIDMode="Static" Value="0" />
														</div>

														<div class="errorBlock7" style="color: red;">Please select one option </div>
													</div>
												</div>
											
												<div class="col-md-12 form-group">
													<div class="col-md-9">
														<label class="control-label pull-left">8. How often do you have difficulty remembering to take all your medications?</label>
													</div>
													<div class="col-md-3">
														<asp:DropDownList ID="Question8" runat="server" ClientIDMode="Static" CssClass="form-control input-sm">
															<asp:ListItem Text="select" Value=""></asp:ListItem>
															<asp:ListItem Text="A. Never/Rarely" Value="0"></asp:ListItem>
															<asp:ListItem Text="B. Once in a while" Value="0.25"></asp:ListItem>
															<asp:ListItem Text="C. Sometimes" Value="0.5"></asp:ListItem>
															<asp:ListItem Text="D. Usually" Value="0.75"></asp:ListItem>
															<asp:ListItem Text="E. All the time" Value="1"></asp:ListItem>
														</asp:DropDownList>
														<div class="errorBlock8" style="color: red;">Please select one option </div>
													</div>
												</div>

												<div class="col-md-12 form-group">
													<div class="col-md-4 pull-left">(MMAS-8) Score</div>
													<div class="col-md-2 pull-left" style="background-color: gray;"><asp:Label ID="mmas8Score" runat="server"></asp:Label></div>
													<div class="col-md-3 pull-left">Score:</div>
													<div class="col-md-3 pull-left" style="background-color: gray;"><asp:Label ID="mmas8Adherence" runat="server"></asp:Label></div>
												</div>

											</div>
										</div>
									</div>
									<div class="modal-footer">
										<div class="col-md-12">
											<button type="button" id="btnAdherenceAssessment" class="btn btn-default" onclientclick="return false;">Save</button>
											<button type="button" id="btnAdherenceAssessmentCancel" class="btn btn-default" data-dismiss="modal">Close</button>
										</div>
									</div>

								</div>

							</div>

						</div>

						<div id="differentiatedModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
							<div class="modal-dialog" style="width: 80%">
								<!-- Modal content-->
								<div class="modal-content">
									<div class="modal-body">
										<div class="row">
											<div id="Categorization" data-parsley-validate="true" data-show-errors="true">
												<div class="col-md-12 col-xs-12 col-sm-12">
													<div class="col-md-12">
														<hr style="margin-top: 1%" class="bg-info" />
													</div>
													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">On their current ART regimen for ≥ 12 months</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="ArtRegimenYes" type="radio" name="ArtRegimenPeriod" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="ArtRegimenNo" type="radio" name="ArtRegimenPeriod" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>

													<div class="col-md-12">
														<hr>
													</div>

													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">No active OIs (including TB) in the previous 6 months</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="OiYes" type="radio" name="ActiveOis" value="true" clientidmode="Static"  runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="OiNo" type="radio" name="ActiveOis" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>

													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">Adherent to scheduled clinic visits for the previous 6 months</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="VisitsAdherantYes" type="radio" name="VisitsAdherant" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="VisitsAdherantNo" type="radio" name="VisitsAdherant" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">Most recent VL < 1,000 copies/ml</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="VlCopiesYes" type="radio" name="VlCopies" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="VlCopiesNo" type="radio" name="VlCopies" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">Has completed 6 months of IPT</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="IptYes" type="radio" name="Ipt" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="IptNo" type="radio" name="Ipt" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">BMI ≥ 18.5</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="BmiYes" type="radio" name="Bmi" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="BmiNo" type="radio" name="Bmi" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">Age ≥ 20 years</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="AgeYes" type="radio" name="Age" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="AgeNo" type="radio" name="Age" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
													<div class="col-md-12">
														<div class="col-md-8">
															<label class="control-lable pull-left">Healthcare team does not have concerns about providing longer follow-up intervals for the patient</label>
														</div>
														<div class="col-md-4">
															<div class="col-md-12">
																<label class="pull-left" style="padding-right: 10px">
																	<input id="HealthcareConcernsYes" type="radio" name="HealthcareConcerns" value="true" clientidmode="Static" runat="server" />Yes
																</label>
																<label class="pull-left" style="padding-right: 10px">
																	<input id="HealthcareConcernsNo" type="radio" name="HealthcareConcerns" value="false" clientidmode="Static" runat="server" data-parsley-required="true" />No
																</label>
															</div>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
													<div class="col-md-12">
														<div class="col-md-12">
															<asp:LinkButton runat="server" ID="btnSaveCategorization" CssClass="btn btn-info" ClientIDMode="Static" OnClientClick="return false;">Update Categorization</asp:LinkButton>
														</div>
													</div>
													<div class="col-md-12">
														<hr>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>

						<div id="pharmacyModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true" style="width: 100%">
							<div class="modal-dialog" style="width: 100%">
								<!-- Modal content-->
								<div class="modal-content" style="width: 100%">
									<div class="modal-body" style="width: 100%">
										<div class="row">
											<uc:ucPharmacyPrescription runat="server" ID="ucPharmacyPrescription" />
										</div>
									</div>
								</div>
							</div>
						</div>

						<div id="labModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true" style="width: 100%">
							<div class="modal-dialog" style="width: 100%">
								<!-- Modal content-->
								<div class="modal-content" style="width: 100%">
									<div class="modal-body" style="width: 100%">
										<div class="row">
											<uc:ucPatientLabs runat="server" ID="ucPatientLabs" />
										</div>
									</div>
								</div>
							</div>
						</div>

                        <div id="gbvAssessmentModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true" style="width: 100%">
                            <div class="modal-dialog" style="width: 100%">
                                <!-- Modal content-->
                                <div class="modal-content" style="width: 100%">
                                    <div class="modal-body" style="width: 100%">
                                        <div class="row">
                                            <uc:ucGenderBasedViolenceAssessment runat="server" ID="ucGenderBasedViolenceAssessment" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
							<hr />
						</div>

						<div class="col-md-12">
							<button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#pharmacyModal" id="prescribeDrugs">Prescibe Drugs</button>
						</div>
						<div class="col-md-12">
							<div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
								<div class="col-md-12 form-group">
									<label class="control-label pull-left">Schedule Appointment</label>
								</div>
								<div class="col-md-12 form-group">
									<div class="col-md-12">
										<div class="col-md-4">
											<div class="form-group">
												<div class="col-md-12">
													<label class="control-label pull-left">Date</label>
												</div>

												<div class="col-md-12 form-group">
													<div class='input-group date' id='PersonAppointmentDateD'>
														<span class="input-group-addon">
															<span class="glyphicon glyphicon-calendar"></span>
														</span>
														<asp:TextBox runat="server"  CssClass="form-control input-sm" ID="AppointmentDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" required ="True" data-parsley-min-message="Input the appointment date"></asp:TextBox>
													</div>
												</div>
											</div>
										</div>
										<div class="col-md-4">
											<div class="col-md-12">
												<label class="control-label pull-left">Service Area</label>
											</div>
											<div class="col-md-12 pull-right">
												<asp:DropDownList runat="server" ID="ServiceArea" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" data-parsley-min-message="Select the service area" />
											</div>
										</div>
										<div class="col-md-4">
											<div class="form-group">
												<div class="col-md-12">
													<label for="reason" class="control-label pull-left">Reason</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" data-parsley-min-message="Select the reason" />
												</div>
											</div>
										</div>
									</div>
									<div class="col-md-12">
										<div class="col-md-4">
											<div class="form-group">
												<div class="col-md-12">
													<label for="reason" class="control-label pull-left">Appointment Type</label>
												</div>
												<div class="col-md-12">
													<asp:DropDownList runat="server" ID="DifferentiatedCare" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" data-parsley-min-message="Select differentiated care" />
												</div>
											</div>
										</div>
										<div class="col-md-4">
											<div class="form-group">
												<div class="col-md-12">
													<label for="description" class="control-label pull-left">Description</label>
												</div>
												<div class="col-md-12">
													<asp:TextBox runat="server" ID="description" CssClass="form-control input-sm" ClientIDMode="Static" />
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
						</div>
						<div class="col-md-12">
							<hr />
						</div>
					</div>
				</div>

				<div id="prevNextButton" class="actions">
					<button type="button" class="btn btn-default btn-prev">
						<span class="glyphicon glyphicon-arrow-left"></span>Prev</button>
					<button type="button" class="btn btn-primary btn-next" data-last="Complete">
						Next
								<span class="glyphicon glyphicon-arrow-right"></span>
					</button>
				</div>
			</div>
		</div>
	</div>

</div>



<div id="AdverseEventOutcomeModal" class="modal fade" data-backdrop="false">

	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Patient Adverse Event Outcome </h4>
			</div>
			<div class="modal-body">
				<div class="row">
					<div class="col-md-12"><h4 id="adverseEventLable" class="text-info"></h4></div>
					<div class="col-md-12"><hr/></div>
					<div class="col-md-12">
						<div class="col-md-6">
							<div class="col-md-12"><label class="control-label  pull-left">Outcome</label></div>
							<div class="col-md-12">
								<select id="EventOutcome" name="EventOutcome" class="form-control" data-parsley-reqired data-parsley-min="1" clientidmode="Static">
									<option value="0">select</option>
								</select>
							</div>
						</div>

						<div class="col-md-6">
							<div class="col-md-12"><label class="control-label  pull-left">Outcome</label></div>
							<div class="col-md-12">
								 <div class="datepicker fuelux" id="outcomeDate">
													<div class="input-group">
														<input class="form-control input-sm" id="AdverseOutomeDate" type="text" runat="server" clientidmode="Static" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
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

<%--                        <div class="col-md-4">
							<div class="col-md-12"><label class="control-label pull-left">Action</label></div>
							<div class="col-md-12">
								<select id="OutcomeAction" class="form-control" data-parsley-required" data-parsley-min="1">
									<option value="0">select</option>
								</select>
							</div>
						</div>--%>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-danger fa fa-times" data-dismiss="modal"> Quit Outcome</button>
				<button type="button" class="btn btn-primary fa fa-plus-circle" id="btnSaveChangesOutcome"> Save changes</button>
			</div>

		</div>

	</div>

</div>



<script type="text/javascript">
	var genderId = <%=genderID%>;
	var gender = "<%=gender%>";
    var Age = "<%=age%>";
	var DateOfEnrollment = "<%=DateOfEnrollment%>";
	var isNoneChecked = false;
    var count;
    var isEditAppointment = "<%=IsEditAppointment%>";
    var isEditAppointmentId="<%=IsEditAppointmentId%>";
    var arrWHoStage = [];
	var PatientId = "<%=PtnId%>";
	var PatientMasterVisitId = "<%=PmVisitId%>";
	var adverseEventName = "";
    var adverseEventId = 0;
    var NextAppointmentDate = "<%=NextAppointmentDate%>";
    var selectedstage = "";
    var OIdata = [];
    var diagnosisListStatus = new Array();
    var diagnosisListStatus = [];
    var DiagnosisList = new Array();
    var DiagnosisList = [];
    var DiseaseList = new Array();
    var DiseaseList = [];
    var PatientOIData = [];
    var arrHighRisk = [];
    var arrSexualHistory = [];
    var arrTextSexualHistory = [];
    var noofpartners = 0;
	document.getElementById('txtPresentingComplaintsID').style.display = 'none';
	document.getElementById('txtAllergyId').style.display = 'none';
	document.getElementById('txtReactionTypeID').style.display = 'none';
	//document.getElementById('txtDiagnosisID').style.display = 'none';
	document.getElementById("<%=txtBMIZ.ClientID%>").style.display = 'none';
   document.getElementById("<%=lblBMIz.ClientID%>").style.display = 'none';
	document.getElementById('adverseEventId').style.display = 'none';


    $(document).ready(function () {
        var encounterExists = "<%=PatientEncounterExists%>";
       

        $('.errorBlock1').hide();
        $('.errorBlock2').hide();
        $('.errorBlock3').hide();
        $('.errorBlock4').hide();

        $('.errorBlock5').hide();
        $('.errorBlock6').hide();
        $('.errorBlock7').hide();
        $('.errorBlock8').hide();
        $('.errorBlock').hide();



        if (($("#cough").val() === 'True') || ($("#fever").val() === 'True') || ($("#weightLoss").val() === 'True') || ($("#nightSweats").val() === 'True')) {
            $("#IcfActionForm").show();
        } else {
            $("#IcfActionForm").hide();
        }
        /*$("#IcfActionForm").hide();*/
        //$("#IptForm").hide();
        //$("#IcfForm").hide();
        //$("#IptClientWorkupForm").hide();
        //$("#IptDetailsForm").hide();
        //$("#IptOutcomeDetailsForm").hide();
        //$("#onIpt").prop("disabled", true);
        $("#MMAS8").hide();
     
        //  $("#EverBeenOnIpt").prop("disabled", true);
        //showHideFPControls();
        loadPresentingComplaints();
        LoadIPTHistory();
        loadAdverseEvents();
        loadAllergies();
        loadAllergyReactions();
        loadDiagnosis();
        showHidePresentingComplaintsDivs();
        showHideAdverseEventsDivs();
        showHideSystemsOkayDivs();
        showHideVisitByTS();
        GetPatientExaminationTypeID();
        loadHighRiskBehavior('HighRisk');
        GetPatientSexualHistory();
        GetPreviousSexualHistory();
        var WhoStage1 = 'WHOStageIConditions';
        var WhoStage2 = 'WHOStageIIConditions';
        var WhoStage3 = 'WHOStageIIIConditions';
        var WhoStage4 = 'WHOStageIVConditions';
        loadWhoStageOIS(WhoStage1, "#dtlStageI", "1");
        loadWhoStageOIS(WhoStage2, "#dtlStageII", "2");
        loadWhoStageOIS(WhoStage3, "#dtlStageIII", "3");
        loadWhoStageOIS(WhoStage4, "#dtlStageIV", "4");
        GetPatientOIS();

       

        $(document).on('change', 'input[type=checkbox].flat-red', function (e) {
            if (this.checked) {
                WhoStageCheckBoxClick(this, false);
            }
            else {
                WhoStageCheckBoxClick(this, true);
            }
        });


        selectedstage = $("#WHOStage option:selected").text();
        // Manage adverse Events
        $("#divAdverseEventOther").hide("fast");
        $("#adverseEvent").focusout(function () {
            if (adverseEventName === 'Other Specify') {
                $("#divAdverseEventOther").show("fast");
            } else {
                $("#divAdverseEventOther").hide("fast");
            }
        });
        


        $("#ddlHighRiskBehaviour").select2();

 

        $("#Diagnosis").select2();
       


        
        //Show the AdverseEventModal Windows
        function loadAdverseEventOutcome() {
            $("#AdverseEventOutcomeModel").modal('show');
        }

        //outcome date
        $('#outcomeDate').datepicker({
            allowPastDates: true,
            restricted: [{ from: tomorrow, to: Infinity }],
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        /** stability assessment 
         * /
         

        //btnStabilityAsessment
	    $.ajax({
	        type: "POST",
            url: "../WebService/PatientEncounterService.asmx/PatientTreatmentDurationInMonths",
	        data: "",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function (response) {
	            
	        },
	        error: function (response) {
	            toastr
	                .error("Error in Fetching Treatment start date " + response.d);
	        }
	    });*/

        $('#differentiatedModal').on('show.bs.modal', function (e) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/DifferentiatedCareParameters",
                //data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var serverData = data.d;
                    var radioArtRegimenYes = document.getElementById("ArtRegimenYes");
                    var radioArtRegimenNo = document.getElementById("ArtRegimenNo");

                    var radioOIYes = document.getElementById("OiYes");
                    var radioOINo = document.getElementById("OiNo");

                    var radioVisitsAdherantYes = document.getElementById("VisitsAdherantYes");
                    var radioVisitsAdherantNo = document.getElementById("VisitsAdherantNo");

                    var radioVlCopiesYes = document.getElementById("VlCopiesYes");
                    var radioVlCopiesNo = document.getElementById("VlCopiesNo");

                    var radioIptYes = document.getElementById("IptYes");
                    var radioIptNo = document.getElementById("IptNo");

                    var radioBmiYes = document.getElementById("BmiYes");
                    var radioBmiNo = document.getElementById("BmiNo");

                    var radioAgeYes = document.getElementById("AgeYes");
                    var radioAgeNo = document.getElementById("AgeNo");

                    var radioHealthcareConcernsYes = document.getElementById("HealthcareConcernsYes");
                    var radioHealthcareConcernsNo = document.getElementById("HealthcareConcernsNo");

                    if (serverData[0][0] == 1)
                        radioArtRegimenYes.checked = true;
                    else
                        radioArtRegimenNo.checked = true;

                    if (serverData[0][1] == 0)
                        radioOIYes.checked = true;
                    else
                        radioOINo.checked = true;

                    if (serverData[0][2] < 1000)
                        radioVlCopiesYes.checked = true;
                    else
                        radioVlCopiesNo.checked = true;

                    if (serverData[0][3] >= 180)
                        radioIptYes.checked = true;
                    else
                        radioIptNo.checked = true;

                    if (serverData[0][4] >= 18.5)
                        radioBmiYes.checked = true;
                    else
                        radioBmiNo.checked = true;

                    if (serverData[0][5] >= 20)
                        radioAgeYes.checked = true;
                    else
                        radioAgeNo.checked = true;


                },
                error: function (response) {
                    toastr
                        .error("Error in Fetching Categorization Parameters Data " + response.d);
                }
            });
        })

        //populate options for ADverEvents;
        var mastrName = 'AdverseEventOutcome';

        $.ajax({
            type: "POST",
            url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
            data: "{'masterName':'" + mastrName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var itemList = JSON.parse(response.d);

                $("#EventOutcome").find('option').remove().end();
                $("#EventOutcome")
                    .append('<option value="0">Select</option>');
                $.each(itemList,
                    function (index, itemList) {
                        $("#EventOutcome")
                            .append('<option value="' +
                            itemList.ItemId +
                            '">' +
                            itemList.ItemName +
                            "(" +
                            itemList.ItemDisplayName +
                            ")" +
                            '</option>');
                    });
            },
            error: function (response) {
                toastr
                    .error("Error in Fetching Adverse Event Outcome List " + response.d);
            }
        });

        //set nutrition status

        if (Age > 15) {
            var txtBmi = $("#<%=txtBMI.ClientID%>").val();
            document.getElementById("<%=txtBMIZ.ClientID%>").style.display = 'none';
             document.getElementById("<%=lblBMIz.ClientID%>").style.display = 'none';
            document.getElementById("<%=lblBMI.ClientID%>").style.display = 'block';
            document.getElementById("<%=txtBMI.ClientID%>").style.display = 'block';
            if (txtBmi > 0 && txtBmi < 16) {
                $("#nutritionscreeningstatus option").filter(function () {
                    return $(this).text() === 'Severe Acute Malnutrition';
                }).prop('selected', true);
            } else if (txtBmi >= 16 && txtBmi < 18.5) {
                $("#nutritionscreeningstatus option").filter(function () {
                    return $(this).text() === 'Moderate Acute Malnutrition';
                }).prop('selected', true);
            } else if (txtBmi >= 18.5 && txtBmi < 25) {
                $("#nutritionscreeningstatus option").filter(function () { return $(this).text() === 'Normal'; })
                    .prop('selected', true);
            } else if (txtBmi >= 25) {
                $("#nutritionscreeningstatus option")
                    .filter(function () { return $(this).text() === 'Overweight/Obese'; }).prop('selected', true);
            }
        } else {
            var txtBMIZ = $("#<%=txtBMIZ.ClientID%>").val();
            document.getElementById("<%=txtBMIZ.ClientID%>").style.display = 'block';
            document.getElementById("<%=txtBMI.ClientID%>").style.display = 'none';
            document.getElementById("<%=lblBMIz.ClientID%>").style.display = 'block';
            document.getElementById("<%=lblBMI.ClientID%>").style.display = 'none';
            console.log(txtBMIZ);
            if ((txtBMIZ == "4 (Overweight)") || (txtBMIZ === "3 (Overweight)") || (txtBMIZ === "2 (Overweight)") || (txtBMIZ === "1 (Overweight)")) {
                $("#nutritionscreeningstatus option").filter(function () { return $(this).text() === 'Overweight/Obese'; }).prop('selected', true);
            } else if ((txtBMIZ === "0 (Normal)")) {
                $("#nutritionscreeningstatus option").filter(function () { return $(this).text() === 'Normal'; }).prop('selected', true);
            } else if ((txtBMIZ === "-1 (Mild)") || (txtBMIZ == "-2 (Moderate)")) {
                $("#nutritionscreeningstatus option").filter(function () { return $(this).text() === 'Moderate Acute Malnutrition'; }).prop('selected', true);
            } else if ((txtBMIZ === "-3 (Severe)") || (txtBMIZ === "-4 (Severe)")) {
                $("#nutritionscreeningstatus option").filter(function () { return $(this).text() === 'Severe Acute Malnutrition'; }).prop('selected', true);
            }
        }

        //set IPT weight
        var weightVal = <%=Weight%>;
        $("#weight").val(weightVal);

        if (Age > 14) {
            document.getElementById('divAntigenToday').style.display = 'none';
        }
        else {
            document.getElementById('divAntigenToday').style.display = 'block';
        }


        var getVisitDateVal = "<%= this.visitdateval %>";
        var getFemaleLMPVal = "<%= this.LMPval %>";
        var getEDDPVal = "<%= this.EDDval %>";
        var getNxtAppDateVal = "<%= this.nxtAppDateval %>";

        if (getVisitDateVal === '' || getVisitDateVal === '01-Jan-1900')
            getVisitDateVal = 0;

        if (getFemaleLMPVal === '' || getFemaleLMPVal === '01-Jan-1900')
            getFemaleLMPVal = 0;

        if (getEDDPVal === '' || getEDDPVal === '01-Jan-1900')
            getEDDPVal = 0;

        if (getNxtAppDateVal === '' || getVisitDateVal === '01-Jan-1900')
            getNxtAppDateVal = 0;
        //Date processing
        var today = new Date();
        var tomorrow = new Date();
        tomorrow.setDate(today.getDate() + 1);

        var minDate = moment(today).add(-1, 'hours');

        $("#PersonAppointmentDateD").datetimepicker({
            defaultDate: getNxtAppDateVal,
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false            
        });

        $("#AppointmentDate").change(function () {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
            if (moment('' + appDate + '').isAfter(futureDate)) {
                toastr.error("Appointment date cannot be set to over 7 months");
                $("#<%=AppointmentDate.ClientID%>").val("");
                return false;
            }
            appointmentCount();
         });



        $('#PersonAppointmentDateD').datetimepicker().on('dp.change', function (e) {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
			if (moment('' + appDate + '').isAfter(futureDate)) {
				toastr.error("Appointment date cannot be set to over 7 months");
				$("#<%=AppointmentDate.ClientID%>").val("");
				return false;
			}
            appointmentCount();
        });

		$('#DateOfVisit').datepicker({
			allowPastDates: true,
			date: getVisitDateVal,
			restricted: [{ from: tomorrow, to: Infinity }],
			momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
			//restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        $('#DateOfVisit').on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {
            var dateOfVisit = $('#DateOfVisit').datepicker('getDate');

            //console.log("changed" + dateOfVisit);
            //console.log("Enrollment Date: " + DateOfEnrollment);
            //console.log("Moment 1 " + moment(dateOfVisit));
            //console.log("Moment 1 " + moment(DateOfEnrollment));

            var isBeforeDateOfEnrollment = moment(moment(dateOfVisit)).isBefore(moment(DateOfEnrollment));
            if (isBeforeDateOfEnrollment) {
                $("#<%=VisitDate.ClientID%>").val("");
                toastr.error("Visit Date should not be before the date of Enrollment", "Clinical Encounter");
            }
            console.log(isBeforeDateOfEnrollment);
        });



        $("#prescribeDrugs").click(function () {
            $("#btnClosePrecriptionModal").show("fast");
            $("#btnClosePrecription").hide("fast");
        });

        $("#btnOrderLabTests").click(function () {
            $("#btnCloseLabOrderModal").show("fast");
            $("#btnCloseLabOrder").hide("fast");
        });

        $('#PCDateOfOnset').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            date: 0,
            restricted: [{ from: tomorrow, to: Infinity }]
            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });
        $('#OnsetDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
            restricted: [{ from: tomorrow, to: Infinity }],
            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        //$('#ChronicIllnessOnsetDate').datepicker({
        //    allowPastDates: true,
        //    momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
        //    date: 0,
        //    restricted: [{ from: tomorrow, to: Infinity }],
        //    //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        //});

        $("#ChronicIllnessOnsetDate").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false
        });

        $('#FemaleLMP').datepicker({
            allowPastDates: true,
            date: getFemaleLMPVal,
            restricted: [{ from: tomorrow, to: Infinity }],
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
            restricted: [{ from: tomorrow, to: Infinity }],
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
            restricted: [{ from: tomorrow, to: Infinity }],
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });

        //$('#PersonAppointmentDate').datepicker({
        //    allowPastDates: false,
        //    momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }

        //});

        $(".sexhistory").hide();


        $('input:radio[name=optSexualHistory]').change(function () {
            if (this.value == 'No') {

                $(".sexhistory").hide();

                
                if (arrTextSexualHistory.length > 0) {
                    index = arrTextSexualHistory.length;
                    if (parseInt(noofpartners) > 0) {
                        if (noofpartners >= index) {
                            noofpartners = parseInt(noofpartners) - parseInt(index);
                            $('#txtPartners').val(noofpartners);
                        }
                        else {
                            $('#txtPartners').val("");
                        }

                    }
                    else {
                        $('#txtPartners').val("");
                    }
                
                    for (var i = arrSexualHistory.length - 1; i >= 0;i-=1) {
                        
                        if (arrSexualHistory[i].id > 0 ) {
                            

                            

                            arrSexualHistory[i].DeleteFlag = true;
                          
                        }
                        else {
                       
                            
                            arrSexualHistory.splice(i, 1);
                            
                        }
                    }
                    arrTextSexualHistory.length = 0;
                }
               
                $("#dtlSexualHistory>tbody:first").empty();
                

            }
            else if (this.value == 'Yes') {

                $(".sexhistory").show();
            }
        });
        $("#PersonAppointmentDateD").datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false,
            minDate: minDate
        });



        $("#AppointmentDate").change(function () {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
            if (moment('' + appDate + '').isAfter(futureDate)) {
                toastr.error("Appointment date cannot be set to over 7 months");
                $("#<%=AppointmentDate.ClientID%>").val("");
                return false;
            }
            appointmentCount();
        });


        /* limit future dates viralload baseline date*/
        $("#DateOfVisit").on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {
            var dlDate = $('#DateOfVisit').datepicker('getDate');
            //alert(dlDate);

            //var beforeEnrollment = moment(dlDate).isBefore(DateOfEnrollment);
            //if (beforeEnrollment) {
            //    toastr.error("VISIT Date CANNOT be before ENROLLMENT Date");
            //    //        $("#TreatmeantInitiationBaselineViralloadDate").val('');
            //           return false;
            //}
            //var futureDate = moment(dlDate).isAfter(today);
            //    if (futureDate) {
            //        toastr.error("Future dates NOT allowed on Baseline ViralLoad Entries");
            //        $("#TreatmeantInitiationBaselineViralloadDate").val('');
            //        return false;
            //    }
            //    var dhid = $("#DHID").datepicker('getDate');
            //    if (moment(dlDate).isBefore(dhid)) {
            //        $("#TreatmeantInitiationBaselineViralloadDate").val('');
            //        toastr.error("Baseline Viral Load date CANNOT be ealier than HIV Diagnosis Date");
            //        return false;
            //    }
        });


        $('#PersonAppointmentDateD').datetimepicker().on('dp.change', function (e) {
            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
            if (moment('' + appDate + '').isAfter(futureDate)) {
                toastr.error("Appointment date cannot be set to over 7 months");
                $("#<%=AppointmentDate.ClientID%>").val("");
                return false;
            }
            appointmentCount();
        });


        $('#PCDateOfOnset').on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {
            presentingComplaintsDateChange();
        });

        ////////////////////////////////////////////////////////////////////////////////////////////
        //Gender validations
        var male = "Male";

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
                    "targets": [0], "visible": false, "searchable": false

                },
                {
                    "targets": [1], "visible": false, "searchable": false
                }
            ]
        });

        $("#btnSaveChangesOutcome").on('click',
            function () {
                var adverseEvent = '';
                var adverseEventId = 0;
                var outcomeId = $("#EventOutcome").val();
                var outcomeDate = moment($("#outcomeDate").datepicker('getDate')).format('DD-MMM-YYYY');



                //$("#dtlAdverseEventsBdy tr").each(function () {
                //    alert($(this).find("td").eq(2).html());
                //});

                //var ae = $("#adverseEventLable").text();
                //alert(ae);
                //adverseEvent = $("#dtlAdverseEvents tr").find('td').eq(0).html();
                adverseEvent = $("#adverseEventLable").text();

                $.ajax({
                    type: "POST",
                    url: "../WebService/LookupService.asmx/GetLookupItemId",
                    data: "{'lookupItemName':'" + adverseEvent + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        $.ajax({
                            type: "POST",
                            url: "../WebService/PatientadverseEventOutcome.asmx/AddAdverseEventOutcome",
                            data: "{'patientId':'" + PatientId + "','patientMasterVisitId':'" + PatientMasterVisitId + "','adverseEventId':'" + response.d + "','outcomeId':'" + outcomeId + "','outcomeDate':'" + outcomeDate + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                toastr.success(response.d);
                                $('.modal').on('hidden.bs.modal', function (e) {
                                    $(this).removeData();
                                });
                                //$("#adverseEventsTable").refresh();
                                //window.location.reload();
                            },
                            error: function (response) {
                                toastr
                                    .error("Error saving adverseEvent Outcome(s) " + response.d);
                                $("#AdverseEventOutcomeModal .close").click();
                            }
                        });
                    },
                    error: function (response) {
                        toastr
                            .error("Error in fetching itemId " + response.d);
                    }
                });
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
                },
                {
                    "targets": [2],
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
                },
                {
                    "targets": [2],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        var diagnosisTable = $('#dtlDiagnosis').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetDiagnosis",
                dataSrc: function (json) {


                    var itemList = json.d;
                    if (itemList !== null && itemList.length > 0) {

                        for (var i = 0; i < itemList.length; i++) {
                            //var j= 0;
                             //j= i + 1;
                            diagnosisListStatus.push({ id: itemList[i][0],Disease:itemList[i][1],Treatment:itemList[i][2], deleteflag:false,deleted:false })
                            diagnosisList.push(itemList[i][0]);
                            treatmentList.push(itemList[i][2]);
                            DiseaseList.push(itemList[i][1]);
                        }
                    }
                    return json.d;
                }
                ,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
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

                
               
            
           

        var presentingComplaintsTable = $('#dtlPresentingComplaints').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/LoadComplaints",
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
        $('#dtlSexualHistory').on('click', '.btnSexualHistoryDelete', function () {

            var index = $(this).closest('tr').find('td:eq(0)').text();

            $(this).closest('tr').remove();
            if (index > -1) {
                var value = index - 1
                if (arrSexualHistory[value].id > 0) {
                    //  arrSexualHistory.splice(value, 1);
                    arrTextSexualHistory.splice(value, 1);
                    arrSexualHistory[value].DeleteFlag = true;
                }
                else {
                    arrTextSexualHistory.splice(value, 1);
                    arrSexualHistory.splice(value, 1);
                }
            }

            if (!(arrTextSexualHistory == null)) {
                noofpartners = noofpartners - 1;
                $('#txtPartners').val(noofpartners);
            }
            else {
                $('#txtPartners').val("");
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
                var index = DiseaseList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                diagnosisTable
                    .row($(this).parents('tr'))
                    .remove()
                    .draw();
              
                if (index >= -1) {
                    DiseaseList.splice(index, 1);
                    diagnosisList.splice(index, 1);
                    treatmentList.splice(index, 1);
                    
                    if (diagnosisListStatus.length > 0) {
                        if (diagnosisListStatus[index].deleted == false) {
                            diagnosisListStatus[index].deleteflag = true;
                        }
                        else if (diagnosisListStatus[index].deleted == true) {
                            diagnosisListStatus.splice(index, 1);
                        }
                    }
                    diagnosisList.splice(index, 1);
                }

            });

        ////dtlPresentingComplaints
        $("#dtlPresentingComplaints").on('click',
            '.btnDelete',
            function () {
                presentingComplaintsTable
                    .row($(this).parents('tr'))
                    .remove()
                    .draw();

                var index = PresentingComplaintsList.indexOf($(this).parents('tr').find('td:eq(0)').text());
                if (index > -1) {
                    PresentingComplaintsList.splice(index, 1);
                }

            });

        ///////////////////////////////////////////////////////////////////////////////////////////////////

<%--        var onAntiTbDrug = $("#<%=ddlOnAntiTBDrugs.ClientID%>").val();
        if (onAntiTbDrug === 'yes') {
            alert('on anti-tb');
        } else {
            alert("not on anti-tb");}--%>


        //Save patient IPT client workup
        $("#btnSaveIptWorkup").click(function () {
            addPatientIptWorkup();
            $('#IptClientWorkupModal').modal('hide');
        });

        //Save patient IPT Details
        $("#btnSaveIptDetails").click(function () {

           /* if (onAntiTbDrug === 'yes') {}
            if ($('#IptFormDetails').parsley().validate()) {
                var dob = $("#IptDateCollected").val();
                if (moment('' + dob + '').isAfter()) {
                    toastr.error("Date collected cannot be a future date.");
                    return false;
                }
                else {
                    addIpt();
                    $('#IptDetailsModal').modal('hide');
                }
            } else {
                return false;
            }-*/
            addPatientIptOutcome();
            $('#IptOutcomeModal').modal('hide');
        });

        //Save patient IPT Outcome
        $("#btnSaveIptOutcome").click(function () {
            
          var IPTDate = $('#IPTDate').val();
            if (IPTDate == "" || IPTDate == undefined) {
                toastr.error("Kindly note IPT Outcome Date is required");
                $('#IptOutcomeModal').modal('show');
              //  return;
            }
            else {
                addPatientIptOutcome();
                $('#IptOutcomeModal').modal('hide');
            }
        });



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
                    if (data.direction === 'previous') {
                        return;
                    }
                    $("#peripheralNeoropathy").prop('required', false);
                    $("#rash").prop('required', false);
                    $("#hepatotoxicity").prop('required', false);
                    $("#adheranceMeasurement").prop('required', false);
                    $("#antiTb").prop('required', true);

                    //if (($("#cough").val() === 'True') || ($("#fever").val() === 'True') || ($("#weightLoss").val() === 'True') || ($("#nightSweats").val() === 'True')) {
                    //	//$("#sputum").prop('required', true);
                    //	//$("#geneXpert").prop('required', true);
                    //	//$("#chest").prop('required', true);
                    //	$("#antiTb").prop('required', true);
                    //	//$("#contactsInvitation").prop('required', true);
                    //	//$("#iptEvaluation").prop('required', true);
                    //}

                    /* add constraints based on age*/
                    if ($('#datastep1').parsley().validate()) {

                        if ($('#EverBeenOnIpt').val() == 'True' && $("#onIpt").val() == 'False' && $('#iptEvent').val() == '0') {
                            toastr.error('Please provide IPT Outcome');
                            evt.preventDefault();
                            return false;
                        }

                       // if (($("#tbInfected").val() === 'True') && ($("#onIpt").val() === 'False') && ($("#EverBeenOnIpt").val() === 'True')) {
                           // if ($('#datastep1').parsley().validate()) {
                                //                  if (($("#tbInfected").val() === 'True') && ($("#onIpt").val() === 'False') && ($("#EverBeenOnIpt").val() === 'True'))
                                //               {

                                //                  }else
                                //                   {
                                //                          addPatientIcf();
                                //                   }


                                //if (($("#cough").val() === 'True') || ($("#fever").val() === 'True') || ($("#weightLoss").val() === 'True') || ($("#nightSweats").val() === 'True')) {
                                //	addPatientIcfAction();
                                //                  }
                                addPatientIcf();
                                addPatientIcfAction();
                                saveNutritionAssessment();
                                savePatientEncounterPresentingComplaint(evt);
                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                    
                
                else if (data.step === 2) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        savePatientEncounterChronicIllness();
                        saveSexualHistory();
                    }
                    //if ($("#datastep2").parsley().validate()) {

                    //} else {
                    //    stepError = $('.parsley-error').length === 0;
                    //    totalError += stepError;
                    //    evt.preventDefault();
                    //}
                }
                else if (data.step === 3) {
                    if (data.direction === 'previous') {




                        return;
                    } else {
                        $('#datastep3').parsley().destroy();
                        $("#<%=ddlExaminationType.ClientID%>").attr('disabled', 'disabled');
                        $('#datastep3').parsley({
                            excluded:
                            "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
                        });

                        if ($('input[name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$systemsOkay"]:checked').length > 0) {
                            $('.errorBlock').hide();
                        } else {
                            $('.errorBlock').show();
                            return false;
                        }

                        if ($('#datastep3').parsley().validate()) {
                            $("#<%=ddlExaminationType.ClientID%>").prop('disabled', false);
                            $.when(savePatientPhysicalExams(evt)).then(
                                function () {
                                    setTimeout(function () {
                                        saveWhoStage();
                                        savePatientOIS();

                                    }, 5000);

                                });
                        } else {
                            stepError = $('.parsley-error').length === 0;
                            totalError += stepError;
                            evt.preventDefault();
                        }
                    }
                    //if ($("#datastep3").parsley().validate()) {

                    //} else {
                    //    stepError = $('.parsley-error').length === 0;
                    //    totalError += stepError;
                    //    evt.preventDefault();
                    //}
                }
                else if (data.step === 4) {
                    if (data.direction === 'previous') {
                        GetPatientOIS();
                        return;
                    } else {
                        //savePatientPatientManagement();
                        if ($('#AppointmentForm').parsley().validate()) {
                            var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
                            var appDate = $("#<%=AppointmentDate.ClientID%>").val();
                            if (moment('' + appDate + '').isAfter(futureDate)) {
                                toastr.error("Appointment date cannot be set to over 7 months");
                                return false;
                            }
                            //save patient management
                            $.when(savePatientPatientManagement()).then(function () {
                                setTimeout(function () {
                                    window.location.href = '<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
                                },2000);
                            });

                            //save appointment
                            checkExistingAppointment();
                        } else {
                            stepError = $('.parsley-error').length === 0;
                            totalError += stepError;
                            evt.preventDefault();
                        }
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

        function savePatientEncounterPresentingComplaint(evt) {
            var visitDate = $("#<%=VisitDate.ClientID%>").val();
            var visitScheduled = $("input[name$=Scheduled]:checked").val();

            var visitBy = $("#<%=ddlVisitBy.ClientID%>").find(":selected").val();
            var anyComplaints = $("input[name$=anyComplaints]:checked").val();
            var adverseEvents = $("input[name$=adverseEvents]:checked").val();
            var complaints = $("#<%=complaints.ClientID%>").val();
            var tbscreening = $("#<%=ddlICFTBScreeningOutcome.ClientID%>").val();
            var nutritionscreening = $("#<%=nutritionscreeningstatus.ClientID%>").find(":selected").val();


            /////////////////////////////////////////////////////
            if (parseInt(anyComplaints, 10) === 1) {
               
                if (!presentingComplaintsTable.data().any()) {
                  
                    toastr.error("Presenting Complaints", "Presenting complaints missing.Kindly indicate the presenting complaints.");
                    
                    evt.preventDefault();
                   
                }
            }

            if (parseInt(adverseEvents,10) === 1) {
                if (!advEventsTable.data().any()) {
                    toastr.error("Adverse Event(s)", "Adverse Event(s) missing.Kindly indicate the adverse events");
                    
                     evt.preventDefault();
                   
                }
            }


            ///////////////////////////////////////////////////////
            var rowCount = $('#dtlAdverseEvents tbody tr').length;
            var adverseEventsArray = new Array();
            try {
                for (var i = 0; i < rowCount; i++) {
                    adverseEventsArray[i] = {
                        //"adverseSeverityID": advEventsTable.row(i).data()[0],
                        //"adverseEvetId":adverseEvent,
                        //"adverseEvent": advEventsTable.row(i).data()[1],
                        //"medicineCausingAE": advEventsTable.row(i).data()[2],
                        //"adverseSeverity": advEventsTable.row(i).data()[3],
                        //"adverseAction": advEventsTable.row(i).data()[4]

                        "adverseSeverityID": advEventsTable.row(i).data()[0],
                        "adverseEventId": advEventsTable.row(i).data()[1],
                        "adverseEvent": advEventsTable.row(i).data()[2],
                        "medicineCausingAE": advEventsTable.row(i).data()[3],
                        "adverseSeverity": advEventsTable.row(i).data()[4],
                        "adverseAction": advEventsTable.row(i).data()[5]
                    }
                }
            }
            catch (ex) { }



            var rowCount = $('#dtlPresentingComplaints tbody tr').length;
            var presentingComplaintsArray = new Array();
            try {
                for (var i = 0; i < rowCount; i++) {
                    presentingComplaintsArray[i] = {
                        "presentingComplaintID": presentingComplaintsTable.row(i).data()[0],
                        "presentingComplaint": presentingComplaintsTable.row(i).data()[1],
                        "onsetDate": presentingComplaintsTable.row(i).data()[2]
                    }
                }
            }
            catch (ex) { }


            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/savePatientEncounterPresentingComplaints",
                data: "{'VisitDate':'" + visitDate + "','VisitScheduled':'" + visitScheduled + "','VisitBy':'" + visitBy + "','anyComplaints':'" + anyComplaints + "','Complaints':'" + complaints + "','TBScreening':'" + tbscreening + "','NutritionalStatus':'" + nutritionscreening + "','adverseEvent':'" + JSON.stringify(adverseEventsArray) + "','presentingComplaints':'" + JSON.stringify(presentingComplaintsArray) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    console.log(response.d);
                    if (response.d > 0)

                        toastr.success(response.d, "Presenting Complaints");
                    else

                        toastr.error(response.d, "Error occured while saving Presenting Complaints");
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
                var active = 0;
                for (var i = 0; i < rowCount; i++) {

                    if ($('#chkChronic' + chronicTable.row(i).data()[0] + '').is(":checked"))
                        active = 1;
                    else
                        active = 0;

                    chronicIllnessArray[i] = {
                        "chronicIllnessID": chronicTable.row(i).data()[0],
                        "chronicIllness": chronicTable.row(i).data()[1],
                        "treatment": chronicTable.row(i).data()[2],
                        "dose": "",
                        "OnsetDate": chronicTable.row(i).data()[3],
                        "Active": active
                        //"Active": chronicTable.row(i).checkboxes.selected()[5]
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
                for (var i = 0; i < rowCount; i++) {
                    AllergyArray[i] = {
                        "allergyID": allergyTable.row(i).data()[0],
                        "reactionID": allergyTable.row(i).data()[1],
                        "severityID": allergyTable.row(i).data()[2],
                        "allergy": allergyTable.row(i).data()[3],
                        "reaction": allergyTable.row(i).data()[4],
                        "severity": allergyTable.row(i).data()[5],
                        "onsetDate": allergyTable.row(i).data()[6]
                    }
                }
            }
            catch (ex) { }
            ///////////////////////////////////////////////////////
            var rowCount = $('#dtlVaccines tbody tr').length;
            var vaccineArray = new Array();
            try {
                for (var i = 0; i < rowCount; i++) {
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

        function savePatientPhysicalExams(evt) {
            var rowCount = $('#dtlPhysicalExam tbody tr').length;
            var generalExamination = getCheckBoxListItemsChecked('<%= cblGeneralExamination.ClientID %>');
            if (generalExamination == "") {
                toastr.error(generalExamination, "Please check at least one General Examination.");
                evt.preventDefault();
                return false;
            }
            var physicalExamArray = new Array();
            try {
                for (var i = 0; i < rowCount; i++) {
                    physicalExamArray[i] = {
                        "reviewOfSystemsID": examTable.row(i).data()[0],
                        "systemTypeID": examTable.row(i).data()[1],
                        "findingID": examTable.row(i).data()[2],
                        "systemTypeText": examTable.row(i).data()[3],
                        "findingIDText": examTable.row(i).data()[4],
                        "findingsNotes": examTable.row(i).data()[5]
                    }
                }
            }
            catch (ex) { }

            //console.log(physicalExamArray);
            //return false;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/savePatientPhysicalExam",
                data: "{'physicalExam':'" + JSON.stringify(physicalExamArray) + "','generalExam':'" + generalExamination + "'}",
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
        function GetPreviousSexualHistory() {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetPreviousSexualHistory",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var ArrayResults = [];
                    var strTable = "";
                    ArrayResults = JSON.parse(response.d);
                    $("#PreviousSexualHistory").show();
                    
                       
                    if (!(ArrayResults.Gender == null || ArrayResults.Orientation == null || ArrayResults.HivStatus == null || ArrayResults.VisitDate == null || ArrayResults.noofpartners == null  ||ArrayResults.noofpartners== 0)) {
                        $("#PreviousSexualHistory").show();

                    
                  


                    if (!(ArrayResults.VisitDate == null)) {
                        var visitdate = new Date();
                        visitdate = moment(ArrayResults.VisitDate).format('YYYY-MM-DD');
                        $("#txtSexualHistoryVisitDate").val(visitdate.toString());
                    }

                    if (!(ArrayResults.Gender == null || ArrayResults.Orientation == null || ArrayResults.HivStatus == null)) {

                        if (!(ArrayResults.noofpartners == null)) {
                            if (!(ArrayResults.noofpartners == undefined)) {
                                strTable += "<tr><th td align='left' class='col_sm-1'>Partners</th></tr>";
                                strTable += "<tr>";
                                strTable += "<td  align='left'>";
                                strTable += "Number of Partners"
                                strTable += "</td>"
                                strTable += "<td  align='left'>";
                                strTable += ArrayResults.noofpartners;
                                strTable += "</td>"
                                strTable += "</tr>";

                            }
                        }
                        if (!(ArrayResults.Gender == null)) {

                            if (ArrayResults.Gender.length > 0) {
                                strTable += "<tr><th td align='left'>Gender</th></tr>"
                                for (var i = 0; i < ArrayResults.Gender.length; i++) {
                                    if (!(ArrayResults.Gender[i].value == 0)) {
                                        strTable += "<tr>";
                                        strTable += "<td align='left'>";
                                        strTable += ArrayResults.Gender[i].ItemValue;
                                        strTable += "</td>"
                                        strTable += "<td align='left'>";
                                        strTable += ArrayResults.Gender[i].value;
                                        strTable += "</td>"
                                        strTable += "</tr>";
                                    }
                                }
                            }


                        }
                        if (!(ArrayResults.HivStatus == null)) {

                            if (ArrayResults.HivStatus.length > 0) {

                                strTable += "<tr><th  align='left' >HIVStatus</th></tr>"
                                for (var i = 0; i < ArrayResults.HivStatus.length; i++) {
                                    if (!(ArrayResults.HivStatus[i].value == 0)) {
                                        strTable += "<tr>";
                                        strTable += "<td align='left' style='width:20%'>";
                                        strTable += ArrayResults.HivStatus[i].ItemValue;
                                        strTable += "</td>"
                                        strTable += "<td align='left' style='width:20%'>"
                                        strTable += ArrayResults.HivStatus[i].value;
                                        strTable += "</td>"
                                        strTable += "</tr>";
                                    }
                                }
                            }


                        }
                        if (!(ArrayResults.Orientation == null)) {

                            if (ArrayResults.Orientation.length > 0) {
                                strTable += "<tr><th align='left'>Orientation</th></tr>"
                                for (var i = 0; i < ArrayResults.Orientation.length; i++) {
                                    if (!(ArrayResults.Orientation[i].value == 0)) {
                                        strTable += "<tr>";
                                        strTable += "<td align='left'>";
                                        strTable += ArrayResults.Orientation[i].ItemValue;
                                        strTable += "</td>"
                                        strTable += "<td align='left'>";
                                        strTable += ArrayResults.Orientation[i].value;
                                        strTable += "</td>"
                                        strTable += "</tr>";
                                    }
                                }
                            }


                        }
                        $("#PreviousSexualOutcome").hide();
                        $("#PreviousSexualHistory").show();
                        $("#dtlPreviousHistory").append('' + strTable  + '');


                    }
                    }

                    else {
                        $("#PreviousSexualHistory").hide();
                        $("#PreviousSexualOutcome").show();
                    }
                },
                error: function (response) {

                    toastr.error(response.d, "Error Loading the Patient OIS");
                }


            });

        }
        function GetPatientSexualHistory() {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetSexualHistory",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var ArrayResults = [];
                    
                    ArrayResults = JSON.parse(response.d);
                  
                        if (!(ArrayResults.sexuallyactive == "" || ArrayResults.sexuallyactive == null || ArrayResults.sexuallyactive == 'undefined' || ArrayResults.sexuallyactive == 0)) {

                            if (ArrayResults.sexuallyactive.toString() == "yes") {
                                var value = "Yes";
                                $("input[name=optSexualHistory][value=" + value + "]").prop('checked', true);
                                $(".sexhistory").show();
                            }
                            else if (ArrayResults.sexuallyactive.toString() == "no") {
                                var value = "No";
                                $("input[name=optSexualHistory][value=" + value + "]").prop('checked', true);
                                $(".sexhistory").hide();
                            }

                    }

               
                    if (!(ArrayResults.list == null)) {
                        if (ArrayResults.list.length > 0) {
                            for (var i = 0; i < ArrayResults.list.length; i++) {

                                var arrHRB = [];
                                var selectedhistory = {
                                    count: 0,
                                    PartnerStatus: "",
                                    Gender: "",
                                    SexualOrientation: "",
                                    HighRisk: arrHRB
                                }
                                var history = {
                                    uniqueid: 0,
                                    id: 0,
                                    PartnerStatus: 0,
                                    Gender: 0,
                                    SexualOrientation: 0,
                                    DeleteFlag: false,
                                    HighRisk: arrHRB
                                }

                                history.id = ArrayResults.list[i].id;
                                history.uniqueid = ArrayResults.list[i].uniqueid;
                                history.PartnerStatus = ArrayResults.list[i].PartnerStatus;
                                history.Gender = ArrayResults.list[i].Gender;
                                history.SexualOrientation = ArrayResults.list[i].SexualOrientation;
                                for (var t = 0; t < ArrayResults.list[i].Highrisk.length; t++) {
                                    var id = ArrayResults.list[i].Highrisk[t].Id;
                                    var value = ArrayResults.list[i].Highrisk[t].value;
                                    arrHRB.push({ id: id, value: value });
                                    
                                }
                              
                                history.HighRisk = arrHRB;
                                
                            
                                arrSexualHistory.push(history);
                                var count = history.uniqueid;
                                selectedhistory.count = count;

                                if (!(ArrayResults.list[i].PartnerStatus == "" || ArrayResults.list[i].PartnerStatus == null || ArrayResults.list[i].PartnerStatus == 'undefined' || ArrayResults.list[i].PartnerStatus == 0)) {
                                    var Status = ArrayResults.hivstatus.find(t => t.ItemId == ArrayResults.list[i].PartnerStatus);
                                    selectedhistory.PartnerStatus = Status.ItemDisplayName;
                                }
                                else {

                                    selectedhistory.PartnerStatus = "";
                                }

                                if (!(ArrayResults.list[i].Gender == "" || ArrayResults.list[i].Gender == null || ArrayResults.list[i].Gender == 'undefined' || ArrayResults.list[i].Gender == 0)) {

                                    var Gender = ArrayResults.gender.find(t => t.ItemId == ArrayResults.list[i].Gender);
                                    selectedhistory.Gender = Gender.ItemDisplayName;
                                }
                                else {
                                    selectedhistory.Gender = "";
                                }

                                if (!(ArrayResults.list[i].SexualOrientation == "" || ArrayResults.list[i].SexualOrientation == null || ArrayResults.list[i].SexualOrientation == 'undefined' || ArrayResults.list[i].SexualOrientation == 0)) {
                                    var SexualOrientation = ArrayResults.sexualorient.find(t => t.ItemId == ArrayResults.list[i].SexualOrientation);
                                    selectedhistory.SexualOrientation = SexualOrientation.ItemDisplayName;
                                }
                                else {
                                    selectedhistory.SexualOrientation = "";
                                }

                                
                                selectedhistory.HighRisk = arrHRB;
                                arrTextSexualHistory.push(selectedhistory);


                            }


                        }
                    }


                   
                    if (arrTextSexualHistory.length > 0) {


                        for (var t = 0; t < arrTextSexualHistory.length; t++) {

                            var str = arrTextSexualHistory[t].HighRisk.map(function (elem) {
                                return elem.value;
                            }).join(",");

                            console.log(arrTextSexualHistory[t].count);
                            var table = "<tr><td align='left'  >" +
                                arrTextSexualHistory[t].count +
                                "</td><td align='left'>" +
                                arrTextSexualHistory[t].PartnerStatus
                                +
                                "</td><td align='left'>" +
                                arrTextSexualHistory[t].Gender
                                +
                                "</td><td align='left'>" +
                                arrTextSexualHistory[t].SexualOrientation
                                +
                                "</td><td align='left' >" +
                                str +
                                "</td><td align='right'><button type='button' class='btnSexualHistoryDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                            $("#dtlSexualHistory>tbody:first").append('' + table + '');
                        }
                    }


                    if (!(ArrayResults.numberofpartners == "" || ArrayResults.numberofpartners == null || ArrayResults.numberofpartners == 'undefined' )) {
                        if (ArrayResults.numberofpartners == arrSexualHistory.length || ArrayResults.numberofpartners > arrSexualHistory.length)
                        {
                            noofpartners = ArrayResults.numberofpartners;
                        $('#txtPartners').val(noofpartners);
                    }
                    else {
                        noofpartners = arrSexualHistory.length;
                        $('#txtPartners').val(noofpartners);
                    }
                }
                
                },
                error: function (response) {

                    toastr.error(response.d, "Error Loading the Patient OIS");
                }


            });

        }



        function saveSexualHistory() {
            var SexuallyActive = $("input[name='optSexualHistory']:checked").val();
           
            var t=0;
            //for (var i= 0; i < arrSexualHistory.length;i++)
            //{
            //    t = i + 1;
            //    if (arrSexualHistory[i].uniqueid == 0) {
            //        arrSexualHistory[i].uniqueid = t;
            //    } 
                        
            // }
           var NoofPartners = $('#txtPartners').val().toString();
            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/SaveSexualHistory",
                data: "{'data': '" + JSON.stringify(arrSexualHistory) + "','sexuallyactive':'" + SexuallyActive +"','partnersno':'"+NoofPartners+"' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success( "Save Patient Sexual History");
                
                    var ArrayResults = [];
                    ArrayResults = JSON.parse(response.d);
                    for (var t = 0; t < arrSexualHistory.length; t++) {
                        for (var i = 0; i < ArrayResults.list.length; i++) {

                            if (ArrayResults.list[i].uniqueid == arrSexualHistory[i].uniqueid) {
                                arrSexualHistory[i].id = ArrayResults.list[i].id;
                            }

                        }
                    }
                    


                },
                error: function (response) {
                    toastr.error(response.d, "Save Patient Sexual History");
                }
        });
        }

       
        function saveWhoStage() {
            var whostage = $("#<%=WHOStage.ClientID%>").val();

            if (whostage.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/savePatientWhoStage",
                    data: "{'whoStage':'" + whostage + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "WHO Stage");
                    },
                    error: function (response) {
                        toastr.error(response.d, "WHO Stage Error");
                    }
                });
            }
            else {
                toastr.info("No  WhoStage was recorded");
               }

         }

		function savePatientPatientManagement() {
			var workPlan = $("#<%=txtWorkPlan.ClientID%>").val();
			var phdp = getCheckBoxListItemsChecked('<%= cblPHDP.ClientID %>');
			var arvAdherence = $("#<%=arvAdherance.ClientID%>").find(":selected").val();
			var ctxAdherence = $("#<%=ctxAdherance.ClientID%>").find(":selected").val();

			var rowCount = $('#dtlDiagnosis tbody tr').length;
			var diagnosisArray = new Array();
            try {
                if (diagnosisListStatus.length > 0) {
                    for (var i = 0; i < diagnosisListStatus.length; i++) {
                        //var j= 0;
                        diagnosisArray[i] = {
                            "diagnosis": diagnosisListStatus[i].id,
                            "treatment": diagnosisListStatus[i].Treatment,
                            "deleteflag": diagnosisListStatus[i].deleteflag
                        }



                    }
                }


                else {
                    for (var i = 0; i < rowCount; i++) {
                        diagnosisArray[i] = {
                            "diagnosis": diagnosisTable.row(i).data()[0],
                            "treatment": diagnosisTable.row(i).data()[2],
                            "deleteflag": false
                        }
                    }
                }
			}
			catch (ex) { }

			$.ajax({
				type: "POST",
				url: "../WebService/PatientEncounterService.asmx/savePatientManagement",
				data: "{'workplan':'" + workPlan + "','phdp':'" + phdp + "','ARVAdherence':'" + arvAdherence + "','CTXAdherence':'" + ctxAdherence + "','diagnosis':'" + JSON.stringify(diagnosisArray) + "'}",
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

		function addPatientIcf() {
			var cough = $("#<%=ddlICFCough.ClientID%>").val();
			var weightLoss = $("#<%=ddlICFWeight.ClientID%>").val();
			var nightSweats = $("#<%=ddlICFNightSweats.ClientID%>").val();
			var fever = $("#<%=ddlICFFever.ClientID%>").val();
			var onIpt = $("#<%=ddlICFCurrentlyOnIPT.ClientID%>").val();
			var onAntiTbDrugs = $("#<%=ddlOnAntiTBDrugs.ClientID%>").val();
			var patientId = <%=PatientId%>;
			var patientMasterVisitId = <%=PatientMasterVisitId%>;
			var everBeenOnIpt = $("#<%=ddlICFStartIPT.ClientID%>").val();
			$.ajax({
				type: "POST",
				url: "../WebService/PatientTbService.asmx/AddPatientIcf",
				data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','cough': '" + cough + "','fever': '" + fever + "','nightSweats': '" + nightSweats + "','weightLoss': '" + weightLoss + "','onAntiTbDrugs': '" + onAntiTbDrugs + "','onIpt': '" + onIpt + "','everBeenOnIpt': '" + everBeenOnIpt + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					toastr.success(response.d, "Patient ICF saved successfully");
				},
                error: function (response) {
                    alert(JSON.stringify(response));
					toastr.error(response.d, "Patient ICF not saved");
				}
			});
		}

		function addPatientIcfAction() {
			var chestXray = $("#<%=ddlChestXray.ClientID%>").val();
			var sputumSmear = $("#<%=ddlSputumSmear.ClientID%>").val();
			var geneXpert = $("#<%=ddlGeneXpert.ClientID%>").val();
			var invitationOfContacts = $("#<%=ddlInvitationofContacts.ClientID%>").val();
			var evaluatedForIpt = $("#<%=ddlEvaluatedforIPT.ClientID%>").val();
			var startAntiTb = $("#<%=ddlStartAntiTB.ClientID%>").val();
			var patientId = <%=PatientId%>;
			var patientMasterVisitId = <%=PatientMasterVisitId%>;
			$.ajax({
				type: "POST",
				url: "../WebService/PatientTbService.asmx/AddPatientIcfAction",
				data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','chestXray': '" + chestXray + "','evaluatedForIpt': '" + evaluatedForIpt + "','invitationOfContacts': '" + invitationOfContacts + "','sputumSmear': '" + sputumSmear + "','startAntiTb': '" + startAntiTb + "','geneXpert': '" + geneXpert + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					toastr.success(response.d, "Patient ICF Action saved successfully");
				},
                error: function (response) {
                    alert(JSON.stringify(response));
					toastr.error(response.d, "Patient ICF Action not saved");
				}
			});
		}

		function addIpt() {
			var weight = $("#weight").val();
			if (weight === '') { weight = 0 }
			var hepatotoxicity = $("#hepatotoxicity").val();
			var iptDateCollected = moment($("#IptDateCollected").val()).format('DD-MMM-YYYY');
			var iptDueDate = moment($("#iptDuedate").val()).format('DD-MMM-YYYY');
			var peripheralneoropathy = $("#peripheralNeoropathy").val();
			var rash = $("#rash").val();
			var adheranceMeasurement = $("#adheranceMeasurement").val();
			var hepatotoxicityAction = $("#hepatotoxicityAction").val();
			var peripheralneoropathyAction = $("#peripheralAction").val();
			var rashAction = $("#rashAction").val();
			var adheranceMeasurementAction = $("#adheranceAction").val();
			var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
			$.ajax({
				type: "POST",
				url: "../WebService/PatientTbService.asmx/AddIpt",
				data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','weight': '" + weight + "','iptDueDate': '" + iptDueDate + "','iptDateCollected': '" + iptDateCollected + "','hepatotoxicity': '" + hepatotoxicity + "','peripheralneoropathy': '" + peripheralneoropathy + "','rash': '" + rash + "','adheranceMeasurement': '" + adheranceMeasurement + "','hepatotoxicityAction': '" + hepatotoxicityAction + "','peripheralneoropathyAction': '" + peripheralneoropathyAction + "','rashAction': '" + rashAction + "','adheranceMeasurementAction': '" + adheranceMeasurementAction + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					toastr.success(response.d, "Patient IPT saved successfully");
				},
				error: function (response) {
					toastr.error(response.d, "Patient IPT not saved");
				}
			});
		}

		function addPatientIptWorkup() {
			var abdominalTenderness = $("#abdominalTenderness").val();
			var yellownessOfEyes = $("#yellowEyes").val();
			var yellowColouredUrine = $("#urineColour").val();
			var numbness = $("#numbness").val();
			var liverFunctionTests = $("#liverTest").val();
			var startIpt = $("#startIpt").val();
            var iptStartDate = $("#IPTStartDate").val();
			var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            
			$.ajax({
				type: "POST",
				url: "../WebService/PatientTbService.asmx/AddPatientIptWorkup",
				data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','abdominalTenderness': '" + abdominalTenderness + "','numbness': '" + numbness + "','yellowColouredUrine': '" + yellowColouredUrine + "','yellownessOfEyes': '" + yellownessOfEyes + "','liverFunctionTests': '" + liverFunctionTests + "','startIpt': '" + startIpt + "','iptStartDate': '" + iptStartDate + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					toastr.success(response.d, "Patient IPT Workup saved successfully");
				},
				error: function (response) {
					toastr.error(response.d, "Patient IPT Workup not saved");
				}
			});
		}

		function addPatientIptOutcome() {
			var iptEvent = $("#iptEvent").val();
            var reasonForDiscontinuation = $("#discontinuation").val();
           
            var iptOutComeDate = $("#IPTDate").val();
			var patientId = <%=PatientId%>;
			var patientMasterVisitId = <%=PatientMasterVisitId%>;
			$.ajax({
				type: "POST",
				url: "../WebService/PatientTbService.asmx/AddPatientIptOutcome",
				data: "{'patientId': '" + patientId + "','IPTDate':'" + iptOutComeDate + "','patientMasterVisitId': '" + patientMasterVisitId + "','iptEvent': '" + iptEvent + "','reasonForDiscontinuation': '" + reasonForDiscontinuation + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					toastr.success(response.d, "Patient IPT outcome saved successfully");
				},
				error: function (response) {
					toastr.error(response.d, "Patient IPT outcome not saved");
				}
			});
        }
        function addPatientTBRx() {
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var TBRxStartDate = $("#<%=tbTBRXStartDate.ClientID%>").val();
            var TBRxEndDate = $("#<%=tbTBRXEndDate.ClientID%>").val();
            var TBRxRegimen = $("#<%=ddlICFRegimen.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "../WebService/PatientTbService.asmx/AddPatienTBRx",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','TBRxStartDate': '" + TBRxStartDate + "','TBRxEndDate': '" + TBRxEndDate + "','TBRxRegimen':'" + TBRxRegimen +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Patient TBRx saved successfully");
                },
                error: function (response) {
                    toastr.error(response.d, "Patient TBRx not saved");
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

		$("#btnAdherenceAssessment").click(function () {

			var question1 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question1']:checked").val());
			var question2 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question2']:checked").val());
			var question3 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question3']:checked").val());
			var question4 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question4']:checked").val());

			var question5 = parseFloat($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question5']:checked").val());
			var question6 = parseFloat($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question6']:checked").val());
			var question7 = parseFloat($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question7']:checked").val());
			var question8 = parseFloat($("#<%=Question8.ClientID%>").val());

			console.log(question8);

			$('.errorBlock1').hide();
			$('.errorBlock2').hide();
			$('.errorBlock3').hide();
			$('.errorBlock4').hide();

			$('.errorBlock5').hide();
			$('.errorBlock6').hide();
			$('.errorBlock7').hide();
			$('.errorBlock8').hide();

			if (isNaN(question1)) {
				$('.errorBlock1').show();
				return false;
			}

			if (isNaN(question2)) {
				$('.errorBlock2').show();
				return false;
			}

			if (isNaN(question3)) {
				$('.errorBlock3').show();
				return false;
			}

			if (isNaN(question4)) {
				$('.errorBlock4').show();
				return false;
			}

			var adherenceScore = question1 + question2 + question3 + question4;

			if (isNaN(question5) && adherenceScore > 0) {
				$('.errorBlock5').show();
				return false;
			}

			if (isNaN(question6) && adherenceScore > 0) {
				$('.errorBlock6').show();
				return false;
			}

			if (isNaN(question7) && adherenceScore > 0) {
				$('.errorBlock7').show();
				return false;
			}

			if (isNaN(question8) && adherenceScore > 0) {
				$('.errorBlock8').show();
				return false;
			}

			$('.errorBlock1').hide();
			$('.errorBlock2').hide();
			$('.errorBlock3').hide();
			$('.errorBlock4').hide();

			$('.errorBlock5').hide();
			$('.errorBlock6').hide();
			$('.errorBlock7').hide();
			$('.errorBlock8').hide();

			/*
			console.log(question1);
			console.log(question2);
			console.log(question3);
			console.log(question4);
			*/

			if (isNaN(question5)) {
				question5 = 0;
			}

			if (isNaN(question6)) {
				question6 = 0;
			}

			if (isNaN(question7)) {
				question7 = 0;
			}

			if (isNaN(question8)) {
				question8 = 0;
			}

			//console.log(adherenceScore);

			$.ajax({
				type: "POST",
				url: "../WebService/PatientEncounterService.asmx/SavePatientAdherenceAssessment",
				data: "{'feelBetter': '" + question4 + "', 'carelessAboutMedicine': '" + question2 + "', 'feelWorse': '" + question3 + "', 'forgetMedicine': '" + question1 + "','takeMedicine':'" + question5 + "', 'stopMedicine':'" + question6 + "', 'underPressure':'" + question7 + "', 'difficultyRemembering':'" + question8 + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (response) {
					//console.log(response.d);
					var returnValue = JSON.parse(response.d);
					toastr.success(returnValue[0], "Adherence Assessment");
					$("#<%=arvAdherance.ClientID%>").val(returnValue[1]);
			   
					$('#adherenceAssessmentModal').modal('hide');
				},
				error: function (xhr, errorType, exception) {
					var jsonError = jQuery.parseJSON(xhr.responseText);
					toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
					return false;
				}
			});
		});


		$("#btnSaveCategorization").click(function () {
			if ($('#Categorization').parsley().validate()) {
				AddPatientCategorization();
			} else {
				return false;
			}
		});


		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question1"]').change(function () {
			calculateAdherenceScore();
		});

		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question2"]').change(function () {
			calculateAdherenceScore();
		});

		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question3"]').change(function () {
			calculateAdherenceScore();
		});

		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question4"]').change(function () {
			calculateAdherenceScore();
		});

		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question5"]').change(function () {
			calculateAdherenceScore();
		});

		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question6"]').change(function () {
			calculateAdherenceScore();
		});

		$('input[type=radio][name="ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question7"]').change(function () {
			calculateAdherenceScore();
		});

		$("#<%=Question8.ClientID%>").change(function() {
			calculateAdherenceScore();
		});


		function calculateAdherenceScore() {
			var question1 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question1']:checked").val());
			var question2 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question2']:checked").val());
			var question3 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question3']:checked").val());
			var question4 = parseInt($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question4']:checked").val());
			var question5 = parseFloat($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question5']:checked").val());
			var question6 = parseFloat($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question6']:checked").val());
			var question7 = parseFloat($("input[name='ctl00$IQCareContentPlaceHolder$ucPatientClinicalEncounter$Question7']:checked").val());
			var question8 = parseFloat($("#<%=Question8.ClientID%>").val());

			var adherenceScore = 0;
			var mmas8Score = 0;

			if (!isNaN(question1)) {
				adherenceScore = adherenceScore + question1;
			}

			if (!isNaN(question2)) {
				adherenceScore = adherenceScore + question2;
			}

			if (!isNaN(question3)) {
				adherenceScore = adherenceScore + question3;
			}

			if (!isNaN(question4)) {
				adherenceScore = adherenceScore + question4;
			}

			if (!isNaN(question5)) {
				mmas8Score = parseFloat(mmas8Score) + question5;
			}

			if (!isNaN(question6)) {
				mmas8Score = parseFloat(mmas8Score) + question6;
			}

			if (!isNaN(question7)) {
				mmas8Score = parseFloat(mmas8Score) + question7;
			}

			if (!isNaN(question8)) {
				mmas8Score = parseFloat(mmas8Score) + question8;
			}

			mmas8Score = parseFloat(mmas8Score) + parseFloat(adherenceScore);
			//var adherenceScore = question1 + question2 + question3 + question4;
			//console.log(adherenceScore);
			$("#<%=adherenceScore.ClientID%>").text(adherenceScore + "/4");
			$("#<%=mmas8Score.ClientID%>").text(mmas8Score + "/8");


			if (!isNaN(question1) && !isNaN(question2) && !isNaN(question3) && !isNaN(question4)) {
				var score = question1 + question2 + question3 + question4;
				var adherenceRating = "";

				if (score === 0) {
					adherenceRating = "Good";
					$("#MMAS8").hide();
				} else if (score >= 1 && score <= 2) {
					adherenceRating = "Fair";
					$("#MMAS8").show();
				} else if (score >= 3 && score <= 4) {
					adherenceRating = "Poor";
					$("#MMAS8").show();
				}

				$("#<%=adherenceRating.ClientID%>").text(adherenceRating);

			}

			if (!isNaN(question1) && !isNaN(question2) && !isNaN(question3) && !isNaN(question4)) {
				var MMAS8Score = "";

				if (mmas8Score === 0) {
					MMAS8Score = "Good";
				} else if (mmas8Score >= 1 && mmas8Score <= 2) {
					MMAS8Score = "Inadequate";
				} else if (mmas8Score >= 3 && mmas8Score <= 8) {
					MMAS8Score = "Poor";
				}
				$("#<%=mmas8Adherence.ClientID%>").text(MMAS8Score);        
			}
        }
    

        function AddPatientCategorization() {
            var artRegimenPeriod = $("input[name$=ArtRegimenPeriod]:checked").val();
            var activeOis = $("input[name$=ActiveOis]:checked").val();
            var visitsAdherant = $("input[name$=VisitsAdherant]:checked").val();
            var vlCopies = $("input[name$=VlCopies]:checked").val();
            var ipt = $("input[name$=Ipt]:checked").val();
            var bmi = $("input[name$=Bmi]:checked").val();
            var age = $("input[name$=Age]:checked").val();
            var healthcareConcerns = $("input[name$=HealthcareConcerns]:checked").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientCategorization",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','artRegimenPeriod': '" + artRegimenPeriod + "','activeOis': '" + activeOis + "','visitsAdherant': '" + visitsAdherant + "','vlCopies': '" + vlCopies + "','ipt': '" + ipt + "','bmi': '" + bmi + "','age': '" + age + "','healthcareConcerns': '" + healthcareConcerns + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    var returnValue = JSON.parse(response.d);

                    toastr.success(returnValue[0], "Patient Categorization");

                    $("#<%=stabilityStatus.ClientID%>").val(returnValue[1]);
                    setTimeout(function () { $('#differentiatedModal').modal('hide'); }, 2000);
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    return false;
                }
            });
        }
    


		function getSelectedItemsList(elementId) {
			var x = document.getElementById(elementId);
			var selectedValues = '';
			for (var i = 0; i < x.options.length; i++) {
				if (x.options[i].selected) {
					selectedValues += x.options[i].value + ',';
				}
			}
			return selectedValues;
		}

        var visitByTS = $('#ddlVisitBy').find(":selected").text();

        
		//$("#AppointmentDate").val("");

		if (encounterExists > 0 && visitByTS !=="Treatment Supporter") {
			//var $wizard = $('#myWizard').wizard();
			//var wizard = $wizard.data('wizard');
			//$wizard.off('click', 'li.complete');
			//$wizard.on('click', 'li', $.proxy(wizard.stepclicked, wizard));

			$('#myWizard').wizard();
			$('#myWizard').find('ul.steps li').toggleClass('complete', true);

			$('#myWizard').on('changed.fu.wizard', function (evt, data) {
				$('#myWizard').find('ul.steps li').toggleClass('complete', true);
			});
		}

        getStatusCtrls();
        ICFStatusCtrls();
        ICFAsctionStatusCtrls();
        RegimenStatusCtrls();

        function getStatusCtrls() {
            var selectedIndex = ($("#ddlOnAntiTBDrugs").prop('selectedIndex'));
            var objectsToHide = [];
            var objectsToShow = [];
            var sectionsToReset = [];
            var tbScreenScore = 0;
            if (selectedIndex == 1) {
                objectsToShow = ["tbScreeningOutcomePanel"];
                objectsToHide = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
                sectionsToReset = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
                //tbScreenScore = 3;
            }
            else if (selectedIndex == 2) {
                objectsToShow = ['ICFScreeningSection', 'IPTPanel'];
                // sectionsToReset = ['tbScreeningOutcomePanel'];
            }
            else {
                sectionsToReset = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
                objectsToHide = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
                objectsToShow = [];
            }
            showHideCtrls(objectsToHide, objectsToShow);
            sectionReset(sectionsToReset);
            getTBOutcome(tbScreenScore);
        }

        function ICFStatusCtrls() {
            var totalIndex = 0;
            var objectsToHide = [];
            var objectsToShow = [];
            var sectionsToReset = [];
            $("#ICFScreeningSection select").each(function () {
                var selectedIndex = ($(this).prop('selectedIndex'));
                if (selectedIndex == 1) {
                    totalIndex = totalIndex + 1;
                }
                else {
                    totalIndex = totalIndex + 0;
                }
            });
            if (totalIndex >= 1) {
                objectsToShow = ['ICFActionTakenPanel'];
                objectsToHide = [];
            }
            else {
                objectsToShow = [];
                objectsToHide = ['ICFActionTakenPanel'];
                sectionsToReset = ['ICFActionTakenPanel'];
                //Reset action taken controls
                $("#ICFActionTakenPanel select").each(function () {
                    $(this).prop('selectedIndex', 0);
                });
            }
            showHideCtrls(objectsToHide, objectsToShow);
            sectionReset(sectionsToReset);
        }
        //ICF Action Screening Section Selection Change
        function ICFAsctionStatusCtrls(){
            var IPTScore = 0;
            var TBScreeningScore = 0;
            var TBTreatmentScore = 0;
            var TBOutcomeScore = 0;
            var tbScreenScore = 0;
            var objectsToHide = [];
            var objectsToShow = [];
            var sectionsToReset = [];
            var objectsToDisable = [];
            var objectsToEnable = [];
            $("#ICFActionScreeningSection select").each(function () {
                var selectedIndex = ($(this).prop('selectedIndex'));
                if (selectedIndex == 1) {
                    TBOutcomeScore = TBOutcomeScore + 1;
                    IPTScore = IPTScore + 0;
                    TBTreatmentScore = TBTreatmentScore + 0;
                }
                else if (selectedIndex == 2) {
                    TBOutcomeScore = TBOutcomeScore + 0;
                    IPTScore = IPTScore + 0;
                    TBTreatmentScore = TBTreatmentScore + 1;
                }
                else if (selectedIndex == 3) {
                    TBOutcomeScore = TBOutcomeScore + 0;
                    IPTScore = IPTScore + 1;
                    TBTreatmentScore = TBTreatmentScore + 0;
                }
                else if (selectedIndex == 4) {
                    TBOutcomeScore = TBOutcomeScore + 0;
                    IPTScore = IPTScore + 1;
                    TBTreatmentScore = TBTreatmentScore + 0;
                }
                else {
                    TBOutcomeScore = TBOutcomeScore + 0;
                    IPTScore = IPTScore + 0;
                    TBTreatmentScore = TBTreatmentScore + 0;
                }
            });
            if (TBTreatmentScore >= 1) {
                objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                objectsToShow = ['TuberclosisTreatmentPanel', 'tbScreeningOutcomePanel'];
                tbScreenScore = 2;
            }
            else {
                if (TBOutcomeScore >= 1) {
                    objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                    objectsToHide = ['TuberclosisTreatmentPanel'];
                    objectsToShow = ['tbScreeningOutcomePanel'];
                    tbScreenScore = 2;
                }
                else {
                    if (IPTScore >= 1) {
                        objectsToEnable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                        objectsToHide = ['TuberclosisTreatmentPanel'];
                        objectsToShow = ['tbScreeningOutcomePanel'];
                        tbScreenScore = 1;
                    }
                    else {
                        objectsToEnable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                        objectsToHide = ['TuberclosisTreatmentPanel'];
                        tbScreenScore = 0;
                    }
                }
            }
            showHideCtrls(objectsToHide, objectsToShow);
            sectionReset(sectionsToReset);
            disableEnableCtrls(objectsToDisable, objectsToEnable);
            getTBOutcome(tbScreenScore);
        }
        //start TBRx
       function RegimenStatusCtrls() {
            var tbScreenScore = 0;
            var selectedIndex = ($("#ddlICFRegimen").prop('selectedIndex'));
            var todayDate = new Date();
            var tbrxStartDate = $("#tbTBRXStartDate").val();
            var tbrxEndDate = $("#tbTBRXEndDate").val();
            if (selectedIndex >= 1 && tbrxStartDate != "") {
                if (new Date(tbrxStartDate) <= new Date(todayDate) && new Date(tbrxEndDate) >= new Date(todayDate)) {
                    tbScreenScore = 4;
                }
                else {
                    tbScreenScore = 2;
                }
            }
            
            getTBOutcome(tbScreenScore);
        }
	});


	//Appointment 

    function checkExistingAppointment() {
        var appointmentId = "<%=AppointmentId%>";
		var patientId = "<%=PatientId%>";
		var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
		var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
		var reason = $("#<%=Reason.ClientID%>").val();
		jQuery.support.cors = true;
		$.ajax(
			{
				type: "POST",
				url: "../WebService/PatientService.asmx/GetExistingPatientAppointment",
				data: "{'patientId':'" + patientId + "','appointmentDate': '" + appointmentDate + "','serviceAreaId': '" + serviceArea + "','reasonId': '" + reason + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				async: false,
				cache: false,
                success: function (response) {
                    console.log(response);
					if (response.d != null) {
                        if (isEditAppointment == 'True') {

                        } else {
					        toastr.error("Appointment already exists");
						    return false;
                        }

                    }
                    //if (isEditAppointment == 'True') {
                    //    EditPatientAppointment();
                    //} else {
                    //    addPatientAppointment();
                    //}
					
                    if (response.d != null) {
                        if (appointmentId = JSON.stringify(response.d.AppointmentId))
                        {
                            updateAppointment(appointmentId);
                        }
                        else {
                            toastr.error("Appointment already exists");
                            return false;
                        }
                        //alert(JSON.stringify(response.d.AppointmentId));
                        //updateAppointment(response.d.AppointmentId);
                    }
                    if (appointmentId > 0) {
                        updateAppointment(appointmentId);
                    }
                    else {
                        addPatientAppointment();
                    }
				},
				error: function (msg) {
				    toastr.error(""+msg+"");
				    return false;
				   // alert(msg.responseText);
				}
			});
	}

	function addPatientAppointment() {
		var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
		var reason = $("#<%=Reason.ClientID%>").val();
		var description = $("#<%=description.ClientID%>").val();
		var status = $("#<%=status.ClientID%>").val();
		var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
		/*if (status === '') { status = null }*/
		var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var userId = <%=UserId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
		$.ajax({
			type: "POST",
			url: "../WebService/PatientService.asmx/AddPatientAppointment",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId': " + userId + "}",
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

    function updateAppointment(AppointmentId) {
        var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
        var reason = $("#<%=Reason.ClientID%>").val();
            var description = $("#<%=description.ClientID%>").val();
            var status = $("#<%=status.ClientID%>").val();
            var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
            /*if (status === '') { status = null }*/
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=UserId%>;
            var appointmentid = AppointmentId;
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientService.asmx/UpdatePatientAppointment",
                    data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId':'" + userId + "','appointmentId':'" + appointmentid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        toastr.success(response.d, "Appointment updated successfully");
                        resetFields();
                        setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                    },
                    error: function (response) {
                        toastr.error(response.d, "Appointment not saved");
                    }
                });
    }

	function EditPatientAppointment() {
	    var serviceArea = $("#<%=ServiceArea.ClientID%>").val();
	    var reason = $("#<%=Reason.ClientID%>").val();
	    var description = $("#<%=description.ClientID%>").val();
	    var status = $("#<%=status.ClientID%>").val();
	    var differentiatedCareId = $("#<%=DifferentiatedCare.ClientID%>").val();
	    /*if (status === '') { status = null }*/
	    var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
	    var patientId = <%=PatientId%>;
	    var userId = <%=UserId%>;
	    var patientMasterVisitId = <%=PatientMasterVisitId%>;
	    $.ajax({
	        type: "POST",
	        url: "../WebService/PatientService.asmx/UpdatePatientAppointment",
	        data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','appointmentDate': '" + appointmentDate + "','description': '" + description + "','reasonId': '" + reason + "','serviceAreaId': '" + serviceArea + "','statusId': '" + status + "','differentiatedCareId': '" + differentiatedCareId + "','userId': '" + userId + "','appointmentId':"+isEditAppointmentId+"}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function (response) {
	            toastr.success(response.d, "Appointment Edited successfully");
	            resetAppointmentFields();
	        },
	        error: function (response) {
	            toastr.error(response.d, "Appointment not Edited");
	        }
	    });
	}

	function appointmentCount() {
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
				success: function (response) {
					var count = response.d;
					var message = count + " appointment(s) scheduled on the chosen date.";
					alert(message);
				},

				error: function (msg) {
					alert(msg.responseText);
				}
			});
	}

    function tbInfectedChange() {
        if ($("#tbInfected").val() === 'False') {
			$("#IptForm").show();
			$("#IcfForm").show();
			$("#tbscreeningstatus option").filter(function () { return $(this).text() === 'NoTB'; }).prop('selected', true);
			$("#onIpt").prop("disabled", false);
        } else if ($("#tbInfected").val() === 'True'){
			$("#IptForm").hide();
			$("#IcfForm").hide();
			$("#IcfActionForm").hide();
            $("#tbscreeningstatus option").filter(function () { return $(this).text() === 'TBRx'; }).prop('selected', true);
			$("#onIpt").prop("disabled", true);
            $("#onIpt").val("False");
			//$("#EverBeenOnIpt").prop("disabled", true);
			// $("#EverBeenOnIpt").val("");
        } else {
            $("#IptForm").hide();
            $("#IcfForm").hide();
            $("#IcfActionForm").hide();
            //$("#tbscreeningstatus option").filter(function () { return $(this).text() === 'TBRx'; }).prop('selected', true);
            $("#onIpt").prop("disabled", false);
            $("#onIpt").val("");
            //$("#EverBeenOnIpt").prop("disabled", true);
            // $("#EverBeenOnIpt").val("");
        }

	}

	function onIptChange() {
		if ($("#onIpt").val() === 'False') {
			$("#btnAddIptWorkUp").prop("disabled", false);
			$("#btnAddIptOutcome").prop("disabled", true);
			$("#EverBeenOnIpt").prop("disabled", false);
			$("#EverBeenOnIpt").val("false");
		} else {
			$("#btnAddIptWorkUp").prop("disabled", true);
			$("#btnAddIptOutcome").prop("disabled", false);
			 $("#EverBeenOnIpt").prop("disabled", true);
			 $("#EverBeenOnIpt").val("True");
		}

	}

	function EverBeenOnIptChange() {
		if ($("#EverBeenOnIpt").val() === 'False') {
			$("#onIpt").prop("disabled", false);
			$("#btnAddIptWorkUp").prop("disabled", false);
			$("#btnAddIptOutcome").prop("disabled", true);
			if ($("#tbInfected").val() === 'False') {
				$("#IptForm").show();
			} else {
				$("#IptForm").hide();
			}

		} else {
			$("#onIpt").prop("disabled", true);
			$("#btnAddIptWorkUp").prop("disabled", true);
            $("#btnAddIptOutcome").prop("disabled", true);

            // if client has ever been on IPT and is currently not on IPT and IPT Outcome has never been entered, then prompt for outcome
            if ($("#onIpt").val() == 'False') {
                $("#IptForm").show();
                $("#btnAddIptOutcome").prop("disabled", false);
                $("#btnAddIpt").prop("disabled", true);
            }

		}

	}

	function IcfChange() {
		if (($("#cough").val() === 'True') || ($("#fever").val() === 'True') || ($("#weightLoss").val() === 'True') || ($("#nightSweats").val() === 'True')) {
			$("#IcfActionForm").show();
			$("#tbscreeningstatus option").filter(function () { return $(this).text() === 'PrTB'; }).prop('selected', true);
		} else {
			$("#IcfActionForm").hide();
			$("#tbscreeningstatus option").filter(function () { return $(this).text() === 'NoTB'; }).prop('selected', true);
			$("#btnAddIptWorkUp").prop("disabled", false);
			$("#btnAddIpt").prop("disabled", false);
			$("#sputum").val("");
			$("#chest").val("");
			$("#antiTb").val("");
			$("#contactsInvitation").val("");
			$("#iptEvaluation").val("");

		}
	}

	function IcfActionChange() {
		if (($("#sputum").val() === '1') || ($("#sputum").val() === '2') || ($("#geneXpert").val() === '1') || ($("#geneXpert").val() === '2') || ($("#chest").val() === '1') || ($("#chest").val() === '2') || ($("#antiTb").val() === '1') || ($("#contactsInvitation").val() === '1') || ($("#iptEvaluation").val() === '1')) {
			$("#btnAddIptWorkUp").prop("disabled", true);
			$("#btnAddIpt").prop("disabled", true);
			$("#tbscreeningstatus option").filter(function () { return $(this).text() === 'PrTB'; }).prop('selected', true);
		} else {
			$("#btnAddIptWorkUp").prop("disabled", false);
			$("#btnAddIpt").prop("disabled", false);
			$("#tbscreeningstatus option").filter(function () { return $(this).text() === 'NoTB'; }).prop('selected', true);
		}
	}

	function IptWorkUp() {
		$("#IptClientWorkupForm").show();
		$("#IptDetailsForm").hide();
		$("#IptOutcomeDetailsForm").hide();
	}

	function Ipt() {
		$("#IptClientWorkupForm").hide();
		$("#IptDetailsForm").show();
		$("#IptOutcomeDetailsForm").hide();
	}

	function IptOutcome() {
		$("#IptOutcomeDetailsForm").show();
		$("#IptClientWorkupForm").hide();
		$("#IptDetailsForm").hide();
	}

	function loadPresentingComplaints() {
		var pcInput = document.getElementById('<%= txtPresentingComplaints.ClientID %>');
		var awesomplete = new Awesomplete(pcInput, {
			minChars: 1
		});

		document.getElementById('<%= txtPresentingComplaints.ClientID %>').addEventListener('awesomplete-selectcomplete', function () {
			var result = this.value.split("~");
			$("#<%=txtPresentingComplaintsID.ClientID%>").val(result[0]);
			$("#<%=txtPresentingComplaints.ClientID%>").val(result[1]);
		});

		$.ajax({
			type: "POST",
			url: "../WebService/PatientEncounterService.asmx/GetPresentingComplaints",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				var PCList = [];

				for (var i = 0; i < serverData.length; i++) {
					//drugList.push(serverData[i][1]);
					PCList.push({ label: serverData[i][1], value: serverData[i][0] });
				}
				awesomplete.list = PCList;
			}
		});

	}

	//adverse Event TypeAhead
	function loadAdverseEvents() {
		var pcInput = document.getElementById('<%= adverseEvent.ClientID %>');
		var awesomplete = new Awesomplete(pcInput, {
			minChars: 1
		});

		document.getElementById('<%= adverseEvent.ClientID %>').addEventListener('awesomplete-selectcomplete', function () {
			var result = this.value.split("~");
			$("#<%=adverseEventId.ClientID%>").val(result[0]);
			$("#<%=adverseEvent.ClientID%>").val(result[1]);
			adverseEventName = result[1];
			adverseEventId = result[0];

		});

		$.ajax({
			type: "POST",
			url: "../WebService/PatientAdverseEventOutcome.asmx/GetAdverseEvent",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				var PCList = [];

				for (var i = 0; i < serverData.length; i++) {
					//drugList.push(serverData[i][1]);
					PCList.push({ label: serverData[i][1], value: serverData[i][0] });
				}
				awesomplete.list = PCList;
			}
		});

	}

	function loadAllergies() {
		var pcInput = document.getElementById('<%= txtAllergy.ClientID %>');
		var awesomplete = new Awesomplete(pcInput, {
			minChars: 1
		});

		document.getElementById('<%= txtAllergy.ClientID %>').addEventListener('awesomplete-selectcomplete', function () {
			var result = this.value.split("~");
			$("#<%=txtAllergyId.ClientID%>").val(result[0]);
			$("#<%=txtAllergy.ClientID%>").val(result[1]);
		});

		$.ajax({
			type: "POST",
			url: "../WebService/PatientEncounterService.asmx/loadAllergies",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				var PCList = [];

				for (var i = 0; i < serverData.length; i++) {
					//drugList.push(serverData[i][1]);
					PCList.push({ label: serverData[i][1], value: serverData[i][0] });
				}
				awesomplete.list = PCList;
			}
		});

	}

	function loadAllergyReactions() {
		var pcInput = document.getElementById('<%= txtReactionType.ClientID %>');
		var awesomplete = new Awesomplete(pcInput, {
			minChars: 1
		});

		document.getElementById('<%= txtReactionType.ClientID %>').addEventListener('awesomplete-selectcomplete', function () {
			var result = this.value.split("~");
			$("#<%=txtReactionTypeID.ClientID%>").val(result[0]);
				   $("#<%=txtReactionType.ClientID%>").val(result[1]);
		});

		$.ajax({
			type: "POST",
			url: "../WebService/PatientEncounterService.asmx/loadAllergyReactions",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				var PCList = [];

				for (var i = 0; i < serverData.length; i++) {
					//drugList.push(serverData[i][1]);
					PCList.push({ label: serverData[i][1], value: serverData[i][0] });
				}
				awesomplete.list = PCList;
			}
		});

	}

    function loadDiagnosis() {

       <%-- $("#Diagnosis").select2({
            placeholder: {
                id: '0',
                text: 'select an option'
            },
            minimumInputLength: 3,
            allowClear: true,
            data:DiagnosisList
            ajax: {
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/loadDiagnosis",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: function (params) { // page is the one-based page number tracked by Select2
                    return {
                        q: params.term, //search term
                        page: params.page, // page number

                    };
                },
                processResults: function (data, params) {
                    var serverData = data.d;
                    params.page = params.page || 1;
                    var DiagnosisList = [];

                    for (var i = 0; i < serverData.length; i++) {
                        //drugList.push(serverData[i][1]);
                        DiagnosisList.push({ label: serverData[i][1], value: serverData[i][0] });
                    }
                    dataObj = new Array();
                    for (var i = 0; i < DiagnosisList.length:i++)
        {
            if (DiagnosisList[i].value == params.term) {
                dataObj.push({ label=DiagnosisList[i].label, value=DiagnosisList[i].value });
            }
        }
        params.page = params.page || 1;
        return {
            results: dataObj,
            pagination: {
                more: (params.page * 30) < data.total_count
            }
        }
    }
   

     
		var diagnosisInput = document.getElementById('<%= Diagnosis.ClientID %>');
        var awesomplete = new Awesomplete(diagnosisInput, {
            minChars: 1,
        
      
          
                replace: function (suggestion) {
                    this.input.value = suggestion.label;
                    // default replace() inserts suggestion.value to input
                    var result = suggestion.value.split("~");
                    $("#<%=txtDiagnosisID.ClientID%>").val(result[0] + "~" + result[1]);
                     $("#<%=Diagnosis.ClientID%>").val(result[2]);
                     diagnosisInput.value = suggestion.value;
                     suggestion.value = "";

                },

            });
        $("#<%=Diagnosis.ClientID%>").on('click', function () {
            this.addEventListener('awesomplete - selectcomplete', function () {
                var result = this.value.split("~");
                $("#<%=txtDiagnosisID.ClientID%>").val(result[0] + "~" + result[1]);
                $("#<%=Diagnosis.ClientID%>").val(result[2]);
            });
        });

        document.getElementById('<%= Diagnosis.ClientID %>').setAttribute('awesomplete-selectcomplete', function () {
            var result = this.value.split("~");
            $("#<%=txtDiagnosisID.ClientID%>").val(result[0] + "~" + result[1]);
            $("#<%=Diagnosis.ClientID%>").val(result[2]);
        });
		document.getElementById('<%= Diagnosis.ClientID %>').addEventListener('awesomplete-selectcomplete', function () {
			var result = this.value.split("~");
			$("#<%=txtDiagnosisID.ClientID%>").val(result[0]+ "~"+result[1]);
				   $("#<%=Diagnosis.ClientID%>").val(result[2]);
        });
        
     --%>

	$.ajax({
			type: "POST",
			url: "../WebService/PatientEncounterService.asmx/loadDiagnosis",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				//var DiagnosisList = [];

				for (var i = 0; i < serverData.length; i++) {
					//drugList.push(serverData[i][1]);
					DiagnosisList.push({ id: serverData[i][0], text: serverData[i][1] });
                }

                LoadDiagnosisList(DiagnosisList);
              
				//awesomplete.list = DiagnosisList;
			}
		});

    }
    function LoadDiagnosisList(Array) {
        $("#Diagnosis").select2({
            placeholder: {
                id: '0',
                text: 'select an option'

            },
            allowClear: true,
            minimumInputLength: 2,
            
            data: DiagnosisList

        });

        $("#Diagnosis").select2("val", "0");
        $("#Diagnosis").trigger('change.select2');
    }
   // $("#Diagnosis").select2("val", "0");
    //$("#Diagnosis").trigger('change.select2');
	function loadSystemReviews() {
		var systemReviewName = $('#ddlExaminationType').find(":selected").text();

		$.ajax({
			type: "POST",
			url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
			data: "{'masterName': '" + systemReviewName + "'}",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				var obj = $.parseJSON(serverData);

				$("#<%=ddlExamination.ClientID%>").find('option').remove().end();
				$("#<%=ddlExamination.ClientID%>").append('<option value="0">Select</option>');
				for (var i = 0; i < obj.length; i++) {
					$("#<%=ddlExamination.ClientID%>").append('<option value="' + obj[i]["ItemId"] + '">' + obj[i]["DisplayName"] + '</option>');
					   }
			}
		});
	}

    
	function showHidePresentingComplaintsDivs() {
		var anyComplaints = $("input[name$=anyComplaints]:checked").val();
		if (anyComplaints ==1) {
			document.getElementById('presentingComplaintsCtrls').style.display = 'block';
			document.getElementById('presentingComplaintsTable').style.display = 'block';
			document.getElementById('presentingComplaintsNotes').style.display = 'block';
		}
		else {
			document.getElementById('presentingComplaintsCtrls').style.display = 'none';
			document.getElementById('presentingComplaintsTable').style.display = 'none';
			document.getElementById('presentingComplaintsNotes').style.display = 'none';
		}
	}

	function showHideAdverseEventsDivs() {
		var adverseEvents = $("input[name$=adverseEvents]:checked").val();

		if (adverseEvents == 1) {
			document.getElementById('adverseEventCtrls').style.display = 'block';
			document.getElementById('adverseEventsTable').style.display = 'block';
		}
		else {
			document.getElementById('adverseEventCtrls').style.display = 'none';
			document.getElementById('adverseEventsTable').style.display = 'none';
		}
	}

	function showHideSystemsOkayDivs() {
		var systems = $("input[name$=systemsOkay]:checked").val();
		if (systems == 1 || systems == undefined) {
			$("#systemsOkayCtrls").hide();
			$('.errorBlock').hide();
		}
		else {
			$("#systemsOkayCtrls").show();
			$('.errorBlock').hide();
		}
	}

	function showHideVisitByTS() {
		var visitByTS = $('#ddlVisitBy').find(":selected").text();

		if (visitByTS === "Treatment Supporter") {
			document.getElementById('divTreatmentSupporter').style.display = 'block';
			document.getElementById('step1Div').style.display = 'none';
			document.getElementById('prevNextButton').style.display = 'none';
		}
		else {
			document.getElementById('divTreatmentSupporter').style.display = 'none';
			document.getElementById('step1Div').style.display = 'block';
			document.getElementById('prevNextButton').style.display = 'block';
		}
	}

	function savePatientEncounterTS() {
		var visitDate = $("#<%=VisitDate.ClientID%>").val();
		var visitScheduled = $("input[name$=Scheduled]:checked").val();
		var visitBy = $("#<%=ddlVisitBy.ClientID%>").find(":selected").val();

			   $.ajax({
				   type: "POST",
				   url: "../WebService/PatientEncounterService.asmx/savePatientEncounterTS",
				   data: "{'VisitDate':'" + visitDate + "','VisitScheduled':'" + visitScheduled + "','VisitBy':'" + visitBy + "'}",
				   contentType: "application/json; charset=utf-8",
				   dataType: "json",
				   success: function (response) {

					   console.log(response.d);
					   if (response.d > 0) {
						   toastr.success(response.d, "Presenting Complaints");

						   setTimeout(function () {
							   window.location
								   .href =
							'<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
						   },
							   2000);
					   }

					   else

						   toastr.error(response.d, "Error occured while saving Presenting Complaints");
				   },
				   error: function (response) {

					   toastr.error(response.d, "Error occured while saving Presenting Complaints");
				   }
			   });
	}

	function cblGeneralExaminationChange() {
		var cblGeneralExamination = document.getElementById("cblGeneralExamination");
		var checkOptions =   cblGeneralExamination.getElementsByTagName('input');
		var checkedValues = null;

		for(var i = 0; i < checkOptions.length; i++)
		{
			var checkBoxRef = checkOptions[i];
			if (checkBoxRef.checked === true) {
				var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');
				checkedValues = labelArray[0].innerHTML;
				if (checkedValues === "None" && isNoneChecked == false) {
					isNoneChecked = true;
					CheckAll();
				}
			} else {
				var labelArrayUnchecked = checkBoxRef.parentNode.getElementsByTagName('label');
				checkedValues = labelArrayUnchecked[0].innerHTML;
				if (checkedValues === "None" && isNoneChecked === true) {
					isNoneChecked = false;
					UnCheckAll();
				}
			}
		}
	}

	function CheckAll() {
		var listBox = document.getElementById("cblGeneralExamination");
		var inputItems = listBox.getElementsByTagName("input");

		for(var i = 0; i < inputItems.length; i++) {
			var opt = inputItems[i];
			var labelArray = opt.parentNode.getElementsByTagName('label');
			if (labelArray.length > 0) {
				if (labelArray[0].innerHTML !== "None") {
					opt.disabled = true;
					opt.checked=false;
				}
			}
		}
	}

	function UnCheckAll() {
		var chkControlId = document.getElementById("cblGeneralExamination");
		var options = chkControlId.getElementsByTagName('input');

		for(var i = 0; i < options.length; i++)
		{
			var opt = options[i];
			var labelArray = opt.parentNode.getElementsByTagName('label');
			if (labelArray.length > 0) {
				opt.disabled = false;
			}
		}
	}


	function GetPatientExaminationTypeID() {
		$.ajax({
			type: "POST",
			url: "../WebService/LookupService.asmx/GetMasterIdByMasterName",
			data: "{'groupName': 'ReviewOfSystems' }",
			dataType: "json",
			contentType: "application/json; charset=utf-8",

			success: function (data) {
				var serverData = data.d;
				var obj = $.parseJSON(serverData);
				$("#hfExaminationReviewSystems").val(obj);
			}
		});
    }
    
    function addSexualHistory() {
        var arrHRB = [];

        arrHRB = GetHighRiskBehaviour();

        



        var ddlPartnerStatus = $("#ddlPartnerStatus").val();

        if (ddlPartnerStatus == "" || ddlPartnerStatus == null || ddlPartnerStatus == 'undefined' || ddlPartnerStatus == 0) {
            toastr.error("Error", "Please enter Partner Status");
            return false;
        }
        else {
            var partnerStatus = $("#ddlPartnerStatus").find('option:selected').text();
            if (ddlPartnerStatus == null) {
                ddlPartnerStatus = 0;
            }
        }

        var ddlPartnerGender = $("#ddlPartnerGender").val();
        if (ddlPartnerGender == "" || ddlPartnerGender == null || ddlPartnerGender == 'undefined' || ddlPartnerGender == 0) {

            toastr.error("Error", "Please enter Partner Gender");
            return false;
        }
        else {
            var GenderText = $('#ddlPartnerGender').find('option:selected').text();
            if (ddlPartnerGender == null) {
                ddlPartnerGender = 0;
            }
        }
        var ddlSexualOrientation = $("#ddlSexualOrientation").val();
        if (ddlSexualOrientation == "" || ddlSexualOrientation == null || ddlSexualOrientation == 'undefined' || ddlSexualOrientation == 0) {
            toastr.error("Error", "Please enter Partner Sexual Orientation");
            return false;
        }
        else {
            var SexualOrientation = $("#ddlSexualOrientation").find('option:selected').text();
            if (ddlSexualOrientation == null) {
                ddlSexualOrientation = 0;
            }
        }
        var selectedhistory = {
            count: "0",
            PartnerStatus: partnerStatus,
            Gender: GenderText,
            SexualOrientation: SexualOrientation,
            HighRisk: arrHRB
        }
        var history = {
            uniqueid: 0,
            id:0,
            PartnerStatus: ddlPartnerStatus,
            Gender: ddlPartnerGender,
            SexualOrientation: ddlSexualOrientation,
            DeleteFlag :false,
            HighRisk: arrHRB
        }
        var res = 0;
        var hist = 0;
        console.log('History')

        //console.log(history);
       // console.log(selectedhistory);
     //   console.log(arrHRB);
        if (arrSexualHistory.length > 0) {
            hist = arrSexualHistory.length;
            hist = hist + 1;
            history.uniqueid = hist.toString();
        }
        else {

            hist = hist + 1;
            history.uniqueid=hist.toString();
         

        }
        arrSexualHistory.push(history);
        if (arrTextSexualHistory.length > 0) {
            res = arrTextSexualHistory.length;
            res = res + 1;
        
            noofpartners = parseInt(noofpartners) + 1;
            selectedhistory.count = res.toString();
        }
        else {
            
            res = res + 1;
            
     
            noofpartners = parseInt(noofpartners) + 1;
            selectedhistory.count =res.toString();
        }
        arrTextSexualHistory.push(selectedhistory);
        console.log(arrTextSexualHistory);
        if (!(arrTextSexualHistory == null)) {

            $('#txtPartners').val(noofpartners.toString());
        }
        else {
            $('#txtPartners').val("");
        }
        $("#dtlSexualHistory>tbody:first").empty();
        if (!(selectedhistory == null)) {
            var i;
            var t;
            var str = "";
           
            for (i = 0; i < arrTextSexualHistory.length; i++) {
               
               
                console.log(arrTextSexualHistory[i].Gender);

                console.log(arrTextSexualHistory[i].HighRisk)
               str= arrTextSexualHistory[i].HighRisk.map(function (elem) {
                    return elem.value;
                }).join(",");
                //for (t = 0; t < arrTextSexualHistory[i].HighRisk.length; t++) {
                //    console.log(arrTextSexualHistory[i].HighRisk[t].value);
                //    var value;
                //    if (!(arrTextSexualHistory[i].HighRisk[t].value == null || arrTextSexualHistory[i].HighRisk[t].value == "")) {

                //        str =  arrTextSexualHistory[i].HighRisk[t].value;
                //        str += ",";
                //    }

                //}

               var table = "<tr><td align='left'  >" +
                   arrTextSexualHistory[i].count +
                    "</td><td align='left'>" +
                    arrTextSexualHistory[i].PartnerStatus
                    +
                    "</td><td align='left'>" +
                    arrTextSexualHistory[i].Gender
                    +
                    "</td><td align='left'>" +
                    arrTextSexualHistory[i].SexualOrientation
                    +
                    "</td><td align='left' >" +
                    str +
                    "</td><td align='right'><button type='button' class='btnSexualHistoryDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                $("#dtlSexualHistory>tbody:first").append('' + table + '');

            }

          

        }


        $("#ddlPartnerStatus").val(0);
        $("#ddlPartnerGender").val("");
        $("#ddlSexualOrientation").val(0);
        $("#ddlHighRiskBehaviour").select2("val", "0");
        
    }
        
   
   
    function loadHighRiskBehavior(MasterName) {
        $.ajax({
            type: "POST",
            url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
            data: "{'masterName':'" + MasterName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var itemList = JSON.parse(response.d);
                BindHighRiskBehavior(itemList);

            },
            error: function (response) {

                toastr.error(response.d, "Error occured while presenting HighRiskBehaviour");
            }
        });
    }
	
	
    function saveNutritionAssessment()
    {
        $("#nutritionscreeningsection .narbList").each(function () {
            var screeningValue = 0;
            var screeningType = <%=screenTypeId%>;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=userId%>;
            var screeningCategory = $(this).attr('id').replace('nutritionarb', '');
            var rdIdValue = $(this).attr('id');
            var checkedValue = $('#' + rdIdValue + ' input[type=radio]:checked').val();
            if (typeof checkedValue != 'undefined') {
                screeningValue = checkedValue;
            }
            $.ajax({
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/AddUpdateScreeningData",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','screeningType':'" + screeningType + "','screeningCategory':'" + screeningCategory + "','screeningValue':'" + screeningValue + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    error = 0;
                },
                error: function (response) {
                    error = 1;
                }
            });
        });
        $("#nutritionscreeningsection textarea").each(function () {
            var categoryId = ($(this).attr('id')).replace('nutritionatb', '');
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var clinicalNotes = $(this).val();
            var serviceAreaId = 203;
            var userId = <%=userId%>;
            if (categoryId > 1) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientClinicalNotesService.asmx/addPatientClinicalNotes",
                    data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','serviceAreaId':'" + serviceAreaId + "','notesCategoryId':'" + categoryId + "','clinicalNotes':'" + clinicalNotes + "','userId':'" + userId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        error = 0;
                    },
                    error: function (response) {
                        error = 1;
                    }
                });
            }
        });
        if (error == 0) {
            toastr.success("Nutrition Assessment Saved");
        }
    }
    function LoadIPTHistory()
    {
   varIPTHistoryTable = $('#dtlIPTHistory').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/PatientTbService.asmx/GetPatientIPTHistory",
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
                    "visible": true,
                    "searchable": false
                }
            ]
        });
    }

    function GetGBVScreeningStatus() {
        var patientId ="<%=PatientId%>";
        var visitDate = moment("<%=visitdateval%>");
        var screeningCategoryId = "<%=GbvScreeningCategoryId%>";

        if (visitDate.isValid()) {
            visitDate = visitDate.format('YYYY-MM-DD');

            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/getPatientScreening",
                data: "{'patientId':'" + patientId + "', 'visitDate': '" + visitDate + "', 'screeningcategoryId': '" + screeningCategoryId + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",

                success: function (response) {
                    var itemList = JSON.parse(response.d);

                    $("#lblGbvAssessmentDone").text(itemList.length > 0 ? 'Yes' : 'No');

                }
            });
        } else {

            $("#lblGbvAssessmentDone").text('No');

        }
    }

    GetGBVScreeningStatus();

    $(".icfdate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });

    function showHideCtrls(objectsToHide, objectsToShow) {
        $.each(objectsToHide, function (index, value) {
            $("#" + value).hide();
        });
        $.each(objectsToShow, function (index, value) {
            $("#" + value).show();
        });
    }

    function sectionReset(sectionsToReset) {
        $.each(sectionsToReset, function (index, value) {
            $("#" + value + " select").each(function () {
                $(this).prop('selectedIndex', 0);
            });
        });
    }

    function disableEnableCtrls(objectsToDisable, objectsToEnable) {
        $.each(objectsToDisable, function (index, value) {
            $("#" + value).prop('disabled', true);
        });
        $.each(objectsToEnable, function (index, value) {
            $("#" + value).prop('disabled', false);
        });
    }

    function getTBOutcome(tbScreenScore) {
        // $("#ddlICFTBScreeningOutcome").prop('selectedIndex', tbScreenScore);
    }

    //Currently on Anti-TB Drugs selection change
    $('#ddlOnAntiTBDrugs').change(function () {
        var selectedIndex = ($(this).prop('selectedIndex'));
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        var tbScreenScore = 0;
        if (selectedIndex == 1) {
            objectsToShow = ["tbScreeningOutcomePanel"];
            objectsToHide = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
            sectionsToReset = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
            tbScreenScore = 3;
        }
        else if (selectedIndex == 2) {
            objectsToShow = ['ICFScreeningSection', 'IPTPanel'];
            // sectionsToReset = ['tbScreeningOutcomePanel'];
        }
        else {
            sectionsToReset = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
            objectsToHide = ['ICFScreeningSection', 'TubeclosisTreatmentPanel', 'IPTPanel', 'ICFActionTakenPanel'];
            objectsToShow = [];
        }

        showHideCtrls(objectsToHide, objectsToShow);
        sectionReset(sectionsToReset);
        getTBOutcome(tbScreenScore);
    });

    $("#ddlICFCough, #ddlICFFever, #ddlICFWeight, #ddlICFNightSweats").change(function () { 
        var cough = $("#<%=ddlICFCough.ClientID%>").find(":selected").text();
        var fever = $("#<%=ddlICFFever.ClientID%>").find(":selected").text();
        var weightLoss = $("#<%=ddlICFWeight.ClientID%>").find(":selected").text();
        var nightSweats = $("#<%=ddlICFNightSweats.ClientID%>").find(":selected").text();

        cough = cough == "Select" ? "" : cough;
        fever = fever == "Select" ? "" : fever;
        weightLoss = weightLoss == "Select" ? "" : weightLoss;
        nightSweats = nightSweats == "Select" ? "" : nightSweats;

        tbScreeningOutcome(cough, fever, weightLoss, nightSweats);
	});

    function tbScreeningOutcome(cough, fever, weightLoss, nightSweats) {
        getTBOutcome(0);
        if (cough == "yes" || fever == "yes" || weightLoss == "yes" || nightSweats == "yes") {
            getTBOutcome(2);
        } else if(cough == "No" && fever == "No" && weightLoss == "No" && nightSweats == "No") {
            getTBOutcome(1);
        } 
    }

    //ICF Screening selection change
    $("#ICFScreeningSection select").change(function (evt, data) {
        var totalIndex = 0;
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        $("#ICFScreeningSection select").each(function () {
            var selectedIndex = ($(this).prop('selectedIndex'));
            if (selectedIndex == 1) {
                totalIndex = totalIndex + 1;
            }
            else {
                totalIndex = totalIndex + 0;
            }
        });
        if (totalIndex >= 1) {
            objectsToShow = ['ICFActionTakenPanel'];
            objectsToHide = [];
        }
        else {
            objectsToShow = [];
            objectsToHide = ['ICFActionTakenPanel'];
            sectionsToReset = ['ICFActionTakenPanel'];
            //Reset action taken controls
            $("#ICFActionTakenPanel select").each(function () {
                $(this).prop('selectedIndex', 0);
            });
        }
        showHideCtrls(objectsToHide, objectsToShow);
        sectionReset(sectionsToReset);
    });
    //ICF Action Screening Section Selection Change
    $("#ICFActionScreeningSection select").change(function (evt, data) {
        var IPTScore = 0;
        var TBScreeningScore = 0;
        var TBTreatmentScore = 0;
        var TBOutcomeScore = 0;
        var tbScreenScore = 0;
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        var objectsToDisable = [];
        var objectsToEnable = [];
        $("#ICFActionScreeningSection select").each(function () {
            var selectedIndex = ($(this).prop('selectedIndex'));
            if (selectedIndex == 1) {
                TBOutcomeScore = TBOutcomeScore + 1;
                IPTScore = IPTScore + 0;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
            else if (selectedIndex == 2) {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 0;
                TBTreatmentScore = TBTreatmentScore + 1;
            }
            else if (selectedIndex == 3) {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 1;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
            else if (selectedIndex == 4) {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 1;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
            else {
                TBOutcomeScore = TBOutcomeScore + 0;
                IPTScore = IPTScore + 0;
                TBTreatmentScore = TBTreatmentScore + 0;
            }
        });
        if (TBTreatmentScore >= 1) {
            objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
            objectsToShow = ['TuberclosisTreatmentPanel', 'tbScreeningOutcomePanel'];
            tbScreenScore = 2;
        }
        else {
            if (TBOutcomeScore >= 1) {
                objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                objectsToHide = ['TuberclosisTreatmentPanel'];
                objectsToShow = ['tbScreeningOutcomePanel'];
                tbScreenScore = 2;
            }
            else {
                if (IPTScore >= 1) {
                    objectsToEnable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                    objectsToHide = ['TuberclosisTreatmentPanel'];
                    objectsToShow = ['tbScreeningOutcomePanel'];
                    tbScreenScore = 1;
                }
                else {
                    objectsToEnable = ['btnAddIptWorkUp2', 'btnAddIpt2'];
                    objectsToHide = ['TuberclosisTreatmentPanel'];
                    tbScreenScore = 0;
                }
            }
        }
        showHideCtrls(objectsToHide, objectsToShow);
        sectionReset(sectionsToReset);
        disableEnableCtrls(objectsToDisable, objectsToEnable);
        getTBOutcome(tbScreenScore);
    });
    //start TBRx
    $("#ddlICFRegimen").change(function (evt, data) {
        var tbScreenScore = 0;
        var selectedIndex = ($(this).prop('selectedIndex'));
        var todayDate = new Date();
        var tbrxStartDate = $("#tTBRXStartDate").val();
        var tbrxEndDate = $("#tbTBRXEndDate").val();
        if (selectedIndex >= 1 && tbrxStartDate != "") {
            if (new Date(tbrxStartDate) <= new Date(todayDate) && new Date(tbrxEndDate) >= new Date(todayDate)) {
                tbScreenScore = 4;
            }
            else {
                tbScreenScore = 2;
            }
        }
        else {
            tbScreenScore = 2;
        }
        getTBOutcome(tbScreenScore);
    }); 
    $("#ddlICFCurrentlyOnIPT").change(function (evt, data) {
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        var objectsToDisable = [];
        var objectsToEnable = [];
        var selectedIndex = ($(this).prop('selectedIndex'));
        if (selectedIndex == 1) {
            objectsToDisable = ['btnAddIptWorkUp2'];
            objectsToHide = ['startIptSection'];
            objectsToEnable = ['btnAddIpt2', 'btnAddIptOutcome2'];
        }
        else if (selectedIndex == 2) {
            objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2', 'btnAddIptOutcome2'];
            objectsToShow = ['startIptSection'];
        }
        else {
            objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2', 'btnAddIptOutcome2'];
            objectsToHide = ['startIptSection'];
        }
        disableEnableCtrls(objectsToDisable, objectsToEnable);
        showHideCtrls(objectsToHide, objectsToShow);
    }); 
    $("#ddlICFStartIPT").change(function (evt, data) {
        var objectsToHide = [];
        var objectsToShow = [];
        var sectionsToReset = [];
        var objectsToDisable = [];
        var objectsToEnable = [];
        var selectedIndex = ($(this).prop('selectedIndex'));
        if (selectedIndex == 1) {
            objectsToDisable = ['btnAddIpt2', 'btnAddIptOutcome2'];
            objectsToEnable = ['btnAddIptWorkUp2'];
        }
        else if (selectedIndex == 2) {
            objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2', 'btnAddIptOutcome2'];
        }
        else {
            objectsToDisable = ['btnAddIptWorkUp2', 'btnAddIpt2', 'btnAddIptOutcome2'];
        }
        disableEnableCtrls(objectsToDisable, objectsToEnable);
        showHideCtrls(objectsToHide, objectsToShow);
    });
    function BindHighRiskBehavior(itemList) {
       // data = [];
        $.each(itemList, function (index, value) {
            arrHighRisk.push({ id: value.ItemId, text: value.ItemDisplayName });
        });
        

        $("#ddlHighRiskBehaviour").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: arrHighRisk
        });
        $("#ddlHighRiskBehaviour").select2("val", "0");
        $("#ddlHighRiskBehaviour").trigger('change.select2');
    }
    function GetHighRiskBehaviour() {
        var ddlHighRiskBehaviour = $("#ddlHighRiskBehaviour").select2("val");
        var arrHRB = [];
        if (ddlHighRiskBehaviour !== null) {
            if (ddlHighRiskBehaviour.length > 0) {
                if (jQuery.isEmptyObject(ddlHighRiskBehaviour) == false) {
                    $.each(ddlHighRiskBehaviour, function (index, arrD) {
                        if (jQuery.isEmptyObject(arrD) == false) {
                            var Risk = arrHighRisk.find(s => s.id == arrD);
                            arrHRB.push({ id: arrD ,value:Risk.text});
                        }
                    });
                }
            }
            else {
                arrHRB.push({ id: 0,value:'null' });
            }
        }
        else {
            arrHRB.push({ id: 0,value:'null' });
        }

        return arrHRB;
    }

    function loadWhoStageOIS(WhoStage,id,title) {
        $.ajax({
            type: "POST",
            url: "../WebService/LookupService.asmx/GetLookUpItemViewByMasterName",
            data: "{'masterName':'" + WhoStage + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var itemList = JSON.parse(response.d);
                console.log(itemList,WhoStage);
                
                BindWhoStage(itemList,WhoStage,id,title);

            },
            error: function (response) {

                toastr.error(response.d, "Error occured while presenting staging");
            }
        });
    }
    function GetPatientOIS() {
        $.ajax({
            type: "POST",
            url: "../WebService/PatientEncounterService.asmx/GetPatientOIs",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = response.d;
                PatientOIData = obj;
            
                AssignTableData("dtlStageI", obj);
                AssignTableData("dtlStageII", obj);
                AssignTableData("dtlStageIII", obj);
                AssignTableData("dtlStageIV", obj);
            },
            error: function (response) {

                toastr.error(response.d, "Error Loading the Patient OIS");
            }


        });
            
    }

    function savePatientOIS() {
        OIdata.length = 0;
      

        GetPatientOIList("dtlStageI");
       GetPatientOIList("dtlStageII");
        GetPatientOIList("dtlStageIII");
        GetPatientOIList("dtlStageIV");



        $.ajax({
            type: "POST",
            url: "../WebService/PatientEncounterService.asmx/SavePatientOI",
            data: "{'data':'" + JSON.stringify(OIdata) + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Save Patient OIS");
            },
            error: function (response) {
                toastr.error(response.d, "Save Patient OIS Error");
            }
        });

    }

    function GetPatientOIList(dataTableName) {


      
        var rows = $("#" + dataTableName).dataTable().fnGetNodes();
        for (var i = 0; i < rows.length; i++) {

            // Get HTML of 3rd column (for example)
            var col0 = $(rows[i]).find("td:eq(0)").html();
            var hidV = $(col0).attr("value");
            var chkVal = $("#chkAchieved" + hidV).prop('checked');
            var current = $("#dtCurrent" + hidV).val();
            var col2 = $(rows[i]).find("td:eq(2)").html();
            var col3 = $(rows[i]).find("td:eq(3)").html()
            for (var t = 0; t <PatientOIData.length; t++) {
                if (PatientOIData[t].OIId == hidV && PatientOIData[t].DeleteFlag == false) {
                 var dt = new Array();
                 if (chkVal == false) {
                     dt = {
                         "OI": hidV,
                         "Checked": chkVal,
                         "Current": current,
                         "DeleteFlag": true
                     }
                     OIdata.push(dt);
                 }
                 }
                }


            if (chkVal === true) {
                var data = new Array();
                data = {
                    "OI": hidV,
                    "Checked": chkVal,
                    "Current": current,
                    "DeleteFlag": false
                }
                OIdata.push(data);
            }

        }
    }
    function AssignTableData(dataTableName, obj) {
            var dtData = obj;
            var rows = $("#" + dataTableName).dataTable().fnGetNodes();
            for (var i = 0; i < rows.length; i++) {

                // Get HTML of 3rd column (for example)
                var col0 = $(rows[i]).find("td:eq(0)").html();
                var hidV = $(col0).attr("value");
                var chkVal = $("#chkAchieved" + hidV).prop('checked');
                var col2 = $(rows[i]).find("td:eq(2)").html();
                var col3 = $(rows[i]).find("td:eq(3)").html();

                for (var t = 0; t < dtData.length; t++) {
                    if (dtData[t].OIId == hidV && dtData[t].DeleteFlag == false) {
                        // var filterDate = $.grep(dtData[t], function (e) { return e.OIId == hidV && e.Deleteflag == false; });
                        // if (jQuery.isEmptyObject(filterDate) == false) {
                        $("#chkAchieved" + hidV).prop('checked', true);

                        CheckDatenAssign(dtData[t].Current, "dtCurrent" + hidV, false);
                        // }
                    }
                }
            }
        }

    function CheckDatenAssign(dtVal, ctrl, IsDisableCheck) {
            var parsedDate;
            var jsDate;
            if (IsDisableCheck) {
                if (dtVal != null) {
                    parsedDate = new Date(parseInt(dtVal.substr(6)));
                    jsDate = new Date(parsedDate); //Date object
                    //console.log(jsDate);
                    // $("#" + ctrl).datepicker('setDate', jsDate);
                    $("#" + ctrl).datetimepicker({
                        format: 'YYYY-MM-DD',
                        date: jsDate,
                        calendarWeeks: true,


                        showClose: true,
                        showClear: true,

                        inline: true,
                        hide: true,
                   
                    });


                    $("#" + ctrl).data("DateTimePicker").hide();
                    $("#" + ctrl).prop('disabled', true);
                }
                else {
                    $("#" + ctrl).prop('disabled', false);
                }
            }
            else {
                if (dtVal != null) {
                    parsedDate = new Date(parseInt(dtVal.substr(6)));
                    jsDate = new Date(parsedDate); //Date object
                    //console.log(jsDate);
                    // $("#" + ctrl).datepicker('setDate', jsDate);

                    $("#" + ctrl).datetimepicker({
                        format: 'YYYY-MM-DD',
                        date: jsDate,
                        calendarWeeks: true,

                        showClose: true,
                        showClear: true,

                        inline:true,
                        hide: true,
                       
                        
                    });

                    $("#" + ctrl).data("DateTimePicker").hide();
                }
            }
        }
    function GetDatePicker() {


            // console.log(event.target);
            //console.log(event.target.value);
            //DD-MM-YYYY

            $("#" + event.target.id).datetimepicker({
                format: 'YYYY-MM-DD',

                calendarWeeks: true,
                maxDate: new Date(),
                showClear: true,
                showClose: true,
               
               inline: true
            }).css({
                'position': 'resolute', 'z-index': '4000', 'overflow': 'hidden'
            });

            $("#" + event.target.id).focus(function () {
                $("#" + event.target.id).datetimepicker({
                    format: 'YYYY-MM-DD',
                    maxDate: new Date(),
                    calendarWeeks: true,

                    showClose: true,
                    showClear: true,

                    inline: true
                }).css({ 'position': 'resolute', 'z-index': '4000', 'overflow': 'hidden' });;
            });

            $("#" + event.target.id).click(function () {
                $("#" + event.target.id).datetimepicker({
                    format: 'YYYY-MM-DD',

                    calendarWeeks: true,
                    
                    showClear: true,
                    showClose: true,

                    inline: true
                }).css({ 'position': 'resolute', 'z-index': '4000', 'overflow': 'hidden' });
            });
            //$('#ui-datepicker-div').show();






        }

        function BindWhoStage(itemlist, masterName, id, title) {
            var collections = itemlist

            data = $.grep(collections, function (e) { return e.MasterName == masterName });
            if (jQuery.isEmptyObject(data) == false) {

                $(id).dataTable().fnDestroy();

                $(id).DataTable({
                    "aaData": data,
                    "bSort": false,
                    "bPaginate": false,
                    "bFilter": false,
                    "bInfo": false,
                    "autoWidth": false,
                    "aoColumns": [
                        {

                            bSortable: false,
                            mRender: function (data, type, full) {


                                var str = $('<input>').attr({ type: 'checkbox', id: 'hidRowId', value: full["ItemId"], runat: "server" }).append('<input >').attr({ type: 'checkbox', name: 'chkAchieved', title: title, id: 'chkAchieved' + full["ItemId"] }).addClass("flat-red").prop('outerHTML');

                                return str;




                            }
                        },
                        {
                            bSortable: false,
                            mRender: function (data, type, full) {

                                return full["ItemDisplayName"];
                            }
                        },
                        {
                            bSortable: false,
                            mRender: function (data, type, full) {
                                var str = "<input type=\"text\"  class=\"form-control  \" id=\"dtCurrent" + full["ItemId"] + "\" placeholder=\"Current\"  onfocus=\"GetDatePicker()\"  data-provide=\"datepicker\" >";
                                return str;
                            }
                        }

                    ]
                });

            }
        }





        function WhoStageCheckBoxClick(ctrlName, isDelete) {

            if (!(selectedstage === "undefined" || selectedstage === null || selectedstage === "")) {
                {
                    if (selectedstage === "Stage4") {

                        var chk = $.grep(arrWHoStage, function (e) { return e == "4"; });

                        if (jQuery.isEmptyObject(chk) == true) {
                            arrWHoStage.push('4');
                        }
                    }
                    else if (selectedstage === "Stage3") {
                        var chk = $.grep(arrWHoStage, function (e) { return e == "3"; });

                        if (jQuery.isEmptyObject(chk) == true) {
                            arrWHoStage.push('3');
                        }
                    }
                    else if (selectedstage === "Stage2") {
                        var chk = $.grep(arrWHoStage, function (e) { return e == "2"; });

                        if (jQuery.isEmptyObject(chk) == true) {
                            arrWHoStage.push('2');
                        }
                    }
                    else if (selectedstage === "Stage1") {
                        var chk = $.grep(arrWHoStage, function (e) { return e == "1"; });

                        if (jQuery.isEmptyObject(chk) == true) {
                            arrWHoStage.push('1');
                        }
                    }

                }
            }

            var chk = $.grep(arrWHoStage, function (e) { return e == ctrlName.title; });

            if (jQuery.isEmptyObject(chk) == true) {
                arrWHoStage.push(ctrlName.title);
            }


            if (isDelete) {
                var cName = "WHOStage";
                if (ctrlName.title == "1") {
                    cName = "dtlStageI";
                }
                else if (ctrlName.title == "2") {
                    cName = "dtlStageII";
                }
                else if (ctrlName.title == "3") {
                    cName = "dtlStageIII";
                }
                else if (ctrlName.title == "4") {
                    cName = "dtlStageIV";
                }

                var isExists = false;
                var rows = $("#" + cName).dataTable().fnGetNodes();
                for (var i = 0; i < rows.length; i++) {
                    // Get HTML of 3rd column (for example)
                    var col0 = $(rows[i]).find("td:eq(0)").html();
                    var hidV = $(col0).attr("value");
                    var chkVal = $("#chkAchieved" + hidV).prop('checked');
                    var col2 = $(rows[i]).find("td:eq(2)").html();
                    var col3 = $(rows[i]).find("td:eq(3)").html();

                    if (chkVal) {
                        isExists = true;
                    }
                }

                if (!isExists) {
                    arrWHoStage = jQuery.grep(arrWHoStage, function (value) {
                        return value != ctrlName.title;
                    });
                }

            }

            if (arrWHoStage.length == 0) {
                if (!(selectedstage === "undefined" || selectedstage === null || selectedstage === "")) {
                    $("#WHOStage option").each(function () {
                        this.selected = $(this).text() == selectedstage;
                    });

                }
                else {
                    $("#WHOStage option").each(function () {
                        this.selected = $(this).text() == "select";

                    });
                }
            }
            if (arrWHoStage.length > 0) {

                var maxValue = Math.max.apply(Math, arrWHoStage);

                var value = "Stage" + maxValue;
                $("#WHOStage option").each(function () {
                    this.selected = $(this).text() == value;
                });



            }

        }




function GetGBVScreeningStatus() {
        var patientId ="<%=PatientId%>";
        var visitDate = moment("<%=visitdateval%>");
        var screeningCategoryId = "<%=GbvScreeningCategoryId%>";

        if (visitDate.isValid()) {
            visitDate = visitDate.format('YYYY-MM-DD');

            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/getPatientScreening",
                data: "{'patientId':'" + patientId + "', 'visitDate': '" + visitDate + "', 'screeningcategoryId': '" + screeningCategoryId + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",

                success: function (response) {
                    var itemList = JSON.parse(response.d);

                    $("#lblGbvAssessmentDone").text(itemList.length > 0 ? 'Yes' : 'No');

                }
            });
        } else {

            $("#lblGbvAssessmentDone").text('No');

        }
    }

    GetGBVScreeningStatus();

</script>

