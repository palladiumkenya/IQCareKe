<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFemaleVitals.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucFemaleVitals" %>

<div class="col-md-12 col-xs-12 col-sm-12 bs-callout bs-callout-primary well well-sm">
    
     <div class="col-md-12" id="FemaleVitals">

          <div class="col-md-12">
               <h2 class=" control-label pull-left"> Reproductive History |</h2> <h6><label class="control-label text-primary pull-left text-muted">Routine Female ONLY observations </label></h6>
          </div>

          <div class="col-md-12">
               <hr />
          </div>

          <div class="col-md-12">
                                            <div class="col-md-4">
                                                 <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">Pregnancy Status</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList runat="server" ID="examinationPregnancyStatus" CssClass="form-control input-sm" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 form-group">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">Female LMP</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="datepicker fuelux" id="FemaleLMP">
                                                            <div class="input-group">
                                                                <input class="form-control input-sm" id="lmp" type="text" runat="server" ClientIDMode="Static" />
                                                                <div class="input-group-btn">
                                                                    <button id="btnFemaleLMP" type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
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
                                                                                    </span><span class="year">2014</span>
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

                                                <div class="col-md-12 form-group" id="divEDD">
                                                    <div class="col-md-12">
                                                        <label class="control-label  pull-left">EDD</label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="datepicker fuelux" id="EDCD">
                                                            <div class="input-group">
                                                                <input class="form-control input-sm" id="ExpectedDateOfChildBirth" type="text" runat="server" ClientIDMode="Static" />
                                                                <div class="input-group-btn">
                                                                    <button id="btnEDD" type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
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
                                                                                    </span><span class="year">2014</span>
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
                                                        <asp:ListBox runat="server" ID="fpMethod" ClientIDMode="Static" CssClass="form-control input-sm" SelectionMode="Multiple" />           
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
                                        </div>
                         
     </div><!-- .col-md-12-->
        
    <div class="col-md-12">
        <hr />
    </div>
   <div class="col-md-12" id="btnFemaleVitals">
             <div class="col-md-8"></div>
             <div class="col-md-4">
            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save </asp:LinkButton>
            </div>

            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset </asp:LinkButton>
            </div>

            <div class="col-md-4 col-xs-12 col-sm-12">
                <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close </asp:LinkButton>
            </div>
        </div>

       </div> <!-- .col-md-12 -->

