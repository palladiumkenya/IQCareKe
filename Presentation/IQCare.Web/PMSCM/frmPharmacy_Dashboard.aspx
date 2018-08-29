<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmPharmacy_Dashboard.aspx.cs" Inherits="IQCare.Web.PMSCM.frmPharmacy_Dashboard" %>

<%--<%@ Register Src="../UC/UserControl_Loading.ascx" TagName="UserControl_Loading"
    TagPrefix="uc1" %>--%>
<%@ Register Src="~/UC/UserControl_Loading.ascx" TagPrefix="IQ" TagName="UserControl_Loading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
    <style type="text/css">
        #mainMaster
        {
            width: 100% !important;
        }
        #containerMaster
        {
            width: 1200px !important;
        }
        #ulAlerts
        {
            width: 1180px !important;
        }
        #divPatientInfo123
        {
            width: 1180px !important;
        }
        .style3
        {
            width: 100%;
        }
    </style>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Pharmacy Dashboard</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Store</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" Width="90%" CssClass="form-control">
                    </asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <%--<uc1:UserControl_Loading ID="UserControl_Loading1" runat="server" />--%>
                <IQ:UserControl_Loading runat="server" id="UserControl_Loading" />
            </ProgressTemplate>
        </asp:UpdateProgress>
             </div>             
 </div>
 <div class="row">
 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="border" id="chart1" runat="server">
        <%--<telerik:radhtmlchart runat="server" id="RadHtmlChart1" width="1150px" height="350px"
            skin="Silk">
            <PlotArea>
                <Series>
                    <telerik:BarSeries DataFieldY="Quantity" Name="Quantity">
                        <TooltipsAppearance Visible="false"></TooltipsAppearance>
                    </telerik:BarSeries>
                </Series>
                <XAxis DataLabelsField="DrugName">
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <MajorGridLines Visible="false"></MajorGridLines>
                </XAxis>
                <YAxis>
                    <TitleAppearance Text="Quantity"></TitleAppearance>
                    <MinorGridLines Visible="false"></MinorGridLines>
                </YAxis>
            </PlotArea>
            <Legend>
                <Appearance Visible="false"></Appearance>
            </Legend>
            <ChartTitle Text="Drugs expiring in a months time">
            </ChartTitle>
        </telerik:radhtmlchart>--%>
                            </div>
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="border" style="width:100%" id="chart2" runat="server" >
                                <br />
                                <%--<telerik:radhtmlchart runat="server" id="RadHtmlChart2" height="321px" width="500%"
                                    skin="Silk">
            <PlotArea>
                <Series>
                    <telerik:ColumnSeries Name="Appointments" DataFieldY="NoOfAppointments">
                        <Appearance>
                            <FillStyle BackgroundColor="#ffb128" />
                        </Appearance>
                        <TooltipsAppearance Color="White" />
                    </telerik:ColumnSeries>
                    <telerik:ColumnSeries Name="Visits" DataFieldY="NoOfVisits" >
                        <Appearance>
                            <FillStyle BackgroundColor="#006caa" />
                        </Appearance>
                        <TooltipsAppearance Color="White" />
                    </telerik:ColumnSeries>
                </Series>
                <XAxis DataLabelsField="Day" Color="#aaaaaa">
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <MajorGridLines Visible="false"></MajorGridLines>
                    <LabelsAppearance>
                        <TextStyle Color="#666666" />
                    </LabelsAppearance>
                </XAxis>
                <YAxis Color="#aaaaaa">
                    <LabelsAppearance>
                        <TextStyle Color="#666666" />
                    </LabelsAppearance>
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <TitleAppearance Text="No. of appointments/visits">
                        <TextStyle Color="#555555" />
                    </TitleAppearance>
                </YAxis>
            </PlotArea>
            <Legend>
            </Legend>
            <ChartTitle Text="Patient Appointments vs Visits">
            </ChartTitle>
        </telerik:radhtmlchart>--%>
                            </div>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <div class="border">
                                <div class="GridView whitebg" style="cursor: pointer;">
                                    <div class="grid" style="width:97%;">
                                        <div class="rounded">
                                            <div class="top-outer">
                                                <div class="top-inner">
                                                    <div class="top">
                                                        <h2 class="center">
                                                            Drugs about to run out</h2>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 300px; overflow: auto;">
                                                       <%-- <div id="div-gridview" class="GridView whitebg">--%>
                                                            <asp:GridView ID="grdDrugsRunningOut" runat="server" AutoGenerateColumns="False"
                                                                ShowHeaderWhenEmpty="True" Width="100%" BorderWidth="0px" CellPadding="0" CssClass="table table-bordered table-hover"
                                                                GridLines="None" DataKeyNames="Drug_pk">
                                                               
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Drug Name" DataField="DrugName" />
                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" HeaderStyle-Width="60px" />
                                                                    <asp:BoundField HeaderText="Quantity" DataField="AvailQty" HeaderStyle-Width="60px" />
                                                                    <asp:BoundField HeaderText="Reorder Level" DataField="MinStock" HeaderStyle-Width="90px" />
                                                                </Columns>
                                                            </asp:GridView>
                                                       <%-- </div>--%>
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
                            </div>
             </div>
            
 </div>
                <table class="style3">
                   
                    <tr>
                        <td width="50%" valign="top">
                            
                        </td>
                        <td width="50%" valign="top">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlStore" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    <div style="padding: 6px">
        
        <br />
        
    </div>
</asp:Content>
