<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriageSummary.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriageSummary" %>
<%@ Import Namespace="Entities.CCC.Lookup" %>
<%@ Import Namespace="Entities.PatientCore" %>
<div class="col-md-8 col-xs-12 col-sm-12">
    

    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-12 col-sm-12">
                    <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Age :</asp:Label></h5></div>
                    <div class="col-md-12">
                        <h6> <asp:Label runat="server" ID="lblAge" CssClass="text-info pull-left"> 0 Years</asp:Label></h6>
                    </div>
                </div>
                <div class="col-md-3 col-xs-12 col-sm-12">
                    <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Weight :</asp:Label></h5></div>
                    <div class="col-md-12">
                         <h6><asp:Label runat="server" ID="lblWeight" CssClass="text-info pull-left"> 0 Kgs</asp:Label></h6>
                    </div>
                </div>
                <div class="col-md-3 col-xs-12 col-sm-12">
                    <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> BMI :</asp:Label></h5></div>
                    <div class="col-md-12">
                        <h6> <asp:Label runat="server" ID="lblBMI" CssClass="text-info pull-left"> 0 Kg/M2</asp:Label></h6>
                    </div>
                </div>
<%--                <div class="col-md-3">
                     <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Temperature :</asp:Label></h5></div>
                     <div class="col-md-12">
                        <h6> <asp:Label runat="server" ID="lblTemperature" CssClass="text-info pull-left"> 0 C</asp:Label></h6>
                    </div>
                </div>--%>
                <div class="col-md-12"><hr/></div>
                <div class="col-md-12">
                     <div class="col-md-12"><h5 class="pull-left"><asp:Label runat="server"> Blood Pressure :</asp:Label></h5></div>
                    <div class="col-md-4">
                        <h6> <asp:Label runat="server" ID="lblbloodpressure" CssClass="text-info pull-left"> 0 mm[Hg]</asp:Label></h6>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">                           
                             <div class="progress">
                                <div class="progress-bar progress-bar-info" id="bgSystolic" style="width: 0;"aria-valuemax="139"></div>
                            </div>
                        </div>
                        <div class="col-md-12"><h6 class="pull-left"><asp:Label runat="server"> (Systolic)</asp:Label></h6></div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12" style="padding-bottom: 1%">
                            <div class="progress">
                                <div class="progress-bar progress-bar-info" id="pgDiastolic" style="width: 0;"aria-valuemax="89"></div>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding-top: 1%"><h6 class="pull-left"><asp:Label runat="server"> (Diastolic)</asp:Label></h6></div>
                    </div>
                </div>
                <div class="col-md-12"><hr/></div>
            </div> <!-- .col-md-12 -->
           
        </div> <!-- .panel-body -->
    </div>


</div>
<div class="col-md-4 col-xs-4 col-sm-4">
    <div class="col-md-12"><h4 class="pull-left"><asp:Label runat="server"> Patient Appointment </asp:Label></h4></div>
    <div class="col-md-12" style="padding-top:1% "><hr/></div>
    <div class="col-md-12 col-xs-12 col-sm-12">
              
        <div class="list-group active col-md-12">
            <ul class="fa-ul">
              <li class="col-md-12"><h6 class="pull-left">Appointment Date : </h6><asp:Label CssClass="text-info" ID="lblappointmentDate" runat="server" ClientIDMode="Static"></asp:Label></li>
              <li class="col-md-12"><h6 class="pull-left">Appointment Status : </h6><asp:Label CssClass="text-danger" ID="lblappointmentStatus" runat="server" ClientIDMode="Static"></asp:Label></li>
              <li class="col-md-12"><h6 class="pull-left">Care Status : </h6><asp:Label ID="lblcareStatus" CssClass="text-info" runat="server" ClientIDMode="Static"></asp:Label></li>
              <li class="col-md-12"><h6 class="pull-left">Appointment Reason : </h6><asp:Label ID="lblAppointmentReason" runat="server" ClientIDMode="Static"></asp:Label></li>
            </ul>
        </div>
    </div>
    
    <div class="col-md-12 col-xs-12 col-sm-12">
           <div class="col-md-6 col-xs-12 col-sm-12 ">
                <asp:LinkButton runat="server" ID="btnLoadTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Add Patient Vitals</asp:LinkButton>
            </div>
            <div class="col-md-6 col-xs-12 col-sm-12" id="divBtnVitals">
                <asp:LinkButton runat="server" ID="btnFemalVitals" CssClass="btn btn-success  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Female Patient Followup </asp:LinkButton>
            </div>
    </div>
    
<%--    <div class="col-md-12"><h4 class="pull-left"><asp:Label runat="server"> Pregnancy Status</asp:Label></h4></div>
    <div class="col-md-12" style="padding-top:1% "><hr/></div>
    <div class="col-md-12 col-xs-12 col-sm-12">
              
        <div class="list-group active">
            <ul class="fa-ul">
              <li><i class="fa-li fa fa-check-square"></i>List icons</li>
              <li><i class="fa-li fa fa-check-square"></i>can be used</li>
              <li><i class="fa-li fa fa-spinner fa-spin"></i>as bullets</li>
              <li><i class="fa-li fa fa-square"></i>in lists</li>
            </ul>
        </div>
    </div>--%>
</div>
<script type="text/javascript">
    $(document).ready(function() {

        var diastolic = "<%=bpDiastolic%>";
        var systolic = "<%=bpSystloic%>";

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