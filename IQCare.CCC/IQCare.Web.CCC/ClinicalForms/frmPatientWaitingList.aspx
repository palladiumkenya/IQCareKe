<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="frmPatientWaitingList.aspx.cs"
    Inherits="IQCare.Web.Clinical.PatientWaitingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript">
    function WindowPrint() {
        window.print();
    }
</script>
<head runat="server">
    <title>Patient Waiting List</title>
    <link href="../Style/styles.css" id="main" rel="stylesheet" type="text/css" />
    <link href="../Style/Menu.css" id="menuStyle" rel="stylesheet" type="text/css" />
    <link href="../Style/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
    <link href="../Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h3 class="left">
        <asp:Label ID="lblTechnicalArea" runat="server"></asp:Label>
        Patient Waiting List
    </h3>
    <table width="100%" border="0" style="padding-top: 5px;">
        <tbody>
            <tr>
                <td colspan="2">
                    <div id="divPatientDetails" runat="server">
                        <table width="100%">
                            <tr>
                                <td class="form" align="center">
                                    <label class="patientInfo">
                                        Patient Name :
                                        <asp:Label ID="lblname" runat="server" Text=""></asp:Label></label>
                                    <label class="bold">
                                        IQ Number:
                                        <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="form bold" align="center">
                    <asp:Panel ID="thePnlIdent" runat="server">
                    </asp:Panel>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <div class="border center formbg">
        <table class="center" width="100%" style="padding-top: 5px;">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" width="50%">
                        <label>
                            Select waiting list:</label>
                        <asp:DropDownList ID="ddWList" runat="server">
                           <%-- <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Consultation" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Laboratory" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Pharmacy" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Triage" Value="5"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                    <td class="border pad5 whitebg">
                        <label>
                            Priority:</label>
                        <asp:DropDownList ID="ddPriority" runat="server" Width="100px">
                            <asp:ListItem Text="1 - Normal" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2 - Medium" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3 - High" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" align="center" colspan="2">
                        <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" Width="50px" />
                    </td>
                </tr>
                <tr>
                    <td class="border center whitebg" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 formbg" colspan="2">
                        <div class="GridView whitebg" style="cursor: pointer;">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    Patient Waiting Lists</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="grdWaitingList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        Width="100%" BorderColor="White" PageIndex="1" BorderWidth="1px" GridLines="None"
                                                        CssClass="datatable table-striped table-responsive" CellPadding="0" OnSelectedIndexChanged="grdWaitingList_SelectedIndexChanged"
                                                        DataKeyNames="WaitingListID" OnRowDataBound="grdWaitingList_RowDataBound" AutoGenerateDeleteButton="True"
                                                        ShowHeaderWhenEmpty="True" OnRowDeleting="grdWaitingList_RowDeleting">
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="List Name" DataField="ListName" />
                                                            <asp:BoundField HeaderText="Service Name" DataField="ModuleName" />
                                                            <asp:BoundField HeaderText="Time On List" DataField="TimeOnList" />
                                                            <asp:BoundField HeaderText="Added by" DataField="AddedBy" />
                                                            <asp:BoundField HeaderText="ListID" DataField="ListID" Visible="False" />
                                                            <asp:BoundField HeaderText="WaitingListID" DataField="WaitingListID" Visible="False" />
                                                            <asp:BoundField HeaderText="Priority" DataField="Priority" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom-outer">
                                        <div class="bottom-inner">
                                            <div class="bottom">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="border center whitebg" colspan="2" style="height: 10px">
                        <asp:Button ID="btnSubmit" runat="server" Font-Size="12px" Width="80px" Text="Save"
                            OnClick="btnSubmit_Click" />
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Font-Size="12px" Width="80px" Text="Close"
                            OnClick="btnBack_Click" OnClientClick="window.close()" />
                        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
