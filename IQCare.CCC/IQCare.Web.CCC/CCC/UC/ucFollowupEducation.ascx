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
                                                     <div class='input-group date' id='HIVEDUDate'>
                                                        <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                        <asp:TextBox runat="server"  CssClass="form-control input-sm" ID="HIVEducationDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" required ="True" data-parsley-min-message="Input the appointment date"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                                  
                                            </div>  


				       
                     <div class="col-md-6 form-group">  
                                                <div class="col-md-6"><label class="control-label pull-left">Comments</label></div>
                                                <div class="col-md-6 pull-right">
                                                   
                                                    <asp:TextBox class="form-control input-sm" runat="server" id="txtComments" ClientIDMode="Static" TextMode="MultiLine" required="required" />
                                                </div>                    
                                            </div>  

				        
               </div>
                 <button type="button" id="submitdata" class="btn btn-primary btn-next" data-last="Complete"/>
			</div>
		</div>
	</div>
</div>
</div>
<script>
    $("#submitdata").click(function () {
       
       // var visitDate = <%=HIVEducationDate.Text%>;
        var comment = document.getElementById('<%= txtComments.ClientID %>').text
         alert(comment);
       
        
    });
       
   
    $("#HIVEDUDate").datetimepicker({
            defaultDate: $("#<%=HIVEducationDate.ClientID%>").val(),
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false
        });

     function selectCounsellingTopic(CounsellingType)
     {
        
           var valSelected = $("#<%=ddlCounsellingType.ClientID%>").find(":selected").text();

          //  alert(valSelected);
           
          valSelected = valSelected.replace(/\s/g, '');

           $.ajax({
               url: "../WebService/HIVEducationService.asmx/GetCounsellingTopics",
               type: "POST",
               dataType: "json",
               data: "{counsellingtopics:'"+ valSelected +"'}",
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

   
   <%-- $("#datastep1").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 2) {
            var error = 0;
          
                var categoryId = $(this).attr('id');
                alert(categoryId);
                var patientId = '7832';
                var visitDate = <%=HIVEducationDate.Text%>;
                var CouncellingTypeId = '';
                var CouncellingType = '';
                var CouncellingTopicId = '';
                var CouncellingTopic= '';
                var  CouncellingTopicOther = '';
                var clinicalNotes = $(this).val();
                var serviceAreaId = 203;
                var comments = <%=txtComments.Text%>;
                var other = "other";
                $.ajax({
                    type: "POST",
                    url: "../WebService/PatientClinicalNotesService.asmx/addPatientClinicalNotes",
                    data: "{'patientId': '" + patientId + "','visitDate': '" + visitDate + "','CouncellingTypeId':'" + serviceAreaId + "','CouncellingType':'" + categoryId + "','CouncellingTopicId':'" + clinicalNotes + "','CouncellingTopic':'" + userId + "','Comments':'" + userId + "','CouncellingTopicOther':'" + userId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        error = 0;
                    },
                    error: function (response) {
                        error = 1;
                    }
                });
            
            if (error == 0) {
                toastr.success("HIV Followup Education saved successfully");
            }
        }
    });--%>
</script>