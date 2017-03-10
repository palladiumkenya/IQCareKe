<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="LabTestMaster.aspx.cs" Inherits="IQCare.Web.Laboratory.Admin.LabTestMaster" %>

<%--
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="../../progressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../../Incl/quicksearch.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        $(function () {
            $("input[name$='chkGroup']").click(function () {

                var chkGroupNo = $('#<%=chkGroupNo.ClientID %>');
                if (chkGroupNo.is(":checked")) {
                    $("#divNonGroup").css('display', 'block');
                } else {
                    $("#divNonGroup").css('display', 'none');
                }
            });
        });
        function ShowModalPopup() {
            $('#<%=textLabName.ClientID %>').val("");
            $('#<%=textReference.ClientID %>').val("");
            $("#<%=chkGroupNo.ClientID %>").prop("checked", true);
            $("#<%=chkGroupYes.ClientID %>").prop("checked", false);
            $find("labtext_bhx").show();
            return false;
        }

        function HideModalPopup() {
            $find("labtext_bhx").hide();
            return false;
        }

        function InferReference() {
            var str = $('#<%=textLabName.ClientID %>').val();
            str = str.replace(/\s+/g, '_').toUpperCase();
            if (str.length < 37) {
                $('#<%=textReference.ClientID %>').val(str);
            }
        }
    </script>
    <div class="row ">
        <i class="fa fa-cogs fa-3x pull-left" aria-hidden="true"></i><span class="text-capitalize pull-left glyphicon-text-size= fa-2x">Laboratory Tests </span>
    </div>
    <hr />
    <div>
        <%--        <h1 id="H1" runat="server" class="margin" style="padding-left: 10px;">
            Laboratory Tests
        </h1>--%>
        <div class="center">
            <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="true">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 300px; overflow: auto">
                                                <div id="grd_custom" class="GridView whitebg">
                                                    <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gridlabMaster" runat="server" OnRowDataBound="gridlabMaster_RowDataBound"
                                                                AutoGenerateColumns="False" Width="100%" AllowSorting="False" BorderWidth="0px"
                                                                GridLines="None" CssClass="datatable" DataKeyNames="Id" CellPadding="0" OnSorting="grdLab_Sorting"
                                                                OnRowCommand="gridlabMaster_RowCommand" OnDataBound="gridlabMaster_DataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name">
                                                                        <HeaderStyle Font-Underline="False" />
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ReferenceId" HeaderText="Reference Name" SortExpression="ReferenceId">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="IsGroup" HeaderText="Group" SortExpression="Group">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" ReadOnly="True"
                                                                        SortExpression="DepartmentName">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ParameterCount" HeaderText="# Parameter" SortExpression="ParameterCount" />
                                                                    <asp:BoundField DataField="Active" HeaderText="Status" SortExpression="Active">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle CssClass="textstyle" />
                                                                        <ItemTemplate>
                                                                            <div style="white-space: nowrap">
                                                                                <span style='display: <%= svDelete %>; white-space: nowrap'>
                                                                                    <asp:Button ID="buttonEdit" CssClass="btn btn-default input-sm" runat="server" CausesValidation="false"
                                                                                        CommandName="SetInactive" Text="Make Inactive" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'></asp:Button></span>
                                                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeInactive" runat="server" DisplayModalPopupID="mpeInactive"
                                                                                    TargetControlID="buttonDelete">
                                                                                </ajaxToolkit:ConfirmButtonExtender>
                                                                                <ajaxToolkit:ModalPopupExtender ID="mpeInactive" runat="server" PopupControlID="pnlInactivePopup"
                                                                                    TargetControlID="buttonDelete" OkControlID="btnInactiveYes" CancelControlID="btnInactiveNo"
                                                                                    BackgroundCssClass="modalBackground">
                                                                                </ajaxToolkit:ModalPopupExtender>
                                                                                <asp:Panel ID="pnlInactivePopup" runat="server" Style="display: none; background-color: #FFFFFF; width: 300px; border: 3px solid #0DA9D0;">
                                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                        Confirmation
                                                                                    </div>
                                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                        <table border="0" cellpadding="15" cellspacing="0" style="width: 100%; height: 25px">
                                                                                            <tr>
                                                                                                <td style="width: 48px" valign="middle" align="center">
                                                                                                    <img src="../../Images/mb_question.gif" alt="" width="32" height="32" />
                                                                                                </td>
                                                                                                <td style="width: 100%; padding-left: 20px" valign="middle" align="left">
                                                                                                    <asp:Label runat="server" ID="labelText"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div style="padding: 3px;" align="center">
                                                                                        <asp:Button ID="btnInactiveYes" runat="server" Text=" Yes " ForeColor="DarkGreen" />
                                                                                        <asp:Button ID="btnInactiveNo" runat="server" Text=" No " ForeColor="DarkBlue" Style="margin-left: 10px" />
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <span style='display: <%# ShowDelete(Eval("DeleteFlag")) %>; white-space: nowrap'>
                                                                                    <asp:Button ID="buttonDelete" CssClass="btn btn-default input-sm" runat="server"
                                                                                        CausesValidation="false" CommandName="DeleteLab" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                                                </span><span style='display: <%# ShowParam(Eval("IsGroup"),Eval("DeleteFlag")) %>; white-space: nowrap'>
                                                                                    <asp:Button ID="buttonParam" CssClass="btn btn-default input-sm" runat="server" CausesValidation="false"
                                                                                        CommandName="ViewParam" Text="Parameters" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                                                </span><span style='display: <%# ShowGroup(Eval("IsGroup"),Eval("DeleteFlag")) %>; white-space: nowrap'>
                                                                                    <asp:Button ID="buttonGroup" CssClass="btn btn-default input-sm" runat="server" CausesValidation="false"
                                                                                        CommandName="ViewGroup" Text="Group" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                                                                        ForeColor="Blue" />
                                                                                </span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <RowStyle CssClass="gridrow" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="buttonSave" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnAdd" CssClass="btn btn-info" runat="server" Text="Add Lab Test" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Close" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:UpdatePanel ID="divLabComponent" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="divLabPopup" runat="server" Style="display: none; width: 680px;" Width="680px"
                        DefaultButton="buttonSave">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                ADD LAB TEST
                            </div>
                            <div class="panel-body">
                                <%--                              <asp:Panel ID="divTitle" runat="server" Style=" margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                
                                


                        
                        
      

                                       

                                  



                           
                            <table border="0" cellpadding="0" cellspacing="0"  height: 18px">
                                <tr>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                    <td style="width: 100%; height: 19px;">
                                        <h2 class="forms" align="left">Add Lab Test </h2>
                                    </td>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                                <div class="col-md-12 text-danger">
                                    <small class="pull-left">* All fields are required</small>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">
                                                    Lab Name</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox ID="textLabName" CssClass="form-control input-sm" runat="server" MaxLength="250"
                                                    Columns="80" ValidationGroup="savelab" onkeyup="javascript:InferReference();"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textLabName"
                                                    ValidChars="-/\* ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Lab Name"
                                                    ControlToValidate="textLabName" Display="Dynamic" ValidationGroup="savelab">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- .col-md-6-->
                                    <div class="col-md-6">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">
                                                Reference Name</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="textReference" CssClass="form-control input-sm" runat="server" MaxLength="36"
                                                    Columns="40" Style="text-transform: uppercase;"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteNameRef" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                                    TargetControlID="textReference" ValidChars="-_&/\*%() ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- .col-md-6-->
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <div class="col-md-4">
                                                <label class="control-label pull-left">
                                                    Is Group :</label>
                                            </div>
                                            <div class="pull-left">
                                                <label for="chkNo">
                                                    <input type="radio" id="chkGroupNo" name="chkGroup" runat="server" />
                                                    No
                                                </label>
                                                <label for="chkYes">
                                                    <input type="radio" id="chkGroupYes" name="chkGroup" runat="server" />
                                                    Yes
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6"></div>
                                    <!-- .col-md-6-->
                                    <%--                                        <div class="col-md-6" id="divNonGroup">
                                       <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">
                                                    Departments :</label></div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <!-- .col-md-6-->
                                </div>

                                <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">LOINC Code :</label></div>
                                            <div class="col-md-12">
                                                <asp:TextBox ID="txtLoinc" CssClass="form-control input-sm" runat="server" MaxLength="15"
                                                    Columns="80" ValidationGroup="savelab" onkeyup="javascript:InferReference();"></asp:TextBox>

                                            </div>
                                        </div>
                                        <!-- .fomr-group-->
                                    </div>
                                    <!-- .col-md-6-->

                                    <div class="col-md-6" id="divNonGroup">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="control-label pull-left">
                                                    Departments :</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <!-- .col-md-6-->

                                    </div>
                                    <!-- .row -->

                                    <div class="row">
                                        <div class="col-md-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div id="divAction" style="border: 0; text-align: center;">
                                            <span>
                                                <asp:Button ID="buttonSave" CssClass="btn btn-info" runat="server" Text="Save Lab Test"
                                                    Width="120px" OnClick="buttonSave_Click" CausesValidation="true" ValidationGroup="savelab" />
                                            </span>
                                            <asp:Button ID="buttonCancel" CssClass="btn btn-danger " runat="server" Text="Cancel"
                                                Width="80px" />
                                        </div>
                                    </div>
                                   <%-- <table cellpadding="1" cellspacing="2" border="0" width="680px" style="">
                                                                    <tr>
                                <td colspan="2" align="left">
                                    <i>All fields are required</i>
                                </td>
                            </tr>--%>
                                        <%--                            <tr>
                                <td colspan="2">
                                    <hr class="forms" />
                                </td>
                            </tr>--%>
                                        <%--                           <tr style="display: block">
                                <td style="height: 25px; text-align: left">
                                    Lab Name&nbsp;
                                </td>--%>
                                        <%--                                <td align="left" style="white-space: nowrap; vertical-align: middle">
                                        <asp:TextBox ID="textLabName" runat="server" MaxLength="250" Columns="80" ValidationGroup="savelab" onkeyup="javascript:InferReference();"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftLabname" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textLabName" ValidChars="-/\* ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Lab Name"
                                        ControlToValidate="textLabName" Display="Dynamic" ValidationGroup="savelab">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                                        <%--                             <tr style="display: block">
                              <td style="height: 25px; text-align: left">
                                    Reference Name&nbsp;
                                </td>--%>
                                        <%--                                <td align="left" style="white-space: nowrap; vertical-align: middle">
                                    <asp:TextBox ID="textReference" runat="server" MaxLength="36" Columns="40" Style="text-transform: uppercase;"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNameRef" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textReference" ValidChars="-_&/\*%() ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td style="height: 25px; text-align: left">
                                    Is Group :
                                </td>
                                <td style="white-space: nowrap; text-align: left">
                                    <label for="chkNo">
                                        <input type="radio" id="chkGroupNo" name="chkGroup" runat="server"  />
                                        No
                                    </label>
                                    &nbsp;
                                    <label for="chkYes">
                                        <input type="radio" id="chkGroupYes" name="chkGroup" runat="server" />
                                        Yes
                                    </label>
                                </td>
                            </tr>--%>
                                        <%-- <tr id="divNonGroup" style="display: block">
                                <td style="height: 25px; text-align: left">
                                    Department :
                                </td>--%>
                                        <%--<td style="white-space: nowrap; text-align: left">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                                        <%--                            <tr>
                                <td colspan="2">
                                    <hr class="forms" />
                                </td>
                            </tr>--%>
                                        <%--                            <tr>
                                <td class="form pad5 center" style="text-align: center; border: 0"
                                    colspan="2">
                                    <div id="divAction" style="border: 0; text-align: center;">
                                        <span><asp:Button ID="buttonSave" CssClass="btn btn-info" runat="server" Text="Save Lab Test" Width="120px"
                                            OnClick="buttonSave_Click" CausesValidation="true" ValidationGroup="savelab" />
                                           </span>
                                        <asp:Button ID="buttonCancel"  CssClass="btn btn-danger " runat="server" Text="Cancel" Width="80px" />
                                    </div>
                                </td>
                            </tr>
                                    </table>--%>
                                </div>
                            </div>
                    </asp:Panel>
                    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="LabTestDialog" runat="server" TargetControlID="buttonRaiseItemPopup"
                        BehaviorID="labtext_bhx" PopupControlID="divLabPopup" BackgroundCssClass="modalBackground"
                        CancelControlID="buttonCancel" DropShadow="false" PopupDragHandleControlID="divTitle"
                        Enabled="True" DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
