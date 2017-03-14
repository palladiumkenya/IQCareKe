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
    
    <div class="col-md-12 col-xs-12 col-sm-12">
      
        <ul class="nav nav-tabs">

            <li class="active"><a data-toggle="tab" href="#EntryPoint">
                <span class="fa-stack fa-lg">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-info-circle fa-stack-1x fa-inverse"></i>
                </span>
               <strong class="text-info">Entry Point & Transfer Status</strong> <br>
                
            </a></li>

            <li><a data-toggle="tab" href="#Diagnosis">Diagnosis & ARV HIstory </a></li>
        
            <li><a data-toggle="tab" href="#Baseline">Baseline Assessment & Treament Initiation </a></li>
        </ul>
    
        <div class="col-md-12 col-xs-12 col-xs-12 form-group">
            <div class="col-md-12 form-group"></div> 
            <div class="tab-content">
                 <div id="EntryPoint" class="tab-pane fade in active">
                     <div class="col-md-12 col-xs-12 col-sm-12">
                         <div class="col-md-4 col-xs-12 col-sm-12">

                         </div><!-- .col-md-4 -->

                         <div class="col-md-4 col-xs-12 col-sm-12">
                         </div><!-- .col-md-4 -->

                         <div class="col-md-4 col-xs-12 col-sm-12">

                         </div><!-- .col-md-4 -->
                     </div>
                 </div>
                 <div id="Diagnosis" class="tab-pane fade"> </div>
                 <div id="Baseline" class="tab-pane fade"></div>       
             </div><!-- .tab-content -->
        </div> <!-- col-md-12 -->

    



    </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
</asp:Content>
