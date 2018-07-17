<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="MessageViewer.aspx.cs" Inherits="IQCare.Web.CCC.IL.MessageViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div>
        <div class="col-md-12 form-group">
            <div class="col-md-12">
                <label class="control-label pull-left text-info"></label>
            </div>
            
            <table id="tblMessages" class="table table-hover cell-border compact stripe">
                <thead class="thead-default">
                <tr>
                    <th><span class="text-primary" aria-hidden="true">#</span></th>
                    <th><span class="text-primary" aria-hidden="true">Date Received</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Sender</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Date Processed</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Message Type</span> </th>
                    <th><span class="text-primary" aria-hidden="true">Error Message</span> </th>
                </tr>
                </thead>
            </table>
        </div>
        
        <script type="text/javascript">
            $(document).ready(function() {
                var messageType = "<%=MessageType%>";

                var messagesArray = [];

                $.ajax({
                    type: "POST",
                    url: "../WebService/LookupService.asmx/GetMessages",
                    data: "{'messageType':'" + messageType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function(response) {
                        var messages = response.d;
                        console.log(messages);
                        for (var i = 0, len = messages.length; i < len; i++) {
                            messagesArray.push([
                                i,
                                moment(messages[i].DateReceived).format('DD-MMM-YYYY'),
                                messages[i].SenderSystem,
                                moment(messages[i].DateProcessed).format('DD-MMM-YYYY'),
                                messages[i].MessageType,
                                messages[i].LogMessage
                            ]);
                        }
                        console.log(messagesArray);
                        initialiseDataTable(messagesArray);
                    },
                    error: function(msg) {
                        alert(msg);
                    }
                });

                function initialiseDataTable(data) {
                    $("#tblMessages").dataTable().fnDestroy();
                    tablefamily = $('#tblMessages').DataTable({
                        "aaData": data,
                        paging: true,
                        searching: false,
                        "columnDefs": [{
                            "data": null,
                            "targets": -1,
                            className: 'dt-head-right'
                        }]
                    });
                    //tableMessages = $('#tblMessages').DataTable({
                    //    "columnDefs": [
                    //        {
                    //            "targets": [6],
                    //            "visible": false,
                    //            "searchable": false
                    //        }
                    //    ],
                    //    //"aaData": data,
                    //    paging: true,
                    //    searching: true
                    //});
                }
            });
        </script>
    </div>
</asp:Content>
