<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.ViewGraph" Codebehind="frmClinical_ViewGraph.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div>
               <h3 align="center"><asp:Label ID="lblWeightBMI" runat="server" Text="Weight and BMI over time"></asp:Label></h3>
               <chart:WebChartViewer ID="WebChartViewerWeight" runat="server" />
            </div>

            <div>
              <h3 align="center"><asp:Label ID="lblCD4" runat="server" Text="CD4 Count and Viral Load over time"></asp:Label></h3>
              <chart:WebChartViewer ID="WebChartViewerCD4VL" runat="server" />
            </div>
    <%--        <table cellspacing="0" cellpadding="0" border="0">
               <tr><td><h3 align="center"><asp:Label ID="lblWeightBMI" runat="server" Text="Weight and BMI over time"></asp:Label></h3>
               <chart:WebChartViewer ID="WebChartViewerWeight" runat="server" Height="950px" Width="650px" />   
               <h3 align="center"><asp:Label ID="lblCD4" runat="server" Text="CD4 Count and Viral Load over time"></asp:Label></h3>
               <chart:WebChartViewer ID="WebChartViewerCD4VL" runat="server" Height="950px" Width="650px" /></td></tr>
            </table>--%>
        </div>
    </form>
</body>
</html>
