<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFollowupEducation.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucFollowupEducation" %>
<div class="box box-default box-solid" id="divFollowupEducation" data-parsley-validate="true">
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
        if ($("#divFollowupEducation").parsley().validate()) {
            console.log('valid');

            var comment = $("#<%=txtComments.ClientID%>").val();
            var counsellingType = $("#<%=ddlCounsellingType.ClientID%>").val();
            var counsellingTypeString = $("#<%=ddlCounsellingType.ClientID%>").find(":selected").text();
            var counsellingTopic = $("#<%=ddlCounsellingTopic.ClientID%>").val();
            var counsellingTopicString = $("#<%=ddlCounsellingTopic.ClientID%>").find(":selected").text();
            var eduDate = $('#HIVEDUDate').datepicker('getDate');
            var patientId = "<%=PatientId%>";

            $.ajax({
                type: "POST",
                url: "../WebService/HIVEducationService.asmx/addHIVEDucation",
                data: "{'patientId':'" + patientId + "','visitdate':'" + moment(eduDate).format('DD-MMM-YYYY') + "','councellingTypeId':'" + counsellingType + "','councellingType':'" + counsellingTypeString + "','councellingTopicId':'" + counsellingTopic + "', 'councellingTopic': '" + counsellingTopicString + "', 'comments':'" + comment + "', 'other':'null'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    window.location.href = '<%=ResolveClientUrl( "~/CCC/PatientHome.aspx")%>';
                    toastr.success(response.d);
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                    return false;
                }
            });
        } else {
            return false;
        }
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
                  $("#<%=ddlCounsellingTopic.ClientID%>").find('option').remove().end();
                  for (var i = 0; i < serverData.length; i++) {
                     $("#<%=ddlCounsellingTopic.ClientID%>").append('<option value="' + serverData[i][0] + '">' + serverData[i][1] + '</option>');
                  }
               }
           });
    }
</script>