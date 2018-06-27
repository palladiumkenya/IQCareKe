<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTannerStaging.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucTannerStaging" %>
<!-- Tanner Staging --->
<div class="panel panel-info">
	<div class="panel-body">
        <div class="col-md-12 form-group">
		    <div>
			    <label class="control-label  pull-left text-primary">Record Tanners Staging</label>
		    </div>

		    <div>
			    <asp:RadioButtonList ClientIDMode="Static" ID="rbRecordTannersStaging" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="3" CellSpacing="2"> 
                </asp:RadioButtonList>
		    </div>
	    </div>
        <div id="tannerspanel">
            <div class="col-md-12 form-group">
			    <label class="control-label pull-left">Tanner Staging</label>
                <div class="milestone-link pull-right"><a target="_blank" href="../../Content/resources/Tanners.pdf"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> Tanner Staging Guide</a></div>
		    </div>

		    <div class="col-md-12 form-group">
			    <div class="col-md-3 form-group">
				    <div class="col-md-12">
                        <label for="txtTannerStagingDate" class="control-label pull-left">Date</label>
					    <%--<label for="ChronicIllnessName" class="control-label pull-left">Illness</label>--%>
				    </div>
				    <div class="col-md-12">
					    <div class='input-group date' id='TannerStagingDate'>
						    <span class="input-group-addon">
							    <span class="glyphicon glyphicon-calendar"></span>
						    </span>
						    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="txtTannerStagingDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
					    </div>
				    </div>
			    </div>

			    <div class="col-md-3 form-group">
				    <div class="col-md-12">
					    <label class="control-label pull-left" for="ddlBreastsGenitals">F Breasts / M Genitals</label>
				    </div>
				    <div class="col-md-12">
					    <asp:DropDownList runat="server" ID="ddlBreastsGenitals" CssClass="form-control input-sm" ClientIDMode="Static" />
				    </div>
			    </div>

			    <div class="col-md-3 form-group">
				    <div class="col-md-12">
					    <label class="control-label pull-left">Pubic Hair</label>
				    </div>
				    <div class="col-md-12">
					    <asp:DropDownList runat="server" ID="ddlPubicHair" CssClass="form-control input-sm" ClientIDMode="Static" />
				    </div>
			    </div>

                <div class="col-md-3 form-group">
			        <div class="col-md-12 text-center">
				        <label class="control-label"><span class="fa fa-cog">Action</span></label>
			        </div>
			        <div class="col-md-12 col-centered">
				        <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddTannerStage" onclick="addTannersStaging();">Add</button>
			        </div>
                </div>
		    </div>

		    <div class="col-md-12 form-group">
			    <div class="panel panel-primary">
				    <div class="panel-heading">Tanners Staging</div>
				    <div style="min-height: 10px; max-height: 550px; overflow-y: auto; overflow-x: hidden;">
					    <table id="dtlTannerStaging" class="table table-bordered table-striped" width="100%">
						    <thead>
							    <tr>
                                    <th><span class="text-primary">Id</span></th>
								    <th><span class="text-primary">Date</span></th>
								    <th><span class="text-primary">F Breasts / M Genitals</span></th>
								    <th><span class="text-primary">Pubic Hair</span></th>
                                    <th><span class="text-primary">Delete</span></th>
							    </tr>
						    </thead>
					    </table>

				    </div>
			    </div>
		    </div>
        </div>
		

	</div>
</div>

