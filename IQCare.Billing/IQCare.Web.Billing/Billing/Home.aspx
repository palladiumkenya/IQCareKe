<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.Billing.Home" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style>
        a.bill:link, a:visited {
            background-color: #f44336;
            color: white;
            padding: 14px 25px;
            text-align: center;
            text-decoration: none;
            font-size: 16px;
        }

        a.bill:hover, a:active {
            background-color: red;
        }
    </style>

    <!-- .row -->
    <hr />

    <div id="maindiv" class="container-fluid">

        <div class="row tilerow" runat="server">
            <asp:Repeater ID="repeaterNavi" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 titletext" id="servicebutton" runat="server" style='padding-bottom: 20px; background-color: <%# Eval("ResourceColor") %>'>
                        <asp:LinkButton runat='server' ID='btnbill' Text='<%# showIcon(Eval("IconFont").ToString()) + "  " + Eval("ResourceName") %>' href='<%# formatUrl(Eval("ResourceUrl")) %>' CssClass='btn btn-info' Height="100%"
                            Width="100%"> </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>




</asp:Content>
