<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ServiceEnrollment.aspx.cs" Inherits="IQCare.Web.CCC.Enrollment.ServiceEnrollment" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

      <%--  <uc:PatientDetails ID="PatientSummary" runat="server" />--%>
   
    
    <div class="col-md-12" id="enrollmentTab" data-parsley-validate="true" data-show-errors="true">
        <div class="col-md-12">
            <label class="control-lable pull-left"> Patient Enrollment </label>
            <asp:HiddenField ID="PatientType" runat="server" ClientIDMode="Static" />
        </div>
        

        <div class="col-md-12">
            <div class="panel-group">
                <div class="panel panel-info">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="col-md-12"><label class="required control-label pull-left">Date Of Birth</label></div>

                                <div class="col-md-12 form-group">
                                    <div class='input-group date' id='PersonDOBdatepicker'>
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PersonDOB" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                    </div>
                                </div>
                            </div>
                        
                            <div class="col-md-5">
                                <div class="col-md-12"><label class="control-label pull-left">National Id/Passport No</label></div>
                            
                                <div class="col-md-12 form-group">
                                    <asp:HiddenField ID="IsCCCEnrolled" runat="server" ClientIDMode="Static" />
                                    <asp:TextBox runat="server" CssClass="form-control input-sm" ID="NationalId" ClientIDMode="Static" data-parsley-length="[7,8]" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="panel panel-info">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="col-md-12"><label class="required control-label pull-left">Enrollment Date </label></div>
                            
                                <div class="col-md-12 form-group">
                                    <div class='input-group date' id='DateOfEnrollmentdatepicker'>
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="DateOfEnrollment" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="col-xs-12"><label class="required control-label pull-left" for="entrypoint">Entry Point</label></div>
                            
                                <div class="col-md-12 form-group">
                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="entryPoint" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1" data-parsley-min-message="Please select Entry Point"/>
                                </div>
                            </div>
                            
                            <div class="col-md-5" id="OtherSpecificEntryPoint">
                                <div class="col-xs-12"><label class="control-label pull-left" for="otherentrypoint">Specify Other Entry Point</label></div>
                            
                                <div class="col-md-12 form-group">
                                    <asp:TextBox runat="server" CssClass="form-control input-sm" ID="SpecificEntryPoint" ClientIDMode="Static"/>
                                </div>
                            </div>
                        </div>
                        
                       <%-- <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="col-md-12"><label class="required pull-left control-label">Enrollment Identifier</label></div>
                                <div class="col-md-12 form-group">
                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="IdentifierTypeId" ClientIDMode="Static" data-parsley-required="true" data-parsley-min="1" data-parsley-min-message="Please select Identifier"/>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
                
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <asp:Panel ID="placeholder" runat="server"></asp:Panel>
                        </div>            
                    </div>
                </div>
            </div>
        </div>
        
    <div class="col-md-12">
        &nbsp;
    </div>

    <div class="col-md-12">
        <div class="col-md-3"></div>
        <div class="col-md-7">
                <div class="col-md-4">
                    <asp:LinkButton runat="server" ID="btnEnroll" CssClass="btn btn-info btn-lg fa fa-plus-circle" ClientIDMode="Static" OnClientClick="return false;"> Enroll and Continue </asp:LinkButton>
                </div>
            <div class="col-md-4">
                    <asp:LinkButton runat="server" ID="btnRese" CssClass="btn btn-warning btn-lg fa fa-refresh" ClientIDMode="Static" OnClientClick="return false;"> Enroll and Register New</asp:LinkButton>
                </div>
            <div class="col-md-4">
                    <asp:LinkButton runat="server" ID="btnClose" CssClass="btn btn-danger btn-lg fa fa-times" ClientIDMode="Static" OnClientClick="return false;"> Close Enrollemnt</asp:LinkButton>
                </div>
        </div>
        <div class="col-md-3"></div>
    </div>
             
    </div>
    
    <style type="text/css">
        .chosen-container-single {
            width: 100% !important;
        }
    </style>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#PersonDOBdatepicker').datetimepicker({
                format: 'DD-MMM-YYYY'
            });

            $('#DateOfEnrollmentdatepicker').datetimepicker({
                format: 'DD-MMM-YYYY'
            });

            $("#OtherSpecificEntryPoint").hide();
            $("#IsCCCEnrolled").val("");

            var code = "<%=Session["AppPosID"]%>";

            $("#entryPoint").change(function () {
                $(this).find(":selected").text();
                if ($(this).find(":selected").text() == 'Other') {
                    $("#OtherSpecificEntryPoint").show();
                } else {
                    $("#OtherSpecificEntryPoint").hide();
                }
            });

            
            var personDOB = '<%=Session["PersonDob"]%>';
            var nationalId = '<%=Session["NationalId"]%>';
            var patientType = '<%=Session["PatientType"]%>';
            //console.log(patientType);
            if (personDOB != null && personDOB !="") {
                $("#DateOfBirth").addClass("noneevents");
                personDOB = new Date(personDOB);
                $('#PersonDOB').val(moment(personDOB.toISOString()).format('DD-MMM-YYYY'));
            }

            if (nationalId != null && nationalId != "") {
                $("#NationalId").val(nationalId);
            }

            if (patientType != null && patientType != "") {
                $("#PatientType").val(patientType);
            }

            $.ajax({
                type: "POST",
                url: "../WebService/LookupService.asmx/GetLookUpItemByName",
                data: "{'itemName':'Entrypoint'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);
                    //console.log(itemList);
                    $("#<%=entryPoint.ClientID%>").find('option').remove().end();
                    $("#<%=entryPoint.ClientID%>").append('<option value="0">Select</option>');
                    $.each(itemList, function (index, itemList) {
                        $("#<%=entryPoint.ClientID%>").append('<option value="' + itemList.ItemId + '">' + itemList.ItemName + '</option>');
                    }); 
                },
                error: function (msg) {
                    alert(msg);
                }
            });

            $("#btnClose").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';
            });

            $("#btnRese").click(function (e) {
                if (!$('#enrollmentTab').parsley().validate()) {
                    return false;
                }

                var enrollmentDate = $('#DateOfEnrollment').val();
                var personDateOfBirth = $("#PersonDOB").val();

                var entryPointId = $("#entryPoint").val();

                var isEnrollmentDateBeforeDob = moment(moment(moment(enrollmentDate, 'DD-MMM-YYYYY').toDate()).format('DD-MMM-YYYY')).isBefore(moment(moment(personDateOfBirth, 'DD-MMM-YYYYY').toDate()).format('DD-MMM-YYYY'));

                if (isEnrollmentDateBeforeDob) {
                    toastr.error("Enrollment Date should not be before date of birth", "Patient Enrollment");
                    return false;
                }

                var nationalId = $("#NationalId").val();
                var patientType = $("#PatientType").val();
                var dobPrecision = '<%=Session["DobPrecision"]%>';

                if (nationalId == null || nationalId == '') {
                    nationalId = 99999999;
                }

                var fields = getDynamicFields();
                var prefix = null;
                var mflCode = code;
                var fieldName = null;
                var identifiers = {};
                $.each(fields, function (index, value) {
                    fieldName = $("#" + value.Code).val();
                    if (value.Prefix != null) {
                        prefix = $("#" + value.Prefix).val();
                        fieldName = prefix + "-" + fieldName;
                    }
                    identifiers[value.ID] = fieldName;
                });

                addPatientRegister(entryPointId, enrollmentDate, personDateOfBirth, nationalId, patientType, mflCode, dobPrecision, JSON.stringify(identifiers));
            });

            $("#btnEnroll").click(function (e) {
                if (!$('#enrollmentTab').parsley().validate()) {
                    return false;
                }

                var enrollmentDate = $('#DateOfEnrollment').val();
                var personDateOfBirth = $("#PersonDOB").val();

                var entryPointId = $("#entryPoint").val();

                var isEnrollmentDateBeforeDob = moment(moment(moment(enrollmentDate, 'DD-MMM-YYYYY').toDate()).format('DD-MMM-YYYY')).isBefore(moment(moment(personDateOfBirth, 'DD-MMM-YYYYY').toDate()).format('DD-MMM-YYYY'));

                if (isEnrollmentDateBeforeDob) {
                    toastr.error("Enrollment Date should not be before date of birth", "Patient Enrollment");
                    return false;
                }
                
                var nationalId = $("#NationalId").val();
                var patientType = $("#PatientType").val();
                var dobPrecision = '<%=Session["DobPrecision"]%>';

                if (nationalId == null || nationalId == '') {
                    nationalId = 99999999;
                }

                var fields = getDynamicFields();
                var prefix = null;
                var mflCode = code;
                var fieldName = null;
                var identifiers = {};
                $.each(fields, function (index, value) {
                    fieldName = $("#" + value.Code).val();
                    if (value.Prefix != null) {
                        prefix = $("#" + value.Prefix).val();
                        fieldName = prefix + "-" + fieldName;
                    }
                    identifiers[value.ID] = fieldName;
                });

                addPatient(entryPointId, enrollmentDate, personDateOfBirth, nationalId, patientType, mflCode, dobPrecision, JSON.stringify(identifiers));
            });

            function addPatientRegister(entryPointId, enrollmentDate, personDateOfBirth, nationalId, patientType, mflCode, dobPrecision, identifiers) {
                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/AddPatient",
                    data: "{'facilityId':'" + mflCode + "','entryPointId': '" + entryPointId + "','enrollmentDate':'" + enrollmentDate + "','personDateOfBirth':'" + personDateOfBirth + "', 'nationalId':'" + nationalId + "','patientType':'" + patientType + "','dobPrecision':'" + dobPrecision + "','identifiersList':'" + identifiers + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        var messageResponse = JSON.parse(response.d);

                        if (messageResponse.errorcode == 1) {
                            toastr.error(messageResponse.msg , "Patient Enrollment");
                            return false;
                        } else {
                            toastr.success(messageResponse.msg, "Patient Enrollment");
                            window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientRegistration.aspx")%>';
                        }
                    },
                    error: function (response) {
                        //generate('error', response.d);
                        toastr.error(response.d, "Patient Enrollment");
                    }
                });
            }

            function addPatient(entryPointId, enrollmentDate, personDateOfBirth, nationalId, patientType, mflCode, dobPrecision, identifiers) {

                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/AddPatient",
                    data: "{'facilityId':'" + mflCode + "','entryPointId': '" + entryPointId + "','enrollmentDate':'" + enrollmentDate + "','personDateOfBirth':'" + personDateOfBirth + "', 'nationalId':'" + nationalId + "','patientType':'" + patientType + "','dobPrecision':'" + dobPrecision + "','identifiersList':'" + identifiers + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        var messageResponse = JSON.parse(response.d);

                        if (messageResponse.errorcode == 1) {
                            toastr.error(messageResponse.msg , "Patient Enrollment");
                            return false;
                        } else {
                            toastr.success(messageResponse.msg, "Patient Enrollment");
                            window.location.href = '<%=ResolveClientUrl("~/CCC/Patient/PatientHome.aspx")%>';
                        }
                        
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                        return false;
                    }
                });
            }

            $('#ctl00_IQCareContentPlaceHolder_mfl_code').chosen();

            
            $.when(createDynamicElements()).then(function () {
                setTimeout(function() {
                    $.when(getFacilitiesList()).then(function() {
                        getPatientEnrollmentDetails();
                    });
                },3000);
            });

            function getDynamicFields() {
                var result = "";
                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/GetDynamicFields",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        var messageResponse = JSON.parse(response.d);
                        result = messageResponse;
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                        return false;
                    }
                });

                return result;
            }

            function createDynamicElements() {
                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/GetDynamicFields",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        var messageResponse = JSON.parse(response.d);
                        //console.log(messageResponse);
                        var table = "<table width='100%'>";
                        for (var i = 0; i < messageResponse.length; i++) {
                            //console.log(messageResponse[i]);
                            table += "<tr>";

                            table += "<td>";
                            table += "<label align='center'>Identifier :</label>";
                            table += "</td>";

                            table += "<td>";
                            table += "<select disabled id=" + messageResponse[i].ID + " class='form-control'><option value=" + messageResponse[i].ID + ">" + messageResponse[i].IdentifierName + "</option></select>";
                            table += "</td>";

                            if (messageResponse[i].Prefix != null) {
                                table += "<td>";
                                table += "<label align='center'>" + messageResponse[i].Prefix + " :</label>";
                                table += "</td>";


                                if (messageResponse[i].Required == true) {
                                    table += "<td>";
                                    if (messageResponse[i].Prefix == "mfl_code") {
                                        table += "<select id=" + messageResponse[i].Prefix + " class='form-control' data-parsley-required='true'></select>";
                                    } else {
                                        table += "<input type='text' id=" + messageResponse[i].Prefix + " class='form-control' data-parsley-required='true' data-parsley-type='digits' />";
                                    }                                 
                                    table += "</td>";
                                } else {
                                    table += "<td>";
                                    if (messageResponse[i].Prefix == "mfl_code") {
                                        table += "<select id=" + messageResponse[i].Prefix + " class='form-control'></select>";
                                    } else {
                                        table += "<input type='text' id=" + messageResponse[i].Prefix + " class='form-control' data-parsley-type='digits' />  ";
                                    }                                   
                                    table += "</td>";
                                }

                            }

                            table += "<td>";
                            table += "<label align='center'>" + messageResponse[i].Label + " :</label>";
                            table += "</td>";

                            if (messageResponse[i].DataType == "Numeric") {
                                table += "<td>";
                                if (messageResponse[i].Required == true) {
                                    table += "<input type='text' id=" + messageResponse[i].Code + " class='form-control' data-parsley-type='digits' data-parsley-required='true' data-parsley-length='[5, 5]' />";
                                } else {
                                    table += "<input type='text' id=" + messageResponse[i].Code + " class='form-control' data-parsley-type='digits' data-parsley-length='[5, 5]' />";
                                }
                                
                                table += "</td>";
                            } else if (messageResponse[i].DataType == "") {
                                
                            }


                            table += "</tr>";
                            table += "<tr><td>&nbsp;</td></tr>";
                        }
                        table += "</table>";

                        $("#ctl00_IQCareContentPlaceHolder_placeholder").append(table);
                    },
                    error: function (xhr, errorType, exception) {
                        //var jsonError = jQuery.parseJSON(xhr.responseText);
                        //toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                        //return false;
                    }
                });
            }

            function getFacilitiesList() {
                $.ajax({
                    type: "GET",
                    url: "../WebService/EnrollmentService.asmx/GetFacilitiesList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        var messageResponse = JSON.parse(response.d);

                        $('#mfl_code').append($('<option></option>'));

                        $.each(messageResponse, function (index, value) {
                            $('#mfl_code').append($('<option>', {
                                value: value.MFLCode,
                                text: value.Name
                            }));
                            //console.log(index + ": " + value);
                        });

                        $("#mfl_code").select2({
                            placeholder: "Select Facility"
                        });
                        $("#mfl_code").val(code).trigger("change");
                        //console.log(code);
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                        return false;
                    }
                });
            }

            function getPatientEnrollmentDetails() {
                $.ajax({
                    type: "POST",
                    url: "../WebService/EnrollmentService.asmx/GetPatientEnrollmentDetails",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //generate('success', '<p>,</p>' + response.d);
                        var messageResponse = JSON.parse(response.d);

                        console.log(messageResponse);
                        if (messageResponse.DOB != null)
                            $("#PersonDOB").val(messageResponse.DOB);
                        if (messageResponse.NationalId)
                            $("#NationalId").val(messageResponse.NationalId);
                        if (messageResponse.EnrollmentDate != null)
                            $("#DateOfEnrollment").val(messageResponse.EnrollmentDate);
                        if (messageResponse.EntryPointIdUnknown == false) {
                            $("#entryPoint").val(messageResponse.EntryPointId);
                        }

                        var fields = getDynamicFields();
                        $.each(fields, function (index, value) {
                            $.each(messageResponse.IndentifiersList, function (key, val) {
                                if (val.Code == value.Code) {
                                    if (value.Prefix != null) {
                                        //console.log(value.Prefix);
                                        //console.log(val);
                                        $("#" + value.Prefix).val(val.PrefixType).trigger("change");
                                    }
                                    $("#" + value.Code).val(val.DataType);
                                }
                            });
                            
                            //console.log(value);
                        });
                        //console.log(fields);
                        //$("#IdentifierTypeId").val(messageResponse.IndentifierId);
                        //$("#mfl_code").val(messageResponse.Prefix);
                        //$("#CCCNumber").val(messageResponse.EnrollmentValue);
                    },
                    error: function (xhr, errorType, exception) {
                        var jsonError = jQuery.parseJSON(xhr.responseText);
                        toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                        return false;
                    }
                });
            }
        });
    </script>

</asp:Content>
