<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ArtDistributionForm.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.ArtDistributionForm" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PharmacyControl" Src="~/CCC/UC/ucPharmacyPrescription.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    </div>

    <div class="col-md-12">
        <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">
            <div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12 form-group">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label for="ArtRefill" class="control-label pull-left">ART Refill Model</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="ArtRefill" CssClass="form-control input-sm" ClientIDMode="Static" required="true" />
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
                                        <input id="mYes" type="radio" name="missedArvDoses" value="1" clientidmode="Static" runat="server" />Yes
                                    </label>
                                    <label class="pull-left" style="padding-right: 10px">
                                        <input id="mNo" type="radio" name="missedArvDoses" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
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
                                            <input id="medYes" type="radio" name="newMedication" value="1" clientidmode="Static" runat="server" />Yes
                                        </label>
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="medNo" type="radio" name="newMedication" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
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
                                        <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1" />
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
                                            <input id="fpYes" type="radio" name="familyPlanning" value="1" clientidmode="Static" runat="server" />Yes
                                        </label>
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="fpNo" type="radio" name="familyPlanning" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
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
                                        <asp:TextBox runat="server" ID="fpmethod" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1" />
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
                                            <input id="refYes" type="radio" name="referredToClinic" value="1" clientidmode="Static" runat="server" />Yes
                                        </label>
                                        <label class="pull-left" style="padding-right: 10px">
                                            <input id="refNo" type="radio" name="referredToClinic" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
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
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label for="missedDoses" class="control-label pull-left">Pregnancy Status:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="pregnancyStatus" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
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
    <div class="modal" id="PharmacyModal" tabindex="-1" role="dialog" aria-labelledby="PharmacyLabel" aria-hidden="true" clientidmode="Static">
        <div class="modal-dialog modal-lg" role="document">
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
        $('#PersonAppointmentDate').datepicker({
            allowPastDates: false,
            Date: 0,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        });
        $(document).ready(function () {
            $("#AppointmentDate").val("");

            $("#btnReset").click(function () {
                resetFields();
            });
            $("#btnCancel").click(function () {
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';
            });
        });

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
        }

    </script>

</asp:Content>
