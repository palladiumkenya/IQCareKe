<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" CodeBehind="ManageTestParameter.aspx.cs" Inherits="IQCare.Web.Laboratory.Admin.ManageTestParameter" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="../../progressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">

        $(function () {
            var mySelect = $('#<%=ddlDataType.ClientID %>');
            mySelect.change(function () {
                var dtype = $(this).val(); if (dtype == 'SELECTLIST') {
                    $("#divSelectList").css('display', '');
                    $("#divNumeric").css('display', 'none');
                    $("#divNumeric").hide();
                    $("#divSelectList").show();
                    $('#<%=hShowUnits.ClientID %>').val("none");
                    $('#<%=hShowOptions.ClientID %>').val("");
                } else if (dtype == "NUMERIC") {
                    $("#divNumeric").css('display', '');
                    $("#divSelectList").css('display', 'none');
                    $("#divSelectList").hide();
                    $("#divNumeric").show();
                    $('#<%=hShowUnits.ClientID %>').val("");
                    $('#<%=hShowOptions.ClientID %>').val("none");
                } else { return false; }
            });

        }
        );
        $(function () {
            $('#<%=btnAdd.ClientID %>').bind("click", function () {
                var value = $('#<%=textValue.ClientID %>').val();
                if (value != "") {
                    var listBox = $('#<%=lboxOptions.ClientID %>');
                    var option = $("<option />").val(value).html(value);
                    listBox.append(option);
                    $('#<%=textValue.ClientID %>').val("");
                }
                return false;
            });
        });
        $(function () {
            $('#<%=btnSave.ClientID %>').click(function () {
                var values = "";
                var selected = $('#<%=lboxOptions.ClientID %> option');
                selected.each(function () {
                    values += $(this).html() + "=" + $(this).val() + ":";
                });
                $('#<%=hOptionsAvailable.ClientID %>').val(values);
                return true;
            });
        });
        $(function () {
            $('#<%=btnDelete.ClientID %>').bind("click", function () {
                $("#<%=lboxOptions.ClientID %> option:selected").remove();
                return false;
            });
        });
        function InferReference() {
            var str = $('#<%=textParameterName.ClientID %>').val();
            str = str.replace(/\s+/g, '_').toUpperCase();
            if (str.length < 37) {
                $('#<%=textReference.ClientID %>').val(str);
            }
        }
    </script>
        <div class="row ">
        <i class="fa fa-cogs fa-3x pull-left" aria-hidden="true"></i><span class="text-capitalize pull-left glyphicon-text-size= fa-2x">
             <asp:Label ID="lblH2" runat="server" Text="Add/Edit LaboratoryTest: "><asp:Label ID="labelTestName" runat="Server" Text=""></asp:Label></asp:Label></span>
    </div>
    <hr />
    <div>
        <h3 class="margin" align="left">
           
            
            <asp:HiddenField ID="hLabTestId" runat="Server" Value="-1" />
            <asp:HiddenField ID="hMode" runat="Server" Value="NEW" />
            <asp:HiddenField ID="hShowUnits" runat="Server" Value="none" />
            <asp:HiddenField ID="hShowOptions" runat="Server" Value="none" />
            <asp:HiddenField ID="hOptionsAvailable" runat="Server" Value="" />
        </h3>
        <div class="center">
            <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="true">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border left pad5 whitebg" width="50%">
                                       <div class="col-md-12"><label class="control-label pull-left" >
                                            Parameter Name :</label></div> 
                                        <div class="col-md-8">
                                        <asp:TextBox ID="textParameterName" CssClass="form-control col-md-6" runat="server" MaxLength="250" Columns="75" onkeyup="javascript:InferReference();"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNameText" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="textParameterName" ValidChars="-_&/\%*()  ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="* Parameter Name required"
                                            ControlToValidate="textParameterName" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:Label ID="labelName" runat="server" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hParameterId" runat="Server" Value="-1" />
                                      </div>
                                    </td>
                                    <td class="border left pad5 whitebg" width="50%">
                                        <div class="col-md-12"><label class="control-label pull-left">
                                            Reference Name :</label></div>
                                        <div class="col-md-8"><asp:TextBox ID="textReference" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:Label ID="labelReference" runat="server"></asp:Label></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border left pad5 whitebg" width="50%" style="white-space: nowrap">
                                        <div class="col-md-12"><label class="pull-left control-label">
                                            Data Type :</label></div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlDataType" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Text" Value="TEXT"></asp:ListItem>
                                                <asp:ListItem Text="Numeric" Value="NUMERIC"></asp:ListItem>
                                                <asp:ListItem Text="Select List" Value="SELECTLIST"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="labelDataType" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="border left pad5 whitebg" width="50%">
                                        <div class="col-md-12"><label class="pull-left control-label">
                                            Loinc Code :</label></div>
                                        <div class="col-md-8">
                                        <asp:TextBox ID="textLoincCode" runat="server" MaxLength="50" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Label ID="labelLoincCode" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border left pad5 whitebg" width="50%" style="white-space: nowrap">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left" for="textRank"> Order (Rank) :</label>
                                         </div>
                                         <div class="col-md-8">
                                              <asp:TextBox ID="textRank" runat="server" MaxLength="5" CssClass="form-control"
                                                Text="0.00"></asp:TextBox>
                                          </div> 
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ext" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="textRank" />                                        
                                    </td>
                                    <td class="border left pad5 whitebg" width="50%">
                                    </td>
                                </tr>
                                <tr id="divSelectList" style="display: <%= svOptions %>;">
                                    <td class=" left pad5 whitebg" style="white-space: nowrap; vertical-align: middle">
                                        <div class="col-md-6"><asp:TextBox ID="textValue" CssClass="form-control" runat="server" />
                                        </div><asp:Button ID="btnAdd" CssClass="btn btn-info" Text="Add Option" runat="server" CausesValidation="false"/>
                                        <td class=" left pad5 whitebg" width="50%" style="white-space: nowrap; vertical-align: middle">
                                            <table>
                                                <tr>
                                                    <td style="white-space: nowrap; vertical-align: middle">
                                                        <asp:ListBox ID="lboxOptions" runat="server" Width="200px" Height="120" SelectionMode="Multiple">
                                                        </asp:ListBox>
                                                    </td>
                                                    <td style="white-space: nowrap; vertical-align: middle">
                                                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" Text="Remove Option" runat="server" CausesValidation="false"
                                                            Style="white-space: nowrap; margin-left: 40px; margin-top: 10px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave"  EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div id="divNumeric" class="border center formbg" style="display: <%= svUnits %>;">
                <asp:UpdatePanel ID="divNumericComponent" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblnumeric" class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                            <tr>
                                <td class="border pad5 whitebg" valign="top">
                                    <div class="grid">
                                        <div class="rounded">
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 300px; overflow: auto">
                                                        <div id="div-gridview" class="GridView whitebg">
                                                            <asp:GridView ID="gridLabUnits" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable" DataKeyNames="Id,ParameterId,UnitId"
                                                                GridLines="None" OnRowCommand="gridLabUnits_RowCommand" OnRowDataBound="gridLabUnits_RowDataBound"
                                                                ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" OnDataBound="gridLabUnits_DataBound"
                                                                OnRowCancelingEdit="gridLabUnits_RowCancelingEdit" OnRowDeleting="gridLabUnits_RowDeleting"
                                                                OnRowEditing="gridLabUnits_RowEditing" OnRowUpdating="gridLabUnits_RowUpdating">
                                                                <Columns>
                                                                  
                                                                    <asp:TemplateField HeaderText="Unit" Visible="True">
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="ddEditUnitName" CssClass="form-control" runat="server" Width="99%">
                                                                            </asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                              <div class="form-group"> 
                                                                                    <asp:DropDownList ID="ddlNewUnitName" CssClass="form-control" runat="server" Width="99%">
                                                                                    </asp:DropDownList>
                                                                            </div>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="labelUnitName"  runat="server" Text='<%# Bind("UnitName") %>' Width="99%"
                                                                                Wrap="False"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lower Boundary">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtEditMinBoundaryValue" CssClass="form-control" runat="server" Text='<%# Bind("MinBoundary") %>'
                                                                                MaxLength="10" Width="99%" Wrap="False"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteEditMinBoound" runat="server" TargetControlID="txtEditMinBoundaryValue"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtNewMinBoundaryValue" CssClass="form-control" runat="server" Width="90%" MaxLength="10"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNewtMinBoound" runat="server" TargetControlID="txtNewMinBoundaryValue"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="labelMinBoundaryValue" CssClass="form-control" runat="server" Text='<%# Bind("MinBoundary") %>'
                                                                                Width="99%" Wrap="False"></asp:Label></ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upper Boundary">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtEditMaxBoundaryValue" CssClass="form-control" runat="server" Text='<%# Bind("MaxBoundary") %>'
                                                                                MaxLength="10" Width="99%" Wrap="False" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteEditMaxBoound" runat="server" TargetControlID="txtEditMaxBoundaryValue"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txtNewMaxBoundaryValue" CssClass="form-control" runat="server" Width="90%" MaxLength="10"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNewMaxBoound" runat="server" TargetControlID="txtNewMaxBoundaryValue"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="labelMaxBoundaryValue"  runat="server" Text='<%# Bind("MaxBoundary") %>'
                                                                                Width="99%" Wrap="False"></asp:Label></ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Min Normal Range">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtEditMinNormalRange" runat="server" Text='<%# Bind("MinNormalRange") %>'
                                                                                MaxLength="10" Width="99%" Wrap="False"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteEditMinNormal" runat="server" TargetControlID="txtEditMinNormalRange"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtNewMinNormalRange" CssClass="form-control" runat="server" Width="90%" MaxLength="10"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNewMinNormal" runat="server" TargetControlID="txtNewMinNormalRange"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtMinNormalRange" runat="server" Text='<%# Bind("MinNormalRange") %>'
                                                                                Width="99%" Wrap="False"></asp:Label></ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Max Normal Range">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtEditMaxNormalRange" CssClass="form-control" runat="server" MaxLength="10" Text='<%# Bind("MaxNormalRange") %>'
                                                                                Width="99%" Wrap="False"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteEditMaxNormal" runat="server" TargetControlID="txtEditMaxNormalRange"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtNewMaxNormalRange" CssClass="form-control" runat="server" MaxLength="10" Width="90%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNewMaxNormal" runat="server" TargetControlID="txtNewMaxNormalRange"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtMaxNormalRange" runat="server" Text='<%# Bind("MaxNormalRange") %>'
                                                                                Width="99%" Wrap="False"></asp:Label></ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Detection Limit">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtEditDetectionLimit" CssClass="form-control" runat="server" MaxLength="10" Text='<%# Bind("DetectionLimit") %>'
                                                                                Width="99%" Wrap="False"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteEditLimit" runat="server" TargetControlID="txtEditDetectionLimit"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtNewDetectionLimit" CssClass="form-control" runat="server" MaxLength="10" Width="90%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNewLimit" runat="server" TargetControlID="txtNewDetectionLimit"
                                                                                FilterType="Numbers, Custom" ValidChars="." />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LabelLimit" runat="server" Text='<%# Bind("DetectionLimit") %>' Width="99%"
                                                                                Wrap="False" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Default">
                                                                        <ItemTemplate>
                                                                            <%# Eval("IsDefault").ToString().Equals("True")?"Yes":"No" %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:CheckBox ID="ckbEditDefault" Checked='<%# Eval("IsDefault").ToString().Equals("True")?true:false %>'
                                                                                runat="server" AutoPostBack="false" onclick="CheckBoxCheck(this);"></asp:CheckBox></ItemTemplate>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:CheckBox ID="ckbNewDefault" runat="server" AutoPostBack="false"></asp:CheckBox></ItemTemplate>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <EditItemTemplate>
                                                                            <asp:Button ID="buttonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                                                Text="Update" ForeColor="Blue"></asp:Button>&#160;<asp:Button ID="buttonCancelEdit"
                                                                                    runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" ForeColor="Blue">
                                                                                </asp:Button></EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnNewAdd" runat="server" CommandName="AddItem" CssClass="btn btn-info"
                                                                                Text="Add" CausesValidation="false" /></FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="white-space: nowrap">
                                                                                <asp:Button ID="buttonEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                    Text="Edit"  CssClass="btn btn-info" Visible="false"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button
                                                                                        ID="buttonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                        Text="Delete"  CssClass="btn btn-danger" CommandArgument="<%# Container.DataItemIndex %>">
                                                                                    </asp:Button></div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <RowStyle CssClass="gridrow" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div id="divAction">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td class="pad5 center" align="center">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save Lab Parameter" OnClick="SaveLabParameter" CausesValidation="true" />
                                <asp:Button ID="btnExit" runat="server" CssClass="btn btn-danger" Text="Close Window" OnClick="ExitPage" CausesValidation="false" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>              
        </div>
    </div>
</asp:Content>
