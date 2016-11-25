<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="AppointmentList.ascx.cs"
    Inherits="IQCare.Web.Scheduler.AppointmentList" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<style type="text/css">
    .ajax__calendar_container
    {
        z-index: 1000;
    }
</style>
<div id="divFilter" class="row" style="display: <% = showFilter %>">
    
    <%--<div class="row">--%>
        <div class="col-md-2">
           <div class="form-group">
            <div class="col-md-12"><label class="control-label pull-left">Service Area:</label></div>
            <div class="col-md-11">
                <asp:DropDownList ID="ddlServiceAreas" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
           </div><!-- .form-group-->  
        </div><!-- .col-md-2-->
        <div class="col-md-2">
            <div class="form-group">
               <div class="col-md-12"><label class="control-label  pull-left"> Status:</label></div> 
               <div class="col-md-11">
                    <asp:DropDownList ID="ddAppointmentStatus" CssClass="form-control" runat="server">
                    </asp:DropDownList>
               </div> 
            </div>
        </div><!-- .col-md-2-->
        <div class="col-md-2">
             <div class="form-group">
               <div class="col-md-12"><label class="control-label  pull-left"> Reason:</label></div> 
               <div class="col-md-11">
                    <asp:DropDownList ID="ddlAppointmentReason" CssClass="form-control" runat="server" Width="180px">
                    </asp:DropDownList>
               </div> 
            </div>
        </div><!-- .col-md-2-->
        <div class="col-md-6">
            <div class="form-group">
                <div class="col-md-12"><label class="control-label pull-left"> Date Range:</label></div>
                
                <div class="col-md-1">
                    <small>From:</small>
                </div>

                <div class="col-md-3" style="padding-right:0%">
                    <asp:TextBox ID="txtFrom" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox>
                </div>        
                <div class="col-md-2" style="padding-left:0%">
                     <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/images/cal_icon.gif" Height="22" AlternateText="Date Helper" />
                     <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtFrom" PopupButtonID="Image1" Format="dd-MMM-yyyy" />
                </div>

                <div class="col-md-1">
                    <small>To:</small>
                </div>

                <div class="col-md-3" style="padding-right:0%">
                    <asp:TextBox ID="txtTo" MaxLength="11" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-2" style="padding-left:0%">
                     <asp:ImageButton runat="Server" ID="Image2" ImageUrl="~/images/cal_icon.gif" Height="22" AlternateText="Date Helper" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo" PopupButtonID="Image2" Format="dd-MMM-yyyy" />
                </div>
            </div><!-- .form-group-->
             
        </div><!-- .col-md-6-->
        <div class="col-md-12"><hr /></div>
        <div class="row">
           
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="col-md-6" style="padding-top:1%;padding-bottom:1%">

                    <asp:Button ID="btnSubmit" CssClass="col-md-12 btn btn-info" runat="server" Text="View" OnClick="btnSubmit_Click" />
                    <asp:HiddenField ID="hidappointment" runat="server" />
                </div>
            </div>
            <div class="col-md-4"></div>
        </div>
    
    </div><!-- .row -->
    <%--<table class="center whitebg" width="100%" border="0" cellpadding="0" cellspacing="6">
        <tr>
            <td class="border pad5" width="20%" style="margin-bottom: 10px; padding: 5px; text-align: left;">
                    <label>
                    Service Area:</label<br />
<%--                <asp:DropDownList ID="ddlServiceAreas" runat="server" Width="180px">
                </asp:DropDownList>
            </td>
            <td class=" border pad5" width="20%" style="margin-bottom: 10px; padding: 5px; text-align: left;">
                <%--<label>
                    Status:</label><br />
                <asp:DropDownList ID="ddAppointmentStatus" runat="server" Width="180px">
                </asp:DropDownList>
            </td>
            <td class="border pad5" style="margin-bottom: 10px; padding: 5px; text-align: left;">
<%--                <label>
                    Reason:</label><br />
                <asp:DropDownList ID="ddlAppointmentReason" runat="server" Width="180px">
                </asp:DropDownList>
            </td>
<%--            <td class="border pad5" id="tdDate" runat="server" visible="true" width="40%" style="margin-bottom: 10px;
                padding: 5px; text-align: left;">
                <label class="margin20" style="text-align: center">
                    Date Range:</label><br />
                <span class="smallerlabel margin10">From:</span>
                <asp:TextBox ID="txtFrom" MaxLength="11" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/images/cal_icon.gif" Height="22"
                    AlternateText="Date Helper" />
                <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtFrom"
                    PopupButtonID="Image1" Format="dd-MMM-yyyy" />
                <span class="smallerlabel margin15">To:</span>
                <asp:TextBox ID="txtTo" MaxLength="11" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton runat="Server" ID="Image2" ImageUrl="~/images/cal_icon.gif" Height="22"
                    AlternateText="Date Helper" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                    PopupButtonID="Image2" Format="dd-MMM-yyyy" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <%--<asp:Button ID="btnSubmit" runat="server" Text="View" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:HiddenField ID="hidappointment" runat="server" />
            </td>
        </tr>
    </table>
