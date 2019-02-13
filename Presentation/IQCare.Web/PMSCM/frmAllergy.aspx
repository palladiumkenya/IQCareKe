<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAllergy.aspx.cs" Inherits="IQCare.Web.PMSCM.frmAllergy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
    <div class="GridView whitebg" style="cursor: pointer;padding-right:5px;">
        <div class="grid">
            <div class="rounded">
                <div class="mid-outer">
                    <div class="mid-inner">
                        <div class="mid" style="height: 100%; overflow: auto; width: 100%">
                            <div id="div-gridview" class="GridView whitebg">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                    Width="100%" Height="100%" BorderWidth="0px" CellPadding="0" OnDataBound="GridView1_DataBound"
                                    CssClass="table table-bordered table-hover" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="allergy" HeaderText="Allergy" />
                                        <asp:BoundField DataField="reaction" HeaderText="Reaction" />
                                        <asp:BoundField DataField="severity" HeaderText="Severity" />
                                        <asp:BoundField DataField="dateAllergy" HeaderText="DateAllergy" />
                                      
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
    </form>
</body>
</html>
