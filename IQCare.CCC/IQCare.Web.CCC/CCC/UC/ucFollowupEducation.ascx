<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFollowupEducation.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucFollowupEducation" %>
<div class="box box-default box-solid" id="divFollowupEducation">
 <div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				 <div class="col-md-12">
                         <div class="col-md-6 form-group">  
                                                <div class="col-md-6"><label class="control-label pull-left">Counselling Type:</label></div>
                                                <div class="col-md-6 pull-right">
                                                    <asp:DropDownList runat="server" CssClass="form-control input-sm " id="ddlCounsellingType" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" onChange="selectCounsellingTopic(this.Name)" />
                                                </div>                    
                                            </div>  

				       
                     <div class="col-md-6 form-group">  
                                                <div class="col-md-6"><label class="control-label pull-left">Counselling Topic:</label></div>
                                                <div class="col-md-6 pull-right">
                                                    <asp:DropDownList runat="server" CssClass="form-control input-sm " id="ddlCounsellingTopic" ClientIDMode="Static" data-parsley-min="1" data-parsley-min-message="Value Required" />
                                                </div>                    
                                            </div>  

				        
               </div>
                 <div class="col-md-12">
                         <div class="col-md-6 form-group">  
                                                <div class="col-md-6"><label class="control-label pull-left">Date</label></div>
                                                <div class="col-md-6 pull-right">
                                                 
                                                        <div class="datepicker fuelux form-group pull-left" id="PrescriptionDate">
                                                            <div class="input-group pull-left">
                                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm pull-left" ID="txtPrescriptionDate" onBlur="checkEnrolmentPrescriptionDates();ValidatePrescriptionDate();DateFormat(this,this.value,event,false,'3');" data-parsley-required="true" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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

				       
                     <div class="col-md-6 form-group">  
                                                <div class="col-md-6"><label class="control-label pull-left">Comments</label></div>
                                                <div class="col-md-6 pull-right">
                                                   
                                                    <asp:TextBox class="form-control input-sm" runat="server" id="txtComments" ClientIDMode="Static" TextMode="MultiLine"  />
                                                </div>                    
                                            </div>  

				        
               </div>
			</div>
		</div>
	</div>
</div>
</div>
<script>
     function selectCounsellingTopic(CounsellingType)
     {
        
           var valSelected = $("#<%=ddlCounsellingType.ClientID%>").find(":selected").text();

            alert(valSelected);
           
          valSelected = valSelected.replace(/\s/g, '');

           $.ajax({
               url: "../WebService/HIVEducationService.asmx/GetCounsellingTopics",
               type: "POST",
               dataType: "json",
               data: "{counsellingtopics:'ProgressionRX'}",
               contentType: "application/json; charset=utf-8",
               success: function (data) {
                   var serverData = data.d;
                   alert(serverData);
                  $("#<%=ddlCounsellingTopic.ClientID%>").find('option').remove().end();
			      $("#<%=ddlCounsellingTopic.ClientID%>").append('<option value="0">Select</option>');
                  for (var i = 0; i < serverData.length; i++) {
                     $("#<%=ddlCounsellingTopic.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
                  }
               }
           });
       }
</script>