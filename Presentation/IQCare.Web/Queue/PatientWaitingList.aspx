<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="PatientWaitingList.aspx.cs" EnableEventValidation="false" Inherits="IQCare.Web.Queue.PatientWaitingList" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
    <div class="row">
         <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                        border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
              </ContentTemplate>
         </asp:UpdatePanel>
    </div><!-- .row -->

    <div class="row">        
         <div class="panel panel-primary">

              <div class="panel-heading"> <asp:Label ID="lblTechnicalArea" CssClass="col-md-6 pull-left" runat="server"></asp:Label> Patient Waiting List</div><!-- .panel-heading -->

              <div class="panel-body">
                   <asp:UpdatePanel ID="divFilterComponent" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3">
                                     <div class="form-group">
                                          <label for="ddWList" class="control-label pull-left">Select waiting list:</label>
                                          <asp:DropDownList CssClass="form-control" ID="ddWList" runat="server">
                                        </asp:DropDownList>
                                     </div><!-- .form-group-->
                                 </div><!-- .col-md-3-->

                                 <div class="col-md-4"></div><!-- .col-md-3-->

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="ddPriority"  class="control-label pull-left">Priority:</label>
                                             <asp:DropDownList ID="ddPriority" CssClass="form-control pull-right" runat="server">
                                            <asp:ListItem Text="1 - Normal" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2 - Medium" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3 - High" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div><!-- .col-md-3-->
                                </div><!-- .col-md-3-->
                                
                                <div class="col-md-2">
                                    <div class="form-group"><br />
                                         <asp:Button runat="server" CssClass="btn btn-info" ID="btnAdd" Text="Add to Waiting List" OnClick="QueuePatient" />
                                    </div><!-- .col-md-3-->
                                </div><!-- .col-md-3-->
                                <%-- <table class="center" width="100%" style="padding-top: 5px;">
                            <tbody>
                                <tr>
                                    <td style="white-space:nowrap">
                                        

                                    </td>
                                    <td style="white-space:nowrap">
                                        

                                    </td>
                                    <td align="left" valign="middle" style="white-space:nowrap">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="3">
                                    </td>
                                </tr>
                            </tbody>
                        </table>--%>
                            </div><!-- .row -->


                       </ContentTemplate>
                   </asp:UpdatePanel>
                                           
                   <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                           <table class="center" width="100%" style="padding-top: 5px;">
                                <tbody>
                                   <tr>
                                     <td class=" " colspan="2">
                                         <div class=" " style="cursor: pointer;">
                                              <div class="grid">
                                                   <div class="">
                                                        <div class="">
                                                    <div class="">
                                                        <div class=" bg-primary">
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
                                                                    CssClass="datatable" CellPadding="0" OnSelectedIndexChanged="SelectedPatientChanged"
                                                                    DataKeyNames="WaitingListID" OnRowDataBound="grdWaitingList_RowDataBound" AutoGenerateDeleteButton="True"
                                                                    ShowHeaderWhenEmpty="True" OnRowDeleting="grdWaitingList_RowDeleting" OnDataBound="grdWaitingList_DataBound">
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
                                                        <div class="">
                                                    <div class="">
                                                        <div class="">
                                                        </div>
                                                    </div>
                                                </div>
                                                    </div>
                                              </div>
                                         </div>
                                     </td>
                                  </tr>

                                   <tr>
                                     <td class=" center " colspan="2" style=" padding:10px">
                                     <hr />
                                       <asp:Button ID="btnBack" CssClass="btn btn-info" runat="server" Text="Close"  />
                                     </td>
                                   </tr>
                                </tbody>
                           </table>
                       </ContentTemplate>

                       <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                </Triggers>
                   </asp:UpdatePanel>
              </div><!-- .panel-body-->

         </div><!-- .panel -->
    </div><!-- .row -->

       <%-- <asp:UpdatePanel ID="divNotifyComponent" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: White">
                                        <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="width: 48px" valign="middle" align="center">
                                <asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px"
                                    Width="32px" />
                            </td>
                            <td style="width: 100%;" valign="middle" align="center">
                                <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                        text-align: center; padding-top: 5px; padding-bottom: 5px">
                        <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                            CausesValidation="false" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    BehaviorID="notify_bhs" PopupDragHandleControlID="pnPopup_Title" Enabled="True"
                    DynamicServicePath="">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <uc1:progressControl ID="progressControl1" runat="server" />--%>
<%--    </div>--%>
</asp:Content>
