<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientLabsExtruder.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientLabsExtruder" %>

<div>
    <h4> <strong>Previous Labs Results:</strong> </h4>                           
    <table class="table table-striped table-condensed" id="tblPendingLabs" clientidmode="Static" runat="server">
        <thead>
            <tr >
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Lab Test</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Order Reason</i> </th>
                <th> <i class="control-label text-warning pull-right " aria-hidden="true">Order Date</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Result </i></th>
            </tr>
        </thead>
        <tbody>                        
        </tbody>                  
    </table>
</div>

<script type="text/javascript">
    $(document).ready(function() {
        
        $.ajax({
            type: "POST",
            url: "../WebService/LabService.asmx/GetLookupPreviousLabsList",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
               console.log(response.d);
                var itemList = JSON.parse(response.d);
                var table = '';
                //itemList.forEach(function (item) {
                $.each(itemList, function (index, itemList) {

                    var dateString = itemList.SampleDate.substr(6);
                    var currentTime = new Date(parseInt(dateString));
                    var month = currentTime.getMonth() + 1;
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var sampleDate = day + "/" + month + "/" + year;

                    table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';

                });

                $('#tblPendingLabs').append(table);
                $('#tblPendingLabs tr:not(:first-child').each(function (idx) {
                    $(this).children(":eq(0)").html(idx + 1);
                });


            },

            error: function (msg) {

                alert(msg.responseText);
            }
        });















    });
    </script>