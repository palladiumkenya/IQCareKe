<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="FacilityStats" Codebehind="FacilityStats.ascx.cs" %>
<%@ Import Namespace="System.Data" %>
<asp:Panel ID="pnlStatsPlugin" runat="server" Style="width: 100%;">
    <table cellpadding="0" cellspacing="3" border="0" style="margin: 3px; border: solid 0px #C0C0C0;width:97%">
        <tr>
            <td>
                <asp:Repeater ID="rptServiceArea" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td colspan="3" style="padding-left: 5px; height: 19px; font-weight: bold; font-size:9px">
                                <asp:Label ID="ServiceName" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Name") %>' Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="PageID" runat="server" Value='<%# DataBinder.Eval( Container, "DataItem.ID"  ) %>' />
                                <asp:HiddenField ID="FeatureID" runat="server" Value='<%# DataBinder.Eval( Container, "DataItem.FeatureId"  ) %>' />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Repeater ID="rptStats" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("myRelation") %>'>
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="5px" style="width: 100%; margin-bottom: 5px"
                                            width="100%">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <%--  <div style="height: 30px;">--%>
                                            <tr style="border-bottom: solid 1px #A0A0A0;" class="gridviewFaclity whitebg">
                                                <td style="white-space: nowrap; padding-left: 20px; padding-right: 15px;height: 25px;width: 43%;">
                                                    <asp:Label ID="lblIndicatorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "[IndicatorName]")%>'
                                                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "[QueryID]")%>' Style="width: 200px; color: blue; font-size: 9pt; font-weight: bold; display: inline-block;"></asp:Label>
                                                </td>
                                                <td colspan="2" style="height: 25px;">
                                                    <asp:HiddenField ID="HT_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "[QueryID]")%>' />
                                                    <asp:Label ID="lblResult" Width="50px" runat="server" Text='<%# GetStatResult(DataBinder.Eval(Container.DataItem, "[Query]").ToString())%>'
                                                        CssClass="rightalign blue" Style="width: 50px; display: inline-block;"></asp:Label>
                                                </td>
                                            </tr>
                                    <%--    </div>--%>
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
            </td>
        </tr>
    </table>
</asp:Panel>