</div>--%>
<div id="divApp" class="col-md-12" >
    <table border="0" cellpadding="0" cellspacing="6" class="col-md-12">
        <tr>
            <td class="border pad5 whitebg" valign="top">
                <div class="grid">
                    <div class="rounded">
                        <div class="top-outer">
                            <div class="top-inner">
                                <div class="top">
                                    <h2>
                                        Appointments</h2>
                                </div>
                            </div>
                        </div>
                        <div class="mid-outer">
                            <div class="mid-inner">
                                <div class="mid" style="height: 300px; overflow: auto">
                                    <div id="div-gridview" class="GridView whitebg">
                                        <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" CssClass="datatable"
                                            CellPadding="0" CellSpacing="0" Width="100%" PageSize="1" AutoGenerateColumns="false"
                                            DataKeyNames="AppointmentId, PatientId, VisitId" OnSorting="grdSearchResult_Sorting"
                                            BorderWidth="0" GridLines="None" OnRowDataBound="grdSearchResult_RowDataBound"
                                            OnRowCommand="grdSearchResult_RowCommand" EmptyDataText="No appointment exists">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <RowStyle CssClass="gridrow" />
                                            <SelectedRowStyle CssClass="selectedrow" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Patient Name" InsertVisible="False" HeaderStyle-Wrap="false"
                                                    ShowHeader="true" SortExpression="FirstName" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Enrollment No" DataField="PatientEnrollmentId" ItemStyle-CssClass="textstyle"
                                                    SortExpression="PatientEnrollmentId" />
                                                <asp:TemplateField HeaderText="App. Date" InsertVisible="False" HeaderStyle-Wrap="false"
                                                    ShowHeader="true" SortExpression="AppointmentDate" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelAppDate" runat="server" Text='<%# Bind("AppointmentDate", "{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Met Date" InsertVisible="False" AccessibleHeaderText="Met Date"
                                                    HeaderStyle-Wrap="false" ShowHeader="true" SortExpression="MetDate" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelMetDate" runat="server" Text='<%# Bind("MetDate", "{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" InsertVisible="False" AccessibleHeaderText="Status"
                                                    HeaderStyle-Wrap="false" ShowHeader="true" SortExpression="Status" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelAppStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AppointmentStatus")%>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Area" InsertVisible="False" AccessibleHeaderText="Service Area"
                                                    HeaderStyle-Wrap="false" ShowHeader="true" SortExpression="ServiceArea" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelServiceArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceArea")%>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purpose" InsertVisible="False" AccessibleHeaderText="Purpose"
                                                    HeaderStyle-Wrap="false" ShowHeader="true" SortExpression="Purpose" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelPurpose" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Purpose")%>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Provider" InsertVisible="False" AccessibleHeaderText="Provider"
                                                    HeaderStyle-Wrap="false" ShowHeader="true" SortExpression="Provider" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelProvider" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Provider")%>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Notes" InsertVisible="False" AccessibleHeaderText="Notes"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="textstyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelAppNotes" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdNotes" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AppNote")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hCreateUserId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CreatedById")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hCreateUser" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CreatedBy")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hUpdateUserId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UpdatedById")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="HUpdateUser" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UpdatedBy")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hStatusDate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StatusDate")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hStatuId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AppointmentStatusId")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hPurposeId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PurposeId")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hModuleId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ServiceAreaId")%>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="hProviderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProviderId")%>'>
                                                        </asp:HiddenField>
                                                        <span style='display: <%# ShowNotes(Eval("AppNote")) %>; white-space: nowrap'>
                                                            <asp:LinkButton ID="linkNotes" runat="server" CausesValidation="false" CommandName="ViewNotes"
                                                                Text="..." CommandArgument="<%# Container.DataItemIndex %>" Font-Bold="True"></asp:LinkButton></span>
                                                        <ajaxToolkit:ModalPopupExtender ID="mpeViewNotes" runat="server" PopupControlID="pnlPopupNotes"
                                                            TargetControlID="linkNotes" CancelControlID="btnNoteClose" BackgroundCssClass="modalBackground">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopupNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                                            width: 300px; border: 3px solid #0DA9D0;">
                                                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                text-align: center; font-weight: bold;">
                                                                <br />
                                                            </div>
                                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                <%# DataBinder.Eval(Container.DataItem, "AppNote")%>
                                                            </div>
                                                            <div style="padding: 3px; text-align: right">
                                                                <asp:Button ID="btnNoteClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
    </table>
</div>
<asp:HiddenField ID="hShowFilter" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hPatientId" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hLocationId" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hServiceArea" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hAppStatus" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hAppReason" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hFromDate" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hToDate" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hUserId" runat="server" Value=""></asp:HiddenField>
