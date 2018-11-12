<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPharmacy_DrugHistory.aspx.cs" Inherits="IQCare.Web.PMSCM.frmPharmacy_DrugHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Content/bootstrap/css/gird.css" rel="stylesheet"  type="text/css" />
    <link href="../Content/bootstrap/css/round.css" rel="stylesheet" />
  

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
                                        <asp:BoundField DataField="DispensedByDate" HeaderText="Date" HeaderStyle-Width="70px"/>
                                        <asp:BoundField DataField="DrugName" HeaderText="Drug Name" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" HeaderStyle-Width="50px" />
                                        <asp:BoundField DataField="Morning" HeaderText="Morning" HeaderImageUrl="~/Images/morning1.jpg" HeaderStyle-Width="40px" />
                                        <asp:BoundField DataField="Midday" HeaderText="Afternoon" HeaderImageUrl="~/Images/midday1.jpg" HeaderStyle-Width="40px" />
                                        <asp:BoundField DataField="Evening" HeaderText="Evening" HeaderImageUrl="~/Images/evening1.jpg" HeaderStyle-Width="40px" />
                                        <asp:BoundField DataField="Night" HeaderText="Night" HeaderImageUrl="~/Images/night2.jpg" HeaderStyle-Width="40px" />
                                        <asp:BoundField DataField="Duration" HeaderText="Duration" HeaderStyle-Width="60px" />
                                        <asp:BoundField DataField="QtyPrescribed" HeaderText="Quantity Pres" HeaderStyle-Width="64px" />
                                        <asp:BoundField DataField="PillCount" HeaderText="Pill Count" HeaderStyle-Width="60px" />
                                        <asp:BoundField DataField="QtyDispensed" HeaderText="Quantity Disp" HeaderStyle-Width="65px" />
                                        <asp:BoundField DataField="comments" HeaderText="Comments" HeaderStyle-Width="170px" />
                                        <asp:BoundField DataField="PrescribedBy" HeaderText="Pres By" HeaderStyle-Width="50px" />
                                        <asp:BoundField DataField="DispensedBy" HeaderText="Disp By" HeaderStyle-Width="50px" />
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
