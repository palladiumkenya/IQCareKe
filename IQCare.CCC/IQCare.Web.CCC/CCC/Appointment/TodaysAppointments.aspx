<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="TodaysAppointments.aspx.cs" Inherits="IQCare.Web.CCC.Appointment.TodaysAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12 form-group">
        <div class="col-md-12 bg-primary"><span class="pull-left"></span>Appointment Details </div>
        <table class="table table-striped table-condensed" id="tblAppointment" clientidmode="Static" runat="server">
            <thead>
                <tr>
                    <th><i class="text-primary" aria-hidden="true">#</i></th>
                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Appointment Type</i> </th>
                    <th><i class="fa fa-calendar-check-o text-primary" aria-hidden="true">Appointment Date</i> </th>
                    <th><i class="fa fa-arrow-circle-o-right text-primary" aria-hidden="true">Reason</i> </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
</asp:Content>
