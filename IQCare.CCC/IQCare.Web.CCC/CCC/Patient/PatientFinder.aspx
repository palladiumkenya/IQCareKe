<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientFinder.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientFinder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12 col-xs-12 col-sm-12 ">
         <div class="col-md-12 col-xs-12 col-sm-12 bs-callout bs-callout-info" id="searchGrid">
              <div class="col-md-12"><small class="pull-left"><strong><i class="fa fa-search fa-2x" aria-hidden="true"> Find Patient </i></strong></small></div>                                                  
              <div class="col-md-12"><hr/></div>
              <div class="col-md-12">
                             <div class="col-md-3 form-group">
                                    <div class="col-md-12"><label class="control-label pull-left">Identification Number</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox runat="server" ID="PatientNumber" CssClass="form-control input-sm" placeholder="patient number.." ClientIDMode="Static"></asp:TextBox>           
                                    </div>
                                </div>
                              
                             <div class="col-md-3">
                                     <div class="col-md-12"><label class="control-label pull-left">First Name</label></div>
                                     <div class="col-md-12">
                                          <asp:TextBox runat="server" ID="FirstName" CssClass="form-control input-sm" placeholder="first name.." ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                               
                             <div class="col-md-3">
                                      <div class="col-md-12"><label class="control-label pull-left">Middle Name</label></div>
                                      <div class="col-md-12">
                                           <asp:TextBox runat="server" ID="MiddleName" CssClass="form-control input-sm" placeholder="middle name.." ClientIDMode="Static"></asp:TextBox>
                                      </div>
                                 </div>
                                 
                              <div class="col-md-3">
                                      <div class="col-md-12"><label class="control-label pull-left">Last Name</label></div>
                                      <div class="col-md-12">
                                           <asp:TextBox runat="server" ID="LastName" CssClass="form-control input-sm" placeholder="Last name.." ClientIDMode="Static"></asp:TextBox>
                                      </div>
                                 </div>
                        </div><%-- .col-md-12--%>
              <div class="col-md-12">
                  <div class="col-md-3">
                                   <div class="col-md-12"><label class="control-label pull-left">Facility</label></div>
                                   <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="Facility" ClientIDMode="Static" CssClass="form-control input-sm"/>                
                                   </div>
                             </div>          
                 
                   <div class="col-md-3">

                              </div>
                                         
                   <div class="col-md-3">

                             </div>
                             
                              
                              
                    <div class="col-md-3">

                              </div>

              </div><%-- .col-md-12--%>
              <div class="col-md-12"><hr/></div>
              <div class="col-md-12 col-xs-12 col-sm-12">
                              <div class="col-md-4 col-xs-12 col-sm-12"></div>
                              <div class="col-md-4 col-xs-12 col-sm-12"></div> 
                              <div class="col-md-4 col-xs-12 col-sm-12">
                                   <div class="col-md-4 col-xs-12 col-sm-12">
                                        <asp:LinkButton runat="server" ID="btnFindPatient" OnClientClick="return false" ClientIDMode="Static" CssClass="btn btn-info btn-lg fa fa-search fa-1x"> Find Patient</asp:LinkButton>
                                    </div>
                                   <div class="col-md-4 col-xs-12 col-sm-12">
                                        <asp:LinkButton runat="server" ID="btnReset" OnClientClick="return false" ClientIDMode="Static" CssClass="btn btn-warning btn-lg fa fa-refresh"> Reset Find</asp:LinkButton>

                                  </div>
                                   <div class="col-md-4 col-xs-12 col-sm-12">
                                        <asp:LinkButton runat="server" ClientIDMode="Static" OnClientClick="return false" ID="btnClose" CssClass="btn btn-danger btn-lg fa fa-times"> Close Find </asp:LinkButton>
                                   </div>

                                  
                              </div> 
                          </div>
         </div> 
         <div class="col-md-12 bs-callout bs-callout-info" id="infoGrid">
             <div class="col-md-6">
                 <label class="control-label pull-left text-warning fa fa-search-plus"> Patient Search Results </label>
             </div>
             <div class="col-md-6 pull-right">
                 <button id="btnRemoveGrid" class="btn btn-warning btn-lg pull-right fa fa-arrow-circle-o-left" onclick="return false"> Back to Search</button>
             </div>
         </div>

         <div class="col-md-12 form-group" id="PatientSearch">
      

             <table id="tblFindPatient" class="display" style="cursor:pointer" width="100%">
                 <thead>
                    <tr>
      	                <th>PatientId</th>
                        <th>CCC Number</th>
      	                <th>First Name</th>
      	                <th>Middle Name</th>
      	                <th>Last Name</th>
      	                <th>Date Of Birth</th>
      	                <th>Sex</th>
      	                <th>Enrollment Date</th>
                        <th>PatientStatus</th>
                    </tr>
                 </thead>
                 <tbody></tbody>
                 <tfoot>
                    <tr>
      	                <th>EnrollmentNumber</th>
                        <th>PatientIndex</th>
      	                <th>FirstName</th>
      	                <th>MiddleName</th>
      	                <th>LastName</th>
      	                <th>DateOfBirth</th>
      	                <th>Sex</th>
      	                <th>RegistrationDate</th>
                        <th>PatientStatus</th>
                    </tr>
                 </tfoot>

                    </table>
                </div>

