<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientLabsExtruder.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientLabsExtruder" %>

  <div class="col-md-12 bs-callout bs-callout-danger">
    <h4 class="pull-left"> <strong>Pending Labs:</strong> </h4>                             
    <table class="table table-striped table-condensed" id="xtblPendingLabs" clientidmode="Static" runat="server">
        <thead>
            <tr >
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Lab Test</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Order Reason</i> </th>
                <th> <i class="control-label text-warning pull-right " aria-hidden="true">Order Date</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Status </i></th>
            </tr>
        </thead>
        <tbody>                        
        </tbody>                  
    </table>
</div>


 <div class="col-md-12 bs-callout bs-callout-info">
                         <h4 class="pull-left"> <strong>Latest Labs Results:</strong> </h4>             
    <table class="table table-striped table-condensed" id="xtblCompleteLabs" clientidmode="Static" runat="server">
        <thead>
            <tr >
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Lab Test</i> </th>
                <%--<th> <i class="control-label text-warning pull-right" aria-hidden="true">Test Parameter</i> </th>--%>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Order Reason</i> </th>
                <th> <i class="control-label text-warning pull-right " aria-hidden="true">Order Date</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Result</i></th>
                <%--<th> <i class="control-label text-warning pull-right" aria-hidden="true">Result Date</i> </th>--%>
            </tr>
        </thead>
        <tbody>                        
        </tbody>                  
    </table>
</div>

<script type="text/javascript">
    $(document).ready(function() {
        
        $.ajax({
            type: 'POST',
            url: '../WebService/LabService.asmx/ExtruderPendingLabsList',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            cache: false,
            success: function (response) {
               //console.log(response.d);
                var itemList = JSON.parse(response.d);
                var table = '';
                //itemList.forEach(function (item) {
                $.each(itemList, function (index, itemList) {

                    var dateString = itemList.SampleDate.substr(6);
                    var currentTime = new Date(parseInt(dateString));
                    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                     "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                    var month = monthNames[currentTime.getMonth()];
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var sampleDate = day + "-" + month + "-" + year;

                    table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';

                });
                $("#xtblPendingLabs td").parent().remove();
                $('#xtblPendingLabs').append(table);
                $('#xtblPendingLabs tr:not(:first-child').each(function (idx) {
                    $(this).children(":eq(0)").html(idx + 1);
                });


            },

            error: function (msg) {

                alert(msg.responseText);
            }
        });

        $.ajax({
            type: 'POST',
            url: '../WebService/LabService.asmx/ExtruderCompleteLabsList',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            cache: false,
            success: function (response) {
                //console.log(response.d);
                var itemList = JSON.parse(response.d);
                console.log(itemList);
                var table = '';
                //itemList.forEach(function (item) {
                $.each(itemList, function (index, itemList) {

                    var dateString = itemList.SampleDate.substr(6);
                    var currentTime = new Date(parseInt(dateString));
                    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                     "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                    var month = monthNames[currentTime.getMonth()];
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var sampleDate = day + "-" + month + "-" + year;                  

                    var resultValues = itemList.ResultValues;
                    var undetectable = itemList.LabTestId;
                    var resultTexts = itemList.ResultTexts;
                    var resultOptions = itemList.ResultOptions;
                    var resultUnits = itemList.ResultUnits;
                   
                    var labResults = "";
                    //Results units
                    if (resultUnits == null) {
                        resultUnits = "";
                    }
                    
                    //Results values==>Text==>Options
                    if (resultTexts == null && resultOptions == null) {
                        labResults = resultValues;
                    
                    } else if (resultTexts != null && resultOptions == null) {
                    labResults = resultTexts;

                    } else if (resultTexts == null && resultOptions != null) {
                        labResults = resultOptions;
                    }

                    //Undetectable vl
                    if ((undetectable == 3) && resultValues <= 50) {
                        labResults = "Undetectable";
                    }

               
                    table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + labResults +" "+ resultUnits + '</td></tr>';

                });
                $("#xtblCompleteLabs td").parent().remove();
                $('#xtblCompleteLabs').append(table);
                $('#xtblCompleteLabs tr:not(:first-child').each(function (idx) {
                    $(this).children(":eq(0)").html(idx + 1);
                });
                
            },

            error: function (msg) {

                alert(msg.responseText);
            }
        });




    });
    </script>