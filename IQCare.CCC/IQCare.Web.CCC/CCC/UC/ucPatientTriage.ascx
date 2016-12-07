<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriage.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriage" %>

<div class="col-md-12 jumbotron">
    <div class="col-md-12"><small class="muted pull-left"><strong>PATIENT TRIAGE <small>vital signs</small></strong></small></div> <div class="col-md-12"><hr /> </div>                                                   
    <div class="col-md-12">
         <div class="col-md-1">
              <h4 class="pull-left text-primary"><i class="fa fa-heartbeat fa-5x" aria-hidden="true"></i></h4>
         </div>
         
         <div class="col-md-5 ">
              <div class="col-md-12"><h1 class="text-primary pull-left"><small>Anthropometric Measurement</small></h1></div>
              <div class="col-md-12"><hr /></div>
              <div class="col-md-12">
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
                   </div><%-- .col-md-12--%>
                   
              <div class="col-md-12" style="padding-top:3%;padding-bottom:3%">
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
                   </div><%-- .col-md-12--%>
              
             <div class="col-md-12"><hr /></div>
             
             <div class="col-md-12"><h1 class="text-primary pull-left"> Vital Signs </h1></div>

              <div class="col-md-12" style="padding-top:5%;padding-bottom:3%">
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

              </div><%-- .col-md-12--%>

              <div class="col-md-12">
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
              </div><%-- .col-md-12--%>

         </div><%-- .col-md-5--%>
        
         <div class="col-md-6">
              <div class="col-md-12"><h1 class="text-primary pull-left"><small>Known Allergies</small></h1></div>
              <div class="col-md-12"><hr /></div>
              <div class="col-md-12">
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
             </div><%-- .col-md-12--%>
             <div class="col-md-12" style="padding-top:3%">
                                                        <div class="col-md-12 form-group">
                                                            <div class="col-md-12"><label for="" class="control-label pull-left">Nurse Note(s)</label></div>
                                                            <div class="col-md-12">
                                                                <textarea id="NurseNotes" class="form-control" rows="3" runat="server"></textarea>
                                                            </div>
                                                        </div>

                                                    </div>
        </div><%-- .col-md-6--%>
	</div><%-- .col-md-12--%>
</div>