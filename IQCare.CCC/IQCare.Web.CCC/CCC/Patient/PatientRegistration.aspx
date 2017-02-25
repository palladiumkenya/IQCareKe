<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientRegistration.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

    <div class="=col-md-12">
        
        <div class="col-md-12">
             <div class="bs-callout bs-callout-danger hidden">
                 <h4 class="fa fa-exclamation-circle"> VALIDATION ERROR(S)</h4>
                 <p>This form seems to be invalid :(</p>
             </div>
        </div> 

        <div class="col-md-12">
            <div class="bs-callout bs-callout-info hidden">
                  <h4 class="fa fa-check-square-o"> All SECTION VALIDATION PASSED</h4>
                  <p>Everything seems to be ok :)</p>
             </div>
        </div>
        
        <div class="col-md-12">
                      
            <div class="wizard" data-initialize="wizard" id="myWizard">
                 <div class="steps-container">
	                  <ul class="steps">
		                  <li data-step="1" data-name="profile" class="active">
			                  <span class="badge">1</span>Patient Profile
			                  <span class="chevron"></span>
		                  </li>

		                  <li data-step="2" data-name="location">
			                  <span class="badge">2</span>Patient Location
			                  <span class="chevron"></span>
		                  </li>

                          <li data-step="3" data-name="contacts">
			                  <span class="badge">3</span>Patient Contacts
			                  <span class="chevron"></span>
		                  </li>

		                  <li data-step="4" data-name="socialstatus">
			                  <span class="badge">4</span>Patient Population
			                  <span class="chevron"></span>
		                  </li>
	                 </ul>
                 </div><%-- .steps-container--%>
                      
                 <div class="actions form-navigation">
	                  <button type="button" class="btn btn-default btn-prev">
		                  <span class="glyphicon glyphicon-arrow-left"></span>Prev</button>
	                  <button type="button" class="btn btn-primary btn-next" id="next" data-last="Complete">Next
		                   <span class="glyphicon glyphicon-arrow-right"></span>
	                  </button>
                 </div>

                 <div class="step-content">
	                  <div class="step-pane active sample-pane" id="datastep1" data-parsley-validate="true" data-show-errors="true" data-step="1">
                          <div class="col-md-12">
                              <small class="pull-left text-primary">1.Patient Basic Details</small>
                              <hr />
                          </div>

		                 <div class="col-md-12" style="margin-bottom: 2%"> 
		                     <div class="col-md-3">
		                         <div class="form-group">
                                    <asp:HiddenField ID="hdnPersonId" ClientIDMode="Static" runat="Server" Value="" />
                                    <div class="col-md-12"><label for="personFname" class="control-label pull-left">First Name</label></div>
                                    <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="personFname" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="first name" data-parsley-required="true" type="text" data-parsley-length="[2,25]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12"><label for="personMName" class="control-label pull-left">Middle Name</label></div>
                                    <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="personMName" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="middle name"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12"><label for="personLName" class="control-label pull-left">Last Name</label></div>
                                    <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="personLName" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="last name" data-parsley-required="true" data-parsley-length="[2,25]"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="col-md-12"><label for="Gender" class="control-label pull-left">Gender</label></div>
                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="Gender" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1"/>
                                </div>
                            </div>

		                </div><%--.col-md-12  --%>
                          
                        <div class="col-md-12">
                              <div class="pull-left text-primary">Social Status</div>

                              <!--<div class="form-group">
                                  <div class="col-md-12"><label for="PersonDoB" class="control-label pull-left text-primary">Social Status </label></div>
                              </div>-->
                          </div>

                        <div class="col-md-12" style="padding-top:2%">
                                               
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="col-md-12"><label class="control-label pull-left">Date of Birth</label></div>
                                        <div class="col-md-12">
                                            <div class="datepicker fuelux form-group" id="MyDateOfBirth">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PersonDoB"></asp:TextBox>        
                                                        <%-- <input ClientIDMode="Static" class="form-control input-sm" runat="server" id="DateOfBirth" type="date" />--%>
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
                                        <div class="col-md-12"><label for="personAge" class="control-label pull-left">Age(years)</label></div>
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="personAge" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="0" required="true" min="0" value="0" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="col-md-12"><label for="ChildOrphan" class="control-label"> (<18yr) Orphan</label></div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="ChildOrphan" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1"/>    
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">

                                    <div class="form-group">
                                            <div class="col-md-12"><label for="Inschool" class="control-label pull-left">In School ? </label></div>
                                            <div class="col-md-12">
                                                <asp:DropDownList runat="server" ID="Inschool" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1"/>
                                            </div>
                                    </div>
                                </div>
                                               
                            </div><%-- .col-md-12--%>

                        <div class="col-md-12 form-group">
                                <div class="col-md-12"><label class="control-label pull-left text-primary">Adult : (<i>provide ID number and marital status</i>)</label></div>    
                            <div class="col-md-12"><hr style="margin-bottom: 1%;margin-top: 1%" /></div>  
                        </div>

                        <div class="col-md-12 form-group">
                                <div class="col-md-3">
                                    <div class="col-md-12"><label for="NationalId" class="control-label pull-left">ID Number</label></div>
                                    <div class="col-md-12">
                                        <asp:TextBox type="text" runat="server" id="NationalId" class="form-control input-sm" placeholder="national id no.." ClientIDMode="Static" required="true" data-parsley-required="true" data-parsley-length="[8,8]"  />
                                    </div>
                                </div>
                                         
                                <div class="col-md-3">
                                    <div class="col-md-12"><label for="MaritalStatusId" class="control-label pull-left">Marital Status </label></div>
                                    <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="MaritalStatusId" class="form-control input-sm" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1"></asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>  
                            </div> 

                            <div class="col-md-12 form-group">  
                                <div class="col-md-12"><hr /></div> 
                                <div class="col-md-12"><label class="control-label pull-left text-primary"><strong>Child :</strong> Parent / Gurdian Name</label></div>  
                            </div>

                            <div class="col-md-12 form-group">   
                                <div class="col-md-3">
                                        <div class="col-md-12"><label class="control-label pull-left">First Name</label></div>
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" type="text" id="GurdianFNames" class="form-control input-sm" placeholder="gurdian first name.." ClientIDMode="Static" data-parsley-required="true" />
                                        </div>
                                </div>
                                              
                                <div class="col-md-3">
                                        <div class="col-md-12"><label class="control-label pull-left">Middle Name</label></div>
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" type="text" id="GurdianMName" name="GurdianMName" class="form-control input-sm" placeholder="guardian Middle name" ClientIDMode="Static" />
                                        </div>
                                </div>
                                               
                                <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Last Name</label></div>
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="GurdianLName" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="guardian last name" data-parsley-required="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Guardian Gender</label></div>
                                    <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="GuardianGender" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-min="1" />
                                    </div>
                                </div>
                            </div>           
                          
                                           
	                    </div><%-- .step-pane--%>

	                  <div class="step-pane sample-pane" id="datastep2" data-parsley-validate="true" data-step="2">      
                           <div class="col-md-12">
                                <small class="pull-left text-primary">2.Patient Location Details</small>
                                <hr />
                           </div><%-- .col-md-12--%>
                                         
                           <div class="col-md-12 form-group">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="col-md-12"><label class="control-label pull-left">County</label></div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="countyId" ClientIDMode="Static" CssClass="form-control input-sm" required="true" data-parsley-min="1"/>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="col-md-12"><label class="control-label pull-left">Sub-County</label></div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="SubcountyId" ClientIDMode="Static" CssClass="form-control input-sm" required="true" data-parsley-min="1" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="col-md-12"><label class="control-label pull-left">Ward</label></div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="WardId" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1"/>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                                  <div class="form-group">
                                                      <div class="col-md-12"><label class="control-label pull-left">Village</label></div>
                                                      <div class="col-md-12">
                                                          <asp:TextBox runat="server" ID="LocalCouncils" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="person village.." ></asp:TextBox>
                                                      </div>
                                                  </div>
                                               </div>
                           </div>

                           <div class="col-md-12 form-group">
                                <div class="col-md-3">
                                      <div class="col-md-12"><label class="control-label pull-left">Location</label></div>
                                      <div class="col-md-12">
                                           <asp:TextBox runat="server" ID="PatientLocation" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="location .."></asp:TextBox>
                                      </div>
                                </div>
                                
                                <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Sub Location</label></div>
                                    <div class="col-md-12">
                                         <input type="text" runat="server" class="form-control input-sm" placeholder="sub location.." id="sublocation" name="sublocation" />
                                    </div>     
                               </div>

                               <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Landmark</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox runat="server" class="form-control input-sm" placeholder="landmark.." id="PatientLandmark" data-parsley-required="true" data-parsley-length="[2,100]" />
                                    </div>
                               </div>
                               
                               <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Nearest Health Centre</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox runat="server" ID="NearestHealthCentre" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="nearest health centre" data-parsley-required="true" data-parsley-length="[2,30]" ></asp:TextBox>
                                    </div>
                               </div>
                           </div>                                                
                      </div><%-- .step-pane-2--%>

	                  <div class="step-pane sample-pane" id="datastep3" data-parsley-validate="true" data-step="3">
                           <div class="col-md-12">
                                <small class="text-primary pull-left"> 3. Patient Contacts </small>
                                <hr />
                           </div>

                           <div class="col-md-12 form-group">
                                <div class="col-md-4">
                                     <div class="col-md-12"><label class="control-label pull-left">Postal Address</label></div>
                                     <div class="col-md-12">
                                          <asp:TextBox type="text" runat="server" id="PatientPostalAddress" name="PatientPostalAddress" class="form-control input-sm" placeholder="postal address" data-parsley-required="true" data-parsley-length="[8,100]" ClientIDMode="Static"/>
                                     </div>     
                                </div>
                               
                               <div class="col-md-2">
                                    <div class="col-md-12"><label class="control-label pull-left">Mobile No.</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox  runat="server" type="text" id="PatientMobileNo" name="PatientMobileNo" class="form-control input-sm" placeholder="Mobile No..." ClientIDMode="Static" data-parsley-pattern-message="Please enter a valid Kenyan mobile phone number. Format ((+2547XXXXXXXX) or (07XXXXXXXX))" data-parsley-required="true" data-parsley-pattern="/(\+?254|0){1}[7]{1}([0-9]{1}[0-9]{1}|[9]{1}[0-2]{1})[0-9]{6}/" />
                                    </div>         
                               </div>
                               
                                <div class="col-md-2">
                                    <div class="col-md-12"><label class="control-label pull-left">Alt. Mobile No.</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox runat="server" type="text" id="PatientAlternativeMobile" name="PatientAlternativeMobile" class="form-control input-sm" placeholder="alternative mobile no..." ClientIDMode="Static" data-parsley-pattern-message="Please enter a valid Kenyan mobile phone number. Format ((+2547XXXXXXXX) or (07XXXXXXXX))" data-parsley-pattern="/(\+?254|0){1}[7]{1}([0-9]{1}[0-9]{1}|[9]{1}[0-2]{1})[0-9]{6}/" />
                                    </div>       
                              </div>

                                <div class="col-md-4">
                                   <div class="col-md-12"><label class="control-label pull-left">Email Address</label></div>
                                   <div class="col-md-12">
                                        <asp:TextBox runat="server" id="PatientEmailAddress" name="PatientEmailAddress" class="form-control input-sm" placeholder="email address" ClientIDMode="Static" data-parsley-type="email" />
                                   </div>    
                              </div>
                            </div> 
                                   
                           <div class="col-md-12"><label class=" control-label text-primary  pull-left">Treatment Supporter Information</label></div>
                           <div class="col-md-12"><hr/></div>
                            
                           <div class="col-md-12 form-group">
                                <div class="col-md-3">
                                              <div class="col-md-12"><label class="control-label pull-left">First Name</label></div>
                                              <div class="col-md-12">
                                                   <asp:TextBox runat="server"  CssClass="form-control input-sm" placeholder="first name..." ID="tsFname" ClientIDMode="Static" data-parsley-required="true" data-parsley-length="[2,50]"></asp:TextBox>
                                               </div>
                                         </div>

                                <div class="col-md-3">
                                             <div class="col-md-12"><label class="control-label pull-left">Middle Name</label></div>
                                             <div class="col-md-12">
                                                  <asp:TextBox runat="server" ID="tsMiddleName" ClientIDMode="Static" CssClass="form-control input-sm"/>
                                             </div>
                                         </div>

                                <div class="col-md-3">
                                              <div class="col-md-12"><label class="control-label pull-left">Last Name</label></div>
                                              <div class="col-md-12">
                                                   <asp:TextBox runat="server" ID="tsLastName" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-length="[2,50]"></asp:TextBox>
                                              </div>
                                         </div>

                                <div class="col-md-3">
                                              <div class="form-group">
                                                  <div class="col-md-12"><label class="control-label pull-left">Gender </label></div>
                                                  <div class="col-md-12">
                                                      <asp:DropDownList runat="server" ID="tsGender" ClientIDMode="Static" CssClass="form-control input-sm" required="true" data-parsley-min="1"/>
                                                  </div>
                                              </div>
                                         </div>
                            </div>
                                   
                           <div class="col-md-12 form-group">
                               
                                <!-- <div class="col-md-3">
                                      <div class="col-md-12"><label class="control-label pull-left">Mobile Contact </label></div>
                                      <div class="col-md-12">
                                           <asp:TextBox runat="server" CssClass="form-control input-sm" id="TSContactsz" placeholder="mobile no.." data-parsley-required="true" data-parsley-length="[8,14]"></asp:TextBox>
                                      </div>
                                 </div>-->
                               
                               <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Mobile Contact.</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox  runat="server" type="text" id="TSContacts" class="form-control input-sm" placeholder="Mobile No..." data-parsley-pattern-message="Please enter a valid Kenyan mobile phone number. Format ((+2547XXXXXXXX) or (07XXXXXXXX))" data-parsley-required="true" data-parsley-pattern="/(\+?254|0){1}[7]{1}([0-9]{1}[0-9]{1}|[9]{1}[0-2]{1})[0-9]{6}/" />
                                    </div>         
                               </div>

                                 <div class="col-md-3"></div>
                                 <div class="col-md-3"></div>
                                 <div class="col-md-3"></div>
                            </div>
	                  </div><%-- .step-pane-3--%>

                      <div class="step-pane sample-pane" id="datastep4" data-parsley-validate="true" data-step="4">
                           <div class="col-md-12">
                               <small class="pull-left text-primary"> 4. Patient population categorization</small>
                               <hr />
                           </div>

                           <div class="col-md-12 form-group">
                                <div class="col-md-3">
                                     <div class="form-group"><label class="control-label pull-left text-primary">Population type</label></div>
                                </div>
                                
                               <div class="col-md-4">
                                                    
                                     <div class="form-group"><div class="col-md-12"><label class="control-label pull-left">Select Population type</label></div></div>

                                     <div class="col-md-12 radio">
                                         
                                          <label class="radio-custom  pull-left" data-initialize="radio" id="GenPopulation">
                                              <input type="radio" name="Population" value="General Population" class="sr-only" id="rdbGenPopulation" />
                                              General Population
                                          </label>
                                     </div>
                                     
                                     <div class="col-md-12 radio checked">
                                          <label class="radio-custom pull-left" data-initialize="radio" id="KeyPopulation">
                                              <input type="radio" name="Population" value="Key Population" class="sr-only" id="rdbKeyPopulation" />
                                              
                                                Key population
                                          </label>
                                     </div>

                                </div>
                                
                                <div class="col-md-3">
                                    <div class="col-md-12"><label class="control-label pull-left">Select if Key.Pop</label></div>
                                    <div class="col-md-12">
                                        <asp:DropDownList runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="KeyPopulationCategoryId" data-parsley-min="1"/>
                                    </div>        
                               </div>
                                
                                <div class="col-md-2"></div>
                           </div>
                                          
	                  </div><%-- .step-content-4--%>

                 </div><%-- .step-content--%>

            </div><%-- .wizard--%>
        </div><%-- .col-md-12--%>

    </div><%--.col-md-12--%>
    
    <script type="text/javascript">
        $(document)
            .ready(function() {

                var personAge = 0;
                var userId=<%=UserId%>;
                var personId=0;

                /*----- make readonly by default ----- */
                $("#<%=ChildOrphan.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=Inschool.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianFNames.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianMName.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianLName.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GuardianGender.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=MaritalStatusId.ClientID%>").attr('disabled', 'disabled');

                $('#MyDateOfBirth').datepicker({
                        date:null,
                        allowPastDates: true,
                        momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                        //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
                });

                $('#MyDateOfBirth').on('changed.fu.datepicker dateClicked.fu.datepicker', function(event,date) {
                    var x = $('#MyDateOfBirth').datepicker('getDate');
                    $('#<%=personAge.ClientID%>').val(getAge(x));
                    personAgeRule();
                });

                $('#<%=countyId.ClientID%>').on("change", function() {
                     getSubcountyList(); /*call AJAX function */
                });

                $("#<%=SubcountyId.ClientID%>").on("change", function() {
                    getWardList();
                });


                
                /* Business Rules setup */
                function personAgeRule()
                {
                    personAge = $("#personAge").val();

                    if (personAge >= 18) {
                        $("#ChildOrphan").val("");
                        $("#Inschool").val("");

                        $("#<%=ChildOrphan.ClientID%>").prop('disabled',true);
                        $("#<%=Inschool.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianFNames.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianMName.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianLName.ClientID%>").prop('disabled', true);
                        $("#<%=GuardianGender.ClientID%>").prop('disabled',true);
                        $("#<%=MaritalStatusId.ClientID%>").prop('disabled', false);
                    } else {
                        $("#<%=ChildOrphan.ClientID%>").prop('disabled',false);
                        $("#<%=Inschool.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianFNames.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianMName.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianLName.ClientID%>").prop('disabled',false);
                        $("#<%=GuardianGender.ClientID%>").prop('disabled',false);
                        $("#<%=MaritalStatusId.ClientID%>").prop('disabled', true);
                    }
                };

                $("#myWizard")
                    .on("actionclicked.fu.wizard", function(evt, data) {
                        var currentStep = data.step;
                        var nextStep = 0;
                        var previousStep = 0;
                        var totalError = 0;
                        var stepError = 0;
                        /*var form = $("form[name='form1']");*/
                           

                        if (data.direction === 'next')
                            nextStep=currentStep += 1;
                        else
                            previousStep=nextStep -= 1;
                        if (data.step === 1) {
                                $('#datastep1').parsley().destroy();
                                $('#datastep1').parsley({
                                    excluded:
                                        "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"});
                                
                            /* add constraints based on age*/                                         
                            if ($('#datastep1').parsley().validate()) {
                                if (personAge >= 18) {
                                    $.when(addPerson()).then(function(){});                                   
                                } else {
                                    $.when(addPerson()).then(function(){
                                        setTimeout(function(){
                                            addPersonGaurdian();
                                        }, 2000);                            
                                    });
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
                                addPersonLocation();
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
                                //$.when(addPatientContact()).then(addPersonTreatmentSupporter());
                                $.when(addPatientContact()).then(function() {
                                    $.when(addPersonTreatmentSupporter()).then(function() {
                                        //addTreatmentSupporter();
                                        /*setTimeout(function(){
                                            addTreatmentSupporter();
                                        }, 2000);*/
                                    });
                                    //addPersonTreatmentSupporter();
                                });
                                //addTreatmentSupporter();
                            } else {
                                stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
                            }
                        }
                        else if (data.step===4) {
                            $('#datastep4').parsley().destroy();
                            $('#datastep4').parsley({
                                excluded:
                                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
                            });
                            if ($("#datastep4").parsley().validate()) {
                                $.when(addPersonPopulation()).then(function() {
                                    setTimeout(function(){
                                        window.location.href ='<%=ResolveClientUrl( "~/CCC/Enrollment/ServiceEnrollment.aspx")%>';
                                    }, 2000);
                                });
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
                        }
                    })
                    .on("changed.fu.wizard",
                        function() {

                        })
                    .on('stepclicked.fu.wizard',
                        function() {

                        })
                    .on('finished.fu.wizard',
                        function(e) {
                        });

                /* calculate Person Age */
                function getAge(dateString) 
                {
                    var today = new Date();
                    var birthDate = new Date(dateString);
                    var age = today.getFullYear() - birthDate.getFullYear();
                    var m = today.getMonth() - birthDate.getMonth();
                    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) 
                    {
                        age--;
                    }
                    if (personAge >= 18)
                    {
                        $("#<%=ChildOrphan.ClientID%>").prop('disabled',true);
                        $("#<%=Inschool.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianFNames.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianMName.ClientID%>").prop('disabled', true);
                        $("#<%=GurdianLName.ClientID%>").prop('disabled', true);
                        $("#<%=GuardianGender.ClientID%>").prop('disabled',true);
                        $("#<%=MaritalStatusId.ClientID%>").prop('disabled', false);
                    } else {
                        $("#<%=ChildOrphan.ClientID%>").prop('disabled',false);
                        $("#<%=Inschool.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianFNames.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianMName.ClientID%>").prop('disabled',false);
                        $("#<%=GurdianLName.ClientID%>").prop('disabled',false);
                        $("#<%=GuardianGender.ClientID%>").prop('disabled',false);
                        $("#<%=MaritalStatusId.ClientID%>").prop('disabled', true);
                    }
                    return age;
                }

                function getSubcountyList()
                {
                    var countyId = $("#<%=countyId.ClientID%>").find(":selected").text();
                    //alert(countyId);
                    $.ajax({
                        type: "POST",
                        url: "../WebService/LookupService.asmx/GetLookupSubcountyList",
                        data: "{'county':'" + countyId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var itemList = JSON.parse(response.d);
                            $("#<%=SubcountyId.ClientID%>").find('option').remove().end();
                            $("#<%=SubcountyId.ClientID%>").append('<option value="0">Select</option>');
                            $.each(itemList, function (index, itemList) {

                                $("#<%=SubcountyId.ClientID%>").append('<option value="' + itemList.SubcountyId + '">' + itemList.SubcountyName + '</option>');
                            }); 
                        },
                        error: function (response) {
                            toastr.error("Error in selecting the SubcountyList to Load "+ response.d, "Fetching subcounty List");
                        }
                    });
                }

                function getWardList()
                {
                    var subcountyName = $("#<%=SubcountyId.ClientID%>").find(":selected").text();
                    //alert(subcountyName);
                    $.ajax({
                        type: "POST",
                        url: "../WebService/LookupService.asmx/GetLookupWardList",
                        data: "{'subcounty':'" + subcountyName + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var itemList = JSON.parse(response.d);
                            $("#<%=WardId.ClientID%>").find('option').remove().end();
                            $("#<%=WardId.ClientID%>").append('<option value="0">Select</option>');
                            $.each(itemList, function (index, itemList) {
                                $("#<%=WardId.ClientID%>").append('<option value="' + itemList.WardId + '">' + itemList.WardName + '</option>');
                            }); 
                        },
                        error: function (response) {
                            toastr.error("Error in Fetching Ward list " + response.d, "Fetching Ward List");
                        }
                    });     
                }

                function addPerson() {

                    var isPatientSet = $.urlParam('PatientId');

                    var fname = $("#<%=personFname.ClientID%>").val();
                    var mname =  $("#<%=personMName.ClientID%>").val();
                    var lname =  $("#<%=personLName.ClientID%>").val();
                    var sex =  $("#<%=Gender.ClientID%>").find(":selected").val();
                    var natId = $("#<%=NationalId.ClientID%>").val();
                    var userId = <%=UserId%>;
                    var dateOfBirth = $('#MyDateOfBirth').datepicker('getDate');
                    var maritalstatusId = $("#<%=MaritalStatusId.ClientID%>").find(":selected").val();

                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPerson",
                        data: "{'firstname':'" + fname + "','middlename':'" + mname + "','lastname':'" + lname + "','gender':" + sex + ",'dateOfBirth':'" + moment(dateOfBirth).format('DD-MMM-YYYY')  + "','nationalId':'" + natId + "', 'maritalStatusId':'" + maritalstatusId + "','userId':'" + userId + "', 'patientid': '" + isPatientSet + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Profile");
                        },
                        error: function (response) {
                            toastr.error(response.d, "Person Profile Error");
                        }
                    });
                }

                function addPersonGaurdian() {

                    var isPatientSet = $.urlParam('PatientId');
					var returnValue=0;
                    var gfname = $("#<%=GurdianFNames.ClientID%>").val();
                    var gmname = $("#<%=GurdianMName.ClientID%>").val();
                    var glname = $("#<%=GurdianLName.ClientID%>").val();
                    var gsex = $("#<%=GuardianGender.ClientID%>").find(":selected").val();
                    var natId = 999999;
                    var orphan = $("#<%=ChildOrphan.ClientID%>").find(":selected").text();
                    var inSchool = $("#<%=Inschool.ClientID%>").find(":selected").text();


                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPersonGuardian",
                        data: "{'firstname':'" + gfname + "','middlename':'" + gmname + "','lastname':'" + glname + "','gender': '" + gsex + "','dateOfBirth':'" + "<%=DateTime.Now%>" + "' ,'nationalId':'" + natId + "','orphan':'" + orphan + "','inSchool':'" + inSchool + "','userId':'" + userId + "', 'patientid':'" + isPatientSet + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                                toastr.success(response.d, "Person Guardian");
                        },
                        error: function (response) {
                            toastr.error(response.d,"Person Guardian Error");
                        }
                    });
                }

               function addPersonTreatmentSupporter() {
                   var isPatientSet = $.urlParam('PatientId');

                    var tFname = $("#<%=tsFname.ClientID%>").val();
                    var tMname = $("#<%=tsMiddleName.ClientID%>").val();
                    var tLname = $("#<%=tsLastName.ClientID%>").val();
                   var tSex = $("#<%=tsGender.ClientID%>").val();
                   var mobileContact = $("#<%=TSContacts.ClientID%>").val();
                   var natId = 999999;

                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPersonTreatmentSupporter",
                        data: "{'firstname':'" + tFname + "','middlename':'" + tMname + "','lastname':'" + tLname + "','gender':" + tSex + ",'nationalId':'" + natId + "','userId':'" + userId + "', 'mobileContact':'" + mobileContact + "', 'patientid': '" + isPatientSet + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Treatment Supporter");
                        },
                        error: function (response) {
                            toastr.error(response.d, "Person Treatment Supporter Error");
                        }
                    });
               }

                function addTreatmentSupporter() {
                    var isPatientSet = $.urlParam('PatientId');
                    var mobileContact = $("#<%=TSContacts.ClientID%>").val();

                    //Set up url for adding a person
                    var url = null;

                    if (isPatientSet > 0) {
                        url = "../WebService/PersonService.asmx/UpdateTreatmentSupporter";
                    } else {
                        url = "../WebService/PersonService.asmx/AddTreatmentSupporter";
                    }

                    $.ajax({
                        type: "POST",
                        url: url,
                        data: "{'personId':'" + personId + "','supporterId':'0','mobileContact':'" + mobileContact + "','userId':'" + userId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Treatment Supporter");
                        },
                        error: function (response) {
                            toastr.success(response.d, "Person Treatment Supporter");
                        }
                    });
                }

                function addPersonMaritalStatus() {

                    var maritalstatusId = $("#<%=MaritalStatusId.ClientID%>").find(":selected").val();
                    var isPatientSet = $.urlParam('PatientId');

                    var url = null;

                    if (isPatientSet > 0) {
                        url = "../WebService/PersonService.asmx/UpdatePersonMaritalStatus";
                    } else {
                        url = "../WebService/PersonService.asmx/AddPersonMaritalStatus";
                    }

                    $.ajax({
                        type: "POST",
                        url: url,
                        data: "{'personId':'" + personId + "','maritalStatusId':'" + maritalstatusId + "','userId':'" + userId + "','patientid':'" + isPatientSet + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Marital Status");
                        },
                        error: function (response) {
                            toastr.error(response.d, "Person Marital Status Error");
                        }
                    });
                }

                function addPersonOvcStatus() {
                    var isPatientSet = $.urlParam('PatientId');

                    var personGuardianId = 0;
                    var orphan = $("#<%=ChildOrphan.ClientID%>").find(":selected").text();
                    var inSchool = $("#<%=Inschool.ClientID%>").find(":selected").text();
                    var url = null;

                    if (isPatientSet > 0) {
                        url = "../WebService/PersonService.asmx/UpdatePersonOvcStatus";
                    } else {
                        url = "../WebService/PersonService.asmx/AddPersonOvcStatus";
                    }

                    $.ajax({
                        type: "POST",
                        url: url,
                        data: "{'personId':'" +personId  + "','guardianId':'" + personGuardianId + "','orphan':'" + orphan + "','inSchool':'" + inSchool + "','userId':'" + userId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person OVC Status");
                        },
                        error: function (response) {
                            toastr.error(response.d, "--- Person OVC Status Error ---");
                        }
                    });
                }

                function addPersonLocation() {

                    var isPatientSet = $.urlParam('PatientId');
                    var county = $("#<%=countyId.ClientID%>").find(":selected").val();
                    var subcounty=$("#<%=SubcountyId.ClientID%>").find(":selected").val();
                    var ward = $("#<%=WardId.ClientID%>").find(":selected").val();
                    var village = $("#<%=LocalCouncils.ClientID%>").val();
                    var location = $("#<%=PatientLocation.ClientID%>").val();
                    var subLocation = $("#<%=sublocation.ClientID%>").val();
                    var landmark = $("#<%=PatientLandmark.ClientID%>").val();
                    var nearestHc = $("#<%=NearestHealthCentre.ClientID%>").val();
              
                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPersonLocation",
                        data: "{'personId':'" + personId + "','county':'" + county + "','subcounty':'" + subcounty + "','ward':'" + ward + "','village':'" + village + "','location':'" + location + "','sublocation':'" + subLocation + "','landmark':'" + landmark + "','nearesthealthcentre':'" + nearestHc + "','userId':'" + userId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Location");
                        },
                        error: function (response) {
                            toastr.error(response.d, "Person Location Error");
                        }
                    });
                }

                function addPatientContact() {
                    var isPatientSet = $.urlParam('PatientId');
                    var postalAddress =$("#<%=PatientPostalAddress.ClientID%>").val() ;
                    var mobileNumber = $("#<%=PatientMobileNo.ClientID%>").val();
                    var altMobile =$("#<%=PatientAlternativeMobile.ClientID%>").val() ;
                    var emailAddress = $("#<%=PatientEmailAddress.ClientID%>").val();

                    var url = null;

                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPersonContact",
                        data: "{'personId':'" + personId + "','physicalAddress':'" + postalAddress + "','mobileNumber':'" + mobileNumber + "','alternativeNumber':'" + altMobile + "','emailAddress':'" + emailAddress + "','userId':'" + userId + "','patientid':'" + isPatientSet + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Contact Information");
                        },
                        error: function (response) {
                            toastr.success(response.d, "Person Contact Information Error");
                        }
                    });
                }

                function addPersonPopulation() {
                    var isPatientSet = $.urlParam('PatientId');
                    var populationType = $("input[name='Population']:checked").val();
                    //var populationType = $('input[name="Population"]').value;
                    var populationCategoryId = $("#<%=KeyPopulationCategoryId.ClientID%>").find(":selected").val();

                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/AddPersonPopulation",
                        data: "{'patientId':'" + personId + "','populationtypeId':'" + populationType + "','populationCategory':'" + populationCategoryId + "','userId':'" + userId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Person Popuation");
                        },
                        error: function (response) {
                            toastr.error(response.d, "Person Population Error");
                        }
                    });
                    //var personId = 0;
                }

                $("input[name='Population']").on("change",
                    function() {
                        var itemName=$(this).val();
                        if (itemName === 'General Population') {
                            $("#<%=KeyPopulationCategoryId.ClientID%>").find('option').remove().end();
                            $("#<%=KeyPopulationCategoryId.ClientID%>").append('<option value="0">N/A</option>');
                            $("#<%=KeyPopulationCategoryId.ClientID%>").prop('disabled', true);
                        } else {
                            itemName = "KeyPopulation";
                            $("#<%=KeyPopulationCategoryId.ClientID%>").prop('disabled', false);
                            $.ajax({
                                type: "POST",
                                url: "../WebService/LookupService.asmx/GetLookUpItemByName",
                                data: "{'itemName':'" + itemName + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                
                                    var itemList = JSON.parse(response.d);
                                    $("#<%=KeyPopulationCategoryId.ClientID%>").find('option').remove().end();
                                    $("#<%=KeyPopulationCategoryId.ClientID%>").append('<option value="0">Select</option>');
                                    $.each(itemList, function (index, itemList) {
                                        $("#<%=KeyPopulationCategoryId.ClientID%>").append('<option value="' + itemList.ItemId + '">' + itemList.ItemDisplayName + ' ('+itemList.ItemName+')</option>');
                                    }); 
                                },
                                error: function (response) {
                                    generate('error', response.d);
                                }
                            });
                        }

                    });

               
                //toastr.options = {
                //    "closeButton": true,
                //    "debug": false,
                //    "newestOnTop": true,
                //    "progressBar": true,
                //    "positionClass": "toast-top-right",
                //    "preventDuplicates": false,
                //    "showDuration": "300",
                //    "hideDuration": "1000",
                //    "timeOut": "5000",
                //    "extendedTimeOut": "1000",
                //    "showEasing": "swing",
                //    "hideEasing": "linear",
                //    "showMethod": "fadeIn",
                //    "hideMethod": "fadeOut"
                //}

                //function generate(type, text) {

                //    var n = noty({
                //        text: text,
                //        type: type,
                //        dismissQueue: true,
                //        progressBar: true,
                //        timeout: 5000,
                //        layout: 'topRight',
                //        closeWith: ['click'],
                //        theme: 'relax',
                //        maxVisible: 10,
                //        animation: {
                //            open: 'animated bounceInLeft',
                //            close: 'animated bounceOutLeft',
                //            easing: 'swing',
                //            speed: 500
                //        }
                //    });
                //    console.log('html: ' + n.options.id);
                //    return n;
                //}

                $.urlParam = function(name){
                    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                    if (results==null){
                        return null;
                    }
                    else{
                        return results[1] || 0;
                    }
                }

                var PatientId = $.urlParam('PatientId');

                if (PatientId > 0) {
                    $.ajax({
                        type: "POST",
                        url: "../WebService/PersonService.asmx/GetPersonDetails",
                        data: "{'PatientId':'" + PatientId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var patientDetails = JSON.parse(response.d);
                            //console.log(patientDetails);
                            /*Patient Details*/
                            $("#personFname").val(patientDetails.FirstName);
                            $("#personMName").val(patientDetails.MiddleName);
                            $("#personLName").val(patientDetails.LastName);
                            $("#Gender").val(patientDetails.Gender);
                            
                            /*Social Status*/
                            $('#MyDateOfBirth').datepicker('setDate', patientDetails.PersonDoB);
                            $("#ChildOrphan").val(patientDetails.ChildOrphan);
                            $("#Inschool").val(patientDetails.Inschool);
                            $("#personAge").val(patientDetails.Age);

                            /*Adult*/
                            $("#NationalId").val(patientDetails.NationalId);
                            $("#MaritalStatusId").val(patientDetails.MaritalStatusId);

                            /*Child*/
                            $("#GurdianFNames").val(patientDetails.GurdianFNames);
                            $("#GurdianMName").val(patientDetails.GurdianMName);
                            $("#GurdianLName").val(patientDetails.GurdianLName);
                            $("#GuardianGender").val(patientDetails.GuardianGender);

                            /*County*/
                            $("#countyId").val(patientDetails.CountyId);
                            $.when(getSubcountyList()).then(function() {
                                setTimeout(function(){

                                    $("#SubcountyId").val(patientDetails.SubCounty);

                                    $.when(getWardList()).then(function() {
                                        setTimeout(function(){
                                            $("#WardId").val(patientDetails.Ward);
                                        }, 2000);
                                    });

                                }, 2000);
                            });

                            $("#LocalCouncils").val(patientDetails.Village);
                            $("#PatientLocation").val(patientDetails.Location);
                            $("#ctl00_IQCareContentPlaceHolder_sublocation").val(patientDetails.SubLocation);
                            $("#ctl00_IQCareContentPlaceHolder_PatientLandmark").val(patientDetails.LandMark);
                            $("#NearestHealthCentre").val(patientDetails.NearestHealthCentre);
                            /*Person Contact*/
                            $("#PatientPostalAddress").val(patientDetails.PatientPostalAddress);
                            $("#PatientMobileNo").val(patientDetails.MobileNumber);
                            $("#PatientAlternativeMobile").val(patientDetails.AlternativeNumber);
                            $("#PatientEmailAddress").val(patientDetails.EmailAddress);
                            /*Treatment Supporter*/
                            $("#tsFname").val(patientDetails.tsFname);
                            $("#tsMiddleName").val(patientDetails.tsMiddleName);
                            $("#tsLastName").val(patientDetails.tsLastName);
                            $("#tsGender").val(patientDetails.tsGender);
                            $("#ctl00_IQCareContentPlaceHolder_TSContacts").val(patientDetails.ISContacts);
                            /*Key Population*/
                            //$('input[name="Population"]').value = patientDetails.population;
                            console.log(patientDetails.population);

                            if (patientDetails.population == "General Population") {
                                var d = document.getElementById("GenPopulation");
                                d.className += " checked";
                            } else if(patientDetails.population == "Key Population") {
                                var d = document.getElementById("KeyPopulation");
                                d.className += " checked";
                            }
                            $('input:radio[name="Population"]').filter('[value="' + patientDetails.population +'"]').attr('checked', true);
                            $("#KeyPopulationCategoryId").val(patientDetails.PopulationCategoryId);

                            personAgeRule();
                        },
                        error: function (response) {
                            toastr.error(response.d, "Error Getting Person Details");
                        }
                    });
                }

            });

    </script>
</asp:Content>


