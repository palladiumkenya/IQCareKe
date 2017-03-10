<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.ARTHistory" EnableEventValidation="false" CodeBehind="frmClinical_ARTHistory.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="ConARTHistory" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div class="row">
        <script language="javascript" type="text/javascript">
            function GetControl() {
                document.forms[0].submit();
            }

            function WindowPrint() {

                window.print();
            }

            function EnableDis(a) {
                var rdoVal = a;
                if (rdoVal.value == "Y") {
                    document.getElementById('<%=ddlpurpose.ClientID%>').disabled = false;
                    document.getElementById('<%=txtRegimen.ClientID%>').disabled = false;
                    document.getElementById('<%=txtLastUsed.ClientID%>').disabled = false;
                    document.getElementById('<%=btnRegimen.ClientID%>').disabled = false;
                    document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = false;
                    document.getElementById('Img1').disabled = false;
                }
                else {
                    document.getElementById('<%=ddlpurpose.ClientID%>').disabled = true;
                    document.getElementById('<%=txtRegimen.ClientID%>').disabled = true;
                    document.getElementById('<%=txtLastUsed.ClientID%>').disabled = true;
                    document.getElementById('<%=btnRegimen.ClientID%>').disabled = true;
                    document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = true;
                    document.getElementById('Img1').disabled = true;
                }
            }
        </script>
        <div class="panel panel-default">
            <div class="panel-heading" style="text-align: left">
                <span class="fa fa-hospital-o"></span><span class="text-info">ART History</span>
                <br />
            </div>
            <div class="panel-body">
                <div class="border center formbg">
                    <h4 class="forms text-info" align="left">
                        Transfer In</h4>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0" id="divwhoto">
                        <tbody>
                            <tr>
                                <td class="border pad6 whitebg" align="center" width="50%">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left" for="transferInDate">
                                            Transfer In Date:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <div class=" input-group">
                                            <input id="txtTransferInDate" class="form-control" onblur="DateFormat(this,this.value,event,false,'3')"
                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                maxlength="11" name="txtTransferInDate" runat="server" />
                                            <span class="input-group-addon">
                                                <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtTransferInDate.ClientID%>');"
                                                    hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-6" style="padding-right: 0%">
                                       
                                    </div>
                                    <div class="col-md-2" style="padding-left: 0%">
                                        <img id="Img4" onclick="w_displayDatePicker('<%=txtTransferInDate.ClientID%>');"
                                            height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" />
                                        <span class="smallerlabel" id="Span3">(DD-MMM-YYYY)</span>
                                    </div>--%>
                                    <div class="col-md-4">
                                    </div>
                                </td>
                                <td class="border pad6 whitebg" align="center" width="50%">
                                    <div class="col-md-12">
                                        <label id="lblenroldate" class="control-label pull-left" for="District">
                                            From County:</label></div>
                                    <%-- <asp:DropDownList ID="dddistrict" runat="server">
                            </asp:DropDownList>--%>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtdistrict" CssClass="form-control pull-right" MaxLength="50" runat="server"></asp:TextBox></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad6 whitebg" align="center" width="50%">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left" style="padding-left: 0px" for="transferInDate">
                                            Facility:</label></div>
                                    <%--<asp:DropDownList ID="ddfacility" runat="server">
                            </asp:DropDownList>--%>
                                    <div class="col-md-8 pull-left">
                                        <asp:TextBox ID="txtFacility" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox></div>
                                </td>
                                <td class="border pad6 whitebg" align="center" width="50%">
                                    <div class="col-md-12">
                                        <label id="Label2" class="control-label pull-left" for="District">
                                            Date Started ART:</label></div>
                                    <div class="col-md-6">
                                        <div class=" input-group">
                                            <input id="txtDateARTStarted" class="form-control" onblur="DateFormat(this,this.value,event,false,'3')"
                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                maxlength="11" name="txtTransferInDate" runat="server" />
                                            <span class="input-group-addon">
                                                <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtDateARTStarted.ClientID%>');"
                                                    hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%--</div>
                <div class="col-md-6 pull-right">
                </div>--%>
                </div>
                <br />
                <div class="border center formbg">
                    <h4 class="forms text-info" align="left">
                        Prior ART</h4>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="form">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="4" align="left">
                                                <div class="col-md-12">
                                                    <label class="control-label pull-left" id="lblpurpose" runat="server">
                                                        Prior ART:</label>
                                                    <input id="rbtnknownYes" runat="server" onmouseup="up(this);" onfocus="up(this);"
                                                        onclick="down(this); EnableDis(this)" type="radio" value="Y" name="PriorART" />
                                                    <label for="y">
                                                        Yes</label>
                                                    <input id="rbtnknownNo" runat="server" onmouseup="up(this);" onfocus="up(this);"
                                                        onclick="down(this); EnableDis(this)" type="radio" value="N" name="PriorART" />
                                                    <label for="n">
                                                        No</label></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%" align="right">
                                                <div class="col-md-12">
                                                    <label class="control-label pull-left" id="Label1" runat="server">
                                                        Purpose:</label></div>
                                                <div class="col-md-12 pull-left">
                                                    <asp:DropDownList CssClass="form-control" ID="ddlpurpose" runat="server" Style="z-index: 2">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 4%" align="right">
                                            </td>
                                            <td style="width: 25%" align="center">
                                                <div class="col-md-12">
                                                    <label class="control-label pull-left">
                                                        Regimen:</label></div>
                                                <div class="col-md-9">
                                                    <div class=" input-group">
                                                        <asp:TextBox ID="txtRegimen" CssClass="form-control" MaxLength="50" runat="server"
                                                            Enabled="False"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:Button ID="btnRegimen" CssClass="btn btn-warning" MaxLength="20" runat="server"
                                                                Text="..." OnClick="btnRegimen_Click" Enabled="false"></asp:Button>
                                                        </span>
                                                    </div>
                                                </div>
                                                <%--<div class="col-md-9" style="padding-right: 0%">
                                                    <asp:TextBox ID="txtRegimen" CssClass="form-control" MaxLength="50" runat="server"
                                                        Enabled="False"></asp:TextBox></div>
                                                <div class="col-md-2" style="padding-left: 0%">
                                                    <asp:Button ID="btnRegimen" CssClass="btn btn-warning" MaxLength="20" runat="server"
                                                        Text="..." OnClick="btnRegimen_Click" Enabled="false"></asp:Button></div>
                                                <div class="col-md-1">
                                                </div>--%>
                                            </td>
                                            <td style="width: 30%" align="left">
                                                <div class="col-md-12">
                                                    <label class="control-label pull-left">
                                                        Date Last Used:</label></div>
                                                <div class="col-md-8" style="padding-right: 0%">
                                                    <div class=" input-group">
                                                        <asp:TextBox ID="txtLastUsed" runat="server" CssClass="form-control" MaxLength="11"
                                                            onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtLastUsed.ClientID%>');"
                                                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                            <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad5 formbg border" colspan="2">
                                    <div id="divbtnPriorART" class="whitebg" align="center">
                                        <asp:Button ID="btnAddPriorART" CssClass="btn btn-info" Text="Add Prior ART" runat="server"
                                            OnClick="btnAddPriorART_Click" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad5 formbg border" colspan="2">
                                    <div class="grid" id="divDrugAllergyMedicalAlr" style="width: 100%;">
                                        <div class="rounded">
                                            <div class="top-outer">
                                                <div class="top-inner">
                                                    <div class="top">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 200px; overflow: auto">
                                                        <div id="div-gridview" class="GridView whitebg">
                                                            <asp:GridView Height="50px" ID="GrdPriorART" runat="server" AutoGenerateColumns="False"
                                                                Width="100%" AllowSorting="true" BorderWidth="1" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                                CellPadding="0" CellSpacing="0" HeaderStyle-HorizontalAlign="Left" RowStyle-CssClass="gridrow"
                                                                OnRowDataBound="GrdPriorART_RowDataBound" OnSelectedIndexChanging="GrdPriorART_SelectedIndexChanging"
                                                                OnRowDeleting="GrdPriorART_RowDeleting">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <RowStyle CssClass="gridrow" />
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
                <br />
                <div class="border center formbg">
                    <h2 class="forms" align="left">
                        HIV Care</h2>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="border pad6 whitebg" align="center" width="50%">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left" id="lblHIVConfirmHIVPosDate">
                                            Date Confirmed HIV Positive:</label>
                                    </div>
                                    <div class="col-md-8">
                                        <div class=" input-group">
                                            <input id="txtHIVConfirmHIVPosDate" class="form-control" onblur="DateFormat(this,this.value,event,false,'3')"
                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                maxlength="11" name="HIVConfirmHIVPosDate" runat="server" />
                                            <span class="input-group-addon">
                                                <img height="22" alt="Date Helper" style="padding-left: 0px" onclick="w_displayDatePicker('<%=txtHIVConfirmHIVPosDate.ClientID%>');"
                                                    hspace="5" src="../images/cal_icon.gif" width="22" border="0" />
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span></span>
                                        </div>
                                    </div>
                                </td>
                                <td class="border pad6 whitebg" width="50%" align="center">
                                    <div class="col-md-12">
                                        <label id="lblwhere" class="control-label pull-left" for="District">
                                            Where:</label></div>
                                    <div class="col-md-8">
                                        <asp:TextBox CssClass="form-control" ID="txtWhere" MaxLength="50" runat="server"></asp:TextBox></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad6 whitebg" width="50%" align="center">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left" id="lblEnrolHIVCare">
                                            Date Enrolled in HIV Care:</label></div>
                                    <div class="col-md-8">
                                        <div class=" input-group">
                                            <input id="txtEnrolledinHIVCare" class="form-control" onblur="DateFormat(this,this.value,event,false,'3')"
                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                maxlength="11" name="EnrolledinHIVCare" runat="server" />
                                            <span class="input-group-addon">
                                                <img id="Img2" onclick="w_displayDatePicker('<%=txtEnrolledinHIVCare.ClientID%>');"
                                                    height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                    border="0" name="appDateimg" />
                                                <span class="smallerlabel" id="Span2">(DD-MMM-YYYY)</span> </span>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2" style="padding-left: 0%">
                                        
                                    </div>--%>
                                </td>
                                <td class="border pad6 whitebg" width="50%" align="center">
                                    <div class="col-md-12">
                                        <label id="lblWHOStage" class="control-label pull-left" for="WHOStage">
                                            WHO Stage:</label></div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ddlWHOStage" CssClass="form-control" runat="server" Style="z-index: 2">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div class="border center formbg">
                    <h2 class="forms" align="left">
                        Drug Allergies
                    </h2>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0" style="height: 73px">
                        <tbody>
                            <tr>
                                <td class="form">
                                    <textarea id="txtAreaAllergy" class="form-control" rows="4" cols="1" runat="server"
                                        style="width: 99%" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form">
                                    <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                        Wrap="true">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div class="border center formbg">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr align="center">
                                <td class="form">
                                    <asp:Button ID="btnsave" CssClass="btn btn-info" runat="server" Text="Save ART History"
                                        OnClick="btnsave_Click" />
                                    <asp:Button ID="btncomplete" CssClass="btn btn-warning" runat="server" Text="Data Quality Check"
                                        OnClick="btncomplete_Click" />
                                    <asp:Button ID="btnback" runat="server" CssClass="btn btn-danger" Text="Close" OnClick="btnback_Click" />
                                    <asp:Button ID="btnPrint" CssClass="btn btn-info" Text="Print" OnClientClick="WindowPrint()"
                                        runat="server" OnClick="btnPrint_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- .panel-body-->
        </div>
        <!-- .panel-->
    </div>
    <!-- .row -->
</asp:Content>
