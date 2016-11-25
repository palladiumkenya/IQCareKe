<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ProgressControl.ascx.cs"
    Inherits="IQCare.Web.ProgressControl" %>
<asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
    <ProgressTemplate>
        <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;z-index:6000; filter: alpha(opacity=60);
    opacity: 0.6; -moz-opacity: 0.8;background-color: Black"  id="divProgressBar">
            <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="white-space: nowrap">
                        <span style="white-space: nowrap">&nbsp;Processing....<asp:Image runat="server" ID="imggif"
                            ImageUrl="~/Images/loading.gif" ImageAlign="AbsMiddle" Style="white-space: nowrap" /></span>
                    </td>
                </tr>
            </table>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>