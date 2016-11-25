<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/IQCare.master"
    AutoEventWireup="True" Inherits="IQCare.Web.Clinical.ChildEnrolment"
    Title="Untitled Page" EnableViewState="true" Codebehind="frmChildEnrolment.aspx.cs" %>
    <%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    
  <div class="row">
        <span class="text-capitalize glyphicon-text-size= fa-2x pull-left"> <i class="fa fa-child" aria-hidden="true"></i> Child Enrollment</span>
        <div class="col-md-12"><hr /></div>
  </div>
                

  <%--  <div class="row">--%>



           
       
        <div class="row" id="DivPMTCT" runat="server">
            <%--<h1 class="margin">
                Child Enrollment</h1>--%>
            
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="width: 500px" class="border pad5 whitebg">
                               <div class="col-md-12"><label id="lblPName" class="control-label required pull-left" for="patientname"> *Patient Name:</label></div> 
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                 <span id="FName" class="smallerlabel control-label pull-left">First: </span>
                                            </div>
                                            <div class="col-md-9">
                                                 <asp:TextBox ID="TxtFirstName" runat="server" MaxLength="50" CssClass="form-control input-sm" OnTextChanged="TxtFirstName_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div><!-- .col-md-4-->
                                    <div class="col-md-4">
                                        <div class="col-md-12">
                                          <span id="Span1" class="smallerlabel control-label pull-left">Mid: </span>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="TxtMidName" runat="server" MaxLength="50" CssClass="form-control input-sm" OnTextChanged="TxtMidName_TextChanged"></asp:TextBox>
                                        </div>

                                    </div><!-- .col-md-4-->
                                    <div class="col-md-4">
                                        <div class="col-md-12">
                                            <span id="LName" class="smallerlabel control-label pull-left">Last: </span>
                                        </div>
                                        <div class="col-md-9">
                                          <asp:TextBox ID="TxtLastName" runat="server" MaxLength="50" CssClass="form-control input-sm" OnTextChanged="TxtLastName_TextChanged"></asp:TextBox>
                                        </div>
                                    </div><!-- .col-md-4-->
                                </div>

                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <div></div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-12"><label id="lblregistrationdate" class="required pull-left" for="TxtRegistrationDate"> *Registration Date:</label></div>
                                        <div class="col-md-4" style="padding-right:0%">
                                            <asp:TextBox ID="TxtRegistrationDate" CssClass="form-control"  runat="server" MaxLength="11"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2" style="padding-left:0%">
                                             <img onclick="w_displayDatePicker('<%= TxtRegistrationDate.ClientID %>');" height="22"
                                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 500px; white-space:nowrap" class="border pad5 whitebg" >
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-12"><label id="lblgender" class="required control-label pull-left" for="DDGender">*Sex:</label></div>
                                            <div class="col-md-12">
                                                 <asp:DropDownList ID="DDGender" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="" Selected="True">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="16">Male</asp:ListItem>
                                                    <asp:ListItem Value="17">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div><!-- .col-md-4-->
                                    <div class="col-md-6">
                                        <div class="form-group" style="padding-right:0%">
                                            <div class="col-md-12"><label id="lblDOB" class="required pull-left" for="TxtDOB">*Date of Birth:</label></div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TxtDOB" runat="server" CssClass="form-control input-sm"  MaxLength="11"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="padding-left:0%">
                                                 <img onclick="w_displayDatePicker('<%=TxtDOB.ClientID %>');" height="22" alt="Date Helper"
                                                    hspace="3" src="../images/cal_icon.gif" width="20" border="0" />
                                                <span class="smallerlabel">DD-MMM-YYYY </span>
                                            </div>
                                            <div class="col-md-2"></div>
                                        </div>
                                    </div><!-- .col-md-4-->
                                    <div class="col-md-2"></div><!-- .col-md-4-->
                                </div>
                               
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <div class="row">
                                    <div class="col-md-12"><label id="Label1" class="required pull-left" for="TxtAdmissionNo"> *HEI No:</label></div>
                                    <div class="col-md-12">
                                         <asp:TextBox ID="txtHEINo" CssClass="form-control" AutoPostBack="true" runat="server" Width="25%" MaxLength="12"
                                    OnTextChanged="txtHEINo_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                
                                   

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" width="100%x" border="0">
                                    <tr>
                                        <td align="right" style="padding-top:1%;padding-bottom:1%">
                                            <asp:Button ID="btnAdd" CssClass="btn btn-info" Text="Add Child" runat="server" OnClick="btnAdd_Click1">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                            </td>
                        </tr>
                        <tr>
                            <td class="whitebg border" valign="top" colspan="2">
                                <div id="div-gridview" class="GridView whitebg">
                                    <asp:GridView ID="grdChildInfo" runat="server" EnableViewState="true" Width="100%"
                                        BorderColor="#666699" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                                        OnRowDataBound="grdChildInfo_RowDataBound" OnSorting="grdChildInfo_Sorting" OnRowDeleting="grdChildInfo_RowDeleting"
                                        OnSelectedIndexChanging="grdChildInfo_SelectedIndexChanging" OnRowCommand="grdChildInfo_RowCommand">
                                        <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Center"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="White" BorderColor="Silver" />
                                        <Columns>
                                            <asp:BoundField HeaderText="First Name" DataField="FirstName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Middle Name" DataField="MiddleName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue" Visible="false">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Last Name" DataField="LastName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Registration Date" DataField="RegistrationDate" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Sex" DataField="Sex" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="DOB" DataField="DOB" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="HEI No" DataField="HEIIDNumber" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Delete" DataField="Id" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="false" ItemStyle-Width="0%" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue" Visible="true">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="0%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Link" DeleteText="<img src='../Images/del.gif' alt='Delete this' border='0' />"
                                                ShowDeleteButton="true" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="cmdBind" CssClass="btn btn-info fa fa-child" runat="server" ID="LbGridChildEnrol">Show</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <asp:TextBox ID="txtSysDate" runat="server" CssClass="textstylehidden"></asp:TextBox>
                            <td align="center" style="padding-top:1%;padding-bottom:1%">
                                <asp:Button ID="btnsave" CssClass="btn btn-info" Text="Save Child" runat="server" OnClick="btnsave_Click"></asp:Button>
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger" Text="Close" runat="server" OnClick="btnCancel_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
<%--   </div>--%>
</asp:Content>
