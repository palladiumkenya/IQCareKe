<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFemaleVitals.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucFemaleVitals" %>

<div class="col-md-12 col-xs-12 col-sm-12" id="divFemalVitals">
    
   <div class="col-md-12" id="FemaleVitals" data-parsley-validate="true" data-show-errors="true">

          <div class="col-md-12">
               <h2 class=" control-label pull-left"> Reproductive History |</h2> <h6><label class="control-label text-primary pull-left text-muted">Routine Female ONLY observations </label></h6>
          </div>

          <div class="col-md-12">
               <hr />
          </div>

          <div class="col-md-12 form-group">
              <div class="col-md-4">
                  <div class="col-md-12"><label class="required control-label pull-left">Visit Date</label></div>

                  <div class="col-md-12 form-group">
                      <div class='input-group date' id='FemaleVisitDate'>
                          <span class="input-group-addon">
                              <span class="glyphicon glyphicon-calendar"></span>
                          </span>
                          <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="femaleDateOfVisit" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                      </div>
                  </div>
               </div>
               <div class="col-md-4"></div>
               <div class="col-md-4"></div>
          </div>
        
          <div class="col-md-12 form-group">
            <div class="col-md-12"><hr/></div>
        </div>
       
       <div id="pregnancyStatusfolloUp">
           <div class="col-md-12 form-group">
               <div class="col-md-4">
                   <div class="col-md-12 form-group">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">Pregnancy Status</label>
                       </div>

                       <div class="col-md-12">
                           <asp:DropDownList runat="server" ID="examinationPregnancyStatus" CssClass="form-control input-sm" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1" />
                       </div>
                   </div>

                   <div class="col-md-12 form-group" id="divFemaleLMP">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">Female LMP</label>
                       </div>

                       <div class="col-md-12">
                           <div class='input-group date' id='FemaleLMP'>
                               <span class="input-group-addon">
                                   <span class="glyphicon glyphicon-calendar"></span>
                               </span>
                               <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="lmp" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                           </div>
                       </div>
                   </div>

                   <div class="col-md-12 form-group" id="divEDD">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">EDD</label>
                       </div>
                       <div class="col-md-12">
                           <div class='input-group date' id='EDCD'>
                               <span class="input-group-addon">
                                   <span class="glyphicon glyphicon-calendar"></span>
                               </span>
                               <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="ExpectedDateOfChildBirth" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                           </div>
                       </div>
                   </div>
               </div>
               
               <div class="col-md-4">
                   <div class="col-md-12 form-group">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">ANC/PNC Profile</label>
                       </div>

                       <div class="col-md-12 pull-left">
                           <label class="pull-left" style="padding-right:10px">
                               <input id="ancYes" runat="server" type="radio" name="ANC" value="yes" ClientIDMode="Static"  runat="server" />Yes
                           </label>
                           <label class="pull-left" style="padding-right:10px">
                               <input id="ancNo" runat="server" type="radio" name="ANC" value="no" ClientIDMode="Static" runat="server" />No
                           </label>
                       </div>
                   </div>

                   <div class="col-md-12 form-group" id="FP">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">On Family Planning</label>
                       </div>
                       <div class="col-md-12">
                           <asp:DropDownList runat="server" ID="onFP" ClientIDMode="Static" CssClass="form-control input-sm" />
                       </div>
                   </div>

                   <div class="col-md-12 form-group" id="divOnFP">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">FP Method</label>
                       </div>
                       <div class="col-md-12">
                           <asp:DropDownList ID="fpMethod" runat="server" ClientIDMode="Static" CssClass="form-control input-sm select" multiple tabindex="3" Width="300" placeholder="FP Method"></asp:DropDownList>
                       </div>
                   </div>

                   <div class="col-md-12 form-group" id="divNoFP" >
                       <div class="col-md-12">
                           <label class="control-label  pull-left">Reason not on FP</label>
                       </div>
                       <div class="col-md-12">
                           <asp:DropDownList runat="server" ID="ddlNoFP" ClientIDMode="Static" CssClass="form-control input-sm" />
                       </div>
                   </div>

               </div>
               
               <div class="col-md-4">
                   <div class="col-md-12 form-group">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">CaCX Screeing</label>
                       </div>
                       <div class="col-md-12">
                           <asp:DropDownList runat="server" ID="cacxscreening" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-min="1" />
                       </div>
                   </div>

                   <div class="col-md-12 form-group">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">STI Screeing</label>
                       </div>
                       <div class="col-md-12">
                           <asp:DropDownList runat="server" ID="stiScreening" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true" data-parsley-min="1" />
                       </div>
                   </div>

                   <div class="col-md-12 form-group">
                       <div class="col-md-12">
                           <label class="control-label  pull-left">STI Partner Notification</label>
                       </div>
                       <div class="col-md-12">
                           <asp:DropDownList runat="server" ID="stiPartnerNotification" CssClass="form-control input-sm" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1"/>
                       </div>
                   </div>
               </div>

           </div><!-- .col-md-12 -->
       </div>
       
       <!-- Modal -->
       <div id="myModal" class="modal fade" role="dialog">
           <div class="modal-dialog">
               <!-- Modal content-->
               <div class="modal-content">
                   <div class="modal-header">
                       <button type="button" class="close" data-dismiss="modal">&times;</button>
                       <h2 class="modal-title">Pregnancy Outcome</h2>
                   </div>
                   <div class="modal-body">
                       <div class="row">
                           <div class="col-md-1">&nbsp;</div>
                           <div class="col-md-3">
                               <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblPregnancyStatus">N/A</asp:Label></div>
                           </div>
                           <div class="col-md-4">
                               <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblLMP">N/A</asp:Label></div>
                           </div>
                           <div class="col-md-4">
                               <div class="col-md-12"><asp:Label runat="server" CssClass="control-label pull-left" ID="lblEDD">N/A</asp:Label></div>
                           </div>
                       </div>
                       <div class="row">&nbsp;</div>
                       <div class="row">
                           <div class="col-md-12 form-group">
                                <div class="col-md-12 form-group">
                                    <div class="col-md-4">
                                        <label class="control-label  pull-left">Pregnancy Outcome</label>
                                    </div>

                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="PregnancyOutCome" CssClass="form-control input-sm" ClientIDMode="Static" />
                                    </div>
                                </div>
                               
                               <div class="col-md-12 form-group">
                                   <div class="col-md-4">
                                       <label class="control-label  pull-left">Date of Outcome</label>
                                   </div>
                                   <div class="col-md-8">
                                       <div class='input-group date' id='OutcomeDate'>
                                           <span class="input-group-addon">
                                               <span class="glyphicon glyphicon-calendar"></span>
                                           </span>
                                           <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="outcomeDatetime" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                       </div>
                                   </div>
                               </div>
                            </div>
                       </div>
                   </div>
                   <div class="modal-footer">
                       <button type="button" id="btnSaveOutcome" class="btn btn-success" OnClientClick="return false;">Save</button>
                       <button type="button" id="btnModalClose" class="btn btn-default" data-dismiss="modal">Close</button>
                   </div>
               </div>

           </div>
       </div>
       
       <div class="col-md-12"><hr /></div>
       
       <div class="col-md-12" id="btnFemaleVitals">
           <div class="col-md-6">
               <div class="col-md-4">
                   <asp:LinkButton runat="server" ID="btnOutcome" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;" data-toggle="modal" data-target="#myModal"> Pregnancy Outcome </asp:LinkButton>
               </div>
               <div class="col-md-4"></div>
               <div class="col-md-4"></div>

           </div>
           
           <div class="col-md-6">
               <div class="col-md-4 col-xs-12 col-sm-12">
                     <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save History </asp:LinkButton>
               </div>

               <div class="col-md-4 col-xs-12 col-sm-12">
                   <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset History </asp:LinkButton>
               </div>

               <div class="col-md-4 col-xs-12 col-sm-12">
                        <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close History </asp:LinkButton>
               </div>
          </div>

     </div>
                         
    </div><!-- .col-md-12-->
        
