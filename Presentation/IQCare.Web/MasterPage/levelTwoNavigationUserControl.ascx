<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="levelTwoNavigationUserControl.ascx.cs" Inherits="IQCare.Web.MasterPage.levelTwoNavigationUserControl" %>
<div class="container-fluid">
    <div class="row">
        <asp:Label ID="lblpntStatus" CssClass="textstylehidden" runat="server" Text="0"></asp:Label>
        <asp:Panel ID="PanelPatiInfo" class="" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="pull-left"></span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            Patient Name : <strong>
                                <asp:Label ID="lblpatientname" runat="server"></asp:Label></strong>
                        </div>
                        <!-- .col-md-2-->
                        <div class="col-md-2">
                            Sex : <strong>
                                <asp:Label ID="lblSex" runat="server"></asp:Label></strong>
                        </div>
                        <!-- .col-md-2-->
                        <div class="col-md-2">
                            DoB : <strong>
                                <asp:Label ID="lblDOB" runat="server"></asp:Label></strong>
                        </div>
                        <!-- .col-md-2-->
                        <div class="col-md-2">
                            Age : <strong>
                                <asp:Label ID="lblAge" runat="server"></asp:Label>
                            </strong>
                        </div>
                        <!-- .col-md-2 -->
                        <div class="col-md-3">
                            IQ Number : <strong>
                                <asp:Label ID="lblIQnumber" runat="server"></asp:Label></strong>
                        </div>
                        <!-- .col-md-2-->
                    </div>
                    <!-- .row -->
                    <%--<hr />--%>
                    <div class="row">
                        <asp:Panel ID="thePnlIdent" runat="server">
                        </asp:Panel>
                    </div>
                </div>
                <!-- .panel-body -->
            </div>
            <div class="row" style="display:none">
                <asp:Panel ID="thePnlBill" runat="server">
                </asp:Panel>
            </div>
            <!-- .row -->
        </asp:Panel>
        <%--        <span class="text-capitalize pull-left glyphicon-text-size= fa-2x"> <i class="fa fa-cubes fa-3x" aria-hidden="true"></i>
         </span>    

            <asp:Label ID="lblTechnicalArea" runat="server"></asp:Label>   Patient Waiting List--%>
    </div>
    <!-- .row -->
