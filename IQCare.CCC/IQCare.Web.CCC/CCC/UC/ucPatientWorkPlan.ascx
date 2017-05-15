<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientWorkPlan.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientWorkPlan" %>
<div>
    <table id="tblWorkPlan" class="table table-bordered table-striped" style="width:100%">
        <thead>
            <tr>
                <th><span class="text-primary">Date</span></th>
                <th><span class="text-primary">Work Plan</span></th>
            </tr>
        </thead>
    </table>
    
</div>
<script>
    $(document).ready(function () {
        if ($.fn.dataTable.isDataTable('#tblWorkPlan')) {
            var workPlanTable = $('#tblWorkPlan').DataTable();
        }
        else {
            var workPlanTable = $('#tblWorkPlan').DataTable({
                ajax: {
                    type: "POST",
                    url: "../WebService/PatientEncounterService.asmx/LoadWorkPlan",
                    dataSrc: 'd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                paging: false,
                searching: false,
                info: false,
                ordering: false
            });
        }

        
    });
</script>