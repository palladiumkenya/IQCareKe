<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.LoginWeb" EnableViewState="true"
    CodeBehind="frmLogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html lang="en-US" xml:lang="en-US" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>International Quality Care Patient Management and Monitoring System</title>
    <link rel="stylesheet" type="text/css" href="../Style/styles.css" />
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="/favicon.ico" type="image/x-icon" />
    <link href="./Content/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="./Content/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="./Content/css/parsley.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                 <img src="Images/kenya1.png"  class="pull-left" alt="Keany Court of Arms"/>  
            </div>
            <div class="col-md-6">
               
                <img src="./Content/img/iqSolutions.png" width="477" height="82" class="pull-right"
                    alt="IQCare logo" />
            </div>
            
            <!-- .col-md-6 -->

            <!-- .col-md-6 -->
        </div>
        <!-- .row -->
        <hr style="border-bottom: 1px solid #6CF" />
    </div>
    <!-- .container-fluid -->
    <div class="container">
        <div class="row">
            <%--<div class="=col-md-1"></div>--%>
            <div class="col-md-12 jumbotron">
                <div class="row" style="padding-top: 3%">
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2 pull-right">
                            <asp:Label ID="lblDate" CssClass="" runat="server" Text="30 September 2006">Date</asp:Label>
                        </div>
                    </div>
                    <!-- .row -->
                    <div class="row">
                        <div class="col-md-12 label label-primary">
                            <div class="col-md-3 pull-left">
                                <span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-cogs fa-stack-1x">
                                </i></span><span class="bold h5">IQCARE AUTHENTICATION</span>
                            </div>
                        </div>
                    </div>
                    <!-- .row -->
                    <asp:Label ID="lblLocation" runat="server" Text="Go Hospital and Medical Center"></asp:Label>
                    <asp:Label ID="lblUserName" CssClass="control-label" runat="server" Text="User name"></asp:Label>
                    <%--<span class="text-capitalize text-primary pull-left"><span class="fa fa-sign-in pull-left"> AUTHENTICATION </span><hr/>--%>
                    <div class="col-md-6">
                        <%--<asp:Image ID="imgLogin" runat="server" ImageUrl="~/Images/signin.jpg"--%>
                        <asp:Image ID="imgLogin" runat="server" ImageUrl="~/Images/signin.jpg" Height="305px" />
                        <%--                    <div class="pull-left">
                    <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/CreativeCommon.jpg"/>
                                            </a></div>--%>
                    </div>
                    <!-- .col-md-6-->
                    <div class="col-md-6">
                        <form id="signIn" enableviewstate="true" runat="server" defaultbutton="btnLogin"
                        defaultfocus="txtuname" data-parsley-validate="true">
                        <asp:ScriptManager ID="mst" runat="server" EnablePageMethods="true" ScriptMode="Auto"
                            OnAsyncPostBackError="ActionScriptManager_AsyncPostBackError">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
                            <ContentTemplate>
                                <div class="form-group  col-md-10">
                                    <label class="control-label pull-left" for="txtuname">
                                        Username</label>
                                    <asp:TextBox ID="txtuname" Name="txtuname" CssClass="form-control" runat="server"
                                        required="true" placeholder="username"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-10">
                                    <label class="control-label pull-left" for="txtpassword">
                                        Password</label>
                                    <asp:TextBox ID="txtpassword" CssClass="form-control" Name="txtpassword" TextMode="Password"
                                        required="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:CheckBox ID="chkPref" CssClass="pull-left" runat="server" Text=" Prefered Location"
                                        AutoPostBack="true" OnCheckedChanged="chkPref_CheckedChanged" />
                                </div>
                                <%-- <asp:CheckBox ID="chkPref" CssClass="pull-left col-md-12" runat="server" Text=" Prefered Location"  AutoPostBack="true" OnCheckedChanged="chkPref_CheckedChanged" /> --%>
                                <div class="form-group col-md-10">
                                    <label class="control-label pull-left" for="ddLocation">
                                        Facility/Satellite</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddLocation" Name="ddLocation" required="true"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-10">
                                    <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-info pull-left col-md-12" runat="server"
                                        Text="Login" OnClick="btnLogin_Click" /><br />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="chkPref" />
                                <asp:PostBackTrigger ControlID="btnLogin" />
                            </Triggers>
                        </asp:UpdatePanel>
                        </form>
                        <!-- end content area -->
                    </div>
                    <!-- </div>-->
                </div>
                <!-- .col-md-6-->
            </div>
            <!-- .row -->
            <hr />
            <div class="row">

                <div class="col-md-1 ">
                    <a href="http://thepalladiumgroup.com/ " onclick="window.open('http://thepalladiumgroup.com/ '); return false;">
                        <img src="./Images/FGI.jpg" width="99" style="margin-left: 10.5%" alt="" /></a>
                </div>
               
                <div class="col-md-2 ">
                    <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/CreativeCommon.jpg" /></a>
                </div>
              <div class="col-md-1">
                <a class="utility pull-right" href="frmLogin.aspx" onclick="window.open('./IQCareHelp/index.html'); return false;">
                    <span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-question-circle-o fa-stack-1x fa-inverse">
                    </i></span><strong>Help</strong>
                    <br />
                </a>
            </div>
                <div class="col-md-5 pull-right">
                    <label>
                        Version :<asp:Label CssClass="blue11 nomargin pull-right" ID="lblversion" Text="Kenya HMIS"
                            runat="server"></asp:Label></label>
                    |
                    <label>
                        Release Date :<asp:Label CssClass="blue11 nomargin" ID="lblrelDate" Text="Date" runat="server"></asp:Label></label>
                </div>
            </div>
            <!-- .row -->
        </div>
        <!-- .row -->
    </div>
    <!-- .col-md-12-->
     <!-- .container-->
    <script type="text/javascript" src="./Incl/jquery-1.9.1.js">  </script>
    <script src="./Content/js/bootstrap.js" type="text/javascript"></script>
    <script src="./Content/js/parsley.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

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
        function OnSuccess(path) {
            window.location.href = path;
        }
        function OnError(error) {

        }
        function fnHelp() {
            // var path = frmLogin.CallHelp().value;
            PageMethods.CallHelp(OnSuccess, OnError);
            // window.location.href = path;
            //window.showHelp("./IQCareHelp/IQCareARUserManualSep2010.chm")
        }
        function CloseWindow() {
            window.focus();
        }

        $(document)
        .ready(function () {
            $("#signIn").parsley();
            localStorage.clear();
        });
   
    </script>
</body>
</html>
