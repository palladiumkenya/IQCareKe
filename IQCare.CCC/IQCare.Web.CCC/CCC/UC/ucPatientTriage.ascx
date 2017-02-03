<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriage.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriage" %>

<div class="col-md-12 bs-callout bs-callout-primary">

    <div class="col-md-12"><small class="muted pull-left"><strong><i class="fa fa-heartbeat fa-2x" aria-hidden="true"></i>PATIENT TRIAGE </strong></small></div>
    <div class="col-md-12">
        <hr />
    </div>
    <div class="col-md-12" id="vitalsform" data-parsley-validate="true" data-show-errors="true">
        <div class="col-md-6" id="anthropometricMeasurement" data-parsley-validate="true" data-show-errors="true">
            <div class="col-md-12">
                <label class="control-label text-primary pull-left text-muted">Anthropometric Measurement</label></div>
            <div class="col-md-12">
                <br />
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger">*</small> Height </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="Heights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." required="true" data-parsley-required="true" Type="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger">*</small> Weight </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="weights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="kgs.." required="true" data-parsley-required="true" Type="Number" ></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger">*</small> Body Mass Index(BMI) </label>
                </div>
                <div class="col-md-6">
                    <%--<asp:Label runat="server"  ID ="bmi" Value = "" CssClass="control-label text-warning pull-left"></asp:Label>--%>
                    <asp:TextBox runat="server" ID="bmivalue" Enabled="False" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="bmi" Type="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger"></small>Head Circumference (cm) </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="circumference" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger"></small>MUAC (cms) </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="muacs" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.." Type="Number"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="col-md-12">
                <label class="control-label text-primary pull-left text-muted">Vital Signs</label></div>
            <div class="col-md-12">
                <br />
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6 clearfix">
                    <label class="control-label pull-left"><small class="text-danger"></small>Blood Pressure </label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="distolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="sys.." Type="Number"></asp:TextBox>
                </div>
                <%--<div class="col-md-2 clearfix">
                    <label class="control-label">/</label></div>--%>
                <div class="col-md-3 clearfix">
                    <asp:TextBox runat="server" ID="systolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="di.." Type="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger"></small>Temperature </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="Tempreture" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger"></small>Respiratory Rate </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="RespiratoryRate" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger"></small>Blood Oxygen Saturation </label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="bosaturation" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="%.." Type="Number"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 form-group">
                <div class="col-md-6">
                    <label class="control-label pull-left"><small class="text-danger"></small>Heart Rate</label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="HeartRate" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." Type="Number"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <%-- .col-md-11--%>

    <div class="col-md-12">
        <hr />
    </div>
    <div class="col-md-12">
        <div class="col-md-8"></div>
        <div class="col-md-4">
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnSaveTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Triage </asp:LinkButton></div>
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg "> Reset Entry  </asp:LinkButton></div>
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg"> Close Triage </asp:LinkButton></div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
            $("#btnSaveTriage").click(function() {
                if ($('#vitalsform').parsley().validate()) {
                    addPatientVitals();
                } else {
                    return false;
                }
            });   
    });
    $("#Heights").change(function () {
        var bmi = calcBMI();
        document.getElementById("bmivalue").value = bmi;
    });
    $("#weights").change(function () {
        var bmi = calcBMI();
        document.getElementById("bmivalue").value = bmi;
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
            var weight =  $("#<%=weights.ClientID%>").val();
            var bmi =  $("#<%=bmivalue.ClientID%>").val();//todo Mwasi: add bmi and headcircumference to database model
            var headCircumference =  $("#<%=circumference.ClientID%>").val();
            var muacs = $("#<%=muacs.ClientID%>").val();
            var diastolic = $("#<%=distolic.ClientID%>").val();
            var systolic = $("#<%=systolic.ClientID%>").val();
            var tempreture = $("#<%=Tempreture.ClientID%>").val();
            var respiratoryRate = $("#<%=RespiratoryRate.ClientID%>").val();
            var patientId = 0;
            var patientMasterVisitId = 0;
            var heartRate = $("#<%=HeartRate.ClientID%>").val();
        var boSaturation = $("#<%=bosaturation.ClientID%>").val();//todo Mwasi: check sp02
        debugger;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddpatientVitals",
                data: "{'patientId': '" + patientId + "','bpSystolic': '" + systolic + "','bpDiastolic': '" + diastolic + "','heartRate': '" + heartRate + "','height': '" + height + "','muac': '" + muacs + "','patientMasterVisitId': '" + patientMasterVisitId + "','respiratoryRate': '" + respiratoryRate + "','spo2': '" + boSaturation + "','tempreture': '" + tempreture + "','weight': '" + weight + "','bmi': '"+ bmi +"','headCircumference': '"+ headCircumference + "'}", //todo Mwasi: add patient id and patientvistId
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                   //generate('success', ''+response.d);
                },
                error: function (response) {
                   //generate('error', response.d);
                }
            });
        }
</script>