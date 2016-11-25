<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.LaboratoryTestMaster"
    Title="Untitled Page" Codebehind="frmAdmin_LaboratoryTestMaster.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowHideBoundary() {          
            var dataType = document.getElementById("<%= ddlDataType.ClientID %>");
            var btnselectList = document.getElementById("<%= btnselectList.ClientID %>");
            var tblnumeric = document.getElementById("<%= tblnumeric.ClientID %>");
            if (dataType[dataType.selectedIndex].text == 'Numeric') {               
                tblnumeric.style.display = 'block';
                btnselectList.style.visibility = "hidden";
            }
            else if (dataType[dataType.selectedIndex].text == 'Select List') {
             
                tblnumeric.style.display = 'none';
                btnselectList.style.visibility = "visible";
            }
            else {
               
                btnselectList.style.visibility = "hidden";
                tblnumeric.style.display = 'none';
            }

        }
        function selectwinopen() {
            window.open('frmAdmin_LaboratorySelectList.aspx?LabId=' + '<%= LabIdforselectList %>', 'Selection', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=330,height=310,scrollbars=yes');
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;

        }

   
        function checkForSecondDecimal(sender, e) {
            formatBox = document.getElementById(sender.id);
            strLen = sender.value.length;
            strVal = sender.value;
           
            hasDec = false;
            e = (e) ? e : (window.event) ? event : null;
            if (e) {
                var charCode = (e.charCode) ? e.charCode :
                            ((e.keyCode) ? e.keyCode :
                            ((e.which) ? e.which : 0));


                if ((charCode == 46) || (charCode == 110) || (charCode == 190)) {
                    for (var i = 0; i < strLen; i++) {
                        hasDec = (strVal.charAt(i) == '.');
                        if (hasDec)
                            return false;
                    }
                    
                }else
                return  isNumber(e)
            }
            return true;
        }
        
         function CheckBoxCheck(rb) {


            var gv = document.getElementById("<%=grdLabUnits.ClientID%>");

            var chk = gv.getElementsByTagName("input");

          
            for (var i = 0; i < chk.length; i++) {

                if (chk[i].type == "checkbox") {

                    if (chk[i].checked && chk[i] != rb) {

                        chk[i].checked = false;

                        break;
                    }

                }

            }
        }    

    </script>
    <%--<form id="adduser" method="post" runat="server">--%>
    <div class="center" style="padding: 5px;">
        <h3 class="margin" align="left">
            <asp:Label ID="lblH2" runat="server" Text="Add/Edit LaboratoryTest: "></asp:Label>
             <asp:Label ID="lblSubTest" runat="Server" Text=""></asp:Label>
            </h3>
              
        <div class="border center formbg">            
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border center pad5 whitebg" width="50%">
                            <label class="right30">
                                Laboratory Test :</label>
                            <asp:TextBox ID="txtLabName" runat="server" MaxLength="50" Width="160"></asp:TextBox>
                        </td>
                        <td class="border center pad5 whitebg" width="50%">
                            <label class="right30">
                                Department :</label>
                            <asp:DropDownList ID="ddDepartment" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td id="tdDataType" class="border center pad5 whitebg" runat="server">
                            <label>
                                Data Type :</label>
                            <asp:DropDownList ID="ddlDataType" runat="server">
                                <asp:ListItem Text="Text" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Numeric" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Select List" Value="2"></asp:ListItem>
                                 <asp:ListItem Text="Group" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="button" id="btnselectList" name="btnselectList" onclick="javascript:selectwinopen();return false;"
                                runat="server" value="..." />
                            <asp:TextBox ID="txtSeq" Visible="false" runat="server">1</asp:TextBox>
                        </td>
                        <asp:Label runat="server" ID="lblStatus1">
                            <td class="border center pad5 whitebg" width="33%">
                                <label class="right20">
                                    Status :</label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </asp:Label>
                    </tr>
                   
                </tbody>
            </table>
            <table id="tblnumeric" runat="server" class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                  
                  
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
                                                     <asp:GridView ID="grdLabUnits" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" 
                                                         CssClass="datatable table-striped table-responsive" DataKeyNames="ID"
                                                                        GridLines="None" OnRowCommand="grdLabUnits_RowCommand"
                                                                        OnRowDataBound="grdLabUnits_RowDataBound"
                                                                        ShowFooter="True" ShowHeaderWhenEmpty="True" 
                                                         Width="100%">
                                                                        <Columns>

                                                                         <asp:TemplateField HeaderText="ID" Visible="False">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditId" runat="server" Text='<%# Bind("ID") %>' Width="99%"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewId" runat="server" Width="99%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="2px" />
                                                                            </asp:TemplateField>
                                                                         
                                                                           <asp:TemplateField HeaderText="UnitID" Visible="False">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditUnitId" runat="server" Text='<%# Bind("UnitID") %>' Width="99%"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewUnitId" runat="server" Width="99%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUnitId" runat="server" Text='<%# Bind("UnitID") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="2px" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="SubTestID" Visible="False">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditSubTestID" runat="server" Text='<%# Bind("SubTestID") %>' Width="99%"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewSubTestID" runat="server" Width="99%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubTestID" runat="server" Text='<%# Bind("SubTestID") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="2px" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Units" Visible="True">
                                                                                <EditItemTemplate>
                                                                                    <asp:DropDownList ID="ddEditUnitName" runat="server"  Width="99%"></asp:DropDownList></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                 <asp:DropDownList ID="ddlNewUnitName" runat="server" Width="99%"></asp:DropDownList></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                     <asp:DropDownList ID="ddlUnitName" runat="server"  Width="99%"></asp:DropDownList></ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Lower Boundary">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditMinBoundaryValue" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MinBoundaryValue") %>' Width="99%" Wrap="False"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewMinBoundaryValue" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Width="90%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtMinBoundaryValue" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MinBoundaryValue") %>' Width="99%" Wrap="False"></asp:TextBox></ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Upper Boundary">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditMaxBoundaryValue" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MaxBoundaryValue") %>' Width="99%" Wrap="False"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewMaxBoundaryValue" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Width="90%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                   <asp:TextBox ID="txtMaxBoundaryValue" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MaxBoundaryValue") %>' Width="99%" Wrap="False"></asp:TextBox></ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Minimum Normal Range">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditMinNormalRange" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MinNormalRange") %>' Width="99%" Wrap="False"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewMinNormalRange" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Width="90%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtMinNormalRange" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MinNormalRange") %>' Width="99%" Wrap="False"></asp:TextBox></ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Maximum Normal Range">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditMaxNormalRange" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MaxNormalRange") %>' Width="99%" Wrap="False"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewMaxNormalRange" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Width="90%"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                   <asp:TextBox ID="txtMaxNormalRange" runat="server"  onkeypress="return checkForSecondDecimal(this,event)"
                                                                                        Text='<%# Bind("MaxNormalRange") %>' Width="99%" Wrap="False"></asp:TextBox></ItemTemplate>
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:TemplateField>

                                                                               <asp:TemplateField HeaderText="Default">
                                                                        
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="ckbDefault"  Checked='<%# Eval("DefaultUnit").ToString().Equals("Yes")?true:false %>' runat="server" AutoPostBack="false"  onclick="CheckBoxCheck(this);"></asp:CheckBox></ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30px" Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                
                                                                                   
                                                                                <FooterTemplate>
                                                                                    <asp:LinkButton ID="btnNewAdd" runat="server" CommandName="AddItem">Add</asp:LinkButton></FooterTemplate>
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
                   
            <%--    <tr>
                    <td class="pad5 center" align="center"  >
                        <input type=button name=btnAdd value='Add' onclick="setVisibility('inline');";> 
                        <asp:Button ID="btnClose1OLD" runat="server" Text="Close" OnClick="btnClose1_Click"  />
                    </td>
                </tr> --%>
               
              
                </table>
           
            <table width="100%">
                <tbody>
                    <tr>
                        <td class="pad5 center" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
        </div>
    </div>
    <script language="javascript">
    
     
        var dataType = document.getElementById("<%= ddlDataType.ClientID %>");
        var btnselectList = document.getElementById("<%= btnselectList.ClientID %>");
        var tblnumeric = document.getElementById("<%= tblnumeric.ClientID %>");

        if (dataType[dataType.selectedIndex].text == 'Numeric') {
          
            tblnumeric.style.display = 'block';
            btnselectList.style.visibility = "hidden";
        }
        else if (dataType[dataType.selectedIndex].text == 'Select List') {
          
            tblnumeric.style.display = 'none';
            btnselectList.style.visibility = "visible";
        }
        else {
           
            tblnumeric.style.display = 'none';
            btnselectList.style.visibility = "hidden";
        }
        
    </script>
</asp:Content>
