<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientLabsExtruder.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientLabsExtruder" %>

<div>
    <h4> <strong>Previous Labs Results:</strong> </h4>                           
    <table class="table table-striped table-condensed" id="tblPendingLabs" clientidmode="Static" runat="server">
        <thead>
            <tr >
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> # </i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Lab Test</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true">Order Reason</i> </th>
                <th> <i class="control-label text-warning pull-right " aria-hidden="true">Order Date</i> </th>
                <th> <i class="control-label text-warning pull-right" aria-hidden="true"> Result </i></th>
            </tr>
        </thead>
        <tbody>                        
        </tbody>                  
    </table>
</div>