</div><!-- .col-md-12 col-xs-12 col-sm-12-->
 <script type="text/javascript">
     $(document).ready(function () {
         var lmpDate;
         var eddDate;
         var pgStatus;
         var fpId = 0;
         var notOnFpId = 0;
         var dateOfOutcome;
         var ancProfileDate;
         var ancprofile = 0;
         var patientId ="<%=PatientId%>";
         var patientMasterVisitId = "<%=PatientMasterVisitId%>";

         var patientGender = "<%=PatientGender%>";
         var patientAge = "<%=PatientAge%>";
         var today = new Date();
         var visitDate;

         $("#divNoFP").hide("fast");

         $("#divEDD").addClass("noneevents");

         $('#FemaleLMP').datetimepicker({
             format: 'DD-MMM-YYYY',
             allowInputToggle: true,
             useCurrent: false
         });

         $('#FemaleVisitDate').datetimepicker({
             format: 'DD-MMM-YYYY',
             allowInputToggle: true,
             useCurrent: false
         });

         $('#EDCD').datetimepicker({
             format: 'DD-MMM-YYYY',
             allowInputToggle: true,
             useCurrent: false
         });

         $('#OutcomeDate').datetimepicker({
             format: 'DD-MMM-YYYY',
             allowInputToggle: true,
             useCurrent: false
         });

         $("#FemaleLMP").on("dp.change", function (e) {
             var lmp = $("#lmp").val();
             lmp = moment(lmp, 'DD-MMM-YYYYY').toDate();
             var futuredate = moment(lmp).isAfter(today);
             if (futuredate) {
                 $("#lmp").val('');
                 toastr.error("Future dates not allowed!");
                 return false;
             }
             lmpDate = moment(lmp).format('DD-MMM-YYYY');
             $("#ExpectedDateOfChildBirth").val(moment(moment(lmp).add(280, 'days')).format('DD-MMM-YYYY'));
         });

         $('#FemaleVisitDate').on('dp.change', function (e) {

             var vDate = moment($("#femaleDateOfVisit").val(), 'DD-MMM-YYYYY').toDate();
             var futuredate = moment(vDate).isAfter(today);
             if (futuredate) {
                 $("#<%=femaleDateOfVisit.ClientID%>").val('');
                 toastr.error("Future dates not allowed!");
                 return false;
             } else {
                 visitDate = moment($("#femaleDateOfVisit").val(), 'DD-MMM-YYYYY').toDate();
                 visitDate = moment(visitDate).format('DD-MMM-YYYY');
             }

         });


         $("#<%=ancYes.ClientID%>").on('click', function () {
             ancprofile = true;
         });

         $("#<%=ancNo.ClientID%>").on('click', function () {
             ancprofile = false;
         });

         $("#<%=onFP.ClientID%>").on('change', function () {
             var fp = $(this).find(":selected").text();

             if (fp === 'No Family Planning(NOFP)') {
                 $("#divOnFP").hide("fast", function () { $("#divNoFP").show("fast"); });
             } else {
                 $("#divOnFP").show("fast", function () { $("#divNoFP").hide("fast"); });
             }

         });


         /* -- Ajax Calls -- */
         // var lmpDate = $('#FemaleLMP').datepicker('getDate');

         $("#<%=examinationPregnancyStatus.ClientID%>").on("change", function () {
             pgStatus = $("#<%=examinationPregnancyStatus.ClientID%>").find(":selected").val();
             if ($(this).find(":selected").text() === 'Pregnant(PG)') {
                 $("#ancYes").prop("checked", true);
                 //$("#ancNo").prop("checked", false);
                 $("#divEDD").show("fast");
                 //$("#divFemaleLMP").show("fast");
                 $("#divOnFP").hide("fast", function () { $("#FP").hide("fast"); });

                 $('#FemaleVitals').parsley().destroy();

                 //set required attribute on input to false
                 $('#lmp').attr('data-parsley-required', 'true');
                 $('#ExpectedDateOfChildBirth').attr('data-parsley-required', 'true');
                 //reinitialize parsley
                 $('#FemaleVitals').parsley();

             } else {
                 $("#ancNo").prop("checked", true);
                 $("#divOnFP").show("fast", function () { $("#FP").show("fast"); });
                 //$("#divFemaleLMP").hide("fast");
                 $("#lmp_Female").val("");
                 $("#divEDD").hide("fast");
                 $("#ExpectedDateOfChildBirth").val("");

                 if (patientAge >= 45) {
                     //destroy parsley
                     $('#FemaleVitals').parsley().destroy();

                     //set required attribute on input to false
                     $('#lmp').attr('data-parsley-required', 'false');
                     $('#ExpectedDateOfChildBirth').attr('data-parsley-required', 'false');
                     //reinitialize parsley
                     $('#FemaleVitals').parsley();

                     //$("#lmp").prop("disabled", true);
                     //$("#FemaleLMP").prop("disabled", true);
                     //$("#ExpectedDateOfChildBirth").prop("disabled", true);
                     //$("#EDCD").prop("disabled", true);
                 }

             }
         });


         fpId = $("#<%=onFP.ClientID%>").find(":selected").val();
         var fpName = $("#<%=onFP.ClientID%>").find(":selected").text();
         notOnFpId = $("#<%=ddlNoFP.ClientID%>").find(":selected").val();
         <%--var fpMethod=[];
            $("#<%=fpMethod.ClientID%>:selected").each(function (i, selected) {
                fpMethod[i] = $(selected).val();
            });--%>

         doesPregnancyExists();

         function addPregnancy() {

             var eddDate = $("#ExpectedDateOfChildBirth").val();
             if (dateOfOutcome == null) { dateOfOutcome = '15-Jun-1900'; }

             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientPregnancy",
                 data: "{'patientId':'" + patientId + "','patientMasterVisitId':'" + patientId + "','LMP':'" + lmpDate + "','EDD':'" + eddDate + "','gravidae':'null','parity':'null','outcome':'0','dateOfOutcome':'" + dateOfOutcome + "','userId':'0'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d);
                 },
                 error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }


         function addPregnancyIndicator() {

             var eddDate = $("#ExpectedDateOfChildBirth").val();

             if (typeof lmpDate == "undefined") {
                 lmpDate = "";
             }
             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientPregnancyIndicator",
                 data: "{'patientId':'" + patientId + "','patientMasterVisitId':'" + patientMasterVisitId + "','visitDate':'" + visitDate + "','lmp':'" + lmpDate + "','edd':'" + eddDate + "','pregnancyStatusId':'" + pgStatus + "','ancProfile':'" + ancprofile + "','ancProfileDate':'" + lmpDate + "','userId':'0'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d);
                 },
                 error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }


         function addFamilyPlanning() {

             fpId = $("#<%=onFP.ClientID%>").find(":selected").val();
             notOnFpId = $("#<%=ddlNoFP.ClientID%>").find(":selected").val();

             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientFamilyPlanning",
                 data: "{'patientId':'" + patientId + "','patientMasterVisitId':'" + patientMasterVisitId + "','visitDate':'" + visitDate + "','FamilyPlanningStatusId':'" + fpId + "','ReasonNoOnFp':'" + notOnFpId + "','userId':'0'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d);
                 },
                 error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }


         function addFamilyPlanningMethod() {
             var fpMethod = $("#fpMethod").val();
             if (fpMethod != null) {
                 var value = JSON.stringify(fpMethod);
                 $.ajax({
                     type: "POST",
                     url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientFamilyPlanningMethod",
                     data: "{'patientId':'" + patientId + "','PatientFPId':'" + value + "','userId':'0'}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (response) {
                         alert(response.d);
                         toastr.success(response.d);
                     },
                     error: function (xhr, errorType, exception) {
                         var jsonError = jQuery.parseJSON(xhr.responseText);
                         toastr.error("" + xhr.status + "" + jsonError.Message);
                     }
                 });
             }        
         }


         //screening

         /*-- CaCX Screening --*/
         function addPatientScreeningcacx() {

             var cacxId = $("#<%=cacxscreening.ClientID%>").find(":selected").val();

             var screeningTypeId = 44;
             var screeningDone = true;
             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientScreening",
                 data: "{'patientId':'" + patientId + "','patientMasterVisitid':'" + patientMasterVisitId + "','visitDate':'" + visitDate + "','screeningTypeId':'" + screeningTypeId + "', 'screeningDone':" + screeningDone + ", 'screeningDate':'15-Jun-1900', 'screeningCategoryId':'0', 'screeningValueId':'" + cacxId + "','comment':'null','userId':'0'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d);
                 },
                 error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }

         function addPatientScreeningSti() {

             var stiId = $("#<%=stiScreening.ClientID%>").find(":selected").val();
             var screeningTypeId = 45;
             var screeningDone = true;
             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientScreening",
                 data: "{'patientId':'" + patientId + "','patientMasterVisitid':'" + patientMasterVisitId + "','visitDate':'" + visitDate + "','screeningTypeId':'" + screeningTypeId + "', 'screeningDone':" + screeningDone + ", 'screeningDate':'15-Jun-1900', 'screeningCategoryId':'0', 'screeningValueId':'" + stiId + "','comment':'null','userId':'0'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d);
                 },
                 error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }

         function addPatientScreeningStiNotification() {

             var stiNotificationId = $("#<%=stiPartnerNotification.ClientID%>").find(":selected").val();
             var screeningTypeId = 87;
             var screeningDone = true;
             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientScreening",
                 data: "{'patientId':'" + patientId + "','patientMasterVisitid':'" + patientMasterVisitId + "','visitDate':'" + visitDate + "','screeningTypeId':'" + screeningTypeId + "', 'screeningDone':" + screeningDone + ", 'screeningDate':'15-Jun-1900', 'screeningCategoryId':'0', 'screeningValueId':'" + stiNotificationId + "','comment':'null','userId':'0'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d);
                 },
                 error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }

         function doesPregnancyExists() {
             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/PregnancyExists",
                 data: "{'patientId':'" + patientId + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d > 0) {
                         $('#btnOutcome').show('slow');
                     } else {
                         $('#btnOutcome').hide('slow');
                     }

                     //console.log(response.d);
                 },error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         }

         $("#btnSave").click(function () {
             $('#FemaleVitals').parsley().destroy();
             $('#FemaleVitals').parsley({
                 excluded:
                     "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
             });

             if ($('#FemaleVitals').parsley().validate()) {
                 var fpMethod = $("#fpMethod").val();
                 var fName = $("#<%=examinationPregnancyStatus.ClientID%>").find(":selected").text();
                 var fpOption = $("#<%=onFP.ClientID%>").find(":selected").text();

                 if (fpOption == "Family Planning(FP)" && fpMethod == null) {
                     toastr.error("Please select a family planning method", "Family Planning");
                     return false;
                 }

                 $.ajax({
                     type: "POST",
                     url: "../WebService/FemaleVitalsWebservice.asmx/PregnancyExists",
                     data: "{'patientId':'" + patientId + "'}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (response) {

                         /* check if patient already has pregagncy without outcome.*/
                         if (response.d > 0) {
                             toastr.info("Patient has a pregnant entry with no Outcome!");
                             addPregnancyIndicator();
                         } else {
                             //insert the preganacy indicator and check if preganant insert into pregnancy table

                             if (fName === 'Pregnant(PG)') {
                                 $.when(addPregnancyIndicator()).then(function() {
                                     setTimeout(function () {
                                             addPregnancy();
                                         },
                                         2000);
                                 });
                             } else {
                                 setTimeout(function() { addPregnancyIndicator(); }, 2000);
                             }

                             /* save family planning */

                             if (fName !== 'Pregnant(PG)') {

                                 if (fpName === "No Family Planning(NOFP)") {
                                     addFamilyPlanning();
                                 } else {
                                     setTimeout(function() {
                                             $.when(addFamilyPlanning()).then(function() {
                                                 setTimeout(function() {
                                                         addFamilyPlanningMethod();
                                                     },
                                                     2000);
                                             });
                                         },
                                         2000);
                                 }
                             }

                             setTimeout(function() {
                                     /* patient screening*/
                                     $.when(addPatientScreeningcacx()).then(function() {
                                         setTimeout(function() {
                                                 addPatientScreeningSti();
                                             },
                                             2000);
                                     });
                                     $.when(addPatientScreeningStiNotification()).then(function() {
                                         $("#FemaleVitals").hide("fast");
                                         //location.reload();
                                     });
                                 },
                                 2000);
                         }        
                     },
                     error: function (xhr, errorType, exception) {
                         var jsonError = jQuery.parseJSON(xhr.responseText);
                         toastr.error("" + xhr.status + "" + jsonError.Message);
                     }
                 });

             } else {
                 stepError = $('.parsley-error').length === 0;
                 totalError += stepError;
                 evt.preventDefault();
             }
         });/* -- end button */

         $("#fpMethod").select2({
             placeholder: "Select FP Method",
             allowClear: true
         });

         $("#btnSaveOutcome").click(function() {
             var PregnancyOutcome = $("#PregnancyOutCome").val();
             var outcomeDatetime = $("#outcomeDatetime").val();

             if (PregnancyOutcome == 0) {
                 toastr.error("Please select one pregnancy option", "Pregnancy Outcome");
                 return false;
             }


             $.ajax({
                 type: "POST",
                 url: "../WebService/FemaleVitalsWebservice.asmx/AddPregnancyOutcome",
                 data: "{'outcome':'" + PregnancyOutcome + "','outcomeDate':'" + outcomeDatetime + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     toastr.success(response.d, "Pregnancy Outcome");
                     setTimeout(function() { location.reload(); return false; }, 3000);
                 }, error: function (xhr, errorType, exception) {
                     var jsonError = jQuery.parseJSON(xhr.responseText);
                     toastr.error("" + xhr.status + "" + jsonError.Message);
                 }
             });
         });

     });
 </script>