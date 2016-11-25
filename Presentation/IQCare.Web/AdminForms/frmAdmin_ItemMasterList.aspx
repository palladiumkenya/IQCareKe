<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" CodeBehind="frmAdmin_ItemMasterList.aspx.cs" Inherits="IQCare.Web.Admin.ItemMasterList" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>

<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">
        function ace1_itemSelected(sender, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();
            //alert(hdCustID.value);
        }
    </script>
    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement {
            margin: 0px !important;
            background-color: inherit;
            color: windowtext;
            border: solid 1px #006699;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            height: 200px;
            width: 580px;
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            text-align: left;
            line-height: 15px;
            list-style-type: none;
        }
        /* AutoComplete highlighted item */
        .autocomplete_highlightedListItem {
            background-color: #ffff99;
            color: black;
            padding: 1px;
        }

        /* AutoComplete item */
        .autocomplete_listItem {
            background-color: window;
            color: windowtext;
            padding: 1px;
        }

        #divwidthfooter {
            width: 800px !important;
        }

            #divwidthfooter div {
                width: 800px !important;
            }
    </style>
    <h2 class="forms" align="left">Items Master List</h2>
    <div class="rounded">
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="left" style="padding-left: 10px; padding-right: 15px">
                        <asp:HiddenField ID="HActiveTabIndex" runat="server" Value="" />

                        <asp:UpdatePanel runat="server" ID="updatePanelMasterItem">
                            <ContentTemplate>
                                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>

                                </asp:Panel>
                                <table border="0" cellpadding="6" cellspacing="0" class="mid" style="cursor: pointer; display: block; height: 30px; overflow: auto; border: 1px solid #666699;">
                                    <tr>
                                        <td style="width: 10%; white-space: nowrap;" align="right">
                                            <b>Main Item Type:</b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMainItem" runat="server" Width="180px" AutoPostBack="true" />
                                        </td>
                                        <td style="width: 10%; white-space: nowrap; display: none" align="right">Item Subtype:
                                        </td>
                                        <td style="display: none;">
                                            <asp:DropDownList ID="ddlSubType" runat="server" Width="180px" />
                                        </td>
                                        <td rowspan="2" valign="middle" style="display: none;">
                                            <asp:Button ID="btnGenerate" runat="server" Text="Filter Items" />
                                            <asp:Button ID="ChangeTabs" runat="server" Style="display: none" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlMainItem" EventName="SelectedIndexChanged" />

                            </Triggers>
                        </asp:UpdatePanel>
                        <ajaxToolkit:TabContainer ID="itemMasterTab" runat="server" Width="100%" ActiveTabIndex="0"
                            AutoPostBack="false" OnClientActiveTabChanged="ActiveTabChanged">
                            <ajaxToolkit:TabPanel ID="tabItemList" runat="server">
                                <HeaderTemplate>
                                    <b>Items</b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel runat="server" ID="divComponent" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="pad5 formbg border">
                                                            <div id="divConsumables" class="grid">
                                                                <div class="mid-outer">
                                                                    <div class="mid-inner">
                                                                        <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto; border: 1px solid #666699;">
                                                                            <div id="div-gridview" class="whitebg">
                                                                                <asp:GridView ID="gridItemList" CssClass="datatable table-striped table-responsive" CellPadding="0" runat="server"
                                                                                    AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="item_pk,itemtypeid,itemname"
                                                                                    EmptyDataText="No Data to display" Width="100%" ShowHeaderWhenEmpty="True">
                                                                                    <Columns>
                                                                                        <asp:CommandField ShowSelectButton="true" Visible="false" />
                                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="itemtypename" HeaderText="Item Type" SortExpression="itemtypename">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Modify" ShowHeader="False" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <div style='white-space: nowrap; text-align: center; display: none'>
                                                                                                    <asp:Button ID="buttonItemDetails" runat="server" CausesValidation="false" CommandName="ManageDetails"
                                                                                                        Text="Details" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue" />
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"></HeaderStyle>
                                                                                    <RowStyle CssClass="gridrow" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                                                            <asp:Panel ID="divData" runat="server" Style="display: none; width: 680px; border: solid 1px #808080; background-color: #6699FF; z-index: 15000; overflow: auto;">
                                                                <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 14px">
                                                                        <tr>
                                                                            <td valign="top" colspan="2" style="font-weight: bold; padding: 3px">
                                                                                <asp:Label ID="lblActionTitle" runat="server" Text="Add|Edit Items"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <table cellpadding="1" cellspacing="1" border="0" width="680px" style="border: solid 1px #808080; background-color: #CCFFFF; margin-bottom: 10px; margin-top: 5px;">
                                                                    <tr>
                                                                        <td colspan="2" align="left">
                                                                            <i>All of the fields in this section are required.</i>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                                                        HorizontalAlign="Left" Visible="true">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                                                                    Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr class="forms">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">Item Type:
                                                                        </td>
                                                                        <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                                                                            <asp:Label ID="labelItemMainType" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">Item Name:
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="textItemName" runat="server" Width="180px" AutoComplete="false"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: <%= ShowHideSubTypes() %>;">
                                                                        <td align="left">Item SubType:
                                                                        </td>
                                                                        <td align="left">
                                                                            <div id="divSubTypes" class="divborder checkbox" style="margin-top: 2px; margin-bottom: 2px; overflow: auto;">
                                                                                <asp:CheckBoxList ID="checkListSubTypes" runat="server" AutoPostBack="false" Width="180px"
                                                                                    RepeatColumns="1" Visible="false" />
                                                                                <asp:DropDownList ID="ddlSubTypes" runat="server" AutoPostBack="false" Width="180px" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">Status:
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                                <asp:ListItem Value="0">InActive</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px; padding-bottom: 5px">
                                                                            <asp:Button ID="buttonSubmitItem" runat="server" Text="Save" Width="80px" OnClick="SaveItem" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:Button ID="buttonClose" runat="server" Text="Close" Width="80px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <ajaxToolkit:ModalPopupExtender ID="mpeItemPopup" runat="server" TargetControlID="buttonRaiseItemPopup"
                                                                PopupControlID="divData" BackgroundCssClass="modalBackground" DropShadow="True"
                                                                BehaviorID="addedititems" PopupDragHandleControlID="divTitle" Enabled="True"
                                                                DynamicServicePath="" CancelControlID="buttonClose">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <!-- Confirmation Popup -->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="form pad5 center" style="text-align: left">
                                                            <asp:HiddenField ID="HItemType" runat="server" Value="299" />
                                                            <asp:HiddenField ID="HSubItemID" runat="server" />
                                                            <asp:HiddenField ID="HItemID" runat="server" />
                                                            <asp:HiddenField ID="HActionType" runat="server" Value="VIEW" />
                                                            <asp:Button ID="buttonAddItem" runat="server" Text="Add Item" Width="90px" OnClick="AddNewItem" />&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="buttonCancelAddItem" runat="server" Text="Cancel" Style="display: none" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="Button3" runat="server" OnClick="btn_close_Click" Text="Close" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ChangeTabs" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlMainItem" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="gridItemList" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonAddItem" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonSubmitItem" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="divDetailsCompnent" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="buttonRaiseDetails" runat="server" Style="display: none" />
                                            <asp:Panel ID="panelDetails" runat="server" Style="display: none; width: 680px; border: solid 1px #808080; background-color: #6699FF; z-index: 15000; overflow: auto; max-height: 480; min-height: 240">
                                                <asp:Panel ID="panelTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                                        <tr>
                                                            <td style="width: 5px; height: 19px;"></td>
                                                            <td style="width: 100%; height: 19px; white-space: nowrap">
                                                                <span style="font-weight: bold;">
                                                                    <asp:Label ID="labelTitle" runat="server">Manage Billable Details</asp:Label></span>
                                                                <label class="margin20">
                                                                    Billable:</label>
                                                                <asp:Label ID="labelSelectedBillable" runat="server" Font-Bold="true" ForeColor="Blue"
                                                                    Width="240px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <table cellpadding="1" cellspacing="1" border="0" width="680px" style="border: solid 1px #808080; background-color: #CCFFFF; margin-bottom: 10px">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <hr class="forms">
                                                            <asp:HiddenField ID="hdCustID" runat="server" />
                                                            <asp:HiddenField ID="hdItemType" runat="server" />
                                                            <asp:HiddenField ID="hdItemID" runat="server" />
                                                            <asp:HiddenField ID="hItemName" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="border pad5">
                                                            <%--<div class="border pad5">--%>
                                                            <label class="" style="margin-left: 1px">
                                                                Select Item Type:</label>
                                                            <asp:DropDownList runat="server" ID="ddlBillingItemType" Width="240px" OnSelectedIndexChanged="BillingGroupChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <%-- </div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="border pad5">
                                                            <%--  <div class="border pad5">--%>
                                                            <label class="" style="margin-left: 1px">
                                                                Find Item:</label>
                                                            <asp:TextBox ID="textSearchName" runat="server" AutoPostBack="true" Width="580px"
                                                                Font-Names="Courier New" OnTextChanged="SearchNameChanged"></asp:TextBox>
                                                            <div id="divwidthfooter" runat="server" style="z-index: 4500000" />
                                                            <ajaxToolkit:AutoCompleteExtender ID="aceSearchItems" runat="server" CompletionInterval="30"
                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="10"
                                                                BehaviorID="AutoCompleteExFooter" EnableCaching="false" FirstRowSelected="false"
                                                                MinimumPrefixLength="3" OnClientItemSelected="ace1_itemSelected" ServiceMethod="SearchItems"
                                                                TargetControlID="textSearchName" CompletionListElementID="divwidthfooter">
                                                                <Animations>
                                                                      <OnShow>
                                                                          <Sequence>
                                                                              <OpacityAction Opacity="0" />
                                                                              <HideAction Visible="true" />
                                                                              <ScriptAction Script="var behavior = $find('AutoCompleteExFooter');if (!behavior._height) { var target = behavior.get_completionList(); behavior._height = target.offsetHeight - 2; target.style.height = '0px'; }" />
                                                                              <Parallel Duration=".4">
                                                                                <FadeIn />
                                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExFooter')._height" />
                                                                              </Parallel>
                                                                          </Sequence>
                                                                      </OnShow>
                                                                      <OnHide>
                                                                          <Parallel Duration=".4">
                                                                              <FadeOut />
                                                                              <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExFooter')._height" EndValue="0" />
                                                                          </Parallel>
                                                                      </OnHide>
                                                                </Animations>
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                            <%-- </div>--%>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none">
                                                        <td style="width: 100%">
                                                            <hr class="forms">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="border pad5">
                                                            <%--<fieldset title="Details">
                                                                <legend>Billable Details</legend>--%>
                                                            <label class="" style="margin-left: 1px">
                                                                Billable Details:</label>
                                                            <div id="divdetails" class="grid" style="width: 100%;">
                                                                <div class="rounded">
                                                                    <div class="mid-outer">
                                                                        <div class="mid-inner">
                                                                            <div class="mid" style="max-height: 200px; min-height: 120px; overflow: auto">
                                                                                <div id="divGridItems">
                                                                                    <asp:GridView ID="gridSelectedItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive" PageIndex="1"
                                                                                        DataKeyNames="ItemID,ItemTypeID" ShowFooter="False" ShowHeaderWhenEmpty="True"
                                                                                        Width="100%" GridLines="None" EmptyDataText="No items for the billables" OnRowCommand="gridSelectedItems_RowCommand">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Item Name">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="labelItemName" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="400px" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Item Type">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("ItemTypeName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="15%" Wrap="false" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ShowHeader="False">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="DeleteItemButton" runat="server" CommandName="RemoveItem" ImageUrl="~/Images/del.gif"
                                                                                                        CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Remove Item" />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20px" Wrap="false" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
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
                                                            <%-- </fieldset>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px; padding-bottom: 5px">
                                                            <asp:Button ID="buttonSaveBillable" runat="server" Text="Save" Width="80px" OnClick="SaveBillableDetails" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="buttonHide" runat="server" Text="Close" Width="80px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <ajaxToolkit:ModalPopupExtender ID="mpeBillableDetails" runat="server" TargetControlID="buttonRaiseDetails"
                                                PopupControlID="panelDetails" BackgroundCssClass="modalBackground" DropShadow="True"
                                                BehaviorID="billableDetails" PopupDragHandleControlID="panelTitle" Enabled="True"
                                                CancelControlID="buttonHide" DynamicServicePath="">
                                            </ajaxToolkit:ModalPopupExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gridItemList" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlBillingItemType" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonSaveBillable" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5" AssociatedUpdatePanelID="divDetailsCompnent">
                                        <ProgressTemplate>
                                            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; z-index: 300000; vertical-align: middle;">
                                                <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080; background-color: #FFFFC0; width: 110px; height: 24px;"
                                                    cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                                            <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                                                        </td>
                                                        <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px; height: 22px; padding-left: 5px">Processing....
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" ID="tabItemSubType" Visible="false">
                                <HeaderTemplate>
                                    <b>Items SubTypes</b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel runat="server" ID="divSubTypeComponent">
                                        <ContentTemplate>
                                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="pad5 formbg border">
                                                            <div id="divSubType" class="grid">
                                                                <div class="mid-outer">
                                                                    <div class="mid-inner">
                                                                        <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto; border: 1px solid #666699;">
                                                                            <div id="div-gridview-2" class="whitebg">
                                                                                <asp:GridView ID="gridSubTypes" CssClass="datatable table-striped table-responsive" CellPadding="0" runat="server"
                                                                                    AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="SubItemTypeID,ItemTypeID"
                                                                                    EnableModelValidation="True" EmptyDataText="No Data to display" OnRowCommand="gridSubTypes_RowCommand"
                                                                                    OnRowDataBound="gridSubTypes_RowDataBound">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SubTypeName" HeaderText="SubType Name" SortExpression="SubTypeName">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="itemtypename" HeaderText="Item Type" SortExpression="itemtypename">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"></HeaderStyle>
                                                                                    <RowStyle CssClass="gridrow" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- SubType Popup -->
                                                            <asp:Button ID="btnRaiseSTPopup" runat="server" Style="display: none" />
                                                            <asp:Panel ID="divSubTypeData" runat="server" Style="display: block; width: 680px; border: solid 1px #808080; background-color: #6699FF; z-index: 15000">
                                                                <asp:Panel ID="divSubTypeTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 14px">
                                                                        <tr>
                                                                            <td valign="top" colspan="2" style="font-weight: bold; padding: 3px">
                                                                                <asp:Label ID="labelSubTypeTitle" runat="server" Text="Add|Edit Items"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <table cellpadding="1" cellspacing="1" border="0" width="680px" style="border: solid 1px #808080; background-color: #CCFFFF; margin-bottom: 10px">
                                                                    <tr>
                                                                        <td colspan="2" align="left">
                                                                            <i>All of the fields in this section are required.</i>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Panel ID="panelError2" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                                                        HorizontalAlign="Left" Visible="true">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="labelError2" runat="server" Style="font-weight: bold; color: #800000"
                                                                                    Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr class="forms">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">Item Type:
                                                                        </td>
                                                                        <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                                                                            <asp:Label ID="labelItemTypeST" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">Sub-Type Name:
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="textSubTypeName" runat="server" Width="180px" AutoComplete="false"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td align="left">Item Type:
                                                                        </td>
                                                                        <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                                                                            <asp:DropDownList ID="ddlItemType" runat="server" Width="180px" AutoPostBack="false" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">Status:
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblSubTypeStatus" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                                <asp:ListItem Value="0">InActive</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px; padding-bottom: 5px">
                                                                            <asp:Button ID="buttonSubmitST" runat="server" Text="Save" Width="80px" OnClick="buttonSubmitST_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:Button ID="buttonCloseST" runat="server" Text="Close" Width="80px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <ajaxToolkit:ModalPopupExtender ID="mpeSubTypePopup" runat="server" TargetControlID="btnRaiseSTPopup"
                                                                PopupControlID="divSubTypeData" BackgroundCssClass="modalBackground" DropShadow="True"
                                                                CancelControlID="buttonCloseST" BehaviorID="addeditsubtypes" PopupDragHandleControlID="divSubTypeTitle"
                                                                Enabled="True" DynamicServicePath="">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="form pad5 center" style="text-align: left">
                                                            <asp:Button ID="buttonAddSubType" runat="server" Text="Add Sub Type" OnClick="buttonAddSubType_Click"
                                                                Width="90px" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="Button1" runat="server" OnClick="btn_close_Click" Text="Close" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ChangeTabs" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlMainItem" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="gridSubTypes" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonAddSubType" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" ID="tabItemType">
                                <HeaderTemplate>
                                    <b>Items Types</b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="pad5 formbg border">
                                                    <asp:UpdatePanel runat="server" ID="divTypeComponent">
                                                        <ContentTemplate>
                                                            <div id="divType" class="grid">
                                                                <div class="mid-outer">
                                                                    <div class="mid-inner">
                                                                        <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto; border: 1px solid #666699;">
                                                                            <div id="div-gridview-type" class="whitebg">
                                                                                <asp:GridView ID="gridItemTypes" CssClass="datatable table-striped table-responsive" CellPadding="0" runat="server"
                                                                                    AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="ItemTypeID"
                                                                                    EmptyDataText="No Data to display" OnRowCommand="gridItemTypes_RowCommand" OnRowDataBound="gridItemTypes_RowDataBound">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Type" SortExpression="ItemName">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="labelStatus" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"></HeaderStyle>
                                                                                    <RowStyle CssClass="gridrow" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ChangeTabs" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form pad5 center" style="text-align: left">
                                                    <asp:Button ID="Button2" runat="server" OnClick="btn_close_Click" Text="Close" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
            </tbody>
        </table>
        <%--  <tr>
                    <td style="padding-left: 10px; padding-right: 15px">--%>
        <!--class="form pad5 center">-->
        <%--<asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080; background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                            <tr>
                                <td style="width: 5px; height: 19px;"></td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: White">
                                        <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;"></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="width: 48px" valign="middle" align="center">
                                <asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px"
                                    Width="32px" />
                            </td>
                            <td style="width: 100%;" valign="middle" align="center">
                                <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%; text-align: center; padding-top: 5px; padding-bottom: 5px">
                        <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                    CancelControlID="btnOkAction">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>--%>
        <%--   </td>
                </tr>--%>
    </div>

</asp:Content>
