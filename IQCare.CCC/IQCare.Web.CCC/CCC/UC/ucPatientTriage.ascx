<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriage.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriage" %>

<div class="col-md-12 bs-callout bs-callout-primary well well-sm">

    <div class="col-md-12" id="vitalsform" data-parsley-validate="true" data-show-errors="true">
        <div class="col-md-12"> <h2 class="text-muted text-Warning pull-left"> Vital Signs | </h2>  <h6> <label class="control-label text-primary pull-left text-muted">Routine Vital Signs Capture</label></h6></div>

         <div class="col-md-12">  <hr style="padding-top:1%" /> </div>
        
        <div class="col-md-12">
            
                <div class="col-md-12"><label class="control-label pull-left">Visit Date :</label></div>
                <div class="col-md-12">
                    <div class="col-md-4">
                        <div class="datepicker fuelux form-group" id="myVisitDate">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PersonDoB" data-parsley-required="true"></asp:TextBox>        
                                                        <%-- <input ClientIDMode="Static" class="form-control input-sm" runat="server" id="DateOfBirth" type="date" />--%>
                                                        <div class="input-group-btn">
                                                            <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                            <span class="sr-only">Toggle Calendar</span>
                                                            </button>
                                                            <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                                                <div class="datepicker-calendar">
                                                                    <div class="datepicker-calendar-header">
                                                                        <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                                                        <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                                                        <button type="button" class="title" data-month="11" data-year="2014">
                                                                            <span class="month">
                                                                                <span data-month="0">January</span>
                                                                                <span data-month="1">February</span>
                                                                                <span data-month="2">March</span>
                                                                                <span data-month="3">April</span>
                                                                                <span data-month="4">May</span>
                                                                                <span data-month="5">June</span>
                                                                                <span data-month="6">July</span>
                                                                                <span data-month="7">August</span>
                                                                                <span data-month="8">September</span>
                                                                                <span data-month="9">October</span>
                                                                                <span data-month="10">November</span>
                                                                                <span data-month="11" class="current">December</span>
                                                                            </span> <span class="year">2014</span>
                                                                        </button>
                                                                    </div>
                                                                    <table class="datepicker-calendar-days">
                                                                    <thead>
                                                                    <tr>
                                                                        <th>Su</th>
                                                                        <th>Mo</th>
                                                                        <th>Tu</th>
                                                                        <th>We</th>
                                                                        <th>Th</th>
                                                                        <th>Fr</th>
                                                                        <th>Sa</th>
                                                                    </tr>
                                                                    </thead>
                                                                    <tbody></tbody>
                                                                    </table>
                                                                    <div class="datepicker-calendar-footer">
                                                                    <button type="button" class="datepicker-today">Today</button>
                                                                    </div>
                                                                </div>
                                                                <div class="datepicker-wheels" aria-hidden="true">
                                                                    <div class="datepicker-wheels-month">
                                                                    <h2 class="header">Month</h2>
                                                                    <ul>
                                                                        <li data-month="0"><button type="button">Jan</button></li>
                                                                        <li data-month="1"><button type="button">Feb</button></li>
                                                                        <li data-month="2"><button type="button">Mar</button></li>
                                                                        <li data-month="3"><button type="button">Apr</button></li>
                                                                        <li data-month="4"><button type="button">May</button></li>
                                                                        <li data-month="5"><button type="button">Jun</button></li>
                                                                        <li data-month="6"><button type="button">Jul</button></li>
                                                                        <li data-month="7"><button type="button">Aug</button></li>
                                                                        <li data-month="8"><button type="button">Sep</button></li>
                                                                        <li data-month="9"><button type="button">Oct</button></li>
                                                                        <li data-month="10"><button type="button">Nov</button></li>
                                                                        <li data-month="11"><button type="button">Dec</button></li>
                                                                    </ul>
                                                                    </div>
                                                                    <div class="datepicker-wheels-year">
                                                                    <h2 class="header">Year</h2>
                                                                    <ul></ul>
                                                                    </div>
                                                                    <div class="datepicker-wheels-footer clearfix">
                                                                    <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                                                    <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                            </div>
                    </div>
                    <div class="col-md-4"></div>
                    <div class="col-md-4"></div>
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
                            <asp:TextBox runat="server" ID="Heights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." required="true" data-parsley-required="true" Type="Number" Min="10"></asp:TextBox>
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
                             <asp:TextBox runat="server" ID="muacs" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number" Min="0"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="Tempreture" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number" Min="25" Max="50" data-parsley-range="[25, 50]" data-parsley-range-message="Tempreture is out of reasonable range"></asp:TextBox>
                    </div>
                    <p class="help-block pull-left"><strong>Normal 36.5–37.5 °C </strong></p>
                </div>   
                 

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
                <asp:LinkButton runat="server" ID="btnResets" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Entry  </asp:LinkButton>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnCancels" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close Triage </asp:LinkButton>
            </div>
        </div>
    </div>
</div> <!-- .col-md-12 -->

<script type="text/javascript">
    $(document).ready(function () {
        var age = "<%=patientAge%>";
        var gender = "<%=patientGender%>";
        var visitDate;

      //if (Gender === 'male') {
          
      //}else if (Gender == 'Female') {
          
      //}

        if(age > 15)
        {
            document.getElementById('divBMI').style.display = 'block';
            document.getElementById('peadsZScores').style.display = 'none';
        }
        else{
            document.getElementById('divBMI').style.display = 'none';
            document.getElementById('peadsZScores').style.display = 'block';
        }

        $('#myVisitDate').datepicker({
                        date:null,
                        allowPastDates: true,
                        momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
                        //restricted: [{ from: '01-01-2013', to: '01-01-2014' }]
        });



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
        calcZScore();
    });
    $("#weights").change(function () {
        var bmi = calcBMI();
        var weight = '';
        if(bmi<18.5){weight = '(Under weight)'}else if(bmi>=18.5<25){weight = '(Normal weight)'}else if(bmi>=25<30){weight = '(Over weight)'}else{weight = '(Obese)'}
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
         var dateOfVisit = moment($('#myVisitDate').datepicker('getDate').format('DD-MMM-YYYY'));
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
            data: "{'patientId': '" + patientId + "','bpSystolic': '" + systolic + "','bpDiastolic': '" + diastolic + "','heartRate': '" + heartRate + "','height': '" + height + "','muac': '" + muacs + "','patientMasterVisitId': '" + patientMasterVisitId + "','respiratoryRate': '" + respiratoryRate + "','spo2': '" + boSaturation + "','tempreture': '" + tempreture + "','weight': '" + weight + "','bmi': '"+ bmi +"','headCircumference': '"+ headCircumference + "','visitDate':'"+dateOfVisit+"'}", //todo Mwasi: add patient id and patientvistId
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
