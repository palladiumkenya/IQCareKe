<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-danger">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>Last VL Results : </strong><span class="badge">
                        <asp:Label ID="lblviralload" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-warning">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>VL Due Date : </strong><span class="badge">
                        <asp:Label ID="lblVLDueDate" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-success">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>Current Regimen </strong><span class="badge">
                        <asp:Label ID="lblCurARVRegimen" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-danger">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>Most Recent CD4 : </strong><span class="badge">
                        <asp:Label ID="lblrecentCD4ValueDate" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
    </div>
    
    <div>
        <div class="row">
            <div class="col-md-12 col-xs-12">
                <div role="tabpanel" class="panel panel-default" id="home">
                    <div class="col-md-4" style="padding-left: 0px;">
                        <div class="panel-heading bs-callout bs-callout-success">Today's Vital Signs</div>
                        <div class="panel-body" style="border-left: solid gray 1px;">
                        
                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Height (cm)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalHeight" CssClass="control-label text-success pull-left">0 cms</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Weight (kg)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalsWeight" CssClass="control-label text-success pull-left">0 Kgs</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Head Circumference (cm)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalsCircumference" CssClass="control-label text-success pull-left">0 cms</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">MUAC (cm)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalsMUAC" CssClass="control-label text-success pull-left">0 cms</asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Blood Pressure </label></div>
                                <div class="col-md-4"> 
                                    <asp:Label runat="server" ID="vitalBloodPressure" CssClass="control-label text-success pull-left">0 </asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Temperature (0C)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalTemperature" CssClass="control-label text-success pull-left">0 0C</asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Respiratory Rate</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalRespiratoryRate" CssClass="control-label text-success pull-left">0 </asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Blood Oxygen Saturation</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblOxygenSaturation" CssClass="control-label text-success pull-left">0 %</asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-4">
                        
                        <div class="panel-heading bs-callout bs-callout-default">Laboratory Summary</div>
                        <div class="panel-body" style="border-left: solid gray 1px;">
                            
                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment CD4</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="EnrollmentCD4" CssClass="control-label pull-left" ClientIDMode="Static">0</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment CD4 Date</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="EnrollmentCD4Date" CssClass="control-label pull-left" ClientIDMode="Static">00-00-0000</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment Viral Load</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="enrollmentViralload" CssClass="control-label pull-left" ClientIDMode="Static">0</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment Viralload Date</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="enrollmentviralloaddate" CssClass="control-label pull-left" ClientIDMode="Static">00-00-000</asp:Label>
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                   &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="col-md-4">
                        
                        <div class="panel-heading bs-callout bs-callout-default">Diagnosis Summary</div>
                        <div class="panel-body" style="border-left: solid gray 1px;">
                            
                             <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                   &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                             <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                   &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>

                        </div>
                    </div>           
                </div>
            </div>
        </div>
    </div>

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
</asp:Content>
