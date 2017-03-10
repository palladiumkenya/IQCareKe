<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" CodeBehind="frmPatient_Profile_Summary.aspx.cs" Inherits="IQCare.Web.ClinicalForms.frmPatient_Profile_Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

<div class="row">    
    <h2> Patient Profile Summary </h2>
</div><!-- .row -->

<div class="row">
    
    <div class="panel panel-default">

         <div class="panel-heading">Patient Information </div>

        <div class="panel-body">
            <div class="row">
                  <div class="col-md-3">
                       <label  class="control-label">Patient Name :</label>
                  </div><!-- col-md-3-->
                  <div class="col-md-2">
                     <label  class="control-label">Age:</label>
                  </div><!-- .col-md-2-->
                  <div class="col-md-1">
                       <label  class="control-label">Sex :</label>
                  </div><!-- .col-md-2-->
                  <div class="col-md-2">
                       <label  class="control-label">Patient Status :</label>
                  </div><!-- .col-md-2-->
                  <div class="col-md-2">
                       <label  class="control-label">Patient Facility Id :</label>
                  </div><!-- .col-md-2-->
            </div><!-- .row --><hr />
            <div class="row">
                 <div class="col-md-3"></div><!-- .col-md-3-->
                 <div class="col-md-3">
                      <label  class="control-label">Patient Unique Number :</label>
                 </div><!-- .col-md-3-->
                 <div class="col-md-3">
                      <label  class="control-label">Patient Clinic ID :</label>
                 </div><!-- .col-md-3-->
                 <div class="col-md-3"></div><!-- .col-md-3-->
            </div><!-- -->

        </div><!-- .panel-body -->
    </div><!-- .panel-->

    <!-- Patient Vitals -->
    <div class="row">
        <h2>PATIENT VITALS</h2>
        <hr />

    </div><!-- .row -->

    <!-- Clinical Notes -->
    <div class="row">
        <h2>CLINICAL NOTES</h2>
        <hr />

    </div><!-- .row -->

    <!-- Prescriptions -->
    <div class="row">
        <h2>PRESCRIPTIONS</h2>
        <hr />

    </div><!-- .row -->

    <!-- Regimen & Side Effects -->
    <div class="row">
        <h2>REGIMEN & SIDE EFFECTS</h2>
        <hr />

    </div><!-- .row -->

    <!-- Appointment -->
    <div class="row">
        <h2>APPOINTMENT</h2>
        <hr />
    
    </div>


</div><!-- .row -->


</asp:Content>