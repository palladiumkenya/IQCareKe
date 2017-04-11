<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriage.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriage" %>

<div class="col-md-12 bs-callout bs-callout-primary well well-sm">

    <div class="col-md-12" id="vitalsform" data-parsley-validate="true" data-show-errors="true">
        <div class="col-md-12"> <h2 class="text-muted text-Warning pull-left"> Vital Signs | </h2>  <h6> <label class="control-label text-primary pull-left text-muted">Routine Vital Signs Capture</label></h6></div>

         <div class="col-md-12">  <hr style="padding-top:1%" /> </div>

        <div class="col-md-12 col-xs-12 col-sm-12 form-group" id="anthropometricMeasurement" data-parsley-validate="true" data-show-errors="true">
            
            <div class="col-md-12">
                <label class="control-label text-primary pull-left text-muted">Anthropometric Measurement</label>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 form-group">
               
                <div class="col-md-3 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> Weight(kgs)</span>
                            <asp:TextBox runat="server" ID="weights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="kgs.." required="true" data-parsley-required="true" Type="Number" Min="0"></asp:TextBox>
                        </div>
                 </div>

                <div class="col-md-3 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> Height(cm)</span>
                            <asp:TextBox runat="server" ID="Heights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." required="true" data-parsley-required="true" Type="Number" Min="10"></asp:TextBox>
                        </div>
                    </div>

                <div class="col-md-4 col-xs-12 col-sm-12">
                        <%--<asp:Label runat="server"  ID ="bmi" Value = "" CssClass="control-label text-warning pull-left"></asp:Label>--%>
                        <div class="input-group">
                            <span class="input-group-addon">BMI (kg/m2)</span>
                            <asp:TextBox runat="server" ID="bmivalue" Enabled="False" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="bmi"></asp:TextBox>
                        </div>
                 </div>
                
                <div class="col-md-2 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger"></small>MUAC (cm)</span> 
                             <asp:TextBox runat="server" ID="muacs" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number" Min="0"></asp:TextBox>
                        </div>
                    </div>
  
            </div> 
            
            <div class="col-md-12 col-xs-12 col-sm-12 form-group">

                 <div class="col-md-3 col-xs-3 col-sm-3">
                         <div class="input-group">
                              <span class="input-group-addon"><small class="text-danger"></small>Head Circumference</span> 
                             <asp:TextBox runat="server" ID="circumference" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number" Min="0"></asp:TextBox>
                         </div>
                     <p class="help-block pull-left"><strong>Normal 46.3528–52.44764 cm </strong></p>
                    </div>
                 <div class="col-md-3 col-xs-12 col-sm-12">
                    <div class="input-group">
                        <span class="input-group-addon">Temperature (°C)</span>
                        <asp:TextBox runat="server" ID="Tempreture" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number" Min="25" Max="50" data-parsley-range="[25, 50]" data-parsley-range-message="Tempreture is out of reasonable range"></asp:TextBox>
                    </div>
                    <p class="help-block pull-left"><strong>Normal 36.5–37.5 °C </strong></p>
                </div>   
                 <div class="col-md-3 col-xs-12 col-sm-12"></div>
                <div class="col-md-3 col-xs-12 col-sm-12"></div>

            </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->
        </div> <!-- .col-md-4 col-xs-4 col-sm-4-->
         
                
        <div class="col-md-12 col-xs-12 col-sm-12 form-group">

            <div class="col-md-6 col-xs-12 col-sm-12">
                 <div class="col-md-12  form-group"><label class="control-label text-primary pull-left text-muted">Blood Pressure</label></div>
            
                  <div class="col-md-12 col-xs-12 col-sm-12 form-group" style=" padding-bottom: 0%">
                     <div class="input-group">
                         <span class="input-group-addon">Systolic</span>
                         <asp:TextBox runat="server" ID="systolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="diastolic.." Type="Number" data-parsley-range="[30, 110]" data-parsley-range-message="Diastolic reading is out of reasonable range"></asp:TextBox>
                         <span class="input-group-addon">mm[Hg]</span>
                     </div>
                      <p class="help-block pull-left"><strong>Normal values: (140-80)</strong></p>
                  </div>
                
                  <div class="col-md-12 col-xs-12 col-sm-12">
                    <div class="input-group">
                         <span class="input-group-addon">Diastolic</span>
                         <asp:TextBox runat="server" ID="distolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="systolic.." Type="Number" data-parsley-range="[60, 200]" data-parsley-range-message="Systolic reading is out of reasonable range"></asp:TextBox>
                         <span class="input-group-addon">mm[Hg]</span>
                    </div>
                    <label class="help-block pull-left"><strong>Normal values: (140-80)</strong></label>
                 </div>

            </div> 
            
            <div class="col-md-6 col-xs-12 col-sm-12">
                 <div class="col-md-12 form-group"><label class="control-label text-primary pull-left text-muted">Pulse Rate/Respiratory/Oxygen Saturation</label></div>
                 <div class="col-md-12 colxs-12 col-sm-12 form-group">
                      <div class="input-group">
                           <span class="input-group-addon">Rate</span>                      
                           <asp:TextBox runat="server" ID="HeartRate" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number" Min="0"></asp:TextBox>
                           <span class="input-group-addon">/Min</span>
                      </div>
                     <p class="help-block pull-left"><strong>60-100 beats /min-Adults</strong></p>
                 </div>
                 <div class="col-md-12 col-xs-12 col-cm-12 form-group">
                    <div class="input-group">
                        <span class="input-group-addon">Respiration</span>  
                        <asp:TextBox runat="server" ID="RespiratoryRate" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number" Min="0"></asp:TextBox>
                        <span class="input-group-addon">/Min</span>
                    </div>
                    <p class="help-block pull-left"><strong>12-20 breaths /min-Adults</strong></p>
                </div>
                 <div class="col-md-12 col-xs-12 col-sm-12">
                     <div class="input-group">
                          <span class="input-group-addon">Blood Oxygen Saturation</span> 
                          <asp:TextBox runat="server" ID="bosaturation" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="%.." Type="Number" Min="0" Max="100"></asp:TextBox>
                         <span class="input-group-addon">%</span> 
                     </div>
                     <p class="help-block pull-left"><strong>95-100 %</strong></p>
                 </div>
            </div><!-- .col-md-6 col-xs-12 col-sm-12-->
              
        </div>
       
    </div>
    
    <%-- .col-md-11--%>


    <div class="col-md-12">
        <div class="col-md-8"></div>
        <div class="col-md-4">
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnSaveTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Triage </asp:LinkButton>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Entry  </asp:LinkButton>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close Triage </asp:LinkButton>
            </div>
        </div>
    </div>
