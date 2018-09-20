<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriageSummary.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriageSummary" %>
<%@ Import Namespace="Entities.CCC.Lookup" %>
<%@ Import Namespace="Entities.PatientCore" %>
<div class="col-md-12 col-xs-12 col-sm-12">

    <div class="panel panel-default" id="panelVitals">
        <div class="panel-body">
            <div class="table-responsive">
                <table class="table table-condensed table-bordered">
                    <tbody>
                        <tr>
                            <td>Date Taken:</td>
                            <td><asp:Label runat="server" ID="lblDatetaken" CssClass="pull-left "> #</asp:Label></td>
                            <td>Age:</td>
                            <td><asp:Label runat="server" ID="lblAge" CssClass="text-info pull-left"> 0 Years</asp:Label></td>
                            <td>MUAC</td>
                            <td><asp:Label runat="server" ID="lblMuac" CssClass="text-info pull-left"> 0 cms</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Temperature:</td> 
                            <td><asp:Label runat="server" ID="lblTemperature" CssClass="text-info pull-left"> 0 °C</asp:Label></td>    
                            <td>BMI(Z):</td>
                            <td><asp:Label runat="server" ID="lblBMI" CssClass="text-info pull-left"> 0 Kg/M2</asp:Label></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Blood Pressure:</td>
                            <td><asp:Label runat="server" ID="lblbloodpressure" CssClass="text-info pull-left"> 0 mm[Hg]</asp:Label></td>
                            <td colspan="2"><div class="progress"><div class="progress-bar progress-bar-info" id="bgSystolic" style="width: 0;"aria-valuemax="0"><asp:Label runat="server" id="bgSystolicT" style="text-align:center"> (Systolic)</asp:Label></div></div></td>
                            <td colspan="2"><div class="progress"><div class="progress-bar progress-bar-info" id="pgDiastolic" style="width: 0;"aria-valuemax="0"><asp:Label runat="server" id="pgDiastolicT" style="text-align:center"> (Diastolic)</asp:Label></div></div></td>
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
        <div class="col-md-3">&nbsp;</div>
        <div class="col-md-3">
            <asp:LinkButton runat="server" ID="btnLoadTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Add Patient Vitals</asp:LinkButton>
        </div>
        <div class="col-md-3" id="divBtnVitals">
            <asp:LinkButton runat="server" ID="btnFemalVitals" CssClass="btn btn-success  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Female Patient Followup </asp:LinkButton>
        </div>
        <div class="col-md-3" id="divBtnGBVAssessment">
            <asp:LinkButton runat="server" ID="btnGBVAssessment" CssClass="btn btn-success  fa fa-list btn-lg " ClientIDMode="Static" OnClientClick="return false;"> GBV Assessment </asp:LinkButton>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function() {

        var systolic  = "<%=bpSystloic%>";
        var diastolic  = "<%=bpDiastolic%>";

        var patientGender = "<%=PatientGender%>";

        var total = parseInt(systolic) + parseInt(diastolic);
        var diaPercentage = ((diastolic / total) * 100);
        var systoPercentage = (systolic / total) * 100;

        
        //systolic >90 and <120 and diastolic >60 and <=80 Normal - green 
        //systolic >120 and <140  and diastolic >80 and <90 Pre-High Blood pressure -Yellow 
       
        //systolic >140 and diastolic >90 -High Blood Pressure Red 

        if (systolic > 0 && diastolic > 0) {
            var bpColorCode = "";
            var bpstatus = "";
            //systolic <90 and diastolic <=60 -LOW | color code Blue
            if (systolic < 120 && diastolic < 80) { bpColorCode = '#8FBC8F'; bpstatus = 'Normal' }
            if (systolic >= 120 && systolic < 129 && diastolic < 80) { bpColorCode = '#B8860B'; bpstatus = 'Elevated' }
            if (systolic >= 130 && systolic < 139 && diastolic > 80 && diastolic < 89) { bpColorCode = '#FF8C00'; bpstatus = 'Hypertension STAGE 1' }
            if (systolic > 140 && diastolic > 90) { bpColorCode = '#8B0000'; bpstatus = 'Hypertension STAGE 2' }
            if (systolic > 180 && diastolic > 120) { bpColorCode = '#DC143C'; bpstatus = 'Hypertension CRISIS' }

            $("#pgDiastolic").css('width', diaPercentage + '%').attr('aria-valuenow', diastolic);
            $("#pgDiastolic").css('background', bpColorCode);
            $("#bgSystolic").css('width', systoPercentage + '%').attr('aria-valuenow', systolic);
            $("#bgSystolic").css('background', '' + bpColorCode + '');
         
        }




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