<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ArtDistributionForm.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.ArtDistributionForm" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PharmacyControl" Src="~/CCC/UC/ucPharmacyPrescription.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    </div>

    <div class="col-md-12">
        <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">

            <div class="col-md-12">
                                        <div class="datepicker fuelux form-group" id="PatientVisitDate">
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="PVisitDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                                                        <span data-month="False">January</span>
                                                                        <span data-month="True">February</span>
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
                                                                    </span><span class="year">2017</span>
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
                                                                    <li data-month="False">
                                                                        <button type="button">Jan</button></li>
                                                                    <li data-month="True">
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
            <div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12 form-group">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label for="ArtRefill" class="control-label pull-left">ART Refill Model</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ArtRefill" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" data-parsley-required="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddPharmacy" data-toggle="modal" data-target="#PharmacyModal">Pharmacy </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-8">
                                    <label for="missedDoses" class="control-label pull-left">Any missed doses of ARVs since last clinic visit:</label>
                                </div>
                                <div class="col-md-4">
                                    <label class="pull-left" style="padding-right: 10px">
                                        <input id="mYes" type="radio" name="missedArvDoses" value="True" clientidmode="Static" runat="server" onclick="showHideMissedArv();"/>Yes
                                    </label>
                                    <label class="pull-left" style="padding-right: 10px">
                                        <input id="mNo" type="radio" name="missedArvDoses" value="False" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showHideMissedArv();"/>No
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-8">
                                    <label for="missedDosesCount" class="control-label pull-left">If Yes, how many missed doses:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="missedDosesCount" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12 form-group">
                    <div class="col-md-12 form-group">
                        <div class="col-md-12 form-group">
                            <label class="control-label pull-left text-primary">Any current/worsening symptoms:</label>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Fatigue</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="fatigue" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Fever</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="fever" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Nauesa/Vomitting</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="nausea" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Diarrhea</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="diarrhea" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Cough</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="cough" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Rash</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="rash" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Genital sore/ Discharge</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="genitalSore" ClientIDMode="Static">
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Other</label>
                            </div>
                            <div class="col-md-12">
                                <asp:TextBox runat="server" ID="otherSymptom" CssClass="form-control input-sm" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDoses" class="control-label pull-left">Any new medication prescribed outside the HIV clinic:</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="medYes" type="radio" name="newMedication" value="True" clientidmode="Static" runat="server" onclick="showNewMedicine();"/>Yes
                                        </label>
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="medNo" type="radio" name="newMedication" value="False" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showNewMedicine();"/>No
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDosesCount" class="control-label pull-left">If Yes, specify:</label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="newMedicineText" CssClass="form-control input-sm" ClientIDMode= "Static"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDoses" class="control-label pull-left">Family Planning:</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="fpYes" type="radio" name="familyPlanning" value="True" clientidmode="Static" runat="server" onclick="showFamilyPlanning();"/>Yes
                                        </label>
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="fpNo" type="radio" name="familyPlanning" value="False" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showFamilyPlanning();" />No
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDosesCount" class="control-label pull-left">If Yes, specify:</label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="fpmethod" CssClass="form-control input-sm" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDoses" class="control-label pull-left">Refered to clinic</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="refYes" type="radio" name="referredToClinic" value="True" clientidmode="Static" runat="server" onclick="showAppointmentDate();" />Yes
                                        </label>
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="refNo" type="radio" name="referredToClinic" value="False" clientidmode="Static" runat="server" data-parsley-required="true" onclick="showAppointmentDate();" />No
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDosesCount" class="control-label pull-left">If Yes, AppointmentDate:</label>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="datepicker fuelux form-group" id="PersonAppointmentDate">
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="AppointmentDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
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
                                                                        <span data-month="False">January</span>
                                                                        <span data-month="True">February</span>
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
                                                                    </span><span class="year">2017</span>
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
                                                                    <li data-month="False">
                                                                        <button type="button">Jan</button></li>
                                                                    <li data-month="True">
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
                            </div>
                        </div>
                        <asp:Panel class="col-md-12" runat="server" ID="pregnancySection" ClientIDMode="Static">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="pregnancyStatus" class="control-label pull-left">Pregnancy Status:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="pregnancyStatus" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <hr />
            </div>
            <div class="col-md-12">
                <div class="col-md-3"></div>
                <div class="col-md-9">
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save </asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Form  </asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close </asp:LinkButton>
                    </div>

                </div>
            </div>
            <div class="col-md-12">
                <hr />
            </div>
        </div>
    </div>
    <div class="modal" id="PharmacyModal" tabindex="-True" role="dialog" aria-labelledby="PharmacyLabel" aria-hidden="true" clientidmode="Static">
        <div class="modal-dialog modal-lg" role="document" style="width: 90%">
            <div class="modal-content" style="width: 100%">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Pharmacy</h4>
                </div>
                <div class="modal-body" style="width: 100%">
                    <div class="row">
                        <uc:PharmacyControl ID="pharmacyDetails" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var VisitDate = "<%=VisitDate%>";
        
       /// console.log(VDate);
        var enrollmentDate = "<%=DateOfEnrollment%>";
 
        $('#PatientVisitDate').datepicker({
            allowPastDates: true,
           // restricted: [{ from: tomorrow, to: Infinity }],
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' },
              date:VisitDate,
            allowInputToggle: true,
            useCurrent: false
        });

       
        
       
         
       // $('#PatientVisitDate').on('dp.change', function (e) {
         $('#PatientVisitDate').on('changed.fu.datepicker dateClicked.fu.datepicker', function (event, date) {

            if (!event.oldDate || !event.date.isSame(event.oldDate, 'day')) {
               // $(this).data('DatePicker')
                  $('#PatientVisitDate').datepicker("hide");
                resetFields();

            }
        

            var vDate = moment($("#PVisitDate").val(), 'DD-MMM-YYYYY').toDate();
            var validDateOfVisit = moment(vDate).isBefore(enrollmentDate);
            var futuredate = moment(vDate).isAfter(new Date());
           
            if (futuredate) {
                $("#<%=PVisitDate.ClientID%>").val('');
                toastr.error("Future dates not allowed!");
               
                return false;
            }
            if (validDateOfVisit) {
                toastr.error("VISIT date CANNOT be before ENROLLMENT date");
                $("#<%=PVisitDate.ClientID%>").val('');
                return false;
            }

        });

        $('#PersonAppointmentDate').datepicker({
            allowPastDates: false,
            Date: 0,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        });

        $(document).ready(function () {

            $("#<%=missedDosesCount.ClientID%>").prop('disabled', true);
            $("#<%=newMedicineText.ClientID%>").prop('disabled', true);
            $("#<%=fpmethod.ClientID%>").prop('disabled', true);
            $("#<%=AppointmentDate.ClientID%>").prop('disabled', true);
            $("#PersonAppointmentDate").addClass('noneevents');
            var IsPatientArtDistributionDone = <%=IsPatientArtDistributionDone%>;

            if (IsPatientArtDistributionDone == 1) {
                getPatientArtDistribution();
            }

            var pregnancyStatus = $("#<%=pregnancyStatus.ClientID%>").val();
            if (parseInt(pregnancyStatus, 10) < 1) {
                getLastPregnancyIndicator();
            }

            getPregnancyStatus();



            $("#btnSave").click(function () {
                if (Validate() == true) {
                    var visitdate = $("#<%=PVisitDate.ClientID%>").val();
                    //addArtDistribution()
                    addARTTrack(visitdate);
                }
                else {
                    return false;
                }

                $("#AppointmentDate").val("");

                $("#btnReset").click(function () {
                    resetFields();
                });
                $("#btnCancel").click(function () {
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
                });


            });

        });
        function addARTTrack(visitDate ) {
        var patientId = <%=PatientId%>;
     var dateOfVisit = $("#PVisitDate").val();
        var ServiceAreaId = <%=serviceAreaId%>;
        var EncounterType = "ARTFastTrack";
        var userId = <%=userId%>;
        var patientMasterVisitId = <%=PatientMasterVisitId%>;
            $.ajax({
               
                type: "POST",
                url: "../WebService/PatientScreeningService.asmx/GetPatientMasterVisitId",
                data: "{'PatientId': '" + patientId + "','ServiceAreaId':'"+ServiceAreaId+"','UserId':'"+userId+"','EncounterType':'"+EncounterType+"','visitDate': '" + dateOfVisit + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
             success: function (response) {
                
                 var res = JSON.parse(response.d);
                 if (res.Result > 0) {

                     var result = res.Result;
                   
                     toastr.success(res.Msg);
                    
                    
                     addArtDistribution(result);
                   }

                },
                error: function (response) {
                    error = 1;
                    toastr.error("ART Fast Track Was not  Saved");
                    window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';


                }
           });
        }


        function Validate() {
            if ($('#PatientVisitDate').parsley().validate()) {
                var visitdate = $("#PVisitDate").val();
                if (moment('' + visitdate + '').isAfter()) {
                    toastr.error("Visit date cannot be a future date.");
                    return false;
                }
                else if (visitdate === "" || visitdate === null) {
                    toastr.error("VisitDate is a required field");
                    return false;
                }

                else if ($('#AppointmentForm').parsley().validate()) {
                    var futureDate = moment().add(7, 'months').format('DD-MMM-YYYY');
                    var appDate = $("#<%=AppointmentDate.ClientID%>").val();
                    if (moment('' + appDate + '').isAfter(futureDate)) {
                        toastr.error("Appointment date cannot be set to over 7 months");
                        return false;
                    }
                    else {
                        return true;
                    }

                }
                else {
                    return true;
                }

            }


          }
                
          
        function getPatientArtDistribution() {
            var patientMasterVisitId = <%=PatientMasterVisitId%>;

            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/GetArtDistributionForVisitDate",
                data: "{ 'pmvisitid': '" + patientMasterVisitId+ "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var artDetails = JSON.parse(response.d);
                    console.log(artDetails);
                    if (artDetails != null) {
                        $("#<%=ArtRefill.ClientID%>").val(artDetails.ArtRefillModel);
                        if (artDetails.MissedArvDosesCount > 0) {
                            $("#<%=missedDosesCount.ClientID%>").val(artDetails.MissedArvDosesCount);
                        }

                        if (artDetails.MissedArvDoses) {
                            $("#mYes").prop("checked", true);
                        } else {
                            $("#mNo").prop("checked", true);
                        }

                        var genitalSore = artDetails.GenitalSore ? "True" : "False";
                        var fatigue = artDetails.Fatigue ? "True" : "False";
                        var fever = artDetails.Fever ? "True" : "False";
                        var nausea = artDetails.Nausea ? "True" : "False";
                        var diarrhea = artDetails.Diarrhea ? "True" : "False";
                        var cough = artDetails.Cough ? "True" : "False";
                        var rash = artDetails.Rash ? "True" : "False";

                        $("#<%=fatigue.ClientID%>").val(fatigue);
                        $("#<%=fever.ClientID%>").val(fever);
                        $("#<%=nausea.ClientID%>").val(nausea);
                        $("#<%=diarrhea.ClientID%>").val(diarrhea);
                        $("#<%=cough.ClientID%>").val(cough);
                        $("#<%=rash.ClientID%>").val(rash);
                        $("#<%=genitalSore.ClientID%>").val(genitalSore);
                        $("#<%=otherSymptom.ClientID%>").val(artDetails.OtherSymptom);

                        if (artDetails.NewMedication) {
                            $("#medYes").prop("checked", true);
                        } else {
                            $("#medNo").prop("checked", true);
                        }

                        if (artDetails.FamilyPlanning) {
                            $("#fpYes").prop("checked", true);
                        } else {
                            $("#fpNo").prop("checked", true);
                        }

                        if (artDetails.ReferedToClinic) {
                            $("#refYes").prop("checked", true);
                        } else {
                            $("#refNo").prop("checked", true);
                        }

                        $("#<%=newMedicineText.ClientID%>").val(artDetails.NewMedicationText);
                        $("#<%=fpmethod.ClientID%>").val(artDetails.FamilyPlanningMethod);
                        $("#<%=pregnancyStatus.ClientID%>").val(artDetails.PregnancyStatus);
                        if (artDetails.DateReferedToClinic != null) {
                            if (artDetails.DateReferedToClinic != "") {
                                $('#PersonAppointmentDate').datepicker('setDate', artDetails.DateReferedToClinic);
                            }
                            else {
                                     $("#AppointmentDate").val("");
                            }

                        }
                        else {
                            $("#AppointmentDate").val("");
                        }
                         $('#PatientVisitDate').datepicker("setDate", new Date(VisitDate) );
                    }
                },
                error: function (response) {
                    toastr.error(response.d, "fast track not saved");
                }
            });
        }

        function addArtDistribution(mastervisitid) {
            var artRefillModel = $("#<%=ArtRefill.ClientID%>").val();
            var missedArvDoses = $("input[name$=missedArvDoses]:checked").val();
            var missedDosesCount = $("#<%=missedDosesCount.ClientID%>").val();
            if (missedDosesCount === "") { missedDosesCount = 0 }
            var fatigue = $("#<%=fatigue.ClientID%>").val();
            var fever = $("#<%=fever.ClientID%>").val();
            var nausea = $("#<%=nausea.ClientID%>").val();
            var diarrhea = $("#<%=diarrhea.ClientID%>").val();
            var cough = $("#<%=rash.ClientID%>").val();
            var rash = $("#<%=rash.ClientID%>").val();
            var genitalSore = $("#<%=genitalSore.ClientID%>").val();
            var otherSymptom = $("#<%=otherSymptom.ClientID%>").val();
            var newMedication = $("input[name$=newMedication]:checked").val();
            var newMedicineText = $("#<%=newMedicineText.ClientID%>").val();
            var familyPlanning = $("input[name$=familyPlanning]:checked").val();
            var fpmethod = $("#<%=fpmethod.ClientID%>").val();
            var referredToClinic = $("input[name$=referredToClinic]:checked").val();
            var appointmentDate = $("#<%=AppointmentDate.ClientID%>").val();
            var pregnancyStatus = $("#<%=pregnancyStatus.ClientID%>").val();
            if (pregnancyStatus === undefined) { pregnancyStatus = 0 }
            var patientId = <%=PatientId%>;
          
            var patientMasterVisitId= mastervisitid;
            var IsPatientArtDistributionDone = <%=IsPatientArtDistributionDone%>;
           

            $.ajax({
                type: "POST",
                url: "../WebService/PatientEncounterService.asmx/AddArtDistribution",
                data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','artRefillModel': '" + artRefillModel + "','missedArvDoses': '" + missedArvDoses +
                "','missedDosesCount': '" + missedDosesCount + "','fatigue': '" + fatigue + "','fever': '" + fever + "','nausea': '" + nausea + "','diarrhea': '" + diarrhea + "','cough': '" + cough + "','rash': '" + rash +
                "','genitalSore': '" + genitalSore + "','otherSymptom': '" + otherSymptom + "','newMedication': '" + newMedication + "','newMedicineText': '" + newMedicineText + "','familyPlanning': '" + familyPlanning +
                "','fpmethod': '" + fpmethod + "','referredToClinic': '" + referredToClinic + "','appointmentDate': '" + appointmentDate + "','pregnancyStatus': '" + pregnancyStatus + "','IsPatientArtDistributionDone':'" + IsPatientArtDistributionDone + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Fast track form saved successfully");
                    resetFields();
                    setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>'; }, 2500);
                },
                error: function (response) {
                    toastr.error(response.d, "fast track not saved");
                }
            });
        }

        function showHideMissedArv() {
            var missedArvs = $("input[name$=missedArvDoses]:checked").val();
            if (missedArvs === "True") {
                $("#<%=missedDosesCount.ClientID%>").prop('disabled', false);
                $("#<%=missedDosesCount.ClientID%>").prop('required', true);
            }
            else {
                $("#<%=missedDosesCount.ClientID%>").prop('disabled', true);
                $("#<%=missedDosesCount.ClientID%>").prop('required', false);
            }
        }

        function showNewMedicine() {
            var missedArvs = $("input[name$=newMedication]:checked").val();
            if (missedArvs === "True") {
                $("#<%=newMedicineText.ClientID%>").prop('disabled', false);
                $("#<%=newMedicineText.ClientID%>").prop('required', true);
            }
            else {
                $("#<%=newMedicineText.ClientID%>").prop('disabled', true);
                $("#<%=newMedicineText.ClientID%>").prop('required', false);
            }
        }

        function showFamilyPlanning() {
            var missedArvs = $("input[name$=familyPlanning]:checked").val();
            if (missedArvs === "True") {
                $("#<%=fpmethod.ClientID%>").prop('disabled', false);
                $("#<%=fpmethod.ClientID%>").prop('required', true);
            }
            else {
                $("#<%=fpmethod.ClientID%>").prop('disabled', true);
                $("#<%=fpmethod.ClientID%>").prop('required', false);
            }
        }

        function showAppointmentDate() {
            var missedArvs = $("input[name$=referredToClinic]:checked").val();
            if (missedArvs === "True") {
                $("#<%=AppointmentDate.ClientID%>").prop('disabled', false);
                $("#PersonAppointmentDate").removeClass('noneevents');
                $("#<%=AppointmentDate.ClientID%>").prop('required', true);
            }
            else {
                $("#<%=AppointmentDate.ClientID%>").prop('disabled', true);
                $("#PersonAppointmentDate").addClass('noneevents');
                $("#<%=AppointmentDate.ClientID%>").prop('required', false);
            }
        }

        function resetFields(parameters) {
            $("#fatigue").val("");
            $("#fever").val("");
            $("#nausea").val("");
            $("#diarrhea").val("");
            $("#cough").val("");
            $("#rash").val("");
            $("#genitalSore").val("");
            $("#otherSymptom").val("");
            $("#ArtRefill").val("");
            $("#missedDosesCount").val("");
            $("#fpMethod").val("");
            $("#otherSymptom").val("");
            $("#ArtRefill").val("");
            $("#missedDosesCount").val("");
            $("#AppointmentDate").val("");
        }

        function getPregnancyStatus() {
            
            $.ajax({
                type: "POST",
                url: "../WebService/PatientVitals.asmx/GetPatientPregnancyOutcomeLookup",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var pregnancyOutcome = response.d;

                    if (pregnancyOutcome != null) {
                        console.log(pregnancyOutcome);
                        if (pregnancyOutcome.Outcome == 0 || pregnancyOutcome.OutcomeStatus == "Unknown")
                            $("#<%=pregnancyStatus.ClientID%>").val(pregnancyOutcome.PregnancyStatusId);
                         
                        
                    }
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    return false;
                }
            });
        }


        function getLastPregnancyIndicator()
        {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientVitals.asmx/GetLastPatientPregnancyIndicator",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var pregnancyIndicator = response.d;

                    if (pregnancyIndicator != null) {
                        console.log(pregnancyIndicator);
                        if (pregnancyIndicator > 0) {
                            $("#<%=pregnancyStatus.ClientID%>").val(pregnancyIndicator);
                        }
                        }
                },
                error: function (xhr, errorType, exception) {
                    var jsonError = jQuery.parseJSON(xhr.responseText);
                    toastr.error("" + xhr.status + "" + jsonError.Message + " " + jsonError.StackTrace + " " + jsonError.ExceptionType);
                    return false;
                }
            });

        }

    </script>

</asp:Content>
