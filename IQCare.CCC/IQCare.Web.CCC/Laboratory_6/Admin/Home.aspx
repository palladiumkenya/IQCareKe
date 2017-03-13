<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.Laboratory.Admin.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 <style>
        a.bill:link, a:visited
        {
            display: inline-block;
            background-color: #f44336;
            color: white;
            padding: 14px 25px;
            text-align: center;
            text-decoration: none;
            font-size: 16px;
        }
        a.bill:hover, a:active
        {
            background-color: red;
        }
    </style>
    <div style="padding-top: 18px;">
        <h2 class="forms" align="left">
            Laboratory Home | Dashboard</h2>
    </div>
</asp:Content>
