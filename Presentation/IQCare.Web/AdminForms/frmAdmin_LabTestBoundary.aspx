<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="false"
    AutoEventWireup="True" Inherits="IQCare.Web.Admin.LabTestBoundary"
    Title="Untitled Page" Codebehind="frmAdmin_LabTestBoundary.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />--%>
    <script language="javascript" type="text/javascript">
        //function setVisibility(divUnit1,divBoundary1,divDefault1,divButtons1,visibility) 
        function setVisibility(visibility) {

            document.getElementById('<%= divUnit.ClientID %>').style.display = visibility;
            document.getElementById('<%= divBoundary.ClientID %>').style.display = visibility;
            document.getElementById('<%= divDefault.ClientID %>').style.display = visibility;
            document.getElementById('<%= divButtons.ClientID %>').style.display = visibility;

            document.getElementById('<%=txtLower.ClientID %>').value = "";
            document.getElementById('<%=txtUpper.ClientID %>').value = "";

            document.getElementById('<%= txtOperation.ClientID %>').value = "ADD";

            document.getElementById('<%=ddUnit.ClientID %>').disabled = false;
            document.getElementById('<%= ddUnit.ClientID %>').selectedIndex = 0;

            //alert(document.getElementById('<%= txtOperation.ClientID %>').value);
        }

        /*
        function Show1(ddlUnit)
        {
        alert("aaaa");
        document.getElementById(ddlUnit).style.visibility = "visible";
        }
    
        function Reset(txtLower)
        {
        alert("reset");
        alert(document.getElementById(txtLower));
        alert(document.getElementById(txtLower).value);
        document.getElementById(txtLower).value = '';
        }
        */
          
    </script>
    <%--  <form id="appointment" method="post" runat="server" enableviewstate="true" title="patientAppointment">--%>
    <div>
        <h1 class="nomargin">
        </h1>
        <br />
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                    <tr>
                        <td class="bold" align="left">
                            <asp:Label ID="lblSubTest" runat="Server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <!-- class="border pad5 whitebg"-->
                            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" width="20%">
                                        <div runat="server" id="divUnit">
                                            <label>
                                                Units:</label>
                                            <asp:DropDownList ID="ddUnit" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td class="border pad5 whitebg" id="tdDate" runat="server" visible="true" valign="top"
                                        width="60%">
                                        <div runat="server" id="divBoundary">
                                            <label>
                                                Boundary : Lower</label>
                                            <asp:TextBox ID="txtLower" MaxLength="11" runat="server" Width="70px"></asp:TextBox>
                                            <label>
                                                Upper:</label>
                                            <asp:TextBox ID="txtUpper" MaxLength="11" runat="server" Width="70px"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="border pad5 whitebg" valign="top" width="20%">
                                        <div runat="server" id="divDefault">
                                            <label>
                                                Default:</label>
                                            <asp:DropDownList ID="ddDefault" runat="server" Width="70px">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" valign="top">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height:300px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" BorderWidth="0"
                                                        GridLines="None" CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0" Width="100%"
                                                        PageSize="1" AutoGenerateColumns="false" OnSorting="grdSearchResult_Sorting"
                                                        OnRowDataBound="grdSearchResult_RowDataBound" OnSelectedIndexChanging="grdSearchResult_SelectedIndexChanging">
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
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
                        </td>
                    </tr>
                    <!--
                <tr>
                    <td class="pad5 center" align="center"  >
                        <input type=button name=btnAdd value='Add' onclick="setVisibility('inline');";> 
                        <asp:Button ID="btnClose1OLD" runat="server" Text="Close" OnClick="btnClose1_Click"  />
                    </td>
                </tr>
                -->
                    <tr>
                        <td class="pad5 center" align="center">
                            <div runat="server" id="divButtons">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <!--<asp:Button ID="btnReset" runat="server" Text="Reset"/>-->
                                <!--<input type=Reset name=btnReset value='Reset' /> -->
                                <input type="button" name="btnReset" value='Reset' onclick="setVisibility('inline');" />
                                <!--<input type=button name=btnClose2 value='Close' onclick="setVisibility('none');";/>-->
                                <asp:Button ID="btnClose1" runat="server" Text="Close" OnClick="btnClose1_Click" />
                            </div>
                        </td>
                        <td>
                            <asp:HiddenField ID="txtOperation" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <%--  <script language="javascript" type="text/javascript">
            
            if('<%= Session["a"] %>'=='1')
            {
             document.getElementById('<%= divUnit.ClientID %>').style.display = 'none';
             document.getElementById('<%= divBoundary.ClientID %>').style.display = 'none';
             document.getElementById('<%= divDefault.ClientID %>').style.display = 'none';
             document.getElementById('<%= divButtons.ClientID %>').style.display = 'none';
            }
        </script>--%>
        </div>
    </div>
</asp:Content>
