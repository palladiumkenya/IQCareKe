<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientLabs.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientLabs" %>
<div class="col-md-12">  
                        <div class="col-md-6">  
                         <div class="col-md-12 bs-callout bs-callout-danger">
                                <h4 class="pull-left"> <strong>Pending Labs:</strong> </h4> 
                             <div class="col-md-12 form-group ">
                              <div id ="tblPendingLabsScrollable" style="overflow: scroll; height:150px;"">                          
                                <table class="table table-striped table-condensed" id="tblPendingLabs" clientidmode="Static" runat="server">
                                    
                                                           <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">Lab Test</span></th>
                                                                    <th><span class="text-primary">Order Reason</span></th>
                                                                    <th><span class="text-primary">Order Date</span></th>
                                                                    <th><span class="text-primary">Status</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                    
                                                       <tbody>                        
                                                  </tbody>                  
                                                </table>
                                             </div>
                                         <div class="col-md-3 pull-right ">
                                      <asp:LinkButton runat="server" ID="addResults" ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle "> Add Results</asp:LinkButton>

                                        </div> 

                            </div>    
                            </div>    
        
                

                <div class="col-md-12 bs-callout bs-callout-info">
                         <h4 class="pull-left"> <strong>Complete Labs:</strong> </h4>    
                      <!--pw implementation of previous labs laboratory module here  previous orders-->
                                        
                                        <div class="col-md-12 form-group ">
                                               <div id ="tblCompleteLabsScrollable" style="overflow: scroll; height:200px;"">
                                              <table class="table table-striped table-condensed" id="tblPrevLabs" clientidmode="Static" runat="server">                                               
                                                   <thead>
                                                                <tr>
                                                                    <th><span class="text-primary">#</span></th>
                                                                    <th><span class="text-primary">Lab Test</span></th>
                                                                    <th><span class="text-primary">Order Reason</span></th>
                                                                    <th><span class="text-primary">Order Date</span></th>
                                                                    <th><span class="text-primary">Results</span></th>
                                                                    
                                                                </tr>
                                                            </thead>
                                                  
                                                        <tbody> 
                                                            
                                                     </tbody>                       
                                                </table>
                                         </div>

                               </div>
                    </div>

                       </div>
                           
                       <div class="col-md-6">
                         <div class="col-md-12">
                                        <div class="col-md-12"><label class="control-label pull-left">Order Lab Test(s)</label></div>
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                              <div class="panel-heading"></div>
                                              <div class="panel-body">
                                               
                                                  <div class="col-md-12 form-group">
                                                       <div class="col-md-4"><label class="control-label pull-left">Select Lab</label></div>
                                                      <div class="col-md-8">
                                                         
                                                          <asp:TextBox runat="server" Width="100%" ID="labTestTypes" data-provide="typeahead" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="type to select...."></asp:TextBox>
                                                          
                                                      </div>
                                                  </div>
                                                  <div class="col-md-12 form-group">
                                                                      <div class="col-md-4"><label class="control-label  pull-left">Reason</label></div>
                                                                     <div class="col-md-8">
                                                                         <asp:DropDownList runat="server" ID="orderReason" CssClass="form-control input-sm" ClientIDMode="Static"/>
                                                                     </div>
                                                         </div>
                                                  
                                                 <div class="col-md-12 form-group">
                                                       <div class="col-md-4"><label class="control-label pull-left">Lab Test Notes</label></div>
                                                      <div class="col-md-8">
                                                         
                                                          <asp:TextBox runat="server" ID="labNotes" Rows="4" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="laboratory test notes...."></asp:TextBox>
                                                      </div>
                                                  </div>                         



                                    <div class="col-md-12">
                                        <div class="col-md-10"></div>
                                        <div class="col-md-3 pull-right ">
                                            <asp:LinkButton runat="server" ID="btnAddLab" ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle "> Add Lab</asp:LinkButton>

                                        </div>
                                        <div></div>
                                    </div>
                                <div class="col-md-12 bs-callout bs-callout-info">
                                    <div class="col-md-12 form-group">
                                        <table class="table table-striped table-condensed" id="tblAddLabs" clientidmode="Static" runat="server">
                                            <thead>

                                                <tr>
                                                    <th><i class="control-label text-warning pull-left" aria-hidden="true"># </i></th>
                                                    <th><i class="control-label text-warning pull-left" aria-hidden="true">Lab Test</i> </th>
                                                    <th><i class="control-label text-warning pull-left " aria-hidden="true">Order Reason</i> </th>
                                                    

                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                    </div>
                                <!--here  -->                                        
                                     <!--datepicker  -->
                              
                                   <div class="col-md-12 form-group">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left">Date</label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="datepicker fuelux form-group" id="LabDatePicker">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="LabDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                                                        </span><span class="year">2014</span>
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
                                                                        <li data-month="0">
                                                                            <button type="button">Jan</button></li>
                                                                        <li data-month="1">
                                                                            <button type="button">Feb</button></li>
                                                                        <li data-month="2">
                                                                            <button type="button">Mar</button></li>
                                                                        <li data-month="3">
                                                                            <button type="button">Apr</button></li>
                                                                        <li data-month="4">
                                                                            <button type="button">May</button></li>
                                                                        <li data-month="5">
                                                                            <button type="button">Jun</button></li>
                                                                        <li data-month="6">
                                                                            <button type="button">Jul</button></li>
                                                                        <li data-month="7">
                                                                            <button type="button">Aug</button></li>
                                                                        <li data-month="8">
                                                                            <button type="button">Sep</button></li>
                                                                        <li data-month="9">
                                                                            <button type="button">Oct</button></li>
                                                                        <li data-month="10">
                                                                            <button type="button">Nov</button></li>
                                                                        <li data-month="11">
                                                                            <button type="button">Dec</button></li>
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
                                             <div class="col-md-12 form-group">
                                                       <div class="col-md-4"><label class="control-label pull-left">Clinical Order Notes</label></div>
                                                      <div class="col-md-8">
                                                         
                                                          <asp:TextBox runat="server" ID="orderNotes" Rows="4" CssClass="form-control input-sm pull-right" ClientIDMode="Static" placeholder="laboratory order notes...."></asp:TextBox>
                                                      </div>
                                                  </div>
                                <!-- to here  -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
    </div>
                <div class="col-md-12">
                    <hr />
                </div>
