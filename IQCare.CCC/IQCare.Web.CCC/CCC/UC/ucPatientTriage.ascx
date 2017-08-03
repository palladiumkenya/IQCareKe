<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriage.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriage" %>

<div class="col-md-12">

    <div class="col-md-12" id="vitalsform" data-parsley-validate="true" data-show-errors="true">
        <div class="col-md-12"> <h2 class="text-muted text-Warning pull-left"> Vital Signs | </h2>  <h6> <label class="control-label text-primary pull-left text-muted">Routine Vital Signs Capture</label></h6></div>

         <div class="col-md-12">  <hr/> </div>
        
        <div class="col-md-12">
            
            <div class="col-md-12"><label class="required control-label pull-left">Visit Date</label></div>

            <div class="col-md-4 form-group">
                <div class='input-group date' id='VisitDatedatepicker'>
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PersonDoB" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                </div>
            </div>
        </div><!-- .col-md-12 -->

        <div class="col-md-12 col-xs-12 col-sm-12 form-group" id="anthropometricMeasurement" data-parsley-validate="true" data-show-errors="true">
            
            <div class="col-md-12">
                <label class="control-label text-primary pull-left text-muted">Anthropometric Measurement</label>
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 form-group">
               
                <div class="col-md-4 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> Weight(kgs)</span>
                            <asp:TextBox runat="server" ID="weights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="kgs.." required="true" data-parsley-required="true" Type="Number" Min="0"></asp:TextBox>
                        </div>
                 </div>

                <div class="col-md-4 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> Height(cm)</span>
                            <asp:TextBox runat="server" ID="Heights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." required="true" data-parsley-required="true" Type="Number" Min="10" Max="250" data-parsley-range="[10, 250]" data-parsley-range-message="Height is out of reasonable range"></asp:TextBox>
                        </div>
                    </div>

                <div id="divBMI" class="col-md-4 col-xs-12 col-sm-12">
                        <%--<asp:Label runat="server"  ID ="bmi" Value = "" CssClass="control-label text-warning pull-left"></asp:Label>--%>
                        <div class="input-group">
                            <span class="input-group-addon">BMI (kg/m2)</span>
                            <asp:TextBox runat="server" ID="bmivalue" Enabled="False" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="bmi"></asp:TextBox>
                        </div>
                 </div>
                
                
  
            </div> 

            <div id="peadsZScores" class="col-md-12 col-xs-12 col-sm-12 form-group">
                <div class="col-md-4 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> BMIz</span>
                            <asp:TextBox runat="server" ID="txtBMIz" Enabled="False" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                 </div>

                <div class="col-md-4 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> Weight for Age</span>
                            <asp:TextBox runat="server" ID="txtWAz" Enabled="False" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                 </div>

                <div class="col-md-4 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger">*</small> Wieght for Height</span>
                            <asp:TextBox runat="server" ID="txtWHz" Enabled="False" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                 </div>
            </div>
            
            <div class="col-md-12 col-xs-12 col-sm-12 form-group">
                <div class="col-md-4 col-xs-12 col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><small class="text-danger"></small>MUAC (cm)</span> 
                             <asp:TextBox runat="server" ID="muacs" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number" Min="1" Max="30" data-parsley-range="[1, 30]" data-parsley-range-message="MUAC is out of reasonable range"></asp:TextBox>
                        </div>
                    </div>

                 <div class="col-md-4 col-xs-3 col-sm-3">
                         <div class="input-group">
                              <span class="input-group-addon"><small class="text-danger"></small>Head Circumference</span> 
                             <asp:TextBox runat="server" ID="circumference" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number" Min="0"></asp:TextBox>
                         </div>
                     <p class="help-block pull-left"><strong>Normal 46.3528–52.44764 cm </strong></p>
                    </div>
                 <div class="col-md-4 col-xs-12 col-sm-12">
                    <div class="input-group">
                        <span class="input-group-addon">Temperature (°C)</span>
                        <asp:TextBox runat="server" ID="Temperature" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number" Min="25" Max="43" data-parsley-range="[25, 43]" data-parsley-range-message="Temperature is out of reasonable range"></asp:TextBox>
                    </div>
                    <p class="help-block pull-left"><strong>Normal 36.5–37.5 °C </strong></p>
                </div>   
                 

            </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->
        </div> <!-- .col-md-4 col-xs-4 col-sm-4-->
         
                
        <div class="col-md-12 col-xs-12 col-sm-12 form-group">

            <div class="col-md-6 col-xs-12 col-sm-12">
                 <div class="col-md-12  form-group"><label class="control-label text-primary pull-left text-muted">Blood Pressure</label></div>
                
                  <div class="col-md-12 col-xs-12 col-sm-12">
                    <div class="input-group">
                         <span class="input-group-addon">Systolic</span>
                         <asp:TextBox runat="server" ID="systolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="systolic.." Type="Number" data-parsley-range="[80, 250]" data-parsley-range-message="Systolic reading is out of reasonable range"></asp:TextBox>
                         <span class="input-group-addon">mm[Hg]</span>
                    </div>
                    <label class="help-block pull-left"><strong>Range: (80-180)</strong></label>
                 </div>

                  <div class="col-md-12 col-xs-12 col-sm-12 form-group" style=" padding-bottom: 0%">
                     <div class="input-group">
                         <span class="input-group-addon">Diastolic</span>
                         <asp:TextBox runat="server" ID="distolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="diastolic.." Type="Number" data-parsley-range="[40, 100]" data-parsley-range-message="Diastolic reading is out of reasonable range"></asp:TextBox>
                         <span class="input-group-addon">mm[Hg]</span>
                     </div>
                      <p class="help-block pull-left"><strong>Range: (40-100)</strong></p>
                  </div>
                
            </div> 
            
            <div class="col-md-6 col-xs-12 col-sm-12">
                 <div class="col-md-12 form-group"><label class="control-label text-primary pull-left text-muted">Pulse Rate/Respiratory/Oxygen Saturation</label></div>
                 <div class="col-md-12 colxs-12 col-sm-12 form-group">
                      <div class="input-group">
                           <span class="input-group-addon">Pulse Rate</span>                      
                           <asp:TextBox runat="server" ID="HeartRate" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number" Min="0"></asp:TextBox>
                           <span class="input-group-addon">/Min</span>
                      </div>
                     <p class="help-block pull-left"><strong>60-100 beats /min-Adults</strong></p>
                 </div>
                 <div class="col-md-12 col-xs-12 col-cm-12 form-group">
                    <div class="input-group">
                        <span class="input-group-addon">Respiration Rate</span>  
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
        
        <div class="col-md-12">
        <div class="col-md-7"></div>
        <div class="col-md-5">
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnSaveTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Triage </asp:LinkButton>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnResets" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Entry  </asp:LinkButton>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnCancels" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close Triage </asp:LinkButton>
            </div>
        </div>
    </div>
       
    </div> <%-- .col-md-11--%>


    
