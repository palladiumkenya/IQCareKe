<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmBilling_Invoice.aspx.cs"
    Inherits="IQCare.Web.Billing.Invoice" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invoice Print Out</title>
    <style>
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
    <form id="frmInvoice" runat="server">
    <div>
        <CR:CrystalReportViewer runat="server" AutoDataBind="True" ID="billingRptViewer"
            HasCrystalLogo="False" HasDrillUpButton="False" ToolPanelView="None" HasDrilldownTabs="False"
            PrintMode="ActiveX" OnUnload="billingRptViewer_Unload" />
    </div>
   </form>
</body>
</html>