<div class="col-md-12">
                <div class="col-md-7"></div>
                <div class="col-md-5">
                    <div class="col-md-3">

                        <asp:LinkButton runat="server" ID="btnSaveLab" OnClientClick="return false" CssClass="btn btn-info fa fa-plus-circle" ClientIDMode="Static"> Save Order</asp:LinkButton>
                    </div>                  
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnResetOrder" OnClientClick="return false" CssClass="btn btn-warning fa fa-refresh" ClientIDMode="Static"> Reset Order</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                         <button type="button" Class="btn btn-danger btn-sm  fa fa-times" data-dismiss="modal">Close Lab Order</button>
                        <%--<asp:LinkButton runat="server" ID="btnCancelOrder" OnClientClick="return false" CssClass="btn btn-danger fa fa-times" ClientIDMode="Static"> Cancel Order</asp:LinkButton>--%>
                      <%-- <asp:LinkButton runat="server" ID="btnClose" OnClientClick="return false" Class="btn btn-danger btn-sm  fa fa-times" data-dismiss="modal" ClientIDMode="Static">Close Lab Order</asp:LinkButton>--%>
                    </div>
                </div>

                <%--</div>--%>
            </div>
 <!-- ajax begin -->
    <script type="text/javascript">
        var patientId = '<%=PatientId%>';
        var patientMasterVisitId = '<%=PatientMasterVisitId%>';    
        var ptn_pk = '<%=Ptn_pk%>';        
        var facilityId = '<%=AppLocationId%>';
        var moduleId = '<%=ModuleId%>';
        var enrollmentDate = '<%=EnrollmentDate%>';
      
      
        $(document).ready(function () {           


            $("#LabDatePicker").datepicker({
                //date: null,
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });
            var LabOrderList = new Array();
        
            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetLookupPreviousLabsList",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    //console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                      "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];    
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;

                        var resultValues = itemList.ResultValues;
                        var resultTexts = itemList.ResultTexts;
                        var resultUnits = itemList.ResultUnits;

                        var labResults;

                        //if (itemList.LabTestId == 1 || itemList.LabTestId == 3 && itemList.ResultValues == 0) {
                        //    labResults = "LDL";
                        //}
                        if (resultUnits == null) {
                            resultUnits = "";
                        } else {
                            resultUnits = resultUnits;

                        }
                        if (resultTexts == null) {
                            labResults = resultValues;
                        } else {
                            labResults = resultTexts;

                        }
                          
                        table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + labResults + " " + resultUnits + '</td></tr>';
                   
                    });
                               
                    $("#tblPrevLabs td").parent().remove();
                    $('#tblPrevLabs').append(table);
                    $('#tblPrevLabs tr:not(:first-child').each(function(idx){
                        $(this).children(":eq(0)").html(idx + 1);
                    });
                    $('#tblCompleteLabsScrollable').append(tblPrevLabs);
                    $('#tblCompleteLabsScrollable').scroll();
                   
                    },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });

            $.ajax({
                type: "POST",
                url: "../WebService/LabService.asmx/GetLookupPendingLabsList",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                   // console.log(response.d);
                    var itemList = JSON.parse(response.d);
                    var table = '';
                    //itemList.forEach(function (item) {
                    $.each(itemList, function (index, itemList) {

                        var dateString = itemList.SampleDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var monthNames = [ "Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                     "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ];                       
                        var month = monthNames[currentTime.getMonth()];                        
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var sampleDate = day + "-" + month + "-" + year;
                        // alert(date);
                        
                            table += '<tr><td></td><td>' + itemList.LabName + '</td><td>' + itemList.Reasons + '</td><td>' + sampleDate + '</td><td>' + itemList.Results + '</td></tr>';
                     
                    });

                    $("#tblPendingLabs td").parent().remove();
                    $('#tblPendingLabs').append(table);
                    $('#tblPendingLabs tr:not(:first-child').each(function(idx){
                    $(this).children(":eq(0)").html(idx + 1);
                    });
                    $('#tblPendingLabsScrollable').append(tblPendingLabs);
                    $('#tblPendingLabsScrollable').scroll();
                },

                error: function (msg) {

                    alert(msg.responseText);
                }
            });          

            var labtests = [];
            var input = document.getElementById("labTestTypes");
            var awesomplete = new Awesomplete(input, {
                minChars: 1,
                autoFirst: true
            });

            $("input").on("keyup", function () {
                $.ajax({
                    url: '../WebService/LookupService.asmx/GetLookupLabsList',
                    type: 'POST',
                    dataType: 'json',
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
           
                    success: function (data) {
                     var serverData = JSON.parse(data.d);
                        //console.log(serverData);
                        
                        var obj = [];
                        obj[key] = val;

                        //labtests.push(obj);
                        
                        for (var i = 0; i < serverData.length; i++) {
                            var key = parseInt(serverData[i]["Id"]);
                            var val = serverData[i]["Name"];

                            labtests[key] = val;
                        }

                        console.log(labtests);
                        awesomplete.list = labtests;
                    }
                });
           
            });
      
      
            function val2key (val, array){
                for(var key in array){
                    var this_val = array[key];
                    if(this_val == val){
                        return key;
                        break;
                    }
                }
            }
            // Load lab order 
           
            $("#btnAddLab").click(function (e) {

                var labOrderFound = 0;
                var labName = $("#labTestTypes").val();
                var labNameId=val2key(labName, labtests);              
                var labOrderReason = $("#orderReason").find(":selected").text();
                var labOrderNotes = $("#labNotes").val();

                if (labName < 1) {
                    toastr.error("Please select at least One(1) Lab Type from the List");
                    return false;
                }
                if (labOrderReason < 1) {
                    toastr.error("Please select at least One(1) Lab Order Reason from the List");
                    return false;
                }

                labOrderFound = $.inArray("" + labName + "", LabOrderList);

                if (labOrderFound > -1) {

                    toastr.error("error", labName + " Lab selected already exists in the List");
                    return false; // message box herer
                }
               

                else {

                  
                    LabOrderList.push("" + labName + "");
                    var tr = "<tr><td></td><td visibility: hidden>" + labNameId + "</td><td align='left'>" + labName + "</td><td align='left'>" + labOrderReason + "</td><td visibility: hidden>" + labOrderNotes + "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                    $("#tblAddLabs>tbody:first").append('' + tr + '');
                    resetLabOrder();

                    $('#tblAddLabs tr:not(:first-child').each(function(idx){
                        $(this).children("td:eq(0)").html(idx + 1);
                    });
                    
                }

                e.preventDefault();
            });

            $("#tblAddLabs").on('click', '.btnDelete', function () {
                $(this).closest('tr').remove();
                var x = $(this).closest('tr').find('td').eq(0).html();
                LabOrderList=[];
               //Allows reentry of removed test
               
            });
       
            $("#btnCancelOrder").click(function (e) {
                $("#tblAddLabs td").parent().remove();
            });
      
            $("#btnResetOrder").click(function (e) {   
                resetLabOrder();
            });

            <%--$("#btnClose").click(function (e) {
               window.location.href = '<%=ResolveClientUrl("../Patient/patientHome.aspx") %>';
            });--%>
            function resetLabOrder(parameters) {
                $("#labTestTypes").val("");
                $("#orderReason").val("");
                $("#labNotes").val("");              
            }

            function getFormattedDate(t_sdate) {
                var sptdate = String(t_sdate).split("-");
                var months = {};
                months["Jan"] = "1";
                months["Feb"] = "2";
                months["Mar"] = "3";
                months["Apr"] = "4";
                months["May"] = "5";
                months["Jun"] = "6";
                months["Jul"] = "7";
                months["Aug"] = "8";
                months["Sep"] = "9";
                months["Oct"] = "10";
                months["Nov"] = "11";
                months["Dec"] = "12";

                var myMonth = sptdate[1];
                var myDay = sptdate[0];
                var myYear = sptdate[2];
                var converted = months[myMonth];               
                var combineDatestr = myDay + "/" + converted + "/" + myYear + " " + "00" + ":" + "00" + ":" + "00" + " " + "AM";
                return combineDatestr;
            }
        

            // Save lab order
            $("#btnSaveLab").click(function (e) {
                var labOrderDate = $("#<%=LabDate.ClientID%>").val();   
                var orderNotes = $("#orderNotes").val();
                var _fp = [];
                var table = "";
                var labDate = getFormattedDate(labOrderDate);
              //using momentjs
               enrollmentDate = moment(enrollmentDate);
               var isBeforeEnrollmentDate = moment(labDate).isBefore(enrollmentDate);        

              
                var data = $('#tblAddLabs tr').each(function (row, tr) {
                    _fp[row] = {
                                "labNameId": $(tr).find('td:eq(1)').text()
                               , "labName": $(tr).find('td:eq(2)').text()
                              , "orderReason": $(tr).find('td:eq(3)').text()                             
                             , "labNotes": $(tr).find('td:eq(4)').text()

                            }                  
                    
                });
                _fp.shift();

                if ($.isEmptyObject(_fp)) {
                    toastr.error("You have not added any lab order");
                 
                    return false;
                } if (isBeforeEnrollmentDate) {
                   
                    toastr.error("You cannot make a lab order before enrollment. Kindly enroll to IQCare first");

                    return false;
                }
                else {
                  
                    addLabOrder(_fp,labOrderDate,orderNotes);       
                  
                                      
                    $('#tblAddLabs tr:first').remove();
                    var data = $('#tblAddLabs tr').each(function (row, tr) {
                        _fp[row] = {
                                "labNameId": $(tr).find('td:eq(1)').text()
                               , "labName": $(tr).find('td:eq(2)').text()
                              , "orderReason": $(tr).find('td:eq(3)').text()                            
                             , "labNotes": $(tr).find('td:eq(4)').text()

                        }                      
                    
                        table ="<tr><td></td><td>" + $(tr).find('td:eq(2)').text() + "</td><td>" + $(tr).find('td:eq(3)').text() + "</td><td>" +labOrderDate+ "</td><td>" + "Pending" + "</td></tr>";
                        $("#tblPendingLabs>tbody:first").append(table);              

                        $('#tblPendingLabs tr:not(:first-child').each(function(idx){
                            $(this).children("td:eq(0)").html(idx + 1);
                        }); 

                    }); 

                }

              
                $("#tblAddLabs td").parent().remove();
            });

            function addLabOrder(_fp,labOrderDate,orderNotes) {
                var labOrder = JSON.stringify(_fp);
                //console.log(labOrderDate);
                //console.log(orderNotes);
                $.ajax({
                    type: "POST",

                    url: "../WebService/LabService.asmx/AddLabOrder",
                    data: "{'patientPk':'" + ptn_pk + "','patientMasterVisitId':'" + patientMasterVisitId + "','labOrderDate':'" + labOrderDate + "','orderNotes':'" + orderNotes + "','patientLabOrder': '" + labOrder + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        toastr.success(response.d, "Lab order successful");
                    },
                    
                });

            };

            // Load lab results        
            $("#addResults").click(function (e) {
                window.location.href = '<%=ResolveClientUrl("~/laboratory/request/findlaborder.aspx")%>'; 
              
            });           
           
        });

    </script>
