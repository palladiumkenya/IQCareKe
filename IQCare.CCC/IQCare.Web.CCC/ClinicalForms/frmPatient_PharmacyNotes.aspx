<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.PharmacyNotes" Codebehind="frmPatient_PharmacyNotes.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<?xml version="1.0" ?>
<html lang="en-US" xml:lang="en-US" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" href="../Style/StyleSheet.css" rel="stylesheet" />
    <link type="text/css" href="../style/styles.css" rel="stylesheet" />
    <link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
    <link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />
    <link type="text/css" href="../style/calendar.css" rel="stylesheet" />
    <style type="text/css">
        .blue
        {
            text-align: center;
        }
        </style>

        <style type="text/css">
            td.locked, th.locked 
            {
                font-size: 14px;
                font-weight: bold;
                border-right: 1px solid silver;
                position:relative;
                cursor: default;
                left: expression(document.getElementById("div-gridview").scrollLeft-2);
            }

            th {
                font-size: 14px;
                font-weight: bold;
                border-right: 1px solid silver;
                position:relative;
                cursor: default;
                /*IE5+ only*/
                top: expression(document.getElementById("div-gridview").scrollTop-2);
                z-index: 1;
            }
         </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" style="background-color: #666699; border-width: 1%" cellpadding="3"
        cellspacing="0">
        <tr>
            <td valign="top" style="color: White">
                IQCare Prescription Notes</td>
        </tr>
    </table>
    <table width="100%" cellpadding="8" cellspacing="0">
        <tr>
            <td valign="top">
                <h1 class="nomargin" id="tHeading" runat="server">
                    Prescription Notes 
                </h1>
            </td>
            <td class="blue" valign="top" align="left">
                &nbsp;</td>
        </tr>
    </table>
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="width: 25%" align="left">
                            <label class="bold">
                                Patient Name:
                                <asp:Label ID="lblpatientname" runat="server"></asp:Label></label>
                        </td>
                        <td style="width: 25%;" align="left">
                            <label class="bold">
                                Age:
                                <asp:Label ID="lblage" runat="server"></asp:Label></label>
                        </td>
                        <td style="width: 25%" align="left">
                            <label class="bold">
                                Sex:
                                <asp:Label ID="lblgender" runat="server"></asp:Label></label>
                        </td>
                        <td style="width: 25%">
                            <div class ="bold">
                                Patient Status:
                                <asp:Label ID="lblptnstatus" runat="server" Font-Bold="True"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    </tbody>
            </table>
    <br />
    <table class="center" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="center" align="center" style="padding-left:15px">
                <div class="grid">
                            <div class="rounded">
                                <div class="top-outer"><div class="top-inner"><div class="top">
                                    <h2>&nbsp;</h2>
                                </div></div></div>
                                    <div class="mid-outer"><div class="mid-inner"><div class="mid">     
                                        <div id="div-gridview" class="GridView whitebg">
                                            <asp:GridView ID="grdPharmacyNotes" runat="server" EnableViewState="False" Width="100%"
                                            OnRowDataBound="grdPharmacyNotes_RowDataBound" OnSorting="grdPharmacyNotes_Sorting"
                                            PageSize="1" CssClass="datatable table-striped table-responsive" AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" 
                                            BorderWidth="0" GridLines="None" AllowSorting="True" 
                                                EmptyDataText="No Prescription Notes for this Patient">
                                            <RowStyle CssClass="gridrow" />
                                            </asp:GridView> 
                                            </div>
                                  </div></div></div>
                                    <div class="bottom-outer"><div class="bottom-inner">
                                        <div class="bottom"></div></div></div>                   
                                        </div>      
                      </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>