</div> <!-- .col-md-12 -->

<script type="text/javascript">
    $(document).ready(function () {
        var age = "<%=PatientAge%>";
        var gender = "<%=PatientGender%>";
        var pregnancyStatus = <%=PregnancyStatus%>;

        var visitDate;
        $("#muacs").prop("disabled", true);
            $('#circumference').prop("disabled", true);


        if(age > 15)
        {
            document.getElementById('divBMI').style.display = 'block';
            document.getElementById('peadsZScores').style.display = 'none';
        }
        else{
            document.getElementById('divBMI').style.display = 'none';
            document.getElementById('peadsZScores').style.display = 'block';
        }

        //$('#myVisitDate').datepicker({
        //                date:null,
        //                allowPastDates: true,
        //                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        //                //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        //});

        $('#VisitDatedatepicker').datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false
        });

        $('#VisitDatedatepicker').on('dp.change', function (e) {

            var vDate = moment($("#PersonDoB").val(), 'DD-MMM-YYYYY').toDate();
            var futuredate = moment(vDate).isAfter(new Date());
            if (futuredate) {
                $("#<%=PersonDoB.ClientID%>").val('');
                toastr.error("Future dates not allowed!");
                return false;
            }

        });

        if (age > 15 && gender === 'Female' && pregnancyStatus > 0) {

            $("#muacs").prop("disabled", false);
        }
        else if (age < 15 && pregnancyStatus<1) {
             $("#muacs").prop("disabled", false);
        }
        else if (age > 15 || gender==='Male') {
            $("#muacs").prop("disabled", true);
            $('#circumference').prop("disabled", true);
        }


        $("#btnSaveTriage").click(function() {
            if ($('#vitalsform').parsley().validate()) {
                var dob = $("#<%=PersonDoB.ClientID%>").val();
                if (moment('' + dob + '').isAfter()) {
                    toastr.error("Visit date cannot be a future date.");
                    return false;
                } 
                else {
                    addPatientVitals();                    
                }
            } else {
                return false;
            }
        });

        $("#btnResets").click(function() {
            resetElements();
        });

        $("#btnCancels").click(function () {
            window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
        });

    });
    $("#Heights").change(function () {
        var bmi = calcBMI();
        var weight = '';
        if (bmi < 18.5) {weight = '(Under weight)';} else if (bmi >= 18.5 && bmi < 25) {weight = '(Normal weight)';} else if (bmi >= 25 && bmi < 30) {weight = '(Over weight)';} else {weight = '(Obese)';}
        document.getElementById("bmivalue").value = bmi+weight;
        calcZScore();
    });
    $("#weights").change(function () {
        var bmi = calcBMI();
        var weight = '';
        if (bmi < 18.5) {weight = '(Under weight)';} else if (bmi >= 18.5 && bmi < 25) {weight = '(Normal weight)';} else if (bmi >= 25 && bmi < 30) {weight = '(Over weight)';} else {weight = '(Obese)';}
        document.getElementById("bmivalue").value = bmi+weight;
        calcZScore();
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
        //var dateOfVisit;
        //$('#myVisitDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function(event,date) {
        //            dateOfVisit = moment($('#myVisitDate').datepicker('getDate').format('DD-MMM-YYYY'));
        //       });
        var bmiz = $("#<%=txtBMIz.ClientID%>").val();
        var weightForAge = $("#<%=txtWAz.ClientID%>").val();
         var weightForHeight = $("#<%=txtWHz.ClientID%>").val();
        var dateOfVisit = $('#myVisitDate').datepicker('getDate');
        dateOfVisit = moment(dateOfVisit).format('DD-MMM-YYYY');
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
        var tempreture = $("#<%=Temperature.ClientID%>").val();
        if (tempreture === '') { tempreture = 0 }
        var respiratoryRate = $("#<%=RespiratoryRate.ClientID%>").val();
        if (respiratoryRate === '') { respiratoryRate = 0 }
        var patientId = <%=PatientId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        var heartRate = $("#<%=HeartRate.ClientID%>").val();
        if (heartRate === '') { heartRate = 0 }
        var boSaturation = $("#<%=bosaturation.ClientID%>").val();//todo Mwasi: check sp02
        if (boSaturation === '') { boSaturation = 0 }
        $.ajax({
            type: "POST",
            url: "../WebService/PatientService.asmx/AddpatientVitals",
            data: "{'patientId': '" + patientId + "','bpSystolic': '" + systolic + "','bpDiastolic': '" + diastolic + "','heartRate': '" + heartRate + "','height': '" + height + "','muac': '" + muacs + "','patientMasterVisitId': '" + patientMasterVisitId + "','respiratoryRate': '" + respiratoryRate + "','spo2': '" + boSaturation + "','tempreture': '" + tempreture + "','weight': '" + weight + "','bmi': '"+ bmi +"','headCircumference': '"+ headCircumference + "','bmiz':'"+bmiz+"','weightForAge':'"+weightForAge+"','weightForHeight':'"+weightForHeight+"','visitDate':'"+dateOfVisit+"'}", //todo Mwasi: add patient id and patientvistId
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
        $("#PersonDoB").val("");
    }

    function calcZScore()
    {
        var weight = document.getElementById('weights').value;
        var height = document.getElementById('Heights').value;
        $.ajax({
            url: '../WebService/PatientEncounterService.asmx/getZScoreValues',
            type: 'POST',
            dataType: 'json',
            data: "{'height':'" + height + "','weight':'" + weight + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var serverData = data.d;
                for (var i = 0; i < serverData.length; i++) {
                    $("#txtWAz").val(serverData[i][0]);
                    $("#txtWHz").val(serverData[i][1]);
                    $("#txtBMIz").val(serverData[i][2]);
                       
                }
            }
        });
    }
</script>
