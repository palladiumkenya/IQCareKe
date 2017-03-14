<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="LabRequestForm.aspx.cs" Inherits="IQCare.Web.Laboratory.LabRequestForm" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
    <asp:HiddenField ID="HLabOrderId" runat="server" Value="-1" />
    <asp:HiddenField ID="HUserId" runat="server" />
    <asp:HiddenField ID="HSysMode" runat="server" />
    <script type="text/javascript">
        function WindowPrint() {
            window.print();
        }
        function ace1_itemSelected(source, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();

        }
        function onClientPopulated(sender, e) {

        }

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


        function ShowModalPopup() {
            $find("request_syspaperles").show();
            return false;
        }

        function HideModalPopup() {
            $find("request_syspaperles").hide();
            return false;
        }
    </script>
    <style type="text/css">
        .autoextender {
            font-family: Courier New, Arial, sans-serif;
            font-size: 15px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 400px;
        }

        .autoextenderlist {
            cursor: pointer;
            color: black;
        }

        .autoextenderhighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        #divwidth {
            width: 400px !important;
        }

            #divwidth div {
                width: 400px !important;
            }
    </style>
    <div class="row" style="padding: 8px;">

        <div class="panel panel-default">
            <div class="panel-heading"><span class="fa fa-barcode fa-1x text-info pull-left"><strong>Lab Request Form </strong></span>
                <br />
            </div>
            <div class="panel-body">


                <div class="border">
                    <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0; border: solid 1px #C00000; margin-bottom: 10px;"
                                HorizontalAlign="Left" Visible="false">
                                <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                    Text=""></asp:Label>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="divTestComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="border pad5" id="divSelectLab" style="display: <%= sEdit %>">
                                <%-- <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="2" style=" padding-bottom:20px" align="left">--%>
                                <div class="col-md-12">
                                    <label class="reuired control-label pull-left" style="margin-left: 10px">Select Lab:</label>
                                    <asp:TextBox ID="textSelectLab" CssClass="form-control" runat="server" AutoPostBack="true" Width="50%"
                                        AutoComplete="off" Font-Names="Courier New" OnTextChanged="LabNameChanged" Font-Size="Medium"></asp:TextBox>
                                    <div id="divwidth" style="width: 63%">
                                    </div>
                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="Searchlab" MinimumPrefixLength="2"
                                        CompletionInterval="30" EnableCaching="false" CompletionSetCount="10" TargetControlID="textSelectLab"
                                        BehaviorID="AutoCompleteEx" OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1"
                                        runat="server" FirstRowSelected="false" CompletionListCssClass="autoextender"
                                        CompletionListItemCssClass="autoextenderlist" CompletionListHighlightedItemCssClass="autoextenderhighlight"
                                        CompletionListElementID="divwidth">
                                        <Animations>
								    <OnShow>
								        <Sequence>
								            <OpacityAction Opacity="0" />
								            <HideAction Visible="true" />
								            <ScriptAction Script="var behavior = $find('AutoCompleteEx');if (!behavior._height) {var target = behavior.get_completionList();behavior._height = target.offsetHeight - 2;target.style.height = '0px';}" />
								            <Parallel Duration=".4">
								                <FadeIn />
								                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
								            </Parallel>
								        </Sequence>
								    </OnShow>
								    <OnHide>
								        <Parallel Duration=".4">
								            <FadeOut />
								            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
								        </Parallel>
								    </OnHide>
                                        </Animations>
                                    </ajaxToolkit:AutoCompleteExtender>
                                    <asp:HiddenField ID="hdCustID" runat="server" />
                                    <asp:HiddenField ID="hLabTestId" runat="server" Value="-1" />
                                    <asp:HiddenField ID="HLabOrderTestId" runat="server" Value="-1" />
                                    <asp:HiddenField ID="hdTestName" runat="server" Value="" />
                                    <asp:HiddenField ID="hdDataType" runat="server" />
                                    <asp:HiddenField ID="hdReferenceId" runat="server" />
                                    <asp:HiddenField ID="hParamCount" runat="server" />
                                    <asp:HiddenField ID="hdDepartmentId" runat="server" />
                                    <asp:HiddenField ID="hdIsGroup" runat="server" Value="false" />
                                    <asp:HiddenField ID="hdDepartmentname" runat="server" />
                                    <asp:HiddenField ID="hdappcurrentdate" runat="server" />
                                </div>
                                <%--  </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">--%>
                                <div class="col-md-12" style="display: <%= sDataEntry %>">
                                    <label class="required pull-left control-label" style="margin-left: 10px" for="txtTestNotes">Description:</label>
                                    <asp:TextBox ID="txtTestNotes" runat="server" CssClass="form-control" MaxLength="255" Width="50%"></asp:TextBox>
                                </div>
                                <%-- </td>
                                </tr>--%>
                                <%--   <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">--%>
                                <div class="col-md-12" style="display: <%= sDataEntry %>">
                                    <div id="divAction" style="white-space: nowrap; border-top: 0px solid #ddd; text-align: center; margin-top: 10px">
                                        <span>&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-info" ID="btnAddLabRecord" runat="server" Text="Add Request"
                                            Width="120px" CausesValidation="true" ValidationGroup="df_x" OnClick="AddLabRecord" />
                                            &nbsp;&nbsp;&nbsp;</span>
                                        <asp:Button ID="buttonCancel" runat="server" CssClass="btn btn-danger" Text="Cancel Request" Width="120px" OnClick="CancelLabEntry" />
                                    </div>
                                </div>
                                <%--</td>
                                </tr>
                            </tbody>
                        </table>--%>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="textSelectLab" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="whitebg border pad5" id="pnllabtest" style="overflow: auto; min-height: 200px">
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 200px; overflow: auto">
                                                    <div id="grd_custom" class="GridView whitebg">
                                                        <asp:GridView ID="gridTestRequested" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            AllowSorting="False" BorderWidth="0px" GridLines="None" CssClass="datatable"
                                                            DataKeyNames="TestId" OnRowCommand="gridTestRequested_RowCommand"
                                                            OnRowDataBound="gridTestRequested_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="TestName" HeaderText="Test Name" ReadOnly="True" SortExpression="TestName">
                                                                    <HeaderStyle Font-Underline="False" />
                                                                    <ItemStyle CssClass="textstyle" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TestNotes" HeaderText="Description" SortExpression="TestNotes">
                                                                    <ItemStyle CssClass="textstyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle CssClass="textstyle" />
                                                                    <ItemTemplate>
                                                                        <div style="white-space: nowrap; margin-left: 10px">
                                                                            <span style='display: <%= sEdit %>; white-space: nowrap'>
                                                                                <asp:Button ID="buttonRemove" runat="server" CausesValidation="false" CommandName="Remove"
                                                                                    Text="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TestId") %>'
                                                                                    CssClass="btn btn-danger pull-right"></asp:Button></span>
                                                                        </div>
                                                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeLabRemove" runat="server" DisplayModalPopupID="mpeLabRemove"
                                                                            TargetControlID="buttonRemove">
                                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                                        <ajaxToolkit:ModalPopupExtender ID="mpeLabRemove" runat="server" PopupControlID="pnlPopup"
                                                                            TargetControlID="buttonRemove" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                                                        </ajaxToolkit:ModalPopupExtender>
                                                                        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF; width: 300px; border: 3px solid #0DA9D0;">
                                                                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                Confirmation
                                                                            </div>
                                                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                Are you sure you want to remove this lab test?
                                                                            </div>
                                                                            <div style="padding: 3px;" align="right">
                                                                                <asp:Button ID="btnYes" runat="server" Text="Yes" ForeColor="DarkGreen" /><asp:Button
                                                                                    ID="btnNo" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" />
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <RowStyle CssClass="gridrow" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddLabRecord" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="buttonCancel" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="divRequestComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="border pad5">
                                <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0"
                                    class="form">
                                    <tbody>
                                        <%-- <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>--%>
                                        <tr>
                                            <td class="pad18 well well-sm" align="left" colspan="2">
                                                <label class="required pull-left control-label" for="txtClinicalNotes">
                                                    Clinical Notes:</label>
                                                <br />
                                                <span style='display: <%= sEdit %>'>
                                                    <asp:TextBox ID="txtClinicalNotes" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255"
                                                        Text=''></asp:TextBox></span> <span style='display: <%= sView %>'>
                                                            <asp:Label ID="labelClinicalNotes" runat="server"></asp:Label></span>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td class="pad18" valign="middle" class="">
                                                <label class="required control-label" for="LabtobeDone">Lab to be done on:</label>
                                                <span style='display: <%= sEdit %>'>
                                                    <asp:TextBox ID="txtLabtobeDone" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox>
                                                    <asp:ImageButton runat="Server" ID="ImageButton2" Height="22" Style="hspace: 3; width: 22; height: 22"
                                                        ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                                    <ajaxToolkit:CalendarExtender ID="cbeLabToBeDone" runat="server" TargetControlID="txtLabtobeDone"
                                                        PopupButtonID="ImageButton2" Format="dd-MMM-yyyy" />
                                                    <span class="smallerlabel" id="SPAN3">(DD-MMM-YYYY)</span></span> <span style='display: <%= sView %>'>
                                                        <asp:Label ID="labelLabtobeDone" MaxLength="11" runat="server"></asp:Label></span>
                                            </td>
                                            <td class="pad18" style="white-space: nowrap; text-align: left">
                                                <label class="required control-label bold" for="labelOrderStatus">Order Status:</label>
                                                <asp:Label ID="labelOrderStatus" runat="Server" Text="New Order">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr class=" well well-sm">
                                            <td class="pad18" style="width: 30%; white-space: nowrap">
                                                <label class="required control-label pull-left">*Ordered by:</label>
                                                <span style='display: <%= sEdit %>'>
                                                    <asp:DropDownList ID="ddlaborderedbyname" CssClass="form-control" runat="Server" Width="180px">
                                                    </asp:DropDownList>
                                                </span><span style='display: <%= sView %>'>
                                                    <asp:Label ID="labelOrderedBy" runat="Server">
                                                    </asp:Label></span>
                                            </td>
                                            <td class="pad18 pull-right" style="width: 50%; white-space: nowrap">
                                                <div class="pull-left">
                                                    <label class="required" class="control-label pull-left" for="LabOrderedbyDate">*Order Date:</label></div>
                                                <span style='display: <%= sEdit %>'>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtlaborderedbydate" CssClass="form-control" MaxLength="12" runat="server"></asp:TextBox></div>
                                                    <div class="pull-left">
                                                        <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="hspace: 3; width: 22; height: 22"
                                                            ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                                        <ajaxToolkit:CalendarExtender ID="cbeOrderDate" runat="server" TargetControlID="txtlaborderedbydate"
                                                            PopupButtonID="Image1" Format="dd-MMM-yyyy" />
                                                        <span class="smallerlabel" id="SPAN2">(DD-MMM-YYYY)</span>
                                                </span>
                            </div>
                            <asp:Label ID="labellaborderedbydate" MaxLength="12" runat="server"></asp:Label>
                            <span style='display: <%= sView %>'>&nbsp;&nbsp;
                                            <label class="right35 bold" for="labelOrderNumber">Order Number:</label>
                                <asp:Label ID="labelOrderNumber" runat="Server" Text="_______________________">
                                </asp:Label></span>
                            </td>
                                </tr>
                                <tr>
                                    <td class="pad18 center form" colspan="2">
                                        <span style='display: <%= sHasData %>'>
                                            <asp:Button ID="buttonSave" runat="server" CssClass="btn btn-info" Text="Save Request" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;</span>
                                        <asp:Button ID="btnclose" runat="server" CssClass="btn btn-danger" Text="Exit Request"
                                            OnClick="ExitPage" />
                                    </td>
                                </tr>
                            </table>
                    </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="textSelectLab" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnAddLabRecord" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="buttonCancel" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click" />

                </div>

            </div>
            <!-- .panel-body-->
        </div>
        <!-- .panel-->
    </div>
    <!-- .row -->
</asp:Content>
