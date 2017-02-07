<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="AddTechnicalArea.aspx.cs" Inherits="IQCare.Web.Patient.AddTechnicalArea" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 <script language="javascript" type="text/javascript">
     function fnChange() {

     }
     function fncheck() {
         //var id = document.getElementById('ctl00_facilityheaderfooter_ddlTecharea').value;
         var id = document.getElementById('ctl00_IQCareContentPlaceHolder_ddlTecharea').value;
         if (id == "0") {
             alert("Select Service");
             return false;
         }
     }
    </script>
    <div class="row">
        <span class="text-capitalize pull-left glyphicon-text-size= fa-2x" id="tHeading" runat="server"> <i class="fa fa-cubes fa-3x" aria-hidden="true"></i>    Select Service</span>    
    </div><!-- .row --> <br/>
    
    <div class="row">
         <div class="panel panel-default">
         <div class="panel-heading">Patient Summary Information</div>
            <div class="panel-body">
                 <div class="row">
            
                        <div class="col-md-3">
                            <label class="text-primary">
                                <span class="fa-stack fa-lg">
                                  <i class="fa fa-circle fa-stack-2x"></i>
                                  <i class="fa fa-male fa-stack-1x fa-inverse"></i>
                                  </span> Patient Names :<asp:Label ID="lblname" runat="server"></asp:Label></label>
                        </div><!-- .col-md-2 -->

                        <div class="col-md-3">
                             <label class=" text-primary">
                                <span class="fa-stack fa-lg">
                                  <i class="fa fa-circle fa-stack-2x"></i>
                                  <i class="fa fa-check-square-o fa-stack-1x fa-inverse"></i>
                                  </span> Sex :<asp:Label ID="lblsex" runat="server"></asp:Label></label>
                        </div><!-- .col-md-2 -->

                        <div class="col-md-3">
                            <label class="text-primary">
                                <span class="fa-stack fa-lg">
                                  <i class="fa fa-circle fa-stack-2x"></i>
                                  <i class="fa fa-calendar fa-stack-1x fa-inverse"></i>
                                </span> DOB :<asp:Label ID="lbldob" runat="server"></asp:Label></label>
                        </div><!-- .col-md-2 -->

                        <div class="col-md-3">
                             <label class="text-primary">
                                <span class="fa-stack fa-lg">
                                  <i class="fa fa-circle fa-stack-2x"></i>
                                  <i class="fa fa-male fa-stack-1x fa-inverse"></i>
                                  </span>IQ Number :<asp:Label ID="lblIQno" runat="server"></asp:Label></label>
                        </div> <!--.col-md-2  -->
                 </div><!-- .row -->
              </div><!-- .panel-body-->
         </div><!-- .panel-->
    </div><!-- .row -->

    <div class="row">

          <div class="border center formbg">
                <table width="100%" cellspacing="8" cellpadding="0">
                  
                    <tr>
                        <td align="center" colspan="2" class="border center pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="right" width="40%">
                                        <label class="required control-label">
                                            *Service:</label>
                                    </td>
                                    <td align="left" width="30%">
                                        <asp:DropDownList ID="ddlTecharea" onchange="fnChange();" runat="server" Style="z-index: 2;"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlTecharea_SelectedIndexChanged" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>  <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="border center pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="middle" width="40%">
                                        <label for="txtenrollmentDate" class="required control-label" id="lblEnrollment" runat="server">
                                            *Enrollment Date:</label>
                                    </td>
                                    <td align="left"  style="white-space:nowrap" width="30%">
                                        <input id="txtenrollmentDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="pharmReportedbyDate" runat="server" class="form-control" style="z-index: 2;"/>                                        
                                    </td>
                                    <td align="left" style="white-space:nowrap" width="30%"> <img id="appDateimg2" height="22" runat="server" alt="Date Helper" hspace="5" src="../images/cal_icon.gif"
                                            width="22" border="0" name="appDateimg" visible="true" /><span class="smallerlabel"
                                                id="appDatespan2">(DD-MMM-YYYY)</span> &nbsp;
                                        <asp:Button ID="btnReEnollPatient" runat="server" Text="Re-Enroll Patient" OnClick="btnReEnollPatient_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trReEnroll" runat="server">
                        <%--<td align="center" colspan="2" class="border pad5 whitebg">--%>
                        <td align="center" colspan="2" class="border center pad5 whitebg">
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="bottom" width="50%">
                                        <label for="ReEnrollDate" class="required">
                                            *Re-Enrollment Date:</label>
                                    </td>
                                    <td align="left" width="50%">
                                        <input id="txtReEnrollmentDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="11" name="ReEnrollDate" runat="server" />
                                        <img id="imgDtReEnroll" height="22" runat="server" alt="Date Helper" hspace="5" src="../images/cal_icon.gif"
                                            width="22" border="0" name="appDateimg" visible="true" /><span class="smallerlabel"
                                                id="Span1">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlIdentFields" runat="server" EnableViewState="true">
                            </asp:Panel>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style=" padding-bottom:10px">
                            <asp:Button ID="btnSaveContinue" CssClass="btn btn-primary input-sm" runat="server" Text="Save and Continue" OnClientClick="return fncheck();"
                                OnClick="btnSaveContinue_Click" />&nbsp;
                            <asp:Button ID="btnContinue" CssClass="btn btn-success input-sm" runat="server" Text="Continue" OnClick="btnContinue_Click" />&nbsp;
                            <asp:Button ID="btnRevisit" CssClass="btn btn-warning input-sm" runat="server" Text="Revisit Check In" 
                                onclick="btnRevisit_Click" />
                        </td>
                    </tr>
                    
                </table>
            </div>

          

        </div><!-- .row -->
</asp:Content>
