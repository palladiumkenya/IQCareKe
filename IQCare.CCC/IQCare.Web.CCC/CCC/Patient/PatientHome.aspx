<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
        </div>

        <div class="col-md-12 col-xs-12">

                 
                           
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

                  
         </div>
    </div>
</asp:Content>