</div> <!-- .col-md-12 -->

<script type="text/javascript">
    $(document).ready(function () {
        $("#btnSaveTriage").click(function() {
            if ($('#vitalsform').parsley().validate()) {
                addPatientVitals();
            } else {
                return false;
            }
        });
        $("#btnReset").click(function() {
            resetElements();
        });
        $("#btnCancel").click(function () {
            window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
        });
    });
    $("#Heights").change(function () {
        var bmi = calcBMI();
        var weight = '';
        if(bmi<18.5){weight = '(Under weight)'}else if(bmi>=18.5<25){weight = '(Normal weight)'}else if(bmi>=25<30){weight = '(Over weight)'}else{weight = '(Obese)'}
        document.getElementById("bmivalue").value = bmi+weight;
    });
    $("#weights").change(function () {
        var bmi = calcBMI();
        var weight = '';
        if(bmi<18.5){weight = '(Under weight)'}else if(bmi>=18.5<25){weight = '(Normal weight)'}else if(bmi>=25<30){weight = '(Over weight)'}else{weight = '(Obese)'}
        document.getElementById("bmivalue").value = bmi+weight;
    });
    function calcBMI()
    {
        var weight = document.getElementById('weights').value;
        var height = document.getElementById('Heights').value/100;
        var bmi = (weight / (height * height)).toFixed(1); //BMI fomula
        return bmi;
    }

    function addPatientVitals() {
        var height = $("#<%=Heights.ClientID%>").val();
        var weight = $("#<%=weights.ClientID%>").val();
        var bmi = calcBMI();
        if (bmi === '') { bmi = 0 }
        var headCircumference = $("#<%=circumference.ClientID%>").val();
        if (headCircumference === '') { headCircumference = 0 }
        var muacs = $("#<%=muacs.ClientID%>").val();
        if (muacs === '') { muacs = 0 }
        var diastolic = $("#<%=distolic.ClientID%>").val();
        if (diastolic === '') { diastolic = 0 }
        var systolic = $("#<%=systolic.ClientID%>").val();
        if (systolic === '') { systolic = 0 }
        var tempreture = $("#<%=Tempreture.ClientID%>").val();
        if (tempreture === '') { tempreture = 0 }
        var respiratoryRate = $("#<%=RespiratoryRate.ClientID%>").val();
        if (respiratoryRate === '') { respiratoryRate = 0 }
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var heartRate = $("#<%=HeartRate.ClientID%>").val();
        if (heartRate === '') { heartRate = 0 }
        var boSaturation = $("#<%=bosaturation.ClientID%>").val();//todo Mwasi: check sp02
        if (boSaturation === '') { boSaturation = 0 }
        debugger;
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/AddpatientVitals",
            data: "{'patientId': '" + patientId + "','bpSystolic': '" + systolic + "','bpDiastolic': '" + diastolic + "','heartRate': '" + heartRate + "','height': '" + height + "','muac': '" + muacs + "','patientMasterVisitId': '" + patientMasterVisitId + "','respiratoryRate': '" + respiratoryRate + "','spo2': '" + boSaturation + "','tempreture': '" + tempreture + "','weight': '" + weight + "','bmi': '"+ bmi +"','headCircumference': '"+ headCircumference + "'}", //todo Mwasi: add patient id and patientvistId
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success(response.d, "Vitals saved successfully");
                resetElements();
                //redirect
                window.location.href="<%=ResolveClientUrl("~/CCC/patient/PatientHome.aspx")%>";
            },
            error: function (response) {
                toastr.success(response.d, "Vitals not saved");
            }
        });
    }

    function resetElements(parameters) {
        $("#Heights").val("");
        $("#weights").val("");
        $("#bmivalue").val("");
        $("#circumference").val("");
        $("#muacs").val("");
        $("#distolic").val("");
        $("#systolic").val("");
        $("#Tempreture").val("");
        $("#RespiratoryRate").val("");
        $("#HeartRate").val("");
        $("#bosaturation").val("");
    }
</script>
