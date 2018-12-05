<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientVitalsExtruder.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientVitalsExtruder" %>
<div style="padding-top:20px">
    <div style="align-content:center" class="label label-primary"><asp:Label ID="lblVitalsDate" runat="server" clientidmode="Static" class="control-label text-primary">Date Taken :</asp:Label></div>
    <div class="panel-body" style="border-left: solid gray 1px;">
         
        <div class="row">            
            <div class="col-md-12">
                <div class="col-md-12"><hr/></div>
            </div> 
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">Height (cm)</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalHeight" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">Weight (kg)</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalsWeight" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">BMI(z)</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalsBMI" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div>
        </div>

        <div class="row" runat="server" id="dvHeadCircum">
            <div class="col-md-12">
                <div class="col-md-5"><label  class="control-label pull-left">Head Circumference (cm)</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalsCircumference" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div>
        </div>

        <div class="row" runat="server" id="dvMuac"> 
            <div class="col-md-12">
                <div class="col-md-5"><label runat="server" ID="lblMuac" class="control-label pull-left">MUAC (cm)</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalsMUAC" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div> 
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">Blood Pressure </label></div>
                <div class="col-md-3"> 
                    <asp:Label runat="server" ID="vitalBloodPressure" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span> </asp:Label>
                </div>
            </div> 
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">Temperature (0C)</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalTemperature" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div> 
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">Respiratory Rate</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="vitalRespiratoryRate" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span> </asp:Label>
                </div>
            </div> 
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"><label class="control-label pull-left">Blood Oxygen Saturation</label></div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="lblOxygenSaturation" CssClass="control-label text-success pull-left"><span class="label label-danger">Not Taken</span></asp:Label>
                </div>
            </div>
        </div>
        

    </div>

</div>

<div class="col-md-12 form-group">
    
    
    <div class="panel panel-default">

        <div class="panel-body">
            
           <div class="col-md-12"><label class="control-label pull-left fa fa-arrow-circle-o-right"> Pregnancy Status</label></div> 
            <div class="col-md-12"><hr/></div>
            <div class="col-md-12 form-group">
    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblPregnancyStatus"></asp:Label></div>
</div>

<div class="col-md-12 form-group">
    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblLMP"></asp:Label></div>
</div>

<div class="col-md-12 form-group">
    <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblEDD"></asp:Label></div>
</div>

        </div>

    </div>


</div>

