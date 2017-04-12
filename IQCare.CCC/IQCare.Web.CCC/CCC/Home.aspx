<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IQCare.Web.CCC.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
  
<div class="col-md-3">  
 <div class="row">             
            <div class="col-md-12">
                <div class="col-md-7"><label class="control-label pull-left">Pending VL Tests</label></div>
                <div class="col-md-2">
                    <asp:Label runat="server" ClientIDMode="Static" ID="pendingVL" CssClass="control-label text-success pull-left"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-7"><label class="control-label pull-left">Complete VL Tests</label></div>
                <div class="col-md-2">
                    <asp:Label runat="server" ClientIDMode="Static" ID="completeVL" CssClass="control-label text-success pull-left"></asp:Label>
                </div>
            </div>
        </div>


 </div>
  <!-- ajax begin -->
    <script type="text/javascript">
      
        $(document).ready(function () {  
           

            console.log("get viral load  called");            

            $.ajax({
                url: 'WebService/LabService.asmx/GetFacilityVLPendingCount',
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    console.log(response.d);
                  
                    document.getElementById("pendingVL").innerHTML= response.d;

                }

            });

            $.ajax({
                url: 'WebService/LabService.asmx/GetFacilityVLCompleteCount',
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (response) {
                    console.log(response.d);

                    document.getElementById("completeVL").innerHTML = response.d;

                }

            });

        });
  </script>
</asp:Content>
