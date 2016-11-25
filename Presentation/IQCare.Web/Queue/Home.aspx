<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.Queue.Home" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div style="padding-top: 18px;">
        <h2 class="forms" align="left">
            Waiting List</h2>
    </div>
    <div id="maindiv" style="min-height: 280px;">
        <div class="container">
            <asp:UpdatePanel ID="panelHome" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row tilerow">
                        <asp:DataList ID="repeaterNavi" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <div class="col-md-3 titletext" id="servicebutton" runat="server" style='margin: 10px;
                                    background-color: <%# Eval("ResourceColor") %>'>
                                    <a href='<%# formatUrl(Eval("ResourceUrl")) %>' target="_self" class="bill" style='background-color: <%# Eval("ResourceColor") %>'>
                                        <%# Eval("ResourceName") %></a>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </ContentTemplate>
                <Triggers>                   
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
     <uc1:progressControl ID="progressControl1" runat="server" />
</asp:Content>
