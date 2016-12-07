<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="ServiceArea.aspx.cs" Inherits="IQCare.Web.CCC.Patient.AerviceArea" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
        <select id="serviceA" class="form-control input-sm">
            <option value="0">select</option>
        </select>
        <
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
</asp:Content>
