<%@ Page Language="c#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.PatientHome" Title="untitled page" MaintainScrollPositionOnPostback="true"
    CodeBehind="frmpatient_home.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function fnSetSession(url) {
            //var result=frmFindAddPatient.SetPatientId_Session(url).value;
            alert(url);
            window.open(url, 'ab', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=800,height=700,scrollbars=yes');

        }
        function fnSetalert(url) {
            alert('Hello');
        }
                    
    </script>
    <script type="text/javascript" src="../Incl/Silverlight.js"></script>
    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
    <div class="row" style="padding-left: 8px; padding-right: 8px; padding-top: 2px;">
        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-6 pull-left">
                        <span id="Span1" class="text-capitalize pull-left glyphicon-text-size= fa-2x" runat="server">
                            <i class="fa fa-user-md" aria-hidden="true"></i>Patient Home</i> </span>
                    </div>
                    <!-- .col-md-6-->
                    <div class="col-md-6 ">
                        <span id="Span2" class="text-capitalize glyphicon-text-size= fa-2x pull-right">
                            <label id="lblServiceArea" runat="server">
                                Service Area</label></i> </span>
                    </div>
                    <!-- .col-md-6-->
                </div>
                <!-- .row -->
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="text-align: left; padding-top: 5; padding-bottom: 5">
                            Patient Identifier Information
                        </div>
                        <div class="panel-body" style="padding-bottom: 0; padding-top: 0">
                            <div class="row">
                                <div class="col-md-3">
                                    Patient Name:<asp:Label ID="lblpatientname" CssClass="control-label text-bold" Font-Bold="true"
                                        runat="server"></asp:Label>
                                </div>
                                <!-- .col-md-2-->
                                <div class="col-md-1">
                                    Age:<asp:Label ID="lblage" CssClass="control-label" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <!-- .col-md-2-->
                                <div class="col-md-2">
                                    Patient Facility ID:<asp:Label CssClass="control-label" ID="labelPatientFacilityID"
                                        runat="server" Font-Bold="true"></asp:Label></label>
                                </div>
                                <!-- .col-md-2-->
                                <div class="col-md-1">
                                    Sex:<asp:Label ID="lblgender" CssClass="control-label" runat="server" Font-Bold="true"></asp:Label></label>
                                </div>
                                <!-- .col-md-2-->
                                <div class="col-md-5" style="white-space: nowrap">
                                    <i class="fa fa-check-square-o text-success" aria-hidden="true"></i>Patient Status:
                                    <asp:Label ID="lblptnstatus" CssClass="control-label text-danger" runat="server"
                                        Text="" Font-Bold="true"></asp:Label>&nbsp;
                                    <asp:Label ID="lbloutofstock" CssClass="text-danger" runat="server" Font-Bold="true"
                                        Visible="false"></asp:Label>
                                    <asp:Button ID="btnReactivate" CssClass="btn btn-info" runat="server" Text="Reactivate Patient"
                                        Font-Bold="true" Visible="false" OnClick="btnReactivate_Click" />
                                </div>
                                <!-- .col-md-2-->
                            </div>
                            <!-- .row -->
                            <div class="row" style="display: <%= IsCCC %>">
                                <hr />
                                <div class="col-md-3">
                                    <div class="bs-component">
                                        <div class="alert alert-dismissible alert-danger">
                                            <button type="button" class="close" data-dismiss="alert">
                                                &times;</button>
                                            <strong>Last VL Results : </strong><span class="badge">
                                                <asp:Label ID="lblviralload" runat="server"></asp:Label></span>
                                        </div>
                                        <!-- .alert -->
                                    </div>
                                    <!-- .bs-component-->
                                </div>
                                <!-- .col-lg-3 -->
                                <div class="col-md-3">
                                    <div class="bs-component">
                                        <div class="alert alert-dismissible alert-warning">
                                            <button type="button" class="close" data-dismiss="alert">
                                                &times;</button>
                                            <strong>VL Due Date : </strong><span class="badge">
                                                <asp:Label ID="lblVLDueDate" runat="server"></asp:Label></span>
                                        </div>
                                        <!-- .alert -->
                                    </div>
                                    <!-- .bs-component-->
                                </div>
                                <!-- .col-lg-3 -->
                                <div class="col-md-3">
                                    <div class="bs-component">
                                        <div class="alert alert-dismissible alert-success">
                                            <button type="button" class="close" data-dismiss="alert">
                                                &times;</button>
                                            <strong>Current Regimen </strong><span class="badge">
                                                <asp:Label ID="lblCurARVRegimen" runat="server"></asp:Label></span>
                                        </div>
                                        <!-- .alert -->
                                    </div>
                                    <!-- .bs-component-->
                                </div>
                                <!-- .col-lg-3 -->
                                <div class="col-md-3">
                                    <div class="bs-component">
                                        <div class="alert alert-dismissible alert-danger">
                                            <button type="button" class="close" data-dismiss="alert">
                                                &times;</button>
                                            <strong>Most Recent CD4 : </strong><span class="badge">
                                                <asp:Label ID="lblrecentCD4ValueDate" runat="server"></asp:Label></span>
                                        </div>
                                        <!-- .alert -->
                                    </div>
                                    <!-- .bs-component-->
                                </div>
                                <!-- .col-lg-3 -->
                            </div>
                            <!-- .row -->
                            <!-- .row -->
                        </div>
                        <!-- .panel-body-->
                    </div>
                    <!-- .panel-->
                </div>
                <!-- -.row -->
                <!-- .row -->
                <%--<div class="row" style="display: none">
                    <div class="col-md-3">
                        <i class="fa fa-arrow-circle-o-right text-success fa-2x pull-left" aria-hidden="true">
                            Service Last Visited: </i>
                        <asp:Label CssClass="required" ID="lblTechVisited" Font-Bold="true" runat="server"
                            Text=""> </asp:Label>
                    </div>
                    <!-- .col-md-3 -->
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnTechChange" CssClass="btn btn-success fa fa-search" runat="server"
                            OnClick="btnTechChange_Click"> Find Patient</asp:LinkButton>
                    </div>
                </div>--%>
                <!-- .row-->
                <div class="row" id="tabDiv">
                    <ajaxToolkit:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_technorati-theme">
                        <ajaxToolkit:TabPanel ID="tbpnlDashboard" Font-Size="Medium" HeaderText="Dashboard"
                            runat="server">
                            <HeaderTemplate>
                                Dashboard
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="col-md-11">
                                            <h3 class="pull-left">
                                                Weight and BMI over time</h3>
                                            <chart:WebChartViewer ID="WebChartViewerWeight" runat="server" Height="200px" Width="300px" />
                                            <asp:Button ID="btnWeightChart" CssClass="btn btn-success" runat="server" Text="View Larger Graph"
                                                Font-Bold="True" OnClick="btnWeightChart_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-5">
                                        <h3>
                                            CD4 Count and Viral Load over time</h3>
                                        <chart:WebChartViewer CssClass="col-md-12" ID="WebChartViewerCD4VL" runat="server"
                                            Height="200px" Width="300px" />
                                        <asp:Button ID="btnCD4Graph" CssClass="btn btn-success" runat="server" Text="View Larger Graph"
                                            Font-Bold="True" OnClick="btnCD4Graph_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tbpnlgeneral" runat="server" Font-Size="Medium" HeaderText="General">
                            <HeaderTemplate>
                                General
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-3 dashboard-action" id="patientServices" style="text-align: left">
                                                <ul class="list-group">
                                                    <li class="list-group-item active">&nbsp; Patient Service Areas </li>
                                                    <asp:Repeater ID="rptServiceAreas" runat="server" OnItemCommand="rptServiceAreas_ItemCommand">
                                                        <ItemTemplate>
                                                            <li class="list-group-item">
                                                                <asp:LinkButton ID="btnService" runat="server" CommandArgument='<%# Eval("ServiceArea.Id") %>'>
                                                                             <%# DataBinder.Eval(Container.DataItem, "ServiceArea.DisplayName")%>
                                                                   &nbsp;&nbsp; <span class="badge" style="text-align:right">  <%# DataBinder.Eval(Container.DataItem, "EnrollmentDate", "{0:dd-MMM-yyyy}")%>   </span>
                                                                </asp:LinkButton>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="col-md-3 dashboard-action" id="patientidentifiers" style="text-align: left">
                                                <ul class="list-group">
                                                    <li class="list-group-item disabled"><span class="fa fa-list-ol"></span> Patient Identifiers</li>
                                                    <asp:Repeater ID="rptIdentifiers" runat="server">
                                                        <ItemTemplate>
                                                            <li class="list-group-item">
                                                                <%# DataBinder.Eval(Container.DataItem, "Identifier.Description")%>
                                                                &nbsp;&nbsp; <span class="badge" style="text-align: right">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Value")%>
                                                                </span></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                                <ul class="list-group">
                                                    <li class="list-group-item disabled fa fa-flask"> CD4 Summary</li>
                                                    <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="lblLowestCD4" runat="server"></asp:Label></span> Lowest CD4:</li>
                                                    <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="lblRecentCD4" runat="server"></asp:Label></span> Most Recent CD4:</li>
                                                    <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="lblCD4Due" runat="server"></asp:Label></span> Next CD4 Due: </li>
                                                    <li class="list-group-item" style="display: none">
                                                        <asp:Label ID="lblnexthivstatuscheck" runat="server" Visible="False" Text="Next HIV Status Check"></asp:Label></li>
                                                    <li class="list-group-item" style="display: none">
                                                        <asp:Label ID="lblnexthivscheck" runat="server" Visible="False"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item" style="display: none">HCT:
                                                        <asp:Label ID="lblHB" runat="server"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item" style="display: none">HB:
                                                        <asp:Label ID="lblHCT" runat="server"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item" style="display: none">AST:
                                                        <asp:Label ID="lblAST" runat="server"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item" style="display: none">Cr:
                                                        <asp:Label ID="lblCr" runat="server"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item" style="display: none">Syphilis:
                                                        <asp:Label ID="lblSyphilis" runat="server"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item" style="display: none">Pregnancy Test:
                                                        <asp:Label ID="lblPregnancyTest" runat="server"></asp:Label>
                                                    </li>
                                                </ul>

                                            </div>
                                            <%-- <div class="col-md-12" style="margin-left: 5px">
                                        <div class="row">--%>
                                            <div class="col-md-3 dashboard-action" style="text-align: left">

                                               <ul class="list-group">
                                                    <li class="list-group-item active"><span class="fa fa-flask"></span> Viral Load Summary</li>
                                                     <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="baselineVL" runat="server"></asp:Label></span> Baseline Viral Load</li>
                                                     <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="latestVL" runat="server"></asp:Label></span> Latest Viral Load </li>
                                                     <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="VLDueDate" runat="server"></asp:Label></span> Viral Load Due Date </li>
                                                </ul>

                                                <ul class="list-group">
                                                    <li class="list-group-item disabled fa fa-shield"> Current ARV Summary</li>
                                                    <li class="list-group-item fa fa-chevron-circle-right">Current Regimen : <strong>
                                                        <asp:Label ID="lblclinicalcurrentregimen" runat="server"></asp:Label></strong></li>
                                                    <li class="list-group-item fa fa-chevron-circle-right">Current ARV Start Date: <strong>
                                                        <asp:Label ID="lblarvstartdate" runat="server"></asp:Label></strong></li>
                                                    <li class="list-group-item fa fa-chevron-circle-right">Next Appointment: <strong>
                                                        <asp:Label ID="lblnextapp" runat="server"></asp:Label></strong></li>
                                                </ul>
                                            </div>
                                            <!-- .col-md-4-->
                                            <div class="col-md-3">
                                                <ul class="list-group">
                                                    <li class="list-group-item disabled fa fa-bar-chart" style="text-align: left"> Clinical
                                                        Information</li>
                                                    <li class="list-group-item">
                                                        <asp:LinkButton ID="lnkPharmacyNotes" CssClass="btn btn-info fa fa-pencil-square-o "
                                                            runat="server" OnClick="lnkPharmacyNotes_Click"> View Prescription Notes</asp:LinkButton></li>
                                                    <li class="list-group-item" style="display: none"><span class="badge">
                                                        <asp:Label ID="lblWABStage" runat="server"></asp:Label></span> Last Recorded WAB:</li>
                                                    <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="lblWHOStage" runat="server"></asp:Label></span> WHO Stage: </li>
                                                    <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="lblweight" runat="server"></asp:Label></span> Weight:</li>
                                                    <li class="list-group-item"><span class="badge">
                                                        <asp:Label ID="lblheight" runat="server"></asp:Label></span> Height:</li>
                                                    <li class="list-group-item" style="display: none">
                                                        <asp:Image ID="Image1" runat="server" Visible="False" /></li>
                                                    <li class="list-group-item" style="display: none">
                                                        <asp:Label ID="lblPregnant" runat="server" Visible="False"></asp:Label>
                                                    </li>
                                                    <li class="list-group-item fa fa-chevron-circle-right">ARV Start Date At This Facility:
                                                        <strong>
                                                            <asp:Label ID="lblaidsrstartdate" runat="server"></asp:Label></strong></li>
                                                    <li class="list-group-item">TB Status : <strong>
                                                        <asp:Label ID="lblclnicalTBStatus" runat="server"></asp:Label></strong></li>
                                                    <li class="list-group-item">Last Visit : <strong>
                                                        <asp:Label ID="lblclinicallastvisit" runat="server"></asp:Label></strong></li>
                                                </ul>
                                            </div>
                                            <!-- .col-md-4-->
                                            <%-- </div>--%>
                                        </div>
                                        <div class="row">
                                            <!-- .col-md-6-->
                                            <!-- .col-md-11 -->
                                            <!-- .col-md-11-->
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rptServiceAreas" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <!-- .row-content -->
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tbpnldynamic" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="row" style="display: <%= _showPMTCT %>">
                                            <div id="divAddChildren" style="display: <%= _showChildren %>">
                                                <li class="list-group-item">
                                                    <asp:LinkButton ID="btnAddChildren" runat="server" Text="Add/Show Children" OnClick="btnAddChildren_Click" />
                                                    <%--  <asp:Button ID="btnAddChildren" CssClass="btn btn-info" runat="server" Text="Add/Show Children"
                                                        Font-Bold="true" OnClick="btnAddChildren_Click" />--%>
                                            </div>
                                            <div style="display: <%= _showMother %>">
                                                <asp:LinkButton ID="btnShowMother" runat="server" Text="Show Mother" OnClick="btnShowMother_Click" />
                                                <%--  <asp:Button ID="btnShowMother" CssClass="btn btn-info" runat="server" Text="Show Mother"
                                                    OnClick="btnShowMother_Click" />--%>
                                                <asp:HiddenField ID="hpIQNumber" runat="server" />
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                            </div>
                                            <div class="panel-body">
                                                <asp:Panel ID="thePnl" Width="100%" runat="server">
                                                </asp:Panel>
                                            </div>
                                            <!-- .panel-body -->
                                        </div>
                                        <!-- .panel-->
                                    </div>
                                    <!-- .col-md-4-->
                                    <div class="col-md-4">
                                        <ul class="list-group">
                                            <li class="list-group-item disabled">Contact Information</li>
                                            <li class="list-group-item">Patient Phone :
                                                <asp:Label ID="lblpatientphone" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblShowAddress" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lbladdress" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblEmrContact" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblemergencycontact" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblEmrPhone" runat="server"></asp:Label></li>
                                            <li class="list-group-item" style="display: none">
                                                <asp:Label ID="lblemgphone" runat="server"></asp:Label></li>
                                        </ul>
                                    </div>
                                    <!-- .col-md-4-->
                                    <div class="col-md-4">
                                        <ul class="list-group">
                                            <li class="list-group-item disabled">Family Information</li>
                                            <li class="list-group-item" style="display: none">
                                                <asp:Label ID="lblhivpositivemother" runat="server" Font-Bold="True" Visible="False"></asp:Label></li>
                                            <li class="list-group-item"><span class="badge">
                                                <asp:Label ID="lblfamilyEnrolled" runat="server"></asp:Label></span> No. of related
                                                members enrolled:</li>
                                            <li class="list-group-item"><span class="badge">
                                                <asp:Label ID="lblfamilyArt" runat="server"></asp:Label></span> No. of related members
                                                on ART: </li>
                                            <li class="list-group-item" style="display: none">
                                                <asp:HyperLink ID="hlFamilyInfo" runat="server" CssClass="utility" Visible="False">Family Information</asp:HyperLink></li>
                                            <li class="list-group-item" style="display: none">
                                                <asp:Image ID="imgfamily" runat="server" Visible="False" /></li>
                                        </ul>
                                    </div>
                                    <!-- .col-md-4-->
                                </div>
                                <!-- .row -->
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanelPatientCosts" runat="server" HeaderText="Patient Costs"
                            Visible="false">
                            <ContentTemplate>
                                <asp:Label ID="Label1" runat="server" Text="HIV Care Program Costs"></asp:Label>
                                Patient Cost Per Debit Note
                                <div class="row grid">
                                    <div id="div-gridview" class="gridviewdebitnote whitebg">
                                        <asp:GridView ID="GridViewSummary" runat="server" AutoGenerateColumns="False" CssClass="datatable"
                                            GridLines="None" OnRowCommand="GridViewSummary_RowCommand">
                                        </asp:GridView>
                                    </div>
                                    <!-- div-gridview-->
                                </div>
                                <!-- .grid -->
                                <div class="row grid">
                                    <div id="div1" class="gridviewDebitNote whitebg">
                                        <asp:GridView ID="GridViewTran" runat="server" AutoGenerateColumns="False" CssClass="datatable"
                                            GridLines="None" AllowSorting="True">
                                            <Columns>
                                                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" HeaderStyle-Width="15%"
                                                    DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="25%"
                                                    ReadOnly="True" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="Adminsitration" HeaderText="Administration" ReadOnly="True"
                                                    HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="Cost" HeaderText="Actual Price" ReadOnly="True" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="ChargedPrice" HeaderText="Charged Price" ReadOnly="True"
                                                    HeaderStyle-Width="15%" />
                                            </Columns>
                                            <RowStyle CssClass="gridrow" />
                                            <EmptyDataTemplate>
                                                <span style="text-align: center">Select a debit note above to see details here.
                                                </span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <!-- .grid-->
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    <div class="row">
                        <asp:MultiView ID="TabView" ActiveViewIndex="0" runat="server">
                            <asp:View ID="ViewGeneral" runat="server">
                            </asp:View>
                            <div class="row">
                                <asp:View ID="ViewPMTCT" runat="server">
                                    <div class="row" id="tblpmtct">
                                        <div class="col-md-3">
                                            <ul class="list-group">
                                                <li class="list-group-item disabled">PMTCT Identification Information</li>
                                                <li class="list-group-item">
                                                    <asp:Label ID="lblanc" runat="server">ANC Number: </asp:Label><asp:Label ID="lblancno"
                                                        runat="server"></asp:Label></li>
                                                <li class="list-group-item">
                                                    <asp:Label ID="lblpmtct" runat="server">PMTCT Number:</asp:Label>
                                                    <asp:Label ID="lblpmtctno" runat="server"></asp:Label></li>
                                                <li class="list-group-item">
                                                    <asp:Label ID="lbladmission" runat="server">Admission Number:</asp:Label>
                                                    <asp:Label ID="lbladmissionno" runat="server"></asp:Label></li>
                                                <li class="list-group-item">
                                                   Admission Number:
                                                    <asp:Label ID="lblHeiNo" runat="server"></asp:Label></li>
                                                <li class="list-group-item">
                                                    <asp:Label ID="lbloutpatient" runat="server">Outpatient Number:</asp:Label>
                                                    <asp:Label ID="lbloutpatientno" runat="server"></asp:Label></li>
                                            </ul>
                                        </div>
                                        <!-- col-md-3-->
                                        <div class="col-md-3">
                                            <ul class="list-group">
                                                <li class="list-group-item disabled">PMTCT Information</li>
                                                <li class="list-group-item">LMP :
                                                    <asp:Label ID="lblLMP" runat="server"></asp:Label></li>
                                                <li class="list-group-item">EDD :
                                                    <asp:Label ID="lblEDD" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Gestational Age (Weeks) :
                                                    <asp:Label ID="lblGestAge" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Current ARV Prophylaxis Regimen :
                                                    <asp:Label ID="lblARVProRegimen" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Current ARV Prophylaxis Start Date :
                                                    <asp:Label ID="lblARVProStartDate" runat="server"></asp:Label></li>
                                                <li class="list-group-item">TB Status :
                                                    <asp:Label ID="lblTBStatus" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Partner HIV Status :
                                                    <asp:Label ID="lblPartnerHIVStatus" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Last Visit :
                                                    <asp:Label ID="lblLastVisit" runat="server"></asp:Label></li>
                                            </ul>
                                        </div>
                                        <!-- col-md-3-->
                                        <div class="col-md-3">
                                            <ul class="list-group">
                                                <li class="list-group-item disabled">Exposed Infant Information</li>
                                                <li class="list-group-item">Date of Birth (Current Pregnancy) :
                                                    <asp:Label ID="lblDeliveryDate" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Infant Prophylaxis Regimen :
                                                    <asp:Label ID="lblInfantProphylaxisRegimen" runat="server"></asp:Label></li>
                                                <li class="list-group-item">Feeding Option :
                                                    <asp:Label ID="lblFeedingOption" runat="server"></asp:Label></li>
                                                <li class="list-group-item">
                                                    <div id="divResult" runat="server">
                                                        <asp:Literal ID="Literal1" runat="server" Text=""></asp:Literal></div>
                                                </li>
                                                <li class="list-group-item">
                                                    <asp:Panel ID="DynControlPMTCT" runat="server">
                                                    </asp:Panel>
                                                </li>
                                            </ul>
                                        </div>
                                        <!-- col-md-3-->
                                    </div>
                                    <!-- .row tblpmtct-->
                                </asp:View>
                                <asp:View ID="ViewART" runat="server">
                                    <div class="col-md-3" id="tbhiv">
                                        <ul class="list-group">
                                            <li class="list-group-item disabled">HIV Static Form Identification Information</li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblenroll" runat="server">Enroll#:</asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblptnenrollment" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblClinicNo" runat="server"></asp:Label></li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblexistingid" runat="server"></asp:Label></li>
                                            <li class="list-group-item ative">ART Information</li>
                                            <li class="list-group-item">ART/Palliative Care:
                                                <asp:Label ID="lblprogram" runat="server"></asp:Label></li>
                                            <li class="list-group-item">Current ARV Regimen:
                                                <asp:Label ID="lblarvregimen" runat="server"></asp:Label></li>
                                            <%--<li class="list-group-item">Current ARV Start Date: <asp:Label ID="lblarvstartdate" runat="server"></asp:Label></li>--%>
                                            <%--<li class="list-group-item">ARV Start Date At This Facility: <asp:Label ID="lblaidsrstartdate" runat="server"></asp:Label></li>--%>
                                            <li class="list-group-item">Historical ART Start Date:
                                                <asp:Label ID="lblhistoricalsdate" runat="server"></asp:Label></li>
                                            <li class="list-group-item">Last Visit:
                                                <asp:Label ID="lbllstvisit" runat="server"></asp:Label>
                                            </li>
                                            <%-- <li class="list-group-item">Next Appointment: <asp:Label ID="lblnextapp" runat="server"></asp:Label></li>--%>
                                            <li class="list-group-item">
                                                <asp:HyperLink ID="hlFollowupeducation" Visible="false" CssClass="utility" runat="server">Follow-up Education</asp:HyperLink></li>
                                            <li class="list-group-item">
                                                <asp:Image ID="imgFollowupeducation" Visible="false" runat="server" /></li>
                                            <li class="list-group-item">
                                                <asp:Panel ID="DynControlsARV" runat="server">
                                                </asp:Panel>
                                            </li>
                                        </ul>
                                    </div>
                                    <!-- col-md-3-->
                                </asp:View>
                                <asp:View ID="ViewDynamic" runat="server">
                                </asp:View>
                            </div>
                            <!-- .row -->
                        </asp:MultiView>
                    </div>
                    <!-- .row -->
                </div>
                <!-- .row TAB -->
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnAddChildren"></asp:PostBackTrigger>--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
