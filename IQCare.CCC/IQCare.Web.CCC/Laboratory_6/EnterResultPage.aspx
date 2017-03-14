<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="EnterResultPage.aspx.cs" Inherits="IQCare.Web.Laboratory.EnterResultPage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function ShowModalPopup() {
            $find("laborder_bhx").show();
            return false;
        }

        function HideModalPopup() {
            $find("laborder_bhx").hide();
            return false;
        }
        function unitSelectedChanged(combo, eventArgs) {
            var item = eventArgs.get_item();
            var detection_limit = (item.get_attributes().getAttribute("detection_limit"));
            var min_value = (item.get_attributes().getAttribute("min"));
            var max_value = (item.get_attributes().getAttribute("max"));
            var min_normal = (item.get_attributes().getAttribute("min_normal"));
            var max_normal = (item.get_attributes().getAttribute("max_normal"));
        }
    </script>
   
       
        <asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
        <asp:HiddenField ID="HLabOrderId" runat="server" Value="-1" />
        <asp:HiddenField ID="HUserId" runat="server" />
        <asp:HiddenField ID="hLabTestId" runat="server" Value="-1" />
        <asp:HiddenField ID="HLabOrderTestId" runat="server" Value="-1" />
        <asp:HiddenField ID="hdappcurrentdate" runat="server" />
        <h3 id="H1" class="margin" style="padding-left: 10px;"></h3>
        <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0; border: solid 1px #C00000; margin-bottom: 10px;"
                    HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label runat="server" ID="labelOrderNumber" CssClass="pull-left"></asp:Label>
                    <strong>
                        <asp:Label runat="server" ID="labelTestOrderStatus"></asp:Label></strong></h3>
            </div>
            <div class="panel-body">
                <div class="alert alert-info alert-dismissable pull-left col-md-12" id="notesdiv" style="display: <%= showNotes %>">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                    <asp:Label ID="labelTestNotes" runat="server"></asp:Label>
                </div>
                <asp:UpdatePanel ID="divResultComponent" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Repeater ID="repeaterResult" runat="server" OnItemDataBound="repeaterResult_ItemDataBound">
                            <HeaderTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label class="pull-left control-label"><b>Parameter Name</b></label>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="pull-left control-label"><b>Result</b></label>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="pull-left control-label"><b>Undetectable & Limit</b></label>
                                        </div>
                                        
                                        <div class="col-md-3">
                                            <label class="pull-left control-label"><b>Unit</b></label>
                                        </div>
                                    </div>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <div class="row">
                                    <div class="col-md-2" style='padding-bottom: 20px'>
                                        <label class="pull-left control-label"><%# DataBinder.Eval(Container.DataItem, "ParameterName")%></label>
                                        <asp:HiddenField ID="hParameterId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ParameterId") %>' />
                                        <asp:HiddenField ID="hResultTestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabTestId") %>' />
                                        <asp:HiddenField ID="hTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabOrderTestId") %>' />
                                        <asp:HiddenField ID="HResultDataType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ResultDataType") %>' />
                                        <asp:HiddenField ID="HResultId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </div>

                                    <div class='col-md-4'>
                                        <div id="divNumeric" style='text-align: center; white-space: nowrap; width: 100%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>

                                            <asp:TextBox ID="textResultValue" runat="server" MaxLength="10" CssClass="form-control input-sm" Text='<%# DataBinder.Eval(Container.DataItem, "ResultValue")%>'></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteValue" runat="server" Enabled="true"
                                                FilterType="Numbers,Custom" TargetControlID="textResultValue" ValidChars=".">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            &nbsp;&nbsp;&nbsp;
                                        </div>
                                        <div id="divText" style='white-space: nowrap; width: 100%; display: <%# ShowTextDiv(Eval("ResultDataType")) %>'>
                                            <asp:TextBox ID="textResultText" CssClass="form-control textarea input-sm" runat="server" Rows="3" MaxLength="255" Text='<%# DataBinder.Eval(Container.DataItem, "ResultText")%>'
                                                Width="90%"></asp:TextBox>
                                        </div>
                                        <div id="divSelect" style='white-space: nowrap; width: 100%; display: <%# ShowSelectDiv(Eval("ResultDataType")) %>'>
                                            <asp:DropDownList ID="ddlResultList" runat="Server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:CheckBox ID="checkUndetectable" aria-label="..." runat="server" Text="" Checked='<%# DataBinder.Eval(Container.DataItem, "Undetectable")%>' />

                                            </span>
                                            <asp:TextBox ID="textDetectionLimit" runat="server" MaxLength="10" placeholder="detection limit" CssClass="form-control input-sm" Text='<%# DataBinder.Eval(Container.DataItem, "DetectionLimit")%>'></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteLimit" runat="server" Enabled="true" 
                                                FilterType="Numbers,Custom" TargetControlID="textDetectionLimit" ValidChars=".">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style='display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                        <asp:DropDownList ID="ddlResultUnit" runat="server" CssClass="form-control" Width="180px" AutoPostBack="false" AppendDataBoundItems="false"></asp:DropDownList>
                                    </div>
                                </div>

                            </ItemTemplate>
                            <FooterTemplate>
                                <div class="row">
                                </div>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="divRequestComponent" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="control-label pull-left" for="txtResultNotes">
                                        Result Comments:</label>
                                    <asp:TextBox ID="txtResultNotes" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control textarea" MaxLength="255"
                                        Text=''></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group col-xs-6">
                                        <label class="required pull-left control-label" for="ddlLabReportedbyName" runat="server"
                                            id="lblreportedby">
                                            *Reported by:</label>
                                        <asp:DropDownList ID="ddlLabReportedbyName" CssClass="form-control input-small" runat="Server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="required pull-left col-sm-8 control-label" for="labReportedbyDate"
                                            id="lblreportedbydate" style="text-align: left">
                                            *Reported By Date:</label>
                                        <div class="col-sm-6 controls">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtlabReportedbyDate" class="form-control" MaxLength="11" name="labReportedbyDate"
                                                    runat="server" />
                                                <span class="input-group-addon">
                                                    <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtlabReportedbyDate.ClientID%>');"
                                                        height="22" alt="Date Helper" src="../images/cal_icon.gif" width="22"
                                                        border="0" />
                                                    <span class="smallerlabel" id="appDatespan1">(DD-MMM-YYYY)</span> </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <br />
                                <br />
                            </div>
                            <div class="row center form pad5">
                                <asp:Button ID="buttonSave" CssClass="btn btn-info" runat="server" Text="Save Lab Result" OnClick="SaveResults" />
                                <asp:Button ID="btnclose" CssClass="btn btn-default" runat="server" Text="Close"
                                    OnClick="ExitPage" />
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
  
</asp:Content>
