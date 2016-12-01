<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" CodeBehind="FindLabOrder.aspx.cs" Inherits="IQCare.Web.Laboratory.Request.FindLabOrder" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../../Incl/quicksearch.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        function FilterGrid() {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=grdPatienOrder] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            })
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(FilterGrid);
        function resetFun() {
             var radioButtons = $('#<%=rbtlst_findOrder.ClientID%>');
            var selectedValue = radioButtons.find('input:checked').val(); // selected value
            var selectedText = radioButtons.find('input:checked').next().html();
            if (selectedValue == "Patient") window.location.href = "FindLabPatient.aspx?FormName=FindLabPatient&mnuClicked=FindLabPatient";
           // alert("Selected Text: " + selectedText + " Selected Value: " + selectedValue);
        }
        //$("[id*=rbtlst_findOrder] input").live("click", function () {

        //    var selectedValue = $(this).val();

        //    var selectedText = $(this).next().html();
        //    if (selectedValue == "Patient")
        //        alert("Selected Text: " + selectedText + " Selected Value: " + selectedValue);

        //});
    </script>
    <div>
        <h2 class="forms" align="left">
            Lab Orders</h2>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <label style="padding-left: 10px" id="lblpurpose" runat="server">
                                        Search for:</label>
                                    <asp:RadioButtonList ID="rbtlst_findOrder" runat="server" AutoPostBack="False" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="FindOrder_SelectedIndexChanged" onchange="javascript:resetFun();">
                                        <asp:ListItem Text="Pending Orders" Selected="True" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Find Patient" Value="Patient"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="ckbTodaysOrders" runat="server" Text="Today's Orders only" AutoPostBack="True"
                                        Checked="True" OnCheckedChanged="ckbTodaysOrders_CheckedChanged"></asp:CheckBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style='display:<%= ShowPatientDetail %>'>
                    <td class="pad5 formbg border" colspan="2">
                        <div class="grid" style="width: 100%;">
                            <div class="rounded">
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <label>
                                                Patient Name:
                                                <asp:Label ID="lblname" runat="server"></asp:Label>
                                            </label>
                                            &nbsp;&nbsp;
                                            <label>
                                                Sex:
                                                <asp:Label ID="lblsex" runat="server"></asp:Label>
                                            </label>
                                            &nbsp;&nbsp;
                                            <label>
                                                DOB:
                                                <asp:Label ID="lbldob" runat="server"></asp:Label>
                                            </label>
                                            &nbsp;&nbsp;<label>
                                                Facility ID:
                                                <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                                            </label>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 formbg border" colspan="2">
                        <asp:UpdatePanel ID="pendingPanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="grid" id="divOrders" style="width: 100%;">
                                    <div class="rounded">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 200px; overflow: auto">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView ID="grdPatienOrder" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                            Width="100%" BorderColor="white" PageIndex="1" BorderWidth="1" GridLines="None"
                                                            CssClass="datatable" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="grdPatienOrder_SelectedIndexChanged"
                                                            AutoGenerateSelectButton="False" DataKeyNames="Id,PatientId,ModuleId" OnRowDataBound="grdPatienOrder_RowDataBound"
                                                            OnDataBound="grdPatienOrder_DataBound" OnRowCommand="grdPatienOrder_RowCommand">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <RowStyle CssClass="gridrow" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Patient ID" DataField="Client.UniqueFacilityId" />
                                                                <asp:TemplateField HeaderText="Patient Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="labelName" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Sex" DataField="Client.Sex" />
                                                                <asp:BoundField HeaderText="DOB" DataField="Client.DateOfBirth" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="Order #" DataField="OrderNumber" />
                                                                <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                                           
                                                                 <asp:TemplateField HeaderText="Ordered By">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="labelOrderedBy" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Order Status" DataField="OrderStatus" />
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ckbTodaysOrders" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <uc1:progressControl ID="progressControl1" runat="server" />
</asp:Content>
