<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.ARVTherapy" CodeBehind="frmClinical_ARVTherapy.aspx.cs" %>
  <%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        function WindowPrint() {

            window.print();

        }

        function GetControl() {
            document.forms[0].submit();
        }
        function ShowValue() {
            document.getElementById('Img1').disabled = true;
            document.getElementById('Img2').disabled = true;
            document.getElementById('Img6').disabled = true;
        }
        function CalEnbleDisble(a, b, c) {
            if (a == 0) {
                document.getElementById('Img1').disabled = true;
            }
            if (b == 0) {
                document.getElementById('Img2').disabled = true;
            }
            if (c == 0) {
                document.getElementById('Img6').disabled = true;
            }
        }
        function setMonthYear() {
            var artTransferDate = document.getElementById("<%=txtanotherRegimendate.ClientID%>").value;
            if (artTransferDate != "") {
                var arrMonthDate = artTransferDate.split('-');
                if (arrMonthDate[1] != "")
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = arrMonthDate[1];
                else
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = "";

                if (arrMonthDate[2] != "")
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = arrMonthDate[2];
                else
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = "";
            }
        }
        function CalcualteBMI(txtBMI, txtWeight, txtHeight) {
            var weight = document.getElementById(txtWeight).value;
            var height = document.getElementById(txtHeight).value;
            if (weight == "" || height == "") {
                weight = 0;
                height = 0;
            }

            weight = parseFloat(weight);
            height = parseFloat(height);

            var BMI = weight / ((height / 100) * (height / 100));
            BMI = BMI.toFixed(2);
            document.getElementById(txtBMI).value = BMI;
        }

        function compareDates(dob, otherdate) {
            if (document.getElementById(otherdate).value == "") {
                return true;
            }
            var dobdd = dob.toString().substr(0, 2);
            var dobmm = dob.toString().substr(3, 3);
            var dobyr = dob.toString().substr(7, 4);
            var dmm;
            switch (dobmm.toLowerCase()) {
                case "jan": dmm = "0";
                    break;
                case "feb": dmm = "1";
                    break;
                case "mar": dmm = "2";
                    break;
                case "apr": dmm = "3";
                    break;
                case "may": dmm = "4";
                    break;
                case "jun": dmm = "5";
                    break;
                case "jul": dmm = "6";
                    break;
                case "aug": dmm = "7";
                    break;
                case "sep": dmm = "8";
                    break;
                case "oct": dmm = "9";
                    break;
                case "nov": dmm = "10";
                    break;
                case "dec": dmm = "11";
                    break;
            }
            var myDOB = new Date();
            myDOB.setFullYear(dobyr, dmm, dobdd);

            var otherdd = document.getElementById(otherdate).value.toString().substr(0, 2);
            var othermm = document.getElementById(otherdate).value.toString().substr(3, 3);
            var otheryr = document.getElementById(otherdate).value.toString().substr(7, 4);
            var omm;
            switch (othermm.toLowerCase()) {
                case "jan": omm = "0";
                    break;
                case "feb": omm = "1";
                    break;
                case "mar": omm = "2";
                    break;
                case "apr": omm = "3";
                    break;
                case "may": omm = "4";
                    break;
                case "jun": omm = "5";
                    break;
                case "jul": omm = "6";
                    break;
                case "aug": omm = "7";
                    break;
                case "sep": omm = "8";
                    break;
                case "oct": omm = "9";
                    break;
                case "nov": omm = "10";
                    break;
                case "dec": omm = "11";
                    break;
            }
            var myOther = new Date();
            myOther.setFullYear(otheryr, omm, otherdd);

            if (myDOB <= myOther) {
                return true;
            }
            else {
                alert("Date cannot be Less than Date of Birth!!");
                document.getElementById(otherdate).value = "";
                document.getElementById(otherdate).focus();
                //document.getElementById(otherdate).select();
                return false;
            }
        }
        $(document).ready(function () {

            $('#<%= ddleligibleThru.ClientID %>').change(function () {

                var element = $(this).find('option').filter(':selected').text();
               // alert(element);
                if (element == "Other") {
                    $('#divOtherEligibility').css("display", "");
                }else
                {
               $('#divOtherEligibility').css("display", "none");
                }
            });
        });
         //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_pageLoaded(ToggleCheckUncheckAllOptionAsNeeded);
    </script>
    <br />
   
    <div class="row">

        <div class="panel panel-default">

             <div class="panel-heading"><span class="fa fa-th fa-2x pull-left text-info"><strong> ART Therapy</strong></span><br /></div>

             <div class="panel-body">
                    
                  <div class="row">
                       <div class="col-md-12">
                            <h4 class="fa fa-arrow-circle-right pull-left"> ART Eligibility</h4>
                       </div>
                      <%--   <h2 class="forms" align="left"> </h2>--%>
                     <div class="col-md-12">
                          <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                 <tbody>
                                     <tr>
                                        <td class="border whitebg formcenter pad5">
                                           <div class="col-md-12"> <label id="Label2" class="control-label required pull-left">Date Medically Eligible:</label></div>
                                           <div class="col-md-4" style="padding-right:0%"><input id="txtdateEligible" class="form-control" runat="server" maxlength="11" size="10" name="txtarttransdate" /></div>
                                           <div class="col-md-2" style="padding-left:0%">
                                                <img id="img2" onclick="w_displayDatePicker('<%=txtdateEligible.ClientID%>');" height="22"
                                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span
                                                 class="smallerlabel">(DD-MMM-YYYY)</span>
                                           </div>
                                           <div class="col-md-6"></div>
                                        </td>

                                         <td class="border whitebg formcenter pad5">
                                            <div class="col-md-12"><label class="control-label required pull-right"> Eligible Through:</label></div>
                                            <div class="col-md-6 pull-right">
                                                 <asp:DropDownList ID="ddleligibleThru" CssClass="form-control" runat="Server" OnSelectedIndexChanged="ddleligibleThru_SelectedIndexChanged" AutoPostBack="False"></asp:DropDownList> 
                                            </div> 
                                        </td>
                                     </tr>
                                     
                                     <tr>
                                         <td colspan="2">
                                              <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                 <tbody>
                                                      <tr>
                                                        <td class="border whitebg formcenter pad5" style="width:25%">
                                                            <div class="col-md-12">
                                                                <label id="Label1" class="control-label pull-left"> WHO Stage:</label>
                                                            </div>
                                                            <div class="col-md-10">
                                                                <asp:DropDownList ID="lstClinicalStage" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        
                                                        <td class="border whitebg formcenter pad5" style="width:50%">
                                                            <div class="row">
                                                                
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12"><label id="control-label pull-left"> CD4:</label></div>
                                                                    <div class="col-md-6 pull-left"><asp:TextBox ID="txtCD4" CssClass="form-control" runat="server" ></asp:TextBox></div>
                                                                </div>

                                                               <div class="col-md-6 pull-right">
                                                                   <div class="col-md-12"><label id="Label4" class="control-label pull-right">CD4 %:</label></div>
                                                                   <div class="col-md-6 pull-right"> <asp:TextBox ID="txtCD4percent" CssClass="form-control" runat="server"></asp:TextBox></div>
                                                               </div>

                                                            </div>
                                                        </td>
                                                        
                                                        <td class="border whitebg formcenter pad5" style="width:25%">
                                                            <div id="divOtherEligibility">
                                                                <div class="col-md-12 "><label id="labelOthers" class="control-label pull-right" for="textOtherEligibility">Other: </label></div>
                                                                <div class="col-md-10 pull-right"><asp:TextBox ID="textOtherEligibility" CssClass="form-control" runat="server" MaxLength="100" /></div>
                                                            </div>
                                                        </td>
                                                      </tr>
                                                 </tbody>
                                              </table>
                                         </td>
                                     </tr>
                                 </tbody>
                          </table>
                     </div><!-- .col-md-12 -->
                  </div><!-- .row eligibility-->
        
                  <div class="border center formbg">

                        <h5 class="text-info" align="left">Cohort</h5>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border whitebg formcenter pad5">
                                        <div class="col-md-12"><label class="control-label pull-left">Cohort Month:</label></div>
                                        <div class="col-md-2"><input id="txtcohortmnth" class="form-control" size="10" name="CohortMonth" runat="server" readonly="readonly" /></div>
                                    </td>
                                    <td class="form   center">
                                        <div class="col-md-12"><label class="control-label pull-right">Cohort Year:</label></div>
                                        <div class="col-md-2 pull-right"><input id="txtcohortyear" class="form-control" size="10" name="CohortYear" runat="server" readonly="readonly" /></div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                  </div>
       
                  <div class="border center formbg">
         
                        <h5 class="forms" align="left">ART Start at Another Facility</h5>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border whitebg formcenter pad5">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-12"><label id="lblrARTdate" class="control-label pull-left">Start ART 1st Line Regimen Date:</label></div>
                                                <div class="col-md-4 pull-left" style=" padding-right:0%">
                                                    <input id="txtanotherRegimendate" class="form-control" runat="server" maxlength="11" size="10" name="txtanotherRegimendate"
                                                    onmouseout="setMonthYear();" />
                                                </div>
                                                <div class="col-md-2" style=" padding-left:0%">
                                                     <img id="imgdate" onclick="w_displayDatePicker('<%=txtanotherRegimendate.ClientID%>');" height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                                     border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 </div>
                                             </div>
                                            <div class="col-md-6">
                                                <div class="col-md-12"><label class="control-label pull-right"> Regimen:</label></div>
                                                <div class="col-md-1 pull-right" style=" padding-left:0%"><asp:Button ID="btnanotherRegimen" CssClass="btn btn-info" runat="server" Text="..." OnClick="btnTransRegimen_Click" /></div>
                                                <div class="col-md-4 pull-right" style=" padding-right:0%"><input id="txtanotherregimen" class="form-control" size="16" name="anotherregimen" readonly="readonly" runat="server" /></div>  
                                             </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border whitebg formcenter pad5">
                                        
                                        <div class="row">
                                             <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">Weight (Kgs): </label></div>
                                                <div class="pull-left"><input id="txtanotherwght" class="form-control" size="6" maxlength="3" name="anotherwght" runat="server" /></div>
                                             </div>
                                        
                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">Height (cm) :</label></div>
                                                <div class="pull-left"><input id="txtanotherheight" class="form-control" size="6" maxlength="3" name="anotherheight" runat="server" /></div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left"> BMI :</label></div>
                                                <div class="pull-left"><input id="txtanotherbmi" class="form-control" size="6" name="anotherbmi" runat="server" readonly="readonly" /></div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">MUAC :</label></div>
                                                <div class="pull-left"><asp:TextBox ID="textAnotherMuac" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox></div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="col-md-12"><ajaxToolkit:FilteredTextBoxExtender ID="ftemuac" runat="server" TargetControlID="textAnotherMuac"
                                                    FilterType="Numbers, Custom" ValidChars="." /></div>
                                                <div class="pull-left"><asp:RangeValidator ID="rgMuac" runat="server" ControlToValidate="textAnotherMuac"
                                                    Type="Double" MinimumValue="0" MaximumValue="300" ErrorMessage="*" Display="Dynamic"
                                                    Enabled="True" /></div>
                                           </div>

                                           <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left"> WHO Stage :</label></div>
                                                <div class="pull-left"><asp:DropDownList ID="lstanotherClinicalStage" class="form-control" runat="server"> </asp:DropDownList></div>
                                            </div>

                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
       
                  <div class="border center formbg">
           
                        <h5 class="forms" align="left"> ART Start at This Facility</h5>
                        
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border whitebg formcenter pad5">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-12"><label id="lblthisregi" class=" control-label pull-left">Start ART 1st Line Regimen Date:</label></div>
                                                <div class="col-md-4" style="padding-right:0%">
                                                    <input id="txtthisRegimendate" runat="server" class="form-control" maxlength="11" size="10" name="txtthisRegimendate" readonly="readonly" />
                                                </div>
                                                <div class="col-md-2" style="padding-left:0%"><img id="img1" height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif"
                                                    width="22" border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </div>
                                                <div class="col-md-6"></div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-12"><label class="control-label pull-right"> Regimen:</label></div>
                                                <div class="col-md-1 pull-right" style="padding-left:0%"><asp:Button ID="btnthisRegimen" CssClass="btn btn-info" runat="server" Text="..." Enabled="False" /></div>
                                                <div class="col-md-4 pull-right" style="padding-right:0%"><input id="txtthisregimen" class="form-control" size="16" name="thisregimen" readonly="readonly" runat="server" /></div>
                                            </div>
                                        </div><!-- .row -->
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border whitebg formcenter pad5">
                                        
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">Weight (Kgs):</label></div>
                                                <div class="pull-left"><input id="txtthiswght" class="form-control" size="6" name="thisweight" runat="server" readonly="readonly" /></div>
                                            </div><!-- .col-md-2-->
                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">Height (cm):</label></div>
                                                <div class="pull-left"><input id="txtthisheight" class="form-control" size="6" name="thisheight" runat="server" readonly="readonly" /></div>
                                            </div><!-- .col-md-2-->
                                            <div class="col-md-2">
                                                 <div class="col-md-12"><label class="control-label pull-left">BMI :</label></div>
                                                <div class="pull-left"><input id="txtthisbmi" class="form-control" size="6" name="thisbmi" runat="server" readonly="readonly" /></div>
                                            </div><!-- .col-md-2-->
                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">MUAC :</label></div>
                                                <div class="pull-left"><asp:TextBox ID="textMuacThis" CssClass="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox></div>
                                            </div><!-- .col-md-2-->
                                            <div class="col-md-2">
                                                <div class="col-md-12"><label class="control-label pull-left">WHO Stage:</label></div>
                                                <div class="pull-left"><asp:DropDownList ID="lstthisClinicalStage" CssClass="form-control" runat="server"  Enabled="false">
                                                </asp:DropDownList></div>
                                            </div><!-- .col-md-2-->
                                        </div><!-- .row -->
                                    </td>
                                </tr>
                            </tbody>
                        </table>
            
                    </div>
         
                  <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <center>
                                                    <h2>
                                                        Substitutions and Switches
                                                    </h2>
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdSubsARVs" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0">
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Date" DataField="ChangeDate" />
                                                            <asp:BoundField HeaderText="Regimen" DataField="regimentype" />
                                                            <asp:BoundField HeaderText="Line" DataField="RegimenLine" />
                                                            <asp:BoundField HeaderText="Why" DataField="ChangeReason" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom-outer">
                                        <div class="bottom-inner">
                                            <div class="bottom">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
     
                  <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center>
                                                        ART Treatment Interruptions</center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview2" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdInteruptions" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        Height="20px" CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0">
                                                        <%--<HeaderStyle   CssClass="tableheaderstyle" ForeColor="Blue"  Font-Underline="true"  HorizontalAlign="Left" />--%>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Stop/Lost" DataField="StopLost" />
                                                            <asp:BoundField HeaderText="Date" DataField="ARTEndDate" />
                                                            <asp:BoundField HeaderText="Why" DataField="Reason" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom-outer">
                                        <div class="bottom-inner">
                                            <div class="bottom">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="form">
                            <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="form pad5 center" colspan="2">
                            <br />
                            <asp:Button ID="btn_save"  CssClass="btn btn-info" Text="Save ART Therapy" runat="server" OnClick="btn_save_Click" />
                            <asp:Button ID="DQ_Check" CssClass="btn btn-warning" Text="Data Quality check" runat="server" OnClick="DQ_Check_Click" />
                            <asp:Button ID="btn_close" CssClass="btn btn-danger" Text="Close ART Therapy" runat="server" OnClick="btn_close_Click" />
                            <asp:Button ID="btn_print" CssClass="btn btn-info" Text="Print ART Therapy" runat="server" OnClientClick="WindowPrint()"
                                OnClick="btn_print_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
   

             </div><!-- .panel-body -->
        </div><!-- .panel-default-->

    </div><!-- .row -->
</asp:Content>
