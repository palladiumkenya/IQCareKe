<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCustomReportPrint.aspx.cs"
    Inherits="IQCare.Web.Billing.frmCustomReportPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
       IQCare Reporting"></title>
    <link href="../Style/styles.css" id="main" rel="stylesheet" type="text/css" />
    <link href="../Style/Menu.css" id="menuStyle" rel="stylesheet" type="text/css" />
    <link href="../Style/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
    <link href="../Style/StyleSheet.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <script language="javascript" type="text/javascript" >

        function WindowPrint() {
            window.print();
          //  window.onfocus = function () { window.close(); }
        }
        </script>

    <form id="frmCustomreportPrint" runat="server">
    <div style="text-align:center">
    <div style="width: 90%;">
        <div id="facilityBanner">
            <asp:Image ID="facilityLevelBanner" runat="server" 
                ImageUrl="~/Images/facilityLevelBanner.png" Width="100%" /></div>
    
    <br />
    <div id="FacilityInfo">
        <h1>
            <asp:Label runat="server" Text="Facility Name Here:" ID="lblFacilityName"></asp:Label><br />
        </h1>
      <%--   <asp:Label runat="server" Text="Facility Adress Here:" ID="FacilityAdress"></asp:Label>
        <asp:Label runat="server" Text="Facility Tel Here:" ID="FacilityTel"></asp:Label><br />--%>
        <h2>
            <asp:Label runat="server" Text="Report Name Here:" ID="lblReportName"></asp:Label></h2>
        <b>
            <asp:Label runat="server" Text="Report Parameters Here:" ID="lblReportParams"></asp:Label><br />
        </b>
    </div>
       
                <div class="mid" >
                    <div class="whitebg" id="div-gridview" style="  margin-top: 5px;
                        margin-bottom: 5px;  height: 280px">
                        <asp:GridView ID="gridResult" runat="server" CellPadding="0" CellSpacing="0" EnableModelValidation="True"
                            BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="100%" UseAccessibleHeader="true"
                            AllowSorting="false" GridLines="Both" CssClass="datatable table-striped table-responsive" BorderColor="#99CCFF"
                            EmptyDataText="The report returned no data">
                            <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                Wrap="False" />
                            <RowStyle CssClass="gridrow" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            
    </div>
    </div>
    <div class="footer">
     <asp:Label runat="server" Text="Printed by Here:" ID="lblUserName"></asp:Label>
  </div>

    </form>
</body>
</html>
