<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="LabResultControl.ascx.cs"
    Inherits="IQCare.Web.Laboratory.LabResultControl" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
<asp:HiddenField ID="HLabOrderId" runat="server" Value="-1" />
<asp:HiddenField ID="HLabTestId" runat="server" Value="-1" />
<asp:HiddenField ID="HLabTestOrderId" runat="server" Value="-1" />
<asp:HiddenField ID="hOpenMode" runat="server" />
<asp:HiddenField ID="HUserId" runat="server" />
<asp:HiddenField ID="hShowModal" runat="server" Value="FALSE" />
<asp:HiddenField ID="HIsGroup" runat="server" Value="FALSE" />
<asp:HiddenField ID="HResultDataType" runat="server" />
<div class="border center pad5 formbg" id="divResult">
    <asp:Panel ID="divResultPopup" runat="server" Style="display: block; width: 720px;
        border: solid 1px #808080;" Width="720px" DefaultButton="buttonRaiseItemPopup">
        <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
            cursor: move; height: 18px;background-color: #6699FF">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                <tr>
                    <td style="width: 5px; height: 19px;">
                    </td>
                    <td style="width: 100%; height: 19px;">
                        <h2 class="forms" align="left">
                            Lab Result
                        </h2>
                    </td>
                    <td style="width: 5px; height: 19px;">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Repeater ID="repeaterResult" runat="server" OnItemDataBound="repeaterResult_ItemDataBound">
            <HeaderTemplate>
                <table style="width: 100%;background-color: #CCFFFF">
                    <tr>
                        <td style="width: 30%">
                        </td>
                        <td style="width: 50%">
                        </td>
                        <td>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style='display: <%= svGroup %>;'>
                    <td colspan="2">
                        <asp:Label ID="labelTestName" runat="server"><%# DataBinder.Eval(Container.DataItem, "TestName")%></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="form bold" align="left">
                        <asp:Label ID="labelParameterName" runat="server"><%# DataBinder.Eval(Container.DataItem, "ParameterName")%></asp:Label>
                        <asp:HiddenField ID="hParameterId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ParameterId") %>' />
                        <asp:HiddenField ID="hResultTestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TestId") %>' />
                        <asp:HiddenField ID="HResultDataType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DataType") %>' />
                        <asp:HiddenField ID="HResultId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ResultId") %>' />
                    </td>
                    <td class="form bold" align="left">
                        <asp:Panel ID="divNumeric" Style='white-space: nowrap; display: <%# ShowNumDiv(Eval("DataType")) %>'
                            runat="server">
                            <asp:TextBox ID="textResultValue" runat="server" MaxLength="6" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "ResultValue")%>'></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteValue" runat="server" Enabled="false"
                                FilterType="Numbers,Custom" TargetControlID="textResultValue" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="checkUndetectable" runat="server" Text="Undetectable" TextAlign="Left"
                                Checked='<%# DataBinder.Eval(Container.DataItem, "Undetectable")%>' />
                            <asp:TextBox ID="textDetectionLimit" runat="server" MaxLength="6" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "DetectionLimit")%>'></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteLimit" runat="server" Enabled="false"
                                FilterType="Numbers,Custom" TargetControlID="textDetectionLimit" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlResultUnit" runat="Server" Width="180px">
                            </asp:DropDownList>
                            <asp:Label ID="labelUnit" runat="server" Visible="false"><%# DataBinder.Eval(Container.DataItem, "ResultUnit")%></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="divTextResult" runat="server" Style='white-space: nowrap; display: <%# ShowTextResult(Eval("DataType"),Eval("ResultText")) %>'>
                            <asp:Label ID="labelResultText" runat="server" Visible="false"><%# DataBinder.Eval(Container.DataItem, "ResultText")%></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="divText" Style='white-space: nowrap; display: <%# ShowTextDiv(Eval("DataType"),Eval("ResultText")) %>'
                            runat="server">
                            <asp:TextBox ID="textResultText" runat="server" Rows="3" MaxLength="255" Text='<%# DataBinder.Eval(Container.DataItem, "ResultText")%>'
                                Width="90%"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="divSelect" Style='white-space: nowrap; display: <%# ShowSelectDiv(Eval("DataType"),Eval("ResultText")) %>'
                            runat="server">
                            <asp:DropDownList ID="ddlResultList" runat="Server" Width="180px">
                            </asp:DropDownList>
                        </asp:Panel>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="2">
                        <div id="divAction" style="white-space: nowrap; border: 0; text-align: center;">
                            <span>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSaveResult" runat="server" Text="Save Result"
                                Width="120px" CausesValidation="true" ValidationGroup="df_x" OnClick="btnSaveResult_Click" />
                                &nbsp;&nbsp;&nbsp;</span>
                            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" Width="80px" />
                        </div>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <%-- <table style="width: 100%">
            
        </table>--%>
    </asp:Panel>
    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mpeResultPopup" runat="server" TargetControlID="buttonRaiseItemPopup"
        PopupControlID="divResultPopup" BackgroundCssClass="modalBackground" DropShadow="True"
        BehaviorID="labResultx572" PopupDragHandleControlID="divTitle" Enabled="True"
        CancelControlID="buttonCancel" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>
</div>
