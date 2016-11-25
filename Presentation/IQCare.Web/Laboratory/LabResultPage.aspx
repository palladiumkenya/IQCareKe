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
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                        border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
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
            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="whitebg border pad5" id="pnllabtest" style="overflow: auto; min-height: 300px">
                        <asp:Repeater EnableTheming="True" EnableViewState="True" ID="repeaterLabTest" runat="server"
                            Visible="True" OnItemDataBound="repeaterLabTest_ItemDataBound" OnItemCommand="repeaterLabTest_ItemCommand">
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                            <HeaderTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <b>Lab Name</b>
                                        </td>
                                        <td style="width: 15%">
                                            <b>Test notes</b>
                                        </td>
                                        <td style="width: 15%">
                                            <b>Status</b>
                                        </td>
                                        <td style="width: 15%">
                                            <b>Result By</b>
                                        </td>
                                        <td style="width: 20%">
                                            <b>Result Date</b>
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 30%; text-align: left">
                                        <asp:Label ID="labelTestName" runat="server" Font-Bold="false"><%# DataBinder.Eval(Container.DataItem, "TestName")%></asp:Label>
                                        <asp:HiddenField ID="TestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TestId") %>' />
                                        <asp:HiddenField ID="hdTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <asp:Label ID="labelRequestNotes" runat="server" Font-Bold="false" Visible="true"></asp:Label>
                                        <span style='display: <%# ShowInfoImage(Eval("TestNotes")) %>'>
                                            <asp:LinkButton ID="imgNotice" runat="server" Text="...." Height="32px" Width="32px"
                                                ForeColor="Black" /></span>
                                        <ajaxToolkit:ModalPopupExtender ID="mpeViewTestNotes" runat="server" PopupControlID="pnlPopupTestNotes"
                                            TargetControlID="imgNotice" CancelControlID="btnTestNoteClose" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopupTestNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                            width: 300px; border: 3px solid #0DA9D0;">
                                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                text-align: center; font-weight: bold;">
                                                <br />
                                            </div>
                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                <%# DataBinder.Eval(Container.DataItem, "TestNotes")%>
                                                &nbsp; &nbsp;
                                            </div>
                                            <div style="padding: 3px;" align="right">
                                                <asp:Button ID="btnTestNoteClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelStatus" runat="server" Font-Bold="false" Visible="true"><%# DataBinder.Eval(Container.DataItem, "TestOrderStatus")%></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="labelReportedbyName" runat="Server">
                                        </asp:Label>
                                        <asp:Button ID="buttonResult" runat="server" CausesValidation="false" Text="Enter Result"
                                            CommandName="EnterResult" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                            ForeColor="Blue"></asp:Button>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="labReportedbyDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <div class="whitebg border pad5">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="textResultNotes" runat="server" Rows="3" MaxLength="255" Text='<%# DataBinder.Eval(Container.DataItem, "ResultNotes")%>'
                                                            Width="90%" Visible="false"></asp:TextBox>
                                                        <asp:Repeater ID="repeaterResult" runat="server" OnItemDataBound="repeaterResult_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <b>Parameter</b>
                                                                        </td>
                                                                        <td style="width: 35%">
                                                                            <b>Result</b>
                                                                        </td>
                                                                        <td style="width: 35%">
                                                                            <b>Reference Range</b>
                                                                        </td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td align="left" style="vertical-align: middle; width: 30%">
                                                                        <asp:Label ID="labelParameterName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParameterName")%>'></asp:Label>
                                                                        <asp:HiddenField ID="hParameterId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ParameterId") %>' />
                                                                        <asp:HiddenField ID="hResultTestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabTestId") %>' />
                                                                        <asp:HiddenField ID="hTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabOrderTestId") %>' />
                                                                        <asp:HiddenField ID="HResultDataType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ResultDataType") %>' />
                                                                        <asp:HiddenField ID="HResultId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                                    </td>
                                                                    <td align="left" style="width: 70%; vertical-align: middle" colspan="2">
                                                                        <div id="divNumeric" style='white-space: nowrap; width: 100%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                                                            <div style="width: 50%;">
                                                                                <span style='white-space: nowrap; display: <%#ShowNumResult(Eval("HasResult"),Eval("Undetectable")) %>'>
                                                                                    <asp:Label ID="labelResultValue" runat="server" Visible="True"><%# DataBinder.Eval(Container.DataItem, "ResultValue")%></asp:Label>
                                                                                </span><span style='white-space: nowrap; display: <%#ShowUndetectable(Eval("HasResult"),Eval("Undetectable")) %>'>
                                                                                    <asp:Label ID="labelDetectionLimit" runat="server" Visible="True"></asp:Label>
                                                                                </span>
                                                                            </div>
                                                                            <div style="width: 50%;">
                                                                                <asp:Label ID="labelRanges" runat="server" Visible="True"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div id="divText" style='white-space: nowrap; width: 100%; display: <%# ShowTextDiv(Eval("ResultDataType")) %>'>
                                                                            <asp:Label ID="labelResultText" runat="server" Visible="True"><%# DataBinder.Eval(Container.DataItem, "ResultText")%></asp:Label>
                                                                        </div>
                                                                        <div id="divSelect" style='white-space: nowrap; width: 100%; display: <%# ShowSelectDiv(Eval("ResultDataType")) %>'>
                                                                            <asp:Label ID="labelResultOption" runat="server" Visible="True"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="pad18 center  form" style="text-align: center; margin-top: 5px; margin: 5px;">
                        <asp:Button ID="buttonPrint" runat="server" Text="Print Result" ForeColor="Black"
                            OnClientClick="javascript:window.location='Reports/PrintOut.aspx?mode=cr';return false;" Style="" />
                        <asp:Button ID="btnExitPage" runat="server" Width="75px" Text="Close"
                            ForeColor="Black" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
           <%-- <uc1:progressControl ID="progressControl1" runat="server" />--%>
        </div>
    </div>
</asp:Content>
