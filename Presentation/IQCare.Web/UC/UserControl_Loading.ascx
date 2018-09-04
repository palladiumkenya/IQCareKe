<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_Loading.ascx.cs" Inherits="IQCare.Web.UC.UserControl_Loading" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>

<style type="text/css">
    #blur
        {
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
        #progress
        {
            z-index: 200;
            background-color: White;
            position: absolute;
            top: 50%;
            left: 50%;
            border: solid 1px black;
            padding: 5px 5px 5px 5px;
            text-align: center;
        }
</style>

<div id="blur"></div>
<div id="progress">
    <img src="../Images/loading.gif" alt="Loading...." /> 
</div>