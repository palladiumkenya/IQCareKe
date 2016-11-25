<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.UserGroup" Title="Untitled Page"
    MaintainScrollPositionOnPostback="true" Codebehind="frmAdmin_UserGroup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--    <form id="addusergroup" method="post" runat="server">
    --%>
    <div>
        <script language="C#" runat="server" type="text/javascript">
    
            /////////////////////////////////////////////////////////////////////
            // Code Written By   : Jayanta Kumar Das
            // Written Date      : 31st Aug 2006
            // Modification Date : 
            // Description       : Add/Edit UserGroup  
            //
            /// /////////////////////////////////////////////////////////////////

        </script>
        <script language="javascript" type="text/javascript">

            function SelectRowCheckBoxes(mnchk) {
                var tmpCn = document.getElementById(mnchk).id;
                var nmstr1 = tmpCn.substr(0, tmpCn.length - 12);
                var nmstr = tmpCn.substr(0, tmpCn.length - 6);
                var chkstate = document.getElementById(nmstr + "_chkFeature").checked;
                //document.getElementById(nmstr+"_chkFeature").click();
                if (chkstate == true) {
                    //document.getElementById(nmstr+"_chkFeature").click();
                    document.getElementById(nmstr + "_chkSave").checked = true;
                    document.getElementById(nmstr + "_chkUpdate").checked = true;
                    document.getElementById(nmstr + "_chkDelete").checked = true;
                    document.getElementById(nmstr + "_chkView").checked = true;
                    document.getElementById(nmstr + "_chkPrint").checked = true;

                }
                else {
                    document.getElementById(nmstr1 + "_ctl01" + '_' + 'chkFormAll').checked = false;
                    document.getElementById(nmstr + "_chkSave").checked = false;
                    document.getElementById(nmstr + "_chkUpdate").checked = false;
                    document.getElementById(nmstr + "_chkDelete").checked = false;
                    document.getElementById(nmstr + "_chkView").checked = false;
                    document.getElementById(nmstr + "_chkPrint").checked = false;
                }
            }

            function SelectRowCheckBoxesOther(chkOther) {

                var tmpCn = document.getElementById(chkOther).id;

                var nmstr = tmpCn.substr(0, tmpCn.length - 6);
                var nmstr1 = tmpCn.substr(0, tmpCn.length - 12);

                var chkstate = document.getElementById(nmstr + "_chkFeatureOther").checked;

                if (chkstate == true) {
                    document.getElementById(nmstr + "_chkYes").checked = true;

                }
                else {
                    document.getElementById(nmstr1 + "_ctl01" + '_' + 'chkOtherAll').checked = false;
                    document.getElementById(nmstr + "_chkYes").checked = false;
                }
            }

            function SelectRowFeatureOther(chkOther) {
                //rupesh
                var tmpCn = document.getElementById(chkOther).id;

                var nmstr = tmpCn.substr(0, tmpCn.length - 6);
                var nmstr1 = tmpCn.substr(0, tmpCn.length - 12);

                var chkstate = document.getElementById(nmstr + "_chkYes").checked;

                if (chkstate == true) {
                    document.getElementById(nmstr + "_chkFeatureOther").checked = true;

                }
                else {
                    document.getElementById(nmstr1 + "_ctl01" + '_' + 'chkOtherAll').checked = false;
                    document.getElementById(nmstr + "_chkFeatureOther").checked = false;
                }
            }



            function SelectRowCheckBoxesAdmin(chkAdmin) {
                var tmpCn = document.getElementById(chkAdmin).id;
                var nmstr = tmpCn.substr(0, tmpCn.length - 6);
                var nmstr1 = tmpCn.substr(0, tmpCn.length - 12);
                var chkstate = document.getElementById(nmstr + "_chkForm").checked;
                if (chkstate == true) {
                    document.getElementById(nmstr + "_chk1Yes").checked = true;
                    //document.getElementById(nmstr1+"_ctl01_chkAllForm").checked = true;
                }
                else {
                    document.getElementById(nmstr1 + "_ctl01" + '_' + 'chkAllForm').checked = false;
                    document.getElementById(nmstr + "_chk1Yes").checked = false;
                }

            }

            function SelectRowFeatureAdmin(chkAdmin) {
                //rupesh
                var tmpCn = document.getElementById(chkAdmin).id;
                var nmstr = tmpCn.substr(0, tmpCn.length - 6);
                var nmstr1 = tmpCn.substr(0, tmpCn.length - 12);
                var chkstate = document.getElementById(nmstr + "_chk1Yes").checked;
                if (chkstate == true) {
                    document.getElementById(nmstr + "_chkForm").checked = true;
                }
                else {
                    document.getElementById(nmstr1 + "_ctl01" + '_' + 'chkAllForm').checked = false;
                    document.getElementById(nmstr + "_chkForm").checked = false;
                }

            }


            /*************To select form on clicking Header Checkbox *******************/

            function SelectHeaderCheckBox(grdForm) {
                var grd = document.getElementById(grdForm).id;
                var grdlen = document.getElementById(grdForm).rows.length;
                var chkstate = document.getElementById(grd + "_ctl01" + '_' + 'chkFormAll').checked;
                // alert(chkstate);
                var j = 0;
                for (i = 1; i < grdlen; i++) {
                    var j = i + 1;
                    if (j < 10) {
                        if (chkstate == true) {

                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkFeature').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkSave').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkUpdate').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkDelete').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkView').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkPrint').checked = true;
                        }
                        else {
                            //document.getElementById(grd+"_ctl01"+'_'+'chkFormAll').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkFeature').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkSave').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkUpdate').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkDelete').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkView').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkPrint').checked = false;
                        }
                    }

                    if (j >= 10) {
                        if (chkstate == true) {

                            document.getElementById(grd + "_ctl" + j + '_' + 'chkFeature').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkSave').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkUpdate').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkDelete').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkView').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkPrint').checked = true;
                        }
                        else {
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkFeature').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkSave').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkUpdate').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkDelete').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkView').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkPrint').checked = false;
                        }
                    }
                }
            }


            /*************To select Reports on clicking Header Checkbox *******************/

            function SelectHeaderReportCheckBox(grdReport) {
                var grd = document.getElementById(grdReport).id;
                var grdlen = document.getElementById(grdReport).rows.length;
                var chkstate = document.getElementById(grd + "_ctl01" + '_' + 'chkOtherAll').checked;

                var j = 0;
                for (i = 1; i < grdlen; i++) {
                    var j = i + 1;
                    if (j < 10) {
                        if (chkstate == true) {
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkFeatureOther').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkYes').checked = true;
                        }
                        else {
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkFeatureOther').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkYes').checked = false;
                        }
                    }

                    if (j >= 10) {
                        if (chkstate == true) {
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkFeatureOther').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkYes').checked = true;
                        }
                        else {
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkFeatureOther').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkYes').checked = false;
                        }
                    }
                }
            }


            /**************To select Admin Forms on clicking Header Checkbox *******************/

            function SelectHeaderAdminFormCheckBox(grdAdminForm) {
                var grd = document.getElementById(grdAdminForm).id;
                var grdlen = document.getElementById(grdAdminForm).rows.length;

                var chkstate = document.getElementById(grd + "_ctl01" + '_' + 'chkAllForm').checked;
                var j = 0;
                for (i = 1; i < grdlen; i++) {

                    var j = i + 1;
                    if (j < 10) {
                        if (chkstate == true) {
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkForm').checked = true;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chk1Yes').checked = true;
                        }
                        else {
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chkForm').checked = false;
                            document.getElementById(grd + "_ctl0" + j + '_' + 'chk1Yes').checked = false;
                        }
                    }
                    else if (j >= 10) {

                        if (chkstate == true) {
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkForm').checked = true;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chk1Yes').checked = true;
                        }
                        else {
                            document.getElementById(grd + "_ctl" + j + '_' + 'chkForm').checked = false;
                            document.getElementById(grd + "_ctl" + j + '_' + 'chk1Yes').checked = false;
                        }
                    }
                }
            }

            function Button1_onclick() {

            }

        </script>
        <h3 class="margin" style="padding-left: 10px;">
            <asp:Label ID="lblh3" runat="server" Text="Add User Group"></asp:Label></h3>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <table cellspacing="5" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border pad5 whitebg center">
                                <label class="right40" for="UserName">
                                    User Group Name:</label>&nbsp;
                                <asp:TextBox ID="txtusergroupname" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 formbg border">
                                <%--<div id="abc" runat="server" class="whitebg" style="overflow: auto; height: 190px">--%>
                                <div id="div-gridview" class="div-gridview whitebg" style="overflow: auto; height: 190px">
                                    <asp:GridView ID="grdUserGroupsForm" runat="server" AutoGenerateColumns="False" Width="100%"
                                        AllowSorting="True" BorderColor="#666699" Font-Bold="True" OnRowDataBound="grdUserGroupsForm_RowDataBound">
                                        <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkFormAll" runat="server" AutoPostBack="false"></asp:CheckBox>
                                                    <%---<input id="chkFormAll"  runat="server" type="checkbox" />---%>
                                                    Select All [Forms]
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div id="grdchk">
                                                        <asp:CheckBox ID="chkFeature" runat="server" AutoPostBack="false" Text='<%#Bind("FeatureName")%>'>
                                                        </asp:CheckBox></div>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False" Width="60%" Font-Bold="True">
                                                </ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="50%"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FeatureId" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Save
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSave" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Update
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkUpdate" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Delete
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    View
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkView" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Print
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPrint" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 formbg border">
                                <div id="div-gridview1" class="div-gridview1 whitebg" style="overflow: auto; height: 90px">
                                    <asp:GridView ID="grdUserGroupsOther" runat="server" AutoGenerateColumns="False"
                                        Width="100%" AllowSorting="True" BorderColor="#666699" Font-Bold="True" OnRowDataBound="grdUserGroupsOther_RowDataBound">
                                        <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%--<input id="chkOtherAll"  runat="server" type="checkbox" />----%>
                                                    <asp:CheckBox ID="chkOtherAll" runat="server"></asp:CheckBox>
                                                    Select All [Reports]
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False" Width="60%" Font-Bold="True">
                                                </ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="50%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <div id="grdchk">
                                                        <asp:CheckBox ID="chkFeatureOther" runat="server" AutoPostBack="false" Text='<%#Bind("FeatureName")%>'>
                                                        </asp:CheckBox></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FeatureId" ControlStyle-CssClass="textstylehidden" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Yes/No
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkYes" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--<asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/XMLFiles/facilitysetupother.xml">
</asp:XmlDataSource> --%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 formbg border">
                                <div id="div-gridview2" class="div-gridview2 whitebg" style="overflow: auto; height: 190px">
                                    <asp:GridView ID="grdUserGroupAdminForm" runat="server" AutoGenerateColumns="False"
                                        Width="100%" AllowSorting="True" BorderColor="#666699" Font-Bold="True" OnRowDataBound="grdUserGroupAdminForm_RowDataBound">
                                        <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%---<input id="chkAllForm" runat="server" type="checkbox" />---%>
                                                    <asp:CheckBox ID="chkAllForm" runat="server"></asp:CheckBox>
                                                    Select All [AdminForms]
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False" Width="60%" Font-Bold="True">
                                                </ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="True" Width="50%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <div id="grdchk">
                                                        <asp:CheckBox ID="chkForm" runat="server" AutoPostBack="false" Text='<%#Bind("FeatureName")%>'>
                                                        </asp:CheckBox></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FeatureId" ControlStyle-CssClass="textstylehidden" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Yes/No
                                                </HeaderTemplate>
                                                <ItemStyle CssClass="TextStyle" HorizontalAlign="Left" Wrap="False"></ItemStyle>
                                                <HeaderStyle ForeColor="#000066" CssClass="tableheaderstyle" HorizontalAlign="Left"
                                                    Wrap="False" Width="10%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk1Yes" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--<asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/XMLFiles/listofAdminForms.xml">
    </asp:XmlDataSource>
<%--<asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/XMLFiles/listofAdminForms.xml">
</asp:XmlDataSource> --%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td id="TDChkEnrolID" class="border pad5 whitebg" visible="false" runat="server">
                                <label style="font: bold; color: #333399; text-decoration: underline;" for="Special Privilege">
                                    Special Privilege:</label><br />
                                <asp:CheckBox ID="chkspenroll" runat="server" />
                                <label style="font: bold" for="Special Privilege">
                                    Enrollment No.</label><br />
                            </td>
                        </tr>
                        <tr>
                            <td id="TDCheckCareEnd" class="border pad5 whitebg" align="left" visible="false" runat="server">
                                <label style="font: bold; color: #333399; text-decoration: underline;" for="Special Privilege">
                                    Special Privilege:</label><br />
                                <asp:CheckBox ID="chkCareEndPrivilege" runat="server" />
                                <label style="font: bold" for="Special Privilege">
                                    Edit form after care end.</label><br />
                                <asp:CheckBox ID="chkpatientIdentifiers" runat="server" />
                                <label style="font: bold" for="Special Privilege">
                                    Edit Patient Identifiers.</label><br />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" style="height: 31px">
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                <asp:Button ID="btncancel" runat="server" Text="Back" OnClick="btncancel_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
