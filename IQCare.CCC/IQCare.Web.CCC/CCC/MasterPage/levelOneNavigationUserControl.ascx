<%@ Control Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.MasterPage.levelOneNavigationUserControl"
    CodeBehind="levelOneNavigationUserControl.ascx.cs" %>
<script runat="server">
    private string showIcon(string iconfont)
    {
        return String.Format(@"<i class='{0}'></i>", iconfont);
    }
</script>
<style type="text/css">
        .navbar-default{background-image: none;box-shadow: none;background-color:#FCFCFC;border: 0px;background-image:none;}
    </style>
<!-- .start menu building here -->
<%-- <div class="row bg-primary">--%>
<div class="navbar navbar-default">
    <%--<div class="navbar-inner">--%>
    <div class="container-fluid" style="border-bottom: 1px solid #6CF">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"
                aria-expanded="False">
                <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                    class="icon-bar"></span><span class="icon-bar"></span>
            </button>
            <div class="navbar-brand" href="#" style="text-decoration: none;margin-top: -15px;">
                <span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-1x"></i><i class="fa fa-code-fork fa-stack-1x fa-inverse">
                </i></span><span class="text-muted small">IQCARE KE-HMIS</span>
            </div>
        </div>
        <!-- .navbar-header -->
        <!-- Everything you want hidden at 940px or less, place within here -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <asp:Menu ID="mainMenu" runat="server" EnableViewState="false" IncludeStyleBlock="false"
                Orientation="Horizontal" RenderingMode="List" CssClass="nav navbar-nav text-muted"
                StaticMenuStyle-CssClass="nav" StaticSelectedStyle-CssClass="active" DynamicMenuStyle-CssClass="dropdown-menu">
                <Items>
                    <asp:MenuItem Text="<i class='fa fa-cubes fa-1x text-muted' aria-hidden='true'><span class='fa-1x text-muted'></i><strong>  Select Service</strong></span>"
                        Value="Facility Home" NavigateUrl="~/frmFacilityHome.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-bar-chart fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'> <strong>Reports</strong></span>"
                        Value="Reports" Selectable="False">
                        <asp:MenuItem Text="Facility Reports" Enabled="true" Value="Facility Reports" NavigateUrl="~/Reports/frmReportFacilityJump.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Donor Reports" Enabled="true" Value="Donor Reports" NavigateUrl="~/Reports/frmReportDonorJump.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Custom Reports" Value="Custom Reports" NavigateUrl="~/Reports/frmReportCustom.aspx"
                            Enabled="false"></asp:MenuItem>
                        <asp:MenuItem Text="Query Builder Reports" Value="Query Builder Reports" NavigateUrl="~/Reports/frmQueryBuilderReports.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="IQTools Reports" Value="IQTools Reports" NavigateUrl="~/IQTools/frmTemplateReport.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Clinical Indicators" Value="Clinical Indicators" NavigateUrl="~/IQTools/frmLinearReport.aspx">
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-calendar fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'> <strong>Scheduler</strong></span>"
                        Value="Scheduler" NavigateUrl="~/Scheduler/frmScheduler_AppointmentMain.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-cogs fa-1x text-muted'></i> <span class='fa-1x text-muted'><strong>Administration</strong></span>"
                        Value="Administration" Selectable="False" NavigateUrl="~/frmFacilityHome.aspx">
                        <asp:MenuItem Text="Facility Setup" Value="Facility Setup" NavigateUrl="~/AdminForms/frmAdmin_FacilityList.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Customize Lists" Value="Customize Lists" NavigateUrl="~/AdminForms/frmAdmin_PMTCT_CustomItems.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="User Administration" Value="User Administration" NavigateUrl="~/AdminForms/frmAdmin_UserList.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="User Group Administration" Value="User Group Administration"
                            NavigateUrl="~/AdminForms/frmAdmin_UserGroupList.aspx"></asp:MenuItem>
                        <asp:MenuItem Text="Delete Patient" Value="Delete Patient" NavigateUrl="~/AdminForms/frmAdmin_DeletePatient.aspx?mnuClicked=DeletePatient">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Configure Custom Fields" Value="Configure Custom Fields" NavigateUrl="~/AdminForms/frmConfig_Customfields.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Export" Value="Export" NavigateUrl="~/AdminForms/frmAdmin_Export.aspx">
                        </asp:MenuItem>
                        <asp:MenuItem Text="Refresh System Cache" Value="Refresh System Cache" NavigateUrl="~/frmSystemCache.aspx">
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-database fa-1x text-muted' aria-hidden='true'></i><span class='fa-1x text-muted'> <strong>Database Back Up</strong></span>"
                        Value="Back Up" NavigateUrl="~/AdminForms/frmDBBackup.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='fa fa-percent fa-1x text-muted' aria-hidden='true'></i> <span class='fa-1x text-muted'>Facility Statistics</span>"
                        Value="Facility Statistics" NavigateUrl="~/Statistics/Facility.aspx"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>
        <!-- .nav-collapse collapse -->
    </div>
    <!-- .container -->
    <%--</div><!-- .navbar-inner -->--%>
</div>
<!-- .navbar -->
<!-- Breadcrumbs/sitemap -->
<div class="row" style="height: 25px">
   <div class="col-md-3 pull-left" style="margin-left:20px"><asp:Label ID="labelModule" CssClass="control-label pull-left" runat="server" Text=""></asp:Label></div>
   <div class="col-md-6"></div>
    <div class="col-md-3 pull-right ">
        <a class="text-muted">
            <asp:Label ID="lblRoot" CssClass="" runat="server" Text=""></asp:Label>
        </a><a class=" fa-1x text-primary">
            <asp:Label ID="lblheader" runat="server"></asp:Label></a>
    </div>
    <div class="col-md-8">
    </div>
</div>
<!-- .row -->
