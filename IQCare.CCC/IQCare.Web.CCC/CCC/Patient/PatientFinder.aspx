<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientFinder.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientFinder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
         <div class="col-md-12 bs-callout bs-callout-info">
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
                                   <div class="col-md-12"><label class="control-label pull-left">DoB</label></div>
                                   <div class="col-md-12">
                                        <div class="datepicker fuelux form-group" id="SearchDoB">
                                                                 <div class="input-group">
                                                                              <input class="form-control input-sm" id="SearchDateOfBirth" type="text" />
                                                                              <div class="input-group-btn">
                                                                                 <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
                                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                                                <span class="sr-only">Toggle Calendar</span>
                                                                              </button>
                                                                              <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                                                                <div class="datepicker-calendar">
                                                                                  <div class="datepicker-calendar-header">
                                                                                       <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                                                                       <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                                                                       <button type="button" class="title" data-month="11" data-year="2014">
                                                                                            <span class="month">
                                                                                              <span data-month="0">January</span>
                                                                                              <span data-month="1">February</span>
                                                                                              <span data-month="2">March</span>
                                                                                              <span data-month="3">April</span>
                                                                                              <span data-month="4">May</span>
                                                                                              <span data-month="5">June</span>
                                                                                              <span data-month="6">July</span>
                                                                                              <span data-month="7">August</span>
                                                                                              <span data-month="8">September</span>
                                                                                              <span data-month="9">October</span>
                                                                                              <span data-month="10">November</span>
                                                                                              <span data-month="11" class="current">December</span>
                                                                                            </span> <span class="year">2014</span>
                                                                                       </button>
                                                                                  </div>
                                                                                  <table class="datepicker-calendar-days">
                                                                                    <thead>
                                                                                    <tr>
                                                                                      <th>Su</th>
                                                                                      <th>Mo</th>
                                                                                      <th>Tu</th>
                                                                                      <th>We</th>
                                                                                      <th>Th</th>
                                                                                      <th>Fr</th>
                                                                                      <th>Sa</th>
                                                                                    </tr>
                                                                                    </thead>
                                                                                    <tbody></tbody>
                                                                                  </table>
                                                                                  <div class="datepicker-calendar-footer">
                                                                                    <button type="button" class="datepicker-today">Today</button>
                                                                                  </div>
                                                                                </div>
                                                                                <div class="datepicker-wheels" aria-hidden="true">
                                                                                  <div class="datepicker-wheels-month">
                                                                                    <h2 class="header">Month</h2>
                                                                                    <ul>
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
                                                                                    </ul>
                                                                                  </div>
                                                                                  <div class="datepicker-wheels-year">
                                                                                    <h2 class="header">Year</h2>
                                                                                    <ul></ul>
                                                                                  </div>
                                                                                  <div class="datepicker-wheels-footer clearfix">
                                                                                    <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                                                                    <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                                                                  </div>
                                                                                </div>
                                                                              </div>
                                                                            </div>
                                                                          </div>
                                       </div>
                                   </div>
                              </div>
                                         
                              <div class="col-md-3">
                                   <div class="col-md-12"><label class="control-label pull-left">Gender</label></div>
                                   <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="Sex" ClientIDMode="Static" CssClass="form-control input-sm"/>
                                   </div>
                             </div>
                             
                              <div class="col-md-3">
                                   <div class="col-md-12"><label class="control-label pull-left">Facility</label></div>
                                   <div class="col-md-12">
                                        <asp:DropDownList runat="server" ID="Facility" ClientIDMode="Static" CssClass="form-control input-sm"/>                
                                   </div>
                             </div>
                              
                              <div class="col-md-3">
                                  <div class="col-md-12"><label class="control-label pull-left">Registration Date</label></div>
                                  <div class="col-md-12 datepicker fuelux form-group" id="RegDate">
                                                                 <div class="input-group">
                                                                              <input class="form-control input-sm" id="RegistrationDate" type="text" />
                                                                              <div class="input-group-btn">
                                                                                 <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown">
                                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                                                <span class="sr-only">Toggle Calendar</span>
                                                                              </button>
                                                                              <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                                                                <div class="datepicker-calendar">
                                                                                  <div class="datepicker-calendar-header">
                                                                                       <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                                                                       <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                                                                       <button type="button" class="title" data-month="11" data-year="2014">
                                                                                            <span class="month">
                                                                                              <span data-month="0">January</span>
                                                                                              <span data-month="1">February</span>
                                                                                              <span data-month="2">March</span>
                                                                                              <span data-month="3">April</span>
                                                                                              <span data-month="4">May</span>
                                                                                              <span data-month="5">June</span>
                                                                                              <span data-month="6">July</span>
                                                                                              <span data-month="7">August</span>
                                                                                              <span data-month="8">September</span>
                                                                                              <span data-month="9">October</span>
                                                                                              <span data-month="10">November</span>
                                                                                              <span data-month="11" class="current">December</span>
                                                                                            </span> <span class="year">2014</span>
                                                                                       </button>
                                                                                  </div>
                                                                                  <table class="datepicker-calendar-days">
                                                                                    <thead>
                                                                                    <tr>
                                                                                      <th>Su</th>
                                                                                      <th>Mo</th>
                                                                                      <th>Tu</th>
                                                                                      <th>We</th>
                                                                                      <th>Th</th>
                                                                                      <th>Fr</th>
                                                                                      <th>Sa</th>
                                                                                    </tr>
                                                                                    </thead>
                                                                                    <tbody></tbody>
                                                                                  </table>
                                                                                  <div class="datepicker-calendar-footer">
                                                                                    <button type="button" class="datepicker-today">Today</button>
                                                                                  </div>
                                                                                </div>
                                                                                <div class="datepicker-wheels" aria-hidden="true">
                                                                                  <div class="datepicker-wheels-month">
                                                                                    <h2 class="header">Month</h2>
                                                                                    <ul>
                                                                                      <li data-month="0"><button type="button">Jan</button></li>
                                                                                      <li data-month="1"><button type="button">Feb</button></li>
                                                                                      <li data-month="2"><button type="button">Mar</button></li>
                                                                                      <li data-month="3"><button type="button">Apr</button></li>
                                                                                      <li data-month="4"><button type="button">May</button></li>
                                                                                      <li data-month="5"><button type="button">Jun</button></li>
                                                                                      <li data-month="6"><button type="button">Jul</button></li>
                                                                                      <li data-month="7"><button type="button">Aug</button></li>
                                                                                      <li data-month="8"><button type="button">Sep</button></li>
                                                                                      <li data-month="9"><button type="button">Oct</button></li>
                                                                                      <li data-month="10"><button type="button">Nov</button></li>
                                                                                      <li data-month="11"><button type="button">Dec</button></li>
                                                                                    </ul>
                                                                                  </div>
                                                                                  <div class="datepicker-wheels-year">
                                                                                    <h2 class="header">Year</h2>
                                                                                    <ul></ul>
                                                                                  </div>
                                                                                  <div class="datepicker-wheels-footer clearfix">
                                                                                    <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                                                                    <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                                                                  </div>
                                                                                </div>
                                                                              </div>
                                                                            </div>
                                                                          </div>
                                       </div>
                              </div>

                         </div><%-- .col-md-12--%>
              <div class="col-md-12"><hr/></div>
              <div class="col-md-12">
                              <div class="col-md-4"></div>
                              <div class="col-md-4"></div> 
                              <div class="col-md-4">
                                   <div class="col-md-4">
                                        <asp:LinkButton runat="server" ID="btnFindPatient" OnClientClick="return false" ClientIDMode="Static" CssClass="btn btn-info btn-lg fa fa-search fa-1x"> Find Patient</asp:LinkButton>
                                    </div>
                                   <div class="col-md-4">
                                        <asp:LinkButton runat="server" ID="btnReset" OnClientClick="return false" ClientIDMode="Static" CssClass="btn btn-warning btn-lg fa fa-refresh"> Reset Find</asp:LinkButton>

                                  </div>
                                   <div class="col-md-4">
                                        <asp:LinkButton runat="server" ClientIDMode="Static" OnClientClick="return false" ID="btnClose" CssClass="btn btn-danger btn-lg fa fa-times"> Close Find </asp:LinkButton>
                                   </div>

                                  
                              </div> 
                          </div>
         </div> 
         <div class="col-md-12 form-group" id="PatientSearch">
      
             <table id="tblFindPatient" class="display">
                        <thead>
                          <tr>
      	                    <th>Enrollment No.#</th>
                            <th>Patient Index</th>
      	                    <th>First Name</th>
      	                    <th>Middle Name</th>
      	                    <th>Last Name</th>
      	                    <th>Date of Birth</th>
      	                    <th>Sex</th>
      	                    <th>Registration Date</th>
                            <th>Patient Status</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
      	                    <td align="left">jane</td>
      	                    <td align="left">doe</td>
      	                    <td align="left">demo</td>
      	                    <td align="left">demo</td>
      	                    <td align="left">mm/dd/yyy</td>
      	                    <td align="left">demo</td>
      	                    <td align="left">mm/dd/yyy</td>
      	                    <td align="left">demo</td>
                              <td align="left">demo</td>
                          </tr>
                          <tr>
      	                    <td>demo</td>
      	                    <td>demo</td>
      	                    <td>demo</td>
      	                    <td>demo</td>
      	                    <td>mm/dd/yyy</td>
      	                    <td>demo</td>
      	                    <td>mm/dd/yyy</td>
      	                    <td>demo</td>
                              <td>demo</td>
                          </tr>
                        </tbody>
                    </table>
                </div>
            
    </div><%--.col-md-12--%>
    
    <script type="text/javascript">
        $(document).ready(function() {

            $.ajaxSetup({
                cache: false
            });

            $("#PatientSearch").hide();
            $("#SearchDoB")
                .datepicker({ allowPastDates: true, momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' } });
            $("#RegDate").datepicker({ allowPastDates: true, momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' } });

            //$('#tblFindPatient tfoot th').each(function () {
            //    var title = $(this).text();
            //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
            //});

            $("#btnFindPatient").click(function(e) {

                var table = $("#tblFindPatient").dataTable({
                    bAutoWidth: false,
                    bDestroy: true,
                    bJQuieryUI: true,
                    //"columns": [
                    //    { "data": "" },
                    //    { "render": "[,].names" },
                    //    { "data": "sex" },
                    //    { "data": "dob" },
                    //    { "data": "type" },
                    //    { "data": "subcounty" },
                    //    { "data": "action" }
                    //],
                    //"aoColumns": [
                    //  { "targets": 0, "bSortable": false, "mRender": function () { return '<label class=\"pos-rel\"><input type=\"checkbox\" class=\"ace\" /><span class=\"lbl\"></span></label>' } },
                    //  { "targets": 1, "bSearchable": true, "bSortable": true, "mData": 1},
                    //  { "targets": 2, "mData": 2, "sTitle": "Houhosehold Number" },
                    //  { "targets": 3, "mData": 3, "sTitle": "Gender" },
                    //  { "targets": 4, "mData": 4, "sTitle": "Date of Birth" },
                    //  { "targets": 5, "mData": 5, "sTitle": "Person Type" },
                    //  { "targets": 6, "mData": 6, "sTitle": "Sub-County" },
                    //  {
                    //      "targets": 7, "sTitle": "<span class='fa fa-cog'> Action </span>", "bSortable": false, "mData": 7, "mRender": function (data, type, full) {
                    //          var BaseURL = '#page/portal.php';
                    //          var PersonId = full[7];
                    //          var url = BaseURL + '' + PersonId;

                    //          var str = PersonId.substring(10);
                    //          var i = str.indexOf('&');
                    //          var Pid = str.substring(0, i);
                    //          //var editUrl="\javascript:InitiateEdit("+personId+"\)";
                    //          //var durl='javascript\:\InitiateEdit(\''+PersonId+'\);\'';
                    //          //var Id=personId.substring(10);
                    //          // var durl= "\<a href=\"javascript:InitiateEdit("+Pid+");\">"
                    //          // return '<a href="' + url + '">Load Person</a>';
                    //          //<a href="'+url+'"><span class=\'fa fa-pencil-square-o\'>update</span></a>
                    //          return '<a href="' + url + '" style=\'margin-right:4%\'><span class=\'fa fa-male\'> Fetch Portal</span></a> '
                    //      }
                    //  }
                    //],
                    "aaSorting": [],

                    "bProcessing": true,
                    "bServerSide": true,
                    /* "ajax":{
                         url:"",
                         type:"get",
                         error:function(data){
                             $(".text-danger").html('');
                              $("#tblPerson").append('<tbody class="text-danger"><tr><th colspan="3">No data found in the server</th></tr></tbody>');
                              $("#tblPerson").css("display","none");
 
                         }
                     },*/
                    "sAjaxSource": "../WebService/PatientLookupService.asmx/GetPatientSearch",
                    "sServerMethod": "POST",
                    //"bDestroy":true,
                    //"bautoWidth": true,

                    ////"bProcessing": true,
                    //"bserverSide": true,
                    //"ajax": {
                    //    url: "../WebService/PatientLookupService.asmx/GetPatientSearch",
                    //    type: "Post",
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    data: {},
                    //    success:function(response) {
                    //        //var json = $.ParseJSON(response.d);
                    //       // $.fnCallback(response.d);
                    //    },
                    //    error: function () {

                    //    }
                    //},
                    //"ScrollCollapse": true,
                    //"info": true,
                    //"select": {
                    //    style: 'single'
                    //},
                    //"responsive": true,
                    //"stateSave": true,
                    "bPaginate": true,
                    "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]]
                });

                $("#PatientSearch").slideDown();
                $("div.toolbar").html('<b>Patient Search Result(s)</b>');
                e.preventDefault();
            });



            //row selection
          //$('#tblFindPatient').on('click', 'tbody tr', function () {
          //    window.location.href = $(this).attr('href');
          //});

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
