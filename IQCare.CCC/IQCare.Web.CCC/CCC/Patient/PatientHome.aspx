<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
        </div>
    </div>
    <div class="col-md-12 form-group">
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
    <div class="col-md-3 col-xs-3">

        <div class="col-md-12">

        </div>
    </div>
    <div class="col-md-8 col-xs-8 col-sm-8">
      
        <ul class="nav nav-tabs">

            <li class="active"><a data-toggle="tab" href="#EntryPoint">
                <span class="fa-stack fa-lg">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-info-circle fa-stack-1x fa-inverse"></i>
                </span>
               <strong class="text-info">Entry Point & Transfer Status</strong> <br>
                
            </a></li>

            <li><a data-toggle="tab" href="#Diagnosis">Diagnosis & ARV HIstory </a></li>
        
            <li><a data-toggle="tab" href="#Baseline">Baseline Assessment & Treament Initiation </a></li>
        </ul>
    
        <div class="col-md-12 col-xs-12 col-xs-12 form-group">
            <div class="col-md-12 form-group"></div> 
            <div class="tab-content">
                 <div id="EntryPoint" class="tab-pane fade in active">
                     <div class="col-md-12 col-xs-12 col-sm-12">
                         <div class="col-md-4 col-xs-12 col-sm-12">

                         </div><!-- .col-md-4 -->

                         <div class="col-md-4 col-xs-12 col-sm-12">
                         </div><!-- .col-md-4 -->

                         <div class="col-md-4 col-xs-12 col-sm-12">

                         </div><!-- .col-md-4 -->
                     </div>
                 </div>
                 <div id="Diagnosis" class="tab-pane fade"> </div>
                 <div id="Baseline" class="tab-pane fade"></div>       
             </div><!-- .tab-content -->
        </div> <!-- col-md-12 -->

    



    </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->

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
