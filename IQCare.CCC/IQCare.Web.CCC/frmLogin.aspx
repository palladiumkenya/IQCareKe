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

	<div class="container-fluid">
	     
         <div class="row">

             <div class="col-md-6">
                 <img src="./Content/img/iqSolutions.png" width="477" height="82" class="pull-left" alt="IQCare logo"/>
             </div><!-- .col-md-6 -->
              
              <div class="col-md-6">
                   <a class="utility pull-right" href="frmLogin.aspx" onclick="window.open('./IQCareHelp/index.html'); return false;">
                         <span class="fa-stack fa-lg">
                                    <i class="fa fa-circle fa-stack-2x"></i>
                                    <i class="fa fa-question-circle-o fa-stack-1x fa-inverse"></i>
                         </span><strong>Help</strong> <br />          
                   </a>
              </div><!-- .col-md-6 -->
           
		 </div><!-- .row -->
	    <hr style="border-bottom: 1px solid #6CF"/>	
	</div><!-- .container-fluid -->

    
    <div class="container">
        
        <div class="row">
            <%--<div class="=col-md-1"></div>--%>
            
            <div class="col-md-12 jumbotron">
                
                <div class="row" style="padding-top:3%">
                
                        <div class="row">
                           <%-- <div class="col-md-3 pull-left">
                                  <span class="fa-stack fa-lg">
                                    <i class="fa fa-circle fa-stack-2x"></i>
                                    <i class="fa fa-cogs fa-stack-1x fa-inverse"></i>
                                 </span><span class="bold">IQCARE AUTHENTICATION</span>
                            </div>--%>
                            <div class="col-md-3"></div>
                            <div class="col-md-4"></div>
                            <div class="col-md-2 pull-right">
                                 <%--<span class="fa-stack fa-lg">
                                    <i class="fa fa-circle fa-stack-2x"></i>
                                    <i class="fa fa-calendar fa-stack-1x fa-inverse"></i>
                                 </span><strong class=""> --%>
                                  <asp:Label ID="lblDate" CssClass="" runat="server" Text="30 September 2006">Date</asp:Label>
                            </div>

                        </div><!-- .row -->

                        <div class="row">
                            <div class="col-md-12 label label-primary" >
                                 <div class="col-md-3 pull-left">
                                    <span class="fa-stack fa-lg"  >
                                    <i class="fa fa-circle fa-stack-2x" ></i>
                                    <i class="fa fa-cogs fa-stack-1x"></i>
                                    </span><span class="bold h5">IQCARE AUTHENTICATION</span>
                                 </div>
                            </div>
                        </div><!-- .row -->
                         
                         
                         
                        <asp:Label ID="lblLocation" runat="server" Text="Nsambya Hospital and Medical Center"></asp:Label>
                        <asp:Label ID="lblUserName" CssClass="control-label" runat="server" Text="Lanette Burrows"></asp:Label>
                    <%--<span class="text-capitalize text-primary pull-left"><span class="fa fa-sign-in pull-left"> AUTHENTICATION </span><hr/>--%>
                    <div class="col-md-6">
                         <%--<asp:Image ID="imgLogin" runat="server" ImageUrl="~/Images/signin.jpg"--%>
                           <asp:Image ID="imgLogin" runat="server" ImageUrl="~/Images/signin.jpg" 
                                                   Height="305px"/>                        

<%--                    <div class="pull-left">
                    <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/CreativeCommon.jpg"/>
                                            </a></div>--%>
                    </div><!-- .col-md-6-->

                    <div class="col-md-6">
                
                        <form id="signIn" enableviewstate="true" runat="server" defaultbutton="btnLogin" defaultfocus="txtuname" data-parsley-validate>
    
                                    <%--       <div class="loginpageheight">
                                            <div class="utility" align="right">
                                                <a class="utility" href="frmLogin.aspx" onclick="window.open('./IQCareHelp/index.html'); return false;">
                                                    Help</a>
                                            </div>--%>
                            <%--  <div id="main">xxxxx
                                          <div id="bluetop">
                                            </div>
                                            <img id="logo" height="94" alt="International Quality Care " src="./images/iq_logo.gif"
                                                width="236" border="0" />
                                            <img id="pmms" height="53" alt="Patient Management and Monitoring System" src="./images/pmms.gif"
                                                width="264" border="0" />
                                            <img id="collage" height="117" alt="" src="./images/collage.jpg" width="424" border="0" />--%>

        <%--                        <div id="username" class="form-group">
                                    <asp:Label ID="lblUserName" CssClass="control-label" runat="server" Text="Lanette Burrows"></asp:Label>
                                </div>

                                <div id="date" class="" align="right">
                                    <asp:Label ID="lblDate" runat="server" Text="30 September 2006"></asp:Label>
                                </div>

                                <div id="border">
                                </div>-->

                                <img id="tabfacility" height="23" alt="Facility Name" src="./images/tab_facility.gif"
                                     width="377" border="0"/>
                                <div id="facility">
                                    <asp:Label ID="lblLocation" runat="server" Text="Nsambya Hospital and Medical Center"></asp:Label>
                                </div>--%>
                        

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <div style="padding-top: 10px;">
                            <!-- begin content area -->