<script type="text/javascript">
    $("#TannerStagingDate").datetimepicker({
        format: 'DD-MMM-YYYY',
        allowInputToggle: true,
        useCurrent: false
    });

    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 2) {
            var tannersId = <%=TannersId%>;
            if (tannersId > 0) {
                updateRecordTannersStaging(tannersId);
            }
            else {
                recordTannersStaging();
            }
        }
    });

    //Call Webservice
    var tannersTbl;
    var previousTannersStaging = getTannersStaging();
    function getTannersStaging() {
        tannersTbl = $('#dtlTannerStaging').DataTable({
            ajax: {
                type: "POST",
                url: "../WebService/TannersStagingService.asmx/LoadTannersStaging",
                dataSrc: 'd',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            },
            destroy: true,
            paging: false,
            searching: false,
            info: false,
            ordering: false,
            columnDefs: [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }

    function addTannersStaging() {
        var tannersStagingDate = $("#<%=txtTannerStagingDate.ClientID%>").val();
        var breastsGenitals = $("#<%=ddlBreastsGenitals.ClientID%>").val();
        var pubicHair = $("#<%=ddlPubicHair.ClientID%>").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/TannersStagingService.asmx/addTannersStaging",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','tannersStagingDate': '" + tannersStagingDate + "'," +
            "'breastsGenitals': '" + breastsGenitals + "', 'pubicHair':'" + pubicHair + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                getTannersStaging();
                if (response.d == "Incremental Error") {
                    toastr.error("Tanner staging should be Incremental");
                }
                else
                {
                    toastr.success(response.d, "Tanner staging saved");
                    resetTannerStagingFields();
                }
            },
            error: function (response) {
                toastr.error(response.d, "Error saving tanner staging");
            }
        });
    }
    $("#dtlTannerStaging").on('click', '.btnDelete', function () {
        var tannersId = tannersTbl.row($(this).parents('tr')).data()["0"];
        var tannersDate = tannersTbl.row($(this).parents('tr')).data()["1"];
        var clickSection = this;
        var delStatus = confirmDeleteTanners(tannersId, tannersDate, clickSection);
        if (delStatus == "Tanners staging not deleted") {
            toastr.error("Tanners staging not deleted");
        }
    });
    function confirmDeleteTanners(tannersId, tannersDate, clickSection) {
        var txt;
        var r = confirm("Delete " + tannersDate + " tanners staging");
        if (r == true) {
            DeleteTanners(tannersId, clickSection);
        }
        else {
            return "Tanners staging not deleted";
        }
    }
    function DeleteTanners(tannersId, clickSection) {
        $.ajax({
            type: "POST",
            url: "../WebService/TannersStagingService.asmx/DeleteTanners",
            data: "{'tannersId': '" + tannersId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                tannersTbl.row($(clickSection).parents('tr'))
                    .remove().draw();
                var index = reactionEventList.indexOf($(clickSection).parents('tr').find('td:eq(0)').text());
                if (index > -1) {
                    reactionEventList.splice(index, 1);
                }
                toastr.success("Tanners Staging Deleted");
            },
            error: function (response) {
                toastr.error("Tanners staging not deleted");
            }
        });
    }
    function resetTannerStagingFields()
    {
        $("#txtTannerStagingDate").val("");
        $("#ddlBreastsGenitals").val(1);
        $("#ddlPubicHair").val(1);
    }

    function recordTannersStaging()
    {
        var recordTannersStaging = $("input[name='<%=rbRecordTannersStaging.UniqueID %>']:checked").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/TannersStagingService.asmx/recordTannersStaging",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','createdBy':'" + userId + "','recordTannersStaging': '" + recordTannersStaging + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success("Tanners staging recorded");
            },
            error: function (response) {
                toastr.error("Tanners staging not recorded");
            }
        });
    }
    function updateRecordTannersStaging(tannersId)
    {
        var recordTannersStaging = $("input[name='<%=rbRecordTannersStaging.UniqueID %>']:checked").val();
        var patientId = <%=PatientId%>;
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
        $.ajax({
            type: "POST",
            url: "../WebService/TannersStagingService.asmx/updateRecordTannersStaging",
            data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','recordTannersStaging': '" + recordTannersStaging + "','tannersId':'" + tannersId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                toastr.success("Tanners staging updated");
            },
            error: function (response) {
                toastr.error("Tanners staging not updated");
            }
        });
    }

    $(document).ready(function () {
        showHideTannersPanel();
    });

    $("input[name='<%=rbRecordTannersStaging.UniqueID%>']").change(function () {
        showHideTannersPanel();
    });

    function showHideTannersPanel() {
        var radioButtons = $("input[name='<%=rbRecordTannersStaging.UniqueID%>']");
        var selectedIndex = radioButtons.index(radioButtons.filter(':checked'));
        if (selectedIndex == 1) {
            $("#tannerspanel").hide();
        }
        else {
            $("#tannerspanel").show();
        }
    }
</script>