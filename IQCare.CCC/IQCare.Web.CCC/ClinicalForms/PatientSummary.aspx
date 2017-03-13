<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="PatientSummary.aspx.cs" Inherits="IQCare.Web.ClinicalForms.PatientSummary" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style type="text/css">
        .cpHeader
        {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS" , Verdana;
            font-size: 12px;
            cursor: pointer;
            width: 100%;
            height: 18px;
            padding: 4px;
        }
        .cpBody
        {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 100%;
            padding: 4px;
            padding-top: 7px;
        }
        .notelist li
        {
            display: inline;
            float: left;
            margin-left: 15px;
            margin-bottom: 15px;
        }
        .datapager
        {
            display: block;
            text-align: center;
            clear: both;
            font-size: medium;
        }
    </style>
    <div style="padding-left: 8px; padding-right: 8px;">
        <h1 class="margin" id="theHeader">
            Patient Summary
        </h1>
        <asp:UpdatePanel ID="upError" runat="server">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                    border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tabMain" />
            </Triggers>
        </asp:UpdatePanel>
        <div align="right">
            <asp:LinkButton ID="btnPrintOut" runat="server" Text="Print out summary"></asp:LinkButton>
        </div>
        <ajaxToolkit:TabContainer ID="tabMain" runat="server" Width="100%" ActiveTabIndex="1"
            OnDemand="true" AutoPostBack="true">
            <ajaxToolkit:TabPanel ID="tabVitals" runat="server" HeaderText="Vital Signs">
                <ContentTemplate>
                    <asp:UpdatePanel ID="divVitalComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabClinicalNotes" runat="server" HeaderText="Clinical Notes">
                <ContentTemplate>
                    <asp:UpdatePanel ID="divNoteComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListView ID="viewNoteList" runat="server">
                                <LayoutTemplate>
                                    <ul class="notelist">
                                        <asp:PlaceHolder ID="itemContainer" runat="server" />
                                    </ul>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li>
                                        <%#Eval("NoteText") %>
                                        <br />
                                        <%#Eval("NoteText")%></li>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <h4>
                                        Sorry - no notes found
                                    </h4>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabLab" runat="server" HeaderText="Lab Investigations">
                <ContentTemplate>
                    <asp:UpdatePanel ID="divLabComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabDrugs" runat="server" HeaderText="Medications">
                <ContentTemplate>
                    <asp:UpdatePanel ID="divDrugs" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        <%--<asp:UpdatePanel ID="divIdentifierComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pIdentifier" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelIdentifier" runat="server" Text="Patient Identifiers" />
                    </asp:Panel>
                    <asp:Panel ID="pidentifierBody" runat="server" CssClass="cpBody">
                        <asp:Repeater ID="repeaterIdentifiers" runat="server">
                            <HeaderTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="pad18 bold" style="width: 20%;">
                                            Identifier Name
                                        </td>
                                        <td class="pad18 bold" style="width: 30%; text-align: center">
                                            Identifier Value
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="pad18" align="left" style="vertical-align: middle">
                                        <asp:Label ID="labelParameterName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name")%>'></asp:Label>
                                    </td>
                                    <td class="pad18" align="left" style="vertical-align: middle">
                                        <asp:Label ID="hParameterId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "value") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeIdentifier" runat="server" TargetControlID="pidentifierBody"
                        CollapseControlID="pIdentifier" ExpandControlID="pIdentifier" Collapsed="true"
                        TextLabelID="labelIdentifier" CollapsedText="Click to Show Patient Identifiers.."
                        ExpandedText="Click to Hide Patient Identifiers.." CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%-- <asp:UpdatePanel ID="divVitalComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pVital" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelVital" Text="Patient Vital Signs" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pvitalBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeVital" runat="server" TargetControlID="pvitalBody"
                        CollapseControlID="pVital" ExpandControlID="pVital" Collapsed="true" TextLabelID="labelVital"
                        CollapsedText="Click to Show Patient Identifiers.." ExpandedText="Click to Hide Patient Identifiers.."
                        CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divNotesComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pNote" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelNote" runat="server" Text="ClinicalNotes" />
                    </asp:Panel>
                    <asp:Panel ID="pnoteBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeNote" runat="server" TargetControlID="pnoteBody"
                        SuppressPostBack="false" CollapseControlID="pNote" ExpandControlID="pNote" Collapsed="true"
                        TextLabelID="labelNote" CollapsedText="Click to Show Patient Identifiers.." ExpandedText="Click to Hide Patient Identifiers.."
                        CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divLabComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pLab" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelLab" runat="server" Text="Lab Investigations" />
                    </asp:Panel>
                    <asp:Panel ID="plabBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeLab" runat="server" TargetControlID="plabBody"
                        CollapseControlID="pLab" ExpandControlID="pLab" Collapsed="true" TextLabelID="labelLab"
                        SuppressPostBack="false" CollapsedText="Click to Show Patient Lab Details.."
                        ExpandedText="Click to Hide Patient Lab Details.." CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divDrugComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pDrug" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelDrug" runat="server" Text="Presecription and Medication" />
                    </asp:Panel>
                    <asp:Panel ID="pdrugBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                        SuppressPostBack="false" TargetControlID="pdrugBody" CollapseControlID="pDrug"
                        ExpandControlID="pDrug" Collapsed="true" TextLabelID="labelDrug" CollapsedText="Click to Show Patient Medication.."
                        ExpandedText="Click to Hide Patient Medication.." CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divRegimenComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pRegimen" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelRegimen" runat="server" Text="Regimen and Side effects" />
                    </asp:Panel>
                    <asp:Panel ID="pregimenBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server"
                        TargetControlID="pregimenBody" CollapseControlID="pRegimen" ExpandControlID="pRegimen"
                        Collapsed="true" TextLabelID="labelRegimen" CollapsedText="Click to Show Patient Regimen and Side Effects.."
                        ExpandedText="Click to Hide Patient Regimen and Side Effects.." CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divOIComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pAllergy" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelallergy" runat="server" Text="Allergies and OIs" />
                    </asp:Panel>
                    <asp:Panel ID="pallergyBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server"
                        TargetControlID="pallergyBody" CollapseControlID="pAllergy" ExpandControlID="pAllergy"
                        Collapsed="true" TextLabelID="labelallergy" CollapsedText="Click to Show Patient Allergies and OIS.."
                        ExpandedText="Click to Hide Patient Allergies and OIS.." CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divAppointmentComponent" runat="server">
            <ContentTemplate>
                <div class="border formbg">
                    <asp:Panel ID="pApp" runat="server" CssClass="cpHeader">
                        <asp:Label ID="labelApp" runat="server" Text="Appointment" />
                    </asp:Panel>
                    <asp:Panel ID="pappBody" runat="server" CssClass="cpBody">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
                        incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure
                        dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="server"
                        TargetControlID="pappBody" CollapseControlID="pApp" ExpandControlID="pApp" Collapsed="true"
                        TextLabelID="labelApp" CollapsedText="Click to Show Patient Identifiers.." ExpandedText="Click to Hide Patient Identifiers.."
                        CollapsedSize="0">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
