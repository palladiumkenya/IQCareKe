<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientDuplicateFinder.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientDuplicateFinder" %>
<div class="row">
    <div class="col-md-12">
        <h4 class="pull-left"><i class="fa fa-search fa-3x warning"></i> Find Duplicate Patient Records <small><i> (Select appropriate comparison parameters) </i></small></h4>
    </div>
    <hr/>
</div>

<div class="row" id="searchGrid">
    <!-- <div id="searchGrid"> -->
    <div class="col-md-12 form-group">
        
        <div class="col-lg-3 bg-success">
            <label> <small>Compare by Name(s) : </small></label>
        </div>

        <div class="col-md-3">
            <input type="checkbox" name="chkFirstName" id="chkFirstName" value="1" checked="checked" class="form-check-input" />
            <label>First Name</label>
        </div>
        <div class="col-md-3">
            <input type="checkbox" name="chkLastName" id="chkLastName" value="1" checked="checked" class="form-check-input" />
            <label class="">Last Name</label>
        </div>
        <div class="col-md-3">
            <input type="checkbox" name="chkMiddleName" id="chkMiddleName" value="1" class="form-check-input" />
            <label>Middle Name</label>
        </div>
    </div>

    <div class="col-md-12 form-group">
        <div class="col-lg-3 bg-warning"><small> Compare By Sex/Gender & Enrollment Date :</small> </div>
        <div class="col-md-3  pull-left">
            <input type="checkbox" name="chkSex" id="chkSex" value="1" checked="checked" class="form-check-input" />
            <label>Sex / Gender </label>
        </div>
        <div class="col-md-3">
            <input type="checkbox" name="chkEnrollmentNo" id="chkEnrollmentNo" value="1" checked="checked" class="form-check-input" />
            <label>Enrollment Number</label>
        </div>
        <div class="col-md-3">
            <input type="checkbox" name="chkDOB" id="chkDOB" value="1" checked="checked" class="form-check-input" />
            <label>Date of Birth</label>
        </div>
    </div>
    <div class="col-md-12 form-group">
        <div class="col-lg-3 bg-danger"><small>Compare By Programme Parameters : </small></div>
        <div class="col-md-3 pull-left">
            <input type="checkbox" name="chkEnrollmentDate" id="chkEnrollmentDate" value="1" checked="checked" class="form-check-input" />
            <label class="`">Enrollment Date</label>
        </div>
        <div class="col-md-3  pull-left">
            <input type="checkbox" name="chkARTStartdate" id="chkARTStartdate" value="1" class="form-check-input" />
            <label class="danger-color">ART Start Date</label>
        </div>
        <div class="col-md-3  pull-left">
            <input type="checkbox" name="chkHIVDiagnosisDate" id="chkHIVDiagnosisDate" value="1" class="form-check-input" />
            <label class="">HIV Diagnosis Date</label>
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
            <button name="btnSearch" id="btnSearch" value=" Search" class="btn btn-info btn-lg  btn-block" onclick="return false"><i class="fa fa-search"></i>Search</button>
        </div>
    </div>
<!--</div> -->
</div>
<div class="row">
    <div class="col-md-12">
        <div class="col-md-3 col-xs-4 col-sm-3">
            <div class="col-md-12 pull-left" id="divAction">
                <i class="fa fa-spinner fa-pulse fa-3x fa-fw pull-left text-info"></i>
                <span class="sr-only"></span><strong class="pull-left" id="divActionString">Fetching Patient data</strong>
            </div>
        </div>
        <div class="col-md-3 col-xs-4 col-sm-3">
        </div>
        <div class="col-md-3 col-xs-4 col-sm-3">
        </div>
        <div class="col-md-3 col-xs-4 col-sm-3">
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-12 col-xs-12 col-sm-12 bs-callout bs-callout-info" id="infoGrid">
        <div class="col-md-6">
            <label class="control-label pull-left text-warning fa fa-search-plus">Patient Duplicates Search Results </label>
        </div>
        <div class="col-md-6 pull-right">
            <button id="btnRemoveGrid" class="btn btn-warning btn-lg btn-sm pull-right fa fa-arrow-circle-o-left" onclick="return false"> Back to Patient Duplicates Finder</button>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-12 col-xs-12 col-sm-12" style="padding: 5px; text-align: left;" id="infoGridMessage"></div>
</div>

<div class="row">
    <div class="col-md-12" id="duplicateFinder">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-3 col-xs-4 col-sm-3">
                </div>
                <div class="col-md-3 col-xs-4 col-sm-3">
                </div>
                <div class="col-md-3 col-xs-4 col-sm-3">
                </div>
                <div class="col-md-3 col-xs-4 col-sm-3">
                    <button type="button" class="btn btn-info btn-sm btn-block pull-right" data-toggle="modal" data-target="#previewDuplicatesModel" id="btnPreviewDuplicates"><i class="fa fa-eye"></i> Preview Selected</button>
                </div>
           
            </div>
        </div>
        <div class="col-md-12">
            <table id="tblFindDuplicates" class="table display" style="cursor: pointer" width="100%">
                <thead>
                <tr>
                    <th>First Name</th>
                    <th>Middle Name</th>
                    <th>Last Name</th>
                    <th>Date Of Birth</th>
                    <th>CCC Number</th>
                    <th>Sex</th>
                    <th>Enrollment Date</th>
                    <th>Cell</th>
                    <th style="width: 10px;">Patient Id</th>
                    <th></th>
                </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>
