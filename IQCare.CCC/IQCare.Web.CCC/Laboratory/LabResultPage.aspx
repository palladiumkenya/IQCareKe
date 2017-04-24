<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="LabResultPage.aspx.cs" Inherits="IQCare.Web.Laboratory.LabResultPage" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div style="padding: 8px;">
        <div class="border">
            <asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
            <asp:HiddenField ID="HLabOrderId" runat="server" Value="-1" />
            <asp:HiddenField ID="HUserId" runat="server" />
            <asp:HiddenField ID="hdCustID" runat="server" />
            <asp:HiddenField ID="hdControlExists" runat="server" />
            <asp:HiddenField ID="hdappcurrentdate" runat="server" />
            <asp:HiddenField ID="hLabTestId" runat="server" Value="-1" />
            <asp:HiddenField ID="HLabOrderTestId" runat="server" Value="-1" />
            <asp:HiddenField ID="hdTestName" runat="server" Value="" />
            <asp:HiddenField ID="hdDataType" runat="server" />
            <asp:HiddenField ID="hdReferenceId" runat="server" />
            <asp:HiddenField ID="hParamCount" runat="server" />
            <asp:HiddenField ID="hdDepartmentId" runat="server" />
            <asp:HiddenField ID="hdDepartmentname" runat="server" />
            <h3 id="H1" class="margin" style="padding-left: 10px;">
                <asp:Label runat="server" ID="labelOrderNumber"></asp:Label>
            </h3>
            <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0; border: solid 1px #C00000; margin-bottom: 10px;"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" class="form">
                <tr style="display: none">
                    <td class="pad18" style="width: 20%; white-space: nowrap">
                        <label class="right35 bold" for="labelOrderedbyname">
                            Ordered by:</label>
                        <asp:Label ID="labelOrderedbyname" runat="Server">
                        </asp:Label>
                    </td>
                    <td class="pad18 bold" style="width: 20%; white-space: nowrap">
                        <label class="right35" for="txtlaborderedbydate">
                            Order Date:</label>
                        <asp:Label ID="labellaborderedbydate" MaxLength="12" runat="server"></asp:Label>&nbsp;
                        <label class="right35" for="LabtobeDone" style="display: none">
                            Lab to be done on:</label>
                        <asp:Label ID="labelLabtobeDone" MaxLength="11" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="2">
                        <label class="right35 bold" for="labelClinicalNotes">
                            Clinical Notes:</label>
                        <asp:Label ID="labelClinicalNotes" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <div class="whitebg border pad5" id="pnllabtest" style="overflow: auto; min-height: 300px">
                        <asp:Repeater EnableTheming="True" EnableViewState="True" ID="repeaterLabTest" runat="server"
                            Visible="True" OnItemDataBound="repeaterLabTest_ItemDataBound" OnItemCommand="repeaterLabTest_ItemCommand">
                            <FooterTemplate>
                                <%--</table>--%>
                                </div>
                            </FooterTemplate>
                            <HeaderTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label class="pull-left control-label"><b>Lab Name</b></label>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="pull-left control-label"><b>Test notes</b></label>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="pull-left control-label"><b>Status</b></label>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="pull-left control-label"><b>Result By</b></label>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="pull-left control-label"><b>Result Date</b></label>
                                        </div>
                                    </div>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <div class="col-md-3" style='padding-bottom: 20px; text-align: left'>
                                    
                                    <asp:Label ID="labelTestName" runat="server" Font-Bold="false" CssClass="pull-left control-label"><%# DataBinder.Eval(Container.DataItem, "TestName")%></asp:Label>
                                    <asp:HiddenField ID="TestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TestId") %>' />
                                    <asp:HiddenField ID="hdTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                </div>

                                <div class="col-md-2" style='padding-bottom: 20px;'>
                                    <asp:Label ID="labelRequestNotes" runat="server" CssClass="pull-left control-label" Font-Bold="false" Visible="true"></asp:Label>
                                    <span style='display: <%# ShowInfoImage(Eval("TestNotes")) %>'>
                                        <asp:LinkButton ID="imgNotice" runat="server" Text="...." Height="32px" Width="32px"
                                            ForeColor="Black" /></span>
                                    <ajaxToolkit:ModalPopupExtender ID="mpeViewTestNotes" runat="server" PopupControlID="pnlPopupTestNotes"
                                        TargetControlID="imgNotice" CancelControlID="btnTestNoteClose" BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopupTestNotes" runat="server" Style="display: none; background-color: #FFFFFF; width: 300px; border: 3px solid #0DA9D0;">
                                        <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px; text-align: center; font-weight: bold;">
                                            <br />
                                        </div>
                                        <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                            <%# DataBinder.Eval(Container.DataItem, "TestNotes")%>
                                                &nbsp; &nbsp;
                                        </div>
                                        <div style="padding: 3px; text-align: right">
                                            <asp:Button ID="btnTestNoteClose" runat="server" Text="Close" Style="margin-left: 10px" CssClass="btn btn-link" />
                                        </div>
                                    </asp:Panel>

                                </div>

                                <div class="col-md-2" style='padding-bottom: 20px;'>
                                    <asp:Label ID="labelStatus" runat="server" Font-Bold="false" CssClass="pull-left control-label" Visible="true"><%# DataBinder.Eval(Container.DataItem, "TestOrderStatus")%></asp:Label>
                                </div>

                                <div class="col-md-2" style='padding-bottom: 20px; display: block'>                                    
                                    <asp:Button ID="buttonResult" runat="server" CausesValidation="false" Text="Enter Result" CssClass="btn btn-info pull-left"
                                        CommandName="EnterResult" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' role="button"></asp:Button>

                                </div>

                                <div class="col-md-3" runat="server" style='padding-bottom: 20px;'>
                                    <asp:Label ID="labReportedbyDate" runat="server" CssClass="pull-left control-label"></asp:Label>
                                </div>

                                <div class="col-md-12 whitebg border pad5">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:TextBox ID="textResultNotes" CssClass="form-control textarea" runat="server" Rows="3" MaxLength="255" Text='<%# DataBinder.Eval(Container.DataItem, "ResultNotes")%>'
                                                    Width="90%" Visible="false"></asp:TextBox>
                                                <asp:Repeater ID="repeaterResult" runat="server" OnItemDataBound="repeaterResult_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <div class="container-fluid">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <label class="pull-left control-label"><b>Parameter</b></label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label class="pull-left control-label"><b>Result</b></label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label class="pull-left control-label"><b>Reference Range</b></label>
                                                                </div>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-md-3" style='padding-bottom: 20px;'>
                                                                <asp:Label ID="labelParameterName" CssClass="pull-left control-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParameterName")%>'></asp:Label>
                                                                <asp:HiddenField ID="hParameterId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ParameterId") %>' />
                                                                <asp:HiddenField ID="hResultTestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabTestId") %>' />
                                                                <asp:HiddenField ID="hTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabOrderTestId") %>' />
                                                                <asp:HiddenField ID="HResultDataType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ResultDataType") %>' />
                                                                <asp:HiddenField ID="HResultId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />

                                                            </div>

                                                            <div class="col-md-9" style='padding-bottom: 20px;'>
                                                                <div id="divNumeric" style='white-space: nowrap; width: 100%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                                                    <div style="width: 50%;">
                                                                        <span style='white-space: nowrap; display: <%#ShowNumResult(Eval("HasResult"),Eval("Undetectable")) %>'>
                                                                            <asp:Label CssClass="pull-left control-label" ID="labelResultValue" runat="server" Visible="True"><%# DataBinder.Eval(Container.DataItem, "ResultValue")%></asp:Label>
                                                                        </span><span style='white-space: nowrap; display: <%#ShowUndetectable(Eval("HasResult"),Eval("Undetectable")) %>'>
                                                                            <asp:Label CssClass="pull-left control-label" ID="labelDetectionLimit" runat="server" Visible="True"></asp:Label>
                                                                        </span>
                                                                    </div>
                                                                    <div style="width: 50%;">
                                                                        <asp:Label CssClass="pull-left control-label" ID="labelRanges" runat="server" Visible="True"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div id="divText" style='white-space: nowrap; width: 100%; display: <%# ShowTextDiv(Eval("ResultDataType")) %>'>
                                                                    <asp:Label CssClass="pull-left control-label" ID="labelResultText" runat="server" Visible="True"><%# DataBinder.Eval(Container.DataItem, "ResultText")%></asp:Label>
                                                                </div>
                                                                <div id="divSelect" style='white-space: nowrap; width: 100%; display: <%# ShowSelectDiv(Eval("ResultDataType")) %>'>
                                                                    <asp:Label CssClass="pull-left control-label" ID="labelResultOption" runat="server" Visible="True"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                   
                     <div class="pad18 center  form" style="text-align: center; margin-top: 5px; margin: 5px;">
                     <%--   <asp:Button ID="buttonDelete" runat="server" Text="Delete Lab Order" ForeColor="Black" CssClass="btn btn-danger" data-toggle="modal" data-target="#myModal" UseSubmitBehavior="true"/>
                     --%>  
                         <button type="button" id="buttonDelete" class="btn btn-danger" data-toggle="modal" data-target="#myModal" style="<%= sDelete %>">Delete Lab Order</button>
                           <asp:Button ID="buttonPrint" runat="server" Text="Print Result" ForeColor="Black" CssClass="btn btn-info"
                            OnClientClick="javascript:window.location='Reports/PrintOut.aspx?mode=cr';return false;" />
                        <asp:Button ID="btnExitPage" runat="server" Width="75px" Text="Close"
                            ForeColor="Black" CssClass="btn btn-default" />
                    </div></ContentTemplate>
            </asp:UpdatePanel>
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title"><b>Delete Lab Order</b></h4>
                                </div>
                                <div class="modal-body">
                                    <label class="control-label" for="txtDeleteReason">You are about to delete a lab order. Provide a reason for this deletion.</label>
                                        <asp:TextBox ID="txtDeleteReason" runat="server" autocomplete="off" AutoCompleteType="None" CssClass="form-control textarea" MaxLength="250" placeholder="Why do you want to delete this" ValidationGroup="del"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvReason" ControlToValidate="txtDeleteReason" Display="Dynamic" ErrorMessage="*" ValidationGroup="del"></asp:RequiredFieldValidator>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="deleteConfirmed"  runat="server" type="button" CssClass="btn btn-danger"  Text="I'm sure. Delete" OnClick="deleteConfirmed_Click" ValidationGroup="del" CausesValidation="true" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                
        </div>
    </div>
    <%--  <script type="text/javascript">
      // Load lab results        
          $("#btnExitPage").click(function (e) {
                window.location.href = '<%=ResolveClientUrl("../CCC/Home.aspx")%>'; 
              
            });           
      </script>      --%>
</asp:Content>
