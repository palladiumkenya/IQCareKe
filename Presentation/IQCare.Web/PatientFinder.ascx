<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PatientFinder.ascx.cs"
    Inherits="IQCare.Web.PatientFinder" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<style type="text/css">
    .ajax__calendar_container {
        z-index: 1000;
    }
</style>
<div class="container-fluid">
    <div class="row ">
        <i class="fa fa-cogs fa-3x pull-left" aria-hidden="true"></i><span class="text-capitalize pull-left glyphicon-text-size= fa-2x"
            id="lblServiceArea" runat="server"></span>
    </div>
    <!-- .row -->
    <br />
    <div class="row">
        <div class="well well-sm">
            <span class="fa-stack fa-lg pull-left"><i class="fa fa-circle fa-stack-2x"></i><i
                class="fa fa-male fa-lg fa-stack-1x fa-inverse"></i></span><span class="tex-primary pull-left">Find/Add Patient</span><hr />
            <asp:UpdatePanel ID="panelSearch" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnView">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group  col-md-10">

                                    <label id="lblidentificationno" class="control-label pull-left" runat="server">
                                        Identification Number:</label>

                                    <asp:DropDownList ID="ddlIdentifier" runat="server" class="form-control" Style="height: inherit; display: none">
                                        <asp:ListItem Text=" Identifier:"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtidentificationno" CssClass="form-control input-sm" runat="server"
                                        MaxLength="20" placeholder="Identifier"></asp:TextBox>


                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEID" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtidentificationno" ValidChars="-@*/\_()| ">
                                    </ajaxToolkit:FilteredTextBoxExtender>


                                </div>
                                <div class="form-group col-md-10">
                                    <label for="FTEPhnoe" class="control-label pull-left">
                                        Phone Number:</label>
                                    <asp:TextBox ID="textPhoneNumber" class="form-control input-sm" runat="server" MaxLength="20"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEPhnoe" runat="server" FilterType="Numbers"
                                        TargetControlID="textPhoneNumber" />
                                </div>
                                <div class="form-group col-md-10">
                                    <label for="ddFacility" class="control-label pull-left">
                                        Facility/Satellite:</label>
                                    <asp:DropDownList ID="ddFacility" CssClass="form-control input-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <!-- .col-md-10 -->
                            </div>
                            <!-- .col-md-4 -->
                            <div class="col-md-4">
                                <div class="form-group col-md-10">
                                    <label for="FTELName" class="control-label pull-left">
                                        Last Name:</label>
                                    <asp:TextBox ID="txtlastname" class="form-control input-sm" runat="server" MaxLength="20"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTELName" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtlastname" ValidChars="-,.@*' " />
                                </div>
                                <div class="form-group col-md-10">
                                    <label class="control-label pull-left" runat="server" id="lblmiddlename" for="FTEMName">
                                        Middle Name:</label>
                                    <asp:TextBox ID="txtmiddlename" CssClass="form-control input-sm" runat="server" MaxLength="20"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEMName" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtmiddlename" ValidChars="-,.@*' " />
                                </div>
                                <div class="form-group col-md-10">
                                    <label for="FTEFName" class="control-label pull-left">
                                        First Name:</label>
                                    <asp:TextBox ID="txtfirstname" CssClass="form-control input-sm" runat="server" MaxLength="20"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEFName" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtfirstname" ValidChars="-,.@*' " />
                                </div>
                                <!-- .form-group-->
                            </div>
                            <!-- .col-md-4 -->
                            <div class="col-md-4">
                                <div class="form-group col-md-10">
                                    <div class="col-md-12">
                                        <label class="control-label pull-left" for="txtDOB">
                                            Date of Birth:</label>
                                    </div>
                                    <div class="col-md-8" style="white-space: nowrap;">
                                        <asp:TextBox CssClass="form-control input-sm col-md-6" ID="txtDOB" runat="server"
                                            MaxLength="11" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onfocus="javascript:vDateType='3'"></asp:TextBox>
                                        <img onclick="w_displayDatePicker('<%= txtDOB.ClientID %>');" height="22" alt="Date Helper"
                                            hspace="3" src="../Images/cal_icon.gif" width="20" border="0" style="z-index: auto" />
                                        <%--  <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="width: 22; height: 22;
                                        z-index: auto; padding-left: 5px" ImageUrl="./images/cal_icon.gif" ImageAlign=" Bottom"
                                        AlternateText="Click to show calendar" />
                                    <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtDOB"
                                        PopupButtonID="Image1" EnabledOnClient="True" Format="dd-MMM-yyyy" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-10">
                                    <div class="col-md-12">
                                        <label for="textRegistrationDate" class="control-label pull-left">
                                            Registration Date:</label>
                                    </div>
                                    <div class="col-md-8" style="white-space: nowrap; position: relative">
                                        <asp:TextBox CssClass="form-control col-md-6 input-sm" ID="textRegistrationDate"
                                            runat="server" AutoComplete="false" MaxLength="11" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"></asp:TextBox>
                                        <img onclick="w_displayDatePicker('<%= textRegistrationDate.ClientID %>');" height="22"
                                            alt="Date Helper" hspace="3" src="../Images/cal_icon.gif" width="20" border="0"
                                            style="z-index: auto" />
                                    </div>
                                </div>
                                <div class="form-group col-md-10" style="display: <% = showService %>">
                                    <label class="control-label pull-left" for="ddlServices">
                                        Service:</label>
                                    <asp:DropDownList ID="ddlServices" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-10">
                                    <div class="form-group col-md-5">
                                        <label for="ddSex" class="control-label pull-left">
                                            Sex/ Gender :</label>
                                        <asp:DropDownList ID="ddSex" CssClass="form-control input-sm" runat="server">
                                            <asp:ListItem Selected="True" Value="">-Select-</asp:ListItem>
                                            <asp:ListItem Value="16">Male</asp:ListItem>
                                            <asp:ListItem Value="17">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <!-- .form-group -->
                                    <div class="form-group col-md-5" style="display: <% = showStatus %>">
                                        <div>
                                            <label class="control-label pull-left" for="ddCareEndedStatus">
                                                Patient Status:</label>
                                            <asp:DropDownList CssClass="form-control input-sm" ID="ddCareEndedStatus" runat="server">
                                                <asp:ListItem Value="" Selected="True">-Any-</asp:ListItem>
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Care Ended</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <!--.form-group -->
                            </div>
                            <!-- .col-md-4 -->
                        </div>

                        <div class="row">
                            <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="bg-danger: #FFFFC0; border: solid 1px #C00000"
                                HorizontalAlign="Left" Visible="false">
                                <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                    Text=""></asp:Label>
                            </asp:Panel>
                        </div>
                        <!-- .row -->
                        <div class="row">
                            <asp:Panel ID="panelSearchAction" DefaultButton="btnView" runat="server">
                                <asp:HiddenField ID="HIncludeEnrollment" runat="server" />
                                <asp:HiddenField ID="HFilterByServiceLine" runat="server" />
                                <asp:HiddenField ID="HSelectedServiceLine" runat="server" />
                                <asp:HiddenField ID="HFilterByStatus" runat="server" />
                                <asp:HiddenField ID="HMaxRecord" runat="server" />
                                <asp:HiddenField ID="HAutoLoad" runat="server" />
                                <asp:HiddenField ID="HCanAdd" runat="server" />
                                <asp:HiddenField ID="HCanDelete" runat="server" Value="FALSE" />
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-8" style="margin-bottom: 1%">
                                        <div class="col-md-3">
                                            <asp:Button CssClass="btn btn-info col-md-12" ID="btnView" runat="server"
                                                OnClick="btnView_Click" Text=" Find" Style="margin-right: 5px" />
                                        </div>
                                        <div class="col-md-3" style="display: <% =  showAdd %>">
                                            <asp:Button CssClass="btn btn-primary col-md-12" ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                Text="Add Patient" Style="margin-right: 5px" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnCancel" CssClass="btn btn-danger col-md-12" runat="server" OnClick="btnCancel_Click"
                                                Text="Cancel" />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                            </asp:Panel>
                        </div>
                        <!-- .row -->
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- .jumbotron -->
        <div class="row">
            <asp:UpdatePanel ID="panelResults" runat="server">
                <ContentTemplate>
                    <div class="grid">
                        <div class="rounded">
                            <div class="top-outer">
                                <div class="top-inner">
                                    <div class="top">
                                        <h2>
                                            <asp:Label runat="server" ID="labelNote"></asp:Label></h2>
                                    </div>
                                </div>
                            </div>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="cursor: pointer; height: 380px; overflow: auto; border: 1px solid #666699;">
                                        <div id="div-gridview" class="whitebg">
                                            <asp:HiddenField ID="resultOpenITem" runat="server" />
                                            <asp:GridView ID="grdSearchResult" runat="server" Width="100%" OnRowDataBound="grdSearchResult_RowDataBound"
                                                PageSize="1" CssClass="datatable table-striped table-responsive" AutoGenerateColumns="False"
                                                CellPadding="0" BorderWidth="0px" GridLines="None" AllowSorting="True" DataKeyNames="patientid,locationid"
                                                OnRowCommand="grdSearchResult_RowCommand" OnRowCreated="grdSearchResult_RowCreated"
                                                OnPreRender="grdSearchResult_PreRender" OnSorting="grdSearchResult_Sorting">
                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                                                    HorizontalAlign="Left"></HeaderStyle>
                                                <RowStyle Height="30" CssClass="gridrow" />
                                                <Columns>
                                                    <asp:TemplateField Visible="true" ItemStyle-Width="32px">
                                                        <ItemTemplate>
                                                            <span style="display: <%# showIdentifiers %>; width: 20px">
                                                                <asp:ImageButton ID="ExpandGridButton" runat="server" CommandName="Expand" ImageUrl="~/Images/plus.png"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" /></span>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="32px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Facility ID">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelPatientFacilityId" runat="server" Text='<%# Bind("PatientFacilityId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CCC Number">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelPatientEnrollmentId" runat="server" Text='<%# Bind("PatientEnrollmentID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="First Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelFirstName" runat="server" Text='<%# Bind("firstname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Middle Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelMiddleName" runat="server" Text='<%# Bind("middlename") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelLastName" runat="server" Text='<%# Bind("lastname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOB">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelDOB" runat="server" Text='<%# Bind("dob", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sex">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelSex" runat="server" Text='<%# Bind("sex") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reg Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelRegDate" runat="server" Text='<%# Bind("RegistrationDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone">

                                                        <ItemTemplate>
                                                            <asp:Label ID="labelPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Facility" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelFacilityName" runat="server" Text='<%# Bind("FacilityName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="labelStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            </td></tr><tr>
                                                                <td colspan="100%">
                                                                    <asp:Panel ID="ContainerDiv" runat="server" Style="display: none; position: relative; left: 5px;">
                                                                        <asp:GridView ID="gridPatientServiceList" runat="server" AllowSorting="False" AutoGenerateColumns="False"
                                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive"
                                                                            DataKeyNames="moduleid,patientid,locationid" Enabled="true" EnableModelValidation="True"
                                                                            GridLines="None" HorizontalAlign="Left" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ModuleName" HeaderText="Service Area" />
                                                                                <asp:BoundField DataField="EnrollmentDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Enrollment Date" />
                                                                                <asp:BoundField DataField="CareStatus" HeaderText="Status" />
                                                                                <asp:TemplateField HeaderText="Identifiers">
                                                                                    <ItemTemplate>
                                                                                        <asp:Repeater ID="repeaterIdentifiers" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                <table style="width: 100%">
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td style="width: 80%;">
                                                                                                        <asp:Label ID="labelIdentifier" Style="color: blue; font-size: 9pt; font-weight: bold; display: inline-block;"
                                                                                                            runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IdentifierName]")%>' />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="identifierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IdentifierValue]")%>'
                                                                                                            Font-Bold="true" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                            <AlternatingItemTemplate>
                                                                                                <tr style="background-color: #EFEFEF">
                                                                                                    <td style="width: 80%;">
                                                                                                        <asp:Label ID="labelIdentifier" Style="color: blue; font-size: 9pt; font-weight: bold; display: inline-block;"
                                                                                                            runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IdentifierName]")%>' />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="identifierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IdentifierValue]")%>'
                                                                                                            Font-Bold="true" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </AlternatingItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                </table>
                                                                                            </FooterTemplate>
                                                                                        </asp:Repeater>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />
                                                                            <RowStyle CssClass="gridrow" />
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
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
                            <asp:Button ID="btnPaxId" runat="server" Style="display: none" /><ajaxToolkit:ModalPopupExtender
                                ID="mpePaxId" runat="server" PopupControlID="panelPaxId" TargetControlID="btnPaxId"
                                BehaviorID="bhxPaxId43" CancelControlID="buttonCancelPaxId" BackgroundCssClass="modalBackground"
                                PopupDragHandleControlID="divTitle">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="panelPaxId" runat="server" Style="display: none; border: solid 1px #808080; width: 500px;">
                                <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px; cursor: move; height: 18px; width: 500px;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; height: 18px">
                                        <tr>
                                            <td style="width: 5px; height: 19px;"></td>
                                            <td style="width: 100%; height: 19px;">
                                                <span style="font-weight: bold;">
                                                    <asp:Label ID="labelParamTitle" runat="server">Identifiers </asp:Label><asp:Label
                                                        ID="labelReceipt" runat="server" /></span>
                                            </td>
                                            <td style="width: 5px; height: 19px;"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Repeater ID="rptPatientServiceList" runat="server" OnItemDataBound="OnItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered">
                                            <tr>
                                                <td>Service Area
                                                </td>
                                                <td>Enrollment Date
                                                </td>
                                                <td>Status
                                                </td>
                                                <td style="display: table-cell;" data-hide="phone">Identifiers
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("ModuleName")%>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "[EnrollmentDate]","{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("CareStatus")%>
                                            </td>
                                            <td>
                                                <asp:Repeater ID="repeaterIdentifiers" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-striped table-bordered">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 80%; white-space: nowrap">
                                                                <asp:Label ID="labelIdentifier" Style="font-size: 9pt; font-weight: bold; display: inline-block;"
                                                                    runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IdentifierName]")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="identifierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IdentifierValue]")%>'
                                                                    Font-Bold="true" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <div style="padding: 6px;" align="center">
                                    <asp:Button ID="buttonCancelPaxId" runat="server" Text="Close" ForeColor="DarkBlue" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!-- .row datagrid -->
    </div>
    <!-- .row -->
    <div class="row">
    </div>
</div>
<%--        </div>
    </div>
</div>--%>
