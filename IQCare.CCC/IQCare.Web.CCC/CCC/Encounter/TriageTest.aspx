<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Greencard.Master" AutoEventWireup="true" CodeBehind="TriageTest.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.TriageTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div>
        <label class="form-label">Height</label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div>
        <label class="form-label">Weight</label>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
    <div>
        <asp:GridView ID="grdSearchResult" runat="server" Width="100%"
            PageSize="1" CssClass="datatable table-striped table-responsive" AutoGenerateColumns="False"
            CellPadding="0" BorderWidth="0px" GridLines="None" AllowSorting="True" DataKeyNames="Id">
            <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                HorizontalAlign="Left"></HeaderStyle>
            <RowStyle Height="30" CssClass="gridrow" />
            <Columns>

                <asp:BoundField DataField="Id" HeaderText="Vital ID" />
                <asp:BoundField DataField="Height" HeaderText="Height (cm)" DataFormatString="{0:N}" />
                <asp:BoundField DataField="Weight" HeaderText="Weight" DataFormatString="{0:N}" />
                <asp:BoundField DataField="CaptureDate" HeaderText="Capture Date" DataFormatString="{0:dd-MMM-yyyy}" />


            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
