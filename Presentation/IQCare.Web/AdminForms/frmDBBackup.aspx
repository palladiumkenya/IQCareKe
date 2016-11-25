<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.DBBackup" Codebehind="frmDBBackup.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    
      <div class="row">
            <span class="text-capitalize pull-left glyphicon-text-size= fa-2x">
               <i class="fa fa-history fa-3x" aria-hidden="true"></i><span class="text-info"> System Back-Up/Restore</span></span> 
        </div><!-- .row -->
       <hr />
       <div class="row" style='display:<% =showSetup %>'>
            <div class="pull-right">
             <asp:LinkButton runat="server" Text=" Configure Backup" data-toggle="modal" data-target="#myModal" CssClass=" fa fa-history  btn btn-info"></asp:LinkButton>
            </div>
       </div> <br />

       <div class="row">
       
            <div class="panel panel-default">

                <div class="panel-heading"><span class="pull-left">SYSTEM BACKUP/RESTORE</span><br /></div>

                <div class="panel-body">
                     <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-12"><asp:Label ID="Label1" CssClass="control-label pull-left" runat="server" Text="Backup Directory :"></asp:Label></div>
                            <div class="col-md-8"> <asp:TextBox ID="txtbakuppath" CssClass="form-control" runat="server" Text="C:\"></asp:TextBox></div>
                            <div class="col-md-12 pull-left">
                                <br />
                                <input id="chkDeidentified" class="pull-left" type="checkbox" runat="server" />
                                <label class="pull-left"  style="padding-left:1%">
                                    Make Backup of the Database with Identifiers Removed</label>
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnBackup" CssClass="btn btn-info pull-left" runat="server" Text="Backup Database" OnClick="btnBackup_Click" />
                            </div>
                        </div><!-- .col-md-6-->
                        <div class="col-md-6">
                             <div class="row" style="display:<% =showRestore %>">
                                  <div class="col--md-6">
                                       <div class="col-md-12">
                                            <asp:Label ID="Label2" CssClass="control-label pull-left" runat="server" Text="Restore File :"></asp:Label>
                                       </div><!-- .col-md-12-->
                                       <div class="col-md-8" style="padding-right:0%">
                                            <asp:TextBox ID="txtRestore" CssClass="form-control" runat="server"></asp:TextBox>
                                       </div><!-- .col-md-8-->
                                       <div class="col-md-2 pull-left" style="padding-left:0%">
                                            <asp:Button ID="btnBrowse" CssClass="btn btn-info" runat="server" Text="Browse" OnClick="btnBrowse_Click" />
                                        </div><!-- .col-md-2-->
                                     <div class="col-md-12" style="padding-top:1%;">
                                          <asp:Button ID="btnRestore" CssClass="btn btn-info pull-left" runat="server" Text="Restore Database" OnClick="btnRestore_Click" />
                                     </div>
                                  </div><!-- .col-md-6-->
                                  <div class="col--md-6"></div><!-- .col-md-6-->
                             </div><!-- .row -->

                        </div><!-- .col-md-6-->
                     </div><!-- .row -->
                </div><!-- .panel-body-->

            </div><!-- .panel panel-default-->

       </div><!-- .row -->

<%--        <h1 class="topmargin">
            System Back-Up/Restore</h1>
        <div class="center" style="padding: 5px;">
            <div class="border formbg center">
                <table class="pad5 formbg" width="100%" cellspacing="6">
                    <tr>
                        <td class="whitebg center border" valign="top" width="100%">
                            <asp:Label ID="Label1" runat="server" Text="Backup Directory :"></asp:Label>
                            <asp:TextBox ID="txtbakuppath" runat="server" Width="406px" Text="C:\"></asp:TextBox>
                            <div width="50%">
                                <input id="chkDeidentified" type="checkbox" runat="server" />
                                <label>
                                    Make Backup of the Database with Identifiers Removed</label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="center">
                            <asp:Button ID="btnBackup" runat="server" Text="Backup" OnClick="btnBackup_Click" />
                        </td>
                    </tr>
                </table>
            </div>--%>
            
            <%-- <br />
           <div class="border formbg center">
                <table class="pad5 formbg" width="100%" cellspacing="6">
                    <tr>
                        <td class="whitebg center border" valign="top" width="100%">
                            <asp:Label ID="Label2" runat="server" Text="Restore File :"></asp:Label>
                            <asp:TextBox ID="txtRestore" runat="server" Width="406px"></asp:TextBox>
                            <asp:Button ID="btnBrowse" runat="server" Text="Browse" OnClick="btnBrowse_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="center">
                            <asp:Button ID="btnRestore" runat="server" Text="Restore" OnClick="btnRestore_Click" />
                        </td>
                    </tr>
                </table>
            </div>--%>
<%--            <div>
                <table cellspacing="6" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal -->
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label><br />
                    
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel"> Backup Restore Setup</h4>
                                </div>
                                
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                             <div class="col-md-12"><label class="control-label pull-left" for="lblBackupTime"> Auto-Backup Time:</label></div>
                                             <div class="col-md-6"><asp:DropDownList CssClass="form-control pull-left input-sm" ID="ddBackupTime" runat="server"></asp:DropDownList></div>
                                        </div><!-- .col-md-6-->
                                        <div class="col-md-6">
                                            <div class="col-md-12"><label class="control-label pull-left" for="lblBackupDrive"> Backup Drive:</label></div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddBackupDrive" CssClass="form-control pull-left input-sm" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>C:</asp:ListItem>
                                                    <asp:ListItem>D:</asp:ListItem>
                                                    <asp:ListItem>E:</asp:ListItem>
                                                    <asp:ListItem>F:</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div><!-- .col-md-6-->
                                    </div><!-- .row -->
                                   

                                        
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                                        Close Setup</button>
                                    <%--<button type="button"  class="btn btn-primary">
                                        Save changes</button>--%>
                                        <asp:Button ID="cmdSave" CssClass="btn btn-info" OnClick="cmdSave_Click" runat="server" Text="Save Setup" data-dismiss="modal"  />
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
    </asp:UpdatePanel>



    <script language="javascript" type="text/javascript">
        function GetControl() {
            document.forms[0].submit();
        }
    </script>

</asp:Content>
