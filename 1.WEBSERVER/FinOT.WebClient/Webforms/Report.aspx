<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="RAP.WebClient.Webforms.Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rent Adjustment Program</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width" />
    <link rel="icon" type="image/png" href="~/favicon.png" />
    <!--[if IE]><link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico"/><![endif]-->
    <style type="text/css">
        html
        {
            margin: 0px;
            padding: 0px;
        }
        body
        {
            margin: 0px;
            padding: 0px;
        }
    </style>
</head>
<body>
    <form id="frmReport" runat="server">
    <div style="border: 1px dotted black">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100%"  Height="1200px" style="max-height: 1200px; overflow-y: auto;">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>