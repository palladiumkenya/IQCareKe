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
        <div class="col-md-6">
            
              <div id="vl_container" style="min-width: 300px; height: 350px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-6">
            

            <div id="vitals_container" style="min-width: 300px; height: 350px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <%--<div class="col-md-3">
            <div id="weight_container" style="min-width: 300px; height: 200px; margin: 0 auto"></div> 
        </div>--%>
        <!-- .col-lg-3 -->
        <%--<div class="col-md-3">
          <%--<div id="bmi_container" style="min-width: 300px; height: 200px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>--%>
        <!-- .col-lg-3 -->
    </div>
    
    <div>
        <div class="row">
            <div class="col-md-12 col-xs-12">
                <div role="tabpanel" class="panel panel-default" id="home">
                    <div class="col-md-4" style="padding-left: 0px;">
                        <div class="panel-heading bs-callout bs-callout-success">Today's Vital Signs</div>
                        <div class="panel-body" style="border-left: solid gray 1px;">
                        
                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Height (cm)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalHeight" CssClass="control-label text-success pull-left">0 cms</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Weight (kg)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalsWeight" CssClass="control-label text-success pull-left">0 Kgs</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Head Circumference (cm)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalsCircumference" CssClass="control-label text-success pull-left">0 cms</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">MUAC (cm)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalsMUAC" CssClass="control-label text-success pull-left">0 cms</asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Blood Pressure </label></div>
                                <div class="col-md-4"> 
                                    <asp:Label runat="server" ID="vitalBloodPressure" CssClass="control-label text-success pull-left">0 </asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Temperature (0C)</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalTemperature" CssClass="control-label text-success pull-left">0 0C</asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Respiratory Rate</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="vitalRespiratoryRate" CssClass="control-label text-success pull-left">0 </asp:Label>
                                </div>
                            </div> 

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left">Blood Oxygen Saturation</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblOxygenSaturation" CssClass="control-label text-success pull-left">0 %</asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-4">
                        
                        <div class="panel-heading bs-callout bs-callout-default">Laboratory Summary</div>
                        <div class="panel-body" style="border-left: solid gray 1px;">
                            
                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment CD4</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="EnrollmentCD4" CssClass="control-label pull-left" ClientIDMode="Static">0</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment CD4 Date</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="EnrollmentCD4Date" CssClass="control-label pull-left" ClientIDMode="Static">00-00-0000</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment Viral Load</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="enrollmentViralload" CssClass="control-label pull-left" ClientIDMode="Static">0</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-8"><label class="control-label pull-left text-default">Enrollment Viralload Date</label></div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="enrollmentviralloaddate" CssClass="control-label pull-left" ClientIDMode="Static">00-00-000</asp:Label>
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                   &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="col-md-4">
                        
                        <div class="panel-heading bs-callout bs-callout-default">Diagnosis Summary</div>
                        <div class="panel-body" style="border-left: solid gray 1px;">
                            
                             <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                   &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                             <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                   &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-8">&nbsp;</div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>

                        </div>
                    </div>           
                </div>
            </div>
        </div>
    </div>

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
     <!-- ajax begin -->
    <script type="text/javascript">
      


     window.onload = function() {
            <%-- var patientId = <%=patientId%>;
            var march_height = <%=march_height%>;
            console.log(patientId);
            console.log(march_height);--%>
            // drawGraph();
            //};
            // $(document).ready(function() {

            $.ajax({
                url: '../WebService/PatientVitals.asmx/GetVitals',
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function(response) {
                    console.log(response.d);
                    var items = response.d;
                    items.forEach(function(item, i) {
                        console.log("Height: " + item.Height + "Month " + item.Month + " " + i);
                        if (item.Month == 1) {

                            var march_height = item.Month;
                            console.log("This is jan height: " + item.Height + " ");
                        }else if (item.Month == 1) {

                            var jan_height = item.Month;
                            console.log("This is Jan height: " + item.Height + " ");
                        }

                    });
                    //var itemList = JSON.parse(response.d);

                    //itemList.forEach(function (item) {

                }

            });

            $(function () {
                $('#vl_container').highcharts({
                    title: {
                        text: 'Viral Load Trend',
                        x: -20 //center
                    },
                    subtitle: {
                        text: 'VL cp/ml',
                        x: -20
                    },
                    xAxis: {
                        categories: ['Jan', 'Mar', 'May', 'Jul', 'Sep', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: 'Viral Load cp/ml'
                        },
                        plotLines: [
                            {
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }
                        ]
                    },
                    tooltip: {
                        valueSuffix: 'cp/ml'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: 'VL',
                            data: [200, 300, 500, 1000, 750, 500, 400]
                        }, {
                            name: 'Threshold',
                            data: [1000, 1000, 1000, 1000, 1000, 1000, 1000]
                        }
                    ]
                });
            });
            $(function () {
                $('#vitals_container').highcharts({
                    title: {
                        text: 'Vitals',
                        x: -20 //center
                    },
                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        categories: ['Jan', 'Mar', 'May', 'Jul', 'Sep', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        plotLines: [
                            {
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }
                        ]
                    },
                    tooltip: {
                        valueSuffix: ''
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: 'Height',
                            data: [118, 120, 126, 127, 128, 128, 126]
                        },
                        {
                            name: 'Weight',
                            data: [80, 82, 88, 82, 80, 78, 76]
                        },
                        {
                            name: 'BMI',
                            data: [24.70, 26.80, 28.80, 30.70, 27.90, 26.20, 25.50]
                        }
                    ]
                });
            });
        }
    </script>
</asp:Content>
