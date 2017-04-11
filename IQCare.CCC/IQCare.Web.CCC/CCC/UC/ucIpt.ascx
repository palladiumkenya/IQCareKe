<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIpt.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucIpt" %>

<div class="col-md-12 form-group">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="col-md-12">
                <div class="col-md-12 form-group">
                    <label class="control-label pull-left">Isoniazid Preventive Therapy(IPT)</label>
                </div>

                <div class="col-md-12 form-group">
                    <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label  pull-left">IPT Due Date</label>
                            </div>
                            <div class="col-md-12">
                                <div class="datepicker fuelux form-group" id="DueDate">
                                    <div class="input-group">
                                        <asp:TextBox ID="iptDuedate" runat="server" class="form-control input-sm" data-parsley-required="true"></asp:TextBox>
                                        <%--<input class="form-control input-sm" id="VisitDate" type="text" runat="server" data-parsley-required="true" />--%>
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
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label  pull-left">IPT Date Collected</label>
                            </div>
                            <div class="col-md-12">
                                <div class="datepicker fuelux form-group" id="DateCollected">
                                    <div class="input-group">
                                        <asp:TextBox ID="IptDateCollected" runat="server" class="form-control input-sm" data-parsley-required="true"></asp:TextBox>
                                        <%--<input class="form-control input-sm" id="VisitDate" type="text" runat="server" data-parsley-required="true" />--%>
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
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Weight(Kg)</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" CssClass="form-control input-sm" ID="weight" ClientIDMode="Static" Type="Number" Min="0" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 form-group">
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Hepatotoxicity</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="hepatotoxicity" ClientIDMode="Static" onChange="HepatotoxicityChange();">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Action Taken</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" CssClass="form-control input-sm" ID="hepatotoxicityAction" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 form-group">
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Peripheral Neoropathy</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="peripheralNeoropathy" ClientIDMode="Static" onChange="PeripheralNeoropathyChange();">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Action Taken</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" CssClass="form-control input-sm" ID="peripheralAction" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 form-group">
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Rash?</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="rash" ClientIDMode="Static" onChange="RashChange();">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Action Taken</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" CssClass="form-control input-sm" ID="rashAction" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 form-group">
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Adherence Measurement</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="adheranceMeasurement" ClientIDMode="Static" onChange="AdheranceMeasurementChange();" />
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Action Taken</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" CssClass="form-control input-sm" ID="adheranceAction" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('#DueDate').datepicker({
            allowPastDates: true,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        });

        $('#DateCollected').datepicker({
            allowPastDates: false,
            momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
        });

        $("#<%=hepatotoxicityAction.ClientID%>").prop('disabled', true);
        $("#<%=peripheralAction.ClientID%>").prop('disabled', true);
        $("#<%=rashAction.ClientID%>").prop('disabled', true);
        $("#<%=adheranceAction.ClientID%>").prop('disabled', true);
    });

    function HepatotoxicityChange() {
        if ($("#hepatotoxicity").val() === 'False') {
            $("#<%=hepatotoxicityAction.ClientID%>").prop('disabled', true);
        } else {
            $("#<%=hepatotoxicityAction.ClientID%>").prop('disabled', false);
        }
    }
    function PeripheralNeoropathyChange() {
        if ($("#peripheralNeoropathy").val() === 'False') {
            $("#<%=peripheralAction.ClientID%>").prop('disabled', true);
        } else {
            $("#<%=peripheralAction.ClientID%>").prop('disabled', false);
        }
    }
    function RashChange() {
        if ($("#rash").val() === 'False') {
            $("#<%=rashAction.ClientID%>").prop('disabled', true);
        } else {
            $("#<%=rashAction.ClientID%>").prop('disabled', false);
        }
    }
    function AdheranceMeasurementChange() {
        if (($("#adheranceMeasurement :selected").text() === "Good") || ($("#adheranceMeasurement :selected").text() === "Fair")) {
            $("#<%=adheranceAction.ClientID%>").prop('disabled', true);
        } else {
            $("#<%=adheranceAction.ClientID%>").prop('disabled', false);
        }
    }
</script>
