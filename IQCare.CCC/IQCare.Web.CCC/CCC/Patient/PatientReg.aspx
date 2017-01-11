<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientReg.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientReg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style type="text/css">
        label.error { float: none; color: red; padding-left: .5em; vertical-align: top; }
     </style>
    <div class="=col-md-12">
         <div id="errors"></div>
<%--     
        <div class="panel panel-default">
             <div class="panel-body">--%>
                 <%--<div class="col-md-12 label label-success"><label class="Control-label pull-left">1.PatientRegistration</label></div>--%>
                <%-- <div class="col-md-12"><hr/></div>--%>
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
                      
                           <div class="actions">
	                            <button type="button" class="btn btn-default btn-prev">
		                               <span class="glyphicon glyphicon-arrow-left"></span>Prev</button>
	                            <button type="button" class="btn btn-primary btn-next" id="next" data-last="Complete">Next
		                              <span class="glyphicon glyphicon-arrow-right"></span>
	                           </button>
                           </div>

                           <div class="step-content">
	                            
                               <div class="step-pane active sample-pane" id="datastep1" role="form" data-step="1">
                            
                                    <div class="col-md-12">
                                               <small class="pull-left text-primary">1.Patient Basic Details</small>
                                               <hr />
                                           </div>

		                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <div class="col-md-12"><label for="personFname" class="control-label pull-left">First Name</label></div>
                                                        <div class="col-md-12">
                                                             <asp:TextBox runat="server" ID="personFname" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="first name" required="true"></asp:TextBox>

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
                                                             <asp:TextBox runat="server" ID="personLName" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="last name" required="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                               </div>
                                                <div class="col-md-3">
                                                   <div class="col-md-12"><label for="Gender" class="control-label pull-left">Gender</label></div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="Gender" CssClass="form-control input-sm" ClientIDMode="Static" required="true"/>
                                                    </div>
                                                </div>
		                                   </div><%--.col-md-12  --%>    

                                    <div class="col-md-12" style="padding-top:2%">
                                               <div class="col-md-3">
                                                   <div class="form-group"><div class="col-md-12"><label for="PersonDoB" class="control-label pull-left text-primary">Social Status </label></div></div>
                                               </div>
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
                                                    <div class="col-md-5">
                                                         <div class="form-group">
                                                              <div class="col-md-12"><label for="personAge" class="control-label pull-left">Age(years)</label></div>
                                                             <div class="col-md-12">
                                                                  <asp:TextBox runat="server" ID="personAge" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="0" required="true"></asp:TextBox>
                                                             </div>
                                                         </div>
                                                    </div>
                                                   <div class="col-md-7">
                                                       <div class="form-group">
                                                            <div class="col-md-12"><label for="ChildOrphan" class="control-label"> (<18yr) Orphan</label></div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList runat="server" ID="ChildOrphan" CssClass="form-control input-sm" ClientIDMode="Static" required="true"/>    
                                                            </div>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div class="col-md-3">
                                                    <div class="form-group">
                                                         <div class="col-md-12"><label for="Inschool" class="control-label pull-left">In School ? </label></div>
                                                         <div class="col-md-12">
                                                             <asp:DropDownList runat="server" ID="Inschool" CssClass="form-control input-sm" ClientIDMode="Static" required="true"/>
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
                                                   <asp:TextBox type="text" runat="server" id="NationalId" name="NationalId" class="form-control input-sm" placeholder="national id no.." ClientIDMode="Static" required="true" />
                                               </div>
                                         </div>
                                         
                                         <div class="col-md-3">
                                             <div class="col-md-12"><label for="MaritalStatusId" class="control-label pull-left">Marital Status </label></div>
                                             <div class="col-md-12">
                                                  <asp:DropDownList runat="server" ID="MaritalStatusId" class="form-control input-sm" ClientIDMode="Static" required="true"></asp:DropDownList>
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
                                                  <asp:TextBox runat="server" type="text" id="GurdianFNames" name="GurdianFNames" class="form-control input-sm" placeholder="gurdian first name.." ClientIDMode="Static" required="true" />
                                             </div>
                                        </div>
                                              
                                        <div class="col-md-3">
                                              <div class="col-md-12"><label class="control-label pull-left">Middle Name</label></div>
                                              <div class="col-md-12">
                                                    <asp:TextBox runat="server" type="text" id="GurdianMName" name="GurdianMName" class="form-control input-sm" placeholder="guardian Middle name" ClientIDMode="Static"  />
                                              </div>
                                        </div>
                                               
                                        <div class="col-md-3">
                                            <div class="col-md-12"><label class="control-label pull-left">Last Name</label></div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="GurdianLName" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="guardian last name" required="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-12"><label class="control-label pull-left">Guradian Gender</label></div>
                                            <div class="col-md-12">
                                                <asp:DropDownList runat="server" ID="GuardianGender" ClientIDMode="Static" CssClass="form-control input-sm" required="true"/>
                                            </div>
                                        </div>
                                     </div>           
                          
                                           
	                            </div><%-- .step-pane--%>

	                            <div class="step-pane sample-pane " data-step="2">      
                                     <div class="col-md-12">
                                               <small class="pull-left text-primary">2.Patient Location Details</small>
                                              <hr />
                                          </div><%-- .col-md-12--%>
                                         
                                     <div class="col-md-12">
                                               <div class="col-md-3">
                                                   <div class="form-group">
                                                        <div class="col-md-12"><label class="control-label pull-left">County</label></div>
                                                       <div class="col-md-12">
                                                           <asp:DropDownList runat="server" ID="countyId" ClientIDMode="Static" CssClass="form-control input-sm" OnSelectedIndexChanged="countyId_SelectedIndexChanged" AutoPostBack="True" required="true"/>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div class="col-md-3">
                                                   <div class="form-group">
                                                       <div class="col-md-12"><label class="control-label pull-left">Sub-County</label></div>
                                                       <div class="col-md-12">
                                                           <asp:DropDownList runat="server" ID="SubcountyId" ClientIDMode="Static" CssClass="form-control input-sm" OnSelectedIndexChanged="SubcountyId_SelectedIndexChanged" AutoPostBack="True" required="true"/>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div class="col-md-3">
                                                  <div class="form-group">
                                                      <div class="col-md-12"><label class="control-label pull-left">Ward</label></div>
                                                      <div class="col-md-12">
                                                          <asp:DropDownList runat="server" ID="WardId" CssClass="form-control input-sm" ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="WardId_SelectedIndexChanged" required="true"/>
                                                      </div>
                                                  </div>
                                              </div>
                                               <div class="col-md-3">
                                                  <div class="form-group">
                                                      <div class="col-md-12"><label class="control-label pull-left">Local Council</label></div>
                                                      <div class="col-md-12">
                                                          <asp:TextBox runat="server" ID="LocalCouncils" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="local council.." ></asp:TextBox>
                                                      </div>
                                                  </div>
                                               </div>
                                            </div>

                                     <div class="col-md-12" style="padding-top:2%;padding-bottom:3%">
                                               <div class="col-md-3">
                                                   <div class="form-group">
                                                        <div class="col-md-12"><label class="control-label pull-left">Location</label></div>
                                                       <div class="col-md-12">
                                                           <asp:TextBox runat="server" ID="PatientLocation" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="location .."></asp:TextBox>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div class="col-md-3">
                                                   <div class="form-group">
                                                       <div class="col-md-12"><label class="control-label pull-left">Sub Location</label></div>
                                                       <div class="col-md-12">
                                                           <input type="text" runat="server" class="form-control input-sm" placeholder="sub location.." id="sublocation" name="sublocation" />
                                                       </div>
                                                   </div>
                                               </div>
                                               <div class="col-md-3">
                                                  <div class="form-group">
                                                      <div class="col-md-12"><label class="control-label pull-left">Landmark</label></div>
                                                      <div class="col-md-12">
                                                          <asp:TextBox runat="server" class="form-control input-sm" placeholder="landmark.." id="PatientLandmark" name="landmark" required="true" />
                                                      </div>
                                                  </div>
                                              </div>
                                               <div class="col-md-3">
                                                  <div class="form-group">
                                                      <div class="col-md-12"><label class="control-label pull-left">Nearest Health Centre</label></div>
                                                      <div class="col-md-12">
                                                           <asp:TextBox runat="server" ID="NearestHealthCentre" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="nearest health centre" required="true" ></asp:TextBox>
                                                      </div>
                                                  </div>
                                              </div>
                                          </div>                                                
                                </div><%-- .step-pane-2--%>

	                            <div class="step-pane sample-pane " data-step="3">
                                     <div class="col-md-12">
                                                <small class="text-primary pull-left"> 3. Patient Conatcts </small>
                                               <hr />
                                           </div>

                                     <div class="col-md-12" style="padding-bottom:3%">
                                               <div class="col-md-4">
                                                   <div class="form-group">
                                                        <div class="col-md-12"><label class="control-label pull-left">Postal Address</label></div>
                                                       <div class="col-md-12">
                                                           <asp:TextBox type="text" runat="server" id="PatientPostalAddress" name="PatientPostalAddress" class="form-control input-sm" placeholder="postal address" required="true" ClientIDMode="Static"/>
                                                      </div>
                                                   </div>
                                               </div>
                                              <div class="col-md-2">
                                                   <div class="form-group">
                                                       <div class="col-md-12"><label class="control-label pull-left">Mobile No.</label></div>
                                                       <div class="col-md-12">
                                                           <asp:TextBox  runat="server" id="PatientMobileNo" name="PatientMobileNo" class="form-control input-sm" placeholder="Mobile No..." ClientIDMode="Static" required="true" />
                                                      </div>
                                                   </div>
                                              </div>
                                              <div class="col-md-2">
                                                   <div class="form-group">
                                                       <div class="col-md-12"><label class="control-label pull-left">Alt. Mobile No.</label></div>
                                                       <div class="col-md-12">
                                                           <asp:TextBox runat="server" id="PatientAlternativeMobile" name="PatientAlternativeMobile" class="form-control input-sm" placeholder="alternative mobile no..." ClientIDMode="Static"  />
                                                      </div>
                                                   </div>
                                              </div>

                                              <div class="col-md-4">
                                                  <div class="form-group">
                                                      <div class="col-md-12"><label class="control-label pull-left">Email Address</label></div>
                                                      <div class="col-md-12">
                                                           <asp:TextBox runat="server" id="PatientEmailAddress" name="PatientEmailAddress" class="form-control input-sm" placeholder="email address" ClientIDMode="Static" />
                                                      </div>
                                                  </div>
                                              </div>
                                          </div>
                                    
                                   
                                    <div class="col-md-12"><label class=" control-label text-primary  pull-left">Treatment Supporter Information</label></div>
                                     <div class="col-md-12"><hr/></div>
                                    <div class="col-md-12 form-group">
                                         <div class="col-md-3">
                                              <div class="col-md-12"><label class="control-label pull-left">First Name</label></div>
                                              <div class="col-md-12">
                                                   <asp:TextBox runat="server"  CssClass="form-control input-sm" placeholder="first name..." ID="tsFname" ClientIDMode="Static" required="true"></asp:TextBox>
                                               </div>
                                         </div>

                                         <div class="col-md-3">
                                             <div class="col-md-12"><label class="control-label pull-left">Middle Name</label></div>
                                             <div class="col-md-12">
                                                  <asp:DropDownList runat="server" ID="tsMiddleName" ClientIDMode="Static" CssClass="form-control input-sm"/>
                                             </div>
                                         </div>

                                         <div class="col-md-3">
                                              <div class="col-md-12"><label class="control-label pull-left">Last Name</label></div>
                                              <div class="col-md-12">
                                                   <asp:TextBox runat="server" ID="tsLastName" ClientIDMode="Static" CssClass="form-control input-sm" required="true"></asp:TextBox>
                                              </div>
                                         </div>

                                         <div class="col-md-3">
                                              <div class="form-group">
                                                  <div class="col-md-12"><label class="control-label pull-left">Gender </label></div>
                                                  <div class="col-md-12">
                                                      <asp:DropDownList runat="server" ID="tsGender" ClientIDMode="Static" CssClass="form-control input-sm" required="true"/>
                                                  </div>
                                              </div>
                                         </div>
                                    </div>
                                    
                                    <div class="col-md-12 form-group">
                                         <div class="col-md-3">
                                              <div class="form-group">
                                                  <div class="col-md-12"><label class="control-label pull-left">Mobile Contact </label></div>
                                                  <div class="col-md-12">
                                                      <asp:TextBox runat="server" CssClass="form-control input-sm" id="TSContacts" placeholder="mobile no.." required="true"></asp:TextBox>
                                                  </div>
                                              </div>
                                         </div> 
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                    </div>
	                            </div><%-- .step-pane-3--%>

                                <div class="step-pane sample-pane " data-step="4">
                                     
                                     <div class="col-md-12">
                                                <small class="pull-left text-primary"> 4. Patient population categorization</small>
                                               <hr />
                                           </div>

                                     <div class="col-md-12" style="padding-top:2%;padding-bottom:3%">
                                               <div class="col-md-3">
                                                    <div class="form-group"><label class="control-label pull-left text-primary">Population type</label></div>
                                               </div>
                                               <div class="col-md-4">
                                                    <div class="form-group">
                                                         <div class="form-group"><div class="col-md-12"><label class="control-label pull-left">Select Population type</label></div></div>
                                                        
                                                             <div class="col-md-12 radio">
                                                                  <label class="radio-custom  pull-left" data-initialize="radio" id="GenPopulation">
                                                                    <input class="sr-only" id="rdbGenPopulation" name="Population" type="radio" value="option1">
                                                                    Genenal Population
                                                                  </label>
                                                                </div>

                                                                <div class="col-md-12 radio checked">
                                                                  <label class="radio-custom pull-left" data-initialize="radio" id="KeyPopulation">
                                                                    <input class="sr-only" checked="checked" id="rdbKeyPopulation" name="Population" type="radio" value="option2">
                                                                    Key population
                                                                  </label>
                                                                </div>
                                                        
                                                    </div>
                                               </div>
                                               <div class="col-md-3">
                                                    <div class="form-group">
                                                         <div class="col-md-12"><label class="control-label pull-left">Select if Key.Pop</label></div>
                                                        <div class="col-md-12">
                                                             <select runat="server" class="form-control" id="KeyPopulationChoose" name="KeyPopulationChoose"></select>
                                                        </div>
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

                $('#MyDateOfBirth')
                    .datepicker({
                        allowPastDates: true,
                        momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
                        restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
                    });

                $('#MyDateOfBirth').on('changed.fu.datepicker dateClicked.fu.datepicker', function(event,date) {
                    var x = $('#MyDateOfBirth').datepicker('getDate');

                    $('#<%=personAge.ClientID%>').val(getAge(x));
                });

                var personAge = 0;

                /* Business Rules setup */
                $('#<%=personAge.ClientID%>')
                    .on("change",function() {
                        personAge = $(this).val();
                        if (personAge >= 18) {
                            
                            $("#<%=NationalId.ClientID%>").removeAttr('disabled');
                            $("#<%=MaritalStatusId.ClientID%>").removeAttr('disabled');
                             
                            $("#<%=ChildOrphan.ClientID%>").Attr('disabled','disabled');
                            $("#<%=Inschool.ClientID%>").Attr('disabled','disabled');
                            $("#<%=GurdianFNames.ClientID%>").attr('disabled', 'disbaled');
                            $("#<%=GurdianMName.ClientID%>").attr('disabled', 'disbaled');
                            $("#<%=GurdianLName.ClientID%>").attr('disabled', 'disbaled');
                            $("#<%=GuardianGender.ClientID%>").attr('disabled', 'disbaled');
                        } else {
                            $("#<%=ChildOrphan.ClientID%>").removeAttr('disabled');
                            $("#<%=Inschool.ClientID%>").removeAttr('disabled');
                            $("#<%=GurdianFNames.ClientID%>").removeAttr('disabled', 'disbaled');
                            $("#<%=GurdianMName.ClientID%>").removeAttr('disabled', 'disbaled');
                            $("#<%=GurdianLName.ClientID%>").removeAttr('disabled', 'disbaled');
                            $("#<%=GuardianGender.ClientID%>").removeAttr('disabled', 'disbaled');
                        }
                    });
                

                /* make readonly by default */
                $("#<%=ChildOrphan.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=Inschool.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=NationalId.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=MaritalStatusId.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianFNames.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianMName.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GurdianLName.ClientID%>").attr('disabled', 'disbaled');
                $("#<%=GuardianGender.ClientID%>").attr('disabled', 'disbaled');
                <%--$("#<%=.ClientID%>").atr('disabled', 'disbaled');--%>

                $('#<%=countyId.ClientID%>')
                    .change(function() {
                        var county = $('#<%=countyId.ClientID%>').Text();

                    });

                $("#myWizard")
                    .on("actionclicked.fu.wizard", function(evt, data) {
                            var currentStep = data.step;
                            var nextStep = 0;
                            var previousStep = 0;
                            /*var form = $("form[name='form1']");*/
                           

                            if (data.direction === 'next')
                               nextStep=currentStep += 1;
                            else
                               previousStep=nextStep -= 1;
                            if (data.step === 1) {
                                var validator = $("#form1").validate();
                                validator.element("#datastep1");

                                //var fields =$("#form1").find("#step-content").find("#datastep1").find(".form-control");
                                var isValid = validator.valid();
                                
                                if (isValid) {
                                    $("#myWizard").wizard('next');
                                } else {
                                    evt.preventDefault();
                                }
                            }
                            else if (data.step === 2) {
                                
                            }
                            else if (data.step === 3) {
                                
                            }
                            else if (data.step===4) {
                                
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

                //$("#myWizard").on('actionclicked.fu.wizard', function(evt,data) {
                //    var step = data.step;
                   
                //    if (data.direction === 'next')
                //        step += 1;
                //    else
                //        step -= 1;
                //    alert(step);


                //});

                

                //$('[id^=next]').on('click', function () {
                //    var curstep = $('#myWizard').wizard('selectedItem');
                //    var form = $("form[name='Form1']");
                //    var fields = form.find('#step-' + curstep.step).find(":input");
                //    if (fields.valid()) {
                //        $('#myWizard').wizard('next');
                //    } else {
                //        alert("invalid");
                //    }
                //});

                //$.validator.addMethod("lettersonly", function (value, element) {
                //    return this.optional(element) || /^[a-z]+$/i.test(value);
                //}, "Letters only please");

                $.validator.addMethod("CheckDropDownList", function (value, element, param) {  
                    if (value === '0')  
                        return false;  
                    else  
                        return true;  
                },"Please select a Department."); 

                $.validator.methods.email = function( value, element ) {
                    return this.optional( element ) || /[a-z]+@[a-z]+\.[a-z]+/.test( value );
                }


                $("#form1")
                    .validate({
                        errorElement: 'div',
                        errorClass:'error',
                        focusInvalid:false,
                        ignore:"",
                        rules: {
                            personFname: {
                                requred: true,
                                minLength: 4
                            },
                            personLName: {
                                required: true,
                                minLength: 4
                            },
                            Gender: {
                                require:true,
                                CheckDropDownList:true
                            },
                            personAge: {
                                required: true,
                                digits:true
                            },
                            NationalId: {
                                required:true,
                                maxLength:8
                            },
                            MaritalStatusId: {
                                required: true,
                                CheckDropDownList:true
                            },
                            tsFname: {
                                required:{
                                      depends:function() {
                                          if (personAge >= 18) {
                                              return false;
                                          } else {
                                              return true;
                                          }
                                      } 
                            },
                                lettersonly:true
                            },
                            tsLastName: {
                                required: {
                                    depends:function () {
                                        if (personAge >= 18) {
                                            return false;
                                        } else {
                                            return true;
                                        }
                                    }
                                },
                                lettersonly:true
                            },
                            GuardianGender: {
                                required: {
                                    depends:function() {
                                        if (personAge >= 18) {
                                            return false;
                                        } else {
                                            return true;
                                        }
                                    }
                                }
                            }
                        },
                        messages: {
                            personFname: {
                                requred: "First name Required!",
                                minLength: "Minimum length is 4"
                            },
                             personLName: {
                                required: "Last name Required!",
                                minLength: "minimum length is 4",
                                lettersonly:"invalid characters"
                            },
                            Gender: {
                                require:"Gender Required!",
                                CheckDropDownList:"Select Gender"
                            },
                            personAge: {
                                required: "Age Required",
                                digits:"Invalid characters in Age"
                            },
                            NationalId: {
                                required:"National Id Required!",
                                maxLength:"Id should not be more than 8"
                            },
                            MaritalStatusId: {
                                required: "Marital Status Required",
                                CheckDropDownList:"Select marital Status"
                            },
                            tsFname: {
                                required: "Treatment support first name required!",
                                lettersonly:"invalid character(s)"
                            },
                            tsLastName: {
                                required: "Treatment support last name required!",
                                lettersonly:"Invalid characters"
                            },
                            GuardianGender: {
                                required: "Guardian gender Required!"                              
                            }
                        },
                        highlight: function (e) {
                            $(e).closest('.form-group').removeClass('has-info').addClass('has-error');
                        },
                        
                        success: function (e) {
                            $(e).closest('.form-group').removeClass('has-error').addClass('has-info');
                            $(e).remove();
                        },

                        errorPlacement: function (error, element) {
                            if(element.is('input[type=checkbox]') || element.is('input[type=radio]')) {
                                var controls = element.closest('div[class*="col-"]');
                                if(controls.find(':checkbox,:radio').length > 1) controls.append(error);
                                else error.insertAfter(element.nextAll('.lbl:eq(0)').eq(0));
                            }
                            else if(element.is('.select2')) {
                                error.insertAfter(element.siblings('[class*="select2-container"]:eq(0)'));
                            }
                            else if(element.is('.chosen-select')) {
                                error.insertAfter(element.siblings('[class*="chosen-container"]:eq(0)'));
                            }
                            else error.insertAfter(element.parent());
                        },
                        invalidHandler:function(event, validator) {
                            var errors = validator.numberOfInvalids();
                            if (errors) {
                                var message = errors === 1
                                    ? 'You Missed 1 field.It hasbeen highlighted'
                                    : 'You missed ' + errors + 'fields.They have been highligted';
                                $("div.error span").html(message).addClass("has-error");
                                //$(this).defaultShowErrors();
                                $("div.error").show();
                            } else {
                                $("div.error").hide();
                            }
                        },
                        submitHandler:function(form) {
                            /*submit form here */
                        }
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
                    return age;
                }


            });
    </script>
</asp:Content>
