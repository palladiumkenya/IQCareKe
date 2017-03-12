<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PrintOut.aspx.cs" Inherits="IQCare.Web.Laboratory.Reports.PrintOut" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lab Print Out</title>
    <style type="text/css">
        body
        {
            font: 13px 'Segoe UI' , Tahoma, Arial, Helvetica, sans-serif;
            background: #ddd;
            color: #333;
            margin: 0;
        }
        h1
        {
            background: #333;
            color: #fff;
            padding: 10px;
            font: 29px 'Segoe UI Light' , 'Tahoma Light' , 'Arial Light' , 'Helvetica Light' , sans-serif;
        }
        .myRow
        {
            width: auto;
            padding: 0 20px 0 20px;
            height: auto;
        }
        .myMenu
        {
            float: left;
            margin: 0 20px 0 0;
            padding: 2px;
            color: #333;
        }
        .cBlue
        {
            border-bottom: 5px solid #6B89B7;
        }
        .cSand
        {
            border-bottom: 5px solid #CCCC66;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
        HorizontalAlign="Left" Visible="false">
        <div>
            <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                Text="Error Occurred when fetching the report"></asp:Label>
        </div>
    </asp:Panel>
    <div style="padding: 8px;">
        <div class="pad18 center  form" style="text-align: center; margin-top: 5px; margin: 5px;">
            <CR:CrystalReportViewer runat="server" AutoDataBind="True" ID="reportViewer" HasCrystalLogo="False"
                HasDrillUpButton="False" ToolPanelView="None" HasDrilldownTabs="False" PrintMode="ActiveX"
                OnUnload="reportViewer_Unload" />
        </div>
        <div class="pad18 center  form" style="text-align: center; margin-top: 5px; margin: 5px;">
            <asp:Button ID="btnExitPage" runat="server" Font-Size="12px" Width="75px" Text="Close"
                ForeColor="Black" />
        </div>
    </div>
    </form>
</body>
</html>