</div><!-- .col-md-12 col-xs-12 col-sm-12-->
 <script type="text/javascript">
        $(document).ready(function(){

            var lmpDate;
            var eddDate;
            var pgStatus;
            var fpId = 0;
            var notOnFpId = 0;
            var dateOfOutcome;
            var ancProfileDate;
            var ancprofile=0;
            var patientId="<%=PatientId%>";
            var patientMasterVisitId = "<%=PatientMasterVisitId%>";

            var patientGender = "<%=PatientGender%>";
            var patientAge = "<%=PatientAge%>";

            //alert(patientAge);
            //alert(patientGender);
            $("#divNoFP").hide("fast");

            $("#FemaleLMP").datepicker({
                  date: null,
                  allowPastDates: true,
                  momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#EDCD").datepicker({
                  date: null,
                  allowPastDates: true,
                  momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $('#FemaleLMP').on('changed.fu.datepicker dateClicked.fu.datepicker',
                function(event, date) {

                    var lmp = $("#FemaleLMP").datepicker('getDate');
                    lmpDate =moment(lmp).format('DD-MMM-YYYY');
                    $("#EDCD").datepicker('setDate',moment(lmp).add(280,'days'));
                });


            $("#<%=ancYes.ClientID%>").on('click',
                function() {
                    ancprofile = "true";
                });

            $("#<%=ancNo.ClientID%>").on('click', function () {
                ancprofile = "false";
            });
            //$('#EDCD').on('changed.fu.datepicker dateClicked.fu.datepicker',
            //    function(event, date) {
            //        alert();
            //        var lmp = $("#EDCD").datepicker('getDate');
            //        eddDate =moment(lmp).format('DD-MMM-YYYY');
            //        $("#EDCD").datepicker('setDate',moment(lmp).add(280,'days'));
            //    });

            $("#<%=onFP.ClientID%>").on('change', function () {
                var fp = $(this).find(":selected").text();
               
                if (fp === 'No Family Planning(NOFP)') {$("#divNoFP").show("fast");}else { $("#divNoFP").hide("fast"); }
               
            });

            /* -- Ajax Calls -- */
           // var lmpDate = $('#FemaleLMP').datepicker('getDate');
           
            $("#<%=examinationPregnancyStatus.ClientID%>").on("change", function () {
                pgStatus = $("#<%=examinationPregnancyStatus.ClientID%>").find(":selected").val();
                if ($(this).find(":selected").text() === 'Pregnant(PG)') {
                    $("#divEDD").show("fast");
                    $("#divOnFP").hide("fast", function () { $("#FP").hide("fast");})
                } else {
                    $("#divOnFP").show("fast", function () { $("#FP").show("fast"); })
                     $("#divEDD").hide("fast");
                }
            });

            fpId = $("#<%=onFP.ClientID%>").find(":selected").val();
            var fpName = $("#<%=onFP.ClientID%>").find(":selected").text();
            notOnFpId=$("#<%=ddlNoFP.ClientID%>").find(":selected").val();
            var fpMethod=[];
            $("#<%=fpMethod.ClientID%>:selected").each(function (i, selected) {
                fpMethod[i] = $(selected).val();
            });

            
            //$("input:radio[name=ANC]").click(function() {
            //    var ancprofile = $(this).val();
            //});

            function AddPregnancy() {

                var eddDate = moment($("#EDCD").datepicker('getDate')).format("DD-MMM-YYYY");
                if (dateOfOutcome == null) { dateOfOutcome = '15-Jun-1900'; }

                $.ajax({
		            type: "POST",
		            url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientPregnancy",
		            data: "{'patientId':'" + patientId + "','patientMasterVisitId':'" + patientId + "','LMP':'" + lmpDate + "','EDD':'" + eddDate + "','gravidae':'null','parity':'null','outcome':'0','dateOfOutcome':'"+dateOfOutcome+"','userId':'0'}",
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function(response) {
		                toastr.success(response.d);
		            },
		            error: function(xhr, errorType, exception) {
		                var jsonError = jQuery.parseJSON(xhr.responseText);
		                toastr.error("" + xhr.status + "" + jsonError.Message);
		            }
               });
            }

            function AddPregnancyIndicator()
            {
                 var eddDate = moment($("#EDCD").datepicker('getDate')).format("DD-MMM-YYYY");
               $.ajax({
		            type: "POST",
		            url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientPregnancyIndicator",
		            data: "{'patientId':'" + patientId + "','patientMasterVisitId':'" + patientId + "','lmp':'" + lmpDate + "','edd':'" + eddDate + "','pregnancyStatusId':'"+pgStatus+"','ancProfile':'"+ancprofile+"','ancProfileDate':'"+lmpDate+"','userId':'0'}",
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function(response) {
		                    toastr.success(response.d);   
		            },
		            error: function(xhr, errorType, exception) {
		                var jsonError = jQuery.parseJSON(xhr.responseText);
		                toastr.error("" + xhr.status + "" + jsonError.Message);
		            }
               });
            }

            function AddFamilyPlanning() {

                fpId = $("#<%=onFP.ClientID%>").find(":selected").val();
                notOnFpId=$("#<%=ddlNoFP.ClientID%>").find(":selected").val();

                $.ajax({
		            type: "POST",
		            url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientFamilyPlanning",
		            data: "{'patientId':'" + patientId + "','patientMasterVisitId':'" + patientId + "','FamilyPlanningStatusId':'" + fpId + "','ReasonNoOnFp':'" + notOnFpId + "','userId':'0'}",
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function(response) {
		                    toastr.success(response.d);   
		            },
		            error: function(xhr, errorType, exception) {
		                var jsonError = jQuery.parseJSON(xhr.responseText);
		                toastr.error("" + xhr.status + "" + jsonError.Message);
		            }
               });
            }

            function AddFamilyPlanningMethod() {
                $.each(fpMethod, function (index, value) {
                      $.ajax({
		                type: "POST",
		                url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientFamilyPlanningMethod",
		                data: "{'patientId':'" + patientId + "','PatientFPId':'" + value + "','userId':'0'}",
		                contentType: "application/json; charset=utf-8",
		                dataType: "json",
		                success: function(response) {
		                        toastr.success(response.d);   
		                },
		                error: function(xhr, errorType, exception) {
		                    var jsonError = jQuery.parseJSON(xhr.responseText);
		                    toastr.error("" + xhr.status + "" + jsonError.Message);
		                }
                   });
                });
            }

            //screening

            /*-- CaCX Screening --*/
           function addPatientScreeningcacx() {

               var cacxId= $("#<%=cacxscreening.ClientID%>").val();
               var screeningTypeId = 44;
               var screeningDone = 1;
                $.ajax({
		            type: "POST",
		            url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientScreening",
		            data: "{'patientId':'" + patientId + "','patientMasterVisitid':'" + patientId + "','screeningTypeId':'" +screeningTypeId  + "', 'screeningDone':'"+screeningDone+"', 'screeningDate':'15-Jun-1900', 'screeningCategoryId':'0', 'screeningValueId':'"+cacxId+"','comment':'','userId':'0'}",
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function(response) {
		                    toastr.success(response.d);   
		            },
		            error: function(xhr, errorType, exception) {
		                var jsonError = jQuery.parseJSON(xhr.responseText);
		                toastr.error("" + xhr.status + "" + jsonError.Message);
		            }
               });
           }

           function addPatientScreeningSTI() {

               var stiId= $("#<%=stiScreening.ClientID%>").val();
               var screeningTypeId = 45;
               var screeningDone = 1;
                $.ajax({
		            type: "POST",
		            url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientScreening",
		            data: "{'patientId':'" + patientId + "','patientMasterVisitid':'" + patientId + "','screeningTypeId':'" +screeningTypeId  + "', 'screeningDone':'"+screeningDone+"', 'screeningDate':'15-Jun-1900', 'screeningCategoryId':'0', 'screeningValueId':'"+stiId+"','comment':'','userId':'0'}",
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function(response) {
		                    toastr.success(response.d);   
		            },
		            error: function(xhr, errorType, exception) {
		                var jsonError = jQuery.parseJSON(xhr.responseText);
		                toastr.error("" + xhr.status + "" + jsonError.Message);
		            }
               });
           }

            function addPatientScreeningStiNotification() {

               var stiNotificationId= $("#<%=stiPartnerNotification.ClientID%>").val();
               var screeningTypeId = 87;
               var screeningDone = 1;
                $.ajax({
		            type: "POST",
		            url: "../WebService/FemaleVitalsWebservice.asmx/AddPatientScreening",
		            data: "{'patientId':'" + patientId + "','patientMasterVisitid':'" + patientId + "','screeningTypeId':'" +screeningTypeId  + "', 'screeningDone':'"+screeningDone+"', 'screeningDate':'15-Jun-1900', 'screeningCategoryId':'0', 'screeningValueId':'"+stiNotificationId+"','comment':'home','userId':'0'}",
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function(response) {
		                    toastr.success(response.d);   
		            },
		            error: function(xhr, errorType, exception) {
		                var jsonError = jQuery.parseJSON(xhr.responseText);
		                toastr.error("" + xhr.status + "" + jsonError.Message);
		            }
               });
            }


            $("#btnSave").click(function() {

                if ($('#FemaleVitals').parsley().validate()) {

                    var fName = $("#<%=examinationPregnancyStatus.ClientID%>").find(":selected").text();
                    var fpOption = $("#<%=onFP.ClientID%>").find(":selected").text();
                    $.ajax({
                        type: "POST",
                        url: "../WebService/FemaleVitalsWebservice.asmx/PregnancyExists",
                        data: "{'patientId':'" + patientId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response) {

                            /* check if patient already has pregagncy without outcome.*/
                            if (response.d > 0) {
                                AddPregnancyIndicator();
                            } else {
                                //insert the preganacy indicator and check if preganant insert into pregnancy table
                              
                                if (fName ==='Pregnant(PG)') {
                                    $.when(AddPregnancyIndicator()).then(AddPregnancy());
                                } else {
                                    AddPregnancyIndicator();
                                }

                            }

                        },
                        error: function(xhr, errorType, exception) {
                            var jsonError = jQuery.parseJSON(xhr.responseText);
                            toastr.error("" + xhr.status + "" + jsonError.Message);
                        }
                    });

                    /* save family planning */

                    if (fName !== 'Pregnant(PG)') {

                        if (fpName === "No Family Planning(NOFP)") {
                            AddFamilyPlanning();
                        } else {

                            $.when(AddFamilyPlanning()).then(AddFamilyPlanningMethod());
                        }
                    }

                    /* patient screening*/
                    $.when(addPatientScreeningcacx()).then(addPatientScreeningSTI());
                    $.when(addPatientScreeningStiNotification()).then(function()
                    {
                        $("#FemaleVitals").hide("fast");
                    });

        }else{
                        stepError = $('.parsley-error').length === 0;
                                totalError += stepError;
                                evt.preventDefault();
             }
        });/* -- end button */
 

        });
 </script>