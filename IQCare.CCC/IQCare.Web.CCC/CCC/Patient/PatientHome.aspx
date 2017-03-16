﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

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
    

    <div class="col-md-12 col-xs-12 col-sm-12">
      
        <ul class="nav nav-tabs">

            <li class="active"><a data-toggle="tab" href="#EntryPoint"><strong>Entry Point & Transfer Status</strong></a> </li>

            <li class="disabled"><a data-toggle="tab" href="#Diagnosis">Diagnosis & ARV HIstory </a></li>
        
            <li class="disabled"><a data-toggle="tab" href="#Baseline">Baseline Assessment & Treament Initiation </a></li>
        </ul>
    
        <div class="col-md-12 col-xs-12 col-xs-12 form-group">
            <div class="col-md-12 form-group"></div> 
            <div class="tab-content">
                 <div id="EntryPoint" class="tab-pane fade in active">
                     <div class="col-md-12 col-xs-12">
                         
                             <div class="col-md-4 col-xs-4 col-sm-4">
                               <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong> <h6>TransferIn Status</h6></label></div>
                              <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                              <div class="col-md-12 form-group">
                                   <div class="col-md-6"><label class="control-lable pull-left">Entry Point :</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblEntryPoint" ClientIDMode="Static"></asp:Label></div>
                              </div>
                                 
                               <div class="col-md-12">
                                   <div class="col-md-6"><label class="control-lable pull-left">TransferIn Date :</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblTransferinDate" ClientIDMode="Static"></asp:Label></div>
                              </div>
                              
                             <div class="col-md-12">
                                   <div class="col-md-6"><label class="control-lable pull-left">Treatment Start:</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary" ID="lblTreatmentStartDate" ClientIDMode="Static"></asp:Label></div>
                              </div>
                              
                              <div class="col-md-12">
                                   <div class="col-md-6"><label class="control-lable pull-left">Regimen : </label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass=" pull-right text-primary"  ID="lblTIRegimen" ClientIDMode="Static"></asp:Label></div>
                              </div>
                            
                              <div class="col-md-12">
                                   <div class="col-md-6"><label class="control-lable pull-left">Facility From :</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblFacilityFrom" ClientIDMode="Static"></asp:Label></div>
                              </div>

                              

                          </div><!-- .col-md-4 -->

                          <div class="col-md-4 col-xs-12 col-sm-12">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6>Patient Diagnosis Status</h6></label></div>
                               <div class="col-md-12"><hr style="margin-top:1%"/></div>
                              
                               <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">HIV Diagnosis Date :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblDateOfHivDiagnosis" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>
                              
                              <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">Date of Enrollmet  :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblDateOfEnrollment" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>
                              
                               <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">WHO at Enrollmet  :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblWhoStage" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>
                              
                              
                              <div class="col-md-12 form-group">
                                    <div class="col-md-6"><label class="control-label pull-left">ART Initiation Date :</label></div>
                                    <div class="col-md-6"><asp:Label runat="server" ID="lblARTInitiationDate" CssClass="text-info pull-right" ClientIDMode="Static"></asp:Label></div>
                               </div>


                          </div><!-- .col-md-4 -->

                          <div class="col-md-4 col-xs-4 col-sm-4">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6>ARV History Status</h6> </label></div>
                              <div class="col-md-12"><hr style="margin-top:1%" class="bg-info"/></div>
                              <div class="col-md-12">
                                  <table class="table table-condensed" width="100%">
                                      <tr>
                                          <th align="left">Purpose</th>
                                          <th align="left">Regimen</th>
                                           <th right="left">Date Last Used</th>
                                      </tr>
                                      <tbody id="tbdArvHistory" clientidmode="Static"></tbody>
                                      <tfoot class="bg-info">
                                          <tr>
                                               <th align="left">Purpose</th>
                                          <th align="left">Regimen</th>
                                           <th right="left">Date Last Used</th>
                                          </tr>
                                      </tfoot>
                                  </table>
                              </div>
                          </div><!-- .col-md-4 -->

                     </div>
                 </div>
                 <div id="Diagnosis" class="tab-pane fade">
                     
                 </div>
                 <div id="Baseline" class="tab-pane fade"></div>       
             </div><!-- .tab-content -->
        </div> <!-- col-md-12 -->

    



    </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
     <!-- ajax begin -->
    <script type="text/javascript">

        $(document).ready(function() {

            var patientId = "<%=PatientId%>";

            $.ajax({
                type: "POST",
                url: "../WebService/PatientBaselineService.asmx/GetPatientPriorArvHistory",
                data: "{'patientId':'" + patientId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#tbdArvHistory").append(response.d);
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message);
                }
            });


            var jan_height = 0;
            var march_height = 0;
            var jan_weight = 0;
            var march_weight = 0;
            var jan_BMI = 0.0;
            var march_BMI = 0.0;

            window.onload = function() {

                $.when(getVitals()).then(function() {
                    setTimeout(function() {
                            vitals();
                        },
                        2000);

                });

                $(function() {
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

            };

            function getVitals() {
                console.log("get vitals called");
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

                            if (item.Month == 1) {

                                jan_height = item.Height;
                                jan_weight = item.Weight;
                                jan_BMI = item.BMI;
                                console.log("This is jan height: " + jan_height + " ");
                                console.log("This is jan weight: " + jan_weight + " ");
                                console.log("This is jan BMI: " + jan_BMI + " ");

                            } else if (item.Month == 3) {

                                march_height = item.Height;
                                march_weight = item.Weight;
                                march_BMI = item.BMI;
                                console.log("This is March height: " + march_height + " ");
                                console.log("This is March weight: " + march_weight + " ");
                                console.log("This is March BMI: " + march_BMI + " ");
                            }

                        });

                    }

                });
            }

            function vitals() {
                //var jan_height = $(".march_height").val();
                //var march_height = $(".march_height").val();
                console.log("vitals graph function called");

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
                            data: [jan_height, march_height, "", "", "", "", ""]
                        },
                        {
                            name: 'Weight',
                            data: [jan_weight, march_weight, "", "", "", "", ""]
                        },
                        {
                            name: 'BMI',
                            data: [jan_BMI, march_BMI, "", "", "", "", "", ""]
                        }
                    ]
                });
            }
       });
    </script>
</asp:Content>
