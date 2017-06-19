<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriageSummary.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriageSummary" %>
<%@ Import Namespace="Entities.CCC.Lookup" %>
<%@ Import Namespace="Entities.PatientCore" %>
<div class="col-md-12 col-xs-12 col-sm-12">
    <style type="text/css">
        .table tbody tr td, .table tbody tr th {
            border: none !important;
        }
    </style>

    <div class="panel panel-default" id="panelVitals">
        <div class="panel-body">
            <div class="table-responsive">
                <table class="table table-condensed">
                    <tbody>
                        <tr>
                            <td>Date Taken:</td>
                            <td><asp:Label runat="server" ID="lblDatetaken" CssClass="pull-left "> #</asp:Label></td>
                            <td>Age:</td>
                            <td><asp:Label runat="server" ID="lblAge" CssClass="text-info pull-left"> 0 Years</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Temperature:</td> 
                            <td><asp:Label runat="server" ID="lblTemperature" CssClass="text-info pull-left"> 0 °C</asp:Label></td>    
                            <td>BMI(Z):</td>
                            <td><asp:Label runat="server" ID="lblBMI" CssClass="text-info pull-left"> 0 Kg/M2</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Blood Pressure:</td>
                            <td><asp:Label runat="server" ID="lblbloodpressure" CssClass="text-info pull-left"> 0 mm[Hg]</asp:Label></td>
                            <td><div class="progress"><div class="progress-bar progress-bar-info" id="bgSystolic" style="width: 0;"aria-valuemax="200"></div></div></td>
                            <td><asp:Label runat="server"> (Systolic)</asp:Label></td>
                            <td><div class="progress"><div class="progress-bar progress-bar-info" id="pgDiastolic" style="width: 0;"aria-valuemax="110"></div></div></td>
                            <td><asp:Label runat="server"> (Diastolic)</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Pulse Rate:</td>
                            <td><asp:Label runat="server" ID="lblPulseRate" CssClass="text-info pull-left"> 0 </asp:Label></td>
                            <td>Respiratory Rate:</td>
                            <td><asp:Label runat="server" ID="lblRespiratoryRate" CssClass="text-info pull-left"> 0 </asp:Label></td>
                            <td>Oxygen Saturation:</td>
                            <td><asp:Label runat="server" ID="lblOxygenSaturation" CssClass="text-info pull-left"> 0 %</asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-12"><h4 class="pull-left"><asp:Label runat="server"> Patient Appointment </asp:Label></h4></div>
                <div class="table-responsive col-md-12">
                    <table class="table table-condensed">
                        <tbody>
                            <tr>
                                <td>Appointment Date:</td>
                                <td><asp:Label CssClass="text-info" ID="lblappointmentDate" runat="server" ClientIDMode="Static"></asp:Label></td>
                                <td>Appointment Status:</td>
                                <td><asp:Label CssClass="text-danger" ID="lblappointmentStatus" runat="server" ClientIDMode="Static"></asp:Label></td>
                                <td>Care Status:</td>
                                <td><asp:Label ID="lblcareStatus" CssClass="text-info" runat="server" ClientIDMode="Static"></asp:Label></td>
                                <td>Appointment Reason:</td>
                                <td><asp:Label ID="lblAppointmentReason" runat="server" ClientIDMode="Static"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 col-xs-12 col-sm-12">
        <div class="col-md-6">&nbsp;</div>
        <div class="col-md-3">
            <asp:LinkButton runat="server" ID="btnLoadTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Add Patient Vitals</asp:LinkButton>
        </div>
        <div class="col-md-3" id="divBtnVitals">
            <asp:LinkButton runat="server" ID="btnFemalVitals" CssClass="btn btn-success  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Female Patient Followup </asp:LinkButton>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function() {

        var systolic  = "<%=bpSystloic%>";
        var diastolic  = "<%=bpDiastolic%>";

        var patientGender = "<%=PatientGender%>";


        $("#pgDiastolic").css('width', diastolic + '%').attr('aria-valuenow', diastolic);
        $("#bgSystolic").css('width', systolic + '%').attr('aria-valuenow', systolic);

  
        //if (patientGender === 'Male') { $("#btnFemalVitals").prop("disabled",false); }
        //else if (patientGender === 'Female') {
        //     $("#btnFemalVitals").prop("disabled",true);
        //}
       // alert(patientGender);


        //$("#btnFemalVitals").click(function() {
        //       var dialog = bootbox.dialog({
        //        title: 'A custom dialog with init',
        //        message: '<p><i class="fa fa-spin fa-spinner"></i> Loading...</p>'
        //   });
        //});

    });
</script>