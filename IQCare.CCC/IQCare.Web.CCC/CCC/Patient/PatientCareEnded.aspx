<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientCareEnded.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientCareEnded" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="uc" TagName="ucExtruder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style>
          .CloseDetails{
              visibility:visible;
          }
         .CareendDetails{
             visibility:hidden;
         }

              
    </style>
    <div class="col-md-12">
    <uc:PatientDetails runat="server" />
    <div class="col-md-12 col-xs-12 panel panel-default">

         <div class="panel-body">
              <div class="col-md-12 col-xs-12"><label class="control-label text-primary pull-left">Patient Care Ending.</label></div>
              <div class="col-md-12 col-xs-12"><hr/></div>
              <div class="col-md-12 col-xs-12" id="PatientCareEndGrid   ">
                 <table class="table table-condensed table-striped table-hover" id="tblCareEnded" clientidmode="Static" runat="server">
                    <thead>
                        <tr class="active">
                            <th><span class="text-primary" aria-hidden="true">#</span></th>
                            <th><span class="text-primary" aria-hidden="true">Exit Date</span> </th>
                            <th><span class="text-primary" aria-hidden="true">Exit Reason</span> </th>
                            <th><span class="text-primary" aria-hidden="true">Status</span> </th>
                           
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                 </table>
              </div>

              <div class="col-md-12 col-xs-12 form-group" id="CareEndedForm" data-parsley-validate="true">           
      
                   <div class="col-md-12 col-xs-12 form-group " >
                        <div class="col-md-12 col-xs-12">
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label for="reason" class="control-label pull-left">Care Ending Reason</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="Reason" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                </div>
                            </div>
                            
                            <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label class="control-label pull-left">Exit Date</label>
                                </div>
                                <div class="col-md-12">
                                    <div class="datepicker fuelux form-group" id="CareEndDate">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="AppointmentDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                                                </span><span class="year">2017</span>
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
                                                                <li data-month="0">
                                                                    <button type="button">Jan</button></li>
                                                                <li data-month="1">
                                                                    <button type="button">Feb</button></li>
                                                                <li data-month="2">
                                                                    <button type="button">Mar</button></li>
                                                                <li data-month="3">
                                                                    <button type="button">Apr</button></li>
                                                                <li data-month="4">
                                                                    <button type="button">May</button></li>
                                                                <li data-month="5">
                                                                    <button type="button">Jun</button></li>
                                                                <li data-month="6">
                                                                    <button type="button">Jul</button></li>
                                                                <li data-month="7">
                                                                    <button type="button">Aug</button></li>
                                                                <li data-month="8">
                                                                    <button type="button">Sep</button></li>
                                                                <li data-month="9">
                                                                    <button type="button">Oct</button></li>
                                                                <li data-month="10">
                                                                    <button type="button">Nov</button></li>
                                                                <li data-month="11">
                                                                    <button type="button">Dec</button></li>
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
                            </div>
                        </div>
                        </div>
                       
                       <div class="col-md-12" id="lostToFollowUp">
                           <div class="col-md-12">
                               <small class="pull-left text-primary">Lost to Follow Up</small>
                               <hr />
                           </div>
                           
                           <div class="col-md-12" id="lostToFollowUpPanel">
                               <div class="col-md-4">
                                   <div class="col-md-12"><label class="control-lable pull-left">Tracing Outcome:</label></div>
                                   <div class="col-md-12">
                                       <asp:DropDownList runat="server" ID="TracingOutcome" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                   </div>
                               </div>
                               
                               <div class="col-md-4">
                                   <div class="col-md-12"><label class="control-lable pull-left">Reason lost to Follow Up:</label></div>
                                   <div class="col-md-12">
                                       <asp:DropDownList runat="server" ID="ReasonLostToFollowup" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                                   </div>
                               </div>
                           </div>
                       </div>
                       
                   <div class="col-md-12" id="documentedTransferOut">
                       <div class="col-md-12">
                           <small class="pull-left text-primary">Transfer Out (Documented)</small>
                           <hr />
                       </div>
                           
                       <div class="col-md-12" id="documentedTransferOutPanel">
                           <div class="col-md-4" id="TransferOutFacility">
                               <div class="col-md-12"><label class="control-lable pull-left">Tranfer Out Facility:</label></div>
                               <div class="col-md-12">
                                   <asp:TextBox runat="server" CssClass="form-control input-sm" ID="Facility" ClientIDMode="Static"></asp:TextBox>
                               </div>
                           </div>
                               
                           <div class="col-md-4" id="ReasonForTransfer">
                               <div class="col-md-12"><label class="control-lable pull-left">Tranfer Out Facility:</label></div>
                               <div class="col-md-12">
                                   <asp:TextBox runat="server" CssClass="form-control input-sm" ID="ReasonForTransferOut" ClientIDMode="Static"></asp:TextBox>
                               </div>
                           </div>
                           
                           <div class="col-md-4">
                               <div class="col-md-12"><label class="control-lable pull-left">Date Expected to Report in new Facility:</label></div>
                               <div class="col-md-12">
                                   <div class='input-group date' id='DateExpectedToReportpicker'>
                                       <span class="input-group-addon">
                                           <span class="glyphicon glyphicon-calendar"></span>
                                       </span>
                                       <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="DateExpectedToReport" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                                   </div>
                               </div>
                           </div>
                       </div>
                   </div>
                       
                   <div class="col-md-12" id="death">
                       <div class="col-md-12">
                           <small class="pull-left text-primary">Death</small>
                           <hr />
                       </div>

                       <div class="col-md-4" id="DeathDate">
                                <div class="col-md-12">
                                    <label class="control-label pull-left">Date Of Death</label>
                                </div>
                                <div class="col-md-12">
                                    <div class="datepicker fuelux" id="DateOfDeath">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="DeathDates" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                                                </span><span class="year">2017</span>
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
                                                                <li data-month="0">
                                                                    <button type="button">Jan</button></li>
                                                                <li data-month="1">
                                                                    <button type="button">Feb</button></li>
                                                                <li data-month="2">
                                                                    <button type="button">Mar</button></li>
                                                                <li data-month="3">
                                                                    <button type="button">Apr</button></li>
                                                                <li data-month="4">
                                                                    <button type="button">May</button></li>
                                                                <li data-month="5">
                                                                    <button type="button">Jun</button></li>
                                                                <li data-month="6">
                                                                    <button type="button">Jul</button></li>
                                                                <li data-month="7">
                                                                    <button type="button">Aug</button></li>
                                                                <li data-month="8">
                                                                    <button type="button">Sep</button></li>
                                                                <li data-month="9">
                                                                    <button type="button">Oct</button></li>
                                                                <li data-month="10">
                                                                    <button type="button">Nov</button></li>
                                                                <li data-month="11">
                                                                    <button type="button">Dec</button></li>
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
                        </div>
                       
                       <div class="col-md-4">
                           <div class="col-md-12">
                               <label for="reason" class="control-label pull-left">Reason for death</label>
                           </div>
                           <div class="col-md-12">
                               <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                           </div>
                       </div>
                       
                       <div class="col-md-4">
                           <div class="col-md-12">
                               <label for="reason" class="control-label pull-left">Specific Cause of death</label>
                           </div>
                           <div class="col-md-12">
                               <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control input-sm" ClientIDMode="Static" required="true" data-parsley-min="1" />
                           </div>
                       </div>
                   </div>
                  </div>
                  <div class="col-md-12 col-xs-12 ">
                      <div class="col-md-6 col-xs-6">

                                <div class="col-md-12">
                                    <label for="reason" class="control-label pull-left"> Care Ending Notes</label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtCareEndingNotes" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                                </div>
                           
                       
                      </div>
                      <div class="col-md-6 col-xs-6">
                          
                      </div>
                  </div>
                  
                  <div class="col-md-12">
                      <div class="col-md-12"><hr/></div>
                      <div class="col-md-12">
                          <div class="col-md-4"></div>
                          <div class="col-md-6  CloseDetails">
                              
                              
                      <button type="button" id="btnClose" class="btn btn-info btn-lg fa fa fa-user-times text-danger pull-right"  onclick="CloseForm();">Close</button>
                              
                          </div>
                          <div class="col-md-2">
                              <div class="col-md-4"></div> 
                              <div class="col-md-4">
                                  

                              </div>
                              <div class="col-md-4 pull-right CareendDetails">
                                  

                                <asp:LinkButton runat="server" ID="EndCare" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa fa-user-times text-danger pull-right"> CareEnd Patient</asp:LinkButton>
                          
 
                               </div>
                          </div>

                      </div>
                  </div>
                 
              </div> <!-- careending \form-->
         </div>

    </div>

        <uc:ucExtruder runat="server" ID="ucExtruder" />
