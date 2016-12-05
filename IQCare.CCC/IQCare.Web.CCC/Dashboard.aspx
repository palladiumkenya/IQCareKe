<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="IQCare.Web.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">

        <div class="col-md-10"></div>

            <div class="col-md-2">
                <div class="list-group">

                    <a href="#" class="list-group-item disabled">
                        <span class=""></span> Green Card Portal <span class="badge"></span>
                    </a>

                    <a href="#" class="list-group-item">
                        <span class="fa fa-search fa-2x pull-left"></span> Find Patients <span class="badge">145</span>
                    </a>

                    <a href="#" class="list-group-item">
                        <span class="fa fa-eye fa-2x pull-left"></span> Active Visit(s) <span class="badge">50</span>
                    </a>

                    <a href="#" class="list-group-item">
                        <span class="fa fa-calendar-check-o fa-2x pull-left"></span> Today's Appointment <span class="badge">8</span>
                    </a>

                    <a href="#" class="list-group-item">
                        <span class="fa fa-heartbeat fa-2x pull-left"></span> Capture Vitals <span class="badge">8</span>
                    </a>

                    <a href="#" class="list-group-item">
                        <span class="fa fa-arrow-circle-o-right fa-2x pull-left"></span>Start Encounter  <span class="badge">8</span>
                    </a>

                    <a href="#" class="list-group-item">
                        <span class="fa fa-calendar fa-2x pull-left"></span>Schedule Appointment <span class="badge">8</span>
                    </a>


    </div>
            </div>

    </div><%-- .col-md-12--%>

    <script type="text/javascript">
        $(document).ready(function () {

        })
    </script>

</asp:Content>