<%--                            <table class="border" cellspacing="0" cellpadding="0"   width="750" align="center"
                                   border="0">
                                <tbody>
                                <tr>--%>
<%--                                    <td style="border-right: #666699 1px solid;" width="500">
                                        <asp:Image ID="imgLogin" runat="server" ImageUrl="~/Images/signin.jpg" Width="500px"
                                                   Height="305px"/>
                                    </td>--%>
                                 <%--   <td class="login" style="width: 250px">
                                        <h2 class="nomargin">
                                            Login
                                        </h2>--%>

                                        <asp:ScriptManager ID="mst" runat="server" EnablePageMethods="true" ScriptMode="Auto"
                                                           OnAsyncPostBackError="ActionScriptManager_AsyncPostBackError">
                                        </asp:ScriptManager>

                                        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
                                            <ContentTemplate>
                                                
                                                <div class="form-group  col-md-10">
                                                    <label class="control-label pull-left" for="txtuname">Username</label>
                                                    <asp:TextBox ID="txtuname" Name="txtuname" CssClass="form-control" runat="server"  required="true" placeholder="username"></asp:TextBox>
                                                </div>

                                                <div class="col-md-2"></div>

                                                <div class="form-group col-md-10">
                                                    <label class="control-label pull-left" for="txtpassword">Password</label>
                                                    <asp:TextBox ID="txtpassword" CssClass="form-control" Name="txtpassword" TextMode="Password" required="true" runat="server"></asp:TextBox>
                                                </div>

                                                 <div class="col-md-2"></div>

                                                <div class="form-group col-md-12">
                                                     <asp:CheckBox ID="chkPref" CssClass="pull-left" runat="server" Text=" Prefered Location"  AutoPostBack="true" OnCheckedChanged="chkPref_CheckedChanged" />                 
                                                </div>

                                               <%-- <asp:CheckBox ID="chkPref" CssClass="pull-left col-md-12" runat="server" Text=" Prefered Location"  AutoPostBack="true" OnCheckedChanged="chkPref_CheckedChanged" /> --%>
                                                
                                                <div class="form-group col-md-10">
                                                    <label class="control-label pull-left" for="ddLocation">Facility/Satellite</label>
                                                    <asp:DropDownList CssClass="form-control" ID="ddLocation" Name="ddLocation" required="true" runat="server"></asp:DropDownList>        
                                                </div>

                                                 <div class="col-md-2"></div>
                                                
                                                <div class="form-group col-md-10">
                                                    <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-info pull-left col-md-12" runat="server" Text="Login" OnClick="btnLogin_Click"/><br />

                                                </div>

                                                <!-- note: method set to get for demo -->