</div>
    
    <script type="text/javascript">
        var pmVisitId = <%=PmVisitId%> ;
        function CloseForm() {
                window.location.href = '<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';
            }
        $(document).ready(function() {

            if (pmVisitId > 0) {
                  $('.CloseDetails').css('visibility', 'visible');

                $('.CareendDetails').css('visibility', 'hidden');
            }
            $("#Facility").prop("disabled", true);
            var today = new Date();
            var futuredate;
            
            if (parseInt(pmVisitId, 10) > 0) {
                $('.CloseDetails').css('visibility', 'visible');

                $('.CareendDetails').css('visibility', 'hidden');
            }
            else {
                 $('.CloseDetails').css('visibility', 'hidden');

                $('.CareendDetails').css('visibility', 'visible');
            }
         getCareEndedByVisitId();
           
         $("#CareEndDate").datepicker({
                date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
         });

         $("#DateOfDeath").datepicker({
                date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
         });

         $("#DateOfDeath").datepicker('disable');


          
        /* validate future dates for date of death */

         $('#DateOfDeath').on('changed.fu.datepicker dateClicked.fu.datepicker', function (evt, date) {
        
             var dateofDeath = $('#DateOfDeath').datepicker('getDate');
             var today = new Date();
            
             futuredate= moment(dateofDeath).isAfter(today);
             if (futuredate) {
                $('#DeathDates').val('');
                 toastr.error("Future Dates not allowed !");                
                 return false;
             }
         });

            /* validate future dates for date of exit */
         $('#CareEndDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function (evt, date) {
        
             var careEndDate = $('#CareEndDate').datepicker('getDate');
             var dateofDeath = $('#DateOfDeath').datepicker('getDate');

             var dateVar = moment(dateofDeath, 'YYYY-MM-DD');
             var reason = $(this).find(":selected").text();

             if (!dateVar.isValid() && reason !== "Death") {
                 dateofDeath = '';
             }
            
             futuredate= moment(careEndDate).isAfter(today);
             if (futuredate) {
                 toastr.error("Future dates not allowed!");     
                 $('#AppointmentDate').val('');            
                 return false;
             }

             futuredate = moment(careEndDate).isBefore(dateofDeath);
             if (futuredate) {
                  $('#AppointmentDate').val('');
                    toastr.error("Exit Date CANNOT be before date of death");
                    return false;
                }
         });

         //$('#DateOfDeath').on('changed.fu.datepicker dateClicked.fu.datepicker',function(evt, date) {

         //       var exitDate = $("#CareEndDate").datepicker('getDate');
         //       var dateofDeath = $('#DateOfDeath').datepicker('getDate');

         //       futuredate= moment(dateofDeath).isAfter(today);
         //       if (futuredate) {
         //           $('#DateOfDeath').val('');
         //           toastr.error("Future Dates NOT allowed for Date of Death!");
         //           return false;
         //       }
         //       futuredate= moment(exitDate).isBefore(dateofDeath);
         //       if (futuredate) {
         //           toastr.error("Exit Date CANNOT be before date of death");
         //           return false;
         //       }
         //   });

            /*careenging reason */
            $("#<%=Reason.ClientID%>").change(
                function() {
                    var reason = $(this).find(":selected").text();
                    if (reason === 'Transfer Out') {
                        $("#DeathDates").val('');
                        $("#DateOfDeath").datepicker('disable');
                        $("#Facility").val('');
                        $("#Facility").prop("disabled", false);
                    }
                    else if (reason === 'Death') {
                        $("#DeathDates").val('');
                        $("#DateOfDeath").datepicker('enable');
                        $("#Facility").val('');
                        $("#Facility").prop("disabled", true);
                    } else {
                        $("#DeathDates").val('');
                        $("#DateOfDeath").datepicker('disable');
                        $("#Facility").val('');
                        $("#Facility").prop("disabled", true);
                    }
                });

           


            $("#EndCare").click(function () {
                if ($("#CareEndedForm").parsley().validate()) {
                    var careEndedDate = $('#CareEndDate').datepicker('getDate');
                var reason = $("#<%=Reason.ClientID%>").val();
                var careEndingNotes = escape($("#<%=txtCareEndingNotes.ClientID%>").val());
                    var transferOutFacility = $("#<%=Facility.ClientID%>").val();
                    var dateOfDeath = $('#DateOfDeath').datepicker('getDate');
                if (careEndedDate == "Invalid Date") {
                    careEndedDate = "";
                    toastr.error("Kindly fill in the CareEnd Date");
                    return false;
                }
                    if (dateOfDeath == "Invalid Date" && reason === 'Death') {
                    toastr.error("Kindly fill in the Death Date");
                    dateOfDeath = "";
                    return false;
                }

                    $.when(endCare()).then(function() {
                        setTimeout(function() {
                                getCareEnded();
                            },
                            2000);
                    });
                } else {
                    return false;
                }
                
            });



            getCareEnded();

            function endCare() {
                var careEndedDate = $('#CareEndDate').datepicker('getDate');
                var reason = $("#<%=Reason.ClientID%>").val();
                var careEndingNotes = escape($("#<%=txtCareEndingNotes.ClientID%>").val());
                var transferOutFacility = $("#<%=Facility.ClientID%>").val();
                var dateOfDeath = $('#DateOfDeath').datepicker('getDate');
                if (careEndedDate == "Invalid Date") {
                    careEndedDate = "";
                    
                    return false;
                }
                if (dateOfDeath == "Invalid Date" && reason === 'Death') {
                      
                    dateOfDeath = "";
                    return false;
                }

                if (Object.prototype.toString.call(dateOfDeath) === '[object Date]') {    
                    dateOfDeath = moment(moment(dateOfDeath, 'DD-MMM-YYYY')).format('DD-MMM-YYYY');
                } else {
                    dateOfDeath = '';
                }

                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/EndPatientCare",
                    data: "{'exitDate':'" + moment(careEndedDate).format('DD-MMM-YYYY') + "','exitReason':'" + reason + "','careEndingNotes':'" + careEndingNotes + "','facilityOutTransfer':'" + transferOutFacility + "','dateOfDeath':'" + dateOfDeath + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                         window.location.href = '<%=ResolveClientUrl( "~/CCC/Patient/PatientFinder.aspx")%>';
                        toastr.success(response.d);
                           <%-- window.location.href = '<%=ResolveClientUrl( "~/CCC/Patient/PatientHome.aspx")%>';--%>
                           
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                        return false;
                    }
                });
            }
            function getCareEndedByVisitId() {
                if (parseInt(pmVisitId, 10) > 0) {
                    $.ajax(
                        {
                            type: "POST",
                            url: "../WebService/EnrollmentService.asmx/GetPatientCareEndingDetailsByVisitId",
                            data: "{'PatientMasterVisitId':'" + pmVisitId + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            cache: false,
                            success: function (response) {
                                //$("#tblCareEnded > tbody").empty();
                                $('#tblCareEnded tr:not(:first)').remove();
                                console.log(response.d);
                                var itemList = response.d;
                                var table = '';

                               

                                if (itemList != null) {
                                    //console.log(itemList);
                                 
                                    if (itemList.ExitDate != null || itemList.ExitDate != undefined) {
                                        $('#CareEndDate').datepicker("setDate", moment(itemList.ExitDate).format('DD-MMM-YYYY').toString());
                                    }
                                    if (itemList.ExitReason != null || itemList.ExitReason!= undefined) {
                                        $("#<%=Reason.ClientID%>").val(itemList.ExitReason.toString());
                                    }
                                    if (itemList.CareEndingNotes != null || itemList.CareEndingNotes != undefined) {
                                        $("#<%=txtCareEndingNotes.ClientID%>").val(itemList.CareEndingNotes.toString());
                                     }
                                         if (itemList.TransferOutFacility != null || itemList.TransferOutFacility != undefined) {
                                             $("#<%=Facility.ClientID%>").val(itemList.TransferOutFacility.toString());
                                            }
                         if (itemList.DateOfDeath != null || itemList.DateOfDeath != undefined) {
                                 $('#DateOfDeath').datepicker("setDate", moment(itemList.DateOfDeath).format('DD-MMM-YYYY').toString())
                                 }
                                  }
                        
                                //itemList.forEach(function (item, i) {
                                //    n = i + 1;
                                //    table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + moment(item.ExitDate).format('DD-MMM-YYYY') + '</td><td style="text-align:left">' + item.ExitReason + '</td><td style="text-align: left">' + moment(item.dateOfDeath).format('DD-MMM-YYYY') + '</td><td style="text-align:left">' + item.transferOutFacility + '</td><td style="text-align:left">' + item.dateofDeath + '</td><td style="text-align:left">' + item.careEndingNotes + '</td></tr>';
                                //});

                                //$('#tblCareEnded').append(table);
                            
                            },

                            error: function (xhr, errorType, exception) {
                                var jsonError = jQuery.parseJSON(xhr.responseText);
                                toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                                return false;
                            }
                        });
                }
            }

            
            
            function getCareEnded() {
                $.ajax(
                {
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/GetPatientCareEnded",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function (response) {
                        //$("#tblCareEnded > tbody").empty();
                        $('#tblCareEnded tr:not(:first)').remove();
                        console.log(response.d);
                        var itemList = response.d;
                        var table = '';
                        itemList.forEach(function (item, i) {
                            n = i + 1;
                            table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + moment(item.ExitDate).format('DD-MMM-YYYY') + '</td><td style="text-align:left">' + item.ExitReason + '</td><td style="text-align: left">' + item.Status + '</td></tr>';
                        });

                        $('#tblCareEnded').append(table);

                    },

                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                        return false;
                    }
                });
            }
        });
        
    </script>
</asp:Content>
