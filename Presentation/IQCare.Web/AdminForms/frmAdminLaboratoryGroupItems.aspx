<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="frmAdminLaboratoryGroupItems.aspx.cs" Inherits="IQCare.Web.Admin.LaboratoryGroupItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="center" style="padding: 5px;">
        <h3 class="margin" align="left">
            <asp:Label ID="lblH2" runat="server" Text="Lab Group Name"></asp:Label></h3>
        <div class="border center formbg">
            <br>
            <div id="div-gridview" class="div-gridview whitebg" style="overflow: auto; height: 400px">
                <asp:GridView ID="grdLabs" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowSorting="True" BorderColor="#666699" Font-Bold="True" 
                    ShowHeaderWhenEmpty="True" CssClass="datatable table-striped table-responsive" >
                   
                    <Columns>
                     <asp:TemplateField Visible="False">
                         
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="lblLabTestID" runat="server" AutoPostBack="false" Text='<%#Bind("LabTestID")%>'>
                                    </asp:Label></div>
                            </ItemTemplate>
                            <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False" Width="5%" Font-Bold="True">
                            </ItemStyle>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lab Test Name">
                            
                            <ItemTemplate>
                                <div id="grdchk">
                                    <asp:CheckBox ID="chkLabTest" runat="server" AutoPostBack="false" Checked='<%#Convert.ToBoolean(Eval("member")) %>' Text='<%#Bind("LabName")%>'>
                                    </asp:CheckBox></div>
                            </ItemTemplate>
                            <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False" Width="60%" Font-Bold="True">
                            </ItemStyle>
                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                           
                            <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                           
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("LabDepartmentName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Status">
                           
                            <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                           
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
            </div>
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
</asp:Content>
