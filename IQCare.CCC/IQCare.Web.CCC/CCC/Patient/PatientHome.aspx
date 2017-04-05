<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

<%--<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>--%>

<%--<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>--%>
<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
      <%--  <div class="col-md-12 col-xs-12 col-sm-12" style="margin-top: 0%;padding-top: 0%">--%>
            <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
        <%--</div>--%>

    <div class="col-md-12 col-xs-12 col-sm-12 form-group">
        <div class="col-md-6 col-xs-12 col-sm-6">
            
              <div id="vl_container" style="min-width: 300px; height: 350px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-6 col-xs-12 col-sm-6">
             <div id="vitals_container" style="min-width: 300px; height: 350px; margin: 0 auto"></div> 
            <!-- .bs-component-->
        </div>
    </div>
    

    <div class="col-md-12 col-xs-12 col-sm-12">
      
        <ul class="nav nav-tabs">

            <li class="active"><a data-toggle="tab" href="#Registration">Registration Summary </a></li>
            <li class="disabled"><a data-toggle="tab" href="#EntryPoint"><strong>Entry Point & Transfer Status</strong></a> </li>
            <li class="disabled"><a data-toggle="tab" href="#Baseline">Baseline Assessment & Treament Initiation </a></li>
           
        

        </ul>
    
        <div class="col-md-12 col-xs-12 col-xs-12 form-group">
            <div class="col-md-12 form-group"></div> 
            <div class="tab-content">
                 <div id="Registration" class="tab-pane fade in active">  </div> 
                 <div id="EntryPoint" class="tab-pane fade">
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
                                   <div class="col-md-4"><label class="control-lable pull-left">Regimen : </label></div>
                                  <div class="col-md-8"><asp:Label runat="server" CssClass=" pull-right text-primary"  ID="lblTIRegimen" ClientIDMode="Static"></asp:Label></div>
                              </div>
                            
                              <div class="col-md-12">
                                   <div class="col-md-5"><label class="control-lable pull-left">Facility From :</label></div>
                                  <div class="col-md-7"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblFacilityFrom" ClientIDMode="Static"></asp:Label></div>
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
                 <div id="Baseline" class="tab-pane fade">
                     <div class="col-md-4 col-xs-4 col-sm-4">
                         <div class="col-md-12 label label-info"><label class="control-label label label-info"><strong class="text-primary"></strong> <h6>Baseline Assessment Summary</h6></label></div>
                              <div class="col-md-12"><hr style="margin-top:1%"/></div>
                             
                              <div class="col-md-12 form-group">
                                   
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">HBV Infected</label></div>
                                       <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblHbvInfectedYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblHbvInfectedNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>
                                                               
                                  <div class="col-md-12 form-group">
                                      <div class="col-md-6"><label class="control-lable pull-left">Pregnant </label></div>
                                       <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPregnantYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblPregnantNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                      <div class="col-md-6"><label class="control-lable pull-left">TB Infected</label></div> 
                                      <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTBInfectedYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblTBInfectedNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                      <div class="col-md-6"><label class="control-lable pull-left">BreastFeeding</label></div> 
                                      <div class="col-md-3">
                                            <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastfeedingYes">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> Yes </span>
                                            </label>
                                       </div>
                                      <div class="col-md-3">
                                           <label class="checkbox-custom checkbox-inline" data-initialize="checkbox"  id="lblBreastfeedingNo">
                                                  <input class="sr-only" type="checkbox" value="option1"> <span class="checkbox-label pull-left"> NO </span>
                                            </label>
                                      </div>
                                  </div>

                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Who Stage</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblwhostage2" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                              
                                  <div class="col-md-12 form-group">
                                   <div class="col-md-6"><label class="control-lable pull-left">CD4 Count</label></div>
                                  <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary" ID="lblcd4" ClientIDMode="Static"></asp:Label></div>
                              </div>
                              

                            </div>                      

                          </div>
                    
                     <div class="col-md-4 col-xs-4 col-sm-4">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6><strong>Patient Baseline Vitals</strong></h6> </label></div>
                              <div class="col-md-12"><hr style="margin-top:1%" class="bg-info"/></div>
                              <div class="col-md-12">
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">MUAC</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblmuac" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Weight</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblweight" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Height</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblheight" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">BMI</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblbmi" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                              </div>
                          </div>

                      <div class="col-md-4 col-xs-4 col-sm-4">
                              <div class="col-md-12 label label-info"><label class="control-label label label-info"> <h6><strong>Treatment Initiation </strong></h6> </label></div>
                              <div class="col-md-12"><hr style="margin-top:1%" class="bg-info"/></div>
                              <div class="col-md-12">
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-7"><label class="control-lable pull-left">Date Started Firstline:</label></div>
                                     <div class="col-md-5"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblFirstline" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">ART Cohort: </label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblcohort" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                    <div class="col-md-12 form-group">
                                       <div class="col-md-5"><label class="control-lable pull-left">Regimen: </label></div>
                                     <div class="col-md-7"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblRegimenName" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">Baseline ViralLoad :</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblbaselineVL" ClientIDMode="Static"></asp:Label></div>
                                  </div>
                                  
                                  <div class="col-md-12 form-group">
                                       <div class="col-md-6"><label class="control-lable pull-left">ViralLoad Date :</label></div>
                                     <div class="col-md-6"><asp:Label runat="server" CssClass="pull-right text-primary"  ID="lblBlDate" ClientIDMode="Static"></asp:Label></div>
                                  </div>

                              </div>
                          </div>
                 </div> <!-- diagnosis -->  
   
             </div><!-- .tab-content -->
        </div> <!-- col-md-12 -->

    </div> <!-- .col-md-12 col-xs-12 col-sm-12 -->

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
     <!-- ajax begin -->
    <script type="text/javascript">

        $(document).ready(function() {

            var patientId = "<%=PatientId%>";
            
            /* populate patient baseline information */
            $.ajax({
                type: "POST",
                url: "../WebService/PatientBaselineService.asmx/GetPatientBaselineInfo",
                data: "{'patientId':'" + patientId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);
                    $.each(itemList,
                        function(index, itemList) {
                            if (itemList.patientId > 0) {
                                
                                /* transferin status */
                                $("#<%=lblTransferinDate.ClientID%>").text(moment(itemList.TransferInDate).format("DD-MMM-YYYY"));
                                $("#<%= lblTreatmentStartDate.ClientID%>").text(moment(itemList.TreatmentStartDate).format("DD-MMM-YYYY"));
                                $("#<%=lblTIRegimen.ClientID%>").text(itemList.CurrentTreatmentName);
                                $("#<%=lblFacilityFrom.ClientID%>").text(itemList.FacilityFrom);

                                /*patient Diagnosis */
                                $("#<%=lblDateOfHivDiagnosis.ClientID%>").text(moment(itemList.HivDiagnosisDate).format("DD-MMM-YYYY"));
                                $("#<%=lblDateOfEnrollment.ClientID%>").text(moment(itemList.EnrollmentDate).format("DD-MMM-YYYY"));
                                $("#<%=lblWhoStage.ClientID%>").text(itemList.EnrollmentWHOStageName);
                                $("#<%=lblARTInitiationDate.ClientID%>").text(moment(itemList.ARTInitiationDate).format("DD-MMM-YYYY"));
                                $("#<%=lblwhostage2.ClientID%>").text(itemList.WHOStageName);
                                $("#<%=lblcd4.ClientID%>").text(itemList.CD4Count);
                                $("#<%=lblmuac.ClientID%>").text(itemList.MUAC);
                                $("#<%=lblweight.ClientID%>").text(itemList.Weight);
                                $("#<%=lblheight.ClientID%>").text(itemList.Height);
                                $("#<%=lblbmi.ClientID%>").text(itemList.BMI.toFixed(2));

                                $("#<%=lblFirstline.ClientID%>").text(moment(itemList.DateStartedOnFirstline).format("DD-MMM-YYYY"));
                                $("#<%=lblcohort.ClientID%>").text(itemList.Cohort);
                                $("#<%=lblRegimenName.ClientID%>").text(itemList.RegimenName);
                                $("#<%=lblbaselineVL.ClientID%>").text(itemList.BaselineViralLoad + ' copies/ml');
                                $("#<%=lblBlDate.ClientID%>").text(moment(itemList.BaselineViralLoadDate).format("DD-MMM-YYYY"));
                                
                                /* patient baseline */
                                if (!itemList.HBVInfected) {
                                    $("#lblHbvInfectedNo").checkbox('check');
                                    $("#lblHbvInfectedYes").checkbox('uncheck');
                                } else {
                                    $("#lblHbvInfectedNo").checkbox('uncheck');
                                    $("#lblHbvInfectedYes").checkbox('check');
                                }

                                if (!itemList.Pregnant) {
                                    $("#lblPregnantNo").checkbox('check');
                                    $("#lblPregnantYes").checkbox('uncheck');
                                } else {
                                    $("#lblPregnantNo").checkbox('uncheck');
                                    $("#lblPregnantYes").checkbox('check');
                                }

                                if (!itemList.TBinfected) {
                                    $("#lblTBInfectedNo").checkbox('check');
                                    $("#lblTBInfectedYes").checkbox('uncheck');
                                } else {
                                    $("#lblTBInfectedNo").checkbox('uncheck');
                                    $("#lblTBInfectedYes").checkbox('check');
                                }

                                if (!itemList.BreastFeeding) {
                                    $("#lblBreastfeedingNo").checkbox('check');
                                    $("#lblBreastfeedingYes").checkbox('uncheck');
                                } else {
                                    $("#lblBreastfeedingNo").checkbox('uncheck');
                                    $("#lblBreastfeedingYes").checkbox('check');
                                }




                            }
                        });
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message);
                }
            });

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


            var jan_height = "";
            var march_height = "";
            var jan_weight = "";
            var march_weight = "";
            var jan_BMI = "";
            var march_BMI = "";

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
