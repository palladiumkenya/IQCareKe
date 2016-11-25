<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.CCC.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style>
        .btn-xlarge {
    padding: 18px 28px;
    font-size: 22px;
    line-height: normal;
    -webkit-border-radius: 8px;
       -moz-border-radius: 8px;
            border-radius: 8px;
    }
        
.btn-circle {
  width: 180px;
  height: 80px;
  text-align: center;
  padding: 6px 0;
  font-size: 12px;
  line-height: 1.428571429;
  border-radius: 15px;
}
.btn-circle.btn-lg {
  width: 50px;
  height: 50px;
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 25px;
}
.btn-circle.btn-xl {
  width: 70px;
  height: 70px;
  padding: 10px 16px;
  font-size: 24px;
  line-height: 1.33;
  border-radius: 35px;
}

    </style>
    <div class="container">
        <div class="jumbotron">
          <h1><span class="text-success"> GREEN CARD MOH 257</span></h1>
          <p>...</p>
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-3" style="padding-right:5px">
                    <asp:LinkButton ID="btnFind" CssClass="btn btn-primary btn-circle" runat="server"><span class="fa fa-search fa-4x"> </span> FIND PATIENT </asp:LinkButton>
                </div>
                <div class="col-md-3" style="padding-left:5px">
                    <asp:LinkButton ID="btnAddNew" CssClass="btn btn-success btn-circle" runat="server"><span class="fa fa-plus fa-4x"> </span> NEW PATIENT </asp:LinkButton>
                </div>
                <div class="col-md-3"></div>

            </div><!-- .row -->
            
        </div>
    </div>

</asp:Content>
