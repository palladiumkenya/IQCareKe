<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.DebitNoteControl" Codebehind="wucDebitNote.ascx.cs" %>

<asp:Panel ID="Panel1" runat="server" >
    <h2 class="forms" align="left">
        <asp:Label ID="Label1" runat="server" Text="HIV Care Program Costs"></asp:Label>
    </h2>
    <table width="100%">
        <tr>
            <th class="bold pad18">
                Patient Cost Per Debit Note
            </th>
            <th class="bold pad18">
                Graph
            </th>
        </tr>
        <tr>
            <td width="50%">
                <div class="grid">
                    <div id="div-gridview" class="gridviewDebitNote whitebg">
                        <asp:GridView ID="GridViewSummary" runat="server" AutoGenerateColumns="False" 
                            CssClass="datatable table-striped table-responsive" GridLines="None"
                            onselectedindexchanging="GridViewSummary_SelectedIndexChanging" 
                            onrowcommand="GridViewSummary_RowCommand" 
                            onrowdatabound="GridViewSummary_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="VisitDateFmt" HeaderText="Billing Date" 
                                    DataFormatString="{0:d}" ReadOnly="True" />
                                <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" ReadOnly="True" />
                                <asp:TemplateField HeaderText="Bill #">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButtonBillId" CommandName="Select" CommandArgument='<%# Eval("BillId") %>' Text='<%# Eval("BillId","{0:000000}") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LinkButtonPrint" CommandName="Print" CommandArgument='<%# Eval("BillId") %>' Text='Print'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="gridrow" />
                            <EmptyDataTemplate>
                                <span style="text-align: center">
                                No debit notes for this patient.&nbsp; Add debit notes from the report menu.
                                </span> 
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </td>
            <td width="50%" style="text-align: center">
                [----------( graph goes here )----------]
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 100%" width="50%">
                <div class="grid">
                    <div id="div1" class="gridviewDebitNote whitebg">
                         <asp:GridView ID="GridViewTran" runat="server" AutoGenerateColumns="False" CssClass="datatable table-striped table-responsive" GridLines="None" AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" HeaderStyle-Width="15%" 
                                    DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True" />
                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="25%" 
                                    ReadOnly="True" />
                                 <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" HeaderStyle-Width="15%" />
                                 <asp:BoundField DataField="Adminsitration" HeaderText="Administration" ReadOnly="True" HeaderStyle-Width="15%" />
                                <asp:BoundField DataField="Cost" HeaderText="Actual Price" ReadOnly="True" HeaderStyle-Width="15%" />
                                <asp:BoundField DataField="ChargedPrice" HeaderText="Charged Price" ReadOnly="True" HeaderStyle-Width="15%" />
                            </Columns>
                            <RowStyle CssClass="gridrow" />
                            <EmptyDataTemplate>
                                <span style="text-align: center">
                                    Select a debit note above to see details here.
                                </span>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
