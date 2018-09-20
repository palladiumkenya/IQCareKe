<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="LabRecordEntry.aspx.cs" Inherits="IQCare.Web.Laboratory.LabRecordEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
    <asp:HiddenField ID="HLabOrderId" runat="server" Value="-1" />
    <asp:HiddenField ID="HUserId" runat="server" />
    <script language="javascript" type="text/javascript">
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
            $find("laborder_bhx").show();
            return false;
        }

        function HideModalPopup() {
            $find("laborder_bhx").hide();
            return false;
        }
        function unitSelectedChanged(combo, eventArgs) {
            var item = eventArgs.get_item();
            var detection_limit = (item.get_attributes().getAttribute("detection_limit"));
            var min_value = (item.get_attributes().getAttribute("min"));
            var max_value = (item.get_attributes().getAttribute("max"));
            var min_normal = (item.get_attributes().getAttribute("min_normal"));
            var max_normal = (item.get_attributes().getAttribute("max_normal"));
        }

    </script>
    <style type="text/css">
        .autoextender
        {
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
        .autoextenderlist
        {
            cursor: pointer;
            color: black;
        }
        .autoextenderhighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 400px !important;
        }
        #divwidth div
        {
            width: 400px !important;
        }
    </style>
    <div class="row">

        <div class="panel panel-default">

             <div class="panel-heading">Lab record Entry </div>

             <div class="panel-body">
                  <div class="border">
                       <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                                    border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </asp:Panel>
                             </ContentTemplate>
                       </asp:UpdatePanel>
                    
                       <asp:UpdatePanel ID="divTestComponent" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                             <ContentTemplate>
                                <div class="border pad5" id="divSelectLab">
                                    <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0">
                                        <tbody>
                                <tr style="display: none; height: 18px">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <div class="pad5">
                                           <div class="col-md-12"> <label class=" required control-label pull-left" for="textSelectLab"> Select Lab Test:</label></div>
                                           <div class="col-md-12"><asp:TextBox ID="textSelectLab" CssClass="form-control input-sm"  runat="server" AutoPostBack="true" AutoComplete="off"
                                                  OnTextChanged="LabNameChanged" ></asp:TextBox>
                                            <div id="divwidth" style="z-index:5000"></div>
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
                                            <asp:HiddenField ID="hdDepartmentname" runat="server" />
                                            <asp:HiddenField ID="hdappcurrentdate" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">
                                       <div  class="col-md-12"> <label class="control-label pull-left" for="txtTestNotes"> Description:</label></div>
                                       <div class="col-md-12"> <asp:TextBox ID="txtTestNotes" CssClass="form-control" runat="server" MaxLength="255"></asp:TextBox></div>
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">
                                        <hr class="forms" />
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">
                                        <asp:Repeater ID="repeaterResult" runat="server" OnItemDataBound="repeaterResult_ItemDataBound">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 30%">
                                                        </td>
                                                        <td style="width: 70%">
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="pad18 bold" align="left" style="border-bottom: 1px solid #ddd; vertical-align: middle">
                                                        <asp:Label ID="labelParameterName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParameterName")%>'></asp:Label>
                                                        <asp:HiddenField ID="hParameterId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ParameterId") %>' />
                                                        <asp:HiddenField ID="hResultTestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TestId") %>' />
                                                        <asp:HiddenField ID="hTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TestOrderId") %>' />
                                                        <asp:HiddenField ID="HResultDataType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DataType") %>' />
                                                        <asp:HiddenField ID="HResultId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ResultId") %>' />
                                                    </td>
                                                    <td class="pad18 bold" align="left" style="border-bottom: 1px solid #ddd; width: 70%;
                                                        vertical-align: middle">
                                                        <div id="divNumeric" style='white-space: nowrap; display: <%#ShowNumDiv(Eval("DataType"))%>'>
                                                            <asp:TextBox ID="textResultValue" runat="server" MaxLength="6" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "ResultValue")%>'></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteValue" runat="server" Enabled="true"
                                                                FilterType="Numbers,Custom" TargetControlID="textResultValue" ValidChars=".">
                                                            </ajaxToolkit:FilteredTextBoxExtender>                                                           
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox ID="checkUndetectable" runat="server" Text="Undetectable" TextAlign="Left"
                                                                Checked='<%# DataBinder.Eval(Container.DataItem, "Undetectable")%>' />
                                                            Limit:
                                                            <asp:TextBox ID="textDetectionLimit" runat="server" MaxLength="6" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "DetectionLimit")%>'></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteLimit" runat="server" Enabled="true"
                                                                FilterType="Numbers,Custom" TargetControlID="textDetectionLimit" ValidChars=".">
                                                            </ajaxToolkit:FilteredTextBoxExtender>                                                            
                                                            &nbsp;&nbsp;&nbsp;
                                                            <telerik:RadComboBox ID="ddlResultUnit" runat="server" Width="180px" AutoPostBack="false"
                                                                MarkFirstMatch="false" HighlightTemplatedItems="true" AppendDataBoundItems="true"
                                                                OnClientSelectedIndexChanged="unitSelectedChanged">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                            </telerik:RadComboBox>
                                                        </div>
                                                        <div id="divText" style='white-space: nowrap; display: <%# ShowTextDiv(Eval("DataType")) %>'>
                                                            <asp:TextBox ID="textResultText" runat="server" Rows="3" MaxLength="255" Text='<%# DataBinder.Eval(Container.DataItem, "ResultText")%>'
                                                                Width="90%"></asp:TextBox>
                                                        </div>
                                                        <div id="divSelect" style='text-align: left; white-space: nowrap; display: <%# ShowSelectDiv(Eval("DataType")) %>'>
                                                            <asp:DropDownList ID="ddlResultList" CssClass="form-control" runat="Server" Width="280px" Height="20px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">
                                        <div id="divAction" style="white-space: nowrap; border-top: 0px solid #ddd; text-align: center;
                                            margin-top: 10px">
                                            <span>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddLabRecord" CssClass="btn btn-info" runat="server" Text="Add Lab Record"
                                                 CausesValidation="true" ValidationGroup="df_x" OnClick="AddLabRecord"
                                                />
                                                &nbsp;&nbsp;&nbsp;</span>
                                            <asp:Button ID="buttonCancel" CssClass="btn btn-danger" runat="server" Text="Cancel"  OnClick="CancelLabEntry"
                                                 />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                                    </table>
                                </div>
                           </ContentTemplate>
                       </asp:UpdatePanel>
                     
                       <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                    DataKeyNames="TestId" OnRowCommand="gridTestRequested_RowCommand" OnRowDataBound="gridTestRequested_RowDataBound">
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
                                                                            ForeColor="Blue"></asp:Button></span></div>
                                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeLabRemove" runat="server" DisplayModalPopupID="mpeLabRemove"
                                                                    TargetControlID="buttonRemove">
                                                                </ajaxToolkit:ConfirmButtonExtender>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeLabRemove" runat="server" PopupControlID="pnlPopup"
                                                                    TargetControlID="buttonRemove" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #0DA9D0;">
                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        Confirmation
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        Are you sure you want to remove this lab test?
                                                                    </div>
                                                                    <div style="padding: 3px;" align="right">
                                                                        <asp:Button ID="btnYes" runat="server" Text="Yes" ForeColor="DarkGreen" /><asp:Button
                                                                            ID="btnNo" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
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
                    <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0"
                        class="form">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td class="pad18" align="left" colspan="2">
                                   <div class="col-md-12"> <label class="control-label pull-left" for="txtClinicalNotes">Clinical Notes:</label></div>
                                  
                                  <div class="col-md-12"> <asp:TextBox ID="txtClinicalNotes" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255"
                                         Text=''></asp:TextBox></div> 
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="pad18" valign="middle" width="50%">
                                </td>
                                <td class="pad18" valign="middle">
                                    <div class="col-md-12"><label class="required control-label pull-left" for="LabtobeDone">Lab to be done on:</label></div>
                                    <div class="col-md-6" style="padding-right:0%><asp:TextBox ID="txtLabtobeDone" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox></div>
                                    <div class="col-md-2" style=" padding-left:0%"><asp:ImageButton runat="Server" ID="ImageButton2" Height="22" Style="hspace: 3; width: 22;
                                        height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                    <ajaxToolkit:CalendarExtender ID="cbeLabToBeDone" runat="server" TargetControlID="txtLabtobeDone"
                                        PopupButtonID="ImageButton2" Format="dd-MMM-yyyy" />
                                    <span class="smallerlabel" id="SPAN3">(DD-MMM-YYYY)</span>
                                    </div>
                                    <div class="col-md-4"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad18" style="width: 50%">
                                    <div class="col-md-3"><label class="required control-label pull-left ">*Ordered by:</label></div>
                                    <div class="col-md-6"><asp:DropDownList CssClass="form-control" ID="ddlaborderedbyname" runat="Server" OnDataBound="ddlaborderedbyname_DataBound">
                                    </asp:DropDownList></div>
                                    <div class="col-md-2"></div>
                                </td>
                                <td class="pad18" style="width: 50%">
                                   <div class="col-md-3"><label class="required control-label pull-right" for="LabOrderedbyDate"> *Ordered By Date:</label></div> 
                                   <div class="col-md-2 pull-right" style=" padding-left:0%">
                                       <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="hspace: 3; width: 22;
                                            height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                        <ajaxToolkit:CalendarExtender ID="cbeOrderDate" runat="server" TargetControlID="txtlaborderedbydate"
                                            PopupButtonID="Image1" Format="dd-MMM-yyyy" />
                                        <span class="smallerlabel" id="SPAN2">(DD-MMM-YYYY)</span></div>
                                   <div class="col-md-6 pull-right" style=" padding-right:0%"> <asp:TextBox CssClass="form-control" ID="txtlaborderedbydate" MaxLength="12" runat="server"></asp:TextBox></div>
                                    <div class="col-md-1"> </div>
                                </td>
                            </tr>
                            <tr style="">
                                <td class="pad18" style="width: 50%">
                                    <div class="col-md-3"><label id="lblreportedby" runat="server" class="required control-label pull-left" for="ddlLabReportedbyName"> *Reported by:</label></div>
                                    <div class="col-md-6"><asp:DropDownList CssClass="form-control" ID="ddlLabReportedbyName" runat="Server">
                                    </asp:DropDownList></div>
                                    <div class="col-md-2"></div>
                                </td>
                                <td class="pad18" style="width: 50%">
                                   <div class="col-md-3" style="padding-right:0%"> <label id="lblreportedbydate" runat="server" for="labReportedbyDate" class="required control-panel pull-right"> *Reported By Date:</label></div>
                                   <div class="col-md-2 pull-right" style=" padding-left:0%">
                                       <asp:ImageButton runat="Server" ID="ImageButton1" Height="22" Style="hspace: 3; width: 22;height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                          <ajaxToolkit:CalendarExtender ID="cbeReportDate" runat="server" TargetControlID="txtlabReportedbyDate"
                                        PopupButtonID="ImageButton1" Format="dd-MMM-yyyy" />
                                        <span class="smallerlabel" id="SPAN1">(DD-MMM-YYYY)</span>
                                   </div>
                                   <div class="col-md-6 pull-right" style=" padding-right:0%"> <asp:TextBox CssClass="form-control" ID="txtlabReportedbyDate" MaxLength="12" runat="server"></asp:TextBox></div>

                                   <div class="col-md-1"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad18 center form" colspan="2">
                                    <asp:Button ID="buttonSave" runat="server" CssClass="btn btn-info" Text="Save Lab Entry" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-danger" Font-Size="12px"  Text="Close Entry "
                                        OnClick="ExitPage" />
                                </td>
                            </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="textSelectLab" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
         
        </div>
             </div><!-- .panel-body -->
        </div><!-- .panel-->
    </div><!-- .row -->

</asp:Content>
