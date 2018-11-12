<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGenderBasedViolenceAssessment.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucGenderBasedViolenceAssessment" %>
<style type="text/css">
    .table {
        margin: 0 0 15px 0;
        width: 100%;
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
        display: table;
    }
</style>
<div class="col-md-12" id="gbvAssessment" data-parsley-validate="true" data-show-errors="true">
    <div class="col-md-12">
        <h2 class="pull-left">GBV Assessment Form |</h2><h6><label class="control-label text-primary pull-left text-muted">Gender Based Violence Screening Tool </label></h6>
    </div>
    <div class="col-md-12">
        <hr />
    </div>
    <div class="col-md-12">
        <div class="alert alert-info">
            <strong>NOTE:</strong>
            <br />
            This form should be used at every visit to screen patients for Gender Based Violence for all patients accessing care at this facility
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-4">
            <div class="col-md-12">
                <label class="required control-label pull-left">Visit Date</label>
            </div>
            <div class='input-group date'>
                <span class="input-group-addon">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
                <input type="text" name="GbvVisitDate" id="GbvVisitDate" class="form-control input-sm" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" />
            </div>
        </div>
    </div>
    <div class="col-md-12"></div>
    <div class="col-md-12">
        <div class="col-md-12" id="positive-screening">
            <div class="alert alert-danger alert-dismissible fade in">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                Please provide this client with some immediate counselling support and <strong>REFER/ESCORT</strong> him/her to the <strong>Gender Based Violence recovery Centre</strong> (GBVRC)
            </div>
        </div>
        <div class="col-md-12">
            <table id="tblGbvScreeningQuestions" runat="server" class="table table-condensed table-bordered">
                <thead>
                    <tr>
                        <th>Screening question</th>
                        <th>Response</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="col-md-12">
            <div class="col-md-6 pull-right">
                <div class="col-md-4 col-xs-12 col-sm-12">
                    <asp:LinkButton runat="server" ID="btnSaveGBVAssessment" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save Entry</asp:LinkButton>
                </div>
                <div class="col-md-4 col-xs-12 col-sm-12">
                    <asp:LinkButton runat="server" ID="btnResetGbvAssessment" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Entries  </asp:LinkButton>
                </div>
                <div class="col-md-4 col-xs-12 col-sm-12">
                    <asp:LinkButton runat="server" ID="btnCancelGbvAssessment" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnCancelGbvAssessmentModal" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;" data-dismiss="modal"> Close</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        var gbvVisitDate = $('#GbvVisitDate');
        var gbvSaveBtn = $("#<%=btnSaveGBVAssessment.ClientID%>");
        var gbvCancelBtn = $("#<%=btnCancelGbvAssessment.ClientID%>");
        var gbvResetBtn = $("#<%=btnResetGbvAssessment.ClientID%>");
        var responseOptions = $('#gbvAssessment input:radio')
    
        gbvVisitDate.datetimepicker({
            format: 'DD-MMM-YYYY',
            allowInputToggle: true,
            useCurrent: false
        });

        gbvVisitDate.on('change blur keyup keypress', function () {
            var visitDate = moment(gbvVisitDate.val());
            var patientId ="<%=PatientId%>";
            var screeningCategoryId = "<%=ScreeningCategoryId%>";

            if (visitDate.isValid()) {
                visitDate = moment(gbvVisitDate.val()).format('YYYY-MM-DD');
                getPatientScreening(patientId, visitDate, screeningCategoryId);
            }
        });

        function notifyPositiveScreening() {
            var positiveScreening = $('#gbvAssessment input[value=<%=yesId%>]:checked');
            if (positiveScreening.length > 0) {
                $('#positive-screening').show('fast');
            } else {
                $('#positive-screening').hide('slow');
            }
        }

        responseOptions.on('click change', function () {
            notifyPositiveScreening();
        });

        gbvSaveBtn.click(function () {
            if ($('#gbvAssessment').parsley().validate()) {
                var visitDate = moment(gbvVisitDate.val()).format('YYYY-MM-DD');
                var screeningDone = 1;
                var patientId ="<%=PatientId%>";
                var patientMasterVisitId = "<%=PatientMasterVisitId%>";
                var screeningCategoryId = "<%=ScreeningCategoryId%>";

                var screeningResponses = [];
                $("#gbvAssessment input:radio").each(function () {
                    if ($(this).is(":checked") == true) {
                        var screeningTypeId = $(this).attr("itemId");
                        var screeningValue = $(this).val();

                        screeningResponses.push({
                            'patientId': patientId,
                            'patientMasterVisitid': patientMasterVisitId,
                            'visitDate': visitDate,
                            'screeningDone': true,
                            'screeningDate': visitDate,
                            'screeningCategoryId': screeningCategoryId,
                            'screeningTypeId': screeningTypeId,
                            'screeningValueId': screeningValue,
                            'comment': 'null'
                        });

                    }
                });

                addPatientScreening(screeningResponses);

            }

        });

        function addPatientScreening(screeningResponses) {
            var userId = "<%=UserId%>";

            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientScreening",
                data: "{'screeningResponseString':'" + JSON.stringify(screeningResponses) + "', userId: '" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d);

                    if ($("#gbvAssessment input:radio").is(":checked") == true) {
                        $("#lblGbvAssessmentDone").text('Yes');
                    }
                    else {
                        $("#lblGbvAssessmentDone").text('No');
                    }
                    $('#gbvAssessmentModal').modal('hide');
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message);
                }
            });
        }

        function getPatientScreening(patientId, visitDate, screeningcategoryId) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/getPatientScreening",
                data: "{'patientId':'" + patientId + "', 'visitDate': '" + visitDate + "', 'screeningcategoryId': '" + screeningcategoryId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var itemList = JSON.parse(response.d);

                    $.each(itemList, function (index, patientScreening) {
                        $("#gbvAssessment input[type=radio][itemid='" + patientScreening.ScreeningTypeId + "'][value='" + patientScreening.ScreeningValueId + "']").prop('checked', true);
                    });

                    notifyPositiveScreening();

                },
                error: function (response) {
                    toastr
                        .error("Error fetching GBV Screening List " + response.d);
                }
            });

        }

        function getPatientVisit(visitId) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientMasterVisitService.asmx/GetVisitById",
                data: "{'visitId':'" + visitId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var patientVisit = JSON.parse(response.d);

                    gbvVisitDate.val(moment(patientVisit.VisitDate).format('DD-MMM-YYYY'));

                    var patientId ="<%=PatientId%>";
                    var visitDate = moment(gbvVisitDate.val());
                    var screeningCategoryId = "<%=ScreeningCategoryId%>";
                  
                    if (visitDate.isValid()) {
                        visitDate = moment(gbvVisitDate.val()).format('YYYY-MM-DD');
                        getPatientScreening(patientId, visitDate, screeningCategoryId);
                    }                    

                },
                error: function (response) {
                    toastr
                        .error("Error fetching GBV Screening List " + response.d);
                }
            });

        }

        function resetElements(parameters) {
            responseOptions.prop('checked', false);
            if (gbvVisitDate.is(':visible')) {
                gbvVisitDate.val('');
            }

            
        }

        gbvResetBtn.click(function () {
            resetElements();
        });

        gbvCancelBtn.click(function () {
           
          
            window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
        });

        if ($('#gbvAssessmentModal').is(':visible')) {
            $("#btnCancelGbvAssessmentModal").show("fast");
            $("#btnCancelGbvAssessment").hide("fast");
        } else {
            $("#btnCancelGbvAssessmentModal").hide("fast");
            $("#btnCancelGbvAssessment").show("fast");
        }

        $('#gbvAssessmentModal').on('show.bs.modal', function () {
            getPatientVisit(<%=Session["PatientMasterVisitId"]%>);
            gbvVisitDate.parents('.col-md-12:first').hide();

            //Hide visit date cells
            $("#btnCancelGbvAssessmentModal").show("fast");
            $("#btnCancelGbvAssessment").hide("fast");
        });

        notifyPositiveScreening();
    });
</script>