<%--        <div class="col-md-12">
            <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>First name</th>
                <th>Last name</th>
                <th>Position</th>
                <th>Office</th>
                <th>Start date</th>
                <th>Salary</th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th>First name</th>
                <th>Last name</th>
                <th>Position</th>
                <th>Office</th>
                <th>Start date</th>
                <th>Salary</th>
            </tr>
        </tfoot>
    </table>
        </div>--%>
            
    </div><%--.col-md-12--%>
    
    <script type="text/javascript">
        $(document).ready(function() {

            $.ajaxSetup({
                cache: false
            });

            $("#btnRemoveGrid").click(function() {
                $("#infoGrid").slideUp("fast",
                    function() {
                        $("#PatientSearch").slideUp("fast",
                            function() {
                                $("#searchGrid").slideDown("fast");
                            });
                    });
            });

            $("#btnClose").click(function() {
                window.location.href="<%=ResolveClientUrl("~/CCC/Home.aspx")%>";
            });

            $("#btnReset").click(function() {

                $("#<%=PatientNumber.ClientID%>").val('');
                $("#<%=FirstName.ClientID%>").val('');
                $("#<%=MiddleName.ClientID%>").val('');
                $("#<%=LastName.ClientID%>").val('');
                $("#<%=Facility.ClientID%>").val('');
            });

            $("#PatientSearch").hide();
            $("#infoGrid").hide();

            //$("#SearchDoB")
            //    .datepicker({ allowPastDates: true, momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' } });
            //$("#RegDate").datepicker('setDate','',{ allowPastDates: true, momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' } });

            /*--change cursor to hand during selection*/
           // $("#tblFindPatient tr").css('cursor', 'hand');

            var table = null;
            $("#btnFindPatient").click(function (e) {

            table=    $("#tblFindPatient").dataTable({
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
                    "sAjaxSource": "../WebService/PatientLookupService.asmx/GetPatientSearchx",
                    "sPaginationType": "full_numbers",
                    "bDeferRender": true,
                    "responsive":true,
                    "sPaginate": true,
                    "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
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
                                    null
                                ],
                    "fnServerData": function (sSource, aoData, fnCallback) {
                        aoData.push({ "name": "patientId", "value": "" + $("#<%=PatientNumber.ClientID%>").val() + "" });
                        aoData.push({ "name": "firstName", "value": ""+$("#<%=FirstName.ClientID%>").val()+"" });
                        aoData.push({ "name": "middleName", "value": ""+$("#<%=MiddleName.ClientID%>").val()+"" });
                        aoData.push({ "name": "lastName", "value": ""+$("#<%=LastName.ClientID%>").val()+"" });
                        aoData.push({ "name": "facility", "value": ""+$("#<%=Facility.ClientID%>").find(":selected").val()+"" });

                        $.ajax({
                            "dataType": 'json',
                            "contentType": "application/json; charset=utf-8",
                            "type": "POST",
                            "url": sSource,
                            "data": JSON.stringify({dataPayLoad: aoData }),
                            "success": function (msg) {
                                var json = jQuery.parseJSON(msg.d);
                                if (json === "null") {
                                    toastr.error("No records Found", "Patient Records Search");
                                } else {
                                    fnCallback(json);
                                    $("#PatientSearch")
                                        .slideDown("fast",
                                            function() {
                                                $("#infoGrid")
                                                    .slideDown("fast", function() { $("#searchGrid").slideUp("fast") });
                                            });
                                }
                            },
                            "error":function(xhr, errorType, exception) {
                                var jsonError = jQuery.parseJSON(xhr.responseText);
                                toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType, "Patient Finder Method");
                                return false;
                            }
                        });
                    }
                });
                 
             });

            //row selection
          $('#tblFindPatient').on('dblclick', 'tbody tr', function () {
              // window.location.href = $(this).attr('href');
              var patientId = $(this).find('td').first().text();

              window.location.href = "../patient/patientHome.aspx?patient="+patientId;
             // alert(rowIndex);
          });


            // Apply the search
          //table.columns().every(function () {
          //    var that = this;

          //    $('input', this.footer()).on('keyup change', function () {
          //        if (that.search() !== this.value) {
          //            that
          //                .search(this.value)
          //                .draw();
          //        }
          //    });
          //});

          //$("#tblFindPatient tbody tr").on('click', function (event) {
          //    $("#tblFindPatient tbody tr").removeClass('row_selected');
          //    $(this).addClass('row_selected');
          //});

      //      $('#tblFindPatient tbody')
      //.on('mouseenter', 'td', function () {
      //    var colIdx = table.cell(this).index().column;

      //    $(table.cells().nodes()).removeClass('highlight');
      //    $(table.column(colIdx).nodes()).addClass('highlight');
      //});


        });
    </script>
</asp:Content>
