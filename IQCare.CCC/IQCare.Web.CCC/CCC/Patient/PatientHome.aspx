<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

<%--<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>--%>

<%--<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>--%>
<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
      <%--  <div class="col-md-12 col-xs-12 col-sm-12" style="margin-top: 0%;padding-top: 0%">--%>
            <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
        <%--</div>--%>

<%--    <div class="col-md-12 col-xs-12 col-sm-12 form-group">
        <div class="col-md-6 col-xs-12 col-sm-6">
            
              <div id="vl_container" style="min-width: 300px; height: 350px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-6 col-xs-12 col-sm-6">
             <div id="vitals_container" style="min-width: 300px; height: 350px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>
    </div>--%>
    <div class="col-md-12 col-xs-12 col-sm-12">
         <div class="panel panel-default">
              <div class="panel-body">
                  <div class="col-md-12">
                      
                      <div class="col-md-11">
                           <div class="col-md-3">
                                 <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Last ViralLoad :</asp:Label></h5></div>
                                 <div class="col-md-12">
                                    <h6> <asp:Label runat="server" ID="lblVL" CssClass="text-info pull-left"> </asp:Label></h6>
                                </div>
                           </div>
                          <div class="col-md-3">
                               <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> VL Due Date :</asp:Label></h5></div>
                          </div>
                          <div class="col-md-3">
                                <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Current Regimen :</asp:Label></h5></div>
                          </div>
                          <div class="col-md-3">
                                <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Adherance Status :</asp:Label></h5></div>
                          </div>
                      </div>
                  </div><!-- .col-md-12 -->
                 
              </div><!-- .panel- body-->
         </div><!-- .panel-->
    </div>
    
    <div class="col-md-12 col-xs-12 col-sm-12">
      
        <ul class="nav nav-tabs">

            <li class="active"><a data-toggle="tab" href="#Registration">Registration Summary </a></li>
            <li class="disabled"><a data-toggle="tab" href="#EntryPoint"><strong>Entry Point & Transfer Status</strong></a> </li>
            <li class="disabled"><a data-toggle="tab" href="#Baseline">Baseline Assessment & Treament Initiation </a></li>
            <li class="disabled"><a data-toggle="tab" href="#Trending">Viral Load & BMI Trending </a></li>          

        </ul>
    
        <div class="col-md-12 col-xs-12 col-xs-12 form-group">
            <div class="col-md-12 form-group"></div> 
            <div class="tab-content">
                 <div id="Registration" class="tab-pane fade in active">
                     <div class="col-md-12 col-xs-12">
                         <!--<div class="col-md-4 col-xs-4 col-sm-4">-->
                             <!--<div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong><h6>Patient Bio</h6></label></div>
                             <div class="col-md-12"><hr style="margin-top:1%"/></div>-->
                             <!--
                             <div class="col-md-12 form-group">
                                 <div class="col-md-6"><label class="control-label pull-left">First Name:</label></div>
                                 <div class="col-md-6">                     
                                     <asp:Label ID="txtFirstName" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-6"><label class="control-label pull-left">Middle Name:</label></div>
                                 <div class="col-md-6">
                                     <asp:Label ID="txtMiddleName" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-6"><label class="control-label pull-left">Last Name:</label></div>
                                 <div class="col-md-6">
                                     <asp:Label ID="txtLastName" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-6" style="padding: 0"><label class="control-label pull-left">Patient Population:</label></div>
                                 <div class="col-md-6" style="padding: 0">                             
                                     <asp:Label ID="drpPatientPopulation" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div> -->
                         <!--</div>-->
                         <div class="col-md-4 col-xs-4 col-sm-4">
                             
                             
                             <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong><h6>Patient Treatment Supporter</h6></label></div>
                             <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-4" style="padding: 0;"><label class="control-label pull-left">Names:</label></div>
                                 <div class="col-md-8" style="padding: 0;">
                                     <asp:Label ID="txtSupporterNames" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-4" style="padding: 0;"><label class="control-label pull-left">Mobile:</label></div>
                                 <div class="col-md-8" style="padding: 0;">
                                     <asp:Label ID="txtSupporterMobile" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             <!-- Modal -->
                             <div id="patientBioModal" class="modal fade" role="dialog">
                                 <div class="modal-dialog">
                                     <!-- Modal content-->
                                     <div class="modal-content">
                                         <div class="modal-header">
                                             <!--<button type="button" class="close" data-dismiss="modal">&times;</button>-->
                                             <h4 class="modal-title">Edit Patient Bio</h4>

                                         </div>
                                         <div class="modal-body">
                                             <div class="row">
                                                 
                                                 <div class="col-md-12 form-group">
                                                     <div class="col-md-3"><label class="control-label pull-left">First Name:</label></div>
                                                     <div class="col-md-6">                     
                                                         <asp:TextBox ID="bioFirstName" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>
                                                     </div>
                                                 </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     <div class="col-md-3"><label class="control-label pull-left">Middle Name:</label></div>
                                                     <div class="col-md-6">                     
                                                         <asp:TextBox ID="bioMiddleName" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>
                                                     </div>
                                                 </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     <div class="col-md-3"><label class="control-label pull-left">Last Name:</label></div>
                                                     <div class="col-md-6">                     
                                                         <asp:TextBox ID="bioLastName" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>
                                                     </div>
                                                 </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     <div class="col-md-3"><label class="control-label pull-left">Patient Population:</label></div>
                                                     <div class="col-md-6">
                                                         <asp:DropDownList ID="bioPatientPopulation" runat="server" ClientIDMode="Static" CssClass="pull-left form-control"></asp:DropDownList>
                                                     </div>
                                                 </div>

                                             </div>                                        
                                         </div>
                                         <div class="modal-footer">
                                             <div class="col-md-12 form-group">
                                                 
                                                <div class="col-md-6">
                                                    <button type="button" id="btnSaveBio" class="btn btn-default" OnClientClick="return false;">Save</button>
                                                </div>

                                                <div class="col-md-6">
                                                    <button type="button" id="btnCancelBio" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>

                                            </div>

                                             

                                         </div>

                                     </div>

                                 </div>

                             </div>
                             
                             
                             

                             <!-- Modal -->
                             <div id="treatmentSupporterModal" class="modal fade" role="dialog">
                                 <div class="modal-dialog">
                                     <!-- Modal content-->
                                     <div class="modal-content">
                                         <div class="modal-header">
                                             <!--<button type="button" class="close" data-dismiss="modal">&times;</button>-->
                                             <h4 class="modal-title">Add Patient Treatment Supporter</h4>

                                         </div>
                                         <div class="modal-body">
                                             <div class="row">
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">First Name:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="trtFirstName" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Gender:</label></div>
                                                         <div class="col-md-12">                                                           
                                                             <asp:DropDownList ID="trtGender" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:DropDownList>
                                                         </div>
                                                     </div>
                                                </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Middle Name:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="trtMiddleName" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Mobile:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="trtMobile" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Last Name:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="trtLastName" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                     
                                                     
                                                </div>

                                             </div>
                                             
                                         </div>
                                         <div class="modal-footer">
                                             <button id="btnAddPatientTreatmentSupporter" type="button" class="btn btn-default" OnClientClick="return false;">Add Patient Treatment Supporter</button>
                                             <button id="btnAddPatientTreatmentSupporterCancel" type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                                         </div>

                                     </div>

                                 </div>

                             </div>

                             <div class="col-md-6 form-group">
                                 <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#patientBioModal">Edit Patient Bio</button>
                             </div>

                             <div class="col-md-6 form-group">
                                 <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#treatmentSupporterModal">Add Patient Supporter</button>
                             </div>

                         </div>
                         <div class="col-md-4 col-xs-4 col-sm-4">
                             <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong><h6>Patient Location</h6></label></div>
                             <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-3"><label class="control-label pull-left">County:</label></div>
                                 <div class="col-md-3">
                                     <asp:Label ID="txtCounty" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             <!--</div>
                             
                             <div class="col-md-12 form-group">-->
                                 <div class="col-md-3"><label class="control-label pull-left">Ward:</label></div>
                                 <div class="col-md-3">
                                     <asp:Label ID="txtWard" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                                       
                             <div class="col-md-12 form-group">
                                 <div class="col-md-3"><label class="control-label pull-left">Village:</label></div>
                                 <div class="col-md-3">
                                     <asp:Label ID="txtVillage" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             <!--</div>

                             <div class="col-md-12 form-group">-->
                                 <div class="col-md-4" style="padding: 0;"><label class="control-label pull-left">Nearest H/C:</label></div>
                                 <div class="col-md-2" style="padding: 0;">
                                     <asp:Label ID="txtNearestHealthCentre" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                             <!-- Modal -->
                             <div id="patientLocationModal" class="modal fade" role="dialog">
                                 <div class="modal-dialog">
                                     <!-- Modal content-->
                                     <div class="modal-content">
                                         <div class="modal-header">
                                             <button type="button" class="close" data-dismiss="modal">&times;</button>
                                             <h4 class="modal-title">Add Patient Location</h4>

                                         </div>
                                         <div class="modal-body">
                                             <div class="row">
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">County:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:DropDownList ID="smrCounty" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:DropDownList>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Sub-Location:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="smrSubLocation" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                    
                                                </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Sub-County:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:DropDownList ID="smrSubCounty" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:DropDownList>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Village:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="smrVillage" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Ward:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:DropDownList ID="smrWard" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:DropDownList>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Landmark:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="smrLandmark" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Location:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="smrLocation" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Nearest Health Center:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="smrNearestHealthCenter" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                </div>

                                             </div>
                                             
                                         </div>
                                         <div class="modal-footer">
                                             <button id="btnAddLocation" type="button" class="btn btn-default" OnClientClick="return false;">Add Location</button>
                                             <button id="btnAddLocationCancel" type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                                         </div>

                                     </div>

                                 </div>

                             </div>
                             
                             <div class="col-md-12 form-group">
                                 <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#patientLocationModal">Add Patient Location</button>
                             </div>

                         </div>
                         <div class="col-md-4 col-xs-4 col-sm-4">
                             <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong><h6>Patient Contact</h6></label></div>
                             <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-6" style="padding: 0;"><label class="control-label pull-left">Postal Address:</label></div>
                                 <div class="col-md-6" style="padding: 0;">
                                     <asp:Label ID="txtPostalAddress" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                             
                             <div class="col-md-12 form-group">
                                 <div class="col-md-6" style="padding: 0;"><label class="control-label pull-left">Mobile:</label></div>
                                 <div class="col-md-6">
                                     <asp:Label ID="txtMobile" runat="server" ClientIDMode="Static" CssClass="pull-left text-primary"></asp:Label>
                                 </div>
                             </div>
                             
                            <!-- Modal -->
                             <div id="patientContactModal" class="modal fade" role="dialog">
                                 <div class="modal-dialog">
                                     <!-- Modal content-->
                                     <div class="modal-content">
                                         <div class="modal-header">
                                             <!--<button type="button" class="close" data-dismiss="modal">&times;</button>-->
                                             <h4 class="modal-title">Edit Patient Contacts</h4>

                                         </div>
                                         <div class="modal-body">
                                             <div class="row">
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Postal Address:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="patPostalAddress" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Mobile:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="patMobile" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                </div>
                                                 
                                                 <div class="col-md-12 form-group">
                                                     
                                                    <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Email Address:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="patEmailAddress" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                     
                                                     <div class="col-md-6">
                                                         <div class="col-md-12"><label class="control-label pull-left">Alternative Mobile:</label></div>
                                                         <div class="col-md-12">
                                                             <asp:TextBox ID="patAlternativeMobile" runat="server" CssClass="pull-left form-control" ClientIDMode="Static"></asp:TextBox>

                                                         </div>
                                                     </div>
                                                </div>

                                             </div>
                                             
                                         </div>
                                         <div class="modal-footer">
                                             <button id="btnEditPatientContacts" type="button" class="btn btn-default" OnClientClick="return false;">Edit Patient Contacts</button>
                                             <button id="btnEditPatientContactsCancel" type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                                         </div>

                                     </div>

                                 </div>

                             </div>
                             
                             <div class="col-md-12 form-group">
                                 <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#patientContactModal">Edit Patient Contacts</button>
                             </div>
                         </div>
                         

                     </div>
                 </div> 
                 <div id="EntryPoint" class="tab-pane fade">
                     <div class="col-md-12 col-xs-12">
                         
                          <div class="col-md-4 col-xs-4 col-sm-4">
                               <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong> <h6>TransferIn Status</h6></label></div>
                              <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                              <div class="col-md-12 form-group">
                                   <div class="col-md-6"><label class="control-lable pull-left">Entry Point :</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblEntryPoint" ClientIDMode="Static"></asp:Label></div>
                              </div>
                                 
                               <div class="col-md-12">
                                   <div class="col-md-6"><label class="control-lable pull-left">TransferIn Date :</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblTransferinDate" ClientIDMode="Static"></asp:Label></div>
                              </div>
                              
                             <div class="col-md-12">
                                   <div class="col-md-6"><label class="control-lable pull-left">Treatment Start:</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary" ID="lblTreatmentStartDate" ClientIDMode="Static"></asp:Label></div>
                              </div>
                              
                              <div class="col-md-12">
                                   <div class="col-md-4"><label class="control-lable pull-left">Regimen : </label></div>
                                  <div class="col-md-8"><asp:Label runat="server" CssClass=" pull-right text-primary"  ID="lblTIRegimen" ClientIDMode="Static"></asp:Label></div>
                              </div>
                            
                              <div class="col-md-12">
                                   <div class="col-md-5"><label class="control-lable pull-left">Facility From :</label></div>
                                  <div class="col-md-7"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblFacilityFrom" ClientIDMode="Static"></asp:Label></div>
                              </div>

                              

                          </div><!-- .col-md-4 -->

                          <div class="col-md-4 col-xs-12 col-sm-12">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6>Patient Diagnosis Status</h6></label></div>
                               <div class="col-md-12"><hr style="margin-top:1%"/></div>
                              
                               <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">HIV Diagnosis Date :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblDateOfHivDiagnosis" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>
                              
                              <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">Date of Enrollmet  :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblDateOfEnrollment" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>
                              
                               <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">WHO at Enrollmet  :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblWhoStage" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>
                              
                              
                              <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">ART Initiation Date :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblARTInitiationDate" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>


                          </div><!-- .col-md-4 -->

                          <div class="col-md-4 col-xs-4 col-sm-4">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6>ARV History Status</h6> </label></div>
                              <div class="col-md-12"><hr style="margin-top:1%" class="bg-info"/></div>
                              <div class="col-md-12">
                                  <table class="table table-condensed" width="100%">
                                      <tr>
                                          <th align="left">Purpose</th>
                                          <th align="left">Regimen</th>
                                           <th right="left">Date Last Used</th>
                                      </tr>
                                      <tbody id="tbdArvHistory" clientidmode="Static"></tbody>
                                      <tfoot class="bg-info">
                                          <tr>
                                               <th align="left">Purpose</th>
                                          <th align="left">Regimen</th>
                                           <th right="left">Date Last Used</th>
                                          </tr>
                                      </tfoot>
                                  </table>
                              </div>
                          </div><!-- .col-md-4 -->

                     </div>
                 </div>
                 <div id="Baseline" class="tab-pane fade">
                     <div class="col-md-4 col-xs-4 col-sm-4">
                         <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong> <h6>Baseline Assessment Summary</h6></label></div>
                              <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                              <div class="col-md-12 form-group">
                                   
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">HBV Infected</label></div>
                                       <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblHbvInfectedYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblHbvInfectedNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>
                                                               
                                  <div class="col-md-12 form-group">
                                      <div class="col-md-6"><label class="control-lable pull-left">Pregnant </label></div>
                                       <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPregnantYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPregnantNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                      <div class="col-md-6"><label class="control-lable pull-left">TB Infected</label></div> 
                                      <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTBInfectedYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTBInfectedNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                      <div class="col-md-6"><label class="control-lable pull-left">BreastFeeding</label></div> 
                                      <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastfeedingYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastfeedingNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>

                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Who Stage</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblwhostage2" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                              
                                  <div class="col-md-12 form-group">
                                   <div class="col-md-6"><label class="control-lable pull-left">CD4 Count</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary" ID="lblcd4" ClientIDMode="Static"></asp:Label></div>
                              </div>
                              

                            </div>                      

                          </div>
                    
                     <div class="col-md-4 col-xs-4 col-sm-4">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6><strong>Patient Baseline Vitals</strong></h6> </label></div>
                              <div class="col-md-12"><hr style="margin-top:1%" class="bg-info"/></div>
                              <div class="col-md-12">
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">MUAC</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblmuac" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Weight</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblweight" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Height</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblheight" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">BMI</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblbmi" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                              </div>
                          </div>

                      <div class="col-md-4 col-xs-4 col-sm-4">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6><strong>Treatment Initiation </strong></h6> </label></div>
                              <div class="col-md-12"><hr style="margin-top:1%" class="bg-info"/></div>
                              <div class="col-md-12">
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-7"><label class="control-lable pull-left">Date Started Firstline:</label></div>
                                     <div class="col-md-5"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblFirstline" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">ART Cohort: </label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblcohort" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                    <div class="col-md-12 form-group">
                                       <div class="col-md-5"><label class="control-lable pull-left">Regimen: </label></div>
                                     <div class="col-md-7"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblRegimenName" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Baseline ViralLoad :</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblbaselineVL" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">ViralLoad Date :</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblBlDate" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                              </div>
                          </div>
                 </div> <!-- diagnosis -->  
                 <div id="Trending" class="tab-pane fade">
                    
                      <div class="col-md-6 col-xs-12 col-sm-12">
                          <div id="vl_container" margin: 0 auto"></div>  
                     </div>  <!-- .bs-component-->
                     
                     <div class="col-md-6 col-xs-12 col-sm-12">
                          <div id="vitals_container" margin: 0 auto"></div>     
                     </div><!-- .bs-component-->
                        
                </div><!-- .trending-->
   
             </div><!-- .tab-content -->
        </div> <!-- col-md-12 -->

    </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
     <!-- ajax begin -->
    <script type="text/javascript">

        $(document).ready(function() {

            var patientId = "<%=PatientId%>";
            
            /* populate patient baseline information */
            $.ajax({
                type: "POST",
                url: "../WebService/PatientBaselineService.asmx/GetPatientBaselineInfo",
                data: "{'patientId':'" + patientId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);
                    $.each(itemList,
                        function(index, itemList) {
                            if (itemList.patientId > 0) {
                                
                                /* transferin status */
                                $("#<%=lblTransferinDate.ClientID%>").text(moment(itemList.TransferInDate).format("DD-MMM-YYYY"));
                                $("#<%= lblTreatmentStartDate.ClientID%>").text(moment(itemList.TreatmentStartDate).format("DD-MMM-YYYY"));
                                $("#<%=lblTIRegimen.ClientID%>").text(itemList.CurrentTreatmentName);
                                $("#<%=lblFacilityFrom.ClientID%>").text(itemList.FacilityFrom);

                                /*patient Diagnosis */
                                $("#<%=lblDateOfHivDiagnosis.ClientID%>").text(moment(itemList.HivDiagnosisDate).format("DD-MMM-YYYY"));
                                $("#<%=lblDateOfEnrollment.ClientID%>").text(moment(itemList.EnrollmentDate).format("DD-MMM-YYYY"));
                                $("#<%=lblWhoStage.ClientID%>").text(itemList.EnrollmentWHOStageName);
                                $("#<%=lblARTInitiationDate.ClientID%>").text(moment(itemList.ARTInitiationDate).format("DD-MMM-YYYY"));
                                $("#<%=lblwhostage2.ClientID%>").text(itemList.WHOStageName);
                                $("#<%=lblcd4.ClientID%>").text(itemList.CD4Count);
                                $("#<%=lblmuac.ClientID%>").text(itemList.MUAC);
                                $("#<%=lblweight.ClientID%>").text(itemList.Weight);
                                $("#<%=lblheight.ClientID%>").text(itemList.Height);
                                $("#<%=lblbmi.ClientID%>").text(itemList.BMI.toFixed(2));

                                $("#<%=lblFirstline.ClientID%>").text(moment(itemList.DateStartedOnFirstline).format("DD-MMM-YYYY"));
                                $("#<%=lblcohort.ClientID%>").text(itemList.Cohort);
                                $("#<%=lblRegimenName.ClientID%>").text(itemList.RegimenName);
                                $("#<%=lblbaselineVL.ClientID%>").text(itemList.BaselineViralLoad + ' copies/ml');
                                $("#<%=lblBlDate.ClientID%>").text(moment(itemList.BaselineViralLoadDate).format("DD-MMM-YYYY"));
                                
                                /* patient baseline */
                                if (!itemList.HBVInfected) {
                                    $("#lblHbvInfectedNo").checkbox('check');
                                    $("#lblHbvInfectedYes").checkbox('uncheck');
                                } else {
                                    $("#lblHbvInfectedNo").checkbox('uncheck');
                                    $("#lblHbvInfectedYes").checkbox('check');
                                }

                                if (!itemList.Pregnant) {
                                    $("#lblPregnantNo").checkbox('check');
                                    $("#lblPregnantYes").checkbox('uncheck');
                                } else {
                                    $("#lblPregnantNo").checkbox('uncheck');
                                    $("#lblPregnantYes").checkbox('check');
                                }

                                if (!itemList.TBinfected) {
                                    $("#lblTBInfectedNo").checkbox('check');
                                    $("#lblTBInfectedYes").checkbox('uncheck');
                                } else {
                                    $("#lblTBInfectedNo").checkbox('uncheck');
                                    $("#lblTBInfectedYes").checkbox('check');
                                }

                                if (!itemList.BreastFeeding) {
                                    $("#lblBreastfeedingNo").checkbox('check');
                                    $("#lblBreastfeedingYes").checkbox('uncheck');
                                } else {
                                    $("#lblBreastfeedingNo").checkbox('uncheck');
                                    $("#lblBreastfeedingYes").checkbox('check');
                                }




                            }
                        });
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message);
                }
            });

            $.ajax({
                type: "POST",
                url: "../WebService/PatientBaselineService.asmx/GetPatientPriorArvHistory",
                data: "{'patientId':'" + patientId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#tbdArvHistory").append(response.d);
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message);
                }
            });


            var jan_height = "";
            var march_height = "";
            var jan_weight = "";
            var march_weight = "";
            var jan_BMI = "";
            var march_BMI = "";
            var jan_vl = "";
            var march_vl = "";

            window.onload = function() {

              $.when(getVitals()).then(function() {
                    setTimeout(function() {
                            vitals();
                        },
                        2000);
                });
              $.when(getViralLoad()).then(function () {
                    setTimeout(function () {
                        viralLoadGraph();
                    },
                        2000);
              });
            };

          function getViralLoad() {
                    console.log("get viral load  called");
                    $.ajax({
                        url: '../WebService/LabService.asmx/GetViralLoad',
                        type: 'POST',
                        dataType: 'json',
                        contentType: "application/json; charset=utf-8",
                        cache: false,
                        success: function (response) {
                            console.log(response.d);
                            var items = response.d;
                            items.forEach(function (item, i) {

                                if (item.Month == 1) {

                                    jan_vl = item.ResultValue;
                                   
                                } else if (item.Month == 3) {

                                    march_vl = item.ResultValue;                                   
                                   
                                }

                            });

                        }

                    });
                }
          function viralLoadGraph() {
            // $(function() {
                    $('#vl_container').highcharts({
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
                            plotLines: [
                                {
                                    value: 0,
                                    width: 1,
                                    color: '#808080'
                                }
                            ]
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
                        series: [
                            {
                                name: 'VL',
                                data: [jan_vl, march_vl, "", "", "", "", ""]
                            }, {
                                name: 'Threshold',
                                data: [1000, 1000, 1000, 1000, 1000, 1000, 1000]
                            }
                        ]
                    });
                };

            function getVitals() {
                console.log("get vitals called");
                $.ajax({
                    url: '../WebService/PatientVitals.asmx/GetVitals',
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    success: function(response) {
                        console.log(response.d);
                        var items = response.d;
                        items.forEach(function(item, i) {

                            if (item.Month == 1) {

                                jan_height = item.Height;
                                jan_weight = item.Weight;
                                jan_BMI = item.BMI;                               

                            } else if (item.Month == 3) {

                                march_height = item.Height;
                                march_weight = item.Weight;
                                march_BMI = item.BMI;
                              
                            }

                        });

                    }

                });
            }

            function vitals() {
                //var jan_height = $(".march_height").val();
                //var march_height = $(".march_height").val();
                console.log("vitals graph function called");

                $('#vitals_container').highcharts({
                    title: {
                        text: 'Vitals',
                        x: -20 //center
                    },
                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        categories: ['Jan', 'Mar', 'May', 'Jul', 'Sep', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        plotLines: [
                            {
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }
                        ]
                    },
                    tooltip: {
                        valueSuffix: ''
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: 'Height',
                            data: [jan_height, march_height, "", "", "", "", ""]
                        },
                        {
                            name: 'Weight',
                            data: [jan_weight, march_weight, "", "", "", "", ""]
                        },
                        {
                            name: 'BMI',
                            data: [jan_BMI, march_BMI, "", "", "", "", "", ""]
                        }
                    ]
                });
            }

            if (patientId > 0) {
                getPatientDetails(patientId);
            }

            $("#btnSaveBio").click(function() {
                var bioFirstName = escape($("#<%=bioFirstName.ClientID%>").val());
                var bioMiddleName = escape($("#<%=bioMiddleName.ClientID%>").val());
                var bioLastName = escape($("#<%=bioLastName.ClientID%>").val());
                var bioPatientPopulation = $("#<%=bioPatientPopulation.ClientID%>").val();
                var userId = <%=UserId%>;

                console.log(bioFirstName);
                console.log(bioMiddleName);
                console.log(bioLastName);
                console.log(bioPatientPopulation);

                if (patientId > 0) {
                    updatePatientBio(patientId, bioFirstName, bioMiddleName, bioLastName, userId, bioPatientPopulation);
                }            
            });

            $('#<%=smrCounty.ClientID%>').on("change", function() {
                getSubcountyList(); /*call AJAX function */
            });

            $("#<%=smrSubCounty.ClientID%>").on("change", function() {
                getWardList();
            });

            $("#btnAddLocation").click(function() {
                var personId = 0;
                var county = $("#<%=smrCounty.ClientID%>").val();
                var subcounty = $("#<%=smrSubCounty.ClientID%>").val();
                var ward = $("#<%=smrWard.ClientID%>").val();
                var village = $("#<%=smrVillage.ClientID%>").val();
                var location = $("#<%=smrLocation.ClientID%>").val();
                var subLocation = $("#<%=smrLocation.ClientID%>").val();
                var landmark = $("#<%=smrLandmark.ClientID%>").val();
                var nearestHc = $("#<%=smrNearestHealthCenter.ClientID%>").val();
                var userId = <%=UserId%>;


                if (patientId > 0) {

                    addNewLocation(personId, county, subcounty, ward, village, location, subLocation, landmark, nearestHc, userId);
                }
                
            });

            $("#btnEditPatientContacts").click(function() {
                var personId = 0;
                var physicalAddress = $("#<%=patPostalAddress.ClientID%>").val();
                var mobileNumber = $("#<%=patMobile.ClientID%>").val();
                var alternativeNumber = $("#<%=patAlternativeMobile.ClientID%>").val();
                var emailAddress = $("#<%=patEmailAddress.ClientID%>").val();
                var userId = <%=UserId%>;

                if (patientId > 0) {
                    editPatientContacts(personId, physicalAddress, mobileNumber, alternativeNumber, emailAddress, userId, patientId);
                }             
            });

            $("#btnAddPatientTreatmentSupporter").click(function() {
                var FirstName = $("#<%=trtFirstName.ClientID%>").val();
                var MiddleName = $("#<%=trtMiddleName.ClientID%>").val();
                var LastName = $("#<%=trtLastName.ClientID%>").val();
                var Gender = $("#<%=trtGender.ClientID%>").val();
                var Mobile = $("#<%=trtMobile.ClientID%>").val();
                var userId = <%=UserId%>;

                if (patientId > 0) {
                    addPatientTreatmentSupporter(patientId, FirstName, MiddleName, LastName, Gender, Mobile, userId);
                }
            });

        });

        function getPatientDetails(patientId) {
            $.ajax({
                type: "POST",
                url: "../WebService/PersonService.asmx/GetPersonDetails",
                data: "{'PatientId':'" + patientId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var patientDetails = JSON.parse(response.d);
                    console.log(patientDetails);

                    $("#<%=txtFirstName.ClientID%>").text(patientDetails.FirstName);
                    $("#<%=bioFirstName.ClientID%>").val(patientDetails.FirstName);

                    $("#<%=txtMiddleName.ClientID%>").text(patientDetails.MiddleName);
                    $("#<%=bioMiddleName.ClientID%>").val(patientDetails.MiddleName);

                    $("#<%=txtLastName.ClientID%>").text(patientDetails.LastName);
                    $("#<%=bioLastName.ClientID%>").val(patientDetails.LastName);

                    $("#<%=drpPatientPopulation.ClientID%>").text(patientDetails.population);
                    var populationType = 0;
                    if (patientDetails.population == "General Population") {
                        populationType = 74;
                    }
                    else if (patientDetails.population == "Key Population") {
                        populationType = 75;
                    }
                    $("#<%=bioPatientPopulation.ClientID%>").val(populationType);


                    /*bioFirstName
                    bioMiddleName
                    bioLastName
                    bioPatientPopulation*/

                    var names = null;
                    names = patientDetails.tsFname +
                        " " +
                        patientDetails.tsMiddleName +
                        " " +
                        patientDetails.tsLastName;

                    var ISContacts = "";
                    if (patientDetails.ISContacts != null && patientDetails.ISContacts != "") {
                        ISContacts = patientDetails.ISContacts;
                    }

                    $("#<%=txtSupporterNames.ClientID%>").text(names);
                    $("#<%=txtSupporterMobile.ClientID%>").text(ISContacts);

                    var village = "";
                    var nearestHealthCentre = "";

                    if(patientDetails.CountyId > 0)
                        $.when(GetLookupNameById(patientDetails.CountyId)).then();
                    if(patientDetails.Ward>0)
                        $.when(GetWardNameByWardId(patientDetails.Ward)).then();


                    if (patientDetails.Village != "" && patientDetails.Village != null) {
                        village = patientDetails.Village;
                    }

                    $("#<%=txtVillage.ClientID%>").text(village);

                    if (patientDetails.NearestHealthCentre != "" && patientDetails.NearestHealthCentre != null) {
                        nearestHealthCentre = patientDetails.NearestHealthCentre;
                    }
                    $("#<%=txtNearestHealthCentre.ClientID%>").text(nearestHealthCentre);

                    var PatientPostalAddress = "";
                    if (patientDetails.PatientPostalAddress != "" &&
                        patientDetails.PatientPostalAddress != null) {
                        PatientPostalAddress = patientDetails.PatientPostalAddress;
                    }
                    $("#<%=txtPostalAddress.ClientID%>").text(PatientPostalAddress);
                    var MobileNumber = "";
                    if (patientDetails.MobileNumber != "" && patientDetails.MobileNumber != null) {
                        MobileNumber = patientDetails.MobileNumber;
                    }
                    $("#<%=txtMobile.ClientID%>").text(MobileNumber);

                    $("#<%=patPostalAddress.ClientID%>").val(PatientPostalAddress);
                    $("#<%=patMobile.ClientID%>").val(MobileNumber);
                    $("#<%=patEmailAddress.ClientID%>").val(patientDetails.EmailAddress);
                    $("#<%=patAlternativeMobile.ClientID%>").val(patientDetails.AlternativeNumber);

                },
                error: function (response) {
                    toastr.error(response.d, "Error Getting Person Details");
                }
            });
        }

        function addPatientTreatmentSupporter(patientId, firstName, middleName, lastName, gender, mobile, userId) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientSummaryService.asmx/AddPatientTreatmentSupporter",
                data: "{'patientId':'"+ patientId + "','firstName':'" + firstName + "', 'middleName':'" + middleName + "', 'lastName':'" + lastName + "', 'gender': '" + gender + "','mobile': '" + mobile + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    var patientId = "<%=PatientId%>";
                    getPatientDetails(patientId);
                    toastr.success(response.d, "add Patient Treatment Supporter");
                },
                error: function (response) {
                    toastr.error(response.d, "Error Upating Patient Bio");
                }
            });
        }

        function editPatientContacts(personId, physicalAddress, mobileNumber, alternativeNumber, emailAddress, userId, patientid) {
            $.ajax({
                type: "POST",
                url: "../WebService/PersonService.asmx/AddPersonContact",
                data: "{'personId':'" + personId + "','physicalAddress':'" + physicalAddress + "','mobileNumber':'" + mobileNumber + "','alternativeNumber':'" + alternativeNumber + "','emailAddress':'" + emailAddress + "','userId':'" + userId + "','patientid':'" + patientid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    var patientId = "<%=PatientId%>";
                    getPatientDetails(patientId);
                    toastr.success(response.d, "Edit Patient Contacts");
                },
                error: function (response) {
                    toastr.error(response.d, "Error Adding New Patient Location");
                }
            });
        }

        function addNewLocation(personId, county, subcounty, ward, village, location, subLocation, landmark, nearestHc, userId) {
            $.ajax({
                type: "POST",
                url: "../WebService/PersonService.asmx/AddPersonLocation",
                data: "{'personId':'" + personId + "','county':'" + county + "','subcounty':'" + subcounty + "','ward':'" + ward + "','village':'" + village + "','location':'" + location + "','sublocation':'" + subLocation + "','landmark':'" + landmark + "','nearesthealthcentre':'" + nearestHc + "','userId':'" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    var patientId = "<%=PatientId%>";
                    getPatientDetails(patientId);
                    toastr.success(response.d, "add New Patient Location");
                },
                error: function (response) {
                    toastr.error(response.d, "Error Adding New Patient Location");
                }
            });
        }

        function updatePatientBio(patientId, bioFirstName, bioMiddleName, bioLastName, userId, bioPatientPopulation) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientSummaryService.asmx/UpdatePatientBio",
                data: "{'patientId':'" + patientId + "', 'bioFirstName':'" + bioFirstName + "', 'bioMiddleName':'" + bioMiddleName + "', 'bioLastName': '" + bioLastName + "','userId': '" + userId + "','bioPatientPopulation':'" + bioPatientPopulation + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                    getPatientDetails(patientId);
                    toastr.success(response.d, "Update Patient Bio");
                },
                error: function (response) {
                    toastr.error(response.d, "Error Upating Patient Bio");
                }
            });
        }

        function GetLookupNameById(Id) {
            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetCountyByCountyId",
                data: "{'Id':'" + Id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#<%=txtCounty.ClientID%>").text(response.d);
                },
                error: function (response) {
                    toastr.error(response.d, "Error Getting County");
                }
            });
        }

        function GetWardNameByWardId(Id) {
            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetWardNameByWardId",
                data: "{'wardId':'" + Id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#<%=txtWard.ClientID%>").text(response.d);
                },
                error: function (response) {
                    toastr.error(response.d, "Error Getting Ward Name");
                }
            });
        }

        function getSubcountyList()
        {
            var countyId = $("#<%=smrCounty.ClientID%>").find(":selected").text();
            //alert(countyId);
            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetLookupSubcountyList",
                data: "{'county':'" + countyId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);
                    $("#<%=smrSubCounty.ClientID%>").find('option').remove().end();
                    $("#<%=smrSubCounty.ClientID%>").append('<option value="0">Select</option>');
                    $.each(itemList, function (index, itemList) {

                        $("#<%=smrSubCounty.ClientID%>").append('<option value="' + itemList.SubcountyId + '">' + itemList.SubcountyName + '</option>');
                    }); 
                },
                error: function (response) {
                    toastr.error("Error in selecting the SubcountyList to Load "+ response.d, "Fetching subcounty List");
                }
            });
        }

        function getWardList()
        {
            var subcountyName = $("#<%=smrSubCounty.ClientID%>").find(":selected").text();
            //alert(subcountyName);
            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetLookupWardList",
                data: "{'subcounty':'" + subcountyName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);
                    $("#<%=smrWard.ClientID%>").find('option').remove().end();
                    $("#<%=smrWard.ClientID%>").append('<option value="0">Select</option>');
                    $.each(itemList, function (index, itemList) {
                        $("#<%=smrWard.ClientID%>").append('<option value="' + itemList.WardId + '">' + itemList.WardName + '</option>');
                    }); 
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    return false;
                }
            });     
        }
    </script>
</asp:Content>
