<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="Default.aspx.cs" Inherits="IQCare.Web.Home.Default" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <div class="row">
            <span class="text-capitalize pull-left glyphicon-text-size= fa-2x" id="tHeading"
                runat="server"><i class="fa fa-cubes fa-3x" aria-hidden="true"></i><span class="text-info">
                    Select Service</span></span>
        </div>
        <hr />
    </div>
    <!-- .container-fluid -->
    <asp:HiddenField runat="server" ID="HMode" />
    <%--runat="server"--%>
    <div class="container-fluid" id="maindiv">
        <div class="row tilerow" runat="server" id="servicerow">
            <asp:Repeater ID="RepeaterseriveAreas" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 titletext" id="servicebutton" runat="server" style='padding-bottom: 20px;
                        background-color: <%# Eval("ResourceColor") %>'>
                        <asp:LinkButton ID="ButtonService" runat="server" CssClass="btn btn-info" Height="100"
                            Width="100%" CommandName="LoadServiceCommand" CommandArgument='<%# Eval("ModuleId") %>'
                            Text='<%# showIcon(Eval("IconFont").ToString()) + "  " + Eval("ResourceDescription") %>'
                            OnCommand="LoadServiceCommand_OnCommand">
                                   
                        </asp:LinkButton>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