<%--                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td class="pad18">
                                                            <label>
                                                                Username
                                                            </label>
                                                            <asp:TextBox ID="txtunamex" runat="server" Width="210px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pad18">
                                                            <label for="passwordx">
                                                                Password
                                                            </label>
                                                            <asp:TextBox ID="txtpasswordx" TextMode="Password" runat="server" Width="210px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pad18">
                                                            <asp:CheckBox ID="chkPref" runat="server" Text="Preferred Location" AutoPostBack="true"
                                                                          OnCheckedChanged="chkPref_CheckedChanged"/>
                                                            <label>
                                                                Facility/Satellite
                                                            </label>
                                                            <asp:DropDownList ID="ddLocation" runat="server" Width="210px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="button" align="center">
                                                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
                                                        </td>
                                                    </tr>
                                                </table>--%>
                                                
                                                

                                                <%-- <fieldset class="noborder loginpad">
                                            <br>
                                            <div style="padding-top: 5px">
                                            </div>
                                           <br>
                                            <div style="padding-top: 5px">
                                            </div>
                                           <br />
                                            <legend class="signinbutton">
                                                </legend>
                                        </fieldset>--%>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="chkPref"/>
                                                <asp:PostBackTrigger ControlID="btnLogin"/>
                                            </Triggers>

                                        </asp:UpdatePanel>

                                        <%--<h3 class="nomargin">
                                            Recommendation
                                        </h3>
                                        <table>
                                            <tr>
                                                <td>
                                                    <p class="blue11 nomargin textjustify">
                                                        IQCare is best viewed on Internet Explorer 8. Web site visitors using earlier versions
                                                        of this or other browsers may encounter occasional format anomalies when viewing
                                                        pages from this web site.
                                                    </p>
                                                </td>--%>
                                            <%--    <td style="display: none">--%>
                                                    <%--        <img height="36" src="./Images/ribbon.gif" width="20" align="right" vspace="5" border="0" />--%>
                                                <%--</td>--%>
<%--                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                </tbody>
                            </table>--%>
                          <%--  <table width="900">
                                <tr style="width: 100%">
                                    <td align="left" style="width: 50%">
                                        <p>--%>
                                            <%--<a href="http://thepalladiumgroup.com/ " onclick="window.open('http://thepalladiumgroup.com/ '); return false;">
                                                <img src="./Images/FGI.jpg" width="99" style="margin-left: 10.5%" alt=""/>
                                            </a>--%>
                                            <%--<a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="
window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/CreativeCommon.jpg"/>
                                            </a>--%>
                                   <%--     </p>
                                    </td>--%>
<%--                                    <td align="right" style="width: 50%">
                                        <label class="right" style="width: 300">
                                            Version :
                                            <asp:Label CssClass="blue11 nomargin" ID="lblversion" Text="Version B1.0" runat="server"></asp:Label>
                                        </label>
                                        <br/>
                                        <label class="right" style="width: 300">
                                            Release Date :
                                            <asp:Label CssClass="blue11 nomargin" ID="lblrelDate" Text="Date" runat="server"></asp:Label>
                                        </label>
                                    </td>--%>
<%--                                </tr>
                            </table>--%>
                            <!-- end content area -->
                        </div>
                            <!-- </div>-->
                        </form>

                   </div><!-- .col-md-6-->

                </div><!-- .row -->

            
            
 <%--           <div class="row">
                <a href="http://thepalladiumgroup.com/ " onclick="window.open('http://thepalladiumgroup.com/ '); return false;">
                                                <img src="./Images/FGI.jpg" width="99" style="margin-left: 10.5%" alt=""/>
                                            </a>
                <h3 class="nomargin pull-left">Recommendation</h3>
                <p class="blue11 nomargin textjustify pull-left">
                                                        IQCare is best viewed on Internet Explorer 8. Web site visitors using earlier versions
                                                        of this or other browsers may encounter occasional format anomalies when viewing
                                                        pages from this web site.
                                                    </p>
            </div>--%>

            <%--<div class="=col-md-1"></div>--%>
            <hr/>

             <div class="row">
            
            <div class="col-md-1 pull-left">
                <a href="http://thepalladiumgroup.com/ " onclick="window.open('http://thepalladiumgroup.com/ '); return false;"><img src="./Images/FGI.jpg" width="99" style="margin-left: 10.5%" alt=""/></a>              
            </div>

            <div class="col-md-1"></div>
            
            <div class="col-md-1">
                <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/CreativeCommon.jpg"/></a>                                                     
            </div>

            

            <div class="col-md-4"></div>
            <div class="col-md-5 pull-right">
                <label>Version :<asp:Label CssClass="blue11 nomargin pull-right" ID="lblversion" Text="Version B1.0" runat="server"></asp:Label></label> |  
                <label>Release Date :<asp:Label CssClass="blue11 nomargin" ID="lblrelDate" Text="Date" runat="server"></asp:Label></label>              
            </div>
        
        </div><!-- .row -->

           </div><!-- .row -->
        </div><!-- .col-md-12-->
     </div><!-- .container-->

  
    <script src="Content/js/bootstrap.js" type="text/javascript"></script>
    <script src="Content/js/parsley.js" type="text/javascript"></script>
    
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
        });
   
</script>

   
</body>
</html>