</div>
<!-- .container-fluid -->
<div runat="server" id="divmenu2">
    <script type="text/javascript">

        function ClearSession() {

            //ClinicalForms_ClinicalHomeHeaderFooter.SetPatientId_Session();
            //MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();
            //PageMethods.SetPatientId_Session(OnPageeSucess, OnPageError);
        }

        function fnSetformID(id) {
            //alert("executed");
            //alert(id);
            //ClinicalForms_ClinicalHomeHeaderFooter.SetDynamic_Session(id);
            //PageMethods.SetDynamic_Session(id,OnPageeSucess,OnPageError);

        }
        function pageLoad() {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndAJAXRequestHandler);
        }
        function EndAJAXRequestHandler(sender, args) {

            if (args.get_error() != undefined && args.get_error().httpStatusCode == '500') {
                var errorMessage = args.get_error().message;
                args.set_errorHandled(true);
                alert("IQCare Application Framework encountered an unrecoverable error:\n" + errorMessage + "\n\nPlease report this error to the support team.");
            }

        }

        function OnPageeSucess(result) {

        }
        //**********************************************
        function OnPageError(error) {

        }
        function openBluecard() {
            window.open('../Reports/frmPatientBlueCart.aspx?name=Add&PatientId=' + '<%#PatientId.ToString()%>' + '&ReportName=bluecart' + '&sts=lblpntStatusText', 'bluecart', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');

        }

        function openClinicalSummary() {
            window.open('../Reports/frmClinical_PatientSummary.aspx', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }

        //window.onload = function setFeatureID(id){
        function setFeatureID(id) {
            var menuTable = document.getElementById("<%#patientLevelMenu.ClientID%>");  //specify your menu id instead of Menu1
            if (menuTable == null || typeof (menuTable) == "undefined") return;
            var menuLinks = menuTable.getElementsByTagName("a");
            for (i = 0; i < menuLinks.length; i++) {
                menuLinks[i].onclick = function () { return confirm("u sure to postback?"); }
            }
            setOnClickForNextLevelMenuItems(menuTable.nextSibling, id);
        }
        function setOnClickForNextLevelMenuItems(currentMenuItemsContainer, id1) {

            var id = currentMenuItemsContainer.id;
            var len = id.length;
            if (id != null && typeof (id) != "undefined" && id.substring(0, parseInt(len) - 7) == "<%#patientLevelMenu.ClientID %>" && id.substring(parseInt(len) - 5, parseInt(len)) == "Items") {
                var subMenuLinks = currentMenuItemsContainer.getElementsByTagName("a");
                for (i = 0; i < subMenuLinks.length; i++) {
                    //subMenuLinks[i].onclick = function () { return confirm("u sure to postback?"); }
                    // fnSetformID(id1);
                    PageMethods.SetDynamic_Session(id1, OnPageeSucess, OnPageError);
                }
                setOnClickForNextLevelMenuItems(currentMenuItemsContainer.nextSibling, id1);
            }
        }
        function openWaitingList() {//opening patients waiting list
            window.open("../ClinicalForms/frmPatientWaitingList.aspx", 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=150,left=150,maximize=yes,resizable=no,width=800,height=500,scrollbars=yes');
        }
    </script>
</div>
<!-- .divmenu2-->
<div class="navbar bg-default">
    <%--<div class="navbar-inner">--%>
    <div class="container-fluid" style="border-bottom: 1px solid #6CF">
        <div class="navbar-header">
        
            <div class="navbar-brand" href="#" style="text-decoration: none">
              
                <asp:Label CssClass="pull-left text-danger fa fa-angle-double-down" ID="lblformname"
                    runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hLoad" runat="server" Value="TRUE" />
            </div>
        </div>
        <!-- .navbar-header -->
        <!-- Everything you want hidden at 940px or less, place within here -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <asp:Menu ID="patientLevelMenu" runat="server" EnableViewState="true" IncludeStyleBlock="false"
                Orientation="Horizontal" RenderingMode="List" OnMenuItemClick="patientLevelMenu_MenuItemClick1"
                CssClass="nav navbar-nav text-muted" StaticMenuStyle-CssClass="nav" StaticSelectedStyle-CssClass="active"
                DynamicMenuStyle-CssClass="dropdown-menu">
                <Items>
                    <asp:MenuItem Text="<i class='fa fa-pencil-square-o fa-lg text-muted' aria-hidden='true'></i> <span class='text-muted'>Registration</span>"
                        Selectable="false" Value="mnuRegistrationMain" >
                         <asp:MenuItem Text="Registration" Value="mnuRegistration" NavigateUrl="~/Patient/Registration.aspx"/>
                        <asp:MenuItem Text="Enrollment" Value="mnuEnrollment" NavigateUrl="~/Patient/AddTechnicalArea.aspx"/>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-exchange text-muted fa-lg' aria-hidden='true'></i><span class='text-muted'>Transfer</span>"
                        Value="mnuPatientTransfer"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-exchange text-muted fa-lg' aria-hidden='true'></i><span class='text-muted'>Additional Forms</span>"
                        Value="Additional Forms" Selectable="False">
                        <asp:MenuItem Text="Family Information" Value="mnuFamilyInformation"></asp:MenuItem>
                        <asp:MenuItem Text="Patient Classification" Value="mnuPatientClassification"></asp:MenuItem>
                        <asp:MenuItem Text="Follow-up Education" Value="mnuFollowupEducation"></asp:MenuItem>
                        <asp:MenuItem Text="Exposed Infant Follow-up" Value="mnuExposedInfant"></asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-folder-open text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Existing Forms<span>"
                        Value="mnuExistingForms"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-file-text-o fa-lg text-muted' aria-hidden='true'></i> <span class='text-muted'>Create New Form</span>"
                        Selectable="False" Value="mnuCreateNewForm">
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-trash text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Delete Form</span>"
                        Value="mnuClinicalDeleteForm"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-bars text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Patient Reports</span>"
                        Value="mnuReport" Selectable="False">
                        <asp:MenuItem Text="Patient ARV Pick-up" Value="mnuDrugPickUp"></asp:MenuItem>
                        <asp:MenuItem Text="HIV Care Patient Profile" Value="mnuPatientProfile"></asp:MenuItem>
                        <asp:MenuItem Text="Patient Blue Card" Value="mnuPatientBlueCard" NavigateUrl="javascript:openBluecard();"
                            Target="_self"></asp:MenuItem>
                        <asp:MenuItem Text="Debit Note" Value="mnuDebitNote" ></asp:MenuItem>
                        <asp:MenuItem Text="Patient Profile Summary" Value="mnuClinicalSummary" NavigateUrl="javascript:openClinicalSummary();"
                            Target="_self"></asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-clock-o text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Schedule App</span>"
                        Value="mnuScheduleAppointment"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-home text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Patient Home</span>"
                        Value="mnuPatientHome" NavigateUrl="~/ClinicalForms/frmPatient_Home.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-building-o text-muted fa-lg' aria-hidden='true'></i> <span class='text-muted'>Waiting List</span>"
                        Value="mnuWaitingList"></asp:MenuItem>
              </Items>
            </asp:Menu>
            
        </div>
        <!-- .nav-collapse collapse -->
    </div>
    <!-- .container -->
    <%--</div><!-- .navbar-inner -->--%>
</div>