</div>

<div class="row">
    <div id="previewDuplicatesModel" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
        <div class="modal-dialog" style="width: 70%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <h4 class="modal-title"><i class="fa fa-eye"></i> Preview/Compare Duplicates</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <table>
                                <tr>
                                    <td>CCC Number</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>First Name</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Middle Name</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Date od Birth</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Last VL</td>
                                </tr>
                                <tr>
                                    <td>Current Regimen</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <label>Preferred</label>
                                        <input type="radio" name="rbPreferred" value="1234" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-12">
                        <div class="pull-right">
                            <button type="button" id="btnMergeDuplicates" class="btn btn-primary"><i class="fa fa-copy"></i> Merge</button>
                            <button type="button" id="btnMergeDuplicatesCancel" class="btn btn-warning" data-dismiss="modal"><i class="fa fa-close"></i> Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<!-- Modal -->


<script type="text/javascript">

    $().ready(function () {

        //Extend data tables to delay search until at least 3 characters hae been entered in the search box and enter pressed
        jQuery.fn.dataTableExt.oApi.fnSetFilteringEnterPress = function (oSettings) {
            var _that = this;

            this.each(function (i) {
                $.fn.dataTableExt.iApiIndex = i;
                var
                    $this = this,
                    oTimerId = null,
                    sPreviousSearch = null,
                    anControl = $('input', _that.fnSettings().aanFeatures.f);

                anControl
                    .unbind('keyup')
                    .bind('keyup', function (e) {

                        if (anControl.val().length > 2 && e.keyCode == 13) {
                            _that.fnFilter(anControl.val());
                        }
                    });

                return this;
            });
            return this;
        }

        $("#duplicateFinder").hide();
        $("#infoGrid").hide();
        $("#infoGridMessage").hide();
        $("#divAction").hide("fast");

        var table = null;
        $("#btnSearch").click(function (e) {

            var selectedCriteria = $("#searchGrid").find("input[type=checkbox]:checked");

            if (selectedCriteria.length < 3) {
                toastr.error("Select at least 3 Search criteria");
                return;
            }

            $("#divAction").show("fast");
            $("#divActionString").text("Consolidating table and data features..");

            table = $("#tblFindDuplicates").dataTable({
                "oLanguage": {
                    "sZeroRecords": "No records to display",
                    "sSearch": "Search from all Records"
                },
                "bProcessing": true,
                "bServerSide": true,
                "ordering": true,
                "searching": true,
                "info": true,
                "bDestroy": true,
                "sAjaxSource": "../WebService/PatientService.asmx/GetDuplicatePatientRecords",
                "sPaginationType": "full_numbers",
                "bDeferRender": true,
                "responsive": true,
                "sPaginate": true,
                "lengthMenu": [[10, 20, 50], [10, 20, 50]],
                "aoColumns":
                    [
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    ],
                'aoColumnDefs': [{
                    'targets': 9,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="groupingId[]" value="' + data + '">';
                    }
                }],
                "fnServerData": function (sSource, aoData, fnCallback) {

                    aoData.push({ "name": "matchFirstName", "value": "" + $("#chkFirstName").is(":checked") + "" });
                    aoData.push({ "name": "matchLastName", "value": "" + $("#chkLastName").is(":checked") + "" });
                    aoData.push({ "name": "matchMiddleName", "value": "" + $("#chkMiddleName").is(":checked") + "" });
                    aoData.push({ "name": "matchSex", "value": "" + $("#chkSex").is(":checked") + "" });
                    aoData.push({ "name": "matchEnrollmentNumber", "value": "" + $("#chkEnrollmentNo").is(":checked") + "" });
                    aoData.push({ "name": "matchDob", "value": "" + $("#chkDOB").is(":checked") + "" });
                    aoData.push({ "name": "matchEnrollmentDate", "value": "" + $("#chkEnrollmentDate").is(":checked") + "" });
                    aoData.push({ "name": "matchARTStartDate", "value": "" + $("#chkARTStartdate").is(":checked") + "" });
                    aoData.push({ "name": "matchHIVDiagnosisDate", "value": "" + $("#chkHIVDiagnosisDate").is(":checked") + "" });

                    $("#divActionString").text("Data features and table preparation complete");
                    var arrayReturn = [];

                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "POST",
                        "url": sSource,
                        "data": JSON.stringify({ dataPayLoad: aoData }),
                        "success": function (msg) {
                            $("#divActionString").text("Rendering patients data...");
                            var json = jQuery.parseJSON(msg.d);
                            if (json === null) {
                                var data = [];
                                arrayReturn["data"] = data;
                                arrayReturn["draw"] = 1;
                                arrayReturn["recordsTotal"] = 0;
                                arrayReturn["recordsFiltered"] = 0;

                                fnCallback(arrayReturn);
                                $("#duplicateFinder").slideDown("fast", function () { $("#divAction").hide("fast"); $("#infoGrid").slideDown("fast", function () { $("#searchGrid").slideUp("fast") }); $("#infoGridMessage").slideDown("fast"); });
                            } else {
                                fnCallback(json);
                                $("#duplicateFinder").slideDown("fast", function () { $("#divAction").hide("fast"); $("#infoGrid").slideDown("fast", function () { $("#searchGrid").slideUp("fast") }); $("#infoGridMessage").slideDown("fast"); });
                            }
                        },
                        "error": function (xhr, errorType, exception) {
                            $("#divAction").show("fast");

                            var jsonError = jQuery.parseJSON(xhr.responseText);
                            $("#divAction").text(jsonError.Message);
                            toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                            return false;
                        }
                    });
                }
            }).fnSetFilteringEnterPress();

        });

        $("#btnPreviewDuplicates").hide("fast");

        function loadPatientDuplicateComparisonTables() {

            var modalContainer = $("#previewDuplicatesModel .modal-body div.row");

            modalContainer.empty();

            var duplicatePatientsList = [];

            $("table#tblFindDuplicates tbody tr").each(function () {

                if ($(this).find("input[type=checkbox]").is(":checked")) {

                    var rowDataCells = $(this).find("td");
                    var patient = {};
                    patient["First Name"] = $(rowDataCells[0]).html();
                    patient["Middle Name"] = $(rowDataCells[1]).html();
                    patient["Last Name"] = $(rowDataCells[2]).html();
                    patient["Date of Birth"] = $(rowDataCells[3]).html();
                    patient["CCC Number"] = $(rowDataCells[4]).html();
                    patient["Sex"] = $(rowDataCells[5]).html();
                    patient["Enrollment Date"] = $(rowDataCells[6]).html();
                    patient["Cell"] = $(rowDataCells[7]).html();
                    patient["Id"] = $(rowDataCells[8]).html();

                    duplicatePatientsList.push(patient);

                }

            });


            $.each(duplicatePatientsList, function (k, patientRecord) {

                var divWrapper = $("<div/>").addClass("col-md-6");

                var table = $("<table/>").addClass("table table-bordered table-striped");

                $.each(Object.keys(patientRecord), function (key, value) {

                    var tr = $("<tr/>");
                    var td1 = $("<td/>").append($("<span/>").addClass("pull-left").append(value));
                    var td2 = $("<td/>").append(patientRecord[value]);
                    tr.append(td1);
                    tr.append(td2);

                    table.append(tr);

                });

                var radio = $("<input/>", { type: "radio", name: "rbPreferred", value: patientRecord["Id"] });
                var lastRow = $("<tr/>").append($("<td/>").append("Preferred?")).append($("<td/>").append(radio));

                table.append(lastRow);

                divWrapper.append(table);

                modalContainer.append(divWrapper);

            });

        }

        $("table#tblFindDuplicates").on("click", "input[type=checkbox]", function () {
            if ($(this).is(':checked')) {
                $("table input[value=" + $(this).val() + "]").prop("checked", true);
                $("table input[value!=" + $(this).val() + "]").prop("checked", false);
                $("#btnPreviewDuplicates").show("fast");
                loadPatientDuplicateComparisonTables();
            } else {
                $("table input[type=checkbox]").prop("checked", false);
                $("#btnPreviewDuplicates").hide("fast");
            }
        });

        $("#btnRemoveGrid").click(function () {
            $("#infoGrid").slideUp("fast",
                function () {
                    $("#duplicateFinder").slideUp("fast",
                        function () {
                            $("#searchGrid").slideDown("fast");
                        });
                });
            $("#infoGridMessage").slideUp("fast");
        });

        $("#btnMergeDuplicates").click(function () {

            var preferred = $("input[name=rbPreferred]:checked");

            if (preferred.length == 0) {
                toastr.error("Please select a preferred Patient Record");
                return;
            }

            if (!confirm("The operation that you are about to perform cannot be undone. Proceed with extreme caution. Are you sure you want to proceed?")) {
                return;
            }

            var preferredId = preferred.val();
            var unPreferredId = $("input[name=rbPreferred]:not(:checked)")[0].value;;
            var mergeData = { preferredPatientId: preferredId, unPreferredPatientId: unPreferredId };

            $.ajax({
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "type": "POST",
                "url": "../WebService/PatientService.asmx/MergePatientRecords",
                "data": JSON.stringify(mergeData),
                "success": function (response) {
                    toastr.success(response.d);

                    $("#previewDuplicatesModel").modal("hide");
                    $("#btnSearch").trigger('click')
                },
                "error": function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message + " ");
                    return false;
                }
            });
        });

    });

</script>
