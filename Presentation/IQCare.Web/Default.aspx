<%@ Page Language="C#" AutoEventWireup="True" Inherits="_Default" CodeBehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <IQ:IQLookupTextBox runat="server" ID="dis" LabelText="My Diagnosis" LookupCategory="MST_MODDECODE" LookupName="Diagnosis1" ShowLabel="true"
          ServicePath="~/WebService/IQLookupWS.asmx" />
    </div>
    </form>
</body>
</html>
